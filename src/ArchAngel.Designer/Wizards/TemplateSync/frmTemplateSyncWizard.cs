using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Designer.DesignerProject;

namespace ArchAngel.Designer.Wizards.TemplateSync
{
    public partial class frmTemplateSyncWizard : Form
    {
        private readonly List<Interfaces.Controls.ContentItems.ContentItem> ContentItems = new List<Interfaces.Controls.ContentItems.ContentItem>();
        internal static Project TheirProject;
        internal static Project MyProject;
        internal static frmTemplateSyncWizard Instance;
    	private bool _RemoveCurrentScreen;

        public frmTemplateSyncWizard()
        {
            InitializeComponent();
            Instance = this;
            ucHeading1.Text = "";
            BackColor = Slyce.Common.Colors.BackgroundColor;

            Controller.ShadeMainForm();
            bool dirtyStatus = Project.Instance.IsDirty;
            ContentItems.Add(new ScreenStart());
            ContentItems.Add(new ScreenProjectDetails());
            ContentItems.Add(new ScreenFileLayouts());
            ContentItems.Add(new ScreenFunctions());
            ContentItems.Add(new ScreenSummary());
            LoadScreen(ContentItems[0]);
            Project.Instance.IsDirty = dirtyStatus;
        }

        internal void EnableNextButton()
        {
            buttonNext.Enabled = true;
        }

        internal void DisableNextButton()
        {
            buttonNext.Enabled = false;
        }

        internal void RemoveCurrentScreen()
        {
            _RemoveCurrentScreen = true;
        }

        private void LoadScreen(Interfaces.Controls.ContentItems.ContentItem screen)
        {
            Cursor = Cursors.WaitCursor;
            screen.Dock = DockStyle.Fill;
            screen.OnDisplaying();
            panelContent.Controls.Clear();
            panelContent.Controls.Add(screen);
            panelTop.Visible = screen.DisplayTopPanel;
            buttonBack.Visible = screen.HasPrev;
            buttonBack.BringToFront();
            buttonNext.Enabled = screen.HasNext || screen.HasFinish;
            buttonBack.Enabled = screen.HasPrev;
            labelPageHeader.Text = screen.PageHeader;
            labelPageDescription.Text = screen.PageDescription;
            buttonNext.Text = screen.HasFinish ? "&Finish" : "&Next >";

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

        private void frmTemplateSyncWizard_Paint(object sender, PaintEventArgs e)
        {
            BackColor = Slyce.Common.Colors.BackgroundColor;
        }

        private void frmTemplateSyncWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Controller.UnshadeMainForm();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
        	_RemoveCurrentScreen = false;
            var currentItem = (Interfaces.Controls.ContentItems.ContentItem)panelContent.Controls[0];

            if (!currentItem.HasPrev || !currentItem.Back())
            {
                return;
            }

            for (int i = 0; i < ContentItems.Count; i++)
            {
                if (i > 0 && ContentItems[i] == currentItem)
                {
                    LoadScreen(ContentItems[i - 1]);

                    while (_RemoveCurrentScreen && i > 0)
                    {
                        i--;
                        _RemoveCurrentScreen = false;
                        LoadScreen(ContentItems[i - 1]);
                    }
                    break;
                }
            }
        }

        internal void buttonNext_Click(object sender, EventArgs e)
        {
        	_RemoveCurrentScreen = false;
            var currentItem = (Interfaces.Controls.ContentItems.ContentItem)panelContent.Controls[0];

            if (!currentItem.HasNext || !currentItem.Next())
            {
                return;
            }
			if (currentItem.HasFinish)
			{
				Close();
				return;
			}
            for (int i = 0; i < ContentItems.Count; i++)
            {
                if (ContentItems[i] == currentItem)
                {
                    LoadScreen(ContentItems[i + 1]);

                    while (_RemoveCurrentScreen && i < ContentItems.Count - 1)
                    {
                        i++;
                        _RemoveCurrentScreen = false;
                        LoadScreen(ContentItems[i + 1]);
                    }
                    break;
                }
            }
        }
    }
}