using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Xml;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents a .NET member which has parameters that is defined in an assembly.
	/// </summary>
	internal class AssemblyDomParameterizedMember : AssemblyDomMember {

		//
		// NOTE: Any changes made to fields need to be persisted to the cache in AssemblyProjectContent and the cache version number must be incremented
		//

		private IDomParameter[]		parameters;
				
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDomParameterizedMember</c> class.
		/// </summary>
		/// <param name="declaringType">The <see cref="IDomType"/> that declared the member.</param>
		/// <param name="memberFlags">The <see cref="DomMemberFlags"/> for the member.</param>
		/// <param name="name">The name of the member.</param>
		/// <param name="modifiers">The <see cref="Modifiers"/> for the member.</param>
		/// <remarks>This overload should only be used when reading a cache file.</remarks>
		internal AssemblyDomParameterizedMember(IDomType declaringType, DomMemberFlags memberFlags, string name, Modifiers modifiers) : base(declaringType, memberFlags, name, modifiers) {}

		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDomParameterizedMember</c> class.
		/// </summary>
		/// <param name="projectContent">The <see cref="AssemblyProjectContent"/> that defines the member.</param>
		/// <param name="declaringType">The <see cref="IDomType"/> that declared the member.</param>
		/// <param name="memberInfo">The <see cref="MemberInfo"/> to wrap with this object.</param>
		/// <param name="defaultMemberName">The default member name.</param>
		internal AssemblyDomParameterizedMember(AssemblyProjectContent projectContent, IDomType declaringType, MemberInfo memberInfo, string defaultMemberName) : 
			base(projectContent, declaringType, memberInfo) {

			MethodBase methodInfo = null;
			switch (memberInfo.MemberType) {
				case MemberTypes.Constructor: {
					methodInfo = (ConstructorInfo)memberInfo;

					ParameterInfo[] parameterInfo = methodInfo.GetParameters();
					if ((parameterInfo != null) && (parameterInfo.Length > 0)) {
						parameters = new IDomParameter[parameterInfo.Length];
						for (int index = 0; index < parameterInfo.Length; index++)
							parameters[index] = new AssemblyDomParameter(projectContent, parameterInfo[index]);
					}
					break;
				}
				case MemberTypes.Method: {
					methodInfo = (MethodInfo)memberInfo;

					ParameterInfo[] parameterInfo = methodInfo.GetParameters();
					if ((parameterInfo != null) && (parameterInfo.Length > 0)) {
						parameters = new IDomParameter[parameterInfo.Length];
						for (int index = 0; index < parameterInfo.Length; index++)
							parameters[index] = new AssemblyDomParameter(projectContent, parameterInfo[index]);
					}
					break;
				}
				case MemberTypes.Property: {
					PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
					int ignoreLastParameterCount = 0;
					if (propertyInfo.CanRead) {
						try {
							methodInfo = propertyInfo.GetGetMethod(true);
						} catch {}
					}
					if ((methodInfo == null) && (propertyInfo.CanWrite)) {
						try {
							methodInfo = propertyInfo.GetSetMethod(true);
							ignoreLastParameterCount = 1;
						} catch {}
					}

					if (methodInfo != null) {
						ParameterInfo[] parameterInfo = methodInfo.GetParameters();
						if ((parameterInfo != null) && (parameterInfo.Length - ignoreLastParameterCount > 0)) {
							if (memberInfo.Name == defaultMemberName) {
								// 1/24/2011 - Apply the default property modifier
								base.Modifiers |= Modifiers.Default;
							}

							parameters = new IDomParameter[parameterInfo.Length - ignoreLastParameterCount];
							for (int index = 0; index < parameterInfo.Length - ignoreLastParameterCount; index++)
								parameters[index] = new AssemblyDomParameter(projectContent, parameterInfo[index]);
						}
					}
					break;
				}
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Sets the value of the <see cref="Parameters"/> property.
		/// </summary>
		/// <param name="value">The value to set.</param>
		internal void SetParameters(IDomParameter[] value) {
			parameters = value;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the array of <see cref="IDomParameter"/> parameters for the member.
		/// </summary>
		/// <value>The array of <see cref="IDomParameter"/> parameters for the member.</value>
		public override IDomParameter[] Parameters {
			get {
				return parameters;
			}
		}
		
		/// <summary>
		/// Resolve all references to types in the same assembly to the actual type.
		/// </summary>
		/// <param name="projectContent">The <see cref="AssemblyProjectContent"/> to use for resolution.</param>
		protected internal override void ResolveTypePlaceHolders(AssemblyProjectContent projectContent) {
			// Call the base method
			base.ResolveTypePlaceHolders(projectContent);

			if (parameters != null) {
				foreach (AssemblyDomParameter parameter in parameters)
					parameter.ResolveTypePlaceHolders(projectContent);
			}
		}
		
	}
}
