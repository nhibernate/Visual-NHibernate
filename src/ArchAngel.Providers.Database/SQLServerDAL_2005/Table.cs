using System;
using System.Data;
using System.Collections.Generic;
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_2005
{
    public class Table : SQLServerBase, ITable
    {
        private DataTable _columns;
        private DataTable _indexes;
        private DataTable _indexReferencedColumns;

        public Table(BLL.ConnectionStringHelper connectionString)
            : base(connectionString)
        {
        }

        private DataTable Columns
        {
            get
            {
                if (_columns == null)
                {
                	const string sql = @"
                        SELECT DISTINCT (
                            SELECT
                                CASE COUNT(T.CONSTRAINT_TYPE)
									WHEN 0 THEN 0
									ELSE 1
								END
                            FROM
                                INFORMATION_SCHEMA.TABLE_CONSTRAINTS T INNER JOIN
                                INFORMATION_SCHEMA.KEY_COLUMN_USAGE U ON C.Column_Name = U.Column_Name AND 
                                C.Table_Name = U.Table_Name
                            WHERE
                                T.Constraint_Name = U.Constraint_Name AND T.CONSTRAINT_TYPE = 'PRIMARY KEY'
                            ) AS InPrimaryKey, 
                            TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME, CAST(ORDINAL_POSITION AS INT) AS ORDINAL_POSITION, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, 
                            COLUMN_DEFAULT, COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA +'.'+ TABLE_NAME), COLUMN_NAME, 'IsIdentity') AS IsIdentity, 
                            COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA +'.'+ TABLE_NAME), COLUMN_NAME, 'IsComputed') AS IsComputed,
                            NUMERIC_PRECISION, NUMERIC_SCALE
                        FROM
                            INFORMATION_SCHEMA.COLUMNS C
                        ORDER BY 
                            TABLE_NAME, ORDINAL_POSITION";

                	_columns = RunQueryDataTable(sql);
                }

            	return _columns;
            }
        }

        private DataTable Indexes
        {
            get
            {
                if (_indexes == null)
                {
                    const string sql = @"
						SELECT  CU.TABLE_SCHEMA AS TABLE_SCHEMA,
								CU.TABLE_NAME AS TABLE_NAME, 
                                CU.COLUMN_NAME AS COLUMN_NAME, 
                                CU.CONSTRAINT_NAME AS CONSTRAINT_NAME,
								TC.CONSTRAINT_TYPE AS CONSTRAINT_TYPE,
								C.DATA_TYPE AS DATA_TYPE,
								CASE WHEN i.type_desc = 'CLUSTERED' THEN 1 ELSE 0 END AS IS_CLUSTERED,
								CASE WHEN i.is_unique = 1 THEN 1 ELSE 0 END as IS_UNIQUE
                        FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CU
							INNER JOIN INFORMATION_SCHEMA.COLUMNS C ON C.COLUMN_NAME = CU.COLUMN_NAME AND C.TABLE_NAME = CU.TABLE_NAME
							INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC ON TC.TABLE_NAME = CU.TABLE_NAME AND TC.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
							LEFT JOIN sys.indexes AS i ON (i.index_id > 0 and i.is_hypothetical = 0) AND i.name = CU.CONSTRAINT_NAME --(i.object_id = tbl.object_id)

                        UNION

                        SELECT
							s.name AS TABLE_SCHEMA,
                            tbl.Name AS TABLE_NAME,
							c.Name AS COLUMN_NAME,
                            i.name AS CONSTRAINT_NAME,
	                        'NONE' AS CONSTRAINT_TYPE,
							'' AS DATA_TYPE,
							CASE WHEN i.type_desc = 'CLUSTERED' THEN 1 ELSE 0 END AS IS_CLUSTERED,
							i.is_unique as IS_UNIQUE
                        FROM
                            sys.tables AS tbl
                                INNER JOIN sys.indexes AS i ON (i.index_id > 0 and i.is_hypothetical = 0) AND (i.object_id = tbl.object_id)
		                        LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU on KCU.TABLE_NAME = tbl.name AND KCU.CONSTRAINT_NAME = i.name
								INNER JOIN sys.index_columns ic on ic.object_id = tbl.object_id and ic.index_id = i.index_id
								INNER JOIN sys.columns c on c.object_id = ic.object_id and c.column_id = ic.column_id
								INNER JOIN sys.schemas s ON tbl.schema_id = s.schema_id
                        WHERE KCU.CONSTRAINT_NAME IS NULL
                        ORDER BY TABLE_NAME, CONSTRAINT_NAME ASC
                    ";

                    const string sql2 = @"
						SELECT	TC.TABLE_SCHEMA AS TABLE_SCHEMA,
								TC.TABLE_NAME AS TABLE_NAME,
    							CU.COLUMN_NAME AS COLUMN_NAME,
								TC.CONSTRAINT_NAME AS CONSTRAINT_NAME,
								TC.CONSTRAINT_TYPE AS CONSTRAINT_TYPE,
								C.DATA_TYPE AS DATA_TYPE,
								CASE INDEXPROPERTY( i.id , i.name , 'IsClustered')
                                     WHEN 1 THEN 1
                                     ELSE 0 END AS IS_CLUSTERED,
								CASE INDEXPROPERTY( i.id , i.name , 'IsUnique')
                                     WHEN 1 THEN 1
                                     ELSE 0 END AS IS_UNIQUE
                        FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
	                        INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CU ON TC.TABLE_NAME = CU.TABLE_NAME AND TC.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
	                        INNER JOIN INFORMATION_SCHEMA.COLUMNS C ON C.COLUMN_NAME = CU.COLUMN_NAME AND C.TABLE_NAME = CU.TABLE_NAME
	                        LEFT JOIN sysindexes AS i ON i.name = CU.CONSTRAINT_NAME --(i.object_id = tbl.object_id)

                        UNION

                        SELECT	'' AS TABLE_SCHEMA,
								o.name AS TABLE_NAME, 
								c.name AS COLUMN_NAME,
								i.name AS CONSTRAINT_NAME, 
								'NONE' AS CONSTRAINT_TYPE,
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
                        ORDER BY TABLE_NAME, CONSTRAINT_NAME ASC";

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

//        private DataTable Keys
//        {
//            get
//            {
//                if (_keys == null)
//                {
//                    //string sql = @"select * from INFORMATION_SCHEMA.KEY_COLUMN_USAGE order by TABLE_NAME, ORDINAL_POSITION";
//                    string sql = @"
//							SELECT  CU.TABLE_NAME, 
//                                    CU.COLUMN_NAME, 
//                                    CU.CONSTRAINT_NAME,
//									C.DATA_TYPE,
//									TC.CONSTRAINT_TYPE
//                            FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CU
//								INNER JOIN INFORMATION_SCHEMA.COLUMNS C ON C.COLUMN_NAME = CU.COLUMN_NAME AND C.TABLE_NAME = CU.TABLE_NAME
//								INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC ON TC.TABLE_NAME = CU.TABLE_NAME AND TC.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
//                            ORDER BY CU.TABLE_NAME, CU.CONSTRAINT_NAME";

//                    _keys = RunQueryDataTable(sql);
//                }

//                return _keys;
//            }
//        }

        private DataTable IndexReferencedColumns
        {
            get
            {
                if (_indexReferencedColumns == null)
                {
                	const string sql = @"
						SELECT 
							FK.CONSTRAINT_NAME AS ForeignKey,
							FK.CONSTRAINT_SCHEMA AS ForeignKeySchema,
							FKCU.TABLE_NAME AS ForeignKeyTable, 
							FKCU.COLUMN_NAME AS ForeignKeyColumn,
							FK.UNIQUE_CONSTRAINT_NAME AS ReferencedKey,
							PKCU.TABLE_SCHEMA AS ReferencedSchema, 
							PKCU.TABLE_NAME AS ReferencedTable, 
							PKCU.COLUMN_NAME AS ReferencedColumn
						FROM	INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS FK INNER JOIN
								INFORMATION_SCHEMA.KEY_COLUMN_USAGE PKCU ON 
									FK.UNIQUE_CONSTRAINT_NAME = PKCU.CONSTRAINT_NAME AND 
									FK.UNIQUE_CONSTRAINT_SCHEMA = PKCU.CONSTRAINT_SCHEMA INNER JOIN
								INFORMATION_SCHEMA.KEY_COLUMN_USAGE FKCU ON 
									FK.CONSTRAINT_NAME = FKCU.CONSTRAINT_NAME AND 
									FK.CONSTRAINT_SCHEMA = FKCU.CONSTRAINT_SCHEMA
						ORDER BY FKCU.TABLE_NAME, FK.CONSTRAINT_NAME, FKCU.ORDINAL_POSITION";

                	_indexReferencedColumns = RunQueryDataTable(sql);
                }

            	return _indexReferencedColumns;
            }
        }

        public Model.Table[] GetTables()
        {
            const string sql1 = @"
                SELECT 
                    TABLE_NAME, 
					TABLE_SCHEMA, 
					(SELECT CAST(
						CASE 
							WHEN tbl.is_ms_shipped = 1 THEN 1 
							WHEN
							   (
								SELECT
									major_id
								FROM
									sys.extended_properties
								WHERE
									major_id = tbl.object_id AND minor_id = 0 AND class = 1 AND name = N'microsoft_database_tools_support'
								) IS NOT NULL THEN 1 
							ELSE 0 
						END AS bit) AS IsSystemObject
                    FROM
                        sys.tables AS tbl
                    WHERE
                        name = T.TABLE_NAME AND SCHEMA_NAME(schema_id) = N'dbo') AS IsSystemObject
                FROM
                    INFORMATION_SCHEMA.TABLES AS T
                WHERE
                    TABLE_TYPE = 'BASE TABLE'
                ORDER BY
                    TABLE_NAME";

            // SQL query for databases that are missing various system tables
            const string sql2 = @"
                SELECT 
                    TABLE_NAME, 
					TABLE_SCHEMA, 
					CAST(
							CASE 
								WHEN (OBJECTPROPERTY(SO.id, N'IsMSShipped') = 1) THEN 1 
								WHEN 1 = OBJECTPROPERTY(SO.id, N'IsSystemTable') THEN 1 
								ELSE 0 
							END 
						AS bit) AS IsSystemObject
                FROM
                    INFORMATION_SCHEMA.TABLES AS T
			INNER JOIN sysobjects SO on T.TABLE_NAME = SO.name
                WHERE
                    TABLE_TYPE = 'BASE TABLE'
                ORDER BY
                    TABLE_NAME";

            List<string> tableNames = new List<string>();
            //OleDbDataReader oleDbDataReader = null;
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;

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

                    if (!isSystemObject || Model.Database.IncludeSystemObjects)
                    {
                        tableNames.Add(sqlDataReader["TABLE_NAME"].ToString() + "|" + sqlDataReader["TABLE_SCHEMA"].ToString());
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

            List<Model.Table> tables = new List<Model.Table>();
            foreach (string tableNameEx in tableNames)
            {
                string tableName = tableNameEx.Split('|')[0];
                string schema = tableNameEx.Split('|')[1];
                Model.Table table = GetNewTable(schema, tableName);
                table.Schema = schema;
                tables.Add(table);
            }

            return tables.ToArray();
        }

        private Model.Table GetNewTable(string schema, string tableName)
        {
            Interfaces.Events.RaiseObjectBeingProcessedEvent(tableName, "Table");
            Model.Table table = new Model.Table(tableName, false);

            #region Columns
            DataRow[] columnRows = Columns.Select(string.Format("TABLE_SCHEMA = '{0}' AND TABLE_NAME = '{1}'", schema, tableName));

            foreach (DataRow columnRow in columnRows)
            {
                bool isReadOnly = false;

                if (!columnRow.IsNull("IsIdentity") && (int)columnRow["IsIdentity"] == 1)
                {
                    isReadOnly = true;
                }
                else if (!columnRow.IsNull("IsComputed") && (int)columnRow["IsComputed"] == 1)
                {
                    isReadOnly = true;
                }
                else if (Slyce.Common.Utility.StringsAreEqual((string)columnRow["DATA_TYPE"], "timestamp", false))
                {
                    isReadOnly = true;
                }
                Column column = new Column(
                    (string)columnRow["COLUMN_NAME"],
                    false,
                    table,
                    (int)columnRow["ORDINAL_POSITION"],
                    Slyce.Common.Utility.StringsAreEqual((string)columnRow["IS_NULLABLE"], "YES", false),
                    (string)columnRow["DATA_TYPE"],
                    columnRow.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : Convert.ToInt32(columnRow["CHARACTER_MAXIMUM_LENGTH"]),
                    (int)columnRow["InPrimaryKey"] == 1,
                    columnRow.IsNull("IsIdentity") ? false : Convert.ToInt32(columnRow["IsIdentity"]) == 1,
                    columnRow.IsNull("COLUMN_DEFAULT") ? "" : (string)columnRow["COLUMN_DEFAULT"],
                    isReadOnly,
                    columnRow.IsNull("IsComputed") ? false : Convert.ToInt32(columnRow["IsComputed"]) == 1,
                    columnRow.IsNull("NUMERIC_PRECISION") ? 0 : Convert.ToInt32(columnRow["NUMERIC_PRECISION"]),
                    columnRow.IsNull("NUMERIC_SCALE") ? 0 : Convert.ToInt32(columnRow["NUMERIC_SCALE"]));

                if (IsSupported(column))
                {
                    table.AddColumn(column);
                }
            }

            #endregion

            #region Indexes
            DataRow[] indexRows = Indexes.Select(string.Format("TABLE_SCHEMA = '{0}' AND TABLE_NAME = '{1}'", schema, tableName));
            //foreach (DataRow indexRow in indexRows)
            for (int i = 0; i < indexRows.Length; i++)
            {
                DataRow indexRow = indexRows[i];
                string indexType;
                string indexKeyType = indexRow["CONSTRAINT_TYPE"].ToString();

                if (indexKeyType == "PRIMARY KEY")
                {
                    indexType = DatabaseConstant.IndexType.PrimaryKey;
                    //continue;
                }
                else if (indexKeyType == "FOREIGN KEY")
                {
                    indexType = DatabaseConstant.IndexType.ForeignKey;
                    //continue;
                }
                else if (indexKeyType == "UNIQUE")
                {
                    //continue;
                    indexType = DatabaseConstant.IndexType.Unique;
                }
                else if (indexKeyType == "CHECK")
                {
                    indexType = DatabaseConstant.IndexType.Check;
                }
                else if (indexKeyType == "NONE")    //TODO check is NONE
                {
                    indexType = DatabaseConstant.IndexType.None;
                }
                else
                {
                    //continue;
                    throw new Exception("IndexType " + indexKeyType + " Not Defined");
                }
                List<DataRow> indexColumnRows = new List<DataRow>();// = IndexColumns.Select(string.Format("TABLE_NAME = '{0}' AND CONSTRAINT_NAME  = '{1}'", tableName, indexRow["CONSTRAINT_NAME"]));

                indexColumnRows.AddRange(Columns.Select(string.Format("TABLE_SCHEMA = '{0}' AND TABLE_NAME = '{1}' AND COLUMN_NAME  = '{2}'", schema, tableName, indexRow["COLUMN_NAME"])));

                while ((i < indexRows.Length - 1) && (string)indexRows[i + 1]["TABLE_NAME"] == tableName && (string)indexRows[i + 1]["CONSTRAINT_NAME"] == (string)indexRow["CONSTRAINT_NAME"])
                {
                    i++;
                    indexRow = indexRows[i];
                    indexColumnRows.AddRange(Columns.Select(string.Format("TABLE_SCHEMA = '{0}' AND TABLE_NAME = '{1}' AND COLUMN_NAME  = '{2}'", schema, tableName, indexRow["COLUMN_NAME"])));
                }
                bool isUnique = (int)indexRow["IS_UNIQUE"] == 1 ? true : false;
                bool isClustered = (int)indexRow["IS_CLUSTERED"] == 1 ? true : false;
                Index index = new Index(indexRow["CONSTRAINT_NAME"].ToString(), false, indexType, table, isUnique, isClustered);

                // Fill Columns
                foreach (DataRow indexColumnRow in indexColumnRows)
                {
                    Column indexColumn = new Column(indexColumnRow["COLUMN_NAME"].ToString(), false);
                    index.AddColumn(indexColumn);
                }
                index.ResetDefaults();
                table.AddIndex(index);
            }

            // Indexes -- that should be keys
            DataRow[] keyRows = Indexes.Select(string.Format("TABLE_SCHEMA = '{0}' AND TABLE_NAME = '{1}'", schema, tableName));

            //foreach (DataRow keyRow in indexRows)
            for (int i = 0; i < keyRows.Length; i++)
            {
                DataRow keyRow = keyRows[i];
                string keyType;
                string indexKeyType = keyRow["CONSTRAINT_TYPE"].ToString();

                if (indexKeyType == "PRIMARY KEY")
                {
                    keyType = DatabaseConstant.KeyType.Primary;
                }
                else if (indexKeyType == "FOREIGN KEY")
                {
                    keyType = DatabaseConstant.KeyType.Foreign;
                }
                else if (indexKeyType == "UNIQUE")
                {
                    keyType = DatabaseConstant.KeyType.Unique;
                }
                else
                {
                    continue;
                }
                Key key = new Key(keyRow["CONSTRAINT_NAME"].ToString(), false, keyType, table, false);
                
                // Fill Columns
                List<DataRow> keyColumnRows = new List<DataRow>();
                keyColumnRows.AddRange(Columns.Select(string.Format("TABLE_SCHEMA = '{0}' AND TABLE_NAME = '{1}' AND COLUMN_NAME = '{2}'", schema, tableName, keyRow["COLUMN_NAME"])));

                while ((i < keyRows.Length - 1) && (string)keyRows[i + 1]["TABLE_NAME"] == tableName && (string)keyRows[i + 1]["CONSTRAINT_NAME"] == (string)keyRow["CONSTRAINT_NAME"])
                {
                    i++;
                    keyRow = keyRows[i];
                    keyColumnRows.AddRange(Columns.Select(string.Format("TABLE_SCHEMA = '{0}' AND TABLE_NAME = '{1}' AND COLUMN_NAME = '{2}'", schema, tableName, keyRow["COLUMN_NAME"])));
                }
                // Fill Columns
                foreach (DataRow keyColumnRow in keyColumnRows)
                {
                    Column keyColumn = new Column(keyColumnRow["COLUMN_NAME"].ToString(), false);
                    keyColumn.DataType = (string)keyColumnRow["DATA_TYPE"];
                    key.AddColumn(keyColumn);
                }
                if (keyType == DatabaseConstant.KeyType.Foreign)
                {
                    DataRow[] keyReferencedColumnRows = IndexReferencedColumns.Select(string.Format("ForeignKeySchema = '{0}' AND ForeignKeyTable = '{1}' AND ForeignKey = '{2}'", schema, tableName, keyRow["CONSTRAINT_NAME"]));
                    DataRow firstKeyReferencedColumnRow = keyReferencedColumnRows[0];

                    // Fill References
					key.ReferencedTable = new Model.Table(firstKeyReferencedColumnRow["ReferencedTable"].ToString(), false) { Schema = firstKeyReferencedColumnRow["ReferencedSchema"].ToString() };
                    key.ReferencedKey = new Key(firstKeyReferencedColumnRow["ReferencedKey"].ToString(), false, true);

                    // Fill Referenced Columns
                    foreach (DataRow keyReferencedColumnRow in keyReferencedColumnRows)
                    {
                        Column keyReferencedColumn = new Column(keyReferencedColumnRow["ReferencedColumn"].ToString(), false);
                        key.AddReferencedColumn(keyReferencedColumn);
                    }
                }
                key.ResetDefaults();
                table.AddKey(key);
            }

            #endregion

            return table;
        }
    }
}