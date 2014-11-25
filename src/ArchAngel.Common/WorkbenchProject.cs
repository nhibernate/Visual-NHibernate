using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using ArchAngel.Common.DesignerProject;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Attributes;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Interfaces.TemplateInfo;
using log4net;
using Slyce.Common;
using Slyce.Common.Exceptions;
using Slyce.Common.Extension_Methods;
using DefaultValueFunction = ArchAngel.Interfaces.DefaultValueFunction;
using File = System.IO.File;
using Function = ArchAngel.Interfaces.TemplateInfo.Function;
using FunctionTypes = ArchAngel.Interfaces.ITemplate.FunctionTypes;
using UserOption = ArchAngel.Interfaces.UserOption;
using Version = System.Version;

namespace ArchAngel.Common
{
	public class WorkbenchProject : IWorkbenchProject
	{
		private bool _BusyLoadingProviders;
		private readonly List<IOption> _Options = new List<IOption>();
		private readonly List<IOutput> _Outputs = new List<IOutput>();
		private readonly List<IDefaultValueFunction> _DefaultValueFunctions = new List<IDefaultValueFunction>();
		private readonly List<Function> _Functions = new List<Function>();
		private readonly List<Assembly> _ReferencedAssemblies = new List<Assembly>();
		private readonly List<BaseAction> _Actions = new List<BaseAction>();
		private readonly List<ProviderInfo> _Providers = new List<ProviderInfo>();
		private readonly Dictionary<Type, List<IOption>> _VirtualPropertiesForTypes = new Dictionary<Type, List<IOption>>();
		private readonly Type _ArchAngelEditorAttributeType = typeof(ArchAngelEditorAttribute);
		private readonly List<AppDomain> _ProviderAppDomains = new List<AppDomain>();
		private string _TempFolder;
		private ArchAngel.Interfaces.Template.TemplateProject _TemplateProject;

		/// <summary>
		/// The template that was used to populate the CurrentProject
		/// </summary>
		private string _CurrentProjectTemplateAssembly = "";

		public bool VirtualPropertiesAreFilled { get; private set; }

		/// <summary>
		/// The filename of the appconfig.xml file used to load this project.
		/// </summary>
		public string AppConfigFilename { get; set; }
		private string TemplateFolder = "";
		public string TemplateName { get; private set; }
		public string TemplateDescription { get; private set; }

		public string ProjectFile { get; set; }

		public ArchAngel.Interfaces.Template.TemplateProject TemplateProject
		{
			get
			{
				if (_TemplateProject == null)
				{
					_TemplateProject = ArchAngel.Common.UserTemplateHelper.GetTemplates().SingleOrDefault(t => t.Name == ProjectSettings.UserTemplateName);

					//if (_TemplateProject == null)
					//    _TemplateProject = ArchAngel.Common.UserTemplateHelper.GetDefaultTemplate(System.Reflection.Assembly.GetExecutingAssembly(), "ArchAngel.Workbench.Resources.Templates.NHibernate.Default NHibernate.vnh_template");
				}
				return _TemplateProject;
			}
			set { _TemplateProject = value; }
		}

		public void InitialiseProvidersPreGeneration()
		{
			var data = new PreGenerationData
			{
				OutputFolder = this.ProjectSettings.OutputPath,
				OverwriteFiles = this.ProjectSettings.OverwriteFiles
			};

			foreach (var uo in this.Options.Where(o => o.IsVirtualProperty == false))
			{
				var optionValue = this.GetUserOption(uo.VariableName);
				data.UserOptions.Add(uo.VariableName, optionValue);
			}
			foreach (var provider in this.Providers)
			{
				ArchAngel.Interfaces.ProviderInfo[] otherProviders = new ProviderInfo[this.Providers.Count];
				this.Providers.CopyTo(otherProviders);
				data.OtherProviderInfos = otherProviders.ToList();
				data.OtherProviderInfos.Remove(provider);
				provider.InitialisePreGeneration(data);
				//_Loader.CallPreGenerationInitialisationFunction(provider, data);
			}
		}

		public void InitialiseScriptObjects()
		{
			var data = new PreGenerationData
			{
				OutputFolder = this.ProjectSettings.OutputPath,
				OverwriteFiles = this.ProjectSettings.OverwriteFiles
			};

			foreach (var uo in this.Options.Where(o => o.IsVirtualProperty == false))
			{
				var optionValue = this.GetUserOption(uo.VariableName);
				data.UserOptions.Add(uo.VariableName, optionValue);
			}
			foreach (var provider in this.Providers)
			{
				ArchAngel.Interfaces.ProviderInfo[] otherProviders = new ProviderInfo[this.Providers.Count];
				this.Providers.CopyTo(otherProviders);
				data.OtherProviderInfos = otherProviders.ToList();
				data.OtherProviderInfos.Remove(provider);
				//provider.InitialiseScriptObjects(data);
			}
			foreach (var provider in this.Providers)
				provider.InitialiseScriptObjects(data);
		}

		protected readonly List<string> ProvidersToBeDisplayed = new List<string>();

		public IWorkbenchProjectSettings ProjectSettings { get; set; }
		public bool FileSkippingIsImplemented { get; set; }
		public IOutput CombinedOutput { get; set; }
		public IList<GeneratedFile> GeneratedFilesThisSession { get; private set; }
		public IList<GeneratedFile> GeneratedFilesLastRun { get; private set; }
		private readonly List<string> ReferencedAssemblyPaths = new List<string>();
		private ArchAngel.Interfaces.Scripting.NHibernate.Model.IProject _ScriptProject;

		public ArchAngel.Interfaces.Scripting.NHibernate.Model.IProject ScriptProject
		{
			get { return _ScriptProject; }
			set { _ScriptProject = value; }
		}

		/// <summary>
		/// The Loader used to work with the template assembly.
		/// </summary>
		public ITemplateLoader TemplateLoader { get; set; }

		private static readonly ILog log = LogManager.GetLogger(typeof(WorkbenchProject));

		public WorkbenchProject()
		{
			VirtualPropertiesAreFilled = false;
			GeneratedFilesLastRun = new List<GeneratedFile>();
			GeneratedFilesThisSession = new List<GeneratedFile>();
			SelectedFilesHaveBeenSet = false;
		}

		~WorkbenchProject()
		{
			Slyce.Common.Utility.DeleteDirectoryBrute(TempFolder);
		}

		public bool SelectedFilesHaveBeenSet { get; set; }

		public List<IOption> Options
		{
			get { return _Options; }
		}

