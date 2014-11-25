using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Designer.DesignerProject;

namespace ArchAngel.Designer
{
	public partial class ucProjectDetails : UserControl
	{
		private bool BusyPopulating = false;
		private Color MissingFileBackColour = Color.PeachPuff;
		private bool BusyClearing = false;
		private Color CurrentBaseColor = Color.Empty;

		public ucProjectDetails()
		{
			InitializeComponent();

			if (Slyce.Common.Utility.InDesignMode) { return; }

			EnableDoubleBuffering();
			Populate();
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

		public void Populate()
		{
			if (Project.Instance == null)
			{
				// This is a new file and no options have been set yet
				// Make sure the user saves on exiting
				Project.Instance.IsDirty = true;
				return;
			}

			BusyPopulating = true;
			txtDescription.Text = Project.Instance.ProjectDescription;
			txtOutputFile.Text = Project.Instance.CompileFolderName;
			txtName.Text = Project.Instance.ProjectName;
			txtVersion.Text = Project.Instance.Version;
			LoadSettings();
			BusyPopulating = false;
		}

		public void Clear()
		{
			BusyClearing = true;
			txtDescription.Text = "";
			txtOutputFile.Text = "";
			txtName.Text = "";
			txtVersion.Text = "";
			lstNamespaces.Items.Clear();
			gridReferencedFiles.Rows.Clear();
			//treeReferencedFiles.BeginUpdate();
			//treeReferencedFiles.Nodes.Clear();
			//treeReferencedFiles.EndUpdate();
			BusyClearing = false;
		}

		public void ShowNew()
		{
			Clear();
			txtDescription.Text = "New Template";
			txtOutputFile.Text = "";
			txtName.Text = "NewTemplate";
			txtVersion.Text = "1.0.0.0";
		}

		/// <summary>
		/// Loads ssttings that used to appear in the Settings window.
		/// </summary>
		private void LoadSettings()
		{
			PopulateReferences();
			PopulateNamespaces();
		}

		private void PopulateReferences()
		{
			//treeReferencedFiles.BeginUpdate();
			//treeReferencedFiles.Nodes.Clear();
			gridReferencedFiles.Rows.Clear();

			foreach (var refFile in Project.Instance.References)
			{
				AddReferenceToList(Path.GetFileName(refFile.FileName), refFile.FileName, refFile.MergeWithAssembly, refFile.UseInWorkbench, refFile.IsProvider);
			}
			ProcessMissingFiles();
			//treeReferencedFiles.EndUpdate();
		}

		private void PopulateNamespaces()
		{
			lstNamespaces.Items.Clear();

			for (int i = 0; i < Project.Instance.Namespaces.Count; i++)
			{
				lstNamespaces.Items.Add(Project.Instance.Namespaces[i]);
			}
		}

		private void AddReferenceToList(string file, string path, bool merge, bool useInWorkbench, bool isProvider)
		{
			AddReferenceToList(file, path, merge, useInWorkbench, isProvider, -1);
		}

		private void AddReferenceToList(string file, string path, bool merge, bool useInWorkbench, bool isProvider, int selectedIndex)
		{
			if (Slyce.Common.Utility.StringsAreEqual(file, "system.dll", false) ||
				Slyce.Common.Utility.StringsAreEqual(file, "mscorlib.dll", false))
			{
				return;
			}

			if (!isProvider)
				useInWorkbench = false;

			if (selectedIndex < 0)
			{
				if (isProvider)
				{
					int newIndex = gridReferencedFiles.Rows.Add(new object[] { file, useInWorkbench, path });
					//gridReferencedFiles.Rows[newIndex].Cells["colRefDisplay"].ValueType = typeof(bool);
					//gridReferencedFiles.Rows[newIndex].Cells["colRefDisplay"].Value = useInWorkbench;
				}
				else
				{
					int newIndex = gridReferencedFiles.Rows.Add(new object[] { file, null, path });
					//gridReferencedFiles.Rows[newIndex].Cells["colRefDisplay"].ValueType = typeof(string);
					//gridReferencedFiles.Rows[newIndex].Cells["colRefDisplay"].Value = "";
					gridReferencedFiles.Rows[newIndex].Cells["colRefDisplay"].ReadOnly = true;
				}
			}
			else
			{
				gridReferencedFiles.Rows[selectedIndex].Cells["colRefFile"].Value = file;
				gridReferencedFiles.Rows[selectedIndex].Cells["colRefPath"].Value = path;

				gridReferencedFiles.Rows[selectedIndex].Cells["colRefDisplay"].ValueType = typeof(bool);

				if (isProvider)
				{
					gridReferencedFiles.Rows[selectedIndex].Cells["colRefDisplay"].Value = useInWorkbench;
				}
				else
				{
					gridReferencedFiles.Rows[selectedIndex].Cells["colRefDisplay"].Value = false;
				}
			}
		}

		private void ProcessMissingFiles()
		{
			foreach (System.Windows.Forms.DataGridViewRow row in gridReferencedFiles.Rows)
			{
				if (!File.Exists(row.Cells["colRefPath"].Value.ToString()))
				{
					row.Cells["colRefPath"].ErrorText = "Missing";
				}
				else
				{
					row.Cells["colRefPath"].ErrorText = "";
				}
			}
		}

		private void ShowFileDialogForReference(bool editSelected)
		{
			//int selectedIndex = -1;
			openFileDialog1.Filter = "DLL files (*.DLL)|*.DLL";
			openFileDialog1.Filter += "|ArchAngel Templates (*.AAT.DLL)|*.AAT.DLL";
			openFileDialog1.Filter += "|All files (*.*)|*.*";
			openFileDialog1.DefaultExt = ".DLL";
			openFileDialog1.FileName = "";
			openFileDialog1.InitialDirectory = Environment.CurrentDirectory;

			if (gridReferencedFiles.SelectedRows.Count > 0)
			{
				string file = (string)gridReferencedFiles.SelectedRows[0].Cells["colRefPath"].Value;// lstReferences2.SelectedItems[0].SubItems[2].Text;
				openFileDialog1.FileName = file;
				openFileDialog1.DefaultExt = Path.GetExtension(file);

				if (file.Length > 0 &&
					Directory.Exists(Path.GetDirectoryName(file)))
				{
					openFileDialog1.InitialDirectory = Path.GetDirectoryName(file);
				}
				if (file.EndsWith(".aat.dll", StringComparison.OrdinalIgnoreCase))
				{
					openFileDialog1.Filter = "ArchAngel Template files (*.AAT.DLL)|*.AAT.DLL|DLL files (*.DLL)|*.DLL|All files (*.*)|*.*";
				}
				else
				{
					openFileDialog1.Filter = "DLL files (*.DLL)|*.DLL|ArchAngel Template files (*.AAT.DLL)|*.AAT.DLL|All files (*.*)|*.*";
				}
			}
			if (!editSelected ||
				(gridReferencedFiles.SelectedRows.Count > 0))
			{
				Controller.ShadeMainForm();

				if (openFileDialog1.ShowDialog(this.ParentForm) == DialogResult.OK)
				{
					Controller.UnshadeMainForm();
					Controller.Instance.MainForm.Refresh();

					string fileName = openFileDialog1.FileName;

					if (fileName.Length > 0)
					{
						if (editSelected && Path.GetFileName(fileName.ToLower()) != GetSelectedAssemblyFilename().ToLower())
						{
							MessageBox.Show(this, "Filename doesn't match the referenced file you are trying to edit.", "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						try
						{
							Cursor = Cursors.WaitCursor;
							bool merge = false;
							bool useInWorkbench = false;

							if (editSelected)
							{
								NotifyUserThatARestartIsRequired(RestartReason.ReferencedAssemblyEdited);

								bool isProvider = IsSelectedRowReadOnly();

								if (isProvider)
								{
									useInWorkbench = (bool)gridReferencedFiles.SelectedRows[0].Cells["colRefDisplay"].Value;
								}
								AddReferenceToList(Path.GetFileName(fileName), fileName, merge, useInWorkbench, isProvider, gridReferencedFiles.SelectedRows[0].Index);
							}
							else
							{
								if (Project.Instance.AssemblyNameAlreadyLoadedFromAnotherLocation(fileName))
								{
									NotifyUserThatARestartIsRequired(RestartReason.NewAssemblyCannotBeLoadedDueToNameConflict);
								}

								var referencedFile = new ReferencedFile(fileName, false, false);
								Project.Instance.AddReferencedFile(referencedFile);
								AddReferenceToList(Path.GetFileName(fileName), fileName, merge, useInWorkbench, referencedFile.IsProvider);
							}
							// Check whether any of the other missing files can be found at the same new location
							string dir = Path.GetDirectoryName(fileName);

							for (int i = 0; i < gridReferencedFiles.Rows.Count; i++)
							{
								if (gridReferencedFiles.Rows[i].Tag != null && (Color)gridReferencedFiles.Rows[i].Tag == MissingFileBackColour)
								{
									string tempPath = Path.Combine(dir, (string)gridReferencedFiles.Rows[i].Cells["colRefFile"].Value);

									if (File.Exists(tempPath))
									{
										gridReferencedFiles.Rows[i].Cells["colRefPath"].Value = tempPath;
										gridReferencedFiles.Rows[i].Tag = Color.White;

										// Update the path in References
										for (int refFileCounter = 0; refFileCounter < Project.Instance.References.Count(); refFileCounter++)
										{
											var refFile = Project.Instance.References.ElementAt(refFileCounter);
											if (Slyce.Common.Utility.StringsAreEqual(Path.GetFileName(refFile.FileName), Path.GetFileName((string)gridReferencedFiles.Rows[i].Cells["colRefFile"].Value), false))
											{
												string filePath = Path.Combine(dir, Path.GetFileName(refFile.FileName));
												refFile.FileName = filePath;
											}
										}
									}
								}
							}
							Save(true);
						}
						finally
						{
							Cursor = Cursors.Default;
						}
					}
				}
				Controller.UnshadeMainForm();
			}
			ProcessMissingFiles();
		}

		private enum RestartReason
		{
			ReferencedAssemblyEdited,
			NewAssemblyCannotBeLoadedDueToNameConflict
		}

		private HashSet<RestartReason> restartReasonsTheUserHasAlreadyBeenNotifiedAbout = new HashSet<RestartReason>();
		private void NotifyUserThatARestartIsRequired(RestartReason reason)
		{
			if (restartReasonsTheUserHasAlreadyBeenNotifiedAbout.Contains(reason)) return;

			string reasonText;

			switch (reason)
			{
				case RestartReason.ReferencedAssemblyEdited:
					reasonText =
						"Referenced Assemblies cannot be reloaded from different paths without a restart. ";
					break;
				case RestartReason.NewAssemblyCannotBeLoadedDueToNameConflict:
					reasonText =
						"An Assembly has already been loaded with that name (not filename, actual Assembly name) from a different path.";
					break;
				default:
					throw new ArgumentOutOfRangeException("reason");
			}

			reasonText += "Any changes you make will not take " +
						"effect until you save and restart the application. You may continue to make changes, " +
						"just remember to save your project and restart ArchAngel when you are done";

			MessageBox.Show(this, reasonText, "Application Restart Required");
			restartReasonsTheUserHasAlreadyBeenNotifiedAbout.Add(reason);
		}

		private bool IsSelectedRowReadOnly()
		{
			return gridReferencedFiles.SelectedRows[0].Cells["colRefDisplay"].State != DataGridViewElementStates.ReadOnly;
		}

		private string GetSelectedAssemblyFilename()
		{
			return gridReferencedFiles.SelectedRows[0].Cells["colRefFile"].Value.ToString();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			switch (btnCancelNamespace.Text)
			{
				case "&Cancel":

					txtNamespace.Text = "";
					txtNamespace.Visible = false;
					btnAddNamespace.Text = "Add";
					btnAddNamespace.Focus();
					btnCancelNamespace.Text = "Remove";
					break;
				case "&Remove":
					lstNamespaces.Items.Remove(lstNamespaces.SelectedItem);
					break;
				default:
					throw new Exception("Not coded yet");
			}
			Save(true);
		}

		private void btnAddNamespace_Click(object sender, EventArgs e)
		{
			AddNamespace();
		}

		private void AddNamespace()
		{
			//Project.Instance.AddNamespace(txtNamespace.Text);
			//txtNamespace.Clear();
			//Populate();

			bool exists = false;

			for (int i = 0; i < lstNamespaces.Items.Count; i++)
			{
				if (lstNamespaces.Items[i].ToString() == txtNamespace.Text)
				{
					exists = true;
					break;
				}
			}
			if (!exists && txtNamespace.Text.Length > 0)
			{
				lstNamespaces.Items.Add(txtNamespace.Text);
				txtNamespace.Text = "";
			}
			txtNamespace.Focus();
			Save();
		}

		/// <summary>
		/// Saves the data for this screen.
		/// </summary>
		public void Save()
		{
			Save(false);
		}

		/// <summary>
		/// Saves the data for this screen.
		/// </summary>
		/// <param name="referencesHaveChanged">Sets whether a reference has been added or removed, so that we know whether to 
		/// update the API Extensions screen with the data for new referenced DLLs or remove data for removed DLLs.</param>
		public void Save(bool referencesHaveChanged)
		{
			if (BusyClearing) { return; }
			Project.Instance.ProjectName = txtName.Text;
			Project.Instance.ProjectDescription = txtDescription.Text;
			Project.Instance.CompileFolderName = txtOutputFile.Text;
			Project.Instance.Version = txtVersion.Text;
			Project.Instance.ProjType = ProjectTypes.Template;



			// Namespaces
			Project.Instance.Namespaces.Clear();

			for (int i = 0; i < lstNamespaces.Items.Count; i++)
			{
				Project.Instance.AddNamespace(lstNamespaces.Items[i].ToString());
			}
			Project.Instance.IsDirty = true;
			//TODO:Controller.Instance.MainForm.RefreshIntellisense();
			if (referencesHaveChanged)
			{
				// References
				List<ReferencedFile> references = new List<ReferencedFile>();

				gridReferencedFiles.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange);
				foreach (DataGridViewRow row in gridReferencedFiles.Rows)
				{
					object refDisplay = row.Cells["colRefDisplay"].Value;
					bool useInWorkbench = refDisplay == null || refDisplay is bool == false ? false : (bool)refDisplay;
					string file = (string)row.Cells["colRefPath"].Value;
					references.Add(new ReferencedFile(file, false, useInWorkbench));
				}
				Project.Instance.SetReferencedFiles(references);

				Controller.Instance.MainForm.UcApiExtensions.Populate();
			}
		}

		private void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!BusyPopulating) { Save(); }
		}

