using System;
using System.Collections.Generic;
using System.Collections.Specialized;
//using ArchAngel.Debugger;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Designer.Properties;
using Slyce.Common;

namespace ArchAngel.Designer
{
	/// <summary>
	/// Summary description for Controller.
	/// </summary>
	[Serializable]
	public class Controller
	{
		public delegate void CompileErrorEventDelegate(int realLineNum, int Column, string errorText, int lineOffset, string functionName, List<ParamInfo> parameters, bool isWarning);
		public delegate void SettingChangedEventDelegate(string name, object oldValue, object newValue);
		[field: NonSerialized]
		public event CompileErrorEventDelegate CompileErrorEvent;
		[field: NonSerialized]
		public event SettingChangedEventDelegate SettingChangedEvent;
		private static Controller instance;
		private SyntaxReader m_theSyntaxReader;
		private static string m_tempPath = "";
		public string[] m_recentFiles = new string[0];
		[NonSerialized]
		private frmMain _MainForm;
		public static frmFind FindForm;
		public static FindReplaceOptions EditorFindReplaceOptions = new FindReplaceOptions();
		internal string ParsedCode = "";
		internal static bool CommandLine;
		//private static string _ilMergePath = "";
		private static string _helpFile;
		private static bool MainFormIsShaded;
		internal static bool BusySaving;
		internal static bool BusyCompiling;
		internal static Dictionary<Assembly, Interfaces.ProviderInfo> PopulatedProviders = new Dictionary<Assembly, Interfaces.ProviderInfo>();

		//[NonSerialized]
		//private Debugger.Debugger _Debugger;
		//[NonSerialized]
		//private DebuggerSyncConstruct _DebuggerSyncConstruct;
		[NonSerialized]
		private readonly BackgroundWorker _BackgroundWorker;
		[NonSerialized]
		private FunctionInfo CurrentFunction;
		private int _BreakOnOutputLineNumber = -1;
		private int _BreakOnOutputColumnNumber = -1;
		private int _BreakOnOutputOffset = -1;
		private int _BreakOnOutputBreakpointHitcount = -1;

		/// <summary>
		/// The root folder used for storing temporary files.
		/// </summary>
		public static readonly string ArchAngelTempFolder = Path.Combine(Path.GetTempPath(), "ArchAngel");

		[NonSerialized]
		private FunctionInfo _BreakOnOutputFunction;
		private int _BreakOnOutputTemplateLineNumber;
		private int _BreakOnOutputTemplateColumn;

		private Controller()
		{
			//Clear();
			_BackgroundWorker = new BackgroundWorker { WorkerReportsProgress = true };
			//_BackgroundWorker.DoWork += backgroundWorker_DoWork;
			//_BackgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
			//_BackgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged_GUI;

			//_DebuggerSyncConstruct = new DebuggerSyncConstruct(_BackgroundWorker);

			//if (!CommandLine)
			//    Debugger.Debugger.SpinUpDebugProcess();

			Project.Instance.ProjectModified += Instance_ProjectModified;
			Project.Instance.IsDirtyChanged += Instance_IsDirtyChanged;
			Project.Instance.DebugProjectFileChanged += Project_DebugProjectFileChanged;
			Settings.Default.PropertyChanged += Settings_PropertyChanged;
		}

		private void Project_DebugProjectFileChanged(object sender, EventArgs e)
		{
			PopulatedProviders.Clear();
			if (File.Exists(Project.Instance.DebugProjectFile))
				Instance.LoadDebugger();
		}

		void Instance_IsDirtyChanged(object sender, EventArgs e)
		{
			if (!CommandLine)
			{
				if (Project.Instance.IsDirty && MainForm.Text[MainForm.Text.Length - 1] != '*')
				{
					if (MainForm.InvokeRequired)
					{
						string text = MainForm.Text + " *";
						MainForm.CrossThreadHelper.SetCrossThreadProperty(MainForm, "Text", text);
					}
					else
					{
						MainForm.Text += " *";
					}
				}
				else if (Project.Instance.IsDirty == false && MainForm.Text[MainForm.Text.Length - 1] == '*')
				{
					MainForm.Text = MainForm.Text.Substring(0, MainForm.Text.Length - 2).Trim();
				}
			}
		}

		void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			// Add code to handle the SettingChangingEvent event here.
			if (e.PropertyName == "DebugCSPDatabasePath")
			{
				// Clear cached populated providers
				PopulatedProviders.Clear();
				LoadDebugger();
			}
		}

		internal void LoadDebugger()
		{
			if (string.IsNullOrEmpty(Project.Instance.DebugProjectFile))
			{
				return;
			}
			IEnumerable<ReferencedFile> referencedAssemblies = Project.Instance.References;
			var assemblyLocations = new List<string>();

			foreach (var file in referencedAssemblies)
			{
				assemblyLocations.Add(file.FileName);
			}
			//DebuggerInstance = new Debugger.Debugger(_DebuggerSyncConstruct,
			//        Project.Instance.DebugProjectFile,
			//        assemblyLocations);
		}

