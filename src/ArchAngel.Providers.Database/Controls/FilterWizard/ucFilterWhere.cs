using System;

namespace ArchAngel.Providers.Database.Controls.FilterWizard
{
    public partial class ucFilterWhere : Interfaces.Controls.ContentItems.ContentItem
    {
    	readonly FormFilter2 Owner;

        public ucFilterWhere(FormFilter2 owner)
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
            Owner.TheFilter.UseCustomWhere = checkBoxOverride.Checked;
            Owner.TheFilter.CustomWhere = syntaxEditorCustomWhere.Text;
            return true;
        }

        public override bool Back()
        {
            return true;
        }

        private void Populate()
        {
            checkBoxOverride.Checked = Owner.TheFilter.UseCustomWhere;
            syntaxEditorCustomWhere.Text = Owner.TheFilter.CustomWhere;
            syntaxEditorCustomWhere.Enabled = Owner.TheFilter.UseCustomWhere;
        }

        private void checkBoxOverride_CheckedChanged(object sender, EventArgs e)
        {
            syntaxEditorCustomWhere.Enabled = checkBoxOverride.Checked;
            //syntaxEditorCustomWhere.Document.ReadOnly = !checkBoxOverride.Checked;
        }

    }
}
