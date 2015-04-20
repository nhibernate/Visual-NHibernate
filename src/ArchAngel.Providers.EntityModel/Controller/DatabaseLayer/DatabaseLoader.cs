using System;
using System.Collections.Generic;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public class DatabaseLoaderFacade
	{
		public static Database LoadDatabase(IDatabaseConnector connector, List<SchemaData> databaseObjectsToFetch)
		{
			var loader = GetDatabaseLoader(connector);

			Database database = loader.LoadDatabase(databaseObjectsToFetch, null);

			new DatabaseProcessor().CreateRelationships(database);

			return database;
		}

		/// <summary>
		/// Gets called when loading an existing VS project.
		/// </summary>
		/// <param name="connector"></param>
		/// <returns></returns>
		public static IDatabaseLoader GetDatabaseLoader(IDatabaseConnector connector)
		{
			if (connector is ISQLCEDatabaseConnector)
				return new SQLCEDatabaseLoader(connector as ISQLCEDatabaseConnector);

			if (connector is ISQLServer2005DatabaseConnector)
				return new SQLServer2005DatabaseLoader(connector as ISQLServer2005DatabaseConnector);

			if (connector is IMySQLDatabaseConnector)
				return new MySQLDatabaseLoader(connector as IMySQLDatabaseConnector);

			if (connector is IOracleDatabaseConnector)
				return new OracleDatabaseLoader(connector as IOracleDatabaseConnector);

			if (connector is IPostgreSQLDatabaseConnector)
				return new PostgreSQLDatabaseLoader(connector as IPostgreSQLDatabaseConnector);

			if (connector is IFirebirdDatabaseConnector)
				return new FirebirdDatabaseLoader(connector as IFirebirdDatabaseConnector);

			throw new Exception("DatabaseLoaderFacade does not know how to deal with a connector of type " + connector.GetType());
		}

		public static SQLCEDatabaseLoader GetSQLCELoader(string databaseFilename)
		{
			return new SQLCEDatabaseLoader(new SQLCEDatabaseConnector(databaseFilename));
		}

		public static Database LoadSQLCeDatabase(string databaseFilename, List<SchemaData> databaseObjectsToFetch)
		{
			return GetSQLCELoader(databaseFilename).LoadDatabase(databaseObjectsToFetch, null);
		}

		public static SQLServer2005DatabaseLoader GetSQLServer2005Loader(Helper.ConnectionStringHelper connectionStringHelper)
		{
			return new SQLServer2005DatabaseLoader(new SQLServer2005DatabaseConnector(connectionStringHelper));
		}

		public static Database LoadSQLServer2005Database(Helper.ConnectionStringHelper connectionStringHelper, List<SchemaData> databaseObjectsToFetch)
		{
			return GetSQLServer2005Loader(connectionStringHelper).LoadDatabase(databaseObjectsToFetch, null);
		}

		public static MySQLDatabaseLoader GetMySQLLoader(Helper.ConnectionStringHelper connectionStringHelper)
		{
			return new MySQLDatabaseLoader(new MySQLDatabaseConnector(connectionStringHelper));
		}

		public static Database LoadMySQLDatabase(Helper.ConnectionStringHelper connectionStringHelper, List<SchemaData> databaseObjectsToFetch)
		{
			return GetMySQLLoader(connectionStringHelper).LoadDatabase(databaseObjectsToFetch, null);
		}

		public static OracleDatabaseLoader GetOracleLoader(Helper.ConnectionStringHelper connectionStringHelper, string databaseName)
		{
			return new OracleDatabaseLoader(new OracleDatabaseConnector(connectionStringHelper, databaseName));
		}

		public static Database LoadOracleDatabase(Helper.ConnectionStringHelper connectionStringHelper, string databaseName, List<SchemaData> databaseObjectsToFetch)
		{
			return GetOracleLoader(connectionStringHelper, databaseName).LoadDatabase(databaseObjectsToFetch, null);
		}

		public static PostgreSQLDatabaseLoader GetPostgreSQLLoader(Helper.ConnectionStringHelper connectionStringHelper, string databaseName)
		{
			return new PostgreSQLDatabaseLoader(new PostgreSQLDatabaseConnector(connectionStringHelper));
		}

		public static Database LoadPostgreSQLDatabase(Helper.ConnectionStringHelper connectionStringHelper, string databaseName, List<SchemaData> databaseObjectsToFetch)
		{
			return GetPostgreSQLLoader(connectionStringHelper, databaseName).LoadDatabase(databaseObjectsToFetch, null);
		}

		public static SQLServerExpressDatabaseLoader GetSqlServerExpressLoader(Helper.ConnectionStringHelper connectionStringHelper, string databaseName)
		{
			return new SQLServerExpressDatabaseLoader(new SQLServerExpressDatabaseConnector(connectionStringHelper, databaseName));
		}

		public static Database LoadSqlServerExpressDatabase(Helper.ConnectionStringHelper connectionStringHelper, string databaseName, List<SchemaData> databaseObjectsToFetch)
		{
			return GetSqlServerExpressLoader(connectionStringHelper, databaseName).LoadDatabase(databaseObjectsToFetch, null);
		}

		public static FirebirdDatabaseLoader GetFirebirdLoader(Helper.ConnectionStringHelper connectionStringHelper)
		{
			return new FirebirdDatabaseLoader(new FirebirdDatabaseConnector(connectionStringHelper));
		}

		public static Database LoadFirebirdDatabase(Helper.ConnectionStringHelper connectionStringHelper, List<SchemaData> databaseObjectsToFetch)
		{
			return GetFirebirdLoader(connectionStringHelper).LoadDatabase(databaseObjectsToFetch, null);
		}

		public static SQLiteDatabaseLoader GetSQLiteLoader(Helper.ConnectionStringHelper connectionStringHelper)
		{
			return new SQLiteDatabaseLoader(new SQLiteDatabaseConnector(connectionStringHelper));
		}
	}
}