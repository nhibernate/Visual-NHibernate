using System;
using System.Windows.Forms;

namespace ApiExtender
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				// TODO: Help message to go here
				const string helpText = "TODO: Add Help text";
				string dllFile = "";
				string projectFile = "";

				for (int i = 0; i < args.Length; i++)
				{
					switch (args[i])
					{
						case "/assembly":
							dllFile = args[i + 1];
							break;
						case "/project":
							projectFile = args[i + 1];
							break;
						case "?":
						case "/?":
							Console.WriteLine(helpText);
							Application.Exit();
							return;
						default:
							// Do nothing
							break;
					}
				}
				bool canRun = true;

				if (string.IsNullOrEmpty(dllFile))
				{
					canRun = false;
					Console.WriteLine("Assembly not specified.");
				}
				if (string.IsNullOrEmpty(projectFile))
				{
					canRun = false;
					Console.WriteLine("Project file not specified.");
				}
				if (!canRun)
				{
					Console.WriteLine(@"Usage: cecil.exe /assembly ""<ASSEMBLY_FILE>"" /project ""<PROJECT_FILE>""");
					Console.WriteLine("Help: cecil.exe ?");
				}
				else
				{
					Injector.RunningCommandLine = true;

					Injector.WriteFunctionBodiesAsXml(projectFile, dllFile);

					//if (!Injector.Run(dllFile, projectFile))
					//{
					//    Console.WriteLine("ApiExtender Failed.");
					//    Console.Error.WriteLine("ApiExtender Failed.");
					//}
				}
				Application.Exit();
				return;
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmMain());
		}
	}
}
