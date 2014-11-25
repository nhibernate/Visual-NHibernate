using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor;
using Algorithm.Diff;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge.UI.VisualDiff;

namespace Slyce.IntelliMerge.UI.Editors
{

	internal abstract class DiffDisplayAlgorithm
	{
		protected readonly ucTextMergeEditor mergeEditor;

		protected readonly SyntaxEditor editorOriginal;
		protected readonly SyntaxEditor editorResolved;
		protected readonly TextFileInformation fileInformation;
		protected readonly List<Button> ApplyButtons = new List<Button>();
		protected readonly List<Button> UndoButtons = new List<Button>();

		protected DiffDisplayAlgorithm(ucTextMergeEditor editor)
		{
			mergeEditor = editor;

			editorOriginal = editor.EditorOriginal;
			editorResolved = editor.EditorResolved;
			fileInformation = editor.FileInformation;
		}

		public abstract VisualDiffOutput GetDiffOutput();

		protected static int GetLastUndoLine(int i, IList<DiffLine> sideWithChanges)
		{
			ChangeType currentChangeType = sideWithChanges[i].Change;
			int lastLine = i;

			while (lastLine + 1 < sideWithChanges.Count && sideWithChanges[lastLine + 1].Change == currentChangeType)
			{
				lastLine++;
			}

			return lastLine;
		}

		protected static void ClearButtons(Panel panel, List<Button> buttons)
		{
			for (int i = buttons.Count - 1; i >= 0; i--)
			{
				panel.Controls.Remove(buttons[i]);
			}
			buttons.Clear();
		}

		internal void CreateButton(int firstDisplayLine, int lineIndex, int lineHeight, Control buttonPanel, string buttonText, string buttonToolTip, EventHandler button_Click, List<Button> buttonList)
		{
			int numLinesOffset = lineIndex - firstDisplayLine;

			Button button = new Button();
			button.BackColor = Color.FromKnownColor(KnownColor.Control);
			button.Width = ucTextMergeEditor.ImageWidth - ucTextMergeEditor.ButtonGap * 2;
			button.Left = ucTextMergeEditor.ButtonGap;
			button.Top = numLinesOffset * lineHeight;
			button.Visible = true;
			button.Tag = lineIndex;

			mergeEditor.ToolTipForButtons.SetToolTip(button, buttonToolTip);
			button.Text = buttonText;
			button.Click += button_Click;
			buttonList.Add(button);

			button.TextAlign = ContentAlignment.MiddleCenter;
			buttonPanel.Controls.Add(button);
		}

		/// <summary>
		/// Adds all buttons to the Original and Resolved button panels on the editor.
		/// </summary>
		public void AddButtons()
		{
			if (mergeEditor.EditingEnabled)
			{
				//Common.Utility.SuspendPainting(mergeEditor);
				try
				{
					AddApplyButtons();
					AddUndoButtons();
				}
				finally
				{
					//Common.Utility.ResumePainting(mergeEditor);
				}
			}
			else
			{
				ClearButtons();
			}
		}

		public void ClearButtons()
		{
			//Common.Utility.SuspendPainting(mergeEditor);
			try
			{
				ClearApplyButtons();
				ClearUndoButtons();
			}
			finally
			{
				//Common.Utility.ResumePainting(mergeEditor);
			}
		}

		private void AddApplyButtons()
		{
			ClearApplyButtons();

			VisualDiffOutput diffOutput = mergeEditor.CurrentDiffInfo;
			if (editorOriginal.Visible == false || diffOutput == null)
			{
				return;
			}
			if (diffOutput.LeftLines.Count == 0)
				return;
			int firstDisplayLine = editorOriginal.SelectedView.FirstVisibleDisplayLineIndex;
			int lineHeight = editorOriginal.SelectedView.DisplayLineHeight;

			for (int i = firstDisplayLine; i < firstDisplayLine + editorOriginal.SelectedView.VisibleDisplayLineCount && i < editorOriginal.Document.Lines.Count; i++)
			{
				DocumentLine line = editorOriginal.SelectedView.DisplayLines[i].DocumentLine;
				VisualDiffOutput.ConflictRange conflict;
				if (diffOutput.IsLineInConflict(line.Index, out conflict) == false) continue;

				// Conflict
				CreateButton(firstDisplayLine, i, lineHeight, mergeEditor.OriginalButtonPanel, "Apply", "Apply the user change", ApplyButton_Click, ApplyButtons);

				// Figure out where the template change starts
				int j = i;
				for (; j < conflict.EndLineIndex && diffOutput.LeftLines[j].Change == ChangeType.User; j++)
				{
				}
				CreateButton(firstDisplayLine, j, lineHeight, mergeEditor.OriginalButtonPanel, "Apply", "Apply the template change", ApplyButton_Click, ApplyButtons);

				i = conflict.EndLineIndex;
			}
		}

