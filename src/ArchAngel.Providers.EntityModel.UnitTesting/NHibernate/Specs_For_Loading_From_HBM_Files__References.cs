using System.IO;
using System.Linq;
using ArchAngel.Interfaces;
using ArchAngel.NHibernateHelper;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;

namespace NHibernate.Specs_For_Loading_From_HBMs__References
{
    [TestFixture]
    public class When_Given_A_Basic_HBM_For_Two_Entities_With_A_OneToMany_Relationship : RelationshipTestBase
    {
		[SetUp]
		public void SetUp()
		{
			SetupBase("two-related-entities.hbm.xml");
		}

        [Test]
        public void It_Creates_Both_Entity_Objects()
        {
            var entities = mappingSet.EntitySet;

            Assert.That(entities.Entities, Has.Count(2));

            Assert.That(entities.GetEntity("BasicClass1"), Is.Not.Null);
            Assert.That(entities.GetEntity("BasicClass2"), Is.Not.Null);
        }

        [Test]
        public void It_Creates_The_Right_Relationship()
        {
            var entity1 = mappingSet.EntitySet.GetEntity("BasicClass1");
            var entity2 = mappingSet.EntitySet.GetEntity("BasicClass2");

            Assert.That(entity1.References, Has.Count(1));
            Assert.That(entity2.References, Has.Count(1));

            Assert.That(entity1.References[0], Is.SameAs(entity2.References[0]));

            DirectedReference reference = entity1.DirectedReferences.First();
            Assert.That(reference.FromName, Is.EqualTo("Class2s"));
            Assert.That(reference.ToName, Is.EqualTo("Property1"));
			Assert.That(reference.FromEndCardinality, Is.EqualTo(Cardinality.Many));
			Assert.That(reference.ToEndCardinality, Is.EqualTo(Cardinality.One));
			Assert.That(reference.FromEndEnabled, Is.True);
			Assert.That(reference.ToEndEnabled, Is.True);
        }

        [Test]
        public void It_Hooks_Up_The_Reference_Mappings()
        {
            var entity = mappingSet.EntitySet.GetEntity("BasicClass1");
            var reference = entity.References[0];

            Assert.That(reference.MappedRelationship(), Is.SameAs(relationship));
        }
    }

	public abstract class RelationshipTestBase
	{
		private EntityLoader loader;
		protected MappingSet mappingSet;
		protected Database database;
		protected Table table1;
		protected Table table2;
		protected Table tableManyToMany;
		protected Relationship relationship;

		protected void SetupBase(string resourcePath)
		{
			loader = new EntityLoader(MockRepository.GenerateStub<IFileController>());

			database = new Database("DB1");
			table1 = new Table("Table1");
			table1.AddColumn(new Column("ID") { Datatype = "int" });
			table1.AddColumn(new Column("BasicClass2_Index") { Datatype = "int" });
			database.AddEntity(table1);

			table2 = new Table("Table2");
			table2.AddColumn(new Column("ID") { Datatype = "int" });
			table2.AddColumn(new Column("BASIC_CLASS_1_ID"));
			database.AddEntity(table2);

			tableManyToMany = new Table("Class1Class2");
			tableManyToMany.AddColumn(new Column("Class1ID") { Datatype = "int" });
			tableManyToMany.AddColumn(new Column("Class2ID") { Datatype = "int" });
			database.AddTable(tableManyToMany);

			relationship = table1.CreateRelationshipTo(table2);
			relationship.PrimaryKey.AddColumn("ID");
			relationship.ForeignKey.AddColumn("BASIC_CLASS_1_ID");

			// Call we are testing
			mappingSet = loader.GetEntities(new[] { Path.Combine("Resources", resourcePath) }, database);
		}
	}

	[TestFixture]
	public class When_Given_A_Basic_HBM_For_Two_Entities_With_A_OneToOne_Relationship : RelationshipTestBase
	{
		[SetUp]
		public void SetUp()
		{
			SetupBase("two-related-entities-1To1.hbm.xml");
		}

		[Test]
		public void It_Creates_Both_Entity_Objects()
		{
			var entities = mappingSet.EntitySet;

			Assert.That(entities.Entities, Has.Count(2));

			Assert.That(entities.GetEntity("BasicClass1"), Is.Not.Null);
			Assert.That(entities.GetEntity("BasicClass2"), Is.Not.Null);
		}

		[Test]
		public void It_Creates_The_Right_Relationship()
		{
			var entity1 = mappingSet.EntitySet.GetEntity("BasicClass1");
			var entity2 = mappingSet.EntitySet.GetEntity("BasicClass2");

			Assert.That(entity1.References, Has.Count(1));
			Assert.That(entity2.References, Has.Count(1));

			Assert.That(entity1.References[0], Is.SameAs(entity2.References[0]));

			DirectedReference reference = entity1.DirectedReferences.First();
			Assert.That(reference.FromName, Is.EqualTo("Property2"));
			Assert.That(reference.ToName, Is.EqualTo("Property1"));
			Assert.That(reference.FromEndCardinality, Is.EqualTo(Cardinality.One));
			Assert.That(reference.ToEndCardinality, Is.EqualTo(Cardinality.One));
			Assert.That(reference.FromEndEnabled, Is.True);
			Assert.That(reference.ToEndEnabled, Is.True);
		}

