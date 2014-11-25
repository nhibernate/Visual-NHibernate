using System;
using System.Collections;
using System.Text;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a comment.
	/// </summary>
	public class Comment : AstNode, ICollapsibleNode {
		
		private string			text;
		private CommentType		type;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>Comment</c> class. 
		/// </summary>
		/// <param name="type">A <see cref="CommentType"/> indicating the type of comment.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		/// <param name="text">The comment text.</param>
		public Comment(CommentType type, TextRange textRange, string text) : base(textRange) {
			// Initialize parameters
			this.type = type;
			this.text = text;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets whether the node is collapsible.
		/// </summary>
		/// <value>
		/// <c>true</c> if the node is collapsible; otherwise, <c>false</c>.
		/// </value>
		bool ICollapsibleNode.IsCollapsible { 
			get {
				return (type != CommentType.SingleLine);
			}
		}

		/// <summary>
		/// Gets whether the outlining indicator should be visible for the node.
		/// </summary>
		/// <value>
		/// <c>true</c> if the outlining indicator should be visible for the node; otherwise, <c>false</c>.
		/// </value>
		bool IOutliningNodeParseData.IndicatorVisible { 
			get {
				return true;
			}
		}
		
		/// <summary>
		/// Gets whether the outlining node is for a language transition.
		/// </summary>
		/// <value>
		/// <c>true</c> if the outlining node is for a language transition; otherwise, <c>false</c>.
		/// </value>
		bool IOutliningNodeParseData.IsLanguageTransition { 
			get {
				return false;
			}
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
				return DotNetNodeType.Comment;
			}
		}
		
		/// <summary>
		/// Gets the comment text.
		/// </summary>
		/// <value>The comment text.</value>
		public string Text {
			get {
				return text;
			}
		}

		/// <summary>
		/// Converts the object to a <c>String</c>.
		/// </summary>
		/// <returns>
		/// A string whose value represents this object.
		/// </returns>
		public override string ToString() {
			return String.Format("[{0}-{1}] {2}: {3}", this.StartOffset, this.EndOffset, this.GetType().Name, text);
		}
		
		/// <summary>
		/// Gets the type of comment.
		/// </summary>
		/// <value>The type of comment.</value>
		public CommentType Type {
			get {
				return type;
			}
		}

	}
}
