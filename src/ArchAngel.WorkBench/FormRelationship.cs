using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.BLL;

namespace ArchAngel.Workbench
{
    public partial class FormRelationship : Form
    {
        private ScriptObject _parent;
        private Relationship _primaryRelationship;
        private Relationship _foreignRelationship;
        private ScriptObject[] _scriptObjects;
        private Color ErrorBackColor = Color.BlanchedAlmond;

        public Relationship PrimaryRelationship
        {
            get { return _primaryRelationship; }
        }

        public Relationship ForeignRelationship
        {
            get { return _foreignRelationship; }
        }

        public FormRelationship(Type type, ScriptObject parent, ScriptObject[] scriptObjects)
        {
            InitializeComponent();
            Controller.ShadeMainForm();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
            ucHeading1.Text = "";

            _parent = parent;
            _scriptObjects = scriptObjects;

            if (type == typeof(OneToOneRelationship))
            {
                checkBoxIsBase.Visible = true;
                radioButtonOneToOne.Checked = true;
                tabStripRelationship.Pages.RemoveAt(1);
            }

            if (type == typeof(OneToManyRelationship))
            {
                radioButtonOneToMany.Checked = true;
                tabStripRelationship.Pages.RemoveAt(1);
            }

            if (type == typeof(ManyToOneRelationship))
            {
                radioButtonManyToOne.Checked = true;
                tabStripRelationship.Pages.RemoveAt(1);
            }

            if (type == typeof(ManyToManyRelationship))
            {
                radioButtonManyToMany.Checked = true;

                tabStripRelationship.Pages.RemoveAt(0);
                tabStripRelationship.Pages.RemoveAt(1);
            }
        }

        public FormRelationship(Relationship relationship, ScriptObject[] scriptObjects)
        {
            InitializeComponent();
            ucHeading1.Text = "";

            _parent = relationship.Parent;
            _primaryRelationship = relationship;
            _scriptObjects = scriptObjects;
            Controller.ShadeMainForm();
        }

