using System;
using System.Collections.Generic;

namespace ArchAngel.Providers.Database.Model
{
    public class DbDataType
    {
        public enum UniversalTypes
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
            Represented,
            TinyInt,
            Time,
            TimeStamp,
            VarChar,
            Xml,
            UNDEFINED
        }
        public enum SqlServerDataTypes
        {
            BIGINT,
            BINARY,
            BIT,
            IMAGE,
            SMALLINT,
            CHAR,
            TEXT,
            DOUBLE,
            DATETIME,
            DECIMAL,
            REAL,
            UNIQUEIDENTIFIER,
            INT,
            NCHAR,
            NTEXT,
            NVARCHAR,
            FLOAT,
            TINYINT,
            VARCHAR,
            UNDEFINED
        }

        public enum OracleDataTypes
        {
            VARRAY,
            NUMBER,
            RAW,
            NUMBER_P,
            LOB,
            CHAR,
            CLOB,
            REF_CURSOR,
            DATE,
            NUMBER_P_S,
            VARCHAR2,
            INTERVAL_DAY_TO_SECOND,
            INTERVAL_YEAR_TO_MONTH,
            NCHAR,
            NCLOB,
            NVARCHAR,
            BINARY_FLOAT,
            TIMESTAMP,
            VARCHAR,
            XMLTYPE,
            UNDEFINED
        }

        public enum MySqlDataTypes
        {
            BIGINT,
            BLOB,
            BIT,
            SMALLINT,
            CHAR,
            TEXT,
            DOUBLE,
            DATE,
            DATETIME,
            DECIMAL,
            VARCHAR,
            INT,
            FLOAT,
            TINYINT,
            TIME,
            TIMESTAMP,
            UNDEFINED
        }

        private static readonly List<UniversalTypes> UniversalTypeArray = new List<UniversalTypes>(new UniversalTypes[] {            
            UniversalTypes.Array,
            UniversalTypes.BigInt,
            UniversalTypes.Binary,
            UniversalTypes.Bit,
            UniversalTypes.Blob,
            UniversalTypes.Boolean,
            UniversalTypes.Byte,
            UniversalTypes.Char,
            UniversalTypes.Clob,
            UniversalTypes.Currency,
            UniversalTypes.Cursor,
            UniversalTypes.Date,
            UniversalTypes.DateTime,
            UniversalTypes.Decimal,
            UniversalTypes.Double,
            UniversalTypes.Guid,
            UniversalTypes.Int,
            UniversalTypes.IntervalDS,
            UniversalTypes.IntervalYM,
            UniversalTypes.NChar,
            UniversalTypes.NClob,
            UniversalTypes.NVarChar,
            UniversalTypes.Object,
            UniversalTypes.Single,
            UniversalTypes.SmallInt,
            UniversalTypes.TinyInt,
            UniversalTypes.Time,
            UniversalTypes.TimeStamp,
            UniversalTypes.VarChar,
            UniversalTypes.Xml
        });

        private static readonly List<Type> DotNetTypeArray = new List<Type>(new Type[] {            
            typeof(object), // TODO: this is wrong. It is actually undefined //UniversalTypes.Array
            typeof(Int64), // UniversalTypes.BigInt
            typeof(byte[]), // UniversalTypes.Binary
            typeof(Int16), // UniversalTypes.Bit
            typeof(byte[]), // UniversalTypes.Blob
            typeof(bool), // UniversalTypes.Boolean
            typeof(Int16), // UniversalTypes.Byte
            typeof(string), // UniversalTypes.Char
            typeof(string), //UniversalTypes.Clob
            typeof(decimal), // UniversalTypes.Currency
            typeof(object), // TODO: this is wrong. It is actually undefined // UniversalTypes.Cursor
            typeof(DateTime), // UniversalTypes.Date
            typeof(DateTime), // UniversalTypes.DateTime
            typeof(decimal), // UniversalTypes.Decimal
            typeof(double), // UniversalTypes.Double
            typeof(Guid), // UniversalTypes.Guid
            typeof(Int32), // UniversalTypes.Int
            typeof(TimeSpan), // UniversalTypes.IntervalDS
            typeof(TimeSpan), // UniversalTypes.IntervalYM
            typeof(string), // UniversalTypes.NChar
            typeof(string), // UniversalTypes.NClob
            typeof(string), // UniversalTypes.NVarChar
            typeof(object), // UniversalTypes.Object
            typeof(double), // UniversalTypes.Single
            typeof(Int16), // UniversalTypes.SmallInt
            typeof(Int16), // UniversalTypes.TinyInt
            typeof(DateTime), // UniversalTypes.Time
            typeof(DateTime), // UniversalTypes.TimeStamp
            typeof(string), // UniversalTypes.VarChar
            typeof(object), // UniversalTypes.Xml
        });

