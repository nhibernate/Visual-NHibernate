using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using ArchAngel.Designer.DesignerProject;
using Slyce.Common;
using Slyce.Common.Exceptions;
using Slyce.Common.ExtensionMethods;

namespace ArchAngel.Common.DesignerProject
{
	public interface IProjectDeserialiser
	{
		IEnumerable<FunctionInfo> LoadFunctionFiles(string folder);
		IEnumerable<UserOption> LoadUserOptionFiles(string folder);
		void LoadUserOptionsDetails(IEnumerable<UserOption> options, string folder);
		IEnumerable<ApiExtensionMethod> LoadApiExtensionFiles(string folder);
		void LoadProjectFile(string projectFilePath, IDesignerProject project);
		void LoadProject(string projectFilename, IDesignerProject project);
		IEnumerable<IncludedFile> LoadStaticFilenames(string folder);
		OutputFolder LoadOutputsFile(string folder);
		bool IgnoreTypeMissingErrors { get; set; }
		void LoadDefaultApiFunctionBodies(XmlNode functionInformationNode, IDesignerProject project);
	}

	public class ProjectDeserialiserV1 : IProjectDeserialiser
	{
		private readonly IFileController controller;

		public bool IgnoreTypeMissingErrors { get; set; }

		public ProjectDeserialiserV1(IFileController controller)
		{
			this.controller = controller;
		}

		public void LoadProject(string projectFilename, IDesignerProject project)
		{
			LoadProjectFile(projectFilename, project);

			string projectFolder = Path.GetDirectoryName(projectFilename).PathSlash(ProjectSerialiserV1.ProjectFilesFolder);

			var apiExtensions = LoadApiExtensionFiles(projectFolder.PathSlash(ProjectSerialiserV1.ApiExtensionFolder));
			var options = LoadUserOptions(projectFolder.PathSlash(ProjectSerialiserV1.UserOptionFolder));
			var functions = LoadFunctionFiles(projectFolder.PathSlash(ProjectSerialiserV1.FunctionFilesFolder));
			var rootFolder = LoadOutputsFile(projectFolder);
			var includedFiles = LoadStaticFilenames(projectFolder.PathSlash(ProjectSerialiserV1.StaticFilesFolder));

			project.ApiExtensions = apiExtensions;
			project.SetUserOptions(options);

			project.Functions.Clear();
			project.Functions.AddRange(functions);

			project.RootOutput = rootFolder;

			project.ClearIncludedFiles();
			project.AddIncludedFiles(includedFiles);

			project.SetupDynamicfilesAndFolders();
		}

		public IEnumerable<IncludedFile> LoadStaticFilenames(string folder)
		{
			CheckFolder(folder, "StaticFiles");

			string filename = folder.PathSlash(ProjectSerialiserV1.StaticFilesDetailsFileName);

			if (controller.FileExists(filename) == false)
				throw new FileNotFoundException(string.Format("Could not find Outputs file at path {0}", filename));
			if (controller.CanReadFile(filename) == false)
				throw new IOException(string.Format("Cannot read from file {0}: Access Denied.", filename));

			string xml = controller.ReadAllText(filename);

			var files = ReadStaticFilesDetails(xml.GetXmlDocRoot());
			SetFullFileNamesForIncludedFiles(files, folder);
			return files;
		}

		private IEnumerable<UserOption> LoadUserOptions(string folder)
		{
			var options = LoadUserOptionFiles(folder);
			LoadUserOptionsDetails(options, folder);
			return options;
		}

		public virtual IEnumerable<FunctionInfo> LoadFunctionFiles(string folder)
		{
			CheckFolder(folder, "Functions");

			List<FunctionInfo> functions = new List<FunctionInfo>();
			foreach (var filePath in controller.FindAllFilesLike(folder, "*.function.xml"))
			{
				string fileText = controller.ReadAllText(filePath);
				functions.Add(ReadFunction(fileText.GetXmlDocRoot()));
			}
			return functions;
		}

		public virtual IEnumerable<UserOption> LoadUserOptionFiles(string folder)
		{
			CheckFolder(folder, "UserOptions");

			List<UserOption> userOptions = new List<UserOption>();
			foreach (var filePath in controller.FindAllFilesLike(folder, "*.useroption.xml"))
			{
				string fileText = controller.ReadAllText(filePath);
				userOptions.Add(ReadUserOption(fileText.GetXmlDocRoot()));
			}
			return userOptions;
		}

