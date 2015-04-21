using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a switch statement section.
	/// </summary>
	public class SwitchSection : AstNode {
		
		/// <summary>
		/// Gets the context ID for a label AST node.
		/// </summary>
		/// <value>The context ID for a label AST node.</value>
		public const byte LabelContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a statement AST node.
		/// </summary>
		/// <value>The context ID for a statement AST node.</value>
		public const byte StatementContextID = AstNode.AstNodeContextIDBase + 1;
		
		/// <summary>
		/// Gets the context ID for a comment AST node.
		/// </summary>
		/// <value>The context ID for a comment AST node.</value>
		public const byte CommentContextID = AstNode.AstNodeContextIDBase + 2;

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
		/// Gets the collection of comments that appear in the node.
		/// </summary>
		/// <value>The collection of comments that appear in the node.</value>
		public IAstNodeList Comments {
			get {
				return new AstNodeListWrapper(this, SwitchSection.CommentContextID);
			}
		}
		
		/// <summary>
		/// Gets the collection of labels.
		/// </summary>
		/// <value>The collection of labels.</value>
		public IAstNodeList Labels {
			get {
				return new AstNodeListWrapper(this, SwitchSection.LabelContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.SwitchSection;
			}
		}

		/// <summary>
		/// Gets the collection of statements.
		/// </summary>
		/// <value>The collection of statements.</value>
		public IAstNodeList Statements {
			get {
				return new AstNodeListWrapper(this, SwitchSection.StatementContextID);
			}
		}

	}
}
