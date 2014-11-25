using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System.Linq;
using Slyce.Common.ExtensionMethods;
using Specs_For_Serialisation_Of_ComponentMappings;

namespace Specs_For_Deserialisation_Of_ComponentMappings
{
	[TestFixture]
	public class When_Deserialising_A_Mapping_With_All_Fields_Set
	{
		public Table table;
		public Component component1;
		public Component component2;
		public ComponentSpecification componentSpec;
		public Database database;
		public EntitySet entitySet;

		[SetUp]
		public void Setup()
		{
			database = new Database("Db1");
			entitySet = new EntitySetImpl();

			table = new Table("Table1");
			table.AddColumn(new Column("AddressStreet"));

			var entity1 = new EntityImpl("Entity1");

			componentSpec = new ComponentSpecificationImpl("Address");
			entitySet.AddComponentSpecification(componentSpec);
			componentSpec.AddProperty(new ComponentPropertyImpl("Street"));

			component1 = componentSpec.CreateImplementedComponentFor(entity1, "HomeAddress");
			component2 = componentSpec.CreateImplementedComponentFor(entity1, "WorkAddress");

			database.AddTable(table);
			entitySet.AddEntity(entity1);
		}

		[Test]
		public void It_Should_Deserialise_To_This()
		{
			var xml = When_Serialising_A_Mapping_With_All_Fields_Set.FullMappingXml;

			ComponentMapping mapping = new MappingSetDeserialisationScheme().DeserialiseComponentMapping(xml.GetXmlDocRoot(), database, entitySet);

			Assert.That(mapping.FromTable, Is.SameAs(table));
			Assert.That(mapping.ToComponent, Is.SameAs(component1));
			Assert.That(mapping.FromColumns.Count, Is.EqualTo(1));
			Assert.That(mapping.ToProperties.Count, Is.EqualTo(1));
			Assert.That(mapping.FromColumns[0], Is.SameAs(((ITable) table).Columns[0]));
			Assert.That(mapping.ToProperties[0], Is.EqualTo(component1.Properties.ElementAt(0)));
		}
	}
}
