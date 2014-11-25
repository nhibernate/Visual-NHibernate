using ArchAngel.Providers.EntityModel.Controller.Validation;
using ArchAngel.Providers.EntityModel.Controller.Validation.Rules;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.NHibernateHelper.Validation
{
	public class NHibernateProjectLoaderModule : ValidationRuleModule
	{
		private readonly IDatabase _database;

		public NHibernateProjectLoaderModule(IDatabase realDatabase)
		{
			_database = realDatabase;
		}

		public override void Setup(ValidationRulesEngine engine)
		{
			engine.AddRule(new EntityNamingRule());
			engine.AddRule(new DatabaseNamingRule());
			engine.AddRule(new AllColumnsHaveADataTypeRule());
			engine.AddRule(new AllPropertiesHaveADataTypeRule());
			engine.AddRule(new AllComponentPropertiesHaveADataTypeRule());
			engine.AddRule(new CheckEntityComponentsRule());
			engine.AddRule(new CheckSchemaAgainstRealDatabaseRule(_database));
			engine.AddRule(new CheckReferenceCollectionTypeRule());
			engine.AddRule(new CheckLazyPropertiesHaveLazyEntityRule());
			engine.AddRule(new CheckEntityInheritanceForTablePerConcreteClassRule());
			engine.AddRule(new CheckEntityInheritanceForTablePerHierarchyRule());
			engine.AddRule(new CheckEntityInheritanceForTablePerSubclassRule());

		}
	}
}