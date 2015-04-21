using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a query expression where operator.
	/// </summary>
	public class WhereQueryOperator : AstNode {

		/// <summary>
		/// Gets the context ID for the condition AST node.
		/// </summary>
		/// <value>The context ID for the condition AST node.</value>
		public const byte ConditionContextID = AstNode.AstNodeContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>WhereQueryOperator</c> class. 
		/// </summary>
		public WhereQueryOperator() {}

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
		/// Gets or sets the condition <see cref="Expression"/> for the where operator.
		/// </summary>
		/// <value>The condition <see cref="Expression"/> for the where operator.</value>
		public Expression Condition {
			get {
				return this.GetChildNode(WhereQueryOperator.ConditionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, WhereQueryOperator.ConditionContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.WhereQueryOperator;
			}
		}
		
	}
}
