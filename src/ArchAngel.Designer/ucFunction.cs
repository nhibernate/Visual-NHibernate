using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ActiproSoftware.Drawing;
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.SyntaxEditor.Addons.Dynamic;
using ArchAngel.Common.DesignerProject;
//using ArchAngel.Debugger;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Designer.Properties;
using ArchAngel.Designer.Wizards;
using ArchAngel.Interfaces;
using DevComponents.DotNetBar;
using Slyce.Common;
using Slyce.Common.EventExtensions;
using UserOption = ArchAngel.Common.DesignerProject.UserOption;

namespace ArchAngel.Designer
{
	public partial class ucFunction : UserControl
	{
		private bool UseSplitLanguage;
		private string m_functionName = "";
		private FunctionInfo _CurrentFunction;
		private UserOption _CurrentUserOption;
		private FunctionTypes _CurrentUserOptionFunctionType;
		private const int Gap = 10;
		public string ReturnType = "";
		private bool _isDirty;
		private bool BusyPopulating;
		public bool AllowEdit = true;
		internal object[] ParametersToPass;
		internal bool[] ValuesThatHaveBeenSet;

		public event EventHandler IsDirtyChanged;
		public event EventHandler OverrideFunctionChanged;
		public event EventHandler ResetDefaultCode;

		#region Preview Members
		private FunctionRunner _FunctionRunner;
		private readonly HighlightingStyle newTextHighlightStyle;
		private int _CurrentPreviewTextOffset;
		private int _PreviousPreviewTextLength;
		#endregion

		private const string BreakpointLayerKey = "breakpoint-layer";
		private const string CurrentStatementLayerKey = "current-statement-layer";
		private const string NewPreviewTextLayerKey = "new-preview-text-layer";

		internal bool IsDirty
		{
			get { return _isDirty; }
			private set
			{
				if (value == _isDirty) return;

				_isDirty = value;
				IsDirtyChanged.RaiseEvent(this);
			}
		}


		private bool allowOverride;

		internal bool AllowOverride
		{
			get
			{
				return allowOverride;
			}
			set
			{
				if (allowOverride == value)
					return;

				allowOverride = value;
				chkOverrideDefaultValueFunction.Visible = value;
				btnResetDefaultCode.Visible = value;
				if (allowOverride == false)
				{
					chkOverrideDefaultValueFunction.Checked = true;
				}
			}
		}

		internal bool OverrideFunctionChecked
		{
			get
			{
				if (AllowOverride == false)
					return true;

				return chkOverrideDefaultValueFunction.Checked;
			}
			set
			{
				chkOverrideDefaultValueFunction.Checked = value;
			}
		}

		public ucFunction()
		{
			InitializeComponent();
			if (Utility.InDesignMode)
			{
				return;
			}

			EnableDoubleBuffering();
			syntaxEditor1.Height = ClientSize.Height - btnEdit.Bottom - Gap;
			//btnTest.Visible = false;
			if (_CurrentFunction != null)
			{
				ReconstructFunctionRunner();
			}

			syntaxEditor1.Document.SpanIndicatorLayers.Add(new SpanIndicatorLayer(BreakpointLayerKey, 1));
			syntaxEditor1.Document.SpanIndicatorLayers.Add(new SpanIndicatorLayer(CurrentStatementLayerKey, 10));

			syntaxEditorPreviewText.Document.SpanIndicatorLayers.Add(new SpanIndicatorLayer(NewPreviewTextLayerKey, 1));

			newTextHighlightStyle = new HighlightingStyle("New Text Style", "New Text", Color.Red, Color.LightGray);

			Controller.Instance.SettingChangedEvent += Instance_SettingChangedEvent;
			SetEditorFontSize((float)Settings.Default.EditorFontSize);

			OverrideFunctionChecked = true;
		}

		private void ucFunction_Load(object sender, EventArgs e)
		{
			SetupSyntaxDocumentsFromOverrideDefault();
		}

		void Instance_SettingChangedEvent(string name, object oldValue, object newValue)
		{
			if (name == "EditorFontSize")
			{
				SetEditorFontSize((float)(double)newValue);
			}
		}

		private void SetEditorFontSize(float size)
		{
			Font font = new Font(FontFamily.GenericMonospace, size);
			syntaxEditor1.Font = syntaxEditor1.LineNumberMarginFont = font;
			syntaxEditorPreviewText.Font = syntaxEditorPreviewText.LineNumberMarginFont = font;
		}

		public FunctionInfo CurrentFunction
		{
			get { return _CurrentFunction; }
			set
			{
				_CurrentFunction = value;
				ReconstructFunctionRunner();
			}
		}

		public UserOption CurrentUserOption
		{
			get { return _CurrentUserOption; }
			set
			{
				_CurrentUserOption = value;
				//ReconstructFunctionRunner();
			}
		}

		public FunctionTypes CurrentUserOptionFunctionType
		{
			get { return _CurrentUserOptionFunctionType; }
			set
			{
				_CurrentUserOptionFunctionType = value;
				//ReconstructFunctionRunner();
			}
		}

		/*
				private void preview_ContextMenuRequested(object sender, ContextMenuRequestEventArgs e)
				{
					ContextMenu menu = syntaxEditorPreviewText.GetDefaultContextMenu();
					menu.MenuItems.Add(0, new MenuItem("Break before this", new EventHandler(BreakBeforeThis)));
					menu.MenuItems.Add(1, new MenuItem("-"));
					menu.Show(syntaxEditorPreviewText, e.MenuLocation);
					syntaxEditorPreviewText.DefaultContextMenuEnabled = false;
				}
		*/

		private void EnableDoubleBuffering()
		{
			// Set the value of the double-buffering style bits to true.
			SetStyle(ControlStyles.DoubleBuffer |
					 ControlStyles.UserPaint |
					 ControlStyles.AllPaintingInWmPaint,
					 true);
			UpdateStyles();
		}

		/// <summary>
		/// Gets the FunctionRunner instance that this function screen uses to run the debugger.
		/// </summary>
		internal FunctionRunner FunctionRunner
		{
			get { return _FunctionRunner; }
		}

		/// <summary>
		/// The main SyntaxEditor that contains the template code.
		/// </summary>
		public SyntaxEditor SyntaxEditor
		{
			get { return syntaxEditor1; }
		}

		/// <summary>
		/// Gets or Sets the name of the function that this control is displaying.
		/// </summary>
		public string FunctionName
		{
			get { return m_functionName; }
			set
			{
				//throw new NotImplementedException("GFH: this function shouldn't exist.");
				Utility.CheckForNulls(new object[] { value }, new[] { "value" });
				m_functionName = value.Trim();
				//_CurrentFunction = Project.Instance.FindFunction(FunctionName);
				ReconstructFunctionRunner();
			}
		}

		/// <summary>
		/// Runs the debugger over the displayed function, and displays the output text
		/// in the preview window. This function should not be called directly by anyone
		/// except the controller. Call StartDebugger() instead.
		/// </summary>
		/// <returns>The result of the debug operation. Could be an exception, or a DebugInformation object, or null.</returns>
		internal object CallFunction()
		{
			return _FunctionRunner.CallFunction();
		}

		/// <summary>
		/// Clears the indicator layers in the preview window.
		/// </summary>
		public void ClearDebugInformation()
		{
			syntaxEditor1.Document.SpanIndicatorLayers[CurrentStatementLayerKey].Clear();
		}

		/// <summary>
		/// Clears the breakpoints from the GUI and the debugger.
		/// </summary>
		internal void ClearBreakpoints()
		{
			FunctionRunner.ClearBreakpoints();
			ClearDebugInformation();
		}

		/// <summary>
		/// Clears the preview window and resets any indicators.
		/// </summary>
		public void ClearPreviewText()
		{
			_CurrentPreviewTextOffset = 0;
			_PreviousPreviewTextLength = 0;
			syntaxEditorPreviewText.Text = "";
			ClearDebugInformation();
		}