		public Dictionary<string, UserControl> OptionForms
		{
			get
			{
				Dictionary<string, UserControl> optionForms = new Dictionary<string, UserControl>();

				foreach (var provider in Providers)
					foreach (var optionForm in provider.OptionForms)
						optionForms.Add(optionForm.Text, (UserControl)optionForm);

				return optionForms;
			}
		}

		public string TempFolder
		{
			get
			{
				if (string.IsNullOrEmpty(_TempFolder))
				{
					_TempFolder = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Visual NHibernate" + Path.DirectorySeparatorChar + "Temp"), Guid.NewGuid().ToString().Replace("-", ""));
				}
				if (!Directory.Exists(_TempFolder))
					Directory.CreateDirectory(_TempFolder);

				return _TempFolder;
			}
		}

		public List<IOutput> Outputs
		{
			get { return _Outputs; }
		}

		public void AddGeneratedFile(GeneratedFile file)
		{
			if (GeneratedFilesLastRun.Contains(file) == false)
				GeneratedFilesLastRun.Add(file);
			if (GeneratedFilesThisSession.Contains(file) == false)
				GeneratedFilesThisSession.Add(file);
		}

		public void StartNewFileGenerationRun()
		{
			GeneratedFilesLastRun.Clear();
		}

		public bool Load(string projectFilename, IVerificationIssueSolver VerificationIssueSolver)
		{
			return Load(projectFilename, VerificationIssueSolver, false, null);
		}

		public bool Load(string projectFilename, IVerificationIssueSolver VerificationIssueSolver, bool skipTemplateLoad, string templateFilename)
		{
			log.DebugFormat("Attempting to load project file \"{0}\"", projectFilename);

			string tempFolder = PathHelper.GetTempFilePathFor("ArchAngel", projectFilename, ComponentKey.Designer_TempRun);

			if (Utility.StringsAreEqual(Path.GetExtension(projectFilename), ".aaproj", false))
			{
				ProjectFile = projectFilename;

				// Open the project file and check it for errors.
				Utility.UnzipFile(projectFilename, tempFolder);

				//Utility.CopyDirectory(Path.GetDirectoryName(projectFilename).PathSlash);

				if (!VerifyProjectFile(projectFilename, tempFolder, VerificationIssueSolver)) return false;

				// this is the Workbench project version
				//int fileVersion = int.Parse(Utility.ReadTextFile(Path.Combine(tempFolder, "version.txt")));

				AppConfigFilename = Path.Combine(tempFolder, "appconfig.xml");

				if (!File.Exists(AppConfigFilename))
				{
					AppConfigFilename = Path.Combine(tempFolder, "project.settings");
				}
			}
			else if (Utility.StringsAreEqual(Path.GetExtension(projectFilename), ".wbproj", false))
			{
				ProjectFile = projectFilename;
				// Get information about project
				tempFolder = ArchAngel.Interfaces.ProviderHelper.GetProjectFilesFolder(projectFilename);

				if (!VerifyProjectFile(projectFilename, tempFolder, VerificationIssueSolver)) return false;

				// this is the Workbench project version
				//int fileVersion = int.Parse(Utility.ReadTextFile(Path.Combine(tempFolder, "version.txt")));

				AppConfigFilename = Path.Combine(tempFolder, "appconfig.xml");
			}
			else // Old format
			{
				AppConfigFilename = projectFilename;
			}

			// Load the Project Settings from the appconfig.xml file
			LoadAppConfig(AppConfigFilename, skipTemplateLoad, templateFilename);

			// Load the project information from the template assembly
			if (skipTemplateLoad == false)
				LoadProjectInfo(TemplateLoader.GetProjectInfoXml());
			else
			{
				// Load serialised provider info
				LoadAndFillProviders(SharedData.ProjectSettingsFolder);
				FillVirtualProperties();
			}

			InitCustomFunctions();

			log.Debug("Sucessfully loaded project file.");
			return true;
		}

		public void NewProject(string projectOutputPath, string projectTemplate, string projectFilename)
		{
			NewAppConfig();
			ProjectFile = projectFilename;
			ProjectSettings.OutputPath = projectOutputPath;
			LoadTemplate(projectTemplate);

			// Load the project information from the template assembly
			LoadProjectInfo(TemplateLoader.GetProjectInfoXml());

			// Load serialised provider info
			LoadProviders();
			InitCustomFunctions();
			FillVirtualProperties();
		}

		public void InitProjectFromProjectWizardInformation(INewProjectInformation information)
		{
			FillProviders(information);
		}

		public object GetDefaultValueOf(IOption option)
		{
			if (option.DefaultValueIsFunction)
				return TemplateLoader.CallDefaultValueFunction(option, new object[0]);
			else
			{
				if (option.VarType == typeof(bool))
					return bool.Parse(option.DefaultValue);
				else
					throw new NotImplementedException("This type not handled yet: " + option.VarType.Name);
			}
		}

		public string GetPathRelativeToProjectFile(string name)
		{
			return RelativePaths.GetRelativePath(Path.GetDirectoryName(ProjectFile), name);
		}

		public string GetPathAbsoluteFromProjectFile(string name)
		{
			return RelativePaths.RelativeToAbsolutePath(Path.GetDirectoryName(ProjectFile), name);
		}

		private bool VerifyProjectFile(string projectFilename, string tempFolder, IVerificationIssueSolver VerificationIssueSolver)
		{
			ProjectVerifier verifier = new ProjectVerifier(Path.GetDirectoryName(projectFilename));

			if (verifier.Verify(tempFolder) == false)
			{
				if (verifier.DealWithVerificationFailures(VerificationIssueSolver, tempFolder, projectFilename) == false)
				{
					return false;
				}
			}
			return true;
		}

