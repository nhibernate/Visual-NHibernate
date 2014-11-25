using ArchAngel.Interfaces.ITemplate;

namespace ArchAngel.Interfaces
{
	public static class TemplateHelper
	{
		public const string LoadFunctionName = "LoadProjectOutput";
		public const string CustomNewProjectScreensFunctionName = "CustomNewProjectScreens";
		public const string PreGenerationModelProcessingFunctionName = "PreGenerationModelInitialisation";

		public const string ResetSkipCurrentFileFunctionName = "InternalFunctions.ResetSkipCurrentFile";
		public const string GetSkipCurrentFileFunctionName = "InternalFunctions.MustSkipCurrentFile";
		public const string ClearTemplateCacheFunctionName = "InternalFunctions.ClearTemplateCache";
		public const string ResetCurrentFileNameFunctionName = "InternalFunctions.ResetCurrentFileName";
		public const string GetCurrentFileNameFunctionName = "InternalFunctions.GetCurrentFileName";
		public const string SetGeneratedFileNameFunctionName = "InternalFunctions.SetGeneratedFileName";

		public const string VirtualPropertiesClass = "VirtualProperties";
		public const string UserOptionsClass = "UserOptions";
		public const string DynamicFilenamesClassName = "DynamicFilenames";
		public const string DynamicFolderNamesClassName = "DynamicFolderNames";

		public static string GetDynamicFileNameMethodName(string id, int i)
		{
			return DynamicFilenamesClassName + "." + ContructFileNameMethodName(id, i);
		}

		public static string GetDynamicFolderNameMethodName(string id, int i)
		{
			return DynamicFolderNamesClassName + "." + ContructFileNameMethodName(id, i);
		}

		public static string ContructFileNameMethodName(string id, int i)
		{
			return string.Format("File_{0}_{1}", id, i);
		}

		public static string ContructFolderNameMethodName(string id, int i)
		{
			return string.Format("Folder_{0}_{1}", id, i);
		}

		public static string GetDefaultValueFunction(IOption option)
		{
			return option.VariableName + "_DefaultValue";
		}

		public static string GetDisplayToUserFunction(IOption option)
		{
			return option.VariableName + "_DisplayToUser";
		}
	}
}