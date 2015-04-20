using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an is typeof expression.
	/// </summary>
	public class IsTypeOfExpression : ChildExpressionExpression {

		/// <summary>
		/// Gets the context ID for a type reference AST node.
		/// </summary>
		/// <value>The context ID for a type reference AST node.</value>
		public const byte TypeReferenceContextID = ChildExpressionExpression.ChildExpressionExpressionContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>IsTypeOfExpression</c> class. 
		/// </summary>
		/// <param name="expression">The <see cref="Expression"/> to examine.</param>
		/// <param name="typeReference">The <see cref="TypeReference"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the expression.</param>
		public IsTypeOfExpression(Expression expression, TypeReference typeReference, TextRange textRange) : base(expression, textRange) {
			// Initialize parameters
			this.TypeReference	= typeReference;
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
				return DotNetNodeType.IsTypeOfExpression;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="TypeReference"/>.
		/// </summary>
		/// <value>The <see cref="TypeReference"/>.</value>
		public TypeReference TypeReference {
			get {
				return this.GetChildNode(IsTypeOfExpression.TypeReferenceContextID) as TypeReference;
			}
			set {
				this.ChildNodes.Replace(value, IsTypeOfExpression.TypeReferenceContextID);
			}
		}

	}
}
