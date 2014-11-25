using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Slyce.Common;

namespace ArchAngel.Licensing.LicenseWizard
{
	public partial class ScreenActivate : Interfaces.Controls.ContentItems.ContentItem
	{
		public enum ActivationTypes
		{
			Instant,
			Website,
			Email,
			ExtendTrial,
			UnlockCode
		}
		internal static ActivationTypes ActivationType = ActivationTypes.ExtendTrial;
		internal static string LicenseNumber = "";
		internal static string LicenseFile = "";
		private PanelEx SelectedPanel = null;
		private List<PanelEx> Panels = new List<PanelEx>();

		public ScreenActivate()
		{
			InitializeComponent();
			HasFinish = true;
			BackColor = Color.White;
			//radioInstant.Checked = true;
			Panels.Add(panelAuto);
			Panels.Add(panelManual);
			ShowPanel(panelAuto);
		}

		public override void OnDisplaying()
		{
			switch (ScreenStart.Action)
			{
				case ScreenStart.Actions.Activate:
					//labelDescription.Text = "Get a license file:";
					//labelSelectActivationMethod.Text = "How do you want to retrieve your license?";
					break;
				case ScreenStart.Actions.Trial:
					//labelDescription.Text = "You need to get a trial license. Note that FULL licenses are not restricted to a specific computer and can be moved between computers.";
					//labelSelectActivationMethod.Text = "How do you want to retrieve your license?";
					break;
				default:
					throw new Exception("Oops!");
			}
		}

		//public override bool Next()
		//{
		//    if (radioWebsite.Checked)
		//    {
		//        ActivationType = ActivationTypes.Website;
		//    }
		//    else if (radioInstant.Checked)
		//    {
		//        ActivationType = ActivationTypes.Instant;
		//    }
		//    else if (radioEmail.Checked)
		//    {
		//        ActivationType = ActivationTypes.Email;
		//    }

		//    #region Activate Full License
		//    if (ScreenStart.Action == ScreenStart.Actions.Activate && SlyceAuthorizer.IsValidLicenseNumber(LicenseNumber))
		//    {
		//        string hardwareId;
		//        string errorMessage;

		//        if (!SlyceAuthorizer.GetMachineId(out hardwareId, out errorMessage))
		//        {
		//            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//            return false;
		//        }
		//        string message;

		//        switch (ActivationType)
		//        {
		//            case ActivationTypes.Email:
		//                return true;
		//            case ActivationTypes.Instant:
		//                if (!SlyceAuthorizer.IsValidLicenseNumber(LicenseNumber))
		//                {
		//                    MessageBox.Show("The license number must be 15 numeric digits.", "Invalid License Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//                    return false;
		//                }
		//                ParentForm.Cursor = Cursors.WaitCursor;
		//                Refresh();

		//                if (SlyceAuthorizer.ActivateViaInternet(LicenseNumber, hardwareId, true, out message))
		//                {
		//                    MessageBox.Show("Your license was installed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
		//                    ParentForm.Cursor = Cursors.Default;
		//                    frmLicenseWizard.Result = ScreenStart.Actions.Activate;
		//                    frmLicenseWizard.CloseWizard();
		//                    return true;
		//                }

		//                MessageBox.Show("Can't unlock.\n" + message, "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//                ParentForm.Cursor = Cursors.Default;
		//                return false;

		//            case ActivationTypes.Website:
		//                System.Diagnostics.Process.Start(string.Format(@"http://www.slyce.com/Support/?action=getTrial&trialNumber={0}", hardwareId));
		//                return false;
		//            default:
		//                throw new NotImplementedException("not coded yet");
		//        }
		//    }
		//    #endregion

		//    #region Activate Trial License
		//    if (ScreenStart.Action == ScreenStart.Actions.Trial)
		//    {
		//        string hardwareId;
		//        string errorMessage;

		//        if (!SlyceAuthorizer.GetMachineId(out hardwareId, out errorMessage))
		//        {
		//            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//            return false;
		//        }
		//        switch (ActivationType)
		//        {
		//            case ActivationTypes.Email:
		//                return true;
		//            case ActivationTypes.Instant:
		//                ParentForm.Cursor = Cursors.WaitCursor;
		//                Refresh();
		//                string message;

