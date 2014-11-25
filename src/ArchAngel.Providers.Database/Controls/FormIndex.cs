using System;
using System.ComponentModel;
using System.Windows.Forms;
// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.Controls
{
    public partial class FormIndex : Form
    {
    	readonly ScriptObject _parent;
        Index _index;

        public Index Index
        {
            get { return _index; }
        }

        public FormIndex(ScriptObject parent)
        {
            InitializeComponent();
            BackColor = Slyce.Common.Colors.BackgroundColor;

            _parent = parent;
            ucHeading1.Text = "";
            Interfaces.Events.ShadeMainForm();
        }

        public FormIndex(Index index)
        {
            InitializeComponent();
            BackColor = Slyce.Common.Colors.BackgroundColor;

            _parent = index.Parent;
            _index = index;
            ucHeading1.Text = "";
            Interfaces.Events.ShadeMainForm();
        }

        private void FormIndex_Load(object sender, EventArgs e)
        {
            if (_index == null)
            {
                Text = "Add New Index";
            }
            else
            {
                Text = "Edit Index " + _index.Name;

                textBoxName.Text = _index.Name;
                textBoxAlias.Text = _index.Alias;
                textBoxType.Text = _index.Type;
                textBoxDescription.Text = _index.Description;

                listBoxColumn.DisplayMember = "Alias";
                foreach (Column column in _index.Columns)
                {
                    listBoxColumn.Items.Add(column);
                }

                if (!_index.IsUserDefined)
                {
                    textBoxName.ReadOnly = true;
                    textBoxType.Enabled = false;
                    listBoxColumn.Enabled = false;
                }
            }
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

            if (_index == null)
            {
                _index = new Index(textBoxName.Text, true, textBoxType.Text, _parent, false, false);
            }
            else
            {
                _index.Name = textBoxName.Text;
                _index.Type = textBoxType.Text;
            }
            _index.Alias = textBoxAlias.Text;
            _index.Description = textBoxDescription.Text;
        }

        private void textBoxName_KeyUp(object sender, KeyEventArgs e)
        {
            if (_index == null)
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

            if (_parent.GetType() != typeof(Table))
            {
                return;
            }

            Table table = (Table)_parent;
            foreach (Index index in table.Indexes)
            {
                if (textBoxName.Text == index.Name && _index != index)
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

        private void FormIndex_FormClosed(object sender, FormClosedEventArgs e)
        {
            Interfaces.Events.UnShadeMainForm();
        }
    }
}