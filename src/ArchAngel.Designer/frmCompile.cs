using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using System.IO;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace SlyceScripter
{
	public partial class frmCompile : Form
	{
		private string CurrentDir = "";
		public bool Success = false;

		public frmCompile()
		{
			InitializeComponent();
		}

		private void btnCreate_Click(object sender, EventArgs e)
		{
			if (Path.GetExtension(txtOutputFile.Text.ToLower()) != ".aal" &&
				Path.GetExtension(txtOutputFile.Text.ToLower()) != ".dll")
			{
				MessageBox.Show("Output file is not an AAL file. Must have '.AAL' extension.", "Invalid file type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Project.Instance.CompileFileName = txtOutputFile.Text;
			Cursor = Cursors.WaitCursor;
			btnCreate.Text = "Busy...";
			btnCreate.Refresh();
			//CompileHelper.Compile();
			btnCreate.Text = "Compile";
			Cursor = Cursors.Default;
			this.Close();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnSelectScriptFile_Click(object sender, EventArgs e)
		{
			if (CurrentDir.Length == 0)
			{
				CurrentDir = Application.ExecutablePath;
			}
			openFileDialog1.Multiselect = true;
			openFileDialog1.FileName = CurrentDir;
			openFileDialog1.ShowDialog();

			foreach (string filename in openFileDialog1.FileNames)
			{
				CompileHelper.ScriptFiles.Add(filename);
			}

			if (openFileDialog1.FileName.Length > 0)
			{
				txtOutputFile.Text = Path.GetFileNameWithoutExtension(openFileDialog1.FileName) + ".aal";
			}
		}

		private void btnOutputFile_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.CheckFileExists = false;
			saveFileDialog.Filter = "ArchAngel Library (*.aal)|*.aal";

			if (saveFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			txtOutputFile.Text = saveFileDialog.FileName;
		}

		private void lstFiles_MouseMove(object sender, MouseEventArgs e)
		{
			Type tt = sender.GetType();

			if (((ListView)sender).GetItemAt(e.X, e.Y) != null)
			{
				//Active ToolTip when want to show it and
				toolTip1.Active = true;
				toolTip1.SetToolTip((Control)sender, (((ListView)sender).GetItemAt(e.X, e.Y)).SubItems[1].Text);
			}
			else
			{
				//InActive
				toolTip1.Active = false;
			}
		}

		private void frmCompile_Load(object sender, EventArgs e)
		{
			CompileHelper.ScriptFiles.Clear();

			if (CompileHelper.DebugVersion) { this.Visible = false; }
			string file = Project.Instance.ProjectFileName;

			if (file.Length == 0)
			{
				MessageBox.Show("Add project details including a project name via the Config Builder in the Tools menu before doing this.", "Missing details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				this.Close();
				return;
			}
			bool firstRun = false;

			if (CompileHelper.SourceFile.Length > 0)
			{
				CompileHelper.ScriptFiles.Add(Path.GetFullPath(CompileHelper.SourceFile));
			}
			if (Project.Instance.CompileFileName.Length == 0)
			{
				firstRun = true;
				Project.Instance.CompileFileName = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file)) + ".aal";
			}
            if (Project.Instance.ProjType == Project.ProjectType.Template &&
				Project.Instance.CompileFileName.Length > 0)
			{
				try
				{
					if (CompileHelper.DebugVersion)
					{
						txtOutputFile.Text = Path.Combine(Path.GetTempPath(), "DebugVersion.tmp");
						//CompileHelper.Compile();
						this.Close();
						return;
					}
					txtOutputFile.Text = Project.Instance.CompileFileName;

					if (!firstRun)
					{
						//CompileHelper.Compile();
					}
				}
				catch (Exception ex)
				{
					// Do nothing
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void frmCompile_Paint(object sender, PaintEventArgs e)
		{
			LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.FromArgb(148, 171, 229), Color.FromArgb(252, 253, 254), LinearGradientMode.Vertical);
			e.Graphics.FillRectangle(brush, this.ClientRectangle); 
		}

		private void frmCompile_FormClosing(object sender, FormClosingEventArgs e)
		{
			CompileHelper.DeleteScriptFiles();
		}

		private void txtOutputFile_TextChanged(object sender, EventArgs e)
		{

		}


	}
}