		///// <summary>
		///// Updates the UI with the latest information from the debugger.
		///// </summary>
		///// <param name="di">The information about the current state of the debugger.</param>
		///// <param name="finished">True if the debugger has finished executing.</param>
		//public void UpdateDebugState(DebugInformation di, bool finished)
		//{
		//    if (finished)
		//    {
		//        ChangeOutputText(di.CurrentOutput);
		//        StopProgressBar();
		//        functionToolStripStatusLabel.Text = "Debugger Stopped";
		//        return;
		//    }

		//    ChangeOutputText(di.CurrentOutput);
		//    SetLocalVariables(di.LocalVariableInformation);

		//    if (!di.Stopped) return;

		//    if (di.StopReason == StopReason.ExceptionOccurred)
		//    {
		//        MessageBox.Show(di.ExceptionInformation.Message, "Error updating debug state", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//    }
		//    else
		//        SetCurrentStatement(di);
		//}

		//private void SetLocalVariables(IEnumerable<LocalVariableInformation> information)
		//{
		//    Controller.Instance.MainForm.UcFunctions.SetLocalVariables(information);
		//}

		///// <summary>
		///// Sets the currently executing statement. Clears the previous marker
		///// and adds a new CurrentStatementSpanIndicator to the current statement
		///// layer.
		///// </summary>
		///// <param name="di">The information passed back from the debugger. Contains the information needed
		///// to highlight the correct text in the syntax editor.</param>
		//public void SetCurrentStatement(DebugInformation di)
		//{
		//    int compiledLineNumber = di.StartLineNumber - 1, compiledColumnNumber = di.StartColumnNumber;
		//    List<CompiledToTemplateLineLookup> lookup =
		//        CompileHelper.TemplateLinesLookup[compiledLineNumber];
		//    int lookupIndex = 0;

		//    if (lookup.Count > 1)
		//    {
		//        for (int i = lookup.Count - 1; i >= 0; i--)
		//        {
		//            lookupIndex = i;
		//            CompiledToTemplateLineLookup ll = lookup[i];
		//            if (ll.CompiledColumn <= compiledColumnNumber)
		//                break;
		//        }
		//    }

		//    int startOffset, endOffset;

		//    // Calculate startOffset
		//    DocumentLine startline;
		//    if (compiledColumnNumber >= lookup[lookup.Count - 1].CompiledColumn + lookup[lookup.Count - 1].SnippetLength)
		//    {
		//        startline = GetStartline_CodeAtEndOfLine(lookup, compiledColumnNumber, out startOffset);
		//    }
		//    else if (compiledColumnNumber >= lookup[lookupIndex].CompiledColumn + lookup[lookupIndex].SnippetLength)
		//    {
		//        // The startOffset is between two <% %> blocks, like so
		//        // <% ... %> The text we are stopped on <% ... %>
		//        startline = GetStartline_TextBetweenCodeSnippets(lookup[lookupIndex], out startOffset);
		//    }
		//    else
		//    {
		//        startline = GetStartline(lookup[lookupIndex], compiledColumnNumber, out startOffset);
		//    }

		//    TextStream stream = syntaxEditor1.Document.GetTextStream(startOffset);
		//    SyntaxLanguage language = stream.Token.Language;

		//    if (language.Tag.ToString() == "ScriptLanguage")
		//    {
		//        if (compiledColumnNumber != 0 &&
		//            lookup[lookupIndex].CompiledColumn + lookup[lookupIndex].SnippetLength == 0)
		//        {
		//            // If there is no offset information about this specific piece of code,
		//            // assume this line in the template only contains code at the start (no <% ).
		//            // Just shift by the column number we get from the debugger.
		//            // An example of this would be stepping through foreach statements - each
		//            // piece of the statement is stepped though individually.
		//            startOffset = startline.StartOffset + compiledColumnNumber;

		//            endOffset = GetEndOffsetScriptLanguage(di, startline, startOffset);
		//        }
		//        else if (compiledColumnNumber != lookup[lookupIndex].CompiledColumn)
		//        {
		//            // Fix for multiple statements on one line.
		//            // <% Write(sb, "something"); if(somethingelse) { %>
		//            startOffset += compiledColumnNumber - lookup[lookupIndex].CompiledColumn;

		//            endOffset = GetEndOffsetScriptLanguage(di, startline, startOffset);
		//        }
		//        else if (syntaxEditor1.Document[startOffset - 3] == '=')
		//        {
		//            // Fix for <%= %> blocks.
		//            // Shift the stream to before the <%=
		//            stream.Offset = startOffset - 5;
		//            if (stream.TokenText == "<%")
		//            {
		//                startOffset -= 2;
		//                endOffset = GetEndOffsetLanguageBlock(syntaxEditor1, startOffset, "ScriptLanguage");
		//            }
		//            else // This is not a <%= %> block so treat it like normal.
		//                endOffset = GetEndOffsetScriptLanguage(di, startline, startOffset);
		//        }
		//        else // No special fixes - just use the start offset calculated before.
		//            endOffset = GetEndOffsetScriptLanguage(di, startline, startOffset);
		//    }
		//    else
		//    {
		//        // The -2 is to bring the offset back before the <%
		//        endOffset = GetEndOffsetLanguageBlock(syntaxEditor1, startOffset, "TemplateLanguage");
		//        if (syntaxEditor1.Document.Lines[syntaxEditor1.Document.Lines.Count - 1].EndOffset != endOffset)
		//            endOffset -= 2;
		//    }

		//    SpanIndicatorLayer layer = syntaxEditor1.Document.SpanIndicatorLayers[CurrentStatementLayerKey];

		//    TextRange range = new TextRange(startOffset, endOffset);
		//    layer.Clear();
		//    if (range.IsZeroLength)
		//    {
		//        // This forces the debugger to skip lines that cannot be displayed.
		//        // This may not be a good idea. I can't think of a better option at 
		//        // this point though.
		//        Controller.Instance.TriggerNextDebugAction();
		//    }
		//    else
		//    {
		//        layer.Add(new CurrentStatementSpanIndicator(), range);
		//        functionToolStripStatusLabel.Text = "Breakpoint hit";
		//    }

		//    syntaxEditor1.SuspendPainting();
		//    syntaxEditor1.SelectedView.GotoNextSpanIndicator(layer, "Current Statement");
		//    syntaxEditor1.SelectedView.Selection.Collapse();
		//    syntaxEditor1.ResumePainting();
		//}

		/// <summary>
		/// Gets the line and start offset of the current statement, making no assumptions
		/// about the position of the current statement. Naive implementation, will fail
		/// for the cases that have specific functions for this. The specific functions will
		/// be named GetStartLine_[condition], where condition describes the situation in which
		/// the function should be used. If there is no other function which covers the current
		/// situation, use this one.
		/// </summary>
		/// <param name="linelookup">The line lookup to search in.</param>
		/// <param name="compiledColumnNumber">The column number of the current statement in the complied code.</param>
		/// <returns>The DocumentLine that the current statement starts on.</returns>
		/// <param name="startOffset"></param>
		private DocumentLine GetStartline(CompiledToTemplateLineLookup linelookup,
										  int compiledColumnNumber, out int startOffset)
		{
			DocumentLine startline = syntaxEditor1.Document.Lines[linelookup.TemplateLineNumber];

			if (compiledColumnNumber == 0 && CheckForClosingASPTag(startline.StartOffset))
			{
				startOffset = startline.StartOffset + 2;
			}
			else
			{
				startOffset = startline.StartOffset + linelookup.TemplateColumn;
			}
			return startline;
		}

		/// <summary>
		/// Gets the line and start offset of the current statement, assuming
		/// it is between two code snippets (<% %> current statement <% %>).
		/// </summary>
		/// <param name="linelookup">The line lookup to search in.</param>
		/// <param name="startOffset">The variable to place the start offset in.</param>
		/// <returns>The DocumentLine that the current statement starts on.</returns>
		private DocumentLine GetStartline_TextBetweenCodeSnippets(CompiledToTemplateLineLookup linelookup,
																  out int startOffset)
		{
			DocumentLine startline = syntaxEditor1.Document.Lines[linelookup.TemplateLineNumber];

			startOffset = startline.StartOffset + linelookup.TemplateColumn +
						  linelookup.SnippetLength + 2;

			TextStream stream = syntaxEditor1.Document.GetTextStream(startOffset);

			while (stream.TokenText != "%>")
				stream.ReadTokenReverse();

			startOffset = stream.Token.EndOffset;

			return startline;
		}

