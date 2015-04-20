using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using ActiproSoftware.ComponentModel;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents an <see cref="IProjectContent"/> implementation for a source code project.
	/// </summary>
	public class SourceProjectContent : DisposableObject, IProjectContent, ISemanticParseDataTarget {

		private string				assemblyFullName;
		private Hashtable			compilationUnits	= new Hashtable();
		private Guid				guid				= Guid.NewGuid();
		private StringCollection	namespaceNames		= new StringCollection();
		private NamespaceInfo		rootNamespaceInfo	= new NamespaceInfo();
		private object				syncRoot			= new object();
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INNER TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Stores data about a namespace node.
		/// </summary>
		private class NamespaceInfo {

			internal HybridDictionary	ChildNamespaces	= new HybridDictionary(); 
			internal string				NamespaceName;
			internal HybridDictionary	StandardModules	= new HybridDictionary(); 
			internal HybridDictionary	Types			= new HybridDictionary(); 

		}
		
		/// <summary>
		/// Stores data about a type node.
		/// </summary>
		private class TypeInfo {

			internal HybridDictionary	NestedTypes		= new HybridDictionary(); 
			internal string				SourceKey;
			internal IDomType			Type;

		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// EVENTS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs after semantic parsing has completed for a source code parse request.
		/// </summary>
		/// <eventdata>
		/// The event handler receives an argument of type <c>SemanticParseEventArgs</c> containing data related to this event.
		/// </eventdata>
		public event SemanticParseEventHandler SemanticParseComplete;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>SourceProjectContent</c> class.
		/// </summary>
		public SourceProjectContent() {}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets a unique GUID that identifies the object.
		/// </summary>
		/// <value>A unique GUID that identifies the object.</value>
		string ISemanticParseDataTarget.Guid { 
			get {
				return guid.ToString();
			}
		}

		/// <summary>
		/// Occurs when a semantic parse request is completed.
		/// </summary>
		/// <param name="request">A <see cref="SemanticParserServiceRequest"/> that contains the semantic parse request information and the parse data result.</param>
		void ISemanticParseDataTarget.NotifySemanticParseComplete(SemanticParserServiceRequest request) {
			CompilationUnit compilationUnit = request.SemanticParseData as CompilationUnit;
			this.Clear(request.Filename);
			if (compilationUnit != null)
				this.AddRange(request.Filename, compilationUnit.Types);

			// Raise an event
			this.OnSemanticParseComplete(new SemanticParseEventArgs(request));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Adds a <see cref="IDomType"/> to the project content.
		/// </summary>
		/// <param name="sourceKey">The string-based key that identifies the source of the <see cref="IDomType"/>, typically a filename.</param>
		/// <param name="type">The <see cref="IDomType"/> to add.</param>
		/// <param name="forcePartial">Whether to force a parial keyword (for VB).</param>
		private void AddCore(string sourceKey, IDomType type, bool forcePartial) {
			// Ensure a type is passed
			if (type == null)
				throw new ArgumentNullException(String.Format("A non-null IDomType must be specified when adding to the SourceProjectContent for source key '{0}'.", sourceKey));

			// Build the type info
			TypeInfo typeInfo = new TypeInfo();
			typeInfo.SourceKey = sourceKey;
			typeInfo.Type = type;

			// Add to the reflection data
			HybridDictionary types;
			if (type.IsNested) {
				// Find the parent type
				TypeInfo parentTypeInfo = this.GetTypeInfo(type.DeclaringType.FullName);
				if (parentTypeInfo == null)
					return;
				
				// Add to the parent type
				types = parentTypeInfo.NestedTypes;
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

				// Add to the namespace info
				types = namespaceInfo.Types;

				// Append to the standard modules collection if it is a standard module
				if (type.Type == DomTypeType.StandardModule)
					namespaceInfo.StandardModules[type.Name] = type;
			}
		
			// If the type is a partial type, see if the target is a merged partial type
			if ((forcePartial) || ((typeInfo.Type.Modifiers & Modifiers.Partial) == Modifiers.Partial)) {
				TypeInfo existingTypeInfo = (TypeInfo)types[type.Name];
				if (existingTypeInfo != null) {
					if (existingTypeInfo.Type is SourceMergedPartialType) {
						// The target is a merged partial type, there are already at least two definitions in it... add/update this one
						((SourceMergedPartialType)existingTypeInfo.Type).Add(sourceKey, type);
						return;
					}
					else if ((existingTypeInfo.SourceKey != sourceKey) && ((forcePartial) || ((existingTypeInfo.Type.Modifiers & Modifiers.Partial) == Modifiers.Partial))) {
						// The target is from a different source key and the target is a partial type... make a merged partial type
						existingTypeInfo.Type = new SourceMergedPartialType(existingTypeInfo.SourceKey, existingTypeInfo.Type, sourceKey, type);
						existingTypeInfo.SourceKey = null;
						return;
					}
				}
			}
			
			// Do a simple replace of the definition since the type is not partial
			types[type.Name] = typeInfo;
		}
		
		/// <summary>
		/// Removes all data that is related to the specified source key.
		/// </summary>
		/// <param name="parentNamespaceInfo">The parent <see cref="NamespaceInfo"/>.</param>
		/// <param name="sourceKey">The source key of data to remove.</param>
		/// <remarks>
		/// <c>true</c> if the namespace info is empty after the method call; otherwise, <c>false</c>.
		/// </remarks>
		private bool ClearCore(NamespaceInfo parentNamespaceInfo, string sourceKey) {
			// Find the types to remove
			StringCollection nodesToRemove = null;
			foreach (TypeInfo typeInfo in parentNamespaceInfo.Types.Values) {
				if ((typeInfo.SourceKey == sourceKey) || ((typeInfo.Type is SourceMergedPartialType) && (((SourceMergedPartialType)typeInfo.Type).Contains(sourceKey)))) {
					if (nodesToRemove == null)
						nodesToRemove = new StringCollection();
					nodesToRemove.Add(typeInfo.Type.Name);
				}
			}

			// Remove the appropriate types
			if (nodesToRemove != null) {
				foreach (string typeName in nodesToRemove) {
					TypeInfo typeInfo = (TypeInfo)parentNamespaceInfo.Types[typeName];
					if (typeInfo.Type is SourceMergedPartialType) {
						// The code is a partial type... remove it from the partial type data
						((SourceMergedPartialType)typeInfo.Type).Remove(sourceKey);
						
						// If there is only one definition left, replace the merged partial type wrapper with it
						if (((SourceMergedPartialType)typeInfo.Type).Count == 1) {
							typeInfo.SourceKey = ((SourceMergedPartialType)typeInfo.Type).PrimarySourceKey;
							typeInfo.Type = ((SourceMergedPartialType)typeInfo.Type).PrimaryType;
						}
					}
					else {
						// The code is a non-partial type or a partial type with only one definition... remove it
						parentNamespaceInfo.Types.Remove(typeName);
					}

					// Remove from the standard modules collection if appropriate
					if ((typeInfo.Type.Type == DomTypeType.StandardModule) && (parentNamespaceInfo.StandardModules.Contains(typeInfo.Type.Name)))
						parentNamespaceInfo.StandardModules.Remove(typeInfo.Type.Name);
				}
			}

			// Recurse
			nodesToRemove = null;
			foreach (NamespaceInfo childNamespaceInfo in parentNamespaceInfo.ChildNamespaces.Values) {
				if (this.ClearCore(childNamespaceInfo, sourceKey)) {
					if (nodesToRemove == null)
						nodesToRemove = new StringCollection();
					nodesToRemove.Add(childNamespaceInfo.NamespaceName);
				}
			}
			
			// Remove the appropriate child namespaces
			if (nodesToRemove != null) {
				foreach (string namespaceName in nodesToRemove)
					parentNamespaceInfo.ChildNamespaces.Remove(namespaceName);
			}

			return (parentNamespaceInfo.ChildNamespaces.Count + parentNamespaceInfo.Types.Count == 0);
		}
		
		#if DEBUG
		/// <summary>
		/// Recursively prints debugging information about the source project content to the console.
		/// </summary>
		/// <param name="namespaceInfo">The parent namespace info node.</param>
		private void Debug(NamespaceInfo namespaceInfo) {
			Trace.WriteLine("N:" + (namespaceInfo.NamespaceName != null ? namespaceInfo.NamespaceName : "<Global>"));
			Trace.Indent();
			foreach (TypeInfo childTypeInfo in namespaceInfo.Types.Values)
				this.Debug(childTypeInfo);
			foreach (NamespaceInfo childNamespaceInfo in namespaceInfo.ChildNamespaces.Values)
				this.Debug(childNamespaceInfo);
			Trace.Unindent();
		}
		
		/// <summary>
		/// Recursively prints debugging information about the source project content to the console.
		/// </summary>
		/// <param name="typeInfo">The parent type info node.</param>
		private void Debug(TypeInfo typeInfo) {
			string sourceKey = typeInfo.SourceKey;
			if (typeInfo.Type is SourceMergedPartialType)
				sourceKey = ((SourceMergedPartialType)typeInfo.Type).Count + " partial definitions";

			Trace.WriteLine("T:" + typeInfo.Type.Name + " (Source: \"" + sourceKey + "\")");
			Trace.Indent();
			foreach (TypeInfo childTypeInfo in typeInfo.NestedTypes.Values)
				this.Debug(childTypeInfo);
			Trace.Unindent();
		}
		#endif

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
		/// Populates a list of types defined within the specified source.
		/// </summary>
		/// <param name="types">The <see cref="IList"/> to append the types to.</param>
		/// <param name="parentNamespaceInfo">The parent <see cref="NamespaceInfo"/>.</param>
		/// <param name="sourceKey">The string-based key that identifies the source of the code, typically a filename.</param>
		/// <param name="returnMergedPartialClasses">Indicates whether to return the merged version of any partial types or only that portion of partial types that are defined in the source.</param>
		private void GetTypesForSourceKeyCore(IList types, NamespaceInfo parentNamespaceInfo, string sourceKey, bool returnMergedPartialClasses) {
			// Look for matches in the namespace
			foreach (TypeInfo typeInfo in parentNamespaceInfo.Types.Values) {
				if ((typeInfo.SourceKey == sourceKey) || ((typeInfo.Type is SourceMergedPartialType) && (((SourceMergedPartialType)typeInfo.Type).Contains(sourceKey)))) {
					if ((typeInfo.Type is SourceMergedPartialType) && (!returnMergedPartialClasses)) {
						// Add the portion of the partial type
						types.Add(((SourceMergedPartialType)typeInfo.Type)[sourceKey]);
					}
					else {
						// Add the type or the merged partial type
						types.Add(typeInfo.Type);
					}
				}
			}

			// Recurse
			foreach (NamespaceInfo childNamespaceInfo in parentNamespaceInfo.ChildNamespaces.Values)
				this.GetTypesForSourceKeyCore(types, childNamespaceInfo, sourceKey, returnMergedPartialClasses);
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
		/// Adds a <see cref="IDomType"/> to the project content.
		/// </summary>
		/// <param name="sourceKey">The string-based key that identifies the source of the <see cref="IDomType"/>, typically a filename.</param>
		/// <param name="type">The <see cref="IDomType"/> to add.</param>
		public void Add(string sourceKey, IDomType type) {
			lock (syncRoot) {
				// Quit if no source key was passed
				if (sourceKey == null)
					return;
				
				// Ensure the compilation unit is updated
				compilationUnits[sourceKey] = ((IAstNode)type).FindAncestor(typeof(ICompilationUnit));

				// See if it comes from VB
				bool isVisualBasic = false;
				CompilationUnit compilationUnit = compilationUnits[sourceKey] as CompilationUnit;
				if (compilationUnit != null)
					isVisualBasic = (compilationUnit.SourceLanguage == DotNetLanguage.VB);

				// Add the type
				this.AddCore(sourceKey, type, isVisualBasic);

				// Update the namespace names
				this.UpdateNamespaceNames();
			}
		}

		/// <summary>
		/// Adds a collection of <see cref="IDomType"/> objects to the project content.
		/// </summary>
		/// <param name="sourceKey">The string-based key that identifies the source of the <see cref="IDomType"/> objects, typically a filename.</param>
		/// <param name="types">The <see cref="ICollection"/> of <see cref="IDomType"/> objects to add.</param>
		public void AddRange(string sourceKey, ICollection types) {
			lock (syncRoot) {
				// Quit if no source key was passed
				if (sourceKey == null)
					return;

				ICompilationUnit compilationUnit = null;
				bool isVisualBasic = false;
				foreach (IDomType type in types) {
					// Load the compilation unit if available
					if (compilationUnit == null)
						compilationUnit = ((IAstNode)type).FindAncestor(typeof(ICompilationUnit)) as ICompilationUnit;

					// See if it comes from VB
					CompilationUnit sourceCompilationUnit = compilationUnit as CompilationUnit;
					if (sourceCompilationUnit != null)
						isVisualBasic = (sourceCompilationUnit.SourceLanguage == DotNetLanguage.VB);

					// Add the type
					this.AddCore(sourceKey, type, isVisualBasic);
				}

				// Ensure the compilation unit is updated
				compilationUnits[sourceKey] = compilationUnit;

				// Update the namespace names
				this.UpdateNamespaceNames();
			}
		}

		/// <summary>
		/// Gets or sets the full name of the assembly that defined this project content, if any.
		/// </summary>
		/// <value>The full name of the assembly that defined this project content, if any.</value>
		public string AssemblyFullName {
			get {
				return assemblyFullName;
			}
			set {
				assemblyFullName = value;
			}
		}
		
		/// <summary>
		/// Gets the location of the assembly that defined this project content, if any.
		/// </summary>
		/// <value>The location of the assembly that defined this project content, if any.</value>
		public string AssemblyLocation { 
			get {
				return null;
			}
		}

		/// <summary>
		/// Clears all data in the project content.
		/// </summary>
		public void Clear() {
			lock (syncRoot) {
				if (rootNamespaceInfo != null) {
					rootNamespaceInfo.ChildNamespaces.Clear();
					rootNamespaceInfo.Types.Clear();
					compilationUnits.Clear();
					this.UpdateNamespaceNames();
				}
			}
		}
		
		/// <summary>
		/// Removes all data that is related to the specified source key.
		/// </summary>
		/// <param name="sourceKey">The source key of data to remove.</param>
		public void Clear(string sourceKey) {
			lock (syncRoot) {
				// Quit if no source key was passed
				if (sourceKey == null)
					return;

				this.ClearCore(rootNamespaceInfo, sourceKey);
				compilationUnits.Remove(sourceKey);

				// Update the namespace names
				this.UpdateNamespaceNames();
			}
		}

		#if DEBUG
		/// <summary>
		/// Prints debugging information about the source project content to the console.
		/// </summary>
		public void Debug() {
			lock (syncRoot) {
				this.Debug(rootNamespaceInfo);
			}
		}
		#endif

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
			lock (syncRoot) {
				StringCollection names = new StringCollection();
				NamespaceInfo namespaceInfo = this.GetNamespaceInfo(namespaceName);
				if (namespaceInfo != null) {
					foreach (NamespaceInfo childNamespaceInfo in namespaceInfo.ChildNamespaces.Values)
						names.Add(childNamespaceInfo.NamespaceName);
				}
				return names;
			}
		}

		/// <summary>
		/// Gets the collection of <see cref="ICompilationUnit"/> objects that are currently loaded in the source project content.
		/// </summary>
		/// <returns>The collection of <see cref="ICompilationUnit"/> objects that are currently loaded in the source project content.</returns>
		public ICollection GetCompilationUnits() {
			ICompilationUnit[] compilationUnitArray = new ICompilationUnit[compilationUnits.Count];
			compilationUnits.Values.CopyTo(compilationUnitArray, 0);
			return compilationUnitArray;
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
			
			lock (syncRoot) {
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
					importedNamespaces.CopyTo(modifiedImportedNamespaces, 1);

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
															if (comparer.IsTypeInstanceOf(projectResolver, member, parameters[0].ParameterType, targetType)) {
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

				return members;
			}
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
			lock (syncRoot) {
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
		}
		
		/// <summary>
		/// Gets the collection of source keys (generally filenames) that are currently loaded in the source project content.
		/// </summary>
		/// <returns>The collection of source keys (generally filenames) that are currently loaded in the source project content.</returns>
		public ICollection GetSourceKeys() {
			string[] sourceKeyArray = new string[compilationUnits.Count];
			compilationUnits.Keys.CopyTo(sourceKeyArray, 0);
			return sourceKeyArray;
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
			lock (syncRoot) {
				NamespaceInfo namespaceInfo = this.GetNamespaceInfo(namespaceName);
				if (namespaceInfo != null) {
					if (namespaceInfo.StandardModules.Count > 0) {
						ArrayList types = new ArrayList();
						// Perform complex processing
						foreach (IDomType type in namespaceInfo.StandardModules.Values) {
							if (AssemblyProjectContent.IsMatch(contextInheritanceHierarchy, type, flags))
								types.Add(type);
						}
						return types;
					}
				}
				return new IDomType[0];
			}
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
			lock (syncRoot) {
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
			lock (syncRoot) {
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
		}

		/// <summary>
		/// Gets the collection of types defined within the specified source.
		/// </summary>
		/// <param name="sourceKey">The string-based key that identifies the source of the code, typically a filename.</param>
		/// <param name="returnMergedPartialClasses">Indicates whether to return the merged version of any partial types or only that portion of partial types that are defined in the source.</param>
		/// <returns>The collection of types defined within within specified source.</returns>
		public ICollection GetTypesForSourceKey(string sourceKey, bool returnMergedPartialClasses) {
			lock (syncRoot) {
				ArrayList types = new ArrayList();
				this.GetTypesForSourceKeyCore(types, rootNamespaceInfo, sourceKey, returnMergedPartialClasses);
				return types;
			}
		}

		/// <summary>
		/// Returns whether the project content defines any types with the specified namespace name.
		/// </summary>
		/// <param name="namespaceName">The namespace name for which to search.</param>
		/// <returns>
		/// <c>true</c> if the project content defines any types with the specified namespace name; otherwise, <c>false</c>.
		/// </returns>
		public bool HasNamespace(string namespaceName) {
			lock (syncRoot) {
				return (this.GetNamespaceInfo(namespaceName) != null);
			}
		}
		
		/// <summary>
		/// Loads the type reflection data into the project content for the specified code.
		/// </summary>
		/// <param name="language">The .NET Languages Add-on language to use for parsing the code.</param>
		/// <param name="sourceKey">The string-based key that identifies the source of the code, typically a filename.</param>
		/// <param name="code">The code to load.</param>
		/// <returns>
		/// The parse hash key identifying the request that was sent to the <see cref="SemanticParserService"/>.
		/// </returns>
		/// <remarks>
		/// The ideal time to use this method is when loading a "project" into an IDE application.
		/// Call it once for each code file in the project whose reflection data should be placed in the project content.
		/// <para>
		/// The results of the parse will be merged into the source project content using the specified source key.
		/// Any existing project content data with a matching source key will be replaced.
		/// </para>
		/// </remarks>
		public string LoadForCode(ISemanticParserServiceProcessor language, string sourceKey, string code) {
			// 10/26/2007 - Ensure that code only has line feeds so that the offsets in the AST are correct when loaded into a SyntaxEditor later
			if (code.IndexOf('\r') != -1) {
				// The inserted text has \r characters, remove them
				code = code.Replace("\r\n", "\n").Replace('\r', '\n');
			}

			// Make a request to the parser service (it runs in a separate thread)
			SemanticParserServiceRequest request = new SemanticParserServiceRequest(
				SemanticParserServiceRequest.LowPriority,
				sourceKey,
				code,
				new TextRange(0, code.Length),
                SemanticParseFlags.None,
				language,
				this
				);
			SemanticParserService.Parse(request);

			return request.ParseHashKey;
		}
		
		/// <summary>
		/// Loads the type reflection data into the project content for the specified code file.
		/// </summary>
		/// <param name="language">The .NET Languages Add-on language to use for parsing the code.</param>
		/// <param name="filename">The path to the .NET code file to load.</param>
		/// <returns>
		/// The parse hash key identifying the request that was sent to the <see cref="SemanticParserService"/>.
		/// </returns>
		/// <remarks>
		/// The ideal time to use this method is when loading a "project" into an IDE application.
		/// Call it once for each code file in the project whose reflection data should be placed in the project content.
		/// <para>
		/// The results of the parse will be merged into the source project content using the filename as the source key.
		/// Any existing project content data with a source key that is the same as the filename will be replaced.
		/// </para>
		/// </remarks>
		public string LoadForFile(ISemanticParserServiceProcessor language, string filename) {
			// Get the text
			StreamReader reader = File.OpenText(filename);
			string code = reader.ReadToEnd();
			reader.Close();

			// Load the data for the code
			return this.LoadForCode(language, filename, code);
		}

		/// <summary>
		/// Gets the collection of namespace names in the project content.
		/// </summary>
		/// <value>The collection of namespace names in the project content.</value>
		public IList NamespaceNames { 
			get {
				lock (syncRoot) {
					return namespaceNames;
				}
			}
		}
		
		/// <summary>
		/// Raises the <c>SemanticParseComplete</c> event.
		/// </summary>
		/// <param name="e">A <c>SemanticParseEventArgs</c> that contains the event data.</param>
		/// <remarks>
		/// The <c>OnSemanticParseComplete</c> method also allows derived classes to handle the event without attaching a delegate. 
		/// This is the preferred technique for handling the event in a derived class.
		/// <para>
		/// When overriding <c>OnSemanticParseComplete</c> in a derived class, be sure to call the base class's 
		/// <c>OnSemanticParseComplete</c> method so that registered delegates receive the event.
		/// </para>
		/// </remarks>
		protected internal virtual void OnSemanticParseComplete(SemanticParseEventArgs e) {
			if (this.SemanticParseComplete != null)
				this.SemanticParseComplete(this, e);			
		}
		
	}
}
