using System;
using System.Windows.Forms;
using ArchAngel.Designer.DesignerProject;
using Slyce.Common;

namespace ArchAngel.Designer.Wizards.FunctionWizardScreens
{
    public partial class Screen2 : Interfaces.Controls.ContentItems.ContentItem
    {
        public Screen2()
        {
            InitializeComponent();
            PageHeader = "Parameters";
            PageDescription = "Define the parameters and return type.";
            HasNext = true;
            HasPrev = true;
            Populate();
        }

        private void Populate()
        {
            for (int i = 0; i < frmFunctionWizard.CurrentFunction.Parameters.Count; i++)
            {
                ParamInfo param = frmFunctionWizard.CurrentFunction.Parameters[i];
                var item = new ListViewItem(new string[] { param.Name, Utility.GetDemangledGenericTypeName(param.DataType, Project.Instance.Namespaces) });
                item.Tag = param.DataType;
                lstParameters.Items.Add(item);
            }
            ddlReturnType.Items.Clear();
            ddlReturnType.Text = "";
            ddlReturnType.Items.Clear();

            if (frmFunctionWizard.CurrentFunction.IsTemplateFunction)
            {
                var outputLanguages = (TemplateContentLanguage[])Enum.GetValues(typeof(TemplateContentLanguage));

                foreach (TemplateContentLanguage outputLanguage in outputLanguages)
                {
                    ddlReturnType.Items.Add(SyntaxEditorHelper.LanguageNameFromEnum(outputLanguage));
                }
                ddlReturnType.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else
            {
                switch (frmFunctionWizard.CurrentFunction.ScriptLanguage)
                {
                    case SyntaxEditorHelper.ScriptLanguageTypes.CSharp:
                        ddlReturnType.Items.AddRange(new[] { "double", "float", "int", "string", "void" });
                        break;
                    case SyntaxEditorHelper.ScriptLanguageTypes.VbNet:
                        ddlReturnType.Items.AddRange(new[] { "Double", "Float", "Int", "String", "Void" });
                        break;
                    default:
                        throw new NotImplementedException("Script language type not handled yet: " + frmFunctionWizard.CurrentFunction.ScriptLanguage.ToString());
                }
                ddlReturnType.DropDownStyle = ComboBoxStyle.DropDown;
            }
            string returnType = frmFunctionWizard.CurrentFunction.ReturnType != null ? frmFunctionWizard.CurrentFunction.ReturnType.Name : "void";
            ddlReturnType.Text = frmFunctionWizard.CurrentFunction.IsTemplateFunction ? frmFunctionWizard.CurrentFunction.TemplateReturnLanguage : returnType;
        }

        private void ddlReturnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Project.Instance.IsDirty = true;
        }

        private void btnDeleteParameter_Click(object sender, EventArgs e)
        {
            for (int i = lstParameters.SelectedIndices.Count - 1; i >= 0; i--)
            {
                lstParameters.Items.RemoveAt(lstParameters.SelectedIndices[i]);
            }
            Project.Instance.IsDirty = true;
        }

        public override void OnDisplaying()
        {
            ddlReturnType.Items.Clear();

            if (frmFunctionWizard.CurrentFunction.IsTemplateFunction)
            {
                ddlReturnType.Items.AddRange(new[] { "C#", "VB.Net", "Plain text", "SQL", "HTML", "CSS", "INI file", "JScript", "Python", "VBScript", "XML" });
                ddlReturnType.DropDownStyle = ComboBoxStyle.DropDownList;
                ddlReturnType.Text = frmFunctionWizard.CurrentFunction.TemplateReturnLanguage;
            }
            else
            {
                ddlReturnType.Items.AddRange(new[] { "bool", "double", "float", "int", "string", "void" });
                ddlReturnType.DropDownStyle = ComboBoxStyle.DropDown;
                ddlReturnType.Text = frmFunctionWizard.CurrentFunction.ReturnType == null ? "void" : frmFunctionWizard.CurrentFunction.ReturnType.Name;
            }

            base.OnDisplaying();
        }

