using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an object or array creation expression.
	/// </summary>
	public class ObjectCreationExpression : Expression {
		
		private bool isArray;
		private bool isImplicitlyTyped;

		/// <summary>
		/// Gets the context ID for an object type AST node.
		/// </summary>
		/// <value>The context ID for an object type AST node.</value>
		public const byte ObjectTypeContextID = Expression.ExpressionContextIDBase;
		
		/// <summary>
		/// Gets the context ID for an argument AST node.
		/// </summary>
		/// <value>The context ID for an argument AST node.</value>
		public const byte ArgumentContextID = Expression.ExpressionContextIDBase + 1;
		
		/// <summary>
		/// Gets the context ID for an initializer AST node.
		/// </summary>
		/// <value>The context ID for an initializer AST node.</value>
		public const byte InitializerContextID = Expression.ExpressionContextIDBase + 2;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>ObjectCreationExpression</c> class. 
		/// </summary>
		/// <param name="objectType">The object <see cref="TypeReference"/>.</param>
		public ObjectCreationExpression(TypeReference objectType) {
			// Initialize parameters
			this.ObjectType = objectType;
		}
		
		/// <summary>
		/// Initializes a new instance of the <c>ObjectCreationExpression</c> class. 
		/// </summary>
		/// <param name="objectType">The object <see cref="TypeReference"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public ObjectCreationExpression(TypeReference objectType, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.ObjectType = objectType;
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
		/// Gets the collection of argument expressions.
		/// </summary>
		/// <value>The collection of argument expressions.</value>
		public IAstNodeList Arguments {
			get {
				return new AstNodeListWrapper(this, ObjectCreationExpression.ArgumentContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="ObjectCollectionInitializerExpression"/> that is used to initialize the object.
		/// </summary>
		/// <value>The <see cref="ObjectCollectionInitializerExpression"/> that is used to initialize the object.</value>
		public ObjectCollectionInitializerExpression Initializer {
			get {
				return this.GetChildNode(ObjectCreationExpression.InitializerContextID) as ObjectCollectionInitializerExpression;
			}
			set {
				this.ChildNodes.Replace(value, ObjectCreationExpression.InitializerContextID);
			}
		}

		/// <summary>
		/// Gets or sets whether this expression creates an array.
		/// </summary>
		/// <value>
		/// <c>true</c> if this expression creates an array; otherwise, <c>false</c>.
		/// </value>
		public bool IsArray {
			get {
				return isArray;
			}
			set {
				isArray = value;
			}
		}
		
		/// <summary>
		/// Gets or sets whether the object is implicitly typed.
		/// </summary>
		/// <value>
		/// <c>true</c> if the object is implicitly typed; otherwise, <c>false</c>.
		/// </value>
		public bool IsImplicitlyTyped {
			get {
				return isImplicitlyTyped;
			}
			set {
				isImplicitlyTyped = value;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.ObjectCreationExpression;
			}
		}

		/// <summary>
		/// Gets or sets the object <see cref="TypeReference"/>.
		/// </summary>
		/// <value>The object <see cref="TypeReference"/>.</value>
		public TypeReference ObjectType {
			get {
				return this.GetChildNode(ObjectCreationExpression.ObjectTypeContextID) as TypeReference;
			}
			set {
				this.ChildNodes.Replace(value, ObjectCreationExpression.ObjectTypeContextID);
			}
		}

	}
}
