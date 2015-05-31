using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ArchAngel.Common;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Wizards.NewProject;
using log4net;

namespace ArchAngel.Workbench.Wizards.NewProject
{
	public partial class frmNewProject : Form, IFormNewProject
	{
		public NewProjectFormActions SetupAction { get; set; }
		public NewProjectFormActions UserChosenAction { get; set; }
		public string ExistingProjectPath { get; set; }
		public string NewProjectTemplate { get; set; }
		public string NewProjectName { get; set; }
		public string NewProjectFolder { get; set; }
		public string NewProjectOutputPath { get; set; }
		private INewProjectInformation _NewProjectInformation;
		public string TemplateName { get; set; }
		public bool UserClickedFinish = false;

		private INewProjectScreen currentScreen;
		private readonly List<INewProjectScreen> wizardScreens = new List<INewProjectScreen>();
		private static readonly ILog log = LogManager.GetLogger(typeof(frmNewProject));
		private bool HasBeenSetUp;

		private readonly Dictionary<string, object> ScreenData = new Dictionary<string, object>();

		public frmNewProject()
		{
			InitializeComponent();
			//BackColor = Slyce.Common.Colors.BackgroundColor;

			UserChosenAction = NewProjectFormActions.None;
		}

		public INewProjectInformation NewProjectInformation
		{
			get { return _NewProjectInformation; }
			set { _NewProjectInformation = value; }
		}

		public void Setup(NewProjectFormActions action)
		{
			ClearScreens();

			SetupAction = action;

			if (Branding.ProductBranding == ApplicationBrand.VisualNHibernate)
			{

//#if DEBUG
//				string templateFile = Slyce.Common.RelativePaths.RelativeToAbsolutePath(Path.GetDirectoryName(Application.ExecutablePath), @"..\..\..\ArchAngel.Templates\NHibernate\Template\NHibernate.AAT.DLL");
//#else
//				//string templateFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Branding.ProductName + Path.DirectorySeparatorChar + "Templates" + Path.DirectorySeparatorChar + "NHibernate.AAT.DLL");
				string templateFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "NHibernate.AAT.DLL");
//#endif
				NewProjectTemplate = templateFile;
				switch (SetupAction)
				{
					case NewProjectFormActions.None:
					case NewProjectFormActions.ExistingProject:
						AddScreen(new Screen1());
						break;
					default:
						break;
				}
				LoadScreensFromTemplate(NewProjectTemplate);
			}
			else
			{

				switch (SetupAction)
				{
					case NewProjectFormActions.None:
					case NewProjectFormActions.ExistingProject:
						AddScreen(new Screen1());
						AddScreen(new Screen2());
						break;
					case NewProjectFormActions.NewProject:
						AddScreen(new Screen2());
						break;
					default:
						throw new NotImplementedException("Not handled yet: " + SetupAction);
				}
			}

			HasBeenSetUp = true;
		}

		public void ShowForm(IWin32Window owner, bool appIsStarting)
		{
			if (HasBeenSetUp == false)
				Setup(SetupAction);

			AppIsStarting = appIsStarting;
			Controller.Instance.ShadeMainForm();

			switch (SetupAction)
			{
				case NewProjectFormActions.NewProject:
					LoadScreen("LoadExistingProject");
					break;
				default:
					LoadScreen(typeof(Screen1));
					break;
			}
			ShowDialog(owner);
		}

		internal bool ApplicationExiting = false;
		private bool _CloseCausedByLoadingNextScreen = false;

		public bool CloseCausedByLoadingNextScreen
		{
			get { return _CloseCausedByLoadingNextScreen; }
			set { _CloseCausedByLoadingNextScreen = value; }
		}

		private bool AppIsStarting = true;

		private void frmNewProject_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (CloseCausedByLoadingNextScreen || !AppIsStarting)
				return;

			if (!ApplicationExiting &&
				MessageBox.Show(this, "Close Visual NHibernate?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				e.Cancel = true;
				return;
			}
			ApplicationExiting = true;
			Controller.Instance.UnshadeMainForm();
			Application.Exit();
		}

		public void ClearScreens()
		{
			wizardScreens.Clear();
			HasBeenSetUp = false;
		}

		public void AddScreen(INewProjectScreen screen)
		{
			HasBeenSetUp = true;
			wizardScreens.Add(screen);
		}

		public void AddScreens(IEnumerable<INewProjectScreen> screens)
		{
			HasBeenSetUp = true;
			foreach (var screen in screens)
			{
				wizardScreens.Add(screen);
			}
		}

		public void SkipScreens(int numberOfScreensToSkip)
		{
			NumberOfScreensToSkip += numberOfScreensToSkip;
		}

		public void ClearScreensToSkip()
		{
			NumberOfScreensToSkip = 0;
		}

		public int NumberOfScreensToSkip { get; set; }

		public void SetScreenData(string key, object data)
		{
			ScreenData[key] = data;
		}

		public object GetScreenData(string key)
		{
			return ScreenData.ContainsKey(key) ? ScreenData[key] : null;
		}

		public void LoadScreen(string screenTypeName)
		{
			LoadScreen(wizardScreens.SingleOrDefault(s => s.GetType().Name == screenTypeName).GetType());
		}

		public void LoadScreen(Type screenType)
		{
			currentScreen = wizardScreens.SingleOrDefault(s => s.GetType() == screenType);
			SetupCurrentScreen();
		}

		public void Finish()
		{
			UserClickedFinish = true;
			CloseCausedByLoadingNextScreen = true;
			Close();
			CloseCausedByLoadingNextScreen = false;
			return;
		}

		private void LoadScreensFromTemplate(string template)
		{
			if (File.Exists(template) == false)
			{
#if DEBUG
                string path = Slyce.Common.RelativePaths.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location, @"..\..\..\..\ArchAngel.Templates\NHibernate\Template\NHibernate.AAT.DLL");

                if (File.Exists(path))
                    template = path;
				else
					throw new Exception("Could not find the Template file at " + template);
#else
				throw new Exception("Could not find the Template file at " + template);
#endif
			}
			Assembly assembly = Assembly.LoadFile(template);
			TemplateLoader loader = new TemplateLoader(assembly);
			var screens = loader.CallCustomNewProjectScreensFunction();
			if (screens.Count > 0)
			{
				AddScreens(screens);
			}
		}

		private void SetupCurrentScreen()
		{
			Controls.Clear();

			if (currentScreen == null) return;

			currentScreen.NewProjectForm = this;
			Control currentScreenAsControl = (Control)currentScreen;
			currentScreenAsControl.Dock = DockStyle.Fill;
			Controls.Add(currentScreenAsControl);

			currentScreen.Setup();
		}

		private void frmNewProject_Load(object sender, EventArgs e)
		{
		}
	}
}
