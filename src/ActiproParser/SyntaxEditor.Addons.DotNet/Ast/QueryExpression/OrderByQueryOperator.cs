using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a query expression order-by operator.
	/// </summary>
	public class OrderByQueryOperator : AstNode {

		/// <summary>
		/// Gets the context ID for an ordering AST node.
		/// </summary>
		/// <value>The context ID for an ordering AST node.</value>
		public const byte OrderingContextID = AstNode.AstNodeContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>OrderByQueryOperator</c> class. 
		/// </summary>
		public OrderByQueryOperator() {}

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
		/// Gets the collection of orderings.
		/// </summary>
		/// <value>The collection of orderings.</value>
		public IAstNodeList Orderings {
			get {
				return new AstNodeListWrapper(this, OrderByQueryOperator.OrderingContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.OrderByQueryOperator;
			}
		}
	
	}
}
