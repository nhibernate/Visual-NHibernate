using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;
using Specs_For_Serialisation_Of_Properties;

namespace Specs_For_Serialisation_Of_Entities
{
	[TestFixture]
	public class When_Serialising_An_Empty_Entity
	{
		public const string BasicEntityXml = "<Entity><Discriminator /><Key /><Name /><Parent /><Properties /></Entity>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = BasicEntityXml;

			Entity entity = GetEntity();

			string outputXML = new EntitySetSerialisationScheme().SerialiseEntity(entity);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}

		public static Entity GetEntity()
		{
			return new EntityImpl();
		}
	}

	[TestFixture]
	public class When_Serialising_An_Entity_With_All_Fields_Set
	{
		public const string FullEntityXml = "<Entity><Discriminator /><Key /><Name>Entity1</Name><Parent>Entity2</Parent><Properties>"
							+ When_Serialising_An_Empty_Property.BasicPropertyXml 
							+ "</Properties></Entity>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = FullEntityXml;

			Entity entity = new EntityImpl("Entity1");
			entity.AddProperty(new PropertyImpl());
			Entity entity2 = new EntityImpl("Entity2");
			entity2.AddChild(entity);

			string outputXML = new EntitySetSerialisationScheme().SerialiseEntity(entity);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}

		public static string GetEntityXmlWithName(string entityName)
		{
			return "<Entity><Discriminator /><Key /><Name>" + entityName + "</Name><Parent /><Properties /></Entity>";	
		}
	}
}
