using System.Collections.Generic;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common.IEnumerableExtensions;

namespace Specs_For_Index_Validation
{
	[TestFixture]
	public class When_Validating_A_Single_Valid_Index
	{
		[Test]
		public void It_Should_Validate_Correctly()
		{
			var Index = new Index { Name = "Index1"};

			var failures = new List<ValidationFailure>();
			Assert.That(Index.ValidateObject(failures), Is.True);
			Assert.That(failures, Is.Empty);
		}
	}

	[TestFixture]
	public class When_Validating_A_Single_Invalid_Index
	{
		[Test]
		public void It_Should_Indicate_The_Invalid_Property_Name()
		{
			var index = new Index { Name = null };

			var failures = new List<ValidationFailure>();
			Assert.That(index.ValidateObject(failures), Is.False);
			Assert.That(failures, Has.Count(1));
			Assert.That(failures[0].Property, Is.EqualTo("Name"));
		}

		[Test]
		public void It_Should_Flag_Names_With_Spaces_As_Invalid()
		{
			var index = new Index { Name = "Index 1"};

			var failures = new List<ValidationFailure>();
			Assert.That(index.ValidateObject(failures), Is.False);
			Assert.That(failures, Has.Count(1));
			Assert.That(failures[0].Property, Is.EqualTo("Name"));
		}
	}

	[TestFixture]
	public class When_Validating_A_Index_With_Siblings
	{
		[Test]
		public void It_Should_Validate_Correcty()
		{
			var index = new Index { Name = "Index1"};

			var table = new Table("Table1");
			table.AddIndex(index);
			table.AddIndex(new Index { Name = "Index2"});

			var failures = new List<ValidationFailure>();
			Assert.That(index.ValidateObject(failures), Is.True);
			Assert.That(failures, Is.Empty);
		}
	}

	[TestFixture]
	public class When_Validating_A_Index_With_Siblings_With_The_Same_Name
	{
		[Test]
		public void Both_Should_Not_Validate()
		{
			var index1 = new Index { Name = "Index"};
			var index2 = new Index { Name = "Index"};

			var table = new Table("Table1");
			table.AddIndex(index1);
			table.AddIndex(index2);

			ValidateIndex(index1);
			ValidateIndex(index2);
		}

		private static void ValidateIndex(ScriptBase Index1)
		{
			var failures = new List<ValidationFailure>();
			Assert.That(Index1.ValidateObject(failures), Is.False);
			Assert.That(failures, Has.Count(1));

			Assert.That(failures[0].Property == "Name");
		}
	}
}
