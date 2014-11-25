using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an event member specifier.
	/// Used in Visual Basic only.
	/// </summary>
	public class EventMemberSpecifier : AstNode {

		/// <summary>
		/// Gets the context ID for the target AST node.
		/// </summary>
		/// <value>The context ID for the target AST node.</value>
		public const byte TargetContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a member name AST node.
		/// </summary>
		/// <value>The context ID for a member name AST node.</value>
		public const byte MemberNameContextID = AstNode.AstNodeContextIDBase + 1;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>EventMemberSpecifier</c> class. 
		/// </summary>
		/// <param name="target">The <see cref="QualifiedIdentifier"/> that specifies the target object.</param>
		/// <param name="memberName">The <see cref="QualifiedIdentifier"/> that contains the name of the member.</param>
		public EventMemberSpecifier(QualifiedIdentifier target, QualifiedIdentifier memberName) : 
			base(new TextRange(target.StartOffset, (memberName != null ? memberName.EndOffset : target.EndOffset))) {
			
			// Initialize parameters
			this.Target			= target;
			this.MemberName		= memberName;
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
		/// Gets or sets the <see cref="QualifiedIdentifier"/> that contains the name of the member.
		/// </summary>
		/// <value>The <see cref="QualifiedIdentifier"/> that contains the name of the member.</value>
		public QualifiedIdentifier MemberName {
			get {
				return this.GetChildNode(EventMemberSpecifier.MemberNameContextID) as QualifiedIdentifier;
			}
			set {
				this.ChildNodes.Replace(value, EventMemberSpecifier.MemberNameContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.EventMemberSpecifier;
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="QualifiedIdentifier"/> that specifies the target object.
		/// </summary>
		/// <value>The <see cref="QualifiedIdentifier"/> that specifies the target object.</value>
		public QualifiedIdentifier Target {
			get {
				return this.GetChildNode(EventMemberSpecifier.TargetContextID) as QualifiedIdentifier;
			}
			set {
				this.ChildNodes.Replace(value, EventMemberSpecifier.TargetContextID);
			}
		}
		
	}
}
