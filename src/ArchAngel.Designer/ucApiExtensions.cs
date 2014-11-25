using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ArchAngel.Common.DesignerProject;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Designer.UI.PropertyGrids;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Wizards.NewProject;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using Slyce.Common;
using UserOption = ArchAngel.Common.DesignerProject.UserOption;

namespace ArchAngel.Designer
{
	public partial class ucApiExtensions : UserControl, RibbonBarContentItem
	{
		private const string ValidationTag = "Validation";
		private const string DefaultValueTag = "DefaultValue";
		private const string DisplayToUserTag = "DisplayToUser";
		private const string DefaultValueNodeName = "Default Value Function";
		private const string ValidatorFunctionNodeName = "Validation Function";
		private const string DisplayToUserFunctionNodeName = "Display To User Function";

		private readonly Dictionary<Type, Node> typeNodes = new Dictionary<Type, Node>();
		private readonly Dictionary<MethodInfo, Node> methodNodes = new Dictionary<MethodInfo, Node>();
		private readonly Dictionary<UserOption, Node> virtualPropertyNodes = new Dictionary<UserOption, Node>();
		private readonly ucFunction functionScreen;
		private readonly ElementStyle virtualPropertyNodeStyle = new ElementStyle();

		private ApiExtensionMethod currentExtentionMethod;
		private UserOption currentVirtualProperty;
		private FunctionTypes currentVirtualPropertyFunction;
		private RibbonBarController RibbonBarController;

		public ucApiExtensions()
		{
			InitializeComponent();

			functionScreen = new ucFunction();
			functionScreen.Dock = DockStyle.Fill;
			panelContent.Controls.Add(functionScreen);

			virtualPropertyNodeStyle.TextColor = Color.DarkSlateBlue;

			functionScreen.IsDirtyChanged += functionScreen_IsDirtyChanged;
			formUserOptionDetails.UserOptionNameChanged += propertyGridUserOption_UserOptionNameChanged;

			formUserOptionDetails.Visible = false;
		}

		public bool CanAddNewVirtualProperty()
		{
			Node selectedNode = treeAPI.SelectedNode;
			if (selectedNode == null) return false;

			Node currentNode = selectedNode;
			while (currentNode != null)
			{
				if (currentNode.Tag is Type)
				{
					return ExtensionAttributeHelper.CanAddVirtualProperties(currentNode.Tag as Type);
				}

				currentNode = currentNode.Parent;
			}

			return false;
		}

		public void OnSave()
		{
			SaveCurrentFunctionInfo();

			treeAPI.BeginUpdate();
			foreach (var userOption in virtualPropertyNodes.Keys)
			{
				SetValidatorNodeToDirty(userOption, false);
				SetDefaultValueNodeToDirty(userOption, false);
				SetDisplayToUserNodeToDirty(userOption, false);
			}
			treeAPI.EndUpdate();
		}

		public bool CanRemoveVirtualProperty()
		{
			Node selectedNode = treeAPI.SelectedNode;
			if (selectedNode == null) return false;

			Node currentNode = selectedNode;
			while (currentNode != null)
			{
				if (currentNode.Tag is UserOption)
				{
					return true;
				}

				currentNode = currentNode.Parent;
			}

			return false;
		}

