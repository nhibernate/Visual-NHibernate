using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using ArchAngel.Common;
using ArchAngel.Debugger.Common;
using ArchAngel.Interfaces;
using log4net;
using Slyce.Common;

namespace ArchAngel.Debugger.DebugProcess
{
	[Serializable]
	public class RunFunctionCommand : ICommand
	{
		public string AssemblyFilename;
		public string Namespace;
		public string Classname;
		public string FunctionName;
		public ArgumentList Arguments;
		public List<UserOptionValues> UserOptions = new List<UserOptionValues>();
		public string AaprojXml;
		public bool IsStaticMethod;

		private static readonly ILog log = LogManager.GetLogger(typeof(RunFunctionCommand));

		public RunFunctionCommand(string assemblyFilename, string _namespace, string classname, string functionName, ArgumentList arguments, List<UserOptionValues> userOptions, string aaprojXml, bool isStaticMethod)
		{
			AssemblyFilename = assemblyFilename;
			Namespace = _namespace;
			Classname = classname;
			FunctionName = functionName;
			Arguments = arguments;
			UserOptions = userOptions;
			AaprojXml = aaprojXml;
			IsStaticMethod = isStaticMethod;
		}

		public bool RunCommand(CommandReceiver runner)
		{
			if (runner.Loaded == false)
				throw new Exception("CommandReceiver not loaded yet");

			SharedData.CurrentProject = runner.CurrentProject;

			Assembly assembly = Assembly.LoadFrom(AssemblyFilename);

			log.DebugFormat("Loading Type {0}.{1}", Namespace, Classname);
			Type generatedObjectType = assembly.GetType(Namespace + "." + Classname);

			if (generatedObjectType == null)
				throw new Exception("Could not find test class");

			log.DebugFormat("Loader State: {0}", runner.CurrentProject.TemplateLoader != null ? "Loaded" : "Not Loaded");

			runner.CurrentProject.LoadTemplate(AssemblyFilename);

			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			doc.LoadXml(AaprojXml);

			runner.CurrentProject.InitFromDesignerProjectXml(doc);

			log.DebugFormat("Loader State: {0}", runner.CurrentProject.TemplateLoader != null ? "Loaded" : "Not Loaded");

			runner.CurrentProject.FillProviders();

			SetUserOptions(assembly);
			runner.CurrentProject.FillVirtualProperties();

			log.DebugFormat("Loading Function {0}", FunctionName);
			MethodInfo method = generatedObjectType.GetMethod(FunctionName);
			ThreadStart start
				= delegate
					{
						Thread.Sleep(1000);

						object instance = IsStaticMethod ? null : assembly.CreateInstance(Namespace + "." + Classname);

						string obj = method.Invoke(instance, Arguments.ToArray(runner.CurrentProject.Providers)).ToString();
#line 16707564
						Console.WriteLine(obj);
#line default
					};
			Thread thread = new Thread(start);
			thread.Start();

			return true;
		}

		// Set the UserOptions
		private void SetUserOptions(Assembly assembly)
		{
			if (IsStaticMethod) return; // Static methods don't have user options.
			Type type = assembly.GetType(Namespace + "." + Classname + "+UserOptions");

			foreach (UserOptionValues option in UserOptions)
			{
				MethodInfo m = type.GetMethod("set_" + option.OptionName);
				m.Invoke(type, new[] { option.OptionValue });
			}
		}
	}

	[Serializable]
	public class UserOptionValues
	{
		public string OptionName;
		public object OptionValue;

		public UserOptionValues(string optionName, object optionValue)
		{
			OptionName = optionName;
			OptionValue = optionValue;
		}

		public override string ToString()
		{
			return OptionName + ": " + OptionValue;
		}
	}

	[Serializable]
	public class ArgumentList
	{
		private readonly object[] simpleObjects;
		private readonly Argument[] arguments;

		public ArgumentList(object[] simpleObjects)
		{
			this.simpleObjects = simpleObjects;
		}

		public ArgumentList(Argument[] arguments)
		{
			this.arguments = arguments;
		}

		public object[] ToArray(List<ProviderInfo> providers)
		{
			if (simpleObjects != null)
				return simpleObjects;

			if (arguments != null)
			{
				object[] objects = new object[arguments.Length];
				for (int i = 0; i < objects.Length; i++)
				{
					objects[i] = arguments[i].GetObject(providers);
				}

				return objects;
			}

			return new object[0];
		}
	}

