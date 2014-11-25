using System;
using System.Collections;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a structure declaration block.
	/// </summary>
	public class StructureDeclaration : TypeDeclaration {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>StructDeclaration</c> class. 
		/// </summary>
		/// <param name="modifiers">The modifiers.</param>
		/// <param name="name">The name.</param>
		public StructureDeclaration(Modifiers modifiers, QualifiedIdentifier name) : base(modifiers, name) {}

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
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		public override int ImageIndex {
			get {
				return AssemblyDomType.GetReflectionImageIndex(DomTypeType.Structure, this.AccessModifiers);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.StructureDeclaration;
			}
		}

	}
}
