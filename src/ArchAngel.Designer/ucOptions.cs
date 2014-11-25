using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Common.DesignerProject;
using ArchAngel.Designer.UI.PropertyGrids;
using DevComponents.AdvTree;

namespace ArchAngel.Designer
{
	public partial class ucOptions : UserControl
	{
		private const string ValidationTag = "Validation";
		private const string DefaultValueTag = "DefaultValue";
		private const string DisplayToUserTag = "DisplayToUser";
		private const string DefaultValueFunctionNodeName = "Default Value Function";
		private const string DisplayToUserFunctionNodeName = "Display To User Function";
		private const string ValidatorFunctionNodeName = "Validation Function";

		// ReSharper disable UnusedMember.Local
		private enum Images
		{
			Class = 0,
			Property = 1,
			Namespace = 2,
			Method = 3
		}
		// ReSharper restore UnusedMember.Local

		private readonly Dictionary<UserOption, Node> userOptionNodes = new Dictionary<UserOption, Node>();
		private readonly Dictionary<string, Node> userOptionCategoryNodes = new Dictionary<string, Node>();
		private bool IgnoreSelectionChanged;
		private readonly ucFunction currentFunctionScreen;
		private UserOption currentUserOption;
		private FunctionTypes currentFunctionType;

		public ucOptions()
		{
			InitializeComponent();

			currentFunctionScreen = new ucFunction();
			currentFunctionScreen.Visible = false;
			panelContent.Controls.Add(currentFunctionScreen);
			currentFunctionScreen.Dock = DockStyle.Fill;

			currentFunctionScreen.IsDirtyChanged += currentFunctionScreen_IsDirtyChanged;
			currentFunctionScreen.ResetDefaultCode += currentFunctionScreen_ResetDefaultCode;

			propertyGridUserOption.UserOptionNameChanged += propertyGridUserOption_UserOptionNameChanged;
			propertyGridUserOption.UserOptionCategoryChanged += propertyGridUserOption_UserOptionCategoryChanged;
		}

		void currentFunctionScreen_ResetDefaultCode(object sender, EventArgs e)
		{
			if (currentUserOption == null) return;

			switch (currentFunctionType)
			{
				case FunctionTypes.DefaultValue:
					currentFunctionScreen.CurrentFunction.Body =
						currentUserOption.DefaultValueFunctionBody = UserOption.Default_DefaultValueFunctionBody;
					SetDefaultValueToDirty(currentUserOption, true);
					break;
				case FunctionTypes.Validation:
					currentFunctionScreen.CurrentFunction.Body =
						currentUserOption.ValidatorFunctionBody = UserOption.Default_ValidatorFunctionBody;
					SetValidatorToDirty(currentUserOption, true);
					break;
				case FunctionTypes.DisplayToUser:
					currentFunctionScreen.CurrentFunction.Body =
						currentUserOption.DisplayToUserFunctionBody = UserOption.Default_DisplayToUserFunctionBody;
					SetDisplayToUserToDirty(currentUserOption, true);
					break;
			}

			currentFunctionScreen.Populate();
			Project.Instance.IsDirty = true;
		}

		void currentFunctionScreen_IsDirtyChanged(object sender, EventArgs e)
		{
			if (currentUserOption == null) return;

			if (currentFunctionScreen.IsDirty)
			{
				Project.Instance.IsDirty = true;
			}

			switch (currentFunctionType)
			{
				case FunctionTypes.DefaultValue:
					SetDefaultValueToDirty(currentUserOption, currentFunctionScreen.IsDirty);
					break;
				case FunctionTypes.Validation:
					SetValidatorToDirty(currentUserOption, currentFunctionScreen.IsDirty);
					break;
				case FunctionTypes.DisplayToUser:
					SetDisplayToUserToDirty(currentUserOption, currentFunctionScreen.IsDirty);
					break;
			}
		}

		private void SetDefaultValueToDirty(UserOption option, bool dirty)
		{
			SetNodeToDirty(option, DefaultValueFunctionNodeName, dirty);
		}

		private void SetValidatorToDirty(UserOption option, bool dirty)
		{
			SetNodeToDirty(option, ValidatorFunctionNodeName, dirty);
		}

		private void SetDisplayToUserToDirty(UserOption option, bool dirty)
		{
			SetNodeToDirty(option, DisplayToUserFunctionNodeName, dirty);
		}

		private void SetNodeToDirty(UserOption option, string nodeName, bool dirty)
		{
			Node userOptionNode;
			if (!userOptionNodes.TryGetValue(option, out userOptionNode)) return;

			Node node = FindFirstNamed(nodeName, userOptionNode.Nodes);

			if (dirty)
				node.Text = string.Format("<b>{0} *</b>", nodeName);
			else
				node.Text = nodeName;
		}

