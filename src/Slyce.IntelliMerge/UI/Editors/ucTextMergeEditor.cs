using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor;
using Algorithm.Diff;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge.UI.VisualDiff;

namespace Slyce.IntelliMerge.UI.Editors
{
	public partial class ucTextMergeEditor : UserControl, IMergeEditor
	{
		private TextFileInformation fileInformation;
		private VisualDiffOutput currentDiffInfo;
		private DiffDisplayAlgorithm displayAlgorithm;

		private bool busyPopulatingEditors;
		internal const int ImageWidth = 50;
		internal const int ButtonGap = 2;

		private Color conflictColour = Color.Red;
		private Color userChangeColour = Color.LightBlue;
		private Color templateChangeColour = Color.LightGreen;
		private Color userAndTemplateChangeColour = Color.LightCyan;
		private Color virtualLineColour = Color.LightGray;
		private bool hasUnsavedChanges = false;
		private bool modifyingEditorText;

		public event EventHandler<MergedFileSavedEventArgs> MergedFileSavedEvent;

		/// <summary>
		/// Contains the merged text that was saved.
		/// </summary>
		public class MergedFileSavedEventArgs : EventArgs
		{
			private readonly TextFileInformation fileInformation;
			private readonly TextFile mergedFile;

			public MergedFileSavedEventArgs()
			{
			}

			public MergedFileSavedEventArgs(TextFile mergedFile, TextFileInformation fileInfo)
			{
				this.mergedFile = mergedFile;
				fileInformation = fileInfo;
			}

			public TextFileInformation FileInformation
			{
				get { return fileInformation; }
			}

			/// <summary>
			/// The TextFile containing the merged text.
			/// </summary>
			public TextFile MergedFile
			{
				get { return mergedFile; }
			}
		}

		public ucTextMergeEditor()
		{
			InitializeComponent();
			editorResolved.Document.TextChanging += editorResolved_DocumentTextChanging;
			editorResolved.Document.TextChanged += editorResolved_DocumentTextChanged;
			editorResolved.Document.PreTextChanging += Document_PreTextChanging;

			displayAlgorithm = new NullDiffDisplayAlgorithm(this);
		}

		public ucTextMergeEditor(TextFileInformation fileInformation)
			: this()
		{
			this.fileInformation = fileInformation;
			toolStripButtonEnableEditing.Checked = false;
			toolStripButtonEnableEditing_CheckedChanged(toolStripButtonEnableEditing, new EventArgs()); // Apply default options

			SyntaxLanguage language = Utility.GetSyntaxLanguageForFileInformation(fileInformation);
			editorOriginal.Document.Language = language;
			editorResolved.Document.Language = language;

			if (fileInformation.PrevGenFile.HasContents == false)
			{
				displayAlgorithm = new TwoWayDiffDisplayAlgorithm(this);
				WarningText =
					"The previous version of the file seems to be missing. This merge is a 2 way merge," +
					" so all changes must be examined to resolve the conflict.";
			}
			else
				displayAlgorithm = new ThreeWayDiffDisplayAlgorithm(this);

			Fill();
		}

		private void Document_PreTextChanging(object sender, DocumentModificationEventArgs e)
		{
			if (e.IsProgrammaticTextReplacement || ModifyingEditorText) return;
			DocumentModification docMod = e.Modification;

			if (CurrentDiffInfo != null && CurrentDiffInfo.RightLines[docMod.StartLineIndex].IsVirtual)
			{
				e.Cancel = true;
			}
		}

