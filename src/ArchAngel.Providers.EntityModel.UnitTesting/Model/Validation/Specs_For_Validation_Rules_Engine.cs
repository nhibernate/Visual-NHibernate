using System.Linq;
using ArchAngel.Providers.EntityModel.Controller.Validation;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using Rhino.Mocks;

namespace Validation.Specs_For_Validation_Rules_Engine
{
	[TestFixture]
	public class When_Running_The_Rules_Engine
	{
		private ValidationRulesEngine engine;
		private IValidationRule rule1;
		private IValidationRule rule2;
		private IValidationResult result1;
		private IValidationResult result2;
		private MappingSetImpl mappingSet;

		[SetUp]
		public void SetUp()
		{
			mappingSet = new MappingSetImpl();
			engine = new ValidationRulesEngine(mappingSet);

			rule1 = MockRepository.GenerateMock<IValidationRule>();
			rule2 = MockRepository.GenerateMock<IValidationRule>();
			result1 = new ValidationResult(rule1);
			result2 = new ValidationResult(rule2);

			rule1.Stub(r => r.Run(mappingSet)).Return(result1);
			rule2.Stub(r => r.Run(mappingSet)).Return(result2);

			engine.AddRule(rule1);
			engine.AddRule(rule2);
		}

		[Test]
		public void It_Runs_Each_Of_The_Rules()
		{
			engine.RunAllRules();

			rule1.AssertWasCalled(r => r.Run(mappingSet));
			rule2.AssertWasCalled(r => r.Run(mappingSet));
		}

		[Test]
		public void It_Stores_Each_Of_The_Results()
		{
			var results = engine.RunAllRules();

			Assert.That(results.Contains(result1));
			Assert.That(results.Contains(result2));
		}
	}
}