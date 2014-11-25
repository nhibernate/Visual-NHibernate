using System.IO;
using NUnit.Framework;
using Slyce.IntelliMerge;
using Slyce.IntelliMerge.UnitTesting;

namespace Specs_for_IntelliMerge_2way
{
	[TestFixture]
	public class When_both_files_are_empty : Base2WaySpec
	{

		[Test]
		public void The_merged_file_is_empty()
		{
			// inputs
			string fileBodyLeft = string.Empty;
			string fileBodyRight = string.Empty;

			TypeOfDiff typeOfDiff =
				SlyceMerge.PerformTwoWayDiff(identifyConflictsOnly, 
					fileBodyLeft, 
					fileBodyRight,
					out leftConflictLines,
					out rightConflictLines,
					out mergedText);
			Assert.AreEqual(TypeOfDiff.ExactCopy, typeOfDiff, "TypeOfDiffs are different.");
			Assert.AreEqual(-1, leftConflictLines.GetUpperBound(0), "Left conflict lines should be zero.");
			Assert.AreEqual(-1, rightConflictLines.GetUpperBound(0), "Right conflict lines should be zero.");
			Assert.AreEqual(0, mergedText.Length, "Combined length should equal start length.");
		}
	}

	[TestFixture]
	public class When_both_files_are_equal : Base2WaySpec
	{
		[Test]
		public void The_merged_file_equals_the_start_file()
		{
			// inputs
			string fileBodyLeft = File.ReadAllText(@"..\..\Notes.txt");
			string fileBodyRight = fileBodyLeft;

			TypeOfDiff typeOfDiff =
				SlyceMerge.PerformTwoWayDiff(identifyConflictsOnly,
					fileBodyLeft,
					fileBodyRight,
					out leftConflictLines,
					out rightConflictLines,
					out mergedText);

			// ignore differences in trailing CrLf
			fileBodyLeft = fileBodyLeft.Trim();
			mergedText = mergedText.Trim();
			
			Assert.AreEqual(TypeOfDiff.ExactCopy, typeOfDiff, "TypeOfDiffs are different.");
			Assert.AreEqual(-1, leftConflictLines.GetUpperBound(0), "Left conflict lines should be zero.");
			Assert.AreEqual(-1, rightConflictLines.GetUpperBound(0), "Right conflict lines should be zero.");
			Assert.AreEqual(fileBodyLeft, mergedText, "Merged text should equal start text.");
		}
	}

	[TestFixture]
	public class When_one_file_is_empty : Base2WaySpec
	{
		[Test]
		public void The_result_is_a_Diff_Warning()
		{
			// inputs
			string fileBodyLeft = File.ReadAllText(@"..\..\Notes.txt");
			string fileBodyRight = string.Empty;

			TypeOfDiff typeOfDiff =
				SlyceMerge.PerformTwoWayDiff(identifyConflictsAndRegions,
					fileBodyLeft,
					fileBodyRight,
					out leftConflictLines,
					out rightConflictLines,
					out mergedText);

			Assert.AreEqual(TypeOfDiff.Warning, typeOfDiff, "TypeOfDiffs are different.");
			Assert.AreEqual(0, leftConflictLines.GetUpperBound(0), "Left conflict lines should be zero.");
			Assert.AreEqual(-1, rightConflictLines.GetUpperBound(0), "Right conflict lines should be > zero.");
			Assert.AreEqual(fileBodyLeft.Trim(), mergedText.Trim(), "Merged text should equal start text.");
		}

		[Test]
		public void The_merged_file_equals_the_nonempty_file()
		{
			// inputs (test opposite order)
			string fileBodyLeft = string.Empty;
			string fileBodyRight = File.ReadAllText(@"..\..\Notes.txt");

			TypeOfDiff typeOfDiff =
				SlyceMerge.PerformTwoWayDiff(identifyConflictsAndRegions,
					fileBodyLeft,
					fileBodyRight,
					out leftConflictLines,
					out rightConflictLines,
					out mergedText);

			Assert.AreEqual(TypeOfDiff.Warning, typeOfDiff, "TypeOfDiffs are different.");
			Assert.AreEqual(-1, leftConflictLines.GetUpperBound(0), "Left conflict lines should be > zero.");
			Assert.AreEqual(0, rightConflictLines.GetUpperBound(0), "Right conflict lines should be zero.");
			Assert.AreEqual(fileBodyRight.Trim(), mergedText.Trim(), "Merged text should equal start text.");
		}
	}

