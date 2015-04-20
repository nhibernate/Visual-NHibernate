using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using System.Data;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.Helper
{
    public sealed class SQLServer
    {
        private static readonly string[] _reservedWords;
        private static readonly string[] _dataTypes;

        public static string[] ReservedWords
        {
            get { return _reservedWords; }
        }

        public static string[] DataTypes
        {
            get { return _dataTypes; }
        }

        static SQLServer()
        {
            // SQL Reserved Words
            Assembly assembly = Assembly.GetAssembly(typeof(SQLServer));
        	StreamReader reader = new StreamReader(assembly.GetManifestResourceStream("ArchAngel.Providers.Database.Helper.ReservedWordSQL.txt"));

            string completeFile = reader.ReadToEnd();
            completeFile = completeFile.Replace("\r\n", "\n");

            _reservedWords = completeFile.Split('\n');
            reader.Close();

            // SQL Data Types
            reader = new StreamReader(assembly.GetManifestResourceStream("ArchAngel.Providers.Database.Helper.DataTypeSQL.txt"));

            completeFile = reader.ReadToEnd();
            completeFile = completeFile.Replace("\r\n", "\n");

            _dataTypes = completeFile.Split('\n');
            reader.Close();
        }

        /// <summary>
        /// Gets all local and network instances of SQL Server, excluding SQL Express.
        /// </summary>
        /// <returns></returns>
        public static string[] GetSqlServers()
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
                            else if (element != "SQLEXPRESS")
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
                    // row["ServerName"]
                    // row["InstanceName"]
                    // row["IsClustered"]
                    // row["Version"]
                }
            }

            #endregion

            return serverNames.ToArray();
        }

        public static string GetSQLParentAlias(MapColumn mapColumn)
        {
            // TODO: Add in one to one support
            string parentName = mapColumn.Parent.Name;

            List<string> foreignTableNames = new List<string>();
            foreignTableNames.Add(parentName);

            Table table = (Table)mapColumn.Parent;
            foreach (MapColumn tempMapColumn in table.MapColumns)
            {
                foreignTableNames.Add(tempMapColumn.ForeignColumn.Parent.Name);

                if (tempMapColumn == mapColumn)
                {
                    break;
                }
            }

            int count = -1;
            foreach (string name in foreignTableNames)
            {
                if (name == mapColumn.ForeignColumn.Parent.Name)
                {
                    count++;
                }
            }

            if (count <= 0)
            {
                return GetSQLName(mapColumn.ForeignColumn.Parent.Name);
            }
        	return GetSQLName(mapColumn.ForeignColumn.Parent.Name + "_" + count);
        }

        public static string GetSQLName(string name)
        {
            if (name.IndexOf(" ") > -1)
            {
                return "[" + name + "]";
            }

            foreach (string reservedWord in _reservedWords)
            {
                if (Slyce.Common.Utility.StringsAreEqual(name, reservedWord, false))
                {
                    return "[" + name + "]";
                }
            }

            return name;
        }

        #region Data Types

        public static string GetSQLDataType(Column column)
        {
            switch (column.DataType)
            {
                case "bigint":
                    return "bigint";

                case "binary":
                    return "binary";

                case "bit":
                    return "bit";

                case "char":
                    return "char(" + column.Size + ")";

                case "datetime":
                    return "datetime";

                case "decimal":
                    return "decimal";

                case "float":
                    return "float";

                case "image":
                    return "image";

                case "int":
                    return "int";

                case "money":
                    return "money";

                case "nchar":
                    return "nchar(" + column.Size + ")";

                case "ntext":
                    return "ntext";

                case "numeric":
                    return "numeric";

                case "nvarchar":
                    if (column.Size == -1)
                    {
                        return "nvarchar(MAX)";
                    }
            		return "nvarchar(" + column.Size + ")";

            	case "real":
                    return "real";

                case "smalldatetime":
                    return "smalldatetime";

                case "smallint":
                    return "smallint";

                case "smallmoney":
                    return "smallmoney";

                case "sql_variant":
                    return "sql_variant";

                case "text":
                    return "text";

                case "timestamp":
                    return "timestamp";

                case "tinyint":
                    return "tinyint";

                case "uniqueidentifier":
                    return "uniqueidentifier";

                case "varbinary":
                    //return "varbinary";
                    if (column.Size == -1)
                    {
                        return "varbinary(MAX)";
                    }
            		return "varbinary(" + column.Size + ")";

            	case "varchar":
                    if (column.Size == -1)
                    {
                        return "varchar(MAX)";
                    }
            		return "varchar(" + column.Size + ")";
            	case "xml":
                    return "xml";

                default:
                    throw new Exception(column.DataType + " data type not supported");
            }
        }

        public static bool IsDataTypeBit(Column column)
        {
            switch (column.DataType)
            {
                case "bit":
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsDataTypeBinary(Column column)
        {
            switch (column.DataType)
            {
                case "binary":
                    return true;

                case "image":
                    return true;

                case "timestamp":
                    return true;

                case "varbinary":
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets whether the column's data-type is textual.
        /// </summary>
        /// <param name="column">Column whose data-type is to be inspected.  Eg: varchar, ntext etc.</param>
        /// <returns>True if the data-type is textual, false otherwise.</returns>
        public static bool IsDataTypeText(Column column)
        {
            return IsDataTypeText(column.DataType);
        }

        /// <summary>
        /// Gets whether the data-type is textual.
        /// </summary>
        /// <param name="sqlDataType">The T-SQL data-type.  Eg: varchar, ntext etc.</param>
        /// <returns>True if the data-type is textual, false otherwise.</returns>
        public static bool IsDataTypeText(string sqlDataType)
        {
            switch (sqlDataType.Trim().ToLower())
            {
                case "char":
                    return true;

                case "nchar":
                    return true;

                case "ntext":
                    return true;

                case "nvarchar":
                    return true;

                case "text":
                    return true;

                case "varchar":
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsDataTypeFixedChar(Column column)
        {
            switch (column.DataType)
            {
                case "char":
                    return true;

                case "nchar":
                    return true;

                case "ntext":
                    return true;

                case "nvarchar":
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsDataTypeNumeric(Column column)
        {
            switch (column.DataType)
            {
                case "bigint":
                    return true;

                case "decimal":
                    return true;

                case "float":
                    return true;

                case "int":
                    return true;

                case "money":
                    return true;

                case "numeric":
                    return true;

                case "real":
                    return true;

                case "smallint":
                    return true;

                case "smallmoney":
                    return true;

                case "tinyint":
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsDataTypeWholeNumber(Column column)
        {
            switch (column.DataType)
            {
                case "bigint":
                    return true;

                case "decimal":
                    return true;

                case "int":
                    return true;

                case "numeric":
                    return true;

                case "smallint":
                    return true;

                case "tinyint":
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsDataTypeDate(Column column)
        {
            switch (column.DataType)
            {
                case "datetime":
                    return true;

                case "smalldatetime":
                    return true;

                default:
                    return false;
            }

        }

        public static bool IsDataTypeChar(Column column)
        {
            switch (column.DataType)
            {
                case "char":
                    return true;

                case "nchar":
                    return true;

                case "nvarchar":
                    return true;

                case "varchar":
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsDataTypeUniqueIdentifier(Column column)
        {
            switch (column.DataType)
            {
                case "uniqueidentifier":
                    return true;

                default:
                    return false;
            }
        }

        #endregion
    }
}