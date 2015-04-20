using System.IO;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Delegate=ArchAngel.Providers.CodeProvider.DotNet.Delegate;

namespace Specs_For_CSharp_Parser
{
    [TestFixture]
    public class Basic_Tests
    {
        [Test]
        public void PrimitiveTypes_ShortNames()
        {
            const string code = @"public class Class1 
            {
                public int a = 0;
                public string b = """";
                public short c = 0;
                public long d = 0;
                public float e = 0;
                public double f = 0;
                public bool g = false;
                public char h = 'c';
				public byte i = 0;
				public object j = null;
				public decimal k = 0m;
            }";

            CSharpParser parser = new CSharpParser();
			parser.FormatSettings.ReorderBaseConstructs = false;
			parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Class clazz = (Class) codeRoot.WalkChildren()[0];

            Field con = (Field) clazz.WalkChildren()[0];
            Assert.That(con.Name, Is.EqualTo("a"));
            Assert.That(con.DataType.Name, Is.EqualTo("int"));

            con = (Field) clazz.WalkChildren()[1];
            Assert.That(con.Name, Is.EqualTo("b"));
            Assert.That(con.DataType.Name, Is.EqualTo("string"));

            con = (Field) clazz.WalkChildren()[2];
            Assert.That(con.Name, Is.EqualTo("c"));
            Assert.That(con.DataType.Name, Is.EqualTo("short"));

            con = (Field) clazz.WalkChildren()[3];
            Assert.That(con.Name, Is.EqualTo("d"));
            Assert.That(con.DataType.Name, Is.EqualTo("long"));

            con = (Field) clazz.WalkChildren()[4];
            Assert.That(con.Name, Is.EqualTo("e"));
            Assert.That(con.DataType.Name, Is.EqualTo("float"));

            con = (Field) clazz.WalkChildren()[5];
            Assert.That(con.Name, Is.EqualTo("f"));
            Assert.That(con.DataType.Name, Is.EqualTo("double"));

            con = (Field) clazz.WalkChildren()[6];
            Assert.That(con.Name, Is.EqualTo("g"));
            Assert.That(con.DataType.Name, Is.EqualTo("bool"));

            con = (Field) clazz.WalkChildren()[7];
            Assert.That(con.Name, Is.EqualTo("h"));
            Assert.That(con.DataType.Name, Is.EqualTo("char"));

			con = (Field)clazz.WalkChildren()[8];
			Assert.That(con.Name, Is.EqualTo("i"));
			Assert.That(con.DataType.Name, Is.EqualTo("byte"));

			con = (Field)clazz.WalkChildren()[9];
			Assert.That(con.Name, Is.EqualTo("j"));
			Assert.That(con.DataType.Name, Is.EqualTo("object"));

			con = (Field)clazz.WalkChildren()[10];
			Assert.That(con.Name, Is.EqualTo("k"));
			Assert.That(con.DataType.Name, Is.EqualTo("decimal"));
        }

        [Test]
        public void PrimitiveTypes_LongNames()
        {
            const string code = @"public class Class1 
            {
                public Int32 a = 0;
                public String b = """";
                public Int16 c = 0;
                public Int64 d = 0;
                public Single e = 0;
                public Double f = 0;
                public Bool g = false;
                public Char h = 'c';
				public Byte i = 0;
				public Object j = null;
				public Decimal k = 0.0m;
            }";
            
            CSharpParser parser = new CSharpParser();
			parser.FormatSettings.ReorderBaseConstructs = false;
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Class clazz = (Class) codeRoot.WalkChildren()[0];

            Field con = (Field) clazz.WalkChildren()[0];
            Assert.That(con.Name, Is.EqualTo("a"));
            Assert.That(con.DataType.Name, Is.EqualTo("Int32"));

            con = (Field) clazz.WalkChildren()[1];
            Assert.That(con.Name, Is.EqualTo("b"));
            Assert.That(con.DataType.Name, Is.EqualTo("String"));

            con = (Field) clazz.WalkChildren()[2];
            Assert.That(con.Name, Is.EqualTo("c"));
            Assert.That(con.DataType.Name, Is.EqualTo("Int16"));

            con = (Field) clazz.WalkChildren()[3];
            Assert.That(con.Name, Is.EqualTo("d"));
            Assert.That(con.DataType.Name, Is.EqualTo("Int64"));

            con = (Field) clazz.WalkChildren()[4];
            Assert.That(con.Name, Is.EqualTo("e"));
            Assert.That(con.DataType.Name, Is.EqualTo("Single"));

            con = (Field) clazz.WalkChildren()[5];
            Assert.That(con.Name, Is.EqualTo("f"));
            Assert.That(con.DataType.Name, Is.EqualTo("Double"));

            con = (Field) clazz.WalkChildren()[6];
            Assert.That(con.Name, Is.EqualTo("g"));
            Assert.That(con.DataType.Name, Is.EqualTo("Bool"));

            con = (Field) clazz.WalkChildren()[7];
            Assert.That(con.Name, Is.EqualTo("h"));
            Assert.That(con.DataType.Name, Is.EqualTo("Char"));

			con = (Field)clazz.WalkChildren()[8];
			Assert.That(con.Name, Is.EqualTo("i"));
			Assert.That(con.DataType.Name, Is.EqualTo("Byte"));

			con = (Field)clazz.WalkChildren()[9];
			Assert.That(con.Name, Is.EqualTo("j"));
			Assert.That(con.DataType.Name, Is.EqualTo("Object"));

			con = (Field)clazz.WalkChildren()[10];
			Assert.That(con.Name, Is.EqualTo("k"));
			Assert.That(con.DataType.Name, Is.EqualTo("Decimal"));
        }

