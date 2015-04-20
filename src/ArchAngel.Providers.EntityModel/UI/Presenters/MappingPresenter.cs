using System;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class MappingPresenter : PresenterBase
	{
		private readonly IMappingForm form;
		private Mapping mapping;

		public MappingPresenter(IMainPanel panel, IMappingForm form) : base(panel)
		{
			this.form = form;

			form.ToEntityChanged += (s, e) => { if (!Detached) mapping.ToEntity = form.ToEntity; };
			form.FromTableChanged += (s, e) => { if (!Detached) mapping.FromTable = form.FromTable; };
			form.MappingsChanged += (s, e) => { if (!Detached) mapping.SetMappings(form.Mappings); };
			form.RemoveMapping += form_RemoveMapping;
		}

		void form_RemoveMapping(object sender, EventArgs e)
		{
			mapping.Delete();
			mainPanel.ShowPropertyGrid(null);
		}

		private void SetupForm()
		{
			if (Detached) return;

			form.StartBulkUpdate();
			form.Clear();

			form.Entities = mapping.MappingSet.GetEntitiesFromEntitySet();
			form.Tables = mapping.MappingSet.GetEntitiesFromDatabase();
			form.ToEntity = mapping.ToEntity;
			if (mapping.FromTable != mapping.MappingSet.GetNullEntityObject())
				form.FromTable = mapping.FromTable;
			form.Mappings = mapping.Mappings;
			form.SetVirtualProperties(mapping.Ex);

			form.EndBulkUpdate();
		}

		public override void DetachFromModel()
		{
			if (Detached) return;

			mapping.PropertyChanged -= mapping_PropertyChanged;
			mapping = null;
			Detached = true;
			SetupForm();
		}

		internal override void AttachToModel(object obj)
		{
			if(obj is Mapping == false)
				throw new ArgumentException("obj must be of type Mapping to be attached to a MappingProcessor" );
			AttachToModel(obj as Mapping);
		}

		public void AttachToModel(Mapping obj)
		{
			if (!Detached) DetachFromModel();

			mapping = obj;
			mapping.PropertyChanged += mapping_PropertyChanged;
			Detached = false;

			SetupForm();
		}

		void mapping_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
            form.RefreshVirtualProperties();
		}

		public override void Show()
		{
			ShowPropertyGrid(form);
		}
	}
}