		/// <summary>
		/// Gets the line and start offset of the current statement, assuming
		/// it is at the end of a line.
		/// </summary>
		/// <param name="lookup">The list of line lookups to search in.</param>
		/// <param name="compiledColumnNumber">The start column number of the current statement in the compiled code.</param>
		/// <param name="startOffset">The variable to place the start offset in.</param>
		/// <returns>The DocumentLine that the current statement starts on.</returns>
		private DocumentLine GetStartline_CodeAtEndOfLine(IList<CompiledToTemplateLineLookup> lookup,
														  int compiledColumnNumber, out int startOffset)
		{
			DocumentLine startline = syntaxEditor1.Document.Lines[lookup[lookup.Count - 1].TemplateLineNumber];
			startOffset = startline.StartOffset + lookup[lookup.Count - 1].TemplateColumn +
						  lookup[lookup.Count - 1].SnippetLength;

			if (compiledColumnNumber != 0 &&
				lookup[lookup.Count - 1].CompiledColumn + lookup[lookup.Count - 1].SnippetLength == 0)
			{
				// The current piece of code is at the end of a line.
				// There is either going to be a %> immediately, compiledColumnNumber characters
				// ahead, or not at all.
				if (CheckForOpeningAndClosingASPTag(startOffset))
					startOffset += 2;
				else
				{
					startOffset += compiledColumnNumber;

					if (CheckForOpeningAndClosingASPTag(startOffset))
						startOffset += 2;
					else
						startOffset -= compiledColumnNumber;
				}
			}
			else
			{
				if (CheckForOpeningAndClosingASPTag(startOffset))
					startOffset += 2;
				// Fix for lines like this:
				// <%= Something(); %>
				// asdfjkaldfj
				// Was incorrectly highlighting the > of the %> tag on the first line.
				TextStream stream = syntaxEditor1.Document.GetTextStream(startOffset);
				if (stream.Token.Key == "ASPDirectiveEndToken")
					startOffset++;
			}
			return startline;
		}

		/// <summary>
		/// Gets the end offset of the specified language.
		/// </summary>
		/// <param name="editor">The editor that contains the Document to search.</param>
		/// <param name="startOffset">The offset to start searching from.</param>
		/// <param name="languageTag">The value of language.Tag.ToString() that we are currently in.</param>
		/// <returns></returns>
		private static int GetEndOffsetLanguageBlock(SyntaxEditor editor, int startOffset, string languageTag)
		{
			TextStream stream = editor.Document.GetTextStream(startOffset);
			SyntaxLanguage language = stream.Token.Language;
			while (language.Tag.ToString() == languageTag)
			{
				if (stream.GoToNextToken() == false)
				{
					return stream.Token.EndOffset;
				}
				language = stream.Token.Language;
			}
			return stream.Token.StartOffset;
		}

		///// <summary>
		///// Gets the end offset for the current statement span, given that we are in a script language block.
		///// </summary>
		///// <param name="di">The DebugInformation object that contains the information about the current
		///// state of the debugger.</param>
		///// <param name="startline">The DocumentLine that the start offset is on.</param>
		///// <param name="startOffset">The start offset calculated for the current statement.</param>
		///// <returns>The end offset of the current statement span in the current Document.</returns>
		//private int GetEndOffsetScriptLanguage(DebugInformation di, DocumentLine startline, int startOffset)
		//{
		//    int totalOffset = 0;
		//    if (di.StartLineNumber == di.EndLineNumber)
		//    {
		//        totalOffset = (di.EndColumnNumber - di.StartColumnNumber);
		//    }
		//    else
		//    {
		//        int startFunctionLineNumber = startline.Index + 1;
		//        int lastFunctionLineNumber =
		//            CompileHelper.TemplateLinesLookup[di.EndLineNumber - 1][0].TemplateLineNumber;
		//        // Add the length of the code on the first line to the totalOffset
		//        totalOffset += syntaxEditor1.Document.Lines[startFunctionLineNumber].StartOffset - startOffset;
		//        // Add the length of every line excluding the last one to the totalOffset
		//        for (int i = startFunctionLineNumber; i < lastFunctionLineNumber; i++)
		//        {
		//            DocumentLine currentLine = syntaxEditor1.Document.Lines[i];
		//            if (i < syntaxEditor1.Document.Lines.Count)
		//            {
		//                DocumentLine nextLine = syntaxEditor1.Document.Lines[i + 1];
		//                totalOffset += nextLine.StartOffset - currentLine.StartOffset;
		//            }
		//            else
		//                totalOffset += currentLine.EndOffset - currentLine.StartOffset;
		//        }

		//        totalOffset += di.EndColumnNumber;
		//    }

		//    int endOffset = startOffset + totalOffset;

		//    return endOffset;
		//}

		/// <summary>
		/// Checks to see if the next text after index is "<%" or "%>"
		/// </summary>
		/// <param name="index">The index to start from.</param>
		/// <returns>true if the characters "<%" or "%>" are at index and index+1</returns>
		private bool CheckForOpeningAndClosingASPTag(int index)
		{
			string nextTwoCharacters = "" + syntaxEditor1.Document[index] +
									   syntaxEditor1.Document[index + 1];
			if (nextTwoCharacters == "%>" || nextTwoCharacters == "<%")
				return true;
			return false;
		}

		/// <summary>
		/// Checks to see if the next text after index is "%>"
		/// </summary>
		/// <param name="index">The index to start from.</param>
		/// <returns>true if the characters "%>" are at index and index+1</returns>
		private bool CheckForClosingASPTag(int index)
		{
			string nextTwoCharacters = "" + syntaxEditor1.Document[index] +
									   syntaxEditor1.Document[index + 1];
			if (nextTwoCharacters == "%>")
				return true;
			return false;
		}

