using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Threading;
using System.Windows.Forms;
using ArchAngel.Debugger.Common;
using ArchAngel.Debugger.DebugProcess;
using ArchAngel.Interfaces;
using Microsoft.Samples.Debugging.CorDebug;
using Microsoft.Samples.Debugging.MdbgEngine;

namespace ArchAngel.Debugger
{
	/// <summary>
	/// Instances of this class are used to synchronise a Debugger with the GUI that is
	/// controlling it.
	/// </summary>
	public class DebuggerSyncConstruct
	{
		/// <summary>The next action the debugger should execute.</summary>
		/// <seealso cref="DebugActionType"/>
		private DebugActionType _NextDebugAction = DebugActionType.Continue;
		/// <summary>
		/// The wait handle that the debugger will wait on before continuing the execution of
		/// the debug session.
		/// </summary>
		private readonly EventWaitHandle _ContinueDebugExecution = new AutoResetEvent(false);
		/// <summary>The BackgroundWorker that is running the Debugger.</summary>
		private readonly BackgroundWorker _BackgroundWorker;

		/// <summary>Constructs a DebuggerSyncConstruct using the given BackgroundWorker.</summary>
		/// <param name="backgroundWorker">The BackgroundWorker that will run the Debugger.</param>
		public DebuggerSyncConstruct(BackgroundWorker backgroundWorker)
		{
			_BackgroundWorker = backgroundWorker;
		}

		/// <summary>The BackgroundWorker that is running the Debugger.</summary>
		public BackgroundWorker DebugBackgroundWorker
		{
			get { return _BackgroundWorker; }
		}

		/// <summary>
		/// The wait handle that the debugger will wait on before continuing the execution of
		/// the debug session.
		/// </summary>
		public EventWaitHandle ContinueDebugExecution
		{
			get { return _ContinueDebugExecution; }
		}

		/// <summary>The next action the debugger should execute.</summary>
		/// <seealso cref="DebugActionType"/>
		public DebugActionType NextDebugAction
		{
			get { return _NextDebugAction; }
			set { _NextDebugAction = value; }
		}
	}

	/// <summary>
	/// Enum representing the different ways the Debugger can continue to the next
	/// action.
	/// </summary>
	public enum DebugActionType
	{
		/// <summary>
		/// The Debugger will run the process to the next breakpoint.
		/// </summary>
		Continue,
		/// <summary>
		/// The Debugger will move the current execution point to the start of the next
		/// function call.
		/// </summary>
		StepInto,
		/// <summary>
		/// The Debugger will break on the next line of source code.
		/// </summary>
		StepOver,
		/// <summary>
		/// The Debugger will detach from the process being debugged.
		/// </summary>
		Stop,
		/// <summary>
		/// The Debugger will step up the stack one level.
		/// </summary>
		StepOut
	}

	/// <summary>
	/// Enum representing the different reason for the Debugger stopping execution.
	/// </summary>
	public enum StopReason
	{
		/// <summary>
		/// A breakpoint was hit.
		/// </summary>
		BreakpointHit,
		/// <summary>
		/// A step into or step over action was completed.
		/// </summary>
		StepComplete,
		/// <summary>
		/// An exception occurred in the code being debugged.
		/// </summary>
		ExceptionOccurred,
		/// <summary>
		/// The debugger started up.
		/// </summary>
		DebuggerStarted,
		/// <summary>
		/// The debugger has reached LAST_LINE_IN_MAIN, and has detached.
		/// </summary>
		DebuggerFinished
	}

	/// <summary>This structure holds information about the current state of the Debugger.</summary>
	public struct DebugInformation
	{
		/// <summary>The line number that the Debugger is stopped on. Numbering starts at 1</summary>
		public int StartLineNumber;
		/// <summary>
		/// The column index of the piece of code that the Debugger is stopped on. Numbering starts at 0.
		/// </summary>
		public int StartColumnNumber;
		/// <summary>
		/// The last line that the currently executing piece of code is on. Numbering starts at 1.
		/// </summary>
		public int EndLineNumber;
		/// <summary>
		/// The final column index of the piece of code that the Debugger is stopped on. Numbering starts at 0.
		/// </summary>
		public int EndColumnNumber;
		/// <summary>The name of the file that contains the current breakpoint.</summary>
		public string SourceFile;
		/// <summary>
		/// A boolean representing whether the Debugger actually stopped and needs signalling
		/// to continue, or whether it is just a status update.
		/// </summary>
		public bool Stopped;
		/// <summary>The current output of the template function being debugged.</summary>
		public string CurrentOutput;
		/// <summary>
		/// If the debugger is stopped, this gives the reason why.
		/// </summary>
		public StopReason StopReason;
		/// <summary>
		/// A string containing the reason the debugger stopped in a useful form. Could be
		/// passed straight to the UI.
		/// </summary>
		public string StopReasonText;
		/// <summary>
		/// Boolean values representing whether an exception was the cause of this
		/// break.
		/// </summary>
		public bool ExceptionThrown;
		/// <summary>
		/// Contains information about the Exception that was thrown, if there was
		/// one.
		/// </summary>
		/// <seealso cref="ExceptionInformation">ExceptionInformation Class</seealso>
		public ExceptionInformation ExceptionInformation;
		/// <summary>
		/// Contains information about the current position in the stack.
		/// </summary>
		public StackInformation StackInformation;

