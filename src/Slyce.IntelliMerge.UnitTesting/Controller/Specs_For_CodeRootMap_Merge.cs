using System;
using System.Collections.Generic;
using ArchAngel.Providers;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.IntelliMerge;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge.UnitTesting;
using Attribute=ArchAngel.Providers.CodeProvider.DotNet.Attribute;
using Controller=ArchAngel.Providers.CodeProvider.DotNet.Controller;
using Delegate=ArchAngel.Providers.CodeProvider.DotNet.Delegate;
using Parameter=ArchAngel.Providers.CodeProvider.DotNet.Parameter;
using Version=Slyce.IntelliMerge.Controller.Version;
using Field = ArchAngel.Providers.CodeProvider.DotNet.Field;

namespace Specs_For_CodeRootMap.Merge
{
    public class GetMergedCodeRootBase
    {
        protected const string FunctionName = "GetVal";
        protected const string Modifier = "public";
        protected const string ReturnType = "int";
        protected const string BodyText = "{ return 5; }";
        protected const string DataType = "int";
        protected Controller controller;

        protected CodeRoot ConstructRootWithClass(IBaseConstruct childOfClass)
        {
            Class cl = new Class(controller, "Class1");
            cl.AddChild(childOfClass);
            AttributeSection attrs = new AttributeSection(controller);
            Attribute attr = new Attribute(controller);
            attr.PositionalArguments.Add("true");
            attr.Name = "Serializable";
            attrs.AddAttribute(attr);
			cl.AddAttributeSection(attrs);
            Namespace ns = new Namespace(controller);
            ns.Name = "ArchAngel.Tests";
            ns.AddChild(cl);
            CodeRoot root = new CodeRoot(controller);
            root.AddChild(ns);
            return root;
        }

        protected CodeRoot CreateFunctionAndClass(string functionParameterName)
        {
            Function inter;
            return CreateFunctionAndClass(functionParameterName, out inter);
        }

        protected CodeRoot CreateFunctionAndClass(string functionParameterName, out Function createdFunction)
        {
            createdFunction = CreateFunction(FunctionName, functionParameterName, 10);

        	CodeRoot userRoot = CreateClassAndNamespace(createdFunction);
            return userRoot;
        }

    	protected ArchAngel.Providers.CodeProvider.DotNet.Function CreateFunction(string functionName, string functionParameterName, int index)
    	{
    		ArchAngel.Providers.CodeProvider.DotNet.Function createdFunction = new ArchAngel.Providers.CodeProvider.DotNet.Function(controller);
    		createdFunction.Name = functionName;
			createdFunction.Index = index;
    		createdFunction.Modifiers.Add(Modifier);
            createdFunction.ReturnType = new DataType(controller, ReturnType);
    		createdFunction.BodyText = BodyText;
    		Parameter param = new Parameter(controller);
    		param.Name = functionParameterName;
    		param.DataType = DataType;
    		createdFunction.Parameters.Add(param);
    		return createdFunction;
    	}

    	protected CodeRoot CreateFieldAndClass(string fieldName)
        {
            Field inter;
            return CreateFieldAndClass(fieldName, out inter);
        }

        protected CodeRoot CreateFieldAndClass(string fieldName, out Field createdField)
        {
            createdField = new Field(controller);
            createdField.Name = fieldName;
            createdField.Modifiers.Add("public");
            createdField.DataType = new DataType(controller, "int");
            createdField.InitialValue = "5";

            CodeRoot userRoot = CreateClassAndNamespace(createdField);
            return userRoot;
        }

        protected CodeRoot CreateNamespaceAndInterface(IBaseConstruct inter)
        {
            Interface interface1 = new Interface(controller, "Interface1");
            interface1.Modifiers.Add("public");
            interface1.AddChild(inter);
            AttributeSection attrs = new AttributeSection(controller);
            Attribute attr = new Attribute(controller);
            attr.PositionalArguments.Add("true");
            attr.Name = "Serializable";
            attrs.AddAttribute(attr);
			interface1.AddAttributeSection(attrs);
            Namespace ns = new Namespace(controller);
            ns.Name = "ArchAngel.Tests";
            ns.AddChild(interface1);
            CodeRoot root = new CodeRoot(controller);
            root.AddChild(ns);
            return root;
        }

        protected CodeRoot CreateClassAndNamespace()
        {
            return CreateClassAndNamespace(new IBaseConstruct[0]);
        }

