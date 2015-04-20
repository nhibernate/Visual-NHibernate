using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using UserOption=ArchAngel.Common.DesignerProject.UserOption;

namespace ArchAngel.Designer.DesignerProject
{
	public interface IDesignerProject
	{
		IEnumerable<ApiExtensionMethod> ApiExtensions { get; set; }

		/// <summary>
		/// The AAPROJ file used during debugging to get a real object model.
		/// </summary>
		string DebugProjectFile { get; set; }

		ReadOnlyCollection<IncludedFile> IncludedFiles { get; }
		OutputFolder RootOutput { get; set; }

		[DotfuscatorDoNotRename]
		string ProjectName { get; set; }

		[DotfuscatorDoNotRename]
		string ProjectDescription { get; set; }

		string CompileFolderName { get; set; }
		string Version { get; set; }
		ReadOnlyCollection<UserOption> UserOptions { get; }
		ReadOnlyCollection<ReferencedFile> References { get; }
		ReadOnlyCollection<Assembly> ReferencedAssemblies { get; }
		List<string> Namespaces { get; }
		List<FunctionInfo> Functions { get; }
		ProjectTypes ProjType { get; set; }
		string TestGenerateDirectory { get; set; }
		bool IsDirty { get; set; }
		string ProjectFileName { get; set; }

		event EventHandler DebugProjectFileChanged;
		string GetPathRelativeToProjectFile(string absolutePath);
		string GetFullPath(string pathRelativeToProjectFile);
		void AddReferencedFile(ReferencedFile reference);
		void SetReferencedFiles(IEnumerable<ReferencedFile> files);
		void ClearReferences();

		/// <summary>
		/// Gets a list of all files in the entire hierarchy.
		/// </summary>
		/// <returns></returns>
		List<OutputFile> GetAllFiles();

		/// <summary>
		/// Gets a list of all folders in the entire hierarchy.
		/// </summary>
		/// <returns></returns>
		List<OutputFolder> GetAllFolders();

		void SetupDynamicfilesAndFolders();
		void AddNamespace(string nameSpace);
		void RemoveReference(ReferencedFile file);

		/// <summary>
		/// Finds a function where only one function with this name exists. Throws an exception if 
		/// more than one function exists with a matching name. Only 'ScriptFunctions' should meet
		/// this criteria. API Extension 'overrides' do not meet this criteria.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		FunctionInfo FindFunctionSingle(string name);

		FunctionInfo FindFunction(string name, List<ParameterInfo> parameters);
		FunctionInfo FindFunction(string name, List<ParamInfo> parameters);

		/// <summary>
		/// Gets all functions with the given name. Multiple overloaded functions can be returned.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		List<FunctionInfo> FindFunctions(string name);

		string GetCompiledDLLPath();
		string GetCompiledDLLDirectory();

		/// <summary>
		/// This event is fired whenever IsDirty changes.
		/// </summary>
		event EventHandler IsDirtyChanged;

		event EventHandler ReferencedAssembliesChanged;
		UserOption FindUserOption(string name);
		UserOption FindUserOption(string name, out int index);

		/// <summary>
		/// Adds the option if it doesn't already exist
		/// </summary>
		/// <param name="option"></param>
		void AddUserOption(UserOption option);

		void SetUserOptions(IEnumerable<UserOption> userOptions);
		void ClearUserOptions();
		void RemoveUserOption(UserOption option);

		/// <summary>
		/// Sorts the user options into alphabetical order
		/// </summary>
		void SortUserOptions();

		void ClearIncludedFiles();
		void AddIncludedFile(IncludedFile file);
		void AddIncludedFiles(IEnumerable<IncludedFile> enumerable);
		void AddOverriddenFunctionInformation(IEnumerable<OverriddenFunctionInformation> informations);
		void ClearOverriddenFunctionInformation();
	}
}
