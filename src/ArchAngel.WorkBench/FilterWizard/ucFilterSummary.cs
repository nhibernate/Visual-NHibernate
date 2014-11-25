using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Workbench.FilterWizard
{
    public partial class ucFilterSummary : ArchAngel.Interfaces.Controls.ContentItems.ContentItem
    {
        ArchAngel.Providers.Database.Controls.FormFilter2 Owner;

        public ucFilterSummary(ArchAngel.Providers.Database.Controls.FormFilter2 owner)
        {
            InitializeComponent();
            HasNext = false;
            HasPrev = true;
            HasFinish = true;
            PageHeader = "Filter Summary";
            PageDescription = "Check that all the information is correct.";
            Owner = owner;
        }

        public override bool Back()
        {
            return true;//FormFilter2.ScreenNames.Start.ToString();
        }
    }
}
