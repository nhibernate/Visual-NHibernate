using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ArchAngel.Providers.EntityModel.Helper;
using Microsoft.Win32;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public static class SqlServer2005Helper
	{
		/// <summary>
		/// Gets all local and network instances of SQL Server, excluding SQL Express.
		/// </summary>
		/// <returns></returns>
		public static List<string> GetSqlServer2005Instances()
		{
			List<string> serverNames = new List<string>();

			#region Instances On Local Machine

			string keyName;

			if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Microsoft SQL Server") != null)
			{
				// we have a 64 bit version of windows
				keyName = @"SOFTWARE\Wow6432Node\Microsoft\Microsoft SQL Server";
			}
			else
			{
				// we have a 32 bit version of windows
				keyName = @"SOFTWARE\Microsoft\Microsoft SQL Server";
			}
			RegistryKey sqlServerRegistryKeys = Registry.LocalMachine.OpenSubKey(keyName);

			if (sqlServerRegistryKeys != null)
			{
				object installedInstanceRegValue = sqlServerRegistryKeys.GetValue("InstalledInstances");

				if (installedInstanceRegValue != null)
				{
					string[] instanceNames = (string[])installedInstanceRegValue;

					if (instanceNames.GetUpperBound(0) > 0)
					{
						foreach (string element in instanceNames)
						{
							if (element == "MSSQLSERVER")
							{
								if (serverNames.BinarySearch(Environment.MachineName) < 0)
								{
									serverNames.Add(Environment.MachineName);
									serverNames.Sort();
								}
							}
							else if (!element.Contains("SQLEXPRESS"))
							{
								// This is where \SQLEXPRESS is added if it exists on the machine,
								// but we don't want it when the user is searching for SQL Servers.
								string sqlExpressServerName = Environment.MachineName + @"\" + element;

								if (serverNames.BinarySearch(sqlExpressServerName) < 0)
								{
									serverNames.Add(sqlExpressServerName);
									serverNames.Sort();
								}
							}
						}
					}
				}
			}

			#endregion

			#region Network Servers

			DataTable dataSources = System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources();

			foreach (DataRow row in dataSources.Rows)
			{
				string serverName = row["ServerName"].ToString();

				if (serverNames.BinarySearch(serverName) < 0)
				{
					serverNames.Add(serverName);
					serverNames.Sort();
				}
			}

			#endregion

			return serverNames;
		}

		public static string[] GetSQlServer2005Databases(ConnectionStringHelper helper)
		{
			string serverName = helper.ServerName;
			string userName = helper.UserName;
			string password = helper.Password;
			bool trustedConnection = helper.UseIntegratedSecurity;

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

				while (rdr != null && rdr.Read())
				{
					databaseNames.Add(rdr.GetString(0));
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