		private void propertyGridUserOption_UserOptionCategoryChanged(object sender, TextChangedEventArgs e)
		{
			IgnoreSelectionChanged = true;

			UserOption option = propertyGridUserOption.UserOption;

			if (!userOptionCategoryNodes.ContainsKey(e.OldText)) return;
			if (!userOptionNodes.ContainsKey(option)) return;

			Node optionNode = userOptionNodes[option];

			Node currentCatNode = userOptionCategoryNodes[e.OldText];

			if (currentCatNode.Nodes.Count == 1)
			{
				if (userOptionCategoryNodes.ContainsKey(option.Category))
				{
					currentCatNode.Nodes.Remove(optionNode);
					currentCatNode.Remove();
					userOptionCategoryNodes.Remove(e.OldText);

					Node newCatNode = FindOrCreateCategoryNode(option.Category);
					newCatNode.Nodes.Add(optionNode);
				}
				else
				{
					userOptionCategoryNodes.Remove(e.OldText);

					currentCatNode.Name = option.Category;
					currentCatNode.Text = FormatCategoryName(option.Category);

					userOptionCategoryNodes.Add(option.Category, currentCatNode);
				}
			}
			else
			{
				// Shift the option node to its new category node.
				Node newCatNode = FindOrCreateCategoryNode(option.Category);
				currentCatNode.Nodes.Remove(optionNode);
				newCatNode.Nodes.Add(optionNode);
			}

			Project.Instance.IsDirty = true;

			optionNode.EnsureVisible();
			treeOptions.SelectedNode = optionNode;
			IgnoreSelectionChanged = false;
		}

		void propertyGridUserOption_UserOptionNameChanged(object sender, TextChangedEventArgs e)
		{
			UserOption option = propertyGridUserOption.UserOption;
			if (!userOptionNodes.ContainsKey(option)) return;

			Node node = userOptionNodes[option];
			node.Name = node.Text = option.VariableName;
		}

		private void ucOptions_Load(object sender, EventArgs e)
		{
			barOptions.Text = "Options";
			barDetails.Text = "Option Details";
			dockContainerItem1.Text = "Option Details";
			Clear();
			Populate();
		}

		public void Populate()
		{
			treeOptions.BeginUpdate();
			ClearTree();

			foreach (UserOption userOption in Project.Instance.UserOptions)
			{
				Node categoryNode = FindOrCreateCategoryNode(userOption.Category);
				if (userOption.IteratorType != null)
				{
					continue;
				}
				Node optionNode = CreateUserOptionNode(userOption);
				categoryNode.Nodes.Add(optionNode);
			}

			treeOptions.CollapseAll();
			treeOptions.EndUpdate();
		}

		public void Clear()
		{
			ClearTree();
			propertyGridUserOption.Visible = false;
		}

		private void ClearTree()
		{
			treeOptions.Nodes.Clear();
			userOptionNodes.Clear();
			userOptionCategoryNodes.Clear();
		}

		private Node FindFirstNamed(string name, NodeCollection nodes)
		{
			Node[] found = nodes.Find(name, false);
			return found.Length > 0 ? found[0] : null;
		}

		internal void CreateNewUserOption()
		{
			UserOption newUserOption =
				new UserOption("NewOption", "General", "<Text displayed to user>", "<Default tooltip text>", new string[0], null, false);
			Node userOptionNode = CreateUserOptionNode(newUserOption);

			// Add the user options node to the category node.
			FindOrCreateCategoryNode(newUserOption.Category).Nodes.Add(userOptionNode);

			// Add to the node collection
			userOptionNodes.Add(newUserOption, userOptionNode);

			userOptionNode.EnsureVisible();
			treeOptions.SelectedNode = userOptionNode;

			Project.Instance.AddUserOption(newUserOption);

			Project.Instance.IsDirty = true;
		}

		private Node CreateUserOptionNode(UserOption newUserOption)
		{
			var node = new Node
			{
				Text = newUserOption.VariableName,
				Name = newUserOption.VariableName,
				Tag = newUserOption
			};

			var defaultValueFunctionNode = new Node
			{
				Text = "Default Value Function",
				Name = DefaultValueFunctionNodeName,
				Tag = DefaultValueTag
			};

			var validationFunctionNode = new Node
			{
				Text = "Validation Function",
				Name = ValidatorFunctionNodeName,
				Tag = ValidationTag
			};

			var displayToUserFunctionNode = new Node
			{
				Text = "Display To User Function",
				Name = DisplayToUserFunctionNodeName,
				Tag = DisplayToUserTag
			};

			node.Nodes.Add(defaultValueFunctionNode);
			node.Nodes.Add(validationFunctionNode);
			node.Nodes.Add(displayToUserFunctionNode);

			return node;
		}

