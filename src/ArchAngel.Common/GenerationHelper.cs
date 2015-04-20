using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Exceptions;
using ArchAngel.Interfaces.ITemplate;
using log4net;
using Slyce.Common;
using Slyce.IntelliMerge.Controller;
using Version = System.Version;

namespace ArchAngel.Common
{
	/// <summary>
	/// Helper class that is used to run the generation process on a Project.
	/// </summary>
	public class GenerationHelper
	{
		private IScriptBaseObject CurrentRootObject;

		private readonly ITemplateLoader _Loader;
		private readonly IWorkbenchProject _Project;
		private readonly ITaskProgressHelper<GenerateFilesProgress> _ProgressHelper;
		private readonly IFileController _FileController;
		private string absoluteBasePath;
		private bool addToProjectFileTree = true;

		private static readonly ILog log = LogManager.GetLogger(typeof(GenerationHelper));

		/// <param name="fileController"></param>
		/// <param name="progressHelper">The TaskProgressHelper to use to report progress and cancel the operation.</param>
		/// <param name="projectInfo">The Project we are generating files from.</param>
		/// <param name="loader"></param>
		public GenerationHelper(ITaskProgressHelper<GenerateFilesProgress> progressHelper, ITemplateLoader loader, IWorkbenchProject projectInfo, IFileController fileController)
		{
			_Loader = loader;
			_ProgressHelper = progressHelper;
			_Project = projectInfo;
			_FileController = fileController;
		}

		/// <summary>
		/// Runs through a project and generates the files in it.
		/// </summary>
		/// <param name="folderName">The name of the root folder to generate into. Not the full path, just the relative path to the 
		/// current folder.</param>
		/// <param name="folder"></param>
		/// <param name="parentNode">If this is null, the files will just be generated, not </param>
		/// <param name="thisLevelRootObject"></param>
		/// <returns></returns>
		/// <param name="basePath">The path to which the files should be generated</param>
		public int GenerateAllFiles(string folderName, IFolder folder, ProjectFileTreeNode parentNode, IScriptBaseObject thisLevelRootObject, string basePath)
		{
			return GenerateAllFiles(folderName, folder, parentNode, thisLevelRootObject, basePath, true);
		}

