// #define DEBUG_COMPARISONS

using System;
using System.Collections;
using System.Diagnostics;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Provides a class that compares <see cref="IDomType"/> instances and uses caching when there are a lot of repeated comparisons.
	/// </summary>
	internal class DomTypeComparer {

		private Hashtable	cachedTargetTypes;
		private Hashtable	cachedTypeHierarchies;
		private Hashtable	cachedTypeResolutions;

		private object Match = new object();
		private object NoMatch = new object();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the <see cref="IDomTypeReference"/> generic type parameter for the <see cref="IDomMember"/> that has the same
		/// name as the supplied <see cref="IDomTypeReference"/>.
		/// </summary>
		/// <param name="member">The <see cref="IDomMember"/> to examine.</param>
		/// <param name="typeReference">The <see cref="IDomTypeReference"/> for which to search.</param>
		/// <returns>The <see cref="IDomTypeReference"/> generic type parameter that was found, if any.</returns>
		private static IDomTypeReference GetGenericParameter(IDomMember member, IDomTypeReference typeReference) {
			foreach (IDomTypeReference genericTypeArgument in member.GenericTypeArguments) {
				// TODO: This needs improvement and right now really only works for 99% of extension methods
				// if (genericTypeArgument.Name == typeReference.Name)
					return genericTypeArgument;
			}
			return null;
		}

		/// <summary>
		/// Returns an array containing the inheritance hierarchy of the <see cref="IDomType"/> and its implemented interfaces and caches the result.
		/// </summary>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use for type resolution.</param>
		/// <param name="type">The <see cref="IDomType"/> to examine.</param>
		/// <returns>An array containing the inheritance hierarchy of the <see cref="IDomType"/> and its implemented interfaces.</returns>
		private IDomType[] GetTypeInheritanceHierarchyAndImplementedInterfaces(DotNetProjectResolver projectResolver, IDomType type) {
			// Create the cache if necessary
			if (cachedTypeHierarchies == null)
				cachedTypeHierarchies = new Hashtable();

			// See if the result is already cached
			if (cachedTypeHierarchies.ContainsKey(type)) {
				// Return the cached result
				return cachedTypeHierarchies[type] as IDomType[];
			}
			else {
				// Get the result
				IDomType[] contextInheritanceHierarchy = projectResolver.GetTypeInheritanceHierarchyAndImplementedInterfaces(type);

				// Cache the result
				cachedTypeHierarchies[type] = contextInheritanceHierarchy;

				return contextInheritanceHierarchy;
			}
		}

		/// <summary>
		/// Returns whether a source type is an instance of a target type.
		/// </summary>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use for type resolution.</param>
		/// <param name="contextMember">The <see cref="IDomMember"/> to examine.</param>
		/// 
		/// <param name="sourceTypeReference">The source <see cref="IDomTypeReference"/>.</param>
		/// <param name="targetType">The target <see cref="IDomType"/>.</param>
		/// <returns>
		/// <c>true</c> if the source type is an instance of the target type.
		/// </returns>
		internal bool IsTypeInstanceOf(DotNetProjectResolver projectResolver, IDomMember contextMember, IDomTypeReference sourceTypeReference, IDomType targetType) {
			if ((sourceTypeReference != null) && (targetType != null)) {
				// Create the cached comparisons as needed
				if (cachedTargetTypes == null)
					cachedTargetTypes = new Hashtable();

				// See if a cached result can be loaded
				Hashtable cachedSourceTypes = cachedTargetTypes[targetType] as Hashtable;
				if (cachedSourceTypes == null) {
					cachedSourceTypes = new Hashtable();
					cachedTargetTypes[targetType] = cachedSourceTypes;
				}
				object cachedResult = cachedSourceTypes[sourceTypeReference];
				if (cachedResult != null) {
					#if DEBUG && DEBUG_COMPARISONS
					Trace.WriteLine(String.Format("DomTypeComparer.IsTypeInstanceOf({0}, {1}) = {2} (cached)", 
						DotNetProjectResolver.GetTypeNameForDebugging(sourceTypeReference), DotNetProjectResolver.GetTypeNameForDebugging(targetType),
						(cachedResult == Match)));
					#endif

					return (cachedResult == Match);
				}

				if (sourceTypeReference.IsGenericParameter) {
					// Check the type parameter
					if (this.IsTypeParameterInstanceOf(projectResolver, contextMember, sourceTypeReference, targetType)) {
						#if DEBUG && DEBUG_COMPARISONS
						Trace.WriteLine(String.Format("DomTypeComparer.IsTypeInstanceOf({0}, {1}, {2}) = True", 
							(contextMember != null ? contextMember.DeclaringType.Name + "." + contextMember.Name : "(no member)"),
							DotNetProjectResolver.GetTypeNameForDebugging(sourceTypeReference), DotNetProjectResolver.GetTypeNameForDebugging(targetType)));
						#endif

						cachedSourceTypes[sourceTypeReference] = Match;
						return true;
					}
				}
				else {
					// Resolve the source type
					IDomType sourceType = sourceTypeReference.Resolve(projectResolver);
					if (sourceType != null) {
						// If the full type names match...
						if (sourceType.FullName == targetType.FullName) {
							// NOTE: More extensive code is needed to check into generics
							// Return true immediately if we are not dealing with generic types
							if (!targetType.IsGenericType)
								return true;

							// If the types are generic...
							if ((sourceType.IsGenericType) && (targetType.IsGenericType) && 
								(sourceType.GenericTypeArguments != null) && (targetType.GenericTypeArguments != null)) {
								
								// Create arrays of arguments
								IDomTypeReference[] sourceTypeParameters = new IDomTypeReference[sourceType.GenericTypeArguments.Count];
								IDomTypeReference[] targetTypeParameters = new IDomTypeReference[targetType.GenericTypeArguments.Count];
								if (sourceTypeParameters.Length == targetTypeParameters.Length) {
									// Build arrays
									sourceType.GenericTypeArguments.CopyTo(sourceTypeParameters, 0);
									targetType.GenericTypeArguments.CopyTo(targetTypeParameters, 0);

									for (int index = 0; index < sourceTypeParameters.Length; index++) {
										bool allowMatch = true;

										// If the source parameter is a generic parameter...
										if (sourceTypeParameters[index].IsGenericParameter) {
											// Check the type parameter
											allowMatch = this.IsTypeParameterInstanceOf(projectResolver, contextMember, sourceTypeParameters[index], targetTypeParameters[index]);
										}
										else if (sourceTypeParameters[index].FullName != targetTypeParameters[index].FullName) {
											// Block match
											allowMatch = false;
										}

										if (!allowMatch) {
											#if DEBUG && DEBUG_COMPARISONS
											Trace.WriteLine(String.Format("DomTypeComparer.IsTypeInstanceOf({0}, {1}, {2}) = False", 
												(contextMember != null ? contextMember.DeclaringType.Name + "." + contextMember.Name : "(no member)"),
												DotNetProjectResolver.GetTypeNameForDebugging(sourceType), DotNetProjectResolver.GetTypeNameForDebugging(targetType)));
											#endif

											cachedSourceTypes[sourceTypeReference] = NoMatch;
											return false;
										}
									}

									#if DEBUG && DEBUG_COMPARISONS
									Trace.WriteLine(String.Format("DomTypeComparer.IsTypeInstanceOf({0}, {1}, {2}) = True", 
										(contextMember != null ? contextMember.DeclaringType.Name + "." + contextMember.Name : "(no member)"),
										DotNetProjectResolver.GetTypeNameForDebugging(sourceType), DotNetProjectResolver.GetTypeNameForDebugging(targetType)));
									#endif

									cachedSourceTypes[sourceTypeReference] = Match;
									return true;
								}
							}
						}
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Returns whether a source type parameter is an instance of a target type.
		/// </summary>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use for type resolution.</param>
		/// <param name="contextMember">The <see cref="IDomMember"/> to examine.</param>
		/// <param name="sourceParameterReference">The source <see cref="IDomTypeReference"/> type parameter.</param>
		/// <param name="targetParameterReference">The target <see cref="IDomTypeReference"/> type parameter.</param>
		/// <returns>
		/// <c>true</c> if the source type parameter is an instance of the target type.
		/// </returns>
		private bool IsTypeParameterInstanceOf(DotNetProjectResolver projectResolver, IDomMember contextMember, IDomTypeReference sourceParameterReference, IDomTypeReference targetParameterReference) {
			bool allowMatch = true;

			// If the target parameter is a generic parameter...
			if (targetParameterReference.IsGenericParameter)
				// Block match
				allowMatch = false;
			else if (contextMember == null) {
				// Block match
				allowMatch = false;
			}
			else {
				// Get the defined parameter
				IDomTypeReference genericParameter = DomTypeComparer.GetGenericParameter(contextMember, sourceParameterReference);
				if (genericParameter == null) {
					// Block match
					allowMatch = false;
				}
				else {
					// Get the target parameter
					IDomType targetParameter = this.ResolveType(projectResolver, targetParameterReference);
					if (targetParameter == null) {
						// Block match
						allowMatch = false;
					}
					else {
						// Check reference type constraint
						if ((allowMatch) && (genericParameter.HasGenericParameterReferenceTypeConstraint) && (targetParameter.Type != DomTypeType.Class)) {
							// Block match
							allowMatch = false;
						}

						// Check not nullable value type constraint
						if ((allowMatch) && (genericParameter.HasGenericParameterNotNullableValueTypeConstraint) && (targetParameter.Type != DomTypeType.Structure)) {
							// Block match
							allowMatch = false;
						}

						// Check parameterless constructor constraint
						if ((allowMatch) && (genericParameter.HasGenericParameterDefaultConstructorConstraint)) {
							IDomMember[] constructors = targetParameter.GetMembers(null, null, DomBindingFlags.Public | DomBindingFlags.Instance | DomBindingFlags.OnlyConstructors);
							if ((constructors != null) && (constructors.Length > 0)) {
								// Look for a parameterless constructor
								allowMatch = false;
								foreach (IDomMember constructor in constructors) {
									if ((constructor.Parameters == null) || (constructor.Parameters.Length == 0)) {
										allowMatch = true;
										break;
									}
								}
							}
						}

						// Check base type and interface constraints
						ICollection constraintTypeReferences = genericParameter.GenericTypeParameterConstraints;
						if ((allowMatch) && (constraintTypeReferences != null) && (constraintTypeReferences.Count > 0)) {
							foreach (IDomTypeReference constraintTypeReference in constraintTypeReferences) {
								// Resolve the constraint to a type
								IDomType constraintType = this.ResolveType(projectResolver, constraintTypeReference);
								if (constraintType == null) {
									// Block match since we couldn't resolve the constraint
									allowMatch = false;
								}
								else {
									// Get the type/interface hierarchy and look for a match
									allowMatch = false;
									IDomType[] contextInheritanceHierarchy = this.GetTypeInheritanceHierarchyAndImplementedInterfaces(projectResolver, targetParameter);
									foreach (IDomType hierarchyType in contextInheritanceHierarchy) {
										if (this.IsTypeInstanceOf(projectResolver, null, hierarchyType, constraintType)) {
											allowMatch = true;
											break;
										}
									}
								}
							}
						}
					}
				}
			}

			return allowMatch;
		}
		
		/// <summary>
		/// Resolves the specified <see cref="IDomTypeReference"/> into an <see cref="IDomType"/> and caches the result.
		/// </summary>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use for type resolution.</param>
		/// <param name="typeReference">The <see cref="IDomTypeReference"/> to resolve.</param>
		/// <returns>The resolved <see cref="IDomType"/>.</returns>
		private IDomType ResolveType(DotNetProjectResolver projectResolver, IDomTypeReference typeReference) {
			// Create the cache if necessary
			if (cachedTypeResolutions == null)
				cachedTypeResolutions = new Hashtable();

			// See if the result is already cached
			if (cachedTypeResolutions.ContainsKey(typeReference)) {
				// Return the cached result
				return cachedTypeResolutions[typeReference] as IDomType;
			}
			else {
				// Do a type resolution
				IDomType type = typeReference.Resolve(projectResolver);

				// Cache the result
				cachedTypeResolutions[typeReference] = type;

				return type;
			}
		}

	}
}
