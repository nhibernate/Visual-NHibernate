using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.SyntaxEditor.Addons.Dynamic;
using ArchAngel.Common.DesignerProject;
//using ArchAngel.Debugger;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Designer.Wizards;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using Slyce.Common;

namespace ArchAngel.Designer
{
	public partial class ucFunctions : UserControl
	{
		private enum ErrorListColumns
		{
			Function = 0,
			Description = 1,
			Line = 2,
			CharPos = 3
		}
		private Control _CallingControl;
		private readonly CrossThreadHelper CrossThreadHelper;
		private readonly RichTextBox rtbOutput = new RichTextBox();
		private const string ErrorLayerKey = "error-layer";
		private readonly SuperTooltip SuperTooltipErrors = new SuperTooltip();
		private bool IsPopulated;

		public ucFunctions()
		{
			InitializeComponent();
			if (Utility.InDesignMode)
			{
				return;
			}

			CrossThreadHelper = new CrossThreadHelper(this);
			EnableDoubleBuffering();
			tabStrip1.Height = ClientSize.Height - tabStrip1.Top;
			tabStrip1.Tabs.Clear();
			Controller.Instance.CompileErrorEvent += HighlightError;

#if DEBUG
			rtbOutput.Dock = DockStyle.Fill;
			rtbOutput.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
#endif
			lblStatus.Top = 1;
			lblStatus.Left = 0;
			lblStatus.Width = ClientSize.Width;
			lblStatus.Height = ClientSize.Height;
			lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			tabStrip1.Visible = false;

			tabStrip1.CloseButtonOnTabsAlwaysDisplayed = true;
			tabStrip1.CloseButtonOnTabsVisible = true;
			tabStrip1.CloseButtonPosition = eTabCloseButtonPosition.Right;
			tabStrip1.CloseButtonVisible = true;

			SetErrorListColumnWidths();

			//Debugger.Debugger.SpinUpDebugProcess();
		}

		private void SetErrorListColumnWidths()
		{
			listErrors.Columns[(int)ErrorListColumns.Function].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
			listErrors.Columns[(int)ErrorListColumns.Line].Width = 0;
			listErrors.Columns[(int)ErrorListColumns.CharPos].Width = 0;

			if (listErrors.Columns[(int)ErrorListColumns.Function].Width + listErrors.Columns[(int)ErrorListColumns.Description].Width < listErrors.Width)
			{
				listErrors.Columns[(int)ErrorListColumns.Description].Width = listErrors.Width - listErrors.Columns[(int)ErrorListColumns.Function].Width;
			}
		}

		/// <summary>
		/// Clears all selections from all currently display functions' editors.
		/// </summary>
		internal void ClearAllSelections()
		{
			for (int tabCounter = 0; tabCounter < tabStrip1.Tabs.Count; tabCounter++)
			{
				SyntaxEditor editor = Controller.Instance.MainForm.UcFunctions.GetFunctionScreenByTabIndex(tabCounter).syntaxEditor1;
				editor.SelectedView.Selection.EndOffset = editor.SelectedView.Selection.StartOffset;
			}
		}

		internal void NextTab()
		{
			if (!tabStrip1.SelectNextTab() && tabStrip1.Tabs.Count > 1)
			{
				tabStrip1.SelectedTabIndex = 0;
			}
		}

		internal void PrevTab()
		{
			if (!tabStrip1.SelectPreviousTab() && tabStrip1.Tabs.Count > 1)
			{
				tabStrip1.SelectedTabIndex = tabStrip1.Tabs.Count - 1;
			}
		}

		public ucFunction GetFunctionScreenByTabIndex(int index)
		{
			if (index >= tabStrip1.Tabs.Count)
			{
				return null;
			}
			return (ucFunction)tabStrip1.Tabs[index].AttachedControl.Controls["ucFunction"];
		}

		public ucFunction GetFunctionScreen(string functionName, ParamInfo[] parameters)
		{
			foreach (TabItem tab in tabStrip1.Tabs)
			{
				ucFunction functionScreen = (ucFunction)tab.AttachedControl.Controls["ucFunction"];
				FunctionInfo func = functionScreen.CurrentFunction;

				if (func.Name == functionName && func.Parameters.Count == parameters.Length)
				{
					bool found = true;

					for (int i = 0; i < func.Parameters.Count; i++)
					{
						if (func.Parameters[i].DataType.FullName != parameters[i].DataType.FullName)
						{
							found = false;
							break;
						}
					}
					if (found)
					{
						return functionScreen;
					}
				}
			}
			return null;
		}

		public Control CallingControl
		{
			get { return _CallingControl; }
			set
			{
				if (value != null)
				{
					_CallingControl = value;
				}
			}
		}

		private void EnableDoubleBuffering()
		{
			// Set the value of the double-buffering style bits to true.
			SetStyle(ControlStyles.DoubleBuffer |
					 ControlStyles.UserPaint |
					 ControlStyles.AllPaintingInWmPaint,
					 true);
			UpdateStyles();
		}

