using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ArchAngel.Providers.Database.SQLServerDAL_2005
{
    public class Database : SQLServerBase
    {
        public Database(BLL.ConnectionStringHelper connectionString)
            : base(connectionString)
        {
        }

        public static string[] GetDatabases(string serverName, string userName, string password, DatabaseTypes databaseType, bool trustedConnection)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn;

            if (trustedConnection)
            {
                conn = new SqlConnection(string.Format("Server={0};Database=master;Trusted_Connection=True", serverName));
            }
            else
            {
                conn = new SqlConnection(string.Format("Server={0};Database=master;User ID={1};Password={2};Trusted_Connection=False", serverName, userName, password));
            }

            List<string> databaseNames = new List<string>();

            try
            {
                conn.Open();

                cmd.Connection = conn;
                cmd.CommandText = "SELECT name FROM master..sysdatabases";
                cmd.CommandType = CommandType.Text;

                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();

                while (rdr.Read())
                {
                    databaseNames.Add(rdr.GetString(0));
                }
            }
            catch
            {
                conn.Close();
                throw;
            }

            return databaseNames.ToArray();
        }
    }
}