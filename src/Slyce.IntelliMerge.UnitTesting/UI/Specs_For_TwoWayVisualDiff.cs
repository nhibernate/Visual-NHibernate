using System;
using System.Collections.Generic;
using Algorithm.Diff;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge.UI.VisualDiff;

namespace Specs_For_TwoWay_Visual_Diff
{
	
	[TestFixture]
	// Adding a line to one is indistinguishable from removing a line from the other.
	public class Add_Lines : TestBase
	{
		[Test]
		public void User_Added_At_Start()
		{
			TwoWayVisualDiff diffUtility = GetDiffUtility("line 1", "line 0\nline 1");
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

			Assert.That(output.LineCount, Is.EqualTo(2));
			CheckLines(output.LeftLines, new [] {true, false});
			CheckLines(output.RightLines, new[] { false, false });

			CheckLines(output.LeftLines, new[] { "", "line 1" });
			CheckLines(output.RightLines, new[] { "line 0", "line 1" });

			CheckLines(output.LeftLines, new[] { ChangeType.None, ChangeType.None });
			CheckLines(output.RightLines, new[] { ChangeType.User, ChangeType.None });
		}

		[Test]
		public void User_Added_At_In_Middle()
		{
			TwoWayVisualDiff diffUtility = GetDiffUtility("line 1\nline 3", "line 1\nline 2\nline 3");
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

			Assert.That(output.LineCount, Is.EqualTo(3));
			CheckLines(output.LeftLines, new[] { false, true });
			CheckLines(output.RightLines, new[] { false, false });

			CheckLines(output.LeftLines, new[] { "line 1", "", "line 3" });
			CheckLines(output.RightLines, new[] { "line 1", "line 2", "line 3" });

			CheckLines(output.LeftLines, new[] { ChangeType.None, ChangeType.None, ChangeType.None });
			CheckLines(output.RightLines, new[] { ChangeType.None, ChangeType.User, ChangeType.None });
		}

		[Test]
		public void User_Added_At_End()
		{
			TwoWayVisualDiff diffUtility = GetDiffUtility("line 1", "line 1\nline 2");
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

			Assert.That(output.LineCount, Is.EqualTo(2));
			CheckLines(output.LeftLines, new[] { false, true });
			CheckLines(output.RightLines, new[] { false, false });

			CheckLines(output.LeftLines, new[] { "line 1", "" });
			CheckLines(output.RightLines, new[] { "line 1", "line 2" });

			CheckLines(output.LeftLines, new[] { ChangeType.None, ChangeType.None });
			CheckLines(output.RightLines, new[] { ChangeType.None, ChangeType.User });
		}

		[Test]
		public void Template_Added_At_Start()
		{
			TwoWayVisualDiff diffUtility = GetDiffUtility("line 0\nline 1", "line 1");
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

			Assert.That(output.LineCount, Is.EqualTo(2));
			CheckLines(output.LeftLines, new[] { true, false });
			CheckLines(output.RightLines, new[] { false, false });

			CheckLines(output.LeftLines, new[] { "", "line 1" });
			CheckLines(output.RightLines, new[] { "line 0", "line 1" });

			CheckLines(output.LeftLines, new[] { ChangeType.None, ChangeType.None });
			CheckLines(output.RightLines, new[] { ChangeType.Template, ChangeType.None });
		}

		[Test]
		public void Template_Added_At_In_Middle()
		{
			TwoWayVisualDiff diffUtility = GetDiffUtility("line 1\nline 2\nline 3", "line 1\nline 3");
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

			Assert.That(output.LineCount, Is.EqualTo(3));
			
			CheckLines(output.LeftLines, new[] { false, true });
			CheckLines(output.RightLines, new[] { false, false });

			CheckLines(output.LeftLines, new[] { "line 1", "", "line 3" });
			CheckLines(output.RightLines, new[] { "line 1", "line 2", "line 3" });

			CheckLines(output.LeftLines, new[] { ChangeType.None, ChangeType.None, ChangeType.None });
			CheckLines(output.RightLines, new[] { ChangeType.None, ChangeType.Template, ChangeType.None });
		}