		void functionScreen_IsDirtyChanged(object sender, EventArgs e)
		{
			if (currentExtentionMethod == null && currentVirtualProperty == null) return;

			if (currentExtentionMethod != null)
			{
				Node node = methodNodes[currentExtentionMethod.ExtendedMethod];
				SetNodeDirty(node, functionScreen.IsDirty);
			}
			else
			{
				switch (currentVirtualPropertyFunction)
				{
					case FunctionTypes.DefaultValue:
						SetDefaultValueNodeToDirty(currentVirtualProperty, functionScreen.IsDirty);
						break;
					case FunctionTypes.Validation:
						SetValidatorNodeToDirty(currentVirtualProperty, functionScreen.IsDirty);
						break;
					case FunctionTypes.DisplayToUser:
						SetDisplayToUserNodeToDirty(currentVirtualProperty, functionScreen.IsDirty);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		private void SetDefaultValueNodeToDirty(UserOption virtualProperty, bool dirty)
		{
			Node currentVPNode = virtualPropertyNodes[virtualProperty];
			var dfnode = FindFirstNodeNamed(DefaultValueNodeName, currentVPNode.Nodes);
			SetNodeDirty(dfnode, dirty);
		}

		private void SetDisplayToUserNodeToDirty(UserOption virtualProperty, bool dirty)
		{
			Node currentVPNode = virtualPropertyNodes[virtualProperty];
			var dtunode = FindFirstNodeNamed(DisplayToUserFunctionNodeName, currentVPNode.Nodes);
			SetNodeDirty(dtunode, dirty);
		}

		private void SetValidatorNodeToDirty(UserOption virtualProperty, bool dirty)
		{
			Node currentVPNode = virtualPropertyNodes[virtualProperty];
			var vnode = FindFirstNodeNamed(ValidatorFunctionNodeName, currentVPNode.Nodes);
			SetNodeDirty(vnode, dirty);
		}

		private void SetNodeDirty(Node node, bool dirty)
		{
			if (dirty)
				node.Text = "<b>" + node.Name + "</b>";
			else
				node.Text = node.Name;
		}

		public void Populate()
		{
			ClearTree();

			var extendableTypes = ExtensionAttributeHelper.GetAllExtendableTypes(Project.Instance.ReferencedAssemblies);
			treeAPI.BeginUpdate();
			foreach (Type type in extendableTypes)
			{
				if (typeNodes.ContainsKey(type)) continue;

				Node parent = FindOrCreateNamespaceNode(type);
				Node typeNode = CreateTypeNode(type, Project.Instance.GetVirtualPropertiesFor(type));

				parent.Nodes.Add(typeNode);
			}
			treeAPI.EndUpdate();
		}

		private Node CreateTypeNode(Type type, IEnumerable<UserOption> virtualProperties)
		{
			var node = new Node { Text = type.Name, Name = type.Name, Tag = type };
			typeNodes.Add(type, node);

			CreateExtensionMethodNodes(type, node);
			CreateVirtualPropertiesNodes(virtualProperties, node);

			return node;
		}

		private void CreateVirtualPropertiesNodes(IEnumerable<UserOption> properties, Node node)
		{
			foreach (var virtualProperty in properties)
			{
				Node vpNode = CreateVirtualPropertyNode(virtualProperty);

				node.Nodes.Add(vpNode);
			}
		}

		private Node CreateVirtualPropertyNode(UserOption virtualProperty)
		{
			var vpNode = new Node { Text = virtualProperty.VariableName, Name = virtualProperty.VariableName, Tag = virtualProperty };
			vpNode.Style = virtualPropertyNodeStyle;

			var defaultValueNode = new Node
									{
										Text = "Default Value Function",
										Name = DefaultValueNodeName,
										Tag = DefaultValueTag
									};

			var validatorNode = new Node
									{
										Text = "Validator Function",
										Name = ValidatorFunctionNodeName,
										Tag = ValidationTag
									};

			var displayToUserNode = new Node
								{
									Text = "Display To User Function",
									Name = DisplayToUserFunctionNodeName,
									Tag = DisplayToUserTag
								};

			vpNode.Nodes.Add(defaultValueNode);
			vpNode.Nodes.Add(validatorNode);
			vpNode.Nodes.Add(displayToUserNode);
			virtualPropertyNodes.Add(virtualProperty, vpNode);
			return vpNode;
		}

		private void CreateExtensionMethodNodes(Type type, Node node)
		{
			foreach (var method in ExtensionAttributeHelper.GetExtendableMethods(type))
			{
				var methodNode = new Node { Text = method.Name, Name = method.Name, Tag = method };

				string description = ExtensionAttributeHelper.GetExtensionDescription(method);

				if (string.IsNullOrEmpty(description) == false)
				{
					SetToolTip(methodNode, description);
				}
				node.Nodes.Add(methodNode);

				methodNodes.Add(method, methodNode);
			}
		}

		private void SetToolTip(IComponent methodNode, string description)
		{
			var info = new SuperTooltipInfo
						{
							BodyText = description,
						};
			superTooltip.SetSuperTooltip(methodNode, info);
		}

		private Node FindFirstNodeNamed(string nodeName, NodeCollection collection)
		{
			Node[] nodes = collection.Find(nodeName, false);
			return nodes.Length > 0 ? nodes[0] : null;
		}

		private Node FindOrCreateNamespaceNode(Type type)
		{
			string ns = type.Namespace;

			string assemblyName = type.Assembly.GetName().Name;

			if (ns.Length > assemblyName.Length)
			{
				// the +1 is to get rid of the next .
				ns = ns.Substring(assemblyName.Length + 1);
			}
			NodeCollection collection = treeAPI.Nodes;
			var parentNamespaceNode = FindOrCreateNamespaceNode(assemblyName, collection);

			return FindOrCreateNamespaceNodeSplit(ns, parentNamespaceNode.Nodes);
		}

		private Node FindOrCreateNamespaceNodeSplit(string ns, NodeCollection collection)
		{
			int index = ns.IndexOf(".");
			if (index > -1)
			{
				string @namespace = ns.Substring(0, index);
				var parent = FindOrCreateNamespaceNode(@namespace, collection);
				return FindOrCreateNamespaceNodeSplit(ns.Substring(index + 1), parent.Nodes);
			}
			return FindOrCreateNamespaceNode(ns, collection);
		}

		private Node FindOrCreateNamespaceNode(string ns, NodeCollection collection)
		{
			var node = FindFirstNodeNamed(ns, collection);

			if (node == null)
			{
				node = new Node { Text = ns, Name = ns };
				collection.Add(node);
			}

			return node;
		}

		private void ClearTree()
		{
			treeAPI.Nodes.Clear();
			methodNodes.Clear();
			typeNodes.Clear();
			virtualPropertyNodes.Clear();
		}

		public void Clear()
		{
			ClearTree();
		}

		private void ClearPanel()
		{
			panelContent.Visible = false;
			currentExtentionMethod = null;
		}

		private void SetPanelToMethod(MethodInfo info)
		{
			SaveCurrentFunctionInfo();

			Utility.SuspendPainting(panelContent);

			ClearPanel();

			// Try find Api Extension in the current project
			if (Project.Instance.TryGetApiExtensionFor(info, out currentExtentionMethod) == false)
			{
				currentExtentionMethod = new ApiExtensionMethod(info);
				currentExtentionMethod.DefaultCode = Project.Instance.GetDefaultFunctionBodyFor(info);
			}

			functionScreen.CurrentFunction = currentExtentionMethod.FunctionInfo;
			functionScreen.AllowOverride = true;
			functionScreen.OverrideFunctionChecked = currentExtentionMethod.HasOverride;

			functionScreen.Populate();

			panelContent.Visible = true;

			Utility.ResumePainting(panelContent);

			formUserOptionDetails.Visible = false;
		}

		private void SetPanelToProperty(UserOption option, FunctionTypes type)
		{
			SaveCurrentFunctionInfo();

			Utility.SuspendPainting(panelContent);

			ClearPanel();

			if (type == FunctionTypes.Validation)
			{
				functionScreen.CurrentFunction = option.GetValidatorFunction();
			}
			else if (type == FunctionTypes.DefaultValue)
			{
				functionScreen.CurrentFunction = option.GetDefaultValueFunction();
			}
			else if (type == FunctionTypes.DisplayToUser)
			{
				functionScreen.CurrentFunction = option.GetDisplayToUserFunction();
			}

			currentVirtualProperty = option;
			currentVirtualPropertyFunction = type;

			functionScreen.Populate();

			panelContent.Visible = true;

			Utility.ResumePainting(panelContent);

			formUserOptionDetails.Visible = true;
			formUserOptionDetails.UserOption = option;
		}

		private void SaveCurrentFunctionInfo()
		{
			if (functionScreen.IsDirty == false) return;
			if (functionScreen.OverrideFunctionChecked == false) return;

			if (currentExtentionMethod != null)
			{
				currentExtentionMethod.OverridingFunctionBody = functionScreen.GetFunctionBody();
				if (Project.Instance.ContainsApiExtension(currentExtentionMethod) == false)
					Project.Instance.AddApiExtension(currentExtentionMethod);
			}
			else if (currentVirtualProperty != null)
			{
				switch (currentVirtualPropertyFunction)
				{
					case FunctionTypes.DefaultValue:
						currentVirtualProperty.DefaultValueFunctionBody = functionScreen.GetFunctionBody();
						break;
					case FunctionTypes.Validation:
						currentVirtualProperty.ValidatorFunctionBody = functionScreen.GetFunctionBody();
						break;
					case FunctionTypes.DisplayToUser:
						currentVirtualProperty.DisplayToUserFunctionBody = functionScreen.GetFunctionBody();
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		private void treeAPI_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (treeAPI.SelectedIndex == -1)
				ClearPanel();

			Node selectedNode = treeAPI.SelectedNode;

			if (selectedNode == null) return;

			if (selectedNode.Tag is MethodInfo)
			{
				SetPanelToMethod(selectedNode.Tag as MethodInfo);
			}
			else if (selectedNode.Tag is UserOption)
			{
				formUserOptionDetails.Visible = true;
				formUserOptionDetails.UserOption = selectedNode.Tag as UserOption;
			}
			else if (selectedNode.Parent != null && selectedNode.Parent.Tag is UserOption)
			{
				UserOption option = (UserOption)selectedNode.Parent.Tag;

				if (ReferenceEquals(selectedNode.Tag, ValidationTag))
				{
					SetPanelToProperty(option, FunctionTypes.Validation);
				}
				else if (ReferenceEquals(selectedNode.Tag, DefaultValueTag))
				{
					SetPanelToProperty(option, FunctionTypes.DefaultValue);
				}
				else if (ReferenceEquals(selectedNode.Tag, DisplayToUserTag))
				{
					SetPanelToProperty(option, FunctionTypes.DisplayToUser);
				}
			}

			RibbonBarController.RefreshButtonStatus(this);
		}

		void propertyGridUserOption_UserOptionNameChanged(object sender, TextChangedEventArgs e)
		{
			UserOption option = formUserOptionDetails.UserOption;
			if (!virtualPropertyNodes.ContainsKey(option)) return;

			Node node = virtualPropertyNodes[option];
			node.Name = node.Text = option.VariableName;
		}

		private void AddNewVirtualProperty_Clicked(object sender, EventArgs e)
		{
			if (CanAddNewVirtualProperty() == false)
			{
				RibbonBarController.RefreshButtonStatus(this);
				return;
			}

			Type currentType = null;
			Node currentNode = treeAPI.SelectedNode;
			while (currentNode != null)
			{
				if (currentNode.Tag is Type)
				{
					currentType = (Type)currentNode.Tag;
					break;
				}

				currentNode = currentNode.Parent;
			}
			if (currentType == null)
				throw new Exception("Cannot add a virtual property without selecting a type to add it to.");

			UserOption uo = new UserOption("NewVirtualProperty", "", "<Display Text>", "<Description>", null, currentType, true);
			Project.Instance.AddUserOption(uo);

			Node node = CreateVirtualPropertyNode(uo);
			currentNode.Nodes.Add(node);
			treeAPI.SelectedNode = node;
		}

		private void RemoveVirtualProperty_Clicked(object sender, EventArgs e)
		{
			if (CanRemoveVirtualProperty() == false)
			{
				RibbonBarController.RefreshButtonStatus(this);
				return;
			}

			UserOption userOption = null;
			Node currentNode = treeAPI.SelectedNode;
			while (currentNode != null)
			{
				if (currentNode.Tag is UserOption)
				{
					userOption = (UserOption)currentNode.Tag;
					break;
				}

				currentNode = currentNode.Parent;
			}
			if (userOption == null)
				throw new Exception("Cannot remove virtual property - cannot find it in the tree list.");

			Project.Instance.RemoveUserOption(userOption);
			currentNode.Remove();
			ClearPanel();
		}

		private void buttonAddLoadFunction_Click(object sender, EventArgs e)
		{
			FunctionInfo function = new FunctionInfo(TemplateHelper.LoadFunctionName, null, "", false,
													 SyntaxEditorHelper.ScriptLanguageTypes.CSharp,
													 "Loads any additional information from an existing Workbench project",
													 "C#", "General");

			function.AddParameter(new ParamInfo("outputFolder", typeof(string)));
			function.AddParameter(new ParamInfo("provider", typeof(IEnumerable<ProviderInfo>)));
			function.AddParameter(new ParamInfo("extraData", typeof(TemplateData)));

			Project.Instance.AddFunction(function);

			Controller.Instance.MainForm.ShowFunction(function, this);
			RibbonBarController.RefreshButtonStatus(this);
		}

		private bool CanAddLoadFunction()
		{
			return !Project.Instance.Functions.Any(f => f.Name == TemplateHelper.LoadFunctionName);
		}

		private void buttonAddProjectScreensFunction_Click(object sender, EventArgs e)
		{
			FunctionInfo function = new FunctionInfo(TemplateHelper.CustomNewProjectScreensFunctionName, null, "", false,
													 SyntaxEditorHelper.ScriptLanguageTypes.CSharp,
													 "Creates a list of custom screens that should be shown during the creation of new Workbench projects.",
													 "C#", "General");

			function.AddParameter(new ParamInfo("screens", typeof(List<INewProjectScreen>), "out"));

			Project.Instance.AddFunction(function);

			Controller.Instance.MainForm.ShowFunction(function, this);
			RibbonBarController.RefreshButtonStatus(this);
		}

		private bool CanAddProjectScreensFunction()
		{
			return !Project.Instance.Functions.Any(f => f.Name == TemplateHelper.CustomNewProjectScreensFunctionName);
		}

		private void buttonAddPreGenerationFunction_Click(object sender, EventArgs e)
		{
			FunctionInfo function = new FunctionInfo(TemplateHelper.PreGenerationModelProcessingFunctionName, null, "", false,
													 SyntaxEditorHelper.ScriptLanguageTypes.CSharp,
													 "Initialises the Provider from the template before generation of the files",
													 "C#", "General");

			function.AddParameter(new ParamInfo("providerInfo", typeof(ProviderInfo)));
			function.AddParameter(new ParamInfo("data", typeof(PreGenerationData)));

			Project.Instance.AddFunction(function);

			Controller.Instance.MainForm.ShowFunction(function, this);
			RibbonBarController.RefreshButtonStatus(this);
		}

		private bool CanAddPreGenerationFunction()
		{
			return !Project.Instance.Functions.Any(f => f.Name == TemplateHelper.PreGenerationModelProcessingFunctionName);
		}

		public void AddRibbonBarButtons(RibbonBarBuilder builder)
		{
			RibbonBarController = builder.Controller;

			var addVPButton = builder.CreateButton()
				.SetText("Add Virtual Property")
				.SetIsEnabledHandler(CanAddNewVirtualProperty)
				.SetToolTip("Add New Virtual Property to Type")
				.AddClickEventHandler(AddNewVirtualProperty_Clicked);

			var removeVPButton = builder.CreateButton()
				.SetText("Remove Virtual Property")
				.SetIsEnabledHandler(CanRemoveVirtualProperty)
				.SetToolTip("Deletes the Virtual Property from the Project")
				.AddClickEventHandler(RemoveVirtualProperty_Clicked);

			var vpBar = builder.CreateBar()
				.SetName("Virtual Properties")
				.SetOrientation(Orientation.Horizontal)
				.AddButton(addVPButton)
				.AddButton(removeVPButton);

			builder.AddRibbonBar(vpBar);

			var bar = builder.CreateBar()
				.SetName("Extension Actions");

			var addLoadFunctionButton = builder.CreateButton()
				.SetText("Add OnLoad Action")
				.AddClickEventHandler(buttonAddLoadFunction_Click)
				.SetIsEnabledHandler(CanAddLoadFunction);

			var addNewProjectScreensFunctionButton = builder.CreateButton()
				.SetText("Add New Project Screens Action")
				.AddClickEventHandler(buttonAddProjectScreensFunction_Click)
				.SetIsEnabledHandler(CanAddProjectScreensFunction);

			var addPreGenerationFunctionButton = builder.CreateButton()
				.SetText("Add Pre Generation Action")
				.AddClickEventHandler(buttonAddPreGenerationFunction_Click)
				.SetIsEnabledHandler(CanAddPreGenerationFunction);

			bar.AddButton(addLoadFunctionButton);
			bar.AddButton(addNewProjectScreensFunctionButton);
			bar.AddButton(addPreGenerationFunctionButton);
			builder.AddRibbonBar(bar);
		}
	}
}
