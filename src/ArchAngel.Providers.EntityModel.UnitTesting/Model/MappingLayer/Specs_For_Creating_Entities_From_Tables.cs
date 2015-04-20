using ArchAngel.Providers.EntityModel.Controller.MappingLayer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using System.Linq;

namespace Creating_Entities_From_Tables
{
	[TestFixture]
	public class When_Constructing_A_One_To_One_Mapping
	{
		[Test]
		public void The_Correct_Entities_Are_Created()
		{
			MappingSet mappings = CreateMappingSet_OneTable();

			EntitySet entities = mappings.EntitySet;
			IDatabase database = mappings.Database;
			ITable table = database.Tables[0];

			Assert.That(entities.MappingSet, Is.SameAs(mappings));
			Assert.That(mappings.Database, Is.SameAs(database));
			Assert.That(entities, Is.Not.Null);
			Assert.That(entities, Has.Count(1));
			Assert.That(entities[0].EntitySet, Is.SameAs(entities));

			Assert.That(mappings.Mappings.Count, Is.EqualTo(1));

			Mapping mapping = mappings.Mappings[0];

			Assert.That(mapping.ToEntity, Is.SameAs(entities[0]));
			Assert.That(mapping.FromTable, Is.SameAs(table));

			Assert.That(mapping.ToProperties.Count(), Is.EqualTo(2));
			Assert.That(mapping.ToProperties[0], Is.SameAs(entities[0].Properties.ElementAt(0)));
			Assert.That(mapping.ToProperties[1], Is.SameAs(entities[0].Properties.ElementAt(1)));
			Assert.That(mapping.FromColumns.Count(), Is.EqualTo(2));
			Assert.That(mapping.FromColumns[0], Is.SameAs(table.Columns[0]));
			Assert.That(mapping.FromColumns[1], Is.SameAs(table.Columns[1]));
		}

		[Test]
		public void The_Correct_Entities_Are_Created2()
		{
			MappingSet mappings = CreateMappingSet_TwoTables();

			EntitySet entities = mappings.EntitySet;
			IDatabase database = mappings.Database;
			ITable table1 = database.Tables[0];
			ITable table2 = database.Tables[1];

			Assert.That(mappings.Database, Is.SameAs(database));
			Assert.That(entities, Is.Not.Null);
			Assert.That(entities, Has.Count(2));
			Assert.That(entities[0].EntitySet, Is.SameAs(entities));
			Assert.That(entities[1].EntitySet, Is.SameAs(entities));

			Assert.That(mappings.Mappings.Count, Is.EqualTo(2));

			var mapping0 = mappings.Mappings[0];
			Assert.That(mapping0.ToEntity, Is.SameAs(entities[0]));
			Assert.That(mapping0.ToProperties.Count() == 1);
			Assert.That(mapping0.ToProperties[0], Is.SameAs(entities[0].Properties.ElementAt(0)));
			Assert.That(mapping0.FromTable, Is.SameAs(table1));
			Assert.That(mapping0.FromColumns.Count() == 1);
			Assert.That(mapping0.FromColumns[0], Is.SameAs(table1.Columns[0]));

			var mapping1 = mappings.Mappings[1];
			Assert.That(mapping1.ToEntity, Is.SameAs(entities[1]));
			Assert.That(mapping1.ToProperties.Count() == 1);
			Assert.That(mapping1.ToProperties[0], Is.SameAs(entities[1].Properties.ElementAt(0)));
			Assert.That(mapping1.FromTable, Is.SameAs(table2));
			Assert.That(mapping1.FromColumns.Count() == 1);
			Assert.That(mapping1.FromColumns[0], Is.SameAs(table2.Columns[0]));
		}

		[Test]
		public void The_Mappings_Are_Setup_Correctly_OneTable()
		{
			MappingSet mappings = CreateMappingSet_OneTable();

			EntitySet entities = mappings.EntitySet;
			IDatabase database = mappings.Database;
			ITable table = database.Tables[0];
			Entity entity = entities.Entities[0];

			TestAllMappings(mappings, table, entity);
		}

		[Test]
		public void The_Mappings_Are_Setup_Correctly_TwoTables()
		{
			MappingSet mappings = CreateMappingSet_TwoTables();

			EntitySet entities = mappings.EntitySet;
			IDatabase database = mappings.Database;

			TestAllMappings(mappings, database.Tables[0], entities.Entities[0]);
			TestAllMappings(mappings, database.Tables[1], entities.Entities[1]);
		}

