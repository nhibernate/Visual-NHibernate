using System;

namespace ArchAngel.Designer.Exceptions
{
    internal class TypeNotFoundException : Exception
    {
        private string _TypeName;

        internal TypeNotFoundException(string message, string typeName)
            : base(message)
        {
            TypeName = typeName;
        }

        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }
    }
}
