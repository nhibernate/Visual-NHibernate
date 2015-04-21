using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents the base requirements for a .NET type member.
	/// </summary>
	public interface IDomMember {
		
		/// <summary>
		/// Gets the access-related values of <see cref="Modifiers"/>.
		/// </summary>
		/// <value>The access-related values of <see cref="Modifiers"/>.</value>
		Modifiers AccessModifiers { get; }

		/// <summary>
		/// Gets a <see cref="IDomTypeReference"/> to the type that declares the member.
		/// </summary>
		/// <value>A <see cref="IDomTypeReference"/> to the type that declares the member.</value>
		IDomTypeReference DeclaringType { get; }
		
		/// <summary>
		/// Gets the <see cref="DomDocumentationProvider"/> for the member.
		/// </summary>
		/// <value>The <see cref="DomDocumentationProvider"/> for the member.</value>
		DomDocumentationProvider DocumentationProvider { get; }
		
		/// <summary>
		/// Gets the type arguments if this is a generic method definition.
		/// </summary>
		/// <value>The type arguments if this is a generic method definition.</value>
		ICollection GenericTypeArguments { get; }
		
		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		int ImageIndex { get; }
		
		/// <summary>
		/// Gets whether the member has an <c>EditorBrowsableAttribute</c> on it with a value of <c>Never</c>.
		/// </summary>
		/// <value>
		/// <c>true</c> if the member has an <c>EditorBrowsableAttribute</c> on it with a value of <c>Never</c>; otherwise, <c>false</c>.
		/// </value>
		bool IsEditorBrowsableNever { get; }
		
		/// <summary>
		/// Gets whether the member is marked with an <c>ExtensionAttribute</c>.
		/// </summary>
		/// <value>
		/// <c>true</c> if the member is marked with an <c>ExtensionAttribute</c>; otherwise, <c>false</c>.
		/// </value>
		bool IsExtension { get; }
		
		/// <summary>
		/// Gets whether the member is a generic method.
		/// </summary>
		/// <value>
		/// <c>true</c> if the method is a generic method; otherwise, <c>false</c>.
		/// </value>
		bool IsGenericMethod { get; }

		/// <summary>
		/// Gets whether the member is a generic method definition, from which other generic methods can be constructed.
		/// </summary>
		/// <value>
		/// <c>true</c> if the method is a generic method definition, from which other generic methods can be constructed; otherwise, <c>false</c>.
		/// </value>
		bool IsGenericMethodDefinition { get; }

		/// <summary>
		/// Gets whether the member is static.
		/// </summary>
		/// <value>
		/// <c>true</c> if the member is static; otherwise, <c>false</c>.
		/// </value>
		bool IsStatic { get; }
		
		/// <summary>
		/// Gets a <see cref="DomMemberType"/> that indicates the type of member.
		/// </summary>
		/// <value>A <see cref="DomMemberType"/> that indicates the type of member.</value>
		DomMemberType MemberType { get; }
		
		/// <summary>
		/// Gets the modifiers for the member.
		/// </summary>
		/// <value>The modifiers for the member.</value>
		Modifiers Modifiers { get; }

		/// <summary>
		/// Gets the name of the member.
		/// </summary>
		/// <value>The name of the member.</value>
		string Name { get; }
		
		/// <summary>
		/// Gets the array of <see cref="IDomParameter"/> parameters for the member.
		/// </summary>
		/// <value>The array of <see cref="IDomParameter"/> parameters for the member.</value>
		IDomParameter[] Parameters { get; }
		
		/// <summary>
		/// Gets a <see cref="IDomTypeReference"/> to the type that is returned by the member.
		/// </summary>
		/// <value>A <see cref="IDomTypeReference"/> to the type that is returned by the member.</value>
		IDomTypeReference ReturnType { get; }

	}
}
