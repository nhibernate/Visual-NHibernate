using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents a generic .NET member that has been constructed and wraps the <see cref="IDomMember"/> that is its definition.
	/// </summary>
	internal class ConstructedGenericMember : IDomMember {
		
		private IDomMember			genericDefinitionMember;
		private ICollection			genericTypeArguments;
		private IDomParameter[]		parameters;
		private IDomTypeReference	returnType;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>ConstructedGenericMember</c> class.
		/// </summary>
		/// <param name="genericDefinitionMember">The <see cref="IDomMember"/> specifying the generic member definition to wrap.</param>
		/// <param name="genericTypeArguments">The <see cref="ICollection"/> of generic type arguments.</param>
		internal ConstructedGenericMember(IDomMember genericDefinitionMember, ICollection genericTypeArguments) {
			// Initialize parameters
			this.genericDefinitionMember	= genericDefinitionMember;
			this.genericTypeArguments		= genericTypeArguments;

			// Get the generic type parameters
			IDomTypeReference[] unresolvedGenericTypeArguments = new IDomTypeReference[genericDefinitionMember.GenericTypeArguments.Count];
			IDomTypeReference[] resolvedGenericTypeArguments = new IDomTypeReference[genericDefinitionMember.GenericTypeArguments.Count];
			genericDefinitionMember.GenericTypeArguments.CopyTo(unresolvedGenericTypeArguments, 0);
			genericTypeArguments.CopyTo(resolvedGenericTypeArguments, 0);

			// Update return type and parameters
			if (genericDefinitionMember.ReturnType != null)
				returnType = this.ConstructTypeReference(unresolvedGenericTypeArguments, resolvedGenericTypeArguments, genericDefinitionMember.ReturnType);
			if (genericDefinitionMember.Parameters != null) {
				int index = 0;
				parameters = new IDomParameter[genericDefinitionMember.Parameters.Length];
				foreach (IDomParameter parameter in genericDefinitionMember.Parameters) {
					if (parameter.ParameterType != null) {
						IDomTypeReference parameterType = this.ConstructTypeReference(unresolvedGenericTypeArguments, resolvedGenericTypeArguments, parameter.ParameterType);
						if (parameterType != null) {
							parameters[index++] = parameter.CloneForType(parameterType);
							continue;
						}
					}
					parameters[index++] = parameter;
				}
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns a constructed type reference where the generic type parameters have been resolved to their matching counterparts.
		/// </summary>
		/// <param name="unresolvedGenericTypeArguments">The array of unresolved generic type arguments.</param>
		/// <param name="resolvedGenericTypeArguments">The array of resolved generic type arguments.</param>
		/// <param name="unresolvedTypeReference">The unresolved generic type reference.</param>
		private IDomTypeReference ConstructTypeReference(IDomTypeReference[] unresolvedGenericTypeArguments, IDomTypeReference[] resolvedGenericTypeArguments, IDomTypeReference unresolvedTypeReference) {
			// If the passed type reference is a generic parameter, try to resolve it
			if (unresolvedTypeReference.IsGenericParameter) {
				for (int index = 0; index < unresolvedGenericTypeArguments.Length; index++) {
					if (unresolvedGenericTypeArguments[index].Name == unresolvedTypeReference.Name)
						return resolvedGenericTypeArguments[index];
				}
			}

			// If the passed type reference is a generic type...
			if (unresolvedTypeReference.IsGenericType) {
				TypeReference typeReference = unresolvedTypeReference as TypeReference;
				if (typeReference != null) {
					// Clone the type reference
					typeReference = typeReference.Clone();
 
					// Build a new list of generic type arguments
					IDomTypeReference[] genericTypeArguments = new IDomTypeReference[typeReference.GenericTypeArguments.Count];
					typeReference.GenericTypeArguments.CopyTo(genericTypeArguments, 0);

					// Update the type reference's generic type arguments
					typeReference.GenericTypeArguments.Clear();
					foreach (IDomTypeReference genericTypeArgument in genericTypeArguments) {
						// Try to construct
						IDomTypeReference constructedGenericTypeArgument = this.ConstructTypeReference(unresolvedGenericTypeArguments, resolvedGenericTypeArguments, genericTypeArgument);

						if (constructedGenericTypeArgument is IAstNode)
							typeReference.GenericTypeArguments.Add((IAstNode)constructedGenericTypeArgument);
						else if (constructedGenericTypeArgument != null)
							typeReference.GenericTypeArguments.Add(new TypeReference(constructedGenericTypeArgument.FullName, false));
					}

					return typeReference;
				}
				else {
					AssemblyDomTypeReference assemblyTypeReference = unresolvedTypeReference as AssemblyDomTypeReference;
					if ((assemblyTypeReference != null) && (assemblyTypeReference.GenericTypeArguments != null)) {
						// Clone the type reference
						assemblyTypeReference = assemblyTypeReference.Clone();

						// Build a new list of generic type arguments
						IDomTypeReference[] genericTypeArguments = new IDomTypeReference[assemblyTypeReference.GenericTypeArguments.Count];
						assemblyTypeReference.GenericTypeArguments.CopyTo(genericTypeArguments, 0);

						// Update the type reference's generic type arguments
						ArrayList constructedGenericTypeArguments = new ArrayList();
						foreach (IDomTypeReference genericTypeArgument in genericTypeArguments) {
							// Try to construct
							IDomTypeReference constructedGenericTypeArgument = this.ConstructTypeReference(unresolvedGenericTypeArguments, resolvedGenericTypeArguments, genericTypeArgument);
							if (constructedGenericTypeArgument != null)
								constructedGenericTypeArguments.Add(constructedGenericTypeArgument);
						}

						// Build the updated array of generic type arguments
						genericTypeArguments = null;
						if (constructedGenericTypeArguments.Count > 0) {
							genericTypeArguments = new IDomTypeReference[constructedGenericTypeArguments.Count];
							constructedGenericTypeArguments.CopyTo(genericTypeArguments, 0);
						}

						// Update the type reference with the generic type arguments
						assemblyTypeReference.SetGenericTypeArguments(genericTypeArguments);

						return assemblyTypeReference;
						// TODO: Need to handle this by cloning the type for resolved generic type arguments
					}
				}
			}

			return unresolvedTypeReference;
		}

		/// <summary>
		/// Gets the <see cref="IDomMember"/> specifying the generic member definition that is wrapped.
		/// </summary>
		/// <value>The <see cref="IDomMember"/> specifying the generic member definition that is wrapped.</value>
		internal IDomMember GenericDefinitionMember {
			get {
				return genericDefinitionMember;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the access-related values of <see cref="Modifiers"/>.
		/// </summary>
		/// <value>The access-related values of <see cref="Modifiers"/>.</value>
		public Modifiers AccessModifiers { 
			get {
				return genericDefinitionMember.AccessModifiers;
			}
		}

		/// <summary>
		/// Gets a <see cref="IDomTypeReference"/> to the type that declares the member.
		/// </summary>
		/// <value>A <see cref="IDomTypeReference"/> to the type that declares the member.</value>
		public IDomTypeReference DeclaringType { 
			get {
				return genericDefinitionMember.DeclaringType;
			}
		}

		/// <summary>
		/// Gets the <see cref="DomDocumentationProvider"/> for the member.
		/// </summary>
		/// <value>The <see cref="DomDocumentationProvider"/> for the member.</value>
		public DomDocumentationProvider DocumentationProvider { 
			get {
				return genericDefinitionMember.DocumentationProvider;
			}
		}
		
		/// <summary>
		/// Gets the type arguments if this is a generic method definition.
		/// </summary>
		/// <value>The type arguments if this is a generic method definition.</value>
		public ICollection GenericTypeArguments { 
			get {
				return genericTypeArguments;
			}
		}
		
		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		public int ImageIndex { 
			get {
				return genericDefinitionMember.ImageIndex;
			}
		}
		
		/// <summary>
		/// Gets whether the member has an <c>EditorBrowsableAttribute</c> on it with a value of <c>Never</c>.
		/// </summary>
		/// <value>
		/// <c>true</c> if the member has an <c>EditorBrowsableAttribute</c> on it with a value of <c>Never</c>; otherwise, <c>false</c>.
		/// </value>
		public bool IsEditorBrowsableNever { 
			get {
				return genericDefinitionMember.IsEditorBrowsableNever;
			}
		}
		
		/// <summary>
		/// Gets whether the member is marked with an <c>ExtensionAttribute</c>.
		/// </summary>
		/// <value>
		/// <c>true</c> if the member is marked with an <c>ExtensionAttribute</c>; otherwise, <c>false</c>.
		/// </value>
		public bool IsExtension { 
			get {
				return genericDefinitionMember.IsExtension;
			}
		}
		
		/// <summary>
		/// Gets whether the member is a generic method.
		/// </summary>
		/// <value>
		/// <c>true</c> if the method is a generic method; otherwise, <c>false</c>.
		/// </value>
		public bool IsGenericMethod { 
			get {
				return genericDefinitionMember.IsGenericMethod;
			}
		}

		/// <summary>
		/// Gets whether the member is a generic method definition, from which other generic methods can be constructed.
		/// </summary>
		/// <value>
		/// <c>true</c> if the method is a generic method definition, from which other generic methods can be constructed; otherwise, <c>false</c>.
		/// </value>
		public bool IsGenericMethodDefinition { 
			get {
				return false;
			}
		}

		/// <summary>
		/// Gets whether the member is static.
		/// </summary>
		/// <value>
		/// <c>true</c> if the member is static; otherwise, <c>false</c>.
		/// </value>
		public bool IsStatic { 
			get {
				return genericDefinitionMember.IsStatic;
			}
		}
		
		/// <summary>
		/// Gets a <see cref="DomMemberType"/> that indicates the type of member.
		/// </summary>
		/// <value>A <see cref="DomMemberType"/> that indicates the type of member.</value>
		public DomMemberType MemberType { 
			get {
				return genericDefinitionMember.MemberType;
			}
		}
		
		/// <summary>
		/// Gets the modifiers for the member.
		/// </summary>
		/// <value>The modifiers for the member.</value>
		public Modifiers Modifiers { 
			get {
				return genericDefinitionMember.Modifiers;
			}
		}

		/// <summary>
		/// Gets the name of the member.
		/// </summary>
		/// <value>The name of the member.</value>
		public string Name { 
			get {
				return genericDefinitionMember.Name;
			}
		}
		
		/// <summary>
		/// Gets the array of <see cref="IDomParameter"/> parameters for the member.
		/// </summary>
		/// <value>The array of <see cref="IDomParameter"/> parameters for the member.</value>
		public IDomParameter[] Parameters { 
			get {
				return parameters;
			}
		}
		
		/// <summary>
		/// Gets a <see cref="IDomTypeReference"/> to the type that is returned by the member.
		/// </summary>
		/// <value>A <see cref="IDomTypeReference"/> to the type that is returned by the member.</value>
		public IDomTypeReference ReturnType { 
			get {
				return returnType;
			}
		}

	}
}
