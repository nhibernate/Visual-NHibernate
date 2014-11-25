using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a qualified identifier.
	/// </summary>
	public class QualifiedIdentifier : AstNode {

		private string		text;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>Identifier</c> class. 
		/// </summary>
		/// <param name="text">The text of the qualified identifier.</param>
		public QualifiedIdentifier(string text) {
			// Initialize parameters
			this.text = text;
		}

		/// <summary>
		/// Initializes a new instance of the <c>Identifier</c> class. 
		/// </summary>
		/// <param name="text">The text of the qualified identifier.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public QualifiedIdentifier(string text, TextRange textRange) : this(text) {
			// Initialize parameters
			this.StartOffset	= textRange.StartOffset;
			this.EndOffset		= textRange.EndOffset;
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
				return DotNetNodeType.QualifiedIdentifier;
			}
		}

		/// <summary>
		/// Gets or sets the text of the qualified identifier.
		/// </summary>
		/// <value>The text of the qualified identifier.</value>
		public string Text {
			get {
				return text;
			}
			set {
				text = value;
			}
		}

		/// <summary>
		/// Converts the object to a <c>String</c>.
		/// </summary>
		/// <returns>
		/// A string whose value represents this object.
		/// </returns>
		public override string ToString() {
			return base.ToString() + (text != null ? ": " + text : String.Empty);
		}
		
	}
}
