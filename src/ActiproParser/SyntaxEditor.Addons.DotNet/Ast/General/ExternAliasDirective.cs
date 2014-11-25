using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an extern alias directive for a <see cref="CompilationUnit"/>.
	/// </summary>
	public class ExternAliasDirective : AstNode {

		private string				alias;
		
		/// <summary>
		/// Gets the context ID for a comment AST node.
		/// </summary>
		/// <value>The context ID for a comment AST node.</value>
		public const byte CommentContextID = AstNode.AstNodeContextIDBase;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>ExternAliasDirective</c> class.
		/// </summary>
		/// <param name="alias">The alias.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public ExternAliasDirective(string alias, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.alias = alias;
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
				return new AstNodeListWrapper(this, ExternAliasDirective.CommentContextID);
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
			return "Extern Alias " + this.Alias;
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
				return "(ExternAlias)." + alias; 
			}
		}

		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.ExternAliasDirective;
			}
		}

	}
}
