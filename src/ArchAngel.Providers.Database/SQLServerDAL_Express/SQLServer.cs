using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
//using Microsoft.SqlServer.Management.Smo;
using Microsoft.Win32;

namespace ArchAngel.Providers.Database.SQLServerDAL_Express
{
    public sealed class SQLServer
    {
        private SQLServer()
        {
        }

        public static string[] GetSqlServers()
        {
            List<string> arrServers = new List<string>();

            string strKey;

            if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Microsoft SQL Server") != null)
            {
                // we have a 64 bit version of windows
                strKey = @"SOFTWARE\Wow6432Node\Microsoft\Microsoft SQL Server";
            }
            else
            {
                // we have a 32 bit version of windows
                strKey = @"SOFTWARE\Microsoft\Microsoft SQL Server";
            }

            RegistryKey rkSqlServers = Registry.LocalMachine.OpenSubKey(strKey);

            if (rkSqlServers != null)
            {
                string[] instances = (string[])rkSqlServers.GetValue("InstalledInstances");

                if (instances.GetUpperBound(0) > 0)
                {
                    foreach (string element in instances)
                    {
                        if (element == "SQLEXPRESS")
                        {
                            arrServers.Add(Environment.MachineName + @"\" + element);
                        }
                    }
                }
            }

            return (string[])arrServers.ToArray();
        }
    }
}