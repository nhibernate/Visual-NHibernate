using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace ArchAngel.Workbench.OptionsItems
{
	public partial class General : OptionScreen
	{
		private bool BusyPopulating = false;

		public General()
		{
			InitializeComponent();
			Title = "General";
			PageHeader = "General";
			PageDescription = "General settings.";
			lblTemplate.Text = Branding.ProductName + " template:";
			Populate();
		}

		private void Populate()
		{
			DisplayTopPanel = true;

			if (Controller.Instance.CurrentProject == null || Controller.Instance.CurrentProject.ProjectSettings == null || string.IsNullOrEmpty(Controller.Instance.CurrentProject.ProjectSettings.TemplateFileName))
			{
				textBoxTemplatePath.Text = "";
			}
			else
			{
				textBoxTemplatePath.Text = Controller.Instance.CurrentProject.ProjectSettings.TemplateFileName;
			}
		}

		public override bool OnSave()
		{
			if (textBoxTemplatePath.Text != Controller.Instance.CurrentProject.ProjectSettings.TemplateFileName)
			{
				if (ValidateTemplatePath() == false)
					return false;
				Controller.Instance.LoadTemplate(textBoxTemplatePath.Text);
			}

			return true;
		}

		private bool ValidateTemplatePath()
		{
			if (!BusyPopulating)
			{
				if (string.IsNullOrEmpty(textBoxTemplatePath.Text) ||
					!File.Exists(textBoxTemplatePath.Text))
				{
					errorProvider1.SetError(textBoxTemplatePath, "Invalid Template File");
					return false;
				}
			}
			errorProvider1.Clear();
			return true;
		}

		private void textBoxTemplateFileName_Validating(object sender, CancelEventArgs e)
		{
			e.Cancel = !ValidateTemplatePath();
		}

		private void buttonTemplatePath_Click(object sender, EventArgs e)
		{
			Controller.Instance.ShadeMainForm();
			OpenFileDialog openFileDialog = new OpenFileDialog();

			if (textBoxTemplatePath.Text != "" && File.Exists(textBoxTemplatePath.Text))
			{
				openFileDialog.FileName = textBoxTemplatePath.Text;
			}
			else if (textBoxTemplatePath.Text.Length > 0 && Directory.Exists(Path.GetDirectoryName(textBoxTemplatePath.Text)))
			{
				openFileDialog.InitialDirectory = Path.GetDirectoryName(textBoxTemplatePath.Text);
			}
			else
			{
				openFileDialog.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Branding.FormTitle + Path.DirectorySeparatorChar + "Templates");
			}
			openFileDialog.Filter = Branding.ProductName + " Template (*.AAT.DLL)|*.AAT.DLL";

			if (openFileDialog.ShowDialog(ParentForm) != DialogResult.OK)
			{
				Controller.Instance.UnshadeMainForm();
				return;
			}
			textBoxTemplatePath.Text = openFileDialog.FileName;

			Controller.Instance.UnshadeMainForm();
		}

		private void LoadTemplate(string templateName)
		{
			if (File.Exists(templateName))
			{
				if (Controller.Instance.IsDirty)
				{
					try
					{
						Controller.Instance.ShadeMainForm();

						if (MessageBox.Show("Save changes before loading new template?", "Save", MessageBoxButtons.YesNo,
											MessageBoxIcon.Question) == DialogResult.Yes)
						{
							Controller.Instance.MainForm.Save();
						}
					}
					finally
					{
						Controller.Instance.UnshadeMainForm();
					}
				}
			}

			try
			{
				Controller.Instance.LoadTemplate(templateName);

				textBoxTemplatePath.Text = templateName;
				FormMain.ContentItemOptions.ShowOptions(); // TODO: GFH: this should be handled with events, not called directly
			}
			catch (Exception e)
			{
				MessageBox.Show(this, e.Message, "An error occurred while loading the template", MessageBoxButtons.OK,
								MessageBoxIcon.Error);
			}
		}
	}
}
