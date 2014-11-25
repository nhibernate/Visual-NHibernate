using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class SimpleDBMergeResultPresenter : PresenterBase
	{
		private readonly ISimpleDBMergeResultForm form;
		private DatabaseMergeResult results;

		public SimpleDBMergeResultPresenter(
			ISimpleDBMergeResultForm form, 
			IMainPanel panel,
		    DatabaseMergeResult results) : base(panel)
		{
			this.form = form;
			this.results = results;

			SetupForm();
		}

		private void SetupForm()
		{
			form.AddedTableOperations = results.TableOperations.Where(r => r is TableAdditionOperation);
			form.RemovedTableOperations = results.TableOperations.Where(r => r is TableRemovalOperation);
			
			form.ChangesAccepted += (sender, e) => AcceptChanges();
			form.ChangesCancelled += (sender, e) => CancelChanges();
		}

		private void CancelChanges()
		{
			mainPanel.CloseDatabaseRefreshResultsForm(Changes.WereRejected);
		}

		private void AcceptChanges()
		{
			List<ITwoStepMergeOperation> twoStepOps = new List<ITwoStepMergeOperation>();
			RunOperations(form.SelectedAddedTableOperations, twoStepOps);
			RunOperations(form.SelectedRemovedTableOperations, twoStepOps);
			RunOperations(results.ColumnOperations, twoStepOps);
			RunOperations(results.IndexOperations, twoStepOps);
			RunOperations(results.KeyOperations, twoStepOps);

			foreach (var op in twoStepOps)
			{
				op.RunSecondStep();
			}

			mainPanel.CloseDatabaseRefreshResultsForm(Changes.WereAccepted);
		}

		private void RunOperations<T>(IEnumerable<IMergeOperation<T>> operations, ICollection<ITwoStepMergeOperation> twoStepOps) where T : class
		{
			foreach(var op in operations)
			{
				op.RunOperation();
				if (op is ITwoStepMergeOperation)
					twoStepOps.Add(op as ITwoStepMergeOperation);
			}
		}

		public override void DetachFromModel()
		{
			results = null;
			Detached = true;
		}

		internal override void AttachToModel(object obj)
		{
			// Do nothing, this presenter doesn't attach to things.
		}

		public override void Show()
		{
			mainPanel.ShowDatabaseRefreshResultsForm(form);
		}
	}
}
