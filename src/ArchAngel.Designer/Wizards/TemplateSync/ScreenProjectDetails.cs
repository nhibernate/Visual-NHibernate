using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Common.DesignerProject;
using DevExpress.XtraTreeList.Nodes;

namespace ArchAngel.Designer.Wizards.TemplateSync
{
	public partial class ScreenProjectDetails : Interfaces.Controls.ContentItems.ContentItem
	{
		#region Enums
		private enum TreeNodeImages
		{
			Unchecked = 0,
			Checked = 1
		}
	    #endregion

		private int ProjectsHash;
		private readonly DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		private bool MustSkip;
	    readonly DevExpress.XtraEditors.Repository.RepositoryItem EmptyRepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItem();
	    readonly Bitmap GreenArrow = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Designer.Resources.green_arrow.png"));
	    readonly Bitmap BlueArrow = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Designer.Resources.blue_arrow.png"));
	    readonly Bitmap RemoveImage = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Designer.Resources.error_16.png"));

		public ScreenProjectDetails()
		{
			InitializeComponent();
			PageHeader = "Project Details";
			PageDescription = "Select which project details to synchronise.";
			HasNext = true;
			HasPrev = true;
			repositoryItemTextEdit.ReadOnly = true;
			repositoryItemTextEdit.AllowFocused = false;
			treeListReferencedFiles.RepositoryItems.Add(EmptyRepositoryItem);
		}

		private void Populate()
		{
			int newHash = frmTemplateSyncWizard.MyProject.GetHashCode() + frmTemplateSyncWizard.TheirProject.GetHashCode();

			if (newHash != ProjectsHash)
			{
				ProjectsHash = newHash;
				treeListReferencedFiles.BeginUnboundLoad();
				treeListReferencedFiles.Nodes.Clear();
				treeListReferencedFiles.Columns[1].Caption = Path.GetFileName(frmTemplateSyncWizard.TheirProject.ProjectFileName) + " (external project)";
				treeListReferencedFiles.Columns[3].Caption = Path.GetFileName(frmTemplateSyncWizard.MyProject.ProjectFileName) + " (current project)";

				int numChanges = AddProjectDetailNodes();
				numChanges += AddReferencedFileNodes();
				numChanges += AddNamespaceNodes();
				numChanges += AddUserOptionNodes();
				numChanges += AddDefaultValueFunctionNodes();
				treeListReferencedFiles.ExpandAll();
				treeListReferencedFiles.EndUnboundLoad();

				if (numChanges == 0)
				{
					MustSkip = true;
					frmTemplateSyncWizard.Instance.RemoveCurrentScreen();
					return;
				}
			    MustSkip = false;
			}
			else if (MustSkip)
			{
				frmTemplateSyncWizard.Instance.RemoveCurrentScreen();
				return;
			}
		}

		private int AddProjectDetailNodes()
		{
			int numNodes = 0;
			TreeListNode projectDetailsNode = AddNodeTheirs("Project Details", "", "", null, "ProjectDetails", false);

			if (!Slyce.Common.Utility.StringsAreEqual(frmTemplateSyncWizard.TheirProject.ProjectName, frmTemplateSyncWizard.MyProject.ProjectName, false))
			{
				AddNodeBoth("Project Name", frmTemplateSyncWizard.TheirProject.ProjectName, frmTemplateSyncWizard.MyProject.ProjectName, projectDetailsNode, "ProjectName");
				numNodes++;
			}
			if (!Slyce.Common.Utility.StringsAreEqual(frmTemplateSyncWizard.TheirProject.ProjectDescription, frmTemplateSyncWizard.MyProject.ProjectDescription, false))
			{
				AddNodeBoth("Project Description", frmTemplateSyncWizard.TheirProject.ProjectDescription, frmTemplateSyncWizard.MyProject.ProjectDescription, projectDetailsNode, "ProjectDescription");
				numNodes++;
			}
			if (!Slyce.Common.Utility.StringsAreEqual(frmTemplateSyncWizard.TheirProject.Version, frmTemplateSyncWizard.MyProject.Version, false))
			{
				AddNodeBoth("Project Version", frmTemplateSyncWizard.TheirProject.Version, frmTemplateSyncWizard.MyProject.Version, projectDetailsNode, "Version");
				numNodes++;
			}
			if (!Slyce.Common.Utility.StringsAreEqual(frmTemplateSyncWizard.TheirProject.TemplateNamespace, frmTemplateSyncWizard.MyProject.TemplateNamespace, false))
			{
				AddNodeBoth("Template Namespace", frmTemplateSyncWizard.TheirProject.TemplateNamespace, frmTemplateSyncWizard.MyProject.TemplateNamespace, projectDetailsNode, "TemplateNamespace");
				numNodes++;
			}
			if (projectDetailsNode.Nodes.Count == 0)
			{
				treeListReferencedFiles.Nodes.Remove(projectDetailsNode);
			}
			return numNodes;
		}

