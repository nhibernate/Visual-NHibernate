using System;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;

namespace Specs_For_Serialisation_Of_RelationshipMapping
{
	[TestFixture]
	public class When_Serialising_An_Empty_RelationshipMapping
	{
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void It_Should_Throw_An_Exception()
		{
			new MappingSetSerialisationScheme().SerialiseRelationshipMapping(new RelationshipReferenceMappingImpl());
		}
	}

	[TestFixture]
	public class When_Serialising_A_RelationshipMapping_That_Is_Missing
	{
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void A_FromTable__It_Should_Throw_An_Exception()
		{
			var mapping = new RelationshipReferenceMappingImpl();
			mapping.ToReference = new ReferenceImpl();
			new MappingSetSerialisationScheme().SerialiseRelationshipMapping(mapping);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void A_ToEntity__It_Should_Throw_An_Exception()
		{
			var mapping = new RelationshipReferenceMappingImpl();
			mapping.FromRelationship = new RelationshipImpl();
			new MappingSetSerialisationScheme().SerialiseRelationshipMapping(mapping);
		}
	}

	[TestFixture]
	public class When_Serialising_A_RelationshipMapping_With_All_Fields_Set
	{
		public const string FullMappingXml = "<RelationshipReferenceMapping><FromRelationship>21111111-1111-1111-1111-111111111111</FromRelationship><ToReference>11111111-1111-1111-1111-111111111111</ToReference></RelationshipReferenceMapping>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = FullMappingXml;

			string outputXML = new MappingSetSerialisationScheme().SerialiseRelationshipMapping(GetMapping());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}

		public static RelationshipReferenceMapping GetMapping()
		{
			RelationshipReferenceMapping mapping = new RelationshipReferenceMappingImpl();
			mapping.FromRelationship = new RelationshipImpl(new Guid("21111111-1111-1111-1111-111111111111"));
			mapping.ToReference = new ReferenceImpl(new Guid("11111111-1111-1111-1111-111111111111"));

			return mapping;
		}
	}
}
