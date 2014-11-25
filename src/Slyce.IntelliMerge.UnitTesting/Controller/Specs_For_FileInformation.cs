using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;
using Slyce.IntelliMerge;
using Slyce.IntelliMerge.Controller;

namespace Specs_For_TextFileInformation
{
    public class DiffTestBaseClass
    {
        protected const string EmptyClass = "public class SomeClass { }";
        protected TextFileInformation tfi;

        public void SetupAndPerformDiff(string user, string prevGen, string newGen)
        {
            tfi = new TextFileInformation();
            tfi.UserFile = string.IsNullOrEmpty(user) ? TextFile.Blank : new TextFile(user);
            tfi.NewGenFile = string.IsNullOrEmpty(newGen) ? TextFile.Blank : new TextFile(newGen);
            tfi.PrevGenFile = string.IsNullOrEmpty(prevGen) ? TextFile.Blank : new TextFile(prevGen);
            tfi.PerformDiff();
        }

        protected void SetupAndPerformDiffWithMD5s(string user, string prevGen, string newGen)
        {
            tfi = new TextFileInformation();
            tfi.UserFile = string.IsNullOrEmpty(user) ? TextFile.Blank : new TextFile(user);
            tfi.NewGenFile = string.IsNullOrEmpty(newGen) ? TextFile.Blank : new TextFile(newGen);
            tfi.PrevGenFile = string.IsNullOrEmpty(prevGen) ? TextFile.Blank : new TextFile(prevGen);
            tfi.SetPreviousVersionMD5s(Utility.GetCheckSumOfString(prevGen), Utility.GetCheckSumOfString(newGen),
                                       Utility.GetCheckSumOfString(user));
            tfi.RelativeFilePath = "Class.cs";
            tfi.PerformDiff();
        }
    }

    [TestFixture]
    public class Exceptions
    {
        [Test(Description = "Ensures that a newly constructed TextFileInformation object cannot perform a diff because it has no files."),
         ExpectedException(typeof(InvalidOperationException))]
        public void TestInvalidDiff_NoFiles()
        {
            TextFileInformation tfi = new TextFileInformation();
            tfi.PerformDiff();
        }

        [Test(Description = "Ensures that a newly constructed TextFileInformation object cannot perform a super diff because it has no files."),
         ExpectedException(typeof(InvalidOperationException))]
        public void TestInvalidSuperDiff_NoFiles()
        {
            TextFileInformation tfi = new TextFileInformation();
            tfi.PerformSuperDiff();
        }
    }

    [TestFixture]
	[Ignore]
    public class When_All_Files_Exist : DiffTestBaseClass
    {
        [Test(Description = "Tests the case when all files are identical")]
        public void All_Files_Identical()
        {
            SetupAndPerformDiff(EmptyClass, EmptyClass, EmptyClass);

            Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));

            SetupAndPerformDiffWithMD5s(EmptyClass, EmptyClass, EmptyClass);
            Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));
        }

        [Test(Description = "Tests the case when the user's version of the file has changed.")]
        public void User_Change()
        {
            SetupAndPerformDiff("public class SomeClass { public int i; }", EmptyClass, EmptyClass);

            Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.UserChangeOnly));

			//SetupAndPerformDiffWithMD5s("public class SomeClass { public int i; }", EmptyClass, EmptyClass);
			//Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));
        }

        [Test(Description = "Tests the case when the NewGen file is different.")]
        public void Template_Change()
        {
            SetupAndPerformDiff(EmptyClass, EmptyClass, "public class SomeClass { public int i; }");

            Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.TemplateChangeOnly));

			//SetupAndPerformDiffWithMD5s(EmptyClass, EmptyClass, "public class SomeClass { public int i; }");
			//Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));
        }

        [Test(Description = "Test the case when the NewGen and User files are identical, but the PrevGen is different.")]
        public void PrevGen_Change()
        {
            SetupAndPerformDiff(EmptyClass, "public class SomeClass { public int i; }", EmptyClass);

            Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.UserAndTemplateChange));

			//SetupAndPerformDiffWithMD5s(EmptyClass, "public class SomeClass { public int i; }", EmptyClass);
			//Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));
        }

        [Test(Description = "Test the case when all of the files have changed.")]
        public void All_Changed()
        {
            SetupAndPerformDiff(EmptyClass, "public class SomeClass { public int i; }", "public class SomeClass { public int j; }");

            Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.UserAndTemplateChange));

            SetupAndPerformDiff("public class SomeClass { public int k; }", "public class SomeClass { public int i; }", "public class SomeClass { public int j; }");

            Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.UserAndTemplateChange));

			//SetupAndPerformDiffWithMD5s(EmptyClass, "public class SomeClass { public int i; }", "public class SomeClass { public int j; }");
			//Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));
        }
    }
}

namespace Specs_For_TextFileInformation
{
    namespace When_One_File_Is_Missing
    {
        [TestFixture]
		[Ignore]
        public class All_Files_Identical : DiffTestBaseClass
        {
            [Test(Description = "Tests the case when all files are identical but the user file is missing. If the prevgen file exists, then the user has deleted the file.")]
            public void User_File_Missing()
            {
                SetupAndPerformDiff(null, EmptyClass, EmptyClass);

                Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.Warning));
            }

            [Test(Description = "Tests the case when all files are identical but the newgen file is missing"),
            ExpectedException(typeof(Exception))]
            public void NewGen_File_Missing()
            {
                SetupAndPerformDiff(EmptyClass, EmptyClass, null);
            }

            [Test(Description = "Tests the case when all files are identical but the prevgen file is missing")]
            public void PrevGen_File_Missing()
            {
                SetupAndPerformDiff(EmptyClass, null, EmptyClass);

                Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));
            }
        }

        [TestFixture]
		[Ignore]
        public class Two_Files_Different : DiffTestBaseClass
        {
            [Test(Description = "Tests the case when the user file is missing and the other files are different")]
            public void User_File_Missing()
            {
                SetupAndPerformDiff(null, EmptyClass, "public class SomeClass { public int i; }");

                Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.Warning));
            }

            [Test(Description = "Tests the case when the newgen file is missing and the other files are different"),
            ExpectedException(typeof(Exception))]
            public void NewGen_File_Missing()
            {
                SetupAndPerformDiff("public class SomeClass { public int i; }", EmptyClass, null);
            }

            [Test(Description = "Tests the case when the prevgen file is missing and the other files are different")]
            public void PrevGen_File_Missing()
            {
                SetupAndPerformDiff("public class SomeClass { public int i; }", null, EmptyClass);

                Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.UserChangeOnly));
            }
        }
    }

    namespace When_Two_Files_Are_Missing
    {
        [TestFixture]
		[Ignore]
        public class One_Remaining_File : DiffTestBaseClass
        {
            [Test(Description = "Tests the case when the only file available is the users file."),
            ExpectedException(typeof(Exception))]
            public void User_File_Remains()
            {
                SetupAndPerformDiff(EmptyClass, null, null);

                Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));
            }

            [Test(Description = "Tests the case when the only file available is the newgen file.")]
            public void NewGen_File_Remains()
            {
                SetupAndPerformDiff(null, null, EmptyClass);

                Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));
            }

            [Test(Description = "Tests the case when the only file available is the prevgen file."),
            ExpectedException(typeof(Exception))]
            public void PrevGen_File_Remains()
            {
                SetupAndPerformDiff(null, EmptyClass, null);

                Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));
            }
        }
    }
}
