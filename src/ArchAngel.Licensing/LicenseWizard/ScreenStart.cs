using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Slyce.Common;

namespace ArchAngel.Licensing.LicenseWizard
{
	public partial class ScreenStart : Interfaces.Controls.ContentItems.ContentItem
	{
		public enum Images
		{
			StartTrial,
			StartTrial_Gray,
			SerialNumber,
			SerialNumber_Gray,
			License,
			License_Gray,
			Purchase,
			Purchase_Gray,
			Extend,
			Extend_Gray
		}
		public enum Actions
		{
			None,
			Trial,
			Activate,
			Purchase,
			ExtendTrial
		}
		internal static Actions Action = Actions.None;
		private int TotalDays = 30;
		private int DaysPassed;
		private PanelEx SelectedPanel = null;
		private List<PanelEx> Panels = new List<PanelEx>();
		private List<PictureBox> PictureBoxes = new List<PictureBox>();

		public ScreenStart()
		{
			InitializeComponent();
			HasPrev = false;
			BackColor = Color.White;

			Panels.Add(panelContinueTrial);
			Panels.Add(panelSerial);
			Panels.Add(panelLicenseFile);
			Panels.Add(panelBuy);
			Panels.Add(panelExtendTrial);

			PictureBoxes.Add(pictureBoxStartTrial);
			PictureBoxes.Add(pictureBoxSerial);
			PictureBoxes.Add(pictureBoxLicense);
			PictureBoxes.Add(pictureBoxPurchase);
			PictureBoxes.Add(pictureBoxExtend);

			Populate();
		}

		private void Populate()
		{
			DaysPassed = 30 - frmLicenseWizard.DaysRemaining;
			panelExtendTrial.Visible = false;
			textBoxSerial.Text = LicenseMonitor.CurrentLicense.Lic == null ? "" : LicenseMonitor.CurrentLicense.Lic.Serial;
			SelectedPanel = panelContinueTrial;

			ShowPanel(panelContinueTrial);
			Refresh();

			string trialWords;

			switch (frmLicenseWizard.LockType)
			{
				case SlyceAuthorizer.LockTypes.Date:
				case SlyceAuthorizer.LockTypes.Days:
					trialWords = "trial period";
					break;
				default:
					trialWords = "30-day trial period";
					break;
			}
			switch (frmLicenseWizard.LicenseStatus)
			{
				case SlyceAuthorizer.LicenseStates.HardwareNotMatched:
					labelWarning.Text = "The Hardware ID in your license does not match your computer.";
					break;
				case SlyceAuthorizer.LicenseStates.InvalidSignature:
					labelWarning.Text = "Your license file has been tampered with, and is therefore invalid.";
					break;
				case SlyceAuthorizer.LicenseStates.Revoked:
					labelWarning.Text = "Your serial number has been revoked. Please contact support@slyce.com for a replacement.";
					break;
				default:
					labelWarning.Text = "";
					break;
			}
			if (frmLicenseWizard.DaysRemaining == 1)
			{
				lblMessage.Text = string.Format("Trial expires tomorrow", trialWords);
				panelExtendTrial.Visible = true;
			}
			else if (frmLicenseWizard.DaysRemaining > 0)
			{
				panelContinueTrial.Text = "Continue trial";
				lblMessage.Text = string.Format("Trial: {0} days remaining", frmLicenseWizard.DaysRemaining, trialWords);
			}
			else if (frmLicenseWizard.DaysRemaining <= 0 && (frmLicenseWizard.LockType == SlyceAuthorizer.LockTypes.Date || frmLicenseWizard.LockType == SlyceAuthorizer.LockTypes.Days))
			{
				panelContinueTrial.Text = "Continue trial";
				panelContinueTrial.Enabled = false;
				ShowPanel(panelSerial);
				panelExtendTrial.Visible = true;

				if (LicenseMonitor.CurrentLicense.Lic == null)
				{
					lblMessage.Text = "No licenses found";
					buttonRefreshLicense.Visible = false;
					buttonRemoveLicense.Visible = false;
				}
				else
				{
					if (LicenseMonitor.CurrentLicense.Lic.Type == License.LicenseTypes.Trial)
						lblMessage.Text = string.Format("Trial expired", trialWords);
					else
						lblMessage.Text = string.Format("License expired", trialWords);

					buttonRefreshLicense.Visible = true;
					buttonRemoveLicense.Visible = true;
				}
			}
			else
			{
				panelContinueTrial.Text = "Start trial";

				if (frmLicenseWizard.LockType == SlyceAuthorizer.LockTypes.None)
				{
					lblMessage.Text = "No license file found";
				}
			}
			labelSerial.Top = panelMain.Height - buttonGetNewTrialLicenseAuto.Height - 10 - textBoxSerial.Height - 5;
			labelSerial.Left = 10;
			textBoxSerial.Top = labelSerial.Top - 2;
			textBoxSerial.Left = labelSerial.Right + 5;
			textBoxSerial.Width = panelMain.Width - textBoxSerial.Left - 10;
			textBoxSerial.BringToFront();
			buttonGetNewTrialLicenseAuto.Left = textBoxSerial.Left;
			buttonGetNewTrialLicenseAuto.Top = panelMain.Height - buttonGetNewTrialLicenseAuto.Height - 10;
			buttonGetNewTrialLicenseManual.Left = buttonGetNewTrialLicenseAuto.Right + 5;
			buttonGetNewTrialLicenseManual.Top = panelMain.Height - buttonGetNewTrialLicenseAuto.Height - 10;
			buttonContinue.Location = buttonGetNewTrialLicenseAuto.Location;
			buttonInstallLicenseFile.Location = buttonGetNewTrialLicenseAuto.Location;
			buttonBuyNow.Location = buttonGetNewTrialLicenseAuto.Location;
			buttonExtendTrial.Location = buttonGetNewTrialLicenseAuto.Location;

			textBoxLicenseFile.Location = labelSerial.Location;
			textBoxLicenseFile.Width = panelMain.Width - textBoxLicenseFile.Left - buttonBrowse.Width - 15;
			textBoxLicenseFile.BringToFront();
			buttonBrowse.Left = textBoxLicenseFile.Right + 5;
			buttonBrowse.Top = textBoxLicenseFile.Top;
			buttonBrowse.BringToFront();
		}

