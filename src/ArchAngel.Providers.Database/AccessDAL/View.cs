using System.Collections.Generic;
// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.IDAL;

namespace ArchAngel.Providers.Database.AccessDAL
{
    public class View : AccessBase, IView
    {
        public View(string fileName)
            : base(fileName)
        {
        }

        public Model.View[] GetViews()
        {
            List<Model.View> views = new List<Model.View>();

            return views.ToArray();
        }
    }
}
