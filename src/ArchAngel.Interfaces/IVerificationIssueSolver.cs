namespace ArchAngel.Interfaces
{
	public interface IVerificationIssueSolver
	{
		/// <summary>
		/// Gets a valid path for the template assembly.
		/// </summary>
		/// <returns>A file path that points to a valid template assembly, or null if the user wants to cancel loading the file.</returns>
		string GetValidTemplateFilePath(string projectFile, string oldTemplateFile);
		/// <summary>
		/// Gets a valid folder path for the project output.
		/// </summary>
		/// <returns>A folder path that points to a valid folder, or null if the user wants to cancel loading the file.</returns>
		string GetValidProjectDirectory(string projectFile, string oldOutputFolder);
		/// <summary>
		/// Informs the user that the app config in their project file is missing or invalid. This is an unrecoverable error.
		/// </summary>
		/// <param name="message">The message that should be shown to the user.</param>
		void InformUserThatAppConfigIsInvalid(string message);
	}
}
