using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents the base requirements for a .NET parameter.
	/// </summary>
	public interface IDomParameter {

		/// <summary>
		/// Close the parameter but assigns it the specified alternate <see cref="IDomTypeReference"/>.
		/// </summary>
		/// <param name="parameterType">A <see cref="IDomTypeReference"/> indicating the type.</param>
		/// <returns>The cloned parameter.</returns>
		IDomParameter CloneForType(IDomTypeReference parameterType);
		
		/// <summary>
		/// Gets whether the parameter is a by-reference parameter.
		/// </summary>
		/// <value>
		/// <c>true</c> if the parameter is a by-reference parameter; otherwise, <c>false</c>.
		/// </value>
		bool IsByReference { get; }

		/// <summary>
		/// Gets whether the parameter is an output parameter.
		/// </summary>
		/// <value>
		/// <c>true</c> if the parameter is an output parameter; otherwise, <c>false</c>.
		/// </value>
		bool IsOutput { get; }

		/// <summary>
		/// Gets the name of the parameter.
		/// </summary>
		/// <value>The name of the parameter.</value>
		string Name { get; }
		
		/// <summary>
		/// Gets the <see cref="IDomTypeReference"/> of the parameter.
		/// </summary>
		/// <value>The <see cref="IDomTypeReference"/> of the parameter.</value>
		IDomTypeReference ParameterType { get; }

	}
}
