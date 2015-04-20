using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Xml;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents a generic .NET member that is defined in an assembly.
	/// </summary>
	internal class AssemblyDomGenericMember : AssemblyDomParameterizedMember {

		//
		// NOTE: Any changes made to fields need to be persisted to the cache in AssemblyProjectContent and the cache version number must be incremented
		//

		private IDomTypeReference[]	genericTypeArguments;
				
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDomGenericMember</c> class.
		/// </summary>
		/// <param name="declaringType">The <see cref="IDomType"/> that declared the member.</param>
		/// <param name="memberFlags">The <see cref="DomMemberFlags"/> for the member.</param>
		/// <param name="name">The name of the member.</param>
		/// <param name="modifiers">The <see cref="Modifiers"/> for the member.</param>
		/// <remarks>This overload should only be used when reading a cache file.</remarks>
		internal AssemblyDomGenericMember(IDomType declaringType, DomMemberFlags memberFlags, string name, Modifiers modifiers) : base(declaringType, memberFlags, name, modifiers) {}

		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDomGenericMember</c> class.
		/// </summary>
		/// <param name="projectContent">The <see cref="AssemblyProjectContent"/> that defines the member.</param>
		/// <param name="declaringType">The <see cref="IDomType"/> that declared the member.</param>
		/// <param name="memberInfo">The <see cref="MemberInfo"/> to wrap with this object.</param>
		internal AssemblyDomGenericMember(AssemblyProjectContent projectContent, IDomType declaringType, MemberInfo memberInfo) : base(projectContent, declaringType, memberInfo, null) {
			switch (memberInfo.MemberType) {
				case MemberTypes.Method: {
					MethodInfo methodInfo = (MethodInfo)memberInfo;

					// Get generic type arguments
					#if !NET11
					Type[] arguments = methodInfo.GetGenericArguments();
					if ((arguments != null) && (arguments.Length > 0)) {
						genericTypeArguments = new IDomTypeReference[arguments.Length];
						for (int index = 0; index < arguments.Length; index++)
							genericTypeArguments[index] = projectContent.GetTypeReference(arguments[index]);
					}
					#endif
					break;
				}
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Sets the value of the <see cref="GenericTypeArguments"/> property.
		/// </summary>
		/// <param name="value">The value to set.</param>
		internal void SetGenericTypeArguments(IDomTypeReference[] value) {
			genericTypeArguments = value;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the type arguments if this is a generic method definition.
		/// </summary>
		/// <value>The type arguments if this is a generic method definition.</value>
		public override ICollection GenericTypeArguments { 
			get {
				return genericTypeArguments;
			}
		}
		
		/// <summary>
		/// Resolve all references to types in the same assembly to the actual type.
		/// </summary>
		/// <param name="projectContent">The <see cref="AssemblyProjectContent"/> to use for resolution.</param>
		protected internal override void ResolveTypePlaceHolders(AssemblyProjectContent projectContent) {
			// Call the base method
			base.ResolveTypePlaceHolders(projectContent);

			if (genericTypeArguments != null) {
				for (int index = genericTypeArguments.Length - 1; index >= 0; index--) {
					if (genericTypeArguments[index] is AssemblyDomTypePlaceHolder)
						genericTypeArguments[index] = projectContent.ResolveAssemblyDomTypePlaceHolder((AssemblyDomTypePlaceHolder)genericTypeArguments[index]);
					else if (genericTypeArguments[index] is AssemblyDomTypeReference)
						((AssemblyDomTypeReference)genericTypeArguments[index]).ResolveTypePlaceHolders(projectContent);
				}
			}
		}

	}
}
