using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArchAngel.Common.DesignerProject;
using ArchAngel.Designer.DesignerProject;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;
using Slyce.Common.ExtensionMethods;

namespace Specs_For_Project_Loading__Functions
{
	[TestFixture]
	public class When_The_Functions_Folder_Doesnt_Exist
	{
		IFileController fileController;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			fileController.Stub(f => f.DirectoryExists("Folder")).Return(false);
			fileController.Stub(f => f.CanReadFilesFrom("Folder")).Return(false);
		}

		[Test]
		[ExpectedException(typeof(DirectoryNotFoundException))]
		public void The_Load_Method_Throws_An_Exception()
		{
			IProjectDeserialiser serialiser = new ProjectDeserialiserV1(fileController);
			serialiser.LoadFunctionFiles("Folder").ToList();
		}
	}

	[TestFixture]
	public class When_The_Function_Folder_Exists_But_Has_No_Functions_Files
	{
		IFileController fileController;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			fileController.Stub(f => f.DirectoryExists("Folder")).Return(true);
            fileController.Stub(f => f.CanReadFilesFrom("Folder")).Return(true);
			fileController.Stub(f => f.FindAllFilesLike("Folder", "*.function.xml")).Return(new string[0]);
		}

		[Test]
		public void The_Load_Method_Creates_Nothing()
		{
			IProjectDeserialiser loader = new ProjectDeserialiserV1(fileController);
            IEnumerable<FunctionInfo> functions = loader.LoadFunctionFiles("Folder");
			
			Assert.That(functions, Is.Empty);
		}
	}

	[TestFixture]
	public class Loading_From_XML
	{
		IFileController fileController;
		
		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
            fileController.Stub(f => f.DirectoryExists("Folder")).Return(true);
            fileController.Stub(f => f.CanReadFilesFrom("Folder")).Return(true);
            fileController.Stub(f => f.FindAllFilesLike("Folder", "*.function.xml")).Return(new[] { "Folder\\Function.function.xml" });
            fileController.Stub(f => f.ReadAllText("Folder\\Function.function.xml")).Return(expectedXml);
		}

		private const string expectedXml =
            "<Function version=\"1\" about=\"This file describes an ArchAngel Template function\">" +
            "<Name>Function</Name><IsTemplateFunction>True</IsTemplateFunction>" +
            "<IsExtensionMethod>False</IsExtensionMethod><ReturnType>System.String</ReturnType>" +
            "<TemplateReturnLanguage>C#</TemplateReturnLanguage><ExtendedType /><ScriptLanguage>CSharp</ScriptLanguage>" +
            "<Description>desc</Description><Category>Gen</Category>"+
            "<Parameters><Parameter name=\"varName\" type=\"System.Int32\" modifiers=\"static\" /></Parameters><Body>Body</Body>" +
            "</Function>";

        [Test]
        public void The_Load_Method_Reads_One_File()
        {
            IProjectDeserialiser loader = new ProjectDeserialiserV1(fileController);
            loader.LoadFunctionFiles("Folder").ToList();

            fileController.AssertWasCalled(f => f.ReadAllText(Arg<string>.Is.Equal("Folder\\Function.function.xml")));
        }

		[Test]
        public void The_Load_Method_Creates_The_Correct_Function()
		{
			ProjectDeserialiserV1 serialiser = new ProjectDeserialiserV1(fileController);
            var fun = serialiser.ReadFunction(expectedXml.GetXmlDocRoot());

			Assert.That(fun.Name, Is.EqualTo("Function"));
            Assert.That(fun.ReturnType, Is.EqualTo(typeof(string)));
            Assert.That(fun.TemplateReturnLanguage, Is.EqualTo("C#"));
            Assert.That(fun.ExtendedType, Is.EqualTo(""));
            Assert.That(fun.ScriptLanguage, Is.EqualTo(SyntaxEditorHelper.ScriptLanguageTypes.CSharp));
            Assert.That(fun.Description, Is.EqualTo("desc"));
            Assert.That(fun.Category, Is.EqualTo("Gen"));
            Assert.That(fun.Body, Is.EqualTo("Body"));
            Assert.That(fun.IsTemplateFunction, Is.True);
            Assert.That(fun.IsExtensionMethod, Is.False);
		}

        [Test]
        public void The_Load_Method_Loads_Parameters_Correctly()
        {
            ProjectDeserialiserV1 serialiser = new ProjectDeserialiserV1(fileController);
            var fun = serialiser.ReadFunction(expectedXml.GetXmlDocRoot());

            Assert.That(fun.Parameters.Count, Is.EqualTo(1));
            ParamInfo parameter = fun.Parameters[0];
            Assert.That(parameter.Name, Is.EqualTo("varName"));
            Assert.That(parameter.Modifiers, Is.EqualTo("static"));
            Assert.That(parameter.DataType, Is.EqualTo(typeof(int)));
        }
	}
}