		public void Clear()
		{
			ClearErrors();
			ClearFindResults();

			for (int i = tabStrip1.Tabs.Count - 1; i >= 0; i--)
			{
				tabStrip1.Tabs.RemoveAt(0);
			}
			treeFunctions.Nodes.Clear();
		}

		/// <summary>
		/// Close tabs that are for functions that have subsequently been deleted.
		/// </summary>
		internal void RemoveTabsOfDeletedFunctions()
		{
			for (int i = tabStrip1.Tabs.Count - 1; i >= 0; i--)
			{
				//throw new NotImplementedException("GFH - not yet updated for new FindFunction");
				FunctionInfo function = (FunctionInfo)tabStrip1.Tabs[i].Tag;

				if (Project.Instance.FindFunction(function.Name, function.Parameters) == null)
				{
					RemoveTab(tabStrip1.Tabs[i]);
				}
			}
		}

		/// <summary>
		/// Displays the results of a Find operation - when a user searches for text.
		/// </summary>
		public void ShowFindResults()
		{
			lblStatus.Visible = false;
			//tabStrip1.Visible = true;

			//if (dockFindResults.State != ToolWindowState.DockableInsideHost)
			//{
			//    dockFindResults.State = ToolWindowState.DockableInsideHost;
			//}
			bar1.SelectedDockContainerItem = dockFindResults;
			((ucFindResults)dockFindResults.Control.Controls.Find("ucFindResults1", false)[0]).ShowResults();
		}

		/// <summary>
		/// Displays list of compilation errors.
		/// </summary>
		public void ShowErrors()
		{
			CrossThreadHelper.SetVisibility(lblStatus, false);
			CrossThreadHelper.SetVisibility(tabStrip1, true);
			listErrors.Columns[(int)ErrorListColumns.Function].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
			bar1.SelectedDockContainerItem = dockErrors;
			Controller.Instance.MainForm.HidePanelControls(Controller.Instance.MainForm.UcFunctions);
		}

		public FunctionInfo NewFunction()
		{
			return NewFunction(null);
		}

		public FunctionInfo NewFunction(Type parameterTypeForNewScriptFunction)
		{
			FunctionInfo newFunction =
				new FunctionInfo("NewFunction", typeof(string), "", true,
										 SyntaxEditorHelper.ScriptLanguageTypes.CSharp, "", "plain text", "");

			if (parameterTypeForNewScriptFunction != null)
			{
				string paramName = parameterTypeForNewScriptFunction.Name.Replace("[]", "s");
				paramName = paramName.Substring(0, 1).ToLower() + paramName.Substring(1);

				if (paramName.IndexOf("List<") == 0)
				{
					paramName = paramName.Replace("List<", "").Replace(">", "") + "s";
				}
				string cleanParamName = "";

				for (int i = 0; i < paramName.Length; i++)
				{
					if (char.IsLetterOrDigit(paramName[i]))
					{
						cleanParamName += paramName[i];
					}
				}

				newFunction.Parameters.Add(new ParamInfo(cleanParamName, parameterTypeForNewScriptFunction));
			}
			Project.Instance.AddFunction(newFunction);
			frmFunctionWizard form = new frmFunctionWizard(newFunction, true);

			if (form.ShowDialog(ParentForm) == DialogResult.Cancel)
			{
				Project.Instance.DeleteFunction(newFunction);
				//OwnerTabStripPage.TabStrip.Pages.Remove(OwnerTabStripPage);
				return null;
			}
			ShowFunction(frmFunctionWizard.CurrentFunction, true, null);
			return newFunction;
		}

		public void RenameSelectedTab(string newName)
		{
			if (tabStrip1.SelectedTab != null)
			{
				tabStrip1.SelectedTab.Text = newName;
			}
		}

		public ucFunction GetFunctionPanel(FunctionInfo function)
		{
			foreach (TabItem tab in tabStrip1.Tabs)
			{
				ucFunction functionScreen = (ucFunction)tab.AttachedControl.Controls["ucFunction"];

				if (functionScreen.CurrentFunction == function)
				{
					return functionScreen;
				}
			}
			ShowFunction(function, null);

			foreach (TabItem tab in tabStrip1.Tabs)
			{
				ucFunction functionScreen = (ucFunction)tab.AttachedControl.Controls["ucFunction"];

				if (functionScreen.CurrentFunction == function)
				{
					return functionScreen;
				}
			}
			throw new Exception("We shouldn't be here.");
		}

		public void ShowFunction(FunctionInfo function, Control callingControl)
		{
			if (function != null)
			{
				ShowFunction(function, true, callingControl);
			}
		}

