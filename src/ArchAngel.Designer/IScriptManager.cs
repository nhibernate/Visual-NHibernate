namespace ArchAngel.Designer
{
	/// <summary>
	/// Summary description for IScriptManager.
	/// </summary>
	public interface IScriptManager
	{
		void CompileAndExecuteFile(string[] files, string[] args, IScriptManagerCallback callback, string[] filesToEmbed);
	}
}
