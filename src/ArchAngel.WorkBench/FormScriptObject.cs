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
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Workbench
{
    public partial class FormScriptObject : Form
    {
        ScriptObject _scriptObject;
        ScriptObject[] _scriptObjects;
        bool IsNew = false;
        internal ArchAngel.Providers.Database.Model.Database ParentDatabase;
        private Color ErrorBackColor = Color.BlanchedAlmond;

        public bool UpdateTreeView
        {
            get { return checkBoxRenameRelatedObjects.Checked; }
        }

        public ScriptObject ScriptObject
        {
            get { return _scriptObject; }
        }

        public FormScriptObject(ScriptObject[] scriptObjects, ArchAngel.Providers.Database.Model.Database parentDatabase)
        {
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
            this.ParentDatabase = parentDatabase;

            _scriptObjects = scriptObjects;
            ucHeading1.Text = "";
            Controller.ShadeMainForm();
        }

        public FormScriptObject(ScriptObject scriptObject, ScriptObject[] scriptObjects)
        {
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;

            _scriptObject = scriptObject;
            _scriptObjects = scriptObjects;
            ucHeading1.Text = "";
            Controller.ShadeMainForm();
        }

        private void FormScriptObject_Load(object sender, EventArgs e)
        {
            if (_scriptObject == null)
            {
                IsNew = true;
                _scriptObject = new ScriptObject("", true);
                _scriptObject.Database = ParentDatabase;

                if (_scriptObjects.GetType() == typeof(ArchAngel.Providers.Database.Model.Table[]))
                {
                    this.Text = "Add New Table";
                }

                if (_scriptObjects.GetType() == typeof(ArchAngel.Providers.Database.Model.View[]))
                {
                    this.Text = "Add New View";
                }

                if (_scriptObjects.GetType() == typeof(ArchAngel.Providers.Database.Model.StoredProcedure[]))
                {
                    this.Text = "Add New Stored Procedure";
                }

                checkBoxRenameRelatedObjects.Enabled = false;
            }
            else
            {
                this.Text = "Edit " + _scriptObject.GetType().Name + " " + _scriptObject.Name;
            }

                textBoxName.Text = _scriptObject.Name;
                textBoxAlias.Text = _scriptObject.Alias;
                textBoxAliasPlural.Text = _scriptObject.AliasPlural;

                if (!_scriptObject.IsUserDefined)
                {
                    textBoxName.ReadOnly = true;
                }
            //}
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            for (int i = this.Controls.Count - 1; i >= 0; i--)
            {
                Control control = this.Controls[i];
                control.Focus();
                if (!Validate())
                {
                    DialogResult = DialogResult.None;
                    return;
                }
            }

            if (IsNew)
            {
                if (_scriptObjects.GetType() == typeof(ArchAngel.Providers.Database.Model.Table[]))
                {
                    _scriptObject = new ArchAngel.Providers.Database.Model.Table(textBoxName.Text, true);
                }

                if (_scriptObjects.GetType() == typeof(ArchAngel.Providers.Database.Model.View[]))
                {
                    _scriptObject = new ArchAngel.Providers.Database.Model.View(textBoxName.Text, true);
                }

                if (_scriptObjects.GetType() == typeof(ArchAngel.Providers.Database.Model.StoredProcedure[]))
                {
                    _scriptObject = new ArchAngel.Providers.Database.Model.StoredProcedure(textBoxName.Text, true);
                }
                _scriptObject.Alias = textBoxAlias.Text;
                _scriptObject.AliasPlural = textBoxAliasPlural.Text;
            }
            else
            {
                _scriptObject.Name = textBoxName.Text;
                _scriptObject.Alias = textBoxAlias.Text;
                _scriptObject.AliasPlural = textBoxAliasPlural.Text;

                if (checkBoxRenameRelatedObjects.Checked)
                {
                    RenameRelatedObjects();
                }
            }
        }

        private void textBoxName_KeyUp(object sender, KeyEventArgs e)
        {
            if (IsNew)
            {
                //textBoxAlias.Text = textBoxName.Text;
                _scriptObject.Name = textBoxName.Text;
                textBoxAlias.Text = _scriptObject.AliasDefault(_scriptObject);
                //textBoxAliasPlural.Text = Script.GetPlural(textBoxAlias.Text);
            }
        }

        private void RenameRelatedObjects()
        {
            //TODO:
            return;

            foreach (ScriptObject scriptObject in _scriptObjects)
            {
                foreach (Relationship relationship in scriptObject.Relationships)
                {
                    if (relationship.ForeignScriptObject != _scriptObject)
                    {
                        continue;
                    }

                    if (relationship.GetType() == typeof(OneToOneRelationship) ||
                        relationship.GetType() == typeof(ManyToOneRelationship))
                    {
                        relationship.Alias = _scriptObject.Alias;
                    }

                    if (relationship.GetType() == typeof(OneToManyRelationship) ||
                        relationship.GetType() == typeof(ManyToManyRelationship))
                    {
                        relationship.Alias = _scriptObject.AliasPlural;
                    }
                }
            }

            if (_scriptObject.GetType() != typeof(ArchAngel.Providers.Database.Model.Table))
            {
                return;
            }

            ArchAngel.Providers.Database.Model.Table table = (ArchAngel.Providers.Database.Model.Table)_scriptObject;

            foreach (Index index in table.Indexes)
            {
                if (index.Type == DatabaseConstant.KeyType.Unique)
                {
                    string filterAlias = ArchAngel.Providers.Database.BLL.Helper.GetFilterAlias(index);
                    Filter filter = ArchAngel.Providers.Database.BLL.Search.GetFilter(table.Filters, index.Name);

                    filter.Alias = filterAlias;
                }
            }

            foreach (Key key in table.Keys)
            {
                if (key.Type == DatabaseConstant.KeyType.Unique ||
                    key.Type == DatabaseConstant.KeyType.Primary ||
                    key.Type == DatabaseConstant.KeyType.Foreign)
                {
                    string filterAlias = ArchAngel.Providers.Database.BLL.Helper.GetFilterAlias(key);
                    Filter filter = ArchAngel.Providers.Database.BLL.Search.GetFilter(table.Filters, key.Name);

                    filter.Alias = filterAlias;
                }
            }
        }

        private void textBoxName_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();
            string failReason;

            string originalName = _scriptObject.Name;
            _scriptObject.Name = textBoxName.Text.Trim();

            if (!_scriptObject.NameValidate(_scriptObject, out failReason))
            {
                _scriptObject.Name = originalName;
                errorProvider.SetError(textBoxName, failReason);
                textBoxName.BackColor = ErrorBackColor;
                e.Cancel = true;
                return;
            }
            _scriptObject.Name = originalName; // Reset, so we don't inadvertantly save
            textBoxName.BackColor = Color.White;
        }

        private void FormScriptObject_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controller.UnshadeMainForm();
        }

        private void textBoxAlias_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
            string failReason;

            string originalAlias = _scriptObject.Alias;
            _scriptObject.Alias = textBoxAlias.Text.Trim();

            if (!_scriptObject.AliasValidate(_scriptObject, out failReason))
            {
                textBoxAliasPlural.Text = _scriptObject.AliasPluralDefault(_scriptObject);
                _scriptObject.Alias = originalAlias;
                errorProvider.SetError(textBoxAlias, failReason);
                textBoxAlias.BackColor = ErrorBackColor;
                //e.Cancel = true;
                return;
            }
            textBoxAliasPlural.Text = _scriptObject.AliasPluralDefault(_scriptObject);
            _scriptObject.Alias = originalAlias; // Reset, so we don't inadvertantly save
            textBoxAlias.BackColor = Color.White;
        }

        private void textBoxAliasPlural_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
            string failReason;

            string originalAliasPlural = _scriptObject.AliasPlural;
            _scriptObject.AliasPlural = textBoxAliasPlural.Text.Trim();

            if (!_scriptObject.AliasPluralValidate(_scriptObject, out failReason))
            {
                _scriptObject.AliasPlural = originalAliasPlural;
                errorProvider.SetError(textBoxAliasPlural, failReason);
                textBoxAliasPlural.BackColor = ErrorBackColor;
                //e.Cancel = true;
                return;
            }
            _scriptObject.AliasPlural = originalAliasPlural; // Reset, so we don't inadvertantly save
            textBoxAliasPlural.BackColor = Color.White;
        }


    }
}