using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Algorithm.Diff;

namespace Slyce.IntelliMerge
{
	///<summary>
	/// Result of a diff and merge.
	///</summary>
	public class SlyceMergeResult
	{
		public List<SlyceMerge.LineText> Lines;
		public TypeOfDiff DiffType = TypeOfDiff.ExactCopy;

		/// <summary>
		/// Returns whether the specified row is the start of a 'coloured' region.
		/// </summary>
		/// <param name="index">index of Lines array</param>
		/// <returns>true or false</returns>
		public bool IsAFirstRow(int index)
		{
			if (index == 0)
			{
				return true;
			}

			if (index >= Lines.Count)
			{
				return false;
			}

			SlyceMerge.TypeOfLine prevLine = Lines[index - 1].LineType;
			SlyceMerge.TypeOfLine line = Lines[index].LineType;

			return (prevLine != line);
		}

		internal void SetDiffType(TypeOfDiff diffType)
		{
			DiffType = SlyceMerge.CombineDiffTypes(DiffType, diffType);
		}
	}

	/// <summary>
	/// Summary description for SlyceMerge.
	/// </summary>
	public class SlyceMerge
	{
		#region Enums
		public enum ActionType
		{
			Accept,
			Edit,
			Delete,
			None,
			Up,
			Down
		}

		public enum TypeOfLine
		{
			Normal,
			/// <summary>
			/// User edited line.
			/// </summary>
			User,
			/// <summary>
			/// Template edited line.
			/// </summary>
			NewGen
		}
		#endregion

		#region Structs
		public struct LineText
		{
			public string Text;
			public int LineNumber;
			public TypeOfLine LineType;

			public LineText(string text, int lineNumber, TypeOfLine lineType)
			{
				Text = text;
				LineNumber = lineNumber;
				LineType = lineType;
			}
		}

		public class LineSpan
		{
			public int StartLine;
			public int EndLine;

			public LineSpan(int startLine, int endLine)
			{
				StartLine = startLine;
				EndLine = endLine;
			}
		}

		#endregion

		#region Fields
		public ArrayList Lines;
		public TypeOfDiff DiffType = TypeOfDiff.ExactCopy;
		#endregion

		/// <summary>
		/// Diffs two text files, by performing a line-by-line, case-sensitive comparison. Does not merge the output.
		/// </summary>
		/// <returns>Enum indicating Conflict, Warning or Exact Copy</returns>
		/// <param name="fileBodyLeft">Contents of LEFT file</param>
		/// <param name="fileBodyRight">Contents of RIGHT file</param>
		public static TypeOfDiff PerformTwoWayDiff(string fileBodyLeft, string fileBodyRight)
		{
			LineSpan[] leftOuput, rightOutput;
			string combined;
			return PerformTwoWayDiff(true, fileBodyLeft, fileBodyRight, out leftOuput, out rightOutput, out combined);
		}

