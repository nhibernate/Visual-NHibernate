using Algorithm.Diff;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge.UI.VisualDiff;

namespace Specs_for_IntelliMerge_UI
{
    [TestFixture]
    public class SyntaxEditorSetup_Add
    {
        [Test]
        public void TestTemplateChange_Add_At_Start()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = tfi.UserFile = new TextFile("line 1");
            tfi.NewGenFile = new TextFile("line 0\nline 1");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Has.Count(2));
            Assert.That(output.RightLines, Has.Count(2));

            Assert.That(output.LeftLines[0].Text, Is.EqualTo(""));
            Assert.That(output.LeftLines[1].Text, Is.EqualTo("line 1"));
            Assert.That(output.RightLines[0].Text, Is.EqualTo("line 0"));
            Assert.That(output.RightLines[1].Text, Is.EqualTo("line 1"));

            Assert.That(output.LeftLines[0].IsVirtual, Is.True); Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.None));
        }

        [Test]
        public void TestTemplateChange_Add_In_Middle()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = tfi.UserFile = new TextFile("line 1\nline 3");
            tfi.NewGenFile = new TextFile("line 1\nline 2\nline 3");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Has.Count(3));
            Assert.That(output.RightLines, Has.Count(3));

            Assert.That(output.LeftLines[0].Text, Is.EqualTo("line 1"), "Line 1 on left");
            Assert.That(output.LeftLines[1].Text, Is.EqualTo(""), "Line 2 on left");
            Assert.That(output.LeftLines[2].Text, Is.EqualTo("line 3"), "Line 3 on left");
            Assert.That(output.RightLines[0].Text, Is.EqualTo("line 1"), "Line 1 on right");
            Assert.That(output.RightLines[1].Text, Is.EqualTo("line 2"), "Line 2 on right");
            Assert.That(output.RightLines[2].Text, Is.EqualTo("line 3"), "Line 3 on right");

            Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[1].IsVirtual, Is.True); Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.LeftLines[2].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.RightLines[2].Change, Is.EqualTo(ChangeType.None));
        }

        [Test]
        public void TestTemplateChange_Add_At_End()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = tfi.UserFile = new TextFile("line1");
            tfi.NewGenFile = new TextFile("line1\nline2");
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
            Assert.That(output.LeftLines[1].Text, Is.EqualTo(""));
            Assert.That(output.RightLines[0].Text, Is.EqualTo("line1"));
            Assert.That(output.RightLines[1].Text, Is.EqualTo("line2"));

            Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[1].IsVirtual, Is.True); Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.Template));
        }

        [Test]
        public void TestTemplateChange_Change_At_End_No_User_Change()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = tfi.UserFile = new TextFile("line1\nline2");
            tfi.NewGenFile = new TextFile("line1\nline23");
            tfi.UserFile = new TextFile("line1\nline2");
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
            Assert.That(output.RightLines[1].Text, Is.EqualTo("line23"));

            Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.Template));
        }

        [Test]
        public void TestUserChange_Add_At_Start()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = tfi.NewGenFile = new TextFile("line1");
            tfi.UserFile = new TextFile("line0\nline1");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Has.Count(2));
            Assert.That(output.RightLines, Has.Count(2));

            Assert.That(output.LeftLines[0].Text, Is.EqualTo(""));
            Assert.That(output.LeftLines[1].Text, Is.EqualTo("line1"));
            Assert.That(output.RightLines[0].Text, Is.EqualTo("line0"));
            Assert.That(output.RightLines[1].Text, Is.EqualTo("line1"));

            Assert.That(output.LeftLines[0].IsVirtual, Is.True); Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.None));
        }

        [Test]
        public void TestUserChange_Add_In_Middle()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = tfi.NewGenFile = new TextFile("line1\nline3");
            tfi.UserFile = new TextFile("line1\nline2\nline3");
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
            Assert.That(output.LeftLines[1].Text, Is.EqualTo(""), "Line 2 on left");
            Assert.That(output.LeftLines[2].Text, Is.EqualTo("line3"), "Line 3 on left");
            Assert.That(output.RightLines[0].Text, Is.EqualTo("line1"), "Line 1 on right");
            Assert.That(output.RightLines[1].Text, Is.EqualTo("line2"), "Line 2 on right");
            Assert.That(output.RightLines[2].Text, Is.EqualTo("line3"), "Line 3 on right");

            Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[1].IsVirtual, Is.True); Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.LeftLines[2].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.RightLines[2].Change, Is.EqualTo(ChangeType.None));
        }

        [Test]
        public void TestUserChange_Add_At_End()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = tfi.NewGenFile = new TextFile("line1");
            tfi.UserFile = new TextFile("line1\nline2");
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
            Assert.That(output.LeftLines[1].Text, Is.EqualTo(""));
            Assert.That(output.RightLines[0].Text, Is.EqualTo("line1"));
            Assert.That(output.RightLines[1].Text, Is.EqualTo("line2"));

            Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[1].IsVirtual, Is.True); Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.User));
        }

        [Test]
        public void TestUserAndTemplateChange_Add_At_Start()
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

            Assert.That(output.LeftLines[0].IsVirtual, Is.True); Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[2].IsVirtual, Is.True); Assert.That(output.LeftLines[2].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[2].Change, Is.EqualTo(ChangeType.User));
        }

        [Test]
        public void TestUserAndTemplateChange_Add_In_Middle()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = new TextFile("line1\nline3");
            tfi.UserFile = new TextFile("line1\nline2\nline3");
            tfi.NewGenFile = new TextFile("line0\nline1\nline3");
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
            Assert.That(output.LeftLines[2].Text, Is.EqualTo(""), "Line 3 on left");
            Assert.That(output.LeftLines[3].Text, Is.EqualTo("line3"), "Line 4 on left");
            Assert.That(output.RightLines[0].Text, Is.EqualTo("line0"), "Line 1 on right");
            Assert.That(output.RightLines[1].Text, Is.EqualTo("line1"), "Line 2 on right");
            Assert.That(output.RightLines[2].Text, Is.EqualTo("line2"), "Line 3 on right");
            Assert.That(output.RightLines[3].Text, Is.EqualTo("line3"), "Line 4 on right");

            Assert.That(output.LeftLines[0].IsVirtual, Is.True); Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[2].IsVirtual, Is.True); Assert.That(output.LeftLines[2].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.LeftLines[3].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[2].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.RightLines[3].Change, Is.EqualTo(ChangeType.None));
        }

        [Test]
        public void TestConflict_At_Start()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = new TextFile("line1");
            tfi.NewGenFile = new TextFile("line0\nline01\nline1");
            tfi.UserFile = new TextFile("line00\nline1");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Has.Count(4));
            Assert.That(output.RightLines, Has.Count(4));

            Assert.That(output.LeftLines[0].Text, Is.EqualTo("line00"));
            Assert.That(output.LeftLines[1].Text, Is.EqualTo("line0"));
            Assert.That(output.LeftLines[2].Text, Is.EqualTo("line01"));
            Assert.That(output.LeftLines[3].Text, Is.EqualTo("line1"));
            Assert.That(output.RightLines[0].Text, Is.EqualTo(""));
            Assert.That(output.RightLines[1].Text, Is.EqualTo(""));
            Assert.That(output.RightLines[2].Text, Is.EqualTo(""));
            Assert.That(output.RightLines[3].Text, Is.EqualTo("line1"));

            Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.LeftLines[2].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.LeftLines[3].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[0].IsVirtual, Is.True);
            Assert.That(output.RightLines[1].IsVirtual, Is.True);
            Assert.That(output.RightLines[2].IsVirtual, Is.True);
            Assert.That(output.RightLines[3].Change, Is.EqualTo(ChangeType.None));
        }

        [Test]
        public void TestConflict_At_Start_Add_At_End()
        {
            TextFileInformation tfi = new TextFileInformation();

            tfi.PrevGenFile = new TextFile(       "line1\nline3");
            tfi.UserFile    = new TextFile("line00\nline1\nline2\nline3");
            tfi.NewGenFile = new TextFile("line0\nline1\nline3\nline4");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Has.Count(6));
            Assert.That(output.RightLines, Has.Count(6));

            Assert.That(output.LeftLines[0].Text, Is.EqualTo("line00"), "Line 1 on left");
            Assert.That(output.LeftLines[1].Text, Is.EqualTo("line0"), "Line 2 on left");
            Assert.That(output.LeftLines[2].Text, Is.EqualTo("line1"), "Line 3 on left");
            Assert.That(output.LeftLines[3].Text, Is.EqualTo(""), "Line 4 on left");
            Assert.That(output.LeftLines[4].Text, Is.EqualTo("line3"), "Line 5 on left");
            Assert.That(output.LeftLines[5].Text, Is.EqualTo(""), "Line 5 on left");
            Assert.That(output.RightLines[0].Text, Is.EqualTo(""), "Line 1 on right");
            Assert.That(output.RightLines[1].Text, Is.EqualTo(""), "Line 2 on right");
            Assert.That(output.RightLines[2].Text, Is.EqualTo("line1"), "Line 3 on right");
            Assert.That(output.RightLines[3].Text, Is.EqualTo("line2"), "Line 4 on right");
            Assert.That(output.RightLines[4].Text, Is.EqualTo("line3"), "Line 5 on right");
            Assert.That(output.RightLines[5].Text, Is.EqualTo("line4"), "Line 6 on right");

            Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.Template));
            Assert.That(output.LeftLines[2].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[3].IsVirtual, Is.True);
            Assert.That(output.LeftLines[4].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.LeftLines[5].IsVirtual, Is.True);
            Assert.That(output.RightLines[0].IsVirtual, Is.True);
            Assert.That(output.RightLines[1].IsVirtual, Is.True);
            Assert.That(output.RightLines[2].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[3].Change, Is.EqualTo(ChangeType.User));
            Assert.That(output.RightLines[4].Change, Is.EqualTo(ChangeType.None));
            Assert.That(output.RightLines[5].Change, Is.EqualTo(ChangeType.Template));
        }
    }
}