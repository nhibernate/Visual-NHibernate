using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.ITemplate;
using log4net;

namespace ArchAngel.Common
{
	public class DuplicateFileNameChecker
	{
		private readonly GenerationHelper helper;
		private readonly IWorkbenchProject _Project;
		private readonly string _outputFolder;

		private static readonly ILog log = LogManager.GetLogger(typeof (DuplicateFileNameChecker));

		public DuplicateFileNameChecker(GenerationHelper helper, IWorkbenchProject project, string outputFolder)
		{
			this.helper = helper;
			_Project = project;
			_outputFolder = outputFolder;
		}

		public bool ValidateFileNames(string folderName, IFolder folder, IScriptBaseObject thisLevelRootObject, out IEnumerable<FilenameInfo> duplicates)
		{
			log.Info("Validating generated files.");

			List<FilenameInfo> generatedPaths = new List<FilenameInfo>();

			ValidateFileNames(generatedPaths, folder, thisLevelRootObject, folderName);

			Dictionary<string, FilenameInfo> generatedPathSet = new Dictionary<string, FilenameInfo>();
			HashSet<FilenameInfo> tempduplicates = new HashSet<FilenameInfo>();

			foreach (var path in generatedPaths)
			{
				if (generatedPathSet.ContainsKey(path.ProcessedFilename) == false)
				{
					generatedPathSet.Add(path.ProcessedFilename, path);
				}
				else
				{
					tempduplicates.Add(generatedPathSet[path.ProcessedFilename]);
					tempduplicates.Add(path);
				}
			}

			if (tempduplicates.Count > 0)
			{
				duplicates = tempduplicates;
				return false;
			}
			duplicates = new List<FilenameInfo>();
			return true;
		}

		private void ValidateFileNames(List<FilenameInfo> generatedPaths, IFolder folder, IScriptBaseObject thisLevelRootObject, string folderName)
		{
			foreach (IFolder subFolder in folder.SubFolders)
			{
				ValidateFolderName(generatedPaths, subFolder, thisLevelRootObject, folderName);
			}

			foreach (IScript script in folder.Scripts)
			{
				// Check script filename
				ValidateScriptName(generatedPaths, script, folderName);
			}

			foreach (IFile file in folder.Files)
			{
				// Check static filename
				ValidateStaticFileName(generatedPaths, file, folderName);
				//fileCount += CreateStaticFile(folderName, file, parentNode);
			}
		}

		private void ValidateStaticFileName(List<FilenameInfo> generatedPaths, IFile file, string folderName)
		{
			if (string.IsNullOrEmpty(file.IteratorName))
			{
				string fileName = helper.UpdateScriptName(null, file);
				string relativeFilePath = Path.Combine(folderName, fileName);

				log.InfoFormat("<Static> {0}, no iterator", relativeFilePath);

				if (helper.GetSkipCurrentFile(file, Path.Combine(_outputFolder, relativeFilePath)))
				{
					return;
				}

				generatedPaths.Add(new FilenameInfo(relativeFilePath, file.Name, null, FilenameInfo.FilenameTypes.StaticFile));

				return;
			}
			ProviderInfo provider;
			Type iteratorType = _Project.GetIteratorTypeFromProviders(file.IteratorName, out provider);

			IScriptBaseObject[] iteratorObjects = helper.GetIteratorObjects(iteratorType, provider);

			if (iteratorObjects != null)
			{
				if (iteratorType.IsArray)
				{
					throw new NotImplementedException("Array iterator types not handled for static files yet. Please inform support@slyce.com about this error.");
				}

				foreach (IScriptBaseObject iteratorObject in iteratorObjects)
				{
					string fileName = helper.UpdateScriptName(iteratorObject, file);
					string relativeFilePath = Path.Combine(folderName, fileName);

					log.InfoFormat("<Static> {0}, {1}", iteratorObject);

					if (helper.GetSkipCurrentFile(file, Path.Combine(_outputFolder, relativeFilePath)))
					{
						return;
					}

					generatedPaths.Add(new FilenameInfo(relativeFilePath, file.Name, iteratorObject, FilenameInfo.FilenameTypes.StaticFile));
				}
			}
		}


