using System;
using System.Linq;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Scripting.NHibernate.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.NHibernateHelper.EntityExtensions
{
	public static class EntityModelExtensionMethods
	{
		public static void SetUserOption<T>(this IScriptBaseObject obj, string optionName, T value)
		{
			var option = obj.Ex.FirstOrDefault(uo => uo.Name == optionName);

			if (option != null)
				option.Value = value;
			else
				obj.AddUserOption(new UserOption(optionName, typeof(T), value));
		}

		#region Reference - End1AssociationType
		public static void SetEnd1AssociationType(this Reference reference, AssociationType associationType)
		{
			reference.SetUserOption("End1CollectionType", associationType);
		}

		public static AssociationType GetEnd1AssociationType(this Reference reference)
		{
			return (AssociationType)reference.GetUserOptionValue("End1CollectionType");
		}
		#endregion

		#region Reference - End2AssociationType
		public static void SetEnd2AssociationType(this Reference reference, AssociationType associationType)
		{
			reference.SetUserOption("End2CollectionType", associationType);
		}

		public static AssociationType GetEnd2AssociationType(this Reference reference)
		{
			return (AssociationType)reference.GetUserOptionValue("End2CollectionType");
		}
		#endregion

		#region Reference - End1IndexColumnName
		public static string GetEnd1IndexColumnName(this Reference reference)
		{
			return (string)reference.GetUserOptionValue("End1IndexColumn");
		}

		public static void SetEnd1IndexColumnName(this Reference reference, string value)
		{
			reference.SetUserOption("End1IndexColumn", value);
		}
		#endregion

		#region Reference - End2IndexColumnName
		public static string GetEnd2IndexColumnName(this Reference reference)
		{
			return (string)reference.GetUserOptionValue("End2IndexColumn");
		}

		public static void SetEnd2IndexColumnName(this Reference reference, string value)
		{
			reference.SetUserOption("End2IndexColumn", value);
		}
		#endregion

		#region Reference - End1SqlWhereClause
		public static void SetEnd1SqlWhereClause(this Reference reference, string sql)
		{
			if (string.IsNullOrEmpty(sql))
				return;

			reference.Ex.Add(new UserOption("End1SqlWhereClause", typeof(string), sql));
		}
		#endregion

		#region Reference - End2SqlWhereClause
		public static void SetEnd2SqlWhereClause(this Reference reference, string sql)
		{
			if (string.IsNullOrEmpty(sql))
				return;

			reference.Ex.Add(new UserOption("End2SqlWhereClause", typeof(string), sql));
		}
		#endregion

		#region Entity - SQL Where
		public static string GetEntitySqlWhereClause(this Entity entity)
		{
			if (entity.HasUserOption("Entity_SqlWhereClause"))
				return entity.GetUserOptionValue<string>("Entity_SqlWhereClause");

			return "";
		}

		public static void SetEntitySqlWhereClause(this Entity entity, string sql)
		{
			if (string.IsNullOrEmpty(sql))
				return;

			entity.SetUserOption("Entity_SqlWhereClause", sql);
		}
		#endregion

		#region Entity - Dynamic Update
		public static bool GetEntityDynamicUpdate(this Entity entity)
		{
			if (entity.HasUserOption("Entity_DynamicUpdate"))
				return entity.GetUserOptionValue<bool>("Entity_DynamicUpdate");

			return false;
		}

		public static void SetEntityDynamicUpdate(this Entity entity, bool dynamicUpdate)
		{
			entity.SetUserOption("Entity_DynamicUpdate", dynamicUpdate);
		}
		#endregion

		#region Entity - Dynamic Insert
		public static bool GetEntityDynamicInsert(this Entity entity)
		{
			if (entity.HasUserOption("Entity_DynamicInsert"))
				return entity.GetUserOptionValue<bool>("Entity_DynamicInsert");

			return false;
		}

		public static void SetEntityDynamicInsert(this Entity entity, bool dynamicInsert)
		{
			entity.SetUserOption("Entity_DynamicInsert", dynamicInsert);
		}
		#endregion

		#region Property - IsVersion
		public static bool GetPropertyIsVersion(this Property property)
		{
			object value = property.GetUserOptionValue("Property_IsVersion");

			if (value is string)
				return bool.Parse((string)value);
			else
				return (bool)value;
		}

		public static void SetPropertyIsVersion(this Property property, bool isVersion)
		{
			property.SetUserOption("Property_IsVersion", isVersion);
		}
		#endregion

		#region Property - Insert
		public static bool GetPropertyInsert(this Property property)
		{
			object value = property.GetUserOptionValue("Property_Insert");

			if (value is string)
				return bool.Parse((string)value);
			else
				return (bool)value;
		}

		public static void SetPropertyInsert(this Property property, bool insert)
		{
			property.SetUserOption("Property_Insert", insert);
		}
		#endregion

		#region Property - Update
		public static bool GetPropertyUpdate(this Property property)
		{
			object value = property.GetUserOptionValue("Property_Update");

			if (value is string)
				return bool.Parse((string)value);
			else
				return (bool)value;
		}

		public static void SetPropertyUpdate(this Property property, bool update)
		{
			property.SetUserOption("Property_Update", update);
		}
		#endregion

		#region Property - Formula
		public static string GetPropertyFormula(this Property property)
		{
			object value = property.GetUserOptionValue("Property_Formula");
			return (string)value;
		}

		public static void SetPropertyFormula(this Property property, string formula)
		{
			property.SetUserOption("Property_Formula", formula);
		}
		#endregion

		#region Property - OptimisticLock
		public static bool GetPropertyOptimisticLock(this Property property)
		{
			object value = property.GetUserOptionValue("Property_OptimisticLock");

			if (value is string)
				return bool.Parse((string)value);
			else
				return (bool)value;
		}

		public static void SetPropertyOptimisticLock(this Property property, bool optimisticLock)
		{
			property.SetUserOption("Property_OptimisticLock", optimisticLock);
		}
		#endregion

		#region Property - Access
		public static ArchAngel.Interfaces.NHibernateEnums.PropertyAccessTypes GetPropertyAccess(this Property property)
		{
			object value = property.GetUserOptionValue("Property_Access");

			if (value is string)
				return (ArchAngel.Interfaces.NHibernateEnums.PropertyAccessTypes)Enum.Parse(typeof(ArchAngel.Interfaces.NHibernateEnums.PropertyAccessTypes), (string)value);
			else
				return (ArchAngel.Interfaces.NHibernateEnums.PropertyAccessTypes)value;
		}

		public static void SetPropertyAccess(this Property property, ArchAngel.Interfaces.NHibernateEnums.PropertyAccessTypes access)
		{
			property.SetUserOption("Property_Access", access);
		}
		#endregion

		#region Property - IsLazy
		public static bool GetPropertyIsLazy(this Property property)
		{
			object value = property.GetUserOptionValue("Property_IsLazy");

			if (value is string)
				return (bool)Enum.Parse(typeof(bool), (string)value);
			else
				return (bool)value;
		}

		public static void SetPropertyIsLazy(this Property property, bool isLazy)
		{
			property.SetUserOption("Property_IsLazy", isLazy);
		}
		#endregion

		#region Entity - DefaultAccess
		public static ArchAngel.Interfaces.NHibernateEnums.PropertyAccessTypes GetEntityDefaultAccess(this Entity entity)
		{
			object value = entity.GetUserOptionValue("Entity_DefaultAccess");

			if (value is string)
				return (ArchAngel.Interfaces.NHibernateEnums.PropertyAccessTypes)Enum.Parse(typeof(ArchAngel.Interfaces.NHibernateEnums.PropertyAccessTypes), (string)value);
			else
				return (ArchAngel.Interfaces.NHibernateEnums.PropertyAccessTypes)value;
		}

		public static void SetEntityDefaultAccess(this Entity entity, ArchAngel.Interfaces.NHibernateEnums.PropertyAccessTypes access)
		{
			entity.SetUserOption("Property_DefaultAccess", access);
		}
		#endregion

		#region Property - Generated
		public static ArchAngel.Interfaces.NHibernateEnums.PropertyGeneratedTypes GetPropertyGenerated(this Property property)
		{
			object value = property.GetUserOptionValue("Property_Generation");

			if (value is string)
				return (ArchAngel.Interfaces.NHibernateEnums.PropertyGeneratedTypes)Enum.Parse(typeof(ArchAngel.Interfaces.NHibernateEnums.PropertyGeneratedTypes), (string)value);
			else
				return (ArchAngel.Interfaces.NHibernateEnums.PropertyGeneratedTypes)value;
		}

		public static void SetPropertyGenerated(this Property property, ArchAngel.Interfaces.NHibernateEnums.PropertyGeneratedTypes generated)
		{
			property.SetUserOption("Property_Generation", generated);
		}
		#endregion

		#region Entity - Mutable
		public static void SetEntityMutable(this Entity entity, bool mutable)
		{
			entity.SetUserOption("Entity_Mutable", mutable);
		}

		public static bool GetEntityMutable(this Entity entity)
		{
			if (entity.HasUserOption("Entity_Mutable"))
				return entity.GetUserOptionValue<bool>("Entity_Mutable");

			return false;
		}
		#endregion

		#region Entity - Persister

		public static void SetEntityPersister(this Entity entity, string persister)
		{
			entity.SetUserOption("Entity_Persister", persister);
		}

		public static string GetEntityPersister(this Entity entity)
		{
			if (entity.HasUserOption("Entity_Persister"))
				return entity.GetUserOptionValue<string>("Entity_Persister");

			return "";
		}
		#endregion

		#region Entity - OptimisticLock

		public static void SetEntityOptimisticLock(this Entity entity, OptimisticLockModes optimisticLock)
		{
			entity.SetUserOption("Entity_OptimisticLock", optimisticLock);
		}

		public static OptimisticLockModes GetEntityOptimisticLock(this Entity entity)
		{
			if (entity.HasUserOption("Entity_OptimisticLock"))
				return entity.GetUserOptionValue<OptimisticLockModes>("Entity_OptimisticLock");

			return OptimisticLockModes.none;
		}
		#endregion

		#region Reference - CollectionFetchMode

		public static void SetReferenceEnd1CollectionFetchMode(this Reference reference, CollectionFetchModes fetchMode)
		{
			reference.SetUserOption("Reference_End1CollectionFetchMode", fetchMode);
		}

		public static CollectionFetchModes GetReferenceEnd1CollectionFetchMode(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End1CollectionFetchMode"))
				return reference.GetUserOptionValue<CollectionFetchModes>("Reference_End1CollectionFetchMode");

			return CollectionFetchModes.select;
		}

		public static void SetReferenceEnd2CollectionFetchMode(this Reference reference, CollectionFetchModes fetchMode)
		{
			reference.SetUserOption("Reference_End2CollectionFetchMode", fetchMode);
		}

		public static CollectionFetchModes GetReferenceEnd2CollectionFetchMode(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End2CollectionFetchMode"))
				return reference.GetUserOptionValue<CollectionFetchModes>("Reference_End2CollectionFetchMode");

			return CollectionFetchModes.select;
		}
		#endregion

		#region Reference - FetchMode

		public static void SetReferenceEnd1FetchMode(this Reference reference, FetchModes fetchMode)
		{
			reference.SetUserOption("Reference_End1FetchMode", fetchMode);
		}

		public static FetchModes GetReferenceEnd1FetchMode(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End1FetchMode"))
				return reference.GetUserOptionValue<FetchModes>("Reference_End1FetchMode");

			return FetchModes.select;
		}

		public static void SetReferenceEnd2FetchMode(this Reference reference, FetchModes fetchMode)
		{
			reference.SetUserOption("Reference_End2FetchMode", fetchMode);
		}

		public static FetchModes GetReferenceEnd2FetchMode(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End2FetchMode"))
				return reference.GetUserOptionValue<FetchModes>("Reference_End2FetchMode");

			return FetchModes.select;
		}
		#endregion

		#region Reference - Insert

		public static void SetReferenceEnd1Insert(this Reference reference, bool insert)
		{
			reference.SetUserOption("Reference_End1Insert", insert);
		}

		public static bool GetReferenceEnd1Insert(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End1Insert"))
				return reference.GetUserOptionValue<bool>("Reference_End1Insert");

			return true;
		}

		public static void SetReferenceEnd2Insert(this Reference reference, bool insert)
		{
			reference.SetUserOption("Reference_End2Insert", insert);
		}

		public static bool GetReferenceEnd2Insert(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End2Insert"))
				return reference.GetUserOptionValue<bool>("Reference_End2Insert");

			return true;
		}
		#endregion

		#region Reference - Update

		public static void SetReferenceEnd1Update(this Reference reference, bool update)
		{
			reference.SetUserOption("Reference_End1Update", update);
		}

		public static bool GetReferenceEnd1Update(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End1Update"))
				return reference.GetUserOptionValue<bool>("Reference_End1Update");

			return true;
		}

		public static void SetReferenceEnd2Update(this Reference reference, bool update)
		{
			reference.SetUserOption("Reference_End2Update", update);
		}

		public static bool GetReferenceEnd2Update(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End2Update"))
				return reference.GetUserOptionValue<bool>("Reference_End2Update");

			return true;
		}
		#endregion

		#region Reference - Inverse

		public static void SetReferenceEnd1Inverse(this Reference reference, ArchAngel.Interfaces.NHibernateEnums.BooleanInheritedTypes inverse)
		{
			reference.SetUserOption("End1Inverse", inverse);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.BooleanInheritedTypes GetReferenceEnd1Inverse(this Reference reference)
		{
			if (reference.HasUserOption("End1Inverse"))
				return reference.GetUserOptionValue<ArchAngel.Interfaces.NHibernateEnums.BooleanInheritedTypes>("End1Inverse");

			return ArchAngel.Interfaces.NHibernateEnums.BooleanInheritedTypes.inherit_default;
		}

		public static void SetReferenceEnd2Inverse(this Reference reference, ArchAngel.Interfaces.NHibernateEnums.BooleanInheritedTypes inverse)
		{
			reference.SetUserOption("End2Inverse", inverse);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.BooleanInheritedTypes GetReferenceEnd2Inverse(this Reference reference)
		{
			if (reference.HasUserOption("End2Inverse"))
				return reference.GetUserOptionValue<ArchAngel.Interfaces.NHibernateEnums.BooleanInheritedTypes>("End2Inverse");

			return ArchAngel.Interfaces.NHibernateEnums.BooleanInheritedTypes.inherit_default;
		}
		#endregion

		#region Reference - Lazy

		public static void SetReferenceEnd1Lazy(this Reference reference, ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes lazy)
		{
			reference.SetUserOption("Reference_End1Lazy", lazy);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes GetReferenceEnd1Lazy(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End1Lazy"))
				return reference.GetUserOptionValue<ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes>("Reference_End1Lazy");

			return ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes.@true;
		}

		public static void SetReferenceEnd2Lazy(this Reference reference, ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes lazy)
		{
			reference.SetUserOption("Reference_End2Lazy", lazy);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes GetReferenceEnd2Lazy(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End2Lazy"))
				return reference.GetUserOptionValue<ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes>("Reference_End2Lazy");

			return ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes.@true;
		}
		#endregion

		#region Reference - Cascade

		public static void SetReferenceEnd1Cascade(this Reference reference, ArchAngel.Interfaces.NHibernateEnums.CascadeTypes cascade)
		{
			reference.SetUserOption("Reference_End1Cascade", cascade);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.CascadeTypes GetReferenceEnd1Cascade(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End1Cascade"))
				return reference.GetUserOptionValue<ArchAngel.Interfaces.NHibernateEnums.CascadeTypes>("Reference_End1Cascade");

			return ArchAngel.Interfaces.NHibernateEnums.CascadeTypes.inherit_default;
		}

		public static void SetReferenceEnd2Cascade(this Reference reference, ArchAngel.Interfaces.NHibernateEnums.CascadeTypes cascade)
		{
			reference.SetUserOption("Reference_End2Cascade", cascade);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.CascadeTypes GetReferenceEnd2Cascade(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End2Cascade"))
				return reference.GetUserOptionValue<ArchAngel.Interfaces.NHibernateEnums.CascadeTypes>("Reference_End2Cascade");

			return ArchAngel.Interfaces.NHibernateEnums.CascadeTypes.inherit_default;
		}
		#endregion

		#region Project - DefaultAccess

		public static void SetProjectDefaultAccess(this IWorkbenchProject project, ArchAngel.Interfaces.NHibernateEnums.TopLevelAccessTypes access)
		{
			project.SetUserOption("DefaultAccess", access);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.TopLevelAccessTypes GetProjectDefaultAccess(this IWorkbenchProject project)
		{
			object value = project.GetUserOption("DefaultAccess");

			if (value is string)
				return (ArchAngel.Interfaces.NHibernateEnums.TopLevelAccessTypes)Enum.Parse(typeof(ArchAngel.Interfaces.NHibernateEnums.TopLevelAccessTypes), (string)value);
			else
				return (ArchAngel.Interfaces.NHibernateEnums.TopLevelAccessTypes)value;
		}
		#endregion

		#region Project - DefaultCascade

		public static void SetProjectDefaultCascade(this IWorkbenchProject project, ArchAngel.Interfaces.NHibernateEnums.TopLevelCascadeTypes cascade)
		{
			project.SetUserOption("DefaultCascade", cascade);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.TopLevelCascadeTypes GetProjectDefaultCascade(this IWorkbenchProject project)
		{
			object value = project.GetUserOption("DefaultCascade");

			if (value is string)
				return (ArchAngel.Interfaces.NHibernateEnums.TopLevelCascadeTypes)Enum.Parse(typeof(ArchAngel.Interfaces.NHibernateEnums.TopLevelCascadeTypes), (string)value);
			else
				return (ArchAngel.Interfaces.NHibernateEnums.TopLevelCascadeTypes)value;
		}
		#endregion

		#region Project - DefaultCollectionCascade

		public static void SetProjectDefaultCollectionCascade(this IWorkbenchProject project, ArchAngel.Interfaces.NHibernateEnums.TopLevelCollectionCascadeTypes cascade)
		{
			project.SetUserOption("DefaultCollectionCascade", cascade);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.TopLevelCollectionCascadeTypes GetProjectDefaultCollectionCascade(this IWorkbenchProject project)
		{
			object value = project.GetUserOption("DefaultCollectionCascade");

			if (value is string)
				return (ArchAngel.Interfaces.NHibernateEnums.TopLevelCollectionCascadeTypes)Enum.Parse(typeof(ArchAngel.Interfaces.NHibernateEnums.TopLevelCollectionCascadeTypes), (string)value);
			else
				return (ArchAngel.Interfaces.NHibernateEnums.TopLevelCollectionCascadeTypes)value;
		}
		#endregion

		#region Project - DefaultLazy

		public static void SetProjectDefaultLazy(this IWorkbenchProject project, bool lazy)
		{
			project.SetUserOption("DefaultLazy", lazy);
		}

		public static bool GetProjectDefaultLazy(this IWorkbenchProject project)
		{
			object value = project.GetUserOption("DefaultLazy");

			if (value is string)
				return bool.Parse((string)value);
			else
				return (bool)value;
		}
		#endregion

		#region Project - DefaultCollectionLazy

		public static void SetProjectDefaultCollectionLazy(this IWorkbenchProject project, bool lazy)
		{
			project.SetUserOption("DefaultCollectionLazy", lazy);
		}

		public static bool GetProjectDefaultCollectionLazy(this IWorkbenchProject project)
		{
			object value = project.GetUserOption("DefaultCollectionLazy");

			if (value is string)
				return bool.Parse((string)value);
			else
				return (bool)value;
		}
		#endregion

		#region Project - DefaultInverse

		public static void SetProjectDefaultInverse(this IWorkbenchProject project, bool inverse)
		{
			project.SetUserOption("DefaultInverse", inverse);
		}

		public static bool GetProjectDefaultInverse(this IWorkbenchProject project)
		{
			object value = project.GetUserOption("DefaultInverse");

			if (value is string)
				return bool.Parse((string)value);
			else
				return (bool)value;
		}
		#endregion

		#region Entity - DefaultCascade

		public static void SetEntityDefaultCascade(this Entity entity, ArchAngel.Interfaces.NHibernateEnums.CascadeTypes cascade)
		{
			entity.SetUserOption("Entity_DefaultCascade", cascade);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.CascadeTypes GetEntityDefaultCascade(this Entity entity)
		{
			object value = entity.GetUserOptionValue("Entity_DefaultCascade");

			if (value is string)
				return (ArchAngel.Interfaces.NHibernateEnums.CascadeTypes)Enum.Parse(typeof(ArchAngel.Interfaces.NHibernateEnums.CascadeTypes), (string)value);
			else
				return (ArchAngel.Interfaces.NHibernateEnums.CascadeTypes)value;
		}
		#endregion

		#region Entity - Lazy

		public static void SetEntityLazy(this Entity entity, bool lazy)
		{
			ArchAngel.Interfaces.NHibernateEnums.EntityLazyTypes lazyType = lazy ? ArchAngel.Interfaces.NHibernateEnums.EntityLazyTypes.@true : ArchAngel.Interfaces.NHibernateEnums.EntityLazyTypes.@false;
			entity.SetUserOption("Entity_Lazy", lazyType);
		}

		public static void SetEntityLazy(this Entity entity, ArchAngel.Interfaces.NHibernateEnums.EntityLazyTypes lazy)
		{
			entity.SetUserOption("Entity_Lazy", lazy);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.EntityLazyTypes GetEntityLazy(this Entity entity)
		{
			object value = entity.GetUserOptionValue("Entity_Lazy");

			if (value is string)
				return (ArchAngel.Interfaces.NHibernateEnums.EntityLazyTypes)Enum.Parse(typeof(ArchAngel.Interfaces.NHibernateEnums.EntityLazyTypes), (string)value);
			else
				return (ArchAngel.Interfaces.NHibernateEnums.EntityLazyTypes)value;
		}
		#endregion

		#region Entity - DefaultCollectionLazy

		public static void SetEntityDefaultCollectionLazy(this Entity entity, ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes lazy)
		{
			entity.SetUserOption("Entity_DefaultCollectionLazy", lazy);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes GetEntityDefaultCollectionLazy(this Entity entity)
		{
			object value = entity.GetUserOptionValue("Entity_DefaultCollectionLazy");

			if (value is string)
				return (ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes)Enum.Parse(typeof(ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes), (string)value);
			else
				return (ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes)value;
		}
		#endregion

		#region Reference - CollectionCascade

		public static void SetReferenceEnd1CollectionCascade(this Reference reference, ArchAngel.Interfaces.NHibernateEnums.CollectionCascadeTypes cascade)
		{
			reference.SetUserOption("Reference_End1CollectionCascade", cascade);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.CollectionCascadeTypes GetReferenceEnd1CollectionCascade(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End1CollectionCascade"))
				return reference.GetUserOptionValue<ArchAngel.Interfaces.NHibernateEnums.CollectionCascadeTypes>("Reference_End1CollectionCascade");

			return ArchAngel.Interfaces.NHibernateEnums.CollectionCascadeTypes.inherit_default;
		}

		public static void SetReferenceEnd2CollectionCascade(this Reference reference, ArchAngel.Interfaces.NHibernateEnums.CollectionCascadeTypes cascade)
		{
			reference.SetUserOption("Reference_End2CollectionCascade", cascade);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.CollectionCascadeTypes GetReferenceEnd2CollectionCascade(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End2CollectionCascade"))
				return reference.GetUserOptionValue<ArchAngel.Interfaces.NHibernateEnums.CollectionCascadeTypes>("Reference_End2CollectionCascade");

			return ArchAngel.Interfaces.NHibernateEnums.CollectionCascadeTypes.inherit_default;
		}
		#endregion

		#region Entity - Batch Size
		public static void SetEntityBatchSize(this Entity entity, int batchSize)
		{
			entity.SetUserOption("Entity_BatchSize", batchSize);
		}

		public static int GetEntityBatchSize(this Entity entity)
		{
			if (entity.HasUserOption("Entity_BatchSize"))
				return entity.GetUserOptionValue<int>("Entity_BatchSize");

			return 1;
		}
		#endregion

		#region Entity - Proxy
		public static void SetEntityProxy(this Entity entity, string proxy)
		{
			entity.SetUserOption("Entity_Proxy", proxy);
		}

		public static string GetEntityProxy(this Entity entity)
		{
			if (entity.HasUserOption("Entity_Proxy"))
				return entity.GetUserOptionValue<string>("Entity_Proxy");

			return "";
		}

		public static ArchAngel.Interfaces.Scripting.NHibernate.Model.ICache GetCache(this Entity entity)
		{
			ArchAngel.Interfaces.Scripting.NHibernate.Model.ICache cache = new Interfaces.Scripting.NHibernate.Model.ICache()
			{
				Include = (CacheIncludeTypes)Enum.Parse(typeof(CacheIncludeTypes), entity.Cache.Include.ToString(), true),
				Region = entity.Cache.Region,
				Usage = (CacheUsageTypes)Enum.Parse(typeof(CacheUsageTypes), entity.Cache.Usage.ToString(), true)
			};
			return cache;
		}

		#endregion

		#region Entity - Select Before Update
		public static void SetEntitySelectBeforeUpdate(this Entity entity, bool selectBeforeUpdate)
		{
			entity.SetUserOption("Entity_SelectBeforeUpdate", selectBeforeUpdate);
		}

		public static bool GetEntitySelectBeforeUpdate(this Entity entity)
		{
			if (entity.HasUserOption("Entity_SelectBeforeUpdate"))
				return entity.GetUserOptionValue<bool>("Entity_SelectBeforeUpdate");

			return false;
		}
		#endregion

		#region Reference - OrderByProperty

		public static void SetReferenceEnd1OrderByProperty(this Reference reference, string propertyName)
		{
			reference.SetUserOption("Reference_End1OrderByProperty", propertyName);
		}

		public static Property GetReferenceEnd1OrderByProperty(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End1OrderByProperty"))
				return reference.GetUserOptionValue<Property>("Reference_End1OrderByProperty");

			return null;
		}

		public static void SetReferenceEnd2OrderByProperty(this Reference reference, string propertyName)
		{
			reference.SetUserOption("Reference_End2OrderByProperty", propertyName);
		}

		public static Property GetReferenceEnd2OrderByProperty(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End2OrderByProperty"))
				return reference.GetUserOptionValue<Property>("Reference_End2OrderByProperty");

			return null;
		}
		#endregion

		#region Reference - OrderByDirection

		public static void SetReferenceEnd1OrderByIsAsc(this Reference reference, bool isAsc)
		{
			reference.SetUserOption("Reference_End1OrderByIsAsc", isAsc);
		}

		public static bool GetReferenceEnd1OrderByIsAsc(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End1OrderByIsAsc"))
				return reference.GetUserOptionValue<bool>("Reference_End1OrderByIsAsc");

			return true;
		}

		public static void SetReferenceEnd2OrderByIsAsc(this Reference reference, bool isAsc)
		{
			reference.SetUserOption("Reference_End2OrderByIsAsc", isAsc);
		}

		public static bool GetReferenceEnd2OrderByIsAsc(this Reference reference)
		{
			if (reference.HasUserOption("Reference_End2OrderByIsAsc"))
				return reference.GetUserOptionValue<bool>("Reference_End2OrderByIsAsc");

			return true;
		}
		#endregion
	}
}