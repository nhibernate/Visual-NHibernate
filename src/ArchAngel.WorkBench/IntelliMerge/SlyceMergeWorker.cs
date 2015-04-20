using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Workbench;
using Slyce.IntelliMerge;
using Controller=ArchAngel.Workbench.Controller;
using DiffFile=Slyce.IntelliMerge.Controller.DiffItems.DiffFile;
using DiffFolder=Slyce.IntelliMerge.Controller.DiffItems.DiffFolder;

namespace ArchAngel.IntelliMerge
{
	#region Delegates
	public delegate void ProcessingFileDelegate(string filename);
	public delegate void DiffFinishedDelegate(string filename, TypeOfDiff diffType);
	public delegate void FinishedDelegate();
	#endregion

	public class SlyceMergeWorker
	{
		#region Events
		public event ProcessingFileDelegate FileBeingProcessed;
		public event DiffFinishedDelegate DiffFinished;
		public event FinishedDelegate Finished;
		public event Providers.ErrorEventHandler ParseError;
		#endregion

		#region Enums
		public enum WorkerStatusTypes
		{
			Idle,
			Busy
		}
		#endregion

		public static string PreviousGenerationFolder;
		public static string StagingFolder;
		private readonly BackgroundWorker backgroundWorker1 = new BackgroundWorker();
		private static bool FoldersHaveBeenCreated = false;
		private Hashtable UniqueFilesForCount = null;
		private int m_numExactCopy = 0;
		private int m_numResolvable = 0;
		private int m_numConflicts = 0;
		public DiffFolder RootFolder;
		private SlyceMerge slyceMerge = null;
		private WorkerStatusTypes m_workerStatus = WorkerStatusTypes.Idle;
		private List<GenerationError> GenerationErrors;

		public SlyceMergeWorker()
		{
			backgroundWorker1.DoWork += this.backgroundWorker1_DoWork;
			backgroundWorker1.RunWorkerCompleted += this.backgroundWorker1_RunWorkerCompleted;
			backgroundWorker1.ProgressChanged += this.backgroundWorker1_ProgressChanged;
		}

		public void Reset()
		{
			UniqueFilesForCount = new Hashtable();
			GenerationErrors = new List<GenerationError>();
		}

		private void RaiseFileBeingProcessedEvent(string filename)
		{
			if (FileBeingProcessed != null)
			{
				FileBeingProcessed(filename);
			}
		}

		private void RaiseFinishedEvent()
		{
			if (Finished != null)
			{
				Finished();
			}
		}

		private void RaiseDiffFinishedEvent(string filename, TypeOfDiff diffType)
		{
			if (DiffFinished != null)
			{
				DiffFinished(filename, diffType);
			}
		}

		public WorkerStatusTypes WorkerStatus
		{
			get { return m_workerStatus; }
		}

		public int NumExactCopy
		{
			get { return m_numExactCopy; }
		}

		public int NumResolvable
		{
			get { return m_numResolvable; }
		}

		public int NumConflicts
		{
			get { return m_numConflicts; }
		}

		public void UndoAllChanges()
		{
			if (Directory.Exists(StagingFolder))
			{
				Slyce.Common.Utility.DeleteDirectoryBrute(StagingFolder);
			}
		}

