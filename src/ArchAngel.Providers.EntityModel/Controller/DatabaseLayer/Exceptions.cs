using System;
using System.Runtime.Serialization;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public class DatabaseSchemaInvalidException : DatabaseException
	{
		public DatabaseSchemaInvalidException()
		{
		}

		public DatabaseSchemaInvalidException(string message)
			: base(message)
		{
		}

		public DatabaseSchemaInvalidException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected DatabaseSchemaInvalidException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	public class DatabaseException : Exception
	{
		public DatabaseException()
		{
		}

		public DatabaseException(string message) : base(message)
		{
		}

		public DatabaseException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected DatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}