		[Test]
		public void It_Hooks_Up_The_Reference_Mappings()
		{
			var entity = mappingSet.EntitySet.GetEntity("BasicClass1");
			var reference = entity.References[0];

			Assert.That(reference.MappedRelationship(), Is.SameAs(relationship));
		}
	}

	[TestFixture]
	public class When_Given_A_Basic_HBM_For_Two_Entities_With_A_ManyToMany_Relationship : RelationshipTestBase
	{
		[SetUp]
		public void SetUp()
		{
			SetupBase("two-related-entities-MToM.hbm.xml");
		}

		[Test]
		public void It_Creates_Both_Entity_Objects()
		{
			var entities = mappingSet.EntitySet;

			Assert.That(entities.Entities, Has.Count(2));

			Assert.That(entities.GetEntity("BasicClass1"), Is.Not.Null);
			Assert.That(entities.GetEntity("BasicClass2"), Is.Not.Null);
		}

		[Test]
		public void It_Creates_The_Right_Relationship()
		{
			var entity1 = mappingSet.EntitySet.GetEntity("BasicClass1");
			var entity2 = mappingSet.EntitySet.GetEntity("BasicClass2");

			Assert.That(entity1.References, Has.Count(1));
			Assert.That(entity2.References, Has.Count(1));

			Assert.That(entity1.References[0], Is.SameAs(entity2.References[0]));

			DirectedReference reference = entity1.DirectedReferences.First();
			Assert.That(reference.FromName, Is.EqualTo("Class2s"));
			Assert.That(reference.ToName, Is.EqualTo("Class1s"));
			Assert.That(reference.FromEndCardinality, Is.EqualTo(Cardinality.Many));
			Assert.That(reference.ToEndCardinality, Is.EqualTo(Cardinality.Many));
			Assert.That(reference.FromEndEnabled, Is.True);
			Assert.That(reference.ToEndEnabled, Is.True);
		}

		[Test]
		public void It_Hooks_Up_The_Reference_Mappings()
		{
			var entity = mappingSet.EntitySet.GetEntity("BasicClass1");
			var reference = entity.References[0];

			Assert.That(reference.MappedRelationship(), Is.Null);
			Assert.That(reference.MappedTable(), Is.EqualTo(tableManyToMany));
		}
	}

	[TestFixture]
	public class When_Given_A_HBM_For_An_Entity_With_A_Map_Association : RelationshipTestBase
	{
		[SetUp]
		public void SetUp()
		{
			SetupBase("two-related-entities-map.hbm.xml");
		}

		[Test]
		public void It_Creates_Both_Entity_Objects()
		{
			var entities = mappingSet.EntitySet;

			Assert.That(entities.Entities, Has.Count(2));

			Assert.That(entities.GetEntity("BasicClass1"), Is.Not.Null);
			Assert.That(entities.GetEntity("BasicClass2"), Is.Not.Null);
		}

		[Test]
		public void It_Creates_The_Right_Relationship()
		{
			var entity1 = mappingSet.EntitySet.GetEntity("BasicClass1");
			var entity2 = mappingSet.EntitySet.GetEntity("BasicClass2");

			Assert.That(entity1.References, Has.Count(1));
			Assert.That(entity2.References, Has.Count(1));

			Assert.That(entity1.References[0], Is.SameAs(entity2.References[0]));

			DirectedReference reference = entity1.DirectedReferences.First();
			Assert.That(reference.FromName, Is.EqualTo("Class2s"));
			Assert.That(reference.ToName, Is.EqualTo("Property1"));
			Assert.That(reference.FromEndCardinality, Is.EqualTo(Cardinality.Many));
			Assert.That(reference.ToEndCardinality, Is.EqualTo(Cardinality.One));
			Assert.That(reference.FromEndEnabled, Is.True);
			Assert.That(reference.ToEndEnabled, Is.True);

			var indexColumnName = NHCollections.GetIndexColumnName(reference);
			Assert.That(indexColumnName, Is.EqualTo("BasicClass2_Index"));
		}

		[Test]
		public void It_Hooks_Up_The_Reference_Mappings()
		{
			var entity = mappingSet.EntitySet.GetEntity("BasicClass1");
			var reference = entity.References[0];

			Assert.That(reference.MappedRelationship(), Is.EqualTo(relationship));
			Assert.That(reference.MappedTable(), Is.Null);
		}
	}
}
