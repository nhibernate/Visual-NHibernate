using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common.ExtensionMethods;
using Specs_For_Serialisation_Of_Components;

namespace Specs_For_Deserialisation_Of_Components
{
	[TestFixture]
	public class When_Deserialising_A_Component
	{
		[Test]
		public void It_Should_Create_This()
		{
			const string xml = When_Serialising_A_Component.BasicComponentXml;
			var parentEntity = new EntityImpl("Entity1");
			
			var entitySet = new EntitySetImpl();
			entitySet.AddEntity(parentEntity);

			var spec = new ComponentSpecificationImpl("Address");
			spec.AddProperty(new ComponentPropertyImpl("Street"));
			spec.EntitySet = entitySet;

			Component comp = new EntitySetDeserialisationScheme().DeserialiseComponent(xml.GetXmlDocRoot(), spec);

			Assert.That(comp.Name, Is.EqualTo("HomeAddress"));
			Assert.That(comp.ParentEntity, Is.SameAs(parentEntity));
			Assert.That(comp.Specification, Is.SameAs(spec));
			Assert.That(comp.Properties, Has.Count(1));
			Assert.That(comp.Properties[0].RepresentedProperty, Is.SameAs(spec.Properties[0]));
		}
	}
}
