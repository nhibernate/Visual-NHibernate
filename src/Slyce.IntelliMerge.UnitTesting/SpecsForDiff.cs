using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Algorithm.Diff;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_Diff
{
    /// <summary>
    /// Tests to determine how the Diff algorithm splits the lines up into hunks.
    /// </summary>
    [TestFixture]
    public class HunkNumbers
    {
        [Test]
        public void TestAddLines()
        {
            string[] leftLines = new string[] {"line1"};
            string[] rightLines = new string[] {"line1", "line2"};
            Diff diff = new Diff(leftLines, rightLines, true, true);
            Diff.Hunk[] hunks = Diff.GetHunkList(diff);

            Assert.That(hunks.Length, Is.EqualTo(2));
            Assert.That(hunks[0].Left, Has.Count(1));
            Assert.That(hunks[0].Right, Has.Count(1));
            Assert.That(hunks[1].Left, Has.Count(0));
            Assert.That(hunks[1].Right, Has.Count(1));
        }

        [Test]
        public void TestDeleteLines()
        {
            string[] leftLines = new string[] { "line1", "line2" };
            string[] rightLines = new string[] { "line1" };
            Diff diff = new Diff(leftLines, rightLines, true, true);
            Diff.Hunk[] hunks = Diff.GetHunkList(diff);

            Assert.That(hunks.Length, Is.EqualTo(2));
            Assert.That(hunks[0].Left, Has.Count(1));
            Assert.That(hunks[0].Right, Has.Count(1));
            Assert.That(hunks[1].Left, Has.Count(1));
            Assert.That(hunks[1].Right, Has.Count(0));
        }

        [Test]
        public void TestConflictLines()
        {
            string[] leftLines = new string[] { "line1", "line2" };
            string[] rightLines = new string[] { "line01", "line2" };
            Diff diff = new Diff(leftLines, rightLines, true, true);
            Diff.Hunk[] hunks = Diff.GetHunkList(diff);

            Assert.That(hunks.Length, Is.EqualTo(2));
            Assert.That(hunks[0].Left, Has.Count(1));
            Assert.That(hunks[0].Right, Has.Count(1));
            Assert.That(hunks[1].Left, Has.Count(1));
            Assert.That(hunks[1].Right, Has.Count(1));
        }

        [Test]
        public void TestConflictMultipleLines()
        {
            string[] leftLines = new string[] { "line1", "line2" };
            string[] rightLines = new string[] { "line01", "line02", "line2" };
            Diff diff = new Diff(leftLines, rightLines, true, true);
            Diff.Hunk[] hunks = Diff.GetHunkList(diff);

            Assert.That(hunks.Length, Is.EqualTo(2));
            Assert.That(hunks[0].Left, Has.Count(1));
            Assert.That(hunks[0].Right, Has.Count(2));
            Assert.That(hunks[1].Left, Has.Count(1));
            Assert.That(hunks[1].Right, Has.Count(1));
        }
    }
}
