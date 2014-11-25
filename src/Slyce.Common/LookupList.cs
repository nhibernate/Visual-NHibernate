using System.Collections.Generic;

namespace Slyce.Common
{
	public class LookupList<TKey, TValue>
	{
		private readonly Dictionary<TKey, List<TValue>> internalDictionary = new Dictionary<TKey, List<TValue>>();

		public void Add(TKey key, TValue value)
		{
			if (internalDictionary.ContainsKey(key) == false)
				internalDictionary[key] = new List<TValue>();

			internalDictionary[key].Add(value);
		}

		public IEnumerable<TValue> this[TKey key]
		{
			get { return internalDictionary[key]; }
		}

		public void Clear()
		{
			internalDictionary.Clear();
		}

		public bool ContainsKey(TKey key)
		{
			return internalDictionary.ContainsKey(key);
		}

		public bool Remove(TKey key)
		{
			return internalDictionary.Remove(key);
		}

		public List<TKey> Keys
		{
			get { 
				List<TKey> keys = new List<TKey>();
				
				foreach (var key in internalDictionary.Keys)
					keys.Add(key);

				return keys;
			}
		}

		public List<TValue> Values
		{
			get
			{
				List<TValue> values = new List<TValue>();

				foreach (var value in internalDictionary.Values)
					values.AddRange(value);

				return values;
			}
		}
	}
}