		private void txtTemplateNamespace_TextChanged(object sender, EventArgs e)
		{
			// TODO: wait until LostFocus and dirty
			if (!BusyPopulating) { Save(); }
		}

		private void txtDescription_TextChanged(object sender, EventArgs e)
		{
			// TODO: wait until LostFocus and dirty
			if (!BusyPopulating) { Save(); }
		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{
			// TODO: wait until LostFocus and dirty
			if (!BusyPopulating) { Save(); }
		}


		private void txtName_Validated(object sender, EventArgs e)
		{
			Project.Instance.ProjectName = txtName.Text;
		}

		private void txtDescription_Validated(object sender, EventArgs e)
		{
			Project.Instance.ProjectDescription = txtDescription.Text;
		}

		private void txtDescription_Leave(object sender, EventArgs e)
		{
			Project.Instance.ProjectDescription = txtDescription.Text;
		}

		private void txtVersion_Validated(object sender, EventArgs e)
		{
			Project.Instance.Version = txtVersion.Text;
		}

		private void btnOutputFile_Click(object sender, EventArgs e)
		{
			string fullPath = txtOutputFile.Text;

			if (File.Exists(fullPath))
			{
				folderBrowserDialog1.SelectedPath = fullPath;
			}
			Controller.ShadeMainForm();

			if (folderBrowserDialog1.ShowDialog(this.ParentForm) != DialogResult.OK)
			{
				Controller.UnshadeMainForm();
				return;
			}
			Controller.UnshadeMainForm();

			string templateFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), @"Samples\Templates");

