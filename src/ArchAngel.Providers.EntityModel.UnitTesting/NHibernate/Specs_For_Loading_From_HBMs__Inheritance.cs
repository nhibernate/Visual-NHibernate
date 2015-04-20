using System.IO;
using System.Linq;
using ArchAngel.NHibernateHelper;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;

namespace NHibernate.Specs_For_Loading_From_HBMs__Inheritance
{
    [TestFixture]
    public class When_Given_An_HBM_For_TablePerClassHierarchy
    {
		private EntityLoader loader;
		protected MappingSet mappingSet;
		protected Database database;
		protected Table table;

    	[SetUp]
		public void SetUp()
		{
			loader = new EntityLoader(MockRepository.GenerateStub<IFileController>());

			database = new Database("DB1");
			table = new Table("Transport");
			table.AddColumn(new Column("ID") { Datatype = "int" });
			table.AddColumn(new Column("Discriminator") { Datatype = "char", Size = 1});
			table.AddColumn(new Column("Name") { Datatype = "varchar", Size = 100 });
			table.AddColumn(new Column("Bike_Code") { Datatype = "varchar", Size = 5 });
			database.AddEntity(table);

			// Call we are testing
			mappingSet = loader.GetEntities(new[] { Path.Combine("Resources", "TablePerClassHierarchy.hbm.xml") }, database);
		}

        [Test]
        public void It_Creates_Both_Entity_Objects()
        {
            var entities = mappingSet.EntitySet;

            Assert.That(entities.Entities, Has.Count(2));

            Assert.That(entities.GetEntity("Transport"), Is.Not.Null);
            Assert.That(entities.GetEntity("Bike"), Is.Not.Null);
        }

		[Test]
		public void It_Creates_The_ParentChild_Relationship()
		{
			var entities = mappingSet.EntitySet;
			var transport = entities.GetEntity("Transport");
			var bike = entities.GetEntity("Bike");

			Assert.That(transport.Children, Has.Member(bike));			
			Assert.That(bike.Parent, Is.SameAs(transport));
		}

		[Test]
		public void It_Creates_All_Of_The_Properties()
		{
			var transport = mappingSet.EntitySet.GetEntity("Transport");

			Assert.That(transport.GetProperty("ID"), Is.Not.Null);
			Assert.That(transport.GetProperty("Name"), Is.Not.Null);

			var bike = mappingSet.EntitySet.GetEntity("Bike");

			Assert.That(bike.GetProperty("ID"), Is.Not.Null);
			Assert.That(bike.GetProperty("ID"), Is.SameAs(transport.GetProperty("ID")));
			Assert.That(bike.GetProperty("Name"), Is.Not.Null);
			Assert.That(bike.GetProperty("Name"), Is.SameAs(transport.GetProperty("Name")));
			Assert.That(bike.GetProperty("Code"), Is.Not.Null);
		}

        [Test]
        public void It_Hooks_Up_The_Table_Mappings()
        {
            var entity = mappingSet.EntitySet.GetEntity("Transport");
        	var tables = entity.MappedTables();

        	Assert.That(tables.Count(), Is.EqualTo(1));
        	Assert.That(tables.First(), Is.SameAs(table));

			entity = mappingSet.EntitySet.GetEntity("Bike");
			tables = entity.MappedTables();

			Assert.That(tables.Count(), Is.EqualTo(1));
			Assert.That(tables.First(), Is.SameAs(table));
        }
    }

	[TestFixture]
	public class When_Given_An_HBM_For_TablePerSubClass
	{
		private EntityLoader loader;
		protected MappingSet mappingSet;
		protected Database database;
		protected Table tableTransport;
		protected Table tableBike;

		[SetUp]
		public void SetUp()
		{
			loader = new EntityLoader(MockRepository.GenerateStub<IFileController>());

			database = new Database("DB1");
			tableTransport = new Table("Transport");
			tableTransport.AddColumn(new Column("ID") { Datatype = "int" });
			tableTransport.AddColumn(new Column("Name") { Datatype = "varchar", Size = 100 });
			database.AddEntity(tableTransport);

			tableBike = new Table("Bike");
			tableBike.AddColumn(new Column("ID") { Datatype = "int" });
			tableBike.AddColumn(new Column("Code") { Datatype = "char", Size = 5 });
			database.AddEntity(tableBike);

			// Call we are testing
			mappingSet = loader.GetEntities(new[] { Path.Combine("Resources", "TablePerSubClass.hbm.xml") }, database);
		}

		[Test]
		public void It_Creates_Both_Entity_Objects()
		{
			var entities = mappingSet.EntitySet;

			Assert.That(entities.Entities, Has.Count(2));

			Assert.That(entities.GetEntity("Transport"), Is.Not.Null);
			Assert.That(entities.GetEntity("Bike"), Is.Not.Null);
		}

		[Test]
		public void It_Creates_The_ParentChild_Relationship()
		{
			var entities = mappingSet.EntitySet;
			var transport = entities.GetEntity("Transport");
			var bike = entities.GetEntity("Bike");

			Assert.That(transport.Children, Has.Member(bike));
			Assert.That(bike.Parent, Is.SameAs(transport));
		}

		[Test]
		public void It_Creates_All_Of_The_Properties()
		{
			var transport = mappingSet.EntitySet.GetEntity("Transport");

			Assert.That(transport.GetProperty("ID"), Is.Not.Null);
			Assert.That(transport.GetProperty("Name"), Is.Not.Null);

			var bike = mappingSet.EntitySet.GetEntity("Bike");

			Assert.That(bike.GetProperty("ID"), Is.Not.Null);
			Assert.That(bike.GetProperty("ID"), Is.SameAs(transport.GetProperty("ID")));
			Assert.That(bike.GetProperty("Name"), Is.Not.Null);
			Assert.That(bike.GetProperty("Name"), Is.SameAs(transport.GetProperty("Name")));
			Assert.That(bike.GetProperty("Code"), Is.Not.Null);
		}

		[Test]
		public void It_Hooks_Up_The_Table_Mappings()
		{
			var entity = mappingSet.EntitySet.GetEntity("Transport");
			var tables = entity.MappedTables();

			Assert.That(tables.Count(), Is.EqualTo(1));
			Assert.That(tables.First(), Is.SameAs(tableTransport));

			entity = mappingSet.EntitySet.GetEntity("Bike");
			tables = entity.MappedTables();

			Assert.That(tables.Count(), Is.EqualTo(1));
			Assert.That(tables.First(), Is.SameAs(tableBike));
		}
	}
}
