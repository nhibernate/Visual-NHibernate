using System;
using System.Diagnostics;
using System.Text;

namespace Slyce.Common
{
    /// <summary>
    /// Class for logging trace calls, and keeping track of calls.
    /// </summary>
    public class ObservableTraceListener : TraceListener
    {
        private readonly StringBuilder backingStore = new StringBuilder(10000);
        private readonly int maximumSize = 10000;
		private bool limitSize = true;

        private readonly object lockObj = new object();

        /// <summary>
        /// The maximum number of characters stored by the TraceListener.
        /// </summary>
        public int MaximumCapacity
        {
            get
            {
                return maximumSize;
            }
        }

    	public bool LimitSize
    	{
    		get { return limitSize; }
    		set
    		{
				lock(lockObj)
				{
					limitSize = value;
				}
    		}
    	}

    	/// <summary>
        /// Fired when the TraceListener recieves a message.
        /// </summary>
        public event EventHandler<TraceEventArgs> TraceUpdated;

        /// <summary>
        /// Creates a TraceListener with the default maximum size of 10000 characters.
        /// </summary>
        public ObservableTraceListener()
        {
        }

        /// <summary>
        /// Creates a TraceListener with the default maximum size of 10000 characters, with the
        /// given name.
        /// </summary>
        /// ///<param name="name">The name of the System.Diagnostics.TraceListener.</param>
        public ObservableTraceListener(string name) : base(name)
        {
        }

        ///<summary>
        /// Creates a TraceListener with the given maximum size and name.
        ///</summary>
        ///<param name="maxCapacity">The maximum number of characters this listener will hold.</param>
        public ObservableTraceListener(int maxCapacity)
        {
            maximumSize = maxCapacity;
        }

        ///<summary>
        /// Creates a TraceListener with the given maximum size and name.
        ///</summary>
        ///<param name="name">The name of the System.Diagnostics.TraceListener.</param>
        ///<param name="maxCapacity">The maximum number of characters this listener will hold.</param>
        public ObservableTraceListener(string name, int maxCapacity) : base(name)
        {
            maximumSize = maxCapacity;
        }

        public override bool IsThreadSafe
        {
            get { return true; }
        }

        public override void Write(string message)
        {
			if (TraceLogger.Enabled == false)
				return;

            lock (lockObj)
            {
                backingStore.Append(message);
                if (limitSize && backingStore.Length > maximumSize)
                {
                    backingStore.Remove(0, backingStore.Length - maximumSize);
                }
                if (TraceUpdated != null)
                    TraceUpdated(this, new TraceEventArgs(message));
            }
        }

        public override void WriteLine(string message)
        {
			if (TraceLogger.Enabled == false)
				return;

            lock (lockObj)
            {
                backingStore.AppendLine(message);
                if (limitSize && backingStore.Length > maximumSize)
                {
                    backingStore.Remove(0, backingStore.Length - maximumSize);
                }
                if (TraceUpdated != null)
                    TraceUpdated(this, new TraceEventArgs(message + Environment.NewLine));
            }
        }

        public override string ToString()
        {
            lock (lockObj)
            {
                return backingStore.ToString();
            }
        }
    }

    /// <summary>
    /// Contains the new text that was added.
    /// </summary>
    public class TraceEventArgs : EventArgs
    {
        public readonly string NewText;

        public TraceEventArgs(string message)
        {
            NewText = message;
        }
    }

	public static class TraceLogger
	{
		private static bool enabled;

		public static bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}

		public static void TraceInformation(string message)
		{
			if (enabled)
				Trace.TraceInformation(message);
		}

		public static void TraceError(string message)
		{
			if (enabled)
				Trace.TraceError(message);
		}

		public static void TraceWarning(string message)
		{
			if (enabled)
				Trace.TraceWarning(message);
		}
	}
}