		private void TestAllMappings(MappingSet mappings, ITable table, Entity entity)
		{
			for (int i = 0; i < table.Columns.Count; i++)
			{
				Assert.That(mappings.GetMappedColumnFor(entity.Properties.ElementAt(i)), Is.SameAs(table.Columns[i]));
				Assert.That(mappings.GetMappedPropertiesFor(table.Columns.ElementAt(i)).ElementAt(0), Is.SameAs(entity.Properties.ElementAt(i)));
			}
		}

		private MappingSet CreateMappingSet_OneTable()
		{
			Database database = new Database("DB1");
			var table = new Table("Table1");

			table.AddColumn(new Column("Column1") { Datatype = "int", InPrimaryKey = true });
			table.AddColumn(new Column("Column2") { Datatype = "nvarchar", Size = 50 });

			database.AddTable(table);
			MappingProcessor processor = new MappingProcessor(new OneToOneEntityProcessor());
			return processor.CreateOneToOneMapping(database);
		}

		private MappingSet CreateMappingSet_TwoTables()
		{
			Database database = new Database("DB1");
			var table1 = new Table("Table1");
			table1.AddColumn(new Column("Column1") { Datatype = "int", InPrimaryKey = true });

			var table2 = new Table("Table2");
			table2.AddColumn(new Column("Column1") { Datatype = "int", InPrimaryKey = true });

			database.AddTable(table1);
			database.AddTable(table2);
			MappingProcessor processor = new MappingProcessor(new OneToOneEntityProcessor());
			return processor.CreateOneToOneMapping(database);
		}
	}

	[TestFixture]
	public class When_Constructing_A_Partial_One_To_One_Mapping
	{
		private Database Database;
		private Table Table1;
		private Table Table2;
		private Table Table3;
		private Relationship Relationship12;
		private Relationship Relationship13;
		private MappingSet Set;

		[SetUp]
		public void SetUp()
		{
			Table1 = new Table("Table1");
			Table2 = new Table("Table2");
			Table3 = new Table("Table3");

			Database = new Database("DB1");
			Database.AddEntity(Table1);
			Database.AddEntity(Table2);
			Database.AddEntity(Table3);

			Relationship12 = Table1.CreateRelationshipTo(Table2);
			Relationship13 = Table1.CreateRelationshipTo(Table3);

			Set = new MappingSetImpl(Database, new EntitySetImpl());
		}

		[Test]
		public void Correct_Entities_Are_Created()
		{
			EntityProcessor proc = MockRepository.GenerateMock<EntityProcessor>();

			Entity entity1 = new EntityImpl();

			proc.Stub(p => p.CreateEntity(Table1)).Return(entity1);

			MappingProcessor mappingProc = new MappingProcessor(proc);
			mappingProc.CreateOneToOneMappingsFor(new[] { Table1 }, Set);

			proc.AssertWasCalled(p => p.CreateEntity(Table1));
			proc.AssertWasNotCalled(p => p.CreateEntity(Table2));
		}

		[Test]
		public void Correct_References_Are_Created()
		{
			EntityProcessor proc = MockRepository.GenerateMock<EntityProcessor>();

			Entity entity1 = new EntityImpl();
			Entity entity2 = new EntityImpl();
			Reference reference = new ReferenceImpl(entity1, entity2);

			proc.Stub(p => p.CreateEntity(Table1)).Return(entity1);
			proc.Stub(p => p.CreateEntity(Table2)).Return(entity2);
			proc.Stub(p => p.CreateReference(Relationship12, Set.EntitySet)).Return(reference);

			MappingProcessor mappingProc = new MappingProcessor(proc);
			mappingProc.CreateOneToOneMappingsFor(new[] { Table1, Table2 }, Set);

			proc.AssertWasCalled(p => p.CreateEntity(Table1));
			proc.AssertWasCalled(p => p.CreateEntity(Table2));
			proc.AssertWasCalled(p => p.CreateReference(Relationship12, Set.EntitySet));
			proc.AssertWasNotCalled(p => p.CreateReference(Relationship13, Set.EntitySet));
		}
	}

	[TestFixture]
	public class When_Constructing_A_Property_From_A_Column
	{
		[Test]
		public void It_Sets_The_IsKeyProperty_Property()
		{
			IColumn column = new Column("column");
			column.InPrimaryKey = true;
			column.Datatype = "int";


			OneToOneEntityProcessor proc = new OneToOneEntityProcessor();
			var property = proc.CreateProperty(column);

			Assert.That(property.IsKeyProperty, Is.True);
		}
	}
}