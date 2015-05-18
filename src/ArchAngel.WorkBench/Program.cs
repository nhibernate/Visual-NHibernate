using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ArchAngel.Common;
using ArchAngel.Interfaces;

namespace ArchAngel.Workbench
{
	public static class Program
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));
		//private static FormSplash _SplashScreen;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main(string[] args)
		{
			//try
			//{
			Branding.ProductBranding = ApplicationBrand.VisualNHibernate;
			//Application.SetUnhandledExceptionMode(UnhandledExceptionMode.Automatic);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Application.ThreadException += Application_ThreadException;

			//AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

			if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				MessageBox.Show("This is not the current AppDomain, so UnhandledException will not get handled properly.");
				Environment.Exit(1);
			}
			log.Debug("");
			log.Debug("SESSION START");
			log.Debug("=============");

			//AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			//string message;
			//int daysRemaining;
			//bool errorOccurred;
			//bool demo = false;
			//Licensing.SlyceAuthorizer.LockTypes lockType;
			//Licensing.SlyceAuthorizer.SetLicenseFilename("Visual NHibernate License.SlyceLicense");
			//Licensing.SlyceAuthorizer.LicenseStates status;
			//Licensing.SlyceAuthorizer.LicenseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Branding.ProductName);

			//if (args.Length == 1 && File.Exists(args[0]) && Path.GetExtension(args[0]).ToLowerInvariant() == ".slycelicense")
			//{
			//	string officialLicenseFile = Path.Combine(Licensing.SlyceAuthorizer.LicenseFolder, "Visual NHibernate License.SlyceLicense");

			//	if (args[0].ToLowerInvariant() != officialLicenseFile.ToLowerInvariant())
			//	{
			//		Slyce.Common.Utility.DeleteFileBrute(officialLicenseFile);
			//		File.Copy(args[0], officialLicenseFile);
			//	}
			//}
			//bool licensed = Licensing.SlyceAuthorizer.IsLicensed("Visual NHibernate License.SlyceLicense", out message, out daysRemaining, out errorOccurred, out demo, out lockType, out status);
			//System.Windows.Forms.MessageBox.Show(string.Format("Program.Main.message = {0}, daysRemaining={1}, errorOccurred={2}", message, daysRemaining, errorOccurred));
			//string temp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Branding.ProductName + Path.DirectorySeparatorChar + "Temp");

			//try
			//{
			//	Slyce.Common.Utility.DeleteDirectoryBrute(temp);
			//}
			//catch
			//{
				// Do nothing. Error caused by user selecting My Documents etc for output folder. All files get copied to temp,
				// including read-only files.
			//}
			//try
			//{
			//	Slyce.Common.Utility.DeleteDirectoryBrute(Path.Combine(Path.GetTempPath(), "ArchAngel"));
			//}
			//catch
			//{
				// Do nothing
			//}

			//if (errorOccurred)
			//{
			//	message = "An error occurred with the Slyce licensing system. Please inform support@slyce.com about this error:\n\nError: " + message;
			//	MessageBox.Show(message, "Licensing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//	Application.Exit();
			//}
			//else if (licensed && !demo)
			//{
			//	if (message.Length > 0)
			//	{
			//		MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//	}
			//	FormMain mainForm = new FormMain();
			//	//ShowSplashScreen(mainForm);
			//	Application.Run(mainForm);
			//}
			//else
			//{
			//	Licensing.LicenseWizard.frmLicenseWizard form = new Licensing.LicenseWizard.frmLicenseWizard(daysRemaining, lockType, status);
			//	Application.Run(form);
			//	// Re-read the license info
			//	ArchAngel.Licensing.SlyceAuthorizer.Reset();
			//	licensed = Licensing.SlyceAuthorizer.IsLicensed("Visual NHibernate License.SlyceLicense", out message, out daysRemaining, out errorOccurred, out demo, out lockType, out status);

			//	if (Licensing.LicenseWizard.frmLicenseWizard.Result == Licensing.LicenseWizard.ScreenStart.Actions.Trial)
			//	{
			//		// Make sure user still has days left
			//		if (daysRemaining < 0)
			//		{
			//			Application.Exit();
			//			return;
			//		}
			//	}
			//	else if (Licensing.LicenseWizard.frmLicenseWizard.Result == Licensing.LicenseWizard.ScreenStart.Actions.Activate)
			//	{
			//		// Supposedly the user has activated, let's check
			//		if (!Licensing.SlyceAuthorizer.IsLicensed("Visual NHibernate License.SlyceLicense", out message, out daysRemaining, out errorOccurred, out demo, out lockType, out status))
			//		{
			//			if (!demo || (demo && daysRemaining < 0))
			//			{
			//				Application.Exit();
			//				return;
			//			}
			//		}
			//	}
			//	else if (Licensing.LicenseWizard.frmLicenseWizard.Result == Licensing.LicenseWizard.ScreenStart.Actions.None)
			//	{
			//		Application.Exit();
			//		return;
			//	}
				FormMain mainForm = new FormMain();
				//ShowSplashScreen(mainForm);
				Application.Run(mainForm);
			//}
			Slyce.Common.SyntaxEditorHelper.DeleteResources();
			//}
			//catch (Exception ex)
			//{
			//    try
			//    {
			//        Controller.ReportError(ex);
			//    }
			//    catch (Exception ex2)
			//    {
			//        string fileManifest = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "ArchAngel.FileManifest.xml");
			//        string version = Application.ProductVersion;// Slyce.Common.Controls.VersionInfo.GetCurrentVersion(fileManifest);

			//        if (string.IsNullOrEmpty(version))
			//        {
			//            version = "debug";
			//        }
			//        Slyce.Common.Utility.SubmitError(@"http://www.slyce.com/errors/reporterror.asp", Application.ProductName, version, ex2.Message, ex2);
			//    }
			//    Environment.Exit(1);
			//}
		}

		//private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		//{
		//	return Slyce.Common.Utility.FindAssembly(args.Name, new List<string>(SharedData.AssemblySearchPaths), "Workbench");
		//}

		//static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		//{
		//    Controller.ReportError((Exception)e.ExceptionObject);
		//    Environment.Exit(1);
		//}

		//static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		//{
		//    Controller.ReportError(e.Exception);
		//}

		//private static void ShowSplashScreen(IWin32Window owner)
		//{
		//    _SplashScreen = new FormSplash();
		//    //_SplashScreen.MinimumDuration = TimeSpan.FromSeconds(3);

		//    _SplashScreen.Show(owner);
		//    _SplashScreen.Update();
		//    Application.DoEvents();
		//}

		//internal static void HideSplashScreen()
		//{
		//    if (_SplashScreen != null)
		//    {
		//        _SplashScreen.Close();
		//        _SplashScreen = null;
		//    }
		//}

		//private static void OnSplashScreenPaint(object sender, PaintEventArgs e)
		//{
		//    // Draw copyright and version information
		//    Rectangle bounds = _SplashScreen.ClientRectangle;

		//    using (Font font = new Font("Tahoma", 11F, FontStyle.Regular, GraphicsUnit.Pixel))
		//    {
		//        using (StringFormat format = new StringFormat())
		//        {
		//            format.LineAlignment = StringAlignment.Far;
		//            e.Graphics.DrawString("Copyright © 2005 David M. Kean. All Rights Reserved.", font, SystemBrushes.WindowText, bounds, format);
		//            format.Alignment = StringAlignment.Far;
		//            e.Graphics.DrawString("Version: 1.0", font, SystemBrushes.WindowText, bounds, format);
		//        }
		//    }
		//}

	}
}
