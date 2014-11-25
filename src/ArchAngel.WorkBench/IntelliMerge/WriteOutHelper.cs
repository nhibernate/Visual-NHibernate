using System.Collections.Generic;
using System.IO;
using System.Xml;
using ArchAngel.Common;
using Slyce.Common;
using Slyce.IntelliMerge;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge.Controller.ManifestWorkers;

namespace ArchAngel.Workbench.IntelliMerge
{
	public class WriteOutHelper
	{
		private IController controller;

		/// <summary>
		/// The write out process involves the following steps:
		/// 1. Back up the files currently in the user's project directory.
		/// 2. Write the template files that are being used to the temporary prevgen directory.
		/// 3. Write the merged files to the user's project directory.
		/// 4. Copy those temporary prevgen files over to the user's prevgen directory.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="tempController"></param>
		/// <param name="files"></param>
		/// <param name="projectTree"></param>
		public void WriteAllFiles(TaskProgressHelper helper, IController tempController, List<IFileInformation> files, ProjectFileTree projectTree, bool overwriteExistingFiles)
		{
			controller = tempController;

			// Calculate which folders need backing up.
			List<string> directories = new List<string>();
			directories.Add(""); // The root folder.
			foreach (ProjectFileTreeNode node in projectTree.AllNodes)
			{
				if (node.IsFolder == false)
					continue;
				string path = node.Path;
				if (directories.Contains(path) == false)
					directories.Add(path);
			}

			//BackupUtility backupUtility = new BackupUtility();
			//string timeString = BackupUtility.GetTimeString();
			//foreach(string dir in directories)
			//{
			//    backupUtility.CreateBackup(
			//    Path.Combine(controller.CurrentProject.ProjectSettings.ProjectPath, dir),
			//    tempController.CurrentProject.ProjectSettings.ProjectGuid,
			//    Path.GetFileNameWithoutExtension(tempController.CurrentProject.ProjectSettings.TemplateFileName),
			//    Path.Combine(controller.CurrentProject.ProjectSettings.ProjectPath, dir),
			//    timeString);
			//}

			foreach (IFileInformation file in files)
			{
				if (file.CurrentDiffResult.DiffType == TypeOfDiff.Conflict)
				{
					throw new WriteOutException(string.Format("Cannot write out files where a conflict exists! The file {0} has a conflict.", file.RelativeFilePath));
				}
			}

			foreach (IFileInformation file in files)
			{
				WriteSingleFile(file, overwriteExistingFiles);
				// Write the newly generated file to the temporary directory so we can copy it to the prevgen directory in the
				// user's project directory. We do this instead of copying the whole newgen directory so we avoid changing the
				// timestamps on the prevgen files.
				string prevgenPath = controller.GetTempFilePathForComponent(ComponentKey.Workbench_FileGeneratorOutput);
				file.WriteNewGenFile(prevgenPath);

				// TODO: Need to copy the Manifest file over from the temporary prevgen folder to the new one.
				//CopyAllManifestFiles(controller.GetTempFilePathForComponent(ComponentKey.SlyceMerge_PrevGen),
				//                     controller.GetTempFilePathForComponent(ComponentKey.Workbench_FileGeneratorOutput));

				if (file.CodeRootMap != null)
				{
					CodeRootMapMatchProcessor processor = new CodeRootMapMatchProcessor();
					string manifestFilename = Path.Combine(prevgenPath, ManifestConstants.MANIFEST_FILENAME);
					XmlDocument doc = ManifestConstants.LoadManifestDocument(manifestFilename);
					processor.SaveCustomMappings(doc, file.CodeRootMap, Path.GetFileName(file.RelativeFilePath));
				}
			}
			// Copy the temporary files we just created to the user's prevgen directory.
			CreatePrevGenFiles();
		}

		private static void CopyAllManifestFiles(string fromDir, string toDir)
		{
			List<string> manifestFiles = Slyce.Common.Utility.GetFiles(fromDir, ManifestConstants.MANIFEST_FILENAME, SearchOption.AllDirectories);

			foreach (string manifestFile in manifestFiles)
			{
				string relativePath = Utility.RelativePathTo(fromDir, manifestFile);
				string toFileName = Path.Combine(toDir, relativePath);

				try
				{
					File.Copy(manifestFile, toFileName, true);
				}
				catch (IOException)
				{
					// don't bother if we couldn't copy it over.
				}
			}
		}

		private void CreatePrevGenFiles()
		{
			//PrevGenUtility utility = new PrevGenUtility();
			//utility.CreateManifestFiles(
			//   controller.GetTempFilePathForComponent(ComponentKey.Workbench_FileGeneratorOutput),
			//   controller.CurrentProject.ProjectSettings.ProjectPath,
			//   controller.GetTempFilePathForComponent(ComponentKey.Workbench_FileGenerator));

			//utility.CopyProgramPrevGenFiles(
			//    controller.GetTempFilePathForComponent(ComponentKey.Workbench_FileGeneratorOutput),
			//    controller.CurrentProject.ProjectSettings.ProjectPath,
			//    controller.CurrentProject.ProjectSettings.ProjectGuid,
			//    Path.GetFileNameWithoutExtension(controller.CurrentProject.ProjectSettings.TemplateFileName));
			//if(controller.AAZFound)
			//{
			//    string[] aazFiles =
			//        Directory.GetFiles(controller.CurrentProject.ProjectSettings.ProjectPath, "_ArchAngel.aaz",
			//                           SearchOption.AllDirectories);
			//    foreach(string file in aazFiles)
			//    {
			//        File.SetAttributes(file, FileAttributes.Normal);
			//        File.Delete(file);
			//    }
			//}
		}

		private void WriteSingleFile(IFileInformation file, bool overwriteExistingFile)
		{
			if (file.CurrentDiffResult.DiffType == TypeOfDiff.Conflict)
				throw new WriteOutException(string.Format("Cannot write out a file with a conflict. {0}", file.RelativeFilePath));

			if (file.CurrentDiffResult.DiffType == TypeOfDiff.Warning && file.MergedFileExists == false)
				throw new WriteOutException(string.Format("Cannot write out a file with a warning. {0}", file.RelativeFilePath));

			if (overwriteExistingFile)
				file.WriteNewGenFile(controller.CurrentProject.ProjectSettings.OutputPath);
			else
				file.WriteMergedFile(controller.CurrentProject.ProjectSettings.OutputPath);
		}
	}
}