		//                if (SlyceAuthorizer.ActivateViaInternet("", hardwareId, false, out message))
		//                {
		//                    int daysRemaining;
		//                    bool errorOccurred;
		//                    bool demo;
		//                    SlyceAuthorizer.LockTypes lockType;
		//                    SlyceAuthorizer.LicenseStates status;
		//                    Licensing.SlyceAuthorizer.Reset();
		//                    bool licensed = Licensing.SlyceAuthorizer.IsLicensed("Visual NHibernate License.SlyceLicense", out message, out daysRemaining, out errorOccurred, out demo, out lockType, out status);

		//                    if (licensed)
		//                        MessageBox.Show("License installed.", "License Installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
		//                    else if (demo && daysRemaining >= 0)
		//                        MessageBox.Show(string.Format("{0} days remaining.", daysRemaining), "Trial Installed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//                    else
		//                        MessageBox.Show("Your trial has expired.", "Trial Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);

		//                    ParentForm.Cursor = Cursors.Default;
		//                    frmLicenseWizard.Result = ScreenStart.Actions.Activate;
		//                    frmLicenseWizard.CloseWizard();
		//                    return true;
		//                }
		//                MessageBox.Show("Can't unlock.\n" + message, "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//                ParentForm.Cursor = Cursors.Default;
		//                return false;
		//            case ActivationTypes.Website:
		//                return true;
		//            default:
		//                throw new NotImplementedException("Not coded yet");
		//        }
		//    }
		//    #endregion
		//    MessageBox.Show("Please enter the Serial Number you received in your purchase confirmation email.\n(If you can't find it then contact sales@slyce.com)", "Invalid License Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//    //txtSerialNumber.Focus();
		//    //txtSerialNumber.SelectAll();
		//    return false;
		//}

		//private void radioWebsite_CheckedChanged(object sender, EventArgs e)
		//{
		//    ActivationType = ActivationTypes.Website;
		//    //ApplyVisibilities();
		//}

		//private void radioInstant_CheckedChanged(object sender, EventArgs e)
		//{
		//    ActivationType = ActivationTypes.Instant;
		//    //ApplyVisibilities();
		//}

		//private void radioEmail_CheckedChanged(object sender, EventArgs e)
		//{
		//    ActivationType = ActivationTypes.Email;
		//    //ApplyVisibilities();
		//}

		//private void label1_Click(object sender, EventArgs e)
		//{

		//}

		//private void buttonUnlock_Click(object sender, EventArgs e)
		//{
		//    if (!SlyceAuthorizer.IsValidUnlockCode(txtLicenseFile.Text))
		//    {
		//        MessageBox.Show("The unlock code must be 16 numeric digits.", "Invalid Unlock Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//        return;
		//    }
		//    string message;

		//    if (SlyceAuthorizer.ActivateManually(txtLicenseFile.Text, out message))
		//    {
		//        MessageBox.Show("ArchAngel has been successfully activated: " + message, "Activation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
		//        ParentForm.Cursor = Cursors.Default;
		//        frmLicenseWizard.Result = ScreenStart.Actions.Activate;
		//        frmLicenseWizard.CloseWizard();
		//    }
		//    else
		//    {
		//        MessageBox.Show("Can't unlock.\n" + message, "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//    }
		//}

		private void radioUnlockCode_CheckedChanged(object sender, EventArgs e)
		{
			ActivationType = ActivationTypes.UnlockCode;
		}

		private void ShowPanel(DevComponents.DotNetBar.PanelEx panel)
		{
			Utility.SuspendPainting(panelMain);
			SelectedPanel = panel;

			foreach (var p in Panels)
			{
				p.Style.BackColor1.Color = this.BackColor;
				p.Style.BackColor2.Color = this.BackColor;
			}
			panel.Style.BackColor1.Color = Color.FromArgb(90, 90, 90);
			panel.Style.BackColor2.Color = Color.FromArgb(90, 90, 90);
			panelMain.Visible = true;

			if (panel == panelAuto)
			{
				labelHeading.Text = "Auto Install";
				labelDescription.Text = "Your license will be retrieved and installed automatically. Requires an internet connection.";
			}
			else if (panel == panelManual)
			{
				labelHeading.Text = "Manual Install";
				labelDescription.Text = "The website will be opened for you. Download the license file. Double-click it to install.";
			}
			Utility.ResumePainting(panelMain);
		}

