using System;
using System.ComponentModel;
using System.Windows.Forms;
using ArchAngel.Providers.CodeProvider;
using DevComponents.AdvTree;
using Slyce.IntelliMerge.Controller;

namespace Slyce.IntelliMerge.UI.Editors
{
	/// <summary>
	/// User Control for displaying a diff and merge of code. Currently supports C#.
	/// </summary>
	public partial class ucCodeMergeEditor : UserControl, IMergeEditor
	{
		private TextFileInformation fileInformation;
		private bool BusyPopulatingEditor;
		private Node currentFocusedNode = null;
		private ImageLoader imageLoader = new CSharpStatusImageLoader();
		public event EventHandler<FileUpdatedEventArgs> FileUpdated;

		/// <summary>
		/// Construct a new ucCodeMergeEditor. If using this constructor, you must set the FileInformation property before this control will do anything.
		/// </summary>
		public ucCodeMergeEditor()
		{
			InitializeComponent();
			treeListObjects.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
		}

		/// <summary>
		/// Construct a new ucCodeMergeEditor and fill it with information from the given fileInformation.
		/// </summary>
		/// <param name="fileInformation">The FileInformation object containing the code files to visually diff and merge. Must have been diffed already, and contain a valid CodeRoot in its DiffFile.</param>
		public ucCodeMergeEditor(TextFileInformation fileInformation)
			: this()
		{
			if (fileInformation == null) throw new ArgumentNullException("fileInformation");


			if (fileInformation.IntelliMerge != IntelliMergeType.CSharp || fileInformation.CurrentDiffResult.DiffPerformedSuccessfully == false)
			{
				throw new ArgumentException("Can only use this user control with C# files that have been diffed already and have a valid CodeRoot.");
			}

			ucTextMergeEditor.TextSyntaxLanguage = Utility.GetSyntaxLanguageForFileInformation(fileInformation);

			ucTextMergeEditor.MergedFileSavedEvent += ucTextMergeEditor1_MergedFileSavedEvent;

			this.fileInformation = fileInformation;

			if (fileInformation.CurrentDiffResult.DiffType == TypeOfDiff.Warning)
			{
				SetWarningText(fileInformation.CurrentDiffResult.DiffWarningDescription);
			}
			PopulateGrid();
		}

		public void SetWarningText(string text)
		{
			if (string.IsNullOrEmpty(text))
				panelWarning.Visible = false;
			else
			{
				lbWarningText.Text = text;
				panelWarning.Visible = true;
			}

		}

		private void FireFileUpdatedEvent()
		{
			if (FileUpdated != null)
			{
				FileUpdated(this, new FileUpdatedEventArgs(fileInformation));
			}
		}

		private void ucTextMergeEditor1_MergedFileSavedEvent(object sender, ucTextMergeEditor.MergedFileSavedEventArgs e)
		{
			CodeRootMapNode CurrentMapInfo = (CodeRootMapNode)treeListObjects.SelectedNode.Tag;
			string mergedCode = e.MergedFile.GetContents();
			CurrentMapInfo.SetMergedBaseConstruct(mergedCode);

			// Write the resolved changes to the merged file
			FileInformation.MergedFile = new TextFile(FileInformation.CodeRootMap.GetMergedCodeRoot().ToString());
			FileInformation.PerformSuperDiff();
			FireFileUpdatedEvent();
			PopulateGrid();
		}

		/// <summary>
		/// The TextFileInformation object that contains the code files we are diffing.
		/// </summary>
		public TextFileInformation FileInformation
		{
			get { return fileInformation; }
			set { fileInformation = value; }
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			if (treeListObjects.SelectedNode != null)
			{
				ToolStripItem item = selectMatchMenuItem;
				if (treeListObjects.SelectedNode.Tag is CodeRootMapNode)
				{
					CodeRootMapNode mapInfo = (CodeRootMapNode)treeListObjects.SelectedNode.Tag;

					int numValues = mapInfo.UserObj == null ? 0 : 1;
					numValues += mapInfo.PrevGenObj == null ? 0 : 1;
					numValues += mapInfo.NewGenObj == null ? 0 : 1;

					item.Enabled = (numValues > 0);
				}
				else
					e.Cancel = true;
			}
			else
				e.Cancel = true;
		}

		private void selectMatchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormSelectMatch form = new FormSelectMatch(fileInformation, treeListObjects.SelectedNode.Tag as CodeRootMapNode);

