using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents an <see cref="IDomType"/> with resolved array rank and pointer information.
	/// </summary>
	internal class DomResolvedTypeReference : IDomType {

		private int[]		arrayRanks;
		private int			pointerLevel;
		private IDomType	type;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>DomResolvedTypeReference</c> class.
		/// </summary>
		/// <param name="type">The <see cref="IDomType"/> that this type reference is based on.</param>
		/// <param name="arrayRanks">The array ranks.</param>
		/// <param name="pointerLevel">The pointer level.</param>
		internal DomResolvedTypeReference(IDomType type, int[] arrayRanks, int pointerLevel) {
			// Initialize parameters
			this.type			= type;
			this.arrayRanks		= arrayRanks;
			this.pointerLevel	= pointerLevel;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
			
		/// <summary>
		/// Gets the <see cref="IDomTypeReference"/> to the base type.
		/// </summary>
		/// <value>The <see cref="IDomTypeReference"/> to the base type.</value>
		IDomTypeReference IDomType.BaseType { 
			get {
				if (this.arrayRanks != null)
					return new TypeReference("System.Array", TextRange.Deleted);
				else
					return type.BaseType;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="IDomTypeReference"/> to the declaring type, if this is a nested type.
		/// </summary>
		/// <value>The <see cref="IDomTypeReference"/> to the declaring type, if this is a nested type.</value>
		IDomTypeReference IDomType.DeclaringType { 
			get {
				return type.DeclaringType;
			}
		}

		/// <summary>
		/// Gets the <see cref="DomDocumentationProvider"/> for the type.
		/// </summary>
		/// <value>The <see cref="DomDocumentationProvider"/> for the type.</value>
		DomDocumentationProvider IDomType.DocumentationProvider { 
			get {
				return type.DocumentationProvider;
			}
		}
		
		/// <summary>
		/// Returns the access <see cref="Modifiers"/> of the type's constructors.
		/// </summary>
		/// <returns>The access <see cref="Modifiers"/> of the type's constructors.</returns>
		Modifiers IDomType.GetConstructorAccessModifiers() {
			return type.GetConstructorAccessModifiers();
		}

		/// <summary>
		/// Returns the array of interfaces that this type implements.
		/// </summary>
		/// <returns>An <see cref="IDomTypeReference"/> array specifying the interfaces that this type implements.</returns>
		IDomTypeReference[] IDomType.GetInterfaces() {
			if (arrayRanks != null) {
				if ((arrayRanks.Length == 1) && (arrayRanks[0] == 1)) {
					// Return the generic implementations of collection interfaces for array types
					TypeReference iListRef = new TypeReference("System.Collections.Generic.IList", TextRange.Deleted);
					iListRef.GenericTypeArguments.Add(new TypeReference(type.FullName, true));
					TypeReference iCollectionRef = new TypeReference("System.Collections.Generic.ICollection", TextRange.Deleted);
					iCollectionRef.GenericTypeArguments.Add(new TypeReference(type.FullName, true));
					TypeReference iEnumerableRef = new TypeReference("System.Collections.Generic.IEnumerable", TextRange.Deleted);
					iEnumerableRef.GenericTypeArguments.Add(new TypeReference(type.FullName, true));

					return new IDomTypeReference[] { iListRef, iCollectionRef, iEnumerableRef };
				}
			}
			return null;
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
		IDomMember IDomType.GetMember(IDomType[] contextInheritanceHierarchy, string name, DomBindingFlags flags) {
			return null;
		}

		/// <summary>
		/// Gets all the members defined in the type, which does not include inherited members.
		/// </summary>
		/// <returns>An <see cref="IDomMember"/> array specifying all the members defined in the type.</returns>
		IDomMember[] IDomType.GetMembers() {
			return null;
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
		IDomMember[] IDomType.GetMembers(IDomType[] contextInheritanceHierarchy, string name, DomBindingFlags flags) {
			return null;
		}
		
		/// <summary>
		/// Returns the string-based keys that identify the sources of the type, which typically are filenames.
		/// </summary>
		/// <returns>The string-based keys that identify the sources of the type, which typically are filenames.</returns>
		/// <remarks>
		/// Types defined in assemblies will return <see langword="null"/>.  
		/// In this case, the <see cref="IDomType.ProjectContent"/> property can be used to determine what assembly defines the type.
		/// <para>
		/// Normally only one source key is returned, however more than one may be returned if the type is a partial type.
		/// A <see langword="null"/> entry in the string array will be made if the type has no parent <see cref="CompilationUnit"/>
		/// or if the <see cref="CompilationUnit"/> has no <see cref="CompilationUnit.SourceKey"/> assigned.
		/// </para>
		/// </remarks>
		string[] IDomType.GetSourceKeys() {
			return type.GetSourceKeys();
		}
		
		/// <summary>
		/// Gets whether the type has an <c>EditorBrowsableAttribute</c> on it with a value of <c>Never</c>.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type has an <c>EditorBrowsableAttribute</c> on it with a value of <c>Never</c>; otherwise, <c>false</c>.
		/// </value>
		bool IDomType.IsEditorBrowsableNever { 
			get {
				return type.IsEditorBrowsableNever;
			}
		}
		
		/// <summary>
		/// Gets whether the type is marked with an <c>ExtensionAttribute</c>.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type is marked with an <c>ExtensionAttribute</c>; otherwise, <c>false</c>.
		/// </value>
		bool IDomType.IsExtension { 
			get {
				return type.IsExtension;
			}
		}
		
		/// <summary>
		/// Gets whether the type is a nested type.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type is a nested type; otherwise, <c>false</c>.
		/// </value>
		bool IDomType.IsNested { 
			get {
				return type.IsNested;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="Modifiers"/> for the type.
		/// </summary>
		/// <value>The <see cref="Modifiers"/> for the type.</value>
		Modifiers IDomType.Modifiers { 
			get {
				return type.Modifiers;
			}
		}

		/// <summary>
		/// Gets the <see cref="IProjectContent"/> that declares the type.
		/// </summary>
		/// <value>The <see cref="IProjectContent"/> that declares the type.</value>
		/// <remarks>
		/// Types defined in source code will return <see langword="null"/> for this property since
		/// they will be contained in the <see cref="SourceProjectContent"/> for the 
		/// <see cref="DotNetProjectResolver"/> in use.
		/// </remarks>
		IProjectContent IDomType.ProjectContent { 
			get {
				return type.ProjectContent;
			}
		}
		
		/// <summary>
		/// Gets the access-related <see cref="Modifiers"/> values.
		/// </summary>
		/// <value>The access-related <see cref="Modifiers"/> values.</value>
		Modifiers IDomTypeReference.AccessModifiers { 
			get {
				return type.AccessModifiers;
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
		int[] IDomTypeReference.ArrayRanks { 
			get {
				return arrayRanks;
			}
		}
		
		/// <summary>
		/// Gets the name of the assembly that defines the referenced type, if known.
		/// </summary>
		/// <value>The name of the assembly that defines the referenced type, if known.</value>
		string IDomTypeReference.AssemblyHint { 
			get {
				return type.AssemblyHint;
			}
		}
		
		/// <summary>
		/// Gets the full name of the type.
		/// </summary>
		string IDomTypeReference.FullName { 
			get {
				return DotNetProjectResolver.GetTypeNameWithArrayPointerSpec(type.FullName, 0, arrayRanks, pointerLevel);
			}
		}
		
		/// <summary>
		/// Gets the type arguments if this is a generic type definition.
		/// </summary>
		/// <value>The type arguments if this is a generic type definition.</value>
		ICollection IDomTypeReference.GenericTypeArguments { 
			get {
				return type.GenericTypeArguments;
			}
		}
		
		/// <summary>
		/// Gets the type contraints if this is a generic type parameter.
		/// </summary>
		/// <value>The type contraints if this is a generic type parameter.</value>
		/// <remarks>This property is only used when the <see cref="IDomTypeReference.IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		ICollection IDomTypeReference.GenericTypeParameterConstraints { 
			get {
				return type.GenericTypeParameterConstraints;
			}
		}
		
		/// <summary>
		/// Gets whether the type has a generic parameter default constructor constraint.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type has a generic parameter default constructor constraint; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>This property is only used when the <see cref="IDomTypeReference.IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		bool IDomTypeReference.HasGenericParameterDefaultConstructorConstraint { 
			get {
				return type.HasGenericParameterDefaultConstructorConstraint;
			}
		}
		
		/// <summary>
		/// Gets whether the type has a generic parameter not-nullable value type constraint.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type has a generic parameter not-nullable value type constraint; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>This property is only used when the <see cref="IDomTypeReference.IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		bool IDomTypeReference.HasGenericParameterNotNullableValueTypeConstraint { 
			get {
				return type.HasGenericParameterNotNullableValueTypeConstraint;
			}
		}
		
		/// <summary>
		/// Gets whether the type has a generic parameter reference type constraint.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type has a generic parameter reference type constraint; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>This property is only used when the <see cref="IDomTypeReference.IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		bool IDomTypeReference.HasGenericParameterReferenceTypeConstraint { 
			get {
				return type.HasGenericParameterReferenceTypeConstraint;
			}
		}
		
		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		int IDomTypeReference.ImageIndex { 
			get {
				return type.ImageIndex;
			}
		}

		/// <summary>
		/// Gets whether the type is a generic type parameter.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type is a generic type parameter; otherwise, <c>false</c>.
		/// </value>
		bool IDomTypeReference.IsGenericParameter { 
			get {
				return type.IsGenericParameter;
			}
		}

		/// <summary>
		/// Gets whether the type is a generic type.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type is a generic type; otherwise, <c>false</c>.
		/// </value>
		bool IDomTypeReference.IsGenericType { 
			get {
				return type.IsGenericType;
			}
		}

		/// <summary>
		/// Gets whether the type is a generic type definition, from which other generic types can be constructed.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type is a generic type definition, from which other generic types can be constructed; otherwise, <c>false</c>.
		/// </value>
		bool IDomTypeReference.IsGenericTypeDefinition { 
			get {
				return type.IsGenericTypeDefinition;
			}
		}
		
		/// <summary>
		/// Gets the name of the type.
		/// </summary>
		/// <value>The name of the type.</value>
		string IDomTypeReference.Name { 
			get {
				return type.Name;
			}
		}
		
		/// <summary>
		/// Gets the name of the namespace that contains the type.
		/// </summary>
		/// <value>The name of the namespace that contains the type.</value>
		string IDomTypeReference.Namespace { 
			get {
				return type.Namespace;
			}
		}
		
		/// <summary>
		/// Gets the unsafe pointer level of the type reference.
		/// </summary>
		/// <value>The unsafe pointer level of the type reference.</value>
		int IDomTypeReference.PointerLevel { 
			get {
				return pointerLevel;
			}
		}

		/// <summary>
		/// Gets the raw, unresolved full name of the type.
		/// </summary>
		/// <value>The raw, unresolved full name of the type.</value>
		string IDomTypeReference.RawFullName { 
			get {
				return type.RawFullName;
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
			return this;  // 4/28/2008 - Commented out since it wipes out arrays... type.Resolve(projectResolver);
		}

		/// <summary>
		/// Gets the <see cref="DomTypeType"/> that indicates the type of type that this object represents.
		/// </summary>
		/// <value>The <see cref="DomTypeType"/> that indicates the type of type that this object represents.</value>
		DomTypeType IDomTypeReference.Type { 
			get {
				return type.Type;
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the core <see cref="IDomType"/> that is wrapped by this class.
		/// </summary>
		/// <value>The core <see cref="IDomType"/> that is wrapped by this class.</value>
		internal IDomType CoreType {
			get {
				return type;
			}
		}
		
		/// <summary>
		/// Converts the object to a <c>String</c>.
		/// </summary>
		/// <returns>
		/// A string whose value represents this object.
		/// </returns>
		public override string ToString() {
			return String.Format("DomResolvedTypeReference[{0}]", ((IDomTypeReference)this).FullName);
		}

	}
}
