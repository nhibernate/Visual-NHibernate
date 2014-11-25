using System;
using System.Collections.Generic;
using System.Data;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Devart.Data.Universal;
using log4net;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public interface IMySQLDatabaseConnector : IDatabaseConnector
	{
		DataTable RunQueryDataTable(string sql);
		UniDataReader RunQuerySQL(string sql);
		void RunNonQuerySQL(string sql);
		UniDataReader RunStoredProcedure(string storedProcedureName);
		void RunStoredProcedureNonQuery(string storedProcedureName);
		bool DoesColumnHaveConstraint(Column column, string constraint);
		DataTable GetForeignKeyConstraints();
		DataTable GetUniqueIndexes();
		DataTable Columns { get; }
		DataTable Indexes { get; }
		DataTable IndexReferencedColumns { get; }
		DataTable GetForeignKeyColumns();
		List<string> GetTableNames();
		ConnectionStringHelper ConnectionInformation { get; set; }
		Guid GetUIDForObject(string name);
	}

	public class MySQLDatabaseConnector : IMySQLDatabaseConnector
	{
		private DataTable _columns;
		private DataTable _indexes;
		private DataTable _indexReferencedColumns;
		private readonly UniConnection uniConnection;
		private DataTable foreignKeyConstraints;
		private DataTable uniqueIndexes;
		private DataTable columnConstraints;
		private DataTable foreignKeyColumns;

		public string DatabaseName { get; set; }
		public ConnectionStringHelper ConnectionInformation { get; set; }

		private static readonly ILog log = LogManager.GetLogger(typeof(MySQLDatabaseConnector));

		public MySQLDatabaseConnector(ConnectionStringHelper connectionStringHelper)
		{
			try
			{
				string connectionString = connectionStringHelper.GetConnectionStringSqlClient(DatabaseTypes.MySQL);
				log.InfoFormat("Opening Database using connection string: {0}", connectionString);
				uniConnection = new UniConnection(connectionString);
				DatabaseName = uniConnection.Database;

				ConnectionInformation = connectionStringHelper;
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Could not open database connection. See inner exception for details.", e);
			}
		}

		private MySQLDatabaseConnector(string connectionString)
		{
			try
			{
				ConnectionInformation = new ConnectionStringHelper(connectionString, DatabaseTypes.MySQL);
				uniConnection = new UniConnection(ConnectionInformation.GetConnectionStringSqlClient());
				DatabaseName = uniConnection.Database;
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Could not open database connection. See inner exception for details.", e);
			}
		}

		public static MySQLDatabaseConnector FromConnectionString(string connectionString)
		{
			return new MySQLDatabaseConnector(connectionString);
		}

		public string SchemaFilterCSV { get; set; }

		public Guid GetUIDForObject(string name)
		{
			const string sql = @"Select OBJECT_ID('{0}') as ID";
			using (var dt = RunQueryDataTable(string.Format(sql, name)))
			{
				DataRow row = dt.Rows[0];
				if (row.IsNull(dt.Columns["ID"]))
					return Guid.Empty;

				int result = (int)row["ID"];
				return new Guid(result, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
			}

		}

		public bool DoesColumnHaveConstraint(Column column, string constraint)
		{
			if (columnConstraints == null)
			{
				string sql = string.Format(@"
				SELECT DISTINCT
					T.CONSTRAINT_TYPE as CONSTRAINT_TYPE,
					U.COLUMN_NAME as COLUMN_NAME,
					U.TABLE_NAME as TABLE_NAME,
					U.TABLE_SCHEMA AS TABLE_SCHEMA
				FROM
					INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS T INNER JOIN
					INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS U ON T.CONSTRAINT_NAME = U.CONSTRAINT_NAME
				WHERE T.TABLE_SCHEMA = '{0}'
				", DatabaseName);

				try
				{
					columnConstraints = RunQueryDataTable(sql);
				}
				catch (Exception e)
				{
					log.Error(e.Message);
					throw;
				}
			}
			DataRow[] rows = columnConstraints.Select(
				string.Format("CONSTRAINT_TYPE = '{0}' AND COLUMN_NAME = '{1}' AND TABLE_NAME = '{2}' AND TABLE_SCHEMA = '{3}'",
							  constraint,
							  column.Name.Replace("'", "''"),
							  column.Parent.Name.Replace("'", "''"),
							  column.Parent.Schema.Replace("'", "''")));

			if (rows.Length == 0)
				return false;

			return true;
		}

		public DataTable GetForeignKeyColumns()
		{
			if (foreignKeyColumns == null)
			{
				string sql = string.Format(
					@"
				SELECT
					CONSTRAINT_NAME, COLUMN_NAME, ORDINAL_POSITION, TABLE_NAME
				FROM
					INFORMATION_SCHEMA.KEY_COLUMN_USAGE
				WHERE TABLE_SCHEMA = '{0}'
				ORDER BY ORDINAL_POSITION
			", DatabaseName);

				foreignKeyColumns = RunQueryDataTable(sql);
			}
			return foreignKeyColumns;
		}

		public void TestConnection()
		{
			if (string.IsNullOrEmpty(ConnectionInformation.DatabaseName))
			{
				throw new DatabaseException("The database name cannot be empty.");
			}

			uniConnection.Open();
			uniConnection.Close();
		}

		public void Open()
		{
			if (uniConnection.State != ConnectionState.Open)
				uniConnection.Open();

			log.InfoFormat("Database {0} opened successfully", uniConnection.Database);

			DatabaseName = uniConnection.Database;
		}

		public void Close()
		{
			uniConnection.Close();
		}

		public DataTable RunQueryDataTable(string sql)
		{
			try
			{
				UniCommand sqlCommand = new UniCommand(sql, uniConnection);
				DataTable dataTable = new DataTable();
				DataSet ds = new DataSet();
				UniDataAdapter sqlDataAdapter = new UniDataAdapter(sqlCommand);
				//sqlDataAdapter.Fill(dataTable, null); // Just pass the DataTable into the SqlDataAdapters Fill Method
				sqlDataAdapter.Fill(ds);
				return ds.Tables[0];
				//return dataTable;
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("Error running SQL: [{0}]", sql), e);
			}
		}

		public UniDataReader RunQuerySQL(string sql)
		{
			try
			{
				UniCommand sqlCommand = new UniCommand(sql, uniConnection);
				return sqlCommand.ExecuteReader();
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("Error running SQL: [{0}]", sql), e);
			}
		}

		public void RunNonQuerySQL(string sql)
		{
			try
			{
				UniCommand sqlCommand = new UniCommand(sql, uniConnection);
				sqlCommand.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("Error running SQL: [{0}]", sql), e);
			}
		}

		public UniDataReader RunStoredProcedure(string storedProcedureName)
		{
			try
			{
				UniCommand sqlCommand = new UniCommand(storedProcedureName, uniConnection);
				//sqlCommand.CommandType = CommandType.StoredProcedure;
				return sqlCommand.ExecuteReader();
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("Error running SP: [{0}]", storedProcedureName), e);
			}
		}

		public void RunStoredProcedureNonQuery(string storedProcedureName)
		{
			try
			{
				UniCommand sqlCommand = new UniCommand(storedProcedureName, uniConnection);
				//sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("Error running SP: [{0}]", storedProcedureName), e);
			}
		}

		public DataTable Columns
		{
			get
			{
				if (_columns == null)
				{
					string sql = string.Format(@"
						SELECT 
							(CASE WHEN COLUMN_KEY = 'PRI' THEN 1 ELSE 0 END) AS InPrimaryKey, 
							TABLE_NAME, TABLE_SCHEMA, COLUMN_NAME, /*CAST(ORDINAL_POSITION AS INT) AS*/ ORDINAL_POSITION, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, 
							COLUMN_DEFAULT, 
							(CASE WHEN EXTRA = 'auto_increment' THEN 1 ELSE 0 END) AS IsIdentity, 
							0 AS IsComputed,
							NUMERIC_PRECISION, NUMERIC_SCALE
						FROM
							INFORMATION_SCHEMA.COLUMNS C
						WHERE TABLE_SCHEMA = '{0}'
						ORDER BY 
							TABLE_NAME, ORDINAL_POSITION", DatabaseName);

					_columns = RunQueryDataTable(sql);
				}
				return _columns;
			}
		}

		public DataTable Indexes
		{
			get
			{
				if (_indexes == null)
				{
					string sql = string.Format(@"
						SELECT DISTINCT
							ST.TABLE_NAME AS TABLE_NAME, 
							ST.TABLE_SCHEMA AS TABLE_SCHEMA,
							ST.COLUMN_NAME AS COLUMN_NAME, 
							ST.INDEX_NAME AS INDEX_NAME,
							TC.CONSTRAINT_TYPE AS CONSTRAINT_TYPE,
							C.DATA_TYPE AS DATA_TYPE,
							0 AS IS_CLUSTERED,
							(CASE WHEN ST.NON_UNIQUE THEN 0 ELSE 1 END) AS IS_UNIQUE
						FROM INFORMATION_SCHEMA.STATISTICS ST
							INNER JOIN INFORMATION_SCHEMA.COLUMNS C ON C.COLUMN_NAME = ST.COLUMN_NAME AND C.TABLE_NAME = ST.TABLE_NAME
							INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC ON TC.TABLE_NAME = ST.TABLE_NAME AND TC.CONSTRAINT_NAME = ST.INDEX_NAME
						WHERE ST.TABLE_SCHEMA = '{0}'
						ORDER BY TABLE_NAME, INDEX_NAME ASC
					", DatabaseName);

					_indexes = RunQueryDataTable(sql);
				}
				return _indexes;
			}
		}

		public DataTable IndexReferencedColumns
		{
			get
			{
				if (_indexReferencedColumns == null)
				{
					string sql = string.Format(@"
						SELECT
							T.TABLE_NAME AS ReferencedTable, 
							K.COLUMN_NAME AS ReferencedColumn, 
							K.CONSTRAINT_NAME AS ReferencedKey, 
							R.CONSTRAINT_NAME AS ForeignKey
						FROM
							INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS R LEFT OUTER JOIN
							INFORMATION_SCHEMA.TABLE_CONSTRAINTS T ON R.UNIQUE_CONSTRAINT_NAME = T.CONSTRAINT_NAME INNER JOIN
							INFORMATION_SCHEMA.KEY_COLUMN_USAGE K ON K.CONSTRAINT_NAME = T.CONSTRAINT_NAME
						WHERE T.TABLE_SCHEMA = '{0}'
						ORDER BY
							T.TABLE_NAME, ORDINAL_POSITION", DatabaseName);

					_indexReferencedColumns = RunQueryDataTable(sql);
				}

				return _indexReferencedColumns;
			}
		}

		public List<string> GetTableNames()
		{
			string sql1 = string.Format(@"
				SELECT 
					TABLE_NAME, 
					TABLE_SCHEMA, 
					0 AS IsSystemObject
				FROM INFORMATION_SCHEMA.TABLES
				WHERE TABLE_SCHEMA = '{0}' AND TABLE_TYPE = 'BASE TABLE'
				ORDER BY TABLE_NAME", DatabaseName);

			List<string> tableNames = new List<string>();
			UniDataReader sqlDataReader = null;

			try
			{
				sqlDataReader = RunQuerySQL(sql1);

				// Exclude system tables
				int isSysObjectColumnOrdinal = sqlDataReader.GetOrdinal("IsSystemObject");
				int ordTableName = sqlDataReader.GetOrdinal("TABLE_NAME");
				int ordTableSchema = sqlDataReader.GetOrdinal("TABLE_SCHEMA");

				while (sqlDataReader.Read())
				{
					bool isSystemObject = sqlDataReader.IsDBNull(isSysObjectColumnOrdinal) ? false : (bool)sqlDataReader.GetBoolean(isSysObjectColumnOrdinal);

					if (!isSystemObject)
					{
						tableNames.Add(sqlDataReader.GetString(ordTableName) + "|" + sqlDataReader.GetString(ordTableSchema));
					}
				}
			}
			finally
			{
				if (sqlDataReader != null)// && !sqlDataReader.IsClosed)
					sqlDataReader.Close();
			}

			return tableNames;
		}

		public List<string> GetViewNames()
		{
			string sql1 = string.Format(@"
				SELECT 
					TABLE_NAME, 
					TABLE_SCHEMA, 
					0 AS IsSystemObject
				FROM INFORMATION_SCHEMA.TABLES
				WHERE TABLE_SCHEMA = '{0}' AND TABLE_TYPE = 'VIEW'
				ORDER BY TABLE_NAME", DatabaseName);

			List<string> viewNames = new List<string>();
			UniDataReader sqlDataReader = null;

			try
			{
				sqlDataReader = RunQuerySQL(sql1);

				// Exclude system tables
				int isSysObjectColumnOrdinal = sqlDataReader.GetOrdinal("IsSystemObject");
				int ordTableName = sqlDataReader.GetOrdinal("TABLE_NAME");
				int ordTableSchema = sqlDataReader.GetOrdinal("TABLE_SCHEMA");

				while (sqlDataReader.Read())
				{
					bool isSystemObject = sqlDataReader.IsDBNull(isSysObjectColumnOrdinal) ? false : (bool)sqlDataReader.GetBoolean(isSysObjectColumnOrdinal);

					if (!isSystemObject)
						viewNames.Add(sqlDataReader.GetString(ordTableName) + "|" + sqlDataReader.GetString(ordTableSchema));
				}
			}
			finally
			{
				if (sqlDataReader != null)// && !sqlDataReader.IsClosed)
					sqlDataReader.Close();
			}
			return viewNames;
		}

		public DataTable GetForeignKeyConstraints()
		{
			if (foreignKeyConstraints == null)
			{
				string sql = string.Format(@"
					SELECT DISTINCT
						'PRIMARY' AS PrimaryKeyName,
						REFERENCED_TABLE_NAME AS PrimaryTable,
						REFERENCED_TABLE_SCHEMA AS PrimaryTableSchema,
						CONSTRAINT_NAME AS ForeignKeyName,
						TABLE_NAME AS ForeignTable,
						CONSTRAINT_SCHEMA AS ForeignTableSchema
					FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
					WHERE CONSTRAINT_SCHEMA = '{0}' AND
						REFERENCED_TABLE_NAME IS NOT NULL
					ORDER BY ForeignKeyName
					", DatabaseName);

				foreignKeyConstraints = RunQueryDataTable(sql);
			}

			return foreignKeyConstraints;
		}

		public void Dispose()
		{
			if (uniConnection != null)
				uniConnection.Dispose();
		}

		public DataTable GetUniqueIndexes()
		{
			if (uniqueIndexes == null)
			{
				string sql = string.Format(@"
					SELECT 
						ST.TABLE_NAME,
						ST.TABLE_SCHEMA,
						ST.COLUMN_NAME,
						ST.INDEX_NAME,
						TC.CONSTRAINT_TYPE,
						C.DATA_TYPE,
						0 AS IS_CLUSTERED,
						(CASE WHEN ST.NON_UNIQUE = 0 THEN 1 ELSE 0 END) AS IS_UNIQUE,
						C.ORDINAL_POSITION
					FROM INFORMATION_SCHEMA.STATISTICS ST
						INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC ON ST.TABLE_NAME = TC.TABLE_NAME AND ST.TABLE_SCHEMA = TC.TABLE_SCHEMA AND ST.INDEX_NAME = TC.CONSTRAINT_NAME
						INNER JOIN INFORMATION_SCHEMA.COLUMNS C ON C.TABLE_SCHEMA = ST.TABLE_SCHEMA AND C.TABLE_NAME = ST.TABLE_NAME AND C.COLUMN_NAME = ST.COLUMN_NAME
					WHERE   ST.TABLE_SCHEMA = '{0}' AND
							ST.NON_UNIQUE = 0
					ORDER BY TABLE_NAME, ORDINAL_POSITION", DatabaseName);

				uniqueIndexes = RunQueryDataTable(sql);
			}
			return uniqueIndexes;
		}
	}
}
