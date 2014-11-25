using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ArchAngel.Designer.Wizards
{
    public partial class frmOutputFileWizard : Form
    {
        internal enum FileTypes
        {
            Script,
            Static,
            Folder
        }

		internal enum SkipFunctionChoice { DontUse = 0, CreateNew, UseExisting }

        private static Type _IterationType;
        private static FileTypes _FileType;
        internal static string FileName;
        internal static string StaticFileName;
        internal static string FunctionName;
		internal static SkipFunctionChoice StaticSkipFunction;
		internal static string StaticSkipFunctionName;
        internal static bool ShowNewFunctionWizardOnClose;
        private readonly List<Interfaces.Controls.ContentItems.ContentItem> ContentItems = new List<Interfaces.Controls.ContentItems.ContentItem>();

        public frmOutputFileWizard()
        {
            InitializeComponent();
            FileName = "";
            ShowNewFunctionWizardOnClose = false;

            ucHeading1.Text = "";
            Controller.ShadeMainForm();
            bool dirtyStatus = Project.Instance.IsDirty;

            if (FileType == FileTypes.Folder)
            {
                ContentItems.Add(new OutputFileWizardScreens.Screen2());
                ContentItems.Add(new OutputFileWizardScreens.Screen3());
                Text = "Folder Name Wizard";
            }
            else if (FileType == FileTypes.Folder)
            {
                ContentItems.Add(new OutputFileWizardScreens.Screen1());
                ContentItems.Add(new OutputFileWizardScreens.Screen2());
                ContentItems.Add(new OutputFileWizardScreens.Screen3());
                Text = "Output-File Wizard";
            }
            else
            {
                ContentItems.Add(new OutputFileWizardScreens.Screen1());
                ContentItems.Add(new OutputFileWizardScreens.Screen2());
                ContentItems.Add(new OutputFileWizardScreens.Screen3());
            	ContentItems.Add(new OutputFileWizardScreens.Screen4());
                Text = "Output-File Wizard";
            }
            LoadScreen(ContentItems[0]);
            Project.Instance.IsDirty = dirtyStatus;
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
            AcceptButton = screen.EnterKeyTriggersNext ? buttonNext : null;
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

        internal static Type IterationType
        {
            get { return _IterationType; }
            set { _IterationType = value; }
        }

        internal static FileTypes FileType
        {
            get { return _FileType; }
            set { _FileType = value; }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            var currentItem = (Interfaces.Controls.ContentItems.ContentItem)panelContent.Controls[0];

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
            var currentItem = (Interfaces.Controls.ContentItems.ContentItem)panelContent.Controls[0];

            if (currentItem.HasFinish)
            {
                if (Save())
                {
                    Project.Instance.IsDirty = true;
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

        private void frmOutputFileWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Controller.UnshadeMainForm();
        }

        private void frmOutputFileWizard_Paint(object sender, PaintEventArgs e)
        {
            BackColor = Slyce.Common.Colors.BackgroundColor;
        }
    }
}