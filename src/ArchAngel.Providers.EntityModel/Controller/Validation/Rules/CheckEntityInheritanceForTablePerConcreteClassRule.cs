using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Rules
{
	public class CheckEntityInheritanceForTablePerConcreteClassRule : IValidationRule
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
			bool keyPropertiesFound = true;
			HashSet<Entity> invalidParents = new HashSet<Entity>();

			foreach (var childEntity in inheritedEntities)
			{
				if (!childEntity.Parent.IsAbstract)
					continue;

				var parent = childEntity.Parent;
				// Check that all key properties in children are in the parent

				foreach (Property property in childEntity.Key.Properties)
				{
					if (!parent.Properties.Any(p => p.Name == property.Name))
					{
						keyPropertiesFound = false;
						break;
					}
				}
				if (!keyPropertiesFound)
					invalidParents.Add(parent);
			}
			foreach (Entity invalidParent in invalidParents)
				result.Issues.Add(new ValidationIssue(string.Format("No key properties in parent Entity [{0}] (Table Per Concrete Class inheritance).", invalidParent.Name), invalidParent, ValidationErrorLevel.Error));

			// Check that all the children have properties matching the names of all the parent's properties.
			foreach (Entity parent in set.EntitySet.Entities.Where(e => e.IsAbstract == true && e.Children.Count > 0))
			{
				List<Property> parentProperties = parent.Properties.ToList();

				foreach (Entity child in parent.Children)
				{
					if (!child.IsAbstract && child.MappedTables().Count() != 1)
						result.Issues.Add(new ValidationIssue(string.Format("Child [{0}] doesn't have exactly 1 mapped table (Table Per Concrete Class inheritance).", child.Name), child, ValidationErrorLevel.Error));

					foreach (Property prop in parentProperties)
						if (!child.Properties.Any(p => p.Name == prop.Name))
							result.Issues.Add(new ValidationIssue(string.Format("Child entity [{0}] missing property named [{1}] of parent (Table Per Concrete Class inheritance).", child.Name, prop.Name), child, ValidationErrorLevel.Error));
				}
			}
			return result;
		}

		public string Name
		{
			get { return "Check Entity Inheritance to make sure it is set up correctly for Table Per Concrete Class mapping."; }
		}
	}
}