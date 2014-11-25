using System;
using System.Collections.Generic;
using System.Text;
using Algorithm.Diff;

namespace Slyce.IntelliMerge.UI.VisualDiff
{
	/// <summary>
	/// Contains the lines of text output of a diff, with metadata associated so a UI component can display the diff
	/// to a user in a useful way.
	/// </summary>
	public class VisualDiffOutput
	{
		/// <summary>
		/// Represents a range of lines in a single conflict. Encompases the user and template lines in the conflict.
		/// </summary>
		public class ConflictRange
		{
			/// <summary>
			/// The index of the first line in the conflict range.
			/// </summary>
			public int StartLineIndex;
			/// <summary>
			/// The index of the first line after the conflict range.
			/// </summary>
			public int EndLineIndex;

			public ConflictRange()
			{
			}

			public ConflictRange(int startLineIndex, int endLineIndex)
			{
				StartLineIndex = startLineIndex;
				EndLineIndex = endLineIndex;
			}
		}

		private readonly List<DiffLine> leftLines = new List<DiffLine>();
		private readonly List<DiffLine> rightLines = new List<DiffLine>();
		private readonly List<ConflictRange> conflictRanges = new List<ConflictRange>();

		private TypeOfDiff diffType;

		public TypeOfDiff DiffType
		{
			get { return diffType; }
			set { diffType = value; }
		}

		public IList<DiffLine> LeftLines
		{
			get
			{
				return leftLines;
			}
		}
		public IList<DiffLine> RightLines
		{
			get
			{
				return rightLines;
			}
		}

		public int LineCount
		{
			get
			{
				if (LeftLines.Count == RightLines.Count)
					return LeftLines.Count;

				throw new InvalidOperationException(
					"The VisualDiffOutput object is in an invalid state. There are extra lines on the " +
					(LeftLines.Count > RightLines.Count
						? "left side."
						: "right side."));
			}
		}

		public IList<ConflictRange> ConflictRanges
		{
			get { return conflictRanges; }
		}

		public bool IsLineInConflict(int lineIndex)
		{
			ConflictRange range;
			return IsLineInConflict(lineIndex, out range);
		}
		public bool IsLineInConflict(int lineIndex, out ConflictRange conflictRange)
		{
			foreach (ConflictRange range in conflictRanges)
			{
				if (range.StartLineIndex > lineIndex || range.EndLineIndex <= lineIndex)
					continue;
				if (range.StartLineIndex <= lineIndex && range.EndLineIndex > lineIndex)
				{
					conflictRange = range;
					return true;
				}
			}
			conflictRange = new ConflictRange();
			return false;
		}

		/// <summary>
		/// Adds a new line to the left hand side with the given text and change type.
		/// Adds a virtual line to the right hand side.
		/// </summary>
		/// <param name="lineText">The text of the new line.</param>
		/// <param name="changeType">The type of change to set the new line to.</param>
		public void AddLineToLeft(string lineText, ChangeType changeType)
		{
			AddLine(lineText, changeType, true);
		}

		/// <summary>
		/// Adds a new line to the right hand side with the given text and change type.
		/// Adds a virtual line to the left hand side.
		/// </summary>
		/// <param name="lineText">The text of the new line.</param>
		/// <param name="changeType">The type of change to set the new line to.</param>
		public void AddLineToRight(string lineText, ChangeType changeType)
		{
			AddLine(lineText, changeType, false);
		}

		/// <summary>
		/// Adds a new line to the both sides with the given text and change type.
		/// </summary>
		/// <param name="lineText">The text of the new line.</param>
		/// <param name="changeType">The type of change to set the new line to.</param>
		public void AddLine(string lineText, ChangeType changeType)
		{
			LeftLines.Add(new DiffLine(lineText, changeType));
			RightLines.Add(new DiffLine(lineText, changeType));
		}

		/// <summary>
		/// Adds a new line to the specified side with the given text and change type.
		/// Adds a virtual line to the other side.
		/// </summary>
		/// <param name="lineText">The text of the new line.</param>
		/// <param name="changeType">The type of change to set the new line to.</param>
		/// <param name="leftSide">True if the new line should be added to the left side.</param>
		public void AddLine(string lineText, ChangeType changeType, bool leftSide)
		{
			if (leftSide)
			{
				LeftLines.Add(new DiffLine(lineText, changeType));
				RightLines.Add(new DiffLine("", changeType, true));
			}
			else
			{
				LeftLines.Add(new DiffLine("", changeType, true));
				RightLines.Add(new DiffLine(lineText, changeType));
			}
		}

		public static string[] LinesToStringArray(IList<DiffLine> lines)
		{
			string[] output = new string[lines.Count];

			for (int i = 0; i < output.Length; i++)
			{
				output[i] = lines[i].Text;
			}

			return output;
		}