        private void FormRelationship_Load(object sender, EventArgs e)
        {
            foreach (ScriptObject scriptObject in _scriptObjects)
            {
                comboBoxPrimaryScriptObject.Items.Add(scriptObject);
            }
            comboBoxPrimaryScriptObject.DisplayMember = "Alias";
            comboBoxPrimaryScriptObject.ValueMember = "Name";

            foreach (OneToManyRelationship oneToManyRelationship in _parent.OneToManyRelationships)
            {
                comboBoxIntermediatePrimaryRelationship.Items.Add(oneToManyRelationship);
            }
            comboBoxIntermediatePrimaryRelationship.DisplayMember = "Alias";
            comboBoxIntermediatePrimaryRelationship.ValueMember = "Name";

            foreach (ScriptObject scriptObject in _scriptObjects)
            {
                comboBoxForeignScriptObject.Items.Add(scriptObject);
            }
            comboBoxForeignScriptObject.DisplayMember = "Alias";
            comboBoxForeignScriptObject.ValueMember = "Name";

            groupBoxType.Enabled = false;

            if (_primaryRelationship == null)
            {
                this.Text = "Add New Relationship";
                comboBoxPrimaryScriptObject.SelectedItem = _parent;

                checkBoxIsBase.Enabled = true;
            }
            else
            {
                this.Text = "Edit Relationship " + _primaryRelationship.Name;

                textBoxName.Text = _primaryRelationship.Name;
                textBoxAlias.Text = _primaryRelationship.Alias;
                textBoxPath.Text = _primaryRelationship.Path;

                comboBoxPrimaryScriptObject.SelectedItem = _parent;
                foreach (Column column in _primaryRelationship.PrimaryColumns)
                {
                    ListViewItem item = new ListViewItem(column.Alias);
                    item.SubItems.Add(column.Alias);
                    item.Tag = column;

                    listViewPrimaryColumn.Items.Add(item);
                }

                comboBoxForeignScriptObject.SelectedItem = _primaryRelationship.ForeignScriptObject;
                foreach (Column column in _primaryRelationship.ForeignColumns)
                {
                    ListViewItem item = new ListViewItem(column.Alias);
                    item.SubItems.Add(column.Alias);
                    item.Tag = column;

                    listViewForeignColumn.Items.Add(item);
                }

                if (_primaryRelationship.GetType() == typeof(ManyToManyRelationship))
                {
                    ManyToManyRelationship manyToManyRelationship = (ManyToManyRelationship)_primaryRelationship;

                    comboBoxIntermediatePrimaryRelationship.SelectedItem = manyToManyRelationship.IntermediatePrimaryRelationship;
                    comboBoxIntermediateForeignRelationship.SelectedItem = manyToManyRelationship.IntermediateForeignRelationship;

                    tabStripRelationship.Pages.RemoveAt(0);
                    tabStripRelationship.Pages.RemoveAt(1);
                }
                else
                {
                    tabStripRelationship.Pages.RemoveAt(1);

                    comboBoxPrimaryFilter.SelectedItem = _primaryRelationship.Filter;
                    comboBoxForeignFilter.SelectedItem = _primaryRelationship.ForeignRelationship.Filter;
                }

                if (_primaryRelationship.GetType() == typeof(OneToOneRelationship))
                {
                    OneToOneRelationship relationship = (OneToOneRelationship)_primaryRelationship;

                    checkBoxIsBase.Checked = relationship.IsBase;
                    checkBoxIsBase.Visible = true;
                    radioButtonOneToOne.Checked = true;
                }
                else if (_primaryRelationship.GetType() == typeof(OneToManyRelationship))
                {
                    OneToManyRelationship relationship = (OneToManyRelationship)_primaryRelationship;

                    radioButtonOneToMany.Checked = true;
                }
                else if (_primaryRelationship.GetType() == typeof(ManyToOneRelationship))
                {
                    radioButtonManyToOne.Checked = true;
                }
                else if (_primaryRelationship.GetType() == typeof(ManyToManyRelationship))
                {
                    ManyToManyRelationship relationship = (ManyToManyRelationship)_primaryRelationship;

                    radioButtonManyToMany.Checked = true;
                }

                if (!_primaryRelationship.IsUserDefined)
                {
                    textBoxName.ReadOnly = true;

                    comboBoxPrimaryColumn.Enabled = false;
                    comboBoxPrimaryScriptObject.Enabled = false;
                    buttonPrimaryColumnAdd.Enabled = false;
                    listViewPrimaryColumn.Enabled = false;
                    comboBoxPrimaryFilter.Enabled = false;

                    comboBoxIntermediatePrimaryRelationship.Enabled = false;
                    comboBoxIntermediateForeignRelationship.Enabled = false;

                    comboBoxForeignColumn.Enabled = false;
                    comboBoxForeignScriptObject.Enabled = false;
                    buttonForeignColumnAdd.Enabled = false;
                    listViewForeignColumn.Enabled = false;
                    comboBoxForeignFilter.Enabled = false;

                    buttonAddPrimaryFilter.Enabled = false;
                    buttonAddIntermediatePrimaryRelationship.Enabled = false;
                    buttonAddIntermediateForeignRelationship.Enabled = false;
                    buttonAddForeignFilter.Enabled = false;
                }
            }

            listViewPrimaryColumn_Resize(null, null);
            listViewForeignColumn_Resize(null, null);
        }

