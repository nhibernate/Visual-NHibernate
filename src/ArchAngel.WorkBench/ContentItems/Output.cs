using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ArchAngel.Common;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Workbench.IntelliMerge;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Validator;
using log4net;
using Slyce.Common;
using Slyce.Common.Controls;
using Slyce.IntelliMerge;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge.UI;
using Slyce.IntelliMerge.UI.Editors;
using FolderBrowserDialog = Vista_Api.FolderBrowserDialog;
using Timer = System.Threading.Timer;
using Utility = Slyce.Common.Utility;

namespace ArchAngel.Workbench.ContentItems
{
	public partial class Output : Interfaces.Controls.ContentItems.ContentItem
	{
		private const string GenerateFilesAsyncKey = "GenerateFilesAsync";
		private const string StartAnalysisKey = "StartAnalysis";
		private const string OutputEditorKey = "OutputEditor";
		private const string IntelliMergeCellKey = "IntellimergeCell";
		private const string FilenameCellKey = "FilenameCell";
		private const int StatusButtonWidthLarge = 110;
		private const int StatusButtonWidthSmall = 50;
		private DevComponents.DotNetBar.ElementStyle elementStyleUnchecked;

		private readonly Dictionary<string, Node> treeNodes = new Dictionary<string, Node>();
		private readonly ProjectFileTree treeModel;

		private static readonly ILog log = LogManager.GetLogger(typeof(Output));

		#region Delegate Definitions
		public delegate void TemplateFunctionErrorDelegate(string functionName, string description);
		internal delegate void RunBackgroundWorkerDelegate();
		#endregion

		#region Enums

		[DotfuscatorDoNotRename]
		enum NodeImages
		{
			//Blah = 0,
			//Field = 1,
			//Method = 2,
			//Properties = 3,
			//File_CSharp = 4,
			//File_Default = 5,
			Blank = 7,
			//OtherOptions = 6,
			StatusWarning = 8,
			//StatusExactCopy = 9,
			StatusExactCopy = 35,//7,
			StatusResolved = 36,//9,
			StatusConflict = 10,
			//StatusResolvedUserOnly = 11,
			//StatusResolvedTemplateOnly = 12,
			//StatusResolvedUserAndTemplate = 13,
			//Namespace = 14,
			//Event = 15,
			//Constant = 16,
			//Operator = 17,
			//Struct = 18,
			//Interface = 19,
			//Enum = 20,
			//Delegate = 21,
			Error = 22,
			FolderClosed = 23,
			FolderOpen = 24,
			QuestionMark = 25,
			FolderClosedError = 26,
			FolderOpenError = 27,
			StatusNewFile = 34
		}
		#endregion

		#region Fields
		private IScriptBaseObject _CurrentRootObject;
		//private int GenFileCounter = 0;
		//private int _countOfFilesToBeProcessed = 0;
		private string OriginalTitle;
		/// <summary>Set to true if we are currently populating the Tree View.</summary>
		private bool BusyPopulatingTreeNodes;
		/// <summary>Set to true if the background worker is currently analysing the generated files.</summary>
		private bool BusyAnalysing;
		//private List<GenerationError> GenerationErrors = new List<GenerationError>();
		//private bool InUnboundLoad = false;
		/// <summary>The number of files that are in a state of conflict. Used for display in the Gui.</summary>
		private int NumConflicts;
		/// <summary>The number of files that have resolvable changes. Used for display in the Gui.</summary>
		private int NumResolved;
		/// <summary>The number of files that are exact copies. Used for display in the Gui.</summary>
		private int NumExactCopy { get; set; }
		/// <summary>The number of files that have errors. Used for display in the Gui.</summary>
		private int NumErrors;
		private int NumNewFiles;
		/// <summary>
		/// Lock object used to prevent multiple threads entering the cancel task region.
		/// </summary>
		private readonly object _CancelBackgroundWorkerLock = new Object();
		/// <summary>
		/// Files will be added to this queue while a message box is shown to the user asking for confirmation
		/// before starting the re-analysis of changed user files.
		/// </summary>
		private readonly Queue<string> _ModifiedFiles = new Queue<string>();
		/// <summary>
		/// Holds information about the progress of the Analyse Files task while it is running.
		/// Also allows the Gui thread to control the task by bumping files to the top of the
		/// process queue to get them processed earlier.
		/// </summary>
		private AnalysisProgressHelper _AnalysisProgressHelper;
		/// <summary>
		/// Set to true once the Generate Files task has run. If the Generate Files task starts again,
		/// it will set this to false again. Because the flag will not be set until the Generate Files task 
		/// has fully finished, we do not have to worry about the flag being in an incorrect state. If the
		/// generate files task is cancelled, then it will need to be run before the next tasks can be run anyway,
		/// and they have checks for that.
		/// </summary>
		private bool HasFileGenerationFinished;
		/// <summary>
		/// Set to true once the Analyse Files task has run. If the Analyse Files task starts again,
		/// it will set this to false again. Because the flag will not be set until the Analyse Files task 
		/// has fully finished, we do not have to worry about the flag being in an incorrect state. If the
		/// generate files task is cancelled, then it will need to be run before the next tasks can be run anyway,
		/// and they have checks for that.
		/// </summary>
		private bool HasFileAnalysisFinished;

		private readonly List<FilenameInfo> duplicatedFiles = new List<FilenameInfo>();
		//private bool NodeToggled = false;

		private readonly RunBackgroundWorkerDelegate _RunBackgroundWorkerAnalyseDelegate;
		private readonly RunBackgroundWorkerDelegate _RunBackgroundWorkerGenerateDelegate;

		private int confirmProjectReloadCounter;
		private Node currentFocusedNode;

		#endregion

		public Output()
		{
			InitializeComponent();

			treeModel = Controller.Instance.ProjectFileTreeModel;
			treeModel.TreeNodeChanged += treeModel_TreeNodeChanged;
			treeModel.TreeNeedsAnalysis += treeModel_TreeNeedsAnalysis;
			Controller.Instance.OnProjectModification += Controller_OnProjectModification;
			Controller.Instance.UserFilesChanged += Controller_UserFilesChanged;
			Controller.Instance.OnProjectLoaded += Project_OnProjectLoaded;

			Interfaces.Events.CancelGenerationEvent += Events_CancelGenerationEvent;

			_RunBackgroundWorkerGenerateDelegate = RunBackgroundWorkerGenerateFiles;
			_RunBackgroundWorkerAnalyseDelegate = RunBackgroundWorkerAnalyseFiles;

			elementStyleUnchecked = elementStyle1.Copy();
			elementStyleUnchecked.TextColor = Color.Gray;
			Populate();
		}

		public override void Clear()
		{
			CancelCurrentTask();
			ClearErrorProvider();
			treeList1.Nodes.Clear();
			treeNodes.Clear();

			ResetRootNode();
		}

		private void Events_CancelGenerationEvent()
		{
			CancelCurrentTask();
		}

		private void treeModel_TreeNeedsAnalysis(object sender, EventArgs e)
		{
			AnalyseFiles();
		}

		#region Gui Update Methods

		public void ProjectPathChanged()
		{
			if (InvokeRequired)
			{
				MethodInvoker mi = ProjectPathChanged;
				Controller.SafeInvoke(this, mi, true);
				return;
			}
			ResetRootNode();
		}

		private Node ResetRootNode()
		{
			Node root;
			string text;

			if (Controller.Instance.CurrentProject != null &&
				Controller.Instance.CurrentProject.ProjectSettings != null &&
				Controller.Instance.CurrentProject.ProjectSettings.OutputPath != null &&
				Directory.Exists(Controller.Instance.CurrentProject.ProjectSettings.OutputPath))
			{
				text = string.Format("<a><font color=\"White\">{0}</font></a>", Controller.Instance.CurrentProject.ProjectSettings.OutputPath);
			}
			else
				text = "<a><font color=\"White\">Click here to set output folder</font></a>";

			if (treeList1.Nodes.Count == 0)
			{
				// Add root node
				root = new Node();
				treeList1.Nodes.Add(root);
				//root.Cells.Add(new Cell());

				// Set the display text of the node.
				//root.Cells[0].Text = text;
				root.MarkupLinkClick += new MarkupLinkClickEventHandler(RootNode_MarkupLinkClick);
				root.CheckBoxVisible = false;
				root.Collapse();
				root.ImageIndex = (int)NodeImages.FolderOpen;
			}
			else
			{
				root = treeList1.Nodes[0];
				//treeList1.Nodes[0].Text = text;
			}
			root.Text = text;
			root.NodeMouseEnter += delegate(object sender, EventArgs e) { ((Node)sender).Text = ((Node)sender).Text.Replace("color=\"White\"", "color=\"Black\""); };
			root.NodeMouseLeave += delegate(object sender, EventArgs e) { ((Node)sender).Text = ((Node)sender).Text.Replace("color=\"Black\"", "color=\"White\""); };

			superTooltip1.SetSuperTooltip(treeList1.Nodes[0], new DevComponents.DotNetBar.SuperTooltipInfo("Change folder", "", "Click to set output folder.", null, null, DevComponents.DotNetBar.eTooltipColor.Default));
			superTooltip1.PositionBelowControl = false;
			return root;
		}

		private void ReloadEntireTree()
		{
			if (InvokeRequired)
			{
				MethodInvoker mi = ReloadEntireTree;
				Controller.SafeInvoke(this, mi, true);
				return;
			}

			if (treeList1.Nodes.Count > 0)
			{
				Controller.Instance.SaveFileTreeStatus(treeModel);
			}

			treeList1.BeginUpdate();

			// Reset the tree and node cache
			treeList1.Nodes.Clear();
			treeNodes.Clear();

			Node root = ResetRootNode();
			Controller.Instance.LoadFileTreeStatus(treeModel);

			treeModel.SortChildren();

			foreach (ProjectFileTreeNode node in treeModel.ChildNodes)
			{
				if (node.IsExternalToOutputFolder)
					AddTreeNode(null, node);
				else
					AddTreeNode(root, node);
			}
			root.Expand();
			root.ImageIndex = (int)NodeImages.FolderOpen;
			SelectFirstFileNode();

			UpdateCheckStates(root);
			UpdateNodeCheckedStatus(root);
			treeList1.EndUpdate();
		}

		private void SelectFirstFileNode()
		{
			ProjectFileTreeNode firstNode = treeModel.DepthFirstSearch(new IsLeafNodeSearchCondition());

			if (firstNode != null)
			{
				Node node = GetTreeNodeForFilePath(firstNode.Path);
				treeList1.SelectedNode = node;
				if (node != null)
					node.Expand();
			}
		}

		void RootNode_MarkupLinkClick(object sender, MarkupLinkClickEventArgs e)
		{
			ChangeOutputPath();
		}


		private static void UpdateCheckStates(Node node)
		{
			/*
			if (node.Cells.Count == 0)
				return;
			
			bool anyChildTicked = false;
			bool anyChildUnticked = false;
			bool anyChildIndeterminate = false;
			
			foreach(Node child in node.Nodes)
			{
				UpdateCheckStates(child);
				if (child.Cells[1].CheckState == CheckState.Checked)
					anyChildTicked = true;
				else if (child.Cells[1].CheckState == CheckState.Unchecked)
					anyChildUnticked = true;
				else
					anyChildIndeterminate = true;
			}

			if(anyChildTicked && !anyChildUnticked && !anyChildIndeterminate)
			{
				// All children are ticked.
				node.Cells[1].CheckState = CheckState.Checked;
			}
			else if((anyChildTicked && anyChildUnticked) || anyChildIndeterminate)
			{
				// Children are both ticked and unticked (or indeterminate).
				node.Cells[1].CheckState = CheckState.Indeterminate;
			}
			else if(anyChildUnticked)
			{
				// All children are unticked.
				node.Cells[1].CheckState = CheckState.Unchecked;
			}
			*/
		}


		private void UpdateNodeCheckedStatus(Node node)
		{
			bool anyChildTicked = false;
			bool anyChildUnticked = false;
			bool anyChildIndeterminate = false;

			foreach (Node child in node.Nodes)
			{
				UpdateNodeCheckedStatus(child);
				UpdateCheckStates(child);
				switch (child.CheckState)
				{
					case CheckState.Checked:
						anyChildTicked = true;
						break;
					case CheckState.Unchecked:
						anyChildUnticked = true;
						break;
					default:
						anyChildIndeterminate = true;
						break;
				}
			}

			if (anyChildTicked && !anyChildUnticked && !anyChildIndeterminate)
			{
				// All children are ticked.
				node.CheckState = CheckState.Checked;
			}
			else if ((anyChildTicked && anyChildUnticked) || anyChildIndeterminate)
			{
				// Children are both ticked and unticked (or indeterminate).
				node.CheckState = CheckState.Indeterminate;
			}
			else if (anyChildUnticked)
			{
				// All children are unticked.
				node.CheckState = CheckState.Unchecked;
			}
		}

