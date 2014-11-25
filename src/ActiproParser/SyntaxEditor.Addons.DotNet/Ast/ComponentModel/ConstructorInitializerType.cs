using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Specifies the type of constructor initializer.
	/// </summary>
	public enum ConstructorInitializerType {
		
		/// <summary>
		/// No constructor initializer.
		/// </summary>
		None,

		/// <summary>
		/// This reference.
		/// </summary>
		This,

		/// <summary>
		/// Base reference.
		/// </summary>
		Base,

	}
}
