using System.Collections.ObjectModel;
using System.Threading;

namespace ArchAngel.Providers.CodeProvider
{
	///<summary>
	/// Interface for Object Model parsers.
	///</summary>
	public interface IParser
	{
		///<summary>
		/// True if any errors occurred during parsing/formatting. The error descriptions
		/// can be retrieved from the SyntaxErrors property.
		///</summary>
		bool ErrorOccurred { get; }

		/// <summary>
		/// Contains the syntax errors that occured during parsing/formatting. Empty if there were none.
		/// </summary>
		ReadOnlyCollection<ParserSyntaxError> SyntaxErrors { get; }

		/// <summary>
		/// Gets the CodeRoot that was created by the ParseCode method.
		/// </summary>
		ICodeRoot CreatedCodeRoot { get; }

		/// <summary>
		/// Removes all state from this object so it can be reused for a different parse.
		/// </summary>
		void Reset();

		/// <summary>
		/// Parses the given code. Uses the default filename of DefaultFilename.cs
		/// </summary>
		/// <param name="code">The code to parse</param>
		/// <returns>A WaitHandle that will be signalled when the code is parsed and 
		/// the CodeRoot is ready for use.</returns>
		void ParseCode(string code);

		/// <summary>
		/// Parses the given code and creates a C# CodeRoot from it.
		/// </summary>
		/// <param name="filename">The name of the file being parsed. Informational use only.</param>
		/// <param name="code">The code to parse</param>
		/// <returns>A WaitHandle that will be signalled when the code is parsed and 
		/// the CodeRoot is ready for use.</returns>
		void ParseCode(string filename, string code);

		/// <summary>
		/// Parses the given code asynchronously. Uses the default filename of DefaultFilename.cs
		/// </summary>
		/// <param name="code">The code to parse</param>
		/// <returns>A WaitHandle that will be signalled when the code is parsed and 
		/// the CodeRoot is ready for use.</returns>
		WaitHandle ParseCodeAsync(string code);

		/// <summary>
		/// Parses the given code asynchronously and creates a C# CodeRoot from it.
		/// </summary>
		/// <param name="filename">The name of the file being parsed. Informational use only.</param>
		/// <param name="code">The code to parse</param>
		/// <returns>A WaitHandle that will be signalled when the code is parsed and 
		/// the CodeRoot is ready for use.</returns>
		WaitHandle ParseCodeAsync(string filename, string code);

		string GetFormattedErrors();
	}
}