		public void SaveProjectFileTree(string savePath)
		{
			if (treeModel.AllNodes.Count > 0)
			{
				Controller.Instance.SaveFileTreeStatus(treeModel, savePath);
			}
		}

		private void AddTreeNode(Node parentNode, ProjectFileTreeNode fileNode)
		{
			Node newNode = new Node();
			if (parentNode == null)
				treeList1.Nodes.Add(newNode);
			else
				parentNode.Nodes.Add(newNode);

			// Set the display text of the node.
			newNode.Text = fileNode.Text;
			newNode.Style = elementStyle1;

			newNode.Checked = fileNode.NodeSelected;
			newNode.CheckBoxVisible = true;

			if (fileNode.ChildNodes.Count > 0)
			{
				newNode.CheckBoxThreeState = true;
				newNode.CheckState = fileNode.NodeSelected ? CheckState.Checked : CheckState.Indeterminate;
			}

			newNode.Tag = fileNode.Path;

			// Add the node to the node hashtable
			// HACK: This means we can ignore duplicate nodes.
			treeNodes[fileNode.Path] = newNode;

			UpdateTreeListNode(newNode, fileNode, false);

			fileNode.SortChildren();

			foreach (ProjectFileTreeNode childNode in fileNode.ChildNodes)
			{
				AddTreeNode(newNode, childNode);
			}
		}

		private void UpdateTreeListNode(Node treeListNode, ProjectFileTreeNode fileNode, bool updateChildren)
		{
			if (InvokeRequired)
			{
				MethodInvoker invoker = () => UpdateTreeListNode(treeListNode, fileNode, updateChildren);
				Controller.SafeInvoke(this, invoker, false);
				return;
			}
			//treeList1.BeginUpdate();

			log.Info("Updating file node " + fileNode.Path + " and tree node " + GetRelativePathForTreeNode(treeListNode));

			// Set the new nodes selection status.
			treeListNode.Checked = fileNode.NodeSelected;
			bool nodeExpanded = treeListNode.Expanded;
			// If the node is a folder, check its children and mark it with an error indicator if one of them has an error.
			if (fileNode.IsFolder)
			{
				if (treeListNode.Expanded)
					treeListNode.ImageIndex = (int)NodeImages.FolderOpen;
				else
					treeListNode.ImageIndex = (int)NodeImages.FolderClosed;

				if (fileNode.HasDecendentWithError())
				{
					treeListNode.ImageIndex = treeListNode.Expanded ? (int)NodeImages.FolderOpenError : (int)NodeImages.FolderClosedError;
				}
			}
			else
			{
				if (!checkBoxPerformAnalysis.Checked)
				{
					fileNode.Status = ProjectFileStatusEnum.AnalysedFile;
					fileNode.AssociatedFile.CurrentDiffResult.SetAsOverwite();
				}
				// work out what icon to show for the file.
				switch (fileNode.Status)
				{
					case ProjectFileStatusEnum.UnAnalysedFile:
						treeListNode.ImageIndex = (int)NodeImages.QuestionMark;
						SetParentToFolder(treeListNode);
						break;
					case ProjectFileStatusEnum.AnalysedFile:
						int imageIndex;
						switch (fileNode.AssociatedFile.CurrentDiffResult.DiffType)
						{
							case TypeOfDiff.ExactCopy:
								imageIndex = (int)NodeImages.StatusExactCopy;
								break;
							case TypeOfDiff.UserChangeOnly:
							case TypeOfDiff.TemplateChangeOnly:
							case TypeOfDiff.UserAndTemplateChange:
								imageIndex = (int)NodeImages.StatusResolved;
								break;
							case TypeOfDiff.Conflict:
								imageIndex = (int)NodeImages.StatusConflict;
								break;
							//case TypeOfDiff.Warning:
							case TypeOfDiff.NewFile:
								imageIndex = (int)NodeImages.StatusNewFile;
								break;
							default:
								imageIndex = (int)NodeImages.StatusWarning;
								break;
						}
						SetParentToFolder(treeListNode);
						treeListNode.ImageIndex = imageIndex;
						break;
					case ProjectFileStatusEnum.GenerationError:
					case ProjectFileStatusEnum.AnalysisError:
					case ProjectFileStatusEnum.MergeError:
						treeListNode.ImageIndex = (int)NodeImages.Error;
						SetParentToError(treeListNode);
						break;
				}
			}

			// If the currently focused node changed
			if (treeList1.SelectedNode == treeListNode)
			{
				// reload the Editor Window
				UpdateEditorView();
			}

			if (updateChildren)
			{
				foreach (ProjectFileTreeNode childFileNode in fileNode.ChildNodes)
				{
					Node childTreeNode;

					if (treeNodes.TryGetValue(childFileNode.Path, out childTreeNode))
					{
						UpdateTreeListNode(childTreeNode, childFileNode, true);
					}
				}
			}

			List<Node> nodes = new List<Node>();
			nodes.Add(treeListNode);
			Node parent = treeListNode.Parent;
			while (parent != null)
			{
				nodes.Add(parent);
				parent = parent.Parent;
			}
			ResetFileCountsFromProjectModel();
			if (treeList1.Nodes.Count > 0)
				UpdateCheckStates(treeList1.Nodes[0]);
			FilterNodes(nodes);
			treeList1.Invalidate();
			if (nodeExpanded != treeListNode.Expanded)
			{

			}
		}

		private void FilterNodes()
		{
			// Don't filter if we are only displaying the root node
			if (treeList1.Nodes.Count == 1 && treeList1.Nodes[0].Nodes.Count == 0)
				return;

			Cursor originalCursor = Cursor;
			Cursor = Cursors.WaitCursor;

			treeList1.BeginUpdate();

			foreach (Node child in treeList1.Nodes)
			{
				FilterNode(child);
			}
			treeList1.EndUpdate();
			//treeList1.RecalcLayout();
			Cursor = originalCursor;
		}

		private void FilterNode(Node node)
		{
			foreach (Node child in node.Nodes)
			{
				FilterNode(child);
			}

			bool nodeVisible = node.Nodes.Count == 0 ? IsNodeVisible(node) : node.AnyVisibleNodes;

			node.Selectable = node.Enabled = node.Visible = nodeVisible;
		}

		private void FilterNodes(IEnumerable<Node> nodes)
		{
			List<Node> folderNodes = new List<Node>();
			foreach (Node node in nodes)
			{
				if (node.Nodes.Count > 0)
				{
					if (folderNodes.Contains(node) == false)
						folderNodes.Add(node);
					continue;
				}

				node.Selectable = node.Enabled = node.Visible = IsNodeVisible(node);// || IsAnyChildVisible(node);
			}

			foreach (Node node in folderNodes)
			{
				node.Selectable = node.Enabled = node.Visible = node.AnyVisibleNodes;
			}
			treeList1.RecalcLayout();
		}

		private static void SetParentToError(Node node)
		{
			Node parent = node.Parent;
			while (parent != null)
			{
				parent.ImageIndex = parent.Expanded ? (int)NodeImages.FolderOpenError : (int)NodeImages.FolderClosedError;
				parent = parent.Parent;
			}
		}

		private static void SetParentToFolder(Node node)
		{
			Node parent = node.Parent;
			while (parent != null)
			{
				bool result = false;
				foreach (Node child in parent.Nodes)
				{
					if (child.ImageIndex == (int)NodeImages.Error ||
						child.ImageIndex == (int)NodeImages.FolderOpenError ||
						child.ImageIndex == (int)NodeImages.FolderClosedError)
					{
						result = true;
						break;
					}
				}

				if (result) // If we found an child with an error.
					break;

				if (parent.Expanded)
					parent.ImageIndex = (int)NodeImages.FolderOpen;
				else
					parent.ImageIndex = (int)NodeImages.FolderClosed;

				parent = parent.Parent;
			}
		}

		private void Populate()
		{
			treeList1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";

			HideDuplicatedFilesPane();

			HasPrev = true;
			HasNext = true;

			NextText = "&Generate";
			HelpPage = "Workbench_Screen_Analysis.htm";
			Title = "Generation";
			PageDescription = "Select the files you want to generate.";
			//ShowConflicts = true;
			ShowResolved = true;
			ShowUnchanged = true;
			ShowNewFiles = true;
			ShowErrors = true;
		}

		/// <summary>
		/// Resets the ExactCopy, Resolved, and Conflicts counts.
		/// </summary>
		public void ResetCounts()
		{
			NumExactCopy = 0;
			NumResolved = 0;
			NumConflicts = 0;
			NumNewFiles = 0;
		}

		private void SetNumGeneratedFiles(int num)
		{
			if (InvokeRequired)
			{
				Controller.SafeInvoke(this, new MethodInvoker(() => SetNumGeneratedFiles(num)), false);
				return;
			}

			labelNumGeneratedFiles.Text = string.Format("Files Generated: {0}", num);
			Utility.UpdateMessagePanelStatus(panelGeneratedFiles, string.Format("Files Generated: {0}", num));
		}

		/// <summary>
		/// Thread-safe method for setting the text on labelNumGeneratedFiles.
		/// </summary>
		/// <param name="text"></param>
		private void UpdateNumGeneratedFilesLabel(string text)
		{
			if (InvokeRequired)
			{
				Controller.SafeInvoke(this, new MethodInvoker(() => UpdateNumGeneratedFilesLabel(text)), false);
				return;
			}
			labelNumGeneratedFiles.Text = text;
		}

		private void SetNumFilesToBeAnalysed(int num)
		{
			UpdateNumGeneratedFilesLabel(string.Format("Files Left To Be Analysed: {0}", num));
		}

		private void FinishedGenerationAndAnalysis()
		{
			UpdateNumGeneratedFilesLabel("Finished Generation and Analysis");
			ResetFileCountsFromProjectModel();
		}

		private void ResetFileCountsFromProjectModel()
		{
			ResetCounts();
			ReadOnlyCollection<ProjectFileTreeNode> nodes = treeModel.AllNodes;
			foreach (ProjectFileTreeNode node in nodes)
			{
				if (node.AssociatedFile != null && node.AssociatedFile.CurrentDiffResult != null)
				{
					TypeOfDiff diffType = node.AssociatedFile.CurrentDiffResult.DiffType;
					switch (diffType)
					{
						case TypeOfDiff.Conflict:
							NumConflicts++;
							break;
						case TypeOfDiff.ExactCopy:
							NumExactCopy++;
							break;
						case TypeOfDiff.TemplateChangeOnly:
						case TypeOfDiff.UserChangeOnly:
						case TypeOfDiff.UserAndTemplateChange:
							NumResolved++;
							break;
						case TypeOfDiff.Warning:
							// TODO: handle warnings
							break;
						case TypeOfDiff.NewFile:
							NumNewFiles++;
							break;
						default:
							throw new NotImplementedException("Not coded yet: " + diffType);
					}
				}
			}
			//UpdateStatusButtons(false);
		}

		/// <summary>
		/// Sets the number of conflicts, resolved and exact copy files and updates the GUI.
		/// </summary>
		/// <param name="numConflicts">The number of files in conflict.</param>
		/// <param name="numResolved">The number of files with resolvable changes.</param>
		/// <param name="numExactCopy">The number of files without changes.</param>
		/// <param name="numErrors">The number of files with errors</param>
		public void UpdateStatusButtons(int numConflicts, int numResolved, int numExactCopy, int numErrors)
		{
			NumConflicts = numConflicts;
			NumResolved = numResolved;
			NumExactCopy = numExactCopy;
			NumErrors = numErrors;

			UpdateStatusButtons(false);
		}

