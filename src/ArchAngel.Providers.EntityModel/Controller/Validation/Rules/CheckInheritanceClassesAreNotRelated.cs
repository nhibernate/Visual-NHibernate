using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Rules
{
    public class CheckInheritanceClassesAreNotRelated : IValidationRule
    {
        public IValidationResult Run(MappingSet set)
        {
            var result = new ValidationResult(this);

            HashSet<string> names = new HashSet<string>();

            foreach (var entity in set.EntitySet.Entities)
            {
                foreach (var child in entity.Children)
                {
                    if (entity.DirectedReferences.Count(d => d.ToEntity == child) > 0)
                        result.Issues.Add(new ValidationIssue(string.Format("Entity [{0}] references a child entity (inheritance): [{1}]. Remove the reference.", entity.Name, child.Name), entity, ValidationErrorLevel.Error));
                }
            }
            return result;
        }

        public string Name
        {
            get { return "Inheritance Objects Not Allowed References Rule"; }
        }
    }
}
