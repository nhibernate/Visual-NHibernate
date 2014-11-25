using System;
using System.Reflection;
using System.Windows.Forms;

namespace ArchAngel.Providers.Database.Model
{
    [Serializable]
    [DotfuscatorDoNotRename]
    public class ConnectionStringHelper
    {
        private string _serverName;
        private string _databaseName;
        private string _userName;
        private string _password;
        private bool _useIntegratedSecurity;
        private string _fileName;
        private bool _useFileName;
        private DatabaseTypes _currentDbType;
        private bool _sqlExpressDbIsAlreadyAttached = false;

        public string ServerName
        {
            get { return _serverName; }
            set 
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _serverName, value); 
                _serverName = value; 
            }
        }

        public string DatabaseName
        {
            get { return _databaseName; }
            set 
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _databaseName, value); 
                _databaseName = value;
            }
        }

        public string UserName
        {
            get { return _userName; }
            set 
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _userName, value); 
                _userName = value; 
            }
        }

        public string Password
        {
            get { return _password; }
            set 
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _password, value); 
                _password = value;
            }
        }

        public bool UseIntegratedSecurity
        {
            get { return _useIntegratedSecurity; }
            set 
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _useIntegratedSecurity, value); 
                _useIntegratedSecurity = value;
            }
        }

        public string FileName
        {
            get { return _fileName; }
            set 
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _fileName, value); 
                _fileName = value;
            }
        }

        public bool UseFileName
        {
            get { return _useFileName; }
            set 
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _useFileName, value); 
                _useFileName = value;
            }
        }

        public DatabaseTypes CurrentDbType
        {
            get { return _currentDbType; }
            set 
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _currentDbType, value); 
                _currentDbType = value;
            }
        }

        public bool SqlExpressDbIsAlreadyAttached
        {
            get { return _sqlExpressDbIsAlreadyAttached; }
            set 
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _sqlExpressDbIsAlreadyAttached, value); 
                _sqlExpressDbIsAlreadyAttached = value;
            }
        }

        public ConnectionStringHelper()
        {
        }

        public ConnectionStringHelper(string serverName, string databaseName, string userName, string password, bool useIntegratedSecurity, bool useFilename, string filename, DatabaseTypes currentDbType)
        {
            _serverName = serverName;
            _databaseName = databaseName;
            _userName = userName;
            _password = password;
            _useIntegratedSecurity = useIntegratedSecurity;
            _useFileName = useFilename;
            _fileName = filename;
            _currentDbType = currentDbType;
        }

        public string GetConnectionStringOleDb()
        {
            return GetConnectionStringOleDb(_currentDbType);
        }

        public string GetConnectionStringOleDb(DatabaseTypes databaseType)
        {
            switch (databaseType)
            {
                case DatabaseTypes.SQLServer2000:
                    if (UseIntegratedSecurity)
                    {
                        return string.Format("Provider=sqloledb;Data Source={0};Initial Catalog={1};Integrated Security=SSPI;", _serverName, _databaseName);
                    }
            		return string.Format("Provider=sqloledb;Data Source={0};Initial Catalog={1};User Id={2};Password={3};", _serverName, _databaseName, _userName, _password);

            	case DatabaseTypes.SQLServer2005:
                    if (UseIntegratedSecurity)
                    {
                        return string.Format("Provider=SQLNCLI;Server={0};Database={1};Trusted_Connection=yes;", _serverName, _databaseName);
                    }
            		return string.Format("Provider=SQLNCLI;Server={0};Database={1};UID={2};PWD={3};", _serverName, _databaseName, _userName, _password);

            	case DatabaseTypes.SQLServerExpress:
                    if (FileName.Length == 0)
                    {
                        throw new MissingFieldException("SQLServerExpress connectionstring requires FileName to be set.");
                    }

                    // Make sure the SQLEXPRESS Windows Service is running on the local machine
                    EnsureSqlExpressServiceIsRunning();
                    return string.Format(@"Provider=SQLNCLI;Server=.\SQLExpress;AttachDbFilename='{2}';Database={1};Trusted_Connection=Yes;", _serverName, _databaseName.Replace("_Data", ""), FileName);

                default:
                    throw new NotImplementedException("Not coded yet.");
            }
        }

        internal static bool EnsureSqlExpressServiceIsRunning()
        {
            System.ServiceProcess.ServiceController serviceController = new System.ServiceProcess.ServiceController("MSSQL$SQLEXPRESS");
            bool result = true;

            if (serviceController == null)
            {
                throw new Exception("SQLExpress Windows Service not found. Please install SQL Server Express.");
            }

            if (serviceController.Status != System.ServiceProcess.ServiceControllerStatus.Running)
            {
                if (MessageBox.Show("SQLExpress is not running. Start?", "SQL Express", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    serviceController.Start();

                    if (serviceController.Status != System.ServiceProcess.ServiceControllerStatus.Running)
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
            return GetConnectionStringSqlClient(_currentDbType);
        }

        public string GetConnectionStringSqlClient(DatabaseTypes databaseType)
        {
            switch (databaseType)
            {
                case DatabaseTypes.SQLServer2000:
                case DatabaseTypes.SQLServer2005:
                    if (UseIntegratedSecurity)
                    {
                        return string.Format("Server={0};Database={1};Trusted_Connection=True", _serverName, _databaseName);
                    }
            		return string.Format("Server={0};Database={1};User ID={2};_password={3};Trusted_Connection=False", _serverName, _databaseName, _userName, _password);

            	case DatabaseTypes.SQLServerExpress:
                    if (!SqlExpressDbIsAlreadyAttached)
                    {
                    	if (UseFileName)
                        {
                            return string.Format(@"Server=.\SQLExpress;AttachDbFilename='{2}';Database='{1}';Trusted_Connection=Yes;User Instance=True", _serverName, _databaseName.Replace("_Data", ""), FileName);
                        }
                    	if (UseIntegratedSecurity)
                    	{
                    		return string.Format(@"Server=.\SQLExpress;;Database='{1}';Trusted_Connection=Yes;User Instance=True", _serverName, _databaseName.Replace("_Data", ""), FileName);
                    	}
                    	return string.Format(@"Server=.\SQLExpress;;Database='{1}';User ID={3};Password={4};", _serverName, _databaseName.Replace("_Data", ""), FileName, _userName, _password);
                    }
            		// Don't include the filename
            		return string.Format(@"Server=.\SQLExpress;Database='{1}';Trusted_Connection=Yes;User Instance=True", _serverName, _databaseName.Replace("_Data", ""), FileName);

            	default:
                    throw new NotImplementedException("Not coded yet.");
            }
        }
    }
}