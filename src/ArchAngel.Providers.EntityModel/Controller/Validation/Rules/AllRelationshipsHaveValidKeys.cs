using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Rules
{
	public class AllRelationshipsHaveValidKeys : IValidationRule
	{
		public IValidationResult Run(MappingSet set)
		{
			var result = new ValidationResult(this);

			foreach (var relationship in set.Database.Relationships.Where(r => r.PrimaryKey.Columns.Count != r.ForeignKey.Columns.Count))
			{
				result.Issues.Add(new ValidationIssue(string.Format("Relationship between {0} and {1}: number of columns in primary key and foreign key don't match.", relationship.PrimaryTable.Name, relationship.ForeignTable.Name), relationship, ValidationErrorLevel.Error));
			}
			return result;
		}

		public string Name
		{
			get { return "Relationship Key Rule"; }
		}

	}
}
