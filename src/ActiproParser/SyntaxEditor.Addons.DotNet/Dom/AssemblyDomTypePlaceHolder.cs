using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Used as an <see cref="IDomTypeReference"/> to reference an <see cref="AssemblyDomType"/> defined in the same assembly.
	/// </summary>
	internal class AssemblyDomTypePlaceHolder : IDomTypeReference {

		private string					rawFullName;
		private int						typeTableIndex;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDomTypePlaceHolder</c> class.
		/// </summary>
		/// <param name="type">The <see cref="Type"/> for which the placeholder is created, if loading from a live assembly.</param>
		/// <param name="typeTableIndex">The type table index of the <see cref="AssemblyDomType"/> that is wrapped by this class.</param>
		internal AssemblyDomTypePlaceHolder(Type type, int typeTableIndex) {
			// Initialize parameters
			if (type != null)
				this.rawFullName = type.FullName;
			this.typeTableIndex = typeTableIndex;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the type table index of the <see cref="AssemblyDomType"/> that is wrapped by this class.
		/// </summary>
		/// <value>The type table index of the <see cref="AssemblyDomType"/> that is wrapped by this class.</value>
		internal int TypeTableIndex {
			get {
				return typeTableIndex;
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
				return Modifiers.None;
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
		/// Gets the full name of the type.
		/// </summary>
		public string FullName {
			get {
				return rawFullName;
			}
		}
		
		/// <summary>
		/// Gets the type arguments if this is a generic type definition.
		/// </summary>
		/// <value>The type arguments if this is a generic type definition.</value>
		public ICollection GenericTypeArguments {
			get {
				return null;
			}
		}
		
		/// <summary>
		/// Gets the type contraints if this is a generic type parameter.
		/// </summary>
		/// <value>The type contraints if this is a generic type parameter.</value>
		/// <remarks>This property is only used when the <see cref="IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		public ICollection GenericTypeParameterConstraints {
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
		/// <remarks>This property is only used when the <see cref="IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		public bool HasGenericParameterDefaultConstructorConstraint {
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
		/// <remarks>This property is only used when the <see cref="IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		public bool HasGenericParameterNotNullableValueTypeConstraint { 
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
		/// <remarks>This property is only used when the <see cref="IsGenericParameter"/> property is set to <c>true</c>.</remarks>
		public bool HasGenericParameterReferenceTypeConstraint {
			get {
				return false;
			}
		}
		
		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		public int ImageIndex {
			get {
				return -1;
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
				return false;
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
				return false;
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
				return false;
			}
		}
		
		/// <summary>
		/// Gets the name of the type.
		/// </summary>
		/// <value>The name of the type.</value>
		public string Name {
			get {
				return null;
			}
		}

		/// <summary>
		/// Gets the name of the namespace that contains the type.
		/// </summary>
		/// <value>The name of the namespace that contains the type.</value>
		public string Namespace {
			get {
				return null;
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
		/// Gets the raw, unresolved full name of the type.
		/// </summary>
		/// <value>The raw, unresolved full name of the type.</value>
		public string RawFullName { 
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
		public IDomType Resolve(DotNetProjectResolver projectResolver) {
			return null;
		}

		/// <summary>
		/// Gets the <see cref="DomTypeType"/> that indicates the type of type that this object represents.
		/// </summary>
		/// <value>The <see cref="DomTypeType"/> that indicates the type of type that this object represents.</value>
		public DomTypeType Type { 
			get {
				return DomTypeType.Class;
			}
		}

	}
}
