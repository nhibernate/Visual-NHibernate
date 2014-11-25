using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Workbench.FilterWizard
{
    public partial class ucFilterStart : ArchAngel.Interfaces.Controls.ContentItems.ContentItem
    {
        ArchAngel.Providers.Database.Controls.FormFilter2 Owner;

        public ucFilterStart(ArchAngel.Providers.Database.Controls.FormFilter2 owner)
        {
            InitializeComponent();
            HasNext = true;
            HasPrev = true;
            Owner = owner;
            PageHeader = "Filter Info";
            PageDescription = "Provide basic information about the filter, such as name.";
            Populate();
            Controller.Instance.OnDataChanged += new ITemplate.DataChangedEventDelegate(Instance_OnDataChanged);
        }

        void Instance_OnDataChanged(Type type, System.Reflection.MethodInfo method, object oldValue, object newValue)
        {
            if (Owner.IsAddingNewFilter &&
                type == typeof(ArchAngel.Providers.Database.Model.Filter) &&
                method.Name == "set_Name")
            {
                textBoxName.Text = (string)newValue;
                textBoxAlias.Text = (string)newValue;
            }
        }

        public override bool Next()
        {
            return true;// FormFilter2.ScreenNames.Summary.ToString();
        }

        public override bool Back()
        {
            return true;//FormFilter2.ScreenNames.WhereClause.ToString();
        }

        private void Populate()
        {
            textBoxName.Text = Owner.Filter.Name;
            textBoxAlias.Text = Owner.Filter.Alias;
            radioButtonCollection.Checked = Owner.Filter.IsReturnTypeCollection;
            radioButtonSingleItem.Checked = !Owner.Filter.IsReturnTypeCollection;
            checkBoxCreateStoredProcedure.Checked = Owner.Filter.CreateStoredProcedure;
        }

        public override bool Save()
        {
            Owner.Filter.Name = textBoxName.Text;
            Owner.Filter.Alias = textBoxAlias.Text;
            Owner.Filter.IsReturnTypeCollection = radioButtonCollection.Checked;
            Owner.Filter.CreateStoredProcedure = checkBoxCreateStoredProcedure.Checked;
            return true;
        }

    }
}
