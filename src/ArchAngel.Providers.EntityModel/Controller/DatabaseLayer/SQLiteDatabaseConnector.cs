using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.SQLiteParsing;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Devart.Data.Universal;
using log4net;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public interface ISQLiteDatabaseConnector : IDatabaseConnector
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

	public class SQLiteDatabaseConnector : ISQLiteDatabaseConnector
	{
		private DataTable _columns;
		private DataTable _indexes;
		private DataTable _indexReferencedColumns;
		//private readonly UniConnection uniConnection;
		private DataTable foreignKeyConstraints;
		private DataTable uniqueIndexes;
		private DataTable columnConstraints;
		private DataTable foreignKeyColumns;

		public string DatabaseName { get; set; }
		public ConnectionStringHelper ConnectionInformation { get; set; }

		private static readonly ILog log = LogManager.GetLogger(typeof(SQLiteDatabaseConnector));

		public SQLiteDatabaseConnector(ConnectionStringHelper connectionStringHelper)
		{
			try
			{
				Init();
				string connectionString = connectionStringHelper.GetConnectionStringSqlClient(DatabaseTypes.SQLite);
				DatabaseName = connectionStringHelper.FileName;
				ConnectionInformation = connectionStringHelper;
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Could not open database connection. See inner exception for details.", e);
			}
		}

		private SQLiteDatabaseConnector(string connectionString)
		{
			try
			{
				Init();
				//ConnectionInformation = new ConnectionStringHelper(connectionString, DatabaseTypes.SQLite);
				//uniConnection = new UniConnection(ConnectionInformation.GetConnectionStringSqlClient());
				DatabaseName = "";// uniConnection.Database;
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Could not open database connection. See inner exception for details.", e);
			}
		}

		private void Init()
		{
			_columns = new DataTable();
			_columns.Columns.Add("InPrimaryKey", typeof(bool));
			_columns.Columns.Add("TABLE_NAME", typeof(string));
			_columns.Columns.Add("TABLE_SCHEMA", typeof(string));
			_columns.Columns.Add("COLUMN_NAME", typeof(string));
			_columns.Columns.Add("ORDINAL_POSITION", typeof(int));
			_columns.Columns.Add("IS_NULLABLE", typeof(bool));
			_columns.Columns.Add("DATA_TYPE", typeof(string));
			_columns.Columns.Add("CHARACTER_MAXIMUM_LENGTH", typeof(long));
			_columns.Columns.Add("COLUMN_DEFAULT", typeof(string));
			_columns.Columns.Add("IsIdentity", typeof(bool));
			_columns.Columns.Add("IsComputed", typeof(bool));
			_columns.Columns.Add("NUMERIC_PRECISION", typeof(int));
			_columns.Columns.Add("NUMERIC_SCALE", typeof(int));

			_indexes = new DataTable();
			_indexes.Columns.Add("TABLE_NAME", typeof(string));
			_indexes.Columns.Add("TABLE_SCHEMA", typeof(string));
			_indexes.Columns.Add("COLUMN_NAME", typeof(string));
			_indexes.Columns.Add("INDEX_NAME", typeof(string));
			_indexes.Columns.Add("CONSTRAINT_TYPE", typeof(string));
			_indexes.Columns.Add("DATA_TYPE", typeof(string));
			_indexes.Columns.Add("IS_CLUSTERED", typeof(bool));
			_indexes.Columns.Add("IS_UNIQUE", typeof(bool));

			foreignKeyColumns = new DataTable();
			foreignKeyColumns.Columns.Add("CONSTRAINT_NAME", typeof(string));
			foreignKeyColumns.Columns.Add("COLUMN_NAME", typeof(string));
			foreignKeyColumns.Columns.Add("ORDINAL_POSITION", typeof(int));
			foreignKeyColumns.Columns.Add("TABLE_NAME", typeof(string));

			foreignKeyConstraints = new DataTable();
			foreignKeyConstraints.Columns.Add("PrimaryKeyName", typeof(string));
			foreignKeyConstraints.Columns.Add("PrimaryTable", typeof(string));
			foreignKeyConstraints.Columns.Add("PrimaryTableSchema", typeof(string));
			foreignKeyConstraints.Columns.Add("ForeignKeyName", typeof(string));
			foreignKeyConstraints.Columns.Add("ForeignTable", typeof(string));
			foreignKeyConstraints.Columns.Add("ForeignTableSchema", typeof(string));

			uniqueIndexes = new DataTable();
			uniqueIndexes.Columns.Add("TABLE_NAME", typeof(string));
			uniqueIndexes.Columns.Add("TABLE_SCHEMA", typeof(string));
			uniqueIndexes.Columns.Add("COLUMN_NAME", typeof(string));
			uniqueIndexes.Columns.Add("INDEX_NAME", typeof(string));
			uniqueIndexes.Columns.Add("CONSTRAINT_TYPE", typeof(string));
			uniqueIndexes.Columns.Add("DATA_TYPE", typeof(string));
			uniqueIndexes.Columns.Add("IS_CLUSTERED", typeof(bool));
			uniqueIndexes.Columns.Add("IS_UNIQUE", typeof(bool));
			uniqueIndexes.Columns.Add("ORDINAL_POSITION", typeof(int));

			_indexReferencedColumns = new DataTable();
			_indexReferencedColumns.Columns.Add("ReferencedTable", typeof(string));
			_indexReferencedColumns.Columns.Add("ReferencedColumn", typeof(string));
			_indexReferencedColumns.Columns.Add("ReferencedKey", typeof(string));
			_indexReferencedColumns.Columns.Add("ForeignKey", typeof(string));

			columnConstraints = new DataTable();
			columnConstraints.Columns.Add("CONSTRAINT_TYPE", typeof(string));
			columnConstraints.Columns.Add("COLUMN_NAME", typeof(string));
			columnConstraints.Columns.Add("TABLE_NAME", typeof(string));
			columnConstraints.Columns.Add("TABLE_SCHEMA", typeof(string));
		}

		private void AddColumn(
								bool inPrimaryKey,
								string tableName,
								string tableSchema,
								string columnName,
								int ordinalPosition,
								bool isNullable,
								string dataType,
								long characterMaxLength,
								string defaultValue,
								bool isIdentity,
								bool isComputed,
								int numericPrecision,
								int numericScale)
		{
			DataRow row = _columns.NewRow();
			row[0] = inPrimaryKey;
			row[1] = tableName;
			row[2] = tableSchema;
			row[3] = columnName;
			row[4] = ordinalPosition;
			row[5] = isNullable;
			row[6] = dataType;
			row[7] = characterMaxLength;
			row[8] = defaultValue;
			row[9] = isIdentity;
			row[10] = isComputed;
			row[11] = numericPrecision;
			row[11] = numericScale;

			_columns.Rows.Add(row);
		}

		private void AddIndex(
								string tableName,
								string tableSchema,
								string columnName,
								string indexName,
								string constraintType,
								string dataType,
								bool isClustered,
								bool isUnique)
		{
			DataRow row = _indexes.NewRow();
			row[0] = tableName;
			row[1] = tableSchema;
			row[2] = columnName;
			row[3] = indexName;
			row[4] = constraintType;
			row[5] = dataType;
			row[6] = isClustered;
			row[7] = isUnique;

			_indexes.Rows.Add(row);
		}

		private void AddForeignKeyColumn(
						string constraintName,
						string columnName,
						int ordinalPosition,
						string tableName)
		{
			DataRow row = foreignKeyColumns.NewRow();
			row[0] = constraintName;
			row[1] = columnName;
			row[2] = ordinalPosition;
			row[3] = tableName;

			foreignKeyColumns.Rows.Add(row);
		}

		private void AddForeignKeyConstraint(
						string primaryKeyName,
						string primaryTable,
						string primaryTableSchema,
						string foreignKeyName,
						string foreignTable,
						string foreignTableSchema)
		{
			DataRow row = foreignKeyConstraints.NewRow();
			row[0] = primaryKeyName;
			row[1] = primaryTable;
			row[2] = primaryTableSchema;
			row[3] = foreignKeyName;
			row[4] = foreignTable;
			row[5] = foreignTableSchema;

			foreignKeyConstraints.Rows.Add(row);
		}

		private void AddUniqueIndex(
									string tableName,
									string tableSchema,
									string columnName,
									string indexName,
									string constraintType,
									string dataType,
									bool isClustered,
									bool isUnique,
									int ordinalPosition)
		{
			DataRow row = uniqueIndexes.NewRow();
			row[0] = tableName;
			row[1] = tableSchema;
			row[2] = columnName;
			row[3] = indexName;
			row[4] = constraintType;
			row[6] = dataType;
			row[7] = isClustered;
			row[8] = isUnique;
			row[9] = ordinalPosition;

			uniqueIndexes.Rows.Add(row);
		}

		private void AddIndexReferencedColumn(
									string referencedTable,
									string referencedColumn,
									string referencedKey,
									string foreignKey)
		{
			DataRow row = _indexReferencedColumns.NewRow();
			row[0] = referencedTable;
			row[1] = referencedColumn;
			row[2] = referencedKey;
			row[3] = foreignKey;

			_indexReferencedColumns.Rows.Add(row);
		}

		private void AddColumnConstraint(
							string constraintType,
							string columnName,
							string tableName,
							string tableSchema)
		{
			DataRow row = columnConstraints.NewRow();
			row[0] = constraintType;
			row[1] = columnName;
			row[2] = tableName;
			row[3] = tableSchema;

			columnConstraints.Rows.Add(row);
		}

		private static bool RunSQLiteCommandLine(string databaseFilePath, string command, out string output)
		{
			string exePath = "";
#if DEBUG
			exePath = Slyce.Common.RelativePaths.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location, @"..\..\..\..\3rd_Party_Libs\sqlite3.exe");
#else
			exePath = @"sqlite3.exe";
#endif
			StringBuilder sb = new StringBuilder(500);

			try
			{
				databaseFilePath = "\"" + databaseFilePath + "\"";
				// See: http://msdn2.microsoft.com/en-us/library/a4sf02ac(VS.80).aspx
				System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(exePath, databaseFilePath);
				psi.RedirectStandardOutput = true;
				psi.RedirectStandardInput = true;
				psi.RedirectStandardError = true;
				psi.UseShellExecute = false;
				psi.CreateNoWindow = true;
				psi.WorkingDirectory = Path.GetDirectoryName(exePath);
				System.Diagnostics.Process process = System.Diagnostics.Process.Start(psi);
				process.StandardInput.WriteLine(command);
				process.StandardInput.Flush();
				process.StandardInput.Close();
				output = process.StandardOutput.ReadToEnd();
				process.WaitForExit();
				return true;
			}
			catch (Exception ex)
			{
				output = ex.Message;
				return false;
			}
		}

		public static SQLiteDatabaseConnector FromConnectionString(string connectionString)
		{
			return new SQLiteDatabaseConnector(connectionString);
		}

		internal void FillData()
		{
			string sql;
			RunSQLiteCommandLine(ConnectionInformation.FileName, ".schema", out sql);

			try
			{
				var ast = GetAST(sql);

				#region TABLES

				var tableScripts = ast.Children.Where(c => c.Text == "CREATE_TABLE").ToList();

				if (tableScripts.Count == 0 && ast.Text == "CREATE_TABLE")
					tableScripts.Add(ast);

				foreach (var t in tableScripts)
				{
					var tableName = ((BaseTree)t).Children[1].Text.Trim('[', ']', '"');

					// Get the columns
					var colDefs = ((BaseTree)((BaseTree)t).Children.Single(c => c.Text == "COLUMNS")).Children;

					for (int i = 0; i < colDefs.Count; i++)
					{
						var c = colDefs[i];
						var colName = c.Text.Trim('[', ']');
						var ordinalPos = i;
						var col = (BaseTree)c;
						var typeNode = (BaseTree)col.Children.Single(x => x.Text == "TYPE");
						var colType = typeNode.Children[1].Text;
						var typeParamNode = typeNode.Children[0];
						int charMaxLength = 0;
						string defaultValue = ""; // TODO

						if (((BaseTree)typeParamNode).Children != null)
							charMaxLength = int.Parse(((BaseTree)typeParamNode).Children[0].Text);

						var colConstraints = ((BaseTree)col.Children.Single(x => x.Text == "CONSTRAINTS")).Children;
						bool isPrimaryKey = false;
						bool isNullable = true;
						bool isIdentity = false;
						int numericPrecision = 0; // TODO
						int numericScale = 0; // TODO

						if (colConstraints != null)
						{
							foreach (var constraint in colConstraints)
							{
								if (((BaseTree)constraint).Children.Count(x => x.Text.ToUpper() == "PRIMARY") == 1)
									isPrimaryKey = true;
								else if (((BaseTree)constraint).Children.Count(x => x.Text == "NOT_NULL") == 1)
									isNullable = false;
								else if (((BaseTree)constraint).Children.Count(x => x.Text == "AUTOINCREMENT") == 1)
									isIdentity = true;
							}
						}
						AddColumn(isPrimaryKey, tableName, "", colName, ordinalPos, isNullable, colType, charMaxLength, defaultValue, isIdentity, false, numericPrecision, numericScale);

						if (isPrimaryKey)
							AddIndex(tableName, "", colName, string.Format("PK_{0}", tableName), "PRIMARY KEY", colType, true, true);
					}
					// Get the constraints
					if (((BaseTree)t).Children.Count(constraint => constraint.Text == "CONSTRAINTS") == 1)
					{
						var constraints = ((BaseTree)((BaseTree)t).Children.Single(constraint => constraint.Text == "CONSTRAINTS")).Children;

						for (int constraintCounter = 0; constraintCounter < constraints.Count; constraintCounter++)
						{
							var constraint = (BaseTree)(BaseTree)constraints[constraintCounter];
							var constraintType = constraint.Children[0].Text.Trim('[', ']');
							var constraintName = constraint.Children[1].Text.Trim('[', ']');

							if (constraintType == "PRIMARY") constraintType = "PRIMARY KEY";
							if (constraintType == "FOREIGN") constraintType = "FOREIGN KEY";

							if (constraintType != "CHECK")
							{
								// Get the columns
								var con = (BaseTree)constraint.Children[0];
								int counter = 0;

								foreach (var constraintColumn in ((BaseTree)con.Children.Single(constraintCol => constraintCol.Text == "COLUMNS")).Children)
								{
									var constraintColumnName = constraintColumn.Text.Trim('[', ']');
									bool isUnique = constraintType == "PRIMARY KEY" || constraintType == "UNIQUE";
									AddIndex(tableName, "", constraintColumnName, constraintName, constraintType, "", false, isUnique);

									if (constraintType == "FOREIGN KEY")
									{
										AddForeignKeyColumn(constraintName, constraintColumnName, counter, tableName);
										//AddForeignKeyConstraint()
									}
									counter++;
								}
							}
						}
					}
				}
				#endregion

				#region Indexes

				foreach (var t in ast.Children.Where(c => c.Text == "CREATE_INDEX"))
				{
					BaseTree node = (BaseTree)t;
					string indexName = node.Children[1].Text.Trim('[', ']');
					string tableName = node.Children[2].Text.Trim('[', ']');
					string tableSchema = "";
					bool isUnique = ((BaseTree)node.Children[0]).Children != null && ((BaseTree)node.Children[0]).Children.Count(x => x.Text == "UNIQUE") == 1;
					string constraintType = isUnique ? "UNIQUE" : "NONE";
					string dataType = "";
					bool isClustered = false;

					// Get the columns
					var colDefs = ((BaseTree)((BaseTree)t).Children.Single(c => c.Text == "COLUMNS")).Children;

					for (int i = 0; i < colDefs.Count; i++)
					{
						string columnName = colDefs[i].Text.Trim('[', ']');
						AddIndex(tableName, tableSchema, columnName, indexName, constraintType, dataType, isClustered, isUnique);
					}
				}
				#endregion

				#region Foreign Key constraints
				foreach (var t in ast.Children.Where(c => c.Text == "CREATE_TABLE"))
				{
					var tableName = ((BaseTree)t).Children[1].Text.Trim('[', ']');

					if (((BaseTree)t).Children.Count(constraint => constraint.Text == "CONSTRAINTS") == 1)
					{
						var constraints = ((BaseTree)((BaseTree)t).Children.Single(constraint => constraint.Text == "CONSTRAINTS")).Children;

						for (int constraintCounter = 0; constraintCounter < constraints.Count; constraintCounter++)
						{
							var constraint = (BaseTree)(BaseTree)constraints[constraintCounter];
							var constraintType = constraint.Children[0].Text.Trim('[', ']');
							var constraintName = constraint.Children[1].Text.Trim('[', ']');

							if (constraintType != "FOREIGN") continue;

							constraintType = "FOREIGN KEY";
							var con = (BaseTree)constraint.Children[0];

							var reference = (BaseTree)con.Children.SingleOrDefault(constraintCol => constraintCol.Text == "REFERENCES");

							if (reference != null)
							{
								string refTableName = reference.Children[0].Text.Trim('[', ']');
								// Get the name of the primary key on the primary table
								DataRow[] keyRows = Indexes.Select(string.Format("TABLE_NAME = '{0}' AND TABLE_SCHEMA = '{1}' AND CONSTRAINT_TYPE = 'PRIMARY KEY'", refTableName, ""));
								string primaryKeyName = keyRows[0]["INDEX_NAME"].ToString();
								AddForeignKeyConstraint(primaryKeyName, refTableName, "", constraintName, tableName, "");
							}
						}
					}
				}
				#endregion
			}
			catch (Exception e)
			{
				throw new Exception(e.Message + Environment.NewLine + "STACKTRACE:" + e.StackTrace + Environment.NewLine + "SQL: " + sql, e);
			}
		}

		private CommonTree GetAST(string sql)
		{
			ANTLRStringStream sStream = new ANTLRStringStream(sql);
			SQLiteLexer lexer = new SQLiteLexer(sStream);
			CommonTokenStream tStream = new CommonTokenStream(lexer);
			SQLiteParser parser = new SQLiteParser(tStream);
			SQLiteParser.sql_stmt_list_return rrr = parser.sql_stmt_list();
			CommonTree ast = (CommonTree)rrr.Tree;
			return ast;
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
			return foreignKeyColumns;
		}

		public void TestConnection()
		{
			if (string.IsNullOrEmpty(ConnectionInformation.FileName) ||
				!File.Exists(ConnectionInformation.FileName))
			{
				throw new DatabaseException("The database name cannot be empty.");
			}
			string result;

			if (!RunSQLiteCommandLine(ConnectionInformation.FileName, ".tables", out result))
				throw new DatabaseException("The database name cannot be empty.");

			if (string.IsNullOrWhiteSpace(result))
				throw new DatabaseException("Invalid database.");
		}

		public void Open()
		{
			//if (uniConnection.State != ConnectionState.Open)
			//    uniConnection.Open();

			//log.InfoFormat("Database {0} opened successfully", uniConnection.Database);

			//DatabaseName = uniConnection.Database;
		}

		public void Close()
		{
			//uniConnection.Close();
		}

		public DataTable RunQueryDataTable(string sql)
		{
			return null;
			//UniCommand sqlCommand = new UniCommand(sql, uniConnection);
			//DataTable dataTable = new DataTable();
			//DataSet ds = new DataSet();
			//UniDataAdapter sqlDataAdapter = new UniDataAdapter(sqlCommand);
			////sqlDataAdapter.Fill(dataTable, null); // Just pass the DataTable into the SqlDataAdapters Fill Method
			//sqlDataAdapter.Fill(ds);
			//return ds.Tables[0];
			////return dataTable;
		}

		public UniDataReader RunQuerySQL(string sql)
		{
			return null;
			//UniCommand sqlCommand = new UniCommand(sql, uniConnection);
			//return sqlCommand.ExecuteReader();
		}

		public void RunNonQuerySQL(string sql)
		{
			//UniCommand sqlCommand = new UniCommand(sql, uniConnection);
			//sqlCommand.ExecuteNonQuery();
		}

		public UniDataReader RunStoredProcedure(string storedProcedureName)
		{
			return null;
			//UniCommand sqlCommand = new UniCommand(storedProcedureName, uniConnection);
			////sqlCommand.CommandType = CommandType.StoredProcedure;
			//return sqlCommand.ExecuteReader();
		}

		public void RunStoredProcedureNonQuery(string storedProcedureName)
		{
			//UniCommand sqlCommand = new UniCommand(storedProcedureName, uniConnection);
			////sqlCommand.CommandType = CommandType.StoredProcedure;
			//sqlCommand.ExecuteNonQuery();
		}

		public DataTable Columns
		{
			get { return _columns; }
		}

		public DataTable Indexes
		{
			get { return _indexes; }
		}

		public DataTable IndexReferencedColumns
		{
			get { return _indexReferencedColumns; }
		}

		public List<string> GetTableNames()
		{
			string sql = @"SELECT name FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%' ORDER BY name;";
			string result;

			if (RunSQLiteCommandLine(ConnectionInformation.FileName, sql, out result))
			{
				result = result.TrimEnd().Replace("\r\n", "\n");
				return result.Split('\n').ToList();
			}
			return new List<string>();
		}

		public List<string> GetViewNames()
		{
			string sql = @"SELECT name FROM sqlite_master WHERE type = 'view' AND name NOT LIKE 'sqlite_%' ORDER BY name;";
			string result;

			if (RunSQLiteCommandLine(ConnectionInformation.FileName, sql, out result))
			{
				result = result.TrimEnd().Replace("\r\n", "\n");
				return result.Split('\n').ToList();
			}
			return new List<string>();
		}

		public DataTable GetForeignKeyConstraints()
		{
			return foreignKeyConstraints;
		}

		public void Dispose()
		{
			//if (uniConnection != null)
			//    uniConnection.Dispose();
		}

		public DataTable GetUniqueIndexes()
		{
			return uniqueIndexes;
		}
	}
}
