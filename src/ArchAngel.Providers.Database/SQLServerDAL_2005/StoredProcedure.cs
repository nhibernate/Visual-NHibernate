using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;

namespace ArchAngel.Providers.Database.SQLServerDAL_2005
{
    public class StoredProcedure : SQLServerBase, IStoredProcedure
    {
        private DataTable _parameters;
        //private List<string> StoredProcsWithTempReferences = new List<string>();
        private static readonly char[] EndChars = new char[] { ',', ' ' };

        private struct StoredProcDef
        {
            public readonly string Name;
            public string Body;

            public StoredProcDef(string name, string body)
            {
                Name = name;
                Body = body;
            }
        }

        public StoredProcedure(BLL.ConnectionStringHelper connectionString)
            : base(connectionString)
        {
        }

        private DataTable Parameters
        {
            get
            {
                if (_parameters == null)
                {
                    const string sql = @"
                        SELECT 
                            PARAMETER_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, PARAMETER_MODE, ORDINAL_POSITION, SPECIFIC_NAME AS ProcedureName,
                            NUMERIC_PRECISION, NUMERIC_SCALE
                        FROM
                            INFORMATION_SCHEMA.PARAMETERS";

                    _parameters = RunQueryDataTable(sql);
                }

                return _parameters;
            }
        }

        public Model.StoredProcedure[] GetStoredProcedures()
        {
            //StoredProcsWithTempReferences.Clear();

            const string sql1 = @"
                SELECT
                    ROUTINE_NAME, ROUTINE_SCHEMA, ROUTINE_DEFINITION,
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
                            (name = T.ROUTINE_NAME)) AS IsSystemObject
                FROM
                    INFORMATION_SCHEMA.ROUTINES AS T
                WHERE
                    ROUTINE_TYPE = 'PROCEDURE'
                ORDER BY
                    ROUTINE_NAME";

            // For the databases that are missing some system tables
            const string sql2 = @"
                SELECT
                    ROUTINE_NAME, ROUTINE_SCHEMA, ROUTINE_DEFINITION,
                    CAST(CASE 
                            WHEN OBJECTPROPERTY(sp.id, N'IsMSShipped') = 1 THEN 1 
                            WHEN OBJECTPROPERTY(sp.id, N'IsSystemTable') = 1 THEN 1 ELSE 0 
                        END AS bit) AS IsSystemObject
                FROM
                    INFORMATION_SCHEMA.ROUTINES AS T
			        INNER JOIN dbo.sysobjects SP ON SP.name = T.ROUTINE_NAME
                WHERE
                    ROUTINE_TYPE = 'PROCEDURE'
                ORDER BY
                    ROUTINE_NAME";

            List<StoredProcDef> storedProcedureNames = new List<StoredProcDef>();
            //OleDbDataReader oleDbDataReader = null;
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;

            try
            {
                try
                {
                    //oleDbDataReader = RunQuerySQL(sql1);
                    sqlDataReader = RunQuerySQL(sql1);
                }
                catch (Exception ex)
                {
                    string message = ex.Message.ToLower();

                    if (message.IndexOf("schema_name") > 0 || message.IndexOf("invalid object name") >= 0)
                    {
                        //oleDbDataReader = RunQuerySQL(sql2);
                        sqlDataReader = RunQuerySQL(sql2);
                    }
                    else
                    {
                        throw;
                    }
                }

                // Exclude system storedProcedures
                int isSysObjectColumnOrdinal = sqlDataReader.GetOrdinal("IsSystemObject");

                while (sqlDataReader.Read())
                {
                    bool isSystemObject = sqlDataReader.IsDBNull(isSysObjectColumnOrdinal) ? false : (bool)sqlDataReader[isSysObjectColumnOrdinal];

                    if (!isSystemObject || Model.Database.IncludeSystemObjects)
                    {
                        //storedProcedureNames.Add(oleDbDataReader["ROUTINE_NAME"].ToString() + "|" + oleDbDataReader["ROUTINE_SCHEMA"].ToString());
                        storedProcedureNames.Add(new StoredProcDef(sqlDataReader["ROUTINE_NAME"].ToString() + "|" + sqlDataReader["ROUTINE_SCHEMA"].ToString(), sqlDataReader["ROUTINE_DEFINITION"].ToString()));

                        //if (oleDbDataReader["ROUTINE_DEFINITION"].ToString().IndexOf("#") > 0)
                        //{
                        //    StoredProcsWithTempReferences.Add(oleDbDataReader["ROUTINE_NAME"].ToString());
                        //}
                    }
                }
            }
            finally
            {
                if (sqlDataReader != null && !sqlDataReader.IsClosed)
                {
                    sqlDataReader.Close();
                }
            }
            //StoredProcsWithTempReferences.Sort();
            //ProcessProsWithTempReferences(storedProcedureNames);

            List<Model.StoredProcedure> storedProcedures = new List<Model.StoredProcedure>();
            foreach (StoredProcDef storedProcedureNameEx in storedProcedureNames)
            {
                string storedProcedureName = storedProcedureNameEx.Name.Split('|')[0];
                string schema = storedProcedureNameEx.Name.Split('|')[1];
                Model.StoredProcedure storedProcedure = GetNewStoredProcedure(storedProcedureName, schema);
                storedProcedures.Add(storedProcedure);
            }

            return storedProcedures.ToArray();
        }

        //private void ProcessProsWithTempReferences(List<StoredProcDef> storedProcs)
        //{
        //    bool added = true;

        //    while (added)
        //    {
        //        added = false;