		void editorResolved_DocumentTextChanged(object sender, DocumentModificationEventArgs e)
		{
			if (e.IsProgrammaticTextReplacement || ModifyingEditorText) return;

			if (e.Cancel) return;

			DocumentModification docMod = e.Modification;

			// Handled added text.
			// If the text was on one line.
			//   - Set the right hand line to the new text.
			//   - Set the right hand line to UserChange.
			//   - Set the left line to UserChange.
			// If the text was on multiple lines
			//   - Determine how many lines changed (numLinesChanged)
			//   - Add the text of the changed lines to the right hand side.
			//   - Set these lines to UserChange.
			//   - Add any needed virtual lines to the left.

			int newCaretOffset = docMod.StartOffset;

			int startLineIndex = docMod.StartLineIndex;
			if (docMod.LinesDelta == 0)
			{
				CurrentDiffInfo.RightLines[startLineIndex].Text = editorResolved.Document.Lines[startLineIndex].Text;
				CurrentDiffInfo.RightLines[startLineIndex].Change = ChangeType.User;
				newCaretOffset = docMod.LengthDelta > 0 ? docMod.InsertionEndOffset : Math.Max(0, docMod.DeletionEndOffset - 1);
			}
			else if (string.IsNullOrEmpty(docMod.InsertedText) == false)
			{
				int numLinesChanged = docMod.LinesInserted;

				// Remove the original start line
				CurrentDiffInfo.RightLines.RemoveAt(startLineIndex);

				// Recreate it and add new lines from editor
				for (int i = 0; i < numLinesChanged + 1; i++)
				{
					string lineText = editorResolved.Document.Lines[startLineIndex + i].Text;
					CurrentDiffInfo.RightLines.Insert(startLineIndex + i, new DiffLine(lineText, ChangeType.User));
				}

				// Add the virtual lines to the left side.
				for (int i = 0; i < numLinesChanged; i++)
				{
					CurrentDiffInfo.LeftLines.Insert(docMod.InsertionEndLineIndex, new DiffLine("", ChangeType.User, true));
				}
				newCaretOffset = docMod.InsertionEndOffset;
			}
			CurrentDiffInfo.CleanUp();

			FillEditors();

			editorResolved.Caret.Offset = newCaretOffset;

			editorOriginal.SelectedView.FirstVisibleX = editorResolved.SelectedView.FirstVisibleX;
			editorOriginal.SelectedView.FirstVisibleDisplayLineIndex = editorResolved.SelectedView.FirstVisibleDisplayLineIndex;
		}

		private void editorResolved_DocumentTextChanging(object sender, DocumentModificationEventArgs e)
		{
			if (e.IsProgrammaticTextReplacement || ModifyingEditorText) return;

			HasUnsavedChanges = true;

			DocumentModification docMod = e.Modification;

			int startLineIndex = docMod.StartLineIndex;
			if (docMod.LinesDelta == 0) // User modified some text on a single line.
			{
				// This gets handled in DocumentTextChanged.
				return;
			}

			if (string.IsNullOrEmpty(docMod.DeletedText)) return;

			// Handle deleted text here.
			// If the changed text was all on one line:
			//   - Set the right hand line to the new text.
			//   - Set the right hand line to UserChange.
			//   - Set the left line to UserChange.
			// If the text was on multiple lines:
			//   - Determine how many lines were removed (numLinesChanged).
			//   - Removed numLinesChanged from the right hand side.
			//   - Add the new text in as a UserChange.
			//   - Add in any needed virtual lines on the right.

			int numLinesChanged = docMod.LinesDeleted;

			DocumentLine oldStartLine = editorResolved.Document.Lines[startLineIndex];
			int endLineIndex = docMod.DeletionEndLineIndex;
			DocumentLine oldEndLine = editorResolved.Document.Lines[endLineIndex];

			string newLineText = oldStartLine.Text.Substring(0, docMod.StartOffset - oldStartLine.StartOffset) + oldEndLine.Text.Substring(docMod.DeletionEndOffset - oldEndLine.StartOffset);

			for (int i = 0; i < numLinesChanged + 1; i++)
			{
				CurrentDiffInfo.RightLines.RemoveAt(startLineIndex);
			}

			for (int i = 0; i < numLinesChanged; i++)
			{
				CurrentDiffInfo.RightLines.Insert(startLineIndex, new DiffLine("", ChangeType.User, true));
			}

			CurrentDiffInfo.RightLines.Insert(startLineIndex, new DiffLine(newLineText, ChangeType.User));
		}

