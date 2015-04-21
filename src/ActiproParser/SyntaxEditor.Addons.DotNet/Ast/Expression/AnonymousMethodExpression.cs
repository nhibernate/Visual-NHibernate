using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an anonymous method expression.
	/// </summary>
	public class AnonymousMethodExpression : Expression {

		/// <summary>
		/// Gets the context ID for an expression AST node.
		/// </summary>
		/// <value>The context ID for an expression AST node.</value>
		public const byte GenericTypeParameterContextID = Expression.ExpressionContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a block statement AST node.
		/// </summary>
		/// <value>The context ID for a block statement AST node.</value>
		public const byte BlockStatementContextID = Expression.ExpressionContextIDBase + 1;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>AnonymousMethodExpression</c> class. 
		/// </summary>
		/// <param name="blockStatement">The <see cref="BlockStatement"/> wrapped by the anonymous method.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public AnonymousMethodExpression(BlockStatement blockStatement, TextRange textRange) : base(textRange) {
			// Initialize parameters
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
		/// Gets or sets the <see cref="BlockStatement"/> wrapped by the anonymous method.
		/// </summary>
		/// <value>The <see cref="BlockStatement"/> wrapped by the anonymous method.</value>
		public BlockStatement BlockStatement {
			get {
				return this.GetChildNode(AnonymousMethodExpression.BlockStatementContextID) as BlockStatement;
			}
			set {
				this.ChildNodes.Replace(value, AnonymousMethodExpression.BlockStatementContextID);
			}
		}
		
		/// <summary>
		/// Gets the collection of generic type parameters.
		/// </summary>
		/// <value>The collection of generic type parameters.</value>
		public IAstNodeList GenericTypeParameters {
			get {
				return new AstNodeListWrapper(this, AnonymousMethodExpression.GenericTypeParameterContextID);
			}
		}

		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.AnonymousMethodExpression;
			}
		}

	}
}