		public void LoadUserOptionsDetails(IEnumerable<UserOption> options, string folder)
		{
			CheckFolder(folder, "UserOptions");

			string projectFileXML = controller.ReadAllText(folder.PathSlash(ProjectSerialiserV1.UserOptionsDetailsFileName));
			ReadUserOptionDetails(options, projectFileXML.GetXmlDocRoot());
		}

		public virtual IEnumerable<ApiExtensionMethod> LoadApiExtensionFiles(string folder)
		{
			CheckFolder(folder, "ApiExtensions");

			var apiExtensionMethods = new List<ApiExtensionMethod>();
			foreach (var filePath in controller.FindAllFilesLike(folder, "*.apiext.xml"))
			{
				string fileText = controller.ReadAllText(filePath);
				apiExtensionMethods.AddRange(ReadApiExtensions(fileText.GetXmlDocRoot()));
			}
			return apiExtensionMethods;
		}

		public virtual void LoadProjectFile(string projectFilePath, IDesignerProject project)
		{
			if (controller.FileExists(projectFilePath) == false)
				throw new FileNotFoundException("Could not find project file at path " + projectFilePath);
			if (controller.CanReadFile(projectFilePath) == false)
				throw new IOException(string.Format("Cannot read from file {0}: Access Denied.", projectFilePath));
			if (project == null)
				throw new DeserialisationException("Cannot load into Project: The given Project was null.");

			string projectFileXML = controller.ReadAllText(projectFilePath);
			ReadProject(projectFileXML.GetXmlDocRoot(), project, projectFilePath);
		}

		public virtual OutputFolder LoadOutputsFile(string folder)
		{
			string outputsFilePath = folder.PathSlash(ProjectSerialiserV1.OutputsFileName);
			if (controller.DirectoryExists(folder) == false)
				throw new DirectoryNotFoundException(string.Format("Could not find Outputs folder at path {0}", folder));
			if (controller.FileExists(outputsFilePath) == false)
				throw new FileNotFoundException(string.Format("Could not find Outputs file at path {0}", outputsFilePath));
			if (controller.CanReadFile(outputsFilePath) == false)
				throw new IOException(string.Format("Cannot read from file {0}: Access Denied.", outputsFilePath));

			string outputsFileXML = controller.ReadAllText(outputsFilePath);
			return ReadOutputs(outputsFileXML.GetXmlDocRoot());
		}

		public virtual void ReadProject(XmlNode projectElement, IDesignerProject project, string projectFilename)
		{
			if (projectElement == null) throw new DeserialisationException("Could not find Project node");

			NodeProcessor proc = new NodeProcessor(projectElement);
			project.ProjectName = proc.GetString("Name");
			project.ProjectDescription = proc.GetString("Description");

			ProcessConfig(projectElement.SelectSingleNode("Config"), project, projectFilename);
		}

		public IEnumerable<IncludedFile> ReadStaticFilesDetails(XmlNode rootElement)
		{
			if (rootElement == null) throw new DeserialisationException("Could not find StaticFiles node");

			List<IncludedFile> paths = new List<IncludedFile>();

			var fileNodes = rootElement.SelectNodes("StaticFile");
			if (fileNodes != null)
				foreach (XmlNode node in fileNodes)
				{
					//					string displayName = node.Attributes["displayname"].Value;
					var file = new IncludedFile(node.Attributes["filename"].Value);
					paths.Add(file);
				}
			return paths;
		}

		public void SetFullFileNamesForIncludedFiles(IEnumerable<IncludedFile> files, string fullPathToStaticFileFolder)
		{
			foreach (var file in files)
			{
				file.FullFilePath = Path.Combine(fullPathToStaticFileFolder, file.FullFilePath);
			}
		}

