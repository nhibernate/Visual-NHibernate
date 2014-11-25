using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.Controls
{
    public partial class FormLookup : Form
    {
        enum ColumnNames
        {
            Enabled = 0,
            Id = 1,
            Name = 2,
            Alias = 3,
            AliasPlural = 4,
            Description = 5
        }
        private Model.Database Database;
        private bool IsSaved = false;
        private Lookup _Lookup;
        private bool IsNew = false;
        private readonly Color ErrorBackColor = Color.BlanchedAlmond;

        public Lookup Lookup
        {
            get { return _Lookup; }
        }

        public FormLookup(Model.Database database)
        {
            InitializeComponent();
            ucHeading1.Text = "";
            BackColor = Slyce.Common.Colors.BackgroundColor;
            Database = database;
            Interfaces.Events.ShadeMainForm();
        }

        public FormLookup(Lookup lookup)
        {
            InitializeComponent();
            BackColor = Slyce.Common.Colors.BackgroundColor;
            _Lookup = lookup;
            ucHeading1.Text = "";
            Interfaces.Events.ShadeMainForm();
        }

        private void PopulateTables()
        {
            ddlLookupTables.Sorted = false;
            ddlLookupTables.Items.Clear();
            ddlLookupTables.DisplayMember = "Alias";
            ddlLookupTables.Items.Add("  -- None --");

            foreach (ScriptObject scriptObject in Lookup.Database.ScriptObjects)
            {
                ddlLookupTables.Items.Add(scriptObject);
            }
            ddlLookupTables.Sorted = true;
            //ddlLookupTables.SelectedIndex = 0;
            ddlLookupTables.SelectedItem = Lookup.BackingObject;
        }

        private void PopulateColumns()
        {
            ddlLookupIdColumns.Sorted = ddlLookupNameColumns.Sorted = false;
            ddlLookupIdColumns.Items.Clear();
            ddlLookupNameColumns.Items.Clear();
            ddlLookupNameColumns.Items.Add("  -- None --");

            if (ddlLookupTables.SelectedItem is ScriptObject)
            {
                ddlLookupIdColumns.DisplayMember = ddlLookupNameColumns.DisplayMember = "Alias";

                foreach (Column column in ((ScriptObject)ddlLookupTables.SelectedItem).Columns)
                {
                    ddlLookupIdColumns.Items.Add(column);

                    if (Helper.SQLServer.IsDataTypeText(column))
                    {
                        ddlLookupNameColumns.Items.Add(column);
                    }
                }
                ddlLookupIdColumns.Sorted = ddlLookupNameColumns.Sorted = true;
            }
            if (ddlLookupTables.SelectedItem == Lookup.BackingObject)
            {
                ddlLookupIdColumns.SelectedItem = Lookup.IdColumn;
                ddlLookupNameColumns.SelectedItem = Lookup.NameColumn;
            }
            else
            {
                if (ddlLookupIdColumns.Items.Count > 0)
                {
                    ddlLookupIdColumns.SelectedIndex = 0;
                }
                if (ddlLookupNameColumns.Items.Count > 0)
                {
                    ddlLookupNameColumns.SelectedIndex = 0;
                }
            }
        }

        private void FormLookup_Load(object sender, EventArgs e)
        {
            if (Lookup == null)
            {
                IsNew = true;
                Text = "Add New Lookup";
                _Lookup = new Lookup("NewLookup", true);
                Database.AddLookup(_Lookup);
            }
            else
            {
                Text = "Edit Lookup " + Lookup.Name;

                txtName.Text = Lookup.Name;
                txtAlias.Text = Lookup.Alias;
                txtDescription.Text = Lookup.Description;

                foreach (LookupValue lookupValue in Lookup.LookupValues)
                {
                    AddLookupValueToGrid(lookupValue);
                }
            }
            PopulateTables();
            PopulateSubscribingColumns();
            txtName.Focus();
        }

        private void AddLookupValueToGrid(LookupValue lookupValue)
        {
            int newIndex = dataGridViewX1.Rows.Add(new object[] { lookupValue.Enabled, lookupValue.Id.ToString(), lookupValue.Name, lookupValue.Alias, lookupValue.AliasPlural, lookupValue.Description });
            dataGridViewX1.Rows[newIndex].Tag = lookupValue;
        }

        private void FormLookup_FormClosed(object sender, FormClosedEventArgs e)
        {
            Interfaces.Events.UnShadeMainForm();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
            string failReason;

            string originalName = Lookup.Name;
            Lookup.Name = txtName.Text.Trim();

            if (!Lookup.NameValidate(Lookup, out failReason))
            {
                if (IsNew)
                {
                    txtAlias.Text = Lookup.AliasDefault(Lookup);
                }
                Lookup.Name = originalName;
                errorProvider.SetError(txtName, failReason);
                txtName.BackColor = ErrorBackColor;
                //e.Cancel = true;
                return;
            }
            if (IsNew)
            {
                txtAlias.Text = Lookup.AliasDefault(Lookup);
            }
            Lookup.Name = originalName; // Reset, so we don't inadvertantly save
            txtName.BackColor = Color.White;
        }

        private void txtAlias_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
            string failReason;

            string originalAlias = Lookup.Alias;
            Lookup.Alias = txtAlias.Text.Trim();

            if (!Lookup.AliasValidate(Lookup, out failReason))
            {
                //if (IsNew)
                //{
                //    textBoxAliasDisplay.Text = _column.AliasDisplayDefault(_column);
                //}
                Lookup.Alias = originalAlias;
                errorProvider.SetError(txtAlias, failReason);
                txtAlias.BackColor = ErrorBackColor;
                //e.Cancel = true;
                return;
            }
            if (IsNew)
            {
                //textBoxAliasDisplay.Text = _column.AliasDisplayDefault(_column);
            }
            Lookup.Alias = originalAlias; // Reset, so we don't inadvertantly save
            //Lookup.BackColor = Color.White;
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
            Lookup.Alias = txtAlias.Text;
            Lookup.Name = txtName.Text;
            Lookup.Description = txtDescription.Text;
            DialogResult = DialogResult.OK;
            IsSaved = true;
            this.Close();
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            Lookup.Description = txtDescription.Text;
        }

        private void ddlLookupTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lookup.BackingObject = (ScriptObject)ddlLookupTables.SelectedItem;
            PopulateColumns();
        }

        private void ddlLookupIdColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lookup.IdColumn = (Column)ddlLookupIdColumns.SelectedItem;
        }

        private void ddlLookupNameColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lookup.NameColumn = (Column)ddlLookupNameColumns.SelectedItem;
        }

        private void btnFetchValuesFromOtherTable_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                if (ddlLookupIdColumns.SelectedItem == null)
                {
                    MessageBox.Show("Please select an ID column.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Model.Column idColumn = (Model.Column)ddlLookupIdColumns.SelectedItem;

                string sql = string.Format("SELECT DISTINCT [{0}]", idColumn.Name);

                if (ddlLookupNameColumns.SelectedItem != null &&
                    ddlLookupNameColumns.SelectedItem is Model.Column)
                {
                    sql += string.Format(", [{0}]", ((Model.Column)ddlLookupNameColumns.SelectedItem).Name);
                }
                Model.ScriptObject table = (Model.ScriptObject)ddlLookupTables.SelectedItem;
                sql += string.Format(" FROM [{0}].[{1}] ORDER BY [{2}]", table.Schema, table.Name, idColumn.Name);
                System.Data.DataTable results = null;

                try
                {
                    Providers.Database.Helper.Utility.ResetAllConnections();
                    results = table.Database.RunQuerySQL(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                System.Data.DataRow[] rows = results.Select();

                if (results.Rows.Count > 100)
                {
                    if (MessageBox.Show(string.Format("{0} rows were returned. Are you sure you want to import this many?", results.Rows.Count), "Many Results", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                foreach (System.Data.DataRow row in rows)
                {
                    string id = row[0].ToString();
                    string name = row.ItemArray.Length == 1 ? "" : row[1].ToString();

                    // Check if already exists
                    bool exists = false;

                    foreach (System.Windows.Forms.DataGridViewRow dataGridRow in dataGridViewX1.Rows)
                    {
                        if (dataGridRow.IsNewRow)
                        {
                            continue;
                        }
                        if (dataGridRow.Cells[(int)ColumnNames.Id].Value != null && Slyce.Common.Utility.StringsAreEqual(dataGridRow.Cells[(int)ColumnNames.Id].Value.ToString(), id, false))
                        {
                            if (!Slyce.Common.Utility.StringsAreEqual(dataGridRow.Cells[(int)ColumnNames.Name].Value.ToString(), name, false) &&
                                string.IsNullOrEmpty(dataGridRow.Cells[(int)ColumnNames.Name].Value.ToString()))
                            {
                                dataGridRow.Cells[(int)ColumnNames.Name].Value = name;
                                ((LookupValue)dataGridRow.Tag).Name = name;
                            }
                            exists = true;
                            break;
                        }
                    }
                    if (exists)
                    {
                        continue;
                    }
                    LookupValue lookupValue = new LookupValue(name, false, Lookup);
                    lookupValue.Id = id;
                    lookupValue.Name = name;
                    Lookup.LookupValues.Add(lookupValue);
                    AddLookupValueToGrid(lookupValue);
                }
                HighlightDuplicates();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void HighlightDuplicates()
        {
            Dictionary<string, List<int>> dictIds = new System.Collections.Generic.Dictionary<string, List<int>>();
            Dictionary<string, List<int>> dictNames = new System.Collections.Generic.Dictionary<string, List<int>>();
            Dictionary<string, List<int>> dictAlias = new System.Collections.Generic.Dictionary<string, List<int>>();
            Dictionary<string, List<int>> dictAliasPlural = new System.Collections.Generic.Dictionary<string, List<int>>();

            foreach (System.Windows.Forms.DataGridViewRow row in dataGridViewX1.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                string id = row.Cells[(int)ColumnNames.Id].Value == null ? "" : row.Cells[(int)ColumnNames.Id].Value.ToString();
                string name = row.Cells[(int)ColumnNames.Name].Value == null ? "" : row.Cells[(int)ColumnNames.Name].Value.ToString();
                string alias = row.Cells[(int)ColumnNames.Alias].Value == null ? "" : row.Cells[(int)ColumnNames.Alias].Value.ToString();
                string aliasPlural = row.Cells[(int)ColumnNames.AliasPlural].Value == null ? "" : row.Cells[(int)ColumnNames.AliasPlural].Value.ToString();

                LookupValue lookupValue = (LookupValue)row.Tag;
                string errorDescription;

                if (!lookupValue.IsValid(false, out errorDescription))
                {
                    row.Cells[(int)ColumnNames.Id].ErrorText = errorDescription;
                }
                else
                {
                    row.Cells[(int)ColumnNames.Id].ErrorText = "";
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dataGridViewX1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            System.Windows.Forms.DataGridViewRow row = ((DevComponents.DotNetBar.Controls.DataGridViewX)sender).Rows[e.RowIndex];

            if (row.IsNewRow)
            {
                return;
            }
            LookupValue lookupValue = (LookupValue)row.Tag;

            if (row.Tag == null)
            {
                lookupValue = new LookupValue("", true, "", "", Lookup);
                lookupValue.Enabled = true;
                row.Cells[(int)ColumnNames.Enabled].Value = true;
                Lookup.LookupValues.Add(lookupValue);
                row.Tag = lookupValue;
            }
            else
            {
                lookupValue = (LookupValue)row.Tag;
            }
            // Has the Name field been edited?
            if (row.Cells[e.ColumnIndex] == row.Cells[(int)ColumnNames.Name])
            {
                if (row.Cells[(int)ColumnNames.Name] == null || row.Cells[(int)ColumnNames.Name].Value == null)
                {
                    lookupValue.Name = "";
                }
                else
                {
                    lookupValue.Name = row.Cells[(int)ColumnNames.Name].Value.ToString();
                }
                if (string.IsNullOrEmpty(lookupValue.Alias))
                {
                    lookupValue.Alias = lookupValue.AliasDefault(lookupValue);
                    row.Cells[(int)ColumnNames.Alias].Value = lookupValue.Alias;
                }
                if (string.IsNullOrEmpty(lookupValue.AliasPlural))
                {
                    lookupValue.AliasPlural = lookupValue.AliasPluralDefault(lookupValue);
                    row.Cells[(int)ColumnNames.AliasPlural].Value = lookupValue.AliasPlural;
                }
            }
            // Has the Alias field been edited?
            if (row.Cells[e.ColumnIndex] == row.Cells[(int)ColumnNames.Alias])
            {
                lookupValue.Alias = row.Cells[(int)ColumnNames.Alias] == null ? "" : row.Cells[(int)ColumnNames.Alias].Value.ToString();
                lookupValue.AliasPlural = lookupValue.AliasPluralDefault(lookupValue);
                row.Cells[(int)ColumnNames.AliasPlural].Value = lookupValue.AliasPlural;
            }
            if (row.Cells[(int)ColumnNames.Enabled].Value == null)
            {
                row.Cells[(int)ColumnNames.Enabled].Value = false;
            }
            lookupValue.Alias = row.Cells[(int)ColumnNames.Alias].Value == null ? "" : row.Cells[(int)ColumnNames.Alias].Value.ToString();
            lookupValue.AliasPlural = row.Cells[(int)ColumnNames.AliasPlural].Value == null ? "" : row.Cells[(int)ColumnNames.AliasPlural].Value.ToString();
            lookupValue.Description = row.Cells[(int)ColumnNames.Description].Value == null ? "" : row.Cells[(int)ColumnNames.Description].Value == null ? "" : row.Cells[(int)ColumnNames.Description].Value.ToString();
            lookupValue.Enabled = (bool)row.Cells[(int)ColumnNames.Enabled].Value;
            lookupValue.Id = row.Cells[(int)ColumnNames.Id].Value == null ? "" : row.Cells[(int)ColumnNames.Id].Value.ToString();
            lookupValue.Name = row.Cells[(int)ColumnNames.Name].Value == null ? "" : row.Cells[(int)ColumnNames.Name].Value.ToString();
            HighlightDuplicates();
        }

        private void dataGridViewX1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            LookupValue lookupValue = (LookupValue)e.Row.Tag;
            Lookup.LookupValues.Remove(lookupValue);
        }

        private void PopulateSubscribingColumns()
        {
            List<Column> columns = Lookup.GetSubscribingColumns();
            listViewSubscribingColumns.Items.Clear();

            foreach (Column column in columns)
            {
                listViewSubscribingColumns.Items.Add(string.Format("[{0}] . {1}", column.Parent.Alias, column.Alias));
            }
        }


    }
}
