using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ArchAngel.Interfaces.ProjectOptions.TypeMappings
{
	public class Utility
	{
		public enum DatabaseTypes
		{
			SqlServer,
			Oracle,
			MySql,
			PostgreSql,
			Firebird,
			SQLite
		}
		public struct ColumnInfo
		{
			public string Name { get; set; }
			public string TypeName { get; set; }
			public bool IsNullable { get; set; }
			public int Precision { get; set; }
			public int Scale { get; set; }
			public long Size { get; set; }
		}
		private readonly static string DEFAULT_TYPE_MAP_XML = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Interfaces.ProjectOptions.TypeMappings.DefaultTypeMap.xml")).ReadToEnd();
		private readonly static string USER_FILE = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Visual NHibernate" + Path.DirectorySeparatorChar + "Settings"), "Custom Type Maps.xml");
		private static bool SaveUnmodified = false;
		public static List<DotNetType> DotNetTypes = new List<DotNetType>();
		public static List<DatabaseTypeMap> SqlServerTypes = new List<DatabaseTypeMap>();
		public static List<DatabaseTypeMap> OracleTypes = new List<DatabaseTypeMap>();
		public static List<DatabaseTypeMap> MySqlTypes = new List<DatabaseTypeMap>();
		public static List<DatabaseTypeMap> PostgreSqlTypes = new List<DatabaseTypeMap>();
		public static List<DatabaseTypeMap> FirebirdTypes = new List<DatabaseTypeMap>();
		public static List<DatabaseTypeMap> SQLiteTypes = new List<DatabaseTypeMap>();
		internal static DateTime TimeOfLastScriptChange = DateTime.Now;

		private static string _PostProcessSciptSqlServer;
		private static string _PostProcessSciptOracle;
		private static string _PostProcessSciptMySql;
		private static string _PostProcessSciptPostgreSql;
		private static string _PostProcessSciptFirebird;
		private static string _PostProcessSciptSQLite;

		private static string _OriginalPostProcessSciptSqlServer = null;
		private static string _OriginalPostProcessSciptOracle = null;
		private static string _OriginalPostProcessSciptMySql = null;
		private static string _OriginalPostProcessSciptPostgreSql = null;
		private static string _OriginalPostProcessSciptFirebird = null;
		private static string _OriginalPostProcessSciptSQLite = null;

		public static string PostProcessSciptSqlServer
		{
			get { return _PostProcessSciptSqlServer; }
			set
			{
				_PostProcessSciptSqlServer = value;
				TimeOfLastScriptChange = DateTime.Now;

				if (_OriginalPostProcessSciptSqlServer == null)
					_OriginalPostProcessSciptSqlServer = value;
			}
		}

		public static string PostProcessSciptOracle
		{
			get { return _PostProcessSciptOracle; }
			set
			{
				_PostProcessSciptOracle = value;
				TimeOfLastScriptChange = DateTime.Now;

				if (_OriginalPostProcessSciptOracle == null)
					_OriginalPostProcessSciptOracle = value;
			}
		}

		public static string PostProcessSciptMySql
		{
			get { return _PostProcessSciptMySql; }
			set
			{
				_PostProcessSciptMySql = value;
				TimeOfLastScriptChange = DateTime.Now;

				if (_OriginalPostProcessSciptMySql == null)
					_OriginalPostProcessSciptMySql = value;
			}
		}

		public static string PostProcessSciptPostgreSql
		{
			get { return _PostProcessSciptPostgreSql; }
			set
			{
				_PostProcessSciptPostgreSql = value;
				TimeOfLastScriptChange = DateTime.Now;

				if (_OriginalPostProcessSciptPostgreSql == null)
					_OriginalPostProcessSciptPostgreSql = value;
			}
		}

		public static string PostProcessSciptFirebird
		{
			get { return _PostProcessSciptFirebird; }
			set
			{
				_PostProcessSciptFirebird = value;
				TimeOfLastScriptChange = DateTime.Now;

				if (_OriginalPostProcessSciptFirebird == null)
					_OriginalPostProcessSciptFirebird = value;
			}
		}

		public static string PostProcessSciptSQLite
		{
			get { return _PostProcessSciptSQLite; }
			set
			{
				_PostProcessSciptSQLite = value;
				TimeOfLastScriptChange = DateTime.Now;

				if (_OriginalPostProcessSciptSQLite == null)
					_OriginalPostProcessSciptSQLite = value;
			}
		}

		public static List<DatabaseTypeMap> GetDatabaseTypes(string databaseTypeName)
		{
			List<DatabaseTypeMap> dbTypes;

			switch (databaseTypeName)
			{
				case "SQLServer2005":
				case "SQLServerExpress":
				case "SQLCE":
					dbTypes = SqlServerTypes;
					break;
				case "Oracle":
					dbTypes = OracleTypes;
					break;
				case "MySQL":
					dbTypes = MySqlTypes;
					break;
				case "PostgreSQL":
					dbTypes = PostgreSqlTypes;
					break;
				case "Firebird":
					dbTypes = FirebirdTypes;
					break;
				case "SQLite":
					dbTypes = SQLiteTypes;
					break;
				default: throw new NotImplementedException("Database type not handled yet: " + databaseTypeName);
			}
			return dbTypes.OrderBy(t => t.TypeName).ToList();
		}

		public static string GetDefaultDatabaseType(string databaseTypeName, string dotNetType)
		{
			switch (databaseTypeName)
			{
				case "SQLServer2005":
				case "SQLServerExpress":
				case "SQLCE":
					return DotNetTypes.Single(d => d.Name == dotNetType).SqlServerName;
				case "Oracle": return DotNetTypes.Single(d => d.Name == dotNetType).OracleName;
				case "MySQL": return DotNetTypes.Single(d => d.Name == dotNetType).MySqlName;
				case "PostgreSQL": return DotNetTypes.Single(d => d.Name == dotNetType).PostgreSqlName;
				case "Firebird": return DotNetTypes.Single(d => d.Name == dotNetType).FirebirdName;
				case "SQLite": return DotNetTypes.Single(d => d.Name == dotNetType).SQLiteName;
				default: throw new NotImplementedException("Database type not handled yet: " + databaseTypeName);
			}
		}

		public static DotNetType GetDotNetType(string databaseTypeName, ColumnInfo column)
		{
			DotNetType type;
			DotNetType processedType = null;
			string processedName;

			switch (databaseTypeName)
			{
				case "SQLServer2005":
				case "SQLServerExpress":
				case "SQLCE":
					type = SqlServerTypes.GetDotNetType(column.TypeName, DatabaseTypes.SqlServer);
					processedName = Scripts.PostProcessSqlServerType(column, type.Name);
					break;
				case "Oracle":
					type = OracleTypes.GetDotNetType(column.TypeName, DatabaseTypes.Oracle);
					processedName = Scripts.PostProcessOracleType(column, type.Name);
					break;
				case "MySQL":
					type = MySqlTypes.GetDotNetType(column.TypeName, DatabaseTypes.MySql);
					processedName = Scripts.PostProcessMySqlType(column, type.Name);
					break;
				case "PostgreSQL":
					type = PostgreSqlTypes.GetDotNetType(column.TypeName, DatabaseTypes.PostgreSql);
					processedName = Scripts.PostProcessPostgreSqlType(column, type.Name);
					break;
				case "Firebird":
					type = FirebirdTypes.GetDotNetType(column.TypeName, DatabaseTypes.Firebird);
					processedName = Scripts.PostProcessFirebirdType(column, type.Name);
					break;
				case "SQLite":
					type = SQLiteTypes.GetDotNetType(column.TypeName, DatabaseTypes.SQLite);
					processedName = Scripts.PostProcessSQLiteType(column, type.Name);
					break;
				default: throw new NotImplementedException("Database type not handled yet: " + databaseTypeName);
			}
			processedType = DotNetTypes.SingleOrDefault(t => t.Name.ToLowerInvariant() == processedName.ToLowerInvariant());

			if (processedType == null)
				throw new Exception(string.Format("Post-processing script returned null for type [{0}]", type.Name));

			return processedType;
		}

		public static string GetDotNetTypeName(string databaseTypeName, ColumnInfo column)
		{
			return GetDotNetType(databaseTypeName, column).Name;
		}

		public static string GetCSharpTypeName(string databaseTypeName, ColumnInfo column)
		{
			return GetDotNetType(databaseTypeName, column).CSharpName;
		}

		public static string GetVbTypeName(string databaseTypeName, ColumnInfo column)
		{
			return GetDotNetType(databaseTypeName, column).VbName;
		}

		public static void LoadSettings()
		{
			DotNetTypes.Clear();
			SqlServerTypes.Clear();
			OracleTypes.Clear();
			MySqlTypes.Clear();
			PostgreSqlTypes.Clear();
			FirebirdTypes.Clear();
			SQLiteTypes.Clear();

			// Load the default settings
			LoadSettings(DEFAULT_TYPE_MAP_XML);
			// Load user-customised settings

			if (File.Exists(USER_FILE))
				LoadSettings(File.ReadAllText(USER_FILE));
		}

		private static void LoadSettings(string xml)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			foreach (XmlNode node in doc.SelectNodes(@"type-maps/dotnet-types/dotnet"))
			{
				string name = node.Attributes["name"].Value;
				string csharpType = node.Attributes["csharp"].Value;
				string vbType = node.Attributes["vb"].Value;
				string sqlServer = node.Attributes["sqlserver"].Value;
				string oracle = node.Attributes["oracle"].Value;
				string mysql = node.Attributes["mysql"].Value;
				string postgresql = node.Attributes["postgresql"].Value;
				string firebird = node.Attributes["firebird"].Value;
				string sqlite = node.Attributes["sqlite"] != null ? node.Attributes["sqlite"].Value : "";

				DotNetType dotNetType = DotNetTypes.SingleOrDefault(u => u.Name.ToLowerInvariant() == name.ToLowerInvariant());

				if (dotNetType != null)
				{
					dotNetType.CSharpName = csharpType;
					dotNetType.VbName = csharpType;
					dotNetType.SqlServerName = sqlServer;
					dotNetType.OracleName = oracle;
					dotNetType.MySqlName = mysql;
					dotNetType.PostgreSqlName = postgresql;
					dotNetType.FirebirdName = firebird;
					dotNetType.SQLiteName = sqlite;
				}
				else
					DotNetTypes.Add(new DotNetType(name, csharpType, vbType, sqlServer, oracle, mysql, postgresql, firebird, sqlite));
			}
			AddMap(SqlServerTypes, doc.SelectNodes(@"type-maps/sql-server-maps/map"));
			AddMap(OracleTypes, doc.SelectNodes(@"type-maps/oracle-maps/map"));
			AddMap(MySqlTypes, doc.SelectNodes(@"type-maps/mysql-maps/map"));
			AddMap(PostgreSqlTypes, doc.SelectNodes(@"type-maps/postgresql-maps/map"));
			AddMap(FirebirdTypes, doc.SelectNodes(@"type-maps/firebird-maps/map"));
			AddMap(SQLiteTypes, doc.SelectNodes(@"type-maps/sqlite-maps/map"));

			if (doc.SelectSingleNode(@"type-maps/scripts/sqlserver") != null)
				PostProcessSciptSqlServer = doc.SelectSingleNode(@"type-maps/scripts/sqlserver").InnerText;

			if (doc.SelectSingleNode(@"type-maps/scripts/oracle") != null)
				PostProcessSciptOracle = doc.SelectSingleNode(@"type-maps/scripts/oracle").InnerText;

			if (doc.SelectSingleNode(@"type-maps/scripts/mysql") != null)
				PostProcessSciptMySql = doc.SelectSingleNode(@"type-maps/scripts/mysql").InnerText;

			if (doc.SelectSingleNode(@"type-maps/scripts/postgresql") != null)
				PostProcessSciptPostgreSql = doc.SelectSingleNode(@"type-maps/scripts/postgresql").InnerText;

			if (doc.SelectSingleNode(@"type-maps/scripts/firebird") != null)
				PostProcessSciptFirebird = doc.SelectSingleNode(@"type-maps/scripts/firebird").InnerText;

			if (doc.SelectSingleNode(@"type-maps/scripts/sqlite") != null)
				PostProcessSciptSQLite = doc.SelectSingleNode(@"type-maps/scripts/sqlite").InnerText;
		}

		private static void AddMap(List<DatabaseTypeMap> mapCollection, XmlNodeList nodes)
		{
			foreach (XmlNode node in nodes)
			{
				string typeName = node.Attributes["type"].Value;
				string dotnetTypeName = node.Attributes["dotnet"].Value;

				DotNetType dotnetType = DotNetTypes.SingleOrDefault(t => t.Name.ToLowerInvariant() == dotnetTypeName.ToLowerInvariant());
				DatabaseTypeMap map = mapCollection.SingleOrDefault(c => c.TypeName.ToLowerInvariant() == typeName.ToLowerInvariant());

				if (map != null)
					map.DotNetType = dotnetType;
				else
					mapCollection.Add(new DatabaseTypeMap(typeName, dotnetType));
			}
		}

		/// <summary>
		/// Save the customised settings
		/// </summary>
		public static void SaveSettings()
		{
			XmlDocument doc = new XmlDocument();

			XmlNode root = doc.CreateElement("type-maps");
			doc.AppendChild(root);
			XmlNode dotnetTypesNode = doc.CreateElement("dotnet-types");
			root.AppendChild(dotnetTypesNode);
			XmlNode sqlServerMapsNode = doc.CreateElement("sql-server-maps");
			root.AppendChild(sqlServerMapsNode);

			XmlNode oracleMapsNode = doc.CreateElement("oracle-maps");
			root.AppendChild(oracleMapsNode);

			XmlNode mySqlMapsNode = doc.CreateElement("mysql-maps");
			root.AppendChild(mySqlMapsNode);

			XmlNode postgreSqlMapsNode = doc.CreateElement("postgresql-maps");
			root.AppendChild(postgreSqlMapsNode);

			XmlNode firebirdMapsNode = doc.CreateElement("firebird-maps");
			root.AppendChild(firebirdMapsNode);

			XmlNode sqliteMapsNode = doc.CreateElement("sqlite-maps");
			root.AppendChild(sqliteMapsNode);

			bool changesExist = false;

			foreach (DotNetType dotNetType in DotNetTypes.OrderBy(u => u.Name))
			{
				if (SaveUnmodified || dotNetType.IsModified)
				{
					changesExist = true;
					XmlNode dotnetTypeNode = doc.CreateElement("dotnet");

					AddAttribute(doc, dotnetTypeNode, "name", dotNetType.Name);
					AddAttribute(doc, dotnetTypeNode, "csharp", dotNetType.CSharpName);
					AddAttribute(doc, dotnetTypeNode, "vb", dotNetType.VbName);
					AddAttribute(doc, dotnetTypeNode, "sqlserver", dotNetType.SqlServerName);
					AddAttribute(doc, dotnetTypeNode, "oracle", dotNetType.OracleName);
					AddAttribute(doc, dotnetTypeNode, "mysql", dotNetType.MySqlName);
					AddAttribute(doc, dotnetTypeNode, "postgresql", dotNetType.PostgreSqlName);
					AddAttribute(doc, dotnetTypeNode, "firebird", dotNetType.FirebirdName);
					AddAttribute(doc, dotnetTypeNode, "sqlite", dotNetType.SQLiteName);

					dotnetTypesNode.AppendChild(dotnetTypeNode);
				}
			}
			foreach (DatabaseTypeMap dbType in SqlServerTypes.Where(o => !string.IsNullOrEmpty(o.TypeName)).OrderBy(o => o.TypeName))
				changesExist = CreateMapNode(doc, sqlServerMapsNode, changesExist, dbType);

			foreach (DatabaseTypeMap dbType in OracleTypes.Where(o => !string.IsNullOrEmpty(o.TypeName)).OrderBy(o => o.TypeName))
				changesExist = CreateMapNode(doc, oracleMapsNode, changesExist, dbType);

			foreach (DatabaseTypeMap dbType in MySqlTypes.Where(o => !string.IsNullOrEmpty(o.TypeName)).OrderBy(o => o.TypeName))
				changesExist = CreateMapNode(doc, mySqlMapsNode, changesExist, dbType);

			foreach (DatabaseTypeMap dbType in PostgreSqlTypes.Where(o => !string.IsNullOrEmpty(o.TypeName)).OrderBy(o => o.TypeName))
				changesExist = CreateMapNode(doc, postgreSqlMapsNode, changesExist, dbType);

			foreach (DatabaseTypeMap dbType in FirebirdTypes.Where(o => !string.IsNullOrEmpty(o.TypeName)).OrderBy(o => o.TypeName))
				changesExist = CreateMapNode(doc, firebirdMapsNode, changesExist, dbType);

			foreach (DatabaseTypeMap dbType in SQLiteTypes.Where(o => !string.IsNullOrEmpty(o.TypeName)).OrderBy(o => o.TypeName))
				changesExist = CreateMapNode(doc, sqliteMapsNode, changesExist, dbType);

			XmlNode scriptsNode = doc.CreateElement("scripts");
			root.AppendChild(scriptsNode);

			if (PostProcessSciptSqlServer != _OriginalPostProcessSciptSqlServer)
			{
				changesExist = true;
				CreateScriptNode(doc, scriptsNode, "sqlserver", PostProcessSciptSqlServer);
			}
			if (PostProcessSciptOracle != _OriginalPostProcessSciptOracle)
			{
				changesExist = true;
				CreateScriptNode(doc, scriptsNode, "oracle", PostProcessSciptOracle);
			}
			if (PostProcessSciptMySql != _OriginalPostProcessSciptMySql)
			{
				changesExist = true;
				CreateScriptNode(doc, scriptsNode, "mysql", PostProcessSciptMySql);
			}
			if (PostProcessSciptPostgreSql != _OriginalPostProcessSciptPostgreSql)
			{
				changesExist = true;
				CreateScriptNode(doc, scriptsNode, "postgresql", PostProcessSciptPostgreSql);
			}
			if (PostProcessSciptFirebird != _OriginalPostProcessSciptFirebird)
			{
				changesExist = true;
				CreateScriptNode(doc, scriptsNode, "firebird", PostProcessSciptFirebird);
			}
			if (PostProcessSciptSQLite != _OriginalPostProcessSciptSQLite)
			{
				changesExist = true;
				CreateScriptNode(doc, scriptsNode, "sqlite", PostProcessSciptSQLite);
			}
			if (changesExist)
			{
				string dir = Path.GetDirectoryName(USER_FILE);

				if (!Directory.Exists(dir))
					Directory.CreateDirectory(dir);

				doc.Save(USER_FILE);
			}
		}

		private static XmlAttribute AddAttribute(XmlDocument doc, XmlNode node, string name, string value)
		{
			XmlAttribute att = doc.CreateAttribute(name);
			att.Value = value;
			node.Attributes.Append(att);
			return att;
		}

		private static bool CreateMapNode(XmlDocument doc, XmlNode sqlServerMapsNode, bool changesExist, DatabaseTypeMap dbType)
		{
			if (SaveUnmodified || dbType.IsModified)
			{
				changesExist = true;
				string dotnetTypeName = dbType.DotNetType == null ? "" : dbType.DotNetType.Name;

				XmlNode mapNode = doc.CreateElement("map");
				AddAttribute(doc, mapNode, "type", dbType.TypeName);
				AddAttribute(doc, mapNode, "dotnet", dotnetTypeName);
				sqlServerMapsNode.AppendChild(mapNode);
			}
			return changesExist;
		}

		private static void CreateScriptNode(XmlDocument doc, XmlNode scriptsNode, string name, string script)
		{
			XmlNode node = doc.CreateElement(name);
			XmlCDataSection cdata = doc.CreateCDataSection(script);
			node.AppendChild(cdata);
			scriptsNode.AppendChild(node);
		}
	}
}
