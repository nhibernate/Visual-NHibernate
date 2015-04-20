using NUnit.Framework;
using ArchAngel.Designer;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_CompileHelper__VirtualProperties
{
	[TestFixture]
	public class When_There_Are_No_Virtual_Properties_Calls
	{
		[Test(Description = "Nothing even remotely like a VP call")]
		public void The_StringBuilder_Does_Not_Change_1()
		{
			const string functionText = "return true;";

			string output = CompileHelper.ReplaceVirtualPropertyCalls(functionText);
			Assert.That(output, Is.EqualTo(functionText));
		}

		[Test(Description = "A statement that has the word VirtualProperty in it")]
		public void The_StringBuilder_Does_Not_Change_2()
		{
			const string functionText = "entity.VirtualPropertyName.ToString();";

			string output = CompileHelper.ReplaceVirtualPropertyCalls(functionText);
			Assert.That(output, Is.EqualTo(functionText));
		}
	}

	[TestFixture]
	public class When_There_Is_One_Virtual_Properties_Getter_Call
	{
		[Test]
		public void The_Call_Is_Changed()
		{
			const string functionText = "entity.VirtualProperties.NameX;";
			const string expectedFunctionText = "entity.get_NameX();";

			string output = CompileHelper.ReplaceVirtualPropertyCalls(functionText);
			Assert.That(output, Is.EqualTo(expectedFunctionText));
		}
	}

	[TestFixture]
	public class When_There_Is_One_Virtual_Properties_Getter_Call_Part_Of_Chain
	{
		[Test]
		public void The_Call_Is_Changed()
		{
			const string functionText = "entity.VirtualProperties.NameX.ToString();";
			const string expectedFunctionText = "entity.get_NameX().ToString();";

			string output = CompileHelper.ReplaceVirtualPropertyCalls(functionText);
			Assert.That(output, Is.EqualTo(expectedFunctionText));
		}
	}

	[TestFixture]
	public class When_There_Is_One_Virtual_Properties_Getter_Call_Part_Of_Method_Call
	{
		[Test]
		public void The_Call_Is_Changed()
		{
			const string functionText = "Process(entity.VirtualProperties.NameX);";
			const string expectedFunctionText = "Process(entity.get_NameX());";

			string output = CompileHelper.ReplaceVirtualPropertyCalls(functionText);
			Assert.That(output, Is.EqualTo(expectedFunctionText));
		}
	}

	[TestFixture]
	public class When_There_Is_One_Virtual_Properties_Getter_Call_Extra_Spaces
	{
		[Test]
		public void The_Call_Is_Changed()
		{
			const string functionText = "entity.VirtualProperties.NameX   ;";
			const string expectedFunctionText = "entity.get_NameX()   ;";

			string output = CompileHelper.ReplaceVirtualPropertyCalls(functionText);
			Assert.That(output, Is.EqualTo(expectedFunctionText));
		}
	}

	[TestFixture]
	public class When_There_Are_Two_Virtual_Properties_Getter_Calls
	{
		[Test]
		public void The_Call_Is_Changed()
		{
			const string functionText = "entity.VirtualProperties.NameX;\nentity.VirtualProperties.NameX;";
			const string expectedFunctionText = "entity.get_NameX();\nentity.get_NameX();";

			string output = CompileHelper.ReplaceVirtualPropertyCalls(functionText);
			Assert.That(output, Is.EqualTo(expectedFunctionText));
		}
	}


	[TestFixture]
	public class When_There_Is_One_Virtual_Properties_Setter_Call
	{
		[Test]
		public void The_Call_Is_Changed()
		{
			const string functionText = "entity.VirtualProperties.NameX = \"5\";";
			const string expectedFunctionText = "entity.set_NameX(\"5\");";

			string output = CompileHelper.ReplaceVirtualPropertyCalls(functionText);
			Assert.That(output, Is.EqualTo(expectedFunctionText));
		}
	}

	[TestFixture]
	public class When_There_Is_One_Virtual_Properties_Setter_Call_Extra_Spaces
	{
		[Test]
		public void The_Call_Is_Changed()
		{
			const string functionText = "entity.VirtualProperties.NameX = \"5\" ;";
			const string expectedFunctionText = "entity.set_NameX(\"5\" );";

			string output = CompileHelper.ReplaceVirtualPropertyCalls(functionText);
			Assert.That(output, Is.EqualTo(expectedFunctionText));
		}
	}
}