		private void AddUndoButtons()
		{
			ClearUndoButtons();

			VisualDiffOutput diffOutput = mergeEditor.CurrentDiffInfo;

			if (editorOriginal.Visible == false || diffOutput == null)
			{
				return;
			}
			if (diffOutput.LeftLines.Count == 0)
				return;

			int firstDisplayLine = editorResolved.SelectedView.FirstVisibleDisplayLineIndex;
			int lineHeight = editorResolved.SelectedView.DisplayLineHeight;

			for (int i = firstDisplayLine; i < firstDisplayLine + editorResolved.SelectedView.VisibleDisplayLineCount && i < editorResolved.Document.Lines.Count; i++)
			{
				DocumentLine line = editorResolved.SelectedView.DisplayLines[i].DocumentLine;
				if ((diffOutput.RightLines[line.Index].Change & ChangeType.UserAndTemplate) == 0) continue;
				if (diffOutput.IsLineInConflict(line.Index)) continue;
				// Someone Deleted a line
				CreateButton(firstDisplayLine, i, lineHeight, mergeEditor.ResolvedButtonPanel, "Undo", "Undo your change", UndoButton_Click, UndoButtons);
				i = GetLastUndoLine(i, diffOutput.RightLines);
			}
		}

		private void ClearApplyButtons()
		{
			ClearButtons(mergeEditor.OriginalButtonPanel, ApplyButtons);
		}

		private void ClearUndoButtons()
		{
			ClearButtons(mergeEditor.ResolvedButtonPanel, UndoButtons);
		}

		private void UndoButton_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			int firstLine = (int)button.Tag;
			mergeEditor.ModifyingEditorText = true;

			// Find the last line
			VisualDiffOutput diffOutput = mergeEditor.CurrentDiffInfo;

			int lastLine = GetLastUndoLine(firstLine, diffOutput.RightLines);

			if (diffOutput.RightLines[firstLine].IsVirtual) // The user deleted the lines
			{
				// Copy the lines from prevgen.
				for (int i = lastLine; i >= firstLine; i--)
				{
					diffOutput.RightLines[i].Text = diffOutput.LeftLines[i].Text;
					diffOutput.RightLines[i].IsVirtual = diffOutput.LeftLines[i].IsVirtual = false;
					diffOutput.RightLines[i].Change = diffOutput.LeftLines[i].Change = ChangeType.None;

					editorResolved.Document.Lines[i].BackColor = Color.Empty;
					editorOriginal.Document.Lines[i].BackColor = Color.Empty;

					editorResolved.Document.Lines[i].Text = diffOutput.RightLines[i].Text;
					editorOriginal.Document.Lines[i].Text = diffOutput.LeftLines[i].Text;
				}
			}
			else
			{
				// Delete the lines
				for (int i = lastLine; i >= firstLine; i--)
				{
					editorResolved.Document.Lines[i].BackColor = Color.Empty;
					editorOriginal.Document.Lines[i].BackColor = Color.Empty;

					diffOutput.RemoveLine(i);

					editorResolved.Document.Lines.RemoveAt(i);
					editorOriginal.Document.Lines.RemoveAt(i);
				}
			}

			mergeEditor.ModifyingEditorText = false;

			AddButtons();
			mergeEditor.SetLineNumbers();
			mergeEditor.HasUnsavedChanges = true;
		}

		private void ApplyButton_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			int firstLine = (int)button.Tag;
			mergeEditor.ModifyingEditorText = true;
			// Find the last line
			int lastLine = firstLine;
			VisualDiffOutput diffOutput = mergeEditor.CurrentDiffInfo;
			ChangeType currentChangeType = diffOutput.LeftLines[firstLine].Change;

			if (currentChangeType == diffOutput.RightLines[lastLine].Change && diffOutput.RightLines[firstLine].IsVirtual)
			{
                while (lastLine < diffOutput.RightLines.Count - 1 && 
                        currentChangeType == diffOutput.RightLines[lastLine + 1].Change &&
				        diffOutput.RightLines[lastLine + 1].IsVirtual)
				{
					lastLine++;
				}
			}
			else
			{
                while (lastLine < diffOutput.LeftLines.Count - 1 &&
                        diffOutput.LeftLines[lastLine + 1].Change == currentChangeType &&
				        diffOutput.LeftLines[lastLine + 1].IsVirtual == false)
				{
					lastLine++;
				}
			}

