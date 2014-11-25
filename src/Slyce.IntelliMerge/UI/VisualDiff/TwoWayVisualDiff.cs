using System;
using Algorithm.Diff;
using Slyce.IntelliMerge.Controller;

namespace Slyce.IntelliMerge.UI.VisualDiff
{
	public class TwoWayVisualDiff :IVisualDiff
	{
		private readonly TextFileInformation fileInformation;

		public TwoWayVisualDiff(TextFileInformation fileInformation)
		{
			if (fileInformation == null) throw new ArgumentNullException("fileInformation");

			if (fileInformation.UserFile.HasContents == false)
				throw new ArgumentException("User file has no contents");
			if (fileInformation.NewGenFile.HasContents == false)
				throw new ArgumentException("Template file has no contents");

			this.fileInformation = fileInformation;
		}

		public VisualDiffOutput ProcessMergeOutput()
		{
			if (fileInformation.UserFile.HasContents == false)
				throw new InvalidOperationException("Cannot merge: User file has no contents");
			if (fileInformation.NewGenFile.HasContents == false)
				throw new InvalidOperationException("Cannot merge: Template file has no contents");

			VisualDiffOutput vdo = new VisualDiffOutput();

			string leftText = Common.Utility.StandardizeLineBreaks(fileInformation.NewGenFile.GetContents(), Common.Utility.LineBreaks.Unix);
			string rightText = Common.Utility.StandardizeLineBreaks(fileInformation.UserFile.GetContents(), Common.Utility.LineBreaks.Unix);

			StringUtility.RemoveTrailingLineBreaks(ref leftText);
			StringUtility.RemoveTrailingLineBreaks(ref rightText);

			leftText = Common.Utility.StandardizeLineBreaks(leftText, Common.Utility.LineBreaks.Unix);
			rightText = Common.Utility.StandardizeLineBreaks(rightText, Common.Utility.LineBreaks.Unix);

			string[] leftLines = leftText.Split('\n');
			string[] rightLines = rightText.Split('\n');

			Diff diff = new Diff(leftLines, rightLines, true, true);

			foreach(Diff.Hunk h in diff)
			{
				if(h.Same)
				{
					foreach(string line in h.Original())
					{
						vdo.AddLine(line, ChangeType.None);
					}
					continue;
				}
				
				// Is this a conflict?
				if(h.Left.Count > 0 && h.Right.Count > 0)
				{
					int startIndex = vdo.LineCount;

					foreach (string line in h.Right)
					{
						vdo.LeftLines.Add(new DiffLine(line, ChangeType.User));
					}
					foreach (string line in h.Left)
					{
						vdo.LeftLines.Add(new DiffLine(line, ChangeType.Template));
					}

					for (int i = 0; i < h.Right.Count; i++)
					{
						vdo.RightLines.Add(new DiffLine("", ChangeType.None, true));
					}
					for (int i = 0; i < h.Left.Count; i++)
					{
						vdo.RightLines.Add(new DiffLine("", ChangeType.None, true));
					}

					vdo.ConflictRanges.Add(new VisualDiffOutput.ConflictRange(startIndex, vdo.LineCount));

					continue;
				}

				// Not a conflict. Just add the new lines and put virtual lines on the left.

				// Only one of these will actually run - we've already handled the case where
				// both sides of the hunk have lines.
				foreach(string line in h.Left)
				{
					vdo.RightLines.Add(new DiffLine(line, ChangeType.Template));
					vdo.LeftLines.Add(new DiffLine("", ChangeType.None, true));
				}
				foreach (string line in h.Right)
				{
					vdo.RightLines.Add(new DiffLine(line, ChangeType.User));
					vdo.LeftLines.Add(new DiffLine("", ChangeType.None, true));
				}
			}
			if(vdo.LineCount > 0)
				vdo.RemoveLine(vdo.LineCount - 1);

			return vdo;
		}
	}
}