		private IEnumerable<LocalVariableInformation> localVariableInformation;
		/// <summary>
		/// Contains information about each local variable in scope at the current breakpoint.
		/// Will not ever be null, just empty.
		/// </summary>
		public IEnumerable<LocalVariableInformation> LocalVariableInformation
		{
			get
			{
				return localVariableInformation ?? new List<LocalVariableInformation>();
			}
			set
			{
				localVariableInformation = value;
			}
		}
	}
	/// <summary>
	/// Contains information about the current position in the stack.
	/// </summary>
	public class StackInformation
	{
		/// <summary>
		/// The depth of the current stack frame.
		/// </summary>
		public int StackFrameDepth;
	}

	public enum VariableType { Local, Parameter, Field, Property }

	/// <summary>
	/// This class holds information about the state of local variables at the current
	/// breakpoint. It currently has Name and String Value.
	/// </summary>
	public class LocalVariableInformation
	{
		public string TypeName { get; set; }
		public string Name { get; set; }
		public string StringValue { get; set; }
		public VariableType TypeOfVariable { get; set; }
		public bool IsPrimitive { get; set; }
		public List<LocalVariableInformation> Fields { get; set; }
	}

	/// <summary>
	/// This class is used to hold information about any exceptions inspected by the
	/// debugger. It includes inner exception information.
	/// </summary>
	public class ExceptionInformation
	{
		/// <summary>The Message property of the Exception.</summary>
		public string Message;

		/// <summary>
		/// An array of strings, each containing information about a frame in the call stack.
		/// The first item in the array corresponds to the current stack frame.
		/// </summary>
		public string[] StackTrace = new string[0];
		/// <summary>A string containing the output of the exception's StackTrace property.</summary>
		public string StackTraceString;
		/// <summary>
		/// Information about the exception's inner exception. If there isn't an inner
		/// exception, this field is null.
		/// </summary>
		public ExceptionInformation InnerExceptionInfo;
	}

	/// <summary>
	/// This class manages the Debugger process. It is used to
	/// run and control the debug process, and uses the DebuggerSyncConstruct
	/// that it is constructed with to communicate with the UI element
	/// that uses it. The debug process is started by calling the Run method.
	/// Once the debugger is started, it cannot be started again until the
	/// debug run has ended.
	/// </summary>
	public class Debugger
	{
		/// <summary>
		/// Constant value that represents the last line number the debugger should execute. Because
		/// the Debugger attaches to the running process, it needs to know when to detach. If there
		/// is no #line directive that correcsponds to this line number, then the debugger will
		/// lock up and not finish until the ArchAngel Debug process exits. It is defined as
		/// 0xfeefee - 1, because #line hidden maps to #line 0xfeefee. We decided that if Microsoft
		/// has decided no one would use C# files that are 16,707,566 lines long, then 16,707,564
		/// would be a fine number for us to use. I believe that there are likely to be other
		/// issues that crop up due to 16 million line source files before this becomes an issue
		/// for us.
		/// </summary>
		public const int LAST_LINE_IN_MAIN = 16707564;
		/// <summary>
		/// Constant value that represents the last line number in a function.
		/// </summary>
		/// <see cref="LAST_LINE_IN_MAIN"/>
		public const int LAST_LINE_IN_FUNCTION = 16707563;
		/// <summary>
		/// The DebuggerSyncConstruct that the Debugger uses to communicate with the GUI
		/// thread that spawned it.
		/// </summary>
		/// <seealso cref="DebuggerSyncConstruct">DebuggerSyncConstruct Class</seealso>
		private readonly DebuggerSyncConstruct _DebuggerSync;

		/// <summary>
		/// The locations of the Assemblies that must be loaded in order to run the template
		/// function being debugged.
		/// </summary>
		private List<string> _AssemblyLocations = new List<string>();

		/// <summary>
		/// The list of lines to put breakpoints on.
		/// </summary>
		private readonly List<int> _Breakpoints = new List<int>();

		/// <summary>
		/// The currently running MDbgProcess, if there is one.
		/// </summary>
		private MDbgProcess _CurrentlyRunningProcess;

		/// <summary>
		/// The breakpoints currently set on the debugger. The key is the line number
		/// the breakpoint was set on. This is needed because MDbg doesn't provide an
		/// easy way to remove breakpoints. There is no method for doing this, instead
		/// you need to call breakpoint.Delete(). You need the MDbgBreakpoint object to
		/// do this, and the only way to get that is to look it up via its index, which
		/// we don't have.
		/// </summary>
		private readonly Dictionary<int, MDbgBreakpoint> _CurrentlyRunningBreakpoints = new Dictionary<int, MDbgBreakpoint>();

		/// <summary>
		/// The MDbgProcess that we are debugging.
		/// </summary>
		private MDbgProcess proc;

		/// <summary>
		/// The file path of the template function source code we are debugging. This is the
		/// large file with all of the generated template functions.
		/// </summary>
		public string CodeFilename { get; set; }

