using System;
using System.Collections.Generic;

namespace Slyce.Common.Extension_Methods
{
	public static class ReflectionExtensions
	{
		public static IEnumerable<string> GetBaseTypeAndInterfacesFullNames(this Type type)
		{
			Queue<Type> typesToSearch = new Queue<Type>();
			HashSet<string> typeNames = new HashSet<string>();

			typesToSearch.Enqueue(type);

			while(typesToSearch.Count > 0)
			{
				Type currentType = typesToSearch.Dequeue();

				if (typeNames.Contains(currentType.FullName))
					continue; // Skip types we have already searched
				
				typeNames.Add(currentType.FullName);

				if(currentType.BaseType != null)
				{
					typesToSearch.Enqueue(currentType.BaseType);
				}

				foreach(var @interface in currentType.GetInterfaces())
				{
					typesToSearch.Enqueue(@interface);
				}
			}

			typeNames.Remove(type.FullName);

			return typeNames;
		}
	}
}