	[TestFixture]
	public class When_left_file_contains_extra_line : Base2WaySpec
	{
		[Test]
		public void The_merged_file_equals_the_left_file()
		{
			// inputs
			string fileBodyLeft = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string fileBodyRight = Helper.GetResource(resourcePrefix + "TwoLines.txt");

			TypeOfDiff typeOfDiff =
				SlyceMerge.PerformTwoWayDiff(identifyConflictsAndRegions,
					fileBodyLeft,
					fileBodyRight,
					out leftConflictLines,
					out rightConflictLines,
					out mergedText);

			Assert.AreEqual(TypeOfDiff.Warning, typeOfDiff, "TypeOfDiffs are different.");
			Assert.AreEqual(0, leftConflictLines.GetUpperBound(0), "Left conflict lines should be zero.");
			Assert.AreEqual(-1, rightConflictLines.GetUpperBound(0), "Right conflict lines should be > zero.");
			Assert.AreEqual(fileBodyLeft.Trim(), mergedText.Trim(), "Left file should equal combined file.");
		}
	}

	[TestFixture]
	public class When_the_middle_line_is_different : Base2WaySpec
	{
		[Test]
		public void The_diff_type_is_conflict()
		{
			// inputs
			string fileBodyLeft = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string fileBodyRight = Helper.GetResource(resourcePrefix + "ThreeOtherLines.txt");
			string fileExpected = Helper.GetResource(resourcePrefix + "MiddleLineDifferent_Expected.txt");

			TypeOfDiff typeOfDiff =
				SlyceMerge.PerformTwoWayDiff(identifyConflictsAndRegions,
					fileBodyLeft,
					fileBodyRight,
					out leftConflictLines,
					out rightConflictLines,
					out mergedText);
			Assert.AreEqual(TypeOfDiff.Conflict, typeOfDiff, "TypeOfDiffs are different.");
			Assert.AreEqual(fileExpected.Trim(), mergedText.Trim(), "Combined file should equal expected file.");
			Assert.AreEqual(0, leftConflictLines.GetUpperBound(0), "Should be one left line in conflict.");
			Assert.AreEqual(0, rightConflictLines.GetUpperBound(0), "Should be one right line in conflict.");
			Assert.AreEqual(1, leftConflictLines[0].StartLine, "Left conflict lines should start at line 2 (index 1).");
			Assert.AreEqual(2, rightConflictLines[0].StartLine, "Right conflict lines should start at line 3 (index 2).");
		}
	}

	[TestFixture]
	public class When_the_first_line_is_different : Base2WaySpec
	{
		[Test]
		public void The_left_conflict_lines_start_from_0()
		{
			// inputs
			string fileBodyLeft = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string fileBodyRight = Helper.GetResource(resourcePrefix + "ThreeLines_FirstLineChanged.txt");
			string fileExpected = Helper.GetResource(resourcePrefix + "FirstLineDifferent_Expected.txt");

			TypeOfDiff typeOfDiff =
				SlyceMerge.PerformTwoWayDiff(identifyConflictsAndRegions,
					fileBodyLeft,
					fileBodyRight,
					out leftConflictLines,
					out rightConflictLines,
					out mergedText);
			Assert.AreEqual(TypeOfDiff.Conflict, typeOfDiff, "TypeOfDiffs are different.");
			Assert.AreEqual(fileExpected.Trim(), mergedText.Trim(), "Combined file should equal expected file.");
			Assert.AreEqual(0, leftConflictLines.GetUpperBound(0), "Should be one left line in conflict.");
			Assert.AreEqual(0, rightConflictLines.GetUpperBound(0), "Should be one right line in conflict.");
			Assert.AreEqual(0, leftConflictLines[0].StartLine, "Left conflict lines should start at line 1 (index 0).");
			Assert.AreEqual(1, rightConflictLines[0].StartLine, "Right conflict lines should start at line 2 (index 1).");
		}
	}

