using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Rules
{
    public class DatabaseNamingRule : IValidationRule
    {
        public IValidationResult Run(MappingSet set)
        {
            var result = new ValidationResult(this);

            CheckForDuplicateNames(result, table => string.Format("{0}.{1}", table.Schema, table.Name), set.Database.Tables, "Table");

            foreach (var table in set.Database.Tables)
            {
                CheckForDuplicateNames(result, c => c.Name, table.Columns, "Column");
                CheckForDuplicateNames(result, k => k.Name, table.Keys, "Key");
            }

            //CheckForDuplicateNames(result, k => k.Name, set.Database.Tables.SelectMany(t => t.Keys), "Key");

            return result;
        }

        private static void CheckForDuplicateNames<T>(ValidationResult result, Func<T, string> getName, IEnumerable<T> collection, string typeName)
            where T : IModelObject
        {
            HashSet<string> names = new HashSet<string>();
            foreach (var item in collection)
            {
                bool isTable = item is Table;
                var name = getName(item);

                ITable parentTable = null;

                if (item is Table)
                    parentTable = (ITable)item;
                else if (item is Key)
                    parentTable = ((Key)(object)item).Parent;
                else if (item is Column)
                    parentTable = ((Column)(object)item).Parent;

                if (string.IsNullOrEmpty(name))
                {
                    result.Issues.Add(new ValidationIssue(string.Format("Missing {0} name on table [{1}]", typeName, parentTable.Name), item, ValidationErrorLevel.Error));
                    continue;
                }
                if (names.Contains(name))
                {
                    if (isTable)
                        result.Issues.Add(new ValidationIssue(string.Format("Duplicate table name [{0}]", name), item, ValidationErrorLevel.Error));
                    else
                        result.Issues.Add(new ValidationIssue(string.Format("Duplicate {0} name [{1}] on table [{2}]", typeName, name, parentTable.Name), item, ValidationErrorLevel.Error));

                    continue;
                }

                names.Add(name);
            }
        }

        public string Name
        {
            get { return "Entity Naming Rule"; }
        }
    }
}