using System;
using System.Collections.Generic;
// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.IDAL;

namespace ArchAngel.Providers.Database.AccessDAL
{
    public class Table : AccessBase, ITable
    {
        public Table(string fileName)
            : base(fileName)
        {
        }

        public Model.Table[] GetTables()
        {
            List<Model.Table> tables = new List<Model.Table>();

            return tables.ToArray();
        }

        public System.Data.DataTable RunQueryDataTable(string sql)
        {
            throw new NotImplementedException("Not coded yet: SQLServerDAL_ADONET.RunQueryDataTable()");
        }
    }
}
