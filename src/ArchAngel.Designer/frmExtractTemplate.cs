using System;
using System.IO;
using System.Windows.Forms;

namespace ArchAngel.Designer
{
	public partial class frmExtractTemplate : Form
	{
		internal string FileName = "";

		public frmExtractTemplate()
		{
			InitializeComponent();
			this.BackColor = Slyce.Common.Colors.BackgroundColor;
			Controller.ShadeMainForm();
			ucHeading1.Text = "";
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void buttonExtract_Click(object sender, EventArgs e)
		{
			if (!File.Exists(txtCompiledFile.Text))
			{
				MessageBox.Show(this, "Please select a compiled ArchAngel template file.", "Missing File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.DefaultExt = ".stz";
			dialog.Filter = "ArchAngel templates (*.stz)|*.stz";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				if (!Directory.Exists(Path.GetDirectoryName(dialog.FileName)))
				{
					MessageBox.Show(this, "Please specify a valid save location.", "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (File.Exists(dialog.FileName))
				{
					Slyce.Common.Utility.DeleteFileBrute(dialog.FileName);
				}
				Project.ExtractTemplateFromCompiledTemplate(txtCompiledFile.Text, dialog.FileName);
				this.FileName = dialog.FileName;
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void frmExtractTemplate_FormClosed(object sender, FormClosedEventArgs e)
		{
			Controller.UnshadeMainForm();
		}

		private void buttonBrowseCompiledFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Compiled ArchAngel templates (*.AAT.DLL)|*.AAT.DLL";
			dialog.FileName = "";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				txtCompiledFile.Text = dialog.FileName;
			}
		}
	}
}
