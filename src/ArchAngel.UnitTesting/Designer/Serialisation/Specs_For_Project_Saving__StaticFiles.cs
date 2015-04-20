using System.IO;
using System.Text;
using System.Xml;
using ArchAngel.Designer.DesignerProject;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;

namespace Specs_For_Project_Saving__StaticFiles
{
	[TestFixture]
	public class When_The_StaticFiles_Folder_Doesnt_Exist
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
			serialiser.CreateStaticFilesDetails(new IncludedFile[0], "Folder");

            fileController.AssertWasCalled(f => f.CreateDirectory("Folder"));
		}
	}

	[TestFixture]
	public class When_The_StaticFiles_Folder_Isnt_Writable
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
			serialiser.CreateStaticFiles(new IncludedFile[0], "Folder");
		}
	}

	[TestFixture]
	public class When_The_StaticFiles_Folder_Does_Exist_But_No_StaticFiles_Are_Given
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
		public void The_CreateFiles_Method_Writes_Just_The_Details()
		{
			IProjectSerialiser serialiser = new ProjectSerialiserV1(fileController);
			serialiser.CreateStaticFilesDetails(new IncludedFile[0], "Folder");
			
			fileController.AssertWasCalled(f => f.WriteAllText(Arg<string>.Is.Equal("Folder\\StaticFiles.xml"), Arg<string>.Is.NotNull));
		}
	}

	[TestFixture]
	public class When_The_StaticFiles_Folder_Exists_And_One_StaticFile_Is_Given
	{
		private IFileController fileController;
		private byte[] bytes;

		[SetUp]
		public void SetUp()
		{
			bytes = new byte[] { 0, 1, 2 };

			fileController = MockRepository.GenerateMock<IFileController>();
			fileController.Stub(f => f.DirectoryExists("Folder")).Return(true);
			fileController.Stub(f => f.CanCreateFilesIn("Folder")).Return(true);
			fileController.Stub(f => f.ReadAllBytes("static.dat")).Return(bytes);
		}

		[Test]
		public void The_Create_Method_Copies_One_Static_File()
		{
			IProjectSerialiser serialiser = new ProjectSerialiserV1(fileController);
			serialiser.CreateStaticFiles(new[] { new IncludedFile("static.dat"),  }, "Folder");

			fileController.AssertWasCalled(f => f.WriteAllBytes(Arg<string>.Is.Equal("Folder\\static.dat"), Arg<byte[]>.Is.NotNull));
		}
	}

	[TestFixture]
	public class Constructing_The_XML_For_StaticFilesDetails
	{
		IFileController fileController;
		
		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
		}

		public const string ExpectedXml =
			"<StaticFiles version=\"1\">" +
            "<StaticFile filename=\"static.dat\" displayname=\"static.dat\" />" +
			"</StaticFiles>";

		[Test]
		public void The_Write_Method_Creates_The_Correct_XML()
		{
			ProjectSerialiserV1 serialiser = new ProjectSerialiserV1(fileController);
			
			StringBuilder sb = new StringBuilder();
			XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings{OmitXmlDeclaration = true});
			serialiser.WriteStaticFilesDetails(new[]{new IncludedFile("static.dat"), }, writer);
			writer.Close();

			string output = XmlSqueezer.RemoveWhitespaceBetweenElements(sb.ToString());
			Assert.That(output, Is.EqualTo(ExpectedXml));
		}
	}
}