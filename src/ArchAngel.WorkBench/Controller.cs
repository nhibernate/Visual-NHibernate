using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using ArchAngel.Common;
using ArchAngel.Workbench.Properties;
using log4net;
using Slyce.Common;

namespace ArchAngel.Workbench
{
	/// <summary>
	/// Summary description for Controller.
	/// </summary>
	public class Controller : WorkbenchProjectController, IController
	{
		#region Delegates
		public delegate void GenerationStartedDelegate();
		public delegate void UserFilesChangedDelegate(FileSystemEventArgs e);
		public delegate void CompileErrorsDelegate(List<System.CodeDom.Compiler.CompilerError> errors);
		#endregion

		#region Events
		/// <summary>
		/// Raised when there is a change in the User's project directory.
		/// </summary>
		public event UserFilesChangedDelegate UserFilesChanged;
		public event GenerationStartedDelegate GenerationStarted;
		public event CompileErrorsDelegate OnCompileErrors;
		/// <summary>
		/// Gets raised when any data changes one of the providers.
		/// </summary>
		public event Interfaces.Events.DataChangedEventDelegate OnDataChanged;
		#endregion

		private static IController _Instance = new Controller();
		private bool _IsDirty;
		//private readonly Dictionary<SettingNames, object> _Settings = new Dictionary<SettingNames, object>();
		private bool _MainFormIsShaded;
		/*
				private XmlDocument _SettingsXmlDoc;
		*/
		private readonly ProjectFileTree projectFileTreeModel;
		//private FileSystemWatcher _UserFilesFileSystemWatcher;
		private DateTime lastFileEventTime = DateTime.MinValue;
		private string lastFileEventName = "";
		private readonly Dictionary<string, string> MD5Hashes = new Dictionary<string, string>();

		//private string EditedText;
		//private SlyceMergeWorker _SlyceMergeWorker = new SlyceMergeWorker();
		private bool _MustDisplaySplash = true;
		private FormMain _MainForm;
		private bool _CommandLine;
		private string[] _RecentFiles = new string[0];
		private bool creatingNewProject;
		private bool applicationClosing;

		// Log4Net Logger for writing out trace and debug statements
		private static readonly ILog log = LogManager.GetLogger(typeof(Controller));

		private Controller()
		{
			CurrentProject = new WorkbenchProject();
			Interfaces.Events.DataChangedEvent += On_DataChanged;
			OnProjectLoaded += Project_OnProjectLoaded;
			projectFileTreeModel = new ProjectFileTree();
			//InitializeSettings();
		}

		private void Project_OnProjectLoaded()
		{
		}

		//private void InitializeSettings()
		//{
		//    InitializeSetting(SettingNames.PerformMergeAnalysis, true, typeof(bool));
		//    InitializeSetting(SettingNames.RecentFiles, "", typeof(string));
		//    InitializeSetting(SettingNames.DisplayTaskHelp, true, typeof(bool));
		//    InitializeSetting(SettingNames.WindowSize, "0,0", typeof(string));
		//    InitializeSetting(SettingNames.DebugLoggingEnabled, false, typeof(bool));
		//}

		public bool ApplicationClosing
		{
			get { return applicationClosing; }
			set
			{
				applicationClosing = value;
				if (!value) return;
			}
		}

		protected override void BeforeOpenProjectFile()
		{
			Utility.DisplayMessagePanel(MainForm, "Opening project...", Slyce.Common.Controls.MessagePanel.ImageType.Hourglass);
			MainForm.Cursor = Cursors.WaitCursor;
			if (FormMain.ContentItemOutput != null) FormMain.ContentItemOutput.CancelCurrentTask();
		}

		protected override void AfterOpenProjectFile(bool successful, string file)
		{
			if (successful)
			{
				MainForm.OnOpenProjectFile();
				Interfaces.SharedData.RegistryUpdateValue("DefaultConfig", CurrentProject.ProjectFile);
				MainForm.AddRecentFile(file);
			}

			Utility.HideMessagePanel(MainForm);
			MainForm.Cursor = Cursors.Default;
		}

