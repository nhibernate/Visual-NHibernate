//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------
using System;

using Microsoft.Samples.Debugging.CorDebug;


namespace Microsoft.Samples.Debugging.MdbgEngine
{
	/// <summary>
	/// MDbgOptions class.  This controls when the debugger should stop.
	/// </summary>
	public sealed class MDbgOptions : MarshalByRefObject
	{
		/// <summary>
		/// Gets or sets if it should stop when modules are loaded.
		/// </summary>
		/// <value>true if it should stop, else false.</value>
		public bool StopOnModuleLoad
		{
			get
			{
				return m_StopOnModuleLoad;
			}
			set
			{
				m_StopOnModuleLoad = value;
			}
		}
		private bool m_StopOnModuleLoad;

		/// <summary>
		/// Gets or sets if it should stop when classes are loaded.
		/// </summary>
		/// <value>true if it should stop, else false.</value>
		public bool StopOnClassLoad
		{
			get
			{
				return m_StopOnClassLoad;
			}
			set
			{
				m_StopOnClassLoad = value;
			}
		}
		private bool m_StopOnClassLoad;

		/// <summary>
		/// Gets or sets if it should stop when assemblies are loaded.
		/// </summary>
		/// <value>true if it should stop, else false.</value>
		public bool StopOnAssemblyLoad
		{
			get
			{
				return m_StopOnAssemblyLoad;
			}
			set
			{
				m_StopOnAssemblyLoad = value;
			}
		}
		private bool m_StopOnAssemblyLoad;

		/// <summary>
		/// Gets or sets if it should stop when assemblies are unloaded.
		/// </summary>
		/// <value>true if it should stop, else false.</value>
		public bool StopOnAssemblyUnload
		{
			get
			{
				return m_StopOnAssemblyUnload;
			}
			set
			{
				m_StopOnAssemblyUnload = value;
			}
		}
		private bool m_StopOnAssemblyUnload;

		/// <summary>
		/// Gets or sets if it should stop when new threads are created.
		/// </summary>
		/// <value>true if it should stop, else false.</value>
		public bool StopOnNewThread
		{
			get
			{
				return m_StopOnNewThread;
			}
			set
			{
				m_StopOnNewThread = value;
			}
		}
		private bool m_StopOnNewThread;

		/// <summary>
		/// Gets or sets if it should stop on Exception callbacks.
		/// </summary>
		/// <value>true if it should stop, else false.</value>
		public bool StopOnException
		{
			get
			{
				return m_StopOnException;
			}
			set
			{
				m_StopOnException = value;
			}
		}
		private bool m_StopOnException;

		/// <summary>
		/// Gets or sets if it should stop on Exception callbacks.
		/// </summary>
		/// <value>true if it should stop, else false.</value>
		public bool StopOnUnhandledException
		{
			get
			{
				return m_StopOnUnhandledException;
			}
			set
			{
				m_StopOnUnhandledException = value;
			}
		}
		private bool m_StopOnUnhandledException = true; // default is on.

		/// <summary>
		/// Gets or sets if it should stop on Enhanced Exception callbacks.
		/// </summary>
		/// <value>true if it should stop, else false.</value>
		public bool StopOnExceptionEnhanced
		{
			get
			{
				return m_StopOnExceptionEnhanced;
			}
			set
			{
				m_StopOnExceptionEnhanced = value;
			}
		}
		private bool m_StopOnExceptionEnhanced;

		/// <summary>
		/// Gets or sets if it should stop when messages are logged.
		/// You must still enable log messages per process by calling CorProcess.EnableLogMessage(true)
		/// </summary>
		/// <value>true if it should stop, else false.</value>
		public bool StopOnLogMessage
		{
			get
			{
				return m_stopOnLogMessage;
			}
			set
			{
				m_stopOnLogMessage = value;
			}
		}
		private bool m_stopOnLogMessage;


		/// <summary>
		/// Gets or sets the Symbol path.
		/// </summary>
		/// <value>The Symbol path.</value>
		public string SymbolPath
		{
			get
			{
				return m_symbolPath;
			}
			set
			{
				m_symbolPath = value;
			}
		}
		private string m_symbolPath = null;

		/// <summary>
		/// Gets or sets if processes are created with a new console.
		/// </summary>
		/// <value>Default is false.</value>
		public bool CreateProcessWithNewConsole
		{
			get
			{
				return m_CreateProcessWithNewConsole;
			}
			set
			{
				m_CreateProcessWithNewConsole = value;
			}
		}
		private bool m_CreateProcessWithNewConsole;

		/// <summary>
		/// Gets or sets if memory addresses are displayed.
		/// Normally of little value in pure managed debugging, and causes
		/// unpredictable output for automated testing.
		/// </summary>
		/// <value>Default is false.</value>
		public bool ShowAddresses
		{
			get
			{
				return m_ShowAddresses;
			}
			set
			{
				m_ShowAddresses = value;
			}
		}
		private bool m_ShowAddresses = false;

		/// <summary>
		/// Gets or sets if full paths are displayed in stack traces.
		/// </summary>
		/// <value>Default is true.</value>
		public bool ShowFullPaths
		{
			get
			{
				return m_ShowFullPaths;
			}
			set
			{
				m_ShowFullPaths = value;
			}
		}
		private bool m_ShowFullPaths = true;


