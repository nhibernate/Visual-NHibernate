using System;
using System.Collections.Generic;
using System.Text;

namespace ArchAngel.Providers.Database.SQLServerDAL_DMO
{
    public sealed class SQLServer
    {
        private SQLServer()
        {
        }

        public static string[] GetSqlServers()
        {
            List<string> servers = new List<string>();
            SQLDMO.ApplicationClass application = new SQLDMO.ApplicationClass();
            foreach (SQLDMO.ServerGroup serverGroup in application.ServerGroups)
            {
                foreach (SQLDMO.RegisteredServer server in serverGroup.RegisteredServers)
                {
                    servers.Add(server.Name);
                }
            }

            return (string[])servers.ToArray();
        }
    }
}
