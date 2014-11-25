using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Slyce.Common;
using Slyce.IntelliMerge;
using Slyce.IntelliMerge.UnitTesting;

namespace Specs_for_IntelliMerge_3way
{
	[TestFixture]
	public class When_all_three_files_are_equal : Base3WaySpec
	{
		[Test]
		public void Even_when_all_files_are_empty()
		{
			string userText = string.Empty;
			string prevGenText = userText;
			string newGenText = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(String.Empty, merged, "The merged text should be empty.");
			Assert.AreEqual(TypeOfDiff.ExactCopy, mergeObject.DiffType, "Mark as Exact Copy.");
		}

		[Test]
		public void The_merged_equals_3CrLfs_input()
		{
			string userText = Environment.NewLine + Environment.NewLine + Environment.NewLine;
			string prevGenText = userText;
			string newGenText = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.ExactCopy, mergeObject.DiffType, "Mark as Exact Copy.");
			Assert.AreEqual(userText, merged, "The merged text should equal the 3 empty lines.");
		}

		[Test]
		public void The_merged_equals_the_2line_input()
		{
			string userText = Helper.GetResource(resourcePrefix + "TwoLines.txt");
			string prevGenText = userText;
			string newGenText = userText;
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.ExactCopy, mergeObject.DiffType, "Mark as Exact Copy.");
			Assert.AreEqual(fileExpected, merged, "The merged text should equal the 2 line input.");
		}

		[Test]
		public void The_merged_equals_the_simple_input_with_linebreaks()
		{
			string userText = Helper.GetResource(resourcePrefix + "TwoLinesWithEmptyLines.txt");
			string prevGenText = userText;
			string newGenText = userText;
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.ExactCopy, mergeObject.DiffType, "Mark as Exact Copy.");
			Assert.AreEqual(fileExpected, merged, "The merged text should equal the input.");
		}
	}

	[TestFixture]
	public class When_PrevGen_is_empty_and_NewGen_and_User_are_equal : Base3WaySpec
	{
		[Test]
		public void The_merged_file_equals_User_and_NewGen()
		{
			string userText = Helper.GetResource(resourcePrefix + "TwoLinesWithEmptyLines.txt");
			string prevGenText = string.Empty;
			string newGenText = userText;
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.ExactCopy, mergeObject.DiffType, "Mark as Exact Copy.");
			Assert.AreEqual(fileExpected, merged, "Merged file should equal User and NewGen.");
		}
	}

	[TestFixture]
	public class When_PrevGen_is_empty_and_NewGen_and_User_differ : Base3WaySpec
	{
		[Test]
		public void The_merged_file_equals_2way_merge_of_User_and_NewGen_CONFLICT()
		{
			string userText = Helper.GetResource(resourcePrefix + "FourLines.txt");
			string prevGenText = string.Empty;
			string newGenText = Helper.GetResource(resourcePrefix + "FourLines_2Different.txt");
			string fileExpected = Helper.GetResource(resourcePrefix + "FourLines_Expected.txt");

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.Conflict, mergeObject.DiffType, "Mark as Exact Copy.");
			Assert.AreEqual(fileExpected, merged, "Merged file should equal merge of User and NewGen.");
		}
	}

	[TestFixture]
	public class When_UserText_equals_PrevGenText_and_NewGenText_is_changed : Base3WaySpec
	{
		[Test]
		public void The_merged_file_equals_the_NewGen_TEMPLATECHANGE()
		{
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string prevGenText = userText;
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeOtherLines.txt");

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.TemplateChangeOnly, mergeObject.DiffType, "Mark as Exact Copy.");
			Assert.AreEqual(newGenText, merged, "Merged file should equal NewGen.");
		}
	}

	[TestFixture]
	public class When_the_same_line_is_different_in_TWO_of_the_files : Base3WaySpec
	{
		[Test]
		public void NewGen_changes_supercede_unchanged_User()
		{
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeOtherLines.txt");
			string fileExpected = newGenText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.TemplateChangeOnly, mergeObject.DiffType, "NewGen change only.");
			Assert.AreEqual(fileExpected, merged, "NewGen supercedes unchanged user.");
		}

		[Test]
		public void User_changes_merge_with_unchanged_NewGen()
		{
			string userText = Helper.GetResource(resourcePrefix + "ThreeOtherLines.txt");
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.UserChangeOnly, mergeObject.DiffType, "User change only.");
			Assert.AreEqual(fileExpected, merged, "NewGen supercedes unchanged user.");
		}
	}

	[TestFixture]
	public class When_the_same_line_is_different_in_all_three_files : Base3WaySpec
	{
		[Test]
		public void Cannot_merge_Mark_as_a_conflict()
		{
			string userText = Helper.GetResource(resourcePrefix + "ThreeOtherLines2.txt");
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeOtherLines.txt");

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.Conflict, mergeObject.DiffType, "Cannot merge. Mark as conflicted.");
		}
	}
	
	[TestFixture]
	public class When_a_line_is_deleted : Base3WaySpec
	{
		[Test]
		public void From_NewGen_remove_from_merge()
		{
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines_FirstLineDeleted.txt");
			string fileExpected = newGenText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.TemplateChangeOnly, mergeObject.DiffType, "User change only.");
			Assert.AreEqual(fileExpected, merged, "NewGen supercedes unchanged user.");
		}

		[Test]
		public void From_User_remove_from_merge()
		{
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines_FirstLineDeleted.txt");
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.UserChangeOnly, mergeObject.DiffType, "User change only.");
			Assert.AreEqual(fileExpected, merged, "User supercedes unchanged NewGen.");
		}
	}

	[TestFixture]
	public class When_a_line_is_deleted_in_NewGen_AND_changed_in_User : Base3WaySpec
	{
		[Test]
		public void Last_line_changed_Cannot_merge_Mark_as_a_conflict()
		{
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines_LastLineChanged.txt");
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "TwoLines.txt");
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.Conflict, mergeObject.DiffType, "Cannot merge. Mark as conflicted.");
		}

		[Test]
		public void First_line_changed_Cannot_merge_Mark_as_a_conflict()
		{
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines_FirstLineChanged.txt");
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines_FirstLineDeleted.txt");
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.Conflict, mergeObject.DiffType, "Cannot merge. Mark as conflicted."); 
		}
	}

	[TestFixture]
	public class When_a_line_is_deleted_in_User_AND_changed_in_NewGen : Base3WaySpec
	{
		[Test]
		public void Last_line_changed_Go_with_the_User_version()
		{
			string userText = Helper.GetResource(resourcePrefix + "TwoLines.txt");
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines_LastLineChanged.txt");
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.Conflict, mergeObject.DiffType, "Cannot merge. Mark as conflicted."); 
		}

		[Test]
		public void First_line_changed_Go_with_the_User_version()
		{
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines_FirstLineDeleted.txt");
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines_FirstLineChanged.txt");
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.Conflict, mergeObject.DiffType, "Cannot merge. Mark as conflicted."); 
		}
	}

	[TestFixture]
	public class When_a_line_is_inserted_in_one_file : Base3WaySpec
	{
	    [Test]
	    public void First_line_inserted_in_NewGen_Include_it()
	    {
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines_FirstLineDeleted.txt");
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines_FirstLineDeleted.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string fileExpected = newGenText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.TemplateChangeOnly, mergeObject.DiffType, "NewGen change only.");
			Assert.AreEqual(fileExpected, merged, "NewGen change incorporated.");
		}

		[Test]
		public void Last_line_inserted_in_NewGen_Include_it()
		{
			string prevGenText = Helper.GetResource(resourcePrefix + "TwoLines.txt");
			string userText = Helper.GetResource(resourcePrefix + "TwoLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string fileExpected = newGenText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.TemplateChangeOnly, mergeObject.DiffType, "NewGen change only.");
			Assert.AreEqual(fileExpected, merged, "NewGen change incorporated.");
		}

		[Test]
		public void First_line_inserted_in_User_Include_it()
		{
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines_FirstLineDeleted.txt");
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines_FirstLineDeleted.txt");
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.UserChangeOnly, mergeObject.DiffType, "User change only.");
			Assert.AreEqual(fileExpected, merged, "User change incorporated.");
		}

		[Test]
		public void Last_line_inserted_in_User_Include_it()
		{
			string prevGenText = Helper.GetResource(resourcePrefix + "TwoLines.txt");
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "TwoLines.txt");
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.UserChangeOnly, mergeObject.DiffType, "User change only.");
			Assert.AreEqual(fileExpected, merged, "User change incorporated.");
		}
	}

	[TestFixture]
	public class When_a_new_line_is_added_in_2_files : Base3WaySpec
	{
		[Test]
		public void Same_last_line_added_to_User_and_NewGen_Include_it()
		{
			string prevGenText = Helper.GetResource(resourcePrefix + "TwoLines.txt");
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.ExactCopy, mergeObject.DiffType, "Same changes to both files.");
			Assert.AreEqual(fileExpected, merged, "User change incorporated.");
		}

		[Test]
		public void Same_first_line_added_to_User_and_NewGen_Include_it()
		{
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines_FirstLineDeleted.txt");
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.ExactCopy, mergeObject.DiffType, "Same changes to both files.");
			Assert.AreEqual(fileExpected, merged, "User change incorporated.");
		}
	}

	[TestFixture]
	public class When_text_is_added_to_the_start_of_a_line : Base3WaySpec
	{
		[Test]
		public void To_User_line1_Include_it()
		{
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines_FirstLine_InsertAtStart.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.UserChangeOnly, mergeObject.DiffType, "User change only.");
			Assert.AreEqual(fileExpected, merged, "User change incorporated.");
		}

		[Test]
		public void To_NewGen_line1_Include_it()
		{
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines_FirstLine_InsertAtStart.txt");
			string fileExpected = newGenText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.TemplateChangeOnly, mergeObject.DiffType, "NewGen change only.");
			Assert.AreEqual(fileExpected, merged, "NewGen change incorporated.");
		}
	}

	[TestFixture]
	public class When_text_is_added_to_the_middle_of_a_line : Base3WaySpec
	{
		[Test]
		public void To_User_line1_Include_it()
		{
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines_MiddleLine_InsertAtMiddle.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.UserChangeOnly, mergeObject.DiffType, "User change only.");
			Assert.AreEqual(fileExpected, merged, "User change incorporated.");
		}

		[Test]
		public void To_NewGen_line1_Include_it()
		{
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines_MiddleLine_InsertAtMiddle.txt");
			string fileExpected = newGenText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.TemplateChangeOnly, mergeObject.DiffType, "NewGen change only.");
			Assert.AreEqual(fileExpected, merged, "NewGen change incorporated.");
		}
	}

	[TestFixture]
	public class When_text_is_added_to_the_end_of_a_line : Base3WaySpec
	{
		[Test]
		public void To_User_line1_Include_it()
		{
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines_LastLineChanged.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string fileExpected = userText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.UserChangeOnly, mergeObject.DiffType, "User change only.");
			Assert.AreEqual(fileExpected, merged, "User change incorporated.");
		}

		[Test]
		public void To_NewGen_line1_Include_it()
		{
			string prevGenText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string userText = Helper.GetResource(resourcePrefix + "ThreeLines.txt");
			string newGenText = Helper.GetResource(resourcePrefix + "ThreeLines_LastLineChanged.txt");
			string fileExpected = newGenText;

            SlyceMergeResult mergeObject = SlyceMerge.Perform3wayDiff(userText, prevGenText, newGenText, out merged);
			Assert.AreEqual(TypeOfDiff.TemplateChangeOnly, mergeObject.DiffType, "NewGen change only.");
			Assert.AreEqual(fileExpected, merged, "NewGen change incorporated.");
		}
	}
}
