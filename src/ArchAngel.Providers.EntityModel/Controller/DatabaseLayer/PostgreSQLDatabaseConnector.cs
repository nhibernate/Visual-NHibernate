using System;
using System.Collections.Generic;
using System.Data;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Devart.Data.Universal;
using log4net;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public interface IPostgreSQLDatabaseConnector : IDatabaseConnector
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

	public class PostgreSQLDatabaseConnector : IPostgreSQLDatabaseConnector
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

		private static readonly ILog log = LogManager.GetLogger(typeof(PostgreSQLDatabaseConnector));

		public PostgreSQLDatabaseConnector(ConnectionStringHelper connectionStringHelper)
		{
			try
			{
				string connectionString = connectionStringHelper.GetConnectionStringSqlClient(DatabaseTypes.PostgreSQL);
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

		private PostgreSQLDatabaseConnector(string connectionString)
		{
			try
			{
				ConnectionInformation = new ConnectionStringHelper(connectionString, DatabaseTypes.PostgreSQL);
				uniConnection = new UniConnection(ConnectionInformation.GetConnectionStringSqlClient());
				DatabaseName = uniConnection.Database;
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Could not open database connection. See inner exception for details.", e);
			}
		}

		public static PostgreSQLDatabaseConnector FromConnectionString(string connectionString)
		{
			return new PostgreSQLDatabaseConnector(connectionString);
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
				string sql = @"
				SELECT DISTINCT
					T.CONSTRAINT_TYPE as CONSTRAINT_TYPE,
					U.COLUMN_NAME as COLUMN_NAME,
					U.TABLE_NAME as TABLE_NAME,
					U.TABLE_SCHEMA as TABLE_SCHEMA
				FROM
					INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS T INNER JOIN
					INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS U ON T.CONSTRAINT_NAME = U.CONSTRAINT_NAME
				";

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
				string sql = @"
					SELECT
						CONSTRAINT_NAME, COLUMN_NAME, ORDINAL_POSITION, TABLE_NAME
					FROM
						INFORMATION_SCHEMA.KEY_COLUMN_USAGE
					ORDER BY ORDINAL_POSITION
				";

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
					string sql = @"
						SELECT distinct
							(       SELECT COUNT(CONSTRAINT_TYPE) 
									FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
									WHERE  TC.TABLE_SCHEMA = K.TABLE_SCHEMA AND
										   TC.TABLE_NAME = K.TABLE_NAME AND
										   TC.CONSTRAINT_TYPE = 'PRIMARY KEY') AS InPrimaryKey,
							C.TABLE_NAME, 
							C.TABLE_SCHEMA, 
							C.COLUMN_NAME, 
							C.ORDINAL_POSITION, 
							C.IS_NULLABLE, 
							C.DATA_TYPE, 
							C.CHARACTER_MAXIMUM_LENGTH, 
							C.COLUMN_DEFAULT, 
							CASE WHEN C.is_identity = 'YES' OR C.COLUMN_DEFAULT LIKE 'nextval%' OR C.DATA_TYPE LIKE '%serial%' THEN 1 ELSE 0 END AS IsIdentity, 
							0 AS IsComputed,
							C.NUMERIC_PRECISION, 
							C.NUMERIC_SCALE
						FROM
							INFORMATION_SCHEMA.COLUMNS C
							LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE K ON C.TABLE_SCHEMA = K.TABLE_SCHEMA AND
																				C.TABLE_NAME = K.TABLE_NAME AND
																				C.COLUMN_NAME = k.COLUMN_NAME
						ORDER BY 
							C.TABLE_NAME, C.COLUMN_NAME, C.ORDINAL_POSITION
							";

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
					string sql = @"
							SELECT a.TABLE_NAME AS TABLE_NAME,
								   c.TABLE_NAME AS TABLE_NAME_2,
								   a.TABLE_SCHEMA AS TABLE_SCHEMA,
								   c.COLUMN_NAME AS COLUMN_NAME,
								   a.CONSTRAINT_NAME AS INDEX_NAME,
								   a.CONSTRAINT_TYPE AS CONSTRAINT_TYPE, 
								   array_to_string(
									 array(
										SELECT CAST(COLUMN_NAME AS varchar)
									   FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
									   WHERE CONSTRAINT_NAME = a.CONSTRAINT_NAME
									   ORDER BY ORDINAL_POSITION
									   ),
									 ', '
									 ) as column_list,
									 CASE WHEN PGI.indisunique THEN 1 ELSE 0 END AS IS_UNIQUE,
									 CASE WHEN PGI.indisclustered THEN 1 ELSE 0 END AS IS_CLUSTERED
							FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS a 
								 INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE b ON a.CONSTRAINT_NAME = b.CONSTRAINT_NAME
								 LEFT JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE c ON a.CONSTRAINT_NAME = c.CONSTRAINT_NAME /*AND 
																						   a.CONSTRAINT_TYPE = 'FOREIGN KEY'*/
								 LEFT OUTER JOIN pg_stat_all_indexes ST ON a.TABLE_NAME = ST.relname AND a.CONSTRAINT_NAME = ST.indexrelname
								 LEFT OUTER JOIN pg_index PGI ON PGI.indexrelid = ST.indexrelid    
							GROUP BY a.TABLE_CATALOG, a.TABLE_SCHEMA, a.TABLE_NAME, 
									 a.CONSTRAINT_NAME, a.CONSTRAINT_TYPE, 
									 c.TABLE_NAME, c.COLUMN_NAME, PGI.indisunique, PGI.indisclustered
							ORDER BY a.TABLE_CATALOG, a.TABLE_SCHEMA, a.TABLE_NAME, 
									 a.CONSTRAINT_NAME
					";

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
					string sql = @"
						SELECT
							T.TABLE_NAME AS ReferencedTable, 
							K.COLUMN_NAME AS ReferencedColumn, 
							K.CONSTRAINT_NAME AS ReferencedKey, 
							R.CONSTRAINT_NAME AS ForeignKey
						FROM
							INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS R 
							LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS T ON R.UNIQUE_CONSTRAINT_NAME = T.CONSTRAINT_NAME 
							INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE K ON K.CONSTRAINT_NAME = T.CONSTRAINT_NAME
						ORDER BY
							T.TABLE_NAME, ORDINAL_POSITION
						";

					_indexReferencedColumns = RunQueryDataTable(sql);
				}

				return _indexReferencedColumns;
			}
		}

		public List<string> GetTableNames()
		{
			string sql1 = @"
				SELECT 
					TABLE_NAME, 
					TABLE_SCHEMA, 
					0 AS IsSystemObject
				FROM INFORMATION_SCHEMA.TABLES
				WHERE TABLE_TYPE = 'BASE TABLE'
				ORDER BY TABLE_NAME
				";

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
			string sql1 = @"
				SELECT 
					TABLE_NAME, 
					TABLE_SCHEMA, 
					0 AS IsSystemObject
				FROM INFORMATION_SCHEMA.TABLES
				WHERE TABLE_TYPE = 'VIEW'
				ORDER BY TABLE_NAME
				";

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
				string sql = @"
					SELECT DISTINCT
						RC.UNIQUE_CONSTRAINT_NAME AS PrimaryKeyName,
						CCU.TABLE_NAME AS PrimaryTable,
						CCU.TABLE_SCHEMA AS PrimaryTableSchema,
						CCU.CONSTRAINT_NAME AS ForeignKeyName,
						K.TABLE_NAME AS ForeignTable,
						K.TABLE_SCHEMA AS ForeignTableSchema
					FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE K
						 INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CCU ON K.TABLE_SCHEMA = CCU.TABLE_SCHEMA AND
																			   K.CONSTRAINT_NAME = CCU.CONSTRAINT_NAME 
						 INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC ON TC.TABLE_SCHEMA = K.TABLE_SCHEMA AND
																			   TC.CONSTRAINT_NAME = K.CONSTRAINT_NAME
						 INNER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC ON RC.CONSTRAINT_SCHEMA = K.TABLE_SCHEMA AND
																					 RC.CONSTRAINT_NAME = K.CONSTRAINT_NAME
					WHERE   CCU.TABLE_NAME IS NOT NULL AND
							TC.CONSTRAINT_TYPE = 'FOREIGN KEY'
					ORDER BY ForeignKeyName
					";

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
				string sql = @"
					SELECT 
						TC.TABLE_NAME,
						TC.TABLE_SCHEMA,
						C.COLUMN_NAME,
						ST.INDEXRELNAME AS INDEX_NAME,
						TC.CONSTRAINT_TYPE,
						C.DATA_TYPE,
						0 AS IS_CLUSTERED,
						(CASE WHEN PGI.indisunique THEN 1 ELSE 0 END) AS IS_UNIQUE,
						C.ORDINAL_POSITION
					FROM pg_index PGI
						 INNER JOIN pg_stat_all_indexes ST ON PGI.INDEXRELID = ST.INDEXRELID
						INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC ON ST.SCHEMANAME = TC.TABLE_SCHEMA AND 
																			  ST.INDEXRELNAME = TC.CONSTRAINT_NAME
						INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CCU ON CCU.TABLE_SCHEMA = TC.TABLE_SCHEMA AND
																					 CCU.TABLE_NAME = TC.TABLE_NAME AND
																					 CCU.CONSTRAINT_NAME = TC.CONSTRAINT_NAME
						INNER JOIN INFORMATION_SCHEMA.COLUMNS C ON C.TABLE_SCHEMA = CCU.TABLE_SCHEMA AND 
																   C.TABLE_NAME = CCU.TABLE_NAME AND 
																   C.COLUMN_NAME = CCU.COLUMN_NAME                                                                                  
					WHERE  PGI.indisunique
					ORDER BY TABLE_NAME, ORDINAL_POSITION
					";

				uniqueIndexes = RunQueryDataTable(sql);
			}
			return uniqueIndexes;
		}
	}
}
