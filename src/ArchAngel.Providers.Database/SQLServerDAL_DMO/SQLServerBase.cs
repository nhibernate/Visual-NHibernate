using System;
using System.Collections.Generic;
using SQLDMO;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.SQLServerDAL_DMO
{
    public class SQLServerBase
    {
        private BLL.ConnectionStringHelper _sqlServerString;
        private string _serverName;
        private string _databaseName;
        private string _login;
        private string _password;

        private SQLDMO.SQLServer _sqlServer = new SQLServerClass();
        private SQLDMO.Database _database;

        public BLL.ConnectionStringHelper ConnectionString
        {
            get { return _sqlServerString; }
        }

        public string ServerName
        {
            get { return _serverName; }
        }

        public string DatabaseName
        {
            get { return _databaseName; }
        }

        public string Login
        {
            get { return _login; }
        }

        public string Password
        {
            get { return _password; }
        }

        public SQLDMO.Database Database
        {
            get { return _database; }
        }

        public SQLDMO.SQLServer SQLServer
        {
            get { return _sqlServer; }
        }

        public SQLServerBase(BLL.ConnectionStringHelper connectionString)
        {
            _sqlServerString = connectionString;
            _serverName = connectionString.ServerName;
            _databaseName = connectionString.DatabaseName;
            _login = connectionString.UserName;
            _password = connectionString.Password;

            _sqlServer.Connect(_serverName, _login, _password);

            if (_databaseName != "")
            {
                _database = (SQLDMO.Database)_sqlServer.Databases.Item(_databaseName, _sqlServer);
            }
        }

        ~SQLServerBase()
        {
            try
            {
                _sqlServer.DisConnect();
            }
            catch
            { }
        }
    }
}