		[Test]
		public void Template_Added_At_End()
		{
			TwoWayVisualDiff diffUtility = GetDiffUtility("line 1\nline 2", "line 1");
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

			Assert.That(output.LineCount, Is.EqualTo(2));
			CheckLines(output.LeftLines, new[] { false, true });
			CheckLines(output.RightLines, new[] { false, false });

			CheckLines(output.LeftLines, new[] { "line 1", "" });
			CheckLines(output.RightLines, new[] { "line 1", "line 2" });

			CheckLines(output.LeftLines, new[] { ChangeType.None, ChangeType.None });
			CheckLines(output.RightLines, new[] { ChangeType.None, ChangeType.Template });
		}
	}
	
	[TestFixture]
	public class Changing_Lines : TestBase
	{
		[Test]
		public void Change_At_Start()
		{
			TwoWayVisualDiff diffUtility = GetDiffUtility("line 1\nline 2", "line 11\nline 2");
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

			Assert.That(output.LineCount, Is.EqualTo(3));
			CheckLines(output.LeftLines, new[] { false, false, false });
			CheckLines(output.RightLines, new[] { true, true, false });

			CheckLines(output.LeftLines, new[] { "line 11", "line 1", "line 2" });
			CheckLines(output.RightLines, new[] { "", "", "line 2" });

			CheckLines(output.LeftLines, new[] { ChangeType.User, ChangeType.Template, ChangeType.None });
			CheckLines(output.RightLines, new[] { ChangeType.None, ChangeType.None, ChangeType.None });
		}

		[Test]
		public void Change_In_Middle()
		{
			TwoWayVisualDiff diffUtility = GetDiffUtility("line 1\nline 22\nline 3", "line 1\nline 2\nline 3");
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

			Assert.That(output.LineCount, Is.EqualTo(4));
			CheckLines(output.LeftLines, new[] { false, false, false, false });
			CheckLines(output.RightLines, new[] { false, true, true, false });

			CheckLines(output.LeftLines, new[] { "line 1", "line 2", "line 22", "line 3" });
			CheckLines(output.RightLines, new[] { "line 1", "", "", "line 3" });

			CheckLines(output.LeftLines, new[] { ChangeType.None, ChangeType.User, ChangeType.Template, ChangeType.None });
			CheckLines(output.RightLines, new[] { ChangeType.None, ChangeType.None, ChangeType.None, ChangeType.None });
		}

		[Test]
		public void Change_At_End()
		{
			TwoWayVisualDiff diffUtility = GetDiffUtility("line 1\nline 2", "line 1\nline 22");
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

			Assert.That(output.LineCount, Is.EqualTo(3));
			CheckLines(output.LeftLines, new[] { false, false, false });
			CheckLines(output.RightLines, new[] { false, true, true });

			CheckLines(output.LeftLines, new[] { "line 1", "line 22", "line 2" });
			CheckLines(output.RightLines, new[] { "line 1", "", "" });

			CheckLines(output.LeftLines, new[] { ChangeType.None, ChangeType.User, ChangeType.Template });
			CheckLines(output.RightLines, new[] { ChangeType.None, ChangeType.None, ChangeType.None });
		}
	}

	[TestFixture]
	public class BasicTests : TestBase
	{
		[Test]
		public void Files_Are_Exact_Copy()
		{
			TwoWayVisualDiff diffUtility = GetDiffUtility("line 1", "line 1");

			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

			Assert.That(output, Is.Not.Null);
			Assert.That(output.LineCount, Is.EqualTo(1));
			Assert.That(output.LeftLines[0].Text, Is.EqualTo("line 1"));
			Assert.That(output.RightLines[0].Text, Is.EqualTo("line 1"));
		}

