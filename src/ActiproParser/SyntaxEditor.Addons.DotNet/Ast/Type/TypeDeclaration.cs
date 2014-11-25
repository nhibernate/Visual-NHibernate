using System;
using System.Collections;
using System.Reflection;
using System.Text;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a type declaration for a class, structure, interface, or enumeration.
	/// </summary>
	public abstract class TypeDeclaration : TypeMemberDeclaration, IBlockAstNode, ICollapsibleNode, IDomType, IDomTypeReference {

		private int					blockEndOffset						= -1;
		private int					blockStartOffset					= -1;
		private bool				firstBaseTypeIsInterface;
		
		/// <summary>
		/// Gets the context ID for a generic type argument AST node.
		/// </summary>
		/// <value>The context ID for a generic type argument AST node.</value>
		public const byte GenericTypeArgumentContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a base type AST node.
		/// </summary>
		/// <value>The context ID for a base type AST node.</value>
		public const byte BaseTypeContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase + 1;

		/// <summary>
		/// Gets the context ID for a member AST node.
		/// </summary>
		/// <value>The context ID for a member AST node.</value>
		public const byte MemberContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase + 2;
		
		/// <summary>
		/// Gets the minimum context ID that should be used in your code for AST nodes inheriting this class.
		/// </summary>
		/// <value>The minimum context ID that should be used in your code for AST nodes inheriting this class.</value>
		/// <remarks>
		/// Base all your context ID constants off of this value.
		/// </remarks>
		protected const byte TypeDeclarationContextIDBase = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase + 3;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>TypeDeclaration</c> class. 
		/// </summary>
		/// <param name="modifiers">The modifiers.</param>
		/// <param name="name">The name.</param>
		public TypeDeclaration(Modifiers modifiers, QualifiedIdentifier name) : base(modifiers, name) {}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the offset at which the outlining node ends.
		/// </summary>
		/// <value>The offset at which the outlining node ends.</value>
		int ICollapsibleNode.EndOffset { 
			get {
				return blockEndOffset;
			}
		}

		/// <summary>
		/// Gets whether the node is collapsible.
		/// </summary>
		/// <value>
		/// <c>true</c> if the node is collapsible; otherwise, <c>false</c>.
		/// </value>
		bool ICollapsibleNode.IsCollapsible { 
			get {
				return (blockStartOffset != -1) && ((blockStartOffset < blockEndOffset) || (blockEndOffset == -1));
			}
		}
		
		/// <summary>
		/// Gets the offset at which the outlining node starts.
		/// </summary>
		/// <value>The offset at which the outlining node starts.</value>
		int ICollapsibleNode.StartOffset { 
			get {
				return blockStartOffset;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="IDomTypeReference"/> to the base type.
		/// </summary>
		/// <value>The <see cref="IDomTypeReference"/> to the base type.</value>
		IDomTypeReference IDomType.BaseType {
			get {
				if (firstBaseTypeIsInterface)
					return null;

				IAstNodeList baseTypes = this.BaseTypes;
				foreach (AstNode node in baseTypes) {
					if ((node is TypeReference) && (((TypeReference)node).Name != null))
						return (TypeReference)node;
				}
				return new AssemblyDomTypeReference(null, null, typeof(object));
			}
		}
		
		/// <summary>
		/// Gets the <see cref="IDomTypeReference"/> to the declaring type, if this is a nested type.
		/// </summary>
		/// <value>The <see cref="IDomTypeReference"/> to the declaring type, if this is a nested type.</value>
		IDomTypeReference IDomType.DeclaringType { 
			get {
				return this.ParentNode as IDomType;
			}
		}
		
		/// <summary>
		/// Returns the access <see cref="Modifiers"/> of the type's constructors.
		/// </summary>
		/// <returns>The access <see cref="Modifiers"/> of the type's constructors.</returns>
		public Modifiers GetConstructorAccessModifiers() {
			Modifiers accessModifiers = Modifiers.None;

			switch (((IDomType)this).Type) {
				case DomTypeType.Class:
					// Do code below
					break;
				case DomTypeType.Structure:
					// Structures always have public constructors
					accessModifiers |= Modifiers.Public;
					// Do code below
					break;
				case DomTypeType.Delegate:
				case DomTypeType.StandardModule:
					// Modules cannot be created
					return Modifiers.None;
				default:
					return accessModifiers;
			}

			// Look for constructors
			IAstNodeList members = this.Members;
			foreach (IAstNode memberNode in members) {
				if (memberNode is ConstructorDeclaration) {
					ConstructorDeclaration member = (ConstructorDeclaration)memberNode;
					if (!member.IsStatic) {
						if ((member.AccessModifiers & Modifiers.Public) == Modifiers.Public)
							accessModifiers |= Modifiers.Public;
						if ((member.AccessModifiers & Modifiers.Family) == Modifiers.Family)
							accessModifiers |= Modifiers.Family;
						if ((member.AccessModifiers & Modifiers.Assembly) == Modifiers.Assembly)
							accessModifiers |= Modifiers.Assembly;
						if ((member.AccessModifiers & Modifiers.Private) == Modifiers.Private)
							accessModifiers |= Modifiers.Private;
					}
				}
			}

			// If a class with no constructors defines, add a public one that is auto-generated by the compile
			if ((accessModifiers == Modifiers.None) && (((IDomType)this).Type == DomTypeType.Class))
				accessModifiers = Modifiers.Public;

			return accessModifiers;
		}
		
		/// <summary>
		/// Returns the array of interfaces that this type implements.
		/// </summary>
		/// <returns>An <see cref="IDomTypeReference"/> array specifying the interfaces that this type implements.</returns>
		IDomTypeReference[] IDomType.GetInterfaces() {
			IAstNodeList baseTypes = this.BaseTypes;
			if (baseTypes.Count - (firstBaseTypeIsInterface ? 0 : 1) <= 0)
				return null;

			IDomTypeReference[] interfaces = new IDomTypeReference[baseTypes.Count - (firstBaseTypeIsInterface ? 0 : 1)];
			for (int index = (firstBaseTypeIsInterface ? 0 : 1); index < baseTypes.Count; index++)
				interfaces[index - (firstBaseTypeIsInterface ? 0 : 1)] = (IDomTypeReference)baseTypes[index];

			return interfaces;
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
			IDomMember[] result = this.GetMembersCore(contextInheritanceHierarchy, name, flags, true, false);
			if (result.Length > 0)
				return result[0];
			else
				return null;
		}
		
		/// <summary>
		/// Gets all the members defined in the type, which does not include inherited members.
		/// </summary>
		/// <returns>An <see cref="IDomMember"/> array specifying all the members defined in the type.</returns>
		IDomMember[] IDomType.GetMembers() {
			return this.GetMembersCore(null, null, DomBindingFlags.Default, false, true);
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
			return this.GetMembersCore(contextInheritanceHierarchy, name, flags, false, false);
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
			CompilationUnit compilationUnit = this.FindAncestor(typeof(CompilationUnit)) as CompilationUnit;
			if (compilationUnit != null)
				return new string[] { compilationUnit.SourceKey };
			else
				return new string[] { null };
		}

		/// <summary>
		/// Gets whether the type is a nested type.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type is a nested type; otherwise, <c>false</c>.
		/// </value>
		bool IDomType.IsNested { 
			get {
				return (((IDomType)this).DeclaringType != null);
			}
		}

		/// <summary>
		/// Gets the <see cref="IProjectContent"/> that declares the type.
		/// </summary>
		/// <value>The <see cref="IProjectContent"/> that declares the type.</value>
		IProjectContent IDomType.ProjectContent { 
			get {
				return null;
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
				return null;
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
		/// <remarks>This property is only used when the <see cref="IDomTypeReference.IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		ICollection IDomTypeReference.GenericTypeParameterConstraints { 
			get {
				return null;
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
				return false;
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
				return false;
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
				return false;
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
				return false;
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
				// Declarations are generic types if they are generic type definitions
				return ((IDomType)this).IsGenericTypeDefinition;
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
				// Declarations are generic type definitions if they have generic type arguments
				return (this.GenericTypeArguments.Count > 0);
			}
		}
		
		/// <summary>
		/// Gets the name of the type.
		/// </summary>
		/// <value>The name of the type.</value>
		string IDomTypeReference.Name {
			get {
				if (this.Name != null) {
					ICollection genericTypeArguments = this.GenericTypeArguments;
					if ((genericTypeArguments != null) && (genericTypeArguments.Count > 0))
						return this.Name.Text + "`" + genericTypeArguments.Count;
					else
						return this.Name.Text;
				}
				else
					return null;
			}
		}

		/// <summary>
		/// Gets the name of the namespace that contains the type.
		/// </summary>
		/// <value>The name of the namespace that contains the type.</value>
		string IDomTypeReference.Namespace { 
			get {
				return DotNetProjectResolver.GetNamespaceName(this.FullName);
			}
		}
			
		/// <summary>
		/// Gets the unsafe pointer level of the type reference.
		/// </summary>
		/// <value>The unsafe pointer level of the type reference.</value>
		int IDomTypeReference.PointerLevel {
			get {
				return 0;
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
			// First, initialize whether the first base type is an interface
			firstBaseTypeIsInterface = (((IDomType)this).Type == DomTypeType.Interface);

			if (projectResolver != null) {
				bool isPartial = ((this.Modifiers & Modifiers.Partial) == Modifiers.Partial);
				if (!isPartial) {
					// See if this is VB code... always make partial in that case
					CompilationUnit compilationUnit = this.FindAncestor(typeof(ICompilationUnit)) as CompilationUnit;
					if (compilationUnit != null)
						isPartial = (compilationUnit.SourceLanguage == DotNetLanguage.VB);
				}
				if (isPartial) {
					// Since a partial modifier is specified, check the project resolver to see if there is merged partial type info available
					IDomType type = projectResolver.GetType(null, (string[])null, this.FullName, DomBindingFlags.Default);
					if (type is SourceMergedPartialType)
						return type;
				}
			}

			// Update whether the first base type is an interface
			if (firstBaseTypeIsInterface) {
				IAstNodeList baseTypes = this.BaseTypes;
				if (baseTypes.Count > 0) {
					IDomType type = ((IDomTypeReference)baseTypes[0]).Resolve(projectResolver);
					if (type != null)
						firstBaseTypeIsInterface = (type.Type == DomTypeType.Interface);
				}
			}

			return this;
		}

		/// <summary>
		/// Gets the <see cref="DomTypeType"/> that indicates the type of type that this object represents.
		/// </summary>
		/// <value>The <see cref="DomTypeType"/> that indicates the type of type that this object represents.</value>
		DomTypeType IDomTypeReference.Type { 
			get {
				if (this is DelegateDeclaration)
					return DomTypeType.Delegate;
				else if (this is EnumerationDeclaration)
					return DomTypeType.Enumeration;
				else if (this is InterfaceDeclaration)
					return DomTypeType.Interface;
				else if (this is StructureDeclaration)
					return DomTypeType.Structure;
				else if (this is StandardModuleDeclaration)
					return DomTypeType.StandardModule;
				else
					return DomTypeType.Class;
			}
		}

		/// <summary>
		/// Gets whether the outlining indicator should be visible for the node.
		/// </summary>
		/// <value>
		/// <c>true</c> if the outlining indicator should be visible for the node; otherwise, <c>false</c>.
		/// </value>
		bool IOutliningNodeParseData.IndicatorVisible { 
			get {
				return true;
			}
		}

		/// <summary>
		/// Gets whether the outlining node is for a language transition.
		/// </summary>
		/// <value>
		/// <c>true</c> if the outlining node is for a language transition; otherwise, <c>false</c>.
		/// </value>
		bool IOutliningNodeParseData.IsLanguageTransition { 
			get {
				return false;
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
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
		/// <param name="returnFirst">Whether to return the first result only.</param>
		/// <param name="returnAll">Whether to return everything.</param>
		/// <returns>An <see cref="IDomMember"/> array specifying all the members defined in the type with the specified name.</returns>
		private IDomMember[] GetMembersCore(IDomType[] contextInheritanceHierarchy, string name, DomBindingFlags flags, bool returnFirst, bool returnAll) {
			// Initialize the result list
			ArrayList memberList = null;

			// Flag whether the context is the current type
			if ((contextInheritanceHierarchy != null) && (contextInheritanceHierarchy.Length > 0) && (contextInheritanceHierarchy[0].FullName == this.FullName))
				flags |= DomBindingFlags.ContextIsDeclaringType;

			// Look for a match
			IAstNodeList members = this.Members;
			foreach (IAstNode node in members) {
				if (node is IDomMember) {
					IDomMember member = (IDomMember)node;

					if ((returnAll) || (AssemblyDomType.IsMatch(contextInheritanceHierarchy, this, member, name, flags))) {
						if (returnFirst)
							return new IDomMember[] { member };
						
						if (memberList == null)
							memberList = new ArrayList();
						memberList.Add(member);
					}
				}
				else if (node is IVariableDeclarationSection) {
					IVariableDeclarationSection memberDeclarationSection = (IVariableDeclarationSection)node;
					foreach (IAstNode childNode in memberDeclarationSection.Variables) {
						if (childNode is IDomMember) {
							IDomMember member = (IDomMember)childNode;

							if ((returnAll) || (AssemblyDomType.IsMatch(contextInheritanceHierarchy, this, member, name, flags))) {
								if (returnFirst)
									return new IDomMember[] { member };
								
								if (memberList == null)
									memberList = new ArrayList();
								memberList.Add(member);
							}
						}
					}
				}
			}

			// If only looking for a single result, quit 
			if ((returnFirst) || (memberList == null))
				return new IDomMember[0];

			// Build the array
			IDomMember[] memberArray = new IDomMember[memberList.Count];
			if (memberList.Count > 0)
				memberList.CopyTo(memberArray);

			return memberArray;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the collection of base types.
		/// </summary>
		/// <value>The collection of base types.</value>
		public IAstNodeList BaseTypes {
			get {
				return new AstNodeListWrapper(this, TypeDeclaration.BaseTypeContextID);
			}
		}

		/// <summary>
		/// Gets or sets the end character offset of the end block delimiter in the original source code that generated the AST node.
		/// </summary>
		/// <value>The end character offset of the end block delimiter in the original source code that generated the AST node.</value>
		/// <remarks>
		/// This value may be <c>-1</c> if there is no source code information for the end character offset.
		/// </remarks>
		public int BlockEndOffset {
			get {
				return blockEndOffset;
			}
			set {
				blockEndOffset = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the start character offset of the start block delimiter in the original source code that generated the AST node.
		/// </summary>
		/// <value>The start character offset of the start block delimiter in the original source code that generated the AST node.</value>
		/// <remarks>
		/// This value may be <c>-1</c> if there is no source code information for the start character offset.
		/// </remarks>
		public int BlockStartOffset {
			get {
				return blockStartOffset;
			}
			set {
				blockStartOffset = value;
			}
		}
		
		/// <summary>
		/// Gets the full name of the type.
		/// </summary>
		/// <value>The full name of the type.</value>
		public string FullName {
			get {
				return AstNode.GetTypeFullName(this);
			}
		}
		
		/// <summary>
		/// Gets the collection of generic type arguments.
		/// </summary>
		/// <value>The collection of generic type arguments.</value>
		public IAstNodeList GenericTypeArguments {
			get {
				return new AstNodeListWrapper(this, TypeDeclaration.GenericTypeArgumentContextID);
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
			return AstNode.GetTypeReferenceName(language, detailLevel, this);
		}

		/// <summary>
		/// Gets the string-based key that uniquely identifies the <see cref="AstNode"/>.
		/// </summary>
		/// <value>The string-based key that uniquely identifies the <see cref="AstNode"/>.</value>
		public override string Key {
			get {
				return DomDocumentationProvider.GetTypeReferenceDocumentationKey(this);
			}
		}

		/// <summary>
		/// Gets the collection of type members.
		/// </summary>
		/// <value>The collection of type members.</value>
		public IAstNodeList Members {
			get {
				return new AstNodeListWrapper(this, TypeDeclaration.MemberContextID);
			}
		}
		
		/// <summary>
		/// Gets an <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.
		/// </summary>
		/// <value>An <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.</value>
		public override DotNetNodeCategory NodeCategory {
			get {
				return DotNetNodeCategory.TypeDeclaration;
			}
		}
		
		/// <summary>
		/// Converts the object to a <c>String</c>.
		/// </summary>
		/// <returns>
		/// A string whose value represents this object.
		/// </returns>
		public override string ToString() {
			return String.Format("[{0}-{1}] {2}: {3}", this.StartOffset, this.EndOffset, this.GetType().Name, this.FullName);
		}

	}
}
