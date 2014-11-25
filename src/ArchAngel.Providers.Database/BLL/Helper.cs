using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.Win32;
// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.BLL
{
    public class Helper
    {
        private const string REGISTRY_KEY = @"Software\ArchAngel";

        private readonly DatabaseTypes _dalAssemblyName;

        public Helper(DatabaseTypes dalAssemblyName)
        {
            _dalAssemblyName = dalAssemblyName;
        }

        public Key GetPrimaryKey(Model.Table table)
        {
            IHelper dal = DALFactory.DataAccess.CreateHelper(_dalAssemblyName);

            return dal.GetPrimaryKey(table);
        }

        public bool IsDataTypeText(Column column)
        {
            IHelper dal = DALFactory.DataAccess.CreateHelper(_dalAssemblyName);

            return dal.IsDataTypeText(column);
        }

        public string[] GetDataTypes()
        {
            IHelper dal = DALFactory.DataAccess.CreateHelper(_dalAssemblyName);

            return dal.GetDataTypes();
        }

        public static string GetFilterAlias(Index index)
        {
            string alias = "Get" + index.Parent.Alias + "By" + Parameter.GetNameAfterSplit(index.Alias, '_');
        	return alias;
        }

        public static string GetFilterAlias(Key key)
        {
            string alias;
            if (key.Type == DatabaseConstant.KeyType.Primary)
            {
                alias = "Get" + key.Parent.Alias;
            }
            else
            {
                if (key.Type == DatabaseConstant.KeyType.Foreign)
                {
                    alias = "Get" + key.Parent.AliasPlural + "By";
                }
                else
                {
                    alias = "Get" + key.Parent.Alias + "By";
                }

                for (int i = 0; i < key.Columns.Length; i++)
                {
                    Column column = key.Columns[i];
                    alias += column.Alias;
                    if (i < key.Columns.Length - 1)
                    {
                        alias += "And";
                    }
                }
            }

            return alias;
        }

        public static void RegistryUpdateValue(string name, string value)
        {
            // Save last config file
            if (value != null)
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY, true);
                if (key != null)
                {
                    key.SetValue(name, value);
                }
            }
        }

        public static string RegistryGetValue(string name)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY);
            
			if (key == null)
            {
            	return null;
            }
        	return (string)key.GetValue(name);
        }

        public static Type GetBaseType(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            Type type = obj.GetType();
            while (type.BaseType != typeof(Object) && type.BaseType != typeof(MarshalByRefObject))
            {
                type = type.BaseType;
            }

            return type;
        }

        public static void SerializeUsingNetDataContract(object objectToSerialize, string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs))
                {
                    NetDataContractSerializer ser = new NetDataContractSerializer();
                    //List<Type> knownTypes = new List<Type>();
                    //DataContractSerializer ser = new DataContractSerializer(typeof(List<ArchAngel.Providers.Database.Model.Database>), "root", "http://www/", knownTypes, 1000, false, true, null);
                    ser.WriteObject(writer, objectToSerialize);
                    writer.Close();
                }
            }
        }

        public static object DeserializeUsingNetDataContract(string filename)
        {
            object theObject = null;
            int maxDepth = 10000;
            bool maxDepthExceeded = true;

            while (maxDepthExceeded)
            {
                try
                {
                    using (FileStream fs = new FileStream(filename, FileMode.Open))
                    {
                        XmlDictionaryReaderQuotas quotas = new XmlDictionaryReaderQuotas();
                        quotas.MaxDepth = maxDepth;

                        using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, quotas))
                        {
                            NetDataContractSerializer ser = new NetDataContractSerializer();
                            //ser.Binder = new Version2SerializationBinder();
                            theObject = (List<ArchAngel.Providers.Database.Model.Database>)ser.ReadObject(reader, true);
                        }
                        fs.Close();
                        maxDepthExceeded = false;
                    }
                }
                catch (SerializationException ex)
                {
                    if (ex.Message.IndexOf("more levels of nesting than is allowed by the quota") >= 0)
                    {
                        // We need to increase the maxDepth
                        maxDepth += 1000;
                        maxDepthExceeded = true;
                    }
                    else
                    {
                        // We have another kind of error
                        throw;
                    }
                }
            }
            return theObject;
        }



    }
}