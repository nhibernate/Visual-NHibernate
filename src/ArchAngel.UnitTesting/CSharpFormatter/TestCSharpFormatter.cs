using System;
using System.IO;
using Algorithm.Diff;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.CSharp;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace ArchAngel.Workbench.Tests.CSharpFormatter
{
    [TestFixture]
    public class TestCSharpFormatter
    {
        public void TestFile(string filename, bool stripText, CSharpFormatSettings settings)
        {
			string fileText = File.ReadAllText(filename);

			CSharpParser formatter = new CSharpParser();
        	formatter.FormatSettings.SetFrom(settings);
            formatter.ParseCode(fileText);

        	Assert.IsFalse(formatter.ErrorOccurred);

			string formattedText = formatter.CreatedCodeRoot.ToString();


        	formattedText =
        		Slyce.Common.Utility.StandardizeLineBreaks(formattedText, Slyce.Common.Utility.LineBreaks.Windows);

            string strippedFileText = Diff.StripWhitespace(new[] {fileText}, stripText)[0].Replace("\t", "    ");
            string strippedFormattedText = Diff.StripWhitespace(new[] { formattedText }, stripText)[0].Replace("\t", "    ");

            Assert.That(strippedFormattedText, Is.EqualTo(strippedFileText));
        }

        [Test]
        public void TestAssemblyInfoFile()
        {
            TestFile("CSharpFormatter\\Test Files\\Attributes.cs", true, new CSharpFormatSettings());
        }

        [Test]
        public void TestLineBreaksBetweenFunctions()
        {
            TestFile("CSharpFormatter\\Test Files\\LineBreaksBetweenFunctions.cs", true, new CSharpFormatSettings());
        }

        [Test]
		//[Ignore("Waiting on Actipro's next release.")]
        public void TestCommentOrdering()
        {
        	CSharpFormatSettings settings = new CSharpFormatSettings();
			settings.ReorderBaseConstructs = true;
            TestFile("CSharpFormatter\\Test Files\\CommentOrdering.cs", true, settings);
        }

        [Test]
		[Ignore("This test has multiple issues in it, and should only be used as a smoke test.")]
        public void TestComments()
        {
            TestFile("CSharpFormatter\\Test Files\\Comments.cs", true, new CSharpFormatSettings());
        }

		//[Test]
		//public void TestOneLineIfStatements()
		//{
		//    CSharpFormatSettings settings = new CSharpFormatSettings();
		//    settings.AddBracesToSingleLineBlocks = false;
		//    TestFile("CSharpFormatter\\Test Files\\OneLineIfStatements.cs", false, settings);
		//}

        [Test]
		[Ignore("This test has multiple issues in it, and should only be used as a smoke test.")]
        public void Test_Complex()
        {
            TestFile("CSharpFormatter\\Test Files\\Complex.cs", false, new CSharpFormatSettings());
        }
    }
}
