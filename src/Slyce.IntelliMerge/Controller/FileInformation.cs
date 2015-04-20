using System;
using System.Diagnostics;
using System.IO;
using Slyce.Common;
using Slyce.IntelliMerge.Controller.ManifestWorkers;

namespace Slyce.IntelliMerge.Controller
{
	/// <summary>
	/// Abstract class that implements much of the functionality of a FileInformation in order to make it easier to implement.
	/// The PerformDiff method is used to do the Diff on the 3 files. This creates a DiffFile, which contains information
	/// about the outcome of the diff.
	/// </summary>
	/// <typeparam name="T">The type of data contained in the the files that this class represents. Should be string for text files and byte[] for binary files.</typeparam>
	public abstract class FileInformation<T> : IFileInformation where T : class
	{
		/// <summary>
		/// The User's version of the file. Returns a blank, uneditable file if one has not been created yet.
		/// </summary>
		private IProjectFile<T> userFile;
		/// <summary>
		/// The result of the previous generation. Returns a blank, uneditable file if one has not been created yet.
		/// </summary>
		private IProjectFile<T> prevgenFile;
		/// <summary>
		/// The result of the current generation. Returns a blank, uneditable file if one has not been created yet.
		/// </summary>
		private IProjectFile<T> newgenFile;
		/// <summary>
		/// The result of the merge. Returns a blank, uneditable file if one has not been created yet.
		/// </summary>
		private IProjectFile<T> mergedFile;
		/// <summary>
		/// The relative path of the file within the project.
		/// </summary>
		private string _relativePath;
		/// <summary>
		/// The type of diff and merge that will be applied to the file.
		/// </summary>
		protected IntelliMergeType intelliMergeType = IntelliMergeType.AutoDetect;
		/// <summary>
		/// The name of the programming language text in this file. Could be C#, Plain Text, etc.
		/// </summary>
		private TemplateContentLanguage? templateLanguage = null;
		/// <summary>
		/// The MD5 of the prevgen file from the last time the files were written out.
		/// </summary>
		private string prevgenPreviousMD5;
		/// <summary>
		/// The MD5 of the newgen file from the last time the files were written out.
		/// </summary>
		private string newgenPreviousMD5;
		/// <summary>
		/// The MD5 of the user file from the last time the files were written out.
		/// </summary>
		private string userPreviousMD5;
		/// <summary>
		/// The output of the diff process.
		/// </summary>
		protected CodeRootMap diffCodeRootMap = new CodeRootMap();
		/// <summary>
		/// Current results of the diff. If one has not been performed, then CurrentDiffResult.DiffPerformedSuccessfully will be false.
		/// </summary>
		private readonly DiffResult currentDiffResult = new DiffResult();
		/// <summary>
		/// Set to true if the next diff should clear their previous state and reload the files from disk.
		/// </summary>
		private bool reloadFiles;
		/// <summary>
		/// Holds the Manifest file filename for the next diff, so that any custom matches can be applied if there are any.
		/// </summary>
		protected string temporaryManifestFile;

		protected bool manifestFileApplied = false;

		/// <summary>
		/// This event is raised when an error occurs during the processing of code files.
		/// </summary>
		public event EventHandler<FileParseErrorArgs> ParseError;

		/// <summary>
		/// The output of the diff process. If the diff has not been performed yet,
		/// this will return an empty CodeRootMap, which will throw exceptions if you
		/// try and get its diff type or merged code root.
		/// </summary>
		public CodeRootMap CodeRootMap
		{
			get
			{
				return diffCodeRootMap;
			}
		}

		/// <summary>
		/// The User's version of the file. Returns a blank, uneditable file if one has not been created yet.
		/// </summary>
		public IProjectFile<T> UserFile
		{
			get { return userFile; }
			set { userFile = value; }
		}

		/// <summary>
		/// The result of the previous generation. Returns a blank, uneditable file if one has not been created yet.
		/// </summary>
		public IProjectFile<T> PrevGenFile
		{
			get { return prevgenFile; }
			set { prevgenFile = value; }
		}

