using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.DotNet;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using CSharpController=ArchAngel.Providers.CodeProvider.CSharp.CSharpController;

namespace Specs_For_CSharp_BaseConstructs.Clone
{
    [TestFixture]
    public class Specs_For_Base_Construct_Clone
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
            Attribute inter = new Attribute(controller);
            inter.PositionalArguments.Add("asdfsadf");

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void AttributeSection()
        {
			AttributeSection inter = new AttributeSection(controller);
            inter.Target = "";

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void Class()
        {
			Class inter = new Class(controller, "Class1");
            inter.IsPartial = true;
            inter.GenericTypes.Add("T");

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void Constant()
        {
			Constant inter = new Constant(controller);
            inter.DataType = new DataType(controller,"int");
            inter.Modifiers.Add("public");
            inter.Name = "CONSTANT1";

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void Constructor()
        {
			Constructor inter = new Constructor(controller);
            inter.Modifiers.Add("public");
            inter.Name = "Class1";
			Parameter param = new Parameter(controller);
            param.Name = "i";
            param.DataType = "int";
            inter.Parameters.Add(param);

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        // Technically this is not a base construct, but it is important that this clone method
        // works or a lot of the BaseConstruct ones won't
        [Test]
        public void DataType()
        {
            DataType inter = new DataType(controller, "File");
            inter.Alias = "Alias";
            DataType gen = new DataType(controller, "T");
            inter.GenericParameters.Add(gen);

            Assert.That(inter == inter.Clone(), Is.True);
        }

        [Test]
        public void Delegate()
        {
			Delegate inter = new Delegate(controller);
            inter.Name = "File";
            inter.Modifiers.Add("public");
            inter.ReturnType = new DataType(controller,"int");
			Parameter param = new Parameter(controller);
            param.Name = "i";
            param.DataType = "int";
            inter.Parameters.Add(param);

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void EmptyPlaceholder()
        {
			EmptyPlaceholder inter = new EmptyPlaceholder(controller, "asdfasd", 2);

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void Enumeration()
        {
			Enumeration inter = new Enumeration(controller);
            inter.Name = "File";
            inter.Modifiers.Add("public");
			inter.Members.Add(new Enumeration.EnumMember(controller, "Read"));
			inter.Members.Add(new Enumeration.EnumMember(controller, "Write"));

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
            Assert.That(inter.Clone().WalkChildren(), Has.Count(0));
        }

        [Test]
        public void Event()
        {
			Event inter = new Event(controller);
            inter.Name = "FileAdded";
            inter.Modifiers.Add("public");
            inter.DataType = new DataType(controller, "EventHandler");

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }
        
        [Test]
        public void Field()
        {
			Field inter = new Field(controller);
            inter.Name = "i";
            inter.Modifiers.Add("public");
            inter.DataType = new DataType(controller, "int");
            inter.InitialValue = "1";

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void Function()
        {
			Function inter = new Function(controller);
            inter.Name = "File";
            inter.Modifiers.Add("public");
            inter.ReturnType = new DataType(controller,"int");
			Parameter param = new Parameter(controller);
            param.Name = "i";
            param.DataType = "int";
            inter.Parameters.Add(param);

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void Indexer()
        {
			Indexer inter = new Indexer(controller);
            inter.Name = "File";
            inter.DataType = new DataType(controller, "int");
			Parameter param = new Parameter(controller);
            param.Name = "i";
            param.DataType = "int";
            inter.Parameters.Add(param);

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void Interface()
        {
			Interface inter = new Interface(controller, "Interface1");
            inter.Modifiers.Add("public");

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void InterfaceAccessor()
        {
			InterfaceAccessor inter = new InterfaceAccessor(controller);
            inter.Name = "File";

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void InterfaceEvent()
        {
			InterfaceEvent inter = new InterfaceEvent(controller);
            inter.Name = "File";
            inter.DataType = new DataType(controller,"int");
            inter.DataType = new DataType(controller, "EventHandler");
            inter.HasNewKeyword = true;

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void InterfaceProperty()
        {
			InterfaceProperty inter = new InterfaceProperty(controller, "File");
            inter.DataType = new DataType(controller,"int");
            inter.HasNewKeyword = true;

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void Namespace()
        {
			Namespace inter = new Namespace(controller);
            inter.Name = "File";
			inter.AddChild(new Class(controller, "Class1"));

        	Namespace clone = (Namespace)inter.Clone();
        	clone.Controller = new CSharpController();

			Assert.That(inter.IsTheSame(clone, ComparisonDepth.Outer), Is.True);
			Assert.That(clone.WalkChildren(), Has.Count(0));
			Assert.That(inter.IsTheSame(clone, ComparisonDepth.Complete), Is.False);
        }

        [Test]
        public void Operator()
        {
			Operator inter = new Operator(controller);
            inter.Modifiers.Add("public");
            inter.Name = "File";
            inter.DataType = new DataType(controller, "int");
			Parameter param = new Parameter(controller);
            param.Name = "i";
            param.DataType = "int";
            inter.Parameters.Add(param);
            inter.BodyText = "asdfasdfasdfasdf";

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
            Assert.That(string.IsNullOrEmpty(((Operator)inter.Clone()).BodyText), Is.True);
        }

        [Test]
        public void Parameter()
        {
			Parameter inter = new Parameter(controller);
            inter.Name = "i";
            inter.DataType = "int";
            inter.IsParams = true;

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void Property()
        {
			Property inter = new Property(controller);
            inter.Name = "File";
            inter.Modifiers.Add("public");
            inter.DataType = new DataType(controller,"int");

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void PropertyAccessor_()
        {
			PropertyAccessor inter = new PropertyAccessor(controller);
            inter.Name = "File";
            inter.Modifier = "protected";
            inter.BodyText = "lkajsdflkjasdlkfjasldfj";
            inter.AccessorType = PropertyAccessor.AccessorTypes.Set;

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
            Assert.That(string.IsNullOrEmpty(((PropertyAccessor)inter.Clone()).BodyText), Is.True);
        }

        [Test]
        public void RegionStart()
        {
            RegionStart inter = new RegionStart(controller, "Start", -1);

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void RegionEnd()
        {
            RegionEnd inter = new RegionEnd(controller, -1);

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void Struct()
        {
			Struct inter = new Struct(controller);
            inter.Name = "Struct1";
            inter.BaseNames.Add("ValueType");
            inter.GenericTypes.Add("T");

            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }

        [Test]
        public void UsingStatement()
        {
			UsingStatement inter = new UsingStatement(controller, "IntelliMerge", "ArchAngel.Workbench.IntelliMerge");
            
            Assert.That(inter.IsTheSame(inter.Clone(), ComparisonDepth.Outer), Is.True);
        }
    }
}