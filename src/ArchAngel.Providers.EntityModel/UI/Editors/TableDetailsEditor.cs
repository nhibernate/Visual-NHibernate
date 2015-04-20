using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

namespace ArchAngel.Providers.EntityModel.UI.Editors
{
    public partial class TableDetailsEditor : UserControl
    {
        private Table _Table;

        public TableDetailsEditor()
        {
            InitializeComponent();

            BackColor = Color.Black; // Color.FromArgb(40, 40, 40);
            ForeColor = Color.White;

            textBoxDescription.TextChanged += new EventHandler(textBoxDescription_TextChanged);
            textBoxName.TextChanged += new EventHandler(textBoxName_TextChanged);
        }

        void textBoxName_TextChanged(object sender, EventArgs e)
        {
			var original = Table.Name;
            Table.Name = textBoxName.Text;

			if (original != Table.Name)
				ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
        }

        void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
			var original = Table.Description;
            Table.Description = textBoxDescription.Text;

			if (original != Table.Description)
				ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
        }

        public Table Table
        {
            get { return _Table; }
            set
            {
                _Table = value;
                Populate();
            }
        }

        private void Populate()
        {
            if (Table == null)
                return;

            textBoxDescription.Text = Table.Description;
            textBoxName.Text = Table.Name;
        }

    }
}
