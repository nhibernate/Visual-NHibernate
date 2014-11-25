using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a query expression.
	/// </summary>
	public class QueryExpression : Expression {

		/// <summary>
		/// Gets the context ID for a query operator AST node.
		/// </summary>
		/// <value>The context ID for a query operator AST node.</value>
		public const byte QueryOperatorContextID = Expression.ExpressionContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>QueryExpression</c> class. 
		/// </summary>
		public QueryExpression() {}
		
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
				return DotNetNodeType.QueryExpression;
			}
		}
		
		/// <summary>
		/// Gets the collection of query operators.
		/// </summary>
		/// <value>The collection of query operators.</value>
		public IAstNodeList QueryOperators {
			get {
				return new AstNodeListWrapper(this, QueryExpression.QueryOperatorContextID);
			}
		}
		
	}
}