		private void panelAuto_MouseEnter(object sender, EventArgs e)
		{
			ShowPanel(panelAuto);
		}

		private void panelManual_MouseEnter(object sender, EventArgs e)
		{
			ShowPanel(panelManual);
		}

		private void btnAuto_MouseEnter(object sender, EventArgs e)
		{
			ShowPanel(panelAuto);
		}

		private void buttonManual_MouseEnter(object sender, EventArgs e)
		{
			ShowPanel(panelManual);
		}

		private void buttonManual_Click(object sender, EventArgs e)
		{
			if (ScreenStart.Action == ScreenStart.Actions.Activate)
			{
				if (!SlyceAuthorizer.IsValidLicenseNumber(LicenseNumber))
				{
					MessageBox.Show("Please enter a valid serial number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				System.Diagnostics.Process.Start(string.Format(@"http://www.slyce.com/Support/?action=getLicense&serial={0}", LicenseNumber));
			}
			else // Trial
			{
				string errorMessage;
				List<string> machineIds = SlyceAuthorizer.GetMachineIds(out errorMessage);

				if (machineIds.Count == 0)
				{
					MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				System.Diagnostics.Process.Start(string.Format(@"http://www.slyce.com/Support/?action=getTrial&trialNumber={0}", machineIds[0]));
			}
		}

		private void btnAuto_Click(object sender, EventArgs e)
		{
			ParentForm.Cursor = Cursors.WaitCursor;
			backgroundWorker1.RunWorkerAsync();
		}

		private bool InstallAuto()
		{
			#region Activate Full License
			if (ScreenStart.Action == ScreenStart.Actions.Activate)
			{
				if (!SlyceAuthorizer.IsValidLicenseNumber(LicenseNumber))
				{
					ShowMessageBox("Please enter a valid serial number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				string message;

				if (SlyceAuthorizer.ActivateViaInternet(LicenseNumber, "", "", true, out message))
				{
					LicenseMonitor.Reset();
					ShowMessageBox("Your license was installed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return true;
				}
				ShowMessageBox("Can't unlock.\n" + message, "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			#endregion

			#region Activate Trial License
			if (ScreenStart.Action == ScreenStart.Actions.Trial)
			{
				string errorMessage;
				List<string> machineIds = SlyceAuthorizer.GetMachineIds(out errorMessage);

				if (machineIds.Count == 0)
				{
					ShowMessageBox(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				string message;
				//throw new NotImplementedException("WTF!");

				if (SlyceAuthorizer.ActivateViaInternet("", machineIds[0], "oops", false, out message))
				{
					int daysRemaining;
					bool errorOccurred;
					bool demo;
					SlyceAuthorizer.LockTypes lockType;
					SlyceAuthorizer.LicenseStates status;
					Licensing.SlyceAuthorizer.Reset();
					bool licensed = Licensing.SlyceAuthorizer.IsLicensed("Visual NHibernate License.SlyceLicense", out message, out daysRemaining, out errorOccurred, out demo, out lockType, out status);

					if (licensed)
						ShowMessageBox("License installed.", "License Installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
					else if (demo && daysRemaining >= 0)
						ShowMessageBox(string.Format("{0} days remaining.", daysRemaining), "Trial Installed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					else
						ShowMessageBox("Your trial has expired.", "Trial Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return true;
				}
				ShowMessageBox("Can't unlock.\n" + message, "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			#endregion

			return false;
		}

		private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			e.Result = InstallAuto();
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			ParentForm.Cursor = Cursors.Default;

			if ((bool)e.Result)
			{
				frmLicenseWizard.Result = ScreenStart.Actions.Activate;
				frmLicenseWizard.CloseWizard();
			}
		}

		private void ShowMessageBox(string message, string title, MessageBoxButtons button, MessageBoxIcon icon)
		{
			if (InvokeRequired)
			{
				MethodInvoker invoker = () => ShowMessageBox(message, title, button, icon);
				this.Invoke(invoker);
				return;
			}
			MessageBox.Show(this, message, title, button, icon);
		}
	}
}
