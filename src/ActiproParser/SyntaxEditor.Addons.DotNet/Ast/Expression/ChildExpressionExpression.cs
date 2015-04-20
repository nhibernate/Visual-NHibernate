using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Provides the base class for an expression that has a single child expression.
	/// </summary>
	public abstract class ChildExpressionExpression : Expression {

		/// <summary>
		/// Gets the context ID for an expression AST node.
		/// </summary>
		/// <value>The context ID for an expression AST node.</value>
		public const byte ExpressionContextID = Expression.ExpressionContextIDBase;
		
		/// <summary>
		/// Gets the minimum context ID that should be used in your code for AST nodes inheriting this class.
		/// </summary>
		/// <value>The minimum context ID that should be used in your code for AST nodes inheriting this class.</value>
		/// <remarks>
		/// Base all your context ID constants off of this value.
		/// </remarks>
		protected const byte ChildExpressionExpressionContextIDBase = Expression.ExpressionContextIDBase + 1;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>ChildExpressionExpression</c> class. 
		/// </summary>
		/// <param name="expression">The child <see cref="Expression"/>.</param>
		public ChildExpressionExpression(Expression expression) {
			// Initialize parameters
			this.Expression = expression;
		}
		
		/// <summary>
		/// Initializes a new instance of the <c>ChildExpressionExpression</c> class. 
		/// </summary>
		/// <param name="expression">The child <see cref="Expression"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public ChildExpressionExpression(Expression expression, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.Expression = expression;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the <see cref="Expression"/> affected by the unary operator.
		/// </summary>
		/// <value>The <see cref="Expression"/> affected by the unary operator.</value>
		public Expression Expression {
			get {
				return this.GetChildNode(ChildExpressionExpression.ExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, ChildExpressionExpression.ExpressionContextID);
			}
		}

	}
}
