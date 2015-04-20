using System;
using System.IO;
using System.Resources;

namespace ArchAngel.Designer
{
	/// <summary>
	/// Summary description for BaseApp.
	/// </summary>
	public abstract class BaseApp : MarshalByRefObject, IScriptManagerCallback
	{
		private static readonly ResourceManager resMgr = new ResourceManager("ArchAngel.Designer.NScript", typeof(BaseApp).Assembly);
		private AppDomain executionDomain;
		private string[] fileNames;

	    #region Overridables for derived classes
		protected void TerminateExecution()
		{
			AppDomain.Unload(executionDomain);
		}
		
		protected abstract void ExecutionLoop(IAsyncResult result);
		protected abstract void TerminateExecutionLoop();
		protected abstract void ShowErrorMessage(string message);

		#endregion

		#region Utility function for derived classes		
		protected string[] FileNames
		{
			get {return fileNames;}
		}

		protected string EntryAssemblyName
		{
			get { return Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath); }
		}

		protected void ShowErrorMessage(string message, params object[] args)
		{
			ShowErrorMessage(String.Format(message, args));
		}

		protected void ShowErrorMessageFromResource(string id, params object[] args)
		{
			ShowErrorMessage(resMgr.GetString(id), args);
		}
		
		protected static string GetResourceString(string name)
		{
			return resMgr.GetString(name);
		}

		protected static object GetResourceObject(string name)
		{
			return resMgr.GetObject(name);
		}
		#endregion

		#region Other Utility Methods
		private void ShowUsage()
		{
			ShowErrorMessageFromResource("Usage", EntryAssemblyName);
		}
		#endregion
		
		delegate void CompileAndExecuteRoutine(string[] files, string[] args, IScriptManagerCallback callback, string[] filesToEmbed);

		private void CompileAndExecute(string[] files, string[] args, IScriptManagerCallback callback, string[] filesToEmbed)
		{
			try
			{
				// Create an AppDomain to compile and execute the code
				// This enables us to cancel the execution if needed
				executionDomain = AppDomain.CreateDomain("ExecutionDomain");
				var manager = (IScriptManager)executionDomain.CreateInstanceFromAndUnwrap(typeof(BaseApp).Assembly.Location, typeof(ScriptManager).FullName); 
				manager.CompileAndExecuteFile(files, args, this, filesToEmbed);
			}
			catch(UnsupportedLanguageExecption e)
			{
				ShowErrorMessageFromResource("UnsupportedLanguage", e.Extension);
			}
			catch(AppDomainUnloadedException e)
			{
				System.Diagnostics.Trace.WriteLine(e.Message);
			}
			catch(Exception e)
			{
				ShowErrorMessage(e.Message);
			}
			
			TerminateExecutionLoop();
		}

		protected void Run(string[] args, string[] filesToEmbed)
		{
            Slyce.Common.Utility.CheckForNulls(new object[] { args, filesToEmbed }, new[] { "args", "filesToEmbed" });
			fileNames = args;
			var codeFiles = new string[fileNames.Length - 2];
			int fileCounter = 0;

			foreach (string fileName in fileNames)
			{
				if (fileName.IndexOf("=")< 0)
				{
					if (!File.Exists(fileName))
					{
						ShowErrorMessageFromResource("FileDoesnotExist", args[0]);
						return;
					}
					codeFiles[fileCounter] = fileName;
					fileCounter++;
				}
			}
		
			//			//Create new argument array removing the file name
			var newargs = new String[args.Length - 1];
			//string[] newargs = new String[1];
			//newargs[0] = outputFileType;
			Array.Copy(args, 1, newargs, 0, args.Length - 1);
			
			var asyncDelegate = new CompileAndExecuteRoutine(CompileAndExecute);
			IAsyncResult result = asyncDelegate.BeginInvoke(codeFiles, newargs, this, null, null, filesToEmbed);
			
			//For a windows app a message loop and for a console app a simple wait
			ExecutionLoop(result);

			asyncDelegate.EndInvoke(result);
		}

		#region Implementation of IScriptManagerCallback
		public void OnCompilerError(CompilerError[] errors)
		{
            Slyce.Common.Utility.CheckForNulls(new object[] { errors }, new[] { "errors" });

			var writer = new StringWriter();
				
			string errorFormat = GetResourceString("CompilerErrorFormat");
				
			foreach(CompilerError error in errors)
			{
				writer.WriteLine(errorFormat, error.File, error.Number, error.Text, error.Line, error.Column);
			}
			throw new ApplicationException(writer.ToString());
		}
		#endregion
	}
}