		private FileController fileController = new FileController();
		public bool IsValidOutputPath(string folder)
		{
			return fileController.DirectoryExists(folder);
		}

		public void LoadOptions()
		{
			if (CurrentProject == null || string.IsNullOrEmpty(CurrentProject.AppConfigFilename))
				return;

			string path = Path.GetDirectoryName(CurrentProject.AppConfigFilename).PathSlash("options.xml");
			LoadOptions(path);
		}

		public void LoadOptions(string fileName)
		{
			if (!File.Exists(fileName))
			{
				return;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(fileName);
			FormMain.ContentItemOptions.LoadOptions(xmlDocument);
		}

		///<summary>
		///</summary>
		public void LoadFileTreeStatus(ProjectFileTree model)
		{
			string xmlFilename = Path.Combine(GetTempFilePathForComponent(ComponentKey.Workbench_Scratch), "filetree.xml");

			if (!File.Exists(xmlFilename))
			{
				string dir = Path.GetDirectoryName(xmlFilename);

				if (!Directory.Exists(dir))
					Directory.CreateDirectory(dir);

				if (CurrentProject != null && !string.IsNullOrEmpty(CurrentProject.AppConfigFilename))
				{
					string path = Path.GetDirectoryName(CurrentProject.AppConfigFilename).PathSlash("filetree.xml");

					if (File.Exists(path))
						File.Copy(path, xmlFilename);
				}
			}
			if (File.Exists(xmlFilename))
			{
				XmlDocument document = new XmlDocument();
				document.Load(xmlFilename);
				try
				{
					model.LoadCheckedStatusFromXml(document);
				}
				catch (ArgumentException)
				{
					// Ignore the file - it is malformed. It will be overwritten when the user saves anyway.
				}
			}
		}

		public bool CreatingNewProject
		{
			get { return creatingNewProject; }
			set
			{
				creatingNewProject = value;
				if (creatingNewProject == false)
					RaiseOnProjectModificationEvent();
			}
		}

		public string[] RecentFiles
		{
			get
			{
				if (_RecentFiles == null || _RecentFiles.Length == 0)
				{
					string recentFileList = Settings.Default.RecentFiles;//SettingsRead(SettingNames.RecentFiles);

					if (recentFileList.Length != 0 && recentFileList != "|")
					{
						_RecentFiles = recentFileList.Split('|');
					}
					else
					{
						_RecentFiles = new string[0];
					}
				}
				return _RecentFiles;
			}
			set
			{
				Utility.CheckForNulls(new object[] { value }, new[] { "value" });
				_RecentFiles = value;
				string val = "";
				int numFilesAdded = 0;
				const int maxNumFiles = 10;

				for (int i = 0; i < _RecentFiles.Length; i++)
				{
					// Don't allow more than max recent files
					if (numFilesAdded > maxNumFiles) { break; }
					// Make sure a file only appears in the list once
					bool alreadyinList = false;

					for (int pre = 0; pre < i; pre++)
					{
						if (Utility.StringsAreEqual(_RecentFiles[pre], _RecentFiles[i], false))
						{
							alreadyinList = true;
							break;
						}
					}
					if (!alreadyinList)
					{
						if (i < _RecentFiles.Length - 1)
						{
							val += _RecentFiles[i] + "|";
						}
						else
						{
							val += _RecentFiles[i];
						}
						numFilesAdded++;
					}
				}
				_RecentFiles = val.Split('|');
				Settings.Default.RecentFiles = val;
				//				SettingSet(SettingNames.RecentFiles, val);
			}
		}

		public Size WindowSize
		{
			get
			{
				Size size;

				try
				{
					string winSize = Settings.Default.WindowSize; //SettingsRead(SettingNames.WindowSize);

					if (string.IsNullOrEmpty(winSize))
					{
						size = new Size(0, 0);
						return size;
					}
					string[] parts = winSize.Split(',');
					size = new Size(int.Parse(parts[0]), int.Parse(parts[1]));
				}
				catch
				{
					size = new Size(0, 0);
				}
				return size;
			}
			set
			{
				string size = string.Format("{0},{1}", _MainForm.Size.Width, _MainForm.Size.Height);
				Settings.Default.WindowSize = size;
			}
		}

		public static string GetHelpFile(string helpFileName)
		{
#if DEBUG
			return Path.Combine(RelativePaths.GetFullPath(Path.GetDirectoryName(Application.ExecutablePath), "../../../Help"), helpFileName);
#else
			return Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), helpFileName);
#endif
		}

