using System.IO;
using System.Xml;
using ArchAngel.NHibernateHelper;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;

namespace NHibernate.Specs_For_Loading_From_An_NHibernate_CSProj_File
{
	[TestFixture]
	public class When_Given_A_Valid_CSProj_File
	{
		private ProjectLoader loader;
		private string csprojPath;
		private XmlDocument doc;
		private XmlNamespaceManager nsManager;
		private const string MSB_NAMESPACE = "http://schemas.microsoft.com/developer/msbuild/2003";

		[SetUp]
		public void Setup()
		{
			var csprojText = File.ReadAllText(Path.Combine("Resources", "NHProject.txt"));
			csprojPath = @"C:\test.csproj";

			doc = new XmlDocument();
			doc.LoadXml(csprojText);

			nsManager = new XmlNamespaceManager(doc.NameTable);
			nsManager.AddNamespace("msb", MSB_NAMESPACE);

			var fileController = MockRepository.GenerateMock<IFileController>();
			fileController
				.Stub(f => f.ReadAllText(csprojPath))
				.Return(csprojText);
			fileController
				.Stub(f => f.ToAbsolutePath(@"Model\Mappings\Category.hbm.xml", csprojPath))
				.Return(@"C:\Model\Mappings\Category.hbm.xml");
			fileController
				.Stub(f => f.ToAbsolutePath(@"Model\Mappings\CustomerDemographic.hbm.xml", csprojPath))
				.Return(@"C:\Model\Mappings\CustomerDemographic.hbm.xml");
			fileController
				.Stub(f => f.ToAbsolutePath(@"Model\Mappings\Customer.hbm.xml", csprojPath))
				.Return(@"C:\Model\Mappings\Customer.hbm.xml");
			
			fileController
				.Stub(f => f.ToAbsolutePath(@"hibernate.cfg.xml", csprojPath))
				.Return(@"C:\hibernate.cfg.xml");

			fileController
				.Stub(f => f.ToAbsolutePath(@"Model\Category.cs", csprojPath))
				.Return(@"C:\Model\Category.cs");
			fileController
				.Stub(f => f.ToAbsolutePath(@"Model\CustomerDemographic.cs", csprojPath))
				.Return(@"C:\Model\CustomerDemographic.cs");
			fileController
				.Stub(f => f.ToAbsolutePath(@"Model\Customer.cs", csprojPath))
				.Return(@"C:\Model\Customer.cs");

			loader = new ProjectLoader(fileController);
		}

		[Test]
		public void It_Returns_All_Of_The_HBMs_From_It()
		{
			var files = loader.GetHBMFilesFromCSProj(doc, csprojPath);

			Assert.That(files, Is.EquivalentTo(new[] 
									{
										@"C:\Model\Mappings\Category.hbm.xml",
										@"C:\Model\Mappings\CustomerDemographic.hbm.xml",
										@"C:\Model\Mappings\Customer.hbm.xml"
			                   		}));
		}

		[Test]
		public void It_Returns_The_NHConfig_From_It()
		{
			var files = loader.GetPossibleNHibernateConfigFilesFromCSProj(doc, csprojPath);

			Assert.That(files, Is.EquivalentTo(new[] { @"C:\hibernate.cfg.xml" }));
		}

		[Test]
		public void It_Returns_All_Of_The_CSharp_Files_From_It()
		{
			var files = loader.GetCSharpFilesFromCSProj(doc, csprojPath);

			Assert.That(files, Is.EquivalentTo(new[] 
									{
										@"C:\Model\Category.cs",
										@"C:\Model\CustomerDemographic.cs",
										@"C:\Model\Customer.cs"
			                   		}));
		}
	}
}
