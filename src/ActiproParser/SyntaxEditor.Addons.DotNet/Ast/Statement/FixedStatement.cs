using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a fixed statement.
	/// </summary>
	public class FixedStatement : ChildStatementStatement {
		
		/// <summary>
		/// Gets the context ID for a declarator AST node.
		/// </summary>
		/// <value>The context ID for a declarator AST node.</value>
		public const byte DeclaratorContextID = ChildStatementStatement.ChildStatementStatementContextIDBase;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>FixedStatement</c> class. 
		/// </summary>
		public FixedStatement() {}
		
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
		/// Gets the collection of declarators.
		/// </summary>
		/// <value>The collection of declarators.</value>
		public IAstNodeList Declarators {
			get {
				return new AstNodeListWrapper(this, FixedStatement.DeclaratorContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.FixedStatement;
			}
		}


	}
}
