using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.Controls
{
    public partial class FormColumn : Form
    {
        private bool IsSaved = false;
        private readonly ScriptObject _parent;
        private Column _column;
        private readonly Color ErrorBackColor = Color.BlanchedAlmond;
        private bool IsNew = false;
        private DevComponents.DotNetBar.ElementStyle _StyleHighlighted;
        private DevComponents.DotNetBar.ElementStyle _StyleNormal;

        public Column Column
        {
            get { return _column; }
        }

        public FormColumn(ScriptObject parent)
        {
            InitializeComponent();
            ucHeading1.Text = "";
            BackColor = Slyce.Common.Colors.BackgroundColor;
            _parent = parent;
            Interfaces.Events.ShadeMainForm();
        }

        public FormColumn(Column column)
        {
            InitializeComponent();
            BackColor = Slyce.Common.Colors.BackgroundColor;
            _parent = column.Parent;
            _column = column;
            ucHeading1.Text = "";
            Interfaces.Events.ShadeMainForm();
        }

        private void FormColumn_Load(object sender, EventArgs e)
        {
            comboBoxDataType.DataSource = new BLL.Helper(_parent.Database.DatabaseType).GetDataTypes();
            PopulateLookups();

            if (_column == null)
            {
                IsNew = true;
                Text = "Add New Column";
                _column = new Column("", true, _parent, _parent.Columns.Length, true, "string", 100, false, false, "", true, false, 0, 0);
            }
            else
            {
                Text = "Edit Column " + _column.Name;

                textBoxName.Text = _column.Name;
                textBoxAlias.Text = _column.Alias;
                textBoxAliasDisplay.Text = _column.AliasDisplay;
                textBoxDescription.Text = _column.Description;
                checkBoxIsNullable.Checked = _column.IsNullable;
                comboBoxDataType.Text = _column.DataType;
                numEditCharMaxLength.Text = _column.Size.ToString();
                textBoxOrdinalPosition.Text = _column.OrdinalPosition.ToString();
                textBoxDefault.Text = _column.Default;
                numEditPrecision.Text = _column.Precision.ToString();
                numEditScale.Text = _column.Scale.ToString();
                ddlLookups.SelectedItem = _column.Lookup;

                if (!_column.IsUserDefined)
                {
                    textBoxName.ReadOnly = true;
                    //checkBoxIsNullable.Enabled = false;
                    comboBoxDataType.Enabled = false;
                    numEditCharMaxLength.Enabled = false;
                }
            }
        }

        private DevComponents.DotNetBar.ElementStyle StyleHighlighted
        {
            get
            {
                if (_StyleHighlighted == null)
                {
                    _StyleHighlighted = new DevComponents.DotNetBar.ElementStyle();
                    _StyleHighlighted.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    _StyleHighlighted.Name = "StyleHighlighted";
                }
                return _StyleHighlighted;
            }
        }

        private DevComponents.DotNetBar.ElementStyle StyleNormal
        {
            get
            {
                if (_StyleNormal == null)
                {
                    _StyleNormal = new DevComponents.DotNetBar.ElementStyle();
                    _StyleNormal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    _StyleNormal.Name = "StyleNormal";
                }
                return _StyleNormal;
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
            if (IsNew)
            {
                _column = new Column(
                    textBoxName.Text,
                    true,
                    _parent,
                    _parent.Columns.Length,
                    checkBoxIsNullable.Checked,
                    comboBoxDataType.Text,
                    int.Parse(numEditCharMaxLength.Text),
                    false,
                    false,
                    textBoxDefault.Text,
                    true,
                    false,
                    int.Parse(numEditPrecision.Text),
                    int.Parse(numEditScale.Text));
            }
            else
            {
                _column.Name = textBoxName.Text;
                _column.IsNullable = checkBoxIsNullable.Checked;
                _column.DataType = comboBoxDataType.Text;
                _column.Size = Convert.ToInt32(numEditCharMaxLength.Text);
                _column.Precision = int.Parse(numEditPrecision.Text);
                _column.Scale = int.Parse(numEditScale.Text);
            }
            _column.Alias = textBoxAlias.Text;
            _column.AliasDisplay = textBoxAliasDisplay.Text;
            _column.Description = textBoxDescription.Text;
            _column.Lookup = ddlLookups.SelectedItem is Lookup ? (Lookup)ddlLookups.SelectedItem : null;
            IsSaved = true;
        }

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

            if (numEditCharMaxLength.Text == "")
            {
                errorProvider.SetError(numEditCharMaxLength, "Must Include Character Maximum Length");
                e.Cancel = true;
                return;
            }

            try
            {
                int.Parse(numEditCharMaxLength.Text);
            }
            catch
            {
                errorProvider.SetError(numEditCharMaxLength, "Must Include Valid Character Maximum Length");
                e.Cancel = true;
                return;
            }
        }

        private void FormColumn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Interfaces.Events.UnShadeMainForm();
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

        private void PopulateLookups()
        {
            ddlLookups.Items.Clear();
            ddlLookups.DisplayMember = "Alias";
            ddlLookups.Items.Add("");

            foreach (Model.Lookup lookup in Column.Parent.Database.Lookups)
            {
                ddlLookups.Items.Add(lookup);
            }
            //ddlLookups.DataSource = Column.Parent.Database.Lookups;
            //ddlLookups.DisplayMember = "Alias";
            //ddlLookups.DataSource = null;
            //ddlLookups.Items.Insert(0, "");
        }

        private void comboBoxDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Slyce.Common.Utility.StringsAreEqual(comboBoxDataType.Text, "decimal", false) ||
                Slyce.Common.Utility.StringsAreEqual(comboBoxDataType.Text, "numeric", false))
            {
                lblPrecision.Visible = lblScale.Visible = numEditPrecision.Visible = numEditScale.Visible = true;
                numEditPrecision.Enabled = numEditScale.Enabled = _column.IsUserDefined;
            }
            else
            {
                lblPrecision.Visible = lblScale.Visible = numEditPrecision.Visible = numEditScale.Visible = false;
            }
        }

        private void labelX4_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Blue);
        }
    }
}