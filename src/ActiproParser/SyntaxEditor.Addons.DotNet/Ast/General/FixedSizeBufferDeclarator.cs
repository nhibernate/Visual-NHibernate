using System;
using System.Collections;
using System.Reflection;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a fixed-size buffer declarator.
	/// </summary>
	public class FixedSizeBufferDeclarator : AstNode, IDomMember {
		
		/// <summary>
		/// Gets the context ID for a return type AST node.
		/// </summary>
		/// <value>The context ID for a return type AST node.</value>
		public const byte ReturnTypeContextID = AstNode.AstNodeContextIDBase;

		/// <summary>
		/// Gets the context ID for a name AST node.
		/// </summary>
		/// <value>The context ID for a name AST node.</value>
		public const byte NameContextID = AstNode.AstNodeContextIDBase + 1;
		
		/// <summary>
		/// Gets the context ID for a size expression AST node.
		/// </summary>
		/// <value>The context ID for a size expression AST node.</value>
		public const byte SizeExpressionContextID = AstNode.AstNodeContextIDBase + 2;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>FixedSizeBufferDeclarator</c> class. 
		/// </summary>
		/// <param name="returnType">The return type.</param>
		/// <param name="name">The name.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public FixedSizeBufferDeclarator(TypeReference returnType, QualifiedIdentifier name, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.ReturnType	= returnType;
			this.Name		= name;
		}
		
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
		/// Gets whether the type has an <c>EditorBrowsableAttribute</c> on it with a value of <c>Never</c>.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type has an <c>EditorBrowsableAttribute</c> on it with a value of <c>Never</c>; otherwise, <c>false</c>.
		/// </value>
		bool IDomMember.IsEditorBrowsableNever { 
			get {
				return false;
			}
		}
		
		/// <summary>
		/// Gets whether the type is marked with an <c>ExtensionAttribute</c>.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type is marked with an <c>ExtensionAttribute</c>; otherwise, <c>false</c>.
		/// </value>
		bool IDomMember.IsExtension { 
			get {
				return false;
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
				return DomMemberType.Field;
			}
		}

		/// <summary>
		/// Gets the name of the member.
		/// </summary>
		/// <value>The name of the member.</value>
		string IDomMember.Name { 
			get {
				QualifiedIdentifier name = this.Name;
				if (name != null)
					return name.Text;
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
		/// Gets the access-related values of <see cref="Modifiers"/>.
		/// </summary>
		/// <value>The access-related values of <see cref="Modifiers"/>.</value>
		public Modifiers AccessModifiers {
			get {
				// Get the access modifiers
				Modifiers accessModifiers = this.Modifiers & Modifiers.AccessMask;

				// Ensure there is an access modifier
				if (accessModifiers == Modifiers.None)
					accessModifiers = Modifiers.Private;

				return accessModifiers;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DomDocumentationProvider"/> for the member.
		/// </summary>
		/// <value>The <see cref="DomDocumentationProvider"/> for the member.</value>
		public DomDocumentationProvider DocumentationProvider {
			get {
				if (this.ParentNode is TypeMemberDeclaration)
					return ((TypeMemberDeclaration)this.ParentNode).DocumentationProvider;
				else
					return null;
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
			QualifiedIdentifier name = this.Name;
			if (name != null)
				return name.Text;
			else
				return "?";
		}
		
		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		public override int ImageIndex {
			get {
				switch (((FixedSizeBufferDeclaration)this.ParentNode).AccessModifiers) {
					case Modifiers.Private:
						return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PrivateField;
					case Modifiers.Assembly:
						return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.InternalField;
					case Modifiers.Family:
					case Modifiers.FamilyOrAssembly:
						return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.ProtectedField;
					default:
						return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField;
				}
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
				if (this.ParentNode is FixedSizeBufferDeclaration)
					return ((((FixedSizeBufferDeclaration)this.ParentNode).Modifiers & Modifiers.Static) == Modifiers.Static);
				else
					return false;				
			}
		}
		
		/// <summary>
		/// Gets the string-based key that uniquely identifies the <see cref="AstNode"/>.
		/// </summary>
		/// <value>The string-based key that uniquely identifies the <see cref="AstNode"/>.</value>
		public override string Key {
			get {
				QualifiedIdentifier name = this.Name;
				if (name != null) {
					if ((this.ParentNode != null) && (this.ParentNode.ParentNode != null) && (this.ParentNode.ParentNode is TypeDeclaration))
						return "F:" + ((TypeDeclaration)this.ParentNode.ParentNode).FullName + "." + name.Text;
					else
						return "F:" + name.Text;
				}
				else
					return base.Key;					
			}
		}
		
		/// <summary>
		/// Gets the modifiers for the member.
		/// </summary>
		/// <value>The modifiers for the member.</value>
		public Modifiers Modifiers {
			get {
				if (this.ParentNode is FixedSizeBufferDeclaration)
					return ((FixedSizeBufferDeclaration)this.ParentNode).Modifiers;
				else
					return Modifiers.None;				
			}
		}

		/// <summary>
		/// Gets or sets the name of the namespace.
		/// </summary>
		/// <value>The name of the namespace.</value>
		public QualifiedIdentifier Name {
			get {
				return this.GetChildNode(FixedSizeBufferDeclarator.NameContextID) as QualifiedIdentifier;
			}
			set {
				this.ChildNodes.Replace(value, FixedSizeBufferDeclarator.NameContextID);
			}
		}

		/// <summary>
		/// Gets an <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.
		/// </summary>
		/// <value>An <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.</value>
		public override DotNetNodeCategory NodeCategory {
			get {
				return DotNetNodeCategory.TypeMemberDeclaration;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.FixedSizeBufferDeclarator;
			}
		}
		
		/// <summary>
		/// Gets or sets the return type.
		/// </summary>
		/// <value>The return type.</value>
		public TypeReference ReturnType {
			get {
				return this.GetChildNode(FixedSizeBufferDeclarator.ReturnTypeContextID) as TypeReference;
			}
			set {
				this.ChildNodes.Replace(value, FixedSizeBufferDeclarator.ReturnTypeContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the size <see cref="Expression"/>.
		/// </summary>
		/// <value>The size <see cref="Expression"/>.</value>
		public Expression SizeExpression {
			get {
				return this.GetChildNode(FixedSizeBufferDeclarator.SizeExpressionContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, FixedSizeBufferDeclarator.SizeExpressionContextID);
			}
		}
		
	}
}
