using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents .NET reflection binding flags.
	/// </summary>
	[Flags()]
	public enum DomBindingFlags {

		/// <summary>
		/// No flags.
		/// </summary>
		None = 0x0,

		/// <summary>
		/// Whether to ignore character case when doing name matches.
		/// </summary>
		IgnoreCase = 0x1,

		/// <summary>
		/// The type must has a constructor that is accessible using the specified access scope.
		/// </summary>
		HasConstructor = 0x2,

		/// <summary>
		/// The binding should assume an object reference, such as a this or base reference. 
		/// </summary>
		ObjectReference = 0x4,

		/// <summary>
		/// The context type is in the same family as the target type.  This flag is for internal use only.
		/// </summary>
		ContextIsTargetFamily = 0x8,

		/// <summary>
		/// The context type is the same as the declaring type.  This flag is for internal use only.
		/// </summary>
		ContextIsDeclaringType = 0x10,

		/// <summary>
		/// Only include members on the declaring type, not on any base types.
		/// </summary>
		DeclaringTypeOnly = 0x20,

		/// <summary>
		/// Include private members.
		/// </summary>
		Private = 0x100,

		/// <summary>
		/// Include assembly (internal) members.
		/// </summary>
		Assembly = 0x200,

		/// <summary>
		/// Include family (protected) members.
		/// </summary>
		Family = 0x400,

		/// <summary>
		/// Include public members.
		/// </summary>
        Public = 0x800,

		/// <summary>
		/// Include all access types (Public, Assembly, Family, and Private).
		/// </summary>
		AllAccessTypes = 0xF00,

		/// <summary>
		/// Include instance members.
		/// </summary>
		Instance = 0x1000,

		/// <summary>
		/// Include static members.
		/// </summary>
		Static = 0x2000,

		/// <summary>
		/// The default options (Instance, Static, and AllAccessTypes).
		/// </summary>
		Default = 0x3F00,
		
		/// <summary>
		/// Indexers should be excluded in member lists (for use with C#-like languages).
		/// </summary>
		ExcludeIndexers = 0x10000,

		/// <summary>
		/// Indexers should be the only thing included in member lists (for use with C#-like languages).
		/// </summary>
		OnlyIndexers = 0x20000,

		/// <summary>
		/// Constructors should be the only thing included in member lists.
		/// </summary>
		OnlyConstructors = 0x40000,

		/// <summary>
		/// Excludes editor never-browsable items.
		/// </summary>
		ExcludeEditorNeverBrowsable = 0x80000,

	}
}
