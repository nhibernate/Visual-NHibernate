using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Helper;
using System.IO;

namespace ArchAngel.Providers.EntityModel.UnitTesting
{
    public class SqlServerHelper
    {
        public static ConnectionStringHelper ConnStringHelper = new ConnectionStringHelper()
        {
            CurrentDbType = DatabaseTypes.SQLServer2005,
            DatabaseName = "TestDatabase",
            ServerName = ".",
            UseFileName = false,
            UseIntegratedSecurity = true
        };

        /// <summary>
        /// Creates a new database on the server, dropping the existing one first if it exists.
        /// </summary>
        public static void CreateDatabaseFiles()
        {
            ConnectionStringHelper masterConnStringHelper = new ConnectionStringHelper()
            {
                CurrentDbType = DatabaseTypes.SQLServer2005,
                DatabaseName = "master",
                ServerName = ".",
                UseFileName = false,
                UseIntegratedSecurity = true
            };
            SQLServer2005DatabaseConnector database = new SQLServer2005DatabaseConnector(masterConnStringHelper);
            database.Open();
            string sql = File.ReadAllText("CreateDatabase - SQL Server.sql");
            sql = sql.Replace("[LOCATION]", Path.GetTempPath());
            database.RunNonQuerySQL(sql);
            database.Close();
        }
    }
}
