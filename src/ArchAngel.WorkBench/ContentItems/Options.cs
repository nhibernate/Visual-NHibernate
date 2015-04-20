using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Interfaces.NHibernateEnums;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Slyce.Common;

namespace ArchAngel.Workbench.ContentItems
{
	public partial class Options : Interfaces.Controls.ContentItems.ContentItem
	{
		private static int CountOfLoads;
		private Panel messagePanel;

		public Options()
		{
			InitializeComponent();
			BackColor = Color.FromArgb(40, 40, 40);

			if (Utility.InDesignMode) { return; }

			Populate();

			if (CountOfLoads == 0)
			{
				Controller.Instance.OnTemplateLoaded += Project_OnTemplateLoaded;
				Controller.Instance.OnProjectLoaded += Project_OnProjectLoaded;
			}
			CountOfLoads++;
			superTabControl1.ControlBox.MenuBox.AutoHide = true;//.Visible = false;
		}

		public override void Clear()
		{
			Populate();
		}

		private void Project_OnTemplateLoaded()
		{
			//ShowOptions();
			//Controller.Instance.LoadOptions();
		}

		void Project_OnProjectLoaded()
		{
			ShowOptions();
			Controller.Instance.LoadOptions();
		}

		private void Populate()
		{
			HasPrev = true;
			HasNext = true;
			HelpPage = "Workbench_Screen_Options.htm";
			Title = "Options";
			PageDescription = "Specify what options you want the generation engine to use.";

			messagePanel = new Panel();
			messagePanel.Visible = false;
			messagePanel.Dock = DockStyle.Fill;
			messagePanel.BringToFront();
			Controls.Add(messagePanel);

			// Settings
			ShowOptions();
			Controller.Instance.LoadOptions();
		}

		public override bool Back()
		{
			return true;
		}

		public override bool Next()
		{
			return true;
		}

		public void LoadOptions(XmlDocument xmlDocument)
		{
			if (!File.Exists(Controller.Instance.CurrentProject.ProjectSettings.TemplateFileName))
			{
				messagePanel.Visible = true;
				messagePanel.Text = "No template has been loaded, so there are no options to display.";
				return;
			}
			messagePanel.Visible = false;

			bool oldPopulatingValue = Controller.Instance.BusyPopulating;
			Controller.Instance.BusyPopulating = true;

			for (int i = 0; i < superTabControl1.Tabs.Count; i++)
			{
				SuperTabItem tabPage = (SuperTabItem)superTabControl1.Tabs[i];
				SuperTabControlPanel panel = (SuperTabControlPanel)tabPage.AttachedControl;

				foreach (Control control in panel.Controls[0].Controls)
				{
					if (control.Name.IndexOf("controlOption_") != 0)
						continue;

					var option = (IOption)control.Tag;
					string name = XmlConvert.EncodeName(tabPage.Text.Replace(" ", "_"));
					XmlNode xmlNodeVariable = xmlDocument.SelectSingleNode("/Options/" + name + "/" + XmlConvert.EncodeName(option.VariableName));
					var correspondingOption = Controller.Instance.CurrentProject.FindOption(option.VariableName, option.IteratorName);

					if (xmlNodeVariable == null)
						continue;

					if (option.VarType == typeof(string) || option.VarType == typeof(int))
					{
						TextBox textBox = (TextBox)control;

						if (correspondingOption.ResetPerSession)
						{
							object[] parameters = new object[0];
							textBox.Text = (string)SharedData.CurrentProject.CallTemplateFunction(correspondingOption.DefaultValue, ref parameters);
						}
						else
							textBox.Text = xmlNodeVariable.InnerXml;
					}
					else if (option.VarType == typeof(bool))
					{
						CheckBox checkBox = (CheckBox)control;

						if (correspondingOption.ResetPerSession)
						{
							object[] parameters = new object[0];
							checkBox.Checked = (bool)SharedData.CurrentProject.CallTemplateFunction(correspondingOption.DefaultValue, ref parameters);
						}
						else
							checkBox.Checked = Convert.ToBoolean(xmlNodeVariable.InnerXml);
					}
					else if (option.VarType == typeof(bool?))
					{
						ComboBox comboBox = (ComboBox)control;

						if (correspondingOption.ResetPerSession)
						{
							object[] parameters = new object[0];
							comboBox.SelectedItem = SharedData.CurrentProject.CallTemplateFunction(correspondingOption.DefaultValue, ref parameters);
						}
						else
							comboBox.Text = xmlNodeVariable.InnerXml;
					}
					else if (option.VarType == typeof(int?))
					{
						TextBox textBox = (TextBox)control;

						if (correspondingOption.ResetPerSession)
						{
							object[] parameters = new object[0];
							textBox.Text = (string)SharedData.CurrentProject.CallTemplateFunction(correspondingOption.DefaultValue, ref parameters);
						}
						else
							textBox.Text = xmlNodeVariable.InnerXml;
					}
					else if (option.VarType == typeof(Enum))
					{
						ComboBox comboBox = (ComboBox)control;

						if (correspondingOption.ResetPerSession)
						{
							object[] parameters = new object[0];
							comboBox.SelectedItem = SharedData.CurrentProject.CallTemplateFunction(correspondingOption.DefaultValue, ref parameters);
						}
						else
							comboBox.Text = xmlNodeVariable.InnerXml;
					}
					else if (option.VarType.IsEnum)
					{
						ComboBox comboBox = (ComboBox)control;

						if (correspondingOption.ResetPerSession)
						{
							object[] parameters = new object[0];
							object selectedValue = SharedData.CurrentProject.CallTemplateFunction(correspondingOption.DefaultValue, ref parameters);
							comboBox.SelectedItem = selectedValue;
						}
						else
						{
							object enumValue;

							if (string.IsNullOrEmpty(xmlNodeVariable.InnerXml))
								enumValue = ExtensionAttributeHelper.GetDefaultEnumValue(option.VarType);
							else
								enumValue = Enum.Parse(option.VarType, xmlNodeVariable.InnerXml, true);

							comboBox.SelectedItem = enumValue;
						}
					}
					else if (option.VarType == typeof(ArchAngel.Interfaces.SourceCodeType) || option.VarType == typeof(ArchAngel.Interfaces.SourceCodeMultiLineType))
					{
						ActiproSoftware.SyntaxEditor.SyntaxEditor textBox = (ActiproSoftware.SyntaxEditor.SyntaxEditor)control;

						if (correspondingOption.ResetPerSession)
						{
							object[] parameters = new object[0];
							textBox.Text = (string)SharedData.CurrentProject.CallTemplateFunction(correspondingOption.DefaultValue, ref parameters);
						}
						else
							textBox.Text = xmlNodeVariable.InnerXml;
					}
					else
						throw new NotImplementedException("Not coded yet: +" + option.VarType);
				}
			}
			Controller.Instance.BusyPopulating = oldPopulatingValue;
		}

