using System.Collections.Generic;
// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;

namespace ArchAngel.Providers.Database.BLL
{
    public class View : ScriptBLL
    {
        private List<Model.View> _scriptObjects = new List<Model.View>();

        public View(DatabaseTypes dalAssemblyName, ConnectionStringHelper connectionString)
            : base(dalAssemblyName, connectionString)//, Model.View.ViewPrefixes)
        {
            IView dal = DALFactory.DataAccess.CreateView(dalAssemblyName, ConnectionString);

            Model.View[] views = dal.GetViews();
            //InitiateAlias(views);
            InitialCreateFilters(views);

            _scriptObjects = new List<Model.View>(views);
        }

        public Model.View[] Views
        {
            get { return _scriptObjects.ToArray(); }
        }

        private static void InitialCreateFilters(IList<Model.View> views)
        {
            foreach (Model.View view in views)
            {
                // Get All Filter
                string allAlias = "Get" + view.AliasPlural;
                Filter allFilter = new Filter(allAlias, false, view, true, true, false, "", null);
                view.AddFilter(allFilter);
            }
        }
    }
}