        protected CodeRoot CreateClassAndNamespace(IBaseConstruct child)
        {
            return CreateClassAndNamespace(new[]{child});
        }

        protected CodeRoot CreateClassAndNamespace(IEnumerable<IBaseConstruct> children)
        {
            Class cl = new Class(controller, "Class1");
            foreach(IBaseConstruct child in children)
                cl.AddChild(child);
            cl.Index = 10;
            AttributeSection attrs = new AttributeSection(controller);
            attrs.Index = 5;
            Attribute attr = new Attribute(controller);
            attr.Index = 6;
            attr.PositionalArguments.Add("true");
            attr.Name = "Serializable";
            attrs.AddAttribute(attr);
			cl.AddAttributeSection(attrs);
            Namespace ns = new Namespace(controller);
            ns.Index = 0;
            ns.Name = "ArchAngel.Tests";
            ns.AddChild(cl);
            CodeRoot userRoot = new CodeRoot(controller);
            userRoot.AddChild(ns);
            return userRoot;
        }

        protected CodeRoot CreateIndexer(string parameterName)
        {
            Indexer inter = new Indexer(controller);
            inter.Name = "File";
            inter.DataType = new DataType(controller, "int");
            Parameter param = new Parameter(controller);
            param.Name = parameterName;
            param.DataType = "int";
            inter.Parameters.Add(param);

            PropertyAccessor acc = new PropertyAccessor(controller);
            acc.Modifier = "public";
            acc.BodyText = "{ return file[i]; }";
            acc.AccessorType = PropertyAccessor.AccessorTypes.Get;
            inter.AddChild(acc);

            acc = new PropertyAccessor(controller);
            acc.Modifier = "protected";
            acc.BodyText = "{ file[i] = value; }";
            acc.AccessorType = PropertyAccessor.AccessorTypes.Set;
            inter.AddChild(acc);

            return CreateClassAndNamespace(inter);
        }

        protected CodeRoot CreateInterfaceAndMethod(string parameterName)
        {
            InterfaceMethod inter = new InterfaceMethod(controller, "Method1");
            inter.ReturnType = new DataType(controller, "int");
            Parameter param = new Parameter(controller);
            param.Name = parameterName;
            param.DataType = "int";
            inter.Parameters.Add(param);

            return CreateNamespaceAndInterface(inter);
        }

        protected CodeRoot CreateClassAndOperator(string secondParameterName)
        {
            Operator inter = new Operator(controller);
            inter.Modifiers.Add("public");
            inter.Modifiers.Add("static");
            inter.Name = "+";
            inter.DataType = new DataType(controller, "int");
            Parameter param = new Parameter(controller);
            param.Name = "self";
            param.DataType = "Class1";
            inter.Parameters.Add(param);
            param = new Parameter(controller);
            param.Name = secondParameterName;
            param.DataType = "int";
            inter.Parameters.Add(param);
            inter.BodyText = "{ return 5; }";

            return CreateClassAndNamespace(inter);
        }
    }

    [TestFixture]
    public class GetMergedCodeRoot : GetMergedCodeRootBase
    {
        [SetUp]
        public void Setup()
        {
            controller = new CSharpController();
			controller.Reorder = true;
        }

        [Test(Description="Getting the Merged CodeRoot of a map without any CodeRoots added should thrown an exception.")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoCodeRootsAdded()
        {
            CodeRootMap map = new CodeRootMap();
            map.GetMergedCodeRoot();
        }

        [Test]
        public void PrevGen_Is_Missing_Others_Are_Exact_Copy()
        {
            Class cl = new Class(controller, "Class1");
            cl.IsPartial = true;
            cl.GenericTypes.Add("T");
            AttributeSection attrs = new AttributeSection(controller);
            Attribute attr = new Attribute(controller);
            attr.PositionalArguments.Add("true");
            attr.Name = "Serializable";
            attrs.AddAttribute(attr);
			cl.AddAttributeSection(attrs);
            Namespace ns = new Namespace(controller);
            ns.Name = "ArchAngel.Tests";
            ns.AddChild(cl);
            CodeRoot root = new CodeRoot(controller);
            root.AddChild(ns);
            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
        }

        [Test]
        public void User_Is_Missing_Others_Are_Exact_Copy()
        {
            Function func = new Function(controller);
            func.Name = "Method1";
            func.Modifiers.Add("public");
            func.ReturnType = new DataType(controller, "void");
            func.BodyText = "{ }";

            Class cl = new Class(controller, "Class1");
            cl.AddChild(func);
            cl.IsPartial = true;
            cl.GenericTypes.Add("T");
            AttributeSection attrs = new AttributeSection(controller);
            Attribute attr = new Attribute(controller);
            attr.PositionalArguments.Add("true");
            attr.Name = "Serializable";
            attrs.AddAttribute(attr);
			cl.AddAttributeSection(attrs);
            Namespace ns = new Namespace(controller);
            ns.Name = "ArchAngel.Tests";
            ns.AddChild(cl);
            CodeRoot root = new CodeRoot(controller);
            root.AddChild(ns);
            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.PrevGen);
            map.AddCodeRoot(root, Version.NewGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "public void Method1()");
            Assertions.StringContains(result, "{ }");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
        }


