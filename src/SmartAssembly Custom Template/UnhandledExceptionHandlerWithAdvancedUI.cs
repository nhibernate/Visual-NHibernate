using System;
using System.Security;
using System.Windows.Forms;

using SmartAssembly.SmartExceptionsCore;

namespace SmartAssembly.SmartExceptionsWithAdvancedUI
{
	public class UnhandledExceptionHandlerWithAdvancedUI : UnhandledExceptionHandler
	{
		protected override Guid GetUserID()
		{
			const string registryString = "AnonymousID";

			try
			{
				string savedID = RegistryHelper.ReadHKLMRegistryString(registryString);

				if (savedID.Length == 0) 
				{
					Guid newID = Guid.NewGuid();
					RegistryHelper.SaveHKLMRegistryString(registryString, newID.ToString("B"));

					if (RegistryHelper.ReadHKLMRegistryString(registryString).Length > 0)
					{
						return newID;
					}
					else
					{
						return Guid.Empty;
					}
				}
				else
				{
					return new Guid(savedID);
				}
			}
			catch
			{
				return Guid.Empty;
			}
		}

		protected override void OnSecurityException(SecurityExceptionEventArgs e)
		{
			SecurityExceptionForm form = new SecurityExceptionForm(e);
			form.ShowDialog();
		}

		protected override void OnReportException(ReportExceptionEventArgs e)
		{
			if (e.Exception.Message.StartsWith("*NO_UI*"))
			{
				ExceptionReportingForm.NoUi = true;
			}
			ExceptionReportingForm form = new ExceptionReportingForm(this, e);
			form.ShowDialog();
		}

		protected override void OnFatalException(FatalExceptionEventArgs e)
		{
			MessageBox.Show(e.FatalException.ToString(), string.Format("{0} Fatal Error", ApplicationName), MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public static bool AttachApp()
		{
			try
			{
				AttachExceptionHandler(new UnhandledExceptionHandlerWithAdvancedUI());
				return true;
			}
			catch (SecurityException)
			{
				try
				{
					Application.EnableVisualStyles();
					string securityMessage = string.Format("{0} cannot initialize itself because some permissions are not granted.\n\nYou probably try to launch {0} in a partial-trust situation. It's usually the case when the application is hosted on a network share.\n\nYou need to run {0} in full-trust, or at least grant it the UnmanagedCode security permission.\n\nTo grant this application the required permission, contact your system administrator, or use the Microsoft .NET Framework Configuration tool.", UnhandledExceptionHandler.ApplicationName);
					SecurityExceptionForm form = new SecurityExceptionForm(new SecurityExceptionEventArgs(securityMessage, false));
					form.ShowInTaskbar = true;
					form.ShowDialog();
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.ToString(), string.Format("{0} Fatal Error", UnhandledExceptionHandler.ApplicationName), MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				return false;
			}
		}
	}
}
