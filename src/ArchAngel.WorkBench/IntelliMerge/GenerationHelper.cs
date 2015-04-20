using System;
using System.Collections.Generic;
using System.IO;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.ITemplate;
using Slyce.IntelliMerge.Controller;
using Slyce.Loader;
using Slyce.Common;
using Version=System.Version;

namespace ArchAngel.Workbench.IntelliMerge
{
	/// <summary>
	/// Helper class that is used to run the generation process on a Project.
	/// </summary>
	public class GenerationHelper
	{
		private IScriptBaseObject CurrentRootObject;

		private ILoader _Loader;
		private IController _Controller;
		private IProjectHelper project;
		private ITaskProgressHelper<GenerateFilesProgress> _ProgressHelper;
		private string absoluteBasePath;

		/// <summary>
		/// Runs through a project and generates the files in it.
		/// </summary>
		/// <param name="progressHelper">The TaskProgressHelper to use to report progress and cancel the operation.</param>
		/// <param name="projectInfo">The Project we are generating files from.</param>
		/// <param name="folderName">The name of the root folder to generate into. Not the full path, just the relative path to the 
		/// current folder.</param>
		/// <param name="folder"></param>
		/// <param name="parentNode"></param>
		/// <param name="thisLevelRootObject"></param>
		/// <returns></returns>
		/// <param name="loader"></param>
		/// <param name="controller"></param>
		public int GenerateAllFiles(ITaskProgressHelper<GenerateFilesProgress> progressHelper, IProjectHelper projectInfo, string folderName, IFolder folder, ProjectFileTreeNode parentNode, IScriptBaseObject thisLevelRootObject, ILoader loader, IController controller)
		{
			if (parentNode is ProjectFileTree)
			{
				((ProjectFileTree)parentNode).TreeRestructuring = true;
				((ProjectFileTree)parentNode).Clear();
			}
			int fileCount = 0;

			try
			{
				_Controller = controller;
				_Loader = loader;
				_ProgressHelper = progressHelper;
				project = projectInfo;
				CurrentRootObject = thisLevelRootObject;
				absoluteBasePath = controller.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator);


				{
					Version version = new Version(loader.GetAssemblyVersionNumber());
					Version expectedVersion = new Version(1, 1, 9, 49);
					if (version < expectedVersion)
					{
						throw new OldVersionException("The template was compiled with an old version of ArchAngel, and cannot be used in this version of Workbench");
					}
				}


				foreach (IFolder subFolder in folder.SubFolders)
				{
					if (progressHelper.IsCancellationPending())
					{
						progressHelper.Cancel();
						return fileCount;
					}

					ProjectFileTreeNode folderNode = null;

					if (parentNode != null && subFolder.Name != "ROOT")
					{
						folderNode = parentNode.AddChildNode(subFolder.Name);
						folderNode.IsFolder = true;
					}

					if (!string.IsNullOrEmpty(subFolder.IteratorName))
					{
						// The folder has an iterator
						ProviderInfo provider;
						Type iteratorType = project.GetTypeFromProviders(subFolder.IteratorName, out provider);

						if (progressHelper.IsCancellationPending())
						{
							progressHelper.Cancel();
							return fileCount;
						}

						if (iteratorType != null)
						{
							IEnumerable<IScriptBaseObject> iteratorObjects;

							if (thisLevelRootObject == null)
							{
								iteratorObjects = provider.GetAllObjectsOfType(iteratorType.FullName);
							}
							else if (iteratorType.IsInstanceOfType(thisLevelRootObject))
							{
								iteratorObjects = new[] { thisLevelRootObject };
							}
							else
							{
								iteratorObjects = provider.GetAllObjectsOfType(iteratorType.FullName, thisLevelRootObject);
							}
							if (iteratorObjects != null)
							{
								foreach (IScriptBaseObject iteratorObject in iteratorObjects)
								{
									if (progressHelper.IsCancellationPending())
									{
										progressHelper.Cancel();
										return fileCount;
									}

									CurrentRootObject = iteratorObject;

									string subFolderName = UpdateScriptName(iteratorObject, subFolder);

									if (folderNode != null)
									{
										folderNode.Text = subFolderName;
									}

									subFolderName = Path.Combine(folderName, subFolderName);

										//Directory.CreateDirectory(Path.Combine(Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator), subFolderName));

									fileCount += GenerateAllFiles(progressHelper, project, subFolderName, subFolder, folderNode, CurrentRootObject, loader, controller);
									
								}
							}
						}
						else
						{
							throw new Exception(string.Format("The IteratorType could not be found: {0}. Are you missing an assembly?", subFolder.IteratorName));
						}
					}
					else
					{
						// The folder doesn't have an iterator
						if (progressHelper.IsCancellationPending())
						{
							progressHelper.Cancel();
							return fileCount;
						}
						string subFolderName = UpdateScriptName(null, subFolder);
						
						if (folderNode != null)
						{
							folderNode.Text = subFolderName;
						}
						subFolderName = Path.Combine(folderName, subFolderName);
						//Directory.CreateDirectory(Path.Combine(Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator), subFolderName));

						fileCount += GenerateAllFiles(progressHelper, projectInfo, subFolderName, subFolder, folderNode, thisLevelRootObject, loader, controller);
					}

					//progressHelper.ReportProgress(20, new GenerateFilesProgress(fileCount));
				}

				//progressHelper.ReportProgress(50, new GenerateFilesProgress(fileCount));

				foreach (IScript script in folder.Scripts)
				{
					if (progressHelper.IsCancellationPending())
					{
						progressHelper.Cancel();
						return fileCount;
					}
					fileCount += CreateScriptFile(progressHelper, folderName, script, parentNode);
				}

				// progressHelper.ReportProgress(80, new GenerateFilesProgress(fileCount));

				foreach (IFile file in folder.Files)
				{
					if (progressHelper.IsCancellationPending())
					{
						progressHelper.Cancel();
						return fileCount;
					}
					fileCount += CreateStaticFile(progressHelper, folderName, file, parentNode);
				}

				//progressHelper.ReportProgress(95, new GenerateFilesProgress(fileCount));

				//Application.DoEvents();
			}
			catch(Exception e)
			{
				progressHelper.ReportProgress(100, new GenerateFilesProgress(fileCount, e));
				return fileCount;
			}
			finally
			{
				if (parentNode is ProjectFileTree)
				{
					((ProjectFileTree) parentNode).TreeRestructuring = false;
					parentNode.RaiseNodeChangedEvent(parentNode, true);
				}
			}
			progressHelper.ReportProgress(50, new GenerateFilesProgress(fileCount));
			return fileCount;
		}

