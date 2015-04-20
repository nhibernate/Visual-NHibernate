using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using AttributeSection=ArchAngel.Providers.CodeProvider.AttributeSection;
using Controller=ArchAngel.Providers.CodeProvider.DotNet.Controller;

namespace Specs_For_CSharp_BaseConstructs.UID
{
    [TestFixture]
    public class Base_Construct
    {
        private Controller controller;

        [SetUp]
        public void Setup()
        {
            controller = new CSharpController();
        }

        [Test]
        public void Attribute()
        {
            Attribute attr = new Attribute(controller);
            attr.Name = "Test";

            Assert.That(attr.FullyQualifiedIdentifer, Is.EqualTo("[Test]"));
            
            AttributeSection attrSec = new AttributeSection(controller);
            attrSec.AddChild(attr);
            Class cl = new Class(controller, "Class1");
            cl.AddAttributeSection(attrSec);

            Assert.That(attr.FullyQualifiedIdentifer, Is.EqualTo("Class1|[Test]"));
        }

        [Test]
        public void Class()
        {
            Class cl = new Class(controller, "Class1");
            Assert.That(cl.FullyQualifiedIdentifer, Is.EqualTo("Class1"));

            Namespace ns = new Namespace(controller);
            ns.Name = "ns1";
            ns.AddChild(cl);

            Assert.That(cl.FullyQualifiedIdentifer, Is.EqualTo("ns1|Class1"));
        }

        [Test]
        public void Constant()
        {
        	Constant con = new Constant(controller, "VALUE_1", new DataType(controller, "int"));
            Assert.That(con.FullyQualifiedIdentifer, Is.EqualTo("VALUE_1"));

			Class cl = new Class(controller, "Class1");
            cl.AddChild(con);

            Assert.That(con.FullyQualifiedIdentifer, Is.EqualTo("Class1|VALUE_1"));
        }

		[Test]
		public void Constructor()
		{
			Constructor con = new Constructor(controller, "Class1");
			con.Parameters.Add(new Parameter(controller, "int", "i"));
			con.Parameters.Add(new Parameter(controller, "string", "j"));
			Assert.That(con.FullyQualifiedIdentifer, Is.EqualTo("Class1 (int, string)"));

			Class cl = new Class(controller, "Class1");
			cl.AddChild(con);

			Assert.That(con.FullyQualifiedIdentifer, Is.EqualTo("Class1|Class1 (int, string)"));
		}

		[Test]
		public void Delegate()
		{
			Delegate item = new Delegate(controller, "Delegate1", new DataType(controller, "void"));
			item.Parameters.Add(new Parameter(controller, "int", "i"));
			item.Parameters.Add(new Parameter(controller, "string", "j"));
			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Delegate1 (int, string)"));

			Class cl = new Class(controller, "Class1");
			cl.AddChild(item);

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Class1|Delegate1 (int, string)"));
		}

		[Test]
		public void Enumeration()
		{
			Enumeration item = new Enumeration(controller, "FileTypes");
			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("FileTypes"));

			Class cl = new Class(controller, "Class1");
			cl.AddChild(item);

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Class1|FileTypes"));
		}

		[Test]
		public void EnumMember()
		{
			Enumeration en = new Enumeration(controller, "FileTypes");
			Enumeration.EnumMember item = new Enumeration.EnumMember(controller, "CSharp");
			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("CSharp"));
			en.AddChild(item);

