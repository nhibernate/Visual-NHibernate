using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArchAngel.Scripting.NHibernate.Model
{
    public class TableBase
    {
        public TableBase()
        {
            Columns = new List<ColumnBase>();
        }

        public string Name { get; set; }
        public List<ColumnBase> Columns { get; set; }
        public object ScriptObject { get; set; }
    }
}