        //        for (int i = 0; i < storedProcs.Count; i++)
        //        {
        //            string name = storedProcs[i].Name;
        //            name = name.Substring(0, name.IndexOf("|"));

        //            if (StoredProcsWithTempReferences.BinarySearch(name) >= 0)
        //            {
        //                continue;
        //            }

        //            for (int procCounter = 0; procCounter < StoredProcsWithTempReferences.Count; procCounter++)
        //            {
        //                string badProc = StoredProcsWithTempReferences[procCounter];

        //                if (storedProcs[i].Body.IndexOf(badProc) > 0)
        //                {
        //                    StoredProcsWithTempReferences.Add(name);
        //                    StoredProcsWithTempReferences.Sort();
        //                    added = true;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}

        private Model.StoredProcedure GetNewStoredProcedure(string storedProcedureName, string schema)
        {
			if (string.IsNullOrEmpty(schema))
				throw new ArgumentException("Schema must be specified to get stored proc.");
			
			Interfaces.Events.RaiseObjectBeingProcessedEvent(storedProcedureName, "SP");
            //_columns = null;
            //_parameters = null;
            Model.StoredProcedure storedProcedure = new Model.StoredProcedure(storedProcedureName, false);
            storedProcedure.Enabled = false;
        	
			
			storedProcedure.Schema = schema;

            DataRow[] parameterRows = Parameters.Select(string.Format("ProcedureName = '{0}'", storedProcedureName));
            foreach (DataRow parameterRow in parameterRows)
            {
                Model.StoredProcedure.Parameter parameter = new Model.StoredProcedure.Parameter(
                    parameterRow["PARAMETER_NAME"].ToString().Replace("@", ""),
                    parameterRow["DATA_TYPE"].ToString(),
                    parameterRow["PARAMETER_MODE"].ToString(),
                    int.Parse(parameterRow["ORDINAL_POSITION"].ToString()),
                    parameterRow.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : int.Parse(parameterRow["CHARACTER_MAXIMUM_LENGTH"].ToString()),
                    parameterRow.IsNull("NUMERIC_PRECISION") ? 0 : int.Parse(parameterRow["NUMERIC_PRECISION"].ToString()),
                    parameterRow.IsNull("NUMERIC_SCALE") ? 0 : int.Parse(parameterRow["NUMERIC_SCALE"].ToString()));
                storedProcedure.AddParameter(parameter);
            }
            FillStoredProcedureColumns(storedProcedure);

            return storedProcedure;
        }

        public void FillStoredProcedureColumns(Model.StoredProcedure storedProcedure)
        {
            string sql = string.Format("SET FMTONLY ON EXEC [{0}].[{1}] ", storedProcedure.Schema, storedProcedure.Name);

            for (int i = 0; i < storedProcedure.Parameters.Length; i++)
            {
                if (Providers.Database.Helper.SQLServer.IsDataTypeText(storedProcedure.Parameters[i].DataType))
                {
                    sql += "\"0\", ";
                }
                else
                {
                    sql += "0, ";
                }
            }
            sql = sql.TrimEnd(EndChars);
            DataTable dataTable;

            try
            {
                dataTable = RunQueryDataTable(sql);
            }
            catch (Exception ex)
            {
                //ResetConnection();
                sql = sql.Replace("SET FMTONLY ON", "SET FMTONLY OFF");

                try
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.RequiresNew))
                    {
                        dataTable = RunQueryDataTable(sql);
                        // We must ALWAYS rollback because we are executing stored procs with FMTONLY = OFF, so just don't call scope.Complete()
                    }
                }
                catch
                {
                    storedProcedure.Errors.Add(string.Format("Error getting returned columns: {0}. SQL: {1}", ex.Message, sql));
                    return;
                }
            }
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
                    false,
                    false,
                    0,
                    0);

                if (IsSupported(column))
                {
                    storedProcedure.AddColumn(column);
                }
            }
        }

        public string GetStoredProcedureBody(string storedProcedureName)
        {
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            StringBuilder sb = new StringBuilder(5000);

            try
            {
                sqlDataReader = RunQuerySQL("sp_helptext " + storedProcedureName);

                while (sqlDataReader.Read())
                {
                    sb.Append(sqlDataReader.GetString(0));
                }
            }
            finally
            {
                if (sqlDataReader != null && !sqlDataReader.IsClosed)
                {
                    sqlDataReader.Close();
                }
            }
            return sb.ToString();
        }

        private static string GetSqlDataType(Type type)
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

            if (type == typeof(Int64))
            {
                return "bigint";
            }
            if (type == typeof(Boolean))
            {
                return "bit";
            }
            if (type == typeof(String))
            {
                return "varchar";
            }
            if (type == typeof(DateTime))
            {
                return "datetime";
            }
            if (type == typeof(Decimal))
            {
                return "decimal";
            }
            if (type == typeof(Double))
            {
                return "float";
            }
            if (type == typeof(Byte[]))
            {
                return "binary";
            }
            if (type == typeof(Int32))
            {
                return "int";
            }
            if (type == typeof(Single))
            {
                return "real";
            }
            if (type == typeof(Int16))
            {
                return "smallint";
            }
            if (type == typeof(Byte))
            {
                return "tinyint";
            }
            if (type == typeof(Object))
            {
                return "sql_variant";
            }
            if (type == typeof(Guid))
            {
                return "uniqueidentifier";
            }
            throw new Exception(type.Name + " data type not supported by the ArchAngel.Providers.Database.Model API yet. Please contact Slyce support: support@slyce.com");
        }
    }
}