using System;
using System.Collections;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents .NET member flags.
	/// </summary>
	[Flags()]
	internal enum DomMemberFlags {

		// NOTE: Any changes here need to be made in DomMemberType as well

		/// <summary>
		/// No flags are set.
		/// </summary>
		None = 0x0,

		/// <summary>
		/// The member is a custom member type.
		/// </summary>
		Custom = 0x1,

		/// <summary>
		/// The member is a constant member type.
		/// </summary>
		Constant = 0x2,

		/// <summary>
		/// The member is a constructor member type.
		/// </summary>
		Constructor = 0x4,

		/// <summary>
		/// The member is an event member type.
		/// </summary>
		Event = 0x8,

		/// <summary>
		/// The member is a field member type.
		/// </summary>
		Field = 0x10,

		/// <summary>
		/// The member is a method member type.
		/// </summary>
		Method = 0x20,
		
		/// <summary>
		/// The member is a property member type.
		/// </summary>
		Property = 0x40,

		/// <summary>
		/// The mask for the member type values.
		/// </summary>
		MemberTypesMask = Custom | Constant | Constructor | Event | Field | Method | Property,

		/// <summary>
		/// The member is a generic method, constructed from a generic method definition.
		/// </summary>
		GenericMethod = 0x400,

		/// <summary>
		/// The member is a generic method definition, from which other generic method can be constructed. 
		/// </summary>
		GenericMethodDefinition = 0x800,
		
		/// <summary>
		/// The member is marked with an <c>ExtensionAttribute</c>.
		/// </summary>
		IsExtension = 0x1000,

		/// <summary>
		/// The member has an <c>EditorBrowsableAttribute</c> on it with a value of <c>Never</c>.
		/// </summary>
		IsEditorBrowsableNever = 0x2000,

	}
}
