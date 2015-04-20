using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database
{
    public class TempDeserializerBase
    {
        [DoNotObfuscate]
        [NonSerialized]
        protected System.Collections.Hashtable HashMemberInfos = new System.Collections.Hashtable();

        public void DeserializeOldWay(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            Type thisType = this.GetType();
            if (false)
            {
                return;
            }
            try
            {
                // Get the set of serializable members for our class and base classes
                MemberInfo[] mi = thisType.GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
                FieldInfo[] ooo = thisType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
                SerializationInfoEnumerator e = serializationInfo.GetEnumerator();

                while (e.MoveNext())
                {
                    //if (e.Name == "_connectionString") { System.Diagnostics.Debugger.Break(); }
                    //if (e.Name == "ScriptObject+_aliasPlural") { System.Diagnostics.Debugger.Break(); }
                    if (e.Name != "__identity") // MarshalByRef property, which we don't want serialized etc.
                    {
                        bool found = false;
                        // Find the corresponding field in the current version of the object
                        for (int i = 0; i < mi.Length; i++)
                        {
                            if (mi[i].Name == e.Name)
                            {
                                found = true;
                                // To ease coding, treat the member as a FieldInfo object
                                FieldInfo fi = (FieldInfo)mi[i];
                                Type currentType = e.ObjectType;

                                if (e.ObjectType == typeof(object))
                                {
                                    try
                                    {
                                        //fi.SetValue(this, serializationInfo.GetValue(fi.Name, fi.FieldType));
                                        fi.SetValue(this, serializationInfo.GetValue(fi.Name, typeof(object)));
                                        break;
                                    }
                                    catch
                                    {
                                        //Do nothing, let the code below handle it
                                    }
                                }
                                while (currentType != null && currentType != fi.FieldType)
                                {
                                    currentType = currentType.BaseType;
                                }
                                if (currentType != null)
                                {
                                    // Set the field to the deserialized value
                                    //fi.SetValue(this, serializationInfo.GetValue(fi.Name, fi.FieldType));
                                    fi.SetValue(this, serializationInfo.GetValue(fi.Name, typeof(object)));
                                    break;
                                }
                                else // We need to perform some kind of transformation from the old type to the new type
                                {
                                    switch (thisType.Name)
                                    {
                                        case "Database":
                                            switch (e.Name)
                                            {
                                                case "_connectionString":
                                                    string gg = "";
                                                    BLL.ConnectionStringHelper connStringHelper = new ArchAngel.Providers.Database.BLL.ConnectionStringHelper("local", "ProMapp_V2", "sa", "", false, false, "", ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServer2000);
                                                    fi.SetValue(this, connStringHelper);
                                                    break;
                                            }
                                            break;
                                        default:
                                            throw new NotImplementedException("A Deserialization transformation is required. See the Model.Database deserialization routine for an example of how to do this.");
                                    }
                                }
                            }
                        }
                        // Check the fields
                        if (!found)
                        {
                            // Check whether the base object has the missing fields
                            bool parentHasField = false;
                            Type currentType = thisType.BaseType;

                            while (!parentHasField && currentType != null)
                            {
                                MemberInfo[] mi2 = null;

                                if (HashMemberInfos.Count == 0 || HashMemberInfos[currentType] == null)
                                {
                                    mi2 = currentType.GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);

                                    if (HashMemberInfos.Count == 0)
                                    {
                                        HashMemberInfos = new System.Collections.Hashtable();
                                    }
                                    HashMemberInfos.Add(currentType, mi2);
                                }
                                mi2 = (MemberInfo[])HashMemberInfos[currentType];

                                for (int i = 0; i < mi2.Length; i++)
                                {
                                    if (mi2[i].Name == e.Name)
                                    {
                                        parentHasField = true;
                                        FieldInfo fi = (FieldInfo)mi2[i];
                                        //fi.SetValue(this, serializationInfo.GetValue(fi.Name, fi.FieldType));
                                        fi.SetValue(this, serializationInfo.GetValue(fi.Name, typeof(object)));
                                        break;
                                    }
                                }
                                if (parentHasField == false && e.Name.IndexOf("+") > 0)
                                {
                                    FieldInfo[] fields = currentType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);

                                    foreach (FieldInfo field in fields)
                                    {
                                        if (field.Name == e.Name.Split('+')[1])
                                        {
                                            parentHasField = true;
                                            ScriptObject oo = (ScriptObject)this;
                                            field.SetValue(oo, serializationInfo.GetValue(field.Name, typeof(object)));
                                            break;
                                        }
                                    }
                                }
                                currentType = currentType.BaseType;
                            }
                            if (!parentHasField)
                            {
                                throw new NotImplementedException(string.Format("Corresponding field not found for field belonging to previous version of '{2}': {0} ({1}).", e.Name, e.ObjectType, this.GetType().Name));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;

                if (ex.InnerException != null)
                {
                    message += "\n\nInner Exception:\n" + ex.InnerException.Message;
                }
                message += "\n\nStack Trace:\n" + ex.StackTrace;
                throw;
            }
        }

    }
}
