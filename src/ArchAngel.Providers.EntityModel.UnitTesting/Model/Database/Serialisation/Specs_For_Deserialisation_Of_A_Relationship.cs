using System;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common.ExtensionMethods;

namespace Specs_For_Deserialisation.Of_A_Relationship
{
	[TestFixture]
	public class When_Given_The_XML_For_A_Relationship_With_Everything_Filled
	{
		public const string FullRelationshipXml = @"<Relationship identifier=""11111111-1111-1111-1111-111111111111""><PrimaryTable>Table1</PrimaryTable><ForeignTable>Table2</ForeignTable><PrimaryKey>K1</PrimaryKey><ForeignKey>ForeignKey</ForeignKey><Name>Relation1</Name></Relationship>";

		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			IDatabase database = new Database("DB1");
			var table1 = new Table("Table1");
			var table2 = new Table("Table2");
			var key1 = new Key("K1", DatabaseKeyType.Primary);
			var key2 = new Key("ForeignKey", DatabaseKeyType.Foreign);

			table1.AddKey(key1);
			table2.AddKey(key2);

			database.AddTable(table1);
			database.AddTable(table2);

			Relationship rel = new DatabaseDeserialisationScheme().DeserialiseRelationship(FullRelationshipXml.GetXmlDocRoot(), database);

			Assert.That(rel.Identifier, Is.EqualTo(new Guid("11111111-1111-1111-1111-111111111111")));
			Assert.That(rel.Name, Is.EqualTo("Relation1"));
			Assert.That(rel.PrimaryTable, Is.SameAs(table1));
			Assert.That(rel.ForeignTable, Is.SameAs(table2));
			Assert.That(rel.PrimaryKey, Is.SameAs(key1));
			Assert.That(rel.ForeignKey, Is.SameAs(key2));
		}
	}
}