		public int GenerateAllFiles(string folderName, IFolder folder, ProjectFileTreeNode parentNode, IScriptBaseObject thisLevelRootObject, string basePath, bool isTopLevel)
		{
			if (isTopLevel)
			{
				SharedData.IsBusyGenerating = true;
				_Project.StartNewFileGenerationRun();

				// Reset the Template before the File name validation run.
				_Loader.CallTemplateFunction(TemplateHelper.ClearTemplateCacheFunctionName);

				// Run the pre generation template function.

				//var data = new PreGenerationData { OutputFolder = _Project.ProjectSettings.OutputPath };

				//foreach (var uo in _Project.Options.Where(o => o.IsVirtualProperty == false))
				//{
				//    var optionValue = _Loader.GetUserOption(uo.VariableName);
				//    data.UserOptions.Add(uo.VariableName, optionValue);
				//}

				//foreach (var provider in _Project.Providers)
				//{
				//    ArchAngel.Interfaces.ProviderInfo[] otherProviders = new ProviderInfo[_Project.Providers.Count];
				//    _Project.Providers.CopyTo(otherProviders);
				//    data.OtherProviderInfos = otherProviders.ToList();
				//    data.OtherProviderInfos.Remove(provider);
				//    provider.InitialisePreGeneration(data);
				//    //_Loader.CallPreGenerationInitialisationFunction(provider, data);
				//}

				IEnumerable<FilenameInfo> duplicates;
				DuplicateFileNameChecker checker = new DuplicateFileNameChecker(this, _Project, _Project.ProjectSettings.OutputPath);
				bool validates = checker.ValidateFileNames(folderName, folder, thisLevelRootObject, out duplicates);

				if (validates == false)
				{
					_ProgressHelper.ReportProgress(100, new GenerateFilesProgress(0, new DuplicateFilesException(duplicates)));
					return 0;
				}

				// Reset the Template again before the real template run.
				object[] parameters = new object[0];
				_Loader.CallTemplateFunction(TemplateHelper.ClearTemplateCacheFunctionName, ref parameters);

				//foreach (var provider in _Project.Providers)
				//{
				//    _Loader.CallPreGenerationInitialisationFunction(provider, data);
				//}
			}

			if (parentNode == null && isTopLevel)
				addToProjectFileTree = false;

			if (_Loader == null)
				return 0;

			if (addToProjectFileTree && parentNode is ProjectFileTree)
			{
				((ProjectFileTree)parentNode).TreeRestructuring = true;
				((ProjectFileTree)parentNode).Clear();
			}
			int fileCount = 0;

			try
			{
				CurrentRootObject = thisLevelRootObject;
				absoluteBasePath = basePath;

				{
					Version version = new Version(_Loader.GetAssemblyVersionNumber());
					Version expectedVersion = new Version(1, 1, 9, 49);
					if (version < expectedVersion)
					{
						throw new OldVersionException("The template was compiled with an old version of ArchAngel, and cannot be used in this version of Workbench");
					}
				}

				foreach (IFolder subFolder in folder.SubFolders)
				{
					if (_ProgressHelper.IsCancellationPending())
					{
						_ProgressHelper.Cancel();
						return fileCount;
					}

					ProjectFileTreeNode folderNode = null;

					if (addToProjectFileTree && parentNode != null && subFolder.Name != "ROOT")
					{
						folderNode = parentNode.AddChildNode(subFolder.Name);
						folderNode.IsFolder = true;
					}

					if (!string.IsNullOrEmpty(subFolder.IteratorName))
					{
						// The folder has an iterator
						ProviderInfo provider;
						Type iteratorType = _Project.GetIteratorTypeFromProviders(subFolder.IteratorName, out provider);

						if (_ProgressHelper.IsCancellationPending())
						{
							_ProgressHelper.Cancel();
							return fileCount;
						}

						object[] iteratorObjects;

						if (thisLevelRootObject == null)
						{
							iteratorObjects = provider.GetAllObjectsOfType(iteratorType.FullName).ToArray();
						}
						else if (iteratorType.IsInstanceOfType(thisLevelRootObject))
						{
							iteratorObjects = new[] { thisLevelRootObject };
						}
						else
						{
							iteratorObjects = provider.GetAllObjectsOfType(iteratorType.FullName, thisLevelRootObject).ToArray();
						}

						if (iteratorObjects != null)
						{
							foreach (IScriptBaseObject iteratorObject in iteratorObjects)
							{
								if (_ProgressHelper.IsCancellationPending())
								{
									_ProgressHelper.Cancel();
									return fileCount;
								}

								CurrentRootObject = iteratorObject;

								string subFolderName = UpdateScriptName(iteratorObject, subFolder);

								if (folderNode != null)
								{
									folderNode.Text = subFolderName;
								}

								subFolderName = Path.Combine(folderName, subFolderName);

								fileCount += GenerateAllFiles(subFolderName, subFolder, folderNode, CurrentRootObject, basePath, false);

							}
						}
					}
					else
					{
						// The folder doesn't have an iterator
						if (_ProgressHelper.IsCancellationPending())
						{
							_ProgressHelper.Cancel();
							return fileCount;
						}
						string subFolderName = UpdateScriptName(null, subFolder);

						if (folderNode != null)
						{
							folderNode.Text = subFolderName;
						}
						subFolderName = Path.Combine(folderName, subFolderName);

						fileCount += GenerateAllFiles(subFolderName, subFolder, folderNode, thisLevelRootObject, basePath, false);
					}
				}

				foreach (IScript script in folder.Scripts)
				{
					if (_ProgressHelper.IsCancellationPending())
					{
						_ProgressHelper.Cancel();
						return fileCount;
					}
					fileCount += CreateScriptFile(folderName, script, parentNode);
				}

				foreach (IFile file in folder.Files)
				{
					if (_ProgressHelper.IsCancellationPending())
					{
						_ProgressHelper.Cancel();
						return fileCount;
					}
					fileCount += CreateStaticFile(folderName, file, parentNode);
				}
			}
			catch (Exception e)
			{
				_ProgressHelper.ReportProgress(100, new GenerateFilesProgress(fileCount, e));
				return fileCount;
			}
			finally
			{
				if (addToProjectFileTree && parentNode is ProjectFileTree)
				{
					((ProjectFileTree)parentNode).TreeRestructuring = false;
					parentNode.RaiseNodeChangedEvent(parentNode, true);
				}
			}

			if (isTopLevel)
				SharedData.IsBusyGenerating = false;

			_ProgressHelper.ReportProgress(50, new GenerateFilesProgress(fileCount));
			return fileCount;
		}

