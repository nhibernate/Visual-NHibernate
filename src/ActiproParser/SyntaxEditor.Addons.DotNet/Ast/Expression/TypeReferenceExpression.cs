using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a type reference expression.
	/// </summary>
	public class TypeReferenceExpression : Expression {

		/// <summary>
		/// Gets the context ID for a type reference AST node.
		/// </summary>
		/// <value>The context ID for a type reference AST node.</value>
		public const byte TypeReferenceContextID = Expression.ExpressionContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>TypeReferenceExpression</c> class. 
		/// </summary>
		/// <param name="typeReference">The <see cref="TypeReference"/>.</param>
		public TypeReferenceExpression(TypeReference typeReference) : base(typeReference.TextRange) {
			// Initialize parameters
			this.TypeReference = typeReference;
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
				return DotNetNodeType.TypeReferenceExpression;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="TypeReference"/>.
		/// </summary>
		/// <value>The <see cref="TypeReference"/>.</value>
		public TypeReference TypeReference {
			get {
				return this.GetChildNode(TypeReferenceExpression.TypeReferenceContextID) as TypeReference;
			}
			set {
				this.ChildNodes.Replace(value, TypeReferenceExpression.TypeReferenceContextID);
			}
		}

	}
}
