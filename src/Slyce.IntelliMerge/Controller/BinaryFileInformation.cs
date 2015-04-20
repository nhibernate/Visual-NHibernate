using System;
using System.IO;

namespace Slyce.IntelliMerge.Controller
{
	/// <summary>
	/// This class represents a set of versions of projects files. The User's files are
	/// the files in the users project directory, which they work on. The previously generated
	/// (prevgen) files are the files stored from the last ArchAngel file generation. The
	/// newly generated (newgen) files are the ones that were generated in this session but haven't
	/// been merged into the user's files yet.
	/// The PerformDiff method is used to do the Diff on the 3 files. This creates a DiffFile, which contains information
	/// about the outcome of the diff.
	/// </summary>
	public class BinaryFileInformation : FileInformation<byte[]>
	{
		/// <summary>
		/// Sets the various files to blanks, so that they are never null.
		/// </summary>
		public BinaryFileInformation()
		{
			PrevGenFile = BinaryFile.Blank;
			NewGenFile = BinaryFile.Blank;
			UserFile = BinaryFile.Blank;
			MergedFile = BinaryFile.Blank;
			RelativeFilePath = "Test.cs"; // We don't need this for our diff, but it has to be set to something
		}

		/// <summary>
		/// Performs the diff between the 3 files, even if some of them do not exist.
		/// </summary>
		protected override bool PerformDiffInternal()
		{
			MergedFile = NewGenFile;

			if (!UserFile.IsFileOnDisk)
				CurrentDiffResult.DiffType = TypeOfDiff.NewFile;
			else if (UserFile.HexStringMD5() == NewGenFile.HexStringMD5())
				CurrentDiffResult.DiffType = TypeOfDiff.ExactCopy;
			else
				CurrentDiffResult.DiffType = TypeOfDiff.TemplateChangeOnly;

			return true;

			if (IntelliMerge == IntelliMergeType.Overwrite)
			{
				CurrentDiffResult.DiffType = TypeOfDiff.ExactCopy;
				MergedFile = NewGenFile;
				return true;
			}

			string crcParent = PrevGenFile.HasContents ? Common.Utility.GetMD5HashString(PrevGenFile.GetContents()) : "";
			string crcTemplate = NewGenFile.HasContents ? Common.Utility.GetMD5HashString(NewGenFile.GetContents()) : "";
			string crcUser = UserFile.HasContents ? Common.Utility.GetMD5HashString(UserFile.GetContents()) : "";

			if (!string.IsNullOrEmpty(crcParent) &&
				!string.IsNullOrEmpty(crcTemplate) &&
				!string.IsNullOrEmpty(crcUser))
			{
				if (crcParent == crcUser &&
					crcUser == crcTemplate)
				{
					// Nothing has changed. Copy the user file.
					CurrentDiffResult.DiffType = TypeOfDiff.ExactCopy;
					MergedFile = new BinaryFile(UserFile.GetContents());
				}
				else if (crcParent == crcUser &&
						 crcParent != crcTemplate)
				{
					// The template file has changed. Copy the template file.
					CurrentDiffResult.DiffType = TypeOfDiff.TemplateChangeOnly;
					MergedFile = new BinaryFile(NewGenFile.GetContents());
				}
				else if (crcParent != crcUser &&
						 crcParent == crcTemplate)
				{
					// The user file has changed. Copy the user file.
					CurrentDiffResult.DiffType = TypeOfDiff.UserChangeOnly;
					MergedFile = new BinaryFile(UserFile.GetContents());
				}
				else if (crcParent != crcUser &&
						 crcUser == crcTemplate)
				{
					//There are changes in the user and template files that do not conflict
					CurrentDiffResult.DiffType = TypeOfDiff.UserAndTemplateChange;
					MergedFile = new BinaryFile(UserFile.GetContents());
				}
				else if (crcParent != crcUser && crcUser != crcTemplate)
				{
					//There are conflicting changes in the user and template files
					CurrentDiffResult.DiffType = TypeOfDiff.Conflict;
				}
			}
			else if (string.IsNullOrEmpty(crcParent) &&
					 !string.IsNullOrEmpty(crcTemplate) &&
					 !string.IsNullOrEmpty(crcUser))
			{
				if (crcTemplate == crcUser)
				{
					CurrentDiffResult.DiffType = TypeOfDiff.UserAndTemplateChange;
					MergedFile = new BinaryFile(UserFile.GetContents());
				}
				else
				{
					CurrentDiffResult.DiffType = TypeOfDiff.Conflict;
				}
			}
			else if (!string.IsNullOrEmpty(crcParent) &&
					 !string.IsNullOrEmpty(crcTemplate) &&
					 string.IsNullOrEmpty(crcUser))
			{
				// User file missing. Use new template file.
				if (crcParent == crcTemplate)
				{
					CurrentDiffResult.DiffType = TypeOfDiff.ExactCopy;
				}
				else
				{
					CurrentDiffResult.DiffType = TypeOfDiff.TemplateChangeOnly;
				}
				MergedFile = new BinaryFile(NewGenFile.GetContents());
			}
			else if (!string.IsNullOrEmpty(crcParent) &&
					 string.IsNullOrEmpty(crcTemplate) &&
					 !string.IsNullOrEmpty(crcUser))
			{
				// The newgen file is missing but user and prevgen are the same. Mark as ExactCopy
				if (crcUser == crcParent)
				{
					CurrentDiffResult.DiffType = TypeOfDiff.ExactCopy;
				}
				else
				{
					CurrentDiffResult.DiffType = TypeOfDiff.UserChangeOnly;
				}
				MergedFile = new BinaryFile(UserFile.GetContents());
			}
			else if (string.IsNullOrEmpty(crcParent) &&
					 !string.IsNullOrEmpty(crcTemplate) &&
					 string.IsNullOrEmpty(crcUser))
			{
				// This is a new file that has been generated
				CurrentDiffResult.DiffType = TypeOfDiff.ExactCopy;
				MergedFile = new BinaryFile(NewGenFile.GetContents());
			}
			else if (!string.IsNullOrEmpty(crcParent) &&
					 string.IsNullOrEmpty(crcTemplate) &&
					 string.IsNullOrEmpty(crcUser))
			{
				// Only an old template exists
				CurrentDiffResult.DiffType = TypeOfDiff.ExactCopy;
				MergedFile = new BinaryFile(PrevGenFile.GetContents());
			}
			else if (string.IsNullOrEmpty(crcParent) &&
					 string.IsNullOrEmpty(crcTemplate) &&
					 !string.IsNullOrEmpty(crcUser))
			{
				// Only a user file exists
				CurrentDiffResult.DiffType = TypeOfDiff.ExactCopy;
				MergedFile = new BinaryFile(UserFile.GetContents());
			}
			else
			{
				throw new NotImplementedException("Not coded yet. Seems like only one version of binary file exists. Probably just need to copy as-is.");
			}

			return true;
		}

