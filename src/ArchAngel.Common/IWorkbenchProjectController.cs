using System;
using ArchAngel.Interfaces;
using Slyce.Common;

namespace ArchAngel.Common
{
	public interface IWorkbenchProjectController
	{
		/// <summary>
		/// The temp path will be of the form Temp/ArchAngel/Guid/ComponentKey
		/// where Temp is the system temp folder, the Guid is generated from the
		/// project filename, and the ComponentKey is the string representation of 
		/// the part of the ArchAngel system that needs a temp folder.
		/// </summary>
		/// <param name="componentKey">The part of the ArchAngel system that needs the
		/// temp path.</param>
		/// <returns>Path of the form Temp/ArchAngel/Guid/ComponentKey. For a given project,
		/// the temp path will be the same as long as the project filename does not change.</returns>
		string GetTempFilePathForComponent(ComponentKey componentKey);

		int FileVersionLatest { get; set; }
		int FileVersion { get; set; }
		bool AAZFound { get; set; }
		bool IsDirty { get; set; }
		bool WritingToUserFolder { get; set; }
		bool BusyPopulating { get; set; }
		IWorkbenchProject CurrentProject { get; set; }
		IVerificationIssueSolver VerificationIssueSolver { get; set; }


		bool OpenProjectFile(string fileName);

		void CreateNewProject(string projectOutputPath, string projectTemplate, string projectFilename);
		void InitProjectFromProjectWizardInformation(INewProjectInformation information);

		void LoadTemplate(string name);

		bool SaveProjectFileAs(string filename, out string errorMessage);

		bool SaveProjectFile(out string errorMessage);

		bool SaveTemplate();

		bool SaveTemplateFileAs(string filename);

		/// <summary>
		/// Raised when IsDirty is set to true.
		/// </summary>
		event WorkbenchProjectController.ProjectIsDirtyDelegate OnProjectModification;

		/// <summary>
		/// Raised when the project is saved.
		/// </summary>
		event EventHandler<EventArgs> OnProjectSave;

		///<summary>
		///</summary>
		void SaveFileTreeStatus(ProjectFileTree model);
		void SaveFileTreeStatus(ProjectFileTree model, string savePath);

		void RaiseSaveEvent();
		event TemplateLoadedDelegate OnTemplateLoaded;
		event ProjectLoadedDelegate OnProjectLoaded;
		void RaiseTemplateLoadedEvent();
		void RaiseProjectLoadedEvent();
	}
}
