using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Reflection;

using Microsoft.Win32;

namespace ArchAngel.Providers.Database.Model
{
    [Serializable()]
    public sealed class AppConfig
    {
        private string _projectPath;
        private string _templateFileName;
        private string _setupTemplateFileName;
        private string _fileName;
        private const string REGISTRY_KEY = @"Software\ArchAngel";
        private static Hashtable Settings = new Hashtable(50);

        public AppConfig()
        {
        }

        public AppConfig(string defaultTemplateFileName, string defaultSetupModelTemplateFileName)
        {
            _templateFileName = defaultTemplateFileName;
            _setupTemplateFileName = defaultSetupModelTemplateFileName;
        }

        public AppConfig(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            Type type = this.GetType();
            while (type != typeof(object))
            {
                MemberInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (MemberInfo field in fields)
                {
                    FieldInfo reflectedField = field.ReflectedType.GetField(field.Name, BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
                    try
                    {
                        if (field.Name != "__identity") // MarshalByRef property, which we don't want serialized etc.
                        {
                            object reflectedVal = serializationInfo.GetValue(field.Name, reflectedField.FieldType);
                            type.InvokeMember(field.Name, BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Instance, null, this, new object[] { reflectedVal });
                        }
                    }
                    catch
                    {
                        //throw; 
                    }
                }
                type = type.BaseType;
            }
        }

        public void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            Type type = this.GetType();
            while (type != typeof(object))
            {
                MemberInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (MemberInfo field in fields)
                {
                    object reflectedObj = type.InvokeMember(field.Name, BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance, null, this, null);
                    serializationInfo.AddValue(field.Name, reflectedObj);
                }
                type = type.BaseType;
            }
        }

        private static string RegistryGetValue(string name)
        {
            if (Settings[name] != null)
            {
                return (string)Settings[name];
            }
            RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY);
            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY);
                Settings[name] = null;
                return null;
            }
            else
            {
                Settings[name] = (string)key.GetValue(name);
                return (string)key.GetValue(name);
            }
        }

        private static void RegistryUpdateValue(string name, string value)
        {
            // Save last config file
            if (value != null)
            {
                if (Settings[name] != null && Settings[name] == value)
                {
                    // Value hasn't changed, so don't bother updating the file
                    return;
                }
                Settings[name] = value;
                RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY, true);
                if (key != null)
                {
                    key.SetValue(name, value);
                }
            }
        }

        public static bool IsActive
        {
            get { return System.Convert.ToBoolean(RegistryGetValue("_isActive")); }
            set { RegistryUpdateValue("_isActive", value.ToString()); }
        }

        public static string ActiveProjectPath
        {
            get { return RegistryGetValue("_activeProjectPath"); }
            set { RegistryUpdateValue("_activeProjectPath", value.ToString()); }
        }

        public string ProjectPath
        {
            get { return _projectPath; }
            set 
            {
                ArchAngel.Interfaces.Helper.RaiseDataChangedEvent(this.GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _projectPath, value);
                _projectPath = value;
                ArchAngel.Interfaces.SharedData.ProjectPath = value;
            }
        }

        public string TemplateFileName
        {
            get { return _templateFileName; }
            set 
            {
                ArchAngel.Interfaces.Helper.RaiseDataChangedEvent(this.GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _templateFileName, value);
                _templateFileName = value;
                ArchAngel.Interfaces.SharedData.TemplateFileName = value;
            }
        }

        public string FileName
        {
            get { return _fileName; }
            set 
            {
                ArchAngel.Interfaces.Helper.RaiseDataChangedEvent(this.GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _fileName, value);
                _fileName = value; 
            }
        }
    }
}
