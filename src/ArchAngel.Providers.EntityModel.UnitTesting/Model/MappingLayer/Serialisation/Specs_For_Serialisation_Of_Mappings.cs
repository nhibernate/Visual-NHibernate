using System;
using System.Linq;
using System.Collections.Generic;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;

namespace Specs_For_Serialisation_Of_Mapping
{
	[TestFixture]
	public class When_Serialising_An_Empty_Mapping
	{
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void It_Should_Throw_An_Exception()
		{
			new MappingSetSerialisationScheme().SerialiseMapping(new MappingImpl());
		}
	}

	[TestFixture]
	public class When_Serialising_A_Mapping_That_Is_Missing
	{
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void A_FromTable__It_Should_Throw_An_Exception()
		{
			var mapping = new MappingImpl();
			mapping.ToEntity = new EntityImpl();
			new MappingSetSerialisationScheme().SerialiseMapping(mapping);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void A_ToEntity__It_Should_Throw_An_Exception()
		{
			var mapping = new MappingImpl();
			mapping.FromTable = new Table();
			new MappingSetSerialisationScheme().SerialiseMapping(mapping);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void ToProperties__It_Should_Throw_An_Exception()
		{
			var mapping = MockRepository.GenerateStub<Mapping>();
			mapping.FromTable = new Table();
			mapping.ToEntity = new EntityImpl();
			mapping.Stub(m => m.FromColumns).Return(new List<IColumn> {new Column()}.AsReadOnly());
			mapping.Stub(m => m.ToProperties).Return(new List<Property>().AsReadOnly());
			new MappingSetSerialisationScheme().SerialiseMapping(mapping);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void FromColumns__It_Should_Throw_An_Exception()
		{
			var mapping = MockRepository.GenerateStub<Mapping>();
			mapping.FromTable = new Table();
			mapping.ToEntity = new EntityImpl();
			mapping.Stub(m => m.FromColumns).Return(new List<IColumn>().AsReadOnly());
			mapping.Stub(m => m.ToProperties).Return(new List<Property>{new PropertyImpl()}.AsReadOnly());
			new MappingSetSerialisationScheme().SerialiseMapping(mapping);
		}
	}

	[TestFixture]
	public class When_Serialising_A_Mapping_With_All_Fields_Set
	{
		public const string FullMappingXml = "<Mapping><FromColumns><Column>Column1</Column></FromColumns><FromTable>Table1</FromTable><ToEntity>Entity1</ToEntity><ToProperties><Property>Property1</Property></ToProperties></Mapping>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = FullMappingXml;

			string outputXML = new MappingSetSerialisationScheme().SerialiseMapping(GetMapping());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}

		public static Mapping GetMapping()
		{
			Mapping mapping = new MappingImpl();
			mapping.FromTable = new Table("Table1");
			mapping.FromTable.AddColumn(new Column("Column1"));
			mapping.ToEntity = new EntityImpl("Entity1");
			mapping.ToEntity.AddProperty(new PropertyImpl("Property1"));
			mapping.AddPropertyAndColumn(mapping.ToEntity.Properties.ElementAt(0), mapping.FromTable.Columns[0]);

			return mapping;
		}
	}
}
