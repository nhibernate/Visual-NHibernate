using System.Linq;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Controller.Validation;
using ArchAngel.Providers.EntityModel.Controller.Validation.Rules;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.NHibernateHelper.Validation
{
	public class CheckSchemaAgainstRealDatabaseRule : IValidationRule
	{
		private IDatabase _realDatabase;

		public CheckSchemaAgainstRealDatabaseRule(IDatabase realDatabase)
		{
			_realDatabase = realDatabase;
		}

		public string Name
		{
			get { return "Checks the given Database Schema against the real Database"; }
		}

		public IValidationResult Run(MappingSet set)
		{
			IValidationResult result = new ValidationResult(this);

			DatabaseProcessor proc = new DatabaseProcessor();
			var mergeResults = proc.MergeDatabases(_realDatabase, set.Database);

			if (mergeResults.AnyChanges)
			{
				foreach (TableAdditionOperation tableOp in mergeResults.TableOperations.OfType<TableAdditionOperation>())
				{
					// Additions show things that are in the generated database schema and not in the real one.
					result.Issues.Add(new ValidationIssue("Table exists in your NHibernate mapping files but not in your database",
														  tableOp.Object, ValidationErrorLevel.Warning));
				}

				foreach (ColumnAdditionOperation columnOp in mergeResults.ColumnOperations.OfType<ColumnAdditionOperation>())
				{
					result.Issues.Add(new ValidationIssue("Column exists in your NHibernate mapping files but not in your database",
														  columnOp.Object, ValidationErrorLevel.Warning));
				}
			}

			return result;
		}
	}
}