using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents a .NET type reference that is defined in an assembly.
	/// </summary>
	internal class AssemblyDomTypeReference : AssemblyDomTypeBase, IDomTypeReference {
		
		//
		// NOTE: Any changes made to fields need to be persisted to the cache in AssemblyProjectContent and the cache version number must be incremented
		//

		private DomArrayPointerInfo		arrayPointerInfo;
		private string					declaringTypeFullName;
		private IDomTypeReference[]		genericTypeParameterConstraints;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDomTypeReference</c> class.
		/// </summary>
		/// <param name="projectContent">The <see cref="IProjectContent"/> that contains the type reference.</param>
		/// <remarks>This overload should only be used when reading a cache file.</remarks>
		internal AssemblyDomTypeReference(AssemblyProjectContent projectContent) : base(projectContent) {}

		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDomTypeReference</c> class.
		/// </summary>
		/// <param name="projectContent">The <see cref="AssemblyProjectContent"/> that contains the type reference.</param>
		/// <param name="callingGenericTypes">The calling generic type array, used to prevent infinite recursion with generic constraints on a generic.</param>
		/// <param name="type">The <see cref="Type"/> to wrap with this object.</param>
		internal AssemblyDomTypeReference(AssemblyProjectContent projectContent, IDomTypeReference[] callingGenericTypes, Type type) : base(projectContent, callingGenericTypes, type) {
			// Get the declaring type full name
			if (type.DeclaringType != null)
				declaringTypeFullName = type.DeclaringType.FullName;
			
			// Get array/pointer info
			if ((type.IsArray) || (type.IsPointer)) {
				arrayPointerInfo = new DomArrayPointerInfo();
				if (type.IsArray) {
					// Build the rank list
					ArrayList rankList = new ArrayList();
					rankList.Add(type.GetArrayRank());
					Type elementType = type.GetElementType();
					while ((elementType != null) && (elementType.IsArray)) {
						rankList.Add(elementType.GetArrayRank());
						elementType = elementType.GetElementType();
					}
					arrayPointerInfo.ArrayRanks = new int[rankList.Count];
					for (int index = 0; index < rankList.Count; index++)
						arrayPointerInfo.ArrayRanks[index] = (int)rankList[index];

					// 5/10/2007 - Have to do a special check because nested type arrays aren't reflected correct in Type (http://www.actiprosoftware.com/Support/Forums/ViewForumTopic.aspx?ForumTopicID=2337#8632)
					if (type.FullName != null) {
						int nestedClassDelimiterIndex = type.FullName.LastIndexOf('+');
						if ((nestedClassDelimiterIndex != -1) && (type.DeclaringType == null))
							declaringTypeFullName = type.FullName.Substring(0, nestedClassDelimiterIndex);
					}

					// 4/29/2008 - Work on the element type from this point on
					type = elementType;
				}
				if (type.IsPointer) {
					// Determine the pointer level
					arrayPointerInfo.PointerLevel = 1;
					Type elementType = type.GetElementType();
					while ((elementType != null) && (elementType.IsPointer)) {
						arrayPointerInfo.PointerLevel++;
						elementType = elementType.GetElementType();
					}

					// 4/29/2008 - Work on the element type from this point on
					type = elementType;
				}
			}

			// Get generic type arguments
			#if !NET11
			if (type.IsGenericParameter) {
				this.TypeFlags |= DomTypeFlags.GenericParameter;
				if ((type.GenericParameterAttributes & GenericParameterAttributes.DefaultConstructorConstraint) == GenericParameterAttributes.DefaultConstructorConstraint)
					this.TypeFlags |= DomTypeFlags.GenericParameterDefaultConstructorConstraint;
				if ((type.GenericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) == GenericParameterAttributes.NotNullableValueTypeConstraint)
					this.TypeFlags |= DomTypeFlags.GenericParameterNotNullableValueTypeConstraint;
				if ((type.GenericParameterAttributes & GenericParameterAttributes.ReferenceTypeConstraint) == GenericParameterAttributes.ReferenceTypeConstraint)
					this.TypeFlags |= DomTypeFlags.GenericParameterReferenceTypeConstraint;

				Type[] parameterConstraints = type.GetGenericParameterConstraints();
				if ((parameterConstraints != null) && (parameterConstraints.Length > 0)) {
					genericTypeParameterConstraints = new IDomTypeReference[parameterConstraints.Length];
					for (int index = 0; index < parameterConstraints.Length; index++) {
						if (callingGenericTypes != null) {
							// 3/25/2008 - There is a stack of calling generic types... check each one to make sure we are not going to infinitely recurse
							foreach (IDomTypeReference callingGenericType in callingGenericTypes) {
								if ((callingGenericType.Name == parameterConstraints[index].Name) && (callingGenericType.Namespace == parameterConstraints[index].Namespace)) {
									genericTypeParameterConstraints = null;
									break;
								}
							}
							if (genericTypeParameterConstraints == null)
								break;
						}

						// Build a new list of calling generic types
						IDomTypeReference[] newCallingGenericTypes = new IDomTypeReference[(callingGenericTypes != null ? callingGenericTypes.Length : 0) + 1];
						newCallingGenericTypes[0] = this;
						if (callingGenericTypes != null)
							callingGenericTypes.CopyTo(newCallingGenericTypes, 1);

						// Get the type reference (recurse)
						genericTypeParameterConstraints[index] = projectContent.GetTypeReference(newCallingGenericTypes, parameterConstraints[index]);
					}
				}
			}
			#endif
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
			
		/// <summary>
		/// Gets the type contraints if this is a generic type parameter.
		/// </summary>
		/// <value>The type contraints if this is a generic type parameter.</value>
		/// <remarks>This property is only used when the <see cref="IDomTypeReference.IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		ICollection IDomTypeReference.GenericTypeParameterConstraints { 
			get {
				return genericTypeParameterConstraints;
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the <see cref="DomArrayPointerInfo"/> that contains array and pointer information.
		/// </summary>
		/// <value>The <see cref="DomArrayPointerInfo"/> that contains array and pointer information.</value>
		internal DomArrayPointerInfo ArrayPointerInfo {
			get {
				return arrayPointerInfo;
			}
			set {
				arrayPointerInfo = value;
			}
		}

		/// <summary>
		/// Clones the object.
		/// </summary>
		/// <returns>The clone that was created.</returns>
		internal AssemblyDomTypeReference Clone() {
			AssemblyDomTypeReference typeReference = new AssemblyDomTypeReference(this.ProjectContentInternal);

			// Get the generic type arguments
			IDomTypeReference[] genericTypeArguments = null;
			if (this.GenericTypeArguments != null) {
				genericTypeArguments = new IDomTypeReference[this.GenericTypeArguments.Count];
				this.GenericTypeArguments.CopyTo(genericTypeArguments, 0);
			}

			// AssemblyDomTypeBase fields
			typeReference.AssemblyIndex = this.AssemblyIndex;
			typeReference.SetGenericTypeArguments(genericTypeArguments);
			typeReference.SetModifiers(this.Modifiers);
			typeReference.SetName(this.Name);
			typeReference.SetNamespace(this.Namespace);
			typeReference.TypeFlags = this.TypeFlags;

			// AssemblyDomTypeReference fields
			typeReference.arrayPointerInfo					= arrayPointerInfo;
			typeReference.declaringTypeFullName				= declaringTypeFullName;
			typeReference.genericTypeParameterConstraints	= genericTypeParameterConstraints;

			return typeReference;
		}

		/// <summary>
		/// Gets or sets the declaring type full name.
		/// </summary>
		/// <value>The declaring type full name.</value>
		internal string DeclaringTypeFullName {
			get {
				return declaringTypeFullName;
			}
			set {
				declaringTypeFullName = ((value != null) && (value.Length > 0) ? value : null);
			}
		}

		/// <summary>
		/// Sets the value of the <see cref="GenericTypeParameterConstraints"/> property.
		/// </summary>
		/// <param name="value">The value to set.</param>
		internal void SetGenericTypeParameterConstraints(IDomTypeReference[] value) {
			genericTypeParameterConstraints = value;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
			
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
		public override int[] ArrayRanks {
			get {
				return (arrayPointerInfo != null ? arrayPointerInfo.ArrayRanks : null);
			}
		}
			
		/// <summary>
		/// Gets the type contraints if this is a generic type parameter.
		/// </summary>
		/// <value>The type contraints if this is a generic type parameter.</value>
		/// <remarks>This property is only used when the <see cref="IDomTypeReference.IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		public ICollection GenericTypeParameterConstraints { 
			get {
				return genericTypeParameterConstraints;
			}
		}
		
		/// <summary>
		/// Returns the full name of the type reference.
		/// </summary>
		/// <param name="includeArrayPointerInfo">Whether to include array and pointer info.</param>
		/// <returns>The full name of the type reference.</returns>
		protected override string GetFullName(bool includeArrayPointerInfo) {
			if (declaringTypeFullName != null)
				return DotNetProjectResolver.GetTypeNameWithArrayPointerSpec(declaringTypeFullName + "+" + this.Name,
					0, (includeArrayPointerInfo ? this.ArrayRanks : null), (includeArrayPointerInfo ? (int)this.PointerLevel : 0));
			else
				return DotNetProjectResolver.GetTypeNameWithArrayPointerSpec(((this.Namespace != null) && (this.Namespace.Length > 0) ? this.Namespace + "." : String.Empty) + this.Name,
					0, (includeArrayPointerInfo ? this.ArrayRanks : null), (includeArrayPointerInfo ? (int)this.PointerLevel : 0));
		}
			
		/// <summary>
		/// Gets the unsafe pointer level of the type reference.
		/// </summary>
		/// <value>The unsafe pointer level of the type reference.</value>
		public override int PointerLevel {
			get {
				return (arrayPointerInfo != null ? arrayPointerInfo.PointerLevel : 0);
			}
		}

		/// <summary>
		/// Resolve all references to types in the same assembly to the actual type.
		/// </summary>
		/// <param name="projectContent">The <see cref="AssemblyProjectContent"/> to use for resolution.</param>
		protected internal override void ResolveTypePlaceHolders(AssemblyProjectContent projectContent) {
			// Call the base method
			base.ResolveTypePlaceHolders(projectContent);

			if (genericTypeParameterConstraints != null) {
				for (int index = genericTypeParameterConstraints.Length - 1; index >= 0; index--) {
					if (genericTypeParameterConstraints[index] is AssemblyDomTypePlaceHolder)
						genericTypeParameterConstraints[index] = projectContent.ResolveAssemblyDomTypePlaceHolder((AssemblyDomTypePlaceHolder)genericTypeParameterConstraints[index]);
					else if (genericTypeParameterConstraints[index] is AssemblyDomTypeReference)
						((AssemblyDomTypeReference)genericTypeParameterConstraints[index]).ResolveTypePlaceHolders(projectContent);
				}
			}
		}

	}
}
