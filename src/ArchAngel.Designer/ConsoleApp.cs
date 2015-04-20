using System;
using System.Runtime.InteropServices;

namespace ArchAngel.Designer
{
	/// <summary>
	/// Summary description for ConsoleApp.
	/// </summary>
	public class ConsoleApp : BaseApp
	{
	    delegate bool CtrlHandlerRoutine(int eventType);

		[DllImport("kernel32.dll", SetLastError=true)]
		extern static bool SetConsoleCtrlHandler(CtrlHandlerRoutine handler);		
		
		private bool CtrlHandler(int eventType)
		{
			Console.Write(BaseApp.GetResourceString("CancelExecution"), "(Y/N)");
		
			if (String.Compare(Console.ReadLine(), "Y", true) == 0)
			{
				TerminateExecution();
			}
			return true;
		}

		protected override void TerminateExecutionLoop()
		{
			
		}

		protected override void ShowErrorMessage(string message)
		{
			System.Windows.Forms.MessageBox.Show(message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
			Console.Error.WriteLine(message);
		}

		protected override void ExecutionLoop(IAsyncResult result)
		{
			result.AsyncWaitHandle.WaitOne();
		}

		public static void RunMe(string[] filenames, string outputFileName, string nrfFilename, string[] filesToEmbed)
		{
            Slyce.Common.Utility.CheckForNulls(new object[] { filenames, outputFileName, nrfFilename, filesToEmbed }, new string[] { "filenames", "outputFileName", "nrfFilename", "filesToEmbed" });
			ConsoleApp app = new ConsoleApp();
			CtrlHandlerRoutine handler = new CtrlHandlerRoutine(app.CtrlHandler);
			
			//Install a console ctrl handler so that we may unload the exceution domain
			//If the user wants to cancel by pressing Ctrl+C
			SetConsoleCtrlHandler(handler);
			string[] args	= new string[filenames.Length + 2];
			int index		= 0;

			for (int i = 0; i < filenames.Length; i++)
			{
				args[i] = filenames[i];
				index++;
			}
			args[index]		= "outputFileName="+ outputFileName;
			args[index + 1] = "nrfFileName="+ nrfFilename;
			app.Run(args, filesToEmbed);
			GC.KeepAlive(handler);
		}
	}
}
