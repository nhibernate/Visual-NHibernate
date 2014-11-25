using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an argument expression.
	/// </summary>
	public class ArgumentExpression : ChildExpressionExpression {

		private ParameterModifiers	modifiers;
		
		/// <summary>
		/// Gets the context ID for a name AST node.
		/// </summary>
		/// <value>The context ID for a name AST node.</value>
		public const byte NameContextID = ChildExpressionExpression.ChildExpressionExpressionContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>ArgumentExpression</c> class. 
		/// </summary>
		/// <param name="modifiers">An <see cref="ParameterModifiers"/> indicating the argument modifier.</param>
		/// <param name="expression">The <see cref="Expression"/> affected by the argument modifier.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public ArgumentExpression(ParameterModifiers modifiers, Expression expression, TextRange textRange) : this(modifiers, null, expression, textRange) {}
		
		/// <summary>
		/// Initializes a new instance of the <c>ArgumentExpression</c> class. 
		/// </summary>
		/// <param name="modifiers">An <see cref="ParameterModifiers"/> indicating the argument modifier.</param>
		/// <param name="name">The name of the argument.</param>
		/// <param name="expression">The <see cref="Expression"/> affected by the argument modifier.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public ArgumentExpression(ParameterModifiers modifiers, QualifiedIdentifier name, Expression expression, TextRange textRange) : base(expression, textRange) {
			// Initialize parameters
			this.modifiers	= modifiers;
			this.Name		= name;
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
		/// Gets or sets an <see cref="ParameterModifiers"/> indicating the argument modifiers.
		/// </summary>
		/// <value>An <see cref="ParameterModifiers"/> indicating the argument modifiers.</value>
		public ParameterModifiers Modifiers {
			get {
				return modifiers;
			}
			set {
				modifiers = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the name of the argument.
		/// </summary>
		/// <value>The name of the argument.</value>
		public QualifiedIdentifier Name {
			get {
				return this.GetChildNode(ArgumentExpression.NameContextID) as QualifiedIdentifier;
			}
			set {
				this.ChildNodes.Replace(value, ArgumentExpression.NameContextID);
			}
		}

		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.ArgumentExpression;
			}
		}


	}
}
