using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_2000
{
    public class StoredProcedure : SQLServerBase, IStoredProcedure
    {
        private static System.Data.DataTable _parameters = null;

        public StoredProcedure(BLL.ConnectionStringHelper connectionString)
            : base(connectionString)
        {
        }

        public string GetStoredProcedureBody(string storedProcedureName)
        {
            List<string> storedProcedureNames = new List<string>();
            OleDbDataReader oleDbDataReader = null;
            StringBuilder sb = new StringBuilder(5000);

            try
            {
                oleDbDataReader = RunQuery("sp_helptext " + storedProcedureName);

                while (oleDbDataReader.Read())
                {
                    sb.Append(oleDbDataReader.GetString(0));
                }
            }
            finally
            {
                if (oleDbDataReader != null && !oleDbDataReader.IsClosed)
                {
                    oleDbDataReader.Close();
                }
            }
            return sb.ToString();
        }

        private DataTable Parameters
        {
            get
            {
                if (_parameters == null)
                {
                    string sql = @"
                        SELECT 
                            PARAMETER_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, PARAMETER_MODE, ORDINAL_POSITION, SPECIFIC_NAME AS ProcedureName
                        FROM
                            INFORMATION_SCHEMA.PARAMETERS";

                    _parameters = RunQueryDataTable(sql);
                }

                return _parameters;
            }
        }

        public Model.StoredProcedure[] GetStoredProcedures()
        {
            string sql1 = @"
                SELECT
                    ROUTINE_NAME, ROUTINE_SCHEMA
                    (
                        SELECT
                            CAST(CASE
                                    WHEN tbl.is_ms_shipped = 1 THEN 1 
                                    WHEN (SELECT major_id FROM sys.extended_properties WHERE major_id = tbl.object_id AND minor_id = 0 AND class = 1 AND name = N'microsoft_database_tools_support') IS NOT NULL THEN 1 ELSE 0 
                                END AS bit
                            ) AS IsSystemObject
                        FROM
                            sys.procedures AS tbl
                        WHERE
                            (name = T.ROUTINE_NAME) AND (SCHEMA_NAME(schema_id) = N'dbo')) AS IsSystemObject
                FROM
                    INFORMATION_SCHEMA.ROUTINES AS T
                WHERE
                    ROUTINE_TYPE = 'PROCEDURE'
                ORDER BY
                    ROUTINE_NAME";

            // For the databases that are missing some system tables
            string sql2 = @"
                SELECT
                    sp.name AS ROUTINE_NAME, '' AS ROUTINE_SCHEMA
                    CAST(CASE 
                            WHEN OBJECTPROPERTY(sp.id, N'IsMSShipped') = 1 THEN 1 
                            WHEN OBJECTPROPERTY(sp.id, N'IsSystemTable') = 1 THEN 1 ELSE 0 
                        END AS bit) AS IsSystemObject
                FROM
                    dbo.sysobjects sp INNER JOIN
                    dbo.sysusers ssp ON ssp.uid = sp.uid
                WHERE
                    sp.type = 'P'
                ORDER BY
                    ROUTINE_NAME";

            List<string> storedProcedureNames = new List<string>();
            OleDbDataReader oleDbDataReader = null;

            try
            {
                try
                {
                    oleDbDataReader = RunQuery(sql1);
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("SCHEMA_NAME") > 0)
                    {
                        oleDbDataReader = RunQuery(sql2);
                    }
                    else
                    {
                        throw;
                    }
                }

                // Exclude system storedProcedures
                int isSysObjectColumnOrdinal = oleDbDataReader.GetOrdinal("IsSystemObject");
                while (oleDbDataReader.Read())
                {
                    bool isSystemObject = oleDbDataReader.IsDBNull(isSysObjectColumnOrdinal) ? false : (bool)oleDbDataReader[isSysObjectColumnOrdinal];
                    if (!isSystemObject)
                    {
                        storedProcedureNames.Add(oleDbDataReader["ROUTINE_NAME"].ToString() + "|" + oleDbDataReader["ROUTINE_SCHEMA"].ToString());
                    }
                }
            }
            finally
            {
                if (oleDbDataReader != null && !oleDbDataReader.IsClosed)
                {
                    oleDbDataReader.Close();
                }
            }

            List<Model.StoredProcedure> storedProcedures = new List<Model.StoredProcedure>();
            foreach (string storedProcedureNameEx in storedProcedureNames)
            {
                string storedProcedureName = storedProcedureNameEx.Split('|')[0];
                string schema = storedProcedureNameEx.Split('|')[1];
                Model.StoredProcedure storedProcedure = GetNewStoredProcedure(storedProcedureName);
                storedProcedure.Schema = schema;
                storedProcedures.Add(storedProcedure);
            }

            return (Model.StoredProcedure[])storedProcedures.ToArray();
        }

        private Model.StoredProcedure GetNewStoredProcedure(string storedProcedureName)
        {
            ArchAngel.Interfaces.ProjectHelper.RaiseObjectBeingProcessedEvent(storedProcedureName, "SP");
            _parameters = null;
            Model.StoredProcedure storedProcedure = new Model.StoredProcedure(storedProcedureName, false);
            storedProcedure.Enabled = false;

            DataRow[] parameterRows = Parameters.Select(string.Format("ProcedureName = '{0}'", storedProcedureName));
            foreach (DataRow parameterRow in parameterRows)
            {
                Model.StoredProcedure.Parameter parameter = new Model.StoredProcedure.Parameter(parameterRow["PARAMETER_NAME"].ToString(), parameterRow["DATA_TYPE"].ToString(),
                    parameterRow["PARAMETER_MODE"].ToString(), System.Convert.ToInt32(parameterRow["ORDINAL_POSITION"]), parameterRow.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : System.Convert.ToInt32(parameterRow["CHARACTER_MAXIMUM_LENGTH"]));
                storedProcedure.AddParameter(parameter);
            }

            return storedProcedure;
        }

        public void FillStoredProcedureColumns(Model.StoredProcedure storedProcedure)
        {
            string sql = "exec [" + storedProcedure.Name + "] ";
            for (int i = 0; i < storedProcedure.Parameters.Length; i++)
            {
                Model.StoredProcedure.Parameter parameter = storedProcedure.Parameters[i];
                sql += parameter.Name + "=NULL";
                if (i < storedProcedure.Parameters.Length - 1)
                {
                    sql += ", ";
                }
            }

            DataTable dataTable = RunQueryDataTable(sql);

            while (storedProcedure.Columns.Length > 0)
            {
                storedProcedure.RemoveColumn(storedProcedure.Columns[0]);
            }

            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                Column column = new Column(
                    dataColumn.ColumnName,
                    false,
                    storedProcedure,
                    dataColumn.Ordinal,
                    dataColumn.AllowDBNull,
                    GetSqlDataType(dataColumn.DataType),
                    0,
                    false,
                    dataColumn.AutoIncrement,
                    dataColumn.DefaultValue.ToString(),
                    false);

                if (IsSupported(column))
                {
                    storedProcedure.AddColumn(column);
                }
            }
        }

        private string GetSqlDataType(Type type)
        {
            /*
            bigint
            bit
            char
            datetime
            decimal
            float
            image
            money
            nchar
            ntext
            numeric
            nvarchar
            real
            smalldatetime
            smallint
            smallmoney
            text
            tinyint
            uniqueidentifier
            */

            if (type == typeof(System.Int64))
            {
                return "bigint";
            }
            else if (type == typeof(System.Boolean))
            {
                return "bit";
            }
            else if (type == typeof(System.String))
            {
                return "varchar";
            }
            else if (type == typeof(System.DateTime))
            {
                return "datetime";
            }
            else if (type == typeof(System.Decimal))
            {
                return "decimal";
            }
            else if (type == typeof(System.Double))
            {
                return "float";
            }
            else if (type == typeof(System.Byte[]))
            {
                return "binary";
            }
            else if (type == typeof(System.Int32))
            {
                return "int";
            }
            else if (type == typeof(System.Single))
            {
                return "real";
            }
            else if (type == typeof(System.Int16))
            {
                return "smallint";
            }
            else if (type == typeof(System.Byte))
            {
                return "tinyint";
            }
            else if (type == typeof(System.Object))
            {
                return "sql_variant";
            }
            else if (type == typeof(System.Guid))
            {
                return "uniqueidentifier";
            }
            else
            {
                throw new Exception(type.Name + " data type not supported by the ArchAngel.Providers.Database.Model API yet. Please contact Slyce support: support@slyce.com");
            }
        }
    }
}