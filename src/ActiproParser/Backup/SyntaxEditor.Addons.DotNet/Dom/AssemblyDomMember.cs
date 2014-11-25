using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Xml;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents a .NET member that is defined in an assembly.
	/// </summary>
	internal class AssemblyDomMember : IDomMember {

		//
		// NOTE: Any changes made to fields need to be persisted to the cache in AssemblyProjectContent and the cache version number must be incremented
		//

		private IDomType			declaringType;
		private DomMemberFlags		memberFlags				= DomMemberFlags.None;
		private Modifiers			modifiers;
		private string				name;
		private IDomTypeReference	returnType;
				
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDomMember</c> class.
		/// </summary>
		/// <param name="declaringType">The <see cref="IDomType"/> that declared the member.</param>
		/// <param name="memberFlags">The <see cref="DomMemberFlags"/> for the member.</param>
		/// <param name="name">The name of the member.</param>
		/// <param name="modifiers">The <see cref="Modifiers"/> for the member.</param>
		/// <remarks>This overload should only be used when reading a cache file.</remarks>
		internal AssemblyDomMember(IDomType declaringType, DomMemberFlags memberFlags, string name, Modifiers modifiers) {
			// Initialize parameters
			this.declaringType	= declaringType;
			this.memberFlags	= memberFlags;
			this.name			= name;
			this.modifiers		= modifiers;
		}

		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDomMember</c> class.
		/// </summary>
		/// <param name="projectContent">The <see cref="AssemblyProjectContent"/> that defines the member.</param>
		/// <param name="declaringType">The <see cref="IDomType"/> that declared the member.</param>
		/// <param name="memberInfo">The <see cref="MemberInfo"/> to wrap with this object.</param>
		internal AssemblyDomMember(AssemblyProjectContent projectContent, IDomType declaringType, MemberInfo memberInfo) {
			// Initialize parameters
			this.declaringType	= declaringType;
			memberFlags			= DomMemberFlags.None;
			name				= memberInfo.Name;
			modifiers			= Modifiers.None;

			MethodBase methodInfo = null;
			switch (memberInfo.MemberType) {
				case MemberTypes.Constructor: {
					methodInfo = (ConstructorInfo)memberInfo;
					memberFlags |= DomMemberFlags.Constructor;
					break;
				}
				case MemberTypes.Event: {
					methodInfo = ((EventInfo)memberInfo).GetAddMethod();
					if (methodInfo == null)
						methodInfo = ((EventInfo)memberInfo).GetRemoveMethod();
					memberFlags |= DomMemberFlags.Event;

					returnType = projectContent.GetTypeReference(((EventInfo)memberInfo).EventHandlerType);
					break;
				}
				case MemberTypes.Field:
					FieldInfo fieldInfo = (FieldInfo)memberInfo;
					memberFlags |= (fieldInfo.IsLiteral ? DomMemberFlags.Constant : DomMemberFlags.Field);
					returnType = projectContent.GetTypeReference(fieldInfo.FieldType);

					if (fieldInfo.IsFamily) 
						modifiers |= Modifiers.Family;					
					else if (fieldInfo.IsFamilyOrAssembly)
						modifiers |= Modifiers.FamilyOrAssembly;					
					else if (fieldInfo.IsPublic)
						modifiers |= Modifiers.Public;					
					if (fieldInfo.IsLiteral)
						modifiers |= Modifiers.Const;					
					if (fieldInfo.IsStatic)
						modifiers |= Modifiers.Static;					
					if (fieldInfo.IsInitOnly)
						modifiers |= Modifiers.ReadOnly;					
					break;
				case MemberTypes.Method: {
					methodInfo = (MethodInfo)memberInfo;
					memberFlags |= DomMemberFlags.Method;
					returnType = projectContent.GetTypeReference(((MethodInfo)methodInfo).ReturnType);

					// Get generic flags
					#if !NET11
					if (methodInfo.IsGenericMethodDefinition)
						memberFlags |= DomMemberFlags.GenericMethodDefinition;
					if (methodInfo.IsGenericMethod)
						memberFlags |= DomMemberFlags.GenericMethod;
					#endif
					break;
				}
				case MemberTypes.Property: {
					PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
					if (propertyInfo.CanRead) {
						try {
							methodInfo = propertyInfo.GetGetMethod(true);
						} catch {}
					}
					if ((methodInfo == null) && (propertyInfo.CanWrite)) {
						try {
							methodInfo = propertyInfo.GetSetMethod(true);
						} catch {}
					}
					memberFlags |= DomMemberFlags.Property;

					if ((propertyInfo.CanRead) && (!propertyInfo.CanWrite))
						modifiers |= Modifiers.ReadOnly;					
					else if ((!propertyInfo.CanRead) && (propertyInfo.CanWrite))
						modifiers |= Modifiers.WriteOnly;		

					returnType = projectContent.GetTypeReference(propertyInfo.PropertyType);
					break;
				}
				default:
					memberFlags |= DomMemberFlags.Custom;
					break;
			}
			if (methodInfo != null) {
				// Update modifiers
				if (methodInfo.IsFamily) 
					modifiers |= Modifiers.Family;					
				else if (methodInfo.IsFamilyOrAssembly)
					modifiers |= Modifiers.FamilyOrAssembly;					
				else if (methodInfo.IsPublic)
					modifiers |= Modifiers.Public;					
				if (methodInfo.IsAbstract)
					modifiers |= Modifiers.Abstract;					
				if (methodInfo.IsStatic)
					modifiers |= Modifiers.Static;					
				if (methodInfo.IsVirtual)
					modifiers |= Modifiers.Virtual;					
			}
			
			// Get custom attributes
			IEnumerable attributes = null;
			try {
				#if NET11
				attributes = memberInfo.GetCustomAttributes(true);
				#else
				attributes = CustomAttributeData.GetCustomAttributes(memberInfo);
				#endif
			}
			catch {}

			// Look for other flags in attributes
			if (attributes != null) {
				foreach (object attribute in attributes) {
					switch (attribute.ToString()) {
						case "[System.ComponentModel.EditorBrowsableAttribute((System.ComponentModel.EditorBrowsableState)1)]":
							memberFlags |= DomMemberFlags.IsEditorBrowsableNever;
							break;
						case "[System.Runtime.CompilerServices.ExtensionAttribute()]":
							memberFlags |= DomMemberFlags.IsExtension;
							break;
					}
				}
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns whether the specified <see cref="DomMemberFlags"/> flag is set.
		/// </summary>
		/// <param name="memberFlags">The <see cref="DomMemberFlags"/> value to examine.</param>
		/// <param name="flag">The <see cref="DomMemberFlags"/> to check for.</param>
		/// <returns>
		/// <c>true</c> if the specified <see cref="DomMemberFlags"/> flag is set; otherwise, <c>false</c>.
		/// </returns>
		internal static bool HasMemberFlag(DomMemberFlags memberFlags, DomMemberFlags flag) {
			return ((memberFlags & flag) == flag);
		}
		
		/// <summary>
		/// Sets the value of the <see cref="ReturnType"/> property.
		/// </summary>
		/// <param name="value">The value to set.</param>
		internal void SetReturnType(IDomTypeReference value) {
			returnType = value;
		}
		
		/// <summary>
		/// Sets or clears the specified <see cref="DomMemberFlags"/> flag.
		/// </summary>
		/// <param name="memberFlags">The <see cref="DomMemberFlags"/> value to update.</param>
		/// <param name="flag">The <see cref="DomMemberFlags"/> to set or clear.</param>
		/// <param name="setBit">Whether to set the flag; otherwise, the flag is cleared.</param>
		/// <returns>The updated <see cref="DomMemberFlags"/> value.</returns>
		internal static DomMemberFlags SetMemberFlag(DomMemberFlags memberFlags, DomMemberFlags flag, bool setBit) {
			if (setBit)
				return memberFlags | flag;
			else
				return memberFlags & (~flag);
		}
		
		/// <summary>
		/// Gets or sets the <see cref="DomMemberFlags"/> for the member.
		/// </summary>
		/// <value>The <see cref="DomMemberFlags"/> for the member.</value>
		internal DomMemberFlags MemberFlags {
			get {
				return memberFlags;
			}
			set {
				memberFlags = value;
			}
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
				return modifiers & Modifiers.AccessMask;
			}
		}

		/// <summary>
		/// Gets a <see cref="IDomTypeReference"/> to the type that declares the member.
		/// </summary>
		/// <value>A <see cref="IDomTypeReference"/> to the type that declares the member.</value>
		public IDomTypeReference DeclaringType {
			get {
				return declaringType;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DomDocumentationProvider"/> for the member.
		/// </summary>
		/// <value>The <see cref="DomDocumentationProvider"/> for the member.</value>
		public DomDocumentationProvider DocumentationProvider {
			get {
				if (declaringType.ProjectContent is AssemblyProjectContent) {
					AssemblyDocumentation documentation = ((AssemblyProjectContent)declaringType.ProjectContent).Documentation;
					if (documentation != null)
						return new DomDocumentationProvider(documentation.GetDocumentation(DomDocumentationProvider.GetMemberDocumentationKey(this)));
				}

				return new DomDocumentationProvider(null);
			}
		}
		
		/// <summary>
		/// Gets the type arguments if this is a generic method definition.
		/// </summary>
		/// <value>The type arguments if this is a generic method definition.</value>
		public virtual ICollection GenericTypeArguments { 
			get {
				return null;
			}
		}
		
		/// <summary>
		/// Returns the reflection image index for the specified member.
		/// </summary>
		/// <param name="memberType">The <see cref="DomMemberType"/> indicating the type of member.</param>
		/// <param name="accessModifiers">A <see cref="Modifiers"/> indicating the access modifiers.</param>
		/// <param name="isExtension">Whether the member is an extension.</param>
		/// <returns>The reflection image index for the specified member.</returns>
		public static int GetReflectionImageIndex(DomMemberType memberType, Modifiers accessModifiers, bool isExtension) {
			switch (memberType) {
				case DomMemberType.Constant:
					switch (accessModifiers) {
						case Modifiers.Public:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicConstant;
						case Modifiers.Assembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.InternalConstant;
						case Modifiers.Family:
						case Modifiers.FamilyOrAssembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.ProtectedConstant;
						default:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PrivateConstant;
					}
				case DomMemberType.Event:
					switch (accessModifiers) {
						case Modifiers.Public:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicEvent;
						case Modifiers.Assembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.InternalEvent;
						case Modifiers.Family:
						case Modifiers.FamilyOrAssembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.ProtectedEvent;
						default:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PrivateEvent;
					}
				case DomMemberType.Field:
					switch (accessModifiers) {
						case Modifiers.Public:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField;
						case Modifiers.Assembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.InternalField;
						case Modifiers.Family:
						case Modifiers.FamilyOrAssembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.ProtectedField;
						default:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PrivateField;
					}
				case DomMemberType.Property:
					switch (accessModifiers) {
						case Modifiers.Public:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicProperty;
						case Modifiers.Assembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.InternalProperty;
						case Modifiers.Family:
						case Modifiers.FamilyOrAssembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.ProtectedProperty;
						default:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PrivateProperty;
					}
				default:
					if (isExtension) {
						switch (accessModifiers) {
							case Modifiers.Public:
								return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicExtensionMethod;
							case Modifiers.Assembly:
								return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.InternalExtensionMethod;
							case Modifiers.Family:
							case Modifiers.FamilyOrAssembly:
								return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.ProtectedExtensionMethod;
							default:
								return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PrivateExtensionMethod;
						}
					}
					else {
						switch (accessModifiers) {
							case Modifiers.Public:
								return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicMethod;
							case Modifiers.Assembly:
								return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.InternalMethod;
							case Modifiers.Family:
							case Modifiers.FamilyOrAssembly:
								return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.ProtectedMethod;
							default:
								return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PrivateMethod;
						}
					}
			}
		}
		
		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		public int ImageIndex {
			get {
				if (declaringType.Type == DomTypeType.Enumeration)
					return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.EnumerationItem;
				else
					return AssemblyDomMember.GetReflectionImageIndex(this.MemberType, this.AccessModifiers, this.IsExtension);
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
				return AssemblyDomMember.HasMemberFlag(memberFlags, DomMemberFlags.IsEditorBrowsableNever);
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
				return AssemblyDomMember.HasMemberFlag(memberFlags, DomMemberFlags.IsExtension);
			}
		}
		
		/// <summary>
		/// Gets whether the member is a generic method.
		/// </summary>
		/// <value>
		/// <c>true</c> if the method is a generic method; otherwise, <c>false</c>.
		/// </value>
		public bool IsGenericMethod { 
			get {
				return AssemblyDomMember.HasMemberFlag(memberFlags, DomMemberFlags.GenericMethod);
			}
		}

		/// <summary>
		/// Gets whether the member is a generic method definition, from which other generic methods can be constructed.
		/// </summary>
		/// <value>
		/// <c>true</c> if the method is a generic method definition, from which other generic methods can be constructed; otherwise, <c>false</c>.
		/// </value>
		public bool IsGenericMethodDefinition { 
			get {
				return AssemblyDomMember.HasMemberFlag(memberFlags, DomMemberFlags.GenericMethodDefinition);
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
				return ((modifiers & Modifiers.Static) == Modifiers.Static);
			}
		}
		
		/// <summary>
		/// Gets a <see cref="DomMemberType"/> that indicates the type of member.
		/// </summary>
		/// <value>A <see cref="DomMemberType"/> that indicates the type of member.</value>
		public DomMemberType MemberType {
			get {
				return (DomMemberType)(memberFlags & DomMemberFlags.MemberTypesMask);
			}
		}

		/// <summary>
		/// Gets the modifiers for the member.
		/// </summary>
		/// <value>The modifiers for the member.</value>
		public Modifiers Modifiers {
			get {
				return modifiers;
			}
			protected set {
				modifiers = value;
			}
		}

		/// <summary>
		/// Gets the name of the type.
		/// </summary>
		/// <value>The name of the type.</value>
		public string Name {
			get {
				return name;
			}
		}
		
		/// <summary>
		/// Gets the array of <see cref="IDomParameter"/> parameters for the member.
		/// </summary>
		/// <value>The array of <see cref="IDomParameter"/> parameters for the member.</value>
		public virtual IDomParameter[] Parameters {
			get {
				return null;
			}
		}
		
		/// <summary>
		/// Resolve all references to types in the same assembly to the actual type.
		/// </summary>
		/// <param name="projectContent">The <see cref="AssemblyProjectContent"/> to use for resolution.</param>
		protected internal virtual void ResolveTypePlaceHolders(AssemblyProjectContent projectContent) {
			if (returnType is AssemblyDomTypePlaceHolder)
				returnType = projectContent.ResolveAssemblyDomTypePlaceHolder((AssemblyDomTypePlaceHolder)returnType);
			else if (returnType is AssemblyDomTypeReference)
				((AssemblyDomTypeReference)returnType).ResolveTypePlaceHolders(projectContent);
		}

		/// <summary>
		/// Gets a <see cref="IDomTypeReference"/> to the type that is returned by the member.
		/// </summary>
		/// <value>A <see cref="IDomTypeReference"/> to the type that is returned by the member.</value>
		public IDomTypeReference ReturnType { 
			get {
				return returnType;
			}
		}
		
		/// <summary>
		/// Converts the object to a <c>String</c>.
		/// </summary>
		/// <returns>
		/// A string whose value represents this object.
		/// </returns>
		public override string ToString() {
			return String.Format("AssemblyDomMember[{0}.{1}]", declaringType.FullName, name);
		}

	}
}