		protected virtual void ProcessConfig(XmlNode node, IDesignerProject project, string projectFilename)
		{
			NodeProcessor proc = new NodeProcessor(node);

			string relativeCompilePath = proc.GetString("RelativeCompilePath");
			project.CompileFolderName = controller.ToAbsolutePath(relativeCompilePath, projectFilename);

			project.Version = proc.GetString("Version");
			project.ProjType = proc.GetEnum<ProjectTypes>("ProjectType");

			string relativeDebugProjectFile = proc.GetString("DebugProjectFile");
			project.DebugProjectFile = controller.ToAbsolutePath(relativeDebugProjectFile, projectFilename);

			var relativeTestGenerateDirectory = proc.GetString("TestGenerateDirectory");
			project.TestGenerateDirectory = controller.ToAbsolutePath(relativeTestGenerateDirectory, projectFilename);

			var namespaceNodes = node.SelectNodes("IncludedNamespaces/Namespace");
			if (namespaceNodes != null)
				foreach (XmlNode namespaceNode in namespaceNodes)
					project.AddNamespace(namespaceNode.InnerText);

			var refereceNodes = node.SelectNodes("References/Reference");
			ProcessProjectReferences(refereceNodes, project, projectFilename);
		}

		protected virtual void ProcessProjectReferences(XmlNodeList nodes, IDesignerProject project, string projectFilename)
		{
			if (nodes == null) return;

			List<ReferencedFile> referencedFiles = new List<ReferencedFile>();
			foreach (XmlNode referenceNode in nodes)
			{
				NodeProcessor proc = new NodeProcessor(referenceNode);

				string relativeFileName = proc.Attributes.GetString("filename");
				bool mergeWithAssembly = proc.Attributes.GetBool("mergewithassembly");
				bool useInWorkbench = proc.Attributes.GetBool("useinworkbench");
				bool isProvider = proc.Attributes.GetBool("isprovider");

				string fileName = controller.ToAbsolutePath(relativeFileName, projectFilename);

				ReferencedFile file = new ReferencedFile(fileName, mergeWithAssembly, useInWorkbench);
				file.IsProvider = isProvider;

				referencedFiles.Add(file);
			}
			project.SetReferencedFiles(referencedFiles);
		}

		/// <summary>
		/// Creates a FunctionInfo object from the given XML. Because it
		/// has to resolve Type objects, this should only be done after
		/// all referenced assemblies are loaded into the current AppDomain.
		/// </summary>
		/// <param name="rootElement"></param>
		/// <returns></returns>
		public virtual FunctionInfo ReadFunction(XmlNode rootElement)
		{
			if (rootElement == null) throw new DeserialisationException("Could not find Function node");

			CheckVersion(rootElement);

			NodeProcessor proc = new NodeProcessor(rootElement);

			var name = proc.GetString("Name");
			bool isTemplateFunc = proc.GetBool("IsTemplateFunction");
			bool isExtMethod = proc.GetBool("IsExtensionMethod");
			var extendedType = proc.GetString("ExtendedType");
			var scriptLang = proc.GetEnum<SyntaxEditorHelper.ScriptLanguageTypes>("ScriptLanguage");
			var description = proc.GetString("Description");
			var category = proc.GetString("Category");
			var bodyText = proc.GetString("Body");
			var returnType = GetTypeNamed(proc.GetString("ReturnType"));
			var templateReturnLang = proc.GetString("TemplateReturnLanguage");

			FunctionInfo fi = new FunctionInfo(name, returnType, bodyText, isTemplateFunc, scriptLang, description, templateReturnLang, category, isExtMethod, extendedType);

			ProcessFunctionParameters(fi, rootElement);

			return fi;
		}

		protected virtual void ProcessFunctionParameters(FunctionInfo info, XmlNode functionNode)
		{
			var parametersNode = functionNode["Parameters"];
			if (parametersNode == null) throw new DeserialisationException("Could not find Parameters node");

			var parameterNodes = parametersNode.SelectNodes("Parameter");
			if (parameterNodes == null) return;

			foreach (XmlNode paramNode in parameterNodes)
			{
				var name = paramNode.Attributes["name"].Value;
				var typeName = paramNode.Attributes["type"].Value;
				var modifiers = paramNode.Attributes["modifiers"].Value;
				var paramType = GetTypeNamed(typeName);

				ParamInfo pi = new ParamInfo(name, paramType) { Modifiers = modifiers };
				info.AddParameter(pi);
			}

		}

