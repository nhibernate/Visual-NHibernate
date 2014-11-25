using System.Linq;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Rules
{
	public class AllColumnsHaveADataTypeRule : IValidationRule
	{
		public string Name
		{
			get { return "Check that all Columns have a datatype"; }
		}

		public IValidationResult Run(MappingSet set)
		{
			IValidationResult result = new ValidationResult(this);
			var columns = set.Database.Tables.SelectMany(t => t.Columns);

			foreach (var column in columns)
			{
				if (column.OriginalDataType == null)
					result.Issues.Add(new ValidationIssue("Column does not have a Type", column, ValidationErrorLevel.Error));
			}
			return result;
		}
	}
}