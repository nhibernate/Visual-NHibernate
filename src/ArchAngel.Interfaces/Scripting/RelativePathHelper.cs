
namespace ArchAngel.Interfaces.Scripting
{
	public static class RelativePathHelper
	{
		public static string GetFullPath(string basePath, string relativePath)
		{
			return Slyce.Common.RelativePaths.GetFullPath(basePath, relativePath);
		}

		public static string GetRelativePath(string basePath, string absolutePath)
		{
			return Slyce.Common.RelativePaths.GetRelativePath(basePath, absolutePath);
		}

		public static string RelativeToAbsolutePath(string basePath, string relativePath)
		{
			return Slyce.Common.RelativePaths.RelativeToAbsolutePath(basePath, relativePath);
		}
	}
}