		/// <summary>The namespace which contains the template function we are debugging.</summary>
		public string TestNamespace { get; set; }

		/// <summary>
		/// The name of the class that contains the template function we are
		/// debugging.
		/// </summary>
		public string TestClassname { get; set; }

		/// <summary>The name of the template function we are debugging.</summary>
		public string TestFunctionName { get; set; }

		/// <summary>
		/// The XML from the AAPROJ file to be used by the debugger.
		/// </summary>
		public string AaprojXml { get; set; }

		/// <summary>
		/// The ArgumentList that contains the arguments to be passed to the debugger for
		/// running the current function.
		/// </summary>
		public ArgumentList ArgumentList { get; set; }

		/// <summary>
		/// A list of UserOptionValues which will be set just before running the template
		/// function.
		/// </summary>
		public List<UserOptionValues> UserOptions { get; set; }

		///<summary>
		/// The location of the assembly the debugger should run.
		///</summary>
		public string CompiledAssemblyLocation { get; set; }

		/// <summary>
		/// Assemblies 
		/// </summary>
		public List<string> AssemblyLocations
		{
			get { return _AssemblyLocations; }
			private set
			{
				List<string> assemblyLocations = new List<string>(value);
				_AssemblyLocations = Slyce.Common.VersionNumber.GetLocationsWithLatestVersions(assemblyLocations);
			}
		}

		public bool IsFunctionStatic
		{
			get;
			set;
		}

		/// <summary>
		/// Constructs a Debugger instance that uses the specified DebuggerSyncConstruct to 
		/// communicate with its controller, and preloads the ArchAngel Debug process with
		/// the assemblies and ArchAngel project specified.
		/// </summary>
		/// <param name="debuggerSync">The DebuggerSyncConstruct object used to synchronise the
		/// Debugger instance with its controller.</param>
		/// <param name="archAngelProjectFilename">The filename of the ArchAngel Project (*.aaproj)
		/// that will be debugged.</param>
		/// <param name="assembliesToLoad">A list of assemblies the template function relies on to run.</param>
		public Debugger(DebuggerSyncConstruct debuggerSync, string archAngelProjectFilename, IEnumerable<string> assembliesToLoad)
		{
			if (debuggerSync == null)
			{
				throw new ArgumentNullException("debuggerSync");
			}

			TestFunctionName = "Main";
			TestClassname = "Test";
			TestNamespace = "Test";
			CodeFilename = "Test.cs";

			ArgumentList = new ArgumentList(new object[] { new string[0] });

			_DebuggerSync = debuggerSync;
			DebugProcess.StartDebugProcess();

			AssemblyLocations = new List<string>(assembliesToLoad);

			DebugProcess.LoadCommandReceiver(AssemblyLocations, archAngelProjectFilename);
		}

		/// <summary>
		/// Spawns a new thread to start up the Debug Process, if it has not already been started.
		/// </summary>
		public static void SpinUpDebugProcess()
		{
			Thread thread = new Thread(DebugProcess.StartDebugProcess);
			thread.Start();
		}

		/// <summary>
		/// Spawns a new thread to kill the Debug Process.
		/// </summary>
		public static void KillDebugProcess()
		{
			Thread thread = new Thread(DebugProcess.EndDebugProcess);
			thread.Start();
		}

		///<summary>
		/// Stops and Starts the DebugProcess, freeing up any old assemblies it was holding onto
		/// but forcing a reload of the template's data.
		///</summary>
		public static void RestartDebugProcess()
		{
			Thread thread = new Thread(new ThreadStart(
				delegate
				{
					DebugProcess.EndDebugProcess();
					DebugProcess.StartDebugProcess();
				}));
			thread.Start();
		}

		///<summary>
		/// Attempts to stop the debugger and the debug process.
		///</summary>
		public void StopDebugger()
		{
			if (proc == null) return;

			try
			{
				proc.Kill();
			}
			catch (NullReferenceException)
			{
				// If this is thrown, the process was exiting anyway.
			}
		}

		/// <summary>
		/// Adds a breakpoint to the list of breakpoints.
		/// </summary>
		/// <param name="lineNumber"></param>
		public void AddBreakpoint(int lineNumber)
		{
			_Breakpoints.Add(lineNumber);
			if (_CurrentlyRunningProcess != null)
			{
				_CurrentlyRunningBreakpoints.Add(lineNumber,
					_CurrentlyRunningProcess.Breakpoints.CreateBreakpoint(CodeFilename, lineNumber));
			}
		}

		/// <summary>
		/// Clears all breakpoints.
		/// </summary>
		public void ClearBreakpoints()
		{
			_Breakpoints.Clear();
			if (_CurrentlyRunningProcess != null)
			{
				_CurrentlyRunningBreakpoints.Clear();
				_CurrentlyRunningProcess.Breakpoints.DeleteAll();
			}
		}

		/// <summary>
		/// Removes the breakpoint from the specified line. If there is
		/// no breakpoint on this line it will return false;
		/// </summary>
		/// <param name="lineNumber"></param>
		public bool RemoveBreakpoint(int lineNumber)
		{
			_Breakpoints.Remove(lineNumber);
			if (_CurrentlyRunningProcess != null)
			{
				if (_CurrentlyRunningBreakpoints.ContainsKey(lineNumber))
				{
					_CurrentlyRunningBreakpoints[lineNumber].Delete();
					_CurrentlyRunningBreakpoints.Remove(lineNumber);
				}
				else
					return false;
			}
			return true;
		}

