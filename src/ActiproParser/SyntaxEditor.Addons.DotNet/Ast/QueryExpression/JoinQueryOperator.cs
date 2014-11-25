using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a query expression join operator.
	/// </summary>
	public class JoinQueryOperator : AstNode {

		/// <summary>
		/// Gets the context ID for the collection range variable declaration AST node.
		/// </summary>
		/// <value>The context ID for the collection range variable declaration AST node.</value>
		public const byte CollectionRangeVariableDeclarationContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the context ID for the condition AST node.
		/// </summary>
		/// <value>The context ID for the condition AST node.</value>
		public const byte ConditionContextID = AstNode.AstNodeContextIDBase + 1;
		
		/// <summary>
		/// Gets the context ID for a child join AST node.
		/// </summary>
		/// <value>The context ID for a child join AST node.</value>
		public const byte ChildJoinContextID = AstNode.AstNodeContextIDBase + 2;

		/// <summary>
		/// Gets the context ID for a target expression AST node.
		/// </summary>
		/// <value>The context ID for a target expression AST node.</value>
		public const byte TargetExpressionContextID = AstNode.AstNodeContextIDBase + 3;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>JoinQueryOperator</c> class. 
		/// </summary>
		public JoinQueryOperator() {}

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
		/// Gets or sets the <see cref="JoinQueryOperator"/> that represents a child join for the join operator.
		/// </summary>
		/// <value>The <see cref="JoinQueryOperator"/> that represents a child join for the join operator.</value>
		public JoinQueryOperator ChildJoin {
			get {
				return this.GetChildNode(JoinQueryOperator.ChildJoinContextID) as JoinQueryOperator;
			}
			set {
				this.ChildNodes.Replace(value, JoinQueryOperator.ChildJoinContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="CollectionRangeVariableDeclaration"/> that declares the collection range variable declaration for the join operator.
		/// </summary>
		/// <value>The <see cref="CollectionRangeVariableDeclaration"/> that declares the collection range variable declaration for the join operator.</value>
		public CollectionRangeVariableDeclaration CollectionRangeVariableDeclaration {
			get {
				return this.GetChildNode(JoinQueryOperator.CollectionRangeVariableDeclarationContextID) as CollectionRangeVariableDeclaration;
			}
			set {
				this.ChildNodes.Replace(value, JoinQueryOperator.CollectionRangeVariableDeclarationContextID);
			}
		}
		
		/// <summary>
		/// Gets the collection of <see cref="JoinCondition"/> nodes for the join operator.
		/// </summary>
		/// <value>The collection of <see cref="JoinCondition"/> nodes for the join operator.</value>
		public IAstNodeList Conditions {
			get {
				return new AstNodeListWrapper(this, JoinQueryOperator.ConditionContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.JoinQueryOperator;
			}
		}
		
		/// <summary>
		/// Gets the collection of target expression nodes for the join operator.
		/// </summary>
		/// <value>The collection of target expression nodes for the join operator.</value>
		public IAstNodeList TargetExpressions {
			get {
				return new AstNodeListWrapper(this, JoinQueryOperator.TargetExpressionContextID);
			}
		}
		
	}
}
