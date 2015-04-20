using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Common;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Designer.Properties;
using ArchAngel.Interfaces;
using DevComponents.DotNetBar;
using Slyce.Common;
using Slyce.Common.Controls;

namespace ArchAngel.Designer
{
	public partial class frmMain : Office2007RibbonForm, /*IErrorReporter,*/ IVerificationIssueSolver, RibbonBarContentItem
	{
		private ucFunctions __ucFunctions;
		private ucOptions _ucOptions;
		private ucApiExtensions _ucApiExtensions;
		private ucGenerationChoices _ucGenerationChoices;
		private ucProjectDetails _ucProjectDetails;
		private Color CurrentBaseColor = Color.Empty;
		internal string DraggedActionItem;
		public readonly CrossThreadHelper CrossThreadHelper;
		private bool UpdateCheckPerformed;
		private bool UpdatesExist;

		private readonly RibbonBarControllerBase ribbonBarController = new RibbonBarControllerBase();

		// Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), @"Samples\Templates");
		public static readonly string SampleTemplatesFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Branding.FormTitle + Path.DirectorySeparatorChar + "Templates");

		internal ucProjectDetails UcProjectDetails
		{
			get
			{
				if (_ucProjectDetails == null)
				{
					_ucProjectDetails = new ucProjectDetails { Dock = DockStyle.Fill };
					panel1.Controls.Add(_ucProjectDetails);
				}
				return _ucProjectDetails;
			}
		}

		internal ucGenerationChoices UcGenerationChoices
		{
			get
			{
				if (_ucGenerationChoices == null)
				{
					_ucGenerationChoices = new ucGenerationChoices { Dock = DockStyle.Fill };
					panel1.Controls.Add(_ucGenerationChoices);
				}
				return _ucGenerationChoices;
			}
		}

		internal ucOptions UcOptions
		{
			get
			{
				if (_ucOptions == null)
				{
					_ucOptions = new ucOptions { Dock = DockStyle.Fill };
					panel1.Controls.Add(_ucOptions);
				}
				return _ucOptions;
			}
		}

		internal ucApiExtensions UcApiExtensions
		{
			get
			{
				if (_ucApiExtensions == null)
				{
					_ucApiExtensions = new ucApiExtensions { Dock = DockStyle.Fill };
					panel1.Controls.Add(_ucApiExtensions);
				}
				return _ucApiExtensions;
			}
		}

		internal ucFunctions UcFunctions
		{
			get
			{
				if (__ucFunctions == null)
				{
					__ucFunctions = new ucFunctions { Dock = DockStyle.Fill };
					panel1.Controls.Add(__ucFunctions);
				}
				return __ucFunctions;
			}
		}

