using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ArchAngel.Designer.Properties;

namespace ArchAngel.Designer
{
	public partial class frmOptions : Form
	{
		public enum Pages
		{
			Programs,
			Files,
			DebugSettings,
			General
		}

		public frmOptions()
		{
			InitializeComponent();
			EnableDoubleBuffering();
			Controller.ShadeMainForm();
			Populate();
			ucHeading3.Text = "";
		}

		public frmOptions(Pages selectedPage)
		{
			InitializeComponent();
			EnableDoubleBuffering();
			Controller.ShadeMainForm();
			Populate();
			ucHeading3.Text = "";

			switch (selectedPage)
			{
				case Pages.Programs:
					tabStrip1.SelectedPage = tabStripPagePrograms;
					break;
				case Pages.DebugSettings:
					tabStrip1.SelectedPage = tabStripPageDebugSettings;
					break;
				case Pages.General:
					tabStrip1.SelectedPage = tabStripPageGeneral;
					break;
				case Pages.Files:
					tabStrip1.SelectedPage = tabStripPageFiles;
					break;
				default:
					throw new NotImplementedException("Tab page not handled yet: " + selectedPage.ToString());
			}
		}

		private void EnableDoubleBuffering()
		{
			// Set the value of the double-buffering style bits to true.
			SetStyle(ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint,
				true);
			UpdateStyles();
		}

		private void Populate()
		{
			chkStartNewTemplate.Checked = !Settings.Default.AutoLoadLastOpenFile;

			//if (!File.Exists(Controller.Instance.ILMergePath))
			//{
			//    string typicalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Microsoft"+ Path.DirectorySeparatorChar +"ILMerge" + Path.DirectorySeparatorChar + "ILMerge.exe");

			//    if (File.Exists(typicalPath))
			//    {
			//        Controller.Instance.ILMergePath = typicalPath;
			//    }
			//    else
			//    {
			//        typicalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "ILMerge" + Path.DirectorySeparatorChar + "ILMerge.exe");

			//        if (File.Exists(typicalPath))
			//        {
			//            Controller.Instance.ILMergePath = typicalPath;
			//        }
			//    }
			//    txtILMergeLocation.Text = Controller.Instance.ILMergePath;
			//}
			//txtILMergeLocation.Text = Controller.Instance.ILMergePath;
			txtDebugCSPDatabasePath.Text = Project.Instance.DebugProjectFile;
			txtDebugGenerationOutputPath.Text = Project.Instance.TestGenerateDirectory;
			// Colors
			chkUseThemeColor.Checked = Settings.Default.UseThemeColour;
			btnColorBrowse.BackColor = Settings.Default.BaseColourToUse;
			btnColorBrowse.Enabled = !Settings.Default.UseThemeColour;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				Cursor = Cursors.WaitCursor;
				Controller.Instance.MainForm.Refresh();
				Settings.Default.AutoLoadLastOpenFile = !chkStartNewTemplate.Checked;
				//Controller.Instance.ILMergePath = txtILMergeLocation.Text;
				Project.Instance.DebugProjectFile = txtDebugCSPDatabasePath.Text;
				Project.Instance.TestGenerateDirectory = txtDebugGenerationOutputPath.Text;
				Close();
			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(@"http://www.microsoft.com/downloads/details.aspx?FamilyID=22914587-b4ad-4eae-87cf-b14ae6a939b0&displaylang=en");
			// Watch 'Program Files' to see when the installation takes place, so we can pick up the file and update the textbox
			fileSystemWatcher1.Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
			fileSystemWatcher1.IncludeSubdirectories = true;
			fileSystemWatcher1.Created += fileSystemWatcher1_Created;
		}

		private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
		{
			if (e.FullPath.ToLower().IndexOf("ilmerge.exe") >= 0)
			{
				txtILMergeLocation.Text = e.FullPath;
				// Stop monitoring
				fileSystemWatcher1.Dispose();
				fileSystemWatcher1 = null;
			}
		}

		private void buttonTemplateFileName_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			if (!string.IsNullOrEmpty(txtDebugCSPDatabasePath.Text))
			{
				openFileDialog.InitialDirectory = Path.GetDirectoryName(txtDebugCSPDatabasePath.Text);
			}
			openFileDialog.Filter = "ArchAngel (*.wbproj, *.aaproj)|*.wbproj;*.aaproj";
			//Slyce.Common.Utility.ShadeForm(this, 180, Color.Black);

			if (openFileDialog.ShowDialog(this) != DialogResult.OK)
			{
				Slyce.Common.Utility.UnShadeForm(this);
				return;
			}
			//Slyce.Common.Utility.UnShadeForm(this);

			//try
			//{
			txtDebugCSPDatabasePath.Text = openFileDialog.FileName;
			//}
			//catch (Exception ex)
			//{
			//    Controller.ReportError(ex);
			//}
		}

		private void frmOptions_Paint(object sender, PaintEventArgs e)
		{
			BackColor = Slyce.Common.Colors.BackgroundColor;
		}

		private void btnILMergeLocation_Click(object sender, EventArgs e)
		{
			//OpenFileDialog openFileDialog = new OpenFileDialog();

			//if (File.Exists(txtILMergeLocation.Text))
			//{
			//    openFileDialog.FileName = txtILMergeLocation.Text;
			//}
			//Slyce.Common.Utility.ShadeForm(this, 180, Color.Black);

			//if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			//{
			//    txtILMergeLocation.Text = openFileDialog.FileName;
			//}
			//Slyce.Common.Utility.UnShadeForm(this);
		}

		private void frmOptions_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (fileSystemWatcher1 != null)
			{
				fileSystemWatcher1.Dispose();
				fileSystemWatcher1 = null;
			}
		}

		private void btnColorBrowse_BackColorChanged(object sender, EventArgs e)
		{
			btnColorBrowse.ForeColor = Slyce.Common.Colors.IdealTextColor(btnColorBrowse.BackColor);
		}

		private void btnColorBrowse_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			colorDialog.Color = Settings.Default.BaseColourToUse;
			Slyce.Common.Utility.ShadeForm(this, 180, Color.Black);

			if (colorDialog.ShowDialog(this) == DialogResult.OK)
			{
				Settings.Default.BaseColourToUse = colorDialog.Color;
				ForceColorRefresh();
			}
			Slyce.Common.Utility.UnShadeForm(this);
		}

		/// <summary>
		/// Causes entire application to repaint
		/// </summary>
		private void ForceColorRefresh()
		{
			btnColorBrowse.BackColor = Settings.Default.BaseColourToUse;
			Refresh();
			Controller.Instance.MainForm.Refresh();
		}

		private void chkUseThemeColor_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.UseThemeColour = chkUseThemeColor.Checked;
			btnColorBrowse.Enabled = !Settings.Default.UseThemeColour;
			ForceColorRefresh();
		}

		private void frmOptions_FormClosed(object sender, FormClosedEventArgs e)
		{
			Controller.UnshadeMainForm();
		}

		private void txtDebugCSPDatabasePath_TextChanged(object sender, EventArgs e)
		{
			Project.Instance.IsDirty = true;
		}

		private void buttonGenerationOutput_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();

			if (!string.IsNullOrEmpty(txtDebugGenerationOutputPath.Text))
			{
				fbd.SelectedPath = Path.GetDirectoryName(txtDebugGenerationOutputPath.Text);
			}

			if (fbd.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}

			//try
			//{
			txtDebugGenerationOutputPath.Text = fbd.SelectedPath;
			//}
			//catch (Exception ex)
			//{
			//    Controller.ReportError(ex);
			//}
		}
	}
}