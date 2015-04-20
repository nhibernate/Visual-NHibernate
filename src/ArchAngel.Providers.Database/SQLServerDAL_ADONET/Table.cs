using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;

namespace ArchAngel.Providers.Database.SQLServerDAL_ADONET
{
    public class Table : SQLServerBase, ITable
    {
        private readonly DbProviderFactory _dbProviderFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");

        public Table(string connectionString)
            : base(connectionString)
        {
        }

        public Model.Table[] GetTables()
        {
            List<Model.Table> tables = new List<Model.Table>();

            using (DbConnection dbConnection = _dbProviderFactory.CreateConnection())
            {
                dbConnection.ConnectionString = ConnectionString;
                dbConnection.Open();

                string[] restrictions = new string[4] { null, null, null, ConstName.Base_Table };
                DataTable dataTable = dbConnection.GetSchema(ConstName.Tables, restrictions);
                //dataTable.WriteXml("C:\\temp.xml");

                foreach (DataRow dataRowTable in dataTable.Rows)
                {
                    Model.Table table = GetNewTable(dataRowTable);
                    tables.Add(table);
                }
            }

            return (Model.Table[])tables.ToArray();
        }

        private Model.Table GetNewTable(DataRow dataRowTable)
        {
            string tableName = dataRowTable[ConstName.Table_Name].ToString();
            Model.Table table = null;// new Model.Table(tableName, tableName);

            // Columns
            using (DbConnection dbConnection = _dbProviderFactory.CreateConnection())
            {
                dbConnection.ConnectionString = ConnectionString;
                dbConnection.Open();

                string[] restrictions = new string[4] { null, null, tableName, null };
                DataTable dataTable = dbConnection.GetSchema(ConstName.Columns, restrictions);
                //dataTable.WriteXml("C:\\temp.xml");

                foreach (DataRow dataRowColumn in dataTable.Rows)
                {
                    Column column = GetNewColumn(dataRowColumn);
                    table.AddColumn(column);
                }
            }

            // Keys
            /*using (DbConnection dbConnection = _dbProviderFactory.CreateConnection())
            {
                dbConnection.ConnectionString = ConnectionString;
                dbConnection.Open();

                string[] restrictions = new string[4] { null, null, tableName, null };
                DataTable dataTable = dbConnection.GetSchema(ConstName.ForeignKeys, restrictions);
                //dataTable.WriteXml("C:\\temp.xml");

                foreach (DataRow dataRowColumn in dataTable.Rows)
                {
                    Column column = GetNewColumn(dataRowColumn);
                    table.Columns.Add(column);
                }
            }*/

            return table;
        }

        private Column GetNewColumn(DataRow dataRow)
        {
            string columnName = dataRow[ConstName.Column_Name].ToString();
            int ordinalPosition = Convert.ToInt32(dataRow[ConstName.Ordinal_Position]);
            bool isNullable = ColumnHelper.ToBoolean(dataRow[ConstName.Is_Nullable].ToString());
            string dataType = dataRow[ConstName.Data_Type].ToString();
            int characterMaximumLength = -1;
            if (ColumnHelper.IsText(dataType))
            {
                characterMaximumLength = Convert.ToInt32(dataRow[ConstName.Character_Maximum_Length]);
            }

            Column column = null;// new Column(columnName, columnName, ordinalPosition, isNullable, dataType, characterMaximumLength);

            return column;
        }
    }
}