		/// <summary>
		/// The result of the current generation. Returns a blank, uneditable file if one has not been created yet.
		/// </summary>
		public IProjectFile<T> NewGenFile
		{
			get { return newgenFile; }
			set { newgenFile = value; }
		}

		/// <summary>
		/// The result of the merge. Returns a blank, uneditable file if one has not been created yet.
		/// </summary>
		public IProjectFile<T> MergedFile
		{
			get { return mergedFile; }
			set { mergedFile = value; }
		}

		/// <summary>
		/// Language retrieved from the function that generated the NewGenFile. This should be "C#" for C# files, "Plain Text" for plain text, ect.
		/// </summary>
		public TemplateContentLanguage? TemplateLanguage
		{
			get { return templateLanguage; }
			set { templateLanguage = value; }
		}

		/// <summary>
		/// The relative path of this file within the project.
		/// </summary>
		public string RelativeFilePath
		{
			get { return _relativePath; }
			set { _relativePath = value; }
		}

		/// <summary>
		/// Set to true if the next diff should clear their previous state and reload the files from disk.
		/// </summary>
		public bool ReloadFiles
		{
			get { return reloadFiles; }
			set { reloadFiles = value; }
		}

		/// <summary>
		/// Returns the IntelliMergeType that should be applied to this file.
		/// </summary>
		public IntelliMergeType IntelliMerge
		{
			get
			{
				if (intelliMergeType == IntelliMergeType.AutoDetect)
				{
					intelliMergeType = FileInformationUtility.GetDefaultIntelliMergeType(this);
				}
				return intelliMergeType;
			}
			set
			{
				IntelliMergeType oldType = intelliMergeType;
				intelliMergeType = value;
				if (value != oldType)
				{
					// Remove the MergedFile.
					MergedFile = null;
					// Force the manifest file mappings to reapply at next diff.
					manifestFileApplied = false;
				}
			}
		}

		/// <summary>
		/// Sets the MD5 hash strings (represented in as hexadecimal numbers) for the these files when they were last written out.
		/// These are used to see if they have changed since the last time the generation, analysis and writing to the user's directory
		/// happened.
		/// </summary>
		/// <param name="prevgen">The MD5 of the prevgen file.</param>
		/// <param name="newgen">The MD5 of the newgen file.</param>
		/// <param name="user">The MD5 of the user file.</param>
		public void SetPreviousVersionMD5s(string prevgen, string newgen, string user)
		{
			prevgenPreviousMD5 = prevgen;
			newgenPreviousMD5 = newgen;
			userPreviousMD5 = user;
		}

		/// <summary>
		/// True if a Merged file has been created. 
		/// </summary>
		public bool MergedFileExists
		{
			get { return MergedFile != null && MergedFile.HasContents; }
		}

		private string GetManifestFilename(string prevgenFolder)
		{
			string dir = Path.GetDirectoryName(RelativeFilePath);
			dir = Path.Combine(prevgenFolder, dir);

			return Path.Combine(dir, ManifestConstants.MANIFEST_FILENAME);
		}

		/// <summary>
		/// Loads the Manifest file so that custom matches can be applied before the next diff.
		/// Doesn't clear the MergedFile, so the next diff will apply these matches on top of any existing ones.
		/// </summary>
		/// <param name="prevgenPath">The path to the temporary prevgen directory. This assumes a flat 
		/// directory structure, so the manifest file for BLL\Test.cs would be in prevgenPath\BLL\{Manifest filename}.</param>
		public void LoadCustomMatches(string prevgenPath)
		{
			temporaryManifestFile = GetManifestFilename(prevgenPath);
			manifestFileApplied = false;
		}

		public abstract bool CalculateMergedFile();

