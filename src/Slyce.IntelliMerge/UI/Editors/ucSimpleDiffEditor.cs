using System;
using System.Drawing;
using System.Windows.Forms;

namespace Slyce.IntelliMerge.UI.Editors
{
	public partial class ucSimpleDiffEditor : UserControl
	{
		public enum DisplayStyles
		{
			SingleEditor,
			TwoEditors
		}
		private DisplayStyles CurrentDisplayStyle;
		private string OldFileContents;
		private string NewFileContents;
		private string RelativePath;
		private bool OldFileExists;
		private bool FilesAreSame = false;
		public bool BusyPopulating = false;

		public ucSimpleDiffEditor(string relativePath, bool oldFileExists, string oldFileContents, string newFileContents, bool filesAreSame, DisplayStyles displayStyle)
		{
			InitializeComponent();

			//toolStripButtonDoubleEditor.Visible = false;
			//toolStripButtonSingleEditor.Visible = false;

			Reset(relativePath, oldFileExists, oldFileContents, newFileContents, filesAreSame, displayStyle);
		}

		public void Reset(string relativePath, bool oldFileExists, string oldFileContents, string newFileContents, bool filesAreSame, DisplayStyles displayStyle)
		{
			RelativePath = relativePath;
			OldFileExists = oldFileExists;
			OldFileContents = oldFileContents;
			NewFileContents = newFileContents;
			CurrentDisplayStyle = displayStyle;
			FilesAreSame = filesAreSame;
			Populate();
		}

		private void SetHeading()
		{
			string spacer = "  ";

			if (FilesAreSame)
			{
				labelHeading.Text = "No changes";
				labelFilePath.Text = spacer + RelativePath;
				labelFilePath.BackgroundStyle.BackColor = labelHeading.BackgroundStyle.BackColor = Color.Green;
				labelFilePath.BackgroundStyle.BackColor2 = labelHeading.BackgroundStyle.BackColor2 = Color.GreenYellow;
			}
			else if (OldFileExists)
			{
				labelHeading.Text = "File changed";
				labelFilePath.Text = spacer + RelativePath;
				labelFilePath.BackgroundStyle.BackColor = labelHeading.BackgroundStyle.BackColor = Color.Brown;
				labelFilePath.BackgroundStyle.BackColor2 = labelHeading.BackgroundStyle.BackColor2 = Color.Orange;
			}
			else
			{
				labelHeading.Text = "New file";
				labelFilePath.Text = spacer + RelativePath;
				labelFilePath.BackgroundStyle.BackColor = labelHeading.BackgroundStyle.BackColor = Color.Green;
				labelFilePath.BackgroundStyle.BackColor2 = labelHeading.BackgroundStyle.BackColor2 = Color.GreenYellow;
			}
			labelFilePath.Width = Convert.ToInt32(System.Drawing.Graphics.FromHwnd(labelFilePath.Handle).MeasureString(labelFilePath.Text, labelFilePath.Font).Width) + 10;
		}