        [Test]
        public void User_Added_Modifier()
        {
            CodeRootMap map = new CodeRootMap();
            CodeRoot root = CreateFunctionAndClass("i");
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            Function userFunction;
            CodeRoot userRoot = CreateFunctionAndClass("i1", out userFunction);
            userFunction.Modifiers.Add("static");
            map.AddCodeRoot(userRoot, Version.User);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.Not.EqualTo(root.ToString()));
            Assert.That(result, Is.EqualTo(userRoot.ToString()));
            Assertions.StringContains(result, "class Class1", 1);
            Assertions.StringContains(result, "namespace ArchAngel.Tests", 1);
            Assertions.StringContains(result, "public static int GetVal(int i1)", 1);
            Assertions.StringContains(result, "{ return 5; }", 1);
        }

        [Test]
        public void PrevGen_Is_Missing_Others_Are_Different_Basic()
        {
            CodeRootMap map = new CodeRootMap();

            Class cl = new Class(controller, "Class1");
            cl.IsPartial = true;
            cl.GenericTypes.Add("T");
            AttributeSection attrs = new AttributeSection(controller);
            Attribute attr = new Attribute(controller);
            attr.PositionalArguments.Add("true");
            attr.Name = "Serializable";
            attrs.AddAttribute(attr);
			cl.AddAttributeSection(attrs);
            Namespace ns = new Namespace(controller);
            ns.Name = "ArchAngel.Tests";
            ns.AddChild(cl);
            CodeRoot root = new CodeRoot(controller);
            root.AddChild(ns);

            map.AddCodeRoot(root, Version.User);

            cl = new Class(controller, "Class2");
            cl.IsPartial = true;
            cl.GenericTypes.Add("T");
            attrs = new AttributeSection(controller);
            attr = new Attribute(controller);
            attr.PositionalArguments.Add("true");
            attr.Name = "SomeOtherAttribute";
            attrs.AddAttribute(attr);
			cl.AddAttributeSection(attrs);
            ns = new Namespace(controller);
            ns.Name = "ArchAngel.OtherTests";
            ns.AddChild(cl);
            root = new CodeRoot(controller);
            root.AddChild(ns);

            map.AddCodeRoot(root, Version.NewGen);

            string result = map.GetMergedCodeRoot().ToString();
            //Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "namespace ArchAngel.OtherTests");
            Assertions.StringContains(result, "class Class2");
            Assertions.StringContains(result, "[SomeOtherAttribute(true)]");
        }

        [Test]
        public void PrevGen_Is_Missing_Others_Are_Different_BodyText()
        {
            CodeRootMap map = new CodeRootMap();

            Function inter = new Function(controller);
            inter.Name = "GetVal";
            inter.Modifiers.Add("public");
            inter.ReturnType = new DataType(controller,"int");
            inter.BodyText = "{ return 5; }";
            Parameter param = new Parameter(controller);
            param.Name = "i";
            param.DataType = "int";
            inter.Parameters.Add(param);

            CodeRoot root = ConstructRootWithClass(inter);

            map.AddCodeRoot(root, Version.User);

            inter = new Function(controller);
            inter.Name = "GetVal";
            inter.Modifiers.Add("public");
            inter.ReturnType = new DataType(controller,"int");
            inter.BodyText = "{ return 6; }"; // Modified Body Text
            param = new Parameter(controller);
            param.Name = "i";
            param.DataType = "int";
            inter.Parameters.Add(param);

            root = ConstructRootWithClass(inter);

            map.AddCodeRoot(root, Version.NewGen);

            Assert.That(map.Diff(), Is.EqualTo(TypeOfDiff.Conflict));
        }

