using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Specs_For_Serialisation_Of_A_Column;
using Specs_For_Serialisation_Of_A_Key;
using Specs_For_Serialisation_Of_A_Table;
using Specs_For_Serialisation_Of_An_Index;

namespace Specs_For_Deserialisation.Of_A_Database
{
	[TestFixture]
	public class When_Given_The_Specs_For_An_Empty_Database
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			const string xml = @"<LogicalSchema Version = ""1""><DatabaseName>Database1</DatabaseName></LogicalSchema>";

			IDatabase db = new DatabaseDeserialisationScheme().Deserialise(xml);

			Assert.That(db.Name, Is.EqualTo("Database1"));
		}
	}

	[TestFixture]
	public class When_Given_The_Specs_For_A_Database_With_Tables
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			const string xml = @"<LogicalSchema Version = ""1""><DatabaseName>Database1</DatabaseName>" +
				When_Serialising_An_Empty_Table.BasicTableXml +
				"</LogicalSchema>";

			IDatabase db = new DatabaseDeserialisationScheme().Deserialise(xml);

			Assert.That(db.Tables, Has.Count(1));
			// Make sure the correct table was loaded up.
			Assert.That(db.Tables[0].Name, Is.EqualTo("Entity1"));
		}
	}

	[TestFixture]
	public class When_Given_The_Specs_For_A_Database_With_Tables_And_Columns
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			const string xml =
			@"<LogicalSchema Version = ""1"">" +
				"<DatabaseName>Database1</DatabaseName>" +
				"<Table>" + When_Serialising_An_Empty_Table.ScriptBaseXml +
					"<Columns>" + When_Serialising_An_Empty_Column.BasicColumnXml + "</Columns>" +
				"</Table>" +
			"</LogicalSchema>";

			IDatabase db = new DatabaseDeserialisationScheme().Deserialise(xml);

			Assert.That(db.Tables, Has.Count(1));
			ITable table = db.Tables[0];
			Assert.That(table.Name, Is.EqualTo("Entity1"));
			Assert.That(table.Columns, Has.Count(1));
			Assert.That(table.Columns[0].Name, Is.EqualTo("Entity1"));
			Assert.That(table.Columns[0].Parent, Is.SameAs(table));
		}
	}

	[TestFixture]
	public class When_Given_The_Specs_For_A_Database_With_Tables_Keys_And_Indexes
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			const string xml = 
			@"<LogicalSchema Version = ""1"">" + 
				"<DatabaseName>Database1</DatabaseName>" +
				"<Table>" + When_Serialising_An_Empty_Table.ScriptBaseXml +
					"<Keys>" + When_Serialising_An_Empty_Key.BasicKeyXml + "</Keys>" +	
					"<Indexes>" + When_Serialising_An_Empty_Index.BasicIndexXml + "</Indexes>" +
				"</Table>"+
			"</LogicalSchema>";

			IDatabase db = new DatabaseDeserialisationScheme().Deserialise(xml);

			Assert.That(db.Tables, Has.Count(1));
			ITable table = db.Tables[0];
			Assert.That(table.Name, Is.EqualTo("Entity1"));
			Assert.That(table.Keys, Has.Count(1));
			Assert.That(table.Keys[0].Name, Is.EqualTo("PrimaryKey"));
			Assert.That(table.Keys[0].Parent, Is.SameAs(table));
			Assert.That(table.Indexes, Has.Count(1));
			Assert.That(table.Indexes, Has.Count(1));
			Assert.That(table.Indexes[0].Name, Is.EqualTo("Index1"));
			Assert.That(table.Indexes[0].Parent, Is.SameAs(table));
		}
	}

	[TestFixture]
	public class When_Given_The_Specs_For_A_Database_With_Foreign_Keys
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			#region Xml
			const string xml =