		[Test]
		public void Files_Are_Exact_Copy_PrevGen_Missing()
		{
			TextFileInformation tfi = new TextFileInformation();
			tfi.PrevGenFile = TextFile.Blank;
			tfi.UserFile = tfi.NewGenFile = new TextFile("line 1");
			tfi.IntelliMerge = IntelliMergeType.PlainText;
			tfi.RelativeFilePath = "text.txt";

			TwoWayVisualDiff diffUtility = new TwoWayVisualDiff(tfi);

			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

			Assert.That(output, Is.Not.Null);
			Assert.That(output.LineCount, Is.EqualTo(1));
			Assert.That(output.LeftLines[0].Text, Is.EqualTo("line 1"));
			Assert.That(output.RightLines[0].Text, Is.EqualTo("line 1"));
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void Files_Are_Missing_User()
		{
			TextFileInformation tfi = new TextFileInformation();
			
			tfi.UserFile = TextFile.Blank;
			tfi.PrevGenFile = tfi.NewGenFile = new TextFile("line 1");
			tfi.IntelliMerge = IntelliMergeType.PlainText;
			tfi.RelativeFilePath = "text.txt";

			new TwoWayVisualDiff(tfi);
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void Files_Are_Missing_NewGen()
		{
			TextFileInformation tfi = new TextFileInformation();

			tfi.NewGenFile = TextFile.Blank;
			tfi.PrevGenFile = tfi.UserFile = new TextFile("line 1");
			tfi.IntelliMerge = IntelliMergeType.PlainText;
			tfi.RelativeFilePath = "text.txt";

			new TwoWayVisualDiff(tfi);
		}

		[Test, ExpectedException(typeof(InvalidOperationException))]
		public void Files_Are_Removed_User()
		{
			TextFileInformation tfi = new TextFileInformation();

			tfi.PrevGenFile = tfi.NewGenFile = tfi.UserFile = new TextFile("line 1");
			tfi.IntelliMerge = IntelliMergeType.PlainText;
			tfi.RelativeFilePath = "text.txt";

			TwoWayVisualDiff diffUtility = new TwoWayVisualDiff(tfi);
			tfi.UserFile = TextFile.Blank;
			diffUtility.ProcessMergeOutput();
		}

		[Test, ExpectedException(typeof(InvalidOperationException))]
		public void Files_Are_Removed_NewGen()
		{
			TextFileInformation tfi = new TextFileInformation();

			tfi.PrevGenFile = tfi.UserFile = tfi.NewGenFile = new TextFile("line 1");
			tfi.IntelliMerge = IntelliMergeType.PlainText;
			tfi.RelativeFilePath = "text.txt";

			TwoWayVisualDiff diffUtility = new TwoWayVisualDiff(tfi);
			tfi.NewGenFile = TextFile.Blank;
			diffUtility.ProcessMergeOutput();
		}
	}

	public class TestBase
	{
		protected static TwoWayVisualDiff GetDiffUtility(string newgen, string user)
		{
			TextFileInformation tfi = new TextFileInformation();

			tfi.PrevGenFile = TextFile.Blank;
			tfi.UserFile = new TextFile(user);
			tfi.NewGenFile = new TextFile(newgen);
			tfi.IntelliMerge = IntelliMergeType.PlainText;
			tfi.RelativeFilePath = "text.txt";

			return new TwoWayVisualDiff(tfi);
		}

		protected static void CheckLines(IList<DiffLine> lines, bool[] virtualState)
		{
			for (int i = 0; i < virtualState.Length; i++)
			{
				Assert.That(lines[i].IsVirtual, Is.EqualTo(virtualState[i]));
			}
		}

		protected static void CheckLines(IList<DiffLine> lines, string[] lineText)
		{
			for (int i = 0; i < lineText.Length; i++)
			{
				Assert.That(lines[i].Text, Is.EqualTo(lineText[i]));
			}
		}

		protected static void CheckLines(IList<DiffLine> lines, ChangeType[] changeTypes)
		{
			for (int i = 0; i < changeTypes.Length; i++)
			{
				Assert.That(lines[i].Change, Is.EqualTo(changeTypes[i]));
			}
		}
	}
}
