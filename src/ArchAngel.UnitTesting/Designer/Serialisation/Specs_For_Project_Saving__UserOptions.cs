using System.IO;
using System.Text;
using System.Xml;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Common.DesignerProject;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;

namespace Specs_For_Project_Saving__UserOptions
{
	[TestFixture]
	public class When_The_UserOptions_Folder_Doesnt_Exist
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
			serialiser.CreateUserOptionsFiles(new UserOption[0], "Folder");

            fileController.AssertWasCalled(f => f.CreateDirectory("Folder"));
		}
	}

	[TestFixture]
	public class When_The_UserOptions_Folder_Isnt_Writable
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
			serialiser.CreateUserOptionsFiles(new UserOption[0], "Folder");
		}
	}

	[TestFixture]
	public class When_The_UserOptions_Folder_Does_Exist_But_No_UserOptions_Are_Given
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
			serialiser.CreateUserOptionsFiles(new UserOption[0], "Folder");
			
			fileController.AssertWasNotCalled(f => f.WriteAllText(null, null), c => c.IgnoreArguments());
			fileController.AssertWasNotCalled(f => f.WriteResourceToFile(null, null, null), c => c.IgnoreArguments());
			fileController.AssertWasNotCalled(f => f.WriteStreamToFile(null, null), c => c.IgnoreArguments());
		}
	}

	[TestFixture]
	public class When_The_UserOptions_Folder_Exists_And_One_UserOption_Is_Given
	{
		private IFileController fileController;
		private UserOption userOption;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			fileController.Stub(f => f.DirectoryExists("Folder")).Return(true);
			fileController.Stub(f => f.CanCreateFilesIn("Folder")).Return(true);

			userOption = new UserOption("var", "Gen", typeof(string), "test-text", "test-description", new string[0], "dv-body", typeof(bool), "validator body", "return true;", true);
		}

		[Test]
		public void The_Create_Method_Writes_One_File()
		{
			IProjectSerialiser serialiser = new ProjectSerialiserV1(fileController);
			serialiser.CreateUserOptionsFiles(new[] { userOption }, "Folder");

			fileController.AssertWasCalled(f => f.WriteAllText(Arg<string>.Is.Equal("Folder\\var.useroption.xml"), Arg<string>.Is.NotNull));
		}
	}

	[TestFixture]
	public class Constructing_The_XML_For_UserOptions
	{
		IFileController fileController;
		private UserOption userOption;
		
		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			userOption = new UserOption("var", "Gen", typeof(string), "test-text", "test-description", new []{"val1"}, "dv-body", typeof(bool), "validator body", "return true;", true);
		}

		private const string expectedXml =
			"<Option version=\"1\">" +
            "<VariableName>var</VariableName><Type>System.String</Type><DisplayText>test-text</DisplayText>" +
			"<Description>test-description</Description>" +
			"<DefaultValue>dv-body</DefaultValue><IteratorName>System.Boolean</IteratorName>" +
			"<ValidatorFunction>validator body</ValidatorFunction>" +
			"<DisplayToUserFunction>return true;</DisplayToUserFunction>" +
			"<ResetPerSession>True</ResetPerSession><Values><Value value=\"val1\" /></Values>" +
			"</Option>";

		[Test]
		public void The_Write_Method_Creates_The_Correct_XML()
		{
			ProjectSerialiserV1 serialiser = new ProjectSerialiserV1(fileController);
			
			StringBuilder sb = new StringBuilder();
			XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings{OmitXmlDeclaration = true});
			serialiser.WriteUserOptionXML(userOption, writer);
			writer.Close();

			string output = XmlSqueezer.RemoveWhitespaceBetweenElements(sb.ToString());
			Assert.That(output, Is.EqualTo(expectedXml));
		}
	}

	[TestFixture]
	public class Constructing_The_XML_For_The_UserOptions_Details
	{
		IFileController fileController;
		private UserOption userOption;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
            fileController.Stub(f => f.DirectoryExists("Folder")).Return(true);
            fileController.Stub(f => f.CanCreateFilesIn("Folder")).Return(true);

			userOption = new UserOption("var", "Gen", typeof(string), "test-text", "test-description", new string[0], "dv-body", typeof(bool), "validator body", "return true;", true);
		}

	    public const string ExpectedXml = "<UserOptionDetails version=\"1\"><Category name=\"Gen\"><UserOption varname=\"var\" /></Category></UserOptionDetails>";

        [Test]
        public void The_Create_Method_Writes_One_File()
        {
            IProjectSerialiser serialiser = new ProjectSerialiserV1(fileController);
            serialiser.CreateUserOptionDetailsFile(new[] { userOption }, "Folder");

            fileController.AssertWasCalled(f => f.WriteAllText(Arg<string>.Is.Equal("Folder\\UserOptions.xml"), Arg<string>.Is.NotNull));
        }

		[Test]
		public void The_Write_Method_Creates_The_Correct_XML()
		{
			ProjectSerialiserV1 serialiser = new ProjectSerialiserV1(fileController);

			StringBuilder sb = new StringBuilder();
			XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings { OmitXmlDeclaration = true });
			serialiser.WriteUserOptionDetailsXML(new[]{userOption}, writer);
			writer.Close();

			string output = XmlSqueezer.RemoveWhitespaceBetweenElements(sb.ToString());
			Assert.That(output, Is.EqualTo(ExpectedXml));
		}
	}
}