		/// <summary>Starts the debugging process. This is synchronised on the Debugger
		/// object it is called on.</summary>
		/// <remarks>
		/// Assumes that the DebuggerSync object has been set up correctly and that something
		/// is listening to the BackgroundWorker events. It will lock indefinitely if something
		/// doesn't tell it to continue by calling set on the DebuggerSync.ContinueDebugExecution
		/// object.
		/// </remarks>
		public void Run()
		{
			lock (this)
			{
				int pid = DebugProcess.GetDebugProcessId();

				CommandReceiver obj = DebugProcess.GetCommandReceiver();

				// Get Assembly Search paths. These are used for finding
				// any assemblies that are used by the template.
				List<string> assemblySearchPaths = new List<string>();

				foreach (string assemblyLocation in AssemblyLocations)
				{
					string dir = Path.GetDirectoryName(assemblyLocation).ToLower();

					if (assemblySearchPaths.BinarySearch(dir) < 0)
					{
						assemblySearchPaths.Add(dir);
						assemblySearchPaths.Sort();
					}
				}
				obj.ExecuteCommand(new RunFunctionCommand(CompiledAssemblyLocation, TestNamespace, TestClassname,
														  TestFunctionName, ArgumentList, UserOptions, AaprojXml, IsFunctionStatic));

				// This is a race condition, but it is required because we can't create the breakpoints
				// before attaching to the process.
				MDbgEngine debugger = new MDbgEngine();
				proc = debugger.Attach(pid);

				_CurrentlyRunningBreakpoints.Clear();

				foreach (int bp in _Breakpoints)
				{
					_CurrentlyRunningBreakpoints.Add(bp,
													 proc.Breakpoints.CreateBreakpoint(CodeFilename, bp));
				}

				proc.Breakpoints.CreateBreakpoint("Commands.cs", LAST_LINE_IN_MAIN);
				proc.Breakpoints.CreateBreakpoint(CodeFilename, LAST_LINE_IN_FUNCTION);

				_CurrentlyRunningProcess = proc;

				while (proc.IsAlive)
				{
					// Let the debuggee run and wait until it hits a debug event.
					switch (_DebuggerSync.NextDebugAction)
					{
						case DebugActionType.Continue:
							proc.Go().WaitOne();
							break;
						case DebugActionType.StepInto:
							proc.StepInto(false).WaitOne();
							break;
						case DebugActionType.StepOver:
							proc.StepOver(false).WaitOne();
							break;
						case DebugActionType.StepOut:
							proc.StepOut().WaitOne();
							break;
						case DebugActionType.Stop:
							_CurrentlyRunningProcess = null;
							proc.Breakpoints.DeleteAll();
							proc.Detach();
							break;
					}

					if (!proc.IsAlive)
					{
						proc = null;
						break;
					}

					// Get a DebugInformation object filled with the info we need.
					DebugInformation di = GetDebugInformation(proc);

					// If this is the last line, we need to stop debugging.
					if (di.StartLineNumber == LAST_LINE_IN_MAIN)
					{
						di.Stopped = false;
						di.StopReason = StopReason.DebuggerFinished;
						di.StopReasonText = "Debugger Finished";
						_DebuggerSync.DebugBackgroundWorker.ReportProgress(99, di);
						_CurrentlyRunningProcess = null;
						proc.Breakpoints.DeleteAll();
						proc.Detach();

						break;
					}
					// Any other lines in the main function should just be skipped.
					if (di.SourceFile == "Commands.cs")
					{
						_DebuggerSync.NextDebugAction = DebugActionType.Continue;
						continue;
					}

					if (di.StartLineNumber == LAST_LINE_IN_FUNCTION)
					{
						_DebuggerSync.NextDebugAction = DebugActionType.StepOut;
						continue;
					}
					// If an exception was thrown, report it then stop debugging.
					if (di.ExceptionThrown)
					{
						di.Stopped = false;
						_DebuggerSync.DebugBackgroundWorker.ReportProgress(99, di);
						_CurrentlyRunningProcess = null;
						proc.Breakpoints.DeleteAll();
						proc.Detach();

						break;
					}

					di.Stopped = true;
					_DebuggerSync.DebugBackgroundWorker.ReportProgress(50, di);
					_DebuggerSync.ContinueDebugExecution.WaitOne();
					continue;
				}
			}
			_CurrentlyRunningProcess = null;
		}

