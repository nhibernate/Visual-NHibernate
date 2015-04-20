using System;
using System.Collections.Generic;
using ArchAngel.Providers.EntityModel.Controller.Validation.Rules;

namespace ArchAngel.Providers.EntityModel.Controller.Validation
{
	public class ExceptionValidationRule : IValidationResult
	{
		public ExceptionValidationRule(IValidationRule rule, Exception thrownException)
		{
			ThrownException = thrownException;
			Rule = rule;
		}

		public Exception ThrownException { get; private set; }
		public IValidationRule Rule { get; private set; }	

		public bool HasFatals
		{
			get { return true; }
		}

		public bool HasErrors
		{
			get { return false; }
		}

		public bool HasWarnings
		{
			get { return false; }
		}

		public bool HasIssues
		{
			get { return true; }
		}

		public IList<ValidationIssue> Issues
		{
			get { return new List<ValidationIssue>
			             	{
			             		new ValidationIssue(
			             			string.Format("Exception thrown while running rule {0}", Rule.Name), 
			             			null, ValidationErrorLevel.Error)
			             	}; }
		}
	}
}