using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Context {
	
	/// <summary>
	/// Specifies the type of a <see cref="DotNetContextItem"/>.
	/// </summary>
	public enum DotNetContextItemType {
		
		/// <summary>
		/// The type is unknown.
		/// </summary>
		Unknown,

		/// <summary>
		/// A namespace reference.
		/// </summary>
		Namespace,

		/// <summary>
		/// A namespace reference.
		/// </summary>
		NamespaceAlias,

		/// <summary>
		/// A number.
		/// </summary>
		Number,

		/// <summary>
		/// A type reference.
		/// </summary>
		Type,

		/// <summary>
		/// A "this" object reference.
		/// </summary>
		This,

		/// <summary>
		/// A "base" object reference.
		/// </summary>
		Base,

		/// <summary>
		/// A member reference.
		/// </summary>
		Member,

		/// <summary>
		/// A variable reference.
		/// </summary>
		Variable,

		/// <summary>
		/// A constant reference.
		/// </summary>
		Constant,

		/// <summary>
		/// A parameter reference.
		/// </summary>
		Parameter,

		/// <summary>
		/// A documentation comment parent tag.
		/// </summary>
		DocumentationCommentParentTag,

		/// <summary>
		/// A string literal.
		/// </summary>
		StringLiteral,

		/// <summary>
		/// An array item.
		/// </summary>
		ArrayItem,

	}
}
 