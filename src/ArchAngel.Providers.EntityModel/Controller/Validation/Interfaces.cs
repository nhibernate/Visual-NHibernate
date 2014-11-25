using System.Collections.Generic;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation
{
	public interface IValidationRule
	{
		string Name { get; }
		IValidationResult Run(MappingSet set);
	}

	public interface IValidationResult
	{
		IValidationRule Rule { get; }
		bool HasFatals { get; }
		bool HasErrors { get; }
		bool HasWarnings { get; }
		bool HasIssues { get; }
		IList<ValidationIssue> Issues { get; }
	}

	public abstract class ValidationRuleModule
	{
		public abstract void Setup(ValidationRulesEngine engine);
	}

    public interface IReturnValdationRules
    {
        IList<ArchAngel.Providers.EntityModel.Controller.Validation.IValidationRule> ValidationRules { get; }
    }
}