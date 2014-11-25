using System.Collections.Generic;

namespace Slyce.Common.ExtensionMethods
{
	public static class DictionaryExtensions
	{
		public static U GetValueOrDefault<T, U>(this IDictionary<T, U> dictionary, T key)
		{
			U obj;
			if(dictionary.TryGetValue(key, out obj))
			{
				return obj;
			}

			return default(U);
		}
	}
}