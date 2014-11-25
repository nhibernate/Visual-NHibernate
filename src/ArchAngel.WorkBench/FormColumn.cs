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
    public partial class FormColumn : Form
    {
        private ScriptObject _parent;
        private Column _column;
        private Color ErrorBackColor = Color.BlanchedAlmond;
        private bool IsNew = false;

        public Column Column
        {
            get { return _column; }
        }

        public FormColumn(ScriptObject parent)
        {
            InitializeComponent();
            ucHeading1.Text = "";
            this.BackColor = Slyce.Common.Colors.BackgroundColor;

            _parent = parent;
            Controller.ShadeMainForm();
        }

        public FormColumn(Column column)
        {
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;

            _parent = column.Parent;
            _column = column;
            ucHeading1.Text = "";
            Controller.ShadeMainForm();
        }

        private void FormColumn_Load(object sender, EventArgs e)
        {
            comboBoxDataType.DataSource = new ArchAngel.Providers.Database.BLL.Helper(_parent.Database.DatabaseType).GetDataTypes();

            if (_column == null)
            {
                IsNew = true;
                this.Text = "Add New Column";
                _column = new Column("", true, _parent, _parent.Columns.Length, true, "string", 100, false, false, "", true);
            }
            else
            {
                this.Text = "Edit Column " + _column.Name;

                textBoxName.Text = _column.Name;
                textBoxAlias.Text = _column.Alias;
                textBoxAliasDisplay.Text = _column.AliasDisplay;
                checkBoxIsNullable.Checked = _column.IsNullable;
                comboBoxDataType.Text = _column.DataType;
                textBoxCharacterMaximumLength.Text = _column.CharacterMaximumLength.ToString();
                textBoxOrdinalPosition.Text = _column.OrdinalPosition.ToString();
                textBoxDefault.Text = _column.Default;

                if (!_column.IsUserDefined)
                {
                    textBoxName.ReadOnly = true;
                    checkBoxIsNullable.Enabled = false;
                    comboBoxDataType.Enabled = false;
                    textBoxCharacterMaximumLength.Enabled = false;
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

            if (IsNew)
            {
                _column = new Column(textBoxName.Text, true, _parent, _parent.Columns.Length, checkBoxIsNullable.Checked, comboBoxDataType.Text, Convert.ToInt32(textBoxCharacterMaximumLength.Text), false, false, textBoxDefault.Text, true);
                _column.Alias = textBoxAlias.Text;
                _column.AliasDisplay = textBoxAliasDisplay.Text;
            }
            else
            {
                _column.Name = textBoxName.Text;
                _column.Alias = textBoxAlias.Text;
                _column.AliasDisplay = textBoxAliasDisplay.Text;
                _column.IsNullable = checkBoxIsNullable.Checked;
                _column.DataType = comboBoxDataType.Text;
                _column.CharacterMaximumLength = Convert.ToInt32(textBoxCharacterMaximumLength.Text);
            }
        }

        //private void textBoxName_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (_column == null)
        //    {
        //        textBoxAlias.Text = textBoxName.Text;
        //        textBoxAliasDisplay.Text = textBoxName.Text;
        //    }
        //}

        //private void textBoxName_Validating(object sender, CancelEventArgs e)
        //{
        //    errorProvider.Clear();

        //    if (textBoxName.Text == "")
        //    {
        //        errorProvider.SetError(textBoxName, "Must Include Name.");
        //        e.Cancel = true;
        //        return;
        //    }

        //    foreach (Column column in _parent.Columns)
        //    {
        //        if (textBoxName.Text == column.Name && _column != column)
        //        {
        //            errorProvider.SetError(textBoxName, "Name Allready Exists.");
        //            e.Cancel = true;
        //            return;
        //        }
        //    }
        //}

        //private void textBoxAlias_Validating(object sender, CancelEventArgs e)
        //{
        //    errorProvider.Clear();

        //    if (textBoxAlias.Text == "")
        //    {
        //        errorProvider.SetError(textBoxAlias, "Must Include Alias.");
        //        e.Cancel = true;
        //        return;
        //    }

        //    foreach (Column column in _parent.Columns)
        //    {
        //        if (textBoxAlias.Text == column.Alias && _column != column)
        //        {
        //            errorProvider.SetError(textBoxAlias, "Alias Allready Exists.");
        //            e.Cancel = true;
        //            return;
        //        }
        //    }
        //}

        //private void textBoxAliasDisplay_Validating(object sender, CancelEventArgs e)
        //{
        //    errorProvider.Clear();

        //    if (textBoxAliasDisplay.Text == "")
        //    {
        //        errorProvider.SetError(textBoxAliasDisplay, "Must Include Alias Display.");
        //        e.Cancel = true;
        //        return;
        //    }

        //    foreach (Column column in _parent.Columns)
        //    {
        //        if (textBoxAliasDisplay.Text == column.AliasDisplay && _column != column)
        //        {
        //            errorProvider.SetError(textBoxAliasDisplay, "Alias Display Allready Exists.");
        //            e.Cancel = true;
        //            return;
        //        }
        //    }
        //}

        private void comboBoxDataType_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (comboBoxDataType.Text == "")
            {
                errorProvider.SetError(comboBoxDataType, "Must Include Data Type");
                e.Cancel = true;
                return;
            }
        }

        private void textBoxCharacterMaximumLength_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (textBoxCharacterMaximumLength.Text == "")
            {
                errorProvider.SetError(textBoxCharacterMaximumLength, "Must Include Character Maximum Length");
                e.Cancel = true;
                return;
            }

            try
            {
                int.Parse(textBoxCharacterMaximumLength.Text);
            }
            catch
            {
                errorProvider.SetError(textBoxCharacterMaximumLength, "Must Include Valid Character Maximum Length");
                e.Cancel = true;
                return;
            }
        }

        private void FormColumn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controller.UnshadeMainForm();
        }

        private void textBoxAlias_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
            string failReason;

            string originalAlias = _column.Alias;
            _column.Alias = textBoxAlias.Text.Trim();

            if (!_column.AliasValidate(_column, out failReason))
            {
                if (IsNew)
                {
                    textBoxAliasDisplay.Text = _column.AliasDisplayDefault(_column);
                }
                _column.Alias = originalAlias;
                errorProvider.SetError(textBoxAlias, failReason);
                textBoxAlias.BackColor = ErrorBackColor;
                //e.Cancel = true;
                return;
            }
            if (IsNew)
            {
                textBoxAliasDisplay.Text = _column.AliasDisplayDefault(_column);
            }
            _column.Alias = originalAlias; // Reset, so we don't inadvertantly save
            textBoxAlias.BackColor = Color.White;
        }

        private void textBoxAliasDisplay_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
            string failReason;

            string originalAliasDisplay = _column.AliasDisplay;
            _column.AliasDisplay = textBoxAliasDisplay.Text.Trim();

            if (!_column.AliasDisplayValidate(_column, out failReason))
            {
                _column.AliasDisplay = originalAliasDisplay;
                errorProvider.SetError(textBoxAliasDisplay, failReason);
                textBoxAliasDisplay.BackColor = ErrorBackColor;
                //e.Cancel = true;
                return;
            }
            _column.AliasDisplay = originalAliasDisplay; // Reset, so we don't inadvertantly save
            textBoxAliasDisplay.BackColor = Color.White;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
            string failReason;

            string originalName = _column.Name;
            _column.Name = textBoxName.Text.Trim();

            if (!_column.NameValidate(_column, out failReason))
            {
                if (IsNew)
                {
                    textBoxAlias.Text = _column.AliasDefault(_column);
                }
                _column.Name = originalName;
                errorProvider.SetError(textBoxName, failReason);
                textBoxName.BackColor = ErrorBackColor;
                //e.Cancel = true;
                return;
            }
            if (IsNew)
            {
                textBoxAlias.Text = _column.AliasDefault(_column);
            }
            _column.Name = originalName; // Reset, so we don't inadvertantly save
            textBoxName.BackColor = Color.White;
        }

    }
}