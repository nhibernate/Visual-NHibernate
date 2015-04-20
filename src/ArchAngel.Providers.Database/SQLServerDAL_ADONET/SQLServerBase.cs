using System;
using System.Collections.Generic;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.SQLServerDAL_ADONET
{
    public class SQLServerBase
    {
        public sealed class ConstName
        {
            private ConstName()
            {
            }
            public static string Tables
            {
                get { return "Tables"; }
            }
            public static string Views
            {
                get { return "Views"; }
            }
            public static string StoredProcedures
            {
                get { return "StoredProcedures"; }
            }
            public static string Columns
            {
                get { return "Columns"; }
            }
            public static string IndexColumns
            {
                get { return "IndexColumns"; }
            }
            public static string Indexes
            {
                get { return "Indexes"; }
            }
            public static string ForeignKeys
            {
                get { return "ForeignKeys"; }
            }

            public static string Base_Table
            {
                get { return "Base Table"; }
            }
            public static string Table_Name
            {
                get { return "Table_Name"; }
            }
            public static string Column_Name
            {
                get { return "Column_Name"; }
            }
            public static string Ordinal_Position
            {
                get { return "ORDINAL_POSITION"; }
            }
            public static string Is_Nullable
            {
                get { return "IS_NULLABLE"; }
            }
            public static string Data_Type
            {
                get { return "DATA_TYPE"; }
            }
            public static string Character_Maximum_Length
            {
                get { return "CHARACTER_MAXIMUM_LENGTH"; }
            }


        }

        private string _connectionString;

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public SQLServerBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public System.Data.DataTable RunQueryDataTable(string sql)
        {
            throw new NotImplementedException("Not coded yet: SQLServerDAL_ADONET.RunQueryDataTable()");
        }
    }
}
