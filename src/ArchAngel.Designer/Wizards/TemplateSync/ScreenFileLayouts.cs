using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ArchAngel.Designer.DesignerProject;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;

namespace ArchAngel.Designer.Wizards.TemplateSync
{
	public partial class ScreenFileLayouts : Interfaces.Controls.ContentItems.ContentItem
	{
		#region Inner Classes

	    private class FolderContainer
		{
			public OutputFolder MyFolder;
			public OutputFolder TheirFolder;
			public string Name;

			public FolderContainer(OutputFolder myFolder, OutputFolder theirFolder, string name)
			{
				MyFolder = myFolder;
				TheirFolder = theirFolder;
				Name = name;
			}

			public override string ToString()
			{
				return Name;
			}
		}

		private class FileContainer
		{
			public OutputFile MyFile;
			public OutputFile TheirFile;
			public string Name;

			public FileContainer(OutputFile myFile, OutputFile theirFile, string name)
			{
				MyFile = myFile;
				TheirFile = theirFile;
				Name = name;
			}

			public override string ToString()
			{
				return Name;
			}
		}
		#endregion

		#region Enums
		enum ActionTypes
		{
			Import,
			Remove
		}
		enum TreeNodeImages
		{
			Unchecked = 0,
			Checked = 1
		}
		#endregion

		private enum Images
		{
			IMG_CLOSED_FOLDER = 0,
			IMG_OPEN_FOLDER = 1,
			IMG_TEMPLATE_SCRIPT = 4,
			IMG_NORMAL_SCRIPT = 2,
			IMG_FILE = 5,
			IMG_ROOT = 3,
			IMG_DOT = 6
		}

		private readonly Bitmap GreenArrow = new Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Designer.Resources.green_arrow.png"));
		private readonly Bitmap BlueArrow = new Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Designer.Resources.blue_arrow.png"));
		private readonly Bitmap RemoveImage = new Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Designer.Resources.error_16.png"));
		private readonly DevExpress.XtraEditors.Repository.RepositoryItem EmptyRepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItem();


		public ScreenFileLayouts()
		{
			InitializeComponent();
			PageHeader = "File Layouts";
			PageDescription = "Select which File Layouts to synchronise.";
			HasNext = true;
			HasPrev = true;
		}

		public void Populate()
		{
			int topNodeIndex = tgv.TopVisibleNodeIndex;
			tgv.BeginUnboundLoad();
			tgv.Nodes.Clear();
			tgv.Columns[1].Caption = System.IO.Path.GetFileName(frmTemplateSyncWizard.TheirProject.ProjectFileName) + " (external project)";
			tgv.Columns[3].Caption = System.IO.Path.GetFileName(frmTemplateSyncWizard.MyProject.ProjectFileName) + " (current project)";

			// Create the treeview
			foreach (string outputName in Project.Instance.OutputNames)
			{
				ActiproSoftware.UIStudio.TabStrip.TabStripPage page = new ActiproSoftware.UIStudio.TabStrip.TabStripPage(outputName, outputName, (int)Images.IMG_ROOT);
				page.TabSelectedBackgroundFill = new ActiproSoftware.Drawing.TwoColorLinearGradient(Color.White, System.Drawing.Color.Khaki, 0F, ActiproSoftware.Drawing.TwoColorLinearGradientStyle.Normal, ActiproSoftware.Drawing.BackgroundFillRotationType.None);
				page.EnsureTabVisible();
			}
			if (Project.Instance.OutputNames.Count == 0)
			{
				ActiproSoftware.UIStudio.TabStrip.TabStripPage page = new ActiproSoftware.UIStudio.TabStrip.TabStripPage("Default", "Default", (int)Images.IMG_ROOT);
				page.TabSelectedBackgroundFill = new ActiproSoftware.Drawing.TwoColorLinearGradient(Color.White, System.Drawing.Color.Khaki, 0F, ActiproSoftware.Drawing.TwoColorLinearGradientStyle.Normal, ActiproSoftware.Drawing.BackgroundFillRotationType.None);
				page.EnsureTabVisible();
			}
			TreeListNode rootNode2 = null;
			rootNode2 = tgv.AppendNode(new object[] { "ROOT", "", "" }, null);
			rootNode2.ImageIndex = rootNode2.SelectImageIndex = (int)Images.IMG_ROOT;

			OutputFolder rootFolderTheirs = frmTemplateSyncWizard.TheirProject.RootOutput;
			OutputFolder rootFolderMine = frmTemplateSyncWizard.MyProject.RootOutput;
			ProcessFolderTGV(rootFolderTheirs, rootFolderMine, rootNode2);
			tgv.ExpandAll();
			tgv.TopVisibleNodeIndex = topNodeIndex;
			tgv.EndUnboundLoad();
		}

