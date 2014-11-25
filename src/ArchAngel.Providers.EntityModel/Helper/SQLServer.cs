using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using log4net;
using Microsoft.Win32;
using Slyce.Common.StringExtensions;

namespace ArchAngel.Providers.EntityModel.Helper
{
	public sealed class SQLServer
	{
		public enum UniDbTypes
		{
			Array,
			BigInt,
			Binary,
			Bit,
			Blob,
			Boolean,
			Byte,
			Char,
			Clob,
			Currency,
			Cursor,
			Date,
			DateTime,
			Decimal,
			Double,
			Guid,
			Int,
			IntervalDS,
			IntervalYM,
			NChar,
			NClob,
			NVarChar,
			Object,
			Single,
			SmallInt,
			TinyInt,
			Time,
			TimeStamp,
			VarChar,
			Xml,


			// Extra SQL Server types
			DateTime2,
			DateTimeOffset,
			Float,
			Geography,
			Geometry,
			HierarchyId,
			Image,
			Numeric,
			SmallDateTime,
			SmallMoney,
			SqlVariant,
			Text,
			VarBinary,

			// Extra MySQL types
			Year,
			Enum,
			Fixed,
			LongBlob,
			LongText,
			MediumBlob,
			MediumInt,
			MediumText,
			Set,
			TinyBlob,
			TinyText,

			// Extra SQL Server Compact (CE) data-types
			Money,
			NText,
			Real,
			Sql_Variant,
			UniqueIdentifier
		}
		private static readonly string[] _reservedWords;
		private static readonly string[] _dataTypes;
		private static readonly ILog log = LogManager.GetLogger(typeof(SQLServer));

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
			//StreamReader reader = new StreamReader(assembly.GetManifestResourceStream("ArchAngel.Providers.EntityModel.Helper.ReservedWordSQL.txt"));

			//string completeFile = reader.ReadToEnd();
			//completeFile = completeFile.Replace("\r\n", "\n");

			//_reservedWords = completeFile.Split('\n');
			//reader.Close();

			// SQL Data Types
			StreamReader reader = new StreamReader(assembly.GetManifestResourceStream("ArchAngel.Providers.EntityModel.Helper.DataTypeSQL.txt"));

			string completeFile = reader.ReadToEnd();
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

		private static readonly List<string> CLRTypes = new List<string> { "bool", "byte", "short", "int", "long", "decimal", "double", "float", "char", "string", "System.Guid", "System.DateTime" };
		public static ReadOnlyCollection<string> CLRTypeList
		{
			get { return CLRTypes.AsReadOnly(); }
		}

		private static readonly Regex ValueRegex = new Regex("(?<Name>.*)(\\((?<Value>.*)\\))", RegexOptions.Compiled);
		public static string ConvertSQLTypeNameToCLRTypeName(string sqlDataType)
		{
			Match match = ValueRegex.Match(sqlDataType);
			string justTypeName = sqlDataType;
			int? value = null;
			bool isMax = false;

			if (match.Success)
			{
				justTypeName = match.Groups["Name"].Value;

				isMax = match.Groups["Value"].Success && match.Groups["Value"].Value == "MAX";
				value = match.Groups["Value"].Success && !isMax ? match.Groups["Value"].Value.As<int>() : null;
			}
			switch (justTypeName.ToLower())
			{
				case "bit":
					//return "System.Boolean";
					return "bool";
				case "tinyint":
					//return "System.Byte";
					return "byte";
				case "smallint":
					//return "System.Int16";
					return "short";
				case "int":
					//return "System.Int32";
					return "int";
				case "bigint":
					//return "System.Int64";
					return "long";
				case "smallmoney":
					//return "System.Decimal";
					return "decimal";
				case "money":
					//return "System.Decimal";
					return "decimal";
				case "decimal":
					if (value.HasValue && value > 29)
						// There is a possible loss of precision here.
						// If we use System.Decimal, the database decimal can hold a bigger number
						// than the CLR Decimal so we go with Double for safety.
						//return "System.Double";
						return "double";
					//return "System.Decimal";
					return "decimal";
				case "numeric":
					if (value.HasValue && value <= 29)
						//return "System.Single";
						return "float";
					if (value.HasValue && value >= 38)
						//return "System.Double";
						return "double";
					//return "System.Double";
					return "double";
				case "real":
					//return "System.Single";
					return "float";
				case "float":
					if (value.HasValue && value <= 24)
						//return "System.Single";
						return "float";
					//return "System.Double";
					return "double";
				case "char":
				case "nchar":
					if (value.HasValue && value > 1 || isMax)
						//return "System.String";
						return "string";
					//return "System.Char";
					return "char";
				case "nvarchar":
				case "varchar":
				case "text":
				case "ntext":
				case "xml":
					//return "System.String";
					return "string";
				case "smalldatetime":
				case "datetime":
				case "datetime2":
				case "date":
				case "time":
					//return "System.DateTime";
					return "DateTime";
				case "datetimeoffset":
					return "System.DateTimeOffset";
				case "binary":
				case "varbinary":
				case "image":
				case "timestamp":
					//return "System.Byte[]";
					return "byte[]";
				case "uniqueidentifier":
					//return "System.Guid";
					return "Guid";
				case "geometry":
					return "GeoAPI.Geometries.IGeometry";
				case "geography":
					return "GeoAPI.Geometries.IGeography";
			}
			log.WarnFormat("Could not match SQL data type {0} to a CLR type", sqlDataType);
			return "System.Object";
		}

