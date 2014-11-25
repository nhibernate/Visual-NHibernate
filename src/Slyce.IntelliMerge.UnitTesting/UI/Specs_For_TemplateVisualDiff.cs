using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.IntelliMerge.UI.VisualDiff;

namespace Specs_For_Template_Visual_Diff
{
	[TestFixture]
	public class Basic_Tests
	{
		[Test]
		public void BasicRun()
		{
			TwoWayDiff diffAlgorithm = new TwoWayDiff();

			DiffResult result = diffAlgorithm.PerformDiff("Common Text\nLeft Text\nCommon Text", "Common Text\nRight Text\nCommon Text");

			Assert.That(result.MergedSuccessfully, Is.False, "Merge should not have been successful");
			Assert.That(result.ConflictBlocks.Count, Is.EqualTo(1), "Wrong number of Blocks in conflict");

			Assert.That(result.BlockCount, Is.EqualTo(3), "Wrong number of blocks.");

			Block left = result.ConflictBlocks[0].Left;
			Block right = result.ConflictBlocks[0].Right;
			Block merged = result.ConflictBlocks[0].Merged;

			CommonTests.CheckBlocksAreNotNull(left, right, merged);
			Assert.That(result.ConflictBlocks[0].Base, Is.Null, "Base is not null");

			CommonTests.AssertConflictBlockHasThisText(left, right, merged, "Left Text", "Right Text");

			ReadOnlyCollection<ObjectList<Block>> blocks = result.GetBlocks();
			left = blocks[0].Left;
			right = blocks[0].Right;
			merged = blocks[0].Merged;

			CommonTests.CheckBlocksAreNotNull(left, right, merged);
			Assert.That(blocks[0].Base, Is.Null, "Base is not null");

			CommonTests.AssertAllHaveSameSingleLineOfText(left, right, merged, "Common Text");

			left = blocks[2].Left;
			right = blocks[2].Right;
			merged = blocks[2].Merged;

			CommonTests.CheckBlocksAreNotNull(left, right, merged);
			Assert.That(blocks[2].Base, Is.Null, "Base is not null");
			
			CommonTests.AssertAllHaveSameSingleLineOfText(left, right, merged, "Common Text");
		}

		[Test]
		public void End_Text_Not_The_Same()
		{
			TwoWayDiff diffAlgorithm = new TwoWayDiff();

			DiffResult result = diffAlgorithm.PerformDiff("Common Text\nLeft Text", "Common Text\nRight Text");

			Assert.That(result.MergedSuccessfully, Is.False, "Merge should not have been successful");
			Assert.That(result.ConflictBlocks.Count, Is.EqualTo(1), "Wrong number of Blocks in conflict");

			Assert.That(result.BlockCount, Is.EqualTo(2), "Wrong number of blocks.");

			Block left = result.ConflictBlocks[0].Left;
			Block right = result.ConflictBlocks[0].Right;
			Block merged = result.ConflictBlocks[0].Merged;

			CommonTests.CheckBlocksAreNotNull(left, right, merged);
			Assert.That(result.ConflictBlocks[0].Base, Is.Null, "Base is not null");

			CommonTests.AssertConflictBlockHasThisText(left, right, merged, "Left Text", "Right Text");

			ReadOnlyCollection<ObjectList<Block>> blocks = result.GetBlocks();
			left = blocks[0].Left;
			right = blocks[0].Right;
			merged = blocks[0].Merged;

			CommonTests.CheckBlocksAreNotNull(left, right, merged);
			Assert.That(blocks[0].Base, Is.Null, "Base is not null");

			CommonTests.AssertAllHaveSameSingleLineOfText(left, right, merged, "Common Text");
		}
	}

	[TestFixture]
	public class Operations
	{
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Error_Thrown_When_Selecting_Merged_Version_Of_Conflict()
		{
			TwoWayDiff diffAlgorithm = new TwoWayDiff();
			DiffResult result = diffAlgorithm.PerformDiff("Common Text\nLeft Text\nCommon Text2", "Common Text\nRight Text\nCommon Text2");

			Assert.That(result.MergedSuccessfully, Is.False, "Merge should not have been successful");
			Assert.That(result.ConflictBlocks.Count, Is.EqualTo(1), "Wrong number of Blocks in conflict");
			Assert.That(result.BlockCount, Is.EqualTo(3), "Wrong number of blocks.");

			result.SelectVersionOfConflict(1, ObjectVersion.Merged);
		}

