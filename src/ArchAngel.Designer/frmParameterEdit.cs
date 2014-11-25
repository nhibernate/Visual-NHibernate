using System;
using System.Windows.Forms;
using ArchAngel.Designer.DesignerProject;

namespace ArchAngel.Designer
{
    public partial class frmParameterEdit : Form
    {
    	internal ParamInfo CurrentParameter;
    	private readonly bool canEditType = true;

        public frmParameterEdit()
        {
            InitializeComponent();
            Controller.ShadeMainForm();
            this.Text = "New Parameter";
        	Populate();
        }

        public frmParameterEdit(ParamInfo parameter, bool canEditType)
        {
            InitializeComponent();
            Controller.ShadeMainForm();
        	CurrentParameter = parameter;
        	this.canEditType = canEditType;
            Populate();
        }

        private void Populate()
        {
            ucHeading1.Text = "";
            PopulateParameterTypes();

            if (CurrentParameter != null)
            {
                txtParamName.Text = CurrentParameter.Name;
                ddlParamType.Text = Slyce.Common.Utility.GetDemangledGenericTypeName(CurrentParameter.DataType, Project.Instance.Namespaces);
            }

			if(canEditType == false)
			{
				ddlParamType.Enabled = false;
			}
        }

        private void PopulateParameterTypes()
        {
            ddlParamType.Items.Clear();

            foreach (Type t in Project.Instance.AllowedScriptParameters)
            {
                ddlParamType.Items.Add(t.FullName);
            }
            ddlParamType.Sorted = true;
            ddlParamType.Sorted = false;
            //if (!CurrentFunction.IsTemplateFunction)
            //{
                ddlParamType.Items.Insert(0, "DateTime");
                ddlParamType.Items.Insert(0, "double");
                ddlParamType.Items.Insert(0, "bool");
                ddlParamType.Items.Insert(0, "int");
                ddlParamType.Items.Insert(0, "string");
                ddlParamType.DropDownStyle = ComboBoxStyle.DropDown; // Allow user to enter their own types
                //toolTip1.SetToolTip(ddlParamType, "This is not a template function, so you can add parameters of any data-type.");
            //}
            //else
            //{
            //    ddlParamType.DropDownStyle = ComboBoxStyle.DropDownList; // Don't allow user to enter their own types.
            //    toolTip1.SetToolTip(ddlParamType, "This is a template function, so you can only add parameters of types specified in the API.");
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ddlParamType.Text.Length == 0 && canEditType)
            {
				MessageBox.Show(this, "No type selected for the parameter", "Missing type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Type parameterType = null;

            try
            {
                parameterType = Project.Instance.GetTypeFromReferencedAssemblies(ddlParamType.Text, false);
            }
            catch
            {
                // Do nothing, because we check for null below
            }
            if (parameterType == null && canEditType)
            {
				MessageBox.Show(this, "The data-type cannot be found in the referenced assemblies. Try using a fully qualified type-name.", "Unknown Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (CurrentParameter == null)
            {
                CurrentParameter = new ParamInfo(txtParamName.Text, parameterType);
            }
            else
            {
                CurrentParameter.Name = txtParamName.Text;
                if(canEditType) CurrentParameter.DataType = parameterType;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ddlParamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtParamName.Text) && !string.IsNullOrEmpty(ddlParamType.Text))
            {
                string typeName = ddlParamType.Text;

                if (typeName.LastIndexOf(".") >= 0)
                {
                    typeName = typeName.Substring(typeName.LastIndexOf(".") + 1);

                    if (typeName.Length > 1)
                    {
                        typeName = typeName[0].ToString().ToLower() + typeName.Substring(1);
                        txtParamName.Text = typeName;
                    }
                }
            }
        }

        private void frmParameterEdit_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
        }

        private void frmParameterEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controller.UnshadeMainForm();
        }

    }
}