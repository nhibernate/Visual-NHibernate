using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Specifies the type of a <see cref="UnstructuredErrorOnErrorStatement"/>.
	/// </summary>
	public enum UnstructuredErrorOnErrorStatementType {

		/// <summary>
		/// Resets the most recent exception to <c>null</c>/
		/// </summary>
		ResetException,

		/// <summary>
		/// Resets the most recent exception-handler location to <c>null</c>.
		/// </summary>
		ResetExceptionHandlerLocation,

		/// <summary>
		/// Establishes the label as the most recent exception-handler location.
		/// </summary>
		EstablishHandlerLocation,

		/// <summary>
		/// Establishes the Resume Next behavior as the most recent exception-handler location.
		/// </summary>
		EstablishResumeNext,

	}
}
