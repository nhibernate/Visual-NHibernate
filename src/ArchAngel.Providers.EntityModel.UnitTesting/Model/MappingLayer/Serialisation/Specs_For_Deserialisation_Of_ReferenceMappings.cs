using System;
using System.Collections.Generic;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common.ExtensionMethods;
using Specs_For_Serialisation_Of_ReferenceMapping;

namespace Specs_For_Deserialisation_Of_ReferenceMappings
{
	[TestFixture]
	public class When_Deserialising_A_ReferenceMapping_With_All_Fields_Set
	{
		private EntitySet entitySet;
		private Database database;
		private Table table;
		private Reference reference;

		[SetUp]
		public void Setup()
		{
			database = new Database("DB1");
			table = new Table("Table1");
			database.AddTable(table);

			reference = new ReferenceImpl(new Guid("11111111-1111-1111-1111-111111111111"));

			entitySet = MockRepository.GenerateMock<EntitySet>();
			entitySet.Stub(e => e.References).Return(new List<Reference> { reference }.AsReadOnly());
		}

		[Test]
		public void It_Should_Deserialise_To_This()
		{
			const string xml = When_Serialising_A_ReferenceMapping_With_All_Fields_Set.FullMappingXml;

			TableReferenceMapping mapping = new MappingSetDeserialisationScheme().DeserialiseReferenceMapping(xml.GetXmlDocRoot(), database, entitySet);

			TestMapping(mapping, table, reference);
		}

		public static void TestMapping(TableReferenceMapping mapping, ITable table, Reference reference)
		{
			Assert.That(mapping.FromTable, Is.SameAs(table));
			Assert.That(mapping.ToReference, Is.SameAs(reference));
		}
	}
}
