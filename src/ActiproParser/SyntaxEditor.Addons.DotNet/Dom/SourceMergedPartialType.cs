// #define DEBUG_ADDREMOVE 

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents a .NET type consisting of one or more partial types in source code.
	/// </summary>
	internal class SourceMergedPartialType : IDomType {

		private string				primaryKey;
		private HybridDictionary	typeData	= new HybridDictionary();
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>SourceMergedPartialType</c> class.
		/// </summary>
		/// <param name="sourceKey1">The source key of the first type.</param>
		/// <param name="type1">The first type with which to initialize.</param>
		/// <param name="sourceKey2">The source key of the second type.</param>
		/// <param name="type2">The second type with which to initialize.</param>
		internal SourceMergedPartialType(string sourceKey1, IDomType type1, string sourceKey2, IDomType type2) {
			this.Add(sourceKey1, type1);
			this.Add(sourceKey2, type2);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Adds type data for the specified <see cref="IDomType"/>.
		/// </summary>
		/// <param name="sourceKey">The source key of the type.</param>
		/// <param name="type">The type to add.</param>
		internal void Add(string sourceKey, IDomType type) {
			#if DEBUG && DEBUG_ADDREMOVE
			Trace.WriteLine("SourceMergedPartialType.Add: " + sourceKey + ", " + type);
			#endif

			lock (this) {
				typeData[sourceKey] = type;
			}
			if (primaryKey == null)
				primaryKey = sourceKey;
		}

		/// <summary>
		/// Returns whether the merged partial type contains the specified source key.
		/// </summary>
		/// <param name="sourceKey">The source key for which to search.</param>
		/// <returns>
		/// <c>true</c> if the source key exists; otherwise, <c>false</c>.
		/// </returns>
		internal bool Contains(string sourceKey) {
			lock (this) {
				return typeData.Contains(sourceKey);
			}
		}

		/// <summary>
		/// Gets the number of type data records in the merged partial type.
		/// </summary>
		/// <value>The number of type data records in the merged partial type.</value>
		internal int Count {
			get {
				lock (this) {
					return typeData.Count;
				}
			}
		}

		/// <summary>
		/// Gets the collection of partial types.
		/// </summary>
		/// <value>The collection of partial types.</value>
		internal ICollection PartialTypes {
			get {
				lock (this) {
					return typeData.Values;
				}
			}
		}

		/// <summary>
		/// Gets the primary source key.
		/// </summary>
		/// <value>The primary source key.</value>
		internal string PrimarySourceKey {
			get {
				return primaryKey;
			}
		}
		
		/// <summary>
		/// Gets the primary <see cref="IDomType"/>.
		/// </summary>
		/// <value>The primary <see cref="IDomType"/>.</value>
		internal IDomType PrimaryType {
			get {
				return this[primaryKey];
			}
		}
		
		/// <summary>
		/// Removes type data from the merged partial type.
		/// </summary>
		/// <param name="sourceKey">The source key of the type.</param>
		internal void Remove(string sourceKey) {
			#if DEBUG && DEBUG_ADDREMOVE
			Trace.WriteLine("SourceMergedPartialType.Remove: " + sourceKey);
			#endif

			lock (this) {
				typeData.Remove(sourceKey);
				if (primaryKey == sourceKey) {
					foreach (string key in typeData.Keys) {
						primaryKey = key;
						break;
					}
				}
			}
		}
		
		/// <summary>
		/// Gets the <see cref="IDomType"/> with the specified source key. 
		/// <para>
		/// [C#] In C#, this property is the indexer for the <c>SourceMergedPartialType</c> class. 
		/// </para>
		/// </summary>
		/// <param name="sourceKey">The source key of the <see cref="IDomType"/> to return.</param>
		/// <value>
		/// The <see cref="IDomType"/> with the specified source key. 
		/// </value>
		internal IDomType this[string sourceKey] {
			get {
				lock (this) {
					return (IDomType)typeData[sourceKey];
				}
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
				Modifiers modifiers = Modifiers.None;
				lock (this) {
					foreach (IDomType type in typeData.Values)
						modifiers |= type.AccessModifiers;
				}
				return modifiers;
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
				return null;
			}
		}
		
		/// <summary>
		/// Gets the name of the assembly that defines the referenced type, if known.
		/// </summary>
		/// <value>The name of the assembly that defines the referenced type, if known.</value>
		public string AssemblyHint { 
			get {
				return null;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="IDomTypeReference"/> to the base type.
		/// </summary>
		/// <value>The <see cref="IDomTypeReference"/> to the base type.</value>
		public IDomTypeReference BaseType { 
			get {
				lock (this) {
					foreach (IDomType type in typeData.Values) {
						if ((type.BaseType != null) && (type.BaseType.FullName != "System.Object"))
							return type.BaseType;
					}
				}
				return new AssemblyDomTypeReference(null, null, typeof(object));
			}
		}
		
		/// <summary>
		/// Gets the <see cref="IDomTypeReference"/> to the declaring type, if this is a nested type.
		/// </summary>
		/// <value>The <see cref="IDomTypeReference"/> to the declaring type, if this is a nested type.</value>
		public IDomTypeReference DeclaringType { 
			get {
				return this.PrimaryType.DeclaringType;
			}
		}

		/// <summary>
		/// Gets the <see cref="DomDocumentationProvider"/> for the type.
		/// </summary>
		/// <value>The <see cref="DomDocumentationProvider"/> for the type.</value>
		public DomDocumentationProvider DocumentationProvider {
			get {
				string mergedDocumentation = String.Empty;
				lock (this) {
					foreach (IDomType type in typeData.Values) {
						string documentation = type.DocumentationProvider.Documentation;
						if (documentation != null)
							mergedDocumentation += documentation;
					}
				}
				return new DomDocumentationProvider(mergedDocumentation);
			}
		}

		/// <summary>
		/// Gets the full name of the type.
		/// </summary>
		public string FullName { 
			get {
				return this.PrimaryType.FullName;
			}
		}
		
		/// <summary>
		/// Gets the type arguments if this is a generic type definition.
		/// </summary>
		/// <value>The type arguments if this is a generic type definition.</value>
		public ICollection GenericTypeArguments { 
			get {
				return this.PrimaryType.GenericTypeArguments;
			}
		}
		
		/// <summary>
		/// Gets the type contraints if this is a generic type parameter.
		/// </summary>
		/// <value>The type contraints if this is a generic type parameter.</value>
		/// <remarks>This property is only used when the <see cref="IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		public ICollection GenericTypeParameterConstraints { 
			get {
				return this.PrimaryType.GenericTypeParameterConstraints;
			}
		}
		
		/// <summary>
		/// Returns the access <see cref="Modifiers"/> of the type's constructors.
		/// </summary>
		/// <returns>The access <see cref="Modifiers"/> of the type's constructors.</returns>
		public Modifiers GetConstructorAccessModifiers() {
			lock (this) {
				Modifiers modifiers = Modifiers.None;
				foreach (IDomType type in typeData.Values)
					modifiers |= type.GetConstructorAccessModifiers();
				return modifiers;
			}
		}

		/// <summary>
		/// Returns the array of interfaces that this type implements.
		/// </summary>
		/// <returns>An <see cref="IDomTypeReference"/> array specifying the interfaces that this type implements.</returns>
		public IDomTypeReference[] GetInterfaces() {
			ArrayList interfaces = new ArrayList();

			lock (this) {
				foreach (IDomType type in typeData.Values) {
					IDomTypeReference[] typeInterfaces = type.GetInterfaces();
					if (typeInterfaces != null)
						interfaces.AddRange(typeInterfaces);
				}
			}

			if (interfaces.Count > 0)
				return (IDomTypeReference[])interfaces.ToArray(typeof(IDomTypeReference));
			else
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
		public IDomMember GetMember(IDomType[] contextInheritanceHierarchy, string name, DomBindingFlags flags) {
			lock (this) {
				foreach (IDomType type in typeData.Values) {
					IDomMember member = type.GetMember(contextInheritanceHierarchy, name, flags);
					if (member != null)
						return member;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets all the members defined in the type, which does not include inherited members.
		/// </summary>
		/// <returns>An <see cref="IDomMember"/> array specifying all the members defined in the type.</returns>
		public IDomMember[] GetMembers() {
			ArrayList members = new ArrayList();
			lock (this) {
				foreach (IDomType type in typeData.Values)
					members.AddRange(type.GetMembers());
			}
			return (IDomMember[])members.ToArray(typeof(IDomMember));
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
			ArrayList members = new ArrayList();
			lock (this) {
				foreach (IDomType type in typeData.Values)
					members.AddRange(type.GetMembers(contextInheritanceHierarchy, name, flags));
			}
			return (IDomMember[])members.ToArray(typeof(IDomMember));
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
			lock (this) {
				string[] sourceKeys = new string[typeData.Count];
				typeData.Keys.CopyTo(sourceKeys, 0);
				return sourceKeys;
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
				return this.PrimaryType.HasGenericParameterDefaultConstructorConstraint;
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
				return this.PrimaryType.HasGenericParameterNotNullableValueTypeConstraint;
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
				return this.PrimaryType.HasGenericParameterReferenceTypeConstraint;
			}
		}
		
		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		public int ImageIndex {
			get {
				lock (this) {
					foreach (IDomType type in typeData.Values) {
						if (type.ImageIndex != -1)
							return type.ImageIndex;
					}
				}
				return -1;
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
				return this.PrimaryType.IsEditorBrowsableNever;
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
				return this.PrimaryType.IsExtension;
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
				return this.PrimaryType.IsGenericParameter;
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
				return this.PrimaryType.IsGenericType;
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
				return this.PrimaryType.IsGenericTypeDefinition;
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
				return this.PrimaryType.IsNested;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="Modifiers"/> for the type.
		/// </summary>
		/// <value>The <see cref="Modifiers"/> for the type.</value>
		public Modifiers Modifiers { 
			get {
				Modifiers modifiers = Modifiers.None;
				lock (this) {
					foreach (IDomType type in typeData.Values)
						modifiers |= type.Modifiers;
				}
				return modifiers;
			}
		}

		/// <summary>
		/// Gets the name of the type.
		/// </summary>
		/// <value>The name of the type.</value>
		public string Name { 
			get {
				return this.PrimaryType.Name;
			}
		}
		
		/// <summary>
		/// Gets the name of the namespace that contains the type.
		/// </summary>
		/// <value>The name of the namespace that contains the type.</value>
		public string Namespace { 
			get {
				return this.PrimaryType.Namespace;
			}
		}
		
		/// <summary>
		/// Gets the unsafe pointer level of the type reference.
		/// </summary>
		/// <value>The unsafe pointer level of the type reference.</value>
		public int PointerLevel {
			get {
				return 0;
			}
		}

		/// <summary>
		/// Gets the <see cref="IProjectContent"/> that declares the type.
		/// </summary>
		/// <value>The <see cref="IProjectContent"/> that declares the type.</value>
		public IProjectContent ProjectContent { 
			get {
				return this.PrimaryType.ProjectContent;
			}
		}

		/// <summary>
		/// Gets the raw, unresolved full name of the type.
		/// </summary>
		/// <value>The raw, unresolved full name of the type.</value>
		public string RawFullName { 
			get {
				return this.PrimaryType.RawFullName;
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
		/// Gets the <see cref="DomTypeType"/> that indicates the type of type that this object represents.
		/// </summary>
		/// <value>The <see cref="DomTypeType"/> that indicates the type of type that this object represents.</value>
		public DomTypeType Type { 
			get {
				return this.PrimaryType.Type;
			}
		}

		
	}
}
