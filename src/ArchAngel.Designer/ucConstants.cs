using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Designer
{
    public partial class ucConstants : UserControl
    {
        public ucConstants()
        {
            InitializeComponent();

            if (Slyce.Common.Utility.InDesignMode) { return; }

            EnableDoubleBuffering();
            ucLabel1.Text = "Constants";
            Populate();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmConstantsDataEntry form = new frmConstantsDataEntry();

            if (form.ShowAddNew())
            {
                Populate();
            }
        }

        private void EnableDoubleBuffering()
        {
            // Set the value of the double-buffering style bits to true.
            this.SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true);
            this.UpdateStyles();
        }

        private void btnRemoveConstant_Click(object sender, EventArgs e)
        {
            if (lstConstants.SelectedItems.Count > 0)
            {
                string constName = lstConstants.SelectedItems[0].SubItems[0].Text;
                string message = string.Format("Delete constant '{0}'?", constName);

                if (MessageBox.Show(message, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Project.Constant[] newConsts = new ArchAngel.Designer.Project.Constant[Project.Instance.Constants.Length - 1];
                    int insertPos = 0;

                    for (int i = 0; i < Project.Instance.Constants.Length; i++)
                    {
                        if (Project.Instance.Constants[i].Name != constName)
                        {
                            newConsts[insertPos] = Project.Instance.Constants[i];
                            insertPos++;
                        }
                    }
                    Project.Instance.Constants = newConsts;
                    Project.Instance.IsDirty = true;
                    Controller.MainForm.PopulateConstantsList();
                    Populate();
                }
            }
        }

        private void lstConstants_DoubleClick(object sender, EventArgs e)
        {
            if (lstConstants.SelectedItems.Count > 0)
            {
                string type = lstConstants.SelectedItems[0].SubItems[1].Text;
                string name = lstConstants.SelectedItems[0].SubItems[0].Text;
                string functionName = type == "Runtime" ? lstConstants.SelectedItems[0].SubItems[2].Text : "";
                string value = type == "Runtime" ? "" : lstConstants.SelectedItems[0].SubItems[2].Text;
                frmConstantsDataEntry form = new frmConstantsDataEntry();

                if (form.ShowEdit(name, type, functionName, value))
                {
                    Populate();
                }
            }
        }

        public void Populate()
        {
            try
            {
                Slyce.Common.Utility.SuspendPainting(lstConstants.Handle);
                lstConstants.Items.Clear();

                foreach (Project.Constant con in Project.Instance.Constants)
                {
                    string[] subItems = new string[4];
                    subItems[0] = con.Name;
                    subItems[1] = con.DataType;
                    subItems[2] = con.Value;
                    subItems[3] = con.IsAssignedAtRuntime ? "Yes" : "";

                    ListViewItem item = new ListViewItem(subItems);
                    lstConstants.Items.Add(item);
                }
            }
            finally
            {
                Slyce.Common.Utility.ResumePainting();
            }
        }

        public void Clear()
        {
            lstConstants.Items.Clear();
        }

        public void SaveConstant()
        {
            Project.Instance.Constants = new Project.Constant[lstConstants.Items.Count];

            for (int i = 0; i < lstConstants.Items.Count; i++)
            {
                Project.Instance.Constants[i] = new ArchAngel.Designer.Project.Constant(lstConstants.Items[i].SubItems[0].Text, lstConstants.Items[i].SubItems[1].Text, lstConstants.Items[i].SubItems[2].Text);//, lstConstants.Items[i].SubItems[3].Text == "Yes");
            }
        }

        private void lstConstants_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstConstants.SelectedItems.Count > 0)
            {
                string type = lstConstants.SelectedItems[0].SubItems[1].Text;
                string name = lstConstants.SelectedItems[0].SubItems[0].Text;
                string functionName = type == "Runtime" ? lstConstants.SelectedItems[0].SubItems[2].Text : "";
                string value = type == "Runtime" ? "" : lstConstants.SelectedItems[0].SubItems[2].Text;
                frmConstantsDataEntry form = new frmConstantsDataEntry();

                if (form.ShowEdit(name, type, functionName, value))
                {
                    Populate();
                }
            }
        }

        private void ucConstants_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
        }


    }
}
