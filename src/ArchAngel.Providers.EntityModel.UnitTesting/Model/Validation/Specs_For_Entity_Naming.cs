using ArchAngel.Providers.EntityModel.Controller.Validation.Rules;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Validation.Specs_For_Entity_Naming
{
	[TestFixture]
	public class When_Two_Entities_Have_The_Same_Name
	{
		[Test]
		public void The_Rule_Fails()
		{
			var set = new MappingSetImpl();
			set.EntitySet.AddEntity(new EntityImpl("Entity1"));
			var duplicate = new EntityImpl("Entity1");
			set.EntitySet.AddEntity(duplicate);

			EntityNamingRule rule = new EntityNamingRule();
			var result = rule.Run(set);

			Assert.That(result.Issues, Has.Count(1));
			
			var issue = result.Issues[0];
			Assert.That(issue.ErrorLevel, Is.EqualTo(ValidationErrorLevel.Error));
			Assert.That(issue.Object, Is.SameAs(duplicate));
			StringAssert.Contains("Entity1", issue.Description);
		}
	}

	[TestFixture]
	public class When_Two_Properties_On_An_Entity_Have_The_Same_Name
	{
		[Test]
		public void The_Rule_Fails()
		{
			var set = new MappingSetImpl();
			var entity = new EntityImpl("Entity1");
			set.EntitySet.AddEntity(entity);
			entity.AddProperty(new PropertyImpl("Property1"));
			var duplicate = new PropertyImpl("Property1");
			entity.AddProperty(duplicate);

			EntityNamingRule rule = new EntityNamingRule();
			var result = rule.Run(set);

			Assert.That(result.Issues, Has.Count(1));

			var issue = result.Issues[0];
			Assert.That(issue.ErrorLevel, Is.EqualTo(ValidationErrorLevel.Error));
			Assert.That(issue.Object, Is.SameAs(duplicate));
			StringAssert.Contains("Property1", issue.Description);
		}
	}

	[TestFixture]
	public class When_A_Child_Overrides_A_Property
	{
		[Test]
		public void The_Rule_Does_Not_Identify_It_As_A_Duplicate_Property()
		{
			var set = new MappingSetImpl();
			var parent = new EntityImpl("Entity1");
			var child = new EntityImpl("Entity2");
			set.EntitySet.AddEntity(parent);
			set.EntitySet.AddEntity(child);

			var property = new PropertyImpl("Property1");
			parent.AddProperty(property);
			child.Parent = parent;
			child.CopyPropertyFromParent(property);

			EntityNamingRule rule = new EntityNamingRule();
			var result = rule.Run(set);

			Assert.That(result.HasIssues, Is.False);
		}
	}

	[TestFixture]
	public class When_A_Property_And_Reference_End_On_An_Entity_Have_The_Same_Name
	{
		[Test]
		public void The_Rule_Fails()
		{
			var set = new MappingSetImpl();
			var entity1 = new EntityImpl("Entity1");
			var entity2 = new EntityImpl("Entity2");
			set.EntitySet.AddEntity(entity1);
			set.EntitySet.AddEntity(entity2);
			entity1.AddProperty(new PropertyImpl("Property1"));
			var reference = entity1.CreateReferenceTo(entity2);
			reference.End1Name = "Property1";

			EntityNamingRule rule = new EntityNamingRule();
			var result = rule.Run(set);

			Assert.That(result.Issues, Has.Count(1));

			var issue = result.Issues[0];
			Assert.That(issue.ErrorLevel, Is.EqualTo(ValidationErrorLevel.Error));
			Assert.That(issue.Object, Is.SameAs(reference));
			StringAssert.Contains("Property1", issue.Description);
		}
	}

	[TestFixture]
	public class When_An_Entities_Has_A_Blank_Name
	{
		[Test]
		public void The_Rule_Fails()
		{
			var set = new MappingSetImpl();
			var emptyEntity = new EntityImpl("");
			set.EntitySet.AddEntity(emptyEntity);

			EntityNamingRule rule = new EntityNamingRule();
			var result = rule.Run(set);

			Assert.That(result.Issues, Has.Count(1));

			var issue = result.Issues[0];
			Assert.That(issue.ErrorLevel, Is.EqualTo(ValidationErrorLevel.Error));
			Assert.That(issue.Object, Is.SameAs(emptyEntity));
			StringAssert.Contains("name", issue.Description);
		}
	}
}