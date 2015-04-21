using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a for/each statement.
	/// </summary>
	public class ForEachStatement : ChildStatementStatement {
		
		/// <summary>
		/// Gets the context ID for a variable declaration AST node.
		/// </summary>
		/// <value>The context ID for a variable declaration AST node.</value>
		public const byte VariableDeclarationContextID = ChildStatementStatement.ChildStatementStatementContextIDBase;
		
		/// <summary>
		/// Gets the context ID for an expression AST node.
		/// </summary>
		/// <value>The context ID for an expression AST node.</value>
		public const byte ExpressionContextID = ChildStatementStatement.ChildStatementStatementContextIDBase + 1;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>ForEachStatement</c> class. 
		/// </summary>
		/// <param name="variableDeclaration">The variable declaration.</param>
		/// <param name="expression">The iteration <see cref="Expression"/>.</param>
		/// <param name="statement">The <see cref="Statement"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public ForEachStatement(IAstNode variableDeclaration, Expression expression, Statement statement, TextRange textRange) : base(statement, textRange) {
			// Initialize parameters
			this.VariableDeclaration	= variableDeclaration;
			this.Expression				= expression;
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
		/// Gets or sets the iteration <see cref="Expression"/>.
		/// </summary>
		/// <value>The iteration <see cref="Expression"/>.</value>
		public Expression Expression {
			get {
				return this.GetChildNode(ForEachStatement.ExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, ForEachStatement.ExpressionContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.ForEachStatement;
			}
		}

		/// <summary>
		/// Gets or sets the variable declaration.
		/// </summary>
		/// <value>The variable declaration.</value>
		public IAstNode VariableDeclaration {
			get {
				return this.GetChildNode(ForEachStatement.VariableDeclarationContextID) as LocalVariableDeclaration;
			}
			set {
				this.ChildNodes.Replace(value, ForEachStatement.VariableDeclarationContextID);
			}
		}
		
	}
}