			Class cl = new Class(controller, "Class1");
			cl.AddChild(en);

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Class1|FileTypes|CSharp"));
		}

		[Test]
		public void Event()
		{
			Event item = new Event(controller, new DataType(controller, "Delegate1"), "Event1", "public");
			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Event1"));

			Class cl = new Class(controller, "Class1");
			cl.AddChild(item);

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Class1|Event1"));
		}

		[Test]
		public void Field()
		{
			Field item = new Field(controller, new DataType(controller, "int"), "Field1", "public");
			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Field1"));

			Class cl = new Class(controller, "Class1");
			cl.AddChild(item);

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Class1|Field1"));
		}

		[Test]
		public void Function()
		{
			Function item = new Function(controller, "Function1", new DataType(controller, "ReturnObjectOfSomeKind"));
			item.Parameters.Add(new Parameter(controller, "float", "f"));
			item.Parameters.Add(new Parameter(controller, "InputObject", "j"));
			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Function1 (float, InputObject)"));

			Class cl = new Class(controller, "Class1");
			cl.AddChild(item);

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Class1|Function1 (float, InputObject)"));
		}

		[Test]
		public void Indexer()
		{
			Indexer item = new Indexer(controller);
			item.Parameters.Add(new Parameter(controller, "float", "f"));
			item.Parameters.Add(new Parameter(controller, "InputObject", "j"));
			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Indexer [float, InputObject]"));

			Class cl = new Class(controller, "Class1");
			cl.AddChild(item);

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Class1|Indexer [float, InputObject]"));
		}

		[Test]
		public void Interface()
		{
			Interface item = new Interface(controller, "Interface1");
			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Interface1"));

			Namespace ns = new Namespace(controller);
			ns.Name = "ns1";
			ns.AddChild(item);

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("ns1|Interface1"));
		}

		[Test]
		public void InterfaceEvent()
		{
			Interface it = new Interface(controller, "Interface1");
			InterfaceEvent item = new InterfaceEvent(controller, "Event1", new DataType(controller, "Delegate1"), false);
			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Event1"));

			it.AddChild(item);
			Namespace ns = new Namespace(controller);
			ns.Name = "ns1";
			ns.AddChild(it);

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("ns1|Interface1|Event1"));
		}

		[Test]
		public void InterfaceIndexer()
		{
			Interface it = new Interface(controller, "Interface1");
			InterfaceIndexer item = new InterfaceIndexer(controller, new DataType(controller, "int"), false);
			item.Parameters.Add(new Parameter(controller, "float", "f"));
			item.Parameters.Add(new Parameter(controller, "InputObject", "j"));
			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Indexer [float, InputObject]"));

			it.AddChild(item);
			Namespace ns = new Namespace(controller);
			ns.Name = "ns1";
			ns.AddChild(it);

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("ns1|Interface1|Indexer [float, InputObject]"));
		}

		[Test]
		public void InterfaceMethod()
		{
			Interface it = new Interface(controller, "Interface1");
			InterfaceMethod item = new InterfaceMethod(controller, "Function1", new DataType(controller, "int"));
			item.Parameters.Add(new Parameter(controller, "float", "f"));
			item.Parameters.Add(new Parameter(controller, "InputObject", "j"));
			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Function1 (float, InputObject)"));

			it.AddChild(item);
			Namespace ns = new Namespace(controller);
			ns.Name = "ns1";
			ns.AddChild(it);

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("ns1|Interface1|Function1 (float, InputObject)"));
		}

		[Test]
		public void InterfaceProperty()
		{
			Interface it = new Interface(controller, "Interface1");
			InterfaceProperty item = new InterfaceProperty(controller, "Property1", new DataType(controller, "int"));
			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Property1"));

			it.AddChild(item);
			Namespace ns = new Namespace(controller);
			ns.Name = "ns1";
			ns.AddChild(it);

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("ns1|Interface1|Property1"));
		}

		[Test]
		public void InterfaceAccessor()
		{
			InterfaceProperty itp = new InterfaceProperty(controller, "Property1", new DataType(controller, "int"));
			InterfaceAccessor item1 = new InterfaceAccessor(controller, ArchAngel.Providers.CodeProvider.DotNet.InterfaceAccessor.AccessorTypes.Get, "");
            InterfaceAccessor item2 = new InterfaceAccessor(controller, ArchAngel.Providers.CodeProvider.DotNet.InterfaceAccessor.AccessorTypes.Set, "");

			itp.AddChild(item1);
			itp.AddChild(item2);
			
			Assert.That(item1.FullyQualifiedIdentifer, Is.EqualTo("Property1|get"));
			Assert.That(item2.FullyQualifiedIdentifer, Is.EqualTo("Property1|set"));

			Interface it = new Interface(controller, "Interface1");
			it.AddChild(itp);
			Namespace ns = new Namespace(controller);
			ns.Name = "ns1";
			ns.AddChild(it);

			Assert.That(item1.FullyQualifiedIdentifer, Is.EqualTo("ns1|Interface1|Property1|get"));
			Assert.That(item2.FullyQualifiedIdentifer, Is.EqualTo("ns1|Interface1|Property1|set"));
		}

		[Test]
		public void Namespace()
		{
			Namespace item = new Namespace(controller, "Namespace1");
			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Namespace1"));

			Namespace ns = new Namespace(controller);
			ns.Name = "ns1";
			ns.AddChild(item);

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("ns1|Namespace1"));
		}

		[Test]
		public void Operator()
		{
			Operator item = new Operator(controller, "+", new DataType(controller, "int"), "public");
			item.Parameters.Add(new Parameter(controller, "Class1", "this"));
			item.Parameters.Add(new Parameter(controller, "InputObject", "j"));

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("+ (Class1, InputObject)"));

			Class cl = new Class(controller, "Class1");
			cl.AddChild(item);

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Class1|+ (Class1, InputObject)"));
		}

		[Test]
		public void Property()
		{
			Property item = new Property(controller, "Property1", new DataType(controller, "int"), "public");
			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Property1"));

			Class cl = new Class(controller, "Class1");
			cl.AddChild(item);

			Assert.That(item.FullyQualifiedIdentifer, Is.EqualTo("Class1|Property1"));
		}

		[Test]
		public void PropertyAccessor()
		{
			Property itp = new Property(controller, "Property1", new DataType(controller, "int"), "public");
			PropertyAccessor item1 = new PropertyAccessor(controller, ArchAngel.Providers.CodeProvider.DotNet.PropertyAccessor.AccessorTypes.Get);
			PropertyAccessor item2 = new PropertyAccessor(controller, ArchAngel.Providers.CodeProvider.DotNet.PropertyAccessor.AccessorTypes.Set);

			itp.AddChild(item1);
			itp.AddChild(item2);

			Assert.That(item1.FullyQualifiedIdentifer, Is.EqualTo("Property1|get"));
			Assert.That(item2.FullyQualifiedIdentifer, Is.EqualTo("Property1|set"));

			Class it = new Class(controller, "Class1");
			it.AddChild(itp);

			Assert.That(item1.FullyQualifiedIdentifer, Is.EqualTo("Class1|Property1|get"));
			Assert.That(item2.FullyQualifiedIdentifer, Is.EqualTo("Class1|Property1|set"));
		}

		[Test]
		public void Struct()
		{
			Struct cl = new Struct(controller, "Struct1");
			Assert.That(cl.FullyQualifiedIdentifer, Is.EqualTo("Struct1"));

			Namespace ns = new Namespace(controller);
			ns.Name = "ns1";
			ns.AddChild(cl);

			Assert.That(cl.FullyQualifiedIdentifer, Is.EqualTo("ns1|Struct1"));
		}
    }
}
