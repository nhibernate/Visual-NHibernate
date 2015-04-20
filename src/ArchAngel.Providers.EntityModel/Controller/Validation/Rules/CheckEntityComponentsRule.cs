using System.Linq;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Rules
{
	public class CheckEntityComponentsRule : IValidationRule
	{
		public string Name
		{
			get { return "All Entity Components have a properties that are mapped."; }
		}

		public IValidationResult Run(MappingSet set)
		{
			IValidationResult result = new ValidationResult(this);

			foreach (Entity entity in set.EntitySet.Entities)
				foreach (var component in entity.Components)
					foreach (var prop in component.Properties.Where(p => p.MappedColumn() == null))
						result.Issues.Add(new ValidationIssue(string.Format("Property [{0}] on Component [{1}] on Entity [{2}] is not mapped.", prop.PropertyName, component.Name, entity.Name), prop, ValidationErrorLevel.Error));

			return result;
		}
	}
}