		public static void ShowHelpTopic(Form form, string helpFile, string helpTopicPage)
		{
			if (File.Exists(helpFile))
			{
				form.Cursor = Cursors.WaitCursor;
				Help.ShowHelp(null, helpFile, helpTopicPage);
				form.Cursor = Cursors.Default;
			}
			else
			{
				MessageBox.Show("The ArchAngel help file is missing. Please repair the ArchAngel installation via Control Panel -> Add/Remove Programs.", "Helpfile Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void ShadeMainForm()
		{
			if (!_MainFormIsShaded)
			{
				_MainFormIsShaded = true;
				Utility.ShadeForm(_MainForm, 140, Color.Black);
			}
		}

		public void ShadeControl(Form control)
		{
			Utility.ShadeForm(control, 140, Color.Black);
		}

		public void UnshadeMainForm()
		{
			if (_MainForm.InvokeRequired)
			{
				_MainForm.Invoke(new MethodInvoker(UnshadeMainForm));
				return;
			}

			if (!_MainForm.IsDisposed)
			{
				Utility.SuspendPainting(_MainForm);
				Utility.UnShadeForm(_MainForm);
				Utility.ResumePainting(_MainForm);
			}
			_MainFormIsShaded = false;
			_MainForm.Focus();
		}

		public void UnshadeControl(Control control)
		{
			if (control.InvokeRequired)
			{
				control.Invoke(new MethodInvoker(() => UnshadeControl(control)));
				return;
			}

			if (!control.IsDisposed)
			{
				Utility.SuspendPainting(control);
				Utility.UnShadeForm(control);
				Utility.ResumePainting(control);
			}
		}

		public void RaiseGenerationStartedEvent()
		{
			if (GenerationStarted != null)
			{
				GenerationStarted();
			}
		}

		public static string FileManifest
		{
			get
			{
				return Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ArchAngel.FileManifest.xml");
			}
		}

		public void ReportFileLoadError(string message)
		{
			if (MainForm.InvokeRequired)
			{
				SafeInvoke(MainForm, new MethodInvoker(() => ReportFileLoadError(message)), true);
				return;
			}

			MessageBox.Show(MainForm, "Your project could not be loaded for the following reason: "
									  + Environment.NewLine + message, "Error loading file",
									  MessageBoxButtons.OK, MessageBoxIcon.Error);
			Interfaces.Events.UnShadeMainForm();
		}

		//public static void ReportError(Exception ex)
		//{
		//    string version = Application.ProductVersion;// Slyce.Common.Controls.VersionInfo.GetCurrentVersion(FileManifest);

		//    if (string.IsNullOrEmpty(version))
		//    {
		//        version = "debug";
		//    }
		//    const string url = "http://www.slyce.com/errors/ReportError.aspx";
		//    Utility.SubmitError(url, Application.ProductName, version, ex.Message, ex);
		//    Interfaces.Events.UnShadeMainForm();
		//}

		/// <summary>
		/// Gets called when any data changes in the Providers.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="method"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		private void On_DataChanged(Type type, MethodInfo method, object oldValue, object newValue)
		{
			IsDirty = true;

			if (OnDataChanged != null)
			{
				OnDataChanged(type, method, oldValue, newValue);
			}
		}

		public void RaiseCompileErrors(List<System.CodeDom.Compiler.CompilerError> errors)
		{
			if (OnCompileErrors != null)
				OnCompileErrors(errors);
		}

		public override bool IsDirty
		{
			get { return _IsDirty; }
			set
			{
				if (!BusyPopulating)
				{
					if (!CommandLine && _MainForm != null && string.IsNullOrEmpty(_MainForm.Text) == false)
					{
						string lastChar = string.IsNullOrEmpty(_MainForm.Text) ? "" : _MainForm.Text[_MainForm.Text.Length - 1].ToString();

						if (value && lastChar != "*")
						{
							if (!_MainForm.InvokeRequired)
							{
								_MainForm.Text += " *";
							}
							else
							{
								_MainForm.CrossThreadHelper.SetCrossThreadProperty(_MainForm, "Text", _MainForm.Text + " *");
							}
						}
						else if (!value && lastChar == "*")
						{
							if (!_MainForm.InvokeRequired)
							{
								_MainForm.Text = _MainForm.Text.Substring(0, _MainForm.Text.Length - 2).Trim();
							}
							else
							{
								_MainForm.CrossThreadHelper.SetCrossThreadProperty(_MainForm, "Text", _MainForm.Text.Substring(0, _MainForm.Text.Length - 2).Trim());
							}
						}
					}
					_IsDirty = value;

					RaiseOnProjectModificationEvent();
				}
			}
		}

		protected override void OnProjectModificationInternal()
		{
		}

		public static IController Instance
		{
			get { return _Instance; }
			set { _Instance = value; }
		}

		public bool MustDisplaySplash
		{
			get { return _MustDisplaySplash; }
			set { _MustDisplaySplash = value; }
		}

		public FormMain MainForm
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
					VerificationIssueSolver = _MainForm;
				}
				else
				{
					VerificationIssueSolver = new NullVerificationIssueSolver();
				}
			}
		}

