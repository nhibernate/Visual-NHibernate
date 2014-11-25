using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Common.DesignerProject;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;
using Slyce.Common.ExtensionMethods;
using Specs_For_Project_Saving__StaticFiles;
using Specs_For_Project_Saving__UserOptions;

namespace Specs_For_Project_Loading__StaticFiles
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
			fileController.Stub(f => f.CanReadFilesFrom("Folder")).Return(false);
		}

		[Test]
		[ExpectedException(typeof(DirectoryNotFoundException))]
		public void The_Load_Method_Throws_An_Exception()
		{
			IProjectDeserialiser deserialiser = new ProjectDeserialiserV1(fileController);
			deserialiser.LoadStaticFilenames("Folder");
		}
	}

	[TestFixture]
	public class When_The_StaticFiles_Folder_Exists_But_Has_No_Static_Files
	{
		IFileController fileController;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			fileController.Stub(f => f.DirectoryExists("Folder")).Return(true);
            fileController.Stub(f => f.CanReadFilesFrom("Folder")).Return(true);
			fileController.Stub(f => f.FindAllFilesLike("Folder", "*.useroption.xml")).Return(new string[0]);
            fileController.Stub(f => f.FileExists(ProjectSerialiserV1.UserOptionsDetailsFileName)).Return(true);
            fileController.Stub(f => f.ReadAllText(ProjectSerialiserV1.UserOptionsDetailsFileName)).Return(Constructing_The_XML_For_The_UserOptions_Details.ExpectedXml);
		}

        public void The_Load_Details_Method_Creates_Nothing()
        {
            IProjectDeserialiser loader = new ProjectDeserialiserV1(fileController);
            loader.LoadStaticFilenames("Folder");

            fileController.AssertWasCalled(f => f.ReadAllText("Folder\\UserOptions.xml"));
        }
	}

	[TestFixture]
	public class Loading_From_XML
	{
		IFileController fileController;

		public const string ExpectedXml = Constructing_The_XML_For_StaticFilesDetails.ExpectedXml;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
            fileController.Stub(f => f.DirectoryExists("Folder")).Return(true);
            fileController.Stub(f => f.CanReadFilesFrom("Folder")).Return(true);
            fileController.Stub(f => f.GetAllFilesFrom("Folder")).Return(new[] { "Folder\\static.dat", "Folder\\StaticFiles.xml" });
			fileController.Stub(f => f.FileExists("Folder\\static.dat")).Return(true);
			fileController.Stub(f => f.CanReadFile("Folder\\static.dat")).Return(true);
			fileController.Stub(f => f.FileExists("Folder\\StaticFiles.xml")).Return(true);
			fileController.Stub(f => f.CanReadFile("Folder\\StaticFiles.xml")).Return(true);
			fileController.Stub(f => f.ReadAllText("Folder\\StaticFiles.xml")).Return(ExpectedXml);
		}

        [Test]
        public void The_Load_Method_Reads_One_File()
        {
            IProjectDeserialiser loader = new ProjectDeserialiserV1(fileController);
            loader.LoadStaticFilenames("Folder");

			fileController.AssertWasCalled(f => f.ReadAllText(Arg<string>.Is.Equal("Folder\\StaticFiles.xml")));
        }

		[Test]
		public void The_Load_Method_Gets_The_Correct_Filenames()
		{
			ProjectDeserialiserV1 deserialiser = new ProjectDeserialiserV1(fileController);
            var files = deserialiser.ReadStaticFilesDetails(ExpectedXml.GetXmlDocRoot());

			Assert.That(files.Count(), Is.EqualTo(1));
			Assert.That(files.ElementAt(0).FullFilePath, Is.EqualTo("static.dat"));
			Assert.That(files.ElementAt(0).DisplayName, Is.EqualTo("static.dat"));
		}
	}
}