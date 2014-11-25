using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;
using Specs_For_Serialisation_Of_A_Column;
using Specs_For_Serialisation_Of_A_Key;
using Specs_For_Serialisation_Of_An_Index;

namespace Specs_For_Serialisation_Of_A_Table
{
	[TestFixture]
	public class When_Serialising_An_Empty_Table
	{
		public const string BasicTableXml =	"<Table>" + ScriptBaseXml + "</Table>";
		public const string ScriptBaseXml =
			"<Description /><Enabled>True</Enabled><IsUserDefined>False</IsUserDefined><Name>Entity1</Name><UID>00000000-0000-0000-0000-000000000000</UID>";

		[Test]
		public void Should_Return_This()
		{
			const string expectedXML = BasicTableXml;
			Table table = new Table("Entity1");

			string outputXML = table.Serialise(new DatabaseSerialisationScheme());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}

	[TestFixture]
	public class When_Serialising_A_Table_With_Keys
	{
		[Test]
		public void Should_Return_This()
		{
			const string expectedXML =	"<Table>" + When_Serialising_An_Empty_Table.ScriptBaseXml + 
										"<Keys>" + When_Serialising_An_Empty_Key.BasicKeyXml + "</Keys>" +
										"</Table>";
			Table table = new Table("Entity1");
			table.AddKey(new Key("PrimaryKey"));

			string outputXML = table.Serialise(new DatabaseSerialisationScheme());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}

	[TestFixture]
	public class When_Serialising_A_Table_With_Indexes
	{
		[Test]
		public void Should_Return_This()
		{
			const string expectedXML = @"<Table>" + When_Serialising_An_Empty_Table.ScriptBaseXml +
									   @"<Indexes>" + When_Serialising_An_Empty_Index.BasicIndexXml + @"</Indexes>" +
			                           @"</Table>";
			Table table = new Table("Entity1");
			table.AddIndex(new Index("Index1"));

			string outputXML = table.Serialise(new DatabaseSerialisationScheme());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}

	[TestFixture]
	public class When_Serialising_A_Table_With_Columns
	{
		[Test]
		public void Should_Return_This()
		{
			const string expectedXML = @"<Table>" + When_Serialising_An_Empty_Table.ScriptBaseXml +
									   @"<Columns>" + When_Serialising_An_Empty_Column.BasicColumnXml + @"</Columns>" +
									   @"</Table>";
			Table table = new Table("Entity1");
			table.AddColumn(new Column("Entity1"));

			string outputXML = table.Serialise(new DatabaseSerialisationScheme());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}
}
