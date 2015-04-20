using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_Express
{
    public class StoredProcedure : SQLServerBase, IStoredProcedure
    {
        // TODO; Implement
        //private readonly string UnsupportedDataTypes = "'binary', 'sql_variant', 'timestamp', 'varbinary'";

        public StoredProcedure(BLL.ConnectionStringHelper connectionString)
            : base(connectionString)
        {
        }

        public Model.StoredProcedure[] GetStoredProcedures()
        {
            List<Model.StoredProcedure> storedProcedures = new List<Model.StoredProcedure>();

            string sql = string.Format(@"SELECT ROUTINE_NAME,
		                                        (
			                                        SELECT
			                                        CAST(
			                                         CASE 
				                                        WHEN tbl.is_ms_shipped = 1 THEN 1
				                                        WHEN (
					                                        SELECT 
						                                        major_id 
					                                        FROM 
						                                        sys.extended_properties 
					                                        WHERE 
						                                        major_id = tbl.object_id and 
						                                        minor_id = 0 and 
						                                        class = 1 and 
						                                        name = N'microsoft_database_tools_support') 
					                                        IS NOT NULL THEN 1
				                                        ELSE 0
			                                        END          
						                                         AS bit) AS [IsSystemObject]
			                                        FROM
			                                        sys.procedures AS tbl
			                                        WHERE
			                                        (tbl.name = T.ROUTINE_NAME and SCHEMA_NAME(tbl.schema_id)=N'dbo')
		                                        ) AS IsSystemObject
                                        FROM {0}.INFORMATION_SCHEMA.ROUTINES T
                                        WHERE T.ROUTINE_TYPE = 'PROCEDURE'
                                        ORDER BY T.ROUTINE_NAME", DatabaseName);

            System.Data.SqlClient.SqlDataReader dr = null;

            try
            {
                dr = RunQuerySqlClient(sql);
                System.Collections.ArrayList arrStoredProcNames = new System.Collections.ArrayList();
                bool isSystemObject;
                int isSysObjectColumnOrdinal = dr.GetOrdinal("IsSystemObject");

                while (dr.Read())
                {
                    isSystemObject = dr.IsDBNull(isSysObjectColumnOrdinal) ? false : (bool)dr[isSysObjectColumnOrdinal];

                    if (!isSystemObject || Model.Database.IncludeSystemObjects)
                    {
                        arrStoredProcNames.Add((string)dr["ROUTINE_NAME"]);
                    }
                }
                dr.Close();

                for (int i = 0; i < arrStoredProcNames.Count; i++)
                {
                    Model.StoredProcedure storedProcedure = GetNewStoredProcedure((string)arrStoredProcNames[i]);
                    storedProcedures.Add(storedProcedure);
                }
            }
            finally
            {
                if (dr != null) { dr.Close(); }
            }
            return (Model.StoredProcedure[])storedProcedures.ToArray();
        }

        private Model.StoredProcedure GetNewStoredProcedure(string storedProcName)
        {
            ArchAngel.Interfaces.Events.RaiseObjectBeingProcessedEvent(storedProcName, "SP");
            Model.StoredProcedure storedProcedure = new Model.StoredProcedure(storedProcName, false);
            storedProcedure.Enabled = false;

            // Columns
            //int ordinalPosition = 0;
            //Microsoft.SqlServer.Management.Smo.Column[] smoColumns = GetColumns(smoStoredProcedure);
            //foreach (Microsoft.SqlServer.Management.Smo.Column smoColumn in smoColumns)
            //{
            /*if (UnsupportedDataTypes.ToLower().IndexOf("'" + smoColumn.PhysicalDatatype.ToLower() + "'") >= 0)
            {
                continue;
            }

            Column column = new Column(smoColumn.Name, Script.GetSingular(smoColumn.Name), smoColumn.Name, storedProcedure, ordinalPosition, smoColumn.Nullable, smoColumn.PhysicalDatatype, smoColumn.Length,
                smoColumn.InPrimaryKey, false, false, smoColumn.Identity);
            storedProcedure.Columns.Add(column);
            ordinalPosition++;*/
            //}

            // Parameters
            string sql = string.Format(@"
                SELECT PARAMETER_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, PARAMETER_MODE
                FROM {0}.INFORMATION_SCHEMA.PARAMETERS 
                WHERE SPECIFIC_NAME = '{1}' ", DatabaseName, storedProcName);

            System.Data.SqlClient.SqlDataReader dr = null;

            try
            {
                dr = RunQuerySqlClient(sql);

                while (dr.Read())
                {
                    //Model.StoredProcedure.Parameter parameter = new Model.StoredProcedure.Parameter(parameterRow["PARAMETER_NAME"].ToString(), parameterRow["PARAMETER_NAME"].ToString().Replace("@", ""), parameterRow["DATA_TYPE"].ToString(),
                    //parameterRow["PARAMETER_MODE"].ToString(), System.Convert.ToInt32(parameterRow["ORDINAL_POSITION"]), System.Convert.ToInt32(parameterRow["CHARACTER_MAXIMUM_LENGTH"]));
                    //storedProcedure.AddParameter(parameter);
                }
            }
            finally
            {
                if (dr != null) { dr.Close(); }
            }
            return storedProcedure;
        }

        public void FillStoredProcedureColumns(Model.StoredProcedure storedProcedure)
        {
        }

        public string GetStoredProcedureBody(string storedProcedureName)
        {
            throw new NotImplementedException("Not coded yet.");
        }
    }
}
