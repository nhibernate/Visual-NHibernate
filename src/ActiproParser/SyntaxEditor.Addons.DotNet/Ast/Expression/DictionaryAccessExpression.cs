using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a dictionary access expression.
	/// </summary>
	public class DictionaryAccessExpression : ChildExpressionExpression {

		/// <summary>
		/// Gets the context ID for a dictionary key AST node.
		/// </summary>
		/// <value>The context ID for a dictionary key AST node.</value>
		public const byte DictionaryKeyContextID = ChildExpressionExpression.ChildExpressionExpressionContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>DictionaryAccessExpression</c> class. 
		/// </summary>
		/// <param name="expression">The <see cref="Expression"/> to cast.</param>
		/// <param name="dictionaryKey">An <see cref="QualifiedIdentifier"/> indicating the dictionary key.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public DictionaryAccessExpression(Expression expression, QualifiedIdentifier dictionaryKey, TextRange textRange) : base(expression, textRange) {
			// Initialize parameters
			this.DictionaryKey = dictionaryKey;
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
		/// Gets or sets the key for the dictionary lookup.
		/// </summary>
		/// <value>The key for the dictionary lookup.</value>
		public QualifiedIdentifier DictionaryKey {
			get {
				return this.GetChildNode(DictionaryAccessExpression.DictionaryKeyContextID) as QualifiedIdentifier;
			}
			set {
				this.ChildNodes.Replace(value, DictionaryAccessExpression.DictionaryKeyContextID);
			}
		}

		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.DictionaryAccessExpression;
			}
		}


	}
}
