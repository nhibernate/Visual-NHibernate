using System.IO;
using System.Text;
using System.Xml;
using ArchAngel.Designer.DesignerProject;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;

namespace Specs_For_Project_Saving__Functions
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
			fileController.Stub(f => f.CanCreateFilesIn("Folder")).Return(true);
		}

		[Test]
		public void The_Create_Method_Creates_The_Folder()
		{
			IProjectSerialiser serialiser = new ProjectSerialiserV1(fileController);
			serialiser.CreateFunctionFiles(new FunctionInfo[0], "Folder");

            fileController.AssertWasCalled(f => f.CreateDirectory("Folder"));
		}
	}

	[TestFixture]
	public class When_The_Functions_Folder_Isnt_Writable
	{
		IFileController fileController;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			fileController.Stub(f => f.DirectoryExists("Folder")).Return(true);
			fileController.Stub(f => f.CanCreateFilesIn("Folder")).Return(false);
		}

		[Test]
		[ExpectedException(typeof(IOException))]
		public void The_Create_Method_Throws_An_Exception()
		{
			IProjectSerialiser serialiser = new ProjectSerialiserV1(fileController);
			serialiser.CreateFunctionFiles(new FunctionInfo[0], "Folder");
		}
	}

	[TestFixture]
	public class When_The_Function_Folder_Does_Exist_But_No_Functions_Are_Given
	{
		IFileController fileController;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			fileController.Stub(f => f.DirectoryExists("Folder")).Return(true);
			fileController.Stub(f => f.CanCreateFilesIn("Folder")).Return(true);
		}

		[Test]
		public void The_Create_Method_Writes_Nothing()
		{
			IProjectSerialiser serialiser = new ProjectSerialiserV1(fileController);
			serialiser.CreateFunctionFiles(new FunctionInfo[0], "Folder");
			
			fileController.AssertWasNotCalled(f => f.WriteAllText(null, null), c => c.IgnoreArguments());
			fileController.AssertWasNotCalled(f => f.WriteResourceToFile(null, null, null), c => c.IgnoreArguments());
			fileController.AssertWasNotCalled(f => f.WriteStreamToFile(null, null), c => c.IgnoreArguments());
		}
	}

	[TestFixture]
	public class When_The_Function_Folder_Exists_And_One_Function_Is_Given
	{
		IFileController fileController;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			fileController.Stub(f => f.DirectoryExists("Folder")).Return(true);
			fileController.Stub(f => f.CanCreateFilesIn("Folder")).Return(true);
		}

		[Test]
		public void The_Create_Method_Writes_One_File()
		{
			IProjectSerialiser serialiser = new ProjectSerialiserV1(fileController);
			FunctionInfo functionInfo = new FunctionInfo("Function", typeof(string), "Body", true, SyntaxEditorHelper.ScriptLanguageTypes.CSharp, "desc", "C#", "Gen");
			serialiser.CreateFunctionFiles(new []{functionInfo}, "Folder");

			fileController.AssertWasCalled(f => f.WriteAllText(Arg<string>.Is.Equal("Folder\\Function.function.xml"), Arg<string>.Is.NotNull));
		}
	}

	[TestFixture]
	public class Constructing_The_XML_For_A_Function
	{
		IFileController fileController;
		
		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
		}

		private const string expectedXml = 
			"<Function version=\"1\" about=\"This file describes an ArchAngel Template function\">" + 
			"<Name>Function</Name><IsTemplateFunction>True</IsTemplateFunction>" +
            "<IsExtensionMethod>False</IsExtensionMethod><ReturnType>System.String</ReturnType>"+
            "<TemplateReturnLanguage>C#</TemplateReturnLanguage><ExtendedType /><ScriptLanguage>CSharp</ScriptLanguage>" +
			"<Description>desc</Description><Category>Gen</Category>" +
            "<Parameters><Parameter name=\"varName\" type=\"System.Int32\" modifiers=\"static\" /></Parameters><Body>Body</Body>" +
			"</Function>";

		[Test]
		public void The_Write_Method_Creates_The_Correct_XML()
		{
			ProjectSerialiserV1 serialiser = new ProjectSerialiserV1(fileController);
			FunctionInfo functionInfo = new FunctionInfo("Function", typeof(string), "Body", true, SyntaxEditorHelper.ScriptLanguageTypes.CSharp, "desc", "C#", "Gen");
			functionInfo.Parameters.Add(new ParamInfo("varName", typeof(int)){Modifiers = "static"});

			StringBuilder sb = new StringBuilder();
			XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings{OmitXmlDeclaration = true});
			serialiser.WriteFunctionXML(functionInfo, writer);
			writer.Close();

			string output = XmlSqueezer.RemoveWhitespaceBetweenElements(sb.ToString());
			Assert.That(output, Is.EqualTo(expectedXml));
		}
	}
}