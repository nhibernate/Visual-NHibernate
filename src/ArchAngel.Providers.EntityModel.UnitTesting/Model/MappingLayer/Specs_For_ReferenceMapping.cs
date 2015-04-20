using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_ReferenceMapping
{
	[TestFixture]
	public class Property_Change_Notifications
	{
		private TableReferenceMapping mapping;

		[SetUp]
		public void SetUp()
		{
			mapping = new TableReferenceMappingImpl();
		}

		[Test]
		public void FromTable()
		{
			bool eventFired = false;
			mapping.PropertyChanged += (s, e) => { if (e.PropertyName == "FromTable") eventFired = true; };

			mapping.FromTable = new Table();
			Assert.That(eventFired, Is.True);
		}

		[Test]
		public void ToReference()
		{
			bool eventFired = false;
			mapping.PropertyChanged += (s, e) => { if (e.PropertyName == "ToReference") eventFired = true; };

			mapping.ToReference = new ReferenceImpl();
			Assert.That(eventFired, Is.True);
		}
	}

	[TestFixture]
	public class When_Adding_A_Reference
	{
		private TableReferenceMapping mapping;
		private MappingSet mappingSet;

		[SetUp]
		public void SetUp()
		{
			mappingSet = new MappingSetImpl();
			mapping = new TableReferenceMappingImpl();
		}

		[Test]
		public void It_Is_Added_Correctly()
		{
			mappingSet.AddMapping(mapping);

			Assert.That(mappingSet.ReferenceMappings.Contains(mapping));
		}

		[Test]
		public void It_Sets_The_Parent_Property()
		{
			mappingSet.AddMapping(mapping);

			Assert.That(mapping.MappingSet, Is.SameAs(mappingSet));
		}

		[Test]
		public void Can_Find_Mapped_Table()
		{
			// No names, should be searching on object identity
			mapping.FromTable = new Table();
			mapping.ToReference = new ReferenceImpl();
			mappingSet.AddMapping(mapping);

			Assert.That(mappingSet.GetMappedTableFor(mapping.ToReference), Is.SameAs(mapping.FromTable));
		}

		[Test]
		public void Can_Find_Mapped_Reference()
		{
			// No names, should be searching on object identity
			mapping.FromTable = new Table();
			mapping.ToReference = new ReferenceImpl();
			mappingSet.AddMapping(mapping);

			var references = mappingSet.GetMappedReferencesFor(mapping.FromTable);
			Assert.That(references.Count(), Is.EqualTo(1));
			Assert.That(references.ElementAt(0), Is.SameAs(mapping.ToReference));
		}
	}

	[TestFixture]
	public class When_Deleting_A_Reference
	{
		private TableReferenceMapping mapping;
		private MappingSet mappingSet;

		[SetUp]
		public void SetUp()
		{
			mappingSet = new MappingSetImpl();
			mapping = new TableReferenceMappingImpl();
			mappingSet.AddMapping(mapping);
		}

		[Test]
		public void It_Should_Be_Removed_From_The_Collection()
		{
			Assert.That(mappingSet.ReferenceMappings.Contains(mapping), Is.True);

			mapping.Delete();

			Assert.That(mappingSet.ReferenceMappings.Contains(mapping), Is.False);
			Assert.That(mapping.MappingSet, Is.Null);
			Assert.That(mapping.FromTable, Is.Null);
			Assert.That(mapping.ToReference, Is.Null);
		}
	}
}
