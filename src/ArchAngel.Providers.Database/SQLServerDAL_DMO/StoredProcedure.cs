using System;
using System.Collections.Generic;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_DMO
{
    public class StoredProcedure : SQLServerBase, IStoredProcedure
    {
        // TODO; Implement
        //private readonly string UnsupportedDataTypes = "'binary', 'sql_variant', 'timestamp', 'varbinary'";

        public StoredProcedure(BLL.ConnectionStringHelper connectionString)
            : base(connectionString)
        {
        }

        public Model.StoredProcedure[] GetStoredProcedures()
        {
            List<Model.StoredProcedure> storedProcedures = new List<Model.StoredProcedure>();

            foreach (SQLDMO.StoredProcedure dmoStoredProcedure in Database.StoredProcedures)
            {
                Model.StoredProcedure storedProcedure = GetNewStoredProcedure(dmoStoredProcedure);
                storedProcedures.Add(storedProcedure);
            }

            return (Model.StoredProcedure[])storedProcedures.ToArray();
        }

        private Model.StoredProcedure GetNewStoredProcedure(SQLDMO.StoredProcedure dmoStoredProcedure)
        {
            Model.StoredProcedure storedProcedure = new Model.StoredProcedure(dmoStoredProcedure.Name, Script.GetSingluar(dmoStoredProcedure.Name), false);
            storedProcedure.Enabled = false;

            // Columns
            //int ordinalPosition = 0;
            //SQLDMO.Column[] dmoColumns = GetColumns(dmoStoredProcedure);
            //foreach (SQLDMO.Column dmoColumn in dmoColumns)
            //{
            /*if (UnsupportedDataTypes.ToLower().IndexOf("'" + dmoColumn.PhysicalDatatype.ToLower() + "'") >= 0)
            {
                continue;
            }

            Column column = new Column(dmoColumn.Name, Script.GetSingluar(dmoColumn.Name), dmoColumn.Name, storedProcedure, ordinalPosition, dmoColumn.AllowNulls, dmoColumn.PhysicalDatatype, dmoColumn.Length,
                dmoColumn.InPrimaryKey, false, false, dmoColumn.Identity);
            storedProcedure.Columns.Add(column);
            ordinalPosition++;*/
            //}

            // Parameters
            SQLDMO.QueryResults queryResults = dmoStoredProcedure.EnumParameters();
            for (int i = 1; i <= queryResults.Rows; i++)
            {
                //Model.StoredProcedure.Parameter parameter = new Model.StoredProcedure.Parameter(queryResults.GetColumnString(i, 1), queryResults.GetColumnString(i, 1).Replace("@", ""), queryResults.GetColumnString(i, 2));
                //storedProcedure.AddParameter(parameter);
            }

            return storedProcedure;
        }

        public void FillStoredProcedureColumns(Model.StoredProcedure storedProcedure)
        {
        }
    }
}
