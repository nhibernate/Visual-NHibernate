using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArchAngel.Providers.CodeProvider
{
	///<summary>
	/// Represents the root of an Object Model.
	///</summary>
	public interface ICodeRoot
	{
		/// <summary>
		/// Returns all of the IBaseConstructs in this tree in no particular order.
		/// </summary>
		/// <returns>All of the IBaseConstructs in this tree in no particular order.</returns>
		IEnumerable<IBaseConstruct> WalkTree();
		/// <summary>
		/// Returns all of the child IBaseConstructs in this node in no particular order.
		/// </summary>
		/// <returns>All of the child IBaseConstructs in this node in no particular order.</returns>
		ReadOnlyCollection<IBaseConstruct> WalkChildren();
		/// <summary>
		/// Determines if two code roots are the same.
		/// </summary>
		/// <param name="comparisonCodeRoot">The CodeRoot to compare against.</param>
		/// <param name="depth">The depth to which we should compare. Signature will always
		/// return true, as CodeRoots have no signature. Outer will return true if the UsingStatements
		/// are the same, and complete will compare all children.</param>
		/// <returns>true if the given CodeRoot matches this one to the specified depth.</returns>
		bool IsTheSame(ICodeRoot comparisonCodeRoot, ComparisonDepth depth);
		/// <summary>
		/// Adds the give base construct as a child of this code root.
		/// </summary>
		/// <param name="child">The chidl object to add.</param>
		void AddChild(IBaseConstruct child);
		/// <summary>
		/// Searches for a child with the given unique id.
		/// </summary>
		/// <param name="uid"></param>
		/// <returns>The child with that id, or null if the uid doesn't exist as a child of this construct.</returns>
		IBaseConstruct FindChild(string uid);
		/// <summary>
		/// Returns a new instance of this type of ICodeRoot
		/// </summary>
		/// <returns>A new instance of this type of ICodeRoot</returns>
		ICodeRoot NewInstance();
		/// <summary>
		/// Attempts to parse given code fragment. Uses the originalConstruct to try determine
		/// what the code fragment represents.
		/// </summary>
		/// <exception cref="ParserException">Thrown if the code could not be parsed.</exception>
		/// <param name="code">The code snippet to be parsed</param>
		/// <param name="originalConstruct">The code construct that the original text came from (before user modification)</param>
		/// <returns>A new base construct representing the code given.</returns>
		IBaseConstruct CreateBaseConstruct(string code, IBaseConstruct originalConstruct);
		/// <summary>
		/// Copies top level items from this ICodeRoot into the given ICodeRoot
		/// </summary>
		/// <param name="codeRoot">The ICodeRoot to copy items into.</param>
		void ShallowCloneInto(ICodeRoot codeRoot);
	}
}