using ArchAngel.Designer.Properties;

namespace ArchAngel.Designer
{
	/// <summary>
	/// Methods to be called as a library function, from external code.
	/// </summary>
	public class ExternalFunctions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns>True if successfully compiled, false otherwise.</returns>
		public static bool CompileStzFile(string fileName)
		{
			Project.Instance.Clear();
			Project.Instance.ProjectFileName = fileName;
			Project.Instance.CompileFolderName = "";
			Project.Instance.Open(fileName);
			Settings.Default.CodeFile = System.IO.Path.GetTempFileName();

			if (CompileHelper.Compile(false))
			{
				return true;
			}
			return false;
		}
	}
}