		/// <summary>
		/// Sets the syntax language the editors will use to colourize the text. If none is set, it will
		/// just default to black and white text.
		/// </summary>
		public SyntaxLanguage TextSyntaxLanguage
		{
			set
			{
				editorOriginal.Document.Language = editorResolved.Document.Language = value;
			}
		}

		public bool EditingEnabled
		{
			get
			{
				return toolStripButtonEnableEditing.Checked;
			}
			set
			{
				toolStripButtonEnableEditing.Checked = value;
			}
		}

		public TextFileInformation FileInformation
		{
			get { return fileInformation; }
			set
			{
				fileInformation = value;
				if (fileInformation != null)
				{
					displayAlgorithm.ClearButtons();

					if (fileInformation.PrevGenFile.HasContents == false)
						displayAlgorithm = new TwoWayDiffDisplayAlgorithm(this);
					else
						displayAlgorithm = new ThreeWayDiffDisplayAlgorithm(this);

					Fill();
				}
			}
		}

		public Color VirtualLineColour
		{
			get { return virtualLineColour; }
			set { virtualLineColour = value; }
		}

		public Color UserAndTemplateChangeColour
		{
			get { return userAndTemplateChangeColour; }
			set { userAndTemplateChangeColour = value; }
		}

		public Color TemplateChangeColour
		{
			get { return templateChangeColour; }
			set { templateChangeColour = value; }
		}

		public Color UserChangeColour
		{
			get { return userChangeColour; }
			set { userChangeColour = value; }
		}

		public Color ConflictColour
		{
			get { return conflictColour; }
			set { conflictColour = value; }
		}

		public bool HasUnsavedChanges
		{
			get { return hasUnsavedChanges; }
			set { hasUnsavedChanges = value; }
		}

		internal bool ModifyingEditorText
		{
			get { return modifyingEditorText; }
			set { modifyingEditorText = value; }
		}

		private void Reset()
		{
			displayAlgorithm.ClearButtons();
			displayAlgorithm.AddButtons();
		}

		internal SyntaxEditor EditorOriginal { get { return editorOriginal; } }
		internal SyntaxEditor EditorResolved { get { return editorResolved; } }
		internal Panel ResolvedButtonPanel { get { return resolvedButtonPanel; } }
		internal Panel OriginalButtonPanel { get { return originalButtonPanel; } }
		internal ToolTip ToolTipForButtons { get { return toolTipForButtons; } }


		internal VisualDiffOutput CurrentDiffInfo
		{
			get { return currentDiffInfo; }
			set { currentDiffInfo = value; }
		}

		internal bool BusyPopulatingEditors
		{
			get { return busyPopulatingEditors; }
			set { busyPopulatingEditors = value; }
		}

		/// <summary>
		/// Forces the control to update the contents of its editors, and display the diff results.
		/// </summary>
		internal void Fill()
		{
			BusyPopulatingEditors = true;
			if (fileInformation == null)
			{
				editorOriginal.Text = "";
				editorResolved.Text = "";
				BusyPopulatingEditors = false;
				return;
			}

			if (string.IsNullOrEmpty(fileInformation.CurrentDiffResult.DiffWarningDescription) == false)
			{
				WarningText = (string.IsNullOrEmpty(WarningText) ? "" : WarningText + Environment.NewLine) + fileInformation.CurrentDiffResult.DiffWarningDescription;
			}
			else
				panelWarning.Visible = false;

			editorOriginal.SuspendPainting();
			editorResolved.SuspendPainting();

			currentDiffInfo = displayAlgorithm.GetDiffOutput();
			editorOriginal.Document.Text = "";
			editorResolved.Document.Text = "";

			if (CurrentDiffInfo.DiffType == TypeOfDiff.Conflict)
				EnableEditing(true);

			FillEditors();

			editorOriginal.SelectedView.ScrollToDocumentStart();
			editorResolved.SelectedView.ScrollToDocumentStart();

			displayAlgorithm.AddButtons();
			HasUnsavedChanges = false;
			Refresh();

			editorOriginal.ResumePainting();
			editorResolved.ResumePainting();
			BusyPopulatingEditors = false;
		}

