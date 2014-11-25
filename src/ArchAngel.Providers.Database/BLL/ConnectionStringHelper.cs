using System;
using System.Windows.Forms;

namespace ArchAngel.Providers.Database.BLL
{
    [Serializable]
    [DotfuscatorDoNotRename]
    public class ConnectionStringHelper
    {
        public string ServerName;
        public string DatabaseName;
        public string UserName;
        public string Password;
        public bool UseIntegratedSecurity;
        public string FileName;
        public bool UseFileName;
        public int Port;
        public DatabaseTypes CurrentDbType;
        public bool SqlExpressDbIsAlreadyAttached = false;
        [NonSerialized]
        private CoreLab.UniDirect.UniConnection _UniDirectConnection;

        public ConnectionStringHelper()
        {
        }

        public ConnectionStringHelper(string serverName, string databaseName, string userName, string password, bool useIntegratedSecurity, bool useFilename, string filename, DatabaseTypes currentDbType)
        {
            ServerName = serverName;
            DatabaseName = databaseName;
            UserName = userName;
            Password = password;
            UseIntegratedSecurity = useIntegratedSecurity;
            UseFileName = useFilename;
            FileName = filename;
            CurrentDbType = currentDbType;
        }


		/// <summary>
		/// Gets whether this ConnectionStringHelper is the 'same' as the comparison object in terms of settings.
		/// </summary>
		/// <param name="comparisonConnStringHelper">Comparison object.</param>
		/// <returns></returns>
		internal bool IsTheSame(ConnectionStringHelper comparisonConnStringHelper)
		{
			return (CurrentDbType == comparisonConnStringHelper.CurrentDbType &&
				DatabaseName == comparisonConnStringHelper.DatabaseName &&
				FileName == comparisonConnStringHelper.FileName &&
				ServerName == comparisonConnStringHelper.ServerName &&
				UseFileName == comparisonConnStringHelper.UseFileName);
		}

        public CoreLab.UniDirect.UniConnection UniDirectConnection
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
                    _UniDirectConnection = new CoreLab.UniDirect.UniConnection();
                    
                    switch (CurrentDbType)
                    {
                        case DatabaseTypes.SQLServer2000:
                        case DatabaseTypes.SQLServer2005:
                            _UniDirectConnection.Provider = "SQL Server";
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

        public string GetConnectionStringOleDb()
        {
            return GetConnectionStringOleDb(CurrentDbType);
        }

        public string GetConnectionStringOleDb(DatabaseTypes databaseType)
        {
            switch (databaseType)
            {
                case DatabaseTypes.SQLServer2000:
                    if (UseIntegratedSecurity) { return string.Format("Provider=sqloledb;Data Source={0};Initial Catalog={1};Integrated Security=SSPI;", ServerName, DatabaseName); }
            		return string.Format("Provider=sqloledb;Data Source={0};Initial Catalog={1};User Id={2};Password={3};", ServerName, DatabaseName, UserName, Password);
            	case DatabaseTypes.SQLServer2005:
                    if (UseIntegratedSecurity) { return string.Format("Provider=SQLNCLI;Server={0};Database={1};Trusted_Connection=yes;", ServerName, DatabaseName); }
            		return string.Format("Provider=SQLNCLI;Server={0};Database={1};UID={2};PWD={3};", ServerName, DatabaseName, UserName, Password);
            	case DatabaseTypes.SQLServerExpress:
                    if (FileName.Length == 0)
                    {
                        throw new MissingFieldException("SQLServerExpress connectionstring requires FileName to be set.");
                    }
                    // Make sure the SQLEXPRESS Windows Service is running on the local machine
                    EnsureSqlExpressServiceIsRunning();
                    return string.Format(@"Provider=SQLNCLI;Server=.\SQLExpress;AttachDbFilename='{2}';Database={1};Trusted_Connection=Yes;", ServerName, DatabaseName.Replace("_Data", ""), FileName);
                default:
                    throw new NotImplementedException("Not coded yet.");
            }
        }

        internal static bool EnsureSqlExpressServiceIsRunning()
        {
            System.ServiceProcess.ServiceController cc = new System.ServiceProcess.ServiceController("MSSQL$SQLEXPRESS");
            bool result = true;

            if (cc == null)
            {
                throw new Exception("SQLExpress Windows Service not found. Please install SQL Server Express.");
            }
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

        public string GetConnectionStringSqlClient()
        {
            return GetConnectionStringSqlClient(CurrentDbType);
        }

        public string GetConnectionStringSqlClient(DatabaseTypes databaseType)
        {
            switch (databaseType)
            {
                case DatabaseTypes.SQLServer2000:
                case DatabaseTypes.SQLServer2005:
                    if (UseIntegratedSecurity) { return string.Format("Server={0};Database={1};Trusted_Connection=True", ServerName, DatabaseName); }
            		return string.Format("Server={0};Database={1};User ID={2};Password={3};Trusted_Connection=False", ServerName, DatabaseName, UserName, Password);
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
            		return string.Format(@"Server=.\SQLExpress;;Database='{1}';User ID={3};Password={4};Trusted_Connection=False;", ServerName, DatabaseName.Replace("_Data", ""), FileName, UserName, Password);
            	default:
                    throw new NotImplementedException("Not coded yet.");
            }
        }


    }
}
