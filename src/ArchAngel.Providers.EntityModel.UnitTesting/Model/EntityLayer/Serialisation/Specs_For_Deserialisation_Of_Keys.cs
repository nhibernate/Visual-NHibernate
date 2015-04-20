using System.Linq;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common.Exceptions;
using Slyce.Common.ExtensionMethods;

namespace Specs_For_Deserialisation_Of_Keys
{
	[TestFixture]
	public class When_Deserialising_An_Empty_Key
	{
		public const string BasicEntityXml = "<Key />";

		[Test]
		public void It_Should_Create_This()
		{
			EntityKey key = new EntitySetDeserialisationScheme().DeserialiseKey(BasicEntityXml.GetXmlDocRoot(), new EntityImpl());
			
			Assert.That(key, Is.Not.Null);
			Assert.That(key.Properties, Is.Empty);
			Assert.That(key.Component, Is.Null);
			Assert.That(key.KeyType, Is.EqualTo(EntityKeyType.Empty));
		}
	}

	[TestFixture]
	public class When_Deserialising_An_Entity_With_A_Property
	{
		public const string FullEntityXml = "<Key keytype=\"Properties\"><Properties><Property>Property1</Property></Properties></Key>";

		[Test]
		public void It_Should_Create_This()
		{
			Entity parentEntity = new EntityImpl();
			Property prop = new PropertyImpl {Name="Property1"};
			parentEntity.AddProperty(prop);

			EntityKey key = new EntitySetDeserialisationScheme().DeserialiseKey(FullEntityXml.GetXmlDocRoot(), parentEntity);

			Assert.That(key.Properties.Count(), Is.EqualTo(1));
			Assert.That(key.Properties.ElementAt(0), Is.SameAs(prop));

			Assert.That(key.Component, Is.Null);
			Assert.That(key.KeyType, Is.EqualTo(EntityKeyType.Properties));
		}
	}

	[TestFixture]
	public class When_Deserialising_An_Entity_With_A_Component
	{
		public const string FullEntityXml = "<Key keytype=\"Component\"><Component name=\"Component_Name\" /></Key>";

		[Test]
		public void It_Should_Create_This()
		{
			Entity parentEntity = new EntityImpl();
			Component component = new ComponentImpl {Name = "Component_Name"};
			parentEntity.AddComponent(component);

			EntityKey key = new EntitySetDeserialisationScheme().DeserialiseKey(FullEntityXml.GetXmlDocRoot(), parentEntity);

			Assert.That(key.Properties, Is.Empty);
			Assert.That(key.Component, Is.SameAs(component));

			Assert.That(key.KeyType, Is.EqualTo(EntityKeyType.Component));
		}
	}
	
	[TestFixture]
	public class When_Deserialising_An_Entity_With_A_Both_A_Component_And_Properties
	{
		public const string FullEntityXml = "<Key keytype=\"Properties\"><Component name=\"Component_Name\" /><Properties><Property>Property1</Property></Properties></Key>";
		public const string NoTypeXml = "<Key><Component name=\"Component_Name\" /><Properties><Property>Property1</Property></Properties></Key>";

		[Test]
		public void It_Should_Create_This()
		{
			Entity parentEntity = new EntityImpl();
			Component component = new ComponentImpl {Name = "Component_Name"};
			parentEntity.AddComponent(component);
			Property prop = new PropertyImpl { Name = "Property1" };
			parentEntity.AddProperty(prop);

			EntityKey key = new EntitySetDeserialisationScheme().DeserialiseKey(FullEntityXml.GetXmlDocRoot(), parentEntity);

			Assert.That(key.Properties.Count(), Is.EqualTo(1));
			Assert.That(key.Properties.ElementAt(0), Is.SameAs(prop));
			Assert.That(key.Component, Is.SameAs(component));

			Assert.That(key.KeyType, Is.EqualTo(EntityKeyType.Properties));
		}

		[Test]
		[ExpectedException(typeof(DeserialisationException))]
		public void It_Fails_If_There_Is_No_KeyType()
		{
			Entity parentEntity = new EntityImpl();
			Component component = new ComponentImpl { Name = "Component_Name" };
			parentEntity.AddComponent(component);
			Property prop = new PropertyImpl { Name = "Property1" };
			parentEntity.AddProperty(prop);

			new EntitySetDeserialisationScheme().DeserialiseKey(NoTypeXml.GetXmlDocRoot(), parentEntity);
		}
	}
}
