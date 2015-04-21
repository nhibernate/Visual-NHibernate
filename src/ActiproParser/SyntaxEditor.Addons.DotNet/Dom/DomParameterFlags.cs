using System;
using System.Collections;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents .NET parameter flags.
	/// </summary>
	[Flags()]
	internal enum DomParameterFlags {

		/// <summary>
		/// No flags are set.
		/// </summary>
		None = 0x0,

		/// <summary>
		/// The type is a structure type.
		/// </summary>
		Ref = 0x1,

		/// <summary>
		/// The type is an interface type.
		/// </summary>
		Out = 0x2,

	}
}
