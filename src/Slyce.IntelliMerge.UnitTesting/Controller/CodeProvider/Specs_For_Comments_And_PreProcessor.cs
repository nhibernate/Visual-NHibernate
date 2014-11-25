using System;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.CSharp;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Specs_For_CSharp_Formatter_Body_Text;

namespace Specs_For_CSharp_Formatter
{
	[TestFixture]
	public class Blank_Line_Handling : MethodBodyTestBase
	{
		[Test]
		public void Single_Blank_Line()
		{
			const string methodBody = "int i = 0; \n\n return 0xFFFFFFFF + 1;";
			string expectedText = "{\n\tint i = 0;\n\n\treturn 0xFFFFFFFF + 1;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void Multiple_Single_Blank_Lines()
		{
			const string methodBody = "int i = 0; \n\n int j = 0; \n\n return 0xFFFFFFFF + 1;";
			string expectedText = "{\n\tint i = 0;\n\n\tint j = 0;\n\n\treturn 0xFFFFFFFF + 1;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void Multiple_Blank_Lines()
		{
			const string methodBody = "int i = 0; \n\n\n \n\n return 0xFFFFFFFF + 1;";
			string expectedText = "{\n\tint i = 0;\n\n\n\n\n\treturn 0xFFFFFFFF + 1;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void Multiple_Multiple_Blank_Lines()
		{
			const string methodBody = "int i = 0; \n\n\n int j = 0; \n\n return 0xFFFFFFFF + 1;";
			string expectedText = "{\n\tint i = 0;\n\n\n\tint j = 0;\n\n\treturn 0xFFFFFFFF + 1;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void Single_Blank_Line_Above_Comment()
		{
			const string methodBody = "int i = 0; \n\n // Comment \nreturn 0xFFFFFFFF + 1;";
			string expectedText = "{\n\tint i = 0;\n\n\t// Comment\n\treturn 0xFFFFFFFF + 1;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}
	}

	[TestFixture]
	public class Comment_Handling : MethodBodyTestBase
	{
		[Test]
		public void Around_Regular_Statements()
		{
			const string methodBody = "// Comment \n return 0xFFFFFFFF + 1;";
			string expectedText = "{\n\t// Comment\n\treturn 0xFFFFFFFF + 1;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void Comment_Blocks_Set_To_True()
		{
			const string methodBody = "// Comment \n /* Another Comment */ \n return 0xFFFFFFFF + 1;";
			string expectedText = "{\n\t/* Comment */\n\t/* Another Comment */\n\treturn 0xFFFFFFFF + 1;\n}\n".Replace("\n", Environment.NewLine);
			CSharpFormatSettings settings = new CSharpFormatSettings();
			settings.CommentLinesAsCommentBlock = true;
			TestBodyText(methodBody, expectedText, settings);
		}

		[Test]
		public void Inside_Statement_Blocks()
		{
			const string methodBody = "do { // Comment \n return 0xFFFFFFFF + 1; } while(true);";
			string expectedText = "{\n\tdo\n\t{\n\t\t// Comment\n\t\treturn 0xFFFFFFFF + 1;\n\t}\n\twhile(true);\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void Inside_Nested_Blocks()
		{
			const string methodBody = "{ // Comment \n return 0xFFFFFFFF + 1; }";
			string expectedText = "{\n\t{\n\t\t// Comment\n\t\treturn 0xFFFFFFFF + 1;\n\t}\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void At_The_End_Of_A_Statement()
		{
			const string methodBody = "return 0xFFFFFFFF + 1; // Comment";
			string expectedText = "{\n\treturn 0xFFFFFFFF + 1; // Comment\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		[Ignore("This is not supported yet. Still trying to think of a good way of doing it.")]
		public void As_Part_Of_A_Method_Invocation()
		{
			const string methodBody = "RunCommand(command, // The Command to Run\n\"Command 5\" // Command Description\n);";
			string expectedText = "{\n\tRunCommand(command, // The Command to Run\n\t\t\"Command 5\" // Command Description\t\n);\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void As_Part_Of_A_Single_Line_Block_With_No_Braces()
		{
			const string methodBody = "if (true)\n// Comment\nActualStatement();";
			string expectedText = "{\n\tif (true) \n\t\t// Comment\n\t\tActualStatement();\n}\n".Replace("\n", Environment.NewLine);
			CSharpFormatSettings settings = new CSharpFormatSettings();
			settings.AddBracesToSingleLineBlocks = false;
			settings.SingleLineBlocksOnSameLineAsParent = false;
			TestBodyText(methodBody, expectedText, settings);
		}

		[Test]
		public void Regular_Comments_Above_A_Method_Declaration()
		{
			const string code =
	@"
public class Class1
{
	// Using regular comments, not xml comments
	public void Method1(AuthenticationSettings settings) { }
}
";
			const string expected =
				@"
public class Class1
{
	// Using regular comments, not xml comments
	public void Method1(AuthenticationSettings settings)
	{
	}
	
}";

			TestFullText(code, expected);
		}

		[Test]
		public void Regular_Comments_Above_A_Field_Declaration()
		{
			const string code =
	@"
public class Class1
{
	// Using regular comments, not xml comments
	private static Class1 instance = new Class1();
}
";
			const string expected =
				@"
public class Class1
{
	// Using regular comments, not xml comments
	private static Class1 instance = new Class1();
}";

			TestFullText(code, expected);
		}

		[Test]
		public void Regular_Comments_Above_A_Field_Declaration_With_Other_Decls()
		{
			const string code =
	@"
public class Class1
{
	/// <summary>
	/// Default constructor
	/// </summary>
	private AlphabeticalListOfProduct()
	{
	}

	// Using regular comments, not xml comments
	private static Class1 instance = new Class1();

	/// <summary>
	/// Get a list of AlphabeticalListOfProducts
	/// </summary>
	/// <returns>Arraylist of AlphabeticalListOfProducts</returns>
	public static IList<AlphabeticalListOfProductInfo> GetAlphabeticalListOfProducts()
	{
		// Run a search against the data store
		return dal.GetAlphabeticalListOfProducts();
	}
}
";
			const string expected =
				@"
public class Class1
{
	/// <summary>
	/// Default constructor
	/// </summary>
	private AlphabeticalListOfProduct()
	{
	}
	
	// Using regular comments, not xml comments
	private static Class1 instance = new Class1();

	/// <summary>
	/// Get a list of AlphabeticalListOfProducts
	/// </summary>
	/// <returns>Arraylist of AlphabeticalListOfProducts</returns>
	public static IList<AlphabeticalListOfProductInfo> GetAlphabeticalListOfProducts()
	{
		// Run a search against the data store
		return dal.GetAlphabeticalListOfProducts();
	}
	
}";

			TestFullText(code, expected);
		}

		[Test]
		public void Regular_Comments_Mixed_With_Xml_Comments()
		{
			const string code =
	@"
public class Class1
{
	/// <summary>
	/// Default constructor
	/// </summary>
	private AlphabeticalListOfProduct()
	{
	}

	/// Using regular comments, not xml comments
	//private static Class1 instance = new Class1();

	/// <summary>
	/// Get a list of AlphabeticalListOfProducts
	/// </summary>
	/// <returns>Arraylist of AlphabeticalListOfProducts</returns>
	public static IList<AlphabeticalListOfProductInfo> GetAlphabeticalListOfProducts()
	{
		// Run a search against the data store
		return dal.GetAlphabeticalListOfProducts();
	}
}
";
			const string expected =
				@"
public class Class1
{
	/// <summary>
	/// Default constructor
	/// </summary>
	private AlphabeticalListOfProduct()
	{
	}
	
	//private static Class1 instance = new Class1();
	/// Using regular comments, not xml comments
	/// <summary>
	/// Get a list of AlphabeticalListOfProducts
	/// </summary>
	/// <returns>Arraylist of AlphabeticalListOfProducts</returns>
	public static IList<AlphabeticalListOfProductInfo> GetAlphabeticalListOfProducts()
	{
		// Run a search against the data store
		return dal.GetAlphabeticalListOfProducts();
	}
	
}";

			TestFullText(code, expected);
		}
	}

	[TestFixture]
	[Ignore("We don't support preprocessor directives at the moment. Waiting on Actipro to include it in their parser.")]
	public class Preprocessor_Directives : MethodBodyTestBase
	{
		[Test]
		public void Completely_Within_A_Method_Body_Single_Scope()
		{
			const string code =
				@"
	#if DEBUG
	Console.WriteLine(""Debug"");
	#else
	Console.WriteLine(""Release"");
	#endif";
			const string expected =
	@"
{
	#if DEBUG
	Console.WriteLine(""Debug"");
	#else
	Console.WriteLine(""Release"");
	#endif
}
";

			TestBodyText(code, expected);
		}

		[Test]
		public void Around_A_Method_Signature()
		{
			const string code =
				@"
public class Class1
{
#if DEBUG
	public void Method1()
#else
	public void Method1(AuthenticationSettings settings)
#endif
	{ }
}
";
			const string expected =
				@"
public class Class1
{
#if DEBUG
	public void Method1()
#else
	public void Method1(AuthenticationSettings settings)
#endif
	{ 
	}
}
";

			TestFullText(code, expected);
		}
	}

	[TestFixture]
	public class Regions : MethodBodyTestBase
	{
		[Test]
		public void Completely_Within_A_Method_Body_Single_Scope()
		{
			const string code =
				@"
	#region DEBUG
	Console.WriteLine(""Debug"");
	#endregion";
			const string expected =
	@"{
	#region DEBUG
	Console.WriteLine(""Debug"");
	#endregion
}
";

			TestBodyText(code, expected);
		}

		[Test]
		public void Around_A_Method()
		{
			const string code =
				@"
public class Class1
{
	#region DEBUG
	public void Method1() { }
	#endregion

}
";
			const string expected =
				@"
public class Class1
{
	#region DEBUG
	public void Method1()
	{
	}
	#endregion

}";

			CSharpParser parser = TestFullText(code, expected);

			CodeRoot codeRoot = (CodeRoot)parser.CreatedCodeRoot;
			Assert.That(codeRoot.Classes[0].Functions.Count, Is.EqualTo(1), "Should be able to access functions inside regions from the class.");
			Assert.That(codeRoot.Classes[0].Functions[0].Name, Is.EqualTo("Method1"));
		}

		[Test]
		public void Nested_Regions_Around_A_Method()
		{
			const string code =
				@"
public class Class1
{
	#region DEBUG
	#region DEBUG2
	public void Method1() { }
	#endregion
	#endregion

}
";
			const string expected =
				@"
public class Class1
{
	#region DEBUG
	#region DEBUG2
	public void Method1()
	{
	}
	#endregion
	#endregion

}";

			CSharpParser parser = TestFullText(code, expected);

			CodeRoot codeRoot = (CodeRoot)parser.CreatedCodeRoot;
			Assert.That(codeRoot.Classes[0].Functions.Count, Is.EqualTo(1), "Should be able to access functions inside nested regions from the class.");
			Assert.That(codeRoot.Classes[0].Functions[0].Name, Is.EqualTo("Method1"));
		}
	}
}