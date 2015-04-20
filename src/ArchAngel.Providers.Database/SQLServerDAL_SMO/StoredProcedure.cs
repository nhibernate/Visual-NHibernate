using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Smo;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_SMO
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

            foreach (Microsoft.SqlServer.Management.Smo.StoredProcedure smoStoredProcedure in Database.StoredProcedures)
            {
                if (!smoStoredProcedure.IsSystemObject)
                {
                    Model.StoredProcedure storedProcedure = GetNewStoredProcedure(smoStoredProcedure);
                    storedProcedures.Add(storedProcedure);
                }
            }

            return (Model.StoredProcedure[])storedProcedures.ToArray();
        }

        private Model.StoredProcedure GetNewStoredProcedure(Microsoft.SqlServer.Management.Smo.StoredProcedure smoStoredProcedure)
        {
            Model.StoredProcedure storedProcedure = new Model.StoredProcedure(smoStoredProcedure.Name, Script.GetSingluar(smoStoredProcedure.Name), false);
            storedProcedure.Enabled = false;

            // Columns
            //int ordinalPosition = 0;
            //Microsoft.SqlServer.Management.Smo.Column[] smoColumns = GetColumns(smoStoredProcedure);
            //foreach (Microsoft.SqlServer.Management.Smo.Column smoColumn in smoColumns)
            //{
            /*if (UnsupportedDataTypes.ToLower().IndexOf("'" + smoColumn.PhysicalDatatype.ToLower() + "'") >= 0)
            {
                continue;
            }

            Column column = new Column(smoColumn.Name, Script.GetSingluar(smoColumn.Name), smoColumn.Name, storedProcedure, ordinalPosition, smoColumn.Nullable, smoColumn.PhysicalDatatype, smoColumn.Length,
                smoColumn.InPrimaryKey, false, false, smoColumn.Identity);
            storedProcedure.Columns.Add(column);
            ordinalPosition++;*/
            //}

            // Parameters
            foreach (Microsoft.SqlServer.Management.Smo.Parameter smoParameter in smoStoredProcedure.Parameters)
            {
                //Model.StoredProcedure.Parameter parameter = new Model.StoredProcedure.Parameter(smoParameter.Name, smoParameter.Name.Replace("@", ""), smoParameter.DataType.Name);
                //storedProcedure.AddParameter(parameter);
            }

            return storedProcedure;
        }

        public void FillStoredProcedureColumns(Model.StoredProcedure storedProcedure)
        {
        }
    }
}
