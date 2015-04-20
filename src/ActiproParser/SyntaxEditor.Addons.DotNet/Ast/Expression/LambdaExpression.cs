using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a lambda expression.
	/// </summary>
	public class LambdaExpression : Expression {

		/// <summary>
		/// Gets the context ID for a parameter AST node.
		/// </summary>
		/// <value>The context ID for a parameter AST node.</value>
		public const byte ParameterContextID = Expression.ExpressionContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a statement AST node.
		/// </summary>
		/// <value>The context ID for a statement AST node.</value>
		public const byte StatementContextID = Expression.ExpressionContextIDBase + 1;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>LambdaExpression</c> class. 
		/// </summary>
		public LambdaExpression() {}
		
		/// <summary>
		/// Initializes a new instance of the <c>LambdaExpression</c> class. 
		/// </summary>
		/// <param name="statement">The child <see cref="Statement"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public LambdaExpression(Statement statement, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.Statement = statement;
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
		/// Gets the collection of parameters.
		/// </summary>
		/// <value>The collection of parameters.</value>
		public IAstNodeList Parameters {
			get {
				return new AstNodeListWrapper(this, LambdaExpression.ParameterContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.LambdaExpression;
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="Statement"/> contained by the lambda expression.
		/// </summary>
		/// <value>The <see cref="Statement"/> contained by the lambda expression.</value>
		public Statement Statement {
			get {
				return this.GetChildNode(LambdaExpression.StatementContextID) as Statement;
			}
			set {
				this.ChildNodes.Replace(value, LambdaExpression.StatementContextID);
			}
		}

	}
}