		/// <summary>
		/// Merge two text files, by performing a line-by-line, case-sensitive comparison
		/// </summary>
		/// <returns>Enum indicating Conflict, Warning or Exact Copy</returns>
		/// <param name="identifyConflictsOnly">Flag - if true, don't merge</param>
		/// <param name="fileBodyLeft">Contents of LEFT file</param>
		/// <param name="fileBodyRight">Contents of RIGHT file</param>
		/// <param name="leftConflictLines">Array of lines with conflict from LEFT file</param>
		/// <param name="rightConflictLines">Array of lines with conflict from RIGHT file</param>
		/// <param name="combinedText">Result of merging files.</param>
		public static TypeOfDiff PerformTwoWayDiff(
									bool identifyConflictsOnly,
									string fileBodyLeft,
									string fileBodyRight,
									out LineSpan[] leftConflictLines,
									out LineSpan[] rightConflictLines,
									out string combinedText)
		{
			TypeOfDiff returnValue = TypeOfDiff.ExactCopy;
			combinedText = "";
			StringUtility.RemoveTrailingLineBreaks(ref fileBodyLeft);
			StringUtility.RemoveTrailingLineBreaks(ref fileBodyRight);

			// break files into arrays of lines
			string[] leftLines = Common.Utility.StandardizeLineBreaks(fileBodyLeft, Common.Utility.LineBreaks.Unix).Split('\n');
			string[] rightLines = Common.Utility.StandardizeLineBreaks(fileBodyRight, Common.Utility.LineBreaks.Unix).Split('\n');

			// handle case where at least one file is empty
			if (fileBodyLeft.Length == 0 || fileBodyRight.Length == 0)
			{
				// both files are empty (unlikely in practice)
				if (fileBodyLeft.Length == 0 && fileBodyRight.Length == 0)
				{
					leftConflictLines = new LineSpan[0];
					rightConflictLines = new LineSpan[0];
					return TypeOfDiff.ExactCopy;
				}

				// one file is empty
				if (fileBodyLeft.Length > 0)
				{
					combinedText = fileBodyLeft;
					leftConflictLines = new[] { new LineSpan(0, leftLines.Length - 1) };
					rightConflictLines = new LineSpan[0];
				}
				else
				{
					combinedText = fileBodyRight;
					leftConflictLines = new LineSpan[0];
					rightConflictLines = new[] { new LineSpan(0, rightLines.Length - 1) };
				}
				return TypeOfDiff.Warning;
			}

			// initialise variables for merging
			StringBuilder sbMerged = new StringBuilder(Math.Max(fileBodyLeft.Length, fileBodyRight.Length) + 1000);

			// DMW_Question Is a 'combinedLineCount' really a 'mergedLineCount'?
			int combinedLineCount = 0;
			ArrayList combinedLeftColouredLines = new ArrayList();
			ArrayList combinedRightColouredLines = new ArrayList();

			// perform the diff (case-sensitive, check white space)
			Diff diff = new Diff(leftLines, rightLines, true, true);

			// loop through parts of the diff
			foreach (Diff.Hunk hunk in diff)
			{
				if (hunk.Same)
				{
					for (int i = hunk.Left.Start; i <= hunk.Left.End; i++)
					{
						//DMW_Changed	sbMerged.Append(leftLines[i] + Environment.NewLine);
						sbMerged.AppendLine(leftLines[i]);
						combinedLineCount++;
					}
				}
				else    // hunks are different
				{
					if (hunk.Left.Count > 0 && hunk.Right.Count > 0)
					{
						returnValue = TypeOfDiff.Conflict;
					}
					else if (returnValue != TypeOfDiff.Conflict)
					{
						returnValue = TypeOfDiff.Warning;
					}

					// LEFT file
					for (int i = hunk.Left.Start; i <= hunk.Left.End; i++)
					{
						//DMW_Changed	sbMerged.Append(leftLines[i] + Environment.NewLine);
						sbMerged.AppendLine(leftLines[i]);

						if (!identifyConflictsOnly || (hunk.Left.Count > 0 && hunk.Right.Count > 0))
						{
							combinedLeftColouredLines.Add(new LineSpan(combinedLineCount, combinedLineCount));
						}
						combinedLineCount++;
					}

					// RIGHT file
					for (int i = hunk.Right.Start; i <= hunk.Right.End; i++)
					{
						//DMW_Changed	sbMerged.Append(leftLines[i] + Environment.NewLine);
						sbMerged.AppendLine(rightLines[i]);

						if (!identifyConflictsOnly || (hunk.Left.Count > 0 && hunk.Right.Count > 0))
						{
							combinedRightColouredLines.Add(new LineSpan(combinedLineCount, combinedLineCount));
						}
						combinedLineCount++;
					}
				}
			}
			leftConflictLines = (LineSpan[])combinedLeftColouredLines.ToArray(typeof(LineSpan));
			rightConflictLines = (LineSpan[])combinedRightColouredLines.ToArray(typeof(LineSpan));
			combinedText = sbMerged.ToString();
			return returnValue;
		}

		/// <summary>
		/// Merge three text files
		/// </summary>
		/// <param name="userText">File with user changes</param>
		/// <param name="prevGenText">Previously generated file</param>
		/// <param name="newGenText">Newly generated file</param>
		/// <param name="mergedText">Merged file</param>
		/// <returns></returns>
		public static SlyceMergeResult Perform3wayDiff(string userText, string prevGenText, string newGenText, out string mergedText)
		{
			return Perform3wayDiff(userText, prevGenText, newGenText, out mergedText, true);
		}

