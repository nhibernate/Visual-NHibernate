using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a using statement.
	/// </summary>
	public class UsingStatement : ChildStatementStatement {
		
		/// <summary>
		/// Gets the context ID for a resource acquisition AST node.
		/// </summary>
		/// <value>The context ID for a resource acquisition AST node.</value>
		public const byte ResourceAcquisitionContextID = ChildStatementStatement.ChildStatementStatementContextIDBase;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>UsingStatement</c> class. 
		/// </summary>
		/// <param name="resourceAcquisitions">The list of resource acquisition <see cref="AstNode"/>.</param>
		/// <param name="statement">The <see cref="Statement"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public UsingStatement(IAstNodeList resourceAcquisitions, Statement statement, TextRange textRange) : base(statement, textRange) {
			// Initialize parameters
			if ((resourceAcquisitions != null) && (resourceAcquisitions.Count > 0))
				this.ResourceAcquisitions.AddRange(resourceAcquisitions.ToArray());
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
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.UsingStatement;
			}
		}
		
		/// <summary>
		/// Gets the collection of resource acquisition <see cref="AstNode"/> objects.
		/// </summary>
		/// <value>The collection of resource acquisition <see cref="AstNode"/> objects.</value>
		public IAstNodeList ResourceAcquisitions {
			get {
				return new AstNodeListWrapper(this, UsingStatement.ResourceAcquisitionContextID);
			}
		}
		
	}
}
