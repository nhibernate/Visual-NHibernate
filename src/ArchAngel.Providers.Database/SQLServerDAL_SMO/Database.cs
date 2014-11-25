using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;

namespace ArchAngel.Providers.Database.SQLServerDAL_SMO
{
    public class Database : SQLServerBase
    {
        public Database(BLL.ConnectionStringHelper connectionString)
            : base(connectionString)
        {
        }

        public static string[] GetDatabases(string serverName, string userName, string password, BLL.ConnectionStringHelper.DatabaseTypes dbType, bool trustedConnection)
        {
            List<string> databaseNames = new List<string>();

            Microsoft.SqlServer.Management.Smo.Server server = new Server(new ServerConnection(serverName, userName, password));

            foreach (Microsoft.SqlServer.Management.Smo.Database database in server.Databases)
            {
                databaseNames.Add(database.Name);
            }

            return (string[])databaseNames.ToArray();
        }
    }
}
