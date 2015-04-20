using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a branch statement.
	/// Used in Visual Basic only.
	/// </summary>
	public class BranchStatement : Statement {

		private BranchStatementType branchType;

		/// <summary>
		/// Gets the minimum context ID that should be used in your code for AST nodes inheriting this class.
		/// </summary>
		/// <value>The minimum context ID that should be used in your code for AST nodes inheriting this class.</value>
		/// <remarks>
		/// Base all your context ID constants off of this value.
		/// </remarks>
		protected const byte BranchStatementContextIDBase = Statement.StatementContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>BranchStatement</c> class. 
		/// </summary>
		/// <param name="branchType">A <see cref="BranchStatementType"/> specifying the branch statement type.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public BranchStatement(BranchStatementType branchType, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.branchType = branchType;
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
		/// Gets or sets a <see cref="BranchStatementType"/> specifying the branch statement type.
		/// </summary>
		/// <value>A <see cref="BranchStatementType"/> specifying the branch statement type.</value>
		public BranchStatementType BranchType {
			get {
				return branchType;
			}
			set {
				branchType = value;
			}
		}

		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.BranchStatement;
			}
		}


	}
}
