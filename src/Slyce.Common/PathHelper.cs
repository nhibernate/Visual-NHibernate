using System;
using System.IO;

namespace Slyce.Common
{
	public class PathHelper
	{
		/// <summary>
		/// The temp path will be of the form Temp/ArchAngel/Guid/ComponentKey
		/// where Temp is the system temp folder, the Guid is generated from the
		/// project filename, and the ComponentKey is the string representation of 
		/// the part of the ArchAngel system that needs a temp folder.
		/// </summary>
		/// <param name="product">The product we need a temp folder for.</param>
		/// <param name="projectFile">The filename of the project this is for</param>
		/// <param name="componentKey">The part of the ArchAngel system that needs the
		/// temp path.</param>
		/// <returns>Path of the form Temp/ArchAngel/Guid/ComponentKey. For a given project,
		/// the temp path will be the same as long as the project filename does not change.</returns>
		public static string GetTempFilePathFor(string product, string projectFile, ComponentKey componentKey)
		{
			if(string.IsNullOrEmpty(product))
				throw new ArgumentException("Product cannot be null");
			
			// Convert projectFile into a Guid string in the form {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}
			byte[] hash = Utility.GetMD5FromString(projectFile ?? "");
			string filename = new Guid(hash).ToString("B").ToUpper();

			string path = Path.GetTempPath().PathSlash(product).PathSlash(filename).PathSlash(componentKey.ToString());

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			return path;
		}

		public static string GetUniqueTempDir(string product)
		{
			string filename = Guid.NewGuid().ToString("B").ToUpper();
			string path = Path.GetTempPath().PathSlash(product).PathSlash("Scratch").PathSlash(filename);

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			return path;
		}
	}

	public static class PathStringExtensions
	{
		[System.Diagnostics.DebuggerStepThrough]
		public static string PathSlash(this string basePath, string newPart)
		{
			return Path.Combine(basePath, newPart);
		}


	}

	/// <summary>
	/// Each of these keys represents a different component of the system.
	/// </summary>
	public enum ComponentKey
	{
		Workbench,
		Workbench_Scratch,
		Workbench_FileGenerator,
		/// <summary>
		/// This component handles the diff'd and merged files.
		/// </summary>
		Workbench_FileGeneratorOutput,
		Workbench_FileGeneratorPrevGen,
		SlyceMerge_PrevGen,
		ArchAngel_Staging,
		UsersMD5Hashes,
		Provider_Database_Settings,
		Designer_CompileFolder,
		Debugger_LoadAssemblies,
		Designer_TempRun,
		Debugger_FakeProjectDirectory
	}
}
