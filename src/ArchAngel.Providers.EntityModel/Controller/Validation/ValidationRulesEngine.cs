using System;
using System.Collections.Generic;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Controller.Validation
{
	public class ValidationRulesEngine
	{
		private readonly MappingSet _mappingSet;
		private readonly List<IValidationRule> _rules = new List<IValidationRule>();

		public ValidationRulesEngine(MappingSet mappingSet)
		{
			_mappingSet = mappingSet;
		}

		public ValidationResults RunAllRules()
		{
			var results = new ValidationResults();

			foreach (var rule in _rules)
			{
				IValidationResult result;
				try
				{
					result = rule.Run(_mappingSet);
				}
				catch (Exception e)
				{
					result = new ExceptionValidationRule(rule, e);
				}

				results.AddResult(result);
			}
			return results;
		}

		public void AddRule(IValidationRule rule)
		{
			foreach (var r in _rules)
			{
				if (r.GetType() == rule.GetType())
					return;
			}
			_rules.Add(rule);
		}

		public void AddModule(ValidationRuleModule module)
		{
			module.Setup(this);
		}
	}
}