using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Specifies the level through which an <see cref="AstVisitor"/> should visit children.
	/// </summary>
	public enum AstVisitorLevel {

		/// <summary>
		/// Only namespaces, types, and type members should be visited.
		/// </summary>
		TypeMembers,

		/// <summary>
		/// All nodes should be visited.
		/// </summary>
		All,

	}
}
