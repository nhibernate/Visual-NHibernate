using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Workbench.ContentItems
{
    public partial class FormStaticFiles : Form
    {
        internal List<string> ResourceNames = new List<string>();

        public FormStaticFiles()
        {
            InitializeComponent();
            PopulateStaticFiles();
        }

        private void PopulateStaticFiles()
        {
            if (ArchAngel.Interfaces.SharedData.CurrentProject == null)
                return;

            listViewEx1.Items.Clear();

            foreach (var file in ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.ResourceFiles.OrderBy(f => f))
                listViewEx1.Items.Add(file);

            SetWidthOfForm();
        }

        private void SetWidthOfForm()
        {
            Graphics g = Graphics.FromHwnd(listViewEx1.Handle);

            float maxWidth = 0;

            foreach (ListViewItem item in listViewEx1.Items)
                maxWidth = Math.Max(maxWidth, g.MeasureString(item.Text, listViewEx1.Font).Width);

            this.Width = (int)maxWidth + listViewEx1.Right + (this.Width - listViewEx1.Right) + 5;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewEx1.SelectedItems)
                ResourceNames.Add(item.Text);

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            ResourceNames.Clear();
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        
    }
}