		public override void OnDisplaying()
		{
			Populate();
		}

		private void ProcessFolderTGV(OutputFolder folderTheirs, OutputFolder folderMine, TreeListNode treenode)
		{
			AddFolderNode(treenode, folderTheirs, folderMine);
		}

		private bool FilesAreDifferent(OutputFile file1, OutputFile file2)
		{
			bool differenceExists = false;

			if (file1 != null && file2 != null)
			{
				//if (file1.FileType != file2.FileType) { differenceExists = true; }
				if (file1.Name != file2.Name) { differenceExists = true; }
				//if (file1.OutputNames != file2.OutputNames) { differenceExists = true; }
				//if (file1.ScriptName != file2.ScriptName) { differenceExists = true; }
				//if (file1.StaticFileIterator != file2.StaticFileIterator) { differenceExists = true; }
				//if (file1.StaticFileName != file2.StaticFileName) { differenceExists = true; }
			}
			return differenceExists;
		}

		private bool FoldersAreDifferent(OutputFolder folder1, OutputFolder folder2)
		{
			bool differenceExists = false;

			if (folder1 != null && folder2 != null)
			{
				//if (folder1.IteratorType != folder2.IteratorType) { differenceExists = true; }
				if (folder1.Name != folder2.Name) { differenceExists = true; }
				//if (folder1.OutputNames != folder2.OutputNames) { differenceExists = true; }
			}
			return differenceExists;
		}

