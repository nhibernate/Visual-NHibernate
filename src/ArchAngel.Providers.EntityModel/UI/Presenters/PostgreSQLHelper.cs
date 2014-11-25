using System;
using System.Collections.Generic;
using System.Data;
using Devart.Data.Universal;
using ArchAngel.Providers.EntityModel.Helper;
using Microsoft.Win32;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
    public static class PostgreSQLHelper
    {
        /// <summary>
        /// Gets all local and network instances of PostgreSQL.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetPostgreSQLInstances()
        {
            return new List<string>();

            //List<string> serverNames = new List<string>();

            //#region Instances On Local Machine

            //string keyName;

            //if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Microsoft SQL Server") != null)
            //{
            //    // we have a 64 bit version of windows
            //    keyName = @"SOFTWARE\Wow6432Node\Microsoft\Microsoft SQL Server";
            //}
            //else
            //{
            //    // we have a 32 bit version of windows
            //    keyName = @"SOFTWARE\Microsoft\Microsoft SQL Server";
            //}
            //RegistryKey sqlServerRegistryKeys = Registry.LocalMachine.OpenSubKey(keyName);

            //if (sqlServerRegistryKeys != null)
            //{
            //    object installedInstanceRegValue = sqlServerRegistryKeys.GetValue("InstalledInstances");

            //    if (installedInstanceRegValue != null)
            //    {
            //        string[] instanceNames = (string[])installedInstanceRegValue;

            //        if (instanceNames.GetUpperBound(0) > 0)
            //        {
            //            foreach (string element in instanceNames)
            //            {
            //                if (element == "MSSQLSERVER")
            //                {
            //                    if (serverNames.BinarySearch(Environment.MachineName) < 0)
            //                    {
            //                        serverNames.Add(Environment.MachineName);
            //                        serverNames.Sort();
            //                    }
            //                }
            //                else if (element != "SQLEXPRESS")
            //                {
            //                    // This is where \SQLEXPRESS is added if it exists on the machine,
            //                    // but we don't want it when the user is searching for SQL Servers.
            //                    string sqlExpressServerName = Environment.MachineName + @"\" + element;

            //                    if (serverNames.BinarySearch(sqlExpressServerName) < 0)
            //                    {
            //                        serverNames.Add(sqlExpressServerName);
            //                        serverNames.Sort();
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            //#endregion

            //#region Network Servers

            //DataTable dataSources = System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources();

            //foreach (DataRow row in dataSources.Rows)
            //{
            //    string serverName = row["ServerName"].ToString();

            //    if (serverNames.BinarySearch(serverName) < 0)
            //    {
            //        serverNames.Add(serverName);
            //        serverNames.Sort();
            //    }
            //}

            //#endregion

            //return serverNames;
        }

        public static string[] GetPostgreSQLDatabases(ConnectionStringHelper helper)
        {
            string serverName = helper.ServerName;
            string userName = helper.UserName;
            string password = helper.Password;
            bool trustedConnection = helper.UseIntegratedSecurity;
            int port = helper.Port;

            UniConnection conn = new UniConnection(string.Format("Provider=PostgreSQL;host={0};port={1};user={2};password={3};initial schema=Public;", serverName, port, userName, password));
            List<string> databaseNames = new List<string>();

            try
            {
                conn.Open();

                using (UniCommand sqlCommand = new UniCommand(@"
                            SELECT datname
                            FROM pg_catalog.pg_database
                            where not datistemplate
                            ORDER BY datname
                            ", conn))
                {
                    using (UniDataReader dr = sqlCommand.ExecuteReader())
                    {
                        while (dr.Read())
                            databaseNames.Add(dr.GetString(0));
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return databaseNames.ToArray();
        }

        public static string[] GetPostgreSQLSchemas(ConnectionStringHelper helper)
        {
            string serverName = helper.ServerName;
            string userName = helper.UserName;
            string password = helper.Password;
            bool trustedConnection = helper.UseIntegratedSecurity;
            int port = helper.Port;
            string database = helper.DatabaseName;

            UniConnection conn = new UniConnection(string.Format("Provider=PostgreSQL;host={0};port={1};user={2};password={3};initial schema=Public;database={4};", serverName, port, userName, password, database));
            List<string> databaseNames = new List<string>();

            try
            {
                conn.Open();

                using (UniCommand sqlCommand = new UniCommand(@"
                                        SELECT schema_name 
                                        FROM information_schema.schemata
                                        WHERE   schema_name not like 'pg_catalog%' and 
                                                schema_name not like 'pg_toast%' and
                                                schema_name not like 'pg_temp%' and
                                                schema_name not like 'information_schema'", conn))
                {
                    using (UniDataReader dr = sqlCommand.ExecuteReader())
                    {
                        while (dr.Read())
                            databaseNames.Add(dr.GetString(0));
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return databaseNames.ToArray();
        }

    }
}