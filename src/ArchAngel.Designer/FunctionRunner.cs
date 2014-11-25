using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
//using ArchAngel.Debugger.DebugProcess;
using ArchAngel.Designer.DesignerProject;
using Microsoft.CSharp;
using Slyce.Common;

namespace ArchAngel.Designer
{
	internal class FunctionRunner
	{
		private bool _ProjectHasBeenCompiled;
		private bool _DebuggerRunning;
		internal FunctionInfo CurrentFunction;
		internal bool[] ValuesThatHaveBeenSet = new bool[0];
		internal object[] ParametersToPass = new object[0];
		private string FunctionNamespace;
		internal string FunctionFilename;
		private readonly List<BreakpointReference> _Breakpoints = new List<BreakpointReference>();

		internal static readonly Dictionary<FunctionInfo, List<object>> CachedParameters = new Dictionary<FunctionInfo, List<object>>();

		private struct BreakpointReference : IEquatable<BreakpointReference>
		{
			private readonly FunctionInfo _Function;
			private readonly int _OriginalLineNumber;
			private readonly int _OriginalColumnIndex;


			public BreakpointReference(FunctionInfo function, int originalLineNumber, int originalColumnIndex)
			{
				_Function = function;
				_OriginalLineNumber = originalLineNumber;
				_OriginalColumnIndex = originalColumnIndex;
			}

			public FunctionInfo Function
			{
				get { return _Function; }
			}

			public int OriginalLineNumber
			{
				get { return _OriginalLineNumber; }
			}


			public int OriginalColumnIndex
			{
				get { return _OriginalColumnIndex; }
			}


			public bool Equals(BreakpointReference breakpointReference)
			{
				if (!Equals(_Function, breakpointReference._Function))
				{
					return false;
				}
				if (_OriginalLineNumber != breakpointReference._OriginalLineNumber)
				{
					return false;
				}
				if (_OriginalColumnIndex != breakpointReference._OriginalColumnIndex)
				{
					return false;
				}
				return true;
			}

			public override bool Equals(object obj)
			{
				if (!(obj is BreakpointReference))
				{
					return false;
				}
				return Equals((BreakpointReference)obj);
			}

			public override int GetHashCode()
			{
				int result = _Function.GetHashCode();
				result = 29 * result + _OriginalLineNumber;
				result = 29 * result + _OriginalColumnIndex;
				return result;
			}
		}


		public FunctionRunner(ucFunction functionScreen, FunctionInfo function)
		{
			//string outputSyntaxFilePath = Slyce.Common.SyntaxEditorHelper.GetLanguageFileName(Project.Instance.TextLanguage);

			//btnTest.Enabled = true;
			CurrentFunction = function;
			//OriginalCompileFolderName = Project.Instance.CompileFolderName;

			//syntaxEditor1.Document.Language = ActiproSoftware.SyntaxEditor.Addons.Dynamic.DynamicSyntaxLanguage.LoadFromXml(outputSyntaxFilePath, 0);
		}

		public bool ProjectHasBeenCompiled
		{
			get { return _ProjectHasBeenCompiled; }
			// The checks beow were commented out when removing the need to save a project before compiling:
			//&& !Project.Instance.IsDirty
			//&& !_FunctionScreen.IsDirty; }
		}

		public bool DebuggerRunning
		{
			get { return _DebuggerRunning; }
		}

		public string TempCompileFolderName { get; set; }

		internal void CompileProject(bool randomiseTheNamespace)
		{
			_ProjectHasBeenCompiled = false;
			//string origTarget = Project.Instance.CompileFolderName;
			bool success = CreateTemplateFile(randomiseTheNamespace, true);
			if (success)
			{
				FunctionFilename = CompileHelper.SourceFile + ".cs";
				FunctionNamespace = CompileHelper.NamespaceUsed;
				CompilerResults result = Recompile(FunctionFilename);
				if (result.Errors.Count == 0)
				{
					_ProjectHasBeenCompiled = true;
					//Controller.Instance.DebuggerInstance.CompiledAssemblyLocation = result.CompiledAssembly.Location;
					//Controller.Instance.DebuggerInstance.AaprojXml = Project.Instance.ProjectXmlConfig;
				}
				else
				{
					// TODO: Find a better way of reporting compile errors to users.
					MessageBox.Show(result.Errors[0].ErrorText);
				}
			}
		}

