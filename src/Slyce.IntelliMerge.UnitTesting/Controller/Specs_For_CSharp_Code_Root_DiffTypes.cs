using System.IO;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.CSharp;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.IntelliMerge;
using Slyce.IntelliMerge.Controller;

namespace Specs_For_CSharp_Code_Root.When_One_File_Is_Missing_Constructs
{
    [TestFixture]
    public class User_Missing_Constructor : TestBaseClass
    {
        [Test]
        public void The_Diff_Type_Is_User_Change()
        {
            AssertDiffType("Class1_WithConstructor.cs", "Class1.cs", "Class1_WithConstructor.cs", TypeOfDiff.UserChangeOnly);
        }
    }

    [TestFixture]
    public class Template_Missing_Constructor : TestBaseClass
    {
        [Test]
        public void The_Diff_Type_Is_Template_Change()
        {
            AssertDiffType("Class1_WithConstructor.cs", "Class1_WithConstructor.cs", "Class1.cs", TypeOfDiff.TemplateChangeOnly);
        }
    }

    [TestFixture]
    public class User_Missing_Method : TestBaseClass
    {
        [Test]
        public void The_Diff_Type_Is_User_Change()
        {
            AssertDiffType("Class1_WithTwoMethods.cs", "Class1.cs", "Class1_WithTwoMethods.cs", TypeOfDiff.UserChangeOnly);
        }
    }

    [TestFixture]
    public class Template_Missing_Method : TestBaseClass
    {
        [Test]
        public void The_Diff_Type_Is_Template_Change()
        {
            AssertDiffType("Class1_WithTwoMethods.cs", "Class1_WithTwoMethods.cs", "Class1.cs", TypeOfDiff.TemplateChangeOnly);
        }
    }
}

namespace Specs_For_CSharp_Code_Root.When_One_File_Adds_Constructs
{
    [TestFixture]
    public class Template_Adds_Constructor : TestBaseClass
    {
        [Test]
        public void The_Diff_Type_Is_User_Change()
        {
            AssertDiffType("Class1.cs", "Class1_WithConstructor.cs", "Class1.cs", TypeOfDiff.UserChangeOnly);
        }
    }

    [TestFixture]
    public class User_Adds_Constructor : TestBaseClass
    {
        [Test]
        public void The_Diff_Type_Is_Template_Change()
        {
            AssertDiffType("Class1.cs", "Class1_WithConstructor.cs", "Class1.cs", TypeOfDiff.UserChangeOnly);
        }
    }

    [TestFixture]
    public class User_Adds_Method : TestBaseClass
    {
        [Test]
        public void The_Diff_Type_Is_User_Change()
        {
            AssertDiffType("Class1.cs", "Class1_WithTwoMethods.cs", "Class1.cs", TypeOfDiff.UserChangeOnly);
        }
    }

    [TestFixture]
    public class Template_Adds_Method : TestBaseClass
    {
        [Test]
        public void The_Diff_Type_Is_Template_Change()
        {
            AssertDiffType("Class1.cs", "Class1.cs", "Class1_WithTwoMethods.cs", TypeOfDiff.TemplateChangeOnly);
        }
    }
}

namespace Specs_For_CSharp_Code_Root.When_Two_Files_Add_Different_Constructs
{
    [TestFixture]
    public class Diff_Type_Is_User_And_Template_Change : TestBaseClass
    {
        [Test]
        public void Test()
        {
            AssertDiffType("Class1.cs", "Class1_WithConstructor.cs", "Class1_WithTwoMethods.cs", TypeOfDiff.UserAndTemplateChange);
        }
    }
}

namespace Specs_For_CSharp_Code_Root.When_The_Files_Are_All_The_Same
{
    [TestFixture]
    public class Empty_Class :TestBaseClass
    {
        [Test]
        public void The_Diff_Type_Is_Exact_Copy()
        {
            AssertDiffType("Class1.cs", "Class1.cs", "Class1.cs", TypeOfDiff.ExactCopy);
        }
    }


    [TestFixture]
    public class Class_With_Constructor : TestBaseClass
    {
        [Test]
        public void The_Diff_Type_Is_Exact_Copy()
        {
            AssertDiffType("Class1_WithConstructor.cs", "Class1_WithConstructor.cs", "Class1_WithConstructor.cs", TypeOfDiff.ExactCopy);
        }
    }
}

namespace Specs_For_CSharp_Code_Root.When_The_PrevGen_File_Is_Missing
{
    [TestFixture]
    public class Files_Are_The_Same : TestBaseClass
    {
        [Test]
        public void The_Diff_Type_Is_Exact_Copy()
        {
            AssertDiffType("", "Class1.cs", "Class1.cs", TypeOfDiff.ExactCopy);
            AssertDiffType("", "Class1_WithConstructor.cs", "Class1_WithConstructor.cs", TypeOfDiff.ExactCopy);
        }
    }


    [TestFixture]
    public class Files_Are_Different : TestBaseClass
    {
        [Test]
        public void The_Diff_Type_Is_Change()
        {
            AssertDiffType("", "Class1.cs", "Class1_WithConstructor.cs", TypeOfDiff.TemplateChangeOnly);
            AssertDiffType("", "Class1_WithConstructor.cs", "Class1.cs", TypeOfDiff.UserChangeOnly);
        }
    }
}

namespace Specs_For_CSharp_Code_Root
{
    public class TestBaseClass
    {
        protected static readonly string RESOURCES_FOLDER = Path.Combine("Resources", "CSharp");

        public CodeRoot GetCodeRoot(string path)
        {
            if (File.Exists(path) == false)
                return null;

			CSharpParser formatter = new CSharpParser();
            formatter.ParseCode(File.ReadAllText(path));
            return (CodeRoot)formatter.CreatedCodeRoot;
        }

        public void AssertDiffType(string prevgenFilename, string userFilename, string templateFilename, TypeOfDiff typeOfDiff)
        {
            CodeRoot prevgen = GetCodeRoot(Path.Combine(RESOURCES_FOLDER, prevgenFilename));
            CodeRoot template = GetCodeRoot(Path.Combine(RESOURCES_FOLDER, templateFilename));
            CodeRoot user = GetCodeRoot(Path.Combine(RESOURCES_FOLDER, userFilename));

            CodeRootMap map = new CodeRootMap();
            if(prevgen != null) map.AddCodeRoot(prevgen, Version.PrevGen);
            if (template != null) map.AddCodeRoot(template, Version.NewGen);
            if (user != null) map.AddCodeRoot(user, Version.User);

            TypeOfDiff diff = map.Diff();

            Assert.That(diff, Is.EqualTo(typeOfDiff));
        }
    }
}