using System;
using System.IO;
using System.Windows.Forms;

namespace ArchAngel.Designer
{
	public partial class frmNewProvider : Form
	{
		public frmNewProvider()
		{
			InitializeComponent();
			Controller.ShadeMainForm();
			ucHeading1.Text = "";
			this.Text = "New Provider";
			ucHeading1.Text = "";

			string defaultFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ArchAngel Custom Providers");

			if (!Directory.Exists(defaultFolder))
			{
				Directory.CreateDirectory(defaultFolder);
			}
			txtFolder.Text = defaultFolder;
		}

		private void frmNewProvider_Paint(object sender, PaintEventArgs e)
		{
			this.BackColor = Slyce.Common.Colors.BackgroundColor;
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.ShowNewFolderButton = true;
			dialog.SelectedPath = txtFolder.Text;

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				txtFolder.Text = dialog.SelectedPath;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnCreate_Click(object sender, EventArgs e)
		{
			if (!Directory.Exists(txtFolder.Text))
			{
				try
				{
					Directory.CreateDirectory(txtFolder.Text);
				}
				catch
				{
					MessageBox.Show(this, "Specified folder is missing:\n\n" + txtFolder.Text, "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			if (Directory.GetFiles(txtFolder.Text).Length > 0 ||
				Directory.GetDirectories(txtFolder.Text).Length > 0)
			{
				MessageBox.Show(this, "The folder is not empty. Please specify an empty folder.", "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			Controller.Instance.MainForm.Cursor = Cursors.WaitCursor;

			try
			{
				string ns = txtNamespace.Text;
				// Copy the zip file to disk
				string zipFile = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".Resources.BlankProvider.zip";
				string tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
				Directory.CreateDirectory(tempFolder);

				using (Stream s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(zipFile))
				{
					zipFile = Path.Combine(tempFolder, "BlankProvider.zip");
					Slyce.Common.Utility.WriteStreamToFile(s, zipFile);
				}
				// Unzip the zip file
				Slyce.Common.Utility.UnzipFile(zipFile, tempFolder);
				Slyce.Common.Utility.DeleteFileBrute(zipFile);

				if (ns != "BlankProvider")
				{
					// Replace the default namespace witht he user-selected namespace
					string text = Slyce.Common.Utility.ReadTextFile(Path.Combine(tempFolder, "BlankProvider.csproj"));
					File.WriteAllText(Path.Combine(tempFolder, ns + ".csproj"), text.Replace("BlankProvider", ns));
					Slyce.Common.Utility.DeleteFileBrute(Path.Combine(tempFolder, "BlankProvider.csproj"));

					text = Slyce.Common.Utility.ReadTextFile(Path.Combine(tempFolder, "BlankProvider.sln"));
					File.WriteAllText(Path.Combine(tempFolder, ns + ".sln"), text.Replace("BlankProvider", ns));
					Slyce.Common.Utility.DeleteFileBrute(Path.Combine(tempFolder, "BlankProvider.sln"));

					text = Slyce.Common.Utility.ReadTextFile(Path.Combine(tempFolder, "ProviderInfo.cs"));
					File.WriteAllText(Path.Combine(tempFolder, "ProviderInfo.cs"), text.Replace("BlankProvider", ns));

					text = Slyce.Common.Utility.ReadTextFile(Path.Combine(tempFolder, @"Properties\AssemblyInfo.cs"));
					File.WriteAllText(Path.Combine(tempFolder, @"Properties\AssemblyInfo.cs"), text.Replace("BlankProvider", ns));

					text = Slyce.Common.Utility.ReadTextFile(Path.Combine(tempFolder, @"Properties\Resources.Designer.cs"));
					File.WriteAllText(Path.Combine(tempFolder, @"Properties\Resources.Designer.cs"), text.Replace("BlankProvider", ns));

					text = Slyce.Common.Utility.ReadTextFile(Path.Combine(tempFolder, @"Screens\Screen1.cs"));
					File.WriteAllText(Path.Combine(tempFolder, @"Screens\Screen1.cs"), text.Replace("BlankProvider", ns));

					text = Slyce.Common.Utility.ReadTextFile(Path.Combine(tempFolder, @"Screens\Screen1.Designer.cs"));
					File.WriteAllText(Path.Combine(tempFolder, @"Screens\Screen1.Designer.cs"), text.Replace("BlankProvider", ns));

					text = Slyce.Common.Utility.ReadTextFile(Path.Combine(tempFolder, @"Screens\Screen2.cs"));
					File.WriteAllText(Path.Combine(tempFolder, @"Screens\Screen2.cs"), text.Replace("BlankProvider", ns));

					text = Slyce.Common.Utility.ReadTextFile(Path.Combine(tempFolder, @"Screens\Screen2.Designer.cs"));
					File.WriteAllText(Path.Combine(tempFolder, @"Screens\Screen2.Designer.cs"), text.Replace("BlankProvider", ns));
				}
				// Set the reference to ArchAngel.Interfaces.dll to the correct location on the user's machine
				string projFile = Path.Combine(tempFolder, ns + ".csproj");
				string code = Slyce.Common.Utility.ReadTextFile(projFile);
				int end = code.IndexOf(@"ArchAngel.Interfaces.dll</HintPath>") + @"ArchAngel.Interfaces.dll</HintPath>".Length;
				int start = code.LastIndexOf(@"<HintPath>", end);
				code = code.Remove(start, end - start);
				string realPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ArchAngel.Interfaces.dll");
				code = code.Insert(start, string.Format(@"<HintPath>{0}</HintPath>", realPath));
				File.WriteAllText(projFile, code);
				// Copy all the files and folders in the temp folder to the folder specified by the user
				foreach (string file in Directory.GetFiles(tempFolder))
				{
					File.Copy(file, Path.Combine(txtFolder.Text, Path.GetFileName(file)));
				}
				Directory.CreateDirectory(Path.Combine(txtFolder.Text, "Properties"));
				Directory.CreateDirectory(Path.Combine(txtFolder.Text, "Screens"));

				foreach (string file in Directory.GetFiles(Path.Combine(tempFolder, "Properties")))
				{
					File.Copy(file, Path.Combine(Path.Combine(txtFolder.Text, "Properties"), Path.GetFileName(file)));
				}
				foreach (string file in Directory.GetFiles(Path.Combine(tempFolder, "Screens")))
				{
					File.Copy(file, Path.Combine(Path.Combine(txtFolder.Text, "Screens"), Path.GetFileName(file)));
				}
				Slyce.Common.Utility.DeleteDirectoryBrute(tempFolder);

				if (MessageBox.Show(this, "Finished.\n\nOpen folder to view?", "Finished", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					System.Diagnostics.Process.Start(txtFolder.Text);
				}
				this.Close();
			}
			finally
			{
				Controller.Instance.MainForm.Cursor = Cursors.Default;
			}
		}

		private void frmNewProvider_FormClosing(object sender, FormClosingEventArgs e)
		{
			Controller.UnshadeMainForm();
		}

		private void linkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Controller.ShowHelpTopic(this, "Designer_Custom_Providers.htm");

		}
	}
}
