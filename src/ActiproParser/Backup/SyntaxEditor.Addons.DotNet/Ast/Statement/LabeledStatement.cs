using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a labeled statement.
	/// </summary>
	public class LabeledStatement : ChildStatementStatement {
		
		/// <summary>
		/// Gets the context ID for a label AST node.
		/// </summary>
		/// <value>The context ID for a label AST node.</value>
		public const byte LabelContextID = ChildStatementStatement.ChildStatementStatementContextIDBase;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>LabeledStatement</c> class. 
		/// </summary>
		/// <param name="label">An <see cref="Expression"/> indicating the label.</param>
		/// <param name="statement">The <see cref="Statement"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public LabeledStatement(Expression label, Statement statement, TextRange textRange) : base(statement, textRange) {
			// Initialize parameters
			this.Label = label;
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
		/// Gets or sets an <see cref="Expression"/> indicating the label.
		/// </summary>
		/// <value>An <see cref="Expression"/> indicating the label.</value>
		public Expression Label {
			get {
				return this.GetChildNode(LabeledStatement.LabelContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, LabeledStatement.LabelContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.LabeledStatement;
			}
		}


	}
}
