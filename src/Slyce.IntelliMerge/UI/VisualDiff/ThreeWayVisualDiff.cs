using System;
using System.Collections.Generic;
using Algorithm.Diff;
using Slyce.IntelliMerge.Controller;

namespace Slyce.IntelliMerge.UI.VisualDiff
{
	/// <summary>
	/// Contains methods to take a FileInformation object and create two representations of the text within those files,
	/// to be used when presenting the diff to a user through the UI.
	/// </summary>
	public class ThreeWayVisualDiff : IVisualDiff
	{
		private List<string> prevgen;
		private List<string> user;
		private List<string> newgen;

		private readonly FileInformation<string> fileInfo;

		/// <summary>
		/// Take a TextFileInformation object, diff it if it hasn't been diffed, and create a VisualDiffOutput object that
		/// represents the output of the diff in a form that can be displayed to the user.
		/// </summary>
		/// <remarks>
		/// If the Prevgen file does not exist, it will perform a diff, then copy the NewGen file into the PrevGen temporarily,
		/// so that all changes will show as User changes.
		/// </remarks>
		/// <param name="fileInfo">The TextFileInformation object that has the information on the diffed files.</param>
		/// <returns>A VisualDiffOutput object containing all of the information needed to show the output of the diff to the user.</returns>
		private VisualDiffOutput ProcessDiff(FileInformation<string> fileInfo)
		{
			if (fileInfo == null) throw new ArgumentNullException("fileInfo");

			VisualDiffOutput output = new VisualDiffOutput();

			bool virtualPrevGen = false;
			string oldPrevGenFilePath = null;

			if (fileInfo.CurrentDiffResult.DiffPerformedSuccessfully == false)
				fileInfo.PerformDiff();

			if (fileInfo.PrevGenFile.HasContents == false)
			{
				virtualPrevGen = true;
				oldPrevGenFilePath = fileInfo.PrevGenFile.FilePath;
				fileInfo.PrevGenFile.ReplaceContents(fileInfo.NewGenFile.GetContents(), true);
			}

			output.DiffType = fileInfo.CurrentDiffResult.DiffType;

			switch (fileInfo.CurrentDiffResult.DiffType)
			{
				case TypeOfDiff.ExactCopy:
					ProcessExactCopy(fileInfo, output);
					break;
				case TypeOfDiff.TemplateChangeOnly:
					ProcessTemplateChange(fileInfo, output);
					break;
				case TypeOfDiff.UserChangeOnly:
					ProcessUserChange(fileInfo, output);
					break;
				case TypeOfDiff.UserAndTemplateChange:
					ProcessUserAndTemplateChange(fileInfo, output);
					break;
				case TypeOfDiff.Conflict:
				case TypeOfDiff.Warning:
					ProcessUserAndTemplateChange(fileInfo, output);
					break;
			}

			if (virtualPrevGen)
			{
				fileInfo.PrevGenFile.FilePath = oldPrevGenFilePath;
				if (oldPrevGenFilePath != null)
				{
					fileInfo.PrevGenFile.ReplaceContents("", false);
				}
			}

			return output;
		}

		private VisualDiffOutput vdo = new VisualDiffOutput();

		public ThreeWayVisualDiff(FileInformation<string> tfi)
		{
			if (tfi == null) throw new ArgumentNullException("tfi");
			fileInfo = tfi;
			prevgen = ExtractFileToStringList(fileInfo.PrevGenFile);
			user = ExtractFileToStringList(fileInfo.UserFile);
			newgen = ExtractFileToStringList(fileInfo.NewGenFile);
		}