		/// <summary>
		/// Populates the form with information about the current function.
		/// </summary>
		public void Populate()
		{
			//_CurrentFunction = Project.Instance.FindFunction(FunctionName);

			if (_CurrentFunction == null && CurrentUserOption == null)
			{
				MessageBox.Show(this, "Function cannot be found.", "Missing Function", MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}

			ReconstructFunctionRunner();

			BusyPopulating = true;
			PopulateFunctionHeader();
			PopulateFunctionBody();
			PopulateDebugParameters();

			SetupSyntaxDocumentsFromOverrideDefault();

			BusyPopulating = false;
		}

		/// <summary>
		/// Fills the debug parameters grid with values.
		/// </summary>
		private void PopulateDebugParameters()
		{
			treeDebugParams.Nodes.Clear();
			ParametersToPass = new object[CurrentFunction.Parameters.Count];
			bool parametersMatch = false;
			List<object> parameterObjects = null;
			ValuesThatHaveBeenSet = new bool[CurrentFunction.Parameters.Count];

			if (FunctionRunner.CachedParameters.ContainsKey(CurrentFunction))
			{
				parameterObjects = FunctionRunner.CachedParameters[CurrentFunction];
				parametersMatch = CurrentFunction.Parameters.Count == parameterObjects.Count;

				if (parametersMatch)
				{
					for (int i = 0; i < CurrentFunction.Parameters.Count; i++)
					{
						if (parameterObjects[i] == null || !CurrentFunction.Parameters[i].DataType.IsInstanceOfType(parameterObjects[i]))
						{
							parametersMatch = false;
						}
					}
				}
			}
			for (int i = 0; i < CurrentFunction.Parameters.Count; i++)
			{
				if (parametersMatch)
				{
					ParametersToPass[i] = parameterObjects[i];
					ValuesThatHaveBeenSet[i] = true;
				}
			}
			for (int i = 0; i < CurrentFunction.Parameters.Count; i++)
			{
				ParamInfo param = CurrentFunction.Parameters[i];
				string displayName = param.Name;
				//string displayName = string.Format("{2}. {0} ({1}):", param.Name, param.DataType, i + 1);
				string displayValue = "";
				DevComponents.AdvTree.Node node = new DevComponents.AdvTree.Node();
				node.Cells[0].StyleNormal = treeDebugParams.Styles["elementStyle1"];
				node.Tag = param;
				Control editControl = null;
				object existingParamObject = null;
				bool isBasicType = true;

				switch (param.DataType.Name.ToLower())
				{
					case "bool":
					case "boolean":
						editControl = new DevComponents.DotNetBar.Controls.CheckBoxX();
						((DevComponents.DotNetBar.Controls.CheckBoxX)editControl).Checked = ValuesThatHaveBeenSet[i] ? (bool)ParametersToPass[i] : false;
						((DevComponents.DotNetBar.Controls.CheckBoxX)editControl).CheckedChangedEx += DebugParamChanged_CheckboxX;
						break;
					case "int":
					case "int32":
						editControl = new DevComponents.Editors.IntegerInput();
						((DevComponents.Editors.IntegerInput)editControl).Value = ValuesThatHaveBeenSet[i] ? (int)ParametersToPass[i] : 0;
						((DevComponents.Editors.IntegerInput)editControl).ValueChanged += DebugParamChanged_IntegerInput;
						break;
					case "double":
						editControl = new DevComponents.Editors.DoubleInput();
						((DevComponents.Editors.DoubleInput)editControl).Value = ValuesThatHaveBeenSet[i] ? (double)ParametersToPass[i] : 0;
						((DevComponents.Editors.DoubleInput)editControl).ValueChanged += DebugParamChanged_DoubleInput;
						break;
					case "string":
						editControl = new DevComponents.DotNetBar.Controls.TextBoxX();
						editControl.Text = ValuesThatHaveBeenSet[i] ? (string)ParametersToPass[i] : "";
						editControl.TextChanged += DebugParamChanged_TextBoxX;
						break;
					default:
						//MessageBox.Show(string.Format("Parameter-type not handled yet: {0}.\n\nPlease contact support@slyce.com", param.DataType.Name), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						//return;
						isBasicType = false;
						break;
				}
				if (isBasicType)
				{
					node.Text = string.Format("{0} ({1})", param.Name, param.DataType);
				}
				//if (param.DataType.GetInterface("ArchAngel.Interfaces.IScriptBaseObject") != null)
				if (!isBasicType)
				{
					if (ParametersToPass[i] != null)
					{
						displayValue = ProviderInfo.GetDisplayName(ParametersToPass[i]);
					}
					else
					{
						// Let's check whether another function has previously set a value for this kind of object...
						foreach (List<object> paramList in FunctionRunner.CachedParameters.Values)
						{
							foreach (object existingParam in paramList)
							{
								if (param.DataType.IsInstanceOfType(existingParam))
								{
									existingParamObject = existingParam;
									ValuesThatHaveBeenSet[i] = true;
									ParametersToPass[i] = existingParamObject;
									displayValue = ProviderInfo.GetDisplayName(existingParam);
								}
							}
						}
						if (existingParamObject == null)
						{
							//node.Cells[0].StyleNormal = treeDebugParams.Styles["elementStyleMissing"];
							node.ImageIndex = 1;
							displayValue = "Click to set";
						}
					}
					//editControl = new DevComponents.DotNetBar.Controls.ComboTree();
					editControl = new ButtonX();
					editControl.Text = editControl.Text = ValuesThatHaveBeenSet[i] ? displayValue : "Click to set";
					editControl.Text = string.Format("<u>{0}</u>", editControl.Text);
					editControl.Tag = existingParamObject;
					((ButtonX)editControl).ColorTable = eButtonColor.Flat;
					((ButtonX)editControl).Style = eDotNetBarStyle.OfficeXP;
					((ButtonX)editControl).FocusCuesEnabled = false;
					editControl.Cursor = Cursors.Hand;
					editControl.Click += DebugParamChanged_ButtonX;

					//string[] prevSelections = new string[] { "red", "blue", "green"};

					//foreach (string prevSelection in prevSelections)
					//{
					//    DevComponents.AdvTree.Node prevSelNode = new DevComponents.AdvTree.Node();
					//    prevSelNode.Text = prevSelection;
					//    //((DevComponents.DotNetBar.Controls.ComboTree)editControl).Nodes.Add(prevSelNode);
					//    //((ComboBox)editControl).Items.Add(prevSelection);
					//    //((DevComponents.DotNetBar.Controls.ComboTree)editControl).Dock = DockStyle.Fill;
					//    //((DevComponents.DotNetBar.Controls.ComboTree)editControl).SelectedIndex = 0;
					//}
					node.Text = displayName;
					//node.he
				}
				DevComponents.AdvTree.Cell cell = new DevComponents.AdvTree.Cell();
				cell.HostedControl = editControl;
				cell.StyleNormal = treeDebugParams.Styles["elementStyle1"];
				node.Cells.Add(cell);
				treeDebugParams.Nodes.Add(node);
			}
			SetCachedParameters();
		}

		void DebugParamChanged_ButtonX(object sender, EventArgs e)
		{
			if (!Controller.Instance.CheckDebugOptions(false))
			{
				return;
			}

			Cursor = Cursors.WaitCursor;
			ButtonX button = (ButtonX)sender;

			ParamInfo p = (ParamInfo)treeDebugParams.SelectedNode.Tag;
			Type dataType = p.DataType;
			frmSelectModelObject form = frmSelectModelObject.Instance;
			form.SelectedObject = button.Tag;
			form.ShowObject(this, dataType);

			if (form.SelectedObject != null)
			{
				int index = treeDebugParams.SelectedIndex;
				ParametersToPass[index] = form.SelectedObject;
				button.Tag = form.SelectedObject;

				if (form.SelectedObject != null)
				{
					button.Text = ProviderInfo.GetDisplayName(form.SelectedObject);
					treeDebugParams.SelectedNode.ImageIndex = -1;
					ValuesThatHaveBeenSet[index] = true;
				}
			}
			SetCachedParameters();
			Controller.Instance.MainForm.Activate();
			Cursor = Cursors.Default;
		}

		void DebugParamChanged_TextBoxX(object sender, EventArgs e)
		{
			ParametersToPass[treeDebugParams.SelectedIndex] = ((DevComponents.DotNetBar.Controls.TextBoxX)sender).Text;
			ValuesThatHaveBeenSet[treeDebugParams.SelectedIndex] = true;
			SetCachedParameters();
		}

		void DebugParamChanged_DoubleInput(object sender, EventArgs e)
		{
			ParametersToPass[treeDebugParams.SelectedIndex] = ((DevComponents.Editors.DoubleInput)sender).Value;
			ValuesThatHaveBeenSet[treeDebugParams.SelectedIndex] = true;
			SetCachedParameters();
		}

		void DebugParamChanged_IntegerInput(object sender, EventArgs e)
		{
			ParametersToPass[treeDebugParams.SelectedIndex] = ((DevComponents.Editors.IntegerInput)sender).Value;
			ValuesThatHaveBeenSet[treeDebugParams.SelectedIndex] = true;
			SetCachedParameters();
		}

		void DebugParamChanged_CheckboxX(object sender, DevComponents.DotNetBar.Controls.CheckBoxXChangeEventArgs e)
		{
			ParametersToPass[treeDebugParams.SelectedIndex] = ((DevComponents.DotNetBar.Controls.CheckBoxX)sender).Checked;
			ValuesThatHaveBeenSet[treeDebugParams.SelectedIndex] = true;
			SetCachedParameters();
		}

		/// <summary>
		/// Recreates the FunctionRunner. Call this if the CurrentFunction changes.
		/// </summary>
		private void ReconstructFunctionRunner()
		{
			_FunctionRunner = new FunctionRunner(this, _CurrentFunction);
		}

		/// <summary>
		/// Sets the main syntax editor's text to the body of the current function.
		/// </summary>
		public void PopulateFunctionBody()
		{
			//syntaxEditor1.SuspendPainting();

			if (_CurrentFunction != null)
			{
				syntaxEditor1.Text = _CurrentFunction.Body;
			}
			else // UserOption
			{
				switch (CurrentUserOptionFunctionType)
				{
					case FunctionTypes.DefaultValue:
						syntaxEditor1.Text = CurrentUserOption.DefaultValueFunctionBody;
						break;
					case FunctionTypes.Validation:
						syntaxEditor1.Text = CurrentUserOption.ValidatorFunctionBody;
						break;
					case FunctionTypes.DisplayToUser:
						syntaxEditor1.Text = CurrentUserOption.DisplayToUserFunctionBody;
						break;
				}
			}
			syntaxEditor1.SelectedView.ScrollToDocumentStart();
			//syntaxEditor1.ResumePainting();
			IsDirty = false;
		}

		private bool OwnerIsTabStrip { get { return Parent is TabControlPanel; } }

		private TabItem OwnerTabStripPage
		{
			get
			{
				if (OwnerIsTabStrip)
					return ((TabControlPanel)Parent).TabItem;

				return null;
			}
		}

		public void PopulateFunctionHeader()
		{
			btnEdit.Enabled = AllowEdit;
			string paramList;

			//if (DefaultValueFunction != null)
			//{
			//    if (DefaultValueFunction.FunctionType == Project.DefaultValueFunction.FunctionTypes.HelperOverride)
			//    {
			//        chkOverrideDefaultValueFunction.Visible = true;
			//    }
			//    else
			//    {
			//        chkOverrideDefaultValueFunction.Visible = false;
			//        chkOverrideDefaultValueFunction.Checked = true; // Just in case we use this somewhere!
			//    }
			//    // Find the DefaultValueFunction
			//    syntaxEditor1.Document.ReadOnly = !DefaultValueFunction.UseCustomCode;
			//    chkOverrideDefaultValueFunction.Checked = DefaultValueFunction.UseCustomCode;
			//    btnResetDefaultCode.Visible = true;
			//}
			//else
			//{
			chkOverrideDefaultValueFunction.Visible = AllowOverride;
			btnResetDefaultCode.Visible = AllowOverride;
			//}
			string returnTypeName = "void";
			if (CurrentFunction != null)
			{
				returnTypeName = _CurrentFunction.ReturnType != null ? Utility.GetDemangledGenericTypeName(_CurrentFunction.ReturnType, Project.Instance.Namespaces) : "void";
				SetTabStripText(CurrentFunction.Name);
				SetTextLanguage(_CurrentFunction);
				paramList = GetParameterListAsString(CurrentFunction);
			}
			else // UserOption
			{
				switch (CurrentUserOptionFunctionType)
				{
					case FunctionTypes.DefaultValue:
						returnTypeName = Utility.GetDemangledGenericTypeName(CurrentUserOption.VarType, Project.Instance.Namespaces);
						SetTabStripText(string.Format("{0} [Default Value]", CurrentUserOption.VariableName));
						break;
					case FunctionTypes.DisplayToUser:
						returnTypeName = "bool";
						SetTabStripText(string.Format("{0} [Display To User]", CurrentUserOption.VariableName));
						break;
					case FunctionTypes.Validation:
						returnTypeName = "bool";
						SetTabStripText(string.Format("{0} [Validation]", CurrentUserOption.VariableName));
						break;
				}
				Project.Instance.TextLanguage = TemplateContentLanguage.CSharp;
				paramList = GetParameterListAsString(CurrentUserOption, CurrentUserOptionFunctionType);
			}
			ReturnType = returnTypeName;
			lblName.Text = string.Format("<output: {0}> Parameters: ({1})", returnTypeName, paramList);

			SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditor1, Project.Instance.TextLanguage, Project.Instance.CodeLanguage, @"<%", @"%>");
			syntaxEditorPreviewText.Document.Language = SyntaxEditorHelper.GetDynamicLanguage(Project.Instance.TextLanguage);
			SwitchFormatting();

			//if (DefaultValueFunction != null)
			//    SetSyntaxEditorBackColorForDefaultValueFunction(DefaultValueFunction.UseCustomCode);
			//else
			SetSyntaxEditorBackColor();
		}

