using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an array redim clause.
	/// Used in Visual Basic only.
	/// </summary>
	public class ArrayRedimClause : AstNode {

		private int[]					arrayRanks;

		/// <summary>
		/// Gets the context ID for the expression AST node.
		/// </summary>
		/// <value>The context ID for the expression AST node.</value>
		public const byte ExpressionContextID = AstNode.AstNodeContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>ArrayRedimClause</c> class. 
		/// </summary>
		/// <param name="expression">The <see cref="Expression"/>.</param>
		/// <param name="arrayRanks">The array ranks.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public ArrayRedimClause(Expression expression, int[] arrayRanks, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.Expression	= expression;
			this.arrayRanks	= arrayRanks;
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
		/// Gets or sets the array dimension ranks.
		/// </summary>
		/// <value>The array dimension ranks.</value>
		/// <remarks>
		/// <c>MyClass</c> is <see langword="null"/>.
		/// <c>MyClass[]</c> is <c>{ 1 }</c>.
		/// <c>MyClass[,]</c> is <c>{ 2 }</c>.
		/// <c>MyClass[][]</c> is <c>{ 1, 1 }</c>.
		/// </remarks>
		public int[] ArrayRanks {
			get {
				return arrayRanks;
			}
			set {
				arrayRanks = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="Expression"/> that represents the expression.
		/// </summary>
		/// <value>The <see cref="Expression"/> that represents the expression.</value>
		public Expression Expression {
			get {
				return this.GetChildNode(ArrayRedimClause.ExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, ArrayRedimClause.ExpressionContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.ArrayRedimClause;
			}
		}
		
	}
}
