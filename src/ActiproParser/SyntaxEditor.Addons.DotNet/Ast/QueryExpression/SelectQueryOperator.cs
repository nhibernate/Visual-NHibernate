using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a query expression select operator.
	/// </summary>
	public class SelectQueryOperator : AstNode {

		/// <summary>
		/// Gets the context ID for the variable declarator AST node.
		/// </summary>
		/// <value>The context ID for the variable declarator AST node.</value>
		public const byte VariableDeclaratorContextID = AstNode.AstNodeContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>SelectQueryOperator</c> class. 
		/// </summary>
		public SelectQueryOperator() {}

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
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.SelectQueryOperator;
			}
		}
		
		/// <summary>
		/// Gets the collection of <see cref="VariableDeclarator"/> nodes that declare the variables or return expressions for the select operator.
		/// </summary>
		/// <value>The collection <see cref="VariableDeclarator"/> nodes that declare the variables or return expressions for the select operator.</value>
		public IAstNodeList VariableDeclarators {
			get {
				return new AstNodeListWrapper(this, SelectQueryOperator.VariableDeclaratorContextID);
			}
		}
		
	}
}
