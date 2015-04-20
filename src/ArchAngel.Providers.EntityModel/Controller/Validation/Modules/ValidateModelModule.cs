using ArchAngel.Providers.EntityModel.Controller.Validation.Rules;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Modules
{
	public class ValidateModelModule : ValidationRuleModule
	{
		public override void Setup(ValidationRulesEngine engine)
		{
			engine.AddRule(new EntityNamingRule());
			engine.AddRule(new DatabaseNamingRule());
			engine.AddRule(new CheckAllPropertiesMappedRule());
			engine.AddRule(new AllPrimaryColumnsUsedRule());
			engine.AddRule(new AllPropertiesHaveADataTypeRule());
			engine.AddRule(new AllColumnsHaveADataTypeRule());
			engine.AddRule(new AllComponentPropertiesHaveADataTypeRule());
			engine.AddRule(new AllEntitiesHaveAPrimaryKeyRule());
			engine.AddRule(new CheckEntityComponentsRule());
			engine.AddRule(new CheckEntityInheritanceForTablePerConcreteClassRule());
			engine.AddRule(new CheckEntityInheritanceForTablePerHierarchyRule());
			engine.AddRule(new CheckEntityInheritanceForTablePerSubclassRule());
			engine.AddRule(new AllReferencesHaveBackingDataRule());
			engine.AddRule(new AllRelationshipsHaveValidKeys());
			engine.AddRule(new CheckInheritanceClassesAreNotRelated());
		}
	}
}