		/// <summary>
		/// Forces an update of the status buttons with the analysed files counts.
		/// </summary>
		/// <param name="resetToUnknown">If true, clear the file counts and display ? as the count.</param>
		[DotfuscatorDoNotRename]
		public void UpdateStatusButtons(bool resetToUnknown)
		{
			if (labelErrors.InvokeRequired)
			{
				MethodInvoker invoker = () => UpdateStatusButtons(resetToUnknown);
				Controller.SafeInvoke(this, invoker, false);
				return;
			}
			if (resetToUnknown)
			{
				if (IsDisplayingLargeStatusButtons)
				{
					labelNewFiles.Text = " ? New";
					labelUnchangedFiles.Text = " ? Unchanged";
					labelChangedFiles.Text = " ? Changed";
					labelErrors.Text = " ? Errors";
				}
				else
				{
					labelNewFiles.Text = " ?";
					labelUnchangedFiles.Text = " ?";
					labelChangedFiles.Text = " ?";
					labelErrors.Text = " ?";
				}
			}
			else
			{
				if (IsDisplayingLargeStatusButtons)
				{
					labelNewFiles.Text = string.Format(" {0} New", NumNewFiles);
					labelUnchangedFiles.Text = string.Format(" {0} Unchanged", NumExactCopy);
					labelChangedFiles.Text = string.Format(" {0} Changed", NumResolved);
					labelErrors.Text = string.Format(" {0} Errors", NumErrors);
				}
				else
				{
					labelNewFiles.Text = string.Format(" {0}", NumNewFiles);
					labelUnchangedFiles.Text = string.Format(" {0}", NumExactCopy);
					labelChangedFiles.Text = string.Format(" {0}", NumResolved);
					labelErrors.Text = string.Format(" {0}", NumErrors);
				}
			}
			//if (BusyAnalysing == false && BusyPopulatingTreeNodes == false
			//    && twEditor.State == ActiproSoftware.UIStudio.Dock.ToolWindowState.DockableInsideHost &&
			//    twEditor.ToolWindowContainer.RootDock == DockStyle.Right)
			//{
			//    const int gap = 5;
			//    int totalSmallRight = StatusButtonWidthSmall * 4 + gap * 3 + treeList1.Left;
			//    int totalLargeRight = StatusButtonWidthLarge * 4 + gap * 3 + treeList1.Left;

			//    if (twEditor.DockedSize.Width > ClientSize.Width - totalSmallRight ||
			//        twEditor.DockedSize.Width < ClientSize.Width - totalLargeRight - 100)
			//    {
			//        int newWidth;

			//        if (totalLargeRight < ClientSize.Width - 200)
			//        {
			//            newWidth = ClientSize.Width - totalLargeRight - 5;
			//        }
			//        else if (totalSmallRight < ClientSize.Width - 200)
			//        {
			//            newWidth = ClientSize.Width - totalSmallRight - 5;
			//        }
			//        else
			//        {
			//            newWidth = ClientSize.Width / 2;
			//        }
			//        twEditor.DockedSize = new Size(newWidth, twEditor.Height);
			//    }
			//    try
			//    {
			//        Utility.SuspendPainting(this);
			//        twEditor.DockTo(dockManager1, ActiproSoftware.UIStudio.Dock.DockOperationType.RightInner);
			//    }
			//    finally
			//    {
			//        Utility.ResumePainting(this);
			//    }
			//}
		}

		private bool IsDisplayingLargeStatusButtons
		{
			get { return labelNewFiles.Width > StatusButtonWidthSmall; }
		}

		/// <summary>
		/// Make status buttons small or large (with/without text) depending on container size.
		/// </summary>
		private void ResizeStatusButtons()
		{
			const int gap = 5;
			int totalLargeRight = gap + StatusButtonWidthLarge * 4 + gap * 3 + treeList1.Left;
			bool changed = false;

			if (IsDisplayingLargeStatusButtons && totalLargeRight > panelGeneratedFiles.Width)
			{
				// Display small versions
				labelNewFiles.Width = labelUnchangedFiles.Width = labelChangedFiles.Width = labelErrors.Width = StatusButtonWidthSmall;
				labelNewFiles.Text = labelNewFiles.Text.Substring(0, labelNewFiles.Text.LastIndexOf(" "));
				labelUnchangedFiles.Text = labelUnchangedFiles.Text.Substring(0, labelUnchangedFiles.Text.LastIndexOf(" "));
				labelChangedFiles.Text = labelChangedFiles.Text.Substring(0, labelChangedFiles.Text.LastIndexOf(" "));
				labelErrors.Text = labelErrors.Text.Substring(0, labelErrors.Text.LastIndexOf(" "));
				changed = true;
			}
			else if (!IsDisplayingLargeStatusButtons && totalLargeRight < panelGeneratedFiles.Width)
			{
				// Display large versions
				labelNewFiles.Width = labelUnchangedFiles.Width = labelChangedFiles.Width = labelErrors.Width = StatusButtonWidthLarge;
				labelNewFiles.Text = labelNewFiles.Text + " New";
				labelUnchangedFiles.Text = labelUnchangedFiles.Text + " Unchanged";
				labelChangedFiles.Text = labelChangedFiles.Text + " Changed";
				labelErrors.Text = labelErrors.Text + " Errors";
				changed = true;
			}
			if (changed)
			{
				labelNewFiles.Left = gap;
				labelUnchangedFiles.Left = labelNewFiles.Right + gap;
				labelChangedFiles.Left = labelUnchangedFiles.Right + gap;
				labelErrors.Left = labelChangedFiles.Right + gap;
			}
		}

		private void SetFilterLabelColour(DevComponents.DotNetBar.LabelX label, bool isSelected)
		{
			if (isSelected)
			{
				//double brightness = Colors.GetBrightness(Colors.BackgroundColor);
				//label.BackColor = Color.Transparent;
				//label.BackgroundStyle.BackColor = Colors.ChangeBrightness(Colors.BackgroundColor, brightness - 0.2);
				//label.BackgroundStyle.BackColor2 = Colors.ChangeBrightness(Colors.BackgroundColor2, brightness - 0.2);
				//label.BackgroundStyle.BackColor2 = Color.GreenYellow;
				//label.BorderStyle = BorderStyle.None;
				//label.BackgroundStyle.BorderBottomWidth = 1;
				//label.BackgroundStyle.BorderLeftWidth = 1;
				//label.BackgroundStyle.BorderRightWidth = 1;
				//label.BackgroundStyle.BorderTopWidth = 1;

				//highlighter1.CustomHighlightColors = new Color[] {Color.FromArgb(110, 110, 110) };
				highlighter1.CustomHighlightColors = new Color[] { Color.FromArgb(150, 150, 150), Color.FromArgb(200, 200, 200), Color.FromArgb(220, 220, 220) };
				highlighter1.SetHighlightColor(label, eHighlightColor.Custom);


				//label.BackgroundStyle.BackColor = Colors.ChangeBrightness(Colors.BackgroundColor, brightness - 0.2);
				//label.BackgroundStyle.BackColor2 = Color.Orange;
			}
			else
			{
				//label.BackColor = Color.Transparent;
				//label.BackgroundStyle.BackColor = Colors.BackgroundColor;
				//label.BackgroundStyle.BackColor2 = Colors.BackgroundColor2;
				//label.BorderStyle = BorderStyle.None;
				//label.BackgroundStyle.BorderBottomWidth = 0;
				//label.BackgroundStyle.BorderLeftWidth = 0;
				//label.BackgroundStyle.BorderRightWidth = 0;
				//label.BackgroundStyle.BorderTopWidth = 0;

				highlighter1.SetHighlightColor(label, eHighlightColor.None);
				//label.BackgroundStyle.BackColor2 = label.BackgroundStyle.BackColor;
			}
			//label.ForeColor = Color.Black;
			label.Refresh();
		}

		private static void SetFolderImage(Node node)
		{
			switch (node.ImageIndex)
			{
				case (int)NodeImages.FolderClosed:
					node.ImageIndex = (int)NodeImages.FolderOpen;
					break;
				case (int)NodeImages.FolderOpen:
					node.ImageIndex = (int)NodeImages.FolderClosed;
					break;
				case (int)NodeImages.FolderClosedError:
					node.ImageIndex = (int)NodeImages.FolderOpenError;
					break;
				case (int)NodeImages.FolderOpenError:
					node.ImageIndex = (int)NodeImages.FolderClosedError;
					break;
			}
		}

		private bool IsNodeVisible(Node node)
		{
			bool currentNodeVisibleStatus;
			switch (node.ImageIndex)
			{
				case (int)NodeImages.FolderClosed:
				case (int)NodeImages.FolderOpen:
					currentNodeVisibleStatus = false;
					break;
				//case (int)NodeImages.StatusConflict:
				//   currentNodeVisibleStatus = ShowConflicts;
				//   break;
				case (int)NodeImages.StatusExactCopy:
					currentNodeVisibleStatus = ShowUnchanged;
					break;
				//case (int)NodeImages.StatusResolvedTemplateOnly:
				//case (int)NodeImages.StatusResolvedUserAndTemplate:
				//case (int)NodeImages.StatusResolvedUserOnly:
				case (int)NodeImages.StatusResolved:
					currentNodeVisibleStatus = ShowResolved;
					break;
				case (int)NodeImages.Error:
					currentNodeVisibleStatus = ShowErrors;
					break;
				case (int)NodeImages.QuestionMark:
					currentNodeVisibleStatus = true;
					break;
				case (int)NodeImages.StatusNewFile:
					currentNodeVisibleStatus = ShowNewFiles;
					break;
				default:
					currentNodeVisibleStatus = ShowUnchanged;
					//throw new NotImplementedException("Not coded yet: " + node.ImageIndex);
					break;
			}
			return currentNodeVisibleStatus;
		}