		/// <summary>
		/// Merge three text files
		/// </summary>
		/// <param name="userText">File with user changes</param>
		/// <param name="prevGenText">Previously generated file</param>
		/// <param name="newGenText">Newly generated file</param>
		/// <param name="mergedText">Merged file</param>
		/// <returns></returns>
		/// <param name="compareWhitespace">True if the whitespace should be counted as a change.</param>
		public static SlyceMergeResult Perform3wayDiff(string userText, string prevGenText, string newGenText, out string mergedText, bool compareWhitespace)
		{
			SlyceMergeResult slyceMerge = new SlyceMergeResult();
			mergedText = Merge3Inputs(slyceMerge, userText, prevGenText, newGenText);

			if (slyceMerge.DiffType == TypeOfDiff.ExactCopy)
			{
				// Successfully merged text (with no conflicts) always gets returned as 'EXACT_COPY',
				// so we need to perform a further test to see if it really is different.
				bool userFileIsIdentical = FilesAreTheSame(userText, mergedText, true, compareWhitespace);
				bool newGenFileIsIdentical = FilesAreTheSame(newGenText, mergedText, true, compareWhitespace);

				if (!userFileIsIdentical || !newGenFileIsIdentical)
				{
					if (!userFileIsIdentical && !newGenFileIsIdentical)
					{
						slyceMerge.DiffType = TypeOfDiff.UserAndTemplateChange;
					}
					else if (userFileIsIdentical)
					{
						slyceMerge.DiffType = TypeOfDiff.UserChangeOnly;
					}
					else // newGenFileIsIdentical
					{
						slyceMerge.DiffType = TypeOfDiff.TemplateChangeOnly;
					}
				}
			}
			return slyceMerge;
		}

		/// <summary>
		/// Gets whether two text files are the same or not. Linebreaks get standardized inside this function.
		/// </summary>
		/// <param name="fileBodyLeft">LEFT text.</param>
		/// <param name="fileBodyRight">RIGHT text.</param>
		/// <param name="caseSensitive">True to consider case, false to ignore differences due to case.</param>
		/// <param name="compareWhitespace">True to consider whitespace, false to ignore differences in whitespace.</param>
		public static bool FilesAreTheSame(string fileBodyLeft, string fileBodyRight, bool caseSensitive, bool compareWhitespace)
		{
			StringUtility.RemoveTrailingLineBreaks(ref fileBodyLeft);
			StringUtility.RemoveTrailingLineBreaks(ref fileBodyRight);
			// Performs a line-by-line, case-insensitive comparison.
			string[] arrFile1 = Common.Utility.StandardizeLineBreaks(fileBodyLeft, Common.Utility.LineBreaks.Unix).Split('\n');
			string[] arrFile2 = Common.Utility.StandardizeLineBreaks(fileBodyRight, Common.Utility.LineBreaks.Unix).Split('\n');
			Diff diff = new Diff(arrFile1, arrFile2, caseSensitive, compareWhitespace);

			foreach (Diff.Hunk hunk in diff)
			{
				if (!hunk.Same)
				{
					return false;
				}
			}
			return true;
		}

		internal static string Merge3Inputs(SlyceMergeResult merge, string userText, string prevGenText, string nextGenText)
		{
			//StringBuilder sb = new StringBuilder(1000);	// DMW_Changed: this is not used
			bool newlineAppended = false;

			newlineAppended = AppendNewLine(ref userText, newlineAppended);
			newlineAppended = AppendNewLine(ref prevGenText, newlineAppended);
			newlineAppended = AppendNewLine(ref nextGenText, newlineAppended);

			// DMW_Changed: remove all CrLfs
			const string pilcrowEth = "¶Ð";
			const char ethChar = 'Ð';

			// standardise linebreaks
			userText = Common.Utility.StandardizeLineBreaks(userText, Common.Utility.LineBreaks.Windows);
			nextGenText = Common.Utility.StandardizeLineBreaks(nextGenText, Common.Utility.LineBreaks.Windows);
			prevGenText = Common.Utility.StandardizeLineBreaks(prevGenText, Common.Utility.LineBreaks.Windows);

			string[] userLines = userText.Replace("\r\n", pilcrowEth).Split(ethChar);
			string[] nextGenLines = nextGenText.Replace("\r\n", pilcrowEth).Split(ethChar);
			string[] prevGenLines = prevGenText.Replace("\r\n", pilcrowEth).Split(ethChar);

			//string[] userLines = Slyce.Common.Utility.StandardizeLineBreaks(userText, Slyce.Common.Utility.LineBreaks.Windows).Split('\r\n');
			//string[] nextGenLines = Slyce.Common.Utility.StandardizeLineBreaks(nextGenText, Slyce.Common.Utility.LineBreaks.Windows).Split('\n');
			//string[] prevGenLines = Slyce.Common.Utility.StandardizeLineBreaks(prevGenText, Slyce.Common.Utility.LineBreaks.Windows).Split('\n');
			GetMergedOutput(merge, prevGenLines, userLines, nextGenLines);
			return WriteLineTextArrayToList(merge.Lines, newlineAppended);
		}

