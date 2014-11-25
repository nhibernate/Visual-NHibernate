using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an array redim statement.
	/// Used in Visual Basic only.
	/// </summary>
	public class ArrayRedimStatement : Statement {
		
		/// <summary>
		/// Gets the context ID for a redim clause AST node.
		/// </summary>
		/// <value>The context ID for a redim clause AST node.</value>
		public const byte RedimClauseContextID = Statement.StatementContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>ArrayRedimStatement</c> class. 
		/// </summary>
		public ArrayRedimStatement() {}
		
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
		/// Gets the collection of redim clauses.
		/// </summary>
		/// <value>The collection of redim clauses.</value>
		public IAstNodeList Clauses {
			get {
				return new AstNodeListWrapper(this, ArrayRedimStatement.RedimClauseContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.ArrayRedimStatement;
			}
		}


	}
}