		/// <summary>
		/// Don't call this outside of the BackgroundWorker thread!
		/// Performs the required setup actions on the form before GenerateFilesAsync is called.
		/// </summary>
		private void SetupPopulateFiles()
		{
			if (InvokeRequired)
			{
				MethodInvoker mi = SetupPopulateFiles;
				Controller.SafeInvoke(this, mi, true);
				return;
			}
			SetNumGeneratedFiles(0);
			OriginalTitle = Controller.Instance.MainForm.Text;
			Cursor = Cursors.WaitCursor;
			//Application.DoEvents(); // TODO Check if this is still required at end of fix

			if (Controller.Instance.CurrentProject.ProjectSettings == null || !Directory.Exists(Controller.Instance.CurrentProject.ProjectSettings.OutputPath))
			{
				MessageBox.Show(
						"The project output folder has not been set, is read only, or does not exist. Please set a valid folder now.",
						"Missing Folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				ChangeOutputPath();
				//if (ChangeOutputPath() == false)
				//{
				Cursor = Cursors.Default;
				return;
				//}
			}

			BusyPopulatingTreeNodes = true;
			if (treeList1.Nodes.Count > 0)
			{
				if (Controller.Instance.CurrentProject.AppConfigFilename != null && !Controller.Instance.CurrentProject.SelectedFilesHaveBeenSet)
				{
					Controller.Instance.LoadFileTreeStatus(treeModel);
					Controller.Instance.CurrentProject.SelectedFilesHaveBeenSet = true;
				}
				Controller.Instance.SaveFileTreeStatus(treeModel);
			}
			treeList1.Nodes.Clear();
			fatalTemplateExceptionMessageShown = false;
			PreGenerationStep();
		}

		private void SetupAnalysis()
		{
			if (InvokeRequired)
			{
				MethodInvoker mi = SetupAnalysis;
				Controller.SafeInvoke(this, mi, true);
				return;
			}
			BusyAnalysing = true;
		}

		#endregion

		#region Event Handlers

		private void treeModel_TreeNodeChanged(object sender, ProjectFileTreeChangedEventArgs e)
		{
			if (e.ChangedNode.IsFolder)
			{
				if (string.IsNullOrEmpty(e.ChangedNode.Path))
					log.Debug("Ignored change to folder node: " + e.ChangedNode.Text);
				else
					log.Debug("Ignored change to folder node: " + e.ChangedNode.Path);
				return;
			}

			if (treeNodes.ContainsKey(e.ChangedNode.AssociatedFile.RelativeFilePath) == false)
			{
				log.Warn("Could not find node for path " + e.ChangedNode.AssociatedFile.RelativeFilePath + ". Reloading entire tree.");
				ReloadEntireTree();
				return;
			}

			currentFocusedNode = null;
			Node updatedNode = treeNodes[e.ChangedNode.AssociatedFile.RelativeFilePath];
			UpdateTreeListNode(updatedNode, e.ChangedNode, e.ChildrenChanged);
		}

		private static string GetRelativePathForTreeNode(Node node)
		{
			if (node == null || node.Parent == null)
				return "";

			string path = node.Text;
			while (node.Parent != null)
			{
				node = node.Parent;
				if (node.Parent == null)
					break; // Ignore the Root node.
				path = Path.Combine(node.Text, path);
			}
			return path;
		}

		private Node GetTreeNodeForFilePath(string path)
		{
			if (treeList1.Nodes.Count == 0)
				return null;

			NodeCollection nodes = treeList1.Nodes[0].Nodes;
			Node nextNode = null;

			if (string.IsNullOrWhiteSpace(path))
				return null;

			string[] folders = path.Split(Path.DirectorySeparatorChar);

			int i = 0;
			if (string.IsNullOrEmpty(folders[0]))
				i = 1;

			for (; i < folders.Length; i++)
			{
				string folder = folders[i];
				if (string.IsNullOrEmpty(folder))
				{
					// The path is invalid.
					return null;
				}

				nextNode = null;

				foreach (Node node in nodes)
				{
					if (node.Text != folder) continue;

					nextNode = node;
					break;
				}
				if (nextNode == null)
				{
					// Could not find a node for the specified path.
					return null;
				}
				nodes = nextNode.Nodes;
			}
			return nextNode;
		}

		private void Project_OnProjectLoaded()
		{
			CancelCurrentTask(backgroundWorker1);
			Clear();
		}

		private void Controller_OnProjectModification()
		{
			if (Controller.Instance.IsDirty)
				UpdateNumGeneratedFilesLabel("Project Modified. Please save the project and refresh to regenerate the files.");
			//if (Controller.Instance.CreatingNewProject)
			//    return;
			//GenerateFiles();
		}

		private void Controller_UserFilesChanged(FileSystemEventArgs e)
		{
			string relativePath = Utility.RelativePathTo(Controller.Instance.CurrentProject.ProjectSettings.OutputPath, e.FullPath);

			if (Directory.Exists(e.FullPath))
				return;

			ProjectFileTreeNode node = treeModel.GetNodeAtPath(relativePath);

			if (HasFileGenerationFinished)
			{
				CancelCurrentTask(backgroundWorker1);

				if (node == null)
				{
					HasFileGenerationFinished = false;
					_ModifiedFiles.Clear();

					GenerateFiles();
				}
				else
				{
					if (node.IsFolder == false && node.AssociatedFile.IntelliMerge == IntelliMergeType.Overwrite)
					{
						// ignore files with overwrite set to true
						return;
					}
					_ModifiedFiles.Enqueue(relativePath);
					ConfirmProjectReload();
				}
			}
			else
			{
				if (backgroundWorker1.IsBusy)
				{
					if (node.IsFolder == false && node.AssociatedFile.IntelliMerge == IntelliMergeType.Overwrite)
					{
						// ignore files with overwrite set to true
						return;
					}
					_ModifiedFiles.Enqueue(relativePath);
					ConfirmProjectReload();
				}
				else
					GenerateFiles();
			}
		}

		private void ConfirmProjectReload()
		{
			if (InvokeRequired)
			{
				MethodInvoker invoker = ConfirmProjectReload;
				Controller.SafeInvoke(this, invoker, true);
				return;
			}

			// Since this is now happening on the UI thread, can we safely assume this part will never get run
			// by two threads at once?
			if (confirmProjectReloadCounter > 0)
				return;
			confirmProjectReloadCounter++;
			SlyceMessageBox messageBox = new SlyceMessageBox();
			messageBox.Caption = "User files have changed";
			messageBox.Message = "Files in your project directory have changed. Do you wish to reload and re-analyse them?";
			messageBox.ResultAvailable += MessageBox_UserChoseOption;
			messageBox.StartPosition = FormStartPosition.CenterParent;
			messageBox.Show(this);

		}

		private void MessageBox_UserChoseOption(object sender, ResultAvailableArgs e)
		{
			if (e.Result == DialogResult.Yes)
			{
				bool changedNodesChanged = false;
				foreach (string item in _ModifiedFiles)
				{
					ProjectFileTreeNode node = treeModel.GetNodeAtPath(item);
					if (node == null)
						continue;
					node.Status = ProjectFileStatusEnum.UnAnalysedFile;
					node.AssociatedFile.ReloadFiles = true;
					if (node.AssociatedFile.MergedFileExists)
					{
						changedNodesChanged = true;
					}
				}
				if (changedNodesChanged)
				{
					MessageBox.Show("You have changed some files in your project directory outside of ArchAngel, " +
								"but you have also worked with those files in ArchAngel. That work is inconsistent with the " +
								"changes you have made on disk, and will be lost.", "Work Lost", MessageBoxButtons.OK,
								MessageBoxIcon.Warning);
				}

				CancelCurrentTaskAndStartNew(backgroundWorker1, _RunBackgroundWorkerAnalyseDelegate, false);
			}
			confirmProjectReloadCounter = 0;
		}

		private void labelResolved_Click(object sender, EventArgs e)
		{
			ShowResolved = !ShowResolved;

			FilterNodes();
		}

		private void labelUnchanged_Click(object sender, EventArgs e)
		{
			ShowUnchanged = !ShowUnchanged;

			FilterNodes();
		}

		private void labelErrors_Click(object sender, EventArgs e)
		{
			ShowErrors = !ShowErrors;

			FilterNodes();
		}

		private void treeList1_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
		{
			UpdateEditorView();
			treeList1.Focus();
		}

		private void ucCodeMergeEditor_FileUpdated(object sender, FileUpdatedEventArgs e)
		{
			treeModel.GetNodeAtPath(e.UpdatedFile.RelativeFilePath).RaiseNodeChangedEvent();
		}

		private void ucTextMergeEditor_MergedFileSavedEvent(object sender, ucTextMergeEditor.MergedFileSavedEventArgs e)
		{
			treeModel.GetNodeAtPath(e.FileInformation.RelativeFilePath).RaiseNodeChangedEvent();
		}

		private void UpdateEditorView()
		{
			if (BusyPopulatingTreeNodes)
				return;

			if (currentFocusedNode != null && treeList1.SelectedNode == currentFocusedNode)
				return;

			if (treeList1.SelectedNodes.Count > 1)
				return;

			// Store the node we are working with now.
			currentFocusedNode = treeList1.SelectedNode;

			// If the node hasn't been analysed, move it to the top of the analysis queue and display nothing.
			if (BusyAnalysing && _AnalysisProgressHelper != null)
			{
				string relativePath = GetRelativePathForTreeNode(treeList1.SelectedNode);
				ProjectFileTreeNode node = treeModel.GetNodeAtPath(relativePath);

				if (node != null && node.Status == ProjectFileStatusEnum.UnAnalysedFile)
				{
					_AnalysisProgressHelper.ShiftToFrontOfQueue(node);
					currentFocusedNode = null;
					ClearEditorPanel();
					return;
				}

				if (node != null && node.Status == ProjectFileStatusEnum.Busy)
				{
					currentFocusedNode = null;
					ClearEditorPanel();
					return;
				}
			}
			try
			{
				Cursor = Cursors.WaitCursor;
				//Utility.SuspendPainting(this);
				//Slyce.Common.Utility.SuspendPainting(panelEditor);

				if (treeList1.SelectedNode == null)
				{
					ClearEditorPanel();
					return;
				}
				// If the path is not stored in the tag, calculate it.
				string path = treeList1.SelectedNode.Tag as string ?? CalculateNodePath(treeList1.SelectedNode);

				ProjectFileTreeNode node = treeModel.GetNodeAtPath(path);

				log.Info("Showing node at path " + path);

				if (node == null || node.AssociatedFile == null || node.AssociatedFile.IntelliMerge == IntelliMergeType.NotSet)
				{
					ClearEditorPanel();
					return;
				}

				//twEditor.Text = node.Path;

				if (node.AssociatedFile.CurrentDiffResult.DiffPerformedSuccessfully == false &&
					node.AssociatedFile.CurrentDiffResult.DiffType != TypeOfDiff.NewFile)
				{
					if (string.IsNullOrEmpty(node.AssociatedFile.CurrentDiffResult.ParserWarningDescription) == false)
					{
						// Remove the old editor if there is one.
						if (panelEditor.Controls.ContainsKey(OutputEditorKey) &&
							panelEditor.Controls[OutputEditorKey] is ucAnalysisErrorEditor)
						{
							((ucAnalysisErrorEditor)panelEditor.Controls[OutputEditorKey]).Reset(((TextFileInformation)node.AssociatedFile).UserFile, TemplateContentLanguage.CSharp);
						}
						else
						{
							ClearEditorPanel();

							ucAnalysisErrorEditor ee = new ucAnalysisErrorEditor(((TextFileInformation)node.AssociatedFile).UserFile, TemplateContentLanguage.CSharp);
							ee.Name = OutputEditorKey;
							ee.Dock = DockStyle.Fill;
							panelEditor.Controls.Add(ee);
						}
						log.Info("Node has a Parser warning.");
						return;
					}
					else if (node.Status == ProjectFileStatusEnum.GenerationError)
					{
						// Remove the old editor if there is one.
						ClearEditorPanel();
						ucGenerateErrorEditor ee = new ucGenerateErrorEditor(node.GenerationError.FileName, node.GenerationError.ErrorDescription);
						ee.Name = OutputEditorKey;
						ee.Dock = DockStyle.Fill;
						panelEditor.Controls.Add(ee);

						log.Info("Node has a Generation error.");
						return;
					}
					else if (node.Status == ProjectFileStatusEnum.MergeError)
					{
						ClearEditorPanel();
						ucMergeErrorEditor ge = new ucMergeErrorEditor(node.MergeError.ErrorDescription, node.MergeError.BaseConstructName, node.MergeError.BaseConstructType);
						ge.Name = OutputEditorKey;
						ge.Dock = DockStyle.Fill;
						panelEditor.Controls.Add(ge);
						log.Info("Node has a Merge Error.");
						return;
					}
					else
					{
						log.Info("Diff was not performed successfully, but no parser error was recorded.");
						ClearEditorPanel();
					}
					return;
				}
				if (node.AssociatedFile is TextFileInformation ||
					IsTextFile(node.AssociatedFile.RelativeFilePath))
				{
					if (panelEditor.Controls.ContainsKey(OutputEditorKey) &&
							!(panelEditor.Controls[OutputEditorKey] is ucSimpleDiffEditor))
					{
						ClearEditorPanel();
					}
					ShowTextMergeEditor(node, node.AssociatedFile.CurrentDiffResult);
					return;
				}
				else
				{
					if (panelEditor.Controls.ContainsKey(OutputEditorKey) &&
							panelEditor.Controls[OutputEditorKey] is ucBinaryMergeEditor)
					{
						((ucBinaryMergeEditor)panelEditor.Controls[OutputEditorKey]).Reset(node.AssociatedFile as BinaryFileInformation);
					}
					else
					{
						ClearEditorPanel();
						ucBinaryMergeEditor nc = new ucBinaryMergeEditor(node.AssociatedFile as BinaryFileInformation);
						nc.Name = OutputEditorKey;
						nc.Dock = DockStyle.Fill;
						panelEditor.Controls.Add(nc);
					}
					log.Info("Node is a binary file.");
					return;
				}
				//if (node.AssociatedFile is BinaryFileInformation)
				//{
				//   ucBinaryMergeEditor nc =
				//      new ucBinaryMergeEditor(node.AssociatedFile as BinaryFileInformation);
				//   nc.Name = OutputEditorKey;
				//   nc.Dock = DockStyle.Fill;
				//   panelEditorEmpty.Visible = false;
				//   twEditor.Controls.Add(nc);
				//   log.Info("Node is a binary file.");
				//   return;
				//}
				//else if (node.AssociatedFile is TextFileInformation)
				//{
				//   ShowTextMergeEditor(node, node.AssociatedFile.CurrentDiffResult);
				//   return;
				//}
				//else
				//{
				//   //throw new InvalidOperationException("Cannot visually diff binary files yet.");
				//   MessageBox.Show("Don't know what to do with a " + node.AssociatedFile.GetType());
				//   return;
				//}
			}
			finally
			{
				Cursor = Cursors.Default;
				//Utility.ResumePainting(this);
				//Slyce.Common.Utility.ResumePainting(panelEditor);
			}
		}

		private void ClearEditorPanel()
		{
			if (panelEditor.Controls.Count > 0)
				panelEditor.Controls.RemoveAt(0);
		}

		private bool IsTextFile(string filepath)
		{
			string ext = Path.GetExtension(filepath).ToLower().Replace(".", "");

			List<string> textExt = new List<string>(new string[] {
				"txt", 
				"cs", 
				"xml", 
				"html",
				"csproj",
				"config",
				"sln"})
			;
			return textExt.Contains(ext);
		}

		private void ShowTextMergeEditor(ProjectFileTreeNode node, DiffResult diffResult)
		{
			if (node.AssociatedFile.IntelliMerge == IntelliMergeType.Overwrite)
			{
				string oldFile = Slyce.Common.RelativePaths.GetFullPath(Controller.Instance.CurrentProject.ProjectSettings.OutputPath, node.AssociatedFile.RelativeFilePath);
				string oldFileContents = "";
				string newFileContents = "";
				bool oldFileExists = node.AssociatedFile.CurrentDiffResult.DiffType != TypeOfDiff.NewFile && File.Exists(oldFile);
				string md5new;
				string md5old = "";

				if (node.AssociatedFile is TextFileInformation)
				{
					try
					{
						newFileContents = ((TextFileInformation)node.AssociatedFile).NewGenFile.GetContents();
					}
					catch (Exception e)
					{
						newFileContents = "ERROR: " + e.Message;
					}
				}
				else if (node.AssociatedFile is BinaryFileInformation)
				{
					string tempFolder = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetTempFileName()));
					((BinaryFileInformation)node.AssociatedFile).WriteNewGenFile(tempFolder);
					string tempFile = Path.Combine(tempFolder, node.AssociatedFile.RelativeFilePath);
					newFileContents = File.ReadAllText(tempFile);
					Slyce.Common.Utility.DeleteFileBrute(tempFile);
				}
				else
				{
					throw new NotImplementedException("Unexpected file type");
				}
				if (oldFileExists)
				{
					oldFileContents = File.ReadAllText(oldFile);
					md5old = Slyce.Common.Utility.GetCheckSumOfString(oldFileContents);
				}
				md5new = Slyce.Common.Utility.GetCheckSumOfString(newFileContents);

				if (panelEditor.Controls.ContainsKey(OutputEditorKey))
					((ucSimpleDiffEditor)panelEditor.Controls[OutputEditorKey]).Reset(node.AssociatedFile.RelativeFilePath, oldFileExists, oldFileContents, newFileContents, md5new == md5old, ucSimpleDiffEditor.DisplayStyles.SingleEditor);
				else
				{
					ucSimpleDiffEditor se = new ucSimpleDiffEditor(node.AssociatedFile.RelativeFilePath, oldFileExists, oldFileContents, newFileContents, md5new == md5old, ucSimpleDiffEditor.DisplayStyles.SingleEditor);
					se.Name = OutputEditorKey;
					se.Dock = DockStyle.Fill;
					panelEditor.Controls.Add(se);
				}
			}
			return;
			TextFileInformation fileInfo = node.AssociatedFile as TextFileInformation;
			if (fileInfo == null) return;

