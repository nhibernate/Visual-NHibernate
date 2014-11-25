using System;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Specs_For_ReferencePresenter
{
	[TestFixture]
	public class When_Constructing_A_RelationshipPresenter
	{
		[Test]
		public void The_Presenter_Hooks_Up_To_The_Right_Events()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IReferenceForm form = MockRepository.GenerateMock<IReferenceForm>();

            //new ReferencePresenter(mainPanel, form);

			form.AssertWasCalled(f => f.Entity1Changed += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.Entity2Changed += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.End1EnabledChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.End2EnabledChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.End1NameChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.End2NameChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.End1CardinalityChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.End2CardinalityChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.MappedTableChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.MappedRelationshipChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.DeleteRelationship += null, c => c.IgnoreArguments());
		}
	}

	internal static class TestHelper
	{
		public static Reference GetReference()
		{
			Reference reference = new ReferenceImpl();

			var mappingSet = new MappingSetImpl();
			var entitySet = new EntitySetImpl();
			entitySet.AddReference(reference);
			mappingSet.EntitySet = entitySet;

			return reference;
		}
	}

	[TestFixture]
	public class When_Attaching_A_Reference
	{
		[Test]
		public void The_Presenter_Fills_In_The_Form()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IReferenceForm form = MockRepository.GenerateMock<IReferenceForm>();

			Reference reference = TestHelper.GetReference();

            //var presenter = new ReferencePresenter(mainPanel, form);
            //presenter.AttachToModel(reference);

			form.AssertWasCalled(f => f.Clear());
			form.AssertWasCalled(f => f.Entity1 = reference.Entity1);
			form.AssertWasCalled(f => f.Entity2 = reference.Entity1);
			form.AssertWasCalled(f => f.End1Enabled = reference.End1Enabled);
			form.AssertWasCalled(f => f.End2Enabled = reference.End2Enabled);
			form.AssertWasCalled(f => f.End1Name = reference.End1Name);
			form.AssertWasCalled(f => f.End2Name = reference.End2Name);
			form.AssertWasCalled(f => f.End1Cardinality = reference.Cardinality1);
			form.AssertWasCalled(f => f.End2Cardinality = reference.Cardinality2);
			form.AssertWasCalled(f => f.EntityList = reference.EntitySet.Entities);
			form.AssertWasCalled(f => f.MappedTableList = reference.PossibleMappedTables());
			form.AssertWasCalled(f => f.MappedRelationshipList = reference.PossibleMappedRelationships());
			form.AssertWasCalled(f => f.MappedTable = reference.MappedTable());
			form.AssertWasCalled(f => f.MappedRelationship = reference.MappedRelationship());
			form.AssertWasCalled(f => f.SetVirtualProperties(reference.Ex));
		}
	}

	[TestFixture]
	public class When_The_Form_Entity1_Changes
	{
		[Test]
		public void The_Reference_Should_Be_Updated()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IReferenceForm form = MockRepository.GenerateStub<IReferenceForm>();
			Reference reference = TestHelper.GetReference();

            //var presenter = new ReferencePresenter(mainPanel, form);
            //presenter.AttachToModel(reference);

			form.Entity1 = new EntityImpl("Entity1");
			form.GetEventRaiser(f => f.Entity1Changed += null).Raise(form, new EventArgs());

			Assert.That(reference.Entity1, Is.SameAs(form.Entity1));
		}
	}

	[TestFixture]
	public class When_The_Form_Entity2_Changes
	{
		[Test]
		public void The_Reference_Should_Be_Updated()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IReferenceForm form = MockRepository.GenerateStub<IReferenceForm>();
			Reference reference = TestHelper.GetReference();

            //var presenter = new ReferencePresenter(mainPanel, form);
            //presenter.AttachToModel(reference);

			form.Entity2 = new EntityImpl("Entity2");
			form.GetEventRaiser(f => f.Entity2Changed += null).Raise(form, new EventArgs());

			Assert.That(reference.Entity2, Is.SameAs(form.Entity2));
		}
	}

	[TestFixture]
	public class When_The_Form_End1Name_Changes
	{
		[Test]
		public void The_Reference_Should_Be_Updated()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IReferenceForm form = MockRepository.GenerateStub<IReferenceForm>();
			Reference reference = TestHelper.GetReference();

            //var presenter = new ReferencePresenter(mainPanel, form);
            //presenter.AttachToModel(reference);

			form.End1Name = "asdf";
			form.GetEventRaiser(f => f.End1NameChanged += null).Raise(form, new EventArgs());

			Assert.That(reference.End1Name, Is.EqualTo("asdf"));
		}
	}

	[TestFixture]
	public class When_The_Form_End2Name_Changes
	{
		[Test]
		public void The_Reference_Should_Be_Updated()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IReferenceForm form = MockRepository.GenerateStub<IReferenceForm>();
			Reference reference = TestHelper.GetReference();

            //var presenter = new ReferencePresenter(mainPanel, form);
            //presenter.AttachToModel(reference);

			form.End2Name = "asdf";
			form.GetEventRaiser(f => f.End2NameChanged += null).Raise(form, new EventArgs());

			Assert.That(reference.End2Name, Is.EqualTo("asdf"));
		}
	}


	[TestFixture]
	public class When_The_Form_End1Enabled_Changes
	{
		[Test]
		public void The_Reference_Should_Be_Updated()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IReferenceForm form = MockRepository.GenerateStub<IReferenceForm>();
			Reference reference = TestHelper.GetReference();

            //var presenter = new ReferencePresenter(mainPanel, form);
            //presenter.AttachToModel(reference);

			form.End1Enabled = false;
			form.GetEventRaiser(f => f.End1EnabledChanged += null).Raise(form, new EventArgs());

			Assert.That(reference.End1Enabled, Is.EqualTo(false));
		}
	}

	[TestFixture]
	public class When_The_Form_End2Enabled_Changes
	{
		[Test]
		public void The_Reference_Should_Be_Updated()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IReferenceForm form = MockRepository.GenerateStub<IReferenceForm>();
			Reference reference = TestHelper.GetReference();

            //var presenter = new ReferencePresenter(mainPanel, form);
            //presenter.AttachToModel(reference);

			form.End2Enabled = false;
			form.GetEventRaiser(f => f.End2EnabledChanged += null).Raise(form, new EventArgs());

			Assert.That(reference.End2Enabled, Is.EqualTo(false));
		}
	}

	[TestFixture]
	public class When_The_Form_End1Cardinality_Changes
	{
		[Test]
		public void The_Reference_Should_Be_Updated()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IReferenceForm form = MockRepository.GenerateStub<IReferenceForm>();
			Reference reference = TestHelper.GetReference();

            //var presenter = new ReferencePresenter(mainPanel, form);
            //presenter.AttachToModel(reference);

			form.End1Cardinality = new Cardinality(0, 5);
			form.GetEventRaiser(f => f.End1CardinalityChanged += null).Raise(form, new EventArgs());

			Assert.That(reference.Cardinality1, Is.SameAs(form.End1Cardinality));
		}
	}


	[TestFixture]
	public class When_The_Form_End2Cardinality_Changes
	{
		[Test]
		public void The_Reference_Should_Be_Updated()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IReferenceForm form = MockRepository.GenerateStub<IReferenceForm>();
			Reference reference = TestHelper.GetReference();

            //var presenter = new ReferencePresenter(mainPanel, form);
            //presenter.AttachToModel(reference);

			form.End2Cardinality = new Cardinality(0, 5);
			form.GetEventRaiser(f => f.End2CardinalityChanged += null).Raise(form, new EventArgs());

			Assert.That(reference.Cardinality2, Is.SameAs(form.End2Cardinality));
		}
	}

	[TestFixture]
	public class When_The_Cardinality_Is_Many_To_Many
	{
		[Test]
		public void The_MappedTable_Should_Be_Enabled()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IReferenceForm form = MockRepository.GenerateStub<IReferenceForm>();
			Reference reference = TestHelper.GetReference();
			reference.Cardinality1 = Cardinality.Many;
			reference.Cardinality2 = Cardinality.Many;

            //var presenter = new ReferencePresenter(mainPanel, form);
            //presenter.AttachToModel(reference);

			form.AssertWasCalled(f => f.MappedTableSelectionEnabled(true));
			form.AssertWasCalled(f => f.MappedRelationshipSelectionEnabled(false));
		}
	}

	[TestFixture]
	public class When_The_Cardinality_Is_Not_Many_To_Many
	{
		[Test]
		public void The_MappedTable_Should_Be_Enabled()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IReferenceForm form = MockRepository.GenerateStub<IReferenceForm>();
			Reference reference = TestHelper.GetReference();
			reference.Cardinality1 = Cardinality.One;
			reference.Cardinality2 = Cardinality.Many;

            //var presenter = new ReferencePresenter(mainPanel, form);
            //presenter.AttachToModel(reference);

			form.AssertWasCalled(f => f.MappedTableSelectionEnabled(false));
			form.AssertWasCalled(f => f.MappedRelationshipSelectionEnabled(true));
		}
	}

	[TestFixture]
	public class When_The_Form_MappedTable_Changes
	{
		[Test]
		public void The_Reference_Should_Be_Updated()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IReferenceForm form = MockRepository.GenerateStub<IReferenceForm>();
			Reference reference = TestHelper.GetReference();

            //var presenter = new ReferencePresenter(mainPanel, form);
            //presenter.AttachToModel(reference);

			form.MappedTable = new Table("Table1");
			form.GetEventRaiser(f => f.MappedTableChanged += null).Raise(form, new EventArgs());

			Assert.That(reference.MappedTable(), Is.SameAs(form.MappedTable));
		}
	}

	[TestFixture]
	public class When_The_Form_MappedRelationship_Changes
	{
		[Test]
		public void The_Reference_Should_Be_Updated()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IReferenceForm form = MockRepository.GenerateStub<IReferenceForm>();
			Reference reference = TestHelper.GetReference();

            //var presenter = new ReferencePresenter(mainPanel, form);
            //presenter.AttachToModel(reference);

			form.MappedRelationship = new RelationshipImpl();
			form.GetEventRaiser(f => f.MappedRelationshipChanged += null).Raise(form, new EventArgs());

			Assert.That(reference.MappedRelationship(), Is.SameAs(form.MappedRelationship));
		}
	}
}