		public void SaveOptions(string fileName)
		{
			SaveOptionsForms();
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNodeOption = xmlDocument.AppendChild(xmlDocument.CreateElement("Options"));
			bool optionValuesAreValid = true;

			for (int i = 0; i < superTabControl1.Tabs.Count; i++)
			{
				SuperTabItem tabPage = (SuperTabItem)superTabControl1.Tabs[i];
				SuperTabControlPanel panel = (SuperTabControlPanel)tabPage.AttachedControl;

				string name = XmlConvert.EncodeName(tabPage.Text.Replace(" ", "_"));
				XmlNode xmlNodeGroup = xmlNodeOption.AppendChild(xmlDocument.CreateElement(name));

				foreach (Control control in panel.Controls[0].Controls)
				{
					if (control.Name.IndexOf("controlOption_") != 0)
					{
						continue;
					}
					string failReason;

					if (!CheckOptionValue(control, out failReason))
					{
						optionValuesAreValid = false;
					}
					IOption option = (IOption)control.Tag;
					XmlNode xmlNodeVariable = xmlNodeGroup.AppendChild(xmlDocument.CreateElement(XmlConvert.EncodeName(option.VariableName)));

					if (option.VarType == typeof(string) || option.VarType == typeof(int))
					{
						TextBox textBox = (TextBox)control;
						xmlNodeVariable.InnerText = textBox.Text;
					}
					else if (option.VarType == typeof(bool))
					{
						CheckBox checkBox = (CheckBox)control;
						xmlNodeVariable.InnerText = checkBox.Checked.ToString();
					}
					else if (option.VarType == typeof(Enum))
					{
						ComboBox comboBox = (ComboBox)control;
						xmlNodeVariable.InnerText = comboBox.Text;
					}
					else if (option.VarType.IsEnum)
					{
						ComboBox comboBox = (ComboBox)control;
						xmlNodeVariable.InnerText = comboBox.Text;
					}
					else if (option.VarType == typeof(bool?))
					{
						ComboBox comboBox = (ComboBox)control;
						xmlNodeVariable.InnerText = comboBox.Text;
					}
					else if (option.VarType == typeof(int?))
					{
						TextBox textBox = (TextBox)control;
						xmlNodeVariable.InnerText = textBox.Text;
					}
					else if (option.VarType == typeof(ArchAngel.Interfaces.SourceCodeType))
					{
						ActiproSoftware.SyntaxEditor.SyntaxEditor textBox = (ActiproSoftware.SyntaxEditor.SyntaxEditor)control;
						xmlNodeVariable.InnerText = textBox.Text;
					}
					else if (option.VarType == typeof(ArchAngel.Interfaces.SourceCodeMultiLineType))
					{
						ActiproSoftware.SyntaxEditor.SyntaxEditor textBox = (ActiproSoftware.SyntaxEditor.SyntaxEditor)control;
						xmlNodeVariable.InnerText = textBox.Text;
					}
					else
					{
						throw new NotImplementedException("Option type not handled yet: " + option.VarType);
					}
				}
			}
			//// Save the options that are not displayed to the user
			//XmlNode xmlNodeGroup2 = xmlNodeOption.AppendChild(xmlDocument.CreateElement("Hidden"));

			//foreach (ArchAngel.Interfaces.ITemplate.IOption option in Project.CurrentProject.Options)
			//{
			//    bool mustDisplayToUser = Project.CurrentProject.DisplayOptionToUser(option, null);

			//    if (!mustDisplayToUser)
			//    {
			//        XmlNode xmlNodeVariable = xmlNodeGroup2.AppendChild(xmlDocument.CreateElement(option.VariableName));
			//        //xmlNodeVariable.InnerText = option.
			//    }
			//}
			if (optionValuesAreValid)
			{
				xmlDocument.Save(fileName);
			}
			else
			{
				MessageBox.Show("Some values you have entered are invalid.", "Invalid Values", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		public bool ShowOptions()
		{
			//try
			//{
			if (Controller.Instance.CurrentProject.ProjectSettings == null || !File.Exists(Controller.Instance.CurrentProject.ProjectSettings.TemplateFileName))
			{
				ClearTabs();
				messagePanel.Visible = true;
				messagePanel.Text = "No template has been loaded, so there are no options to display.";
				return false;
			}
			messagePanel.Visible = false;

			if (SharedData.CurrentProject != null && SharedData.CurrentProject.Options.Count == 0)
			{
				messagePanel.Text = "This project contains no User Options";
				messagePanel.Visible = true;
			}
			else
			{
				messagePanel.Visible = false;
				CreateOptionControls();
			}
			//}
			//catch (Exception ex)
			//{
			//    Controller.ReportError(ex);
			//    return false;
			//}
			Refresh();
			return true;
		}

		private void ClearTabs()
		{
			for (int i = superTabControl1.Controls.Count - 1; i >= 0; i--)
			{
				if (superTabControl1.Controls[i] is SuperTabControlPanel)
					superTabControl1.Controls.RemoveAt(i);
			}
			superTabControl1.Tabs.Clear();
		}

		private void CreateOptionControls()
		{
			ClearTabs();

			if (SharedData.CurrentProject == null)
				return;

			const int padding = 8;
			int maxLabelWidth = 0;
			const int sidePadding = 30;

			Font font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);

			// Get all the categories and types
			var categories = new List<string>();
			var types = new List<string>();

			Label measureLabel = new Label();
			measureLabel.BackColor = Colors.BackgroundColor;
			measureLabel.Font = font;
			Graphics graphics = Graphics.FromHwnd(measureLabel.Handle);

			foreach (var option in SharedData.CurrentProject.Options)
			{
				if (!string.IsNullOrEmpty(option.IteratorName) && option.IteratorName != "No iteration")
					continue;

				// Get the value from the specified function. This function should have no parameters
				bool mustDisplayToUser = Controller.Instance.CurrentProject.DisplayOptionToUser(option, null);

				if (!mustDisplayToUser)
					option.Category = "HIDDEN";

				if (option.Category.Length == 0)
					option.Category = "General";

				if (categories.BinarySearch(option.Category) < 0)
				{
					categories.Add(option.Category);
					categories.Sort();
				}
				if (types.BinarySearch(option.VarType.FullName) < 0)
				{
					types.Add(option.VarType.FullName);
					types.Sort();
				}
				maxLabelWidth = maxLabelWidth > graphics.MeasureString(option.Text, font).Width ? maxLabelWidth : (int)graphics.MeasureString(option.Text, new Font("Microsoft Sans Serif", 8.25f)).Width;
			}
			foreach (var optionForm in SharedData.CurrentProject.OptionForms)
			{
				if (categories.BinarySearch(optionForm.Key) < 0)
				{
					categories.Add(optionForm.Key);
					categories.Sort();
				}
			}
			for (int i = 0; i < categories.Count; i++)
			{
				string category = categories[i];
				SuperTabControlPanel tabPanel = new SuperTabControlPanel();
				SuperTabItem tabItem = new SuperTabItem();

				superTabControl1.Controls.Add(tabPanel);
				superTabControl1.Tabs.Add(tabItem);

				tabPanel.Dock = System.Windows.Forms.DockStyle.Fill;
				tabPanel.Location = new System.Drawing.Point(102, 0);
				tabPanel.Name = "superTabControlPanel" + category;
				//tabPanel.Size = superTabControl1.SelectedPanel.Size;//.Width new System.Drawing.Size(211, 116);
				tabPanel.TabIndex = 1;
				tabPanel.TabItem = tabItem;
				//tabPanel.CanvasColor = Color.FromArgb(40, 40, 40);

				//tabPanel.ColorScheme.PanelBackground = this.BackColor;
				//tabPanel.ColorSchemeStyle = eDotNetBarStyle.Office2003;
				//tabPanel.ColorScheme.BarBackground = this.BackColor;

				tabItem.GlobalItem = false;
				tabItem.Name = "superTabItem" + category;
				tabItem.Text = " " + category + "  ";
				tabItem.AttachedControl = tabPanel;

				superTabControl1.BackColor = Color.FromArgb(30, 30, 30);

				tabItem.TabStripItem.FixedTabSize = new Size(160, 45);
				tabItem.TabStripItem.TabStripColor.Background.Colors = new Color[] { Color.FromArgb(10, 10, 10) };
				tabItem.TabStripItem.TabStripColor.InnerBorder = Color.FromArgb(140, 140, 140);//this.BackColor;
				tabItem.TabStripItem.TabStripColor.OuterBorder = Color.Black;//this.BackColor;
				tabItem.TabStripItem.TabStripColor.InsertMarker = Color.Yellow;

				//tabItem.TabStripItem.TabStripColor.ControlBoxDefault.Background = Color.Yellow;
				//tabItem.TabStripItem.TabStripColor.InsertMarker = Color.Pink;

				tabItem.TabColor.Default.Normal.Background.Colors = new Color[] { Color.FromArgb(10, 10, 10) };
				//tabItem.TabColor.Default.Normal.OuterBorder = Color.FromArgb(140, 140, 140);

				tabItem.TabColor.Default.Selected.Background.Colors = new Color[] { Color.FromArgb(100, 100, 100), Color.FromArgb(20, 20, 20) };
				tabItem.TabColor.Default.Selected.OuterBorder = Color.Black;
				tabItem.TabColor.Default.Selected.InnerBorder = Color.FromArgb(140, 140, 140);

				tabItem.TabColor.Default.MouseOver.Background.Colors = new Color[] { Color.FromArgb(50, 50, 50) };
				tabItem.TabColor.Default.MouseOver.OuterBorder = Color.Black;
				tabItem.TabColor.Default.MouseOver.InnerBorder = Color.FromArgb(140, 140, 140);

				Panel backgroundPanel = new Panel();
				backgroundPanel.Name = string.Format("BackgroundPanel{0}", i);
				backgroundPanel.BackColor = this.BackColor;
				backgroundPanel.Dock = DockStyle.Fill;
				backgroundPanel.Resize += new EventHandler(backgroundPanel_Resize);
				//backgroundPanel.Size = superTabControl1.SelectedPanel.Size;
				tabPanel.Controls.Add(backgroundPanel);

				//superTabControl1.PerformLayout();
				//tabPanel.PerformLayout();
				//backgroundPanel.PerformLayout();
				//superTabControl1.Refresh();

				if (category == "HIDDEN")
					tabItem.Visible = false;

				int currentTop = 30;
				int maxWidth = 0;

				foreach (IOption option in SharedData.CurrentProject.Options.Where(o => o.Category == category))
				{
					if (option.IsVirtualProperty)
						continue;

					bool mustDisplay = SharedData.CurrentProject.DisplayOptionToUser(option, null);

					if (!mustDisplay)
						option.Category = "HIDDEN";

					//if (option.Category != category)
					//    continue;

					Label label = new Label();
					label.ForeColor = Color.FromArgb(250, 250, 250);
					label.BackColor = Color.Transparent;
					label.Left = sidePadding;
					label.Text = option.Text;
					//label.TextAlignment = StringAlignment.Far;
					//label.TextLineAlignment = StringAlignment.Center;
					label.Top = currentTop;
					label.Width = maxLabelWidth + 10;
					//label.Style = eDotNetBarStyle.StyleManagerControlled;
					toolTip1.SetToolTip(label, option.Description);

					//panel.Controls.Add(label);
					backgroundPanel.Controls.Add(label);
					//backgroundPanel.PerformLayout();

					Control control = null;

					object defaultValue = SharedData.CurrentProject.GetDefaultValueOf(option);

					string typeName = option.VarType.FullName;

					if (option.VarType == typeof(bool?))
						typeName = "bool?";
					else if (option.VarType == typeof(int?))
						typeName = "int?";

					switch (typeName)
					{
						case "System.String":
							control = new TextBox();
							control.Text = (string)defaultValue;
							control.Left = label.Right + padding;
							//control.Width = panel.ClientSize.Width - control.Left - padding;
							//control.Width = superTabControl1.SelectedPanel.Width - control.Left - padding - (sidePadding * 2);
							control.Width = 250;
							control.Anchor = AnchorStyles.Top | AnchorStyles.Left;// | AnchorStyles.Right;
							control.TextChanged += Options_ValueChanged;
							break;

						case "System.Int32":
							control = new Slyce.Common.Controls.NumEdit();
							control.Text = ((int)defaultValue).ToString();
							((Slyce.Common.Controls.NumEdit)control).InputType = Slyce.Common.Controls.NumEdit.NumEditType.Integer;
							control.Left = label.Right + padding;
							control.TextChanged += Options_ValueChanged;
							break;

						case "System.Boolean":
							label.Visible = false;
							control = new CheckBox();
							control.Text = option.Text;
							control.ForeColor = Color.FromArgb(250, 250, 250);
							//((CheckBoxX)control).RightToLeft = RightToLeft.Yes;
							((CheckBox)control).TextAlign = ContentAlignment.MiddleRight;
							((CheckBox)control).BackColor = Color.Transparent;

							if (option.DefaultValue != "")
							{
								CheckBox checkBox = (CheckBox)control;
								checkBox.Checked = (bool)defaultValue;
							}

							control.Left = sidePadding + 5;
							Graphics graphicsChk = Graphics.FromHwnd(control.Handle);
							control.Width = (int)graphicsChk.MeasureString(option.Text, font).Width + 30;
							((CheckBox)control).CheckedChanged += Options_ValueChanged;
							break;

						case "System.Enum":
							control = new ComboBoxEx();
							ComboBoxEx comboBox = (ComboBoxEx)control;
							comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

							for (int j = 0; j < option.EnumValues.Length; j++)
								comboBox.Items.Add(option.EnumValues[j]);

							if (option.EnumValues.Length > 0)
								comboBox.SelectedIndex = 0;

							control.Left = label.Right + padding;
							((ComboBoxEx)control).SelectedIndexChanged += Options_ValueChanged;
							break;

						case "bool?":
							control = new ComboBoxEx();
							ComboBoxEx comboBoxBool = (ComboBoxEx)control;
							comboBoxBool.DropDownStyle = ComboBoxStyle.DropDownList;
							comboBoxBool.Items.Add("");
							comboBoxBool.Items.Add("true");
							comboBoxBool.Items.Add("false");
							comboBoxBool.SelectedIndex = 0;
							control.Left = label.Right + padding;
							((ComboBoxEx)control).SelectedIndexChanged += Options_ValueChanged;
							break;

						case "int?":
							control = new Slyce.Common.Controls.NumEdit();

							if (defaultValue == null)
								control.Text = "";
							else
								control.Text = ((int)defaultValue).ToString();

							((Slyce.Common.Controls.NumEdit)control).InputType = Slyce.Common.Controls.NumEdit.NumEditType.Integer;
							control.Left = label.Right + padding;
							control.TextChanged += Options_ValueChanged;
							break;

						case "ArchAngel.Interfaces.SourceCodeType":
							control = new ActiproSoftware.SyntaxEditor.SyntaxEditor();// TextBox();

							ConfigureSyntaxEditor((ActiproSoftware.SyntaxEditor.SyntaxEditor)control, false);

							control.Text = ((ArchAngel.Interfaces.SourceCodeType)defaultValue).Value;
							control.Left = label.Right + padding;
							//control.Width = panel.ClientSize.Width - control.Left - padding;
							//control.Width = superTabControl1.SelectedPanel.Width - control.Left - padding - (sidePadding * 2);
							control.Width = 400;// backgroundPanel.Width - control.Left - 100;
							control.Anchor = AnchorStyles.Top | AnchorStyles.Left;// | AnchorStyles.Right;
							control.TextChanged += Options_ValueChanged;
							break;

						case "ArchAngel.Interfaces.SourceCodeMultiLineType":
							control = new ActiproSoftware.SyntaxEditor.SyntaxEditor();// TextBox();

							ConfigureSyntaxEditor((ActiproSoftware.SyntaxEditor.SyntaxEditor)control, true);

							control.Text = ((ArchAngel.Interfaces.SourceCodeMultiLineType)defaultValue).Value;
							control.Left = label.Right + padding;
							//control.Width = panel.ClientSize.Width - control.Left - padding;
							//control.Width = superTabControl1.SelectedPanel.Width - control.Left - padding - (sidePadding * 2);
							control.Width = 400;// backgroundPanel.Width - control.Left - 100;
							control.Anchor = AnchorStyles.Top | AnchorStyles.Left;// | AnchorStyles.Right;
							control.TextChanged += Options_ValueChanged;
							break;
					}
					if (control == null)
						control = CreateControlUnknownType(option, label, padding);

					control.Name = "controlOption_" + option.VariableName;
					control.Tag = option;
					control.Top = currentTop;
					toolTip1.SetToolTip(control, option.Description);
					currentTop += padding + control.Height;
					backgroundPanel.Controls.Add(control);
					control.BringToFront();
					label.BringToFront();

					maxWidth = maxWidth > control.Left + control.Width + padding ? maxWidth : control.Left + control.Width + padding;
				}
				KeyValuePair<string, UserControl> optionForm = SharedData.CurrentProject.OptionForms.SingleOrDefault(o => o.Key == category);

				if (optionForm.Key != null)
				{
					optionForm.Value.Dock = DockStyle.Fill;
					((IOptionForm)optionForm.Value).Fill(SharedData.CurrentProject.Providers);
					backgroundPanel.Controls.Add(optionForm.Value);
				}
			}
			superTabControl1.SelectedTabIndex = 1;
			superTabControl1.Refresh();
			superTabControl1.SelectedTabIndex = 0;
			superTabControl1.Refresh();

			ResetDefaults();
		}

		void backgroundPanel_Resize(object sender, EventArgs e)
		{
			Panel backPanel = (Panel)sender;

			foreach (Control control in backPanel.Controls)
			{
				if (control is ActiproSoftware.SyntaxEditor.SyntaxEditor)
				{
					control.Width = backPanel.ClientSize.Width - control.Left - 20;
				}
			}
		}

		private void ConfigureSyntaxEditor(ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor, bool multiLineMode)
		{
			#region Syntax Editor settings
			syntaxEditor.Document.Multiline = multiLineMode;
			SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditor, TemplateContentLanguage.CSharp, SyntaxEditorHelper.ScriptLanguageTypes.CSharp, @"<%", @"%>");
			ActiproSoftware.SyntaxEditor.KeyPressTrigger t = new ActiproSoftware.SyntaxEditor.KeyPressTrigger("MemberListTrigger2", true, '#');
			t.ValidLexicalStates.Add(syntaxEditor.Document.Language.DefaultLexicalState);
			syntaxEditor.Document.Language.Triggers.Add(t);
			SwitchFormatting(syntaxEditor);
			#endregion
		}

		/// <summary>
		/// Swap faded and syntax-highlighted text.
		/// </summary>
		public void SwitchFormatting(ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor)
		{
			//UseSplitLanguage = !UseSplitLanguage;
			if (syntaxEditor.Document.Language.LexicalStates.Count > 1)
			{
				syntaxEditor.Document.Language.LexicalStates["ASPDirectiveState"].LexicalStateTransitionLexicalState.
					Language.BackColor = SyntaxEditorHelper.EDITOR_BACK_COLOR_FADED;
				syntaxEditor.Document.Language.BackColor = SyntaxEditorHelper.EDITOR_BACK_COLOR_NORMAL;
				syntaxEditor.Refresh();
			}
		}

		private Control CreateControlUnknownType(IOption option, Label label, int padding)
		{
			if (option.VarType.IsEnum)
			{
				ComboBoxEx control = new ComboBoxEx();
				control.DropDownStyle = ComboBoxStyle.DropDownList;

				foreach (var value in Enum.GetValues(option.VarType))
					control.Items.Add(value);

				if (control.Items.Count > 0)
					control.SelectedIndex = 0;

				control.Left = label.Left + label.Width + padding;
				control.SelectedIndexChanged += Options_ValueChanged;

				//if (option.VariableName == "NHibernateVersion")
				//    control.Enabled = false;

				return control;
			}

			throw new NotImplementedException("Cannot display UserOptions of type " + option.VarType.FullName);
		}

		private void Options_ValueChanged(object sender, EventArgs e)
		{
			if (!Controller.Instance.BusyPopulating)
			{
				string failReason;

				if (!CheckOptionValue((Control)sender, out failReason))
					MessageBox.Show("The value you have entered is invalid: " + failReason, "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				else
				{
					IOption option = (IOption)((Control)sender).Tag;

					if (option.VarType == typeof(int))
						SharedData.CurrentProject.SetUserOption(option.VariableName, ((TextBox)sender).Text);
					else if (option.VarType == typeof(string))
						SharedData.CurrentProject.SetUserOption(option.VariableName, ((TextBox)sender).Text);
					else if (option.VarType == typeof(bool))
						SharedData.CurrentProject.SetUserOption(option.VariableName, ((CheckBox)sender).Checked);
					else if (option.VarType == typeof(SourceCodeMultiLineType))
					{
						SourceCodeMultiLineType var = new SourceCodeMultiLineType(((ActiproSoftware.SyntaxEditor.SyntaxEditor)sender).Document.Text);
						SharedData.CurrentProject.SetUserOption(option.VariableName, var);
					}
					else if (option.VarType == typeof(SourceCodeType))
					{
						SourceCodeType var = new SourceCodeType(((ActiproSoftware.SyntaxEditor.SyntaxEditor)sender).Document.Text);
						SharedData.CurrentProject.SetUserOption(option.VariableName, var);
					}
					else if (option.VarType == typeof(bool?))
					{
						ComboBox combo = (ComboBox)sender;

						if (string.IsNullOrEmpty(combo.Text))
							SharedData.CurrentProject.SetUserOption(option.VariableName, null);
						else
							SharedData.CurrentProject.SetUserOption(option.VariableName, bool.Parse(((ComboBox)sender).Text));
					}
					else if (option.VarType == typeof(int?))
					{
						TextBox tb = (TextBox)sender;

						if (string.IsNullOrEmpty(tb.Text))
							SharedData.CurrentProject.SetUserOption(option.VariableName, null);
						else
							SharedData.CurrentProject.SetUserOption(option.VariableName, int.Parse(((TextBox)sender).Text));
					}
					else if (option.VarType == typeof(ArchAngel.NHibernateHelper.BytecodeGenerator))
					{
						ArchAngel.NHibernateHelper.BytecodeGenerator var = (ArchAngel.NHibernateHelper.BytecodeGenerator)Enum.Parse(typeof(ArchAngel.NHibernateHelper.BytecodeGenerator), ((ComboBox)sender).Text, true);
						SharedData.CurrentProject.SetUserOption(option.VariableName, var);
					}
					else if (option.VarType == typeof(TopLevelCascadeTypes))
					{
						TopLevelCascadeTypes var = (TopLevelCascadeTypes)Enum.Parse(typeof(TopLevelCascadeTypes), ((ComboBox)sender).Text, true);
						SharedData.CurrentProject.SetUserOption(option.VariableName, var);
					}
					else if (option.VarType == typeof(TopLevelCollectionCascadeTypes))
					{
						TopLevelCollectionCascadeTypes var = (TopLevelCollectionCascadeTypes)Enum.Parse(typeof(TopLevelCollectionCascadeTypes), ((ComboBox)sender).Text, true);
						SharedData.CurrentProject.SetUserOption(option.VariableName, var);
					}
					else if (option.VarType == typeof(ArchAngel.NHibernateHelper.VisualStudioVersions))
					{
						ArchAngel.NHibernateHelper.VisualStudioVersions var = (ArchAngel.NHibernateHelper.VisualStudioVersions)Enum.Parse(typeof(ArchAngel.NHibernateHelper.VisualStudioVersions), ((ComboBox)sender).Text, true);
						SharedData.CurrentProject.SetUserOption(option.VariableName, var);
					}
					else if (option.VarType == typeof(ArchAngel.NHibernateHelper.NHibernateVersions))
					{
						ArchAngel.NHibernateHelper.NHibernateVersions var = (ArchAngel.NHibernateHelper.NHibernateVersions)Enum.Parse(typeof(ArchAngel.NHibernateHelper.NHibernateVersions), ((ComboBox)sender).Text, true);
						SharedData.CurrentProject.SetUserOption(option.VariableName, var);
					}
					else if (option.VarType == typeof(TopLevelAccessTypes))
					{
						TopLevelAccessTypes var = (TopLevelAccessTypes)Enum.Parse(typeof(TopLevelAccessTypes), ((ComboBox)sender).Text, true);
						SharedData.CurrentProject.SetUserOption(option.VariableName, var);
					}
					else if (option.VarType == typeof(TopLevelCascadeTypes))
					{
						TopLevelCascadeTypes var = (TopLevelCascadeTypes)Enum.Parse(typeof(TopLevelCascadeTypes), ((ComboBox)sender).Text, true);
						SharedData.CurrentProject.SetUserOption(option.VariableName, var);
					}
					else
						throw new NotImplementedException("Option type not handled yet: " + option.VarType.Name);
				}
				Controller.Instance.IsDirty = true;
			}
		}

		private bool CheckOptionValue(Control control, out string failReason)
		{
			failReason = "";
			IOption option = (IOption)control.Tag;
			bool valueIsValid = true;

			if (option.IsValidValue.HasValue)
			{
				valueIsValid = option.IsValidValue.Value;

				if (valueIsValid)
					highlighter1.SetHighlightColor(control, DevComponents.DotNetBar.Validator.eHighlightColor.None);
				else
					highlighter1.SetHighlightColor(control, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
			}
			else if (option.ValidatorFunction.Length > 0)
			{
				if (option.VarType == typeof(string) || option.VarType == typeof(int))
				{
					TextBox textBox = (TextBox)control;
					object[] parameters = new object[] { textBox.Text, failReason };
					valueIsValid = (bool)SharedData.CurrentProject.CallTemplateFunction(option.ValidatorFunction, ref parameters);
					failReason = (string)parameters[1];
					//textBox.BackColor = valueIsValid ? Colors.BackgroundColor : Color.Salmon;

					if (valueIsValid)
						highlighter1.SetHighlightColor(textBox, DevComponents.DotNetBar.Validator.eHighlightColor.None);
					else
						highlighter1.SetHighlightColor(textBox, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
				}
				else if (option.VarType == typeof(bool))
				{
					CheckBox checkBox = (CheckBox)control;
					object[] parameters = new object[] { checkBox.Checked, failReason };
					valueIsValid = (bool)SharedData.CurrentProject.CallTemplateFunction(option.ValidatorFunction, ref parameters);
					failReason = (string)parameters[1];
					//checkBox.BackColor = valueIsValid ? Colors.BackgroundColor : Color.Salmon;

					if (valueIsValid)
						highlighter1.SetHighlightColor(checkBox, DevComponents.DotNetBar.Validator.eHighlightColor.None);
					else
						highlighter1.SetHighlightColor(checkBox, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
				}
				else if (option.VarType.IsEnum)
				{
					ComboBox comboBox = (ComboBox)control;
					object[] parameters = new[] { comboBox.SelectedValue, failReason };
					valueIsValid = (bool)SharedData.CurrentProject.CallTemplateFunction(option.ValidatorFunction, ref parameters);
					failReason = (string)parameters[1];
					//comboBox.BackColor = valueIsValid ? Colors.BackgroundColor : Color.Salmon;

					if (valueIsValid)
						highlighter1.SetHighlightColor(comboBox, DevComponents.DotNetBar.Validator.eHighlightColor.None);
					else
						highlighter1.SetHighlightColor(comboBox, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
				}
			}
			return valueIsValid;
		}

		/// <summary>
		/// Sets the option values in the live template, ready for generation.
		/// </summary>
		public void SetOptions()
		{
			//for (int i = 0; i <= tabControl1.Tabs.Count; i++)
			for (int i = 0; i < superTabControl1.Tabs.Count; i++)
			{
				SuperTabItem tabPage = (SuperTabItem)superTabControl1.Tabs[i];
				SuperTabControlPanel panel = (SuperTabControlPanel)tabPage.AttachedControl;

				foreach (Control controlP in panel.Controls)
				{
					Panel backgroundPanel = (Panel)controlP;

					foreach (Control control in backgroundPanel.Controls)
					{
						if (control.Name.IndexOf("controlOption_") != 0)
							continue;

						IOption option = (IOption)control.Tag;
						TextBox textBox;
						ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor;
						string typeName = option.VarType.FullName;

						if (option.VarType == typeof(bool?))
							typeName = "bool?";
						else if (option.VarType == typeof(int?))
							typeName = "int?";

						switch (typeName)
						{
							case "System.String":
								textBox = (TextBox)control;
								SharedData.CurrentProject.SetUserOption(option.VariableName, textBox.Text);
								break;
							case "ArchAngel.Interfaces.SourceCodeType":
								syntaxEditor = (ActiproSoftware.SyntaxEditor.SyntaxEditor)control;
								SharedData.CurrentProject.SetUserOption(option.VariableName, new ArchAngel.Interfaces.SourceCodeType(syntaxEditor.Text));
								break;
							case "ArchAngel.Interfaces.SourceCodeMultiLineType":
								syntaxEditor = (ActiproSoftware.SyntaxEditor.SyntaxEditor)control;
								SharedData.CurrentProject.SetUserOption(option.VariableName, new ArchAngel.Interfaces.SourceCodeMultiLineType(syntaxEditor.Text));
								break;
							case "System.Int32":
								textBox = (TextBox)control;
								SharedData.CurrentProject.SetUserOption(option.VariableName, Convert.ToInt32(textBox.Text));
								break;
							case "System.Boolean":
								CheckBox checkBox = (CheckBox)control;
								SharedData.CurrentProject.SetUserOption(option.VariableName, checkBox.Checked);
								break;
							case "System.Enum":
								ComboBox comboBox = (ComboBox)control;
								SharedData.CurrentProject.SetUserOption(option.VariableName, comboBox.Text);
								break;
							case "bool?":
								ComboBox comboBoxBool = (ComboBox)control;

								if (string.IsNullOrEmpty(comboBoxBool.Text))
									SharedData.CurrentProject.SetUserOption(option.VariableName, null);
								else
									SharedData.CurrentProject.SetUserOption(option.VariableName, bool.Parse(comboBoxBool.Text));

								break;
							case "int?":
								textBox = (TextBox)control;

								if (string.IsNullOrEmpty(textBox.Text))
									SharedData.CurrentProject.SetUserOption(option.VariableName, null);
								else
									SharedData.CurrentProject.SetUserOption(option.VariableName, Convert.ToInt32(textBox.Text));

								break;
							default:
								SetOptionsForUnknownTypes(option, control);
								break;
						}
					}
				}
			}
		}

		private void SetOptionsForUnknownTypes(IOption option, Control control)
		{
			if (option.VarType.IsEnum)
			{
				ComboBox comboBox = (ComboBox)control;
				SharedData.CurrentProject.SetUserOption(option.VariableName, comboBox.SelectedItem);
				return;
			}

			throw new Exception("Option type not handled yet: " + option.VarType);
		}

		private void buttonResetDefaults_Click(object sender, EventArgs e)
		{
			ResetDefaults();
		}

		internal void ResetDefaults()
		{
			if (SharedData.CurrentProject == null)
				return;

			foreach (var option in SharedData.CurrentProject.Options)
			{
				Control control = null;

				foreach (SuperTabItem tabPage in superTabControl1.Tabs)
				{
					SuperTabControlPanel panel = (SuperTabControlPanel)tabPage.AttachedControl;
					Control[] foundControls = panel.Controls.Find("controlOption_" + option.VariableName, true);

					if (foundControls.Length > 0)
					{
						control = foundControls[0];
						break;
					}
				}
				if (control != null)
				{
					object[] parameters = new object[0];
					string typeName = option.VarType.FullName;

					if (option.VarType == typeof(bool?))
						typeName = "bool?";
					else if (option.VarType == typeof(int?))
						typeName = "int?";

					// Controls will not exist for Options that have DisplayToUser = false
					switch (typeName)
					{
						case "System.String":
							control.Text = (string)SharedData.CurrentProject.TemplateLoader.CallDefaultValueFunction(option, parameters);
							break;

						case "System.Int32":
							control.Text = ((int)SharedData.CurrentProject.TemplateLoader.CallDefaultValueFunction(option, parameters)).ToString();
							break;

						case "System.Boolean":
							((CheckBox)control).Checked = (bool)SharedData.CurrentProject.TemplateLoader.CallDefaultValueFunction(option, parameters);
							break;

						case "bool?":
							bool? val = (bool?)SharedData.CurrentProject.TemplateLoader.CallDefaultValueFunction(option, parameters);
							((ComboBoxEx)control).Text = val.HasValue ? val.Value.ToString().ToLower() : "";
							break;

						case "int?":
							int? intVal = (int?)SharedData.CurrentProject.TemplateLoader.CallDefaultValueFunction(option, parameters);
							control.Text = intVal.HasValue ? intVal.ToString() : "";
							break;

						case "System.Enum":
							// TODO: default values for enumerations
							throw new NotImplementedException("Enums not coded yet");

						case "ArchAngel.Interfaces.SourceCodeType":
							control.Text = ((ArchAngel.Interfaces.SourceCodeType)SharedData.CurrentProject.TemplateLoader.CallDefaultValueFunction(option, parameters)).Value;
							break;

						case "ArchAngel.Interfaces.SourceCodeMultiLineType":
							control.Text = ((ArchAngel.Interfaces.SourceCodeMultiLineType)SharedData.CurrentProject.TemplateLoader.CallDefaultValueFunction(option, parameters)).Value;
							break;

						default:
							ResetDefaultsUnknownType(option, control);
							break;
					}
				}
			}
		}

		private void ResetDefaultsUnknownType(IOption option, Control control)
		{
			if (option.VarType.IsEnum)
			{
				var parameters = new object[0];
				((ComboBox)control).SelectedItem = SharedData.CurrentProject.TemplateLoader.CallDefaultValueFunction(option, parameters);
				return;
			}

			throw new NotImplementedException("Not coded yet");
		}

		/// <summary>
		/// Updates the vallues in the controls to the actual values. This only needs to get called
		/// once after a project is loaded. Best to do it when the sceen is going to be displayed for the first time.
		/// We need it because this screen is popoulated with default values and not real values if
		/// a project is started from an existing VS project. However, with NHibernate, we load the ProjectGuid
		/// from the existing VS project file, and if we don't update this screen the the guid will be the default value.
		/// </summary>
		public void UpdateValues()
		{
			messagePanel.Visible = false;

			bool oldPopulatingValue = Controller.Instance.BusyPopulating;
			Controller.Instance.BusyPopulating = true;

			for (int i = 0; i < superTabControl1.Tabs.Count; i++)
			{
				SuperTabItem tabPage = (SuperTabItem)superTabControl1.Tabs[i];
				SuperTabControlPanel panel = (SuperTabControlPanel)tabPage.AttachedControl;

				foreach (Control control in panel.Controls[0].Controls)
				{
					if (control.Name.IndexOf("controlOption_") != 0)
					{
						if (control is IOptionForm)
							((IOptionForm)control).Fill(SharedData.CurrentProject.Providers);

						continue;
					}
					continue; // GFH
					var option = (IOption)control.Tag;
					string name = XmlConvert.EncodeName(tabPage.Text.Replace(" ", "_"));
					var correspondingOption = Controller.Instance.CurrentProject.FindOption(option.VariableName, option.IteratorName);
					object value = Controller.Instance.CurrentProject.GetUserOption(option.VariableName);

					if (option.VarType == typeof(string) || option.VarType == typeof(int))
					{
						TextBox textBox = (TextBox)control;
						object[] parameters = new object[0];
						textBox.Text = (string)value;
					}
					else if (option.VarType == typeof(bool))
					{
						CheckBox checkBox = (CheckBox)control;
						checkBox.Checked = (bool)value;
					}
					else if (option.VarType == typeof(Enum))
					{
						ComboBox comboBox = (ComboBox)control;
						comboBox.SelectedItem = value;
					}
					else if (option.VarType.IsEnum)
					{
						ComboBox comboBox = (ComboBox)control;
						comboBox.SelectedItem = value;
					}
					else if (option.VarType == typeof(bool?))
					{
						ComboBox comboBox = (ComboBox)control;
						comboBox.SelectedItem = value == null ? "" : value.ToString();
					}
					else if (option.VarType == typeof(int?))
					{
						TextBox textBox = (TextBox)control;
						object[] parameters = new object[0];
						textBox.Text = (string)value;
					}
					else if (option.VarType == typeof(ArchAngel.Interfaces.SourceCodeType))
					{
						ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor = (ActiproSoftware.SyntaxEditor.SyntaxEditor)control;
						object[] parameters = new object[0];
						syntaxEditor.Text = ((ArchAngel.Interfaces.SourceCodeType)value).Value;
					}
					else if (option.VarType == typeof(ArchAngel.Interfaces.SourceCodeMultiLineType))
					{
						ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor = (ActiproSoftware.SyntaxEditor.SyntaxEditor)control;
						object[] parameters = new object[0];
						syntaxEditor.Text = ((ArchAngel.Interfaces.SourceCodeMultiLineType)value).Value;
					}
					else
						throw new NotImplementedException("Not coded yet: +" + option.VarType);
				}
			}
			Controller.Instance.BusyPopulating = oldPopulatingValue;
		}

		private void Options_VisibleChanged(object sender, EventArgs e)
		{
			if (!this.Visible)
				SaveOptionsForms();
		}

		private void SaveOptionsForms()
		{
			// Finalize any edits to IOptionForms
			for (int i = 0; i < superTabControl1.Tabs.Count; i++)
			{
				SuperTabItem tabPage = (SuperTabItem)superTabControl1.Tabs[i];
				SuperTabControlPanel panel = (SuperTabControlPanel)tabPage.AttachedControl;

				foreach (Control control in panel.Controls[0].Controls)
				{
					if (control.Name.IndexOf("controlOption_") != 0)
					{
						if (control is IOptionForm)
							((IOptionForm)control).FinaliseEdits();
					}
				}
			}
		}

		private void buttonResetAllDefaults_Click(object sender, EventArgs e)
		{
			if (MessageBoxEx.Show(this, "Reset ALL options to default values?", "Reset values", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				ResetDefaults();
		}

	}
}