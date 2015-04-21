using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Specifies the type of a comment.
	/// </summary>
	public enum CommentType {

		/// <summary>
		/// A single-line comment.
		/// </summary>
		SingleLine,

		/// <summary>
		/// A multi-line comment.
		/// </summary>
		MultiLine,

		/// <summary>
		/// A documentation comment.
		/// </summary>
		Documentation,

	}
}
