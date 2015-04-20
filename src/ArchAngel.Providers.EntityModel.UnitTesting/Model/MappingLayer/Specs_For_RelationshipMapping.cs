using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_RelationshipMapping
{
	[TestFixture]
	public class Property_Change_Notifications
	{
		private RelationshipReferenceMapping mapping;

		[SetUp]
		public void SetUp()
		{
			mapping = new RelationshipReferenceMappingImpl();
		}

		[Test]
		public void FromRelationship()
		{
			bool eventFired = false;
			mapping.PropertyChanged += (s, e) => { if (e.PropertyName == "FromRelationship") eventFired = true; };

			mapping.FromRelationship = new RelationshipImpl();
			Assert.That(eventFired, Is.True);
		}

		[Test]
		public void ToReference()
		{
			bool eventFired = false;
			mapping.PropertyChanged += (s, e) => { if (e.PropertyName == "ToReference") eventFired = true; };

			mapping.ToReference = new ReferenceImpl();
			Assert.That(eventFired, Is.True);
		}
	}

	[TestFixture]
	public class When_Adding_A_Reference
	{
		private RelationshipReferenceMapping mapping;
		private MappingSet mappingSet;

		[SetUp]
		public void SetUp()
		{
			mappingSet = new MappingSetImpl();
			mapping = new RelationshipReferenceMappingImpl();
		}

		[Test]
		public void It_Is_Added_Correctly()
		{
			mappingSet.AddMapping(mapping);

			Assert.That(mappingSet.RelationshipMappings.Contains(mapping));
		}

		[Test]
		public void It_Sets_The_Parent_Property()
		{
			mappingSet.AddMapping(mapping);

			Assert.That(mapping.MappingSet, Is.SameAs(mappingSet));
		}

		[Test]
		public void Can_Find_Mapped_Relationship()
		{
			// No names, should be searching on object identity
			mapping.FromRelationship = new RelationshipImpl();
			mapping.ToReference = new ReferenceImpl();
			mappingSet.AddMapping(mapping);

			Assert.That(mappingSet.GetMappedRelationshipFor(mapping.ToReference), Is.SameAs(mapping.FromRelationship));
		}

		[Test]
		public void Can_Find_Mapped_Reference()
		{
			// No names, should be searching on object identity
			mapping.FromRelationship = new RelationshipImpl();
			mapping.ToReference = new ReferenceImpl();
			mappingSet.AddMapping(mapping);

			var references = mappingSet.GetMappedReferencesFor(mapping.FromRelationship);
			Assert.That(references.Count(), Is.EqualTo(1));
			Assert.That(references.ElementAt(0), Is.SameAs(mapping.ToReference));
		}
	}

	[TestFixture]
	public class When_Deleting_A_RelationshipMapping
	{
		private RelationshipReferenceMapping mapping;
		private MappingSet mappingSet;

		[SetUp]
		public void SetUp()
		{
			mappingSet = new MappingSetImpl();
			mapping = new RelationshipReferenceMappingImpl();
			mappingSet.AddMapping(mapping);
		}

		[Test]
		public void It_Should_Be_Removed_From_The_Collection()
		{
			Assert.That(mappingSet.RelationshipMappings.Contains(mapping), Is.True);

			mapping.Delete();

			Assert.That(mappingSet.RelationshipMappings.Contains(mapping), Is.False);
			Assert.That(mapping.MappingSet, Is.Null);
			Assert.That(mapping.FromRelationship, Is.Null);
			Assert.That(mapping.ToReference, Is.Null);
		}
	}
}
