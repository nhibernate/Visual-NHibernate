using System;
using System.ComponentModel;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class IndexPresenter : PresenterBase
	{
		private readonly IIndexForm form;

		public IIndex Index
		{
			get;
			private set;
		}

		public IndexPresenter(IMainPanel panel, IIndexForm form)
			: base(panel)
		{
			
			this.form = form;

			SetupForm();

			form.IndexNameChanged += (sender, e) => { if (!Detached) Index.Name = form.IndexName; };
			form.DatatypeChanged += (sender, e) => { if (!Detached) Index.Datatype = form.Datatype; };
			form.DescriptionChanged += (sender, e) => { if (!Detached) Index.Description = form.Description; };
			form.SelectedColumnChanged += (sender, e) => { if (!Detached) mainPanel.ShowObjectPropertyGrid(form.SelectedColumn); };
			form.DeleteColumn += form_DeleteColumn;
		}

		void form_DeleteColumn(object sender, EventArgs e)
		{
			if (Detached) return;

			mainPanel.ShowPropertyGrid(null);
			Index.DeleteSelf();
		}

		public void AttachToModel(IIndex index)
		{
			if (!Detached) DetachFromModel();

			Index = index;
			index.PropertyChanged += Index_PropertyChanged;
			Detached = false;
			SetupForm();
		}

		public override void DetachFromModel()
		{
			if (Detached || Index == null) return;

			Index.PropertyChanged -= Index_PropertyChanged;
			Index = null;
			Detached = true; 
			SetupForm();
		}

		internal override void AttachToModel(object obj)
		{
			if (obj is IIndex == false)
				throw new ArgumentException("Model must be an IDatabase");
			AttachToModel((IIndex)obj);
		}

		private void SetupForm()
		{
			if (Detached)
			{
				mainPanel.ShowPropertyGrid(null);
				return;
			}

			form.Clear();
			form.IndexName = Index.Name;
			form.Datatype = Index.Datatype;
			form.Description = Index.Description;
			form.Columns = Index.Columns;
			form.SetVirtualProperties(Index.Ex);
		}

		private void Index_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
            form.RefreshVirtualProperties();

			switch (e.PropertyName)
			{
				case "Name":
					form.IndexName = Index.Name;
					break;
				case "Datatype":
					form.Datatype = Index.Datatype;
					break;
				case "Description":
					form.Description = Index.Description;
					break;
				case "Columns":
					form.Columns = Index.Columns;
					break;
			}
		}

		public override void Show()
		{
			ShowPropertyGrid(form);
		}
	}
}