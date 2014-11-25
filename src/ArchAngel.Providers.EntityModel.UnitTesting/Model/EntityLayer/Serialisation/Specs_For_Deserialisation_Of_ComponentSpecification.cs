using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common.ExtensionMethods;
using Specs_For_Serialisation_Of_ComponentSpecifications;

namespace Specs_For_Deserialisation_Of_ComponentSpecification
{
	[TestFixture]
	public class When_Deserialising_A_ComponentSpecification
	{
		[Test]
		public void It_Should_Create_This()
		{
			const string xml = When_Serialising_A_ComponentSpecification.BasicSpecXml;
			var parentEntity = new EntityImpl("Entity1");
			
			var entitySet = new EntitySetImpl();
			entitySet.AddEntity(parentEntity);

			ComponentSpecification spec = new EntitySetDeserialisationScheme().DeserialiseComponentSpecification(xml.GetXmlDocRoot(), entitySet);

			Assert.That(spec.Name, Is.EqualTo("Address"));
			Assert.That(spec.Properties, Has.Count(1));
			Assert.That(spec.Properties[0].Name, Is.EqualTo("Street"));
			Assert.That(spec.ImplementedComponents, Has.Count(1));
			Assert.That(spec.ImplementedComponents[0].ParentEntity, Is.SameAs(parentEntity));
		}
	}
}
