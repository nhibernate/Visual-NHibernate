using ArchAngel.Providers.EntityModel.Controller.Validation.Rules;

namespace ArchAngel.Providers.EntityModel.Controller.Validation.Modules
{
	public class OneToOneDatabaseMappingModule : ValidationRuleModule
	{
		public override void Setup(ValidationRulesEngine engine)
		{
			engine.AddRule(new EntityNamingRule());
			engine.AddRule(new CheckEntityInheritanceForTablePerSubclassRule());
			engine.AddRule(new CheckEntityInheritanceForTablePerConcreteClassRule());
			engine.AddRule(new CheckEntityInheritanceForTablePerHierarchyRule());
			engine.AddRule(new AllPropertiesHaveADataTypeRule());
		}
	}
}