		public static UniDbTypes ConvertCLRTypeNameToSQLTypeName(string clrDataType)
		{
			switch (clrDataType)
			{
				case "System.Boolean":
				case "bool":
					return UniDbTypes.Bit;
				case "System.Byte":
				case "byte":
					return UniDbTypes.TinyInt;
				case "System.Int16":
				case "short":
					return UniDbTypes.SmallInt;
				case "System.Int32":
				case "int":
					return UniDbTypes.Int;
				case "System.Int64":
				case "long":
					return UniDbTypes.BigInt;
				case "System.Decimal":
				case "decimal":
					return UniDbTypes.Decimal;
				case "System.Double":
				case "double":
					return UniDbTypes.Double;
				case "System.Single":
				case "float":
					return UniDbTypes.Float;
				case "System.Char":
				case "char":
					return UniDbTypes.NChar;
				case "System.String":
				case "string":
					return UniDbTypes.NClob; /* ntext */
				case "System.DateTime":
					return UniDbTypes.DateTime;
				case "System.DateTimeOffset":
					return UniDbTypes.DateTimeOffset;
				case "System.Byte[]":
				case "byte[]":
					return UniDbTypes.VarBinary;
				case "System.Guid":
					return UniDbTypes.Guid;
				case "GeoAPI.Geometries.IGeometry":
					return UniDbTypes.Geometry;
				case "GeoAPI.Geometries.IGeography":
					return UniDbTypes.Geography;
			}
			log.WarnFormat("Could not match CLR data type {0} to a SQL type", clrDataType);
			return UniDbTypes.VarBinary;
		}

