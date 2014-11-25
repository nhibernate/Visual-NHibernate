using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a local variable declaration.
	/// </summary>
	public class LocalVariableDeclaration : Statement, IVariableDeclarationSection {
		
		private Modifiers				modifiers;

		/// <summary>
		/// Gets the context ID for a variable AST node.
		/// </summary>
		/// <value>The context ID for a variable AST node.</value>
		public const byte VariableContextID = Statement.StatementContextIDBase;
		
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
		/// Gets or sets the modifiers.
		/// </summary>
		/// <value>The modifiers.</value>
		public Modifiers Modifiers {
			get {
				return modifiers;
			}
			set {
				modifiers = value;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.LocalVariableDeclaration;
			}
		}

		/// <summary>
		/// Gets the collection of variables that are declared.
		/// </summary>
		/// <value>The collection of variables that are declared.</value>
		public IAstNodeList Variables {
			get {
				return new AstNodeListWrapper(this, LocalVariableDeclaration.VariableContextID);
			}
		}

	}
}
