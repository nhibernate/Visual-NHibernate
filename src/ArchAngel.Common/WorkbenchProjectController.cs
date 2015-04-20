using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using ArchAngel.Interfaces;
using log4net;
using Slyce.Common;

namespace ArchAngel.Common
{
	public class WorkbenchProjectController : IWorkbenchProjectController
	{
		public delegate void ProjectIsDirtyDelegate();

		/// <summary>
		/// Raised when IsDirty is set to true.
		/// </summary>
		public event ProjectIsDirtyDelegate OnProjectModification;
		/// <summary>
		/// Raised when the project is saved.
		/// </summary>
		public event EventHandler<EventArgs> OnProjectSave;
		public event TemplateLoadedDelegate OnTemplateLoaded;
		public event ProjectLoadedDelegate OnProjectLoaded;

		public int FileVersionLatest { get; set; }
		public int FileVersion { get; set; }
		public bool AAZFound { get; set; }
		public virtual bool IsDirty { get; set; }
		public bool WritingToUserFolder { get; set; }
		public string AppConfigFileName { get; set; }
		public IWorkbenchProject CurrentProject { get; set; }
		public IVerificationIssueSolver VerificationIssueSolver { get; set; }
		public bool BusyPopulating { get; set; }
		private static readonly ILog log = LogManager.GetLogger(typeof(WorkbenchProjectController));

		protected WorkbenchProjectController()
		{
			VerificationIssueSolver = new NullVerificationIssueSolver();
		}

		public void RaiseTemplateLoadedEvent()
		{
			if (OnTemplateLoaded != null)
			{
				OnTemplateLoaded();
			}
		}

		public void RaiseProjectLoadedEvent()
		{
			if (OnProjectLoaded != null)
			{
				OnProjectLoaded();
			}
		}

		public virtual bool OpenProjectFile(string file)
		{
			log.DebugFormat("Attempting to load project file \"{0}\"", file);
			BusyPopulating = true;
			BeforeOpenProjectFile();
			bool result = false;
			try
			{
				CurrentProject = new WorkbenchProject();
				SharedData.CurrentProject = CurrentProject;
				result = CurrentProject.Load(file, VerificationIssueSolver);

				if (!result)
				{
					System.Windows.Forms.MessageBox.Show("Load unsuccessful");
				}
				RaiseTemplateLoadedEvent();
				RaiseProjectLoadedEvent();
			}
			catch (OldVersionException ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message, "Out-Of-Date Template", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
				result = false;
			}
			//catch (Exception e)
			//{
			//    ErrorReportingService.ReportUnhandledException(e);
			//    result = false;
			//}
			finally
			{
				AfterOpenProjectFile(result, CurrentProject.ProjectFile);
			}
			BusyPopulating = false;
			return result;
		}

		public bool SaveProjectFileAs(string filename, out string errorMessage)
		{
			if (filename == null) throw new ArgumentNullException("filename");

			CurrentProject.ProjectFile = filename;
			return SaveProjectFile(out errorMessage);
		}

