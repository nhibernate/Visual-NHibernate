using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Slyce.Common;
using Slyce.Common.EventExtensions;
using System.Linq;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormDatabaseTable : UserControl, ITableForm, IEventSender
	{
		public FormDatabaseTable()
        {
            InitializeComponent();

			textBoxName.TextChanged += (sender, e) => EntityNameChanged.RaiseEventEx(this);
			textBoxDescription.TextChanged += (sender, e) => DescriptionChanged.RaiseEventEx(this);
        }

		public FormDatabaseTable(string title) : this()
		{
			Title = title;
		}

		public string Title 
		{ 
			get 
			{
				return panelEx3.Text; 
			}
			set
			{
				panelEx3.Text = value;
			}

		}

		public string EntityName
		{
			get { return textBoxName.Text; }
			set
			{
				EventRaisingDisabled = true;
				textBoxName.Text = value;
				EventRaisingDisabled = false;
			}
		}

		public string Description
		{
			get { return textBoxDescription.Text; }
			set
			{
				EventRaisingDisabled = true;
				textBoxDescription.Text = value;
				EventRaisingDisabled = false;
			}
		}

	    public void SetColumns(IEnumerable<IColumn> columns)
	    {
            listBoxColumns.Items.Clear();

	        foreach(var col in columns)
	        {
	            listBoxColumns.Items.Add(new ComboBoxItemEx<IColumn>(col, c => c.Name));
	        }
	    }

	    public void SetSelectedColumnName(IColumn column)
	    {
	        listBoxColumns.SelectedItem =
	            listBoxColumns.Items.Cast<ComboBoxItemEx<IColumn>>().First(i => i.Object == column);
	    }

		public void SetKeys(IEnumerable<IKey> keys)
		{
			listBoxKeys.Items.Clear();

			foreach (var key in keys)
			{
				listBoxKeys.Items.Add(new ComboBoxItemEx<IKey>(key, k => k.Name));
			}
		}

		private void buttonAddNewKey_Click(object sender, EventArgs e)
		{
			AddNewKey.RaiseEventEx(this);
		}

		private void buttonDeleteKey_Click(object sender, EventArgs e)
		{
			var key = listBoxKeys.GetSelectedItem<IKey>();
			if (key != null)
			{
				DeleteKey.RaiseEventEx(this, new GenericEventArgs<IKey>(key));
			}
		}

		private void buttonEditKey_Click(object sender, EventArgs e)
		{
			var key = listBoxKeys.GetSelectedItem<IKey>();
			if (key != null)
			{
				EditKey.RaiseEventEx(this, new GenericEventArgs<IKey>(key));
			}
		}

		public void SetVirtualProperties(IEnumerable<IUserOption> virtualProperties)
        {
            virtualPropertyGrid1.SetVirtualProperties(virtualProperties);
        }

        public void RefreshVirtualProperties()
        {
            virtualPropertyGrid1.RefreshVisibilities();
        }

	    public bool EventRaisingDisabled
		{
			get; set;
		}

		public void Clear()
		{
			virtualPropertyGrid1.Clear();
		}

		public void StartBulkUpdate()
		{
			Utility.SuspendPainting(this);
		}

		public void EndBulkUpdate()
		{
			Utility.ResumePainting(this);
		}

        private void buttonAddColumn_Click(object sender, EventArgs e)
        {
            AddNewColumn.RaiseEventEx(this);
        }

		private void buttonDeleteColumn_Click(object sender, EventArgs e)
		{
			var column = listBoxColumns.GetSelectedItem<IColumn>();
			if(column != null)
			{
				DeleteColumn.RaiseEventEx(this, new GenericEventArgs<IColumn>(column));
			}
		}

		private void buttonEditColumn_Click(object sender, EventArgs e)
		{
			var column = listBoxColumns.GetSelectedItem<IColumn>();
			if(column != null)
			{
				EditColumn.RaiseEventEx(this, new GenericEventArgs<IColumn>(column));
			}
		}

		private void listBoxColumns_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshColumnButtons();
		}

		private void RefreshColumnButtons()
		{
			buttonEditColumn.Enabled = listBoxColumns.SelectedIndex != -1;
			buttonDeleteColumn.Enabled = listBoxColumns.SelectedIndex != -1;
		}

		public event EventHandler AddNewColumn;
		public event EventHandler AddNewKey;
		public event EventHandler DescriptionChanged;
		public event EventHandler EntityNameChanged;
		public event EventHandler DeleteEntity;
		public event EventHandler<GenericEventArgs<IColumn>> EditColumn;
		public event EventHandler<GenericEventArgs<IColumn>> DeleteColumn;
		public event EventHandler<GenericEventArgs<IKey>> EditKey;
		public event EventHandler<GenericEventArgs<IKey>> DeleteKey;

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DeleteEntity.RaiseEventEx(this);
		}
	}
}