		private int AddNamespaceNodes()
		{
			TreeListNode namespacesNodeBoth = AddNodeBoth("Namespaces", "", "", null, "Namespaces", false);
			int numAdded = 0;

			foreach (string theirNamespace in frmTemplateSyncWizard.TheirProject.Namespaces)
			{
				bool found = false;

				foreach (string myNamespace in frmTemplateSyncWizard.MyProject.Namespaces)
				{
					if (Slyce.Common.Utility.StringsAreEqual(theirNamespace, myNamespace, false))
					{
						found = true;
						break;
					}
				}
				if (!found)
				{
					AddNodeTheirs(theirNamespace, theirNamespace, "", namespacesNodeBoth, theirNamespace);
					numAdded++;
				}
			}
			foreach (string myNamespace in frmTemplateSyncWizard.MyProject.Namespaces)
			{
				bool found = false;

				foreach (string theirNamespace in frmTemplateSyncWizard.TheirProject.Namespaces)
				{
					if (Slyce.Common.Utility.StringsAreEqual(theirNamespace, myNamespace, false))
					{
						found = true;
					}
				}
				if (!found)
				{
					AddNodeMine(myNamespace, "", myNamespace, namespacesNodeBoth, myNamespace);
					numAdded++;
				}
			}
			if (numAdded == 0)
			{
				treeListReferencedFiles.Nodes.Remove(namespacesNodeBoth);
			}
			return numAdded;
		}

