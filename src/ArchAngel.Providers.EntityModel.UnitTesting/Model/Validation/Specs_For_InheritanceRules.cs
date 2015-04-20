using ArchAngel.Providers.EntityModel.Controller.Validation.Rules;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Validation.Specs_For_Inheritance_Rules
{
	[TestFixture]
	public class When_The_Entity_Set_Has_No_Entities
	{
		[Test]
		public void The_Rule_Fails()
		{
			var mappingSet = new MappingSetImpl();
			var rule = new CheckEntityInheritanceForTablePerSubclassRule();
			var result = rule.Run(mappingSet);

			Assert.That(result.HasWarnings, Is.True);
			Assert.That(result.Issues[0].ErrorLevel, Is.EqualTo(ValidationErrorLevel.Warning));
		}
	}

	[TestFixture]
	public class When_The_Entity_Set_Has_No_Inheritance
	{
		[Test]
		public void The_Rule_Passes()
		{
			var mappingSet = new MappingSetImpl();
			mappingSet.EntitySet.AddEntity(new EntityImpl("Entity1"));
			var rule = new CheckEntityInheritanceForTablePerSubclassRule();
			var result = rule.Run(mappingSet);

			Assert.That(result.HasIssues, Is.False);
		}
	}

	[TestFixture]
	public class When_The_Entity_Set_Has_Correct_Inheritance
	{
		[Test]
		public void The_Rule_Passes()
		{
			var mappingSet = new MappingSetImpl();
			var parent = new EntityImpl("Parent");
			var child = new EntityImpl("Child");

			child.Parent = parent;
			var idProperty = new PropertyImpl("ID") { IsKeyProperty = true };
			parent.AddProperty(idProperty);
			child.CopyPropertyFromParent(idProperty);

			mappingSet.EntitySet.AddEntity(parent);
			mappingSet.EntitySet.AddEntity(child);


			var rule = new CheckEntityInheritanceForTablePerSubclassRule();
			var result = rule.Run(mappingSet);

			Assert.That(result.HasIssues, Is.False);
		}
	}

	[TestFixture]
	public class When_The_Child_Entity_Is_Missing_The_Parents_ID_Property
	{
		[Test]
		public void The_Rule_Fails()
		{
			var mappingSet = new MappingSetImpl();
			var parent = new EntityImpl("Parent");
			var child = new EntityImpl("Child");

			child.Parent = parent;
			var idProperty = new PropertyImpl("ID") { IsKeyProperty = true };
			parent.AddProperty(idProperty);

			mappingSet.EntitySet.AddEntity(parent);
			mappingSet.EntitySet.AddEntity(child);

			var rule = new CheckEntityInheritanceForTablePerSubclassRule();
			var result = rule.Run(mappingSet);

			Assert.That(result.Issues, Has.Count(1));
			
			var issue = result.Issues[0];
			Assert.That(issue.Object, Is.SameAs(child));
			Assert.That(issue.ErrorLevel, Is.EqualTo(ValidationErrorLevel.Error));
			StringAssert.Contains("ID", issue.Description);
		}
	}

	[TestFixture]
	public class When_The_Grandchild_Entity_Is_Missing_Its_Grandparents_ID_Property_But_Its_Parent_Isnt
	{
		[Test]
		public void The_Rule_Fails()
		{
			var mappingSet = new MappingSetImpl();
			var grandparent = new EntityImpl("Grandparent");
			var parent = new EntityImpl("Parent");
			var child = new EntityImpl("Child");

			parent.Parent = grandparent;
			child.Parent = parent;
			var idProperty = new PropertyImpl("ID") { IsKeyProperty = true };
			grandparent.AddProperty(idProperty);
			parent.CopyPropertyFromParent(idProperty);

			mappingSet.EntitySet.AddEntity(grandparent);
			mappingSet.EntitySet.AddEntity(parent);
			mappingSet.EntitySet.AddEntity(child);

			var rule = new CheckEntityInheritanceForTablePerSubclassRule();
			var result = rule.Run(mappingSet);

			Assert.That(result.Issues, Has.Count(1));

			var issue = result.Issues[0];
			Assert.That(issue.Object, Is.SameAs(child));
			Assert.That(issue.ErrorLevel, Is.EqualTo(ValidationErrorLevel.Error));
			StringAssert.Contains("ID", issue.Description);
		}
	}
}