        [Test]
        public void PrimitiveTypes_FullNames()
        {
            const string code = @"public class Class1 
            {
                public System.Int32 a = 0;
                public System.String b = """";
                public System.Int16 c = 0;
                public System.Int64 d = 0;
                public System.Single e = 0;
                public System.Double f = 0;
                public System.Bool g = false;
                public System.Char h = 'c';
				public System.Byte i = 0;
				public System.Object j = null;
				public System.Decimal k = 0m;
            }";

            CSharpParser parser = new CSharpParser();
			parser.FormatSettings.ReorderBaseConstructs = false;
            parser.ParseCodeAsync(code).WaitOne();

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Class clazz = (Class) codeRoot.WalkChildren()[0];

            Field con = (Field) clazz.WalkChildren()[0];
            Assert.That(con.Name, Is.EqualTo("a"));
            Assert.That(con.DataType.Name, Is.EqualTo("System.Int32"));

            con = (Field) clazz.WalkChildren()[1];
            Assert.That(con.Name, Is.EqualTo("b"));
            Assert.That(con.DataType.Name, Is.EqualTo("System.String"));

            con = (Field) clazz.WalkChildren()[2];
            Assert.That(con.Name, Is.EqualTo("c"));
            Assert.That(con.DataType.Name, Is.EqualTo("System.Int16"));

            con = (Field) clazz.WalkChildren()[3];
            Assert.That(con.Name, Is.EqualTo("d"));
            Assert.That(con.DataType.Name, Is.EqualTo("System.Int64"));

            con = (Field) clazz.WalkChildren()[4];
            Assert.That(con.Name, Is.EqualTo("e"));
            Assert.That(con.DataType.Name, Is.EqualTo("System.Single"));

            con = (Field) clazz.WalkChildren()[5];
            Assert.That(con.Name, Is.EqualTo("f"));
            Assert.That(con.DataType.Name, Is.EqualTo("System.Double"));

            con = (Field) clazz.WalkChildren()[6];
            Assert.That(con.Name, Is.EqualTo("g"));
            Assert.That(con.DataType.Name, Is.EqualTo("System.Bool"));

            con = (Field) clazz.WalkChildren()[7];
            Assert.That(con.Name, Is.EqualTo("h"));
            Assert.That(con.DataType.Name, Is.EqualTo("System.Char"));

			con = (Field)clazz.WalkChildren()[8];
			Assert.That(con.Name, Is.EqualTo("i"));
			Assert.That(con.DataType.Name, Is.EqualTo("System.Byte"));

			con = (Field)clazz.WalkChildren()[9];
			Assert.That(con.Name, Is.EqualTo("j"));
			Assert.That(con.DataType.Name, Is.EqualTo("System.Object"));

			con = (Field)clazz.WalkChildren()[10];
			Assert.That(con.Name, Is.EqualTo("k"));
			Assert.That(con.DataType.Name, Is.EqualTo("System.Decimal"));
        }

        [Test]
        public void Namespaces()
        {
            string code = @"namespace n1 { }";

            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Namespace n1 = (Namespace)codeRoot.WalkChildren()[0];

            Assert.That(n1.Name, Is.EqualTo("n1"));

            code = @"namespace n1 { namespace n2 { } }";

            parser = new CSharpParser();
            parser.ParseCode(code);

            codeRoot = parser.CreatedCodeRoot;
            n1 = (Namespace)codeRoot.WalkChildren()[0];
            Namespace n2 = n1.InnerNamespaces[0];

            Assert.That(n2.Name, Is.EqualTo("n2"));
            Assert.That(n2.FullyQualifiedName, Is.EqualTo("n1.n2"));
        }

        [Test]
        public void UsingStatements()
        {
            string code = @"using System;
namespace n1 { }";
            
            CSharpParser parser = new CSharpParser();
			parser.FormatSettings.ReorderBaseConstructs = false;
			parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Assert.That(((CodeRoot)codeRoot).UsingStatements[0].ToString(), Is.EqualTo("using System;"));
            Namespace n1 = (Namespace)codeRoot.WalkChildren()[0];

            Assert.That(n1.Name, Is.EqualTo("n1"));
            Assert.That(n1.UsingStatements, Is.Empty);

            code = @"using System;
namespace n1 
{ 
    using System.Web;
    namespace n2 
    {
        using Slyce.Common;
    }
}";

            parser = new CSharpParser();
			parser.ParseCode(code);

            codeRoot = parser.CreatedCodeRoot;
            Assert.That(((CodeRoot)codeRoot).UsingStatements[0].ToString(), Is.EqualTo("using System;"));

            n1 = (Namespace)codeRoot.WalkChildren()[0];
            Assert.That(n1.UsingStatements[0].ToString(), Is.EqualTo("using System.Web;"));

            Namespace n2 = n1.InnerNamespaces[0];

            Assert.That(n2.Name, Is.EqualTo("n2"));
            Assert.That(n2.FullyQualifiedName, Is.EqualTo("n1.n2"));
            Assert.That(n2.UsingStatements[0].ToString(), Is.EqualTo("using Slyce.Common;"));
        }



        [Test]
        public void Attributes()
        {
            const string code = @"
            namespace n1 
            { 
                [Serializable]
                [SomeAttribute(NamedParam = ""Nothing"")]
                public class Class1
                {

                }
                [Positional(1, ""string"")]
                public class Class2
                {

                }
            }";
            
            CSharpParser parser = new CSharpParser();
			parser.FormatSettings.ReorderBaseConstructs = false;
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Namespace n1 = (Namespace)codeRoot.WalkChildren()[0];

            Class clazz = n1.Classes[0];
            Assert.That(clazz.Attributes, Has.Count(2));
            Assert.That(clazz.Attributes[0].ToString(), Is.EqualTo("Serializable"));
            Assert.That(clazz.Attributes[1].ToString(), Is.EqualTo("SomeAttribute(NamedParam = \"Nothing\")"));

            clazz = n1.Classes[1];
            Assert.That(clazz.Attributes, Has.Count(1));
            Assert.That(clazz.Attributes[0].ToString(), Is.EqualTo("Positional(1, \"string\")"));
        }

		/// <summary>
		/// If this test fails, it may be because the Actipro parser source has been updated from
		/// Actipro, and the XML comment fix has not been applied. Goto CSharpRecursiveDescentLexicalParser.cs,
		/// find GetNextTokenCore() and look for this.documentationComment.Append(documentationComment.ToString().Trim() + " ");
		/// Change that " " to "\n".
		/// </summary>
        [Test]
        public void Comments()
        {
            const string code = @"
            /// <summary>
            /// Xml Comment line\r\n
			/// break.
            ///</summary>
            public class Class1 // Class Trailing Comment
            {
                // Comment1-1
                // Comment1-2
                public int i = 0; // Trailing Comment
                // Comment2-1
                public int j = 0;
            }";
            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;

            Assert.That(codeRoot, Is.InstanceOfType(typeof (CodeRoot)));
            Assert.That(codeRoot.WalkChildren(), Has.Count(1));

            Class clazz = (Class) codeRoot.WalkChildren()[0];
            Assert.That(clazz, Is.Not.Null);

            Assert.That(clazz.Name, Is.EqualTo("Class1"));
            Assert.That(clazz.XmlComments, Has.Count(4));
            Assert.That(clazz.XmlComments[0].Trim(), Is.EqualTo(@"<summary>"));
			Assert.That(clazz.XmlComments[1].Trim(), Is.EqualTo(@"Xml Comment line\r\n"));
			Assert.That(clazz.XmlComments[2].Trim(), Is.EqualTo(@"break."));
			Assert.That(clazz.XmlComments[3].Trim(), Is.EqualTo(@"</summary>"));
			
            Assert.That(clazz.Comments.TrailingComment, Is.EqualTo("// Class Trailing Comment"));
            
            Assert.That(clazz.WalkChildren(), Has.Count(2));
            Field field = (Field) clazz.WalkChildren()[0];
            Assert.That(field.Name, Is.EqualTo("i"));
            Assert.That(field.Comments.PreceedingComments, Has.Count(2));
            Assert.That(field.Comments.PreceedingComments[0], Is.EqualTo("// Comment1-1"));
            Assert.That(field.Comments.PreceedingComments[1], Is.EqualTo("// Comment1-2"));
            Assert.That(field.Comments.TrailingComment, Is.EqualTo("// Trailing Comment"));

            field = (Field)clazz.WalkChildren()[1];
            Assert.That(field.Name, Is.EqualTo("j"));
            Assert.That(field.Comments.PreceedingComments, Has.Count(1));
            Assert.That(field.Comments.PreceedingComments[0], Is.EqualTo("// Comment2-1"));
        }

        [Test]
        public void MultiLineComments()
        {
            const string code = @"
            /* adfjlaskdjflkasdjflkjdf */
            public class Class1 
            {
                /* Comment1-1
                 * Comment1-2 */
                public int i = 0; /* Trailing Comment
                * Comment2-1 */
                public int j = 0;
            }";
            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;

            Assert.That(codeRoot, Is.InstanceOfType(typeof(CodeRoot)));
            Assert.That(codeRoot.WalkChildren(), Has.Count(1));

            Class clazz = (Class)codeRoot.WalkChildren()[0];
            Assert.That(clazz, Is.Not.Null);

            Assert.That(clazz.Name, Is.EqualTo("Class1"));
            Assert.That(clazz.Comments.PreceedingComments, Has.Count(1));
            Assert.That(clazz.Comments.PreceedingComments[0].Trim(), Is.EqualTo("/* adfjlaskdjflkasdjflkjdf */"));

            Assert.That(clazz.WalkChildren(), Has.Count(2));
            Field field = (Field)clazz.WalkChildren()[0];
            Assert.That(field.Name, Is.EqualTo("i"));
            Assert.That(field.Comments.PreceedingComments, Has.Count(1));
            Assert.That(field.Comments.PreceedingComments[0], Is.EqualTo(@"/* Comment1-1
                 * Comment1-2 */"));
            Assert.That(field.Comments.TrailingComment, Is.EqualTo(@"/* Trailing Comment
                * Comment2-1 */"));

            field = (Field)clazz.WalkChildren()[1];
            Assert.That(field.Name, Is.EqualTo("j"));
            Assert.That(field.Comments.PreceedingComments, Has.Count(0));
        }

        [Test]
        public void Enums()
        {
            const string code = @"    
            public class Class1 
            {
                public enum Enumeration1
                {
                    One = 1, Two = 2
                }
            }";

            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Class clazz = (Class) codeRoot.WalkChildren()[0];

            Enumeration enu = (Enumeration) clazz.WalkChildren()[0];
            Assert.That(enu.Name, Is.EqualTo("Enumeration1"));
            Assert.That(enu.Modifiers, Has.Count(1));
            Assert.That(enu.Modifiers[0], Is.EqualTo("public"));
        }

        [Test]
        public void Events()
        {
            const string code = @"    
            public class Class1 
            {
                public event Delegate1 Event1;
            }
            ";

            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Class clazz = (Class) codeRoot.WalkChildren()[0];

            Event enu = (Event) clazz.WalkChildren()[0];
            Assert.That(enu.Name, Is.EqualTo("Event1"));
            Assert.That(enu.Modifiers, Has.Count(1));
            Assert.That(enu.Modifiers[0], Is.EqualTo("public"));
            Assert.That(enu.DataType.ToString(), Is.EqualTo("Delegate1"));
        }


        [Test]
        //[ExpectedException(ExceptionType = typeof(Exception), UserMessage = "We do not currently support add/remove accessors on events.")]
        [Ignore("We currently don't support add/remove accessors on events")]
        public void Events_Do_not_Support_Children()
        {
            const string code = @"    
            public class Class1 
            {
                public event Delegate1 Event1 { add {} remove{} };
            }
            ";

            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);
        }

        [Test]
        public void Constructors()
        {
            const string code = @"    
            public class Class1 
            {
                public Class1(string param1) 
                {
                }
            }
            ";

            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Class clazz = (Class) codeRoot.WalkChildren()[0];

            Constructor con = (Constructor) clazz.WalkChildren()[0];
            Assert.That(con.Name, Is.EqualTo("Class1"));
            Assert.That(con.Modifiers, Has.Count(1));
            Assert.That(con.Modifiers[0], Is.EqualTo("public"));
            Assert.That(con.Parameters, Has.Count(1));
            Assert.That(con.Parameters[0].Name, Is.EqualTo("param1"));
            Assert.That(con.Parameters[0].DataType, Is.EqualTo("string"));
            Assert.That(con.BodyText, Is.EqualTo("{\r\n}\r\n"));
        }

		[Test]
		public void GenericClasses()
		{
			const string code =
				@"    
            public class Class1<T> : IComparer<T>
            {
            }
            ";

			CSharpParser parser = new CSharpParser();
			parser.ParseCode(code);

			ICodeRoot codeRoot = parser.CreatedCodeRoot;
			Class clazz = (Class)codeRoot.WalkChildren()[0];

			Assert.That(clazz.Name, Is.EqualTo("Class1<T>"));
			Assert.That(clazz.BaseNames[0], Is.EqualTo("IComparer<T>"));
		}

		[Test]
		public void GenericClasses_WithConstraints()
		{
			const string code =
				@"    
            public class Class1<T> : IComparer<T> where T : Class2
            {
            }
            ";

			CSharpParser parser = new CSharpParser();
			parser.ParseCode(code);

			ICodeRoot codeRoot = parser.CreatedCodeRoot;
			Class clazz = (Class)codeRoot.WalkChildren()[0];

			Assert.That(clazz.Name, Is.EqualTo("Class1<T>"));
			Assert.That(clazz.BaseNames[0], Is.EqualTo("IComparer<T>"));
			Assert.That(clazz.GenericConstraintClause, Is.EqualTo("where T : Class2"));
		}

    	[Test]
        public void Interfaces()
        {
            const string code = @"    
            public class Class1 
            {
                public interface MarkerInterface { }
            }
            ";

            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Class clazz = (Class) codeRoot.WalkChildren()[0];

            Interface con = (Interface) clazz.WalkChildren()[0];
            Assert.That(con.Name, Is.EqualTo("MarkerInterface"));
            Assert.That(con.Modifiers, Has.Count(1));
            Assert.That(con.Modifiers[0], Is.EqualTo("public"));
        }

        [Test]
        public void Structures()
        {
            const string code = @"    
            public class Class1 
            {
                public struct Structure1 { }
            }
            ";

            CSharpParser parser = new CSharpParser();
			parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Class clazz = (Class) codeRoot.WalkChildren()[0];

            Struct con = (Struct) clazz.WalkChildren()[0];
            Assert.That(con.Name, Is.EqualTo("Structure1"));
            Assert.That(con.Modifiers, Has.Count(1));
            Assert.That(con.Modifiers[0], Is.EqualTo("public"));
        }

		[Test]
		public void Structures_MultipleFields()
		{
			const string code = @"    
            public class Class1 
            {
                public struct Structure1 
				{
					public int i;
					public int j;
				}
            }
            ";

			CSharpParser parser = new CSharpParser();
			parser.ParseCode(code);

			ICodeRoot codeRoot = parser.CreatedCodeRoot;
			Class clazz = (Class)codeRoot.WalkChildren()[0];

			Struct con = (Struct)clazz.WalkChildren()[0];
			Assert.That(con.Name, Is.EqualTo("Structure1"));
			Assert.That(con.Modifiers, Has.Count(1));
			Assert.That(con.Modifiers[0], Is.EqualTo("public"));

			Field f1 = con.Fields[0];
			Assert.That(f1.Name, Is.EqualTo("i"));
			Assert.That(f1.Modifiers, Has.Count(1));
			Assert.That(f1.Modifiers[0], Is.EqualTo("public"));
			Assert.That(f1.DataType.Name, Is.EqualTo("int"));

			Field f2 = con.Fields[1];
			Assert.That(f2.Name, Is.EqualTo("j"));
			Assert.That(f2.Modifiers, Has.Count(1));
			Assert.That(f2.Modifiers[0], Is.EqualTo("public"));
			Assert.That(f2.DataType.Name, Is.EqualTo("int"));
		}

        [Test]
        public void Delegates()
        {
            const string code = @"    
            public class Class1 
            {
                public delegate int Delegate1(string param1, Class1 p2);
            }
            ";

            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Class clazz = (Class) codeRoot.WalkChildren()[0];

            Delegate con = (Delegate) clazz.WalkChildren()[0];
            Assert.That(con.Name, Is.EqualTo("Delegate1"));
            Assert.That(con.Modifiers, Has.Count(1));
            Assert.That(con.Modifiers[0], Is.EqualTo("public"));
            Assert.That(con.ReturnType.Name, Is.EqualTo("int"));
            Assert.That(con.Parameters, Has.Count(2));
            Assert.That(con.Parameters[0].Name, Is.EqualTo("param1"));
            Assert.That(con.Parameters[0].DataType, Is.EqualTo("string"));
            Assert.That(con.Parameters[1].Name, Is.EqualTo("p2"));
            Assert.That(con.Parameters[1].DataType, Is.EqualTo("Class1"));
        }


        [Test]
        public void Fields()
        {
            const string code = @"    
            public class Class1 
            {
                public static int i = 0;
                public const float PI = 3.14159265358979323846;
            }
            ";

            CSharpParser parser = new CSharpParser();
			parser.FormatSettings.ReorderBaseConstructs = false;
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Class clazz = (Class) codeRoot.WalkChildren()[0];

            Field con = (Field) clazz.WalkChildren()[0];
            Assert.That(con.Name, Is.EqualTo("i"));
            Assert.That(con.Modifiers, Has.Count(2));
            Assert.That(con.DataType.Name, Is.EqualTo("int"));
            Assert.That(con.InitialValue, Is.EqualTo("0"));
            // Modifiers should be kept in order.
            Assert.That(con.Modifiers, Has.Count(2));
            Assert.That(con.Modifiers[0], Is.EqualTo("public"));
            Assert.That(con.Modifiers[1], Is.EqualTo("static"));

            con = (Field) clazz.WalkChildren()[1];
            Assert.That(con.Name, Is.EqualTo("PI"));
            Assert.That(con.Modifiers, Has.Count(2));
            Assert.That(con.DataType.Name, Is.EqualTo("float"));
            Assert.That(con.InitialValue, Is.EqualTo("3.14159265358979323846"));
            // Modifiers should be kept in order.
            Assert.That(con.Modifiers, Has.Count(2));
            Assert.That(con.Modifiers[0], Is.EqualTo("public"));
            Assert.That(con.Modifiers[1], Is.EqualTo("const"));
        }

        [Test]
        public void Interface_Events()
        {
            const string code = @"    
            public interface Interface1 
            {
                event Delegate1 Event1;
            }
            ";

            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Interface clazz = (Interface) codeRoot.WalkChildren()[0];

            InterfaceEvent enu = (InterfaceEvent) clazz.WalkChildren()[0];
            Assert.That(enu.Name, Is.EqualTo("Event1"));
            Assert.That(enu.DataType.ToString(), Is.EqualTo("Delegate1"));
        }

        [Test]
        public void Interface_Methods()
        {
            const string code = @"    
            public interface Interface1 
            {
                string Method1(int param, Interface1 i);
            }
            ";

            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Interface clazz = (Interface) codeRoot.WalkChildren()[0];

            InterfaceMethod inter = (InterfaceMethod) clazz.WalkChildren()[0];
            Assert.That(inter.Name, Is.EqualTo("Method1"));
            Assert.That(inter.ReturnType.ToString(), Is.EqualTo("string"));
            Assert.That(inter.Parameters, Has.Count(2));
            Assert.That(inter.Parameters[0].Name, Is.EqualTo("param"));
            Assert.That(inter.Parameters[0].DataType, Is.EqualTo("int"));
            Assert.That(inter.Parameters[1].Name, Is.EqualTo("i"));
            Assert.That(inter.Parameters[1].DataType, Is.EqualTo("Interface1"));
        }

		[Test]
		public void GenericInterfaceMethod_WithConstraints()
		{
			const string code = @"    
            public interface Interface1 
            {
                string Method1<T>(int param, T i) where T : class;
            }
            ";

			CSharpParser parser = new CSharpParser();
			parser.ParseCode(code);

			if(parser.ErrorOccurred)
				Assert.Fail(parser.GetFormattedErrors());

			ICodeRoot codeRoot = parser.CreatedCodeRoot;
			Interface clazz = (Interface)codeRoot.WalkChildren()[0];

			InterfaceMethod inter = (InterfaceMethod)clazz.WalkChildren()[0];
			Assert.That(inter.Name, Is.EqualTo("Method1"));
			Assert.That(inter.GenericParameters.Count, Is.EqualTo(1));
			Assert.That(inter.GenericParameters[0], Is.EqualTo("T"));
			Assert.That(inter.GenericConstraintClause, Is.EqualTo("where T : class"));
			Assert.That(inter.ReturnType.ToString(), Is.EqualTo("string"));
			Assert.That(inter.Parameters, Has.Count(2));
			Assert.That(inter.Parameters[0].Name, Is.EqualTo("param"));
			Assert.That(inter.Parameters[0].DataType, Is.EqualTo("int"));
			Assert.That(inter.Parameters[1].Name, Is.EqualTo("i"));
			Assert.That(inter.Parameters[1].DataType, Is.EqualTo("T"));
		}

        [Test]
        public void Interface_Properties()
        {
            const string code = @"    
            public interface Interface1 
            {
                string Property1 { get; set; }
            }
            ";

            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Interface clazz = (Interface) codeRoot.WalkChildren()[0];

            InterfaceProperty inter = (InterfaceProperty) clazz.WalkChildren()[0];
            Assert.That(inter.Name, Is.EqualTo("Property1"));
            Assert.That(inter.DataType.ToString(), Is.EqualTo("string"));
            Assert.That(inter.GetAccessor, Is.Not.Null);
            Assert.That(inter.SetAccessor, Is.Not.Null);
        }

        [Test]
        public void Interface_Indexer()
        {
            const string code = @"    
            public interface Interface1 
            {
                string this[int i] { get; set; }
            }
            ";

            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Interface clazz = (Interface) codeRoot.WalkChildren()[0];

            InterfaceIndexer inter = (InterfaceIndexer) clazz.WalkChildren()[0];
            Assert.That(inter.DataType.ToString(), Is.EqualTo("string"));
            Assert.That(inter.GetAccessor, Is.Not.Null);
            Assert.That(inter.SetAccessor, Is.Not.Null);
            Assert.That(inter.Parameters, Has.Count(1));
            Assert.That(inter.Parameters[0].Name, Is.EqualTo("i"));
            Assert.That(inter.Parameters[0].DataType, Is.EqualTo("int"));
        }

        [Test]
        public void Methods()
        {
            const string code = @"    
            public class Class1 
            {
                public void Class1(string param1) 
                {
                    int i = 0;
                }
            }
            ";

            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Class clazz = (Class) codeRoot.WalkChildren()[0];

            Function con = (Function) clazz.WalkChildren()[0];
            Assert.That(con.Name, Is.EqualTo("Class1"));
            Assert.That(con.ReturnType.ToString(), Is.EqualTo("void"));
            Assert.That(con.Modifiers, Has.Count(1));
            Assert.That(con.Modifiers[0], Is.EqualTo("public"));
            Assert.That(con.Parameters, Has.Count(1));
            Assert.That(con.Parameters[0].Name, Is.EqualTo("param1"));
            Assert.That(con.Parameters[0].DataType, Is.EqualTo("string"));
            Assert.That(con.BodyText, Is.EqualTo("{\r\n\tint i = 0;\r\n}\r\n"));
        }

		[Test]
		public void GenericMethod_WithConstraints()
		{
			const string code = @"    
            public class Class1 
            {
                public void Method1<T>(T param) where T : struct
				{

				}
            }
            ";

			CSharpParser parser = new CSharpParser();
			parser.ParseCode(code);

			if (parser.ErrorOccurred)
				Assert.Fail(parser.GetFormattedErrors());

			ICodeRoot codeRoot = parser.CreatedCodeRoot;
			Class clazz = (Class)codeRoot.WalkChildren()[0];

			Function function = (Function)clazz.WalkChildren()[0];
			Assert.That(function.Name, Is.EqualTo("Method1"));
			Assert.That(function.GenericParameters.Count, Is.EqualTo(1));
			Assert.That(function.GenericParameters[0], Is.EqualTo("T"));
			Assert.That(function.GenericConstraintClause, Is.EqualTo("where T : struct"));
			Assert.That(function.ReturnType.ToString(), Is.EqualTo("void"));
			Assert.That(function.Parameters, Has.Count(1));
			Assert.That(function.Parameters[0].Name, Is.EqualTo("param"));
			Assert.That(function.Parameters[0].DataType, Is.EqualTo("T"));
		}

        [Test]
        public void Operators()
        {
            const string code = @"    
            public class Class1 
            {
                public static int operator +(Class1 self, string param1)
                {
                    int i = 0;
                    return i;
                }
            }";

            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Class clazz = (Class) codeRoot.WalkChildren()[0];

            Operator con = (Operator) clazz.WalkChildren()[0];
            Assert.That(con.Name, Is.EqualTo("+"));
            Assert.That(con.DataType.ToString(), Is.EqualTo("int"));
            Assert.That(con.Modifiers, Has.Count(2));
            Assert.That(con.Modifiers[0], Is.EqualTo("public"));
            Assert.That(con.Modifiers[1], Is.EqualTo("static"));
            Assert.That(con.Parameters, Has.Count(2));
            Assert.That(con.Parameters[0].Name, Is.EqualTo("self"));
            Assert.That(con.Parameters[0].DataType, Is.EqualTo("Class1"));
            Assert.That(con.Parameters[1].Name, Is.EqualTo("param1"));
            Assert.That(con.Parameters[1].DataType, Is.EqualTo("string"));
            Assert.That(con.BodyText, Is.EqualTo("{\r\n\tint i = 0;\r\n\treturn i;\r\n}\r\n"));
        }

        [Test]
        public void Properties()
        {
            const string code = @"    
            public class Class1 
            {
                public static int SomeValue
                {
                    get { return i; }
                    set { }
                }
            }";

            CSharpParser parser = new CSharpParser();
            parser.FormatSettings.InlineSingleLineGettersAndSetters = true;
            parser.FormatSettings.PutBracesOnNewLines = false;
			parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Class clazz = (Class)codeRoot.WalkChildren()[0];

            Property con = (Property)clazz.WalkChildren()[0];
            Assert.That(con.Name, Is.EqualTo("SomeValue"));
            Assert.That(con.DataType.ToString(), Is.EqualTo("int"));
            Assert.That(con.Modifiers, Has.Count(2));
            Assert.That(con.Modifiers[0], Is.EqualTo("public"));
            Assert.That(con.Modifiers[1], Is.EqualTo("static"));
            Assert.That(con.GetAccessor, Is.Not.Null);
            Assert.That(con.GetAccessor.AccessorType, Is.EqualTo(PropertyAccessor.AccessorTypes.Get));
            Assert.That(con.GetAccessor.BodyText, Is.EqualTo("{ return i; }"));
            Assert.That(con.SetAccessor, Is.Not.Null);
            Assert.That(con.SetAccessor.AccessorType, Is.EqualTo(PropertyAccessor.AccessorTypes.Set));
            Assert.That(con.SetAccessor.BodyText, Is.EqualTo("{ }"));
        }

        [Test]
        public void Indexers()
        {
            const string code = @"    
            public class Class1 
            {
                public static int this[int i]
                {
                    get { return i; }
                    set { }
                }
            }";

            CSharpParser parser = new CSharpParser();
            parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
            Class clazz = (Class)codeRoot.WalkChildren()[0];

            Indexer con = (Indexer)clazz.WalkChildren()[0];
            Assert.That(con.Name, Is.Empty);
            Assert.That(con.DataType.ToString(), Is.EqualTo("int"));
            Assert.That(con.Parameters, Has.Count(1));
            Assert.That(con.Parameters[0].Name, Is.EqualTo("i"));
            Assert.That(con.Parameters[0].DataType, Is.EqualTo("int"));
            Assert.That(con.GetAccessor, Is.Not.Null);
            Assert.That(con.GetAccessor.AccessorType, Is.EqualTo(PropertyAccessor.AccessorTypes.Get));
            Assert.That(con.GetAccessor.BodyText, Is.EqualTo("{ return i; }"));
            Assert.That(con.SetAccessor, Is.Not.Null);
            Assert.That(con.SetAccessor.AccessorType, Is.EqualTo(PropertyAccessor.AccessorTypes.Set));
            Assert.That(con.SetAccessor.BodyText, Is.EqualTo("{ }"));
        }

        [Test]
        public void Regions()
        {
            const string code = @"
            namespace N1
            {
                #region Region Name
                public class Class1 
                {
                }
                #endregion
            }";

            CSharpParser parser = new CSharpParser();
			parser.ParseCode(code);

            ICodeRoot codeRoot = parser.CreatedCodeRoot;
        	Namespace ns = (Namespace)codeRoot.WalkChildren()[0];

        	Assert.That(ns.WalkChildren(), Has.Count(1));

			Region region = ns.SortedConstructs[0] as Region;
        	Assert.IsNotNull(region);
        	Assert.That(region.Name, Is.EqualTo("Region Name"));

        	Assert.That(region.WalkChildren(), Has.Count(1));
			Assert.That(region.SortedConstructs[0], Is.InstanceOfType(typeof(Class)));
        }
    }

	[TestFixture]
	public class Real_Code
	{
		[Test]
		public void Test_Script_Base_Class()
		{
			string resourcePath = "Resources/CSharp/ScriptBase.cs".Replace('/', Path.DirectorySeparatorChar);
			string fileText = File.ReadAllText(resourcePath);

			CSharpParser parser = new CSharpParser();
			parser.ParseCode(fileText);

			if(parser.ErrorOccurred)
				Assert.Fail("Parsing failed");

			CodeRoot codeRoot = (CodeRoot) parser.CreatedCodeRoot;

			Namespace ns = codeRoot.Namespaces[0];
			Class scriptBase = ns.Classes[0];

			Assert.That(scriptBase.Functions.Count, Is.EqualTo(13));
		}

		[Test]
		public void Test_Large_Switch_Single_Construct_Parse()
		{
			const string codeToParse = "public void Method1(){ string nullableType = isNullable ? \"?\" : \"\";\tswitch(sqlDataType.Trim().ToLower())\t{\t\tcase \"bigint\":\t\t\treturn \"long\" + nullableType;\t\tcase \"binary\":\t\t\treturn \"byte[]\";\t\tcase \"bit\":\t\t\treturn \"bool\" + nullableType;\t\tcase \"char\":\t\t\treturn \"string\";\t\tcase \"datetime\":\t\t\treturn \"DateTime\" + nullableType;\t\tcase \"decimal\":\t\t\treturn \"decimal\" + nullableType;\t\tcase \"float\":\t\t\treturn \"double\" + nullableType;\t\tcase \"image\":\t\t\treturn \"byte[]\";\t\tcase \"int\":\t\t\treturn \"int\" + nullableType;\t\tcase \"money\":\t\t\treturn \"decimal\" + nullableType;\t\tcase \"nchar\":\t\t\treturn \"string\";\t\tcase \"ntext\":\t\t\treturn \"string\";\t\tcase \"numeric\":\t\t\treturn \"decimal\" + nullableType;\t\tcase \"nvarchar\":\t\t\treturn \"string\";\t\tcase \"real\":\t\t\treturn \"float\" + nullableType;\t\tcase \"smalldatetime\":\t\t\treturn \"DateTime\" + nullableType;\t\tcase \"smallint\":\t\t\treturn \"short\" + nullableType;\t\tcase \"smallmoney\":\t\t\treturn \"decimal\" + nullableType;\t\tcase \"sql_variant\":\t\t\treturn \"object\";\t\tcase \"text\":\t\t\treturn \"string\";\t\tcase \"timestamp\":\t\t\treturn \"byte[]\";\t\tcase \"tinyint\":\t\t\treturn \"byte\" + nullableType;\t\tcase \"uniqueidentifier\":\t\t\treturn \"Guid\" + nullableType;\t\tcase \"varbinary\":\t\t\treturn \"byte[]\";\t\tcase \"varchar\":\t\t\treturn \"string\";\t\tcase \"xml\":\t\t\treturn \"string\";\t\tdefault:\t\t\tthrow new Exception(sqlDataType + \" data type not supported by the ArchAngel.Providers.Database.Model API yet. Please contact Slyce support: support@slyce.com\"); } }";

			CSharpParser csf = new CSharpParser();
			Function f = (Function) csf.ParseSingleConstruct(codeToParse, BaseConstructType.MethodDeclaration);
			Assert.That(csf.ErrorOccurred, Is.False);
			Assert.That(f.Name, Is.EqualTo("Method1"));
		}

		[Test]
		public void Test_Generic_Cast()
		{
			const string codeToParse = "using System;\r\nusing System.Collections.Generic;\r\n\r\n// References to BioControl.Framework specific libraries\r\nusing BioControl.Framework.Model;\r\nusing BioControl.Framework.SQLServerDAL;\r\n\r\nnamespace BioControl.Framework.BLL\r\n{\r\n\t/// <summary>\r\n\t/// A business component used to manage Compositions\r\n\t/// The BioControl.Framework.Model.Composition is used to store\r\n\t/// serializable information about a specific Composition\r\n\t/// </summary>\r\n\tpublic sealed partial class Composition\r\n\t{\r\n\t\t// Get an instance of the Composition DAL using the DALFactory\r\n        // Making this static will cache the DAL instance after the initial load\r\n        private static readonly SQLServerDAL.Composition dal = new SQLServerDAL.Composition();\r\n\r\n\t\t/// <summary>\r\n\t\t/// Default constructor\r\n\t\t/// </summary>\r\n\t\tprivate Composition()\r\n\t\t{\r\n\t\t}\r\n\r\n\t\t/// <summary>\r\n\t\t/// Get a list of Compositions\r\n\t\t/// </summary>\r\n\t\t/// <returns>Arraylist of Compositions</returns>\r\n\t\tpublic static IList<CompositionInfo> GetCompositions()\r\n\t\t{\r\n\t\t\t// Run a search against the data store\r\n\t\t\treturn dal.GetCompositions();\r\n\t\t}\r\n\t\t\r\n\t\t/// <summary>\r\n\t\t/// Get a list of sorted Compositions\r\n\t\t/// </summary>\r\n\t\t/// <returns>Arraylist of Compositions</returns>\r\n\t\tpublic static IList<CompositionInfo> GetCompositionsSorted(string sortExpression)\r\n\t\t{\t\t\t\r\n\t\t\tif (string.IsNullOrEmpty(sortExpression))\r\n            {\r\n\t\t\t\treturn GetCompositions();\r\n            }\r\n\r\n            var listSortDirection = System.ComponentModel.ListSortDirection.Ascending;\r\n            string[] sortExpressionParams = sortExpression.Split(' ');\r\n            string sortFieldName = sortExpressionParams[0];\r\n            if (sortExpressionParams.Length > 1)\r\n            {\r\n                string direction = sortExpressionParams[1];\r\n                if (direction.ToUpper() == \"DESC\")\r\n                {\r\n                   listSortDirection = System.ComponentModel.ListSortDirection.Descending;\r\n                }\r\n            }\r\n\r\n            // Run a search against the data store\r\n            var compositions = (List<CompositionInfo>)dal.GetCompositions();\r\n\r\n            var comparer = new Model.SortComparer<CompositionInfo>(sortFieldName, listSortDirection);\r\n            compositions.Sort(comparer);\r\n\r\n            return (IList<CompositionInfo>)compositions;\r\n\t\t}\r\n\r\n\t\t/// <summary>\r\n\t\t/// Search for a specific Composition given its unique contraints\r\n\t\t/// </summary>\r\n\t\t/// <param name=\"compositionID\">Constraint for a Composition</param>\r\n\t\t/// <returns>A specific Composition business entity</returns>\r\n\t\tpublic static CompositionInfo GetComposition(int compositionID)\r\n\t\t{\r\n\t\t\t// Use the dal to search by unique constraint\r\n\t\t\treturn dal.GetComposition(compositionID);\r\n\t\t}\r\n\r\n\t\t/// <summary>\r\n\t\t/// Get a filtered list of Compositions\r\n\t\t/// </summary>\r\n\t\t/// <param name=\"fieldName\">Database Field to filter on</param>\r\n\t\t/// <param name=\"operatorValue\">SQL boolean operator (like, =, <, >, <>, >=, <=)</param>\r\n\t\t/// <param name=\"fieldValue\">Data to search for</param>\r\n\t\t/// <returns>Arraylist of Compositions</returns>\r\n\t\tpublic static IList<CompositionInfo> GetCompositionsByFilter(string fieldName, string operatorValue, string fieldValue, string sortExpression)\r\n\t\t{\r\n\t\t\t// Return new if the string is empty\r\n\t\t\tif (string.IsNullOrEmpty(fieldName) || string.IsNullOrEmpty(operatorValue) || string.IsNullOrEmpty(fieldValue))\r\n\t\t\t{\r\n\t\t\t\treturn GetCompositionsSorted(sortExpression);\r\n\t\t\t}\r\n\t\t\t\r\n\t\t\tif (string.IsNullOrEmpty(sortExpression))\r\n            {\r\n\t\t\t\treturn dal.GetCompositionsByFilter(fieldName, operatorValue, fieldValue);\r\n            }\r\n\r\n            var listSortDirection = System.ComponentModel.ListSortDirection.Ascending;\r\n            string[] sortExpressionParams = sortExpression.Split(' ');\r\n            string sortFieldName = sortExpressionParams[0];\r\n            if (sortExpressionParams.Length > 1)\r\n            {\r\n                string direction = sortExpressionParams[1];\r\n                if (direction.ToUpper() == \"DESC\")\r\n                {\r\n                    listSortDirection = System.ComponentModel.ListSortDirection.Descending;\r\n                }\r\n            }\r\n\r\n            // Run a search against the data store\r\n            var compositions = (List<CompositionInfo>)dal.GetCompositionsByFilter(fieldName, operatorValue, fieldValue);\r\n\r\n            var comparer = new Model.SortComparer<CompositionInfo>(sortFieldName, listSortDirection);\r\n            compositions.Sort(comparer);\r\n\t\t\t\r\n\t\t\treturn (IList<CompositionInfo>)compositions;\r\n\t\t}\r\n\r\n\t\t/// <summary>\r\n\t\t/// Insert a new Composition\r\n\t\t/// </summary>\r\n\t\t/// <param name=\"composition\">A specific Composition entity with information about the new Composition</param>\r\n\t\tpublic static void Insert(CompositionInfo composition)\r\n\t\t{\r\n\t\t\t// Send the new Composition information to the DAL\r\n\t\t\tdal.Insert(composition);\r\n\t\t}\r\n\r\n\t\t/// <summary>\r\n\t\t/// Update an existing Composition\r\n\t\t/// </summary>\r\n\t\t/// <param name=\"composition\">A specific Composition entity with information about the Composition to be updated</param>\r\n\t\tpublic static void Update(CompositionInfo composition)\r\n\t\t{\r\n\t\t\t// Send the updated Composition information to the DAL\r\n\t\t\tdal.Update(composition);\r\n\t\t}\r\n\r\n\t\t/// <summary>\r\n\t\t/// Delete an existing Composition\r\n\t\t/// </summary>\r\n\t\t/// <param name=\"composition\">A specific Composition entity with information about the Composition to be updated</param>\r\n\t\tpublic static void Delete(CompositionInfo composition)\r\n\t\t{\r\n\t\t\t// Send the Composition information to the DAL\r\n\t\t\tdal.Delete(composition);\r\n\t\t}\r\n\t}\r\n}";

			CSharpParser csf = new CSharpParser();
			csf.ParseCode(codeToParse);
			Assert.That(csf.ErrorOccurred, Is.False);
		}
	}	
}