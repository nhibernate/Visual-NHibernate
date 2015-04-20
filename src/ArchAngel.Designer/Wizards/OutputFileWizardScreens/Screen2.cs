using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Interfaces;
using Slyce.Common;

namespace ArchAngel.Designer.Wizards.OutputFileWizardScreens
{
	public partial class Screen2 : Interfaces.Controls.ContentItems.ContentItem
	{
		public Screen2()
		{
			InitializeComponent();
			HasNext = true;
			HasPrev = true;

			switch (frmOutputFileWizard.FileType)
			{
				case frmOutputFileWizard.FileTypes.Script:
					PageHeader = "Iterator Selection";
					PageDescription = "Select the type of object you want to iterate over.";
					lblHeadingFiles.Text = "Create one of these files for every instance of:";
					break;
				case frmOutputFileWizard.FileTypes.Folder:
					PageHeader = "Iterator Selection";
					PageDescription = "Select the type of object you want to iterate over.";
					lblHeadingFiles.Text = "Create one of these folders for every instance of:";
					HasNext = true;
					HasPrev = false;
					break;
				case frmOutputFileWizard.FileTypes.Static:
					PageHeader = "File Selection";
					PageDescription = "Select the static file to include.";
					break;
				default:
					throw new NotImplementedException("Not coded yet.");
			}
			OnDisplaying();
			ResizePanels();
			Populate();
			PopulateStaticFiles();
		}

		private void Populate()
		{
			listTypes.Items.Clear();
			listTypes.Items.Add("    NO ITERATOR (one per project)");
			var emptyNamespaceList = new List<string>();

			var generatorIterators =
				ExtensionAttributeHelper.GetGeneratorIteratorTypes(Project.Instance.ReferencedAssemblies);
			foreach (var type in generatorIterators.OrderBy(t => t.FullName))
			{
				listTypes.Items.Add(Utility.GetDemangledGenericTypeName(type, emptyNamespaceList));
				var alternativeForms = ExtensionAttributeHelper.GetAlternativeForms(type);

				foreach (Type alternativeForm in alternativeForms.OrderBy(t => t.FullName))
				{
					listTypes.Items.Add(Utility.GetDemangledGenericTypeName(alternativeForm, emptyNamespaceList));
				}
			}
		}

		public override void OnDisplaying()
		{
			if (frmOutputFileWizard.FileType == frmOutputFileWizard.FileTypes.Script ||
				frmOutputFileWizard.FileType == frmOutputFileWizard.FileTypes.Folder)
			{
				panelStaticFile.Visible = false;
				panelScriptFile.Visible = true;
				NextText = "&Next >";
				HasFinish = false;
				SelectIteratorType(frmOutputFileWizard.IterationType);
			}
			else
			{
				panelScriptFile.Visible = true;
				panelStaticFile.Visible = true;

				for (int i = 0; i < listStaticFiles.Items.Count; i++)
				{
					if (Utility.StringsAreEqual((string)listStaticFiles.Items[i], frmOutputFileWizard.StaticFileName, false))
					{
						listStaticFiles.SelectedIndex = i;
						break;
					}
				}
				SelectIteratorType(frmOutputFileWizard.IterationType);
			}
			ResizePanels();
			base.OnDisplaying();
		}

		private void SelectIteratorType(Type iteratorType)
		{
			if (listTypes.Items.Count == 0) return;

			if (iteratorType == null)
			{
				listTypes.SelectedIndex = 0;
				return;
			}

			string iteratorTypeName = iteratorType.FullName;

			for (int i = 0; i < listTypes.Items.Count; i++)
			{
				if (Utility.StringsAreEqual((string)listTypes.Items[i], iteratorTypeName, false))
				{
					listTypes.SelectedIndex = i;
					break;
				}
			}
		}

		public override bool Next()
		{
			if (listTypes.SelectedIndex < 0)
			{
				MessageBox.Show(this, "You must select what type of object to iterate over.", "Missing type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (frmOutputFileWizard.FileType == frmOutputFileWizard.FileTypes.Static)
			{
				frmOutputFileWizard.StaticFileName = (string)listStaticFiles.SelectedItem;
			}
			// Check for no iterator
			if (listTypes.SelectedIndex == 0)
			{
				frmOutputFileWizard.IterationType = null;
				return true;
			}
			foreach (System.Reflection.Assembly assembly in Project.Instance.ReferencedAssemblies)
			{
				if (Interfaces.ProviderInfo.IsProvider(assembly))
				{
					Type type = assembly.GetType((string)listTypes.SelectedItem);

					if (type != null)
					{
						frmOutputFileWizard.IterationType = type;
						break;
					}
				}
			}
			return true;
		}

		public override bool Save()
		{
			if (frmOutputFileWizard.FileType == frmOutputFileWizard.FileTypes.Static)
			{
				if (listStaticFiles.SelectedIndex < 0)
				{
					MessageBox.Show(this, "You must select a file from the list.", "Missing file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
				frmOutputFileWizard.StaticFileName = (string)listStaticFiles.SelectedItem;
			}
			return true;
		}

		private void PopulateStaticFiles()
		{
			listStaticFiles.Items.Clear();

			foreach (string file in Project.Instance.IncludedFiles.Select(f => f.DisplayName))
			{
				if (file != "definition.xml")
				{
					listStaticFiles.Items.Add(file);
				}
			}
		}

		private void buttonAddStaticFile_Click(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog();

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				string fileNameOnly = Path.GetFileName(dialog.FileName);

				foreach (string existingFileName in listStaticFiles.Items)
				{
					if (Slyce.Common.Utility.StringsAreEqual(existingFileName, fileNameOnly, false))
					{
						MessageBox.Show(this, "A file of that name is already part of the project. Please rename the file before importing.", "Duplicate Filename", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
				}

				//File.Copy(dialog.FileName, Path.Combine(Controller.TempPath, fileNameOnly));
				Project.Instance.AddIncludedFile(new IncludedFile(dialog.FileName));
				PopulateStaticFiles();

				for (int i = 0; i < listStaticFiles.Items.Count; i++)
				{
					if (Utility.StringsAreEqual((string)listStaticFiles.Items[i], fileNameOnly, false))
					{
						listStaticFiles.SelectedIndex = i;
						break;
					}
				}
			}
		}

		private void Screen2_Resize(object sender, EventArgs e)
		{
			ResizePanels();
		}

		private void ResizePanels()
		{
			if (panelScriptFile.Visible && panelStaticFile.Visible)
			{
				panelStaticFile.Top = 0;
				//panelStaticFile.Height = ClientSize.Height / 2;
				panelStaticFile.Left = 0;
				panelStaticFile.Width = ClientSize.Width;

				panelScriptFile.Top = panelStaticFile.Bottom + 1;
				//panelScriptFile.Height = ClientSize.Height - panelScriptFile.Top - 1;
				panelScriptFile.Left = 0;
				panelScriptFile.Width = ClientSize.Width;

				//ClientSize = new Size(ClientSize.Width, panelStaticFile.Height + panelScriptFile.Height + 2);
			}
			else
			{
				Panel visiblePanel = panelScriptFile.Visible ? panelScriptFile : panelStaticFile;

				visiblePanel.Top = 0;
				visiblePanel.Height = ClientSize.Height;
				visiblePanel.Left = 0;
				visiblePanel.Width = ClientSize.Width;
			}
		}
	}
}
