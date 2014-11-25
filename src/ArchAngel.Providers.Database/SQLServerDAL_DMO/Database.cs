using System;
using System.Collections.Generic;
using System.Text;
using SQLDMO;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;

namespace ArchAngel.Providers.Database.SQLServerDAL_DMO
{
    public class Database : SQLServerBase
    {
        public Database(BLL.ConnectionStringHelper connectionString)
            : base(connectionString)
        {
        }

        public static string[] GetDatabases(string serverName, string userName, string password, BLL.ConnectionStringHelper.DatabaseTypes dbType, bool trustedConnection)
        {
            List<string> databases = new List<string>();

            SQLDMO.SQLServer sqlServer = new SQLServerClass();
            sqlServer.Connect(serverName, userName, password);

            foreach (SQLDMO.Database database in sqlServer.Databases)
            {
                databases.Add(database.Name);
            }

            return (string[])databases.ToArray();
        }
    }
}
