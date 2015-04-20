using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
    public partial class FormAttributeEditor : Form
    {
        public FormAttributeEditor(string name, string argumentString, bool autoAddToEntities)
        {
            InitializeComponent();

            DialogResult = DialogResult.Cancel;
            RawName = name;
            RawArgumentString = argumentString;
            AutoAddToEntities = autoAddToEntities;
            Populate();
        }

        private void Populate()
        {
            textBoxName.Text = RawName;
            textBoxBody.Text = RawArgumentString;
            checkBoxAutoAddToEntities.Checked = AutoAddToEntities;
            buttonOk.Enabled = !string.IsNullOrEmpty(textBoxName.Text.Trim());
            textBoxName.Focus();
        }

        public string RawName
        {
            get { return textBoxName.Text.Trim(); }
            set { textBoxName.Text = value.Trim(); }
        }

        public string RawArgumentString
        {
            get { return textBoxBody.Text.Trim(); }
            set { textBoxBody.Text = value.Trim(); }
        }

        public bool AutoAddToEntities
        {
            get { return checkBoxAutoAddToEntities.Checked; }
            set { checkBoxAutoAddToEntities.Checked = value; }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(RawName))
            {
                buttonOk.Enabled = false;
                highlighter1.SetHighlightColor(textBoxName, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
            }
            else
            {
                buttonOk.Enabled = true;
                highlighter1.SetHighlightColor(textBoxName, DevComponents.DotNetBar.Validator.eHighlightColor.None);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
