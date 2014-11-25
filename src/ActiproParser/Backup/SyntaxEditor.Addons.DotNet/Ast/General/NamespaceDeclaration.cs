using System;
using System.Collections;
using System.Text;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a namespace declaration block.
	/// </summary>
	public class NamespaceDeclaration : AstNode, IBlockAstNode, ICollapsibleNode {

		private int						blockEndOffset		= -1;
		private int						blockStartOffset	= -1;
		
		/// <summary>
		/// Gets the context ID for a namespace name AST node.
		/// </summary>
		/// <value>The context ID for a namespace name AST node.</value>
		public const byte NameContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a using directive section AST node.
		/// </summary>
		/// <value>The context ID for a using directive section AST node.</value>
		public const byte UsingDirectiveSectionContextID = AstNode.AstNodeContextIDBase + 1;

		/// <summary>
		/// Gets the context ID for a namespace member AST node.
		/// </summary>
		/// <value>The context ID for a namespace member AST node.</value>
		public const byte NamespaceMemberContextID = AstNode.AstNodeContextIDBase + 2;
		
		/// <summary>
		/// Gets the context ID for a comment AST node.
		/// </summary>
		/// <value>The context ID for a comment AST node.</value>
		public const byte CommentContextID = AstNode.AstNodeContextIDBase + 3;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the offset at which the outlining node ends.
		/// </summary>
		/// <value>The offset at which the outlining node ends.</value>
		int ICollapsibleNode.EndOffset { 
			get {
				return blockEndOffset;
			}
		}

		/// <summary>
		/// Gets whether the node is collapsible.
		/// </summary>
		/// <value>
		/// <c>true</c> if the node is collapsible; otherwise, <c>false</c>.
		/// </value>
		bool ICollapsibleNode.IsCollapsible { 
			get {
				return (blockStartOffset != -1) && ((blockStartOffset < blockEndOffset) || (blockEndOffset == -1));
			}
		}

		/// <summary>
		/// Gets the offset at which the outlining node starts.
		/// </summary>
		/// <value>The offset at which the outlining node starts.</value>
		int ICollapsibleNode.StartOffset { 
			get {
				return blockStartOffset;
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
		/// Gets or sets the end character offset of the end block delimiter in the original source code that generated the AST node.
		/// </summary>
		/// <value>The end character offset of the end block delimiter in the original source code that generated the AST node.</value>
		/// <remarks>
		/// This value may be <c>-1</c> if there is no source code information for the end character offset.
		/// </remarks>
		public int BlockEndOffset {
			get {
				return blockEndOffset;
			}
			set {
				blockEndOffset = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the start character offset of the start block delimiter in the original source code that generated the AST node.
		/// </summary>
		/// <value>The start character offset of the start block delimiter in the original source code that generated the AST node.</value>
		/// <remarks>
		/// This value may be <c>-1</c> if there is no source code information for the start character offset.
		/// </remarks>
		public int BlockStartOffset {
			get {
				return blockStartOffset;
			}
			set {
				blockStartOffset = value;
			}
		}
	
		/// <summary>
		/// Gets the collection of comments that appear at the end of the node.
		/// </summary>
		/// <value>The collection of comments that appear at the end of the node.</value>
		public IAstNodeList Comments {
			get {
				return new AstNodeListWrapper(this, NamespaceDeclaration.CommentContextID);
			}
		}
		
		/// <summary>
		/// Gets the full name of the namespace.
		/// </summary>
		/// <value>The full name of the namespace.</value>
		public string FullName {
			get {
				StringBuilder fullName = new StringBuilder();
				if (this.ParentNode is NamespaceDeclaration) {
					fullName.Insert(0, ".");
					fullName.Insert(0, ((NamespaceDeclaration)this.ParentNode).FullName);
				}
				if (this.Name != null)
					fullName.Append(this.Name.Text);
				return fullName.ToString();
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
			if (this.Name != null)
				return this.Name.Text;
			else
				return "?";
		}

		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		public override int ImageIndex {
			get {
				return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.Namespace;
			}
		}

		/// <summary>
		/// Gets the string-based key that uniquely identifies the <see cref="AstNode"/>.
		/// </summary>
		/// <value>The string-based key that uniquely identifies the <see cref="AstNode"/>.</value>
		public override string Key {
			get {
				return "N:" + this.FullName;
			}
		}
		
		/// <summary>
		/// Gets or sets the name of the namespace.
		/// </summary>
		/// <value>The name of the namespace.</value>
		public QualifiedIdentifier Name {
			get {
				return this.GetChildNode(NamespaceDeclaration.NameContextID) as QualifiedIdentifier;
			}
			set {
				this.ChildNodes.Replace(value, NamespaceDeclaration.NameContextID);
			}
		}

		/// <summary>
		/// Gets the collection of namespaces and members.
		/// </summary>
		/// <value>The collection of namespaces and members.</value>
		public IAstNodeList NamespaceMembers {
			get {
				return new AstNodeListWrapper(this, NamespaceDeclaration.NamespaceMemberContextID);
			}
		}
		
		/// <summary>
		/// Gets the character offset at which to navigate when the editor's caret should jump to the text representation of the AST node.
		/// </summary>
		/// <value>The character offset at which to navigate when the editor's caret should jump to the text representation of the AST node.</value>
		public override int NavigationOffset {
			get {
				QualifiedIdentifier name = this.Name;
				if ((name != null) && (name.HasStartOffset))
					return name.NavigationOffset;
				else
					return base.NavigationOffset;
			}
		}
		
		/// <summary>
		/// Gets an <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.
		/// </summary>
		/// <value>An <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.</value>
		public override DotNetNodeCategory NodeCategory {
			get {
				return DotNetNodeCategory.NamespaceDeclaration;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.NamespaceDeclaration;
			}
		}
		
		/// <summary>
		/// Converts the object to a <c>String</c>.
		/// </summary>
		/// <returns>
		/// A string whose value represents this object.
		/// </returns>
		public override string ToString() {
			return base.ToString() + (this.Name != null ? ": Name=" + this.Name.Text : String.Empty);
		}
		
		/// <summary>
		/// Gets or sets the using directives block.
		/// </summary>
		/// <value>The using directives block.</value>
		public UsingDirectiveSection UsingDirectives {
			get {
				return this.GetChildNode(NamespaceDeclaration.UsingDirectiveSectionContextID) as UsingDirectiveSection;
			}
			set {
				this.ChildNodes.Replace(value, NamespaceDeclaration.UsingDirectiveSectionContextID);
			}
		}

	}
}