		public string WarningText
		{
			get { return lbWarningText.Text; }
			set
			{
				panelWarning.Visible = true;
				lbWarningText.Text = value;
			}
		}

		public void FillEditors()
		{
			StringBuilder left = new StringBuilder();
			StringBuilder right = new StringBuilder();

			for (int i = 0; i < CurrentDiffInfo.LeftLines.Count; i++)
			{
				DiffLine leftline = CurrentDiffInfo.LeftLines[i];
				DiffLine rightline = CurrentDiffInfo.RightLines[i];

				left.AppendFormat("{0}{1}", i != 0 ? Environment.NewLine : "", leftline.Text);
				right.AppendFormat("{0}{1}", i != 0 ? Environment.NewLine : "", rightline.Text);
			}

			editorOriginal.SuspendPainting();
			editorResolved.SuspendPainting();
			modifyingEditorText = true;
			editorOriginal.Document.ReplaceText(DocumentModificationType.ReplaceAll, 0, editorOriginal.Document.Length, left.ToString());
			editorResolved.Document.ReplaceText(DocumentModificationType.ReplaceAll, 0, editorResolved.Document.Length, right.ToString());
			modifyingEditorText = false;

			editorOriginal.ResumePainting();
			editorResolved.ResumePainting();

			RecalculateVisualAspects();
		}


		/// <summary>
		/// Recalculates and redisplays line numbers, line colours, and the apply/undo buttons
		/// </summary>
		private void RecalculateVisualAspects()
		{
			SetLineNumbers();
			SetLineColours();
			displayAlgorithm.AddButtons();
		}

		/// <summary>
		/// Saves the current text of the resolved editor.
		/// </summary>
		protected virtual void SaveEdits()
		{
			// You can't save if there are conflicts.
			if (currentDiffInfo.ConflictRanges.Count > 0)
			{
				MessageBox.Show(this, "You cannot save if there are conflicts remaining to merge.", "Cannot save", MessageBoxButtons.OK);
				return;
			}

			StringBuilder sb = new StringBuilder();

			foreach (DocumentLine line in editorResolved.Document.Lines)
			{
				if (line.BackColor != VirtualLineColour)
					sb.AppendLine(line.Text);
			}
			fileInformation.MergedFile = new TextFile(sb.ToString());
			fileInformation.PerformDiff();
			HasUnsavedChanges = false;

			TriggerMergedFileSavedEvent();
			Reset();
		}

		protected virtual void TriggerMergedFileSavedEvent()
		{
			if (MergedFileSavedEvent != null)
			{
				MergedFileSavedEvent(this, new MergedFileSavedEventArgs((TextFile)fileInformation.MergedFile, fileInformation));
			}
		}


		///<summary>
		///Forces the control to invalidate its client area and immediately redraw itself and any child controls.
		///</summary>
		///<filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
		public override void Refresh()
		{
			base.Refresh();

			if (CurrentDiffInfo == null)
				return;
			SetLineColours();
			//for (int i = 0; i < currentDiffInfo.LeftLines.Count; i++)
			//{
			//    SetLineColours(currentDiffInfo.LeftLines, currentDiffInfo.RightLines, editorOriginal, editorResolved, i);

			//}

			displayAlgorithm.AddButtons();
		}

