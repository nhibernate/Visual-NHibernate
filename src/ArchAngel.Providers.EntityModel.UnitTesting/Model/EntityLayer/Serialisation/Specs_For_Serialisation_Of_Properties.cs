using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;

namespace Specs_For_Serialisation_Of_Properties
{
	[TestFixture]
	public class When_Serialising_An_Empty_Property
	{
		public const string BasicPropertyXml = "<Property><IsKey>False</IsKey><Name /><ReadOnly>False</ReadOnly><Type>object</Type><Validation /></Property>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = BasicPropertyXml;

			Property property = new PropertyImpl();

			string outputXML = new EntitySetSerialisationScheme().SerialiseProperty(property);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}

	[TestFixture]
	public class When_Serialising_A_Property_With_All_Fields_Set
	{
		public const string FullPropertyXml = "<Property><IsKey>True</IsKey><Name>Property1</Name><ReadOnly>True</ReadOnly><Type>SomeType</Type><Validation /></Property>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = FullPropertyXml;

			Property property = new PropertyImpl
			                    	{
			                    		Name = "Property1",
										ReadOnly = true,
										Type = "SomeType",
										//IsVirtual = true,
										IsKeyProperty = true
			                    	};

			string outputXML = new EntitySetSerialisationScheme().SerialiseProperty(property);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}
}
