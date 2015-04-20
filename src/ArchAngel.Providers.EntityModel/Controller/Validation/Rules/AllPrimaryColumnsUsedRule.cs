using System.Linq;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Rules
{
	public class AllPrimaryColumnsUsedRule : IValidationRule
	{
		public IValidationResult Run(MappingSet set)
		{
			var result = new ValidationResult(this);

			foreach (var entity in set.EntitySet.Entities)
			{
				var mappedTables = entity.MappedTables();

				var primaryColumns = mappedTables.SelectMany(t => t.ColumnsInPrimaryKey);
				var propertyMappedColumns = entity.Properties.Concat(entity.PropertiesHiddenByAbstractParent).Select(p => p.MappedColumn()).Where(c => c != null);

				// This gets all of the component properties, and selects the non null mapped components.
				var componentMappedColumns =
					entity.Components.SelectMany(c => c.Properties).Select(p => p.MappedColumn()).Where(c => c != null);

				var mappedColumns = propertyMappedColumns.Concat(componentMappedColumns).ToLookup(c => c.InternalIdentifier);

				// Only one of the MappedTables needs their primary key columns mapped, because the other MappedTables
				// should have relationships with the primary mappedTable.
				bool oneFound = mappedTables.Count() == 0;

				foreach (var mappedTable in mappedTables)
				{
					bool allPrimaryKeyColumnsFound = true;

					foreach (var primaryColumn in mappedTable.ColumnsInPrimaryKey)// primaryColumns)
					{
						if (mappedColumns.Contains(primaryColumn.InternalIdentifier) == false)
						{
							allPrimaryKeyColumnsFound = false;
							break;
						}
					}
					if (allPrimaryKeyColumnsFound)
					{
						oneFound = true;
						break;
					}
				}
				if (!oneFound)
				{
					EntityImpl.InheritanceType inheritanceType = EntityImpl.DetermineInheritanceTypeWithParent(entity);

					if (inheritanceType == EntityImpl.InheritanceType.TablePerSubClass ||
						inheritanceType == EntityImpl.InheritanceType.TablePerConcreteClass)
					{
						if (mappedTables.Count() == 1)
						{
							Model.DatabaseLayer.ITable table = mappedTables.ElementAt(0);

							if (table.FirstPrimaryKey != null)
							{
								// All ok
								continue;
							}
						}
					}
					string tables = "";

					foreach (var name in mappedTables.Select(t => t.Name))
						tables += name + ", ";

					tables = tables.TrimEnd(',', ' ');

					result.Issues.Add(new ValidationIssue(
										string.Format("Entity {0} is mapped to Tables [{1}] but doesn't have a property mapped to any primary key columns.", entity.Name, tables),
										entity, ValidationErrorLevel.Error));
				}
			}
			return result;
		}

		public string Name
		{
			get { return "Check that all primary columns on the mapped tables are mapped correctly"; }
		}
	}
}