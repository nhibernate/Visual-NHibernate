using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge.UI.VisualDiff;
using TypeOfDiff=Slyce.IntelliMerge.TypeOfDiff;

namespace Specs_for_IntelliMerge_UI
{
    [TestFixture]
    public class TestSyntaxEditorSetup
    {
        [Test]
        public void TestExactCopy()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.NewGenFile = new TextFile("aklsdjflkjasdf");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

        	ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
            VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));
            Assert.That(output.RightLines, Has.Count(1));
            Assert.That(output.LeftLines, Has.Count(1));
            Assert.That(output.LeftLines[0].Text, Is.EqualTo(tfi.NewGenFile.GetContents()));
            Assert.That(output.RightLines[0].Text, Is.EqualTo(tfi.NewGenFile.GetContents()));
        }

        [Test]
        public void TestDeleteLine()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.NewGenFile = new TextFile("line 0\r\nline 1\r\nline 2");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Has.Count(3));
            Assert.That(output.LeftLines, Has.Count(3));

            output.RemoveLine(0);
            Assert.That(output.RightLines, Has.Count(2));
            Assert.That(output.LeftLines, Has.Count(2));
            Assert.That(output.RightLines[0].Text, Is.EqualTo("line 1"));
            Assert.That(output.LeftLines[0].Text, Is.EqualTo("line 1"));
        }

        [Test]
        public void TestConflictOperations_NoConflicts()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.NewGenFile = new TextFile("line 0\r\nline 1\r\nline 2");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.IsLineInConflict(0), Is.False);
            Assert.That(output.GetConflictBefore(0), Is.Null);
            Assert.That(output.GetConflictAfter(0), Is.Null);
        }

        [Test]
        public void TestToString()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.NewGenFile = new TextFile("line 0\r\nline 1\r\nline 2");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.ToString().Contains("Left Lines"));
            Assert.That(output.ToString().Contains("Right Lines"));
            Assert.That(output.ToString().IndexOf("line 0"),
                        Is.Not.EqualTo(output.ToString().LastIndexOf("line 0")));
        }

        [Test]
        public void TestDeleteLineWithConflicts()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.NewGenFile  = new TextFile("line 0\r\nline 12\r\nline 2");
            tfi.UserFile    = new TextFile("line 0\r\nline 13\r\nline 2");
            tfi.PrevGenFile = new TextFile("line 0\r\nline 1\r\nline 2");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Has.Count(4));
            Assert.That(output.LeftLines, Has.Count(4));

            output.RemoveLine(0);
            Assert.That(output.RightLines, Has.Count(3));
            Assert.That(output.LeftLines, Has.Count(3));
            
            Assert.That(output.LeftLines[0].Text, Is.EqualTo("line 13"));
            Assert.That(output.RightLines[0].Text, Is.EqualTo(""));
            Assert.That(output.RightLines[0].IsVirtual, Is.EqualTo(true));

            Assert.That(output.ConflictRanges[0].StartLineIndex, Is.EqualTo(0));
            Assert.That(output.ConflictRanges[0].EndLineIndex, Is.EqualTo(2));

        }

        [Test]
        public void TestGetConflictBefore_And_After()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.NewGenFile = new TextFile("line 0\r\nline 10\r\nline 2\r\nline32\r\nline 4\r\nline54\r\nline6");
            tfi.UserFile = new TextFile("line 0\r\nline 12\r\nline 2\r\nline35\r\nline 4\r\nline56\r\nline6");
            tfi.PrevGenFile = new TextFile("line 0\r\nline 1\r\nline 2\r\nline3\r\nline 4\r\nline5\r\nline6");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Has.Count(10));
            Assert.That(output.LeftLines, Has.Count(10));

            VisualDiffOutput.ConflictRange conflict;
            Assert.That(output.IsLineInConflict(1), Is.True);
            Assert.That(output.IsLineInConflict(1, out conflict), Is.True);

            Assert.That(conflict, Is.Not.Null);
            Assert.That(conflict.StartLineIndex, Is.EqualTo(1));
            Assert.That(conflict.EndLineIndex, Is.EqualTo(3));
            Assert.That(output.GetConflictAfter(1).StartLineIndex, Is.EqualTo(4));
            Assert.That(output.GetConflictBefore(1).StartLineIndex, Is.EqualTo(7));

            Assert.That(output.GetConflictAfter(4).StartLineIndex, Is.EqualTo(7));
            Assert.That(output.GetConflictBefore(4).StartLineIndex, Is.EqualTo(1));
        }
    }
}
