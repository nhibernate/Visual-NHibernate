using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Designer.Wizards;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace ArchAngel.Designer
{
    public partial class ucUserOptionsAPI : UserControl
    {
        private enum Images
        {
            Class = 0,
            Property = 1,
            Namespace = 2,
            Method = 3
        }
        private Color CurrentBaseColor = Color.Empty;
        private Dictionary<string, TreeListNode> SubNamespaceNodes = new Dictionary<string, TreeListNode>();
        private Type MethodInfoType = typeof(System.Reflection.MethodInfo);
        private Type UserOptionType = typeof(Project.UserOption);
        private bool BusySettingNodeValue = false;
        private static Type ExtensionAttributeType = typeof(ArchAngel.Interfaces.Attributes.ApiExtensionAttribute);
        private double TreelistApiHeightRatio;

        public ucUserOptionsAPI()
        {
            InitializeComponent();
            if (Slyce.Common.Utility.InDesignMode) { return; }

            ucLabel1.Text = "User Options";
            ucHeading1.Text = "Extensions to Provider API Functions";
            // TODO: position splitter equidistant from top and bottom
            //expandableSplitter1.SplitPosition = this.ClientSize.Height / 2;
            treeListAPI.Height = this.ClientSize.Height / 2 - ucLabel1.Height;
            TreelistApiHeightRatio = (double)treeListAPI.Height / (this.ClientSize.Height - ucLabel1.Height);
            Populate();
        }

        public void Clear()
        {
            treeListAPI.ClearNodes();
            treeListAPIHelper.ClearNodes();
        }

        public void Populate()
        {
            SubNamespaceNodes.Clear();

            PopulateUserOptions();
            PopulateApiHelperTreeview();

        }

        private void PopulateApiHelperTreeview()
        {
            treeListAPIHelper.BeginUnboundLoad();
            treeListAPIHelper.ClearNodes();

            foreach (System.Reflection.Assembly assembly in Project.Instance.ReferencedAssemblies)
            {
                if (ArchAngel.Interfaces.ProviderInfo.IsProvider(assembly))
                {
                    AddAssemblyToHelperTree(assembly, treeListAPIHelper);
                }
            }
            treeListAPIHelper.EndUnboundLoad();
        }

        private void AddAssemblyToHelperTree(System.Reflection.Assembly assembly, TreeList treelist)
        {
            string assemblyName = assembly.GetName().Name;
            TreeListNode assemblyNode = treelist.AppendNode(new object[] { assemblyName, "", "", "" }, null);
            assemblyNode.Tag = null;
            assemblyNode.ImageIndex = assemblyNode.SelectImageIndex = (int)Images.Namespace;
            Type[] types = assembly.GetTypes();
            string subNamespace = "";
            //Project.Instance.Namespaces.Sort();

            foreach (Type type in types)
            {
                TreeListNode classNode = null;
                subNamespace = type.FullName.Replace("+", ".").Replace(assemblyName + ".", "").Replace("." + type.Name, "").Replace(type.Name, "");
                List<Project.UserOption> virtualProperties = new List<Project.UserOption>();

                // UserOptions for this Type
                foreach (Project.UserOption uo in Project.Instance.UserOptions)
                {
                    if (uo.IteratorType != null && uo.IteratorType.FullName == type.FullName)
                    {
                        virtualProperties.Add(uo);
                    }
                }

                foreach (System.Reflection.MethodInfo method in type.GetMethods())
                {
                    bool hasApiExt = false;
                    string apiExtensionDescription = "";

                    //ArchAngel.Interfaces.Attributes.ApiExtensionAttribute[] attributes = (ArchAngel.Interfaces.Attributes.ApiExtensionAttribute[])method.GetCustomAttributes(ExtensionAttributeType, false);

                    //if (apiExtensionType == null)
                    //{
                    object[] allAttributes = method.GetCustomAttributes(false);

                    foreach (object att in allAttributes)
                    {
                        Type attType = att.GetType();

                        if (Slyce.Common.Utility.StringsAreEqual(attType.Name, "ApiExtensionAttribute", true))
                        {
                            hasApiExt = true;
                            apiExtensionDescription = (string)attType.InvokeMember("Description", System.Reflection.BindingFlags.GetProperty, null, att, null);

                            //if (Project.Instance.Namespaces.BinarySearch(type.FullName) < 0)
                            //{
                            //    Project.Instance.Namespaces.Add(type.FullName);
                            //    Project.Instance.Namespaces.Sort();
                            //}
                        }
                    }
                    //}
                    //else
                    //{
                    //    ArchAngel.Interfaces.Attributes.ApiExtensionAttribute[] attributes = (ArchAngel.Interfaces.Attributes.ApiExtensionAttribute[])method.GetCustomAttributes(ExtensionAttributeType, false);
                    //}
                    if (hasApiExt || virtualProperties.Count > 0)
                    {
                        // We are going to have child nodes, so make sure we add this type as a parent node.
                        if (classNode == null)
                        {
                            string fullNamespace = type.FullName.Substring(0, type.FullName.Replace("+", ".").LastIndexOf("." + type.Name));
                            TreeListNode subNamespaceNode = GetSubNamespaceNode(subNamespace, fullNamespace, assemblyNode, treelist);

                            if (subNamespaceNode == null)
                            {
                                classNode = treelist.AppendNode(new object[] { type.Name, "", "", "" }, assemblyNode);
                            }
                            else
                            {
                                classNode = treelist.AppendNode(new object[] { type.Name, "", "", "" }, subNamespaceNode);
                            }
                            classNode.Tag = type;
                            classNode.ImageIndex = classNode.SelectImageIndex = (int)Images.Class;
                        }
                    }
                    if (hasApiExt)
                    {
                        StringBuilder sbParams = new StringBuilder(100);

                        foreach (System.Reflection.ParameterInfo param in method.GetParameters())
                        {
                            sbParams.Append(param.ParameterType.Name + ", ");
                        }
                        string displayName = string.Format("{0}({1})", method.Name, sbParams.ToString().TrimEnd(new char[] { ' ', ',' }));
                        Project.FunctionInfo function = Project.Instance.FindFunction(method.Name, method.GetParameters());

                        //TreeListNode methodNode = AddTreeListNode(treelist, classNode, method.ReturnType, displayName, attributes[0].Description, "", "", Images.Method, false);
                        TreeListNode methodNode = AddTreeListNode(treelist, classNode, function, displayName, apiExtensionDescription, "", "", Images.Method, false);
                        methodNode.Tag = method;
                    }
                }
                if (classNode == null)
                {
                    object[] allAttributes = type.GetCustomAttributes(false);

                    foreach (object att in allAttributes)
                    {
                        Type attType = att.GetType();

                        if (Slyce.Common.Utility.StringsAreEqual(attType.Name, "ArchAngelEditorAttribute", true))
                        {
                            bool virtualPropertiesAllowed = (bool)attType.InvokeMember("VirtualPropertiesAllowed", System.Reflection.BindingFlags.GetProperty, null, att, null);

                            if (virtualPropertiesAllowed)
                            {
                                string fullNamespace = type.FullName.Substring(0, type.FullName.Replace("+", ".").LastIndexOf("." + type.Name));
                                TreeListNode subNamespaceNode = GetSubNamespaceNode(subNamespace, fullNamespace, assemblyNode, treelist);

                                if (subNamespaceNode == null)
                                {
                                    classNode = treelist.AppendNode(new object[] { type.Name, "", "", "" }, assemblyNode);
                                }
                                else
                                {
                                    classNode = treelist.AppendNode(new object[] { type.Name, "", "", "" }, subNamespaceNode);
                                }
                                classNode.Tag = type;
                                classNode.ImageIndex = classNode.SelectImageIndex = (int)Images.Class;
                            }
                            break;
                        }
                    }
                }
                // Add UserOptions for this Type to the tree
                foreach (Project.UserOption uo in virtualProperties)
                {
                    TreeListNode virtualPropertyNode = AddTreeListNode(treelist, classNode, uo, uo.VariableName, uo.Description, uo.ResetPerSession, "", Images.Property, "");
					//AddTreeListNode(treelist, virtualPropertyNode, uo.DefaultValue, "DefaultValueFunction", "Assigns the default value.", "", "", Images.Method, false);
					//AddTreeListNode(treelist, virtualPropertyNode, uo.ValidatorFunction, "ValidatorFunction", "Validate the value.", "", "", Images.Method, false);
					//AddTreeListNode(treelist, virtualPropertyNode, uo.DisplayToUserFunction, "DisplayToUserFunction", "Specifies whether to display to the user.", "", "", Images.Method, false);
					AddTreeListNode(treelist, virtualPropertyNode, null, "DefaultValueFunction", "Assigns the default value.", "", "", Images.Method, false);
					AddTreeListNode(treelist, virtualPropertyNode, null, "ValidatorFunction", "Validate the value.", "", "", Images.Method, false);
					AddTreeListNode(treelist, virtualPropertyNode, null, "DisplayToUserFunction", "Specifies whether to display to the user.", "", "", Images.Method, false);
                }

				// Add Extension Methods for this Type to the tree
				// Extension Methods for this Type
				foreach (var function in Project.Instance.Functions)
				{
					if (function.ExtendedType == type.FullName)
					{
						TreeListNode node = AddTreeListNode(treelist, classNode, function, function.Name, function.Description, "", "", Images.Method, false);

					}
				}
            }
        }

        private TreeListNode GetSubNamespaceNode(string subNamespace, string fullNamespace, TreeListNode assemblyNode, TreeList treelist)
        {
            TreeListNode subNamespaceNode = null;

            if (string.IsNullOrEmpty(subNamespace))
            {
                return null;
            }
        	
			if (SubNamespaceNodes.ContainsKey(fullNamespace))
        	{
        		subNamespaceNode = SubNamespaceNodes[fullNamespace];
        	}
        	else
        	{
        		subNamespaceNode = treelist.AppendNode(new object[] { subNamespace, "", "", "" }, assemblyNode);
        		subNamespaceNode.Tag = subNamespace;
        		subNamespaceNode.ImageIndex = subNamespaceNode.SelectImageIndex = (int)Images.Namespace;
        		SubNamespaceNodes.Add(fullNamespace, subNamespaceNode);
        	}
        	return subNamespaceNode;
        }

        internal void PopulateUserOptions()
        {
            treeListAPI.BeginUnboundLoad();
            treeListAPI.ClearNodes();

            TreeListNode namespaceNode = treeListAPI.AppendNode(new object[] { "User Options", "", "", "" }, null);
            namespaceNode.Tag = "User Options";
            namespaceNode.ImageIndex = namespaceNode.SelectImageIndex = (int)Images.Namespace;

            foreach (string category in Project.Instance.UserOptionCategories)
            {
                TreeListNode categoryNode = treeListAPI.AppendNode(new object[] { category, "", "", "" }, namespaceNode);
                categoryNode.Tag = "Category";
                categoryNode.ImageIndex = categoryNode.SelectImageIndex = (int)Images.Namespace;

                foreach (Project.UserOption userOption in Project.Instance.UserOptions)
                {
                    if (userOption.IteratorType != null || userOption.Category != category)
                    {
                        continue;
                    }
                    TreeListNode propertyNode = AddTreeListNode(
                        treeListAPI,
                        categoryNode,
                        "User Option",
                        userOption.VariableName,
                        string.Format("DefaultValue_UserOption_{0}", userOption.VariableName),
                        string.Format("Validate_UserOption_{0}", userOption.VariableName),
                        string.Format("DisplayToUser_UserOption_{0}", userOption.VariableName),
                        Images.Property,
                        userOption.ResetPerSession);
                }
            }
            treeListAPI.CollapseAll();
            treeListAPI.EndUnboundLoad();
        }

        private void AddAssemblyToTreelistAPI(System.Reflection.Assembly assembly, string allowedNamespace, TreeList treelist)
        {
            TreeListNode namespaceNode = treelist.AppendNode(new object[] { assembly.GetName().Name, "", "", "" }, null);
            namespaceNode.Tag = assembly;
            namespaceNode.ImageIndex = namespaceNode.SelectImageIndex = (int)Images.Namespace;

            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                if (type.FullName.IndexOf(allowedNamespace) != 0)
                {
                    continue;
                }
                TreeListNode objectNode = treelist.AppendNode(new object[] { type.Name, "", "", "" }, namespaceNode);
                objectNode.Tag = type;
                objectNode.ImageIndex = objectNode.SelectImageIndex = (int)Images.Class;
                bool methodsAdded = false;

                foreach (System.Reflection.MethodInfo method in type.GetMethods())
                {
                    ArchAngel.Interfaces.Attributes.ApiExtensionAttribute[] attributes = (ArchAngel.Interfaces.Attributes.ApiExtensionAttribute[])method.GetCustomAttributes(typeof(ArchAngel.Interfaces.Attributes.ApiExtensionAttribute), false);

                    if (attributes.Length == 0)
                    {
                        continue;
                    }
                    string overrideName = type.FullName.Replace("ArchAngel.Providers.Database.", "") + "." + method.Name;
                    overrideName = overrideName.Replace(".", "_");
                    TreeListNode propertyNode = AddTreeListNode(treelist, objectNode, method, method.Name, attributes[0].Description, "", "", Images.Method, false);
                    methodsAdded = true;
                }
                // UserOptions for this Type
                foreach (Project.UserOption uo in Project.Instance.UserOptions)
                {
                    if (uo.IteratorType == type)
                    {
                        TreeListNode propertyNode = AddTreeListNode(
                            treelist,
                            objectNode,
                            uo,
                            uo.VariableName,
                            string.Format("DefaultValue_{0}_{1}", type.Name, uo.VariableName),
                            string.Format("Validate_{0}_{1}", type.Name, uo.VariableName),
                            string.Format("DisplayToUser_{0}_{1}", type.Name, uo.VariableName),
                            Images.Property,
                            uo.ResetPerSession);
                        methodsAdded = true;
                    }
                }
                if (!methodsAdded)
                {
                    treelist.Nodes.Remove(objectNode);
                }
            }
        }

        private void mnuItemViewFunction_Click(object sender, EventArgs e)
        {
            Project.UserOption.FunctionTypes funcType;

            switch (treeListAPI.FocusedColumn.AbsoluteIndex)
            {
                case 1:
                    funcType = Project.UserOption.FunctionTypes.DefaultValue;
                    break;
                case 2:
					funcType = Project.UserOption.FunctionTypes.Validation;
                    break;
                case 3:
					funcType = Project.UserOption.FunctionTypes.DisplayToUser;
                    break;
                default:
                    throw new NotImplementedException("Not coded yet.");
            }
            Type tagType = treeListAPI.FocusedNode.Tag.GetType();
            //bool isForUserOption = tagType == typeof(Project.UserOption) || (tagType == typeof(string) && treeListAPI.FocusedNode.Tag.ToString() == "User Option");
            ViewFunctionForUserOption(funcType);
        }

        /// <summary>
        /// Shows the function. Creates the function if it doesn't yet exist.
        /// </summary>
        /// <param name="functionType">DefaultValue or Validate function-type.</param>
        /// <param name="isForUserOption">Whether the function is for a user-defined property (UserOption) or a standard property of the API.</param>
		private void ViewFunction(Project.UserOption.FunctionTypes functionType, bool isForUserOption)
        {
            Controller.Instance.MainForm.Cursor = Cursors.WaitCursor;
            TreeListNode selectedNode = treeListAPI.Selection[0];
            TreeListNode topLevelNode = selectedNode;
            Type objectType = null;
            string functionName;
            string propertyName = "";
            Project.FunctionInfo function;
            Type returnType;
            bool isNew = false;

            while (topLevelNode.ParentNode != null)
            {
                topLevelNode = topLevelNode.ParentNode;
            }
            if (topLevelNode.Tag.GetType() == typeof(System.Reflection.Assembly))
            {
                objectType = (Type)selectedNode.ParentNode.Tag;
                propertyName = selectedNode[0].ToString();
                System.Reflection.PropertyInfo prop = objectType.GetProperty(propertyName);
                Type propertyType;
                bool isUserOption = false;

                if (prop == null)
                {
                    // This must be a custom property added by the user, so therefore a UserOption
                    Project.UserOption userOption = Project.Instance.FindUserOption(propertyName);
                    propertyType = userOption.VarType;
                    isUserOption = true;
                }
                else
                {
                    propertyType = objectType.GetProperty(propertyName).PropertyType;
                }
                string description;
                string category;

                switch (functionType)
                {
                    case Project.UserOption.FunctionTypes.DefaultValue:
                        functionName = string.Format("DefaultValue_{0}_{1}", objectType.Name, propertyName);
                        description = string.Format("Default value function for {0}.{1}.", objectType.Name, propertyName);
                        category = "Default Values";
                        returnType = propertyType;
                        break;
					case Project.UserOption.FunctionTypes.Validation:
                        functionName = string.Format("Validate_{0}_{1}", objectType.Name, propertyName);
                        description = string.Format("Validation function for {0}.{1}.", objectType.Name, propertyName);
                        category = "Validators";
                        returnType = typeof(bool);
                        break;
					case Project.UserOption.FunctionTypes.DisplayToUser:
                        functionName = string.Format("DisplayToUser_{0}_{1}", objectType.Name, propertyName);
                        description = string.Format("DisplayToUser function for {0}.{1}.", objectType.Name, propertyName);
                        category = "DisplayToUser";
                        returnType = typeof(bool);
                        break;
                    default:
                        throw new NotImplementedException("Not coded yet: " + functionType.ToString());
                }
                //function = Project.Instance.FindFunctionSingle(functionName);
                //Project.DefaultValueFunction defValFunc = Project.Instance.FindDefaultValueFunction(functionName, function.Parameters);

                //if (defValFunc == null)
                //{
				//    defValFunc = new Project.DefaultValueFunction(objectType, propertyName, false, functionType, isForUserOption);
				//    Project.Instance.DefaultValueFunctions.Add(defValFunc);
				//    Project.Instance.IsDirty = true;
				//}
				//if (isNew && !isUserOption)
				//{
				//    function.Body = defValFunc.GetFormattedDefaultCode();
				//}
                Controller.Instance.MainForm.ShowFunction(function, defValFunc, false, this);
            }
            else
            {
				// User Option function
				ViewFunctionForUserOption(functionType);
            }
            Controller.Instance.MainForm.Cursor = Cursors.Default;
        }

		private void ViewFunctionForUserOption(Project.UserOption.FunctionTypes functionType)
		{
			TreeListNode selectedNode = treeListAPI.Selection[0];
			Project.UserOption userOption = Project.Instance.FindUserOption((string)selectedNode.GetValue(0));

			switch (functionType)
			{
				case Project.DefaultValueFunction.FunctionTypes.DefaultValue:
					Controller.Instance.MainForm.ShowFunction(userOption, Project.UserOption.FunctionTypes.DefaultValue, false, (Control)this);
					break;
				case Project.DefaultValueFunction.FunctionTypes.DisplayToUser:
					Controller.Instance.MainForm.ShowFunction(userOption, Project.UserOption.FunctionTypes.DisplayToUser, false, this);
					break;
				//case Project.DefaultValueFunction.FunctionTypes.HelperOverride:
				//    Controller.Instance.MainForm.ShowFunction(userOption, Project.UserOption.FunctionTypes.HelperOverride, false, this);
				//    break;
				case Project.DefaultValueFunction.FunctionTypes.Validate:
					Controller.Instance.MainForm.ShowFunction(userOption, Project.UserOption.FunctionTypes.Validation, false, this);
					break;
				default:
					throw new NotImplementedException("Not handled yet: " + functionType.ToString());
			}
		}

        private TreeListNode AddTreeListNode(
            TreeList treelist,
            TreeListNode node,
            object tag,
            object column0,
            object column1,
            object column2,
            object column3,
            Images image,
            object column4)
        {
            TreeListNode newNode = treelist.AppendNode(new object[] { column0, column1, column2, column3, column4 }, node);
            newNode.ImageIndex = newNode.SelectImageIndex = (int)image;
            newNode.Tag = tag;
            return newNode;
        }

        private void treeListAPI_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraTreeList.TreeListHitInfo hInfo = treeListAPI.CalcHitInfo(new Point(e.X, e.Y));
            TreeListNode node = hInfo.Node;

            if (node == null)
            {
                return;
            }
            // Toggle Checkbox
            if (e.Button == MouseButtons.Right)
            {
                treeListAPI.FocusedNode = node;
                treeListAPI.FocusedColumn = hInfo.Column;
            }
        }

        private void treeListAPI_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            bool isFocusedNode = e.Node.TreeList.FocusedNode == e.Node;
            bool hasActualDefaultFunction = false;
            FontStyle fontStyle;

            switch (e.Column.AbsoluteIndex)
            {
                case 0:
                    fontStyle = FontStyle.Regular;

                    if (e.Node.Tag != null) 
					{
						if(e.Node.Tag is Project.UserOption)
						{
							fontStyle = FontStyle.Italic;
							e.Appearance.ForeColor = Color.Blue;
						}
						else if(e.Node.Tag is Project.FunctionInfo)
						{
							fontStyle = FontStyle.Italic;
							e.Appearance.ForeColor = Color.DarkBlue;
						}
					}
                    break;
                case 1:
                    string val = (string)e.Node.GetValue(1);

                    if (Project.Instance.FindDefaultValueFunction(val, new Project.ParamInfo[0]) != null || Project.Instance.FindFunctionSingle(val) != null)
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
                case 2:
                    string val2 = (string)e.Node.GetValue(2);

                    if (Project.Instance.FindDefaultValueFunction(val2, new Project.ParamInfo[0]) != null || Project.Instance.FindFunctionSingle(val2) != null)
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
                case 3:
                    string val3 = (string)e.Node.GetValue(3);

                    if (Project.Instance.FindDefaultValueFunction(val3, new Project.ParamInfo[0]) != null || Project.Instance.FindFunctionSingle(val3) != null)
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
                case 4: // Reset per session
                    fontStyle = FontStyle.Regular;

                    //if (!typeof(Project.UserOption).IsInstanceOfType(e.Node.Tag))
                    //{
                    //    e.Node..Visible = false;
                    //}
                    break;
                default:
                    throw new NotImplementedException("Not coded yet.");
            }
            if (hasActualDefaultFunction)
            {
                //e.Appearance.BackColor = Slyce.Common.Colors.ChangeBrightness(Slyce.Common.Colors.BackgroundColor, 0.9); //Slyce.Common.Colors.FadingTitleLightColor;// Color.AliceBlue;
                if (e.Node == treeListAPI.FocusedNode && e.Column.AbsoluteIndex == treeListAPI.FocusedColumn.AbsoluteIndex)
                {
                    e.Appearance.BackColor = Slyce.Common.Colors.ChangeBrightness(Slyce.Common.Colors.BackgroundColor, 0.9);
                }
            }
            //e.Appearance.BackColor = Slyce.Common.Colors.ChangeBrightness(Slyce.Common.Colors.BackgroundColor, 0.9); //Slyce.Common.Colors.FadingTitleLightColor;// Color.AliceBlue;
            e.Appearance.Font = new Font(e.Appearance.Font, fontStyle);

            if (e.Node.Focused && e.Column.AbsoluteIndex == treeListAPI.FocusedColumn.AbsoluteIndex)
            {
                if (e.Column.AbsoluteIndex != 0 ||
                    (e.Column.AbsoluteIndex == 0 && e.Node.ParentNode != null) ||
                    (e.Node.ParentNode == null && e.Node.Tag != null && e.Node.Tag.ToString() == "User Options"))
                {
                    SetFocusedNodeProperties(e, true);
                }
            }
        }

        private void SetFocusedNodeProperties(GetCustomNodeCellStyleEventArgs e, bool isFocused)
        {
            if (isFocused)
            {
                e.Appearance.BackColor = Color.White;
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

        private void ucUserOptionsAPI_Paint(object sender, PaintEventArgs e)
        {
            if (CurrentBaseColor != Slyce.Common.Colors.BaseColor)
            {
                CurrentBaseColor = Slyce.Common.Colors.BaseColor;
                this.BackColor = Slyce.Common.Colors.BackgroundColor;
            }
            ucLabel1.Refresh();
            ucHeading1.Refresh();
        }

        private void mnuItemDelete_Click(object sender, EventArgs e)
        {
            TreeListNode selectedNode = treeListAPI.Selection[0];

            if (treeListAPI.FocusedColumn.AbsoluteIndex > 0)
            {
                DeleteFunction((string)selectedNode[treeListAPI.FocusedColumn.AbsoluteIndex]);
                return;
            }
            object tag = treeListAPI.FocusedNode.Tag;

            if (tag.GetType() == typeof(System.Reflection.PropertyInfo))
            {
                // This is an API property
                //mnuItemViewFunction.Visible = true;
            }
            else if (tag.GetType() == typeof(string) && (string)tag == "Category")
            {
                // This is a user option category
                mnuItemNewUserOption.Visible = true;
                return;
            }
            else if (treeListAPI.FocusedNode.ParentNode != null && treeListAPI.FocusedNode.ParentNode.Tag.GetType() == typeof(string) && (string)treeListAPI.FocusedNode.ParentNode.Tag == "Category")
            {
                string userOptionName = (string)selectedNode.GetValue(0);

                // This is an individual user option
                if (MessageBox.Show(this, string.Format("Delete {0}?", userOptionName), "Delete UserOption", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    Controller.Instance.MainForm.Cursor = Cursors.WaitCursor;
                    Project.Instance.DeleteUserOption(userOptionName);
                    treeListAPI.DeleteNode(selectedNode);
                    Controller.Instance.MainForm.Cursor = Cursors.Default;
                }
                return;
            }
            else if (treeListAPI.FocusedNode.Tag.GetType() == typeof(Project.UserOption))
            {
                string userOptionName = (string)selectedNode.GetValue(0);

                // This is an individual user option
                if (MessageBox.Show(this, string.Format("Delete {0}?", userOptionName), "Delete UserOption", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    Controller.Instance.MainForm.Cursor = Cursors.WaitCursor;
                    Project.Instance.DeleteUserOption(userOptionName);
                    treeListAPI.DeleteNode(selectedNode);
                    Controller.Instance.MainForm.Cursor = Cursors.Default;
                }
                return;

            }
        }

        private void DeleteFunction(string functionName)
        {
            throw new NotImplementedException("GFH: this parameter is wrong.");
            /*
                        if (MessageBox.Show(this, string.Format("Delete {0}?", functionName), "Delete Function", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            Controller.Instance.MainForm.Cursor = Cursors.WaitCursor;
                            throw new NotImplementedException("GFH: need to fix this function. Shouldn't be FindFunctionSingle below.....");
                            Project.FunctionInfo function = Project.Instance.FindFunctionSingle(functionName);

                            if (function != null)
                            {
                                // Delete function deletes the DefaultValueFunction as well, if it exists.
                                Project.Instance.DeleteFunction(function);

                                if (treeListAPI.FocusedNode.Tag.GetType() == typeof(string) && (string)treeListAPI.FocusedNode.Tag == "User Option" && treeListAPI.FocusedColumn.AbsoluteIndex == 2)
                                {
                                    Project.UserOption userOption = Project.Instance.FindUserOption((string)treeListAPI.FocusedNode.GetValue(0));
                                    userOption.ValidatorFunction = "";
                                }
                            }
                            else
                            {
                                Project.DefaultValueFunction defValFunc = Project.Instance.FindDefaultValueFunction(function.Name, new Project.ParamInfo[0]);

                                if (defValFunc != null)
                                {
                                    Project.Instance.DefaultValueFunctions.Remove(defValFunc);
                                }
                            }
                            Controller.Instance.MainForm.Cursor = Cursors.Default;
                        }
                        //Populate();
                        treeListAPI.Invalidate();
            */
        }

        private void HideAllMenuItems(ContextMenuStrip menu)
        {
            foreach (ToolStripItem item in menu.Items)
            {
                item.Visible = false;
            }
        }

        private void mnuTreeNode_Opening(object sender, CancelEventArgs e)
        {
            HideAllMenuItems(mnuTreeNode);

            if (treeListAPI.FocusedNode == null)
            {
                e.Cancel = true;
                return;
            }

            if (treeListAPI.FocusedColumn.AbsoluteIndex == 0)
            {
                object tag = treeListAPI.FocusedNode.Tag;

                if (tag.GetType() == typeof(System.Reflection.PropertyInfo))
                {
                    // This is an API property
                    //mnuItemViewFunction.Visible = true;
                    // Make at least one item visible
                    mnuItemEdit.Visible = true;
                    //string gg = "";
                }
                else if (treeListAPI.FocusedNode.ParentNode == null && treeListAPI.FocusedNode.Tag.GetType() == typeof(System.Reflection.Assembly))
                {
                    // This is the root API assembly node
                    // Make at least one item visible
                    mnuItemEdit.Visible = true;
                    e.Cancel = true;
                    return;
                }
                else if (treeListAPI.FocusedNode.ParentNode != null && treeListAPI.FocusedNode.ParentNode.Tag.GetType() == typeof(System.Reflection.Assembly))
                {
                    // This is an object of an API
                    mnuItemNewProperty.Visible = true;
                    mnuTreeNode.Invalidate();
                    // TODO: ability to delete virtual properties
                    return;
                }
                else if (treeListAPI.FocusedNode.ParentNode == null && tag.GetType() == typeof(string) && (string)tag == "User Options")
                {
                    // This is the root UserOptions node
                    mnuItemNewUserOption.Visible = true;
                    return;
                }
                else if (tag.GetType() == typeof(string) && (string)tag == "Category")
                {
                    // This is a user option category
                    mnuItemNewUserOption.Visible = true;
                    return;
                }
				else if(tag.GetType() == typeof(string) && (string) tag == "Extension Method")
				{
					// This is an extension method
					mnuItemDelete.Visible = true;
					mnuItemEdit.Visible = true;
					mnuItemViewFunction.Visible = true;
					return;
				}
                else if (treeListAPI.FocusedNode.ParentNode != null && treeListAPI.FocusedNode.ParentNode.Tag.GetType() == typeof(string) && (string)treeListAPI.FocusedNode.ParentNode.Tag == "Category")
                {
                    // This is an individual user option
                    mnuItemDelete.Visible = true;
                    mnuItemEdit.Visible = true;
                    return;
                }
                else if (tag.GetType() == typeof(Project.UserOption))
                {
                    // Custom property (UserOption)
                    mnuItemEdit.Visible = true;
                    mnuItemDelete.Visible = true;
                    return;
                }
                // Make at least one item visible
                mnuItemEdit.Visible = true;
                e.Cancel = true;
                return;
            }
            else
            {
                if (!typeof(string).IsInstanceOfType(treeListAPI.FocusedNode.GetValue(treeListAPI.FocusedColumn.AbsoluteIndex)) ||
                    ((string)treeListAPI.FocusedNode.GetValue(treeListAPI.FocusedColumn.AbsoluteIndex)).Length == 0)
                {
                    e.Cancel = true;
                    return;
                }
                string tag = treeListAPI.FocusedNode.Tag.ToString();

                if (tag == "User Option")
                {
                    mnuItemViewFunction.Visible = true;

                    if (treeListAPI.FocusedColumn.AbsoluteIndex == 2)
                    {
                        mnuItemDelete.Visible = true;
                    }
                }
                else if (tag == "Category")
                {
                    // Make at least one item visible
                    mnuItemEdit.Visible = true;
                    e.Cancel = true;
                    return;
                }
                else
                {
                    if (treeListAPI.FocusedNode.Tag.GetType() != typeof(Project.UserOption))
                    {
                        mnuItemDelete.Visible = true;
                        mnuTreeNodeSeparator1.Visible = true;
                    }
                    mnuItemViewFunction.Visible = true;
                }
            }
        }

        private void treeListAPI_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {

        }

        private void mnuItemEdit_Click(object sender, EventArgs e)
        {
            Controller.Instance.MainForm.Cursor = Cursors.WaitCursor;
            mnuTreeNode.Visible = false;
            Controller.Instance.MainForm.Refresh();
            Application.DoEvents();
            TreeListNode selectedNode = treeListAPI.Selection[0];
            Project.UserOption userOption = Project.Instance.FindUserOption((string)selectedNode.GetValue(0));

            frmOptionEdit form = new frmOptionEdit(userOption);
            Cursor = Cursors.Default;

            if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
            {
                selectedNode.SetValue(0, form.CurrentOption.VariableName);
				//selectedNode.SetValue(1, form.CurrentOption.DefaultValue);
				//selectedNode.SetValue(2, form.CurrentOption.ValidatorFunction);
				selectedNode.SetValue(1, "DefaultValue");
				selectedNode.SetValue(2, "Validator");
                selectedNode.SetValue(4, form.CurrentOption.ResetPerSession);

                string currentCategory = (string)selectedNode.ParentNode.GetValue(0);

                if (currentCategory != form.CurrentOption.Category)
                {
                    bool found = false;

                    foreach (TreeListNode node in selectedNode.ParentNode.ParentNode.Nodes)
                    {
                        if ((string)node.GetValue(0) == form.CurrentOption.Category)
                        {
                            found = true;
                            treeListAPI.BeginUnboundLoad();
                            treeListAPI.MoveNode(selectedNode, node);
                            treeListAPI.EndUnboundLoad();
                            break;
                        }
                    }
                    if (!found)
                    {
                        treeListAPI.BeginUnboundLoad();
                        TreeListNode categoryNode = treeListAPI.AppendNode(new object[] { form.CurrentOption.Category, "", "", "", "" }, selectedNode.ParentNode.ParentNode);
                        categoryNode.Tag = "Category";
                        categoryNode.ImageIndex = categoryNode.SelectImageIndex = (int)Images.Namespace;
                        treeListAPI.MoveNode(selectedNode, categoryNode);
                        treeListAPI.EndUnboundLoad();
                    }
                }
                return;
            }
        }

        private void mnuItemNewUserOption_Click(object sender, EventArgs e)
        {
            mnuTreeNode.Visible = false;
            Controller.Instance.MainForm.Refresh();
            Application.DoEvents();

            TreeListNode selectedNode = treeListAPI.Selection[0];
            string category;

            if ((string)treeListAPI.FocusedNode.Tag == "User Options")
            {
                category = "General";
            }
            else
            {
                category = (string)treeListAPI.FocusedNode.GetValue(0);
            }
            frmOptionEdit form = new frmOptionEdit(category, null);

            if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
            {
                bool addedFromRoot = false;
                string currentCategory = "";

                if ((string)selectedNode.Tag == "User Options")
                {
                    addedFromRoot = true;
                }
                else
                {
                    currentCategory = (string)selectedNode.ParentNode.GetValue(0);
                }
                TreeListNode rootNode = addedFromRoot ? selectedNode : selectedNode.ParentNode;
                TreeListNode userOptionNode = AddTreeListNode(
                    treeListAPI,
                    rootNode,
                    "User Option",
                    form.CurrentOption.VariableName,
                    string.Format("DefaultValue_UserOption_{0}", form.CurrentOption.VariableName),
                    string.Format("Validate_UserOption_{0}", form.CurrentOption.VariableName),
                    string.Format("DisplayToUser_UserOption_{0}", form.CurrentOption.VariableName),
                    Images.Property,
                    form.CurrentOption.ResetPerSession);

                if (addedFromRoot || currentCategory != form.CurrentOption.Category)
                {
                    bool found = false;
                    TreeListNodes categoryNodes = rootNode.Nodes;

                    foreach (TreeListNode node in categoryNodes)
                    {
                        if ((string)node.GetValue(0) == form.CurrentOption.Category)
                        {
                            found = true;
                            treeListAPI.BeginUnboundLoad();
                            treeListAPI.MoveNode(userOptionNode, node);
                            treeListAPI.EndUnboundLoad();
                            break;
                        }
                    }
                    if (!found)
                    {
                        treeListAPI.BeginUnboundLoad();
                        TreeListNode categoryNode = treeListAPI.AppendNode(new object[] { form.CurrentOption.Category, "", "", "", "" }, rootNode);
                        categoryNode.Tag = "Category";
                        categoryNode.ImageIndex = categoryNode.SelectImageIndex = (int)Images.Namespace;
                        treeListAPI.MoveNode(userOptionNode, categoryNode);
                        treeListAPI.EndUnboundLoad();
                    }
                }
            }
        }

        private void mnuHelperItemViewFunction_Click(object sender, EventArgs e)
        {
            Controller.Instance.MainForm.Cursor = Cursors.WaitCursor;
            TreeListNode selectedNode = treeListAPIHelper.FocusedNode;
            System.Reflection.MethodInfo method = null;
            string functionName = "";
            Project.FunctionInfo function;
            bool isForUserOption = false;
            bool isApiMethodOverride = false;
            Project.UserOption userOption = null;

            if (selectedNode.ParentNode != null &&
                selectedNode.ParentNode.Tag.GetType() == typeof(Project.UserOption))
            {
                isForUserOption = true;
                userOption = (Project.UserOption)selectedNode.ParentNode.Tag;
                functionName = (string)selectedNode.Tag;
                function = Project.Instance.FindFunctionSingle(functionName);
            }
            else if (selectedNode.ParentNode.Tag.GetType() == typeof(Project.FunctionInfo))
            {
                isApiMethodOverride = true;
                function = (Project.FunctionInfo)selectedNode.Tag;
                functionName = function.Name.Replace("+", ".") + "." + method.Name;
                functionName = functionName.Replace(".", "_");
            }
			else if (selectedNode.Tag is Project.FunctionInfo)
			{
				function = (Project.FunctionInfo)selectedNode.Tag;
				functionName = function.Name;
			}
            else
            {
                isApiMethodOverride = true;
				method = (System.Reflection.MethodInfo)selectedNode.Tag;
                functionName = method.ReflectedType.FullName.Replace("+", ".") + "." + method.Name;
                functionName = functionName.Replace(".", "_");
                function = Project.Instance.FindFunction(functionName, method.GetParameters());
            }
            //Project.FunctionInfo function = Project.Instance.FindFunction(functionName);
            bool isNewFunction = function == null;

            if (function == null)
            {
                //ArchAngel.Interfaces.Attributes.ApiExtensionAttribute[] attributes = null;
                string defaultCode = "";
                System.Reflection.ParameterInfo[] parameters;

                if (!isForUserOption)
                {
                    //attributes = (ArchAngel.Interfaces.Attributes.ApiExtensionAttribute[])method.GetCustomAttributes(typeof(ArchAngel.Interfaces.Attributes.ApiExtensionAttribute), true);

                    //if (attributes.Length == 0)
                    //{
                    //    throw new NotImplementedException(string.Format("DefaultCodeAttribute not implemented for {0}.{1} yet.", method.ReflectedType.FullName, method.Name));
                    //}
                    object[] allAttributes = method.GetCustomAttributes(false);
                    bool hasApiExt = false;

                    foreach (object att in allAttributes)
                    {
                        Type attType = att.GetType();

                        if (Slyce.Common.Utility.StringsAreEqual(attType.Name, "ApiExtensionAttribute", true))
                        {
                            hasApiExt = true;
                            defaultCode = (string)attType.InvokeMember("DefaultCode", System.Reflection.BindingFlags.GetProperty, null, att, null);
                            break;
                        }
                    }
                    if (!hasApiExt)
                    {
                        throw new NotImplementedException(string.Format("DefaultCodeAttribute not implemented for {0}.{1} yet.", method.ReflectedType.FullName, method.Name));
                    }
                    parameters = method.GetParameters();
                }
                else
                {
                    parameters = new System.Reflection.ParameterInfo[0];
                }
                Type returnType;

                if (isForUserOption)
                {
                    returnType = userOption.VarType;
                    //function = new Project.FunctionInfo(functionName, returnType, "XXX", false, Project.ScriptLanguageTypes.CSharp, string.Format("Override of helper function: {0}.{1}.", userOption.IteratorType.FullName, userOption.VariableName), "XXX", "Helper Overrides");
                }
                else
                {
                    returnType = method.ReturnType;
                }
                string description = method == null ? "" : string.Format("Override of helper function: {0}.{1}.", method.ReflectedType.FullName, method.Name);
                function = new Project.FunctionInfo(functionName, returnType, defaultCode, false, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp, description, "XXX", "Helper Overrides");
                //System.Reflection.ParameterInfo[] parameters = method.GetParameters();

                foreach (System.Reflection.ParameterInfo parameter in parameters)
                {
                    ArchAngel.Designer.Project.ParamInfo par = new Project.ParamInfo(parameter.Name, parameter.ParameterType);
                    function.AddParameter(par);
                }
                Project.Instance.AddFunction(function);
            }
            Project.DefaultValueFunction defValFunc = Project.Instance.FindDefaultValueFunction(functionName, function.Parameters);

            if (defValFunc == null && function.IsExtensionMethod == false)
            {
                Project.ParamInfo[] parameterTypes = new Project.ParamInfo[function.Parameters.Length];

                for (int i = 0; i < function.Parameters.Length; i++)
                {
                    parameterTypes[i] = new Project.ParamInfo(Slyce.Common.Utility.GetCamelCase(function.Parameters[i].Name), function.Parameters[i].DataType);
                	parameterTypes[i].Modifiers = function.Parameters[i].Modifiers;
                }
                if (isApiMethodOverride)
                {
                    defValFunc = new Project.DefaultValueFunction(method.ReflectedType, method.Name, false, Project.DefaultValueFunction.FunctionTypes.HelperOverride, isForUserOption);
                }
                else
                {
                    Project.UserOption.FunctionTypes funcType = Project.UserOption.FunctionTypes.DefaultValue;

                    switch ((string)selectedNode.GetValue(0))
                    {
                        case "DefaultValueFunction":
                            funcType = Project.DefaultValueFunction.FunctionTypes.DefaultValue;
                            break;
                        case "DisplayToUserFunction":
                            funcType = Project.DefaultValueFunction.FunctionTypes.DisplayToUser;
                            break;
                        case "ValidateFunction":
                        case "ValidatorFunction":
                            funcType = Project.DefaultValueFunction.FunctionTypes.Validate;
                            break;
                        default:
                            throw new NotImplementedException("Virtual property method-type not handled yet. Please inform support@slyce.com about this error.");
                    }
                    defValFunc = new Project.DefaultValueFunction(function.ReturnType, function.Name, false, funcType, isForUserOption);
                }
                defValFunc.ParameterTypes = parameterTypes;
                Project.Instance.DefaultValueFunctions.Add(defValFunc);

                if (isNewFunction && !isForUserOption)
                {
                    function.Body = defValFunc.GetFormattedDefaultCode();
                }
                Project.Instance.IsDirty = true;
            }
            bool allowEdit = defValFunc == null;
            Controller.Instance.MainForm.ShowFunction(function, defValFunc, allowEdit, this);
            Controller.Instance.MainForm.Cursor = Cursors.Default;
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
            }
        }

        private void treeListAPIHelper_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (BusySettingNodeValue || Controller.BusySaving || Controller.BusyCompiling) { return; }
            bool isFocusedNode = e.Node.TreeList.FocusedNode == e.Node;
            bool hasActualDefaultFunction = false;
            bool isVirtualProperty = false;
            object obj = e.Node.Tag;
            string functionName = "";
            System.Reflection.MethodInfo method = null;

            if (e.Node.Tag != null && e.Node.Tag.GetType().BaseType == MethodInfoType)
            {
                method = (System.Reflection.MethodInfo)e.Node.Tag;
                functionName = method.ReflectedType.FullName.Replace("+", ".") + "." + method.Name;

                if (Project.Instance.FindDefaultValueFunction(functionName, method.GetParameters()) != null || Project.Instance.FindFunction(functionName, method.GetParameters()) != null)
                {
                    hasActualDefaultFunction = true;
                }
            }
            else if (e.Node.Tag != null && e.Node.Tag.GetType() == typeof(Project.UserOption))
            {
                isVirtualProperty = true;

                if (Project.Instance.FindDefaultValueFunction(functionName, new Project.ParamInfo[0]) != null || Project.Instance.FindFunctionSingle(functionName) != null)
                {
                    hasActualDefaultFunction = true;
                }
            }
            FontStyle fontStyle;

            switch (e.Column.AbsoluteIndex)
            {
                case 0:
                    string functionDisplayName = e.Node.GetValue(0).ToString().Replace(" (overridden)", "");

                    if (hasActualDefaultFunction)
                    {
                        fontStyle = FontStyle.Bold;
                        e.Appearance.BackColor = Slyce.Common.Colors.ChangeBrightness(Slyce.Common.Colors.BackgroundColor, 0.9);
                        SetFocusedNodeProperties(e, false);
                        BusySettingNodeValue = true;

                        if (method != null)
                        {
                            e.Node.SetValue(0, string.Format("{0} (overridden)", functionDisplayName));
                        }
                        BusySettingNodeValue = false;
                    }
                    else
                    {
                        fontStyle = FontStyle.Regular;
                        BusySettingNodeValue = true;

                        if (method != null)
                        {
                            e.Node[0] = functionDisplayName;
                        }
                        BusySettingNodeValue = false;
                    }
                    break;
                case 1:
                    if (hasActualDefaultFunction)
                    {
                        fontStyle = FontStyle.Italic | FontStyle.Bold;
                        e.Appearance.BackColor = Slyce.Common.Colors.ChangeBrightness(Slyce.Common.Colors.BackgroundColor, 0.9);
                        SetFocusedNodeProperties(e, false);
                    }
                    else
                    {
                        fontStyle = FontStyle.Italic;
                    }
                    break;
                case 2:
                    fontStyle = FontStyle.Italic;
                    break;
                default:
                    throw new NotImplementedException("Not coded yet.");
            }
            if (hasActualDefaultFunction)
            {
                if (e.Node == treeListAPIHelper.FocusedNode && e.Column.AbsoluteIndex == treeListAPIHelper.FocusedColumn.AbsoluteIndex)
                {
                    e.Appearance.BackColor = Slyce.Common.Colors.ChangeBrightness(Slyce.Common.Colors.BackgroundColor, 0.9);
                }
            }
            if (isVirtualProperty)
            {
                fontStyle = FontStyle.Italic;
                e.Appearance.ForeColor = Color.Blue;
            }
            if (e.Node != null && e.Node.ParentNode != null && e.Node.ParentNode.Tag != null && e.Node.ParentNode.Tag.GetType() == typeof(Project.UserOption))
            {
                e.Appearance.BackColor = Slyce.Common.Colors.ChangeBrightness(Slyce.Common.Colors.BackgroundColor, 0.9);
                SetFocusedNodeProperties(e, false);
            }
            e.Appearance.Font = new Font(e.Appearance.Font, fontStyle);

            if (e.Node.Focused)// && e.Column.AbsoluteIndex == treeListAPIHelper.FocusedColumn.AbsoluteIndex)
            {
                //if (e.Column.AbsoluteIndex != 0)
                //{
                SetFocusedNodeProperties(e, true);
                //}
            }
        }

        private void mnuHelperTree_Opening(object sender, CancelEventArgs e)
        {
            HideAllMenuItems(mnuHelperTreeEditVirtualProperty);
        	TreeListNode node = treeListAPIHelper.FocusedNode;
        	if (node == null || node.Tag == null)
            {
                e.Cancel = true;
                return;
            }
            mnuHelperTreeEditVirtualProperty.Visible = false;

            if (node.Tag.GetType().BaseType == MethodInfoType)
            {
                mnuHelperItemViewFunction.Visible = true;
            }
			else if(node.Tag is Project.FunctionInfo)
			{
                Project.FunctionInfo func = (Project.FunctionInfo)node.Tag;
				mnuHelperItemViewFunction.Visible = true;
                mnuHelperItemDelete.Visible = func.IsExtensionMethod;
			}
            else if (node.Tag.GetType().BaseType == typeof(Type))
            {
                Type objType = (Type)node.Tag;
                object[] allAttributes = objType.GetCustomAttributes(false);
                bool hasApiExt = false;
                bool virtualPropertiesAllowed = false;

                foreach (object att in allAttributes)
                {
                    Type attType = att.GetType();

                    if (Slyce.Common.Utility.StringsAreEqual(attType.Name, "ArchAngelEditorAttribute", true))
                    {
                        hasApiExt = true;
                        virtualPropertiesAllowed = (bool)attType.InvokeMember("VirtualPropertiesAllowed", System.Reflection.BindingFlags.GetProperty, null, att, null);
                        break;
                    }
                }
                if (hasApiExt && virtualPropertiesAllowed)
                {
                    mnuHelperItemNewVirtualProperty.Enabled = true;
                }
                else
                {
                    mnuHelperItemNewVirtualProperty.Enabled = false;
                }
                mnuItemNewExtensionMethod.Visible = true;
                mnuHelperItemNewVirtualProperty.Visible = true;
            }
            else if (node.ParentNode != null &&
                node.ParentNode.Tag != null &&
           node.ParentNode.Tag.GetType() == typeof(Project.UserOption))
            {
                mnuHelperItemViewFunction.Visible = true;
            }
            else if (node.Tag.GetType() == typeof(Project.UserOption))
            {
                mnuHelperItemDelete.Visible = true;
                editToolStripMenuItem.Visible = true;
                mnuHelperTreeEditVirtualProperty.Visible = true;
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }

        private void mnuHelperItemDelete_Click(object sender, EventArgs e)
        {
            TreeListNode selectedNode = treeListAPIHelper.Selection[0];

            if (treeListAPIHelper.FocusedColumn.AbsoluteIndex > 0)
            {
                //DeleteHelperFunction((string)selectedNode[1]);
                DeleteHelperFunction((Project.FunctionInfo)selectedNode.Tag);
                return;
            }
            else if (selectedNode.Tag is Project.UserOption)
            {
                Project.Instance.DeleteUserOption(((Project.UserOption)selectedNode.Tag).VariableName);
                treeListAPIHelper.Nodes.Remove(selectedNode);
            }
            else if (selectedNode.Tag is Project.FunctionInfo)
            {
                Project.Instance.DeleteFunction((Project.FunctionInfo)selectedNode.Tag);
                treeListAPIHelper.Nodes.Remove(selectedNode);
            }
        }

        private void DeleteHelperFunction(Project.FunctionInfo function)
        {
            if (MessageBox.Show(this, string.Format("Delete {0}?", function.Name), "Delete Function", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                // Delete function deletes the DefaultValueFunction as well, if it exists.
                Project.Instance.DeleteFunction(function);
            }
            //Populate();
            treeListAPIHelper.Invalidate();
        }

        private void mnuItemNewProperty_Click(object sender, EventArgs e)
        {
			try
			{
				Controller.Instance.MainForm.Cursor = Cursors.WaitCursor;
				mnuTreeNode.Visible = false;
				Controller.Instance.MainForm.Refresh();
				Application.DoEvents();
				Type type = (Type)treeListAPI.FocusedNode.Tag;
				frmOptionEdit form = new frmOptionEdit("General", type);
				Controller.Instance.MainForm.Cursor = Cursors.Default;

				if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
				{
					try
					{
						treeListAPI.BeginUnboundLoad();
						AddTreeListNode(treeListAPI, treeListAPI.FocusedNode, form.CurrentOption, form.CurrentOption.VariableName, form.CurrentOption.Description, "", "", Images.Property, form.CurrentOption.ResetPerSession);
					}
					finally
					{
						treeListAPI.EndUnboundLoad();
					}
				}
			}
			finally
			{
				Controller.Instance.MainForm.Cursor = Cursors.Default;
			}
        }

        private void mnuHelperItemNewVirtualProperty_Click(object sender, EventArgs e)
        {
			try
			{
				Controller.Instance.MainForm.Cursor = Cursors.WaitCursor;
				mnuHelperTreeEditVirtualProperty.Visible = false;
				Controller.Instance.MainForm.Refresh();
				Type type = (Type)treeListAPIHelper.FocusedNode.Tag;
				frmOptionEdit form = new frmOptionEdit("General", type);
				Controller.Instance.MainForm.Cursor = Cursors.Default;

				if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
				{
					try
					{
						treeListAPIHelper.BeginUnboundLoad();
						TreeListNode userOptionNode = AddTreeListNode(treeListAPIHelper, treeListAPIHelper.FocusedNode, form.CurrentOption, form.CurrentOption.VariableName, form.CurrentOption.Description, "", "", Images.Property, form.CurrentOption.ResetPerSession);
						AddTreeListNode(treeListAPIHelper, userOptionNode, null, "DefaultValueFunction", "Sets the default value for this Virtual Property", "", "", Images.Method, form.CurrentOption.ResetPerSession);
						AddTreeListNode(treeListAPIHelper, userOptionNode, null, "ValidatorFunction", "Returns whether the value is valid or not.", "", "", Images.Method, form.CurrentOption.ResetPerSession);
						AddTreeListNode(treeListAPIHelper, userOptionNode, null, "DisplayToUserFunction", "Returns whether the virtual property should be displayed to the user.", "", "", Images.Method, form.CurrentOption.ResetPerSession);
					}
					finally
					{
						treeListAPIHelper.EndUnboundLoad();
					}
				}
			}
			finally
			{
				Controller.Instance.MainForm.Refresh();
				Controller.Instance.MainForm.Cursor = Cursors.Default;
			}
        }

		private void mnuItem_newExtensionMethod_Click(object sender, EventArgs e)
		{
			Controller.Instance.MainForm.Cursor = Cursors.WaitCursor;
			mnuItemNewExtensionMethod.Visible = false;
			Controller.Instance.MainForm.Refresh();

			Type type = (Type)treeListAPIHelper.FocusedNode.Tag;

			var function = new Project.FunctionInfo(true, type.FullName);
			function.AddParameter(new Project.ParamInfo("obj", type));
			function.Parameters[0].Modifiers = "this ";
			frmFunctionWizard wiz = new frmFunctionWizard(function, true );
			Controller.Instance.MainForm.Cursor = Cursors.Default;
			if (wiz.ShowDialog(this.ParentForm) == DialogResult.OK)
			{
                Project.Instance.AddFunction(function);
				treeListAPIHelper.BeginUnboundLoad();
				AddTreeListNode(treeListAPIHelper, treeListAPIHelper.FocusedNode, function, function.Name, function.Description, "", "", Images.Method, false);
				treeListAPIHelper.EndUnboundLoad();
                Controller.Instance.MainForm.ShowFunction(function, true, this);
			}
            Controller.Instance.MainForm.Refresh();
		}

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controller.Instance.MainForm.Cursor = Cursors.WaitCursor;
            mnuHelperTreeEditVirtualProperty.Visible = false;
            Controller.Instance.MainForm.Refresh();
            //Application.DoEvents();

            //Type type = (Type)treeListAPIHelper.FocusedNode.Tag;

            frmOptionEdit form = new frmOptionEdit((Project.UserOption)treeListAPIHelper.FocusedNode.Tag);
            Controller.Instance.MainForm.Cursor = Cursors.Default;

            if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
            {
                treeListAPIHelper.BeginUnboundLoad();
                treeListAPIHelper.FocusedNode.SetValue(0, form.CurrentOption.VariableName);
                treeListAPIHelper.FocusedNode.SetValue(1, form.CurrentOption.Description);
                treeListAPIHelper.FocusedNode.SetValue(2, form.CurrentOption.ResetPerSession);
                treeListAPIHelper.EndUnboundLoad();
            }
            Controller.Instance.MainForm.Refresh();
            //Controller.Instance.MainForm.navigationBar1.Invalidate();
            Controller.Instance.MainForm.Cursor = Cursors.Default;

        }

        private void treeListAPI_CustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
        {
            if (e.Column.AbsoluteIndex != 4)
            {
                return;
            }
            TreeListNode node = e.Node;

            if (node != null)
            {
                if (node.Tag != null && node.Tag.ToString() == "User Option")
                {
                    e.RepositoryItem = repositoryItemCheckEdit1;
                }
            }
        }

        private void treeListAPIHelper_CustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
        {
            if (e.Column.AbsoluteIndex != 2)
            {
                return;
            }
            TreeListNode node = e.Node;

            if (node != null && node.Tag != null)
            {
                if (UserOptionType.IsInstanceOfType(node.Tag))
                {
                    e.RepositoryItem = repositoryItemCheckEdit1;
                }
            }
        }

        private void ucUserOptionsAPI_Resize(object sender, EventArgs e)
        {
            treeListAPI.Height = (int)((this.ClientSize.Height - ucLabel1.Height) * TreelistApiHeightRatio);
        }

        private void expandableSplitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            TreelistApiHeightRatio = (double)treeListAPI.Height / (this.ClientSize.Height - ucLabel1.Height);
        }
    }
}
