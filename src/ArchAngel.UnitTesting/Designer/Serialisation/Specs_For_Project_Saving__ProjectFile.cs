using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using ArchAngel.Designer.DesignerProject;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;

namespace Specs_For_Project_Saving__Projects
{
	[TestFixture]
	public class When_The_Project_Folder_Doesnt_Exist
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
	public class When_The_Project_Folder_Isnt_Writable
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
	public class When_The_Project_Folder_Exists_And_A_Valid_Project_Is_Given
	{
		private IFileController fileController;
		private IDesignerProject project;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			fileController.Stub(f => f.DirectoryExists("Folder")).Return(true);
			fileController.Stub(f => f.CanCreateFilesIn("Folder")).Return(true);

			project = MockRepository.GenerateStub<IDesignerProject>();
			project.ProjectName = "Project";
            project.Stub(p => p.Namespaces).Return(new List<string>());
            project.Stub(p => p.References).Return(new List<ReferencedFile>().AsReadOnly());
		}

		[Test]
		public void The_Create_Method_Writes_One_File()
		{
			IProjectSerialiser serialiser = new ProjectSerialiserV1(fileController);
			serialiser.CreateProjectDetailsFile(project, "Folder");

			fileController.AssertWasCalled(f => f.WriteAllText(Arg<string>.Is.Equal("Folder\\Project.aad"), Arg<string>.Is.NotNull));
		}
	}

	[TestFixture]
	public class Constructing_The_XML_For_A_Project
	{
		private IFileController fileController;
		private IDesignerProject project;
		
		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			project = MockRepository.GenerateStub<IDesignerProject>();
			project.ProjectName = "Test";
			project.ProjectDescription = "Desc";
			project.DebugProjectFile = "debug.wbproj";
			project.Stub(p => p.GetCompiledDLLDirectory()).Return("temp-compile");
			project.Stub(p => p.GetPathRelativeToProjectFile("debug.wbproj")).Return("debug.wbproj");
			project.Stub(p => p.GetPathRelativeToProjectFile("test gen")).Return("test gen");
			project.Version = "1.2.3.2.1";
			project.TestGenerateDirectory = "test gen";
            project.Stub(p => p.Namespaces).Return(new List<string>());
            project.Stub(p => p.References).Return(new List<ReferencedFile>().AsReadOnly());
		}

		private const string expectedXml =
			"<Project version=\"1\">" +
			"<Name>Test</Name><Description>Desc</Description>" +
			"<Config><RelativeCompilePath>temp-compile</RelativeCompilePath>" +
			"<Version>1.2.3.2.1</Version><ProjectType>None</ProjectType><DebugProjectFile>debug.wbproj</DebugProjectFile>" +
			"<TestGenerateDirectory>test gen</TestGenerateDirectory><IncludedNamespaces /><References /></Config>" +
			"</Project>";

		[Test]
		public void The_Write_Method_Creates_The_Right_XML()
		{
			ProjectSerialiserV1 serialiser = new ProjectSerialiserV1(fileController);
			
			StringBuilder sb = new StringBuilder();
			XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings{OmitXmlDeclaration = true});
			serialiser.WriteProjectDetailsXML(project, writer);
			writer.Close();

			string output = XmlSqueezer.RemoveWhitespaceBetweenElements(sb.ToString());
			Assert.That(output, Is.EqualTo(expectedXml));
		}
	}
}