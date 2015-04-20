using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common.ExtensionMethods;
using Specs_For_Deserialisation_Of_Mappings;
using Specs_For_Deserialisation_Of_ReferenceMappings;

namespace Specs_For_Deserialisation_Of_MappingSets
{
	[TestFixture]
	public class When_Deserialising_An_Empty_MappingSet
	{
		[Test]
		public void It_Should_Deserialise_To_This()
		{
			var xml = "<MappingSet />";

			Database database = new Database("DB1");
			EntitySet entitySet = new EntitySetImpl();

			MappingSet set = new MappingSetDeserialisationScheme().DeserialiseMappingSet(xml.GetXmlDocRoot(), database, entitySet);

			Assert.That(set.EntitySet, Is.SameAs(entitySet));
			Assert.That(set.Database, Is.SameAs(database));
		}
	}

	[TestFixture]
	public class When_Deserialising_A_MappingSet_With_Mappings
	{
		[Test]
		public void It_Should_Deserialise_To_This()
		{
			var xml = Specs_For_Serialisation_Of_MappingSets.When_Serialising_A_MappingSet_With_Mappings.FullMappingSetXml;
			
			var testData = new When_Deserialising_A_Mapping_With_All_Fields_Set();
			testData.Setup();

			MappingSet set = new MappingSetDeserialisationScheme().DeserialiseMappingSet(xml.GetXmlDocRoot(), testData.database, testData.entitySet);

			Assert.That(set.EntitySet, Is.SameAs(testData.entitySet));
			Assert.That(set.Database, Is.SameAs(testData.database));
			Assert.That(set.Mappings.Count, Is.EqualTo(1));
			Assert.That(set.ReferenceMappings.Count, Is.EqualTo(1));
			When_Deserialising_A_Mapping_With_All_Fields_Set.TestMapping(set.Mappings[0], testData.table, testData.entity1);
			When_Deserialising_A_ReferenceMapping_With_All_Fields_Set.TestMapping(set.ReferenceMappings[0], testData.table, testData.entity1.References[0]);
		}
	}
}
