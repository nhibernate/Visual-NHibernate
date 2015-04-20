using System;
using System.Text;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_CSharp_Formatter_Body_Text
{
	public class MethodBodyTestBase
	{
		public static void TestBodyText(string methodBody, string expectedText)
		{
			TestBodyText(methodBody, expectedText, new CSharpFormatSettings());
		}

		public static void TestBodyText(string methodBody, string expectedText, CSharpFormatSettings formatSettings)
		{
			string code =
				string.Format("public class Class1\r\n{0}\r\npublic void Method1(string param1)\r\n{0}\r\n{2}\r\n{1}\r\n{1}", '{',
							  '}', methodBody);

			CSharpParser parser = new CSharpParser();
			parser.FormatSettings.SetFrom(formatSettings);
			parser.ParseCode(code);

			Assert.That(parser.ErrorOccurred, Is.False, "Parser errors occurred:\n" + GetSyntaxErrors(parser));

			ICodeRoot codeRoot = parser.CreatedCodeRoot;
			Class clazz = (Class)codeRoot.WalkChildren()[0];

			Function con = (Function)clazz.WalkChildren()[0];
			Assert.That(con.Name, Is.EqualTo("Method1"));

			Assert.That(con.BodyText, Is.EqualTo(expectedText));
		}

		public static CSharpParser TestFullText(string code, string expectedText)
		{
			return TestFullText(code, expectedText, new CSharpFormatSettings());
		}

		public static CSharpParser TestFullText(string code, string expectedText, CSharpFormatSettings formatSettings)
		{
			CSharpParser parser = new CSharpParser();
			parser.FormatSettings.SetFrom(formatSettings);
			parser.ParseCode(code);

			Assert.That(parser.ErrorOccurred, Is.False, "Parser errors occurred:\n" + GetSyntaxErrors(parser));

			Assert.That(parser.CreatedCodeRoot.ToString(), Is.EqualTo(expectedText));

			return parser;
		}

		private static string GetSyntaxErrors(CSharpParser parser)
		{
			StringBuilder sb = new StringBuilder();
			foreach (ParserSyntaxError error in parser.SyntaxErrors)
			{
				sb.Append(error.LineNumber).Append(": ").AppendLine(error.ErrorMessage);
			}
			if(parser.ExceptionThrown != null)
			{
				sb.AppendLine("Exception Thrown:");
				sb.Append(parser.ExceptionThrown.ToString());
			}
			return sb.ToString();
		}
	}

	[TestFixture]
	public class Statement_Formatting : MethodBodyTestBase
	{
		[Test]
		public void ComplexMethodBody_Directive()
		{
			const string code =
				@"int i = 0;
	if (i == 0)
	{
		Console.WriteLine(""Debug"");
	}
	else 
	{
		Console.WriteLine(""Release"");
	}
	foreach (int j in obj.Items)
	{
		Console.WriteLine(j.ToString());
	}
	for (int k = 0; k < 10; k++)
	{
		Console.WriteLine(k.ToString());
	}
";
			const string expected =
	@"{
	int i = 0;
	if (i == 0) 
	{
		Console.WriteLine(""Debug"");
	}
	else 
	{
		Console.WriteLine(""Release"");
	}
	foreach(int j in obj.Items) 
	{
		Console.WriteLine(j.ToString());
	}
	for(int k = 0; k < 10; k++) 
	{
		Console.WriteLine(k.ToString());
	}
}
";
			TestBodyText(code, expected);
		}

		[Test]
		public void AddBracesToSingleLineBlocks()
		{
			const string methodBody = "if (true) return; else Console.WriteLine();";
			string expectedText =
				"{\n\tif (true) \n\t{\n\t\treturn;\n\t}\n\telse \n\t{\n\t\tConsole.WriteLine();\n\t}\n}\n"
					.Replace("\n", Environment.NewLine);

			CSharpFormatSettings settings = new CSharpFormatSettings();
			settings.AddBracesToSingleLineBlocks = true;
			settings.PutBracesOnNewLines = true;
			TestBodyText(methodBody, expectedText, settings);

			expectedText = "{\n\tif (true) return;\n\telse Console.WriteLine();\n}\n".Replace("\n", Environment.NewLine);
			settings.AddBracesToSingleLineBlocks = false;
			settings.SingleLineBlocksOnSameLineAsParent = true;
			TestBodyText(methodBody, expectedText, settings);

			expectedText = "{\n\tif (true) \n\t\treturn;\n\telse \n\t\tConsole.WriteLine();\n}\n".Replace("\n",
			                                                                                              Environment.NewLine);
			settings.AddBracesToSingleLineBlocks = false;
			settings.SingleLineBlocksOnSameLineAsParent = false;
			TestBodyText(methodBody, expectedText, settings);
		}

		[Test]
		public void BlockWithinBlock()
		{
			const string methodBody =
				@"
                    int i = 0;
                    {
                        // Do some other stuff
                        Console.WriteLine(i.ToString());
                        {
                            // Another block!
                            return;
                        }
                    }
                    ";

			string expectedText =
				"{\n\tint i = 0;\n\t{\n\t\t// Do some other stuff\n\t\tConsole.WriteLine(i.ToString());\n\t\t{\n\t\t\t// Another block!\n\t\t\treturn;\n\t\t}\n\t}\n}\n"
					.Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void CheckedStatements()
		{
			const string methodBody = "checked{ return 0xFFFFFFFF + 1; }";
			string expectedText = "{\n\tchecked\n\t{\n\t\treturn 0xFFFFFFFF + 1;\n\t}\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void DoWhileStatements()
		{
			const string methodBody = "do { return 0xFFFFFFFF + 1; } while(true);";
			string expectedText = "{\n\tdo\n\t{\n\t\treturn 0xFFFFFFFF + 1;\n\t}\n\twhile(true);\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void EmptyStatementsRemoved()
		{
			const string methodBody = "string i = \"\"; \r\n;";
			string expectedText = "{\n\tstring i = \"\";\n}\n".Replace("\n", Environment.NewLine);

			CSharpFormatSettings settings = new CSharpFormatSettings();
			settings.OmitEmptyStatements = true;
			TestBodyText(methodBody, expectedText, settings);

			expectedText = "{\n\tstring i = \"\";\n\t;\n}\n".Replace("\n", Environment.NewLine);
			settings.OmitEmptyStatements = false;
			TestBodyText(methodBody, expectedText, settings);
		}

		[Test]
		public void FixedStatements()
		{
			const string methodBody = "unsafe { fixed(object * i = &x, j = & y) { i++; } }";
			string expectedText =
				"{\n\tunsafe\n\t{\n\t\tfixed(object * i = &x, j = &y)\n\t\t{\n\t\t\ti++;\n\t\t}\n\t}\n}\n".Replace("\n",
				                                                                                                   Environment.
				                                                                                                   	NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void ForEachLoop()
		{
			const string methodBody =
				@"
                    foreach(int i in new int[]{1, 2, 3, 4, 5, 6})
                    {
                        Console.WriteLine(i);
                    }
                    ";

			string expectedText = "{\n\tforeach(int i in new int[]{1, 2, 3, 4, 5, 6}) \n\t{\n\t\tConsole.WriteLine(i);\n\t}\n}\n"
				.Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void ForLoop()
		{
			string methodBody =
				@"
                    for(int i = 0; i < 10; i++)
                    {
                        Console.WriteLine();
                    }
                    ";

			string expectedText = "{\n\tfor(int i = 0; i < 10; i++) \n\t{\n\t\tConsole.WriteLine();\n\t}\n}\n"
				.Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);

			methodBody =
				@"
                    for(int i = 0, j = 1; i < 10 && j < 11; i++, j++)
                    {
                        Console.WriteLine();
                    }
                    ";

			expectedText = "{\n\tfor(int i = 0, j = 1; i < 10 && j < 11; i++, j++) \n\t{\n\t\tConsole.WriteLine();\n\t}\n}\n"
				.Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void GotoStatements()
		{
			string methodBody = "x:    i = 10; goto \n  x;";
			string expectedText = "{\n\tx: i = 10; goto x;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);

			methodBody =
				@"switch(nodeType)
{ 
	case DotNet.SwitchStatement:
		goto default;
	default:
		goto case DotNet.SwitchStatement;
}";
			expectedText = "{\n\tswitch(nodeType)\n\t{\n\t\tcase DotNet.SwitchStatement:\n\t\t\tgoto default;\n\t\tdefault:\n\t\t\tgoto case DotNet.SwitchStatement;\n\t}\n}\n"
				.Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void IfStatement()
		{
			const string methodBody =
				@"
                    decimal i = 0m;
                    if (i == 0m)
                    {
                        // Do some other stuff
                        Console.WriteLine(i.ToString());
                    }
                    else Console.WriteLine(""i not equal to 0!"");";

			string expectedText =
				"{\n\tdecimal i = 0m;\n\tif (i == 0m) \n\t{\n\t\t// Do some other stuff\n\t\tConsole.WriteLine(i.ToString());\n\t}\n\telse \n\t{\n\t\tConsole.WriteLine(\"i not equal to 0!\");\n\t}\n}\n"
					.Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void LabeledStatements()
		{
			const string methodBody = "x:    throw  new Exception();";
			string expectedText = "{\n\tx: throw new Exception();\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void LockStatements()
		{
			const string methodBody = "lock(this) { throw new Exception(); }";
			string expectedText = "{\n\tlock(this)\n\t{\n\t\tthrow new Exception();\n\t}\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void ReturnStatements()
		{
			string methodBody = "return;";
			string expectedText =
				"{\n\treturn;\n}\n"
					.Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);

			methodBody = "return 5d;";
			expectedText =
				"{\n\treturn 5d;\n}\n"
					.Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void SwitchStatements()
		{
			const string methodBody =
				@"
switch(nodeType)
{ 
	case DotNet.SwitchStatement:
		// Do nothing
		break;
	default:
		break;
}";
			string expectedText = "{\n\tswitch(nodeType)\n\t{\n\t\tcase DotNet.SwitchStatement:\n\t\t\t// Do nothing\n\t\t\tbreak;\n\t\tdefault:\n\t\t\tbreak;\n\t}\n}\n"
				.Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void ThrowStatements()
		{
			string methodBody = "throw  ;";
			string expectedText = "{\n\tthrow;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);

			methodBody = "throw    e;";
			expectedText = "{\n\tthrow e;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);

			methodBody = "throw new \n Exception( );";
			expectedText = "{\n\tthrow new Exception();\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void TryCatchStatements()
		{
			// use multiple catch statements, and chuck a throw; and throw e; in for good measure.
			const string methodBody =
				@"
try
{
	throw new Exception();
}
catch(IOException e)
{
	throw;
}
catch(Exception e)
{
	throw e;
}
catch
{
}
";
			string expectedText = "{\n\ttry\n\t{\n\t\tthrow new Exception();\n\t}\n\tcatch(IOException e)\n\t{\n\t\tthrow;\n\t}\n\tcatch(Exception e)\n\t{\n\t\tthrow e;\n\t}\n\tcatch\n\t{\n\t}\n}\n"
				.Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void UncheckedStatements()
		{
			const string methodBody = "unchecked{ return 0xFFFFFFFF + 1; }";
			string expectedText = "{\n\tunchecked\n\t{\n\t\treturn 0xFFFFFFFF + 1;\n\t}\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void UnsafeStatements()
		{
			const string methodBody = "unsafe { object *   i  =   &x; }";
			string expectedText = "{\n\tunsafe\n\t{\n\t\tobject * i = &x;\n\t}\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void WhileStatements()
		{
			const string methodBody = @"
while(true)
{ 
	if(false) continue;
	if(true) break;
}";
			string expectedText = "{\n\twhile(true)\n\t{\n\t\tif (false) continue;\n\t\tif (true) break;\n\t}\n}\n"
				.Replace("\n", Environment.NewLine);

			CSharpFormatSettings settings = new CSharpFormatSettings();
			settings.AddBracesToSingleLineBlocks = false;
			settings.SingleLineBlocksOnSameLineAsParent = true;
			TestBodyText(methodBody, expectedText, settings);
		}

		[Test]
		public void YieldStatements()
		{
			string methodBody = "yield  return  \n 5;";
			string expectedText = "{\n\tyield return 5;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);

			methodBody = "yield \n break;";
			expectedText = "{\n\tyield break;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}
	}
}