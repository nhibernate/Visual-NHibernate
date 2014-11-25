using System.Linq;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common.ExtensionMethods;
using Specs_For_Serialisation_Of_Entities;
using Specs_For_Serialisation_Of_References;

namespace Specs_For_Deserialisation_Of_EntitySets
{
	[TestFixture]
	public class When_Deserialising_An_Empty_EntitySet
	{
		public const string BasicEntitySetXml = "<EntitySet />";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			EntitySet entitySet = new EntitySetDeserialisationScheme().DeserialiseEntitySet(BasicEntitySetXml.GetXmlDocRoot(), null);
			
			Assert.That(entitySet.Entities, Is.Empty);
			Assert.That(entitySet.References, Is.Empty);
		}
	}

	[TestFixture]
	public class When_Deserialising_An_EntitySet_With_Entities_And_References
	{
		public string FullEntitySetXml;

		[SetUp]
		public void Setup()
		{
			FullEntitySetXml = "<EntitySet>"
						+ "<Entities>" 
						+ When_Serialising_An_Entity_With_All_Fields_Set.FullEntityXml
						+ When_Serialising_An_Entity_With_All_Fields_Set.GetEntityXmlWithName("Entity2")
						+ "</Entities>"
						+ "<References>" + When_Serialising_An_Reference_With_All_Fields_Set.FullReferenceXml + "</References>"
						+ "</EntitySet>";
		}

		[Test]
		public void It_Should_Serialise_To_This()
		{
			EntitySet entitySet = new EntitySetDeserialisationScheme().DeserialiseEntitySet(FullEntitySetXml.GetXmlDocRoot(), null);

			Assert.That(entitySet.Entities.Count(), Is.EqualTo(2));
			Assert.That(entitySet.References.Count(), Is.EqualTo(1));

			var entity1 = entitySet.Entities.ElementAt(0);
			var entity2 = entitySet.Entities[1];
			var reference = entitySet.References.ElementAt(0);
			
			Assert.That(entity1.Name, Is.EqualTo("Entity1"));
			Assert.That(entity1.HasParent, Is.True);
			Assert.That(entity1.Parent, Is.SameAs(entity2));
			Assert.That(entity2.Name, Is.EqualTo("Entity2"));

			Assert.That(reference.Entity1, Is.SameAs(entity2));
			Assert.That(reference.Entity2, Is.SameAs(entity1));
		}
	}
}
