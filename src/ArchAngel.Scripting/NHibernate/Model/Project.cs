using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArchAngel.Scripting.NHibernate.Model
{
    public partial class ProjectBase
    {
        public static ProjectBase Instance { get; set; }

        public ProjectBase()
        {
            Entities = new List<EntityBase>();
            Components = new List<ComponentBase>();
            Tables = new List<TableBase>();
        }

        public List<EntityBase> Entities { get; set; }
        public List<ComponentBase> Components { get; set; }
        public List<TableBase> Tables { get; set; }
    }
}
