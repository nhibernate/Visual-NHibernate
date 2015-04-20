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
	public class When_Deleting_A_Reference
	{
		private Entity entity;
		private Relationship relationship;
		private Reference reference;

		[SetUp]
		public void Setup()
		{
			var mappingSet = ModelSetup.SetupModel();
			var entitySet = mappingSet.EntitySet;
			entity = entitySet.Entities[0];
			reference = entity.References[0];
			relationship = reference.MappedRelationship();
		}

		[Test]
		public void The_Reference_Is_Removed_From_The_Entity()
		{
			Assert.That(entity.References, Has.Count(1));

			reference.DeleteSelf();

			Assert.That(entity.References, Has.Count(0));
		}

		[Test]
		public void The_Relationship_Mapping_Is_Removed()
		{
			Assert.That(relationship.MappedReferences().First(), Is.SameAs(reference));

			reference.DeleteSelf();

			Assert.That(relationship.MappedReferences().Count(), Is.EqualTo(0));
		}
	}
}