        private static readonly List<SqlServerDataTypes> SqlServerTypeArray = new List<SqlServerDataTypes>(new SqlServerDataTypes[] {            
            SqlServerDataTypes.UNDEFINED, // UniversalTypes.Array
            SqlServerDataTypes.BIGINT, // UniversalTypes.BigInt
            SqlServerDataTypes.BINARY, // UniversalTypes.Binary
            SqlServerDataTypes.BIT, // UniversalTypes.Bit
            SqlServerDataTypes.IMAGE, // UniversalTypes.Blob
            SqlServerDataTypes.BIT, // UniversalTypes.Boolean
            SqlServerDataTypes.SMALLINT, // UniversalTypes.Byte
            SqlServerDataTypes.CHAR, // UniversalTypes.Char
            SqlServerDataTypes.TEXT, // UniversalTypes.Clob
            SqlServerDataTypes.DOUBLE, // UniversalTypes.Currency
            SqlServerDataTypes.UNDEFINED, // UniversalTypes.Cursor
            SqlServerDataTypes.DATETIME, // UniversalTypes.Date
            SqlServerDataTypes.DATETIME, // UniversalTypes.DateTime
            SqlServerDataTypes.DECIMAL, // UniversalTypes.Decimal
            SqlServerDataTypes.REAL, // UniversalTypes.Double
            SqlServerDataTypes.UNIQUEIDENTIFIER, // UniversalTypes.Guid
            SqlServerDataTypes.INT, // UniversalTypes.Int
            SqlServerDataTypes.UNDEFINED, // UniversalTypes.IntervalDS
            SqlServerDataTypes.UNDEFINED, // UniversalTypes.IntervalYM
            SqlServerDataTypes.NCHAR, // UniversalTypes.NChar
            SqlServerDataTypes.NTEXT, // UniversalTypes.NClob
            SqlServerDataTypes.NVARCHAR, // UniversalTypes.NVarChar
            SqlServerDataTypes.UNDEFINED, // UniversalTypes.Object
            SqlServerDataTypes.FLOAT, // UniversalTypes.Single
            SqlServerDataTypes.SMALLINT, // UniversalTypes.SmallInt
            SqlServerDataTypes.TINYINT, // UniversalTypes.TinyInt
            SqlServerDataTypes.DATETIME, // UniversalTypes.Time
            SqlServerDataTypes.DATETIME, // UniversalTypes.TimeStamp
            SqlServerDataTypes.VARCHAR, // UniversalTypes.VarChar
            SqlServerDataTypes.UNDEFINED // UniversalTypes.Xml
        });

        private static readonly List<OracleDataTypes> OracleTypeArray = new List<OracleDataTypes>(new OracleDataTypes[] {            
            OracleDataTypes.VARRAY, // UniversalTypes.Array
            OracleDataTypes.NUMBER, // UniversalTypes.BigInt
            OracleDataTypes.RAW, // UniversalTypes.Binary
            OracleDataTypes.NUMBER_P, // UniversalTypes.Bit
            OracleDataTypes.LOB, // UniversalTypes.Blob
            OracleDataTypes.NUMBER, // UniversalTypes.Boolean
            OracleDataTypes.NUMBER_P, // UniversalTypes.Byte
            OracleDataTypes.CHAR, // UniversalTypes.Char
            OracleDataTypes.CLOB, // UniversalTypes.Clob
            OracleDataTypes.NUMBER, // UniversalTypes.Currency
            OracleDataTypes.REF_CURSOR, // UniversalTypes.Cursor
            OracleDataTypes.DATE, // UniversalTypes.Date
            OracleDataTypes.DATE, // UniversalTypes.DateTime
            OracleDataTypes.NUMBER, // UniversalTypes.Decimal
            OracleDataTypes.NUMBER_P_S, // UniversalTypes.Double
            OracleDataTypes.VARCHAR2, // UniversalTypes.Guid
            OracleDataTypes.NUMBER_P, // UniversalTypes.Int
            OracleDataTypes.INTERVAL_DAY_TO_SECOND, // UniversalTypes.IntervalDS
            OracleDataTypes.INTERVAL_YEAR_TO_MONTH, // UniversalTypes.IntervalYM
            OracleDataTypes.NCHAR, // UniversalTypes.NChar
            OracleDataTypes.NCLOB, // UniversalTypes.NClob
            OracleDataTypes.NVARCHAR, // UniversalTypes.NVarChar
            OracleDataTypes.UNDEFINED, // UniversalTypes.Object
            OracleDataTypes.BINARY_FLOAT, // UniversalTypes.Single
            OracleDataTypes.NUMBER_P, // UniversalTypes.SmallInt
            OracleDataTypes.NUMBER_P, // UniversalTypes.TinyInt
            OracleDataTypes.DATE, // UniversalTypes.Time
            OracleDataTypes.TIMESTAMP, // UniversalTypes.TimeStamp
            OracleDataTypes.VARCHAR, // UniversalTypes.VarChar
            OracleDataTypes.XMLTYPE // UniversalTypes.Xml
        });

