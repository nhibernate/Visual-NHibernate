using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ArchAngel.Interfaces
{
	public static class ApiExtensionHelper
	{
		private static readonly HashSet<string> OverriddenMethods = new HashSet<string>();

		public static void SetMethodOverridden(string typeName, string methodName, IEnumerable<string> parameterTypes)
		{
			OverriddenMethods.Add(ContructDictKey(typeName, methodName, parameterTypes));
		}

		private static string ContructDictKey(string typeName, string methodName, IEnumerable<string> parameterTypes)
		{
			return typeName + "." + methodName + "++" + string.Join(",", parameterTypes.ToArray());
		}

		public static bool IsOverridden(Expression<Func<string>> methodLamba)
		{
			var method = methodLamba.Body as MethodCallExpression;
			if (method == null) throw new Exception("Cannot determine method information from the given lamba expression. Must be an expression like this: () => MethodCall(param, param2)");

			List<string> parameterTypes = new List<string>();

			foreach (var param in method.Arguments)
			{
				parameterTypes.Add(param.Type.FullName);
			}

			return IsOverridden(method.Method.DeclaringType.FullName, method.Method.Name, parameterTypes);
		}

		public static bool RunIfExtended(Type typeName, string methodName, out object returnValue, params object[] parameters)
		{
			returnValue = null;
			if (SharedData.CurrentProject == null) return false;
			return SharedData.CurrentProject.CallApiExtensionFunction(typeName + "." + methodName, out returnValue, ref parameters);
		}

		public static bool IsOverridden(string typeName, string methodName, IEnumerable<string> parameterTypes)
		{
			return OverriddenMethods.Contains(ContructDictKey(typeName, methodName, parameterTypes));
		}

		private static T Extension<T>(string typeName, string methodName)
		{
			object obj2;
			object[] parameters = new object[0];

			if (SharedData.CurrentProject.CallApiExtensionFunction(typeName + "." + methodName, out obj2, ref parameters))
			{
				return (T)obj2;
			}
			return default(T);
		}

		public static T Extension<T>(Type type, string methodName, params object[] parameters)
		{
			object obj2;
			if (SharedData.CurrentProject.CallApiExtensionFunction(type.FullName + "." + methodName, out obj2, ref parameters))
			{
				return (T)obj2;
			}
			return default(T);
		}
	}
}
