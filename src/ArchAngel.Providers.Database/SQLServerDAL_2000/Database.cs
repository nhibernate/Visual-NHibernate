using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_2000
{
    public class Database : SQLServerBase
    {
        public Database(BLL.ConnectionStringHelper connectionString)
            : base(connectionString)
        {
        }

        public static string[] GetDatabases(string serverName, string userName, string password, BLL.ConnectionStringHelper.DatabaseTypes databaseType, bool trustedConnection)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = null;

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

            return (string[])databaseNames.ToArray();
        }
    }
}