		/// <summary>
		/// Append a NewLine if there isn't one already
		/// </summary>
		/// <param name="text">Text to check</param>
		/// <param name="newlineAppended">Flag to indicate if any NewLines added</param>
		/// <returns>Updated flag to indicate if any NewLines added</returns>
		private static bool AppendNewLine(ref string text, bool newlineAppended)
		{
			if (text.Length > 0 &&
				text[text.Length - 1] != '\r' &&
				text[text.Length - 1] != '\n')
			{
				text += Environment.NewLine;
				newlineAppended = true;
			}
			return newlineAppended;
		}

		/// <summary>
		/// Gets the severest common type of diff from those supplied in the parameter list,
		/// returning a higher severity level than either value if required.
		/// </summary>
		/// <returns>Resulting DiffType.</returns>
		/// <param name="diffType1">First DiffType.</param>
		/// <param name="diffType2">Second DiffType.</param>
		public static TypeOfDiff CombineDiffTypes(TypeOfDiff diffType1, TypeOfDiff diffType2)
		{
			TypeOfDiff resultingDiff = diffType1;

			switch (diffType2)
			{
				case TypeOfDiff.Conflict:
					resultingDiff = TypeOfDiff.Conflict;
					break;
				case TypeOfDiff.ExactCopy:
					resultingDiff = diffType1;
					break;
				case TypeOfDiff.TemplateChangeOnly:
					switch (diffType1)
					{
						case TypeOfDiff.Conflict:
						case TypeOfDiff.TemplateChangeOnly:
						case TypeOfDiff.UserAndTemplateChange:
							resultingDiff = diffType1;
							break;
						case TypeOfDiff.ExactCopy:
							resultingDiff = TypeOfDiff.TemplateChangeOnly;
							break;
						case TypeOfDiff.UserChangeOnly:
							//DiffType = TypeOfDiff.UserAndTemplateChange;
							//resultingDiff = TypeOfDiff.Conflict;
							resultingDiff = TypeOfDiff.UserAndTemplateChange;
							break;
						case TypeOfDiff.Warning:
							resultingDiff = TypeOfDiff.Warning;
							break;
					}
					break;
				case TypeOfDiff.UserAndTemplateChange:
					switch (diffType1)
					{
						case TypeOfDiff.Conflict:
						case TypeOfDiff.UserAndTemplateChange:
							resultingDiff = diffType1;
							break;
						case TypeOfDiff.ExactCopy:
						case TypeOfDiff.UserChangeOnly:
						case TypeOfDiff.TemplateChangeOnly:
							resultingDiff = TypeOfDiff.UserAndTemplateChange;
							//resultingDiff = TypeOfDiff.Conflict;
							break;
						case TypeOfDiff.Warning:
							resultingDiff = TypeOfDiff.Warning;
							break;
					}
					break;
				case TypeOfDiff.UserChangeOnly:
					switch (diffType1)
					{
						case TypeOfDiff.Conflict:
						case TypeOfDiff.UserChangeOnly:
						case TypeOfDiff.UserAndTemplateChange:
							resultingDiff = diffType1;
							break;
						case TypeOfDiff.ExactCopy:
							resultingDiff = TypeOfDiff.UserChangeOnly;
							break;
						case TypeOfDiff.TemplateChangeOnly:
							//DiffType = TypeOfDiff.UserAndTemplateChange;
							resultingDiff = TypeOfDiff.Conflict;
							break;
						case TypeOfDiff.Warning:
							resultingDiff = TypeOfDiff.Warning;
							break;
					}
					break;
				case TypeOfDiff.Warning:
					resultingDiff = TypeOfDiff.Warning;
					break;
			}
			return resultingDiff;
		}

