using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a query expression collection range variable declaration.
	/// </summary>
	public class CollectionRangeVariableDeclaration : AstNode {

		/// <summary>
		/// Gets the context ID for the variable declarator AST node.
		/// </summary>
		/// <value>The context ID for the variable declarator AST node.</value>
		public const byte VariableDeclaratorContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a source AST node.
		/// </summary>
		/// <value>The context ID for a source AST node.</value>
		public const byte SourceContextID = AstNode.AstNodeContextIDBase + 1;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>CollectionRangeVariableDeclaration</c> class. 
		/// </summary>
		public CollectionRangeVariableDeclaration() {}

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
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.CollectionRangeVariableDeclaration;
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="Expression"/> that provides the source for the from clause.
		/// </summary>
		/// <value>The <see cref="Expression"/> that provides the source for the from clause.</value>
		public Expression Source {
			get {
				return this.GetChildNode(CollectionRangeVariableDeclaration.SourceContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, CollectionRangeVariableDeclaration.SourceContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="VariableDeclarator"/> that declares the variable for the from clause.
		/// </summary>
		/// <value>The <see cref="VariableDeclarator"/> that declares the variable for the from clause.</value>
		public VariableDeclarator VariableDeclarator {
			get {
				return this.GetChildNode(CollectionRangeVariableDeclaration.VariableDeclaratorContextID) as VariableDeclarator;
			}
			set {
				this.ChildNodes.Replace(value, CollectionRangeVariableDeclaration.VariableDeclaratorContextID);
			}
		}
		
	}
}
