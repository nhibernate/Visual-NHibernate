using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Smo;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_SMO
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

            foreach (Microsoft.SqlServer.Management.Smo.View smoView in Database.Views)
            {
					if (!smoView.IsSystemObject)
					{
						Model.View view = GetNewView(smoView);
						views.Add(view);
					}
            }

            return (Model.View[])views.ToArray();
        }

        private Model.View GetNewView(Microsoft.SqlServer.Management.Smo.View smoView)
        {
            Model.View view = new Model.View(smoView.Name, Script.GetSingluar(smoView.Name), false);

            // Columns
            int ordinalPosition = 0;
            List<Microsoft.SqlServer.Management.Smo.Column> smoColumns = new List<Microsoft.SqlServer.Management.Smo.Column>();
            foreach (Microsoft.SqlServer.Management.Smo.Column smoColumn in smoView.Columns)
            {
                smoColumns.Add(smoColumn);
            }

            smoColumns.Sort(new SortComparer<Microsoft.SqlServer.Management.Smo.Column>("ID", System.ComponentModel.ListSortDirection.Ascending));

            foreach (Microsoft.SqlServer.Management.Smo.Column smoColumn in smoColumns)
            {
                if (UnsupportedDataTypes.ToLower().IndexOf("'" + smoColumn.DataType.Name.ToLower() + "'") >= 0)
                {
                    continue;
                }

                Model.Column column = new Model.Column(smoColumn.Name, Script.GetSingluar(smoColumn.Name), false, smoColumn.Name, view, ordinalPosition, smoColumn.Nullable, smoColumn.DataType.Name, smoColumn.DataType.MaximumLength,
                    false, smoColumn.Identity, smoColumn.Default, true);
                view.AddColumn(column);
                ordinalPosition++;
            }

            return view;
        }
    }
}