			// Copy all the lines over from left to right.
			for (int i = firstLine; i <= lastLine; i++)
			{
				editorResolved.Document.Lines[i].Text = editorOriginal.Document.Lines[i].Text;
				diffOutput.RightLines[i].Text = diffOutput.LeftLines[i].Text;
				diffOutput.RightLines[i].Change = ChangeType.None;
				diffOutput.LeftLines[i].Change = ChangeType.None;
				diffOutput.RightLines[i].IsVirtual = false;
				diffOutput.LeftLines[i].IsVirtual = false;
				editorResolved.Document.Lines[i].BackColor = Color.Empty;
				editorOriginal.Document.Lines[i].BackColor = Color.Empty;
			}

			VisualDiffOutput.ConflictRange conflictRange;
			if (diffOutput.IsLineInConflict(firstLine, out conflictRange))
			{
				for (int i = conflictRange.EndLineIndex - 1; i >= conflictRange.StartLineIndex; i--)
				{
					diffOutput.LeftLines[i].IsVirtual = false;
					diffOutput.RightLines[i].IsVirtual = false;
					// The copied lines in the conflict region are now marked with ChangeType.None
					if (diffOutput.LeftLines[i].Change == ChangeType.None) continue;

					editorResolved.Document.Lines[i].BackColor = Color.Empty;
					editorOriginal.Document.Lines[i].BackColor = Color.Empty;
					editorOriginal.Document.Lines.RemoveAt(i);
					editorResolved.Document.Lines.RemoveAt(i);
					diffOutput.RemoveLine(i);
				}
			}

			diffOutput.ConflictRanges.Remove(conflictRange);
			mergeEditor.ModifyingEditorText = false;

