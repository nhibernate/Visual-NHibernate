using System;
using System.Collections.Generic;
using System.Data;
using Devart.Data.Universal;
using ArchAngel.Providers.EntityModel.Helper;
using Microsoft.Win32;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
    public static class FirebirdHelper
    {
        /// <summary>
        /// Gets all local and network instances of Firebird.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetFirebirdInstances()
        {
            List<string> serverNames = new List<string>();
            return serverNames;

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
            //                else if (!element.Contains("SQLEXPRESS"))
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

        public static string[] GetFirebirdDatabases(ConnectionStringHelper helper)
        {
            string serverName = helper.ServerName;
            string userName = helper.UserName;
            string password = helper.Password;
            int port = helper.Port;
            bool trustedConnection = helper.UseIntegratedSecurity;

            UniConnection conn = new UniConnection(string.Format("Provider=Firebird;direct=true;Server={0};User={1};Password={2};Port={3};", serverName, userName, password, port));

            List<string> databaseNames = new List<string>();

            try
            {
                conn.Open();

                DataTable table1 = conn.GetSchema("Databases");

                foreach (DataRow row in table1.Rows)
                {
                    string dbName = row[0].ToString();
                    databaseNames.Add(dbName);
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