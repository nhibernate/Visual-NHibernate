using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
    public class CustomNamespace
    {
        private bool _AutoAddToEntities = true;

        public CustomNamespace(string value)
        {
            Entities = new HashSet<Entity>();
            InternalIdentifier = Guid.NewGuid();
            Value = value;
        }

        public Guid InternalIdentifier { get; private set; }
        public string Value { get; set; }
        
        public bool AutoAddToEntities
        {
            get { return _AutoAddToEntities; }
            set { _AutoAddToEntities = value; }
        }

        public HashSet<Entity> Entities { get; set; }
    }
}
