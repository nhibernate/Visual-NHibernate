using System;
using System.Collections.Generic;
using System.ComponentModel;
using ArchAngel.Providers.CodeProvider;
using Slyce.Common;

namespace Slyce.IntelliMerge.Controller
{
	/// <summary>
	/// This interface 
	/// The PerformDiff method is used to do the Diff on the 3 files. This creates a DiffFile, which contains information
	/// about the outcome of the diff.
	/// </summary>
	public interface IFileInformation
	{
		/// <summary>
		/// This event is raised when an error occurs during the processing of code files.
		/// </summary>
		event EventHandler<FileParseErrorArgs> ParseError;

		/// <summary>
		/// The output of the diff process. If the diff has not been performed yet,
		/// this should perform it and return the result. 
		/// </summary>
		CodeRootMap CodeRootMap { get; }

		/// <summary>
		/// Performs the diff between the 3 files, even if some of them do not exist.
		/// </summary>
		bool PerformDiff();

		/// <summary>
		/// Current results of the diff. If one has not been performed, then CurrentDiffResult.DiffPerformedSuccessfully will be false.
		/// </summary>
		DiffResult CurrentDiffResult { get; }

		/// <summary>
		/// The relative path of this file within the project.
		/// </summary>
		string RelativeFilePath { get; }

		/// <summary>
		/// Set to true if the next diff should clear their previous state and reload the files from disk.
		/// </summary>
		bool ReloadFiles { get; set; }

		/// <summary>
		/// Writes the output of the Diff and Merge to the disk.
		/// </summary>
		/// <param name="absolutePathBase"></param>
		void WriteMergedFile(string absolutePathBase);

		/// <summary>
		/// Writes the prevgen file out to disk.
		/// </summary>
		/// <param name="absolutePathBase">uses this as a base for the RelativeFilePath. The file will be written to
		/// Path.Combine(absolutePathbase, RelativeFilePath).</param>
		void WriteNewGenFile(string absolutePathBase);

		/// <summary>
		/// Loads the Prev Gen file.
		/// </summary>
		/// <param name="baseFilePath">The root folder to find the prev gen file in. The RelativeFilePath will be combined with this to produce the filename
		/// to load.</param>
		void LoadPrevGenFile(string baseFilePath);

		/// <summary>
		/// Loads the User File.
		/// </summary>
		/// <param name="baseFilePath">The root folder to find the user file in. The RelativeFilePath will be combined with this to produce the filename
		/// to load.</param>
		void LoadUserFile(string baseFilePath);

		/// <summary>
		/// Language retrieved from the function that generated the NewGenFile. This should be "C#" for C# files, "Plain Text" for plain text, ect.
		/// </summary>
		TemplateContentLanguage? TemplateLanguage { get; set; }

		/// <summary>
		/// The type of diff and merge that will be applied to the file.
		/// </summary>
		IntelliMergeType IntelliMerge
		{
			get;
			set;
		}

		/// <summary>
		/// Sets the MD5 hash strings (represented in as hexadecimal numbers) for the these files when they were last written out.
		/// These are used to see if they have changed since the last time the generation, analysis and writing to the user's directory
		/// happened.
		/// </summary>
		/// <param name="prevgen">The MD5 of the prevgen file.</param>
		/// <param name="newgen">The MD5 of the newgen file.</param>
		/// <param name="user">The MD5 of the user file.</param>
		void SetPreviousVersionMD5s(String prevgen, string newgen, string user);

		/// <summary>
		/// True if a Merged file has been created. 
		/// </summary>
		bool MergedFileExists { get; }

		/// <summary>
		/// Creates a MergedFile if it is possible - if any conflicts have been handled.
		/// </summary>
		/// <returns>True if the MergedFile was created successfully, false if it was not possible to create one.</returns>
		bool CalculateMergedFile();

		/// <summary>
		/// Checks if the MD5 of the current PrevGen file matches the MD5 of the previous PrevGen file.
		/// </summary>
		/// <returns>True if the file has changed. False if it has not.</returns>
		bool HasPrevGenFileChanged();
		/// <summary>
		/// Checks if the MD5 of the current NewGen file matches the MD5 of the previous PrevGen file.
		/// </summary>
		/// <returns>True if the file has changed. False if it has not.</returns>
		bool HasNewGenFileChanged();
		/// <summary>
		/// Checks if the MD5 of the current User file matches the MD5 of the previous PrevGen file.
		/// </summary>
		/// <returns>True if the file has changed. False if it has not.</returns>
		bool HasUserFileChanged();

		/// <summary>
		/// Loads any custom matches from the Manifest file.
		/// </summary>
		/// <param name="prevgenPath">The path to the temporary prevgen directory. This assumes a flat 
		/// directory structure, so the manifest file for BLL\Test.cs would be in prevgenPath\BLL\{Manifest filename}.</param>
		void LoadCustomMatches(string prevgenPath);
	}

	public class DiffResult
	{
		/// <summary>
		/// True if the diff has been performed successfully.
		/// </summary>
		private bool diffPerformedSuccessfully;
		/// <summary>
		/// The output of the last diff.
		/// </summary>
		private TypeOfDiff diffType = TypeOfDiff.ExactCopy;
		/// <summary>
		/// Description of the warning encountered during file parse.
		/// </summary>
		private string parserWarningDescription = "";
		/// <summary>
		/// Description of the warning that was encoutnered durring the Diff process.
		/// </summary>
		private string diffWarningDescription = "";