			if (folderBrowserDialog1.SelectedPath == templateFolder)
			{
				MessageBox.Show(this, "You can't generate to the Samples folder, because these files get overwritten when installing a new version.", "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (txtOutputFile.Text != folderBrowserDialog1.SelectedPath)
			{
				txtOutputFile.Text = folderBrowserDialog1.SelectedPath;

				if (Project.Instance.CompileFolderName != txtOutputFile.Text)
				{
					Project.Instance.CompileFolderName = txtOutputFile.Text;
					Project.Instance.IsDirty = true;
				}
			}
		}

		private void txtOutputFile_TextChanged(object sender, EventArgs e)
		{
			Project.Instance.IsDirty = true;
		}

		private void txtOutputFile_Validated(object sender, EventArgs e)
		{
			if (Project.Instance.CompileFolderName != txtOutputFile.Text)
			{
				Project.Instance.CompileFolderName = txtOutputFile.Text;
				Project.Instance.IsDirty = true;
			}
		}

		private void lstReferences2_DoubleClick(object sender, EventArgs e)
		{
			ShowFileDialogForReference(true);
		}

		private void btnCancelNamespace_Click(object sender, EventArgs e)
		{
			RemoveNamespace();
		}

		private void RemoveNamespace()
		{
			Project.Instance.RemoveNamespace((string)lstNamespaces.SelectedItem);
			lstNamespaces.Items.Remove(lstNamespaces.SelectedItem);
		}

		private void ucProjectDetails_Paint(object sender, PaintEventArgs e)
		{
			if (CurrentBaseColor != Slyce.Common.Colors.BaseColor)
			{
				CurrentBaseColor = Slyce.Common.Colors.BaseColor;
				this.BackColor = Slyce.Common.Colors.BackgroundColor;
			}
		}

		private void txtName_Validating(object sender, CancelEventArgs e)
		{
			txtName.Text = txtName.Text.Replace(" ", "");
		}

		private void lstNamespaces_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				RemoveNamespace();
			}
		}

