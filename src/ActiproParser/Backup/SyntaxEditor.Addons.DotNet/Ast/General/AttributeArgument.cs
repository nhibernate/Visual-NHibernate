using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an attribute argument.
	/// </summary>
	public class AttributeArgument : AstNode {

		private string		name;
		
		/// <summary>
		/// Gets the context ID for an expression AST node.
		/// </summary>
		/// <value>The context ID for an expression AST node.</value>
		public const byte ExpressionContextID = AstNode.AstNodeContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>Attribute</c> class. 
		/// </summary>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="expression">The argument <see cref="Expression"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public AttributeArgument(string name, Expression expression, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.name		= name;
			this.Expression = expression;
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
		/// Gets or sets the argument <see cref="Expression"/>.
		/// </summary>
		/// <value>The argument <see cref="Expression"/>.</value>
		public Expression Expression {
			get {
				return this.GetChildNode(AttributeArgument.ExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, AttributeArgument.ExpressionContextID);
			}
		}

		/// <summary>
		/// Gets or sets the name of the attribute argument.
		/// </summary>
		/// <value>The name of the attribute argument.</value>
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.AttributeArgument;
			}
		}

	}
}
