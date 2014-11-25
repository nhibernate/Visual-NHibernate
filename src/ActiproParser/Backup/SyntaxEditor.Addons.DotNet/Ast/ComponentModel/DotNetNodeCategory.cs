using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Specifies the category of an <see cref="AstNode"/>.
	/// </summary>
	public enum DotNetNodeCategory {

		/// <summary>
		/// Other type of AST node.
		/// </summary>
		Other = 0,

		/// <summary>
		/// Compilation unit.
		/// </summary>
		CompilationUnit,

		/// <summary>
		/// Namespace declaration.
		/// </summary>
		NamespaceDeclaration,

		/// <summary>
		/// Type declaration (includes delegates).
		/// </summary>
		TypeDeclaration,

		/// <summary>
		/// Type member declaration.
		/// </summary>
		TypeMemberDeclaration,

		/// <summary>
		/// Type member declaration section (such as a <see cref="FieldDeclaration"/> or <see cref="FixedSizeBufferDeclaration"/>).
		/// </summary>
		TypeMemberDeclarationSection,

		/// <summary>
		/// Statement.
		/// </summary>
		Statement,

		/// <summary>
		/// Expression.
		/// </summary>
		Expression,

	}
}