		private void pnlRemainingDays_Paint(object sender, PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;

			if (TotalDays == 0)
			{
				TotalDays = 30;
			}
			int daysWidth = (int)(pnlRemainingDays.Width * (double)DaysPassed / TotalDays);

			if (daysWidth == 0)
			{
				daysWidth = 1;
			}
			Rectangle redRectTop = new Rectangle(0, 0, daysWidth, pnlRemainingDays.Height / 2 + 1);
			Rectangle greenRectTop = new Rectangle(daysWidth, 0, pnlRemainingDays.Width - daysWidth, pnlRemainingDays.Height / 2 + 1);
			Rectangle redRectBottom = new Rectangle(0, pnlRemainingDays.Height / 2, daysWidth, pnlRemainingDays.Height / 2);
			Rectangle greenRectBottom = new Rectangle(daysWidth, pnlRemainingDays.Height / 2, pnlRemainingDays.Width - daysWidth, pnlRemainingDays.Height / 2);
			LinearGradientBrush redBrushTop = redRectTop.Width > 0 ? new LinearGradientBrush(redRectTop, Color.Wheat, Color.Red, LinearGradientMode.Vertical) : null;
			LinearGradientBrush greenBrushTop = greenRectTop.Width > 0 ? new LinearGradientBrush(greenRectTop, Color.GreenYellow, Color.Green, LinearGradientMode.Vertical) : null;
			LinearGradientBrush redBrushBottom = redRectBottom.Width > 0 ? new LinearGradientBrush(redRectBottom, Color.Red, Color.Wheat, LinearGradientMode.Vertical) : null;
			LinearGradientBrush greenBrushBottom = greenRectBottom.Width > 0 ? new LinearGradientBrush(greenRectBottom, Color.Green, Color.GreenYellow, LinearGradientMode.Vertical) : null;

			if (DaysPassed == 0)
			{
				redBrushBottom = greenBrushBottom;
				redBrushTop = greenBrushTop;
			}
			if (redRectBottom.Width > 0 && redBrushBottom != null)
			{
				g.FillRectangle(redBrushBottom, redRectBottom);
			}
			if (redRectTop.Width > 0 && redBrushTop != null)
			{
				g.FillRectangle(redBrushTop, redRectTop);
			}
			if (greenRectBottom.Width > 0 && greenBrushBottom != null)
			{
				g.FillRectangle(greenBrushBottom, greenRectBottom);
			}
			if (greenRectTop.Width > 0 && greenBrushTop != null)
			{
				g.FillRectangle(greenBrushTop, greenRectTop);
			}
			g.Flush();

			if (redBrushBottom != null) { redBrushBottom.Dispose(); }
			if (redBrushTop != null) { redBrushTop.Dispose(); }
			if (greenBrushBottom != null) { greenBrushBottom.Dispose(); }
			if (greenBrushTop != null) { greenBrushTop.Dispose(); }
		}

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
			ScreenActivate2.LicenseNumber = textBoxSerial.Text;
		}

