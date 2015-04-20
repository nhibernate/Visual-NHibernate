using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_2000
{
    public class View : SQLServerBase, IView
    {
        private static System.Data.DataTable _columns = null;

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
                    string sql = @"
                        SELECT DISTINCT
                            TABLE_NAME, 
                            COLUMN_NAME, CAST(ORDINAL_POSITION AS INT) AS ORDINAL_POSITION, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, 
                            COLUMN_DEFAULT, COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA +'.'+ TABLE_NAME), COLUMN_NAME, 'IsIdentity') AS IsIdentity, 
                            COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA +'.'+ TABLE_NAME), COLUMN_NAME, 'IsComputed') AS IsComputed
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
            string sql = @"
                SELECT
                    TABLE_NAME, TABLE_SCHEMA
                FROM
                    INFORMATION_SCHEMA.VIEWS
                ORDER BY TABLE_NAME";

            List<string> viewNames = new List<string>();
            OleDbDataReader oleDbDataReader = null;

            try
            {
                oleDbDataReader = RunQuery(sql);

                while (oleDbDataReader.Read())
                {
                    viewNames.Add(oleDbDataReader["TABLE_NAME"].ToString() + "|" + oleDbDataReader["TABLE_SCHEMA"].ToString());
                }
            }
            finally
            {
                if (oleDbDataReader != null && !oleDbDataReader.IsClosed)
                {
                    oleDbDataReader.Close();
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

            return (Model.View[])views.ToArray();
        }

        private Model.View GetNewView(string viewName)
        {
            ArchAngel.Interfaces.ProjectHelper.RaiseObjectBeingProcessedEvent(viewName, "View");
            //_columns = null;
            Model.View view = new Model.View(viewName, false);

            #region Columns

            DataRow[] columnRows = Columns.Select(string.Format("TABLE_NAME = '{0}'", viewName));
            foreach (DataRow columnRow in columnRows)
            {
                Column column = new Column(
                    (string)columnRow["COLUMN_NAME"],
                    false,
                    view,
                    (int)columnRow["ORDINAL_POSITION"],
                    Slyce.Common.Utility.StringsAreEqual((string)columnRow["IS_NULLABLE"], "YES", false),
                    (string)columnRow["DATA_TYPE"],
                    columnRow.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : (int)columnRow["CHARACTER_MAXIMUM_LENGTH"],
                    false,
                    columnRow.IsNull("IsIdentity") ? false : (int)columnRow["IsIdentity"] == 1,
                    columnRow.IsNull("COLUMN_DEFAULT") ? "" : (string)columnRow["COLUMN_DEFAULT"],
                    columnRow.IsNull("IsComputed") ? false : (int)columnRow["IsComputed"] == 1);

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