			form.SetSyntaxLanguage(Utility.GetSyntaxLanguageForFileInformation(fileInformation));
			form.ShowDialog();
			fileInformation.PerformDiff();
			FireFileUpdatedEvent();
			PopulateGrid();
		}

		private void PopulateGrid()
		{
			if (DesignMode)
				return;

			ucTextMergeEditor.FileInformation = null;
			ucTextMergeEditor.Fill();

			try
			{
				treeListObjects.BeginUpdate();
				treeListObjects.Nodes.Clear();

				Node root = new Node();
				treeListObjects.Nodes.Add(root);
				root.Text = fileInformation.RelativeFilePath;
				root.Image = imageLoader.GetFileImage();

				CodeRootMap map = fileInformation.CodeRootMap;

				foreach (CodeRootMapNode node in map.ChildNodes)
				{
					AddCodeRootNodeToTreeView(node, root);
				}
				root.Expanded = true;
			}
			finally
			{
				FilterNodes();
				treeListObjects.EndUpdate();
				if (treeListObjects.Nodes.Count > 0)
				{
					Node nodeToSelect = treeListObjects.Nodes[0];
					while (nodeToSelect != null)
					{
						if (nodeToSelect.Tag != null && ((CodeRootMapNode)nodeToSelect.Tag).DiffTypeExcludingChildren != TypeOfDiff.ExactCopy)
						{
							treeListObjects.SelectedNode = nodeToSelect;
							break;
						}
						nodeToSelect = nodeToSelect.NextVisibleNode;
					}
				}
			}
		}

		private void AddCodeRootNodeToTreeView(CodeRootMapNode codeRootNode, Node parent)
		{
			//IBaseConstruct baseConstruct = codeRootNode.GetMergedBaseConstruct();
			Node node = new Node();
			parent.Nodes.Add(node);
			node.Tag = codeRootNode;

			node.Style = elementStyle1;
			if (((CodeRootMapNode)node.Tag).DiffTypeExcludingChildren == TypeOfDiff.ExactCopy && AnyChildrenShowing(node) == false)
				node.Style = elementStyleUnchanged;

			node.Cells.Add(new Cell());
			node.Cells.Add(new Cell());
			node.Cells.Add(new Cell());

			node.Text = codeRootNode.GetFirstValidBaseConstruct().ShortName;
			node.Cells[1].Text = codeRootNode.UserObj != null ? codeRootNode.UserObj.ShortName : "";
			node.Cells[2].Text = codeRootNode.NewGenObj != null ? codeRootNode.NewGenObj.ShortName : "";
			node.Cells[3].Text = codeRootNode.PrevGenObj != null ? codeRootNode.PrevGenObj.ShortName : "";

			ImageLoader.Status status = ImageLoader.Status.Resolved;
			if (codeRootNode.DiffTypeExcludingChildren == TypeOfDiff.Conflict)
			{
				status = ImageLoader.Status.Conflict;
			}
			else if (codeRootNode.DiffTypeExcludingChildren == TypeOfDiff.ExactCopy)
			{
				status = ImageLoader.Status.ExactCopy;
			}
			node.Image = imageLoader.GetImageForBaseConstruct(codeRootNode.GetFirstValidBaseConstruct(), status);

			foreach (CodeRootMapNode child in codeRootNode.ChildNodes)
			{
				AddCodeRootNodeToTreeView(child, node);
				node.Expanded = true;
			}
		}

		private void DisplayNodeFiles(Node node)
		{
			if (DesignMode)
				return;

			if (!BusyPopulatingEditor)
			{
				if (node == null)
				{
					ucTextMergeEditor.FileInformation = null;
					return;
				}
				if (node.Tag == null)
				{
					return;
				}
				try
				{
					Cursor = Cursors.WaitCursor;
					BusyPopulatingEditor = true;
					DisplayNodeFiles(node.Tag as CodeRootMapNode);
				}
				finally
				{
					BusyPopulatingEditor = false;
					Cursor = Cursors.Default;
				}
			}
		}