		private string GetTabStripText()
		{
			if (OwnerIsTabStrip)
				return OwnerTabStripPage.Text;

			return "";
		}

		private void SetTabStripText(string format)
		{
			if (OwnerIsTabStrip)
				OwnerTabStripPage.Text = format;
		}

		/// <summary>
		/// Gets a comma-separated list of parameters and their data-types for the function signature.
		/// </summary>
		/// <param name="userOption"></param>
		/// <param name="userOptionFunctionType"></param>
		/// <returns></returns>
		private string GetParameterListAsString(UserOption userOption, FunctionTypes userOptionFunctionType)
		{
			string paramList = "";

			if (userOptionFunctionType == FunctionTypes.DefaultValue ||
				userOptionFunctionType == FunctionTypes.DisplayToUser)
			{
				if (userOption.IteratorType != null)
				{
					paramList +=
					string.Format("{0} {1}",
								  Utility.GetDemangledGenericTypeName(userOption.IteratorType, Project.Instance.Namespaces).Replace("+", "."),
								  userOption.IteratorType.Name.ToLower());
				}
			}
			else // Validation function
			{
				if (userOption.IteratorType != null)
				{
					paramList +=
						string.Format("{0} {1}",
									  Utility.GetDemangledGenericTypeName(userOption.IteratorType, Project.Instance.Namespaces).Replace("+", "."),
									  userOption.IteratorType.Name.ToLower());
				}
				paramList +=
				string.Format(", {0} value, out string failReason",
								  Utility.GetDemangledGenericTypeName(userOption.VarType, Project.Instance.Namespaces).Replace("+", "."));
			}
			return paramList;
		}

		/// <summary>
		/// Gets a comma-separated list of parameters and their data-types for the function signature.
		/// </summary>
		/// <param name="function"></param>
		/// <returns></returns>
		private string GetParameterListAsString(FunctionInfo function)
		{
			string paramList = "";

			for (int i = 0; i < function.Parameters.Count; i++)
			{
				ParamInfo param = function.Parameters[i];

				if (param.Modifiers.Length > 0)
				{
					paramList += param.Modifiers + " ";
				}
				paramList +=
					string.Format("{0} {1}",
								  Utility.GetDemangledGenericTypeName(param.DataType, Project.Instance.Namespaces).Replace("+", "."),
								  param.Name);

				if (i < function.Parameters.Count - 1)
				{
					paramList += ", ";
				}
			}
			return paramList;
		}

		private void SetSyntaxEditorBackColor()
		{
			Color backColor = Color.White;
			VisualStudio2005SyntaxEditorRenderer vs = (VisualStudio2005SyntaxEditorRenderer)syntaxEditor1.RendererResolved;
			SolidColorBackgroundFill fill = new SolidColorBackgroundFill(backColor);
			vs.TextAreaBackgroundFill = fill;
			syntaxEditor1.Document.Language.BackColor = backColor;
		}

		/// <summary>
		/// Determines and sets the language of the 'text' or 'ASP' text is. This
		/// allows us to use the correct syntax file to display syntax highlighting
		/// in CreateDirectiveXmlToCSharpLanguage.
		/// </summary>
		private void SetTextLanguage(FunctionInfo currentFunction)
		{
			if (currentFunction.IsTemplateFunction)
			{
				Project.Instance.TextLanguage =
					SyntaxEditorHelper.LanguageEnumFromName(currentFunction.TemplateReturnLanguage);
			}
			else
			{
				// TODO: create plain text syntax file and set it here
				Project.Instance.TextLanguage = TemplateContentLanguage.CSharp;
			}
		}