		public virtual IEnumerable<ApiExtensionMethod> ReadApiExtensions(XmlNode rootElement)
		{
			if (rootElement == null)
				throw new DeserialisationException("Could not find API Extension element");

			CheckVersion(rootElement);

			string fullTypeName = rootElement.Attributes["type"].Value;

			Type extendedType = GetTypeNamed(fullTypeName);
			if (extendedType == null)
				throw new DeserialisationException(string.Format("Cannot find the type named {0}, have you loaded all referenced assemblies?", fullTypeName));

			var extensionNodes = rootElement.SelectNodes("ApiExtension");
			if (extensionNodes == null) return new ApiExtensionMethod[0];

			List<ApiExtensionMethod> extensionMethods = new List<ApiExtensionMethod>();

			foreach (XmlNode extensionNode in extensionNodes)
			{
				ApiExtensionMethod extMethod = ReadApiExtension(extensionNode, extendedType);
				extensionMethods.Add(extMethod);
			}

			return extensionMethods;
		}

		protected virtual ApiExtensionMethod ReadApiExtension(XmlNode extensionNode, Type extendedType)
		{
			NodeProcessor proc = new NodeProcessor(extensionNode);
			var methodName = proc.GetString("MethodName");
			MethodInfo method = extendedType.GetMethod(methodName);
			if (method == null)
				throw new DeserialisationException(string.Format("Cannot find method named {0} on type {1}", methodName, extendedType.FullName));

			string overrideText = proc.GetString("OverrideFunctionText");

			ApiExtensionMethod extMethod = new ApiExtensionMethod(method);
			extMethod.OverridingFunctionBody = overrideText;
			return extMethod;
		}

		public virtual UserOption ReadUserOption(XmlNode rootElement)
		{
			if (rootElement == null)
				throw new DeserialisationException("Could not find Option element");

			CheckVersion(rootElement);

			NodeProcessor proc = new NodeProcessor(rootElement);

			var varName = proc.GetString("VariableName");
			var varType = GetTypeNamed(proc.GetString("Type"));
			var displayText = proc.GetString("DisplayText");
			var descr = proc.GetString("Description");
			var defaultValueBody = proc.GetString("DefaultValue");
			var iteratorType = GetTypeNamed(proc.GetString("IteratorName"));
			var validatorBody = proc.GetString("ValidatorFunction");
			var displayToUserBody = proc.GetString("DisplayToUserFunction");
			bool resetPerSesion = proc.GetBool("ResetPerSession");

			List<string> values = new List<string>();
			XmlNodeList valueNodes = rootElement.SelectNodes("Values/Value");
			if (valueNodes != null)
			{
				foreach (XmlNode valueNode in valueNodes)
				{
					values.Add(valueNode.Attributes["value"].Value);
				}
			}

			UserOption uo = new UserOption(varName, "", varType,
										   displayText, descr, values, defaultValueBody,
										   iteratorType, validatorBody, displayToUserBody, resetPerSesion);

			return uo;
		}

		public virtual void ReadUserOptionDetails(IEnumerable<UserOption> userOptions, XmlNode rootElement)
		{
			if (rootElement == null)
				throw new DeserialisationException("Could not find UserOptionsDetails element");

			CheckVersion(rootElement);

			var categories = LoadCategories(rootElement);

			foreach (var userOption in userOptions)
			{
				// The default category is General.
				userOption.Category =
					categories.ContainsKey(userOption.VariableName) ? categories[userOption.VariableName] : "General";
			}
		}

		public virtual void LoadDefaultApiFunctionBodies(XmlNode functionsElement, IDesignerProject project)
		{
			if (functionsElement == null)
				throw new DeserialisationException("Could not find FunctionInformation element");
			CheckVersion(functionsElement);

			project.ClearOverriddenFunctionInformation();

			var functionInformations = new List<OverriddenFunctionInformation>();

			var functions = functionsElement.SelectNodes("Function");

			if (functions == null) return;

			foreach (XmlNode funcNode in functions)
			{
				var proc = new NodeProcessor(funcNode);

				var parameters = proc.GetStrings("Parameter").Select(p => GetTypeNamed(p));


				string functionName = proc.Attributes.GetString("name");
				string typeName = proc.Attributes.GetString("type");

				string xmlcomments = proc.GetString("XmlComments");
				string functionBody = proc.GetString("BodyText");

				functionInformations.Add(
					new OverriddenFunctionInformation(GetTypeNamed(typeName), functionName, parameters, xmlcomments, functionBody));
			}

			project.AddOverriddenFunctionInformation(functionInformations);
		}

