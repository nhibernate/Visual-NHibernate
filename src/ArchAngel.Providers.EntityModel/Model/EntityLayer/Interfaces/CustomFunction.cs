using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
    public class CustomFunction
    {
        private bool _AutoAddToEntities = true;
        public string Name;
        private string _UserString;
        private ArchAngel.Providers.CodeProvider.DotNet.Function _CodeFunction;

        public CustomFunction(string name, string userString)
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
                    _CodeFunction = null;
                }
            }
        }

        public ArchAngel.Providers.CodeProvider.DotNet.Function CodeFunction
        {
            get
            {
                if (_CodeFunction == null && !string.IsNullOrEmpty(UserString))
                {
                    string tempName;

                    if (!CodeMethodIsValid(UserString, out tempName, out _CodeFunction))
                        throw new Exception("The code is not valid. It has an error.");

                    Name = tempName;
                }
                return _CodeFunction;
            }
            set { _CodeFunction = value; }
        }

        public bool AutoAddToEntities
        {
            get { return _AutoAddToEntities; }
            set { _AutoAddToEntities = value; }
        }

        public string GetNameFor(Entity entity)
        {
            return CodeFunction.Name.Replace("#entity.Name#", entity.Name);
        }

        public string ToStringFor(Entity entity, int tabs)
        {
            return UserString.Replace("#entity.Name#", entity.Name).Replace("\n", "\n" + new string('\t', tabs));
        }

        internal static bool CodeMethodIsValid(string rawString, out string methodName, out ArchAngel.Providers.CodeProvider.DotNet.Function codeMethod)
        {
            methodName = "";
            codeMethod = null;
            string rawMethodCode = string.Format(@"public class Dummy {{{0}}}", rawString);
            string cleanMethodCode = rawMethodCode
                .Replace("#entity.Name#", "E_NAME")
                .Replace("#entity.Type#", "E_TYPE");

            try
            {
                var parseResults = ArchAngel.Providers.CodeProvider.ParseResults.ParseCSharpCode(cleanMethodCode);

                if (parseResults.Classes.Count != 1 || parseResults.Classes[0].Functions.Count != 1)
                    return false;

                codeMethod = parseResults.Classes[0].Functions[0];

                methodName = codeMethod.Name
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
