using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a conditional expression.
	/// </summary>
	public class ConditionalExpression : Expression {
		
		/// <summary>
		/// Gets the context ID for a test expression AST node.
		/// </summary>
		/// <value>The context ID for a test expression AST node.</value>
		public const byte TestExpressionContextID = Expression.ExpressionContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a true expression AST node.
		/// </summary>
		/// <value>The context ID for a true expression AST node.</value>
		public const byte TrueStatementContextID = Expression.ExpressionContextIDBase + 1;
		
		/// <summary>
		/// Gets the context ID for a false expression AST node.
		/// </summary>
		/// <value>The context ID for a false expression AST node.</value>
		public const byte FalseStatementContextID = Expression.ExpressionContextIDBase + 2;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>ConditionalExpression</c> class. 
		/// </summary>
		/// <param name="testExpression">The test <see cref="Expression"/>.</param>
		/// <param name="trueExpression">The <see cref="Expression"/> returned for a true test.</param>
		/// <param name="falseExpression">The <see cref="Expression"/> returned for a false test.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public ConditionalExpression(Expression testExpression, Expression trueExpression, Expression falseExpression, TextRange textRange) {
			// Initialize parameters
			this.TestExpression		= testExpression;
			this.TrueExpression		= trueExpression;
			this.FalseExpression	= falseExpression;
			this.StartOffset		= textRange.StartOffset;
			this.EndOffset			= textRange.EndOffset;
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
		/// Gets or sets the <see cref="Expression"/> returned for a false test.
		/// </summary>
		/// <value>The <see cref="Expression"/> returned for a false test.</value>
		public Expression FalseExpression {
			get {
				return this.GetChildNode(ConditionalExpression.FalseStatementContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, ConditionalExpression.FalseStatementContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.ConditionalExpression;
			}
		}

		/// <summary>
		/// Gets or sets the test <see cref="Expression"/>.
		/// </summary>
		/// <value>The test <see cref="Expression"/>.</value>
		public Expression TestExpression {
			get {
				return this.GetChildNode(ConditionalExpression.TestExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, ConditionalExpression.TestExpressionContextID);
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Expression"/> returned for a true test.
		/// </summary>
		/// <value>The <see cref="Expression"/> returned for a true test.</value>
		public Expression TrueExpression {
			get {
				return this.GetChildNode(ConditionalExpression.TrueStatementContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, ConditionalExpression.TrueStatementContextID);
			}
		}
	}
}
