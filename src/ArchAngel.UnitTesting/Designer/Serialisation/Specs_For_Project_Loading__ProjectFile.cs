using System.IO;
using ArchAngel.Common.DesignerProject;
using ArchAngel.Designer.DesignerProject;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;
using Slyce.Common.ExtensionMethods;

namespace Specs_For_Project_Loading__ProjectFile
{
	[TestFixture]
	public class When_The_Project_File_Doesnt_Exist
	{
		IFileController fileController;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			fileController.Stub(f => f.FileExists("ProjectFile.aad")).Return(false);
			fileController.Stub(f => f.CanReadFile("ProjectFile.aad")).Return(false);
		}

		[Test]
		[ExpectedException(typeof(FileNotFoundException))]
		public void The_Load_Method_Throws_An_Exception()
		{
			IProjectDeserialiser deserialiser = new ProjectDeserialiserV1(fileController);
			deserialiser.LoadProjectFile("ProjectFile.aad", null);
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
			fileController.Stub(f => f.ToAbsolutePath("temp-compile", "Folder\\Project.aad")).Return("Folder\\temp-compile");
			fileController.Stub(f => f.ToAbsolutePath("debug.wbproj", "Folder\\Project.aad")).Return("Folder\\debug.wbproj");
			fileController.Stub(f => f.ToAbsolutePath("file.dll", "Folder\\Project.aad")).Return("Folder\\file.dll");
            fileController.Stub(f => f.ToAbsolutePath("test gen", "Folder\\Project.aad")).Return("Folder\\test gen");
		}

		private const string ExpectedXml =
			"<Project version=\"1\">" +
			"<Name>Test</Name><Description>Desc</Description>" +
			"<Config><RelativeCompilePath>temp-compile</RelativeCompilePath>" +
			"<Version>1.2.3.2.1</Version><ProjectType>None</ProjectType><DebugProjectFile>debug.wbproj</DebugProjectFile>" +
			"<TestGenerateDirectory>test gen</TestGenerateDirectory>"+
			"<IncludedNamespaces><Namespace>Ns1</Namespace></IncludedNamespaces>"+
			"<References><Reference filename=\"file.dll\" mergewithassembly=\"true\" useinworkbench=\"true\" isprovider=\"false\" /></References></Config>" +
			"</Project>";

		[Test]
		public void The_Load_Method_Creates_The_Correct_Project()
		{
			ProjectDeserialiserV1 deserialiser = new ProjectDeserialiserV1(fileController);
			IDesignerProject project = MockRepository.GenerateMock<IDesignerProject>();

            deserialiser.ReadProject(ExpectedXml.GetXmlDocRoot(), project, "Folder\\Project.aad");

			project.AssertWasCalled(p => p.ProjectName = "Test");
			project.AssertWasCalled(p => p.ProjectDescription = "Desc");
			project.AssertWasCalled(p => p.CompileFolderName = "Folder\\temp-compile"); // This should be the absolute path.
			project.AssertWasCalled(p => p.Version = "1.2.3.2.1");
			project.AssertWasCalled(p => p.ProjType = ProjectTypes.None);
			project.AssertWasCalled(p => p.DebugProjectFile = "Folder\\debug.wbproj"); // Absolute path
            project.AssertWasCalled(p => p.TestGenerateDirectory = "Folder\\test gen"); // Absolute path
		}

		[Test]
		public void The_Load_Method_Loads_Namespaces_Correctly()
		{
			ProjectDeserialiserV1 deserialiser = new ProjectDeserialiserV1(fileController);
			IDesignerProject project = MockRepository.GenerateMock<IDesignerProject>();

            deserialiser.ReadProject(ExpectedXml.GetXmlDocRoot(), project, "Folder\\Project.aad");

			project.AssertWasCalled(p => p.AddNamespace(Arg<string>.Is.Equal("Ns1")));
			
		}

		[Test]
		public void The_Load_Method_Loads_ReferencedFiles_Correctly()
		{
			ProjectDeserialiserV1 deserialiser = new ProjectDeserialiserV1(fileController);
            IDesignerProject project = new ProjectBase(); //MockRepository.GenerateStub<IDesignerProject>();

            deserialiser.ReadProject(ExpectedXml.GetXmlDocRoot(), project, "Folder\\Project.aad");

			Assert.That(project.References, Has.Count(1));

            Assert.That(project.References[0].FileName, Is.EqualTo("Folder\\file.dll"));
            Assert.That(project.References[0].MergeWithAssembly, Is.True);
            Assert.That(project.References[0].UseInWorkbench, Is.True);
		}
	}


}