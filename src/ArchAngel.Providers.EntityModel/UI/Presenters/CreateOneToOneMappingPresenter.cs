using System.Linq;
using ArchAngel.Providers.EntityModel.Controller.MappingLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class CreateOneToOneMappingPresenter : PresenterBase
	{
		public ICreateOneToOneMappingsForm Form { get; set; }
		public MappingSet Set { get; set; }

		public CreateOneToOneMappingPresenter(ICreateOneToOneMappingsForm form, IMainPanel mainPanel, MappingSet set)
			: base(mainPanel)
		{
			Form = form;
			Set = set;

			form.ChangesAccepted += (s, e) => AcceptChanges();
			form.Cancelled += (s, e) => CancelChanges();
		}

		private void CancelChanges()
		{
			mainPanel.CloseDatabaseRefreshResultsForm(Changes.WereRejected);
		}

		private void AcceptChanges()
		{
			var tablesToProcess = Form.SelectedTables;

			MappingProcessor proc = new MappingProcessor(new OneToOneEntityProcessor(Set.EntitySet.Entities.Select(e => e.Name)));
			proc.CreateOneToOneMappingsFor(tablesToProcess, Set);

			mainPanel.CloseDatabaseRefreshResultsForm(Changes.WereAccepted);
		}

		public override void DetachFromModel()
		{
		}

		internal override void AttachToModel(object obj)
		{
		}

		public override void Show()
		{
			Form.SetAllTables(Set.Database.Tables);
			Form.SetAlreadyMappedTables(Set.Mappings.Select(m => m.FromTable));
			mainPanel.ShowCreateOneToOneMappingForm(Form);
		}
	}
}
