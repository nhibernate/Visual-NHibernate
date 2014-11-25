using System;
using System.IO;
using System.Text;
using System.Threading;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.CSharp;
using Slyce.IntelliMerge.Controller.ManifestWorkers;

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
	public class TextFileInformation : FileInformation<string>
	{
		private Encoding _Encoding = Encoding.Unicode;

		/// <summary>
		/// Sets the various files to blanks, so that they are never null.
		/// </summary>
		public TextFileInformation()
		{
			PrevGenFile = TextFile.Blank;
			NewGenFile = TextFile.Blank;
			UserFile = TextFile.Blank;
			MergedFile = TextFile.Blank;
			RelativeFilePath = "Test.cs"; // We don't need this for our diff, but it has to be set to something
		}

		public Encoding Encoding
		{
			get { return _Encoding; }
			set { _Encoding = value; }
		}

		/// <summary>
		/// Performs the diff between the 3 files, even if some of them do not exist.
		/// </summary>
		protected override bool PerformDiffInternal()
		{
			//////////////////////////
			MergedFile = NewGenFile;

			if (!UserFile.IsFileOnDisk)
				CurrentDiffResult.DiffType = TypeOfDiff.NewFile;
			else if (UserFile.HexStringMD5() == NewGenFile.HexStringMD5())
				CurrentDiffResult.DiffType = TypeOfDiff.ExactCopy;
			else
				CurrentDiffResult.DiffType = TypeOfDiff.TemplateChangeOnly;

			return true;
			///////////////////////////
			if (IntelliMerge == IntelliMergeType.Overwrite)
			{
				CurrentDiffResult.DiffType = TypeOfDiff.ExactCopy;
				MergedFile = NewGenFile;
				return true;
			}

			if (IntelliMerge == IntelliMergeType.CreateOnly)
			{
				CurrentDiffResult.DiffType = TypeOfDiff.ExactCopy;
				MergedFile = UserFile.HasContents ? UserFile : NewGenFile;

				return true;
			}

			if (UserFile.HasContents == false && PrevGenFile.HasContents == false && NewGenFile.HasContents == false)
			{
				throw new InvalidOperationException("Cannot perform a diff if there are no files!");
			}

			if (MergedFileExists)
			{
				CurrentDiffResult.DiffPerformedSuccessfully = true;
				CurrentDiffResult.DiffType = TypeOfDiff.ExactCopy;
				CurrentDiffResult.DiffWarningDescription = CurrentDiffResult.ParserWarningDescription = "";

				return true;
			}

			if (PrevGenFile.HasContents &&
				UserFile.HasContents &&
				NewGenFile.HasContents)
			{
				// Perform 3-way diff
				string fileBodyParent = PrevGenFile.GetContents();
				string fileBodyUser = UserFile.GetContents();
				// Template code is not formatted until it is needed. Do the formatting here.

				string fileBodyGenerated;
				string mergedText;
				SlyceMergeResult slyceMerge;
				if (IntelliMerge == IntelliMergeType.CSharp)
				{
					if (NewGenFile.GetContents().Trim() == string.Empty)
					{
						fileBodyGenerated = "";
					}
					else
					{
						CSharpParser formatter = new CSharpParser();
						formatter.ParseCode(NewGenFile.FilePath, NewGenFile.GetContents());
						if (formatter.ErrorOccurred)
						{
							CurrentDiffResult.ParserWarningDescription = formatter.GetFormattedErrors();
							return false;
						}
						CodeRoot codeRoot = (CodeRoot)formatter.CreatedCodeRoot;
						fileBodyGenerated = codeRoot.ToString();
					}
					slyceMerge = SlyceMerge.Perform3wayDiff(fileBodyUser, fileBodyParent, fileBodyGenerated, out mergedText, false);
				}
				else
				{
					fileBodyGenerated = NewGenFile.GetContents();
					slyceMerge = SlyceMerge.Perform3wayDiff(fileBodyUser, fileBodyParent, fileBodyGenerated, out mergedText, true);
					MergedFile = new TextFile(mergedText);
				}
				CurrentDiffResult.DiffType = slyceMerge.DiffType;

				if (slyceMerge.DiffType == TypeOfDiff.Warning)
				{
					// TODO: What should be done here?
					throw new Exception(
						 "There was a warning during the diff process when there shouldn't have been. Please report this to Slyce.");
				}

				if (slyceMerge.DiffType != TypeOfDiff.ExactCopy)
				{
					return PerformSuperDiff();
				}

				// File was exact copy - use user version
				MergedFile = new TextFile(fileBodyUser);
			}
			else if (PrevGenFile.HasContents &&
					 UserFile.HasContents == false &&
					 NewGenFile.HasContents)
			{
				// No user file, just use the template file
				CurrentDiffResult.DiffType = TypeOfDiff.Warning;
				CurrentDiffResult.DiffWarningDescription =
					 "The User's version of this file has been deleted or renamed, but the Template and previous version of this file still exist.";
				MergedFile = new TextFile(NewGenFile.GetContents());
			}
			else if (PrevGenFile.HasContents == false &&
					 UserFile.HasContents &&
					 NewGenFile.HasContents)
			{
				//CurrentDiffResult.DiffType = TypeOfDiff.Warning;
				//CurrentDiffResult.DiffWarningDescription =
				//    "User version of a file clashes with a new file the template is trying to create.";

				// Perform 2-way diff
				string fileBodyNewGen = NewGenFile.GetContents();
				string fileBodyUser = UserFile.GetContents();
				CurrentDiffResult.DiffType = SlyceMerge.PerformTwoWayDiff(fileBodyNewGen, fileBodyUser);

				if (CurrentDiffResult.DiffType != TypeOfDiff.ExactCopy)
				{
					// Also perform a super diff 
					return PerformSuperDiff();
				}
				MergedFile = new TextFile(fileBodyUser);
			}
			else if (PrevGenFile.HasContents == false &&
					 UserFile.HasContents == false &&
					 NewGenFile.HasContents)
			{
				// The template has added a new file.
				CurrentDiffResult.DiffType = TypeOfDiff.ExactCopy;
				MergedFile = new TextFile(NewGenFile.GetContents());
			}
			else
			{
				// Cases covered by this else:
				// * User and prevgen file exist, no template
				// * Prevgen, no user or template.
				// * User file, no template or prevgen
				// TODO: Shouldn't really be a warning...
				CurrentDiffResult.DiffType = TypeOfDiff.Warning;
				throw new Exception(string.Format("TODO: determine course of action, what should be copied to staging folder, because no file exists: \nparent file path:\"{0}\" : {1}\nuser file path:\"{2}\" : {3}\ntemplate file path:\"{4}\" : {5}", PrevGenFile.FilePath, PrevGenFile.HasContents, UserFile.FilePath, UserFile.HasContents, NewGenFile.FilePath, NewGenFile.HasContents));
			}

			return true;
		}

		/// <summary>
		/// Performs an in depth diff by breaking code files into their constituent parts (functions, properties
		/// etc, so that these elements can be diffed without regard to their ordering.
		/// </summary>
		/// <remarks>
		/// I've put some Thread.Sleep(1) calls in here to ensure that this processing doesn't bring a single core machine 
		/// to its knees. This should ensure that any GUI or other background threads can still get some CPU time,
		/// as there is not much in here that could cause a context switch.
		/// </remarks>
		public bool PerformSuperDiff()
		{
			SetupPerformDiff();

			try
			{
				switch (IntelliMerge)
				{
					case IntelliMergeType.CSharp:
						CSharpParser formatter = new CSharpParser();

						try
						{
							// Reset the DiffType
							CurrentDiffResult.DiffType = TypeOfDiff.ExactCopy;

							if (diffCodeRootMap.PrevGenCodeRoot == null && PrevGenFile.HasContents || ReloadFiles)
							{
								formatter.Reset();
								string filename = string.IsNullOrEmpty(PrevGenFile.FilePath) ? "Prev Gen File" : PrevGenFile.FilePath;
								formatter.ParseCode(filename, PrevGenFile.GetContents());
								if (formatter.ErrorOccurred)
								{
									return false;
								}

								diffCodeRootMap.AddCodeRoot(formatter.CreatedCodeRoot, Version.PrevGen);
							}
							// Force a context switch
							Thread.Sleep(1);

							if (diffCodeRootMap.NewGenCodeRoot == null && NewGenFile.HasContents || ReloadFiles)
							{
								formatter.Reset();
								string filename = string.IsNullOrEmpty(NewGenFile.FilePath) ? "New Gen File" : NewGenFile.FilePath;
								formatter.ParseCode(filename, NewGenFile.GetContents());
								if (formatter.ErrorOccurred)
									return false;
								diffCodeRootMap.AddCodeRoot(formatter.CreatedCodeRoot, Version.NewGen);
							}

							// Force a context switch
							Thread.Sleep(1);

							if (diffCodeRootMap.UserCodeRoot == null && UserFile.HasContents || ReloadFiles)
							{
								formatter.Reset();
								string filename = string.IsNullOrEmpty(UserFile.FilePath) ? "User File" : UserFile.FilePath;
								formatter.ParseCode(filename, UserFile.GetContents());
								if (formatter.ErrorOccurred)
								{
									return false;
								}
								diffCodeRootMap.AddCodeRoot(formatter.CreatedCodeRoot, Version.User);
							}

							// Force a context switch
							Thread.Sleep(1);

							// Set this back to false. If it was true, we have reloaded the files, if it was already false this
							// does nothing.
							ReloadFiles = false;
						}
						catch (Exception ex)
						{
							CurrentDiffResult.ParserWarningDescription = ex.Message;
							return false;
						}
						finally
						{
							if (formatter.ErrorOccurred)
							{
								CurrentDiffResult.ParserWarningDescription = formatter.GetFormattedErrors();
							}
						}

						if (string.IsNullOrEmpty(temporaryManifestFile) == false && manifestFileApplied == false)
						{
							CodeRootMapMatchProcessor processor = new CodeRootMapMatchProcessor();
							processor.LoadCustomMappings(ManifestConstants.LoadManifestDocument(temporaryManifestFile), diffCodeRootMap, Path.GetFileName(RelativeFilePath));
							manifestFileApplied = true;
						}

						CurrentDiffResult.DiffType = diffCodeRootMap.Diff();

						if (CurrentDiffResult.DiffType != TypeOfDiff.Conflict && CurrentDiffResult.DiffType != TypeOfDiff.Warning)
						{
							//mergedFile = new TextFile(CodeRootMap.GetMergedCodeRoot().ToString());
							//    Slyce.Common.Utility.WriteToFile(newFile, userFile.GetContents()); 
						}



						break;
					default:
						// No SuperDiff available for this type of file (no parser created yet).
						break;
				}
			}
			catch (Exception ex)
			{
				throw new DiffException(ex);
			}

			return true;
		}

		public override bool CalculateMergedFile()
		{
			// Plain text files create the merged file during their diff/merge.
			if (diffCodeRootMap.ContainsCodeRoots == false)
			{
				return true;
			}

			if (IntelliMerge == IntelliMergeType.CSharp && diffCodeRootMap.Diff() != TypeOfDiff.Conflict)
			{
				MergedFile = new TextFile(diffCodeRootMap.GetMergedCodeRoot().ToString());
				return true;
			}

			return false;
		}

		public override void WriteMergedFile(string absolutePathBase)
		{
			if (CalculateMergedFile() == false)
				throw new WriteOutException(string.Format("Could not create the merged file for file \"{0}\". It has conflicts.", RelativeFilePath));

			if (!MergedFile.HasContents)
			{
				// This occurs when the user analyses the files, then deletes the existing files on disk, then tries to write the files to disk
				if (!File.Exists(MergedFile.FilePath))
				{
					WriteNewGenFile(absolutePathBase);
					return;
				}
				else
					throw new WriteOutException("The Merged File does not have any contents. Has the file been analysed?");
			}
			string filePath = Path.Combine(absolutePathBase, RelativeFilePath);
			Directory.CreateDirectory(Path.GetDirectoryName(filePath));
			string text = Common.Utility.StandardizeLineBreaks(MergedFile.GetContents(), Environment.NewLine);
			File.WriteAllText(filePath, text, Encoding);
		}

		public override void WriteNewGenFile(string absolutePathBase)
		{
			if (!NewGenFile.HasContents)
				throw new WriteOutException("The Template File does not have any contents.?");

			string filePath = Path.Combine(absolutePathBase, RelativeFilePath);
			Directory.CreateDirectory(Path.GetDirectoryName(filePath));
			string text = NewGenFile.GetContents();
			File.WriteAllText(filePath, text, Encoding);
		}

		public override void LoadPrevGenFile(string baseFilePath)
		{
			string filename = Path.Combine(baseFilePath, RelativeFilePath);
			PrevGenFile = File.Exists(filename) ? new TextFile(filename, false) : TextFile.Blank;
			ReloadFiles = true;
		}

		public override void LoadUserFile(string baseFilePath)
		{
			string filename = Path.Combine(baseFilePath, RelativeFilePath);
			UserFile = File.Exists(filename) ? new TextFile(filename, false) : TextFile.Blank;
			ReloadFiles = true;
		}
	}
}
