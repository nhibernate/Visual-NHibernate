using System;
using System.Collections.Generic;

namespace ArchAngel.Interfaces
{
	public interface IErrorReporter
	{
		void ReportError(ErrorLevel level, string message);
		void ReportUnhandledException(Exception e);
	}

	public enum ErrorLevel
	{
		UnhandledException,
		Fatal,
		Error,
		Warn,
		Trace,
		Debug,
		Info
	}

	public static class ErrorReportingService
	{
		private static readonly List<IErrorReporter> reporters = new List<IErrorReporter>();
		
		public static void RegisterErrorReporter(IErrorReporter reporter)
		{
			reporters.Add(reporter);
		}

		public static void ReportError(ErrorLevel level, string message)
		{
			foreach(var reporter in reporters)
				reporter.ReportError(level, message);
		}

		public static void ReportUnhandledException(Exception ex)
		{
			foreach (var reporter in reporters)
				reporter.ReportUnhandledException(ex);
		}
	}
}
