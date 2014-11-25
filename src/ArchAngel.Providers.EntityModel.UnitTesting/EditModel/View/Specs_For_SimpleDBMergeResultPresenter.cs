using System.Collections.Generic;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using System.Linq;

namespace Specs_For_SimpleDBMergeResultPresenter
{
	[TestFixture]
	public class When_Creating_A_New_Presenter
	{
		[Test]
		public void The_Event_Handlers_Are_Registered()
		{
			var form = MockRepository.GenerateMock<ISimpleDBMergeResultForm>();
			var results = new DatabaseMergeResult();
			var panel = MockRepository.GenerateStub<IMainPanel>();

			new SimpleDBMergeResultPresenter(form, panel, results);

			form.AssertWasCalled(f => f.ChangesAccepted += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.ChangesCancelled += null, c => c.IgnoreArguments());
		}
		
		[Test]
		public void The_Form_Is_Set_Up_Properly()
		{
			var form = MockRepository.GenerateMock<ISimpleDBMergeResultForm>();
            var results = new DatabaseMergeResult();
			var panel = MockRepository.GenerateStub<IMainPanel>();
		    var addOp = new TableAdditionOperation(new Database("db"), new Table("table"));
            var remOp = new TableRemovalOperation(new Table("table"));

		    results.AddTableOperation(addOp);
		    results.AddTableOperation(remOp);

			new SimpleDBMergeResultPresenter(form, panel, results);

			// Not the nicest syntax, but this is a test to check that all 
			form.AssertWasCalled(f => f.AddedTableOperations = Arg<IEnumerable<IMergeOperation<ITable>>>.Matches(t => t.Count() == 1 &&
			                                                                                                 t.ElementAt(0) == addOp ));
			form.AssertWasCalled(f => f.RemovedTableOperations = Arg<IEnumerable<IMergeOperation<ITable>>>.Matches(t => t.Count() == 1 &&
																											 t.ElementAt(0) == remOp));
		}
	}

	[TestFixture]
	public class When_Showing_A_Presenter
	{
		[Test]
		public void It_Should_Call_Back_To_The_Main_Panel()
		{
			var form = MockRepository.GenerateMock<ISimpleDBMergeResultForm>();
			var results = new DatabaseMergeResult();
			var panel = MockRepository.GenerateMock<IMainPanel>();

			var presenter = new SimpleDBMergeResultPresenter(form, panel, results);
			presenter.Show();

			panel.AssertWasCalled(f => f.ShowDatabaseRefreshResultsForm(form));
		}
	}

	[TestFixture]
	public class When_Changes_Are_Cancelled
	{
		[Test]
		public void It_Should_Call_Back_To_The_Main_Panel()
		{
			var form = MockRepository.GenerateMock<ISimpleDBMergeResultForm>();
			var results = new DatabaseMergeResult();
			var panel = MockRepository.GenerateMock<IMainPanel>();
			
			var tableOp = MockRepository.GenerateMock<IMergeOperation<ITable>>();
			var columnOp = MockRepository.GenerateMock<IMergeOperation<IColumn>>();
			var keyOp = MockRepository.GenerateMock<IMergeOperation<IKey>>();
			var indexOp = MockRepository.GenerateMock<IMergeOperation<IIndex>>();
			results.AddTableOperation(tableOp);
			results.AddColumnOperation(columnOp);
			results.AddKeyOperation(keyOp);
			results.AddIndexOperation(indexOp);

			new SimpleDBMergeResultPresenter(form, panel, results);
			form.GetEventRaiser(f => f.ChangesCancelled += null).Raise(form, null);

			panel.AssertWasCalled(f => f.CloseDatabaseRefreshResultsForm(Changes.WereRejected));
			tableOp .AssertWasNotCalled(o => o.RunOperation());
			columnOp.AssertWasNotCalled(o => o.RunOperation());
			keyOp   .AssertWasNotCalled(o => o.RunOperation());
			indexOp .AssertWasNotCalled(o => o.RunOperation());
		}
	}

	[TestFixture]
	public class When_Changes_Are_Accepted
	{
		[Test]
		public void It_Should_Apply_Changes_And_Call_Back_To_The_Main_Panel()
		{
			var form = MockRepository.GenerateStub<ISimpleDBMergeResultForm>();
			var results = new DatabaseMergeResult();
			var panel = MockRepository.GenerateMock<IMainPanel>();
			
			// These are all two step processes
			var tableOp = new MockMergeOperation<ITable, Table>();
			var columnOp = new MockMergeOperation<IColumn, Column>();
			var keyOp = new MockMergeOperation<IKey, Key>();
			var indexOp = new MockMergeOperation<IIndex, Index>();

			results.AddTableOperation(tableOp);
			results.AddColumnOperation(columnOp);
			results.AddKeyOperation(keyOp);
			results.AddIndexOperation(indexOp);

			form.Stub(f => { var v = f.SelectedAddedTableOperations; }).Return(new List<IMergeOperation<ITable>> {tableOp});
			form.Stub(f => { var v = f.SelectedRemovedTableOperations; }).Return(new List<IMergeOperation<ITable>>());

			new SimpleDBMergeResultPresenter(form, panel, results);
			form.GetEventRaiser(f => f.ChangesAccepted += null).Raise(form, null);

			panel.AssertWasCalled(f => f.CloseDatabaseRefreshResultsForm(Changes.WereAccepted));

			Assert.That(tableOp.OperationRun, Is.True);
			Assert.That(indexOp.OperationRun, Is.True);
			Assert.That(columnOp.OperationRun, Is.True);
			Assert.That(keyOp.OperationRun, Is.True);

			Assert.That(tableOp.SecondStepRun, Is.True);
			Assert.That(indexOp.SecondStepRun, Is.True);
			Assert.That(columnOp.SecondStepRun, Is.True);
			Assert.That(keyOp.SecondStepRun, Is.True);
		}
	}

	internal class MockMergeOperation<T, Q> : IMergeOperation<T>, ITwoStepMergeOperation where T : class where Q : T, new()
	{
		public bool OperationRun;
		public bool SecondStepRun;
		public bool NotAppliedRun;

		public T Object
		{
			get { return new Q(); }
			set {  }
		}

		public string DisplayName
		{
			get { return ""; }
		}

		public void RunOperation()
		{
			OperationRun = true;
		}

		public void NotApplied()
		{
			NotAppliedRun = true;
		}

		public IEnumerable<ChangeToNote> ChangesToNote
		{
			get { return new List<ChangeToNote>(); }
		}

		public void RunSecondStep()
		{
			SecondStepRun = true;
		}
	}
}
