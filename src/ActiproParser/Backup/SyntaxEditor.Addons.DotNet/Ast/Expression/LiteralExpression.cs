using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a literal expression.
	/// </summary>
	public class LiteralExpression : Expression {

		private LiteralType	literalType;
		private string		literalValue;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>LiteralExpression</c> class. 
		/// </summary>
		/// <param name="literalType">A <see cref="LiteralType"/> indicating the literal type.</param>
		/// <param name="literalValue">The value of the literal.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public LiteralExpression(LiteralType literalType, string literalValue, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.literalType	= literalType;
			this.literalValue	= literalValue;
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
		/// Gets or sets a <see cref="LiteralType"/> indicating the literal type.
		/// </summary>
		/// <value>A <see cref="LiteralType"/> indicating the literal type.</value>
		public LiteralType LiteralType {
			get {
				return literalType;
			}
			set {
				literalType = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the value of the literal.
		/// </summary>
		/// <value>The value of the literal.</value>
		/// <remarks>This property will only return values for numeric, string and character literals.</remarks>
		public string LiteralValue {
			get {
				return literalValue;
			}
			set {
				literalValue = value;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.LiteralExpression;
			}
		}
		
		/// <summary>
		/// Converts the object to a <c>String</c>.
		/// </summary>
		/// <returns>
		/// A string whose value represents this object.
		/// </returns>
		public override string ToString() {
			return base.ToString() + ": " + literalType;
		}
		

	}
}