		public void LoadAppConfig(string appConfigFileName, bool skipTemplateLoad, string templateFilename)
		{
			if (!File.Exists(appConfigFileName))
			{
				log.Error("Could not find the App Config file.");
				throw new FileNotFoundException("Could not find appconfig.xml at the following path: " + appConfigFileName);
			}
			SharedData.ActiveProjectPath = "";
			SharedData.ProjectPath = "";
			SharedData.ProjectSettingsFolder = "";
			SharedData.TemplateFileName = "";
			string file = Path.GetFileName(appConfigFileName).ToLower();

			if (file == "appconfig.xml")
			{
				ProjectSettings = new ProjectSettings();
				ProjectSettings.Open(appConfigFileName, this);
			}
			else if (file == "project.settings")
			{
				using (StreamReader reader = new StreamReader(appConfigFileName))
				{
					if (reader.BaseStream.Length > 0)
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(IWorkbenchProjectSettings));
						ProjectSettings = (IWorkbenchProjectSettings)xmlSerializer.Deserialize(reader);
						//ProjectSettings.FileName = appConfigFileName;
					}
					reader.Close();
				}
			}
			if (skipTemplateLoad == false)
			{
				if (templateFilename != null) ProjectSettings.TemplateFileName = templateFilename;
				LoadTemplate(ProjectSettings.TemplateFileName);
			}
			else
			{
				log.Info("Skipped loading the template.");
			}
			SharedData.ProjectSettingsFolder = Path.GetDirectoryName(appConfigFileName);
		}

		public void SetUserOption(string name, object val)
		{
			TemplateLoader.SetUserOption(name, val);
		}

		public object GetUserOption(string name)
		{
			return TemplateLoader.GetUserOption(name);
		}

		public void NewAppConfig()
		{
			SharedData.ActiveProjectPath = "";
			SharedData.ProjectPath = "";
			SharedData.ProjectSettingsFolder = "";
			SharedData.TemplateFileName = "";
			string defaultTemplateFileName = SharedData.RegistryGetValue("DefaultTemplateFileName");
			string defaultSetupModelTemplateFileName = SharedData.RegistryGetValue("DefaultSetupModelTemplateFileName");

			ProjectSettings = new ProjectSettings(defaultTemplateFileName);
		}

		/// <summary>
		/// Save the ProjectSettings
		/// </summary>
		/// <param name="folder"></param>
		public void SaveAppConfig(string folder)
		{
			string file = Path.Combine(folder, "appconfig.xml");

			// Temporarily change project folder to a relative path.

			ProjectSettings.Save(file, this);
		}

		/// <summary>
		/// Loads the given template assembly (.aal) file from disk, sets the template name in the ProjectSettings,
		/// and fires the ProjectLoaded event
		/// </summary>
		/// <param name="templateFileName">The filename of the template to load.</param>
		/// <returns></returns>
		public void LoadTemplate(string templateFileName)
		{
			log.Debug("Loading Template from " + templateFileName);
			SharedData.CustomFunctions.Clear();

			if (!string.IsNullOrEmpty(templateFileName))
			{
				try
				{
					FileVersionInfo myFI = FileVersionInfo.GetVersionInfo(templateFileName);
					Version version = new Version(myFI.ProductVersion);

					Version expectedVersion = new Version(1, 1, 9, 49);

					if (version < expectedVersion)
						throw new OldVersionException(string.Format("The template was compiled with an old version of ArchAngel [{0}]. Recompile in the template project (.stz) in ArchAngel Designer.\n\nTemplate file: {1}", version, templateFileName));

					var tempTemplateName = templateFileName;
					bool result = LoadAssembly(tempTemplateName);

					if (result)
						ProjectSettings.TemplateFileName = templateFileName;
					else
						throw new Exception("Could not load compiled template assembly.");
				}
				catch (TargetInvocationException ex)
				{
					string message = ex.GetBaseException().Message;

					if (message.IndexOf("Could not load type 'ArchAngel.Providers.Database.Model.AppConfig'") >= 0)
						throw new Exception("Template out-of-date. This template refers to a type which has moved. Any references to 'ArchAngel.Providers.Database.Model.AppConfig.ActiveProjectPath' in this template must be changed to 'ArchAngel.Interfaces.SharedData.ActiveProjectPath'. If this template is based on the PetShop template, then this value is used for one of the Constants. Make this change then re-compile the template.");

					throw new Exception("Error while loading template: " + ProjectSettings.TemplateFileName +
										Environment.NewLine + Environment.NewLine + ex.GetBaseException().Message +
										ex.StackTrace);
				}
			}
			ProjectSettings.TemplateFileName = templateFileName;
		}

		/// <summary>
		/// Loads the new assembly, unloading the previously loaded assembly, if it is loaded.
		/// </summary>
		/// <param name="filepath"></param>
		private bool LoadAssembly(string filepath)
		{
			if (filepath == null)
			{

			}
			log.Debug("Loading Template Assembly");
			Reset(true);

			if (!string.IsNullOrEmpty(filepath) && !File.Exists(filepath))
			{
				throw new Exception("Could not file the template file at " + filepath);
			}

			_CurrentProjectTemplateAssembly = filepath;
			TemplateFolder = Path.GetDirectoryName(filepath);

			List<string> searchPaths = new List<string>();
			searchPaths.AddRange(new[] { Path.GetDirectoryName(Application.ExecutablePath), TemplateFolder });

			string providerFolder = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Providers");

			if (Directory.Exists(providerFolder))
			{
				searchPaths.Add(providerFolder);
			}

			SharedData.AddAssemblySearchPaths(searchPaths);

			// Assembly.LoadFile needs an absolute path
			string absoluteFilePath = Path.GetFullPath(filepath);

			// Does this ArchAngel template exist in the executable directory? If yes, then use this version.
			string betterPath = "";

#if DEBUG
			betterPath = Path.Combine(Slyce.Common.RelativePaths.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location, @"..\..\..\..\ArchAngel.Templates\NHibernate\Template"), Path.GetFileName(filepath));
#else
			betterPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Path.GetFileName(filepath));
