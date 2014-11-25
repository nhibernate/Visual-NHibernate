namespace ArchAngel.Designer.Wizards.OutputFileWizardScreens
{
    public partial class Screen1 : Interfaces.Controls.ContentItems.ContentItem
    {
        public Screen1()
        {
            InitializeComponent();
            PageHeader = "File Type";
            PageDescription = "Select the type of file.";
            HasNext = true;
            HasPrev = false;
        }

        public override void OnDisplaying()
        {
            if (frmOutputFileWizard.FileType == frmOutputFileWizard.FileTypes.Script)
            {
                optScriptFile.Checked = true;
            }
            else
            {
                optBinaryFile.Checked = true;
            }
            base.OnDisplaying();
        }

        public override bool Next()
        {
            if (optScriptFile.Checked)
            {
                frmOutputFileWizard.FileType = frmOutputFileWizard.FileTypes.Script;
            }
            else
            {
                frmOutputFileWizard.FileType = frmOutputFileWizard.FileTypes.Static;
                frmOutputFileWizard.ShowNewFunctionWizardOnClose = false;
                frmOutputFileWizard.FunctionName = "";
            }
            return true;
        }
    }
}
