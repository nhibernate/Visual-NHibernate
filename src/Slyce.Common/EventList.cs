using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Slyce.Common
{
    #region Delegates
    public delegate void ListItemEventHandler(object sender, ListItemEventArgs e);
    public delegate void ListRangeEventHandler(object sender, ListRangeEventArgs e);
    #endregion

    /// <summary>
    /// A version of a generic list List&lt;T&gt; that raises events.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable, DebuggerDisplay("Count = {Count}")]
    public class EventList<T> : List<T>
    {
    	public event EventHandler OnCleared;
        public event ListItemEventHandler OnItemAdded;
        public event ListItemEventHandler OnItemModified;
        public event EventHandler OnItemRemoved;
        public event ListRangeEventHandler OnRangeAdded;
        public event EventHandler OnRangeRemoved;
        public event EventHandler OnCollectionModified;

    	protected bool EventsSuppressed { get; private set; }

    	public new T this[int index]
        {
            get { return base[index]; }
            set
            {
                bool equal = false;

                if (base[index] != null)
                {
                    equal = base[index].Equals(value);
                }
                else if (base[index] == null && value == null)
                {
                    equal = true;
                }
                // Don't replace the object or raise an event if we are inserting the same object
                if (!equal)
                {
                    base[index] = value;
                    ItemModified(new ListItemEventArgs(index));
                }
            }
        }

        public new void Add(T item)
        {
            base.Add(item);
            ItemAdded(new ListItemEventArgs(Count - 1));
        }

        public new void AddRange(IEnumerable<T> collection)
        {
            InsertRange(Count, collection);
        }

        public new void Clear()
        {
            base.Clear();
            Cleared(EventArgs.Empty);
        }

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            ItemAdded(new ListItemEventArgs(index));
        }

        public new void InsertRange(int index, IEnumerable<T> collection)
        {
            base.InsertRange(index, collection);
            RangeAdded(new ListRangeEventArgs(index, Count - index));
        }

        public new bool Remove(T item)
        {
            bool result = base.Remove(item);

            if (result)
            {
                ItemRemoved(EventArgs.Empty);
            }
            return result;
        }

        public new int RemoveAll(Predicate<T> match)
        {
            int count = base.RemoveAll(match);

            if (count > 0)
            {
                RangeRemoved(EventArgs.Empty);
            }
            return count;
        }

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            ItemRemoved(EventArgs.Empty);
        }

        public new void RemoveRange(int index, int count)
        {
            int listCount = Count;
            base.RemoveRange(index, count);

            if (listCount != Count)
            {
                RangeRemoved(EventArgs.Empty);
            }
        }

        public void RemoveRange(List<T> collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                Remove(collection[i]);
            }
        }

        public void SuppressEvents()
        {
            EventsSuppressed = true;
        }

        public void ResumeEvents()
        {
            EventsSuppressed = false;
        }

        protected virtual void Cleared(EventArgs e)
        {
            if (EventsSuppressed) { return; }

            if (OnCleared != null)
            {
                OnCleared(this, e);
            }
            CollectionModified(e);
        }

        protected virtual void CollectionModified(EventArgs e)
        {
            if (EventsSuppressed) { return; }

            if (OnCollectionModified != null)
            {
                OnCollectionModified(this, e);
            }
        }

        protected virtual void ItemAdded(ListItemEventArgs e)
        {
            if (EventsSuppressed) { return; }

            if (OnItemAdded != null)
            {
                OnItemAdded(this, e);
            }
            CollectionModified(e);
        }

        protected virtual void ItemModified(ListItemEventArgs e)
        {
            if (EventsSuppressed) { return; }

            if (OnItemModified != null)
            {
                OnItemModified(this, e);
            }
            CollectionModified(e);
        }

        protected virtual void ItemRemoved(EventArgs e)
        {
            if (EventsSuppressed) { return; }

            if (OnItemRemoved != null)
            {
                OnItemRemoved(this, e);
            }
            CollectionModified(e);
        }

        protected virtual void RangeAdded(ListRangeEventArgs e)
        {
            if (EventsSuppressed) { return; }

            if (OnRangeAdded != null)
            {
                OnRangeAdded(this, e);
            }
            CollectionModified(e);
        }

        protected virtual void RangeRemoved(EventArgs e)
        {
            if (EventsSuppressed) { return; }

            if (OnRangeRemoved != null)
            {
                OnRangeRemoved(this, e);
            }
            CollectionModified(e);
        }
    }

    [Serializable]
    public class ListItemEventArgs : EventArgs
    {
    	public ListItemEventArgs(int itemIndex)
        {
            this.ItemIndex = itemIndex;
        }

    	public int ItemIndex { get; private set; }
    }

    [Serializable]
    public class ListRangeEventArgs : EventArgs
    {
    	public ListRangeEventArgs(int startIndex, int count)
        {
            this.StartIndex = startIndex;
            this.Count = count;
        }

    	public int StartIndex { get; private set; }

    	public int Count { get; private set; }
    }
}
