using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_2000
{
    public class SQLServerBase
    {
        private readonly string[] _unsupportedDataTypes = new string[] { "" };

        private static BLL.ConnectionStringHelper _connectionString;
        private string _serverName;
        private string _databaseName;
        private string _userName;
        private string _password;
        private string _collation = null;

        private static OleDbConnection _oleDbConnection = null;

        public static BLL.ConnectionStringHelper ConnectionString
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

        /// <summary>
        /// Gets the collation of the 'master' database in a SQL server
        /// </summary>
        protected string Collation
        {
            get
            {
                if (_collation == null)
                {
                    string sql = "select cast( DATABASEPROPERTYEX ( 'master', N'Collation' ) as varchar(128) )";
                    using (OleDbCommand oleDbCommand = new OleDbCommand(sql, OleDbConnection))
                    {
                        using (OleDbDataReader oleDbDataReader = RunQuery(sql))
                        {
                            if (!oleDbDataReader.HasRows)
                            {
                                throw new Exception("The collation type cannot be read from the database. Probably a permissions issue.");
                            }

                            oleDbDataReader.Read();
                            _collation = (string)oleDbDataReader[0];
                        }
                    }
                }

                return _collation;
            }
        }

        public static OleDbConnection OleDbConnection
        {
            get
            {
                if (_oleDbConnection == null)
                {
                    _oleDbConnection = new OleDbConnection(ConnectionString.GetConnectionStringOleDb());
                }

                if (_oleDbConnection.State == System.Data.ConnectionState.Closed)
                {
                    _oleDbConnection.Open();
                }

                return _oleDbConnection;
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
                //_oleDbConnection.DisConnect();
            }
            catch
            {
            }
        }

        protected OleDbDataReader RunQuery(string sql)
        {
            OleDbCommand oleDbCommand = new OleDbCommand(sql, OleDbConnection);

            return oleDbCommand.ExecuteReader();
        }

        protected static System.Data.DataTable RunQueryDataTable(string sql)
        {
            using (OleDbCommand oleDbCommand = new OleDbCommand(sql, OleDbConnection))
            {
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand);
                System.Data.DataTable dataTable = new System.Data.DataTable();
                oleDbDataAdapter.Fill(dataTable); // Just pass the DataTable into the SqlDataAdapters Fill Method

                return dataTable;
            }
        }

        protected bool IsSupported(Model.Column column)
        {
            foreach (string unsupportedDataType in _unsupportedDataTypes)
            {
                if (column.DataType == unsupportedDataType)
                {
                    string message = "Column " + column.Name + " of " + column.Parent.GetType().Name + " " + column.Parent.Name + " is of Type " + column.DataType + " and is not supported";
                    //throw new Exception(message);
                    return false;
                }
            }

            return true;
        }
    }
}