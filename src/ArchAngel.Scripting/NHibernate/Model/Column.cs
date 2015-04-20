using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArchAngel.Scripting.NHibernate.Model
{
    public class ColumnBase
    {
        public ColumnBase()
        {
        }

        public string Name { get; set; }
        public object ScriptObject { get; set; }
    }
}
