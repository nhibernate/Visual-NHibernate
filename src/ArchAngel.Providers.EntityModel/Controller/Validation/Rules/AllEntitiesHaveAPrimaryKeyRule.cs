using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Rules
{
	public class AllEntitiesHaveAPrimaryKeyRule : IValidationRule
	{
		public IValidationResult Run(MappingSet set)
		{
			var result = new ValidationResult(this);

			HashSet<string> names = new HashSet<string>();
			foreach (var entity in set.EntitySet.Entities)
			{
				if (entity.PrimaryKeyColumns().Count == 0 &&
					entity.MappedTables().Count() > 0)
					result.Issues.Add(new ValidationIssue(string.Format("Entity [{0}] has no columns in a primary key.", entity.Name), entity, ValidationErrorLevel.Error));
			}
			return result;
		}

		public string Name
		{
			get { return "Entity Primary Key Rule"; }
		}
	}
}