		/// <summary>
		/// Swap faded and syntax-highlighted text.
		/// </summary>
		public void SwitchFormatting()
		{
			UseSplitLanguage = !UseSplitLanguage;

			if (syntaxEditor1.Document.Language.LexicalStates.Count > 1)
			{
				syntaxEditor1.Document.Language.LexicalStates["ASPDirectiveState"].LexicalStateTransitionLexicalState.
					Language.BackColor = UseSplitLanguage
											 ? SyntaxEditorHelper.EDITOR_BACK_COLOR_FADED
											 : SyntaxEditorHelper.EDITOR_BACK_COLOR_NORMAL;
				syntaxEditor1.Document.Language.BackColor = UseSplitLanguage
																? SyntaxEditorHelper.EDITOR_BACK_COLOR_NORMAL
																: SyntaxEditorHelper.EDITOR_BACK_COLOR_FADED;
				syntaxEditor1.Refresh();
			}
		}

		private void syntaxEditor1_KeyDown(object sender, KeyEventArgs e)
		{
			if (!e.Control || e.KeyCode != Keys.Space) return;

			try
			{
				syntaxEditor1.SuspendPainting();
				int originalCaretPos = syntaxEditor1.SelectedView.Selection.StartOffset - 1;
				IntelliSense.AutoComplete(ref syntaxEditor1, _CurrentFunction);

				// Show the list
				if (syntaxEditor1.IntelliPrompt.MemberList.Count > 0)
				{
					if (syntaxEditor1.IntelliPrompt.MemberList.Count == 1)
					{
						syntaxEditor1.SelectedView.Selection.StartOffset = originalCaretPos;
						syntaxEditor1.SelectedView.Selection.EndOffset = originalCaretPos;
						syntaxEditor1.SelectedView.Selection.SelectWord();
						//syntaxEditor1.SelectedView.Delete();
						syntaxEditor1.SelectedView.ReplaceSelectedText(DocumentModificationType.Replace,
																	   syntaxEditor1.IntelliPrompt.MemberList[0].
																		Text);
					}
					else
					{
						syntaxEditor1.SelectedView.Selection.StartOffset = originalCaretPos;
						syntaxEditor1.SelectedView.Selection.EndOffset = originalCaretPos;
						syntaxEditor1.SelectedView.Selection.SelectWord();
						int tempOffset = syntaxEditor1.SelectedView.Selection.StartOffset;
						int tempWordLength = syntaxEditor1.SelectedView.Selection.Length;
						syntaxEditor1.SelectedView.Selection.StartOffset = originalCaretPos + 1;
						syntaxEditor1.SelectedView.Selection.EndOffset = originalCaretPos + 1;

						if (syntaxEditor1.Document.GetSubstring(originalCaretPos, 1) == ".")
						{
							tempOffset++;

							if (syntaxEditor1.Document.GetSubstring(originalCaretPos + 1, 1) == "\n")
							{
								tempWordLength--;
							}
						}
						syntaxEditor1.IntelliPrompt.MemberList.Show(tempOffset, tempWordLength);
					}
				}
			}
			finally
			{
				syntaxEditor1.ResumePainting();
			}
			return;
		}

		/// <summary>
		/// Save the function.
		/// </summary>
		public void Save()
		{
			if (CurrentFunction != null)
			{
				_CurrentFunction.Body = syntaxEditor1.Text;
			}
			else
			{
				// UserOption
				switch (CurrentUserOptionFunctionType)
				{
					case FunctionTypes.DefaultValue:
						CurrentUserOption.DefaultValueFunctionBody = syntaxEditor1.Text;
						break;
					case FunctionTypes.DisplayToUser:
						CurrentUserOption.DisplayToUserFunctionBody = syntaxEditor1.Text;
						break;
					case FunctionTypes.Validation:
						CurrentUserOption.ValidatorFunctionBody = syntaxEditor1.Text;
						break;
					//case Project.UserOption.FunctionTypes.HelperOverride:
					//	throw new NotImplementedException("HelperOverride not implemented yet.");
					//	break;
					default:
						throw new NotImplementedException("This type of UserOption hasn't been catered for yet: " + CurrentUserOptionFunctionType.ToString());
				}
			}
			//if (chkOverrideDefaultValueFunction.Visible)
			//{
			//    if (DefaultValueFunction == null)
			//    {
			//        DefaultValueFunction = Project.Instance.FindDefaultValueFunction(_CurrentFunction.Name, _CurrentFunction.Parameters);
			//    }
			//    DefaultValueFunction.UseCustomCode = chkOverrideDefaultValueFunction.Checked;
			//}
			IsDirty = false;

		}

		private void SetStatusMessage(string message)
		{
			functionToolStripStatusLabel.Text = message;
		}

		private void syntaxEditor1_IncrementalSearch(object sender, IncrementalSearchEventArgs e)
		{
			switch (e.EventType)
			{
				case IncrementalSearchEventType.Activated:
				case IncrementalSearchEventType.Search:
					if ((syntaxEditor1.SelectedView.FindReplace.IncrementalSearch.FindText.Length == 0) ||
						(e.ResultSet.Count > 0))
					{
						string text = "Incremental Search: " +
									  syntaxEditor1.SelectedView.FindReplace.IncrementalSearch.FindText;
						if (syntaxEditor1.SelectedView.FindReplace.IncrementalSearch.SearchUp)
							text = "Reverse " + text;
						SetStatusMessage(text);
					}
					else
					{
						string text = "Incremental Search: " +
									  syntaxEditor1.SelectedView.FindReplace.IncrementalSearch.FindText + " (not found)";
						if (syntaxEditor1.SelectedView.FindReplace.IncrementalSearch.SearchUp)
							text = "Reverse " + text;
						SetStatusMessage(text);
					}
					break;
				case IncrementalSearchEventType.Deactivated:
					SetStatusMessage("Ready");
					break;
				case IncrementalSearchEventType.CharacterIgnored:
					// This happens if a character is ignored because the current find text was not found in the last search
					break;
			}
		}