		public static string GetCLRTypeAsString(UniDbTypes uniDbType)
		{
			switch (uniDbType)
			{
				case UniDbTypes.Array:
					//throw new NotImplementedException("No equivalent CLR type");
					return "object";
				case UniDbTypes.BigInt:
					return "long";
				case UniDbTypes.Binary:
					return "byte[]";
				case UniDbTypes.Bit:
					return "bool";
				case UniDbTypes.Blob:
					return "byte[]";
				case UniDbTypes.Boolean:
					return "bool";
				case UniDbTypes.Byte:
					return "byte";
				case UniDbTypes.Char:
					return "string";
				case UniDbTypes.Clob:
					return "string";
				case UniDbTypes.Currency:
					return "decimal";
				case UniDbTypes.Cursor:
					//return typeof(Int64);
					throw new NotImplementedException("No equivalent CLR type");
				case UniDbTypes.Date:
					return "DateTime";
				case UniDbTypes.DateTime:
					return "DateTime";
				case UniDbTypes.DateTime2:
					return "DateTime";
				case UniDbTypes.DateTimeOffset:
					return "DateTime";
				case UniDbTypes.Decimal:
					return "decimal";
				case UniDbTypes.Double:
					return "double";
				case UniDbTypes.Enum:
					return "string";
				case UniDbTypes.Fixed:
					return "decimal";
				case UniDbTypes.Float:
					return "float";
				case UniDbTypes.Geography:
					return "GeoAPI.Geometries.IGeography";
				case UniDbTypes.Geometry:
					return "GeoAPI.Geometries.IGeometry";
				case UniDbTypes.Guid:
					return "Guid";
				case UniDbTypes.HierarchyId:
					return "string";
				case UniDbTypes.Image:
					return "byte[]";
				case UniDbTypes.Int:
					return "int";
				case UniDbTypes.IntervalDS:
					return "TimeSpan";
				case UniDbTypes.IntervalYM:
					return "TimeSpan";
				case UniDbTypes.LongBlob:
					return "byte[]";
				case UniDbTypes.LongText:
					return "string";
				case UniDbTypes.MediumBlob:
					return "byte[]";
				case UniDbTypes.MediumInt:
					return "int";
				case UniDbTypes.MediumText:
					return "string";
				case UniDbTypes.Money:
					return "decimal";
				case UniDbTypes.NChar:
					return "string";
				case UniDbTypes.NClob:
					return "string";
				case UniDbTypes.NText:
					return "string";
				case UniDbTypes.Numeric:
					return "double";
				case UniDbTypes.NVarChar:
					return "string";
				case UniDbTypes.Object:
					return "object";
				case UniDbTypes.Real:
					return "float";
				case UniDbTypes.Set:
					return "string";
				case UniDbTypes.Single:
					return "double";
				case UniDbTypes.SmallDateTime:
					return "DateTime";
				case UniDbTypes.SmallInt:
					return "short";
				case UniDbTypes.SmallMoney:
					return "decimal";
				case UniDbTypes.SqlVariant:
					return "object";
				case UniDbTypes.Text:
					return "string";
				case UniDbTypes.Time:
					return "DateTime";
				case UniDbTypes.TimeStamp:
					return "DateTime";
				case UniDbTypes.TinyBlob:
					return "byte[]";
				case UniDbTypes.TinyInt:
					return "short";
				case UniDbTypes.TinyText:
					return "string";
				case UniDbTypes.UniqueIdentifier:
					return "Guid";
				case UniDbTypes.VarBinary:
					return "byte[]";
				case UniDbTypes.VarChar:
					return "string";
				case UniDbTypes.Xml:
					return "object";
				case UniDbTypes.Year:
					return "short";
				default:
					throw new NotImplementedException("UniDBType not handled yet: " + uniDbType.ToString());
			}
		}

		public static Type GetCLRType(UniDbTypes uniDbType)
		{
			switch (uniDbType)
			{
				case UniDbTypes.Array:
					throw new NotImplementedException("No equivalent CLR type");
				case UniDbTypes.BigInt:
					return typeof(Int64);
				case UniDbTypes.Binary:
					return typeof(byte[]);
				case UniDbTypes.Bit:
					return typeof(Int16);
				case UniDbTypes.Blob:
					return typeof(byte[]);
				case UniDbTypes.Boolean:
					return typeof(bool);
				case UniDbTypes.Byte:
					return typeof(Int16);
				case UniDbTypes.Char:
					return typeof(string);
				case UniDbTypes.Clob:
					return typeof(string);
				case UniDbTypes.Currency:
					return typeof(decimal);
				case UniDbTypes.Cursor:
					//return typeof(Int64);
					throw new NotImplementedException("No equivalent CLR type");
				case UniDbTypes.Date:
					return typeof(DateTime);
				case UniDbTypes.DateTime:
					return typeof(DateTime);
				case UniDbTypes.Decimal:
					return typeof(decimal);
				case UniDbTypes.Double:
					return typeof(double);
				case UniDbTypes.Guid:
					return typeof(Guid);
				case UniDbTypes.Int:
					return typeof(Int32);
				case UniDbTypes.IntervalDS:
					return typeof(TimeSpan);
				case UniDbTypes.IntervalYM:
					return typeof(TimeSpan);
				case UniDbTypes.NChar:
					return typeof(string);
				case UniDbTypes.NClob:
					return typeof(string);
				case UniDbTypes.NVarChar:
					return typeof(string);
				case UniDbTypes.Object:
					return typeof(object);
				case UniDbTypes.Single:
					return typeof(double);
				case UniDbTypes.SmallInt:
					return typeof(Int16);
				case UniDbTypes.Time:
					return typeof(DateTime);
				case UniDbTypes.TimeStamp:
					return typeof(DateTime);
				case UniDbTypes.TinyInt:
					return typeof(Int16);
				case UniDbTypes.VarChar:
					return typeof(string);
				case UniDbTypes.Xml:
					return typeof(object);

				case UniDbTypes.Year:
					return typeof(short);
				case UniDbTypes.Enum:
					return typeof(string);
				case UniDbTypes.Fixed:
					return typeof(decimal);
				case UniDbTypes.LongBlob:
					return typeof(byte[]);
				case UniDbTypes.LongText:
					return typeof(string);
				case UniDbTypes.MediumBlob:
					return typeof(byte[]);
				case UniDbTypes.MediumInt:
					return typeof(int);
				case UniDbTypes.MediumText:
					return typeof(string);
				case UniDbTypes.Set:
					return typeof(string);
				case UniDbTypes.TinyBlob:
					return typeof(byte[]);
				case UniDbTypes.TinyText:
					return typeof(string);
				default:
					throw new NotImplementedException("UniDBType not handled yet: " + uniDbType.ToString());
			}
		}

