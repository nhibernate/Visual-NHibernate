using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArchAngel.Scripting.NHibernate.Model
{
    public class ComponentBase
    {
        public ComponentBase()
        {
            Properties = new List<FieldBase>();
        }

        public string Name { get; set; }
        public List<FieldBase> Properties { get; set; }
        public object ScriptObject { get; set; }
    }
}
