using System;
using System.Collections.Generic;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;

namespace ArchAngel.Providers.Database.SQLServerDAL_ADONET
{
    public class View : SQLServerBase, IView
    {
        public View(string connectionString)
            : base(connectionString)
        {
        }

        public Model.View[] GetViews()
        {
            List<Model.View> views = new List<Model.View>();

            return (Model.View[])views.ToArray();
        }
    }
}
