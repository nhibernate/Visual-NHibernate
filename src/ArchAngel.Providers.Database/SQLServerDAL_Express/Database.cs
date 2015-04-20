using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;

namespace ArchAngel.Providers.Database.SQLServerDAL_Express
{
    public class Database : SQLServerBase
    {
        public Database(BLL.ConnectionStringHelper connectionString)
            : base(connectionString)
        {
        }

        public static string[] GetDatabases(string serverName, string userName, string password, DatabaseTypes dbType, bool trustedConnection)
        {
            // SQL Server, Sybase: SELECT * FROM master..sysdatabases
            // MySQL: show databases;
            // Oracle: SELECT tablespace_name FROM dba_tablespaces
            List<string> arrDatabases = new List<string>();

            SqlConnection conn = null;
            System.Data.SqlClient.SqlDataReader dr = null;

            if (trustedConnection)
            {
                conn = new SqlConnection(string.Format("Server={0};Database=master;Trusted_Connection=True", serverName));
            }
            else
            {
                conn = new SqlConnection(string.Format("Server={0};Database=master;User ID={1};Password={2};Trusted_Connection=False", serverName, userName, password));
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM master..sysdatabases", conn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    arrDatabases.Add(dr.GetString(0));
                }
            }
            finally
            {
                if (dr != null) { dr.Close(); }
                if (conn != null) { conn.Close(); }
            }

            return (string[])arrDatabases.ToArray();
        }
    }
}