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

namespace Specs_For_Serialisation_Of_ComponentMappings
{
	[TestFixture]
	public class When_Serialising_A_Mapping_That_Is_Missing
	{
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void A_FromTable__It_Should_Throw_An_Exception()
		{
			var mapping = new ComponentMappingImpl();
			mapping.ToComponent = new ComponentImpl();
			new MappingSetSerialisationScheme().SerialiseComponentMapping(mapping);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void A_ToComponent__It_Should_Throw_An_Exception()
		{
			var mapping = new ComponentMappingImpl();
			mapping.FromTable = new Table();
			new MappingSetSerialisationScheme().SerialiseComponentMapping(mapping);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void ToProperties__It_Should_Throw_An_Exception()
		{
			var mapping = MockRepository.GenerateStub<ComponentMapping>();
			mapping.FromTable = new Table();
			mapping.ToComponent = new ComponentImpl(new ComponentSpecificationImpl(""), new EntityImpl(), "");
			mapping.Stub(m => m.FromColumns).Return(new List<IColumn> {new Column()}.AsReadOnly());
			mapping.Stub(m => m.ToProperties).Return(new List<ComponentPropertyMarker>().AsReadOnly());
			new MappingSetSerialisationScheme().SerialiseComponentMapping(mapping);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ToComponent_Spec__IT_Should_Throw_An_Exception()
		{
			var mapping = MockRepository.GenerateStub<ComponentMapping>();
			mapping.FromTable = new Table();
			mapping.ToComponent = new ComponentImpl(null, new EntityImpl(), "");
			mapping.Stub(m => m.FromColumns).Return(new List<IColumn> { new Column() }.AsReadOnly());
			mapping.Stub(m => m.ToProperties).Return(new List<ComponentPropertyMarker> { new ComponentPropertyMarker(new ComponentPropertyImpl("")) }.AsReadOnly());
			new MappingSetSerialisationScheme().SerialiseComponentMapping(mapping);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void FromColumns__It_Should_Throw_An_Exception()
		{
			var mapping = MockRepository.GenerateStub<ComponentMapping>();
			mapping.FromTable = new Table();
			mapping.ToComponent = new ComponentImpl(new ComponentSpecificationImpl(""), new EntityImpl(), "");
			mapping.Stub(m => m.FromColumns).Return(new List<IColumn>().AsReadOnly());
			mapping.Stub(m => m.ToProperties).Return(new List<ComponentPropertyMarker> { new ComponentPropertyMarker(new ComponentPropertyImpl("")) }.AsReadOnly());
			new MappingSetSerialisationScheme().SerialiseComponentMapping(mapping);
		}
	}

	[TestFixture]
	public class When_Serialising_A_Mapping_With_All_Fields_Set
	{
		public const string FullMappingXml = "<ComponentMapping><FromTable>Table1</FromTable><ToComponent specification=\"Address\" parent-entity=\"Entity1\" name=\"HomeAddress\" /><FromColumns><Column>AddressStreet</Column></FromColumns><ToProperties><Property>Street</Property></ToProperties></ComponentMapping>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			const string expectedXML = FullMappingXml;

			string outputXML = new MappingSetSerialisationScheme().SerialiseComponentMapping(GetMapping());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}

		public static ComponentMapping GetMapping()
		{
			ComponentMapping mapping = new ComponentMappingImpl();
			mapping.FromTable = new Table("Table1");
			mapping.FromTable.AddColumn(new Column("AddressStreet"));
			mapping.ToComponent = new ComponentImpl(new ComponentSpecificationImpl("Address"), new EntityImpl("Entity1"), "HomeAddress");
			mapping.ToComponent.AddProperty(new ComponentPropertyMarker(new ComponentPropertyImpl("Street")));
			mapping.AddPropertyAndColumn(mapping.ToComponent.Properties[0], mapping.FromTable.Columns[0]);

			return mapping;
		}
	}
}
