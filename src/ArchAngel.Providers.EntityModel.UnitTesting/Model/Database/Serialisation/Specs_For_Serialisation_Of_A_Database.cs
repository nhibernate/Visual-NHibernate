using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;

namespace Specs_For_Serialisation_Of_A_Database
{
	[TestFixture]
	public class When_Serialising_An_Empty_Database
	{
		[Test]
		public void Should_Return_This()
		{
			const string expectedXML = @"<LogicalSchema Version=""1""><DatabaseName>Database1</DatabaseName></LogicalSchema>";

			Database db1 = new Database("Database1");
			
			string outputXml = db1.Serialise(new DatabaseSerialisationScheme());
			outputXml = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXml);

			Assert.That(outputXml, Is.EqualTo(expectedXML));
		}
	}

	[TestFixture]
	public class When_Serialising_A_Database_With_Tables
	{
		[Test]
		public void Should_Return_This()
		{
			const string expectedXML = @"<LogicalSchema Version=""1""><DatabaseName>Database1</DatabaseName>" +
						"<Table><Description /><Enabled>True</Enabled><IsUserDefined>False</IsUserDefined><Name>Table1</Name><UID>00000000-0000-0000-0000-000000000000</UID></Table>" +
						"<Table><Description /><Enabled>True</Enabled><IsUserDefined>False</IsUserDefined><Name>Table2</Name><UID>00000000-0000-0000-0000-000000000000</UID></Table>" +
						"</LogicalSchema>";

			Database db1 = new Database("Database1");
			db1.AddTable(new Table("Table1"));
			db1.AddTable(new Table("Table2"));

			string outputXml = db1.Serialise(new DatabaseSerialisationScheme());
			outputXml = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXml);

			Assert.That(outputXml, Is.EqualTo(expectedXML));
		}
	}
}
