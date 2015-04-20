using System;
using System.Runtime.Serialization;

namespace ArchAngel.Interfaces
{
	/// <summary>
	/// Represents the common project settings that are stored in appconfig.xml
	/// </summary>
	public interface IWorkbenchProjectSettings : ISerializable
	{
		/// <summary>
		/// The full path of the output directory.
		/// </summary>
		[DotfuscatorDoNotRename]
		string OutputPath { get; set; }

		/// <summary>
		/// The full path of the template used for this project.
		/// </summary>
		[DotfuscatorDoNotRename]
		string TemplateFileName
		{
			get;
			set;
		}

		///<summary>
		/// The unique identifier for this project.
		///</summary>
		[DotfuscatorDoNotRename]
		Guid ProjectGuid { get; set; }

		/// <summary>
		/// Save the project settings to the specified file.
		/// </summary>
		/// <param name="file">The path of the file to save the settings to.</param>
		/// <param name="project">The path that the aaprj file is being saved to.</param>
		void Save(string file, IWorkbenchProject project);
		/// <summary>
		/// Load the project settings from the specified file.
		/// </summary>
		/// <param name="file">The path of the file to load the settings from.</param>
		/// <param name="project">The path that the aaprj file is being opened from.</param>
		void Open(string file, IWorkbenchProject project);

		string UserTemplateName { get; set; }

		bool OverwriteFiles { get; set; }
	}
}