		/// <summary>
		/// Checks if the MD5 of the current PrevGen file matches the MD5 of the previous PrevGen file.
		/// </summary>
		/// <returns>True if the file has changed. False if it has not.</returns>
		public bool HasPrevGenFileChanged()
		{
			string currentMD5 = prevgenFile.HexStringMD5();

			return !currentMD5.Equals(prevgenPreviousMD5);
		}
		/// <summary>
		/// Checks if the MD5 of the current NewGen file matches the MD5 of the previous PrevGen file.
		/// </summary>
		/// <returns>True if the file has changed. False if it has not.</returns>
		public bool HasNewGenFileChanged()
		{
			string currentMD5 = newgenFile.HexStringMD5();

			return !currentMD5.Equals(newgenPreviousMD5);
		}
		/// <summary>
		/// Checks if the MD5 of the current User file matches the MD5 of the previous PrevGen file.
		/// </summary>
		/// <returns>True if the file has changed. False if it has not.</returns>
		public bool HasUserFileChanged()
		{
			string currentMD5 = userFile.HexStringMD5();

			return !currentMD5.Equals(userPreviousMD5);
		}

		protected void RaiseParseError(IFileInformation information, FileParseErrorArgs args)
		{
			if (ParseError != null)
			{
				ParseError(information, args);
			}
		}

		/// <summary>
		/// Writes the output of the Diff and Merge to the disk.
		/// </summary>
		/// <param name="absolutePathBase"></param>
		public abstract void WriteMergedFile(string absolutePathBase);

		/// <summary>
		/// Writes the prevgen file out to disk.
		/// </summary>
		/// <param name="absolutePathBase">uses this as a base for the RelativeFilePath. The file will be written to
		/// Path.Combine(absolutePathbase, RelativeFilePath).</param>
		public abstract void WriteNewGenFile(string absolutePathBase);

		/// <summary>
		/// Loads the Prev Gen file.
		/// </summary>
		/// <param name="baseFilePath">The root folder to find the prev gen file in. The RelativeFilePath will be combined with this to produce the filename
		/// to load.</param>
		public abstract void LoadPrevGenFile(string baseFilePath);

		/// <summary>
		/// Loads the User File.
		/// </summary>
		/// <param name="baseFilePath">The root folder to find the user file in. The RelativeFilePath will be combined with this to produce the filename
		/// to load.</param>
		public abstract void LoadUserFile(string baseFilePath);

		/// <summary>
		/// Performs the diff between the 3 files, even if some of them do not exist.
		/// </summary>
		/// <exception cref="InvalidOperationException">Throws an InvalidOperationException if the relative file path has not been set yet.</exception>
		public bool PerformDiff()
		{
			SetupPerformDiff();
			bool result = PerformDiffInternal();
			if (result)
			{
				currentDiffResult.DiffPerformedSuccessfully = true;
			}

			temporaryManifestFile = null;

			return result;
		}

		/// <summary>
		/// Current results of the diff. If one has not been performed, then CurrentDiffResult.DiffPerformedSuccessfully will be false.
		/// </summary>
		public DiffResult CurrentDiffResult
		{
			get { return currentDiffResult; }
		}

		/// <summary>
		/// Performs the diff between the 3 files, even if some of them do not exist.
		/// </summary>
		/// <exception cref="InvalidOperationException">Throws an InvalidOperationException if the relative file path has not been set yet.</exception>
		protected abstract bool PerformDiffInternal();

		/// <summary>
		/// Sets up the object before the diff is performed. Any classes inheriting from this one that providing custom diff methods (PerformDiff calls this itself, 
		/// so you do not need to call it for PerformDiffInternal) should call this first.
		/// </summary>
		protected void SetupPerformDiff()
		{
			if (userFile.HasContents == false && prevgenFile.HasContents == false && newgenFile.HasContents == false)
				throw new InvalidOperationException("Cannot perform a diff if there are no files! ( " + RelativeFilePath + ")");

			if (string.IsNullOrEmpty(RelativeFilePath))
				throw new InvalidOperationException("The relative file path of the FileInformation object has not been set, so we cannot perform a diff.");

			if (diffCodeRootMap == null)
			{
				diffCodeRootMap = new CodeRootMap();
			}
		}
	}

