using System;
using System.Runtime.Serialization;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2
{
    internal class NHibernateMappingException : Exception
    {
        public NHibernateMappingException()
        {
        }

        public NHibernateMappingException(string message) : base(message)
        {
        }

        public NHibernateMappingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NHibernateMappingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}