		private void DisplayNodeFiles(CodeRootMapNode node)
		{
			if (DesignMode)
				return;

			string userText = "";
			string templateText = "";
			string prevGenText = null;

			if (node.MergedObj != null && node.DiffTypeExcludingChildren == TypeOfDiff.ExactCopy)
			{
				userText = node.MergedObj.IsLeaf ? node.MergedObj.GetFullText() : node.MergedObj.GetOuterText();
				userText = Common.Utility.StandardizeLineBreaks(userText, Common.Utility.LineBreaks.Unix);

				templateText = prevGenText = userText;
			}
			else
			{
				if (node.UserObj != null)
				{
					userText = GetObjectText(node.UserObj);
				}
				if (node.NewGenObj != null)
				{
					templateText = GetObjectText(node.NewGenObj);
				}
				if (node.PrevGenObj != null)
				{
					prevGenText = GetObjectText(node.PrevGenObj);
				}
			}

			TextFileInformation tfi = new TextFileInformation();
			tfi.PrevGenFile = prevGenText != null ? new TextFile(prevGenText) : TextFile.Blank;
			tfi.NewGenFile = new TextFile(templateText);
			tfi.UserFile = new TextFile(userText);
			tfi.RelativeFilePath = FileInformation.RelativeFilePath;
			tfi.IntelliMerge = IntelliMergeType.PlainText;
			ucTextMergeEditor.FileInformation = tfi;
		}

		private static string GetObjectText(IBaseConstruct obj)
		{
			string text = obj.IsLeaf ? obj.GetFullText() : obj.GetOuterText();
			text = Common.Utility.StandardizeLineBreaks(text, Common.Utility.LineBreaks.Unix);
			if (text.IndexOf('\n') == 0)
				text = text.Remove(0, 1);
			return text;
		}

		#region Event Handlers

		private void treeListObjects_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
		{
			bool selectMatchEnabled = false;

			// this try finally block is here to stop the select match button flickering every time the user clicks
			// a new node in the treelist.
			try
			{
				if (DesignMode)
					return;

				if (currentFocusedNode != null && treeListObjects.SelectedNode == currentFocusedNode)
				{
					selectMatchEnabled = true;
					return;
				}

				if (currentFocusedNode != null && ucTextMergeEditor.HasUnsavedChanges)
				{
					DialogResult result = MessageBox.Show(this,
														  "You have unsaved changes in the Diff editor. Do you wish to discard those changes?",
														  "Discard Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
														  MessageBoxDefaultButton.Button2);
					if (result == DialogResult.No)
					{
						treeListObjects.SelectedNode = currentFocusedNode;
						return;
					}
				}

				if (treeListObjects.SelectedNode == null || treeListObjects.SelectedNode.Tag == null)
				{
					// Hide the Text Merge Editor.
					ucTextMergeEditor.Visible = false;
					currentFocusedNode = null;
					return;
				}

				ucTextMergeEditor.Visible = true;

				DisplayNodeFiles(treeListObjects.SelectedNode);
				currentFocusedNode = treeListObjects.SelectedNode;
				selectMatchEnabled = true;
				Refresh();
			}
			finally
			{
				btnSelectMatch.Enabled = selectMatchEnabled;
			}
		}

		private void FilterNodes()
		{
			foreach (Node child in treeListObjects.Nodes)
			{
				FilterNode(child);
			}
		}

		private void FilterNode(Node node)
		{
			foreach (Node child in node.Nodes)
			{
				FilterNode(child);
			}

			bool nodeVisible;
			if (checkBox1.Checked == false && node.Tag is CodeRootMapNode)
			{
				nodeVisible = ((CodeRootMapNode)node.Tag).DiffTypeExcludingChildren != TypeOfDiff.ExactCopy || AnyChildrenShowing(node);
			}
			else
			{
				nodeVisible = true;
			}
			node.Visible = node.Selectable = node.Enabled = nodeVisible;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			FilterNodes();
		}

		#endregion

		private static bool AnyChildrenShowing(Node node)
		{
			foreach (Node child in node.Nodes)
			{
				if (AnyChildrenShowing(child))
					return true;
				CodeRootMapNode crmn = child.Tag as CodeRootMapNode;
				if (crmn == null) return true;

				if (crmn.DiffTypeExcludingChildren != TypeOfDiff.ExactCopy)
					return true;
			}

			return false;
		}

		public bool HasUnsavedChanges
		{
			get
			{
				return ucTextMergeEditor.HasUnsavedChanges;
			}
		}
	}

	public class FileUpdatedEventArgs : EventArgs
	{
		public readonly IFileInformation UpdatedFile;

		public FileUpdatedEventArgs(IFileInformation updatedFile)
		{
			UpdatedFile = updatedFile;
		}
	}
}