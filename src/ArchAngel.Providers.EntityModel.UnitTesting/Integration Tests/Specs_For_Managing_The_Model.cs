using System.Linq;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_Managing_The_MappingSet_Model
{
    [TestFixture]
    public class When_Deleting_A_Table
    {
    	private Table table1;
    	private Mapping mapping3;
    	private TableReferenceMapping refMapping1;
    	private MappingSet mappingSet;

		[SetUp]
		public void Setup()
		{
			mappingSet = new MappingSetImpl();

			table1 = new Table("Table1");
			var table2 = new Table("Table2");

			mapping3 = new MappingImpl { FromTable = table2 };
			refMapping1 = new TableReferenceMappingImpl { FromTable = table2, ToReference = new ReferenceImpl()};

			mappingSet.AddMapping(new MappingImpl { FromTable = table1 });
			mappingSet.AddMapping(new MappingImpl { FromTable = table1 });
			mappingSet.AddMapping(mapping3);
			mappingSet.AddMapping(refMapping1);
			mappingSet.AddMapping(new TableReferenceMappingImpl { FromTable = table1, ToReference = new ReferenceImpl()} );
		}


        [Test]
        public void All_Mappings_Are_Deleted()
        {
            Assert.That(mappingSet.Mappings.Count(), Is.EqualTo(3));

            mappingSet.DeleteTable(table1);

			Assert.That(mappingSet.Mappings.Count(), Is.EqualTo(1));
			Assert.That(mappingSet.Mappings[0], Is.SameAs(mapping3));
        }

		[Test]
		public void All_ReferenceMappings_Are_Deleted()
		{
			Assert.That(mappingSet.ReferenceMappings.Count(), Is.EqualTo(2));

			mappingSet.DeleteTable(table1);

			Assert.That(mappingSet.ReferenceMappings.Count, Is.EqualTo(1));
			Assert.That(mappingSet.ReferenceMappings[0], Is.SameAs(refMapping1));
		}
    }

	[TestFixture]
	public class When_Deleting_A_Reference
	{
		private Entity entity1;
		private Entity entity2;
		private Reference reference1;
		private TableReferenceMapping refMapping1;
		private RelationshipReferenceMapping relMapping1;
		private MappingSet mappingSet;

		[SetUp]
		public void Setup()
		{
			mappingSet = new MappingSetImpl(new Database("DB1"), new EntitySetImpl());

			entity1 = new EntityImpl("Entity1");
			entity2 = new EntityImpl("Entity2");
			mappingSet.EntitySet.AddEntity(entity1); 
			mappingSet.EntitySet.AddEntity(entity2);

			reference1 = entity1.CreateReferenceTo(entity2);
			var reference2 = entity1.CreateReferenceTo(entity2);

			refMapping1 = new TableReferenceMappingImpl { FromTable = new Table("Table2"), ToReference = reference2 };
			var refMapping2 = new TableReferenceMappingImpl { FromTable = new Table("Table1"), ToReference = reference1 };

			relMapping1 = new RelationshipReferenceMappingImpl { FromRelationship = new RelationshipImpl(), ToReference = reference2 };
			var relMapping2 = new RelationshipReferenceMappingImpl { FromRelationship = new RelationshipImpl(), ToReference = reference1 };

			mappingSet.AddMapping(refMapping1);
			mappingSet.AddMapping(refMapping2);

			mappingSet.AddMapping(relMapping1);
			mappingSet.AddMapping(relMapping2);
		}

		[Test]
		public void All_ReferenceMappings_Are_Deleted()
		{
			Assert.That(mappingSet.ReferenceMappings.Count(), Is.EqualTo(2));

			reference1.DeleteSelf(); // Should delete refMapping2

			Assert.That(mappingSet.ReferenceMappings.Count(), Is.EqualTo(1));
			Assert.That(mappingSet.ReferenceMappings[0], Is.SameAs(refMapping1));
		}

		[Test]
		public void All_RelationshipMappings_Are_Deleted()
		{
			Assert.That(mappingSet.RelationshipMappings.Count(), Is.EqualTo(2));

			reference1.DeleteSelf(); // Should delete relMapping2

			Assert.That(mappingSet.RelationshipMappings.Count(), Is.EqualTo(1));
			Assert.That(mappingSet.RelationshipMappings[0], Is.SameAs(relMapping1));
		}
	}

	[TestFixture]
	public class When_Deleting_A_Relationship
	{
		private Relationship relationship1;
		private RelationshipReferenceMapping relMapping1;
		private MappingSet mappingSet;

		[SetUp]
		public void Setup()
		{
			mappingSet = new MappingSetImpl(new Database("DB1"), new EntitySetImpl());

			relationship1 = new RelationshipImpl();
			relMapping1 = new RelationshipReferenceMappingImpl { FromRelationship = new RelationshipImpl(), ToReference = new ReferenceImpl() };
			var relMapping2 = new RelationshipReferenceMappingImpl { FromRelationship = relationship1, ToReference = new ReferenceImpl() };

			mappingSet.AddMapping(relMapping1);
			mappingSet.AddMapping(relMapping2);
		}

		[Test]
		public void All_Mappings_Are_Deleted()
		{
			Assert.That(mappingSet.RelationshipMappings.Count(), Is.EqualTo(2));

			mappingSet.DeleteRelationship(relationship1);

			Assert.That(mappingSet.RelationshipMappings.Count(), Is.EqualTo(1));
			Assert.That(mappingSet.RelationshipMappings[0], Is.SameAs(relMapping1));
		}
	}

	[TestFixture]
	public class When_Deleting_An_Entity
	{
		private Entity entity1;
		private Mapping mapping3;
		private MappingSet mappingSet;

		[SetUp]
		public void Setup()
		{
			mappingSet = new MappingSetImpl();

			entity1 = new EntityImpl("Entity1");
			var entity2 = new EntityImpl("Entity2");

			mapping3 = new MappingImpl { ToEntity = entity2 };

			mappingSet.AddMapping(new MappingImpl { ToEntity = entity1 });
			mappingSet.AddMapping(new MappingImpl { ToEntity = entity1 });
			mappingSet.AddMapping(mapping3);
		}

		[Test]
		public void All_Mappings_Are_Deleted()
		{
			Assert.That(mappingSet.Mappings.Count(), Is.EqualTo(3));

			mappingSet.DeleteEntity(entity1);

			Assert.That(mappingSet.Mappings.Count(), Is.EqualTo(1));
			Assert.That(mappingSet.Mappings[0], Is.SameAs(mapping3));
		}
	}
}
