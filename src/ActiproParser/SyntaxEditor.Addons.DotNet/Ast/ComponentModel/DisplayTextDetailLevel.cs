using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Specifies the detail level for display text.
	/// </summary>
	public enum DisplayTextDetailLevel {

		/// <summary>
		/// Returns a simple representation of the AST node.
		/// </summary>
		Simple,

		/// <summary>
		/// Returns a simple representation of the AST node that fully qualifies type references.
		/// </summary>
		SimpleFullyQualified,

	}
}