@"<LogicalSchema Version = ""1"">
<DatabaseName>Database1</DatabaseName>
<Table>
    <Name>Table1</Name>
    <NamePlural />
    <Description />
    <Enabled>False</Enabled>
    <IsUserDefined>False</IsUserDefined>
    <UID>00000000-0000-0000-0000-000000000000</UID>
    <Keys>
      <Key>
        <Name>FK_Table1</Name>
        <NamePlural />
        <Description />
        <Enabled>False</Enabled>
        <IsUserDefined>False</IsUserDefined>
        <UID>00000000-0000-0000-0000-000000000000</UID>
        <IsUnique>False</IsUnique>
        <Keytype>Foreign</Keytype>
        <ReferencedKey>
          <TableName>Table2</TableName>
          <KeyName>PK_Table2</KeyName>
        </ReferencedKey>
        <Columns>
          <ColumnName>ColumnT11</ColumnName>
        </Columns>
      </Key>
    </Keys>
    <Columns>
      <Column>
        <Name>ColumnT11</Name>
        <NamePlural />
        <Description />
        <Enabled>False</Enabled>
        <IsUserDefined>False</IsUserDefined>
        <UID>00000000-0000-0000-0000-000000000000</UID>
        <Datatype>int</Datatype>
        <Default />
        <InPrimaryKey>False</InPrimaryKey>
        <IsCalculated>False</IsCalculated>
        <IsComputed>False</IsComputed>
        <IsIdentity>False</IsIdentity>
        <IsNullable>True</IsNullable>
        <IsReadOnly>False</IsReadOnly>
        <IsUnique>True</IsUnique>
        <OrdinalPosition>3</OrdinalPosition>
        <Precision>10</Precision>
        <Scale>0</Scale>
        <Size>0</Size>
      </Column>
    </Columns>
  </Table>
  <Table>
    <Name>Table2</Name>
    <NamePlural />
    <Description />
    <Enabled>False</Enabled>
    <IsUserDefined>False</IsUserDefined>
    <UID>00000000-0000-0000-0000-000000000000</UID>
    <Keys>
      <Key>
        <Name>PK_Table2</Name>
        <NamePlural />
        <Description />
        <Enabled>False</Enabled>
        <IsUserDefined>False</IsUserDefined>
        <UID>00000000-0000-0000-0000-000000000000</UID>
        <IsUnique>True</IsUnique>
        <Keytype>Primary</Keytype>
        <Columns>
          <ColumnName>Column1</ColumnName>
        </Columns>
      </Key>
    </Keys>
    <Columns>
      <Column>
        <Name>Column1</Name>
        <NamePlural />
        <Description />
        <Enabled>False</Enabled>
        <IsUserDefined>False</IsUserDefined>
        <UID>00000000-0000-0000-0000-000000000000</UID>
        <Datatype>int</Datatype>
        <Default />
        <InPrimaryKey>True</InPrimaryKey>
        <IsCalculated>False</IsCalculated>
        <IsComputed>False</IsComputed>
        <IsIdentity>False</IsIdentity>
        <IsNullable>False</IsNullable>
        <IsReadOnly>False</IsReadOnly>
        <IsUnique>True</IsUnique>
        <OrdinalPosition>1</OrdinalPosition>
        <Precision>10</Precision>
        <Scale>0</Scale>
        <Size>0</Size>
      </Column>
    </Columns>
  </Table>
</LogicalSchema>";
			#endregion
			IDatabase db = new DatabaseDeserialisationScheme().Deserialise(xml);

			Assert.That(db.Tables, Has.Count(2));

			ITable table1 = db.Tables[0];
			Assert.That(table1.Name, Is.EqualTo("Table1"));
			Assert.That(table1.Keys, Has.Count(1));
			Assert.That(table1.Keys[0].Name, Is.EqualTo("FK_Table1"));
			Assert.That(table1.Keys[0].Keytype, Is.EqualTo(DatabaseKeyType.Foreign));

			ITable table2 = db.Tables[1];
			Assert.That(table2.Name, Is.EqualTo("Table2"));
			Assert.That(table2.Keys, Has.Count(1));
			Assert.That(table2.Keys[0].Name, Is.EqualTo("PK_Table2"));
			Assert.That(table2.Keys[0].Keytype, Is.EqualTo(DatabaseKeyType.Primary));

			Assert.That(table1.Keys[0].ReferencedKey, Is.SameAs(table2.Keys[0]));
		}
	}
}
