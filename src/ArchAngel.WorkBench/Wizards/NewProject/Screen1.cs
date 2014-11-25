using System;
using System.IO;
using System.Windows.Forms;
using ArchAngel.Interfaces.Wizards.NewProject;

namespace ArchAngel.Workbench.Wizards.NewProject
{
	[SmartAssembly.Attributes.DoNotObfuscate]
	public partial class Screen1 : UserControl, INewProjectScreen
	{
		public IFormNewProject NewProjectForm { get; set; }

		public Screen1()
		{
			InitializeComponent();
		}

		public void Setup()
		{
			NewProjectForm.Text = "New/Open Project";
			PopulateRecentFiles();
		}

		internal void PopulateRecentFiles()
		{
			listViewRecentFiles.Items.Clear();

			if (Controller.Instance.RecentFiles.Length > 0)
			{
				for (int i = 0; i < Controller.Instance.RecentFiles.Length; i++)
				{
					string filePath = Controller.Instance.RecentFiles[i];

					if (File.Exists(filePath))
					{
						string fileName = Path.GetFileName(filePath);
						ListViewItem newItem = listViewRecentFiles.Items.Add(fileName);
						newItem.ToolTipText = filePath;
						newItem.SubItems.Add(filePath);
						newItem.ImageIndex = 0;
					}
				}
			}
			if (listViewRecentFiles.Items.Count > 0)
			{
				listViewRecentFiles.Items[0].Selected = true;
				listViewRecentFiles.TabIndex = 0;
				btnNew.TabIndex = 20;
				listViewRecentFiles.Focus();
			}
			btnOpen.Enabled = listViewRecentFiles.Items.Count > 0;
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			OpenExistingProject();
		}

		private void listViewRecentFiles_DoubleClick(object sender, EventArgs e)
		{
			OpenExistingProject();
		}

		private void OpenExistingProject()
		{
			if (listViewRecentFiles.SelectedItems.Count > 0)
			{
				NewProjectForm.UserChosenAction = NewProjectFormActions.ExistingProject;
				NewProjectForm.ExistingProjectPath = listViewRecentFiles.SelectedItems[0].SubItems[1].Text;
				NewProjectForm.CloseCausedByLoadingNextScreen = true;
				NewProjectForm.Finish();
				//NewProjectForm.Close();
			}
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			NewProjectForm.UserChosenAction = NewProjectFormActions.NewProject;
			NewProjectForm.LoadScreen("LoadExistingProject");
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			string path = Path.GetDirectoryName(Application.ExecutablePath);

			if (Directory.Exists(path))
			{
				dialog.InitialDirectory = path;
			}
			else
			{
				dialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
			}
			dialog.Filter = Branding.FormTitle + " Project (*.wbproj, *.aaproj)|*.wbproj;*.aaproj";

			if (dialog.ShowDialog(this.ParentForm) == DialogResult.OK)
			{
				if (File.Exists(dialog.FileName))
				{
					NewProjectForm.ExistingProjectPath = dialog.FileName;
					NewProjectForm.UserChosenAction = NewProjectFormActions.ExistingProject;
					NewProjectForm.Finish();
					//NewProjectForm.CloseCausedByLoadingNextScreen = true;
					//NewProjectForm.Close();
					//NewProjectForm.CloseCausedByLoadingNextScreen = false;
				}
				else
				{
					MessageBox.Show("Invalid project file specified.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}

		}

		private void listViewRecentFiles_KeyPress(object sender, KeyPressEventArgs e)
		{
			OpenExistingProject();
		}

		private void buttonCheckForUpdates_Click(object sender, EventArgs e)
		{
			Controller.Instance.ShadeMainForm();
			Slyce.Common.Updates.frmUpdate form = new Slyce.Common.Updates.frmUpdate(Branding.ProductBranding.ToString());
			form.ShowDialog(this);
			Controller.Instance.UnshadeMainForm();
		}

	}
}
