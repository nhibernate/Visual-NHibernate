// #define DEBUG_CACHE_WRITING
// #define DEBUG_EXTENSIONMETHODS

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using ActiproSoftware.ComponentModel;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents an <see cref="IProjectContent"/> implementation for a .NET assembly.
	/// </summary>
	internal class AssemblyProjectContent : DisposableObject, IProjectContent {

		#region Cache File Format
		/*
		 
		 Header (one instance)
		 - Int32: Version
		 - Int32: Hash code verification
		 - Int64: Date/time updated
		 - String: Assembly full name
		 - String: Assembly location
		 - Int64: Assembly size
		 - Int32: Referenced assembly count
		 - String (0+): Referenced assemblies		  
		 - Int32: Type table entry count
		 
		 TypeTable Entry (done to pre-load types so that type references to types defined in the assembly can be direct references to the types)
		 - String: Type full name
		 
		 NamespaceInfo Entry (one for each entry)
		 - String: Namespace name
		 - Int32: Child namespace count
		 - NamespaceInfo (0+): Child namespaces
		 - Int32: Child type count
		 - TypeInfo (0+): Child types
		 
		 TypeInfo Entry (one for each type)
		 - AssemblyDomType: Type
		 - Int32: Nested type count
		 - TypeInfo (0+): Nested types
		 
		 Type and Type Reference Entry (one for each type)
		 --- Type Reference Only ---------------------------------------------------------------------
		 - Int32: Type (defined in this assembly) table index or -1 if external type (quits if >= 0)
		 --- All ------------------------------------------------------------------------------------
		 - String: Name
		 - Byte: Referenced assembly index
		 - String: Namespace
		 - Int32: Type flags
		 - Int32: Modifiers
		 - Int32: Generic type argument count
		 - IDomTypeReference (0+): Generic type arguments
		 --- Type Reference Only ---------------------------------------------------------------------
		 - String: Declaring type full name
		 - Int32: Generic type parameter constraint count
		 - IDomTypeReference (0+): Generic type parameter constraints
		 - Int32: Array rank count
		 - Int32: Array rank (0+)
		 - Int32: Pointer Level
		 --- Type Only -------------------------------------------------------------------------------
		 - Bool: Has base type
		 - IDomTypeReference: Base type
		 - Bool: Has declaring type
		 - IDomTypeReference: Declaring type
		 - Int32: Interface type count
		 - IDomTypeReference (0+): Interface types
		 - Int32: Type member count
		 - AssemblyDomMember (0+): Type members

		 Member Entry (one for each member)
		 - String: Name
		 - Int32: Member flags
		 - Int32: Modifiers
		 - Int32: Generic type argument count
		 - IDomTypeReference (0+): Generic type arguments
		 - Int32: Parameter count
		 - AssemblyDomParameter (0+): Parameters
		 - Bool: Has return type
		 - IDomTypeReference: Return type
		 
		 Parameter Entry (one for each parameter)
		 - String: Name
		 - Int32: Parameter flags
		 - IDomTypeReference: Parameter type
		 
		 */
		#endregion

		#region Fully-Qualified Type Name Format
		/*

		 Type Name Syntax:
		 - . = Separates identifiers (e.g. System.Windows.Forms.Control)
		 - + = Used in place of "." to separate an nested type (e.g. System.Windows.Forms.Control+ControlCollection)
		 - `<number> = Added at the end of a type to indicate the generic argument count (e.g. System.Collections.Generic.IComparer`1)

		 Member Syntax:
		 - .ctor = Instance constructor member name
		 - .cctor = Static constructor member name
		 
		 */
		#endregion

		//
		// NOTE: Any changes made to fields need to be persisted to the cache in AssemblyProjectContent and the cache version number must be incremented
		//

		private string					assemblyFullName;
		private string					assemblyLocation;
		private long					assemblySize;
		private AssemblyDocumentation	documentation;
		private StringCollection		namespaceNames			= new StringCollection();
		private string					realAssemblyFullName;
		private StringCollection		referencedAssemblies	= new StringCollection();
		private NamespaceInfo			rootNamespaceInfo		= new NamespaceInfo();
		private AssemblyDomType[]		typeTable;

		// Used during initial loading only
		private Type[]					types;

		internal static readonly int	HashCodeVerification	= "AssemblyProjectContent".GetHashCode();
		internal const int				Version					= 69;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INNER TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		#region CacheHeader Class

		/// <summary>
		/// Stores data for the cache header.
		/// </summary>
		internal class CacheHeader {

			internal int		Version;
			internal int		HashCodeVerification;
			internal long		LastWriteDateTime;
			internal string		AssemblyFullName;
			internal string		RealAssemblyFullName;
			internal string		AssemblyLocation;
			internal long		AssemblySize;
			
			/// <summary>
			/// Returns whether the cache header is valid.
			/// </summary>
			/// <returns>
			/// <c>true</c> if the cache header is valid; otherwise, <c>false</c>.
			/// </returns>
			internal bool IsValid() {
				if (this.Version != AssemblyProjectContent.Version)
					return false;
				else if (this.HashCodeVerification != AssemblyProjectContent.HashCodeVerification)
					return false;
				else if (!File.Exists(this.AssemblyLocation))
					return false;

				FileInfo fileInfo = new FileInfo(this.AssemblyLocation);
				if (this.AssemblySize != fileInfo.Length)
					return false;
				else if (this.LastWriteDateTime != fileInfo.LastWriteTimeUtc.Ticks)
					return false;

				return true;
			}

		}

		#endregion

		/// <summary>
		/// Stores data about a namespace node.
		/// </summary>
		private class NamespaceInfo {

			internal HybridDictionary			ChildNamespaces	= new HybridDictionary(); 
			internal string						NamespaceName;
			internal ArrayList					StandardModules	= new ArrayList();
			internal HybridDictionary			Types			= new HybridDictionary(); 

		}
		
		/// <summary>
		/// Stores data about a type node.
		/// </summary>
		private class TypeInfo {

			internal HybridDictionary			NestedTypes		= new HybridDictionary(); 
			internal AssemblyDomType			Type;

		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>AssemblyProjectContent</c> class.
		/// </summary>
		/// <param name="assembly">The <see cref="Assembly"/> that has the project content.</param>
		/// <param name="cachePath">The path to the cache folder.</param>
		/// <param name="documentationEnabled">Whether documentation loading is enabled.</param>
		internal AssemblyProjectContent(Assembly assembly, string cachePath, bool documentationEnabled) :
			this(assembly, assembly.FullName, assembly.Location, cachePath, documentationEnabled) {}
		
		/// <summary>
		/// Initializes a new instance of the <c>AssemblyProjectContent</c> class.
		/// </summary>
		/// <param name="assemblyFullName">The full name of the assembly.</param>
		/// <param name="assemblyLocation">The location of the assembly.</param>
		/// <param name="cachePath">The path to the cache folder.</param>
		/// <param name="documentationEnabled">Whether documentation loading is enabled.</param>
		internal AssemblyProjectContent(string assemblyFullName, string assemblyLocation, string cachePath, bool documentationEnabled) :
			this(null, assemblyFullName, assemblyLocation, cachePath, documentationEnabled) {}

		/// <summary>
		/// Initializes a new instance of the <c>AssemblyProjectContent</c> class.
		/// </summary>
		/// <param name="assembly">The <see cref="Assembly"/> that has the project content, if known.</param>
		/// <param name="assemblyFullName">The full name of the assembly.</param>
		/// <param name="assemblyLocation">The location of the assembly.</param>
		/// <param name="cachePath">The path to the cache folder.</param>
		/// <param name="documentationEnabled">Whether documentation loading is enabled.</param>
		internal AssemblyProjectContent(Assembly assembly, string assemblyFullName, string assemblyLocation, string cachePath, bool documentationEnabled) {
			// Initialize parameters
			this.assemblyFullName = assemblyFullName;
			this.realAssemblyFullName = (assembly != null ? assembly.FullName : assemblyFullName);
			this.assemblyLocation = assemblyLocation;

			// Get the assembly file size and last write date/time
			DateTime lastWriteDateTime = DateTime.MinValue;
			if ((assemblyLocation != null) && (File.Exists(assemblyLocation))) {
				FileInfo fileInfo = new FileInfo(assemblyLocation);
				lastWriteDateTime = fileInfo.LastWriteTimeUtc;
				assemblySize = fileInfo.Length;
			}

			bool loadedFromCache = false;
			if (cachePath != null) {
				// Try and load the cached reflection data
				string reflectionCachePath = Path.Combine(cachePath, this.CachedReflectionFilename);
				loadedFromCache = this.LoadFromCache(lastWriteDateTime, reflectionCachePath);
			}

			// If no cached reflection data has been loaded, use reflection to load the data...
			if (!loadedFromCache) {
				if (assembly == null)
					throw new ApplicationException(String.Format("Could not load reflection data for assembly '{0}' since no cache information was found.", assemblyFullName));

				// Add this assembly's full name as the first entry in the referenced assemblies
				this.GetReferencedAssemblyIndex(assemblyFullName);

				// Load namespaces
				try { 
					try {
						types = assembly.GetExportedTypes();
					}
					catch (Exception ex) {
						throw new ApplicationException(String.Format("Could not load exported type data for assembly '{0}'.", assemblyFullName), ex);
					}

					foreach (Type type in types) {
						// Build the type info
						TypeInfo typeInfo = new TypeInfo();
						try {
							typeInfo.Type = new AssemblyDomType(this, type);
						}
						catch (Exception ex) {
							throw new ApplicationException(String.Format("Could not load exported type data for assembly '{0}' and type '{1}'.", assemblyFullName, type.FullName), ex);
						}
						
						// Add to the reflection data
						if (typeInfo.Type.IsNested) {
							// Find the parent type
							TypeInfo parentTypeInfo = this.GetTypeInfo(type.DeclaringType.FullName);

							// Add to the parent type
							parentTypeInfo.NestedTypes[type.Name] = typeInfo;
						}
						else {
							// Get the namespace
							NamespaceInfo namespaceInfo = rootNamespaceInfo;
							if ((type.Namespace != null) && (type.Namespace.Length > 0)) {
								// Get the proper namespace part info
								string[] namespaceParts = type.Namespace.Split(new char[] { '.' });
								foreach (string namespacePart in namespaceParts)
									namespaceInfo = this.GetChildNamespaceInfo(namespaceInfo, namespacePart, true);
							}

							// Add to the parent namespace
							namespaceInfo.Types[type.Name] = typeInfo;

							// If the type is a standard module, add it to the standard modules collection
							if ((typeInfo.Type != null) && (typeInfo.Type.Type == DomTypeType.StandardModule))
								namespaceInfo.StandardModules.Add(typeInfo.Type);
						}
					}

					// Build the type table
					typeTable = new AssemblyDomType[types.Length];
					for (int index = 0; index < typeTable.Length; index++)
						typeTable[index] = (AssemblyDomType)this.GetType(null, types[index].FullName, DomBindingFlags.Default);
				}
				finally {
					types = null;
				}

				if (cachePath != null) {
					// Save the cached reflection data
					string reflectionCachePath = Path.Combine(cachePath, this.CachedReflectionFilename);
					this.SaveToCache(lastWriteDateTime, reflectionCachePath);
				}
			}

			// Resolve type placeholders to the actual type
			this.ResolveTypePlaceHolders();

			// Load namespace names
			this.UpdateNamespaceNames();

			// If documentation loading is enabled...
			if (documentationEnabled) {
				// Get the XML documentation file last write date/time
				string xmlDocumentationPath = this.GetXmlDocumentationPath(assembly);
				lastWriteDateTime = DateTime.MinValue;
				long xmlDocumentationSize = 0;
				if (xmlDocumentationPath != null) {
					FileInfo fileInfo = new FileInfo(xmlDocumentationPath);
					lastWriteDateTime = fileInfo.LastWriteTimeUtc;
					xmlDocumentationSize = fileInfo.Length;
				}
				
				if (cachePath != null) {
					// Try and load the cached documentation data
					string documentationCachePath = Path.Combine(cachePath, this.CachedDocumentationFilename);
					if (File.Exists(documentationCachePath))
						documentation = AssemblyDocumentation.LoadFromCache(lastWriteDateTime, documentationCachePath);

					// No cached documentation has been loaded so see if it's possible to load one based on the XML documentation (will write to the cache)
					if ((documentation == null) && (xmlDocumentationPath != null))
						documentation = AssemblyDocumentation.SaveToCache(xmlDocumentationPath, lastWriteDateTime, xmlDocumentationSize, documentationCachePath);
				}
				if ((documentation == null) && (xmlDocumentationPath != null)) {
					// Load the raw XML documentation
					documentation = new AssemblyDocumentation(xmlDocumentationPath);
				}
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// READ/WRITE CACHE PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Reader a <see cref="NamespaceInfo"/> node and its child nodes from a <see cref="BinaryReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to use.</param>
		/// <returns>The <see cref="NamespaceInfo"/> that was read.</returns>
		private NamespaceInfo ReadNamespaceInfoNode(BinaryReader reader) {
			NamespaceInfo namespaceInfo = new NamespaceInfo();

			// Read the namespace
			string namespaceName = reader.ReadString();
			if (namespaceName != String.Empty)
				namespaceInfo.NamespaceName = namespaceName;

			// Read child namespaces
			int childNamespaceCount = reader.ReadInt32();
			for (int index = 0; index < childNamespaceCount; index++) {
				NamespaceInfo childNamespaceInfo = this.ReadNamespaceInfoNode(reader);
				namespaceInfo.ChildNamespaces[childNamespaceInfo.NamespaceName] = childNamespaceInfo;
			}

			// Read types
			int typeCount = reader.ReadInt32();
			for (int index = 0; index < typeCount; index++) {
				TypeInfo typeInfo = this.ReadTypeInfoNode(reader);
				namespaceInfo.Types[typeInfo.Type.Name] = typeInfo;

				// If the type is a standard module, add it to the standard modules collection
				if (typeInfo.Type.Type == DomTypeType.StandardModule)
					namespaceInfo.StandardModules.Add(typeInfo.Type);
			}

			return namespaceInfo;
		}
		
		/// <summary>
		/// Reads an <see cref="AssemblyDomMember"/> from a <see cref="BinaryReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to use.</param>
		/// <param name="declaringType">The <see cref="AssemblyDomType"/> that declared the member.</param>
		/// <returns>The <see cref="AssemblyDomMember"/> that was read.</returns>
		private AssemblyDomMember ReadMember(BinaryReader reader, AssemblyDomType declaringType) {
			string name					= reader.ReadString();
			DomMemberFlags memberFlags	= (DomMemberFlags)reader.ReadInt32();
			Modifiers modifiers			= (Modifiers)reader.ReadInt32();

			int genericTypeArgumentCount = reader.ReadInt32();
			IDomTypeReference[] genericTypeArguments = null;
			if (genericTypeArgumentCount > 0) {
				genericTypeArguments = new IDomTypeReference[genericTypeArgumentCount];
				for (int index = 0; index < genericTypeArgumentCount; index++)
					genericTypeArguments[index] = this.ReadTypeReference(reader);
			}

			int parameterCount = reader.ReadInt32();
			AssemblyDomParameter[] parameters = null;
			if (parameterCount > 0) {
				parameters = new AssemblyDomParameter[parameterCount];
				for (int index = 0; index < parameterCount; index++)
					parameters[index] = this.ReadParameter(reader);
			}

			// Create the member (use various types depending on need to save on memory)
			AssemblyDomMember member;
			if (genericTypeArgumentCount > 0)
				member = new AssemblyDomGenericMember(declaringType, memberFlags, name, modifiers);
			else if (parameterCount > 0)
				member = new AssemblyDomParameterizedMember(declaringType, memberFlags, name, modifiers);
			else
				member = new AssemblyDomMember(declaringType, memberFlags, name, modifiers);

			// Set generic type arguments and parameters
			if (genericTypeArgumentCount > 0)
				((AssemblyDomGenericMember)member).SetGenericTypeArguments(genericTypeArguments);
			if (parameterCount > 0)
				((AssemblyDomParameterizedMember)member).SetParameters(parameters);

			bool hasReturnType = reader.ReadBoolean();
			if (hasReturnType)
				member.SetReturnType(this.ReadTypeReference(reader));

			return member;
		}

		/// <summary>
		/// Reads an <see cref="AssemblyDomParameter"/> from a <see cref="BinaryReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to use.</param>
		/// <returns>The <see cref="AssemblyDomParameter"/> that was read.</returns>
		private AssemblyDomParameter ReadParameter(BinaryReader reader) {
			string name = reader.ReadString();
			DomParameterFlags parameterFlags = (DomParameterFlags)reader.ReadInt32();
			IDomTypeReference parameterType = this.ReadTypeReference(reader);

			return new AssemblyDomParameter(name, parameterFlags, parameterType);
		}

		/// <summary>
		/// Reads an <see cref="AssemblyDomType"/> from a <see cref="BinaryReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to use.</param>
		/// <returns>The <see cref="AssemblyDomType"/> that was read.</returns>
		private AssemblyDomType ReadType(BinaryReader reader) {
			AssemblyDomType type = new AssemblyDomType(this);

			this.ReadTypeBase(reader, type);

			bool hasBaseType = reader.ReadBoolean();
			if (hasBaseType)
				type.SetBaseType(this.ReadTypeReference(reader));

			bool hasDeclaringType = reader.ReadBoolean();
			if (hasDeclaringType)
				type.SetDeclaringType(this.ReadTypeReference(reader));

			int interfaceTypeCount = reader.ReadInt32();
			if (interfaceTypeCount > 0) {
				IDomTypeReference[] interfaceTypes = new IDomTypeReference[interfaceTypeCount];
				for (int index = 0; index < interfaceTypeCount; index++)
					interfaceTypes[index] = this.ReadTypeReference(reader);
				type.SetInterfaceTypes(interfaceTypes);
			}

			int memberCount = reader.ReadInt32();
			AssemblyDomMember[] members = new AssemblyDomMember[memberCount];
			for (int index = 0; index < memberCount; index++)
				members[index] = this.ReadMember(reader, type);
			type.SetMembers(members);

			return type;
		}
		
		/// <summary>
		/// Reads an <see cref="AssemblyDomTypeBase"/> from a <see cref="BinaryReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to use.</param>
		/// <param name="typeReference">The existing <see cref="AssemblyDomTypeBase"/> to fill.</param>
		private void ReadTypeBase(BinaryReader reader, AssemblyDomTypeBase typeReference) {
			typeReference.SetName(reader.ReadString());
			typeReference.AssemblyIndex = reader.ReadByte();
			string @namespace = reader.ReadString();
			if (@namespace.Length > 0)
				typeReference.SetNamespace(@namespace);
			typeReference.TypeFlags = (DomTypeFlags)reader.ReadInt32();
			typeReference.SetModifiers((Modifiers)reader.ReadInt32());

			int genericTypeArgumentCount = reader.ReadInt32();
			if (genericTypeArgumentCount > 0) {
				IDomTypeReference[] genericTypeArguments = new IDomTypeReference[genericTypeArgumentCount];
				for (int index = 0; index < genericTypeArgumentCount; index++)
					genericTypeArguments[index] = this.ReadTypeReference(reader);
				typeReference.SetGenericTypeArguments(genericTypeArguments);
			}
		}

		/// <summary>
		/// Reader a <see cref="TypeInfo"/> node and its child nodes from a <see cref="BinaryReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to use.</param>
		/// <returns>The <see cref="TypeInfo"/> that was read.</returns>
		private TypeInfo ReadTypeInfoNode(BinaryReader reader) {
			TypeInfo typeInfo = new TypeInfo();

			// Read the type
			typeInfo.Type = this.ReadType(reader);

			// Read nested types
			int nestedTypeCount = reader.ReadInt32();
			for (int index = 0; index < nestedTypeCount; index++) {
				TypeInfo nestedTypeInfo = this.ReadTypeInfoNode(reader);
				typeInfo.NestedTypes[nestedTypeInfo.Type.Name] = nestedTypeInfo;
			}

			return typeInfo;
		}
		
		/// <summary>
		/// Reads an <see cref="IDomTypeReference"/> from a <see cref="BinaryReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to use.</param>
		/// <returns>The <see cref="IDomTypeReference"/> that was read.</returns>
		private IDomTypeReference ReadTypeReference(BinaryReader reader) {
			// Read a type table index that indicates if the type reference is to a type in the same assembly
			int typeTableIndex = reader.ReadInt32();
			if (typeTableIndex != -1) {
				// Return a type placeholder
				return new AssemblyDomTypePlaceHolder(null, typeTableIndex);
			}

			AssemblyDomTypeReference typeReference = new AssemblyDomTypeReference(this);

			this.ReadTypeBase(reader, typeReference);

			typeReference.DeclaringTypeFullName = reader.ReadString();

			int genericTypeParameterConstraintCount = reader.ReadInt32();
			if (genericTypeParameterConstraintCount > 0) {
				IDomTypeReference[] genericTypeParameterConstraints = new IDomTypeReference[genericTypeParameterConstraintCount];
				for (int index = 0; index < genericTypeParameterConstraintCount; index++)
					genericTypeParameterConstraints[index] = this.ReadTypeReference(reader);
				typeReference.SetGenericTypeParameterConstraints(genericTypeParameterConstraints);
			}
			
			DomArrayPointerInfo arrayPointerInfo = null;
			int arrayRankCount = reader.ReadInt32();
			if (arrayRankCount > 0) {
				int[] arrayRanks = new int[arrayRankCount];
				for (int index = 0; index < arrayRankCount; index++)
					arrayRanks[index] = reader.ReadInt32();
				arrayPointerInfo = new DomArrayPointerInfo(arrayRanks);
			}
			int pointerLevel = reader.ReadInt32();
			if (pointerLevel > 0) {
				if (arrayPointerInfo == null)
					arrayPointerInfo = new DomArrayPointerInfo(pointerLevel);
				else
					arrayPointerInfo.PointerLevel = pointerLevel;
			}
			typeReference.ArrayPointerInfo = arrayPointerInfo;
			
			return typeReference;
		}

		/// <summary>
		/// Writes an <see cref="AssemblyDomMember"/> to a <see cref="BinaryWriter"/>.
		/// </summary>
		/// <param name="writer">The <see cref="BinaryWriter"/> to use.</param>
		/// <param name="member">The <see cref="AssemblyDomMember"/> to write.</param>
		private void WriteMember(BinaryWriter writer, AssemblyDomMember member) {
			#if DEBUG && DEBUG_CACHE_WRITING
			Trace.WriteLine("Member: " + member.Name);
			Trace.Indent();
			#endif

			writer.Write(member.Name);
			writer.Write((Int32)member.MemberFlags);
			writer.Write((Int32)member.Modifiers);

			if (member.GenericTypeArguments != null) {
				writer.Write(member.GenericTypeArguments.Count);
				foreach (IDomTypeReference genericTypeArgument in member.GenericTypeArguments)
					this.WriteTypeReference(writer, genericTypeArgument);
			}
			else
				writer.Write((Int32)0);
			
			if (member.Parameters != null) {
				writer.Write(member.Parameters.Length);
				foreach (AssemblyDomParameter parameter in member.Parameters)
					this.WriteParameter(writer, parameter);
			}
			else
				writer.Write((Int32)0);
			
			writer.Write(member.ReturnType != null);
			if (member.ReturnType != null)
				this.WriteTypeReference(writer, member.ReturnType);

			#if DEBUG && DEBUG_CACHE_WRITING
			Trace.Unindent();
			#endif
		}

		/// <summary>
		/// Writes a <see cref="NamespaceInfo"/> node and its child nodes to a <see cref="BinaryWriter"/>.
		/// </summary>
		/// <param name="writer">The <see cref="BinaryWriter"/> to use.</param>
		/// <param name="namespaceInfo">The <see cref="NamespaceInfo"/> to write.</param>
		private void WriteNamespaceInfoNode(BinaryWriter writer, NamespaceInfo namespaceInfo) {
			#if DEBUG && DEBUG_CACHE_WRITING
			Trace.WriteLine("Namespace: " + namespaceInfo.NamespaceName);
			Trace.Indent();
			#endif

			// Write the namespace
			writer.Write(namespaceInfo.NamespaceName != null ? namespaceInfo.NamespaceName : String.Empty);

			// Write child namespaces
			writer.Write(namespaceInfo.ChildNamespaces.Count);
			foreach (NamespaceInfo childNamespaceInfo in namespaceInfo.ChildNamespaces.Values)
				this.WriteNamespaceInfoNode(writer, childNamespaceInfo);

			// Write types
			writer.Write(namespaceInfo.Types.Count);
			foreach (TypeInfo typeInfo in namespaceInfo.Types.Values)
				this.WriteTypeInfoNode(writer, typeInfo);

			#if DEBUG && DEBUG_CACHE_WRITING
			Trace.Unindent();
			#endif
		}
		
		/// <summary>
		/// Writes an <see cref="AssemblyDomParameter"/> to a <see cref="BinaryWriter"/>.
		/// </summary>
		/// <param name="writer">The <see cref="BinaryWriter"/> to use.</param>
		/// <param name="parameter">The <see cref="AssemblyDomParameter"/> to write.</param>
		private void WriteParameter(BinaryWriter writer, AssemblyDomParameter parameter) {
			writer.Write(parameter.Name != null ? parameter.Name : String.Empty);
			writer.Write((Int32)parameter.ParameterFlags);
			this.WriteTypeReference(writer, parameter.ParameterType);
		}

		/// <summary>
		/// Writes an <see cref="AssemblyDomType"/> to a <see cref="BinaryWriter"/>.
		/// </summary>
		/// <param name="writer">The <see cref="BinaryWriter"/> to use.</param>
		/// <param name="type">The <see cref="AssemblyDomType"/> to write.</param>
		private void WriteType(BinaryWriter writer, AssemblyDomType type) {
			#if DEBUG && DEBUG_CACHE_WRITING
			Trace.WriteLine("Type: " + type.Name);
			Trace.Indent();
			#endif

			this.WriteTypeBase(writer, type);

			writer.Write(type.BaseType != null);
			if (type.BaseType != null) {
				#if DEBUG && DEBUG_CACHE_WRITING
				Trace.WriteLine("Base Type: " + type.BaseType.FullName);
				#endif

				this.WriteTypeReference(writer, type.BaseType);
			}
			
			writer.Write(type.DeclaringType != null);
			if (type.DeclaringType != null)
				this.WriteTypeReference(writer, type.DeclaringType);

			IDomTypeReference[] interfaceTypes = type.GetInterfaces();
			if (interfaceTypes != null) {
				writer.Write(interfaceTypes.Length);
				foreach (IDomTypeReference interfaceType in interfaceTypes)
					this.WriteTypeReference(writer, interfaceType);
			}
			else
				writer.Write((Int32)0);

			IDomMember[] members = type.GetMembers();
			writer.Write(members.Length);
			foreach (AssemblyDomMember member in members)
				this.WriteMember(writer, member);

			#if DEBUG && DEBUG_CACHE_WRITING
			Trace.Unindent();
			#endif
		}
		
		/// <summary>
		/// Writes an <see cref="AssemblyDomTypeBase"/> to a <see cref="BinaryWriter"/>.
		/// </summary>
		/// <param name="writer">The <see cref="BinaryWriter"/> to use.</param>
		/// <param name="typeReference">The <see cref="AssemblyDomTypeBase"/> to write.</param>
		private void WriteTypeBase(BinaryWriter writer, AssemblyDomTypeBase typeReference) {
			writer.Write(typeReference.Name);
			writer.Write(typeReference.AssemblyIndex);
			writer.Write(typeReference.Namespace != null ? typeReference.Namespace : String.Empty);
			writer.Write((Int32)typeReference.TypeFlags);
			writer.Write((Int32)typeReference.Modifiers);
			
			if (typeReference.GenericTypeArguments != null) {
				writer.Write(typeReference.GenericTypeArguments.Count);
				foreach (IDomTypeReference genericTypeArgument in typeReference.GenericTypeArguments)
					this.WriteTypeReference(writer, genericTypeArgument);
			}
			else
				writer.Write((Int32)0);
		}

		/// <summary>
		/// Writes a <see cref="TypeInfo"/> node and its child nodes to a <see cref="BinaryWriter"/>.
		/// </summary>
		/// <param name="writer">The <see cref="BinaryWriter"/> to use.</param>
		/// <param name="typeInfo">The <see cref="TypeInfo"/> to write.</param>
		private void WriteTypeInfoNode(BinaryWriter writer, TypeInfo typeInfo) {
			// Write the type
			this.WriteType(writer, typeInfo.Type);

			// Write nested types
			writer.Write(typeInfo.NestedTypes.Count);
			foreach (TypeInfo nestedTypeInfo in typeInfo.NestedTypes.Values)
				this.WriteTypeInfoNode(writer, nestedTypeInfo);
		}
		
		/// <summary>
		/// Writes an <see cref="IDomTypeReference"/> to a <see cref="BinaryWriter"/>.
		/// </summary>
		/// <param name="writer">The <see cref="BinaryWriter"/> to use.</param>
		/// <param name="typeReference">The <see cref="IDomTypeReference"/> to write.</param>
		private void WriteTypeReference(BinaryWriter writer, IDomTypeReference typeReference) {
			if (typeReference is AssemblyDomTypePlaceHolder) {
				// Double check valid type table index
				if ((((AssemblyDomTypePlaceHolder)typeReference).TypeTableIndex < 0) || (((AssemblyDomTypePlaceHolder)typeReference).TypeTableIndex >= typeTable.Length))
					throw new ApplicationException("Invalid type table index.");

				// Write the type table index
				writer.Write(((AssemblyDomTypePlaceHolder)typeReference).TypeTableIndex);
				return;
			}

			// Flag that it is an external type reference
			writer.Write((int)-1);

			this.WriteTypeBase(writer, (AssemblyDomTypeReference)typeReference);

			writer.Write(((AssemblyDomTypeReference)typeReference).DeclaringTypeFullName != null ? ((AssemblyDomTypeReference)typeReference).DeclaringTypeFullName : String.Empty);

			if (typeReference.GenericTypeParameterConstraints != null) {
				writer.Write(typeReference.GenericTypeParameterConstraints.Count);
				foreach (IDomTypeReference genericTypeParameterConstraint in typeReference.GenericTypeParameterConstraints)
					this.WriteTypeReference(writer, genericTypeParameterConstraint);
			}
			else
				writer.Write((Int32)0);

			int[] arrayRanks = typeReference.ArrayRanks;
			if (arrayRanks != null) {
				writer.Write(arrayRanks.Length);
				foreach (int arrayRank in arrayRanks)
					writer.Write(arrayRank);
			}
			else
				writer.Write((Int32)0);
			writer.Write(typeReference.PointerLevel);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the filename for the documentation cache.
		/// </summary>
		/// <value>The filename for the documentation cache.</value>
		private string CachedDocumentationFilename {
			get {
				string assemblyName = assemblyFullName;
				int commaIndex = assemblyName.IndexOf(',');
				if (commaIndex != -1)
					assemblyName = assemblyName.Substring(0, commaIndex);

				int hashCode = assemblyFullName.GetHashCode() ^ (assemblyLocation != null ? assemblyLocation.GetHashCode() : 0) ^ assemblySize.GetHashCode();
				return String.Format("{0}.{1}.Documentation.dat", assemblyName, hashCode.ToString("x"));
			}
		}

		/// <summary>
		/// Gets the filename for the reflection cache.
		/// </summary>
		/// <value>The filename for the reflection cache.</value>
		private string CachedReflectionFilename {
			get {
				string assemblyName = assemblyFullName;
				int commaIndex = assemblyName.IndexOf(',');
				if (commaIndex != -1)
					assemblyName = assemblyName.Substring(0, commaIndex);

				int hashCode = assemblyFullName.GetHashCode() ^ (assemblyLocation != null ? assemblyLocation.GetHashCode() : 0) ^ assemblySize.GetHashCode();
				return String.Format("{0}.{1}.Reflection.dat", assemblyName, hashCode.ToString("x"));
			}
		}
		
		/// <summary>
		/// Gets the <see cref="AssemblyDocumentation"/> if one exists.
		/// </summary>
		/// <value>The <see cref="AssemblyDocumentation"/> if one exists.</value>
		internal AssemblyDocumentation Documentation {
			get {
				return documentation;
			}
		}

		/// <summary>
		/// Gets a <see cref="NamespaceInfo"/> object for the specified child namespace name and creates one if necessary.
		/// </summary>
		/// <param name="parentNamespaceInfo">The parent <see cref="NamespaceInfo"/>.</param>
		/// <param name="childNamespaceName">The child namespace name.</param>
		/// <param name="createIfNecessary">Whether to create a <see cref="NamespaceInfo"/> if none is found.</param>
		/// <returns>A <see cref="NamespaceInfo"/> object for the specified child namespace name.</returns>
		private NamespaceInfo GetChildNamespaceInfo(NamespaceInfo parentNamespaceInfo, string childNamespaceName, bool createIfNecessary) {
			NamespaceInfo namespaceInfo = (NamespaceInfo)parentNamespaceInfo.ChildNamespaces[childNamespaceName];
			if ((namespaceInfo == null) && (createIfNecessary)) {
				namespaceInfo = new NamespaceInfo();
				namespaceInfo.NamespaceName = childNamespaceName;
				parentNamespaceInfo.ChildNamespaces[childNamespaceName] = namespaceInfo;
			}
			return namespaceInfo;
		}
		
		/// <summary>
		/// Gets a <see cref="NamespaceInfo"/> object for the specified namespace name.
		/// </summary>
		/// <param name="namespaceName">The namespace name for which to search.</param>
		/// <returns>A <see cref="NamespaceInfo"/> object for the specified namespace name.</returns>
		private NamespaceInfo GetNamespaceInfo(string namespaceName) {
			NamespaceInfo namespaceInfo = rootNamespaceInfo;
			if ((namespaceName != null) && (namespaceName.Length > 0)) {
				string[] namespaceParts = namespaceName.Split(new char[] { '.' });
				foreach (string namespacePart in namespaceParts) {
					namespaceInfo = this.GetChildNamespaceInfo(namespaceInfo, namespacePart, false);
					if (namespaceInfo == null)
						return null;
				}
			}
			return namespaceInfo;
		}

		/// <summary>
		/// Returns the referenced assembly with the specified index.
		/// </summary>
		/// <param name="index">The index of the referenced assembly to retrieve.</param>
		/// <returns>The referenced assembly with the specified index.</returns>
		internal string GetReferencedAssembly(int index) {
			return referencedAssemblies[index];
		}
		
		/// <summary>
		/// Returns the index of the specified assembly within the referenced assemblies collection.
		/// </summary>
		/// <param name="assemblyName">The name of the assembly.</param>
		/// <returns>The index of the specified assembly within the referenced assemblies collection.</returns>
		internal int GetReferencedAssemblyIndex(string assemblyName) {
			assemblyName = assemblyName.Trim().ToLower();

			// Perform a mapping from the real assembly full name to an alias if one was passed (for dynamically generated assemblies)
			if (String.Compare(assemblyName, realAssemblyFullName, true) == 0)
				assemblyName = assemblyFullName.ToLower();

			int index = referencedAssemblies.IndexOf(assemblyName);
			if (index == -1)
				index = referencedAssemblies.Add(assemblyName);

			return index;
		}
		
		/// <summary>
		/// Gets a <see cref="TypeInfo"/> object for the specified type name.
		/// </summary>
		/// <param name="typeFullName">The type name for which to search.</param>
		/// <returns>A <see cref="TypeInfo"/> object for the specified type name.</returns>
		private TypeInfo GetTypeInfo(string typeFullName) {
			if ((typeFullName == null) || (typeFullName.Length == 0))
				return null;

			// Change nested type specification work with lookup code below
			typeFullName = typeFullName.Replace('+', '.');

			string[] identifiers = typeFullName.Split(new char[] { '.' });
			NamespaceInfo namespaceInfo = rootNamespaceInfo;
			TypeInfo typeInfo = null;
			foreach (string identifier in identifiers) {
				if (typeInfo != null) {
					// Check for a nested type
					typeInfo = (TypeInfo)typeInfo.NestedTypes[identifier];
					if (typeInfo == null)
						return null;
				}
				else {
					// Check for a type in the namespace
					typeInfo = (TypeInfo)namespaceInfo.Types[identifier];
					if (typeInfo == null) {
						// Check for a child namespace in the namespace
						namespaceInfo = this.GetChildNamespaceInfo(namespaceInfo, identifier, false);
						if (namespaceInfo == null)
							return null;
					}
				}
			}

			return typeInfo;
		}

		/// <summary>
		/// Returns an <see cref="IDomTypeReference"/> for the specified <see cref="Type"/>.
		/// </summary>
		/// <param name="type">The <see cref="Type"/> that needs a reference.</param>
		/// <returns>An <see cref="IDomTypeReference"/> for the specified <see cref="Type"/>.</returns>
		internal IDomTypeReference GetTypeReference(Type type) {
			return this.GetTypeReference(null, type);
		}
		
		/// <summary>
		/// Returns an <see cref="IDomTypeReference"/> for the specified <see cref="Type"/>.
		/// </summary>
		/// <param name="callingGenericTypes">The calling generic type array, used to prevent infinite recursion with generic constraints on a generic.</param>
		/// <param name="type">The <see cref="Type"/> that needs a reference.</param>
		/// <returns>An <see cref="IDomTypeReference"/> for the specified <see cref="Type"/>.</returns>
		internal IDomTypeReference GetTypeReference(IDomTypeReference[] callingGenericTypes, Type type) {
			// If the type is defined in the same assembly...
			int assemblyIndex = this.GetReferencedAssemblyIndex(type.Assembly.FullName);
			if (assemblyIndex == 0) {
				int typeTableIndex = Array.IndexOf(types, type);
				if (typeTableIndex != -1)
					return new AssemblyDomTypePlaceHolder(type, typeTableIndex);
			}

			return new AssemblyDomTypeReference(this, callingGenericTypes, type);
		}
		
		/// <summary>
		/// Returns the path for the XML documentation file, if one exists.
		/// </summary>
		/// <param name="assembly">The <see cref="Assembly"/> for which to obtain documentation.</param>
		/// <returns>The path for the XML documentation file, if one exists.</returns>
		private string GetXmlDocumentationPath(Assembly assembly) {
			// Quit if there is no assembly location
			if ((assemblyLocation == null) || (assemblyLocation.Trim().Length == 0))
				return null;

			// Get the XML filename
			string xmlFilename = Path.GetFileNameWithoutExtension(assemblyLocation) + ".xml";

			// See if there is an XML documentation file in the folder where the assembly is located
			string path;
			try {
				path = Path.Combine(Path.GetDirectoryName(assemblyLocation), xmlFilename);
				if (File.Exists(path))
					return path;
			}
			catch {
				return null;
			}
			
			// Get the search paths (have to do all this to properly handle 64-bit computers)
			string net20FrameworkPath = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory() ?? String.Empty;  // .NET 2.0 and earlier
			net20FrameworkPath = net20FrameworkPath.Replace(@"Microsoft.NET\Framework64", @"Microsoft.NET\Framework");
			string[] searchPaths = new string[] {
				net20FrameworkPath,
				@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0",
				@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5",
				@"C:\Program Files\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0",
				net20FrameworkPath.Replace(@"Microsoft.NET\Framework", @"Microsoft.NET\Framework64"),
				@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\v3.0",
				@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\v3.5",
				@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0",
			};
			foreach (string searchPath in searchPaths) {
				if (Directory.Exists(searchPath)) {
					// See if there is an XML documentation file in the search path
					path = Path.Combine(searchPath, xmlFilename);
					if (File.Exists(path))
						return path;
				}

				// 1/26/2009 - Try culture-specific child folder (was hard-coded "en" before) (034-12F1DA0F-6AFA)
				if (Directory.Exists(Path.Combine(searchPath, CultureInfo.CurrentCulture.TwoLetterISOLanguageName))) {  
					// See if there is an XML documentation file in the search path
					path = Path.Combine(Path.Combine(searchPath, CultureInfo.CurrentCulture.TwoLetterISOLanguageName), xmlFilename);
					if (File.Exists(path))
						return path;
				}
			}

			// If the assembly is an Actipro one but documentation has not yet been found...
			if ((assemblyFullName.ToLower().StartsWith("actiprosoftware.")) && (assembly != null)) {
				AssemblyName assemblyName = assembly.GetName();
				string name = assemblyName.Name.ToLower().Substring("actiprosoftware.".Length);
				if ((name.EndsWith(".net11")) || (name.EndsWith(".net20")))
					name = name.Substring(0, name.Length - 6);

				switch (name) {
					case "shared":
					case "winuicore":
						#if DEBUG
						path = Path.Combine(@"C:\Code\ActiproSoftware\Common\Deploy\Dotfuscator10", xmlFilename);
						#else
						// Look in the Common Files folder
						path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles), 
							String.Format(@"Actipro Software\{0}\v{1}.{2}.{3:D4}\{4}", name, assemblyName.Version.Major, assemblyName.Version.Minor, assemblyName.Version.Build, xmlFilename));
						#endif
						if (File.Exists(path))
							return path;
						break;
					default:
						#if DEBUG
						path = Path.Combine(String.Format( @"C:\Code\ActiproSoftware\{0}4\Deploy\Dotfuscator", name), xmlFilename); // NOTE: Remove 4 after directories change
						#else
						// Look in the Program Files folder
						path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), 
							String.Format(@"Actipro Software\{0}\v{1}.{2}.{3:D4}\{4}", name, assemblyName.Version.Major, assemblyName.Version.Minor, assemblyName.Version.Build, xmlFilename));
						#endif
						if (File.Exists(path))
							return path;
						break;
				}
			}

			return null;
		}
		
		/// <summary>
		/// Returns whether the <see cref="IDomType"/> matches with the desired <see cref="DomBindingFlags"/>.
		/// </summary>
		/// <param name="contextInheritanceHierarchy">
		/// An optional array of the inheritance hierarchy of the context <see cref="IDomType"/>.
		/// The first array item contains the context <see cref="IDomType"/>.
		/// Each following item indicates a base <see cref="IDomType"/> of the context <see cref="IDomType"/>.
		/// </param>
		/// <param name="type">The <see cref="IDomType"/> to examine.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>
		/// <c>true</c> if the <see cref="IDomType"/> matches with the desired <see cref="DomBindingFlags"/>; otherwise, <c>false</c>.
		/// </returns>
		internal static bool IsMatch(IDomType[] contextInheritanceHierarchy, IDomType type, DomBindingFlags flags) {
			// If there must be an accessible constructor...
			if ((flags & DomBindingFlags.HasConstructor) == DomBindingFlags.HasConstructor) {
				Modifiers accessModifiers = type.GetConstructorAccessModifiers();

				if ((accessModifiers & Modifiers.Public) == Modifiers.Public)
					return true;
				else if ((type.ProjectContent == null) && ((accessModifiers & Modifiers.Assembly) == Modifiers.Assembly))
					return true;
				if ((contextInheritanceHierarchy != null) && (contextInheritanceHierarchy.Length > 0)) {
					if (
						((type.ProjectContent != null) && (contextInheritanceHierarchy[0] == type)) ||
						((type.ProjectContent == null) && (contextInheritanceHierarchy[0].FullName == type.FullName))
						) {
						if ((accessModifiers & Modifiers.Private) == Modifiers.Private)
							return true;
					}

					if ((accessModifiers & Modifiers.Family) == Modifiers.Family) {
						if (type.ProjectContent != null) {
							// If there is a project content, do a simple match
							return (Array.IndexOf(contextInheritanceHierarchy, type) != -1);
						}
						else {
							// Match by type full name
							foreach (IDomType inheritedType in contextInheritanceHierarchy) {
								if (inheritedType.FullName == type.FullName)
									return true;
							}
						}
					}
				}
				return false;
			}

			return true;
		}
		
		/// <summary>
		/// Loads the <see cref="AssemblyProjectContent"/> from the specified path.
		/// </summary>
		/// <param name="dateTime">The last modification date/time of the assembly.</param>
		/// <param name="path">The path to the cached reflection file.</param>
		/// <returns>
		/// <c>true</c> if the file was loaded; otherwise, <c>false</c>.
		/// </returns>
		private bool LoadFromCache(DateTime dateTime, string path) {
			// Quit if there is no file
			if (!File.Exists(path))
				return false;

			// Open the cache file
			using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)) {
				using (BinaryReader reader = new BinaryReader(stream)) {
					// Check the version
					if (reader.ReadInt32() != AssemblyProjectContent.Version)
						return false;

					// Check the hash code verification
					if (reader.ReadInt32() != AssemblyProjectContent.HashCodeVerification)
						return false;

					// Check the date/time
					if (reader.ReadInt64() != dateTime.Ticks)
						return false;

					// Get the full name and location
					assemblyFullName = reader.ReadString();
					realAssemblyFullName = reader.ReadString();
					assemblyLocation = reader.ReadString();

					// Check the size
					if (reader.ReadInt64() != assemblySize)
						return false;

					// Read the referenced assemblies
					int referencedAssemblyCount = reader.ReadInt32();
					for (int index = 0; index < referencedAssemblyCount; index++)
						referencedAssemblies.Add(reader.ReadString());

					// Load the type table full names
					string[] typeTableFullNames = new string[reader.ReadInt32()];
					for (int index = 0; index < typeTableFullNames.Length; index++)
						typeTableFullNames[index] = reader.ReadString();

					// Read the namespace info node tree
					rootNamespaceInfo = this.ReadNamespaceInfoNode(reader);

					// Build the type table
					typeTable = new AssemblyDomType[typeTableFullNames.Length];
					for (int index = 0; index < typeTable.Length; index++)
						typeTable[index] = (AssemblyDomType)this.GetType(null, typeTableFullNames[index], DomBindingFlags.Default);
				}
			}

			return true;
		}
		
		/// <summary>
		/// Loads the <see cref="CacheHeader"/> from the specified cache file.
		/// </summary>
		/// <param name="path">The path to the cached reflection file.</param>
		/// <returns>The <see cref="CacheHeader"/> that was loaded.</returns>
		internal static CacheHeader LoadHeaderFromCache(string path) {
			// Quit if there is no file
			if (!File.Exists(path))
				return null;

			// Open the cache file
			using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)) {
				using (BinaryReader reader = new BinaryReader(stream)) {
					CacheHeader header = new CacheHeader();
					if (stream.Length > 0) {
						header.Version = reader.ReadInt32();
						header.HashCodeVerification = reader.ReadInt32();
						header.LastWriteDateTime = reader.ReadInt64();
						header.AssemblyFullName = reader.ReadString();
						header.RealAssemblyFullName = reader.ReadString();
						header.AssemblyLocation = reader.ReadString();
						header.AssemblySize = reader.ReadInt64();
					}
					return header;
				}
			}
		}

		/// <summary>
		/// Recursively loads the namespace names.
		/// </summary>
		/// <param name="namespaceInfo">The <see cref="NamespaceInfo"/> to examine.</param>
		/// <param name="namespaceBase">The base namespace name.</param>
		private void LoadNamespaceNames(NamespaceInfo namespaceInfo, string namespaceBase) {
			string namespaceName = namespaceBase;
			if (namespaceInfo.NamespaceName != null) {
				namespaceName += (namespaceName.Length > 0 ? "." : String.Empty) + namespaceInfo.NamespaceName;
				if (namespaceInfo.Types.Count > 0)
					namespaceNames.Add(namespaceName);
			}

			foreach (NamespaceInfo childNamespaceInfo in namespaceInfo.ChildNamespaces.Values)
				this.LoadNamespaceNames(childNamespaceInfo, namespaceName);
		}

		/// <summary>
		/// Resolves an <see cref="AssemblyDomTypePlaceHolder"/> into a <see cref="AssemblyDomType"/>.
		/// </summary>
		/// <param name="placeHolder">The <see cref="AssemblyDomTypePlaceHolder"/> to examine.</param>
		/// <returns>The <see cref="AssemblyDomType"/> that was found.</returns>
		internal AssemblyDomType ResolveAssemblyDomTypePlaceHolder(AssemblyDomTypePlaceHolder placeHolder) {
			return typeTable[placeHolder.TypeTableIndex];
		}

		/// <summary>
		/// Resolve all references to types in the same assembly to the actual type.
		/// </summary>
		private void ResolveTypePlaceHolders() {
			this.ResolveTypePlaceHolders(rootNamespaceInfo);
		}
		
		/// <summary>
		/// Resolve all references to types in the same assembly to the actual type.
		/// </summary>
		/// <param name="namespaceInfo">The <see cref="NamespaceInfo"/> to examine.</param>
		private void ResolveTypePlaceHolders(NamespaceInfo namespaceInfo) {
			foreach (TypeInfo typeInfo in namespaceInfo.Types.Values)
				this.ResolveTypePlaceHolders(typeInfo);

			foreach (NamespaceInfo childNamespaceInfo in namespaceInfo.ChildNamespaces.Values)
				this.ResolveTypePlaceHolders(childNamespaceInfo);
		}
		
		/// <summary>
		/// Resolve all references to types in the same assembly to the actual type.
		/// </summary>
		/// <param name="typeInfo">The <see cref="TypeInfo"/> to examine.</param>
		private void ResolveTypePlaceHolders(TypeInfo typeInfo) {
			typeInfo.Type.ResolveTypePlaceHolders(this);

			foreach (TypeInfo nestedTypeInfo in typeInfo.NestedTypes.Values)
				this.ResolveTypePlaceHolders(nestedTypeInfo);
		}
		
		/// <summary>
		/// Saves assembly project content to a cache file.
		/// </summary>
		/// <param name="dateTime">The last modification date/time of the assembly.</param>
		/// <param name="path">The path to the cached reflection file.</param>
		private void SaveToCache(DateTime dateTime, string path) {
			// Create the cache directory if necessary
			if (!Directory.Exists(Path.GetDirectoryName(path)))
				Directory.CreateDirectory(Path.GetDirectoryName(path));

			// Open a writer to the file
			using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None)) {
				using (BinaryWriter writer = new BinaryWriter(stream)) {
					#if DEBUG && DEBUG_CACHE_WRITING
					Trace.WriteLine("SaveToCache: " + path);
					Trace.Indent();
					Trace.WriteLine("Full Name: " + assemblyFullName);
					Trace.WriteLine("Real Full Name: " + realAssemblyFullName);
					#endif

					// Write the header
					writer.Write(AssemblyProjectContent.Version);
					writer.Write(AssemblyProjectContent.HashCodeVerification);
					writer.Write(dateTime.Ticks);
					writer.Write(assemblyFullName);
					writer.Write(realAssemblyFullName);
					writer.Write(assemblyLocation != null ? assemblyLocation : String.Empty);
					writer.Write(assemblySize);

					// Write the referenced assemblies
					writer.Write(referencedAssemblies.Count);
					foreach (string referencedAssembly in referencedAssemblies) {
						#if DEBUG && DEBUG_CACHE_WRITING
						Trace.WriteLine("ReferencedAssembly: " + referencedAssembly);
						#endif

						writer.Write(referencedAssembly);
					}

					// Write out the type table
					writer.Write(typeTable.Length);
					foreach (AssemblyDomType type in typeTable) {
						#if DEBUG && DEBUG_CACHE_WRITING
						Trace.WriteLine("TypeTable: " + type.FullName);
						#endif

						writer.Write(type.FullName);
					}

					// Write the namespace info node tree
					this.WriteNamespaceInfoNode(writer, rootNamespaceInfo);

					#if DEBUG && DEBUG_CACHE_WRITING
					Trace.Unindent();
					#endif
				}
			}
		}
		
		/// <summary>
		/// Updates the namespace names.
		/// </summary>
		private void UpdateNamespaceNames() {
			namespaceNames.Clear();
			this.LoadNamespaceNames(rootNamespaceInfo, String.Empty);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the full name of the assembly that defined this project content, if any.
		/// </summary>
		/// <value>The full name of the assembly that defined this project content, if any.</value>
		public string AssemblyFullName {
			get {
				return assemblyFullName;
			}
		}
		
		/// <summary>
		/// Gets the location of the assembly that defined this project content, if any.
		/// </summary>
		/// <value>The location of the assembly that defined this project content, if any.</value>
		public string AssemblyLocation {
			get {
				return assemblyLocation;
			}
		}
		
		/// <summary>
		/// Releases the unmanaged resources used by the object and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">
		/// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources. 
		/// </param>
		/// <remarks>
		/// This method is called by the public <c>Dispose</c> method and the <c>Finalize</c> method. 
		/// <c>Dispose</c> invokes this method with the <paramref name="disposing"/> parameter set to <c>true</c>. 
		/// <c>Finalize</c> invokes this method with <paramref name="disposing"/> set to <c>false</c>.
		/// </remarks>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (documentation != null) {
					documentation.Dispose();
					documentation = null;
				}
				namespaceNames = null;
				rootNamespaceInfo = null;
			}
		}

		/// <summary>
		/// Gets the collection of child namespace names for the specified namespace name.
		/// </summary>
		/// <param name="namespaceName">The namespace name for which to search.</param>
		/// <returns>The collection of child namespace names for the specified namespace name.</returns>
		public ICollection GetChildNamespaceNames(string namespaceName) {
			StringCollection names = new StringCollection();
			NamespaceInfo namespaceInfo = this.GetNamespaceInfo(namespaceName);
			if (namespaceInfo != null) {
				foreach (NamespaceInfo childNamespaceInfo in namespaceInfo.ChildNamespaces.Values)
					names.Add(childNamespaceInfo.NamespaceName);
			}
			return names;
		}
		
		/// <summary>
		/// Gets the collection of available extension methods that target the specified type.
		/// </summary>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> used to resolve type references.</param>
		/// <param name="contextInheritanceHierarchy">
		/// An optional array of the inheritance hierarchy of the context <see cref="IDomType"/>.
		/// The first array item contains the context <see cref="IDomType"/>.
		/// Each following item indicates a base <see cref="IDomType"/> of the context <see cref="IDomType"/>.
		/// </param>
		/// <param name="importedNamespaces">The imported namespaces.</param>
		/// <param name="targetTypes">
		/// The array of the inheritance hierarchy of the target <see cref="IDomType"/>.
		/// The first array item contains the target <see cref="IDomType"/>.
		/// Each following item indicates a base <see cref="IDomType"/> of the target <see cref="IDomType"/>.
		/// </param>
		/// <param name="name">The name of the desired members.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>The collection of available extension methods that target the specified type.</returns>
		public ICollection GetExtensionMethods(DotNetProjectResolver projectResolver, IDomType[] contextInheritanceHierarchy, string[] importedNamespaces, IDomType[] targetTypes, string name, DomBindingFlags flags) {
			// NOTE: Don't really need contextInheritanceHierarchy here since extension methods are only on static classes

			// Create a type comparer
			DomTypeComparer comparer = new DomTypeComparer();

			// Tweak flags
			DomBindingFlags originalFlags = flags;
			flags &= ~DomBindingFlags.Instance;
			flags |= DomBindingFlags.Static;
			ArrayList members = new ArrayList();

			// Add in the global namespace (null value)
			string[] modifiedImportedNamespaces = new string[(importedNamespaces != null ? importedNamespaces.Length : 0) + 1];
			if (importedNamespaces != null)
				importedNamespaces.CopyTo(modifiedImportedNamespaces, 0);

			#if DEBUG && DEBUG_EXTENSIONMETHODS
			Trace.WriteLine("AssemblyProjectContent.GetExtensionMethods");
			Trace.Indent();
			#endif

			if (targetTypes != null) {
				// Loop through the imported namespaces
				foreach (string importedNamespace in modifiedImportedNamespaces) {
					NamespaceInfo namespaceInfo = this.GetNamespaceInfo(importedNamespace);
					if (namespaceInfo != null) {
						// Loop through the types
						foreach (TypeInfo typeInfo in namespaceInfo.Types.Values) {
							// If the type contains extension methods...
							if (typeInfo.Type.IsExtension) {
								IDomMember[] typeMembers = typeInfo.Type.GetMembers(contextInheritanceHierarchy, name, flags);
								if (typeMembers != null) {
									// Loop through members
									foreach (IDomMember member in typeMembers) {
										// If the member is an extension method...
										if (member.IsExtension) {
											IDomParameter[] parameters = member.Parameters;
											if ((parameters != null) && (parameters.Length > 0)) {
												foreach (IDomType targetType in targetTypes) {
													// See if the parameter type matches the target type...
													if (projectResolver != null) {
														bool isMatch = (comparer.IsTypeInstanceOf(projectResolver, member, parameters[0].ParameterType, targetType));

														#if DEBUG && DEBUG_EXTENSIONMETHODS
														if ((isMatch) && (member.DeclaringType.Name == "Enumerable") && (member.Name == "Average")) {
														Trace.WriteLine(String.Format("{0}.{1}: ParamType={2}, ConParamType={3}, TargetType={4}, Result={5}",
															member.DeclaringType.Name, member.Name, 
															DotNetProjectResolver.GetTypeNameForDebugging(parameters[0].ParameterType),
															(parameterType != null ? DotNetProjectResolver.GetTypeNameForDebugging(parameterType) : "null"),
															DotNetProjectResolver.GetTypeNameForDebugging(targetType),
															(isMatch ? "Match" : "No match")
															));
														}
														#endif

														if (isMatch) {
															// The first parameter is a match 
															members.Add(member);
															break;
														}
													}
												}
											}
										}
									}
								}
							}
						}						
					}
				}
			}
			
			#if DEBUG && DEBUG_EXTENSIONMETHODS
			Trace.Unindent();
			#endif

			return members;
		}
		
		/// <summary>
		/// Gets the collection of nested types within the specified type.
		/// </summary>
		/// <param name="contextInheritanceHierarchy">
		/// An optional array of the inheritance hierarchy of the context <see cref="IDomType"/>.
		/// The first array item contains the context <see cref="IDomType"/>.
		/// Each following item indicates a base <see cref="IDomType"/> of the context <see cref="IDomType"/>.
		/// </param>
		/// <param name="typeFullName">The full name of the type for which to search.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>The collection of nested types within the specified type.</returns>
		public ICollection GetNestedTypes(IDomType[] contextInheritanceHierarchy, string typeFullName, DomBindingFlags flags) {
			TypeInfo typeInfo = this.GetTypeInfo(typeFullName);
			if (typeInfo != null) {
				if (typeInfo.NestedTypes.Count > 0) {
					ArrayList types = new ArrayList();
					if ((flags & DomBindingFlags.HasConstructor) == DomBindingFlags.HasConstructor) {
						// Perform complex processing
						foreach (TypeInfo nestedTypeInfo in typeInfo.NestedTypes.Values) {
							if (AssemblyProjectContent.IsMatch(contextInheritanceHierarchy, nestedTypeInfo.Type, flags))
								types.Add(nestedTypeInfo.Type);
						}
					}
					else {
						// Perform a straight copy
						foreach (TypeInfo nestedTypeInfo in typeInfo.NestedTypes.Values)
							types.Add(nestedTypeInfo.Type);
					}
					return types;
				}
			}
			return new IDomType[0];
		}
		
		/// <summary>
		/// Gets the collection of standard modules within the specified namespace name.
		/// </summary>
		/// <param name="contextInheritanceHierarchy">
		/// An optional array of the inheritance hierarchy of the context <see cref="IDomType"/>.
		/// The first array item contains the context <see cref="IDomType"/>.
		/// Each following item indicates a base <see cref="IDomType"/> of the context <see cref="IDomType"/>.
		/// </param>
		/// <param name="namespaceName">The namespace name for which to search.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>The collection of standard modules within the specified namespace name.</returns>
		public ICollection GetStandardModules(IDomType[] contextInheritanceHierarchy, string namespaceName, DomBindingFlags flags) {
			NamespaceInfo namespaceInfo = this.GetNamespaceInfo(namespaceName);
			if (namespaceInfo != null) {
				if (namespaceInfo.StandardModules.Count > 0) {
					ArrayList types = new ArrayList();
					// Perform complex processing
					foreach (IDomType type in namespaceInfo.StandardModules) {
						if (AssemblyProjectContent.IsMatch(contextInheritanceHierarchy, type, flags))
							types.Add(type);
					}
					return types;
				}
			}
			return new IDomType[0];
		}

		/// <summary>
		/// Gets the <see cref="IDomType"/> that is defined in the project content with the specified type full name.
		/// </summary>
		/// <param name="contextInheritanceHierarchy">
		/// An optional array of the inheritance hierarchy of the context <see cref="IDomType"/>.
		/// The first array item contains the context <see cref="IDomType"/>.
		/// Each following item indicates a base <see cref="IDomType"/> of the context <see cref="IDomType"/>.
		/// </param>
		/// <param name="typeFullName">The full name of the type for which to search.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>The <see cref="IDomType"/> that is defined in the project content with the specified type full name.</returns>
		public IDomType GetType(IDomType[] contextInheritanceHierarchy, string typeFullName, DomBindingFlags flags) {
			TypeInfo typeInfo = this.GetTypeInfo(typeFullName);
			if (typeInfo != null) {
				if ((flags & DomBindingFlags.HasConstructor) == DomBindingFlags.HasConstructor) {
					// Perform complex processing
					if (AssemblyProjectContent.IsMatch(contextInheritanceHierarchy, typeInfo.Type, flags))
						return typeInfo.Type;
				}
				else
					return typeInfo.Type;
			}
			return null;
		}

		/// <summary>
		/// Gets the collection of types within the specified namespace name.
		/// </summary>
		/// <param name="contextInheritanceHierarchy">
		/// An optional array of the inheritance hierarchy of the context <see cref="IDomType"/>.
		/// The first array item contains the context <see cref="IDomType"/>.
		/// Each following item indicates a base <see cref="IDomType"/> of the context <see cref="IDomType"/>.
		/// </param>
		/// <param name="namespaceName">The namespace name for which to search.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>The collection of types within the specified namespace name.</returns>
		public ICollection GetTypes(IDomType[] contextInheritanceHierarchy, string namespaceName, DomBindingFlags flags) {
			NamespaceInfo namespaceInfo = this.GetNamespaceInfo(namespaceName);
			if (namespaceInfo != null) {
				if (namespaceInfo.Types.Count > 0) {
					ArrayList types = new ArrayList();
					if ((flags & DomBindingFlags.HasConstructor) == DomBindingFlags.HasConstructor) {
						// Perform complex processing
						foreach (TypeInfo typeInfo in namespaceInfo.Types.Values) {
							if (AssemblyProjectContent.IsMatch(contextInheritanceHierarchy, typeInfo.Type, flags))
								types.Add(typeInfo.Type);
						}
					}
					else {
						// Perform a straight copy
						foreach (TypeInfo typeInfo in namespaceInfo.Types.Values)
							types.Add(typeInfo.Type);
					}
					return types;
				}
			}
			return new IDomType[0];
		}

		/// <summary>
		/// Returns whether the project content defines any types with the specified namespace name.
		/// </summary>
		/// <param name="namespaceName">The namespace name for which to search.</param>
		/// <returns>
		/// <c>true</c> if the project content defines any types with the specified namespace name; otherwise, <c>false</c>.
		/// </returns>
		public bool HasNamespace(string namespaceName) {
			return (this.GetNamespaceInfo(namespaceName) != null);
		}

		/// <summary>
		/// Gets the collection of namespace names in the project content.
		/// </summary>
		/// <value>The collection of namespace names in the project content.</value>
		public IList NamespaceNames { 
			get {
				return namespaceNames;
			}
		}
		
	}
}
