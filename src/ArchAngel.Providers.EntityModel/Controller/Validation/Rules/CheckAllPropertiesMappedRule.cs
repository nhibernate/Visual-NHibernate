using System.Linq;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Rules
{
	public class CheckAllPropertiesMappedRule : IValidationRule
	{
		public IValidationResult Run(MappingSet set)
		{
			var result = new ValidationResult(this);

			var properties = set.EntitySet.Entities.SelectMany(e => e.ConcreteProperties);

			foreach (var property in properties)
			{
				if (!property.Entity.IsAbstract && property.MappedColumn() == null)
					result.Issues.Add(new ValidationIssue(string.Format("Property {0} on Entity {1} has not been mapped.", property.Name, property.Entity.Name), property, ValidationErrorLevel.Warning));
			}

			return result;
		}

		public string Name
		{
			get { return "Check all Properties are mapped to a Column"; }
		}
	}
}