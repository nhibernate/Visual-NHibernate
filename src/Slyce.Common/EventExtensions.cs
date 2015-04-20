using System;
using System.ComponentModel;

namespace Slyce.Common.EventExtensions
{
	public static class EventExtensions
	{
		public static void RaiseEvent(this EventHandler @event, object sender, EventArgs e)
		{
			if (@event != null)
				@event(sender, e);
		}

		public static void RaiseEvent(this EventHandler @event, object sender)
		{
			@event.RaiseEvent(sender, null);
		}

		public static void RaiseEventEx(this EventHandler @event, IEventSender sender)
		{
			if(sender.EventRaisingDisabled)
				return;
			@event.RaiseEvent(sender, null);
		}

		public static void RaiseEventEx(this PropertyChangedEventHandler @event, IEventSender sender, string propertyName)
		{
			if (sender.EventRaisingDisabled)
				return;
			if (@event != null)
				@event(sender, new PropertyChangedEventArgs(propertyName));
		}

		public static void RaiseEventEx<T>(this EventHandler<T> @event, IEventSender sender, T e)
			where T : EventArgs
		{
			if (sender.EventRaisingDisabled)
				return;
			@event.RaiseEvent(sender, e);
		}

		public static void RaiseEvent<T>(this EventHandler<T> @event, object sender, T e)
			where T : EventArgs
		{
			if (@event != null)
				@event(sender, e);
		}

		public static void RaiseEvent(this PropertyChangedEventHandler @event, object sender, string propertyName)
		{
			if (@event != null)
				@event(sender, new PropertyChangedEventArgs(propertyName));
		}

		public static void RaiseAdditionEventEx<T>(this CollectionChangeHandler<T> @event, IEventSender sender, T obj)
		{
			if (sender.EventRaisingDisabled)
				return;
			@event.RaiseAdditionEvent(sender, obj);
		}

		public static void RaiseAdditionEvent<T>(this CollectionChangeHandler<T> @event, object sender, T obj)
		{
			if (@event != null)
				@event(sender, new CollectionChangeEvent<T>(CollectionChangeAction.Addition, obj));
		}

		public static void RaiseDeletionEventEx<T>(this CollectionChangeHandler<T> @event, IEventSender sender, T obj)
		{
			if (sender.EventRaisingDisabled)
				return;
			@event.RaiseDeletionEvent(sender, obj);
		}

		public static void RaiseDeletionEvent<T>(this CollectionChangeHandler<T> @event, object sender, T obj)
		{
			if (@event != null)
				@event(sender, new CollectionChangeEvent<T>(CollectionChangeAction.Deletion, obj));
		}
	}

	/// <summary>
	/// Use this class to wrap code blocks up that shouldn't raise events.
	/// </summary>
	/// <example>
	/// using(new EventDisabler(this)) { tbName.Text = "asd"; }
	/// </example>
	public class EventDisabler : IDisposable
	{
		private readonly IEventSender sender;

		public EventDisabler(IEventSender sender)
		{
			this.sender = sender;
			sender.EventRaisingDisabled = true;
		}

		public void Dispose()
		{
			sender.EventRaisingDisabled = false;
		}
	}

	public interface IEventSender
	{
		bool EventRaisingDisabled { get; set; }
	}
}
