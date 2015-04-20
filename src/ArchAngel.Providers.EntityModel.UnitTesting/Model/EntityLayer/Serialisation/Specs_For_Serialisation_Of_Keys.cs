using System.Linq;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;

namespace Specs_For_Serialisation_Of_Keys
{
	[TestFixture]
	public class When_Serialising_An_Empty_Key
	{
		public const string BasicKeyXml = "<Key />";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = BasicKeyXml;

			EntityKey key = new EntityKeyImpl();

			string outputXML = new EntitySetSerialisationScheme().SerialiseKey(key);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}

	[TestFixture]
	public class When_Serialising_A_Key_With_Multiple_Properties
	{
		public const string FullKeyXml = "<Key keytype=\"Properties\"><Properties><Property>Property1</Property><Property>Property2</Property></Properties></Key>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = FullKeyXml;

			EntityKey key = new EntityKeyImpl();
			key.AddProperty(new PropertyImpl { Name = "Property1" });
			key.AddProperty(new PropertyImpl { Name = "Property2" });

			string outputXML = new EntitySetSerialisationScheme().SerialiseKey(key);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}

	[TestFixture]
	public class When_Serialising_A_Key_With_A_Component
	{
		public const string FullKeyXml = "<Key keytype=\"Component\"><Component name=\"Component_Name\" /></Key>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = FullKeyXml;

			EntityKey key = new EntityKeyImpl();
			key.Component = new ComponentImpl {Name = "Component_Name"};

			string outputXML = new EntitySetSerialisationScheme().SerialiseKey(key);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}

	[TestFixture]
	public class When_Serialising_A_Key_With_Both_A_Component_And_Properties
	{
		public const string FullKeyXml = "<Key keytype=\"Component\"><Component name=\"Component_Name\" /><Properties><Property>Property1</Property></Properties></Key>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = FullKeyXml;

			EntityKey key = new EntityKeyImpl();
			key.AddProperty(new PropertyImpl { Name = "Property1" });
			key.Component = new ComponentImpl { Name = "Component_Name" };
			key.KeyType = EntityKeyType.Component;

			string outputXML = new EntitySetSerialisationScheme().SerialiseKey(key);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}
}