		private void Populate()
		{
			try
			{
				BusyPopulating = true;
				editorNew.SuspendLayout();
				editorNew.SuspendPainting();
				editorOriginal.SuspendLayout();
				editorOriginal.SuspendPainting();

				//editorNew.Document.Language = Slyce.Common.SyntaxEditorHelper.GetSyntaxLanguageFromFileName(RelativePath);

				if (FilesAreSame)
				{
					ActiproSoftware.SyntaxEditor.Document doc = new ActiproSoftware.SyntaxEditor.Document();
					doc.Text = NewFileContents;
					editorNew.Document = doc;
					//editorNew.Text = NewFileContents;
					toolStripButtonNextChange.Enabled = false;
					toolStripButtonPrevChange.Enabled = false;
					toolStripButtonSingleEditor.Enabled = false;
					toolStripButtonDoubleEditor.Enabled = false;
					DisplaySingleEditor();
				}
				else if (OldFileExists)
				{
					string userFileBody = OldFileContents;

					switch (CurrentDisplayStyle)
					{
						case DisplayStyles.SingleEditor:
							//FilesAreSame = Slyce.IntelliMerge.UI.Utility.Perform2WayDiffInSingleEditor(editorNew, ref OldFileContents, ref NewFileContents);
							FilesAreSame = Slyce.IntelliMerge.UI.Utility.Perform2WayDiffInSingleEditor(editorNew, OldFileContents, NewFileContents);
							DisplaySingleEditor();
							break;
						case DisplayStyles.TwoEditors:
							FilesAreSame = Slyce.IntelliMerge.UI.Utility.Perform2WayDiffInTwoEditors(editorOriginal, editorNew, ref OldFileContents, ref NewFileContents);
							editorOriginal.Document.Language = editorNew.Document.Language;
							DisplayTwoEditors();
							break;
						default:
							throw new NotImplementedException("Not handled yet");
					}
					if (FilesAreSame)
					{
						toolStripButtonNextChange.Enabled = false;
						toolStripButtonPrevChange.Enabled = false;
						toolStripButtonSingleEditor.Enabled = false;
						toolStripButtonDoubleEditor.Enabled = false;
					}
					else
					{
						toolStripButtonNextChange.Enabled = true;
						toolStripButtonPrevChange.Enabled = true;
						toolStripButtonSingleEditor.Enabled = true;
						toolStripButtonDoubleEditor.Enabled = true;
					}
				}
				else
				{
					editorNew.Text = NewFileContents;
					toolStripButtonNextChange.Enabled = false;
					toolStripButtonPrevChange.Enabled = false;
					toolStripButtonSingleEditor.Enabled = false;
					toolStripButtonDoubleEditor.Enabled = false;
					DisplaySingleEditor();
				}
				editorNew.Document.Language = Slyce.Common.SyntaxEditorHelper.GetSyntaxLanguageFromFileName(RelativePath);
				SetHeading();
				// Set the scrollbar max
				editorNew.SelectedView.ScrollToDocumentStart();
				editorOriginal.SelectedView.ScrollToDocumentStart();
				scrollBar1.Maximum = editorNew.SelectedView.VerticalScrollBarMaximum;// editorNew.SelectedView.DisplayLines.Count - editorNew.SelectedView.VisibleDisplayLineCount;// +1;
				splitContainer1.SplitterDistance = splitContainer1.Width / 2;
				scrollBar1.Value = 0;
			}
			finally
			{
				editorNew.ResumeLayout();
				editorNew.ResumePainting();
				editorOriginal.ResumeLayout();
				editorOriginal.ResumePainting();
				BusyPopulating = false;
			}
		}

		private void DisplaySingleEditor()
		{
			splitContainer1.Panel2.Controls.Remove(editorNew);
			panel1.Controls.Remove(splitContainer1);
			panel1.Controls.Add(editorNew);
			editorNew.Dock = DockStyle.Fill;
			editorNew.ScrollBarType = ActiproSoftware.SyntaxEditor.ScrollBarType.Both;
			editorNew.Focus();
			scrollBar1.Visible = false;
			toolStripButtonSingleEditor.BackColor = SystemColors.ControlDark;
			toolStripButtonDoubleEditor.BackColor = SystemColors.Control;
		}

		private void DisplayTwoEditors()
		{
			panel1.Controls.Remove(editorNew);
			panel1.Controls.Add(splitContainer1);
			splitContainer1.Panel2.Controls.Add(editorNew);
			editorNew.BringToFront();
			editorNew.Dock = DockStyle.Fill;
			editorNew.ScrollBarType = ActiproSoftware.SyntaxEditor.ScrollBarType.ForcedHorizontal;
			editorNew.Focus();
			scrollBar1.Visible = true;
			toolStripButtonSingleEditor.BackColor = SystemColors.Control;
			toolStripButtonDoubleEditor.BackColor = SystemColors.ControlDark;
		}

