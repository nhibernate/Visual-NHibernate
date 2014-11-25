using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a <c>using</c> directive for a <see cref="CompilationUnit"/>.
	/// </summary>
	public class UsingDirective : AstNode {

		private string				alias;
		
		/// <summary>
		/// Gets the context ID for a namespace name AST node.
		/// </summary>
		/// <value>The context ID for a namespace name AST node.</value>
		public const byte NamespaceNameContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a comment AST node.
		/// </summary>
		/// <value>The context ID for a comment AST node.</value>
		public const byte CommentContextID = AstNode.AstNodeContextIDBase + 1;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>UsingDirective</c> class.
		/// </summary>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public UsingDirective(TextRange textRange) : base(textRange) {}
		
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
		/// Gets or sets the alias.
		/// </summary>
		/// <value>The alias.</value>
		public string Alias {
			get {
				return alias;
			}
			set {
				alias = value;
			}
		}
		
		/// <summary>
		/// Gets the collection of comments that appear before the node.
		/// </summary>
		/// <value>The collection of comments that appear before the node.</value>
		public IAstNodeList Comments {
			get {
				return new AstNodeListWrapper(this, UsingDirective.CommentContextID);
			}
		}
		
		/// <summary>
		/// Returns display text that represents the AST node using the specified options.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> to use for formatting the text.</param>
		/// <param name="detailLevel">A <see cref="DisplayTextDetailLevel"/> indicating the desired level of detail.</param>
		/// <returns>The display text that represents the AST node using the specified options.</returns>
		/// <remarks>
		/// This method is useful for getting text to display for the node for use in a type/member drop-down list or class browser.
		/// </remarks>
		public override string GetDisplayText(DotNetLanguage language, DisplayTextDetailLevel detailLevel) {
			switch (language) {
				case DotNetLanguage.VB:
					return "Imports " + this.NamespaceName.Text;
				default:
					return "Using " + this.NamespaceName.Text;
			}
		}

		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		public override int ImageIndex {
			get {
				return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword;
			}
		}
		
		/// <summary>
		/// Gets the string-based key that uniquely identifies the <see cref="AstNode"/>.
		/// </summary>
		/// <value>The string-based key that uniquely identifies the <see cref="AstNode"/>.</value>
		public override string Key {
			get {
				return "(Using)." + this.NamespaceName.Text; 
			}
		}

		/// <summary>
		/// Gets or sets the namespace name.
		/// </summary>
		/// <value>The namespace name.</value>
		public QualifiedIdentifier NamespaceName {
			get {
				return this.GetChildNode(UsingDirective.NamespaceNameContextID) as QualifiedIdentifier;
			}
			set {
				this.ChildNodes.Replace(value, UsingDirective.NamespaceNameContextID);
			}
		}
		
		/// <summary>
		/// Gets the character offset at which to navigate when the editor's caret should jump to the text representation of the AST node.
		/// </summary>
		/// <value>The character offset at which to navigate when the editor's caret should jump to the text representation of the AST node.</value>
		public override int NavigationOffset {
			get {
				QualifiedIdentifier namespaceName = this.NamespaceName;
				if ((namespaceName != null) && (namespaceName.HasStartOffset))
					return namespaceName.NavigationOffset;
				else
					return base.NavigationOffset;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.UsingDirective;
			}
		}

	}
}
