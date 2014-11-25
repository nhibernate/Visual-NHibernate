using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Specifies the type of a literal.
	/// </summary>
	public enum LiteralType {
		
		/// <summary>
		/// No valid literal type.
		/// </summary>
		None,

		/// <summary>
		/// True.
		/// </summary>
		True,

		/// <summary>
		/// False.
		/// </summary>
		False,

		/// <summary>
		/// A decimal integer number.
		/// </summary>
		DecimalInteger,

		/// <summary>
		/// A hexidecimal integer number.
		/// </summary>
		HexadecimalInteger,

		/// <summary>
		/// An octal integer number.
		/// Used in Visual Basic only.
		/// </summary>
		OctalInteger,

		/// <summary>
		/// A real number.
		/// </summary>
		Real,

		/// <summary>
		/// A character.
		/// </summary>
		Character,

		/// <summary>
		/// A string.
		/// </summary>
		String,

		/// <summary>
		/// A verbatim string.
		/// </summary>
		VerbatimString,

		/// <summary>
		/// A <see cref="DateTime"/>.
		/// </summary>
		Date,

		/// <summary>
		/// An XML literal.
		/// </summary>
		Xml,

		/// <summary>
		/// A null value.
		/// </summary>
		Null,
	}
}
