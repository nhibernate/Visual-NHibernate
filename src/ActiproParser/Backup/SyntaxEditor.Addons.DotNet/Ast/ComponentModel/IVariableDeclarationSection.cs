using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Provides the base requirements of an <see cref="IAstNode"/> that is a member declaration section.
	/// </summary>
	public interface IVariableDeclarationSection : IAstNode {
		
		/// <summary>
		/// Gets the collection of variables that are declared.
		/// </summary>
		/// <value>The collection of variables that are declared.</value>
		IAstNodeList Variables { get; }

	}
}
