using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ArchAngel.Workbench.ProjectOptions.TypeMappings
{
	public class Utility
	{
		private readonly static string DEFAULT_TYPE_MAP_XML = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Workbench.ProjectOptions.TypeMappings.DefaultTypeMap.xml")).ReadToEnd();
		private readonly static string USER_FILE = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Branding.FormTitle + Path.DirectorySeparatorChar + "Settings"), "Custom Type Maps.xml");
		private static bool SaveUnmodified = false;
		public static List<UniType> UniTypes = new List<UniType>();
		public static List<DatabaseTypeMap> SqlServerTypes = new List<DatabaseTypeMap>();
		public static List<DatabaseTypeMap> OracleTypes = new List<DatabaseTypeMap>();
		public static List<DatabaseTypeMap> MySqlTypes = new List<DatabaseTypeMap>();
		public static List<DatabaseTypeMap> PostgreSqlTypes = new List<DatabaseTypeMap>();
		public static List<DatabaseTypeMap> FirebirdTypes = new List<DatabaseTypeMap>();

		public static void LoadSettings()
		{
			UniTypes.Clear();
			SqlServerTypes.Clear();
			OracleTypes.Clear();
			MySqlTypes.Clear();
			PostgreSqlTypes.Clear();
			FirebirdTypes.Clear();

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

			foreach (XmlNode node in doc.SelectNodes(@"type-maps/unitypes/unitype"))
			{
				string name = node.Attributes["name"].Value;
				string csharpType = node.Attributes["csharp-type"].Value;

				UniType uniType = UniTypes.SingleOrDefault(u => u.Name.ToLowerInvariant() == name.ToLowerInvariant());

				if (uniType != null)
					uniType.CSharpType = csharpType;
				else
					UniTypes.Add(new UniType(name, csharpType));
			}
			AddMap(SqlServerTypes, doc.SelectNodes(@"type-maps/sql-server-maps/map"));
			AddMap(OracleTypes, doc.SelectNodes(@"type-maps/oracle-maps/map"));
			AddMap(MySqlTypes, doc.SelectNodes(@"type-maps/mysql-maps/map"));
			AddMap(PostgreSqlTypes, doc.SelectNodes(@"type-maps/postgresql-maps/map"));
			AddMap(FirebirdTypes, doc.SelectNodes(@"type-maps/firebird-maps/map"));
		}

		private static void AddMap(List<DatabaseTypeMap> mapCollection, XmlNodeList nodes)
		{
			foreach (XmlNode node in nodes)
			{
				string typeName = node.Attributes["type"].Value;
				string uniTypeName = node.Attributes["unitype"].Value;

				UniType uniType = UniTypes.SingleOrDefault(t => t.Name.ToLowerInvariant() == uniTypeName.ToLowerInvariant());
				DatabaseTypeMap map = mapCollection.SingleOrDefault(c => c.TypeName.ToLowerInvariant() == typeName.ToLowerInvariant());

				if (map != null)
					map.UniType = uniType;
				else
					mapCollection.Add(new DatabaseTypeMap(typeName, uniType));
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
			XmlNode uniTypesNode = doc.CreateElement("unitypes");
			root.AppendChild(uniTypesNode);
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

			bool changesExist = false;

			foreach (UniType uniType in UniTypes.OrderBy(u => u.Name))
			{
				if (SaveUnmodified || uniType.IsModified)
				{
					changesExist = true;
					XmlNode uniTypeNode = doc.CreateElement("unitype");

					XmlAttribute att = doc.CreateAttribute("name");
					att.Value = uniType.Name;
					uniTypeNode.Attributes.Append(att);

					att = doc.CreateAttribute("csharp-type");
					att.Value = uniType.CSharpType;
					uniTypeNode.Attributes.Append(att);

					uniTypesNode.AppendChild(uniTypeNode);
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

			if (changesExist)
			{
				string dir = Path.GetDirectoryName(USER_FILE);

				if (!Directory.Exists(dir))
					Directory.CreateDirectory(dir);

				doc.Save(USER_FILE);
			}
		}

		private static bool CreateMapNode(XmlDocument doc, XmlNode sqlServerMapsNode, bool changesExist, DatabaseTypeMap dbType)
		{
			if (SaveUnmodified || dbType.IsModified)
			{
				changesExist = true;
				XmlNode mapNode = doc.CreateElement("map");

				XmlAttribute att = doc.CreateAttribute("type");
				att.Value = dbType.TypeName;
				mapNode.Attributes.Append(att);

				att = doc.CreateAttribute("unitype");
				att.Value = dbType.UniType == null ? "" : dbType.UniType.Name;
				mapNode.Attributes.Append(att);

				sqlServerMapsNode.AppendChild(mapNode);
			}
			return changesExist;
		}
	}
}