		[Test]
		public void Select_Left_Version_Of_Conflict()
		{
			Select_One_Version_Of_Conflict(ObjectVersion.Left, "Left Text");
		}

		[Test]
		public void Select_Right_Version_Of_Conflict()
		{
			Select_One_Version_Of_Conflict(ObjectVersion.Right, "Right Text");
		}

		[Test]
		public void Change_Line_Of_Text()
		{
			TwoWayDiff diffAlgorithm = new TwoWayDiff();
			DiffResult result = diffAlgorithm.PerformDiff("Common Text\nLeft Text\nCommon Text2", "Common Text\nRight Text\nCommon Text2");

			result.ChangeLine(1, "Line Text");

			Assert.That(result.Merged.Lines[1], Is.EqualTo("Line Text"));
		}

		[Test(Description = 
		@"This test makes sure that if you change a line to the same as the left or right, it registers as a match.")]
		public void Change_Line_Of_Text_To_Left()
		{
			TwoWayDiff diffAlgorithm = new TwoWayDiff();
			DiffResult result = diffAlgorithm.PerformDiff("Common Text\nLeft Text\nCommon Text2", "Common Text\nRight Text\nCommon Text2");

			result.InsertLine(1, 0, "Left Text");

			Assert.That(result.Merged.Lines[1], Is.EqualTo("Left Text"));
			Assert.That(result.MergedSuccessfully, Is.True);
		}

		[Test]
		public void Change_Line_Of_Text_From_Left()
		{
			TwoWayDiff diffAlgorithm = new TwoWayDiff();
			DiffResult result = diffAlgorithm.PerformDiff("Common Text\nLeft Text\nCommon Text2", "Common Text\nRight Text\nCommon Text2");

			Assert.That(result.MergedSuccessfully, Is.False);

			result.InsertLine(1, 0, "Left Text");

			Assert.That(result.Merged.Lines[1], Is.EqualTo("Left Text"));
			Assert.That(result.MergedSuccessfully, Is.True);

			// Change the line back
			result.ChangeLine(1, "Different Text");

			Assert.That(result.Merged.Lines[1], Is.EqualTo("Different Text"));
			Assert.That(result.MergedSuccessfully, Is.False);
		}

		[Test]
		public void Remove_Line_Of_Existing_Text()
		{
			TwoWayDiff diffAlgorithm = new TwoWayDiff();
			DiffResult result = diffAlgorithm.PerformDiff("Common Text\nLeft Text\nCommon Text2", "Common Text\nRight Text\nCommon Text2");

			Assert.That(result.MergedSuccessfully, Is.False);
			Assert.That(result.ConflictBlocks.Count, Is.EqualTo(1));

			result.RemoveLine(2, 0);

			Assert.That(result.Merged.Lines.Count, Is.EqualTo(2));
			Assert.That(result.ConflictBlocks.Count, Is.EqualTo(2));
			Assert.That(result.MergedSuccessfully, Is.False);
		}

		[Test]
		public void Remove_Line_Of_Text_Weve_Just_Added()
		{
			TwoWayDiff diffAlgorithm = new TwoWayDiff();
			DiffResult result = diffAlgorithm.PerformDiff("Common Text\nLeft Text\nCommon Text2", "Common Text\nRight Text\nCommon Text2");

			Assert.That(result.MergedSuccessfully, Is.False);

			result.SelectVersionOfConflict(1, ObjectVersion.Left);

			Assert.That(result.MergedSuccessfully, Is.True);

			result.InsertLine(1, 0, "New Text");

			Assert.That(result.Merged.Lines.Count, Is.EqualTo(5));
			Assert.That(result.Merged.Lines[1], Is.EqualTo("New Text"));
			Assert.That(result.MergedSuccessfully, Is.False);

			// Change the line back
			result.RemoveLine(1, 0);

			Assert.That(result.Merged.Lines[1], Is.EqualTo("Left Text"));
			Assert.That(result.MergedSuccessfully, Is.True);
		}
		
