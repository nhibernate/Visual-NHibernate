using System;
using System.Linq;
using ArchAngel.Providers.EntityModel.Controller.Validation;
using ArchAngel.Providers.EntityModel.Controller.Validation.Rules;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.NHibernateHelper.Validation
{
	// TODO: This rule needs to be added to the template, not here in the code, because it
	// accesses user options which the code doesn't know about.
	public class CheckReferenceCollectionTypeRule : IValidationRule
	{
		public string Name
		{
			get { return "All many-to-many references must have a MappedTable. All other references must have a mapped relationship."; }
		}

		public IValidationResult Run(MappingSet set)
		{
			IValidationResult result = new ValidationResult(this);

			foreach (var reference in set.EntitySet.Entities.SelectMany(e => e.References).Distinct())
			{
				var mappedTable = reference.MappedTable();

				if (reference.Cardinality1 == ArchAngel.Interfaces.Cardinality.Many &&
					reference.Cardinality2 == ArchAngel.Interfaces.Cardinality.Many)
				{
					if (mappedTable == null)
						result.Issues.Add(new ValidationIssue(string.Format("Reference [{0} to {1}] with many-to-many collection type is missing an Association Table.", reference.Entity1.Name, reference.Entity2.Name), reference, ValidationErrorLevel.Error));
					else
					{
						// Check that columns exist and match for the mapped table.
						var primaryTable1 = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.EntityMapper.GetPrimaryTable(reference.Entity1);
						var primaryTable2 = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.EntityMapper.GetPrimaryTable(reference.Entity2);

						if (mappedTable.Relationships.Where(t => t.PrimaryTable == primaryTable1).Count() == 0)
							result.Issues.Add(new ValidationIssue(string.Format("Reference [{0} to {1}] with many-to-many collection type is missing an Association Table.", reference.Entity1.Name, reference.Entity2.Name), reference, ValidationErrorLevel.Error));

						if (mappedTable.Relationships.Where(t => t.PrimaryTable == primaryTable2).Count() == 0)
							result.Issues.Add(new ValidationIssue(string.Format("Reference [{0} to {1}] with many-to-many collection type is missing an Association Table.", reference.Entity1.Name, reference.Entity2.Name), reference, ValidationErrorLevel.Error));
					}
				}
				else if (reference.MappedRelationship() == null && mappedTable == null)
					result.Issues.Add(new ValidationIssue(string.Format("Reference [{0} to {1}] is missing a Mapped Relationship or Association Table.", reference.Entity1.Name, reference.Entity2.Name), reference, ValidationErrorLevel.Error));
			}
			foreach (var directedReference in set.EntitySet.Entities.SelectMany(e => e.DirectedReferences).Distinct())
			{
				var collectionType = (ArchAngel.Interfaces.Scripting.NHibernate.Model.CollectionTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.CollectionTypes), NHCollections.GetAssociationType(directedReference).ToString(), true);

				if (collectionType == ArchAngel.Interfaces.Scripting.NHibernate.Model.CollectionTypes.Map ||
					collectionType == ArchAngel.Interfaces.Scripting.NHibernate.Model.CollectionTypes.IDBag ||
					collectionType == ArchAngel.Interfaces.Scripting.NHibernate.Model.CollectionTypes.List)
				{
					var indexColumn = NHCollections.GetIndexColumn(directedReference, ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.EntityMapper.GetPrimaryTable(directedReference.ToEntity));

					if (indexColumn == null)
					{
						result.Issues.Add(new ValidationIssue(string.Format("'{0}' collection [{1} to {2}] has an invalid index column specified.",
							collectionType.ToString(),
							directedReference.FromEntity.Name,
							directedReference.ToEntity.Name), directedReference.Reference, ValidationErrorLevel.Error));
					}
					//newReference.CollectionIndexColumn = columnLookups[NHCollections.GetIndexColumn(directedReference, ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.EntityMapper.GetPrimaryTable(directedReference.FromEntity))];
				}
				//else if (collectionType == Interfaces.Scripting.NHibernate.Model.CollectionTypes.None &&
				//    directedReference.FromEndCardinality == ArchAngel.Interfaces.Cardinality.Many)
				//{
				//    result.Issues.Add(new ValidationIssue(string.Format("No collection type has been set for the reference between {1} and {2}.",
				//        collectionType.ToString(),
				//        directedReference.FromEntity.Name,
				//        directedReference.ToEntity.Name), directedReference.Reference, ValidationErrorLevel.Error));
				//}
			}
			return result;
		}
	}
}
