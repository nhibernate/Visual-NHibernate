using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a object or collection initializer expression.
	/// </summary>
	public class ObjectCollectionInitializerExpression : Expression {

		/// <summary>
		/// Gets the context ID for an initializer AST node.
		/// </summary>
		/// <value>The context ID for an initializer AST node.</value>
		public const byte InitializerContextID = Expression.ExpressionContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>ObjectCollectionInitializerExpression</c> class. 
		/// </summary>
		public ObjectCollectionInitializerExpression() {}
		
		/// <summary>
		/// Initializes a new instance of the <c>ObjectCollectionInitializerExpression</c> class. 
		/// </summary>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public ObjectCollectionInitializerExpression(TextRange textRange) : base(textRange) {}
		
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
		/// Gets the collection of initializers.
		/// </summary>
		/// <value>The collection of initializers.</value>
		public IAstNodeList Initializers {
			get {
				return new AstNodeListWrapper(this, ObjectCollectionInitializerExpression.InitializerContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.ObjectCollectionInitializerExpression;
			}
		}
		
	}
}
