using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents a generic .NET type that has been constructed and wraps the <see cref="IDomType"/> that is its definition.
	/// </summary>
	internal class ConstructedGenericType : IDomType {

		private IDomType	genericDefinitionType;
		private ICollection	genericTypeArguments;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>ConstructedGenericType</c> class.
		/// </summary>
		/// <param name="genericDefinitionType">The <see cref="IDomType"/> specifying the generic type definition to wrap.</param>
		/// <param name="genericTypeArguments">The <see cref="ICollection"/> of generic type arguments.</param>
		internal ConstructedGenericType(IDomType genericDefinitionType, ICollection genericTypeArguments) {
			// There is a scenario that needs to be fixed where genericDefinitionType is being passed a ConstructedGenericType, which is bad
			int recursionCount = 0;
			while (genericDefinitionType is ConstructedGenericType) {
				#if DEBUG
				// throw new ArgumentException("Cannot wrap a ConstructedGenericType with a ConstructedGenericType.");
				#endif
				genericDefinitionType = ((ConstructedGenericType)genericDefinitionType).GenericDefinitionType;
				if (recursionCount > 10)
					break;
			}

			// Initialize parameters
			this.genericDefinitionType	= genericDefinitionType;
			this.genericTypeArguments	= genericTypeArguments;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="IDomType"/> specifying the generic type definition that is wrapped.
		/// </summary>
		/// <value>The <see cref="IDomType"/> specifying the generic type definition that is wrapped.</value>
		internal IDomType GenericDefinitionType {
			get {
				return genericDefinitionType;
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
				return genericDefinitionType.AccessModifiers;
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
		public int[] ArrayRanks {
			get {
				return genericDefinitionType.ArrayRanks;
			}
		}
		
		/// <summary>
		/// Gets the name of the assembly that defines the referenced type, if known.
		/// </summary>
		/// <value>The name of the assembly that defines the referenced type, if known.</value>
		public string AssemblyHint { 
			get {
				return genericDefinitionType.AssemblyHint;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="IDomTypeReference"/> to the base type.
		/// </summary>
		/// <value>The <see cref="IDomTypeReference"/> to the base type.</value>
		public IDomTypeReference BaseType { 
			get {
				return genericDefinitionType.BaseType;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="IDomTypeReference"/> to the declaring type, if this is a nested type.
		/// </summary>
		/// <value>The <see cref="IDomTypeReference"/> to the declaring type, if this is a nested type.</value>
		public IDomTypeReference DeclaringType { 
			get {
				return genericDefinitionType.DeclaringType;
			}
		}

		/// <summary>
		/// Gets the <see cref="DomDocumentationProvider"/> for the type.
		/// </summary>
		/// <value>The <see cref="DomDocumentationProvider"/> for the type.</value>
		public DomDocumentationProvider DocumentationProvider {
			get {
				return genericDefinitionType.DocumentationProvider;
			}
		}
		
		/// <summary>
		/// Determines whether the specified <c>Object</c> is equal to the current <c>Object</c>. 
		/// </summary>
		/// <param name="obj">The <c>Object</c> to compare to the current <c>Object</c>.</param>
		/// <returns>
		/// <c>true</c> if the specified <c>Object</c> is equal to the current <c>Object</c>; 
		/// otherwise, <c>false</c>. 
		/// </returns>
		public override bool Equals(Object obj) {
			ConstructedGenericType otherType = obj as ConstructedGenericType;
			if (otherType == null)
				return false;

			if (genericDefinitionType != otherType.genericDefinitionType)
				return false;
			if ((genericTypeArguments == null) && (otherType.genericTypeArguments == null))
				return true;
			if ((genericTypeArguments == null) || (otherType.genericTypeArguments == null))
				return false;

			// Create arrays of arguments
			IDomTypeReference[] sourceTypeParameters = new IDomTypeReference[genericTypeArguments.Count];
			IDomTypeReference[] targetTypeParameters = new IDomTypeReference[otherType.genericTypeArguments.Count];
			if (sourceTypeParameters.Length == targetTypeParameters.Length) {
				// Build arrays
				genericTypeArguments.CopyTo(sourceTypeParameters, 0);
				otherType.genericTypeArguments.CopyTo(targetTypeParameters, 0);

				for (int index = 0; index < sourceTypeParameters.Length; index++) {
					if (sourceTypeParameters[index].FullName != targetTypeParameters[index].FullName)
						return false;
				}
				return true;
			}

			return false;
		}
		
		/// <summary>
		/// Gets the full name of the type.
		/// </summary>
		public string FullName { 
			get {
				return genericDefinitionType.FullName;
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
		/// Gets the type contraints if this is a generic type parameter.
		/// </summary>
		/// <value>The type contraints if this is a generic type parameter.</value>
		/// <remarks>This property is only used when the <see cref="IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		public ICollection GenericTypeParameterConstraints { 
			get {
				return genericDefinitionType.GenericTypeParameterConstraints;
			}
		}
		
		/// <summary>
		/// Returns the access <see cref="Modifiers"/> of the type's constructors.
		/// </summary>
		/// <returns>The access <see cref="Modifiers"/> of the type's constructors.</returns>
		public Modifiers GetConstructorAccessModifiers() {
			return genericDefinitionType.GetConstructorAccessModifiers();
		}
		
		/// <summary>
		/// Returns a hash code for this object.
		/// </summary>
		/// <returns>An integer value that specifies a hash value for this object.</returns>
		public override int GetHashCode() {
			int hashCode = genericDefinitionType.GetHashCode();
			if (genericTypeArguments != null) {
				foreach (IDomTypeReference genericTypeArgument in genericTypeArguments)
					hashCode ^= genericTypeArgument.GetHashCode();
			}
			return hashCode;
		}

		/// <summary>
		/// Returns the array of interfaces that this type implements.
		/// </summary>
		/// <returns>An <see cref="IDomTypeReference"/> array specifying the interfaces that this type implements.</returns>
		public IDomTypeReference[] GetInterfaces() {
			return genericDefinitionType.GetInterfaces();
		}
		
		/// <summary>
		/// Gets a member defined in the type with the specified name, which does not include inherited members.
		/// </summary>
		/// <param name="contextInheritanceHierarchy">
		/// An optional array of the inheritance hierarchy of the context <see cref="IDomType"/>.
		/// The first array item contains the context <see cref="IDomType"/>.
		/// Each following item indicates a base <see cref="IDomType"/> of the context <see cref="IDomType"/>.
		/// </param>
		/// <param name="name">The name of the desired member.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>A member defined in the type with the specified name.</returns>
		public IDomMember GetMember(IDomType[] contextInheritanceHierarchy, string name, DomBindingFlags flags) {
			return genericDefinitionType.GetMember(contextInheritanceHierarchy, name, flags);
		}

		/// <summary>
		/// Gets all the members defined in the type, which does not include inherited members.
		/// </summary>
		/// <returns>An <see cref="IDomMember"/> array specifying all the members defined in the type.</returns>
		public IDomMember[] GetMembers() {
			return genericDefinitionType.GetMembers();
		}

		/// <summary>
		/// Gets all the members defined in the type with the specified name, which does not include inherited members.
		/// </summary>
		/// <param name="contextInheritanceHierarchy">
		/// An optional array of the inheritance hierarchy of the context <see cref="IDomType"/>.
		/// The first array item contains the context <see cref="IDomType"/>.
		/// Each following item indicates a base <see cref="IDomType"/> of the context <see cref="IDomType"/>.
		/// </param>
		/// <param name="name">The name of the desired members.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>An <see cref="IDomMember"/> array specifying all the members defined in the type with the specified name.</returns>
		public IDomMember[] GetMembers(IDomType[] contextInheritanceHierarchy, string name, DomBindingFlags flags) {
			return genericDefinitionType.GetMembers(contextInheritanceHierarchy, name, flags);
		}

		/// <summary>
		/// Returns the string-based keys that identify the sources of the type, which typically are filenames.
		/// </summary>
		/// <returns>The string-based keys that identify the sources of the type, which typically are filenames.</returns>
		/// <remarks>
		/// Types defined in assemblies will return <see langword="null"/>.  
		/// In this case, the <see cref="ProjectContent"/> property can be used to determine what assembly defines the type.
		/// <para>
		/// Normally only one source key is returned, however more than one may be returned if the type is a partial type.
		/// A <see langword="null"/> entry in the string array will be made if the type has no parent <see cref="CompilationUnit"/>
		/// or if the <see cref="CompilationUnit"/> has no <see cref="CompilationUnit.SourceKey"/> assigned.
		/// </para>
		/// </remarks>
		public string[] GetSourceKeys() {
			return genericDefinitionType.GetSourceKeys();
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
				return genericDefinitionType.HasGenericParameterDefaultConstructorConstraint;
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
				return genericDefinitionType.HasGenericParameterNotNullableValueTypeConstraint;
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
				return genericDefinitionType.HasGenericParameterReferenceTypeConstraint;
			}
		}
		
		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		public int ImageIndex {
			get {
				return genericDefinitionType.ImageIndex;
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
				return genericDefinitionType.IsEditorBrowsableNever;
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
				return genericDefinitionType.IsExtension;
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
				return genericDefinitionType.IsGenericParameter;
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
				return genericDefinitionType.IsGenericType;
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
				return false;
			}
		}
		
		/// <summary>
		/// Gets whether the type is a nested type.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type is a nested type; otherwise, <c>false</c>.
		/// </value>
		public bool IsNested { 
			get {
				return genericDefinitionType.IsNested;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="Modifiers"/> for the type.
		/// </summary>
		/// <value>The <see cref="Modifiers"/> for the type.</value>
		public Modifiers Modifiers { 
			get {
				return genericDefinitionType.Modifiers;
			}
		}

		/// <summary>
		/// Gets the name of the type.
		/// </summary>
		/// <value>The name of the type.</value>
		public string Name { 
			get {
				return genericDefinitionType.Name;
			}
		}
		
		/// <summary>
		/// Gets the name of the namespace that contains the type.
		/// </summary>
		/// <value>The name of the namespace that contains the type.</value>
		public string Namespace { 
			get {
				return genericDefinitionType.Namespace;
			}
		}
		
		/// <summary>
		/// Gets the unsafe pointer level of the type reference.
		/// </summary>
		/// <value>The unsafe pointer level of the type reference.</value>
		public int PointerLevel {
			get {
				return genericDefinitionType.PointerLevel;
			}
		}

		/// <summary>
		/// Gets the <see cref="IProjectContent"/> that declares the type.
		/// </summary>
		/// <value>The <see cref="IProjectContent"/> that declares the type.</value>
		public IProjectContent ProjectContent { 
			get {
				return genericDefinitionType.ProjectContent;
			}
		}

		/// <summary>
		/// Gets the raw, unresolved full name of the type.
		/// </summary>
		/// <value>The raw, unresolved full name of the type.</value>
		public string RawFullName { 
			get {
				return genericDefinitionType.RawFullName;
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
		public IDomType Resolve(DotNetProjectResolver projectResolver) {
			return this;
		}
		
		/// <summary>
		/// Converts the object to a <c>String</c>.
		/// </summary>
		/// <returns>
		/// A string whose value represents this object.
		/// </returns>
		public override string ToString() {
			return String.Format("ConstructedGenericType[{0}]", this.FullName);
		}

		/// <summary>
		/// Gets the <see cref="DomTypeType"/> that indicates the type of type that this object represents.
		/// </summary>
		/// <value>The <see cref="DomTypeType"/> that indicates the type of type that this object represents.</value>
		public DomTypeType Type { 
			get {
				return genericDefinitionType.Type;
			}
		}

		
	}
}