		public static string CompileTemplate()
		{
			bool success = CreateTemplateFile(false, true);
			if (success)
			{
				CompilerResults result = Recompile(CompileHelper.SourceFile + ".cs");
				if (result.Errors.Count != 0)
				{
					MessageBox.Show(result.Errors[0].ErrorText);
				}
				else
				{
					return result.PathToAssembly;
				}
			}
			return null;
		}

		public static bool CreateTemplateFile(bool randomiseTheNamespace, bool debugVersion)
		{
			string TempCompileFolderName = Project.Instance.CompileFolderName;
			try
			{
				Project.Instance.CompileFolderName = Path.Combine(Controller.Instance.GetTempFilePathForComponent(ComponentKey.Designer_CompileFolder), Path.GetRandomFileName().Replace(".", ""));

				if (!Directory.Exists(Project.Instance.CompileFolderName))
				{
					Directory.CreateDirectory(Project.Instance.CompileFolderName);
				}

				bool originalRandomise = CompileHelper.RandomiseTheNamespace;
				CompileHelper.RandomiseTheNamespace = randomiseTheNamespace;

				bool success = Controller.Instance.MainForm.CompileProject(debugVersion, false);

				CompileHelper.RandomiseTheNamespace = originalRandomise;
				return success;
			}
			finally
			{
				Project.Instance.CompileFolderName = TempCompileFolderName;
			}
		}

