using System;
using System.Collections.Generic;
using System.Linq;

namespace ArchAngel.Providers.CodeProvider.Extensions.IEnumerableExtensions
{
	public static class IEnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
		{
			foreach (var item in list)
				action(item);
		}

		public static IEnumerable<T> Distinct<T, Q>(this IEnumerable<T> list, Func<T, Q> selectorFunction)
		{
			HashSet<Q> items = new HashSet<Q>();

			foreach (var item in list)
			{
				var identifier = selectorFunction(item);
				if (items.Contains(identifier)) continue;

				items.Add(identifier);
				yield return item;
			}
		}

		public static IEnumerable<int> IndicesOfAll<T>(this IEnumerable<T> list, Func<T, bool> selector)
		{
			int index = 0;
			foreach (var item in list)
			{
				if (selector(item))
					yield return index;
				index++;
			}
		}

		public static IEnumerable<IndexedItem<T>> WithIndices<T>(this IEnumerable<T> list)
		{
			int i = 0;
			foreach (var item in list)
			{
				yield return new IndexedItem<T>(i++, item);
			}
		}


		/// <summary>
		/// Takes two lists, and returns the compliment of them.
		/// Creates a new list that contains the items in list2 that do not exist in list1
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list1"></param>
		/// <param name="list2"></param>
		/// <returns></returns>
		public static IEnumerable<T> Compliment<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
		{
			var compliment = new List<T>(list2);
			foreach (var item in list1)
				compliment.Remove(item);
			return compliment;
		}

		public static IEnumerable<KeyValuePair<TLeft, TRight>> Zip<TLeft, TRight>(
				this IEnumerable<TLeft> left, IEnumerable<TRight> right)
		{
			return Zip(left, right, (x, y) => new KeyValuePair<TLeft, TRight>(x, y));
		}

		// accepts a projection from the caller for each pair
		public static IEnumerable<TResult> Zip<TLeft, TRight, TResult>(
			this IEnumerable<TLeft> left, IEnumerable<TRight> right,
			Func<TLeft, TRight, TResult> selector)
		{
			using (IEnumerator<TLeft> leftE = left.GetEnumerator())
			using (IEnumerator<TRight> rightE = right.GetEnumerator())
			{
				while (leftE.MoveNext() && rightE.MoveNext())
				{
					yield return selector(leftE.Current, rightE.Current);
				}
			}
		}

		public static bool UnorderedEqual<T>(this IEnumerable<T> a, IEnumerable<T> b)
		{
			// 1
			// Require that the counts are equal
			if (a.Count() != b.Count())
			{
				return false;
			}
			// 2
			// Initialize new Dictionary of the type
			Dictionary<T, int> d = new Dictionary<T, int>();
			// 3
			// Add each key's frequency from collection A to the Dictionary
			foreach (T item in a)
			{
				int c;
				if (d.TryGetValue(item, out c))
				{
					d[item] = c + 1;
				}
				else
				{
					d.Add(item, 1);
				}
			}
			// 4
			// Add each key's frequency from collection B to the Dictionary
			// Return early if we detect a mismatch
			foreach (T item in b)
			{
				int c;
				if (d.TryGetValue(item, out c))
				{
					if (c == 0)
					{
						return false;
					}
					else
					{
						d[item] = c - 1;
					}
				}
				else
				{
					// Not in dictionary
					return false;
				}
			}
			// 5
			// Verify that all frequencies are zero
			foreach (int v in d.Values)
			{
				if (v != 0)
				{
					return false;
				}
			}
			// 6
			// We know the collections are equal
			return true;
		}
	}

	public class IndexedItem<T>
	{
		public int Index { get; private set; }
		public T Value { get; private set; }

		public IndexedItem(int index, T value)
		{
			Index = index;
			Value = value;
		}
	}
}
