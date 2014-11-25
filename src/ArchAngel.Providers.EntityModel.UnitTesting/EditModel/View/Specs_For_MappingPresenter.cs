using System;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Specs_For_MappingPresenter
{
	[TestFixture]
	public class When_Constructing_A_MappingPresenter
	{
		[Test]
		public void The_Presenter_Hooks_Up_To_The_Right_Events()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IMappingForm form = MockRepository.GenerateMock<IMappingForm>();

			//new MappingPresenter(mainPanel, form);

			form.AssertWasCalled(f => f.ToEntityChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.FromTableChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.MappingsChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.RemoveMapping += null, c => c.IgnoreArguments());
		}

		[Test]
		public void Show_Was_Called()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IMappingForm form = MockRepository.GenerateMock<IMappingForm>();

            //var presenter = new MappingPresenter(mainPanel, form);
            //presenter.Show();

            //mainPanel.AssertWasCalled(m => m.ShowPropertyGrid(form));
		}
	}

	[TestFixture]
	public class When_Detaching_The_Model
	{
		[Test]
		public void Property_Changed_Deregistered()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IMappingForm form = MockRepository.GenerateMock<IMappingForm>();
			var mapping = MockRepository.GenerateStub<Mapping>();
			mapping.MappingSet = new MappingSetImpl();

            //var presenter = new MappingPresenter(mainPanel, form);
            //presenter.AttachToModel(mapping);
            //presenter.DetachFromModel();

			mapping.AssertWasCalled(o => o.PropertyChanged -= null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.Clear());
		}
	}


	[TestFixture]
	public class When_Attaching_A_Mapping
	{
		private IMainPanel mainPanel;
		private IMappingForm form;
		private Mapping mapping;
		//private MappingPresenter presenter;

		[SetUp]
		public void Setup()
		{
			mainPanel = MockRepository.GenerateStub<IMainPanel>();
			form = MockRepository.GenerateMock<IMappingForm>();
			mapping = MockRepository.GenerateStub<Mapping>();
			mapping.MappingSet = new MappingSetImpl();
			//presenter = new MappingPresenter(mainPanel, form);
		}

		[Test]
		public void Property_Changed_Registered()
		{
			//presenter.AttachToModel(mapping);

			mapping.AssertWasCalled(o => o.PropertyChanged += null, c => c.IgnoreArguments());
		}

		[Test]
		public void The_Presenter_Fills_In_The_Form()
		{
			var m = new MappingImpl();
			m.MappingSet = MockRepository.GenerateMock<MappingSet>();
			m.ToEntity = new EntityImpl("entity1");
			m.FromTable = new Table("table1");
			//presenter.AttachToModel(m);
			form.AssertWasCalled(f => f.Clear());
			form.AssertWasCalled(f => f.ToEntity = m.ToEntity);
			form.AssertWasCalled(f => f.FromTable = m.FromTable);
			form.AssertWasCalled(f => f.SetVirtualProperties(m.Ex));
			// Need to ignore arguments on these one as the description of the
			// object being set is too complex, and gets us very little in the
			// way of coverage. If someone screws this up, it'd be a pretty basic mistake,
			// and very obvious in the UI.
			form.AssertWasCalled(f => f.Mappings = m.Mappings, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.Entities = null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.Tables = null, c => c.IgnoreArguments());

			// If these methods were called, then it is very likely that they
			// were used to initialise the Entities and Tables collections,
			// unlesss someone is trying to break something on purpose.
			m.MappingSet.AssertWasCalled(ms => ms.GetEntitiesFromEntitySet());
			m.MappingSet.AssertWasCalled(ms => ms.GetEntitiesFromDatabase());
		}
	}

	[TestFixture]
	public class When_The_Form_Changes
	{
		private IMainPanel mainPanel;
		private IMappingForm form;
		private Mapping mapping;
		//private MappingPresenter presenter;

		[SetUp]
		public void Setup()
		{
			mainPanel = MockRepository.GenerateStub<IMainPanel>();
			form = MockRepository.GenerateStub<IMappingForm>();
			mapping = new MappingImpl();
			mapping.MappingSet = new MappingSetImpl();
            //presenter = new MappingPresenter(mainPanel, form);
            //presenter.AttachToModel(mapping);
		}

		[Test]
		public void The_Mapping_ToEntity_Should_Be_Updated()
		{
			form.ToEntity = new EntityImpl("Entity1");
			form.GetEventRaiser(f => f.ToEntityChanged += null).Raise(form, new EventArgs());

			Assert.That(mapping.ToEntity, Is.SameAs(form.ToEntity));
		}

		[Test]
		public void The_Mapping_FromTable_Should_Be_Updated()
		{
			form.FromTable = new Table("Table2");
			form.GetEventRaiser(f => f.FromTableChanged += null).Raise(form, new EventArgs());

			Assert.That(mapping.FromTable, Is.SameAs(form.FromTable));
		}

		[Test]
		public void The_Mapping_Mappings_Should_Be_Updated()
		{
			var m = new MappingImpl();
			m.FromTable = new Table("table1");
			m.ToEntity = new EntityImpl("entity1");
			m.MappingSet = new MappingSetImpl();

			var property = new PropertyImpl("Prop");
			var column = new Column("Col");
			m.FromTable.AddColumn(column);
			m.ToEntity.AddProperty(property);

			m.AddPropertyAndColumn(property, column);

			form.Mappings = m.Mappings;
			form.GetEventRaiser(f => f.FromTableChanged += null).Raise(form, new EventArgs());

			var colAndProp = m.Mappings.ElementAt(0);

			Assert.That(colAndProp.Property, Is.SameAs(property));
			Assert.That(colAndProp.Column, Is.SameAs(column));
		}

		[Test]
		public void The_Mapping_Should_Be_Deleted()
		{
			var m = new MappingImpl();
			var ms = new MappingSetImpl();

			ms.AddMapping(m);

			Assert.That(ms.Mappings.Contains(m), Is.True);
			//presenter.AttachToModel(m);

			form.Raise(f => f.RemoveMapping += null, form, new EventArgs());

			Assert.That(ms.Mappings.Contains(m), Is.False);
		}
	}
}
