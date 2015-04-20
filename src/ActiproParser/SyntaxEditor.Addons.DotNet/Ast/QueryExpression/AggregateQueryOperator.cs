using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a query expression aggregate operator.
	/// </summary>
	public class AggregateQueryOperator : AstNode {

		/// <summary>
		/// Gets the context ID for a collection range variable declaration AST node.
		/// </summary>
		/// <value>The context ID for a collection range variable declaration AST node.</value>
		public const byte CollectionRangeVariableDeclarationContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a query operator AST node.
		/// </summary>
		/// <value>The context ID for a query operator AST node.</value>
		public const byte QueryOperatorContextID = AstNode.AstNodeContextIDBase + 1;
		
		/// <summary>
		/// Gets the context ID for a target expression AST node.
		/// </summary>
		/// <value>The context ID for a target expression AST node.</value>
		public const byte TargetExpressionContextID = AstNode.AstNodeContextIDBase + 2;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>AggregateQueryOperator</c> class. 
		/// </summary>
		public AggregateQueryOperator() {}

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
				return new AstNodeListWrapper(this, AggregateQueryOperator.CollectionRangeVariableDeclarationContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.AggregateQueryOperator;
			}
		}
		
		/// <summary>
		/// Gets the collection of query operators.
		/// </summary>
		/// <value>The collection of query operators.</value>
		public IAstNodeList QueryOperators {
			get {
				return new AstNodeListWrapper(this, AggregateQueryOperator.QueryOperatorContextID);
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
