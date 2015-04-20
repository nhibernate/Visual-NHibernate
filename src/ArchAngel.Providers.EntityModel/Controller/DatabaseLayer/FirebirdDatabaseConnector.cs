using System;
using System.Collections.Generic;
using System.Data;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using FirebirdSql.Data.FirebirdClient;
using log4net;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public interface IFirebirdDatabaseConnector : IDatabaseConnector
	{
		DataTable RunQueryDataTable(string sql);
		FbDataReader RunQuerySQL(string sql);
		void RunNonQuerySQL(string sql);
		FbDataReader RunStoredProcedure(string storedProcedureName);
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

	public class FirebirdDatabaseConnector : IFirebirdDatabaseConnector
	{
		private DataTable _columns;
		private DataTable _indexes;
		private DataTable _indexReferencedColumns;
		private readonly FbConnection sqlConnection;
		private DataTable foreignKeyConstraints;
		private DataTable uniqueIndexes;
		private DataTable columnConstraints;
		private DataTable foreignKeyColumns;

		public string DatabaseName { get; set; }
		public ConnectionStringHelper ConnectionInformation { get; set; }

		private static readonly ILog log = LogManager.GetLogger(typeof(FirebirdDatabaseConnector));

		public FirebirdDatabaseConnector(ConnectionStringHelper connectionStringHelper)
		{
			try
			{
				string connectionString = connectionStringHelper.GetConnectionStringSqlClient(DatabaseTypes.Firebird);
				log.InfoFormat("Opening Database using connection string: {0}", connectionString);
				sqlConnection = new FbConnection(connectionString);
				DatabaseName = sqlConnection.Database;

				ConnectionInformation = connectionStringHelper;
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Could not open database connection. See inner exception for details.", e);
			}
		}

		private FirebirdDatabaseConnector(string connectionString)
		{
			try
			{
				sqlConnection = new FbConnection(connectionString);
				DatabaseName = sqlConnection.Database;

				ConnectionInformation = new ConnectionStringHelper(connectionString, DatabaseTypes.Firebird);
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Could not open database connection. See inner exception for details.", e);
			}
		}

		public static FirebirdDatabaseConnector FromConnectionString(string connectionString)
		{
			return new FirebirdDatabaseConnector(connectionString);
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
							TRIM(rc.RDB$CONSTRAINT_TYPE) AS CONSTRAINT_TYPE,
							TRIM(RDB$FIELD_NAME) AS COLUMN_NAME,
							TRIM(rc.RDB$RELATION_NAME) AS TABLE_NAME,
							'' AS TABLE_SCHEMA       
					 FROM RDB$INDEX_SEGMENTS s
						  LEFT JOIN RDB$INDICES i ON i.RDB$INDEX_NAME = s.RDB$INDEX_NAME
						  LEFT JOIN RDB$RELATION_CONSTRAINTS rc ON rc.RDB$INDEX_NAME = s.RDB$INDEX_NAME
					 WHERE rc.RDB$CONSTRAINT_TYPE IS NOT NULL
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
					 SELECT DISTINCT
							TRIM(RDB$CONSTRAINT_NAME) AS CONSTRAINT_NAME,
							TRIM(RDB$FIELD_NAME) AS COLUMN_NAME,
							TRIM(i.RDB$RELATION_NAME) AS TABLE_NAME,
							RDB$FIELD_POSITION AS ORDINAL_POSITION
					 FROM RDB$INDEX_SEGMENTS s
						  LEFT JOIN RDB$INDICES i ON i.RDB$INDEX_NAME = s.RDB$INDEX_NAME
						  LEFT JOIN RDB$RELATION_CONSTRAINTS rc ON rc.RDB$INDEX_NAME = s.RDB$INDEX_NAME
					 WHERE rc.RDB$CONSTRAINT_TYPE = 'FOREIGN KEY'
					";

				foreignKeyColumns = RunQueryDataTable(sql);
			}
			return foreignKeyColumns;
		}

		public void TestConnection()
		{
			if (string.IsNullOrEmpty(ConnectionInformation.FileName))
				throw new DatabaseException("The filename name cannot be empty.");

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
				using (FbCommand sqlCommand = new FbCommand(sql, sqlConnection))
				{
					DataTable dataTable = new DataTable();
					FbDataAdapter sqlDataAdapter = new FbDataAdapter(sqlCommand);
					sqlDataAdapter.Fill(dataTable); // Just pass the DataTable into the SqlDataAdapters Fill Method
					return dataTable;
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("Error running SQL: [{0}]", sql), e);
			}
		}

		public FbDataReader RunQuerySQL(string sql)
		{
			throw new Exception("Firebird can't use this method as connection gets closed.");
			//using (FbCommand sqlCommand = new FbCommand(sql, sqlConnection))
			//    return sqlCommand.ExecuteReader(CommandBehavior.SequentialAccess);
		}

		public void RunNonQuerySQL(string sql)
		{
			try
			{
				using (FbCommand sqlCommand = new FbCommand(sql, sqlConnection))
				{
					sqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("Error running SQL: [{0}]", sql), e);
			}
		}

		public FbDataReader RunStoredProcedure(string storedProcedureName)
		{
			try
			{
				using (FbCommand sqlCommand = new FbCommand(storedProcedureName, sqlConnection))
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
				using (FbCommand sqlCommand = new FbCommand(storedProcedureName, sqlConnection))
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
							SELECT  DISTINCT
									(       SELECT COUNT (segments.RDB$FIELD_NAME)
											FROM RDB$INDEX_SEGMENTS segments
												 LEFT JOIN RDB$INDICES indices ON indices.RDB$INDEX_NAME = segments.RDB$INDEX_NAME
												 LEFT JOIN RDB$RELATION_CONSTRAINTS relconstraints ON relconstraints.RDB$INDEX_NAME = segments.RDB$INDEX_NAME
											WHERE relconstraints.RDB$RELATION_NAME = r.RDB$RELATION_NAME AND
												  segments.RDB$FIELD_NAME = r.RDB$FIELD_NAME AND
												  relconstraints.RDB$CONSTRAINT_TYPE = 'PRIMARY KEY'
									) AS InPrimaryKey,
									TRIM(r.RDB$RELATION_NAME) AS TABLE_NAME,
									'' AS TABLE_SCHEMA,
									TRIM(r.RDB$FIELD_NAME) AS COLUMN_NAME,
									r.RDB$FIELD_POSITION AS ORDINAL_POSITION,
									CASE WHEN r.RDB$NULL_FLAG > 0 THEN 0 ELSE 1 END AS IS_NULLABLE,
									TRIM(t.RDB$TYPE_NAME) AS DATA_TYPE,
									f.RDB$FIELD_LENGTH AS CHARACTER_MAXIMUM_LENGTH,
									TRIM(r.RDB$DESCRIPTION) AS field_description,
									'' AS COLUMN_DEFAULT, --TRIM(r.RDB$DEFAULT_VALUE) AS COLUMN_DEFAULT,
									CASE 
										 WHEN (
											  SELECT COUNT(D1.RDB$DEPENDENT_NAME) 
											  FROM RDB$DEPENDENCIES D1
												   INNER JOIN RDB$DEPENDENCIES D2 ON D1.RDB$DEPENDENT_NAME = D2.RDB$DEPENDENT_NAME AND
																   D2.RDB$DEPENDED_ON_TYPE = 14
											  WHERE D1.RDB$DEPENDED_ON_TYPE = 0 AND
													D1.RDB$DEPENDED_ON_NAME = r.RDB$RELATION_NAME AND
													D1.RDB$FIELD_NAME = r.RDB$FIELD_NAME) > 0 THEN 1 
										 ELSE 0 
									END AS IsIdentity,
									0 AS IsComputed,
									f.RDB$FIELD_PRECISION AS NUMERIC_PRECISION,
									f.RDB$FIELD_SCALE AS NUMERIC_SCALE
							   FROM RDB$RELATION_FIELDS r
									LEFT JOIN RDB$FIELDS f ON r.RDB$FIELD_SOURCE = f.RDB$FIELD_NAME
									INNER JOIN RDB$TYPES t ON f.RDB$FIELD_TYPE = t.RDB$TYPE AND t.RDB$FIELD_NAME = 'RDB$FIELD_TYPE'
									LEFT JOIN RDB$COLLATIONS coll ON f.RDB$COLLATION_ID = coll.RDB$COLLATION_ID
									LEFT JOIN RDB$CHARACTER_SETS cset ON f.RDB$CHARACTER_SET_ID = cset.RDB$CHARACTER_SET_ID
							ORDER BY TABLE_NAME, ORDINAL_POSITION
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
									TRIM(RDB$INDICES.RDB$RELATION_NAME) AS TABLE_NAME,
									'' AS TABLE_SCHEMA,
									TRIM(RDB$INDEX_SEGMENTS.RDB$FIELD_NAME) AS COLUMN_NAME,
									TRIM(RDB$RELATION_CONSTRAINTS.RDB$CONSTRAINT_NAME) AS INDEX_NAME,
									TRIM(RDB$RELATION_CONSTRAINTS.RDB$CONSTRAINT_TYPE) AS CONSTRAINT_TYPE,
									0 AS IS_CLUSTERED,
									CASE WHEN RDB$UNIQUE_FLAG > 0 THEN 1 ELSE 0 END AS IS_UNIQUE
							FROM RDB$INDEX_SEGMENTS
								LEFT JOIN RDB$INDICES ON RDB$INDICES.RDB$INDEX_NAME = RDB$INDEX_SEGMENTS.RDB$INDEX_NAME
								LEFT JOIN RDB$RELATION_CONSTRAINTS ON RDB$RELATION_CONSTRAINTS.RDB$INDEX_NAME = RDB$INDEX_SEGMENTS.RDB$INDEX_NAME
							WHERE RDB$RELATION_CONSTRAINTS.RDB$CONSTRAINT_TYPE IS NOT NULL
							ORDER BY    RDB$INDICES.RDB$RELATION_NAME,
										RDB$RELATION_CONSTRAINTS.RDB$CONSTRAINT_NAME,
										RDB$INDEX_SEGMENTS.RDB$FIELD_POSITION
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
						SELECT DISTINCT
							TRIM(rc.RDB$CONSTRAINT_NAME) AS ForeignKey,
							TRIM(refc.RDB$CONST_NAME_UQ) AS ReferencedKey,
							TRIM(d2.RDB$DEPENDED_ON_NAME) AS ReferencedTable,
							TRIM(d2.RDB$FIELD_NAME) AS ReferencedColumn
						FROM RDB$RELATION_CONSTRAINTS AS rc
							LEFT JOIN RDB$REF_CONSTRAINTS refc ON rc.RDB$CONSTRAINT_NAME = refc.RDB$CONSTRAINT_NAME
							LEFT JOIN RDB$DEPENDENCIES d1 ON d1.RDB$DEPENDED_ON_NAME = rc.RDB$RELATION_NAME
							LEFT JOIN RDB$DEPENDENCIES d2 ON d1.RDB$DEPENDENT_NAME = d2.RDB$DEPENDENT_NAME
						WHERE rc.RDB$CONSTRAINT_TYPE = 'FOREIGN KEY'
							AND d1.RDB$DEPENDED_ON_NAME <> d2.RDB$DEPENDED_ON_NAME
							AND d1.RDB$FIELD_NAME <> d2.RDB$FIELD_NAME
						";
					_indexReferencedColumns = RunQueryDataTable(sql);
				}

				return _indexReferencedColumns;
			}
		}

		public List<string> GetTableNames()
		{
			const string sql1 = @"
							SELECT DISTINCT 
								TRIM(RDB$RELATION_NAME) AS TABLE_NAME,
								'' AS TABLE_SCHEMA,
								RDB$SYSTEM_FLAG AS IsSystemObject
							FROM RDB$RELATION_FIELDS
							WHERE (RDB$SYSTEM_FLAG IS NULL OR RDB$SYSTEM_FLAG = 0)
								AND RDB$VIEW_CONTEXT IS NULL";

			List<string> tableNames = new List<string>();
			DataTable dt = RunQueryDataTable(sql1);

			foreach (DataRow row in dt.Rows)
				tableNames.Add(((string)row["TABLE_NAME"]).Trim() + "|" + ((string)row["TABLE_SCHEMA"]).Trim());

			return tableNames;
		}

		public List<string> GetViewNames()
		{
			const string sql1 = @"
							SELECT DISTINCT 
								TRIM(RDB$RELATION_NAME) AS TABLE_NAME,
								'' AS TABLE_SCHEMA,
								RDB$SYSTEM_FLAG AS IsSystemObject
							FROM RDB$RELATION_FIELDS
							WHERE (RDB$SYSTEM_FLAG IS NULL OR RDB$SYSTEM_FLAG = 0)
								AND RDB$VIEW_CONTEXT IS NOT NULL";

			List<string> viewNames = new List<string>();
			DataTable dt = RunQueryDataTable(sql1);

			foreach (DataRow row in dt.Rows)
				viewNames.Add(((string)row["TABLE_NAME"]).Trim() + "|" + ((string)row["TABLE_SCHEMA"]).Trim());

			return viewNames;
		}

		public DataTable GetForeignKeyConstraints()
		{
			// I am so sorry for this. SQL Server is a PITA, and does have index information in the INFORMATION_SCHEMA. Meaning you have to select it
			// from all over the place.
			if (foreignKeyConstraints == null)
			{
				const string sql =
					//                    @"
					//                    SELECT DISTINCT 
					//                        TRIM(refc.RDB$CONST_NAME_UQ) AS PrimaryKeyName,
					//                        TRIM(d2.RDB$DEPENDED_ON_NAME) AS PrimaryTable,
					//                        TRIM(rc.RDB$CONSTRAINT_NAME) AS ForeignKeyName,
					//                        TRIM(rc.RDB$RELATION_NAME) AS ForeignTable,
					//                        t.RDB$TYPE_NAME
					//                    FROM RDB$RELATION_CONSTRAINTS AS rc
					//                        LEFT JOIN RDB$REF_CONSTRAINTS refc ON rc.RDB$CONSTRAINT_NAME = refc.RDB$CONSTRAINT_NAME
					//                        LEFT JOIN RDB$DEPENDENCIES d1 ON d1.RDB$DEPENDED_ON_NAME = rc.RDB$RELATION_NAME
					//                        LEFT JOIN RDB$DEPENDENCIES d2 ON d1.RDB$DEPENDENT_NAME = d2.RDB$DEPENDENT_NAME
					//                        LEFT JOIN RDB$TYPES t ON t.RDB$TYPE = d1.RDB$DEPENDENT_TYPE AND t.RDB$FIELD_NAME = 'RDB$OBJECT_TYPE'
					//                    WHERE rc.RDB$CONSTRAINT_TYPE = 'FOREIGN KEY'
					//                        AND d1.RDB$DEPENDED_ON_NAME <> d2.RDB$DEPENDED_ON_NAME
					//                        AND d1.RDB$FIELD_NAME <> d2.RDB$FIELD_NAME
					//                        AND t.RDB$TYPE_NAME = 'TRIGGER' -- IN ('TRIGGER', 'PROCEDURE')
					//                    ";
					@"
					select DISTINCT 
						TRIM(rc.RDB$RELATION_NAME) AS ForeignTable,
						TRIM(rc.RDB$CONSTRAINT_NAME) AS ForeignKeyName,
						'' AS ForeignSchema,
						TRIM(rcP.RDB$RELATION_NAME) AS PrimaryTable,
						TRIM(rcP.RDB$CONSTRAINT_NAME) AS PrimaryKeyName,
						'' AS PrimarySchema
					from RDB$RELATION_CONSTRAINTS rc
						inner join RDB$REF_CONSTRAINTS refc on refc.RDB$CONSTRAINT_NAME = rc.RDB$CONSTRAINT_NAME
						inner join RDB$RELATION_CONSTRAINTS rcP on rcP.RDB$CONSTRAINT_NAME = refc.RDB$CONST_NAME_UQ
					where rc.RDB$CONSTRAINT_TYPE = 'FOREIGN KEY'
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
							SELECT  DISTINCT
									TRIM(RDB$INDICES.RDB$RELATION_NAME) AS TABLE_NAME,
									'' AS TABLE_SCHEMA,
									TRIM(RDB$INDEX_SEGMENTS.RDB$FIELD_NAME) AS COLUMN_NAME,
									TRIM(RDB$INDICES.RDB$INDEX_NAME) AS INDEX_NAME,
									TRIM(RDB$RELATION_CONSTRAINTS.RDB$CONSTRAINT_TYPE) AS CONSTRAINT_TYPE,
									0 AS IS_CLUSTERED,
									CASE WHEN RDB$UNIQUE_FLAG > 0 THEN 1 ELSE 0 END AS IS_UNIQUE
							FROM RDB$INDEX_SEGMENTS
								LEFT JOIN RDB$INDICES ON RDB$INDICES.RDB$INDEX_NAME = RDB$INDEX_SEGMENTS.RDB$INDEX_NAME
								LEFT JOIN RDB$RELATION_CONSTRAINTS ON RDB$RELATION_CONSTRAINTS.RDB$INDEX_NAME = RDB$INDEX_SEGMENTS.RDB$INDEX_NAME
							WHERE RDB$RELATION_CONSTRAINTS.RDB$CONSTRAINT_TYPE IS NOT NULL AND
								RDB$UNIQUE_FLAG = 1
							ORDER BY RDB$INDEX_SEGMENTS.RDB$FIELD_POSITION
						";
				uniqueIndexes = RunQueryDataTable(sql);
			}

			return uniqueIndexes;
		}
	}
}