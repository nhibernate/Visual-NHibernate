using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_Express
{
    public class Table : SQLServerBase, ITable
    {
        enum KeyTypes
        {
            None = 231,
            Primary = 56,
            Unique = 36,
            Check = 4,
            Foreign = 5
        }
        private System.Data.DataTable _columns = null;
        private System.Data.DataTable _indexes = null;
        private System.Data.DataTable _dtReferencedColumns = null;
        private DataTable _indexColumns = null;
        private DataTable _indexReferencedColumns = null;

        public Table(BLL.ConnectionStringHelper connectionString)
            : base(connectionString)
        {
        }

        private DataTable IndexColumns
        {
            get
            {
                if (_indexColumns == null)
                {
                    //string sql = @"select * from INFORMATION_SCHEMA.KEY_COLUMN_USAGE order by TABLE_NAME, ORDINAL_POSITION";
//                    string sql = @"
//                            SELECT  TABLE_NAME, 
//                                    COLUMN_NAME, 
//                                    CONSTRAINT_NAME
//                            FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE 
//                            ORDER BY TABLE_NAME, CONSTRAINT_NAME";
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

        internal DataTable Columns
        {
            //get
            //{
            //    if (_columns == null)
            //    {
            //        _columns = ConnectionObjectSqlClient.GetSchema(OleDbMetaDataCollectionNames.Columns);//, restrictions);
            //    }
            //    return _columns;
            //}
            get
            {
                if (_columns == null)
                {
                    string sql = string.Format(@"
                        SELECT	DISTINCT 
					            (
						            SELECT COUNT(T.CONSTRAINT_TYPE) 
						            FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS T
							            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE U ON C.Column_Name = U.Column_Name AND C.Table_Name = U.Table_Name
						            WHERE T.Constraint_Name = U.Constraint_Name AND T.CONSTRAINT_TYPE = 'PRIMARY KEY'
					            ) AS InPrimaryKey,
                                C.TABLE_NAME,
					            C.COLUMN_NAME, 
					            CAST(C.ORDINAL_POSITION AS INT) AS [ORDINAL_POSITION],
					            C.IS_NULLABLE,
					            C.DATA_TYPE,
					            C.CHARACTER_MAXIMUM_LENGTH,
					            C.COLUMN_DEFAULT,
	                            COLUMNPROPERTY(object_id(C.TABLE_SCHEMA +'.'+ C.TABLE_NAME), C.COLUMN_NAME, 'IsIdentity') AS IsIdentity,
	                            COLUMNPROPERTY(object_id(C.TABLE_SCHEMA +'.'+ C.TABLE_NAME), C.COLUMN_NAME, 'IsComputed') AS IsComputed,
                                NUMERIC_PRECISION,
                                NUMERIC_SCALE
                        FROM INFORMATION_SCHEMA.COLUMNS C
                        ORDER BY C.TABLE_NAME, C.COLUMN_NAME", DatabaseName);

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
                    string sql = @"
						SELECT  CU.TABLE_NAME, 
                                CU.COLUMN_NAME, 
                                CU.CONSTRAINT_NAME,
								TC.CONSTRAINT_TYPE,
								C.DATA_TYPE,
								CASE WHEN i.type_desc = 'CLUSTERED' THEN 1 ELSE 0 END AS IS_CLUSTERED,
								i.is_unique as IS_UNIQUE
                        FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CU
							INNER JOIN INFORMATION_SCHEMA.COLUMNS C ON C.COLUMN_NAME = CU.COLUMN_NAME AND C.TABLE_NAME = CU.TABLE_NAME
							INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC ON TC.TABLE_NAME = CU.TABLE_NAME AND TC.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
							INNER JOIN sys.indexes AS i ON (i.index_id > 0 and i.is_hypothetical = 0) AND i.name = CU.CONSTRAINT_NAME --(i.object_id = tbl.object_id)

                        UNION

                        SELECT
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
                        WHERE KCU.CONSTRAINT_NAME IS NULL
                        ORDER BY TABLE_NAME, CONSTRAINT_NAME ASC
                        ";
                    _indexes = RunQueryDataTable(sql);
                }
                return _indexes;
            }
        }

        private DataTable DtReferencedColumns
        {
            get
            {
                if (_dtReferencedColumns == null)
                {
                    string sql = string.Format(@"
                        SELECT T.TABLE_NAME AS Referenced_Table_Name,
	                            K.COLUMN_NAME AS Referenced_Column_Name,
                                K.CONSTRAINT_NAME AS Referenced_Key,
								R.CONSTRAINT_NAME AS FOREIGN_KEY
                        FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS R
	                            LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS T ON R.UNIQUE_CONSTRAINT_NAME = T.CONSTRAINT_NAME
	                            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE K ON K.CONSTRAINT_NAME = T.CONSTRAINT_NAME
                        ORDER BY T.TABLE_NAME, UNIQUE_CONSTRAINT_NAME", DatabaseName);

                    _dtReferencedColumns = RunQueryDataTable(sql);
                }
                return _dtReferencedColumns;
            }
        }

        public Model.Table[] GetTables()
        {
            string sql = string.Format(@"SELECT TABLE_NAME,
		                                        (
			                                        SELECT
			                                        CAST(
			                                         CASE 
				                                        WHEN tbl.is_ms_shipped = 1 THEN 1
				                                        WHEN (
					                                        SELECT 
						                                        major_id 
					                                        FROM 
						                                        sys.extended_properties 
					                                        WHERE 
						                                        major_id = tbl.object_id and 
						                                        minor_id = 0 and 
						                                        class = 1 and 
						                                        name = N'microsoft_database_tools_support') 
					                                        IS NOT NULL THEN 1
				                                        ELSE 0
			                                        END          
						                                         AS bit) AS [IsSystemObject]
			                                        FROM
			                                        sys.tables AS tbl
			                                        WHERE
			                                        (tbl.name = T.TABLE_NAME and SCHEMA_NAME(tbl.schema_id)=N'dbo')
		                                        ) AS IsSystemObject
                                        FROM INFORMATION_SCHEMA.TABLES T
                                        WHERE TABLE_TYPE = 'BASE TABLE'
                                        ORDER BY T.TABLE_NAME", DatabaseName);
            System.Collections.ArrayList arrTableNames = new System.Collections.ArrayList();
            System.Data.SqlClient.SqlDataReader dr = null;

            try
            {
                dr = RunQuerySqlClient(sql);
                bool isSystemObject;
                int isSysObjectColumnOrdinal = dr.GetOrdinal("IsSystemObject");

                while (dr.Read())
                {
                    isSystemObject = dr.IsDBNull(isSysObjectColumnOrdinal) ? false : (bool)dr[isSysObjectColumnOrdinal];

                    if (!isSystemObject || Model.Database.IncludeSystemObjects)
                    {
                        arrTableNames.Add((string)dr["TABLE_NAME"]);
                    }
                }
            }
            finally
            {
                if (dr != null) { dr.Close(); }
            }
            List<Model.Table> tables = new List<Model.Table>();

            for (int i = 0; i < arrTableNames.Count; i++)
            {
                Model.Table table = GetNewTable((string)arrTableNames[i]);
                tables.Add(table);
            }
            return (Model.Table[])tables.ToArray();
        }

        private Model.Table GetNewTable(string tableName)
        {
            ArchAngel.Interfaces.Events.RaiseObjectBeingProcessedEvent(tableName, "Table");
            //_columns = null; // Reset the columns
            //_indexes = null;
            //_dtReferencedColumns = null;
            //return new Model.Table();
            Model.Table table = new Model.Table(tableName, false);

            #region Columns
            DataRow[] columnRows = Columns.Select(string.Format("TABLE_NAME = '{0}'", tableName));

            foreach (DataRow row in columnRows)
            {
                bool isReadOnly = false;

                if (!row.IsNull("IsIdentity") && (int)row["IsIdentity"] == 1)
                {
                    isReadOnly = true;
                }
                else if (!row.IsNull("IsComputed") && (int)row["IsComputed"] == 1)
                {
                    isReadOnly = true;
                }
                else if (Slyce.Common.Utility.StringsAreEqual((string)row["DATA_TYPE"], "timestamp", false))
                {
                    isReadOnly = true;
                }
                // Check whether we have added this column before. Columns are repeated if they are both a PRIMARY_KEY and a FOREIGN_KEY
                Column column = new Column(
                    (string)row["COLUMN_NAME"],
                    false,
                    table,
                    (int)row["ORDINAL_POSITION"],
                    Slyce.Common.Utility.StringsAreEqual((string)row["IS_NULLABLE"], "YES", false),
                    (string)row["DATA_TYPE"],
                    row.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : Convert.ToInt32(row["CHARACTER_MAXIMUM_LENGTH"]),
                    (int)row["InPrimaryKey"] == 1,
                    row.IsNull("IsIdentity") ? false : Convert.ToInt32(row["IsIdentity"]) == 1,
                    row.IsNull("COLUMN_DEFAULT") ? "" : (string)row["COLUMN_DEFAULT"],
                    isReadOnly,
                    row.IsNull("IsComputed") ? false : Convert.ToInt32(row["IsComputed"]) == 1,
                    row.IsNull("NUMERIC_PRECISION") ? 0 : Convert.ToInt32(row["NUMERIC_PRECISION"]),
                    row.IsNull("NUMERIC_SCALE") ? 0 : Convert.ToInt32(row["NUMERIC_SCALE"]));

                table.AddColumn(column);
                //ordinalPosition++;
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
                Index index = new Index(indexRow["CONSTRAINT_NAME"].ToString(), false, indexType, table, (bool)indexRow["IS_UNIQUE"], (bool)indexRow["IS_CLUSTERED"]);

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
                Key key = new Key(keyRow["CONSTRAINT_NAME"].ToString(), false, keyType, table, false);
                DataRow[] keyColumnRows = IndexColumns.Select(string.Format("TABLE_NAME = '{0}' AND CONSTRAINT_NAME = '{1}'", tableName, keyRow["CONSTRAINT_NAME"]));

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

            //#region Indexes -- that should be keys
            //string prevConstraintName = "";
            //Key key = null;
            //DataRow[] indexRows = DtIndexes.Select(string.Format("TABLE_NAME = '{0}'", tableName));

            //for (int rowCounter = 0; rowCounter < indexRows.Length; rowCounter++)
            //{
            //    DataRow row = indexRows[rowCounter];
            //    for (int colCounter = 0; colCounter < DtIndexes.Columns.Count; colCounter++)
            //    {
            //        string colName = DtIndexes.Columns[colCounter].ColumnName;
            //    }
            //    string keyType;
            //    string indexKeyType = row.IsNull("Constraint_Type") ? "NONE" : (string)row["Constraint_Type"];
            //    string constraintName = row.IsNull("Constraint_Name") ? "" : (string)row["Constraint_Name"];
            //    string columnName = (string)row["COLUMN_NAME"];

            //    if (indexKeyType == "NONE")
            //    {
            //        keyType = DatabaseConstant.KeyType.None;
            //    }
            //    else if (indexKeyType == "PRIMARY KEY")
            //    {
            //        keyType = DatabaseConstant.KeyType.Primary;
            //    }
            //    else if (indexKeyType == "UNIQUE")
            //    {
            //        keyType = DatabaseConstant.KeyType.Unique;
            //    }
            //    else if (indexKeyType == "CHECK") // TODO: was 'None' for SMO
            //    {
            //        keyType = DatabaseConstant.KeyType.None;
            //    }
            //    else if (indexKeyType == "FOREIGN KEY") // TODO: was 'None' for SMO
            //    {
            //        keyType = DatabaseConstant.KeyType.Foreign;
            //    }
            //    else
            //    {
            //        throw new Exception("KeyType " + indexKeyType + " Not Defined");
            //    }
            //    // Create Alias
            //    if (string.Format("{0}{1}", constraintName, keyType) != prevConstraintName)
            //    {
            //        if (key != null)
            //        {
            //            // Reset the alias, because it is based on the Columns collection which has just finished being modified.
            //            key.ResetDefaults();
            //        }
            //        // Create a new Key
            //        key = new Key(constraintName, false, keyType, table, false);
            //        table.AddKey(key);
            //        prevConstraintName = string.Format("{0}{1}", constraintName, keyType);
            //        Column keyColumn = new Column(columnName, false);
            //        key.AddColumn(keyColumn);
            //    }
            //    else
            //    {
            //        // We are processing another column of the same Index as the previous index
            //        Column keyColumn = new Column(columnName, false);
            //        key.AddColumn(keyColumn);
            //    }
            //    if (keyType == DatabaseConstant.KeyType.Foreign)
            //    {
            //        DataRow[] referencedColumnRows = DtReferencedColumns.Select(string.Format("FOREIGN_KEY = '{0}'", key.Name));

            //        foreach (DataRow refColRow in referencedColumnRows)
            //        {
            //            // Fill References
            //            if (key.ReferencedTable == null)
            //            {
            //                string referencedTableName = (string)refColRow["Referenced_Table_Name"];
            //                string referencedKeyName = (string)refColRow["Referenced_Key"];

            //                key.ReferencedTable = new Model.Table(referencedTableName, false);
            //                key.ReferencedKey = new Key(referencedKeyName, false, true);
            //            }
            //            string referencedColumnName = (string)refColRow["Referenced_Column_Name"];

            //            // Fill Referenced Columns
            //            Column referencedKeyColumn = new Column(referencedColumnName, false);
            //            key.AddReferencedColumn(referencedKeyColumn);
            //        }
            //    }
            //    key.ResetDefaults();
            //}
            //#endregion

            return table;
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
    }
}