	/// <summary>
	/// This class is used to represent an Argument to a method.
	/// Since some of the objects we needs to pass to the debugging
	/// process contain links to other objects, in order to serialise 
	/// just one of these often requires a large object graph to be serialised.
	/// Since that can be expensive, you can instead send over a type name and
	/// an index into the ProviderInfo object that created it. We can then
	/// recreate the object on the remote end without having to serialize it.
	/// This means that there is no repeat cost of creating the RootPreviewObjects
	/// that each Provider creates.
	/// If you need to pass a primitive object, or an object that you know won't
	/// require much in the way of serialization, use the Argument.Primitive class.
	/// If the object will need a lot of serialization, create a sub class of Argument
	/// and override GetObject to return your object from some easily serializable
	/// information.
	/// </summary>
	[Serializable]
	public class Argument
	{
		private readonly string FullTypeName;
		private readonly int ObjectIndex;
		private readonly string FullProviderAssemblyName;

		private static readonly ILog log = LogManager.GetLogger(typeof(Argument));

		/// <summary>
		/// Constructor for use by sub classes.
		/// </summary>
		private Argument() { }

		/// <summary>
		/// Constructs an Argument object from the fully qualified type name of the object,
		/// plus an index into the array returned by the ArchAngel Provider 
		/// GetAllObjectsOfType(fullTypeName) method.
		/// </summary>
		/// <param name="fullTypeName">The fully qualified type name of the object.</param>
		/// <param name="fullProviderAssemblyName">The full name of the assembly that contains the Provider.</param>
		/// <param name="objectIndex">An index into the array returned by the 
		/// ProviderInfo.GetAllObjectsOfType(fullTypeName) method. Will be used at a
		/// later point to recreate the object referenced by this index.</param>
		public Argument(string fullTypeName, string fullProviderAssemblyName, int objectIndex)
		{
			FullTypeName = fullTypeName;
			FullProviderAssemblyName = fullProviderAssemblyName;
			ObjectIndex = objectIndex;
		}

		/// <summary>
		/// Gets the object represented by this Argument from the ProviderInfo
		/// object provided. The ProviderInfo object given should be of the same 
		/// type as the one that was used in the construction of this object, and
		/// the same project file should be loaded.</summary>
		/// <param name="providers">The list of ProviderInfos to search for the provider used to get the referenced object.</param>
		/// <returns>The object returned by provider.GetAllObjectsOfType(FullTypeName)[ObjectIndex]</returns>
		internal virtual object GetObject(IEnumerable<ProviderInfo> providers)
		{
			ProviderInfo provider = providers.FirstOrDefault(p => p.GetType().Assembly.FullName == FullProviderAssemblyName);

			if (provider == null)
			{
				log.ErrorFormat("Could not find the provider assembly {0} in the currently loaded providers",
								FullProviderAssemblyName);
				throw new Exception(string.Format("Could not find the provider assembly {0} in the currently loaded providers", FullProviderAssemblyName));
			}

			IEnumerable<IScriptBaseObject> type = provider.GetAllObjectsOfType(FullTypeName);
			if (type.Count() <= ObjectIndex)
			{
				throw new Exception(
					string.Format("ObjectIndex was greater than the number of objects returned ({0} > {1} from GetAllObjectsOfType({2})",
					ObjectIndex, type.Count(), FullTypeName));
			}
			return type.ElementAt(ObjectIndex);
		}

		/// <summary>
		/// IMPORTANT!!!
		/// If you use this Argument class, make sure the CommandReceiver
		/// can load the type of the object without loading additional assemblies,
		/// and that the object has no object references.
		/// In other words, only use this for primitives and Strings.
		/// You will kill the performance benefits and clean separation we were
		/// going for if you ignore this warning!
		/// </summary>
		[Serializable]
		public class Primitive : Argument
		{
			private readonly object Object;
			/// <summary>
			/// Setup the Argument with the specified object
			/// </summary>
			/// <param name="primitive">The object to use as an Argument. 
			/// Make sure that it is a primitive or String!</param>
			public Primitive(object primitive)
			{
				Object = primitive;
			}
			/// <summary>
			/// Returns the primitive (or string) object it was constructed with.
			/// </summary>
			/// <param name="providers">Ignored. See parent class documentation for more information.
			/// Can be null.</param>
			/// <returns>The primitive (or string) object it was constructed with.</returns>
			internal override object GetObject(IEnumerable<ProviderInfo> providers)
			{
				return Object;
			}
		}
	}

