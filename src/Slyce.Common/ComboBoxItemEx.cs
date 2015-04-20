using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Slyce.Common
{
	public class ComboBoxItemEx<T>
	{
	    private Guid identifier;

		private readonly T obj;
		private readonly Func<T, string> stringFunction;

		public ComboBoxItemEx(T obj, Func<T, string> stringFunction)
		{
			this.obj = obj;
			this.stringFunction = stringFunction;
		    identifier = Guid.NewGuid();
		}

		public T Object
		{
			get
			{
				return obj;
			}
		}

	    public bool Equals(ComboBoxItemEx<T> other)
	    {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;
	        return other.identifier.Equals(identifier);
	    }

	    public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj)) return false;
	        if (ReferenceEquals(this, obj)) return true;
	        if (obj.GetType() != typeof (ComboBoxItemEx<T>)) return false;
	        return Equals((ComboBoxItemEx<T>) obj);
	    }

	    public override int GetHashCode()
	    {
	        return identifier.GetHashCode();
	    }

	    public override string ToString()
		{
// ReSharper disable CompareNonConstrainedGenericWithNull
			if (obj == null) return "";
// ReSharper restore CompareNonConstrainedGenericWithNull

			return stringFunction(obj) ?? "";
		}
	}

	public static class ComboBoxItemExtensions
	{
		public static void Fill<T>(this ComboBox box, IEnumerable<T> items, Func<T, string> nameFunc)
		{
			foreach(var item in items)
			{
				box.Items.Add(new ComboBoxItemEx<T>(item, nameFunc));
			}
		}

		public static void SetSelectedItem<T>(this ComboBox box, T item) where T : class
		{
			for (int i = 0; i < box.Items.Count; i++)
			{
				if(((ComboBoxItemEx<T>)box.Items[i]).Object == item)
				{
					box.SelectedIndex = i;
				}
			}
		}

		public static IEnumerable<KeyValuePair<int, T>> GetInnerObjects<T>(this ComboBox box)
		{
			for(int i = 0; i < box.Items.Count; i++)
			{
				var wrapper = box.Items[i] as ComboBoxItemEx<T>;
				if(wrapper == null) continue;

				yield return new KeyValuePair<int, T>(i, wrapper.Object);
			}
		}

		public static T GetSelectedItem<T>(this ComboBox box)
		{
			var item = box.SelectedItem as ComboBoxItemEx<T>;
			return item == null ? default(T) : item.Object;
		}
	}

	public static class ListBoxItemExtensions
	{
		public static void SetSelectedItem<T>(this ListBox box, T item) where T : class
		{
			for (int i = 0; i < box.Items.Count; i++)
			{
				if (((ComboBoxItemEx<T>)box.Items[i]).Object == item)
				{
					box.SelectedIndex = i;
				}
			}
		}

		public static IEnumerable<KeyValuePair<int, T>> GetInnerObjects<T>(this ListBox box)
		{
			for (int i = 0; i < box.Items.Count; i++)
			{
				var wrapper = box.Items[i] as ComboBoxItemEx<T>;
				if (wrapper == null) continue;

				yield return new KeyValuePair<int, T>(i, wrapper.Object);
			}
		}

		public static T GetSelectedItem<T>(this ListBox box)
		{
			var item = box.SelectedItem as ComboBoxItemEx<T>;
			return item == null ? default(T) : item.Object;
		}
	}
}