		public bool ShowFunction(FunctionInfo function,
									bool allowEdit,
									Control callingControl)
		{

			if (function == null) return false;

			try
			{
				Cursor = Cursors.WaitCursor;
				CallingControl = callingControl;
				lblStatus.Visible = false;
				tabStrip1.Visible = true;

				for (int i = 0; i < tabStrip1.Tabs.Count; i++)
				{
					if (GetFunctionScreenByTabIndex(i).CurrentFunction == function)
					{
						tabStrip1.SelectedTabIndex = i;
						Controller.Instance.MainForm.MenuItemDebug.Enabled = true;
						return true;
					}
				}
				TabItem newPage = CreateNewFunctionTabPage(function, allowEdit);
				newPage.MouseUp += newPage_MouseUp;
				tabStrip1.Controls.Add(newPage.AttachedControl);
				tabStrip1.Tabs.Insert(0, newPage);
				tabStrip1.SelectedTabIndex = 0;
				Controller.Instance.MainForm.MenuItemDebug.Enabled = true;
				Controller.Instance.MainForm.ShowMenuBarFor(this);
			}
			finally
			{
				Cursor = Cursors.Default;
			}
			return true;
		}

		void newPage_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				TabItem tab = (TabItem)sender;
				tab.Parent.SelectedTab = tab;
				Point pt = tab.Parent.PointToScreen(new Point(tab.DisplayRectangle.X, tab.DisplayRectangle.Y + tab.DisplayRectangle.Height));
				contextMenuTabs.Show(pt);
			}
		}

		/// <summary>
		/// Returns the ucFunction control that is currently displayed in the tab strip.
		/// </summary>
		/// <returns>Returns the ucFunction control that is currently displayed or null if no
		/// function pages are currently being displayed.</returns>
		public ucFunction GetCurrentlyDisplayedFunctionPage()
		{
			if (tabStrip1.SelectedTab != null)
			{
				return GetFunctionScreenByTabIndex(tabStrip1.SelectedTabIndex);
			}
			return null;
		}

		private TabItem CreateNewFunctionTabPage(FunctionInfo function, bool allowEdit)
		{
			TabItem newPage = new TabItem();
			TabControlPanel panel = new TabControlPanel();
			panel.TabItem = newPage;
			panel.Dock = DockStyle.Fill;
			newPage.AttachedControl = panel;
			newPage.Text = function.Name;
			newPage.ImageIndex = 0;
			newPage.Tag = function;
			newPage.CloseButtonVisible = true;
			ucFunction funcPanel = new ucFunction();
			funcPanel.Dock = DockStyle.Fill;
			funcPanel.AllowEdit = allowEdit;
			newPage.AttachedControl.Controls.Add(funcPanel);
			funcPanel.FunctionName = function.Name;
			funcPanel.CurrentFunction = function;
			//funcPanel.DefaultValueFunction = defaultValueFunction;
			funcPanel.Populate();

			switch (SyntaxEditorHelper.GetScriptingLanguage(function.ScriptLanguage))
			{
				case TemplateContentLanguage.CSharp:
					newPage.ImageIndex = 3;
					break;
				case TemplateContentLanguage.VbDotNet:
					newPage.ImageIndex = 5;
					break;
				case TemplateContentLanguage.Sql:
					newPage.ImageIndex = 0;
					break;
				case TemplateContentLanguage.Html:
					newPage.ImageIndex = 4;
					break;
				case TemplateContentLanguage.Css:
					newPage.ImageIndex = 2;
					break;
				case TemplateContentLanguage.IniFile:
					newPage.ImageIndex = 0;
					break;
				case TemplateContentLanguage.JScript:
					newPage.ImageIndex = 0;
					break;
				case TemplateContentLanguage.Python:
					newPage.ImageIndex = 0;
					break;
				case TemplateContentLanguage.VbScript:
					newPage.ImageIndex = 5;
					break;
				case TemplateContentLanguage.Xml:
					newPage.ImageIndex = 6;
					break;
				case TemplateContentLanguage.PlainText:
					newPage.ImageIndex = 0;
					break;
				default:
					throw new Exception("This function return type not handled yet in ShowFunction: " +
										funcPanel.ReturnType);
			}

			return newPage;
		}

		private TabItem CreateNewFunctionTabPage(UserOption userOption, FunctionTypes functionType, bool allowEdit)
		{
			var newPage = new TabItem();
			var panel = new TabControlPanel { TabItem = newPage, Dock = DockStyle.Fill };
			newPage.AttachedControl = panel;

			switch (functionType)
			{
				case FunctionTypes.DefaultValue:
					newPage.Text = userOption.VariableName + " [Default Value]";
					break;
				case FunctionTypes.DisplayToUser:
					newPage.Text = userOption.VariableName + " [Display To User]";
					break;
				case FunctionTypes.Validation:
					newPage.Text = userOption.VariableName + " [Valitation]";
					break;
			}
			newPage.ImageIndex = 0;
			newPage.Tag = userOption;
			newPage.CloseButtonVisible = true;
			var funcPanel = new ucFunction
								{
									Dock = DockStyle.Fill,
									AllowEdit = allowEdit

								};
			newPage.AttachedControl.Controls.Add(funcPanel);
			funcPanel.FunctionName = userOption.VariableName;
			funcPanel.CurrentUserOption = userOption;
			funcPanel.CurrentUserOptionFunctionType = functionType;
			funcPanel.Populate();
			newPage.ImageIndex = 0;
			return newPage;
		}

		//private void tabStrip1_Command(object sender, CommandEventArgs e)
		//{
		//    if (e.Command == TabStrip.CloseCommand)
		//    {
		//        if (tabStrip1.SelectedTab != null)
		//        {
		//            //string selectedTabKey = tabStrip1.SelectedTab.Key;

		//            foreach (Control ctl in tabStrip1.SelectedTab.AttachedControl.Controls)
		//            {
		//                if (ctl.GetType() == typeof(ucFunction))
		//                {
		//                    ucFunction function = (ucFunction)ctl;

		//                    if (function.IsDirty &&
		//                        (function.DefaultValueFunction == null ||
		//                         function.DefaultValueFunction.UseCustomCode))
		//                    {
		//                        if (
		//                            MessageBox.Show(this,
		//                                            "You have modified the code of the function you are closing without saving. Save the changes?",
		//                                            "Save Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
		//                            DialogResult.Yes)
		//                        {
		//                            function.Save();
		//                        }
		//                    }
		//                    else if (function.DefaultValueFunction != null &&
		//                             !function.DefaultValueFunction.UseCustomCode &&
		//                             !function.DefaultValueFunction.IsForUserOption)
		//                    {
		//                        if (Project.Instance.FindFunction(function.CurrentFunction.Name, function.CurrentFunction.Parameters) != null)
		//                        {
		//                            Project.Instance.DeleteFunction(function.CurrentFunction);
		//                        }
		//                    }
		//                }
		//            }
		//            if (tabStrip1.SelectedTab != null)
		//            {
		//                RemoveTab(tabStrip1.SelectedTab);
		//            }
		//            if (tabStrip1.Tabs.Count == 0)
		//            {
		//                Controller.Instance.MainForm.debugToolStripMenuItem.Enabled = false;
		//            }
		//        }
		//    }
		//}

		///// <summary>
		///// Removes the specified tab and closes the screen if no more tabs remain.
		///// </summary>
		///// <param name="pageToRemove">Tab to remove.</param>
		//internal void RemoveTab(DevComponents.DotNetBar.TabItem pageToRemove)
		//{
		//    if (pageToRemove != null & tabStrip1.Tabs.Contains(pageToRemove))
		//    {
		//        tabStrip1.Tabs.Remove(pageToRemove);
		//    }
		//    if (tabStrip1.Tabs.Count == 0)
		//    {
		//        if (CallingControl != null)
		//        {
		//            // If only one function got loaded, then show the form that caused it to get displayed if there are no
		//            // other functions left. Will usually be the API Extension form.
		//            Controller.Instance.MainForm.HidePanelControls(CallingControl);
		//            //return;
		//        }
		//        tabStrip1.Visible = false;
		//        lblStatus.Visible = true;
		//        Refresh();
		//    }
		//}

		public void SwitchFormatting()
		{
			if (tabStrip1.SelectedTab != null)
			{
				((ucFunction)tabStrip1.SelectedTab.AttachedControl.Controls["ucFunction"]).SwitchFormatting();
			}
		}

		public void SaveAllFunctions(bool debugVersion)
		{
			foreach (TabItem page in tabStrip1.Tabs)
			{
				if (page.AttachedControl.Controls["ucFunction"] != null)
				{
					((ucFunction)page.AttachedControl.Controls["ucFunction"]).Save();
					page.Text = page.Text.Replace("*", "").Trim();
				}
			}
		}

		public void ClearErrors()
		{
			foreach (TabItem page in tabStrip1.Tabs)
			{
				foreach (Control ctl in page.AttachedControl.Controls)
				{
					if (ctl.GetType() == typeof(ucFunction))
					{
						SpanIndicatorLayer layer =
							((ucFunction)ctl).syntaxEditor1.Document.SpanIndicatorLayers[ErrorLayerKey];
						if (layer != null)
							layer.Clear();
						break;
					}
				}
			}
			if (listErrors.InvokeRequired)
			{
				CrossThreadHelper.CallCrossThreadMethod(listErrors.Items, "Clear", null);
			}
			else
			{
				listErrors.Items.Clear();
			}
		}

		/// <summary>
		/// Opens function associated with first error
		/// </summary>
		public void DisplayFirstErrorFunction()
		{
			for (int i = 0; i < listErrors.Items.Count; i++)
			{
				if (!(listErrors.Items[i].Tag is FunctionInfo)) continue;

				FunctionInfo function = (FunctionInfo)listErrors.Items[i].Tag;
				MarkErrorsForFunction(function, listErrors.Items[i]);
				break;
			}
		}

		public void ClearFindResults()
		{
			ucFindResults1.Clear();
		}

		public void HighlightError(int lineNum, int characterPosition, string message, int switchOffset,
								   string functionName, List<ParamInfo> parameters, bool isWarning)
		{
			ShowErrors();
			FunctionInfo function = Project.Instance.FindFunction(functionName, parameters);

			if (tabStrip1.InvokeRequired)
			{
				CrossThreadHelper.SetCrossThreadProperty(tabStrip1, "Height", ClientSize.Height - tabStrip1.Top - 200);
			}
			else
			{
				tabStrip1.Height = ClientSize.Height - tabStrip1.Top - 200;
			}
			ListViewItem lvi;

			if (lineNum >= 0)
			{
				SyntaxEditor syntaxEditor1 = null;
				ucFunction currentPage = GetCurrentlyDisplayedFunctionPage();

				if (currentPage != null && currentPage.CurrentFunction == function)
				{
					foreach (Control ctl in tabStrip1.SelectedTab.AttachedControl.Controls)
					{
						if (ctl.GetType() == typeof(ucFunction))
						{
							syntaxEditor1 = ((ucFunction)ctl).syntaxEditor1;
							break;
						}
					}
					MarkErrorWord(syntaxEditor1, lineNum, characterPosition, message);
				}
				lvi = new ListViewItem(new[] { functionName, message, lineNum.ToString(), characterPosition.ToString() });
				lvi.Tag = function;
				lvi.StateImageIndex = lvi.ImageIndex = isWarning ? 1 : 0;

				if (listErrors.InvokeRequired)
				{
					CrossThreadHelper.CallCrossThreadMethod(listErrors.Items, "Add", new object[] { lvi });
				}
				else
				{
					listErrors.Items.Add(lvi);
				}
			}
			else
			{
				if (lineNum == 0)
				{
					lvi = new ListViewItem(new[] { functionName, message, "Func Header", "" });
				}
				else
				{
					lvi = new ListViewItem(new[] { functionName, message, "", "" });
				}
				lvi.StateImageIndex = lvi.ImageIndex = isWarning ? 1 : 0;
				lvi.Tag = function;
				listErrors.Items.Add(lvi);
				SuperTooltipInfo sti = new SuperTooltipInfo(functionName, null, message, null, null, eTooltipColor.Office2003);
			}
		}

		private void MarkErrorWord(SyntaxEditor editor, int lineNumber, int characterPos, string message)
		{
			string text = editor.Document.Lines[lineNumber].Text;
			string compileText = CompileHelper.ReplaceUserOptionCalls(text);
			string preceedingText = characterPos <= compileText.Length ? compileText.Substring(0, characterPos) : "";

			#region Find all GetUserOption calls and discount them

			int index = preceedingText.LastIndexOf("GetUserOption");
			int offsetUO = "GetUserOptionValue('')".Length - "UserOptions.".Length;
			int castEndPos = 0;
			int castStartPos = 0;

			while (index >= 0)
			{
				characterPos -= offsetUO;

				while (preceedingText[index] != ')')
				{
					castEndPos = index;
					index -= 1;
				}
				while (preceedingText[index] != '(')
				{
					castStartPos = index;
					index -= 1;
				}
				characterPos -= castEndPos - castStartPos + 1;
				index = preceedingText.LastIndexOf("GetUserOption", index);
			}

			#endregion

			DocumentPosition position = new DocumentPosition(lineNumber, characterPos);
			int offset = editor.Document.PositionToOffset(position);
			DynamicToken token = (DynamicToken)editor.Document.Tokens.GetTokenAtOffset(offset);
			SpanIndicator indicator = new WaveLineSpanIndicator("ErrorIndicator", Color.Red);
			indicator.Tag = message;
			SpanIndicatorLayer indicatorLayer = editor.Document.SpanIndicatorLayers[ErrorLayerKey];

			if (indicatorLayer == null)
			{
				indicatorLayer = new SpanIndicatorLayer(ErrorLayerKey, 1);
				editor.Document.SpanIndicatorLayers.Add(indicatorLayer);
			}
			int startOffset = Math.Min(token.StartOffset, indicatorLayer.Document.Length - 1);
			int length = Math.Max(token.Length, 1);
			SpanIndicator[] indicators = indicatorLayer.GetIndicatorsForTextRange(new TextRange(startOffset, startOffset + length));

			foreach (SpanIndicator i in indicators)
			{
				// If there is already an error indicator on that word, don't add another one.
				if (i.TextRange.StartOffset == startOffset && i.TextRange.Length == length)
					return;
			}
			indicatorLayer.Add(indicator, startOffset, length);
		}

		private void closeTabPageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (tabStrip1.SelectedTab != null)
			{
				ucFunction function = (ucFunction)tabStrip1.SelectedTab.AttachedControl.Controls["ucFunction"];

				if (function.IsDirty)// &&
				//(function.DefaultValueFunction == null ||
				// function.DefaultValueFunction.UseCustomCode))
				{
					if (
						MessageBox.Show(this,
										"You have modified the code of the function you are closing without saving. Save the changes?",
										"Save Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
						DialogResult.Yes)
					{
						function.Save();
					}
				}
				//else if (function.DefaultValueFunction != null && !function.DefaultValueFunction.UseCustomCode)
				//{
				//    if (Project.Instance.FindFunction(function.CurrentFunction.Name, function.CurrentFunction.Parameters) != null)
				//    {
				//        Project.Instance.DeleteFunction(function.CurrentFunction);
				//    }
				//}
				tabStrip1.Tabs.Remove(tabStrip1.SelectedTab);

				if (tabStrip1.SelectedTab != null)
				{
					tabStrip1.SelectedTab.AttachedControl.Controls["ucFunction"].Controls[3].Focus();
				}
			}
		}

		private void MarkErrorsForFunction(FunctionInfo function, ListViewItem clickedNode)
		{
			if (!Controller.Instance.MainForm.ShowFunction(function, null))
			{
				return;
			}
			int line;
			//string charPosString;
			SyntaxEditor editor = ((ucFunction)Controller.Instance.MainForm.UcFunctions.tabStrip1.SelectedTab.AttachedControl.Controls["ucFunction"]).syntaxEditor1;

			foreach (ListViewItem node in listErrors.Items)
			{
				if (node.Tag != function)
				{
					// Not the correct function, so skip
					continue;
				}

				if (int.TryParse(node.SubItems[(int)ErrorListColumns.Line].Text, out line))
				{
					int charPos;
					if (int.TryParse(node.SubItems[(int)ErrorListColumns.CharPos].Text, out charPos) && charPos >= 0)
					{
						MarkErrorWord(editor, line, charPos, node.SubItems[(int)ErrorListColumns.Description].Text);
					}
				}
			}
			// Highlight the line referred to by the clicked node
			if (int.TryParse(clickedNode.SubItems[(int)ErrorListColumns.Line].Text, out line))
			{
				int startOffset = editor.Document.Lines[line].StartOffset;
				editor.SelectedView.Selection.StartOffset = startOffset;
				editor.SelectedView.Selection.EndOffset = editor.SelectedView.Selection.StartOffset;
				editor.SelectedView.Selection.SelectToLineStart();
				editor.SelectedView.Selection.SelectToLineEnd();
				editor.SelectedView.ScrollLineToVisibleMiddle();
			}
		}

		private void treeList1_DoubleClick(object sender, EventArgs e)
		{
			if (listErrors.SelectedItems.Count > 0)
			{
				string functionName = listErrors.SelectedItems[0].SubItems[1].ToString();

				if (functionName == "<Project Details>")
				{
					Controller.Instance.MainForm.HidePanelControls(Controller.Instance.MainForm.UcProjectDetails);
				}
				else
				{
					FunctionInfo function = (FunctionInfo)listErrors.SelectedItems[0].Tag;
					MarkErrorsForFunction(function, listErrors.SelectedItems[0]);
				}
			}
		}

		private void ucFunctions_Paint(object sender, PaintEventArgs e)
		{
			BackColor = Colors.BackgroundColor;
		}

		/// <summary>
		/// Adds a descriptive messages to the Output window in the following form:
		/// component: description
		/// </summary>
		/// <param name="component">The component that sent the message.</param>
		/// <param name="description">The message to show to the user.</param>
		internal void AddOutput(string component, string description)
		{
			rtbOutput.AppendText(string.Format("{0}: {1}{2}", component, description, Environment.NewLine));
		}

		/// <summary>
		/// Adds an error messages to the Errors window.
		/// </summary>
		/// <param name="function">The function that caused the error.</param>
		/// <param name="description">The message to show to the user.</param>
		internal void AddError(string function, string description)
		{
			ListViewItem lvi = new ListViewItem(new[] { function, description });
			listErrors.Items.Add(lvi);
		}

		/// <summary>
		/// Removes the specified tab and closes the screen if no more tabs remain.
		/// </summary>
		/// <param name="tab">Tab to remove.</param>
		internal void RemoveTab(TabItem tab)
		{
			foreach (Control ctl in tab.AttachedControl.Controls)
			{
				if (ctl.GetType() == typeof(ucFunction))
				{
					ucFunction function = (ucFunction)ctl;

					if (function.IsDirty)// &&
					//(function.DefaultValueFunction == null ||
					// function.DefaultValueFunction.UseCustomCode))
					{
						DialogResult result = MessageBox.Show(this,
											"You have modified the code of the function you are closing without saving. Save the changes?",
											"Save Changes: " + function.CurrentFunction.Name, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

						switch (result)
						{
							case DialogResult.Yes:
								function.Save();
								break;
							case DialogResult.Cancel:
								return;
						}
					}
					//else if (function.DefaultValueFunction != null &&
					//         !function.DefaultValueFunction.UseCustomCode &&
					//         !function.DefaultValueFunction.IsForUserOption)
					//{
					//    if (Project.Instance.FindFunction(function.CurrentFunction.Name, function.CurrentFunction.Parameters) != null)
					//    {
					//        Project.Instance.DeleteFunction(function.CurrentFunction);
					//    }
					//}
				}
			}
			if (tabStrip1.Tabs.Contains(tab))
			{
				tabStrip1.Tabs.Remove(tab);
			}
			if (tabStrip1.Tabs.Count == 0)
			{
				tabStrip1.Visible = false;
				lblStatus.Visible = true;
				Refresh();
			}
			Refresh();
		}

		private void tabStrip1_TabItemClose(object sender, TabStripActionEventArgs e)
		{
			if (tabStrip1.SelectedTab != null)
			{
				RemoveTab(tabStrip1.SelectedTab);
				// The actual cancel is performed in RemoveTab(), so we need to
				// cancel this removal, otherwise unexpected behaviour occurs.
				e.Cancel = true;
			}
		}

		private void listErrors_Resize(object sender, EventArgs e)
		{
			if (listErrors.Columns[(int)ErrorListColumns.Function].Width + listErrors.Columns[(int)ErrorListColumns.Description].Width < listErrors.Width)
			{
				listErrors.Columns[(int)ErrorListColumns.Description].Width = listErrors.Width - listErrors.Columns[(int)ErrorListColumns.Function].Width;
			}
		}

		private void listErrors_DoubleClick(object sender, EventArgs e)
		{
			if (listErrors.SelectedItems.Count > 0)
			{
				string functionName = listErrors.SelectedItems[0].SubItems[0].ToString();

				if (functionName == "<Project Details>")
				{
					Controller.Instance.MainForm.HidePanelControls(Controller.Instance.MainForm.UcProjectDetails);
				}
				else
				{
					var function = (FunctionInfo)listErrors.SelectedItems[0].Tag;
					MarkErrorsForFunction(function, listErrors.SelectedItems[0]);
				}
			}
		}

		private void mnuTabsClose_Click(object sender, EventArgs e)
		{
			RemoveSelectedTab();
		}

		internal void RemoveSelectedTab()
		{
			RemoveTab(tabStrip1.SelectedTab);
		}

		private void mnuTabsCloseAllButThis_Click(object sender, EventArgs e)
		{
			TabItem selectedTab = tabStrip1.SelectedTab;

			for (int i = tabStrip1.Tabs.Count - 1; i >= 0; i--)
			{
				if (tabStrip1.Tabs[i] != selectedTab)
				{
					RemoveTab(tabStrip1.Tabs[i]);
				}
			}
		}

		private void mnuTabsCloseAll_Click(object sender, EventArgs e)
		{
			for (int i = tabStrip1.Tabs.Count - 1; i >= 0; i--)
			{
				RemoveTab(tabStrip1.Tabs[i]);
			}
		}

		private void listErrors_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
		{
			string footer = "";

			if (e.Item.SubItems.Count > 2)
			{
				footer = string.Format("Line {0}, column {1}", e.Item.SubItems[2].Text, e.Item.SubItems[3].Text);
			}
			var sti = new SuperTooltipInfo(e.Item.Text, footer, e.Item.SubItems[1].Text, imageList32.Images[2], null, eTooltipColor.Office2003);
			SuperTooltipErrors.SetSuperTooltip(listErrors, sti);
			SuperTooltipErrors.ShowTooltip(listErrors, new Point(Cursor.Position.X + 5, Cursor.Position.Y + 15));
		}

		public void PopulateFunctionList()
		{
			try
			{
				superTooltip1 = new SuperTooltip();
				superTooltip1.BeforeTooltipDisplay += superTooltip1_BeforeTooltipDisplay;
				List<string> specialFunctions = Project.Instance.InternalFunctionNames;

				treeFunctions.BeginUpdate();
				treeFunctions.Nodes.Clear();

				foreach (string category in Project.Instance.FunctionCategories)
				{
					RemoveTabsOfDeletedFunctions();
					var categoryNode = new Node();
					categoryNode.Text = " " + (string.IsNullOrEmpty(category) ? "General" : category);
					bool categoryAdded = false;

					foreach (FunctionInfo function in Project.Instance.Functions)
					{
						if (function.Category != category || specialFunctions.BinarySearch(function.Name) >= 0)
						{
							continue;
						}
						if (function.IsExtensionMethod)
							continue;
						if (!categoryAdded)
						{
							categoryNode.Style = treeFunctions.Styles["elementStyleGroup"];
							treeFunctions.Nodes.Add(categoryNode);
							categoryAdded = true;
						}
						var functionNode = new Node { Text = function.Name, Tag = function };

						if (!function.IsTemplateFunction)
						{
							functionNode.Image = imageListFunctions.Images[0];
						}
						else
						{
							switch (SyntaxEditorHelper.LanguageEnumFromName(function.TemplateReturnLanguage))
							{
								case TemplateContentLanguage.CSharp:
									functionNode.Image = imageListFunctions.Images[3];
									break;
								case TemplateContentLanguage.VbDotNet:
									functionNode.Image = imageListFunctions.Images[5];
									break;
								case TemplateContentLanguage.Sql:
									functionNode.Image = imageListFunctions.Images[0];
									break;
								case TemplateContentLanguage.Html:
									functionNode.Image = imageListFunctions.Images[4];
									break;
								case TemplateContentLanguage.Css:
									functionNode.Image = imageListFunctions.Images[2];
									break;
								case TemplateContentLanguage.IniFile:
									functionNode.Image = imageListFunctions.Images[0];
									break;
								case TemplateContentLanguage.JScript:
									functionNode.Image = imageListFunctions.Images[0];
									break;
								case TemplateContentLanguage.Python:
									functionNode.Image = imageListFunctions.Images[0];
									break;
								case TemplateContentLanguage.VbScript:
									functionNode.Image = imageListFunctions.Images[5];
									break;
								case TemplateContentLanguage.Xml:
									functionNode.Image = imageListFunctions.Images[6];
									break;
								case TemplateContentLanguage.PlainText:
									functionNode.Image = imageListFunctions.Images[0];
									break;
								default:
									functionNode.Image = imageListFunctions.Images[0];
									//throw new Exception("This function return type not handled yet in CreateDirectiveXmlToCSharpLanguage: " + function.ReturnType);
									break;
							}
						}
						//toolTipForNavBar.SetToolTip(functionNode, string.Format("({1}) {0}", function.Name, function.ReturnType));
						var sti = new SuperTooltipInfo(function.Name, "", function.Description, functionNode.Image, null, eTooltipColor.Office2003);
						superTooltip1.SetSuperTooltip(functionNode, sti);
						categoryNode.Nodes.Add(functionNode);
					}
				}
				foreach (Node node in treeFunctions.Nodes)
				{
					node.ExpandAll();
					node.Expand();
				}
			}
			finally
			{
				treeFunctions.EndUpdate();
			}
		}

		private void treeFunctions_NodeClick(object sender, TreeNodeMouseEventArgs e)
		{
			if (e.Node.Tag != null)
			{
				ShowFunction((FunctionInfo)e.Node.Tag, null);
			}
		}

		private void superTooltip1_BeforeTooltipDisplay(object sender, SuperTooltipEventArgs e)
		{
			//DevComponents.DotNetBar.SuperTooltip tooltip = (DevComponents.DotNetBar.SuperTooltip)sender;
			e.Location = new Point(Cursor.Position.X + 15, Cursor.Position.Y + 5);
		}

		//public void SetLocalVariables(IEnumerable<LocalVariableInformation> infos)
		//{
		//    IsPopulated = false;
		//    localVariableList.Nodes.Clear();

		//    foreach(var info in infos)
		//    {
		//        AddLocalVariable(info, localVariableList.Nodes);
		//    }

		//    if(infos.Count() > 0)
		//        bar1.SelectedDockContainerItem = dockLocalVariables;

		//    IsPopulated = true;
		//}

		//private void AddLocalVariable(LocalVariableInformation info, NodeCollection parentNodeCollection)
		//{
		//    var node = new Node
		//                   {
		//                       Text = info.Name,
		//                       Tag = info
		//                   };
		//    node.Cells.Add(new Cell(info.StringValue));
		//    if(info.IsPrimitive == false)
		//    {
		//        // Forces the node to have an expand symbol next to it.
		//        node.Nodes.Add(new Node());
		//    }
		//    parentNodeCollection.Add(node);
		//}

		//private void AddChildrenToTreeview(LocalVariableInformation info, Node node)
		//{
		//    node.Nodes.Clear();
		//    foreach(var field in info.Fields)
		//    {
		//        AddLocalVariable(field, node.Nodes);
		//    }
		//}

		private readonly HashSet<Node> PopulatedCollectionNodes = new HashSet<Node>();

		private void localVariableList_BeforeExpand(object sender, AdvTreeNodeCancelEventArgs e)
		{
			//if (IsPopulated)
			//{
			//    if (!PopulatedCollectionNodes.Contains(e.Node))
			//    {
			//        Cursor = Cursors.WaitCursor;
			//        AddChildrenToTreeview(e.Node.Tag as LocalVariableInformation, e.Node);
			//        Cursor = Cursors.Default;
			//    }
			//}
		}
	}
}