	/// <summary>
	/// Thrown if there was an exceptional circumstance during the diff process. Usually contains an inner exception.
	/// </summary>
	public class DiffException : Exception
	{
		/// <summary>
		/// Constuct a new DiffException with the given exception as the inner exception, and a message with instructions
		/// to examine the inner exception for more detail.
		/// </summary>
		/// <param name="ex">The exception that caused this exception.</param>
		public DiffException(Exception ex)
			: base("Exception occurred during Diff operation, see inner exception.", ex)
		{

		}
	}

	/// <summary>
	/// Represents a file that is part of an ArchAngel project.
	/// </summary>
	/// <typeparam name="T">The type of content in the file. Typically this will be string for text files and byte[] for binary files, although other representations may be possible.</typeparam>
	public abstract class IProjectFile<T> where T : class
	{
		protected bool fileOnDisk;
		protected bool preloadFile;
		protected string path;
		protected DateTime lastAccessTime = DateTime.MinValue;
		protected T fileContents;

		/// <summary>
		/// Set to true if the file is stored on disk, and has a FilePath. 
		/// </summary>
		public virtual bool IsFileOnDisk
		{
			get { return fileOnDisk && File.Exists(path); }
		}

		/// <summary>
		/// Returns true if the file contents are available. 
		/// </summary>
		public virtual bool HasContents
		{
			get
			{
				if (fileOnDisk)
				{
					return File.Exists(path);
				}
				return fileContents != null;
			}
		}

		/// <summary>
		/// The path of this file, if it has one. Returns null if the file is not stored on disk.
		/// If this is set to a path that does not exist, any later calls to GetContents will fail.
		/// </summary>
		public string FilePath
		{
			get { return path; }
			set
			{
				path = value;
				fileOnDisk = true;
			}
		}

		/// <summary>
		/// Returns the contents of the file. This may get the contents from any source, depending on
		/// the implementation.
		/// </summary>
		/// <returns>The contents of the file.</returns>
		public abstract T GetContents();

		/// <summary>
		/// Calculates and returns the Base64 encoded MD5 hash of the file's contents.
		/// </summary>
		/// <returns>The Base64 encoded MD5 hash of the file's contents.</returns>
		public abstract string HexStringMD5();

		/// <summary>
		/// This will replace the current contents of the file with newContents.
		/// </summary>
		/// <param name="newContents">The new contents of the file. if null, the file will be empty, but still exist.</param>
		/// <param name="keepInMemory">If true, the contents will be written to memory but not disk. If false, the file will attempt to write itself
		/// to the filepath it currently has. If this doesn't exist, an InvalidOperation exception will be thrown.</param>
		/// <exception cref="InvalidOperationException">Thrown if keepInMemory is false and path is not writable.</exception>
		public abstract void ReplaceContents(T newContents, bool keepInMemory);

		/// <summary>
		/// This will replace the current contents of the file with newContents, writing the file to path.
		/// </summary>
		/// <param name="newContents">The new contents of the file. if null, the file will be empty, but still exist.</param>
		/// <exception cref="InvalidOperationException">Thrown if path is not writable.</exception>
		/// <param name="filePath">The path to write the file to. The file and directory structure will be created if it does not exist.</param>
		public abstract void ReplaceContents(T newContents, string filePath);
	}

	/// <summary>
	/// Represents a text file that is part of a project.
	/// </summary>
	public class TextFile : IProjectFile<string>
	{
		/// <summary>
		/// Initialises the TextFile with the path of the file it is to represent. 
		/// </summary>
		/// <param name="filePath">The path of the text file.</param>
		/// <param name="preloadFromDisk">Whether the file should be read from disk now. This provides fail fast behaviour, and means that the next
		/// read will be faster.</param>
		/// <exception cref="ArgumentException">Throws an ArgumentException if path is not a file, or if preloadFromDisk was true and the file could not be read from.</exception>
		public TextFile(string filePath, bool preloadFromDisk)
		{
			fileOnDisk = true;
			FilePath = filePath;
			preloadFile = preloadFromDisk;
			if (preloadFromDisk)
			{
				try
				{
					fileContents = File.ReadAllText(filePath);
					lastAccessTime = File.GetLastAccessTimeUtc(filePath);
				}
				catch (IOException e)
				{
					throw new ArgumentException("It was not possible to read from the file at path.", e);
				}
			}
		}

