using System;
using System.Collections.Generic;
using System.Text;

namespace ArchAngel.Interfaces
{
    /// <summary>
    /// A Setting that the Provider uses and which can be set by template authors in ArchAngel Designer.
    /// </summary>
    [DotfuscatorDoNotRename]
    public class Setting
    {
        [DotfuscatorDoNotRename]
        private static List<Type> SupportedTypes = new List<Type>(new Type[] { typeof(string), typeof(int), typeof(Enum), typeof(bool) });
        [DotfuscatorDoNotRename]
        private Type _Type;
        [DotfuscatorDoNotRename]
        private object _Value;
        [DotfuscatorDoNotRename]
        private string _Description;
        [DotfuscatorDoNotRename]
        private object _DefaultValue;

        /// <summary>
        /// Setting Constructor.
        /// </summary>
        /// <param name="type">The data-type of the Setting.</param>
        /// <param name="description">A description of the Setting.</param>
        /// <param name="defaultValue">The default value of the Setting.</param>
        public Setting(Type type, string description, object defaultValue)
        {
            CheckTypeIsSupported(type);
            Type = type;
            Value = defaultValue;
            Description = description;
            DefaultValue = defaultValue;
        }

        private void CheckTypeIsSupported(Type value)
        {
            bool isSupported = false;

            foreach (Type type in SupportedTypes)
            {
                if (value == type)
                {
                    isSupported = true;
                    break;
                }
            }
            if (!isSupported)
            {
                StringBuilder sb = new StringBuilder();

                foreach (Type type in SupportedTypes)
                {
                    sb.AppendLine(type.FullName);
                }
                throw new NotSupportedException(string.Format("ArchAngel.Interfaces.Setting does not support this type: {0}.{1}Supported types are:{1}{2}", value.FullName, Environment.NewLine, sb));
            }
        }

        /// <summary>
        /// The data-type of the Setting.
        /// </summary>
        public Type Type
        {
            get { return _Type; }
            set
            {
                CheckTypeIsSupported(value);
                _Type = value;
            }
        }

        /// <summary>
        /// The value of the Setting.
        /// </summary>
        public object Value
        {
            get { return _Value; }
            set 
            {
                if (this.Type == typeof(Enum))
                {
                    if (!typeof(List<string>).IsInstanceOfType(value))
                    {
                        throw new InvalidOperationException(string.Format("Value is an invalid data-type. Setting.Value should be [List<string>], but is [{0}].", value.GetType().FullName));
                    }
                }
                else if (!this.Type.IsInstanceOfType(value))
                {
                    throw new InvalidOperationException(string.Format("Value is an invalid data-type. Setting.Type is [{0}], but Setting.Value is being set to [{1}].", this.Type.FullName, value.GetType().FullName));
                }
                _Value = value; 
            }
        }

        /// <summary>
        /// The description of the Setting which is diaplyed to template authors in ArchAngel Designer.
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        /// <summary>
        /// The default value of the Setting.
        /// </summary>
        public object DefaultValue
        {
            get { return _DefaultValue; }
            set { _DefaultValue = value; }
        }
    }
}
