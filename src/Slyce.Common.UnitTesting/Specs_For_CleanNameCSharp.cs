using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;

namespace Specs_For_CleanNameCSharp
{
	[TestFixture]
	public class When_Given_A_Valid_Name
	{
		[Test]
		public void It_Returns_The_Same_String()
		{
			var name = "Class1";
			var output = Utility.CleanNameCSharp(name);

			Assert.That(output, Is.EqualTo(name));
		}
	}

	[TestFixture]
	public class When_Given_A_Name_With_Invalid_Characters
	{
		[Test]
		public void It_Changes_Them_To_Underscores()
		{
			var name = "Class-1;Spec";
			var output = Utility.CleanNameCSharp(name);

			Assert.That(output, Is.EqualTo("Class_1_Spec"));
		}
	}

	[TestFixture]
	public class When_Given_A_Name_That_Is_A_CSharp_Identifier
	{
		[Test]
		public void It_Prefixes_It_With_An_At_Symbol()
		{
			var name = "class";
			var output = Utility.CleanNameCSharp(name);

			Assert.That(output, Is.EqualTo("@class"));
		}
	}
}