#endif

			if (File.Exists(betterPath))
				absoluteFilePath = betterPath;

			// Create a new TemplateLoader with the new assembly we are working with.
			Assembly assembly = Assembly.LoadFile(absoluteFilePath);
			TemplateLoader = new TemplateLoader(assembly);
			TemplateLoader.SetAssemblySearchPaths(new List<string>(SharedData.AssemblySearchPaths));

			LoadProjectFunctions();

			return true;
		}

		/// <summary>
		/// Clears the cached assembly and project objects. Required when user compiles a new AAL file.
		/// </summary>
		public void Reset(bool force)
		{
			// Only reset if the file is different to the currently loaded file. If the file isn't different, don't reset.
			if (force || _CurrentProjectTemplateAssembly != TemplateLoader.CurrentAssembly.Location ||
				Utility.GetCheckSumOfFile(_CurrentProjectTemplateAssembly) != Utility.GetCheckSumOfFile(TemplateLoader.CurrentAssembly.Location))
			{
				Clear();

				//_currentProject = null;
				ResetCustomFunctions();
				_CurrentProjectTemplateAssembly = null;
				TemplateLoader = null;
			}
		}

		private void Clear()
		{
			foreach (ProviderInfo provider in Providers)
			{
				provider.Clear();
			}
			_Providers.Clear();
			_Options.Clear();
			_Outputs.Clear();
			CombinedOutput = null;
			_DefaultValueFunctions.Clear();
			TemplateName = "";
			TemplateDescription = "";
			_Functions.Clear();
			_ReferencedAssemblies.Clear();
			ReferencedAssemblyPaths.Clear();
			_Actions.Clear();
		}

		public void LoadProjectInfo(string projectInfoXml)
		{
			XmlDocument xmlProjectInfo = new XmlDocument();

			try
			{
				xmlProjectInfo.LoadXml(projectInfoXml);
				InitFromDesignerProjectXml(xmlProjectInfo);
			}
			catch (Exception ex)
			{
				log.ErrorFormat("Error while loading the template: {0}", ex.Message);
				throw new Exception("Error while loading template: ", ex);
			}
		}

		private void LoadProjectFunctions()
		{
			XmlDocument xmlFunctions = new XmlDocument();
			xmlFunctions.LoadXml(TemplateLoader.GetFunctionsXml());
			PopulateFunctions(xmlFunctions);
		}

		public void FillProviders()
		{
			FillProviders(ArchAngel.Interfaces.ProviderHelper.GetProjectFilesFolder(ProjectFile));
		}

		private void LoadAndFillProviders()
		{
			LoadProviders();

			if (string.IsNullOrEmpty(ProjectFile) == false)
			{
				foreach (var provider in _Providers)
				{
					ProviderHelper.PopulateProviderFromProjectFile(provider, ProjectFile);
				}
			}
		}

		public void FillProviders(INewProjectInformation info)
		{
			if (info == null) return;

			foreach (var provider in _Providers.Where(p => p.GetType().FullName == info.ValidProviderType.FullName))
			{
				provider.LoadFromNewProjectInformation(info);
			}
		}

		public void LoadAndFillProviders(string folder)
		{
			LoadProviders();
			FillProviders(folder);
		}

		private void LoadProviders()
		{
			log.DebugFormat("Loading Providers");

			_BusyLoadingProviders = true;

			_Providers.Clear();

			foreach (Assembly assembly in ReferencedAssemblies)
			{
				if (ProviderInfo.IsProvider(assembly))
				{
					string assemblyFilename = assembly.GetName().Name;

					//foreach (string providerFilename in ProvidersToBeDisplayed.Select(p => Path.GetFileNameWithoutExtension(p)))
					//{
					//	if (Utility.StringsAreEqual(providerFilename, assemblyFilename, false))
					//	{
					ProviderInfo provider = ProviderInfo.GetProviderInfo(assembly);
					provider.Assembly = assembly;
					_Providers.Add(provider);
					//	}
					//}
				}
			}
			foreach (ProviderInfo providerInfo in _Providers)
			{
				providerInfo.OtherProviders = _Providers;
			}
			_BusyLoadingProviders = false;
		}

		private void FillProviders(string folder)
		{
			foreach (ProviderInfo provider in Providers)
			{
				log.InfoFormat("Loading serialized provider data for provider {0}", provider.Name);

				ProviderHelper.PopulateProvider(provider, folder);
			}
		}

		private void FillOptionForms()
		{
			foreach (ProviderInfo provider in Providers)
			{
				foreach (var form in provider.OptionForms)
				{
					this.OptionForms.Add(form.Text, (UserControl)form);
				}
			}
		}

		/// <summary>
		/// Adds the required Virtual Properties to all objects in all Providers.
		/// </summary>
		public void FillVirtualProperties()
		{
			log.DebugFormat("Filling Virtual Properties");
			log.DebugFormat("Number of UserOptions currently loaded: " + _Options.Count);

			foreach (ProviderInfo provider in Providers)
			{
				foreach (Type type in provider.Assembly.GetTypes())
				{
					ArchAngelEditorAttribute[] atts = (ArchAngelEditorAttribute[])type.GetCustomAttributes(_ArchAngelEditorAttributeType, false);

					if (atts.Length > 0 && atts[0].VirtualPropertiesAllowed)
					{
						List<IOption> options = GetVirtualProperties(type);

						IScriptBaseObject[] objects = provider.GetAllObjectsOfType(type.FullName).ToArray();

						if (objects != null)
						{
							foreach (IScriptBaseObject obj in objects)
							{
								if (obj.GetType() != type)
								{
									continue;
								}
								foreach (IOption opt in options)
								{
									bool found = false;

									foreach (IUserOption existingOption in obj.Ex)
									{
										if (existingOption.Name == opt.VariableName)
										{
											object[] parameters = new object[] { obj };

											if (opt.ResetPerSession)
											{
												object newValue = TemplateLoader.CallDefaultValueFunction(opt, parameters);
												existingOption.Value = newValue;
											}

											// If the data type has changed in the template, any stored values are of the wrong
											// type, so we need to discard them.
											if (existingOption.DataType.Equals(opt.VarType) == false)
											{
												existingOption.DataType = opt.VarType;
												existingOption.Value = null;
											}

											found = true;
											break;
										}
									}
									if (!found)
									{
										var parameters = new object[] { obj };
										object defaultValue = TemplateLoader.CallDefaultValueFunction(opt, parameters);
										obj.AddUserOption(new UserOption(opt.VariableName, opt.VarType, defaultValue));
									}
								}
								// Remove any VirtualProperties that are no longer in the template
								List<IUserOption> virtualPropertiesToRemove = new List<IUserOption>();

								foreach (IUserOption existingOption in obj.Ex)
								{
									bool found = false;

									foreach (IOption opt in options)
									{
										if (existingOption.Name == opt.VariableName)
										{
											found = true;
											break;
										}
									}
									if (!found)
									{
										virtualPropertiesToRemove.Add(existingOption);
									}
								}
								foreach (IUserOption virtualPropToRemove in virtualPropertiesToRemove)
								{
									obj.Ex.Remove(virtualPropToRemove);
								}
							}
						}
					}
				}
			}
			VirtualPropertiesAreFilled = true;
		}

		/// <summary>
		/// Gets the virtual properties for the specified type, including options that 
		/// have been specified for base types of the type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public List<IOption> GetVirtualProperties(Type type)
		{
			List<string> typeNames = new List<string>();
			typeNames.Add(type.FullName);

			typeNames.AddRange(type.GetBaseTypeAndInterfacesFullNames());

			List<IOption> options = _Options.Where(o => typeNames.Contains(o.IteratorName)).ToList();

			return options;
		}

		private void PopulateFunctions(XmlNode doc)
		{
			log.DebugFormat("Populating Functions");
			XmlNodeList nodes = doc.SelectNodes("ROOT/function");
			_Functions.Clear();

			if (nodes != null)
				for (int i = 0; i < nodes.Count; i++)
				{
					var function = new Function();
					_Functions.Add(function);
					function.Name = nodes[i].Attributes["name"].InnerText;
					function.ParameterTypeName = nodes[i].Attributes["parametertypename"].InnerText;
				}
		}

		public void InitFromDesignerProjectXml(XmlDocument doc)
		{
			log.DebugFormat("Loading Designer Project Info from Xml Document");

			if (doc.DocumentElement == null)
				throw new DeserialisationException("Could not load designer project info - xml document is empty");

			if (doc.DocumentElement.Name == "ROOT")
			{
				LoadOldDesignerProjectXml(doc);
				return;
			}

			// New loading code
			ProjectBase designerProject = new ProjectBase();
			ProjectDeserialiserV1 deserialiserV1 = new ProjectDeserialiserV1(new FileController());
			deserialiserV1.LoadFromSingleXmlFile(designerProject, doc, ProjectFile);

			TemplateName = designerProject.ProjectName;
			TemplateDescription = designerProject.ProjectDescription;

			// Providers and referenced files
			ProvidersToBeDisplayed.Clear();
			foreach (var referencedFile in designerProject.References)
			{
				if (referencedFile.IsProvider && referencedFile.UseInWorkbench)
				{
					ProvidersToBeDisplayed.Add(Path.GetFileName(referencedFile.FileName));
				}

				string pathsSearched = "";
				string currentPath = Path.GetFileName(referencedFile.FileName);
				bool assemblyFound = false;

				foreach (string searchPath in SharedData.AssemblySearchPaths)
				{
					pathsSearched += searchPath + Environment.NewLine;
					string filePath = Path.Combine(searchPath, currentPath);

					if (File.Exists(filePath))
					{
						log.DebugFormat("Loading Provider from {0}", filePath);
						assemblyFound = true;
						ReferencedAssemblyPaths.Add(filePath);
						break;
					}
				}

				if (!assemblyFound)
				{
					if (MessageBox.Show(string.Format("'{0}' can't be located. The following folders have been searched: \n\n{1}\nDo you want to locate this file?", currentPath, pathsSearched), "File not found - Loading Project", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
					{
						OpenFileDialog openFileDialog = new OpenFileDialog();
						openFileDialog.FileName = currentPath;

						if (openFileDialog.ShowDialog() == DialogResult.OK)
						{
							if (File.Exists(openFileDialog.FileName))
							{
								// Add this path to the search paths, because related assemblies might be in here as well.
								SharedData.AddAssemblySearchPath(Path.GetDirectoryName(openFileDialog.FileName));
								ReferencedAssemblyPaths.Add(openFileDialog.FileName);
								assemblyFound = true;
							}
						}
					}
					if (!assemblyFound)
					{
						throw new FileNotFoundException(string.Format("Referenced assembly not found: {0}\n\nPlease make sure the assembly exists in one of these folders:{1}", currentPath, pathsSearched));
					}
				}
			}
			// User Options
			var options = designerProject.UserOptions.Select(uo => uo.ToOption());
			_Options.AddRange(options);

			log.DebugFormat("Loaded up {0} UserOptions", _Options.Count);
			_Options.ForEach(o => log.DebugFormat("UserOption {0} | On Type {1}", o.VariableName, o.IteratorName));

			LoadReferencedAssemblies();
			LoadAndFillProviders();

			CombinedOutput = designerProject.RootOutput.ToCombinedOutput();
			// I am not handling Default Value Functions. I think they no longer exist.

			FillVirtualProperties();
		}

		private void LoadOldDesignerProjectXml(XmlNode doc)
		{
			TemplateName = doc.SelectSingleNode("ROOT/config/project/name").InnerText;
			TemplateDescription = doc.SelectSingleNode("ROOT/config/project/description").InnerText;

			#region Referenced Assemblies
			ProvidersToBeDisplayed.Clear();
			string[] allPaths = doc.SelectSingleNode("ROOT/referencedfiles").InnerText.Split(',');

			foreach (var path in allPaths)
			{
				if (string.IsNullOrEmpty(path))
				{
					continue;
				}
				string[] parts = path.Split('|');
				string currentPath = parts[0];
				currentPath = currentPath.Substring(currentPath.LastIndexOf(@"\") + 1);
				bool isProvider = parts.Length > 3 ? parts[3] == "useInWorkbench=True" : true;
				List<string> providerAssemblyFiles = new List<string>();

				if (isProvider)
				{
					ProvidersToBeDisplayed.Add(currentPath);
				}
				bool asssemblyFound = false;
				string pathsSearched = "";

				foreach (string searchPath in SharedData.AssemblySearchPaths)
				{
					pathsSearched += searchPath + Environment.NewLine;
					string filePath = Path.Combine(searchPath, currentPath);

					if (File.Exists(filePath))
					{
						log.DebugFormat("Loading Provider from {0}", filePath);
						asssemblyFound = true;
						providerAssemblyFiles.Add(filePath);
						break;
					}
				}

				ReferencedAssemblyPaths.AddRange(providerAssemblyFiles.ToArray());

				if (!asssemblyFound)
				{
					if (MessageBox.Show(string.Format("'{0}' can't be located. The following folders have been searched: \n\n{1}\nDo you want to locate this file?", currentPath, pathsSearched), "File not found - Loading Project", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
					{
						OpenFileDialog openFileDialog = new OpenFileDialog();
						openFileDialog.FileName = currentPath;

						if (openFileDialog.ShowDialog() == DialogResult.OK)
						{
							if (File.Exists(openFileDialog.FileName))
							{
								// Add this path to the search paths, because related assemblies might be in here as well.
								SharedData.AddAssemblySearchPath(Path.GetDirectoryName(openFileDialog.FileName));
								providerAssemblyFiles.Add(openFileDialog.FileName);
								asssemblyFound = true;
							}
						}
					}
					if (!asssemblyFound)
					{
						throw new FileNotFoundException(string.Format("Referenced assembly not found: {0}\n\nPlease make sure the assembly exists in one of these folders:{1}", currentPath, pathsSearched));
					}
				}
			}

			LoadReferencedAssemblies();
			LoadAndFillProviders();

			#endregion

			#region User Options
			XmlNodeList nodes = doc.SelectNodes("ROOT/config/project/options/option");
			_Options.Clear();

			if (nodes != null)
			{
				for (int i = 0; i < nodes.Count; i++)
				{
					XmlNode subNode = nodes[i];
					Option opt = new Option();
					_Options.Add(opt);
					opt.Description = subNode.SelectSingleNode("description").InnerText;
					opt.Text = subNode.SelectSingleNode("text").InnerText;
					opt.VariableName = subNode.SelectSingleNode("variablename").InnerText;
					opt.VarType = GetTypeFromReferencedAssemblies(subNode.SelectSingleNode("type").InnerText, true);
					opt.Category = subNode.SelectSingleNode("category").InnerText.Replace("_", " ");
					opt.DefaultValue = subNode.SelectSingleNode("defaultvalue") != null ? subNode.SelectSingleNode("defaultvalue").InnerText : "";
					opt.DefaultValueIsFunction = subNode.SelectSingleNode("defaultvalueisfunction") != null ? bool.Parse(subNode.SelectSingleNode("defaultvalueisfunction").InnerText) : false;
					opt.IteratorName = subNode.SelectSingleNode("iteratorname") != null ? subNode.SelectSingleNode("iteratorname").InnerText : "";
					opt.ValidatorFunction = subNode.SelectSingleNode("validatorfunction") != null ? subNode.SelectSingleNode("validatorfunction").InnerText : "";
					opt.ResetPerSession = subNode.SelectSingleNode("resetpersession") != null ? bool.Parse(subNode.SelectSingleNode("resetpersession").InnerText) : false;

					opt.IsValidValue = null;

					if (subNode.SelectSingleNode("isvalidvalue") != null && !string.IsNullOrEmpty(subNode.SelectSingleNode("isvalidvalue").InnerText))
					{
						opt.IsValidValue = bool.Parse(subNode.SelectSingleNode("isvalidvalue").InnerText);
					}
					opt.DisplayToUserValue = null;

					if (subNode.SelectSingleNode("displaytouserfunction") != null)
					{
						opt.DisplayToUserFunction = subNode.SelectSingleNode("displaytouserfunction").InnerText;
					}

					XmlNodeList valueNodes = subNode.SelectNodes("values/value");
					if (valueNodes != null)
					{
						opt.EnumValues = new string[valueNodes.Count];

						for (int x = 0; x < valueNodes.Count; x++)
						{
							opt.EnumValues[x] = valueNodes[x].InnerText;
						}
					}
					else
					{
						opt.EnumValues = new string[0];
					}
				}
			}
			#endregion

			#region Default Value Functions
			nodes = doc.SelectNodes("ROOT/config/project/defaultvaluefunctions/defaultvaluefunction");
			_DefaultValueFunctions.Clear();

			if (nodes != null)
				for (int i = 0; i < nodes.Count; i++)
				{
					XmlNode subNode = nodes[i];
					DefaultValueFunction defaultValueFunction = new DefaultValueFunction();
					defaultValueFunction.ObjectType = GetTypeFromReferencedAssemblies(subNode.SelectSingleNode("objecttype").InnerText, true);
					defaultValueFunction.PropertyName = subNode.SelectSingleNode("propertyname").InnerText;
					defaultValueFunction.UseCustomCode = bool.Parse(subNode.SelectSingleNode("usecustomcode").InnerText);
					defaultValueFunction.FunctionType = (FunctionTypes)Enum.Parse(typeof(FunctionTypes), subNode.SelectSingleNode("functiontype").InnerText, true);
					_DefaultValueFunctions.Add(defaultValueFunction);
				}

			#endregion

			#region Outputs
			XmlNode rootFolderNode = doc.SelectSingleNode("ROOT/config/project/rootoutput/rootfolder");

			CombinedOutput = new Output();
			CombinedOutput.RootFolder = new Folder();
			CombinedOutput.RootFolder.Name = "Root";
			CombinedOutput.Name = "Combined";

			Output output = new Output();
			_Outputs.Add(output);
			output.RootFolder = new Folder();
			output.RootFolder.Name = "Root";
			output.Name = "";
			ProcessFolderNode(rootFolderNode, output.RootFolder);
			ProcessFolderNode(rootFolderNode, CombinedOutput.RootFolder);
			#endregion
		}

		private void ProcessFolderNode(XmlNode folderNode, IFolder parentFolder)
		{
			#region Process sub-folders
			XmlNodeList subFolderNodes = folderNode.SelectNodes("folder");
			List<IFolder> actualSubFolders = new List<IFolder>();

			if (subFolderNodes != null)
				for (int i = 0; i < subFolderNodes.Count; i++)
				{
					XmlNode subFolderNode = subFolderNodes[i];
					Folder tempFolder = new Folder();

					tempFolder.Name = subFolderNode.Attributes["name"].Value;
					tempFolder.Id = subFolderNode.Attributes["id"] == null ? "" : subFolderNode.Attributes["id"].Value;
					tempFolder.IteratorName = subFolderNode.SelectSingleNode("@iteratortype") == null ? "" : subFolderNode.SelectSingleNode("@iteratortype").InnerText;
					ProcessFolderNode(subFolderNode, tempFolder);
					actualSubFolders.Add(tempFolder);
				}

			#endregion

			#region Process scripts
			XmlNodeList scriptNodes = folderNode.SelectNodes("script");
			List<IScript> actualScripts = new List<IScript>();

			if (scriptNodes != null)
				for (int i = 0; i < scriptNodes.Count; i++)
				{
					XmlNode scriptNode = scriptNodes[i];

					// Only process if the correct output
					//if (HasDesiredOutput(scriptNode, outputName))
					//{
					Script tempScript = new Script();
					tempScript.FileName = scriptNode.Attributes["filename"].Value;
					tempScript.ScriptName = scriptNode.Attributes["scriptname"].Value;
					tempScript.IteratorName = scriptNode.Attributes["iteratorname"].Value;

					tempScript.Id = scriptNode.Attributes["id"] == null ? "" : scriptNode.Attributes["id"].Value;
					actualScripts.Add(tempScript);
					//}
				}

			#endregion

			#region Process files
			XmlNodeList fileNodes = folderNode.SelectNodes("file");
			List<IFile> actualFiles = new List<IFile>();

			if (fileNodes != null)
				for (int i = 0; i < fileNodes.Count; i++)
				{
					XmlNode fileNode = fileNodes[i];

					Interfaces.TemplateInfo.File tempFile = new Interfaces.TemplateInfo.File();
					tempFile.Name = fileNode.Attributes["name"].Value;
					tempFile.StaticFileName = fileNode.Attributes["staticfilename"] == null ? fileNode.Attributes["name"].Value : fileNode.Attributes["staticfilename"].Value;
					tempFile.IteratorName = fileNode.Attributes["iteratorname"] == null ? fileNode.Attributes["iteratorname"].Value : fileNode.Attributes["iteratorname"].Value;
					tempFile.Id = fileNode.Attributes["id"] == null ? "" : fileNode.Attributes["id"].Value;
					actualFiles.Add(tempFile);
				}

			#endregion

			//parentFolder.Files = files;
			//parentFolder.SubFolders = subFolders;
			//parentFolder.Scripts = scripts;

			AddFiles(parentFolder, actualFiles);
			AddScriptFiles(parentFolder, actualScripts);
			AddSubFolders(parentFolder, actualSubFolders);
		}

		/// <summary>
		/// Adds new sub-folders to an existing array of sub-folders for the folder object, ensuring that no duplicates are added.
		/// </summary>
		/// <param name="folder"></param>
		/// <param name="subFolders"></param>
		private void AddSubFolders(IFolder folder, IEnumerable<IFolder> subFolders)
		{
			List<IFolder> subFoldersToAdd = new List<IFolder>(folder.SubFolders);

			foreach (IFolder newSubFolder in subFolders)
			{
				bool found = false;

				for (int i = 0; i < folder.SubFolders.Length; i++)
				{
					if (folder.SubFolders[i].Name == newSubFolder.Name &&
						folder.SubFolders[i].IteratorName == newSubFolder.IteratorName)
					{
						found = true;
						break;
					}
				}
				if (!found)
				{
					subFoldersToAdd.Add(newSubFolder);
				}
			}
			folder.SubFolders = subFoldersToAdd.ToArray();
		}

		/// <summary>
		/// Adds new files to an existing array of files for the folder object, ensuring that no duplicates are added.
		/// </summary>
		/// <param name="folder"></param>
		/// <param name="files"></param>
		private void AddFiles(IFolder folder, IEnumerable<IFile> files)
		{
			List<IFile> filesToAdd = new List<IFile>(folder.Files);

			foreach (IFile newFile in files)
			{
				bool found = false;

				for (int i = 0; i < folder.Files.Length; i++)
				{
					if (folder.Files[i].Name == newFile.Name &&
						folder.Files[i].IteratorName == newFile.IteratorName &&
						folder.Files[i].StaticFileName == newFile.StaticFileName)
					{
						found = true;
						break;
					}
				}
				if (!found)
				{
					filesToAdd.Add(newFile);
				}
			}
			folder.Files = filesToAdd.ToArray();
		}

		/// <summary>
		/// Adds new script-files to an existing array of script-files for the folder object, ensuring that no duplicates are added.
		/// </summary>
		/// <param name="folder"></param>
		/// <param name="scriptFiles"></param>
		private void AddScriptFiles(IFolder folder, IEnumerable<IScript> scriptFiles)
		{
			List<IScript> filesToAdd = new List<IScript>(folder.Scripts);

			foreach (IScript newFile in scriptFiles)
			{
				bool found = false;

				for (int i = 0; i < folder.Scripts.Length; i++)
				{
					if (folder.Scripts[i].FileName == newFile.FileName &&
						folder.Scripts[i].IteratorName == newFile.IteratorName &&
						folder.Scripts[i].ScriptName == newFile.ScriptName)
					{
						found = true;
						break;
					}
				}
				if (!found)
				{
					filesToAdd.Add(newFile);
				}
			}
			folder.Scripts = filesToAdd.ToArray();
		}

		private void InitCustomFunctions()
		{
			SharedData.CustomFunctions = new Dictionary<string, IDefaultValueFunction>();

			foreach (IDefaultValueFunction defFunc in _DefaultValueFunctions)
			{
				switch (defFunc.FunctionType)
				{
					case FunctionTypes.DefaultValue:
						SharedData.CustomFunctions.Add(defFunc.ObjectType.FullName + "." + defFunc.PropertyName + "Default", defFunc);
						break;
					case FunctionTypes.HelperOverride:
						SharedData.CustomFunctions.Add(defFunc.ObjectType.FullName + "." + defFunc.PropertyName, defFunc);
						break;
					case FunctionTypes.Validate:
						SharedData.CustomFunctions.Add(defFunc.ObjectType.FullName + "." + defFunc.PropertyName + "Validate", defFunc);
						break;
				}
			}
		}

		private void ResetCustomFunctions()
		{
			SharedData.CustomFunctions = new Dictionary<string, IDefaultValueFunction>();
		}

		/// <summary>
		/// Searches the running assembly as well as all referenced assemblies for the given type.
		/// </summary>
		/// <param name="typeName"></param>
		/// <returns></returns>
		/// <param name="throwOnError"></param>
		public Type GetTypeFromReferencedAssemblies(string typeName, bool throwOnError)
		{
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
					return typeof(int);
				case "double":
					return typeof(double);
				case "table":
					typeName = "ArchAngel.Providers.Database.Model.Table";
					break;
				case "view":
					typeName = "ArchAngel.Providers.Database.Model.View";
					break;
				case "storedprocedure":
					typeName = "ArchAngel.Providers.Database.Model.View";
					break;
				case "scriptobject":
					typeName = "ArchAngel.Providers.Database.Model.ScriptObject";
					break;
				case "database":
					typeName = "ArchAngel.Providers.Database.Model.Database";
					break;
				case "column":
					typeName = "ArchAngel.Providers.Database.Model.Column";
					break;
				case "mapcolumn":
					typeName = "ArchAngel.Providers.Database.Model.MapColumn";
					break;
				case "database[]":
					typeName = "ArchAngel.Providers.Database.Model.Database[]";
					break;
				case "column[]":
					typeName = "ArchAngel.Providers.Database.Model.Column[]";
					break;
				case "enumeration":
				case "enum":
					return typeof(Enum);
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
					break;
				}
			}
			if (type == null && throwOnError)
			{
				throw new Exception("Type not found: " + typeName);
			}
			return type;
		}

		public Type GetIteratorTypeFromProviders(string typeName)
		{
			ProviderInfo provider;
			return GetIteratorTypeFromProviders(typeName, out provider);
		}

		public Type GetIteratorTypeFromProviders(string typeName, out ProviderInfo provider)
		{
			foreach (ProviderInfo providerObject in Providers)
			{
				Type iteratorType = providerObject.Assembly.GetType(typeName);
				if (iteratorType == null) continue; // Not found in this assembly

				provider = providerObject;
				return iteratorType;
			}

			// We couldn't find the assembly. Try figure out why.
			// Search the currently loaded assemblies.
			var allAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
			foreach (var assem in allAssemblies)
			{
				if (assem.GetType(typeName) != null)
				{
					// Type exists in another assembly.
					throw new TypeNotAnIteratorException(typeName,
						string.Format("Type {0} does not exist in Provider assemblies, but was found in {1}",
						typeName, assem.FullName));
				}
			}

			throw new TypeDoesNotExistException(typeName,
				string.Format("The IteratorType {0} could not be found in any assembly. This is an issue with the Template, please contact the template author.", typeName));
		}

		/// <summary>
		/// Gets an array of assemblies that are referenced by this project
		/// </summary>
		public List<Assembly> ReferencedAssemblies
		{
			get
			{
				if (_ReferencedAssemblies.Count == 0)
				{
					LoadReferencedAssemblies();
				}
				return _ReferencedAssemblies;
			}
		}

		private void LoadReferencedAssemblies()
		{
			log.DebugFormat("Loading Referenced Assemblies");
			UnloadAppDomains();

			_ReferencedAssemblies.Clear();

			foreach (string referencedAssembly in ReferencedAssemblyPaths)
			{
				// Does an app.config file exist for this assembly?
				string configFile = referencedAssembly + ".config";

				if (File.Exists(configFile))
				{
					ConfigurationUtility.MergeAppConfigFiles(configFile);
				}

				_ReferencedAssemblies.Add(Assembly.Load(Path.GetFileNameWithoutExtension(referencedAssembly)));
			}
		}

		public List<ProviderInfo> Providers
		{
			get
			{
				if (_Providers.Count == 0 && !_BusyLoadingProviders)
				{
					LoadAndFillProviders();
				}
				return _Providers;
			}
		}

		public void PopulateVirtualProperties(IScriptBaseObject obj)
		{
			Type type = obj.GetType();

			if (!_VirtualPropertiesForTypes.ContainsKey(type))
			{
				ArchAngelEditorAttribute[] atts = (ArchAngelEditorAttribute[])type.GetCustomAttributes(_ArchAngelEditorAttributeType, false);

				if (atts.Length > 0 && atts[0].VirtualPropertiesAllowed)
				{
					_VirtualPropertiesForTypes.Add(type, GetVirtualProperties(type));
				}
				else
				{
					_VirtualPropertiesForTypes.Add(type, null);
				}
			}
			List<IOption> options = _VirtualPropertiesForTypes[type];

			if (options == null)
			{
				return;
			}

			foreach (IOption opt in options)
			{
				object defaultValue = null;
				bool found = false;

				if (obj.Ex == null)
				{
					obj.Ex = new List<IUserOption>();
				}
				foreach (IUserOption existingOption in obj.Ex)
				{
					if (existingOption.Name == opt.VariableName)
					{
						defaultValue = existingOption.Value;
						found = true;
						break;
					}
				}
				if (!found)
				{
					obj.AddUserOption(new UserOption(opt.VariableName, opt.VarType, defaultValue));
				}
			}
		}

		public object CallTemplateFunction(string functionName, ref object[] parameters)
		{
			return TemplateLoader.CallTemplateFunction(functionName, ref parameters);
		}

		public bool CallApiExtensionFunction(string name, out object result, ref object[] parameters)
		{
			return TemplateLoader.CallApiExtensionFunction(name, out result, ref parameters);
		}

		public bool DisplayOptionToUser(IOption option, IScriptBaseObject iteratorObject)
		{
			try
			{
				if (option.DisplayToUserValue.HasValue)
				{
					return option.DisplayToUserValue.Value;
				}
				if (string.IsNullOrEmpty(option.DisplayToUserFunction))
				{
					return true;
				}
				if (iteratorObject == null)
				{
					object[] parameters = new object[0];
					return (bool)TemplateLoader.CallTemplateFunction(option.DisplayToUserFunction, ref parameters);
				}
				else
				{
					object[] parameters = new object[] { iteratorObject };
					return (bool)TemplateLoader.CallTemplateFunction(option.DisplayToUserFunction, ref parameters);
				}
			}
			catch (MissingMethodException)
			{
				object[] parameters = new object[] { iteratorObject };
				return (bool)TemplateLoader.CallTemplateFunction(option.DisplayToUserFunction, ref parameters);
			}
		}

		/// <summary>
		/// Gets the default value from the function that has been specified as the DefaultValueFunction.
		/// </summary>
		/// <param name="virtualPropertyName"></param>
		/// <param name="iteratorObject"></param>
		/// <returns></returns>        
		public object GetVirtualPropertyDefaultValue(string virtualPropertyName, object iteratorObject)
		{
			string functionName = string.Format("{0}_{1}Default", iteratorObject.GetType().FullName.Replace("+", ".").Replace(".", "_"), virtualPropertyName);
			//string functionName = virtualPropertyName;

			try
			{
				object[] parameters = new[] { iteratorObject };
				return CallTemplateFunction(functionName, ref parameters);
			}
			catch (MissingMethodException)
			{
				object[] parameters = new object[0];
				return CallTemplateFunction(functionName, ref parameters);
			}
		}

		public bool IsValid(string validationFunctionName, object objectToCheck, out string failReason)
		{
			if (typeof(IOption).IsInstanceOfType(objectToCheck))
			{
				IOption opt = (IOption)objectToCheck;

				if (opt.IsValidValue.HasValue)
				{
					failReason = "";
					return opt.IsValidValue.Value;
				}
			}
			failReason = "";
			object[] objs = new[] { objectToCheck, failReason };
			bool isValid = (bool)TemplateLoader.CallTemplateFunction(validationFunctionName, ref objs);
			failReason = (string)objs[1];
			return isValid;
		}

		public void UnloadAppDomains()
		{
			foreach (var appDomain in _ProviderAppDomains)
			{
				if (appDomain != null)
				{
					AppDomain.Unload(appDomain);
				}
			}
		}

		public IOption FindOption(string name, string iteratorName)
		{
			foreach (IOption option in Options)
			{
				if (option.VariableName == name && option.IteratorName == iteratorName)
				{
					return option;
				}
			}
			return null;
		}

		public void PerformPreAnalysisActions()
		{
			foreach (ProviderInfo provider in Providers)
			{
				provider.PerformPreAnalysisActions();
			}
		}

		public static string GetOptionsXmlPathForProjectFile(string projectFile)
		{
			string directory = Directory.GetParent(projectFile).FullName;
			return directory.PathSlash("Project Files").PathSlash("options.xml");
		}
	}
}
