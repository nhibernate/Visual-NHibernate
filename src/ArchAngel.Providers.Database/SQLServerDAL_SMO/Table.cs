using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_SMO
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

            foreach (Microsoft.SqlServer.Management.Smo.Table smoTable in Database.Tables)
            {
                if (!smoTable.IsSystemObject)
                {
                    Model.Table table = GetNewTable(smoTable);
                    tables.Add(table);
                }
            }
            return (Model.Table[])tables.ToArray();
        }

        private Model.Table GetNewTable(Microsoft.SqlServer.Management.Smo.Table smoTable)
        {
            Model.Table table = new Model.Table(smoTable.Name, Script.GetSingluar(smoTable.Name), false);

            #region Columns
            int ordinalPosition = 0;
            List<Microsoft.SqlServer.Management.Smo.Column> smoColumns = new List<Microsoft.SqlServer.Management.Smo.Column>();

            foreach (Microsoft.SqlServer.Management.Smo.Column smoColumn in smoTable.Columns)
            {
                smoColumns.Add(smoColumn);
            }
            foreach (Microsoft.SqlServer.Management.Smo.Column smoColumn in smoColumns)
            {
                if (UnsupportedDataTypes.ToLower().IndexOf("'" + smoColumn.DataType.Name.ToLower() + "'") >= 0)
                {
                    continue;
                }

                // Some columns do not have default values
                string defaultValue = null;
                try
                {
                    defaultValue = smoColumn.Default;
                }
                catch { }

                Column column = new Column(smoColumn.Name, Script.GetSingluar(smoColumn.Name), false, smoColumn.Name, table, ordinalPosition, smoColumn.Nullable, smoColumn.DataType.Name,
                    smoColumn.DataType.MaximumLength, smoColumn.InPrimaryKey, smoColumn.Identity, defaultValue, smoColumn.Computed);
                table.AddColumn(column);
                ordinalPosition++;
            }
            #endregion

            #region Indexes
            //foreach (Microsoft.SqlServer.Management.Smo.Index smoIndex in smoTable.Indexes)
            //{
            //    string indexType;
            //    string indexKeyType = smoIndex.IndexKeyType.ToString();
            //    if (indexKeyType == "DriPrimaryKey")
            //    {
            //        continue;
            //    }
            //    else if (indexKeyType == "DriUniqueKey")
            //    {
            //        continue;
            //    }
            //    else if (indexKeyType == "None")
            //    {
            //        continue;
            //        //indexType = DatabaseConstant.IndexType.None;
            //    }
            //    else
            //    {
            //        throw new Exception("IndexType " + indexKeyType + " Not Defined");
            //    }

            //    // Create Alias
            //    string indexAlias = indexType + "_";
            //    for (int i = 0; i <= smoIndex.IndexedColumns.Count - 1; i++)
            //    {
            //        Microsoft.SqlServer.Management.Smo.IndexedColumn smoIndexedColumn = smoIndex.IndexedColumns[i];

            //        indexAlias += smoIndexedColumn.Name;
            //        if (i < smoIndex.IndexedColumns.Count - 1)
            //        {
            //            indexAlias += "And";
            //        }
            //    }

            //    Index index = new Index(smoIndex.Name, Script.GetSingluar(indexAlias), false, indexType, table);

            //    // Fill Columns
            //    foreach (Microsoft.SqlServer.Management.Smo.IndexedColumn smoColumn in smoIndex.IndexedColumns)
            //    {
            //        Column indexColumn = new Column(smoColumn.Name, Script.GetSingluar(smoColumn.Name), false);
            //        index.AddColumn(indexColumn);
            //    }

            //    table.AddIndex(index);
            //}

            // Indexes -- that should be keys
            foreach (Microsoft.SqlServer.Management.Smo.Index smoIndex in smoTable.Indexes)
            {
                string keyType;
                string indexKeyType = smoIndex.IndexKeyType.ToString();
                if (indexKeyType == "DriPrimaryKey")
                {
                    keyType = DatabaseConstant.KeyType.Primary;
                }
                else if (indexKeyType == "DriUniqueKey")
                {
                    keyType = DatabaseConstant.KeyType.Unique;
                }
                else if (indexKeyType == "None")
                {
                    keyType = DatabaseConstant.KeyType.None;
                    continue;
                }
                else
                {
                    throw new Exception("KeyType " + indexKeyType + " Not Defined");
                }

                // Create Alias
                string keyAlias = keyType + "_";
                for (int i = 0; i <= smoIndex.IndexedColumns.Count - 1; i++)
                {
                    Microsoft.SqlServer.Management.Smo.Column smoColumn = smoTable.Columns[smoIndex.IndexedColumns[i].Name];

                    keyAlias += smoColumn.Name;
                    if (i < smoIndex.IndexedColumns.Count - 1)
                    {
                        keyAlias += "And";
                    }
                }

                Key key = new Key(smoIndex.Name, Script.GetSingluar(keyAlias), false, keyType, table);

                // Fill Columns
                foreach (Microsoft.SqlServer.Management.Smo.IndexedColumn smoColumn in smoIndex.IndexedColumns)
                {
                    Column keyColumn = new Column(smoColumn.Name, Script.GetSingluar(smoColumn.Name), false);
                    key.AddColumn(keyColumn);
                }

                table.AddKey(key);
            }
            #endregion

            #region Keys
            foreach (Microsoft.SqlServer.Management.Smo.ForeignKey smoForeignKey in smoTable.ForeignKeys)
            {
                string keyType = DatabaseConstant.KeyType.Foreign;

                // Create Alias
                string keyAlias = keyType + "_";
                for (int i = 0; i <= smoForeignKey.Columns.Count - 1; i++)
                {
                    Microsoft.SqlServer.Management.Smo.Column smoColumn = smoTable.Columns[smoForeignKey.Columns[i].Name];

                    keyAlias += smoColumn.Name;
                    if (i < smoForeignKey.Columns.Count - 1)
                    {
                        keyAlias += "And";
                    }
                }

                Key key = new Key(smoForeignKey.Name, Script.GetSingluar(keyAlias), false, keyType, table);

                // Fill Columns
                foreach (Microsoft.SqlServer.Management.Smo.ForeignKeyColumn smoColumn in smoForeignKey.Columns)
                {
                    Column keyColumn = new Column(smoColumn.Name, Script.GetSingluar(smoColumn.Name), false);
                    key.AddColumn(keyColumn);
                }

                // Fill References
                key.ReferencedTable = new Model.Table(smoForeignKey.ReferencedTable, Script.GetSingluar(smoForeignKey.ReferencedTable), false);
                key.ReferencedKey = new Key(smoForeignKey.ReferencedKey, Script.GetSingluar(smoForeignKey.ReferencedKey), false);

                // Fill Referenced Columns
                foreach (Microsoft.SqlServer.Management.Smo.ForeignKeyColumn smoColumn in smoForeignKey.Columns)
                {
                    Column referencedKeyColumn = new Column(smoColumn.ReferencedColumn, Script.GetSingluar(smoColumn.ReferencedColumn), false);
                    key.AddReferencedColumn(referencedKeyColumn);
                }

                table.AddKey(key);
            }
            #endregion

            return table;
        }
    }
}