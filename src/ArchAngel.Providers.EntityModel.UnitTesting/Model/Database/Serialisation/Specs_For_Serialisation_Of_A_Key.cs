using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;
using Specs_For_Serialisation_Of_A_Table;

namespace Specs_For_Serialisation_Of_A_Key
{
	[TestFixture]
	public class When_Serialising_An_Empty_Key
	{
		public const string BasicKeyXml = @"<Key><Description /><Enabled>True</Enabled><IsUserDefined>False</IsUserDefined><Name>PrimaryKey</Name><UID>00000000-0000-0000-0000-000000000000</UID><Keytype>Primary</Keytype></Key>";

		[Test]
		public void Should_Return_This()
		{
			const string expectedXML = BasicKeyXml;

			Key key = new Key("PrimaryKey");

			string outputXML = key.Serialise(new DatabaseSerialisationScheme());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}

	[TestFixture]
	public class When_Serialising_Key_With_All_Information_Set
	{
		public const string FullKeyXml = @"<Key><Description /><Enabled>True</Enabled><IsUserDefined>False</IsUserDefined><Name>PrimaryKey</Name><UID>00000000-0000-0000-0000-000000000000</UID><Keytype>Unique</Keytype></Key>";

		[Test]
		public void Should_Return_This()
		{
			Key key = new Key("PrimaryKey");
			key.Keytype = DatabaseKeyType.Unique;

			string outputXML = key.Serialise(new DatabaseSerialisationScheme());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(FullKeyXml));
		}
	}

	[TestFixture]
	public class When_Serialising_Key_With_Columns
	{
		public const string KeyWithColumnsXml = @"<Key><Description /><Enabled>True</Enabled><IsUserDefined>False</IsUserDefined><Name>PrimaryKey</Name><UID>00000000-0000-0000-0000-000000000000</UID><Keytype>Primary</Keytype><Columns><ColumnName>Column1</ColumnName></Columns></Key>";

		[Test]
		public void Should_Return_This()
		{
			const string expectedXML = KeyWithColumnsXml;

			Table table1 = new Table("Table1");
			table1.AddColumn(new Column("Column1"));
			Key key = new Key("PrimaryKey");
			key.Parent = table1;
			key.AddColumn("Column1");

			string outputXML = key.Serialise(new DatabaseSerialisationScheme());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}

	[TestFixture]
	public class When_Serialising_Key_With_A_Referenced_Key
	{
		public const string KeyWithReferencedKeyXml = @"<Key>" +When_Serialising_An_Empty_Table.ScriptBaseXml+ @"<Keytype>Primary</Keytype><ReferencedKey><KeyName>ForeignKey</KeyName><TableName>Table2</TableName></ReferencedKey></Key>";

		[Test]
		public void Should_Return_This()
		{
			const string expectedXML = KeyWithReferencedKeyXml;

			Table table1 = new Table("Table1");
			table1.AddColumn(new Column("Column1"));
			Key key = new Key("Entity1");
			Table table2 = new Table("Table2");
			key.ReferencedKey = new Key("ForeignKey") { Parent = table2 };

			string outputXML = key.Serialise(new DatabaseSerialisationScheme());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}
}
