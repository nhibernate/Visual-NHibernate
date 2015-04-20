using System;
using System.Collections.Generic;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;

namespace ArchAngel.Providers.Database.SQLServerDAL_ADONET
{
    public class StoredProcedure : SQLServerBase, IStoredProcedure
    {
        public StoredProcedure(string connectionString)
            : base(connectionString)
        {
        }

        public Model.StoredProcedure[] GetStoredProcedures()
        {
            List<Model.StoredProcedure> storedProcedures = new List<Model.StoredProcedure>();

            return (Model.StoredProcedure[])storedProcedures.ToArray();
        }

        public void FillStoredProcedureColumns(Model.StoredProcedure storedProcedure)
        {
        }

        public string GetStoredProcedureBody(string storedProcedureName)
        {
            throw new NotImplementedException("Not coded yet.");
        }
    }
}
