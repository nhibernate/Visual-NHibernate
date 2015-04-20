using Algorithm.Diff;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge.UI.VisualDiff;

namespace Specs_for_IntelliMerge_UI
{
    [TestFixture]
    public class SyntaxEditorSetup_Change
    {
        [Test]
        public void UserAndTemplate_Two_Changes_In_Middle()
        {
            TextFileInformation tfi = new TextFileInformation();
			tfi.PrevGenFile = new TextFile("line0\nline1\nline2\nline3");
			tfi.UserFile = new TextFile("line0\nline11\nline12\nline2\nline3");
            tfi.NewGenFile = new TextFile("line0\nline1\nline21\nline3");
            tfi.IntelliMerge = IntelliMergeType.PlainText;
            tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

        	Assert.That(output.DiffType, Is.EqualTo(Slyce.IntelliMerge.TypeOfDiff.UserAndTemplateChange));
            Assert.IsNotNull(output);
            Assert.That(output.RightLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Is.Not.Empty);
            Assert.That(output.LeftLines, Has.Count(5));
            Assert.That(output.RightLines, Has.Count(5));

            Assert.That(output.LeftLines[0].Text, Is.EqualTo("line0"));
            Assert.That(output.LeftLines[1].Text, Is.EqualTo("line1"));
			Assert.That(output.LeftLines[2].Text, Is.EqualTo(""));
			Assert.That(output.LeftLines[3].Text, Is.EqualTo("line2"));
			Assert.That(output.LeftLines[4].Text, Is.EqualTo("line3"));

			Assert.That(output.RightLines[0].Text, Is.EqualTo("line0"));
			Assert.That(output.RightLines[1].Text, Is.EqualTo("line11"));
			Assert.That(output.RightLines[2].Text, Is.EqualTo("line12"));
			Assert.That(output.RightLines[3].Text, Is.EqualTo("line21"));
			Assert.That(output.RightLines[4].Text, Is.EqualTo("line3"));

			Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.None));
			Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.User));
			Assert.That(output.LeftLines[2].Change, Is.EqualTo(ChangeType.User)); Assert.That(output.LeftLines[2].IsVirtual, Is.EqualTo(true));
			Assert.That(output.LeftLines[3].Change, Is.EqualTo(ChangeType.Template));
			Assert.That(output.LeftLines[4].Change, Is.EqualTo(ChangeType.None));

			Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.None));
			Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.User));
			Assert.That(output.RightLines[2].Change, Is.EqualTo(ChangeType.User));
			Assert.That(output.RightLines[3].Change, Is.EqualTo(ChangeType.Template));
			Assert.That(output.RightLines[4].Change, Is.EqualTo(ChangeType.None));
        }

		[Test]
		public void UserAndTemplate_Change_In_Middle()
		{
			TextFileInformation tfi = new TextFileInformation();
			tfi.PrevGenFile = new TextFile("line0\nline1\nline2");
			tfi.UserFile = new TextFile("line0\njajlkjer\nline2");
			tfi.NewGenFile = new TextFile("line0\nkljadsf\nline2");
			tfi.IntelliMerge = IntelliMergeType.PlainText;
			tfi.RelativeFilePath = "text.txt";

			ThreeWayVisualDiff diffUtility = new ThreeWayVisualDiff(tfi);
			VisualDiffOutput output = diffUtility.ProcessMergeOutput();

			//        	Assert.That(output.DiffType, Is.EqualTo(Slyce.IntelliMerge.TypeOfDiff.UserAndTemplateChange));
			Assert.IsNotNull(output);
			Assert.That(output.RightLines, Is.Not.Empty);
			Assert.That(output.LeftLines, Is.Not.Empty);
			Assert.That(output.LeftLines, Has.Count(4));
			Assert.That(output.RightLines, Has.Count(4));

			Assert.That(output.LeftLines[0].Text, Is.EqualTo("line0"));
			Assert.That(output.LeftLines[1].Text, Is.EqualTo("jajlkjer"));
			Assert.That(output.LeftLines[2].Text, Is.EqualTo("kljadsf"));
			Assert.That(output.LeftLines[3].Text, Is.EqualTo("line2"));

			Assert.That(output.RightLines[0].Text, Is.EqualTo("line0"));
			Assert.That(output.RightLines[1].Text, Is.EqualTo(""));
			Assert.That(output.RightLines[2].Text, Is.EqualTo(""));
			Assert.That(output.RightLines[3].Text, Is.EqualTo("line2"));

			Assert.That(output.LeftLines[0].Change, Is.EqualTo(ChangeType.None));
			Assert.That(output.LeftLines[1].Change, Is.EqualTo(ChangeType.User));
			Assert.That(output.LeftLines[2].Change, Is.EqualTo(ChangeType.Template));
			Assert.That(output.LeftLines[3].Change, Is.EqualTo(ChangeType.None));

			Assert.That(output.RightLines[0].Change, Is.EqualTo(ChangeType.None));
			Assert.That(output.RightLines[1].Change, Is.EqualTo(ChangeType.User)); Assert.That(output.RightLines[1].IsVirtual, Is.EqualTo(true));
			Assert.That(output.RightLines[2].Change, Is.EqualTo(ChangeType.Template)); Assert.That(output.RightLines[2].IsVirtual, Is.EqualTo(true));
			Assert.That(output.RightLines[3].Change, Is.EqualTo(ChangeType.None));
		}
    }
}