using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a catch clause.
	/// </summary>
	public class CatchClause : AstNode {

		/// <summary>
		/// Gets the context ID for a variable declarator AST node.
		/// </summary>
		/// <value>The context ID for a variable declarator AST node.</value>
		public const byte VariableDeclaratorContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a block statement name AST node.
		/// </summary>
		/// <value>The context ID for a block statement AST node.</value>
		public const byte BlockStatementContextID = AstNode.AstNodeContextIDBase + 2;
		
		/// <summary>
		/// Gets the context ID for an evaluation expression name AST node.
		/// </summary>
		/// <value>The context ID for an evaluation expression AST node.</value>
		public const byte EvaluationExpressionContextID = AstNode.AstNodeContextIDBase + 3;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>CatchClause</c> class. 
		/// </summary>
		/// <param name="variableDeclarator">The variable declarator.</param>
		/// <param name="blockStatement">The catch <see cref="BlockStatement"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public CatchClause(VariableDeclarator variableDeclarator, BlockStatement blockStatement, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.VariableDeclarator	= variableDeclarator;
			this.BlockStatement	= blockStatement;
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
		/// Gets or sets the <see cref="BlockStatement"/>.
		/// </summary>
		/// <value>The <see cref="BlockStatement"/>.</value>
		public BlockStatement BlockStatement {
			get {
				return this.GetChildNode(CatchClause.BlockStatementContextID) as BlockStatement;
			}
			set {
				this.ChildNodes.Replace(value, CatchClause.BlockStatementContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the evaluation <see cref="Expression"/>.
		/// </summary>
		/// <value>The evaluation <see cref="Expression"/>.</value>
		/// <remarks>
		/// Used in Visual Basic only.
		/// </remarks>
		public Expression EvaluationExpression {
			get {
				return this.GetChildNode(CatchClause.EvaluationExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, CatchClause.EvaluationExpressionContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.CatchClause;
			}
		}

		/// <summary>
		/// Gets or sets the variable declarator.
		/// </summary>
		/// <value>The variable declarator.</value>
		public VariableDeclarator VariableDeclarator {  
			get {
				return this.GetChildNode(CatchClause.VariableDeclaratorContextID) as VariableDeclarator;
			}
			set {
				this.ChildNodes.Replace(value, CatchClause.VariableDeclaratorContextID);
			}
		}


	}
}
