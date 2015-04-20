using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge;

namespace Specs_For_Binary_FileInformation
{
    public class TestBase
    {
        protected BinaryFileInformation bfi;
        private const string filePath = "BinaryFileTest.bin";

        public void SetupAndPerformDiff(byte[] user, byte[] prevGen, byte[] newGen)
        {
            bfi = new BinaryFileInformation();
            bfi.RelativeFilePath = filePath;
            bfi.UserFile = user==null ? BinaryFile.Blank : new BinaryFile(user);
            bfi.NewGenFile = newGen==null ? BinaryFile.Blank : new BinaryFile(newGen);
            bfi.PrevGenFile = prevGen==null ? BinaryFile.Blank : new BinaryFile(prevGen);
            bfi.IntelliMerge = IntelliMergeType.NotSet;
            Assert.That(bfi.PerformDiff(),Is.EqualTo(true),"Diff not performed correctly");
        }
    }

    [TestFixture]
    public class One_Missing_File_Tests : TestBase
    {

        //if prevgen == newgen, user has renamed or deleted the file.
        //Otherwise:
        //If renamed - Allow user to make the match
        //If deleted, generate it from newgen.
        [Test]
        public void User_Empty_Others_Same()
        {
            SetupAndPerformDiff(null, new byte[]{1,2,3}, new byte[]{1,2,3});
            Assert.That(bfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));//???
        }


        [Test]
        public void User_Empty_Others_Differ()
        {
            //User has deleted the file, but the template has made changes we want to keep
            SetupAndPerformDiff(null, new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 4 });
            Assert.That(bfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.TemplateChangeOnly));//???
        }


        [Test]
        public void PrevGen_Empty_Others_Same()
        {
            SetupAndPerformDiff(new byte[] { 1, 2, 3 }, null, new byte[] { 1, 2, 3 });
            Assert.That(bfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.UserAndTemplateChange));//???
        }

        [Test]
        public void PrevGen_Empty_Others_Differ()
        {
            SetupAndPerformDiff(new byte[] { 1, 2, 3 }, null, new byte[] { 1, 2, 3, 4 });
            Assert.That(bfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.Conflict));//???
        }

        [Test]
        public void NewGen_Empty_Others_Same()
        {
            SetupAndPerformDiff(new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 }, null);
            Assert.That(bfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));
        }

        [Test]
        public void NewGen_Empty_Others_Differ()
        {
            //Consider that the template has deleted the file, but the user has made changes we want to keep
            SetupAndPerformDiff(new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 4 }, null);
            Assert.That(bfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.UserChangeOnly));
        }

    }

    [TestFixture]
    public class Multiple_Files_Missing_Tests : TestBase
    {
        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void All_Empty()
        {
            //Cannot do a diff on no files
            SetupAndPerformDiff(null, null, null);
        }

        [Test]
        public void User_And_PrevGen_Empty()
        {
            SetupAndPerformDiff(null, null, new byte[]{1,2,3});
            Assert.That(bfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));
        }

        [Test]
        public void User_And_NewGen_Empty()
        {
            SetupAndPerformDiff(null, new byte[] { 1, 2, 3 }, null);
            Assert.That(bfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));
        }

        [Test]
        public void NewGen_And_PrevGen_Empty()
        {
            SetupAndPerformDiff(new byte[] { 1, 2, 3 }, null, null);
            Assert.That(bfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));
        }
    }

    [TestFixture]
    public class One_File_Changes_Tests : TestBase
    {

        [Test]
        public void User_Change()
        {
            SetupAndPerformDiff(new byte[] { 1, 2, 3, 4}, new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 });
            Assert.That(bfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.UserChangeOnly));
        }
        
        [Test]
        public void NewGen_Change()
        {
            SetupAndPerformDiff(new byte[] { 1, 2, 3}, new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3, 4 });
            Assert.That(bfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.TemplateChangeOnly));
        }

    }

    [TestFixture]
    public class Multiple_Files_Change_Tests : TestBase
    {

        [Test]
        public void User_And_NewGen_Change_Different()
        {
            SetupAndPerformDiff(new byte[] { 1, 2, 3, 4}, new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3, 5 });
            Assert.That(bfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.Conflict));
        }

        [Test]
        public void User_And_NewGen_Change_Same()
        {
            SetupAndPerformDiff(new byte[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3, 4 });
            Assert.That(bfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.UserAndTemplateChange));
        }
    }

    [TestFixture]
    public class No_Files_Changed_Test : TestBase
    {

        [Test]
        public void All_Files_Same()
        {
            SetupAndPerformDiff(new byte[] { 1, 2, 3}, new byte[] { 1, 2, 3}, new byte[] { 1, 2, 3});
            Assert.That(bfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));
        }

    }
}
