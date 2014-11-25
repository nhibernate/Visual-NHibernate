using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Slyce.Common
{
	public static class ReflectionHelper
	{
		/// <summary>
		/// Gets Dictionary of extension methods for the supplied types from the supplied assemblies.
		/// </summary>
		/// <param name="assembliesToSearch">Assemblies to search.</param>
		/// <param name="types">Types to find extension methods for.</param>
		/// <returns>Dictionary of extension methods.</returns>
		public static Dictionary<Type, List<MethodInfo>> GetExtensionMethods(List<Assembly> assembliesToSearch, List<Type> types)
		{
			Dictionary<Type, List<MethodInfo>> typeExtensions = new Dictionary<Type, List<MethodInfo>>();

			foreach (var type in types)
			{
				typeExtensions.Add(type, new List<MethodInfo>());
			}
			foreach (var assembly in assembliesToSearch)
			{
				foreach (var type in types)
				{
					typeExtensions[type].AddRange(assembly.GetExtensionMethods(type));
				}
			}
			return typeExtensions;
		}

		/// <summary>
		/// Gets all extension methods for the supplied type in the supplied assembly.
		/// See: http://stackoverflow.com/questions/299515/c-reflection-to-identify-extension-methods
		/// </summary>
		/// <param name="assembly">Assembly to search.</param>
		/// <param name="extendedType">Type whose extension methods must be returned.</param>
		/// <returns>List of extension methods.</returns>
		public static IEnumerable<MethodInfo> GetExtensionMethods(this Assembly assembly, Type extendedType)
		{
			var query = from type in assembly.GetTypes()
						where type.IsSealed && !type.IsGenericType && !type.IsNested
						from method in type.GetMethods(BindingFlags.Static
							| BindingFlags.Public | BindingFlags.NonPublic)
						where method.IsDefined(typeof(ExtensionAttribute), false)
						where method.GetParameters()[0].ParameterType == extendedType
						select method;
			return query;
		}

	}
}