		/// <summary>
		/// Creates the actual files, or returns a count of the number of files that will be created, depending on value of createFiles.
		/// </summary>
		/// <param name="progressHelper"></param>
		/// <param name="folderName"></param>
		/// <param name="script"></param>
		/// <param name="parentNode"></param>
		/// <returns></returns>
		private int CreateScriptFile(ITaskProgressHelper<GenerateFilesProgress> progressHelper, string folderName, IScript script, ProjectFileTreeNode parentNode)
		{
			int fileCount = 0;

			if (string.IsNullOrEmpty(script.IteratorName))
			{
				if (ProcessScriptObject(progressHelper, null, folderName, script, parentNode))
				{
					fileCount++;
				}
				return fileCount;
			}
			ProviderInfo provider;
			Type iteratorType = project.GetTypeFromProviders(script.IteratorName, out provider);

			if (iteratorType != null)
			{
				IEnumerable<IScriptBaseObject> iteratorObjects;

				if (CurrentRootObject == null)
				{
					iteratorObjects = provider.GetAllObjectsOfType(iteratorType.FullName);
				}
				else if (iteratorType.IsInstanceOfType(CurrentRootObject))
				{
					iteratorObjects = new[] { CurrentRootObject };
				}
				else
				{
					iteratorObjects = provider.GetAllObjectsOfType(iteratorType.FullName, CurrentRootObject);
				}
				if (iteratorObjects != null)
				{
					if (iteratorType.IsArray)
					{
						if (ProcessScriptObject(progressHelper, iteratorObjects, folderName, script, parentNode))
						{
							fileCount++;
						}
					}
					else
					{
						foreach (IScriptBaseObject iteratorObject in iteratorObjects)
						{
							if (iteratorObject != null && ProcessScriptObject(progressHelper, iteratorObject, folderName, script, parentNode))
							{
								fileCount++;
							}
						}
					}
				}
			}
			else
			{
				throw new Exception(string.Format("The IteratorType could not be found: {0}. Are you missing an assembly?", script.IteratorName));
			}
			return fileCount;
		}

