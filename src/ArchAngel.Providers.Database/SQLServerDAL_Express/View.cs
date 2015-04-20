using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
//using Microsoft.SqlServer.Management.Smo;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_Express
{
    public class View : SQLServerBase, IView
    {
        private System.Data.DataTable _dtColumns = null;

        public View(BLL.ConnectionStringHelper connectionString)
            : base(connectionString)
        {
        }

        private DataTable DtColumns
        {
            get
            {
                if (_dtColumns == null)
                {
                    string sql = string.Format(@"
                        SELECT	DISTINCT 
					            (
						            SELECT COUNT(T.CONSTRAINT_TYPE) 
						            FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS T
							            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE U ON C.Column_Name = U.Column_Name AND C.Table_Name = U.Table_Name
						            WHERE T.Constraint_Name = U.Constraint_Name AND T.CONSTRAINT_TYPE = 'PRIMARY KEY'
					            ) AS InPrimaryKey,
                                C.TABLE_NAME,
					            C.COLUMN_NAME, 
					            CAST(C.ORDINAL_POSITION AS INT) AS [ORDINAL_POSITION],
					            C.IS_NULLABLE,
					            C.DATA_TYPE,
					            C.CHARACTER_MAXIMUM_LENGTH,
					            C.COLUMN_DEFAULT,
	                            COLUMNPROPERTY(object_id(C.TABLE_SCHEMA +'.'+ C.TABLE_NAME), C.COLUMN_NAME, 'IsIdentity') AS IsIdentity,
	                            COLUMNPROPERTY(object_id(C.TABLE_SCHEMA +'.'+ C.TABLE_NAME), C.COLUMN_NAME, 'IsComputed') AS IsComputed,
                                NUMERIC_PRECISION, NUMERIC_SCALE
                        FROM INFORMATION_SCHEMA.COLUMNS C
                        ORDER BY C.TABLE_NAME, C.COLUMN_NAME", DatabaseName);

                    _dtColumns = RunQueryDataTable(sql);
                }
                return _dtColumns;
            }
        }

        public Model.View[] GetViews()
        {
            List<Model.View> views = new List<Model.View>();

            string sql = string.Format(@"SELECT TABLE_NAME
                                        FROM INFORMATION_SCHEMA.VIEWS V
                                        ORDER BY V.TABLE_NAME", DatabaseName);

            System.Data.SqlClient.SqlDataReader dr = null;

            try
            {
                dr = RunQuerySqlClient(sql);
                System.Collections.ArrayList arrTableNames = new System.Collections.ArrayList();

                while (dr.Read())
                {
                    arrTableNames.Add((string)dr["TABLE_NAME"]);
                }
                dr.Close();

                for (int i = 0; i < arrTableNames.Count; i++)
                {
                    Model.View view = GetNewView((string)arrTableNames[i]);
                    views.Add(view);
                }
            }
            finally
            {
                if (dr != null) { dr.Close(); }
            }
            return (Model.View[])views.ToArray();
        }

        private Model.View GetNewView(string viewName)
        {
            ArchAngel.Interfaces.Events.RaiseObjectBeingProcessedEvent(viewName, "View");
            //_dtColumns = null;
            Model.View view = new Model.View(viewName, false);

            // Columns
            DataRow[] columnRows = DtColumns.Select(string.Format("TABLE_NAME = '{0}'", viewName));

            foreach (DataRow row in columnRows)
            {
                bool isReadOnly = false;

                if (!row.IsNull("IsIdentity") && (int)row["IsIdentity"] == 1)
                {
                    isReadOnly = true;
                }
                else if (!row.IsNull("IsComputed") && (int)row["IsComputed"] == 1)
                {
                    isReadOnly = true;
                }
                else if (Slyce.Common.Utility.StringsAreEqual((string)row["DATA_TYPE"], "timestamp", false))
                {
                    isReadOnly = true;
                }
                // Check whether we have added this column before. Columns are repeated if they are both a PRIMARY_KEY and a FOREIGN_KEY
                ArchAngel.Providers.Database.Model.Column column = new ArchAngel.Providers.Database.Model.Column(
                    (string)row["COLUMN_NAME"],
                    false,
                    view,
                    (int)row["ORDINAL_POSITION"],
                    Slyce.Common.Utility.StringsAreEqual((string)row["IS_NULLABLE"], "YES", false),
                    (string)row["DATA_TYPE"],
                    row.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : Convert.ToInt32(row["CHARACTER_MAXIMUM_LENGTH"]),
                    (int)row["InPrimaryKey"] == 1,
                    row.IsNull("IsIdentity") ? false : Convert.ToInt32(row["IsIdentity"]) == 1,
                    row.IsNull("COLUMN_DEFAULT") ? "" : (string)row["COLUMN_DEFAULT"],
                    isReadOnly,
                    row.IsNull("IsComputed") ? false : Convert.ToInt32(row["IsComputed"]) == 1,
                    row.IsNull("NUMERIC_PRECISION") ? 0 : Convert.ToInt32(row["NUMERIC_PRECISION"]),
                    row.IsNull("NUMERIC_SCALE") ? 0 : Convert.ToInt32(row["NUMERIC_SCALE"]));

                view.AddColumn(column);
            }
            //smoColumns.Sort(new SortComparer<Microsoft.SqlServer.Management.Smo.Column>("ID", System.ComponentModel.ListSortDirection.Ascending));

            //foreach (Microsoft.SqlServer.Management.Smo.Column smoColumn in smoColumns)
            //{
            //    if (UnsupportedDataTypes.ToLower().IndexOf("'" + smoColumn.DataType.Name.ToLower() + "'") >= 0)
            //    {
            //        continue;
            //    }

            //    Model.Column column = new Model.Column(smoColumn.Name, Script.GetSingular(smoColumn.Name), false, smoColumn.Name, view, ordinalPosition, smoColumn.Nullable, smoColumn.DataType.Name, smoColumn.DataType.MaximumLength,
            //        false, smoColumn.Identity, smoColumn.Default, true);
            //    view.AddColumn(column);
            //    ordinalPosition++;
            //}

            return view;
        }
    }
}
