using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common.ExtensionMethods;
using Specs_For_Serialisation_Of_ComponentProperties;

namespace Specs_For_Deserialisation_Of_Components
{
	[TestFixture]
	public class When_Deserialising_A_ComponentProperty
	{
		[Test]
		public void It_Should_Create_This()
		{
			const string xml = When_Serialising_A_ComponentProperty.BasicPropertyXml;
			ComponentSpecification spec = new ComponentSpecificationImpl();

			ComponentProperty prop = new EntitySetDeserialisationScheme().DeserialiseComponentProperty(xml.GetXmlDocRoot(), spec);

			Assert.That(prop.Specification, Is.SameAs(spec));
			Assert.That(prop.Name, Is.EqualTo("Street"));
			Assert.That(prop.ValidationOptions, Is.Not.Null);
			Assert.That(prop.Type, Is.EqualTo("object"));
		}
	}
}
