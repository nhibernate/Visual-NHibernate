using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Specifies the modifiers for a type or member.
	/// </summary>
	[Flags()]
	public enum Modifiers {

		/// <summary>
		/// No modifier.
		/// </summary>
		None = 0x0,

		/// <summary>
		/// Private access.
		/// Code within the type that declares a private element, including code within contained types, can access the element.
		/// </summary>
		Private = 0x1,

		/// <summary>
		/// Internal (also known as Internal or Friend) access.
		/// Code within the assembly that declares an assembly element can access it.
		/// </summary>
		Assembly = 0x2,

		/// <summary>
		/// Protected access.  
		/// Code within the class that declares a family (also known as Protected) element, or a class derived from it, can access the element.
		/// </summary>
		Family = 0x4,

		/// <summary>
		/// Public access. 
		/// Any code that can see a public element can access it.
		/// </summary>
		Public = 0x8,

		/// <summary>
		/// Family or Assembly access.  This is a union of <c>Family</c> and <c>Assembly</c> access.
		/// Code within the same class or the same assembly as the element, 
		/// or within any class derived from the element's class, can access it.
		/// </summary>
		FamilyOrAssembly = 0x6,

		/// <summary>
		/// The bitmask for access modifiers.
		/// </summary>
		AccessMask = 0xF,

		/// <summary>
		/// Defines a class or member that must be inherited.
		/// Represents <c>MustOverride</c> and <c>MustInherit</c> in Visual Basic.
		/// </summary>
		Abstract = 0x100,

		/// <summary>
		/// Defines a constant member.
		/// </summary>
		Const = 0x200,
		
		/// <summary>
		/// Identifies a property as the default property of its class, structure, or interface.
		/// Used in Visual Basic only.
		/// </summary>
		Default = 0x400,

		/// <summary>
		/// Declares a variable.
		/// Used in Visual Basic only.
		/// </summary>
		Dim = 0x800,

		/// <summary>
		/// Defines a method that is implemented externally.
		/// Used in C# only.
		/// </summary>
		Extern = 0x1000,

		/// <summary>
		/// Defines a class or member that cannot be overridden.
		/// Represents <c>NotInheritable</c> and <c>NotOverridable</c> in Visual Basic.
		/// </summary>
		Final = 0x2000,
		
		/// <summary>
		/// Indicates that a conversion operator (CType) converts a class or structure to a type that might not be able to hold some of the possible values of the original class or structure.
		/// Used in Visual Basic only.
		/// </summary>
		Narrowing = 0x4000,

		/// <summary>
		/// Hides a defined member in a base class.
		/// Represents <c>Shadows</c> in Visual Basic.
		/// </summary>
		New = 0x8000,

		/// <summary>
		/// Defines the type as a partial type which is defined in multiple parts.
		/// </summary>
		Partial = 0x10000,
		
		/// <summary>
		/// Specifies that a property or procedure redeclares one or more existing properties or procedures with the same name.
		/// Used in Visual Basic only.
		/// </summary>
		Overloads = 0x20000,

		/// <summary>
		/// Defines a member that overrides a base abstract or virtual implementation.
		/// Represents <c>Overrides</c> in Visual Basic.
		/// </summary>
		Override = 0x40000,

		/// <summary>
		/// Defines a read-only member.
		/// </summary>
		ReadOnly = 0x80000,

		/// <summary>
		/// Defines a static member.
		/// Represents <c>Shared</c> and <c>Static</c> in Visual Basic.
		/// </summary>
		Static = 0x100000,

		/// <summary>
		/// Defines an unsafe context.
		/// Used in C# only.
		/// </summary>
		Unsafe = 0x200000,

		/// <summary>
		/// Defines a member that can be overridden.
		/// Represents <c>Overridable</c> in Visual Basic.
		/// </summary>
		Virtual = 0x400000,

		/// <summary>
		/// Defines a field that can be modified by multiple concurrently executing threads.
		/// Used in C# only.
		/// </summary>
		Volatile = 0x800000,
		
		/// <summary>
		/// Indicates that a conversion operator (CType) converts a class or structure to a type that can hold all possible values of the original class or structure.
		/// Used in Visual Basic only.
		/// </summary>
		Widening = 0x1000000,

		/// <summary>
		/// Specifies that one or more declared member variables refer to an instance of a class that can raise events.
		/// Used in Visual Basic only.
		/// </summary>
		WithEvents = 0x2000000,

		/// <summary>
		/// Specifies that a property can be written but not read.
		/// Used in Visual Basic only.
		/// </summary>
		WriteOnly = 0x4000000,

	}
}
