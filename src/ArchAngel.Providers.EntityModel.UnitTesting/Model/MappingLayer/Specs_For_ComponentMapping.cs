using System.Linq;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_ComponentMapping
{
	[TestFixture]
	public class Property_Change_Notifications
	{
		private ComponentMapping mapping;

		[SetUp]
		public void SetUp()
		{
			mapping = new ComponentMappingImpl();
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
		public void ToComponent()
		{
			bool eventFired = false;
			mapping.PropertyChanged += (s, e) => { if (e.PropertyName == "ToComponent") eventFired = true; };

			mapping.ToComponent = new ComponentImpl();
			Assert.That(eventFired, Is.True);
		}
	}

	[TestFixture]
	public class When_Adding_A_Component
	{
		private ComponentMapping mapping;
		private MappingSet mappingSet;

		[SetUp]
		public void SetUp()
		{
			mappingSet = new MappingSetImpl();
			mapping = new ComponentMappingImpl();
		}

		[Test]
		public void It_Is_Added_Correctly()
		{
			mappingSet.AddMapping(mapping);

			Assert.That(mappingSet.ComponentMappings.Contains(mapping));
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
			mapping.ToComponent = new ComponentImpl();
			mappingSet.AddMapping(mapping);

			Assert.That(mappingSet.GetMappedTableFor(mapping.ToComponent), Is.SameAs(mapping.FromTable));
		}

		[Test]
		public void Can_Find_Mapped_Reference()
		{
			// No names, should be searching on object identity
			mapping.FromTable = new Table();
			mapping.ToComponent = new ComponentImpl();
			mappingSet.AddMapping(mapping);

			var components = mappingSet.GetMappedComponentsFor(mapping.FromTable);
			Assert.That(components.Count(), Is.EqualTo(1));
			Assert.That(components.ElementAt(0), Is.SameAs(mapping.ToComponent));
		}
	}

	[TestFixture]
	public class When_Deleting_A_Component
	{
		private ComponentMapping mapping;
		private MappingSet mappingSet;

		[SetUp]
		public void SetUp()
		{
			mappingSet = new MappingSetImpl();
			mapping = new ComponentMappingImpl();
			mappingSet.AddMapping(mapping);
		}

		[Test]
		public void It_Should_Be_Removed_From_The_Collection()
		{
			Assert.That(mappingSet.ComponentMappings.Contains(mapping), Is.True);

			mapping.Delete();

			Assert.That(mappingSet.ComponentMappings.Contains(mapping), Is.False);
			Assert.That(mapping.MappingSet, Is.Null);
			Assert.That(mapping.FromTable, Is.Null);
			Assert.That(mapping.ToComponent, Is.Null);
		}
	}
}
