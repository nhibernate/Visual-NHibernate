using System.Xml;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Specs_For_Serialisation_Of_A_Table;

namespace Specs_For_Deserialisation.Of_A_Table
{
	[TestFixture]
	public class When_Given_The_Specs_For_A_Basic_Table
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			const string xml = When_Serialising_An_Empty_Table.BasicTableXml;
			XmlDocument document = new XmlDocument();
			document.LoadXml(xml);

			ITable table = new DatabaseDeserialisationScheme().ProcessTableNode(document.DocumentElement);

			// All of the table stuff is tested in the tests for IScriptObject
			Assert.That(table.Name, Is.EqualTo("Entity1"));
		}
	}
}
