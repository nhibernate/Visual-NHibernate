using System;

namespace ArchAngel.Providers.Database.Controls.FilterWizard
{
    public partial class ucFilterStart : Interfaces.Controls.ContentItems.ContentItem
    {
    	readonly FormFilter2 Owner;

        public ucFilterStart(FormFilter2 owner)
        {
            InitializeComponent();
            HasNext = true;
            HasPrev = true;
            Owner = owner;
            PageHeader = "Filter Info";
            PageDescription = "Provide basic information about the filter, such as name.";
            Populate();
        }

        public override void OnDataChanged(Type type, System.Reflection.MethodInfo method, object oldValue, object newValue)
        {
            if (Owner.IsAddingNewFilter &&
                type == typeof(Model.Filter) &&
                method.Name == "set_Name")
            {
                textBoxName.Text = (string)newValue;
                textBoxAlias.Text = (string)newValue;
            }
        }

        public override bool Next()
        {
            Owner.TheFilter.Name = textBoxName.Text;
            Owner.TheFilter.Alias = textBoxAlias.Text;
            Owner.TheFilter.Description = textBoxDescription.Text;
            Owner.TheFilter.IsReturnTypeCollection = radioButtonCollection.Checked;
            Owner.TheFilter.CreateStoredProcedure = checkBoxCreateStoredProcedure.Checked;
            return true;
        }

        public override bool Back()
        {
            return true;
        }

        private void Populate()
        {
            textBoxName.Text = Owner.TheFilter.Name;
            textBoxAlias.Text = Owner.TheFilter.Alias;
            textBoxDescription.Text = Owner.TheFilter.Description;
            radioButtonCollection.Checked = Owner.TheFilter.IsReturnTypeCollection;
            radioButtonSingleItem.Checked = !Owner.TheFilter.IsReturnTypeCollection;
            checkBoxCreateStoredProcedure.Checked = Owner.TheFilter.CreateStoredProcedure;

            switch (Owner.ReturnType)
            {
                case FormFilter2.ReturnTypes.Any:
                    radioButtonCollection.Enabled = true;
                    radioButtonSingleItem.Enabled = true;
                    break;
                case FormFilter2.ReturnTypes.Collection:
                    radioButtonCollection.Checked = true;
                    radioButtonCollection.Enabled = false;
                    radioButtonSingleItem.Enabled = false;
                    break;
                case FormFilter2.ReturnTypes.Single:
                    radioButtonSingleItem.Checked = true;
                    radioButtonCollection.Enabled = false;
                    radioButtonSingleItem.Enabled = false;
                    break;
                default:
                    throw new NotImplementedException("ReturnType not handled yet: " + Owner.ReturnType);
            }
        }


    }
}
