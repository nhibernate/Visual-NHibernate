using System;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;

namespace Specs_For_Serialisation_Of_References
{
	[TestFixture]
	public class When_Serialising_An_Empty_Reference
	{
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void It_Should_Throw_An_Exception()
		{
			Reference reference = new ReferenceImpl();

			new EntitySetSerialisationScheme().SerialiseReference(reference);
		}
	}

	[TestFixture]
	public class When_Serialising_A_Reference_That_Is_Missing
	{
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void A_FromEntity__It_Should_Throw_An_Exception()
		{
			Reference reference = new ReferenceImpl();
			reference.Entity2 = new EntityImpl("Entity1");

			new EntitySetSerialisationScheme().SerialiseReference(reference);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void A_ToEntity__It_Should_Throw_An_Exception()
		{
			Reference reference = new ReferenceImpl();
			reference.Entity1 = new EntityImpl("Entity1");

			new EntitySetSerialisationScheme().SerialiseReference(reference);
		}

		private const string CardinalityFormatXml =
			"<Reference identifier=\"11111111-1111-1111-1111-111111111111\"><Cardinality1 min=\"{0}\" max=\"{1}\" /><End1Enabled>True</End1Enabled><End1Name>ParentEntity1</End1Name><Entity1>Entity2</Entity1><Cardinality2 min=\"{2}\" max=\"{3}\" /><End2Enabled>True</End2Enabled><End2Name>Entity2s</End2Name><Entity2>Entity1</Entity2></Reference>";

		[Test]
		public void A_FromCardinality__It_Should_Default_To_One()
		{
			string expectedXML = string.Format(CardinalityFormatXml, 3, 4, 1, 1);

			Reference reference = new ReferenceImpl (new Guid("11111111-1111-1111-1111-111111111111"))
			                      	{
			                      		Entity2 = new EntityImpl("Entity1"),
			                      		Entity1 = new EntityImpl("Entity2"),
			                      		Cardinality1 = new Cardinality(3, 4),
										End1Enabled = true,
										End2Enabled = true,
										End1Name = "ParentEntity1",
										End2Name = "Entity2s"
			                      	};

			string outputXML = new EntitySetSerialisationScheme().SerialiseReference(reference);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}

		[Test]
		public void A_ToCardinality__It_Should_Default_To_One()
		{
			string expectedXML = string.Format(CardinalityFormatXml, 1, 1, 2, 2);

			Reference reference = new ReferenceImpl (new Guid("11111111-1111-1111-1111-111111111111"))
			                      	{
			                      		Entity2 = new EntityImpl("Entity1"),
			                      		Entity1 = new EntityImpl("Entity2"),
			                      		Cardinality2 = new Cardinality(2),
										End1Enabled = true,
										End2Enabled = true,
										End1Name = "ParentEntity1",
										End2Name = "Entity2s"
			                      	};

			string outputXML = new EntitySetSerialisationScheme().SerialiseReference(reference);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}

	[TestFixture]
	public class When_Serialising_An_Reference_With_All_Fields_Set
	{
		public const string FullReferenceXml = "<Reference identifier=\"11111111-1111-1111-1111-111111111111\"><Cardinality1 min=\"1\" max=\"1\" /><End1Enabled>True</End1Enabled><End1Name>ParentEntity1</End1Name><Entity1>Entity2</Entity1><Cardinality2 min=\"0\" max=\"5\" /><End2Enabled>True</End2Enabled><End2Name>Entity2s</End2Name><Entity2>Entity1</Entity2></Reference>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = FullReferenceXml;

			Reference reference = GetReference();

			string outputXML = new EntitySetSerialisationScheme().SerialiseReference(reference);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}

		public static Reference GetReference()
		{
			return new ReferenceImpl(new Guid("11111111-1111-1111-1111-111111111111"))
			       	{
			       		Name = "Ref1",
			       		Cardinality1 = Cardinality.One,
			       		Cardinality2 = new Cardinality(0, 5),
			       		Entity1 = new EntityImpl("Entity2"),
			       		Entity2 = new EntityImpl("Entity1"),
			       		End1Enabled = true,
			       		End2Enabled = true,
						End1Name = "ParentEntity1",
						End2Name = "Entity2s"
			       	};
		}
	}
}
