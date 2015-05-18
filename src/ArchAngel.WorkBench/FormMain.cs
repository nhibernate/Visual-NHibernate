using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using ArchAngel.Common;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Controls.ContentItems;
using ArchAngel.Interfaces.Wizards.NewProject;
using ArchAngel.Workbench.Properties;
using ArchAngel.Workbench.Wizards.NewProject;
using DevComponents.DotNetBar;
using log4net;
using Slyce.Common;
//using FolderBrowserDialog = Vista_Api.FolderBrowserDialog;

namespace ArchAngel.Workbench
{
	public partial class FormMain : Office2007RibbonForm, IVerificationIssueSolver//, IErrorReporter
	{
		#region Enums
		public enum TaskHelpTypes
		{
			ProjectDetails,
			Database,
			Model,
			Options,
			Generation,
			Merge,
			PreActions,
			PostActions
		}
		#endregion

		internal static Image ImageUnpinned = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Workbench.Resources.Unpinned.png"));
		internal static Image ImageDocument = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Workbench.Resources.Document.png"));
		private string _helpDirectory = "";
		private Dictionary<string, ContentItem> _contentItems = new Dictionary<string, ContentItem>();
		private List<ContentItem> _orderedContentItems = new List<ContentItem>();
		private bool BusyPopulating;
		private bool BusyClosing;
		private bool InitContentItemsIsRunning;
		internal readonly CrossThreadHelper CrossThreadHelper;
		internal static bool IsClosed;
		private bool UpdateCheckPerformed;
		private bool UpdatesExist;
		private readonly List<RibbonTabItem> ProviderRibbonTabs = new List<RibbonTabItem>();
		private readonly List<RibbonPanel> ProviderRibbonPanels = new List<RibbonPanel>();
		private readonly DoubleLookup<RibbonTabItem, ContentItem> Screens = new DoubleLookup<RibbonTabItem, ContentItem>();

		private readonly RibbonBarControllerBase ribbonBarController = new RibbonBarControllerBase();

		public static ContentItems.Options ContentItemOptions;
		public static ContentItems.Output ContentItemOutput;
		public static ContentItems.Templates ContentItemTemplate;

		private readonly ObservableTraceListener traceListener = new ObservableTraceListener(50000);

		private static readonly ILog log = LogManager.GetLogger(typeof(FormMain));
		private bool saveAsSuccessful;

		public FormMain()
		{
			SharedData.IsBusyGenerating = false;

			InitializeComponent();

			SetStyle(
					ControlStyles.UserPaint |
					ControlStyles.AllPaintingInWmPaint |
					ControlStyles.OptimizedDoubleBuffer, true);

			log.Debug("Main Form initialised.");

			Settings.Default.Reload();

			labelAboutCopyright.Text = AssemblyCopyright;
			//labelAboutProductDetails.Text = string.Format("{0} version {1}", AssemblyTitle, Assembly.GetExecutingAssembly().GetName().Version);

			ThemeChange("Office2010Black");
			SetStyleCheckboxes(buttonItemStyleVistaGlass);
			this.BackColor = Color.FromArgb(40, 40, 40);
			panelContent.BackColor = this.BackColor;
			cbWordWrap.Checked = textBoxTrace.WordWrap;

			if (Utility.InDesignMode) { return; }

			Text = Branding.FormTitle;

			//ErrorReportingService.RegisterErrorReporter(this);
			//throw new Exception("GFH testing");
			MinimumSize = new Size(750, 550);
			Size size = Controller.Instance.WindowSize;

			if (size.Width >= Screen.PrimaryScreen.WorkingArea.Width ||
				size.Height >= Screen.PrimaryScreen.WorkingArea.Height)
			{
				size.Width = Screen.PrimaryScreen.WorkingArea.Width;
				size.Height = Screen.PrimaryScreen.WorkingArea.Height;
				WindowState = FormWindowState.Maximized;
			}
			else // Size is less than max
			{
				if (size.Width < MinimumSize.Width ||
					size.Height < MinimumSize.Height)
				{
					//size = recommendedSize;
					size.Width = Screen.PrimaryScreen.WorkingArea.Width;
					size.Height = Screen.PrimaryScreen.WorkingArea.Height;
					WindowState = FormWindowState.Maximized;
				}
				Width = size.Width;
				Height = size.Height;

				Top = (Screen.PrimaryScreen.WorkingArea.Height - size.Height) / 2;
				Left = (Screen.PrimaryScreen.WorkingArea.Width - size.Width) / 2;
			}
			Interfaces.Events.RefreshApplicationEvent += Helper_RefreshApplicationEvent;
			Controller.Instance.OnTemplateLoaded += Project_OnTemplateLoaded;

			// Set up Log4Net to use the app.config file.
			log4net.Config.XmlConfigurator.Configure();

			// Set up our log listener. Log4Net has a TraceAppender set up to 
			// send log messages as Trace messages, so we can show them in our window.
			cbEnableDebugLogging.Checked = LogDebuggingInfoEnabled;
			Trace.Listeners.Add(traceListener);
			traceListener.TraceUpdated += traceListener_TraceUpdated;
			Controller.Instance.MainForm = this;
			CrossThreadHelper = new CrossThreadHelper(this);
			PopulateRecentFiles();
			PopulateLicenseStatus();
		}

