using System;
using System.Collections.Generic;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_DMO
{
    public class View : SQLServerBase, IView
    {
        private readonly string UnsupportedDataTypes = "'binary', 'sql_variant', 'timestamp', 'varbinary'";

        public View(BLL.ConnectionStringHelper connectionString)
            : base(connectionString)
        {
        }

        public Model.View[] GetViews()
        {
            List<Model.View> views = new List<Model.View>();

            foreach (SQLDMO.View dmoView in Database.Views)
            {
                Model.View view = GetNewView(dmoView);
                views.Add(view);
            }

            return (Model.View[])views.ToArray();
        }

        private Model.View GetNewView(SQLDMO.View dmoView)
        {
            Model.View view = new Model.View(dmoView.Name, Script.GetSingluar(dmoView.Name), false);

            // Columns
            int ordinalPosition = 0;
            List<SQLDMO.Column> dmoColumns = new List<SQLDMO.Column>();
            SQLDMO.SQLObjectList sqlObjectList = dmoView.ListColumns();
            foreach (SQLDMO.Column dmoColumn in sqlObjectList)
            {
                dmoColumns.Add(dmoColumn);
            }

            dmoColumns.Sort(new SortComparer<SQLDMO.Column>("ID", System.ComponentModel.ListSortDirection.Ascending));

            foreach (SQLDMO.Column dmoColumn in dmoColumns)
            {
                if (UnsupportedDataTypes.ToLower().IndexOf("'" + dmoColumn.PhysicalDatatype.ToLower() + "'") >= 0)
                {
                    continue;
                }

                Column column = new Column(dmoColumn.Name, Script.GetSingluar(dmoColumn.Name), false, dmoColumn.Name, view, ordinalPosition, dmoColumn.AllowNulls, dmoColumn.PhysicalDatatype, dmoColumn.Length,
                    false, dmoColumn.Identity, dmoColumn.Default, true);
                view.AddColumn(column);
                ordinalPosition++;
            }

            return view;
        }
    }
}
