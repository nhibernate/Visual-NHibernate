using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;

namespace Specs_For_Serialisation_Of_ComponentProperties
{
	[TestFixture]
	public class When_Serialising_A_ComponentProperty
	{
		public const string BasicPropertyXml = "<Property><Name>Street</Name><Type>object</Type><Validation /></Property>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = BasicPropertyXml;

			ComponentProperty property = new ComponentPropertyImpl("Street");

			string outputXML = new EntitySetSerialisationScheme().SerialiseComponentProperty(property);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}
}
