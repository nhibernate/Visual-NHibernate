using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a member access expression.
	/// </summary>
	public class MemberAccess : ChildExpressionExpression {

		private MemberAccessType memberAccessType = MemberAccessType.Default;

		/// <summary>
		/// Gets the context ID for a member name AST node.
		/// </summary>
		/// <value>The context ID for a member name AST node.</value>
		public const byte MemberNameContextID = ChildExpressionExpression.ChildExpressionExpressionContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>MemberAccess</c> class. 
		/// </summary>
		/// <param name="target">The target <see cref="Expression"/>.</param>
		/// <param name="memberName">A <see cref="QualifiedIdentifier"/> indicating the member name.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public MemberAccess(Expression target, QualifiedIdentifier memberName, TextRange textRange) : base(target, textRange) {
			// Initialize parameters
			this.MemberName = memberName;
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
		/// Gets or sets a <see cref="MemberAccessType"/> indicating the type of member access.
		/// </summary>
		/// <value>A <see cref="MemberAccessType"/> indicating the type of member access.</value>
		public MemberAccessType MemberAccessType {
			get {
				return memberAccessType;
			}
			set {
				memberAccessType = value;
			}
		}
		
		/// <summary>
		/// Gets or sets a <see cref="QualifiedIdentifier"/> indicating the member name.
		/// </summary>
		/// <value>A <see cref="QualifiedIdentifier"/> indicating the member name.</value>
		public QualifiedIdentifier MemberName {
			get {
				return this.GetChildNode(MemberAccess.MemberNameContextID) as QualifiedIdentifier;
			}
			set {
				this.ChildNodes.Replace(value, MemberAccess.MemberNameContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.MemberAccess;
			}
		}

	}
}
