using System.Linq;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_Entities
{
	[TestFixture]
	public class When_Constructing_An_Entity
	{
		[Test]
		public void Nothing_Is_Null()
		{
			Entity entity = new EntityImpl();
			Assert.That(entity.Key, Is.Not.Null);
			Assert.That(entity.Properties, Is.Not.Null);
			Assert.That(entity.References, Is.Not.Null);
		}

		[Test]
		public void Everything_Is_Empty()
		{
			Entity entity = new EntityImpl();
			Assert.That(entity.Key.Properties, Is.Empty);
			Assert.That(entity.Properties, Is.Empty);
			Assert.That(entity.References, Is.Empty);
		}
	}

	[TestFixture]
	public class When_Creating_A_Reference
	{
		private Entity entity1;
		private Entity entity2;
		private EntitySet entitySet;

		[SetUp]
		public void SetUp()
		{
			entity1 = new EntityImpl("Entity1");
			entity2 = new EntityImpl("Entity2");			

			entitySet = new EntitySetImpl();
			entitySet.AddEntity(entity1);
			entitySet.AddEntity(entity2);
		}

		[Test]
		public void The_Reference_Is_Added_To_Both_Sides()
		{
			var reference = entity1.CreateReferenceTo(entity2);

			Assert.That(entity1.References.Contains(reference));
			Assert.That(entity2.References.Contains(reference));
		}

		[Test]
		public void The_Reference_Is_Added_To_The_EntitySet()
		{
			var reference = entity1.CreateReferenceTo(entity2);

			Assert.That(entitySet.References.Contains(reference));
		}
	}

	namespace Inheritance
	{
		[TestFixture]
		public class When_An_Entity_Has_No_Parent
		{
			private Entity entity;

			[SetUp]
			public void Setup()
			{
				entity = new EntityImpl();
			}

			[Test]
			public void Its_Parent_Is_Null()
			{
				Assert.That(entity.Parent, Is.Null);
			}

			[Test]
			public void It_Has_No_Inherited_Properties()
			{
				Assert.That(entity.InheritedProperties, Is.Not.Null);
				Assert.That(entity.InheritedProperties, Is.Empty);
			}
		}

		[TestFixture]
		public class When_An_Entity_Has_A_Parent
		{
			private Entity entity;
			private Entity parent;
			private Property property;

			[SetUp]
			public void Setup()
			{
				entity = new EntityImpl("Child");
				parent = new EntityImpl("Parent");
				parent.AddChild(entity);

				property = new PropertyImpl { Name = "Prop1"};
				parent.AddProperty(property);
			}

			[Test]
			public void Its_Parent_Is_Not_Null()
			{
				Assert.That(entity.Parent, Is.Not.Null);
				Assert.That(entity.Parent, Is.SameAs(parent));
			}

			[Test]
			public void It_Has_Some_Inherited_Properties()
			{
				Assert.That(entity.InheritedProperties.Count(), Is.EqualTo(1));
			}

			[Test]
			public void The_Properties_Collection_Contains_The_Parents_Properties()
			{
				Assert.That(entity.Properties, Is.EquivalentTo(parent.Properties.ToList()));
			}

			[Test]
			public void Overridden_Properties_Are_Marked_As_Inherited()
			{
				var property1 = new PropertyImpl { Name = "Prop1" };
				Assert.That(property1.IsInherited, Is.False);
				Assert.That(property.IsOverridden, Is.False);
				entity.AddProperty(property1);

				Assert.That(property.IsOverridden, Is.True);
				Assert.That(property1.IsInherited, Is.True);

				Assert.That(entity.Properties.Count(), Is.EqualTo(2));
				Assert.That(entity.Properties.Contains(property), "Parent property missing");
				Assert.That(entity.Properties.Contains(property1), "Child property missing");
			}

			[Test]
			public void Inherited_Properties_Are_Not_Included_In_ConcreteProperties()
			{
				var property1 = new PropertyImpl { Name = "Prop2" };
				entity.AddProperty(property1);

				Assert.That(entity.ConcreteProperties.Count, Is.EqualTo(1));
				Assert.That(entity.Properties.Contains(property1), "Child property missing");
			}
		}
	}

	[TestFixture]
	public class When_Dealing_With_Key_Properties
	{
		[Test]
		public void Adding_An_Existing_KeyProperty_Should_Add_It_To_The_EntityKey()
		{
			var entity = new EntityImpl();
			var property = new PropertyImpl();
			property.IsKeyProperty = true;

			entity.AddProperty(property);

			Assert.That(entity.Key.Properties.Contains(property), "Key doesn't contain the property");
		}

		[Test]
		public void Setting_IsKeyProperty_To_True_Should_Add_It_To_The_Key()
		{
			var entity = new EntityImpl();
			var property = new PropertyImpl();
			entity.AddProperty(property);

			Assert.IsFalse(entity.Key.Properties.Contains(property), "Key shouldn't already contain the property");

			property.IsKeyProperty = true;

			Assert.That(entity.Key.Properties.Contains(property), "Key doesn't contain the property");
		}

		[Test]
		public void Setting_IsKeyProperty_To_False_Should_Remove_It_From_The_Key()
		{
			var entity = new EntityImpl();
			var property = new PropertyImpl();
			property.IsKeyProperty = true;
			entity.AddProperty(property);

			Assert.IsTrue(entity.Key.Properties.Contains(property), "Key should already contain the property");

			property.IsKeyProperty = false;

			Assert.IsFalse(entity.Key.Properties.Contains(property), "Key shouldn't contain the property");
		}
	}
}