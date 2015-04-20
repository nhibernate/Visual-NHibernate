using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common.IEnumerableExtensions;
using NUnit.Framework;

namespace Specs_For_New_Distinct
{
	[TestFixture]
	public class When_Given_A_Property_That_Implements_GetHashCode
	{
		[Test]
		public void It_Should_Return_Distinct_Instances_Based_On_That_Property()
		{
			var list = new List<TestClass> {new TestClass("Test", 1), new TestClass("Test", 2), new TestClass("Something", 1)};

			var distinctList = list.Distinct(i => i.Identifier).ToList();

			Assert.That(distinctList[0].IntId, Is.EqualTo(1));
			Assert.That(distinctList[0].Identifier, Is.EqualTo("Test"));

			Assert.That(distinctList[1].IntId, Is.EqualTo(1));
			Assert.That(distinctList[1].Identifier, Is.EqualTo("Something"));
		}

		private class TestClass
		{
			public string Identifier;
			public int IntId;

			public TestClass(string identifier, int intId)
			{
				Identifier = identifier;
				IntId = intId;
			}
		}
	}
}