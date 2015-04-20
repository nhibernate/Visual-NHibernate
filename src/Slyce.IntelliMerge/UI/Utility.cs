using System;
using System.Collections.Generic;
using System.Drawing;
using ActiproSoftware.SyntaxEditor;
using Algorithm.Diff;
using Slyce.Common;
using Slyce.IntelliMerge.Controller;

namespace Slyce.IntelliMerge.UI
{
	public static class Utility
	{
		public static Color ColourNewGen = Color.FromArgb(135, 172, 220);

		public static Color ColourUser = Color.FromArgb(224, 234, 243);
		public const string ColourNewGenString = "135, 172, 220";
		public const string ColourUserString = "224, 234, 243";
		private static Color addedMarkerColour = Color.FromArgb(108, 226, 108); // Green
		private static Color changedMarkerColour = Color.FromArgb(255, 238, 98); // Yellow
		private static Color deletedMarkerColour = Color.Red;// Color.FromArgb(230, 176, 165); // Red

		public static SyntaxLanguage GetSyntaxLanguageForFileInformation(IFileInformation fi)
		{
			if (fi.TemplateLanguage.HasValue)
			{
				return SyntaxEditorHelper.GetDynamicLanguage(fi.TemplateLanguage.Value);
			}

			return SyntaxEditorHelper.GetDynamicLanguage(TemplateContentLanguage.PlainText);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="editor"></param>
		/// <param name="text"></param>
		/// <param name="lines1"></param>
		/// <param name="lines2"></param>
		public static void PopulateSyntaxEditor(ActiproSoftware.SyntaxEditor.SyntaxEditor editor, string text, SlyceMerge.LineSpan[] lines1, SlyceMerge.LineSpan[] lines2)
		{
			PopulateSyntaxEditor(editor, text, lines1, lines2, false);
		}

		/// <summary>
		/// Populates a single Actipro SyntaxEditor with diff-highlighted text.
		/// </summary>
		/// <param name="editor">Actipro SyntaxEditor</param>
		/// <param name="text">Fully combined text.</param>
		/// <param name="newLines">Lines unique to the new file.</param>
		/// <param name="oldLines">Lines unique to the original file.</param>
		/// <param name="strikeoutLine2Lines"></param>
		public static void PopulateSyntaxEditor(ActiproSoftware.SyntaxEditor.SyntaxEditor editor, string text, SlyceMerge.LineSpan[] newLines, SlyceMerge.LineSpan[] oldLines, bool strikeoutLine2Lines)
		{
			editor.Text = text;
			SpanIndicatorLayer layer = null;
			HighlightingStyle highlightingStyle = null;
			List<int> linesToNotCount = new List<int>();

			if (strikeoutLine2Lines)
			{
				layer = new SpanIndicatorLayer("Diff", 1000);
				highlightingStyle = new HighlightingStyle("Diff", null, Color.Empty, Color.Empty);
				highlightingStyle.StrikeOutStyle = HighlightingStyleLineStyle.Solid;
				highlightingStyle.StrikeOutColor = Color.Red;
				editor.Document.SpanIndicatorLayers.Add(layer);
			}
			for (int i = 0; i < oldLines.Length; i++)
			{
				if (strikeoutLine2Lines)
				{
					for (int lineCounter = oldLines[i].StartLine; lineCounter <= oldLines[i].EndLine; lineCounter++)
					{
						editor.Document.Lines[lineCounter].BackColor = Color.MistyRose;// ColourUser;
						editor.Document.Lines[lineCounter].SelectionMarginMarkColor = deletedMarkerColour;// changedMarkerColour;
						editor.Document.Lines[lineCounter].CustomLineNumber = String.Empty;
						layer.Add(new HighlightingStyleSpanIndicator("Diff", highlightingStyle), editor.Document.Lines[lineCounter].TextRange);
						linesToNotCount.Add(lineCounter);
					}
				}
				else
				{
					for (int lineCounter = oldLines[i].StartLine; lineCounter <= oldLines[i].EndLine; lineCounter++)
					{
						editor.Document.Lines[lineCounter].BackColor = ColourNewGen;
						editor.Document.Lines[lineCounter].SelectionMarginMarkColor = addedMarkerColour;
					}
				}
			}
			for (int i = 0; i < newLines.Length; i++)
			{
				for (int lineCounter = newLines[i].StartLine; lineCounter <= newLines[i].EndLine; lineCounter++)
				{
					editor.Document.Lines[lineCounter].BackColor = Color.Honeydew;// ColourUser;
					editor.Document.Lines[lineCounter].SelectionMarginMarkColor = addedMarkerColour;// changedMarkerColour;
				}
			}
			// Fix-up changed vs. new/deleted
			for (int i = 0; i < editor.Document.Lines.Count; i++)
			{
				if (editor.Document.Lines[i].SelectionMarginMarkColor == addedMarkerColour)
				{
					int startLine = i;
					int endLine = -1;
					bool changeProcessed = false;

					for (int checkCounter = i + 1; checkCounter < editor.Document.Lines.Count; checkCounter++)
					{
						if (changeProcessed)
						{
							break;
						}
						if (editor.Document.Lines[checkCounter].SelectionMarginMarkColor == addedMarkerColour)
						{
							continue;
						}
						else if (editor.Document.Lines[checkCounter].SelectionMarginMarkColor == deletedMarkerColour)
						{
							// We have found a change
							for (int deleteCounter = checkCounter + 1; deleteCounter < editor.Document.Lines.Count; deleteCounter++)
							{
								if (editor.Document.Lines[deleteCounter].SelectionMarginMarkColor != deletedMarkerColour)
								{
									endLine = deleteCounter - 1;

									// Apply the Change colouring
									for (int changeCounter = startLine; changeCounter <= endLine; changeCounter++)
									{
										editor.Document.Lines[changeCounter].SelectionMarginMarkColor = changedMarkerColour;
										editor.Document.Lines[changeCounter].BackColor = Color.LightYellow;
									}
									changeProcessed = true;
									// We are back to 'normal' lines - no change found
									i = checkCounter;
									break;
								}
								else
								{
									editor.Document.Lines[deleteCounter].CustomLineNumber = string.Empty;
								}
							}
						}
						else
						{
							// We are back to 'normal' lines - no change found
							i = checkCounter;
							break;
						}
					}
				}
			}
			int lineNumber = 1;
			for (int i = 0; i < editor.Document.Lines.Count; i++)
			{
				if (linesToNotCount.Contains(i))
				{
					editor.Document.Lines[i].CustomLineNumber = string.Empty;
				}
				else
				{
					editor.Document.Lines[i].CustomLineNumber = lineNumber.ToString();
					lineNumber++;
				}
			}
		}

		///// <summary>
		///// Diffs two files and displays the diffed content in a single SyntaxEditor, with no striking out of lines.
		///// </summary>
		///// <param name="editor"></param>
		///// <param name="fileBodyLeft"></param>
		///// <param name="fileBodyRight"></param>
		///// <returns></returns>
		//public static bool Perform2WayDiffInSingleEditor(
		//     ActiproSoftware.SyntaxEditor.SyntaxEditor editor,
		//     ref string fileBodyLeft,
		//     ref string fileBodyRight)
		//{
		//    return Perform2WayDiffInSingleEditor(editor, ref fileBodyLeft, ref fileBodyRight, true);
		//}

		public static bool Perform2WayDiffInSingleEditor(SyntaxEditor editor, string text1, string text2)
		{
			text1 = text1.Replace("\r\n", "\n");
			text2 = text2.Replace("\r\n", "\n");

			var dmp = new DiffMatchPatch.diff_match_patch();
			dmp.Diff_Timeout = 5.0F;
			dmp.Diff_EditCost = 4;
			dmp.Match_Threshold = 0.5F;

			var diffs = dmp.diff_main(text1, text2, true);
			dmp.diff_cleanupSemantic(diffs);

			if (diffs.Count == 1 && diffs[0].operation == DiffMatchPatch.Operation.EQUAL)
			{
				editor.Document.Text = diffs[0].text;
				return true;
			}
			DiffInSingleEditor(diffs, editor);
			return false;
		}

		private static void DiffInSingleEditor(List<DiffMatchPatch.Diff> diffs, SyntaxEditor editor)
		{
			//editor.SuspendLayout();
			//editor.SuspendPainting();
			Document doc = new Document();

			Color backColorGreen = Color.FromArgb(230, 255, 230);
			Color backColorRed = Color.FromArgb(255, 230, 230);

			#region Deleted style
			SpanIndicatorLayer deletedLayer = new SpanIndicatorLayer("Deleted", 1000);
			HighlightingStyle deletedHighlightingStyle = new HighlightingStyle("Deleted", null, Color.Empty, backColorRed)
			{
				StrikeOutStyle = HighlightingStyleLineStyle.Solid,
				StrikeOutColor = Color.Red
			};
			doc.SpanIndicatorLayers.Add(deletedLayer);
			#endregion

			#region New style
			SpanIndicatorLayer newLayer = new SpanIndicatorLayer("New", 1000);
			HighlightingStyle newHighlightingStyle = new HighlightingStyle("New", null, Color.Empty, backColorGreen);
			doc.SpanIndicatorLayers.Add(newLayer);
			#endregion

			System.Text.StringBuilder sb = new System.Text.StringBuilder(10000);

			foreach (DiffMatchPatch.Diff aDiff in diffs)
				sb.Append(aDiff.text);

			doc.Text = sb.ToString();

			int start = 0;
			int endOffset = 0;
			int i = 0;

			foreach (DiffMatchPatch.Diff aDiff in diffs)
			{
				int diffLength = aDiff.text.Length;

				switch (aDiff.operation)
				{
					case DiffMatchPatch.Operation.INSERT://green
						start = i;
						endOffset = i + diffLength;

						if (endOffset > start)
							newLayer.Add(new HighlightingStyleSpanIndicator("New", newHighlightingStyle), new TextRange(start, endOffset));

						break;
					case DiffMatchPatch.Operation.DELETE://red
						start = i;
						endOffset = i + diffLength;

						if (endOffset > start)
							deletedLayer.Add(new HighlightingStyleSpanIndicator("Deleted", deletedHighlightingStyle), new TextRange(start, endOffset));

						break;
					//case Operation.EQUAL:
					//    start = i;// editor.Document.GetText(LineTerminator.Newline).Length;
					//    //editor.Document.InsertText(DocumentModificationType.Custom, start, aDiff.text);
					//    //editor.Document.AppendText(aDiff.text);
					//    break;
				}
				//if (aDiff.operation != Operation.DELETE)
				i += diffLength;
			}
			editor.Document = doc;
			//editor.SelectedView.EnsureVisible(1, false);
			//editor.ResumeLayout();
			//editor.ResumePainting();
		}

		/// <summary>
		/// Diffs two files and displays the diffed content in a single SyntaxEditor.
		/// </summary>
		/// <param name="editor"></param>
		/// <param name="fileBodyLeft"></param>
		/// <param name="fileBodyRight"></param>
		/// <param name="strikeoutRightLines"></param>
		/// <returns></returns>
		public static bool Perform2WayDiffInSingleEditor(
			 ActiproSoftware.SyntaxEditor.SyntaxEditor editor,
			 ref string fileBodyLeft,
			 ref string fileBodyRight,
			 bool strikeoutRightLines)
		{
			string combinedText;
			SlyceMerge.LineSpan[] userLines;
			SlyceMerge.LineSpan[] templateLines;
			SlyceMerge.PerformTwoWayDiff(false, fileBodyLeft, fileBodyRight, out userLines, out templateLines, out combinedText);
			PopulateSyntaxEditor(editor, combinedText, userLines, templateLines, strikeoutRightLines);
			bool filesTheSame = userLines.Length == 0 && templateLines.Length == 0;
			return filesTheSame;
		}

		/// <summary>
		/// Performs a Diff between two strings and displays them in two SyntaxEditors with offset coloured lines.
		/// </summary>
		/// <param name="editorLeft"></param>
		/// <param name="editorRight"></param>
		/// <param name="fileBodyLeft"></param>
		/// <param name="fileBodyRight"></param>
		/// <returns></returns>
		public static bool Perform2WayDiffInTwoEditors(
			 ActiproSoftware.SyntaxEditor.SyntaxEditor editorLeft,
			 ActiproSoftware.SyntaxEditor.SyntaxEditor editorRight,
			 ref string fileBodyLeft,
			 ref string fileBodyRight)
		{
			string combinedText;
			SlyceMerge.LineSpan[] userLines;
			SlyceMerge.LineSpan[] templateLines;
			SlyceMerge.PerformTwoWayDiff(false, fileBodyLeft, fileBodyRight, out userLines, out templateLines, out combinedText);
			Slyce.IntelliMerge.UI.Utility.PopulateSyntaxEditors(editorLeft, editorRight, combinedText, userLines, templateLines);
			bool filesTheSame = userLines.Length == 0 && templateLines.Length == 0;
			return filesTheSame;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="editorLeft"></param>
		/// <param name="editorRight"></param>
		/// <param name="fileBodyLeft"></param>
		/// <param name="fileBodyRight"></param>
		/// <returns>Returns true if files the same, false if they differ</returns>
		public static bool PerformDiff(ActiproSoftware.SyntaxEditor.SyntaxEditor editorLeft, ActiproSoftware.SyntaxEditor.SyntaxEditor editorRight, ref string fileBodyLeft, ref string fileBodyRight)
		{
			bool filesTheSame = true;
			editorLeft.Text = "";
			editorRight.Text = "";
			string file1 = fileBodyLeft;
			string file2 = fileBodyRight;

			// Performs a line-by-line, case-insensitive comparison.
			string[] arrFile1 = file1.Split('\n');
			string[] arrFile2 = file2.Split('\n');
			Diff diff = new Diff(arrFile1, arrFile2, true, false);

			foreach (Diff.Hunk hunk in diff)
			{
				int lineMismatch = hunk.Right.Count - hunk.Left.Count;
				Color leftCol;
				Color rightCol;
				bool iii = hunk.Same;

				if (!hunk.Same) { filesTheSame = false; }

				if (hunk.Same)
				{
					leftCol = Color.White;
					rightCol = Color.White;
				}
				else if (lineMismatch < 0)
				{
					leftCol = ColourNewGen;
					rightCol = Color.Silver;
				}
				else
				{
					leftCol = Color.Silver;
					rightCol = leftCol = ColourUser;
				}
				for (int i = hunk.Left.Start; i <= hunk.Left.End; i++)
				{
					string leftLine = arrFile1[i];

					if (!hunk.Same)
					{
						leftCol = ColourNewGen;
						editorLeft.Document.AppendText(arrFile1[i]);

						if (editorLeft.Document.Lines.Count > 1)
						{
							editorLeft.Document.Lines[editorLeft.Document.Lines.Count - 2].BackColor = leftCol;
						}
					}
					else
					{
						editorLeft.Document.AppendText(arrFile1[i]);

						if (editorLeft.Document.Lines.Count > 1)
						{
							editorLeft.Document.Lines[editorLeft.Document.Lines.Count - 2].BackColor = leftCol;
						}
					}
				}
				for (int i = hunk.Right.Start; i <= hunk.Right.End; i++)
				{
					string rightLine = arrFile2[i];

					if (!hunk.Same)
					{
						leftCol = ColourUser;
						editorRight.Document.AppendText(arrFile2[i]);

						if (editorRight.Document.Lines.Count > 2)
						{
							editorRight.Document.Lines[editorRight.Document.Lines.Count - 2].BackColor = leftCol;
						}
					}
					else
					{
						editorRight.Document.AppendText(arrFile2[i]);

						if (editorRight.Document.Lines.Count > 1)
						{
							editorRight.Document.Lines[editorRight.Document.Lines.Count - 2].BackColor = rightCol;
						}
					}
				}
				if (hunk.Left.Count < hunk.Right.Count)
				{
					for (int i = 0; i < hunk.Right.Count - hunk.Left.Count; i++)
					{
						editorLeft.Document.AppendText("" + Environment.NewLine);
						editorLeft.Document.Lines[editorLeft.Document.Lines.Count - 2].BackColor = Color.Silver;
					}
				}
				if (hunk.Right.Count < hunk.Left.Count)
				{
					for (int i = 0; i < hunk.Left.Count - hunk.Right.Count; i++)
					{
						editorRight.Document.AppendText("" + Environment.NewLine);
						editorRight.Document.Lines[editorRight.Document.Lines.Count - 2].BackColor = Color.Silver;
					}
				}
			}
			return filesTheSame;
		}

		/// <summary>
		/// Populates two Actipro SyntaxEditors with diff-highlighted text.
		/// </summary>
		/// <param name="editor1">Actipro SyntaxEditor</param>
		/// <param name="editor2">Actipro SyntaxEditor</param>
		/// <param name="text">Fully combined text.</param>
		/// <param name="lines1">Lines unique to the left file.</param>
		/// <param name="lines2">Lines unique to the right file.</param>
		public static void PopulateSyntaxEditors(
  ActiproSoftware.SyntaxEditor.SyntaxEditor editor1,
  ActiproSoftware.SyntaxEditor.SyntaxEditor editor2,
  string text,
  SlyceMerge.LineSpan[] lines1,
  SlyceMerge.LineSpan[] lines2)
		{
			editor1.Text = text;
			editor2.Text = text;

			for (int i = 0; i < lines1.Length; i++)
			{
				for (int lineCounter = lines1[i].StartLine; lineCounter <= lines1[i].EndLine; lineCounter++)
				{
					editor1.Document.Lines[lineCounter].BackColor = ColourNewGen;
					editor2.Document.Lines[lineCounter].BackColor = Color.LightGray;
					editor2.Document.DeleteText(ActiproSoftware.SyntaxEditor.DocumentModificationType.Delete, editor2.Document.Lines[lineCounter].StartOffset, editor2.Document.Lines[lineCounter].Length);
				}
			}
			for (int i = 0; i < lines2.Length; i++)
			{
				for (int lineCounter = lines2[i].StartLine; lineCounter <= lines2[i].EndLine; lineCounter++)
				{
					editor2.Document.Lines[lineCounter].BackColor = ColourNewGen;
					editor1.Document.Lines[lineCounter].BackColor = Color.LightGray;
					editor1.Document.DeleteText(ActiproSoftware.SyntaxEditor.DocumentModificationType.Delete, editor1.Document.Lines[lineCounter].StartOffset, editor1.Document.Lines[lineCounter].Length);
				}
			}
			// Compact the displays
			int lineCount1 = 0;
			int lineCount2 = 0;

			for (int i = editor1.Document.Lines.Count - 1; i >= -1; i--)
			{
				Color lineColor = Color.Empty;

				if (i >= 0)
				{
					lineColor = editor1.Document.Lines[i].BackColor;
				}
				if (lineColor == Color.Empty && (lineCount1 + lineCount2) > 0)
				{
					// Process counted lines
					int startIndex = i + 1;
					int condensedLineCount = Math.Max(lineCount1, lineCount2);
					int numLinesToRemove = lineCount1 + lineCount2 - condensedLineCount;
					int lastLine = startIndex + lineCount1 + lineCount2 - 1;

					//if (numLinesToRemove > 0)
					//{
					//   // Walk backward when processing Left
					//   for (int removeIndex = lastLine; removeIndex >= startIndex + condensedLineCount; removeIndex--)
					//   {
					//      editor1.Document.Lines[removeIndex].BackColor = editor1.Document.Lines[removeIndex + 1].BackColor;
					//      editor1.Document.Lines.RemoveAt(removeIndex);

					//      int newPos = lastLine - condensedLineCount - (lastLine - removeIndex);
					//      string gg = editor2.Document.Lines[removeIndex].Text;
					//      editor2.Document.Lines[newPos].Text = editor2.Document.Lines[removeIndex].Text;
					//      editor2.Document.Lines[newPos].BackColor = editor2.Document.Lines[removeIndex].BackColor;
					//      editor2.Document.Lines[removeIndex].BackColor = editor2.Document.Lines[removeIndex + 1].BackColor;
					//      editor2.Document.Lines.RemoveAt(removeIndex);
					//   }
					//}
					numLinesToRemove = Math.Min(lineCount1, lineCount2);
					int linesRemoved1 = 0;
					int linesRemoved2 = 0;

					for (int x = startIndex + (lineCount1 + lineCount2); x >= startIndex; x--)
					{
						if (linesRemoved1 < numLinesToRemove &&
							editor1.Document.Lines[x].BackColor == Color.LightGray)
						{
							editor1.Document.Lines[x].BackColor = editor1.Document.Lines[x + 1].BackColor;
							editor1.Document.Lines.RemoveAt(x);
							linesRemoved1++;
						}
						if (linesRemoved2 < numLinesToRemove &&
							editor2.Document.Lines[x].BackColor == Color.LightGray)
						{
							editor2.Document.Lines[x].BackColor = editor2.Document.Lines[x + 1].BackColor;
							editor2.Document.Lines.RemoveAt(x);
							linesRemoved2++;
						}
					}
					if (linesRemoved1 != linesRemoved2)
						throw new Exception("Non-equal number of lines removed.");

					//if (lineCount1 > lineCount2)
					//{
					//   // Remove trailing gray lines from editor2
					//   for (int removeIndex = startIndex; removeIndex <= startIndex + numLinesToRemove; removeIndex++)
					//   {
					//      string q111 = editor1.Document.Lines[removeIndex].Text;
					//      string gg = editor2.Document.Lines[removeIndex].Text;
					//      editor2.Document.Lines.RemoveAt(removeIndex);
					//   }
					//}
					//else if (lineCount2 > lineCount1)
					//{
					//   // Remove trailing gray lines from editor1
					//   for (int removeIndex = lastLine; removeIndex >= startIndex + condensedLineCount; removeIndex--)
					//   {
					//      string q111 = editor1.Document.Lines[removeIndex].Text;
					//      string gg = editor2.Document.Lines[removeIndex].Text;
					//      editor1.Document.Lines.RemoveAt(removeIndex);
					//   }
					//}
					lineCount1 = 0;
					lineCount2 = 0;
					continue;
				}
				else if (lineColor == ColourNewGen)
				{
					lineCount1++;
				}
				else if (lineColor == Color.LightGray)
				{
					lineCount2++;
				}
			}
			// Line Marker Colours, Strikethroughs
			string layerKey = "Diff";
			string indicatorKey = "Diff";
			SpanIndicatorLayer layer = new SpanIndicatorLayer(layerKey, 1000);
			HighlightingStyle highlightingStyle = new HighlightingStyle("Diff", null, Color.Empty, Color.Empty);
			highlightingStyle.StrikeOutStyle = HighlightingStyleLineStyle.Solid;
			highlightingStyle.StrikeOutColor = Color.Red;
			editor1.Document.SpanIndicatorLayers.Add(layer);

			int lineNumber1 = 1;
			int lineNumber2 = 1;

			for (int i = 0; i < editor1.Document.Lines.Count; i++)
			{
				// Set the line marker colours
				if (editor1.Document.Lines[i].BackColor == Color.LightGray &&
					editor2.Document.Lines[i].BackColor == ColourNewGen)
				{
					if (i > 0 && editor2.Document.Lines[i - 1].SelectionMarginMarkColor == changedMarkerColour)
					{
						editor2.Document.Lines[i].SelectionMarginMarkColor = changedMarkerColour;
						editor2.Document.Lines[i].BackColor = Color.LightYellow;
					}
					else
					{
						editor2.Document.Lines[i].SelectionMarginMarkColor = addedMarkerColour;
						editor2.Document.Lines[i].BackColor = Color.Honeydew;
					}
					editor1.Document.Lines[i].BackColor = Color.WhiteSmoke;
					editor1.Document.Lines[i].CustomLineNumber = string.Empty;
					editor2.Document.Lines[i].CustomLineNumber = lineNumber2.ToString();
					lineNumber2++;
				}
				else if (editor1.Document.Lines[i].BackColor == ColourNewGen &&
					editor2.Document.Lines[i].BackColor == Color.LightGray)
				{
					editor2.Document.Lines[i].SelectionMarginMarkColor = deletedMarkerColour;
					editor1.Document.Lines[i].BackColor = Color.MistyRose;
					editor2.Document.Lines[i].BackColor = Color.WhiteSmoke;
					editor1.Document.Lines[i].CustomLineNumber = lineNumber1.ToString();
					editor2.Document.Lines[i].CustomLineNumber = string.Empty;
					lineNumber1++;
				}
				else if (editor1.Document.Lines[i].BackColor == ColourNewGen &&
					editor2.Document.Lines[i].BackColor == ColourNewGen)
				{
					editor2.Document.Lines[i].SelectionMarginMarkColor = changedMarkerColour;
					editor1.Document.Lines[i].BackColor = Color.LightYellow;
					editor2.Document.Lines[i].BackColor = Color.LightYellow;
					layer.Add(new HighlightingStyleSpanIndicator(indicatorKey, highlightingStyle), editor1.Document.Lines[i].TextRange);
					editor1.Document.Lines[i].CustomLineNumber = lineNumber1.ToString();
					editor2.Document.Lines[i].CustomLineNumber = lineNumber2.ToString();
					lineNumber1++;
					lineNumber2++;
				}
				else
				{
					editor1.Document.Lines[i].CustomLineNumber = lineNumber1.ToString();
					editor2.Document.Lines[i].CustomLineNumber = lineNumber2.ToString();
					lineNumber1++;
					lineNumber2++;
				}
			}
		}


	}
}
