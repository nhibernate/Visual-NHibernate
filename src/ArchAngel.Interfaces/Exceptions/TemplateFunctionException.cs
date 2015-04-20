using System;

namespace ArchAngel.Interfaces.Exceptions
{
	public class TemplateFunctionException : Exception
	{
		public string FunctionName;

		public TemplateFunctionException(string functionName, string message) : base(message)
		{
			FunctionName = functionName;
		}

		public TemplateFunctionException(string functionName, string message, Exception innerException) : base(message, innerException)
		{
			FunctionName = functionName;
		}
	}

	public class FunctionMissingException : Exception
	{
		public string FunctionName;

		public FunctionMissingException(string functionName, string message)
			: base(message)
		{
			FunctionName = functionName;
		}

		public FunctionMissingException(string functionName, string message, Exception innerException)
			: base(message, innerException)
		{
			FunctionName = functionName;
		}
	}
}