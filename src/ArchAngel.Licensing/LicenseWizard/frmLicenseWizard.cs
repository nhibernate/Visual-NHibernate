using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ArchAngel.Licensing.LicenseWizard
{
	public partial class frmLicenseWizard : Form
	{
		private readonly ScreenStart _ScreenStart;
		public static ScreenStart.Actions Result = ScreenStart.Actions.None;
		internal static int DaysRemaining = 0;
		internal static string InstallationId;
		private static frmLicenseWizard Instance;
		internal static SlyceAuthorizer.LockTypes LockType;
		internal static SlyceAuthorizer.LicenseStates LicenseStatus;

		public frmLicenseWizard(int daysRemaining, SlyceAuthorizer.LockTypes lockType, SlyceAuthorizer.LicenseStates licenseStatus)
		{
			InitializeComponent();
			Instance = this;
			DaysRemaining = daysRemaining;
			LockType = lockType;
			LicenseStatus = licenseStatus;
			ucHeading1.Text = "";
			_ScreenStart = new ScreenStart();
			_ScreenStart.BackColor = Color.FromArgb(220, 220, 220);
			LoadScreen(_ScreenStart);

			string message;
			List<string> machineIds = SlyceAuthorizer.GetMachineIds(out message);

			if (machineIds.Count > 0)
				InstallationId = machineIds[0];
			else
				MessageBox.Show(this, message, "Error Determining InstallationID", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		internal static void CloseWizard()
		{
			Instance.Close();
		}

		internal static void UpdateButtonText()
		{
			Interfaces.Controls.ContentItems.ContentItem currentItem = (Interfaces.Controls.ContentItems.ContentItem)Instance.panelContent.Controls[0];
		}

		private void LoadScreen(Interfaces.Controls.ContentItems.ContentItem screen)
		{
			Cursor = Cursors.WaitCursor;
			panelContent.Controls.Clear();
			panelContent.Controls.Add(screen);
			screen.Dock = DockStyle.Fill;
			screen.OnDisplaying();
			Cursor = Cursors.Default;
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		internal static void GoNext()
		{
			Instance.LoadNextScreen();
		}

		internal void LoadNextScreen()
		{
			Interfaces.Controls.ContentItems.ContentItem currentItem = (Interfaces.Controls.ContentItems.ContentItem)panelContent.Controls[0];

			if (currentItem.HasFinish)
			{
				DialogResult = DialogResult.OK;
				Close();
				return;
			}
			if (!currentItem.HasNext || !currentItem.Next())
			{
				return;
			}
			if (currentItem.HasFinish)
			{
				DialogResult = DialogResult.OK;
				Close();
				return;
			}
			if (typeof(ScreenStart).IsInstanceOfType(currentItem))
			{
				ScreenStart.Actions action = ScreenStart.Action;

				switch (action)
				{
					case ScreenStart.Actions.Purchase:
						break;
					case ScreenStart.Actions.Trial:
						Result = ScreenStart.Actions.Trial;

						if (ScreenStart.Action == ScreenStart.Actions.Trial && DaysRemaining > 0)
						{
							DialogResult = DialogResult.OK;
							Close();
							return;
						}
						break;
					default:
						throw new NotImplementedException(action.ToString());
				}
			}
			else
				throw new NotImplementedException(currentItem.GetType().Name);
		}

		private void frmLicenseWizard_FormClosing(object sender, FormClosingEventArgs e)
		{
			LicenseMonitor.Reset();

			switch (LicenseMonitor.CurrentLicense.LicenseStatus)
			{
				case LicenseMonitor.LicenseStates.EvaluationMode:
					Result = Licensing.LicenseWizard.ScreenStart.Actions.Trial;
					break;
				case LicenseMonitor.LicenseStates.Licensed:
					Result = Licensing.LicenseWizard.ScreenStart.Actions.Activate;
					break;
				default:
					Result = Licensing.LicenseWizard.ScreenStart.Actions.None;
					//Application.Exit();
					break;
			}
		}
	}
}
