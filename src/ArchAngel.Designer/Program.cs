using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Designer.Properties;
using log4net.Config;
using Slyce.Common;

namespace ArchAngel.Designer
{
	public static class Program
	{
		private static frmSplash _SplashScreen;
		private static readonly string ApplicationDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
		private static readonly List<string> _AssemblySearchPaths = new List<string>();

		internal static List<string> AssemblySearchPaths
		{
			get
			{
				if (_AssemblySearchPaths.Count == 0)
				{
					_AssemblySearchPaths.Add(ApplicationDirectory);
				}
				return _AssemblySearchPaths;
			}
		}

		internal static void AddAssemblySearchPath(string path)
		{
			foreach (string dir in _AssemblySearchPaths)
			{
				if (Slyce.Common.Utility.StringsAreEqual(dir, path, false))
				{
					return;
				}
			}
			_AssemblySearchPaths.Add(path);
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static int Main(string[] args)
		{
			//try
			//{
			//XmlConfigurator.Configure();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Application.ThreadException += Application_ThreadException;
			//AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

			if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				MessageBox.Show("This is not the current AppDomain, so UnhandledException will not get handled properly.");
				return 1;
			}
			AddAssemblySearchPath(System.IO.Path.GetDirectoryName(Application.ExecutablePath));

			//foreach (string arg in args)
			//{
			//    if (Slyce.Common.Utility.StringsAreEqual(arg, "/compile", false))
			//    {
			//        Controller.CommandLine = true;
			//        int exitCode = CompileFromCommandLine(args[1], false) ? 0 : 1;
			//        Environment.Exit(exitCode);
			//    }
			//}
			//string message;
			//int daysRemaining;
			//bool errorOccurred;
			//bool demo;
			//Licensing.SlyceAuthorizer.LockTypes lockType;
			//Licensing.SlyceAuthorizer.LicenseStates status;
			//bool licensed = Licensing.SlyceAuthorizer.IsLicensed("Visual NHibernate License.SlyceLicense", out message, out daysRemaining, out errorOccurred, out demo, out lockType, out status);

			//if (errorOccurred)
			//{
			//    message = "An error occurred with the ArchAngel licensing system. Please inform support@slyce.com about this error:\n\nError: " + message;
			//    MessageBox.Show(message, "Licensing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//    Application.Exit();
			//}
			//else if (licensed && !demo)
			//{
			//    if (message.Length > 0)
			//    {
			//        MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//    }
			//    ShowSplashScreen(null);
			frmMain mainForm = new frmMain(args);
			Application.Run(mainForm);
			//}
			//else
			//{
			//    Licensing.LicenseWizard.frmLicenseWizard form = new Licensing.LicenseWizard.frmLicenseWizard(daysRemaining, lockType, status);
			//    Application.Run(form);

			//    if (Licensing.LicenseWizard.frmLicenseWizard.Result == Licensing.LicenseWizard.ScreenStart.Actions.Trial)
			//    {
			//        // Make sure user still has days left
			//        if (daysRemaining < 0)
			//        {
			//            Application.Exit();
			//            return 0;
			//        }
			//    }
			//    else if (Licensing.LicenseWizard.frmLicenseWizard.Result == Licensing.LicenseWizard.ScreenStart.Actions.Activate)
			//    {
			//        // Supposedly the user has activated, let's check
			//        if (!Licensing.SlyceAuthorizer.IsLicensed("Visual NHibernate License.SlyceLicense", out message, out daysRemaining, out errorOccurred, out demo, out lockType, out status))
			//        {
			//            Application.Exit();
			//            return 0;
			//        }
			//    }
			//    else if (Licensing.LicenseWizard.frmLicenseWizard.Result == Licensing.LicenseWizard.ScreenStart.Actions.None)
			//    {
			//        Application.Exit();
			//        return 0;
			//    }
			//    ShowSplashScreen(null);
			//    frmMain mainForm = new frmMain(args);
			//    Application.Run(mainForm);
			//}
			return 0;
			//}
			//catch (Exception ex)
			//{
			//    try
			//    {
			//        Controller.ReportError(ex);
			//    }
			//    catch (Exception ex2)
			//    {
			//        string fileManifest = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "ArchAngel.FileManifest.xml");
			//        string version = Application.ProductVersion;// Slyce.Common.Controls.VersionInfo.GetCurrentVersion(fileManifest);

			//        if (string.IsNullOrEmpty(version))
			//        {
			//            version = "debug";
			//        }
			//        Slyce.Common.Utility.SubmitError(@"http://www.slyce.com/errors/reporterror.asp", Application.ProductName, version, ex2.Message, ex2);
			//    }
			//    Environment.Exit(1);
			//    return 1;
			//}
		}

		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			AssemblyName assemblyToFind = new AssemblyName(args.Name);

			// Check already loaded assemblies. This list includes assemblies in the LoadFrom context, which is not
			// searched by default.
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				AssemblyName assemblyName = assembly.GetName();
				if (assemblyName.FullName == assemblyToFind.FullName)
				{
					return assembly;
				}
			}

			// Check referenced files
			ReferencedFile file =
				Project.Instance.References.FirstOrDefault(f => f.AssemblyName == assemblyToFind.Name);

			if (file != null && File.Exists(file.FileName))
			{
				var bytes = File.ReadAllBytes(file.FileName);
				return Assembly.Load(bytes);
			}

			// Last resort. Search the paths that the referenced files are on.
			var searchPaths = Project.Instance.References.Select(f => Path.GetDirectoryName(f.FileName));

			foreach (var path in searchPaths)
			{
				string filePath = path.PathSlash(assemblyToFind.Name) + ".dll";
				if (!File.Exists(filePath)) continue;

				var bytes = File.ReadAllBytes(filePath);
				return Assembly.Load(bytes);
			}

			return null;
		}

		//static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		//{
		//    Controller.ReportError((Exception)e.ExceptionObject);
		//    Environment.Exit(1);
		//}

		//static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		//{
		//    Controller.ReportError(e.Exception);
		//}

		internal static bool CompileFromCommandLine(string fileName, bool debugVersion)
		{
			Project.Instance.Open(fileName);
			Settings.Default.CodeFile = System.IO.Path.GetTempFileName();
			return CompileHelper.Compile(debugVersion);
		}

		private static void ShowSplashScreen(IWin32Window owner)
		{
			_SplashScreen = new frmSplash();
			_SplashScreen.MinimumDuration = TimeSpan.FromSeconds(3);

			if (_SplashScreen != null)
			{
				_SplashScreen.Show(owner);
				_SplashScreen.Update();
				Application.DoEvents();
			}
		}

		internal static void HideSplashScreen()
		{
			if (_SplashScreen == null) return;

			_SplashScreen.Close();
			_SplashScreen = null;
		}
	}
}
