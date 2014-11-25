using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a query expression collection range variable.
	/// </summary>
	public class JoinCondition : AstNode {

		/// <summary>
		/// Gets the context ID for the left condition expression AST node.
		/// </summary>
		/// <value>The context ID for the left condition expression AST node.</value>
		public const byte LeftConditionExpressionContextID = AstNode.AstNodeContextIDBase;

		/// <summary>
		/// Gets the context ID for the right condition expression AST node.
		/// </summary>
		/// <value>The context ID for the right condition expression AST node.</value>
		public const byte RightConditionExpressionContextID = AstNode.AstNodeContextIDBase + 1;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>JoinCondition</c> class. 
		/// </summary>
		public JoinCondition() {}

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
		/// Gets or sets the left condition <see cref="Expression"/>.
		/// </summary>
		/// <value>The left condition <see cref="Expression"/>.</value>
		public Expression LeftConditionExpression {
			get {
				return this.GetChildNode(JoinCondition.LeftConditionExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, JoinCondition.LeftConditionExpressionContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.JoinCondition;
			}
		}
		
		/// <summary>
		/// Gets or sets the right condition <see cref="Expression"/>.
		/// </summary>
		/// <value>The right condition <see cref="Expression"/>.</value>
		public Expression RightConditionExpression {
			get {
				return this.GetChildNode(JoinCondition.RightConditionExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, JoinCondition.RightConditionExpressionContextID);
			}
		}
		
	}
}
