using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Provides the base requirements for project content, which might represent an assembly or the contents of an open project.
	/// </summary>
	public interface IProjectContent : IDisposable {
		
		/// <summary>
		/// Gets the full name of the assembly that defined this project content, if any.
		/// </summary>
		/// <value>The full name of the assembly that defined this project content, if any.</value>
		string AssemblyFullName { get; }
		
		/// <summary>
		/// Gets the location of the assembly that defined this project content, if any.
		/// </summary>
		/// <value>The location of the assembly that defined this project content, if any.</value>
		string AssemblyLocation { get; }

		/// <summary>
		/// Gets the collection of child namespace names for the specified namespace name.
		/// </summary>
		/// <param name="namespaceName">The namespace name for which to search.</param>
		/// <returns>The collection of child namespace names for the specified namespace name.</returns>
		ICollection GetChildNamespaceNames(string namespaceName);
		
		/// <summary>
		/// Gets the collection of available extension methods that target the specified type.
		/// </summary>
		/// <param name="contextInheritanceHierarchy">
		/// An optional array of the inheritance hierarchy of the context <see cref="IDomType"/>.
		/// The first array item contains the context <see cref="IDomType"/>.
		/// Each following item indicates a base <see cref="IDomType"/> of the context <see cref="IDomType"/>.
		/// </param>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> used to resolve type references.</param>
		/// <param name="importedNamespaces">The imported namespaces.</param>
		/// <param name="targetTypes">
		/// The array of the inheritance hierarchy of the target <see cref="IDomType"/>.
		/// The first array item contains the target <see cref="IDomType"/>.
		/// Each following item indicates a base <see cref="IDomType"/> of the target <see cref="IDomType"/>.
		/// </param>
		/// <param name="name">The name of the desired members.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>The collection of available extension methods that target the specified type.</returns>
		ICollection GetExtensionMethods(DotNetProjectResolver projectResolver, IDomType[] contextInheritanceHierarchy, string[] importedNamespaces, IDomType[] targetTypes, string name, DomBindingFlags flags);
		
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
		ICollection GetNestedTypes(IDomType[] contextInheritanceHierarchy, string typeFullName, DomBindingFlags flags);
		
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
		ICollection GetStandardModules(IDomType[] contextInheritanceHierarchy, string namespaceName, DomBindingFlags flags);

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
		IDomType GetType(IDomType[] contextInheritanceHierarchy, string typeFullName, DomBindingFlags flags);

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
		ICollection GetTypes(IDomType[] contextInheritanceHierarchy, string namespaceName, DomBindingFlags flags);

		/// <summary>
		/// Returns whether the project content defines any types with the specified namespace name.
		/// </summary>
		/// <param name="namespaceName">The namespace name for which to search.</param>
		/// <returns>
		/// <c>true</c> if the project content defines any types with the specified namespace name; otherwise, <c>false</c>.
		/// </returns>
		bool HasNamespace(string namespaceName);

		/// <summary>
		/// Gets the collection of namespace names in the project content.
		/// </summary>
		/// <value>The collection of namespace names in the project content.</value>
		IList NamespaceNames { get; }

	}
}
