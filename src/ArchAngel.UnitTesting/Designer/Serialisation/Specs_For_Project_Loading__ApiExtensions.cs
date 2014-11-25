using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArchAngel.Common.DesignerProject;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Attributes;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;
using Slyce.Common.ExtensionMethods;

namespace Specs_For_Project_Loading__ApiExtensions
{
	[TestFixture]
	public class When_The_ApiExtensions_Folder_Doesnt_Exist
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
			deserialiser.LoadApiExtensionFiles("Folder");
		}
	}

	[TestFixture]
	public class When_The_ApiExtensions_Folder_Exists_But_Has_No_Functions_Files
	{
		IFileController fileController;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			fileController.Stub(f => f.DirectoryExists("Folder")).Return(true);
            fileController.Stub(f => f.CanReadFilesFrom("Folder")).Return(true);
			fileController.Stub(f => f.FindAllFilesLike("Folder", "*.apiext.xml")).Return(new string[0]);
		}

		[Test]
		public void The_Load_Method_Creates_Nothing()
		{
			IProjectDeserialiser loader = new ProjectDeserialiserV1(fileController);
            IEnumerable<ApiExtensionMethod> apiExtensionMethods = loader.LoadApiExtensionFiles("Folder");
			
			Assert.That(apiExtensionMethods, Is.Empty);
		}
	}

	internal class ApiExtensionTest
	{
		[ApiExtension("ApiClass", "return \"\";")]
		public string Method1()
		{
			object retVal;
			if (ApiExtensionHelper.RunIfExtended(GetType(), "Method1", out retVal))
				return (string)retVal;
			return "";
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
			fileController.Stub(f => f.FindAllFilesLike("Folder", "*.apiext.xml")).Return(new[] { "Folder\\Specs_For_Project_Loading__ApiExtensions.ApiExtensionTest.apiext.xml" });
			fileController.Stub(f => f.ReadAllText("Folder\\Specs_For_Project_Loading__ApiExtensions.ApiExtensionTest.apiext.xml")).Return(ExpectedXml);
		}

		private const string ExpectedXml =
			"<ApiExtensions version=\"1\" type=\"Specs_For_Project_Loading__ApiExtensions.ApiExtensionTest\">" +
			"<ApiExtension><MethodName>Method1</MethodName>" +
			"<HasOverride>True</HasOverride><OverrideFunctionText>return \"&lt;&gt;\";</OverrideFunctionText></ApiExtension>" +
			"</ApiExtensions>";

        [Test]
        public void The_Load_Method_Reads_One_File()
        {
            IProjectDeserialiser loader = new ProjectDeserialiserV1(fileController);
            loader.LoadApiExtensionFiles("Folder");

			fileController.AssertWasCalled(f => f.ReadAllText(Arg<string>.Is.Equal("Folder\\Specs_For_Project_Loading__ApiExtensions.ApiExtensionTest.apiext.xml")));
        }

		[Test]
		public void The_Load_Method_Creates_The_Correct_UserOption()
		{
			ProjectDeserialiserV1 deserialiser = new ProjectDeserialiserV1(fileController);
            var options = deserialiser.ReadApiExtensions(ExpectedXml.GetXmlDocRoot());
			Assert.That(options.Count(), Is.EqualTo(1), "Should only load one ApiExtension");

			var option = options.ElementAt(0);
			Assert.That(option.HasOverride, Is.True);
			Assert.That(option.ExtendedMethod.DeclaringType.Name, Is.EqualTo("ApiExtensionTest"));
			Assert.That(option.ExtendedMethod.Name, Is.EqualTo("Method1"));
			Assert.That(option.OverridingFunctionBody, Is.EqualTo("return \"<>\";"));
		}
	}
}