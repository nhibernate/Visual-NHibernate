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
    public partial class FormKey : Form
    {
        ScriptObject _parent;
        Key _key;

        public Key Key
        {
            get { return _key; }
        }

        public FormKey(ScriptObject parent)
        {
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;

            _parent = parent;
            ucHeading1.Text = "";
            Controller.ShadeMainForm();
        }

        public FormKey(Key key)
        {
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;

            _parent = key.Parent;
            _key = key;
            ucHeading1.Text = "";
            Controller.ShadeMainForm();
        }

        private void FormKey_Load(object sender, EventArgs e)
        {
            if (_key == null)
            {
                this.Text = "Add New Key";
            }
            else
            {
                this.Text = "Edit Key " + _key.Name;

                textBoxName.Text = _key.Name;
                textBoxAlias.Text = _key.Alias;
                textBoxType.Text = _key.Type;

                listBoxColumn.DisplayMember = "Alias";
                foreach (Column column in _key.Columns)
                {
                    listBoxColumn.Items.Add(column);
                }

                if (!_key.IsUserDefined)
                {
                    textBoxName.ReadOnly = true;
                    textBoxType.Enabled = false;
                    listBoxColumn.Enabled = false;
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

            if (_key == null)
            {
                _key = new Key(textBoxName.Text, true, textBoxType.Text, _parent, false);
                _key.Alias = textBoxAlias.Text;
            }
            else
            {
                _key.Name = textBoxName.Text;
                _key.Alias = textBoxAlias.Text;
                _key.Type = textBoxType.Text;
            }
        }

        private void textBoxName_KeyUp(object sender, KeyEventArgs e)
        {
            if (_key == null)
            {
                textBoxAlias.Text = textBoxName.Text;
            }
        }

        private void textBoxName_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (textBoxName.Text == "")
            {
                errorProvider.SetError(textBoxAlias, "Must Include Name.");
                e.Cancel = true;
                return;
            }

            if (_parent.GetType() != typeof(ArchAngel.Providers.Database.Model.Table))
            {
                return;
            }

            ArchAngel.Providers.Database.Model.Table table = (ArchAngel.Providers.Database.Model.Table)_parent;
            foreach (Key key in table.Keys)
            {
                if (textBoxName.Text == key.Name && _key != key)
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
        }

        private void textBoxType_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (textBoxType.Text == "")
            {
                errorProvider.SetError(textBoxType, "Must Include Type");
                e.Cancel = true;
                return;
            }
        }

        private void FormKey_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controller.UnshadeMainForm();
        }
    }
}