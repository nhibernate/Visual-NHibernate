using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ArchAngel.Common
{
	[DotfuscatorDoNotRename]
	public interface ITaskProgressHelper<T>
	{
		/// <summary>
		/// Returns true if the task has a pending cancellation. Upon noticing this, the task should
		/// return.
		/// </summary>
		/// <returns>True if the task has a pending cancellation.</returns>
		[DotfuscatorDoNotRename]
		bool IsCancellationPending();
		/// <summary>
		/// Cancels the task.
		/// </summary>
		[DotfuscatorDoNotRename]
		void Cancel();
		/// <summary>
		/// Reports the progress of the task, passing back the percentage complete and an
		/// object describing the state of the task.
		/// </summary>
		/// <param name="percentageComplete">Integer between 0-100, describes how finished the task is.</param>
		/// <param name="userState">The state of the task.</param>
		[DotfuscatorDoNotRename]
		void ReportProgress(int percentageComplete, T userState);
		/// <summary>
		/// Gets the last state object reported.
		/// </summary>\
		[DotfuscatorDoNotRename]
		T LastProgressObject { get; }
	}

	[DotfuscatorDoNotRename]
	public class NullTaskProgressHelper<T> : ITaskProgressHelper<T>
	{
		private bool isCancellationPending;

		public bool IsCancellationPending()
		{
			return isCancellationPending;
		}

		public void Cancel()
		{
			isCancellationPending = true;
		}

		public void ReportProgress(int percentageComplete, T userState)
		{
		}

		public T LastProgressObject
		{
			get { return default(T); }
		}
	}

	[DotfuscatorDoNotRename]
	public class QueueingTaskProgressHelper<T> : ITaskProgressHelper<T> where T : class
	{
		private readonly Queue<ProgressMessage<T>> queuedProgressMessages = new Queue<ProgressMessage<T>>();
		private ProgressMessage<T> lastMessage;
		private readonly BackgroundWorker worker;
		private readonly object lockObj = new object();

		public QueueingTaskProgressHelper(BackgroundWorker worker)
		{
			this.worker = worker;
		}

		[DotfuscatorDoNotRename]
		public bool IsCancellationPending()
		{
			return worker.CancellationPending;
		}

		public void Cancel()
		{
			worker.CancelAsync();
		}

		public void ReportProgress(int percentageComplete, T userState)
		{
			lock (lockObj)
			{
				ProgressMessage<T> item = new ProgressMessage<T>(percentageComplete, userState);

				lastMessage = item;
				queuedProgressMessages.Enqueue(item);

				Application.DoEvents();
			}
		}

		public T LastProgressObject
		{
			get
			{
				lock(lockObj)
				{
					return lastMessage == null ? null : lastMessage.UserState;
				}
			}
		}

		public T GetLastProgressObjectAndClearQueue()
		{
			lock(lockObj)
			{
				var retVal = lastMessage == null ? null : lastMessage.UserState;

				ClearQueue();
				return retVal;
			}
		}

		public IEnumerable<ProgressMessage<T>> DequeueAllItems()
		{
			lock(lockObj)
			{
				while (queuedProgressMessages.Count > 0)
				{
					ProgressMessage<T> item = queuedProgressMessages.Dequeue();
					yield return item;					
				}

				ClearQueue();
			}
		}

		public void ClearQueue()
		{
			lock(lockObj)
			{
				queuedProgressMessages.Clear();
				lastMessage = null;
			}
		}
	}

	public class ProgressMessage<T>
	{
		public ProgressMessage(int percentageComplete, T userState)
		{
			PercentageComplete = percentageComplete;
			UserState = userState;
		}

		public T UserState { get; private set; }
		public int PercentageComplete { get; private set; }
	}

	/// <summary>
	/// Encapsulates progress reporting and task cancellation.
	/// </summary>
	[DotfuscatorDoNotRename]
	public class TaskProgressHelper : ITaskProgressHelper<object>
	{
		private readonly BackgroundWorker worker;
		private readonly CancelEventArgs e;
		private object lastProgressObject;

		/// <summary>
		/// Gets a Task Helper that does nothing at all.
		/// </summary>
		public static TaskProgressHelper NullHelper = new NullTaskProgressHelper();

		private class NullTaskProgressHelper : TaskProgressHelper
		{
			[DotfuscatorDoNotRename]
			public override bool IsCancellationPending()
			{
				return false;
			}

			public override void Cancel()
			{
				return;
			}

			public override void ReportProgress(int percentageComplete, object userState)
			{
				return;
			}

			public override object LastProgressObject
			{
				get { return new object(); }
			}
		}

		protected TaskProgressHelper()
		{
		}

		/// <summary>
		/// Constructs a TaskProgressHelper, uses the given worker and event args to handle progress reporting
		/// and cancellation.
		/// </summary>
		/// <param name="worker">The BacgroundWorker executing the task.</param>
		/// <param name="e">The CancelEventArgs passed to the worker when it was started.</param>
		public TaskProgressHelper(BackgroundWorker worker, CancelEventArgs e)
		{
			this.worker = worker;
			this.e = e;
		}

		/// <summary>
		/// Returns true if the task has a pending cancelation. Upon noticing this, the task should
		/// return.
		/// </summary>
		/// <returns></returns>
		[DotfuscatorDoNotRename]
		public virtual bool IsCancellationPending()
		{
			return worker.CancellationPending;
		}

		/// <summary>
		/// Cancels the task.
		/// </summary>
		[DotfuscatorDoNotRename]
		public virtual void Cancel()
		{
			e.Cancel = true;
		}

		/// <summary>
		/// Reports the progress of the task, passing back the percentage complete and an
		/// object describing the state of the task.
		/// </summary>
		/// <param name="percentageComplete">Integer between 0-100, describes how finished the task is.</param>
		/// <param name="userState">The state of the task.</param>
		[DotfuscatorDoNotRename]
		public virtual void ReportProgress(int percentageComplete, object userState)
		{
			if (worker.WorkerReportsProgress)
				worker.ReportProgress(percentageComplete, userState);
			lastProgressObject = userState;
		}

		/// <summary>
		/// Gets the last state object reported.
		/// </summary>
		[DotfuscatorDoNotRename]
		public virtual object LastProgressObject
		{
			get
			{
				return lastProgressObject;
			}
		}
	}

	[DotfuscatorDoNotRename]
	public interface IAnalysisProgressHelper : ITaskProgressHelper<object>
	{
		void AddItemToQueue(ProjectFileTreeNode node);
		void AddItemRangeToQueue(List<ProjectFileTreeNode> nodes);
		void ShiftToFrontOfQueue(ProjectFileTreeNode node);

		/// <summary>
		/// Removes and returns the object at the start of the queue.
		/// </summary>
		/// <returns>The object at the front of the queue, or null.</returns>
		ProjectFileTreeNode Dequeue();

		bool HasItemsLeftToProcess();

		int Count { get; }
	}

	///<summary>
	/// Helper object that abstracts the process of queueing items to process.
	///</summary>
	public class AnalysisProgressHelper : TaskProgressHelper, IAnalysisProgressHelper
	{
		private readonly List<ProjectFileTreeNode> nodesToProcess = new List<ProjectFileTreeNode>();

		protected AnalysisProgressHelper()
		{
		}

		/// <see cref="TaskProgressHelper"/>
		public AnalysisProgressHelper(BackgroundWorker worker, CancelEventArgs e)
			: base(worker, e)
		{
		}

		[DotfuscatorDoNotRename]
		public virtual void AddItemToQueue(ProjectFileTreeNode node)
		{
			lock (nodesToProcess)
			{
				nodesToProcess.Add(node);
			}
		}

		[DotfuscatorDoNotRename]
		public virtual void AddItemRangeToQueue(List<ProjectFileTreeNode> nodes)
		{
			lock (nodesToProcess)
			{
				nodesToProcess.AddRange(nodes);
			}
		}

		[DotfuscatorDoNotRename]
		public virtual void ShiftToFrontOfQueue(ProjectFileTreeNode node)
		{
			lock (nodesToProcess)
			{
				if (nodesToProcess.Contains(node))
				{
					nodesToProcess.Remove(node);
					nodesToProcess.Insert(0, node);
				}
			}
		}

		/// <summary>
		/// Removes and returns the object at the start of the queue.
		/// </summary>
		/// <returns>The object at the front of the queue, or null.</returns>
		[DotfuscatorDoNotRename]
		public virtual ProjectFileTreeNode Dequeue()
		{
			lock (nodesToProcess)
			{
				if (nodesToProcess.Count != 0)
				{
					ProjectFileTreeNode node = nodesToProcess[0];
					nodesToProcess.RemoveAt(0);
					return node;
				}
			}
			return null;
		}

		[DotfuscatorDoNotRename]
		public virtual bool HasItemsLeftToProcess()
		{
			lock (nodesToProcess)
			{
				return nodesToProcess.Count > 0;
			}
		}

		[DotfuscatorDoNotRename]
		public virtual int Count
		{
			get
			{
				lock (nodesToProcess)
				{
					return nodesToProcess.Count;
				}
			}
		}
	}

	public class GenerateFilesProgress
	{
		public int NumberOfFilesGenerated { get; private set; }
		public bool FatalErrorOccurred { get; private set; }
		public Exception Exception { get; private set; }

		public GenerateFilesProgress(int numberOfFilesGenerated)
		{
			NumberOfFilesGenerated = numberOfFilesGenerated;
		}

		public GenerateFilesProgress(int numberOfFilesGenerated, Exception e) : this(numberOfFilesGenerated)
		{
			Exception = e;
			FatalErrorOccurred = true;
		}
	}

	public class AnalyseFilesProgress
	{
		public int NumberOfFilesLeftToAnalyse;
		public int NumberOfExactCopies;
		public int NumberOfResolved;
		public int NumberOfConflicts;
		public int NumberOfErrors;
		public int NumberOfNewFiles;
		public string ChangedFilePath;
		public ProjectFileStatusEnum ChangedFileStatus;

		public AnalyseFilesProgress(int numberOfFilesLeftToAnalyse, int numExactCopies, int numResolved, int numConflicts, int numErrors, int numNewFiles) : this(numberOfFilesLeftToAnalyse, numExactCopies, numResolved, numConflicts, numErrors, numNewFiles, null, ProjectFileStatusEnum.UnAnalysedFile)
		{
		}

		public AnalyseFilesProgress(int numberOfFilesLeftToAnalyse, int numExactCopies, int numResolved, int numConflicts, int numErrors, int numNewFiles, string changedFilePath, ProjectFileStatusEnum changedStatus)
		{
			NumberOfFilesLeftToAnalyse = numberOfFilesLeftToAnalyse;
			NumberOfExactCopies = numExactCopies;
			NumberOfResolved = numResolved;
			NumberOfConflicts = numConflicts;
			NumberOfErrors = numErrors;
			NumberOfNewFiles = numNewFiles;
			ChangedFilePath = changedFilePath;
			ChangedFileStatus = changedStatus;
		}
	}
}