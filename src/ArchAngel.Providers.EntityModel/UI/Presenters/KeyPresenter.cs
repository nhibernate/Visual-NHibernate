using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class KeyPresenter : PresenterBase
	{
		private readonly IKeyForm form;
		public IKey Key
		{
			get;
			private set;
		}

		public KeyPresenter(IMainPanel panel, IKeyForm form) : base(panel)
		{	
			this.form = form;
			
			SetupForm();
			form.KeyNameChanged += (sender, e) => { if (!Detached) Key.Name = form.KeyName; };
			form.KeytypeChanged += (sender, e) => { if (!Detached) Key.Keytype = form.Keytype; };
			form.DescriptionChanged += (sender, e) => { if (!Detached) Key.Description = form.Description; };
			form.EditColumn += (sender, e) => { if (!Detached) mainPanel.ShowObjectPropertyGrid(form.SelectedColumn); };
			form.DeleteKey += form_DeleteKey;
			form.AddNewColumn += form_AddNewColumn;
			form.RemoveColumn += form_RemoveColumn;
		}

		void form_RemoveColumn(object sender, Slyce.Common.GenericEventArgs<IColumn> e)
		{
			if (Detached) return;
			
			Key.RemoveColumn(e.Object);
		}

		void form_AddNewColumn(object sender, Slyce.Common.GenericEventArgs<IColumn> e)
		{
			if (Detached) return;

			if(e.Object.Parent.InternalIdentifier != Key.Parent.InternalIdentifier)
				throw new ArgumentException("Cannot add column to key - it is from a different table.");

			Key.AddColumn(e.Object.Name);
			SetAvailableColumns();
		}

		private void form_DeleteKey(object sender, EventArgs e)
		{
			if (Detached) return;

			mainPanel.ShowPropertyGrid(null);
			Key.DeleteSelf();
		}

		public void AttachToModel(IKey key)
		{
			if (!Detached) DetachFromModel();

			Key = key;
			key.PropertyChanged += Key_PropertyChanged;
			Detached = false;
			SetupForm();
		}

		public override void DetachFromModel()
		{
			if (Detached || Key == null) return;

			Key.PropertyChanged -= Key_PropertyChanged;
			Key = null;
			Detached = true;
			SetupForm();
		}

		internal override void AttachToModel(object obj)
		{
			if (obj is IKey == false)
				throw new ArgumentException("Model must be an IKey");
			AttachToModel((IKey)obj);
		}

		private void SetupForm()
		{
			if (Detached)
			{
				mainPanel.ShowPropertyGrid(null);
				return;
			}

			form.Clear();
			form.KeyName = Key.Name;
			form.Keytype = Key.Keytype;
			form.Description = Key.Description;
			form.Columns = Key.Columns;
			form.SetVirtualProperties(Key.Ex);

			SetAvailableColumns();
		}

		private void SetAvailableColumns()
		{
			IEnumerable<IColumn> columns = Key.Parent != null ? Key.Parent.Columns.Except(Key.Columns) : new List<IColumn>();

			form.SetAvailableColumns(columns);
		}

		private void Key_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
            form.RefreshVirtualProperties();

			switch (e.PropertyName)
			{
				case "Name":
					form.KeyName = Key.Name;
					break;
				case "Keytype":
					form.Keytype = Key.Keytype;
					break;
				case "Description":
					form.Description = Key.Description;
					break;
				case "Columns":
					form.Columns = Key.Columns;
					break;
			}
		}

		public override void Show()
		{
			ShowPropertyGrid(form);
		}
	}
}