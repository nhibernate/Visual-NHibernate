using System.IO;
using System.Text;
using System.Xml;
using ArchAngel.Designer.DesignerProject;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;

namespace Specs_For_Project_Saving__Outputs
{
	[TestFixture]
	public class When_The_ProjectFiles_Folder_Doesnt_Exist
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
			serialiser.CreateOutputFile(new OutputFolder("ROOT", ""), "Folder");

            fileController.AssertWasCalled(f => f.CreateDirectory("Folder"));
		}
	}

	[TestFixture]
	public class When_The_ProjectFiles_Folder_Isnt_Writable
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
			serialiser.CreateOutputFile(new OutputFolder("ROOT", ""), "Folder");
		}
	}

	[TestFixture]
	public class When_The_ProjectFiles_Folder_Exists_And_A_Valid_RootFolder_Is_Given
	{
		private IFileController fileController;

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
			serialiser.CreateOutputFile(new OutputFolder("ROOT", ""), "Folder");

			fileController.AssertWasCalled(f => f.WriteAllText(Arg<string>.Is.Equal("Folder\\Outputs.xml"), Arg<string>.Is.NotNull));
		}
	}

	[TestFixture]
	public class Constructing_The_XML_For_A_Projects_Outputs
	{
		private IFileController fileController;
		private OutputFolder rootFolder;
		
		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			rootFolder = new OutputFolder("ROOT", "1");
		}

		private const string expectedXml =
			"<Outputs version=\"1\"><Folder name=\"ROOT\" id=\"1\" /></Outputs>";

		[Test]
		public void The_Write_Method_Creates_The_Right_XML()
		{
			ProjectSerialiserV1 serialiser = new ProjectSerialiserV1(fileController);
			
			StringBuilder sb = new StringBuilder();
			XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings{OmitXmlDeclaration = true});
			serialiser.WriteOutputsXML(rootFolder, writer);
			writer.Close();

			string output = XmlSqueezer.RemoveWhitespaceBetweenElements(sb.ToString());
			Assert.That(output, Is.EqualTo(expectedXml));
		}
	}

    [TestFixture]
    public class Constructing_The_XML_For_A_Projects_Outputs_SubFolder
    {
        private IFileController fileController;
        private OutputFolder rootFolder;

        [SetUp]
        public void SetUp()
        {
            fileController = MockRepository.GenerateMock<IFileController>();
            rootFolder = new OutputFolder("ROOT", "1");
            rootFolder.Folders.Add(new OutputFolder("Sub", "2"));
        }

        private const string expectedXml =
            "<Outputs version=\"1\"><Folder name=\"ROOT\" id=\"1\"><Folder name=\"Sub\" id=\"2\" /></Folder></Outputs>";

        [Test]
        public void The_Write_Method_Creates_The_Right_XML()
        {
            ProjectSerialiserV1 serialiser = new ProjectSerialiserV1(fileController);

            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings { OmitXmlDeclaration = true });
            serialiser.WriteOutputsXML(rootFolder, writer);
            writer.Close();

            string output = XmlSqueezer.RemoveWhitespaceBetweenElements(sb.ToString());
            Assert.That(output, Is.EqualTo(expectedXml));
        }
    }

	[TestFixture]
	public class Constructing_The_XML_For_A_Projects_Outputs_Static_OutputFile_No_Iterator
	{
		private IFileController fileController;
		private OutputFile outputFile;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			outputFile = new OutputFile("ROOT", OutputFileTypes.File, "aaaa", "1") {StaticFileSkipFunctionName = "func1_Skip"};
		}

		private const string expectedXml =
			"<StaticFile name=\"ROOT\" id=\"1\" staticfilename=\"aaaa\" skipfunction=\"func1_Skip\" />";

		[Test]
		public void The_Write_Method_Creates_The_Right_XML()
		{
			ProjectSerialiserV1 serialiser = new ProjectSerialiserV1(fileController);

			StringBuilder sb = new StringBuilder();
			XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings { OmitXmlDeclaration = true });
			serialiser.WriteOutputFileXML(outputFile, writer);
			writer.Close();

			string output = XmlSqueezer.RemoveWhitespaceBetweenElements(sb.ToString());
			Assert.That(output, Is.EqualTo(expectedXml));
		}
	}

	[TestFixture]
	public class Constructing_The_XML_For_A_Projects_Outputs_Static_OutputFile_With_Iterator
	{
		private IFileController fileController;
		private OutputFile outputFile;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			outputFile = new OutputFile("ROOT", OutputFileTypes.File, "aaaa", "1") { StaticFileIterator = typeof(string)};
		}

		private const string expectedXml =
			"<StaticFile name=\"ROOT\" id=\"1\" staticfilename=\"aaaa\" iteratorname=\"System.String\" />";

		[Test]
		public void The_Write_Method_Creates_The_Right_XML()
		{
			ProjectSerialiserV1 serialiser = new ProjectSerialiserV1(fileController);

			StringBuilder sb = new StringBuilder();
			XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings { OmitXmlDeclaration = true });
			serialiser.WriteOutputFileXML(outputFile, writer);
			writer.Close();

			string output = XmlSqueezer.RemoveWhitespaceBetweenElements(sb.ToString());
			Assert.That(output, Is.EqualTo(expectedXml));
		}
	}

	[TestFixture]
	public class Constructing_The_XML_For_A_Projects_Outputs_Script_OutputFile
	{
		private IFileController fileController;
		private OutputFile outputFile;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			outputFile = new OutputFile("ROOT", OutputFileTypes.Script, "aaaa", "1");
		}

		private const string expectedXml =
			"<ScriptFile name=\"ROOT\" id=\"1\" scriptname=\"aaaa\" />";

		[Test]
		public void The_Write_Method_Creates_The_Right_XML()
		{
			ProjectSerialiserV1 serialiser = new ProjectSerialiserV1(fileController);

			StringBuilder sb = new StringBuilder();
			XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings { OmitXmlDeclaration = true });
			serialiser.WriteOutputFileXML(outputFile, writer);
			writer.Close();

			string output = XmlSqueezer.RemoveWhitespaceBetweenElements(sb.ToString());
			Assert.That(output, Is.EqualTo(expectedXml));
		}
	}
}