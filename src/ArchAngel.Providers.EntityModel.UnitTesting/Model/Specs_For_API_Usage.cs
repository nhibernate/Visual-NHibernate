using System.Linq;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_API_Usage
{
	[TestFixture]
	public class When_Working_With_Entities_And_Tables
	{
		private Entity entity;
		private ITable table;

		[SetUp]
		public void Setup()
		{
			EntitySet entitySet = new EntitySetImpl();
			entity = new EntityImpl("Entity1");
			entity.AddProperty(new PropertyImpl { Name = "Property1"});
			entitySet.AddEntity(entity);

			IDatabase database = new Database("DB1");
			table = new Table("Table");
			table.AddColumn(new Column("Column1"));
			database.AddEntity(table);

			Mapping mapping = new MappingImpl();
			mapping.AddPropertyAndColumn(entity.Properties.ElementAt(0), table.Columns[0]);

			MappingSet mappingSet = new MappingSetImpl(database, entitySet);
			mappingSet.AddMapping(mapping);
		}

		[Test]
		public void You_Should_Be_Able_To_Navigate_To_The_Mapped_Tables()
		{
			MappingSet ms = entity.EntitySet.MappingSet;
			var tablesFor = ms.GetMappedTablesFor(entity);

			Assert.That(tablesFor.Count(), Is.EqualTo(1), "Wrong number of entities returned.");
			Assert.That(tablesFor.ElementAt(0), Is.SameAs(table));
		}

		[Test]
		public void You_Should_Be_Able_To_Navigate_To_The_Mapped_Entities()
		{
			MappingSet ms = table.Database.MappingSet;
			var entitiesFor = ms.GetMappedEntitiesFor(table);

			Assert.That(entitiesFor.Count(), Is.EqualTo(1), "Wrong number of entities returned.");
			Assert.That(entitiesFor.ElementAt(0), Is.SameAs(entity));
		}
	}
}