		private void _MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			CrossThreadHelper.ApplicationClosing = true;
			ApplicationClosing = true;
		}

		public bool CommandLine
		{
			get { return _CommandLine; }
			set { _CommandLine = value; }
		}

		public ProjectFileTree ProjectFileTreeModel
		{
			get { return projectFileTreeModel; }
		}

		protected override void ExtraSaveSteps(string tempFolder)
		{
			string optionsFile = Path.Combine(tempFolder, "options.xml");
			string filetreeFile = Path.Combine(Path.Combine(Path.GetDirectoryName(CurrentProject.ProjectFile), ArchAngel.Interfaces.ProviderHelper.GetProjectFilesDirectoryName(CurrentProject.ProjectFile)), "filetree.xml");
			FormMain.ContentItemOptions.SaveOptions(optionsFile);
			FormMain.ContentItemOutput.SaveProjectFileTree(filetreeFile);
		}

		protected override void OnSaveSuccess()
		{
			MainForm.AddRecentFile(CurrentProject.ProjectFile);
		}

		///<summary>
		/// Calls Invoke with a timeout, which will keep waiting until either the 
		/// timer runs out and the Application is closing, or the method finishes.
		///</summary>
		///<param name="control">The control to Invoke on.</param>
		///<param name="theMethod">The delegate to invoke.</param>
		///<param name="wait">Whether to wait for the invocation to finish.</param>
		public static void SafeInvoke(Control control, Delegate theMethod, bool wait)
		{
			IAsyncResult result = control.BeginInvoke(theMethod);
			if (wait == false)
				return;
			while (result.IsCompleted == false && Instance.ApplicationClosing == false)
			{
				result.AsyncWaitHandle.WaitOne(10000, false);
			}
		}
	}
}
