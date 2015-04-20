using System;
using ArchAngel.Providers;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.DotNet;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Delegate=ArchAngel.Providers.CodeProvider.DotNet.Delegate;

namespace Specs_For_Single_Base_Construct_Parsing
{
	[TestFixture]
	public class When_Parsing_Invalid_Code
	{
		[Test]
		public void Parser_Exception_Thrown()
		{
			CSharpParser parser = new CSharpParser();
			try
			{
				const string code = "Nonsense, and more nonsense.";

				parser.ParseSingleConstruct(code, BaseConstructType.ConstructorDeclaration);

				Assert.Fail("Parser exception not thrown");
			}
			catch(ParserException)
			{
				// Everything went according to plan
			}

			Assert.That(parser.ErrorOccurred);
			Assert.That(parser.SyntaxErrors.Count, Is.GreaterThan(0));
		}
	}

	[TestFixture]
	public class When_Parsing_Valid_Code
	{
		[Test]
		public void A_Constructor_Is_Created()
		{
			// This should not be the name of the class 
			const string code = "public MadeUpClassName() { }";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.ConstructorDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(Constructor)));

			Constructor con = (Constructor)bc;

			Assert.That(con.Name, Is.EqualTo("MadeUpClassName"));
			Assert.That(con.Parameters.Count, Is.EqualTo(0));
			Assert.That(con.BodyText, Is.EqualTo("{\n}\n".Replace("\n", Environment.NewLine)));
			Assert.That(con.Modifiers.Count, Is.EqualTo(1));
			Assert.That(con.Modifiers[0], Is.EqualTo("public"));
		}

		[Test]
		public void A_Destructor_Is_Created()
		{
			// This should not be the name of the class 
			const string code = "public ~MadeUpClassName() { }";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.DestructorDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(Destructor)));

			Destructor con = (Destructor)bc;

			Assert.That(con.Name, Is.EqualTo("MadeUpClassName"));
			Assert.That(con.BodyText, Is.EqualTo("{\n}\n".Replace("\n", Environment.NewLine)));
		}

		[Test]
		public void A_Delegate_Is_Created()
		{
			const string code = "public delegate int Delegate1();";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.DelegateDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(Delegate)));

			Delegate con = (Delegate)bc;

			Assert.That(con.Name, Is.EqualTo("Delegate1"));
			Assert.That(con.Parameters.Count, Is.EqualTo(0));
			Assert.That(con.ReturnType.Name, Is.EqualTo("int"));
			Assert.That(con.Modifiers.Count, Is.EqualTo(1));
			Assert.That(con.Modifiers[0], Is.EqualTo("public"));
		}

		[Test]
		public void A_Constant_Is_Created()
		{
			const string code = "public const string Key = \"12345\";";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.FieldDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(Field)));

			Field con = (Field)bc;

			Assert.That(con.Name, Is.EqualTo("Key"));
			Assert.That(con.Modifiers.Count, Is.EqualTo(2));
			Assert.That(con.Modifiers[0], Is.EqualTo("public"));
			Assert.That(con.Modifiers[1], Is.EqualTo("const"));
			Assert.That(con.DataType.Name, Is.EqualTo("string"));
			Assert.That(con.InitialValue, Is.EqualTo("\"12345\""));
		}

		[Test]
		public void A_Field_Is_Created()
		{
			const string code = "public string Key = \"12345\";";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.FieldDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(Field)));

			Field con = (Field)bc;

			Assert.That(con.Name, Is.EqualTo("Key"));
			Assert.That(con.Modifiers.Count, Is.EqualTo(1));
			Assert.That(con.Modifiers[0], Is.EqualTo("public"));
			Assert.That(con.DataType.Name, Is.EqualTo("string"));
			Assert.That(con.InitialValue, Is.EqualTo("\"12345\""));
		}

		[Test]
		public void An_Event_Is_Created()
		{
			const string code = "public event Delegate1 Event1;";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.EventDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(Event)));

			Event con = (Event)bc;

			Assert.That(con.Name, Is.EqualTo("Event1"));
			Assert.That(con.Modifiers.Count, Is.EqualTo(1));
			Assert.That(con.Modifiers[0], Is.EqualTo("public"));
			Assert.That(con.DataType.Name, Is.EqualTo("Delegate1"));
		}

		[Test]
		public void A_Method_Is_Created()
		{
			const string code = "public static SomeDataType Method1(int i) { }";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.MethodDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(Function)));

			Function con = (Function)bc;

			Assert.That(con.Name, Is.EqualTo("Method1"));
			Assert.That(con.Modifiers.Count, Is.EqualTo(2));
			Assert.That(con.Modifiers[0], Is.EqualTo("public"));
			Assert.That(con.Modifiers[1], Is.EqualTo("static"));
			Assert.That(con.ReturnType.Name, Is.EqualTo("SomeDataType"));
			Assert.That(con.Parameters.Count, Is.EqualTo(1));
			Assert.That(con.Parameters[0].DataType, Is.EqualTo("int"));
			Assert.That(con.Parameters[0].Name, Is.EqualTo("i"));
		}

		[Test]
		public void An_Operator_Is_Created()
		{
			const string code = "public static int operator+(int i) { }";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.OperatorDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(Operator)));

			Operator con = (Operator)bc;

			Assert.That(con.Name, Is.EqualTo("+"));
			Assert.That(con.Modifiers.Count, Is.EqualTo(2));
			Assert.That(con.Modifiers[0], Is.EqualTo("public"));
			Assert.That(con.Modifiers[1], Is.EqualTo("static"));
			Assert.That(con.DataType.Name, Is.EqualTo("int"));
			Assert.That(con.Parameters.Count, Is.EqualTo(1));
			Assert.That(con.Parameters[0].DataType, Is.EqualTo("int"));
			Assert.That(con.Parameters[0].Name, Is.EqualTo("i"));
		}

		[Test]
		public void A_Property_Is_Created()
		{
			const string code = "public int Property1 { get {return 1; } }";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.PropertyDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(Property)));

			Property con = (Property)bc;

			Assert.That(con.Name, Is.EqualTo("Property1"));
			Assert.That(con.Modifiers.Count, Is.EqualTo(1));
			Assert.That(con.Modifiers[0], Is.EqualTo("public"));
			Assert.That(con.DataType.Name, Is.EqualTo("int"));
			Assert.That(con.GetAccessor, Is.Not.Null);
			Assert.That(con.SetAccessor, Is.Null);
			Assert.That(con.GetAccessor.BodyText, Is.EqualTo("{ return 1; }"));
		}

		[Test]
		public void A_Class_Is_Created()
		{
			const string code = "public class Class1 { }";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.ClassDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(Class)));

			Class con = (Class)bc;

			Assert.That(con.Name, Is.EqualTo("Class1"));
			Assert.That(con.Modifiers.Count, Is.EqualTo(1));
			Assert.That(con.Modifiers[0], Is.EqualTo("public"));
		}

		[Test]
		public void An_Interface_Is_Created()
		{
			const string code = "public interface Interface1 { }";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.InterfaceDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(Interface)));

			Interface con = (Interface)bc;

			Assert.That(con.Name, Is.EqualTo("Interface1"));
			Assert.That(con.Modifiers.Count, Is.EqualTo(1));
			Assert.That(con.Modifiers[0], Is.EqualTo("public"));
		}

		[Test]
		public void A_Namespace_Is_Created()
		{
			const string code = "namespace Namespace1 { }";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.NamespaceDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(Namespace)));

			Namespace con = (Namespace)bc;

			Assert.That(con.Name, Is.EqualTo("Namespace1"));
		}

		[Test]
		public void A_Structure_Is_Created()
		{
			const string code = "public struct Structure1 { }";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.StructureDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(Struct)));

			Struct con = (Struct)bc;

			Assert.That(con.Name, Is.EqualTo("Structure1"));
		}

		[Test]
		public void A_UsingStatement_Is_Created()
		{
			const string code = "using System;";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.UsingDirective);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(UsingStatement)));

			UsingStatement con = (UsingStatement)bc;

			Assert.That(con.Value, Is.EqualTo("System"));
		}

		[Test]
		public void An_InterfaceMethod_Is_Created()
		{
			const string code = "void Method1();";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.InterfaceMethodDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(InterfaceMethod)));

			InterfaceMethod con = (InterfaceMethod)bc;

			Assert.That(con.Name, Is.EqualTo("Method1"));
			Assert.That(con.ReturnType.Name, Is.EqualTo("void"));
		}

		[Test]
		public void An_InterfaceProperty_Is_Created()
		{
			const string code = "int Property1 { get; set; }";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.InterfacePropertyDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(InterfaceProperty)));

			InterfaceProperty con = (InterfaceProperty)bc;

			Assert.That(con.Name, Is.EqualTo("Property1"));
			Assert.That(con.DataType.Name, Is.EqualTo("int"));
			Assert.That(con.GetAccessor, Is.Not.Null);
			Assert.That(con.SetAccessor, Is.Not.Null);
		}

		[Test]
		public void An_InterfaceEvent_Is_Created()
		{
			const string code = "event Delegate1 Event1;";

			CSharpParser parser = new CSharpParser();
			IBaseConstruct bc = parser.ParseSingleConstruct(code, BaseConstructType.InterfaceEventDeclaration);

			Assert.That(bc, Is.Not.Null);
			Assert.That(bc, Is.InstanceOfType(typeof(InterfaceEvent)));

			InterfaceEvent con = (InterfaceEvent)bc;

			Assert.That(con.Name, Is.EqualTo("Event1"));
			Assert.That(con.DataType.Name, Is.EqualTo("Delegate1"));
		}
	}
}