using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using ActiproSoftware.SyntaxEditor;
using ArchAngel.Common.DesignerProject;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Designer.Exceptions;
using ArchAngel.Designer.Properties;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Attributes;
using ArchAngel.Providers.CodeProvider.DotNet;
using Slyce.Common;
using DefaultValueFunction = ArchAngel.Designer.DesignerProject.DefaultValueFunction;
using UserOption = ArchAngel.Common.DesignerProject.UserOption;

namespace ArchAngel.Designer
{
	[Serializable]
	public class Project : ProjectBase
	{
		#region Internal fields
		/* =====================================
		 * Project File Storage Version History
		 * =====================================
		 * 1) Initial
		 * 2) Changed UserOptions to store their own function body for DefaultValue
		 * 
		 */
		private static Project instance = new Project();
		private readonly List<string> m_outputNames = new List<string>();
		private string m_templateNamespace = "";
		private string m_projectXmlConfig = "";
		public Constant[] Constants = new Constant[0];
		public List<DefaultValueFunction> m_defaultValueFunctions = new List<DefaultValueFunction>();
		public TemplateContentLanguage TextLanguage = TemplateContentLanguage.CSharp;
		public SyntaxEditorHelper.ScriptLanguageTypes CodeLanguage = SyntaxEditorHelper.ScriptLanguageTypes.CSharp;
		private string m_scriptFunctionsFile = "";
		private string m_templateFile = "";
		private string m_targetFile = "";
		private Type[] m_allowedParameters = null;
		protected static bool AlertUserAboutMissingReferences = true;
		private bool ResetTemplateEnumCache = true;
		private IEnumerable<Type> templateEnumTypesCache;

		#endregion

		/// <summary>
		/// This event is fired whenever the Project instance is changed (Clear() or Open() is called).
		/// </summary>
		[field: NonSerialized]
		public event EventHandler<ProjectOpenedEventArgs> ProjectModified;

		public class ProjectOpenedEventArgs : EventArgs
		{
			public Project OpenedProject = null;
			public bool isNewProject = false;

			public ProjectOpenedEventArgs()
			{
			}

			public ProjectOpenedEventArgs(Project openedProject, bool isNewProject)
			{
				OpenedProject = openedProject;
				this.isNewProject = isNewProject;
			}
		}

		private Project()
		{
			Functions = new List<FunctionInfo>();
		}

		/// <summary>
		/// This constructor should only be used by TemplateSync - to create 'their' Project to import.
		/// </summary>
		/// <param name="fileName"></param>
		internal Project(string fileName)
		{
			Functions = new List<FunctionInfo>();
			Open(fileName);
		}

		public List<string> FunctionCategories
		{
			get
			{
				List<string> specialFunctions = InternalFunctionNames;
				List<string> categories = new List<string>();

				foreach (FunctionInfo func in Functions)
				{
					if (categories.BinarySearch(func.Category) < 0 && specialFunctions.BinarySearch(func.Name) < 0)
					{
						categories.Add(func.Category);
						categories.Sort();
					}
				}
				return categories;
			}
		}

		protected override void ReferencedFilesModified()
		{
			base.ReferencedFilesModified();
			ResetTemplateEnumCache = true;
		}

		/// <summary>
		/// Gets a sorted list of 'special' functions (defaultvalue functions etc) that the user shouldn't have direct access to.
		/// </summary>
		public List<string> InternalFunctionNames
		{
			get
			{
				List<string> specialFunctions = new List<string>();

				//foreach (UserOption userOption in Instance.UserOptions)
				//{
				//    specialFunctions.Add(userOption.DefaultValue);
				//    specialFunctions.Add(userOption.DisplayToUserFunction);
				//    specialFunctions.Add(userOption.ValidatorFunction);
				//}
				foreach (DefaultValueFunction defaultValueFunction in Instance.DefaultValueFunctions)
				{
					if (defaultValueFunction.IsForUserOption)
					{
						specialFunctions.Add(defaultValueFunction.FunctionName);
					}
				}
				foreach (MethodInfo method in GetExtensibleMethods())
				{
					string functionName = method.ReflectedType.FullName.Replace("+", ".") + "." + method.Name;
					functionName = functionName.Replace(".", "_");

					if (Instance.FindFunction(functionName, method.GetParameters().ToList()) != null)
					{
						specialFunctions.Add(functionName);
					}
				}
				specialFunctions.Sort();
				return specialFunctions;
			}
		}

		private List<MethodInfo> GetExtensibleMethods()
		{
			List<MethodInfo> methods = new List<MethodInfo>();
			Type extensionAttributeType = typeof(ApiExtensionAttribute);

			foreach (Assembly assembly in Instance.ReferencedAssemblies)
			{
				foreach (Type type in assembly.GetTypes())
				{
					foreach (MethodInfo method in type.GetMethods())
					{
						if (method.GetCustomAttributes(extensionAttributeType, false).Length > 0)
						{
							methods.Add(method);
						}
					}
				}
			}
			return methods;
		}

		public static Project GetNewTempProject()
		{
			return new Project();
		}

