using System.Collections.Generic;

namespace ArchAngel.Interfaces
{
	public class TemplateData
	{
		private readonly Dictionary<string, object> internalDictionary = new Dictionary<string, object>();

		public void Clear() { internalDictionary.Clear(); }

		public void Put(string key, object value)
		{
			internalDictionary[key] = value;
		}

		public T Get<T>(string key)
		{
			if (Has<T>(key)) return (T)internalDictionary[key];

			return default(T);
		}

		public bool Has<T>(string key)
		{
			if (internalDictionary.ContainsKey(key) == false)
				return false;

			object data = internalDictionary[key];
			return data is T;
		}
	}
}
