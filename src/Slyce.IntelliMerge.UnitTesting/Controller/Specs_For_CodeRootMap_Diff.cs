using System;
using System.IO;
using ArchAngel.Providers;
using ArchAngel.Providers.CodeProvider;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.IntelliMerge;
using Slyce.IntelliMerge.Controller;
using Version=Slyce.IntelliMerge.Controller.Version;

namespace Specs_For_CodeRootMap.Diff
{
    [TestFixture]
    public class No_Code_Roots_Added
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Exception_Raised()
        {
            CodeRootMap map = new CodeRootMap();
            map.Diff();
        }
    }

    [TestFixture]
    public class Diffing_A_Basic_CSharp_File
    {
        public static readonly string ResourcePath = Path.Combine("Resources", "CSharp");

        [Test]
        public void Diff_Result_Is_Exact_Copy()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");

            CodeRootMap codeRootMap = new CodeRootMap();

			CSharpParser formatter = new CSharpParser();
            formatter.ParseCode(File.ReadAllText(original));
            ICodeRoot codeRoot = formatter.CreatedCodeRoot;

            codeRootMap.AddCodeRoot(codeRoot, Version.User);
            codeRootMap.AddCodeRoot(codeRoot, Version.NewGen);
            codeRootMap.AddCodeRoot(codeRoot, Version.PrevGen);

            Assert.That(codeRootMap.Diff(), Is.EqualTo(TypeOfDiff.ExactCopy));
        }
    }

    [TestFixture]
    public class Diffing_CSharp_Files_One_File_Missing
    {
        public static readonly string ResourcePath = Path.Combine("Resources", "CSharp");

        [Test]
        public void User_File_Doesnt_Exist_Diff_Result_Is_Exact_Copy()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");

            CodeRootMap codeRootMap = new CodeRootMap();

            ICodeRoot codeRoot = Diffing_Two_Different_Basic_CSharp_Files.GetCodeRoot(original);
            codeRootMap.AddCodeRoot(codeRoot, Version.NewGen);

            Assert.That(codeRootMap.Diff(), Is.EqualTo(TypeOfDiff.ExactCopy));
        }
    }

    [TestFixture]
    public class Diffing_Two_Different_Basic_CSharp_Files
    {
        public static readonly string ResourcePath = Path.Combine("Resources", "CSharp");

        [Test]
        public void Diff_Result_Is_Change_Assembly_CS()
        {
            string original = Path.Combine(ResourcePath, "AssemblyInfo.cs");
            string changed = Path.Combine(ResourcePath, "AssemblyInfoWithExtraAttribute.cs");

            TestDiff(original, changed, true);
        }

    	[Test]
    	public void Diff_Does_Not_Modify_Base_Constructs()
    	{
			string original = Path.Combine(ResourcePath, "Class1.cs");
			string changed = Path.Combine(ResourcePath, "Class1_WithConstructor.cs");
			
			ICodeRoot userCR = GetCodeRoot(original);
			ICodeRoot newgenCR = GetCodeRoot(changed);
			ICodeRoot prevgenCR = GetCodeRoot(changed);

    		CodeRootMap map = new CodeRootMap();
    		map.AddCodeRoot(userCR, Version.User);
			map.AddCodeRoot(newgenCR, Version.NewGen);
			map.AddCodeRoot(prevgenCR, Version.PrevGen);

    		map.Diff();

    		ICodeRoot withoutDiff = GetCodeRoot(changed);

    		Assert.That(map.PrevGenCodeRoot.ToString(), Is.EqualTo(withoutDiff.ToString()));
			Assert.That(map.NewGenCodeRoot.ToString(), Is.EqualTo(withoutDiff.ToString()));
    	}

        [Test]
        public void Diff_Result_Is_Change_Constructor()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithConstructor.cs");

            ICodeRoot originalCR = GetCodeRoot(original);

            AssertBasicConstructsExist(originalCR);

            TestDiff(original, changed, true);
        }

        /// <summary>
        /// Returns the IBaseConstruct for Class1
        /// </summary>
        /// <param name="cr"></param>
        /// <returns></returns>
        private static void AssertBasicConstructsExist(ICodeRoot cr)
        {
            Assert.That(cr.WalkChildren(), Has.Count(1));

            IBaseConstruct namespaceBC = cr.WalkChildren()[0];
            Assert.That(namespaceBC.FullyQualifiedIdentifer, Is.EqualTo("Slyce.IntelliMerge.UnitTesting.Resources.CSharp"));

            Assert.That(namespaceBC.WalkChildren(), Has.Count(1));
            IBaseConstruct classBC = namespaceBC.WalkChildren()[0];
            Assert.That(classBC.FullyQualifiedIdentifer,
                        Is.EqualTo(string.Format("Slyce.IntelliMerge.UnitTesting.Resources.CSharp{0}Class1", BaseConstructConstants.FullyQualifiedIdentifierSeparator)));

            Assert.That(classBC.WalkChildren(), Has.Count(1));
            IBaseConstruct methodBC = classBC.WalkChildren()[0];
            Assert.That(methodBC.FullyQualifiedIdentifer,
                        Is.EqualTo(string.Format("Slyce.IntelliMerge.UnitTesting.Resources.CSharp{0}Class1{0}Method ()", BaseConstructConstants.FullyQualifiedIdentifierSeparator)));

            return;
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_Constructor_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithConstructor.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithConstructor_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        [Test]
        public void Diff_Result_Is_Change_Event()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithEvent.cs");

            TestDiff(original, changed, true);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_Reordered_Event_And_Method()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithEvent.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithEvent_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        [Test]
        public void Diff_Result_Is_Change_Extra_Method()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithTwoMethods.cs");

            TestDiff(original, changed, true);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_Reordered_Methods()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithTwoMethods.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithTwoMethods_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        [Test]
        public void Diff_Result_Is_Change_Change_Method_Text()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_ChangedMethodText.cs");

            TestDiff(original, changed, false);
        }

        [Test]
        public void Diff_Result_Is_Change_Class_Comments()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_ExtraComments.cs");

        	//TestDiff(original, changed, true);

			TestFiles_User(original, changed);
			TestFiles_Template(original, changed);
			TestFiles_UserAndTemplate(original, changed, TypeOfDiff.UserAndTemplateChange);
        }

        [Test]
        public void Diff_Result_Is_Change_Field()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithField.cs");

            TestDiff(original, changed, true);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_Field_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithField.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithField_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        [Test]
        public void Diff_Result_Is_Change_Operator()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithOperator.cs");

            TestDiff(original, changed, true);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_Operator_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithOperator.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithOperator_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }


        [Test]
        public void Diff_Result_Is_Change_PropertyAccessor()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithPropertyAccessors.cs");

            TestDiff(original, changed, true);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_PropertyAccessor_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithPropertyAccessors.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithPropertyAccessors_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_PropertyAccessor_GetSet_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithPropertyAccessors_Reordered.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithPropertyAccessors_GetSet_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        [Test]
        public void Diff_Result_Is_Change_Delegate()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithPropertyAccessors.cs");

            TestDiff(original, changed, true);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_Delegate_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithDelegate.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithDelegate_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        [Test]
        public void Diff_Result_Is_Change_Enum()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithEnum.cs");

            TestDiff(original, changed, true);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_Enum_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithEnum.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithEnum_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_Enum_Values_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithEnum_Reordered.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithEnum_Values_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        [Test]
        public void Diff_Result_Is_Change_Indexer()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithIndexer.cs");

            TestDiff(original, changed, true);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_Indexer_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithIndexer.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithIndexer_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_Indexer_GetSet_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithIndexer_Reordered.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithIndexer_GetSet_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        [Test]
        public void Diff_Result_Is_Change_UsingStatements()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithUsingStatements.cs");

			TestFiles_User(original, changed);
			TestFiles_Template(original, changed);
			TestFiles_UserAndTemplate(original, changed, TypeOfDiff.UserAndTemplateChange);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_UsingStatements_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithUsingStatements.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithUsingStatements_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        [Test]
        public void Diff_Result_Is_Change_Attributes()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithAttributes.cs");

        	TestFiles_User(original, changed);
			TestFiles_Template(original, changed);
			TestFiles_UserAndTemplate(original, changed, TypeOfDiff.UserAndTemplateChange);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_Attributes_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithAttributes.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithAttributes_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

		[Test]
		public void Diff_Result_Is_Change_Region()
		{
			string original = Path.Combine(ResourcePath, "Class1.cs");
			string changed = Path.Combine(ResourcePath, "Class1_WithRegion.cs");

			TestDiff(original, changed, true);
		}

		[Test]
		public void Diff_Result_Is_Exact_Copy_Region_Reordered()
		{
			string original = Path.Combine(ResourcePath, "Class1_WithRegion.cs");
			string changed = Path.Combine(ResourcePath, "Class1_WithRegion_Reordered.cs");

			AssertFilesAreSame(original, changed);
		}

        [Test]
		[Ignore("We don't support empty regions at the moment")]
        public void Diff_Result_Is_Change_Empty_Region()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithEmptyRegion.cs");

            TestDiff(original, changed, true);
        }

        [Test]
		[Ignore("We don't support empty regions at the moment")]
        public void Diff_Result_Is_Exact_Copy_Empty_Region_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithEmptyRegion.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithEmptyRegion_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        [Test]
        public void Diff_Result_Is_Change_Struct()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithStruct.cs");

            TestDiff(original, changed, true);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_Struct_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithStruct.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithStruct_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        [Test]
        public void Diff_Result_Is_Change_Interface()
        {
            string original = Path.Combine(ResourcePath, "Class1.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithInterface.cs");

            TestDiff(original, changed, true);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_Interface_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithInterface.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithInterface_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        [Test]
        public void Diff_Result_Is_Exact_Copy_Interface_Internally_Reordered()
        {
            string original = Path.Combine(ResourcePath, "Class1_WithInterface_Reordered.cs");
            string changed = Path.Combine(ResourcePath, "Class1_WithInterface_Internally_Reordered.cs");

            AssertFilesAreSame(original, changed);
        }

        internal static void TestDiff(string original, string changed, bool doUserAndTemplateChangeTest)
        {
            TestFiles_User(original, changed);
            TestFiles_Template(original, changed);
            if (doUserAndTemplateChangeTest)
                TestFiles_UserAndTemplate(original, changed, TypeOfDiff.ExactCopy);
        }

        internal static void TestFiles_User(string original, string changed)
        {
            CodeRootMap codeRootMap = new CodeRootMap();

            ICodeRoot codeRoot = GetCodeRoot(original);
            codeRootMap.AddCodeRoot(codeRoot, Version.NewGen);
            codeRootMap.AddCodeRoot(codeRoot, Version.PrevGen);

            codeRoot = GetCodeRoot(changed);
            codeRootMap.AddCodeRoot(codeRoot, Version.User);

            Assert.That(codeRootMap.Diff(), Is.EqualTo(TypeOfDiff.UserChangeOnly));
        }

        internal static void TestFiles_Template(string original, string changed)
        {
            CodeRootMap codeRootMap = new CodeRootMap();

            ICodeRoot codeRoot = GetCodeRoot(original);
            codeRootMap.AddCodeRoot(codeRoot, Version.User);
            codeRootMap.AddCodeRoot(codeRoot, Version.PrevGen);

            codeRoot = GetCodeRoot(changed);
            codeRootMap.AddCodeRoot(codeRoot, Version.NewGen);

            Assert.That(codeRootMap.Diff(), Is.EqualTo(TypeOfDiff.TemplateChangeOnly));
        }


        internal static void TestFiles_UserAndTemplate(string original, string changed, TypeOfDiff diffType)
        {
            CodeRootMap codeRootMap = new CodeRootMap();

            ICodeRoot codeRoot = GetCodeRoot(original);
            codeRootMap.AddCodeRoot(codeRoot, Version.PrevGen);

            codeRoot = GetCodeRoot(changed);
            codeRootMap.AddCodeRoot(codeRoot, Version.User);
            codeRootMap.AddCodeRoot(codeRoot, Version.NewGen);

            Assert.That(codeRootMap.Diff(), Is.EqualTo(diffType));
        }

        internal static ICodeRoot GetCodeRoot(string fileName)
        {
            CSharpParser formatter = new CSharpParser();
        	formatter.ParseCode(File.ReadAllText(fileName));
            return formatter.CreatedCodeRoot;
        }

        internal static void AssertFilesAreSame(string original, string changed)
        {
            CodeRootMap codeRootMap = new CodeRootMap();

            CSharpParser formatter = new CSharpParser();
			formatter.FormatSettings.ReorderBaseConstructs = true;
            formatter.ParseCode(File.ReadAllText(original));
            ICodeRoot codeRoot = formatter.CreatedCodeRoot;

            codeRootMap.AddCodeRoot(codeRoot, Version.NewGen);
            codeRootMap.AddCodeRoot(codeRoot, Version.PrevGen);

        	formatter = new CSharpParser();
			formatter.FormatSettings.ReorderBaseConstructs = true;
            formatter.ParseCode(File.ReadAllText(changed));
            codeRoot = formatter.CreatedCodeRoot;

            codeRootMap.AddCodeRoot(codeRoot, Version.User);

            Assert.That(codeRootMap.Diff(), Is.EqualTo(TypeOfDiff.ExactCopy));
        }
    }
}