		/// <summary>
		/// Gets the current output of the template function.
		/// </summary>
		/// <remarks>
		/// Assumes that debuggedProc can be examined safely, and that the current output is stored in a static
		/// variable called __CurrentOutput in the class containing the template function.
		/// Alternatively, if the </remarks>
		/// <param name="debuggedProc">The Process that is being debugged.</param>
		/// <returns>The current output of the template function.</returns>
		private string GetCurrentOutput(MDbgProcess debuggedProc)
		{
			MDbgValue val = null;
			int lineNumber = (debuggedProc.Threads.HaveActive && debuggedProc.Threads.Active.HaveCurrentFrame &&
							  debuggedProc.Threads.Active.CurrentFrame.SourcePosition != null) ?
								 debuggedProc.Threads.Active.CurrentFrame.SourcePosition.Line : 0;
			if (lineNumber == LAST_LINE_IN_MAIN)
			{
				val = debuggedProc.ResolveVariable("obj", debuggedProc.Threads.Active.CurrentFrame);
			}
			else if (debuggedProc.Threads.Active.CurrentFrame.SourcePosition != null)
			{
				if (debuggedProc.Threads.Active.CurrentFrame.SourcePosition.Path.Contains(Path.Combine("ArchAngel.Debugger.DebugProcess", "Commands.cs")))
				{
					return "";
				}

				val = debuggedProc.ResolveVariable("instance", debuggedProc.Threads.Active.CurrentFrame)
					  ??
					  debuggedProc.ResolveVariable("this", debuggedProc.Threads.Active.CurrentFrame);

				if (val == null)
					return "Could not get value of current output";
				string funcName = TestNamespace + "." + TestClassname + ".GetCurrentStringBuilder";
				val = ResolveFunction(debuggedProc, val, funcName);
				val = ResolveFunction(debuggedProc, val, "System.Text.StringBuilder.ToString");
			}

			if (val == null)
				return "Could not get value of current output";

			string currentOutput = val.GetStringValue(1);
			currentOutput = currentOutput.Remove(0, 1);
			currentOutput = currentOutput.Remove(currentOutput.Length - 1, 1);
			return currentOutput;
		}

		/// <summary>
		/// Returns the text associated with the passed
		/// stop reason.
		/// </summary>
		/// <param name="stopReasonText">The rext of the stop reason.</param>
		/// <param name="stopReason">The stop reason to examine</param>
		/// <param name="stopReasonObject"></param>
		/// <returns></returns>
		private void GetStopReasonInfo(object stopReasonObject, out string stopReasonText, out StopReason stopReason)
		{
			if (stopReasonObject is BreakpointHitStopReason)
			{
				stopReasonText = "Breakpoint Hit";
				stopReason = StopReason.BreakpointHit;
			}
			else if (stopReasonObject is StepCompleteStopReason)
			{
				stopReasonText = "Step Complete";
				stopReason = StopReason.StepComplete;
			}
			else if (stopReasonObject is ExceptionThrownStopReason)
			{
				stopReasonText = "Exception Thrown!";
				stopReason = StopReason.ExceptionOccurred;
			}
			else if (stopReasonObject is ExceptionUnwindStopReason)
			{
				stopReasonText = "An Exception is being unwound";
				stopReason = StopReason.ExceptionOccurred;
			}
			else if (stopReasonObject is AttachCompleteStopReason)
			{
				stopReasonText = "Debugger successfully started";
				stopReason = StopReason.DebuggerStarted;
			}
			else
			{
				stopReasonText = "Unsupported Stop Reason";
				stopReason = StopReason.ExceptionOccurred;
			}
		}

		/// <summary>Gets information about the current state of the debugger.</summary>
		/// <returns>A filled DebugInformation object. Contains exception information if available.</returns>
		/// <remarks>Assumes that the process is safe to examine.</remarks>
		/// <param name="debuggedProc">The Process to examine.</param>
		private DebugInformation GetDebugInformation(MDbgProcess debuggedProc)
		{
			DebugInformation di = new DebugInformation();

			object stopReason = debuggedProc.StopReason;

			if (debuggedProc.Threads.HaveActive && debuggedProc.Threads.Active.HaveCurrentFrame)
			{
				di.LocalVariableInformation = GetScopeVariables(debuggedProc.Threads.Active.CurrentFrame);
			}

			// AttachComplete is triggered once the debugger
			if (stopReason is AttachCompleteStopReason)
			{
				di.CurrentOutput = "";
				di.Stopped = true;
				di.StartLineNumber = 0;
				GetStopReasonInfo(stopReason, out di.StopReasonText, out di.StopReason);
				return di;
			}

			if (stopReason is ExceptionThrownStopReason || stopReason is UnhandledExceptionThrownStopReason)
			{
				ExceptionInformation ei = GetExceptionInformation(debuggedProc);

				di.ExceptionInformation = ei;
				di.ExceptionThrown = true;
			}

			GetStopReasonInfo(stopReason, out di.StopReasonText, out di.StopReason);

			if (debuggedProc.Threads.HaveActive == false || debuggedProc.Threads.Active.HaveCurrentFrame == false ||
				  debuggedProc.Threads.Active.CurrentFrame.SourcePosition == null)
			{
				di.Stopped = false;
				di.StartLineNumber = int.MinValue;
				di.EndLineNumber = int.MinValue;
				di.StartColumnNumber = int.MinValue;
				di.EndColumnNumber = int.MinValue;
			}
			else
			{
				MDbgFrame currentFrame = debuggedProc.Threads.Active.CurrentFrame;
				if (currentFrame.SourcePosition != null)
				{
					di.StartLineNumber = currentFrame.SourcePosition.Line;
					di.StartColumnNumber = currentFrame.SourcePosition.StartColumn - 1;
					di.EndColumnNumber = currentFrame.SourcePosition.EndColumn - 1;
					di.EndLineNumber = currentFrame.SourcePosition.EndLine;
				}
				else
				{
					di.StartLineNumber = 0;
					di.StartColumnNumber = 0;
					di.EndColumnNumber = 0;
					di.EndLineNumber = 0;
				}
				di.CurrentOutput = GetCurrentOutput(debuggedProc);
				if (di.CurrentOutput == null)
				{
					di.Stopped = false;
					di.StopReason = StopReason.StepComplete;
					di.StartLineNumber = int.MinValue;
					di.EndLineNumber = int.MinValue;
					di.StartColumnNumber = int.MinValue;
					di.EndColumnNumber = int.MinValue;
				}
			}



			return di;
		}

