using System;
using System.Collections.Generic;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common.ExtensionMethods;
using Specs_For_Serialisation_Of_RelationshipMapping;

namespace Specs_For_Deserialisation_Of_RelationshipMappings
{
	[TestFixture]
	public class When_Deserialising_A_RelationshipMapping_With_All_Fields_Set
	{
		private EntitySet entitySet;
		private IDatabase database;
		private Relationship relationship;
		private Reference reference;

		[SetUp]
		public void Setup()
		{
			relationship = new RelationshipImpl(new Guid("21111111-1111-1111-1111-111111111111"));
			reference = new ReferenceImpl(new Guid("11111111-1111-1111-1111-111111111111"));

			database = MockRepository.GenerateMock<IDatabase>();
			database.Stub(d => d.Relationships).Return(new List<Relationship>{relationship});

			entitySet = MockRepository.GenerateMock<EntitySet>();
			entitySet.Stub(e => e.References).Return(new List<Reference>{ reference }.AsReadOnly());
		}

		[Test]
		public void It_Should_Deserialise_To_This()
		{
			const string xml = When_Serialising_A_RelationshipMapping_With_All_Fields_Set.FullMappingXml;

			RelationshipReferenceMapping mapping = new MappingSetDeserialisationScheme().DeserialiseRelationshipMapping(xml.GetXmlDocRoot(), database, entitySet);

			TestMapping(mapping, relationship, reference);
		}

		public static void TestMapping(RelationshipReferenceMapping mapping, Relationship table, Reference reference)
		{
			Assert.That(mapping.FromRelationship, Is.SameAs(table));
			Assert.That(mapping.ToReference, Is.SameAs(reference));
		}
	}
}
