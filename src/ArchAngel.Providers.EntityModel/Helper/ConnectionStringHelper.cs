using System;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.Helper
{
	[Serializable]
	[DotfuscatorDoNotRename]
	public class ConnectionStringHelper
	{
		public string _ServerName;
		public string DatabaseName;
		public string _UserName;
		private string _Password;
		public bool UseIntegratedSecurity;
		public string FileName;
		public bool UseFileName;
		private int _Port = int.MinValue;
		public string ServiceName;// Oracle: Sid
		public DatabaseTypes CurrentDbType;
		public bool SqlExpressDbIsAlreadyAttached = false;
		public bool UseDirectConnection = false; // For Oracle
		[NonSerialized]
		private Devart.Data.Universal.UniConnection _UniDirectConnection;
		public bool Direct = true;

		public ConnectionStringHelper()
		{
		}

		public string UserName
		{
			get { return _UserName; }
			set { _UserName = value; }
		}

		public string Password
		{
			get { return _Password; }
			set { _Password = value; }
		}

		public string ServerName
		{
			get { return _ServerName; }
			set { _ServerName = value; }
		}

		public ConnectionStringHelper(string connectionString, DatabaseTypes databaseType)
		{
			CurrentDbType = databaseType;

			FillFromConnectionString(connectionString, databaseType);
		}

		private void FillFromConnectionString(string connectionString, DatabaseTypes databaseType)
		{
			var values = connectionString.Trim(' ', '"', ';').Split(';');

			foreach (var nameValuePair in values)
			{
				string nameValuePairValue = nameValuePair.Trim();

				if (string.IsNullOrEmpty(nameValuePairValue))
					continue;

				var data = nameValuePairValue.Split('=');
				string name = data[0], value = data[1];

				switch (name.ToLower())
				{
					case "server":
						ServerName = value;
						break;
					case "data source":
						if (databaseType == DatabaseTypes.SQLCE)
						{
							FileName = value;
							UseFileName = true;

							if (string.IsNullOrEmpty(value) || value.LastIndexOf(".") < 0)
								DatabaseName = "";
							else
								DatabaseName = value.Substring(0, value.LastIndexOf(".") - 1);
						}
						else if (databaseType == DatabaseTypes.SQLServer2005)
						{
							UseFileName = false;
							ServerName = value;
						}
						else if (databaseType == DatabaseTypes.MySQL)
						{
							UseFileName = false;
							ServerName = value;
						}
						else if (databaseType == DatabaseTypes.Oracle)
						{
							UseFileName = false;
							ServerName = value;
						}
						else if (databaseType == DatabaseTypes.SQLite)
						{
							FileName = value;
						}
						break;
					case "initial catalog":
						DatabaseName = value;
						break;
					case "database":
						DatabaseName = value;
						break;
					case "integrated security":
						UseIntegratedSecurity = value.Equals("SSPI", StringComparison.OrdinalIgnoreCase);
						break;
					case "trusted_connection":
						UseIntegratedSecurity = ParseBoolean(value);
						break;
					case "user id":
						UserName = value;
						break;
					case "password":
						Password = value;
						break;
					case "user instance":
						UseFileName = ParseBoolean(value);
						break;
				}
			}
		}

		private static bool ParseBoolean(string text)
		{
			switch (text.ToLower())
			{
				case "true":
				case "t":
				case "yes":
				case "y":
					return true;
				case "false":
				case "f":
				case "no":
				case "n":
					return false;
				default:
					throw new NotImplementedException("Boolean variant not handled yet: " + text);
			}
		}

		public ConnectionStringHelper(string serverName, string databaseName, string userName, string password, bool useIntegratedSecurity, bool useFilename, string filename, DatabaseTypes currentDbType, int port)
		{
			ServerName = serverName;
			DatabaseName = databaseName;
			UserName = userName;
			Password = password;
			UseIntegratedSecurity = useIntegratedSecurity;
			UseFileName = useFilename;
			FileName = filename;
			CurrentDbType = currentDbType;
			Port = port;
		}

		public int Port
		{
			get
			{
				if (_Port == int.MinValue)
					_Port = GetDefaultPort(CurrentDbType);

				return _Port;
			}
			set
			{
				if (value == int.MinValue)
					_Port = GetDefaultPort(CurrentDbType);
				else
					_Port = value;
			}
		}

		///// <summary>
		///// Gets whether this ConnectionStringHelper is the 'same' as the comparison object in terms of settings.
		///// </summary>
		///// <param name="comparisonConnStringHelper">Comparison object.</param>
		///// <returns></returns>
		//internal bool IsTheSame(ConnectionStringHelper comparisonConnStringHelper)
		//{
		//    return (CurrentDbType == comparisonConnStringHelper.CurrentDbType &&
		//        DatabaseName == comparisonConnStringHelper.DatabaseName &&
		//        FileName == comparisonConnStringHelper.FileName &&
		//        ServerName == comparisonConnStringHelper.ServerName &&
		//        UseFileName == comparisonConnStringHelper.UseFileName);
		//}

		public Devart.Data.Universal.UniConnection UniDirectConnection
		{
			get
			{
				if (_UniDirectConnection == null ||
					_UniDirectConnection.DataSource != ServerName ||
					_UniDirectConnection.Database != DatabaseName ||
					_UniDirectConnection.UserId != UserName ||
					_UniDirectConnection.Password != Password ||
					_UniDirectConnection.Port != Port)
				{
					_UniDirectConnection = new Devart.Data.Universal.UniConnection();

					switch (CurrentDbType)
					{
						//case DatabaseTypes.SQLServer2000:
						case DatabaseTypes.SQLServer2005:
							_UniDirectConnection.Provider = "SQL Server";
							break;
						case DatabaseTypes.MySQL:
							_UniDirectConnection.Provider = "MySQL";
							break;
						default:
							throw new NotImplementedException("Database type not handled yet: " + CurrentDbType.ToString());
					}
					_UniDirectConnection.DataSource = ServerName;
					_UniDirectConnection.Database = DatabaseName;
					_UniDirectConnection.UserId = UserName;
					_UniDirectConnection.Password = Password;
					_UniDirectConnection.Port = Port;
				}
				return _UniDirectConnection;
			}
		}

		public System.Data.DataTable GetSchema(string collectionName)
		{
			try
			{
				if (UniDirectConnection.State != System.Data.ConnectionState.Open)
				{
					UniDirectConnection.Open();
				}
				return UniDirectConnection.GetSchema(collectionName);
			}
			finally
			{
				UniDirectConnection.Close();
			}
		}

		//public string GetConnectionStringOleDb()
		//{
		//    return GetConnectionStringOleDb(CurrentDbType);
		//}

		//public string GetConnectionStringOleDb(DatabaseTypes databaseType)
		//{
		//    switch (databaseType)
		//    {
		//        case DatabaseTypes.SQLServer2000:
		//            string portString1 = Port == 1433 ? "" : string.Format(",{0}", Port);
		//            if (UseIntegratedSecurity) { return string.Format("Provider=sqloledb;Data Source={0}{2};Initial Catalog={1};Integrated Security=SSPI;", ServerName, DatabaseName, portString1); }
		//            return string.Format("Provider=sqloledb;Data Source={0}{4};Initial Catalog={1};User Id={2};Password={3};", ServerName, DatabaseName, UserName, Password, portString1);
		//        case DatabaseTypes.SQLServer2005:
		//            string portString2 = Port == 1433 ? "" : string.Format(",{0}", Port);
		//            if (UseIntegratedSecurity) { return string.Format("Provider=SQLNCLI;Server={0}{2};Database={1};Trusted_Connection=yes;", ServerName, DatabaseName, portString2); }
		//            return string.Format("Provider=SQLNCLI;Server={0}{4};Database={1};UID={2};PWD={3};", ServerName, DatabaseName, UserName, Password, portString2);
		//        case DatabaseTypes.SQLServerExpress:
		//            if (FileName.Length == 0)
		//            {
		//                throw new MissingFieldException("SQLServerExpress connectionstring requires FileName to be set.");
		//            }
		//            // Make sure the SQLEXPRESS Windows Service is running on the local machine
		//            EnsureSqlExpressServiceIsRunning();
		//            return string.Format(@"Provider=SQLNCLI;Server=.\SQLExpress;AttachDbFilename='{2}';Database={1};Trusted_Connection=Yes;", ServerName, DatabaseName.Replace("_Data", ""), FileName);
		//        case DatabaseTypes.SQLCE:
		//            return string.Format(@"Data Source={0};", FileName);
		//        default:
		//            throw new NotImplementedException("Not coded yet.");
		//    }
		//}

		internal static bool EnsureSqlExpressServiceIsRunning()
		{
			System.ServiceProcess.ServiceController cc = new System.ServiceProcess.ServiceController("MSSQL$SQLEXPRESS");
			bool result = true;

			if (cc == null)
			{
				throw new Exception("SQLExpress Windows Service not found. Please install SQL Server Express.");
			}
			try
			{
				if (cc.Status != System.ServiceProcess.ServiceControllerStatus.Running)
				{
					if (MessageBox.Show("SQLExpress is not running. Start?", "SQL Express", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						cc.Start();
						cc.Refresh();
						if (cc.Status != System.ServiceProcess.ServiceControllerStatus.Running)
						{
							MessageBox.Show(@"There was a problem starting the SQLExpress Windows Service. Please start it using 'Control Panel\Administration Tools\Services' .", "SQL Express", MessageBoxButtons.OK, MessageBoxIcon.Error);
							result = false;
						}
					}
					else
					{
						result = false;
					}
				}
				return result;
			}
			catch
			{
				//throw new Exception("SQLExpress Windows Service not found. Please install SQL Server Express.");
				return false;
			}
		}

		public string GetConnectionStringSqlClient()
		{
			return GetConnectionStringSqlClient(CurrentDbType);
		}

		public static int GetDefaultPort(DatabaseTypes databaseType)
		{
			switch (databaseType)
			{
				case DatabaseTypes.MySQL:
					return 3306;
				case DatabaseTypes.Oracle:
					return 1521;
				//case DatabaseTypes.SQLServer2000:
				case DatabaseTypes.SQLServer2005:
				case DatabaseTypes.SQLServerExpress:
					return 1433;
				case DatabaseTypes.PostgreSQL:
					return 5432;
				case DatabaseTypes.Firebird:
					return 3050;
				case DatabaseTypes.SQLCE:
					return 0;
				case DatabaseTypes.SQLite:
					return 0;
				default:
					throw new NotImplementedException("Database type not handled yet: " + databaseType.ToString());
			}
		}

		public string GetDefaultServiceName(DatabaseTypes databaseType)
		{
			return "";

			//switch (databaseType)
			//{
			//    case DatabaseTypes.MySQL:
			//        return "";
			//    case DatabaseTypes.Oracle:
			//        return 1521;
			//    case DatabaseTypes.SQLServer2000:
			//    case DatabaseTypes.SQLServer2005:
			//        return 1433;
			//    //case DatabaseTypes.PostgreSQL:
			//    //    return 5432;
			//}
		}

		internal string GetConnectionStringSqlClient(DatabaseTypes databaseType)
		{
			string portString;
			string serviceNameString;

			switch (databaseType)
			{
				//case DatabaseTypes.SQLServer2000:
				case DatabaseTypes.SQLServer2005:
					portString = Port == GetDefaultPort(databaseType) ? "" : string.Format(",{0}", Port);
					if (UseIntegratedSecurity) { return string.Format("Server={0}{2};Database={1};Trusted_Connection=True", ServerName, DatabaseName, portString); }
					return string.Format("Server={0}{4};Database={1};User ID={2};Password={3};Trusted_Connection=False", ServerName, DatabaseName, UserName, Password, portString);

				case DatabaseTypes.SQLServerExpress:
					EnsureSqlExpressServiceIsRunning();

					if (UseFileName)
					{
						if (!SqlExpressDbIsAlreadyAttached)
						{
							return string.Format(@"Server=.\SQLExpress;AttachDbFilename='{2}';Database='{1}';Trusted_Connection=Yes;User Instance=True", ServerName, DatabaseName.Replace("_Data", ""), FileName);
						}
						// Don't include the filename
						return string.Format(@"Server=.\SQLExpress;Database='{1}';Trusted_Connection=Yes;User Instance=True", ServerName, DatabaseName.Replace("_Data", ""), FileName);
					}
					if (UseIntegratedSecurity)
					{
						return string.Format(@"Data Source=.\SQLEXPRESS;Initial Catalog={1};Integrated Security=True;Pooling=False", ServerName, DatabaseName.Replace("_Data", ""), FileName);
					}
					return string.Format(@"Server=.\SQLExpress;Database='{1}';User ID={3};Password={4};Trusted_Connection=False;", ServerName, DatabaseName.Replace("_Data", ""), FileName, UserName, Password);

				case DatabaseTypes.MySQL:
					return string.Format("Provider=MySQL;direct=true;host={0};port={1};user={2};password={3};database={4}", ServerName, Port, UserName, Password, DatabaseName);

				case DatabaseTypes.Oracle:
					if (Direct)
					{
						serviceNameString = ServiceName == GetDefaultServiceName(databaseType) ? "" : string.Format("sid={0};", ServiceName);
						return string.Format("Provider=Oracle;Direct=true;data source={0};port={1};{2}user={3};password={4};", ServerName, Port, serviceNameString, UserName, Password);
					}
					else
						return string.Format("Provider=Oracle;data source={0};user={1};password={2};", ServerName, UserName, Password);

				case DatabaseTypes.PostgreSQL:
					//return string.Format("Provider=PostgreSQL;host={0};port={1};user={2};password={3};initial schema={4};database={5};", ServerName, Port, UserName, Password, SchemaName, DatabaseName);
					return string.Format("Provider=PostgreSQL;host={0};port={1};user={2};password={3};database={4};", ServerName, Port, UserName, Password, DatabaseName);

				case DatabaseTypes.Firebird:
					return string.Format("Server={0};Port={1};User={2};Password={3};Database={4};", ServerName, Port, UserName, Password, FileName);

				case DatabaseTypes.SQLite:
					return string.Format("'Data Source=\"{0}\";Version=3;New=True'", FileName);

				default:
					throw new NotImplementedException("Not coded yet: " + databaseType.ToString());
			}
		}

		public string GetNHConnectionStringSqlClient()
		{
			string portString;
			string serviceNameString;

			switch (CurrentDbType)
			{
				//case DatabaseTypes.SQLServer2000:
				case DatabaseTypes.SQLServer2005:
					portString = Port == GetDefaultPort(CurrentDbType) ? "" : string.Format(",{0}", Port);
					if (UseIntegratedSecurity) { return string.Format("Server={0}{2};Database={1};Trusted_Connection=True", ServerName, DatabaseName, portString); }
					return string.Format("Server={0}{4};Database={1};User ID={2};Password={3};Trusted_Connection=False", ServerName, DatabaseName, UserName, Password, portString);

				case DatabaseTypes.SQLServerExpress:
					EnsureSqlExpressServiceIsRunning();

					if (UseFileName)
					{
						if (!SqlExpressDbIsAlreadyAttached)
						{
							return string.Format(@"Server={0};AttachDbFilename='{2}';Database='{1}';Trusted_Connection=Yes;User Instance=True", ServerName, DatabaseName.Replace("_Data", ""), FileName);
						}
						// Don't include the filename
						return string.Format(@"Server={0};Database='{1}';Trusted_Connection=Yes;User Instance=True", ServerName, DatabaseName.Replace("_Data", ""), FileName);
					}
					if (UseIntegratedSecurity)
					{
						return string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security=SSPI;Pooling=False", ServerName, DatabaseName.Replace("_Data", ""), FileName);
					}
					return string.Format(@"Server={0};Database='{1}';User ID={3};Password={4};Trusted_Connection=False;", ServerName, DatabaseName.Replace("_Data", ""), FileName, UserName, Password);

				case DatabaseTypes.SQLCE:
					return string.Format("Data Source={0};", System.IO.Path.GetFileName(FileName));
				case DatabaseTypes.MySQL:
					portString = Port == GetDefaultPort(CurrentDbType) ? "" : string.Format("Port={0};", Port);
					return string.Format("Database={1};Data Source={0};{4}User Id={2};Password={3};", ServerName, DatabaseName, UserName, Password, portString);

				case DatabaseTypes.Oracle:
					portString = Port == GetDefaultPort(CurrentDbType) ? "" : string.Format("Port={0};", Port);
					return string.Format("Data Source={0};{4}User ID={2};Password={3};", ServerName, DatabaseName, UserName, Password, portString);

				case DatabaseTypes.PostgreSQL:
					portString = Port == GetDefaultPort(CurrentDbType) ? "" : string.Format("Port={0};", Port);
					//return string.Format("Server={0};initial catalog={1};User ID={2};Password={3};Database={4};", ServerName, SchemaName, UserName, Password, DatabaseName);
					return string.Format("Server={0};User ID={1};Password={2};Database={3};", ServerName, UserName, Password, DatabaseName);

				case DatabaseTypes.Firebird:
					portString = Port == GetDefaultPort(CurrentDbType) ? "" : string.Format("Port={0};", Port);
					return string.Format("Server={0};Database={1};User={2};Password={3};{4}", ServerName, FileName, UserName, Password, portString);

				case DatabaseTypes.SQLite:
					return string.Format("'Data Source=\"{0}\";Version=3;New=True'", FileName);

				default:
					throw new NotImplementedException("Not coded yet: " + CurrentDbType.ToString());
			}
		}

	}
}
