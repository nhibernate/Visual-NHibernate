using System.IO;
using System.Text;
using System.Xml;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Attributes;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;

namespace Specs_For_Project_Saving__ApiExtensions
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
			fileController.Stub(f => f.CanCreateFilesIn("Folder")).Return(true);
		}

		[Test]
        public void The_Create_Method_Creates_The_Folder()
		{
			IProjectSerialiser serialiser = new ProjectSerialiserV1(fileController);
			serialiser.CreateApiExtensionFiles(new ApiExtensionMethod[0], "Folder");

            fileController.AssertWasCalled(f => f.CreateDirectory("Folder"));
		}
	}

	[TestFixture]
	public class When_The_ApiExtensions_Folder_Isnt_Writable
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
			serialiser.CreateApiExtensionFiles(new ApiExtensionMethod[0], "Folder");
		}
	}

	[TestFixture]
	public class When_The_ApiExtensions_Folder_Does_Exist_But_No_UserOptions_Are_Given
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
			serialiser.CreateApiExtensionFiles(new ApiExtensionMethod[0], "Folder");
			
			fileController.AssertWasNotCalled(f => f.WriteAllText(null, null), c => c.IgnoreArguments());
			fileController.AssertWasNotCalled(f => f.WriteResourceToFile(null, null, null), c => c.IgnoreArguments());
			fileController.AssertWasNotCalled(f => f.WriteStreamToFile(null, null), c => c.IgnoreArguments());
		}
	}

	[TestFixture]
	public class When_The_ApiExtensions_Folder_Exists_And_One_UserOption_Is_Given
	{
		private IFileController fileController;
		private ApiExtensionMethod ExtensionMethod;

		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			fileController.Stub(f => f.DirectoryExists("Folder")).Return(true);
			fileController.Stub(f => f.CanCreateFilesIn("Folder")).Return(true);

			ExtensionMethod = new ApiExtensionMethod(typeof(ApiExtensionTest).GetMethod("Method1"));
		}

		[Test]
		public void The_Create_Method_Writes_One_File()
		{
			IProjectSerialiser serialiser = new ProjectSerialiserV1(fileController);
			serialiser.CreateApiExtensionFiles(new[] { ExtensionMethod }, "Folder");

			string expectedFilename = "Folder".PathSlash(ExtensionMethod.ExtendedMethod.DeclaringType.FullName + ".apiext.xml");

			fileController.AssertWasCalled(f => f.WriteAllText(Arg<string>.Is.Equal(expectedFilename), Arg<string>.Is.NotNull));
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
	public class Constructing_The_XML_For_ApiExtensions
	{
		IFileController fileController;
		private ApiExtensionMethod ExtensionMethod;
		
		[SetUp]
		public void SetUp()
		{
			fileController = MockRepository.GenerateMock<IFileController>();
			ExtensionMethod = new ApiExtensionMethod(typeof(ApiExtensionTest).GetMethod("Method1"));
			ExtensionMethod.OverridingFunctionBody = "return \"<>\";"; // Testing XML Escaping
		}

		private const string expectedXml =
			"<ApiExtensions version=\"1\" type=\"Specs_For_Project_Saving__ApiExtensions.ApiExtensionTest\">" +
			"<ApiExtension><MethodName>Method1</MethodName>" +
			"<OverrideFunctionText>return \"&lt;&gt;\";</OverrideFunctionText></ApiExtension>" +
			"</ApiExtensions>";

		[Test]
		public void The_Write_Method_Creates_The_Correct_XML()
		{
			ProjectSerialiserV1 serialiser = new ProjectSerialiserV1(fileController);
			
			StringBuilder sb = new StringBuilder();
			XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings{OmitXmlDeclaration = true});
			serialiser.WriteApiExtensionMethodXML(new []{ExtensionMethod}, typeof(ApiExtensionTest), writer);
			writer.Close();

			string output = XmlSqueezer.RemoveWhitespaceBetweenElements(sb.ToString());
			Assert.That(output, Is.EqualTo(expectedXml));
		}
	}
}