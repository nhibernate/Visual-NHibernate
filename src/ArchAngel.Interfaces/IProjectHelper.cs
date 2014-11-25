using System;
using System.Collections.Generic;
using System.Xml;
using ArchAngel.Interfaces.ITemplate;

namespace ArchAngel.Interfaces
{
	public interface IProjectHelper : IProject
	{
		void Clear();

		IOutput CombinedOutput { get; set; }

		List<BaseAction> Actions { get; set; }

		List<IDefaultValueFunction> DefaultValueFunctions { get; set; }

		List<ProviderInfo> Providers { get; }

		/// <summary>
		/// Gets the virtual properties for the specified type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		List<IOption> GetVirtualProperties(Type type);

		void LoadProviders(string folder);
		void PerformPreAnalysisActions();
		IOption FindOption(string name, string iteratorName);

		List<BaseAction> PreActions { get; }

		List<BaseAction> PostActions { get; }

		void PopulateFunctions(XmlDocument doc);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="assemblySearchFolders">Folders to search for referenced assemblies.</param>
		void Init(XmlDocument doc, List<string> assemblySearchFolders);

		/// <summary>
		/// Gets an array of assemblies that are referenced by this project
		/// </summary>
		List<System.Reflection.Assembly> ReferencedAssemblies { get; }

		bool VirtualPropertiesAreFilled { get; set; }

		string GenerationRootPath { get; set; }

		bool FileSkippingIsImplemented
		{
			get;
			set;
		}

		void UnloadAppDomains();
		Type GetTypeFromProviders(string typeName);
		Type GetTypeFromProviders(string typeName, out ProviderInfo provider);

		/// <summary>
		/// Searches the running assembly as well as all referenced assemblies for the given type.
		/// </summary>
		/// <param name="typeName"></param>
		/// <param name="throwOnError"></param>
		/// <returns></returns>
		Type GetTypeFromReferencedAssemblies(string typeName, bool throwOnError);

		bool DisplayOptionToUser(IOption option, IScriptBaseObject iteratorObject);
		bool IsValid(string validationFunctionName, object objectToCheck, out string failReason);

		/// <summary>
		/// Gets the default value from the function that has been specified as the DefaultValueFunction.
		/// </summary>
		/// <param name="virtualPropertyName"></param>
		/// <param name="iteratorObject"></param>
		/// <returns></returns>
		object GetVirtualPropertyDefaultValue(string virtualPropertyName, object iteratorObject);

		object CallTemplateFunction(string functionName, ref object[] parameters);

		/// <summary>
		/// Adds the required Virtual Properties to all objects in all Providers.
		/// </summary>
		void FillVirtualProperties();

		void PopulateVirtualProperties(IScriptBaseObject obj);
	}
}