		private string FormatCategoryName(string category)
		{
			return string.Format("<b>{0}</b>", category);
		}

		private Node FindOrCreateCategoryNode(string categoryName)
		{
			// Does this category exist in the treelist?
			Node categoryNode = FindFirstNamed(categoryName, treeOptions.Nodes);

			if (categoryNode == null)
			{
				categoryNode = new Node
								{
									Text = FormatCategoryName(categoryName),
									Name = categoryName,
									Tag = "Category",
									ImageIndex = (int)Images.Namespace
								};

				treeOptions.Nodes.Add(categoryNode);

				userOptionCategoryNodes.Add(categoryName, categoryNode);
			}
			return categoryNode;
		}

		private void treeOptions_SelectionChanged(object sender, EventArgs e)
		{
			if (IgnoreSelectionChanged)
				return;

			StoreCurrentFunctionText();

			currentUserOption = null;

			if (treeOptions.SelectedNode == null)
				return;

			object tag = treeOptions.SelectedNode.Tag;
			if (tag is UserOption)
			{
				propertyGridUserOption.UserOption = (UserOption)tag;
				propertyGridUserOption.Visible = true;
			}
			else if (tag is string && (string)tag == DefaultValueTag)
			{
				DisplayDefaultValueFunctionFor(treeOptions.SelectedNode.Parent.Tag as UserOption);
			}
			else if (tag is string && (string)tag == ValidationTag)
			{
				DisplayValidationFunctionFor(treeOptions.SelectedNode.Parent.Tag as UserOption);
			}
			else if (tag is string && (string)tag == DisplayToUserTag)
			{
				DisplayDisplayToUserFunctionFor(treeOptions.SelectedNode.Parent.Tag as UserOption);
			}
			else
			{
				propertyGridUserOption.Visible = false;
			}
		}

		public void OnSave()
		{
			StoreCurrentFunctionText();

			treeOptions.BeginUpdate();
			foreach (var userOption in userOptionNodes.Keys)
			{
				SetValidatorToDirty(userOption, false);
				SetDefaultValueToDirty(userOption, false);
				SetDisplayToUserToDirty(userOption, false);
			}
			treeOptions.EndUpdate();
		}

		private void StoreCurrentFunctionText()
		{
			if (currentUserOption != null)
			{
				string functionBody = currentFunctionScreen.GetFunctionBody();

				switch (currentFunctionType)
				{
					case FunctionTypes.DefaultValue:
						currentUserOption.DefaultValueFunctionBody = functionBody;
						break;
					case FunctionTypes.Validation:
						currentUserOption.ValidatorFunctionBody = functionBody;
						break;
					case FunctionTypes.DisplayToUser:
						currentUserOption.DisplayToUserFunctionBody = functionBody;
						break;
				}
			}
		}

		private void DisplayValidationFunctionFor(UserOption option)
		{
			currentFunctionScreen.CurrentFunction = option.GetValidatorFunction();
			currentFunctionScreen.AllowOverride = true;
			currentFunctionScreen.OverrideFunctionChecked = option.IsValidationFunctionOverridden();
			currentFunctionScreen.Visible = true;
			currentFunctionScreen.Populate();

			currentUserOption = option;
			currentFunctionType = FunctionTypes.Validation;
		}

		private void DisplayDisplayToUserFunctionFor(UserOption option)
		{
			currentFunctionScreen.CurrentFunction = option.GetDisplayToUserFunction();
			currentFunctionScreen.AllowOverride = true;
			currentFunctionScreen.OverrideFunctionChecked = option.IsDisplayToUserFunctionOverridden();
			currentFunctionScreen.Visible = true;
			currentFunctionScreen.Populate();

			currentUserOption = option;
			currentFunctionType = FunctionTypes.DisplayToUser;
		}

		private void DisplayDefaultValueFunctionFor(UserOption option)
		{
			currentFunctionScreen.CurrentFunction = option.GetDefaultValueFunction();
			currentFunctionScreen.AllowOverride = true;
			currentFunctionScreen.OverrideFunctionChecked = option.IsDefaultValueFunctionOverridden();
			currentFunctionScreen.Visible = true;
			currentFunctionScreen.Populate();

			currentUserOption = option;
			currentFunctionType = FunctionTypes.DefaultValue;
		}
	}
}
