using System.Collections.Generic;
// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.BLL
{
    public class Table : ScriptBLL
    {
        private List<Model.Table> _scriptObjects = new List<Model.Table>();

        public Table(DatabaseTypes dalAssemblyName, ConnectionStringHelper connectionString)
            : base(dalAssemblyName, connectionString)//, Model.Table.TablePrefixes)
        {
            ITable dal = DALFactory.DataAccess.CreateTable(dalAssemblyName, ConnectionString);

            Model.Table[] tables = dal.GetTables();
            //InitiateAlias(tables);
            InitialUpdateIndexes(tables);
            InitialUpdateKeys(tables);
            InitialCreateFilters(tables);
            InitialUpdateRelationships(tables);
            // GFH: I don't think that we should automatically add MapColumns, because we don't know what makes sense in the user's
            // schema. Users get very confused as to why Mapped columns suddenly appear - causes more confusion than anything else.
            //InitialUpdateMapColumns(tables);

            _scriptObjects = new List<Model.Table>(tables);
        }

        public Model.Table[] Tables
        {
            get { return _scriptObjects.ToArray(); }
        }

        private static void InitialUpdateIndexes(IList<Model.Table> tables)
        {
            foreach (Model.Table table in tables)
            {
                foreach (Index index in table.Indexes)
                {
                    // Column
                    for (int i = 0; i < index.Columns.Length; i++)
                    {
                        Column column = Search.GetColumn(tables, table, index.Columns[i].Name);
                        index.UpdateColumn(i, column);
                    }
                }
            }
        }

        private static void InitialUpdateKeys(IList<Model.Table> tables)
        {
            foreach (Model.Table table in tables)
            {
                foreach (Key key in table.Keys)
                {
                    // Column
                    for (int i = 0; i < key.Columns.Length; i++)
                    {
                        Column column = Search.GetColumn(tables, table, key.Columns[i].Name);
                        key.UpdateColumn(i, column);
                    }

                    // Referenced Table
                    if (key.Type == DatabaseConstant.KeyType.Foreign)
                    {
                        Model.Table referencedTable = Search.GetTable(tables, key.ReferencedTable);
                        key.ReferencedTable = referencedTable;
                    }

                    // Referenced Column
                    for (int i = 0; i < key.ReferencedColumns.Length; i++)
                    {
                        Column column = Search.GetColumn(tables, key.ReferencedTable, key.ReferencedColumns[i].Name);
                        key.UpdateReferencedColumn(i, column);
                    }
                }
            }

            foreach (Model.Table table in tables)
            {
                foreach (Key key in table.Keys)
                {
                    if (key.Type == DatabaseConstant.KeyType.Foreign && key.ReferencedKey != null)
                    {
                        // Referenced Key
                        Key referencedKey = Search.GetKey(tables, key.ReferencedTable, key.ReferencedKey.Name);
                        key.ReferencedKey = referencedKey;
                    }
                }
            }
        }

        private static void InitialCreateFilters(IList<Model.Table> tables)
        {
            foreach (Model.Table table in tables)
            {
                // Get All Filter
                string allAlias = "Get" + table.AliasPlural;
                Filter allFilter = new Filter(allAlias, false, table, true, true, false, "", null);
                
                foreach (Column primaryKeyColumn in table.PrimaryKeyColumns)
                {
                    Filter.OrderByColumn orderByColumn = new Filter.OrderByColumn(primaryKeyColumn, "ASC");
                    allFilter.AddOrderByColumn(orderByColumn);
                }

                // Filter[0]
                table.AddFilter(allFilter);

                // Filter[1]
                foreach (Key key in table.Keys)
                {
                    if (key.Type == DatabaseConstant.KeyType.Primary)
                    {
                        //string filterAlias = Helper.GetFilterAlias(key);
                        bool isReturnTypeCollection = false;

                        Filter filter = new Filter(key.Name, false, key.Parent, isReturnTypeCollection, true, false, "", key);

                        foreach (Column column in key.Columns)
                        {
                            string logicalOperator = "";
                            if (filter.FilterColumns.Length > 0)
                            {
                                logicalOperator = "And";
                            }
                            Filter.FilterColumn filterColumn = new Filter.FilterColumn(column, logicalOperator, null, null);
                            filter.AddFilterColumn(filterColumn);
                        }

                        table.AddFilter(filter);
                    }
                }

                // Remaing filters
                foreach (Key key in table.Keys)
                {
                    if (key.Type == DatabaseConstant.KeyType.Foreign ||
                        key.Type == DatabaseConstant.KeyType.Unique ||
                        key.Type == DatabaseConstant.KeyType.None
                        )
                    {
                        //string filterAlias = Helper.GetFilterAlias(key);
                        bool isReturnTypeCollection = false;
                        if (key.Type == DatabaseConstant.KeyType.Foreign ||
                            key.Type == DatabaseConstant.KeyType.None
                            )
                        {
                            isReturnTypeCollection = true;
                        }
                        Filter filter = new Filter(key.Name, false, key.Parent, isReturnTypeCollection, true, false, "", key);

                        foreach (Column column in key.Columns)
                        {
                            string logicalOperator = "";
                            if (filter.FilterColumns.Length > 0)
                            {
                                logicalOperator = "And";
                            }

                            Filter.FilterColumn filterColumn = new Filter.FilterColumn(column, logicalOperator, null, null);
                            filter.AddFilterColumn(filterColumn);
                        }

                        if (isReturnTypeCollection)
                        {
                            foreach (Column primaryKeyColumn in table.PrimaryKeyColumns)
                            {
                                Filter.OrderByColumn orderByColumn = new Filter.OrderByColumn(primaryKeyColumn, "ASC");
                                filter.AddOrderByColumn(orderByColumn);
                            }
                        }

                        table.AddFilter(filter);
                    }
                }

                /*foreach (Index index in table.Indexes)
                {
                    if (index.Type == DatabaseConstant.IndexType.Unique ||
                        index.Type == DatabaseConstant.IndexType.None)
                    {
                        string filterAlias = Helper.GetFilterAlias(index);
                        bool isReturnTypeCollection = false;
                        if (index.Type == DatabaseConstant.KeyType.None)
                        {
                            isReturnTypeCollection = true;
                        }

                        Filter filter = new Filter(index.Name, filterAlias, false, index.Parent, isReturnTypeCollection, true, false, "");

                        foreach (Column column in index.Columns)
                        {
                            string logicalOperator = "";
                            if (filter.FilterColumns.Length > 0)
                            {
                                logicalOperator = "And";
                            }

                            Filter.FilterColumn filterColumn = new Filter.FilterColumn(column, logicalOperator, "=", column.Alias);
                            filter.AddFilterColumn(filterColumn);
                        }

                        if (isReturnTypeCollection)
                        {
                            foreach (Column primaryKeyColumn in table.PrimaryKeyColumns)
                            {
                                Filter.OrderByColumn orderByColumn = new Filter.OrderByColumn(primaryKeyColumn, "ASC");
                                filter.AddOrderByColumn(orderByColumn);
                            }
                        }

                        table.AddFilter(filter);
                    }
                }*/
            }
        }

        private void InitialUpdateRelationships(IList<Model.Table> tables)
        {
            foreach (Model.Table table in tables)
            {
                foreach (Key key in table.Keys)
                {
                    // Look for one to one relationship
                    if (key.Type != DatabaseConstant.KeyType.Foreign)
                    {
                        continue;
                    }
                    if (IsOneToOneRelationship(key))
                    {
                        Filter filter = Search.GetFilter(table.Filters, new Helper(DalAssemblyName).GetPrimaryKey((Model.Table)key.Parent).Name);
                        OneToOneRelationship oneToOneRelationship = new OneToOneRelationship("One_" + key.Name, false, table, key.Columns, key.ReferencedTable, key.ReferencedKey.Columns, filter, false);
                        table.AddRelationship(oneToOneRelationship);

                        // Back other way
                        Filter filter2 = Search.GetFilter(key.ReferencedKey.Parent.Filters, new Helper(DalAssemblyName).GetPrimaryKey((Model.Table)key.ReferencedKey.Parent).Name);
                        OneToOneRelationship oneToOneRelationship2 = new OneToOneRelationship("One_" + key.Name, false, key.ReferencedTable, key.ReferencedKey.Columns, table, key.Columns, filter2, true);
                        key.ReferencedTable.AddRelationship(oneToOneRelationship2);

                        oneToOneRelationship.ForeignRelationship = oneToOneRelationship2;
                        oneToOneRelationship2.ForeignRelationship = oneToOneRelationship;
                    }
                    else
                    {
                        Filter filter = Search.GetFilter(table.Filters, key.Name);
                        ManyToOneRelationship manyToOneRelationship = new ManyToOneRelationship("One_" + key.Name, false, table, key.Columns, key.ReferencedTable, key.ReferencedKey.Columns, filter);
                        table.AddRelationship(manyToOneRelationship);

                        // Back other way
                        Filter filter2 = Search.GetFilter(key.ReferencedTable.Filters, key.ReferencedKey.Name);
                        OneToManyRelationship oneToManyRelationship = new OneToManyRelationship("Many_" + key.Name, false, key.ReferencedTable, key.ReferencedKey.Columns, table, key.Columns, filter2);
                        key.ReferencedTable.AddRelationship(oneToManyRelationship);

                        manyToOneRelationship.ForeignRelationship = oneToManyRelationship;
                        oneToManyRelationship.ForeignRelationship = manyToOneRelationship;
                    }
                }
            }
            // Temp list to search for foreign relationship
            List<Relationship> manyToManyRelationships = new List<Relationship>();
            foreach (Model.Table table in tables)
            {
                foreach (ManyToOneRelationship manyToOneRelationship in table.ManyToOneRelationships)
                {
                    string path = manyToOneRelationship.ForeignScriptObject.Name + " -> " + GetColumnNameList(manyToOneRelationship.ForeignColumns) + " -> " + manyToOneRelationship.PrimaryScriptObject.Name + " -> " + GetColumnNameList(manyToOneRelationship.PrimaryColumns);

                    foreach (ManyToOneRelationship manyToOneRelationship2 in table.ManyToOneRelationships)
                    {
                        if (manyToOneRelationship2.Name == manyToOneRelationship.Name)
                        {
                            continue;
                        }
                        path += " -> " + manyToOneRelationship2.PrimaryScriptObject.Name + " -> " + GetColumnNameList(manyToOneRelationship2.PrimaryColumns) + " -> " + manyToOneRelationship2.ForeignScriptObject.Name + " -> " + GetColumnNameList(manyToOneRelationship2.ForeignColumns);
                        ManyToManyRelationship manyToManyRelationship = new ManyToManyRelationship("Many_" + manyToOneRelationship.Name + "_" + manyToOneRelationship2.Name,
                            false, (OneToManyRelationship)manyToOneRelationship.ForeignRelationship, manyToOneRelationship2, manyToOneRelationship2.ForeignRelationship.Filter);

                        manyToOneRelationship.ForeignScriptObject.AddRelationship(manyToManyRelationship);
                        manyToManyRelationships.Add(manyToManyRelationship);
                    }
                }
			}
			#region Fill foreign relationships
			foreach (Model.Table table in tables)
            {
                foreach (ManyToManyRelationship manyToManyRelationship in table.ManyToManyRelationships)
                {
                    string foreignRelationshipName = "Many_" + manyToManyRelationship.IntermediatePrimaryRelationship.ForeignRelationship.Name + "_" + manyToManyRelationship.IntermediateForeignRelationship.Name;

                    ManyToManyRelationship foreignRelationship = (ManyToManyRelationship)Search.GetRelationship(manyToManyRelationships.ToArray(), foreignRelationshipName);
                    manyToManyRelationship.ForeignRelationship = foreignRelationship;
                }
			}
			#endregion
		}

        private void InitialUpdateMapColumns(IList<Model.Table> tables)
        {
            foreach (Model.Table table in tables)
            {
                foreach (ManyToOneRelationship manyToOneRelationship in table.ManyToOneRelationships)
                {
                	Column dataDisplayColumn = GetDataDisplayColumn((Model.Table)manyToOneRelationship.ForeignScriptObject);

                    if (dataDisplayColumn != null)
                    {
                        MapColumn mapColumn = new MapColumn(manyToOneRelationship.Name, false, new Relationship[] { manyToOneRelationship }, dataDisplayColumn, table.Columns.Length, manyToOneRelationship.PrimaryColumns[0].IsNullable || manyToOneRelationship.ForeignColumns[0].IsNullable, dataDisplayColumn.DataType, dataDisplayColumn.Size, dataDisplayColumn.Precision, dataDisplayColumn.Scale);
                        table.AddColumn(mapColumn);
                    }
                }
            }
        }

        private static bool IsOneToOneRelationship(Key key)
        {
            // Foreign key must reference primary key
            if (key.ReferencedKey.Type != DatabaseConstant.KeyType.Primary)
            {
                return false;
            }
            // Check to see if this key columns are the same as the primary key columns
            Model.Table table = (Model.Table)key.Parent;

            if (key.Columns.Length != table.PrimaryKeyColumns.Length)
            {
                return false;
            }
            foreach (Column column in key.Columns)
            {
                if (!column.InPrimaryKey)
                {
                    return false;
                }
            }
            return true;
        }

        private static string GetColumnNameList(Column[] columns)
        {
            string columnList = "";

            if (columns.Length > 1)
            {
                columnList += "(";
            }
            for (int i = 0; i < columns.Length; i++)
            {
                Column column = columns[i];
                columnList += column.Name;

                if (i < columns.Length - 1)
                {
                    columnList += ",";
                }
            }
            if (columns.Length > 1)
            {
                columnList += ")";
            }
            return columnList;
        }

        private Column GetDataDisplayColumn(Model.Table table)
        {
            foreach (Column column in table.Columns)
            {
                // Don't link MapColumns, because they are virtual!
                if (new BLL.Helper(DalAssemblyName).IsDataTypeText(column) && !ModelTypes.MapColumn.IsInstanceOfType(column))
                {
                    return column;
                }
            }
            return null;
        }
    }
}