		public static UniDbTypes GetUniDbTypeFromOracleType(string mySQLType, int precision, int scale)
		{
			// See: http://www.c-sharpcorner.com/UploadFile/ramamohang/OracleDataProviderfor.NET312012005023724AM/OracleDataProviderfor.NET3.aspx
			int parenIndex = mySQLType.IndexOf("(");

			if (parenIndex > 0)
				mySQLType = mySQLType.Substring(0, parenIndex);

			switch (mySQLType.ToUpper())
			{
				case "BFILE":
					return UniDbTypes.Binary;
				case "BINARY_DOUBLE":
					return UniDbTypes.Double;
				case "BINARY_FLOAT":
					return UniDbTypes.Single;
				case "BLOB":
					return UniDbTypes.Binary;
				case "CHAR":
					return UniDbTypes.Char;
				case "CLOB":
					return UniDbTypes.Clob;
				case "DATE":
					return UniDbTypes.Date;
				case "FLOAT":
					return UniDbTypes.Float;
				case "INTERVAL DAY TO SECOND":
					return UniDbTypes.IntervalDS;
				case "INTERVAL YEAR TO MONTH":
					return UniDbTypes.IntervalYM;
				case "LOB":
					return UniDbTypes.Blob;
				case "LONG":
					return UniDbTypes.LongText;
				case "LONG RAW":
					return UniDbTypes.LongBlob;
				case "NCHAR":
					return UniDbTypes.NChar;
				case "NCLOB":
					return UniDbTypes.NClob;
				case "NUMBER":
					if (precision < 10 && scale <= 0)
						return UniDbTypes.Int;
					else
						return UniDbTypes.Double;
				case "NVARCHAR":
					return UniDbTypes.NVarChar;
				case "NVARCHAR2":
					return UniDbTypes.NVarChar;
				case "RAW":
					return UniDbTypes.Binary;
				case "REF CURSOR":
					return UniDbTypes.Cursor;
				case "ROWID":
					return UniDbTypes.Int;
				case "TIMESTAMP":
					return UniDbTypes.TimeStamp;
				case "TIMESTAMP WITH LOCAL TIME ZONE":
					return UniDbTypes.TimeStamp;
				case "TIMESTAMP WITH TIME ZONE":
					return UniDbTypes.TimeStamp;
				case "UROWID":
					return UniDbTypes.VarChar;
				case "VARARRAY":
					return UniDbTypes.Array;
				case "VARCHAR":
					return UniDbTypes.VarChar;
				case "VARCHAR2":
					return UniDbTypes.VarChar;
				case "XMLTYPE":
					return UniDbTypes.Xml;
				default:
					throw new NotImplementedException("Oracle data-type not handled yet. Note that user-defined types should map to 'object'.: " + mySQLType);

			}
		}

