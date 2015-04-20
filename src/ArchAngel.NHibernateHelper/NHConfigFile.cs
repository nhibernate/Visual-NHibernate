using System;
using System.IO;
using System.Xml;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;

namespace ArchAngel.NHibernateHelper
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = false, IsGeneratorIterator = true)]
	public class NHConfigFile : ArchAngel.Interfaces.ScriptBaseObject
	{
		public class Test
		{
		}

		public enum ConfigLocations
		{
			NHConfigFile,
			WebConfigFile,
			AppConfigFile,
			Other
		}
		//[ArchAngel.Interfaces.Attributes.TemplateEnum]
		//public enum ByteCodeProviders
		//{
		//    None,
		//    Null,
		//    lcg,
		//    codedom
		//}
		private bool BusyProcessingFile = false;
		private IDatabaseConnector _DatabaseConnector;
		private string _ConfigXmlFragment;

		public string FilePath { get; set; }
		public ConfigLocations ConfigLocation { get; internal set; }
		public bool HasNamespace { get; private set; }
		public bool? use_outer_join = null;
		public bool? show_sql = null;
		public bool? generate_statistics = null;
		public int? max_fetch_depth = null;
		//public bool? use_reflection_optimizer = null;
		//public ByteCodeProviders ByteCodeProvider = ByteCodeProviders.None;
		public string cache_provider_class = null;
		public bool? cache_use_minimal_puts = null;
		public bool? cache_use_query_cache = null;
		public string cache_query_cache_factory = null;
		public string cache_region_prefix = null;
		public string query_substitutions = null;
		public bool? use_proxy_validator = null;
		public string transaction_factory_class = null;

		public bool FileExists
		{
			get { return File.Exists(FilePath); }
		}

		/// <summary>
		/// Save the ConfigXmlFragment to an existing file.
		/// </summary>
		public string ProcessExistingFile(out string CurrentFileName)
		{
			if (!FileExists)
				throw new Exception("This method can only be called if the file exists, which it doesn't. Check by inspecting the FileExists property first.");

			CurrentFileName = FilePath;
			string xml = File.ReadAllText(FilePath);
			string startText = "<hibernate-configuration";
			string endText = "</hibernate-configuration>";
			int startPos = xml.IndexOf(startText);
			int endPos = xml.IndexOf(endText) + endText.Length;

			if (startPos < 0)
				throw new Exception("XML node not found: '<hibernate-configuration>'. File: " + FilePath);

			if (endPos < startPos)
				throw new Exception("XML node not found: '</hibernate-configuration>'. File: " + FilePath);

			xml = xml.Remove(startPos, endPos - startPos);
			xml = xml.Insert(startPos, ConfigXmlFragment);
			return xml;
		}

		public ArchAngel.Providers.EntityModel.ProviderInfo EntityProviderInfo
		{
			get
			{
				if (this.ProviderInfo == null)
					return null;

				return this.ProviderInfo.EntityProviderInfo;
			}
		}

		public ProviderInfo ProviderInfo { get; internal set; }

		public IDatabaseConnector DatabaseConnector
		{
			get
			{
				ProcessFile();
				return _DatabaseConnector;
			}
			private set
			{
				_DatabaseConnector = value;
			}
		}

		public string ConfigXmlFragment
		{
			get
			{
				if (string.IsNullOrEmpty(_ConfigXmlFragment) && File.Exists(FilePath))
				{
					ProcessFile();
					//}
					if (_ConfigXmlFragment == null)
						return _ConfigXmlFragment;

					_ConfigXmlFragment = Slyce.Common.XmlUtility.GetIndentedUTF8Xml(_ConfigXmlFragment);
					// Remove the xml declaration (first line)
					_ConfigXmlFragment = _ConfigXmlFragment.Substring(_ConfigXmlFragment.IndexOf("<", 5));
				}
				return _ConfigXmlFragment;
			}
			set { _ConfigXmlFragment = value; }
		}

		public static string GetDialect(bool isSpatial, ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes databaseType)
		{
			switch (databaseType)
			{
				case DatabaseTypes.MySQL:
					return isSpatial ? "NHibernate.Spatial.Dialect.MySQLSpatialDialect,NHibernate.Spatial.MySQL" : "NHibernate.Dialect.MySQLDialect";
				case DatabaseTypes.SQLCE:
					return isSpatial ? "NHibernate.Dialect.MsSqlCeDialect" : "NHibernate.Dialect.MsSqlCeDialect";
				case DatabaseTypes.SQLServer2005:
					return isSpatial ? "NHibernate.Spatial.Dialect.MsSql2008SpatialDialect,NHibernate.Spatial.MsSql2008" : "NHibernate.Dialect.MsSql2005Dialect";
				case DatabaseTypes.SQLServerExpress:
					return isSpatial ? "NHibernate.Dialect.MsSql2005Dialect" : "NHibernate.Dialect.MsSql2005Dialect";
				case DatabaseTypes.Oracle:
					return isSpatial ? "NHibernate.Dialect.Oracle9Dialect" : "NHibernate.Dialect.Oracle9Dialect";
				case DatabaseTypes.PostgreSQL:
					return "NHibernate.Dialect.PostgreSQLDialect";
				case DatabaseTypes.Firebird:
					return "NHibernate.Dialect.FirebirdDialect";
				case DatabaseTypes.SQLite:
					return "NHibernate.Dialect.SQLiteDialect";
				case DatabaseTypes.Unknown:
					return "unknown";
				default:
					throw new NotImplementedException("Dialect not handled yet: " + databaseType.ToString());
			}
		}

		public string GetDialect(bool isSpatial)
		{
			return GetDialect(isSpatial, EntityProviderInfo.MappingSet.Database.DatabaseType);
		}

		public static string GetDriver(ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes databaseType)
		{
			switch (databaseType)
			{
				case DatabaseTypes.MySQL:
					return "NHibernate.Driver.MySqlDataDriver";
					break;
				case DatabaseTypes.SQLCE:
					return "NHibernate.Driver.SqlServerCeDriver";
					break;
				case DatabaseTypes.SQLServer2005:
				case DatabaseTypes.SQLServerExpress:
					return "NHibernate.Driver.SqlClientDriver";
					break;
				case DatabaseTypes.Oracle:
					return "NHibernate.Driver.OracleDataClientDriver";
					break;
				case DatabaseTypes.PostgreSQL:
					return "NHibernate.Driver.NpgsqlDriver";
				case DatabaseTypes.Firebird:
					return "NHibernate.Driver.FirebirdClientDriver";
				case DatabaseTypes.SQLite:
					return "NHibernate.Driver.SQLite20Driver";
				case DatabaseTypes.Unknown:
					return "unknown";
				default:
					throw new NotImplementedException("Driver not handled yet: " + databaseType.ToString());
			}
		}

		public string GetDriver()
		{
			return GetDriver(EntityProviderInfo.MappingSet.Database.DatabaseType);
		}

		public string GetConnectionString()
		{
			if (EntityProviderInfo == null ||
				EntityProviderInfo.MappingSet.Database.ConnectionInformation == null)
				return "";

			return EntityProviderInfo.MappingSet.Database.ConnectionInformation.GetNHConnectionStringSqlClient();
		}

		private void ProcessFile()
		{
			if (BusyProcessingFile) return;

			try
			{
				BusyProcessingFile = true;
				string xml = "";

				if (string.IsNullOrEmpty(ConfigXmlFragment))
				{
					if (!File.Exists(FilePath))
						throw new Exception("Filepath hasn't been set yet.");

					string filename = Path.GetFileName(FilePath);

					if (filename.Equals("web.config", StringComparison.OrdinalIgnoreCase))
						ConfigLocation = ConfigLocations.WebConfigFile;
					else if (filename.EndsWith(".config", StringComparison.OrdinalIgnoreCase))
						ConfigLocation = ConfigLocations.AppConfigFile;
					else if (filename.EndsWith(".cfg.xml", StringComparison.OrdinalIgnoreCase))
						ConfigLocation = ConfigLocations.NHConfigFile;
					else
						ConfigLocation = ConfigLocations.Other;

					xml = File.ReadAllText(FilePath);
				}
				else
					xml = ConfigXmlFragment;

				XmlDocument doc = new XmlDocument();
				doc.LoadXml(xml);
				XmlNode dialectNode;
				XmlNode driverClassNode;
				XmlNode connStringNode;
				XmlNode nhConfigNode;
				XmlNode defaultSchemaNode;

				/*
						public bool? use_outer_join = null;
			public bool? show_sql = null;
			public bool? generate_statistics = null;
			public int? max_fetch_depth = null;
			//public bool? use_reflection_optimizer = null;
			public ByteCodeProviders BytCodeProvider = ByteCodeProviders.None;
			public string cache_provider_class = null;
			public bool? cache_use_minimal_puts = null;
			public bool? cache_use_query_cache = null;
			public string cache_query_cache_factory = null;
			public string cache_region_prefix = null;
			public string query_substitutions = null;
			public bool? use_proxy_validator = null;
			public string transaction_factory_class = null;
				*/

				XmlNode use_outer_joinNode;
				XmlNode show_sqlNode;
				XmlNode generate_statisticsNode;
				XmlNode max_fetch_depthNode;
				XmlNode BytCodeProviderNode;
				XmlNode cache_provider_classNode;
				XmlNode cache_use_minimal_putsNode;
				XmlNode cache_use_query_cacheNode;
				XmlNode cache_query_cache_factoryNode;
				XmlNode cache_region_prefixNode;
				XmlNode query_substitutionsNode;
				XmlNode use_proxy_validatorNode;
				XmlNode transaction_factory_classNode;

				HasNamespace = xml.Contains("xmlns");

				if (HasNamespace)
				{
					var nsmgr = new XmlNamespaceManager(doc.NameTable);
					nsmgr.AddNamespace("nh", "urn:nhibernate-configuration-2.2");

					nhConfigNode = doc.SelectSingleNode("//nh:hibernate-configuration", nsmgr);
					dialectNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='dialect']", nsmgr);
					driverClassNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='connection.driver_class']", nsmgr);
					connStringNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='connection.connection_string']", nsmgr);
					defaultSchemaNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='default_schema']", nsmgr);

					use_outer_joinNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='use_outer_join']", nsmgr);
					show_sqlNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='show_sql']", nsmgr);
					generate_statisticsNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='generate_statistics']", nsmgr);
					max_fetch_depthNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='max_fetch_depth']", nsmgr);
					BytCodeProviderNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='bytecode.provider']", nsmgr);
					cache_provider_classNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='cache.provider_class']", nsmgr);
					cache_use_minimal_putsNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='cache.use_minimal_puts']", nsmgr);
					cache_use_query_cacheNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='cache.use_query_cache']", nsmgr);
					cache_query_cache_factoryNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='cache.query_cache_factory']", nsmgr);
					cache_region_prefixNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='cache.region_prefix']", nsmgr);
					query_substitutionsNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='query_substitutions']", nsmgr);
					use_proxy_validatorNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='use_proxy_validator']", nsmgr);
					transaction_factory_classNode = nhConfigNode.SelectSingleNode("nh:session-factory/nh:property[@name='transaction_factory_class']", nsmgr);
				}
				else
				{
					// Not using a namespace
					nhConfigNode = doc.SelectSingleNode("//hibernate-configuration");
					dialectNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='dialect']");
					driverClassNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='connection.driver_class']");
					connStringNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='connection.connection_string']");
					defaultSchemaNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='default_schema']");

					use_outer_joinNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='use_outer_join']");
					show_sqlNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='show_sql']");
					generate_statisticsNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='generate_statistics']");
					max_fetch_depthNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='max_fetch_depth']");
					BytCodeProviderNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='bytecode.provider']");
					cache_provider_classNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='cache.provider_class']");
					cache_use_minimal_putsNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='cache.use_minimal_puts']");
					cache_use_query_cacheNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='cache.use_query_cache']");
					cache_query_cache_factoryNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='cache.query_cache_factory']");
					cache_region_prefixNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='cache.region_prefix']");
					query_substitutionsNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='query_substitutions']");
					use_proxy_validatorNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='use_proxy_validator']");
					transaction_factory_classNode = nhConfigNode.SelectSingleNode("session-factory/property[@name='transaction_factory_class']");
				}
				ConfigXmlFragment = nhConfigNode.OuterXml;

				if (driverClassNode == null && dialectNode == null)
					throw new Exception("Cannot find 'driver_class' or 'dialect' in NHibernate config file for determining database type.");

				if (connStringNode == null)
					throw new Exception("Cannot find connection string in NHibernate config file.");

				// Determine the database type from driver_class, falling back to dialect if necessary
				// See: https://www.hibernate.org/361.html
				if (driverClassNode != null)
				{
					switch (driverClassNode.InnerText.Trim())
					{
						case "NHibernate.Driver.SqlServerCeDriver":
							DatabaseConnector = SQLCEDatabaseConnector.FromConnectionString(connStringNode.InnerText);
							break;
						case "NHibernate.Driver.SqlClientDriver":
							DatabaseConnector = SQLServer2005DatabaseConnector.FromConnectionString(connStringNode.InnerText.Trim());
							break;
						case "NHibernate.Driver.MySqlDataDriver":
							DatabaseConnector = MySQLDatabaseConnector.FromConnectionString(connStringNode.InnerText.Trim());
							break;
						case "NHibernate.Driver.OracleDataClientDriver":
							DatabaseConnector = OracleDatabaseConnector.FromConnectionString(connStringNode.InnerText.Trim(), "");

							if (defaultSchemaNode != null)
								DatabaseConnector.DatabaseName = defaultSchemaNode.InnerText.Trim();

							break;
						case "NHibernate.Driver.NpgsqlDriver":
							DatabaseConnector = PostgreSQLDatabaseConnector.FromConnectionString(connStringNode.InnerText.Trim());
							break;
						case "NHibernate.Driver.FirebirdClientDriver":
							DatabaseConnector = FirebirdDatabaseConnector.FromConnectionString(connStringNode.InnerText.Trim());
							break;
						case "NHibernate.Driver.SQLite20Driver":
							DatabaseConnector = SQLiteDatabaseConnector.FromConnectionString(connStringNode.InnerText.Trim());
							break;
						default:
							throw new DatabaseException(string.Format("Could not determine the database type from the Dialect: '{0}'.", dialectNode.InnerText.Trim()));
					}
				}
				else
				{
					switch (dialectNode.InnerText.Trim())
					{
						case "NHibernate.Dialect.MsSqlCeDialect":
							DatabaseConnector = SQLCEDatabaseConnector.FromConnectionString(connStringNode.InnerText);
							break;
						case "NHibernate.Dialect.MsSql2000Dialect":
						case "NHibernate.Dialect.MsSql2005Dialect":
						case "NHibernate.Spatial.Dialect.MsSql2008SpatialDialect,NHibernate.Spatial.MsSql2008": // TODO: set spatial flag
							DatabaseConnector = SQLServer2005DatabaseConnector.FromConnectionString(connStringNode.InnerText.Trim());
							break;
						case "NHibernate.Dialect.MySQLDialect":
						case "NHibernate.Dialect.MySQL5Dialect":
						case "NHibernate.Spatial.Dialect.MySQLSpatialDialect,NHibernate.Spatial.MySQL": // TODO: set spatial flag
							DatabaseConnector = MySQLDatabaseConnector.FromConnectionString(connStringNode.InnerText.Trim());
							break;
						case "NHibernate.Dialect.Oracle8iDialect":
						case "NHibernate.Dialect.Oracle9Dialect":
						case "NHibernate.Dialect.Oracle9iDialect":
						case "NHibernate.Dialect.Oracle10gDialect":
							DatabaseConnector = OracleDatabaseConnector.FromConnectionString(connStringNode.InnerText.Trim(), "");

							if (defaultSchemaNode != null)
								DatabaseConnector.DatabaseName = defaultSchemaNode.InnerText.Trim();

							break;
						case "NHibernate.Dialect.PostgreSQLDialect":
						case "NHibernate.Dialect.PostgreSQL81Dialect":
						case "NHibernate.Dialect.PostgreSQL82Dialect":
							DatabaseConnector = PostgreSQLDatabaseConnector.FromConnectionString(connStringNode.InnerText.Trim());
							break;
						case "NHibernate.Dialect.FirebirdDialect":
							DatabaseConnector = FirebirdDatabaseConnector.FromConnectionString(connStringNode.InnerText.Trim());
							break;
						default:
							throw new DatabaseException(string.Format("Could not determine the database type from the Dialect: '{0}'.", dialectNode.InnerText.Trim()));
					}
				}

				if (use_outer_joinNode != null) use_outer_join = bool.Parse(use_outer_joinNode.InnerText);
				if (show_sqlNode != null) show_sql = bool.Parse(show_sqlNode.InnerText);
				if (generate_statisticsNode != null) generate_statistics = bool.Parse(generate_statisticsNode.InnerText);
				if (max_fetch_depthNode != null) max_fetch_depth = int.Parse(max_fetch_depthNode.InnerText);

				//if (BytCodeProviderNode != null)
				//{
				//    switch (BytCodeProviderNode.InnerText)
				//    {
				//        case "codedom":
				//            ByteCodeProvider = ByteCodeProviders.codedom;
				//            break;
				//        case "lcg":
				//            ByteCodeProvider = ByteCodeProviders.lcg;
				//            break;
				//        case "null":
				//            ByteCodeProvider = ByteCodeProviders.Null;
				//            break;
				//        default:
				//            throw new NotImplementedException("This value of bytecode.provider not handled yet: " + BytCodeProviderNode.InnerText);
				//    }
				//}
				if (cache_provider_classNode != null) cache_provider_class = cache_provider_classNode.InnerText;
				if (cache_use_minimal_putsNode != null) cache_use_minimal_puts = bool.Parse(cache_use_minimal_putsNode.InnerText);
				if (cache_use_query_cacheNode != null) cache_use_query_cache = bool.Parse(cache_use_query_cacheNode.InnerText);
				if (cache_query_cache_factoryNode != null) cache_query_cache_factory = cache_query_cache_factoryNode.InnerText;
				if (cache_region_prefixNode != null) cache_region_prefix = cache_region_prefixNode.InnerText;
				if (query_substitutionsNode != null) query_substitutions = query_substitutionsNode.InnerText;
				if (use_proxy_validatorNode != null) use_proxy_validator = bool.Parse(use_proxy_validatorNode.InnerText);
				if (transaction_factory_classNode != null) transaction_factory_class = transaction_factory_classNode.InnerText;
			}
			finally
			{
				BusyProcessingFile = false;
			}
		}
	}
}
