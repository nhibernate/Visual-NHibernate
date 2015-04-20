using System.Linq;
using ArchAngel.NHibernateHelper.EntityExtensions;
using ArchAngel.Providers.EntityModel.Controller.Validation;
using ArchAngel.Providers.EntityModel.Controller.Validation.Rules;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.NHibernateHelper.Validation
{
	// TODO: This rule needs to be added to the template, not here in the code, because it
	// accesses user options which the code doesn't know about.
	public class CheckLazyPropertiesHaveLazyEntityRule : IValidationRule
	{
		public string Name
		{
			get { return "All lazy properties must have a lazy entity."; }
		}

		public IValidationResult Run(MappingSet set)
		{
			IValidationResult result = new ValidationResult(this);

			bool projectDefaultLazyIsTrue = ArchAngel.Interfaces.SharedData.CurrentProject.GetProjectDefaultLazy();

			if (projectDefaultLazyIsTrue)
				foreach (var entity in set.EntitySet.Entities.Where(e => e.GetEntityLazy() == Interfaces.NHibernateEnums.EntityLazyTypes.@false))
					foreach (var property in entity.Properties.Where(p => p.GetPropertyIsLazy()))
						result.Issues.Add(new ValidationIssue(string.Format("Property [{0}.{1}] is Lazy, but the entity is not Lazy-load.", entity.Name, property.Name), entity, ValidationErrorLevel.Error));
			else
				foreach (var entity in set.EntitySet.Entities.Where(e => e.GetEntityLazy() == Interfaces.NHibernateEnums.EntityLazyTypes.@false || e.GetEntityLazy() == Interfaces.NHibernateEnums.EntityLazyTypes.inherit_default))
					foreach (var property in entity.Properties.Where(p => p.GetPropertyIsLazy()))
						result.Issues.Add(new ValidationIssue(string.Format("Property [{0}.{1}] is Lazy, but the entity is not Lazy-load.", entity.Name, property.Name), entity, ValidationErrorLevel.Error));

			return result;
		}
	}
}