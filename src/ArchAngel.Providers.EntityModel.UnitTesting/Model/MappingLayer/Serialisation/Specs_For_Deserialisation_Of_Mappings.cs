using System;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System.Linq;
using Slyce.Common.ExtensionMethods;
using Specs_For_Serialisation_Of_Mapping;

namespace Specs_For_Deserialisation_Of_Mappings
{
	[TestFixture]
	public class When_Deserialising_A_Mapping_With_All_Fields_Set
	{
		public Table table;
		public EntityImpl entity1;
		public EntityImpl entity2;
		public Database database;
		public EntitySetImpl entitySet;

		[SetUp]
		public void Setup()
		{
			database = new Database("Db1");
			entitySet = new EntitySetImpl();

			table = new Table("Table1");
			table.AddColumn(new Column("Column1"));
			entity1 = new EntityImpl("Entity1");
			entity1.AddProperty(new PropertyImpl("Property1"));
			entity2 = new EntityImpl("Entity2");
			
			entity2.AddProperty(new PropertyImpl("Property2"));
			var reference = entity1.CreateReferenceTo(entity2);
			reference.Identifier = new Guid("11111111-1111-1111-1111-111111111111");
			reference.End1Name = "end1";
			reference.End2Name = "end2";
			entitySet.AddReference(reference);

			database.AddTable(table);
			entitySet.AddEntity(entity1);
		}

		[Test]
		public void It_Should_Deserialise_To_This()
		{
			var xml = When_Serialising_A_Mapping_With_All_Fields_Set.FullMappingXml;

			Mapping mapping = new MappingSetDeserialisationScheme().DeserialiseMapping(xml.GetXmlDocRoot(), database, entitySet);

			TestMapping(mapping, table, entity1);
		}

		public static void TestMapping(Mapping mapping, ITable table, Entity entity)
		{
			Assert.That(mapping.FromTable, Is.SameAs(table));
			Assert.That(mapping.ToEntity, Is.SameAs(entity));
			Assert.That(mapping.FromColumns.Count, Is.EqualTo(1));
			Assert.That(mapping.ToProperties.Count, Is.EqualTo(1));
			Assert.That(mapping.FromColumns[0], Is.SameAs(table.Columns[0]));
			Assert.That(mapping.ToProperties[0], Is.EqualTo(entity.Properties.ElementAt(0)));
		}
	}
}