		/// <summary>
		/// Creates the actual files, or returns a count of the number of files that will be created, depending on value of createFiles.
		/// </summary>
		/// <param name="folderName"></param>
		/// <param name="script"></param>
		/// <param name="parentNode"></param>
		/// <returns></returns>
		private int CreateScriptFile(string folderName, IScript script, ProjectFileTreeNode parentNode)
		{
			int fileCount = 0;

			if (string.IsNullOrEmpty(script.IteratorName))
			{
				if (ProcessScriptObject(null, folderName, script, parentNode))
				{
					fileCount++;
				}
				return fileCount;
			}
			ProviderInfo provider;
			Type iteratorType = _Project.GetIteratorTypeFromProviders(script.IteratorName, out provider);

			IScriptBaseObject[] iteratorObjects = GetIteratorObjects(iteratorType, provider);
			if (iteratorObjects != null)
			{
				if (iteratorType.IsArray)
				{
					if (ProcessScriptObject(iteratorObjects, folderName, script, parentNode))
					{
						fileCount++;
					}
				}
				else
				{
					foreach (IScriptBaseObject iteratorObject in iteratorObjects)
					{
						if (iteratorObject != null && ProcessScriptObject(iteratorObject, folderName, script, parentNode))
						{
							fileCount++;
						}
					}
				}
			}

			return fileCount;
		}

		/// <summary>
		/// Creates the static files and writes them to the Workbench_FileGenerator component directory.
		/// </summary>
		/// <param name="folderName">The relative path of the folder this file will be placed in.</param>
		/// <param name="file"></param>
		/// <param name="parentNode"></param>
		/// <returns>The number of files created.</returns>
		private int CreateStaticFile(string folderName, IFile file, ProjectFileTreeNode parentNode)
		{
			int fileCount = 0;

			if (string.IsNullOrEmpty(file.IteratorName))
			{
				fileCount++;
				string fileName = UpdateScriptName(null, file);
				string relativeFilePath = Path.Combine(folderName, fileName);
				string fullPath = Path.Combine(absoluteBasePath, relativeFilePath);

				if (GetSkipCurrentFile(file, Path.Combine(_Project.ProjectSettings.OutputPath, relativeFilePath)))
				{
					return fileCount;
				}

				_FileController.WriteResourceToFile(_Loader.CurrentAssembly, file.StaticFileName, fullPath);

				_Project.AddGeneratedFile(new GeneratedFile(file.Name, fullPath, relativeFilePath, "", file.IteratorName));

				if (addToProjectFileTree)
				{
					BinaryFile outFile = new BinaryFile(fullPath, false);
					BinaryFileInformation binFileInfo = new BinaryFileInformation();
					binFileInfo.NewGenFile = outFile;
					binFileInfo.RelativeFilePath = relativeFilePath;
					parentNode.AddChildNode(binFileInfo, fileName);
				}
				AddFileCountToPreviousEventAndRefire(_ProgressHelper, 1);

				return fileCount;
			}
			ProviderInfo provider;
			Type iteratorType = _Project.GetIteratorTypeFromProviders(file.IteratorName, out provider);

			IScriptBaseObject[] iteratorObjects = GetIteratorObjects(iteratorType, provider);

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

					if (GetSkipCurrentFile(file, Path.Combine(_Project.ProjectSettings.OutputPath, relativeFilePath)))
					{
						// Skip the file.
						continue;
					}

					_FileController.WriteResourceToFile(_Loader.CurrentAssembly, file.StaticFileName, fullPath);

					_Project.AddGeneratedFile(new GeneratedFile(file.Name, fullPath, relativeFilePath, "", file.IteratorName));

					if (addToProjectFileTree)
					{
						BinaryFile outFile = new BinaryFile(fullPath, false);
						BinaryFileInformation binFileInfo = new BinaryFileInformation();
						binFileInfo.RelativeFilePath = relativeFilePath;
						binFileInfo.NewGenFile = outFile;


						parentNode.AddChildNode(binFileInfo, fileName);
					}
					fileCount++;
					AddFileCountToPreviousEventAndRefire(_ProgressHelper, 1);
				}
			}

