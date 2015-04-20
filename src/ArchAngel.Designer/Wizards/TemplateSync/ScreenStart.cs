using System;
using System.Windows.Forms;
using System.IO;
using ArchAngel.Designer.DesignerProject;

namespace ArchAngel.Designer.Wizards.TemplateSync
{
    public partial class ScreenStart : Interfaces.Controls.ContentItems.ContentItem
    {
        public ScreenStart()
        {
            InitializeComponent();
            PageHeader = "Synchronise Two Templates";
            PageDescription = "Select the template you want to synchronise with.";
            HasNext = true;
            HasPrev = false;
        }

        public override bool Next()
        {
            if (!File.Exists(txtTheirTemplateFile.Text))
            {
				MessageBox.Show(this, "Please enter a valid template file.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (File.Exists(txtTheirTemplateFile.Text))
            {
                dialog.FileName = txtTheirTemplateFile.Text;
            }
            dialog.Filter = "ArchAngel Template (*.stz)|*.stz";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(dialog.FileName))
                {
                    txtTheirTemplateFile.Text = dialog.FileName;
                }
            }
        }

        private void txtTheirTemplateFile_TextChanged(object sender, EventArgs e)
        {
            frmTemplateSyncWizard.Instance.DisableNextButton();
            frmTemplateSyncWizard.TheirProject = new Project(txtTheirTemplateFile.Text);
            frmTemplateSyncWizard.MyProject = (Project)Slyce.Common.Utility.CloneObject(Project.Instance);
            frmTemplateSyncWizard.Instance.EnableNextButton();
        }
    }
}