		internal void MergeOutput(VisualDiffOutput otherOutput, ChangeType thisChangeType, ChangeType otherChangeType)
		{
			string[] thisTempLines = LinesToStringArray(this.RightLines);
			string[] otherTempLines = LinesToStringArray(otherOutput.RightLines);

			this.LeftLines.Clear();
			this.RightLines.Clear();

			Diff diff = new Diff(thisTempLines, otherTempLines, true, true);

			// loop through parts of the diff
			foreach (Diff.Hunk hunk in diff)
			{
				int lineMismatch = hunk.Right.Start - hunk.Left.Start;
				if (hunk.Same)
				{
					for (int i = hunk.Right.Start; i <= hunk.Right.End; i++)
					{
						if (otherOutput.RightLines[i].Change != ChangeType.None)
						{
							//Both the user and template have changed to the same thing. Need to display this.
							leftLines.Add(new DiffLine(otherOutput.LeftLines[i].Text, thisChangeType | otherChangeType, otherOutput.LeftLines[i].IsVirtual));
							rightLines.Add(new DiffLine(otherTempLines[i], thisChangeType | otherChangeType));
						}
						else
						{
							leftLines.Add(new DiffLine(otherTempLines[i]));
							rightLines.Add(new DiffLine(otherTempLines[i]));
						}
					}
				}
				else
				{
					// If the left and right of the hunk both have lines, this could be a conflict.
					if (hunk.Right.Count != 0 && hunk.Left.Count != 0)
					{
						ConflictRange conflictRange = new ConflictRange();
						conflictRange.StartLineIndex = leftLines.Count;

						for (int i = hunk.Right.Start; i <= hunk.Right.End; i++)
						{
							leftLines.Add(new DiffLine(otherTempLines[i], otherChangeType));
							rightLines.Add(new DiffLine("", otherChangeType, true));
						}
						for (int i = hunk.Left.Start; i <= hunk.Left.End; i++)
						{
							leftLines.Add(new DiffLine(thisTempLines[i], thisChangeType));
							rightLines.Add(new DiffLine("", thisChangeType, true));
						}

						conflictRange.EndLineIndex = leftLines.Count;
						conflictRanges.Add(conflictRange);
					}
					else if (hunk.Right.Count != 0)
					{
						for (int i = hunk.Right.Start; i <= hunk.Right.End; i++)
						{
							leftLines.Add(new DiffLine("", otherChangeType, true));
							rightLines.Add(new DiffLine(otherTempLines[i], otherChangeType));
						}
					}
					else
					{
						for (int i = hunk.Left.Start; i <= hunk.Left.End; i++)
						{
							leftLines.Add(new DiffLine("", thisChangeType, true));
							rightLines.Add(new DiffLine(thisTempLines[i], thisChangeType));
						}
					}

				}
			}
		}


		///<summary>
		///Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		///</summary>
		///
		///<returns>
		///A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		///</returns>
		///<filterpriority>2</filterpriority>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Left Lines");
			foreach (DiffLine line in LeftLines)
			{
				sb.AppendLine(string.Format("\"{0}\" \t\t- {1} - {2}", line.Text, line.Change, line.IsVirtual));
			}

			sb.AppendLine("Right Lines");
			foreach (DiffLine line in RightLines)
			{
				sb.AppendLine(string.Format("\"{0}\" \t\t- {1} - {2}", line.Text, line.Change, line.IsVirtual));
			}

			return sb.ToString();
		}

		public void RemoveLine(int i)
		{
			leftLines.RemoveAt(i);
			rightLines.RemoveAt(i);

			for (int j = 0; j < conflictRanges.Count; j++)
			{
				if (conflictRanges[j].StartLineIndex > i)
					conflictRanges[j].StartLineIndex--;
				if (conflictRanges[j].EndLineIndex > i)
					conflictRanges[j].EndLineIndex--;
			}
		}

		public ConflictRange GetConflictBefore(int lineNum)
		{
			for (int i = 0; i < conflictRanges.Count; i++)
			{
				ConflictRange range = conflictRanges[i];

				if (range.StartLineIndex < lineNum)
					continue;

				return i == 0 ? conflictRanges[conflictRanges.Count - 1] : conflictRanges[i - 1];
			}

			return conflictRanges.Count == 0 ? null : conflictRanges[0];
		}

		public ConflictRange GetConflictAfter(int offset)
		{
			for (int i = 0; i < conflictRanges.Count; i++)
			{
				ConflictRange range = conflictRanges[i];

				if (range.StartLineIndex < offset)
					continue;

				return i == conflictRanges.Count - 1 ? conflictRanges[0] : conflictRanges[i + 1];
			}

			return conflictRanges.Count == 0 ? null : conflictRanges[conflictRanges.Count - 1];
		}

		public void CleanUp()
		{
			for (int i = 0; i < leftLines.Count; i++)
			{
				if (LeftLines[i].IsVirtual && RightLines[i].IsVirtual)
				{
					RemoveLine(i);
					i--;
				}
			}
		}
	}

	public class DiffLine
	{
		private string text;
		private ChangeType change = ChangeType.None;
		private bool isVirtual = false;

		public DiffLine()
		{
		}

		/// <summary>
		/// Initialises a DiffLine with the given text, and the LineChange set to None.
		/// </summary>
		/// <param name="lineText"></param>
		public DiffLine(string lineText)
			: this(lineText, ChangeType.None)
		{
		}

		public DiffLine(string lineText, ChangeType lineChange)
		{
			this.text = lineText;
			this.change = lineChange;
		}

		public DiffLine(string lineText, ChangeType lineChange, bool isVirtual)
		{
			text = lineText;
			change = lineChange;
			this.isVirtual = isVirtual;
		}

		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		public ChangeType Change
		{
			get { return change; }
			set { change = value; }
		}

		public bool IsVirtual
		{
			get { return isVirtual; }
			set { isVirtual = value; }
		}

		///<summary>
		///Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		///</summary>
		///
		///<returns>
		///A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		///</returns>
		///<filterpriority>2</filterpriority>
		public override string ToString()
		{
			return Text;
		}
	}
}