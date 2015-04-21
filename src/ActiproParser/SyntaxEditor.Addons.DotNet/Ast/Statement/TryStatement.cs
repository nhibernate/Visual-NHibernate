using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a try statement.
	/// </summary>
	public class TryStatement : Statement {

		/// <summary>
		/// Gets the context ID for a try block statement AST node.
		/// </summary>
		/// <value>The context ID for a try block statement AST node.</value>
		public const byte TryBlockStatementContextID = Statement.StatementContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a catch clause AST node.
		/// </summary>
		/// <value>The context ID for a catch clause AST node.</value>
		public const byte CatchClauseContextID = Statement.StatementContextIDBase + 1;

		/// <summary>
		/// Gets the context ID for a finally block statement AST node.
		/// </summary>
		/// <value>The context ID for a finally block statement AST node.</value>
		public const byte FinallyBlockStatementContextID = Statement.StatementContextIDBase + 2;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>TryStatement</c> class. 
		/// </summary>
		/// <param name="tryBlock">The try <see cref="Statement"/>.</param>
		/// <param name="finallyBlock">The finally <see cref="Statement"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the statement.</param>
		public TryStatement(Statement tryBlock, Statement finallyBlock, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.TryBlock		= tryBlock;
			this.FinallyBlock	= finallyBlock;
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
		/// Gets the collection of catch clauses.
		/// </summary>
		/// <value>The collection of catch clauses.</value>
		public IAstNodeList CatchClauses {
			get {
				return new AstNodeListWrapper(this, TryStatement.CatchClauseContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the finally <see cref="Statement"/>.
		/// </summary>
		/// <value>The finally <see cref="Statement"/>.</value>
		public Statement FinallyBlock {
			get {
				return this.GetChildNode(TryStatement.FinallyBlockStatementContextID) as Statement;
			}
			set {
				this.ChildNodes.Replace(value, TryStatement.FinallyBlockStatementContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.TryStatement;
			}
		}

		/// <summary>
		/// Gets or sets the try <see cref="Statement"/>.
		/// </summary>
		/// <value>The try <see cref="Statement"/>.</value>
		public Statement TryBlock {
			get {
				return this.GetChildNode(TryStatement.TryBlockStatementContextID) as Statement;
			}
			set {
				this.ChildNodes.Replace(value, TryStatement.TryBlockStatementContextID);
			}
		}


	}
}
