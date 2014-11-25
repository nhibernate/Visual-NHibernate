using System;
using NUnit.Framework;

namespace Specs_For_CSharp_Formatter_Body_Text
{
	[TestFixture]
	public class Expression_Formatting : MethodBodyTestBase
	{
		[Test]
		public void AddressOfOperator()
		{
			const string methodBody = "int * pointer =   &i;";
			string expectedText = "{\n\tint * pointer = &i;\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void AnonymousMethod()
		{
			const string methodBody =
				@"      
				SomeDelegate del = delegate(int i, string j) 
							{
								MessageBox.Show(""Hello"");
						        };
				del();";
			string expectedText =
				"{\n\tSomeDelegate del = delegate(int i, string j)\n\t\t{\n\t\t\tMessageBox.Show(\"Hello\");\n\t\t};\n\tdel();\n}\n"
					.Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void ArgumentExpression()
		{
			const string methodBody = "int  i  =   GetI(out j, \nref k);";
			string expectedText = "{\n\tint i = GetI(out j, ref k);\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void ArrayCreationWithRanks()
		{
			const string methodBody = "int[,] arr = new int[2  , 2]{{1,2   }, {  3, 4 }};";
			string expectedText = "{\n\tint[,] arr = new int[2, 2]{{1, 2}, {3, 4}};\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void ArrayCreationWithInitialiser()
		{
			const string methodBody = "int[] array = new int[]{3,4,5,6};";
			string expectedText = "{\n\tint[] array = new int[]{3, 4, 5, 6};\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void ArrayCreationWithoutInitialiser()
		{
			const string methodBody = "int[] array = new int[ 5];";
			string expectedText = "{\n\tint[] array = new int[5];\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void UnaryAddition()
		{
			const string methodBody = "int   i   =   +1;";
			string expectedText = "{\n\tint i = +1;\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void UnarySubtraction()
		{
			const string methodBody = "int   i   =   -1;";
			string expectedText = "{\n\tint i = -1;\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void BaseAccess()
		{
			const string methodBody = "Type   type  =   base.GetType();";
			string expectedText = "{\n\tType type = base.GetType();\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void CastExpression()
		{
			const string methodBody = "int      i = (int) 0;";
			string expectedText = "{\n\tint i = (int) 0;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void Test_Var_Literal_String_Expression()
		{
			const string methodBody = "var comp = \"\";";
			string expectedText = "{\n\tvar comp = \"\";\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void Test_Var_Simple_Literal_Expression()
		{
			const string methodBody = "var comp = 5;";
			string expectedText = "{\n\tvar comp = 5;\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void Test_Var_Cast_Expression()
		{
			const string methodBody = "var comp = (List<CompositionInfo>)compositions;";
			string expectedText = "{\n\tvar comp = (List<CompositionInfo>) compositions;\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void Test_Var_New_Object_Expression()
		{
			const string methodBody = "var comp = new List<CompositionInfo>();";
			string expectedText = "{\n\tvar comp = new List<CompositionInfo>();\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void CheckedExpression()
		{
			const string methodBody = "checked (i  = \n0x00000000);";
			string expectedText = "{\n\tchecked(i = 0x00000000);\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void ConditionalExpression()
		{
			const string methodBody = "object   obj = (i != null) ? \ni.Text : \n\"\";";
			string expectedText = "{\n\tobject obj = (i != null) ? i.Text : \"\";\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void DefaultValueExpression()
		{
			string methodBody = "object   obj = default(object);";
			string expectedText = "{\n\tobject obj = default(object);\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);

			methodBody = "int i = default(int);";
			expectedText = "{\n\tint i = default(int);\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void GenericMethodInvocation()
		{
			const string methodBody =
				"int i = 1; \r\n\tConsole.  WriteLine<string  ,   int>( \"44444\"  .ToString(   ),  \r\n 5, i);";
			string expectedText =
				"{\n\tint i = 1;\n\tConsole.WriteLine<string, int>(\"44444\".ToString(), 5, i);\n}\n".Replace("\n",
				                                                                                              Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void GenericObjectCreation()
		{
			const string methodBody = "File<string, string > \r\n file = new    File<string  , string>(\"filename.txt\");";
			string expectedText =
				"{\n\tFile<string, string> file = new File<string, string>(\"filename.txt\");\n}\n".Replace("\n",
				                                                                                            Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void IsTypeOfExpression()
		{
			const string methodBody = "bool  \n \nresult = obj is \n ObjectType;";
			string expectedText = "{\n\tbool result = obj is ObjectType;\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void LambaExpression()
		{
			string methodBody = "int  totalUnits = orderDetails.Sum(d   =>   d.UnitCount);";
			string expectedText = "{\n\tint totalUnits = orderDetails.Sum(d => d.UnitCount);\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);

			methodBody = "int  totalUnits = orderDetails.Sum((d)   =>   d.UnitCount);";
			expectedText = "{\n\tint totalUnits = orderDetails.Sum(d => d.UnitCount);\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);

			methodBody = "int  totalUnits = orderDetails.Sum((int d)   =>   d.UnitCount);";
			expectedText = "{\n\tint totalUnits = orderDetails.Sum((int d) => d.UnitCount);\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);

			methodBody = "int  totalUnits = orderDetails.Sum((int d,float f)   =>   d.UnitCount);";
			expectedText = "{\n\tint totalUnits = orderDetails.Sum((int d, float f) => d.UnitCount);\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);

			methodBody = "int  totalUnits = orderDetails.Sum(x, y   => { return x + y; } );";
			expectedText = "{\n\tint totalUnits = orderDetails.Sum(x, y => { return x + y; } );\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void MethodInvocation()
		{
			const string methodBody = "int i = 1; \r\n\tConsole.  WriteLine( \"44444\"  .ToString(   ),  \r\n 5, i);";
			string expectedText = "{\n\tint i = 1;\n\tConsole.WriteLine(\"44444\".ToString(), 5, i);\n}\n"
				.Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void ObjectCreation()
		{
			const string methodBody = "File \r\n file = new    File    (\"filename.txt\");";
			string expectedText = "{\n\tFile file = new File(\"filename.txt\");\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void ParenthesizedExpression()
		{
			const string methodBody = "int  result = (\n(i + j) + \n 5) * 4;";
			string expectedText = "{\n\tint result = ((i + j) + 5) * 4;\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void PointerMemberAccessExpression()
		{
			string methodBody = "int  result = i->x;";
			string expectedText = "{\n\tint result = i->x;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
			
			methodBody = "string text = i->ToString();";
			expectedText = "{\n\tstring text = i->ToString();\n}\n".Replace("\n", Environment.NewLine); 
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void SizeOfExpression()
		{
			string methodBody = "int  size = sizeof(int);";
			string expectedText = "{\n\tint size = sizeof(int);\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);

			methodBody = "int  size = sizeof(int*);";
			expectedText = "{\n\tint size = sizeof(int*);\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void StackAllocExpression()
		{
			const string methodBody = "int * array  = stackalloc int[100];";
			string expectedText = "{\n\tint * array = stackalloc int[100];\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);

		}

		[Test]
		public void ThisAccess()
		{
			const string methodBody = "Type   type  =   this.GetType();";
			string expectedText = "{\n\tType type = this.GetType();\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void TryCastExpression()
		{
			const string methodBody = "SomeType  variable = otherVariable \n as SomeType;";
			string expectedText = "{\n\tSomeType variable = otherVariable as SomeType;\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void TypeOfExpression()
		{
			const string methodBody = "Type   type  =   typeof(Array);";
			string expectedText = "{\n\tType type = typeof(Array);\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void TypeReferenceExpression()
		{
			// TypeReferenceExpressions are constructed when there is a static invocation in method call.
			const string methodBody = "object \nobj = GetInformation(byte\n.MaxValue);";
			string expectedText = "{\n\tobject obj = GetInformation(byte.MaxValue);\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void UncheckedExpression()
		{
			const string methodBody = "unchecked (i = \n0x00000000);";
			string expectedText = "{\n\tunchecked(i = 0x00000000);\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void VariableAssignment()
		{
			const string methodBody = "int i = 0; \r\n i  = \r\n1;";
			string expectedText = "{\n\tint i = 0;\n\ti = 1;\n}\n".Replace("\n", Environment.NewLine);

			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void VariableDeclaration()
		{
			const string methodBody = "int      i = 0;";
			string expectedText = "{\n\tint i = 0;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void ImplicitArrayCreation()
		{
			const string methodBody = "int[] array = new[]{3,4,5,6};";
			string expectedText = "{\n\tint[] array = new []{3, 4, 5, 6};\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void ArrayIndexers()
		{
			const string methodBody = "int[] array = new int[]{3,4,5,6};\n array[5] = 5; \n int i = array[2];";
			string expectedText = "{\n\tint[] array = new int[]{3, 4, 5, 6};\n\tarray[5] = 5;\n\tint i = array[2];\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void ObjectInitialisers()
		{
			const string methodBody = "Point p = new Point {  X = 5, Y = 1 };";
			const string expectedText = @"{
	Point p = new Point(){X = 5, Y = 1};
}
";
			TestBodyText(methodBody, expectedText);

		}

		[Test]
		public void ObjectInitialisersWithParameters()
		{
			const string methodBody = "Point p = new Point(true){  X = 5, Y = 1 };";
			const string expectedText = @"{
	Point p = new Point(true){X = 5, Y = 1};
}
";
			TestBodyText(methodBody, expectedText);

		}
	}
}