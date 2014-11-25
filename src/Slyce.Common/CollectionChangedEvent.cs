using System;

namespace Slyce.Common
{
	public delegate void CollectionChangeHandler<T>(object sender, CollectionChangeEvent<T> args);

	public class CollectionChangeEvent<T> : EventArgs
	{
		public readonly CollectionChangeAction ChangeType;
		public readonly T ChangedObject;

		public CollectionChangeEvent(CollectionChangeAction changeType, T changedObject)
		{
			ChangeType = changeType;
			ChangedObject = changedObject;
		}
	}

	public enum CollectionChangeAction
	{
		Addition, Deletion
	}
}
