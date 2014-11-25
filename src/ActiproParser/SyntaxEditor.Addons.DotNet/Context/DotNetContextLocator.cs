using System;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Context {

	/// <summary>
	/// Provides a class that locates the closest type or member <see cref="AstNode"/> to the specified offset.
	/// </summary>
	internal class DotNetContextLocator : AstVisitor {

		private AstNode	contextAstNode;
		private int		searchOffset;
	
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Returns the closest type or member <see cref="AstNode"/> to the specified offset.
		/// </summary>
		/// <param name="compilationUnit">The <see cref="CompilationUnit"/> to examine.</param>
		/// <param name="searchOffset">The offset whose context is requested.</param>
		/// <returns>The closest type or member <see cref="AstNode"/> to the specified offset.</returns>
		internal AstNode FindClosestTypeOrMember(CompilationUnit compilationUnit, int searchOffset) {
			// Initialize parameters
			this.searchOffset = searchOffset;

			// Clear the result
			contextAstNode = null;

			// Visit each node
			compilationUnit.Accept(this);

			// If no context node was found and there is at least one type...
			if ((contextAstNode == null) && (compilationUnit.Types.Count > 0)) {
				// Look for the first type that the search offset is before
				for (int index = 0; index < compilationUnit.Types.Count; index++) {
					if ((compilationUnit.Types[index].HasStartOffset) && (searchOffset < compilationUnit.Types[index].StartOffset)) {
						contextAstNode = compilationUnit.Types[index] as AstNode;
						break;
					}
				}

				// If there still is no type found, return the last one
				if (contextAstNode == null)
					contextAstNode = compilationUnit.Types[compilationUnit.Types.Count - 1] as AstNode;
			}

			// If a type declaration was chosen, try and narrow down to the closest member
			if ((contextAstNode != null) && (contextAstNode is TypeDeclaration) && (((TypeDeclaration)contextAstNode).Members.Count > 0)) {
				// Get the type declaration
				TypeDeclaration typeDeclaration = (TypeDeclaration)contextAstNode;

				// Loop through the types
				foreach (AstNode member in typeDeclaration.Members) {
					// Quit if the member is a delegate declaration
					if ((member is DelegateDeclaration) && (member.HasEndOffset) && (searchOffset < member.EndOffset))
						break;

					// If the member is defined in code, use it as the current context
					if (member.HasStartOffset)
						contextAstNode = member;

					if (member.EndOffset > searchOffset)
						break;
				}
			}

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
			switch (node.NodeCategory) {
				case DotNetNodeCategory.TypeDeclaration:
				case DotNetNodeCategory.TypeMemberDeclaration:
					if (node.IntersectsWith(searchOffset))
						contextAstNode = node;
					break;
				case DotNetNodeCategory.TypeMemberDeclarationSection:
					if ((node.IntersectsWith(searchOffset)) && (node is IVariableDeclarationSection) && (((IVariableDeclarationSection)node).Variables.Count > 0))
						contextAstNode = ((IVariableDeclarationSection)node).Variables[0] as AstNode;
					break;
			}
			return true;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the level through which the <see cref="AstVisitor"/> should visit children, by default.
		/// </summary>
		/// <value>An <see cref="AstVisitorLevel"/> indicating the level through which the <see cref="AstVisitor"/> should visit children.</value>
		protected override AstVisitorLevel Level {
			get {
				return AstVisitorLevel.TypeMembers;
			}
		}

	}
}