		private void PopulateLicenseStatus()
		{
			labelVersion.Text = string.Format("Version:  {0}", Assembly.GetExecutingAssembly().GetName().Version);

			//string message;
			//int daysRemaining;
			//bool errorOccurred;
			//bool demo = false;
			//SlyceAuthorizer.LockTypes lockType;
			//SlyceAuthorizer.LicenseStates status;
			//bool licensed = SlyceAuthorizer.IsLicensed("Visual NHibernate License.SlyceLicense", out message, out daysRemaining, out errorOccurred, out demo, out lockType, out status);

			//if (errorOccurred)
			//{
			//	labelErrorMessage.Text = "An error occurred with the Slyce licensing system. Please inform support@slyce.com about this error:\n\nError: " + message;
			//}
			//else if (licensed && !demo)
			//{
			//	if (message.Length > 0)
			//	{
			//		labelErrorMessage.Text = message;
			//	}
			//	labelLicenseDetails.Text = "License Details";
			//	//buttonRemove.Enabled = true;
			//	string registrationDetails = string.Format("{1}{0}{2}{0}{3}{0}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}",
			//		Environment.NewLine,
			//		SlyceAuthorizer.Lic.Name,
			//		SlyceAuthorizer.Lic.Email,
			//		SlyceAuthorizer.Lic.Phone,
			//		SlyceAuthorizer.Lic.Company,
			//		SlyceAuthorizer.Lic.AddressLine1,
			//		SlyceAuthorizer.Lic.AddressLine2,
			//		SlyceAuthorizer.Lic.AddressCity,
			//		SlyceAuthorizer.Lic.AddressCountry);

			//	labelSerialNumber.Text = SlyceAuthorizer.Lic.Serial;
			//	labelLicenseRegistrationDetails.Text = registrationDetails.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
			//}
			//else if (licensed && demo)
			//{
			//	labelLicenseDetails.Text = string.Format("License Details", daysRemaining);
			//	labelRegisteredTo.Text = string.Format(@"Trial version: <font color='Red'>{0} days</font> remaining", daysRemaining);
			//	//labelStatus.Text = string.Format("{0} days remaining of your extended trial.", daysRemaining);
			//	//buttonRemove.Enabled = false;
			//}
			//else
			//{
				//switch (lockType)
				//{
				//    case SlyceAuthorizer.LockTypes.Days:
				//        labelStatus.Text = string.Format("{0} days remaining of your 30-day trial.", daysRemaining);
				//        break;
				//    default:
				//        labelStatus.Text = string.Format("{0} days remaining of your trial.", daysRemaining);
				//        break;
				//}
				//labelLicenseDetails.Text = string.Format("License Details", daysRemaining);
				//labelRegisteredTo.Text = string.Format(@"Trial version: <font color='Red'>{0} days</font> remaining", daysRemaining);
				//labelStatus.Text = string.Format("{0} days remaining of your trial.", daysRemaining);
				//buttonRemove.Enabled = false;
			//}
			//if (!licensed)
			//{
			//	labelSerialNumber.Visible = false;
			//	buttonCopySerial.Visible = false;
			//	labelLicenseRegistrationDetails.Visible = false;
			//}
			//if (licensed || demo)
			//{
			//    Dictionary<string, string> licenseDetails = SlyceAuthorizer.AdditionalLicenseInfo;

			//    foreach (string key in licenseDetails.Keys)
			//    {
			//        listDetails.Items.Add(new ListViewItem(new string[] { key, licenseDetails[key] }));

			//        if (key.ToLower().Replace(" ", "") == "licensenumber")
			//        {
			//            LicenseNumber = licenseDetails[key];
			//        }
			//    }
			//    if (licensed)
			//    {
			//        txtSerial.Text = SlyceAuthorizer.Serial;
			//    }
			//    else
			//    {
			//        txtSerial.Enabled = false;
			//    }

			//}
			panelAbout.Top = labelLicenseRegistrationDetails.Bottom + 10;
		}


		private BaseItem CreateMruFileView(string item)
		{
			string filename = Path.GetFileName(item);
			string directory = Path.GetDirectoryName(item);
			ItemDockContainer container = new ItemDockContainer();
			container.LastChildFill = true;
			// Create Pin Button
			ButtonItem pinButton = new ButtonItem();
			pinButton.ImagePaddingHorizontal = 6;
			pinButton.Image = ImageUnpinned;
			container.SetDock(pinButton, eItemDock.Right); // Position pin button on right side
			container.SubItems.Add(pinButton);
			// Create button with file name and folder
			ButtonItem fileButton = new ButtonItem();
			fileButton.ForeColor = Color.Black;
			fileButton.ButtonStyle = eButtonStyle.ImageAndText;
			fileButton.ImagePosition = eImagePosition.Left;
			fileButton.Text = filename + "<br/><font color=\"Gray\">" + directory + "</font>";
			fileButton.Image = ImageDocument;
			fileButton.Click += new EventHandler(recentFileButton_Click);
			fileButton.Tag = item;
			container.SubItems.Add(fileButton);

			return container;
		}

		void recentFileButton_Click(object sender, EventArgs e)
		{
			Controller.Instance.OpenProjectFile(((ButtonItem)sender).Tag.ToString());
			superTabControlFileMenu.Hide();
		}

