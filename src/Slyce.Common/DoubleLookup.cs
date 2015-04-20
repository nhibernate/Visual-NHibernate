using System;
using System.Collections.Generic;

namespace Slyce.Common
{

	public class DoubleLookup<TKey, TValue>
	{
		private readonly IDictionary<TKey, TValue> keys;
		private readonly IDictionary<TValue, TKey> values;
		public IEnumerable<KeyValuePair<TKey, TValue>> Pairs { get { return keys; } }
		public IEnumerable<TKey> Keys { get { return keys.Keys; } }
		public IEnumerable<TValue> Values { get { return values.Keys; } }

		public int Count { get { return keys.Count; } }

		public DoubleLookup()
		{
			values = new Dictionary<TValue, TKey>();
			keys = new Dictionary<TKey, TValue>();
		}

		public DoubleLookup(IEqualityComparer<TKey> keyComp, IEqualityComparer<TValue> valComp)
		{
			values = new Dictionary<TValue, TKey>(valComp);
			keys = new Dictionary<TKey, TValue>(keyComp);
		}

		public bool Contains(TKey key)
		{
			return keys.ContainsKey(key);
		}

		public bool Contains(TValue val)
		{
			return values.ContainsKey(val);
		}

		public void Add(TKey key, TValue value)
		{
			keys.Add(key, value);
			values.Add(value, key);
		}

		public TKey this[TValue val]
		{
			get
			{
				if (val == null)
					throw new ArgumentNullException("val", "Value cannot be null");

				if (values.ContainsKey(val))
					return values[val];
				else
					foreach (var key in values.Keys)
						if (key.GetType() == val.GetType())
							return values[key];

				throw new Exception("Screen not found.");
			}
			set
			{
				if (val == null)
					throw new ArgumentNullException("val", "Value cannot be null");
				if (value == null)
					throw new ArgumentNullException("value", "Key cannot be null");

				keys.Add(value, val);
				values.Add(val, value);
			}
		}

		public TValue this[TKey key]
		{
			get
			{
				if (key == null)
					throw new ArgumentNullException("key", "Key cannot be null");

				return keys[key];
			}
			set
			{
				if (key == null)
					throw new ArgumentNullException("key", "Key cannot be null");
				if (value == null)
					throw new ArgumentNullException("value", "Value cannot be null");

				keys.Add(key, value);
				values.Add(value, key);
			}
		}

		public TKey GetKeyFromValue(TValue value)
		{
			return values[value];
		}

		public TValue GetValueFromKey(TKey key)
		{
			return keys[key];
		}

		public void Clear()
		{
			keys.Clear();
			values.Clear();
		}
	}

}
