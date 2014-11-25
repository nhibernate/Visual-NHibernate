using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Rules
{
	public class AllReferencesHaveBackingDataRule : IValidationRule
	{
		public IValidationResult Run(MappingSet set)
		{
			var result = new ValidationResult(this);

			foreach (ReferenceImpl reference in set.EntitySet.References)
			{
				var mappedRelationship = reference.MappedRelationship();
				var mappedTable = reference.MappedTable();

				if (mappedRelationship == null && mappedTable != null && !reference.PossibleMappedTables().Contains(mappedTable))
					result.Issues.Add(new ValidationIssue(string.Format("No backing relationship or association-table for reference between [{0}] and [{1}].", reference.Entity1.Name, reference.Entity2.Name), reference, ValidationErrorLevel.Error));

				if (mappedRelationship != null)
				{
					if (mappedRelationship.ForeignKey.Columns.Count == 0)
						result.Issues.Add(new ValidationIssue(string.Format("Foreign key has no columns for reference between [{0}] and [{1}].", reference.Entity1.Name, reference.Entity2.Name), reference, ValidationErrorLevel.Error));

					if (mappedRelationship.PrimaryKey.Columns.Count == 0)
						result.Issues.Add(new ValidationIssue(string.Format("Primary key has no columns for reference between [{0}] and [{1}].", reference.Entity1.Name, reference.Entity2.Name), reference, ValidationErrorLevel.Error));
				}

				//if (reference.FromRelationship == null && reference.ToReference.MappedTable() == null)
				//{
				//    result.Issues.Add(new ValidationIssue(string.Format("No backing relationship or association-table for reference between [{0}] and [{1}].", reference.ToReference.End1Name, reference.ToReference.End2Name), reference.ToReference, ValidationErrorLevel.Error));
				//}
			}
			//HashSet<string> names = new HashSet<string>();
			//foreach (var entity in set.EntitySet.Entities)
			//{
			//    if (string.IsNullOrEmpty(entity.Name))
			//    {
			//        result.Issues.Add(new ValidationIssue("Entity objects must have a name.", entity, ValidationErrorLevel.Error));
			//        continue;
			//    }

			//    if (names.Contains(entity.Name))
			//    {
			//        result.Issues.Add(new ValidationIssue(string.Format("Duplicate Entity name: {0}", entity.Name), entity, ValidationErrorLevel.Error));
			//        continue;
			//    }

			//    names.Add(entity.Name);
			//}

			//foreach (var entity in set.EntitySet.Entities)
			//{
			//    var properties = entity.Properties.Where(p => p.IsInherited == false).Select(p => new KeyValuePair<string, IModelObject>(p.Name, p));
			//    var references = entity.DirectedReferences.Select(r => new KeyValuePair<string, IModelObject>(r.FromName, r.Reference));

			//    HashSet<string> names1 = new HashSet<string>();
			//    foreach (var objPair in properties.Concat(references))
			//    {
			//        string typeName = objPair.Value is Property ? "Property" : "Reference";

			//        if (string.IsNullOrEmpty(objPair.Key))
			//        {
			//            result.Issues.Add(new ValidationIssue(string.Format("{0} objects must have a name.", typeName), objPair.Value, ValidationErrorLevel.Error));
			//            continue;
			//        }

			//        if (names1.Contains(objPair.Key))
			//        {
			//            result.Issues.Add(new ValidationIssue(string.Format("Duplicate {0} name on entity [{2}]: {1}", typeName, objPair.Key, entity.Name), objPair.Value, ValidationErrorLevel.Error));
			//            continue;
			//        }

			//        names1.Add(objPair.Key);
			//    }
			//}

			return result;
		}

		public string Name
		{
			get { return "Reference Backing Data Rule"; }
		}
	}
}