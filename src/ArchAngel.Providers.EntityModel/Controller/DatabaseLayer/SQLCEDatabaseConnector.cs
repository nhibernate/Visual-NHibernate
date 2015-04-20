using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using log4net;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public interface ISQLCEDatabaseConnector : IDatabaseConnector
	{
		DataTable Columns { get; }
		DataTable Indexes { get; }
		DataTable IndexReferencedColumns { get; }
		bool DoesColumnHaveConstraint(Column column, string constraint);
		DataTable GetForeignKeyColumns();
		DataTable GetForeignKeyConstraints();
		DataTable GetUniqueIndexes();
		DataTable RunQueryDataTable(string sql);
		SqlCeDataReader RunQuerySQL(string sql);
		List<string> GetTableNames();
		ConnectionStringHelper ConnectionInformation { get; set; }

		string Filename { get; set; }
	}

	public class SQLCEDatabaseConnector : ISQLCEDatabaseConnector
	{
		private DataTable _columns;
		private DataTable _indexes;
		private readonly SqlCeConnection sqlConnection;
		private DataTable _indexReferencedColumns;
		private DataTable foreignKeyColumns;
		private DataTable columnConstraints;
		private DataTable uniqueIndexes;
		private DataTable foreignKeyConstraints;
		public string DatabaseName { get; set; }
		public string Filename { get; set; }


		private readonly static ILog log = LogManager.GetLogger(typeof(SQLCEDatabaseConnector));

		protected SQLCEDatabaseConnector()
		{
			DatabaseName = "Test";
		}

		public SQLCEDatabaseConnector(string filename)
		{
			try
			{
				string connectionString = string.Format("Data Source={0}", filename);

				log.InfoFormat("Creating database connector using connection string: {0}", connectionString);

				sqlConnection = new SqlCeConnection(connectionString);
				DatabaseName = sqlConnection.Database;
				Filename = filename;
				ConnectionInformation = new ConnectionStringHelper(connectionString, DatabaseTypes.SQLCE);
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Could not open database connection. See inner exception for details.", e);
			}
		}

		private SQLCEDatabaseConnector(SqlCeConnection connection)
		{
			try
			{
				log.InfoFormat("Creating database connector using connection : {0}", connection.ConnectionString);

				sqlConnection = connection;
				DatabaseName = sqlConnection.Database;
				Filename = connection.DataSource;
				//ConnectionInformation = new ConnectionStringHelper(connectionString, DatabaseTypes.SQLCE);
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Could not open database connection. See inner exception for details.", e);
			}
		}

		public ConnectionStringHelper ConnectionInformation { get; set; }

		public string SchemaFilterCSV { get; set; }

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

		public void Dispose()
		{
			if (sqlConnection != null)
				sqlConnection.Dispose();
		}

		public void TestConnection()
		{
			if (sqlConnection.State == ConnectionState.Open)
				return;

			log.Info("Testing SqlServerCe connection with the following connection string:");
			log.Info(sqlConnection.ConnectionString);

			sqlConnection.Open();
			sqlConnection.Close();
		}

		public bool DoesColumnHaveConstraint(Column column, string constraint)
		{
			if (columnConstraints == null)
			{
				const string sql = @"
				SELECT DISTINCT
					T.CONSTRAINT_TYPE as CONSTRAINT_TYPE,
					U.COLUMN_NAME as COLUMN_NAME,
					U.TABLE_NAME as TABLE_NAME
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
				string.Format("CONSTRAINT_TYPE = '{0}' AND COLUMN_NAME = '{1}' AND TABLE_NAME = '{2}'",
							  constraint, column.Name.Replace("'", "''"), column.Parent.Name.Replace("'", "''")));

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
					CONSTRAINT_NAME, COLUMN_NAME, ORDINAL_POSITION, TABLE_NAME
				FROM
					INFORMATION_SCHEMA.KEY_COLUMN_USAGE
				ORDER BY ORDINAL_POSITION
			";

				foreignKeyColumns = RunQueryDataTable(sql);
			}
			return foreignKeyColumns;
		}

		public DataTable GetForeignKeyConstraints()
		{
			if (foreignKeyConstraints == null)
			{
				const string sql =
					@"
				SELECT DISTINCT *,
					CONSTRAINT_NAME AS PrimaryKeyName, 
					CONSTRAINT_TABLE_NAME AS PrimaryTable, 
					CONSTRAINT_SCHEMA AS PrimaryTableSchema,
					UNIQUE_CONSTRAINT_TABLE_NAME AS ForeignTable,
					UNIQUE_CONSTRAINT_NAME AS ForeignKeyName, 
					UNIQUE_CONSTRAINT_SCHEMA AS ForeignTableSchema,
					DESCRIPTION
				FROM
					INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS

					";

				foreignKeyConstraints = RunQueryDataTable(sql);
			}

			return foreignKeyConstraints;
		}

		public DataTable GetUniqueIndexes()
		{
			if (uniqueIndexes == null)
			{
				const string sql =
					@"
			SELECT
				I.INDEX_NAME as INDEX_NAME, 
				I.[UNIQUE] AS ISUNIQUE, 
				I.[CLUSTERED] AS ISCLUSTERED, 
				I.TABLE_NAME as TABLE_NAME, 
				I.TABLE_SCHEMA AS TABLE_SCHEMA,
				I.ORDINAL_POSITION as ORDINAL_POSITION, 
				I.COLUMN_NAME as COLUMN_NAME
			FROM
				INFORMATION_SCHEMA.INDEXES AS I LEFT OUTER JOIN
				INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC
				ON I.INDEX_NAME = TC.CONSTRAINT_NAME
			WHERE
				(TC.CONSTRAINT_NAME IS NULL) AND ([UNIQUE] = 1)
			ORDER BY ORDINAL_POSITION
					";

				uniqueIndexes = RunQueryDataTable(sql);
			}

			return uniqueIndexes;
		}

		public DataTable RunQueryDataTable(string sql)
		{
			try
			{
				using (SqlCeCommand sqlCommand = new SqlCeCommand(sql, sqlConnection))
				{
					DataTable dataTable = new DataTable();
					SqlCeDataAdapter sqlDataAdapter = new SqlCeDataAdapter(sqlCommand);
					sqlDataAdapter.Fill(dataTable); // Just pass the DataTable into the SqlDataAdapters Fill Method
					return dataTable;
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("Error running SQL: [{0}]", sql), e);
			}
		}

		public SqlCeDataReader RunQuerySQL(string sql)
		{
			try
			{
				using (SqlCeCommand sqlCommand = new SqlCeCommand(sql, sqlConnection))
				{
					return sqlCommand.ExecuteReader();
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("Error running SQL: [{0}]", sql), e);
			}
		}

		public DataTable Columns
		{
			get
			{
				if (_columns == null)
				{
					const string sql = @"
					SELECT
						TABLE_NAME, 
						COLUMN_NAME, 
						CAST(ORDINAL_POSITION AS INT) AS ORDINAL_POSITION,
						IS_NULLABLE, 
						DATA_TYPE, 
						CHARACTER_MAXIMUM_LENGTH, 
						COLUMN_DEFAULT, 
						NUMERIC_PRECISION, 
						NUMERIC_SCALE, 
						CASE WHEN AUTOINC_INCREMENT IS NULL THEN 'FALSE' ELSE 'TRUE' END AS IS_IDENTITY
					FROM
						INFORMATION_SCHEMA.COLUMNS AS C
					ORDER BY 
						TABLE_NAME, ORDINAL_POSITION";

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
					SELECT
						IND.TABLE_NAME as TABLE_NAME, 
						IND.INDEX_NAME as INDEX_NAME,
						IND.COLUMN_NAME as COLUMN_NAME,
						IND.PRIMARY_KEY as IsPrimaryKey,
						IND.[UNIQUE] as IsUnique,
						IND.[CLUSTERED] as IsClustered,
						CASE WHEN CONSTRAINT_TYPE IS NULL THEN 'NONE' ELSE CONSTRAINT_TYPE END AS CONSTRAINT_TYPE
					FROM
						INFORMATION_SCHEMA.INDEXES AS IND LEFT OUTER JOIN
						INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC ON IND.INDEX_NAME = TC.CONSTRAINT_NAME
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
					const string sql = @"
						SELECT
							T.TABLE_NAME AS ReferencedTable, 
							K.COLUMN_NAME AS ReferencedColumn, 
							K.CONSTRAINT_NAME AS ReferencedKey, 
							R.CONSTRAINT_NAME AS ForeignKey
						FROM
							INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS R LEFT OUTER JOIN
							INFORMATION_SCHEMA.TABLE_CONSTRAINTS T ON R.UNIQUE_CONSTRAINT_NAME = T.CONSTRAINT_NAME INNER JOIN
							INFORMATION_SCHEMA.KEY_COLUMN_USAGE K ON K.CONSTRAINT_NAME = T.CONSTRAINT_NAME
						ORDER BY
							T.TABLE_NAME, ORDINAL_POSITION";

					_indexReferencedColumns = RunQueryDataTable(sql);
				}

				return _indexReferencedColumns;
			}
		}

		public List<string> GetTableNames()
		{
			const string sql1 = @"
				SELECT DISTINCT TABLE_NAME
				FROM INFORMATION_SCHEMA.TABLES
				WHERE TABLE_TYPE = 'TABLE'
				ORDER BY TABLE_NAME";

			List<string> tableNames = new List<string>();
			//OleDbDataReader oleDbDataReader = null;
			SqlCeDataReader sqlDataReader = null;

			try
			{
				try
				{
					sqlDataReader = RunQuerySQL(sql1);
				}
				catch (Exception ex)
				{
					log.InfoFormat("Exception while running sql1: {0}", ex.Message);
					throw;
				}

				while (sqlDataReader.Read())
				{
					tableNames.Add((string)sqlDataReader["TABLE_NAME"] + "|");
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
			// SQL Server CE doesn't support user-defined views. It only has a set of built-in system views.
			return new List<string>();
		}

		public static IDatabaseConnector FromConnectionString(string connectionString)
		{
			var sqlConnection = new SqlCeConnection(connectionString);
			return new SQLCEDatabaseConnector(sqlConnection);
		}
	}
}