		/// <summary>
		/// Creates the static files and writes them to the WorkbenchFileGenerator component directory.
		/// </summary>
		/// <param name="progressHelper"></param>
		/// <param name="folderName">The relative path of the folder this file will be placed in.</param>
		/// <param name="file"></param>
		/// <param name="parentNode"></param>
		/// <returns>The number of files created.</returns>
		private int CreateStaticFile(ITaskProgressHelper<GenerateFilesProgress> progressHelper, string folderName, IFile file, ProjectFileTreeNode parentNode)
		{
			int fileCount = 0;

			if (string.IsNullOrEmpty(file.IteratorName))
			{
				fileCount++;
				string fileName = UpdateScriptName(null, file);
				string relativeFilePath = Path.Combine(folderName, fileName);
				string fullPath = Path.Combine(absoluteBasePath, relativeFilePath);

				_Loader.WriteResourceToFile(file.StaticFileName, fullPath);

				BinaryFile outFile = new BinaryFile(fullPath, false);
				BinaryFileInformation binFileInfo = new BinaryFileInformation();
				binFileInfo.NewGenFile = outFile;
				binFileInfo.RelativeFilePath = relativeFilePath;
				parentNode.AddChildNode(binFileInfo, fileName);
				//Project.CurrentProject.AddGeneratedFile(new ProjectHelper.GeneratedFile(file.Name, fileName, fullPath, null, file.IteratorName));
				AddFileCountToPreviousEventAndRefire(progressHelper, 1);

				return fileCount;
			}
			ProviderInfo provider;
			Type iteratorType = project.GetTypeFromProviders(file.IteratorName, out provider);

			if (iteratorType != null)
			{
				IEnumerable<IScriptBaseObject> iteratorObjects;

				if (CurrentRootObject == null)
				{
					iteratorObjects = provider.GetAllObjectsOfType(iteratorType.FullName);
				}
				else if (iteratorType.IsInstanceOfType(CurrentRootObject))
				{
					iteratorObjects = new[] { CurrentRootObject };
				}
				else
				{
					iteratorObjects = provider.GetAllObjectsOfType(iteratorType.FullName, CurrentRootObject);
				}
				if (iteratorObjects != null)
				{
					if (iteratorType.IsArray)
					{
						throw new NotImplementedException("Array iterator types not handled for static files yet. Please inform support@slyce.com about this error.");
					}
					
					foreach (IScriptBaseObject iteratorObject in iteratorObjects)
					{
						string fileName = UpdateScriptName(iteratorObject, file);
						string relativeFilePath = Path.Combine(folderName, fileName);
						string fullPath = Path.Combine(absoluteBasePath, relativeFilePath);

						_Loader.WriteResourceToFile(file.StaticFileName, fullPath);

						BinaryFile outFile = new BinaryFile(fullPath, false);
						BinaryFileInformation binFileInfo = new BinaryFileInformation();
						binFileInfo.RelativeFilePath = relativeFilePath;
						binFileInfo.NewGenFile = outFile;

						
						parentNode.AddChildNode(binFileInfo, fileName);

						fileCount++;
						AddFileCountToPreviousEventAndRefire(progressHelper, 1);
					}
				}
			}
			else
			{
				throw new Exception(string.Format("The IteratorType could not be found: {0}. Are you missing an assembly?", file.IteratorName));
			}
			return fileCount;
		}

