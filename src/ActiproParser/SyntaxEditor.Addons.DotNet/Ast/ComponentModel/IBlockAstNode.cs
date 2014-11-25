using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Provides the base requirements of an <see cref="IAstNode"/> that is a block.
	/// </summary>
	public interface IBlockAstNode {
		
		/// <summary>
		/// Gets or sets the end character offset of the end block delimiter in the original source code that generated the AST node.
		/// </summary>
		/// <value>The end character offset of the end block delimiter in the original source code that generated the AST node.</value>
		/// <remarks>
		/// This value may be <c>-1</c> if there is no source code information for the end character offset.
		/// </remarks>
		int BlockEndOffset { get; set; }
		
		/// <summary>
		/// Gets or sets the start character offset of the start block delimiter in the original source code that generated the AST node.
		/// </summary>
		/// <value>The start character offset of the start block delimiter in the original source code that generated the AST node.</value>
		/// <remarks>
		/// This value may be <c>-1</c> if there is no source code information for the start character offset.
		/// </remarks>
		int BlockStartOffset { get; set; }
		
	}
}
