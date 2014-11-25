using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents a .NET parameter that is defined in an assembly.
	/// </summary>
	internal class AssemblyDomParameter : IDomParameter {
		
		//
		// NOTE: Any changes made to fields need to be persisted to the cache in AssemblyProjectContent and the cache version number must be incremented
		//

		private string				name;
		private DomParameterFlags	parameterFlags							= DomParameterFlags.None;
		private IDomTypeReference	parameterType;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDomParameter</c> class.
		/// </summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="parameterFlags">The <see cref="DomParameterFlags"/> for the parameter.</param>
		/// <param name="parameterType">The <see cref="IDomTypeReference"/> of the parameter.</param>
		/// <remarks>This overload should only be used when reading a cache file.</remarks>
		internal AssemblyDomParameter(string name, DomParameterFlags parameterFlags, IDomTypeReference parameterType) {
			// Initialize parameters
			this.name			= name;
			this.parameterFlags	= parameterFlags;
			this.parameterType	= parameterType;
		}
		
		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDomParameter</c> class.
		/// </summary>
		/// <param name="projectContent">The <see cref="AssemblyProjectContent"/> that defines the parameter.</param>
		/// <param name="parameterInfo">The <see cref="ParameterInfo"/> to wrap with this object.</param>
		internal AssemblyDomParameter(AssemblyProjectContent projectContent, ParameterInfo parameterInfo) {
			// Initialize parameters
			name = parameterInfo.Name;

			if (parameterInfo.IsOut)
				parameterFlags |= DomParameterFlags.Out;
			else if (parameterInfo.ParameterType.IsByRef)
				parameterFlags |= DomParameterFlags.Ref;

			parameterType = projectContent.GetTypeReference(parameterInfo.ParameterType);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Close the parameter but assigns it the specified alternate <see cref="IDomTypeReference"/>.
		/// </summary>
		/// <param name="parameterType">A <see cref="IDomTypeReference"/> indicating the type.</param>
		/// <returns>The cloned parameter.</returns>
		IDomParameter IDomParameter.CloneForType(IDomTypeReference parameterType) {
			return new AssemblyDomParameter(name, parameterFlags, parameterType);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns whether the specified <see cref="DomParameterFlags"/> flag is set.
		/// </summary>
		/// <param name="parameterFlags">The <see cref="DomParameterFlags"/> value to examine.</param>
		/// <param name="flag">The <see cref="DomParameterFlags"/> to check for.</param>
		/// <returns>
		/// <c>true</c> if the specified <see cref="DomParameterFlags"/> flag is set; otherwise, <c>false</c>.
		/// </returns>
		internal static bool HasParameterFlag(DomParameterFlags parameterFlags, DomParameterFlags flag) {
			return ((parameterFlags & flag) == flag);
		}
		
		/// <summary>
		/// Gets or sets the <see cref="DomParameterFlags"/> for the parameter.
		/// </summary>
		/// <value>The <see cref="DomParameterFlags"/> for the parameter.</value>
		internal DomParameterFlags ParameterFlags {
			get {
				return parameterFlags;
			}
			set {
				parameterFlags = value;
			}
		}
		
		/// <summary>
		/// Sets or clears the specified <see cref="DomParameterFlags"/> flag.
		/// </summary>
		/// <param name="parameterFlags">The <see cref="DomParameterFlags"/> value to update.</param>
		/// <param name="flag">The <see cref="DomParameterFlags"/> to set or clear.</param>
		/// <param name="setBit">Whether to set the flag; otherwise, the flag is cleared.</param>
		/// <returns>The updated <see cref="DomParameterFlags"/> value.</returns>
		internal static DomParameterFlags SetParameterFlag(DomParameterFlags parameterFlags, DomParameterFlags flag, bool setBit) {
			if (setBit)
				return parameterFlags | flag;
			else
				return parameterFlags & (~flag);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets whether the parameter is a by-reference parameter.
		/// </summary>
		/// <value>
		/// <c>true</c> if the parameter is a by-reference parameter; otherwise, <c>false</c>.
		/// </value>
		public bool IsByReference { 
			get {
				return AssemblyDomParameter.HasParameterFlag(parameterFlags, DomParameterFlags.Ref);
			}
		}

		/// <summary>
		/// Gets whether the parameter is an output parameter.
		/// </summary>
		/// <value>
		/// <c>true</c> if the parameter is an output parameter; otherwise, <c>false</c>.
		/// </value>
		public bool IsOutput { 
			get {
				return AssemblyDomParameter.HasParameterFlag(parameterFlags, DomParameterFlags.Out);
			}
		}

		/// <summary>
		/// Gets the name of the parameter.
		/// </summary>
		/// <value>The name of the parameter.</value>
		public string Name { 
			get {
				return name;
			}
		}

		/// <summary>
		/// Gets the <see cref="IDomTypeReference"/> of the parameter.
		/// </summary>
		/// <value>The <see cref="IDomTypeReference"/> of the parameter.</value>
		public IDomTypeReference ParameterType { 
			get {
				return parameterType;
			}
		}
		
		/// <summary>
		/// Resolve all references to types in the same assembly to the actual type.
		/// </summary>
		/// <param name="projectContent">The <see cref="AssemblyProjectContent"/> to use for resolution.</param>
		protected internal virtual void ResolveTypePlaceHolders(AssemblyProjectContent projectContent) {
			if (parameterType is AssemblyDomTypePlaceHolder)
				parameterType = projectContent.ResolveAssemblyDomTypePlaceHolder((AssemblyDomTypePlaceHolder)parameterType);
			else if (parameterType is AssemblyDomTypeReference)
				((AssemblyDomTypeReference)parameterType).ResolveTypePlaceHolders(projectContent);
		}

		/// <summary>
		/// Converts the object to a <c>String</c>.
		/// </summary>
		/// <returns>
		/// A string whose value represents this object.
		/// </returns>
		public override string ToString() {
			return String.Format("AssemblyDomParameter[{0}]", name);
		}

	}
}
