using System.Xml;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Specs_For_Serialisation_Of_A_Key;

namespace Specs_For_Deserialisation.Of_A_Key
{
	[TestFixture]
	public class When_Given_The_Specs_For_A_Key_With_Default_Settings
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			const string xml = When_Serialising_An_Empty_Key.BasicKeyXml;
			XmlDocument document = new XmlDocument();
			document.LoadXml(xml);

			IKey key = new DatabaseDeserialisationScheme().ProcessKeyNode(document.DocumentElement, new Table(), new Database(""));

			// All of the other key stuff is tested in the tests for IScriptObject
			Assert.That(key.Keytype, Is.EqualTo(DatabaseKeyType.Primary));
		}
	}

	[TestFixture]
	public class When_Given_The_Specs_For_A_Key_With_Everything_Set
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			const string xml = When_Serialising_Key_With_All_Information_Set.FullKeyXml;
			XmlDocument document = new XmlDocument();
			document.LoadXml(xml);

			IKey key = new DatabaseDeserialisationScheme().ProcessKeyNode(document.DocumentElement, new Table(), new Database(""));

			// All of the other key stuff is tested in the tests for IScriptObject
			Assert.That(key.Keytype, Is.EqualTo(DatabaseKeyType.Unique));
		}
	}

	[TestFixture]
	public class When_Given_The_Specs_For_A_Key_With_Columns
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			const string xml = When_Serialising_Key_With_Columns.KeyWithColumnsXml;
			XmlDocument document = new XmlDocument();
			document.LoadXml(xml);

			Table table1 = new Table("Table1");
			table1.AddColumn(new Column("Column1"));

			IKey key = new DatabaseDeserialisationScheme().ProcessKeyNode(document.DocumentElement, table1, new Database(""));

			Assert.That(key.Columns, Has.Count(1));
			Assert.That(key.Columns[0], Is.SameAs(table1.Columns[0]));
		}
	}

	[TestFixture]
	public class When_Given_The_Specs_For_A_Key_With_A_Referenced_Key
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			const string xml = When_Serialising_Key_With_A_Referenced_Key.KeyWithReferencedKeyXml;
			XmlDocument document = new XmlDocument();
			document.LoadXml(xml);
			
			Database db = new Database("Database1");
			Table table1 = new Table("Table1");
			Table table2 = new Table("Table2");
			db.AddTable(table1);
			db.AddTable(table2);

			IKey key2 = new Key("ForeignKey") { Parent = table2 };
			table2.AddKey(key2);

			var scheme = new DatabaseDeserialisationScheme();
			IKey key = scheme.ProcessKeyNode(document.DocumentElement, table1, db);
			scheme.ProcessReferencedKey(key, document.DocumentElement, db);

			Assert.That(key.ReferencedKey, Is.SameAs(key2));
		}
	}
}