		private Dictionary<string, string> LoadCategories(XmlNode docElement)
		{
			var categories = new Dictionary<string, string>();

			var categoryNodes = docElement.SelectNodes("Category");
			if (categoryNodes == null) return categories;

			foreach (XmlNode catNode in categoryNodes)
			{
				string category = catNode.Attributes["name"].Value;

				var userOptionNodes = catNode.SelectNodes("UserOption");
				if (userOptionNodes == null) continue;

				foreach (XmlNode uo in userOptionNodes)
				{
					string value = uo.Attributes["varname"].Value;
					if (categories.ContainsKey(value))
					{
						throw new DeserialisationException("UserOption defined in multiple categories.");
					}
					categories.Add(value, category);
				}
			}

			return categories;
		}

		internal Type GetTypeNamed(string typeName)
		{
			if (string.IsNullOrEmpty(typeName)) return null;

			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
			{
				Type type = asm.GetType(typeName);
				if (type != null) return type;
			}
			if (IgnoreTypeMissingErrors)
				return null;

			throw new DeserialisationException("Could not find type " + typeName + ". A referenced assembly may be missing.");
		}

		internal static void CheckVersion(XmlNode node)
		{
			if (node.Attributes["version"] == null)
				throw new VersionException("Could not find the version number for " + node.Name + " node.");

			string version = node.Attributes["version"].Value;
			if (version != "1")
			{
				throw new VersionException("Cannot load version " + version + " files with a version 1 ProjectDeserialiser");
			}
		}

		private void CheckFolder(string folder, string folderName)
		{
			if (controller.DirectoryExists(folder) == false)
				throw new DirectoryNotFoundException(string.Format("The {0} files folder does not exist", folderName));
			if (controller.CanReadFilesFrom(folder) == false)
				throw new IOException(folderName + " folder is not readable");
		}

		public OutputFolder ReadOutputs(XmlNode rootElement)
		{
			OutputsParserV1 parser = new OutputsParserV1(this);
			return parser.Read(rootElement);
		}

		public static TemplateDefinition GetTemplateDefinitionFromXML(List<string> providerNames, string file, string refFilesXML)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(refFilesXML);

			if (doc.DocumentElement == null)
				throw new DeserialisationException("Template XML is empty");

			string description = doc.DocumentElement.SelectSingleNode("Project/Description").InnerText;

			var referenceNodes = doc.DocumentElement.SelectNodes("Project/Config/References/Reference");
			if (referenceNodes != null)
			{
				foreach (XmlNode refNode in referenceNodes)
				{
					NodeProcessor proc = new NodeProcessor(refNode);
					if (proc.Attributes.GetBool("isprovider"))
					{
						providerNames.Add(Path.GetFileNameWithoutExtension(proc.Attributes.GetString("filename")));
					}
				}
			}

			TemplateDefinition template = new TemplateDefinition(Path.GetFileName(file), description, file, providerNames);
			return template;
		}

		public void LoadFromSingleXmlFile(ProjectBase project, XmlDocument document, string templateFilename)
		{
			XmlElement root = document.DocumentElement;
			if (root == null) throw new DeserialisationException("Designer Project XML does not contain MegaProject root node.");

			// Get each of the XmlElements corresponding to the individual files.
			var projectNode = root.SelectSingleNode("Project");
			var apiExtensions = root.SelectSingleNode("AllApiExtensions");
			var functions = root.SelectSingleNode("Functions");
			var userOptions = root.SelectSingleNode("UserOptions");
			var staticFiles = root.SelectSingleNode("StaticFiles");
			var outputs = root.SelectSingleNode("Outputs");

			ReadProject(projectNode, project, templateFilename);

			// force the project to load its references
#pragma warning disable 168
			var list = project.ReferencedAssemblies;
#pragma warning restore 168

			ReadFunctions(functions, project);
			ReadAllApiExtensions(apiExtensions, project);
			ReadUserOptions(userOptions, project);
			project.AddIncludedFiles(ReadStaticFilesDetails(staticFiles));
			project.RootOutput = ReadOutputs(outputs);

			project.SetupDynamicfilesAndFolders();
		}

		private void ReadAllApiExtensions(XmlNode extensions, IDesignerProject project)
		{
			if (extensions == null)
			{
				project.ApiExtensions = new List<ApiExtensionMethod>();
				return;
			}

			var apiExtensions = new List<ApiExtensionMethod>();
			foreach (XmlNode extNode in extensions)
			{
				apiExtensions.AddRange(ReadApiExtensions(extNode));
			}

			project.ApiExtensions = apiExtensions;
		}

