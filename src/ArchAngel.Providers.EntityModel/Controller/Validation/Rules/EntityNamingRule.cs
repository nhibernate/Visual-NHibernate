using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Rules
{
	public class EntityNamingRule : IValidationRule
	{
		public IValidationResult Run(MappingSet set)
		{
			var result = new ValidationResult(this);

			HashSet<string> names = new HashSet<string>();
			foreach (var entity in set.EntitySet.Entities)
			{
				if (string.IsNullOrEmpty(entity.Name))
				{
					result.Issues.Add(new ValidationIssue("Entity objects must have a name.", entity, ValidationErrorLevel.Error));
					continue;
				}

				if (names.Contains(entity.Name))
				{
					result.Issues.Add(new ValidationIssue(string.Format("Duplicate Entity name: {0}", entity.Name), entity, ValidationErrorLevel.Error));
					continue;
				}

				names.Add(entity.Name);
			}
			foreach (var entity in set.EntitySet.Entities)
			{
				//var references = entity.DirectedReferences.Select(r => new KeyValuePair<string, IModelObject>(r.FromName, r.Reference));
				List<KeyValuePair<string, IModelObject>> references = new List<KeyValuePair<string, IModelObject>>();// entity.DirectedReferences.Select(r => new KeyValuePair<string, IModelObject>(r.FromName, r.Reference));

				foreach (var r in entity.DirectedReferences)
				{
					if (r.FromEntity == entity)
					{
						references.Add(new KeyValuePair<string, IModelObject>(r.FromName, r.Reference));

						if (r.ToEntity == entity)
						{
							// This is a self-referencing entity
							if (r.ToName == entity.Name)
								result.Issues.Add(new ValidationIssue(string.Format("Entity {0} has a reference named {0}. Properties can't have the same name as their entity.", entity.Name), entity, ValidationErrorLevel.Error));
						}
					}
					else if (r.ToEntity == entity)
						references.Add(new KeyValuePair<string, IModelObject>(r.ToName, r.Reference));
					else
						throw new System.Exception("WTF!");
				}
				//var properties = entity.Properties.Where(p => p.IsInherited == false).Select(p => new KeyValuePair<string, IModelObject>(p.Name, p));
				var properties = entity.Properties.Where(p => p.IsInherited == false && !entity.ForeignKeyPropertiesToExclude.Contains(p)).Select(p => new KeyValuePair<string, IModelObject>(p.Name, p));
				// Remove properties that are hidden due to a relationship

				HashSet<string> names1 = new HashSet<string>();
				foreach (var objPair in properties.Concat(references))
				{
					string typeName = objPair.Value is Property ? "Property" : "Reference";

					if (string.IsNullOrEmpty(objPair.Key))
					{
						result.Issues.Add(new ValidationIssue(string.Format("{0} objects must have a name.", typeName), objPair.Value, ValidationErrorLevel.Error));
						continue;
					}

					if (names1.Contains(objPair.Key))
					{
						if (objPair.Value is Reference && properties.Any(p => p.Key == objPair.Key))
							result.Issues.Add(new ValidationIssue(string.Format("Reference has the same name as a property on entity [{2}]: {1}", typeName, objPair.Key, entity.Name), objPair.Value, ValidationErrorLevel.Error));
						else
							result.Issues.Add(new ValidationIssue(string.Format("Duplicate {0} name on entity [{2}]: {1}", typeName, objPair.Key, entity.Name), objPair.Value, ValidationErrorLevel.Error));

						continue;
					}
					switch (typeName)
					{
						case "Property":
							if (((Property)objPair.Value).Name == entity.Name)
								result.Issues.Add(new ValidationIssue(string.Format("Entity {0} has a property named {0}. Properties can't have the same name as their entity.", entity.Name), entity, ValidationErrorLevel.Error));
							break;
						case "Reference":
							if (((Reference)objPair.Value).Name == entity.Name)
								result.Issues.Add(new ValidationIssue(string.Format("Entity {0} has a reference named {0}. Properties can't have the same name as their entity.", entity.Name), entity, ValidationErrorLevel.Error));
							break;
						default:
							throw new System.NotImplementedException("Type not handled yet in EntityNamingRuke.Map(): " + typeName);
					}
					names1.Add(objPair.Key);
				}
				// CHeck that all entity properties have unique names
				List<string> propertyNames = new List<string>();

				foreach (var prop in entity.ConcreteProperties)
				{
					if (propertyNames.Contains(prop.Name))
						result.Issues.Add(new ValidationIssue(string.Format("Entity {0} has multiple properties with the same name: {1}.", entity.Name, prop.Name), entity, ValidationErrorLevel.Error));
					else
						propertyNames.Add(prop.Name);
				}

			}
			return result;
		}

		public string Name
		{
			get { return "Entity Naming Rule"; }
		}
	}
}