		/// <summary>
		/// True if the diff has been performed successfully.
		/// </summary>
		public bool DiffPerformedSuccessfully
		{
			get { return diffPerformedSuccessfully; }
			internal set { diffPerformedSuccessfully = value; }
		}

		/// <summary>
		/// The output of the last diff.
		/// </summary>
		public TypeOfDiff DiffType
		{
			get { return diffType; }
			internal set { diffType = value; }
		}

		/// <summary>
		/// To be called when no analysis is being performed and files are to be overwritten, not merged.
		/// </summary>
		public void SetAsOverwite()
		{
			diffType = TypeOfDiff.NewFile;
		}

		/// <summary>
		/// Description of the warning encountered during file parse.
		/// </summary>
		public string ParserWarningDescription
		{
			get { return parserWarningDescription; }
			internal set { parserWarningDescription = value; }
		}

		/// <summary>
		/// Description of the warning that was encoutnered durring the Diff process.
		/// </summary>
		public string DiffWarningDescription
		{
			get { return diffWarningDescription; }
			internal set { diffWarningDescription = value; }
		}
	}

	/// <summary>
	/// Represents the type of diff and merge that will be applied to the file.
	/// </summary>
	public enum IntelliMergeType
	{
		/// <summary>
		/// Automatically determine what kind of diff and merge to do based on our own logic.
		/// </summary>
		[Description("AutoDetect")]
		AutoDetect,
		/// <summary>
		/// Treat the file as a C# code file for the purposes of doing the diff. If this is applied to a non-C# file, exceptions will
		/// be thrown during the diff. This is here to allow the user to override our automatic file type checking.
		/// </summary>
		[Description("C# IntelliMerge")]
		CSharp,
		/// <summary>
		/// Treat the file as a plain text file, and just do a regular text diff on it.
		/// </summary>
		[Description("Plain Text")]
		PlainText,
		/// <summary>
		/// Don't do a diff at all, just overwrite the file with whatever was newly generated.
		/// </summary>
		[Description("Overwrite from Template")]
		Overwrite,
		/// <summary>
		/// Don't do a diff at all, just overwrite the file with whatever was newly generated.
		/// </summary>
		[Description("Create Only")]
		CreateOnly,
		/// <summary>
		/// The IntelliMerge type has not been set. This can be used to force the user to choose 
		/// an IntelliMerge type.
		/// </summary>
		[Description("Not Set")]
		NotSet
	}

	public static class IntelliMergeTypeUtility
	{
		private static readonly Dictionary<IntelliMergeType, string> _ShortNamesDic = new Dictionary<IntelliMergeType, string>();
		private static readonly Dictionary<string, IntelliMergeType> _IntelliMergeDic = new Dictionary<string, IntelliMergeType>();
		private static readonly string[] _ShortNames;

		static IntelliMergeTypeUtility()
		{
			_ShortNamesDic.Add(IntelliMergeType.AutoDetect, "Use Default");
			_ShortNamesDic.Add(IntelliMergeType.CSharp, "C# IntelliMerge");
			_ShortNamesDic.Add(IntelliMergeType.PlainText, "Plain Text");
			_ShortNamesDic.Add(IntelliMergeType.Overwrite, "Overwrite");
			_ShortNamesDic.Add(IntelliMergeType.CreateOnly, "Create Only");
			_ShortNamesDic.Add(IntelliMergeType.NotSet, "Not Set");

			_IntelliMergeDic.Add("Use Default", IntelliMergeType.AutoDetect);
			_IntelliMergeDic.Add("C# IntelliMerge", IntelliMergeType.CSharp);
			_IntelliMergeDic.Add("Plain Text", IntelliMergeType.PlainText);
			_IntelliMergeDic.Add("Overwrite", IntelliMergeType.Overwrite);
			_IntelliMergeDic.Add("Create Only", IntelliMergeType.CreateOnly);
			_IntelliMergeDic.Add("Not Set", IntelliMergeType.NotSet);

			_ShortNames = new string[_ShortNamesDic.Count];
			_ShortNamesDic.Values.CopyTo(_ShortNames, 0);
		}

		public static string ShortName(IntelliMergeType type)
		{
			return _ShortNamesDic[type];
		}

		public static string[] ShortNames { get { return _ShortNames; } }

		public static IntelliMergeType FromShortName(string value)
		{
			IntelliMergeType retVal;
			return _IntelliMergeDic.TryGetValue(value, out retVal) ? retVal : IntelliMergeType.NotSet;
		}
	}

	/// <summary>
	/// Contains information about the parse error that occured during formatting of a text file.
	/// </summary>
	public class FileParseErrorArgs : EventArgs
	{
		private ParserSyntaxError parseError;
		private FileVersion fileVersion;
		private IFileInformation fileInfo;

		public FileParseErrorArgs(ParserSyntaxError parseError, FileVersion fileVersion, IFileInformation fileInfo)
		{
			this.parseError = parseError;
			this.fileVersion = fileVersion;
			this.fileInfo = fileInfo;
		}

		public ParserSyntaxError ParseError
		{
			get { return parseError; }
			set { parseError = value; }
		}

		public FileVersion FileVersion
		{
			get { return fileVersion; }
			set { fileVersion = value; }
		}

		public IFileInformation FileInfo
		{
			get { return fileInfo; }
			set { fileInfo = value; }
		}
	}

	public enum FileVersion
	{
		User,
		PrevGen,
		Template
	}

	public class WriteOutException : Exception
	{
		public WriteOutException(string s)
			: base(s)
		{
		}
	}
}
