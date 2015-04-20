using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using ArchAngel.Interfaces;
using Slyce.Common;
using Slyce.Common.EventExtensions;
using Slyce.Common.IEnumerableExtensions;
using UserOption = ArchAngel.Common.DesignerProject.UserOption;

namespace ArchAngel.Designer.DesignerProject
{
	public class ProjectBase : IDesignerProject
	{
		private string m_projectName = "";
		private string m_projectDescription = "";
		private string m_version = "";
		private OutputFolder m_rootOutput;// This just holds a copy of ALL files and folders
		protected List<ReferencedFile> m_references = new List<ReferencedFile>();
		protected readonly List<Assembly> m_referencedAssemblies = new List<Assembly>();
		private List<string> m_namespaces = new List<string>();
		private ProjectTypes m_projectType = ProjectTypes.None;
		protected List<UserOption> m_userOptions = new List<UserOption>();
		private string m_compileFolderName = "";
		private int NamespaceCount = -1;
		private string _DebugProjectFile = "";
		private string _TestGenerateDirectory;
		protected readonly List<IncludedFile> m_includedFiles = new List<IncludedFile>();
		protected readonly Dictionary<MethodInfo, ApiExtensionMethod> ApiExtensionMethods = new Dictionary<MethodInfo, ApiExtensionMethod>();
		protected bool RefreshReferencedAssemblies = true;
		private readonly Dictionary<MethodInfo, OverriddenFunctionInformation> OverriddenFunctionInformation = new Dictionary<MethodInfo, OverriddenFunctionInformation>();

		public ProjectBase()
		{
			Functions = new List<FunctionInfo>();
		}

		public IEnumerable<ApiExtensionMethod> ApiExtensions
		{
			get { return ApiExtensionMethods.Values; }
			set
			{
				ApiExtensionMethods.Clear();
				foreach (var apiExt in value)
				{
					ApiExtensionMethods.Add(apiExt.ExtendedMethod, apiExt);
				}
			}
		}
		public string m_projectFileName = "";
		private bool m_isDirty;
		public event EventHandler DebugProjectFileChanged;

		/// <summary>
		/// The AAPROJ file used during debugging to get a real object model.
		/// </summary>
		public string DebugProjectFile
		{
			get { return _DebugProjectFile; }
			set
			{
				if (_DebugProjectFile == value) return;

				_DebugProjectFile = value;
				// Clear cached populated providers
				DebugProjectFileChanged.RaiseEvent(this);
			}
		}

		public ReadOnlyCollection<IncludedFile> IncludedFiles
		{
			get { return m_includedFiles.AsReadOnly(); }
		}

		public OutputFolder RootOutput
		{
			get
			{
				if (m_rootOutput == null)
				{
					m_rootOutput = new OutputFolder("ROOT");
				}
				return m_rootOutput;
			}
			set { m_rootOutput = value; }
		}

