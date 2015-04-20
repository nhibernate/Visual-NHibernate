using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a for statement.
	/// </summary>
	public class ForStatement : ChildStatementStatement {
		
		/// <summary>
		/// Gets the context ID for an initializer AST node.
		/// </summary>
		/// <value>The context ID for an initializer AST node.</value>
		public const byte InitializerContextID = ChildStatementStatement.ChildStatementStatementContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a condition AST node.
		/// </summary>
		/// <value>The context ID for a condition AST node.</value>
		public const byte ConditionContextID = ChildStatementStatement.ChildStatementStatementContextIDBase + 1;

		/// <summary>
		/// Gets the context ID for an iterator AST node.
		/// </summary>
		/// <value>The context ID for an iterator AST node.</value>
		public const byte IteratorContextID = ChildStatementStatement.ChildStatementStatementContextIDBase + 2;

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
		/// Gets or sets the condition <see cref="Expression"/>.
		/// </summary>
		/// <value>The condition <see cref="Expression"/>.</value>
		public Expression Condition {
			get {
				return this.GetChildNode(ForStatement.ConditionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, ForStatement.ConditionContextID);
			}
		}
		
		/// <summary>
		/// Gets the collection of declarators.
		/// </summary>
		/// <value>The collection of declarators.</value>
		public IAstNodeList Initializers {
			get {
				return new AstNodeListWrapper(this, ForStatement.InitializerContextID);
			}
		}

		/// <summary>
		/// Gets the collection of iterators.
		/// </summary>
		/// <value>The collection of iterators.</value>
		public IAstNodeList Iterators {
			get {
				return new AstNodeListWrapper(this, ForStatement.IteratorContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.ForStatement;
			}
		}

	}
}