		/// <summary>
		/// Perform 3-way merge
		/// </summary>
		/// <returns>Merged text.</returns>
		/// <param name="merge"></param>
		/// <param name="prevGenLines">Previously generated version ie: 'base version'.</param>
		/// <param name="userLines">User-modified version of 'base'.</param>
		/// <param name="nextGenLines">Latest generated version of 'base'.</param>
		internal static string GetMergedOutput(SlyceMergeResult merge, string[] prevGenLines, string[] userLines, string[] nextGenLines)
		{
			// default values and initialisation
			merge.DiffType = TypeOfDiff.ExactCopy;
			merge.Lines = new List<LineText>(200);
			string output = "";
			int lineCounter = -1;
			const string pilcrowString = "¶";
			const char pilcrow = '¶';

			// diff the User version and LatestGenerated version against the PrevGenerated version
			IList res = Merge.MergeLists(prevGenLines, new[] { userLines, nextGenLines });

			// handle empty input
			if (res.Count == 1 && res[0].Equals(string.Empty))
			{
				return string.Empty;
			}
			string conflictTypeName = typeof(Merge.Conflict).FullName;

			// process each line from the diff
			foreach (object line in res)
			{
				lineCounter++;
				string lineTypeName = line.GetType().ToString();

				if (lineTypeName == "System.String")
				{
					string thisLine = (string)line;
					merge.Lines.Add(new LineText(thisLine.Replace(pilcrowString, ""), lineCounter, TypeOfLine.Normal));
				}
				else if (lineTypeName == conflictTypeName)
				{
					Merge.Conflict conf = (Merge.Conflict)line;
					Range[] ranges = conf.Ranges;
					Range rangeUser = ranges[0];
					Range rangeNewGen = ranges[1];

					string[] conflictUserLines = GetLinesFromRange(userLines, rangeUser);
					string[] conflictNewGenLines = GetLinesFromRange(nextGenLines, rangeNewGen);

					// Get diff of the conflicts
					Diff diff = new Diff(conflictUserLines, conflictNewGenLines, true, false);

					foreach (Diff.Hunk hunk in diff)
					{
						if (hunk.Same)
						{
							string same = GetPortionOfString(conflictUserLines, hunk.Left.Start, hunk.Left.End);
							same = RemoveTrailingCharacter(pilcrowString, same);
							same = same.Replace(pilcrowString, "\r\n");
							output += same;
							merge.Lines.Add(new LineText(same, lineCounter, TypeOfLine.Normal));
						}
						else
						{
							// Get the user modified lines
							string userPortion = GetPortionOfString(conflictUserLines, hunk.Left.Start, hunk.Left.End);
							userPortion = RemoveTrailingCharacter(pilcrowString, userPortion);

							// Get the newly generated lines
							string newGenPortion = GetPortionOfString(conflictNewGenLines, hunk.Right.Start, hunk.Right.End);
							newGenPortion = RemoveTrailingCharacter(pilcrowString, newGenPortion);

							merge.SetDiffType(TypeOfDiff.Conflict);
							TypeOfLine lineType = newGenPortion.Length > 0 ? TypeOfLine.User : TypeOfLine.Normal;

							string[] userSplitLines = userPortion.Split(pilcrow);
							if (userPortion.Length > 0)
							{
								merge.Lines.Add(new LineText(userSplitLines[0], lineCounter, lineType));
							}
							for (int myCount = 1; myCount < userSplitLines.Length; myCount++)
							{
								lineCounter++;
								merge.Lines.Add(new LineText(userSplitLines[myCount], lineCounter, lineType));
							}

							lineType = userPortion.Length > 0 ? TypeOfLine.NewGen : TypeOfLine.Normal;
							string[] newGenSplitLines = newGenPortion.Split(pilcrow);
							if (newGenPortion.Length > 0)
							{
								merge.Lines.Add(new LineText(newGenSplitLines[0], lineCounter, lineType));
							}
							for (int myCount = 1; myCount < newGenSplitLines.Length; myCount++)
							{
								lineCounter++;
								merge.Lines.Add(new LineText(newGenSplitLines[myCount], lineCounter, lineType));
							}
						}
					}
				}
				else
				{
					throw new Exception(string.Format("Unexpected line type: {0}\nText1:{1}\n\nText2:{2}\n\nText3:{3}", line.GetType(), prevGenLines, userLines, nextGenLines));
				}
			}
			return output;
		}