		public override bool CalculateMergedFile()
		{
			// We always create the merged file during the diff.
			return true;
		}

		public override void WriteMergedFile(string absolutePathBase)
		{
			if (!MergedFile.HasContents)
				throw new WriteOutException("The Merged File does not have any contents. Has the file been analysed?");

			string filePath = Path.Combine(absolutePathBase, RelativeFilePath);
			string dir = Path.GetDirectoryName(filePath);

			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			File.WriteAllBytes(Path.Combine(absolutePathBase, RelativeFilePath), MergedFile.GetContents());
		}

		public override void WriteNewGenFile(string absolutePathBase)
		{
			if (!NewGenFile.HasContents)
				throw new WriteOutException("The Template File does not have any contents. Has the file been analysed?");

			string filePath = Path.Combine(absolutePathBase, RelativeFilePath);
			string dir = Path.GetDirectoryName(filePath);

			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);
			File.WriteAllBytes(Path.Combine(absolutePathBase, RelativeFilePath), NewGenFile.GetContents());
		}

		public override void LoadPrevGenFile(string baseFilePath)
		{
			string filename = Path.Combine(baseFilePath, RelativeFilePath);
			PrevGenFile = File.Exists(filename) ? new BinaryFile(filename, false) : BinaryFile.Blank;
			ReloadFiles = true;
		}

		public override void LoadUserFile(string baseFilePath)
		{
			string filename = Path.Combine(baseFilePath, RelativeFilePath);
			UserFile = File.Exists(filename) ? new BinaryFile(filename, false) : BinaryFile.Blank;
			ReloadFiles = true;
		}
	}
}