		public bool SaveProjectFile(out string errorMessage)
		{
			errorMessage = "";
			WritingToUserFolder = true;
			//try
			//{
			if (string.IsNullOrEmpty(CurrentProject.ProjectFile) ||
				!Directory.Exists(Path.GetDirectoryName(CurrentProject.ProjectFile)))
			{
				throw new InvalidOperationException("Project Filename hasn't been set or the directory doesn't exist.");
				//toolStripMenuItemFileSaveAs_Click(null, null);
			}

			string tempFolder = PathHelper.GetUniqueTempDir("ArchAngel");

			if (Directory.Exists(tempFolder))
			{
				Utility.DeleteDirectoryBrute(tempFolder);
			}
			if (ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject == null)
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject = ArchAngel.Common.UserTemplateHelper.GetDefaultTemplate();

			CurrentProject.ProjectSettings.UserTemplateName = CurrentProject.TemplateProject.Name;
			string versionFile = Path.Combine(tempFolder, "version.txt");

			Directory.CreateDirectory(tempFolder);
			ExtraSaveSteps(tempFolder);
			File.WriteAllText(versionFile, FileVersionLatest.ToString());
			CurrentProject.SaveAppConfig(tempFolder);
			var providerFolders = SaveProviders(tempFolder);


			//string zipFile = Path.Combine(Path.GetDirectoryName(CurrentProject.ProjectFile), Path.GetFileNameWithoutExtension(CurrentProject.ProjectFile) + ".aaproj");
			//try
			//{
			//    //Slyce.Common.Utility.DeleteFileBrute(zipFile);
			//    Utility.ZipFile(tempFolder, zipFile);
			//}
			//catch (Exception)
			//{
			//    if (File.Exists(zipFile) && (File.GetAttributes(zipFile) & FileAttributes.ReadOnly) != 0)
			//    {
			//        // We don't need to show a message to the user here, because ZipFile() already does that.
			//        //MessageBox.Show("Cannot save because project file is readonly: " + zipFile, "Cannot Save - ReadOnly", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//        return false;
			//    }

			//    throw;
			//}

			// Delete the temp folder
			//Slyce.Common.Utility.DeleteDirectoryBrute(tempFolder);

			// ## New Project Save Method ## //

			string outputDirectory = Path.Combine(Path.GetDirectoryName(CurrentProject.ProjectFile), ProviderHelper.GetProjectFilesDirectoryName(CurrentProject.ProjectFile));

			for (int i = 0; i < 3; i++)
			{
				try
				{
					Utility.CopyDirectory(tempFolder, outputDirectory, true);
				}
				catch (Exception ex)
				{
					if (i == 2)
					{
						errorMessage = "Save failed: " + ex.Message;
						return false;
					}
					else
						System.Threading.Thread.Sleep(100);
				}
			}
			string newFilename = Path.ChangeExtension(CurrentProject.ProjectFile, ".wbproj");

			string projectFileText = CreateProjectDescriptorFile(newFilename, providerFolders);
			File.WriteAllText(newFilename, projectFileText);

			CurrentProject.ProjectFile = newFilename;
			OnSaveSuccess();

			// ## New Project Save Method ## //

			SharedData.RegistryUpdateValue("DefaultConfig", CurrentProject.ProjectFile);
			IsDirty = false;
			RaiseSaveEvent();
			// To show user something has happened
			//System.Threading.Thread.Sleep(500);
			return true;
			//}
			//catch (Exception ex)
			//{
			//    ErrorReportingService.ReportUnhandledException(ex);
			//    return false;
			//}
		}


		public bool SaveTemplate()
		{
			throw new NotImplementedException("Not coded yet.");
		}

		public bool SaveTemplateFileAs(string filename)
		{
			throw new NotImplementedException("Not coded yet.");
		}

		private string CreateProjectDescriptorFile(string projectFilePath, IEnumerable<string> providerFolders)
		{
			StringBuilder sb = new StringBuilder();
			XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true, IndentChars = "\t" });

			// ReSharper disable PossibleNullReferenceException
			writer.WriteStartElement("Project");

			//writer.WriteElementString("Folder", GetProjectFilesDirectoryName(projectFilePath));
			foreach (string folder in providerFolders)
			{
				writer.WriteElementString("ProviderFolder", folder);
			}

			writer.WriteEndElement();
			writer.Flush();
			writer.Close();
			// ReSharper restore PossibleNullReferenceException