		public static UniDbTypes GetUniDbTypeFromMySQLType(string mySQLType)
		{
			switch (mySQLType.ToUpper())
			{
				case "BIGINT":
					return UniDbTypes.BigInt;
				case "BLOB":
					return UniDbTypes.Blob;
				case "BIT":
					return UniDbTypes.Boolean;
				case "CHAR":
					return UniDbTypes.Char;
				case "TEXT":
					return UniDbTypes.Clob;
				case "DOUBLE":
					return UniDbTypes.Double;
				case "DATE":
					return UniDbTypes.Date;
				case "DATETIME":
					return UniDbTypes.DateTime;
				case "DECIMAL":
					return UniDbTypes.Decimal;
				case "INT":
					return UniDbTypes.Int;
				case "VARCHAR":
					return UniDbTypes.VarChar;
				case "FLOAT":
					return UniDbTypes.Single;
				case "SMALLINT":
					return UniDbTypes.SmallInt;
				case "TIME":
					return UniDbTypes.Time;
				case "TIMESTAMP":
					return UniDbTypes.TimeStamp;
				case "TINYINT":
					return UniDbTypes.TinyInt;


				case "YEAR":
					return UniDbTypes.Year;
				case "ENUM":
					return UniDbTypes.Enum;
				case "FIXED":
					return UniDbTypes.Fixed;
				case "LONGBLOB":
					return UniDbTypes.LongBlob;
				case "LONGTEXT":
					return UniDbTypes.LongText;
				case "MEDIUMBLOB":
					return UniDbTypes.MediumBlob;
				case "MEDIUMINT":
					return UniDbTypes.MediumInt;
				case "MEDIUMTEXT":
					return UniDbTypes.MediumText;
				case "SET":
					return UniDbTypes.Set;
				case "TINYBLOB":
					return UniDbTypes.TinyBlob;
				case "TINYTEXT":
					return UniDbTypes.TinyText;
				default:
					throw new NotImplementedException("MySQL data-type not handled yet: " + mySQLType);
			}
		}

		public static UniDbTypes GetUniDbTypeFromFirebirdType(string firebirdType)
		{
			switch (firebirdType.ToUpper())
			{
				case "BLOB":
					return UniDbTypes.Blob;
				case "BLOB_ID":
					return UniDbTypes.Int;
				case "CSTRING":
					return UniDbTypes.VarChar;
				case "DATE":
					return UniDbTypes.Date;
				case "DOUBLE":
					return UniDbTypes.Double;
				case "FLOAT":
					return UniDbTypes.Single;
				case "LONG":
					return UniDbTypes.BigInt;
				case "INT64":
					return UniDbTypes.BigInt;
				case "QUAD":
					return UniDbTypes.BigInt;
				case "SHORT":
					return UniDbTypes.SmallInt;
				case "TEXT":
					return UniDbTypes.VarChar;
				case "TIME":
					return UniDbTypes.Time;
				case "TIMESTAMP":
					return UniDbTypes.TimeStamp;
				case "VARYING":
					return UniDbTypes.VarChar;
				default:
					throw new NotImplementedException("Firebird data-type not handled yet: " + firebirdType);
			}
		}

		public static UniDbTypes GetUniDbTypeFromPostgreSQLype(string postgreSQLType)
		{
			switch (postgreSQLType.ToUpper())
			{
				case "ANYARRAY":
				case "ARRAY":
					return UniDbTypes.Array;
				case "BIGINT":
				case "INT8":
				case "BIGSERIAL":
				case "SERIAL8":
					return UniDbTypes.BigInt;
				case "BYTEA":
					return UniDbTypes.Binary;
				case "BIT":
				case "BIT VARYING":
				case "VARBIT":
					return UniDbTypes.Bit;
				case "BOOLEAN":
				case "BOOL":
					return UniDbTypes.Boolean;
				case "CHAR":
				case "\"CHAR\"":
				case "CHARACTER":
					return UniDbTypes.Char;
				case "TEXT":
					return UniDbTypes.Clob;
				case "CURRENCY":
				case "MONEY":
					return UniDbTypes.Currency;
				case "DATE":
					return UniDbTypes.Date;
				case "NUMERIC":
				case "DECIMAL":
					return UniDbTypes.Decimal;
				case "DOUBLE":
				case "DOUBLE PRECISION":
				case "FLOAT8":
					return UniDbTypes.Double;
				case "INTEGER":
				case "INT":
				case "INT4":
				case "SERIAL":
				case "SERIAL4":
				case "XID":
				case "OID":
				case "REGPROC":
					return UniDbTypes.Int;
				case "INTERVAL":
					return UniDbTypes.IntervalDS;
				case "INET":
				case "CIDR":
				case "MACADDR":
					return UniDbTypes.Object;
				case "REAL":
				case "FLOAT4":
					return UniDbTypes.Single;
				case "SMALLINT":
				case "INT2":
					return UniDbTypes.SmallInt;
				case "TIMESTAMP":
				case "TIMESTAMPTZ":
				case "TIMESTAMP WITH TIME ZONE":
				case "TIMESTAMP WITHOUT TIME ZONE":
					return UniDbTypes.TimeStamp;
				case "TIME":
				case "TIME WITH TIME ZONE":
				case "TIME WITHOUT TIME ZONE":
				case "TIMETZ":
				case "ABSTIME":
					return UniDbTypes.Time;
				case "VARCHAR":
				case "CHARACTER VARYING":
				case "NAME":
					return UniDbTypes.VarChar;
				case "BOX":
				case "CIRCLE":
				case "LINE":
				case "LSEG":
				case "PATH":
				case "POINT":
				case "POLYGON":
					return UniDbTypes.Object;
				default:
					throw new NotImplementedException("MySQL data-type not handled yet: " + postgreSQLType);
			}
		}

