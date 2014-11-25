using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an accessor declaration.
	/// </summary>
	public class AccessorDeclaration : AstNode, ICollapsibleNode {

		private Modifiers				modifiers			= Modifiers.None;
		
		/// <summary>
		/// Gets the context ID for an attribute section AST node.
		/// </summary>
		/// <value>The context ID for an attribute section AST node.</value>
		public const byte AttributeSectionContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a block statement AST node.
		/// </summary>
		/// <value>The context ID for a block statement AST node.</value>
		public const byte BlockStatementContextID = AstNode.AstNodeContextIDBase + 1;
		
		/// <summary>
		/// Gets the context ID for a parameter AST node.
		/// </summary>
		/// <value>The context ID for a parameter AST node.</value>
		public const byte ParameterContextID = AstNode.AstNodeContextIDBase + 2;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the offset at which the outlining node ends.
		/// </summary>
		/// <value>The offset at which the outlining node ends.</value>
		int ICollapsibleNode.EndOffset { 
			get {
				BlockStatement blockStatement = this.BlockStatement;
				return (blockStatement != null ? blockStatement.EndOffset : -1);
			}
		}

		/// <summary>
		/// Gets whether the node is collapsible.
		/// </summary>
		/// <value>
		/// <c>true</c> if the node is collapsible; otherwise, <c>false</c>.
		/// </value>
		bool ICollapsibleNode.IsCollapsible { 
			get {
				ICollapsibleNode node = (ICollapsibleNode)this;
				return (node.StartOffset != -1) && ((node.StartOffset < node.EndOffset) || (node.EndOffset == -1));
			}
		}

		/// <summary>
		/// Gets the offset at which the outlining node starts.
		/// </summary>
		/// <value>The offset at which the outlining node starts.</value>
		int ICollapsibleNode.StartOffset { 
			get {
				BlockStatement blockStatement = this.BlockStatement;
				return (blockStatement != null ? blockStatement.StartOffset : -1);
			}
		}
		
		/// <summary>
		/// Gets whether the outlining indicator should be visible for the node.
		/// </summary>
		/// <value>
		/// <c>true</c> if the outlining indicator should be visible for the node; otherwise, <c>false</c>.
		/// </value>
		bool IOutliningNodeParseData.IndicatorVisible { 
			get {
				return true;
			}
		}

		/// <summary>
		/// Gets whether the outlining node is for a language transition.
		/// </summary>
		/// <value>
		/// <c>true</c> if the outlining node is for a language transition; otherwise, <c>false</c>.
		/// </value>
		bool IOutliningNodeParseData.IsLanguageTransition { 
			get {
				return false;
			}
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
		/// Gets the access-related values of <see cref="Modifiers"/>.
		/// </summary>
		/// <value>The access-related values of <see cref="Modifiers"/>.</value>
		public Modifiers AccessModifiers {
			get {
				return modifiers & Modifiers.AccessMask;
			}
		}

		/// <summary>
		/// Gets the collection of attribute sections.
		/// </summary>
		/// <value>The collection of attribute sections.</value>
		public IAstNodeList AttributeSections {
			get {
				return new AstNodeListWrapper(this, AccessorDeclaration.AttributeSectionContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the block <see cref="Statement"/>.
		/// </summary>
		/// <value>The block <see cref="Statement"/>.</value>
		public BlockStatement BlockStatement {
			get {
				return this.GetChildNode(AccessorDeclaration.BlockStatementContextID) as BlockStatement;
			}
			set {
				this.ChildNodes.Replace(value, AccessorDeclaration.BlockStatementContextID);
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
				return DotNetNodeType.AccessorDeclaration;
			}
		}
		
		/// <summary>
		/// Gets the collection of parameters.
		/// </summary>
		/// <value>The collection of parameters.</value>
		public IAstNodeList Parameters {
			get {
				return new AstNodeListWrapper(this, AccessorDeclaration.ParameterContextID);
			}
		}
		
	}
}