		/// <summary>
		/// Creates an in memory copy of a file. Sets IsFileOnDisk to false, and the Contents() method will never go to disk to get the file contents.
		/// </summary>
		/// <param name="fileContents"></param>
		public TextFile(string fileContents)
		{
			this.fileContents = fileContents;
			fileOnDisk = false;
			path = null;
		}


		/// <summary>
		/// A blank file.
		/// </summary>
		public static IProjectFile<string> Blank
		{
			get
			{
				return new TextFile(null);
			}
		}

		/// <summary>
		/// Returns true if the file contents are available. 
		/// </summary>
		public override bool HasContents
		{
			get
			{
				if (fileOnDisk)
				{
					return File.Exists(path);
				}

				return fileContents != null;
			}
		}

		public override string GetContents()
		{
			if (fileOnDisk == false)
			{
				return fileContents ?? "";
			}

			string tempFileContents;

			if (preloadFile == false || File.GetLastWriteTimeUtc(path) > lastAccessTime)
			{
				try
				{
					tempFileContents = File.ReadAllText(path);
					lastAccessTime = File.GetLastAccessTimeUtc(path);
				}
				catch (IOException e)
				{
					throw new InvalidOperationException("It was not possible to read from the file at path.", e);
				}
			}
			else
			{
				tempFileContents = fileContents;
			}

			if (preloadFile)
			{
				fileContents = tempFileContents;
			}

			return tempFileContents;
		}

		/// <summary>
		/// Returns the Hex encoded MD5 hash.
		/// </summary>
		/// <returns>The Hex encoded MD5 hash.</returns>
		public override string HexStringMD5()
		{
			return HasContents ? Utility.GetCheckSumOfString(GetContents()) : "";
		}

		/// <summary>
		/// This will replace the current contents of the file with newContents.
		/// </summary>
		/// <param name="newContents">The new contents of the file. if null, the file will be empty, but still exist.</param>
		/// <param name="keepInMemory">If true, the contents will be written to memory but not disk. If false, the file will attempt to write itself
		/// to the FilePath it currently has. If this doesn't exist, an InvalidOperation exception will be thrown.</param>
		/// <exception cref="InvalidOperationException">Thrown if keepInMemory is false and path is not writable.</exception>
		public override void ReplaceContents(string newContents, bool keepInMemory)
		{
			if (keepInMemory)
			{
				fileContents = newContents;
				fileOnDisk = false;
			}
			else
			{
				if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
				}
				File.WriteAllText(FilePath, newContents, System.Text.Encoding.Unicode);
				fileOnDisk = true;
			}
		}