		internal void SetLineNumbers()
		{
			int lineNumber = 1;

			for (int i = 0; i < currentDiffInfo.RightLines.Count; i++)
			{
				DiffLine line = currentDiffInfo.RightLines[i];
				if (line.IsVirtual == false)
				{
					editorResolved.Document.Lines[i].CustomLineNumber = lineNumber.ToString();
					lineNumber++;
				}
				else
				{
					editorResolved.Document.Lines[i].CustomLineNumber = string.Empty;
				}
			}
			lineNumber = 1;
			for (int i = 0; i < currentDiffInfo.LeftLines.Count; i++)
			{
				DiffLine line = currentDiffInfo.LeftLines[i];
				if (line.IsVirtual == false)
				{
					editorOriginal.Document.Lines[i].CustomLineNumber = lineNumber.ToString();
					lineNumber++;
				}
				else
				{
					editorOriginal.Document.Lines[i].CustomLineNumber = string.Empty;
				}
			}
		}

		private void SetLineColours()
		{
			for (int i = 0; i < CurrentDiffInfo.LeftLines.Count; i++)
			{
				SetLineColours(i);
			}
		}

		private void SetLineColours(int lineIndex)
		{
			IList<DiffLine> leftLines = CurrentDiffInfo.LeftLines, rightLines = CurrentDiffInfo.RightLines;
			SyntaxEditor leftEditor = editorOriginal, rightEditor = editorResolved;

			if (CurrentDiffInfo.IsLineInConflict(lineIndex))
			{
				SetCommonLineColours(lineIndex, leftEditor, leftLines);
				rightEditor.Document.Lines[lineIndex].BackColor = ConflictColour;
				return;
			}

			SetCommonLineColours(lineIndex, leftEditor, leftLines);
			SetCommonLineColours(lineIndex, rightEditor, rightLines);

			if (rightLines[lineIndex].Change == ChangeType.None) return;

			if (rightLines[lineIndex].IsVirtual)
			{
				switch (rightLines[lineIndex].Change)
				{
					case ChangeType.Template:
						leftEditor.Document.Lines[lineIndex].BackColor = TemplateChangeColour;
						break;
					case ChangeType.User:
						leftEditor.Document.Lines[lineIndex].BackColor = UserChangeColour;
						break;
					case ChangeType.UserAndTemplate:
						leftEditor.Document.Lines[lineIndex].BackColor = UserAndTemplateChangeColour;
						break;
				}
			}
		}

		private void SetCommonLineColours(int lineIndex, SyntaxEditor editor, IList<DiffLine> lines)
		{
			switch (lines[lineIndex].Change)
			{
				case ChangeType.Template:
					editor.Document.Lines[lineIndex].BackColor = TemplateChangeColour;
					break;
				case ChangeType.User:
					editor.Document.Lines[lineIndex].BackColor = UserChangeColour;
					break;
				case ChangeType.UserAndTemplate:
					editor.Document.Lines[lineIndex].BackColor = UserAndTemplateChangeColour;
					break;
			}
			if (lines[lineIndex].IsVirtual)
			{
				editor.Document.Lines[lineIndex].BackColor = VirtualLineColour;
			}
		}

		internal void EnableEditing(bool enable)
		{
			toolStripButtonEnableEditing.Checked = enable;
		}

		#region Event Handlers

		private void btnAccept_Click(object sender, EventArgs e)
		{
			SaveEdits();
		}

		private void editorOriginal_ViewVerticalScroll(object sender, EditorViewEventArgs e)
		{
			if (DesignMode)
				return;
			if (BusyPopulatingEditors) return;

			displayAlgorithm.ClearButtons();
			editorResolved.SelectedView.FirstVisibleDisplayLineIndex = editorOriginal.SelectedView.FirstVisibleDisplayLineIndex;
			displayAlgorithm.AddButtons();
		}

		private void editorResolved_ViewVerticalScroll(object sender, EditorViewEventArgs e)
		{
			if (DesignMode)
				return;
			if (BusyPopulatingEditors) return;

			displayAlgorithm.ClearButtons();
			editorOriginal.SelectedView.FirstVisibleDisplayLineIndex = editorResolved.SelectedView.FirstVisibleDisplayLineIndex;
			displayAlgorithm.AddButtons();
		}