		private void ReadUserOptions(XmlNode options, IDesignerProject project)
		{
			var userOptionNodes = options.SelectNodes("Option");
			if (userOptionNodes == null) return;

			List<UserOption> userOptions = new List<UserOption>();
			foreach (XmlElement uoNode in userOptionNodes)
			{
				var uo = ReadUserOption(uoNode);
				userOptions.Add(uo);
			}

			ReadUserOptionDetails(userOptions, options.SelectSingleNode("UserOptionDetails"));

			project.SetUserOptions(userOptions);
		}

		private void ReadFunctions(XmlNode functions, IDesignerProject project)
		{
			var functionNodes = functions.SelectNodes("Function");
			if (functionNodes == null) return;

			foreach (XmlElement fNode in functionNodes)
			{
				var func = ReadFunction(fNode);
				project.Functions.Add(func);
			}
		}
	}

	public class OutputsParserV1
	{
		private readonly ProjectDeserialiserV1 Deserialiser;

		public OutputsParserV1(ProjectDeserialiserV1 deserialiser)
		{
			Deserialiser = deserialiser;
		}

		public virtual OutputFolder Read(XmlNode rootElement)
		{
			if (rootElement == null)
				throw new DeserialisationException("Could not find Option element");
			ProjectDeserialiserV1.CheckVersion(rootElement);

			return ReadFolder(rootElement.SelectSingleNode("Folder"));
		}

		protected virtual OutputFolder ReadFolder(XmlNode node)
		{
			NodeProcessor proc = new NodeProcessor(node);
			string name = proc.Attributes.GetString("name");
			string id = proc.Attributes.GetString("id");
			string iteratorType = proc.Attributes.Exists("iterator") ? proc.Attributes.GetString("iterator") : null;

			OutputFolder folder = new OutputFolder(name, id);
			if (iteratorType != null)
			{
				folder.IteratorType = Deserialiser.GetTypeNamed(iteratorType);
			}

			// Process child folders.
			ProcessChildrenOfFolder(node, folder);

			return folder;
		}

		private OutputFile ReadStaticFile(XmlNode node)
		{
			NodeProcessor proc = new NodeProcessor(node);
			string name = proc.Attributes.GetString("name");
			string id = proc.Attributes.GetString("id");

			string staticFileName = proc.Attributes.Exists("staticfilename")
										? proc.Attributes.GetString("staticfilename") : null;
			string iteratorname = proc.Attributes.Exists("iteratorname")
									? proc.Attributes.GetString("iteratorname") : null;
			string skipFunction = proc.Attributes.Exists("skipfunction")
									? proc.Attributes.GetString("skipfunction") : null;

			OutputFile file = new OutputFile(name, OutputFileTypes.File, staticFileName, id);
			if (iteratorname != null) file.StaticFileIterator = Deserialiser.GetTypeNamed(iteratorname);
			file.StaticFileSkipFunctionName = skipFunction;

			return file;
		}

		private OutputFile ReadScriptFile(XmlNode node)
		{
			NodeProcessor proc = new NodeProcessor(node);

			string name = proc.Attributes.GetString("name");
			string id = proc.Attributes.GetString("id");
			string scriptName = proc.Attributes.GetString("scriptname");

			if (string.IsNullOrEmpty(scriptName))
				throw new DeserialisationException(string.Format("File {0} has no ScriptName", name));


			return new OutputFile(name, OutputFileTypes.Script, scriptName, id);
		}

		private void ProcessChildrenOfFolder(XmlNode node, OutputFolder folder)
		{
			var folderNodes = node.SelectNodes("Folder");
			if (folderNodes != null)
			{
				foreach (XmlNode child in folderNodes)
				{
					OutputFolder childFolder = ReadFolder(child);
					folder.Folders.Add(childFolder);
				}
			}

			// Process child files.
			var fileNodes = node.SelectNodes("StaticFile");
			if (fileNodes != null)
			{
				foreach (XmlNode child in fileNodes)
				{
					OutputFile childFile = ReadStaticFile(child);
					folder.Files.Add(childFile);
				}
			}

			fileNodes = node.SelectNodes("ScriptFile");
			if (fileNodes != null)
			{
				foreach (XmlNode child in fileNodes)
				{
					OutputFile childFile = ReadScriptFile(child);
					folder.Files.Add(childFile);
				}
			}
		}
	}

	public class VersionException : Exception
	{
		public VersionException(string msg)
			: base(msg)
		{
		}
	}
}