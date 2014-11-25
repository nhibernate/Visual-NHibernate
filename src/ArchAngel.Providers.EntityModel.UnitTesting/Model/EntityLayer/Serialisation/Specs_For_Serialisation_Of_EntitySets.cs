using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;
using Specs_For_Serialisation_Of_Entities;
using Specs_For_Serialisation_Of_References;

namespace Specs_For_Serialisation_Of_EntitySets
{
	[TestFixture]
	public class When_Serialising_An_Empty_EntitySet
	{
		public const string BasicEntitySetXml = "<EntitySet Version=\"1\" />";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = BasicEntitySetXml;

			EntitySet entitySet = new EntitySetImpl();

			string outputXML = new EntitySetSerialisationScheme().SerialiseEntitySet(entitySet);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}

	[TestFixture]
	public class When_Serialising_An_EntitySet_With_Both_Entities_And_References
	{
		public const string FullEntitySetXml = "<EntitySet Version=\"1\"><Entities>"
							+ When_Serialising_An_Empty_Entity.BasicEntityXml
							+ "</Entities><References>"
							+ When_Serialising_An_Reference_With_All_Fields_Set.FullReferenceXml
							+ "</References></EntitySet>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = FullEntitySetXml;

			EntitySet entitySet = new EntitySetImpl();
			entitySet.AddEntity(When_Serialising_An_Empty_Entity.GetEntity());
			entitySet.AddReference(When_Serialising_An_Reference_With_All_Fields_Set.GetReference());

			string outputXML = new EntitySetSerialisationScheme().SerialiseEntitySet(entitySet);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}
}
