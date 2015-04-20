
using System;
using System.IO;
using System.Xml;
namespace ArchAngel.Interfaces.ProjectOptions.DatabaseScripts
{
	public class Utility
	{
		private readonly static string DEFAULT_SCRIPTS_XML = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.xml")).ReadToEnd();
		private readonly static string USER_FILE = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Visual NHibernate" + Path.DirectorySeparatorChar + "Settings"), "Database Scripts.xml");

		internal static DateTime TimeOfLastScriptChange = DateTime.Now;

		public static MaintenanceScript SqlServerScript { get; set; }
		public static MaintenanceScript OracleScript { get; set; }
		public static MaintenanceScript MySqlScript { get; set; }
		public static MaintenanceScript PostgreSqlScript { get; set; }
		public static MaintenanceScript FirebirdScript { get; set; }
		public static MaintenanceScript SQLiteScript { get; set; }

		//public string GetHeaderScript(TypeMappings.Utility.DatabaseTypes databaseType)
		//{
		//    switch (databaseType)
		//    {

		//        default:
		//            throw new NotImplementedException("Database type not handled yet: " + databaseType.ToString());
		//    }
		//}

		public static void LoadSettings()
		{
			// Load the default settings
			LoadSettings(DEFAULT_SCRIPTS_XML);
			// Load user-customised settings

			if (File.Exists(USER_FILE))
				LoadSettings(File.ReadAllText(USER_FILE));
		}

		private static void LoadSettings(string xml)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			if (SqlServerScript == null)
				SqlServerScript = ReadScripts(doc, "sqlserver");
			else
				UpdateMaintenanceScript(doc, SqlServerScript, "sqlserver");

			if (OracleScript == null)
				OracleScript = ReadScripts(doc, "oracle");
			else
				UpdateMaintenanceScript(doc, OracleScript, "oracle");

			if (MySqlScript == null)
				MySqlScript = ReadScripts(doc, "mysql");
			else
				UpdateMaintenanceScript(doc, MySqlScript, "mysql");

			if (PostgreSqlScript == null)
				PostgreSqlScript = ReadScripts(doc, "postgresql");
			else
				UpdateMaintenanceScript(doc, PostgreSqlScript, "postgresql");

			if (FirebirdScript == null)
				FirebirdScript = ReadScripts(doc, "firebird");
			else
				UpdateMaintenanceScript(doc, FirebirdScript, "firebird");

			if (SQLiteScript == null)
				SQLiteScript = ReadScripts(doc, "sqlite");
			else
				UpdateMaintenanceScript(doc, SQLiteScript, "sqlite");
		}

		private static void UpdateMaintenanceScript(XmlDocument doc, MaintenanceScript script, string name)
		{
			MaintenanceScript userScript = ReadScripts(doc, name);

			if (userScript == null)
				return;

			script.Header = userScript.Header;
			script.Create = userScript.Create;
			script.Update = userScript.Update;
			script.Delete = userScript.Delete;
		}

		private static MaintenanceScript ReadScripts(XmlDocument doc, string elementName)
		{
			XmlNode node = doc.SelectSingleNode(string.Format(@"database-scripts/{0}", elementName));

			if (node == null)
				return null;

			return new MaintenanceScript(node.SelectSingleNode("header").InnerText, "", "", "");

			//return new MaintenanceScript(
			//    node.SelectSingleNode("header").InnerText,
			//    node.SelectSingleNode("create").InnerText,
			//    node.SelectSingleNode("update").InnerText,
			//    node.SelectSingleNode("delete").InnerText);
		}

		/// <summary>
		/// Save the customised settings
		/// </summary>
		public static void SaveSettings()
		{
			bool changesExist = SqlServerScript.IsModified ||
				OracleScript.IsModified ||
				MySqlScript.IsModified ||
				PostgreSqlScript.IsModified ||
				FirebirdScript.IsModified ||
				SQLiteScript.IsModified;

			if (changesExist)
			{
				XmlDocument doc = new XmlDocument();

				XmlNode root = doc.CreateElement("database-scripts");
				doc.AppendChild(root);

				if (SqlServerScript.IsModified)
					root.AppendChild(CreateScriptNode(doc, SqlServerScript, "sqlserver"));

				if (OracleScript.IsModified)
					root.AppendChild(CreateScriptNode(doc, OracleScript, "oracle"));

				if (MySqlScript.IsModified)
					root.AppendChild(CreateScriptNode(doc, MySqlScript, "mysql"));

				if (PostgreSqlScript.IsModified)
					root.AppendChild(CreateScriptNode(doc, PostgreSqlScript, "postgresql"));

				if (FirebirdScript.IsModified)
					root.AppendChild(CreateScriptNode(doc, FirebirdScript, "firebird"));

				if (SQLiteScript.IsModified)
					root.AppendChild(CreateScriptNode(doc, SQLiteScript, "sqlite"));

				string dir = Path.GetDirectoryName(USER_FILE);

				if (!Directory.Exists(dir))
					Directory.CreateDirectory(dir);

				doc.Save(USER_FILE);
			}
		}

		private static XmlNode CreateScriptNode(XmlDocument doc, MaintenanceScript script, string name)
		{
			XmlNode node = doc.CreateElement(name);

			XmlNode headerNode = doc.CreateElement("header");
			headerNode.InnerText = script.Header;
			node.AppendChild(headerNode);

			//XmlNode createNode = doc.CreateElement("create");
			//createNode.InnerText = script.Create;
			//node.AppendChild(createNode);

			//XmlNode updateNode = doc.CreateElement("update");
			//updateNode.InnerText = script.Update;
			//node.AppendChild(updateNode);

			//XmlNode deleteNode = doc.CreateElement("delete");
			//deleteNode.InnerText = script.Delete;
			//node.AppendChild(deleteNode);

			return node;
		}

	}
}
