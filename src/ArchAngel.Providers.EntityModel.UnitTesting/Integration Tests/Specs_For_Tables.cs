using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UnitTesting.Integration_Tests;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_Tables
{
    [TestFixture]
	[Category("IntegrationTests")]
    public class When_Deleting_A_Table
    {
		private ITable table1;
        private ITable table2;
    	private MappingSet mappingSet;
    	private IDatabase db;
    	private EntitySet entitySet;


        [Test]
        public void All_Relationships_Are_Deleted()
        {
        	Assert.That(table2.Relationships.Count, Is.EqualTo(2));
            Assert.That(db.Tables.Count, Is.EqualTo(2));

            table1.DeleteSelf();

            Assert.That(db.Tables.Count, Is.EqualTo(1));
            Assert.That(db.Tables[0], Is.SameAs(table2));
            Assert.That(table2.Relationships.Count, Is.EqualTo(0));
        }

		[Test]
		public void All_Reference_Mappings_Are_Deleted()
		{
			var reference = entitySet.Entities[0].CreateReferenceTo(entitySet.Entities[1]);
			mappingSet.ChangeMappingFor(reference).To(table1);

			Assert.That(reference.MappedTable(), Is.EqualTo(table1));

			table1.DeleteSelf();

			Assert.That(reference.MappedTable(), Is.Null);
		}

		[Test]
		public void All_Regular_Mappings_Are_Deleted()
		{
			Assert.That(entitySet.Entities[0].ConcreteProperties[0].MappedColumn(), Is.EqualTo(table1.Columns[0]));

			table1.DeleteSelf();

			Assert.That(entitySet.Entities[0].ConcreteProperties[0].MappedColumn(), Is.Null);
		}

		[SetUp]
    	public void Setup()
    	{
			mappingSet = ModelSetup.SetupModel();
			entitySet = mappingSet.EntitySet;
			db = mappingSet.Database;
			table1 = db.Tables[0];
			table2 = db.Tables[1];

    		table1.CreateRelationshipTo(table2);
    	}
    }
}
