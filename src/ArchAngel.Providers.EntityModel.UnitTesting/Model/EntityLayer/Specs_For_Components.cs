using System.Linq;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace EntityLayer.Specs_For_Components
{
	[TestFixture]
	public class When_Working_With_A_OneToOne_Component
	{
		private MappingSet mappingSet;
		private EntitySet entitySet;
		private Database database;

		private ComponentMapping componentMapping;
		private Component component;
		private ComponentSpecification spec;

		[SetUp]
		public void Setup()
		{
			// Setup Database
			database = new Database("DB1");
			var table = new Table("User");
			table.AddColumn(new Column("Name"));
			table.AddColumn(new Column("AddressStreet"));
			table.AddColumn(new Column("AddressCity"));
			table.AddColumn(new Column("AddressCountry"));
			database.AddTable(table);

			// Setup Entities
			entitySet = new EntitySetImpl();
			Entity userEntity = new EntityImpl("User");
			userEntity.AddProperty(new PropertyImpl("Name"));

			// Create the Address type
			spec = new ComponentSpecificationImpl("Address");

			spec.AddProperty(new ComponentPropertyImpl("Street"));
			spec.AddProperty(new ComponentPropertyImpl("City"));
			spec.AddProperty(new ComponentPropertyImpl("Country"));

			// Create the Address component for the User entity.
			component = spec.CreateImplementedComponentFor(userEntity, "HomeAddress");

			entitySet.AddEntity(userEntity);
			entitySet.AddComponentSpecification(spec);

			// Setup the Mappings
			mappingSet = new MappingSetImpl(database, entitySet);
			componentMapping = new ComponentMappingImpl();
			mappingSet.AddMapping(componentMapping);

			componentMapping.AddPropertyAndColumn(component.Properties[0], table.Columns[1]);
			componentMapping.AddPropertyAndColumn(component.Properties[1], table.Columns[2]);
			componentMapping.AddPropertyAndColumn(component.Properties[2], table.Columns[3]);

			// Add the mapping between the Name property and the Name column in the database table.
			mappingSet.ChangeMappedColumnFor(userEntity.ConcreteProperties[0]).To(table.Columns[0]);
		}

		[Test]
		public void Can_Get_Component_From_Entity()
		{
			var entity = entitySet.GetEntity("User");
			Assert.That(entity.Components, Has.Count(1));
			Assert.That(entity.Components[0].ParentEntity, Is.SameAs(entity));
			Assert.That(entity.Components[0].Specification.Name, Is.EqualTo("Address"));
		}

		[Test]
		public void Can_Get_ComponentSpecfication_From_EntitySet()
		{
			var componentSpec = entitySet.GetComponentSpecification("Address");
			Assert.That(componentSpec, Is.Not.Null);
			Assert.That(componentSpec.Name, Is.EqualTo("Address"));
		}

		[Test]
		public void The_Spec_Has_A_Reference_To_The_Component()
		{
			var implComponent = spec.GetImplementationFor(entitySet.GetEntity("User"));
			Assert.That(implComponent, Is.Not.Null, "Could not find the component");
			Assert.That(implComponent.ParentEntity.Name, Is.EqualTo("User"));
		}

		[Test]
		public void Address_Component_Is_Mapped_To_User_Table()
		{
			var table = component.MappedTable();
			Assert.That(table, Is.Not.Null);
			Assert.That(table.Name, Is.EqualTo("User"));
		}

		[Test]
		public void Component_Properties_Are_Mapped_Correctly()
		{		
			var table = database.GetTable("User");

			Assert.That(component.Properties[0].MappedColumn(), Is.EqualTo(table.Columns[1]));
			Assert.That(component.Properties[1].MappedColumn(), Is.EqualTo(table.Columns[2]));
			Assert.That(component.Properties[2].MappedColumn(), Is.EqualTo(table.Columns[3]));
		}
	}

	[TestFixture]
	public class When_Deleting_A_ComponentSpecification
	{
		private MappingSet set;
		private Entity entity;
		private ITable table;
		private ComponentSpecification spec;
		private Component component;

		[Test]
		public void The_ComponentSpec_Is_Removed_Completely()
		{
			spec.DeleteSelf();

			Assert.That(set.EntitySet.ComponentSpecifications, Is.Empty);
			Assert.That(entity.Components, Is.Empty);
			Assert.That(set.ComponentMappings, Is.Empty);
		}

		[Test]
		public void The_Component_Is_Removed_Completely()
		{
			component.DeleteSelf();

			Assert.That(set.EntitySet.ComponentSpecifications, Is.Not.Empty, "The ComponentSpec was removed, but it shouldn't have been.");
			Assert.That(entity.Components, Is.Empty, "The Component itself was not removed.");
			Assert.That(set.ComponentMappings, Is.Empty, "The Component Mapping was not removed.");
		}

		[Test]
		public void The_Property_Is_Removed_Completely()
		{
			spec.Properties[0].DeleteSelf();

			Assert.That(set.EntitySet.ComponentSpecifications, Is.Not.Empty, "The ComponentSpec was removed, but it shouldn't have been.");
			Assert.That(entity.Components, Is.Not.Empty, "The Component itself was removed. This shouldn't have happened");
			Assert.That(set.ComponentMappings, Is.Empty, "The Component Mapping was not removed. It is empty and should be taken out.");
		}

		[SetUp]
		public void Setup()
		{
			set = new MappingSetImpl();
			entity = new EntityImpl("Entity1");
			set.EntitySet.AddEntity(entity);
			table = new Table("Table1");
			table.AddColumn(new Column("Street"));
			set.Database.AddTable(table);
			
			spec = new ComponentSpecificationImpl("Address");
			spec.AddProperty(new ComponentPropertyImpl("Street"));
			set.EntitySet.AddComponentSpecification(spec);

			component = spec.CreateImplementedComponentFor(entity, "HomeAddress");
			set.ChangeMappingFor(component.Properties[0]).To(table.Columns[0]);
		}
	}
}
