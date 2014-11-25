using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a lock statement.
	/// </summary>
	public class LockStatement : ChildStatementStatement {

		/// <summary>
		/// Gets the context ID for an expression AST node.
		/// </summary>
		/// <value>The context ID for an expression AST node.</value>
		public const byte ExpressionContextID = ChildStatementStatement.ChildStatementStatementContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>LockStatement</c> class. 
		/// </summary>
		/// <param name="expression">The lock <see cref="Expression"/>.</param>
		/// <param name="statement">The <see cref="Statement"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the statement.</param>
		public LockStatement(Expression expression, Statement statement, TextRange textRange) : base(statement, textRange) {
			// Initialize parameters
			this.Expression = expression;
		}
		
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
		/// Gets or sets the lock <see cref="Expression"/>.
		/// </summary>
		/// <value>The lock <see cref="Expression"/>.</value>
		public Expression Expression {
			get {
				return this.GetChildNode(LockStatement.ExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, LockStatement.ExpressionContextID);
			}
		}

		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.LockStatement;
			}
		}

		

	}
}
