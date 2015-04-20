using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
    public class CustomProperty
    {
        private bool _AutoAddToEntities = true;
        public string Name;
        private string _UserString;
        private ArchAngel.Providers.CodeProvider.DotNet.Property _CodeProperty;

        public CustomProperty(string name, string userString)
        {
            Entities = new HashSet<Entity>();
            InternalIdentifier = Guid.NewGuid();
            Name = name;
            UserString = userString;
        }

        public string Id { get; set; }
        public Guid InternalIdentifier { get; private set; }
        public HashSet<Entity> Entities { get; set; }

        public string UserString
        {
            get { return _UserString; }
            set
            {
                if (_UserString != value)
                {
                    _UserString = value;
                    _CodeProperty = null;
                }
            }
        }

        public ArchAngel.Providers.CodeProvider.DotNet.Property CodeProperty
        {
            get
            {
                if (_CodeProperty == null && !string.IsNullOrEmpty(UserString))
                {
                    string tempName;

                    if (!CodePropertyIsValid(UserString, out tempName, out _CodeProperty))
                        throw new Exception("The code is not valid. It has an error.");

                    Name = tempName;
                }
                return _CodeProperty;
            }
            set { _CodeProperty = value; }
        }

        public bool AutoAddToEntities
        {
            get { return _AutoAddToEntities; }
            set { _AutoAddToEntities = value; }
        }

        public string ToStringFor(Entity entity)
        {
            return UserString.Replace("#entity.Name#", entity.Name);
        }

        public string GetNameFor(Entity entity)
        {
            return CodeProperty.Name.Replace("#entity.Name#", entity.Name);
        }

        public string GetTypeFor(Entity entity)
        {
            return CodeProperty.DataType.ToString().Replace("#entity.Name#", entity.Name);
        }

        public string GetModifiersFor(Entity entity)
        {
            string result = "";

            foreach (string modifier in CodeProperty.Modifiers)
            {
                if (result.Length > 0)
                    result += ", ";

                result += modifier;
            }
            return result;
        }

        public bool HasGetAccessor
        {
            get { return CodeProperty.GetAccessor != null; }
        }

        public bool HasSetAccessor
        {
            get { return CodeProperty.SetAccessor != null; }
        }

        public string GetAccessorModifier
        {
            get { return CodeProperty.GetAccessor.Modifier; }
        }

        public string SetAccessorModifier
        {
            get { return CodeProperty.SetAccessor.Modifier; }
        }

        public string GetAccessorBody
        {
            get { return CodeProperty.GetAccessor.BodyText; }
        }

        public string SetAccessorBody
        {
            get { return CodeProperty.SetAccessor.BodyText; }
        }

        internal static bool CodePropertyIsValid(string rawString, out string propertyName, out ArchAngel.Providers.CodeProvider.DotNet.Property codeProperty)
        {
            propertyName = "";
            codeProperty = null;
            string rawPropertyCode = string.Format(@"public class Dummy {{{0}}}", rawString);
            string cleanPropertyCode = rawPropertyCode
                .Replace("#entity.Name#", "E_NAME")
                .Replace("#entity.Type#", "E_TYPE");

            try
            {
                var parseResults = ArchAngel.Providers.CodeProvider.ParseResults.ParseCSharpCode(cleanPropertyCode);

                if (parseResults.Classes.Count != 1 || parseResults.Classes[0].Properties.Count != 1)
                    return false;

                codeProperty = parseResults.Classes[0].Properties[0];

                propertyName = codeProperty.Name
                    .Replace("E_NAME", "#entity.Name#")
                    .Replace("E_TYPE", "#entity.Type#");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