		private bool ProcessScriptObject(ITaskProgressHelper<GenerateFilesProgress> progressHelper, object scriptObject, string folderName, IScript script, ProjectFileTreeNode parentNode)
		{
			bool success = true;
			string scriptName = UpdateScriptName(scriptObject, script);
			string fileName = Path.Combine(folderName, scriptName);

			if (scriptName.IndexOf("#") >= 0)
			{
				success = false;
			}
			if (success)
			{
				TextFileInformation fileInfo = new TextFileInformation();
				fileInfo.RelativeFilePath = fileName;

				try
				{
					object[] parameters = new object[0];

					if (project.FileSkippingIsImplemented)
					{
						try
						{
							// Reset the SkipCurrentFile variable
							_Loader.CallTemplateFunction("InternalFunctions.ResetSkipCurrentFile", ref parameters);
						}
						catch
						{
							project.FileSkippingIsImplemented = false;
						}
					}
					// Call the script file function to get the file text body
					parameters = new[] { scriptObject };
					// Check whether we must skip the current file
					string str = (string)_Loader.CallTemplateFunction(script.ScriptName, ref parameters);

					if (progressHelper.IsCancellationPending())
						return false;
					
					parameters = new object[0];

					bool skipCurrentFile = false;

					if (project.FileSkippingIsImplemented)
					{
						skipCurrentFile = (bool)_Loader.CallTemplateFunction("InternalFunctions.MustSkipCurrentFile", ref parameters);
					}
					if (!skipCurrentFile)
					{
						str = Utility.StandardizeLineBreaks(str, Utility.LineBreaks.Windows);
						string fullPath = Path.Combine(
							_Controller.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator),
							fileName);

						_Loader.WriteScriptToFile(str, fullPath);

						fileInfo.NewGenFile = new TextFile(fullPath, false);

						string versionNumberString = _Loader.GetAssemblyVersionNumber();
						VersionNumber versionNumber;
						if (VersionNumber.TryParse(versionNumberString, out versionNumber))
						{
							// Get the template language from the template function.
							string templateLanguageString = _Loader.GetTemplateFunctionLanguage(script.ScriptName);
							try
							{
								fileInfo.TemplateLanguage = SyntaxEditorHelper.LanguageEnumFromName(templateLanguageString);
							} catch (NotImplementedException)
							{
								fileInfo.TemplateLanguage = null;
							}
						}

						parentNode.AddChildNode(fileInfo, scriptName);
						AddFileCountToPreviousEventAndRefire(progressHelper, 1);
					}

				}
				catch (Slyce.Loader.Exceptions.TemplateFunctionException ex)
				{
					success = false;
					string message = "<span class='error'>" + ex.Message + "</span>";

					if (ex.InnerException != null)
					{
						message += ":<br/>" + Environment.NewLine + "<b>" + ex.InnerException.Message + "</b>" +
								   Environment.NewLine + GetCleanTemplateFunctionErrorStackTrace(ex) +
								   Environment.NewLine + "Target Site: " + ex.InnerException.TargetSite;
					}
					RaiseTemplateFunctionCallErrorEvent(ex);
					// Do nothing, just skip the file because the error will get reported to the user.
					parentNode.AddChildNode(fileInfo, scriptName).GenerationError = new GenerationError(fileName, message);
				}
				catch (Exception ex)
				{
					string message = "<span class='error'>" + ex.Message + "</span>";

					if (ex.InnerException != null)
					{
						message += ":<br/>" + Environment.NewLine + "<b>" + ex.InnerException.Message + "</b>" +
								   Environment.NewLine + GetCleanTemplateFunctionErrorStackTrace(ex) +
								   Environment.NewLine + "Target Site: " + ex.InnerException.TargetSite;
					}

					parentNode.AddChildNode(fileInfo, scriptName).GenerationError = new GenerationError(fileName, message);
					// Make sure any other type of exception gets thrown
					throw;
				}
			}
			return success;
		}

        ///// <summary>
        ///// Replaces placeholders in a dynamic filename with the actual names.
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //private string UpdateName(string name)
        //{
        //    foreach (IOption option in project.Options)
        //    {
        //        if (!string.IsNullOrEmpty(option.IteratorName))
        //        {
        //            continue;
        //        }
        //        object propertyValue = _Loader.GetUserOption(option.VariableName);
        //        string propertyValueString = propertyValue == null ? "" : propertyValue.ToString();
        //        name = name.Replace("#UserOptions." + option.VariableName + "#", propertyValueString);
        //        // TODO: This should be removed - it is here for backwards compatibility. UserOptions should be prepended explicitly  with 'UserOptions.'
        //        name = name.Replace("#" + option.VariableName + "#", propertyValueString);
        //    }
        //    PropertyInfo[] properties = typeof(IProjectSettings).GetProperties();

        //    foreach (PropertyInfo property in properties)
        //    {
        //        if (property.GetValue(Controller.Instance.ProjectSettings, null) == null)
        //        {
        //            continue;
        //        }
        //        name = name.Replace("#" + property.Name + "#", property.GetValue(Controller.Instance.ProjectSettings, null).ToString());
        //    }
        //    return name;
        //}

		private string UpdateScriptName(object iteratorObject, IFile file)
		{
            if (file.Name.IndexOf("#") < 0)
            {
                return file.Name;
            }
			List<string> replacements = new List<string>();
			bool inReplacementSection = false;

			for (int i = 0; i < file.Name.Length; i++)
			{
				if (file.Name[i] == '#')
				{
					if (!inReplacementSection)
					{
						inReplacementSection = true;
						replacements.Add("");
					}
					else
					{
						inReplacementSection = false;
					}
				}
				else if (inReplacementSection)
				{
					replacements[replacements.Count - 1] += file.Name[i];
				}
			}
            string name = file.Name;
            object[] args = new object[] { iteratorObject };

			for (int i = 0; i < replacements.Count; i++)
			{    
                string xxx = (string)_Loader.CallTemplateFunction(string.Format("DynamicFunctions.File_{0}_{1}", file.Id, i), ref args);
				name = name.Replace(string.Format("#{0}#", replacements[i]), xxx);
			}
			return name;
		}

        private string UpdateScriptName(object iteratorObject, IScript file)
        {
            if (file.FileName.IndexOf("#") < 0)
            {
                return file.FileName;
            }
            List<string> replacements = new List<string>();
            bool inReplacementSection = false;

            for (int i = 0; i < file.FileName.Length; i++)
            {
                if (file.FileName[i] == '#')
                {
                    if (!inReplacementSection)
                    {
                        inReplacementSection = true;
                        replacements.Add("");
                    }
                    else
                    {
                        inReplacementSection = false;
                    }
                }
                else if (inReplacementSection)
                {
                    replacements[replacements.Count - 1] += file.FileName[i];
                }
            }
            string name = file.FileName;
            object[] args = new object[] { iteratorObject };

            for (int i = 0; i < replacements.Count; i++)
            {
                string xxx = (string)_Loader.CallTemplateFunction(string.Format("DynamicFilenames.File_{0}_{1}", file.Id, i), ref args);
                name = name.Replace(string.Format("#{0}#", replacements[i]), xxx);
            }
            return name;
        }

        private string UpdateScriptName(object iteratorObject, IFolder folder)
        {
            if (folder.Name.IndexOf("#") < 0)
            {
                return folder.Name;
            }
            List<string> replacements = new List<string>();
            bool inReplacementSection = false;

            for (int i = 0; i < folder.Name.Length; i++)
            {
                if (folder.Name[i] == '#')
                {
                    if (!inReplacementSection)
                    {
                        inReplacementSection = true;
                        replacements.Add("");
                    }
                    else
                    {
                        inReplacementSection = false;
                    }
                }
                else if (inReplacementSection)
                {
                    replacements[replacements.Count - 1] += folder.Name[i];
                }
            }
            string name = folder.Name;
            object[] args = new object[] { iteratorObject };

            for (int i = 0; i < replacements.Count; i++)
            {
                string xxx = (string)_Loader.CallTemplateFunction(string.Format("DynamicFolderNames.Folder_{0}_{1}", folder.Id, i), ref args);
                name = name.Replace(string.Format("#{0}#", replacements[i]), xxx);
            }
            return name;
        }

		private void RaiseTemplateFunctionCallErrorEvent(Exception ex)
		{
			if (_ProgressHelper == null) return;

			int generated = 0;

			if (_ProgressHelper.LastProgressObject != null)
				generated = _ProgressHelper.LastProgressObject.NumberOfFilesGenerated;
				
			_ProgressHelper.ReportProgress(40, new GenerateFilesProgress(generated, ex));
		}

		private static void AddFileCountToPreviousEventAndRefire(ITaskProgressHelper<GenerateFilesProgress> progressHelper, int additionalFileCount)
		{
			if (progressHelper.LastProgressObject != null)
			{
				int count = progressHelper.LastProgressObject.NumberOfFilesGenerated + additionalFileCount;
				progressHelper.ReportProgress(40, new GenerateFilesProgress(count));
			}
		}

		private static string GetCleanTemplateFunctionErrorStackTrace(Exception ex)
		{
			string stackTrace = "";

			if (ex.InnerException != null)
			{
				stackTrace = Utility.StandardizeLineBreaks(ex.InnerException.StackTrace, Utility.LineBreaks.Unix);

				int ourCodeStart = stackTrace.IndexOf("ScriptFunctionWrapper.RunScriptFunction");
				ourCodeStart = stackTrace.LastIndexOf("\n", ourCodeStart);

				stackTrace = stackTrace.Substring(0, ourCodeStart);// - 1);
				stackTrace = stackTrace.Replace("TemplateGen.", "");
				stackTrace = stackTrace.Replace(" at ", "<br/> at ");
				stackTrace = "<i>" + stackTrace + "</i>";
			}
			return stackTrace;
		}
	}

	internal class OldVersionException : Exception
	{
		public OldVersionException(string s) : base(s)
		{
		}
	}

	//internal class DuplicateFileException : Exception
	//{
	//    public string ScriptName { get; private set; }
	//    public string GeneratedName { get; private set;}

	//    public DuplicateFileException(string generatedName, string scriptName) : base("Duplicate file created by script: " + scriptName)
	//    {
	//        ScriptName = scriptName;
	//        GeneratedName = generatedName;
	//    }
	//}
}
