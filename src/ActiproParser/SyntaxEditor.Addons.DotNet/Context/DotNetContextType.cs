using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Context {
	
	/// <summary>
	/// Specifies the type of a <see cref="DotNetContext"/>.
	/// </summary>
	public enum DotNetContextType {
	
		/// <summary>
		/// There is no valid context.
		/// </summary>
		None,

		/// <summary>
		/// Any code-based context is valid.
		/// </summary>
		AnyCode,
		
		/// <summary>
		/// The context is the type specification of a variable declaration.
		/// </summary>
		AsType,

		/// <summary>
		/// The context is a "base" object reference.
		/// </summary>
		BaseAccess,
		
		/// <summary>
		/// The context is a "base" object member reference.
		/// </summary>
		BaseMemberAccess,

		/// <summary>
		/// The context is a decimal integer literal.
		/// </summary>
		DecimalIntegerLiteral,

		/// <summary>
		/// The context is a hexadecimal integer literal.
		/// </summary>
		HexadecimalIntegerLiteral,

		/// <summary>
		/// The context is in a documentation comment tag.
		/// </summary>
		DocumentationCommentTag,
		
		/// <summary>
		/// The context is an is-type-of expression for a type reference.
		/// </summary>
		IsTypeOfType,

		/// <summary>
		/// The context is a namespace, type or member reference.
		/// </summary>
		NamespaceTypeOrMember,

		/// <summary>
		/// The context is a native type reference.
		/// </summary>
		NativeType,

		/// <summary>
		/// The context is a new object declaration.
		/// </summary>
		NewObjectDeclaration,

		/// <summary>
		/// The context is a string literal.
		/// </summary>
		StringLiteral,

		/// <summary>
		/// The context is a "this" object reference.
		/// </summary>
		ThisAccess,

		/// <summary>
		/// The context is a "this" object member reference.
		/// </summary>
		ThisMemberAccess,

		/// <summary>
		/// The context is a try-cast expression to a type reference.
		/// </summary>
		TryCastType,

		/// <summary>
		/// The context is a type-of expression for a type reference.
		/// </summary>
		TypeOfType,

		/// <summary>
		/// The context is a "using" declaration.
		/// </summary>
		UsingDeclaration,

	}
}
 