        [Test]
        public void PrevGen_Is_Skipped_Where_No_Template_Or_User_Exist()
        {
            Class cl = new Class(controller, "Class1");
            cl.IsPartial = true;
            cl.GenericTypes.Add("T");
            Class c2 = new Class(controller, "Class2"); // Extra class in prevgen
            AttributeSection attrs = new AttributeSection(controller);
            Attribute attr = new Attribute(controller);
            attr.PositionalArguments.Add("true");
            attr.Name = "Serializable";
            attrs.AddAttribute(attr);
			cl.AddAttributeSection(attrs);
            Namespace ns = new Namespace(controller);
            ns.Name = "ArchAngel.Tests";
            ns.AddChild(cl);
            ns.AddChild(c2);
            CodeRoot root = new CodeRoot(controller);
            root.AddChild(ns);

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.PrevGen);
            
            cl = new Class(controller, "Class1");
            cl.IsPartial = true;
            cl.GenericTypes.Add("T");
            attrs = new AttributeSection(controller);
            attr = new Attribute(controller);
            attr.PositionalArguments.Add("true");
            attr.Name = "Serializable";
            attrs.AddAttribute(attr);
			cl.AddAttributeSection(attrs);
            ns = new Namespace(controller);
            ns.Name = "ArchAngel.Tests";
            ns.AddChild(cl);
            root = new CodeRoot(controller);
            root.AddChild(ns);

            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            // make sure that the deleted class Class2 was not included int he merged code root.
            Assertions.StringContains(result, "Class2", 0);
        }

        [Test]
        public void Attributes()
        {
            AttributeSection attrs = new AttributeSection(controller);
            Attribute attr = new Attribute(controller);
            attr.PositionalArguments.Add("true");
            attr.Name = "Serializable";
            attrs.AddAttribute(attr);
            CodeRoot root = new CodeRoot(controller);
            root.AddChild(attrs);
            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "[Serializable(true)]");
        }


        [Test]
        public void Class()
        {
            Class cl = new Class(controller, "Class1");
            cl.IsPartial = true;
            cl.GenericTypes.Add("T");
            AttributeSection attrs = new AttributeSection(controller);
            Attribute attr = new Attribute(controller);
            attr.PositionalArguments.Add("true");
            attr.Name = "Serializable";
            attrs.AddAttribute(attr);
            cl.AddAttributeSection(attrs);
            Namespace ns = new Namespace(controller);
            ns.Name = "ArchAngel.Tests";
            ns.AddChild(cl);
            CodeRoot root = new CodeRoot(controller);
            root.AddChild(ns);
            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
        }

        [Test]
        public void Constant()
        {
            Constant con = new Constant(controller);
            con.DataType = new DataType(controller,"int");
            con.Modifiers.Add("public");
            con.Name = "CONSTANT1";
            con.Expression = "5";
            
            CodeRoot root = CreateClassAndNamespace(con);

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "public const int CONSTANT1 = 5;");
        }

        [Test]
        public void Constructor()
        {
            Constructor constructor = new Constructor(controller);
            constructor.Modifiers.Add("public");
            constructor.Name = "Class1";
            constructor.BodyText = "{ }";
            Parameter param = new Parameter(controller);
            param.Name = "i";
            param.DataType = "int";
            constructor.Parameters.Add(param);

            CodeRoot root = CreateClassAndNamespace(constructor);

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "public Class1");
            Assertions.StringContains(result, "{ }");
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

            CodeRoot root = CreateClassAndNamespace(inter);

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "public delegate int File");
        }

        [Test]
        public void EmptyPlaceholder()
        {
            EmptyPlaceholder inter = new EmptyPlaceholder(controller, "asdfasd", 2);

            CodeRoot root = CreateClassAndNamespace(inter);

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "asdfasd", 0);
        }

        [Test]
        public void Enumeration()
        {
            Enumeration inter = new Enumeration(controller);
            inter.Name = "File";
            inter.Modifiers.Add("public");
            inter.Members.Add(new Enumeration.EnumMember(controller, "Read"));
            inter.Members.Add(new Enumeration.EnumMember(controller, "Write"));

            CodeRoot root = CreateClassAndNamespace(inter);

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assert.That(result.Contains("Read"), Is.True, "Contains Read enum value");
            Assert.That(result.Contains("Write"), Is.True, "Contains Write enum value");
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "Read");
            Assertions.StringContains(result, "Write");
            Assertions.StringContains(result, "public enum File");
        }

        [Test]
        public void Event()
        {
            Event inter = new Event(controller);
            inter.Name = "FileAdded";
            inter.Modifiers.Add("public");
            inter.DataType = new DataType(controller,"EventHandler");
            CodeRoot root = CreateClassAndNamespace(inter);

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "public event EventHandler FileAdded");
        }

        [Test]
        public void Field()
        {
            Field inter = new Field(controller);
            inter.Name = "i";
            inter.Modifiers.Add("public");
            inter.DataType = new DataType(controller,"int");
            inter.InitialValue = "1";
            CodeRoot root = CreateClassAndNamespace(inter);

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "public int i = 1");
        }

        [Test]
        public void Function()
        {
            CodeRootMap map = new CodeRootMap();
            CodeRoot root = CreateFunctionAndClass("i");
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "public int GetVal(int i)");
            Assertions.StringContains(result, "{ return 5; }");
        }

        [Test]
        public void Function_Renamed_Params()
        {
            CodeRootMap map = new CodeRootMap();
            CodeRoot root = CreateFunctionAndClass("i");
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);
            CodeRoot userRoot = CreateFunctionAndClass("i1");
            map.AddCodeRoot(userRoot, Version.User);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.Not.EqualTo(root.ToString()));
            Assert.That(result, Is.EqualTo(userRoot.ToString()));
            Assertions.StringContains(result, "class Class1", 1);
            Assertions.StringContains(result, "namespace ArchAngel.Tests", 1);
            Assertions.StringContains(result, "public int GetVal(int i1)", 1);
            Assertions.StringContains(result, "{ return 5; }", 1);
        }

        [Test]
        public void Indexer()
        {
            CodeRoot root = CreateIndexer("i");

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "int this [int i]");
            Assertions.StringContains(result, "get{ return file[i]; }");
            Assertions.StringContains(result, "set{ file[i] = value; }");
        }

        [Test]
        public void Indexer_Renamed_Params()
        {
            CodeRoot root = CreateIndexer("i");

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            CodeRoot userRoot = CreateIndexer("i1");
            map.AddCodeRoot(userRoot, Version.User);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.Not.EqualTo(root.ToString()));
            Assert.That(result, Is.EqualTo(userRoot.ToString()));
            
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "int this [int i1]");
            Assertions.StringContains(result, "get{ return file[i]; }");
            Assertions.StringContains(result, "set{ file[i] = value; }");
        }

        [Test]
        public void Interface()
        {
            Interface inter = new Interface(controller, "Interface1");
            inter.Modifiers.Add("public");

            Class cl = new Class(controller, "Class1");
            cl.AddChild(inter);
            AttributeSection attrs = new AttributeSection(controller);
            Attribute attr = new Attribute(controller);
            attr.PositionalArguments.Add("true");
            attr.Name = "Serializable";
            attrs.AddAttribute(attr);
			cl.AddAttributeSection(attrs);
            Namespace ns = new Namespace(controller);
            ns.Name = "ArchAngel.Tests";
            ns.AddChild(cl);
            CodeRoot root = new CodeRoot(controller);
            root.AddChild(ns);

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "public interface Interface1");
        }

        [Test]
        public void InterfaceProperty()
        {
            InterfaceProperty inter = new InterfaceProperty(controller,"InterfaceProperty1");
            inter.Name = "File";
            inter.DataType = new DataType(controller,"int");

            InterfaceAccessor acc = new InterfaceAccessor(controller);
            acc.AccessorType = InterfaceAccessor.AccessorTypes.Get;
            inter.AddChild(acc);

            acc = new InterfaceAccessor(controller);
            acc.AccessorType = InterfaceAccessor.AccessorTypes.Set;
            inter.AddChild(acc);

            CodeRoot root = CreateNamespaceAndInterface(inter);

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "public interface Interface1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "int File");
            Assertions.StringContains(result, "get;");
            Assertions.StringContains(result, "set;");
        }

        [Test]
        public void InterfaceMethod()
        {
            CodeRoot root = CreateInterfaceAndMethod("i");

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "public interface Interface1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "int Method1 (int i);");
        }

        [Test]
        public void InterfaceMethod_Renamed_Params()
        {
            CodeRoot root = CreateInterfaceAndMethod("i");

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);
            CodeRoot userRoot = CreateInterfaceAndMethod("i1");
            map.AddCodeRoot(userRoot, Version.User);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(userRoot.ToString()));
            Assert.That(result, Is.Not.EqualTo(root.ToString()));
            Assertions.StringContains(result, "public interface Interface1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "int Method1 (int i1);");
        }

        [Test]
        public void InterfaceEvent()
        {
            InterfaceEvent inter = new InterfaceEvent(controller);
            inter.Name = "File";
            inter.DataType = new DataType(controller, "EventHandler");
            inter.HasNewKeyword = true;

            CodeRoot root = CreateNamespaceAndInterface(inter);

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "public interface Interface1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "new event EventHandler File");
        }

        [Test]
        public void Namespace()
        {
            Namespace inter = new Namespace(controller);
            inter.Name = "ArchAngel.Tests";
            inter.AddChild(new UsingStatement(controller, "", "System"));
            CodeRoot root = new CodeRoot(controller);
            root.AddChild(inter);

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "using System");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
        }

        [Test]
        public void Operator()
        {
            CodeRoot root = CreateClassAndOperator("i");

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "public static int operator +(Class1 self, int i)");
            Assertions.StringContains(result, "{ return 5; }");
        }

        [Test]
        public void Operator_Renamed_Params()
        {
            CodeRoot root = CreateClassAndOperator("i");

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            CodeRoot userRoot = CreateClassAndOperator("i1");
            map.AddCodeRoot(userRoot, Version.User);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.Not.EqualTo(root.ToString()));
            Assert.That(result, Is.EqualTo(userRoot.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "public static int operator +(Class1 self, int i1)");
            Assertions.StringContains(result, "{ return 5; }");
        }

        [Test]
        public void Property()
        {
            Property inter = new Property(controller);
            inter.Name = "File";
            inter.Modifiers.Add("public");
            inter.DataType = new DataType(controller, "string");

            PropertyAccessor acc = new PropertyAccessor(controller);
            acc.Modifier = "public";
            acc.BodyText = "{ return file; }";
            acc.AccessorType = PropertyAccessor.AccessorTypes.Get;
            inter.AddChild(acc);

            acc = new PropertyAccessor(controller);
            acc.Modifier = "protected";
            acc.BodyText = "{ file = value; }";
            acc.AccessorType = PropertyAccessor.AccessorTypes.Set;
            inter.AddChild(acc);

            CodeRoot root = CreateClassAndNamespace(inter);

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "public string File");
            Assertions.StringContains(result, "get{ return file; }");
            Assertions.StringContains(result, "set{ file = value; }");
        }

        [Test]
        public void Regions()
        {
			controller.Reorder = false;
			Region region = new Region(controller, "Start", 11);
            
            CodeRoot root = CreateClassAndNamespace(new IBaseConstruct[]{region});

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "#region Start");
            Assertions.StringContains(result, "#endregion");
            Assert.That(result.IndexOf("#region Start"), Is.LessThan(result.IndexOf("#endregion")));
        }

        [Test]
        public void Struct()
        {
            Struct inter = new Struct(null);
            inter.Name = "Struct1";
            inter.BaseNames.Add("ValueType");
            inter.GenericTypes.Add("T");
            CodeRoot root = CreateClassAndNamespace(inter);

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "[Serializable(true)]");
            Assertions.StringContains(result, "namespace ArchAngel.Tests");
            Assertions.StringContains(result, "struct Struct1<T> : ValueType");
        }

        [Test]
        public void UsingStatements()
        {
            CodeRoot root = CreateClassAndNamespace(new IBaseConstruct[0]);
            UsingStatement st = new UsingStatement(controller, "", "System");
            st.Index = 0;
            root.AddChild(st);

            CodeRootMap map = new CodeRootMap();
            map.AddCodeRoot(root, Version.User);
            map.AddCodeRoot(root, Version.NewGen);
            map.AddCodeRoot(root, Version.PrevGen);

            string result = map.GetMergedCodeRoot().ToString();
            Assert.That(result, Is.EqualTo(root.ToString()));
            Assertions.StringContains(result, "class Class1");
            Assertions.StringContains(result, "using System;");
        }
    }
}