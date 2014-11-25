using System;
using System.Data.OleDb;

namespace ArchAngel.Providers.Database.SQLServerDAL_2005
{
    public class SQLServerBase
    {
        private readonly string[] _unsupportedDataTypes = new string[] { "" };

        private static BLL.ConnectionStringHelper _connectionString;
        private readonly string _serverName;
        private readonly string _databaseName;
        private readonly string _userName;
        private readonly string _password;
        private string _collation;
        private static OleDbConnection _oleDbConnection;
        private static System.Data.SqlClient.SqlConnection _sqlConnection;

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
                	const string sql = "select cast( DATABASEPROPERTYEX ( 'master', N'Collation' ) as varchar(128) )";
                	//using (OleDbCommand oleDbCommand = new OleDbCommand(sql, OleDbConnection))
                    //{
                    //using (OleDbDataReader oleDbDataReader = RunQuerySQL(sql))
                    using (System.Data.SqlClient.SqlDataReader sqlDataReader = RunQuerySQL(sql))
                    {
                        //if (!oleDbDataReader.HasRows)
                        if (!sqlDataReader.HasRows)
                        {
                            throw new Exception("The collation type cannot be read from the database. Probably a permissions issue.");
                        }

                        //oleDbDataReader.Read();
                        //_collation = (string)oleDbDataReader[0];
                        sqlDataReader.Read();
                        _collation = (string)sqlDataReader[0];

                    }
                    //}
                }

            	return _collation;
            }
        }

        public static void ResetConnection()
        {
            if (_oleDbConnection != null && _oleDbConnection.State != System.Data.ConnectionState.Closed)
            {
                System.Data.OleDb.OleDbConnection.ReleaseObjectPool();
                //System.Data.SqlClient.SqlConnection.ClearAllPools
                _oleDbConnection.Close();
            }
            _oleDbConnection = null;

            if (_sqlConnection != null && _sqlConnection.State != System.Data.ConnectionState.Closed)
            {
                System.Data.SqlClient.SqlConnection.ClearAllPools();
                _sqlConnection.Close();
            }
            _sqlConnection = null;
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

        public static System.Data.SqlClient.SqlConnection SqlConnection
        {
            get
            {
                if (_sqlConnection == null)
                {
                    _sqlConnection = new System.Data.SqlClient.SqlConnection(ConnectionString.GetConnectionStringSqlClient());
                }

                if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    _sqlConnection.Open();
                }

                return _sqlConnection;
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

        protected static OleDbDataReader RunQueryOleDB(string sql)
        {
            OleDbCommand oleDbCommand = new OleDbCommand(sql, OleDbConnection);

            return oleDbCommand.ExecuteReader();
        }

        internal static System.Data.SqlClient.SqlDataReader RunQuerySQL(string sql)
        {
            using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(sql, SqlConnection))
            {
                return sqlCommand.ExecuteReader();
            }
        }

        public System.Data.DataTable RunQueryDataTable(string sql)
        {
            using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(sql, SqlConnection))
            {
                System.Data.DataTable dataTable = new System.Data.DataTable();
                System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable); // Just pass the DataTable into the SqlDataAdapters Fill Method
                return dataTable;
            }
        }

        protected bool IsSupported(Model.Column column)
        {
            foreach (string unsupportedDataType in _unsupportedDataTypes)
            {
                if (column.DataType == unsupportedDataType)
                {
                    return false;
                }
            }

            return true;
        }
    }
}