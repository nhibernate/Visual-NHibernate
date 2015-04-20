using System;
using System.Threading;

namespace ArchAngel.Debugger.Common
{
	/// <summary>
	/// Class that provides a callback mechanism for the Debug Process
	/// to inform the main ArchAngel process that it has started, and
	/// the URI at which it can be accessed.
	/// </summary>
	[Serializable]
	public class DebugProcessInfoService : MarshalByRefObject
	{
		[NonSerialized]
		private static AutoResetEvent _StartedWaitHandle = new AutoResetEvent(false);
		private static string _Uri = "";

		public DebugProcessInfoService()
		{
		}

		/// <summary>
		/// The debug process should call this method when it has started.
		/// </summary>
		/// <param name="uri">The URI at which the debug process's CommandReceiver instance
		/// can be accessed.</param>
		public void Started(string uri)
		{
			_Uri = uri;
			_StartedWaitHandle.Set();
		}

		/// <summary>
		/// The wait handle that will be signalled when the Debug Process has started.
		/// </summary>
		public static WaitHandle StartedWaitHandle
		{
			get { return _StartedWaitHandle; }
		}

		/// <summary>
		/// The Uri at which the Debug Process has registered its CommandReceiver.
		/// Will be an empty string until the StartedWaitHandle has been signalled.
		/// </summary>
		public static string Uri
		{
			get { return _Uri; }
		}
	}
}
