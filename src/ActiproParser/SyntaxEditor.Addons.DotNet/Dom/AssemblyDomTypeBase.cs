using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents the abstract base class of a .NET type or type reference that is defined in an assembly.
	/// </summary>
	internal abstract class AssemblyDomTypeBase : IDomTypeReference {
		
		//
		// NOTE: Any changes made to fields need to be persisted to the cache in AssemblyProjectContent and the cache version number must be incremented
		//

		private byte					assemblyIndex						= byte.MaxValue;
		private string					defaultMemberName;					// Only used when first loading
		private IDomTypeReference[]		genericTypeArguments;
		private Modifiers				modifiers;
		private string					name;
		private string					@namespace;
		private AssemblyProjectContent	projectContent;
		private DomTypeFlags			typeFlags							= DomTypeFlags.None;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDomTypeBase</c> class.
		/// </summary>
		/// <param name="projectContent">The <see cref="IProjectContent"/> that contains the type reference.</param>
		/// <remarks>This overload should only be used when reading a cache file.</remarks>
		internal AssemblyDomTypeBase(AssemblyProjectContent projectContent) {
			// Initialize parameters
			this.projectContent	= projectContent;
		}

		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDomTypeBase</c> class.
		/// </summary>
		/// <param name="projectContent">The <see cref="AssemblyProjectContent"/> that contains the type reference.</param>
		/// <param name="callingGenericTypes">The calling generic type array, used to prevent infinite recursion with generic constraints on a generic.</param>
		/// <param name="type">The <see cref="Type"/> to wrap with this object.</param>
		internal AssemblyDomTypeBase(AssemblyProjectContent projectContent, IDomTypeReference[] callingGenericTypes, Type type) {
			// Initialize parameters
			this.projectContent	= projectContent;
			name				= DotNetProjectResolver.GetTypeNameWithoutArrayPointerSpec(type.Name);
			@namespace			= type.Namespace;

			// Get custom attributes
			IEnumerable attributes = null;
			try {
				#if NET11
				attributes = type.GetCustomAttributes(true);
				#else
				attributes = CustomAttributeData.GetCustomAttributes(type);
				#endif
			}
			catch {}

			// Determine type type
			if (type.IsEnum)
				typeFlags |= DomTypeFlags.Enumeration;
			else if (type.IsInterface)
				typeFlags |= DomTypeFlags.Interface;
			else if (type.IsValueType)
				typeFlags |= DomTypeFlags.Structure;
			else if ((type.IsSubclassOf(typeof(Delegate))) && (type != typeof(MulticastDelegate)))
				typeFlags |= DomTypeFlags.Delegate;
			else {
				// See if the type is a standard module
				if (attributes != null) {
					foreach (object attribute in attributes) {
						if (attribute.ToString().IndexOf("StandardModule") != -1) {  // [Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute()]
							typeFlags |= DomTypeFlags.StandardModule;
							break;
						}
					}
				}
			}

			// Determine whether the type is nested
			if ((type.FullName != null) && (type.FullName.IndexOf('+') != -1))
				typeFlags |= DomTypeFlags.Nested;

			// Look for other flags in attributes
			if (attributes != null) {
				foreach (object attribute in attributes) {
					switch (attribute.ToString()) {
						case "[System.ComponentModel.EditorBrowsableAttribute((System.ComponentModel.EditorBrowsableState)1)]":
							typeFlags |= DomTypeFlags.IsEditorBrowsableNever;
							break;
						case "[System.Runtime.CompilerServices.ExtensionAttribute()]":
							typeFlags |= DomTypeFlags.IsExtension;
							break;
						case "[System.Reflection.DefaultMemberAttribute(\"Item\")]": {
							CustomAttributeData defaultMemberAttr = attribute as CustomAttributeData;
							if ((defaultMemberAttr != null) && (defaultMemberAttr.ConstructorArguments != null) && (defaultMemberAttr.ConstructorArguments.Count > 0)) {
								// Temporarily store the default member name
								defaultMemberName = defaultMemberAttr.ConstructorArguments[0].Value as string;
							}
							break;
						}
					}
				}
			}

			// Build modifiers
			modifiers = Modifiers.None;
			if (type.IsAbstract)
				modifiers |= Modifiers.Abstract;
			if (type.IsSealed)
				modifiers |= Modifiers.Final;
			if (type.IsNestedPrivate )
				modifiers |= Modifiers.Private;
			else if (type.IsNestedAssembly)
				modifiers |= Modifiers.Assembly;
			else if ((type.IsNestedFamORAssem) || (type.IsNestedFamANDAssem)) {
				modifiers |= Modifiers.Family;
				modifiers |= Modifiers.Assembly;
			}
			else if (type.IsNestedFamily)
				modifiers |= Modifiers.Family;
			else if (type.IsNestedPublic) 
				modifiers |= Modifiers.Public;
			else if (type.IsNotPublic)
				modifiers |= Modifiers.Assembly;
			else if (type.IsPublic)
				modifiers |= Modifiers.Public;

			// Get the assembly index
			if (projectContent != null)
				assemblyIndex = (byte)Math.Min(byte.MaxValue, projectContent.GetReferencedAssemblyIndex(type.Assembly.FullName));			
			
			// Get generic type arguments
			#if !NET11
			if (type.IsGenericTypeDefinition)
				typeFlags |= DomTypeFlags.GenericTypeDefinition;
			if (type.IsGenericType) {
				typeFlags |= DomTypeFlags.GenericType;
				Type[] arguments = type.GetGenericArguments();
				if ((arguments != null) && (arguments.Length > 0)) {
					// Build a new list of calling generic types
					IDomTypeReference[] newCallingGenericTypes = new IDomTypeReference[(callingGenericTypes != null ? callingGenericTypes.Length : 0) + 1];
					newCallingGenericTypes[0] = this;
					if (callingGenericTypes != null)
						callingGenericTypes.CopyTo(newCallingGenericTypes, 1);


					genericTypeArguments = new IDomTypeReference[arguments.Length];
					for (int index = 0; index < arguments.Length; index++)
						genericTypeArguments[index] = projectContent.GetTypeReference(newCallingGenericTypes, arguments[index]);
				}
			}
			#endif
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
				
		/// <summary>
		/// Gets the name of the assembly that defines the referenced type, if known.
		/// </summary>
		/// <value>The name of the assembly that defines the referenced type, if known.</value>
		string IDomTypeReference.AssemblyHint { 
			get {
				if (assemblyIndex < byte.MaxValue)
					return projectContent.GetReferencedAssembly(assemblyIndex);
				else
					return null;
			}
		}
			
		/// <summary>
		/// Gets the type contraints if this is a generic type parameter.
		/// </summary>
		/// <value>The type contraints if this is a generic type parameter.</value>
		/// <remarks>This property is only used when the <see cref="IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		ICollection IDomTypeReference.GenericTypeParameterConstraints { 
			get {
				return null;
			}
		}
		
		/// <summary>
		/// Gets the raw, unresolved full name of the type.
		/// </summary>
		/// <value>The raw, unresolved full name of the type.</value>
		string IDomTypeReference.RawFullName { 
			get {
				return this.FullName;
			}
		}
		
		/// <summary>
		/// Resolves the type reference into an <see cref="IDomType"/>.
		/// </summary>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use for resolving type references.</param>
		/// <returns>
		/// The <see cref="IDomType"/> to which the type reference was resolved, if any.
		/// </returns>
		/// <remarks>This method should always be called before any other properties are accessed.</remarks>
		IDomType IDomTypeReference.Resolve(DotNetProjectResolver projectResolver) {
			IDomType resolvedType;
			string assemblyHint = ((IDomTypeReference)this).AssemblyHint;
			if (assemblyHint != null)
				resolvedType =  projectResolver.GetType(assemblyHint, this.GetFullName(false));
			else
				resolvedType = projectResolver.GetType(null, (string[])null, this.GetFullName(false), DomBindingFlags.Default);

			// If the resolved type should be constructed...
			if (
				(resolvedType != null) &&
				(this.IsGenericType) && (resolvedType.IsGenericType) &&
				(genericTypeArguments != null) && (resolvedType.GenericTypeArguments != null) &&
				(genericTypeArguments.Length == resolvedType.GenericTypeArguments.Count)
				) {
				bool hasResolvedGenericTypeArguments = true;
				foreach (IDomTypeReference genericTypeArgument in genericTypeArguments) {
					if (genericTypeArgument.IsGenericParameter) {
						hasResolvedGenericTypeArguments = false;
						break;
					}
				}

				if (hasResolvedGenericTypeArguments)
					resolvedType = new ConstructedGenericType(resolvedType, genericTypeArguments);
			}

			if ((resolvedType != null) && ((this.ArrayRanks != null) || (this.PointerLevel > 0)))
				resolvedType = new DomResolvedTypeReference(resolvedType, this.ArrayRanks, this.PointerLevel);

			return resolvedType;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the index of the referenced assembly within the project content that defines the type.
		/// </summary>
		/// <value>The index of the referenced assembly within the project content that defines the type.</value>
		internal byte AssemblyIndex {
			get {
				return assemblyIndex;
			}
			set {
				assemblyIndex = value;
			}
		}

		/// <summary>
		/// Gets the default member name.
		/// </summary>
		/// <value>The default member name.</value>
		/// <remarks>
		/// Only used when first loading.  Caching marks the actual members instead.
		/// </remarks>
		internal string DefaultMemberName {
			get {
				return defaultMemberName;
			}
		}

		/// <summary>
		/// Returns whether the specified <see cref="DomTypeFlags"/> flag is set.
		/// </summary>
		/// <param name="typeFlags">The <see cref="DomTypeFlags"/> value to examine.</param>
		/// <param name="flag">The <see cref="DomTypeFlags"/> to check for.</param>
		/// <returns>
		/// <c>true</c> if the specified <see cref="DomTypeFlags"/> flag is set; otherwise, <c>false</c>.
		/// </returns>
		internal static bool HasTypeFlag(DomTypeFlags typeFlags, DomTypeFlags flag) {
			return ((typeFlags & flag) == flag);
		}
		
		/// <summary>
		/// Gets the <see cref="AssemblyProjectContent"/> that contains the type reference.
		/// </summary>
		/// <value>The <see cref="AssemblyProjectContent"/> that contains the type reference.</value>
		internal AssemblyProjectContent ProjectContentInternal { 
			get {
				return projectContent;
			}
		}

		/// <summary>
		/// Sets the value of the <see cref="GenericTypeArguments"/> property.
		/// </summary>
		/// <param name="value">The value to set.</param>
		internal void SetGenericTypeArguments(IDomTypeReference[] value) {
			genericTypeArguments = value;
		}

		/// <summary>
		/// Sets the value of the <see cref="Modifiers"/> property.
		/// </summary>
		/// <param name="value">The value to set.</param>
		internal void SetModifiers(Modifiers value) {
			modifiers = value;
		}

		/// <summary>
		/// Sets the value of the <see cref="Name"/> property.
		/// </summary>
		/// <param name="value">The value to set.</param>
		internal void SetName(string value) {
			name = value;
		}

		/// <summary>
		/// Sets the value of the <see cref="Namespace"/> property.
		/// </summary>
		/// <param name="value">The value to set.</param>
		internal void SetNamespace(string value) {
			@namespace = value;
		}
		
		/// <summary>
		/// Sets or clears the specified <see cref="DomTypeFlags"/> flag.
		/// </summary>
		/// <param name="typeFlags">The <see cref="DomTypeFlags"/> value to update.</param>
		/// <param name="flag">The <see cref="DomTypeFlags"/> to set or clear.</param>
		/// <param name="setBit">Whether to set the flag; otherwise, the flag is cleared.</param>
		/// <returns>The updated <see cref="DomTypeFlags"/> value.</returns>
		internal static DomTypeFlags SetTypeFlag(DomTypeFlags typeFlags, DomTypeFlags flag, bool setBit) {
			if (setBit)
				return typeFlags | flag;
			else
				return typeFlags & (~flag);
		}
		
		/// <summary>
		/// Gets or sets the <see cref="DomTypeFlags"/> for the type reference.
		/// </summary>
		/// <value>The <see cref="DomTypeFlags"/> for the type reference.</value>
		internal DomTypeFlags TypeFlags {
			get {
				return typeFlags;
			}
			set {
				typeFlags = value;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the access-related <see cref="Modifiers"/> values.
		/// </summary>
		/// <value>The access-related <see cref="Modifiers"/> values.</value>
		public Modifiers AccessModifiers {
			get {
				return modifiers & Modifiers.AccessMask;
			}
		}
		
		/// <summary>
		/// Gets the array dimension ranks.
		/// </summary>
		/// <value>The array dimension ranks.</value>
		/// <remarks>
		/// <c>MyClass</c> is <see langword="null"/>.
		/// <c>MyClass[]</c> is <c>{ 1 }</c>.
		/// <c>MyClass[,]</c> is <c>{ 2 }</c>.
		/// <c>MyClass[][]</c> is <c>{ 1, 1 }</c>.
		/// </remarks>
		public virtual int[] ArrayRanks {
			get {
				return null;
			}
		}
		
		/// <summary>
		/// Gets the full name of the type.
		/// </summary>
		public string FullName { 
			get {
				return this.GetFullName(true);
			}
		}
		
		/// <summary>
		/// Gets the type arguments if this is a generic type definition.
		/// </summary>
		/// <value>The type arguments if this is a generic type definition.</value>
		public ICollection GenericTypeArguments {
			get {
				return genericTypeArguments;
			}
		}
		
		/// <summary>
		/// Returns the full name of the type reference.
		/// </summary>
		/// <param name="includeArrayPointerInfo">Whether to include array and pointer info.</param>
		/// <returns>The full name of the type reference.</returns>
		protected abstract string GetFullName(bool includeArrayPointerInfo);
		
		/// <summary>
		/// Returns the reflection image index for the specified type.
		/// </summary>
		/// <param name="type">The <see cref="DomTypeType"/> indicating the type of type.</param>
		/// <param name="accessModifiers">A <see cref="Modifiers"/> indicating the access modifiers.</param>
		/// <returns>The reflection image index for the specified type.</returns>
		public static int GetReflectionImageIndex(DomTypeType type, Modifiers accessModifiers) {
			switch (type) {
				case DomTypeType.Delegate:
					switch (accessModifiers) {
						case Modifiers.Private:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PrivateDelegate;
						case Modifiers.Assembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.InternalDelegate;
						case Modifiers.Family:
						case Modifiers.FamilyOrAssembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.ProtectedDelegate;
						default:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicDelegate;
					}
				case DomTypeType.Enumeration:
					switch (accessModifiers) {
						case Modifiers.Private:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PrivateEnumeration;
						case Modifiers.Assembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.InternalEnumeration;
						case Modifiers.Family:
						case Modifiers.FamilyOrAssembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.ProtectedEnumeration;
						default:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicEnumeration;
					}
				case DomTypeType.Interface:
					switch (accessModifiers) {
						case Modifiers.Private:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PrivateInterface;
						case Modifiers.Assembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.InternalInterface;
						case Modifiers.Family:
						case Modifiers.FamilyOrAssembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.ProtectedInterface;
						default:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicInterface;
					}
				case DomTypeType.Structure:
					switch (accessModifiers) {
						case Modifiers.Private:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PrivateStructure;
						case Modifiers.Assembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.InternalStructure;
						case Modifiers.Family:
						case Modifiers.FamilyOrAssembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.ProtectedStructure;
						default:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicStructure;
					}
				case DomTypeType.StandardModule:
					switch (accessModifiers) {
						case Modifiers.Assembly:
						case Modifiers.FamilyOrAssembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.InternalStandardModule;
						default:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicStandardModule;
					}
				default:
					switch (accessModifiers) {
						case Modifiers.Private:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PrivateClass;
						case Modifiers.Assembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.InternalClass;
						case Modifiers.Family:
						case Modifiers.FamilyOrAssembly:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.ProtectedClass;
						default:
							return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicClass;
					}
			}
		}
		
		/// <summary>
		/// Gets whether the type has a generic parameter default constructor constraint.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type has a generic parameter default constructor constraint; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>This property is only used when the <see cref="IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		public bool HasGenericParameterDefaultConstructorConstraint {
			get {
				return AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.GenericParameterDefaultConstructorConstraint);
			}
		}
		
		/// <summary>
		/// Gets whether the type has a generic parameter not-nullable value type constraint.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type has a generic parameter not-nullable value type constraint; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>This property is only used when the <see cref="IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		public bool HasGenericParameterNotNullableValueTypeConstraint {
			get {
				return AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.GenericParameterNotNullableValueTypeConstraint);
			}
		}
		
		/// <summary>
		/// Gets whether the type has a generic parameter reference type constraint.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type has a generic parameter reference type constraint; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>This property is only used when the <see cref="IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		public bool HasGenericParameterReferenceTypeConstraint {
			get {
				return AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.GenericParameterReferenceTypeConstraint);
			}
		}
		
		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		public int ImageIndex {
			get {
				return AssemblyDomType.GetReflectionImageIndex(this.Type, this.AccessModifiers);
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
				return AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.IsEditorBrowsableNever);
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
				return AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.IsExtension);
			}
		}
		
		/// <summary>
		/// Gets whether the type is a generic type parameter.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type is a generic type parameter; otherwise, <c>false</c>.
		/// </value>
		public bool IsGenericParameter { 
			get {
				return AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.GenericParameter);
			}
		}
		
		/// <summary>
		/// Gets whether the type is a generic type.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type is a generic type; otherwise, <c>false</c>.
		/// </value>
		public bool IsGenericType {
			get {
				return AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.GenericType);
			}
		}

		/// <summary>
		/// Gets whether the type is a generic type definition, from which other generic types can be constructed.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type is a generic type definition, from which other generic types can be constructed; otherwise, <c>false</c>.
		/// </value>
		public bool IsGenericTypeDefinition {
			get {
				return AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.GenericTypeDefinition);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="Modifiers"/> for the type.
		/// </summary>
		/// <value>The <see cref="Modifiers"/> for the type.</value>
		public Modifiers Modifiers { 
			get {
				return modifiers;
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
		/// Gets the name of the namespace that contains the type.
		/// </summary>
		/// <value>The name of the namespace that contains the type.</value>
		public string Namespace {
			get {
				return @namespace;
			}
		}
		
		/// <summary>
		/// Gets the unsafe pointer level of the type reference.
		/// </summary>
		/// <value>The unsafe pointer level of the type reference.</value>
		public virtual int PointerLevel {
			get {
				return 0;
			}
		}

		/// <summary>
		/// Resolve all references to types in the same assembly to the actual type.
		/// </summary>
		/// <param name="projectContent">The <see cref="AssemblyProjectContent"/> to use for resolution.</param>
		protected internal virtual void ResolveTypePlaceHolders(AssemblyProjectContent projectContent) {
			if (genericTypeArguments != null) {
				for (int index = genericTypeArguments.Length - 1; index >= 0; index--) {
					if (genericTypeArguments[index] is AssemblyDomTypePlaceHolder)
						genericTypeArguments[index] = projectContent.ResolveAssemblyDomTypePlaceHolder((AssemblyDomTypePlaceHolder)genericTypeArguments[index]);
					else if (genericTypeArguments[index] is AssemblyDomTypeReference)
						((AssemblyDomTypeReference)genericTypeArguments[index]).ResolveTypePlaceHolders(projectContent);
				}
			}
		}

		/// <summary>
		/// Converts the object to a <c>String</c>.
		/// </summary>
		/// <returns>
		/// A string whose value represents this object.
		/// </returns>
		public override string ToString() {
			return String.Format("AssemblyDomTypeReference[{0}]", this.FullName);
		}

		/// <summary>
		/// Gets the <see cref="DomTypeType"/> that indicates the type of type that this object represents.
		/// </summary>
		/// <value>The <see cref="DomTypeType"/> that indicates the type of type that this object represents.</value>
		public DomTypeType Type {
			get {
				if (AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.Delegate))
					return DomTypeType.Delegate;
				else if (AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.Enumeration))
					return DomTypeType.Enumeration;
				else if (AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.Interface))
					return DomTypeType.Interface;
				else if (AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.Structure))
					return DomTypeType.Structure;
				else if (AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.StandardModule))
					return DomTypeType.StandardModule;
				else
					return DomTypeType.Class;
			}
		}

	}
}