		public bool CheckDebugOptions(bool checkGeneratePath)
		{
			bool debugProjectExists = true;
			bool generatePathExists = true;

			if (string.IsNullOrEmpty(Project.Instance.DebugProjectFile) ||
				!File.Exists(Project.Instance.DebugProjectFile))
			{
				debugProjectExists = false;
			}

			if (string.IsNullOrEmpty(Project.Instance.TestGenerateDirectory) ||
							!Directory.Exists(Project.Instance.TestGenerateDirectory))
			{
				generatePathExists = false;
			}

			string message = "Please select the";
			bool errorOccurred = debugProjectExists == false || (checkGeneratePath && generatePathExists == false);

			if (debugProjectExists == false)
			{
				message += " ArchAngel project file (.wbproj) you want to read settings from";
			}
			if (checkGeneratePath && generatePathExists == false)
			{
				if (debugProjectExists == false) message += ", and the";

				message += " path you wish to generate to";
			}

			if (errorOccurred)
			{
				message += ".\n\nThe options menu will now be opened for you.";
				MessageBox.Show(MainForm,
								message,
								"Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				var formOpt = new frmOptions(frmOptions.Pages.DebugSettings);
				formOpt.ShowDialog(MainForm);

				if (string.IsNullOrEmpty(Project.Instance.DebugProjectFile) ||
					!File.Exists(Project.Instance.DebugProjectFile))
				{
					MainForm.Cursor = Cursors.Default;
					return false;
				}
				if (checkGeneratePath && (string.IsNullOrEmpty(Project.Instance.TestGenerateDirectory) ||
				!Directory.Exists(Project.Instance.TestGenerateDirectory)))
				{
					MainForm.Cursor = Cursors.Default;
					return false;
				}
			}
			return true;
		}

		///<summary>
		/// Calls Invoke with a timeout, which will keep waiting until either the 
		/// timer runs out and the Application is closing, or the method finishes.
		///</summary>
		///<param name="control">The control to Invoke on.</param>
		///<param name="theMethod">The delegate to invoke.</param>
		public static void SafeInvoke(Control control, Delegate theMethod)
		{
			IAsyncResult result = control.BeginInvoke(theMethod);
			while (result.IsCompleted == false && Instance.ApplicationClosing == false)
			{
				result.AsyncWaitHandle.WaitOne(10000, false);
			}
		}

		public bool ApplicationClosing { get; set; }

		private void _MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			ApplicationClosing = true;
		}

		public frmMain MainForm
		{
			get { return _MainForm; }
			set
			{
				if (_MainForm != null)
				{
					_MainForm.FormClosing -= _MainForm_FormClosing;
				}
				_MainForm = value;
				if (_MainForm != null)
				{
					_MainForm.FormClosing += _MainForm_FormClosing;
				}
			}
		}

		void Instance_ProjectModified(object sender, Project.ProjectOpenedEventArgs e)
		{
			//if (_Debugger == null) return;

			////_Debugger.StopDebugger();
			//Debugger.Debugger.RestartDebugProcess();
			//_Debugger = null;
		}

