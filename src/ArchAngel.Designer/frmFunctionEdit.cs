using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Designer
{
    public partial class frmFunctionEdit : Form
    {
        private string m_functionName = "";
        internal Project.FunctionInfo CurrentFunction;
        internal bool IsDeleted = false;
        private bool IsNewFunction = false;

        public frmFunctionEdit(string functionName, bool isNewFunction)
        {
            InitializeComponent();
            IsNewFunction = isNewFunction;
            FunctionName = functionName;
            Controller.ShadeMainForm();
            Populate();
            ucHeading1.Text = ""; if (Slyce.Common.Utility.InDesignMode) { return; }

            EnableDoubleBuffering();
            Project.ScriptLanguageTypes[] scriptLanguages = (Project.ScriptLanguageTypes[])Enum.GetValues(typeof(Project.ScriptLanguageTypes));

            foreach (Project.ScriptLanguageTypes scriptLanguage in scriptLanguages)
            {
                ddlScriptLanguage.Items.Add(Project.ScriptLanguageNameFromEnum(scriptLanguage));
            }
            ddlScriptLanguage.Text = Project.ScriptLanguageNameFromEnum(Project.ScriptLanguageTypes.CSharp);

            if (IsNewFunction)
            {
                txtName.SelectAll();
                txtName.Focus();
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

        public string FunctionName
        {
            get { return m_functionName; }
            set
            {
                Slyce.Common.Utility.CheckForNulls(new object[] { value }, new string[] { "value" });
                m_functionName = value.Trim();
            }
        }

        private void Populate()
        {
            PopulateFunctionHeader();
        }

        public void PopulateFunctionHeader()
        {
            bool dirtyStatus = Project.Instance.IsDirty;
            CurrentFunction = Project.Instance.FindFunction(FunctionName);
            chkTemplateFunction.Checked = CurrentFunction.IsTemplateFunction;
            txtName.Text = CurrentFunction.Name;
            txtDescription.Text = CurrentFunction.Description;

            for (int i = 0; i < CurrentFunction.Parameters.Length; i++)
            {
                Project.ParamInfo param = (Project.ParamInfo)CurrentFunction.Parameters[i];
                ListViewItem item = new ListViewItem(new string[2] { param.Name, param.DataType.Name });
                item.Tag = param.DataType;
                lstParameters.Items.Add(item);
            }
            
            ddlReturnType.Items.Clear();
            ddlReturnType.Text = "";
            ddlReturnType.Items.Clear();

            if (CurrentFunction.IsTemplateFunction)
            {
                Slyce.Common.SyntaxEditorHelper.Languages[] outputLanguages = (Slyce.Common.SyntaxEditorHelper.Languages[])Enum.GetValues(typeof(Slyce.Common.SyntaxEditorHelper.Languages));

                foreach (Slyce.Common.SyntaxEditorHelper.Languages outputLanguage in outputLanguages)
                {
                    ddlReturnType.Items.Add(Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(outputLanguage));
                }
                ddlReturnType.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else
            {
                switch (CurrentFunction.ScriptLanguage)
                {
                    case Project.ScriptLanguageTypes.CSharp:
                        ddlReturnType.Items.AddRange(new string[] { "double", "float", "int", "string", "void" });
                        break;
                    case Project.ScriptLanguageTypes.VbNet:
                        ddlReturnType.Items.AddRange(new string[] { "Double", "Float", "Int", "String", "Void" });
                        break;
                    default:
                        throw new NotImplementedException("Script language type not handled yet: " + CurrentFunction.ScriptLanguage.ToString());
                }
                ddlReturnType.DropDownStyle = ComboBoxStyle.DropDown;
            }
            ddlReturnType.Text = CurrentFunction.IsTemplateFunction ? CurrentFunction.TemplateReturnLanguage : CurrentFunction.ReturnType.Name;
            ddlScriptLanguage.Text = Project.ScriptLanguageNameFromEnum(CurrentFunction.ScriptLanguage);

            #region Fill Categories
            foreach (string category in Project.Instance.FunctionCategories)
            {
                if (ddlCategory.FindStringExact(category) < 0)
                {
                    ddlCategory.Items.Add(category);
                }
            }
            ddlCategory.Sorted = true;
            ddlCategory.Text = CurrentFunction.Category;
            #endregion

            Project.Instance.IsDirty = dirtyStatus;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddParameter();
        }

        private bool AddParameter()
        {
            frmParameterEdit form = new frmParameterEdit(CurrentFunction);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ListViewItem item = new ListViewItem(new string[2] { form.CurrentParameter.Name, form.CurrentParameter.DataType.Name });
                item.Tag = form.CurrentParameter.DataType;
                lstParameters.Items.Add(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                Project.Instance.IsDirty = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnDeleteFunction_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete this function?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Project.Instance.DeleteFunction(CurrentFunction);
                IsDeleted = true;
                this.Close();
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            Project.Instance.IsDirty = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = lstParameters.SelectedIndices.Count - 1; i >= 0; i--)
            {
                lstParameters.Items.RemoveAt(lstParameters.SelectedIndices[i]);
            }
            Project.Instance.IsDirty = true;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            //if (lstParameters.SelectedIndex > 0)
            //{
            //    string selItemText = lstParameters.SelectedItem.ToString();
            //    int index = lstParameters.SelectedIndex;
            //    lstParameters.Items.RemoveAt(lstParameters.SelectedIndex);
            //    lstParameters.Items.Insert(index - 1, selItemText);
            //    lstParameters.SelectedIndex = index - 1;
            //}
            //Project.Instance.IsDirty = true;
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
//            if (lstParameters.SelectedIndex != -1 &&
//lstParameters.SelectedIndex < lstParameters.Items.Count - 1)
//            {
//                string selItemText = lstParameters.SelectedItem.ToString();
//                int index = lstParameters.SelectedIndex;
//                lstParameters.Items.RemoveAt(lstParameters.SelectedIndex);
//                lstParameters.Items.Insert(index + 1, selItemText);
//                lstParameters.SelectedIndex = index + 1;
//                Project.Instance.IsDirty = true;
//            }

        }

        private void chkTemplateFunction_CheckedChanged(object sender, EventArgs e)
        {
            Project.Instance.IsDirty = true;
            ddlReturnType.Items.Clear();

            if (chkTemplateFunction.Checked)
            {
                ddlReturnType.Items.AddRange(new string[] { "C#", "VB.Net", "Plain text", "SQL", "HTML", "CSS", "INI file", "JScript", "Python", "VBScript", "XML" });
                ddlReturnType.DropDownStyle = ComboBoxStyle.DropDownList;

                if (CurrentFunction.IsTemplateFunction)
                {
                    ddlReturnType.Text = CurrentFunction.TemplateReturnLanguage;
                }
            }
            else
            {
                ddlReturnType.Items.AddRange(new string[] { "double", "float", "int", "string", "void" });
                ddlReturnType.DropDownStyle = ComboBoxStyle.DropDown;

                if (!CurrentFunction.IsTemplateFunction)
                {
                    ddlReturnType.Text = CurrentFunction.ReturnType.Name;
                }
            }
            CurrentFunction.IsTemplateFunction = chkTemplateFunction.Checked;
        }

        public bool Save()
        {
            if (ddlCategory.Text.ToLower().IndexOf("default value") == 0)
            {
                MessageBox.Show("'Default Value' is reserved category name and can't be used.", "Invalid Category", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (ddlCategory.Text.ToLower().IndexOf("validate") == 0)
            {
                MessageBox.Show("'Validate' is reserved category name and can't be used.", "Invalid Category", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (txtName.Text == "NewFunction")
            {
                MessageBox.Show("'NewFunction' is not a valid name.", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show("Please enter a function name", "Function name missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtName.Focus();
                return false;
            }
            if (!Slyce.Common.Utility.IsValidFunctionName(txtName.Text))
            {
                MessageBox.Show("The function name has some invalid characters. Only alphabetic, numeric and underscores allowed.", "Invalid Function Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtName.Focus();
                return false;
            }
            if (ddlReturnType.Text.Length == 0)
            {
                MessageBox.Show("Please add a return type.", "No return type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if ((IsNewFunction && Project.Instance.FindFunction(txtName.Text) != null) ||
                (!IsNewFunction && (CurrentFunction.Name != txtName.Text) && Project.Instance.FindFunction(txtName.Text) != null))
            {
                MessageBox.Show("A function with this name already exists. Please choose another name.", "Duplicate function name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            bool nameHasChanged = CurrentFunction.Name != txtName.Text;
            CurrentFunction.Name = txtName.Text;
            CurrentFunction.Description = txtDescription.Text;
            int foundIndex = -1;
            bool mustRefreshFunctionList = IsNewFunction;
            Project.FunctionInfo fi;

            for (int i = 0; i < Project.Instance.Functions.Count; i++)
            {
                if (Project.Instance.Functions[i].Name == CurrentFunction.Name)
                {
                    foundIndex = i;
                }
            }
            string templateReturnLanguage = chkTemplateFunction.Checked ? ddlReturnType.Text : "";
            Type dataType = chkTemplateFunction.Checked ? typeof(string) : Project.Instance.GetTypeFromReferencedAssemblies(ddlReturnType.Text, true);

            if (foundIndex >= 0)
            {
                fi = Project.Instance.Functions[foundIndex];
            }
            else
            {
                mustRefreshFunctionList = true;
                fi = new Project.FunctionInfo(txtName.Text, dataType, "", chkTemplateFunction.Checked, (Project.ScriptLanguageTypes)Enum.Parse(typeof(Project.ScriptLanguageTypes), ddlScriptLanguage.Text), txtDescription.Text, templateReturnLanguage, ddlCategory.Text);
                //int funcCount = Project.Instance.Functions.Count;
                //Project.FunctionInfo[] coll = new Project.FunctionInfo[funcCount + 1];
                //coll[coll.Length - 1] = fi;
                //Array.Copy(Project.Instance.Functions, coll, funcCount);
                //Project.Instance.Functions = coll;
                Project.Instance.Functions.Add(fi);
                foundIndex = Project.Instance.Functions.Count - 1;
            }
            if (!IsNewFunction && fi.Name != txtName.Text)
            {
                // Name has changed
                if (MessageBox.Show("Rename this function throughout the code?", "Name Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Project.Instance.RenameFunctionAll(fi.Name, txtName.Text);
                }
                Project.Instance.SortFunctions();
                mustRefreshFunctionList = true;
            }
            fi.Name = txtName.Text;
            FunctionName = fi.Name;
            string[] parms = new string[lstParameters.Items.Count];
            fi.Parameters = new Project.ParamInfo[lstParameters.Items.Count];
            fi.IsTemplateFunction = chkTemplateFunction.Checked;
            fi.ReturnType = dataType;
            fi.ScriptLanguage = Project.ScriptLanguageEnumFromName(ddlScriptLanguage.Text);
            fi.TemplateReturnLanguage = templateReturnLanguage;

            if (fi.Category != ddlCategory.Text)
            {
                mustRefreshFunctionList = true;
            }
            fi.Category = ddlCategory.Text;

            for (int i = 0; i < lstParameters.Items.Count; i++)
            {
                string name = lstParameters.Items[i].SubItems[0].Text.Trim();
                //string dt = lstParameters.Items[i].SubItems[1].Text.Trim();
                fi.Parameters[i] = new Project.ParamInfo(name, (Type)lstParameters.Items[i].Tag);
            }
            Project.Instance.Functions[foundIndex] = fi;

            if (!IsNewFunction)
            {
                Controller.MainForm.UcFunctions.RenameSelectedTab(fi.Name);
            }
            txtName.Clear();
            lstParameters.Items.Clear();
            txtName.Focus();

            if (nameHasChanged || IsNewFunction)
            {
                Project.Instance.SortFunctions();
            }
            Project.Instance.IsDirty = true;

            if (mustRefreshFunctionList)
            {
                Controller.MainForm.PopulateFunctionList();
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lstParameters_DoubleClick(object sender, EventArgs e)
        {
            string paramName = lstParameters.SelectedItems[0].SubItems[0].Text;
            Project.ParamInfo currentParam = null;

            foreach (Project.ParamInfo p in CurrentFunction.Parameters)
            {
                if (p.Name == paramName)
                {
                    currentParam = new Project.ParamInfo(p.Name, p.DataType);
                    break;
                }
            }
            frmParameterEdit form = new frmParameterEdit(CurrentFunction, currentParam);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ListViewItem item = new ListViewItem(new string[2] { form.CurrentParameter.Name, form.CurrentParameter.DataType.Name });
                lstParameters.Items[lstParameters.SelectedItems[0].Index] = item;
                //lstParameters.Items.Add(item);
                return;
            }
        }

        private void frmFunctionEdit_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
        }

        private void btnAddParameter_Click(object sender, EventArgs e)
        {
            AddParameter();
        }

        private void frmFunctionEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controller.UnshadeMainForm();
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (txtName.Text.ToLower().IndexOf("defaultvalue") == 0)
            {
                //e.Cancel = true;
                MessageBox.Show("Function name cannot start with 'DefaultValue', because it is a reserved word.", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (txtName.Text.ToLower().IndexOf("validate") == 0)
            {
                //e.Cancel = true;
                MessageBox.Show("Function name cannot start with 'Validate', because it is a reserved word.", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


    }
}