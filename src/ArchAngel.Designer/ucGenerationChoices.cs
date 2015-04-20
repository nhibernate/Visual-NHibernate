using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ArchAngel.Designer.DesignerProject;
using DevComponents.AdvTree;
using Slyce.Common;

namespace ArchAngel.Designer
{
	public partial class ucGenerationChoices : UserControl
	{
		#region Structs

		private class TagInfo
		{
			public enum FileTypes
			{
				Folder,
				NormalFile,
				ScriptFile
			}
			public string Id;
			public FileTypes FileType;


			public TagInfo(string id, FileTypes fileType)
			{
				Id = id;
				FileType = fileType;
			}
		}

		#endregion

		#region Enums
		enum CellTypes
		{
			FileFolderName = 0,
			Function = 1,
			Iterator = 2,
			SkipFunction = 3
		}
		#endregion

		private const int IMG_CLOSED_FOLDER = 0;
		private const int IMG_OPEN_FOLDER = 1;
		private const int IMG_TEMPLATE_SCRIPT = 4;
		private const int IMG_NORMAL_SCRIPT = 2;
		private const int IMG_FILE = 5;
		private const int IMG_ROOT = 3;

		public ucGenerationChoices()
		{
			InitializeComponent();
			if (Slyce.Common.Utility.InDesignMode) { return; }

			EnableDoubleBuffering();
			treeFiles.AllowDrop = true;
			treeFiles.DragDropEnabled = true;
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

		public void Clear()
		{
			treeFiles.Nodes.Clear();
		}

		public void ShowNew()
		{
			Clear();
			Populate();
		}

		public void Populate()
		{
			Node rootNode2 = null;

			try
			{
				treeFiles.BeginUpdate();
				treeFiles.Nodes.Clear();
				ProcessFolder(Project.Instance.RootOutput, ref rootNode2);

				if (rootNode2 != null)
				{
					rootNode2.ExpandAll();
					rootNode2.Expand();
				}
			}
			finally
			{
				treeFiles.EndUpdate();
			}
		}

		private void ProcessFolder(OutputFolder folder, ref Node treenode)
		{
			if (treenode == null && Slyce.Common.Utility.StringsAreEqual(folder.Name, "root", false))
			{
				treenode = new Node();
				treenode.Text = "ROOT";
				treenode.Cells.Add(new Cell(""));
				treenode.Cells.Add(new Cell(""));
				treenode.ImageIndex = IMG_ROOT;
				treenode.Tag = new TagInfo(folder.Id, TagInfo.FileTypes.Folder);
				treeFiles.Nodes.Add(treenode);
			}
			else if (folder.Files.Count > 0 ||
				 folder.Folders.Count > 0 &&
				 treenode.ImageIndex != IMG_ROOT)
			{
				treenode.ImageIndex = IMG_OPEN_FOLDER;
			}
			foreach (OutputFolder subFolder in folder.Folders)
			{
				// TODO: This check for a root folder can be removed once all templates have been saved without
				// the extra root folders. Remove anytime after September 2006.
				if (Slyce.Common.Utility.StringsAreEqual(subFolder.Name, "root", false)) { continue; }

				Node newNode = AddFolderNode(treenode, subFolder);
				ProcessFolder(subFolder, ref newNode);
			}
			foreach (OutputFile file in folder.Files)
			{
				AddFileNode(treenode, file);
			}
		}

		private Node AddFolderNode(Node parentNode, OutputFolder subFolder)
		{
			string iteratorName = subFolder.IteratorType == null ? "" : subFolder.IteratorType.FullName;
			Node newNode = new Node();
			newNode.Text = subFolder.Name;
			newNode.DragDropEnabled = true;
			newNode.Cells.Add(new Cell(""));
			newNode.Cells.Add(new Cell(iteratorName));
			newNode.ImageIndex = IMG_CLOSED_FOLDER;
			newNode.Tag = new TagInfo(subFolder.Id, TagInfo.FileTypes.Folder);
			parentNode.Nodes.Add(newNode);
			return newNode;
		}

		private Node AddFileNode(Node parentNode, OutputFile file)
		{
			int imageType = file.ScriptName.Length == 0 ? IMG_FILE : IMG_TEMPLATE_SCRIPT;
			string secondColumn = "";

			switch (file.FileType)
			{
				case OutputFileTypes.Script:
					secondColumn = file.ScriptName;
					break;
				case OutputFileTypes.File:
					secondColumn = "[File] " + file.StaticFileName;
					break;
				default:
					throw new NotImplementedException("Not coded yet.");
			}
			Node newNode = new Node();
			newNode.Text = file.Name;
			newNode.DragDropEnabled = true;
			newNode.Cells.Add(new Cell(secondColumn));
			newNode.Cells.Add(new Cell(file.IteratorTypes));
			newNode.Cells.Add(new Cell());
			newNode.ImageIndex = imageType;
			TagInfo.FileTypes fileType = file.ScriptName.Length == 0 ? TagInfo.FileTypes.NormalFile : TagInfo.FileTypes.ScriptFile;
			newNode.Tag = new TagInfo(file.Id, fileType);
			newNode.Cells[(int)CellTypes.Function].StyleNormal = treeFiles.Styles["functionLinkStyle"];
			newNode.Cells[(int)CellTypes.Function].StyleMouseOver = treeFiles.Styles["functionLinkHoverStyle"];
			newNode.Cells[(int)CellTypes.Function].Cursor = Cursors.Hand;

			if (file.StaticFileIterator != null)
			{
				newNode.Cells[(int)CellTypes.Iterator].Text = file.StaticFileIterator.FullName;
				newNode.Cells[(int)CellTypes.Iterator].StyleNormal = treeFiles.Styles["functionLinkStyle"];
				newNode.Cells[(int)CellTypes.Iterator].StyleMouseOver = treeFiles.Styles["functionLinkHoverStyle"];
				newNode.Cells[(int)CellTypes.Iterator].Cursor = Cursors.Hand;
			}

			if (string.IsNullOrEmpty(file.StaticFileSkipFunctionName) == false)
			{
				newNode.Cells[(int)CellTypes.SkipFunction].Text = file.StaticFileSkipFunctionName;
				newNode.Cells[(int)CellTypes.SkipFunction].StyleNormal = treeFiles.Styles["functionLinkStyle"];
				newNode.Cells[(int)CellTypes.SkipFunction].StyleMouseOver = treeFiles.Styles["functionLinkHoverStyle"];
				newNode.Cells[(int)CellTypes.SkipFunction].Cursor = Cursors.Hand;
			}

			parentNode.Nodes.Add(newNode);
			return newNode;
		}

		private void deleteNodeToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			DeleteFocusedNodes();
		}

