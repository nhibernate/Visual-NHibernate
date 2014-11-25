using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ArchAngel.Interfaces.Wizards.NewProject
{
	public enum NewProjectFormActions
	{
		None,
		NewProject,
		ExistingProject
	}

	public interface IFormNewProject
	{
		NewProjectFormActions SetupAction { get; set; }
		NewProjectFormActions UserChosenAction { get; set; }
		string ExistingProjectPath { get; set; }
		string NewProjectTemplate { get; set; }
		string NewProjectName { get; set; }
		string NewProjectFolder { get; set; }
		string NewProjectOutputPath { get; set; }
		INewProjectInformation NewProjectInformation { get; set; }
		void LoadScreen(Type screenType);
		void LoadScreen(string screenTypeName);
		void Finish();
		void Setup(NewProjectFormActions action);
		void Close();
		void ShowForm(IWin32Window owner, bool appIsStarting);
		string Text { get; set; }
		bool CloseCausedByLoadingNextScreen { get; set; }
		string TemplateName { get; set; }

		//bool OtherScreensFollowCurrent { get; }
		void ClearScreens();
		void AddScreen(INewProjectScreen screen);
		void AddScreens(IEnumerable<INewProjectScreen> screens);
		/// <summary>
		/// Moves the current screen position this number extra ahead when 
		/// LoadNextScreen is called. Calling SkipScreens(1) then LoadNextScreen()
		/// will result in the next screen after the current one being skipped.
		/// Subsequent calls will add to the previous value, so calling
		/// SkipScreens(1); SkipScreens(1); will skip two screens.
		/// </summary>
		/// <seealso cref="ClearScreensToSkip"/>
		/// <param name="numberOfScreensToSkip">The number of additional screens to skip.</param>
		void SkipScreens(int numberOfScreensToSkip);
		/// <summary>
		/// Sets the number of screens to skip at the next LoadNextScreen to 0.
		/// </summary>
		void ClearScreensToSkip();
		/// <summary>
		/// The number of screens that will be skipped at the next LoadNextScreen() call.
		/// If this is greater than the number of screens left in the wizard, the wizard will
		/// Close().
		/// </summary>
		int NumberOfScreensToSkip { get; set; }

		/// <summary>
		/// Stores some data for the duration of this wizard process.
		/// </summary>
		/// <param name="key">The key used to access the data.</param>
		/// <param name="data">The data to be stored.</param>
		void SetScreenData(string key, object data);
		/// <summary>
		/// Gets some previously stored data. Returns null if there is no data stored under that key.
		/// </summary>
		/// <param name="key">The key the data was previously stored under.</param>
		/// <returns>Returns the data previously stored under the given key, or null if there is no data stored under that key.</returns>
		object GetScreenData(string key);
	}

	public interface INewProjectScreen
	{
		IFormNewProject NewProjectForm { get; set; }
		void Setup();
	}
}
