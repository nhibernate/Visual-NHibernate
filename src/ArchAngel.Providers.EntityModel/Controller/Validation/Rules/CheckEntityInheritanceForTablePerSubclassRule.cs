using System.Linq;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Rules
{
	public class CheckEntityInheritanceForTablePerSubclassRule : IValidationRule
	{
		public IValidationResult Run(MappingSet set)
		{
			ValidationResult result = new ValidationResult(this);

			if (set.EntitySet == null)
			{
				result.Issues.Add(new ValidationIssue("No Entities are present in the Entity Model", set, ValidationErrorLevel.Warning));
				return result;
			}
			if (set.EntitySet.IsEmpty)
			{
				result.Issues.Add(new ValidationIssue("No Entities are present in the Entity Model", set.EntitySet, ValidationErrorLevel.Warning));
				return result;
			}
			var inheritedEntities = set.EntitySet.Entities.Where(e => e.Parent != null);

			//foreach (var childEntity in inheritedEntities)
			//{
			//    var parent = childEntity.Parent;

			//    foreach (Property property in parent.Key.Properties.Distinct(p => p.Name))
			//        if (childEntity.ConcreteProperties.Any(p => p.Name == property.Name) == false && EntityImpl.DetermineInheritanceTypeWithParent(childEntity) != EntityImpl.InheritanceType.TablePerClassHierarchy)
			//            result.Issues.Add(new ValidationIssue(string.Format("Key Property {0} is missing from child Entity named {1}", property.Name, childEntity.Name), childEntity, ValidationErrorLevel.Error));
			//}
			return result;
		}

		public string Name
		{
			get { return "Check Entity Inheritance to make sure it is set up correctly for Table Per Subclass mapping."; }
		}
	}

	public enum ValidationErrorLevel { Warning, Error, Fatal }
}