		private void syntaxEditor1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '(')
			{
				// Get the offset
				int offset = syntaxEditor1.SelectedView.Selection.EndOffset;

				// Get the text stream
				TextStream stream = syntaxEditor1.Document.GetTextStream(offset);
				//ActiproSoftware.SyntaxEditor.IToken prevToken = stream.PeekTokenReverse();//.TextRange.ToString();

				//if (syntaxEditor1.Document.GetSubstring(prevToken.TextRange) == "UserOptions")
				//{
				//    syntaxEditor1.Document.Tokens[prevToken.get
				//}
				// Get the language
				SyntaxLanguage language = stream.Token.Language;

				// If in C#...
				if (language.Key == "C#")
				{
					if ((offset >= 10) && (syntaxEditor1.Document.GetSubstring(offset - 10, 10) == "Invalidate"))
					{
						// Show an info tip
						syntaxEditor1.IntelliPrompt.ParameterInfo.Info.Clear();
						syntaxEditor1.IntelliPrompt.ParameterInfo.Info.Add(@"void <b>Control.Invalidate</b>()<br/>" +
																		   @"Invalidates the entire surface of the control and causes the control to be redrawn.");
						syntaxEditor1.IntelliPrompt.ParameterInfo.Info.Add(
							@"void Control.Invalidate(<b>System.Drawing.Rectangle rc</b>, bool invalidateChildren)<br/>" +
							@"<b>rc:</b> A System.Drawing.Rectangle object that represents the region to invalidate.");
						syntaxEditor1.IntelliPrompt.ParameterInfo.Show(offset - 10);
					}
					IntelliSense.GetIntelliSenseList(ref syntaxEditor1, _CurrentFunction);
				}
			}
		}

		private void syntaxEditor1_TriggerActivated(object sender, TriggerEventArgs e)
		{
			string language = "";
			TokenStream stream = syntaxEditor1.Document.GetTokenStream(syntaxEditor1.Document.Tokens.IndexOf(
																		   syntaxEditor1.SelectedView.Selection.
																			   EndOffset - 1));

			if (stream.Position > 0)
			{
				DynamicToken token = (DynamicToken)stream.ReadReverse();
				language = token.LexicalState.Language.Key;

				if ((string)token.Language.Tag == "TemplateLanguage" && _CurrentFunction.IsTemplateFunction)
				{
					// Don't provide IntelliSense for output text....it is just text!
					return;
				}
			}
			if (language == "C#")
			{
				switch (e.Trigger.Key)
				{
					case "MemberListTrigger":
						{
							//IntelliSense.RefreshFunctionParameters(this.CurrentFunction.Name);
							IntelliSense.GetIntelliSenseList(ref syntaxEditor1, _CurrentFunction);
							// Show the list
							if (syntaxEditor1.IntelliPrompt.MemberList.Count > 0)
							{
								syntaxEditor1.IntelliPrompt.MemberList.Show();
							}
							return;
						}
				}
			}
		}

		private void syntaxEditor1_ViewMouseHover(object sender, EditorViewMouseEventArgs e)
		{
			//SpanIndicator[] indicators = syntaxEditor1.Document.Indicators.GetSpanIndicatorsForOffset(e.OldMouseOverToken.StartOffset);

			if (e.HitTestResult.Token == null)
			{
				return;
			}
			SpanIndicator[] indicators =
				syntaxEditor1.Document.SpanIndicatorLayers.GetIndicatorsForTextRange(
					new TextRange(e.HitTestResult.Token.StartOffset, e.HitTestResult.Token.EndOffset), false);

			if (indicators != null)
			{
				foreach (SpanIndicator ind in indicators)
				{
					if (ind.Name == "ErrorIndicator")
					{
						e.ToolTipText = (string)ind.Tag;
					}
				}
			}
		}

		/*
				private void btnDeleteFunction_Click(object sender, EventArgs e)
				{
					if (
						MessageBox.Show(this, "Delete this function?", "Delete", MessageBoxButtons.YesNo,
										MessageBoxIcon.Question) == DialogResult.Yes)
					{
						Project.Instance.DeleteFunction(_CurrentFunction);
					}
				}
		*/

		private void btnEdit_Click(object sender, EventArgs e)
		{
			frmFunctionWizard form = new frmFunctionWizard(CurrentFunction, false);
			form.ShowDialog(ParentForm);

			if (frmFunctionWizard.IsDeleted)
			{
				if (OwnerIsTabStrip && Controller.Instance.MainForm.UcFunctions != null)
				{
					Controller.Instance.MainForm.UcFunctions.RemoveTab(OwnerTabStripPage);
				}
				return;
			}
			//FunctionName = frmFunctionWizard.CurrentFunction.Name;
			// Switch UseSplitLanguage because it gets re-switched inside SwitchFormatting, so we need to make sure we don't switch the formatting the user currently sees.
			UseSplitLanguage = !UseSplitLanguage;
			PopulateFunctionHeader();
		}

		private void syntaxEditor1_DocumentTextChanged(object sender, DocumentModificationEventArgs e)
		{
			if (!IsDirty && !BusyPopulating)
			{
				IsDirty = true;
				SetTabStripText(GetTabStripText() + " *");

				if (!Project.Instance.IsDirty)
				{
					Project.Instance.IsDirty = true;
				}
			}
		}

		private void ucFunction_Paint(object sender, PaintEventArgs e)
		{
			BackColor = Colors.BackgroundColor;
		}

		private void cbHighlightTemplateWrites_CheckedChanged(object sender, EventArgs e)
		{
			if (cbHighlightTemplateWrites.Checked == false)
			{
				syntaxEditorPreviewText.Document.SpanIndicatorLayers[NewPreviewTextLayerKey].Clear();
			}
		}

		private void chkOverrideDefaultValueFunction_CheckedChanged(object sender, EventArgs e)
		{
			SetupSyntaxDocumentsFromOverrideDefault();
		}

		private void SetupSyntaxDocumentsFromOverrideDefault()
		{
			syntaxEditor1.Document.ReadOnly = !chkOverrideDefaultValueFunction.Checked;

			SetSyntaxEditorBackColorForDefaultValueFunction(chkOverrideDefaultValueFunction.Checked);

			OverrideFunctionChanged.RaiseEvent(this);
		}

		private void SetSyntaxEditorBackColorForDefaultValueFunction(bool enabled)
		{
			Color backColor = enabled ? Color.White : Color.LightGray;
			VisualStudio2005SyntaxEditorRenderer vs =
				(VisualStudio2005SyntaxEditorRenderer)syntaxEditor1.RendererResolved;
			SolidColorBackgroundFill fill = new SolidColorBackgroundFill(backColor);
			vs.TextAreaBackgroundFill = fill;
			syntaxEditor1.Document.Language.BackColor = backColor;
		}

		private void btnResetDefaultCode_Click(object sender, EventArgs e)
		{
			if (CurrentUserOption != null)
			{
				switch (CurrentUserOptionFunctionType)
				{
					case FunctionTypes.DisplayToUser:
						syntaxEditor1.Text = UserOption.Default_DisplayToUserFunctionBody;
						return;
					case FunctionTypes.Validation:
						syntaxEditor1.Text = UserOption.Default_ValidatorFunctionBody;
						return;
					case FunctionTypes.DefaultValue:
						// TODO: fix for enum parameters
						//string firstEnumVal = enumArray.Length == 0 ? "" : enumArray[0];
						//string firstEnumVal = "";
						//string functionBody = Project.GetDefaultFunctionBody(Project.UserOptionEnumFromName(CurrentUserOption.VarType.Name), firstEnumVal);
						return;
				}
			}
			else
			{
				ResetDefaultCode.RaiseEvent(this);
			}
		}

		#region Preview Code

		/// <summary>
		/// Updates the text in the preview syntax editor window and the new preview text span.
		/// </summary>
		/// <param name="text">The updated preview text.</param>
		private void ChangeOutputText(string text)
		{
			SpanIndicatorLayer layer = syntaxEditorPreviewText.Document.SpanIndicatorLayers[NewPreviewTextLayerKey];

			if (string.IsNullOrEmpty(text))
			{
				syntaxEditorPreviewText.Text = "";
				_CurrentPreviewTextOffset = 0;
				return;
			}

			layer.Clear();

			if (text.Length > _PreviousPreviewTextLength)
			{
				syntaxEditorPreviewText.Document.Text = text;
				_PreviousPreviewTextLength = text.Length;

				int lastOffset =
					syntaxEditorPreviewText.Document.Lines[syntaxEditorPreviewText.Document.Lines.Count - 1].EndOffset;

				if (cbHighlightTemplateWrites.Checked)
				{
					TextRange range = new TextRange(_CurrentPreviewTextOffset, lastOffset);
					if (range.IsZeroLength == false)
					{
						syntaxEditorPreviewText.SuspendPainting();
						layer.Clear();
						layer.Add(new HighlightingStyleSpanIndicator("New Text", newTextHighlightStyle), range);
						syntaxEditorPreviewText.ResumePainting();
					}
				}
				_CurrentPreviewTextOffset = lastOffset;

				syntaxEditorPreviewText.SelectedView.ScrollToDocumentEnd();
			}
		}

		/*
				private void formatter_RaiseError(string fileName, string procedureName, string description, string originalText,
												  int lineNumber, int startPos, int length)
				{
					MessageBox.Show(this, string.Format("An error occurred on line {1}:\n\n{0}", description, lineNumber),
									"Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
		*/

		#endregion

		/*
		private void llParameters_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			frmSetParameters setParams = new frmSetParameters(_FunctionRunner.CurrentFunction);
			if (setParams.ShowDialog(this) == DialogResult.OK)
			{
				_FunctionRunner.ParametersToPass = setParams.ParametersToPass;
				_FunctionRunner.ValuesThatHaveBeenSet = setParams.ValuesThatHaveBeenSet;
			}
			Controller.Instance.MainForm.Activate();
		}
*/

		private void SetCachedParameters()
		{
			if (!FunctionRunner.CachedParameters.ContainsKey(CurrentFunction))
			{
				FunctionRunner.CachedParameters.Add(CurrentFunction, null);
			}
			FunctionRunner.CachedParameters[CurrentFunction] = new List<object>(ParametersToPass.Length);

			foreach (object paramToPass in ParametersToPass)
			{
				FunctionRunner.CachedParameters[CurrentFunction].Add(paramToPass);
			}
		}

		/// <summary>
		/// Sets up the debug environment, displays any errors to the user, then starts the debugger.
		/// </summary>
		/// <remarks>
		/// Compiles the project if it hasn't been compiled already.
		/// Checks the parameter data to make sure it is valid.
		/// Checks whether a project has been specified.
		/// 
		/// If any of these checks fail, it will throw an Exception.
		/// </remarks>
		public void StartDebugger()
		{
			if (!_CurrentFunction.IsTemplateFunction && _CurrentFunction.ReturnType != typeof(string))
			{
				MessageBox.Show(this, "This function does not output text, so cannot be viewed.", "Invalid Call",
								MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (Controller.Instance.CheckDebugOptions(false) == false)
			{
				Cursor = Cursors.Default;
				functionToolStripStatusLabel.Text = "Could not load ArchAngel project";
				return;
			}

			Cursor = Cursors.WaitCursor;
			functionToolStripStatusLabel.Text = "Compiling Project...";
			_FunctionRunner.CompileProject(true);

			if (!_FunctionRunner.ProjectHasBeenCompiled)
			{
				MessageBox.Show(this, "Project compilation was not successful, so the debugger cannot run.",
								"Error While Compiling", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Cursor = Cursors.Default;
				functionToolStripStatusLabel.Text = "Project Compilation Failed.";
				StopProgressBar();
				return;
			}

			functionToolStripStatusLabel.Text = "Project Compiled";
			StopProgressBar();

			if (FunctionRunner.CachedParameters.ContainsKey(CurrentFunction))
			{
				_FunctionRunner.ParametersToPass = FunctionRunner.CachedParameters[CurrentFunction].ToArray();
				_FunctionRunner.ValuesThatHaveBeenSet = new bool[_FunctionRunner.ParametersToPass.Length];

				for (int i = 0; i < _FunctionRunner.ValuesThatHaveBeenSet.Length; i++)
				{
					_FunctionRunner.ValuesThatHaveBeenSet[i] = true;
				}
			}
			_FunctionRunner.ValuesThatHaveBeenSet = ValuesThatHaveBeenSet;

			if (_FunctionRunner.CheckParameterData() == false)
			{
				MessageBox.Show(this, "Some parameter values haven't been set.", "Missing Data", MessageBoxButtons.OK,
								MessageBoxIcon.Warning);
				Cursor = Cursors.Default;
				functionToolStripStatusLabel.Text = "Parameter values have not been set";
				return;
			}

			Cursor = Cursors.WaitCursor;
			functionToolStripStatusLabel.Text = "Starting Debugger...";
			StartProgressBar();
			Cursor = Cursors.WaitCursor;

			//Controller.Instance.RunDebugger();

		}

		private void StopProgressBar()
		{
			functionToolStripProgressBar.MarqueeAnimationSpeed = 0;
			functionToolStripProgressBar.Visible = false;
		}

		private void StartProgressBar()
		{
			functionToolStripProgressBar.MarqueeAnimationSpeed = 20;
			functionToolStripProgressBar.Visible = true;
		}

		public override string ToString()
		{
			return "";
		}

		/// <summary>
		/// Fired when the user clicks the sidebar to add a breakpoint. Determines whether to add or 
		/// remove a breakpoint, does that, then updates the debugger breakpoints.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void syntaxEditor1_ViewMouseDown(object sender, EditorViewMouseEventArgs e)
		{
			if (e.Clicks == 1 && e.HitTestResult.Target == SyntaxEditorHitTestTarget.IndicatorMargin)
			{
				if (e.HitTestResult.DisplayLine.TextRange.AbsoluteLength == 0)
				{
					return;
				}

				SpanIndicatorLayer layer = syntaxEditor1.Document.SpanIndicatorLayers[BreakpointLayerKey];
				SpanIndicator[] indicators = layer.GetIndicatorsForTextRange(e.HitTestResult.DisplayLine.TextRange);
				if (indicators != null && indicators.Length != 0)
				{
					foreach (BreakpointSpanIndicator indicator in indicators)
					{
						layer.Remove(indicator);
					}
					_FunctionRunner.RemoveBreakpoint(CurrentFunction, e.HitTestResult.DisplayLineIndex, 0);
					syntaxEditor1.Invalidate();
				}
				else
				{
					DocumentLine line = e.HitTestResult.DisplayLine.DocumentLine;

					CreateBreakpoint(line.Index, 0);
				}
			}
		}

		public void ToggleBreakpointOnCurrentLine()
		{
			int line = syntaxEditor1.Caret.DocumentPosition.Line;
			TextRange lineRange = syntaxEditor1.Document.Lines[line].TextRange;
			SpanIndicatorLayer layer = syntaxEditor1.Document.SpanIndicatorLayers[BreakpointLayerKey];
			SpanIndicator[] indicators = layer.GetIndicatorsForTextRange(lineRange);

			if (indicators != null && indicators.Length != 0)
			{
				foreach (BreakpointSpanIndicator indicator in indicators)
				{
					layer.Remove(indicator);
				}
				_FunctionRunner.RemoveBreakpoint(CurrentFunction, line, 0);
				syntaxEditor1.Invalidate();
			}
			else
			{
				CreateBreakpoint(line, 0);
			}
		}

		/// <summary>
		/// Creates a breakpoint on the specified (0 based) template line number at the specified column.
		/// If the line specified is not valid, then the first valid line previous 
		/// to this will be used. If there is already a breakpoint on the line that
		/// is chosen, nothing happens.
		/// </summary>
		/// <param name="lineNumber"></param>
		/// <param name="column"></param>
		public void CreateBreakpoint(int lineNumber, int column)
		{
			DocumentLine line = syntaxEditor1.Document.Lines[lineNumber];
			TextStream stream = syntaxEditor1.Document.GetTextStream(line.StartOffset);
			SpanIndicatorLayer layer = syntaxEditor1.Document.SpanIndicatorLayers[BreakpointLayerKey];
			SpanIndicator[] indicators;

			if (SyntaxEditorHelper.IsEntireLineOneLanguage(line, stream, "TemplateLanguage"))
			{
				stream.Offset = line.StartOffset;
				stream.SeekToken(-1);
				line = stream.DocumentLine;
			}

			if (SyntaxEditorHelper.IsEntireLineOneLanguage(line, stream, "ScriptLanguage"))
			{
				indicators = layer.GetIndicatorsForTextRange(line.TextRange);
				if (indicators == null || indicators.Length == 0)
				{
					_FunctionRunner.SetBreakpoint(CurrentFunction, line.Index, 0);
					layer.Add(new BreakpointSpanIndicator(), line.TextRange);
				}
				return;
			}

			stream.Offset = line.StartOffset;
			int startOffset = SyntaxEditorHelper.GetFirstStartScriptTag(stream);
			if (startOffset == -1 || startOffset > line.EndOffset)
				startOffset = line.StartOffset;
			else
				startOffset += 2;

			stream.Offset = startOffset;
			int endOffset = SyntaxEditorHelper.GetFirstEndScriptTag(stream);
			if (endOffset == -1 || endOffset > line.EndOffset)
				endOffset = line.EndOffset;

			if (startOffset == endOffset)
				return;

			indicators = layer.GetIndicatorsForTextRange(new TextRange(startOffset, endOffset));

			if (indicators != null && indicators.Length != 0) return;

			_FunctionRunner.SetBreakpoint(CurrentFunction, line.Index, 0);
			layer.Add(new BreakpointSpanIndicator(), new TextRange(startOffset, endOffset));
		}

		public void DebuggerStarted()
		{
			Cursor = Cursors.Default;
			ClearPreviewText();
			StopProgressBar();
			functionToolStripStatusLabel.Text = "Debugger Started.";
		}

		private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (syntaxEditorPreviewText.Document.Length == 0)
				e.Cancel = true;
		}

		private void toolStripMenuItemBOO_Click(object sender, EventArgs e)
		{
			int offset = syntaxEditorPreviewText.Document.PositionToOffset(syntaxEditorPreviewText.Caret.DocumentPosition);

			//Controller.Instance.BreakOnOutput(CurrentFunction, offset);
		}

		public string GetFunctionBody()
		{
			return syntaxEditor1.Text;
		}
	}
}