		/// <summary>
		/// Fills an ExceptionInformation object with information about the
		/// current exception in the active thread. 
		/// </summary>
		/// <param name="debuggedProc">The Process to extract exception from.</param>
		/// <returns>A filled ExceptionInformation object, or null if there is no current exception.</returns>
		private ExceptionInformation GetExceptionInformation(MDbgProcess debuggedProc)
		{
			ExceptionInformation ei = new ExceptionInformation();
			//ei.StackTrace = GetStackTrace(debuggedProc.Threads.Active);

			ExceptionInformation inner = ei;

			MDbgValue ex = debuggedProc.ResolveVariable("$exception", debuggedProc.Threads.Active.CurrentFrame);
			if (ex == null)
				return null;
			do
			{
				MDbgValue messageValue = ResolveFunction(debuggedProc, ex, "System.Exception.get_Message");
				if (messageValue != null)
				{
					inner.Message = messageValue.GetStringValue(true);
				}
				else
				{
					inner.Message = "Could not retrieve the exception message";
				}

				MDbgValue stackTrace = ResolveFunction(debuggedProc, ex, "System.Exception.get_StackTrace");
				if (stackTrace != null)
				{
					inner.StackTraceString = stackTrace.GetStringValue(true);
				}
				else
				{
					inner.StackTraceString = "Could not retrieve stack trace";
				}

				ex = ResolveFunction(debuggedProc, ex, "System.Exception.get_InnerException");
				if (ex != null && ex.IsNull != true)
				{
					inner.InnerExceptionInfo = new ExceptionInformation();
					inner = inner.InnerExceptionInfo;
				}
			} while (ex != null && ex.IsNull != true);

			return ei;
		}

		/// <summary>
		/// Gets the output of a function using MDbg.
		/// </summary>
		/// <example>
		/// 	<code lang="CS" title="Getting the Message from an Exception" description="An example showing how you would get the value of the Message property of an Exception. The variable ex is a MDbgValue object that represents the exception you are examining.">
		/// MDbgValue returnValue = ResolveFunction(debuggedProc, ex, "System.Exception.get_Message");
		/// string message = returnValue.GetStringValue(true);
		///     </code>
		/// </example>
		/// <param name="debuggedProc">The process we are debugging.</param>
		/// <param name="ex">The MDbgValue object to execute the function on.</param>
		/// <param name="function">The full name of the function in MDbg syntax.</param>
		/// <returns>The MDbgValue that contains the return value of the function. If the function
		/// could not be evaluated, it returns null.</returns>
		private MDbgValue ResolveFunction(MDbgProcess debuggedProc, MDbgValue ex, string function)
		{
			CorAppDomain appDomain = debuggedProc.Threads.Active.CorThread.AppDomain;
			MDbgFunction func = debuggedProc.ResolveFunctionNameFromScope(function, appDomain);
			if (func == null)
				throw new Exception("A required function is missing. Are you running a template compiled with an different version of ArchAngel?");
			CorEval eval = debuggedProc.Threads.Active.CorThread.CreateEval();
			eval.CallFunction(func.CorFunction, new[] { ex.CorValue });

			debuggedProc.Go().WaitOne();

			if (!(debuggedProc.StopReason is EvalCompleteStopReason))
			{
				return null;
			}

			eval = ((EvalCompleteStopReason)debuggedProc.StopReason).Eval;
			if (eval == null)
			{
				return null;
			}

			CorValue cv = eval.Result;
			if (cv != null)
			{
				MDbgValue mv = new MDbgValue(debuggedProc, cv);
				return mv;
			}

			return null;
		}

		/// <summary>
		/// Gets the variables available for inspection in the supplied MDbgFrame.
		/// Currently does nothing with those variables. Left here for future use.
		/// </summary>
		/// <param name="frame">The frame to get the local variables from.</param>
		private IEnumerable<LocalVariableInformation> GetScopeVariables(MDbgFrame frame)
		{
			frame.Function.GetArguments(frame);

			LocalVariableProcessor processor = new LocalVariableProcessor();

			processor.AddLocalValues(frame.Function.GetActiveLocalVars(frame));
			processor.AddLocalValues(frame.Function.GetArguments(frame));

			return processor.LocalVariables;
		}

		private class LocalVariableProcessor
		{
			private readonly List<LocalVariableInformation> locals = new List<LocalVariableInformation>();
			private readonly Dictionary<long, LocalVariableInformation> createdLocals = new Dictionary<long, LocalVariableInformation>();

