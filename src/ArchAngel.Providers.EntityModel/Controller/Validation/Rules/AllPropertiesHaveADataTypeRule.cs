using System.Linq;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Rules
{
	public class AllPropertiesHaveADataTypeRule : IValidationRule
	{
		public string Name
		{
			get { return "All Properties have a datatype"; }
		}

		public IValidationResult Run(MappingSet set)
		{
			IValidationResult result = new ValidationResult(this);

			foreach (Entity entity in set.EntitySet.Entities)
			{
				foreach (var property in entity.Properties.Except(entity.ForeignKeyPropertiesToExclude))
				{
					if (string.IsNullOrEmpty(property.Type))
						result.Issues.Add(new ValidationIssue(string.Format("Property does not have a Type: {0}.{1}", property.Entity.Name, property.Name), property, ValidationErrorLevel.Error));
				}
			}
			return result;
		}
	}
}