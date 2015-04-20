using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;
using Specs_For_Serialisation_Of_ComponentProperties;
using Specs_For_Serialisation_Of_Components;

namespace Specs_For_Serialisation_Of_ComponentSpecifications
{
	[TestFixture]
	public class When_Serialising_A_ComponentSpecification
	{
		public const string BasicSpecXml = "<ComponentSpecification name=\"Address\">"
												+ When_Serialising_A_ComponentProperty.BasicPropertyXml
												+ When_Serialising_A_Component.BasicComponentXml
										+	"</ComponentSpecification>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = BasicSpecXml;

			var spec = new ComponentSpecificationImpl();
			spec.Name = "Address";
			spec.AddProperty(new ComponentPropertyImpl("Street"));
			spec.CreateImplementedComponentFor(new EntityImpl("Entity1"), "HomeAddress");

			string outputXML = new EntitySetSerialisationScheme().SerialiseComponentSpecification(spec);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}
}
