using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Rules
{
	public class AllComponentPropertiesHaveADataTypeRule : IValidationRule
	{
		public string Name
		{
			get { return "All Properties have a datatype"; }
		}

		public IValidationResult Run(MappingSet set)
		{
			IValidationResult result = new ValidationResult(this);

			foreach (var componentSpec in set.EntitySet.ComponentSpecifications)
			{
				var properties = componentSpec.Properties;// set.EntitySet.ComponentSpecifications.SelectMany(e => e.Properties);

				foreach (var property in properties)
				{
					if (string.IsNullOrEmpty(property.Type))
						result.Issues.Add(new ValidationIssue(string.Format("Component Property does not have a Type: {0}.{1}", componentSpec.Name, property.Name), property, ValidationErrorLevel.Error));
				}
			}
			return result;
		}
	}
}