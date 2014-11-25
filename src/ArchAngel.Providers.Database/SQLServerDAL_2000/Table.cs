using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_2000
{
    public class Table : SQLServerBase, ITable
    {
        private static System.Data.DataTable _columns = null;

        private System.Data.DataTable _indexes = null;
        private System.Data.DataTable _indexColumns = null;
        private System.Data.DataTable _indexReferencedColumns = null;

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
                    string sql = @"
                        SELECT DISTINCT (
                            SELECT
                                COUNT(T.CONSTRAINT_TYPE)
                            FROM
                                INFORMATION_SCHEMA.TABLE_CONSTRAINTS T INNER JOIN
                                INFORMATION_SCHEMA.KEY_COLUMN_USAGE U ON C.Column_Name = U.Column_Name AND 
                                C.Table_Name = U.Table_Name
                            WHERE
                                T.Constraint_Name = U.Constraint_Name AND T.CONSTRAINT_TYPE = 'PRIMARY KEY'
                            ) AS InPrimaryKey, 
                            TABLE_NAME, COLUMN_NAME, CAST(ORDINAL_POSITION AS INT) AS ORDINAL_POSITION, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, 
                            COLUMN_DEFAULT, COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA +'.'+ TABLE_NAME), COLUMN_NAME, 'IsIdentity') AS IsIdentity, 
                            COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA +'.'+ TABLE_NAME), COLUMN_NAME, 'IsComputed') AS IsComputed
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
                    //string sql = @"select * from INFORMATION_SCHEMA.TABLE_CONSTRAINTS ORDER BY constraint_name";
                    string sql = @"
			                SELECT TC.TABLE_NAME AS TABLE_NAME,
                                TC.CONSTRAINT_NAME AS CONSTRAINT_NAME,
                                TC.CONSTRAINT_TYPE AS CONSTRAINT_TYPE,
	                        CU.COLUMN_NAME AS COLUMN_NAME
                            FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
	                        INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CU ON TC.TABLE_NAME = CU.TABLE_NAME AND TC.CONSTRAINT_NAME = CU.CONSTRAINT_NAME

                            UNION

                            SELECT o.name AS TABLE_NAME, 
                                    i.name AS CONSTRAINT_NAME, 
                                    'NONE' AS CONSTRAINT_TYPE,
                                    c.name AS COLUMN_NAME
                            FROM sysindexes i
                                    INNER JOIN sysindexkeys ik ON i.id = ik.id AND i.indid = ik.indid
                                    INNER JOIN syscolumns c ON i.id = c.id AND ik.colid = c.colid
                                    INNER JOIN sysobjects o ON o.id = i.id
                                    LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU ON KCU.TABLE_NAME = o.name AND KCU.CONSTRAINT_NAME = i.name
                            WHERE KCU.CONSTRAINT_NAME IS NULL AND
                                    o.xtype = 'U'
                            ORDER BY TABLE_NAME, CONSTRAINT_NAME ASC
                                ";

                    _indexes = RunQueryDataTable(sql);
                }

                return _indexes;
            }
        }

        private DataTable IndexColumns
        {
            get
            {
                if (_indexColumns == null)
                {
                    //string sql = @"select * from INFORMATION_SCHEMA.KEY_COLUMN_USAGE order by TABLE_NAME, ORDINAL_POSITION";
                    string sql = @"
                            SELECT  CU.TABLE_NAME, 
                                    CU.COLUMN_NAME, 
                                    CU.CONSTRAINT_NAME,
									C.DATA_TYPE
                            FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CU
								INNER JOIN INFORMATION_SCHEMA.COLUMNS C ON C.COLUMN_NAME = CU.COLUMN_NAME AND C.TABLE_NAME = CU.TABLE_NAME
                            ORDER BY CU.TABLE_NAME, CU.CONSTRAINT_NAME";

                    _indexColumns = RunQueryDataTable(sql);
                }

                return _indexColumns;
            }
        }

        private DataTable IndexReferencedColumns
        {
            get
            {
                if (_indexReferencedColumns == null)
                {
                    string sql = @"
                        SELECT
                            T.TABLE_NAME AS ReferencedTable, K.COLUMN_NAME AS ReferencedColumn, K.CONSTRAINT_NAME AS ReferencedKey, R.CONSTRAINT_NAME AS ForeignKey
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

        public Model.Table[] GetTables()
        {
            string sql1 = @"
                SELECT 
                    TABLE_NAME, TABLE_SCHEMA, (SELECT CAST(CASE WHEN tbl.is_ms_shipped = 1 THEN 1 WHEN
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
                        name = T.TABLE_NAME) AS IsSystemObject
                FROM
                    INFORMATION_SCHEMA.TABLES AS T
                WHERE
                    TABLE_TYPE = 'BASE TABLE'
                ORDER BY
                    TABLE_NAME";

            // SQL query for databases that are missing various system tables
            string sql2 = @"
                SELECT 
                    TABLE_NAME, TABLE_SCHEMA, CAST(CASE WHEN (OBJECTPROPERTY(SO.id, N'IsMSShipped') = 1) THEN 1 WHEN 1 = OBJECTPROPERTY(SO.id, N'IsSystemTable') THEN 1 ELSE 0 END AS bit) AS IsSystemObject
                FROM
                    INFORMATION_SCHEMA.TABLES AS T
			INNER JOIN sysobjects SO on T.TABLE_NAME = SO.name
                WHERE
                    TABLE_TYPE = 'BASE TABLE'
                ORDER BY
                    TABLE_NAME";

            List<string> tableNames = new List<string>();
            OleDbDataReader oleDbDataReader = null;

            try
            {
                try
                {
                    oleDbDataReader = RunQuery(sql1);
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("SCHEMA_NAME") > 0)
                    {
                        oleDbDataReader = RunQuery(sql2);
                    }
                    else
                    {
                        throw;
                    }
                }

                // Exclude system tables
                int isSysObjectColumnOrdinal = oleDbDataReader.GetOrdinal("IsSystemObject");
                while (oleDbDataReader.Read())
                {
                    bool isSystemObject = oleDbDataReader.IsDBNull(isSysObjectColumnOrdinal) ? false : (bool)oleDbDataReader[isSysObjectColumnOrdinal];
                    if (!isSystemObject)
                    {
                        tableNames.Add(oleDbDataReader["TABLE_NAME"].ToString() + "|" + oleDbDataReader["TABLE_NAME"].ToString());
                    }
                }
            }
            finally
            {
                if (oleDbDataReader != null && !oleDbDataReader.IsClosed)
                {
                    oleDbDataReader.Close();
                }
            }

            List<Model.Table> tables = new List<Model.Table>();
            foreach(string tableNameEx in tableNames)
            {
                string tableName = tableNameEx.Split('|')[0];
                string schema = tableNameEx.Split('|')[1];
                Model.Table table = GetNewTable(tableName);
                table.Schema = schema;
                tables.Add(table);
            }

            return (Model.Table[])tables.ToArray();
        }

        private Model.Table GetNewTable(string tableName)
        {
            ArchAngel.Interfaces.ProjectHelper.RaiseObjectBeingProcessedEvent(tableName, "Table");
            //_columns = null; // Reset the columns
            //_indexColumns = null;
            //_indexes = null;
            //_indexReferencedColumns = null;
            //_referencedColumns = null;
            Model.Table table = new Model.Table(tableName, false);

            #region Columns

            DataRow[] columnRows = Columns.Select(string.Format("TABLE_NAME = '{0}'", tableName));

            foreach (DataRow columnRow in columnRows)
            {
                Column column = new Column(
                    (string)columnRow["COLUMN_NAME"],
                    false,
                    table,
                    (int)columnRow["ORDINAL_POSITION"],
                    Slyce.Common.Utility.StringsAreEqual((string)columnRow["IS_NULLABLE"], "YES", false),
                    (string)columnRow["DATA_TYPE"],
                    columnRow.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : (int)columnRow["CHARACTER_MAXIMUM_LENGTH"],
                    (int)columnRow["InPrimaryKey"] == 1,
                    columnRow.IsNull("IsIdentity") ? false : (int)columnRow["IsIdentity"] == 1,
                    columnRow.IsNull("COLUMN_DEFAULT") ? "" : (string)columnRow["COLUMN_DEFAULT"],
                    columnRow.IsNull("IsComputed") ? false : (int)columnRow["IsComputed"] == 1);

                if (IsSupported(column))
                {
                    table.AddColumn(column);
                }
            }

            #endregion

            #region Indexes

            DataRow[] indexRows = Indexes.Select(string.Format("TABLE_NAME = '{0}'", tableName));
            foreach (DataRow indexRow in indexRows)
            {
                string indexType;
                string indexKeyType = indexRow["CONSTRAINT_TYPE"].ToString();
                if (indexKeyType == "PRIMARY KEY")
                {
                    continue;
                }
                else if (indexKeyType == "FOREIGN KEY")
                {
                    continue;
                }
                else if (indexKeyType == "UNIQUE")
                {
                    continue;
                    //indexType = DatabaseConstant.IndexType.Unique;
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
                DataRow[] indexColumnRows;// = IndexColumns.Select(string.Format("TABLE_NAME = '{0}' AND CONSTRAINT_NAME  = '{1}'", tableName, indexRow["CONSTRAINT_NAME"]));

                if (indexKeyType == "NONE")
                {
                    indexColumnRows = Columns.Select(string.Format("TABLE_NAME = '{0}' AND COLUMN_NAME  = '{1}'", tableName, indexRow["ColumnName"]));
                }
                else
                {
                    indexColumnRows = IndexColumns.Select(string.Format("TABLE_NAME = '{0}' AND CONSTRAINT_NAME  = '{1}'", tableName, indexRow["CONSTRAINT_NAME"]));
                }
                Index index = new Index(indexRow["CONSTRAINT_NAME"].ToString(), false, indexType, table);

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
            foreach (DataRow keyRow in indexRows)
            {
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
                else if (indexKeyType == "CHECK")
                {
                    continue;
                }
                else if (indexKeyType == "NONE")
                {
                    continue;
                    //keyType = DatabaseConstant.KeyType.None;
                }
                else
                {
                    //continue;
                    throw new Exception("KeyType " + indexKeyType + " Not Defined");
                }

                // Create Alias
                string keyAlias = keyType + "_";
                DataRow[] keyColumnRows = IndexColumns.Select(string.Format("TABLE_NAME = '{0}' AND CONSTRAINT_NAME = '{1}'", tableName, keyRow["CONSTRAINT_NAME"]));
                Key key = new Key(keyRow["CONSTRAINT_NAME"].ToString(), false, keyType, table, false);

                // Fill Columns
                foreach (DataRow keyColumnRow in keyColumnRows)
                {
                    Column keyColumn = new Column(keyColumnRow["COLUMN_NAME"].ToString(), false);
                    keyColumn.DataType = (string)keyColumnRow["DATA_TYPE"];
                    key.AddColumn(keyColumn);
                }
                if (keyType == DatabaseConstant.KeyType.Foreign)
                {
                    DataRow[] keyReferencedColumnRows = IndexReferencedColumns.Select(string.Format("ForeignKey = '{0}'", keyRow["CONSTRAINT_NAME"]));
                    DataRow firstKeyReferencedColumnRow = keyReferencedColumnRows[0];
                    // Fill References
                    key.ReferencedTable = new Model.Table(firstKeyReferencedColumnRow["ReferencedTable"].ToString(), false);

                    //if (dmoKey.ReferencedKey != null)
                    //{
                    key.ReferencedKey = new Key(firstKeyReferencedColumnRow["ReferencedKey"].ToString(), false, true);
                    //}

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