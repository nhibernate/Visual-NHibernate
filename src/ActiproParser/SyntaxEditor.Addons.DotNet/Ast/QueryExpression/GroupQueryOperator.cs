using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a query expression group operator.
	/// </summary>
	public class GroupQueryOperator : AstNode {

		/// <summary>
		/// Gets the context ID for a grouping expression AST node.
		/// </summary>
		/// <value>The context ID for a grouping expression AST node.</value>
		public const byte GroupingExpressionContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a group by expression AST node.
		/// </summary>
		/// <value>The context ID for a group by expression AST node.</value>
		public const byte GroupByExpressionContextID = AstNode.AstNodeContextIDBase + 1;
		
		/// <summary>
		/// Gets the context ID for a target expression AST node.
		/// </summary>
		/// <value>The context ID for a target expression AST node.</value>
		public const byte TargetExpressionContextID = AstNode.AstNodeContextIDBase + 2;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>GroupQueryOperator</c> class. 
		/// </summary>
		public GroupQueryOperator() {}

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
		/// Gets the collection of <see cref="VariableDeclarator"/> nodes that specify by what to group.
		/// </summary>
		/// <value>The collection <see cref="VariableDeclarator"/> nodes that specify by what to group.</value>
		public IAstNodeList GroupBys {
			get {
				return new AstNodeListWrapper(this, GroupQueryOperator.GroupByExpressionContextID);
			}
		}
		
		/// <summary>
		/// Gets the collection of <see cref="VariableDeclarator"/> nodes that specify the data being grouped.
		/// </summary>
		/// <value>The collection <see cref="VariableDeclarator"/> nodes that specify the data being grouped.</value>
		public IAstNodeList Groupings {
			get {
				return new AstNodeListWrapper(this, GroupQueryOperator.GroupingExpressionContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.GroupQueryOperator;
			}
		}
		
		/// <summary>
		/// Gets the collection of <see cref="VariableDeclarator"/> nodes that specify the into portion of the group query operator.
		/// </summary>
		/// <value>The collection <see cref="VariableDeclarator"/> nodes that specify the into portion of the group query operator.</value>
		public IAstNodeList TargetExpressions {
			get {
				return new AstNodeListWrapper(this, GroupQueryOperator.TargetExpressionContextID);
			}
		}
		
	}
}
