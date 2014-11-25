using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
    public class CustomAttribute
    {
        private bool _AutoAddToEntities = true;
        private bool _AutoAddToProperties = true;
        private string _RawName;
        private string _RawArgumentString;

        public CustomAttribute(string name, string argumentString)
        {
            Entities = new HashSet<Entity>();
            InternalIdentifier = Guid.NewGuid();
            RawName = name;
            RawArgumentString = argumentString;
        }

        public string Id { get; set; }
        public Guid InternalIdentifier { get; private set; }
        public HashSet<Entity> Entities { get; set; }
        public HashSet<Property> Properties { get; set; }

        public string RawName 
        {
            get { return _RawName; }
            set { _RawName = value.Trim(); }
        }
        
        public string RawArgumentString
        {
            get { return _RawArgumentString; }
            set { _RawArgumentString = value.Trim(); }
        }
        
        public string GetFullStringFor(Entity entity)
        {
            if (string.IsNullOrEmpty(RawArgumentString))
                return string.Format("[{0}]", GetNameFor(entity));
            else
                return string.Format("[{0}({1})]", GetNameFor(entity), GetArgumentStringFor(entity));
        }
        
        public string GetNameFor(Entity entity)
        {
            return RawName.Replace("#entity.Name#", entity.Name);
        }

        public string GetArgumentStringFor(Entity entity)
        {
            return RawArgumentString.Replace("#entity.Name#", entity.Name);
        }

        public string GetNameFor(Property property)
        {
            return RawName
                .Replace("#entity.Name#", property.Entity.Name)
                .Replace("#property.Name#", property.Name)
                .Replace("#property.Type#", property.Type);
        }

        public string GetArgumentStringFor(Property property)
        {
            return RawArgumentString
                .Replace("#entity.Name#", property.Entity.Name)
                .Replace("#property.Name#", property.Name)
                .Replace("#property.Type#", property.Type);
        }

        public bool AutoAddToEntities
        {
            get { return _AutoAddToEntities; }
            set { _AutoAddToEntities = value; }
        }

        public bool AutoAddToProperties
        {
            get { return _AutoAddToProperties; }
            set { _AutoAddToProperties = value; }
        }
    }
}
