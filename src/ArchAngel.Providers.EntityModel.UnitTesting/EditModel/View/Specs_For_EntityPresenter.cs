using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;

namespace Specs_For_EntityPresenter
{
	[TestFixture]
	public class Specs_For_EntityPresenter
	{
		[Test]
		public void Show_Property_Grid_Called()
		{
			IEntityForm form = MockRepository.GenerateMock<IEntityForm>();
			Entity obj = MockRepository.GenerateStub<Entity>();
			obj.Key = new EntityKeyImpl();			
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			EntityPresenter presenter = new EntityPresenter(panel, form);
			presenter.AttachToModel(obj);
			presenter.Show();

			panel.AssertWasCalled(p => p.ShowPropertyGrid(form));
		}

		[Test]
		public void Clear_Called()
		{
			IEntityForm form = MockRepository.GenerateMock<IEntityForm>();
			Entity obj = MockRepository.GenerateStub<Entity>();
			obj.Key = new EntityKeyImpl();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			var presenter = new EntityPresenter(panel, form);
			presenter.AttachToModel(obj);

			form.AssertWasCalled(f => f.Clear());
		}

		[Test]
		public void Property_Changed_Registered()
		{
			IEntityForm form = MockRepository.GenerateMock<IEntityForm>();
			Entity obj = MockRepository.GenerateStub<Entity>();
			obj.Key = new EntityKeyImpl();
			obj.EntitySet = new EntitySetImpl();
			obj.EntitySet.MappingSet = MockRepository.GenerateMock<MappingSet>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			var presenter = new EntityPresenter(panel, form);
			presenter.AttachToModel(obj);

			obj.AssertWasCalled(o => o.PropertyChanged += null, c => c.IgnoreArguments());
			obj.EntitySet.MappingSet.AssertWasCalled(ms => ms.PropertyChanged += null, c => c.IgnoreArguments());
		}

		[Test]
		public void The_Presenter_Hooks_Up_To_The_Right_Events_On_The_Form()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IEntityForm form = MockRepository.GenerateMock<IEntityForm>();

			new EntityPresenter(mainPanel, form);

			form.AssertWasCalled(f => f.NameChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.AddNewProperty += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.PropertyNameChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.CreateNewTableFromEntity += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.RemoveEntity += null, c => c.IgnoreArguments());
            form.AssertWasCalled(f => f.RemoveProperty += null, c => c.IgnoreArguments());
            form.AssertWasCalled(f => f.EditProperty += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.MappingsChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.NewMappingAdded += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.CopyProperty += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.MappingRemoved += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.SingleMappingChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.DiscriminatorChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.ParentEntityChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.ChildEntityAdded += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.ChildEntityRemoved += null, c => c.IgnoreArguments());
		}

		[Test]
		public void Property_Changed_Deregistered()
		{
			IEntityForm form = MockRepository.GenerateMock<IEntityForm>();
			Entity obj = MockRepository.GenerateStub<Entity>();
			obj.Key = new EntityKeyImpl();
			obj.EntitySet = new EntitySetImpl();
			obj.EntitySet.MappingSet = MockRepository.GenerateMock<MappingSet>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			var presenter = new EntityPresenter(panel, form);
			presenter.AttachToModel(obj);
			presenter.DetachFromModel();

			obj.AssertWasCalled(o => o.PropertyChanged -= null, c => c.IgnoreArguments());
			obj.EntitySet.MappingSet.AssertWasCalled(ms => ms.PropertyChanged -= null, c => c.IgnoreArguments());
		}

		[Test]
		public void The_Presenter_Fills_In_The_Form()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IEntityForm form = MockRepository.GenerateMock<IEntityForm>();

			form.Expect(f => f.Mappings = null)
				.IgnoreArguments()
				.WhenCalled(action => Assert.That(((IEnumerable<Mapping>) action.Arguments[0]).Count(), Is.EqualTo(0)));
			form.Expect(f => f.SetAvailableTables(null))
				.IgnoreArguments()
				.WhenCalled(action => Assert.That(((IEnumerable<ITable>)action.Arguments[0]).Count(), Is.EqualTo(0)));

			form.Expect(f => f.SetProperties(null))
				.IgnoreArguments()
				.WhenCalled(action => Assert.That(((IEnumerable<Property>)action.Arguments[0]).Count(), Is.EqualTo(1)));

			form.Expect(f => f.SetAvailableEntities(null))
				.IgnoreArguments()
				.WhenCalled(action => Assert.That(((IEnumerable<Entity>)action.Arguments[0]).Count(), Is.EqualTo(2)));