		/// <summary>
		/// Gets the singleton Controller instance.
		/// </summary>
		public static Controller Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Controller();
				}
				return instance;
			}
		}

		#region Debugger Methods
		///// <summary>
		///// The synchronisation construct for the GUI to use
		///// to communicate with the debugger.
		///// </summary>
		//public DebuggerSyncConstruct DebuggerSyncConstruct
		//{
		//    get { return _DebuggerSyncConstruct; }
		//    set { _DebuggerSyncConstruct = value; }
		//}

		///// <summary>
		///// The single template debugger instance that should be used in the
		///// rest of the application.
		///// </summary>
		//public Debugger.Debugger DebuggerInstance
		//{
		//    get
		//    {
		//        if (_Debugger == null)
		//        {
		//            LoadDebugger();
		//        }
		//        return _Debugger;
		//    }
		//    private set { _Debugger = value; }
		//}

		///// <summary>
		///// Runs the Debugger on the currently displayed function page.
		///// Switches the MainForm to debug mode.
		///// </summary>
		///// <exception cref="System.Exception">Throws an execption if there is no function currently
		///// displayed, or if the debugger is already running.</exception>
		//internal void RunDebugger()
		//{
		//    ucFunction panel = MainForm.UcFunctions.GetCurrentlyDisplayedFunctionPage();
		//    if (panel == null)
		//    {
		//        MainForm.MenuItemDebug.Enabled = false;
		//        throw new Exception("Cannot run the debugger when no function is currently displayed");
		//    }
		//    FunctionInfo currentFunction = panel.CurrentFunction;

		//    if (_BackgroundWorker.IsBusy)
		//        throw new Exception("The debugger is already running. Please finish that run before starting another.");

		//    if (DebuggerInstance == null)
		//        LoadDebugger();

		//    CurrentFunction = currentFunction;
		//    _BackgroundWorker.RunWorkerAsync();
		//    MainForm.SwitchToDebugRunningMode();
		//}

		///// <summary>
		///// Continue the debugger using the specified debug action type.
		///// </summary>
		///// <param name="debugActionType"></param>
		//internal void TriggerNextDebugAction(DebugActionType debugActionType)
		//{
		//    GetCurrentFunctionPage().ClearDebugInformation();
		//    _DebuggerSyncConstruct.NextDebugAction = debugActionType;
		//    _DebuggerSyncConstruct.ContinueDebugExecution.Set();
		//}

		///// <summary>
		///// Continues the debugger using the current debug action.
		///// </summary>
		//internal void TriggerNextDebugAction()
		//{
		//    GetCurrentFunctionPage().ClearDebugInformation();
		//    _DebuggerSyncConstruct.ContinueDebugExecution.Set();
		//}

		//internal void RaiseSettingChangedEvent(string name, object oldValue, object newValue)
		//{
		//    if (SettingChangedEvent != null)
		//    {
		//        SettingChangedEvent(name, oldValue, newValue);
		//    }
		//}

		///// <summary>
		///// Helper Method. Gets the curently displayed ucFunction form from MainForm.
		///// </summary>
		///// <returns></returns>
		//private ucFunction GetCurrentFunctionPage()
		//{
		//    return MainForm.UcFunctions.GetCurrentlyDisplayedFunctionPage();
		//}

		///// <summary>
		///// Runs the CallFunction method on the current ucFunction page.
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		//{
		//    e.Result = GetCurrentFunctionPage().CallFunction();
		//}

		///// <summary>
		///// Updates the GUI with information from the background worker running the Debugger.
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e">e.UserState should be a DebugInformation object. If it isn't, this method will
		///// do nothing.</param>
		//private void backgroundWorker_ProgressChanged_GUI(object sender, ProgressChangedEventArgs e)
		//{
		//    if (!(e.UserState is DebugInformation)) return;

		//    var di = (DebugInformation)e.UserState;

		//    if (di.StopReason == StopReason.DebuggerStarted)
		//    {
		//        GetCurrentFunctionPage().DebuggerStarted();
		//        MainForm.UcFunctions.AddOutput("Debugger", di.StopReasonText);
		//        TriggerNextDebugAction(DebugActionType.Continue);
		//    }
		//    else if (CompileHelper.TemplateLinesLookup.ContainsKey(di.StartLineNumber - 1))
		//    {
		//        if (di.StartLineNumber == _BreakOnOutputLineNumber)
		//        {
		//            // If this is a breakpoint set by Break On Output, remove it.
		//            _BreakOnOutputLineNumber = -1;
		//            ThreadStart ts = () => DebuggerInstance.RemoveBreakpoint(di.StartLineNumber);
		//            var thread = new Thread(ts);
		//            thread.SetApartmentState(ApartmentState.MTA);
		//            thread.Start();
		//        }
		//        FunctionInfo function = CompileHelper.TemplateLinesLookup[di.StartLineNumber - 1][0].Function;
		//        MainForm.UcFunctions.ShowFunction(function, null);
		//        ucFunction functionPanel = MainForm.UcFunctions.GetFunctionPanel(function);
		//        functionPanel.UpdateDebugState(di, false);
		//        MainForm.UcFunctions.AddOutput("Debugger", di.StopReasonText);
		//    }
		//    else switch (di.StopReason)
		//        {
		//            case StopReason.DebuggerFinished:
		//                {
		//                    MainForm.UcFunctions.ShowFunction(CurrentFunction, null);
		//                    ucFunction panel = MainForm.UcFunctions.GetFunctionPanel(CurrentFunction);
		//                    panel.UpdateDebugState(di, true);
		//                    TriggerNextDebugAction(DebugActionType.Continue);
		//                    MainForm.UcFunctions.AddOutput("Debugger", di.StopReasonText);
		//                }
		//                break;
		//            case StopReason.ExceptionOccurred:
		//                {
		//                    MainForm.UcFunctions.ShowFunction(CurrentFunction, null);
		//                    ucFunction panel = MainForm.UcFunctions.GetFunctionPanel(CurrentFunction);
		//                    panel.UpdateDebugState(di, false);

		//                    var sb = new StringBuilder();

		//                    sb.AppendLine("Primary Exception:");
		//                    sb.AppendLine(di.ExceptionInformation.Message);
		//                    sb.AppendLine(di.ExceptionInformation.StackTraceString);

		//                    MainForm.UcFunctions.AddOutput("Debugger", di.StopReasonText);
		//                    MainForm.UcFunctions.AddOutput("Debugger - Exception", di.ExceptionInformation.Message);
		//                    MainForm.UcFunctions.AddOutput("Debugger - Exception", di.ExceptionInformation.StackTraceString);
		//                    if (di.ExceptionInformation.InnerExceptionInfo != null)
		//                    {
		//                        MainForm.UcFunctions.AddOutput("Debugger - Exception", "Inner Exception Info:");
		//                        MainForm.UcFunctions.AddOutput("Debugger - Exception", di.ExceptionInformation.InnerExceptionInfo.Message);
		//                        MainForm.UcFunctions.AddOutput("Debugger - Exception", di.ExceptionInformation.InnerExceptionInfo.StackTraceString);

		//                        sb.AppendLine("Inner Exception:");
		//                        sb.AppendLine(di.ExceptionInformation.InnerExceptionInfo.Message);
		//                        sb.AppendLine(di.ExceptionInformation.InnerExceptionInfo.StackTraceString);
		//                    }

		//                    MainForm.UcFunctions.AddError(CurrentFunction.Name, sb.ToString());
		//                }
		//                break;
		//            default:
		//                TriggerNextDebugAction();
		//                break;
		//        }
		//}

		///// <summary>
		///// Reports any exceptions, and sets the GUI properties to indicate that the debugger has stopped.
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		//{
		//    try
		//    {
		//        if (e.Result is Exception)
		//        {
		//            throw (Exception)e.Result;
		//            //ReportError((Exception)e.Result);
		//        }
		//    }
		//    finally
		//    {
		//        MainForm.SwitchToDebugStoppedMode();
		//        _DebuggerSyncConstruct.ContinueDebugExecution.Reset();
		//        _DebuggerSyncConstruct.NextDebugAction = DebugActionType.Continue;
		//        GetCurrentFunctionPage().ClearDebugInformation();
		//    }
		//}

		///// <summary>
		///// Background worker progress method for use with the Break On Output functionality.
		///// Meant to be used as a replacement for the GUI progress method, and should not be
		///// used at the same time as any other progress method.
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void backgroundWorker_ProgressChanged_BreakOnThisFunction(object sender, ProgressChangedEventArgs e)
		//{
		//    if (!(e.UserState is DebugInformation)) return;

		//    var di = (DebugInformation)e.UserState;

		//    if (di.StopReason == StopReason.DebuggerStarted)
		//    {
		//        TriggerNextDebugAction(DebugActionType.Continue);
		//    }
		//    else if (CompileHelper.TemplateLinesLookup.ContainsKey(di.StartLineNumber - 1) &&
		//             CompileHelper.TemplateLinesLookup[di.StartLineNumber - 1][0].Function == CurrentFunction)
		//    {
		//        if (di.CurrentOutput.Length > _BreakOnOutputOffset)
		//        {
		//            _BreakOnOutputLineNumber = di.StartLineNumber - 1;
		//            _BreakOnOutputColumnNumber = di.StartColumnNumber;
		//            TriggerNextDebugAction(DebugActionType.Stop);
		//            return;
		//        }
		//        TriggerNextDebugAction(DebugActionType.StepOver);
		//    }
		//    else switch (di.StopReason)
		//        {
		//            case StopReason.DebuggerFinished:
		//                MessageBox.Show(MainForm, "Your output was not found during the Break On Output run. You may have changed your template code since the last run.");
		//                break;
		//            case StopReason.ExceptionOccurred:
		//                MainForm.UcFunctions.AddOutput("Debugger", di.StopReasonText);
		//                MessageBox.Show(MainForm, "An exception occurred during Break On Output execution.");
		//                break;
		//            default:
		//                TriggerNextDebugAction();
		//                break;
		//        }
		//}

		///// <summary>
		///// Background worker progress method for use with the Break On Output functionality.
		///// Meant to be used as a replacement for the GUI progress method, and should not be
		///// used at the same time as any other progress method.
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void backgroundWorker_ProgressChanged_BreakOnThisFunction_SecondRun(object sender, ProgressChangedEventArgs e)
		//{
		//    if (!(e.UserState is DebugInformation)) return;

		//    var di = (DebugInformation)e.UserState;

		//    if (di.StopReason == StopReason.DebuggerStarted)
		//    {
		//        TriggerNextDebugAction(DebugActionType.Continue);
		//    }
		//    else if (CompileHelper.TemplateLinesLookup.ContainsKey(di.StartLineNumber - 1)
		//            && CompileHelper.TemplateLinesLookup[di.StartLineNumber - 1][0].Function == CurrentFunction
		//            && _BreakOnOutputLineNumber == di.StartLineNumber - 1)
		//    {
		//        if (di.CurrentOutput.Length > _BreakOnOutputOffset)
		//        {
		//            TriggerNextDebugAction(DebugActionType.Stop);
		//            return;
		//        }
		//        _BreakOnOutputBreakpointHitcount++;

		//        TriggerNextDebugAction(DebugActionType.Continue);
		//    }
		//    else switch (di.StopReason)
		//        {
		//            case StopReason.DebuggerFinished:
		//                MessageBox.Show(MainForm, "Your output was not found during the Break On Output run. You may have changed your template code since the last run.");
		//                break;
		//            case StopReason.ExceptionOccurred:
		//                MainForm.UcFunctions.AddOutput("Debugger", di.StopReasonText);
		//                MessageBox.Show(MainForm, "An exception occurred during Break On Output execution.");
		//                break;
		//            default:
		//                TriggerNextDebugAction();
		//                break;
		//        }
		//}

		///// <summary>
		///// Background worker progress method for use with the Break On Output functionality.
		///// Meant to be used as a replacement for the GUI progress method, and should not be
		///// used at the same time as any other progress method.
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void backgroundWorker_ProgressChanged_BreakOnThisFunction_ThirdRun(object sender, ProgressChangedEventArgs e)
		//{
		//    if (!(e.UserState is DebugInformation)) return;

		//    var di = (DebugInformation)e.UserState;

		//    if (di.StopReason == StopReason.DebuggerStarted)
		//    {
		//        TriggerNextDebugAction(DebugActionType.Continue);
		//    }
		//    else if (CompileHelper.TemplateLinesLookup.ContainsKey(di.StartLineNumber - 1)
		//            && CompileHelper.TemplateLinesLookup[di.StartLineNumber - 1][0].Function == CurrentFunction
		//            && _BreakOnOutputLineNumber == di.StartLineNumber - 1)
		//    {
		//        _BreakOnOutputBreakpointHitcount--;

		//        if (_BreakOnOutputBreakpointHitcount > 0)
		//            TriggerNextDebugAction(DebugActionType.Continue);
		//        else
		//        {
		//            // Pass control back to the UI.

		//            ucFunction functionScreen = GetCurrentFunctionPage();
		//            functionScreen.ClearPreviewText();

		//            _DebuggerSyncConstruct.DebugBackgroundWorker.ProgressChanged -= backgroundWorker_ProgressChanged_BreakOnThisFunction_ThirdRun;
		//            _DebuggerSyncConstruct.DebugBackgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged_GUI;
		//            TriggerNextDebugAction(DebugActionType.Continue);
		//        }
		//    }
		//    else switch (di.StopReason)
		//        {
		//            case StopReason.DebuggerFinished:
		//                MessageBox.Show(MainForm, "Your output was not found during the Break On Output run. You may have changed your template code since the last run.");
		//                break;
		//            case StopReason.ExceptionOccurred:
		//                MainForm.UcFunctions.AddOutput("Debugger", di.StopReasonText);
		//                MessageBox.Show(MainForm, "An exception occurred during Break On Output execution.");
		//                break;
		//            default:
		//                TriggerNextDebugAction();
		//                break;
		//        }
		//}

		///// <summary>
		///// Background worker completed method for use with the first run through the Break On Output functionality.
		///// Sets up the next run.
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void BreakOnOutput_FirstRun_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		//{
		//    _DebuggerSyncConstruct.DebugBackgroundWorker.ProgressChanged -= backgroundWorker_ProgressChanged_BreakOnThisFunction;
		//    _DebuggerSyncConstruct.DebugBackgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged_BreakOnThisFunction_SecondRun;

		//    _DebuggerSyncConstruct.DebugBackgroundWorker.RunWorkerCompleted -= BreakOnOutput_FirstRun_RunWorkerCompleted;
		//    _DebuggerSyncConstruct.DebugBackgroundWorker.RunWorkerCompleted += BreakOnOutput_SecondRun_RunWorkerCompleted;

		//    if (_BreakOnOutputLineNumber == -1)
		//    {
		//        // The error has already been handled by this point.
		//        return;
		//    }

		//    ucFunction functionScreen = GetCurrentFunctionPage();
		//    functionScreen.ClearBreakpoints();

		//    int i = 0;
		//    List<CompiledToTemplateLineLookup> lookupList = CompileHelper.TemplateLinesLookup[_BreakOnOutputLineNumber];
		//    CompiledToTemplateLineLookup lookup = lookupList[i++];
		//    while (lookup.CompiledColumn < _BreakOnOutputColumnNumber && i < lookupList.Count)
		//        lookup = lookupList[i++];

		//    _BreakOnOutputTemplateColumn = lookup.CompiledColumn;
		//    _BreakOnOutputFunction = lookup.Function;
		//    _BreakOnOutputTemplateLineNumber = lookup.TemplateLineNumber;

		//    functionScreen.FunctionRunner.SetBreakpoint(_BreakOnOutputFunction, _BreakOnOutputTemplateLineNumber, _BreakOnOutputTemplateColumn);
		//    functionScreen.StartDebugger();
		//}


		///// <summary>
		///// Background worker completed method for use with the second run through the Break On Output functionality.
		///// Works out how many times the breakpoint we have calculated must be hit before the template function gets into
		///// the right state, then runs the debugger again to hit that point.
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void BreakOnOutput_SecondRun_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		//{
		//    _DebuggerSyncConstruct.DebugBackgroundWorker.ProgressChanged -= backgroundWorker_ProgressChanged_BreakOnThisFunction_SecondRun;
		//    _DebuggerSyncConstruct.DebugBackgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged_BreakOnThisFunction_ThirdRun;

		//    _DebuggerSyncConstruct.DebugBackgroundWorker.RunWorkerCompleted -= BreakOnOutput_SecondRun_RunWorkerCompleted;

		//    ucFunction functionScreen = MainForm.UcFunctions.GetFunctionPanel(_BreakOnOutputFunction);
		//    functionScreen.ClearBreakpoints();

		//    functionScreen.CreateBreakpoint(_BreakOnOutputTemplateLineNumber, _BreakOnOutputTemplateColumn);
		//    functionScreen.StartDebugger();
		//}

		///// <summary>
		///// Starts the Break On Output function. Given the name of a function and an offset
		///// into its output, it will run the debugger until the output is longer than that offset.
		///// It then adds a breakpoint at the start of that line, and re-runs the debugger, leaving it
		///// broken on that line.
		///// </summary>
		///// <remarks>This function will clear your breakpoints. Also, it is not working yet.</remarks>
		///// <param name="function">The function to work on.</param>
		///// <param name="offset">The 0 based offset of the text that should be broken before.</param>
		//public void BreakOnOutput(FunctionInfo function, int offset)
		//{
		//    _BreakOnOutputOffset = offset;
		//    MainForm.MenuItemDebug.Enabled = false;
		//    ucFunction functionScreen = MainForm.UcFunctions.GetFunctionPanel(function);
		//    FunctionRunner runner = functionScreen.FunctionRunner;
		//    CurrentFunction = function;

		//    if (runner.DebuggerRunning)
		//    {
		//        TriggerNextDebugAction(DebugActionType.Stop);

		//        Thread.Sleep(100);
		//        if (runner.DebuggerRunning)
		//            Debugger.Debugger.RestartDebugProcess();

		//        Thread.Sleep(100);
		//        if (runner.DebuggerRunning)
		//            throw new Exception("Could not stop the previous debug session in order to start reverse debugging. Please restart ArchAngel.");
		//    }
		//    _DebuggerSyncConstruct.DebugBackgroundWorker.RunWorkerCompleted += BreakOnOutput_FirstRun_RunWorkerCompleted;

		//    _DebuggerSyncConstruct.DebugBackgroundWorker.ProgressChanged -= backgroundWorker_ProgressChanged_GUI;
		//    _DebuggerSyncConstruct.DebugBackgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged_BreakOnThisFunction;
		//    functionScreen.ClearBreakpoints();
		//    runner.SetBreakpoint(function, 0, 0);

		//    functionScreen.StartDebugger();
		//}

		#endregion

		//        /// <summary>
		//        /// Helper Method. Shows the standard ArchAngel error dialog box.
		//        /// </summary>
		//        /// <param name="ex">The exception to show to the user.</param>
		//        public static void ReportError(Exception ex)
		//        {
		//#if DEBUG
		//            const string url = "http://localhost/ErrorReporter/Default.aspx";
		//#else
		//            const string url = "http://www.slyce.com/ErrorReporter/Default.aspx";
		//#endif
		//            Utility.SubmitError(url, Application.ProductName, Application.ProductVersion, ex.Message, ex);
		//        }

		internal static string FileManifest
		{
			get
			{
				return Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ArchAngel.FileManifest.xml");
			}
		}

		public static string HelpFile
		{
			get
			{
				if (string.IsNullOrEmpty(_helpFile))
				{
#if DEBUG
					_helpFile = Path.Combine(Slyce.Common.RelativePaths.GetFullPath(Path.GetDirectoryName(Application.ExecutablePath), "../../../Help"), "ArchAngel Help.chm");
#else
					_helpFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ArchAngel Help.chm");
#endif
				}
				return _helpFile;
			}
		}

		public static void ShowHelpTopic(Form form, string helpTopicPage)
		{
			if (File.Exists(HelpFile))
			{
				form.Cursor = Cursors.WaitCursor;
				Help.ShowHelp(null, HelpFile, helpTopicPage);
				form.Cursor = Cursors.Default;
			}
			else
			{
				MessageBox.Show(Instance.MainForm, "The ArchAngel help file is missing. Please repair the ArchAngel installation via Control Panel -> Add/Remove Programs.", "Helpfile Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		internal static void ShadeMainForm()
		{
			if (!MainFormIsShaded)
			{
				MainFormIsShaded = true;

				if (!Instance.MainForm.IsDisposed)
				{
					Utility.ShadeForm(Instance.MainForm, 140, Color.Black);
				}
			}
		}

		internal static void UnshadeMainForm()
		{
			if (!Instance.MainForm.IsDisposed)
			{
				//Slyce.Common.Utility.SuspendPainting(Instance.MainForm.Handle);
				Slyce.Common.Utility.UnShadeForm(Instance.MainForm);
				//Slyce.Common.Utility.ResumePainting();
			}
			MainFormIsShaded = false;
			Instance.MainForm.Focus();
		}

		public SyntaxReader TheSyntaxReader
		{
			get
			{
				if (m_theSyntaxReader == null)
				{
					// Accessing Language will fill this property
					//Project.ScriptLanguage x = this.Language;
				}
				return m_theSyntaxReader;
			}
			set
			{
				m_theSyntaxReader = value;
			}
		}

		public void ShowFindForm(bool showReplace)
		{
			bool formExists = true;

			if (FindForm == null)
			{
				formExists = false;
				FindForm = new frmFind(showReplace);
			}
			if (MainForm.UcFunctions.tabStrip1.SelectedTab != null)
			{
				//string selectedText = ((ActiproSoftware.SyntaxEditor.SyntaxEditor)((ucFunction)MainForm.UcFunctions.tabStrip1.SelectedPage.Controls[0]).Controls[1]).SelectedView.SelectedText;
				string selectedText = ((ucFunction)MainForm.UcFunctions.tabStrip1.SelectedTab.AttachedControl.Controls[0]).SyntaxEditor.SelectedView.SelectedText;

				if (selectedText.Length > 0 &&
					 selectedText.IndexOf("\n") < 0 &&
					 selectedText.IndexOf("\r") < 0)
				{
					SearchHelper.Options.FindText = selectedText;
				}
				FindForm.SetSearchText();
			}
			FindForm.TopMost = false;

			if (formExists)
			{
				FindForm.Show();
			}
			else
			{
				FindForm.Show(MainForm);
			}
		}

		public void ShowCompileErrors(CompilerError[] errors, bool debugVersion, string code)
		{
			try
			{
				bool realErrorsExist = false;
				StringBuilder sbCommandLine = new StringBuilder(1000);

				if (!CommandLine && errors.Length > 0)
				{
					MainForm.HidePanelControls(MainForm.UcFunctions);
				}
				Utility.CheckForNulls(new object[] { errors, debugVersion, code }, new[] { "errors", "debugVersion", "code" });
				string[] codeLines = new string[0];

				foreach (CompilerError err in errors)
				{
					if (!err.IsWarning)
					{
						realErrorsExist = true;

						if (codeLines.Length == 0)
						{
							codeLines = Utility.StandardizeLineBreaks(code, Slyce.Common.Utility.LineBreaks.Unix).Split('\n');
						}
					}
					if (CommandLine || CompileErrorEvent != null)// && err.Line < CompileHelper.FunctionLines.Count)
					{
						if (err.Line == 0)
						{
							if (CommandLine)
							{
								sbCommandLine.AppendLine(string.Format("[FUNCTION: ?, LINE: {0}, COL: {1}] {2}", err.Line, err.Column, err.Text));
							}
							else
							{
								CompileErrorEvent(err.Line, err.Column, err.Text, 0, "", null, err.IsWarning);
							}
							continue;
						}

						if (CompileHelper.TemplateLinesLookup.ContainsKey(err.Line - 1) == false &&
							(err.Line == 0 || codeLines.Length == 0 || (err.Line < codeLines.Length - 1 && codeLines[err.Line - 1].IndexOf("DynamicFilename: (") < 0)) &&
							(err.Line == 0 || codeLines.Length == 0 || (err.Line < codeLines.Length - 1 && codeLines[err.Line - 1].IndexOf("DynamicFolderName: (") < 0)))
						{
							// Skip error CS1687. This is a warning about having too many lines of code in our template,
							// and is triggered because our special line number is too large.
							if (err.IsWarning && err.Number != "CS1687")
							{
#if DEBUG
								throw new Exception("Warning in code. Use Debugger to examine this and remove it");
#else
								continue; // Don't show warnings in our code to user
#endif
							}
							if (err.Text.IndexOf("The process cannot access the file because it is being used by another process") >= 0)
							{
								const string message = "Cannot build. The compiled template is being used by another program, probably ArchAngel Workbench.";

								if (CommandLine)
								{
									sbCommandLine.AppendLine(message);
								}
								else
								{
									MessageBox.Show(MainForm, message, "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}
								continue;
							}
							else
							{
								//if (string.IsNullOrEmpty(code))
								//{
								//    Console.Error.WriteLine("code is  null");
								//    Environment.Exit(1);
								//}
								//else
								//{
								//    Console.Error.WriteLine("code length: "+ code.Length.ToString());
								//    Environment.Exit(1);
								//}
								//Console.Error.WriteLine("err is not null. Code = "+ code);
								//Environment.Exit(1);
								string message = string.Format("An error has occurred in the generated code. Please report this to Slyce as this is a serious error: {0}\n\n Line Number:{1}", err.Text, err.Line);

								//Slyce.Common.Utility.WriteToFile(@"C:\dump.cs", code);

								if (CommandLine)
								{
									sbCommandLine.AppendLine(message);

									string tempFile = Path.Combine(Path.GetTempPath(), "TemplateCode.cs");
									Slyce.Common.Utility.DeleteFileBrute(tempFile);
									Console.Error.WriteLine("Compile Errors: " + sbCommandLine + Environment.NewLine + "Code file: " + tempFile);
									File.WriteAllText(tempFile, ParsedCode, Encoding.Unicode);
									Environment.Exit(1);
								}
								else
								{
									MessageBox.Show(MainForm, message, "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
									//throw new Exception(message);
									return;
								}
							}
						}
						List<CompiledToTemplateLineLookup> lineData = null;

						if (CompileHelper.TemplateLinesLookup.ContainsKey(err.Line - 1) &&
							err.Line > 0 &&
							err.Line - 1 < CompileHelper.TemplateLinesLookup.Count)
						{
							lineData = CompileHelper.TemplateLinesLookup[err.Line - 1];
						}
						if (lineData != null)
						{
							for (int i = 0; i < lineData.Count; i++)
							{
								CompiledToTemplateLineLookup funcCompiledToTemplateLines = lineData[i];
								string functionName = funcCompiledToTemplateLines.Function.Name;
								int realLineNum = funcCompiledToTemplateLines.TemplateLineNumber;
								int realColumnNum = funcCompiledToTemplateLines.TemplateColumn;
								int compiledColNum = funcCompiledToTemplateLines.CompiledColumn;

								if (i < lineData.Count - 1)
								{
									CompiledToTemplateLineLookup funcCompiledToTemplateLinesNextSnippet = lineData[i + 1];
									int nextCompiledColNum = funcCompiledToTemplateLinesNextSnippet.CompiledColumn;

									if (err.Column > nextCompiledColNum)
									{
										// We are meant to be reading a later snippet
										continue;
									}
								}
								if (realColumnNum > 0)
								{
									int compiledOffset = compiledColNum - realColumnNum;
									realColumnNum = err.Column - compiledOffset;
								}
								else
								{
									realColumnNum = err.Column;
								}
								if (CommandLine)
								{
									sbCommandLine.AppendLine(string.Format("[FUNCTION: {0}, LINE: {1}, COL: {2}] {3}", functionName, realLineNum, realColumnNum, err.Text));
								}
								else
								{
									CompileErrorEvent(realLineNum, realColumnNum, err.Text, 0, functionName, funcCompiledToTemplateLines.Function.Parameters, err.IsWarning);
								}
								break;
							}
						}
						else if (err.File == "<Project Details>")
						{
							if (CommandLine)
							{
								sbCommandLine.AppendLine(string.Format("[FUNCTION: <Project Details>, LINE: {0}, COL: {1}] {2}", err.Line, err.Column, err.Text));
							}
							else
							{
								CompileErrorEvent(err.Line, err.Column, err.Text, 0, "<Project Details>", null, err.IsWarning);
							}
						}
						else if (codeLines.Length > 0 &&
							err.Line > 0 &&
							err.Line - 1 < codeLines.Length &&
							codeLines[err.Line - 1].IndexOf("DynamicFilename: (") >= 0)
						{
							int index = codeLines[err.Line - 1].IndexOf("DynamicFilename: (") + "DynamicFilename: (".Length;
							string lookup = codeLines[err.Line - 1].Substring(index);
							string fileId = lookup.Substring(0, lookup.IndexOf(","));
							string dynamicOrdinal = lookup.Substring(lookup.IndexOf(",") + 1).Trim(new[] { ')' });
							OutputFile file = Project.Instance.FindFile(fileId);
							string text = string.Format("Snippet [{1}] of Dynamic filename: {0}", file.Name, dynamicOrdinal);
							CompileErrorEvent(err.Line, err.Column, err.Text, 0, text, null, err.IsWarning);
						}
						else if (codeLines.Length > 0 &&
							err.Line > 0 &&
							err.Line - 1 < codeLines.Length &&
							codeLines[err.Line - 1].IndexOf("DynamicFolderName: (") >= 0)
						{
							int index = codeLines[err.Line - 1].IndexOf("DynamicFolderName: (") + "DynamicFolderName: (".Length;
							string lookup = codeLines[err.Line - 1].Substring(index);
							string folderId = lookup.Substring(0, lookup.IndexOf(","));
							string dynamicOrdinal = lookup.Substring(lookup.IndexOf(",") + 1).Trim(new[] { ')' });
							OutputFolder folder = Project.Instance.FindFolder(folderId);
							string text = string.Format("Snippet [{1}] of Dynamic foldername: {0}", folder.Name, dynamicOrdinal);
							CompileErrorEvent(err.Line, err.Column, err.Text, 0, text, null, err.IsWarning);
						}
						else
						{
							if (CommandLine)
							{
								sbCommandLine.AppendLine(string.Format("[FUNCTION: ?, LINE: {0}, COL: {1}] {2}", err.Line, err.Column, err.Text));
							}
							else
							{
								CompileErrorEvent(err.Line, err.Column, err.Text, 0, "", null, err.IsWarning);
							}
						}
					}
				}
				if (CommandLine)
				{
					if (realErrorsExist)
					{
						string tempFile = Path.Combine(Path.GetTempPath(), "TemplateCode.cs");
						Slyce.Common.Utility.DeleteFileBrute(tempFile);
						Console.Error.WriteLine("Compile Errors: " + sbCommandLine + Environment.NewLine + "Code file: " + tempFile);
						File.WriteAllText(tempFile, ParsedCode, Encoding.Unicode);
						Environment.Exit(1);
					}
					Environment.Exit(0);
				}
				MainForm.UcFunctions.DisplayFirstErrorFunction();
				return;
			}
			catch (Exception ex)
			{
				if (CommandLine)
				{
					Console.Error.WriteLine("Exception in ShowCompileErrors: " + ex.Message);
					Environment.Exit(1);
				}
				else
				{
					throw;
				}
			}
		}

		public static string TempPath
		{
			get
			{
				if (string.IsNullOrEmpty(m_tempPath))
				{
					m_tempPath = Path.Combine(Path.GetTempPath(), "ArchAngel.Designer.Temp" + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
				}
				if (!Directory.Exists(m_tempPath))
				{
					Directory.CreateDirectory(m_tempPath);
				}
				return m_tempPath;
			}
		}

		public void AddRecentFile(string file)
		{
			string[] newRecentFiles = new string[RecentFiles.Length + 1];
			Array.Copy(RecentFiles, 0, newRecentFiles, 1, RecentFiles.Length);
			newRecentFiles[0] = file;
			RecentFiles = newRecentFiles;
			MainForm.PopulateRecentFiles();
		}

		public string[] RecentFiles
		{
			get
			{
				if (m_recentFiles == null || m_recentFiles.Length == 0)
				{
					StringCollection recentFileList = Settings.Default.RecentFiles;

					if (recentFileList == null)
					{
						recentFileList = new StringCollection();
					}
					if (recentFileList.Count != 0)
					{
						m_recentFiles = new string[recentFileList.Count];
						recentFileList.CopyTo(m_recentFiles, 0);
					}
					else if (m_recentFiles == null)
					{
						m_recentFiles = new string[0];
					}

					// Remove any null or empty strings.
					List<string> files = new List<string>();
					foreach (string file in m_recentFiles)
					{
						if (string.IsNullOrEmpty(file) == false)
							files.Add(file);
					}
					m_recentFiles = files.ToArray();
				}
				return m_recentFiles;
			}
			set
			{
				Utility.CheckForNulls(new object[] { value }, new[] { "value" });
				m_recentFiles = value;
				int numFilesAdded = 0;
				const int maxNumFiles = 10;

				StringCollection stringCollection = new StringCollection();

				for (int i = 0; i < m_recentFiles.Length; i++)
				{
					// Don't allow more than max recent files
					if (numFilesAdded > maxNumFiles) { break; }
					// Make sure a file only appears in the list once
					bool alreadyinList = false;

					for (int pre = 0; pre < i; pre++)
					{
						if (Utility.StringsAreEqual(m_recentFiles[pre], m_recentFiles[i], false))
						{
							alreadyinList = true;
							break;
						}
					}
					if (!alreadyinList)
					{
						if (i < m_recentFiles.Length - 1)
						{
							stringCollection.Add(m_recentFiles[i]);
						}
						else
						{
							stringCollection.Add(m_recentFiles[i]);
						}
						numFilesAdded++;
					}
				}
				m_recentFiles = new string[stringCollection.Count];

				for (int i = 0; i < stringCollection.Count; i++)
				{
					m_recentFiles[i] = stringCollection[i];
				}
				Settings.Default.RecentFiles = stringCollection;
			}
		}

		/*
		internal static string SettingsRead(string regKey)
		{
			if (settings[regKey] != null)
			{
				return (string)settings[regKey];
			}
			string returnVal;
			XmlNode node = SettingsXmlDoc.SelectSingleNode("/ROOT/" + regKey);

			if (node != null)
			{
				returnVal = node.InnerText;
			}
			else
			{
				returnVal = "";
			}
			settings[regKey] = returnVal;
			return returnVal;
		}
		*/

		/*
		internal static void SaveSettings()
		{
			string configPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Designer.settings");

			if (File.Exists(configPath))
			{
				File.Delete(configPath);
			}
			bool readSuccessfully = false;
			int numReadAttempts = 0;

			while (!readSuccessfully && numReadAttempts < 5)
			{
				try
				{
					using (XmlTextWriter writer = new XmlTextWriter(configPath, System.Text.Encoding.UTF8))
					{
						writer.Formatting = Formatting.Indented;
						SettingsXmlDoc.Save(writer);
						writer.Flush();
						writer.Close();
						readSuccessfully = true;
					}
				}
				catch
				{
					Thread.Sleep(50);
				}
				numReadAttempts++;
			}
		}

		internal static void SettingsWrite(string regKey, string val)
		{
			if (settings[regKey] != null && settings[regKey].ToString() == val)
			{
				// Value hasn't changed, so don't bother updating the file
				return;
			}
			settings[regKey] = val;
			XmlNode node = SettingsXmlDoc.SelectSingleNode("/ROOT/" + regKey);

			if (node == null)
			{
				XmlNode rootNode = SettingsXmlDoc.SelectSingleNode("/ROOT");
				XmlElement xmlelem2 = SettingsXmlDoc.CreateElement("", regKey, "");
				XmlText xmltext = SettingsXmlDoc.CreateTextNode(val);
				xmlelem2.AppendChild(xmltext);
				rootNode.AppendChild(xmlelem2);
			}
			else
			{
				node.InnerText = val;
			}
		}
		 */

		/// <summary>
		/// The temp path will be of the form Temp/ArchAngel/Guid/ComponentKey
		/// where Temp is the system temp folder, the Guid is generated from the
		/// project filename, and the ComponentKey is the string representation of 
		/// the part of the ArchAngel system that needs a temp folder.
		/// </summary>
		/// <param name="componentKey">The part of the ArchAngel system that needs the
		/// temp path.</param>
		/// <returns>Path of the form Temp/ArchAngel/Guid/ComponentKey. For a given project,
		/// the temp path will be the same as long as the project filename does not change.</returns>
		public string GetTempFilePathForComponent(ComponentKey componentKey)
		{
			return PathHelper.GetTempFilePathFor("ArchAngel", Project.Instance.DebugProjectFile ?? "", componentKey);
		}
	}
}