		public frmMain(string[] args)
		{
			//try
			//{
			CrossThreadHelper = new CrossThreadHelper(this);
			InitializeComponent();

#if !DEBUG
			// Hide these components from real users. Only Slyce should see these components
			//mnuCopyCode.Visible = false;
#endif
			EnableDoubleBuffering();
			PopulateRecentFiles();
			Controller.Instance.MainForm = this;
			Project.Instance.ReferencedAssembliesChanged += Project_ReferencedAssembliesChanged;
			//ErrorReportingService.RegisterErrorReporter(this);

			// When a file is opened through association (right-clicking an STZ file in Windows Explorer), then
			// the path is passed as a commandline parameter, but without quotes. This means that if the path has
			// spaces in it, it appears as separate arguments. We need to check for this before proceeding.
			bool openViaFileAssociation = false;

			if (Environment.CommandLine.IndexOf("\"", 1) > 0)
			{
				string commandLineFile = Environment.CommandLine.Substring(Environment.CommandLine.IndexOf("\"", 1) + 1).Trim(new[] { '"', ' ' });

				if (File.Exists(commandLineFile))
				{
					try
					{
						openViaFileAssociation = true;
						OpenFile(commandLineFile);
					}
					catch (Exception ex)
					{
						MessageBox.Show(string.Format("An error occurred while trying to open the file: {0}.\n\nThe error was:\n{1}", commandLineFile, ex.Message), "Unknown Parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
						Program.HideSplashScreen();
						CreateNewProject();
					}
				}
			}
			if (!openViaFileAssociation)
			{
				if (args.Length == 0)
				{
					if (Settings.Default.AutoLoadLastOpenFile && Controller.Instance.RecentFiles.Length > 0 && File.Exists(Controller.Instance.RecentFiles[0]))
					{
						try
						{
							OpenFile(Controller.Instance.RecentFiles[0]);
						}
						catch (Exception ex)
						{
							Program.HideSplashScreen();
							Settings.Default.AutoLoadLastOpenFile = false;
							MessageBox.Show(string.Format("An error occurred while trying to open your most recent project: {0}. A new blank project will now be created.\n\nThe error was:\n{1}", Controller.Instance.RecentFiles[0], ex.Message), "Unknown Parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
							CreateNewProject();
						}
					}
					else
					{
						CreateNewProject();
					}
				}
				else
				{
					Program.HideSplashScreen();

					foreach (string arg in args)
					{
						if (Slyce.Common.Utility.StringsAreEqual(arg, "/compile", false))
						{
							OpenFile(args[1]);
							//Controller.CommandLine = true;
							//int exitCode = Program.CompileFromCommandLine(args[1], false) ? 0 : 1;
							int exitCode = CompileProject(false, false) ? 0 : 1;
							Environment.Exit(exitCode);
						}
					}
					//MessageBox.Show("ArchAngel Designer was called with unexpected arguments. A filepath was expected: " + args[0], "Unexpected Arguments", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			backgroundWorkerUpdateChecker.RunWorkerAsync();
			//Debugger.Debugger.SpinUpDebugProcess();
			//Activate();
			Program.HideSplashScreen();
			// Force the taskbar button to display. If user sets focus to another program while the splash-screen
			// is displaying, then no button gets added to the taskbar until he alt-tabs to find the app.
			ShowInTaskbar = false;
			ShowInTaskbar = true;
			ribbonControl1.SelectedRibbonTabItem = ribbonTabItemProject;

			// Add the ribbon bar items from the ApiExtensions screen
			ribbonBarController.ProcessRibbonBarButtons(this, ribbonPanelProject);
			// Add the ribbon bar items from the ApiExtensions screen
			ribbonBarController.ProcessRibbonBarButtons(UcApiExtensions, ribbonPanelApiExtensions);

			ribbonBarController.RefreshButtonStatus();
			//}
			//catch (Exception ex)
			//{
			//    Controller.ReportError(ex);
			//}
		}

		void Project_ReferencedAssembliesChanged(object sender, EventArgs e)
		{
			// Need to update the treeview with the new referenced assemblies.
			if (UcApiExtensions != null)
			{
				UcApiExtensions.Populate();
			}
		}

		internal void SwitchToDebugRunningMode()
		{
			mnuDebugStart.Enabled = false;
			mnuDebugStart.Shortcuts.Clear();

			mnuDebugContinue.Enabled = true;
			mnuDebugContinue.Shortcuts.Add(eShortcut.F5);

			mnuDebugStepInto.Enabled = true;
			mnuDebugStepOver.Enabled = true;
			mnuDebugStop.Enabled = true;
		}

		internal void SwitchToDebugStoppedMode()
		{
			mnuDebugStart.Enabled = true;
			mnuDebugStart.Shortcuts.Add(eShortcut.F5);

			mnuDebugContinue.Enabled = false;
			mnuDebugContinue.Shortcuts.Clear();

			mnuDebugStepInto.Enabled = false;
			mnuDebugStepOver.Enabled = false;
			mnuDebugStop.Enabled = false;
		}

		internal void EnableFindNext()
		{
			mnuFindNext.Enabled = true;
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

		internal void PopulateRecentFiles()
		{
			mnuOpen.SubItems.Clear();

			// Clear the existing list
			while (galleryContainer3.SubItems.Count > 1)
			{
				galleryContainer3.SubItems.Remove(galleryContainer3.SubItems.Count - 1);
			}
			if (Controller.Instance.RecentFiles.Length > 0)
			{
				for (int i = 0; i < Controller.Instance.RecentFiles.Length; i++)
				{
					string filePath = Controller.Instance.RecentFiles[i];

					if (File.Exists(filePath))
					{
						string fileName = string.Format("&{1}) {0}", Path.GetFileName(filePath), i + 1);

						// Add to toolbar dropdown
						var menuButton = new ButtonItem
											{
												Text = fileName,
												Tooltip = filePath,
												ButtonStyle = eButtonStyle.TextOnlyAlways
											};
						menuButton.Click += boxRecentFile_Click;
						mnuOpen.SubItems.Add(menuButton);

						// Add to File menu
						var boxButton = new ButtonItem
						{
							Text = fileName,
							Tooltip = filePath,
							ButtonStyle = eButtonStyle.TextOnlyAlways
						};
						boxButton.Click += boxRecentFile_Click;
						galleryContainer3.SubItems.Add(boxButton);
					}
				}
				mnuOpen.Refresh();
			}
			else
			{
				mnuOpen.SubItems.Add(new LabelItem("No recent files"));
			}
		}

		void boxRecentFile_Click(object sender, EventArgs e)
		{
			OpenFile(((ButtonItem)sender).Tooltip);
		}

		private void OpenFile()
		{
			Refresh();

			if (Project.Instance.IsDirty)
			{
				DialogResult result = MessageBox.Show(this, "Save changes?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					Save(false);
					Project.Instance.IsDirty = false;
					Text = Text.Replace(" *", "");
				}
				else if (result == DialogResult.No)
				{
					// The user has said No, so set IsDirty to false so they don't get prompted
					// again in OpenFile(string fileName)
					Project.Instance.IsDirty = false;
				}
				else if (result == DialogResult.Cancel)
				{
					return;
				}
			}
			openFileDialog1.Filter = "ArchAngel Templates (*.aad, *.stz)|*.aad;*.stz";
			//openFileDialog1.Filter += "|Old Slyce template files (*.st)|*.st";
			//openFileDialog1.Filter += "|Compiled ArchAngel templates (*.AAT.DLL)|*.AAT.DLL";
			openFileDialog1.Filter += "|All files (*.*)|*.*";
			openFileDialog1.FileName = "";
			Controller.ShadeMainForm();

			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				Controller.UnshadeMainForm();

				if (!File.Exists(openFileDialog1.FileName))
				{
					MessageBox.Show(this, "File doesn't exist. Please select another file.", "Missing File", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				bool isDirtyValue = Project.Instance.IsDirty;
				Clear();
				Project.Instance.IsDirty = isDirtyValue;
				OpenFile(openFileDialog1.FileName);
				Text = Branding.FormTitle + " - " + Path.GetFileName(openFileDialog1.FileName);
				Project.Instance.IsDirty = false;
				Populate();
			}
			Controller.UnshadeMainForm();
		}

		private void Clear()
		{
			Project.Instance.Clear();
			Text = Branding.FormTitle;

			if (UcGenerationChoices != null)
			{
				UcGenerationChoices.Clear();
			}
			if (UcProjectDetails != null)
			{
				UcProjectDetails.Clear();
			}
			if (UcOptions != null)
			{
				UcOptions.Clear();
			}
			if (UcApiExtensions != null)
			{
				UcApiExtensions.Clear();
			}
			if (UcFunctions != null)
			{
				UcFunctions.Clear();
			}
		}

		private void OpenFile(string fileName)
		{
			if (!File.Exists(fileName))
			{
				MessageBox.Show(this, "This file is missing: " + Environment.NewLine + fileName, "File not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (Project.Instance.IsDirty)
			{
				DialogResult result = MessageBox.Show(this, "Save changes?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					Save(false);
				}
				else if (result == DialogResult.Cancel)
				{
					return;
				}
				Project.Instance.IsDirty = false;
			}
			try
			{
				Cursor = Cursors.WaitCursor;

				if (File.Exists(Project.Instance.ProjectFileName))
				{
					Controller.Instance.AddRecentFile(Project.Instance.ProjectFileName);
				}
				Clear();

				SharedData.AddAssemblySearchPath(Path.GetDirectoryName(fileName));
				Project.Instance.ProjectFileName = fileName;
				Project.Instance.CompileFolderName = "";
				Project.Instance.Open(fileName);
				Controller.Instance.AddRecentFile(fileName);

				if (Project.Instance.ProjType == ProjectTypes.AddIn)
				{
					Project.Instance.ProjectFileName = fileName;
					Text = Branding.FormTitle + " - " + Path.GetFileName(openFileDialog1.FileName);
					Project.Instance.IsDirty = false;
				}
				else // Template project
				{
					Text = Branding.FormTitle + " - " + Path.GetFileName(fileName);
					Project.Instance.IsDirty = false;
					AddRecentFile(fileName);
				}
				RefreshScreens();
			}
			finally
			{
				Populate();
				//mnuSyncTemplateTool.Enabled = true;
				Cursor = Cursors.Default;
			}
		}

		private void RefreshScreens()
		{
			if (UcGenerationChoices != null) { UcGenerationChoices.Populate(); }
			if (UcProjectDetails != null) { UcProjectDetails.Populate(); }
			if (UcOptions != null) { UcOptions.Populate(); }
			if (UcApiExtensions != null) { UcApiExtensions.Populate(); }

			UcProjectDetails.Populate();
			ribbonControl1.SelectedRibbonTabItem = ribbonTabItemProject;
		}

		private static void AddRecentFile(string filepath)
		{
			if (string.IsNullOrEmpty(filepath))
				return;

			filepath = filepath.Trim();

			if (!string.IsNullOrEmpty(filepath) &&
				File.Exists(filepath))
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
				int num = Controller.Instance.RecentFiles.Length + 1 - numMissingFiles;

				if (num > 0)
				{
					string[] newRecentFiles = new string[num];
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
				else
				{
					Controller.Instance.RecentFiles = new string[0];
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="forceFileSelection">If true forces user to select new filename (for Save As)</param>
		private bool Save(bool forceFileSelection)
		{
			if (Controller.BusySaving)
			{
				return false;
			}
			Cursor = Cursors.WaitCursor;
			Controller.BusySaving = true;
			bool mustSave = false;

			if (UcProjectDetails != null)
			{
				UcProjectDetails.Save();
			}
			if (Project.Instance.ProjectFileName.Length == 0)
			{
				Cursor = Cursors.Default;
				MessageBox.Show(this, "Please set the project name and output location on the Template Details screen before saving.", "Missing Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Controller.Instance.MainForm.HidePanelControls(UcProjectDetails);
				Controller.BusySaving = false;
				return false;
			}
			try
			{
				if (forceFileSelection)
				{
					Project.Instance.ProjectFileName = "";
				}
				if (forceFileSelection || Project.Instance.ProjectFileName.Length == 0 ||
					 !File.Exists(Project.Instance.ProjectFileName))
				{
					switch (Project.Instance.ProjType)
					{
						case ProjectTypes.AddIn:
							saveFileDialog1.AddExtension = false;
							break;
						case ProjectTypes.Template:
							saveFileDialog1.AddExtension = true;

							if (Project.Instance.ProjectFileName.Length > 0)
							{
								saveFileDialog1.FileName = Project.Instance.ProjectFileName;

								if (Directory.Exists(Path.GetDirectoryName(Project.Instance.ProjectFileName)))
								{
									saveFileDialog1.InitialDirectory = Path.GetDirectoryName(Project.Instance.ProjectFileName);
								}
							}
							saveFileDialog1.DefaultExt = ".aad";
							saveFileDialog1.Filter = "ArchAngel Template Files (*.aad)|*.aad";
							saveFileDialog1.Filter += "|All files (*.*)|*.*";
							break;
						default:
							throw new Exception("Not coded yet");
					}
					Controller.ShadeMainForm();

					if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
					{
						if (Path.GetDirectoryName(saveFileDialog1.FileName) == SampleTemplatesFolderPath)
						{
							Cursor = Cursors.Default;
							MessageBox.Show(this, "You can't save to the Samples folder, because these files get overwritten when installing a new version.", "Can't Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							Controller.Instance.MainForm.HidePanelControls(UcProjectDetails);
							Controller.BusySaving = false;
							return false;
						}
						mustSave = true;
						Project.Instance.ProjectFileName = saveFileDialog1.FileName;
					}
					Controller.UnshadeMainForm();
				}
				else
				{
					mustSave = true;
				}
				if (mustSave)
				{
					if (Path.GetDirectoryName(Project.Instance.ProjectFileName) == SampleTemplatesFolderPath)
					{
						Cursor = Cursors.Default;
						MessageBox.Show(this, "You can't save to the Samples folder, because these files get overwritten when installing a new version.", "Can't Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						Controller.Instance.MainForm.HidePanelControls(UcProjectDetails);
						Controller.BusySaving = false;
						return false;
					}
					if (Project.Instance.ProjectFileName.Length > 0
						&& Project.Instance.ProjType == ProjectTypes.AddIn)
					{
						FileStream fs = new FileStream(Project.Instance.ProjectFileName, FileMode.OpenOrCreate,
													   FileAccess.Write);
						StreamWriter sw = new StreamWriter(fs);
						//TODO:sw.Write(syntaxEditor2.Text);
						sw.Close();
						fs.Close();
					}
					if (UcFunctions != null)
					{
						UcFunctions.SaveAllFunctions(false);
					}
					if (UcOptions != null)
					{
						UcOptions.OnSave();
					}
					if (UcApiExtensions != null)
					{
						UcApiExtensions.OnSave();
					}
					if (File.Exists(Project.Instance.ProjectFileName) && (File.GetAttributes(Project.Instance.ProjectFileName) & FileAttributes.ReadOnly) != 0)
					{
						MessageBox.Show(this, "Cannot save because project file is readonly: " + Project.Instance.ProjectFileName, "Cannot Save - ReadOnly", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return false;
					}
					Project.Instance.SaveToXml(Project.Instance.ProjectFileName);
					Project.Instance.IsDirty = false;
					Text = Branding.FormTitle + " - " + Path.GetFileName(Project.Instance.ProjectFileName);
					AddRecentFile(Project.Instance.ProjectFileName);
					Controller.BusySaving = false;
					//mnuSyncTemplateTool.Enabled = true;
					return true;
				}
				Controller.BusySaving = false;
				//mnuSyncTemplateTool.Enabled = true;
				return false;
			}
			//catch (Exception ex)
			//{
			//    Controller.BusySaving = false;
			//    Controller.ReportError(ex);
			//    return false;
			//}
			finally
			{
				Controller.Instance.AddRecentFile(Project.Instance.ProjectFileName);
				Controller.BusySaving = false;
				Cursor = Cursors.Default;
			}
		}

		public void Populate()
		{
			toolTipForNavBar.RemoveAll();

			UcFunctions.PopulateFunctionList();

			Project.Instance.IsDirty = false;
		}

		private void ShowProjectDetails()
		{
			UcProjectDetails.Populate();
			HidePanelControls(UcProjectDetails);
		}

		internal bool ShowFunction(FunctionInfo function, Control callingControl)
		{
			return ShowFunction(function, true, callingControl);
		}

		internal bool ShowFunction(FunctionInfo function, bool allowEdit, Control callingControl)
		{
			HidePanelControls(UcFunctions);
			return UcFunctions.ShowFunction(function, allowEdit, callingControl);
		}

		//internal bool ShowFunction(UserOption userOption, UserOption.FunctionTypes functionType, bool allowEdit, Control callingControl)
		//{
		//    HidePanelControls(UcFunctions);
		//    UcFunctions.ShowFunction(userOption, functionType, allowEdit, callingControl);
		//    return true;
		//}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Default.Save();
			SyntaxEditorHelper.DeleteResources();

			if (Project.Instance.IsDirty)
			{
				Focus();
				DialogResult result = MessageBox.Show(this, "Save changes?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					// Don't exit if there was a problem saving
					if (!Save(false))
					{
						e.Cancel = true;
					}
				}
				else if (result == DialogResult.Cancel)
				{
					e.Cancel = true;
					return;
				}
			}
			// Delete temp folder
			if (Directory.Exists(Controller.TempPath))
			{
				try
				{
					Directory.Delete(Controller.TempPath, true);
				}
				catch
				{
					// Do nothing - sometimes some dev process is accessing the directory
				}
			}
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

		internal void HidePanelControls(Control ctlToKeep)
		{
			try
			{
				SetCursor(Cursors.WaitCursor);
				Utility.SuspendPainting(this);

				for (int i = 0; i < panel1.Controls.Count; i++)
				{
					if (panel1.Controls[i] != ctlToKeep)
					{
						CrossThreadHelper.SetVisibility(panel1.Controls[i], false);
					}
					else
					{
						CrossThreadHelper.SetVisibility(panel1.Controls[i], true);
						panel1.Controls[i].Refresh();
					}
				}
				ShowMenuBarFor(ctlToKeep);
			}
			finally
			{
				SetCursor(Cursors.Default);
				Utility.ResumePainting(this);
			}
		}

		internal bool CompileProject(bool debugVersion, bool showSuccessMessage)
		{
			if (Project.Instance.Functions.Count == 0)
			{
				MessageBox.Show(this, "Build cancelled because this project has no functions.", "Build cancelled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			List<string> projectIssues = Project.Instance.VerifyProjectCorrectness();

			if (projectIssues.Count > 0)
			{
				StringBuilder sb = new StringBuilder();
				foreach (string val in projectIssues)
				{
					sb.AppendLine(val);
				}
				MessageBox.Show(string.Format("Project is in an invalid state. Problems:\n{0}", sb), "Problems exist", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			try
			{
				Controller.BusyCompiling = true;
				if (!InvokeRequired)
				{
					Cursor = Cursors.WaitCursor;
				}
				if (UcFunctions != null)
				{
					UcFunctions.ClearErrors();
				}
				if (Project.Instance.ProjType == ProjectTypes.Template)
				{
					Settings.Default.CodeFile = Path.GetTempFileName();
				}
				Invalidate();

				if (!InvokeRequired)
				{
					Refresh();
				}
				bool successful = false;

				try
				{
					successful = CompileHelper.Compile(debugVersion);
				}
				catch (IOException ex)
				{
					if (ex.Message.IndexOf("being used by another process") > 0)
					{
						MessageBox.Show(this, "File is being used by another program, probably ArchAngel Workbench. Close ArchAngel Workbench then try again.", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
				}
				catch (MissingMemberException ex)
				{
					// UserOption not found
					if (ex.Message.IndexOf("UserOption") >= 0)
					{
						MessageBox.Show(this, ex.Message, "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
				}
				if (successful)
				{
					if (!debugVersion && showSuccessMessage)
					{
						string filePath = Project.Instance.GetCompiledDLLPath();

						MessageBox.Show(this, "ArchAngel template created successfully: \n\n" + filePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					return true;
				}
				else
				{
					if (!debugVersion)
					{
						MessageBox.Show(this, "Errors exist. See error listing at bottom of Functions page.", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					return false;
				}
			}
			//catch (FileNotFoundException ex)
			//{
			//    Controller.ReportError(ex);
			//    return false;
			//}
			//catch (Exception ex)
			//{
			//    Controller.ReportError(ex);
			//    return false;
			//}
			finally
			{
				Controller.BusyCompiling = false;
				if (!InvokeRequired)
				{
					Cursor = Cursors.Default;
				}
			}
		}

		private void CreateNewProject()
		{
			if (Project.Instance.IsDirty)
			{
				DialogResult result = MessageBox.Show(this, "Save changes?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					Save(false);
					Project.Instance.IsDirty = false;
					Text = Text.Replace(" *", "");
				}
				else if (result == DialogResult.Cancel)
				{
					return;
				}
			}
			Cursor = Cursors.WaitCursor;
			Clear();
			Text = Branding.FormTitle + " - New Template";
			Project.Instance.IsDirty = false;
			Project.Instance.SetupDefaults();
			ShowProjectDetails();
			UcProjectDetails.ShowNew();
			UcGenerationChoices.ShowNew();
			UcOptions.Populate();
			UcApiExtensions.Populate();
			Project.Instance.IsDirty = false;
			Populate();
			HidePanelControls(UcProjectDetails);
			Cursor = Cursors.Default;
		}

		private void frmMain_Paint(object sender, PaintEventArgs e)
		{
			if (CurrentBaseColor != Colors.BaseColor)
			{
				CurrentBaseColor = Colors.BaseColor;
				BackColor = Colors.BackgroundColor;
				ToolStripManager.Renderer = new ToolStripProfessionalRenderer(new CustomProfessionalColors());
			}
		}

		#region Inner Classes
		/// <summary>
		/// This class defines the gradient colors for 
		/// the MenuStrip and the ToolStrip.
		/// </summary>
		class CustomProfessionalColors : ProfessionalColorTable
		{
			public override Color ToolStripGradientBegin
			{ get { return Colors.GetBaseColorVariant(0.95); } }// Color.BlueViolet; } }

			public override Color ToolStripGradientMiddle
			{ get { return Colors.GetBaseColorVariant(0.85); } }// Color.CadetBlue; } }

			public override Color ToolStripGradientEnd
			{ get { return Colors.GetBaseColorVariant(0.75); } }

			public override Color MenuStripGradientBegin
			{ get { return Colors.GetBaseColorVariant(0.85); } }

			public override Color MenuStripGradientEnd
			{ get { return Colors.GetBaseColorVariant(0.85); } }

			public override Color ImageMarginGradientBegin
			{ get { return Colors.GetBaseColorVariant(0.95); } }

			public override Color ImageMarginGradientMiddle
			{ get { return Colors.GetBaseColorVariant(0.85); } }

			public override Color ImageMarginGradientEnd
			{ get { return Colors.GetBaseColorVariant(0.75); } }

			//public override Color MenuItemSelected
			//{ get { return Slyce.Common.Colors.GetBaseColorVariant(0.75); } }

			public override Color MenuItemSelected
			{ get { return Colors.GetBaseColorVariant(0.85); } }

		}
		#endregion

		private void frmMain_Activated(object sender, EventArgs e)
		{
			Refresh();
		}

		//public void ReportError(ErrorLevel level, string message)
		//{
		//    if (InvokeRequired)
		//    {
		//        MethodInvoker mi = () => ReportError(level, message);
		//        Invoke(mi);
		//        return;
		//    }
		//    if (level == ErrorLevel.Error)
		//        MessageBox.Show(this, message, "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//}

		//public void ReportUnhandledException(Exception e)
		//{
		//    Controller.ReportError(e);
		//}

		public string GetValidTemplateFilePath(string projectFile, string oldTemplateFile)
		{
			return CompileHelper.CompiledAssemblyFileName;
		}

		public string GetValidProjectDirectory(string projectFile, string oldOutputFolder)
		{
			return Project.Instance.TestGenerateDirectory;
		}

		public void InformUserThatAppConfigIsInvalid(string message)
		{
			MessageBox.Show(this, message, "Unrecoverable Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void backgroundWorkerUpdateChecker_DoWork(object sender, DoWorkEventArgs e)
		{
			AutoCheckForUpdates();
		}

		private void AutoCheckForUpdates()
		{
			if (!UpdateCheckPerformed)
			{
				UpdateCheckPerformed = true;
				Slyce.Common.Updates.frmUpdate.SilentMode = true;
				// TODO: Implement Branding like Workbench, so that we can swap between ArchANgel builds and VisualNHibernate builds
				//UpdatesExist = Slyce.Common.Updates.frmUpdate.UpdateExists(Branding.ProductBranding);
				UpdatesExist = Slyce.Common.Updates.frmUpdate.UpdateExists("ArchAngel");
				Slyce.Common.Updates.frmUpdate.SilentMode = false;
			}
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
				var form = new Slyce.Common.Updates.frmUpdate();
				form.ShowDialog(this);
			}
		}

		private static void IncreaseEditorFontSize()
		{
			double oldValue = Settings.Default.EditorFontSize;
			double newValue = oldValue + 1;
			Settings.Default.EditorFontSize = newValue;
			//Controller.Instance.RaiseSettingChangedEvent("EditorFontSize", oldValue, newValue);
		}

		private static void DecreaseEditorFontSize()
		{
			if (Settings.Default.EditorFontSize < 2)
			{
				return;
			}
			double oldValue = Settings.Default.EditorFontSize;
			double newValue = oldValue - 1;
			Settings.Default.EditorFontSize = newValue;
			//Controller.Instance.RaiseSettingChangedEvent("EditorFontSize", oldValue, newValue);
		}

		private void mnuBuild_Click(object sender, EventArgs e)
		{
			CompileProject(false, true);
		}

		private void mnuBuildTop_Click(object sender, EventArgs e)
		{
			CompileProject(false, true);
		}

		private void buttonItem53_Click(object sender, EventArgs e)
		{
			CompileProject(false, true);
		}

		private void mnuNew_Click(object sender, EventArgs e)
		{
			CreateNewProject();
		}

		private void mnuDebugStart_Click(object sender, EventArgs e)
		{
			//CompileProject(true, true);
			StartDebugging();
		}

		private void StartDebugging()
		{
			Cursor = Cursors.WaitCursor;
			try
			{
				ucFunction functionScreen = UcFunctions.GetCurrentlyDisplayedFunctionPage();
				if (functionScreen != null)
				{
					functionScreen.StartDebugger();
				}
			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			Save(false);
		}

		private void mnuOpen_Click(object sender, EventArgs e)
		{
			OpenFile();
		}

		private void mnuBoxSaveAs_Click(object sender, EventArgs e)
		{
			Save(true);
		}

		private void mnuHelp_Click(object sender, EventArgs e)
		{
			ShowHelp();
		}

		private void ShowHelp()
		{
			Cursor = Cursors.WaitCursor;
			Refresh();
			string helpfile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ArchAngel Help.chm");

			if (File.Exists(helpfile))
			{
				System.Diagnostics.Process.Start(helpfile);
			}
			else
			{
				MessageBox.Show(this, "The ArchAngel help file is missing. Please repair the ArchAngel installation via Control Panel -> Add/Remove Programs.", "Helpfile Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			Cursor = Cursors.Default;
		}

		private void CheckForUpdates()
		{
			Controller.ShadeMainForm();
			var form = new Slyce.Common.Updates.frmUpdate();
			form.ShowDialog(this);
			Controller.UnshadeMainForm();
		}

		private void mnuUpdateCheck_Click(object sender, EventArgs e)
		{
			CheckForUpdates();
		}

		private void ShowLicenseDetails()
		{
			Cursor = Cursors.WaitCursor;
			Refresh();
			Controller.ShadeMainForm();
			var form = new Licensing.frmStatus("Visual NHibernate License.SlyceLicense");
			form.ShowDialog(this);
			Controller.UnshadeMainForm();
			Cursor = Cursors.Default;
			Activate();
		}

		private void mnuLicense_Click(object sender, EventArgs e)
		{
			ShowLicenseDetails();
		}

		private void ShowAbout()
		{
			Cursor = Cursors.WaitCursor;
			Refresh();
			var form = new frmAbout();
			form.ShowDialog(this);
			Cursor = Cursors.Default;
		}

		private void mnuAbout_Click(object sender, EventArgs e)
		{
			ShowAbout();
		}

		//private static void DebuggerStop()
		//{
		//    Controller.Instance.TriggerNextDebugAction(Debugger.DebugActionType.Stop);
		//}

		private void mnuDebugStop_Click(object sender, EventArgs e)
		{
			//DebuggerStop();
		}

		//private static void DebuggerContinue()
		//{
		//    Controller.Instance.TriggerNextDebugAction(Debugger.DebugActionType.Continue);
		//}

		private void mnuDebugContinue_Click(object sender, EventArgs e)
		{
			//DebuggerContinue();
		}

		//private static void DebuggerStepInto()
		//{
		//    Controller.Instance.TriggerNextDebugAction(Debugger.DebugActionType.StepInto);
		//}

		private void mnuDebugStepInto_Click(object sender, EventArgs e)
		{
			//DebuggerStepInto();
		}

		//private static void DebuggerStepOver()
		//{
		//    Controller.Instance.TriggerNextDebugAction(Debugger.DebugActionType.StepOver);
		//}

		private void mnuDebugStepOver_Click(object sender, EventArgs e)
		{
			//DebuggerStepOver();
		}

		//private void DebuggerToggleBreakpoint()
		//{
		//    ucFunction functionScreen = UcFunctions.GetCurrentlyDisplayedFunctionPage();
		//    if (functionScreen != null)
		//    {
		//        functionScreen.ToggleBreakpointOnCurrentLine();
		//    }
		//}

		private void mnuToggleBreakpoint_Click(object sender, EventArgs e)
		{
			//DebuggerToggleBreakpoint();
		}

		//private static void DebuggerRestartProcess()
		//{
		//    Debugger.Debugger.RestartDebugProcess();
		//}

		private void mnuDebugRestart_Click(object sender, EventArgs e)
		{
			//DebuggerRestartProcess();
		}

		private void TestGenerateProject()
		{
			// Check whether the debug project exists.
			if (Controller.Instance.CheckDebugOptions(true) == false)
				return;

			Cursor = Cursors.WaitCursor;
			try
			{
				using (var worker = new BackgroundWorker())
				{
					worker.DoWork += delegate
					{
						var project = new WorkbenchProject();
						SharedData.CurrentProject = project;

						Utility.DisplayMessagePanel(this, "Generating Files", MessagePanel.ImageType.Hourglass);
						Utility.UpdateMessagePanelStatus(this, "Compiling Project");

						// Compile the template to the new temporary location.
						bool compileResult = FunctionRunner.CreateTemplateFile(false, false);

						if (compileResult == false)
						{
							//ErrorReportingService.ReportError(ErrorLevel.Error, "Could not compile project");
							throw new Exception("Could not compile project");
							//return;
						}

						Utility.UpdateMessagePanelStatus(this, "Loading Project File");

						// Load the project
						bool loadResult = project.Load(Project.Instance.DebugProjectFile, this, false, CompileHelper.CompiledAssemblyFileName);

						if (loadResult == false)
						{
							//ErrorReportingService.ReportError(ErrorLevel.Error, "Could not load project file. Open in ArchAngel Workbench for more information.");
							throw new Exception("Could not load project file. Open in ArchAngel Workbench for more information.");
							//return;
						}

						var helper = new GenerationHelper(new NullTaskProgressHelper<GenerateFilesProgress>(),
										 project.TemplateLoader, project,
										 new FileController());
						Utility.UpdateMessagePanelStatus(this, "Writing Files");
						project.PerformPreAnalysisActions();
						helper.GenerateAllFiles("", project.CombinedOutput.RootFolder, null, null,
							Project.Instance.TestGenerateDirectory);
					};

					worker.RunWorkerCompleted += (sndr, evnt) => Utility.HideMessagePanel(this);

					worker.RunWorkerAsync();
				}
			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		private void mnuTestGenerateProject_Click(object sender, EventArgs e)
		{
			TestGenerateProject();
		}

		private void ShowNewProviderTool()
		{
			Refresh();
			var form = new frmNewProvider();
			form.ShowDialog(this);
		}

		private void mnuNewProviderTool_Click(object sender, EventArgs e)
		{
			ShowNewProviderTool();
		}

		//private void ShowSyncTemplateTool()
		//{
		//    Refresh();
		//    var form = new Wizards.TemplateSync.frmTemplateSyncWizard();
		//    form.ShowDialog();
		//    this.Activate();
		//}

		//private void mnuSyncTemplateTool_Click(object sender, EventArgs e)
		//{
		//    ShowSyncTemplateTool();
		//}

		private void ShowExtractTemplateTool()
		{
			Refresh();
			var form = new frmExtractTemplate();
			form.ShowDialog(this);

			if (!string.IsNullOrEmpty(form.FileName))
			{
				OpenFile(form.FileName);
			}
		}

		private void mnuExtractTemplateTool_Click(object sender, EventArgs e)
		{
			ShowExtractTemplateTool();
		}

		private void ShowCleanupTool()
		{
			Cursor = Cursors.WaitCursor;
			Refresh();
			var form = new frmCleanUp();
			form.ShowDialog();
			Cursor = Cursors.Default;
			Activate();
		}

		private void mnuCleanupTool_Click(object sender, EventArgs e)
		{
			ShowCleanupTool();
		}

		private void ShowProviderUtilityTool()
		{
			string file = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ArchAngel.ProviderUtility.exe");

			if (!File.Exists(file))
			{
				MessageBox.Show(this, "ArchAngel.ProviderUtility.exe is missing from the ArchAngel installation folder. Repair ArchAngel via Control Panel -> Add/Remove Programs", "Missing File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			System.Diagnostics.Process.Start(file);
		}

		private void ShowOptions()
		{
			new frmOptions().ShowDialog(this);
		}

		private void mnuOptions_Click(object sender, EventArgs e)
		{
			ShowOptions();
		}

		private void mnuFontIncrease_Click(object sender, EventArgs e)
		{
			IncreaseEditorFontSize();
		}

		private void mnuFontDecrease_Click(object sender, EventArgs e)
		{
			DecreaseEditorFontSize();
		}

		private void mnuFind_Click(object sender, EventArgs e)
		{
			ShowFind();
		}

		private static void ShowFind()
		{
			Controller.Instance.ShowFindForm(false);
		}

		private static void FindNext()
		{
			SearchHelper.Search();
		}

		private static void ShowReplace()
		{
			Controller.Instance.ShowFindForm(true);
		}

		private void mnuReplace_Click(object sender, EventArgs e)
		{
			ShowReplace();
		}

		private void mnuFindNext_Click(object sender, EventArgs e)
		{
			FindNext();
		}

		private void CreateNewFunction()
		{
			// Don't execute if there is no project
			if (Project.Instance != null)
			{
				HidePanelControls(UcFunctions);
				UcFunctions.NewFunction();
			}
		}

		private void mnuNewFunction_Click(object sender, EventArgs e)
		{
			CreateNewFunction();
		}

		private static void CopyCodeToClipboard()
		{
			if (Controller.Instance.ParsedCode.Length > 0)
			{
				Clipboard.SetText(Controller.Instance.ParsedCode);
			}
			else
			{
				Clipboard.SetText(" ");
			}
		}

		private void SwitchFormatting()
		{
			if (panel1.Controls["ucFunctions"] != null)
			{
				((ucFunctions)panel1.Controls["ucFunctions"]).SwitchFormatting();
			}
		}

		private void mnuSwitchHighlighting_Click(object sender, EventArgs e)
		{
			SwitchFormatting();
		}

		public ButtonItem MenuItemDebug
		{
			get { return mnuDebugStart; }
		}

		private void mnuCopyCode_Click(object sender, EventArgs e)
		{
			CopyCodeToClipboard();
		}

		private void mnuBoxExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void buttonItem17_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void frmMain_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.F6)
			{
				CompileProject(false, true);
			}

			if (e.Control && KeyboardHelper.IsMinusKeyDown(e.KeyData))
			{
				DecreaseEditorFontSize();
			}

			if (e.Control && KeyboardHelper.IsAddKeyDown(e.KeyData))
			{
				IncreaseEditorFontSize();
			}

			if (e.Control && KeyboardHelper.IsTabKeyDown(e.KeyData))
			{
				List<RibbonTabItem> tabs = ribbonControl1.Items.OfType<RibbonTabItem>().ToList();
				int selectedIndex = tabs.IndexOf(ribbonControl1.SelectedRibbonTabItem);

				if (e.Shift)
				{
					// CTRL+SHIFT+TAB so move backwards
					if (selectedIndex > 0)
						ribbonControl1.SelectedRibbonTabItem = tabs[selectedIndex - 1];
					else if (tabs.Count > 0)
						ribbonControl1.SelectedRibbonTabItem = tabs[tabs.Count - 1];
				}
				else
				{
					// CTRL+TAB so move forwards
					if (selectedIndex < tabs.Count - 1)
						ribbonControl1.SelectedRibbonTabItem = tabs[selectedIndex + 1];
					else if (tabs.Count > 0)
						ribbonControl1.SelectedRibbonTabItem = tabs[0];
				}
			}
		}

		private void mnuBoxNew_Click(object sender, EventArgs e)
		{
			CreateNewProject();
		}

		private void mnuBoxOpen_Click(object sender, EventArgs e)
		{
			OpenFile();
		}

		private void mnuBoxSave_Click(object sender, EventArgs e)
		{
			Save(false);
		}

		private void buttonShowOptions_Click(object sender, EventArgs e)
		{
			ShowOptions();
		}

		private void ribbonControl1_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			if (ribbonControl1.SelectedRibbonTabItem == ribbonTabItemProject)
				HidePanelControls(UcProjectDetails);
			else if (ribbonControl1.SelectedRibbonTabItem == ribbonTabItemFileLayouts)
				HidePanelControls(UcGenerationChoices);
			else if (ribbonControl1.SelectedRibbonTabItem == ribbonTabItemUserOptions)
				HidePanelControls(UcOptions);
			else if (ribbonControl1.SelectedRibbonTabItem == ribbonTabItemApiExtensions)
				HidePanelControls(UcApiExtensions);
			else if (ribbonControl1.SelectedRibbonTabItem == ribbonTabItemFunctions)
				HidePanelControls(UcFunctions);
		}
		private void buttonAddNewFile_Click(object sender, EventArgs e)
		{
			_ucGenerationChoices.AddNewFile();
		}

		private void mnuUserOptionNew_Click(object sender, EventArgs e)
		{
			UcOptions.CreateNewUserOption();
		}

		private void buttonAddNewFolder_Click(object sender, EventArgs e)
		{
			_ucGenerationChoices.AddNewFolder();
		}

		private void mnuDeleteUserOption_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException("Have not implemented deletion of user options");
		}

		private void mnuSaveTop_Click(object sender, EventArgs e)
		{
			buttonSave_Click(sender, e);
		}

		private void buttonOptionsRefresh_Click(object sender, EventArgs e)
		{
			_ucOptions.Populate();
		}

		public void AddRibbonBarButtons(RibbonBarBuilder builder)
		{
		}

		public void ShowMenuBarFor(Control page)
		{
			if (page == UcFunctions)
				ribbonTabItemFunctions.Select();
			else if (page == UcProjectDetails)
				ribbonTabItemProject.Select();
			else if (page == UcApiExtensions)
				ribbonTabItemApiExtensions.Select();
			else if (page == UcGenerationChoices)
				ribbonTabItemFileLayouts.Select();
		}

		private void mnuDeleteFunction_Click(object sender, EventArgs e)
		{
			DeleteCurrentFunction();
		}

		private void DeleteCurrentFunction()
		{
			if (UcFunctions.GetCurrentlyDisplayedFunctionPage() == null)
			{
				MessageBox.Show(this, "No function selected", "Invalid operation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			FunctionInfo function = UcFunctions.GetCurrentlyDisplayedFunctionPage().CurrentFunction;

			if (function == null)
			{
				MessageBox.Show(this, "No function selected", "Invalid operation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (MessageBox.Show(this, string.Format("Delete this function? [{0}]", function.Name), "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Project.Instance.DeleteFunction(UcFunctions.GetCurrentlyDisplayedFunctionPage().CurrentFunction);
			}
		}
	}
}