		[DotfuscatorDoNotRename]
		public string ProjectName
		{
			get { return m_projectName; }
			set
			{
				Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), m_projectName, value);
				m_projectName = value;
			}
		}

		[DotfuscatorDoNotRename]
		public string ProjectDescription
		{
			get { return m_projectDescription; }
			set
			{
				Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), m_projectDescription, value);
				m_projectDescription = value;
			}
		}

		public string CompileFolderName
		{
			get
			{
				return m_compileFolderName;
			}
			set
			{
				Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), m_compileFolderName, value);
				m_compileFolderName = value;
			}
		}

		public string Version
		{
			get { return m_version; }
			set
			{
				Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), m_version, value);
				m_version = value;
			}
		}

		public ReadOnlyCollection<UserOption> UserOptions
		{
			get { return m_userOptions.AsReadOnly(); }
		}

		public ReadOnlyCollection<ReferencedFile> References
		{
			get
			{

				if (m_references == null)
				{
					m_references = new List<ReferencedFile>();
				}
				foreach (ReferencedFile reference in m_references)
				{
					string directory = Path.GetDirectoryName(reference.FileName);
					SharedData.AddAssemblySearchPath(directory);
				}
				return m_references.AsReadOnly();
			}
		}

		public void ClearOverriddenFunctionInformation()
		{
			OverriddenFunctionInformation.Clear();
		}

		protected virtual void CheckReferences()
		{
		}

		public void ClearReferences()
		{
			m_references.Clear();
		}

		public List<string> Namespaces
		{
			get
			{
				if (m_namespaces != null && m_namespaces.Count == NamespaceCount)
				{
					return m_namespaces;
				}
				// Make sure that System, System.Xml and System.Text are included
				bool systemXmlFound = false;
				bool systemFound = false;
				bool systemTextFound = false;
				int numBlanks = 0;

				if (m_namespaces == null)
				{
					m_namespaces = new List<string>();
				}
				foreach (string ns in m_namespaces)
				{
					if (string.IsNullOrEmpty(ns)) { numBlanks++; }
					if (Utility.StringsAreEqual(ns, "system.xml", false)) { systemXmlFound = true; }
					if (Utility.StringsAreEqual(ns, "system", false)) { systemFound = true; }
					if (Utility.StringsAreEqual(ns, "system.text", false)) { systemTextFound = true; }
				}
				// Ensure that System.Xml is always included
				int numToAdd = !systemXmlFound ? 1 : 0;
				numToAdd += !systemFound ? 1 : 0;
				numToAdd += !systemTextFound ? 1 : 0;

				if (numToAdd > 0 || numBlanks > 0)
				{
					List<string> tempNamespaces = new List<string>();

					for (int i = 0; i < m_namespaces.Count; i++)
					{
						if (!string.IsNullOrEmpty(m_namespaces[i]) && m_namespaces[i].Trim().Length > 0)
						{
							tempNamespaces.Add(m_namespaces[i]);
						}
					}
					if (!systemXmlFound)
					{
						tempNamespaces.Insert(0, "System.Xml");
					}
					if (!systemFound)
					{
						tempNamespaces.Insert(0, "System");
					}
					if (!systemTextFound)
					{
						tempNamespaces.Insert(0, "System.Text");
					}
					m_namespaces = tempNamespaces;
				}
				// Remove duplicates
				string prevNamespace = "";
				m_namespaces.Sort();

				for (int i = m_namespaces.Count - 1; i >= 0; i--)
				{
					if (m_namespaces[i] == prevNamespace)
					{
						m_namespaces.RemoveAt(i);
					}
					else
					{
						prevNamespace = m_namespaces[i];
					}
				}
				NamespaceCount = m_namespaces.Count;
				return m_namespaces;
			}
			protected set { m_namespaces = value; }
		}

		public List<FunctionInfo> Functions { get; protected set; }

		public ProjectTypes ProjType
		{
			get
			{
				if (m_projectType == ProjectTypes.None)
				{
					m_projectType = ProjectTypes.Template;
				}
				return m_projectType;
			}
			set
			{
				m_projectType = value;
			}
		}

		public string TestGenerateDirectory
		{
			get
			{
				if (string.IsNullOrEmpty(_TestGenerateDirectory))
				{
					// If no directory has been specified, then create a default one in the temp folder.
					_TestGenerateDirectory = Path.Combine(Path.GetTempPath(), "ArchAngel_Temp_Designer_Gen");
					Utility.DeleteDirectoryBrute(_TestGenerateDirectory);
					Directory.CreateDirectory(_TestGenerateDirectory);
				}
				return _TestGenerateDirectory;
			}
			set { _TestGenerateDirectory = value; }
		}

		public bool IsDirty
		{
			get { return m_isDirty; }
			set
			{
				// This should get refactored into an event IsDirtyChanged
				// and an event handler on Controller.                
				if (m_isDirty != value)
				{
					m_isDirty = value;

					if (m_isDirty)
					{
					}

					IsDirtyChanged.RaiseEvent(this);
				}
			}
		}

		public string ProjectFileName
		{
			get
			{
				if (m_projectFileName.Length == 0 && Directory.Exists(CompileFolderName) && ProjectName.Length > 0)
				{
					m_projectFileName = Path.Combine(CompileFolderName, ProjectName + ".stz");
				}
				return m_projectFileName;
			}
			set
			{
				Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), m_projectFileName, value);
				m_projectFileName = value;
			}
		}

		public string GetPathRelativeToProjectFile(string absolutePath)
		{
			return RelativePaths.GetRelativePath(Path.GetDirectoryName(ProjectFileName), absolutePath);
		}

		public string GetFullPath(string pathRelativeToProjectFile)
		{
			return RelativePaths.GetFullPath(ProjectFileName, pathRelativeToProjectFile);
		}

		public void AddReferencedFile(ReferencedFile reference)
		{
			bool found = false;

			for (int i = 0; i < m_references.Count(); i++)
			{
				if (m_references[i].FileName == reference.FileName)
				{
					found = true;
					break;
				}
			}
			if (!found)
			{
				m_references.Add(reference);

				ReferencedFilesModified();
				Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, reference);
			}
		}

		public void SetReferencedFiles(IEnumerable<ReferencedFile> files)
		{
			bool difference = false;

			if (files.Count() == m_references.Count)
			{
				var tempFiles = files.OrderBy(f => f.AssemblyName).Zip(m_references.OrderBy(f => f.AssemblyName));

				foreach (var fileComp in tempFiles)
				{
					if (fileComp.Key.Equals(fileComp.Value.AssemblyName) == false)
					{
						difference = true;
						break;
					}
				}
			}
			else
			{
				difference = true;
			}

			if (difference)
			{
				m_references.Clear();
				m_references.AddRange(files);
				ReferencedFilesModified();
				RecalculateReferencedAssemblies();
			}
		}

		public void RemoveReference(ReferencedFile file)
		{
			m_references.Remove(file);
			ReferencedFilesModified();
			Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), file, null);
		}

		protected virtual void ReferencedFilesModified()
		{
			IsDirty = true;
			RefreshReferencedAssemblies = true;
			ReferencedAssembliesChanged.RaiseEvent(this);
		}

		public void ClearIncludedFiles()
		{
			m_includedFiles.Clear();
		}

		public void AddIncludedFile(IncludedFile file)
		{
			m_includedFiles.Add(file);
		}

		public void AddIncludedFiles(IEnumerable<IncludedFile> files)
		{
			m_includedFiles.AddRange(files);
		}

		public void AddOverriddenFunctionInformation(IEnumerable<OverriddenFunctionInformation> informations)
		{
			foreach (var info in informations)
			{
				MethodInfo mi = info.Type.GetMethod(info.MethodName);
				OverriddenFunctionInformation.Add(mi, info);

				if (ApiExtensionMethods.ContainsKey(mi))
				{
					ApiExtensionMethods[mi].DefaultCode = info.BodyText;
				}
			}
		}

		public string GetDefaultFunctionBodyFor(MethodInfo method)
		{
			OverriddenFunctionInformation ofi;

			return OverriddenFunctionInformation.TryGetValue(method, out ofi) ? ofi.BodyText : "";
		}

		/// <summary>
		/// Gets a list of all files in the entire hierarchy.
		/// </summary>
		/// <returns></returns>
		public List<OutputFile> GetAllFiles()
		{
			List<OutputFile> files = new List<OutputFile>(RootOutput.Files.Count);

			foreach (OutputFile file in RootOutput.Files)
			{
				files.Add(file);
			}
			foreach (OutputFolder folder in RootOutput.Folders)
			{
				files.AddRange(GetAllFilesInFolder(folder));
			}
			return files;
		}

		/// <summary>
		/// Gets a list of all the files in the folder and its sub-folders.
		/// </summary>
		/// <param name="folder"></param>
		/// <returns></returns>
		private static List<OutputFile> GetAllFilesInFolder(OutputFolder folder)
		{
			List<OutputFile> files = new List<OutputFile>(folder.Files.Count);

			foreach (OutputFile file in folder.Files)
			{
				files.Add(file);
			}
			foreach (OutputFolder subFolder in folder.Folders)
			{
				files.AddRange(GetAllFilesInFolder(subFolder));
			}
			return files;
		}

		/// <summary>
		/// Gets a list of all folders in the entire hierarchy.
		/// </summary>
		/// <returns></returns>
		public List<OutputFolder> GetAllFolders()
		{
			List<OutputFolder> folders = new List<OutputFolder>(RootOutput.Folders.Count);

			foreach (OutputFolder folder in RootOutput.Folders)
			{
				folders.Add(folder);
				folders.AddRange(GetAllSubFolders(folder));
			}
			return folders;
		}

		/// <summary>
		/// Gets a list of all the sub-folders in the folder.
		/// </summary>
		/// <param name="folder"></param>
		/// <returns></returns>
		private static List<OutputFolder> GetAllSubFolders(OutputFolder folder)
		{
			List<OutputFolder> folders = new List<OutputFolder>(folder.Folders.Count);

			foreach (OutputFolder subFolder in folder.Folders)
			{
				folders.Add(subFolder);
				folders.AddRange(GetAllSubFolders(subFolder));
			}
			return folders;
		}

		public void SetupDynamicfilesAndFolders()
		{
			foreach (OutputFile file in GetAllFiles())
			{
				if (string.IsNullOrEmpty(file.ScriptName)) continue;

				FunctionInfo function = FindFunctionSingle(file.ScriptName);

				if (function != null)
				{
					file.IteratorFunction = function;
				}
			}
		}

		private void RecalculateReferencedAssemblies()
		{
			m_referencedAssemblies.Clear();
			foreach (var refFile in References)
			{
				if (File.Exists(refFile.FileName))
				{
					Assembly assembly = Assembly.Load(refFile.AssemblyName);
					m_referencedAssemblies.Add(assembly);
					refFile.IsProvider = ProviderInfo.IsProvider(assembly);
				}
			}
			RefreshReferencedAssemblies = false;
			ReferencedAssembliesChanged.RaiseEvent(this);
		}

		/// <summary>
		/// Gets an array of assemblies that are referenced by this project
		/// </summary>
		public ReadOnlyCollection<Assembly> ReferencedAssemblies
		{
			get
			{
				if (m_referencedAssemblies.Count != m_references.Count)
					RefreshReferencedAssemblies = true;

				if (RefreshReferencedAssemblies)
				{
					m_referencedAssemblies.Clear();

					foreach (ReferencedFile refFile in References)
					{
						Assembly assembly = Assembly.Load(refFile.AssemblyName);
						m_referencedAssemblies.Add(assembly);
					}
					RefreshReferencedAssemblies = false;
				}
				return m_referencedAssemblies.AsReadOnly();
			}
		}

		public void AddNamespace(string nameSpace)
		{
			if (Namespaces.BinarySearch(nameSpace) < 0)
			{
				Namespaces.Add(nameSpace);
				Namespaces.Sort();
				IsDirty = true;
				Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, nameSpace);
			}
		}

		/// <summary>
		/// Finds a function where only one function with this name exists. Throws an exception if 
		/// more than one function exists with a matching name. Only 'ScriptFunctions' should meet
		/// this criteria. API Extension 'overrides' do not meet this criteria.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public FunctionInfo FindFunctionSingle(string name)
		{
			List<FunctionInfo> foundFunctions = FindFunctions(name);

			switch (foundFunctions.Count)
			{
				case 0:
					return null;
				case 1:
					return foundFunctions[0];
				default: // multiple
					throw new InvalidOperationException("Multiple functions found. FindFunctionSingle should only be called when only a single function with the same name can exist.");
			}
		}

		public FunctionInfo FindFunction(string name, List<ParameterInfo> parameters)
		{
			List<ParamInfo> parameters2 = new List<ParamInfo>(parameters.Count);

			for (int i = 0; i < parameters.Count; i++)
			{
				parameters2.Add(new ParamInfo(parameters[i].Name, parameters[i].ParameterType));
			}
			return FindFunction(name, parameters2);
		}

		public FunctionInfo FindFunction(string name, List<ParamInfo> parameters)
		{
			if (name == null)
			{
				return null;
			}
			name = name.Replace("*", "").Trim().ToLower();

			for (int i = 0; i < Functions.Count; i++)
			{
				if (Utility.StringsAreEqual(Functions[i].Name.ToLower(), name, true) && Functions[i].Parameters.Count == parameters.Count)
				{
					bool found = true;

					for (int paramCounter = 0; paramCounter < Functions[i].Parameters.Count; paramCounter++)
					{
						if (Functions[i].Parameters[paramCounter].DataType.FullName != parameters[paramCounter].DataType.FullName)
						{
							found = false;
							break;
						}
					}
					if (found)
					{
						return Functions[i];
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Gets all functions with the given name. Multiple overloaded functions can be returned.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public List<FunctionInfo> FindFunctions(string name)
		{
			List<FunctionInfo> results = new List<FunctionInfo>();

			if (name == null)
			{
				return results;
			}
			name = name.Replace("*", "").Trim().ToLower();

			for (int i = 0; i < Functions.Count; i++)
			{
				if (Utility.StringsAreEqual(Functions[i].Name.ToLower(), name, true))
				{
					results.Add(Functions[i]);
				}
			}
			return results;
		}

		public string GetCompiledDLLPath()
		{
			return GetPathRelativeToProjectFile(Path.Combine(CompileFolderName, ProjectName + ".AAT.DLL"));
		}

		public string GetCompiledDLLDirectory()
		{
			return GetPathRelativeToProjectFile(CompileFolderName);
		}

		/// <summary>
		/// This event is fired whenever IsDirty changes.
		/// </summary>
		public event EventHandler IsDirtyChanged;
		public event EventHandler ReferencedAssembliesChanged;

		public UserOption FindUserOption(string name)
		{
			int index;
			return FindUserOption(name, out index);
		}

		public UserOption FindUserOption(string name, out int index)
		{
			index = -1;
			name = name.ToLower();

			for (int i = 0; i < UserOptions.Count; i++)
			{
				if (Utility.StringsAreEqual(UserOptions[i].VariableName, name, false))
				{
					index = i;
					return UserOptions[i];
				}
			}
			return null;
		}

		/// <summary>
		/// Adds the option if it doesn't already exist
		/// </summary>
		/// <param name="option"></param>
		public void AddUserOption(UserOption option)
		{
			int index;
			UserOption currentUserOption = FindUserOption(option.VariableName, out index);

			if (currentUserOption == null)
			{
				m_userOptions.Add(option);
				SortUserOptions();
				IsDirty = true;
			}
			else
			{
				m_userOptions[index] = option;
			}
		}

		public void SetUserOptions(IEnumerable<UserOption> userOptions)
		{
			m_userOptions.Clear();
			m_userOptions.AddRange(userOptions);
		}

		public void ClearUserOptions()
		{
			m_userOptions.Clear();
		}

		public void RemoveUserOption(UserOption option)
		{
			m_userOptions.Remove(option);
		}

		/// <summary>
		/// Sorts the user options into alphabetical order
		/// </summary>
		public void SortUserOptions()
		{
			Comparers.UserOptionComparer comparer = new Comparers.UserOptionComparer();
			m_userOptions.Sort(comparer);
		}

		public string GetRelativeDebugProjectFile()
		{
			return RelativePaths.GetRelativePath(Path.GetDirectoryName(ProjectFileName), DebugProjectFile);
		}
	}
}