        private bool ValidateControl(Control control)
        {
            for (int i = control.Controls.Count - 1; i >= 0; i--)
            {
                Control childControl = control.Controls[i];
                childControl.Focus();

                if (!ValidateControl(childControl))
                {
                    return false;
                }

                if (!Validate())
                {
                    return false;
                }
            }

            control.Focus();
            return Validate();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            for (int i = this.Controls.Count - 1; i >= 0; i--)
            {
                Control control = this.Controls[i];
                control.Focus();

                // First tab
                tabStripRelationship.SelectedIndex = 0;
                if (!ValidateControl(control))
                {
                    DialogResult = DialogResult.None;
                    return;
                }

                // Second tab
                if (radioButtonOneToOne.Checked ||
                radioButtonOneToMany.Checked ||
                radioButtonManyToOne.Checked)
                {
                    tabStripRelationship.SelectedIndex = 1;
                    if (!ValidateControl(control))
                    {
                        DialogResult = DialogResult.None;
                        return;
                    }
                }
            }

            Column[] primaryColumns = new Column[listViewPrimaryColumn.Items.Count];
            for (int i = 0; i < listViewPrimaryColumn.Items.Count; i++)
            {
                ListViewItem listViewItem = listViewPrimaryColumn.Items[i];
                primaryColumns[i] = (Column)listViewItem.Tag;
            }

            Column[] foreignColumns = new Column[listViewForeignColumn.Items.Count];
            for (int i = 0; i < listViewForeignColumn.Items.Count; i++)
            {
                ListViewItem listViewItem = listViewForeignColumn.Items[i];
                foreignColumns[i] = (Column)listViewItem.Tag;
            }

            if (_primaryRelationship == null)
            {
                ScriptObject primaryScriptObject = (ScriptObject)comboBoxPrimaryScriptObject.SelectedItem;
                ScriptObject foreignScriptObject = (ScriptObject)comboBoxForeignScriptObject.SelectedItem;

                Filter primaryFilter = (Filter)comboBoxPrimaryFilter.SelectedItem;
                Filter foreignFilter = (Filter)comboBoxForeignFilter.SelectedItem;

                if (radioButtonOneToOne.Checked)
                {
                    _primaryRelationship = new OneToOneRelationship(textBoxName.Text, true, _parent, primaryColumns, foreignScriptObject, foreignColumns, primaryFilter, checkBoxIsBase.Checked);
                    _primaryRelationship.Alias = textBoxAlias.Text;
                    _foreignRelationship = new OneToOneRelationship(primaryScriptObject.Alias, true, foreignScriptObject, foreignColumns, _parent, primaryColumns, foreignFilter, !checkBoxIsBase.Checked);
                    _foreignRelationship.Alias = primaryScriptObject.Alias;
                }

                if (radioButtonOneToMany.Checked)
                {
                    _primaryRelationship = new OneToManyRelationship(textBoxName.Text, true, _parent, primaryColumns, foreignScriptObject, foreignColumns, primaryFilter);
                    _primaryRelationship.Alias = textBoxAlias.Text;
                    _foreignRelationship = new ManyToOneRelationship(primaryScriptObject.Alias, true, foreignScriptObject, foreignColumns, _parent, primaryColumns, foreignFilter);
                    _foreignRelationship.Alias = primaryScriptObject.Alias;
                }

                if (radioButtonManyToOne.Checked)
                {
                    _primaryRelationship = new ManyToOneRelationship(textBoxName.Text, true, _parent, primaryColumns, foreignScriptObject, foreignColumns, primaryFilter);
                    _primaryRelationship.Alias = textBoxAlias.Text;
                    _foreignRelationship = new OneToManyRelationship(primaryScriptObject.AliasPlural, true, foreignScriptObject, foreignColumns, _parent, primaryColumns, foreignFilter);
                    _foreignRelationship.Alias = primaryScriptObject.AliasPlural;
                }

                if (radioButtonManyToMany.Checked)
                {
                    OneToManyRelationship oneToManyRelationship = (OneToManyRelationship)comboBoxIntermediatePrimaryRelationship.SelectedItem;
                    ManyToOneRelationship manyToOneRelationship = (ManyToOneRelationship)comboBoxIntermediateForeignRelationship.SelectedItem;
                    _primaryRelationship = new ManyToManyRelationship(textBoxName.Text, true, oneToManyRelationship, manyToOneRelationship, oneToManyRelationship.Filter);
                    _primaryRelationship.Alias = textBoxAlias.Text;
                    _foreignRelationship = new ManyToManyRelationship(oneToManyRelationship.Parent.AliasPlural, true, (OneToManyRelationship)manyToOneRelationship.ForeignRelationship, (ManyToOneRelationship)oneToManyRelationship.ForeignRelationship, manyToOneRelationship.ForeignRelationship.Filter);
                    _foreignRelationship.Alias = oneToManyRelationship.Parent.AliasPlural;
                }
                _primaryRelationship.ForeignRelationship = _foreignRelationship;
                _foreignRelationship.ForeignRelationship = _primaryRelationship;

            }
            else
            {
                _primaryRelationship.Name = textBoxName.Text;
                _primaryRelationship.Alias = textBoxAlias.Text;
                _primaryRelationship.PrimaryColumns = primaryColumns;
                _primaryRelationship.ForeignScriptObject = (ScriptObject)comboBoxForeignScriptObject.SelectedItem;
                _primaryRelationship.ForeignColumns = foreignColumns;

                if (_primaryRelationship.GetType() == typeof(ManyToManyRelationship))
                {
                    ManyToManyRelationship manyToManyRelationship = (ManyToManyRelationship)_primaryRelationship;
                    manyToManyRelationship.IntermediatePrimaryRelationship = (OneToManyRelationship)comboBoxIntermediatePrimaryRelationship.SelectedItem;
                    manyToManyRelationship.IntermediateForeignRelationship = (ManyToOneRelationship)comboBoxIntermediateForeignRelationship.SelectedItem;
                }
            }
        }

