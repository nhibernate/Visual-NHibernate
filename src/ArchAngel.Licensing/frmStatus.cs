using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ArchAngel.Licensing
{
	public partial class frmStatus : Form
	{
		private string LicenseNumber = "";
		private string LicenseFilename;

		public frmStatus(string licenseFilename)
		{
			InitializeComponent();
			LicenseFilename = licenseFilename;
			ucHeading1.Text = "";
			BackColor = Slyce.Common.Colors.BackgroundColor;
			string message;
			List<string> machineIds = SlyceAuthorizer.GetMachineIds(out message);

			if (machineIds.Count > 0)
			{
				txtInstallationID.Text = machineIds[0];
			}
			else
			{
				MessageBox.Show(message, "Error Determining InstallationID", MessageBoxButtons.OK, MessageBoxIcon.Error);
				labelErrorMessage.Text = message;
				labelErrorMessage.Visible = true;
			}
			PopulateLicenseStatus();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonRemove_Click(object sender, EventArgs e)
		{
			//if (MessageBox.Show("Are you sure you want to remove the license? You will not be able to run ArchAngel once the license has been removed.", "Remove License?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			//{
			//    string proofOfRemovalCode;
			//    string message;

			//    if (SlyceAuthorizer.RemoveLicense(out proofOfRemovalCode, out message))
			//    {
			//        string installationId;
			//        string xxx;
			//        SlyceAuthorizer.GetMachineId(out installationId, out xxx);
			//        txtInstallationID.Text = installationId;
			//        Clipboard.SetText(proofOfRemovalCode);
			//        MessageBox.Show("Your license has been successfully removed. Your 'Proof of Removal Code' is [" + proofOfRemovalCode + "]. It has been copied to your ClipBoard, and an email to Slyce will now be created...", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

			//        Cursor = Cursors.WaitCursor;
			//        const string toEmail = "sales@slyce.com";
			//        string subject = string.Format("[{0} : {1}] Proof of Removal Code", txtInstallationID.Text, proofOfRemovalCode);
			//        string body = string.Format("I have removed my ArchAngel license. {2}{2}Proof of Removal Code: {3}{2}Hardware ID: {1}", LicenseNumber, txtInstallationID.Text, System.Web.HttpUtility.UrlEncode(Environment.NewLine), proofOfRemovalCode);
			//        message = string.Format("mailto:{0}?subject={1}&body={2}", toEmail, subject, body);

			//        try
			//        {
			//            // Write details to file so that user can get to them later if they forget to write them down
			//            Slyce.Common.Utility.WriteToFile(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "_Proof Of Removal.txt"), string.Format("SUBJECT: {1}{0}{0}MESSAGE:{0}{2}", Environment.NewLine, subject, body));
			//        }
			//        catch (Exception)
			//        {
			//            // Fail silently
			//        }
			//        System.Diagnostics.Process.Start(message);
			//        Cursor = Cursors.Default;
			//    }
			//    else
			//    {
			//        MessageBox.Show(message, "Error Removing License", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//        labelErrorMessage.Text = message;
			//        labelErrorMessage.Visible = true;
			//    }
			//}
			//PopulateLicenseStatus();
		}

		private void PopulateLicenseStatus()
		{
			string message;
			int daysRemaining;
			bool errorOccurred;
			bool demo = false;
			SlyceAuthorizer.LockTypes lockType;
			SlyceAuthorizer.LicenseStates status;
			bool licensed = SlyceAuthorizer.IsLicensed(LicenseFilename, out message, out daysRemaining, out errorOccurred, out demo, out lockType, out status);

			if (errorOccurred)
			{
				labelErrorMessage.Text = "An error occurred with the Slyce licensing system. Please inform support@slyce.com about this error:\n\nError: " + message;
			}
			else if (licensed && !demo)
			{
				if (message.Length > 0)
				{
					labelErrorMessage.Text = message;
				}
				labelStatus.Text = "Licensed";
				buttonRemove.Enabled = true;
			}
			else if (licensed && demo)
			{
				labelStatus.Text = string.Format("{0} days remaining of your extended trial.", daysRemaining);
				buttonRemove.Enabled = false;
			}
			else
			{
				switch (lockType)
				{
					case SlyceAuthorizer.LockTypes.Days:
						labelStatus.Text = string.Format("{0} days remaining of your 30-day trial.", daysRemaining);
						break;
					default:
						labelStatus.Text = string.Format("{0} days remaining of your trial.", daysRemaining);
						break;
				}
				labelStatus.Text = string.Format("{0} days remaining of your trial.", daysRemaining);
				buttonRemove.Enabled = false;
			}
			if (licensed || demo)
			{
				Dictionary<string, string> licenseDetails = SlyceAuthorizer.AdditionalLicenseInfo;

				foreach (string key in licenseDetails.Keys)
				{
					listDetails.Items.Add(new ListViewItem(new string[] { key, licenseDetails[key] }));

					if (key.ToLower().Replace(" ", "") == "licensenumber")
					{
						LicenseNumber = licenseDetails[key];
					}
				}
				if (licensed)
				{
					txtSerial.Text = SlyceAuthorizer.Serial;
				}
				else
				{
					txtSerial.Enabled = false;
				}

			}

		}
	}
}