		public override void OnDisplaying()
		{
			textBoxSerial.Text = LicenseMonitor.CurrentLicense.Lic == null ? "" : LicenseMonitor.CurrentLicense.Lic.Serial;// ScreenActivate.LicenseNumber;
			textBoxLicenseFile.Text = ScreenActivate2.LicenseFile;
			ShowPanel(SelectedPanel);
		}

		private void textBoxLicenseFile_TextChanged(object sender, System.EventArgs e)
		{
			ScreenActivate2.LicenseFile = textBoxLicenseFile.Text;
			ParentForm.Cursor = Cursors.WaitCursor;
			backgroundWorker1.RunWorkerAsync("LicenseFile");
		}

		private void buttonBrowse_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Slyce License File | *.SlyceLicense";
			dialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);

			if (dialog.ShowDialog(ParentForm) == DialogResult.OK)
			{
				textBoxLicenseFile.Text = dialog.FileName;
				return;
			}
		}

		private void ShowPanel(DevComponents.DotNetBar.PanelEx panel)
		{
			Utility.SuspendPainting(panelMain);
			SelectedPanel = panel;

			for (int i = 0; i < Panels.Count; i++)
			{
				var p = Panels[i];
				p.Style.BackColor1.Color = this.BackColor;
				p.Style.BackColor2.Color = this.BackColor;
				p.Style.ForeColor.Color = Color.Black;
				PictureBoxes[i].Image = imageList1.Images[i * 2 + 1];
			}
			panel.Style.BackColor1.Color = Color.FromArgb(90, 90, 90);
			panel.Style.BackColor2.Color = Color.FromArgb(90, 90, 90);
			panel.Style.ForeColor.Color = Color.White;
			panelMain.Visible = true;
			textBoxSerial.Visible = false;
			textBoxLicenseFile.Visible = false;
			buttonBrowse.Visible = false;
			buttonGetNewTrialLicenseAuto.Visible = false;
			buttonGetNewTrialLicenseManual.Visible = false;
			buttonContinue.Visible = false;
			buttonBuyNow.Visible = false;
			buttonInstallLicenseFile.Visible = false;
			buttonExtendTrial.Visible = false;
			labelDescription.Top = labelHeading.Bottom + 10;
			labelSerial.Visible = false;
			labelEmail.Visible = false;
			textBoxEmail.Visible = false;

			if (panel == panelContinueTrial)
			{
				if (frmLicenseWizard.LicenseStatus == SlyceAuthorizer.LicenseStates.LicenseFileNotFound)
				{
					labelHeading.Text = "Start trial";
					labelDescription.Text = "Get a trial license. Click 'Start trial' to automatically install a license (requires internet connection). Click 'Visit website' to manually download a license from the website.";
					buttonGetNewTrialLicenseAuto.Text = "Start trial";
					buttonGetNewTrialLicenseAuto.Visible = true;
					buttonGetNewTrialLicenseManual.Visible = true;
					labelEmail.Visible = true;
					textBoxEmail.Visible = true;
				}
				else
				{
					labelHeading.Text = "Continue trial";
					labelDescription.Text = "Click to 'Continue trial' to continue using the trial version.";
					buttonContinue.Visible = true;
				}
				pictureBoxStartTrial.Image = imageList1.Images[(int)Images.StartTrial];
			}
			else if (panel == panelSerial)
			{
				labelHeading.Text = "Enter serial number";
				labelDescription.Text = "If you have a serial number (purchased), enter it then click 'Get license' to download and install your full license.";
				textBoxSerial.Visible = true;
				labelSerial.Visible = true;
				buttonGetNewTrialLicenseAuto.Text = "Get license";
				buttonGetNewTrialLicenseAuto.Visible = true;
				buttonGetNewTrialLicenseManual.Visible = true;
				pictureBoxSerial.Image = imageList1.Images[(int)Images.SerialNumber];
			}
			else if (panel == panelLicenseFile)
			{
				labelHeading.Text = "Install license file";
				labelDescription.Text = "If you already have a license file, locate it then click 'Install'.";
				textBoxLicenseFile.Visible = true;
				buttonBrowse.Visible = true;
				buttonInstallLicenseFile.Visible = true;
				pictureBoxLicense.Image = imageList1.Images[(int)Images.License];
			}
			else if (panel == panelBuy)
			{
				labelHeading.Text = "Buy now";
				labelDescription.Text = "Click 'Buy now' to purchase online.";
				buttonBuyNow.Visible = true;
				pictureBoxPurchase.Image = imageList1.Images[(int)Images.Purchase];
			}
			else if (panel == panelExtendTrial)
			{
				labelHeading.Text = "Request trial extesion";
				labelDescription.Text = "Click to send an email requesting a trial extension.";
				pictureBoxExtend.Image = imageList1.Images[(int)Images.Extend];
				buttonExtendTrial.Visible = true;
			}
			Utility.ResumePainting(panelMain);
		}

		private void btnContinueTrial_MouseEnter(object sender, System.EventArgs e)
		{
			ShowPanel(panelContinueTrial);
		}

		private void buttonEnterSerial_MouseEnter(object sender, System.EventArgs e)
		{
			ShowPanel(panelSerial);
		}

		private void buttonLicenseFile_MouseEnter(object sender, System.EventArgs e)
		{
			ShowPanel(panelLicenseFile);
		}

		private void buttonBuy_MouseEnter(object sender, System.EventArgs e)
		{
			ShowPanel(panelBuy);
		}

		private void buttonExtendTrial_MouseEnter(object sender, System.EventArgs e)
		{
			ShowPanel(panelExtendTrial);
		}

		private void buttonEnterSerial_Click(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxSerial.Text))
			{
				MessageBox.Show("Please enter a serial number", "Invalid serial", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			ScreenActivate2.LicenseNumber = textBoxSerial.Text;
			frmLicenseWizard.GoNext();
		}

		private void buttonLicenseFile_Click(object sender, System.EventArgs e)
		{
			if (!File.Exists(textBoxLicenseFile.Text))
			{
				MessageBox.Show("License file not found.", "Invalid License File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			string message;

			if (SlyceAuthorizer.ActivateManually(textBoxLicenseFile.Text, out message))
			{
				SlyceAuthorizer.Reset();
				int daysRemaining;
				bool errorOccurred;
				bool demo;
				SlyceAuthorizer.LockTypes lockType;
				SlyceAuthorizer.LicenseStates status;

				bool licensed = Licensing.SlyceAuthorizer.IsLicensed("Visual NHibernate License.SlyceLicense", out message, out daysRemaining, out errorOccurred, out demo, out lockType, out status);
				bool mustClose = false;

				if (licensed)
				{
					ShowMessageBox("License installed.", "License Installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
					mustClose = true;
				}
				else if (demo && daysRemaining >= 0)
				{
					ShowMessageBox(string.Format("{0} days remaining.", daysRemaining), "Trial Installed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					mustClose = true;
				}
				else
				{
					ShowMessageBox("Your trial has expired.", "Trial Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					mustClose = false;
				}
				//MessageBox.Show("ArchAngel has been successfully activated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
				ParentForm.Cursor = Cursors.Default;
				HasNext = false;
				HasFinish = true;

				if (mustClose)
				{
					frmLicenseWizard.Result = ScreenStart.Actions.Activate;
					frmLicenseWizard.CloseWizard();
				}
				return;
			}
			MessageBox.Show("Can't unlock.\n" + message, "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		private void buttonBuy_Click(object sender, System.EventArgs e)
		{
			ParentForm.Cursor = Cursors.WaitCursor;

			if (MessageBox.Show("Visit www.slyce.com to buy. Do you want to go there now?", "Purchase", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				System.Diagnostics.Process.Start("http://www.slyce.com/Store/");

			ParentForm.Cursor = Cursors.Default;
		}

		private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			if (e.Argument == "LicenseFile")
				e.Result = InstallFullLicenseAuto();
			else if (e.Argument == "GetNewTrialLicenseAuto")
				e.Result = GetNewTrialLicenseAuto();
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			Cursor = Cursors.Default;
			buttonGetNewTrialLicenseAuto.Enabled = true;

			if (e.Error != null)
			{
				// Ensure that we get this reported to us.
				throw e.Error;
			}
			if (LicenseMonitor.CurrentLicense.LicenseStatus != LicenseMonitor.LicenseStates.EvaluationExpired)
			{
				switch ((Licensing.SlyceAuthorizer.LicenseStates)e.Result)
				{
					case SlyceAuthorizer.LicenseStates.Licensed:
					case SlyceAuthorizer.LicenseStates.EvaluationMode:
						frmLicenseWizard.Result = ScreenStart.Actions.Activate;
						frmLicenseWizard.CloseWizard();
						break;
				}
			}
		}

		private SlyceAuthorizer.LicenseStates InstallFullLicenseAuto()
		{
			if (ScreenStart.Action == ScreenStart.Actions.Activate)
			{
				if (!SlyceAuthorizer.IsValidLicenseNumber(textBoxSerial.Text))
				{
					ShowMessageBox("Please enter a valid serial number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return SlyceAuthorizer.LicenseStates.NotChecked;
				}
				string message;

				if (SlyceAuthorizer.ActivateViaInternet(textBoxSerial.Text, "", "", true, out message))
				{
					ShowMessageBox("Your license was installed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return SlyceAuthorizer.LicenseStates.Licensed;
				}
				ShowMessageBox("Can't unlock.\n" + message, "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return SlyceAuthorizer.LicenseStates.ServerValidationFailed;
			}
			return SlyceAuthorizer.LicenseStates.ServerValidationFailed;
		}

		private SlyceAuthorizer.LicenseStates GetNewTrialLicenseAuto()
		{
			if (SelectedPanel == panelContinueTrial) // Trial
			{
				string errorMessage;
				List<string> machineIds = SlyceAuthorizer.GetMachineIds(out errorMessage);

				if (machineIds.Count == 0)
				{
					ShowMessageBox(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return SlyceAuthorizer.LicenseStates.HardwareNotMatched;
				}
				ShowMessage("Fetching trial license...");
				string message;

				if (SlyceAuthorizer.ActivateViaInternet("", machineIds[0], textBoxEmail.Text, false, out message))
				{
					int daysRemaining;
					bool errorOccurred;
					bool demo;
					SlyceAuthorizer.LockTypes lockType;
					SlyceAuthorizer.LicenseStates status;
					Licensing.SlyceAuthorizer.Reset();
					bool licensed = Licensing.SlyceAuthorizer.IsLicensed("Visual NHibernate License.SlyceLicense", out message, out daysRemaining, out errorOccurred, out demo, out lockType, out status);

					if (licensed)
					{
						ShowMessage("Trial started");
						ShowMessageBox("License installed.", "License Installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else if (demo && daysRemaining >= 0)
					{
						ShowMessage("Trial running");
						ShowMessageBox(string.Format("{0} days remaining.", daysRemaining), "Trial Installed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					else
					{
						SetRenewButtonsVisibility(true);
						ShowMessage("Trial expired");
						ShowMessageBox("Your trial has expired. Re-downloading won't help.\n\nGet a new serial when you purchase.", "Trial Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					return status;
				}
				ShowMessage("Activation failed");
				ShowMessageBox("Can't unlock.\n" + message, "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return SlyceAuthorizer.LicenseStates.ServerValidationFailed;
			}
			else // Full
			{
				if (!SlyceAuthorizer.IsValidLicenseNumber(textBoxSerial.Text))
				{
					ShowMessageBox("Please enter a valid serial number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return SlyceAuthorizer.LicenseStates.LicenseFileNotFound;
				}
				ShowMessage("Fetching license...");
				string message;

				if (SlyceAuthorizer.ActivateViaInternet(textBoxSerial.Text, "", "", true, out message))
				{
					LicenseMonitor.Reset();

					if (LicenseMonitor.CurrentLicense.LicenseStatus == LicenseMonitor.LicenseStates.EvaluationExpired)
					{
						SetRenewButtonsVisibility(true);
						ShowMessage("License expired");
						ShowMessageBox("Your license has expired. Re-downloading won't help.\n\nGet a new serial when you purchase.", "Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return SlyceAuthorizer.LicenseStates.EvaluationExpired;
					}
					else
					{
						ShowMessageBox("Your license was installed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return SlyceAuthorizer.LicenseStates.Licensed;
					}
				}
				ShowMessageBox("Can't unlock.\n" + message, "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return SlyceAuthorizer.LicenseStates.ServerValidationFailed;
			}
		}

		private void SetRenewButtonsVisibility(bool visible)
		{
			if (InvokeRequired)
			{
				MethodInvoker invoker = () => SetRenewButtonsVisibility(visible);
				this.Invoke(invoker);
				return;
			}
			buttonRefreshLicense.Visible = visible;
			buttonRemoveLicense.Visible = visible;
		}

		private void ShowMessage(string message)
		{
			if (InvokeRequired)
			{
				MethodInvoker invoker = () => ShowMessage(message);
				this.Invoke(invoker);
				return;
			}
			lblMessage.Text = message;
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

		private void buttonGetNewTrialLicenseAuto_Click(object sender, System.EventArgs e)
		{
			if (SelectedPanel == panelSerial && string.IsNullOrEmpty(textBoxSerial.Text)) // Full
			{
				MessageBox.Show("Please enter a valid serial number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (SelectedPanel == panelContinueTrial && string.IsNullOrEmpty(textBoxEmail.Text)) // Trial
			{
				MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			ParentForm.Cursor = Cursors.WaitCursor;
			buttonGetNewTrialLicenseAuto.Enabled = false;
			backgroundWorker1.RunWorkerAsync("GetNewTrialLicenseAuto");
		}

		private void buttonGetNewTrialLicenseManual_Click(object sender, System.EventArgs e)
		{
			string url = GetWebsiteUrl();

			if (!string.IsNullOrEmpty(url))
				System.Diagnostics.Process.Start(url);
		}

		private void buttonContinue_Click(object sender, System.EventArgs e)
		{
			Action = Actions.Trial;
			frmLicenseWizard.GoNext();
		}

		private string GetWebsiteUrl()
		{
			if (SelectedPanel == panelContinueTrial) // Trial
			{
				string errorMessage;
				List<string> machineIds = SlyceAuthorizer.GetMachineIds(out errorMessage);

				if (machineIds.Count == 0)
				{
					MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return "";
				}
				return string.Format(@"http://www.slyce.com/Support/?action=getTrial&trialNumber={0}&email={1}", machineIds[0], textBoxEmail.Text);
			}
			else // Full
			{
				if (!SlyceAuthorizer.IsValidLicenseNumber(textBoxSerial.Text))
				{
					MessageBox.Show("Please enter a valid serial number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return "";
				}
				return string.Format(@"http://www.slyce.com/Support/?action=getLicense&serial={0}", textBoxSerial.Text);
			}
		}

		private void buttonCopyUrl_Click(object sender, System.EventArgs e)
		{
			string url = GetWebsiteUrl();

			if (!string.IsNullOrEmpty(url))
				Clipboard.SetText(url);
		}

		private void buttonInstallLicenseFile_Click(object sender, System.EventArgs e)
		{
			if (!File.Exists(textBoxLicenseFile.Text))
			{
				MessageBox.Show("License file not found.", "Invalid License File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			string message;

			if (SlyceAuthorizer.ActivateManually(textBoxLicenseFile.Text, out message))
			{
				SlyceAuthorizer.Reset();
				int daysRemaining;
				bool errorOccurred;
				bool demo;
				SlyceAuthorizer.LockTypes lockType;
				SlyceAuthorizer.LicenseStates status;

				bool licensed = Licensing.SlyceAuthorizer.IsLicensed("Visual NHibernate License.SlyceLicense", out message, out daysRemaining, out errorOccurred, out demo, out lockType, out status);
				bool mustClose = false;

				if (licensed)
				{
					ShowMessageBox("License installed.", "License Installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
					mustClose = true;
				}
				else if (demo && daysRemaining >= 0)
				{
					ShowMessageBox(string.Format("{0} days remaining.", daysRemaining), "Trial Installed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					mustClose = true;
				}
				else
				{
					ShowMessageBox("Your trial has expired.", "Trial Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					mustClose = false;
				}
				//MessageBox.Show("ArchAngel has been successfully activated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
				ParentForm.Cursor = Cursors.Default;
				HasNext = false;
				HasFinish = true;

				if (mustClose)
				{
					frmLicenseWizard.Result = ScreenStart.Actions.Activate;
					frmLicenseWizard.CloseWizard();
				}
				return;
			}
			MessageBox.Show("Can't unlock.\n" + message, "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		private void buttonBuyNow_Click(object sender, System.EventArgs e)
		{
			ParentForm.Cursor = Cursors.WaitCursor;

			if (MessageBox.Show("Visit www.slyce.com to buy. Do you want to go there now?", "Purchase", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				System.Diagnostics.Process.Start("http://www.slyce.com/Store/");

			ParentForm.Cursor = Cursors.Default;
		}

		private void buttonExtendTrial_Click(object sender, System.EventArgs e)
		{
			string errMessage = "";
			List<string> machineIds = Licensing.LicenseMonitor.GetMachineIds(out errMessage);

			if (MessageBox.Show("Please tell us why you need to extend your trial period.\n\nAuto-create email request?", "Reason", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
			{
				try
				{
					ParentForm.Cursor = Cursors.WaitCursor;
					string message = string.Format("mailto:{0}?subject={1}&body={2}", "support@slyce.com", "Trial extension: " + machineIds[0], "");
					System.Diagnostics.Process.Start(message);
				}
				catch
				{
					string message = string.Format("Email to: support@slyce.com\nSubject: Trial extension: {0}", machineIds[0]);
					MessageBox.Show(message + "\n\nThis data has been copied to your clipboard.", "Email", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
				}
				finally
				{
					ParentForm.Cursor = Cursors.Default;
				}
			}
			else
			{
				string message = string.Format("Email to: support@slyce.com\nSubject: Trial extension: {0}", machineIds[0]);
				MessageBox.Show(message + "\n\nThis data has been copied to your clipboard.", "Email", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
			}
		}

		private void buttonRefreshLicense_Click(object sender, System.EventArgs e)
		{
			// Determine existing license, trial or full
			if (LicenseMonitor.CurrentLicense.Lic.Type == License.LicenseTypes.Trial)
			{
				File.Delete(LicenseMonitor.LicenseFilePath);
				ParentForm.Cursor = Cursors.WaitCursor;
				buttonGetNewTrialLicenseAuto.Enabled = false;
				backgroundWorker1.RunWorkerAsync("GetNewTrialLicenseAuto");
			}
			else if (LicenseMonitor.CurrentLicense.Lic.Type == License.LicenseTypes.Full)
			{
				textBoxSerial.Text = LicenseMonitor.CurrentLicense.Lic.Serial;
				SelectedPanel = panelSerial;
				ShowPanel(panelSerial);
				Refresh();

				File.Delete(LicenseMonitor.LicenseFilePath);

				ParentForm.Cursor = Cursors.WaitCursor;
				buttonGetNewTrialLicenseAuto.Enabled = false;
				backgroundWorker1.RunWorkerAsync("GetNewTrialLicenseAuto");
			}
		}

		private void buttonRemoveLicense_Click(object sender, System.EventArgs e)
		{
			if (File.Exists(LicenseMonitor.LicenseFilePath))
			{
				File.Delete(LicenseMonitor.LicenseFilePath);
				LicenseMonitor.Reset();
				Populate();
			}
		}

		private void panelLicenseFile_Click(object sender, System.EventArgs e)
		{
			ShowPanel(panelLicenseFile);
		}

		private void panelContinueTrial_Click(object sender, System.EventArgs e)
		{
			ShowPanel(panelContinueTrial);
		}

		private void panelSerial_Click(object sender, System.EventArgs e)
		{
			ShowPanel(panelSerial);
		}

		private void panelBuy_Click(object sender, System.EventArgs e)
		{
			ShowPanel(panelBuy);
		}

		private void panelExtendTrial_Click(object sender, System.EventArgs e)
		{
			ShowPanel(panelExtendTrial);
		}
	}
}
