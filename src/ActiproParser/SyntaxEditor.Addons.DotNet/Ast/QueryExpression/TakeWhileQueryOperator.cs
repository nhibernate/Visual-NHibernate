using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a query expression take while operator.
	/// </summary>
	public class TakeWhileQueryOperator : AstNode {

		/// <summary>
		/// Gets the context ID for the expression AST node.
		/// </summary>
		/// <value>The context ID for the expression AST node.</value>
		public const byte ExpressionContextID = AstNode.AstNodeContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>TakeWhileQueryOperator</c> class. 
		/// </summary>
		public TakeWhileQueryOperator() {}

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
		/// Gets or sets the <see cref="Expression"/> for the take while operator.
		/// </summary>
		/// <value>The <see cref="Expression"/> for the take while operator.</value>
		public Expression Expression {
			get {
				return this.GetChildNode(TakeWhileQueryOperator.ExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, TakeWhileQueryOperator.ExpressionContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.TakeWhileQueryOperator;
			}
		}
		
	}
}
