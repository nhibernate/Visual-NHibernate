using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an field declaration.
	/// </summary>
	public class FieldDeclaration : TypeMemberDeclaration, IVariableDeclarationSection {

		/// <summary>
		/// Gets the context ID for a variable AST node.
		/// </summary>
		/// <value>The context ID for a variable AST node.</value>
		public const byte VariableContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>FieldDeclaration</c> class. 
		/// </summary>
		/// <param name="modifiers">The modifiers.</param>
		public FieldDeclaration(Modifiers modifiers) : base(modifiers, null) {}

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
		/// Gets whether the member is a constant.
		/// </summary>
		/// <value>
		/// <c>true</c> if the member is a constant; otherwise, <c>false</c>.
		/// </value>
		public bool IsConst { 
			get {
				return ((this.Modifiers & Modifiers.Const) == Modifiers.Const);
			}
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
				return DotNetNodeType.FieldDeclaration;
			}
		}

		/// <summary>
		/// Gets the collection of variables that are declared.
		/// </summary>
		/// <value>The collection of variables that are declared.</value>
		public IAstNodeList Variables {
			get {
				return new AstNodeListWrapper(this, FieldDeclaration.VariableContextID);
			}
		}

	}
}