		private int AddReferencedFileNodes()
		{
			TreeListNode referencedFilesNodeBoth = AddNodeBoth("Referenced Files", "", "", null, "References", false);
			int numAddedBoth = 0;

			foreach (ReferencedFile theirAssembly in frmTemplateSyncWizard.TheirProject.References)
			{
				bool found = false;
				TreeListNode fileNode;

				foreach (ReferencedFile myAssembly in frmTemplateSyncWizard.MyProject.References)
				{
					if (Slyce.Common.Utility.StringsAreEqual(Path.GetFileName(theirAssembly.FileName), Path.GetFileName(myAssembly.FileName), false))
					{
						found = true;
						fileNode = AddNodeBoth(Path.GetFileName(theirAssembly.FileName), "", "", referencedFilesNodeBoth, myAssembly, false);

						if (AddReferencedFileNode(fileNode, myAssembly, theirAssembly))
						{
							numAddedBoth++;
						}
						else
						{
							referencedFilesNodeBoth.Nodes.Remove(fileNode);
						}
						break;
					}
				}
				if (!found)
				{
					fileNode = AddNodeTheirs(Path.GetFileName(theirAssembly.FileName), "", "", referencedFilesNodeBoth, null);
					AddReferencedFileNode(fileNode, null, theirAssembly);
					numAddedBoth++;
				}
			}
			foreach (ReferencedFile myAssembly in frmTemplateSyncWizard.MyProject.References)
			{
				bool found = false;

				foreach (ReferencedFile theirAssembly in frmTemplateSyncWizard.TheirProject.References)
				{
					if (Slyce.Common.Utility.StringsAreEqual(Path.GetFileName(theirAssembly.FileName), Path.GetFileName(myAssembly.FileName), false))
					{
						found = true;
						break;
					}
				}
				if (!found)
				{
					TreeListNode fileNode = AddNodeMine(Path.GetFileName(myAssembly.FileName), "", myAssembly.FileName, referencedFilesNodeBoth, myAssembly);
					AddReferencedFileNode(fileNode, myAssembly, null);
					numAddedBoth++;
				}
			}
			if (numAddedBoth == 0)
			{
				treeListReferencedFiles.Nodes.Remove(referencedFilesNodeBoth);
			}
			return numAddedBoth;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>The number of useroptions that have differences.</returns>
		private int AddUserOptionNodes()
		{
			TreeListNode userOptionsNodeBoth = AddNodeBoth("User Options", "", "", null, "UserOptions", false);
			int numAdded = 0;

			foreach (UserOption theirUserOption in frmTemplateSyncWizard.TheirProject.UserOptions)
			{
				if (theirUserOption.IteratorType != null)
				{
					continue;
				}
				bool found = false;
				TreeListNode userOptionNode;

				foreach (UserOption myUserOption in frmTemplateSyncWizard.MyProject.UserOptions)
				{
					if (Slyce.Common.Utility.StringsAreEqual(theirUserOption.VariableName, myUserOption.VariableName, false))
					{
						found = true;
						userOptionNode = AddNodeBoth(myUserOption.VariableName, "", "", userOptionsNodeBoth, myUserOption);

						if (AddUserOptionNode(userOptionNode, myUserOption, theirUserOption))
						{
							numAdded++;
						}
						else
						{
							treeListReferencedFiles.Nodes.Remove(userOptionNode);
						}
						break;
					}
				}
				if (!found)
				{
					userOptionNode = AddNodeTheirs("", theirUserOption.VariableName, "", userOptionsNodeBoth, theirUserOption);
					numAdded++;
				}
			}
			foreach (UserOption myUserOption in frmTemplateSyncWizard.MyProject.UserOptions)
			{
				if (myUserOption.IteratorType != null)
				{
					continue;
				}
				bool found = false;

			    foreach (UserOption theirUserOption in frmTemplateSyncWizard.TheirProject.UserOptions)
				{
					if (Slyce.Common.Utility.StringsAreEqual(theirUserOption.VariableName, myUserOption.VariableName, false))
					{
						found = true;
						break;
					}
				}
				if (!found)
				{
					AddNodeMine("", "", myUserOption.VariableName, userOptionsNodeBoth, myUserOption);
					numAdded++;
				}
			}
			if (numAdded == 0)
			{
				treeListReferencedFiles.Nodes.Remove(userOptionsNodeBoth);
			}
			return numAdded;
		}

		private int AddDefaultValueFunctionNodes()
		{
			TreeListNode typeNodesRoot = AddNodeBoth("API Extensions - Virtual Properties", "", "", null, "APIExtensions", false);

			#region Build list of owner types for virtual properties
			var typeNames = new List<string>();
			var typeNodesBoth = new Dictionary<string, TreeListNode>();

			foreach (UserOption theirUserOption in frmTemplateSyncWizard.TheirProject.UserOptions)
			{
				if (theirUserOption.IteratorType == null)
				{
					continue;
				}
				if (typeNames.BinarySearch(theirUserOption.IteratorType.FullName) < 0)
				{
					typeNames.Add(theirUserOption.IteratorType.FullName);
					typeNames.Sort();
				}
			}
			foreach (UserOption myUserOption in frmTemplateSyncWizard.MyProject.UserOptions)
			{
				if (myUserOption.IteratorType == null)
				{
					continue;
				}
				if (typeNames.BinarySearch(myUserOption.IteratorType.FullName) < 0)
				{
					typeNames.Add(myUserOption.IteratorType.FullName);
					typeNames.Sort();
				}
			}
			foreach (string typeName in typeNames)
			{
			    var newNode = AddNodeBoth(typeName, null, null, typeNodesRoot, typeName, false);
				newNode.StateImageIndex = -1;
				typeNodesBoth.Add(typeName, newNode);
			}
			#endregion

			int numAdded = 0;

			foreach (UserOption theirUserOption in frmTemplateSyncWizard.TheirProject.UserOptions)
			{
				if (theirUserOption.IteratorType == null)
				{
					continue;
				}
				bool found = false;
				TreeListNode userOptionNode;

				foreach (UserOption myUserOption in frmTemplateSyncWizard.MyProject.UserOptions)
				{
					if (Slyce.Common.Utility.StringsAreEqual(theirUserOption.VariableName, myUserOption.VariableName, false))
					{
						found = true;
						userOptionNode = AddNodeBoth("", theirUserOption.VariableName + "[Virtual Property]", myUserOption.VariableName + "[Virtual Property]", typeNodesBoth[theirUserOption.IteratorType.FullName], myUserOption, false);

						if (AddUserOptionNode(userOptionNode, myUserOption, theirUserOption))
						{
							numAdded++;
						}
						else
						{
							treeListReferencedFiles.Nodes.Remove(userOptionNode);
						}
						break;
					}
				}
				if (!found)
				{
					userOptionNode = AddNodeTheirs("", theirUserOption.VariableName + "[Virtual Property]", "", typeNodesBoth[theirUserOption.IteratorType.FullName], theirUserOption, true);
					numAdded++;
				}
			}
			foreach (UserOption myUserOption in frmTemplateSyncWizard.MyProject.UserOptions)
			{
				if (myUserOption.IteratorType == null)
				{
					continue;
				}
				bool found = false;

			    foreach (UserOption theirUserOption in frmTemplateSyncWizard.TheirProject.UserOptions)
				{
					if (Slyce.Common.Utility.StringsAreEqual(theirUserOption.VariableName, myUserOption.VariableName, false))
					{
						found = true;
						break;
					}
				}
				if (!found)
				{
					AddNodeMine("", "", myUserOption.VariableName + "[Virtual Property]", typeNodesBoth[myUserOption.IteratorType.FullName], myUserOption, true);
					numAdded++;
				}
			}
			return numAdded;
		}

	    /// <summary>
		/// 
		/// </summary>
		/// <param name="parentNode"></param>
		/// <param name="myReferencedFile"></param>
		/// <param name="theirReferencedFile"></param>
		/// <returns>True if the referenced files were different and a node was added, false if they were the same and no node was added.</returns>
		private bool AddReferencedFileNode(TreeListNode parentNode, ReferencedFile myReferencedFile, ReferencedFile theirReferencedFile)
		{
			if (myReferencedFile != null && theirReferencedFile != null)
			{
				if (Slyce.Common.Utility.StringsAreEqual(theirReferencedFile.FileName, myReferencedFile.FileName, false) &&
					theirReferencedFile.UseInWorkbench == myReferencedFile.UseInWorkbench)
				{
					return false;
				}
				if (theirReferencedFile.FileName != myReferencedFile.FileName)
				{
					AddNodeBoth("Location", theirReferencedFile.FileName, myReferencedFile.FileName, parentNode, null);
				}
				if (theirReferencedFile.UseInWorkbench != myReferencedFile.UseInWorkbench)
				{
					AddNodeBoth("Display In Workbench", theirReferencedFile.UseInWorkbench, myReferencedFile.UseInWorkbench, parentNode, null);
				}
			}
			else if (theirReferencedFile != null)
			{
				AddNodeTheirs("Location", theirReferencedFile.FileName, "", parentNode, null);
				AddNodeTheirs("Display In Workbench", theirReferencedFile.UseInWorkbench, "", parentNode, null);
			}
			else if (myReferencedFile != null)
			{
				AddNodeMine("Location", "", myReferencedFile.FileName, parentNode, null);
				AddNodeMine("Display In Workbench", "", myReferencedFile.UseInWorkbench, parentNode, null);
			}
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parentNode"></param>
		/// <param name="myOption"></param>
		/// <param name="theirOption"></param>
		/// <returns>True if the referenced files were different and a node was added, false if they were the same and no node was added.</returns>
		private bool AddUserOptionNode(TreeListNode parentNode, UserOption myOption, UserOption theirOption)
		{
			int originalNumChildNodes = parentNode.Nodes.Count;

			if (myOption != null && theirOption != null)
			{
				if (myOption.Category != theirOption.Category) { AddNodeBoth("Category", theirOption.Category, myOption.Category, parentNode, null); }
				if (myOption.DefaultValueFunctionBody != theirOption.DefaultValueFunctionBody) { AddNodeBoth("DefaultValueFunctionBody", theirOption.DefaultValueFunctionBody, myOption.DefaultValueFunctionBody, parentNode, null); }
				if (myOption.Description != theirOption.Description) { AddNodeBoth("Description", theirOption.Description, myOption.Description, parentNode, null); }
				if (myOption.IteratorType != theirOption.IteratorType) { AddNodeBoth("IteratorType", theirOption.IteratorType, myOption.IteratorType, parentNode, null); }
				if (myOption.ResetPerSession != theirOption.ResetPerSession) { AddNodeBoth("ResetPerSession", theirOption.ResetPerSession, myOption.ResetPerSession, parentNode, null); }
				if (myOption.Text != theirOption.Text) { AddNodeBoth("Text", theirOption.Text, myOption.Text, parentNode, null); }
				// TODO: Handle enum values
				//if (myOption.Values != theirOption.Category) { AddNodeBoth("Category", theirOption.Category, myOption.Category, parentNode, null); }
				if (myOption.VariableName != theirOption.VariableName) { AddNodeBoth("VariableName", theirOption.VariableName, myOption.VariableName, parentNode, null); }
				if (myOption.VarType != theirOption.VarType) { AddNodeBoth("VarType", theirOption.VarType, myOption.VarType, parentNode, null); }

				return originalNumChildNodes != parentNode.Nodes.Count;
			}
			
			if (theirOption != null)
			{
				AddNodeTheirs("Category", theirOption.Category, null, parentNode, null);
				AddNodeTheirs("DefaultValue", theirOption.DefaultValueFunctionBody, null, parentNode, null);
				AddNodeTheirs("Description", theirOption.Description, null, parentNode, null);
				AddNodeTheirs("IteratorType", theirOption.IteratorType, null, parentNode, null);
				AddNodeTheirs("ResetPerSession", theirOption.ResetPerSession, null, parentNode, null);
				AddNodeTheirs("Text", theirOption.Text, null, parentNode, null);
				// TODO: Handle enum values
				//if (myOption.Values != theirOption.Category) { AddNodeTheirs("Category", myOption.Category, theirOption.Category, parentNode, null); }
				AddNodeTheirs("VariableName", theirOption.VariableName, null, parentNode, null);
				AddNodeTheirs("VarType", theirOption.VarType, null, parentNode, null);
			}
			else if (myOption != null)
			{
				AddNodeMine("Category", null, myOption.Category, parentNode, null);
				AddNodeMine("DefaultValue", null, myOption.DefaultValueFunctionBody, parentNode, null);
				AddNodeMine("Description", null, myOption.Description, parentNode, null);
				AddNodeMine("IteratorType", null, myOption.IteratorType, parentNode, null);
				AddNodeMine("ResetPerSession", null, myOption.ResetPerSession, parentNode, null);
				AddNodeMine("Text", null, myOption.Text, parentNode, null);
				// TODO: Handle enum values
				//if (myOption.Values != theirOption.Category) { AddNodeMine("Category", myOption.Category, theirOption.Category, parentNode, null); }
				AddNodeMine("VariableName", null, myOption.VariableName, parentNode, null);
				AddNodeMine("VarType", null, myOption.VarType, parentNode, null);
			}
			return true;
		}

		private TreeListNode AddNodeBoth(string name, object theirValue, object myValue, TreeListNode parentNode, object tag)
		{
			return AddNodeBoth(name, theirValue, myValue, parentNode, tag, true);
		}

		private TreeListNode AddNodeBoth(string name, object theirValue, object myValue, TreeListNode parentNode, object tag, bool addActionText)
		{
			TreeListNode node;

			if (addActionText)
			{
				node = treeListReferencedFiles.AppendNode(new[] { name, theirValue, BlueArrow, myValue, "" }, parentNode);
			}
			else
			{
				node = treeListReferencedFiles.AppendNode(new[] { name, theirValue, "", myValue, "" }, parentNode);
			}
			node.Tag = tag;
			return node;
		}

		private TreeListNode AddNodeTheirs(string name, object theirValue, object myValue, TreeListNode parentNode, object tag)
		{
			return AddNodeTheirs(name, theirValue, myValue, parentNode, tag, true);
		}

		private TreeListNode AddNodeTheirs(string name, object theirValue, object myValue, TreeListNode parentNode, object tag, bool addActionText)
		{
			TreeListNode node;

			if (addActionText)
			{
				node = treeListReferencedFiles.AppendNode(new[] { name, theirValue, GreenArrow, myValue, "" }, parentNode);
			}
			else
			{
				node = treeListReferencedFiles.AppendNode(new[] { name, theirValue, "", myValue, "" }, parentNode);
			}
			node.Tag = tag;
			node.StateImageIndex = (int)TreeNodeImages.Unchecked;
			return node;
		}

		private TreeListNode AddNodeMine(string name, object theirValue, object myValue, TreeListNode parentNode, object tag)
		{
			return AddNodeMine(name, theirValue, myValue, parentNode, tag, true);
		}

		private TreeListNode AddNodeMine(string name, object theirValue, object myValue, TreeListNode parentNode, object tag, bool addActionText)
		{
			TreeListNode node;

			if (addActionText)
			{
				node = treeListReferencedFiles.AppendNode(new[] { name, theirValue, "", myValue, RemoveImage }, parentNode);
			}
			else
			{
				node = treeListReferencedFiles.AppendNode(new[] { name, theirValue, "", myValue, "" }, parentNode);
			}
			node.Tag = tag;
			node.StateImageIndex = (int)TreeNodeImages.Unchecked;
			return node;
		}

		public override void OnDisplaying()
		{
			Populate();
		}
		
		private void ApplyChangeForProjectDetails(TreeListNode node)
		{
			var tagName = (string)node.GetValue(0);
			object theirValue = node.GetValue(1);

			switch (tagName)
			{
				case "Project Name":
					frmTemplateSyncWizard.MyProject.ProjectName = (string)theirValue;
					break;
				case "Project Description":
					frmTemplateSyncWizard.MyProject.ProjectDescription = (string)theirValue;
					break;
				case "Project Version":
					frmTemplateSyncWizard.MyProject.Version = (string)theirValue;
					break;
				case "TemplateNamespace":
					frmTemplateSyncWizard.MyProject.TemplateNamespace = (string)theirValue;
					break;
				case "Location":
					((ReferencedFile)treeListReferencedFiles.FocusedNode.ParentNode.Tag).FileName = (string)theirValue;
					break;
				case "Display In Workbench":
					((ReferencedFile)treeListReferencedFiles.FocusedNode.ParentNode.Tag).UseInWorkbench = (bool)theirValue;
					break;
				default:
					throw new NotImplementedException("Node not hendled yet: " + tagName);
			}
		}

		private static void ApplyChangeForApiExtensions(TreeListNode node)
		{
			var tagName = (string)node.GetValue(0);
			object theirValue = node.GetValue(1);

			switch (tagName)
			{
				case "Category":
					((UserOption)node.ParentNode.Tag).Category = (string)theirValue;
					break;
				case "DefaultValue":
					((UserOption)node.ParentNode.Tag).DefaultValueFunctionBody = (string)theirValue;
					break;
				case "Description":
					((UserOption)node.ParentNode.Tag).Description = (string)theirValue;
					break;
				case "IteratorType":
					((UserOption)node.ParentNode.Tag).IteratorType = (Type)theirValue;
					break;
				case "ResetPerSession":
					((UserOption)node.ParentNode.Tag).ResetPerSession = (bool)theirValue;
					break;
				case "Text":
					((UserOption)node.ParentNode.Tag).Text = (string)theirValue;
					break;
				case "VariableName":
					((UserOption)node.ParentNode.Tag).VariableName = (string)theirValue;
					break;
				case "VarType":
					((UserOption)node.ParentNode.Tag).VarType = (Type)theirValue;
					break;
				default:
					throw new NotImplementedException("Node not hendled yet: " + tagName);
			}
		}

		private void treeListReferencedFiles_MouseDown(object sender, MouseEventArgs e)
		{
			DevExpress.XtraTreeList.TreeListHitInfo hInfo = treeListReferencedFiles.CalcHitInfo(new Point(e.X, e.Y));
			TreeListNode node = hInfo.Node;

			if (node == null ||
				hInfo.Column == null ||
				!(hInfo.Column.AbsoluteIndex == 2 || hInfo.Column.AbsoluteIndex == 4))
			{
				return;
			}
			TreeListNode topNode = node;

			while (topNode.ParentNode != null)
			{
				topNode = topNode.ParentNode;
			}
			treeListReferencedFiles.FocusedNode = node;
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
			var topNodeText = (string)topNode.GetValue(0);

			switch (action)
			{
				case "Import >":
					switch (topNodeText)
					{
						case "Referenced Files":
							frmTemplateSyncWizard.MyProject.AddReferencedFile((ReferencedFile)node.Tag);
							break;
						case "Namespaces":
							frmTemplateSyncWizard.MyProject.AddNamespace((string)node.Tag);
							break;
						case "User Options":
							frmTemplateSyncWizard.MyProject.AddUserOption((UserOption)node.Tag);
							break;
						case "API Extensions - Virtual Properties":
							frmTemplateSyncWizard.MyProject.AddUserOption((UserOption)node.Tag);
							break;
						default:
							throw new NotImplementedException("Not coded yet.");
					}
					break;
				case "Apply Change >":
					switch (topNodeText)
					{
						case "Referenced Files":
							throw new NotImplementedException("Not handled yet.");
						case "Namespaces":
							throw new NotImplementedException("Not handled yet.");
						case "User Options":
							throw new NotImplementedException("Not handled yet.");
						case "Project Details":
							ApplyChangeForProjectDetails(node);
							break;
						case "API Extensions - Virtual Properties":
							ApplyChangeForApiExtensions(node);
							break;
						default:
							throw new NotImplementedException("Not coded yet.");
					}
					break;
				case "Remove":
					switch (topNodeText)
					{
						case "Referenced Files":
							frmTemplateSyncWizard.MyProject.RemoveReference((ReferencedFile)node.Tag);
							break;
						case "Namespaces":
							frmTemplateSyncWizard.MyProject.RemoveNamespace((string)node.Tag);
							break;
						case "User Options":
							frmTemplateSyncWizard.MyProject.RemoveUserOption((UserOption)node.Tag);
							break;
						case "API Extensions - Virtual Properties":
							frmTemplateSyncWizard.MyProject.RemoveUserOption((UserOption)node.Tag);
							break;
						default:
							throw new NotImplementedException("Not coded yet.");
					}
					break;
				default:
					throw new NotImplementedException("ActionType not handled yet: " + action);
			}
			// Do any siblings remain?
			while (node.ParentNode != null)
			{
				if (node.ParentNode.Nodes.Count == 1)
				{
					node = node.ParentNode;
				}
				else // The parent has > 1 child
				{
					node.ParentNode.Nodes.Remove(node);
					return;
				}

			}
			treeListReferencedFiles.Nodes.Remove(node);
		}

		private void treeListReferencedFiles_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
		{
			if (e.Node.ParentNode == null)
			{
				e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
				SetFocusedNodeProperties(e);
			}
		}

		private static void SetFocusedNodeProperties(DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
		{
			double brightness = Slyce.Common.Colors.GetBrightness(e.Appearance.BackColor);
			double lightBrightness = brightness > 0.5 ? brightness - 0.2 : brightness + 0.1;
			double darkBrightness = brightness > 0.5 ? brightness - 0.6 : brightness - 0.4;

			if (lightBrightness > 1) { lightBrightness = 1; }
			if (darkBrightness < 0) { darkBrightness = 0; }

			Color lightColor = Slyce.Common.Colors.ChangeBrightness(e.Appearance.BackColor, lightBrightness);
			Color darkColor = Slyce.Common.Colors.ChangeBrightness(e.Appearance.BackColor, darkBrightness);

			e.Appearance.ForeColor = Slyce.Common.Colors.IdealTextColor(darkColor);
			e.Appearance.BackColor = lightColor;
			e.Appearance.BackColor2 = darkColor;
			e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			e.Appearance.Options.UseBackColor = true;
		}

		private void treeListReferencedFiles_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
		{
			int columnIndex = e.Column.AbsoluteIndex;

			if (columnIndex == 2 || columnIndex == 4)
			{
				if (typeof(Bitmap).IsInstanceOfType(e.Node.GetValue(columnIndex)))
				{
					e.RepositoryItem = repositoryItemPictureEdit4;
				}
				else
				{
					e.RepositoryItem = EmptyRepositoryItem;
				}
			}
		}
	}
}
