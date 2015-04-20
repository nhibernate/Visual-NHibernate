using System;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;

namespace Specs_For_Serialisation_Of_ReferenceMapping
{
	[TestFixture]
	public class When_Serialising_A_ReferenceMapping_With_All_Fields_Set
	{
		public const string FullMappingXml = "<TableReferenceMapping><FromTable>Table1</FromTable><ToReference>11111111-1111-1111-1111-111111111111</ToReference></TableReferenceMapping>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = FullMappingXml;

			string outputXML = new MappingSetSerialisationScheme().SerialiseReferenceMapping(GetMapping());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}

		public static TableReferenceMapping GetMapping()
		{
			TableReferenceMapping mapping = new TableReferenceMappingImpl();
			mapping.FromTable = new Table("Table1");
			mapping.ToReference = new ReferenceImpl(new Guid("11111111-1111-1111-1111-111111111111"));
			mapping.MappingSet = new MappingSetImpl();

			return mapping;
		}
	}
}