		private TreeListNode AddFolderNode(TreeListNode treenode, OutputFolder folderTheirs, OutputFolder folderMine)
		{
			TreeListNode folderNode = null;

			if (folderMine != null && folderTheirs != null && folderMine.Name == "root" && folderTheirs.Name == "root" && treenode.ParentNode == null)
			{
				folderNode = treenode;
			}
			bool differenceExists = false;

			if (folderTheirs != null && folderMine != null)
			{
				FolderContainer folderContainer = new FolderContainer(folderMine, folderTheirs, folderTheirs.Name);

				if (folderNode == null)
				{
					folderNode = AddNodeBoth(Images.IMG_OPEN_FOLDER, "", folderTheirs.Name, folderMine.Name, treenode, folderContainer, false);
				}
				if (folderTheirs.IteratorType != folderMine.IteratorType)
				{
					differenceExists = true;
					AddNodeBoth(Images.IMG_DOT, "Iterator Type", folderTheirs.IteratorType, folderMine.IteratorType, folderNode, null, true);
				}
				if (folderTheirs.Name != folderMine.Name)
				{
					differenceExists = true;
					AddNodeBoth(Images.IMG_DOT, "Name", folderTheirs.Name, folderMine.Name, folderNode, null, true);
				}
				#region Output Names
				TreeListNode outputNamesNode = AddNodeBoth(Images.IMG_DOT, "Output Names", "", "", folderNode, null, false);
				
				if (outputNamesNode.Nodes.Count > 0)
				{
					differenceExists = true;
				}
				else
				{
					folderNode.Nodes.Remove(outputNamesNode);
				}
				#endregion
				// Process sub-folders and files
				foreach (OutputFolder subFolderMine in folderMine.Folders)
				{
					bool found = false;

					foreach (OutputFolder subFolderTheirs in folderTheirs.Folders)
					{
						if (!FoldersAreDifferent(subFolderMine, subFolderTheirs))
						{
							found = true;
							AddFolderNode(folderNode, subFolderTheirs, subFolderMine);
							break;
						}
					}
					if (!found)
					{
						AddFolderNode(folderNode, null, subFolderMine);
					}
				}
				foreach (OutputFolder subFolderTheirs in folderTheirs.Folders)
				{
					bool found = false;

					foreach (OutputFolder subFolderMine in folderMine.Folders)
					{
						if (!FoldersAreDifferent(subFolderMine, subFolderTheirs))
						{
							found = true;
							break;
						}
					}
					if (!found)
					{
						AddFolderNode(folderNode, subFolderTheirs, null);
					}
				}
				foreach (OutputFile fileTheirs in folderTheirs.Files)
				{
					bool found = false;

					foreach (OutputFile fileMine in folderMine.Files)
					{
						if (!FilesAreDifferent(fileMine, fileTheirs))
						{
							found = true;
							AddFileNode(folderNode, fileTheirs, fileMine);
							break;
						}
					}
					if (!found)
					{
						AddFileNode(folderNode, fileTheirs, null);
					}
				}
				foreach (OutputFile fileMine in folderMine.Files)
				{
					bool found = false;

					foreach (OutputFile fileTheirs in folderTheirs.Files)
					{
						if (!FilesAreDifferent(fileMine, fileTheirs))
						{
							found = true;
							break;
						}
					}
					if (!found)
					{
						AddFileNode(folderNode, null, fileMine);
					}
				}
				if (folderNode.Nodes.Count == 0 && !differenceExists)
				{
					if (folderNode.ParentNode != null)
					{
						folderNode.ParentNode.Nodes.Remove(folderNode);
					}
				}
				else if (!differenceExists)
				{
					folderNode.SetValue(0, folderMine.Name);
					folderNode.SetValue(1, "");
					folderNode.SetValue(3, "");
				}
			}
			else if (folderTheirs != null)
			{
				FolderContainer folderContainer = new FolderContainer(null, folderTheirs, folderTheirs.Name);

				if (folderNode == null)
				{
					folderNode = AddNodeTheirs(Images.IMG_CLOSED_FOLDER, "", folderTheirs.Name, "", treenode, folderContainer, true);
				}
			}
			else if (folderMine != null)
			{
				FolderContainer folderContainer = new FolderContainer(folderMine, null, folderMine.Name);

				if (folderNode == null)
				{
					folderNode = AddNodeMine(Images.IMG_CLOSED_FOLDER, "", "", folderMine.Name, treenode, folderContainer, true);
				}
			}
			return folderNode;
		}

		private TreeListNode AddFileNode(TreeListNode treenode, OutputFile fileTheirs, OutputFile fileMine)
		{
			TreeListNode fileNode = null;
			bool differenceExists = false;

			if (fileTheirs != null && fileMine != null)
			{
				FileContainer fileContainer = new FileContainer(fileMine, fileTheirs, fileTheirs.Name);
				fileNode = AddNodeBoth(Images.IMG_FILE, "", fileTheirs.Name, fileMine.Name, treenode, fileContainer, false);

				if (fileTheirs.FileType != fileMine.FileType)
				{
					differenceExists = true;
					AddNodeBoth(Images.IMG_DOT, "File Type", fileTheirs.FileType, fileMine.FileType, fileNode, null, true);
				}
				if (fileTheirs.Name != fileMine.Name)
				{
					differenceExists = true;
					AddNodeBoth(Images.IMG_DOT, "Name", fileTheirs.Name, fileMine.Name, fileNode, null, true);
				}
				if (fileTheirs.ScriptName != fileMine.ScriptName)
				{
					differenceExists = true;
					AddNodeBoth(Images.IMG_DOT, "Script Name", fileTheirs.ScriptName, fileMine.ScriptName, fileNode, null, true);
				}
				if (fileTheirs.StaticFileIterator != fileMine.StaticFileIterator)
				{
					differenceExists = true;
					AddNodeBoth(Images.IMG_DOT, "Static File Iterator", fileTheirs.StaticFileIterator, fileMine.StaticFileIterator, fileNode, null, true);
				}
				if (fileTheirs.StaticFileName != fileMine.StaticFileName)
				{
					differenceExists = true;
					AddNodeBoth(Images.IMG_DOT, "Static File Name", fileTheirs.StaticFileName, fileMine.StaticFileName, fileNode, null, true);
				}

				if (fileNode.Nodes.Count == 0 && !differenceExists)
				{
					fileNode.ParentNode.Nodes.Remove(fileNode);
				}
				else if (!FilesAreDifferent(fileTheirs, fileMine))
				{
					fileNode.SetValue(0, fileMine.Name);
					fileNode.SetValue(1, "");
					fileNode.SetValue(3, "");
				}
			}
			else if (fileTheirs != null)
			{
				FileContainer fileContainer = new FileContainer(null, fileTheirs, fileTheirs.Name);
				fileNode = AddNodeTheirs(Images.IMG_FILE, "", fileTheirs.Name, "", treenode, fileContainer, true);
			}
			else if (fileMine != null)
			{
				FileContainer fileContainer = new FileContainer(fileMine, null, fileMine.Name);
				fileNode = AddNodeMine(Images.IMG_FILE, "", "", fileMine.Name, treenode, fileContainer, true);
			}
			return fileNode;
		}