		private void ValidateScriptName(List<FilenameInfo> generatedPaths, IScript script, string folderName)
		{
			if (string.IsNullOrEmpty(script.IteratorName))
			{
				string scriptName = helper.UpdateScriptName(null, script);
				string fileName = Path.Combine(folderName, scriptName);

				log.InfoFormat("<Script> {0}, no iterator", fileName);

				bool skipCurrentFile = helper.GetSkipCurrentFileOrIsCodeInsertedFile(script, null, Path.Combine(_outputFolder, fileName));
				if (skipCurrentFile)
				{
					log.Info("Skipped");
					return;
				}

				generatedPaths.Add(new FilenameInfo(fileName, script.FileName, null, FilenameInfo.FilenameTypes.GeneratedFile));
				return;
			}

			ProviderInfo provider;
			Type iteratorType = _Project.GetIteratorTypeFromProviders(script.IteratorName, out provider);

			IScriptBaseObject[] iteratorObjects = helper.GetIteratorObjects(iteratorType, provider);
			if (iteratorObjects != null)
			{
				if (iteratorType.IsArray)
				{
					string scriptName = helper.UpdateScriptName(iteratorObjects, script);
					string fileName = Path.Combine(folderName, scriptName);

					log.InfoFormat("<Script> {0}, {1}", fileName, iteratorObjects);

					bool skipCurrentFile = helper.GetSkipCurrentFileOrIsCodeInsertedFile(script, iteratorObjects, Path.Combine(_outputFolder, fileName));
					if (skipCurrentFile)
					{
						log.Info("Skipped");
						return;
					}

					generatedPaths.Add(new FilenameInfo(fileName, script.FileName, iteratorObjects, FilenameInfo.FilenameTypes.GeneratedFile));
				}
				else
				{
					foreach (IScriptBaseObject iteratorObject in iteratorObjects)
					{
						string scriptName = helper.UpdateScriptName(iteratorObject, script);
						string fileName = Path.Combine(folderName, scriptName);

						log.InfoFormat("<Script> {0}, {1}", fileName, iteratorObject);

						bool skipCurrentFile = helper.GetSkipCurrentFileOrIsCodeInsertedFile(script, iteratorObject, Path.Combine(_outputFolder, fileName));
						if (skipCurrentFile)
						{
							log.Info("Skipped");
							continue;
						}

						generatedPaths.Add(new FilenameInfo(fileName, script.FileName, iteratorObject, FilenameInfo.FilenameTypes.GeneratedFile));
					}
				}
			}
		}

		private void ValidateFolderName(List<FilenameInfo> generatedPaths, IFolder subFolder, IScriptBaseObject thisLevelRootObject, string folderName)
		{
			if (!string.IsNullOrEmpty(subFolder.IteratorName))
			{
				// The folder has an iterator
				ProviderInfo provider;
				Type iteratorType = _Project.GetIteratorTypeFromProviders(subFolder.IteratorName, out provider);

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
						string subFolderName = helper.UpdateScriptName(iteratorObject, subFolder);

						subFolderName = Path.Combine(folderName, subFolderName);
						log.InfoFormat("<Folder> {0}, {1}", subFolderName, iteratorObject);
						generatedPaths.Add(new FilenameInfo(subFolderName, subFolder.Name, iteratorObject, FilenameInfo.FilenameTypes.Folder));
						// Check Subfolders
						ValidateFileNames(generatedPaths, subFolder, thisLevelRootObject, subFolderName);
						//fileCount += GenerateAllFiles(subFolderName, subFolder, folderNode, iteratorObjects, basePath, false);
					}
				}
			}
			else
			{
				string subFolderName = helper.UpdateScriptName(null, subFolder);
				subFolderName = Path.Combine(folderName, subFolderName);
				log.InfoFormat("<Folder> {0}, no iterator", subFolderName);

				generatedPaths.Add(new FilenameInfo(subFolderName, subFolder.Name, null, FilenameInfo.FilenameTypes.Folder));
				// check sub folders
				ValidateFileNames(generatedPaths, subFolder, thisLevelRootObject, subFolderName);
			}
		}
	}
}
