using System;
using System.Collections.Generic;
using System.Linq;

namespace ArchAngel.Interfaces.ProjectOptions.TypeMappings
{
	public static class ExtensionMethods
	{
		private static DotNetType GetDotNetTypeinternalFromDatabaseType(List<DatabaseTypeMap> maps, string typeName, Utility.DatabaseTypes databaseType)
		{
			DatabaseTypeMap map = maps.SingleOrDefault(m => m.TypeName.ToLowerInvariant() == typeName.ToLowerInvariant());

			if (map != null)
				return map.DotNetType;

			FormCustomType form = new FormCustomType(typeName, databaseType);
			form.ShowDialog();
			return form.DotNetType;
		}

		private static DotNetType GetDotNetTypeinternalFromDotNetType(List<DatabaseTypeMap> maps, string dotNetTypeName, Utility.DatabaseTypes databaseType)
		{
			DatabaseTypeMap map = maps.SingleOrDefault(m => m.DotNetType.Name.ToLowerInvariant() == dotNetTypeName.ToLowerInvariant());

			if (map != null)
				return map.DotNetType;

			FormCustomType form = new FormCustomType(dotNetTypeName, databaseType);
			form.ShowDialog();
			return form.DotNetType;
		}

		public static DotNetType GetDotNetType(this List<DatabaseTypeMap> maps, string typeName, Utility.DatabaseTypes databaseType)
		{
			DotNetType t = GetDotNetTypeinternalFromDatabaseType(maps, typeName, databaseType);
			return t;
		}

		public static string GetCSharpType(this List<DatabaseTypeMap> maps, string typeName, Utility.DatabaseTypes databaseType)
		{
			DotNetType t = GetDotNetTypeinternalFromDatabaseType(maps, typeName, databaseType);
			return t == null ? null : t.CSharpName;
		}

		public static string GetVbType(this List<DatabaseTypeMap> maps, string typeName, Utility.DatabaseTypes databaseType)
		{
			DotNetType t = GetDotNetTypeinternalFromDatabaseType(maps, typeName, databaseType);
			return t == null ? null : t.VbName;
		}

		public static string GetDefaultDatabaseType(this List<DatabaseTypeMap> maps, Utility.DatabaseTypes dbType, string dotnetTypeName)
		{
			DotNetType dotNetType = GetDotNetTypeinternalFromDotNetType(maps, dotnetTypeName, dbType);

			if (dotNetType == null)
				return null;

			switch (dbType)
			{
				case Utility.DatabaseTypes.Firebird: return dotNetType.FirebirdName;
				case Utility.DatabaseTypes.MySql: return dotNetType.MySqlName;
				case Utility.DatabaseTypes.Oracle: return dotNetType.OracleName;
				case Utility.DatabaseTypes.PostgreSql: return dotNetType.PostgreSqlName;
				case Utility.DatabaseTypes.SqlServer: return dotNetType.SqlServerName;
				default: throw new NotImplementedException("Database type not handled yet: " + dbType.ToString());
			}
		}
	}
}
