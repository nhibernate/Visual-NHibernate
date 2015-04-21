using System;
using System.Collections;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a type reference.
	/// </summary>
	public class TypeReference : AstNode, IDomTypeReference {
		
		private int[]				arrayRanks;
		private string				name;
		private byte				pointerLevel;
		private IDomType			resolvedType;
		private DomTypeFlags		typeFlags							= DomTypeFlags.None;

		/// <summary>
		/// Gets the type name that is used to denote an anonymous type.
		/// </summary>
		/// <value>The type name that is used to denote an anonymous type.</value>
		public const string	AnonymousTypeName = "_Anonymous";
		
		/// <summary>
		/// Gets the context ID for an attribute section AST node.
		/// </summary>
		/// <value>The context ID for an attribute section AST node.</value>
		public const byte AttributeSectionContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a generic type argument AST node.
		/// </summary>
		/// <value>The context ID for a generic type argument AST node.</value>
		public const byte GenericTypeArgumentContextID = AstNode.AstNodeContextIDBase + 1;

		/// <summary>
		/// Gets the context ID for a generic type parameter contraint AST node.
		/// </summary>
		/// <value>The context ID for a generic type parameter contraint AST node.</value>
		public const byte GenericTypeParameterConstraintContextID = AstNode.AstNodeContextIDBase + 2;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>TypeReference</c> class. 
		/// </summary>
		/// <param name="name">The name of the type reference.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public TypeReference(string name, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.name = name;
		}
		
		/// <summary>
		/// Initializes a new instance of the <c>TypeReference</c> class. 
		/// </summary>
		/// <param name="name">The name of the type reference.</param>
		/// <param name="isGenericParameter">Whether the type reference is for a generic parameter.</param>
		internal TypeReference(string name, bool isGenericParameter) : this(name, TextRange.Deleted) {
			// Initialize parameters
			this.IsGenericParameter = isGenericParameter;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the access-related <see cref="Modifiers"/> values.
		/// </summary>
		/// <value>The access-related <see cref="Modifiers"/> values.</value>
		Modifiers IDomTypeReference.AccessModifiers { 
			get {
				if (resolvedType != null)
					return resolvedType.AccessModifiers;
				else
					return Modifiers.None;
			}
		}
		
		/// <summary>
		/// Gets the name of the assembly that defines the referenced type, if known.
		/// </summary>
		/// <value>The name of the assembly that defines the referenced type, if known.</value>
		string IDomTypeReference.AssemblyHint { 
			get {
				return null;
			}
		}
		
		/// <summary>
		/// Gets the full name of the type.
		/// </summary>
		string IDomTypeReference.FullName { 
			get {
				return this.GetFullName(true);
			}
		}
		
		/// <summary>
		/// Gets the type arguments if this is a generic type definition.
		/// </summary>
		/// <value>The type arguments if this is a generic type definition.</value>
		ICollection IDomTypeReference.GenericTypeArguments { 
			get {
				return this.GenericTypeArguments;
			}
		}
		
		/// <summary>
		/// Gets the type contraints if this is a generic type parameter.
		/// </summary>
		/// <value>The type contraints if this is a generic type parameter.</value>
		ICollection IDomTypeReference.GenericTypeParameterConstraints { 
			get {
				return this.GenericTypeParameterConstraints;
			}
		}
		
		/// <summary>
		/// Gets the name of the type.
		/// </summary>
		/// <value>The name of the type.</value>
		string IDomTypeReference.Name { 
			get {
				return DotNetProjectResolver.GetTypeName(this.GetFullName(false));
			}
		}
		
		/// <summary>
		/// Gets the name of the namespace that contains the type.
		/// </summary>
		/// <value>The name of the namespace that contains the type.</value>
		string IDomTypeReference.Namespace { 
			get {
				return DotNetProjectResolver.GetNamespaceName(this.GetFullName(false));
			}
		}
			
		/// <summary>
		/// Gets the raw, unresolved full name of the type.
		/// </summary>
		/// <value>The raw, unresolved full name of the type.</value>
		string IDomTypeReference.RawFullName { 
			get {
				return name;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DomTypeType"/> that indicates the type of type that this object represents.
		/// </summary>
		/// <value>The <see cref="DomTypeType"/> that indicates the type of type that this object represents.</value>
		DomTypeType IDomTypeReference.Type { 
			get {
				if (resolvedType != null)
					return resolvedType.Type;
				else
					return DomTypeType.Class;
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Clones the <see cref="TypeReference"/>.
		/// </summary>
		/// <returns>The cloned instance.</returns>
		internal TypeReference Clone() {
			TypeReference typeReference = new TypeReference(name, this.TextRange);

			typeReference.arrayRanks = arrayRanks;
			typeReference.pointerLevel = pointerLevel;
			typeReference.resolvedType = resolvedType;
			typeReference.typeFlags = typeFlags;

			foreach (object obj in this.GenericTypeArguments) {
				TypeReference genericTypeArgument = obj as TypeReference;
				if (genericTypeArgument != null)
					typeReference.GenericTypeArguments.Add(genericTypeArgument.Clone());
			}
			
			foreach (object obj in this.GenericTypeParameterConstraints) {
				TypeReference genericTypeParameterConstraints = obj as TypeReference;
				if (genericTypeParameterConstraints != null)
					typeReference.GenericTypeParameterConstraints.Add(genericTypeParameterConstraints.Clone());
			}

			// Set the parent node so that a frame of reference can be maintained
			typeReference.ParentNode = this.ParentNode;

			return typeReference;
		}

		/// <summary>
		/// Returns the full name of the type reference.
		/// </summary>
		/// <param name="includeArrayPointerInfo">Whether to include array and pointer info.</param>
		/// <returns>The full name of the type reference.</returns>
		private string GetFullName(bool includeArrayPointerInfo) {
			if (resolvedType != null)  // If looking at a resolved type, we already have passed in the array and pointer info to the type so no need to duplicate
				return DotNetProjectResolver.GetTypeNameWithArrayPointerSpec(resolvedType.FullName, 0, null, 0);
			else
				return DotNetProjectResolver.GetTypeNameWithArrayPointerSpec(name, this.GenericTypeArguments.Count, 
					(includeArrayPointerInfo ? arrayRanks : null), (includeArrayPointerInfo ? (int)pointerLevel : 0));
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
		/// Gets or sets the array ranks.
		/// </summary>
		/// <value>The array ranks.</value>
		/// <remarks>
		/// <c>MyClass</c> is <see langword="null"/>.
		/// <c>MyClass[]</c> is <c>{ 1 }</c>.
		/// <c>MyClass[,]</c> is <c>{ 2 }</c>.
		/// <c>MyClass[][]</c> is <c>{ 1, 1 }</c>.
		/// </remarks>
		public int[] ArrayRanks {
			get {
				return arrayRanks;
			}
			set {
				arrayRanks = value;
			}
		}

		/// <summary>
		/// Gets the collection of attribute sections.
		/// </summary>
		/// <value>The collection of attribute sections.</value>
		public IAstNodeList AttributeSections {
			get {
				return new AstNodeListWrapper(this, TypeReference.AttributeSectionContextID);
			}
		}

		/// <summary>
		/// Gets the collection of generic type arguments.
		/// </summary>
		/// <value>The collection of generic type arguments.</value>
		public IAstNodeList GenericTypeArguments {
			get {
				return new AstNodeListWrapper(this, TypeReference.GenericTypeArgumentContextID);
			}
		}
		
		/// <summary>
		/// Gets the type contraints if this is a generic type parameter.
		/// </summary>
		/// <value>The type contraints if this is a generic type parameter.</value>
		public IAstNodeList GenericTypeParameterConstraints { 
			get {
				return new AstNodeListWrapper(this, TypeReference.GenericTypeParameterConstraintContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets whether the type has a generic parameter default constructor constraint.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type has a generic parameter default constructor constraint; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>This property is only used when the <see cref="IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		public bool HasGenericParameterDefaultConstructorConstraint {
			get {
				return AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.GenericParameterDefaultConstructorConstraint);
			}
			set {
				typeFlags = AssemblyDomTypeReference.SetTypeFlag(typeFlags, DomTypeFlags.GenericParameterDefaultConstructorConstraint, value);
			}
		}
		
		/// <summary>
		/// Gets or sets whether the type has a generic parameter not-nullable value type constraint.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type has a generic parameter not-nullable value type constraint; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>This property is only used when the <see cref="IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		public bool HasGenericParameterNotNullableValueTypeConstraint {
			get {
				return AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.GenericParameterNotNullableValueTypeConstraint);
			}
			set {
				typeFlags = AssemblyDomTypeReference.SetTypeFlag(typeFlags, DomTypeFlags.GenericParameterNotNullableValueTypeConstraint, value);
			}
		}
		
		/// <summary>
		/// Gets or sets whether the type has a generic parameter reference type constraint.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type has a generic parameter reference type constraint; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>This property is only used when the <see cref="IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		public bool HasGenericParameterReferenceTypeConstraint {
			get {
				return AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.GenericParameterReferenceTypeConstraint);
			}
			set {
				typeFlags = AssemblyDomTypeReference.SetTypeFlag(typeFlags, DomTypeFlags.GenericParameterReferenceTypeConstraint, value);
			}
		}
		
		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		public override int ImageIndex {
			get {
				return AssemblyDomType.GetReflectionImageIndex(((IDomTypeReference)this).Type, ((IDomTypeReference)this).AccessModifiers);
			}
		}

		/// <summary>
		/// Gets or sets whether the type is a generic type parameter.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type is a generic type parameter; otherwise, <c>false</c>.
		/// </value>
		public bool IsGenericParameter { 
			get {
				return AssemblyDomTypeReference.HasTypeFlag(typeFlags, DomTypeFlags.GenericParameter);
			}
			set {
				typeFlags = AssemblyDomTypeReference.SetTypeFlag(typeFlags, DomTypeFlags.GenericParameter, value);
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
				return (this.GenericTypeArguments.Count > 0);
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
				return false;
			}
		}
		
		/// <summary>
		/// Gets or sets the name of the type.
		/// </summary>
		/// <value>The name of the type.</value>
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
				return DotNetNodeType.TypeReference;
			}
		}

		/// <summary>
		/// Gets or sets the unsafe pointer level of the type reference.
		/// </summary>
		/// <value>The unsafe pointer level of the type reference.</value>
		public int PointerLevel {
			get {
				return pointerLevel;
			}
			set {
				pointerLevel = (byte)Math.Min(byte.MaxValue, value);
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
			resolvedType = null;
			CompilationUnit compilationUnit = this.RootNode as CompilationUnit;
			if (projectResolver != null) {
				// Get the full name
				string typeFullName = this.GetFullName(false);

				string[] importedNamespaces;
				Hashtable namespaceAliases;

				// If there is a compilation unit available...
				if (compilationUnit != null) {
					// Get the imported namespaces
					compilationUnit.GetImportedNamespaces(this, out importedNamespaces, out namespaceAliases);
				}
				else {
					importedNamespaces = new string[0];
					namespaceAliases = new Hashtable();
				}

				// Resolve aliases
				int dotIndex = typeFullName.IndexOf('.');
				if ((dotIndex != -1) && (namespaceAliases.Count > 0)) {
					string resolvedNamespace = (string)namespaceAliases[typeFullName.Substring(0, dotIndex).Trim()];
					if (resolvedNamespace != null)
						typeFullName = resolvedNamespace + typeFullName.Substring(dotIndex);
				}

				// Try and find a resolved type
				resolvedType = projectResolver.GetType(this.ParentTypeDeclaration, importedNamespaces, typeFullName, DomBindingFlags.Default);
				if (resolvedType != null) {
					if ((arrayRanks != null) || (pointerLevel > 0))
						resolvedType = new DomResolvedTypeReference(resolvedType, arrayRanks, pointerLevel);

					if (resolvedType.IsGenericTypeDefinition) {
						IAstNodeList genericTypeArguments = this.GenericTypeArguments;
						if ((genericTypeArguments != null) && (genericTypeArguments.Count > 0)) {
							// If all generic type arguments are already resolved... construct the resolved type
							foreach (IDomTypeReference genericTypeArgument in genericTypeArguments) {
								if (genericTypeArgument.IsGenericParameter) {
									// If unresolved, just return the resolved type
									return resolvedType;
								}
							}

							// All generic type arguments are already resolved... construct the type
							return new ConstructedGenericType(resolvedType, genericTypeArguments);
						}
					}
				}
			}

			return resolvedType;
		}
		
		/// <summary>
		/// Converts the object to a <c>String</c>.
		/// </summary>
		/// <returns>
		/// A string whose value represents this object.
		/// </returns>
		public override string ToString() {
			return base.ToString() + (name != null ? ": " + 
				DotNetProjectResolver.GetTypeNameWithArrayPointerSpec(name, this.GenericTypeArguments.Count, arrayRanks, pointerLevel) : String.Empty);
		}
		
	}
}
