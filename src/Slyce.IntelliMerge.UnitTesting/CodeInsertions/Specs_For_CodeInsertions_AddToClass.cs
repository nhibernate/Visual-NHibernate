using System;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.CodeInsertions;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_CodeInsertions_AddToClass
{
	internal static class Code
	{
		public const string Class1Text =
@"namespace Something.Else
{
    public class Class1
    {
		public void Method()
		{
			// Method Text
			Console.WriteLine();
		}
    }
}";

		public const string Class1Plus2PropertiesText =
@"namespace Something.Else
{
    public class Class1
    {
		public string Property1 { get; set; }
		private Class1 Property2 { get; set; }
		public void Method()
		{
			// Method Text
			Console.WriteLine();
		}
    }
}";
	}


    [TestFixture]
    public class When_Adding_Two_New_Properties_To_The_End
    {
        [Test]
        public void They_Are_Added_Correctly()
        {
            var parser = new CSharpParser();
            parser.ParseCode(Code.Class1Text);

            var codeRoot = (CodeRoot)parser.CreatedCodeRoot;
            var basicClass = codeRoot.Namespaces[0].Classes[0];

            var property2 = new Property(codeRoot.Controller, "Property1", new DataType(codeRoot.Controller, "string"), "public");
			var property3 = new Property(codeRoot.Controller, "Property2", new DataType(codeRoot.Controller, "Class1"), "private");

            var action1 = new AddPropertyToClassAction(property2, AdditionPoint.EndOfParent, basicClass);
            var action2 = new AddPropertyToClassAction(property3, AdditionPoint.EndOfParent, basicClass);

            Actions actions = new Actions();
            actions.AddAction(action1);
            actions.AddAction(action2);

            string output = actions.RunActions(parser);
        	output = Slyce.Common.Utility.StandardizeLineBreaks(output, Environment.NewLine);
            Assert.That(output, Is.EqualTo(Code.Class1Plus2PropertiesText));
        }
    }
}
