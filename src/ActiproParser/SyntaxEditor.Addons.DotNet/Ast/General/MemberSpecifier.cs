using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a member specifier.
	/// </summary>
	public class MemberSpecifier : AstNode {

		/// <summary>
		/// Gets the context ID for the type reference AST node.
		/// </summary>
		/// <value>The context ID for the type reference AST node.</value>
		public const byte TypeReferenceContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a member name AST node.
		/// </summary>
		/// <value>The context ID for a member name AST node.</value>
		public const byte MemberNameContextID = AstNode.AstNodeContextIDBase + 1;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>MemberSpecifier</c> class. 
		/// </summary>
		/// <param name="typeReference">The <see cref="TypeReference"/> to the type that contains the member.</param>
		/// <param name="memberName">The <see cref="QualifiedIdentifier"/> that contains the name of the member.</param>
		public MemberSpecifier(TypeReference typeReference, QualifiedIdentifier memberName) : 
			base(new TextRange(typeReference.StartOffset, memberName.EndOffset)) {
			
			// Initialize parameters
			this.TypeReference	= typeReference;
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
				return this.GetChildNode(MemberSpecifier.MemberNameContextID) as QualifiedIdentifier;
			}
			set {
				this.ChildNodes.Replace(value, MemberSpecifier.MemberNameContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.MemberSpecifier;
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="TypeReference"/> to the type that contains the member.
		/// </summary>
		/// <value>The <see cref="TypeReference"/> to the type that contains the member.</value>
		public TypeReference TypeReference {
			get {
				return this.GetChildNode(MemberSpecifier.TypeReferenceContextID) as TypeReference;
			}
			set {
				this.ChildNodes.Replace(value, MemberSpecifier.TypeReferenceContextID);
			}
		}
		
	}
}
