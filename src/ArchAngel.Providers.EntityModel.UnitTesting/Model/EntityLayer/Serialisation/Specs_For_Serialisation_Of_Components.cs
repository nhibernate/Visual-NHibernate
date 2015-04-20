using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;

namespace Specs_For_Serialisation_Of_Components
{
	[TestFixture]
	public class When_Serialising_A_Component
	{
		public const string BasicComponentXml = "<Component parent-type=\"Entity1\" name=\"HomeAddress\" />";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = BasicComponentXml;

			var component = new ComponentImpl();
			component.ParentEntity = new EntityImpl("Entity1");
			component.Name = "HomeAddress";

			string outputXML = new EntitySetSerialisationScheme().SerialiseComponent(component);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}
}