		private void txtNamespace_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				AddNamespace();
			}
		}

		private void txtName_TextChanged_1(object sender, EventArgs e)
		{
			Project.Instance.ProjectName = txtName.Text;
			Project.Instance.IsDirty = true;
		}

		private void txtVersion_TextChanged(object sender, EventArgs e)
		{
			Project.Instance.Version = txtVersion.Text;
			Project.Instance.IsDirty = true;
		}

		private void txtDescription_TextChanged_1(object sender, EventArgs e)
		{
			Project.Instance.ProjectDescription = txtDescription.Text;
			Project.Instance.IsDirty = true;
		}

		private void gridReferencedFiles_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			Save(true);
		}

		private void btnAddReferencedFile_Click(object sender, EventArgs e)
		{
			ShowFileDialogForReference(false);
			// Reset the allowed (exposed) script parameters
			Project.Instance.AllowedScriptParameters = null;
		}

		private void txtOutputFile_Validating(object sender, CancelEventArgs e)
		{
			string templateFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), @"Samples\Templates");

			if (!string.IsNullOrEmpty(txtOutputFile.Text) && Path.GetDirectoryName(txtOutputFile.Text) == templateFolder)
			{
				MessageBox.Show(this, "You can't generate to the Samples folder, because these files get overwritten when installing a new version.", "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}

		private void buttonEditReference_Click(object sender, EventArgs e)
		{
			ShowFileDialogForReference(true);
		}

		private void gridReferencedFiles_SelectionChanged(object sender, EventArgs e)
		{
			bool oneRowSelected = gridReferencedFiles.SelectedRows.Count == 1;

			buttonEditReference.Enabled = oneRowSelected;

			bool multipleRowsSelected = gridReferencedFiles.SelectedRows.Count > 1;
			buttonDeleteReference.Enabled = multipleRowsSelected;
		}

		private void buttonDeleteReference_Click(object sender, EventArgs e)
		{
			List<int> rowsToRemove = new List<int>();

			if (gridReferencedFiles.SelectedCells.Count > 0)
			{
				foreach (var cell in gridReferencedFiles.SelectedCells)
				{
					int rowIndex = ((DataGridViewCell)cell).RowIndex;

					if (!rowsToRemove.Contains(rowIndex))
						rowsToRemove.Add(rowIndex);
				}
				rowsToRemove.Sort();

				for (int index = rowsToRemove.Count - 1; index >= 0; index--)
				{
					gridReferencedFiles.Rows.RemoveAt(rowsToRemove[index]);
				}
				Save(true);
			}
		}
	}
}
