using ArchAngel.Providers.EntityModel.Controller.Validation.Rules;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Validation.Specs_For_All_Properties_Mapped
{
	[TestFixture]
	public class When_All_Properties_Are_Mapped
	{
		[Test]
		public void The_Rule_Passes()
		{
			var set = new MappingSetImpl();
			var entity = new EntityImpl("Entity1");
			var property = new PropertyImpl("Property1");
			entity.AddProperty(property);
			set.EntitySet.AddEntity(entity);

			var table = new Table("Table1");
			var column = new Column("Column1");
			table.AddColumn(column);
			set.Database.AddTable(table);

			set.ChangeMappedColumnFor(property).To(column);

			var rule = new CheckAllPropertiesMappedRule();
			var result = rule.Run(set);

			Assert.That(result.HasIssues, Is.False);
		}
	}

	[TestFixture]
	public class When_A_Property_Isnt_Mapped
	{
		[Test]
		public void The_Rule_Fails()
		{
			var set = new MappingSetImpl();
			var entity = new EntityImpl("Entity1");
			var property = new PropertyImpl("Property1");
			entity.AddProperty(property);
			set.EntitySet.AddEntity(entity);

			var table = new Table("Table1");
			var column = new Column("Column1");
			table.AddColumn(column);
			set.Database.AddTable(table);

			var rule = new CheckAllPropertiesMappedRule();
			var result = rule.Run(set);

			Assert.That(result.HasIssues);
			Assert.That(result.Issues, Has.Count(1));

			var issue = result.Issues[0];
			Assert.That(issue.ErrorLevel, Is.EqualTo(ValidationErrorLevel.Warning));
			Assert.That(issue.Object, Is.SameAs(property));
			StringAssert.Contains("Property1", issue.Description);
			StringAssert.Contains("Entity1", issue.Description);
		}
	}

    [TestFixture]
    public class When_A_Parent_Property_Isnt_Mapped
    {
        [Test]
        public void The_Rule_Fails_But_Only_For_The_Parent()
        {
            var set = new MappingSetImpl();
            var parentEntity = new EntityImpl("Parent");
            var childEntity = new EntityImpl("Child");
            childEntity.Parent = parentEntity;
            var property = new PropertyImpl("Property1");
            parentEntity.AddProperty(property);
            set.EntitySet.AddEntity(parentEntity);
            set.EntitySet.AddEntity(childEntity);

            var table = new Table("Table1");
            var column = new Column("Column1");
            table.AddColumn(column);
            set.Database.AddTable(table);

            var rule = new CheckAllPropertiesMappedRule();
            var result = rule.Run(set);

            Assert.That(result.HasIssues);
            Assert.That(result.Issues, Has.Count(1));

            var issue = result.Issues[0];
            Assert.That(issue.ErrorLevel, Is.EqualTo(ValidationErrorLevel.Warning));
            Assert.That(issue.Object, Is.SameAs(property));
            StringAssert.Contains("Property1", issue.Description);
            StringAssert.Contains("Parent", issue.Description);
        }
    }
}