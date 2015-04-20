using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a goto statement.
	/// </summary>
	public class GotoStatement : BranchStatement {

		/// <summary>
		/// Gets the context ID for an identifier AST node.
		/// </summary>
		/// <value>The context ID for an identifier AST node.</value>
		public const byte IdentifierContextID = BranchStatement.BranchStatementContextIDBase;
		
		/// <summary>
		/// Gets the context ID for an expression AST node.
		/// </summary>
		/// <value>The context ID for an expression AST node.</value>
		public const byte ExpressionContextID = BranchStatement.BranchStatementContextIDBase + 1;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>GotoStatement</c> class. 
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		/// <param name="expression">The <see cref="Expression"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public GotoStatement(QualifiedIdentifier identifier, Expression expression, TextRange textRange) : base(BranchStatementType.Goto, textRange) {
			// Initialize parameters
			this.Identifier	= identifier;
			this.Expression	= expression;
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
		/// Gets or sets the <see cref="Expression"/>.
		/// </summary>
		/// <value>The <see cref="Expression"/>.</value>
		public Expression Expression {
			get {
				return this.GetChildNode(GotoStatement.ExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, GotoStatement.ExpressionContextID);
			}
		}

		/// <summary>
		/// Gets or sets the identifer.
		/// </summary>
		/// <value>The identifier.</value>
		public QualifiedIdentifier Identifier {
			get {
				return this.GetChildNode(GotoStatement.IdentifierContextID) as QualifiedIdentifier;
			}
			set {
				this.ChildNodes.Replace(value, GotoStatement.IdentifierContextID);
			}
		}

		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.GoToStatement;
			}
		}


	}
}