		internal MDbgOptions()  // only one instance in mdbgeng
		{
		}

	}

	/// <summary>
	/// A delegate that returns a default implementation for stack walking frame factory.
	/// </summary>
	/// <returns>Frame factory used for newly created processes.</returns>
	public delegate IMDbgFrameFactory StackWalkingFrameFactoryProvider();

	/// <summary>
	/// The MDbgEngine class.
	/// </summary>
	public sealed class MDbgEngine : MarshalByRefObject
	{

		/// <summary>
		/// Initializes a new instance of the MDbgEngine class.
		/// </summary>
		public MDbgEngine()
		{
			m_processMgr = new MDbgProcessCollection(this);
		}

		/// <summary>
		/// Function that extensions can call to register a FrameFactory used for all new processes
		/// </summary>
		/// <param name="provider">A delegate that creates a new FrameFactory</param>
		/// <param name="updateExistingProcesses">If set, all currently debugged programs will be refreshed with new FrameFactory
		/// from the supplied provider.</param>
		public void RegisterDefaultStackWalkingFrameFactoryProvider(StackWalkingFrameFactoryProvider provider, bool updateExistingProcesses)
		{
			m_defaultStackWalkingFrameFactoryProvider = provider;
			if (updateExistingProcesses)
			{
				foreach (MDbgProcess p in Processes)
				{
					// force reloading of new frame factories...
					p.Threads.FrameFactory = null;
				}
			}
		}

		//////////////////////////////////////////////////////////////////////////////////
		//
		// Controlling Commands
		//
		//////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// creates a new debugged process.
		/// </summary>
		/// <param name="commandLine">The command to run.</param>
		/// <param name="commandArguments">The arguments for the command.</param>
		/// <param name="debugMode">The debug mode to run with.</param>
		/// <param name="deeVersion">The version of debugging interfaces that should be used for
		///   debuging of the started program. If this value is null, the default (latest) version
		///   of interface is used.
		/// </param>
		/// <returns>The resulting MDbgProcess.</returns>
		public MDbgProcess CreateProcess(string commandLine, string commandArguments,
										 DebugModeFlag debugMode, string deeVersion)
		{
			MDbgProcess p = m_processMgr.CreateLocalProcess(deeVersion);
			p.DebugMode = debugMode;
			p.CreateProcess(commandLine, commandArguments);
			return p;
		}

		/// <summary>
		/// Attach to a process with the given Process ID
		/// </summary>
		/// <param name="processId">The Process ID to attach to.</param>
		/// <returns>The resulting MDbgProcess.</returns>
		public MDbgProcess Attach(int processId)
		{
			string deeVersion;
			try
			{
				deeVersion = CorDebugger.GetDebuggerVersionFromPid(processId);
			}
			catch
			{
				// GetDebuggerVersionFromPid isn't implemented on Win9x and so will
				// throw NotImplementedException.  We'll also get an ArgumentException
				// if the specified process doesn't have the CLR loaded yet.  
				// Rather than be selective (and potentially brittle), we'll handle all errors.
				// Ideally we'd assert (or log) that we're only getting the errors we expect,
				// but it's complex and ugly to do that in C#.

				// Fall back to guessing the version based on the filename.
				// Environment variables (eg. COMPLUS_Version) may have resulted 
				// in a different choice.
				try
				{
					CorPublish.CorPublish cp = new CorPublish.CorPublish();
					CorPublish.CorPublishProcess cpp = cp.GetProcess(processId);
					string programBinary = cpp.DisplayName;

					deeVersion = CorDebugger.GetDebuggerVersionFromFile(programBinary);
				}
				catch
				{
					// This will also fail if the process doesn't have the CLR loaded yet.
					// It could also fail if the image EXE has been renamed since it started.
					// For whatever reason, fall back to using the default CLR
					deeVersion = null;
				}
			}

			MDbgProcess p = m_processMgr.CreateLocalProcess(deeVersion);
			p.Attach(processId);
			return p;
		}


		//////////////////////////////////////////////////////////////////////////////////
		//
		// Info about debugger process
		//
		//////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the MDbgProcessCollection.
		/// </summary>
		/// <value>The MDbgProcessCollection.</value>
		public MDbgProcessCollection Processes
		{
			get
			{
				return m_processMgr;
			}
		}

		/// <summary>
		/// Gets the current MDbgOptions.
		/// </summary>
		/// <value>The MDbgOptions.</value>
		public MDbgOptions Options
		{
			get
			{
				return m_options;
			}
		}

		//////////////////////////////////////////////////////////////////////////////////
		//
		// Private Implementation Part
		//
		//////////////////////////////////////////////////////////////////////////////////

		//////////////////////////////////////////////////////////////////////////////////
		//
		// Local variables
		//
		//////////////////////////////////////////////////////////////////////////////////

		private MDbgProcessCollection m_processMgr;

		private MDbgOptions m_options = new MDbgOptions();

		internal StackWalkingFrameFactoryProvider m_defaultStackWalkingFrameFactoryProvider = null;
	}
}
