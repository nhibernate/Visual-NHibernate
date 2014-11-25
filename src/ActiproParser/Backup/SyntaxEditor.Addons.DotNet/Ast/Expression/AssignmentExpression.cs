using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an assignment expression.
	/// </summary>
	public class AssignmentExpression : Expression {

		private OperatorType	operatorType;
		
		/// <summary>
		/// Gets the context ID for a left expression AST node.
		/// </summary>
		/// <value>The context ID for a left expression AST node.</value>
		public const byte LeftExpressionContextID = Expression.ExpressionContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a right expression AST node.
		/// </summary>
		/// <value>The context ID for a right expression AST node.</value>
		public const byte RightExpressionContextID = Expression.ExpressionContextIDBase + 1;
		
		/// <summary>
		/// Gets the context ID for a start index expression AST node.
		/// Used in Visual Basic only.
		/// </summary>
		/// <value>The context ID for a start index expression AST node.</value>
		public const byte StartIndexExpressionContextID = Expression.ExpressionContextIDBase + 2;
		
		/// <summary>
		/// Gets the context ID for a length expression AST node.
		/// Used in Visual Basic only.
		/// </summary>
		/// <value>The context ID for a length expression AST node.</value>
		public const byte LengthExpressionContextID = Expression.ExpressionContextIDBase + 3;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>AssignmentExpression</c> class. 
		/// </summary>
		/// <param name="operatorType">An <see cref="OperatorType"/> indicating the binary operator type to apply, if any.</param>
		/// <param name="leftExpression">The left <see cref="Expression"/> affected by the unary operator.</param>
		/// <param name="rightExpression">The right <see cref="Expression"/> affected by the unary operator.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public AssignmentExpression(OperatorType operatorType, Expression leftExpression, Expression rightExpression, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.operatorType		= operatorType;
			this.LeftExpression		= leftExpression;
			this.RightExpression	= rightExpression;
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
		/// Gets or sets the left <see cref="Expression"/> affected by the binary operator.
		/// </summary>
		/// <value>The left <see cref="Expression"/> affected by the binary operator.</value>
		public Expression LeftExpression {
			get {
				return this.GetChildNode(AssignmentExpression.LeftExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, AssignmentExpression.LeftExpressionContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the length <see cref="Expression"/> for the target.
		/// Used in Visual Basic only.
		/// </summary>
		/// <value>The length <see cref="Expression"/> for the target.</value>
		public Expression LengthExpression {
			get {
				return this.GetChildNode(AssignmentExpression.LengthExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, AssignmentExpression.LengthExpressionContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.AssignmentExpression;
			}
		}

		/// <summary>
		/// Gets or sets an <see cref="OperatorType"/> indicating the binary operator type to apply, if any.
		/// </summary>
		/// <value>An <see cref="OperatorType"/> indicating the binary operator type to apply, if any.</value>
		public OperatorType OperatorType {
			get {
				return operatorType;
			}
			set {
				operatorType = value;
			}
		}

		/// <summary>
		/// Gets or sets the right <see cref="Expression"/> affected by the binary operator.
		/// </summary>
		/// <value>The right <see cref="Expression"/> affected by the binary operator.</value>
		public Expression RightExpression {
			get {
				return this.GetChildNode(AssignmentExpression.RightExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, AssignmentExpression.RightExpressionContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the start index <see cref="Expression"/> for the target.
		/// Used in Visual Basic only.
		/// </summary>
		/// <value>The start index <see cref="Expression"/> for the target.</value>
		public Expression StartIndexExpression {
			get {
				return this.GetChildNode(AssignmentExpression.StartIndexExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, AssignmentExpression.StartIndexExpressionContextID);
			}
		}
		

	}
}
