using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using log4net;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public class SQLServer2005DatabaseLoader : IDatabaseLoader
	{
		private readonly static ILog log = LogManager.GetLogger(typeof(SQLServer2005DatabaseLoader));
		private readonly string[] _unsupportedDataTypes = new[] { "" };
		private ISQLServer2005DatabaseConnector connector;

		public event PropertyChangedEventHandler PropertyChanged;

		public SQLServer2005DatabaseLoader(ISQLServer2005DatabaseConnector connector)
		{
			this.connector = connector;
		}

		public List<SchemaData> DatabaseObjectsToFetch { get; set; }

		public void TestConnection()
		{
			try
			{
				connector.TestConnection();
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Could not open database connection. See inner exception for details.", e);
			}
		}

		public void GetSchemaObjects(out List<string> schemaNames, out List<string> tableNames, out List<string> viewNames)
		{
			try
			{
				connector.Open();
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Error opening database connection. See inner exception.", e);
			}
			tableNames = new List<string>();
			schemaNames = new List<string>();
			viewNames = new List<string>();
			List<string> fullTableNames = connector.GetTableNames();
			List<string> fullViewNames = connector.GetViewNames();
			HashSet<string> uniqueSchemaNames = new HashSet<string>();

			foreach (string fullName in fullTableNames)
			{
				string[] parts = fullName.Split('|');
				tableNames.Add(parts[0]);
				uniqueSchemaNames.Add(parts[1]);
			}
			foreach (string fullName in fullViewNames)
			{
				string[] parts = fullName.Split('|');
				viewNames.Add(parts[0]);
				uniqueSchemaNames.Add(parts[1]);
			}
			schemaNames = uniqueSchemaNames.ToList();
		}

		public Database LoadDatabase(List<SchemaData> databaseObjectsToFetch, List<string> allowedSchemas)
		{
			try
			{
				connector.Open();
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Error opening database connection. See inner exception.", e);
			}
			try
			{
				var db = new Database(connector.DatabaseName, DatabaseTypes.SQLServer2005) { Loader = this };
				db.ConnectionInformation = DatabaseConnector.ConnectionInformation;
				SetSchemaFilter(databaseObjectsToFetch);

				foreach (var table in GetTables(databaseObjectsToFetch))
					db.AddTable(table);

				foreach (var view in GetViews(databaseObjectsToFetch))
					db.AddView(view);

				GetForeignKeys(db);
				connector.Close();
				return db;
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Error opening database connection. See inner exception.", e);
			}
		}

		private void SetSchemaFilter(List<SchemaData> databaseObjectsToFetch)
		{
			connector.SchemaFilterCSV = "";

			if (databaseObjectsToFetch != null)
				foreach (SchemaData data in databaseObjectsToFetch)
					connector.SchemaFilterCSV += string.Format("'{0}',", data.Name);

			if (!string.IsNullOrEmpty(connector.SchemaFilterCSV))
				connector.SchemaFilterCSV = connector.SchemaFilterCSV.TrimEnd(',');
		}

		public IDatabaseConnector DatabaseConnector
		{
			get { return connector; }
			set
			{
				if (value is SQLServer2005DatabaseConnector == false)
					throw new ArgumentException("Cannot use a " + value.GetType() +
												" as a connector for a SQLServer2005DatabaseLoader");
				connector = (SQLServer2005DatabaseConnector)value;
			}
		}

		private void GetForeignKeys(IDatabase db)
		{
			DataTable keysTable = connector.GetForeignKeyConstraints();

			ITable foreignTable;

			for (int i = 0; i < keysTable.Rows.Count; i++)
			{
				var keyRow = keysTable.Rows[i];
				string primaryTableSchema = (string)keyRow["PrimaryTableSchema"];
				ITable parentTable = db.GetTable((string)keyRow["PrimaryTable"], primaryTableSchema);
				string foreignTableSchema = (string)keyRow["ForeignTableSchema"];
				foreignTable = db.GetTable((string)keyRow["ForeignTable"], foreignTableSchema);
				var currentKeyName = keyRow["ForeignKeyName"].ToString();

				// Don't process keys of tables that the user hasn't selected
				if (parentTable == null || foreignTable == null)
					continue;

				Key key = new Key
							  {
								  Name = currentKeyName,
								  IsUserDefined = false,
								  Keytype = DatabaseKeyType.Foreign,
								  Parent = parentTable
							  };
				//////////////////
				key.IsUnique = connector.GetUniqueIndexes().Select(string.Format("INDEX_NAME = '{0}' AND TABLE_SCHEMA = '{1}'", currentKeyName, primaryTableSchema)).Length > 0;
				//////////////////
				foreignTable.AddKey(key);

				// Get Foreign Key columns
				DataRow[] foreignKeyColumns = connector.GetForeignKeyColumns().Select(string.Format("CONSTRAINT_NAME = '{0}' AND TABLE_SCHEMA = '{1}' AND TABLE_NAME = '{2}'", key.Name.Replace("'", "''"), foreignTableSchema, foreignTable.Name));

				foreach (DataRow row in foreignKeyColumns)
					key.AddColumn((string)row["COLUMN_NAME"]);

				key.ReferencedKey = parentTable.GetKey((string)keyRow["PrimaryKeyName"]);
				bool isUnique = false;

				if (foreignTable.FirstPrimaryKey != null)
				{
					isUnique = true;

					foreach (var col in foreignTable.FirstPrimaryKey.Columns)
					{
						if (!key.Columns.Contains(col))
						{
							isUnique = false;
							break;
						}
					}
				}
				key.IsUnique = isUnique;
			}
		}

		public List<SchemaData> GetSchemaObjects()
		{
			try
			{
				connector.Open();
			}
			catch (Exception e)
			{
				throw new DatabaseLoaderException("Error opening database connection. See inner exception.", e);
			}
			try
			{
				HashSet<string> schemaNames = new HashSet<string>();
				List<string> fullTableNames = connector.GetTableNames();
				List<string> fullViewNames = connector.GetViewNames();

				foreach (string fullName in fullTableNames)
					schemaNames.Add(fullName.Split('|')[1]);

				foreach (string fullName in fullViewNames)
					schemaNames.Add(fullName.Split('|')[1]);

				List<SchemaData> schemaDatas = new List<SchemaData>();

				foreach (var schema in schemaNames)
				{
					List<string> tableNames = new List<string>();
					List<string> viewNames = new List<string>();

					foreach (string tableName in fullTableNames.Where(f => f.EndsWith("|" + schema)))
						tableNames.Add(tableName.Split('|')[0]);

					foreach (string viewName in fullViewNames.Where(f => f.EndsWith("|" + schema)))
						viewNames.Add(viewName.Split('|')[0]);

					schemaDatas.Add(new SchemaData(schema, tableNames, viewNames));
				}
				return schemaDatas;
			}
			finally
			{
				connector.Close();
			}
		}

		public List<Table> GetTables(List<SchemaData> tablesToFetch)
		{
			log.DebugFormat("GetTables() called");
			List<string> tableNames = connector.GetTableNames();

			List<Table> tables = new List<Table>();
			foreach (string tableNameEx in tableNames)
			{
				string[] parts = tableNameEx.Split('|');
				string tableName = parts[0];
				string schema = parts[1];

				if (tablesToFetch == null ||
					tablesToFetch.Exists(t => (string.IsNullOrEmpty(t.Name) || t.Name.ToLowerInvariant() == schema.ToLowerInvariant()) && t.TableNames.Contains(tableName)))
				{
					Table table = GetNewTable(tableName, schema);
					tables.Add(table);
				}
			}
			return tables;
		}

		private Table GetNewTable(string tableName, string schema)
		{
			Interfaces.Events.RaiseObjectBeingProcessedEvent(tableName, "Table");
			Table table = new Table { Name = tableName, Schema = schema, IsUserDefined = false, IsView = false };

			GetColumns(table);
			GetIndexes(table);
			GetKeys(table);

			table.ResetDefaults();

			return table;
		}

		public List<Table> GetViews(List<SchemaData> viewsToFetch)
		{
			log.DebugFormat("GetViews() called");
			connector.SchemaFilterCSV = "";

			if (viewsToFetch != null)
			{
				foreach (SchemaData data in viewsToFetch)
					connector.SchemaFilterCSV += data.Name + ",";

				if (!string.IsNullOrEmpty(connector.SchemaFilterCSV))
					connector.SchemaFilterCSV = connector.SchemaFilterCSV.TrimEnd(',');
			}
			List<string> viewNames = connector.GetViewNames();

			List<Table> views = new List<Table>();
			foreach (string viewNameEx in viewNames)
			{
				string[] parts = viewNameEx.Split('|');
				string viewName = parts[0];
				string schema = parts[1];

				if (viewsToFetch == null ||
					viewsToFetch.Exists(t => (string.IsNullOrEmpty(t.Name) || t.Name.ToLowerInvariant() == schema.ToLowerInvariant()) && t.ViewNames.Contains(viewName)))
				{
					Table view = GetNewView(viewName, schema);
					views.Add(view);
				}
			}
			return views;
		}

		private Table GetNewView(string viewName, string schema)
		{
			Interfaces.Events.RaiseObjectBeingProcessedEvent(viewName, "View");
			Table table = new Table { Name = viewName, Schema = schema, IsUserDefined = false, IsView = true };

			GetColumns(table);
			//GetIndexes(table);
			//GetKeys(table);

			table.ResetDefaults();
			return table;
		}

		private void GetKeys(ITable table)
		{
			DataRow[] keyRows = connector.Indexes.Select(string.Format("TABLE_NAME = '{0}' AND TABLE_SCHEMA = '{1}'", table.Name.Replace("'", "''"), table.Schema));
			bool hasPrimaryKey = false;

			for (int i = 0; i < keyRows.Length; i++)
			{
				DataRow keyRow = keyRows[i];
				DatabaseKeyType keyType;
				string indexKeyType = keyRow["CONSTRAINT_TYPE"].ToString();

				if (indexKeyType == "PRIMARY KEY")
				{
					keyType = DatabaseKeyType.Primary;
					hasPrimaryKey = true;
				}
				else if (indexKeyType == "UNIQUE")
					keyType = DatabaseKeyType.Unique;
				else
					continue;

				Key key = new Key
				{
					Name = keyRow["INDEX_NAME"].ToString(),
					IsUserDefined = false,
					Keytype = keyType,
					Parent = table,
					IsUnique = true
				};

				// Fill Columns
				List<DataRow> keyColumnRows = new List<DataRow>();
				keyColumnRows.AddRange(connector.Columns.Select(string.Format("TABLE_NAME = '{0}' AND TABLE_SCHEMA = '{1}' AND COLUMN_NAME  = '{2}'", table.Name.Replace("'", "''"), table.Schema, keyRow["COLUMN_NAME"])));

				while ((i < keyRows.Length - 1) && (string)keyRows[i + 1]["TABLE_NAME"] == table.Name && (string)keyRows[i + 1]["INDEX_NAME"] == (string)keyRow["INDEX_NAME"])
				{
					i++;
					keyRow = keyRows[i];
					keyColumnRows.AddRange(connector.Columns.Select(string.Format("TABLE_NAME = '{0}' AND TABLE_SCHEMA = '{1}' AND COLUMN_NAME  = '{2}'", table.Name.Replace("'", "''"), table.Schema, keyRow["COLUMN_NAME"])));
				}
				// Fill Columns
				foreach (DataRow keyColumnRow in keyColumnRows.OrderBy(r => r["ORDINAL_POSITION"]))
					key.AddColumn((string)keyColumnRow["COLUMN_NAME"]);

				key.ResetDefaults();
				table.AddKey(key);
			}
			if (!hasPrimaryKey)
			{
				Key key = new Key
				{
					Name = "PK_" + table.Name,
					IsUserDefined = true,
					Keytype = DatabaseKeyType.Primary,
					Parent = table,
					IsUnique = true,
					Description = "Added by Visual NHibernate"
				};
				key.ResetDefaults();
				table.AddKey(key);
			}
		}

		private void GetIndexes(Table table)
		{
			GetTableConstraintIndexes(table);
			//GetUniqueIndexes(table);
		}

		internal void GetTableConstraintIndexes(Table table)
		{
			DataRow[] indexRows = connector.Indexes.Select(string.Format("TABLE_NAME = '{0}' AND TABLE_SCHEMA = '{1}'", table.Name.Replace("'", "''"), table.Schema));

			for (int i = 0; i < indexRows.Length; i++)
			{
				DataRow indexRow = indexRows[i];
				DatabaseIndexType indexType = GetIndexType(indexRow["CONSTRAINT_TYPE"].ToString());

				var indexColumnRows = new List<DataRow>();

				indexColumnRows.AddRange(connector.Columns.Select(string.Format("TABLE_NAME = '{0}' AND TABLE_SCHEMA = '{1}' AND COLUMN_NAME  = '{2}'", table.Name.Replace("'", "''"), table.Schema, indexRow["COLUMN_NAME"])));

				while ((i < indexRows.Length - 1) && (string)indexRows[i + 1]["TABLE_NAME"] == table.Name && (string)indexRows[i + 1]["INDEX_NAME"] == (string)indexRow["INDEX_NAME"])
				{
					i++;
					indexRow = indexRows[i];
					indexColumnRows.AddRange(connector.Columns.Select(string.Format("TABLE_NAME = '{0}' AND TABLE_SCHEMA = '{1}' AND COLUMN_NAME  = '{2}'", table.Name.Replace("'", "''"), table.Schema, indexRow["COLUMN_NAME"])));
				}
				bool isUnique = (bool)indexRow["IS_UNIQUE"];
				bool isClustered = (bool)indexRow["IS_CLUSTERED"];
				Index index = new Index
				{
					Name = indexRow["INDEX_NAME"].ToString(),
					IsUserDefined = false,
					IndexType = indexType,
					Parent = table,
					IsUnique = isUnique,
					IsClustered = isClustered
				};

				// Fill Columns
				foreach (DataRow indexColumnRow in indexColumnRows)
				{
					var column = index.AddColumn((string)indexColumnRow["COLUMN_NAME"]);

					if (index.IsUnique && indexColumnRows.Count == 1)
					{
						// Columns are only unique if the index has one column
						column.IsUnique = true;
					}
				}
				index.ResetDefaults();
				table.AddIndex(index);
			}
		}

		private void GetUniqueIndexes(Table table)
		{
			DataRow[] indexRows = connector.GetUniqueIndexes().Select(string.Format("TABLE_NAME = '{0}' AND TABLE_SCHEMA = '{1}'", table.Name.Replace("'", "''"), table.Schema));

			for (int i = 0; i < indexRows.Length; i++)
			{
				DataRow indexRow = indexRows[i];

				Index index = new Index
				{
					Name = (string)indexRow["INDEX_NAME"],
					IsUserDefined = false,
					IsUnique = true,
					IsClustered = (bool)indexRow["IS_CLUSTERED"],
					IndexType = GetIndexType(indexRow["CONSTRAINT_TYPE"].ToString()),
					Parent = table
				};

				List<DataRow> indexColumnRows = new List<DataRow>();

				indexColumnRows.AddRange(connector.Columns.Select(string.Format("TABLE_NAME = '{0}' AND TABLE_SCHEMA = '{1}' AND COLUMN_NAME  = '{2}'", table.Name.Replace("'", "''"), table.Schema, indexRow["COLUMN_NAME"])));

				while ((i < indexRows.Length - 1) && (string)indexRows[i + 1]["TABLE_NAME"] == table.Name && (string)indexRows[i + 1]["INDEX_NAME"] == (string)indexRow["INDEX_NAME"])
				{
					i++;
					indexRow = indexRows[i];
					indexColumnRows.AddRange(connector.Columns.Select(string.Format("TABLE_NAME = '{0}' AND TABLE_SCHEMA = '{1}' AND COLUMN_NAME  = '{2}'", table.Name.Replace("'", "''"), table.Schema, indexRow["COLUMN_NAME"])));
				}

				// Fill Columns
				foreach (DataRow indexColumnRow in indexColumnRows)
				{
					var column = index.AddColumn((string)indexColumnRow["COLUMN_NAME"]);

					if (index.IsUnique && indexColumnRows.Count == 1)
					{
						// Columns are only unique if the index has one column
						column.IsUnique = true;
					}
				}

				index.ResetDefaults();
				table.AddIndex(index);
			}
		}

		private void GetColumns(ITable table)
		{
			Type uniDBType = typeof(SQLServer.UniDbTypes);
			DataRow[] columnRows = connector.Columns.Select(string.Format("TABLE_NAME = '{0}' AND TABLE_SCHEMA = '{1}'", table.Name.Replace("'", "''"), table.Schema));

			foreach (DataRow columnRow in columnRows)
			{
				bool isReadOnly = false;

				if (Slyce.Common.Utility.StringsAreEqual((string)columnRow["DATA_TYPE"], "timestamp", false))
				{
					isReadOnly = true;
				}
				Column column = new Column();
				column.Name = (string)columnRow["COLUMN_NAME"];
				column.IsUserDefined = false;
				column.Parent = table;
				column.IsIdentity = columnRow["IsIdentity"] is DBNull ? false : ((int)columnRow["IsIdentity"]) == 1;
				column.OrdinalPosition = (int)columnRow["ORDINAL_POSITION"];
				column.IsNullable = Slyce.Common.Utility.StringsAreEqual((string)columnRow["IS_NULLABLE"], "YES", false);
				column.OriginalDataType = (string)columnRow["DATA_TYPE"];
				column.Size = columnRow.IsNull("CHARACTER_MAXIMUM_LENGTH") || column.OriginalDataType.ToLowerInvariant() == "text" // TODO: ntext/text returns useless length
								? 0
								: Convert.ToInt64(columnRow["CHARACTER_MAXIMUM_LENGTH"]);
				column.InPrimaryKey = connector.DoesColumnHaveConstraint(column, "PRIMARY KEY");
				column.IsUnique = connector.DoesColumnHaveConstraint(column, "UNIQUE");
				column.Default = columnRow.IsNull("COLUMN_DEFAULT") ? "" : (string)columnRow["COLUMN_DEFAULT"];
				column.IsReadOnly = isReadOnly;
				column.Precision = columnRow.IsNull("NUMERIC_PRECISION") ? 0 : Convert.ToInt32(columnRow["NUMERIC_PRECISION"]);
				column.Scale = columnRow.IsNull("NUMERIC_SCALE") ? 0 : Convert.ToInt32(columnRow["NUMERIC_SCALE"]);

				column.ResetDefaults();

				table.AddColumn(column);
			}
		}

		private DatabaseIndexType GetIndexType(string indexKeyType)
		{
			DatabaseIndexType indexType;
			switch (indexKeyType)
			{
				case "PRIMARY KEY":
					indexType = DatabaseIndexType.PrimaryKey;
					break;
				case "UNIQUE":
					indexType = DatabaseIndexType.Unique;
					break;
				case "CHECK":
					indexType = DatabaseIndexType.Check;
					break;
				case "NONE":
					indexType = DatabaseIndexType.None;
					break;
				case "FOREIGN KEY":
					indexType = DatabaseIndexType.ForeignKey;
					break;
				default:
					throw new Exception("IndexType " + indexKeyType + " Not Defined");
			}
			return indexType;
		}
	}
}