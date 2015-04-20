using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using System.Security.Permissions;
using ArchAngel.Interfaces.ITemplate;

namespace ArchAngel.Providers.Database.Model
{
    [Serializable]
    [DoNotObfuscate]
    public class UserOption : ISerializable, IUserOption
    {
        [DoNotObfuscate]
        private string _name;
        [DoNotObfuscate]
        private Type _dataType;
        [DoNotObfuscate]
        private object _value;
        [DoNotObfuscate]
        [NonSerialized]
        private System.Collections.Hashtable HashMemberInfos = new System.Collections.Hashtable();

        public UserOption()
        {
        }

        public UserOption(string name, Type dataType, object value)
        {
            Name = name;
            DataType = dataType;
            Value = value;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                ArchAngel.Interfaces.Helper.RaiseDataChangedEvent(this.GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _name, value);
                _name = value;
            }
        }

        public Type DataType
        {
            get { return _dataType; }
            set
            {
                ArchAngel.Interfaces.Helper.RaiseDataChangedEvent(this.GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _dataType, value);
                _dataType = value;
            }
        }

        public object Value
        {
            get { return _value; }
            set
            {
                ArchAngel.Interfaces.Helper.RaiseDataChangedEvent(this.GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _value, value);
                _value = value;
            }
        }

        #region Serialization Stuff
        /// <exclude/>
        public UserOption(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            if (SerializerHelper.FileVersionCurrent <= SerializerHelper.FileVersionLatest)
            {
                this._dataType = (Type)serializationInfo.GetValue("DataType", typeof(Type));
                this._name = serializationInfo.GetString("Name");
                this._value = serializationInfo.GetValue("Value", typeof(object));
            }
            else
            {
                throw new NotImplementedException(string.Format("UserOption deserialize does not handle version {0} yet.", SerializerHelper.FileVersionCurrent));
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        /// <exclude/>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DataType", this._dataType);
            info.AddValue("Name", this._name);
            info.AddValue("Value", this._value);
        }
        #endregion

        public bool IsValid(bool deepCheck, out string failReason)
        {
            bool isValid = true;
            failReason = "";
            string tempFailReason = "";
            return true;
        }
    }
}