		private void scrollBar1_ValueChanged(object sender, EventArgs e)
		{
			if (BusyPopulating)
				return;

			if (scrollBar1.Maximum != editorNew.SelectedView.VerticalScrollBarMaximum)
				scrollBar1.Maximum = editorNew.SelectedView.VerticalScrollBarMaximum;

			editorOriginal.SelectedView.FirstVisibleDisplayLineIndex = scrollBar1.Value;
			editorNew.SelectedView.FirstVisibleDisplayLineIndex = scrollBar1.Value;
		}

		private void editorNew_ViewVerticalScroll(object sender, ActiproSoftware.SyntaxEditor.EditorViewEventArgs e)
		{
			if (BusyPopulating)
				return;

			if (scrollBar1.Value != editorNew.SelectedView.FirstVisibleDisplayLineIndex)
			{
				if (editorNew.SelectedView.FirstVisibleDisplayLineIndex > scrollBar1.Maximum)
					scrollBar1.Maximum = editorNew.SelectedView.VerticalScrollBarMaximum;// editorNew.SelectedView.DisplayLines.Count - editorNew.SelectedView.VisibleDisplayLineCount + 1;

				scrollBar1.Value = editorNew.SelectedView.FirstVisibleDisplayLineIndex;
			}
		}

		private void editorOriginal_ViewVerticalScroll(object sender, ActiproSoftware.SyntaxEditor.EditorViewEventArgs e)
		{
			if (BusyPopulating)
				return;

			if (scrollBar1.Value != editorOriginal.SelectedView.FirstVisibleDisplayLineIndex)
			{
				if (editorOriginal.SelectedView.FirstVisibleDisplayLineIndex > scrollBar1.Maximum)
					scrollBar1.Maximum = editorNew.SelectedView.VerticalScrollBarMaximum;// editorNew.SelectedView.DisplayLines.Count - editorNew.SelectedView.VisibleDisplayLineCount + 1;

				scrollBar1.Value = editorOriginal.SelectedView.FirstVisibleDisplayLineIndex;
			}
		}

		private void splitContainer1_SizeChanged(object sender, EventArgs e)
		{

		}

		private void radioButtonSingleEditor_CheckedChanged(object sender, EventArgs e)
		{
			Slyce.Common.Utility.SuspendPainting(this);
			CurrentDisplayStyle = DisplayStyles.SingleEditor;
			Populate();
			Slyce.Common.Utility.ResumePainting(this);
		}

		private void radioButtonTwoEditors_CheckedChanged(object sender, EventArgs e)
		{
			Slyce.Common.Utility.SuspendPainting(this);
			CurrentDisplayStyle = DisplayStyles.TwoEditors;
			Populate();
			Slyce.Common.Utility.ResumePainting(this);
		}

		private void editorNew_ViewHorizontalScroll(object sender, ActiproSoftware.SyntaxEditor.EditorViewEventArgs e)
		{
			editorOriginal.HorizontalScroll.Value = editorNew.HorizontalScroll.Value;
		}

		private void editorOriginal_ViewHorizontalScroll(object sender, ActiproSoftware.SyntaxEditor.EditorViewEventArgs e)
		{
			editorNew.HorizontalScroll.Value = editorOriginal.HorizontalScroll.Value;
		}

		private void toolStripButtonSingleEditor_Click(object sender, EventArgs e)
		{
			Slyce.Common.Utility.SuspendPainting(this);
			CurrentDisplayStyle = DisplayStyles.SingleEditor;
			Populate();
			toolStripButtonSingleEditor.BackColor = SystemColors.ControlDark;
			toolStripButtonDoubleEditor.BackColor = SystemColors.Control;
			Slyce.Common.Utility.ResumePainting(this);
		}

		private void toolStripButtonDoubleEditor_Click(object sender, EventArgs e)
		{
			Slyce.Common.Utility.SuspendPainting(this);
			CurrentDisplayStyle = DisplayStyles.TwoEditors;
			Populate();
			toolStripButtonSingleEditor.BackColor = SystemColors.Control;
			toolStripButtonDoubleEditor.BackColor = SystemColors.ControlDark;
			Slyce.Common.Utility.ResumePainting(this);
		}

