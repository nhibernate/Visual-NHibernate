using System;
using System.Xml;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Helper;
using log4net;
using Slyce.Common;
using Slyce.Common.StringExtensions;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer
{
	public interface IDatabaseDeserialisationScheme
	{
		IDatabase Deserialise(string databaseXML);
	}

	/// <summary>
	/// Deserialises a Version 1 Database XML file.
	/// </summary>
	public class DatabaseDeserialisationScheme : IDatabaseDeserialisationScheme
	{
		/*
		 * Current Performance Results (18th December 2008):
		 * Small DB (20 Tables, 5 Columns each): 10ms
		 * Medium DB (100 Tables, 10 Columns each): 100ms
		 * Large DB (1000 Tables, 20 Columns each): 1000ms
		 */

		private static readonly ILog log = LogManager.GetLogger(typeof(DatabaseDeserialisationScheme));
		private Type uniDbType = typeof(EntityModel.Helper.SQLServer.UniDbTypes);

		public IDatabase Deserialise(string databaseXML)
		{
			// ReSharper disable PossibleNullReferenceException			
			log.Info("Loading database XML");
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(databaseXML);

			XmlElement root = doc.DocumentElement;
			string databaseName = root.SelectSingleNode("DatabaseName").InnerText;
			string databaseType = root.SelectSingleNode("DatabaseType").InnerText;
			DatabaseTypes dbType = (DatabaseTypes)Enum.Parse(typeof(DatabaseTypes), databaseType, true);
			log.InfoFormat("Processing database XML (DB Name = {0})", databaseName);
			Database database = new Database(databaseName, dbType);

			var connectionInfoNode = root.SelectSingleNode("ConnectionInformation");

			if (connectionInfoNode != null)
			{
				database.Loader = DeserialiseConnectionInformation(connectionInfoNode, databaseName);
				database.ConnectionInformation = database.Loader.DatabaseConnector.ConnectionInformation;
			}
			XmlNodeList tableNodes = root.SelectNodes("Table");
			XmlNodeList viewNodes = root.SelectNodes("View");
			XmlNodeList relationshipNodes = root.SelectNodes("Relationship");

			// The process:
			// 1) Deserialise all Tables. Don't do their columns/keys/indexes yet.
			// 2) Deserialise the Columns from each Table node. Add them to their parent Tables.
			// 3) Deserialise Indexes, add them to their parent Tables.
			// 4) Deserialise Keys.
			// 5) Deserialise Relationships

			DeserialiseTables(database, tableNodes);
			DeserialiseColumns(database, tableNodes);
			DeserialiseIndexes(database, tableNodes);
			DeserialiseKeys(database, tableNodes);

			DeserialiseViews(database, viewNodes);
			DeserialiseViewColumns(database, viewNodes);
			DeserialiseViewIndexes(database, viewNodes);
			DeserialiseViewKeys(database, viewNodes);

			DeserialiseRelationships(database, relationshipNodes);

			return database;

			// ReSharper restore PossibleNullReferenceException
		}

		private void DeserialiseRelationships(IDatabase database, XmlNodeList nodes)
		{
			//
			foreach (XmlNode relNode in nodes)
			{
				DeserialiseRelationship(relNode, database);
			}
		}

		private void DeserialiseKeys(IDatabase database, XmlNodeList nodes)
		{
			// 1) Deserialise Primary Keys.
			foreach (XmlNode tableNode in nodes)
			{
				var keyElements = tableNode.SelectSingleNode("Keys");
				ProcessKeysNode(database, keyElements);
			}

			// 2) Deserialise Foreign Keys.
			foreach (XmlNode tableNode in nodes)
			{
				ITable parent = database.GetTable(tableNode.SelectSingleNode("Name").InnerText, tableNode.SelectSingleNode("Schema").InnerText);
				var keyElements = tableNode.SelectNodes("Keys/Key");
				if (keyElements == null) continue;

				foreach (XmlNode keyElement in keyElements)
				{
					IKey key = parent.GetKey(keyElement.SelectSingleNode("Name").InnerText);
					ProcessReferencedKey(key, keyElement, database);
				}
			}
		}

		private void DeserialiseViewKeys(IDatabase database, XmlNodeList nodes)
		{
			// 1) Deserialise Primary Keys.
			foreach (XmlNode tableNode in nodes)
			{
				var keyElements = tableNode.SelectSingleNode("Keys");
				ProcessKeysNode(database, keyElements);
			}

			// 2) Deserialise Foreign Keys.
			foreach (XmlNode tableNode in nodes)
			{
				ITable parent = database.GetView(tableNode.SelectSingleNode("Name").InnerText, tableNode.SelectSingleNode("Schema").InnerText);
				var keyElements = tableNode.SelectNodes("Keys/Key");
				if (keyElements == null) continue;

				foreach (XmlNode keyElement in keyElements)
				{
					IKey key = parent.GetKey(keyElement.SelectSingleNode("Name").InnerText);
					ProcessReferencedKey(key, keyElement, database);
				}
			}
		}

		public Relationship DeserialiseRelationship(XmlNode node, IDatabase database)
		{
			NodeProcessor proc = new NodeProcessor(node);

			var rel = new RelationshipImpl();

			rel.Identifier = proc.Attributes.GetGuid("identifier");
			rel.Name = proc.GetString("Name");

			ITable table1 = database.GetTable(proc.GetString("PrimaryTable"), proc.GetString("PrimarySchema"));
			ITable table2 = database.GetTable(proc.GetString("ForeignTable"), proc.GetString("ForeignSchema"));

			if (table1 == null)
				table1 = database.GetView(proc.GetString("PrimaryTable"), proc.GetString("PrimarySchema"));

			if (table2 == null)
				table2 = database.GetView(proc.GetString("ForeignTable"), proc.GetString("ForeignSchema"));

			if (table1 == null || table2 == null)
				return null;

			var key1 = table1.GetKey(proc.GetString("PrimaryKey"));
			var key2 = table2.GetKey(proc.GetString("ForeignKey"));

			if (key1 == null || key2 == null)
				return null; // The foreign key has probably been removed

			if (key1.Keytype == DatabaseKeyType.Foreign)
			{
				// The keys are around the wrong way.
				var tempKey = key2;
				key2 = key1;
				key1 = tempKey;

				var tempTable = table2;
				table2 = table1;
				table1 = tempTable;
			}

			rel.AddThisTo(table1, table2);
			rel.PrimaryKey = key1;
			rel.ForeignKey = key2;
			rel.PrimaryCardinality = rel.ForeignKey.IsUnique ? Cardinality.One : Cardinality.Many;
			rel.ForeignCardinality = rel.PrimaryKey.IsUnique ? Cardinality.One : Cardinality.Many;
			rel.Database = database;

			return rel;
		}

		private void DeserialiseIndexes(IDatabase database, XmlNodeList nodes)
		{
			foreach (XmlNode tableNode in nodes)
			{
				var indexElements = tableNode.SelectSingleNode("Indexes");
				ProcessIndexesNode(database, indexElements);
			}
		}

		private void DeserialiseViewIndexes(IDatabase database, XmlNodeList nodes)
		{
			foreach (XmlNode tableNode in nodes)
			{
				var indexElements = tableNode.SelectSingleNode("Indexes");
				ProcessViewIndexesNode(database, indexElements);
			}
		}

		private void ProcessKeysNode(IDatabase database, XmlNode keysNode)
		{
			// ReSharper disable PossibleNullReferenceException
			if (keysNode == null)
				return;

			string parentName = keysNode.SelectSingleNode("../Name").InnerText;
			string parentSchema = keysNode.SelectSingleNode("../Schema").InnerText;

			ITable parent = database.GetTable(parentName, parentSchema);

			if (parent == null)
				parent = database.GetView(parentName, parentSchema);

			if (parent == null)
				throw new Exception(string.Format("Table/View not found: {0}.{1}", parentSchema, parentName));

			foreach (XmlNode keyNode in keysNode.SelectNodes("Key"))
			{
				IKey key = ProcessKeyNode(keyNode, parent, database);
				parent.AddKey(key);
			}
			// ReSharper restore PossibleNullReferenceException
		}

		private void ProcessIndexesNode(IDatabase database, XmlNode indexesNode)
		{
			// ReSharper disable PossibleNullReferenceException
			if (indexesNode == null)
				return;

			ITable parent = database.GetTable(indexesNode.SelectSingleNode("../Name").InnerText, indexesNode.SelectSingleNode("../Schema").InnerText);

			foreach (XmlNode indexNode in indexesNode.SelectNodes("Index"))
			{
				IIndex index = ProcessIndexNode(indexNode, parent, database);
				parent.AddIndex(index);
			}
			// ReSharper restore PossibleNullReferenceException
		}

		private void ProcessViewIndexesNode(IDatabase database, XmlNode indexesNode)
		{
			// ReSharper disable PossibleNullReferenceException
			if (indexesNode == null)
				return;

			ITable parent = database.GetView(indexesNode.SelectSingleNode("../Name").InnerText, indexesNode.SelectSingleNode("../Schema").InnerText);

			foreach (XmlNode indexNode in indexesNode.SelectNodes("Index"))
			{
				IIndex index = ProcessIndexNode(indexNode, parent, database);
				parent.AddIndex(index);
			}
			// ReSharper restore PossibleNullReferenceException
		}

		public IIndex ProcessIndexNode(XmlNode node, ITable parent, IDatabase db)
		{
			IIndex index = new Index();
			index.Parent = parent;
			ProcessScriptBase(index, node);
			/*
				<IsUnique>True</IsUnique>
				<IsClustered>False</IsClustered>
				<Datatype>Unique</Datatype>
				<Columns>
				  <ColumnName>ColumnT13</ColumnName>
				</Columns>
			 */
			NodeProcessor proc = new NodeProcessor(node);

			index.IsUnique = proc.GetBool("IsUnique");
			index.IsClustered = proc.GetBool("IsClustered");
			index.IndexType = proc.GetEnum<DatabaseIndexType>("Datatype");

			var columnNodeList = node.SelectNodes("Columns/ColumnName");

			if (columnNodeList != null)
				foreach (XmlNode columnRef in columnNodeList)
					index.AddColumn(columnRef.InnerText);

			return index;
		}

		private void DeserialiseColumns(IDatabase database, XmlNodeList nodes)
		{
			foreach (XmlNode tableNode in nodes)
			{
				var columnsElement = tableNode.SelectSingleNode("Columns");
				ITable table = database.GetTable(tableNode.SelectSingleNode("Name").InnerText, tableNode.SelectSingleNode("Schema").InnerText);
				ProcessColumnsNode(table, columnsElement);
			}
		}

		private void DeserialiseViewColumns(IDatabase database, XmlNodeList nodes)
		{
			foreach (XmlNode tableNode in nodes)
			{
				var columnsElement = tableNode.SelectSingleNode("Columns");
				ITable table = database.GetView(tableNode.SelectSingleNode("Name").InnerText, tableNode.SelectSingleNode("Schema").InnerText);
				ProcessColumnsNode(table, columnsElement);
			}
		}

		private void ProcessColumnsNode(IColumnContainer parent, XmlNode columnsNode)
		{
			// ReSharper disable PossibleNullReferenceException
			if (columnsNode == null)
				return;

			foreach (XmlNode columnNode in columnsNode.SelectNodes("Column"))
			{
				IColumn column = ProcessColumnNode(columnNode);
				parent.AddColumn(column);
			}
			// ReSharper restore PossibleNullReferenceException
		}

		public IColumn ProcessColumnNode(XmlNode node)
		{
			IColumn column = new Column();
			ProcessScriptBase(column, node);
			/*
				<Datatype>int</Datatype>
				<Default />
				<InPrimaryKey>True</InPrimaryKey>
				<IsCalculated>False</IsCalculated>
				<IsComputed>False</IsComputed>
				<IsIdentity>False</IsIdentity>
				<IsNullable>False</IsNullable>
				<IsReadOnly>False</IsReadOnly>
				<IsUnique>True</IsUnique>
				<OrdinalPosition>1</OrdinalPosition>
				<Precision>10</Precision>
				<Scale>0</Scale>
				<Size>0</Size>
			 */
			NodeProcessor proc = new NodeProcessor(node);

			column.OriginalDataType = proc.GetString("Datatype");
			column.Default = proc.GetString("Default");
			column.InPrimaryKey = proc.GetBool("InPrimaryKey");
			column.IsCalculated = proc.GetBool("IsCalculated");
			column.IsComputed = proc.GetBool("IsComputed");
			column.IsIdentity = proc.GetBool("IsIdentity");
			column.IsNullable = proc.GetBool("IsNullable");
			column.IsReadOnly = proc.GetBool("IsReadOnly");
			column.IsUnique = proc.GetBool("IsUnique");
			column.OrdinalPosition = proc.GetInt("OrdinalPosition");
			column.Precision = proc.GetInt("Precision");
			column.Scale = proc.GetInt("Scale");
			column.Size = proc.GetLong("Size");

			return column;
		}

		public IKey ProcessKeyNode(XmlNode node, ITable parent, IDatabase db)
		{
			IKey key = new Key();
			key.Parent = parent;
			ProcessScriptBase(key, node);
			/*
				<Keytype>Primary</Keytype>
				<Columns>
				  <ColumnName>ColumnT11</ColumnName>
				</Columns>
				<ReferencedKey>
					<KeyName>ForeignKey</KeyName>
					<TableName>Table2</TableName>
				</ReferencedKey>
			 */
			NodeProcessor proc = new NodeProcessor(node);

			key.Keytype = proc.GetEnum<DatabaseKeyType>("Keytype");
			key.IsUnique = proc.GetBool("IsUnique");

			var columnNodeList = node.SelectNodes("Columns/ColumnName");
			if (columnNodeList != null)
			{
				foreach (XmlNode columnRef in columnNodeList)
				{
					key.AddColumn(columnRef.InnerText);
				}
			}

			return key;
		}

		public void ProcessReferencedKey(IKey key, XmlNode keyNode, IDatabase db)
		{
			var referencedKeyNode = keyNode.SelectSingleNode("ReferencedKey");
			if (referencedKeyNode != null)
			{
				NodeProcessor proc = new NodeProcessor(referencedKeyNode);
				string tableName = proc.GetString("TableName");
				string schema = proc.GetString("Schema");
				string keyName = proc.GetString("KeyName");
				key.ReferencedKey = db.GetKey(tableName, schema, keyName);
			}
		}

		private void DeserialiseTables(ITableContainer database, XmlNodeList tableNodes)
		{
			foreach (XmlNode tableNode in tableNodes)
			{
				ITable table = ProcessTableNode(tableNode);
				database.AddTable(table);
			}
		}

		private void DeserialiseViews(IViewContainer database, XmlNodeList viewNodes)
		{
			foreach (XmlNode viewNode in viewNodes)
			{
				ITable view = ProcessTableNode(viewNode);
				database.AddView(view);
			}
		}

		/// <summary>
		/// This method will fill a Table object with its basic properties, but won't
		/// fill its columns or anything else. Things need to be processed in a certain
		/// order to be correctly deserialised.
		/// </summary>
		/// <param name="tableNode"></param>
		/// <returns></returns>
		public ITable ProcessTableNode(XmlNode tableNode)
		{
			Table table = new Table();
			ProcessScriptBase(table, tableNode);
			return table;
		}

		public void ProcessScriptBase(IScriptBase scriptBase, XmlNode scriptBaseNode)
		{
			/*
				<Name>Table1</Name>
				<Description />
				<Enabled>False</Enabled>
				<IsUserDefined>False</IsUserDefined>
				<UID>00000000-0000-0000-0000-000000000000</UID>
			 */

			NodeProcessor proc = new NodeProcessor(scriptBaseNode);

			scriptBase.Name = proc.GetString("Name");
			scriptBase.Schema = proc.GetString("Schema");
			scriptBase.Description = proc.GetString("Description");
			scriptBase.Enabled = proc.GetBool("Enabled");
			scriptBase.IsUserDefined = proc.GetBool("IsUserDefined");

			if (proc.Exists(VirtualPropertyDeserialiser.VirtualPropertiesNodeName))
			{
				var deserialiser = new VirtualPropertyDeserialiser();
				scriptBase.Ex =
					deserialiser.DeserialiseVirtualProperties(
						scriptBaseNode.SelectSingleNode(VirtualPropertyDeserialiser.VirtualPropertiesNodeName));
			}
		}

		public IDatabaseLoader DeserialiseConnectionInformation(XmlNode node, string databaseName)
		{
			if (node == null) throw new ArgumentNullException("node");

			NodeProcessor proc = new NodeProcessor(node);

			DatabaseTypes connectorType = proc.Attributes.GetEnum<DatabaseTypes>("DatabaseConnector");

			switch (connectorType)
			{
				//case DatabaseTypes.SQLServer2000:
				case DatabaseTypes.SQLServer2005:
					return LoadSqlServer2005Connector(node);
				case DatabaseTypes.SQLServerExpress:
					return LoadSqlServerExpressConnector(node, databaseName);
				case DatabaseTypes.SQLCE:
					return LoadSqlCEConnector(node);
				case DatabaseTypes.MySQL:
					return LoadMySQLConnector(node);
				case DatabaseTypes.Oracle:
					return LoadOracleConnector(node);
				case DatabaseTypes.PostgreSQL:
					return LoadPostgreSQLConnector(node);
				case DatabaseTypes.Firebird:
					return LoadFirebirdConnector(node);
				case DatabaseTypes.SQLite:
					return LoadSQLiteConnector(node);
				default:
					throw new NotImplementedException(string.Format("Have not implemented {0} in DeserialiseConnectionInformation().", connectorType));
			}
		}

		private ConnectionStringHelper LoadGenericDatabaseData(XmlNode node, DatabaseTypes databaseType)
		{
			NodeProcessor proc = new NodeProcessor(node);

			var connectionHelper = new ConnectionStringHelper();

			if (string.IsNullOrEmpty(connectionHelper.ServerName))
			{
				switch (databaseType)
				{
					case DatabaseTypes.SQLCE:
					//case DatabaseTypes.SQLServer2000:
					case DatabaseTypes.SQLServer2005:
						connectionHelper.ServerName = Environment.MachineName;
						break;
					case DatabaseTypes.SQLServerExpress:
						connectionHelper.ServerName = Environment.MachineName + "\\SQLEXPRESS";
						break;
					case DatabaseTypes.MySQL:
						connectionHelper.ServerName = "localhost";
						break;
					case DatabaseTypes.Oracle:
						connectionHelper.ServerName = "localhost";
						break;
					case DatabaseTypes.PostgreSQL:
						connectionHelper.ServerName = "localhost";
						break;
					case DatabaseTypes.Firebird:
						connectionHelper.ServerName = "localhost";
						break;
					case DatabaseTypes.SQLite:
						connectionHelper.ServerName = Environment.MachineName;
						break;
					default:
						throw new NotImplementedException("Database type not handled yet: " + databaseType.ToString());

				}
			}
			connectionHelper.CurrentDbType = databaseType;

			if (proc.Exists("ServerName"))
				connectionHelper.ServerName = proc.GetString("ServerName");

			if (proc.Exists("DatabaseName"))
			{
				connectionHelper.DatabaseName = proc.GetString("DatabaseName");
				connectionHelper.UseFileName = false;
			}
			if (proc.Exists("FileName"))
			{
				connectionHelper.FileName = proc.GetString("FileName");
				connectionHelper.UseFileName = true;
			}
			if (proc.Exists("UseIntegratedSecurity"))
				connectionHelper.UseIntegratedSecurity = proc.GetBool("UseIntegratedSecurity");

			if (proc.Exists("UserName"))
				connectionHelper.UserName = proc.GetString("UserName");

			if (proc.Exists("Password"))
			{
				string password = "";
				try
				{
					password = proc.GetString("Password").Decrypt();
				}
				catch
				{
					// Do nothing
					password = "";
				}
				connectionHelper.Password = password;
			}
			if (proc.Exists("Port"))
				connectionHelper.Port = proc.GetInt("Port");

			if (proc.Exists("ServiceName"))
				connectionHelper.ServiceName = proc.GetString("ServiceName");

			if (proc.Exists("SchemaName"))
				connectionHelper.ServiceName = proc.GetString("SchemaName");

			if (proc.Exists("UseDirectConnection"))
				connectionHelper.UseDirectConnection = proc.GetBool("UseDirectConnection");
			else
				connectionHelper.UseDirectConnection = false;

			return connectionHelper;
		}

		private SQLServer2005DatabaseLoader LoadSqlServer2005Connector(XmlNode node)
		{
			var connectionHelper = LoadGenericDatabaseData(node, DatabaseTypes.SQLServer2005);
			return new SQLServer2005DatabaseLoader(new SQLServer2005DatabaseConnector(connectionHelper));
		}

		private SQLServerExpressDatabaseLoader LoadSqlServerExpressConnector(XmlNode node, string databasename)
		{
			var connectionHelper = LoadGenericDatabaseData(node, DatabaseTypes.SQLServerExpress);
			connectionHelper.DatabaseName = databasename;
			return new SQLServerExpressDatabaseLoader(new SQLServerExpressDatabaseConnector(connectionHelper, connectionHelper.DatabaseName));
		}

		private MySQLDatabaseLoader LoadMySQLConnector(XmlNode node)
		{
			var connectionHelper = LoadGenericDatabaseData(node, DatabaseTypes.MySQL);
			return new MySQLDatabaseLoader(new MySQLDatabaseConnector(connectionHelper));
		}

		private OracleDatabaseLoader LoadOracleConnector(XmlNode node)
		{
			var connectionHelper = LoadGenericDatabaseData(node, DatabaseTypes.Oracle);
			return new OracleDatabaseLoader(new OracleDatabaseConnector(connectionHelper, connectionHelper.DatabaseName));
		}

		private PostgreSQLDatabaseLoader LoadPostgreSQLConnector(XmlNode node)
		{
			var connectionHelper = LoadGenericDatabaseData(node, DatabaseTypes.PostgreSQL);
			return new PostgreSQLDatabaseLoader(new PostgreSQLDatabaseConnector(connectionHelper));
		}

		private SQLCEDatabaseLoader LoadSqlCEConnector(XmlNode node)
		{
			var connectionHelper = LoadGenericDatabaseData(node, DatabaseTypes.SQLCE);
			return new SQLCEDatabaseLoader(new SQLCEDatabaseConnector(connectionHelper.FileName));
		}

		private FirebirdDatabaseLoader LoadFirebirdConnector(XmlNode node)
		{
			var connectionHelper = LoadGenericDatabaseData(node, DatabaseTypes.Firebird);
			return new FirebirdDatabaseLoader(new FirebirdDatabaseConnector(connectionHelper));
		}

		private SQLiteDatabaseLoader LoadSQLiteConnector(XmlNode node)
		{
			var connectionHelper = LoadGenericDatabaseData(node, DatabaseTypes.SQLite);
			return new SQLiteDatabaseLoader(new SQLiteDatabaseConnector(connectionHelper));
		}
	}
}