using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Workbench.UserControls
{
    public delegate void AddDelegate(object sender);
    public delegate void UpdateDelegate(object sender);
    public delegate void CancelDelegate(object sender);

    public partial class ItemEdit : UserControl
    {
        public event AddDelegate ButtonAddClicked;
        public event UpdateDelegate ButtonUpdateClicked;
        public event CancelDelegate ButtonCancelClicked;

        public ItemEdit()
        {
            InitializeComponent();
            if (Slyce.Common.Utility.InDesignMode) { return; }
            buttonUpdate.Visible = false;
            buttonCancel.Visible = false;
        }

        public void SetAddMode()
        {
            buttonAdd.Visible = true;

            buttonUpdate.Visible = false;
            buttonCancel.Visible = false;
        }

        public void SetEditMode()
        {
            buttonAdd.Visible = false;

            buttonUpdate.Visible = true;
            buttonCancel.Visible = true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (ButtonAddClicked != null)
            {
                ButtonAddClicked(sender);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            SetAddMode();
            if (ButtonUpdateClicked != null)
            {
                ButtonUpdateClicked(sender);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            SetAddMode();
            if (ButtonCancelClicked != null)
            {
                ButtonCancelClicked(sender);
            }
        }
    }
}