		private TreeListNode AddNodeBoth(Images fileType, string name, object theirValue, object myValue, TreeListNode parentNode, object tag)
		{
			return AddNodeBoth(fileType, name, theirValue, myValue, parentNode, tag, true);
		}

		private TreeListNode AddNodeBoth(Images fileType, string name, object theirValue, object myValue, TreeListNode parentNode, object tag, bool addActionText)
		{
			TreeListNode node;

			if (addActionText)
			{
				node = tgv.AppendNode(new object[] { name, theirValue, BlueArrow, myValue, "" }, parentNode);
			}
			else
			{
				node = tgv.AppendNode(new object[] { name, theirValue, "", myValue, "" }, parentNode);
			}
			node.Tag = tag;
			node.SelectImageIndex = node.ImageIndex = (int)fileType;
			return node;
		}

		private TreeListNode AddNodeTheirs(Images fileType, string name, object theirValue, object myValue, TreeListNode parentNode, object tag)
		{
			return AddNodeTheirs(fileType, name, theirValue, myValue, parentNode, tag, true);
		}

		private TreeListNode AddNodeTheirs(Images fileType, string name, object theirValue, object myValue, TreeListNode parentNode, object tag, bool addActionText)
		{
			TreeListNode node;

			if (addActionText)
			{
				node = tgv.AppendNode(new object[] { name, theirValue, GreenArrow, myValue, "" }, parentNode);
			}
			else
			{
				node = tgv.AppendNode(new object[] { name, theirValue, "", myValue, "" }, parentNode);
			}
			node.Tag = tag;
			node.StateImageIndex = (int)TreeNodeImages.Unchecked;
			node.SelectImageIndex = node.ImageIndex = (int)fileType;
			return node;
		}

		private TreeListNode AddNodeMine(Images fileType, string name, object theirValue, object myValue, TreeListNode parentNode, object tag)
		{
			return AddNodeMine(fileType, name, theirValue, myValue, parentNode, tag, true);
		}

		private TreeListNode AddNodeMine(Images fileType, string name, object theirValue, object myValue, TreeListNode parentNode, object tag, bool addActionText)
		{
			TreeListNode node;

			if (addActionText)
			{
				node = tgv.AppendNode(new object[] { name, theirValue, "", myValue, RemoveImage }, parentNode);
			}
			else
			{
				node = tgv.AppendNode(new object[] { name, theirValue, "", myValue, "" }, parentNode);
			}
			node.Tag = tag;
			node.StateImageIndex = (int)TreeNodeImages.Unchecked;
			node.SelectImageIndex = node.ImageIndex = (int)fileType;
			return node;
		}

		private void tgv_CustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
		{
			int columnIndex = e.Column.AbsoluteIndex;

			if (columnIndex == 2 || columnIndex == 4)
			{
				if (typeof(Bitmap).IsInstanceOfType(e.Node.GetValue(columnIndex)))
				{
					e.RepositoryItem = repositoryItemPictureEdit1;
				}
				else
				{
					e.RepositoryItem = EmptyRepositoryItem;
				}
			}
		}

