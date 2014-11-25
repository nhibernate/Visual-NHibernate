using System;
using System.Drawing;
using ArchAngel.Common;
using Slyce.Common;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ArchAngel.Workbench
{
	public interface IController : IWorkbenchProjectController
    {
        /// <summary>
        /// Raised when there is a change in the User's project directory.
        /// </summary>
        event Controller.UserFilesChangedDelegate UserFilesChanged;

        event Controller.GenerationStartedDelegate GenerationStarted;
        event Controller.CompileErrorsDelegate OnCompileErrors;

        /// <summary>
        /// Gets raised when any data changes one of the providers.
        /// </summary>
        event Interfaces.Events.DataChangedEventDelegate OnDataChanged;

        string[] RecentFiles { get; set; }

        Size WindowSize { get; set; }

		//void SettingSet(Controller.SettingNames setting, object value);
		//object SettingGet(Controller.SettingNames setting);
		//void SaveSettings();

//        SlyceMergeWorker SlyceMergeWorker { get; set; }

        bool MustDisplaySplash { get; set; }

        FormMain MainForm { get; set; }

        bool CommandLine { get; set; }

		bool ApplicationClosing
        {
            get;
            set;
        }

        
        bool CreatingNewProject { get; set; }
        ProjectFileTree ProjectFileTreeModel { get; }

        void ShadeMainForm();
        void UnshadeMainForm();

        void RaiseGenerationStartedEvent();

    	void LoadOptions(string fileName);
    	void LoadOptions();
        void LoadFileTreeStatus(ProjectFileTree model);
		bool IsValidOutputPath(string folder);
        void RaiseCompileErrors(List<System.CodeDom.Compiler.CompilerError> errors);
    }
}