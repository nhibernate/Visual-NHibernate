using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a unary expression.
	/// </summary>
	public class UnaryExpression : ChildExpressionExpression {

		private OperatorType	operatorType;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>UnaryExpression</c> class. 
		/// </summary>
		/// <param name="operatorType">An <see cref="OperatorType"/> indicating the unary operator type.</param>
		/// <param name="expression">The <see cref="Expression"/> affected by the unary operator.</param>
		public UnaryExpression(OperatorType operatorType, Expression expression) : base(expression) {
			// Initialize parameters
			this.operatorType = operatorType;
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
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.UnaryExpression;
			}
		}

		/// <summary>
		/// Gets or sets an <see cref="OperatorType"/> indicating the unary operator type.
		/// </summary>
		/// <value>An <see cref="OperatorType"/> indicating the unary operator type.</value>
		public OperatorType OperatorType {
			get {
				return operatorType;
			}
			set {
				operatorType = value;
			}
		}


	}
}
