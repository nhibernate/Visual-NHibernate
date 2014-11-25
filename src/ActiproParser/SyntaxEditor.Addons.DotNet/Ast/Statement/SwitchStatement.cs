using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a switch statement.
	/// </summary>
	public class SwitchStatement : Statement {
		
		/// <summary>
		/// Gets the context ID for an expression AST node.
		/// </summary>
		/// <value>The context ID for an expression AST node.</value>
		public const byte ExpressionContextID = Statement.StatementContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a section AST node.
		/// </summary>
		/// <value>The context ID for a section AST node.</value>
		public const byte SectionContextID = Statement.StatementContextIDBase + 1;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>SwitchStatement</c> class. 
		/// </summary>
		/// <param name="expression">The switch <see cref="Expression"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public SwitchStatement(Expression expression, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.Expression	= expression;
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
		/// Gets or sets the switch <see cref="Expression"/>.
		/// </summary>
		/// <value>The switch <see cref="Expression"/>.</value>
		public Expression Expression {
			get {
				return this.GetChildNode(SwitchStatement.ExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, SwitchStatement.ExpressionContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.SwitchStatement;
			}
		}

		/// <summary>
		/// Gets the collection of switch sections.
		/// </summary>
		/// <value>The collection of switch sections.</value>
		public IAstNodeList Sections {
			get {
				return new AstNodeListWrapper(this, SwitchStatement.SectionContextID);
			}
		}


	}
}
