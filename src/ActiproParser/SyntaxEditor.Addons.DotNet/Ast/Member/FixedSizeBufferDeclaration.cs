using System;
using System.Collections;
using System.Reflection;
using System.Text;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a fixed size buffer declaration.
	/// </summary>
	public class FixedSizeBufferDeclaration : TypeMemberDeclaration, IVariableDeclarationSection {

		/// <summary>
		/// Gets the context ID for a variable AST node.
		/// </summary>
		/// <value>The context ID for a variable AST node.</value>
		public const byte VariableContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>FixedSizeBufferDeclaration</c> class. 
		/// </summary>
		/// <param name="modifiers">The modifiers.</param>
		public FixedSizeBufferDeclaration(Modifiers modifiers) : base(modifiers, null) {}

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
		/// Gets an <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.
		/// </summary>
		/// <value>An <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.</value>
		public override DotNetNodeCategory NodeCategory {
			get {
				return DotNetNodeCategory.TypeMemberDeclarationSection;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.FixedSizeBufferDeclaration;
			}
		}

		/// <summary>
		/// Gets the collection of variables that are declared.
		/// </summary>
		/// <value>The collection of variables that are declared.</value>
		public IAstNodeList Variables {
			get {
				return new AstNodeListWrapper(this, FixedSizeBufferDeclaration.VariableContextID);
			}
		}

	}
}