	[TestFixture]
	public class When_the_last_line_is_different : Base2WaySpec
	{
		[Test]
		public void The_left_conflict_lines_start_from_another_number()
		{
			// inputs
			string fileBodyLeft = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string fileBodyRight = Helper.GetResource(resourcePrefix + "ThreeLines_LastLineChanged.txt");
			string fileExpected = Helper.GetResource(resourcePrefix + "LastLineDifferent_Expected.txt");

			TypeOfDiff typeOfDiff =
				SlyceMerge.PerformTwoWayDiff(identifyConflictsAndRegions,
					fileBodyLeft,
					fileBodyRight,
					out leftConflictLines,
					out rightConflictLines,
					out mergedText);
			Assert.AreEqual(TypeOfDiff.Conflict, typeOfDiff, "TypeOfDiffs are different.");
			Assert.AreEqual(fileExpected.Trim(), mergedText.Trim(), "Combined file should equal expected file.");
			Assert.AreEqual(0, leftConflictLines.GetUpperBound(0), "Should be one left line in conflict.");
			Assert.AreEqual(0, rightConflictLines.GetUpperBound(0), "Should be one right line in conflict.");
			Assert.AreEqual(2, leftConflictLines[0].StartLine, "Left conflict lines should start at line 3 (index 2).");
			Assert.AreEqual(3, rightConflictLines[0].StartLine, "Right conflict lines should start at line 4 (index 3).");
		}
	}

	[TestFixture]
	public class When_all_the_lines_are_different : Base2WaySpec
	{
		[Test]
		public void The_result_is_LeftFile_plus_RightFile()
		{
			// inputs
			string fileBodyLeft = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string fileBodyRight = Helper.GetResource(resourcePrefix + "ThreeDiffLines.txt");
			string fileExpected = Helper.GetResource(resourcePrefix + "AllLinesDifferent_Expected.txt");

			TypeOfDiff typeOfDiff =
				SlyceMerge.PerformTwoWayDiff(identifyConflictsAndRegions,
					fileBodyLeft,
					fileBodyRight,
					out leftConflictLines,
					out rightConflictLines,
					out mergedText);
			Assert.AreEqual(TypeOfDiff.Conflict, typeOfDiff, "TypeOfDiffs are different.");
			Assert.AreEqual(fileExpected.Trim(), mergedText.Trim(), "Combined file should equal expected file.");
			Assert.AreEqual(2, leftConflictLines.GetUpperBound(0), "Should be one left line in conflict.");
			Assert.AreEqual(2, rightConflictLines.GetUpperBound(0), "Should be one right line in conflict.");
			Assert.AreEqual(0, leftConflictLines[0].StartLine, "Left conflict lines should start at line 1 (index 0).");
			Assert.AreEqual(3, rightConflictLines[0].StartLine, "Right conflict lines should start at line 4 (index 3).");
		}
	}

	[TestFixture]
	public class When_various_lines_are_different : Base2WaySpec
	{
		[Test]
		public void The_merged_file_needs_careful_testing()
		{
			// inputs
			string fileBodyLeft = Helper.GetResource(resourcePrefix + "MultiDiffs_Left.txt");
			string fileBodyRight = Helper.GetResource(resourcePrefix + "MultiDiffs_Right.txt");
			string fileExpected = Helper.GetResource(resourcePrefix + "MultiDiffs_Expected.txt");

			TypeOfDiff typeOfDiff =
				SlyceMerge.PerformTwoWayDiff(identifyConflictsAndRegions,
					fileBodyLeft,
					fileBodyRight,
					out leftConflictLines,
					out rightConflictLines,
					out mergedText);

			Assert.AreEqual(TypeOfDiff.Conflict, typeOfDiff, "TypeOfDiffs are different.");
			Assert.AreEqual(fileExpected.Trim(), mergedText.Trim(), "Combined file should equal expected file.");
			int leftConflictLinesMax = leftConflictLines.GetUpperBound(0);
			Assert.AreEqual(7, leftConflictLinesMax, "Should be 8 left lines in conflict.");
			int rightConflictLinesMax = rightConflictLines.GetUpperBound(0);
			Assert.AreEqual(7, rightConflictLinesMax, "Should be 8 right lines in conflict.");
			Assert.AreEqual(0, leftConflictLines[0].StartLine, "Left conflict lines should start at line 1 (index 0).");
			Assert.AreEqual(23, leftConflictLines[leftConflictLinesMax].StartLine, "Last left conflict line should start at line 24 (index 23).");
			Assert.AreEqual(2, rightConflictLines[0].StartLine, "Right conflict lines should start at line 3 (index 2).");
			Assert.AreEqual(25, rightConflictLines[rightConflictLinesMax].EndLine, "Last right conflict lines should start at line 26 (index 25).");
		}
	}
}