			public IEnumerable<LocalVariableInformation> LocalVariables { get { return locals; } }

			public void AddLocalValues(IEnumerable<MDbgValue> values)
			{
				foreach (MDbgValue value in values)
				{
					LocalVariableInformation lvi = GetLocalVariableInformation(value, VariableType.Local, 0);

					if (lvi == null) continue;

					// Variables starting with CS$ are internal variables, and don't exist in C# source code.
					if (lvi.Name.StartsWith("CS$")) continue;

					locals.Add(lvi);
				}
			}

			private LocalVariableInformation GetLocalVariableInformation(MDbgValue value, VariableType variableType, int depth)
			{
				// Some MDbgValues don't have an associated CorValue. This occurs when there is no variable declaration in IL.
				if (value == null || value.CorValue == null)
					return null;

				if (createdLocals.ContainsKey(value.CorValue.Address))
					return createdLocals[value.CorValue.Address];

				var fields = new List<LocalVariableInformation>();

				if (value.IsComplexType && value.IsNull == false && depth < 10)
				{
					MDbgValue[] fieldValues = value.GetFields();
					foreach (var fieldValue in fieldValues)
					{
						fields.Add(GetLocalVariableInformation(fieldValue, VariableType.Field, depth + 1));
					}
				}

				var information = new LocalVariableInformation
				{
					TypeName = value.TypeName,
					Name = value.Name,
					StringValue = value.GetStringValue(false),
					TypeOfVariable = variableType,
					IsPrimitive = !value.IsComplexType,
					Fields = fields
				};

				createdLocals[value.CorValue.Address] = information;

				return information;
			}
		}

		/// <summary>
		/// Gets an array of strings containing information about each level of the call stack.
		/// </summary>
		/// <param name="thread">The thread to get the call stack from</param>
		/// <returns>An array of strings containing information about each level of the call stack.</returns>
		private string[] GetStackTrace(MDbgThread thread)
		{
			List<string> stackTrace = new List<string>();

			MDbgFrame af = thread.HaveCurrentFrame ? thread.CurrentFrame : null;
			MDbgFrame f = thread.BottomFrame;
			int i = 0;
			while (f != null)
			{
				if (f.IsInfoOnly)
				{
					f = f.NextUp;
					continue;
				}

				string functionName = f.Function.FullName;
				int sourceLine = -1;
				if (f.SourcePosition != null)
					sourceLine = f.SourcePosition.Line;
				string line
					= string.Format(CultureInfo.InvariantCulture,
					"{0}{1}. {2}:{3}",
						f.Equals(af) ? "*" : " ",
						i,
						functionName,
						sourceLine == -1 ? "" : sourceLine.ToString()
					);
				stackTrace.Add(line);
				f = f.NextUp;
				++i;
			}

			return stackTrace.ToArray();
		}

		/// <summary>
		/// This class is responsible for starting, monitoring, and stopping the ArchAngel
		/// Debug process. It also provides a method for getting the CommandReceiver object 
		/// required to communicate with the process once it has started up.
		/// </summary>
		private class DebugProcess
		{
			private static Process _DebugProcessInfo;
			private static List<string> _AssemblyLocations;
			private static string _ArchAngelProjectFilename;
			private static string _DebugProcessUri;

			private const int MAX_RETRIES = 5;

			/// <summary>
			/// Assemblies 
			/// </summary>
			internal static List<string> AssemblyLocations
			{
				get { return _AssemblyLocations; }
				set
				{
					List<string> assemblyLocations = new List<string>(value);
					_AssemblyLocations = Slyce.Common.VersionNumber.GetLocationsWithLatestVersions(assemblyLocations);
				}
			}

			private static string ArchAngelProjectFilename
			{
				get { return _ArchAngelProjectFilename; }
				set { _ArchAngelProjectFilename = value; }
			}

			/// <summary>
			/// Loads the specified assembly locations and project file in the Debug Process.
			/// </summary>
			/// <param name="locations">
			/// The locations where the Debug Process should look for assemblies. 
			/// These should be absolute paths, as the Debug Process may be running from a
			/// different location.
			/// </param>
			/// <param name="archAngelProjectFilename">The filename of the ArchAngel project to load. Should be an absolute path.</param>
			public static void LoadCommandReceiver(IEnumerable<string> locations, string archAngelProjectFilename)
			{
				AssemblyLocations = new List<string>(locations);
				ArchAngelProjectFilename = archAngelProjectFilename;
				CommandReceiver cr = GetCommandReceiver();
				bool result = cr.ExecuteCommand(new LoadAssembliesCommand(ArchAngelProjectFilename, AssemblyLocations, SharedData.AssemblySearchPaths, archAngelProjectFilename));
				if (result == false)
				{
					throw new Exception("Could not load the specified assemblies and project into the debug process.");
				}
			}

			/// <summary>
			/// Gets the Debug process id. Starts the process if it has not already been
			/// started.
			/// </summary>
			/// <returns>The PID of the Debug process.</returns>
			public static int GetDebugProcessId()
			{
				lock (typeof(DebugProcess))
				{
					if (_DebugProcessInfo != null && _DebugProcessInfo.HasExited == false)
					{
						return _DebugProcessInfo.Id;
					}

					StartDebugProcess();
					Thread.Sleep(50);
					return _DebugProcessInfo != null ? _DebugProcessInfo.Id : 0;
				}
			}

