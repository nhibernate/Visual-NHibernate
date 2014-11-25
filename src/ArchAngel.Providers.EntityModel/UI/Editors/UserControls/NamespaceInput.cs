using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
    public partial class NamespaceInput : Form
    {
        public NamespaceInput()
        {
            InitializeComponent();
            textBoxValue.Focus();
            buttonOk.Enabled = !string.IsNullOrEmpty(Text);
        }

        public string Text
        {
            get { return textBoxValue.Text.Trim(); }
            set
            {
                this.DialogResult = DialogResult.Cancel;
                textBoxValue.Text = value;
                textBoxValue.Focus();
                buttonOk.Enabled = !string.IsNullOrEmpty(textBoxValue.Text.Trim());
            }
        }

        public bool AutoAddToEntities
        {
            get { return checkBoxAutoAddToEntities.Checked; }
            set { checkBoxAutoAddToEntities.Checked = value; }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                MessageBox.Show(this, "Empty name not allowed", "Missing value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public string DisplayName
        {
            get { return labelDisplayName.Text; }
            set 
            { 
                labelDisplayName.Text = value;
                textBoxValue.Focus();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                buttonOk.Enabled = false;
                highlighter1.SetHighlightColor(textBoxValue, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
            }
            else
            {
                buttonOk.Enabled = true;
                highlighter1.SetHighlightColor(textBoxValue, DevComponents.DotNetBar.Validator.eHighlightColor.None);
            }
        }
    }
}
