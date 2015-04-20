using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Devart.Data.Universal;
using log4net;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public interface IOracleDatabaseConnector : IDatabaseConnector
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
		Dictionary<string, Dictionary<string, List<DataRow>>> ColumnsBySchema { get; }
		DataTable Indexes { get; }
		List<DataRow> GetIndexRows(string schemaName, string tableName);
		DataTable IndexReferencedColumns { get; }
		DataTable GetForeignKeyColumns();
		List<string> GetTableNames();
		ConnectionStringHelper ConnectionInformation { get; set; }
		Guid GetUIDForObject(string name);
	}

	public class OracleDatabaseConnector : IOracleDatabaseConnector
	{
		private DataTable _columns;
		private DataTable _indexes;
		private DataTable _indexReferencedColumns;
		private readonly UniConnection uniConnection;
		private DataTable foreignKeyConstraints;
		private DataTable uniqueIndexes;
		private DataTable columnConstraints;
		private DataTable foreignKeyColumns;
		private string _DatabaseName;
		private DataTable _PrimaryKeyColumns;
		private DataTable _ColumnDataTypes;

		public string DatabaseName
		{
			get { return _DatabaseName; }
			set { _DatabaseName = value; }
		}

		public ConnectionStringHelper ConnectionInformation { get; set; }

		public string SchemaFilterCSV { get; set; }

		private static readonly ILog log = LogManager.GetLogger(typeof(OracleDatabaseConnector));

		public OracleDatabaseConnector(ConnectionStringHelper connectionStringHelper, string databaseName)
		{
			try
			{
				string connectionString = connectionStringHelper.GetConnectionStringSqlClient(DatabaseTypes.Oracle);
				log.InfoFormat("Opening Database using connection string: {0}", connectionString);
				uniConnection = new UniConnection(connectionString);
				DatabaseName = databaseName;// uniConnection.Database;

				ConnectionInformation = connectionStringHelper;
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Could not open database connection. See inner exception for details.", e);
			}
		}

		private OracleDatabaseConnector(string connectionString, string databaseName)
		{
			try
			{
				ConnectionInformation = new ConnectionStringHelper(connectionString, DatabaseTypes.Oracle);
				uniConnection = new UniConnection(ConnectionInformation.GetConnectionStringSqlClient());
				DatabaseName = databaseName;
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Could not open database connection. See inner exception for details.", e);
			}
		}

		public static OracleDatabaseConnector FromConnectionString(string connectionString, string databaseName)
		{
			return new OracleDatabaseConnector(connectionString, databaseName);
		}

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
				string where = string.IsNullOrEmpty(SchemaFilterCSV) ? "" : string.Format("WHERE cols.OWNER IN ({0})", SchemaFilterCSV);

				string sql = string.Format(@"
						SELECT 
						  CASE 
							  WHEN cons.CONSTRAINT_TYPE = 'C' THEN 'CHECK'
							  WHEN cons.CONSTRAINT_TYPE = 'O' THEN 'READONLY'
							  WHEN cons.CONSTRAINT_TYPE = 'P' THEN 'PRIMARY KEY'
							  WHEN cons.CONSTRAINT_TYPE = 'R' THEN 'FOREIGN KEY'
							  WHEN cons.CONSTRAINT_TYPE = 'U' THEN 'UNIQUE'
							  WHEN cons.CONSTRAINT_TYPE = 'V' THEN 'CHECK (VIEW)'
						  END AS CONSTRAINT_TYPE,
						  cols.COLUMN_NAME AS COLUMN_NAME,
						  cols.TABLE_NAME,
						  cols.OWNER AS TABLE_SCHEMA
						FROM all_constraints cons
							  INNER JOIN all_cons_columns cols ON cons.constraint_name = cols.constraint_name
						{0}
				", where);

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
				string where = string.IsNullOrEmpty(SchemaFilterCSV) ? "" : string.Format("WHERE cons.OWNER IN ({0})", SchemaFilterCSV);

				string sql = string.Format(@"
						SELECT 
						  cons.CONSTRAINT_NAME AS CONSTRAINT_NAME, 
						  cols.COLUMN_NAME AS COLUMN_NAME,
						  cols.POSITION AS ORDINAL_POSITION,
						  cols.TABLE_NAME
						FROM all_constraints cons
							  INNER JOIN all_cons_columns cols ON cons.constraint_name = cols.constraint_name
						{0}
						ORDER BY ORDINAL_POSITION
						", where);

				foreignKeyColumns = RunQueryDataTable(sql);
			}
			return foreignKeyColumns;
		}

		public void TestConnection()
		{
			//if (string.IsNullOrEmpty(ConnectionInformation.DatabaseName))
			//    throw new DatabaseException("The database name cannot be empty.");

			uniConnection.Open();
			uniConnection.Close();
		}

		public void Open()
		{
			if (uniConnection.State != ConnectionState.Open)
				uniConnection.Open();

			log.InfoFormat("Database {0} opened successfully", uniConnection.Database);

			//DatabaseName = uniConnection.Database;
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

		public DataTableCollection RunQueryMultipleResults(string sql)
		{
			try
			{
				UniCommand sqlCommand = new UniCommand(sql, uniConnection);
				DataTable dataTable = new DataTable();
				DataSet ds = new DataSet();
				UniDataAdapter sqlDataAdapter = new UniDataAdapter(sqlCommand);
				sqlDataAdapter.Fill(ds);
				return ds.Tables;
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
				if (_columns != null)
					return _columns;

				string where = string.IsNullOrEmpty(SchemaFilterCSV) ? "" : string.Format("WHERE OWNER IN ({0})", SchemaFilterCSV);

				string sql = string.Format(@"
									SELECT 
										  0 AS InPrimaryKey,
										  TABLE_NAME,
										  owner TABLE_SCHEMA,
										  COLUMN_NAME,
										  column_id ORDINAL_POSITION,
										  nullable IS_NULLABLE,
										  data_type AS DATA_TYPE,
										  data_length CHARACTER_MAXIMUM_LENGTH,
										  data_default COLUMN_DEFAULT,
										  0 AS IsIdentity, /* TODO: how does Oracle support auto-increment? */
										  0 AS IsComputed,
										  data_precision NUMERIC_PRECISION,
										  data_scale NUMERIC_SCALE
								  FROM all_tab_columns av
								  {0}
								  ORDER BY TABLE_SCHEMA, TABLE_NAME, ORDINAL_POSITION
								  ", where);

				_columns = RunQueryDataTable(sql);

				int primaryKeyOrdinal = -1;// = _columns.Columns["InPrimaryKey"].Ordinal;
				int schemaNameOrdinal = -1;// = _columns.Columns["TABLE_SCHEMA"].Ordinal;
				int tableNameOrdinal = -1;// = _columns.Columns["TABLE_NAME"].Ordinal;
				int columnNameOrdinal = -1;// = _columns.Columns["COLUMN_NAME"].Ordinal;
				int pkTableSchemaOrdinal = -1;// = PrimaryKeyColumns.Columns["TABLE_SCHEMA"].Ordinal;
				int pkTableNameOrdinal = -1;// = PrimaryKeyColumns.Columns["TABLE_NAME"].Ordinal;
				int pkColumnNameOrdinal = -1;// = PrimaryKeyColumns.Columns["COLUMN_NAME"].Ordinal;

				foreach (DataColumn c in _columns.Columns)
				{
					if (c.ColumnName.Equals("InPrimaryKey", StringComparison.InvariantCultureIgnoreCase)) primaryKeyOrdinal = c.Ordinal;
					else if (c.ColumnName.Equals("TABLE_SCHEMA", StringComparison.InvariantCultureIgnoreCase)) schemaNameOrdinal = c.Ordinal;
					else if (c.ColumnName.Equals("TABLE_NAME", StringComparison.InvariantCultureIgnoreCase)) tableNameOrdinal = c.Ordinal;
					else if (c.ColumnName.Equals("COLUMN_NAME", StringComparison.InvariantCultureIgnoreCase)) columnNameOrdinal = c.Ordinal;
				}
				foreach (DataColumn c in PrimaryKeyColumns.Columns)
				{
					if (c.ColumnName.Equals("TABLE_SCHEMA", StringComparison.InvariantCultureIgnoreCase)) pkTableSchemaOrdinal = c.Ordinal;
					else if (c.ColumnName.Equals("TABLE_NAME", StringComparison.InvariantCultureIgnoreCase)) pkTableNameOrdinal = c.Ordinal;
					else if (c.ColumnName.Equals("COLUMN_NAME", StringComparison.InvariantCultureIgnoreCase)) pkColumnNameOrdinal = c.Ordinal;
				}
				string currentSchema = "";
				string currentTable = "";
				_ColumnsBySchema = new Dictionary<string, Dictionary<string, List<DataRow>>>();

				foreach (DataRow row in _columns.Rows)
				{
					if ((string)row[schemaNameOrdinal] != currentSchema)
					{
						currentSchema = (string)row[schemaNameOrdinal];
						_ColumnsBySchema.Add(currentSchema, new Dictionary<string, List<DataRow>>());
					}
					if ((string)row[tableNameOrdinal] != currentTable)
					{
						currentTable = (string)row[tableNameOrdinal];
						_ColumnsBySchema[currentSchema].Add(currentTable, new List<DataRow>());
					}
					_ColumnsBySchema[currentSchema][currentTable].Add(row);
				}
				foreach (DataRow pkRow in PrimaryKeyColumns.Rows)
				{
					try
					{
						if (currentSchema != pkRow[pkTableSchemaOrdinal].ToString())
							currentSchema = pkRow[pkTableSchemaOrdinal].ToString();
					}
					catch (Exception ex)
					{
						throw new Exception(string.Format("pkTableSchemaOrdinal [{0}] not found in pkRow", pkTableSchemaOrdinal));
					}
					try
					{
						if (currentTable != pkRow[pkTableNameOrdinal].ToString())
							currentTable = pkRow[pkTableNameOrdinal].ToString();
					}
					catch (Exception ex)
					{
						throw new Exception(string.Format("pkTableNameOrdinal [{0}] not found in pkRow", pkTableNameOrdinal));
					}
					DataRow row = null;

					//try
					//{
					if (_ColumnsBySchema.ContainsKey(currentSchema) &&
						_ColumnsBySchema[currentSchema].ContainsKey(currentTable))
					{
						row = _ColumnsBySchema[currentSchema][currentTable].SingleOrDefault(r => r[columnNameOrdinal].ToString() == pkRow[pkColumnNameOrdinal].ToString());
					}
					//}
					//catch (Exception ex)
					//{
					//    if (!_ColumnsBySchema.ContainsKey(currentSchema))
					//    {
					//        List<string> availableSchemas = _ColumnsBySchema.Keys.ToList();
					//        string keyString = "";

					//        foreach (string key in availableSchemas)
					//            keyString += key + ", ";

					//        keyString = keyString.TrimEnd(' ', ',');

					//        throw new Exception(string.Format("_ColumnsBySchema doesn't contain key for schema [{0}]. Available keys (schemas): [{1}]", currentSchema, keyString));
					//    }
					//    else if (!_ColumnsBySchema[currentSchema].ContainsKey(currentTable))
					//    {
					//        List<string> availableTables = _ColumnsBySchema[currentSchema].Keys.ToList();
					//        string keyString = "";

					//        foreach (string key in availableTables)
					//            keyString += key + ", ";

					//        keyString = keyString.TrimEnd(' ', ',');

					//        throw new Exception(string.Format("_ColumnsBySchema[{0}] doesn't contain key [{1}]. Available tables: [{2}]", currentSchema, currentTable, keyString));
					//    }
					//}
					if (row != null)
						row[primaryKeyOrdinal] = 1;
				}
				return _columns;
			}
		}

		internal void SetBooleanColumns(IList<ITable> tables, IList<ITable> views)
		{
			List<string> sb = new List<string>();

			foreach (var table in tables)
				foreach (var col in table.Columns.Where(c => c.OriginalDataType == "CHAR" && c.Size == 1))
					sb.Add(string.Format("SELECT DISTINCT '{0}.{1}' AS TABLE_NAME, '{2}' AS COLUMN_NAME, {2} AS VALUE FROM {0}.{1}", table.Schema, table.Name, col.Name));

			if (sb.Count == 0)
				return;

			List<string> trueValues = new List<string>();
			trueValues.Add("t");
			trueValues.Add("y");
			trueValues.Add("1");

			List<string> falseValues = new List<string>();
			falseValues.Add("f");
			falseValues.Add("n");
			falseValues.Add("0");

			for (int i = 0; i < sb.Count; i++)
			{
				DataTable result = RunQueryDataTable(sb[i]);
				// Is this boolean (two values)?
				if (result.Rows.Count != 2)
					continue;

				string columnName = result.Rows[0][1].ToString();
				string value1 = result.Rows[0][2].ToString();
				string value2 = result.Rows[1][2].ToString();
				string value1Lower = value1.ToLowerInvariant();
				string value2Lower = value2.ToLowerInvariant();
				IColumn col = tables[i].Columns.Single(c => c.Name == columnName);

				if (trueValues.Contains(value1Lower))
				{
					col.IsPseudoBoolean = true;
					col.PseudoTrue = value1;
					col.PseudoFalse = value2;
				}
				else if (falseValues.Contains(value1Lower))
				{
					col.IsPseudoBoolean = true;
					col.PseudoFalse = value1;
					col.PseudoTrue = value2;
				}
			}
		}

		private Dictionary<string, Dictionary<string, List<DataRow>>> _ColumnsBySchema;

		public Dictionary<string, Dictionary<string, List<DataRow>>> ColumnsBySchema
		{
			get
			{
				// Ensure Indexes is lazy-loaded
				if (Columns != null)
					return _ColumnsBySchema;

				return null;
			}
		}

		public DataTable PrimaryKeyColumns
		{
			get
			{
				if (_PrimaryKeyColumns == null)
				{
					string where = string.IsNullOrEmpty(SchemaFilterCSV) ? "" : string.Format("AND cons.OWNER IN ({0}) AND cols.OWNER IN ({0})", SchemaFilterCSV);

					string sql = string.Format(@"
							SELECT  cons.owner AS TABLE_SCHEMA,
									cols.table_name AS TABLE_NAME,
									cols.column_name AS COLUMN_NAME,
									cons.CONSTRAINT_NAME AS PRIMARY_KEY_NAME
							FROM all_constraints cons
								  INNER JOIN all_cons_columns cols ON cons.constraint_name = cols.constraint_name
							WHERE cons.constraint_type = 'P' {0}
							ORDER BY cons.owner, cols.table_name, cols.column_name
							", where);

					_PrimaryKeyColumns = RunQueryDataTable(sql);
				}
				return _PrimaryKeyColumns;
			}
		}

		private Dictionary<string, Dictionary<string, string>> _PrimaryKeyNames;

		public Dictionary<string, Dictionary<string, string>> PrimaryKeyNames
		{
			get
			{
				if (_PrimaryKeyNames == null)
				{
					string where = string.IsNullOrEmpty(SchemaFilterCSV) ? "" : string.Format("AND cons.OWNER IN ({0})", SchemaFilterCSV);

					string sql = string.Format(@"
							SELECT  cons.owner AS TABLE_SCHEMA,
									cons.table_name AS TABLE_NAME,
									cons.CONSTRAINT_NAME AS PRIMARY_KEY_NAME
							FROM all_constraints cons
							WHERE cons.constraint_type = 'P' {0}
							ORDER BY cons.owner, cons.table_name, cons.CONSTRAINT_NAME
							", where);

					DataTable result = RunQueryDataTable(sql);

					_PrimaryKeyNames = new Dictionary<string, Dictionary<string, string>>();
					string currentSchema = null;

					foreach (DataRow row in result.Rows)
					{
						var schema = (string)row[0];
						var table = (string)row[1];
						var pkName = (string)row[2];

						if (schema != currentSchema)
						{
							_PrimaryKeyNames.Add(schema, new Dictionary<string, string>());
							currentSchema = schema;
						}
						_PrimaryKeyNames[schema].Add(table, pkName);
					}
				}
				return _PrimaryKeyNames;
			}
		}

		public DataTable Indexes
		{
			get
			{
				if (_indexes == null)
				{
					string where = string.IsNullOrEmpty(SchemaFilterCSV) ? "" : string.Format("WHERE i.OWNER IN ({0})", SchemaFilterCSV);

					string sql = string.Format(@"
								SELECT 
										i.TABLE_NAME AS TABLE_NAME, 
										  i.OWNER AS TABLE_SCHEMA,
										  cols.COLUMN_NAME AS COLUMN_NAME, 
										  i.INDEX_NAME AS INDEX_NAME,
										/*i.INDEX_TYPE AS CONSTRAINT_TYPE,
										C.DATA_TYPE AS DATA_TYPE,*/
										CAST(0 AS INT) AS IS_CLUSTERED,
										CAST(CASE WHEN i.UNIQUENESS = 'UNIQUE' THEN 1 ELSE 0 END AS INT) AS IS_UNIQUE,
										CASE 
											WHEN cons.CONSTRAINT_TYPE = 'C' THEN 'CHECK'
											WHEN cons.CONSTRAINT_TYPE = 'O' THEN 'READONLY'
											WHEN cons.CONSTRAINT_TYPE = 'P' THEN 'PRIMARY KEY'
											WHEN cons.CONSTRAINT_TYPE = 'R' THEN 'FOREIGN KEY'
											WHEN cons.CONSTRAINT_TYPE = 'U' THEN 'UNIQUE'
											WHEN cons.CONSTRAINT_TYPE = 'V' THEN 'CHECK (VIEW)'
										END AS CONSTRAINT_TYPE
								FROM all_indexes i
									  INNER JOIN all_cons_columns cols ON i.INDEX_NAME = cols.CONSTRAINT_NAME
									  inner join all_constraints cons on i.INDEX_NAME = cons.CONSTRAINT_NAME
								{0}
								ORDER BY TABLE_SCHEMA, TABLE_NAME, INDEX_NAME
								", where);

					_indexes = RunQueryDataTable(sql);
					_IndexesBySchema = new Dictionary<string, Dictionary<string, List<DataRow>>>();
					string currentSchema = "";
					string currentTable = "";
					int ordSchema = -1;// _indexes.Columns["TABLE_SCHEMA"].Ordinal;
					int ordTable = -1;// _indexes.Columns["TABLE_NAME"].Ordinal;

					foreach (DataColumn c in _indexes.Columns)
					{
						if (c.ColumnName.Equals("TABLE_SCHEMA", StringComparison.InvariantCultureIgnoreCase)) ordSchema = c.Ordinal;
						else if (c.ColumnName.Equals("TABLE_NAME", StringComparison.InvariantCultureIgnoreCase)) ordTable = c.Ordinal;
					}
					foreach (DataRow row in _indexes.Rows)
					{
						if ((string)row[ordSchema] != currentSchema)
						{
							currentSchema = (string)row[ordSchema];
							_IndexesBySchema.Add(currentSchema, new Dictionary<string, List<DataRow>>());
						}
						if ((string)row[ordTable] != currentTable)
						{
							currentTable = (string)row[ordTable];
							_IndexesBySchema[currentSchema].Add(currentTable, new List<DataRow>());
						}
						_IndexesBySchema[currentSchema][currentTable].Add(row);
					}
				}
				return _indexes;
			}
		}

		private Dictionary<string, Dictionary<string, List<DataRow>>> _IndexesBySchema;

		private Dictionary<string, Dictionary<string, List<DataRow>>> IndexesBySchema
		{
			get
			{
				// Ensure Indexes is lazy-loaded
				if (Indexes != null)
					return _IndexesBySchema;

				return null;
			}
		}

		public List<DataRow> GetIndexRows(string schemaName, string tableName)
		{
			if (IndexesBySchema.ContainsKey(schemaName) && IndexesBySchema[schemaName].ContainsKey(tableName))
				return IndexesBySchema[schemaName][tableName];

			return new List<DataRow>();
		}

		public DataTable IndexReferencedColumns
		{
			get
			{
				if (_indexReferencedColumns == null)
				{
					string where = string.IsNullOrEmpty(SchemaFilterCSV) ? "" : string.Format("AND cols.OWNER IN ({0})", SchemaFilterCSV);

					string sql = string.Format(@"
								SELECT 
								  cols.TABLE_NAME AS ReferencedTable, 
								  cols.COLUMN_NAME AS ReferencedColumn, 
								  cons.R_CONSTRAINT_NAME AS ReferencedKey, 
								  cons.CONSTRAINT_NAME AS ForeignKey
								FROM all_constraints cons
									  INNER JOIN all_cons_columns cols ON cons.r_constraint_name = cols.constraint_name
								WHERE cons.CONSTRAINT_TYPE = 'R' {0}
								ORDER BY ReferencedTable, cons.CONSTRAINT_NAME, cols.POSITION
								", where);

					_indexReferencedColumns = RunQueryDataTable(sql);
				}
				return _indexReferencedColumns;
			}
		}

		public List<string> GetTableNames()
		{
			string where = string.IsNullOrEmpty(SchemaFilterCSV) ? "WHERE NESTED <> 'YES'" : string.Format("WHERE OWNER IN ({0}) AND NESTED <> 'YES'", SchemaFilterCSV);

			string sql1 = string.Format(@"
							SELECT 
							  TABLE_NAME,
							  OWNER AS TABLE_SCHEMA,
							  0 AS IsSystemObject
							FROM All_Tables 
							{0}
							ORDER BY TABLE_NAME
							", where);

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
			string where = string.IsNullOrEmpty(SchemaFilterCSV) ? "" : string.Format("WHERE OWNER IN ({0})", SchemaFilterCSV);

			string sql1 = string.Format(@"
							SELECT 
							  VIEW_NAME,
							  OWNER AS VIEW_SCHEMA,
							  0 AS IsSystemObject
							FROM All_Views 
							{0}
							ORDER BY VIEW_NAME
							", where);

			List<string> viewNames = new List<string>();
			UniDataReader sqlDataReader = null;

			try
			{
				sqlDataReader = RunQuerySQL(sql1);

				// Exclude system tables
				int isSysObjectColumnOrdinal = sqlDataReader.GetOrdinal("IsSystemObject");
				int ordTableName = sqlDataReader.GetOrdinal("VIEW_NAME");
				int ordTableSchema = sqlDataReader.GetOrdinal("VIEW_SCHEMA");

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
				string where = string.IsNullOrEmpty(SchemaFilterCSV) ? "" : string.Format("AND pc.OWNER IN ({0})", SchemaFilterCSV);

				string sql = string.Format(@"
						SELECT DISTINCT
							pc.CONSTRAINT_NAME AS PrimaryKeyName,
							pc.TABLE_NAME AS PrimaryTable,
							pc.OWNER AS PrimaryTableSchema,
							fc.CONSTRAINT_NAME AS ForeignKeyName,
							fc.TABLE_NAME AS ForeignTable,
							fc.OWNER AS ForeignTableSchema
						FROM all_constraints fc
							INNER JOIN all_constraints pc ON fc.R_CONSTRAINT_NAME = pc.CONSTRAINT_NAME
						WHERE fc.CONSTRAINT_TYPE = 'R' {0}
						ORDER BY ForeignKeyName
						", where);

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
				string where = string.IsNullOrEmpty(SchemaFilterCSV) ? "" : string.Format("AND i.OWNER IN ({0})", SchemaFilterCSV);

				string sql = string.Format(@"
							SELECT DISTINCT
							  i.TABLE_NAME,
							  i.OWNER AS TABLE_SCHEMA,
							  COLUMN_NAME,
							  i.INDEX_NAME,
							  CASE 
								  WHEN cons.CONSTRAINT_TYPE = 'C' THEN 'CHECK'
								  WHEN cons.CONSTRAINT_TYPE = 'O' THEN 'READONLY'
								  WHEN cons.CONSTRAINT_TYPE = 'P' THEN 'PRIMARY KEY'
								  WHEN cons.CONSTRAINT_TYPE = 'R' THEN 'FOREIGN KEY'
								  WHEN cons.CONSTRAINT_TYPE = 'U' THEN 'UNIQUE'
								  WHEN cons.CONSTRAINT_TYPE = 'V' THEN 'CHECK (VIEW)'
							  END AS CONSTRAINT_TYPE,
							  0 AS IS_CLUSTERED,
							  CASE WHEN i.UNIQUENESS = 'UNIQUE' THEN 1 ELSE 0 END AS IS_UNIQUE,
							  cols.POSITION AS ORDINAL_POSITION
							FROM all_indexes i
								  INNER JOIN all_constraints cons on i.INDEX_NAME = cons.CONSTRAINT_NAME
								  INNER JOIN all_cons_columns cols ON i.INDEX_NAME = cols.constraint_name
							WHERE UNIQUENESS = 'UNIQUE' {0}
							 ORDER BY TABLE_NAME, INDEX_NAME, ORDINAL_POSITION
							", where);

				uniqueIndexes = RunQueryDataTable(sql);
			}
			return uniqueIndexes;
		}
	}
}
