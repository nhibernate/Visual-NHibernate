using System.IO;
using ArchAngel.Common.DesignerProject;
using ArchAngel.Designer.DesignerProject;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;
using Slyce.Common.ExtensionMethods;

namespace Specs_For_Project_Loading__Outputs
{
	[TestFixture]
    public class When_The_Outputs_Folder_Doesnt_Exist
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
			IProjectDeserialiser deserialiser = new ProjectDeserialiserV1(fileController);
			deserialiser.LoadOutputsFile("Folder");
		}
	}

	[TestFixture]
	public class When_The_Outputs_File_Doesnt_Exist
	{
		IFileController fileController;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			fileController.Stub(f => f.DirectoryExists("Folder")).Return(false);
			fileController.Stub(f => f.CanReadFile("Folder\\Outputs.xml")).Return(false);
		}

		[Test]
		[ExpectedException(typeof(DirectoryNotFoundException))]
		public void The_Load_Method_Throws_An_Exception()
		{
			IProjectDeserialiser deserialiser = new ProjectDeserialiserV1(fileController);
			deserialiser.LoadOutputsFile("Folder");
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
			fileController.Stub(f => f.FileExists("Folder\\Outputs.xml")).Return(true);
			fileController.Stub(f => f.CanReadFile("Folder\\Outputs.xml")).Return(true);
			fileController.Stub(f => f.ReadAllText("Folder\\Outputs.xml")).Return(JustRootFolder);
		}


        [Test]
        public void The_Load_Method_Reads_One_File()
        {
            IProjectDeserialiser loader = new ProjectDeserialiserV1(fileController);
			loader.LoadOutputsFile("Folder");

			fileController.AssertWasCalled(f => f.ReadAllText(Arg<string>.Is.Equal("Folder\\Outputs.xml")));
        }

		private const string JustRootFolder = "<Outputs version=\"1\"><Folder name=\"ROOT\" id=\"1\" /></Outputs>";
		[Test]
		public void The_Load_Method_Creates_The_Correct_Outputs_JustRoot()
		{
			ProjectDeserialiserV1 deserialiser = new ProjectDeserialiserV1(fileController);
            OutputFolder rootFolder = deserialiser.ReadOutputs(JustRootFolder.GetXmlDocRoot());
		    
			Assert.That(rootFolder.Name, Is.EqualTo("ROOT"));
			Assert.That(rootFolder.Id, Is.EqualTo("1"));
			Assert.That(rootFolder.IteratorType, Is.Null);
		}

		private const string JustRootFolderWithIterator = "<Outputs version=\"1\"><Folder name=\"ROOT\" id=\"1\" iterator=\"System.String\"/></Outputs>";
		[Test]
		public void The_Load_Method_Creates_The_Correct_Outputs_JustRoot_With_Iterator()
		{
			ProjectDeserialiserV1 deserialiser = new ProjectDeserialiserV1(fileController);
            OutputFolder rootFolder = deserialiser.ReadOutputs(JustRootFolderWithIterator.GetXmlDocRoot());

			Assert.That(rootFolder.Name, Is.EqualTo("ROOT"));
			Assert.That(rootFolder.Id, Is.EqualTo("1"));
			Assert.That(rootFolder.IteratorType, Is.EqualTo(typeof(string)));
		}

		private const string RootFolderWithSubFolder = "<Outputs version=\"1\"><Folder name=\"ROOT\" id=\"1\"><Folder name=\"Sub\" id=\"2\" /></Folder></Outputs>";
		[Test]
		public void The_Load_Method_Creates_The_Correct_Outputs_SubFolder()
		{
			ProjectDeserialiserV1 deserialiser = new ProjectDeserialiserV1(fileController);
            OutputFolder rootFolder = deserialiser.ReadOutputs(RootFolderWithSubFolder.GetXmlDocRoot());

			Assert.That(rootFolder.Folders, Has.Count(1));
			var subfolder = rootFolder.Folders[0];

			Assert.That(subfolder.Name, Is.EqualTo("Sub"));
			Assert.That(subfolder.Id, Is.EqualTo("2"));
			Assert.That(subfolder.IteratorType, Is.Null);
		}

		private const string SubFolderWithFile = "<Outputs version=\"1\"><Folder name=\"ROOT\" id=\"1\"><Folder name=\"Sub\" id=\"2\"><ScriptFile name=\"A1\" id=\"3\" scriptname=\"script-1\" /></Folder></Folder></Outputs>";
		[Test]
		public void The_Load_Method_Creates_The_Correct_Outputs_SubFolder_With_File()
		{
			ProjectDeserialiserV1 deserialiser = new ProjectDeserialiserV1(fileController);
            OutputFolder rootFolder = deserialiser.ReadOutputs(SubFolderWithFile.GetXmlDocRoot());

			Assert.That(rootFolder.Folders, Has.Count(1));
			var subfolder = rootFolder.Folders[0];
			Assert.That(subfolder.Files, Has.Count(1));
			var file = subfolder.Files[0];

			Assert.That(file.Name, Is.EqualTo("A1"));
			Assert.That(file.Id, Is.EqualTo("3"));
			Assert.That(file.ScriptName, Is.EqualTo("script-1"));
		}
	}
}