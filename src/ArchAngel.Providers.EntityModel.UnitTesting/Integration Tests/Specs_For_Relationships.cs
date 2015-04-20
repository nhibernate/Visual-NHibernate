using System.Linq;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.UnitTesting.Integration_Tests;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_Relationships
{
	[TestFixture]
	[Category("IntegrationTests")]
	public class When_Deleting_A_Relationship
	{
		private IDatabase database;
		private ITable table;
		private Relationship relationship;
		private Reference reference;

		[SetUp]
		public void Setup()
		{
			var mappingSet = ModelSetup.SetupModel();
			database = mappingSet.Database;
			table = database.Tables[0];
			relationship = table.Relationships[0];
			reference = relationship.MappedReferences().First();
		}

		[Test]
		public void The_Relationship_Is_Removed_From_The_Table()
		{
			Assert.That(table.Relationships, Has.Count(1));

			relationship.DeleteSelf();

			Assert.That(table.Relationships, Has.Count(0));
		}

		[Test]
		public void The_Relationship_Mapping_Is_Removed()
		{
			Assert.That(reference.MappedRelationship(), Is.SameAs(relationship));

			relationship.DeleteSelf();

			Assert.That(reference.MappedRelationship(), Is.Null);
		}
	}
}
