using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;
using Specs_For_Serialisation_Of_Mapping;
using Specs_For_Serialisation_Of_ReferenceMapping;

namespace Specs_For_Serialisation_Of_MappingSets
{
	[TestFixture]
	public class When_Serialising_An_Empty_MappingSet
	{
		public const string EmptyMappingSetXml = "<MappingSet Version=\"1\" />";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			string outputXML = new MappingSetSerialisationScheme().SerialiseMappingSet(new MappingSetImpl());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(EmptyMappingSetXml));
		}
	}

	[TestFixture]
	public class When_Serialising_A_MappingSet_With_Mappings
	{
		public const string FullMappingSetXml = "<MappingSet Version=\"1\"><Mappings>" 
			+ When_Serialising_A_Mapping_With_All_Fields_Set.FullMappingXml 
			+ "</Mappings><ReferenceMappings>"+ When_Serialising_A_ReferenceMapping_With_All_Fields_Set.FullMappingXml
			+ "</ReferenceMappings></MappingSet>";
		[Test]
		public void It_Should_Serialise_To_This()
		{
			var set = new MappingSetImpl();
			set.AddMapping(When_Serialising_A_Mapping_With_All_Fields_Set.GetMapping());
			set.AddMapping(When_Serialising_A_ReferenceMapping_With_All_Fields_Set.GetMapping());

			string outputXML = new MappingSetSerialisationScheme().SerialiseMappingSet(set);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(FullMappingSetXml));
		}
	}
}
