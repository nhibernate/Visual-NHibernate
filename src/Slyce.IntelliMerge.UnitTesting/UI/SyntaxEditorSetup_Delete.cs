using System;
using Algorithm.Diff;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge.UI.VisualDiff;

namespace Specs_for_IntelliMerge_UI
{
    [TestFixture]
    public class SyntaxEditorSetup_Delete
    {
        [Test]
        public void TestTemplateChange_Delete_At_Start()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = tfi.UserFile = new TextFile("line0\nline1");
            tfi.NewGenFile = new TextFile("line1");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Has.Count(2));
            Assert.That(output.RightLines, Has.Count(2));

            Assert.That(output.LeftLines[0].Text, Is.EqualTo("line0"));
            Assert.That(output.LeftLines[1].Text, Is.EqualTo("line1"));
            Assert.That(output.RightLines[0].Text, Is.EqualTo(""));
            Assert.That(output.RightLines[1].Text, Is.EqualTo("line1"));

            Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.None));
        }

        [Test]
        public void TestTemplateChange_Delete_In_Middle()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.NewGenFile  = new TextFile("line1\nline3");
            tfi.PrevGenFile = tfi.UserFile = new TextFile("line1\nline2\nline3");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Has.Count(3));
            Assert.That(output.RightLines, Has.Count(3));

            Assert.That(output.LeftLines[0].Text, Is.EqualTo("line1"), "Line 1 on right");
            Assert.That(output.LeftLines[1].Text, Is.EqualTo("line2"), "Line 2 on right");
            Assert.That(output.LeftLines[2].Text, Is.EqualTo("line3"), "Line 3 on right");
            Assert.That(output.RightLines[0].Text, Is.EqualTo("line1"), "Line 1 on left");
            Assert.That(output.RightLines[1].Text, Is.EqualTo(""), "Line 2 on left");
            Assert.That(output.RightLines[2].Text, Is.EqualTo("line3"), "Line 3 on left");

            Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.LeftLines[2].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.RightLines[2].Change, Is.EqualTo(ChangeType.None));
        }

        [Test]
        public void TestTemplateChange_Delete_At_End()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = tfi.UserFile = new TextFile("line1\nline2");
            tfi.NewGenFile = new TextFile("line1");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Has.Count(2));
            Assert.That(output.RightLines, Has.Count(2));

            Assert.That(output.LeftLines[0].Text, Is.EqualTo("line1"));
            Assert.That(output.LeftLines[1].Text, Is.EqualTo("line2"));
            Assert.That(output.RightLines[0].Text, Is.EqualTo("line1"));
            Assert.That(output.RightLines[1].Text, Is.EqualTo(""));

            Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.Template));
        	Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.None));
			Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.Template)); Assert.That(output.RightLines[1].IsVirtual, Is.True);
        }

        [Test]
        public void TestUserChange_Delete_At_Start()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = tfi.NewGenFile = new TextFile("line0\nline1");
            tfi.UserFile = new TextFile("line1");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Has.Count(2));
            Assert.That(output.RightLines, Has.Count(2));

            Assert.That(output.LeftLines[0].Text, Is.EqualTo("line0"));
            Assert.That(output.LeftLines[1].Text, Is.EqualTo("line1"));
            Assert.That(output.RightLines[0].Text, Is.EqualTo(""));
            Assert.That(output.RightLines[1].Text, Is.EqualTo("line1"));

            Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.RightLines[0].IsVirtual, Is.True);
            Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.None));
        }

        [Test]
        public void TestUserChange_Delete_In_Middle()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = tfi.NewGenFile = new TextFile("line1\nline2\nline3");
            tfi.UserFile = new TextFile("line1\nline3");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Has.Count(3));
            Assert.That(output.RightLines, Has.Count(3));

            Assert.That(output.LeftLines[0].Text, Is.EqualTo("line1"), "Line 1 on left");
            Assert.That(output.LeftLines[1].Text, Is.EqualTo("line2"), "Line 2 on left");
            Assert.That(output.LeftLines[2].Text, Is.EqualTo("line3"), "Line 3 on left");
            Assert.That(output.RightLines[0].Text, Is.EqualTo("line1"), "Line 1 on right");
            Assert.That(output.RightLines[1].Text, Is.EqualTo(""), "Line 2 on right");
            Assert.That(output.RightLines[2].Text, Is.EqualTo("line3"), "Line 3 on right");

            Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.LeftLines[2].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.None));
			Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.User)); Assert.That(output.RightLines[1].IsVirtual, Is.True);
            Assert.That(output.RightLines[2].Change, Is.EqualTo(ChangeType.None));
        }

        [Test]
        public void TestUserChange_Delete_At_End()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = tfi.NewGenFile = new TextFile("line1\nline2");
            tfi.UserFile = new TextFile("line1");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Has.Count(2));
            Assert.That(output.RightLines, Has.Count(2));

            Assert.That(output.LeftLines[0].Text, Is.EqualTo("line1"));
            Assert.That(output.LeftLines[1].Text, Is.EqualTo("line2"));
            Assert.That(output.RightLines[0].Text, Is.EqualTo("line1"));
            Assert.That(output.RightLines[1].Text, Is.EqualTo(""));

            Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.None));
			Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.User)); Assert.That(output.RightLines[1].IsVirtual, Is.True);            
        }

        [Test]
        public void TestUserAndTemplateChange_Delete_At_Start()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = new TextFile("line1");
            tfi.NewGenFile = new TextFile("line0\nline1");
            tfi.UserFile = new TextFile("line1\nline2");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Has.Count(3));
            Assert.That(output.RightLines, Has.Count(3));

            Assert.That(output.LeftLines[0].Text, Is.EqualTo(""));
            Assert.That(output.LeftLines[1].Text, Is.EqualTo("line1"));
            Assert.That(output.LeftLines[2].Text, Is.EqualTo(""));
            Assert.That(output.RightLines[0].Text, Is.EqualTo("line0"));
            Assert.That(output.RightLines[1].Text, Is.EqualTo("line1"));
            Assert.That(output.RightLines[2].Text, Is.EqualTo("line2"));

            Assert.That(output.LeftLines[0].IsVirtual, Is.True);
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[2].IsVirtual, Is.True);
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[2].Change, Is.EqualTo(ChangeType.User));
        }

        [Test]
        public void TestUserAndTemplateChange_Delete_In_Middle()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = new TextFile("line1\nline2\nline3");
            tfi.UserFile = new TextFile("line1\nline3");
            tfi.NewGenFile = new TextFile("line0\nline1\nline2\nline3");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Has.Count(4));
            Assert.That(output.RightLines, Has.Count(4));

            Assert.That(output.LeftLines[0].Text, Is.EqualTo(""), "Line 1 on left");
            Assert.That(output.LeftLines[1].Text, Is.EqualTo("line1"), "Line 2 on left");
            Assert.That(output.LeftLines[2].Text, Is.EqualTo("line2"), "Line 3 on left");
            Assert.That(output.LeftLines[3].Text, Is.EqualTo("line3"), "Line 4 on left");

            Assert.That(output.RightLines[0].Text, Is.EqualTo("line0"), "Line 1 on right");
            Assert.That(output.RightLines[1].Text, Is.EqualTo("line1"), "Line 2 on right");
            Assert.That(output.RightLines[2].Text, Is.EqualTo(""), "Line 3 on right");
            Assert.That(output.RightLines[3].Text, Is.EqualTo("line3"), "Line 4 on right");

            Assert.That(output.LeftLines[0].IsVirtual, Is.True);
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[2].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.LeftLines[3].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.None));
			Assert.That(output.RightLines[2].Change, Is.EqualTo(ChangeType.User)); Assert.That(output.RightLines[2].IsVirtual, Is.True);
            Assert.That(output.RightLines[3].Change, Is.EqualTo(ChangeType.None));
        }

        [Test]
        public void TestConflict_At_Start_Delete_At_End()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = new TextFile(       "line1\nline3");
            tfi.UserFile    = new TextFile("line00\nline1");
            tfi.NewGenFile = new TextFile("line0\nline1");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Has.Count(4));
            Assert.That(output.RightLines, Has.Count(4));

            Assert.That(output.LeftLines[0].Text, Is.EqualTo("line00"), "Line 1 on left");
            Assert.That(output.LeftLines[1].Text, Is.EqualTo("line0"), "Line 2 on left");
            Assert.That(output.LeftLines[2].Text, Is.EqualTo("line1"), "Line 3 on left");
            Assert.That(output.LeftLines[3].Text, Is.EqualTo("line3"), "Line 4 on left");
            Assert.That(output.RightLines[0].Text, Is.EqualTo(""), "Line 1 on right");
            Assert.That(output.RightLines[1].Text, Is.EqualTo(""), "Line 2 on right");
            Assert.That(output.RightLines[2].Text, Is.EqualTo("line1"), "Line 3 on right");
            Assert.That(output.RightLines[3].Text, Is.EqualTo(""), "Line 4 on right");

            Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.LeftLines[2].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[3].Change, Is.EqualTo(ChangeType.UserAndTemplate));

            Assert.That(output.RightLines[0].IsVirtual, Is.True);
            Assert.That(output.RightLines[1].IsVirtual, Is.True);
            Assert.That(output.RightLines[2].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[3].Change, Is.EqualTo(ChangeType.User | ChangeType.Template));
        }
    }
}