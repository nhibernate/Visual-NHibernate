using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ArchAngel.Licensing.LicenseWizard
{
	public partial class ScreenUnlock : Interfaces.Controls.ContentItems.ContentItem
	{
		public enum ActivationTypes
		{
			Instant,
			Website,
			Email,
			ExtendTrial//,
			//UnlockCode
		}
		private static ActivationTypes ActivationType = ActivationTypes.ExtendTrial;

		public ScreenUnlock()
		{
			InitializeComponent();
			HasFinish = false;
			NextText = "Next >";
			BackColor = Color.White;
		}

		public override void OnDisplaying()
		{
			labelDescription.Visible = false;
			panelEmailActivation.Visible = false;
			panelWebsiteActivation.Visible = false;
			panelEmailActivation.Dock = DockStyle.None;
			panelWebsiteActivation.Dock = DockStyle.None;
			panelExtendTrial.Visible = false;
			panelExtendTrial.Dock = DockStyle.None;

			switch (ScreenActivate.ActivationType)
			{
				case ScreenActivate.ActivationTypes.Instant:
					labelDescription.Text = "Success! ArchAngel was activated successfully.";
					labelDescription.Visible = true;
					ActivationType = ActivationTypes.Instant;
					break;
				case ScreenActivate.ActivationTypes.Email:
					txtWebsiteLicenseNumber.Text = ScreenActivate.LicenseNumber;
					txtWebsiteInstallationID.Text = frmLicenseWizard.InstallationId;
					panelEmailActivation.Visible = true;
					panelEmailActivation.Dock = DockStyle.Fill;
					ActivationType = ActivationTypes.Email;
					NextText = "Close";
					txtEmailTo.Text = "support@slyce.com";
					txtEmailSubject.Text = "Trial License for ArchAngel";
					txtEmailBody.Text = string.Format("Please send me an ArchAngel trial license.{1}{1}Hardware ID{1}================================{1}[{0}]{1}================================", frmLicenseWizard.InstallationId, Environment.NewLine);
					break;
				case ScreenActivate.ActivationTypes.Website:
					txtWebsiteLicenseNumber.Text = ScreenActivate.LicenseNumber;
					txtWebsiteInstallationID.Text = frmLicenseWizard.InstallationId;
					panelWebsiteActivation.Visible = true;
					panelWebsiteActivation.Dock = DockStyle.Fill;
					ActivationType = ActivationTypes.Website;
					NextText = "Close";
					txtWebsiteLicenseNumber.Visible = ScreenStart.Action != ScreenStart.Actions.Trial;
					lblWebsiteLicenseNumber.Visible = ScreenStart.Action != ScreenStart.Actions.Trial;
					buttonCopyLicenseNumber.Visible = ScreenStart.Action != ScreenStart.Actions.Trial;
					break;
				case ScreenActivate.ActivationTypes.ExtendTrial:
					txtWebsiteLicenseNumber.Text = ScreenActivate.LicenseNumber;
					txtWebsiteInstallationID.Text = frmLicenseWizard.InstallationId;
					panelExtendTrial.Visible = true;
					panelExtendTrial.Dock = DockStyle.Fill;
					ActivationType = ActivationTypes.ExtendTrial;
					break;
				case ScreenActivate.ActivationTypes.UnlockCode:
					// Do nothing - we are about to restart the application
					break;
				default:
					throw new NotImplementedException("ActivationType not handled yet: " + ScreenActivate.ActivationType.ToString());
			}
		}

		public override bool Next()
		{
			try
			{
				switch (ActivationType)
				{
					case ActivationTypes.Email:
						ParentForm.Cursor = Cursors.Default;
						frmLicenseWizard.Result = ScreenStart.Actions.None;
						frmLicenseWizard.CloseWizard();
						break;
					case ActivationTypes.ExtendTrial:
						string message;
						if (SlyceAuthorizer.ActivateManually("", out message))
						{
							MessageBox.Show("ArchAngel trial has been successfully extended.", "Trial Extended", MessageBoxButtons.OK, MessageBoxIcon.Information);
							ParentForm.Cursor = Cursors.Default;
							frmLicenseWizard.Result = ScreenStart.Actions.Activate;
							frmLicenseWizard.CloseWizard();
						}
						else
						{
							MessageBox.Show(message, "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						break;
					case ActivationTypes.Website:
						if (File.Exists(txtLicenseFile2.Text))
						{
							// User has downloaded a license file
							if (SlyceAuthorizer.ActivateManually(txtLicenseFile2.Text, out message))
							{
								MessageBox.Show("License successfully installed!", "Activation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
								ParentForm.Cursor = Cursors.Default;
								frmLicenseWizard.Result = ScreenStart.Actions.Activate;
								frmLicenseWizard.CloseWizard();
								return true;
							}
							MessageBox.Show("Can't unlock.\n" + message, "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						ParentForm.Cursor = Cursors.Default;
						frmLicenseWizard.CloseWizard();
						break;
					default:
						MessageBox.Show("Not handled yet: " + ActivationType.ToString());
						break;
				}
				return false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error");
				return false;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ParentForm.Cursor = Cursors.WaitCursor;
			System.Diagnostics.Process.Start(string.Format("{0}?fromAA_LicenseNumber={1}&fromAA_HardwareID={2}", SlyceAuthorizer.ActivationUrl, txtWebsiteLicenseNumber.Text, txtWebsiteInstallationID.Text));
			ParentForm.Cursor = Cursors.Default;
		}

		private void buttonCopyInstallationID_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(txtWebsiteInstallationID.Text);
		}

		private void buttonCopyLicenseNumber_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(txtWebsiteLicenseNumber.Text);
		}

		private void buttonExtendTrial_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtExtendTrial.Text.Trim()))
			{
				MessageBox.Show("Please give a reason for requesting an extension to your 30-day trial period.", "Reason is Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			ParentForm.Cursor = Cursors.WaitCursor;
			string message = string.Format("mailto:{0}?subject={1}&body={2}", txtEmailTo.Text, txtEmailSubject.Text, txtEmailBody.Text);
			System.Diagnostics.Process.Start(message);
			ParentForm.Cursor = Cursors.Default;
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ParentForm.Cursor = Cursors.WaitCursor;
			string querystring;

			if (ScreenStart.Action == ScreenStart.Actions.Trial)
			{
				querystring = string.Format(@"?hardwareid={0}&licensetype=Trial&product=ArchAngel&version=1&edition=Professional", txtWebsiteInstallationID.Text);
			}
			else
			{
				// Full license
				querystring = string.Format(@"?serial={0}&hardwareid={1}&licensetype=Full&product=ArchAngel&version=1&edition=Professional", txtWebsiteLicenseNumber.Text, txtWebsiteInstallationID.Text);
			}
			System.Diagnostics.Process.Start(SlyceAuthorizer.ActivationUrl + querystring);
			ParentForm.Cursor = Cursors.Default;
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = ".xml";
			dialog.Filter = "Slyce License Files | Visual NHibernate License.SlyceLicense";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				if (File.Exists(dialog.FileName))
				{
					txtLicenseFile2.Text = dialog.FileName;
				}
			}
		}

		private void btnCreateEmail_Click(object sender, EventArgs e)
		{
			ParentForm.Cursor = Cursors.WaitCursor;
			string message = string.Format("mailto:{0}?subject={1}&body={2}", txtEmailTo.Text, txtEmailSubject.Text, txtEmailBody.Text.Replace(Environment.NewLine, System.Web.HttpUtility.UrlEncode(Environment.NewLine)));
			System.Diagnostics.Process.Start(message);
			ParentForm.Cursor = Cursors.Default;
		}
	}
}
