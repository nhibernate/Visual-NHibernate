using System;
using System.Collections;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a type member declaration, such as a field, method, property, constructor, or event.
	/// </summary>
	public abstract class TypeMemberDeclaration : AstNode {

		private string					documentation;
		private Modifiers				modifiers;
		
		/// <summary>
		/// Gets the context ID for an attribute section AST node.
		/// </summary>
		/// <value>The context ID for an attribute section AST node.</value>
		public const byte AttributeSectionContextID = AstNode.AstNodeContextIDBase;

		/// <summary>
		/// Gets the context ID for a name AST node.
		/// </summary>
		/// <value>The context ID for a name AST node.</value>
		public const byte NameContextID = AstNode.AstNodeContextIDBase + 1;
		
		/// <summary>
		/// Gets the context ID for a comment AST node.
		/// </summary>
		/// <value>The context ID for a comment AST node.</value>
		public const byte CommentContextID = AstNode.AstNodeContextIDBase + 2;

		/// <summary>
		/// Gets the minimum context ID that should be used in your code for AST nodes inheriting this class.
		/// </summary>
		/// <value>The minimum context ID that should be used in your code for AST nodes inheriting this class.</value>
		/// <remarks>
		/// Base all your context ID constants off of this value.
		/// </remarks>
		protected const byte TypeMemberDeclarationContextIDBase = AstNode.AstNodeContextIDBase + 3;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>TypeMemberDeclaration</c> class. 
		/// </summary>
		/// <param name="modifiers">The modifiers.</param>
		/// <param name="name">The name.</param>
		public TypeMemberDeclaration(Modifiers modifiers, QualifiedIdentifier name) {
			// Initialize parameters
			this.modifiers	= modifiers;
			this.Name		= name;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the access-related values of <see cref="Modifiers"/>.
		/// </summary>
		/// <value>The access-related values of <see cref="Modifiers"/>.</value>
		public Modifiers AccessModifiers {
			get {
				// Get the access modifiers
				Modifiers accessModifiers = modifiers & Modifiers.AccessMask;

				// Ensure there is an access modifier
				if (accessModifiers == Modifiers.None)
					accessModifiers = Modifiers.Private;

				return accessModifiers;
			}
		}

		/// <summary>
		/// Gets the collection of attribute sections.
		/// </summary>
		/// <value>The collection of attribute sections.</value>
		public IAstNodeList AttributeSections {
			get {
				return new AstNodeListWrapper(this, TypeMemberDeclaration.AttributeSectionContextID);
			}
		}
		
		/// <summary>
		/// Gets the collection of comments that appear at the end of the node.
		/// </summary>
		/// <value>The collection of comments that appear at the end of the node.</value>
		public IAstNodeList Comments {
			get {
				return new AstNodeListWrapper(this, TypeMemberDeclaration.CommentContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the documentation.
		/// </summary>
		/// <value>The documentation.</value>
		public string Documentation { 
			get {
				return documentation;
			}
			set {
				documentation = value;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DomDocumentationProvider"/> for the type.
		/// </summary>
		/// <value>The <see cref="DomDocumentationProvider"/> for the type.</value>
		public DomDocumentationProvider DocumentationProvider {
			get {
				return new DomDocumentationProvider(documentation);
			}
		}
		
		/// <summary>
		/// Gets whether the type has an <c>EditorBrowsableAttribute</c> on it with a value of <c>Never</c>.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type has an <c>EditorBrowsableAttribute</c> on it with a value of <c>Never</c>; otherwise, <c>false</c>.
		/// </value>
		public bool IsEditorBrowsableNever { 
			get {
				// Look for "EditorBrowsable(EditorBrowsableState.Never)"
				IAstNodeList sections = this.AttributeSections;
				int sectionCount = sections.Count;
				for (int sectionIndex = 0; sectionIndex < sectionCount; sectionIndex++) {
					AttributeSection section = sections[sectionIndex] as AttributeSection;
					if (section != null) {
						IAstNodeList attributes = section.Attributes;
						int attributeCount = attributes.Count;
						for (int attributeIndex = 0; attributeIndex < attributeCount; attributeIndex++) {
							Attribute attribute = attributes[attributeIndex] as Attribute;
							if ((attribute != null) && (attribute.AttributeType != null)) {
								switch (DotNetProjectResolver.GetTypeName(attribute.AttributeType.Name)) {
									case "EditorBrowsable":
									case "EditorBrowsableAttribute":
										if (attribute.Arguments.Count == 1) {
											AttributeArgument argument = attribute.Arguments[0] as AttributeArgument;
											if (argument != null) {
												MemberAccess memberAccess = argument.Expression as MemberAccess;
												return (memberAccess != null) && (memberAccess.MemberName != null) && (memberAccess.MemberName.Text == "Never");
											}
										}
										break;
								}
							}
						}
					}
				}
				return false;
			}
		}
		
		/// <summary>
		/// Gets whether the type is marked with an <c>ExtensionAttribute</c>.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type is marked with an <c>ExtensionAttribute</c>; otherwise, <c>false</c>.
		/// </value>
		public bool IsExtension { 
			get {
				// Only static types/members can be an extension
				if ((modifiers & Modifiers.Static) == 0)
					return false;

				// Look for "ExtensionAttribute()"
				IAstNodeList sections = this.AttributeSections;
				int sectionCount = sections.Count;
				for (int sectionIndex = 0; sectionIndex < sectionCount; sectionIndex++) {
					AttributeSection section = sections[sectionIndex] as AttributeSection;
					if (section != null) {
						IAstNodeList attributes = section.Attributes;
						int attributeCount = attributes.Count;
						for (int attributeIndex = 0; attributeIndex < attributeCount; attributeIndex++) {
							Attribute attribute = attributes[attributeIndex] as Attribute;
							if ((attribute != null) && (attribute.AttributeType != null)) {
								switch (DotNetProjectResolver.GetTypeName(attribute.AttributeType.Name)) {
									case "Extension":
									case "ExtensionAttribute":
										return true;
								}
							}
						}
					}
				}
				return false;
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
		/// Gets or sets the name of the namespace.
		/// </summary>
		/// <value>The name of the namespace.</value>
		public QualifiedIdentifier Name {
			get {
				return this.GetChildNode(TypeMemberDeclaration.NameContextID) as QualifiedIdentifier;
			}
			set {
				this.ChildNodes.Replace(value, TypeMemberDeclaration.NameContextID);
			}
		}

		/// <summary>
		/// Gets the character offset at which to navigate when the editor's caret should jump to the text representation of the AST node.
		/// </summary>
		/// <value>The character offset at which to navigate when the editor's caret should jump to the text representation of the AST node.</value>
		public override int NavigationOffset {
			get {
				QualifiedIdentifier name = this.Name;
				if ((name != null) && (name.HasStartOffset))
					return name.NavigationOffset;
				else
					return base.NavigationOffset;
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
		/// Converts the object to a <c>String</c>.
		/// </summary>
		/// <returns>
		/// A string whose value represents this object.
		/// </returns>
		public override string ToString() {
			return base.ToString() + (this.Name != null ? ": Name=" + this.Name.Text : String.Empty);
		}
		
	}
}