			return sb.ToString();
		}

		/// <summary>
		/// Called when a sucessful save has occurred
		/// </summary>
		protected virtual void OnSaveSuccess()
		{
		}

		protected virtual void ExtraSaveSteps(string tempFolder)
		{
		}

		/// <summary>
		/// Save all provider-related data.
		/// </summary>
		private List<string> SaveProviders(string folder)
		{
			List<string> providerFolderNames = new List<string>();

			if (CurrentProject != null)
			{
				foreach (ProviderInfo provider in CurrentProject.Providers)
				{
					string tempFolder = Path.Combine(Path.GetTempPath(), provider.Assembly.GetName().Name.Replace(".", "_") + "Temp");

					if (Directory.Exists(tempFolder))
						Utility.DeleteDirectoryContentsBrute(tempFolder);

					Directory.CreateDirectory(tempFolder);

					int tries = 0;

					while (!Directory.Exists(tempFolder) && tries < 20)
					{
						tries++;
						System.Threading.Thread.Sleep(50);
					}
					// Get the Provider to save it's file to its own folder.
					provider.Save(tempFolder);

					string folderName = provider.Assembly.GetName().Name.Replace(".", "_") + "_data";
					providerFolderNames.Add(folderName);
					string fullFolderName = Path.Combine(folder, folderName);

					if (Directory.Exists(fullFolderName))
						Utility.DeleteDirectoryBrute(fullFolderName);

					if (!Directory.Exists(Path.GetDirectoryName(folder)))
						Directory.CreateDirectory(folder);

					Directory.Move(tempFolder, fullFolderName);
				}
			}
			return providerFolderNames;
		}

		protected virtual void BeforeOpenProjectFile()
		{
		}

		protected virtual void AfterOpenProjectFile(bool successful, string file)
		{
		}

		public void CreateNewProject(string projectOutputPath, string projectTemplate, string projectFilename)
		{
			CurrentProject = new WorkbenchProject();
			SharedData.CurrentProject = CurrentProject;

			CurrentProject.NewProject(projectOutputPath, projectTemplate, projectFilename);

			RaiseTemplateLoadedEvent();
			RaiseProjectLoadedEvent();
		}

		public void InitProjectFromProjectWizardInformation(INewProjectInformation information)
		{
			CurrentProject.InitProjectFromProjectWizardInformation(information);
		}

		public void LoadTemplate(string name)
		{
			CurrentProject.LoadTemplate(name);
			RaiseTemplateLoadedEvent();
		}

		public virtual string GetTempFilePathForComponent(ComponentKey componentKey)
		{
			string combine = Path.Combine(CurrentProject.TempFolder, componentKey.ToString());
			return combine;
			/*
						return PathHelper.GetTempFilePathFor("ArchAngel", CurrentProject.ProjectFile, componentKey);
			*/
		}

		public void SaveFileTreeStatus(ProjectFileTree model)
		{
			SaveFileTreeStatus(model, "");
		}

		///<summary>
		///</summary>
		public void SaveFileTreeStatus(ProjectFileTree model, string savePath)
		{
			string folderPath = GetTempFilePathForComponent(ComponentKey.Workbench_Scratch);
			string xmlFilename = Path.Combine(folderPath, "filetree.xml");

			if (Directory.Exists(folderPath) == false)
				Directory.CreateDirectory(folderPath);

			File.Create(xmlFilename).Close();
			XmlDocument document = model.WriteIntelliMergeAndEnableStatusToXml();
			document.Save(xmlFilename);

			if (!string.IsNullOrEmpty(savePath))
			{
				Slyce.Common.Utility.DeleteFileBrute(savePath);

				var savePathDir = Path.GetDirectoryName(savePath);

				if (!Directory.Exists(savePathDir))
					Directory.CreateDirectory(savePathDir);

				File.Copy(xmlFilename, savePath);
			}
		}

		public void RaiseSaveEvent()
		{
			if (OnProjectSave != null)
			{
				OnProjectSave(this, new EventArgs());
			}
		}

		protected void RaiseOnProjectModificationEvent()
		{
			if (OnProjectModification != null)
			{
				OnProjectModification();
				OnProjectModificationInternal();
			}
		}

		protected virtual void OnProjectModificationInternal()
		{

		}
	}
}