		public static Project Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Project();
				}
				return instance;
			}
		}

		/// <summary>
		/// Open project file.
		/// </summary>
		/// <param name="filePath"></param>
		public void Open(string filePath)
		{
			ProjectFileName = filePath;
			if (Path.GetExtension(filePath) == ".stz")
			{
				ProjectLoadAndSaveHelper.ReadFromXml(this, filePath);
			}
			else
			{
				ProjectDeserialiserV1 deserialiserV1 = new ProjectDeserialiserV1(new FileController());
				deserialiserV1.LoadProject(filePath, this);

				foreach (var provider in ReferencedAssemblies.Where(ProviderInfo.IsProvider))
				{
					var resourceName = provider.GetName().Name + ".FunctionInfo.xml";
					if (provider.GetManifestResourceInfo(resourceName) != null)
					{
						using (Stream funcInfo = provider.GetManifestResourceStream(resourceName))
							if (funcInfo != null)
								using (StreamReader reader = new StreamReader(funcInfo))
								{
									XmlDocument doc = new XmlDocument();
									var xml = reader.ReadToEnd();
									doc.LoadXml(xml);

									deserialiserV1.LoadDefaultApiFunctionBodies(doc.DocumentElement, this);
								}
					}
				}
			}
			IsDirty = false;
			TriggerProjectChangedEvent(false);
		}

		private void TriggerProjectChangedEvent(bool isNewProject)
		{
			if (ProjectModified != null)
			{
				ProjectModified(this, new ProjectOpenedEventArgs(this, isNewProject));
			}
		}

		public DefaultValueFunction FindDefaultValueFunction(string name, List<ParamInfo> parameters)
		{
			if (string.IsNullOrEmpty(name)) { return null; }
			name = name.Replace(".", "_").ToLower();

			foreach (DefaultValueFunction func in DefaultValueFunctions)
			{
				if (Utility.StringsAreEqual(func.FunctionName.ToLower(), name, true) && func.ParameterTypes.Count == parameters.Count)
				{
					bool found = true;

					for (int i = 0; i < func.ParameterTypes.Count; i++)
					{
						if (func.ParameterTypes[i].DataType.FullName != parameters[i].DataType.FullName)
						{
							found = false;
							break;
						}
					}
					if (found)
					{
						return func;
					}
				}
			}
			return null;
		}

		public DefaultValueFunction FindDefaultValueFunction(string name, List<ParameterInfo> parameters)
		{
			if (string.IsNullOrEmpty(name)) { return null; }
			name = name.Replace(".", "_").ToLower();

			foreach (DefaultValueFunction func in DefaultValueFunctions)
			{
				if (Utility.StringsAreEqual(func.FunctionName.ToLower(), name, true) && func.ParameterTypes.Count == parameters.Count)
				{
					bool found = true;

					for (int i = 0; i < func.ParameterTypes.Count; i++)
					{
						if (func.ParameterTypes[i].DataType.FullName != parameters[i].ParameterType.FullName)
						{
							found = false;
							break;
						}
					}
					if (found)
					{
						return func;
					}
				}
			}
			return null;
		}

		public Constant FindConstant(string name)
		{
			name = name.ToLower();

			for (int i = 0; i < Constants.Length; i++)
			{
				if (Utility.StringsAreEqual(Constants[i].Name, name, false))
				{
					return Constants[i];
				}
			}
			return null;
		}

		public string FindNamespace(string name)
		{
			name = name.ToLower();

			for (int i = 0; i < Namespaces.Count; i++)
			{
				if (Utility.StringsAreEqual(Namespaces[i], name, false))
				{
					return Namespaces[i];
				}
			}
			return null;
		}

		/// <summary>
		/// Processes the project and ensures that it is in a valid state.
		/// </summary>
		public List<string> VerifyProjectCorrectness()
		{
			List<string> results = new List<string>();

			// Ensure that all FileOutputs are associated with a function
			foreach (var file in GetAllFiles())
			{
				if (!string.IsNullOrEmpty(file.ScriptName) && FindFunctionSingle(file.ScriptName) == null)
				{
					results.Add((string.Format("The function [{0}] specified for the file [{1}] doesn't exist.", file.ScriptName, file.Name)));
				}
			}
			// Check for duplicate Functions

			// Check for duplicate DefaultValueFunctions

			// Check that each DefaultValueFunction has a corresponding Function
			for (int i = DefaultValueFunctions.Count - 1; i >= 0; i--)
			{
				//if (DefaultValueFunctions[i].i
			}

			VerifyReferencedFiles();

			return results;
		}

		private void VerifyReferencedFiles()
		{
			int numBlanks = 0;
			foreach (ReferencedFile refFile in m_references)
			{
				if (refFile == null || refFile.FileName.Trim().ToLower().Length == 0) { numBlanks++; }
			}
			for (int i = 0; i < m_references.Count; i++)
			{
				// Make sure we only have one copy of ArchAngel.Interfaces.dll, and it is the copy in the installation folder
				if (Path.GetFileName(m_references[i].FileName).ToLower() == "archangel.interfaces.dll" && Path.GetDirectoryName(m_references[i].FileName).ToLower() != Path.GetDirectoryName(Application.ExecutablePath).ToLower())
				{
					m_references.RemoveAt(i);
					break;
				}
			}
			// Ensure that System.Xml is always included

			if (numBlanks > 0)
			{
				List<ReferencedFile> tempRefFiles = new List<ReferencedFile>(m_references.Count - numBlanks);
				int insertPos = 0;

				for (int i = 0; i < m_references.Count; i++)
				{
					if (m_references[i] != null && m_references[i].FileName.Trim().Length > 0)
					{
						tempRefFiles[insertPos] = new ReferencedFile(m_references[i].FileName, m_references[i].MergeWithAssembly, m_references[i].UseInWorkbench);
						insertPos++;
					}
				}
				// Reset the insertPos
				m_references = tempRefFiles;
			}
			// Make sure required files are referenced
			bool interfaceAssemblyFound = false;

			for (int i = 0; i < m_references.Count; i++)
			{
				if (m_references[i].FileName.ToLower().IndexOf("archangel.interfaces.dll") >= 0)
				{
					interfaceAssemblyFound = true;
				}
			}
			if (!interfaceAssemblyFound)
			{
				string installDir = Path.GetDirectoryName(Application.ExecutablePath);
				string interfaceAssemblyPath = Path.Combine(installDir, "ArchAngel.Interfaces.dll");

				if (!File.Exists(interfaceAssemblyPath))
				{
					string message = "ArchAngel.Interfaces.dll is missing from the ArchAngel folder (" + installDir + ").\n\nPlease repair ArchAngel via the Control Panel -> Add/Remove Programs.";
					MessageBox.Show(Controller.Instance.MainForm, message, "Missing File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				m_references.Add(new ReferencedFile(interfaceAssemblyPath, false, false));
				m_referencedAssemblies.Clear();
				RefreshReferencedAssemblies = true;
			}
		}

		/// <summary>
		/// Saves the project to an xml definition file
		/// </summary>
		public void SaveToXml(string filePath)
		{
			// This is how to save the old project file format.
			//ProjectLoadAndSaveHelper.SaveToXml(filePath, this);
			if (ProjectName != Path.GetFileNameWithoutExtension(filePath))
				ProjectName = Path.GetFileNameWithoutExtension(filePath);

			IProjectSerialiser serialiser = new ProjectSerialiserV1(new FileController());
			serialiser.WriteProjectToDisk(this, Path.GetDirectoryName(filePath));
		}

		public string GetProjectXml()
		{
			IProjectSerialiser serialiser = new ProjectSerialiserV1(new FileController());
			ProjectXmlConfig = serialiser.WriteProjectToSingleXmlFile(this);
			return ProjectXmlConfig;
		}

		//private static void CreateDefaultValueFunctionsElement(Project project, XmlElement projectElement)
		//{
		//    XmlElement defaultValueFunctionsElement = AppendNewXmlElement(projectElement, "defaultvaluefunctions", "");

		//    //foreach (DefaultValueFunction defaultValueFunction in project.DefaultValueFunctions)
		//    for (int defValFuncCounter = project.DefaultValueFunctions.Count - 1; defValFuncCounter >= 0; defValFuncCounter--)
		//    {
		//        DefaultValueFunction defaultValueFunction = project.DefaultValueFunctions[defValFuncCounter];

		//        // Clean up unnecessary DefaultValueFunctions whose functions have been deleted
		//        if (project.FindFunction(defaultValueFunction.FunctionName, defaultValueFunction.ParameterTypes) == null)
		//        {
		//            project.DefaultValueFunctions.RemoveAt(defValFuncCounter);
		//            continue;
		//        }
		//        XmlElement defaultValueFunctionElement = AppendNewXmlElement(defaultValueFunctionsElement, "defaultvaluefunction", "");
		//        AppendNewXmlElement(defaultValueFunctionElement, "objecttype", defaultValueFunction.ObjectType.FullName);
		//        AppendNewXmlElement(defaultValueFunctionElement, "propertyname", defaultValueFunction.PropertyName);
		//        AppendNewXmlElement(defaultValueFunctionElement, "usecustomcode", defaultValueFunction.UseCustomCode.ToString());
		//        AppendNewXmlElement(defaultValueFunctionElement, "functiontype", defaultValueFunction.FunctionType.ToString());
		//        AppendNewXmlElement(defaultValueFunctionElement, "isforuseroption", defaultValueFunction.IsForUserOption.ToString());
		//        XmlElement parametersNode = AppendNewXmlElement(defaultValueFunctionElement, "parametertypes", "");

		//        foreach (ParamInfo paraType in defaultValueFunction.ParameterTypes)
		//        {
		//            XmlElement parameterNode = AppendNewXmlElement(parametersNode, "parametertype", paraType.DataType.FullName);
		//            AppendNewAttribute(parameterNode, "modifiers", paraType.Modifiers);
		//        }
		//    }
		//}

		public bool HasVbFunctions()
		{
			foreach (FunctionInfo func in Functions)
			{
				if (func.ScriptLanguage == SyntaxEditorHelper.ScriptLanguageTypes.VbNet)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Extracts the template definition from an existing AAL file and creates a new project.
		/// </summary>
		/// <param name="compiledFile"></param>
		/// <param name="destinationFile"></param>
		public static void ExtractTemplateFromCompiledTemplate(string compiledFile, string destinationFile)
		{
			string tempFolder = Path.Combine(Controller.TempPath, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));

			if (!Directory.Exists(Controller.TempPath))
			{
				Directory.CreateDirectory(Controller.TempPath);
			}
			if (!Directory.Exists(tempFolder))
			{
				Directory.CreateDirectory(tempFolder);
			}
			Assembly assembly = Assembly.ReflectionOnlyLoadFrom(compiledFile);
			List<string> files = new List<string>();

			foreach (string resourceName in assembly.GetManifestResourceNames())
			{
				string file = Path.Combine(tempFolder, resourceName);

				if (Utility.StringsAreEqual(resourceName, "options.xml", false))
				{
					file = Path.Combine(tempFolder, "definition.xml");
				}
				files.Add(file);
				Utility.WriteStreamToFile(assembly.GetManifestResourceStream(resourceName), file);
			}
			Utility.ZipFile(files, destinationFile);
		}

		internal Type[] AllowedScriptParameters
		{
			get
			{
				if (m_allowedParameters == null)
				{
					List<Type> arr = new List<Type>();

					foreach (ReferencedFile referencedFile in References)
					{
						Assembly dll = Assembly.Load(referencedFile.AssemblyName);

						if (dll == null) { continue; }

						foreach (Type t in dll.GetTypes())
						{
							object[] allAttributes = t.GetCustomAttributes(false);

							foreach (object att in allAttributes)
							{
								Type attType = att.GetType();

								if (Utility.StringsAreEqual(attType.Name, ExtensionAttributeHelper.ArchAngelEditorAttributeName, true))
								{
									arr.Add(t);
									break;
								}
							}
						}
					}
					m_allowedParameters = arr.ToArray();
				}
				return m_allowedParameters;
			}
			set { m_allowedParameters = value; }
		}

		protected override void CheckReferences()
		{
			ArrayList arrValid = new ArrayList();
			ArrayList arrInvalid = new ArrayList();
			List<ReferencedFile> origReferences = m_references;
			string programDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().CodeBase.Replace(@"file:///", ""));

			// Make sure all references can be found
			for (int i = 0; i < m_references.Count; i++)
			{
				// If any file can be found in the program folder, use that version
				string fileName = Path.GetFileName(m_references[i].FileName);

				if (File.Exists(Path.Combine(programDir, fileName)))
					m_references[i].FileName = Path.Combine(programDir, fileName);

				string file = m_references[i].FileName;
				string filePath = GetFullPath(file);

				if (File.Exists(filePath) ||
					Utility.StringsAreEqual(file, "system.dll", false) ||
					Utility.StringsAreEqual(file, "mscorlib.dll", false))
				{
					arrValid.Add(file);
				}
				else
				{
					// Let's see if we can find the file in other common paths.
					string filename = Path.GetFileName(file);
					string pathToCheck = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), filename);

					if (File.Exists(pathToCheck))
					{
						m_references[i].FileName = pathToCheck;
						arrValid.Add(m_references[i].FileName);
					}
					else
					{
						if (!string.IsNullOrEmpty(file))
						{
							string newLocation = LocateFile(file);

							if (!string.IsNullOrEmpty(newLocation))
							{
								m_references[i].FileName = newLocation;
								arrValid.Add(m_references[i].FileName);
							}
							else
							{
								arrInvalid.Add(file);
							}
						}
						else
						{
							// Try the AAL directory
							pathToCheck = Path.Combine(Path.GetDirectoryName(ProjectFileName), filename);

							if (File.Exists(pathToCheck))
							{
								m_references[i].FileName = pathToCheck;
								arrValid.Add(m_references[i].FileName);
							}
							else
							{
								if (!string.IsNullOrEmpty(file))
								{
									string newLocation = LocateFile(file);

									if (!string.IsNullOrEmpty(newLocation))
									{
										m_references[i].FileName = newLocation;
										arrValid.Add(m_references[i].FileName);
									}
									else
									{
										arrInvalid.Add(file);
									}
								}
							}
						}
					}
				}
			}
			string message = "The following referenced files could not be found. Add them again in the Settings screen:" + Environment.NewLine;

			for (int i = 0; i < arrInvalid.Count; i++)
			{
				string invalidFile = (string)arrInvalid[i];
				message += string.Format("\n{0}", Path.GetFileName(invalidFile));
			}
			if (arrInvalid.Count > 0 && AlertUserAboutMissingReferences)
			{
				MessageBox.Show(Controller.Instance.MainForm, message, "Missing References", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			m_references = origReferences;

			// Remove duplicates
			int numDuplicates = 0;

			for (int i = 0; i < m_references.Count; i++)
			{
				for (int x = i + 1; x < m_references.Count; x++)
				{
					if (Utility.StringsAreEqual(m_references[i].FileName, m_references[x].FileName, false))
					{
						numDuplicates++;
						m_references[x].FileName = "";
					}
				}
			}
			List<ReferencedFile> nonDuplicates = new List<ReferencedFile>();//m_references.Count - numDuplicates);

			for (int i = 0; i < m_references.Count; i++)
			{
				if (m_references[i].FileName.Length > 0)
				{
					nonDuplicates.Add(new ReferencedFile(m_references[i].FileName, m_references[i].MergeWithAssembly, m_references[i].UseInWorkbench));
				}
			}
			m_references = nonDuplicates;
		}

		protected string LocateFile(string missingFilename)
		{
			Program.HideSplashScreen();
			if (MessageBox.Show(Controller.Instance.MainForm, string.Format("{0} is missing. You need to locate it, otherwise all references to it will be removed. Do you want to locate it now?", missingFilename), "Missing File", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				OpenFileDialog dialog = new OpenFileDialog();
				dialog.FileName = Path.GetFileName(missingFilename);
				string dir = Path.GetDirectoryName(missingFilename);

				if (Directory.Exists(dir))
				{
					dialog.InitialDirectory = dir;
				}
				if (dialog.ShowDialog() == DialogResult.OK && File.Exists(dialog.FileName))
				{
					return dialog.FileName;
				}
			}
			return "";
		}

		//private void ReadXmlDefaultValueFunctionNode(List<string> missingTypesMessages, XPathNavigator subNode)
		//{
		//    Type objectType = Instance.GetTypeFromReferencedAssemblies(subNode.SelectSingleNode("objecttype").Value, false);

		//    if (objectType == null)
		//    {
		//        missingTypesMessages.Add(string.Format("The type ({0}) cannot be located in any of the referenced files. It will be removed from the API Extensibility grid.", subNode.SelectSingleNode("objecttype").Value));
		//        return;
		//    }
		//    string propertyName = subNode.SelectSingleNode("propertyname").Value;
		//    bool userCustomCode = bool.Parse(subNode.SelectSingleNode("usecustomcode").Value);
		//    bool isForUserOption = subNode.SelectSingleNode("isforuseroption") != null ? bool.Parse(subNode.SelectSingleNode("isforuseroption").Value) : false;
		//    DefaultValueFunction.FunctionTypes functionType = subNode.SelectSingleNode("functiontype") == null ? DefaultValueFunction.FunctionTypes.DefaultValue : (DefaultValueFunction.FunctionTypes)Enum.Parse(typeof(DefaultValueFunction.FunctionTypes), subNode.SelectSingleNode("functiontype").Value, true);
		//    DefaultValueFunction defaultValueFunction = new DefaultValueFunction(objectType, propertyName, userCustomCode, functionType, isForUserOption);

		//    XPathNodeIterator paramTypeNodes = subNode.Select("parametertypes/parametertype");
		//    defaultValueFunction.ParameterTypes = new ParamInfo[paramTypeNodes.Count];
		//    int paramCounter = 0;

		//    foreach (XPathNavigator paramTypeNode in paramTypeNodes)
		//    {
		//        defaultValueFunction.ParameterTypes[paramCounter] = new ParamInfo("namedoesntmatter", Instance.GetTypeFromReferencedAssemblies(paramTypeNode.Value, true));
		//        defaultValueFunction.ParameterTypes[paramCounter].Modifiers = paramTypeNode.SelectSingleNode("@modifiers") == null ? "" : paramTypeNode.SelectSingleNode("@modifiers").Value;
		//        paramCounter++;
		//    }
		//    // Don't add duplicates which may appear in a corrupt file
		//    if (FindDefaultValueFunction(defaultValueFunction.FunctionName, defaultValueFunction.ParameterTypes) == null)
		//    {
		//        m_defaultValueFunctions.Add(defaultValueFunction);
		//    }
		//}

		public List<string> OutputNames
		{
			get
			{
				// If no names exist, add a default name
				//if (m_outputNames.Count == 0)
				//{
				//    m_outputNames.Add("Default");
				//}
				return m_outputNames;
			}
		}

		public void AddTopLevelFolder(OutputFolder folder)
		{
			RootOutput.Folders.Add(folder);
		}

		public void AddTopLevelFile(OutputFile file)
		{
			RootOutput.Files.Add(file);
		}

		public void AddFunction(FunctionInfo function)
		{
			FunctionInfo existingFunction = FindFunction(function.Name, function.Parameters);

			if (existingFunction == null)
			{
				Functions.Add(function);
				SortFunctions();
				Controller.Instance.MainForm.UcFunctions.PopulateFunctionList();
				IsDirty = true;
			}
		}

		public void DeleteFunction(FunctionInfo function)
		{
			DeleteFunction(function.Name, function.Parameters, true);
		}

		public void DeleteFunction(FunctionInfo function, bool updateScreens)
		{
			DeleteFunction(function.Name, function.Parameters, updateScreens);
		}

		public void DeleteFunction(string functionName, List<ParamInfo> parameters)
		{
			DeleteFunction(functionName, parameters, true);
		}

		public void DeleteFunction(string functionName, List<ParamInfo> parameters, bool updateScreens)
		{
			DefaultValueFunction defValFunc = Instance.FindDefaultValueFunction(functionName, parameters);

			if (defValFunc != null)
			{
				Instance.DefaultValueFunctions.Remove(defValFunc);
			}
			FunctionInfo existingFunction = FindFunction(functionName, parameters);

			if (existingFunction != null)
			{
				Functions.Remove(existingFunction);
				IsDirty = true;

				if (updateScreens)
				{
					Controller.Instance.MainForm.UcFunctions.PopulateFunctionList();
				}
			}
			else
			{
				throw new Exception("Couldn't find function.");
			}
		}

		public void DeleteUserOption(string variableName)
		{
			UserOption existingUserOption = FindUserOption(variableName);

			if (existingUserOption != null)
			{
				UserOption[] newUserOptions = new UserOption[UserOptions.Count - 1];
				int currentPos = 0;

				for (int i = 0; i < UserOptions.Count; i++)
				{
					UserOption uo = UserOptions[i];

					if (uo.VariableName != variableName)
					{
						newUserOptions[currentPos] = uo;
						currentPos++;
					}
				}
				m_userOptions = new List<UserOption>(newUserOptions);
				IsDirty = true;
				// Update the functions screen
				Controller.Instance.MainForm.UcFunctions.PopulateFunctionList();
			}
			else
			{
				throw new Exception("Couldn't find user option.");
			}
		}

		public void AddConstant(Constant con)
		{
			Constant existingConst = FindConstant(con.Name);

			if (existingConst == null)
			{
				Constant[] newConstants = new Constant[Constants.Length + 1];
				Array.Copy(Constants, newConstants, Constants.Length);
				newConstants[newConstants.Length - 1] = con;
				Constants = newConstants;
				SortConstants();
				IsDirty = true;
			}
			else
			{
				// Make sure the existing user option and the new user option match
				if (con != existingConst)
				{
					throw new Exception("A Constant with the same name as an existing Constant has attempted to be added, but it's values are different. Not added.");
				}
			}
		}

		public void AddAction(BaseAction action)
		{
			AddAction(action, Actions.Length);
		}

		public void SwapActionPositions(int index1, int index2)
		{
			BaseAction temp = Actions[index1];
			Actions[index1] = Actions[index2];
			Actions[index2] = temp;
		}

		public void AddAction(BaseAction action, int index)
		{
			BaseAction[] newActions = new BaseAction[Actions.Length + 1];
			Array.Copy(Actions, newActions, Actions.Length);

			for (int i = newActions.Length - 1; i > index; i--)
			{
				newActions[i] = newActions[i - 1];
			}
			newActions[index] = action;
			Actions = newActions;
			IsDirty = true;
		}

		public void RemoveAction(int index)
		{
			BaseAction[] newActions = new BaseAction[Actions.Length - 1];
			int newIndex = 0;

			for (int i = 0; i < Actions.Length; i++)
			{
				if (i == index)
				{
					continue;
				}
				newActions[newIndex] = Actions[i];
				newIndex++;
			}
			Actions = newActions;
			IsDirty = true;
		}

		//public void RemoveUserOption(string variableName)
		//{
		//    ArrayList tempArray = new ArrayList(UserOptions.Count);
		//    bool functionsRemoved = false;

		//    for (int i = 0; i < UserOptions.Count; i++)
		//    {
		//        if (UserOptions[i].VariableName != variableName)
		//        {
		//            tempArray.Add(UserOptions[i]);
		//        }
		//        else
		//        {
		//            DeleteFunction(UserOptions[i].DefaultValue, false);
		//            DeleteFunction(UserOptions[i].DisplayToUserFunction, false);
		//            DeleteFunction(UserOptions[i].ValidatorFunction, false);
		//            IsDirty = true;
		//            functionsRemoved = true;
		//        }
		//    }
		//    m_userOptions = new List<UserOption>((UserOption[])tempArray.ToArray(typeof(UserOption)));

		//    if (functionsRemoved)
		//    {
		//        Controller.Instance.MainForm.PopulateFunctionList();
		//    }
		//}

		/// <summary>
		/// Removes a folder from the collection.
		/// </summary>
		/// <param name="folderToRemove"></param>
		/// <returns>True if the folder was found and removed, false otherwise.</returns>
		public bool RemoveFolder(OutputFolder folderToRemove)
		{
			for (int i = 0; i < RootOutput.Folders.Count; i++)
			{
				OutputFolder folder = RootOutput.Folders[i];

				if (folder == folderToRemove)
				{
					RootOutput.RemoveFolder(folderToRemove);
					IsDirty = true;
					return true;
				}
				if (RemoveFolder(folderToRemove, folder))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Removes a folder from the collection.
		/// </summary>
		/// <param name="folderToRemove"></param>
		/// <param name="parentFolder"></param>
		/// <returns>True is folder was found and removed, false otherwise.</returns>
		public bool RemoveFolder(OutputFolder folderToRemove, OutputFolder parentFolder)
		{
			for (int i = 0; i < parentFolder.Folders.Count; i++)
			{
				OutputFolder folder = parentFolder.Folders[i];

				if (folder == folderToRemove)
				{
					parentFolder.RemoveFolder(folder);
					IsDirty = true;
					return true;
				}
				if (RemoveFolder(folderToRemove, folder))
				{
					return true;
				}
			}
			return false;
		}

		internal void RemoveNamespace(string nameSpace)
		{
			if (Namespaces.BinarySearch(nameSpace) >= 0)
			{
				Namespaces.Remove(nameSpace);
				IsDirty = true;
			}
		}

		internal void ReadXmlFolderNode(ref OutputFolder folder, XPathNavigator folderNode)
		{
			#region Add Files
			XPathNodeIterator fileNodes = folderNode.Select("file");

			foreach (XPathNavigator fileNode in fileNodes)
			{
				XPathNavigator idNode = fileNode.SelectSingleNode("@id");
				string id = idNode == null ? Guid.NewGuid().ToString() : idNode.Value;
				OutputFile file = new OutputFile(fileNode.SelectSingleNode("@name").Value, OutputFileTypes.File, "", id);

				file.StaticFileName = fileNode.SelectSingleNode("@staticfilename") == null ? file.Name : fileNode.SelectSingleNode("@staticfilename").Value;
				file.StaticFileIterator = null;

				if (fileNode.SelectSingleNode("@iteratorname") != null && !string.IsNullOrEmpty(fileNode.SelectSingleNode("@iteratorname").Value))
				{
					file.StaticFileIterator = GetTypeFromReferencedAssemblies(fileNode.SelectSingleNode("@iteratorname").Value, true);
				}
				folder.Files.Add(file);
			}
			#endregion

			#region Add Script Files
			fileNodes = folderNode.Select("script");

			foreach (XPathNavigator fileNode in fileNodes)
			{
				XPathNavigator idNode = fileNode.SelectSingleNode("@id");
				string id = idNode == null ? Guid.NewGuid().ToString() : idNode.Value;
				OutputFile file = new OutputFile(fileNode.SelectSingleNode("@filename").Value, OutputFileTypes.Script, fileNode.SelectSingleNode("@scriptname").Value, id);
				folder.Files.Add(file);
			}
			#endregion

			#region Process folders
			XPathNodeIterator subFolderNodes = folderNode.Select("folder");

			foreach (XPathNavigator subFolderNode in subFolderNodes)
			{
				string id = subFolderNode.SelectSingleNode("@id") == null ? Guid.NewGuid().ToString() : subFolderNode.SelectSingleNode("@id").Value;
				OutputFolder subFolder = new OutputFolder(subFolderNode.SelectSingleNode("@name").Value, id);
				string iteratorTypeName = subFolderNode.SelectSingleNode("@iteratortype") != null ? subFolderNode.SelectSingleNode("@iteratortype").Value : "";

				if (!string.IsNullOrEmpty(iteratorTypeName))
				{
					Type iteratorType = Instance.GetTypeFromReferencedAssemblies(iteratorTypeName, false);

					if (iteratorType == null)
					{
						throw new InvalidDataException("Data type of the iterator for folder [" + subFolderNode.Name + "] cannot be found in the referenced assemblies: " + iteratorTypeName);
					}
					subFolder.IteratorType = iteratorType;
				}

				folder.Folders.Add(subFolder);
				ReadXmlFolderNode(ref subFolder, subFolderNode);
			}
			#endregion
		}

		public string ProjectNamespace
		{
			get
			{
				return ProjectName.Replace(" ", "_");
			}
		}

		public string[] UserOptionCategories
		{
			get
			{
				ArrayList arr = new ArrayList();

				for (int i = 0; i < UserOptions.Count; i++)
				{
					if (arr.BinarySearch(UserOptions[i].Category) < 0)
					{
						arr.Add(UserOptions[i].Category);
						arr.Sort();
					}
				}
				if (arr.Count == 0)
				{
					arr.Add("General");
				}
				return (string[])arr.ToArray(typeof(string));
			}
		}

		public List<DefaultValueFunction> DefaultValueFunctions
		{
			get { return m_defaultValueFunctions; }
		}

		public void Clear()
		{
			if (Directory.Exists(Controller.TempPath))
			{
				try
				{
					Directory.Delete(Controller.TempPath, true);
				}
				catch
				{
					// Do nothing - usually some dev process is accessing the directory
				}
			}
			SharedData.ClearAssemblySearchPaths();
			DefaultValueFunctions.Clear();
			Settings.Default.CodeFile = "";
			CompileFolderName = "";
			Functions.Clear();
			ClearIncludedFiles();
			IsDirty = false;
			Namespaces.Clear();
			Parameters = new ParamInfo[0];
			ProjectFileName = "";
			ProjectXmlConfig = "";
			m_references.Clear();
			ScriptFunctionsFile = "";
			TargetFile = "";
			TemplateFile = "";
			TemplateNamespace = "";
			OutputNames.Clear();
			RootOutput = new OutputFolder("ROOT");
			Constants = new Constant[0];
			m_userOptions.Clear();
			DebugProjectFile = "";
			Actions = new BaseAction[0];
			m_referencedAssemblies.Clear();
			ApiExtensionMethods.Clear();

			TriggerProjectChangedEvent(true);
		}

		public string ProjectXmlConfig
		{
			get { return m_projectXmlConfig; }
			set { m_projectXmlConfig = value; }
		}

		public string ScriptFunctionsFile
		{
			get { return m_scriptFunctionsFile; }
			set { m_scriptFunctionsFile = value; }
		}

		public string TargetFile
		{
			get { return m_targetFile; }
			set
			{
				Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), m_targetFile, value);
				m_targetFile = value;
			}
		}

		public string TemplateNamespace
		{
			get
			{
				if (m_templateNamespace.Length == 0)
				{
					m_templateNamespace = "SlyceScripterTemplate";
				}
				return m_templateNamespace;
			}
			set
			{
				Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), m_templateNamespace, value);
				m_templateNamespace = value;
				m_templateNamespace = "SlyceScripterTemplate";
			}
		}

		public string TemplateFile
		{
			get
			{
				return m_templateFile;
			}
			set
			{
				Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), m_templateFile, value);
				m_templateFile = value;
			}
		}

		/// <summary>
		/// Searches the running assembly as well as all referenced assemblies for the given type.
		/// </summary>
		/// <param name="typeName"></param>
		/// <param name="throwOnError"></param>
		/// <returns></returns>
		public Type GetTypeFromReferencedAssemblies(string typeName, bool throwOnError)
		{
			// Check for generic types
			if (typeName.IndexOf('<') >= 0)
			{
				// Replace all arguments with their full typenames
				int start = typeName.IndexOf('<') + 1;
				int end = typeName.LastIndexOf('>');
				string[] args = typeName.Substring(start, end - start).Split(',');
				string genericTypeName = typeName.Substring(0, start - 1);
				Type genericType = Type.GetType(string.Format("{0}`{1}", genericTypeName, args.Length));

				if (genericType == null)
				{
					bool found = false;

					// Now check defined namespaces
					foreach (string ns in Namespaces)
					{
						genericType = Type.GetType(ns + "." + genericTypeName + "`" + args.Length, false);

						if (genericType != null)
						{
							found = true;
							break;
						}
					}
					if (!found)
					{
						foreach (Assembly assembly in ReferencedAssemblies)
						{
							genericType = assembly.GetType(genericTypeName + "`" + args.Length, false);

							if (genericType != null)
							{
								break;
							}
							found = false;

							// Now check defined namespaces
							foreach (string ns in Namespaces)
							{
								genericType = assembly.GetType(ns + "." + genericTypeName + "`" + args.Length, false);

								if (genericType != null)
								{
									found = true;
									break;
								}
							}
							if (found)
							{
								break;
							}
						}
					}
				}
				List<Type> genericArgTypes = new List<Type>();

				for (int i = 0; i < args.Length; i++)
				{
					genericArgTypes.Add(GetTypeFromReferencedAssemblies(args[i], throwOnError));
				}

				genericType = genericType.MakeGenericType(genericArgTypes.ToArray());

				if (genericType != null)
				{
					return genericType;
				}
			}

			// TODO: This switch statement is only required until all templates have been compiled with the new strongly-typed system
			switch (typeName.ToLower())
			{
				case "string":
					return typeof(string);
				case "bool":
				case "boolean":
					return typeof(bool);
				case "int":
				case "int32":
				case "integer":
					return typeof(int);
				case "double":
					return typeof(double);
				case "enumeration":
				case "enum":
					return typeof(Enum);
				case "directory":
					return typeof(DirectoryInfo);
				case "color":
					return typeof(Color);
			}
			Type type = Type.GetType(typeName);

			if (type != null)
			{
				return type;
			}
			foreach (Assembly assembly in ReferencedAssemblies)
			{
				type = assembly.GetType(typeName, false);

				if (type != null)
				{
					return type;
				}
				// Now check defined namespaces
				foreach (string ns in Namespaces)
				{
					type = assembly.GetType(ns + "." + typeName, false);

					if (type != null)
					{
						return type;
					}
				}
				foreach (Type realType in assembly.GetTypes())
				{
					if (realType.Name == typeName)
					{
						return realType;
					}
				}
			}
			if (type == null && throwOnError)
			{
				throw new TypeNotFoundException("Type not found in referenced assemblies: " + typeName, typeName);
			}
			return type;
		}

		public ParamInfo[] Parameters { get; set; }

		/// <summary>
		/// Sorts the functions into alphabetical order
		/// </summary>
		public void SortFunctions()
		{
			// Sort the functions
			Comparers.FunctionComparer comparer = new Comparers.FunctionComparer();
			Functions.Sort(comparer);
		}

		/// <summary>
		/// Sorts the constants into alphabetical order
		/// </summary>
		public void SortConstants()
		{
			// Sort the constants
			string[] names = new string[Constants.Length];

			for (int i = 0; i < Constants.Length; i++)
			{
				if (Constants[i] != null)
				{
					names[i] = Constants[i].Name;
				}
			}
			Array.Sort(names, Constants);
		}

		/// <summary>
		/// Renames a function throughout the project.
		/// </summary>
		/// <param name="oldName"></param>
		/// <param name="newName"></param>
		public void RenameFunctionAll(string oldName, string newName)
		{
			// TODO: Replace this search with a regular expression that searches for a real function. ie: followed by 0-m spaces then an opening bracket. Also, preceeded by a non-alphanumeric character.
			oldName += "(";
			newName += "(";
			// Store original settings
			SearchHelper.SearchFunctions origSearchFunctions = SearchHelper.searchFunctions;
			SearchHelper.Scope origScope = SearchHelper.scope;
			FindReplaceOptions origFindReplaceOptions = SearchHelper.Options;
			string origTextToFind = SearchHelper.Options == null ? "" : SearchHelper.Options.FindText;
			// Modify settings for special replace
			SearchHelper.searchFunctions = SearchHelper.SearchFunctions.AllFunctions;
			SearchHelper.scope = SearchHelper.Scope.ScriptOnly;
			FindReplaceOptions opt = new FindReplaceOptions();
			opt.MatchCase = true;// Might need to revisit this decision. Will miss possible mis-spellings in comments etc.
			SearchHelper.Options = opt;
			SearchHelper.Options.FindText = oldName;
			// Replace name in script portion of all functions
			SearchHelper.ReplaceAll(oldName, newName);

			// Replace in generated file names
			RenameFunctionInRootOuput(oldName, newName);

			if (Controller.Instance.MainForm.UcGenerationChoices != null)
			{
				Controller.Instance.MainForm.UcGenerationChoices.Populate();
			}
			// Reset original settings
			SearchHelper.searchFunctions = origSearchFunctions;
			SearchHelper.scope = origScope;
			SearchHelper.Options = origFindReplaceOptions;

			if (SearchHelper.Options != null)
			{
				SearchHelper.Options.FindText = origTextToFind;
			}
		}

		private void RenameFunctionInRootOuput(string oldName, string newName)
		{
			if (RootOutput.Files != null)
			{
				foreach (OutputFile outputFile in RootOutput.Files)
				{
					if (outputFile.ScriptName == oldName)
					{
						outputFile.ScriptName = newName;
					}
				}
				foreach (OutputFolder subFolder in RootOutput.Folders)
				{
					RenameFunctionInOuputFolder(subFolder, oldName, newName);
				}
			}
		}

		private static void RenameFunctionInOuputFolder(OutputFolder outputFolder, string oldName, string newName)
		{
			foreach (OutputFile outputFile in outputFolder.Files)
			{
				if (outputFile.ScriptName == oldName)
				{
					outputFile.ScriptName = newName;
				}
			}
			foreach (OutputFolder subFolder in outputFolder.Folders)
			{
				RenameFunctionInOuputFolder(subFolder, oldName, newName);
			}
		}

		public BaseAction[] Actions { get; set; }

		/// <summary>
		/// Finds a folder by its qualified path eg: BLL\Widget\Foo
		/// </summary>
		/// <param name="qualifiedName"></param>
		/// <returns></returns>
		public OutputFolder FindFolderByQualifiedName(string qualifiedName)
		{
			string[] qualifiedNames = qualifiedName.Split('\\');

			foreach (OutputFolder folder in RootOutput.Folders)
			{
				if (folder.Name == qualifiedNames[0])
				{
					if (qualifiedNames.Length == 1)
					{
						return folder;
					}

					string[] remainingFolders = new string[qualifiedNames.Length - 1];
					Array.Copy(qualifiedNames, 1, remainingFolders, 0, qualifiedNames.Length - 1);
					return FindFolderByQualifiedName(remainingFolders, folder);
				}
			}
			return null;
		}

		private static OutputFolder FindFolderByQualifiedName(string[] qualifiedNames, OutputFolder folder)
		{
			foreach (OutputFolder subFolder in folder.Folders)
			{
				if (subFolder.Name == qualifiedNames[0])
				{
					if (qualifiedNames.Length == 1)
					{
						return subFolder;
					}

					string[] remainingFolders = new string[qualifiedNames.Length - 1];
					Array.Copy(qualifiedNames, 1, remainingFolders, 0, qualifiedNames.Length - 1);
					return FindFolderByQualifiedName(remainingFolders, subFolder);
				}
			}
			return null;
		}

		public OutputFolder FindFolder(string id)
		{
			if (id == RootOutput.Id)
				return RootOutput;

			foreach (OutputFolder folder in RootOutput.Folders)
			{
				if (folder.Id == id)
				{
					return folder;
				}
				OutputFolder temp = FindSubFolder(folder, id);

				if (temp != null)
				{
					return temp;
				}
			}
			return null;
		}

		public OutputFile FindFile(string id)
		{
			foreach (OutputFile file in RootOutput.Files)
			{
				if (file.Id == id)
				{
					return file;
				}
			}
			foreach (OutputFolder folder in RootOutput.Folders)
			{
				OutputFile file = FindFileInFolder(folder, id);

				if (file != null && file.Id == id)
				{
					return file;
				}
			}
			return null;
		}

		private static OutputFile FindFileInFolder(OutputFolder folder, string id)
		{
			foreach (OutputFile file in folder.Files)
			{
				if (file != null && file.Id == id)
				{
					return file;
				}
			}
			foreach (OutputFolder subFolder in folder.Folders)
			{
				OutputFile file = FindFileInFolder(subFolder, id);

				if (file != null)
				{
					return file;
				}
			}
			return null;
		}

		public List<OutputFile> FindFilesUsingFunction(string functionName)
		{
			List<OutputFile> matchingFiles = new List<OutputFile>();

			foreach (OutputFile file in RootOutput.Files)
			{
				if (file.ScriptName == functionName)
				{
					matchingFiles.Add(file);
				}
			}
			foreach (OutputFolder folder in RootOutput.Folders)
			{
				matchingFiles.AddRange(FindFilesUsingFunctionInFolder(folder, functionName));
			}
			return matchingFiles;
		}

		/// <summary>
		/// Gets a list of functions that use the supplied object in script.
		/// </summary>
		/// <param name="objectName">Name of object to find, eg: name of Function or UserOption.</param>
		/// <param name="allowDuplicates">Whether duplicate function names should be returned.</param>
		/// <returns></returns>
		public List<FunctionInfo> FindFunctionsUsing(string objectName, bool allowDuplicates)
		{
			SearchHelper.scope = SearchHelper.Scope.ScriptOnly;
			SearchHelper.Options = new FindReplaceOptions
			{
				FindText = objectName,
				MatchCase = true,
				MatchWholeWord = true,
				SearchHiddenText = true,
				SearchType = FindReplaceSearchType.Normal
			};
			SearchHelper.searchFunctions = SearchHelper.SearchFunctions.AllFunctions;
			SearchHelper.Search(objectName);
			List<FoundLocation> foundLocations = SearchHelper.FoundLocations;
			FunctionInfo prevFunction = null;
			List<FunctionInfo> functions = new List<FunctionInfo>();

			for (int i = foundLocations.Count - 1; i >= 0; i--)
			{
				if (allowDuplicates)
				{
					functions.Add(foundLocations[i].Function);
				}
				else
				{
					if (foundLocations[i].Function.Name != objectName &&
						(prevFunction == null || foundLocations[i].Function != prevFunction))
					{
						prevFunction = foundLocations[i].Function;
						functions.Add(foundLocations[i].Function);
					}
				}
			}
			return functions;
		}

		public bool UserOptionIsUsedInFilenames(UserOption userOption)
		{
			foreach (OutputFile file in RootOutput.Files)
			{
				if (file.Name.IndexOf("#UserOptions." + userOption.VariableName + "#") >= 0)
				{
					return true;
				}
			}
			foreach (OutputFolder subFolder in RootOutput.Folders)
			{
				if (subFolder.Name.IndexOf("#UserOptions." + userOption.VariableName + "#") >= 0)
				{
					return true;
				}

				if (UserOptionIsUsedInFilenames(userOption, subFolder))
				{
					return true;
				}
			}
			return false;
		}

		private static bool UserOptionIsUsedInFilenames(UserOption userOption, OutputFolder folder)
		{
			foreach (OutputFile file in folder.Files)
			{
				if (file.Name.IndexOf("#UserOptions." + userOption.VariableName + "#") >= 0)
				{
					return true;
				}
			}
			foreach (OutputFolder subFolder in folder.Folders)
			{
				if (subFolder.Name.IndexOf("#UserOptions." + userOption.VariableName + "#") >= 0)
				{
					return true;
				}

				if (UserOptionIsUsedInFilenames(userOption, subFolder))
				{
					return true;
				}
			}
			return false;
		}

		private static List<OutputFile> FindFilesUsingFunctionInFolder(OutputFolder folder, string functionName)
		{
			List<OutputFile> matchingFiles = new List<OutputFile>();

			foreach (OutputFile file in folder.Files)
			{
				if (file != null && file.ScriptName == functionName)
				{
					matchingFiles.Add(file);
				}
			}
			foreach (OutputFolder subFolder in folder.Folders)
			{
				matchingFiles.AddRange(FindFilesUsingFunctionInFolder(subFolder, functionName));
			}
			return matchingFiles;
		}

		private static OutputFolder FindSubFolder(OutputFolder folder, string id)
		{
			foreach (OutputFolder subFolder in folder.Folders)
			{
				if (subFolder != null && subFolder.Id == id)
				{
					return subFolder;
				}
				OutputFolder temp = FindSubFolder(subFolder, id);

				if (temp != null)
				{
					return temp;
				}
			}
			return null;
		}

		public bool TryGetApiExtensionFor(MethodInfo info, out ApiExtensionMethod extension)
		{
			return ApiExtensionMethods.TryGetValue(info, out extension);
		}

		public void AddApiExtension(ApiExtensionMethod extension)
		{
			ApiExtensionMethods.Add(extension.ExtendedMethod, extension);
		}

		public void RemoveApiExtension(ApiExtensionMethod extension)
		{
			ApiExtensionMethods.Remove(extension.ExtendedMethod);
		}

		/// <summary>
		/// Returns an existing ApiExtension or creates a new one, depending on whether
		/// one has been created for that 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public ApiExtensionMethod GetOrCreateApiExtensionFor(MethodInfo info)
		{
			ApiExtensionMethod ext;
			if (TryGetApiExtensionFor(info, out ext))
				return ext;

			ext = new ApiExtensionMethod(info);
			AddApiExtension(ext);

			return ext;
		}

		/// <summary>
		/// Checks to see if the Assembly at fileName has already been loaded from
		/// another file path. If ArchAngel.Providers.Whatever has been loaded from
		/// AA.P.Whatever.dll, it cannot be reloaded from AA.P.Whatever_EXT.dll for
		/// instance. This is a limitation in .Net, and requires an application restart
		/// in order to load the new assembly.
		/// </summary>
		/// <param name="fileName">The full filename of the assembly to check.</param>
		/// <returns>True if the assembly has already been loaded from another path,
		/// false if it has not.</returns>
		public bool AssemblyNameAlreadyLoadedFromAnotherLocation(string fileName)
		{
			string newAssemblyName = Path.GetFileNameWithoutExtension(fileName);

			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				AssemblyName assemblyName = assembly.GetName();
				if (assemblyName.FullName == newAssemblyName)
				{
					return true;
				}
			}

			return false;
		}

		public bool ContainsApiExtension(ApiExtensionMethod method)
		{
			return ApiExtensionMethods.ContainsKey(method.ExtendedMethod);
		}

		public void SetupDefaults()
		{
			string installDir = Path.GetDirectoryName(Application.ExecutablePath);
			string interfaceAssemblyPath = Path.Combine(installDir, "ArchAngel.Interfaces.dll");
			if (File.Exists(interfaceAssemblyPath))
				AddReferencedFile(new ReferencedFile(interfaceAssemblyPath, false, false));
		}

		public IEnumerable<UserOption> GetVirtualPropertiesFor(Type type)
		{
			foreach (UserOption uo in UserOptions)
			{
				if (uo.IteratorType == null) continue;

				if (uo.IteratorType.FullName == type.FullName)
					yield return uo;
			}
		}

		public IEnumerable<Type> GetUserOptionTypes()
		{
			yield return typeof(int);
			yield return typeof(string);
			yield return typeof(bool);
			yield return typeof(bool?);
			yield return typeof(int?);
			yield return typeof(ArchAngel.Interfaces.SourceCodeType);
			yield return typeof(ArchAngel.Interfaces.SourceCodeMultiLineType);

			if (ResetTemplateEnumCache)
			{
				templateEnumTypesCache = ExtensionAttributeHelper.GetAllTemplateEnumTypes(ReferencedAssemblies);
				ResetTemplateEnumCache = false;
			}

			foreach (var enumType in templateEnumTypesCache)
				yield return enumType;
		}
	}
}