		public static string AssemblyCopyright
		{
			get
			{
				// Get all Copyright attributes on this assembly
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				// If there aren't any Copyright attributes, return an empty string
				if (attributes.Length == 0)
					return "";
				// If there is a Copyright attribute, return its value
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		public static string AssemblyTitle
		{
			get
			{
				// Get all Title attributes on this assembly
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				// If there is at least one Title attribute
				if (attributes.Length > 0)
				{
					// Select the first one
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					// If it is not an empty string, return it
					if (titleAttribute.Title != "")
						return titleAttribute.Title;
				}
				// If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
				return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public bool LogDebuggingInfoEnabled
		{
			get { return Settings.Default.DebugLoggingEnabled; }
			set
			{
				Settings.Default.DebugLoggingEnabled = value;
				TraceLogger.Enabled = value;
			}
		}

		private void traceListener_TraceUpdated(object sender, TraceEventArgs e)
		{
			if (InvokeRequired)
			{
				Controller.SafeInvoke(this, new MethodInvoker(() => traceListener_TraceUpdated(sender, e)), false);
				return;
			}

			if (textBoxTrace.IsDisposed)
				return;

			if (cbLimitSize.Checked && textBoxTrace.Text.Length > traceListener.MaximumCapacity * 2 || textBoxTrace.Text.Length == 0)
			{
				textBoxTrace.Clear();
				textBoxTrace.AppendText(traceListener.ToString());
			}
			else
			{
				textBoxTrace.AppendText(e.NewText);
			}

			if (cbAutoScroll.Checked)
				textBoxTrace.ScrollToCaret();

		}

		void Helper_RefreshApplicationEvent()
		{
			Application.DoEvents();
		}

		void Project_OnTemplateLoaded()
		{
			GC.Collect();
			InitContentItems();
		}

		///<summary>
		/// Directory of help file.
		///</summary>
		///<exception cref="FileNotFoundException"></exception>
		public string HelpDirectory
		{
			get
			{
				if (_helpDirectory.Length == 0)
				{
					string dir = Path.GetDirectoryName(Application.ExecutablePath);

					DirectoryInfo directory = new DirectoryInfo(dir);

					while (directory != null && directory.GetDirectories("Help").Length == 0)
					{
						directory = directory.Parent;
					}
					if (directory != null)
					{
						_helpDirectory = Path.Combine(directory.FullName, "Help");
					}
					else
					{
						throw new FileNotFoundException("Help folder is missing.");
					}
				}
				return _helpDirectory;
			}
		}

		private void CreateNewProject(bool appIsStarting)
		{
			try
			{
				if (ContentItemOutput != null) ContentItemOutput.CancelCurrentTask();
				Controller.Instance.CreatingNewProject = true;
				if (BusyPopulating)
				{
					if (Controller.Instance.CurrentProject == null)
						Controller.Instance.CurrentProject = new WorkbenchProject();

					Controller.Instance.CurrentProject.Reset(true);
					Controller.Instance.CurrentProject.NewAppConfig();
					panelContent.Controls.Clear();
					InitContentItems();
					ShowContentItem(null);
					Show();
					Controller.Instance.MustDisplaySplash = false;
					//Program.HideSplashScreen();
				}
				Refresh();

				//Refresh();
				frmNewProject form = new frmNewProject();
				form.SetupAction = BusyPopulating ? NewProjectFormActions.None : NewProjectFormActions.NewProject;
				form.ShowForm(this, appIsStarting);

				if (form.ApplicationExiting || !form.UserClickedFinish)
					return;

				Refresh();

				if (form.UserChosenAction == NewProjectFormActions.ExistingProject &&
					string.IsNullOrEmpty(form.ExistingProjectPath))
				{
					form.UserChosenAction = NewProjectFormActions.NewProject;
				}
				switch (form.UserChosenAction)
				{
					case NewProjectFormActions.ExistingProject:
						Controller.Instance.CurrentProject.Reset(true);
						Controller.Instance.CurrentProject.NewAppConfig();
						Controller.Instance.OpenProjectFile(form.ExistingProjectPath);
						ShowContentItem(GetFirstScreen());
						Controller.Instance.IsDirty = false;
						superTabControlFileMenu.Hide();
						break;
					case NewProjectFormActions.NewProject:
						string projectFilename = string.IsNullOrEmpty(form.NewProjectFolder)
													? ""
													: Path.Combine(form.NewProjectFolder,
																   form.NewProjectName +
																   ".wbproj");

						Controller.Instance.CreateNewProject(form.NewProjectOutputPath ?? "",
															 form.NewProjectTemplate,
															 projectFilename ?? "");
						//InitContentItems();

						ShowContentItem(GetFirstScreen());

						try
						{
							Controller.Instance.InitProjectFromProjectWizardInformation(form.NewProjectInformation);
						}
						catch (Exception e)
						{
							string message = string.Format("An error occurred accessing the database:{0}{1}", Environment.NewLine, e.Message);

							Exception innerException = e.InnerException;

							while (innerException != null)
							{
								message += Environment.NewLine + "INNER EXCEPTION: " + e.InnerException.Message;
								innerException = innerException.InnerException;
							}
							//if (e.InnerException != null)
							//    message += Environment.NewLine + e.InnerException.Message;
							MessageBox.Show(this, message, "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							throw;
						}

						if (ContentItemOutput != null) ContentItemOutput.ProjectPathChanged();

						Text = Branding.FormTitle + " - " + Path.GetFileName(Controller.Instance.CurrentProject.ProjectFile) + " *";
						superTabControlFileMenu.Hide();
						break;
					case NewProjectFormActions.None:
						// Do nothing
						break;
					default:
						throw new NotImplementedException("Not handled yet: " + form.UserChosenAction);
				}
				if (backgroundWorkerUpdateChecker.IsBusy == false)
				{
					backgroundWorkerUpdateChecker.RunWorkerAsync();
				}
			}
			finally
			{
				Controller.Instance.CreatingNewProject = false;
				Controller.Instance.BusyPopulating = false;
			}
		}

		private ContentItem GetFirstScreen()
		{
			return _orderedContentItems.FirstOrDefault();
		}

		private void OpenProject()
		{
			Refresh();
			Controller.Instance.ShadeMainForm();
			OpenFileDialog openFileDialog = new OpenFileDialog();

			if (Controller.Instance.CurrentProject.AppConfigFilename != null)
			{
				openFileDialog.InitialDirectory = Path.GetDirectoryName(Controller.Instance.CurrentProject.AppConfigFilename);
			}

			openFileDialog.Filter = Branding.FormTitle + " Project (*.wbproj, *.aaproj)|*.wbproj;*.aaproj";

			if (openFileDialog.ShowDialog(this) != DialogResult.OK)
			{
				Controller.Instance.UnshadeMainForm();
				return;
			}
			Controller.Instance.UnshadeMainForm();
			Controller.Instance.OpenProjectFile(openFileDialog.FileName);
			superTabControlFileMenu.Hide();
		}

		internal void OnOpenProjectFile()
		{
			log.Debug("OnOpenProjectFile() called");
			Utility.DisplayMessagePanel(this, "Loading project...", Slyce.Common.Controls.MessagePanel.ImageType.Hourglass);
			panelContent.Controls.Clear();
			ShowContentItem(GetFirstScreen());
			ribbonControl.SelectedRibbonTabItem = Screens[GetFirstScreen()];
			Text = Branding.FormTitle + " - " + Path.GetFileName(Controller.Instance.CurrentProject.ProjectFile);

			IWorkbenchProject project = Controller.Instance.CurrentProject;
			if (project != null && project.ProjectSettings != null)
			{
				string path = project.ProjectSettings.OutputPath;
				if (path != null && Directory.Exists(path) && ContentItemOutput != null)
				{
					ContentItemOutput.ProjectPathChanged();
				}
			}
		}

		internal void AddRecentFile(string filepath)
		{
			filepath = filepath.Trim();

			if (File.Exists(filepath))
			{
				int numMissingFiles = 0;

				foreach (string file in Controller.Instance.RecentFiles)
				{
					if (!File.Exists(file.Trim()))
					{
						numMissingFiles++;
					}
					if (filepath == file.Trim())
					{
						break;
					}
				}
				string[] newRecentFiles = new string[Controller.Instance.RecentFiles.Length + 1 - numMissingFiles];
				int ctr = 0;

				for (int i = 0; i < Controller.Instance.RecentFiles.Length; i++)
				{
					if (File.Exists(Controller.Instance.RecentFiles[i]))
					{
						newRecentFiles[ctr + 1] = Controller.Instance.RecentFiles[i].Trim();
						ctr++;
					}
				}
				newRecentFiles[0] = filepath;
				Controller.Instance.RecentFiles = newRecentFiles;
			}
			PopulateRecentFiles();
		}

		internal bool Save()
		{
			Cursor originalCursor = Cursor;
			Cursor = Cursors.WaitCursor;

			try
			{
				ArchAngel.Interfaces.SharedData.RaiseAboutToSave();

				if (string.IsNullOrEmpty(Controller.Instance.CurrentProject.ProjectFile) ||
					!Directory.Exists(Path.GetDirectoryName(Controller.Instance.CurrentProject.ProjectFile)))
				{
					SaveAs();

					if (saveAsSuccessful)
						superTabControlFileMenu.Hide();

					return saveAsSuccessful;
				}
				else
				{
					string errorMessage;
					bool saveSuccessful = Controller.Instance.SaveProjectFile(out errorMessage);

					if (saveSuccessful)
						superTabControlFileMenu.Hide();
					else
						MessageBox.Show(this, errorMessage, "Save error", MessageBoxButtons.OK, MessageBoxIcon.Error);

					return saveSuccessful;
				}
			}
			finally
			{
				Cursor = originalCursor;
				Controller.Instance.WritingToUserFolder = false;
			}
		}

		private void SaveAs()
		{
			saveAsSuccessful = false;
			Refresh();

			SaveFileDialog saveFileDialog = new SaveFileDialog
												{
													FileName = Path.GetFileName(Controller.Instance.CurrentProject.ProjectFile),
													Filter = (Branding.ProductName + " Project (*.wbproj)|*.wbproj")
												};
			saveFileDialog.InitialDirectory = GetProjectsFolder();
			Controller.Instance.ShadeMainForm();

			if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
			{
				Controller.Instance.UnshadeMainForm();
				return;
			}
			Controller.Instance.UnshadeMainForm();
			Controller.Instance.CurrentProject.ProjectFile = saveFileDialog.FileName;
			saveAsSuccessful = Save();
			Text = Branding.FormTitle + " - " + Path.GetFileName(saveFileDialog.FileName);
		}

		public static string GetProjectsFolder()
		{
			string vnhFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Visual NHibernate");

			if (!Directory.Exists(vnhFolder))
				Directory.CreateDirectory(vnhFolder);

			string folder = Path.Combine(vnhFolder, "Projects");

			if (!Directory.Exists(folder))
				Directory.CreateDirectory(folder);

			return folder;
		}


		private void AutoCheckForUpdates()
		{
#if !DEBUG
			if (!UpdateCheckPerformed)
			{
				Slyce.Common.Updates.frmUpdate.SilentMode = true;
				UpdateCheckPerformed = true;
				UpdatesExist = Slyce.Common.Updates.frmUpdate.UpdateExists(Branding.ProductBranding.ToString());
				Slyce.Common.Updates.frmUpdate.SilentMode = false;
			}
#endif
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			//try
			//{
			Cursor = Cursors.WaitCursor;
			BusyPopulating = true;

			// Load last config file
			string fileName = null;// = ArchAngel.Interfaces.SharedData.RegistryGetValue("DefaultConfig");

			// When a file is opened through association (right-clicking an STZ file in Windows Explorer), then
			// the path is passed as a commandline parameter, but without quotes. This means that if the path has
			// spaces in it, it appears as separate arguments. We need to check for this before proceeding.
			if (Environment.CommandLine.IndexOf("\"", 1) > 0)
			{
				string commandLineFile = Environment.CommandLine.Substring(Environment.CommandLine.IndexOf("\"", 1) + 1).Trim(new[] { '"', ' ' });

				if (File.Exists(commandLineFile) && Path.GetExtension(commandLineFile).ToLowerInvariant() == ".aaproj")
				{
					fileName = commandLineFile;
				}
			}
			if (fileName != null && File.Exists(fileName))
			{
				InitContentItems();
				Controller.Instance.OpenProjectFile(fileName);

				panelContent.Controls.Clear();
				ShowContentItem(GetFirstScreen());
				Text = Branding.FormTitle + " - " + Path.GetFileName(fileName);
				//Program.HideSplashScreen();
			}
			else
			{
				Controller.Instance.MustDisplaySplash = false;
				Refresh();
				Cursor = Cursors.Default;
				// Opens a new blank project.
				try
				{
					CreateNewProject(true);
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				BusyPopulating = false;
				//Program.HideSplashScreen();
			}
			//}
			//catch (Exception ex)
			//{
			//    Controller.ReportError(ex);
			//}
			Cursor = Cursors.Default;
			BusyPopulating = false;
		}

		private void InitContentItems()
		{
			if (InitContentItemsIsRunning) { return; }
			InitContentItemsIsRunning = true;
			Cursor originalCursor = Cursor;
			try
			{
				Cursor = Cursors.WaitCursor;

				Clear();
				Screens.Clear();

				Interfaces.Events.IsDirtyEvent += ContentItem_IsDirtyEvent;
				Interfaces.Events.SetCursorEvent += ContentItem_SetCursorEvent;
				Interfaces.Events.ShadeMainFormEvent += ContentItem_ShadeMainFormEvent;
				Interfaces.Events.UnShadeMainFormEvent += ContentItem_UnShadeMainFormEvent;
				Interfaces.Events.SetBusyPopulatingEvent += ContentItem_SetBusyPopulatingEvent;
				//Interfaces.Events.ReportErrorEvent += ContentItem_ReportErrorEvent;

				#region Create Provider Screens
				if (Controller.Instance.CurrentProject != null)
				{
					ribbonBarController.Clear();

					int tabIndex = 1;// 0;

					foreach (ProviderInfo provider in Controller.Instance.CurrentProject.Providers)
					{
						provider.CreateScreens();

						foreach (ContentItem screen in provider.Screens)
						{
							screen.Dock = DockStyle.Fill;

							// Add event handlers
							Controller.Instance.OnProjectLoaded += screen.ProjectLoadedEventHandler;
							Controller.Instance.OnTemplateLoaded += screen.TemplateLoadedEventHandler;
							Controller.Instance.OnDataChanged += screen.OnDataChanged;

							_contentItems.Add(screen.GetType().FullName, screen);
							_orderedContentItems.Add(screen);

							RibbonTabItem ribbonTabItem = new RibbonTabItem
															{
																Text = " " + screen.Title,
																Tooltip = screen.PageHeader, //screen.PageDescription
																Image = screen.NavBarIcon,
																ButtonStyle = eButtonStyle.ImageAndText
															};

							RibbonPanel ribbonPanel = new RibbonPanel
							{
								//ColorSchemeStyle = eDotNetBarStyle.Office2007,
								Dock = DockStyle.Fill,
								Location = new Point(0, 55),
								Padding = new System.Windows.Forms.Padding(3, 0, 3, 3)
							};
							ribbonPanel.Style.BackColor = Color.FromArgb(95, 95, 95);

							if (screen is RibbonBarContentItem)
								ribbonBarController.ProcessRibbonBarButtons(screen as RibbonBarContentItem, ribbonPanel);
							else
								throw new Exception("Please inform Slyce: screen is not RibbonBarContentItem, it is " + screen.GetType().Name);

							ribbonControl.Controls.Add(ribbonPanel);
							ribbonTabItem.Panel = ribbonPanel;
							ribbonTabItem.Click += ribbonTab_Click;
							ribbonControl.Items.Insert(tabIndex++, ribbonTabItem);
							ProviderRibbonPanels.Add(ribbonPanel);
							ProviderRibbonTabs.Add(ribbonTabItem);

							Screens.Add(ribbonTabItem, screen);
						}

						ribbonBarController.RefreshButtonStatus();
					}
				}
				#endregion

				#region Options screen
				if (ContentItemOptions == null)
				{
					ContentItemOptions = new ContentItems.Options { Dock = DockStyle.Fill };
					Controller.Instance.OnProjectLoaded += ContentItemOptions.ProjectLoadedEventHandler;
					Controller.Instance.OnTemplateLoaded += ContentItemOptions.TemplateLoadedEventHandler;
					Controller.Instance.OnDataChanged += ContentItemOptions.OnDataChanged;
				}
				//else
				//{
				//    ContentItemOptions.Clear();
				//}

				Screens.Add(ribbonTabItemOptions, ContentItemOptions);
				_contentItems.Add(ContentItemOptions.GetType().FullName, ContentItemOptions);
				_orderedContentItems.Add(ContentItemOptions);
				#endregion

				#region Template screen
				if (ContentItemTemplate == null)
				{
					ContentItemTemplate = new ContentItems.Templates { Dock = DockStyle.Fill };
					Controller.Instance.OnProjectLoaded += ContentItemTemplate.ProjectLoadedEventHandler;
					Controller.Instance.OnTemplateLoaded += ContentItemTemplate.TemplateLoadedEventHandler;
					Controller.Instance.OnDataChanged += ContentItemTemplate.OnDataChanged;
				}
				//else
				//{
				//    ContentItemOptions.Clear();
				//}

				Screens.Add(ribbonTabItemTemplate, ContentItemTemplate);
				_contentItems.Add(ContentItemTemplate.GetType().FullName, ContentItemTemplate);
				_orderedContentItems.Add(ContentItemTemplate);
				#endregion


				#region Output screen
				if (ContentItemOutput == null)
				{
					ContentItemOutput = new ContentItems.Output { Dock = DockStyle.Fill };
					Controller.Instance.OnProjectLoaded += ContentItemOutput.ProjectLoadedEventHandler;
					Controller.Instance.OnTemplateLoaded += ContentItemOutput.TemplateLoadedEventHandler;
					Controller.Instance.OnDataChanged += ContentItemOutput.OnDataChanged;
				}
				else
				{
					ContentItemOutput.Clear();
				}

				Screens.Add(ribbonTabItemFiles, ContentItemOutput);
				_contentItems.Add(ContentItemOutput.GetType().FullName, ContentItemOutput);
				_orderedContentItems.Add(ContentItemOutput);
				#endregion
			}
			finally
			{
				InitContentItemsIsRunning = false;
				Cursor = originalCursor;
			}
		}

		private void Clear()
		{
			if (_orderedContentItems.Count > 0)
			{
				for (int i = 0; i < _orderedContentItems.Count; i++)
				{
					// Foreach of the content screens, remove the event handler.
					ContentItem contentItem = _orderedContentItems[i];
					Controller.Instance.OnProjectLoaded -= contentItem.ProjectLoadedEventHandler;
					Controller.Instance.OnTemplateLoaded -= contentItem.TemplateLoadedEventHandler;
					Controller.Instance.OnDataChanged -= contentItem.OnDataChanged;
				}
			}
			_contentItems.Clear();

			ClearProviderRibbonTabs();
			_orderedContentItems.Clear();

			Screens.Clear();

			_contentItems = new Dictionary<string, ContentItem>();
			_orderedContentItems = new List<ContentItem>();
		}


		/// <summary>
		/// Removes provider-specific RibbonTabs from the RibbonControl, and resets the Lists.
		/// </summary>
		private void ClearProviderRibbonTabs()
		{
			foreach (var ribbonTabItem in ProviderRibbonTabs)
			{
				if (ribbonControl.Items.Contains(ribbonTabItem))
					ribbonControl.Items.Remove(ribbonTabItem);

				ribbonTabItem.Click -= ribbonTab_Click;
			}
			foreach (var ribbonPanel in ProviderRibbonPanels)
				ribbonControl.Controls.Remove(ribbonPanel);

			ProviderRibbonTabs.Clear();
			ProviderRibbonPanels.Clear();
			ribbonBarController.Clear();
		}

		internal void ShowContentItemByName(string controlName)
		{
			foreach (ContentItem ctl in panelContent.Controls)
			{
				if (ctl.Name == controlName)
				{
					ShowContentItem(ctl);
					return;
				}
			}
			throw new NotImplementedException("Not handled yet: " + controlName);
		}

		internal void ShowContentItem(ContentItem ctlToKeep)
		{
			if (InvokeRequired)
			{
				Controller.SafeInvoke(this, new MethodInvoker(() => ShowContentItem(ctlToKeep)), true);
				return;
			}

			SetCursor(Cursors.WaitCursor);
			Utility.SuspendPainting(panelContent);

			if (ctlToKeep is ContentItems.Templates)
				((ContentItems.Templates)ctlToKeep).Populate();

			if (ctlToKeep != null && !panelContent.Controls.Contains(ctlToKeep))
				panelContent.Controls.Add(ctlToKeep);

			for (int i = 0; i < panelContent.Controls.Count; i++)
			{
				if (panelContent.Controls[i] != ctlToKeep)
				{
					if (panelContent.Controls[i] is ContentItems.Templates && panelContent.Controls[i].Visible)
						((ContentItems.Templates)panelContent.Controls[i]).SaveCurrent();

					CrossThreadHelper.SetVisibility(panelContent.Controls[i], false);
				}
				else
				{
					CrossThreadHelper.SetVisibility(panelContent.Controls[i], true);
					panelContent.Controls[i].Refresh();
				}
			}

			if (ctlToKeep == null)
				ribbonControl.CloseRibbonMenu();
			else
			{
				ctlToKeep.OnDisplaying();

				var ribbonTab = Screens[ctlToKeep];
				ribbonControl.SelectedRibbonTabItem = ribbonTab;
			}
			if (ctlToKeep is ContentItems.Options)
			{
				((ContentItems.Options)ctlToKeep).UpdateValues();
			}

			Utility.ResumePainting(panelContent);
			SetCursor(Cursors.Default);
		}

		private void SetCursor(Cursor value)
		{
			if (InvokeRequired)
			{
				CrossThreadHelper.SetCrossThreadProperty(this, "Cursor", value);
			}
			else
			{
				Cursor = value;
			}
		}

		//void ContentItem_ReportErrorEvent(Exception ex)
		//{
		//    Controller.ReportError(ex);
		//}

		void ContentItem_SetBusyPopulatingEvent(bool busyPopulating)
		{
			Controller.Instance.BusyPopulating = busyPopulating;
		}

		void ContentItem_UnShadeMainFormEvent()
		{
			Controller.Instance.UnshadeMainForm();
		}

		void ContentItem_ShadeMainFormEvent()
		{
			Controller.Instance.ShadeMainForm();
		}

		void ContentItem_SetCursorEvent(Cursor cursor)
		{
			Cursor = cursor;
		}

		private void ContentItem_IsDirtyEvent()
		{
			Controller.Instance.IsDirty = true;
		}

		private void FormMain_Resize(object sender, EventArgs e)
		{
			Invalidate();
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			Controller.Instance.WindowSize = Size;

			// Check the settings file for corruption
			{
				string filename = AASettingsProvider.GetSettingsFilename();
				if (File.Exists(filename))
				{
					try
					{
						XmlDocument doc = new XmlDocument();
						doc.Load(filename);
					}
					catch (XmlException)
					{
						try
						{
							File.Delete(filename);
						}
						// ReSharper disable EmptyGeneralCatchClause
						catch
						// ReSharper restore EmptyGeneralCatchClause
						{
						}
					}
				}
			}
			Settings.Default.Save();

			if (Controller.Instance.IsDirty && !BusyClosing)
			{
				try
				{
					Controller.Instance.ShadeMainForm();
					DialogResult result = MessageBox.Show(this, "Save changes?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

					if (result == DialogResult.Yes)
					{
						// Don't exit if there was a problem saving
						if (!Save())
						{
							e.Cancel = true;
							return;
						}
					}
					else if (result == DialogResult.Cancel)
					{
						e.Cancel = true;
						return;
					}
				}
				finally
				{
					Controller.Instance.UnshadeMainForm();
				}
			}

			try
			{
#if !DEV
				// Clean up
				string[] tempFolders = Directory.GetDirectories(Path.GetTempPath(), "ArchAngelShadow_*");

				foreach (string folder in tempFolders)
				{
					try
					{
						// Don't call Slyce.Common.Utility.DeleteDirectoryBrute(), because we want to shutdown quickly
						Directory.Delete(folder, true);
					}
					catch (Exception)
					{
						// Do nothing
					}
				}
#endif
				BusyClosing = true;
				Application.Exit();
			}
			catch
			{
				// Do nothing
			}
		}

		private static void ShowHelpFile()
		{
			System.Diagnostics.Process.Start(@"http://www.slyce.com/Support/");

			//if (File.Exists("ArchAngel Help.chm"))
			//{
			//    Process.Start("ArchAngel Help.chm");
			//}
			//else
			//{
			//    MessageBox.Show(
			//        "Help is on the way, but not in this release sorry. Please visit our forums http://forums.slyce.com for help.",
			//        "Helpfile Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//    //MessageBox.Show("The ArchAngel help file is missing. Please repair the ArchAngel installation via Control Panel -> Add/Remove Programs.", "Helpfile Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//}
		}

		internal void PopulateRecentFiles()
		{
			recentDocsItemPane.Items.Clear();
			mnuTopOpen2.SubItems.Clear();
			int i = 0;

			foreach (string filePath in Controller.Instance.RecentFiles)
			{
				if (File.Exists(filePath))
				{
					recentDocsItemPane.Items.Add(CreateMruFileView(filePath));

					//Add to toolbar dropdown
					string fileName = string.Format("&{1}) {0}", Path.GetFileName(filePath), i + 1);
					var menuButton = new ButtonItem
										{
											Text = fileName,
											Tooltip = filePath,
											ButtonStyle = eButtonStyle.TextOnlyAlways
										};
					menuButton.Click += boxRecentFile_Click;
					mnuTopOpen2.SubItems.Add(menuButton);
					i++;
				}
			}
		}

		//internal void PopulateRecentFiles()
		//{
		//    mnuTopOpen2.SubItems.Clear();

		//    // Clear the existing list
		//    while (galleryContainer3.SubItems.Count > 1)
		//    {
		//        galleryContainer3.SubItems.Remove(galleryContainer3.SubItems.Count - 1);
		//    }
		//    if (Controller.Instance.RecentFiles.Length > 0)
		//    {
		//        for (int i = 0; i < Controller.Instance.RecentFiles.Length; i++)
		//        {
		//            string filePath = Controller.Instance.RecentFiles[i];

		//            if (File.Exists(filePath))
		//            {
		//                string fileName = string.Format("&{1}) {0}", Path.GetFileName(filePath), i + 1);

		//                // Add to toolbar dropdown
		//                var menuButton = new ButtonItem
		//                                    {
		//                                        Text = fileName,
		//                                        Tooltip = filePath,
		//                                        ButtonStyle = eButtonStyle.TextOnlyAlways
		//                                    };
		//                menuButton.Click += boxRecentFile_Click;
		//                mnuTopOpen2.SubItems.Add(menuButton);

		//                // Add to File menu
		//                var boxButton = new ButtonItem
		//                                    {
		//                                        Text = fileName,
		//                                        Tooltip = filePath,
		//                                        ButtonStyle = eButtonStyle.TextOnlyAlways
		//                                    };
		//                boxButton.Click += boxRecentFile_Click;
		//                galleryContainer3.SubItems.Add(boxButton);
		//            }
		//        }
		//        mnuTopOpen2.Refresh();
		//    }
		//}

		static void boxRecentFile_Click(object sender, EventArgs e)
		{
			Controller.Instance.OpenProjectFile(((ButtonItem)sender).Tooltip);
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			IsClosed = true;
		}

		private void ShowLicense()
		{
			//Refresh();
			//Controller.Instance.ShadeMainForm();
			//Licensing.frmStatus form = new Licensing.frmStatus("Visual NHibernate License.SlyceLicense");
			//form.ShowDialog(this);
			//Controller.Instance.UnshadeMainForm();
		}

		private void FormMain_KeyDown(object sender, KeyEventArgs e)
		{
			//if (e.Alt && e.Shift && e.KeyCode == Keys.D)
			//{
			//    // Toggle trace panel visibility.
			//    tracePanel.Visible = !tracePanel.Visible;
			//    e.Handled = true;
			//}
			//else if (e.Control && e.KeyCode == Keys.F)
			//{
			//    // Toggle trace panel visibility.
			//    tracePanel.Visible = !tracePanel.Visible;
			//    e.Handled = true;
			//}
			if (e.Control && e.KeyCode == Keys.F &&
				ribbonControl.SelectedRibbonTabItem == ribbonTabItemTemplate)
			{
				ContentItems.Templates.Instance.ShowFindForm(false, e.Shift);
			}
			else if (e.Control && e.KeyCode == Keys.H &&
				ribbonControl.SelectedRibbonTabItem == ribbonTabItemTemplate)
			{
				ContentItems.Templates.Instance.ShowFindForm(true, e.Shift);
			}
			else if (e.KeyCode == Keys.F3 &&
				ribbonControl.SelectedRibbonTabItem == ribbonTabItemTemplate)
			{
				SearchHelper.Search();
			}
		}

		private void cbLimitSize_CheckedChanged(object sender, EventArgs e)
		{
			traceListener.LimitSize = cbLimitSize.Checked;
		}

		private void cbWordWrap_CheckedChanged(object sender, EventArgs e)
		{
			textBoxTrace.WordWrap = cbWordWrap.Checked;
		}

		private void btnSaveLogToFile_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog
									{
										CheckFileExists = false,
										CheckPathExists = false,
										DereferenceLinks = true,
										ValidateNames = true
									};

			DialogResult result = sfd.ShowDialog();
			if (result != DialogResult.OK) return;

			if (Directory.Exists(Path.GetDirectoryName(sfd.FileName)) == false)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(sfd.FileName));
			}
			File.WriteAllText(sfd.FileName, traceListener.ToString(), System.Text.Encoding.Unicode);
		}

		private void cbEnableDebugLogging_CheckedChanged(object sender, EventArgs e)
		{
			LogDebuggingInfoEnabled = cbEnableDebugLogging.Checked;
		}

		public string GetValidTemplateFilePath(string projectFile, string oldTemplateFile)
		{
			DialogResult r = MessageBox.Show(this,
										string.Format("Template file not found: [{0}]. Click Ok to select a new template or cancel to select a different project.", oldTemplateFile), "Template Assembly missing", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			if (r == DialogResult.Cancel)
				return null;
			OpenFileDialog dialog = new OpenFileDialog
										{
											InitialDirectory = Path.GetDirectoryName(projectFile),
											CheckFileExists = true
										};
			dialog.Filter += "ArchAngel Compiled Template (*.AAT.DLL)|*.AAT.DLL";
			dialog.Filter += "|All files (*.*)|*.*";

			// If exactly one template exists in the folder, elect it
			string[] templateFiles = Directory.GetFiles(dialog.InitialDirectory, "*.aal");

			if (templateFiles.Length == 1)
			{
				dialog.FileName = templateFiles[0];
			}
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				return dialog.FileName;
			}

			MessageBox.Show(this,
							"You have not chosen a template. The project has not been loaded.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return null;
		}

		public string GetValidProjectDirectory(string projectFile, string oldOutputFolder)
		{
			DialogResult r = MessageBox.Show(this, string.Format("Output folder not found: [{0}]. Click Ok to select a new folder or cancel to select a different project.", oldOutputFolder), "Ouput folder missing", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

			if (r == DialogResult.Cancel)
				return null;

			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.SelectedPath = Path.GetDirectoryName(projectFile);
			dialog.ShowNewFolderButton = true;

			//FolderBrowserDialog dialog = new FolderBrowserDialog
			//                                {
			//                                    ShowNewFolderButton = true,
			//                                    SelectedPath = Path.GetDirectoryName(projectFile)
			//                                };
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				return dialog.SelectedPath;
			}
			MessageBox.Show(this, "You have not chosen an output folder. The project has not been loaded.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return null;
		}

		public void InformUserThatAppConfigIsInvalid(string message)
		{
			MessageBox.Show(this, message, "Unrecoverable Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void backgroundWorkerUpdateChecker_DoWork(object sender, DoWorkEventArgs e)
		{
			AutoCheckForUpdates();
		}

		private void backgroundWorkerUpdateChecker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				// Ensure that we get this reported to us.
				throw e.Error;
			}
			if (UpdatesExist)
			{
				if (InvokeRequired)
				{
					MethodInvoker mi = () => backgroundWorkerUpdateChecker_RunWorkerCompleted(sender, e);
					Invoke(mi);
					return;
				}
				Slyce.Common.Updates.frmUpdate form = new Slyce.Common.Updates.frmUpdate(Branding.ProductBranding.ToString());
				form.ShowDialog(this);
			}
		}

		private void mnuBoxNew_Click(object sender, EventArgs e)
		{
			CreateNewProject(false);
		}

		private void mnuBoxOpen_Click(object sender, EventArgs e)
		{
			OpenProject();
		}

		private void mnuBoxSave_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void mnuBoxSaveAs_Click(object sender, EventArgs e)
		{
			SaveAs();
		}

		private void ribbonTab_Click(object sender, EventArgs e)
		{
			ShowContentItem(Screens[sender as RibbonTabItem]);
		}

		private void CheckForUpdates()
		{
			Slyce.Common.Updates.frmUpdate form = new Slyce.Common.Updates.frmUpdate(Branding.ProductBranding.ToString());
			form.ShowDialog(this);
			superTabControlPanelHelp.Refresh();
		}

		private void mnuHelp_Click(object sender, EventArgs e)
		{
			ShowHelpFile();
		}

		private void mnuSaveTop_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void buttonItem17_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void mnuBoxExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void ShowAboutScreen()
		{
			Refresh();
			Controller.Instance.ShadeMainForm();
			AboutBoxArchAngel form = new AboutBoxArchAngel();
			form.ShowDialog(this);
			Controller.Instance.UnshadeMainForm();
		}

		private void buttonItemStyleOfficeBlue_Click(object sender, EventArgs e)
		{
			ThemeChange("Office2007Blue");
			SetStyleCheckboxes(buttonItemStyleOfficeBlue);
		}

		private void buttonItemStyleOfficeBlack_Click(object sender, EventArgs e)
		{
			ThemeChange("Office2007Black");
			SetStyleCheckboxes(buttonItemStyleOfficeBlack);
		}

		private void buttonItemStyleOfficeSilver_Click(object sender, EventArgs e)
		{
			ThemeChange("Office2007Silver");
			SetStyleCheckboxes(buttonItemStyleOfficeSilver);
		}

		private void buttonItemStyleVistaGlass_Click(object sender, EventArgs e)
		{
			ThemeChange("Office2010Black");
			SetStyleCheckboxes(buttonItemStyleVistaGlass);
		}

		private void SetStyleCheckboxes(ButtonItem button)
		{
			buttonItemStyleOfficeBlue.Checked = false;
			buttonItemStyleOfficeBlack.Checked = false;
			buttonItemStyleOfficeSilver.Checked = false;
			buttonItemStyleVistaGlass.Checked = false;
			buttonItemStyleOffice2010Blue.Checked = false;
			buttonItemStyleOffice2010Silver.Checked = false;
			buttonItemStyleWindows7.Checked = false;

			button.Checked = true;
		}

		//public void ReportError(ErrorLevel level, string message)
		//{
		//    if (InvokeRequired)
		//    {
		//        MethodInvoker mi = () => ReportError(level, message);
		//        Invoke(mi);
		//    }
		//    if (level == ErrorLevel.Error)
		//        MessageBox.Show(this, message, "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//}

		//public void ReportUnhandledException(Exception e)
		//{
		//    Controller.ReportError(e);
		//}

		private void mnuRefresh_Click(object sender, EventArgs e)
		{
			ContentItemOutput.GenerateFiles();
		}

		private void mnuBoxAbout_Click(object sender, EventArgs e)
		{
			ShowAboutScreen();
		}

		private void mnuBoxCheckUpdates_Click(object sender, EventArgs e)
		{
			CheckForUpdates();
		}

		private void mnuBoxLicense_Click(object sender, EventArgs e)
		{
			ShowLicense();
		}

		private void mnuTopNew_Click(object sender, EventArgs e)
		{
			CreateNewProject(false);
		}

		private void buttonItem3_Click(object sender, EventArgs e)
		{
			OpenProject();
		}

		private void mnuChangeOutputPath_Click(object sender, EventArgs e)
		{
			ContentItemOutput.ChangeOutputPath();
		}

		private void buttonItemResetDefaultOptions_Click(object sender, EventArgs e)
		{
			if (ContentItemOptions != null)
			{
				ContentItemOptions.ResetDefaults();
			}
		}

		private void buttonItemStyleOffice2010Blue_Click(object sender, EventArgs e)
		{
			ThemeChange("Office2010Blue");
			SetStyleCheckboxes(buttonItemStyleOffice2010Blue);
		}

		private void buttonItemStyleOffice2010Silver_Click(object sender, EventArgs e)
		{
			ThemeChange("Office2010Silver");
			SetStyleCheckboxes(buttonItemStyleOffice2010Silver);
		}

		private void ThemeChange(string styleName)
		{
			eStyle style = (eStyle)Enum.Parse(typeof(eStyle), styleName, true);
			// Using StyleManager change the style and color tinting
			StyleManager.ChangeStyle(style, Color.Empty);
			style = eStyle.Office2010Black;
		}

		private void ThemeChange(Color color)
		{
			StyleManager.ColorTint = color;
		}

		private void buttonItemStyleWindows7_Click(object sender, EventArgs e)
		{
			ThemeChange("Windows7Blue");
			SetStyleCheckboxes(buttonItemStyleWindows7);
		}

		private void buttonStyleCustom_ColorPreview(object sender, ColorPreviewEventArgs e)
		{
			StyleManager.ColorTint = e.Color;
		}

		private bool m_ColorSelected = false;
		private eStyle m_BaseStyle = eStyle.Office2010Silver;
		private void buttonStyleCustom_ExpandChange(object sender, EventArgs e)
		{
			if (buttonStyleCustom.Expanded)
			{
				// Remember the starting color scheme to apply if no color is selected during live-preview
				m_ColorSelected = false;
				m_BaseStyle = StyleManager.Style;
			}
			else
			{
				if (!m_ColorSelected)
				{
					StyleManager.ChangeStyle(m_BaseStyle, Color.Empty);
				}
			}
		}

		private void buttonStyleCustom_SelectedColorChanged(object sender, EventArgs e)
		{
			m_ColorSelected = true; // Indicate that color was selected for buttonStyleCustom_ExpandChange method
			buttonStyleCustom.CommandParameter = buttonStyleCustom.SelectedColor;
		}

		private void mnuWriteFilesToDisk_Click(object sender, EventArgs e)
		{
			ContentItemOutput.WriteFilesToDisk();
		}

		private void mnuForums_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			System.Diagnostics.Process.Start(@"http://forums.slyce.com/");
			Cursor = Cursors.Default;
		}

		private void mnuReportBug_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			System.Diagnostics.Process.Start(@"http://support.slyce.com/");
			Cursor = Cursors.Default;
		}

		private void mnuSuggestion_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			System.Diagnostics.Process.Start(@"http://www.slyce.com/Contact/");
			Cursor = Cursors.Default;
		}

		private void buttonItem2_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Find!");
		}

		private void buttonNewProject_Click(object sender, EventArgs e)
		{
			CreateNewProject(false);
		}

		private void buttonOpenProject_Click(object sender, EventArgs e)
		{
			OpenProject();
		}

		private void buttonExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void buttonCheckForUpdates_Click(object sender, EventArgs e)
		{
			CheckForUpdates();
		}

		private void buttonLicenseDetails_Click(object sender, EventArgs e)
		{
			ShowLicense();
		}

		private void buttonSaveProject_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void buttonSaveAsProject_Click(object sender, EventArgs e)
		{
			SaveAs();
		}

		private void buttonForums_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			System.Diagnostics.Process.Start(@"http://forums.slyce.com/");
			Cursor = Cursors.Default;
		}

		private void buttonReportBug_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			System.Diagnostics.Process.Start(@"http://support.slyce.com/");
			Cursor = Cursors.Default;
		}

		private void buttonSuggestion_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			System.Diagnostics.Process.Start(@"http://www.slyce.com/Contact/");
			Cursor = Cursors.Default;
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(@"http://www.slyce.com");
		}

		private void superTabControlFileMenu_VisibleChanged(object sender, EventArgs e)
		{
			if (superTabControlFileMenu.Visible)
				superTabControlFileMenu.SelectedTab = superTabItemRecentProjects;
		}

		private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void linkLabelLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string licenseFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "license.pdf");

			if (File.Exists(licenseFile))
			{
				System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(licenseFile);
				startInfo.UseShellExecute = true;
				System.Diagnostics.Process.Start(startInfo);
			}
			else
				System.Windows.Forms.MessageBox.Show("License file not found: " + licenseFile);
		}

		private void linkLabelCopySerialNumber_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Clipboard.SetText(labelSerialNumber.Text);
		}

		private void superTabControlPanel8_Click(object sender, EventArgs e)
		{

		}

		private void buttonCopySerial_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(labelSerialNumber.Text);
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(@"http://www.slyce.com");
		}

		private void buttonX1_Click(object sender, EventArgs e)
		{
			CheckForUpdates();
		}
	}
}
