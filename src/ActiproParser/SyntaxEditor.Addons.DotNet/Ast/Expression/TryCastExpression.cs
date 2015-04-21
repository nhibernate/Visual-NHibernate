using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a try cast expression.
	/// </summary>
	public class TryCastExpression : ChildExpressionExpression {
		
		/// <summary>
		/// Gets the context ID for a return type AST node.
		/// </summary>
		/// <value>The context ID for a return type AST node.</value>
		public const byte ReturnTypeContextID = ChildExpressionExpression.ChildExpressionExpressionContextIDBase;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>TryCastExpression</c> class. 
		/// </summary>
		/// <param name="expression">The <see cref="Expression"/> to examine.</param>
		/// <param name="returnType">An <see cref="TypeReference"/> indicating the return type.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public TryCastExpression(Expression expression, TypeReference returnType, TextRange textRange) : base(expression, textRange) {
			// Initialize parameters
			this.ReturnType = returnType;
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
				return DotNetNodeType.TryCastExpression;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="TypeReference"/> of the return type.
		/// </summary>
		/// <value>The <see cref="TypeReference"/> of the return type.</value>
		public TypeReference ReturnType {
			get {
				return this.GetChildNode(TryCastExpression.ReturnTypeContextID) as TypeReference;
			}
			set {
				this.ChildNodes.Replace(value, TryCastExpression.ReturnTypeContextID);
			}
		}

	}
}
