using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ArchAngel.Providers.Database.Controls.AssociationWizard
{
    public partial class frmAssociationWizard : Form
    {
        private readonly List<Interfaces.Controls.ContentItems.ContentItem> ContentItems = new List<Interfaces.Controls.ContentItems.ContentItem>();
        internal static Model.Association Association;

        public frmAssociationWizard()
        {
            InitializeComponent();
            ucHeading1.Text = "";
            Interfaces.Events.ShadeMainForm();
            ContentItems.Add(new Screen1());
            ContentItems.Add(new Screen2());
            Text = "Association Wizard";
            LoadScreen(ContentItems[0]);
            //Project.Instance.IsDirty = dirtyStatus;
            btnCancel.Top = -100;
            btnCancel.Left = -100;
        }

        private void LoadScreen(Interfaces.Controls.ContentItems.ContentItem screen)
        {
            Cursor = Cursors.WaitCursor;
            panelContent.Controls.Clear();
            panelContent.Controls.Add(screen);
            screen.Dock = DockStyle.Fill;
            panelTop.Visible = screen.DisplayTopPanel;
            buttonBack.Visible = screen.HasPrev;
            buttonBack.BringToFront();
            buttonNext.Enabled = screen.HasNext || screen.HasFinish;
            buttonBack.Enabled = screen.HasPrev;
            labelPageHeader.Text = screen.PageHeader;
            labelPageDescription.Text = screen.PageDescription;
            buttonNext.Text = screen.HasFinish ? "&Finish" : "&Next >";
            screen.OnDisplaying();

            if (!string.IsNullOrEmpty(screen.NextText))
            {
                buttonNext.Text = screen.NextText;
            }
            else
            {
                buttonNext.Text = "&Next >";
            }
            Cursor = Cursors.Default;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Interfaces.Controls.ContentItems.ContentItem currentItem = (Interfaces.Controls.ContentItems.ContentItem)panelContent.Controls[0];

            if (!currentItem.HasPrev || !currentItem.Back())
            {
                return;
            }

            for (int i = 0; i < ContentItems.Count; i++)
            {
                if (ContentItems[i] == currentItem)
                {
                    LoadScreen(ContentItems[i - 1]);
                }
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            Interfaces.Controls.ContentItems.ContentItem currentItem = (Interfaces.Controls.ContentItems.ContentItem)panelContent.Controls[0];

            if (currentItem.HasFinish)
            {
                if (Save())
                {
                    //Project.Instance.IsDirty = true;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                return;
            }
            if (currentItem.HasFinish || !currentItem.HasNext || !currentItem.Next())
            {
                return;
            }
            for (int i = 0; i < ContentItems.Count; i++)
            {
                if (ContentItems[i] == currentItem)
                {
                    LoadScreen(ContentItems[i + 1]);
                }
            }
        }

        public bool Save()
        {
            foreach (ArchAngel.Interfaces.Controls.ContentItems.ContentItem screen in ContentItems)
            {
                if (!screen.Save())
                {
                    return false;
                }
            }
            return true;
        }

        private void frmAssociationWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Interfaces.Events.UnShadeMainForm();
        }

        private void frmAssociationWizard_Paint(object sender, PaintEventArgs e)
        {
            BackColor = Slyce.Common.Colors.BackgroundColor;
        }


    }
}