		private void Select_One_Version_Of_Conflict(ObjectVersion versionToUse, string mergedText)
		{
			TwoWayDiff diffAlgorithm = new TwoWayDiff();
			DiffResult result = diffAlgorithm.PerformDiff("Common Text\nLeft Text\nCommon Text2", "Common Text\nRight Text\nCommon Text2");

			Assert.That(result.MergedSuccessfully, Is.False, "Merge should not have been successful");
			Assert.That(result.ConflictBlocks.Count, Is.EqualTo(1), "Wrong number of Blocks in conflict");
			Assert.That(result.BlockCount, Is.EqualTo(3), "Wrong number of blocks.");

			// Use the Left text for the conflict.
			result.SelectVersionOfConflict(1, versionToUse);

			ReadOnlyCollection<ObjectList<Block>> blocks = result.GetBlocks();

			Block left = blocks[0].Left;
			Block right = blocks[0].Right;
			Block merged = blocks[0].Merged;

			CommonTests.CheckBlocksAreNotNull(left, right, merged);
			Assert.That(blocks[0].Base, Is.Null, "Base is not null");

			CommonTests.AssertAllHaveSameSingleLineOfText(left, right, merged, "Common Text");

			left = blocks[1].Left;
			right = blocks[1].Right;
			merged = blocks[1].Merged;

			CommonTests.AssertAllHaveOneLineOfText(left, right, merged);

			CommonTests.AssertSingleLineOfTextIneachVersionIsEqualTo(left, right, merged, "Left Text", "Right Text", mergedText);

			left = blocks[2].Left;
			right = blocks[2].Right;
			merged = blocks[2].Merged;

			CommonTests.CheckBlocksAreNotNull(left, right, merged);
			Assert.That(blocks[0].Base, Is.Null, "Base is not null");

			CommonTests.AssertAllHaveSameSingleLineOfText(left, right, merged, "Common Text2");

			Assert.That(result.MergedSuccessfully, Is.True, "MergedSucessfully was not set to true after all conflicts handled.");
		}
	}

	internal static class CommonTests
	{
		public static void AssertConflictBlockHasThisText(Block left, Block right, Block merged, string leftText, string rightText)
		{
			Assert.That(left.Text.Length, Is.EqualTo(1), "Left is wrong length");
			Assert.That(right.Text.Length, Is.EqualTo(1), "Right is wrong length");
			Assert.That(merged.Text.Length, Is.EqualTo(0), "Merged is wrong length");

			Assert.That(left.Text[0], Is.EqualTo(leftText), "Left text is wrong");
			Assert.That(right.Text[0], Is.EqualTo(rightText), "Right text is wrong");
		}

		public static void AssertAllHaveSameSingleLineOfText(Block left, Block right, Block merged, string expectedText)
		{
			AssertAllHaveOneLineOfText(left, right, merged);

			Assert.That(left.Text[0], Is.EqualTo(expectedText), "Left text is wrong");
			Assert.That(right.Text[0], Is.EqualTo(expectedText), "Right text is wrong");
			Assert.That(merged.Text[0], Is.EqualTo(expectedText), "Merged text is wrong");
		}

		public static void CheckBlocksAreNotNull(Block left, Block right, Block merged)
		{
			Assert.That(left, Is.Not.Null, "Left is null");
			Assert.That(right, Is.Not.Null, "Right is null");
			Assert.That(merged, Is.Not.Null, "Merged is null");
		}

		public static void AssertAllHaveOneLineOfText(Block left, Block right, Block merged)
		{
			Assert.That(left.Text.Length, Is.EqualTo(1), "Left is wrong length");
			Assert.That(right.Text.Length, Is.EqualTo(1), "Right is wrong length");
			Assert.That(merged.Text.Length, Is.EqualTo(1), "Merged is wrong length");
		}

		public static void AssertSingleLineOfTextIneachVersionIsEqualTo(Block left, Block right, Block merged, string leftExpected, string rightExpected, string mergedExpected)
		{
			Assert.That(left.Text[0], Is.EqualTo(leftExpected), "Left text is wrong");
			Assert.That(right.Text[0], Is.EqualTo(rightExpected), "Right text is wrong");
			Assert.That(merged.Text[0], Is.EqualTo(mergedExpected), "Merged text is wrong");
		}
	}
}