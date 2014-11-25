using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.SQLServerDAL_SMO
{
    public class SQLServerBase
    {
        private BLL.ConnectionStringHelper _connectionString;
        private string _serverName;
        private string _databaseName;
        private string _userName;
        private string _password;

        private Microsoft.SqlServer.Management.Smo.Server _server = null;
        private Microsoft.SqlServer.Management.Smo.Database _database = null;

        public BLL.ConnectionStringHelper ConnectionString
        {
            get { return _connectionString; }
        }

        public string ServerName
        {
            get { return _serverName; }
        }

        public string DatabaseName
        {
            get { return _databaseName; }
        }

        public string UserName
        {
            get { return _userName; }
        }

        public string Password
        {
            get { return _password; }
        }

        public Microsoft.SqlServer.Management.Smo.Server Server
        {
            get
            {
                if (_server == null)
                {
                    _server = new Server(new ServerConnection(_serverName, _userName, _password));
                }

                return _server;
            }
        }

        public Microsoft.SqlServer.Management.Smo.Database Database
        {
            get
            {
                if (_database == null && _databaseName != "")
                {
                    _database = (Microsoft.SqlServer.Management.Smo.Database)Server.Databases[_databaseName];
                }

                return _database;
            }
        }

        public SQLServerBase(BLL.ConnectionStringHelper connectionString)
        {
            _connectionString = connectionString;
            _serverName = connectionString.ServerName;
            _databaseName = connectionString.DatabaseName;
            _userName = connectionString.UserName;
            _password = connectionString.Password;
        }

        ~SQLServerBase()
        {
            try
            {
                //_server.DisConnect();
            }
            catch
            {
            }
        }
    }
}