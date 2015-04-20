using Algorithm.Diff;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace ArchAngel.Workbench.Tests.IntelliMerge
{
    [TestFixture]
    public class TestDiff
    {
        [Test]
        public void TestStripWhiteSpace_SingleLine()
        {
            string testString = "blah blah\r\nblah";
            string[] output = Diff.StripWhitespace(new string[]{testString}, true);

            Assert.That(output, Is.Not.Empty);
            Assert.That(output.Length, Is.EqualTo(1));
            Assert.That(output[0], Is.EqualTo("blahblahblah"));
        }

        [Test]
        public void TestStripWhiteSpace_SingleLine_Tabs()
        {
            string testString = "blah\t\tblah\r\nblah";
            string[] output = Diff.StripWhitespace(new string[] { testString }, true);

            Assert.That(output, Is.Not.Empty);
            Assert.That(output.Length, Is.EqualTo(1));
            Assert.That(output[0], Is.EqualTo("blahblahblah"));
        }

        [Test]
        public void TestStripWhiteSpace_Multiline_NoBlanks()
        {
            string testString = "blah blah\r\nblah";
            string[] output = Diff.StripWhitespace(new string[] { testString, testString }, true);

            Assert.That(output, Is.Not.Empty);
            Assert.That(output.Length, Is.EqualTo(2));
            Assert.That(output[0], Is.EqualTo("blahblahblah"));
            Assert.That(output[1], Is.EqualTo("blahblahblah"));
        }

        [Test]
        public void TestStripWhiteSpace_Multiline_BlankLine()
        {
            string testString = "blah blah\r\nblah";
            string[] output = Diff.StripWhitespace(new string[] { testString, "", testString }, true);

            Assert.That(output, Is.Not.Empty);
            Assert.That(output.Length, Is.EqualTo(2));
            Assert.That(output[0], Is.EqualTo("blahblahblah"));
            Assert.That(output[1], Is.EqualTo("blahblahblah"));
        }
    }
}
