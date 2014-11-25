using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using ArchAngel.Interfaces.ITemplate;

namespace ArchAngel.Interfaces
{
	public delegate void TemplateLoadedDelegate();
	public delegate void ProjectLoadedDelegate();

	public interface IWorkbenchProject
	{
		IWorkbenchProjectSettings ProjectSettings { get; set; }
		IOutput CombinedOutput { get; set; }
		string TempFolder { get; }

		IList<GeneratedFile> GeneratedFilesThisSession { get; }
		IList<GeneratedFile> GeneratedFilesLastRun { get; }
		void AddGeneratedFile(GeneratedFile file);
		void StartNewFileGenerationRun();
		ArchAngel.Interfaces.Scripting.NHibernate.Model.IProject ScriptProject { get; set; }
		bool SelectedFilesHaveBeenSet { get; set; }

		void InitialiseProvidersPreGeneration();
		void InitialiseScriptObjects();
		bool CallApiExtensionFunction(string name, out object result, ref object[] parameters);

		/// <summary>
		/// The Loader used to work with the template assembly.
		/// </summary>
		ITemplateLoader TemplateLoader { get; set; }

		/// <summary>
		/// Gets an array of assemblies that are referenced by this project
		/// </summary>
		List<Assembly> ReferencedAssemblies { get; }

		List<ProviderInfo> Providers { get; }
		List<IOption> Options { get; }
		Dictionary<string, System.Windows.Forms.UserControl> OptionForms { get; }
		List<IOutput> Outputs { get; }
		//List<BaseAction> PreActions { get; }
		//List<BaseAction> PostActions { get; }

		/// <summary>
		/// The filename of the appconfig.xml file used to load this project.
		/// </summary>
		string AppConfigFilename { get; set; }

		/// <summary>
		/// The filename of the currently loaded project.
		/// </summary>
		string ProjectFile { get; set; }

		Template.TemplateProject TemplateProject { get; set; }

		bool Load(string projectFilename, IVerificationIssueSolver VerificationIssueSolver);
		bool Load(string projectFilename, IVerificationIssueSolver VerificationIssueSolver, bool skipTemplateLoad, string templateFilename);
		void NewAppConfig();

		/// <summary>
		/// Save the ProjectSettings
		/// </summary>
		/// <param name="folder"></param>
		void SaveAppConfig(string folder);

		/// <summary>
		/// Clears the cached assembly and project objects. Required when user compiles a new AAL file.
		/// </summary>
		void Reset(bool force);

		void LoadProjectInfo(string projectInfoXml);
		void LoadAndFillProviders(string folder);
		void FillProviders();

		/// <summary>
		/// Adds the required Virtual Properties to all objects in all Providers.
		/// </summary>
		void FillVirtualProperties();

		/// <summary>
		/// Gets the virtual properties for the specified type, including options that 
		/// have been specified for base types of the type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		List<IOption> GetVirtualProperties(Type type);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="doc"></param>
		void InitFromDesignerProjectXml(XmlDocument doc);

		/// <summary>
		/// Searches the running assembly as well as all referenced assemblies for the given type.
		/// </summary>
		/// <param name="typeName"></param>
		/// <returns></returns>
		/// <param name="throwOnError"></param>
		Type GetTypeFromReferencedAssemblies(string typeName, bool throwOnError);

		/// <summary>
		/// Gets the Type for the given full type name. If the type
		/// does not exist in one of the loaded provider assemblies
		/// or at all, an exception is thrown.
		/// </summary>
		/// <param name="typeName"></param>
		/// <returns></returns>
		/// <exception cref="TypeNotAnIteratorException">
		/// Thrown if the given type exists, but not as part of a provider.
		/// </exception>
		/// <exception cref="TypeDoesNotExistException">
		/// Thrown if the type cannot be found in the currently loaded assemblies.
		/// </exception>
		Type GetIteratorTypeFromProviders(string typeName);
		/// <summary>
		/// Gets the Type and Provider for the given full type name. If the type
		/// does not exist in one of the loaded provider assemblies
		/// or at all, an exception is thrown.
		/// </summary>
		/// <param name="typeName">The full name of the iterator type you are looking for.</param>
		/// <param name="provider">The provider the type exists in</param>
		/// <returns></returns>
		/// <exception cref="TypeNotAnIteratorException">
		/// Thrown if the given type exists, but not as part of a provider.
		/// </exception>
		/// <exception cref="TypeDoesNotExistException">
		/// Thrown if the type cannot be found in the currently loaded assemblies.
		/// </exception>
		Type GetIteratorTypeFromProviders(string typeName, out ProviderInfo provider);
		void UnloadAppDomains();
		void PopulateVirtualProperties(IScriptBaseObject obj);
		object CallTemplateFunction(string name, ref object[] parameters);
		bool IsValid(string validationFunctionName, object objectToCheck, out string failReason);
		bool DisplayOptionToUser(IOption option, IScriptBaseObject iteratorObject);

		/// <summary>
		/// Gets the default value from the function that has been specified as the DefaultValueFunction.
		/// </summary>
		/// <param name="virtualPropertyName"></param>
		/// <param name="iteratorObject"></param>
		/// <returns></returns>        
		object GetVirtualPropertyDefaultValue(string virtualPropertyName, object iteratorObject);

		IOption FindOption(string name, string iteratorName);
		void PerformPreAnalysisActions();

		/// <summary>
		/// Loads the given template file from disk, sets the template name in the PrjectSettings,
		/// and fires the ProjectLoaded event
		/// </summary>
		/// <param name="templateFileName">The filename of the template to load.</param>
		/// <returns></returns>
		void LoadTemplate(string templateFileName);

		void LoadAppConfig(string appConfigFileName, bool skipTemplateLoad, string templateFilename);
		void SetUserOption(string name, object text);
		object GetUserOption(string name);
		void NewProject(string projectOutputPath, string projectTemplate, string projectFilename);
		void InitProjectFromProjectWizardInformation(INewProjectInformation information);
		object GetDefaultValueOf(IOption option);
		string GetPathRelativeToProjectFile(string name);
		string GetPathAbsoluteFromProjectFile(string name);
	}

	public class TypeNotAnIteratorException : Exception
	{
		public string TypeName { get; private set; }
		public TypeNotAnIteratorException(string typeName, string message)
			: base(message)
		{
			TypeName = typeName;
		}
	}

	public class TypeDoesNotExistException : Exception
	{
		public string TypeName { get; private set; }
		public TypeDoesNotExistException(string typeName, string message)
			: base(message)
		{
			TypeName = typeName;
		}
	}
}