	[Serializable]
	public class ExitCommand : ICommand
	{
		public bool RunCommand(CommandReceiver c)
		{
			DebugProcessMain.EndDebugProcess.Set();
			return false;
		}
	}

	[Serializable]
	public class LoadAssembliesCommand : ICommand
	{
		public string ArchAngelProjectFileLocation = "";
		public List<string> AssemblyFullNames;
		public string CurrentFilePath;
		private readonly List<string> AssemblySearchPaths;

		private static readonly ILog log = LogManager.GetLogger(typeof(LoadAssembliesCommand));

		public LoadAssembliesCommand(string archAngelProjectFileLocation, IEnumerable<string> assemblyFileNames, IEnumerable<string> assemblySearchPaths, string currentFilePath)
		{
			ArchAngelProjectFileLocation = archAngelProjectFileLocation;
			AssemblyFullNames = new List<string>(assemblyFileNames);
			AssemblySearchPaths = new List<string>(assemblySearchPaths);
			CurrentFilePath = currentFilePath;
		}

		public bool RunCommand(CommandReceiver runner)
		{
			log.Debug("Running LoadAssemblies command.");
			log.InfoFormat("Project File: {0}", ArchAngelProjectFileLocation);
			log.InfoFormat("Current File Path: {0}", CurrentFilePath);

			log.Info("The following assemblies were added to the AssemblySearchPaths:");
			if (log.IsInfoEnabled)
				foreach (var path in AssemblySearchPaths) log.Info(path);

			SharedData.AddAssemblySearchPaths(AssemblySearchPaths);

			runner.CurrentProject = new WorkbenchProject();

			string fakeProjectDirectory = PathHelper.GetTempFilePathFor("ArchAngel", ArchAngelProjectFileLocation, ComponentKey.Debugger_FakeProjectDirectory);
			IVerificationIssueSolver solver = new CustomVerificationIssueSolver("", fakeProjectDirectory);
			bool result = runner.CurrentProject.Load(ArchAngelProjectFileLocation, solver, true, null);

			if (result)
			{
				runner.Loaded = true;
				return runner.Loaded;
			}

			log.ErrorFormat("Could not load the project: {0}", solver.GetValidTemplateFilePath("", ""));
			throw new Exception("Could not load the project.");

			/*
			foreach (string assemblyFullname in AssemblyFullNames)
			{
				Assembly assembly = Assembly.LoadFrom(assemblyFullname);
                
				if (ProviderInfo.IsProvider(assembly))
				{
					// Unzip .aaproj file
					XmlDocument doc = new XmlDocument();
					doc.Load(projectFilename);

					NodeProcessor proc = new NodeProcessor(doc.DocumentElement);
					string folder = proc.GetString("Folder");
					tempFolder = RelativePaths.RelativeToAbsolutePath(Path.GetDirectoryName(projectFilename), folder);


					string tempFolder = PathHelper.GetTempFilePathFor("ArchAngel", ArchAngelProjectFileLocation, ComponentKey.Debugger_LoadAssemblies);
					Directory.CreateDirectory(tempFolder);
					Utility.UnzipFile(ArchAngelProjectFileLocation, tempFolder);
					string zipFile = tempFolder.PathSlash(assembly.GetName().Name.Replace(".", "_") + "_data.zip");
					string tempProviderFolder = Path.Combine(Path.GetTempPath(), assembly.GetName().Name.Replace(".", "_") + "Temp");

					if (Directory.Exists(tempProviderFolder))
					{
						Utility.DeleteDirectoryBrute(tempProviderFolder);
					}
					Directory.CreateDirectory(tempProviderFolder);

					if (File.Exists(zipFile))
					{
						Utility.UnzipFile(zipFile, tempProviderFolder);

						ProviderInfo providerInfo = ProviderInfo.GetProviderInfo(assembly);
						if (providerInfo == null)
						{
							throw new Exception("ProviderInfo type not found in assembly: " + assembly.GetName().Name);
						}

						providerInfo.Open(tempProviderFolder);
						runner.CurrentProvider = providerInfo;
						runner.Loaded = true;
					}
					//else
					//	throw new Exception("Could not find the Provider data zip file in the temp directory: " + zipFile);
				}
				//runner.Loaded = true;
			}
			*/
		}
	}

	public interface ICommand
	{
		bool RunCommand(CommandReceiver runner);
	}
}