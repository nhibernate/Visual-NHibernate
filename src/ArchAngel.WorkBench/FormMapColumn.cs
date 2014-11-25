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
    public partial class FormMapColumn : Form
    {
        ScriptObject _parent;
        MapColumn _mapColumn;

        public MapColumn MapColumn
        {
            get { return _mapColumn; }
        }

        public FormMapColumn(ScriptObject parent)
        {
            InitializeComponent();

            _parent = parent;
            ucHeading1.Text = "";
            Controller.ShadeMainForm();
        }

        public FormMapColumn(ScriptObject parent, MapColumn mapColumn)
        {
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;

            _parent = parent;
            _mapColumn = mapColumn;
            ucHeading1.Text = "";
            Controller.ShadeMainForm();
        }

        private void FormMapColumn_Load(object sender, EventArgs e)
        {
            List<Relationship> relationships = new List<Relationship>();
            FillRelationships(_parent, relationships);

            comboBoxRelationship.DataSource = relationships;
            comboBoxRelationship.DisplayMember = "Alias";
            comboBoxRelationship.ValueMember = "Name";

            if (_mapColumn == null)
            {
                this.Text = "Add Map Column";
            }
            else
            {
                this.Text = "Edit Map Column " + _mapColumn.Name;

                comboBoxRelationship.SelectedItem = _mapColumn.RelationshipPath[0];
                comboBoxMapColumn.SelectedItem = _mapColumn.ForeignColumn;

                textBoxName.Text = _mapColumn.Name;
                textBoxAlias.Text = _mapColumn.Alias;
                textBoxAliasDisplay.Text = _mapColumn.AliasDisplay;

                if (!_mapColumn.IsUserDefined)
                {
                    comboBoxRelationship.Enabled = false;
                    textBoxName.ReadOnly = true;
                }
            }
        }

        private void FillRelationships(ScriptObject parent, List<Relationship> relationships)
        {
            foreach (Relationship relationship in parent.Relationships)
            {
                if (relationships.Contains(relationship))
                {
                    continue;
                }

                if (relationship.GetType() == typeof(ManyToOneRelationship))
                {
                    relationships.Add(relationship);
                    //FillRelationships(relationship.ForeignRelationship.Parent, relationships);
                }

                if (relationship.GetType() == typeof(OneToOneRelationship))
                {
                    relationships.Add(relationship);
                    //FillRelationships(relationship.ForeignRelationship.Parent, relationships);
                }
            }
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

            if (_mapColumn == null)
            {
                Relationship[] relationshipPath = new Relationship[1] { (Relationship)comboBoxRelationship.SelectedItem };
                Column column = (Column)comboBoxMapColumn.SelectedItem;
                _mapColumn = new MapColumn(textBoxName.Text, true, relationshipPath, column, _parent.Columns.Length, column.IsNullable, column.DataType, column.CharacterMaximumLength);
                _mapColumn.Alias = textBoxAlias.Text;
                _mapColumn.AliasDisplay = textBoxAliasDisplay.Text;
            }
            else
            {
                _mapColumn.ForeignColumn = (Column)comboBoxMapColumn.SelectedItem;

                _mapColumn.Name = textBoxName.Text;
                _mapColumn.Alias = textBoxAlias.Text;
                _mapColumn.AliasDisplay = textBoxAliasDisplay.Text;
            }
        }

        private void textBoxName_KeyUp(object sender, KeyEventArgs e)
        {
            if (_mapColumn == null)
            {
                textBoxAlias.Text = textBoxName.Text;
                textBoxAliasDisplay.Text = textBoxName.Text;
            }
        }

        private void comboBoxScriptObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            Relationship relationship = (Relationship)comboBoxRelationship.SelectedItem;
            comboBoxMapColumn.DataSource = relationship.ForeignRelationship.Parent.Columns;
            comboBoxMapColumn.DisplayMember = "Alias";
            comboBoxMapColumn.ValueMember = "Name";

            textBoxScriptObject.Text = relationship.ForeignRelationship.Parent.Alias;
        }

        private void comboBoxMapColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_mapColumn == null)
            {
                Column column = (Column)comboBoxMapColumn.SelectedItem;
                textBoxName.Text = column.Name;
                textBoxAlias.Text = column.Alias;
                textBoxAliasDisplay.Text = column.AliasDisplay;
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

            foreach (Column column in _parent.Columns)
            {
                if (textBoxName.Text == column.Name && _mapColumn != column)
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

            foreach (Column column in _parent.Columns)
            {
                if (textBoxAlias.Text == column.Name && _mapColumn != column)
                {
                    errorProvider.SetError(textBoxAlias, "Alias Allready Exists.");
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void textBoxAliasDisplay_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (textBoxAliasDisplay.Text == "")
            {
                errorProvider.SetError(textBoxAliasDisplay, "Must Include Alias Display.");
                e.Cancel = true;
                return;
            }

            foreach (Column column in _parent.Columns)
            {
                if (textBoxAliasDisplay.Text == column.Name && _mapColumn != column)
                {
                    errorProvider.SetError(textBoxAliasDisplay, "Alias Display Allready Exists.");
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void comboBoxRelationship_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (comboBoxRelationship.Text == "")
            {
                errorProvider.SetError(comboBoxRelationship, "Must Select Relationship.");
                e.Cancel = true;
                return;
            }
        }

        private void comboBoxMapColumn_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (comboBoxMapColumn.Text == "")
            {
                errorProvider.SetError(comboBoxMapColumn, "Must Select Map Column.");
                e.Cancel = true;
                return;
            }
        }

        private void FormMapColumn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controller.UnshadeMainForm();
        }

    }
}