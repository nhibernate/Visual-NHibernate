using System;
using System.Collections.Generic;
using System.Data;
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;

namespace ArchAngel.Providers.Database.SQLServerDAL_2005
{
    public class View : SQLServerBase, IView
    {
        private DataTable _columns;

        public View(BLL.ConnectionStringHelper connectionString)
            : base(connectionString)
        {
        }

        private DataTable Columns
        {
            get
            {
                if (_columns == null)
                {
                	const string sql = @"
                        SELECT DISTINCT
                            TABLE_NAME, 
                            COLUMN_NAME, CAST(ORDINAL_POSITION AS INT) AS ORDINAL_POSITION, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, 
                            COLUMN_DEFAULT, COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA +'.'+ TABLE_NAME), COLUMN_NAME, 'IsIdentity') AS IsIdentity, 
                            COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA +'.'+ TABLE_NAME), COLUMN_NAME, 'IsComputed') AS IsComputed,
                            NUMERIC_PRECISION, NUMERIC_SCALE
                        FROM
                            INFORMATION_SCHEMA.COLUMNS C
                        ORDER BY 
                            TABLE_NAME, ORDINAL_POSITION";

                	_columns = RunQueryDataTable(sql);
                }

            	return _columns;
            }
        }

        public Model.View[] GetViews()
        {
            const string sql = @"
                SELECT
                    TABLE_NAME, TABLE_SCHEMA
                FROM
                    INFORMATION_SCHEMA.VIEWS
                ORDER BY TABLE_NAME";

            List<string> viewNames = new List<string>();
            //OleDbDataReader oleDbDataReader = null;
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;

            try
            {
                sqlDataReader = RunQuerySQL(sql);

                while (sqlDataReader.Read())
                {
                    viewNames.Add(sqlDataReader["TABLE_NAME"].ToString() + "|" + sqlDataReader["TABLE_SCHEMA"].ToString());
                }
            }
            finally
            {
                if (sqlDataReader != null && !sqlDataReader.IsClosed)
                {
                    sqlDataReader.Close();
                }
            }

            List<Model.View> views = new List<Model.View>();
            foreach (string viewNameEx in viewNames)
            {
                string viewName = viewNameEx.Split('|')[0];
                string schema = viewNameEx.Split('|')[1];
                Model.View view = GetNewView(viewName);
                view.Schema = schema;
                views.Add(view);
            }

            return views.ToArray();
        }

        private Model.View GetNewView(string viewName)
        {
            Interfaces.Events.RaiseObjectBeingProcessedEvent(viewName, "View");
            //_columns = null;
            Model.View view = new Model.View(viewName, false);

            #region Columns

            DataRow[] columnRows = Columns.Select(string.Format("TABLE_NAME = '{0}'", viewName));
            foreach (DataRow columnRow in columnRows)
            {
                bool isReadOnly = false;

                if (!columnRow.IsNull("IsIdentity") && (int)columnRow["IsIdentity"] == 1)
                {
                    isReadOnly = true;
                }
                else if (!columnRow.IsNull("IsComputed") && (int)columnRow["IsComputed"] == 1)
                {
                    isReadOnly = true;
                }
                else if (Slyce.Common.Utility.StringsAreEqual((string)columnRow["DATA_TYPE"], "timestamp", false))
                {
                    isReadOnly = true;
                }
                Column column = new Column(
                    (string)columnRow["COLUMN_NAME"],
                    false,
                    view,
                    (int)columnRow["ORDINAL_POSITION"],
                    Slyce.Common.Utility.StringsAreEqual((string)columnRow["IS_NULLABLE"], "YES", false),
                    (string)columnRow["DATA_TYPE"],
                    columnRow.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : Convert.ToInt32(columnRow["CHARACTER_MAXIMUM_LENGTH"]),
                    false,
                    columnRow.IsNull("IsIdentity") ? false : Convert.ToInt32(columnRow["IsIdentity"]) == 1,
                    columnRow.IsNull("COLUMN_DEFAULT") ? "" : (string)columnRow["COLUMN_DEFAULT"],
                    isReadOnly,
                    columnRow.IsNull("IsComputed") ? false : Convert.ToInt32(columnRow["IsComputed"]) == 1,
                    columnRow.IsNull("NUMERIC_PRECISION") ? 0 : Convert.ToInt32(columnRow["NUMERIC_PRECISION"]),
                    columnRow.IsNull("NUMERIC_SCALE") ? 0 : Convert.ToInt32(columnRow["NUMERIC_SCALE"]));

                if (IsSupported(column))
                {
                    view.AddColumn(column);
                }
            }

            #endregion

            return view;
        }
    }
}