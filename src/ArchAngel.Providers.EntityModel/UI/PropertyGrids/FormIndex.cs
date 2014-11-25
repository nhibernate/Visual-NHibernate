using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormIndex : UserControl, IIndexForm, IEventSender
    {
		public bool EventRaisingDisabled { get; set; }

		private List<IColumn> columns;

        public FormIndex()
        {
            InitializeComponent();

			foreach (DatabaseIndexType indexType in Enum.GetValues(typeof(DatabaseIndexType)))
				comboBoxType.Items.Add(indexType);

			textBoxName.TextChanged += (sender, e) => IndexNameChanged.RaiseEventEx(this);
			textBoxDescription.TextChanged += (sender, e) => DescriptionChanged.RaiseEventEx(this);
			comboBoxType.SelectedValueChanged += (sender, e) => DatatypeChanged.RaiseEventEx(this);
			listBoxColumn.MouseDoubleClick += listBoxColumn_MouseDoubleClick;
        }

		void listBoxColumn_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			int index = listBoxColumn.IndexFromPoint(e.Location);
			if (index == -1) return;

			SelectedColumnChanged.RaiseEventEx(this);
		}

		public event EventHandler IndexNameChanged;
		public event EventHandler DescriptionChanged;
		public event EventHandler DatatypeChanged;
		public event EventHandler SelectedColumnChanged;
		public event EventHandler DeleteColumn;

		public void SetVirtualProperties(IEnumerable<IUserOption> virtualProperties)
		{
			virtualPropertyGrid1.SetVirtualProperties(virtualProperties);
		}

        public void RefreshVirtualProperties()
        {
            virtualPropertyGrid1.RefreshVisibilities();
        }

		public string IndexName
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

		public DatabaseIndexType Datatype
		{
			get { return (DatabaseIndexType)comboBoxType.SelectedItem; }
			set
			{
				EventRaisingDisabled = true;
				comboBoxType.SelectedItem = value;
				EventRaisingDisabled = false;
			}
		}

		public List<IColumn> Columns
		{
			get
			{
				return columns;
			}
			set
			{
				EventRaisingDisabled = true;
				columns = value;
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
				EventRaisingDisabled = false;
			}
		}

		public void Clear() { }

		public void StartBulkUpdate()
		{
			Slyce.Common.Utility.SuspendPainting(this);
		}

		public void EndBulkUpdate()
		{
			Slyce.Common.Utility.ResumePainting(this);
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DeleteColumn.RaiseEventEx(this);
		}
    }
}