        private static readonly List<MySqlDataTypes> MySqlTypeArray = new List<MySqlDataTypes>(new MySqlDataTypes[] {            
                        MySqlDataTypes.UNDEFINED, // UniversalTypes.Array,
            MySqlDataTypes.BIGINT, // UniversalTypes.BigInt,
            MySqlDataTypes.BLOB, // UniversalTypes.Binary,
            MySqlDataTypes.BIT, // UniversalTypes.Bit,
            MySqlDataTypes.BLOB, // UniversalTypes.Blob,
            MySqlDataTypes.BIT, // UniversalTypes.Boolean,
            MySqlDataTypes.SMALLINT, // UniversalTypes.Byte,
            MySqlDataTypes.CHAR, // UniversalTypes.Char,
            MySqlDataTypes.TEXT, // UniversalTypes.Clob,
            MySqlDataTypes.DOUBLE, // UniversalTypes.Currency,
            MySqlDataTypes.UNDEFINED, // UniversalTypes.Cursor,
            MySqlDataTypes.DATE, // UniversalTypes.Date,
            MySqlDataTypes.DATETIME, // UniversalTypes.DateTime,
            MySqlDataTypes.DECIMAL, // UniversalTypes.Decimal,
            MySqlDataTypes.DOUBLE, // UniversalTypes.Double,
            MySqlDataTypes.VARCHAR, // UniversalTypes.Guid,
            MySqlDataTypes.INT, // UniversalTypes.Int,
            MySqlDataTypes.UNDEFINED, // UniversalTypes.IntervalDS,
            MySqlDataTypes.UNDEFINED, // UniversalTypes.IntervalYM,
            MySqlDataTypes.CHAR, // UniversalTypes.NChar,
            MySqlDataTypes.TEXT, // UniversalTypes.NClob,
            MySqlDataTypes.VARCHAR, // UniversalTypes.NVarChar,
            MySqlDataTypes.UNDEFINED, //UniversalTypes.Object,
            MySqlDataTypes.FLOAT, // UniversalTypes.Single,
            MySqlDataTypes.SMALLINT, // UniversalTypes.SmallInt,
            MySqlDataTypes.TINYINT, // UniversalTypes.TinyInt,
            MySqlDataTypes.TIME, // UniversalTypes.Time,
            MySqlDataTypes.TIMESTAMP, // UniversalTypes.TimeStamp,
            MySqlDataTypes.VARCHAR, // UniversalTypes.VarChar,
            MySqlDataTypes.VARCHAR // UniversalTypes.Xml
        });

        public static Type GetDotNetType(UniversalTypes databaseType)
        {
            return DotNetTypeArray[UniversalTypeArray.IndexOf(databaseType)];
        }

        public static SqlServerDataTypes GetSqlServerType(UniversalTypes databaseType)
        {
            return SqlServerTypeArray[UniversalTypeArray.IndexOf(databaseType)];
        }

        public static OracleDataTypes GetOracleType(UniversalTypes databaseType)
        {
            return OracleTypeArray[UniversalTypeArray.IndexOf(databaseType)];
        }

        public static MySqlDataTypes GetMySqlType(UniversalTypes databaseType)
        {
            return MySqlTypeArray[UniversalTypeArray.IndexOf(databaseType)];
        }
    }
}
