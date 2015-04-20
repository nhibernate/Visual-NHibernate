using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Specifies the type of a <see cref="ModifyEventHandlerStatement"/>.
	/// </summary>
	public enum ModifyEventHandlerStatementType {

		/// <summary>
		/// Adds an event handler.
		/// </summary>
		Add,

		/// <summary>
		/// Removes an event handler.
		/// </summary>
		Remove

	}
}
