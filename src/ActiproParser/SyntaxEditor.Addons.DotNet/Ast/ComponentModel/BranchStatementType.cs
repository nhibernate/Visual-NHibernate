using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Specifies the type of a <see cref="BranchStatement"/>.
	/// </summary>
	public enum BranchStatementType {

		/// <summary>
		/// A goto statement, implemented by the <see cref="GotoStatement"/> class.
		/// </summary>
		Goto,

		/// <summary>
		/// An exit statement, implemented by the <see cref="ExitStatement"/> class.
		/// </summary>
		Exit,

		/// <summary>
		/// A continue statement, implemented by the <see cref="ContinueStatement"/> class.
		/// </summary>
		Continue,

		/// <summary>
		/// A stop statement.
		/// Used in Visual Basic only.
		/// </summary>
		Stop,

		/// <summary>
		/// An end statement.
		/// Used in Visual Basic only.
		/// </summary>
		End,
	
		/// <summary>
		/// A return statement, implemented by the <see cref="ReturnStatement"/> class.
		/// </summary>
		Return


	}
}