			if (node.AssociatedFile.IntelliMerge == IntelliMergeType.Overwrite)
			{
				ucNoChangeEditor nc =
					 new ucNoChangeEditor(fileInfo);
				nc.Name = OutputEditorKey;
				nc.Dock = DockStyle.Fill;
				nc.TitleText = diffResult.DiffType != TypeOfDiff.ExactCopy
										 ? "Overwrite Mode active - Select IntelliMerge to see changes made"
										 : nc.TitleText;
				panelEditor.Controls.Add(nc);
				log.Info("Overwrite mode is turned on.");
			}
			else if (node.AssociatedFile.IntelliMerge == IntelliMergeType.CreateOnly)
			{
				ucNoChangeEditor nc =
					new ucNoChangeEditor(fileInfo);
				nc.Name = OutputEditorKey;
				nc.Dock = DockStyle.Fill;
				nc.TitleText = "Create Only mode active.";
				panelEditor.Controls.Add(nc);
				log.Info("Create Only mode is turned on.");
			}
			else if (node.AssociatedFile.IntelliMerge == IntelliMergeType.PlainText)
			{
				if (node.AssociatedFile.CurrentDiffResult.DiffType == TypeOfDiff.ExactCopy)
				{
					ucNoChangeEditor nc = new ucNoChangeEditor(fileInfo,
						fileInfo.CurrentDiffResult.DiffWarningDescription ?? fileInfo.CurrentDiffResult.ParserWarningDescription);
					nc.Name = OutputEditorKey;
					nc.Dock = DockStyle.Fill;
					panelEditor.Controls.Add(nc);
					log.Info("Plain Text file diff type is exact copy.");
				}
				else
				{
					ucTextMergeEditor te = new ucTextMergeEditor(fileInfo);
					te.Name = OutputEditorKey;
					te.Dock = DockStyle.Fill;
					te.EditingEnabled = true;
					panelEditor.Controls.Add(te);
					te.MergedFileSavedEvent += ucTextMergeEditor_MergedFileSavedEvent;
					te.TextSyntaxLanguage = Slyce.IntelliMerge.UI.Utility.GetSyntaxLanguageForFileInformation(fileInfo);
					log.Info("Node is plain text.");
				}
			}
			else if (node.Status == ProjectFileStatusEnum.AnalysisError)
			{
				ucAnalysisErrorEditor ee = new ucAnalysisErrorEditor(fileInfo.UserFile, TemplateContentLanguage.CSharp);
				ee.Name = OutputEditorKey;
				ee.Dock = DockStyle.Fill;
				panelEditor.Controls.Add(ee);
				log.Info("Node has an Analysis error.");
				return;
			}

			else
			{
				switch (diffResult.DiffType)
				{
					case TypeOfDiff.ExactCopy:
						ucNoChangeEditor nc =
							new ucNoChangeEditor(fileInfo, fileInfo.CurrentDiffResult.DiffWarningDescription ?? fileInfo.CurrentDiffResult.ParserWarningDescription);
						nc.Name = OutputEditorKey;
						nc.Dock = DockStyle.Fill;
						panelEditor.Controls.Add(nc);
						log.Info("Text file diff type is exact copy.");
						break;
					//case (int)NodeImages.StatusResolvedTemplateOnly:
					//case (int)NodeImages.StatusResolvedUserAndTemplate:
					//case (int)NodeImages.StatusResolvedUserOnly:
					case TypeOfDiff.Warning:
						if (fileInfo.CurrentDiffResult.DiffType == TypeOfDiff.Warning && fileInfo.UserFile.HasContents == false)
						{
							ucNoChangeEditor wnc =
							new ucNoChangeEditor(fileInfo, fileInfo.CurrentDiffResult.DiffWarningDescription ?? fileInfo.CurrentDiffResult.ParserWarningDescription);
							wnc.Name = OutputEditorKey;
							wnc.Dock = DockStyle.Fill;
							panelEditor.Controls.Add(wnc);
							log.Info("Text file diff type is warning because user file is missing.");
							break;
						}

						ucCodeMergeEditor wce = new ucCodeMergeEditor(fileInfo);
						wce.FileUpdated += ucCodeMergeEditor_FileUpdated;
						wce.Name = OutputEditorKey;
						wce.Dock = DockStyle.Fill;
						panelEditor.Controls.Add(wce);
						log.Info("Text file diff type is warning.");
						break;
					case TypeOfDiff.TemplateChangeOnly:
					case TypeOfDiff.UserChangeOnly:
					case TypeOfDiff.UserAndTemplateChange:
					case TypeOfDiff.Conflict:
						ucCodeMergeEditor ce = new ucCodeMergeEditor(fileInfo);
						ce.FileUpdated += ucCodeMergeEditor_FileUpdated;
						ce.Name = OutputEditorKey;
						ce.Dock = DockStyle.Fill;
						panelEditor.Controls.Add(ce);
						log.Info("Text file diff type is " + diffResult.DiffType);
						break;
					default:
						ClearEditorPanel();
						log.Info("Default case: Text file diff type is " + diffResult.DiffType);
						break;
				}
			}
		}

		private static string CalculateNodePath(Node treeListNode)
		{
			if (treeListNode.Parent != null)
				return treeListNode.Text + CalculateNodePath(treeListNode.Parent);



			return treeListNode.Text;
		}

		private void treeList1_BeforeCollapse(object sender, AdvTreeNodeCancelEventArgs e)
		{
			SetFolderImage(e.Node);
		}

		private void treeList1_BeforeExpand(object sender, AdvTreeNodeCancelEventArgs e)
		{
			SetFolderImage(e.Node);
		}

		private bool OverrideCodeEventCheck;
		private FileController fileController = new FileController();
		private bool fatalTemplateExceptionMessageShown = false;

		void treeList1_BeforeCheck(object sender, AdvTreeCellBeforeCheckEventArgs e)
		{
			try
			{
				if (e.Action == eTreeAction.Code && !OverrideCodeEventCheck)
					return;

				treeList1.BeginUpdate();

				OverrideCodeEventCheck = false;

				Controller.Instance.IsDirty = true;

				// Override user changes that set checkboxes to indeterminate.
				if (e.NewCheckState == CheckState.Indeterminate)
				{
					OverrideCodeEventCheck = true;
					e.Cell.CheckState = e.Cell.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
					e.Cancel = true;
					return;
				}

				if (e.Cell.Name == FilenameCellKey || e.Cell.Name == "")
				{
					string relativePath = GetRelativePathForTreeNode(e.Cell.Parent);
					ProjectFileTreeNode node = treeModel.GetNodeAtPath(relativePath);
					// If the node is checked, set Checked to true. If it is Unchecked, set it to false. If it is indeterminite, set it to checked.
					bool checkedValue = e.NewCheckState == CheckState.Checked
												? true
												: e.NewCheckState == CheckState.Unchecked ? false : true;
					if (node != null)
					{
						node.NodeSelected = checkedValue;
					}
					else
					{
						treeModel.NodeSelected = checkedValue;
					}
					UpdateNodeCheckedStatus(treeList1.Nodes[0]);
				}
				else if (e.Cell.Name == IntelliMergeCellKey)
				{
					if (e.Cell.Parent.Parent == null)
					{
						// Root node
						try
						{
							Cursor = Cursors.WaitCursor;
							treeModel.SetIntelliMergeOnEntireTree(e.NewCheckState == CheckState.Unchecked);
						}
						finally
						{
							Cursor = Cursors.Default;
						}

						UpdateCheckStates(treeList1.Nodes[0]);
						return;
					}
					string relativePath = GetRelativePathForTreeNode(e.Cell.Parent);
					ProjectFileTreeNode node = treeModel.GetNodeAtPath(relativePath);

					if (node == null) return;

					if (node.IsFolder == false && node.AssociatedFile != null)
					{
						node.AssociatedFile.IntelliMerge = e.NewCheckState == CheckState.Unchecked ?
							IntelliMergeType.AutoDetect : IntelliMergeType.Overwrite;
						node.Status = ProjectFileStatusEnum.UnAnalysedFile;
						AnalyseFiles();
					}
					else
					{
						// Node is a folder. Deal to it.
						if (e.NewCheckState == CheckState.Checked)
							node.SetIntelliMergeOnSelfAndAllChildren(IntelliMergeType.Overwrite);
						else if (e.NewCheckState == CheckState.Unchecked)
							node.SetIntelliMergeOnSelfAndAllChildren(IntelliMergeType.AutoDetect);
					}
					UpdateCheckStates(treeList1.Nodes[0]);
				}
			}
			finally
			{
				treeList1.EndUpdate();
			}
		}

		//private void treeList1_BeforeCellEdit(object sender, CellEditEventArgs e)
		//{
		//    if (e.Cell.Name == FilenameCellKey)
		//    {
		//        string relativePath = GetRelativePathForTreeNode(e.Cell.Parent);
		//        ProjectFileTreeNode node = treeModel.GetNodeAtPath(relativePath);
		//        node.NodeSelected = e.Cell.Checked;
		//    }
		//    else if (e.Cell.Name == IntelliMergeCellKey)
		//    {
		//        if (e.Cell.Parent.Parent == null)
		//        {
		//            // Root node
		//            try
		//            {
		//                Cursor = Cursors.WaitCursor;
		//                treeModel.SetIntelliMergeOnEntireTree(e.Cell.Checked);
		//            }
		//            finally
		//            {
		//                Cursor = Cursors.Default;
		//            }
		//            return;
		//        }
		//        string relativePath = GetRelativePathForTreeNode(e.Cell.Parent);
		//        ProjectFileTreeNode node = treeModel.GetNodeAtPath(relativePath);

