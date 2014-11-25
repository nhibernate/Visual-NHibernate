using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Controller.Validation.Rules;
using ArchAngel.Providers.EntityModel.Model;

namespace ArchAngel.Providers.EntityModel.Controller.Validation
{
	public class ValidationResult : IValidationResult
	{
		public ValidationResult(IValidationRule rule)
		{
			Rule = rule;
			Issues = new List<ValidationIssue>();
		}

		public IValidationRule Rule
		{
			get;
			private set;
		}

		public bool HasFatals
		{
			get { return Issues.Where(i => i.ErrorLevel == ValidationErrorLevel.Fatal).Any(); }
		}

		public bool HasErrors
		{
			get { return Issues.Where(i => i.ErrorLevel == ValidationErrorLevel.Error).Any(); }
		}

		public bool HasWarnings
		{
			get { return Issues.Where(i => i.ErrorLevel == ValidationErrorLevel.Warning).Any(); }
		}

		public bool HasIssues
		{
			get { return Issues.Any(); }
		}

		public IList<ValidationIssue> Issues { get; private set; }
	}

	public class ValidationIssue
	{
		public ValidationIssue(string description, IModelObject @object, ValidationErrorLevel errorLevel)
		{
			Description = description;
			Object = @object;
			ErrorLevel = errorLevel;
		}

		public string Description { get; private set; }
		public IModelObject Object { get; private set; }
		public ValidationErrorLevel ErrorLevel { get; private set; }
	}

	public class ValidationResults : IEnumerable<IValidationResult>
	{
		private readonly List<IValidationResult> results = new List<IValidationResult>();

		public void AddResult(IValidationResult result)
		{
			results.Add(result);
		}

		public bool HasAnyIssues { get { return results.Any(r => r.Issues.Any()); } }
		public bool HasAnyErrorsOrWorse { get { return results.Any(r => r.HasErrors || r.HasFatals); } }

		public IEnumerator<IValidationResult> GetEnumerator()
		{
			return results.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return results.GetEnumerator();
		}
	}
}