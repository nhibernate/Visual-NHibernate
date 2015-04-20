using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Workbench.FilterWizard
{
    public partial class ucFilterWhere : ArchAngel.Interfaces.Controls.ContentItems.ContentItem
    {
        ArchAngel.Providers.Database.Controls.FormFilter2 Owner;

        public ucFilterWhere(ArchAngel.Providers.Database.Controls.FormFilter2 owner)
        {
            InitializeComponent();
            HasNext = true;
            HasPrev = true;
            Owner = owner;
            PageHeader = "Custom Where Clause";
            PageDescription = "If you want a custom 'where' clause for the SQL, add it here.";
            Populate();
        }

        public override bool Next()
        {
            return true;// FormFilter2.ScreenNames.Start.ToString();
        }

        public override bool Back()
        {
            return true;//FormFilter2.ScreenNames.ReturnOrder.ToString();
        }

        private void Populate()
        {
            checkBoxOverride.Checked = Owner.Filter.UseCustomWhere;
            syntaxEditorCustomWhere.Text = Owner.Filter.CustomWhere;
            syntaxEditorCustomWhere.Enabled = Owner.Filter.UseCustomWhere;
        }

        public override bool Save()
        {
            Owner.Filter.UseCustomWhere = checkBoxOverride.Checked;
            Owner.Filter.CustomWhere = syntaxEditorCustomWhere.Text;
            return true;
        }

        private void checkBoxOverride_CheckedChanged(object sender, EventArgs e)
        {
            syntaxEditorCustomWhere.Enabled = checkBoxOverride.Checked;
            //syntaxEditorCustomWhere.Document.ReadOnly = !checkBoxOverride.Checked;
        }

    }
}
