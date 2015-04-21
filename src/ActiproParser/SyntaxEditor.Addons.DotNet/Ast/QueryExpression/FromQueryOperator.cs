using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a query expression from operator.
	/// </summary>
	public class FromQueryOperator : AstNode {

		/// <summary>
		/// Gets the context ID for a collection range variable declaration AST node.
		/// </summary>
		/// <value>The context ID for a collection range variable declaration AST node.</value>
		public const byte CollectionRangeVariableDeclarationContextID = AstNode.AstNodeContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>FromQueryOperator</c> class. 
		/// </summary>
		public FromQueryOperator() {}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Accepts the specified visitor for visiting this node.
		/// </summary>
		/// <param name="visitor">The visitor to accept.</param>
		/// <remarks>This method is part of the visitor design pattern implementation.</remarks>
		protected override void AcceptCore(AstVisitor visitor) {
			if (visitor.OnVisiting(this)) {
				// Visit children
				if (this.ChildNodeCount > 0)
					this.AcceptChildren(visitor, this.ChildNodes);
			}
			visitor.OnVisited(this);
		}
	
		/// <summary>
		/// Gets the collection of collection range variable declarations.
		/// </summary>
		/// <value>The collection of collection range variable declarations.</value>
		public IAstNodeList CollectionRangeVariableDeclarations {
			get {
				return new AstNodeListWrapper(this, FromQueryOperator.CollectionRangeVariableDeclarationContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.FromQueryOperator;
			}
		}
		
	}
}
