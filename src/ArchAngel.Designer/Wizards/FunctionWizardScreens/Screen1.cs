using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Designer.DesignerProject;

namespace ArchAngel.Designer.Wizards.FunctionWizardScreens
{
    public partial class Screen1 : Interfaces.Controls.ContentItems.ContentItem
    {
        public Screen1()
        {
            InitializeComponent();
            PageHeader = "General";
            PageDescription = "Define the general details of the function.";
            HasNext = true;
            HasPrev = false;
            Populate();
        }

        private void Populate()
        {
            if (frmFunctionWizard.CurrentFunction.IsTemplateFunction)
            {
                optIsTemplateFunction.Checked = true;
            }
            else
            {
                optNotTemplateFunction.Checked = true;
            }
            //chkTemplateFunction.Checked = frmFunctionWizard.CurrentFunction.IsTemplateFunction;
            txtName.Text = frmFunctionWizard.CurrentFunction.Name;
            txtDescription.Text = frmFunctionWizard.CurrentFunction.Description;
            ddlScriptLanguage.Text = ProjectEnumHelper.ScriptLanguageNameFromEnum(frmFunctionWizard.CurrentFunction.ScriptLanguage);

            if (frmFunctionWizard.CurrentFunction.IsExtensionMethod)
            {
                buttonDelete.Enabled = false;
            }

            #region Fill Categories
            foreach (string category in Project.Instance.FunctionCategories)
            {
                if (ddlCategory.FindStringExact(category) < 0)
                {
                    ddlCategory.Items.Add(category);
                }
            }
            ddlCategory.Sorted = true;
            ddlCategory.Text = frmFunctionWizard.CurrentFunction.Category;
            #endregion
        }

        public override void OnDisplaying()
        {
            if (frmFunctionWizard.IsNewFunction)
            {
                txtName.SelectAll();
                txtName.Focus();
            }
			if(frmFunctionWizard.CurrentFunction.IsExtensionMethod)
			{
				label3.Enabled = false;
				ddlCategory.Enabled = false;
			}
			else
			{
				label3.Enabled = true;
				ddlCategory.Enabled = true;
			}
            base.OnDisplaying();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            Project.Instance.IsDirty = true;
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            Project.Instance.IsDirty = true;
        }

        private void ddlCategory_TextChanged(object sender, EventArgs e)
        {
            Project.Instance.IsDirty = true;
        }

        private void optIsTemplateFunction_CheckedChanged(object sender, EventArgs e)
        {
            frmFunctionWizard.CurrentFunction.IsTemplateFunction = optIsTemplateFunction.Checked;
            Project.Instance.IsDirty = true;
        }

        private void optNotTemplateFunction_CheckedChanged(object sender, EventArgs e)
        {
            frmFunctionWizard.CurrentFunction.IsTemplateFunction = optIsTemplateFunction.Checked;
            Project.Instance.IsDirty = true;
        }
        public override bool Save()
        {
            string newCategoryName = ddlCategory.Text.ToLower();

            if ((frmFunctionWizard.IsNewFunction && newCategoryName.IndexOf("default value") == 0) ||
                (!frmFunctionWizard.IsNewFunction && frmFunctionWizard.CurrentFunction.Category.ToLower().IndexOf("default value") != 0 && newCategoryName.IndexOf("default value") == 0))
            {
				MessageBox.Show(this, "'Default Value' is reserved category name and can't be used.", "Invalid Category", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if ((frmFunctionWizard.IsNewFunction && newCategoryName.IndexOf("validate") == 0) ||
                (!frmFunctionWizard.IsNewFunction && frmFunctionWizard.CurrentFunction.Category.ToLower().IndexOf("validate") != 0 && newCategoryName.IndexOf("validate") == 0))
            {
				MessageBox.Show(this, "'Validate' is reserved category name and can't be used.", "Invalid Category", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (txtName.Text == "NewFunction")
            {
				MessageBox.Show(this, "'NewFunction' is not a valid name.", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (txtName.Text.Length == 0)
            {
				MessageBox.Show(this, "Please enter a function name", "Function name missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtName.Focus();
                return false;
            }
            if (!Slyce.Common.Utility.IsValidFunctionName(txtName.Text))
            {
				MessageBox.Show(this, "The function name has some invalid characters. Only alphabetic, numeric and underscores allowed.", "Invalid Function Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtName.Focus();
                return false;
            }
            List<FunctionInfo> duplicateFunctions = Project.Instance.FindFunctions(txtName.Text);

            // Remove this function from the collection
            for (int i = 0; i < duplicateFunctions.Count; i++)
            {
                if (duplicateFunctions[i] == frmFunctionWizard.CurrentFunction)
                {
                    duplicateFunctions.RemoveAt(i);
                }
            }
            if (duplicateFunctions.Count > 0)
            {
				MessageBox.Show(this, "A function with this name already exists. Please choose another name.", "Duplicate function name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (!frmFunctionWizard.IsNewFunction && frmFunctionWizard.CurrentFunction.Name != txtName.Text)
            {
                // Name has changed
				if (MessageBox.Show(this, "Rename this function throughout the code?", "Name Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Project.Instance.RenameFunctionAll(frmFunctionWizard.CurrentFunction.Name, txtName.Text);
                }
                Project.Instance.SortFunctions();
                frmFunctionWizard.MustRefreshFunctionList = true;
            }
            if (frmFunctionWizard.CurrentFunction.Category != ddlCategory.Text)
            {
                frmFunctionWizard.MustRefreshFunctionList = true;
            }
            frmFunctionWizard.CurrentFunction.Name = txtName.Text;
            frmFunctionWizard.CurrentFunction.Description = txtDescription.Text;
            frmFunctionWizard.CurrentFunction.Category = ddlCategory.Text;
            frmFunctionWizard.CurrentFunction.ScriptLanguage = ProjectEnumHelper.ScriptLanguageEnumFromName(ddlScriptLanguage.Text);

            return true;
        }

        public override bool Next()
        {
            frmFunctionWizard.TempFunction.Name = txtName.Text;
            frmFunctionWizard.TempFunction.Description = txtDescription.Text;
            frmFunctionWizard.TempFunction.Category = ddlCategory.Text;
            frmFunctionWizard.TempFunction.ScriptLanguage = ProjectEnumHelper.ScriptLanguageEnumFromName(ddlScriptLanguage.Text);
            frmFunctionWizard.TempFunction.IsTemplateFunction = optIsTemplateFunction.Checked;
            return true;
        }

        private void labelIsTemplateFunction_Click(object sender, EventArgs e)
        {
            optIsTemplateFunction.Checked = true;
        }

        private void labelNotTemplateFunction_Click(object sender, EventArgs e)
        {
            optNotTemplateFunction.Checked = true;
        }

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "Delete this function?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Project.Instance.DeleteFunction(frmFunctionWizard.CurrentFunction);
				frmFunctionWizard.IsDeleted = true;
				frmFunctionWizard.Instance.Close();
			}
		}
    }
}