		public VisualDiffOutput ProcessMergeOutput()
		{
			vdo = new VisualDiffOutput();

			if (!fileInfo.NewGenFile.HasContents &&
			   !fileInfo.UserFile.HasContents &&
			   !fileInfo.PrevGenFile.HasContents)
			{
				vdo.DiffType = TypeOfDiff.ExactCopy;
				return vdo;
			}

			if (fileInfo.PrevGenFile.HasContents == false)
			{
				if (fileInfo.NewGenFile.HasContents == false)
				{
					// prevGen and NewGen are missing. Copy everything from user and mark exact copy.
					AddAllLinesFromList(user, ChangeType.None);
					return vdo;
				}
				if (fileInfo.UserFile.HasContents == false)
				{
					// prevGen and user are missing. Copy everything from NewGen and mark exact copy.
					AddAllLinesFromList(newgen, ChangeType.None);
					return vdo;
				}
				prevgen = ExtractFileToStringList(fileInfo.NewGenFile);
			}
			else
			{
				if (fileInfo.UserFile.HasContents == false && fileInfo.NewGenFile.HasContents)
					user = ExtractFileToStringList(fileInfo.PrevGenFile);
				else if (fileInfo.NewGenFile.HasContents == false && fileInfo.UserFile.HasContents)
					newgen = ExtractFileToStringList(fileInfo.PrevGenFile);
				else if (fileInfo.NewGenFile.HasContents == false && fileInfo.UserFile.HasContents == false)
				{
					// User and NewGen are missing. Copy everything from prevgen and mark everything missing.
					AddAllLinesFromList(prevgen, ChangeType.UserAndTemplate);
					return vdo;
				}
			}

			Queue<Merge.Conflict> conflicts;
			Queue<ExtendedRange> ranges;

			// We need to add a blank line to the bottom of each file, or any lines that are added
			// at the bottom of the new files will be discarded by the diff/merge. I have spent far
			// too long trying to work out why, this work around works so I'm going with it. If someone
			// fixes the underlying problem, this should still work fine. We remove the last line of the
			// merged lines to compensate for this.
			/* Begin Hack */
			prevgen.Add("");
			user.Add("");
			newgen.Add("");
			/* End Hack */

			// Do the initial 3 way diff
			Merge.MergeLists(prevgen, user, newgen, out conflicts, out ranges);

			while (conflicts.Count + ranges.Count > 0)
			{
				// Get the next range or conflict
				int conflictStart = int.MaxValue, rangeStart = int.MaxValue;
				if (conflicts.Count > 0)
				{
					conflictStart = conflicts.Peek().Ranges[0].Start;
				}
				if (ranges.Count > 0)
				{
					rangeStart = ranges.Peek().ChangedRange.Start;
				}
				// If the next range came first, process it next. Otherwise process the next conflict.
				if (rangeStart < conflictStart)
					ProcessRange(ranges.Dequeue());
				else
					ProcessConflict(conflicts.Dequeue());
			}
			// Other half of the above hack.
			if (vdo.LeftLines[vdo.LeftLines.Count - 1].Text == "" && vdo.RightLines[vdo.RightLines.Count - 1].Text == "")
				vdo.RemoveLine(vdo.LeftLines.Count - 1);
			return vdo;
		}

		private void AddAllLinesFromList(IEnumerable<string> list, ChangeType changeType)
		{
			foreach (string line in list)
			{
				vdo.AddLine(line, changeType);
				vdo.DiffType = TypeOfDiff.ExactCopy;
			}
		}

		private static List<string> ExtractFileToStringList(IProjectFile<string> file)
		{
			return new List<string>(Common.Utility.StandardizeLineBreaks(file.GetContents(), Common.Utility.LineBreaks.Unix).Split('\n'));
		}

		private void ProcessConflict(Merge.Conflict conflict)
		{
			string[] userLines = SlyceMerge.GetLinesFromRange(user, conflict.Ranges[Merge.UserIndex]);
			string[] newgenLines = SlyceMerge.GetLinesFromRange(newgen, conflict.Ranges[Merge.NewGenIndex]);

			if (userLines.Length == 0 && newgenLines.Length == 0)
			{
				// user and template have both deleted some lines. Add them in as virtual lines.
				foreach (string line in SlyceMerge.GetLinesFromRange(prevgen, conflict.Ranges[Merge.PrevGenIndex]))
				{
					vdo.AddLineToLeft(line, ChangeType.UserAndTemplate);
				}

				return;
			}

			Diff diff = new Diff(userLines, newgenLines, true, true);

			foreach (Diff.Hunk hunk in diff)
			{
				if (hunk.Same)
				{
					foreach (string line in hunk.Original())
					{
						vdo.AddLine(line, ChangeType.None);
					}
					continue;
				}

				// Conflict
				VisualDiffOutput.ConflictRange crange = new VisualDiffOutput.ConflictRange();
				crange.StartLineIndex = vdo.LeftLines.Count;
				for (int i = hunk.Left.Start; i <= hunk.Left.End; i++)
				{
					vdo.LeftLines.Add(new DiffLine(userLines[i], ChangeType.User));
					vdo.RightLines.Add(new DiffLine("", ChangeType.User, true));
				}
				for (int i = hunk.Right.Start; i <= hunk.Right.End; i++)
				{
					vdo.LeftLines.Add(new DiffLine(newgenLines[i], ChangeType.Template));
					vdo.RightLines.Add(new DiffLine("", ChangeType.Template, true));
				}
				crange.EndLineIndex = vdo.LeftLines.Count;
				vdo.ConflictRanges.Add(crange);
				vdo.DiffType = TypeOfDiff.Conflict;
			}
		}

		private void ProcessRange(ExtendedRange range)
		{
			for (int i = 0; i < range.ChangedRange.Count; i++)
			{
				string line = (string)range.ChangedRange[i];
				vdo.RightLines.Add(new DiffLine(line, range.ChangeType));
			}
			for (int i = 0; i < range.OriginalRange.Count; i++)
			{
				string line = (string)range.OriginalRange[i];
				vdo.LeftLines.Add(new DiffLine(line, range.ChangeType));
			}

			for (int i = 0; i < range.OriginalRange.Count - range.ChangedRange.Count; i++)
			{
				vdo.RightLines.Add(new DiffLine("", range.ChangeType, true));
			}

			for (int i = 0; i < range.ChangedRange.Count - range.OriginalRange.Count; i++)
			{
				vdo.LeftLines.Add(new DiffLine("", range.ChangeType, true));
			}

			switch (range.ChangeType)
			{
				case ChangeType.User:
					vdo.DiffType = SlyceMerge.CombineDiffTypes(vdo.DiffType, TypeOfDiff.UserChangeOnly);
					break;
				case ChangeType.Template:
					vdo.DiffType = SlyceMerge.CombineDiffTypes(vdo.DiffType, TypeOfDiff.TemplateChangeOnly);
					break;
				case ChangeType.UserAndTemplate:
					vdo.DiffType = SlyceMerge.CombineDiffTypes(vdo.DiffType, TypeOfDiff.UserAndTemplateChange);
					break;
				default:
					break;
			}
		}

