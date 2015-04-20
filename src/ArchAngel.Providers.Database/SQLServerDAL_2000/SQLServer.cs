using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_2000
{
    public sealed class SQLServer
    {
        private SQLServer()
        {
        }

        public static string[] GetSqlServers()
        {
            return ArchAngel.Providers.Database.Helper.SQLServer.GetSqlServers();
        }
    }
}
