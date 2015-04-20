using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Designer.DesignerProject;

namespace ArchAngel.Designer.Wizards
{
    public partial class frmFunctionWizard : Form
    {
        private readonly List<Interfaces.Controls.ContentItems.ContentItem> ContentItems = new List<Interfaces.Controls.ContentItems.ContentItem>();
        public static FunctionInfo CurrentFunction;
        internal static FunctionInfo TempFunction;
        internal static bool IsDeleted;
        internal static bool IsNewFunction;
        internal static bool MustRefreshFunctionList;
		internal static frmFunctionWizard Instance;

        public frmFunctionWizard(FunctionInfo function, bool isNewFunction)
        {
            InitializeComponent();
			Instance = this;
            ucHeading1.Text = "";
            BackColor = Slyce.Common.Colors.BackgroundColor;

            IsDeleted = false;
            MustRefreshFunctionList = false;
            IsNewFunction = isNewFunction;
            CurrentFunction = function;
            // Clone the function.
			TempFunction =  new FunctionInfo(CurrentFunction);
            Controller.ShadeMainForm();
            bool dirtyStatus = Project.Instance.IsDirty;
            CurrentFunction = function;
            ContentItems.Add(new FunctionWizardScreens.Screen1());
            ContentItems.Add(new FunctionWizardScreens.Screen2());
            ContentItems.Add(new FunctionWizardScreens.Screen3());
            LoadScreen(ContentItems[0]);
            Project.Instance.IsDirty = dirtyStatus;
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

        private void button2_Click(object sender, EventArgs e)
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
            if (!currentItem.HasNext || !currentItem.Next())
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
            if (CurrentFunction.Body == "SAMPLE")
            {
                if (CurrentFunction.IsTemplateFunction)
                {
                    var sb = new StringBuilder(500);
                    sb.AppendLine("Sample");
                    sb.AppendLine("This is plain text. You can execute code like this: Today's date is <%=DateTime.Now.ToString(\"dd MM yyyy\");%> or <% Write(DateTime.Now.ToString(\"dd MM yyyy\"));%>");
                    sb.AppendLine("List numbers from 0 to 9:<%");
                    sb.AppendLine("for (int i = 0; i < 10; i++)");
                    sb.AppendLine("{");
                    sb.AppendLine("\tif (i > 0) {Write(\", \");}");
                    sb.AppendLine("\tWrite(i.ToString());");
                    sb.AppendLine("}");
                    sb.AppendLine("%>");
                    CurrentFunction.Body = sb.ToString();
                }
                else
                {
                    CurrentFunction.Body = "// Add your C# code here...";
                }
            }
            if (IsNewFunction)
            {
                MustRefreshFunctionList = true;
            }
            if (!IsNewFunction)
            {
                Controller.Instance.MainForm.UcFunctions.RenameSelectedTab(CurrentFunction.Name);
            }
            Project.Instance.SortFunctions();
            Project.Instance.IsDirty = true;

            if (MustRefreshFunctionList)
            {
                Controller.Instance.MainForm.UcFunctions.PopulateFunctionList();
            }
            return true;
        }

        private void frmFunctionWizard_Paint(object sender, PaintEventArgs e)
        {
            BackColor = Slyce.Common.Colors.BackgroundColor;
        }

        private void frmFunctionWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Controller.UnshadeMainForm();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}