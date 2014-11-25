using System;
using System.Collections;
using System.Reflection;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a variable declarator.
	/// </summary>
	public class VariableDeclarator : AstNode, IDomMember {

		private bool					isConstant;
		private bool					isImplicitlyTyped;
		private bool					isLocal;
		
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
		/// Gets the context ID for an initializer AST node.
		/// </summary>
		/// <value>The context ID for an initializer AST node.</value>
		public const byte InitializerContextID = AstNode.AstNodeContextIDBase + 2;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>VariableDeclarator</c> class. 
		/// </summary>
		/// <param name="returnType">The return type.</param>
		/// <param name="name">The name.</param>
		/// <param name="isConstant">Whether the declarator is for a constant variable.</param>
		/// <param name="isLocal">Whether the declarator is for a local variable.</param>
		public VariableDeclarator(TypeReference returnType, QualifiedIdentifier name, bool isConstant, bool isLocal) : 
			base(name != null ? name.TextRange : (returnType != null ? returnType.TextRange : TextRange.Deleted)) {

			// Initialize parameters
			this.ReturnType	= returnType;
			this.Name		= name;
			this.isConstant = isConstant;
			this.isLocal	= isLocal;
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
				return (isConstant ? DomMemberType.Constant : DomMemberType.Field);
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
				if (isConstant) {
					if (this.ParentNode is FieldDeclaration)
						return AssemblyDomMember.GetReflectionImageIndex(DomMemberType.Constant, ((FieldDeclaration)this.ParentNode).AccessModifiers, false);
					else
						return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicConstant;
				}
				else {
					if (this.ParentNode is FieldDeclaration)
						return AssemblyDomMember.GetReflectionImageIndex(DomMemberType.Field, ((FieldDeclaration)this.ParentNode).AccessModifiers, false);
					else
						return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField;
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the initializer <see cref="Expression"/>.
		/// </summary>
		/// <value>The initializer <see cref="Expression"/>.</value>
		public Expression Initializer {
			get {
				return this.GetChildNode(VariableDeclarator.InitializerContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, VariableDeclarator.InitializerContextID);
			}
		}

		/// <summary>
		/// Gets or sets whether the declarator is for a constant variable.
		/// </summary>
		/// <value>
		/// <c>true</c> if the declarator is for a constant variable; otherwise, <c>false</c>.
		/// </value>
		public bool IsConstant {
			get {
				return isConstant;
			}
			set {
				isConstant = value;
			}
		}
		
		/// <summary>
		/// Gets or sets whether the declarator is implicitly typed.
		/// </summary>
		/// <value>
		/// <c>true</c> if the declarator is implicitly typed; otherwise, <c>false</c>.
		/// </value>
		public bool IsImplicitlyTyped {
			get {
				return isImplicitlyTyped;
			}
			set {
				isImplicitlyTyped = value;
			}
		}
		
		/// <summary>
		/// Gets or sets whether the declarator is for a local variable.
		/// </summary>
		/// <value>
		/// <c>true</c> if the declarator is for a local variable; otherwise, <c>false</c>.
		/// </value>
		public bool IsLocal {
			get {
				return isLocal;
			}
			set {
				isLocal = value;
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
				if (isConstant) 
					return true;
				else if (this.ParentNode is FieldDeclaration)
					return ((((FieldDeclaration)this.ParentNode).Modifiers & Modifiers.Static) == Modifiers.Static);
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
				if ((!isLocal) && (name != null)) {
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
				if (this.ParentNode is FieldDeclaration)
					return ((FieldDeclaration)this.ParentNode).Modifiers;
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
				return this.GetChildNode(VariableDeclarator.NameContextID) as QualifiedIdentifier;
			}
			set {
				this.ChildNodes.Replace(value, VariableDeclarator.NameContextID);
			}
		}

		/// <summary>
		/// Gets an <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.
		/// </summary>
		/// <value>An <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.</value>
		public override DotNetNodeCategory NodeCategory {
			get {
				if (!isLocal)
					return DotNetNodeCategory.TypeMemberDeclaration;
				else
					return base.NodeCategory;
			}
		}
		
		/// <summary>
		/// Gets or sets the return type.
		/// </summary>
		/// <value>The return type.</value>
		public TypeReference ReturnType {
			get {
				return this.GetChildNode(VariableDeclarator.ReturnTypeContextID) as TypeReference;
			}
			set {
				this.ChildNodes.Replace(value, VariableDeclarator.ReturnTypeContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.VariableDeclarator;
			}
		}

	}
}
