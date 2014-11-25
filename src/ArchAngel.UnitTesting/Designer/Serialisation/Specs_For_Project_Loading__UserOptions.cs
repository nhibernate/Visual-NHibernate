using System.Collections.Generic;
using System.IO;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Common.DesignerProject;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;
using Slyce.Common.ExtensionMethods;
using Specs_For_Project_Saving__UserOptions;

namespace Specs_For_Project_Loading__UserOptions
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
			fileController.Stub(f => f.CanReadFilesFrom("Folder")).Return(false);
		}

		[Test]
		[ExpectedException(typeof(DirectoryNotFoundException))]
		public void The_Load_Method_Throws_An_Exception()
		{
			IProjectDeserialiser deserialiser = new ProjectDeserialiserV1(fileController);
			deserialiser.LoadUserOptionFiles("Folder");
		}
	}

	[TestFixture]
	public class When_The_UserOptions_Folder_Exists_But_Has_No_UserOption_Files
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
            loader.LoadUserOptionsDetails(new UserOption[0], "Folder");

            fileController.AssertWasCalled(f => f.ReadAllText("Folder\\UserOptions.xml"));
        }

		[Test]
		public void The_Load_Method_Creates_Nothing()
		{
			IProjectDeserialiser loader = new ProjectDeserialiserV1(fileController);
            IEnumerable<UserOption> optionFiles = loader.LoadUserOptionFiles("Folder");
			
			Assert.That(optionFiles, Is.Empty);
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
            fileController.Stub(f => f.FindAllFilesLike("Folder", "*.useroption.xml")).Return(new[] { "Folder\\var.useroption.xml" });
            fileController.Stub(f => f.ReadAllText("Folder\\var.useroption.xml")).Return(expectedXml);
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
        public void The_Load_Method_Reads_One_File()
        {
            IProjectDeserialiser loader = new ProjectDeserialiserV1(fileController);
            loader.LoadUserOptionFiles("Folder");

            fileController.AssertWasCalled(f => f.ReadAllText(Arg<string>.Is.Equal("Folder\\var.useroption.xml")));
        }

		[Test]
		public void The_Load_Method_Creates_The_Correct_UserOption()
		{
			ProjectDeserialiserV1 deserialiser = new ProjectDeserialiserV1(fileController);
            var option = deserialiser.ReadUserOption(expectedXml.GetXmlDocRoot());
		    
			Assert.That(option.VariableName, Is.EqualTo("var"));
			Assert.That(option.VarType, Is.EqualTo(typeof(string)));
			Assert.That(option.Text, Is.EqualTo("test-text"));
			Assert.That(option.Description, Is.EqualTo("test-description"));
			Assert.That(option.Category, Is.EqualTo(""), "No category should be loaded at this level, see LoadUserOptionsDetails");
			Assert.That(option.DefaultValueFunctionBody, Is.EqualTo("dv-body"));
			Assert.That(option.IteratorType, Is.EqualTo(typeof(bool)));
			Assert.That(option.ValidatorFunctionBody, Is.EqualTo("validator body"));
			Assert.That(option.DisplayToUserFunctionBody, Is.EqualTo("return true;"));
			Assert.That(option.ResetPerSession, Is.True);
		}

		[Test]
		public void The_Values_Are_Loaded_Properly()
		{
			ProjectDeserialiserV1 deserialiser = new ProjectDeserialiserV1(fileController);
            var option = deserialiser.ReadUserOption(expectedXml.GetXmlDocRoot());

			Assert.That(option.Values, Has.Count(1));
			Assert.That(option.Values[0], Is.EqualTo("val1"));
		}
	}

    [TestFixture]
	public class Loading_Details_From_XML
    {
		IFileController fileController;

		private const string expectedXml = "<UserOptionDetails version=\"1\"><Category name=\"Gen\"><UserOption varname=\"var\" /></Category></UserOptionDetails>";

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
		}

		[Test]
		public void The_Categories_Are_Loaded_Properly()
		{
			UserOption[] userOptions = new[]
			                                      	{
			                                      		new UserOption {VariableName = "var"},
			                                      		new UserOption {VariableName = "Var2"}
			                                      	};

			ProjectDeserialiserV1 deserialiser = new ProjectDeserialiserV1(fileController);
            deserialiser.ReadUserOptionDetails(userOptions, expectedXml.GetXmlDocRoot());

			Assert.That(userOptions[0].Category, Is.EqualTo("Gen"), "The UserOption category was not loaded.");
			Assert.That(userOptions[1].Category, Is.EqualTo("General"), "The default UserOption category is wrong.");
		}
    }
}