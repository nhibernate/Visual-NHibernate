using System;
using System.Collections;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Provides the base class for a parameter declaration.
	/// </summary>
	public class ParameterDeclaration : AstNode, IDomParameter {

		private ParameterModifiers	modifiers;
		private string				name;
		
		/// <summary>
		/// Gets the context ID for an attribute section AST node.
		/// </summary>
		/// <value>The context ID for an attribute section AST node.</value>
		public const byte AttributeSectionContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a parameter type AST node.
		/// </summary>
		/// <value>The context ID for a parameter type AST node.</value>
		public const byte ParameterTypeContextID = AstNode.AstNodeContextIDBase + 1;
		
		/// <summary>
		/// Gets the context ID for an initializer AST node.
		/// </summary>
		/// <value>The context ID for an initializer AST node.</value>
		public const byte InitializerContextID = AstNode.AstNodeContextIDBase + 2;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>ParameterDeclaration</c> class. 
		/// </summary>
		/// <param name="modifiers">The modifiers.</param>
		/// <param name="name">The name.</param>
		public ParameterDeclaration(ParameterModifiers modifiers, string name) {
			// Initialize parameters
			this.modifiers = modifiers;
			this.name = name;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Close the parameter but assigns it the specified alternate <see cref="IDomTypeReference"/>.
		/// </summary>
		/// <param name="parameterType">A <see cref="IDomTypeReference"/> indicating the type.</param>
		/// <returns>The cloned parameter.</returns>
		IDomParameter IDomParameter.CloneForType(IDomTypeReference parameterType) {
			ParameterDeclaration decl = new ParameterDeclaration(modifiers, name);
			decl.TextRange = this.TextRange;

			// NOTE: Won't clone attribute sections

			decl.ParameterType = parameterType as TypeReference;
			if ((decl.ParameterType == null) && (parameterType != null))
				decl.ParameterType = new TypeReference(parameterType.FullName, false);
			decl.Initializer = this.Initializer;

			decl.ParentNode = this.ParentNode;

			return decl;
		}
		
		/// <summary>
		/// Gets the <see cref="IDomTypeReference"/> of the parameter.
		/// </summary>
		/// <value>The <see cref="IDomTypeReference"/> of the parameter.</value>
		IDomTypeReference IDomParameter.ParameterType { 
			get {
				return this.ParameterType;
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
		/// Gets the collection of attribute sections.
		/// </summary>
		/// <value>The collection of attribute sections.</value>
		public IAstNodeList AttributeSections {
			get {
				return new AstNodeListWrapper(this, ParameterDeclaration.AttributeSectionContextID);
			}
		}
			
		/// <summary>
		/// Gets or sets the initializer <see cref="Expression"/> for an optional parameter.
		/// </summary>
		/// <value>The initializer <see cref="Expression"/> for an optional parameter.</value>
		public Expression Initializer {
			get {
				return this.GetChildNode(ParameterDeclaration.InitializerContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, ParameterDeclaration.InitializerContextID);
			}
		}

		/// <summary>
		/// Gets whether the parameter is a by-reference parameter.
		/// </summary>
		/// <value>
		/// <c>true</c> if the parameter is a by-reference parameter; otherwise, <c>false</c>.
		/// </value>
		public bool IsByReference { 
			get {
				return ((modifiers & ParameterModifiers.Ref) == ParameterModifiers.Ref);
			}
		}

		/// <summary>
		/// Gets whether the parameter is an optional parameter.
		/// </summary>
		/// <value>
		/// <c>true</c> if the parameter is an optional parameter; otherwise, <c>false</c>.
		/// </value>
		public bool IsOptional { 
			get {
				return ((modifiers & ParameterModifiers.Optional) == ParameterModifiers.Optional);
			}
		}

		/// <summary>
		/// Gets whether the parameter is an output parameter.
		/// </summary>
		/// <value>
		/// <c>true</c> if the parameter is an output parameter; otherwise, <c>false</c>.
		/// </value>
		public bool IsOutput { 
			get {
				return ((modifiers & ParameterModifiers.Out) == ParameterModifiers.Out);
			}
		}

		/// <summary>
		/// Gets whether the parameter is a parameter array.
		/// </summary>
		/// <value>
		/// <c>true</c> if the parameter is a parameter array; otherwise, <c>false</c>.
		/// </value>
		public bool IsParameterArray { 
			get {
				return ((modifiers & ParameterModifiers.ParameterArray) == ParameterModifiers.ParameterArray);
			}
		}

		/// <summary>
		/// Gets or sets the modifiers.
		/// </summary>
		/// <value>The modifiers.</value>
		public ParameterModifiers Modifiers {
			get {
				return modifiers;
			}
			set {
				modifiers = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.ParameterDeclaration;
			}
		}
		
		/// <summary>
		/// Gets or sets the parameter type.
		/// </summary>
		/// <value>The parameter type.</value>
		public TypeReference ParameterType {
			get {
				return this.GetChildNode(ParameterDeclaration.ParameterTypeContextID) as TypeReference;
			}
			set {
				this.ChildNodes.Replace(value, ParameterDeclaration.ParameterTypeContextID);
			}
		}

	}
}
