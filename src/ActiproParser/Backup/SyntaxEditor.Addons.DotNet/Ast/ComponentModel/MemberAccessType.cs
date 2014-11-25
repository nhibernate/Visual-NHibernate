using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Specifies the type of a member access.
	/// </summary>
	public enum MemberAccessType {
		
		/// <summary>
		/// The standard type of access.  When used on XML elements, this also indicates element access.
		/// </summary>
		Default,

		/// <summary>
		/// XML attribute access.  In VB: <c>element.@attributename</c>
		/// </summary>
		XmlAttribute,

		/// <summary>
		/// XML descendent access.  In VB: <c>element...&lt;childelement&gt;</c>
		/// </summary>
		XmlDescendent

	}
}
