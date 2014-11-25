using System;
using System.Runtime.Serialization;
using System.Reflection;
using ArchAngel.Interfaces.ITemplate;
using System.Collections.Generic;
using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.BLL
{
	[DotfuscatorDoNotRename]
	sealed class Version2SerializationBinder : SerializationBinder
	{
		private readonly Dictionary<string, Type> FoundTypes = new Dictionary<string, Type>();
		private readonly Assembly ExecutingAssembly = Assembly.GetExecutingAssembly();

		[DotfuscatorDoNotRename]
		public override Type BindToType(string assemblyName, string typeName)
		{
			try
			{
				string key = assemblyName + "_" + typeName;

				if (FoundTypes.ContainsKey(key))
				{
					return FoundTypes[key];
				}
				bool keepAssemblyName = false;
				string originalAssemblyName = assemblyName;
				typeName = typeName.Replace("ArchAngel.Model", "ArchAngel.Providers.Database.Model");
				typeName = typeName.Replace("ArchAngel.BLL", "ArchAngel.Providers.Database.BLL");
				typeName = typeName.Replace("Slyce.ITemplate", "ArchAngel.Interfaces.ITemplate");

				if (typeName.IndexOf("ArchAngel.Interfaces.ITemplate") >= 0)
				{
					assemblyName = "ArchAngel.Interfaces";
					keepAssemblyName = true;
				}
				Type typeToDeserialize;

				if (!keepAssemblyName)
				{
					// Override the assembly name with the name of our new assembly
					assemblyName = ExecutingAssembly.FullName;
				}
				if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.Database,") >= 0)
				{
					typeToDeserialize = typeof(List<Model.Database>);
				}
				else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.Table,") >= 0)
				{
					typeToDeserialize = typeof(List<Model.Table>);
				}
				else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.View,") >= 0)
				{
					typeToDeserialize = typeof(List<Model.View>);
				}
				else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.StoredProcedure,") >= 0)
				{
					typeToDeserialize = typeof(List<Model.StoredProcedure>);
				}
				else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.Index,") >= 0)
				{
					typeToDeserialize = typeof(List<Index>);
				}
				else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.Key,") >= 0)
				{
					typeToDeserialize = typeof(List<Key>);
				}
				else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.Column,") >= 0)
				{
					typeToDeserialize = typeof(List<Column>);
				}
				else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.OneToOneRelationship,") >= 0)
				{
					typeToDeserialize = typeof(List<OneToOneRelationship>);
				}
				else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.OneToManyRelationship,") >= 0)
				{
					typeToDeserialize = typeof(List<OneToManyRelationship>);
				}
				else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.ManyToOneRelationship,") >= 0)
				{
					typeToDeserialize = typeof(List<ManyToOneRelationship>);
				}
				else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.ManyToManyRelationship,") >= 0)
				{
					typeToDeserialize = typeof(List<ManyToManyRelationship>);
				}
				else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.Filter,") >= 0)
				{
					typeToDeserialize = typeof(List<Filter>);
				}
				else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.StoredProcedure+Parameter,") >= 0)
				{
					typeToDeserialize = typeof(List<Model.StoredProcedure.Parameter>);
				}
				else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.Filter+FilterColumn,") >= 0)
				{
					typeToDeserialize = typeof(List<Filter.FilterColumn>);
				}
				else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.Filter+OrderByColumn,") >= 0)
				{
					typeToDeserialize = typeof(List<Filter.OrderByColumn>);
				}
				else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Interfaces.ITemplate.IUserOption,") >= 0)
				{
					typeToDeserialize = typeof(List<IUserOption>);
				}
                else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.Association,") >= 0)
                {
                    typeToDeserialize = typeof(List<Association>);
                }
                else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.RestrictedValue,") >= 0)
                {
                    return typeof(object);
                    //typeToDeserialize = typeof(List<RestrictedValue>);
                }
                else if (typeName.IndexOf("ArchAngel.Providers.Database.Model.RestrictedValue") >= 0)
                {
                    return typeof(object);
                    //typeToDeserialize = typeof(List<RestrictedValue>);
                }
                else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.Association+Mapping,") >= 0)
                {
                    typeToDeserialize = typeof(List<Association.Mapping>);
                }
                else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.Lookup,") >= 0)
                {
                    typeToDeserialize = typeof(List<Lookup>);
                }
                else if (typeName.IndexOf("System.Collections.Generic.List`1[[ArchAngel.Providers.Database.Model.LookupValue,") >= 0)
                {
                    typeToDeserialize = typeof(List<LookupValue>);
                }
				else
				{
					typeToDeserialize = ExecutingAssembly.GetType(typeName);

					if (typeToDeserialize == null)
					{
						// We should be able to resolve all non-generic types here, such as Table, Column, View etc
						typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
					}
				}
				if (typeToDeserialize == null)
				{
					typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, originalAssemblyName));

					if (typeToDeserialize == null)
					{
						if (Slyce.Common.Utility.StringsAreEqual(typeName, "ArchAngel.Providers.Database.BLL.ConnectionStringHelper+DatabaseTypes", false))
						{
							FoundTypes.Add(key, typeof(DatabaseTypes));
							return typeof(DatabaseTypes);
						}
						throw new NotImplementedException("ArchAngel type not yet handled in Version2SerializationBinder: " + typeName);
					}
				}
				if (typeToDeserialize == null)
				{
					throw new NotImplementedException("ArchAngel type not yet handled in Version2SerializationBinder: " + typeName);
				}
				FoundTypes.Add(key, typeToDeserialize);
				return typeToDeserialize;
			}
			catch (Exception ex)
			{
				NotImplementedException newEx = new NotImplementedException("ArchAngel type not yet handled in Version2SerializationBinder: " + typeName, ex);
				throw newEx;
			}
		}
	}

}