		/// <summary>
		/// Saves pending files to project folder.
		/// </summary>
		/// <returns>True if successful, false otherwise.</returns>
		public bool SaveChanges()
		{
			// TODO: Make sure no conflicts still exist

			// Save all changes in the user folders
			if (Directory.Exists(Controller.Instance.ProjectSettings.ProjectPath))
			{
				IOUtility.CopyMergedFolder(StagingFolder, Controller.Instance.ProjectSettings.ProjectPath);
                IOUtility.ArchiveFolder(Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator), Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator), Controller.Instance.ProjectSettings.ProjectPath);
				return true;
			}
			throw new Exception("Project folder is missing: " + Controller.Instance.ProjectSettings.ProjectPath);
		}

		public void StartAnalysis(List<GenerationError> generationErrors)
		{
			if (backgroundWorker1.IsBusy)
			{
				return;
			}
			GenerationErrors = generationErrors;
			SetupFolders();
			m_numResolvable = 0;
			m_numExactCopy = 0;
			m_numConflicts = 0;

			if (!Directory.Exists(Controller.Instance.ProjectSettings.ProjectPath))
			{
				throw new Exception("The project folder is missing. Please fix this on the 'Project Details' screen.");
			}
            if (!Directory.Exists(Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator)))
			{
                throw new FileNotFoundException("Invalid Generation folder: " + Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator));
			}
			m_workerStatus = WorkerStatusTypes.Busy;
			backgroundWorker1.WorkerReportsProgress = false;
			backgroundWorker1.WorkerSupportsCancellation = false;
			// Start the asynchronous analysis
			backgroundWorker1.RunWorkerAsync();
		}

		#region BackgroundWorker Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				RootFolder = null;
				SetupFolders();
				AnalyseFiles();
				return;
			}
			catch (Exception ex)
			{
				Controller.ReportError(ex);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				Controller.ReportError(e.Error);
			}
			// Reset FoldersHaveBeenCreated back to false, because the user might re-generate after this run is now finished.
			FoldersHaveBeenCreated = false;
			m_workerStatus = WorkerStatusTypes.Idle;
			RaiseFinishedEvent();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
		}
		#endregion

		/// <summary>
		/// Gets the total nummber of files that are to be analysed.
		/// </summary>
		/// <returns></returns>
		public int GetTotalFileCount()
		{
			SetupFolders();
			UniqueFilesForCount = new Hashtable();
			CountFiles(PreviousGenerationFolder);
			CountFiles(Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator));
			CountFiles(Controller.Instance.ProjectSettings.ProjectPath);
			return UniqueFilesForCount.Count;
		}

		/// <summary>
		/// Count the files in the folder. Stores result in UniqueFilesForCount.
		/// </summary>
		/// <param name="folder">Root Folder</param>
		private void CountFiles(string folder)
		{
			CountFiles(folder, folder);
		}

		/// <summary>
		/// Count the files in the folder. Stores result in UniqueFilesForCount.
		/// </summary>
		/// <param name="folder">Sub-folder to analyse.</param>
		/// <param name="rootPath">Root folder, so that we can determine the relative path - so 
		/// that Generated, PreviousGenerated and User files are rebased to the same root folder, 
		/// so that we can determine unique file count.</param>
		private void CountFiles(string folder, string rootPath)
		{
			string[] files = Directory.GetFiles(folder);
			string relativePath;

			foreach (string file in files)
			{
				relativePath = file.Replace(rootPath, "");

				if (!UniqueFilesForCount.ContainsKey(relativePath))
				{
					UniqueFilesForCount.Add(relativePath, false);
				}
			}
			string[] subDirs = Directory.GetDirectories(folder);

			foreach (string subDir in subDirs)
			{
				CountFiles(subDir, rootPath);
			}
		}

		/// <summary>
		/// Analyse the files for differences.
		/// </summary>
		private void AnalyseFiles()
		{
			UniqueFilesForCount = null;
			ProcessFolder(Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator), null, true, DiffFolder.TypeOfOwner.Generated);
		}

		/// <summary>
		/// Unzips the _AAZ file into a mirror folder structure so that diffing can be performed.
		/// </summary>
		/// <param name="fromFolder">Folder where the _AAZ resides.</param>
		/// <param name="toFolder">Temporary folder to mirror the extracted files.</param>
		public static void CopyPrevGenFiles(string fromFolder, string toFolder)
		{
		    if (Directory.Exists(toFolder))
			{
				Slyce.Common.Utility.DeleteDirectoryBrute(toFolder);
			}
			Directory.CreateDirectory(toFolder);

			if (Directory.Exists(fromFolder))
			{
				// Look for a zip file in the fromFolder
				string temp = Path.Combine(fromFolder, "_ArchAngel.aaz");

				if (File.Exists(temp))
				{
					// Copy all the files into the toFolder
					Slyce.Common.Utility.UnzipFile(temp, toFolder);
				}
				// Process sub-folders
				string[] subFolders = Directory.GetDirectories(fromFolder);

				foreach (string subFolder in subFolders)
				{
					string dirName = subFolder.Substring(subFolder.LastIndexOf(Path.DirectorySeparatorChar) + 1);
					CopyPrevGenFiles(subFolder, Path.Combine(toFolder, dirName));
				}
			}
		}

		/// <summary>
		/// Creates the folder structures required for Generation, PreviousGeneration and User files.
		/// </summary>
		public static void SetupFolders()
		{
			if (FoldersHaveBeenCreated)
			{
				return;
			}
			
            PreviousGenerationFolder = Controller.Instance.GetTempFilePathForComponent(ComponentKey.SlyceMergePrevGen);
			StagingFolder = Controller.Instance.GetTempFilePathForComponent(ComponentKey.ArchAngelStaging);

			if (Controller.Instance.ProjectSettings.ProjectPath.LastIndexOf(Path.DirectorySeparatorChar) != Controller.Instance.ProjectSettings.ProjectPath.Length - 1)
			{
				Controller.Instance.ProjectSettings.ProjectPath += Path.DirectorySeparatorChar;
			}
			if (Directory.Exists(PreviousGenerationFolder))
			{
				if (!Slyce.Common.Utility.DeleteDirectoryBrute(PreviousGenerationFolder))
				{
					// "Previous generation folder could not be deleted: " + 
					throw new Slyce.Common.Exceptions.FileLockedException("Previous generation folder could not be deleted", PreviousGenerationFolder);
				}
			}
			if (Directory.Exists(StagingFolder))
			{
				if (!Slyce.Common.Utility.DeleteDirectoryBrute(StagingFolder))
				{
					//throw new Exception("Staging folder could not be deleted: " + StagingFolder);
					throw new Slyce.Common.Exceptions.FileLockedException("Staging folder could not be deleted", StagingFolder);
				}
			}
			Directory.CreateDirectory(PreviousGenerationFolder);
			Directory.CreateDirectory(StagingFolder);

			// Copy previous generation files to the new folders
			CopyPrevGenFiles(Controller.Instance.ProjectSettings.ProjectPath, PreviousGenerationFolder);

			if (!Directory.Exists(Controller.Instance.ProjectSettings.ProjectPath) ||
                !Directory.Exists(Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator)) ||
				!Directory.Exists(PreviousGenerationFolder))
			{
                throw new Exception(string.Format("Invalid arguments have been passed to the application.\nExisting Project Folder:{0}\nGeneration Folder: {1}", Controller.Instance.ProjectSettings.ProjectPath, Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator)));
			}
			FoldersHaveBeenCreated = true;
		}

		private void ProcessFolder(string folder, DiffFolder parentFolder, DiffFolder.TypeOfOwner ownerType)
		{
			ProcessFolder(folder, parentFolder, false, ownerType);
		}

		private void ProcessFolder(string folder, DiffFolder parentFolder, bool topFolder, DiffFolder.TypeOfOwner ownerType)
		{
			string singleFolderName = Slyce.Common.Utility.GetSingleDirectoryName(folder).ToLower();
			// TODO: We need to add the ability for the user to specify folders that should be ignored
			if (singleFolderName == ".svn" ||
				singleFolderName == "_svn")
			{
				return;
			}
			if (!Directory.Exists(folder))
			{
				return;
			}
			// Make sure the folder exists in the user folder structure on disk
			string stagingFolder = "";

			switch (ownerType)
			{
				case DiffFolder.TypeOfOwner.Parent:
					stagingFolder = folder.Replace(PreviousGenerationFolder, StagingFolder);
					break;
				case DiffFolder.TypeOfOwner.Generated:
                    stagingFolder = folder.Replace(Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator), StagingFolder);
					break;
				case DiffFolder.TypeOfOwner.User:
					stagingFolder = folder.Replace(Controller.Instance.ProjectSettings.ProjectPath, StagingFolder);
					break;
			}
			if (!Directory.Exists(stagingFolder))
			{
				Directory.CreateDirectory(stagingFolder);
			}
			DiffFolder folderNode = null;

			if (parentFolder != null)
			{
				string folderName = folder.Substring(folder.LastIndexOf(Path.DirectorySeparatorChar) + 1);

				foreach (DiffFolder dFolder in parentFolder.SubFolders)
				{
					if (dFolder.Name == folderName)
					{
						folderNode = dFolder;
						break;
					}
				}
				if (folderNode == null)
				{
					folderNode = new DiffFolder(folderName);
					parentFolder.AddSubFolder(folderNode);
				}
			}
			else
			{
				if (RootFolder == null)
				{
					RootFolder = new DiffFolder("Root");
				}
				folderNode = RootFolder;
			}
			string[] folders = Directory.GetDirectories(folder);
			string[] fileNames = Directory.GetFiles(folder);

			foreach (string folderName in folders)
			{
				ProcessFolder(folderName, folderNode, ownerType);
			}
			AddFileNodesToFolderNode(ref folderNode, fileNames, ownerType);
			// Perform a diff on all files in the folder
			PerformDiffOfFolder(folderNode);
		}

		/// <summary>
		/// Creates inert DiffFiles and adds them to the folderNode, so that further operations
		/// can be performed on them later.
		/// </summary>
		/// <param name="folderNode"></param>
		/// <param name="fileNames"></param>
		/// <param name="ownerType"></param>
		private void AddFileNodesToFolderNode(ref DiffFolder folderNode, string[] fileNames, DiffFolder.TypeOfOwner ownerType)
		{
			foreach (string fileName in fileNames)
			{
				if (Path.GetExtension(fileName).ToLower() != ".aaz")
				{
					DiffFile fileNode = new DiffFile(Path.GetFileName(fileName));

					switch (ownerType)
					{
						case DiffFolder.TypeOfOwner.Parent:
							fileNode.FilePathPrevGen = fileName;
							break;
						case DiffFolder.TypeOfOwner.Generated:
							fileNode.FilePathTemplate = fileName;
							break;
						case DiffFolder.TypeOfOwner.User:
							fileNode.FilePathUser = fileName;
							break;
					}
					folderNode.AddFile(fileNode, ownerType);
				}
			}
		}

		private void PerformDiffOfFolder(DiffFolder diffFolder)
		{
			bool fileNeedsToBeCounted = false;

			if (UniqueFilesForCount == null)
			{
				GetTotalFileCount();
			}

			for (int i = 0; i < diffFolder.Files.Length; i++)
			{
				PerformDiffOfFile(diffFolder.Files[i], ref fileNeedsToBeCounted);
			}
		}

		internal void PerformDiffOfFile(DiffFile diffFile, ref bool fileNeedsToBeCounted)
		{
			// Don't process files that had errors during generation
			for (int errFileCounter = 0; errFileCounter < GenerationErrors.Count; errFileCounter++)
			{
				if (Slyce.Common.Utility.StringsAreEqual(diffFile.RelativePath, GenerationErrors[errFileCounter].FileName, false))
				{
					//continue;
					return;
				}
			}
			if (UniqueFilesForCount[diffFile.RelativePath] != null &&
				(bool)UniqueFilesForCount[diffFile.RelativePath] == false)
			{
				fileNeedsToBeCounted = true;
				UniqueFilesForCount[diffFile.RelativePath] = true;
			}
			else
			{
				fileNeedsToBeCounted = false;
			}
			if (diffFile.Path.IndexOf(".aaz") > 0 ||
				diffFile.HasParseError)
			{
				//continue;
				return;
			}
			if (FileBeingProcessed != null)
			{
				FileBeingProcessed(diffFile.Path);
			}
			// Perform a 3-way diff on the file
			string parentFile = Path.Combine(PreviousGenerationFolder, diffFile.RelativePath);
			string userFile = Path.Combine(Controller.Instance.ProjectSettings.ProjectPath, diffFile.RelativePath);
			string templateFile = Path.Combine(Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator), diffFile.RelativePath);
			string mergedFile = Path.Combine(StagingFolder, diffFile.RelativePath + ".merged");

		    if(CheckFilesForModifications(diffFile.RelativePath) == false)
		    {
                RaiseDiffFinishedEvent(diffFile.Name, TypeOfDiff.ExactCopy);
		        return;
		    }

			diffFile.FilePathTemplate = templateFile;
			diffFile.FilePathUser = userFile;
			diffFile.FilePathPrevGen = parentFile;

			if (File.Exists(mergedFile))
			{
				bool mergeComplete = true;

				// Binary files are never in an unknown state, they are all or nothing.
				// Text files need to be checked.
				if (diffFile.IsText)
				{
					using (TextReader tr = new StreamReader(mergedFile))
					{
						string line;
						//int lineCounter = 0;

					    while ((line = tr.ReadLine()) != null &&
							mergeComplete)
						{
							int pipeIndex = line.IndexOf("|");
							int backColor = int.Parse(line.Substring(0, pipeIndex));

							if (backColor != -1 &&
								backColor != 0)
							{
								mergeComplete = false;
							}
							//lineCounter++;
						}
					}
				}
				if (mergeComplete)
				{
					diffFile.DiffType = TypeOfDiff.Warning;
				}
				else
				{
					diffFile.DiffType = TypeOfDiff.Conflict;
				}
			}
			else
			{
				if (diffFile.IsText)
				{
					if (File.Exists(parentFile) &&
							  File.Exists(userFile) &&
							  File.Exists(templateFile))
					{
						// Perform 3-way diff
						string fileBodyParent = IOUtility.GetTextFileBody(parentFile);
						string fileBodyUser = IOUtility.GetTextFileBody(userFile);
						string fileBodyGenerated = IOUtility.GetTextFileBody(templateFile);

						string mergedText;
						slyceMerge = SlyceMerge.Perform3wayDiff(fileBodyUser, fileBodyParent, fileBodyGenerated, out mergedText);
						diffFile.DiffType = slyceMerge.DiffType;
						string newFile = "";

						if (slyceMerge.DiffType != TypeOfDiff.Conflict)
						{
							switch (slyceMerge.DiffType)
							{
								case TypeOfDiff.ExactCopy:
									newFile = Path.Combine(StagingFolder, diffFile.RelativePath) + ".copy";
									Slyce.Common.Utility.FileCopy(userFile, newFile);
									break;
								case TypeOfDiff.TemplateChangeOnly:
                                    newFile = Path.Combine(StagingFolder, diffFile.RelativePath) + ".copy";
									Slyce.Common.Utility.FileCopy(templateFile, newFile);
									break;
								case TypeOfDiff.UserAndTemplateChange:
                                    newFile = Path.Combine(StagingFolder, diffFile.RelativePath) + ".copy";

									if (!Directory.Exists(Path.GetDirectoryName(newFile)))
									{
										Directory.CreateDirectory(Path.GetDirectoryName(newFile));
									}
									Slyce.Common.Utility.WriteToFile(newFile, mergedText);
									break;
								case TypeOfDiff.UserChangeOnly:
                                    newFile = Path.Combine(StagingFolder, diffFile.RelativePath) + ".copy";
									Slyce.Common.Utility.FileCopy(userFile, newFile);
									break;
								case TypeOfDiff.Warning:
									throw new Exception("Oops");
							}
						}
					}
					else if (File.Exists(parentFile) &&
							  File.Exists(userFile) &&
							  !File.Exists(templateFile))
					{
						// No template file, just use the user file
						diffFile.DiffType = TypeOfDiff.UserChangeOnly;
                        string newPath = Path.Combine(StagingFolder, diffFile.RelativePath) + ".copy";
						Slyce.Common.Utility.FileCopy(userFile, newPath);
					}
					else if (File.Exists(parentFile) &&
								  !File.Exists(userFile) &&
								  File.Exists(templateFile))
					{
						// No user file, just use the template file
						diffFile.DiffType = TypeOfDiff.TemplateChangeOnly;
                        string newPath = Path.Combine(StagingFolder, diffFile.RelativePath) + ".copy";
						Slyce.Common.Utility.FileCopy(templateFile, newPath);
					}
					else if (!File.Exists(parentFile) &&
								  File.Exists(userFile) &&
								  File.Exists(templateFile))
					{
						// No parent file, make sure the user merges the template and user files
						SlyceMerge.LineSpan[] userLines;
						SlyceMerge.LineSpan[] templateLines;
						string combinedText;
						diffFile.DiffType = SlyceMerge.PerformTwoWayDiff(false, IOUtility.GetTextFileBody(userFile), IOUtility.GetTextFileBody(templateFile), out userLines, out templateLines, out combinedText);

						if (diffFile.DiffType == TypeOfDiff.ExactCopy)
						{
                            string newPath = Path.Combine(StagingFolder, diffFile.RelativePath) + ".copy";
							Slyce.Common.Utility.FileCopy(templateFile, newPath);
						}
						else
						{
							throw new NotImplementedException("This scenario has not been handled yet. Please contact support@slyce.com with: PerformDiffOfFolder(PrevGen file doesn't exist, and TypeOfDiff = " + diffFile.DiffType + ")");
						}
						//diffFile.DiffType = SlyceMerge.TypeOfDiff.Conflict; // TODO: Work out what should be done in this instance
						//throw new Exception("TODO: determine course of action, what should be copied to staging folder.");
						//string newPath = templateFile.Replace(ProjectFolder, StagingFolder) + ".copy";
						//Utility.FileCopy(templateFile, newPath);
					}
					else if (File.Exists(parentFile) &&
						!File.Exists(userFile) &&
						!File.Exists(templateFile))
					{
						// TODO: It looks like the file was deleted by the user, AND it was removed from the template, so therefore the 
						// user doesn't want this file anymore. Get user to confirm?
                        string newPath = Path.Combine(StagingFolder, diffFile.RelativePath) + ".copy";
						Slyce.Common.Utility.FileCopy(parentFile, newPath);
					}
					else if (!File.Exists(parentFile) &&
								 !File.Exists(userFile) &&
								 File.Exists(templateFile))
					{
                        string newPath = templateFile.Replace(Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator), StagingFolder) + ".copy";
						Slyce.Common.Utility.FileCopy(templateFile, newPath);
					}
					else if (!File.Exists(parentFile) &&
								  File.Exists(userFile) &&
								  !File.Exists(templateFile))
					{
						// TODO: Do we really need to go to the effort of copying etc, because it only exists in the user folder,
						// so if we take no action it will still exist there - that should be fine ;-)
                        string newPath = Path.Combine(StagingFolder, diffFile.RelativePath) + ".copy";
						Slyce.Common.Utility.FileCopy(userFile, newPath);
					}
					else
					{
						// TODO: Shouldn't really be a warning...
						diffFile.DiffType = TypeOfDiff.Warning;
						throw new Exception(string.Format("TODO: determine course of action, what should be copied to staging folder, because no file exists: \n{0}\n{1}\n{2}", parentFile, userFile, templateFile));
					}
				}
				else // Binary file
				{
					string crcParent = File.Exists(parentFile) ? Slyce.Common.Utility.GetCheckSumOfFile(parentFile) : "";
					string crcTemplate = File.Exists(templateFile) ? Slyce.Common.Utility.GetCheckSumOfFile(templateFile) : "";
					string crcUser = File.Exists(userFile) ? Slyce.Common.Utility.GetCheckSumOfFile(userFile) : "";

					diffFile.MD5Parent = crcParent;
					diffFile.MD5Template = crcTemplate;
					diffFile.MD5User = crcUser;

					// TODO: perform CheckSum of binary file
					if (!string.IsNullOrEmpty(crcParent) &&
						!string.IsNullOrEmpty(crcTemplate) &&
						!string.IsNullOrEmpty(crcUser))
					{
						if (crcParent == crcUser &&
								crcUser == crcTemplate)
						{
							diffFile.DiffType = TypeOfDiff.ExactCopy;
                            string newPath = Path.Combine(StagingFolder, diffFile.RelativePath) + ".copy";
							Slyce.Common.Utility.FileCopy(userFile, newPath);
						}
						else if (crcParent == crcUser &&
									crcParent != crcTemplate)
						{
							diffFile.DiffType = TypeOfDiff.TemplateChangeOnly;
                            string newPath = Path.Combine(StagingFolder, diffFile.RelativePath) + ".copy";
							Slyce.Common.Utility.FileCopy(userFile, newPath);
						}
						else if (crcParent != crcUser &&
									crcParent == crcTemplate)
						{
							diffFile.DiffType = TypeOfDiff.UserChangeOnly;
                            string newPath = Path.Combine(StagingFolder, diffFile.RelativePath) + ".copy";
							Slyce.Common.Utility.FileCopy(userFile, newPath);
						}
						else if (crcParent != crcUser &&
							 crcUser == crcTemplate)
						{
							diffFile.DiffType = TypeOfDiff.UserAndTemplateChange;
						}
					}
					else if (string.IsNullOrEmpty(crcParent) &&
						!string.IsNullOrEmpty(crcTemplate) &&
						!string.IsNullOrEmpty(crcUser))
					{
						diffFile.DiffType = TypeOfDiff.Conflict;
					}
					else if (!string.IsNullOrEmpty(crcParent) &&
								!string.IsNullOrEmpty(crcTemplate) &&
								string.IsNullOrEmpty(crcUser))
					{
						diffFile.DiffType = TypeOfDiff.TemplateChangeOnly;
                        string newPath = Path.Combine(StagingFolder, diffFile.RelativePath) + ".copy";
						Slyce.Common.Utility.FileCopy(templateFile, newPath);
					}
					else if (!string.IsNullOrEmpty(crcParent) &&
					        string.IsNullOrEmpty(crcTemplate) &&
					        !string.IsNullOrEmpty(crcUser))
					{
						diffFile.DiffType = TypeOfDiff.UserChangeOnly;
                        string newPath = Path.Combine(StagingFolder, diffFile.RelativePath) + ".copy";
						Slyce.Common.Utility.FileCopy(userFile, newPath);
					}
					else if (string.IsNullOrEmpty(crcParent) &&
			                !string.IsNullOrEmpty(crcTemplate) &&
			                string.IsNullOrEmpty(crcUser))
					{
						// This is a new file that has been generated
						diffFile.DiffType = TypeOfDiff.ExactCopy;
                        string newPath = Path.Combine(StagingFolder, diffFile.RelativePath) + ".copy";
						Slyce.Common.Utility.FileCopy(templateFile, newPath);
					}
					else
					{
						throw new NotImplementedException("Not coded yet. Seems like only one version of binary file exists. Probably just need to copy as-is.");
					}
				}
			}
			if (diffFile.DiffType != TypeOfDiff.ExactCopy)
			{
				// Only perform the costly SuperDiff if it is not an exact copy
				//ArchAngel.IntelliMerge.DiffItems.DiffFile diffFile = diffFile;
				PerformSuperDiff(ref diffFile);
			}
			if (fileNeedsToBeCounted)
			{
				switch (diffFile.DiffType)
				{
					case TypeOfDiff.Conflict:
						m_numConflicts++;
						break;
					case TypeOfDiff.ExactCopy:
						m_numExactCopy++;
						break;
					case TypeOfDiff.TemplateChangeOnly:
					case TypeOfDiff.UserAndTemplateChange:
					case TypeOfDiff.UserChangeOnly:
					case TypeOfDiff.Warning:
						m_numResolvable++;
						break;
					default:
						throw new NotImplementedException("Not coded yet");
				}
				RaiseDiffFinishedEvent(diffFile.Name, diffFile.DiffType);
			}

            Slyce.Common.Utility.DeleteFileBrute(templateFile + ".prevgen.md5");
            Slyce.Common.Utility.DeleteFileBrute(templateFile + ".user.md5");

            if (diffFile.DiffType == TypeOfDiff.ExactCopy)
            {
                // Store the MD5 hashes of the files we just diff'd, so we don't do them again.
                if(File.Exists(parentFile))
                    Slyce.Common.Utility.CreateMD5HashFileForTextFile(parentFile, templateFile + ".prevgen.md5");
                if(File.Exists(userFile))
                    Slyce.Common.Utility.CreateMD5HashFileForTextFile(userFile, templateFile + ".user.md5");
            }
		}

        /// <summary>
        /// Checks the templace, prevgen and user files for modifications since the last time they were analysed.
        /// </summary>
        /// <param name="relativeFilename"></param>
        /// <returns></returns>
        private static bool CheckFilesForModifications(string relativeFilename)
        {
            string templateFilename = Path.Combine(Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator), relativeFilename);
            string prevgenFile = Path.Combine(PreviousGenerationFolder, relativeFilename);
            string userFile = Path.Combine(Controller.Instance.ProjectSettings.ProjectPath, relativeFilename);

            string templateFileMD5 = templateFilename + ".md5";
            string prevgenFileMD5 = templateFilename + ".prevgen.md5";
            string userFileMD5 = templateFilename + ".user.md5";

            if(Slyce.Common.Utility.HasFileChangedMD5(templateFilename, templateFileMD5) == false
                && Slyce.Common.Utility.HasFileChangedMD5(prevgenFile, prevgenFileMD5) == false
                && Slyce.Common.Utility.HasFileChangedMD5(userFile, userFileMD5) == false)
            {
                return false;
            }

            return true;
        }

		/// <summary>
		/// Performs an in depth diff by breaking code files into their constituent parts (functions, properties
		/// etc, so that these elements can be diffed without regard to their ordering.
		/// </summary>
		/// <param name="diffFile"></param>
		private void PerformSuperDiff(ref DiffFile diffFile)
		{
			try
			{
				switch (Path.GetExtension(diffFile.Path).ToLower())
				{
					case ".cs":
						Providers.CSharpFormatter formatter = new Providers.CSharpFormatter(diffFile.Path);
						formatter.CreateObjectModel = true;
						formatter.RaiseError += formatter_RaiseError;

						try
						{
							// TODO: get the code for each file type (user, generated, prevGen), then
							// break into classes, functions etc, and  perform a diff3 on each entity.
							// We REALLY need to store these results in the objects, so that this process 
							// happens once, when the initial population occurs, and doesn't need to happen 
							// again when the user clicks a childTreeListNode to display the text and conflicts.

							// Reset the DiffType
							diffFile.DiffType = TypeOfDiff.ExactCopy;

							if (File.Exists(diffFile.FilePathPrevGen))
							{
								formatter.Reset();
								formatter.CodeFilePath = diffFile.FilePathPrevGen;
								formatter.GetFormattedCode(Slyce.Common.Utility.ReadTextFile(diffFile.FilePathPrevGen));
                                diffFile.CodeRootParent = formatter.Controller.Root;
								diffFile.CodeRootParent.SortAllMembers();
							}
							if (File.Exists(diffFile.FilePathTemplate))
							{
								formatter.Reset();
								formatter.CodeFilePath = diffFile.FilePathTemplate;
								formatter.GetFormattedCode(Slyce.Common.Utility.ReadTextFile(diffFile.FilePathTemplate));
                                diffFile.CodeRootTemplate = formatter.Controller.Root;
								diffFile.CodeRootTemplate.SortAllMembers();
							}
							if (File.Exists(diffFile.FilePathUser))
							{
								formatter.Reset();
								formatter.CodeFilePath = diffFile.FilePathUser;
								formatter.GetFormattedCode(Slyce.Common.Utility.ReadTextFile(diffFile.FilePathUser));
                                diffFile.CodeRootUser = formatter.Controller.Root;
								diffFile.CodeRootUser.SortAllMembers();

								// TODO: this check of whether the code object model has been correctly created must get removed from the final build
								//string code = diffFile.CodeRootUser.ToString();
								//formatter.Reset();
								//formatter.ParseCode(code);

								//if (!diffFile.CodeRootUser.IsTheSame(ArchAngel.Providers.CodeProvider.Controller.Root))
								//{
								//    throw new InvalidProgramException("The code object model has not been written correctly: " + ArchAngel.Providers.CodeProvider.CSharp.BaseConstruct.ComparisonDifference);
								//}
							}
						}
						catch (Exception ex)
						{
							diffFile.ParseErrorDescription = ex.Message;
						}
						bool ignoreAllOmits = false;

						if (File.Exists(diffFile.FilePathTemplate) &&
							!File.Exists(diffFile.FilePathUser))
						{
							// If the user file is missing, then all code objects are also going to be missing and 
							// will therefore get marked at 'Omit = true', but infact we need to generate them.
							ignoreAllOmits = true;
						}
						TypeOfDiff resultingDiffType;
                        Providers.CodeProvider.CSharp.Utility utility = new Providers.CodeProvider.CSharp.Utility();
				        diffFile.CodeRootAll = utility.CreateCombinedCodeRoot(diffFile.CodeRootTemplate, diffFile.CodeRootUser, diffFile.CodeRootParent, out resultingDiffType, ignoreAllOmits);
						diffFile.CodeRootAll.SortAllMembers();
						diffFile.DiffType = resultingDiffType;

						break;
					default:
						// No SuperDiff available for this type of file (no parser created yet).
						break;
				}
			}
			catch (Exception ex)
			{
				Controller.ReportError(ex);
			}
		}


		/// <summary>
		/// Gets the type of diff for the current object. Also sets the lowest common denominator diff type for the diffFile.
		/// </summary>
		/// <param name="diffFile"></param>
		/// <param name="userFileExists"></param>
		/// <param name="parentFileExists"></param>
		/// <param name="userEntityFound"></param>
		/// <param name="parentEntityFound"></param>
		/// <returns>The type of diff of the current object.</returns>
		private TypeOfDiff SetDiffType(DiffFile diffFile, bool userFileExists, bool parentFileExists, bool userEntityFound, bool parentEntityFound)
		{
			TypeOfDiff newDiffType = TypeOfDiff.ExactCopy;

			if (userFileExists && parentFileExists)
			{
				if (userEntityFound && parentEntityFound)
				{
					newDiffType = TypeOfDiff.ExactCopy;
				}
				else if (userEntityFound && !parentEntityFound)
				{
					newDiffType = TypeOfDiff.TemplateChangeOnly;
				}
				else if (!userEntityFound && parentEntityFound)
				{
					newDiffType = TypeOfDiff.UserChangeOnly;
				}
				else if (!userEntityFound && !parentEntityFound)
				{
					newDiffType = TypeOfDiff.UserAndTemplateChange;
				}
			}
			else if (userFileExists && !parentFileExists)
			{
				if (userEntityFound)
				{
					newDiffType = TypeOfDiff.ExactCopy;
				}
				else
				{
					newDiffType = TypeOfDiff.UserChangeOnly;
				}
			}
			else if (!userFileExists && parentFileExists)
			{
				if (parentEntityFound)
				{
					newDiffType = TypeOfDiff.ExactCopy;
				}
				else
				{
					newDiffType = TypeOfDiff.UserChangeOnly;
				}
			}
			TypeOfDiff currentDiffType = diffFile.DiffType;
			diffFile.DiffType = ArchAngel.Providers.CodeProvider.CSharp.Utility.ModifyTypeOfDiff(currentDiffType, newDiffType);
			return newDiffType;
		}


		void formatter_RaiseError(string fileName, string procedureName, string description, string originalText, int lineNumber, int startPos, int length)
		{
			if (ParseError != null)
			{
				ParseError(fileName, procedureName, description, originalText, lineNumber, startPos, length);
			}
		}

	}
}