		/// <summary>
		/// This will replace the current contents of the file with newContents, writing the file to path.
		/// </summary>
		/// <param name="newContents">The new contents of the file. if null, the file will be empty, but still exist.</param>
		/// <exception cref="InvalidOperationException">Thrown if path is not writable.</exception>
		/// <param name="filepath">The path to write the file to. The file and directory structure will be created if it does not exist.</param>
		public override void ReplaceContents(string newContents, string filepath)
		{
			FilePath = filepath;
			Directory.CreateDirectory(Path.GetDirectoryName(filepath));
			File.CreateText(filepath).Close();
			ReplaceContents(newContents, false);
		}
	}

	/// <summary>
	/// Represents a text file that is part of a project. Subclasses exist for files on disk and
	/// files in memory.
	/// </summary>
	public class BinaryFile : IProjectFile<byte[]>
	{
		/// <summary>
		/// Initialises the TextFile with the path of the file it is to represent. 
		/// </summary>
		/// <param name="filePath">The path of the text file.</param>
		/// <param name="preloadFromDisk">Whether the file should be read from disk now. This provides fail fast behaviour, and means that the next
		/// read will be faster.</param>
		/// <exception cref="ArgumentException">Throws an ArgumentException if path is not a file, or if preloadFromDisk was true and the file could not be read from.</exception>
		public BinaryFile(string filePath, bool preloadFromDisk)
		{
			fileOnDisk = true;
			FilePath = filePath;
			if (preloadFromDisk)
			{
				try
				{
					fileContents = File.ReadAllBytes(filePath);
					lastAccessTime = File.GetLastAccessTimeUtc(filePath);
				}
				catch (IOException e)
				{
					throw new ArgumentException("It was not possible to read from the file at path.", e);
				}
			}
		}

		/// <summary>
		/// Creates an in memory copy of a file. Sets IsFileOnDisk to false, and the Contents() method will never go to disk to get the file contents.
		/// </summary>
		/// <param name="fileContents">The contents of the file.</param>
		public BinaryFile(byte[] fileContents)
		{
			this.fileContents = fileContents;
			fileOnDisk = false;
			path = null;
		}

		public override byte[] GetContents()
		{
			if (fileOnDisk == false)
			{
				return fileContents;
			}

			byte[] tempFileContents;

			if (preloadFile == false || File.GetLastWriteTimeUtc(path) > lastAccessTime)
			{
				try
				{
					tempFileContents = File.ReadAllBytes(path);
					lastAccessTime = File.GetLastAccessTimeUtc(path);
				}
				catch (IOException e)
				{
					throw new ArgumentException("It was not possible to read from the file at path.", e);
				}
			}
			else
			{
				tempFileContents = fileContents;
			}

			if (preloadFile)
			{
				fileContents = tempFileContents;
			}

			return tempFileContents;
		}

		public override string HexStringMD5()
		{
			return HasContents ? Utility.GetCheckSumOfBytes(GetContents()) : "";
		}

		/// <summary>
		/// This will replace the current contents of the file with newContents.
		/// </summary>
		/// <param name="newContents">The new contents of the file. if null, the file will be empty, but still exist.</param>
		/// <param name="keepInMemory">If true, the contents will be written to memory but not disk. If false, the file will attempt to write itself
		/// to the FilePath it currently has. If this doesn't exist, an InvalidOperation exception will be thrown.</param>
		/// <exception cref="InvalidOperationException">Thrown if keepInMemory is false and path is not writable.</exception>
		public override void ReplaceContents(byte[] newContents, bool keepInMemory)
		{
			if (keepInMemory)
			{
				fileContents = newContents;
				fileOnDisk = false;
			}
			else
			{
				File.WriteAllBytes(FilePath, newContents);
				fileOnDisk = true;
			}
		}

		/// <summary>
		/// This will replace the current contents of the file with newContents, writing the file to path.
		/// </summary>
		/// <param name="newContents">The new contents of the file. if null, the file will be empty, but still exist.</param>
		/// <exception cref="InvalidOperationException">Thrown if path is not writable.</exception>
		/// <param name="filepath">The path to write the file to. The file and directory structure will be created if it does not exist.</param>
		public override void ReplaceContents(byte[] newContents, string filepath)
		{
			FilePath = filepath;
			Directory.CreateDirectory(Path.GetDirectoryName(filepath));
			File.CreateText(filepath).Close();
			ReplaceContents(newContents, false);
		}

		public string GetVersionInformation()
		{
			if (!IsFileOnDisk) return "";

			var versionInfo = FileVersionInfo.GetVersionInfo(FilePath);
			return versionInfo.FileVersion ?? "";
		}

		public DateTime? GetFileModificationDate()
		{
			if (!IsFileOnDisk) return null;

			return System.IO.File.GetLastWriteTime(FilePath);
		}

		public string GetMD5()
		{
			if (!IsFileOnDisk) return "";

			return Slyce.Common.Utility.GetCheckSumOfFile(FilePath);
		}

		/// <summary>
		/// A blank file.
		/// </summary>
		public static IProjectFile<byte[]> Blank
		{
			get
			{
				return new BinaryFile(null);
			}
		}

		/// <summary>
		/// Returns the file size in bytes
		/// </summary>
		/// <returns></returns>
		public int GetFileSize()
		{
			if (HasContents == false) return 0;

			// This is ok, because we cache the file in memory.
			return GetContents().Length;
		}
	}
}