        public override bool Save()
        {
            if (ddlReturnType.Text.Length == 0)
            {
				MessageBox.Show(this, "Please add a return type.", "No return type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            Type newReturnType;

            if (frmFunctionWizard.CurrentFunction.IsTemplateFunction)
            {
                newReturnType = typeof(string);
            }
            else
            {
                if (Utility.StringsAreEqual(ddlReturnType.Text, "void", false))
                {
                    newReturnType = null;
                }
                else
                {
                    newReturnType = Project.Instance.GetTypeFromReferencedAssemblies(ddlReturnType.Text, true);
                }
            }
            if (frmFunctionWizard.CurrentFunction.ReturnType != newReturnType)
            {
                frmFunctionWizard.MustRefreshFunctionList = true;
                frmFunctionWizard.CurrentFunction.ReturnType = newReturnType;
            }
            if (frmFunctionWizard.CurrentFunction.TemplateReturnLanguage != ddlReturnType.Text)
            {
                frmFunctionWizard.MustRefreshFunctionList = true;
            }
            if (frmFunctionWizard.CurrentFunction.IsTemplateFunction)
            {
                frmFunctionWizard.CurrentFunction.TemplateReturnLanguage = ddlReturnType.Text;
            }
            frmFunctionWizard.CurrentFunction.Parameters.Clear();

            for (int i = 0; i < lstParameters.Items.Count; i++)
            {
                string name = lstParameters.Items[i].SubItems[0].Text.Trim();
                frmFunctionWizard.CurrentFunction.Parameters.Add(new ParamInfo(name, (Type)lstParameters.Items[i].Tag));

				if (i == 0 && frmFunctionWizard.CurrentFunction.IsExtensionMethod)
				{
					frmFunctionWizard.CurrentFunction.Parameters[i].Modifiers = "this ";
				}
            }
            return true;
        }

        public override bool Next()
        {
            if (frmFunctionWizard.TempFunction.IsTemplateFunction)
            {
                frmFunctionWizard.TempFunction.ReturnType = typeof(string);
            }
            else
            {
                if (Utility.StringsAreEqual(ddlReturnType.Text, "void", false))
                {
                    frmFunctionWizard.TempFunction.ReturnType = null;
                }
                else
                {
                    frmFunctionWizard.TempFunction.ReturnType = Project.Instance.GetTypeFromReferencedAssemblies(ddlReturnType.Text, true);
                }
            }
            if (frmFunctionWizard.TempFunction.IsTemplateFunction)
            {
                frmFunctionWizard.TempFunction.TemplateReturnLanguage = ddlReturnType.Text;
            }
            frmFunctionWizard.TempFunction.Parameters.Clear();

            for (int i = 0; i < lstParameters.Items.Count; i++)
            {
                string name = lstParameters.Items[i].SubItems[0].Text.Trim();
                frmFunctionWizard.TempFunction.Parameters.Add(new ParamInfo(name, (Type)lstParameters.Items[i].Tag));

				if (i == 0 && frmFunctionWizard.CurrentFunction.IsExtensionMethod)
				{
					frmFunctionWizard.CurrentFunction.Parameters[i].Modifiers = "this ";
				}
            }
            return true;
        }

        private void lstParameters_DoubleClick(object sender, EventArgs e)
        {
            EditParameter();
        }

		private void btnAddParameter_Click(object sender, EventArgs e)
		{
			AddParameter();
		}

		private bool AddParameter()
        {
            var form = new frmParameterEdit();

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                string demangledTypeName = Utility.GetDemangledGenericTypeName(form.CurrentParameter.DataType, Project.Instance.Namespaces);
                var item = new ListViewItem(new string[2] { form.CurrentParameter.Name, demangledTypeName });
                item.Tag = form.CurrentParameter.DataType;
                lstParameters.Items.Add(item);
                return true;
            }
		    return false;
        }

        private void EditParameter()
        {
            if (lstParameters.SelectedItems.Count > 0)
            {
                string paramName = lstParameters.SelectedItems[0].SubItems[0].Text;
                ParamInfo currentParam = null;

                foreach (ParamInfo p in frmFunctionWizard.CurrentFunction.Parameters)
                {
                    if (p.Name == paramName)
                    {
                        currentParam = new ParamInfo(p.Name, p.DataType);
                        break;
                    }
                }
            	bool canEditType = frmFunctionWizard.CurrentFunction.IsExtensionMethod == false ||
            	                   lstParameters.SelectedItems[0].Index > 0;
				var form = new frmParameterEdit(currentParam, canEditType);

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    var item = new ListViewItem(new[] { form.CurrentParameter.Name, form.CurrentParameter.DataType.Name });
                    item.Tag = form.CurrentParameter.DataType;
                    lstParameters.Items[lstParameters.SelectedItems[0].Index] = item;
                    return;
                }
            }
        }

        private void buttonEditParameter_Click(object sender, EventArgs e)
        {
            EditParameter();
        }

        private void lstParameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstParameters.SelectedIndices.Count > 0 && lstParameters.SelectedIndices[0] == 0 && frmFunctionWizard.CurrentFunction.IsExtensionMethod)
            {
                btnDeleteParameter.Enabled = false;
            }
            else
            {
                btnDeleteParameter.Enabled = true;
            }
        }
    }
}