			form.Expect(f => f.SetChildEntities(null))
				.IgnoreArguments()
				.WhenCalled(action => Assert.That(((IEnumerable<Entity>)action.Arguments[0]).Count(), Is.EqualTo(1)));

			Entity parentEntity = new EntityImpl("Parent");
			Entity childEntity = new EntityImpl("Child");
			Property property = new PropertyImpl("Prop1");
			EntityKey key = new EntityKeyImpl();
			Entity entity = new EntityImpl("Entity1") { Key = key };
			entity.Parent = parentEntity;
			entity.AddChild(childEntity);
			entity.AddProperty(property);
			key.AddProperty(property);
			
			EntitySet es = new EntitySetImpl();
			es.AddEntity(parentEntity);
			es.AddEntity(entity);
			es.AddEntity(childEntity);
			MappingSet ms = new MappingSetImpl();
			ms.EntitySet = es;

			var presenter = new EntityPresenter(mainPanel, form);
			presenter.AttachToModel(entity);

			form.AssertWasCalled(f => f.EntityName = entity.Name);
			form.AssertWasCalled(f => f.Discriminator = entity.Discriminator);
			form.AssertWasCalled(f => f.ParentEntity = entity.Parent);
			form.AssertWasCalled(f => f.SetVirtualProperties(entity.Ex));
			form.VerifyAllExpectations();
		}

		[Test]
		public void Triggering_The_DiscriminatorChanged_Event_Changes_The_Discriminator()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IEntityForm form = MockRepository.GenerateMock<IEntityForm>();
			Entity entity = MockRepository.GenerateMock<Entity>();

			form.Stub(f => f.Discriminator).Return(new DiscriminatorImpl());

			var presenter = new EntityPresenter(mainPanel, form);
			presenter.AttachToModel(entity);

			form.Raise(f => f.DiscriminatorChanged += null, form, new EventArgs());

			entity.AssertWasCalled(e => e.Discriminator = form.Discriminator);
		}

		[Test]
		public void Triggering_The_PropertyNameChanged_Event_Changes_The_Property()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IEntityForm form = MockRepository.GenerateMock<IEntityForm>();
			Entity entity = MockRepository.GenerateMock<Entity>();
			Property property = MockRepository.GenerateMock<Property>();

			var presenter = new EntityPresenter(mainPanel, form);
			presenter.AttachToModel(entity);

			form.Raise(f => f.PropertyNameChanged += null, form, new PropertyNameChangeEventArgs(property, "NewName"));

			property.AssertWasCalled(e => e.Name = "NewName");
		}

		[Test]
		public void Triggering_The_ParentEntityChanged_Event_Changes_The_Parent()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IEntityForm form = MockRepository.GenerateMock<IEntityForm>();
			Entity entity = MockRepository.GenerateMock<Entity>();

			form.Stub(f => f.ParentEntity).Return(new EntityImpl("Parent2"));

			var presenter = new EntityPresenter(mainPanel, form);
			presenter.AttachToModel(entity);

			form.Raise(f => f.ParentEntityChanged += null, form, new EventArgs());

			entity.AssertWasCalled(e => e.Parent = form.ParentEntity);
		}

		[Test]
		public void Triggering_The_ChildEntityAdded_Event_Adds_The_New_Child()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IEntityForm form = MockRepository.GenerateMock<IEntityForm>();
			Entity entity = MockRepository.GenerateMock<Entity>();
			Entity childEntity = MockRepository.GenerateMock<Entity>();

			var presenter = new EntityPresenter(mainPanel, form);
			presenter.AttachToModel(entity);

			form.Raise(f => f.ChildEntityAdded += null, form, new GenericEventArgs<Entity>(childEntity));

			entity.AssertWasCalled(e => e.AddChild(childEntity));
		}

		[Test]
		public void Triggering_The_ChildEntityRemoved_Event_Adds_The_New_Child()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IEntityForm form = MockRepository.GenerateMock<IEntityForm>();
			Entity entity = MockRepository.GenerateMock<Entity>();
			Entity childEntity = MockRepository.GenerateMock<Entity>();

			var presenter = new EntityPresenter(mainPanel, form);
			presenter.AttachToModel(entity);

			form.Raise(f => f.ChildEntityRemoved += null, form, new GenericEventArgs<Entity>(childEntity));

			entity.AssertWasCalled(e => e.RemoveChild(childEntity));
		}

		[Test]
		public void Triggering_The_AddProperty_Event_Adds_A_New_Property_To_The_Entity()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IEntityForm form = MockRepository.GenerateMock<IEntityForm>();

			Entity entity = new EntityImpl("Entity1") { Key = new EntityKeyImpl() };
			entity.AddProperty(new PropertyImpl("Prop1"));

			form.Expect(f => f.SetProperties(Arg<IEnumerable<Property>>.List.ContainsAll(entity.ConcreteProperties)));

			var presenter = new EntityPresenter(mainPanel, form);
			presenter.AttachToModel(entity);

			form.Raise(f => f.AddNewProperty += null, form, new EventArgs());

			Assert.That(entity.Properties.Count(), Is.EqualTo(2));

			form.AssertWasCalled(f => f.Mappings = entity.Mappings(), c => c.Repeat.Twice());
			form.AssertWasCalled(f => f.SetSelectedPropertyName(entity.Properties.ElementAt(1)));
			form.VerifyAllExpectations();
		}

		[Test]
		public void Triggering_The_RemoveEntity_Event_Removes_It_From_The_Model_And_Clears_The_Screen()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IEntityForm form = MockRepository.GenerateMock<IEntityForm>();

			Entity entity = new EntityImpl("Entity1");
			EntitySet entitySet = new EntitySetImpl();
			entitySet.AddEntity(entity);

			var presenter = new EntityPresenter(mainPanel, form);
			presenter.AttachToModel(entity);

			form.Raise(f => f.RemoveEntity += null, form, new EventArgs());

			Assert.That(entity.EntitySet, Is.Null);
			Assert.That(entitySet.Entities.Count(), Is.EqualTo(0));
			// Assert that the main panel was instructed to clear the property grid.
			mainPanel.AssertWasCalled(m => m.ShowPropertyGrid(null));
		}

		[Test]
		public void Triggering_The_CreateTable_Event_Creates_A_Table_With_One_To_One_Mapping_To_The_Entity()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IEntityForm form = MockRepository.GenerateMock<IEntityForm>();

			Entity entity = new EntityImpl("Entity1");
			entity.AddProperty(new PropertyImpl("Property1"){Type = "System.Int32"});
			EntitySet entitySet = new EntitySetImpl();
			entitySet.AddEntity(entity);

			IDatabase database = new Database("DB1");
			new MappingSetImpl(database, entitySet);

			var presenter = new EntityPresenter(mainPanel, form);
			presenter.AttachToModel(entity);

			form.Raise(f => f.CreateNewTableFromEntity += null, form, new EventArgs());

			Assert.That(database.Tables.Count, Is.EqualTo(1));
			ITable table = database.Tables[0];
			Assert.That(table.Name, Is.EqualTo("Entity1s"));
			Assert.That(table.Columns.Count, Is.EqualTo(1));
			Assert.That(table.Columns[0].Name, Is.EqualTo("Property1"));
		}
	}

	[TestFixture]
	public class Mapping_Tests
	{
		private IMainPanel mainPanel;
		private IEntityForm form;
		private MappingSet ms;
		private Entity entity;
		private Mapping mapping;

		[SetUp]
		public void SetUp()
		{
			mainPanel = MockRepository.GenerateStub<IMainPanel>();
			form = MockRepository.GenerateMock<IEntityForm>();
			ms = MockRepository.GenerateStub<MappingSet>();

			Property property = new PropertyImpl("Prop1");
			EntityKey key = new EntityKeyImpl();
			entity = new EntityImpl("Entity1") { Key = key };
			entity.AddProperty(property);
			key.AddProperty(property);

			mapping = new MappingImpl();
			form.Stub(f => f.Mappings).Return(new List<Mapping> { mapping });

			EntitySet es = new EntitySetImpl();
			es.AddEntity(entity);
			ms.EntitySet = es;
			es.MappingSet = ms;
			ms.Stub(m => m.GetMappingsContaining(entity)).Return(new List<Mapping>());

			var presenter = new EntityPresenter(mainPanel, form);
			presenter.AttachToModel(entity);
		}

		[Test]
		public void Triggering_The_MappingsChanged_Event_Rewrites_The_Mappings()
		{
			form.Raise(f => f.MappingsChanged += null, form, new EventArgs());

			ms.AssertWasCalled(m => m.DeleteEntity(entity));
			ms.AssertWasCalled(m => m.AddMapping(mapping));
		}

		[Test]
		public void Triggering_The_NewMappingAdded_Event_Adds_That_Mapping()
		{
			form.Raise(f => f.NewMappingAdded += null, form, new MappingEventArgs(mapping));

			ms.AssertWasCalled(m => m.AddMapping(mapping));
		}

		[Test]
		public void Triggering_The_MappingRemoved_Event_Adds_That_Mapping()
		{
			form.Raise(f => f.MappingRemoved += null, form, new MappingEventArgs(mapping));

			ms.AssertWasCalled(m => m.RemoveMapping(mapping));
		}
	}
}