		public static string[] GetLinesFromRange(string[] lines, Range range)
		{
			string[] linesFromRange = new string[range.End - range.Start + 1];
			for (int i = range.Start; i <= range.End; i++)
			{
				linesFromRange[i - range.Start] = lines[i];
			}
			return linesFromRange;
		}

		public static string[] GetLinesFromRange(List<string> lines, Range range)
		{
			string[] linesFromRange = new string[range.End - range.Start + 1];
			for (int i = range.Start; i <= range.End; i++)
			{
				linesFromRange[i - range.Start] = lines[i];
			}
			return linesFromRange;
		}

		private static string RemoveTrailingCharacter(string characterToRemove, string stringToAdjust)
		{
			if (stringToAdjust.EndsWith(characterToRemove))
			{
				stringToAdjust = stringToAdjust.Substring(0, stringToAdjust.Length - 1);
			}
			return stringToAdjust;
		}

		/// <param name="lines"></param>
		/// <param name="removeFinalLineBreak">
		/// True to remove the trailing line-break. This is due to an extra linebreak being
		/// appended in other section of code TODO: identify where the the other code
		/// exists.
		/// </param>
		public static string WriteLineTextArrayToList(List<LineText> lines, bool removeFinalLineBreak)
		{
			StringBuilder sb = new StringBuilder(1000);
			const string pilcrow = "¶";

			//for (int i = 0; i < Lines.Count; i++)
			for (int i = 0; i < lines.Count - 1; i++)
			{
				LineText item = lines[i];
				sb.Append(item.Text.Replace(pilcrow, string.Empty));
				// DMW_Added
				//if(item.Text.Length > 0)
				//{
				sb.Append("\r\n");
				//}
			}
			// DMW_Changed
			//if (removeFinalLineBreak && sb.Length > 0 && (sb[sb.Length - 1] == '\n' || sb[sb.Length - 1] == '\r'))
			//{
			//    sb.Remove(sb.Length - 1, 1);
			//}
			if (removeFinalLineBreak && sb.Length > 0 && (sb.ToString().EndsWith("\r\n")))
			{
				sb.Remove(sb.Length - 2, 2);
			}
			return sb.ToString();
		}

		public static string GetPortionOfString(string[] textArray, int startPos, int EndPos)
		{
			StringBuilder sb = new StringBuilder(EndPos - startPos + 1);
			int lastPos = EndPos < textArray.Length ? EndPos : textArray.Length - 1;

			for (int i = startPos; i <= lastPos; i++)
			{
				// sb.Append(textArray[i] + "\n");
				// DMW_Changed: try not putting in \n at this point
				sb.Append(textArray[i]);
			}
			return sb.ToString();
		}
	}

	/// <summary>
	/// Enumeration that defines what kind of 3-way diff exists for the object/text in
	/// question. Usually pertains to diff between NewGen, User and PrevGen
	/// objects/text.
	/// </summary>
	public enum TypeOfDiff
	{
		/// <summary>No differences. All three versions are exactly the same. Also if there is only one file</summary>
		ExactCopy,
		/// <summary>User version is different from PrevGen. No changes in NewGen version.</summary>
		UserChangeOnly, // a warning
		/// <summary>NewGen version is different from PrevGen. No changes in User version.</summary>
		TemplateChangeOnly, // a warning		
		/// <summary>
		/// Changes in both User and NewGen compared to the PrevGen version, but no
		/// conflicting changes ie: User changes and NewGen changes are in different parts of the
		/// text and do not overlap.
		/// </summary>
		UserAndTemplateChange, // a warning		
		/// <summary>
		/// Conflicting changes in both User and NewGen compared to the PrevGen version. This
		/// means that the changes overlap and appear in the same part of the document. The correct
		/// course of action cannot be determined without human help.
		/// </summary>
		Conflict,
		/// <summary>
		/// The PrevGen file is missing. This means a 3-Way diff cannot be performed, so a 2-Way
		/// diff was performed instead. User intervention is required to merge the changes. The 
		/// changes look mergeable at this point - however the diff algorithm is not as
		/// accurate in this mode so the automatic merge may not go as intended and must be
		/// reviewed by the user.
		/// </summary>
		Warning,
		/// <summary>
		/// This is an invalid value and has only been included for completeness. If this
		/// value is ever used an exception should get thrown.
		/// </summary>
		Unknown,
		NewFile
	}
}