			/// <summary>
			/// Starts the ArchAngel Debug process, if it has not already been started.
			/// </summary>
			public static void StartDebugProcess()
			{
				lock (typeof(DebugProcess))
				{
					if (_DebugProcessInfo != null && !_DebugProcessInfo.HasExited) return;

					TcpChannel chan = SetupAndReturnTcpChannel();

					// Register as an available service with the name DebugProcessInfoService
					// Register as a Singleton so only one runs at a time.
					// The default lease time is 5 minutes, so the object will be
					// garbage collected eventually. Not that it is taking up a lot 
					// of memory anyway.
					RemotingConfiguration.RegisterWellKnownServiceType(
						typeof(DebugProcessInfoService),
						"DebugProcessInfoService",
						WellKnownObjectMode.Singleton);

					// Get the URI of our remoting services, so we can tell the Debug
					// Process how to contact us.
					string objectUri = "";
					string[] urls = chan.GetUrlsForUri("DebugProcessInfoService");
					if (urls != null && urls.Length > 0)
					{
						string objectUrl = urls[0];
						string channelUri = chan.Parse(objectUrl, out objectUri);
						objectUri = channelUri + objectUri;
					}

					if (string.IsNullOrEmpty(objectUri))
					{
						throw new Exception(
							"Debug Process Info Service could not register itself with .Net" +
							"remoting. This is a serious error and must be reported to Slyce.");
					}

					_DebugProcessInfo = new Process();
					// This is a replicated piece of information.
					// If the Debug Process is renamed, we need to change
					// this value.
					string path = Path.GetDirectoryName(Application.ExecutablePath);
					_DebugProcessInfo.StartInfo.FileName = Path.Combine(path, "ArchAngel.Debugger.DebugProcess.exe");
					_DebugProcessInfo.StartInfo.CreateNoWindow = true;
					_DebugProcessInfo.StartInfo.Arguments = Process.GetCurrentProcess().Id + " " + objectUri;
					_DebugProcessInfo.Start();

					// Wait until the process has started and filled out its information.
					DebugProcessInfoService.StartedWaitHandle.WaitOne();

					// Get the URI of the debug process control object.
					_DebugProcessUri = DebugProcessInfoService.Uri;

					// If we have this information, pass it through to the newly spawned
					// Debug Process.
					if (ArchAngelProjectFilename != null && _AssemblyLocations != null)
					{
						LoadCommandReceiver(_AssemblyLocations, ArchAngelProjectFilename);
					}
				}
			}

			/// <summary>
			/// Creates a new TcpChannel with a system assigned port, if no TcpChannel
			/// has been registered. Returns the registered TcpChannel.
			/// </summary>
			/// <returns></returns>
			private static TcpChannel SetupAndReturnTcpChannel()
			{
				if (ChannelServices.GetChannel("tcp") != null)
					return (TcpChannel)ChannelServices.GetChannel("tcp");

				TcpChannel chan = new TcpChannel(0);
				ChannelServices.RegisterChannel(chan, false);
				return chan;
			}

			/// <summary>
			/// Stops the currently running ArchAngel Debug process if it is running.
			/// This will attempt to inform it that it needs to stop once it has finished
			/// executing its current operation first, but if that fails it will kill it
			/// using Process.Kill(). Will do nothing if the Debug process is not running.
			/// </summary>
			public static void EndDebugProcess()
			{
				lock (typeof(DebugProcess))
				{
					if (_DebugProcessInfo == null || _DebugProcessInfo.HasExited) return;

					// Use this instead of GetCommandReceiver(), as we don't want to
					// start it if it isn't there.
					CommandReceiver obj
						= (CommandReceiver)Activator.GetObject(
											typeof(CommandReceiver), _DebugProcessUri);

					// Attempt to kill it nicely first.
					if (obj != null)
					{
						obj.ExecuteCommand(new ExitCommand());
					}

					// Otherwise kill it the hard way.
					_DebugProcessInfo.Kill();
				}
			}

			/// <summary>
			/// Gets the CommandReceiver object from the currently running
			/// ArchAngel Debug process.
			/// </summary>
			/// <returns>The remote CommandReceiver object</returns>
			/// <exception cref="System.Exception">If the ArchAngel Debug Process could 
			/// not be started or found</exception>
			public static CommandReceiver GetCommandReceiver()
			{
				int i = 0;
				while (_DebugProcessInfo == null || _DebugProcessInfo.HasExited)
				{
					if (i == MAX_RETRIES)
						throw new Exception("Could not start the ArchAngel Debugger Process.");
					StartDebugProcess();
					i++;
				}

				//SetupAndReturnTcpChannel();

				CommandReceiver obj
					= (CommandReceiver)Activator.GetObject(typeof(CommandReceiver), _DebugProcessUri);

				if (obj == null)
				{
					throw new Exception("Error: Unable to locate ArchAngel Debug Server");
				}

				return obj;
			}
		}
	}
}
