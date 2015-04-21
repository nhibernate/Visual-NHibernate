using System;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Context {

	/// <summary>
	/// Provides a class that locates the containing <see cref="AstNode"/> of the specified offset.
	/// </summary>
	internal class DotNetExactContextLocator : AstVisitor {

		private AstNode	contextAstNode;
		private int		searchOffset;
	
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Returns the lowest level <see cref="AstNode"/> that contains the specified offset.
		/// </summary>
		/// <param name="compilationUnit">The <see cref="CompilationUnit"/> to examine.</param>
		/// <param name="searchOffset">The offset whose context is requested.</param>
		/// <returns>The closest type or member <see cref="AstNode"/> to the specified offset.</returns>
		internal AstNode FindContainingNode(CompilationUnit compilationUnit, int searchOffset) {
			// Initialize parameters
			this.searchOffset = searchOffset;

			// Clear the result
			contextAstNode = null;

			// Visit each node
			compilationUnit.Accept(this);

			return contextAstNode;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// GENERIC VISIT PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Visits the <see cref="IAstNode"/> before the type-specific <c>OnVisiting</c> method is executed.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node and its children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public override bool OnPreVisiting(AstNode node) {
			if (node.Contains(searchOffset)) {
				contextAstNode = node;
				return true;
			}
			return false;
		}

	}
}