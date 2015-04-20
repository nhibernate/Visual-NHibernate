using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.Controls
{
    public partial class FormFilter : Form
    {
    	private ScriptObject ParentScriptObject { get; set; }
    	private ScriptObject[] ScriptObjects { get; set; }

    	private bool _loading;

        public Filter Filter { get; private set; }

        public FormFilter(ScriptObject parent, ScriptObject[] scriptObjects)
        {
            InitializeComponent();
            BackColor = Slyce.Common.Colors.BackgroundColor;

            ParentScriptObject = parent;
            ScriptObjects = scriptObjects;
            ucHeading1.Text = "";
            Interfaces.Events.ShadeMainForm();
        }

        public FormFilter(Filter filter, ScriptObject[] scriptObjects)
        {
            InitializeComponent();
            BackColor = Slyce.Common.Colors.BackgroundColor;

            ParentScriptObject = filter.Parent;
            Filter = filter;
            ScriptObjects = scriptObjects;
            ucHeading1.Text = "";
            Interfaces.Events.ShadeMainForm();
        }

        private void FormFilter_Load(object sender, EventArgs e)
        {
            _loading = true;

            comboBoxScriptObject.DataSource = ScriptObjects;
            comboBoxScriptObject.DisplayMember = "Alias";
            comboBoxScriptObject.ValueMember = "Name";

            comboBoxScriptObject.SelectedItem = ParentScriptObject;

            comboBoxCompareOperator.Items.Add("");
            comboBoxCompareOperator.Items.Add("LIKE");
            comboBoxCompareOperator.Items.Add("=");
            comboBoxCompareOperator.Items.Add("<");
            comboBoxCompareOperator.Items.Add(">");
            comboBoxCompareOperator.Items.Add("<>");
            comboBoxCompareOperator.Items.Add("<=");
            comboBoxCompareOperator.Items.Add(">=");

            if (Filter == null)
            {
                Text = "Add New Filter";
                textBoxName.Text = "Get" + ParentScriptObject.AliasPlural + "By ...";
                textBoxAlias.Text = "Get" + ParentScriptObject.AliasPlural + "By ...";
            }
            else
            {
                Text = "Edit Filter " + Filter.Name;
                textBoxName.Text = Filter.Name;
                textBoxAlias.Text = Filter.Alias;
                radioButtonReturnCollection.Checked = Filter.IsReturnTypeCollection;
                radioButtonReturnSpecificItem.Checked = !Filter.IsReturnTypeCollection;
                checkBoxCreateStoredProcedure.Checked = Filter.CreateStoredProcedure;
                checkBoxUseCustomWhereClause.Checked = Filter.UseCustomWhere;
                textBoxCustomWhereClause.Text = Filter.CustomWhere;
                textBoxDescription.Text = Filter.Description;

                comboBoxScriptObject.SelectedItem = Filter.Parent;

                if (Filter.FilterColumns.Length > 0)
                {
                    radioButtonFilterAnd.Checked = true;
                    radioButtonFilterAnd.Enabled = true;
                    radioButtonFilterOr.Enabled = true;
                }

                foreach (Filter.FilterColumn filterColumn in Filter.FilterColumns)
                {
                    ListViewItem item = new ListViewItem(filterColumn.LogicalOperator);
                    item.SubItems.Add(filterColumn.Column.Parent.Alias); 
                    item.SubItems.Add(filterColumn.Column.Alias);
                    item.SubItems.Add(filterColumn.Alias); 
                    item.SubItems.Add(filterColumn.CompareOperator);

                    item.Tag = filterColumn;

                    listViewColumn.Items.Add(item);
                }

                foreach (Filter.OrderByColumn orderByColumn in Filter.OrderByColumns)
                {
                    ListViewItem item = new ListViewItem(orderByColumn.Column.Parent.Alias);
                    item.SubItems.Add(orderByColumn.Column.Alias);
                    item.SubItems.Add(orderByColumn.SortOperator);

                    item.Tag = orderByColumn;

                    listViewOrderByColumn.Items.Add(item);
                }

                CheckAbilityToCreateProcedure();

                if (!Filter.IsUserDefined)
                {
                    textBoxName.ReadOnly = true;

                    checkBoxCreateStoredProcedure.Enabled = false;
                    checkBoxUseCustomWhereClause.Enabled = false;
                    radioButtonReturnCollection.Enabled = false;
                    radioButtonReturnSpecificItem.Enabled = false;
                    comboBoxColumn.Enabled = false;
                    comboBoxScriptObject.Enabled = false;
                    tabStripFilter.Enabled = false;

                    tabStripFilter.Pages.RemoveAt(2);
                    tabStripFilter.Pages.RemoveAt(1);
                }
            }

            _loading = false;

            listViewColumn_Resize(null, null);
            listViewOrderByColumn_Resize(null, null);

            CheckAbilityToCreateProcedure();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                Control control = Controls[i];
                control.Focus();
                if (!Validate())
                {
                    DialogResult = DialogResult.None;
                    return;
                }
            }

            if (Filter == null)
            {
                Filter = new Filter(textBoxName.Text, true, ParentScriptObject, radioButtonReturnCollection.Checked, checkBoxCreateStoredProcedure.Checked, checkBoxUseCustomWhereClause.Checked, textBoxCustomWhereClause.Text, null);
            }
            else
            {
                Filter.Name = textBoxName.Text;
                Filter.IsReturnTypeCollection = radioButtonReturnCollection.Checked;
                Filter.CreateStoredProcedure = checkBoxCreateStoredProcedure.Checked;
                Filter.UseCustomWhere = checkBoxUseCustomWhereClause.Checked;
                Filter.CustomWhere = textBoxCustomWhereClause.Text;
            }
            Filter.Alias = textBoxAlias.Text;
            Filter.Description = textBoxDescription.Text;
            Filter.ClearFilterColumns();

            foreach (ListViewItem listViewItem in listViewColumn.Items)
            {
                Filter.FilterColumn filterColumn = (Filter.FilterColumn)listViewItem.Tag;
                Filter.AddFilterColumn(filterColumn);
            }

            Filter.ClearOrderByColumns();
            foreach (ListViewItem listViewItem in listViewOrderByColumn.Items)
            {
                Filter.OrderByColumn orderByColumn = (Filter.OrderByColumn)listViewItem.Tag;
                Filter.AddOrderByColumn(orderByColumn);
            }
        }

        private void comboBoxScriptObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptObject scriptObject = (ScriptObject)comboBoxScriptObject.SelectedItem;

            comboBoxColumn.DataSource = scriptObject.Columns;
            comboBoxColumn.DisplayMember = "Alias";
            comboBoxColumn.ValueMember = "Name";

            // If Stored Procedure the filter must contain required paramters
            if (scriptObject.GetType() == typeof(StoredProcedure))
            {
                listViewColumn.Items.Clear();
                radioButtonFilterAnd.Checked = false;
                radioButtonFilterOr.Checked = false;

                StoredProcedure storedProcedure = (StoredProcedure)scriptObject;

                textBoxName.Text = storedProcedure.Filters[0].Name;
                foreach (Filter.FilterColumn filterColumn in storedProcedure.Filters[0].FilterColumns)
                {
                    AddFilterColumn(filterColumn.Column);
                }
            }
        }

        private void checkBoxUseCustomWhereClause_CheckedChanged(object sender, EventArgs e)
        {
            if (_loading)
            {
                return;
            }

            CheckAbilityToCreateProcedure();
        }

        private void radioButtonReturnCollection_CheckedChanged(object sender, EventArgs e)
        {
            if (_loading)
            {
                return;
            }

            textBoxName.Text = "Get" + ParentScriptObject.AliasPlural + "By ...";
            textBoxAlias.Text = "Get" + ParentScriptObject.AliasPlural + "By ...";
        }

        private void radioButtonReturnSpecificItem_CheckedChanged(object sender, EventArgs e)
        {
            if (_loading)
            {
                return;
            }

            textBoxName.Text = "Get" + ParentScriptObject.AliasPlural + "By ...";
            textBoxAlias.Text = "Get" + ParentScriptObject.Alias + "By ...";
        }

        private void textBoxName_KeyUp(object sender, KeyEventArgs e)
        {
            if (Filter == null)
            {
                textBoxAlias.Text = textBoxName.Text;
            }
        }

        private void CheckAbilityToCreateProcedure()
        {
            if (ParentScriptObject.GetType() == typeof(StoredProcedure))
            {
                checkBoxCreateStoredProcedure.Checked = false;
                checkBoxCreateStoredProcedure.Enabled = false;

                checkBoxUseCustomWhereClause.Checked = false;
                checkBoxUseCustomWhereClause.Enabled = false;
                return;
            }

            if (checkBoxUseCustomWhereClause.Checked)
            {
                checkBoxCreateStoredProcedure.Enabled = true;
                checkBoxCreateStoredProcedure.Checked = true;
                return;
            }

            foreach (ListViewItem listViewItem in listViewColumn.Items)
            {
                Filter.FilterColumn filterColumn = (Filter.FilterColumn)listViewItem.Tag;
                if (filterColumn.Column.Parent != ParentScriptObject)
                {
                    checkBoxCreateStoredProcedure.Enabled = false;
                    checkBoxCreateStoredProcedure.Checked = false;
                    return;
                }
            }

            checkBoxCreateStoredProcedure.Enabled = true;
        }

        private void buttonAddFilter_Click(object sender, EventArgs e)
        {
            Column column = (Column)comboBoxColumn.SelectedItem;

            AddFilterColumn(column);
        }

        private void AddFilterColumn(Column column)
        {
        	string logicalOperator;

            if (radioButtonFilterAnd.Checked)
            {
                logicalOperator = "And";
            }
            else if (radioButtonFilterOr.Checked)
            {
                logicalOperator = "Or";
            }
            else
            {
                logicalOperator = "";
                radioButtonFilterAnd.Checked = true;
                radioButtonFilterAnd.Enabled = true;
                radioButtonFilterOr.Enabled = true;
            }
            Filter.FilterColumn filterColumn = new Filter.FilterColumn(column, logicalOperator, comboBoxCompareOperator.Text, textBoxFilterColumnAlias.Text);
            ListViewItem item = new ListViewItem(filterColumn.LogicalOperator);
            item.SubItems.Add(filterColumn.Column.Parent.Alias);
            item.SubItems.Add(filterColumn.Column.Alias);
            item.SubItems.Add(filterColumn.Alias);
            item.SubItems.Add(filterColumn.CompareOperator);
            item.Tag = filterColumn;

            listViewColumn.Items.Add(item);

            CheckAbilityToCreateProcedure();

            comboBoxCompareOperator.Text = "";
            textBoxFilterColumnAlias.Text = "";
        }

        private void buttonAddOrder_Click(object sender, EventArgs e)
        {
            Column column = (Column)comboBoxColumn.SelectedItem;

            foreach (ListViewItem listViewItem in listViewOrderByColumn.Items)
            {
                if (listViewItem.Tag == column)
                {
                    return;
                }
            }

            ListViewItem item = new ListViewItem(column.Parent.Alias);

            item.SubItems.Add(column.Alias);

            string sortOperator = "";
            if (radioButtonOrderAscending.Checked)
            {
                sortOperator = "ASC";
            }

            if (radioButtonOrderDescending.Checked)
            {
                sortOperator = "DESC";
            }

            item.SubItems.Add(sortOperator);

            Filter.OrderByColumn orderByColumn = new Filter.OrderByColumn(column, sortOperator);
            item.Tag = orderByColumn;

            listViewOrderByColumn.Items.Add(item);
        }

        private void listViewColumn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            Point point = new Point(e.X, e.Y);

            if (listViewColumn.SelectedItems.Count == 0)
                return;

            contextMenuStripFilter.Show(listViewColumn, point);
        }

        private void listViewOrderBy_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            Point point = new Point(e.X, e.Y);

            if (listViewOrderByColumn.SelectedItems.Count == 0)
                return;

            contextMenuStripFilter.Show(listViewOrderByColumn, point);
        }

        private void toolStripMenuItemFilterDelete_Click(object sender, EventArgs e)
        {
            ListView listView = (ListView)contextMenuStripFilter.SourceControl;
            listView.Items.Remove(listView.SelectedItems[0]);

            if (listView.SelectedItems.Count == 0)
            {
                radioButtonFilterAnd.Checked = false;
                radioButtonFilterOr.Checked = false;
            }
        }

        private void textBoxName_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (textBoxName.Text == "")
            {
                errorProvider.SetError(textBoxName, "Must Include Name.");
                e.Cancel = true;
                return;
            }

            if (textBoxName.Text.IndexOf("...") > 0)
            {
                errorProvider.SetError(textBoxName, "Must Include Valid Name");
                e.Cancel = true;
                return;
            }

            foreach (Filter filter in ParentScriptObject.Filters)
            {
                if (textBoxName.Text == filter.Name && Filter != filter)
                {
                    errorProvider.SetError(textBoxName, "Name Allready Exists.");
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void textBoxAlias_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (textBoxAlias.Text == "")
            {
                errorProvider.SetError(textBoxAlias, "Must Include Alias.");
                e.Cancel = true;
                return;
            }

            if (textBoxAlias.Text.IndexOf("...") > 0)
            {
                errorProvider.SetError(textBoxAlias, "Must Include Valid Alias");
                e.Cancel = true;
                return;
            }

            foreach (Filter filter in ParentScriptObject.Filters)
            {
                if (textBoxAlias.Text == filter.Alias && Filter != filter)
                {
                    errorProvider.SetError(textBoxAlias, "Alias Allready Exists.");
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void listViewColumn_Resize(object sender, EventArgs e)
        {
            foreach (ColumnHeader columnHeader in listViewColumn.Columns)
            {
                columnHeader.Width = (listViewColumn.Width - 5) / listViewColumn.Columns.Count;
            }
        }

        private void listViewOrderByColumn_Resize(object sender, EventArgs e)
        {
            foreach (ColumnHeader columnHeader in listViewOrderByColumn.Columns)
            {
                columnHeader.Width = (listViewOrderByColumn.Width - 5) / listViewOrderByColumn.Columns.Count;
            }
        }

        private void FormFilter_FormClosed(object sender, FormClosedEventArgs e)
        {
            Interfaces.Events.UnShadeMainForm();
        }
    }
}