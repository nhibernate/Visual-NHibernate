using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.IntelliMerge.UI.VisualDiff;

namespace Specs_For_Visual_Diff_Output
{
	[TestFixture]
	public class Conflict_Ranges
	{
		[Test]
		public void Line_Is_In_Conflict()
		{
			VisualDiffOutput vdo = new VisualDiffOutput();
			vdo.AddLine("line 1", Algorithm.Diff.ChangeType.User, true);
			vdo.AddLine("line 2", Algorithm.Diff.ChangeType.Template, true);
			vdo.AddLine("line 3", Algorithm.Diff.ChangeType.None);
			vdo.ConflictRanges.Add(new VisualDiffOutput.ConflictRange(0, 2));

			Assert.That(vdo.IsLineInConflict(0), "First line is in conflict.");
			Assert.That(vdo.IsLineInConflict(1), "Second line is in conflict.");
			Assert.That(vdo.IsLineInConflict(2), Is.False, "Third line is not a conflict.");
		}

		[Test]
		public void Conflict_Range_Moves_Correctly()
		{
			VisualDiffOutput vdo = new VisualDiffOutput();
			vdo.AddLine("line 0", Algorithm.Diff.ChangeType.None);
			vdo.AddLine("line 1", Algorithm.Diff.ChangeType.User, true);
			vdo.AddLine("line 2", Algorithm.Diff.ChangeType.Template, true);
			vdo.ConflictRanges.Add(new VisualDiffOutput.ConflictRange(1, 3));

			Assert.That(vdo.IsLineInConflict(0), Is.False, "First line is not in conflict.");
			Assert.That(vdo.IsLineInConflict(1), "Second line is in conflict.");
			Assert.That(vdo.IsLineInConflict(2), "Third line is in conflict.");

			vdo.RemoveLine(0);

			Assert.That(vdo.LineCount, Is.EqualTo(2));
			Assert.That(vdo.IsLineInConflict(0), "First line is in conflict.");
			Assert.That(vdo.IsLineInConflict(1), "Second line is in conflict.");
		}


		[Test]
		public void Conflict_Range_Doesnt_Move()
		{
			VisualDiffOutput vdo = CreateVisualDiffOutput();

			Assert.That(vdo.IsLineInConflict(0), Is.False, "First line is not in conflict.");
			Assert.That(vdo.IsLineInConflict(1), "Second line is in conflict.");
			Assert.That(vdo.IsLineInConflict(2), "Third line is in conflict.");
			Assert.That(vdo.IsLineInConflict(3), Is.False, "Fourth line is not in conflict.");

			vdo.RemoveLine(3);

			Assert.That(vdo.LineCount, Is.EqualTo(3));
			Assert.That(vdo.IsLineInConflict(0), Is.False, "First line is not in conflict.");
			Assert.That(vdo.IsLineInConflict(1), "Second line is in conflict.");
			Assert.That(vdo.IsLineInConflict(2), "Third line is in conflict.");
		}

		[Test]
		public void Conflict_Range_Shrinks_Correctly()
		{
			VisualDiffOutput vdo = CreateVisualDiffOutput();

			Assert.That(vdo.IsLineInConflict(0), Is.False, "First line is not in conflict.");
			Assert.That(vdo.IsLineInConflict(1), "Second line is in conflict.");
			Assert.That(vdo.IsLineInConflict(2), "Third line is in conflict.");
			Assert.That(vdo.IsLineInConflict(3), Is.False, "Fourth line is not in conflict.");

			vdo.RemoveLine(2);

			Assert.That(vdo.LineCount, Is.EqualTo(3));
			Assert.That(vdo.IsLineInConflict(0), Is.False, "First line is not in conflict.");
			Assert.That(vdo.IsLineInConflict(1), "Second line is in conflict.");
			Assert.That(vdo.IsLineInConflict(2), Is.False, "Third line is not in conflict.");
			Assert.That(vdo.ConflictRanges[0].StartLineIndex, Is.EqualTo(1));
			Assert.That(vdo.ConflictRanges[0].EndLineIndex, Is.EqualTo(2));
		}

		private VisualDiffOutput CreateVisualDiffOutput()
		{
			VisualDiffOutput vdo = new VisualDiffOutput();
			vdo.AddLine("line 0", Algorithm.Diff.ChangeType.None);
			vdo.AddLine("line 1", Algorithm.Diff.ChangeType.User, true);
			vdo.AddLine("line 2", Algorithm.Diff.ChangeType.Template, true);
			vdo.AddLine("line 3", Algorithm.Diff.ChangeType.None);
			vdo.ConflictRanges.Add(new VisualDiffOutput.ConflictRange(1, 3));
			return vdo;
		}
	}
}