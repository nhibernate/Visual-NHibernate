using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_DMO
{
    public class Table : SQLServerBase, ITable
    {
        private readonly string UnsupportedDataTypes = "'binary', 'sql_variant', 'timestamp', 'varbinary'";

        public Table(BLL.ConnectionStringHelper connectionString)
            : base(connectionString)
        {
        }

        public Model.Table[] GetTables()
        {
            List<Model.Table> tables = new List<Model.Table>();

            foreach (SQLDMO.Table dmoTable in Database.Tables)
            {
                if (dmoTable.Name.Length >= 3 && dmoTable.Name.Substring(0, 3) == "sys")
                {
                    continue;
                }

                if (dmoTable.Name == "dtproperties")
                {
                    continue;
                }

                Model.Table table = GetNewTable(dmoTable);
                tables.Add(table);
            }

            return (Model.Table[])tables.ToArray();
        }

        private Model.Table GetNewTable(SQLDMO.Table dmoTable)
        {
            Model.Table table = new Model.Table(dmoTable.Name, Script.GetSingluar(dmoTable.Name), false);

            // Columns
            int ordinalPosition = 0;
            List<SQLDMO.Column> dmoColumns = new List<SQLDMO.Column>();
            foreach (SQLDMO.Column dmoColumn in dmoTable.Columns)
            {
                dmoColumns.Add(dmoColumn);
            }

            foreach (SQLDMO.Column dmoColumn in dmoColumns)
            {
                if (UnsupportedDataTypes.ToLower().IndexOf("'" + dmoColumn.PhysicalDatatype.ToLower() + "'") >= 0)
                {
                    continue;
                }

                Column column = new Column(dmoColumn.Name, Script.GetSingluar(dmoColumn.Name), false, dmoColumn.Name, table, ordinalPosition, dmoColumn.AllowNulls, dmoColumn.PhysicalDatatype, dmoColumn.Length,
                    dmoColumn.InPrimaryKey, dmoColumn.Identity, dmoColumn.Default, dmoColumn.IsComputed);
                table.AddColumn(column);
                ordinalPosition++;
            }

            // Index
            foreach (SQLDMO.Index dmoIndex in dmoTable.Indexes)
            {
                string indexType;
                if (dmoIndex.Type == SQLDMO.SQLDMO_INDEX_TYPE.SQLDMOIndex_DRIPrimaryKey)
                {
                    continue;
                }
                if (dmoIndex.Type == SQLDMO.SQLDMO_INDEX_TYPE.SQLDMOIndex_DRIUniqueKey)
                {
                    continue;
                }
                else
                {
                    continue;
                    //throw new Exception("IndexType " + dmoIndex.Type + " Not Defined");
                }

                // Create Alias
                string indexAlias = indexType + "_";
                SQLDMO.SQLObjectList indexes = dmoIndex.ListIndexedColumns();

                for (int i = 1; i <= indexes.Count; i++)
                {
                    SQLDMO._Column dmoColumn = (SQLDMO._Column)indexes.Item(i);

                    indexAlias += dmoColumn.Name;
                    if (i < indexes.Count)
                    {
                        indexAlias += "And";
                    }
                }

                Index index = new Index(dmoIndex.Name, Script.GetSingluar(indexAlias), false, indexType, table);

                // Fill Columns
                for (int i = 1; i <= indexes.Count; i++)
                {
                    SQLDMO._Column dmoColumn = (SQLDMO._Column)indexes.Item(i);

                    Column indexColumn = new Column(dmoColumn.Name, Script.GetSingluar(dmoColumn.Name), false);
                    index.AddColumn(indexColumn);
                }

                table.AddIndex(index);
            }

            // Indexes -- that should be keys
            foreach (SQLDMO.Index dmoIndex in dmoTable.Indexes)
            {
                string keyType;
                if (dmoIndex.Type == SQLDMO.SQLDMO_INDEX_TYPE.SQLDMOIndex_DRIPrimaryKey)
                {
                    keyType = DatabaseConstant.KeyType.Primary;
                }
                if (dmoIndex.Type == SQLDMO.SQLDMO_INDEX_TYPE.SQLDMOIndex_DRIUniqueKey)
                {
                    keyType = DatabaseConstant.KeyType.Unique;
                }
                else
                {
                    continue;
                    //throw new Exception("KeyType " + dmoIndex.Type + " Not Defined");
                }

                // Create Alias
                string keyAlias = keyType + "_";
                SQLDMO.SQLObjectList indexes = dmoIndex.ListIndexedColumns();

                for (int i = 1; i <= indexes.Count; i++)
                {
                    SQLDMO._Column dmoColumn = dmoTable.Columns.Item(indexes.Item(i));

                    keyAlias += dmoColumn.Name;
                    if (i < indexes.Count)
                    {
                        keyAlias += "And";
                    }
                }

                Key key = new Key(dmoIndex.Name, Script.GetSingluar(keyAlias), false, keyType, table);

                // Fill Columns
                for (int i = 1; i <= indexes.Count; i++)
                {
                    Column keyColumn = new Column(indexes.Item(i).ToString(), Script.GetSingluar(indexes.Item(i).ToString()), false);
                    key.AddColumn(keyColumn);
                }

                table.AddKey(key);
            }

            // Keys
            foreach (SQLDMO.Key dmoKey in dmoTable.Keys)
            {
                string keyType;
                if (dmoKey.Type == SQLDMO.SQLDMO_KEY_TYPE.SQLDMOKey_Primary)
                {
                    keyType = DatabaseConstant.KeyType.Primary;
                }
                else if (dmoKey.Type == SQLDMO.SQLDMO_KEY_TYPE.SQLDMOKey_Foreign)
                {
                    keyType = DatabaseConstant.KeyType.Foreign;
                }
                else if (dmoKey.Type == SQLDMO.SQLDMO_KEY_TYPE.SQLDMOKey_Unique)
                {
                    keyType = DatabaseConstant.KeyType.Unique;
                }
                else if (dmoKey.Type == SQLDMO.SQLDMO_KEY_TYPE.SQLDMOKey_Unknown)
                {
                    continue;
                }
                else
                {
                    throw new Exception("KeyType " + dmoKey.Type.ToString() + " Not Defined");
                }

                // Create Alias
                string keyAlias = keyType + "_";
                for (int i = 1; i <= dmoKey.KeyColumns.Count; i++)
                {
                    SQLDMO._Column dmoColumn = dmoTable.Columns.Item(dmoKey.KeyColumns.Item(i));

                    keyAlias += dmoColumn.Name;
                    if (i < dmoKey.KeyColumns.Count)
                    {
                        keyAlias += "And";
                    }
                }

                Key key = new Key(dmoKey.Name, Script.GetSingluar(keyAlias), false, keyType, table);

                // Fill Columns
                for (int i = 1; i <= dmoKey.KeyColumns.Count; i++)
                {
                    Column keyColumn = new Column(dmoKey.KeyColumns.Item(i), Script.GetSingluar(dmoKey.KeyColumns.Item(i)), false);
                    key.AddColumn(keyColumn);
                }

                if (keyType == DatabaseConstant.KeyType.Foreign)
                {
                    // Fill References
                    key.ReferencedTable = new Model.Table(dmoKey.ReferencedTable, Script.GetSingluar(dmoKey.ReferencedTable), false);

                    if (dmoKey.ReferencedKey != null)
                    {
                        key.ReferencedKey = new Key(dmoKey.ReferencedKey, Script.GetSingluar(dmoKey.ReferencedKey), false);
                    }

                    // Fill Referenced Columns
                    for (int i = 1; i <= dmoKey.KeyColumns.Count; i++)
                    {
                        Column referencedKeyColumn = new Column(dmoKey.ReferencedColumns.Item(i), Script.GetSingluar(dmoKey.ReferencedColumns.Item(i)), false);
                        key.AddReferencedColumn(referencedKeyColumn);
                    }
                }

                table.AddKey(key);
            }

            return table;
        }
    }
}