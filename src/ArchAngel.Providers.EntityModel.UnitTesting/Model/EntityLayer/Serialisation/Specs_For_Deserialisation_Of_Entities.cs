using System.Xml;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common.ExtensionMethods;

namespace Specs_For_Deserialisation_Of_Entities
{
	[TestFixture]
	public class When_Deserialising_An_Empty_Entity
	{
		[Test]
		public void It_Should_Deserialise_To_This()
		{
			const string xml = Specs_For_Serialisation_Of_Entities.When_Serialising_An_Empty_Entity.BasicEntityXml;

			var scheme = new EntitySetDeserialisationScheme();
			Entity entity = scheme.DeserialiseEntity(xml.GetXmlDocRoot());

			scheme.PostProcessEntity(new EntitySetImpl(), null, entity, xml.GetXmlDocRoot());

			Assert.That(entity.Name, Is.EqualTo(""));
			Assert.That(entity.Key.Properties, Is.Empty);
			Assert.That(entity.Properties, Is.Empty);
			Assert.That(entity.References, Is.Empty);
			Assert.That(entity.Discriminator, Is.Null, "The deserialisation of the Discriminator should happen in the post process step.");
			Assert.That(entity.HasParent, Is.False);
		}
	}

	[TestFixture]
	public class When_Deserialising_An_Entity_With_All_Fields_Set
	{
		[Test]
		public void It_Should_Deserialise_To_This()
		{
			const string xml = Specs_For_Serialisation_Of_Entities.When_Serialising_An_Entity_With_All_Fields_Set.FullEntityXml;

			var scheme = new EntitySetDeserialisationScheme();
			XmlNode root = xml.GetXmlDocRoot();
			Entity entity = scheme.DeserialiseEntity(root);
			Entity entity2 = new EntityImpl("Entity2");
			EntitySet entitySet = new EntitySetImpl();
			entitySet.AddEntity(entity);
			entitySet.AddEntity(entity2);

			scheme.PostProcessEntity(entitySet, null, entity, root);

			Assert.That(entity.Name, Is.EqualTo("Entity1"));
			Assert.That(entity.Key, Is.Not.Null);
			// Don't both checking the contents of the Properties collection,
			// as long as it had something added to it. The tests on deserialising
			// properties will catch most bugs.
			Assert.That(entity.Properties, Is.Not.Empty);
			Assert.That(entity.Discriminator, Is.Not.Null, "The deserialisation of the Discriminator should happen in the post process step.");
			Assert.That(entity.HasParent, Is.True);
			Assert.That(entity.Parent, Is.SameAs(entity2));
		}
	}
}
