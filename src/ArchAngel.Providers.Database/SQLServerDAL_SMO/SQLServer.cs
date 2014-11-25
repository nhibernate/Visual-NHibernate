using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Smo;

namespace ArchAngel.Providers.Database.SQLServerDAL_SMO
{
    public sealed class SQLServer
    {
        private SQLServer()
        {
        }

        public static string[] GetSqlServers()
        {
            List<string> sqlServerNames = new List<string>();

            // Get a list of SQL servers available on the networks
            DataTable sqlServers = SmoApplication.EnumAvailableSqlServers(false);

            foreach (DataRow sqlServer in sqlServers.Rows)
            {
                string serverName = sqlServer["Server"].ToString();

                if (sqlServer["Instance"] != null && sqlServer["Instance"].ToString().Length > 0)
                {
                    serverName += @"\" + sqlServer["Instance"].ToString();
                }

                sqlServerNames.Add(serverName);
            }

            return (string[])sqlServerNames.ToArray();
        }
    }
}