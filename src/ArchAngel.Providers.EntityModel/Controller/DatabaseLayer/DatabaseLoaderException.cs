using System;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public class DatabaseLoaderException : Exception
	{
		private readonly bool useInnerException;

		public DatabaseLoaderException(string message, Exception innerException) : base(message, innerException)
		{
			useInnerException = true;
		}

		public DatabaseLoaderException(string message) : base(message)
		{
		}

		public string ActualMessage
		{
			get
			{
				if (useInnerException)
					return InnerException.Message;
				
				return Message;
			}
		}
	}
}