using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
    public partial class FormKey : UserControl, IKeyForm, IEventSender
    {
		private readonly List<IColumn> columns = new List<IColumn>();
		private readonly List<IColumn> possibleColumnsForKey = new List<IColumn>();
		public bool EventRaisingDisabled { get; set; }

        public FormKey()
        {
            InitializeComponent();

			foreach (DatabaseKeyType keyType in Enum.GetValues(typeof(DatabaseKeyType)))
				comboBoxKeytype.Items.Add(keyType);

			textBoxName.TextChanged += (sender, e) => KeyNameChanged.RaiseEventEx(this);
			textBoxDescription.TextChanged += (sender, e) => DescriptionChanged.RaiseEventEx(this);
			comboBoxKeytype.SelectedValueChanged += (sender, e) => KeytypeChanged.RaiseEventEx(this);
			listBoxColumn.MouseDoubleClick += listBoxColumn_MouseDoubleClick;
        	listBoxColumn.SelectedIndexChanged += (s, e) => RefreshColumnButtons();
        }

		void listBoxColumn_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			int index = listBoxColumn.IndexFromPoint(e.Location);
			if (index == -1) return;

			EditColumn.RaiseEventEx(this);
		}

		public void SetVirtualProperties(IEnumerable<IUserOption> virtualProperties)
		{
			virtualPropertyGrid1.SetVirtualProperties(virtualProperties);
		}

        public void RefreshVirtualProperties()
        {
            virtualPropertyGrid1.RefreshVisibilities();
        }

    	public string KeyName
		{
			get { return textBoxName.Text; }
			set
			{
				EventRaisingDisabled = true;
				textBoxName.Text = value;
				EventRaisingDisabled = false;
			}
		}

		public DatabaseKeyType Keytype
		{
			get { return (DatabaseKeyType)comboBoxKeytype.SelectedItem; }
			set
			{
				EventRaisingDisabled = true;
				comboBoxKeytype.SelectedItem = value;
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

		public IEnumerable<IColumn> Columns
		{
			get
			{
				return columns;
			}
			set
			{
				EventRaisingDisabled = true;
				columns.Clear();
				columns.AddRange(value);

				listBoxColumn.Items.Clear();
				foreach (var column in columns)
					listBoxColumn.Items.Add(column.Name);
				EventRaisingDisabled = false;
			}
		}

		public IColumn SelectedColumn
    	{
    		get
    		{
    			return listBoxColumn.SelectedIndex > -1 ? columns[listBoxColumn.SelectedIndex] : null;
    		}
    		set
    		{
				EventRaisingDisabled = true;
    			listBoxColumn.SelectedIndex = columns.IndexOf(value);
				RefreshColumnButtons();
				EventRaisingDisabled = false;
    		}
    	}

    	public void SetAvailableColumns(IEnumerable<IColumn> columnForKey)
    	{
    		EventRaisingDisabled = true;
    		possibleColumnsForKey.Clear();
    		possibleColumnsForKey.AddRange(columnForKey);
			RefreshColumnButtons();
			EventRaisingDisabled = false;
    	}

		private void RefreshColumnButtons()
		{
			buttonAddColumn.Enabled = possibleColumnsForKey.Count > 0;
			buttonDeleteColumn.Enabled = listBoxColumn.SelectedIndex > -1;
		}

    	public void Clear()
    	{
    	}

		public void StartBulkUpdate()
		{
			Utility.SuspendPainting(this);
		}

		public void EndBulkUpdate()
		{
			Utility.ResumePainting(this);
		}

		public event EventHandler KeytypeChanged;
		public event EventHandler DescriptionChanged;
		public event EventHandler KeyNameChanged;
		public event EventHandler<GenericEventArgs<IColumn>> AddNewColumn;
    	public event EventHandler EditColumn;
    	public event EventHandler<GenericEventArgs<IColumn>> RemoveColumn;
    	public event EventHandler DeleteKey;

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DeleteKey.RaiseEventEx(this);
		}

		private void buttonAddColumn_Click(object sender, EventArgs e)
		{
			FormSelectColumn form = new FormSelectColumn(possibleColumnsForKey);
			var result = form.ShowDialog(this);

			if(result == DialogResult.OK)
			{
				AddNewColumn.RaiseEventEx(this, new GenericEventArgs<IColumn>(form.SelectedColumn));
			}
		}

		private void buttonDeleteColumn_Click(object sender, EventArgs e)
		{
			int selectedIndex = listBoxColumn.SelectedIndex;
			if(selectedIndex > -1)
			{
				RemoveColumn.RaiseEventEx(this, new GenericEventArgs<IColumn>(columns[selectedIndex]));
			}
		}
    }
}