		private void editorOriginal_Resize(object sender, EventArgs e)
		{
			if (DesignMode)
				return;
			if (!BusyPopulatingEditors && displayAlgorithm != null)
			{
				displayAlgorithm.AddButtons();
			}
		}

		private void editorResolved_Resize(object sender, EventArgs e)
		{
			if (DesignMode)
				return;
			if (!BusyPopulatingEditors && displayAlgorithm != null)
			{
				displayAlgorithm.AddButtons();
			}
		}

		private void editorOriginal_UserMarginPaint(object sender, UserMarginPaintEventArgs e)
		{
			if (DesignMode)
				return;
			if (CurrentDiffInfo == null)
				return;
			if (CurrentDiffInfo.IsLineInConflict(e.DocumentLineIndex) == false)
				return;
			userMarginPaint(CurrentDiffInfo.LeftLines, e);
		}

		private void editorResolved_UserMarginPaint(object sender, UserMarginPaintEventArgs e)
		{
			if (DesignMode)
				return;
			if (CurrentDiffInfo == null)
				return;
			if (CurrentDiffInfo.RightLines[e.DocumentLineIndex].IsVirtual)
				return;
			if (CurrentDiffInfo.IsLineInConflict(e.DocumentLineIndex))
				return;
			userMarginPaint(CurrentDiffInfo.RightLines, e);
		}

		private static void userMarginPaint(IList<DiffLine> lines, UserMarginPaintEventArgs e)
		{
			// Add text to the user margin to indicate what kind of line it is.
			if (e.DocumentLineIndex >= lines.Count)
				return;
			if (lines[e.DocumentLineIndex].Change == ChangeType.None) return;

			// Custom draw a word wrap continuation marker
			int y = e.DisplayLineBounds.Top;
			Font font = new Font("Verdana", 6);
			SolidBrush brush = new SolidBrush(Color.DarkBlue);

			int newX = e.DisplayLineBounds.Left + (int)((e.DisplayLineBounds.Width - e.Graphics.MeasureString(lines[e.DocumentLineIndex].Change.ToString(), font).Width) / 2);
			e.Graphics.DrawString(lines[e.DocumentLineIndex].Change.ToString(), font, brush, new Point(newX, y));
		}

		private void toolStripButtonPrevConflict_Click(object sender, EventArgs e)
		{
			Document currentDocument = editorOriginal.Document;

			VisualDiffOutput.ConflictRange conflict = CurrentDiffInfo.GetConflictBefore(editorOriginal.Caret.DocumentPosition.Line);
			if (conflict != null)
			{
				editorOriginal.Caret.Offset =
					currentDocument.PositionToOffset(new DocumentPosition(conflict.StartLineIndex, 0));
			}
		}

		private void toolStripButtonNextConflict_Click(object sender, EventArgs e)
		{
			Document currentDocument = editorOriginal.Document;

			VisualDiffOutput.ConflictRange conflict = CurrentDiffInfo.GetConflictAfter(editorOriginal.Caret.DocumentPosition.Line);
			if (conflict != null)
			{
				editorOriginal.Caret.Offset =
					currentDocument.PositionToOffset(new DocumentPosition(conflict.StartLineIndex, 0));
				editorOriginal.SelectedView.ScrollLineToVisibleMiddle();
			}
		}

		private void toolStripButtonEnableEditing_CheckedChanged(object sender, EventArgs e)
		{
			if (EditingEnabled)
			{
				toolStripButtonEnableEditing.Text = "Disable Editing";
			}
			else
			{
				toolStripButtonEnableEditing.Text = " Enable Editing";
			}

			btnAccept.Enabled = EditingEnabled;
			editorResolved.Document.ReadOnly = !EditingEnabled;
			displayAlgorithm.AddButtons();
		}

		#endregion
	}
}