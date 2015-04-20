using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Interfaces.Controls.ContentItems;

namespace ArchAngel.Providers.EntityModel.UI
{
    public partial class CustomCode : ContentItem
    {
        EntitySet EntitySet;

        public CustomCode()
        {
            InitializeComponent();

            Name = "Custom Code";
            Title = "Custom Code";
            NavBarIcon = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.Resources.doc_arrow_right_green_16.png"));
            //PageDescription = "This screen is where you pull your database tables in, and define the entities that they map to.";
        }

        public void FillData(EntitySet entitySet)
        {
            EntitySet = entitySet;
            entities1.FillData(EntitySet);
        }

        private void Populate()
        {
            
        }
    }
}
