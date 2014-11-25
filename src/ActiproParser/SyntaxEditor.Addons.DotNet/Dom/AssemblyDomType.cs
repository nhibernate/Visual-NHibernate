using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents a .NET type that is defined in an assembly.
	/// </summary>
	internal class AssemblyDomType : AssemblyDomTypeBase, IDomType {
		
		//
		// NOTE: Any changes made to fields need to be persisted to the cache in AssemblyProjectContent and the cache version number must be incremented
		//

		private IDomTypeReference	baseType;
		private IDomTypeReference	declaringType;
		private IDomTypeReference[]	interfaceTypes;
		private AssemblyDomMember[]	members;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDomType</c> class.
		/// </summary>
		/// <param name="projectContent">The <see cref="AssemblyProjectContent"/> that defines the type.</param>
		/// <remarks>This overload should only be used when reading a cache file.</remarks>
		internal AssemblyDomType(AssemblyProjectContent projectContent) : base(projectContent) {}

		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDomType</c> class.
		/// </summary>
		/// <param name="projectContent">The <see cref="AssemblyProjectContent"/> that defines the type.</param>
		/// <param name="type">The <see cref="Type"/> to wrap with this object.</param>
		internal AssemblyDomType(AssemblyProjectContent projectContent, Type type) : base(projectContent, null, type) {
			// Get the base type
			if (type.BaseType != null)
				baseType = projectContent.GetTypeReference(type.BaseType);
			
			// Get the declaring type
			if (type.DeclaringType != null)
				declaringType = projectContent.GetTypeReference(type.DeclaringType);

			// Get the implemented interfaces
			Type[] interfaceTypeArray = type.GetInterfaces();
			if ((interfaceTypeArray != null) && (interfaceTypeArray.Length > 0)) {
				interfaceTypes = new IDomTypeReference[interfaceTypeArray.Length];
				for (int index = 0; index < interfaceTypeArray.Length; index++)
					interfaceTypes[index] = projectContent.GetTypeReference(interfaceTypeArray[index]);
			}

			// Get whether the type has a constructor
			if ((type.IsValueType) && (type != typeof(void))) {
				// All value types have a public parameterless constructor
				this.TypeFlags = AssemblyDomType.SetTypeFlag(this.TypeFlags, DomTypeFlags.HasPublicConstructor, true);
			}
			if ((type.IsClass) || ((!type.IsEnum) && (type.IsValueType))) {
				ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				foreach (ConstructorInfo constructor in constructors) {
					if (constructor.IsPublic)
						this.TypeFlags = AssemblyDomType.SetTypeFlag(this.TypeFlags, DomTypeFlags.HasPublicConstructor, true);
					if (constructor.IsFamily)
						this.TypeFlags = AssemblyDomType.SetTypeFlag(this.TypeFlags, DomTypeFlags.HasFamilyConstructor, true);
					if (constructor.IsAssembly)
						this.TypeFlags = AssemblyDomType.SetTypeFlag(this.TypeFlags, DomTypeFlags.HasAssemblyConstructor, true);
					if (constructor.IsPrivate)
						this.TypeFlags = AssemblyDomType.SetTypeFlag(this.TypeFlags, DomTypeFlags.HasPrivateConstructor, true);
				}
			}

			// Get members
			ArrayList memberList = new ArrayList();
			MemberInfo[] reflectedMemberInfos = type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			foreach (MemberInfo reflectedMemberInfo in reflectedMemberInfos) {
				// Ensure proper access (public or protected)
				MethodBase methodInfo = reflectedMemberInfo as MethodBase;
				if (reflectedMemberInfo is PropertyInfo) {
					PropertyInfo propertyInfo = (PropertyInfo)reflectedMemberInfo;
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
					if (methodInfo == null)
						continue;
				}
				else if (reflectedMemberInfo is FieldInfo) {
					FieldInfo fieldInfo = (FieldInfo)reflectedMemberInfo;
					if ((!fieldInfo.IsPublic) && (!fieldInfo.IsFamily) && (!fieldInfo.IsFamilyOrAssembly))
						continue;
				}
				else if (reflectedMemberInfo is EventInfo) {
					EventInfo eventInfo = (EventInfo)reflectedMemberInfo;
					methodInfo = eventInfo.GetAddMethod();
					if (methodInfo == null)
						continue;
				}
				else if (methodInfo == null)
					continue;

				if ((methodInfo != null) && ((!methodInfo.IsPublic) && (!methodInfo.IsFamily) && (!methodInfo.IsFamilyOrAssembly)))
					continue;

				// Ignore specially named implmentation members
				if ((reflectedMemberInfo.Name.StartsWith("add_")) || (reflectedMemberInfo.Name.StartsWith("get_")) ||
					(reflectedMemberInfo.Name.StartsWith("op_")) || (reflectedMemberInfo.Name.StartsWith("remove_")) ||
					(reflectedMemberInfo.Name.StartsWith("set_")))
					continue;

				// Add the member
				#if !NET11
				if ((methodInfo != null) && ((methodInfo.IsGenericMethod) || (methodInfo.ContainsGenericParameters)))
					memberList.Add(new AssemblyDomGenericMember(projectContent, this, reflectedMemberInfo));
				else 
				#endif
				if ((methodInfo != null) && (methodInfo.GetParameters().Length > 0))
					memberList.Add(new AssemblyDomParameterizedMember(projectContent, this, reflectedMemberInfo, this.DefaultMemberName));
				else
					memberList.Add(new AssemblyDomMember(projectContent, this, reflectedMemberInfo));
			}

			// Build the array
			members = new AssemblyDomMember[memberList.Count];
			if (memberList.Count > 0)
				memberList.CopyTo(members);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
			
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
		string[] IDomType.GetSourceKeys() {
			return null;
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
			return this;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns whether the <see cref="IDomMember"/> matches with the desired name and <see cref="DomBindingFlags"/>.
		/// </summary>
		/// <param name="contextInheritanceHierarchy">
		/// An optional array of the inheritance hierarchy of the context <see cref="IDomType"/>.
		/// The first array item contains the context <see cref="IDomType"/>.
		/// Each following item indicates a base <see cref="IDomType"/> of the context <see cref="IDomType"/>.
		/// </param>
		/// <param name="declaringType">The declaring <see cref="IDomType"/> of the member.</param>
		/// <param name="member">The <see cref="IDomMember"/> to examine.</param>
		/// <param name="name">The name to match.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>
		/// <c>true</c> if the <see cref="IDomMember"/> matches with the desired name and <see cref="DomBindingFlags"/>; otherwise, <c>false</c>.
		/// </returns>
		internal static bool IsMatch(IDomType[] contextInheritanceHierarchy, IDomType declaringType, IDomMember member, string name, DomBindingFlags flags) {
			// Ensure that instance/static request matches
			if (!(
				((!member.IsStatic) && ((flags & DomBindingFlags.Instance) == DomBindingFlags.Instance)) ||
				((member.IsStatic) && ((flags & DomBindingFlags.Static) == DomBindingFlags.Static))
				))
				return false;

			// Exclude indexers if appropriate
			if (((flags & DomBindingFlags.ExcludeIndexers) == DomBindingFlags.ExcludeIndexers) && (member.MemberType == DomMemberType.Property) && (member.Parameters != null) && (member.Parameters.Length > 0))
				return false;
			
			// Include only indexers if appropriate
			if (((flags & DomBindingFlags.OnlyIndexers) == DomBindingFlags.OnlyIndexers) && ((member.MemberType != DomMemberType.Property) || (member.Parameters == null) || (member.Parameters.Length == 0)))
				return false;

			// Include only constructors if appropriate
			if (((flags & DomBindingFlags.OnlyConstructors) == DomBindingFlags.OnlyConstructors) != (member.MemberType == DomMemberType.Constructor))
				return false;

			// Exclude editor never-browsable members
			if (((flags & DomBindingFlags.ExcludeEditorNeverBrowsable) == DomBindingFlags.ExcludeEditorNeverBrowsable) && (member.IsEditorBrowsableNever))
				return false;

			// Get the access modifiers
			Modifiers accessModifiers = member.AccessModifiers;
			if (declaringType.ProjectContent != null) {
				// Remove any assembly access since the declaring type is not in the source project content
				accessModifiers &= ~Modifiers.Assembly;
			}
			if (contextInheritanceHierarchy != null) {
				// Since a context inheritance hierarchy was passed in, use it to tweak the access modifiers based on where the declaring
				//   type is defined in relation to the context type

				if ((flags & DomBindingFlags.ContextIsDeclaringType) != DomBindingFlags.ContextIsDeclaringType) {
					// Remove any private access
					accessModifiers &= ~Modifiers.Private;

					// Rules for family access to be allowed:
					// 1) Code is in the same class, similar to private (ContextIsDeclaringType)
					// 2) Is a direct "this" or "base" type of reference (ObjectReference)
					// 3) Is a static member and the context type is the target's family (ContextIsTargetFamily)
					if ((flags & DomBindingFlags.ObjectReference) != DomBindingFlags.ObjectReference) {
						// Is an object reference so only match if the context is the declaring type
						if ((flags & (DomBindingFlags.Static | DomBindingFlags.ContextIsTargetFamily)) != (DomBindingFlags.Static | DomBindingFlags.ContextIsTargetFamily)) {
							// Remove any family access 
							accessModifiers &= ~Modifiers.Family;
						}
					}
				}
			}
			else {
				// Remove any family or private access
				accessModifiers &= ~(Modifiers.Family | Modifiers.Private);
			}

			// Ensure that scope access matches
			if (!(
				(((accessModifiers & Modifiers.Public) == Modifiers.Public) && ((flags & DomBindingFlags.Public) == DomBindingFlags.Public)) ||
				(((accessModifiers & Modifiers.Family) == Modifiers.Family) && ((flags & DomBindingFlags.Family) == DomBindingFlags.Family)) ||
				(((accessModifiers & Modifiers.Assembly) == Modifiers.Assembly) && ((flags & DomBindingFlags.Assembly) == DomBindingFlags.Assembly)) ||
				(((accessModifiers & Modifiers.Private) == Modifiers.Private) && ((flags & DomBindingFlags.Private) == DomBindingFlags.Private))
				))
				return false;

			// Compare the name
			if ((name == null) || (String.Compare(member.Name, name, ((flags & DomBindingFlags.IgnoreCase) == DomBindingFlags.IgnoreCase)) == 0))
				return true;
			else
				return false;
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
		/// <param name="returnFirst">Whether to return the first result only.</param>
		/// <returns>An <see cref="IDomMember"/> array specifying all the members defined in the type with the specified name.</returns>
		private IDomMember[] GetMembersCore(IDomType[] contextInheritanceHierarchy, string name, DomBindingFlags flags, bool returnFirst) {
			// Initialize the result list
			ArrayList memberList = null;

			// Flag whether the context is the current type
			if ((contextInheritanceHierarchy != null) && (contextInheritanceHierarchy.Length > 0) && (contextInheritanceHierarchy[0] == this))
				flags |= DomBindingFlags.ContextIsDeclaringType;

			// Look for a match
			foreach (IDomMember member in members) {
				if (AssemblyDomType.IsMatch(contextInheritanceHierarchy, this, member, name, flags)) {
					if (returnFirst)
						return new IDomMember[] { member };
					
					if (memberList == null)
						memberList = new ArrayList();
					memberList.Add(member);
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
		
		/// <summary>
		/// Sets the value of the <see cref="BaseType"/> property.
		/// </summary>
		/// <param name="value">The value to set.</param>
		internal void SetBaseType(IDomTypeReference value) {
			baseType = value;
		}
		
		/// <summary>
		/// Sets the value of the <see cref="DeclaringType"/> property.
		/// </summary>
		/// <param name="value">The value to set.</param>
		internal void SetDeclaringType(IDomTypeReference value) {
			declaringType = value;
		}
		
		/// <summary>
		/// Sets the return value of the <see cref="GetInterfaces"/> method.
		/// </summary>
		/// <param name="value">The value to set.</param>
		internal void SetInterfaceTypes(IDomTypeReference[] value) {
			interfaceTypes = value;
		}
		
		/// <summary>
		/// Sets the value of the members array.
		/// </summary>
		/// <param name="value">The value to set.</param>
		internal void SetMembers(AssemblyDomMember[] value) {
			members = value;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="IDomTypeReference"/> to the base type.
		/// </summary>
		/// <value>The <see cref="IDomTypeReference"/> to the base type.</value>
		public IDomTypeReference BaseType {
			get {
				return baseType;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="IDomTypeReference"/> to the declaring type, if this is a nested type.
		/// </summary>
		/// <value>The <see cref="IDomTypeReference"/> to the declaring type, if this is a nested type.</value>
		public IDomTypeReference DeclaringType { 
			get {
				return declaringType;
			}
		}

		/// <summary>
		/// Gets the <see cref="DomDocumentationProvider"/> for the type.
		/// </summary>
		/// <value>The <see cref="DomDocumentationProvider"/> for the type.</value>
		public DomDocumentationProvider DocumentationProvider {
			get {
				AssemblyDocumentation documentation = this.ProjectContentInternal.Documentation;
				if (documentation != null)
					return new DomDocumentationProvider(documentation.GetDocumentation(DomDocumentationProvider.GetTypeReferenceDocumentationKey(this)));
				else
					return new DomDocumentationProvider(null);
			}
		}
		
		/// <summary>
		/// Returns the access <see cref="Modifiers"/> of the type's constructors.
		/// </summary>
		/// <returns>The access <see cref="Modifiers"/> of the type's constructors.</returns>
		public Modifiers GetConstructorAccessModifiers() {
			Modifiers accessModifiers = Modifiers.None;
			if (AssemblyDomType.HasTypeFlag(this.TypeFlags, DomTypeFlags.HasPublicConstructor))
				accessModifiers |= Modifiers.Public;
			if (AssemblyDomType.HasTypeFlag(this.TypeFlags, DomTypeFlags.HasFamilyConstructor))
				accessModifiers |= Modifiers.Family;
			if (AssemblyDomType.HasTypeFlag(this.TypeFlags, DomTypeFlags.HasAssemblyConstructor))
				accessModifiers |= Modifiers.Assembly;
			if (AssemblyDomType.HasTypeFlag(this.TypeFlags, DomTypeFlags.HasPrivateConstructor))
				accessModifiers |= Modifiers.Private;
			return accessModifiers;
		}
		
		/// <summary>
		/// Returns the full name of the type reference.
		/// </summary>
		/// <param name="includeArrayPointerInfo">Whether to include array and pointer info.</param>
		/// <returns>The full name of the type reference.</returns>
		protected override string GetFullName(bool includeArrayPointerInfo) {
			if (declaringType != null)
				return DotNetProjectResolver.GetTypeNameWithArrayPointerSpec(declaringType.FullName + "+" + this.Name,
					0, (includeArrayPointerInfo ? this.ArrayRanks : null), (includeArrayPointerInfo ? (int)this.PointerLevel : 0));
			else
				return DotNetProjectResolver.GetTypeNameWithArrayPointerSpec(((this.Namespace != null) && (this.Namespace.Length > 0) ? this.Namespace + "." : String.Empty) + this.Name,
					0, (includeArrayPointerInfo ? this.ArrayRanks : null), (includeArrayPointerInfo ? (int)this.PointerLevel : 0));
		}
		
		/// <summary>
		/// Returns the array of interfaces that this type implements.
		/// </summary>
		/// <returns>An <see cref="IDomTypeReference"/> array specifying the interfaces that this type implements.</returns>
		public IDomTypeReference[] GetInterfaces() {
			return interfaceTypes;
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
			IDomMember[] result = this.GetMembersCore(contextInheritanceHierarchy, name, flags, true);
			if (result.Length > 0)
				return result[0];
			else
				return null;
		}

		/// <summary>
		/// Gets all the members defined in the type, which does not include inherited members.
		/// </summary>
		/// <returns>An <see cref="IDomMember"/> array specifying all the members defined in the type.</returns>
		public IDomMember[] GetMembers() {
			return members;
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
			return this.GetMembersCore(contextInheritanceHierarchy, name, flags, false);
		}
		
		/// <summary>
		/// Gets whether the type is a nested type.
		/// </summary>
		/// <value>
		/// <c>true</c> if the type is a nested type; otherwise, <c>false</c>.
		/// </value>
		public bool IsNested { 
			get {
				return AssemblyDomTypeReference.HasTypeFlag(this.TypeFlags, DomTypeFlags.Nested);
			}
		}

		/// <summary>
		/// Gets the <see cref="IProjectContent"/> that declares the type.
		/// </summary>
		/// <value>The <see cref="IProjectContent"/> that declares the type.</value>
		public IProjectContent ProjectContent { 
			get {
				return this.ProjectContentInternal;
			}
		}
		
		/// <summary>
		/// Resolve all references to types in the same assembly to the actual type.
		/// </summary>
		/// <param name="projectContent">The <see cref="AssemblyProjectContent"/> to use for resolution.</param>
		protected internal override void ResolveTypePlaceHolders(AssemblyProjectContent projectContent) {
			// Call the base method
			base.ResolveTypePlaceHolders(projectContent);

			if (declaringType is AssemblyDomTypePlaceHolder)
				declaringType = projectContent.ResolveAssemblyDomTypePlaceHolder((AssemblyDomTypePlaceHolder)declaringType);
			else if (declaringType is AssemblyDomTypeReference)
				((AssemblyDomTypeReference)declaringType).ResolveTypePlaceHolders(projectContent);

			if (baseType is AssemblyDomTypePlaceHolder)
				baseType = projectContent.ResolveAssemblyDomTypePlaceHolder((AssemblyDomTypePlaceHolder)baseType);
			else if (baseType is AssemblyDomTypeReference)
				((AssemblyDomTypeReference)baseType).ResolveTypePlaceHolders(projectContent);
			
			if (interfaceTypes != null) {
				for (int index = 0; index < interfaceTypes.Length; index++) {
					IDomTypeReference interfaceType = interfaceTypes[index];
					if (interfaceType is AssemblyDomTypePlaceHolder)
						interfaceTypes[index] = projectContent.ResolveAssemblyDomTypePlaceHolder((AssemblyDomTypePlaceHolder)interfaceType);
					else if (interfaceType is AssemblyDomTypeReference)
						((AssemblyDomTypeReference)interfaceTypes[index]).ResolveTypePlaceHolders(projectContent);
				}
			}

			if (members != null) {
				foreach (AssemblyDomMember member in members)
					member.ResolveTypePlaceHolders(projectContent);
			}
		}
		
		/// <summary>
		/// Converts the object to a <c>String</c>.
		/// </summary>
		/// <returns>
		/// A string whose value represents this object.
		/// </returns>
		public override string ToString() {
			return String.Format("AssemblyDomType[{0}]", this.FullName);
		}

	}
}
