using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
//using Microsoft.SqlServer.Management.Common;
//using Microsoft.SqlServer.Management.Smo;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ConnectionStringHelper=ArchAngel.Providers.Database.BLL.ConnectionStringHelper;

namespace ArchAngel.Providers.Database.SQLServerDAL_Express
{
    public class SQLServerBase
    {
        private static BLL.ConnectionStringHelper _connectionString;
        private string _serverName;
        private string _databaseName;
        private string _userName;
        private string _password;
        private static OleDbConnection _connectionObject;
        private static System.Data.SqlClient.SqlConnection _connectionObjectSqlClient;

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
            get
            {
                if (ConnectionString.CurrentDbType == DatabaseTypes.SQLServerExpress)
                {
                    _databaseName = _databaseName.ToUpper().Replace("_DATA", "");
                }
                return _databaseName;
            }
        }

        public string UserName
        {
            get { return _userName; }
        }

        public string Password
        {
            get { return _password; }
        }

        //public Microsoft.SqlServer.Management.Smo.Database Database
        //{
        //    get { return _database; }
        //}

        //public Microsoft.SqlServer.Management.Smo.Server Server
        //{
        //    get { return _server; }
        //}

        public SQLServerBase(BLL.ConnectionStringHelper connectionString)
        {
            _connectionString = connectionString;
            _serverName = connectionString.ServerName;
            _databaseName = connectionString.DatabaseName;
            _userName = connectionString.UserName;
            _password = connectionString.Password;

            //_server = new Server(new ServerConnection(_serverName, _userName, _password));

            //if (_databaseName != "")
            //{
            //    _database = (Microsoft.SqlServer.Management.Smo.Database)_server.Databases[_databaseName];
            //}
        }

        internal void DetachSqlExpressDatabase()
        {
            if (ConnectionString.CurrentDbType == DatabaseTypes.SQLServerExpress &&
                    _connectionObjectSqlClient != null &&
                    _connectionObjectSqlClient.State == System.Data.ConnectionState.Open)
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("exec sp_detach_db", _connectionObjectSqlClient);
                cmd.ExecuteNonQuery();
                _connectionObjectSqlClient.Close();
            }
        }

        ~SQLServerBase()
        {
            try
            {
                //_server.DisConnect();
                //DetachSqlExpressDatabase();
            }
            catch
            {
                if (_connectionObject != null && _connectionObject.State != System.Data.ConnectionState.Closed)
                {
                    _connectionObject.Close();
                }
            }
        }

        public static void ResetConnection()
        {
            if (_connectionObject != null && _connectionObject.State != System.Data.ConnectionState.Closed)
            {
                _connectionObject.Close();
            }
            _connectionObject = null;
        }

        public static OleDbConnection ConnectionObject
        {
            get
            {
                if (_connectionObject == null)
                {
                    _connectionObject = new OleDbConnection(ConnectionString.GetConnectionStringOleDb());
                }
                if (_connectionObject.State == System.Data.ConnectionState.Closed)
                {
                    _connectionObject.Open();
                }
                return _connectionObject;
            }
        }

        public static System.Data.SqlClient.SqlConnection ConnectionObjectSqlClient
        {
            get
            {
                if (_connectionObjectSqlClient == null)
                {
                    _connectionObjectSqlClient = new System.Data.SqlClient.SqlConnection(ConnectionString.GetConnectionStringSqlClient());
                }
                if (_connectionObjectSqlClient.State == System.Data.ConnectionState.Closed)
                {
                    try
                    {
                        _connectionObjectSqlClient.Open();
                    }
                    catch (Exception ex)
                    {
                        if (ConnectionString.CurrentDbType == DatabaseTypes.SQLServerExpress &&
                            ex.Message.IndexOf("already exists.") > 0 &&
                            ex.Message.IndexOf("Could not attach file") > 0)
                        {
                            ConnectionString.SqlExpressDbIsAlreadyAttached = true;
                            _connectionObjectSqlClient = new System.Data.SqlClient.SqlConnection(ConnectionString.GetConnectionStringSqlClient());
                        }
                        if (ConnectionString.CurrentDbType == DatabaseTypes.SQLServerExpress)
                        {
                            if (ConnectionStringHelper.EnsureSqlExpressServiceIsRunning())
                            {
                                _connectionObjectSqlClient.Open();
                            }
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return _connectionObjectSqlClient;
            }
        }

        protected OleDbDataReader RunQuery(string sql)
        {
            OleDbCommand cmd = new OleDbCommand(sql, ConnectionObject);
            return cmd.ExecuteReader();
        }

        public System.Data.DataTable RunQueryDataTable(string sql)
        {
            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, ConnectionObjectSqlClient))
            {
                System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt); // Just pass the DataTable into the SqlDataAdapters Fill Method
                return dt;
            }

        }

        protected System.Data.SqlClient.SqlDataReader RunQuerySqlClient(string sql)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, ConnectionObjectSqlClient);
            //string[] restrictions = new string[] { };
            //System.Data.DataTable dt = ConnectionObjectSqlClient.GetSchema(OleDbMetaDataCollectionNames.Columns);
            //string ss = "";

            //foreach (System.Data.DataRow row in dt.Rows)
            //{
            //    for (int i = 0; i < row.ItemArray.Length; i++)
            //    {
            //        ss = row[i].ToString();
            //    }
            //}
            return cmd.ExecuteReader();
        }

        protected static System.Data.DataTable RunQueryDataTableSqlClient(string sql)
        {
            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, ConnectionObjectSqlClient))
            {
                System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt); // Just pass the DataTable into the SqlDataAdapters Fill Method
                return dt;
            }

        }
    }
}
