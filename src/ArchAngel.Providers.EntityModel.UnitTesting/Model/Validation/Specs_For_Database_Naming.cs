using ArchAngel.Providers.EntityModel.Controller.Validation.Rules;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Validation.Specs_For_Database_Naming
{
	[TestFixture]
	public class When_Two_Tables_Have_The_Same_Name
	{
		[Test]
		public void The_Rule_Fails()
		{
			var set = new MappingSetImpl();
			set.Database.AddTable(new Table("Table1"));
			var duplicate = new Table("Table1");
			set.Database.AddTable(duplicate);

			DatabaseNamingRule rule = new DatabaseNamingRule();
			var result = rule.Run(set);

			Assert.That(result.Issues, Has.Count(1));
			
			var issue = result.Issues[0];
			Assert.That(issue.ErrorLevel, Is.EqualTo(ValidationErrorLevel.Error));
			Assert.That(issue.Object, Is.SameAs(duplicate));
			StringAssert.Contains("Table1", issue.Description);
		}
	}

	[TestFixture]
	public class When_Two_Keys_On_Different_Tables_Have_The_Same_Name
	{
		[Test]
		public void The_Rule_Fails()
		{
			var set = new MappingSetImpl();
			set.Database.AddTable(new Table("Table1"));
			set.Database.AddTable(new Table("Table2"));
			set.Database.Tables[0].AddKey(new Key("Key1"));
			var duplicate = new Key("Key1");
			set.Database.Tables[1].AddKey(duplicate);

			DatabaseNamingRule rule = new DatabaseNamingRule();
			var result = rule.Run(set);

			Assert.That(result.Issues, Has.Count(1));

			var issue = result.Issues[0];
			Assert.That(issue.ErrorLevel, Is.EqualTo(ValidationErrorLevel.Error));
			Assert.That(issue.Object, Is.SameAs(duplicate));
			StringAssert.Contains("Key1", issue.Description);
		}
	}

	[TestFixture]
	public class When_Two_Columns_On_A_Table_Have_The_Same_Name
	{
		[Test]
		public void The_Rule_Fails()
		{
			var set = new MappingSetImpl();
			var entity = new Table("Table1");
			set.Database.AddTable(entity);
			entity.AddColumn(new Column("Column1"));
			var duplicate = new Column("Column1");
			entity.AddColumn(duplicate);

			DatabaseNamingRule rule = new DatabaseNamingRule();
			var result = rule.Run(set);

			Assert.That(result.Issues, Has.Count(1));

			var issue = result.Issues[0];
			Assert.That(issue.ErrorLevel, Is.EqualTo(ValidationErrorLevel.Error));
			Assert.That(issue.Object, Is.SameAs(duplicate));
			StringAssert.Contains("Column1", issue.Description);
		}
	}

	[TestFixture]
	public class When_A_Column_Has_A_Blank_Name
	{
		[Test]
		public void The_Rule_Fails()
		{
			var set = new MappingSetImpl();
			var emptyEntity = new Table("");
			set.Database.AddTable(emptyEntity);

			DatabaseNamingRule rule = new DatabaseNamingRule();
			var result = rule.Run(set);

			Assert.That(result.Issues, Has.Count(1));

			var issue = result.Issues[0];
			Assert.That(issue.ErrorLevel, Is.EqualTo(ValidationErrorLevel.Error));
			Assert.That(issue.Object, Is.SameAs(emptyEntity));
			StringAssert.Contains("name", issue.Description);
		}
	}
}