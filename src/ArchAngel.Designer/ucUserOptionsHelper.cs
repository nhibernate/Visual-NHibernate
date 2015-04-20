using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace ArchAngel.Designer
{
    public partial class ucUserOptionsHelper : UserControl
    {
        private enum Images
        {
            Class = 0,
            Property = 1,
            Method = 2
        }
        private Color CurrentBaseColor = Color.Empty;

        public ucUserOptionsHelper()
        {
            InitializeComponent();
            if (Slyce.Common.Utility.InDesignMode) { return; }

            //EnableDoubleBuffering();
            ucLabel1.Text = "API Extensions - Helper Functions";
            Populate();
        }

        public void Clear()
        {
            treeListAPIHelper.ClearNodes();
        }

        public void Populate()
        {
            treeListAPIHelper.ClearNodes();
            System.Reflection.Assembly modelAssembly = System.Reflection.Assembly.GetAssembly(typeof(ArchAngel.Providers.Database.Model.Database));
            AddAssemblyToHelperTree(modelAssembly, "ArchAngel.Providers.Database.Helper", treeListAPIHelper);
            treeListAPIHelper.ExpandAll();
        }

        private void AddAssemblyToHelperTree(System.Reflection.Assembly assembly, string allowedNamespace, TreeList treelist)
        {
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                if (type.FullName.IndexOf(allowedNamespace) != 0)
                {
                    continue;
                }
                ArchAngel.Interfaces.Attributes.ArchAngelEditorAttribute[] attributes = (ArchAngel.Interfaces.Attributes.ArchAngelEditorAttribute[])type.GetCustomAttributes(typeof(ArchAngel.Interfaces.Attributes.ArchAngelEditorAttribute), true);
                List<string> validPropertyNames = new List<string>();

                foreach (ArchAngel.Interfaces.Attributes.ArchAngelEditorAttribute attribute in attributes)
                {
                    if (attribute.PropertiesWithDefaultValueFunctionality.Length == 0)
                    {
                        continue;
                    }
                    string[] names = attribute.PropertiesWithDefaultValueFunctionality.Split(',');

                    foreach (string name in names)
                    {
                        if (validPropertyNames.BinarySearch(name) < 0)
                        {
                            validPropertyNames.Add(name);
                            validPropertyNames.Sort();
                        }
                    }
                }
                if (type != typeof(ArchAngel.Providers.Database.Model.Database) &&
                    (attributes.Length == 0 ||
                    !attributes[0].VirtualPropertiesAllowed ||
                    validPropertyNames.Count == 0))
                {
                    continue;
                }
                TreeListNode objectNode = treelist.AppendNode(new object[] { type.Name, "", "", "" }, null);
                objectNode.Tag = type;
                objectNode.ImageIndex = objectNode.SelectImageIndex = (int)Images.Class;

                foreach (string propertyName in validPropertyNames)
                {
                    System.Reflection.MethodInfo method = type.GetMethod(propertyName);
                    string overrideName = type.FullName.Replace("ArchAngel.Providers.Database.", "") + "." + method.Name;
                    overrideName = overrideName.Replace(".", "_");
                    TreeListNode propertyNode = AddTreeListNode(treelist, objectNode, method.ReturnType, method.Name, overrideName, "", Images.Method);
                }
            }
        }

        private TreeListNode AddTreeListNode(TreeList treelist, TreeListNode node, Type type, string propertyName, string defaultFunction, string validationFunction, Images image)
        {
            TreeListNode newNode = treelist.AppendNode(new object[] { propertyName, defaultFunction, validationFunction }, node);
            newNode.ImageIndex = newNode.SelectImageIndex = (int)image;
            newNode.Tag = type;
            return newNode;
        }

        private void mnuHelperItemViewFunction_Click(object sender, EventArgs e)
        {
            Controller.MainForm.Cursor = Cursors.WaitCursor;
            TreeListNode selectedNode = treeListAPIHelper.Selection[0];
            Type objectType = (Type)selectedNode.ParentNode.Tag;
            string methodName = selectedNode[0].ToString();
            string functionName = objectType.FullName.Replace("ArchAngel.Providers.Database.", "") + "." + methodName;
            functionName = functionName.Replace(".", "_");

            Project.FunctionInfo function = Project.Instance.FindFunction(functionName);

            if (function == null)
            {
                System.Reflection.MethodInfo method = objectType.GetMethod(methodName);
                ArchAngel.Interfaces.Attributes.ApiExtensionAttribute[] attributes = (ArchAngel.Interfaces.Attributes.ApiExtensionAttribute[])method.GetCustomAttributes(typeof(ArchAngel.Interfaces.Attributes.ApiExtensionAttribute), true);

                if (attributes.Length == 0)
                {
                    throw new NotImplementedException(string.Format("DefaultCodeAttribute not implemented for {0}.{1} yet.", objectType.FullName, methodName));
                }
                Type returnType = method.ReturnType;

                function = new Project.FunctionInfo(functionName, returnType, "XXX", false, Project.ScriptLanguageTypes.CSharp, string.Format("Override of helper function: {0}.{1}.", objectType.Name, methodName), "XXX", "Helper Overrides");
                System.Reflection.ParameterInfo[] parameters = method.GetParameters();

                foreach (System.Reflection.ParameterInfo parameter in parameters)
                {
                    ArchAngel.Designer.Project.ParamInfo par = new Project.ParamInfo(parameter.Name, parameter.ParameterType);
                    function.AddParameter(par);
                }
                Project.Instance.AddFunction(function);
                //int funcCount = Project.Instance.Functions.Length;
                //Project.FunctionInfo[] coll = new Project.FunctionInfo[funcCount + 1];
                //coll[coll.Length - 1] = function;
                //Array.Copy(Project.Instance.Functions, coll, funcCount);
                //Project.Instance.Functions = coll;
            }
            Project.DefaultValueFunction defValFunc = Project.Instance.FindDefaultValueFunction(functionName);

            if (defValFunc == null)
            {
                Project.ParamInfo[] parameterTypes = new Project.ParamInfo[function.Parameters.Length];

                for (int i = 0; i < function.Parameters.Length; i++)
                {
                    parameterTypes[i] = new Project.ParamInfo(Slyce.Common.Utility.GetCamelCase(function.Parameters[i].Name), function.Parameters[i].DataType);
                }
                defValFunc = new Project.DefaultValueFunction(objectType, methodName, false, Project.DefaultValueFunction.FunctionTypes.HelperOverride, false);
                defValFunc.ParameterTypes = parameterTypes;
                Project.Instance.DefaultValueFunctions.Add(defValFunc);
                function.Body = defValFunc.GetFormattedDefaultCode();
                Project.Instance.IsDirty = true;
            }
            Controller.MainForm.ShowFunction(functionName, defValFunc, false, this);
            Controller.MainForm.Cursor = Cursors.Default;
        }

        private void treeListAPIHelper_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraTreeList.TreeListHitInfo hInfo = treeListAPIHelper.CalcHitInfo(new Point(e.X, e.Y));
            TreeListNode node = hInfo.Node;

            if (node == null)
            {
                return;
            }
            // Toggle Checkbox
            if (e.Button == MouseButtons.Right)
            {
                treeListAPIHelper.FocusedNode = node;
                treeListAPIHelper.FocusedColumn = hInfo.Column;
                //treeListAPI.InvalidateNodes();
            }

        }

        private void treeListAPIHelper_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            bool isFocusedNode = e.Node.TreeList.FocusedNode == e.Node;
            bool hasActualDefaultFunction = false;
            FontStyle fontStyle;

            switch (e.Column.AbsoluteIndex)
            {
                case 0:
                    fontStyle = FontStyle.Regular;
                    break;
                case 1:
                    string val = (string)e.Node.GetValue(1);

                    if (Project.Instance.FindDefaultValueFunction(val) != null || Project.Instance.FindFunction(val) != null)
                    {
                        hasActualDefaultFunction = true;
                        fontStyle = FontStyle.Italic | FontStyle.Bold;
                        e.Appearance.BackColor = Slyce.Common.Colors.ChangeBrightness(Slyce.Common.Colors.BackgroundColor, 0.9);
                        SetFocusedNodeProperties(e, false);
                    }
                    else
                    {
                        fontStyle = FontStyle.Italic;
                    }
                    break;
                default:
                    throw new NotImplementedException("Not coded yet.");
            }
            if (hasActualDefaultFunction)
            {
                //e.Appearance.BackColor = Slyce.Common.Colors.ChangeBrightness(Slyce.Common.Colors.BackgroundColor, 0.9); //Slyce.Common.Colors.FadingTitleLightColor;// Color.AliceBlue;
                if (e.Node == treeListAPIHelper.FocusedNode && e.Column.AbsoluteIndex == treeListAPIHelper.FocusedColumn.AbsoluteIndex)
                {
                    e.Appearance.BackColor = Slyce.Common.Colors.ChangeBrightness(Slyce.Common.Colors.BackgroundColor, 0.9);
                }
            }
            //e.Appearance.BackColor = Slyce.Common.Colors.ChangeBrightness(Slyce.Common.Colors.BackgroundColor, 0.9); //Slyce.Common.Colors.FadingTitleLightColor;// Color.AliceBlue;
            e.Appearance.Font = new Font(e.Appearance.Font, fontStyle);

            if (e.Node.Focused && e.Column.AbsoluteIndex == treeListAPIHelper.FocusedColumn.AbsoluteIndex)
            {
                if (e.Column.AbsoluteIndex != 0)
                {
                    SetFocusedNodeProperties(e, true);
                }
            }
        }

        private void SetFocusedNodeProperties(GetCustomNodeCellStyleEventArgs e, bool isFocused)
        {
            if (isFocused)
            {
                e.Appearance.BackColor = Color.Red;
            }
            double brightness = Slyce.Common.Colors.GetBrightness(e.Appearance.BackColor);
            double lightBrightness = brightness > 0.5 ? brightness - 0.2 : brightness + 0.1;
            double darkBrightness = brightness > 0.5 ? brightness - 0.6 : brightness - 0.4;

            if (lightBrightness > 1) { lightBrightness = 1; }
            if (darkBrightness < 0) { darkBrightness = 0; }

            Color lightColor = Slyce.Common.Colors.ChangeBrightness(e.Appearance.BackColor, lightBrightness);
            Color darkColor = Slyce.Common.Colors.ChangeBrightness(e.Appearance.BackColor, darkBrightness);

            e.Appearance.ForeColor = Slyce.Common.Colors.IdealTextColor(darkColor);// Color.White;
            e.Appearance.BackColor = lightColor;
            e.Appearance.BackColor2 = darkColor;
            e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            e.Appearance.Options.UseBackColor = true;
        }

        private void ucUserOptionsHelper_Paint(object sender, PaintEventArgs e)
        {
            if (CurrentBaseColor != Slyce.Common.Colors.BaseColor)
            {
                CurrentBaseColor = Slyce.Common.Colors.BaseColor;
                this.BackColor = Slyce.Common.Colors.BackgroundColor;
            }
        }

        private void mnuHelperTree_Opening(object sender, CancelEventArgs e)
        {
            if (treeListAPIHelper.FocusedColumn.AbsoluteIndex == 0)
            {
                e.Cancel = true;
                return;
            }
            mnuHelperItemDelete.Visible = true;
        }

        private void mnuHelperItemDelete_Click(object sender, EventArgs e)
        {
            TreeListNode selectedNode = treeListAPIHelper.Selection[0];

            if (treeListAPIHelper.FocusedColumn.AbsoluteIndex > 0)
            {
                DeleteHelperFunction((string)selectedNode[1]);
                return;
            }
        }

        private void DeleteHelperFunction(string functionName)
        {
            if (MessageBox.Show(string.Format("Delete {0}?", functionName), "Delete Function", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                Project.FunctionInfo function = Project.Instance.FindFunction(functionName);

                if (function != null)
                {
                    // Delete function deletes the DefaultValueFunction as well, if it exists.
                    Project.Instance.DeleteFunction(function);
                }
                else
                {
                    Project.DefaultValueFunction defValFunc = Project.Instance.FindDefaultValueFunction(functionName);

                    if (defValFunc != null)
                    {
                        Project.Instance.DefaultValueFunctions.Remove(defValFunc);
                    }
                }
            }
            //Populate();
            treeListAPIHelper.Invalidate();
        }





    }
}
