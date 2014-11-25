using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Threading;
using ArchAngel.Debugger.Common;
using log4net.Config;

namespace ArchAngel.Debugger
{
	public static class DebugProcessMain
	{
		internal static readonly EventWaitHandle EndDebugProcess = new AutoResetEvent(false);
		internal static List<string> _AssemblySearchPaths = new List<string>();
		internal static readonly string ApplicationDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

		internal static List<string> AssemblySearchPaths
		{
			get
			{
				if (_AssemblySearchPaths.Count == 0)
				{
					_AssemblySearchPaths.Add(ApplicationDirectory);
				}
				return _AssemblySearchPaths;
			}
		}

		public static int ArchAngelPID;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static int Main(string[] argv)
		{
			XmlConfigurator.Configure();
			//AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

			// Get ArchAngel PID from command line arguments
			if (argv.Length != 2)
			{
				Console.WriteLine("This application cannot be run separately from ArchAngel");
				return -1;
			}

			ArchAngelPID = Int32.Parse(argv[0]);
			string uri = argv[1];

			// Create an instance of a channel
			TcpChannel channel = new TcpChannel(0);
			ChannelServices.RegisterChannel(channel, false);

			// Register as an available service with the name CommandReceiver
			// Register as a Singleton so only one runs at a time.
			RemotingConfiguration.RegisterWellKnownServiceType(
				typeof(CommandReceiver),
				"CommandReceiver",
				WellKnownObjectMode.Singleton);

			ThreadStart start = SearchForArchAngelProcess;
			Thread thread = new Thread(start);
			thread.Start();

			DebugProcessInfoService dpis = (DebugProcessInfoService)Activator.GetObject(
							   typeof(DebugProcessInfoService), uri);

			if (dpis == null)
			{
				// ArchAngel is either not running or not responding. The process should die either way.
				EndDebugProcess.Set();
			}
			else
			{
				// Get the URI of our remoting services, so we can tell the ArchAngel
				// process how to contact us.
				string objectUri = "";
				string[] urls = channel.GetUrlsForUri("CommandReceiver");
				if (urls.Length > 0)
				{
					string objectUrl = urls[0];
					string channelUri = channel.Parse(objectUrl, out objectUri);
					objectUri = channelUri + objectUri;
				}
				if (string.IsNullOrEmpty(objectUri))
				{
					throw new Exception(
						"The Debug Process could not register itself with .Net" +
						"remoting. This is a serious error and must be reported to Slyce.");
				}

				// Inform ArchAngel that we have started.
				dpis.Started(objectUri);
			}

			// Wait for the signal to end the app.
			EndDebugProcess.WaitOne();
			return 0;
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			throw new Exception("An unhandled exception occurred in the DebugProcess", (Exception)e.ExceptionObject);
		}

		static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			return Slyce.Common.Utility.FindAssembly(args.Name, AssemblySearchPaths, "Debugger");
		}

		private static void SearchForArchAngelProcess()
		{
			Process proc = null;
			try
			{
				proc = Process.GetProcessById(ArchAngelPID);
			}
			catch (ArgumentException)
			{
				EndDebugProcess.Set();
				return;
			}

			while (true)
			{
				Thread.Sleep(10000);

				if (proc == null || proc.HasExited)
				{
					EndDebugProcess.Set();
					break;
				}
			}
		}
	}
}
