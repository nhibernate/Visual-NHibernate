using System.Collections.Generic;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common.IEnumerableExtensions;

namespace Specs_For_Table_Validation
{
	[TestFixture]
	public class When_Validating_A_Single_Valid_Table
	{
		[Test]
		public void It_Should_Validate_Correctly()
		{
			var table = new Table { Name = "Table1"};

			var failures = new List<ValidationFailure>();
			Assert.That(table.ValidateObject(failures), Is.True);
			Assert.That(failures, Is.Empty);
		}
	}

	[TestFixture]
	public class When_Validating_A_Single_Invalid_Table
	{
		[Test]
		public void It_Should_Indicate_The_Invalid_Property_Name()
		{
			var table = new Table { Name = null};

			var failures = new List<ValidationFailure>();
			Assert.That(table.ValidateObject(failures), Is.False);
			Assert.That(failures, Has.Count(1));
			Assert.That(failures[0].Property, Is.EqualTo("Name"));
		}

		[Test]
		public void It_Should_Flag_Names_With_Spaces_As_Invalid()
		{
			var table = new Table { Name = "Table 1"};

			var failures = new List<ValidationFailure>();
			Assert.That(table.ValidateObject(failures), Is.False);
			Assert.That(failures, Has.Count(1));
			Assert.That(failures[0].Property, Is.EqualTo("Name"));
		}
	}

	[TestFixture]
	public class When_Validating_A_Table_With_Siblings
	{
		[Test]
		public void Both_Should_Validate_Correcty()
		{
			var table1 = new Table { Name = "Table1"};
			var table2 = new Table { Name = "Table2"};

			var database = new Database("DB");
			database.AddTable(table1);
			database.AddTable(table2);

			var failures = new List<ValidationFailure>();
			Assert.That(table1.ValidateObject(failures), Is.True);
			Assert.That(failures, Is.Empty);
		}
	}

	[TestFixture]
	public class When_Validating_A_Table_With_Siblings_With_The_Same_Name
	{
		[Test]
		public void Both_Should_Not_Validate()
		{
			var table1 = new Table { Name = "Table"};
			var table2 = new Table { Name = "Table"};

			var database = new Database("DB");
			database.AddTable(table1);
			database.AddTable(table2);

			ValidateTable(table1);
			ValidateTable(table2);
		}

		private static void ValidateTable(ScriptBase table1)
		{
			var failures = new List<ValidationFailure>();
			Assert.That(table1.ValidateObject(failures), Is.False);
			Assert.That(failures, Has.Count(1));

			Assert.That(failures[0].Property == "Name");
		}
	}
}
