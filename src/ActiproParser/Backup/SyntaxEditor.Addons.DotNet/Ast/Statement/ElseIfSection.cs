using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an else if statement section.
	/// </summary>
	public class ElseIfSection : AstNode {
	
		/// <summary>
		/// Gets the context ID for a condition AST node.
		/// </summary>
		/// <value>The context ID for a condition AST node.</value>
		public const byte ConditionContextID = AstNode.AstNodeContextIDBase;
		
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
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>ElseIfSection</c> class. 
		/// </summary>
		/// <param name="condition">The condition <see cref="Expression"/>.</param>
		/// <param name="statement">The true <see cref="Statement"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public ElseIfSection(Expression condition, Statement statement, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.Condition	= condition;
			this.Statement	= statement;
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
		/// Gets the collection of comments that appear in the node.
		/// </summary>
		/// <value>The collection of comments that appear in the node.</value>
		public IAstNodeList Comments {
			get {
				return new AstNodeListWrapper(this, SwitchSection.CommentContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the condition <see cref="Expression"/>.
		/// </summary>
		/// <value>The condition <see cref="Expression"/>.</value>
		public Expression Condition {
			get {
				return this.GetChildNode(ElseIfSection.ConditionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, ElseIfSection.ConditionContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.ElseIfSection;
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="Statement"/>.
		/// </summary>
		/// <value>The <see cref="Statement"/>.</value>
		public Statement Statement {
			get {
				return this.GetChildNode(ElseIfSection.StatementContextID) as Statement;
			}
			set {
				this.ChildNodes.Replace(value, ElseIfSection.StatementContextID);
			}
		}

	}
}