		public static UniDbTypes GetUniDbTypeFromSqlServer(string sqlType)
		{
			sqlType = sqlType.Trim().ToUpper();

			switch (sqlType)
			{
				case "BIGINT":
					return UniDbTypes.BigInt;
				case "BINARY":
					return UniDbTypes.Binary;
				case "BIT":
					return UniDbTypes.Bit;
				case "CHAR":
					return UniDbTypes.Char;
				case "DATE":
					return UniDbTypes.Date;
				case "DATETIME":
					return UniDbTypes.DateTime;
				case "DATETIME2":
					return UniDbTypes.DateTime2;
				case "DATETIMEOFFSET":
					return UniDbTypes.DateTimeOffset;
				case "DECIMAL":
					return UniDbTypes.Decimal;
				case "FLOAT":
					return UniDbTypes.Single;
				case "GEOGRAPHY":
					return UniDbTypes.Geography;
				case "GEOMETRY":
					return UniDbTypes.Geometry;
				case "HIERARCHYID":
					return UniDbTypes.HierarchyId;
				case "IMAGE":
					return UniDbTypes.Blob;
				case "INT":
					return UniDbTypes.Int;
				case "MONEY":
					return UniDbTypes.Currency;
				case "NCHAR":
					return UniDbTypes.NChar;
				case "NTEXT":
					return UniDbTypes.NClob;
				case "NUMERIC":
					return UniDbTypes.Numeric;
				case "NVARCHAR":
					return UniDbTypes.NVarChar;
				case "REAL":
					return UniDbTypes.Double;
				case "SMALLDATETIME":
					return UniDbTypes.SmallDateTime;
				case "SMALLINT":
					return UniDbTypes.SmallInt;
				case "SMALLMONEY":
					return UniDbTypes.SmallMoney;
				case "SQL_VARIANT":
					return UniDbTypes.SqlVariant;
				case "TEXT":
					return UniDbTypes.Clob;
				case "TIME":
					return UniDbTypes.Time;
				case "TIMESTAMP":
					return UniDbTypes.TimeStamp;
				case "TINYINT":
					return UniDbTypes.TinyInt;
				case "UNIQUEIDENTIFIER":
					return UniDbTypes.Guid;
				case "VARBINARY":
					return UniDbTypes.VarBinary;
				case "VARCHAR":
					return UniDbTypes.VarChar;
				case "XML":
					return UniDbTypes.Xml;
				default:
					throw new NotImplementedException("SQL Server data-type not handled yet: " + sqlType);
			}
		}

		public static UniDbTypes ConvertCLRTypeNameToUniDbType(Type clrDataType)
		{
			if (clrDataType == typeof(Int64))
				return UniDbTypes.BigInt;
			if (clrDataType == typeof(byte[]))
				return UniDbTypes.Binary;
			if (clrDataType == typeof(Int16))
				return UniDbTypes.Bit;
			if (clrDataType == typeof(bool))
				return UniDbTypes.Boolean;
			if (clrDataType == typeof(Int16))
				return UniDbTypes.SmallInt;
			if (clrDataType == typeof(string))
				return UniDbTypes.NVarChar;
			if (clrDataType == typeof(decimal))
				return UniDbTypes.Decimal;
			if (clrDataType == typeof(DateTime))
				return UniDbTypes.DateTime;
			if (clrDataType == typeof(double))
				return UniDbTypes.Double;
			if (clrDataType == typeof(Guid))
				return UniDbTypes.Guid;
			if (clrDataType == typeof(int))
				return UniDbTypes.Int;
			if (clrDataType == typeof(TimeSpan))
				return UniDbTypes.IntervalDS;
			if (clrDataType == typeof(object))
				return UniDbTypes.Object;

			throw new NotImplementedException("CLR type not handled yet: " + clrDataType.Name);
		}

	}
}