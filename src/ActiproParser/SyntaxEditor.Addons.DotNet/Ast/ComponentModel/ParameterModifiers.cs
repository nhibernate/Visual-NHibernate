using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Specifies the modifiers for a parameter.
	/// </summary>
	[Flags()]
	public enum ParameterModifiers {

		/// <summary>
		/// No modifier.
		/// </summary>
		None = 0x0,

		/// <summary>
		/// A pass-by-reference parameter.
		/// </summary>
		Ref = 0x1,

		/// <summary>
		/// An output parameter.
		/// </summary>
		Out = 0x2,

		/// <summary>
		/// The parameter is a parameter array.
		/// </summary>
		ParameterArray = 0x4,

		/// <summary>
		/// The parameter is optional.
		/// Used in Visual Basic only.
		/// </summary>
		Optional = 0x8,

	}
}