		private void tgv_MouseDown(object sender, MouseEventArgs e)
		{
			DevExpress.XtraTreeList.TreeListHitInfo hInfo = tgv.CalcHitInfo(new Point(e.X, e.Y));
			TreeListNode node = hInfo.Node;

			if (node == null ||
				hInfo.Column == null ||
				!(hInfo.Column.AbsoluteIndex == 2 || hInfo.Column.AbsoluteIndex == 4))
			{
				return;
			}
			tgv.FocusedNode = node;
			string action;

			switch (hInfo.Column.AbsoluteIndex)
			{
				case 2:
					action = node.GetValue(2).ToString();

					if (!string.IsNullOrEmpty(action) && action == "System.Drawing.Bitmap")
					{
						object image = node.GetValue(2);

						if (image == GreenArrow)
						{
							action = "Import >";
						}
						else if (image == BlueArrow)
						{
							action = "Apply Change >";
						}
					}
					break;
				case 4:
					action = node.GetValue(4).ToString();

					if (!string.IsNullOrEmpty(action) && action == "System.Drawing.Bitmap")
					{
						action = "Remove";
					}
					break;
				default:
					return;
			}
			if (string.IsNullOrEmpty(action))
			{
				return;
			}
			TreeListNode ownerNode = node;

			while (ownerNode != null && ownerNode.Tag == null)
			{
				ownerNode = ownerNode.ParentNode;
			}
			if (ownerNode == null)
			{
				return;
			}
			switch (action)
			{
				case "Import >":
					if (node.Tag == null)
					{
						// Parent is a FileContainer
						string parentText = node.ParentNode.GetValue(0).ToString();

						FileContainer fc = (FileContainer)node.ParentNode.Tag;
						string propertyName = node.GetValue(1).ToString();

						switch (propertyName)
						{
							case "":
								break;
							default:
								throw new NotImplementedException("Not coded yet.");
						}
						
						Populate();
					}
					else if (node.Tag.GetType() == typeof(FolderContainer))
					{
						Stack<FolderContainer> folderStack = new Stack<FolderContainer>();
						TreeListNode parentNode = node;
						bool matchingAncestorFound = false;
						OutputFolder myTopFolder = null;

						while (parentNode.ParentNode != null)
						{
							FolderContainer fc = (FolderContainer)parentNode.Tag;

							if (fc.MyFolder != null)
							{
								matchingAncestorFound = true;
								myTopFolder = fc.MyFolder;
								break;
							}
							folderStack.Push(fc);
							parentNode = parentNode.ParentNode;
						}
						if (!matchingAncestorFound)
						{
							FolderContainer fc = folderStack.Pop();
							OutputFolder topLevelFolder = fc.TheirFolder;
							myTopFolder = new OutputFolder(topLevelFolder.Name, topLevelFolder.Id);
							myTopFolder.IteratorType = topLevelFolder.IteratorType;
							frmTemplateSyncWizard.MyProject.AddTopLevelFolder(myTopFolder);
						}
						while (folderStack.Count > 0)
						{
							FolderContainer fc = folderStack.Pop();
							OutputFolder theirFolder = fc.TheirFolder;
							OutputFolder newFolder = new OutputFolder(theirFolder.Name, theirFolder.Id);
							newFolder.IteratorType = theirFolder.IteratorType;
							myTopFolder.Folders.Add(newFolder);
						}
						Populate();
					}
					else // FileContainer
					{
						OutputFile theirFile = ((FileContainer)node.Tag).TheirFile;
						OutputFolder folder = null;
						bool isTopLevelNode = node.ParentNode.ParentNode == null;

						if (!isTopLevelNode)
						{
							folder = ((FolderContainer)node.ParentNode.Tag).MyFolder;
						}
						OutputFile newFile = new OutputFile(theirFile.Name, theirFile.FileType, theirFile.ScriptName, theirFile.Id);
						newFile.StaticFileIterator = theirFile.StaticFileIterator;
						newFile.StaticFileName = theirFile.StaticFileName;

						// Copy new function as well, if it doesn't already exist
						if (!string.IsNullOrEmpty(theirFile.ScriptName))
						{
							FunctionInfo myFunction = frmTemplateSyncWizard.MyProject.FindFunctionSingle(theirFile.ScriptName);

							if (myFunction == null)
							{
								FunctionInfo theirFunction = frmTemplateSyncWizard.TheirProject.FindFunctionSingle(theirFile.ScriptName);
								frmTemplateSyncWizard.MyProject.AddFunction(theirFunction);
                                newFile.IteratorFunction = frmTemplateSyncWizard.MyProject.FindFunctionSingle(theirFile.ScriptName);
							}
						}
						if (!isTopLevelNode)
						{
							folder.Files.Add(newFile);
						}
						else
						{
							frmTemplateSyncWizard.MyProject.AddTopLevelFile(newFile);
						}
						Populate();
					}
					break;
				case "Apply Change >":
					string propertyName2 = node.GetValue(0).ToString();

					if (node.ParentNode.Tag.GetType() == typeof(FolderContainer))
					{
						FolderContainer fc2 = (FolderContainer)node.ParentNode.Tag;

						switch (propertyName2)
						{
							case "Iterator Type":
								fc2.MyFolder.IteratorType = fc2.TheirFolder.IteratorType;
								break;
							default:
								throw new NotImplementedException("Not coded yet: " + propertyName2);
						}
					}
					else if (node.ParentNode.Tag.GetType() == typeof(FileContainer))
					{
						FileContainer fileContainer = (FileContainer)node.ParentNode.Tag;

						switch (propertyName2)
						{
							case "File Type":
								fileContainer.MyFile.FileType = fileContainer.TheirFile.FileType;
								break;
							case "Script Name":
								fileContainer.MyFile.ScriptName = fileContainer.TheirFile.ScriptName;
								break;
							case "Static File Iterator":
								fileContainer.MyFile.StaticFileIterator = fileContainer.TheirFile.StaticFileIterator;
								break;
							case "Static File Name":
								fileContainer.MyFile.StaticFileName = fileContainer.TheirFile.StaticFileName;
								break;
							default:
								throw new NotImplementedException("Not coded yet: " + propertyName2);
						}
					}
					else
					{
						throw new NotImplementedException("Not coded yet:" + node.ParentNode.Tag.GetType().FullName);
					}
					break;
				case "Remove":
					// Parent is a FileContainer
					string parentText2 = node.ParentNode.GetValue(0).ToString();

					
					if (node.Tag.GetType() == typeof(FolderContainer))
					{
						((FolderContainer)node.ParentNode.Tag).MyFolder.RemoveFolder(((FolderContainer)node.Tag).MyFolder);
					}
					else if (node.Tag.GetType() == typeof(FileContainer))
					{
						((FolderContainer)node.ParentNode.Tag).MyFolder.RemoveFile(((FileContainer)node.Tag).MyFile);
					}
					else
					{
						throw new NotImplementedException("Not coded yet: " + node.Tag.GetType().FullName);
					}
					
					Populate();
					break;
				default:
					throw new NotImplementedException("ActionType not handled yet: " + action);
			}
		}

		private void tgv_MouseMove(object sender, MouseEventArgs e)
		{
			DevExpress.XtraTreeList.TreeListHitInfo hInfo = tgv.CalcHitInfo(new Point(e.X, e.Y));

			if (hInfo.Node == null ||
				hInfo.Column == null)
			{
				if (Cursor != Cursors.Default)
				{
					Cursor = Cursors.Default;
				}
				return;
			}

			if (hInfo.Column.AbsoluteIndex == 2 &&
				hInfo.Node.GetValue(2).ToString() == "System.Drawing.Bitmap")
			{
				if (Cursor != Cursors.Hand)
				{
					Cursor = Cursors.Hand;
				}
			}
			else if (hInfo.Column.AbsoluteIndex == 4 && 
				hInfo.Node.GetValue(4).ToString() == "System.Drawing.Bitmap")
			{
				if (Cursor != Cursors.Hand)
				{
					Cursor = Cursors.Hand;
				}
			}
			else
			{
				if (Cursor != Cursors.Default)
				{
					Cursor = Cursors.Default;
				}
			}
		}


	}
}
