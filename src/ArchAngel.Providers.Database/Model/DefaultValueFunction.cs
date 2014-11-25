using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using System.Security.Permissions;
using ArchAngel.Interfaces.ITemplate;

namespace ArchAngel.Providers.Database.Model
{
    //[Serializable]
    [DoNotObfuscate]
    public class DefaultValueFunction : /*ISerializable,*/ IDefaultValueFunction
    {   
        [DoNotObfuscate]
        private Type _objectType;
        [DoNotObfuscate]
        private string _propertyName;
        [DoNotObfuscate]
        private string _functionName;
        [DoNotObfuscate]
        private bool _useCustomCode;
        [DoNotObfuscate]
        private FunctionTypes _functionType;

        public DefaultValueFunction()
        {
        }

        public DefaultValueFunction(Type objectType, string propertyName, bool useCustomCode, FunctionTypes functionType)
        {
            ObjectType = objectType;
            PropertyName = propertyName;
            UseCustomCode = useCustomCode;
            FunctionType = functionType;
        }

        public Type ObjectType
        {
            get { return _objectType; }
            set
            {
                //ArchAngel.Interfaces.Helper.RaiseDataChangedEvent(this.GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _objectType, value);
                _objectType = value;
            }
        }

        public string PropertyName
        {
            get { return _propertyName; }
            set
            {
                //ArchAngel.Interfaces.Helper.RaiseDataChangedEvent(this.GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _dataType, value);
                _propertyName = value;
            }
        }

        public string FunctionName
        {
            get { 
                switch (FunctionType)
                    {
                        case FunctionTypes.DefaultValue:
                            return string.Format("DefaultValue_{0}_{1}", ObjectType.Name, PropertyName);
                        case FunctionTypes.Validate:
                            return string.Format("Validate_{0}_{1}", ObjectType.Name, PropertyName);
                        case FunctionTypes.HelperOverride:
                            string functionName = ObjectType.FullName.Replace("ArchAngel.Providers.Database.", "") + "." + PropertyName;
                            return functionName.Replace(".", "_");
                        default:
                            throw new NotImplementedException("Not coded yet.");
                    }
                //return string.Format("DefaultValue_{0}_{1}", ObjectType.Name, PropertyName); 
            }
        }

        public bool UseCustomCode
        {
            get { return _useCustomCode; }
            set {_useCustomCode = value;}
        }

        public FunctionTypes FunctionType
        {
            get { return _functionType; }
            set { _functionType = value; }
        }

    }
}
