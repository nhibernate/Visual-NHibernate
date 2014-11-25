using System;
using System.Collections;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents a .NET member type.
	/// </summary>
	public enum DomMemberType {

		// NOTE: Any changes here need to be made in DomMemberFlags as well

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

	}
}
