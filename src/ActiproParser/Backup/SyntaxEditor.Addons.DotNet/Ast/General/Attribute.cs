using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an attribute.
	/// </summary>
	public class Attribute : AstNode {
		
		private string		target;
		
		/// <summary>
		/// Gets the context ID for an attribute type AST node.
		/// </summary>
		/// <value>The context ID for an attribute type AST node.</value>
		public const byte AttributeTypeContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the context ID for an argument AST node.
		/// </summary>
		/// <value>The context ID for an argument AST node.</value>
		public const byte ArgumentContextID = AstNode.AstNodeContextIDBase + 1;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>Attribute</c> class. 
		/// </summary>
		/// <param name="typeReference">The <see cref="TypeReference"/> of the attribute.</param>
		public Attribute(TypeReference typeReference) {
			// Initialize parameters
			this.AttributeType = typeReference;
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
		/// Gets the collection of arguments.
		/// </summary>
		/// <value>The collection of arguments.</value>
		public IAstNodeList Arguments {
			get {
				return new AstNodeListWrapper(this, Attribute.ArgumentContextID);
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="TypeReference"/> of the attribute.
		/// </summary>
		/// <value>The <see cref="TypeReference"/> of the attribute.</value>
		public TypeReference AttributeType {
			get {
				return this.GetChildNode(Attribute.AttributeTypeContextID) as TypeReference;
			}
			set {
				this.ChildNodes.Replace(value, Attribute.AttributeTypeContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.Attribute;
			}
		}
		
		/// <summary>
		/// Gets or sets the target of the attribute section.
		/// </summary>
		/// <value>The target of the attribute section.</value>
		public string Target {
			get {
				return target;
			}
			set {
				target = value;
			}
		}

	}
}
