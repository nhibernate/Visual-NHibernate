using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common.ExtensionMethods;
using Specs_For_Serialisation_Of_Properties;

namespace Specs_For_Deserialisation_Of_Properties
{
	[TestFixture]
	public class When_Deserialising_A_Property_With_Default_Nullable_Fields
	{
		[Test]
		public void It_Should_Create_This()
		{
			const string xml = When_Serialising_An_Empty_Property.BasicPropertyXml;
			var parentEntity = new EntityImpl();
			Property prop = new EntitySetDeserialisationScheme().DeserialiseProperty(xml.GetXmlDocRoot(), parentEntity);		

			Assert.That(prop.Entity, Is.SameAs(parentEntity));
			Assert.That(prop.Name, Is.EqualTo(""));
			Assert.That(prop.ReadOnly, Is.False);
			Assert.That(prop.ValidationOptions, Is.Not.Null);
			Assert.That(prop.Type, Is.EqualTo("object"));
			Assert.That(prop.IsKeyProperty, Is.False);
			//Assert.That(prop.IsVirtual, Is.False);
		}
	}

	[TestFixture]
	public class When_Deserialising_A_Property_With_All_Fields_Set_To_Something
	{
		[Test]
		public void It_Should_Create_This()
		{
			const string xml = When_Serialising_A_Property_With_All_Fields_Set.FullPropertyXml;
			var parentEntity = new EntityImpl();
			Property prop = new EntitySetDeserialisationScheme().DeserialiseProperty(xml.GetXmlDocRoot(), parentEntity);

			Assert.That(prop.Entity, Is.SameAs(parentEntity));
			Assert.That(prop.Name, Is.EqualTo("Property1"));
			Assert.That(prop.ReadOnly, Is.True);
			Assert.That(prop.ValidationOptions, Is.Not.Null);
			Assert.That(prop.Type, Is.EqualTo("SomeType"));
			Assert.That(prop.IsKeyProperty, Is.True);
			//Assert.That(prop.IsVirtual, Is.True);
		}
	}
}