		private void DeleteFocusedNodes()
		{
			Controller.ShadeMainForm();
			treeFiles.BeginUpdate();
			try
			{
				if (MessageBox.Show(this, "Delete selected files?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				{
					List<int> selectedIndexes = new List<int>();
					Node parentNode = null;

					foreach (Node node in treeFiles.SelectedNodes)
					{
						selectedIndexes.Add(node.Index);

						if (parentNode == null)
						{
							parentNode = node.Parent;
						}
					}
					for (int i = selectedIndexes.Count - 1; i >= 0; i--)
					{
						TagInfo ti = (TagInfo)parentNode.Nodes[selectedIndexes[i]].Tag;

						if (ti.FileType == TagInfo.FileTypes.Folder)
						{
							OutputFolder folder = Project.Instance.FindFolder(ti.Id);

							if (folder != null)
							{
								Project.Instance.RemoveFolder(folder);
								parentNode.Nodes.RemoveAt(selectedIndexes[i]);
							}
						}
						else // Is a file
						{
							OutputFile file = Project.Instance.FindFile(ti.Id);
							OutputFolder folder = Project.Instance.FindFolder(((TagInfo)parentNode.Tag).Id);

							if (folder != null && file != null)
							{
								folder.RemoveFile(file);
								parentNode.Nodes.RemoveAt(selectedIndexes[i]);
							}
						}
					}
				}
			}
			finally
			{
				treeFiles.EndUpdate();
				Controller.UnshadeMainForm();
			}
		}

		public void AddNewFolder()
		{
			try
			{
				if (treeFiles.SelectedNodes.Count == 0)
				{
					MessageBox.Show(this, "Select a folder to add this file to first.", "No Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				else if (treeFiles.SelectedNodes.Count > 1)
				{
					throw new Exception("Only one node should be selected.");
				}
				Cursor = Cursors.WaitCursor;
				Refresh();
				Node selectedNode = treeFiles.SelectedNodes[0];
				TagInfo ti = (TagInfo)selectedNode.Tag;

				if (ti.FileType != TagInfo.FileTypes.Folder)
				{
					MessageBox.Show(this, "A folder cannot be added as a child of a file. Select a parent folder", "Invalid Node Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				Wizards.frmOutputFileWizard.FileType = Wizards.frmOutputFileWizard.FileTypes.Folder;
				Wizards.frmOutputFileWizard.IterationType = null;
				Wizards.frmOutputFileWizard form = new Wizards.frmOutputFileWizard();

				if (form.ShowDialog() == DialogResult.OK)
				{
					var fileName = Wizards.frmOutputFileWizard.FileName;
					var iteratorType = Wizards.frmOutputFileWizard.IterationType;

					OutputFolder newFolder = new OutputFolder(fileName, Guid.NewGuid().ToString());

					CreateNewFolderAndAddToTree(selectedNode, newFolder, iteratorType);
				}
			}
			finally
			{
				Controller.Instance.MainForm.Activate();
				Cursor = Cursors.Default;
			}
		}

		private void CreateNewFolderAndAddToTree(Node selectedNode, OutputFolder newFolder, Type iteratorType)
		{
			newFolder.IteratorType = iteratorType;
			string id = ((TagInfo)selectedNode.Tag).Id;
			OutputFolder parentFolder = Project.Instance.FindFolder(id);

			if (parentFolder != null)
			{
				parentFolder.Folders.Add(newFolder);
				Project.Instance.IsDirty = true;
				Node newFolderNode = AddFolderNode(selectedNode, newFolder);
				selectedNode.Expanded = true;
				treeFiles.SelectedNode = newFolderNode;
			}
			else
			{
				throw new Exception("No parent folder found.");
			}
		}

		public void AddNewStaticFiles()
		{
			if (treeFiles.SelectedNodes.Count == 0)
			{
				MessageBox.Show(this, "Select a folder to add this file to first.", "No Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (treeFiles.SelectedNodes.Count > 1)
			{
				throw new Exception("Only one node can be selected.");
			}

			Node selectedNode = treeFiles.SelectedNodes[0];
			TagInfo ti = (TagInfo)selectedNode.Tag;

			if (ti.FileType != TagInfo.FileTypes.Folder)
			{
				MessageBox.Show(this, "A file cannot be added as a child of a file. Select a parent folder", "No Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Cursor = Cursors.WaitCursor;

			try
			{
				Refresh();

				OpenFileDialog form = new OpenFileDialog();
				form.CheckFileExists = true;
				form.CheckPathExists = true;
				form.Multiselect = true;

				if (form.ShowDialog() == DialogResult.OK)
				{
					string id = ((TagInfo)selectedNode.Tag).Id;
					OutputFolder folder = Project.Instance.FindFolder(id);

					if (folder != null)
					{
						foreach (var filename in form.FileNames)
						{
							CreateNewStaticFileAndAddItToTheTree(selectedNode, folder, filename);
						}
						selectedNode.Expanded = true;
					}
					else
					{
						MessageBox.Show(this, "No matching folder found.", "No matching folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					Project.Instance.IsDirty = true;
				}
			}
			finally
			{
				Controller.Instance.MainForm.Activate();
				Cursor = Cursors.Default;
			}
		}

		private void CreateNewStaticFileAndAddItToTheTree(Node selectedNode, OutputFolder folder, string filename)
		{
			Project.Instance.AddIncludedFile(new IncludedFile(filename));
			OutputFile file = new OutputFile(Path.GetFileName(filename), OutputFileTypes.File, Path.GetFileName(filename), Guid.NewGuid().ToString());
			folder.AddFile(file);

			AddFileNode(selectedNode, file);
		}


		private void newStaticFilesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddNewStaticFiles();
		}

		private void mnuItemAddFolder_Click(object sender, EventArgs e)
		{
			AddNewFolder();
		}

		private void mnuItemEdit_Click(object sender, EventArgs e)
		{
			if (treeFiles.SelectedNodes.Count == 0)
			{
				MessageBox.Show(this, "Select a folder first.", "No Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (treeFiles.SelectedNodes.Count > 1)
			{
				throw new Exception("Only one node can be selected.");
			}
			try
			{
				Node selectedNode = treeFiles.SelectedNodes[0];
				TagInfo ti = (TagInfo)selectedNode.Tag;

				if (ti.FileType == TagInfo.FileTypes.Folder)
				{
					string id = ((TagInfo)selectedNode.Tag).Id;
					OutputFolder folder = Project.Instance.FindFolder(id);
					Wizards.frmOutputFileWizard.FileType = Wizards.frmOutputFileWizard.FileTypes.Folder;
					Wizards.frmOutputFileWizard form = new Wizards.frmOutputFileWizard();
					Wizards.frmOutputFileWizard.FileName = selectedNode.Text;
					Wizards.frmOutputFileWizard.IterationType = folder.IteratorType;

					if (form.ShowDialog() == DialogResult.OK)
					{
						if (folder != null)
						{
							folder.Name = Wizards.frmOutputFileWizard.FileName;
							folder.IteratorType = Wizards.frmOutputFileWizard.IterationType;
						}
						Project.Instance.IsDirty = true;
						selectedNode.Text = folder.Name;
						selectedNode.Cells[(int)CellTypes.Iterator].Text = folder.IteratorType == null ? "" : folder.IteratorType.FullName;
					}
				}
				else //if (ti.FileType == TagInfo.FileTypes.ScriptFile)
				{
					string id = ((TagInfo)selectedNode.Tag).Id;
					OutputFile file = Project.Instance.FindFile(id);

					Wizards.frmOutputFileWizard.StaticSkipFunction = Wizards.frmOutputFileWizard.SkipFunctionChoice.DontUse;

					if (ti.FileType == TagInfo.FileTypes.ScriptFile)
					{
						Wizards.frmOutputFileWizard.FileType = Wizards.frmOutputFileWizard.FileTypes.Script;
						Wizards.frmOutputFileWizard.FunctionName = selectedNode.Cells[(int)CellTypes.Function].Text;
						Wizards.frmOutputFileWizard.StaticFileName = "";

						if (!string.IsNullOrEmpty(file.IteratorTypes))
						{
							Wizards.frmOutputFileWizard.IterationType = Project.Instance.GetTypeFromReferencedAssemblies(file.IteratorTypes, false);
						}
						else
						{
							Wizards.frmOutputFileWizard.IterationType = null;
						}
					}
					else
					{
						Wizards.frmOutputFileWizard.FileType = Wizards.frmOutputFileWizard.FileTypes.Static;
						Wizards.frmOutputFileWizard.StaticFileName = file.StaticFileName;
						Wizards.frmOutputFileWizard.FunctionName = file.StaticFileSkipFunctionName;
						Wizards.frmOutputFileWizard.IterationType = file.StaticFileIterator;
					}
					Wizards.frmOutputFileWizard form = new Wizards.frmOutputFileWizard();
					Wizards.frmOutputFileWizard.FileName = selectedNode.Text;
					FunctionInfo func = Project.Instance.FindFunctionSingle(Wizards.frmOutputFileWizard.FunctionName);

					if (func != null && func.Parameters.Count > 0)
					{
						Wizards.frmOutputFileWizard.IterationType = func.Parameters[0].DataType;
					}
					bool showFunctions = false;

					if (form.ShowDialog() == DialogResult.OK)
					{
						if (Wizards.frmOutputFileWizard.ShowNewFunctionWizardOnClose &&
							Wizards.frmOutputFileWizard.StaticSkipFunction == Wizards.frmOutputFileWizard.SkipFunctionChoice.DontUse)
						{
							Controller.Instance.MainForm.Refresh();
							FunctionInfo newFunc = Controller.Instance.MainForm.UcFunctions.NewFunction(Wizards.frmOutputFileWizard.IterationType);

							if (newFunc != null)
							{
								Wizards.frmOutputFileWizard.FunctionName = newFunc.Name;
								showFunctions = true;
							}
						}
						else if (Wizards.frmOutputFileWizard.StaticSkipFunction == Wizards.frmOutputFileWizard.SkipFunctionChoice.CreateNew)
						{
							FunctionInfo newFunction = new FunctionInfo(
															NamingHelper.CleanNameCSharp(Wizards.frmOutputFileWizard.StaticFileName) + "_SkipFile",
																typeof(bool), "return false;", false, SyntaxEditorHelper.ScriptLanguageTypes.CSharp,
																"Returns true if the static file should be skipped and not generated", "plain text", "Skip Static Files");

							Project.Instance.AddFunction(newFunction);
							Wizards.frmFunctionWizard functionForm = new Wizards.frmFunctionWizard(newFunction, true);

							if (functionForm.ShowDialog(ParentForm) == DialogResult.Cancel)
							{
								Project.Instance.DeleteFunction(newFunction);
								//OwnerTabStripPage.TabStrip.Pages.Remove(OwnerTabStripPage);
							}
							file.StaticFileSkipFunctionName = newFunction.Name;
						}
						else if (Wizards.frmOutputFileWizard.StaticSkipFunction == Wizards.frmOutputFileWizard.SkipFunctionChoice.UseExisting)
						{
							file.StaticFileSkipFunctionName = Wizards.frmOutputFileWizard.FunctionName;
						}

						file.Name = Wizards.frmOutputFileWizard.FileName;
						file.ScriptName = Wizards.frmOutputFileWizard.FunctionName;
						Project.Instance.IsDirty = true;
						selectedNode.Text = file.Name;

						switch (Wizards.frmOutputFileWizard.FileType)
						{
							case Wizards.frmOutputFileWizard.FileTypes.Script:
								file.FileType = OutputFileTypes.Script;
								ti.FileType = TagInfo.FileTypes.ScriptFile;
								selectedNode.Cells[(int)CellTypes.Function].Text = file.ScriptName;
								selectedNode.Cells[(int)CellTypes.Iterator].Text = file.IteratorTypes;
								break;
							case Wizards.frmOutputFileWizard.FileTypes.Static:
								file.FileType = OutputFileTypes.File;
								file.StaticFileIterator = Wizards.frmOutputFileWizard.IterationType;
								ti.FileType = TagInfo.FileTypes.NormalFile;
								selectedNode.Cells[(int)CellTypes.Function].Text = "[File] " + Wizards.frmOutputFileWizard.StaticFileName;

								if (file.StaticFileIterator != null)
								{
									selectedNode.Cells[(int)CellTypes.Iterator].Text = file.StaticFileIterator.FullName;
									selectedNode.Cells[(int)CellTypes.Iterator].StyleNormal = treeFiles.Styles["functionLinkStyle"];
									selectedNode.Cells[(int)CellTypes.Iterator].StyleMouseOver = treeFiles.Styles["functionLinkHoverStyle"];
									selectedNode.Cells[(int)CellTypes.Iterator].Cursor = Cursors.Hand;
								}
								if (string.IsNullOrEmpty(file.StaticFileSkipFunctionName) == false)
								{
									selectedNode.Cells[(int)CellTypes.SkipFunction].Text = file.StaticFileSkipFunctionName;
									selectedNode.Cells[(int)CellTypes.SkipFunction].StyleNormal = treeFiles.Styles["functionLinkStyle"];
									selectedNode.Cells[(int)CellTypes.SkipFunction].StyleMouseOver = treeFiles.Styles["functionLinkHoverStyle"];
									selectedNode.Cells[(int)CellTypes.SkipFunction].Cursor = Cursors.Hand;
								}
								break;
							default:
								throw new NotImplementedException("Filetype not handled yet: " + Wizards.frmOutputFileWizard.FileType.ToString());
						}
						selectedNode.Tag = ti;

					}
					if (showFunctions)
					{
						Controller.Instance.MainForm.HidePanelControls(Controller.Instance.MainForm.UcFunctions);
					}
				}
			}
			finally
			{
				Controller.Instance.MainForm.Activate();
			}
		}

		public void AddNewFile()
		{
			if (treeFiles.SelectedNodes.Count == 0)
			{
				MessageBox.Show(this, "Select a folder to add this file to first.", "No Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (treeFiles.SelectedNodes.Count > 1)
			{
				throw new Exception("Only one node can be selected.");
			}

			Node selectedNode = treeFiles.SelectedNodes[0];
			TagInfo ti = (TagInfo)selectedNode.Tag;

			if (ti.FileType != TagInfo.FileTypes.Folder)
			{
				MessageBox.Show(this, "A file cannot be added as a child of a file. Select a parent folder", "No Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Cursor = Cursors.WaitCursor;

			try
			{
				Refresh();
				Wizards.frmOutputFileWizard.IterationType = null;
				Wizards.frmOutputFileWizard.FileType = Wizards.frmOutputFileWizard.FileTypes.Script;
				Wizards.frmOutputFileWizard.FileName = "";
				Wizards.frmOutputFileWizard.StaticFileName = "";
				Wizards.frmOutputFileWizard.FunctionName = "";
				Wizards.frmOutputFileWizard form = new Wizards.frmOutputFileWizard();
				bool showFunctions = false;

				if (form.ShowDialog() == DialogResult.OK)
				{
					var createNewFunction = Wizards.frmOutputFileWizard.ShowNewFunctionWizardOnClose &&
							(Wizards.frmOutputFileWizard.StaticSkipFunction == Wizards.frmOutputFileWizard.SkipFunctionChoice.DontUse
							|| Wizards.frmOutputFileWizard.FileType == Wizards.frmOutputFileWizard.FileTypes.Script);
					if (createNewFunction)
					{
						Controller.Instance.MainForm.Refresh();
						FunctionInfo newFunc = Controller.Instance.MainForm.UcFunctions.NewFunction(Wizards.frmOutputFileWizard.IterationType);

						if (newFunc != null)
						{
							Wizards.frmOutputFileWizard.FunctionName = newFunc.Name;
							showFunctions = true;
						}
					}
					else if (Wizards.frmOutputFileWizard.StaticSkipFunction == Wizards.frmOutputFileWizard.SkipFunctionChoice.CreateNew)
					{
						FunctionInfo newFunction = new FunctionInfo(
														NamingHelper.CleanNameCSharp(Wizards.frmOutputFileWizard.StaticFileName) + "_SkipFile",
															typeof(bool), "return false;", false, SyntaxEditorHelper.ScriptLanguageTypes.CSharp,
															"Returns true if the static file should be skipped and not generated", "plain text", "Skip Static Files");

						Project.Instance.AddFunction(newFunction);
						Wizards.frmFunctionWizard functionForm = new Wizards.frmFunctionWizard(newFunction, true);

						if (functionForm.ShowDialog(ParentForm) == DialogResult.Cancel)
						{
							Project.Instance.DeleteFunction(newFunction);
							//OwnerTabStripPage.TabStrip.Pages.Remove(OwnerTabStripPage);
						}
					}

					string id = ((TagInfo)selectedNode.Tag).Id;

					OutputFolder folder = Project.Instance.FindFolder(id);

					if (folder != null)
					{
						OutputFile file;

						if (Wizards.frmOutputFileWizard.FileType == Wizards.frmOutputFileWizard.FileTypes.Static)
						{
							file = new OutputFile(Wizards.frmOutputFileWizard.FileName, OutputFileTypes.File, "", Guid.NewGuid().ToString());
							file.StaticFileName = Wizards.frmOutputFileWizard.StaticFileName;
							file.StaticFileIterator = Wizards.frmOutputFileWizard.IterationType;
							file.StaticFileSkipFunctionName = Wizards.frmOutputFileWizard.StaticSkipFunctionName;
						}
						else if (Wizards.frmOutputFileWizard.FileType == Wizards.frmOutputFileWizard.FileTypes.Script)
						{
							file = new OutputFile(Wizards.frmOutputFileWizard.FileName, OutputFileTypes.Script, Wizards.frmOutputFileWizard.FunctionName, Guid.NewGuid().ToString());
							file.IteratorFunction = Project.Instance.FindFunctionSingle(Wizards.frmOutputFileWizard.FunctionName);
						}
						else
						{
							throw new NotImplementedException("Not catered for yet.");
						}
						folder.AddFile(file);
						Node newFileNode = AddFileNode(selectedNode, file);
						selectedNode.Expanded = true;
						treeFiles.SelectedNode = newFileNode;
					}
					else
					{
						MessageBox.Show(this, "No matching folder found.", "No matching folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					Project.Instance.IsDirty = true;
				}
				if (showFunctions)
				{
					Controller.Instance.MainForm.HidePanelControls(Controller.Instance.MainForm.UcFunctions);
				}
			}
			finally
			{
				Controller.Instance.MainForm.Activate();
				Cursor = Cursors.Default;
			}
		}

		private void newAddFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddNewFile();
		}

		private void treeFiles_KeyDown(object sender, KeyEventArgs e)
		{
			if (treeFiles.SelectedNodes.Count > 0 && e.KeyCode == Keys.Delete)
			{
				DeleteFocusedNodes();
			}
		}

		private void HideAllMenuItems(ContextMenuStrip menu)
		{
			foreach (ToolStripItem item in menu.Items)
			{
				item.Visible = false;
			}
		}

		private void mnuTreeNode_Opening(object sender, CancelEventArgs e)
		{
			if (treeFiles.SelectedNodes.Count == 0)
			{
				e.Cancel = true;
				return;
			}
			if (treeFiles.SelectedNodes.Count > 1)
			{
				HideAllMenuItems(mnuTreeNode);
				mnuItemDelete.Visible = true;
				return;
			}
			Node node = treeFiles.SelectedNodes[0];

			HideAllMenuItems(mnuTreeNode);

			if (node == null)
			{
				return;
			}
			switch (((TagInfo)node.Tag).FileType)
			{
				case TagInfo.FileTypes.Folder:
					mnuItemAddFolder.Visible = true;
					mnuTreeNodeSeparator2.Visible = true;
					mnuItemAddFile.Visible = true;
					mnuItemAddStaticFiles.Visible = true;

					if (node.Text != "ROOT")
					{
						mnuItemEdit.Visible = true;
						mnuItemDelete.Visible = true;
					}
					break;
				case TagInfo.FileTypes.NormalFile:
					mnuItemEdit.Visible = true;
					mnuItemDelete.Visible = true;
					break;
				case TagInfo.FileTypes.ScriptFile:
					mnuTreeNodeSeparator2.Visible = true;
					mnuItemEdit.Visible = true;
					mnuItemDelete.Visible = true;
					break;
				default:
					throw new NotImplementedException("Not coded yet.");
			}
		}

		private void treeFiles_AfterNodeDrop(object sender, TreeDragDropEventArgs e)
		{
			TagInfo ti = (TagInfo)e.Node.Tag;
			OutputFolder oldParentFolder = Project.Instance.FindFolder(((TagInfo)e.OldParentNode.Tag).Id);
			OutputFolder newParentFolder = Project.Instance.FindFolder(((TagInfo)e.NewParentNode.Tag).Id);
			Node prevNode = e.Node.PrevNode;

			if (prevNode != null && ((TagInfo)prevNode.Tag).FileType == TagInfo.FileTypes.Folder)
			{
				// The user dropped just below a folder, so meant to add to that folder, so we must make the dragged node a child node, not a sibling.
				e.Node.PrevNode.Nodes.Add(e.Node);
				newParentFolder = Project.Instance.FindFolder(((TagInfo)prevNode.Tag).Id);
			}
			switch (ti.FileType)
			{
				case TagInfo.FileTypes.Folder:
					OutputFolder folder = Project.Instance.FindFolder(((TagInfo)e.Node.Tag).Id);
					oldParentFolder.Folders.Remove(folder);
					newParentFolder.Folders.Add(folder);
					break;
				case TagInfo.FileTypes.NormalFile:
				case TagInfo.FileTypes.ScriptFile:
					OutputFile normalFile = Project.Instance.FindFile(((TagInfo)e.Node.Tag).Id);
					oldParentFolder.RemoveFile(normalFile);
					newParentFolder.Files.Add(normalFile);
					break;
				default:
					throw new NotImplementedException("Not coded yet: " + ti.FileType);
			}
			Project.Instance.IsDirty = true;
		}

		private void treeFiles_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Cell cell = treeFiles.GetCellAt(e.Location);

				if (cell != null &&
					cell.Parent.Cells.IndexOf(cell) == (int)CellTypes.Function)
				{
					Cursor = Cursors.WaitCursor;
					TagInfo ti = (TagInfo)cell.Parent.Tag;

					if (ti.FileType == TagInfo.FileTypes.ScriptFile)
					{
						OutputFile file = Project.Instance.FindFile(ti.Id);

						if (file == null)
						{
							Cursor = Cursors.Default;
							MessageBox.Show(this, "The function is missing. Click 'Edit' to select the correct function.", "Missing Function", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						FunctionInfo function = Project.Instance.FindFunctionSingle(file.ScriptName);
						Controller.Instance.MainForm.ShowFunction(function, this);
						Cursor = Cursors.Default;
						return;
					}
				}
				else if (cell != null &&
					cell.Parent.Cells.IndexOf(cell) == (int)CellTypes.SkipFunction)
				{
					Cursor = Cursors.WaitCursor;
					TagInfo ti = (TagInfo)cell.Parent.Tag;
					if (ti.FileType == TagInfo.FileTypes.NormalFile)
					{
						OutputFile file = Project.Instance.FindFile(ti.Id);

						if (file == null)
						{
							Cursor = Cursors.Default;
							MessageBox.Show(this, "The function is missing. Click 'Edit' to select the correct function.", "Missing Function", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						if (string.IsNullOrEmpty(file.StaticFileSkipFunctionName)) return;

						FunctionInfo function = Project.Instance.FindFunctionSingle(file.StaticFileSkipFunctionName);
						Controller.Instance.MainForm.ShowFunction(function, this);
					}
					Cursor = Cursors.Default;
				}
			}
		}

		private void treeFiles_DragDrop(object sender, DragEventArgs e)
		{
			// If files are being dropped handle the drop
			if (e.Data != null && e.Data.GetDataPresent("FileDrop"))
			{
				// Disable control layout updates while adding new nodes
				treeFiles.BeginUpdate();

				// Default parent is root node
				Node parentNode = treeFiles.Nodes[0];

				// Find out which node is under the cursor
				Point clientPoint = treeFiles.PointToClient(new Point(e.X, e.Y));
				Node node = treeFiles.GetNodeAt(clientPoint.Y);
				// If there is node under cursor add files to it instead
				if (node != null) parentNode = node;

				string id = ((TagInfo)parentNode.Tag).Id;
				OutputFolder folder = Project.Instance.FindFolder(id);

				string[] fileNames = (string[])e.Data.GetData("FileDrop", true);
				foreach (var filename in fileNames)
				{
					CreateNewStaticFileAndAddItToTheTree(parentNode, folder, filename);
				}

				// Resume tree control layout
				treeFiles.EndUpdate();

				// Enable internal drag & drop
				treeFiles.DragDropEnabled = true;
			}
		}

		private void treeFiles_DragEnter(object sender, DragEventArgs e)
		{
			// Disable internal drag & drop if files are being dragged onto the control
			if (e.Data != null && e.Data.GetDataPresent("FileDrop"))
				treeFiles.DragDropEnabled = false;
		}

		private void treeFiles_DragLeave(object sender, EventArgs e)
		{
			// Enable internal drag & drop if drop is leaving
			treeFiles.DragDropEnabled = true;
		}

		private void treeFiles_DragOver(object sender, DragEventArgs e)
		{
			// If files are being dragged handle it
			if (e.Data != null && e.Data.GetDataPresent("FileDrop"))
			{
				e.Effect = DragDropEffects.Link;

				// Get node that is under the mouse
				Point clientPoint = treeFiles.PointToClient(new Point(e.X, e.Y));
				Node node = treeFiles.GetNodeAt(clientPoint.Y);
				// Expand it if it is not expanded
				if (node != null && !node.Expanded) node.Expanded = true;
				// Select node under mouse
				treeFiles.SelectedNode = node;
			}
		}
	}
}