        private void textBoxName_KeyUp(object sender, KeyEventArgs e)
        {
            if (_primaryRelationship == null)
            {
                textBoxAlias.Text = textBoxName.Text;
            }
        }

        private void comboBoxPrimaryScriptObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptObject primaryScriptObject = (ScriptObject)comboBoxPrimaryScriptObject.SelectedItem;

            comboBoxPrimaryColumn.DataSource = primaryScriptObject.Columns;
            comboBoxPrimaryColumn.DisplayMember = "Alias";
            comboBoxPrimaryColumn.ValueMember = "Name";

            comboBoxPrimaryFilter.DataSource = primaryScriptObject.Filters;
            comboBoxPrimaryFilter.DisplayMember = "Alias";
            comboBoxPrimaryFilter.ValueMember = "Name";

            // If Stored Procedure the filter must contain required paramters
            if (_primaryRelationship == null && primaryScriptObject.GetType() == typeof(ArchAngel.Providers.Database.Model.StoredProcedure))
            {
                ArchAngel.Providers.Database.Model.StoredProcedure parentStoredProcedure = (ArchAngel.Providers.Database.Model.StoredProcedure)primaryScriptObject;
                foreach (Filter.FilterColumn filterColumn in parentStoredProcedure.Filters[0].FilterColumns)
                {
                    AddPrimaryColumn(filterColumn.Column);
                }
            }
        }

        private void comboBoxForeignScriptObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptObject foreignScriptObject = (ScriptObject)comboBoxForeignScriptObject.SelectedItem;

            comboBoxForeignColumn.DataSource = foreignScriptObject.Columns;
            comboBoxForeignColumn.DisplayMember = "Alias";
            comboBoxForeignColumn.ValueMember = "Name";

            comboBoxForeignFilter.DataSource = foreignScriptObject.Filters;
            comboBoxForeignFilter.DisplayMember = "Alias";
            comboBoxForeignFilter.ValueMember = "Name";

