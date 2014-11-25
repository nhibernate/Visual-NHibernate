using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an invocation expression.
	/// </summary>
	public class InvocationExpression : ChildExpressionExpression {

		private bool isIndexerInvocation;

		/// <summary>
		/// Gets the context ID for an argument AST node.
		/// </summary>
		/// <value>The context ID for an argument AST node.</value>
		public const byte ArgumentContextID = ChildExpressionExpression.ChildExpressionExpressionContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>InvocationExpression</c> class. 
		/// </summary>
		/// <param name="target">The target <see cref="Expression"/>.</param>
		public InvocationExpression(Expression target) : base(target) {}
		
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
		/// Gets or sets whether the invocation is for an indexer (if known).
		/// </summary>
		/// <value>
		/// <c>true</c> if the invocation is for an indexer (if known); otherwise, <c>false</c>.
		/// </value>
		public bool IsIndexerInvocation {
			get {
				return isIndexerInvocation;
			}
			set {
				isIndexerInvocation = value;
			}
		}
		
		/// <summary>
		/// Gets the collection of argument expressions.
		/// </summary>
		/// <value>The collection of argument expressions.</value>
		public IAstNodeList Arguments {
			get {
				return new AstNodeListWrapper(this, InvocationExpression.ArgumentContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.InvocationExpression;
			}
		}

	}
}
