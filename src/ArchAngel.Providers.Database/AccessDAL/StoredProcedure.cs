using System;
using System.Collections.Generic;
// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.IDAL;

namespace ArchAngel.Providers.Database.AccessDAL
{
    public class StoredProcedure : AccessBase, IStoredProcedure
    {
        public StoredProcedure(string fileName)
            : base(fileName)
        {
        }

        public Model.StoredProcedure[] GetStoredProcedures()
        {
            List<Model.StoredProcedure> storedProcedures = new List<Model.StoredProcedure>();

            return storedProcedures.ToArray();
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