		private void toolStripButtonNextChange_Click(object sender, EventArgs e)
		{
			try
			{
				editorNew.SuspendPainting();
				editorOriginal.SuspendPainting();

				int currentLine = editorNew.SelectedView.CurrentDisplayLine.DocumentLineIndex;

				if (CurrentDisplayStyle == DisplayStyles.TwoEditors)
				{
					Color currentLineColor = editorNew.SelectedView.CurrentDisplayLine.DocumentLine.BackColor;
					bool colorHasChanged = false;

					for (int i = currentLine + 1; i < editorNew.SelectedView.DisplayLines.Count; i++)
					{
						Color backColor = editorNew.SelectedView.DisplayLines[i].DocumentLine.BackColor;

						if (!colorHasChanged && backColor != currentLineColor)
							colorHasChanged = true;

						if (colorHasChanged &&
							backColor != Color.Empty)
						{
							editorNew.Focus();
							editorNew.SelectedView.GoToLine(i);
							editorNew.SelectedView.ScrollLineToVisibleMiddle();
							break;
						}
					}
				}
				else
				{
					int currentNumSpans = editorNew.SelectedView.CurrentDisplayLine.DocumentLine.SpanIndicators.Count;
					bool numSpansHasChanged = false;

					for (int i = currentLine + 1; i < editorNew.SelectedView.DisplayLines.Count; i++)
					{
						int numSpans = editorNew.SelectedView.DisplayLines[i].DocumentLine.SpanIndicators.Count;

						if (!numSpansHasChanged && numSpans != currentNumSpans)
							numSpansHasChanged = true;

						if (numSpansHasChanged &&
							numSpans > 0)
						{
							editorNew.Focus();
							editorNew.SelectedView.GoToLine(i);
							editorNew.SelectedView.ScrollLineToVisibleMiddle();
							break;
						}
					}
				}
			}
			finally
			{
				editorNew.ResumePainting();
				editorOriginal.ResumePainting();
			}
		}

		private void toolStripButtonPrevChange_Click(object sender, EventArgs e)
		{
			try
			{
				int currentLine = editorNew.SelectedView.CurrentDisplayLine.DocumentLineIndex;
				Color currentLineColor = editorNew.SelectedView.CurrentDisplayLine.DocumentLine.BackColor;
				bool colorHasChanged = false;

				for (int i = currentLine - 1; i >= 0; i--)
				{
					Color backColor = editorNew.SelectedView.DisplayLines[i].DocumentLine.BackColor;

					if (!colorHasChanged && backColor != currentLineColor)
						colorHasChanged = true;

					if (colorHasChanged &&
						backColor != Color.Empty)
					{
						editorNew.SelectedView.GoToLine(i);
						currentLineColor = editorNew.SelectedView.DisplayLines[i].DocumentLine.BackColor;

						// We now have the next block of coloured lines....now get the first one
						for (int coloredCounter = i; coloredCounter >= 0; coloredCounter--)
						{
							backColor = editorNew.SelectedView.DisplayLines[coloredCounter].DocumentLine.BackColor;

							if (backColor == currentLineColor)
								editorNew.SelectedView.GoToLine(coloredCounter);
							else
								break;
						}
						editorNew.Focus();
						editorNew.SelectedView.ScrollToCaret();
						break;
					}
				}
			}
			finally
			{
				editorNew.ResumePainting();
				editorOriginal.ResumePainting();
			}
		}

		private void ucSimpleDiffEditor_Resize(object sender, EventArgs e)
		{
			int headerTextWidth = Convert.ToInt32(System.Drawing.Graphics.FromHwnd(labelHeading.Handle).MeasureString(labelHeading.Text, labelHeading.Font).Width);
			int headerTextStartPos = (labelHeading.Width - headerTextWidth) / 2;

			if (headerTextStartPos < labelFilePath.Width + 20)
				labelHeading.TextAlignment = StringAlignment.Far;
			else
				labelHeading.TextAlignment = StringAlignment.Center;
		}
	}
}
