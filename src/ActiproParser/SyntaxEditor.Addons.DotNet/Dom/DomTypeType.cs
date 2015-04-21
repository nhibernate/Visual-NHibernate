using System;
using System.Collections;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents a .NET type type.
	/// </summary>
	public enum DomTypeType {

		/// <summary>
		/// A class type.
		/// </summary>
		Class,

		/// <summary>
		/// A structure type.
		/// </summary>
		Structure,

		/// <summary>
		/// An interface type.
		/// </summary>
		Interface,

		/// <summary>
		/// An enumeration type.
		/// </summary>
		Enumeration,

		/// <summary>
		/// A delegate type.
		/// </summary>
		Delegate,

		/// <summary>
		/// A standard module type.
		/// </summary>
		StandardModule,
		
	}
}