			AddButtons();
			mergeEditor.SetLineNumbers();
			mergeEditor.HasUnsavedChanges = true;
		}
	}

	/// <summary>
	/// Does nothing when used. Removes the need for checking whether a DiffDisplayAlgorithm has been constructed.
	/// </summary>
	internal class NullDiffDisplayAlgorithm : DiffDisplayAlgorithm
	{
		public NullDiffDisplayAlgorithm(ucTextMergeEditor editor)
			: base(editor)
		{
		}

		public override VisualDiffOutput GetDiffOutput()
		{
			return new VisualDiffOutput();
		}

		//public override void AddButtons()
		//{
		//}

		//public override void ClearButtons()
		//{
		//}
	}

	internal class TwoWayDiffDisplayAlgorithm : DiffDisplayAlgorithm
	{
		private readonly List<Button> UseTemplateButtons = new List<Button>();
		private readonly List<Button> UseUserButtons = new List<Button>();

		public TwoWayDiffDisplayAlgorithm(ucTextMergeEditor mergeEditor)
			: base(mergeEditor)
		{
		}

		public override VisualDiffOutput GetDiffOutput()
		{
			TwoWayVisualDiff diffUtility = new TwoWayVisualDiff(fileInformation);
			return diffUtility.ProcessMergeOutput();
		}

		public new void AddButtons()
		{
			if (mergeEditor.EditingEnabled)
			{
				Common.Utility.SuspendPainting(mergeEditor);
				try
				{
					AddUserButtons();
					AddTemplateButtons();
				}
				finally
				{
					Common.Utility.ResumePainting(mergeEditor);
				}
			}
			else
			{
				ClearButtons();
			}
		}

		private void AddTemplateButtons()
		{
			ClearTemplateButtons();
			AddButton(editorOriginal, mergeEditor.CurrentDiffInfo.LeftLines, mergeEditor.OriginalButtonPanel,
					  UseTemplateButton_Click, UseTemplateButtons);
		}

		private void AddUserButtons()
		{
			ClearUserButtons();
			AddButton(editorResolved, mergeEditor.CurrentDiffInfo.RightLines, mergeEditor.ResolvedButtonPanel,
					  UseUserButton_Click, UseUserButtons);
		}

		private void AddButton(SyntaxEditor editor, IList<DiffLine> diffLines, Panel buttonPanel, EventHandler button_Click, List<Button> buttonList)
		{
			if (editor.Visible == false)
			{
				return;
			}
			if (diffLines.Count == 0)
				return;

			int firstDisplayLine = editor.SelectedView.FirstVisibleDisplayLineIndex;
			int lineHeight = editor.SelectedView.DisplayLineHeight;

			for (int i = firstDisplayLine; i < firstDisplayLine + editor.SelectedView.VisibleDisplayLineCount && i < editor.Document.Lines.Count; i++)
			{
				DocumentLine line = editor.SelectedView.DisplayLines[i].DocumentLine;
				if ((diffLines[line.Index].Change & ChangeType.UserAndTemplate) == 0) continue;
				// Someone Deleted a line
				CreateButton(firstDisplayLine, i, lineHeight, buttonPanel, "Use This", "Use the text block to the left in the merged text", button_Click, buttonList);
				i = GetLastUndoLine(i, diffLines);
			}
		}

		private void UseUserButton_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			int firstLine = (int)button.Tag;
			mergeEditor.ModifyingEditorText = true;

			// Find the last line
			VisualDiffOutput diffOutput = mergeEditor.CurrentDiffInfo;

			int lastLine = GetLastUndoLine(firstLine, diffOutput.RightLines);

			if (diffOutput.RightLines[firstLine].IsVirtual) // The user deleted the lines
			{
				// Delete the lines
				for (int i = lastLine; i >= firstLine; i--)
				{
					editorResolved.Document.Lines[i].BackColor = Color.Empty;
					editorOriginal.Document.Lines[i].BackColor = Color.Empty;

					diffOutput.RemoveLine(i);

					editorResolved.Document.Lines.RemoveAt(i);
					editorOriginal.Document.Lines.RemoveAt(i);
				}
			}
			else
			{
				// Mark the lines as ChangeType.None, as the changes have been accepted.
				for (int i = lastLine; i >= firstLine; i--)
				{
					diffOutput.RightLines[i].IsVirtual = diffOutput.LeftLines[i].IsVirtual = false;
					diffOutput.RightLines[i].Change = diffOutput.LeftLines[i].Change = ChangeType.None;

					editorResolved.Document.Lines[i].BackColor = Color.Empty;
					editorOriginal.Document.Lines[i].BackColor = Color.Empty;
				}
			}

			mergeEditor.ModifyingEditorText = false;

			AddButtons();
			mergeEditor.SetLineNumbers();

		}

		private void UseTemplateButton_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			int firstLine = (int)button.Tag;
			mergeEditor.ModifyingEditorText = true;

			// Find the last line
			VisualDiffOutput diffOutput = mergeEditor.CurrentDiffInfo;

			int lastLine = GetLastUndoLine(firstLine, diffOutput.LeftLines);

			if (diffOutput.LeftLines[firstLine].IsVirtual) // The template deleted the lines
			{
				// Delete the lines
				for (int i = lastLine; i >= firstLine; i--)
				{
					editorResolved.Document.Lines[i].BackColor = Color.Empty;
					editorOriginal.Document.Lines[i].BackColor = Color.Empty;

					diffOutput.RemoveLine(i);

					editorResolved.Document.Lines.RemoveAt(i);
					editorOriginal.Document.Lines.RemoveAt(i);
				}
			}
			else
			{
				// Mark the lines as ChangeType.None and copy the text left to right, as the template changes have been accepted.
				for (int i = lastLine; i >= firstLine; i--)
				{
					diffOutput.LeftLines[i].Text = diffOutput.RightLines[i].Text;
					diffOutput.RightLines[i].IsVirtual = diffOutput.LeftLines[i].IsVirtual = false;
					diffOutput.RightLines[i].Change = diffOutput.LeftLines[i].Change = ChangeType.None;

					editorResolved.Document.Lines[i].BackColor = Color.Empty;
					editorOriginal.Document.Lines[i].BackColor = Color.Empty;

					editorResolved.Document.Lines[i].Text = editorOriginal.Document.Lines[i].Text;
				}
			}

			mergeEditor.ModifyingEditorText = false;

			AddButtons();
			mergeEditor.SetLineNumbers();
		}

		public new void ClearButtons()
		{
			Common.Utility.SuspendPainting(mergeEditor);
			try
			{
				ClearTemplateButtons();
				ClearUserButtons();
			}
			finally
			{
				Common.Utility.ResumePainting(mergeEditor);
			}
		}

		private void ClearTemplateButtons()
		{
			ClearButtons(mergeEditor.OriginalButtonPanel, UseTemplateButtons);
		}

		private void ClearUserButtons()
		{
			ClearButtons(mergeEditor.ResolvedButtonPanel, UseUserButtons);
		}
	}

	internal class ThreeWayDiffDisplayAlgorithm : DiffDisplayAlgorithm
	{
		public ThreeWayDiffDisplayAlgorithm(ucTextMergeEditor editor)
			: base(editor)
		{
		}

		public override VisualDiffOutput GetDiffOutput()
		{
			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(fileInformation);
			return diffUtility.ProcessMergeOutput();
		}
	}
}
