using System;
using System.Runtime.Serialization;

namespace Slyce.Common.Exceptions
{
    public class DeserialisationException : Exception
    {
        public DeserialisationException()
        {
        }

        public DeserialisationException(string message) : base(message)
        {
        }

        public DeserialisationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeserialisationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

	public class SerialisationException : Exception
	{
		public SerialisationException()
		{
		}

		public SerialisationException(string message)
			: base(message)
		{
		}

		public SerialisationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected SerialisationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}