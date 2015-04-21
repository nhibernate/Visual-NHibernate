using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a pointer member access expression.
	/// </summary>
	public class PointerMemberAccess : MemberAccess {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>PointerMemberAccess</c> class. 
		/// </summary>
		/// <param name="target">The target <see cref="Expression"/>.</param>
		/// <param name="memberName">A <see cref="QualifiedIdentifier"/> indicating the member name.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public PointerMemberAccess(Expression target, QualifiedIdentifier memberName, TextRange textRange) : base(target, memberName, textRange) {}
		
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
				return DotNetNodeType.PointerMemberAccess;
			}
		}

	}
}
