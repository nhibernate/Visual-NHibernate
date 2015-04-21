using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an if statement.
	/// </summary>
	public class IfStatement : Statement {
		
		/// <summary>
		/// Gets the context ID for a condition AST node.
		/// </summary>
		/// <value>The context ID for a condition AST node.</value>
		public const byte ConditionContextID = Statement.StatementContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a true statement AST node.
		/// </summary>
		/// <value>The context ID for a true statement AST node.</value>
		public const byte TrueStatementContextID = Statement.StatementContextIDBase + 1;
		
		/// <summary>
		/// Gets the context ID for a false statement name AST node.
		/// </summary>
		/// <value>The context ID for a false statement AST node.</value>
		public const byte FalseStatementContextID = Statement.StatementContextIDBase + 2;
		
		/// <summary>
		/// Gets the context ID for an else-if statement section AST node.
		/// </summary>
		/// <value>The context ID for an else-if statement section AST node.</value>
		public const byte ElseIfSectionContextID = Statement.StatementContextIDBase + 3;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>IfStatement</c> class. 
		/// </summary>
		/// <param name="condition">The condition <see cref="Expression"/>.</param>
		/// <param name="trueStatement">The true <see cref="Statement"/>.</param>
		public IfStatement(Expression condition, Statement trueStatement) {
			// Initialize parameters
			this.Condition		= condition;
			this.TrueStatement	= trueStatement;
		}
		
		/// <summary>
		/// Initializes a new instance of the <c>IfStatement</c> class. 
		/// </summary>
		/// <param name="condition">The condition <see cref="Expression"/>.</param>
		/// <param name="trueStatement">The true <see cref="Statement"/>.</param>
		/// <param name="falseStatement">The false <see cref="Statement"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public IfStatement(Expression condition, Statement trueStatement, Statement falseStatement, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.Condition		= condition;
			this.TrueStatement	= trueStatement;
			this.FalseStatement	= falseStatement;
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
		/// Gets or sets the condition <see cref="Expression"/>.
		/// </summary>
		/// <value>The condition <see cref="Expression"/>.</value>
		public Expression Condition {
			get {
				return this.GetChildNode(IfStatement.ConditionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, IfStatement.ConditionContextID);
			}
		}
		
		/// <summary>
		/// Gets the collection of else-if statement sections.
		/// Used in Visual Basic only.
		/// </summary>
		/// <value>The collection of else-if statement sections.</value>
		public IAstNodeList ElseIfSections {
			get {
				return new AstNodeListWrapper(this, IfStatement.ElseIfSectionContextID);
			}
		}

		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.IfStatement;
			}
		}

		/// <summary>
		/// Gets or sets the false <see cref="Statement"/>.
		/// </summary>
		/// <value>The false <see cref="Statement"/>.</value>
		public Statement FalseStatement {
			get {
				return this.GetChildNode(IfStatement.FalseStatementContextID) as Statement;
			}
			set {
				this.ChildNodes.Replace(value, IfStatement.FalseStatementContextID);
			}
		}

		/// <summary>
		/// Gets or sets the true <see cref="Statement"/>.
		/// </summary>
		/// <value>The true <see cref="Statement"/>.</value>
		public Statement TrueStatement {
			get {
				return this.GetChildNode(IfStatement.TrueStatementContextID) as Statement;
			}
			set {
				this.ChildNodes.Replace(value, IfStatement.TrueStatementContextID);
			}
		}


	}
}
