using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an interface accessor.
	/// </summary>
	public class InterfaceAccessor : AstNode {

		private InterfaceAccessorType	accessorType;
		
		/// <summary>
		/// Gets the context ID for an attribute section AST node.
		/// </summary>
		/// <value>The context ID for an attribute section AST node.</value>
		public const byte AttributeSectionContextID = AstNode.AstNodeContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>InterfaceAccessor</c> class. 
		/// </summary>
		/// <param name="accessorType">The accessor type.</param>
		public InterfaceAccessor(InterfaceAccessorType accessorType) {
			// Initialize parameters
			this.accessorType = accessorType;
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
		/// Gets or sets the accessor type.
		/// </summary>
		/// <value>The accessor type.</value>
		public InterfaceAccessorType AccessorType {
			get {
				return accessorType;
			}
			set {
				accessorType = value;
			}
		}
	
		/// <summary>
		/// Gets the collection of attribute sections.
		/// </summary>
		/// <value>The collection of attribute sections.</value>
		public IAstNodeList AttributeSections {
			get {
				return new AstNodeListWrapper(this, InterfaceAccessor.AttributeSectionContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.InterfaceAccessor;
			}
		}

	}
}
