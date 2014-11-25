using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ArchAngel.Providers.EntityModel.Helper;
using Devart.Data.Universal;
using Microsoft.Win32;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public static class OracleHelper
	{
		public enum OracleVersion
		{
			Oracle9,
			Oracle10,
			Oracle0
		}

		/// <summary>
		/// Gets all local and network instances of Oracle.
		/// </summary>
		/// <returns></returns>
		public static HashSet<string> GetOracleInstances()
		{
			return TnsNamesReader.LoadTNSNames();
		}

		public static string[] GetOracleDatabases(ConnectionStringHelper helper)
		{
			string serverName = helper.ServerName;
			string userName = helper.UserName;
			string password = helper.Password;
			bool trustedConnection = helper.UseIntegratedSecurity;
			int port = helper.Port;

			UniConnection conn;

			//if (trustedConnection)
			//    conn = new UniConnection(string.Format("Provider=Oracle;host=server;user=root;password=root;database=myDB", serverName));
			//else
			//conn = new UniConnection(string.Format("Provider=Oracle;host={0};user={1};password={2};database=myDB", serverName, userName, password));
			conn = new UniConnection(string.Format("Provider=Oracle;Direct=true;data source={0};user={1};password={2};port={3};", serverName, userName, password, port));
			List<string> databaseNames = new List<string>();

			try
			{
				conn.Open();

				using (UniCommand sqlCommand = new UniCommand("SELECT DISTINCT OWNER FROM all_tables ORDER BY OWNER", conn))
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



		public class TnsNamesReader
		{
			/// <summary> 
			/// Get TNS Name Entries from TNSNames.ora file 
			/// </summary> 
			/// <returns></returns> 
			public static HashSet<string> LoadTNSNames()
			{
				HashSet<string> DBNamesCollection = new HashSet<string>();
				//string regPattern = @"[\n][\s]*[^\(][a-zA-Z0-9_.]+[\s]*";
				string regPattern = @"^[\s]*[^\(][a-zA-Z0-9_.]+[\s]*";
				HashSet<string> tnsNamesOraFilePaths = GetPathToTNSNameFiles();

				foreach (string tnsFile in tnsNamesOraFilePaths)
				{
					if (!tnsFile.Equals(""))
					{
						// Verify file exists 
						FileInfo tnsNamesOraFile = new FileInfo(tnsFile);

						if (tnsNamesOraFile.Exists)
						{
							if (tnsNamesOraFile.Length > 0)
							{
								//read tnsnames.ora file                        
								string tnsNamesContents = File.ReadAllText(tnsNamesOraFile.FullName);
								int numMatches = Regex.Matches(tnsNamesContents, regPattern).Count;
								MatchCollection col = Regex.Matches(tnsNamesContents, regPattern, RegexOptions.Multiline);

								foreach (Match match in col)
								{
									string dbName = match.ToString().Trim();

									if (!DBNamesCollection.Contains(dbName))
										DBNamesCollection.Add(dbName);
								}
							}
						}
					}
				}
				return DBNamesCollection;
			}

			/// <summary> 
			/// Gets TNSNames file path from system path 
			/// </summary> 
			/// <returns>TNSNames.ora file path</returns> 
			private static HashSet<string> GetPathToTNSNameFiles()
			{
				HashSet<string> results = new HashSet<string>();

				#region Try the PATH environment variable first
				string systemPath = Environment.GetEnvironmentVariable("Path");
				Regex reg = new Regex("[a-zA-Z]:\\\\[a-zA-Z0-9\\\\]*(oracle|app)[a-zA-Z0-9_.\\\\]*(?=bin)");
				MatchCollection col = reg.Matches(systemPath);
				string subpath = "network\\ADMIN\\tnsnames.ora";

				foreach (Match match in col)
				{
					string path = match.ToString() + subpath;

					if (File.Exists(path))
						results.Add(path.Trim().ToLower());
				}
				#endregion

				#region Try the registry
				RegistryKey rgkLM = Registry.LocalMachine;
				OracleVersion ov = GetOracleVersion();

				switch (ov)
				{
					case OracleVersion.Oracle10:
						foreach (var key64 in new string[] { "", @"\Wow6432Node" })
						{
							var k = rgkLM.OpenSubKey(string.Format(@"SOFTWARE{0}\ORACLE", key64));

							if (k != null)
								foreach (string okey in k.GetSubKeyNames())
								{
									if (okey.StartsWith("KEY_"))
									{
										string dir = rgkLM.OpenSubKey(string.Format(@"SOFTWARE{0}\ORACLE\{1}", key64, okey)).GetValue("ORACLE_HOME") as string;

										if (File.Exists(Path.Combine(dir, @"NETWORK\ADMIN\TNSNAMES.ORA")))
											results.Add(Path.Combine(dir, @"NETWORK\ADMIN\TNSNAMES.ORA").Trim().ToLower());
										else if (File.Exists(Path.Combine(dir, @"NET80\ADMIN\TNSNAMES.ORA")))
											results.Add(Path.Combine(dir, @"NET80\ADMIN\TNSNAMES.ORA").Trim().ToLower());
									}
								}
						}
						break;
					case OracleVersion.Oracle9:
						foreach (var key64 in new string[] { "", @"\Wow6432Node" })
						{
							string strLastHome = "";
							RegistryKey rgkAllHome = rgkLM.OpenSubKey(string.Format(@"SOFTWARE{0}\ORACLE\ALL_HOMES", key64));

							if (rgkAllHome != null)
							{
								object objLastHome = rgkAllHome.GetValue("LAST_HOME");
								strLastHome = objLastHome.ToString();
								RegistryKey rgkActualHome = Registry.LocalMachine.OpenSubKey(string.Format(@"SOFTWARE{0}\ORACLE\HOME{1}", key64, strLastHome));
								string strOraHome = "";
								object objOraHome = rgkActualHome.GetValue("ORACLE_HOME");
								string strOracleHome = strOraHome = objOraHome.ToString();

								if (File.Exists(Path.Combine(strOracleHome, @"NETWORK\ADMIN\TNSNAMES.ORA")))
									results.Add(Path.Combine(strOracleHome, @"NETWORK\ADMIN\TNSNAMES.ORA").Trim().ToLower());
								if (File.Exists(Path.Combine(strOracleHome, @"NET80\ADMIN\TNSNAMES.ORA")))
									results.Add(Path.Combine(strOracleHome, @"NET80\ADMIN\TNSNAMES.ORA").Trim().ToLower());
							}
						}
						break;
				}
				#endregion

				return results;
			}

			private static OracleVersion GetOracleVersion()
			{
				RegistryKey rgkLM = Registry.LocalMachine;
				/* 
				 * 10g Installationen don't have an ALL_HOMES key
				 * Try to find HOME at SOFTWARE\ORACLE\
				 * 10g homes start with KEY_
				 */
				RegistryKey rgkOracle = rgkLM.OpenSubKey(@"SOFTWARE\ORACLE");

				if (rgkOracle == null)
					rgkOracle = rgkLM.OpenSubKey(@"SOFTWARE\Wow6432Node\ORACLE");

				if (rgkOracle != null)
					foreach (string okey in rgkOracle.GetSubKeyNames())
						if (okey.StartsWith("KEY_"))
							return OracleVersion.Oracle10;

				RegistryKey rgkAllHome = rgkLM.OpenSubKey(@"SOFTWARE\ORACLE\ALL_HOMES");

				if (rgkAllHome == null)
					rgkAllHome = rgkLM.OpenSubKey(@"SOFTWARE\Wow6432Node\ORACLE\ALL_HOMES");

				if (rgkAllHome != null)
				{
					string strLastHome = "";
					object objLastHome = rgkAllHome.GetValue("LAST_HOME");
					strLastHome = objLastHome.ToString();
					RegistryKey rgkActualHome = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ORACLE\HOME" + strLastHome);

					if (rgkActualHome == null)
						rgkActualHome = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\ORACLE\HOME" + strLastHome);

					if (rgkActualHome != null)
					{
						//string strOraHome = "";
						//object objOraHome = rgkActualHome.GetValue("ORACLE_HOME");
						//string strOracleHome = strOraHome = objOraHome.ToString();
						return OracleVersion.Oracle9;
					}
				}
				return OracleVersion.Oracle0;
			}
		}

	}
}