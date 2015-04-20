using System;
using System.Collections;
using System.Reflection;
using System.Text;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a property or indexer declaration for an anonymous type.
	/// </summary>
	internal class AnonymousTypePropertyDeclaration : PropertyDeclaration, IDomMember {

		private IDomType returnType;
	
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>AnonymousTypePropertyDeclaration</c> class. 
		/// </summary>
		/// <param name="modifiers">The modifiers.</param>
		/// <param name="name">The name.</param>
		/// <param name="returnType">The resolved return type.</param>
		public AnonymousTypePropertyDeclaration(Modifiers modifiers, QualifiedIdentifier name, IDomType returnType) : base(modifiers, name) {
			// Initialize 
			this.returnType = returnType;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets a <see cref="IDomTypeReference"/> to the type that is returned by the member.
		/// </summary>
		/// <value>A <see cref="IDomTypeReference"/> to the type that is returned by the member.</value>
		IDomTypeReference IDomMember.ReturnType { 
			get {
				return returnType;
			}
		}

		
	}
}
