using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using log4net;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public interface ISQLServerExpressDatabaseConnector : IDatabaseConnector
	{
		DataTable RunQueryDataTable(string sql);
		SqlDataReader RunQuerySQL(string sql);
		void RunNonQuerySQL(string sql);
		SqlDataReader RunStoredProcedure(string storedProcedureName);
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

	public class SQLServerExpressDatabaseConnector : ISQLServerExpressDatabaseConnector
	{
		private DataTable _columns;
		private DataTable _indexes;
		private DataTable _indexReferencedColumns;
		private readonly SqlConnection sqlConnection;
		private DataTable foreignKeyConstraints;
		private DataTable uniqueIndexes;
		private DataTable columnConstraints;
		private DataTable foreignKeyColumns;

		public string DatabaseName { get; set; }
		public ConnectionStringHelper ConnectionInformation { get; set; }

		private static readonly ILog log = LogManager.GetLogger(typeof(SQLServerExpressDatabaseConnector));

		public SQLServerExpressDatabaseConnector(ConnectionStringHelper connectionStringHelper, string databaseName)
		{
			try
			{
				DatabaseName = databaseName;
				string connectionString = connectionStringHelper.GetConnectionStringSqlClient(DatabaseTypes.SQLServerExpress);
				log.InfoFormat("Opening Database using connection string: {0}", connectionString);
				sqlConnection = new SqlConnection(connectionString);

				if (connectionStringHelper.UseFileName)
					connectionStringHelper.DatabaseName = System.IO.Path.GetFileNameWithoutExtension(connectionStringHelper.FileName);

				ConnectionInformation = connectionStringHelper;
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Could not open database connection. See inner exception for details.", e);
			}
		}

		private SQLServerExpressDatabaseConnector(string connectionString, string databaseName)
		{
			try
			{
				DatabaseName = databaseName;
				sqlConnection = new SqlConnection(connectionString);

				ConnectionInformation = new ConnectionStringHelper(connectionString, DatabaseTypes.SQLServerExpress);
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Could not open database connection. See inner exception for details.", e);
			}
		}

		public static SQLServerExpressDatabaseConnector FromConnectionString(string connectionString, string databaseName)
		{
			return new SQLServerExpressDatabaseConnector(connectionString, databaseName);
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
				const string sql = @"
				SELECT DISTINCT
					T.CONSTRAINT_TYPE as CONSTRAINT_TYPE,
					U.COLUMN_NAME as COLUMN_NAME,
					U.TABLE_NAME as TABLE_NAME,
					U.TABLE_SCHEMA as TABLE_SCHEMA
				FROM
					INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS T INNER JOIN
					INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS U ON T.CONSTRAINT_NAME = U.CONSTRAINT_NAME AND
																T.CONSTRAINT_SCHEMA = U.CONSTRAINT_SCHEMA
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
							  column.Parent.Schema));

			if (rows.Length == 0)
				return false;

			return true;
		}

		public DataTable GetForeignKeyColumns()
		{
			if (foreignKeyColumns == null)
			{
				const string sql =
					@"
					SELECT
						CONSTRAINT_NAME, 
						COLUMN_NAME, 
						ORDINAL_POSITION, 
						CONSTRAINT_SCHEMA, 
						TABLE_SCHEMA,
						TABLE_NAME
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

			sqlConnection.Open();
			sqlConnection.Close();
		}

		public void Open()
		{
			if (sqlConnection.State != ConnectionState.Open)
				sqlConnection.Open();

			log.InfoFormat("Database {0} opened successfully", sqlConnection.Database);

			DatabaseName = sqlConnection.Database;
		}

		public void Close()
		{
			sqlConnection.Close();
		}

		public DataTable RunQueryDataTable(string sql)
		{
			try
			{
				using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
				{
					DataTable dataTable = new DataTable();
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
					sqlDataAdapter.Fill(dataTable); // Just pass the DataTable into the SqlDataAdapters Fill Method
					return dataTable;
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("Error running SQL: [{0}]", sql), e);
			}
		}

		public SqlDataReader RunQuerySQL(string sql)
		{
			try
			{
				using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
				{
					return sqlCommand.ExecuteReader();
				}
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
				using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
				{
					sqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("Error running SQL: [{0}]", sql), e);
			}
		}

		public SqlDataReader RunStoredProcedure(string storedProcedureName)
		{
			try
			{
				using (SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection))
				{
					sqlCommand.CommandType = CommandType.StoredProcedure;
					return sqlCommand.ExecuteReader();
				}
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
				using (SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection))
				{
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.ExecuteNonQuery();
				}
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
					const string sql = @"
						SELECT DISTINCT (
							SELECT
								COUNT(T.CONSTRAINT_TYPE)
							FROM
								INFORMATION_SCHEMA.TABLE_CONSTRAINTS T INNER JOIN
								INFORMATION_SCHEMA.KEY_COLUMN_USAGE U ON C.COLUMN_NAME = U.COLUMN_NAME AND 
																		 C.TABLE_NAME = U.TABLE_NAME AND
																		 C.TABLE_SCHEMA = U.TABLE_SCHEMA
							WHERE
								T.CONSTRAINT_NAME = U.CONSTRAINT_NAME AND 
								T.CONSTRAINT_TYPE = 'PRIMARY KEY' AND
								T.TABLE_SCHEMA = U.TABLE_SCHEMA
							) AS InPrimaryKey, 
							TABLE_NAME, 
							TABLE_SCHEMA, 
							COLUMN_NAME, 
							CAST(ORDINAL_POSITION AS INT) AS ORDINAL_POSITION, 
							IS_NULLABLE, 
							DATA_TYPE, 
							CHARACTER_MAXIMUM_LENGTH, 
							COLUMN_DEFAULT, 
							COLUMNPROPERTY(OBJECT_ID('[' + TABLE_SCHEMA +'].['+ TABLE_NAME + ']'), COLUMN_NAME, 'IsIdentity') AS IsIdentity, 
							COLUMNPROPERTY(OBJECT_ID('[' + TABLE_SCHEMA +'].['+ TABLE_NAME +']'), COLUMN_NAME, 'IsComputed') AS IsComputed,
							NUMERIC_PRECISION, 
							NUMERIC_SCALE
						FROM
							INFORMATION_SCHEMA.COLUMNS C
						ORDER BY 
							TABLE_SCHEMA, TABLE_NAME, ORDINAL_POSITION
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
					const string sql = @"
						SELECT  DISTINCT
								CU.TABLE_NAME AS TABLE_NAME, 
								CU.TABLE_SCHEMA AS TABLE_SCHEMA,
								CU.COLUMN_NAME AS COLUMN_NAME, 
								CU.CONSTRAINT_NAME AS INDEX_NAME,
								TC.CONSTRAINT_TYPE AS CONSTRAINT_TYPE,
								C.DATA_TYPE AS DATA_TYPE,
								CAST(CASE WHEN i.type_desc = 'CLUSTERED' THEN 1 ELSE 0 END AS BIT) AS IS_CLUSTERED,
								CAST(CASE WHEN i.is_unique = 1 THEN 1 ELSE 0 END AS BIT) AS IS_UNIQUE
						FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CU
							INNER JOIN INFORMATION_SCHEMA.COLUMNS C ON C.COLUMN_NAME = CU.COLUMN_NAME AND 
																	   C.TABLE_NAME = CU.TABLE_NAME AND
																	   C.TABLE_SCHEMA = CU.TABLE_SCHEMA
							INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC ON TC.TABLE_NAME = CU.TABLE_NAME AND 
																				  TC.CONSTRAINT_NAME = CU.CONSTRAINT_NAME AND
																				  TC.TABLE_SCHEMA = CU.TABLE_SCHEMA
							LEFT JOIN sys.indexes AS i ON (i.index_id > 0 and i.is_hypothetical = 0) AND i.name = CU.CONSTRAINT_NAME --(i.object_id = tbl.object_id)

						UNION

						SELECT DISTINCT
							tbl.Name AS TABLE_NAME,
							sc.name AS TABLE_SCHEMA,
							c.Name AS COLUMN_NAME,
							i.name AS INDEX_NAME,
							CASE WHEN i.is_unique = 1 THEN 'UNIQUE' ELSE 'NONE' END AS CONSTRAINT_TYPE,
							'' AS DATA_TYPE,
							CAST(CASE WHEN i.type_desc = 'CLUSTERED' THEN 1 ELSE 0 END AS BIT) AS IS_CLUSTERED,
							i.is_unique AS IS_UNIQUE
						FROM
							sys.tables AS tbl
								INNER JOIN sys.schemas AS sc ON sc.schema_id = tbl.schema_id
								INNER JOIN sys.indexes AS i ON (i.index_id > 0 and i.is_hypothetical = 0) AND (i.object_id = tbl.object_id)
								LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU ON KCU.TABLE_NAME = tbl.name AND 
																					 KCU.CONSTRAINT_NAME = i.name AND
																					 KCU.TABLE_SCHEMA = sc.name
								INNER JOIN sys.index_columns ic on ic.object_id = tbl.object_id and ic.index_id = i.index_id
								INNER JOIN sys.columns c on c.object_id = ic.object_id and c.column_id = ic.column_id
						WHERE KCU.CONSTRAINT_NAME IS NULL
						ORDER BY TABLE_NAME, INDEX_NAME ASC
					";

					const string sql2 = @"
						SELECT DISTINCT
							TC.TABLE_NAME AS TABLE_NAME,
							TC.TABLE_SCHEMA AS TABLE_SCHEMA,
							CU.COLUMN_NAME AS COLUMN_NAME,
							TC.CONSTRAINT_NAME AS INDEX_NAME,
							TC.CONSTRAINT_TYPE AS CONSTRAINT_TYPE,
							C.DATA_TYPE AS DATA_TYPE,
							CASE INDEXPROPERTY( i.id , i.name , 'IsClustered')
									 WHEN 1 THEN 1
									 ELSE 0 END AS IS_CLUSTERED,
							CASE INDEXPROPERTY( i.id , i.name , 'IsUnique')
									 WHEN 1 THEN 1
									 ELSE 0 END AS IS_UNIQUE
						FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
							INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CU ON TC.TABLE_NAME = CU.TABLE_NAME AND 
																						TC.CONSTRAINT_NAME = CU.CONSTRAINT_NAME AND
																						TC.TABLE_SCHEMA = CU.TABLE_SCHEMA
							INNER JOIN INFORMATION_SCHEMA.COLUMNS C ON C.COLUMN_NAME = CU.COLUMN_NAME AND 
																	   C.TABLE_NAME = CU.TABLE_NAME AND
																	   C.TABLE_SCHEMA = CU.TABLE_SCHEMA
							INNER JOIN sysindexes AS i ON i.name = CU.CONSTRAINT_NAME --(i.object_id = tbl.object_id)

						UNION

						SELECT DISTINCT
							o.name AS TABLE_NAME, 
							'' AS TABLE_SCHEMA,
							c.name AS COLUMN_NAME,
							i.name AS INDEX_NAME, 
							CASE INDEXPROPERTY( i.id , i.name , 'IsUnique') 
								WHEN 1 THEN 'UNIQUE' 
								ELSE 'NONE' END AS CONSTRAINT_TYPE,
							'' AS DATA_TYPE,
							CASE INDEXPROPERTY( i.id , i.name , 'IsClustered')
									 WHEN 1 THEN 1
									 ELSE 0 END AS IS_CLUSTERED,
							CASE INDEXPROPERTY( i.id , i.name , 'IsUnique')
									 WHEN 1 THEN 1
									 ELSE 0 END AS IS_UNIQUE
						FROM sysindexes i
							INNER JOIN sysindexkeys ik ON i.id = ik.id AND i.indid = ik.indid
							INNER JOIN syscolumns c ON i.id = c.id AND ik.colid = c.colid
							INNER JOIN sysobjects o ON o.id = i.id
							LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU ON KCU.TABLE_NAME = o.name AND KCU.CONSTRAINT_NAME = i.name
						WHERE KCU.CONSTRAINT_NAME IS NULL AND
							o.xtype = 'U'
						ORDER BY TABLE_NAME, INDEX_NAME ASC
						";

					try
					{
						_indexes = RunQueryDataTable(sql);
					}
					catch
					{
						_indexes = RunQueryDataTable(sql2);
					}
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
					const string sql = @"
						SELECT
							T.TABLE_SCHEMA AS TABLE_SCHEMA,
							T.TABLE_NAME AS ReferencedTable, 
							K.COLUMN_NAME AS ReferencedColumn, 
							K.CONSTRAINT_NAME AS ReferencedKey, 
							R.CONSTRAINT_NAME AS ForeignKey
						FROM
							INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS R 
							LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS T ON R.UNIQUE_CONSTRAINT_NAME = T.CONSTRAINT_NAME 
																					  AND R.CONSTRAINT_SCHEMA = T.CONSTRAINT_SCHEMA
							INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE K ON K.CONSTRAINT_NAME = T.CONSTRAINT_NAME AND
																				K.TABLE_SCHEMA = T.TABLE_SCHEMA
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
			const string sql1 = @"
				SELECT 
					TABLE_NAME, 
					TABLE_SCHEMA, 
					(SELECT CAST(CASE WHEN tbl.is_ms_shipped = 1 THEN 1 WHEN
					   (
						SELECT
							major_id
						FROM
							sys.extended_properties
						WHERE
							major_id = tbl.object_id AND minor_id = 0 AND class = 1 AND name = N'microsoft_database_tools_support'
						) IS NOT NULL THEN 1 ELSE 0 END AS bit) AS IsSystemObject
					FROM
						sys.tables AS tbl
					WHERE
						name = T.TABLE_NAME AND SCHEMA_NAME(schema_id) = N'dbo') AS IsSystemObject
				FROM
					INFORMATION_SCHEMA.TABLES AS T
				WHERE
					TABLE_TYPE = 'BASE TABLE'
				ORDER BY
					TABLE_NAME
				";

			// SQL query for databases that are missing various system tables
			const string sql2 = @"
				SELECT 
					TABLE_NAME, 
					TABLE_SCHEMA, 
					CAST(CASE WHEN (OBJECTPROPERTY(SO.id, N'IsMSShipped') = 1) THEN 1 WHEN 1 = OBJECTPROPERTY(SO.id, N'IsSystemTable') THEN 1 ELSE 0 END AS bit) AS IsSystemObject
				FROM
					INFORMATION_SCHEMA.TABLES AS T
					INNER JOIN sysobjects SO on T.TABLE_NAME = SO.name
				WHERE
					TABLE_TYPE = 'BASE TABLE'
				ORDER BY
					TABLE_NAME
			";

			List<string> tableNames = new List<string>();
			SqlDataReader sqlDataReader = null;

			try
			{
				try
				{
					sqlDataReader = RunQuerySQL(sql1);
				}
				catch (Exception ex)
				{
					if (ex.Message.IndexOf("SCHEMA_NAME") > 0)
					{
						sqlDataReader = RunQuerySQL(sql2);
					}
					else
					{
						throw;
					}
				}

				// Exclude system tables
				int isSysObjectColumnOrdinal = sqlDataReader.GetOrdinal("IsSystemObject");

				while (sqlDataReader.Read())
				{
					bool isSystemObject = sqlDataReader.IsDBNull(isSysObjectColumnOrdinal) ? false : (bool)sqlDataReader[isSysObjectColumnOrdinal];

					if (!isSystemObject)
					{
						tableNames.Add(sqlDataReader["TABLE_NAME"] + "|" + sqlDataReader["TABLE_SCHEMA"]);
					}
				}
			}
			finally
			{
				if (sqlDataReader != null && !sqlDataReader.IsClosed)
				{
					sqlDataReader.Close();
				}
			}

			return tableNames;
		}

		public List<string> GetViewNames()
		{
			const string sql1 = @"
				SELECT
						TABLE_NAME,
						TABLE_SCHEMA
				FROM INFORMATION_SCHEMA.VIEWS
				ORDER BY TABLE_NAME
				";

			List<string> viewNames = new List<string>();
			SqlDataReader sqlDataReader = null;

			try
			{
				sqlDataReader = RunQuerySQL(sql1);

				while (sqlDataReader.Read())
					viewNames.Add(sqlDataReader["TABLE_NAME"] + "|" + sqlDataReader["TABLE_SCHEMA"]);
			}
			finally
			{
				if (sqlDataReader != null && !sqlDataReader.IsClosed)
					sqlDataReader.Close();
			}
			return viewNames;
		}

		public DataTable GetForeignKeyConstraints()
		{
			// I am so sorry for this. SQL Server is a PITA, and does have index information in the INFORMATION_SCHEMA. Meaning you have to select it
			// from all over the place.
			if (foreignKeyConstraints == null)
			{
				const string sql =
					@"
					SELECT DISTINCT
						R.UNIQUE_CONSTRAINT_NAME as PrimaryKeyName,
						I.TABLE_NAME as PrimaryTable,
						I.TABLE_SCHEMA AS PrimaryTableSchema,
						R.CONSTRAINT_NAME as ForeignKeyName,
						(SELECT DISTINCT TABLE_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE KF WHERE R.CONSTRAINT_NAME = KF.CONSTRAINT_NAME AND R.CONSTRAINT_SCHEMA = KF.CONSTRAINT_SCHEMA) AS ForeignTable,
						(SELECT DISTINCT TABLE_SCHEMA FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE KF WHERE R.CONSTRAINT_NAME = KF.CONSTRAINT_NAME AND R.CONSTRAINT_SCHEMA = KF.CONSTRAINT_SCHEMA) AS ForeignTableSchema
					FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS R

					JOIN

					(
						SELECT		
							CU.TABLE_SCHEMA AS TABLE_SCHEMA,					
							CU.TABLE_NAME AS TABLE_NAME,
							CU.CONSTRAINT_NAME AS INDEX_NAME,
							CU.CONSTRAINT_SCHEMA AS CONSTRAINT_SCHEMA,
							TC.CONSTRAINT_TYPE AS CONSTRAINT_TYPE,			
							C.DATA_TYPE AS DATA_TYPE,
							CAST(CASE WHEN i.type_desc = 'CLUSTERED' THEN 1 ELSE 0 END AS BIT) AS IS_CLUSTERED,
							CAST(CASE WHEN i.is_unique = 1 THEN 1 ELSE 0 END AS BIT) AS IS_UNIQUE
						FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CU
							INNER JOIN INFORMATION_SCHEMA.COLUMNS C ON C.COLUMN_NAME = CU.COLUMN_NAME AND 
																	   C.TABLE_NAME = CU.TABLE_NAME AND
																	   C.TABLE_SCHEMA = CU.TABLE_SCHEMA
							INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC ON TC.TABLE_NAME = CU.TABLE_NAME AND 
																			TC.CONSTRAINT_NAME = CU.CONSTRAINT_NAME AND
																			TC.TABLE_SCHEMA = CU.TABLE_SCHEMA
							LEFT JOIN sys.indexes AS i ON (i.index_id > 0 and i.is_hypothetical = 0) AND i.name = CU.CONSTRAINT_NAME

						UNION

						SELECT
							SC.name AS TABLE_SCHEMA,
							tbl.Name AS TABLE_NAME,
							i.name AS INDEX_NAME,
							SC.name AS CONSTRAINT_SCHEMA,
							'NONE' AS CONSTRAINT_TYPE,
							'' AS DATA_TYPE,
							CAST(CASE WHEN i.type_desc = 'CLUSTERED' THEN 1 ELSE 0 END AS BIT) AS IS_CLUSTERED,
							i.is_unique AS IS_UNIQUE
						FROM
							sys.tables AS tbl
								INNER JOIN sys.schemas SC ON tbl.schema_id = SC.schema_id
								INNER JOIN sys.indexes AS i ON (i.index_id > 0 and i.is_hypothetical = 0) AND (i.object_id = tbl.object_id)
								LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU ON KCU.TABLE_NAME = tbl.name AND 
																					 KCU.CONSTRAINT_NAME = i.name AND
																					 KCU.TABLE_SCHEMA = SC.name
								INNER JOIN sys.index_columns ic on ic.object_id = tbl.object_id and ic.index_id = i.index_id
								INNER JOIN sys.columns c ON c.object_id = ic.object_id and c.column_id = ic.column_id
								INNER JOIN INFORMATION_SCHEMA.COLUMNS ISC ON c.name = ISC.COLUMN_NAME AND 
																			 tbl.name = ISC.TABLE_NAME AND
																			 ISC.TABLE_SCHEMA = SC.name
						WHERE KCU.CONSTRAINT_NAME IS NULL
					) I
					On R.UNIQUE_CONSTRAINT_NAME = I.INDEX_NAME AND
						R.CONSTRAINT_SCHEMA = I.CONSTRAINT_SCHEMA
					Order By ForeignKeyName
					";

				foreignKeyConstraints = RunQueryDataTable(sql);
			}

			return foreignKeyConstraints;
		}

		public void Dispose()
		{
			if (sqlConnection != null)
				sqlConnection.Dispose();
		}

		public DataTable GetUniqueIndexes()
		{
			if (uniqueIndexes == null)
			{
				const string sql =
					@"
						SELECT  CU.TABLE_NAME AS TABLE_NAME, 
								CU.TABLE_SCHEMA AS TABLE_SCHEMA,
								CU.COLUMN_NAME AS COLUMN_NAME, 
								CU.CONSTRAINT_NAME AS INDEX_NAME,
								TC.CONSTRAINT_TYPE AS CONSTRAINT_TYPE,
								C.DATA_TYPE AS DATA_TYPE,
								CAST(CASE WHEN i.type_desc = 'CLUSTERED' THEN 1 ELSE 0 END AS BIT) AS IS_CLUSTERED,
								CAST(CASE WHEN i.is_unique = 1 THEN 1 ELSE 0 END AS BIT) AS IS_UNIQUE,
								i.index_id AS ORDINAL_POSITION
						FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CU
							INNER JOIN INFORMATION_SCHEMA.COLUMNS C ON C.COLUMN_NAME = CU.COLUMN_NAME AND 
																	   C.TABLE_NAME = CU.TABLE_NAME AND
																	   C.TABLE_SCHEMA = CU.TABLE_SCHEMA
							INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC ON TC.TABLE_NAME = CU.TABLE_NAME AND
																				  TC.CONSTRAINT_NAME = CU.CONSTRAINT_NAME AND
																				  TC.TABLE_SCHEMA = CU.TABLE_SCHEMA
							LEFT JOIN sys.indexes AS i ON (i.index_id > 0 and i.is_hypothetical = 0) AND i.name = CU.CONSTRAINT_NAME --(i.object_id = tbl.object_id)
						WHERE IS_UNIQUE = 1

						UNION

						SELECT
							tbl.Name AS TABLE_NAME,
							sc.name AS TABLE_SCHEMA,
							c.Name AS COLUMN_NAME,
							i.name AS INDEX_NAME,
							'NONE' AS CONSTRAINT_TYPE,
							'' AS DATA_TYPE,
							CAST(CASE WHEN i.type_desc = 'CLUSTERED' THEN 1 ELSE 0 END AS BIT) AS IS_CLUSTERED,
							i.is_unique AS IS_UNIQUE,
							i.index_id AS ORDINAL_POSITION
						FROM
							sys.tables AS tbl
								INNER JOIN sys.schemas sc ON sc.schema_id = tbl.schema_id
								INNER JOIN sys.indexes AS i ON (i.index_id > 0 and i.is_hypothetical = 0) AND (i.object_id = tbl.object_id)
								LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU on KCU.TABLE_NAME = tbl.name AND 
																					 KCU.CONSTRAINT_NAME = i.name AND
																					 KCU.TABLE_SCHEMA = sc.name
								INNER JOIN sys.index_columns ic on ic.object_id = tbl.object_id and ic.index_id = i.index_id
								INNER JOIN sys.columns c on c.object_id = ic.object_id and c.column_id = ic.column_id
						WHERE KCU.CONSTRAINT_NAME IS NULL AND I.IS_UNIQUE = 1
						ORDER BY TABLE_NAME, ORDINAL_POSITION ASC
						";

				const string sql2 = @"
						SELECT 
							TC.TABLE_NAME AS TABLE_NAME,
							TC.TABLE_SCHEMA AS TABLE_SCHEMA,
							CU.COLUMN_NAME AS COLUMN_NAME,
							TC.CONSTRAINT_NAME AS INDEX_NAME,
							TC.CONSTRAINT_TYPE AS CONSTRAINT_TYPE,
							C.DATA_TYPE AS DATA_TYPE,
							CAST(CASE INDEXPROPERTY( i.id , i.name , 'IsClustered')
									 WHEN 1 THEN 1
									 ELSE 0 END AS BIT) AS IS_CLUSTERED,
							CAST(CASE INDEXPROPERTY( i.id , i.name , 'IsUnique')
									 WHEN 1 THEN 1
									 ELSE 0 END AS BIT) AS IS_UNIQUE,
							i.indid AS ORDINAL_POSITION
						FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
							INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CU ON TC.TABLE_NAME = CU.TABLE_NAME AND 
																						TC.CONSTRAINT_NAME = CU.CONSTRAINT_NAME AND
																						TC.TABLE_SCHEMA = CU.TABLE_SCHEMA
							INNER JOIN INFORMATION_SCHEMA.COLUMNS C ON C.COLUMN_NAME = CU.COLUMN_NAME AND 
																	   C.TABLE_NAME = CU.TABLE_NAME AND
																	   C.TABLE_SCHEMA = CU.TABLE_SCHEMA
							LEFT JOIN sysindexes AS i ON i.name = CU.CONSTRAINT_NAME --(i.object_id = tbl.object_id)
						WHERE (CASE INDEXPROPERTY( i.id , i.name , 'IsUnique')
									 WHEN 1 THEN 1
									 ELSE 0 END = 1)

						UNION

						SELECT 
							o.name AS TABLE_NAME, 
							'' AS TABLE_SCHEMA,
							c.name AS COLUMN_NAME,
							i.name AS INDEX_NAME, 
							'NONE' AS CONSTRAINT_TYPE,
							'' AS DATA_TYPE,
							CAST(CASE INDEXPROPERTY( i.id , i.name , 'IsClustered')
									 WHEN 1 THEN 1
									 ELSE 0 END AS BIT) AS IS_CLUSTERED,
							CAST(CASE INDEXPROPERTY( i.id , i.name , 'IsUnique')
									 WHEN 1 THEN 1
									 ELSE 0 END AS BIT) AS IS_UNIQUE,
							i.indid AS ORDINAL_POSITION
						FROM sysindexes i
							INNER JOIN sysindexkeys ik ON i.id = ik.id AND i.indid = ik.indid
							INNER JOIN syscolumns c ON i.id = c.id AND ik.colid = c.colid
							INNER JOIN sysobjects o ON o.id = i.id
							LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU ON KCU.TABLE_NAME = o.name AND KCU.CONSTRAINT_NAME = i.name
						WHERE KCU.CONSTRAINT_NAME IS NULL AND
								o.xtype = 'U' AND
								(CASE INDEXPROPERTY( i.id , i.name , 'IsUnique')
									 WHEN 1 THEN 1
									 ELSE 0 END = 1)
						ORDER BY TABLE_NAME, ORDINAL_POSITION ASC
						";

				try
				{
					uniqueIndexes = RunQueryDataTable(sql);
				}
				catch
				{
					uniqueIndexes = RunQueryDataTable(sql2);
				}
			}

			return uniqueIndexes;
		}
	}
}