			return fileCount;
		}

		internal bool GetSkipCurrentFile(IFile staticFile, string generatedFilename)
		{
			if (string.IsNullOrEmpty(staticFile.StaticFileSkipFunction))
			{
				return false;
			}

			bool skipCurrentFile;
			try
			{
				_Loader.SetGeneratedFileNameOnTemplate(generatedFilename);

				object[] parameters = new object[0];
				// Check whether we must skip the current file
				skipCurrentFile = (bool)_Loader.CallTemplateFunction(staticFile.StaticFileSkipFunction, ref parameters);
			}
			catch
			{
				skipCurrentFile = false;
			}
			if (skipCurrentFile)
				log.InfoFormat("Skipping static file {0}", staticFile.Name);
			return skipCurrentFile;
		}

		internal bool GetSkipCurrentFileOrIsCodeInsertedFile(IScript script, object iteratorObjects, string generatedFilename)
		{
			bool skipCurrentFile;
			bool isCodeInserted;
			try
			{
				object[] parameters = new object[0];
				// Reset the SkipCurrentFile variable
				_Loader.CallTemplateFunction(TemplateHelper.ResetSkipCurrentFileFunctionName, ref parameters);
				// Reset the CurrentFileName variable
				_Loader.CallTemplateFunction(TemplateHelper.ResetCurrentFileNameFunctionName, ref parameters);
				_Loader.SetGeneratedFileNameOnTemplate(generatedFilename);
				// Call the script file function to get the file text body
				parameters = new[] { iteratorObjects };
				// Check whether we must skip the current file
				_Loader.CallTemplateFunction(script.ScriptName, ref parameters);
				parameters = new object[0];
				skipCurrentFile = (bool)_Loader.CallTemplateFunction(TemplateHelper.GetSkipCurrentFileFunctionName, ref parameters);
				string currentFileName = (string)_Loader.CallTemplateFunction(TemplateHelper.GetCurrentFileNameFunctionName, ref parameters);
				isCodeInserted = !string.IsNullOrEmpty(currentFileName);
			}
			catch
			{
				skipCurrentFile = false;
				isCodeInserted = false;
			}
			if (skipCurrentFile)
				log.InfoFormat("Skip Current File = true for script {0}", script.FileName);

			if (isCodeInserted)
				log.InfoFormat("Code Inserted = true for script {0}", script.FileName);

			return skipCurrentFile | isCodeInserted;
		}

		internal bool GetSkipCurrentFile(IScript script, object iteratorObjects)
		{
			bool skipCurrentFile;
			try
			{
				object[] parameters = new object[0];
				// Reset the SkipCurrentFile variable
				_Loader.CallTemplateFunction(TemplateHelper.ResetSkipCurrentFileFunctionName, ref parameters);

				// Call the script file function to get the file text body
				parameters = new[] { iteratorObjects };
				// Check whether we must skip the current file
				_Loader.CallTemplateFunction(script.ScriptName, ref parameters);

				parameters = new object[0];

				skipCurrentFile = (bool)_Loader.CallTemplateFunction(TemplateHelper.GetSkipCurrentFileFunctionName, ref parameters);
			}
			catch
			{
				skipCurrentFile = false;
			}
			if (skipCurrentFile)
				log.InfoFormat("Skipping script {0}", script.FileName);
			return skipCurrentFile;
		}

		internal IScriptBaseObject[] GetIteratorObjects(Type iteratorType, ProviderInfo provider)
		{
			if (CurrentRootObject == null)
			{
				return provider.GetAllObjectsOfType(iteratorType.FullName).ToArray();
			}
			if (iteratorType.IsInstanceOfType(CurrentRootObject))
			{
				return new[] { CurrentRootObject };
			}

			return provider.GetAllObjectsOfType(iteratorType.FullName, CurrentRootObject).ToArray();
		}

		private bool ProcessScriptObject(object scriptObject, string folderName, IScript script, ProjectFileTreeNode parentNode)
		{
			bool success = true;
			string scriptName = UpdateScriptName(scriptObject, script);
			string fileName = Path.Combine(folderName, scriptName);

			_Loader.SetGeneratedFileNameOnTemplate(Path.Combine(_Project.ProjectSettings.OutputPath, fileName));

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
					// Reset the SkipCurrentFile and CurrentFileName variable
					_Loader.CallTemplateFunction(TemplateHelper.ResetSkipCurrentFileFunctionName);
					_Loader.CallTemplateFunction(TemplateHelper.ResetCurrentFileNameFunctionName);
					// Call the script file function to get the file text body
					object[] parameters = new[] { scriptObject };
					// Check whether we must skip the current file
					string templateOutput = (string)_Loader.CallTemplateFunction(script.ScriptName, ref parameters);

					if (_ProgressHelper.IsCancellationPending())
						return false;

					bool skipCurrentFile = (bool)_Loader.CallTemplateFunction(TemplateHelper.GetSkipCurrentFileFunctionName);

					if (!skipCurrentFile)
					{
						templateOutput = Utility.StandardizeLineBreaks(templateOutput, Utility.LineBreaks.Windows);

						string codeInsertionFilename =
							(string)_Loader.CallTemplateFunction(TemplateHelper.GetCurrentFileNameFunctionName);

						if (string.IsNullOrEmpty(codeInsertionFilename))
						{
							string fullPath = Path.Combine(absoluteBasePath, fileName);
							_FileController.WriteAllText(fullPath, templateOutput);

							// The file has successfully been written - add it to the GeneratedFiles
							_Project.AddGeneratedFile(new GeneratedFile(script.FileName, fullPath, fileName, script.ScriptName, script.IteratorName));

							// Set the NewGen text to point to that file.
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
								}
								catch (NotImplementedException)
								{
									fileInfo.TemplateLanguage = null;
								}
							}

							if (addToProjectFileTree)
							{
								parentNode.AddChildNode(fileInfo, scriptName);
							}
						}
						else
						{
							// Code insertions were performed. Need to update a node in the tree.
							// expand the path
							if (Path.IsPathRooted(codeInsertionFilename) == false)
							{
								codeInsertionFilename = RelativePaths.RelativeToAbsolutePath(absoluteBasePath, codeInsertionFilename);
							}
							codeInsertionFilename = _FileController.GetFullPath(codeInsertionFilename);

							// Get the relative path
							string relativeCodeInsertionFilename = RelativePaths.GetRelativePath(_Project.ProjectSettings.OutputPath, codeInsertionFilename);

							// If the file is not under the output path, then reset it to the full path
							if (relativeCodeInsertionFilename.StartsWith(".."))
							{
								relativeCodeInsertionFilename = codeInsertionFilename;

								// We need to add its folder as a root node of the tree
								if (!parentNode.IsTreeRoot)
									parentNode = parentNode.ParentTree;
							}

							fileInfo.RelativeFilePath = relativeCodeInsertionFilename;
							string relPathStart = "." + Path.DirectorySeparatorChar;

							if (fileInfo.RelativeFilePath.StartsWith(relPathStart))
								fileInfo.RelativeFilePath = fileInfo.RelativeFilePath.Substring(2);

							if (addToProjectFileTree)
							{
								ProjectFileTree tree;

								if (!parentNode.IsTreeRoot)
									tree = parentNode.ParentTree;
								else
									tree = (ProjectFileTree)parentNode;

								var possibleNode = tree.GetNodeAtPath(codeInsertionFilename);

								if (possibleNode == null)
								{
									// Need to create this node.


									// Create the node and it's parent folders if need be
									var node = tree.CreateFileNodeForPath(relativeCodeInsertionFilename);
									node.AssociatedFile = fileInfo;
								}
								else
								{
									// Update the NewGen text, don't add it to the generated files list.
									fileInfo = possibleNode.AssociatedFile as TextFileInformation;
									if (fileInfo == null) throw new NotSupportedException("Cannot code merge a binary file");
								}
							}
							else
							{
								// Write the file to disk
								_FileController.WriteAllText(codeInsertionFilename, templateOutput);
							}

							fileInfo.NewGenFile = new TextFile(templateOutput);
						}

						AddFileCountToPreviousEventAndRefire(_ProgressHelper, 1);
					}

				}
				catch (TemplateFunctionException ex)
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
					if (addToProjectFileTree)
					{
						parentNode.AddChildNode(fileInfo, scriptName).GenerationError = new GenerationError(fileName, message);
					}
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

					if (addToProjectFileTree)
					{
						parentNode.AddChildNode(fileInfo, scriptName).GenerationError = new GenerationError(fileName, message);
					}
					// Make sure any other type of exception gets thrown
					throw;
				}
			}
			return success;
		}

		public string UpdateScriptName(object iteratorObject, IFile file)
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
			object[] args = new[] { iteratorObject };

			for (int i = 0; i < replacements.Count; i++)
			{
				string xxx = (string)_Loader.CallTemplateFunction(TemplateHelper.GetDynamicFileNameMethodName(file.Id, i), ref args);
				name = name.Replace(string.Format("#{0}#", replacements[i]), xxx);
			}
			return name;
		}

		public string UpdateScriptName(object iteratorObject, IScript file)
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
			object[] args = new[] { iteratorObject };

			for (int i = 0; i < replacements.Count; i++)
			{
				string xxx = (string)_Loader.CallTemplateFunction(TemplateHelper.GetDynamicFileNameMethodName(file.Id, i), ref args);
				name = name.Replace(string.Format("#{0}#", replacements[i]), xxx);
			}
			return name;
		}

		public string UpdateScriptName(object iteratorObject, IFolder folder)
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
			object[] args = new[] { iteratorObject };

			for (int i = 0; i < replacements.Count; i++)
			{
				string xxx = (string)_Loader.CallTemplateFunction(TemplateHelper.GetDynamicFolderNameMethodName(folder.Id, i), ref args);
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

	public class OldVersionException : Exception
	{
		public OldVersionException(string s)
			: base(s)
		{
		}
	}

	public class DuplicateFilesException : Exception
	{
		public readonly IEnumerable<FilenameInfo> DuplicateFiles;

		public DuplicateFilesException(IEnumerable<FilenameInfo> duplicates)
			: base("There were duplicate files generated.")
		{
			DuplicateFiles = duplicates;
		}
	}

	public class FilenameInfo
	{
		public enum FilenameTypes
		{
			StaticFile,
			GeneratedFile,
			Folder
		}
		public readonly string ProcessedFilename;
		public readonly string RawFilename;
		public readonly object IteratorObject;
		public readonly FilenameTypes FilenameType;
		public string RelativePath;

		public FilenameInfo(string processedFilename, string rawFilename, object iteratorObject, FilenameTypes filenameType)
		{
			ProcessedFilename = processedFilename;
			RawFilename = rawFilename;
			IteratorObject = iteratorObject;
			FilenameType = filenameType;
		}
	}
}
