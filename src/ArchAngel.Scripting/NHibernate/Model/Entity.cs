using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArchAngel.Scripting.NHibernate.Model
{
    public class EntityBase
    {
        public EntityBase()
        {
            Properties = new List<PropertyBase>();
            Components = new List<ComponentPropertyBase>();
            References = new List<ReferenceBase>();
        }

        public string Name { get; set; }
        public string ParentName { get; set; }
        public bool IsInherited { get; set; }
        public bool HasMappedTables { get; set; }
        public List<PropertyBase> Properties { get; set; }
        public List<ComponentPropertyBase> Components { get; set; }
        public List<ReferenceBase> References { get; set; }
        public object ScriptObject { get; set; }
    }
}
