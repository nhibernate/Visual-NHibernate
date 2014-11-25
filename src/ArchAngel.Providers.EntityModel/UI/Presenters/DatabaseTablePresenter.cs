using System;
using System.ComponentModel;
using System.Linq;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class DatabaseTablePresenter : PresenterBase
	{
		public ITable Table { get; private set; }

		private readonly ITableForm form;

		public DatabaseTablePresenter(IMainPanel panel, ITableForm form) : base(panel)
		{
			this.form = form;
			SetupForm();

			form.DeleteEntity += form_DeleteEntity;
            form.AddNewColumn += form_AddNewColumn;
			form.DeleteColumn += form_DeleteColumn;
			form.AddNewKey += form_AddNewKey;
			form.DeleteKey += form_DeleteKey;

			form.EntityNameChanged += (sender, e) => { if (!Detached) Table.Name = form.EntityName; };
			form.DescriptionChanged += (sender, e) => { if (!Detached) Table.Description = form.Description; };
			form.EditColumn += ((s, e) => { if (Detached) return; mainPanel.ShowObjectPropertyGrid(e.Object); });
			form.EditKey += ((s, e) => { if (Detached) return; mainPanel.ShowObjectPropertyGrid(e.Object); });
		}

		void form_AddNewKey(object sender, EventArgs e)
		{
			string name = "New_Key".GetNextName(Table.Keys.Select(p => p.Name));
			var key = new Key(name);
			Table.AddKey(key);

			form.SetKeys(Table.Keys);
		}

		void form_DeleteKey(object sender, Slyce.Common.GenericEventArgs<IKey> e)
		{
			if (Detached) return;

			Table.RemoveKey(e.Object);
		}

		void form_DeleteColumn(object sender, Slyce.Common.GenericEventArgs<IColumn> e)
		{
			if (Detached) return;

			Table.RemoveColumn(e.Object);
		}

		void form_DeleteEntity(object sender, EventArgs e)
		{
			if (Detached) return;
            
            //mainPanel.DisableDiagramRefresh();
		    var table = Table;
		    var database = table.Database;
		    DetachFromModel();
            
			//mainPanel.ShowPropertyGrid(null);
			table.DeleteSelf();
            //mainPanel.EnableDiagramRefresh();
            //mainPanel.ShowOnDiagram(database);
		}

        void form_AddNewColumn(object sender, EventArgs e)
        {
            string name = "New_Column".GetNextName(Table.Columns.Select(p => p.Name));
            var column = new Column(name);
            Table.AddColumn(column);

            form.SetColumns(Table.Columns);
            form.SetSelectedColumnName(column);
        }

		public void AttachToModel(ITable entity)
		{
			if (!Detached) DetachFromModel();

			Table = entity;
			Table.PropertyChanged += Entity_PropertyChanged;

			Detached = false;
			SetupForm();
		}

		public override void DetachFromModel()
		{
			if (Detached || Table == null) return;

			Table.PropertyChanged -= Entity_PropertyChanged;

			Table = null;
			Detached = true;
			SetupForm();
		}

		internal override void AttachToModel(object obj)
		{
			if (obj is ITable == false)
				throw new ArgumentException("Model must be an ITable");
			AttachToModel((ITable)obj);
		}

		private void SetupForm()
		{
			if (Detached)
			{
				//mainPanel.ShowPropertyGrid(null);
				return;
			}

			form.Clear();
			form.Title = "Table";

			form.EntityName = Table.Name;
			form.Description = Table.Description;
            form.SetColumns(Table.Columns);
			form.SetVirtualProperties(Table.Ex);
			form.SetKeys(Table.Keys);
		}

		private void Entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
            form.RefreshVirtualProperties();

			switch (e.PropertyName)
			{
				case "Name":
					form.EntityName = Table.Name;
					break;
				case "Description":
					form.Description = Table.Description;
					break;
                case "Columns":
                    form.SetColumns(Table.Columns);
			        break;
				case "Keys":
					form.SetKeys(Table.Keys);
					break;
			}
		}

		public override void Show()
		{
			ShowPropertyGrid(form);
		}
	}
}