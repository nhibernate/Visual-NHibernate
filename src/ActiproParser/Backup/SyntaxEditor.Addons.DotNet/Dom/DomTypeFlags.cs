using System;
using System.Collections;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents .NET type flags.
	/// </summary>
	[Flags()]
	internal enum DomTypeFlags {

		/// <summary>
		/// No flags are set.
		/// </summary>
		None = 0x0,

		/// <summary>
		/// The type is a structure type.
		/// </summary>
		Structure = 0x1,

		/// <summary>
		/// The type is an interface type.
		/// </summary>
		Interface = 0x2,

		/// <summary>
		/// The type is an enumeration type.
		/// </summary>
		Enumeration = 0x4,

		/// <summary>
		/// The type is a delegate type.
		/// </summary>
		Delegate = 0x8,

		/// <summary>
		/// The type is a standard module.
		/// </summary>
		StandardModule = 0x10,

		/// <summary>
		/// The type has a public contructor that can be called.
		/// </summary>
		HasPublicConstructor = 0x20,

		/// <summary>
		/// The type has a family (protected) contructor that can be called.
		/// </summary>
		HasFamilyConstructor = 0x40,

		/// <summary>
		/// The type has an assembly (internal) contructor that can be called.
		/// </summary>
		HasAssemblyConstructor = 0x80,

		/// <summary>
		/// The type has a private contructor that can be called.
		/// </summary>
		HasPrivateConstructor = 0x100,

		/// <summary>
		/// The type is a nested type.
		/// </summary>
		Nested = 0x200,
		
		/// <summary>
		/// The type is a generic type.
		/// </summary>
		GenericType = 0x400,

		/// <summary>
		/// The type is a generic type definition, from which other generic types can be constructed. 
		/// </summary>
		GenericTypeDefinition = 0x800,

		/// <summary>
		/// The type is a generic parameter.
		/// </summary>
		GenericParameter = 0x1000,

		/// <summary>
		/// A type can be substituted for the generic type parameter only if it has a parameterless constructor. 
		/// </summary>
		GenericParameterDefaultConstructorConstraint = 0x2000,

		/// <summary>
		/// A type can be substituted for the generic type parameter only if it is a value type and is not nullable. 
		/// </summary>
		GenericParameterNotNullableValueTypeConstraint = 0x4000,

		/// <summary>
		/// A type can be substituted for the generic type parameter only if it is a reference type. 
		/// </summary>
		GenericParameterReferenceTypeConstraint = 0x8000,

		/// <summary>
		/// The type is marked with an <c>ExtensionAttribute</c>.
		/// </summary>
		IsExtension = 0x10000,

		/// <summary>
		/// The type has an <c>EditorBrowsableAttribute</c> on it with a value of <c>Never</c>.
		/// </summary>
		IsEditorBrowsableNever = 0x20000,

	}
}
