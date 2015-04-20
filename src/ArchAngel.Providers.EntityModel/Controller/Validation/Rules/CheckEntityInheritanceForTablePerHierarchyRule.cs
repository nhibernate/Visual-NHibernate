using System.Linq;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Rules
{
	public class CheckEntityInheritanceForTablePerHierarchyRule : IValidationRule
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
			var inheritedEntities = set.EntitySet.Entities.Where(e => e.Parent != null && EntityImpl.DetermineInheritanceTypeWithParent(e) == EntityImpl.InheritanceType.TablePerClassHierarchy);

			foreach (var childEntity in inheritedEntities)
			{
				if (EntityImpl.DetermineInheritanceTypeWithParent(childEntity) == EntityImpl.InheritanceType.TablePerClassHierarchy &&
					string.IsNullOrEmpty(childEntity.DiscriminatorValue))
				{
					result.Issues.Add(new ValidationIssue(string.Format("Entity {0} is missing a discriminator-value (Table Per Hierarchy inheritance)", childEntity.Name), childEntity, ValidationErrorLevel.Error));
				}
			}
			return result;
		}

		public string Name
		{
			get { return "Check Entity Inheritance to make sure it is set up correctly for Table Per Hierarchy mapping."; }
		}
	}
}