		//        if (node == null) return;

		//        if (node.IsFolder == false && node.AssociatedFile != null)
		//        {
		//            if (e.Cell.Checked)
		//                node.AssociatedFile.IntelliMerge = IntelliMergeType.AutoDetect;
		//            else
		//                node.AssociatedFile.IntelliMerge = IntelliMergeType.Overwrite;
		//            node.Status = ProjectFileStatusEnum.UnAnalysedFile;
		//            AnalyseFiles();
		//        }
		//    }
		//}

		#endregion

		private bool WriteFilesToUsersProjectFolder(bool overwriteExistingFiles)
		{
			Controller.Instance.WritingToUserFolder = true;
			Controller.Instance.MainForm.Cursor = Cursors.WaitCursor;
			Cursor = Cursors.WaitCursor;
			UpdateNumGeneratedFilesLabel("Writing Files To Disk");
			Refresh();

			try
			{
				List<IFileInformation> files = GetListOfFilesToWrite(treeModel, overwriteExistingFiles);
				WriteOutHelper helper = new WriteOutHelper();
				helper.WriteAllFiles(TaskProgressHelper.NullHelper, Controller.Instance, files, treeModel, overwriteExistingFiles);

				if (MessageBox.Show("File output complete.\n\nOpen project folder to view?", "Finished", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					Process.Start(Controller.Instance.CurrentProject.ProjectSettings.OutputPath);

				UpdateNumGeneratedFilesLabel("Finished Writing Files To Disk");

				Application.DoEvents();

				Controller.Instance.MainForm.Cursor = Cursors.WaitCursor;
				treeModel.TreeRestructuring = true;

				foreach (ProjectFileTreeNode node in treeModel.AllNodes)
				{
					if (node.Status == ProjectFileStatusEnum.AnalysedFile)
						node.Status = ProjectFileStatusEnum.UnAnalysedFile;
				}
				treeModel.LoadUserFiles(Controller.Instance);
				treeModel.TreeRestructuring = false;

				Controller.Instance.MainForm.Cursor = Cursors.Default;

				return true;
			}
			catch (WriteOutException e)
			{
				MessageBox.Show(this, e.Message, "Error Writing To Files");
				return false;
			}
			//catch (Exception ex)
			//{
			//    Controller.ReportError(ex);
			//    return false;
			//}
			finally
			{
				Controller.Instance.MainForm.Cursor = Cursors.Default;
				Cursor = Cursors.Default;
				Controller.Instance.WritingToUserFolder = false;
			}
		}

		#region Properties

		private bool ShowNewFiles
		{
			get { return highlighter1.GetHighlightColor(labelNewFiles) != eHighlightColor.None; }
			set { SetFilterLabelColour(labelNewFiles, value); }
		}

		private bool ShowErrors
		{
			get { return highlighter1.GetHighlightColor(labelErrors) != eHighlightColor.None; }
			set { SetFilterLabelColour(labelErrors, value); }
		}

		private bool ShowResolved
		{
			get { return highlighter1.GetHighlightColor(labelChangedFiles) != eHighlightColor.None; }
			set { SetFilterLabelColour(labelChangedFiles, value); }
		}

		private bool ShowUnchanged
		{
			get { return highlighter1.GetHighlightColor(labelUnchangedFiles) != eHighlightColor.None; }
			set { SetFilterLabelColour(labelUnchangedFiles, value); }
		}

		internal IScriptBaseObject CurrentRootObject
		{
			get { return _CurrentRootObject; }
			set { _CurrentRootObject = value; }
		}

		#endregion

		#region Content Item Overrides

		public override bool Back()
		{
			return true;
		}

		public override bool Next()
		{
			return WriteFilesToDisk();
		}

		public bool WriteFilesToDisk()
		{
			if (!ObjectModelIsValid())
			{
				return false;
			}
			bool overwriteExistingFiles = !checkBoxPerformAnalysis.Checked;

			if (treeModel.AllNodesAnalysed(overwriteExistingFiles) == false)
			{
				MessageBox.Show("You need to let the analysis finish before you can output the files.");
				return false;
			}

			if (treeModel.AllChangesResolved(overwriteExistingFiles) == false)
			{
				MessageBox.Show(
					"You need to make sure all changes are resolved, or that files with unresolved changes are excluded from the build.");
				return false;
			}
			Controller.Instance.SaveFileTreeStatus(treeModel);

			if (HasFileAnalysisFinished)
			{
				bool result = WriteFilesToUsersProjectFolder(overwriteExistingFiles);

				// Reanalyse the files. Should just check the MD5s and decide nothing has changed.
				if (result)
				{
					AnalyseFiles();
				}

				return result;
			}

			MessageBox.Show(this, "File have not been generated yet. Click the 'Re-generate' button first.", "No files", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return false;
		}

		public override void OnDisplaying()
		{
			PerformLayout();
			Refresh();
			//Application.DoEvents();
		}

		#endregion

		#region BackgroundWorker Methods

		/// <summary>
		/// Runs the Generate Files task. If there is another task currently running, it is cancelled.
		/// </summary>
		public void GenerateFiles()
		{
			bool invalid = false;
			ClearEditorPanel();
			StringBuilder failures = new StringBuilder();
			failures.AppendLine("The following providers are in an invalid state:");

			foreach (ProviderInfo provider in Controller.Instance.CurrentProject.Providers)
			{
				string failReason;

				try
				{
					Controller.Instance.BusyPopulating = true;

					if (provider.IsValid(out failReason) == false)
					{
						failures.AppendFormat("{0}: {1}{2}", provider.Name, failReason, Environment.NewLine);
						invalid = true;
					}
				}
				finally
				{
					Controller.Instance.BusyPopulating = false;
				}
			}
			if (invalid)
			{
				Utility.DisplayMessagePanel(panelGeneratedFiles, "File generation failed!", Slyce.Common.Controls.MessagePanel.ImageType.Alarm);
				Utility.UpdateMessagePanelStatus(panelGeneratedFiles, failures.ToString());
				CancelCurrentTask(backgroundWorker1);
				return;
			}

			bool validationFailed = false;

			foreach (var provider in Controller.Instance.CurrentProject.Providers)
			{
				var validationResults = provider.RunPreGenerationValidation();
				if (validationResults.ValidationFailed == false) continue;

				validationFailed = true;
				CancelCurrentTask(backgroundWorker1);

				//if (validationResults.ScreenToShow == null)
				//{
				//    Utility.DisplayMessagePanel(panelGeneratedFiles, "Model Validation Failed in Provider " + provider.Name, Slyce.Common.Controls.MessagePanel.ImageType.Alarm);
				//}
				//else
				//{
				//    Controller.Instance.MainForm.ShowContentItem(validationResults.ScreenToShow);
				//}
				Utility.DisplayMessagePanel(panelGeneratedFiles, "Model validation failed", "See model screen", Slyce.Common.Controls.MessagePanel.ImageType.Alarm);
				Controller.Instance.MainForm.ShowContentItemByName("Model");
				MessageBox.Show(this, "Model validation failed", "Invalid model", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			if (!validationFailed)
			{
				Utility.DisplayMessagePanel(panelGeneratedFiles, "Busy generating files...");
				//Controller.Instance.ShowWaitDialog("Busy generating files...", panelGeneratedFiles);
				CancelCurrentTaskAndStartNew(backgroundWorker1, _RunBackgroundWorkerGenerateDelegate, false);
			}
		}

		/// <summary>
		/// Runs the Analyse Files task. If there is another task currently running, it is cancelled.
		/// </summary>
		public void AnalyseFiles()
		{
			CancelCurrentTaskAndStartNew(backgroundWorker1, _RunBackgroundWorkerAnalyseDelegate, false);
		}

		/// <summary>
		/// This method will run start the background worker on the Generate Files task,
		/// starting the run process from the Gui thread. This is important, because the 
		/// background worker progress reports seem to be executed on the thread that called
		/// RunWorkerAsync. The progress reports update the Gui, so to prevent cross threading
		/// issues, the background worker needs to be run from the Gui thread.
		/// </summary>
		private void RunBackgroundWorkerGenerateFiles()
		{
			if (InvokeRequired)
				Controller.SafeInvoke(this, _RunBackgroundWorkerGenerateDelegate, true);
			else
			{
				treeList1.SelectedNode = null;
				UpdateEditorView();
				backgroundWorker1.RunWorkerAsync(GenerateFilesAsyncKey);
				Refresh();
			}
		}

		/// <summary>
		/// This method will run start the background worker on the Analyse Files task,
		/// starting the run process from the Gui thread. This is important, because the 
		/// background worker progress reports seem to be executed on the thread that called
		/// RunWorkerAsync. The progress reports update the Gui, so to prevent cross threading
		/// issues, the background worker needs to be run from the Gui thread.
		/// </summary>
		private void RunBackgroundWorkerAnalyseFiles()
		{
			if (InvokeRequired)
				Controller.SafeInvoke(this, _RunBackgroundWorkerAnalyseDelegate, true);
			else
			{
				backgroundWorker1.RunWorkerAsync(StartAnalysisKey);
			}
		}

		public void CancelCurrentTask()
		{
			if (InvokeRequired)
			{
				Controller.SafeInvoke(this, new MethodInvoker(CancelCurrentTask), true);
				return;
			}
			CancelCurrentTask(backgroundWorker1);
		}

		/// <summary>
		/// If the supplied BackgroundWorker is running, cancels it and
		/// waits for the RunWorkerCompleted event before returning.
		/// </summary>
		/// <param name="backgroundWorker"></param>
		private void CancelCurrentTask(BackgroundWorker backgroundWorker)
		{
			// We need to remove the event handler from background worker before running the
			// cancellation process. Otherwise we block waiting, and the event handlers will
			// never run, blocking the cancellation. In short, this is deadlock prevention.
			backgroundWorker1.RunWorkerCompleted -= backgroundWorker1_RunWorkerCompleted;
			backgroundWorker1.ProgressChanged -= backgroundWorker1_ProgressChanged;
			backgroundWorker1.DoWork -= backgroundWorker1_DoWork;

			CancelCurrentTaskAndStartNew(backgroundWorker, null, true);

			// Force the cleanup code to run.
			backgroundWorker1_RunWorkerCompleted(backgroundWorker1, new RunWorkerCompletedEventArgs(null, null, true));

			backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
			backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
			backgroundWorker1.DoWork += backgroundWorker1_DoWork;
		}

		/// <summary>
		/// If the supplied BackgroundWorker is running, cancels it and waits
		/// for the RunWorkerCompleted event. Once that has occurred, it starts
		/// the BackgroundWorker again.
		/// </summary>
		/// <param name="worker">The BackgroundWorker to cancel.</param>
		/// <param name="newTask">The new task to run. If this is null, then worker will be cancelled but no new task will be executed.</param>
		/// <param name="wait">If true, will wait until the task is finished befoer returning.</param>
		private void CancelCurrentTaskAndStartNew(BackgroundWorker worker, RunBackgroundWorkerDelegate newTask, bool wait)
		{
			Thread t = new Thread(
				new ThreadStart(
					delegate
					{
						Monitor.Enter(_CancelBackgroundWorkerLock);
						AutoResetEvent waitHandle = new AutoResetEvent(false);
						if (worker.IsBusy)
						{
							RunWorkerCompletedEventHandler cancelledDelegate =
								delegate
								{
									waitHandle.Set();
								};
							// The last event handler to be added is the last one to be called,
							// so any cleanup code the worker has in other run worker completed
							// methods is called first.
							worker.RunWorkerCompleted += cancelledDelegate;
							worker.CancelAsync();
							waitHandle.WaitOne(10000, false);
							worker.RunWorkerCompleted -= cancelledDelegate;
						}

						if (!worker.IsBusy && newTask != null)
						{
							newTask.Invoke();
						}
						Monitor.Exit(_CancelBackgroundWorkerLock);
					}));
			t.Start();

			if (!wait) return;

			// We can't just call t.Join(). It doesn't do what it says on the box - the gui message pump
			// does not continue to work while this code blocks. 
			while (t.IsAlive)
			{
				Application.DoEvents();
				Thread.Sleep(10);
			}
		}

		private void GenerateFilesChangeEvent(object state)
		{
			if (state == null) return;

			if (InvokeRequired)
			{
				Controller.SafeInvoke(this, new MethodInvoker(() => GenerateFilesChangeEvent(state)), false);
				return;
			}

			QueueingTaskProgressHelper<GenerateFilesProgress> helper = (QueueingTaskProgressHelper<GenerateFilesProgress>)state;


			ProcessChangeMessages(helper);
		}

		private void ProcessChangeMessages<T>(QueueingTaskProgressHelper<T> helper) where T : class
		{
			foreach (var obj in helper.DequeueAllItems())
			{
				if (obj == null) continue;

				backgroundWorker1_ProgressChanged(null, new ProgressChangedEventArgs(40, obj.UserState));
			}
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			HasFileAnalysisFinished = false;

			//if (Environment.ProcessorCount == 1)
			{
				Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
			}

			switch ((string)e.Argument)
			{
				case GenerateFilesAsyncKey:
					QueueingTaskProgressHelper<GenerateFilesProgress> progressHelper = new QueueingTaskProgressHelper<GenerateFilesProgress>(backgroundWorker1);

					using (new Timer(GenerateFilesChangeEvent, progressHelper, 50, 50))
					{
						HasFileGenerationFinished = false;
						SetupPopulateFiles();

						if (SharedData.CurrentProject == null)
							return;

						IOutput combinedOutput = SharedData.CurrentProject.CombinedOutput;

						if (combinedOutput == null)
							return;

						if (string.IsNullOrEmpty(Controller.Instance.CurrentProject.ProjectSettings.OutputPath))
							return;

						if (progressHelper.IsCancellationPending())
						{
							break;
						}
						Controller.Instance.CurrentProject.ProjectSettings.OverwriteFiles = !checkBoxPerformAnalysis.Checked;
						ArchAngel.Interfaces.SharedData.CurrentProject.InitialiseProvidersPreGeneration();

						// Utility.DisplayMessagePanel(panelGeneratedFiles, "Initialization complete...");

						//GenerationHelper generator = new GenerationHelper(progressHelper, Controller.Instance.CurrentProject.TemplateLoader, SharedData.CurrentProject, fileController);
						//// Reset the progress counter. Needed for the AddFileCount... method to work.
						//progressHelper.ReportProgress(0, new GenerateFilesProgress(0));

						//generator.GenerateAllFiles("", combinedOutput.RootFolder, treeModel, null,
						//                                Controller.Instance.GetTempFilePathForComponent(ComponentKey.Workbench_FileGenerator));

						string exeDir = Path.GetDirectoryName(Application.ExecutablePath);
						List<string> referencedAssemblies = new List<string>();
						//referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.Scripting.dll"));
						referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.Interfaces.dll"));
						referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.Providers.EntityModel.dll"));
						referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.NHibernateHelper.dll"));
#if DEBUG
						string path = Slyce.Common.RelativePaths.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location, @"..\..\..\..\3rd_Party_Libs\Inflector.Net.dll");
						referencedAssemblies.Add(path);
#else
						referencedAssemblies.Add(Path.Combine(exeDir, "Inflector.Net.dll"));
#endif

						List<System.CodeDom.Compiler.CompilerError> compileErrors;
						string temp = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Branding.ProductName + Path.DirectorySeparatorChar + "Temp"), "Compile");

						if (!Directory.Exists(temp))
							Directory.CreateDirectory(temp);

						temp = Path.Combine(temp, Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".dll");

						if (ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject == null)
							ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject = ArchAngel.Common.UserTemplateHelper.GetDefaultTemplate();

						ArchAngel.Common.Generator gen = new ArchAngel.Common.Generator(
							progressHelper,
							Controller.Instance.CurrentProject.TemplateLoader);

						System.Reflection.Assembly baseAssembly = gen.CompileCombinedAssembly(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject,
							referencedAssemblies,
							null,
							out compileErrors,
							true,
							temp);

						if (compileErrors.Count > 0)
						{
							Controller.Instance.RaiseCompileErrors(compileErrors);
							//progressHelper.ReportProgress(100, new GenerateFilesProgress(0, new Controller.CompileErrorsDelegate(compileErrors)));
							break;
						}
						//gen.SetProjectInCode(baseAssembly, ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject);

						List<FilenameInfo> duplicateFiles;
						string targetFolder = Controller.Instance.GetTempFilePathForComponent(ComponentKey.Workbench_FileGenerator);

						if (!Directory.Exists(targetFolder))
							Directory.CreateDirectory(targetFolder);
						else
							Slyce.Common.Utility.DeleteDirectoryContentsBrute(targetFolder);

						// Copy all files and sub-folders from real output folder to the temp folder
						try
						{
							//GFH
							//Slyce.Common.IOHelper.CopyDirectory(Controller.Instance.CurrentProject.ProjectSettings.OutputPath, targetFolder, false);
						}
						catch (Exception ex)
						{
							progressHelper.ReportProgress(100, new GenerateFilesProgress(0, ex));
						}
						Controller.Instance.CurrentProject.ScriptProject.OutputFolder = Controller.Instance.CurrentProject.ProjectSettings.OutputPath;
						Controller.Instance.CurrentProject.ScriptProject.TempFolder = targetFolder;

						//ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.OutputFolder = targetFolder;

						int numFiles = gen.WriteFiles(
							treeModel,
							Controller.Instance.CurrentProject.ScriptProject,
							targetFolder,
							ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject,
							out duplicateFiles);

						if (duplicateFiles.Count > 0)
							progressHelper.ReportProgress(100, new GenerateFilesProgress(0, new DuplicateFilesException(duplicateFiles)));
						else
							progressHelper.ReportProgress(50, new GenerateFilesProgress(numFiles));
					}

					ProcessChangeMessages(progressHelper);
					break;
				case StartAnalysisKey:
					AnalysisProgressHelper analysisProgressHelper = new AnalysisProgressHelper((BackgroundWorker)sender, e);
					if (HasFileGenerationFinished == false)
					{
						GenerateFiles();
						return;
					}
					//Utility.HideMessagePanel(panelGeneratedFiles);
					//List<ProjectFileTreeNode> nodes = OutputHelper.GetListOfNodesToProcess(treeModel, e.Argument.ToString() == StartAnalysisOfMarkedFilesKey);

					if (checkBoxPerformAnalysis.Checked)
					{
						SetupAnalysis();
						AnalysisHelper helper = new AnalysisHelper();
						helper.StartAnalysis(analysisProgressHelper, Controller.Instance, treeModel);
						//OutputHelper.StartAnalysis(this, analysisProgressHelper, nodes);
					}
					break;
				default:
					throw new NotImplementedException("Not handled yet: " + (string)e.Argument);
			}
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (e.UserState != null)
			{
				if (e.UserState is GenerateFilesProgress)
				{
					GenerateFilesProgress state = (GenerateFilesProgress)e.UserState;
					if (state.FatalErrorOccurred && state.Exception is PathTooLongException)
					{
						backgroundWorker1.CancelAsync();
					}
					ProcessGenerateFilesProgressState(state);
				}
				else if (e.UserState is AnalyseFilesProgress)
				{
					AnalyseFilesProgress afp = (AnalyseFilesProgress)e.UserState;
					SetNumFilesToBeAnalysed(afp.NumberOfFilesLeftToAnalyse);
					UpdateStatusButtons(afp.NumberOfConflicts, afp.NumberOfResolved, afp.NumberOfExactCopies, afp.NumberOfErrors);
				}
				else if (e.UserState is AnalysisProgressHelper)
				{
					_AnalysisProgressHelper = e.UserState as AnalysisProgressHelper;
				}
				else if (e.UserState is Node)
				{
					if (treeList1.SelectedNode == e.UserState as Node)
					{
						treeList1.SelectedNode = treeList1.SelectedNode;
					}
				}
			}
		}

		private void ProcessGenerateFilesProgressState(GenerateFilesProgress state)
		{
			if (InvokeRequired)
			{
				Controller.SafeInvoke(this, new MethodInvoker(() => ProcessGenerateFilesProgressState(state)), false);
				return;
			}
			if (state.FatalErrorOccurred)
			{
				if (state.Exception is OldVersionException)
				{
					fatalTemplateExceptionMessageShown = true;
					log.Fatal(state.Exception.Message);
					MessageBox.Show(this, "The template was compiled with an older version of ArchAngel that is no longer supported. "
										  + "Please recompile it with ArchAngel Designer, or you will be unable to generate files from it.", "Recompile your template");
				}
				else if (state.Exception is PathTooLongException)
				{
					log.Fatal(state.Exception.Message);
				}
				else if (state.Exception is DuplicateFilesException)
				{
					DuplicateFilesException e = state.Exception as DuplicateFilesException;

					duplicatedFiles.Clear();
					duplicatedFiles.AddRange(e.DuplicateFiles);

					treeListDuplicatedFiles.Nodes.Clear();
					treeListDuplicatedFiles.ImageList = imageListNodes;

					foreach (var fileGroup in duplicatedFiles.GroupBy(f => f.ProcessedFilename))
					{
						Node node = new Node();
						node.Cells.Add(new Cell());

						node.Text = fileGroup.Key;
						node.ImageIndex = (int)NodeImages.Error;

						foreach (var file in fileGroup)
						{
							Node childNode = new Node();
							childNode.Text = string.Format("{0} :: [{1}]", file.RawFilename, file.FilenameType);
							node.Nodes.Add(childNode);

							string iteratorText = file.IteratorObject == null ? "NO ITERATOR" : string.Format("ITERATOR [{0}]: {1}", file.IteratorObject.GetType().Name, file.IteratorObject.ToString());
							Node iteratorNode = new Node();
							iteratorNode.Text = iteratorText;
							childNode.Nodes.Add(iteratorNode);
						}
						treeListDuplicatedFiles.Nodes.Add(node);
					}

					ShowDuplicatedFilesPane();
				}
				else
				{
					log.Fatal(state.Exception.Message);
					if (fatalTemplateExceptionMessageShown == false)
					{
						fatalTemplateExceptionMessageShown = true;
						MessageBox.Show(this, state.Exception.Message, "A fatal exception occurred while running the template");
					}
				}
			}
			else
			{
				SetNumGeneratedFiles(state.NumberOfFilesGenerated);
			}
		}

		private void ShowDuplicatedFilesPane()
		{
			panelGenerationErrors.Left = treeList1.Left;
			panelGenerationErrors.Top = treeList1.Top;
			panelGenerationErrors.Width = treeList1.Width;
			panelGenerationErrors.Height = treeList1.Height;
			panelGenerationErrors.Visible = true;
		}

		private void HideDuplicatedFilesPane()
		{
			panelGenerationErrors.Visible = false;
			panelGenerationErrors.Left += 40;
			panelGenerationErrors.Top += 40;
			panelGenerationErrors.Width = treeList1.Width;
			panelGenerationErrors.Height = treeList1.Height;
			treeList1.Refresh();
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (InvokeRequired)
			{
				Controller.SafeInvoke(this, new MethodInvoker(() => backgroundWorker1_RunWorkerCompleted(sender, e)), true);
				return;
			}

			_AnalysisProgressHelper = null;
			bool wasGenerateRun = BusyPopulatingTreeNodes;

			if (e.Cancelled || e.Error != null)
			{
				BusyPopulatingTreeNodes = false;
				SharedData.IsBusyGenerating = false;
				panelGeneratedFiles.Visible = true;

				Cursor = Cursors.Default;
				Controller.Instance.MainForm.Cursor = Cursors.Default;
				Controller.Instance.MainForm.Text = OriginalTitle;
				FilterNodes();

				if (e.Error != null)
					throw new Exception("Error during Generation/Analysis.", e.Error);

				return;
			}

			if (wasGenerateRun)
			{
				SharedData.IsBusyGenerating = false;

				// We used to inform the user when an error occurred. This doesn't need to happen anymore because
				// the error icons are shown all the way up the tree, and an error count is given at the top of the screen.
				//if (errorOccurredInTemplate)
				//{
				//    MessageBox.Show("An error occurred in one or more of the files generated by the template.\n" +
				//        "They have been marked with an error indicator.\n" +
				//        "These files will be excluded from the template build. Please inform the template author of these errors.",
				//        "Error occurred during file generation", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//}

				ReloadEntireTree();

				Application.DoEvents();

				treeList1.BeginUpdate();

				ThreadStart ts =
				() =>
				{
					// Load the tree model with the user and prevgen versions of the generated files.
					//treeModel.LoadPrevGenFiles(Controller.Instance);
					treeModel.LoadUserFiles(Controller.Instance);
				};
				Thread t = new Thread(ts);
				t.Start();
				t.Join();

				BusyPopulatingTreeNodes = false;

				SelectFirstFileNode();

				if (e.Cancelled == false)
					HasFileGenerationFinished = true;
				panelGeneratedFiles.Visible = true;
				treeList1.EndUpdate();
			}
			BusyAnalysing = false;
			UpdateStatusButtons(false);
			treeList1.Invalidate();
			Cursor = Cursors.Default;
			Controller.Instance.MainForm.Cursor = Cursors.Default;
			Controller.Instance.MainForm.Text = OriginalTitle;

			if (e.Cancelled == false && wasGenerateRun)
			{
				if (checkBoxPerformAnalysis.Checked)
					Utility.DisplayMessagePanel(panelGeneratedFiles, "Analysing files for changes...");
				else
					Utility.DisplayMessagePanel(panelGeneratedFiles, "Skipping analysis...");

				//Utility.UpdateMessagePanelStatus(panelGeneratedFiles, "Analysing files for changes...");
				AnalyseFiles();
			}
			else
			{
				FinishedGenerationAndAnalysis();
				HasFileAnalysisFinished = true;
			}

			FilterNodes();

			Application.DoEvents();

			if (!wasGenerateRun)
				Utility.HideMessagePanel(panelGeneratedFiles);
		}

		#endregion

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			Node treeNode = treeList1.SelectedNode;

			if (treeNode == null)
			{
				e.Cancel = true;
				return;
			}

			string path = GetRelativePathForTreeNode(treeNode);

			if (string.IsNullOrEmpty(path))
			{
				e.Cancel = true;
				return;
			}

			ProjectFileTreeNode fileNode = treeModel.GetNodeAtPath(path);
			if (fileNode == null)
			{
				e.Cancel = true;
				return;
			}

			if (fileNode.AssociatedFile == null)
			{
				e.Cancel = true;
				return;
			}

			string filePath = Path.Combine(Controller.Instance.CurrentProject.ProjectSettings.OutputPath, fileNode.Path);
			if (!File.Exists(filePath))
			{
				toolStripMenuItemViewFileDefault.Enabled = false;
				toolStripMenuItemViewfileInExplorer.Enabled = false;
			}
			else
			{
				toolStripMenuItemViewFileDefault.Enabled = true;
				toolStripMenuItemViewfileInExplorer.Enabled = true;
			}
		}

		private void toolStripMenuItemViewFileDefault_Click(object sender, EventArgs e)
		{
			Node treeNode = treeList1.SelectedNode;
			string path = GetRelativePathForTreeNode(treeNode);
			ProjectFileTreeNode fileNode = treeModel.GetNodeAtPath(path);
			if (fileNode == null)
			{
				return;
			}
			string filePath = Path.Combine(Controller.Instance.CurrentProject.ProjectSettings.OutputPath, fileNode.Path);

			try
			{
				Process.Start(filePath);
			}
			catch (Win32Exception)
			{
				MessageBox.Show(this, "Could not open file as no default editor has been set in Windows.", "Could not open file.",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (FileNotFoundException)
			{
				MessageBox.Show(this,
								"An error occurred while trying to open the file on disk. Have you deleted it from your user directory?",
								"Could not open file", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void toolStripMenuItemViewfileInExplorer_Click(object sender, EventArgs e)
		{
			Node treeNode = treeList1.SelectedNode;
			string path = GetRelativePathForTreeNode(treeNode);
			ProjectFileTreeNode fileNode = treeModel.GetNodeAtPath(path);
			if (fileNode == null)
			{
				return;
			}
			string filePath = Path.Combine(Controller.Instance.CurrentProject.ProjectSettings.OutputPath, fileNode.Path);
			Process.Start("explorer.exe", "/select,\"" + filePath + "\"");
		}

		internal static bool ObjectModelIsValid()
		{
			StringBuilder sb = new StringBuilder(100);
			bool isInvalid = false;

			if (Controller.Instance.CurrentProject.Providers.Count == 0)
			{
				return false;
			}
			foreach (ProviderInfo provider in Controller.Instance.CurrentProject.Providers)
			{
				string tempString;

				if (!provider.IsValid(out tempString))
				{
					isInvalid = true;
					sb.AppendLine(tempString);
				}
			}
			if (isInvalid)
			{
				MessageBox.Show("The properties of some objects in the object model are invalid. Generation cannot continue until you fix these issues: \n\n" + sb, "Invalid Data");
			}
			return !isInvalid;
		}

		/// <summary>
		/// This method is supposed to run before the file generation takes place. It takes care of setting up shared data,
		/// like the Loader, MainForm and Project singletons, and setting up directories. Any Output related setup code
		/// should go in Output.SetupPopulateFiles(). This must be run from the same thread as the Output control.
		/// </summary>
		internal static void PreGenerationStep()
		{
			if (!Directory.Exists(Controller.Instance.GetTempFilePathForComponent(ComponentKey.Workbench_FileGenerator)))
			{
				Directory.CreateDirectory(Controller.Instance.GetTempFilePathForComponent(ComponentKey.Workbench_FileGenerator));
			}

			// TODO: Figure out if this call was still needed
			//Loader.Instance.Reload();
			FormMain.ContentItemOptions.SetOptions();
			Controller.Instance.CurrentProject.PerformPreAnalysisActions();
			SharedData.ActiveProjectPath = Controller.Instance.CurrentProject.ProjectSettings.OutputPath;
		}

		internal static List<IFileInformation> GetListOfFilesToWrite(ProjectFileTree tree, bool overwriteExistingFiles)
		{
			List<IFileInformation> checkedNodes = new List<IFileInformation>();
			foreach (ProjectFileTreeNode node in tree.AllNodes)
			{
				if (node.NodeSelected &&
					// GenerationErrors don't stop the writeout process,
					// but they shouldn't be written either.
					node.Status != ProjectFileStatusEnum.GenerationError &&
					node.AssociatedFile != null &&
					(node.AssociatedFile.CurrentDiffResult.DiffPerformedSuccessfully ||
					overwriteExistingFiles)
					)
				{
					checkedNodes.Add(node.AssociatedFile);
				}
			}
			return checkedNodes;
		}

		private void panelGeneratedFiles_Resize(object sender, EventArgs e)
		{
			ResizeStatusButtons();
		}

		private void treeList1_BeforeNodeSelect(object sender, AdvTreeNodeCancelEventArgs e)
		{
			if (e.Node == null) return;

			//if (e.Node.HasChildNodes)
			//e.Cancel = true;
		}

		private void buttonX1_Click(object sender, EventArgs e)
		{
			HideDuplicatedFilesPane();
		}

		private void buttonCopyToClipboard_Click(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();

			foreach (var fileGroup in duplicatedFiles.GroupBy(f => f.ProcessedFilename))
			{
				sb.AppendLine(fileGroup.Key);
				foreach (var file in fileGroup)
				{
					sb.AppendFormat("\t created by {0} :: [{1}]", file.RawFilename, file.FilenameType);
					sb.AppendLine();

					if (file.IteratorObject == null)
						sb.Append("\t\t iterator: None");
					else
						sb.AppendFormat("\t\t iterator [{0}]: {1}", file.IteratorObject.GetType().Name, file.IteratorObject.ToString());

					sb.AppendLine();
					sb.AppendLine();
				}
			}
			Clipboard.Clear();
			Clipboard.SetText(sb.ToString());
		}

		public bool ChangeOutputPath()
		{
			Refresh();

			string originalProjectPath = Controller.Instance.CurrentProject.ProjectSettings.OutputPath;

			try
			{
				Controller.Instance.ShadeMainForm();
				var folderBrowserDialog = new FolderBrowserDialog()
				{
					ShowNewFolderButton = true,
					Description = "Choose folder to generate files to",
					UseDescriptionForTitle = true
				};

				var outputPath = Controller.Instance.CurrentProject.ProjectSettings.OutputPath;

				if (!string.IsNullOrEmpty(outputPath))
					folderBrowserDialog.SelectedPath = outputPath;
				else
				{
					string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Visual NHibernate");

					if (!Directory.Exists(dir))
						Directory.CreateDirectory(dir);

					folderBrowserDialog.SelectedPath = dir;
				}

				if (folderBrowserDialog.ShowDialog(this) != DialogResult.OK)
					return false;

				ValidateOutputPath();
				Controller.Instance.CurrentProject.ProjectSettings.OutputPath = folderBrowserDialog.SelectedPath;
				ProjectPathChanged();
			}
			finally
			{
				Cursor = Cursors.Default;
				Controller.Instance.UnshadeMainForm();
			}
			// Check if we need to generate the files
			if ((!Controller.Instance.CurrentProject.ProjectSettings.OutputPath.Equals(originalProjectPath, StringComparison.OrdinalIgnoreCase) ||
				treeList1.Nodes.Count == 1) &&
				 Directory.Exists(Controller.Instance.CurrentProject.ProjectSettings.OutputPath))
			{
				GenerateFiles();
			}
			return true;
		}

		private void ValidateOutputPath()
		{
			if (Controller.Instance.IsValidOutputPath(Controller.Instance.CurrentProject.ProjectSettings.OutputPath))
				treeList1.Nodes[0].ImageIndex = (int)NodeImages.FolderOpen;
			else
				treeList1.Nodes[0].ImageIndex = (int)NodeImages.Error;
		}

		private void labelNewFiles_Click(object sender, EventArgs e)
		{
			ShowNewFiles = !ShowNewFiles;
			FilterNodes();
		}

		private void labelUnchangedFiles_Click(object sender, EventArgs e)
		{
			ShowUnchanged = !ShowUnchanged;
			FilterNodes();
		}

		private void labelChangedFiles_Click(object sender, EventArgs e)
		{
			ShowResolved = !ShowResolved;
			FilterNodes();
		}

		private void labelErrors_Click_1(object sender, EventArgs e)
		{
			ShowErrors = !ShowErrors;
			FilterNodes();
		}

		private void buttonWriteFileToDisk_Click(object sender, EventArgs e)
		{
			WriteFilesToDisk();
		}

		private void buttonSelectAll_Click(object sender, EventArgs e)
		{
			SelectAllVisibleNodes(true);
		}

		private void SelectAllVisibleNodes(bool select)
		{
			treeList1.BeginUpdate();

			foreach (DevComponents.AdvTree.Node childNode in treeList1.Nodes)
			{
				SelectAllVisibleNodes(childNode, select);
			}
			treeList1.EndUpdate();
		}

		private void SelectAllVisibleNodes(DevComponents.AdvTree.Node node, bool select)
		{
			if (node.Visible)
			{
				node.SetChecked(select, eTreeAction.Code);

				foreach (DevComponents.AdvTree.Node childNode in node.Nodes)
				{
					SelectAllVisibleNodes(childNode, select);
				}
			}
		}

		private void buttonDeselectAll_Click(object sender, EventArgs e)
		{
			SelectAllVisibleNodes(false);
		}

		private void Output_Resize(object sender, EventArgs e)
		{
			if (panel2.Right > this.ClientSize.Width - 100)
				panel2.Width = panel2.MinimumSize.Width;
		}

		private void superTooltip1_BeforeTooltipDisplay(object sender, DevComponents.DotNetBar.SuperTooltipEventArgs e)
		{
			if (e.Source == labelNewFiles)
				e.TooltipInfo.HeaderText = string.Format("{0} new files", NumNewFiles);
			else if (e.Source == labelChangedFiles)
				e.TooltipInfo.HeaderText = string.Format("{0} changed files", NumResolved);
			else if (e.Source == labelUnchangedFiles)
				e.TooltipInfo.HeaderText = string.Format("{0} unchanged files", NumExactCopy);
			else if (e.Source == labelErrors)
				e.TooltipInfo.HeaderText = string.Format("{0} files with errors", NumErrors);
		}

		private void treeList1_AfterCheck(object sender, AdvTreeCellEventArgs e)
		{
			Node node = e.Cell.Parent;

			switch (node.CheckState)
			{
				case CheckState.Checked:
					if (node.Style != elementStyle1)
						node.Style = elementStyle1;
					break;
				case CheckState.Indeterminate:
					if (node.Style != elementStyle1)
						node.Style = elementStyle1;
					break;
				case CheckState.Unchecked:
					if (node.Style != elementStyleUnchecked)
						node.Style = elementStyleUnchecked;
					break;
			}
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			GenerateFiles();
		}

		private void pictureRefresh_Click(object sender, EventArgs e)
		{
			GenerateFiles();
		}
	}
}