		private static CompilerResults Recompile(string sourceFilename)
		{
			string tempPath = Path.Combine(Path.GetTempPath(), "ArchAngel.Debug");
			Directory.CreateDirectory(tempPath);

			// Compile the template file.
			CSharpCodeProvider codeProvider = new CSharpCodeProvider();
			CompilerParameters param = new CompilerParameters
										{
											GenerateExecutable = false,
											IncludeDebugInformation = true,
											TreatWarningsAsErrors = false,
											CompilerOptions = "/nowarn:1687 ",
											OutputAssembly = Path.Combine(tempPath, Path.GetRandomFileName() + ".dll")
										};

			// Add all of the assemblies we know we need.
			//foreach (string loc in Controller.Instance.DebuggerInstance.AssemblyLocations)
			//{
			//    param.ReferencedAssemblies.Add(loc);

			//}

			// Add the common assemblies that we always need.
			param.ReferencedAssemblies.Add("System.Xml.dll");
			param.ReferencedAssemblies.Add("System.Core.dll");
			param.ReferencedAssemblies.Add("System.dll");
			param.ReferencedAssemblies.Add("System.Windows.Forms.dll");

			//param.ReferencedAssemblies.Add("Slyce.Common.dll");
			param.ReferencedAssemblies.Add(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Slyce.Common.dll"));

			return codeProvider.CompileAssemblyFromFile(param, sourceFilename);
		}

		/// <summary>
		/// Adds a breakpoint to the assocated debugger. If the debugger is running,
		/// it spawns a new thread that adds the breakpoint to avoid cross threading issues
		/// with MDbg (see http://blogs.msdn.com/jmstall/archive/2006/01/27/mdbg_threading_2.aspx for
		/// more information about this).
		/// </summary>
		/// <param name="function">The name of the function we are adding the breakpoint to.</param>
		/// <param name="functionLineNumber">The line number in the function code to add the breakpoint to.</param>
		/// <param name="originalColumnIndex">The column index of the piece of function code to add the breakpoint to.</param>
		public void SetBreakpoint(FunctionInfo function, int functionLineNumber, int originalColumnIndex)
		{
			BreakpointReference bp = new BreakpointReference(function, functionLineNumber, originalColumnIndex);
			foreach (BreakpointReference bpr in _Breakpoints)
			{
				if (bpr.Equals(bp))
					return;
			}
			_Breakpoints.Add(bp);

			//if (DebuggerRunning)
			//{
			//    ThreadStart ts = () => Controller.Instance.DebuggerInstance.AddBreakpoint(ResolveBreakpointReference(bp));

			//    Thread thread = new Thread(ts);
			//    thread.SetApartmentState(ApartmentState.MTA);
			//    thread.Start();
			//}
		}

		/// <summary>
		/// Removes a breakpoint from the assocated debugger. If the debugger is running,
		/// it spawns a new thread that removes the breakpoint to avoid cross threading issues
		/// with MDbg (see http://blogs.msdn.com/jmstall/archive/2006/01/27/mdbg_threading_2.aspx for
		/// more information about this).
		/// </summary>
		/// <param name="function">The name of the function we are removing the breakpoint from.</param>
		/// <param name="functionLineNumber">The line number in the function code to remove the breakpoint from.</param>
		/// <param name="originalColumnIndex">The column index of the piece of function code to remove the breakpoint from.</param>
		public void RemoveBreakpoint(FunctionInfo function, int functionLineNumber, int originalColumnIndex)
		{
			BreakpointReference bp = new BreakpointReference(function, functionLineNumber, originalColumnIndex);
			_Breakpoints.Remove(bp);

			//if (DebuggerRunning)
			//{
			//    ThreadStart ts = () => Controller.Instance.DebuggerInstance.RemoveBreakpoint(ResolveBreakpointReference(bp));

			//    Thread thread = new Thread(ts);
			//    thread.SetApartmentState(ApartmentState.MTA);
			//    thread.Start();
			//}
		}

		/// <summary>
		/// Clears all breakpoints from the assocated debugger. If the debugger is running,
		/// it spawns a new thread that clears the breakpoints to avoid cross threading issues
		/// with MDbg (see http://blogs.msdn.com/jmstall/archive/2006/01/27/mdbg_threading_2.aspx for
		/// more information about this).
		/// </summary>
		public void ClearBreakpoints()
		{
			_Breakpoints.Clear();

			//if (DebuggerRunning)
			//{
			//    ThreadStart ts = () => Controller.Instance.DebuggerInstance.ClearBreakpoints();

			//    Thread thread = new Thread(ts);
			//    thread.SetApartmentState(ApartmentState.MTA);
			//    thread.Start();
			//}
		}

		/// <summary>
		/// Loads the new assembly, unloading the previously loaded assembly ( if it is loaded.)
		/// </summary>
		/// <param name="filepath">The path of the assembly to load.</param>
		/// <param name="executablePath">The path of the currently executing program.</param>
		/// <exception cref="Exception">Throws an Exception if the template file couldn't be loaded.</exception>
		internal static bool LoadAssembly(string filepath, string executablePath)
		{
			if (!File.Exists(filepath))
			{
				throw new Exception(string.Format("The template file could not be loaded: \n{0}", filepath));
			}
			//            if (!_TemplateHasBeenLoaded)
			//            {
			//                string templateFolder = Path.GetDirectoryName(filepath);
			//                List<string> searchPaths = new List<string>();
			//                //searchPaths.Add(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath));
			//                searchPaths.Add(Path.GetDirectoryName(executablePath));
			//                searchPaths.Add(Path.GetDirectoryName(filepath));
			//                searchPaths.Sort();

			//                foreach (Assembly assembly in Project.Instance.ReferencedAssemblies)
			//                {
			//                    string directory = Path.GetDirectoryName(assembly.Location);

			//                    if (searchPaths.BinarySearch(directory) < 0)
			//                    {
			//                        searchPaths.Add(directory);
			//                        searchPaths.Sort();
			//                    }
			//#if DEBUG
			//                    // Copy the referenced assmblies to the temp folder
			//                    string destinationFile = Path.Combine(Path.GetDirectoryName(executablePath), Path.GetFileName(assembly.Location));

			//                    if (!File.Exists(destinationFile) && assembly.Location != null)
			//                    {
			//                        File.Copy(assembly.Location, destinationFile);
			//                    }
			//                    string natFile = Path.Combine(Path.GetDirectoryName(assembly.Location), Path.GetFileNameWithoutExtension(assembly.Location) + "_nat" + Path.GetExtension(assembly.Location));
			//                    destinationFile = Path.Combine(Path.GetDirectoryName(executablePath), Path.GetFileName(natFile));

			//                    if (File.Exists(natFile) && !File.Exists(destinationFile))
			//                    {
			//                        File.Copy(natFile, destinationFile);
			//                    }
			//#endif
			//                }
			//                Loader.Instance.SetTemplateFolder(templateFolder, searchPaths);

			//                if (!File.Exists(filepath))
			//                {
			//                    throw new FileNotFoundException("File is missing: " + filepath);
			//                }
			//                Loader.ForceNewAppDomain = true;
			//                bool result = Loader.Instance.LoadAssembly(filepath, searchPaths, FunctionNamespace);
			//                _TemplateHasBeenLoaded = result;
			//            }
			return true;//_TemplateHasBeenLoaded;
		}

		///// <summary>
		///// Gets a list of the UserOptionValues from the current project.
		///// </summary>
		///// <returns>A list of the UserOptionValues from the current project.</returns>
		///// <remarks>Creates a temp folder called ArchAngel.DebugCSPDatabase.</remarks>
		//internal static List<UserOptionValues> GetUserOptionValues()
		//{
		//    string projectFilePath = Project.Instance.DebugProjectFile;
		//    string optionsXmlPath = WorkbenchProject.GetOptionsXmlPathForProjectFile(projectFilePath);

		//    return GetUserOptionValuesFromFile(optionsXmlPath, Project.Instance.UserOptions);
		//}

		//private static List<UserOptionValues> GetUserOptionValuesFromFile(string optionsXmlPath, IEnumerable<UserOption> options)
		//{
		//    List<UserOptionValues> userOptionValues = new List<UserOptionValues>();

		//    XmlDocument doc = new XmlDocument();
		//    doc.Load(optionsXmlPath);

		//    XmlNodeList nodes = doc.SelectNodes("/*/*/*");
		//    if(nodes != null)
		//    {
		//        foreach (XmlNode userOptionNode in nodes)
		//        {
		//            foreach (UserOption opt in options)
		//            {
		//                if (opt.VariableName != userOptionNode.Name) continue;
		//                object obj;

		//                if (opt.VarType == typeof(string) || opt.VarType.IsValueType || opt.VarType is IConvertible)
		//                    obj = Convert.ChangeType(userOptionNode.InnerText, opt.VarType);
		//                else if (opt.VarType.IsEnum)
		//                    obj = Enum.Parse(opt.VarType, userOptionNode.InnerText, true);
		//                else if (opt.VarType.Name.ToLower() == "enum")
		//                    obj = userOptionNode.InnerText;
		//                else
		//                    throw new NotImplementedException("This object type has not been catered for yet: " + opt.VarType.Name);

		//                userOptionValues.Add(new UserOptionValues(opt.VariableName, obj));
		//            }
		//        }
		//    }
		//    return userOptionValues;
		//}

		/// <summary>
		/// Runs the current function using the debugger from the Controller instance.
		/// Checks that the parameter data has been set correctly.
		/// </summary>
		/// <returns>Any exceptions that were thrown, or null.</returns>
		internal object CallFunction()
		{
			return null;
			//lock (this)
			//{
			//    if (CheckParameterData() == false)
			//        return new Exception("Parameter data not set correctly");

			//    try
			//    {
			//        Debugger.Debugger debugger = Controller.Instance.DebuggerInstance;
			//        debugger.ClearBreakpoints();
			//        foreach (BreakpointReference bp in _Breakpoints)
			//        {
			//            debugger.AddBreakpoint(ResolveBreakpointReference(bp));
			//        }

			//        _DebuggerRunning = true;

			//        debugger.CodeFilename = FunctionFilename;
			//        debugger.IsFunctionStatic = CurrentFunction.IsExtensionMethod;
			//        debugger.TestClassname = CurrentFunction.IsExtensionMethod ? "TemplateGenExtensionMethods" : "TemplateGen";
			//        debugger.TestNamespace = FunctionNamespace;
			//        debugger.TestFunctionName = CurrentFunction.Name;
			//        debugger.ArgumentList = GetArgumentList();
			//        debugger.UserOptions = GetUserOptionValues();
			//        debugger.Run();
			//        _DebuggerRunning = false;
			//        return null;
			//    }
			//    catch (Exception ex)
			//    {
			//        return ex;
			//    }
			//}
		}

		private static int ResolveBreakpointReference(BreakpointReference bp)
		{
			return CompileHelper.CompiledLinesLookup[
				bp.Function,
				bp.OriginalLineNumber,
				bp.OriginalColumnIndex]
					.CompiledLineNumber + 1;
		}

		//private ArgumentList GetArgumentList()
		//{
		//    if(ParametersToPass == null || ParametersToPass.Length == 0)
		//        return new ArgumentList(new object[0]);

		//    List<Argument> arguments = new List<Argument>();
		//    for (int paramIndex = 0; paramIndex < ParametersToPass.Length; paramIndex++)
		//    {
		//        object obj = ParametersToPass[paramIndex];
		//        if (obj.GetType().IsPrimitive || obj is string)
		//        {
		//            arguments.Add(new Argument.Primitive(obj));
		//        }
		//        else
		//        {
		//            // Figure out the index of the object
		//            IScriptBaseObject baseObj = obj as IScriptBaseObject;
		//            if (baseObj == null)
		//            {
		//                throw new Exception("Type of object not supported as a parameter yet: " + obj.GetType());
		//            }

		//            IEnumerable<IScriptBaseObject> objects = null;
		//            foreach (ProviderInfo provider in Controller.PopulatedProviders.Values)
		//            {
		//                try
		//                {
		//                    objects = provider.GetAllObjectsOfType(baseObj.GetType().FullName);
		//                    break;
		//                }
		//                catch (Exception)
		//                {
		//                }
		//            }

		//            if (objects == null)
		//            {
		//                throw new Exception("Could not find the type in the currently loaded providers");
		//            }

		//            bool found = false;
		//            for (int i = 0; i < objects.Count(); i++)
		//            {
		//                IScriptBaseObject sbo = objects.ElementAt(i);
		//                if (baseObj.Equals(sbo))
		//                {
		//                    arguments.Add(new Argument(baseObj.GetType().FullName, baseObj.GetType().Assembly.FullName, i));
		//                    found = true;
		//                    break;
		//                }
		//            }
		//            if (found == false)
		//            {
		//                throw new Exception("Could not find parameter " + paramIndex +
		//                                    " in the currently loaded providers");
		//            }
		//        }
		//    }
		//    return new ArgumentList(arguments.ToArray());
		//}

		public bool CheckParameterData()
		{
			if (ValuesThatHaveBeenSet == null)
				return false;
			if (ValuesThatHaveBeenSet.Length != CurrentFunction.Parameters.Count)
				return false;
			foreach (bool b in ValuesThatHaveBeenSet)
			{
				if (!b)
				{
					return false;
				}
			}

			return true;
		}
	}
}