		private static void ProcessUserAndTemplateChange(FileInformation<string> fileInfo, VisualDiffOutput output)
		{
			string[] newlines = Common.Utility.StandardizeLineBreaks(
				fileInfo.NewGenFile.GetContents(),
				Common.Utility.LineBreaks.Unix)
				.Split('\n');

			ProcessSingleChange(fileInfo, output, newlines, ChangeType.Template);

			newlines = Common.Utility.StandardizeLineBreaks(
				fileInfo.UserFile.GetContents(),
				Common.Utility.LineBreaks.Unix)
				.Split('\n');

			VisualDiffOutput secondOutput = new VisualDiffOutput();
			ProcessSingleChange(fileInfo, secondOutput, newlines, ChangeType.User);

			output.MergeOutput(secondOutput, ChangeType.Template, ChangeType.User);
		}

		private static void ProcessTemplateChange(FileInformation<string> fileInfo, VisualDiffOutput output)
		{
			string[] newlines =
				Common.Utility.StandardizeLineBreaks(fileInfo.NewGenFile.GetContents(),
													 Common.Utility.LineBreaks.Unix).Split('\n');
			ProcessSingleChange(fileInfo, output, newlines, ChangeType.Template);
		}

		private static void ProcessUserChange(FileInformation<string> fileInfo, VisualDiffOutput output)
		{
			string[] newlines =
				Common.Utility.StandardizeLineBreaks(fileInfo.UserFile.GetContents(),
													 Common.Utility.LineBreaks.Unix).Split('\n');
			ProcessSingleChange(fileInfo, output, newlines, ChangeType.User);
		}

		private static void ProcessSingleChange(FileInformation<string> fileInfo, VisualDiffOutput output, string[] newlines, ChangeType changeType)
		{
			string[] prevGenLines =
				Common.Utility.StandardizeLineBreaks(fileInfo.PrevGenFile.GetContents(), Common.Utility.LineBreaks.Unix).Split('\n');

			Diff diff = new Diff(prevGenLines, newlines, true, true);


			// loop through parts of the diff
			foreach (Diff.Hunk hunk in diff)
			{
				if (hunk.Same)
				{
					for (int i = hunk.Right.Start; i <= hunk.Right.End; i++)
					{
						output.LeftLines.Add(new DiffLine(newlines[i]));
						output.RightLines.Add(new DiffLine(newlines[i]));
					}
				}
				else
				{
					// If the left and right of the hunk both have lines in the hunk, this is a change.
					if (hunk.Right.Count != 0 && hunk.Left.Count != 0)
					{
						for (int i = hunk.Right.Start; i <= hunk.Right.End; i++)
						{
							output.RightLines.Add(new DiffLine(newlines[i], changeType));
						}
						for (int i = hunk.Left.Start; i <= hunk.Left.End; i++)
						{
							output.LeftLines.Add(new DiffLine(prevGenLines[i], ChangeType.None));
						}

						while (output.LeftLines.Count < output.RightLines.Count)
						{
							output.LeftLines.Add(new DiffLine("", changeType, true));
						}
						while (output.RightLines.Count < output.LeftLines.Count)
						{
							output.RightLines.Add(new DiffLine("", changeType, true));
						}
					}
					else if (hunk.Right.Count != 0) // The right hand side has added lines.
					{
						for (int i = hunk.Right.Start; i <= hunk.Right.End; i++)
						{
							output.LeftLines.Add(new DiffLine("", changeType, true));
							output.RightLines.Add(new DiffLine(newlines[i], changeType));
						}
					}
					else // The left hand side has extra lines.
					{
						for (int i = hunk.Left.Start; i <= hunk.Left.End; i++)
						{
							output.LeftLines.Add(new DiffLine(prevGenLines[i], ChangeType.None));
							output.RightLines.Add(new DiffLine("", changeType, true));
						}
					}

				}
			}
		}

		private static void ProcessExactCopy(FileInformation<string> fileInfo, VisualDiffOutput output)
		{
			{
				string fileText = fileInfo.NewGenFile.GetContents();
				string[] lines =
					Common.Utility.StandardizeLineBreaks(fileText, Common.Utility.LineBreaks.Unix).Split('\n');

				foreach (string line in lines)
				{
					output.LeftLines.Add(new DiffLine(line, ChangeType.None));
					output.RightLines.Add(new DiffLine(line, ChangeType.None));
				}
			}
		}
	}
}