            // If Stored Procedure the filter must contain required paramters
            if (_primaryRelationship == null && foreignScriptObject.GetType() == typeof(ArchAngel.Providers.Database.Model.StoredProcedure))
            {
                ArchAngel.Providers.Database.Model.StoredProcedure parentStoredProcedure = (ArchAngel.Providers.Database.Model.StoredProcedure)foreignScriptObject;
                foreach (Filter.FilterColumn filterColumn in parentStoredProcedure.Filters[0].FilterColumns)
                {
                    AddForeignColumn(filterColumn.Column);
                }
            }
        }

        private void listViewPrimaryColumn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            Point point = new Point(e.X, e.Y);

            if (listViewPrimaryColumn.SelectedItems.Count == 0)
                return;

            contextMenuStripRelationship.Show(listViewPrimaryColumn, point);
        }

        private void listViewForeignColumn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            Point point = new Point(e.X, e.Y);

            if (listViewForeignColumn.SelectedItems.Count == 0)
                return;

            contextMenuStripRelationship.Show(listViewForeignColumn, point);
        }

        private void toolStripMenuItemRelationshipDelete_Click(object sender, EventArgs e)
        {
            ListView listView = (ListView)contextMenuStripRelationship.SourceControl;
            listView.Items.Remove(listView.SelectedItems[0]);
        }

        private void buttonPrimaryColumnAdd_Click(object sender, EventArgs e)
        {
            Column column = (Column)comboBoxPrimaryColumn.SelectedItem;
            AddPrimaryColumn(column);   
        }

        private void AddPrimaryColumn(Column column)
        {
            ListViewItem item = new ListViewItem(column.Alias);
            item.SubItems.Add(column.Alias);
            item.Tag = column;

            listViewPrimaryColumn.Items.Add(item);
        }

        private void buttonForeignColumnAdd_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            if (comboBoxForeignColumn.Text == "")
            {
                errorProvider.SetError(comboBoxForeignColumn, "Must Select Foreign Column.");
                return;
            }

            Column column = (Column)comboBoxForeignColumn.SelectedItem;
            AddForeignColumn(column);
        }

        private void AddForeignColumn(Column column)
        {
            ListViewItem item = new ListViewItem(column.Alias);
            item.SubItems.Add(column.Alias);
            item.Tag = column;

            listViewForeignColumn.Items.Add(item);
        }

        private void listViewPrimaryColumn_Resize(object sender, EventArgs e)
        {
            foreach (ColumnHeader columnHeader in listViewPrimaryColumn.Columns)
            {
                columnHeader.Width = (listViewPrimaryColumn.Width - 5) / listViewPrimaryColumn.Columns.Count;
            }
        }

        private void listViewForeignColumn_Resize(object sender, EventArgs e)
        {
            foreach (ColumnHeader columnHeader in listViewForeignColumn.Columns)
            {
                columnHeader.Width = (listViewForeignColumn.Width - 5) / listViewForeignColumn.Columns.Count;
            }
        }

        private void comboBoxIntermediatePrimaryRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {
            OneToManyRelationship oneToManyRelationship = (OneToManyRelationship)comboBoxIntermediatePrimaryRelationship.SelectedItem;
            ManyToOneRelationship[] manyToOneRelationships = (ManyToOneRelationship[])oneToManyRelationship.ForeignScriptObject.ManyToOneRelationships;

            comboBoxIntermediateForeignRelationship.DataSource = manyToOneRelationships;
            comboBoxIntermediateForeignRelationship.DisplayMember = "Alias";
            comboBoxIntermediateForeignRelationship.ValueMember = "Name";
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

            foreach (Relationship relationship in _parent.Relationships)
            {
                if (textBoxName.Text == relationship.Name && _primaryRelationship != relationship)
                {
                    errorProvider.SetError(textBoxName, "Name Allready Exists.");
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void textBoxAlias_Validating(object sender, CancelEventArgs e)
        {
            //errorProvider.Clear();

            //if (textBoxAlias.Text == "")
            //{
            //    errorProvider.SetError(textBoxAlias, "Must Include Alias.");
            //    e.Cancel = true;
            //    return;
            //}

            //foreach (Relationship relationship in _parent.Relationships)
            //{
            //    if (textBoxAlias.Text == relationship.Alias && _primaryRelationship != relationship)
            //    {
            //        errorProvider.SetError(textBoxAlias, "Alias Allready Exists.");
            //        e.Cancel = true;
            //        return;
            //    }
            //}
        }

        private void comboBoxPrimaryScriptObject_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (!radioButtonOneToOne.Checked &&
                !radioButtonOneToMany.Checked &&
                !radioButtonManyToOne.Checked)
            {
                return;
            }

            if (comboBoxPrimaryScriptObject.Text == "")
            {
                errorProvider.SetError(comboBoxPrimaryScriptObject, "Must Select Primary Script Object.");
                e.Cancel = true;
                return;
            }
        }

        private void listViewPrimaryColumn_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (!radioButtonOneToOne.Checked &&
                !radioButtonOneToMany.Checked &&
                !radioButtonManyToOne.Checked)
            {
                return;
            }

            if (listViewPrimaryColumn.Items.Count == 0)
            {
                errorProvider.SetError(listViewPrimaryColumn, "Must Include Primary Columns.");
                e.Cancel = true;
                return;
            }
        }

        private void comboBoxPrimaryFilter_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (!radioButtonOneToOne.Checked &&
                !radioButtonOneToMany.Checked &&
                !radioButtonManyToOne.Checked)
            {
                return;
            }

            if (comboBoxPrimaryFilter.Text == "")
            {
                errorProvider.SetError(comboBoxPrimaryFilter, "Must Select Primary Filter.");
                e.Cancel = true;
                return;
            }
        }

        private void comboBoxIntermediatePrimaryRelationship_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (!radioButtonManyToMany.Checked)
            {
                return;
            }

            if (comboBoxIntermediatePrimaryRelationship.Text == "")
            {
                errorProvider.SetError(comboBoxIntermediatePrimaryRelationship, "Must Select Intermediate Primary Relationship.");
                e.Cancel = true;
                return;
            }
        }

        private void comboBoxIntermediateForeignRelationship_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (!radioButtonManyToMany.Checked)
            {
                return;
            }

            if (comboBoxIntermediateForeignRelationship.Text == "")
            {
                errorProvider.SetError(comboBoxIntermediateForeignRelationship, "Must Select Intermediate Foreign Relationship.");
                e.Cancel = true;
                return;
            }
        }

        private void comboBoxForeignScriptObject_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (!radioButtonOneToOne.Checked &&
                !radioButtonOneToMany.Checked &&
                !radioButtonManyToOne.Checked)
            {
                return;
            }

            if (comboBoxForeignScriptObject.Text == "")
            {
                errorProvider.SetError(comboBoxForeignScriptObject, "Must Select Foreign Script Object.");
                e.Cancel = true;
                return;
            }
        }

        private void listViewForeignColumn_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (!radioButtonOneToOne.Checked &&
                !radioButtonOneToMany.Checked &&
                !radioButtonManyToOne.Checked)
            {
                return;
            }

            if (listViewForeignColumn.Items.Count == 0)
            {
                errorProvider.SetError(listViewForeignColumn, "Must Include Foreign Columns.");
                e.Cancel = true;
                return;
            }

            if (listViewForeignColumn.Items.Count != listViewPrimaryColumn.Items.Count)
            {
                errorProvider.SetError(listViewForeignColumn, "Foreign Column Count Must Equal Primary Column Count.");
                e.Cancel = true;
                return;
            }
        }

        private void comboBoxForeignFilter_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (!radioButtonOneToOne.Checked &&
                !radioButtonOneToMany.Checked &&
                !radioButtonManyToOne.Checked)
            {
                return;
            }

            if (comboBoxForeignFilter.Text == "")
            {
                errorProvider.SetError(comboBoxForeignFilter, "Must Include Foreign Filter.");
                e.Cancel = true;
                return;
            }
        }

        private void buttonAddPrimaryFilter_Click(object sender, EventArgs e)
        {
            FormFilter form = new FormFilter((ScriptObject)comboBoxPrimaryScriptObject.SelectedItem, _parent.Database.EnabledScriptObjects);
            // Offset new form
            form.StartPosition = FormStartPosition.Manual;
            form.Left = this.Left + 20;
            form.Top = this.Top + 20;
            if (form.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            form.Filter.Parent.AddFilter(form.Filter);

            comboBoxPrimaryScriptObject_SelectedIndexChanged(null, null);
            comboBoxPrimaryFilter.SelectedItem = form.Filter;
        }

        private void buttonAddIntermediatePrimaryRelationship_Click(object sender, EventArgs e)
        {
            FormRelationship form = new FormRelationship(typeof(OneToManyRelationship), _parent, _parent.Database.EnabledScriptObjects);
            // Offset new form
            form.StartPosition = FormStartPosition.Manual;
            form.Left = this.Left + 20;
            form.Top = this.Top + 20;
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ScriptObject primaryScriptObject = form.PrimaryRelationship.Parent;
                primaryScriptObject.AddRelationship(form.PrimaryRelationship);

                ScriptObject foreignScriptObject = form.ForeignRelationship.Parent;
                foreignScriptObject.AddRelationship(form.ForeignRelationship);

                foreach (OneToManyRelationship oneToManyRelationship in _parent.OneToManyRelationships)
                {
                    comboBoxIntermediatePrimaryRelationship.Items.Add(oneToManyRelationship);
                }
                comboBoxIntermediatePrimaryRelationship.DisplayMember = "Alias";
                comboBoxIntermediatePrimaryRelationship.ValueMember = "Name";

                comboBoxIntermediatePrimaryRelationship.SelectedItem = form.PrimaryRelationship;
            }
        }

        private void buttonAddIntermediateForeignRelationship_Click(object sender, EventArgs e)
        {
            if (comboBoxIntermediatePrimaryRelationship.SelectedItem == null)
            {
                errorProvider.SetError(comboBoxIntermediatePrimaryRelationship, "Must Select Intermediate Primary Script Object.");
                return;
            }

            ScriptObject intermediateForeignScriptObject = ((Relationship)comboBoxIntermediatePrimaryRelationship.SelectedItem).ForeignScriptObject;
            FormRelationship form = new FormRelationship(typeof(ManyToOneRelationship), intermediateForeignScriptObject, intermediateForeignScriptObject.Database.EnabledScriptObjects);
            // Offset new form
            form.StartPosition = FormStartPosition.Manual;
            form.Left = this.Left + 20;
            form.Top = this.Top + 20;
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ScriptObject primaryScriptObject = form.PrimaryRelationship.Parent;
                primaryScriptObject.AddRelationship(form.PrimaryRelationship);

                ScriptObject foreignScriptObject = form.ForeignRelationship.Parent;
                foreignScriptObject.AddRelationship(form.ForeignRelationship);

                comboBoxIntermediatePrimaryRelationship_SelectedIndexChanged(null, null);
                comboBoxIntermediateForeignRelationship.SelectedItem = form.PrimaryRelationship;
            }
        }

        private void buttonAddForeignFilter_Click(object sender, EventArgs e)
        {
            if (comboBoxForeignScriptObject.SelectedItem == null)
            {
                errorProvider.SetError(comboBoxForeignScriptObject, "Must Select Foreign Script Object.");
                return;
            }

            FormFilter form = new FormFilter((ScriptObject)comboBoxForeignScriptObject.SelectedItem, _parent.Database.EnabledScriptObjects);
            // Offset new form
            form.StartPosition = FormStartPosition.Manual;
            form.Left = this.Left + 20;
            form.Top = this.Top + 20;
            if (form.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            form.Filter.Parent.AddFilter(form.Filter);

            comboBoxForeignScriptObject_SelectedIndexChanged(null, null);
            comboBoxForeignFilter.SelectedItem = form.Filter;
        }

        private void FormRelationship_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controller.UnshadeMainForm();
        }

        private void textBoxAlias_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
            string failReason;

            string originalAlias = _primaryRelationship.Alias;
            _primaryRelationship.Alias = textBoxAlias.Text.Trim();

            if (!_primaryRelationship.AliasValidate(_primaryRelationship, out failReason))
            {
                //textBoxAliasPlural.Text = _primaryRelationship.AliasPluralDefault(_primaryRelationship);
                _primaryRelationship.Alias = originalAlias;
                errorProvider.SetError(textBoxAlias, failReason);
                textBoxAlias.BackColor = ErrorBackColor;
                //e.Cancel = true;
                return;
            }
            //textBoxAliasPlural.Text = _primaryRelationship.AliasPluralDefault(_primaryRelationship);
            _primaryRelationship.Alias = originalAlias; // Reset, so we don't inadvertantly save
            textBoxAlias.BackColor = Color.White;
        }
    }
}