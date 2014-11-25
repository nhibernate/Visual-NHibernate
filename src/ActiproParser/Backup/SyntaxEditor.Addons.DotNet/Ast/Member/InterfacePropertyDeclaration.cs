using System;
using System.Collections;
using System.Reflection;
using System.Text;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an interface property declaration.
	/// </summary>
	public class InterfacePropertyDeclaration : TypeMemberDeclaration, IDomMember {

		/// <summary>
		/// Gets the context ID for a return type AST node.
		/// </summary>
		/// <value>The context ID for a return type AST node.</value>
		public const byte ReturnTypeContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase;

		/// <summary>
		/// Gets the context ID for a get accessor AST node.
		/// </summary>
		/// <value>The context ID for a get accessor AST node.</value>
		public const byte GetAccessorContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase + 1;

		/// <summary>
		/// Gets the context ID for a set accessor AST node.
		/// </summary>
		/// <value>The context ID for a set accessor AST node.</value>
		public const byte SetAccessorContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase + 2;
		
		/// <summary>
		/// Gets the context ID for a parameter AST node.
		/// </summary>
		/// <value>The context ID for a parameter AST node.</value>
		public const byte ParameterContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase + 3;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>InterfacePropertyDeclaration</c> class. 
		/// </summary>
		/// <param name="modifiers">The modifiers.</param>
		/// <param name="name">The name.</param>
		public InterfacePropertyDeclaration(Modifiers modifiers, QualifiedIdentifier name) : base(modifiers | Modifiers.Public, name) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets a <see cref="IDomTypeReference"/> to the type that declares the member.
		/// </summary>
		/// <value>A <see cref="IDomTypeReference"/> to the type that declares the member.</value>
		IDomTypeReference IDomMember.DeclaringType {
			get {
				return this.ParentTypeDeclaration;
			}
		}
		
		/// <summary>
		/// Gets the type arguments if this is a generic method definition.
		/// </summary>
		/// <value>The type arguments if this is a generic method definition.</value>
		ICollection IDomMember.GenericTypeArguments { 
			get {
				return null;
			}
		}
		
		/// <summary>
		/// Gets whether the member is a generic method.
		/// </summary>
		/// <value>
		/// <c>true</c> if the method is a generic method; otherwise, <c>false</c>.
		/// </value>
		bool IDomMember.IsGenericMethod { 
			get {
				return false;
			}
		}

		/// <summary>
		/// Gets whether the member is a generic method definition, from which other generic methods can be constructed.
		/// </summary>
		/// <value>
		/// <c>true</c> if the method is a generic method definition, from which other generic methods can be constructed; otherwise, <c>false</c>.
		/// </value>
		bool IDomMember.IsGenericMethodDefinition { 
			get {
				return false;
			}
		}

		/// <summary>
		/// Gets a <see cref="DomMemberType"/> that indicates the type of member.
		/// </summary>
		/// <value>A <see cref="DomMemberType"/> that indicates the type of member.</value>
		DomMemberType IDomMember.MemberType { 
			get {
				return DomMemberType.Property;
			}
		}

		/// <summary>
		/// Gets the name of the member.
		/// </summary>
		/// <value>The name of the member.</value>
		string IDomMember.Name { 
			get {
				if (this.Name != null)
					return this.Name.Text;
				else
					return null;
			}
		}
		
		/// <summary>
		/// Gets the array of <see cref="IDomParameter"/> parameters for the member.
		/// </summary>
		/// <value>The array of <see cref="IDomParameter"/> parameters for the member.</value>
		IDomParameter[] IDomMember.Parameters {
			get {
				IAstNodeList parameters = this.Parameters;
				if (parameters.Count > 0) {
					IDomParameter[] parameterArray = new IDomParameter[parameters.Count];
					for (int index = 0; index < parameters.Count; index++)
						parameterArray[index] = (ParameterDeclaration)parameters[index];
					return parameterArray;
				}
				return null;
			}
		}
		
		/// <summary>
		/// Gets a <see cref="IDomTypeReference"/> to the type that is returned by the member.
		/// </summary>
		/// <value>A <see cref="IDomTypeReference"/> to the type that is returned by the member.</value>
		IDomTypeReference IDomMember.ReturnType { 
			get {
				return this.ReturnType;
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
		/// Gets or sets the getter <see cref="InterfaceAccessor"/>.
		/// </summary>
		/// <value>The getter <see cref="InterfaceAccessor"/>.</value>
		public InterfaceAccessor GetAccessor {
			get {
				return this.GetChildNode(InterfacePropertyDeclaration.GetAccessorContextID) as InterfaceAccessor;
			}
			set {
				this.ChildNodes.Replace(value, InterfacePropertyDeclaration.GetAccessorContextID);
			}
		}
		
		/// <summary>
		/// Returns display text that represents the AST node using the specified options.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> to use for formatting the text.</param>
		/// <param name="detailLevel">A <see cref="DisplayTextDetailLevel"/> indicating the desired level of detail.</param>
		/// <returns>The display text that represents the AST node using the specified options.</returns>
		/// <remarks>
		/// This method is useful for getting text to display for the node for use in a type/member drop-down list or class browser.
		/// </remarks>
		public override string GetDisplayText(DotNetLanguage language, DisplayTextDetailLevel detailLevel) {
			if (this.IsIndexer) {
				StringBuilder text = new StringBuilder();
				text.Append("this[");
				AstNode.AppendParametersToDisplayText(language, detailLevel, text, this.Parameters);
				text.Append("]");
				return text.ToString();
			}
			else if (this.Name != null)
				return this.Name.Text;
			else
				return "?";
		}
		
		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		public override int ImageIndex {
			get {
				return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicProperty;
			}
		}
		
		/// <summary>
		/// Gets whether this property is an indexer.
		/// </summary>
		/// <value>
		/// <c>true</c> if this property is an indexer; otherwise, <c>false</c>.
		/// </value>
		public bool IsIndexer {
			get {
				return (this.Name == null) || (this.Parameters.Count > 0);
			}
		}
		
		/// <summary>
		/// Gets whether the member is static.
		/// </summary>
		/// <value>
		/// <c>true</c> if the member is static; otherwise, <c>false</c>.
		/// </value>
		public bool IsStatic { 
			get {
				return ((this.Modifiers & Modifiers.Static) == Modifiers.Static);
			}
		}
		
		/// <summary>
		/// Gets the string-based key that uniquely identifies the <see cref="AstNode"/>.
		/// </summary>
		/// <value>The string-based key that uniquely identifies the <see cref="AstNode"/>.</value>
		public override string Key {
			get {
				return DomDocumentationProvider.GetMemberDocumentationKey(this);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.InterfacePropertyDeclaration;
			}
		}
		
		/// <summary>
		/// Gets the collection of parameters.
		/// </summary>
		/// <value>The collection of parameters.</value>
		public IAstNodeList Parameters {
			get {
				return new AstNodeListWrapper(this, InterfacePropertyDeclaration.ParameterContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the return type.
		/// </summary>
		/// <value>The return type.</value>
		public TypeReference ReturnType {
			get {
				return this.GetChildNode(InterfacePropertyDeclaration.ReturnTypeContextID) as TypeReference;
			}
			set {
				this.ChildNodes.Replace(value, InterfacePropertyDeclaration.ReturnTypeContextID);
			}
		}

		/// <summary>
		/// Gets or sets the setter <see cref="InterfaceAccessor"/>.
		/// </summary>
		/// <value>The setter <see cref="InterfaceAccessor"/>.</value>
		public InterfaceAccessor SetAccessor {
			get {
				return this.GetChildNode(InterfacePropertyDeclaration.SetAccessorContextID) as InterfaceAccessor;
			}
			set {
				this.ChildNodes.Replace(value, InterfacePropertyDeclaration.SetAccessorContextID);
			}
		}

	}
}
