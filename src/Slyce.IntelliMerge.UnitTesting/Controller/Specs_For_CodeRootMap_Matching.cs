using ArchAngel.Providers;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.IntelliMerge;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge.UnitTesting;

namespace Specs_For_CSharp_Code_Root.Matching
{
    [TestFixture]
    public class Utility_Methods
    {
        private Controller controller = null;

        [SetUp]
        public void Setup()
        {
            controller = new CSharpController();
			controller.Reorder = true;
        }

        [Test]
        public void Remove_Base_Construct()
        {
            Class cl2;
            CodeRootMap map = CreateBasicMap(out cl2);

            Assert.That(map.ChildNodes[0].ChildNodes, Has.Count(2));

            map.ChildNodes[0].ChildNodes[1].RemoveBaseConstruct(cl2, true);

            Assert.That(map.ChildNodes[0].ChildNodes, Has.Count(1));
        }

        [Test]
        public void Remove_Base_Construct_No_Tree_Cleanup()
        {
            Class cl2;
            CodeRootMap map = CreateBasicMap(out cl2);

            Assert.That(map.ChildNodes[0].ChildNodes, Has.Count(2));

            map.ChildNodes[0].ChildNodes[1].RemoveBaseConstruct(cl2, false);

            Assert.That(map.ChildNodes[0].ChildNodes, Has.Count(2));
            Assert.That(map.ChildNodes[0].ChildNodes[1].GetFirstValidBaseConstruct(), Is.Null);
        }

        private CodeRootMap CreateBasicMap(out Class cl2)
        {
            CodeRootMap map = new CodeRootMap();

            Class cl = new Class(controller, "Class1");
            Namespace ns = new Namespace(controller);
            ns.Name = "ArchAngel.Tests";
            ns.AddChild(cl);
            CodeRoot root = new CodeRoot(controller);
            root.AddChild(ns);
            map.AddCodeRoot(root, Version.PrevGen);

            cl = new Class(controller, "Class1");
            ns = new Namespace(controller);
            ns.Name = "ArchAngel.Tests";
            ns.AddChild(cl);
            cl2 = new Class(controller, "Class2");
            ns.AddChild(cl2);
            root = new CodeRoot(controller);
            root.AddChild(ns);
            map.AddCodeRoot(root, Version.NewGen);
            return map;
        }
    }

	[TestFixture]
	public class When_Adding_Similar_Item : Matching_Constructs_Base
	{
		[SetUp]
		public void Setup()
		{
			controller = new CSharpController();
			controller.Reorder = true;
		}

		[Test]
		public void Matching_Successful()
		{
			Class cl = new Class(controller, "Class1");
			cl.IsPartial = true;
			cl.GenericTypes.Add("T");
			Class c2 = new Class(controller, "Class2"); // Extra class in user
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
			map.AddCodeRoot(root, Version.User);

			cl = new Class(controller, "Class1");
			cl.IsPartial = true;
			cl.GenericTypes.Add("T");
			attrs = new AttributeSection(controller);
			attr = new Attribute(controller);
			attr.PositionalArguments.Add("true");
			attr.Name = "Serializable";
			attrs.AddAttribute(attr);
			cl.AddAttributeSection(attrs);

			// Create another class to match to the user one. 
			Class c3 = new Class(controller, "Class3");

			ns = new Namespace(controller);
			ns.Name = "ArchAngel.Tests";
			ns.AddChild(cl);
			ns.AddChild(c3);
			root = new CodeRoot(controller);
			root.AddChild(ns);

			map.AddCodeRoot(root, Version.PrevGen);
			map.AddCodeRoot(root, Version.NewGen);

			map.MatchConstructs(map.GetExactNode(ns), c2, c3, c3);

			string result = map.GetMergedCodeRoot().ToString();
			Assert.That(map.Diff(), Is.EqualTo(TypeOfDiff.UserChangeOnly));
			Assertions.StringContains(result, "class Class1");
			Assertions.StringContains(result, "[Serializable(true)]");
			Assertions.StringContains(result, "namespace ArchAngel.Tests");
			Assertions.StringContains(result, "Class2");
			Assertions.StringContains(result, "Class3", 0);

			int count = 0;
			foreach (CodeRootMapNode node in map.AllNodes)
			{
				if (node.IsTheSameReference(c2))
					count++;
			}
			Assert.That(count, Is.EqualTo(1));
		}
	}

	[TestFixture]
	public class When_Matching_Renamed_Namespaces : Matching_Constructs_Base
	{
		[SetUp]
		public void Setup()
		{
			controller = new CSharpController();
			controller.Reorder = true;
		}

		[Test]
		public void Matching_Successful()
		{
			CodeRootMap map = new CodeRootMap();

			Class cl = new Class(controller, "Class1");
			Namespace ns1 = new Namespace(controller);
			ns1.Name = "ArchAngel.Tests";
			ns1.AddChild(cl);
			CodeRoot root = new CodeRoot(controller);
			root.AddChild(ns1);
			map.AddCodeRoot(root, Version.User);

			cl = new Class(controller, "Class1");
			Namespace ns2 = new Namespace(controller);
			ns2.Name = "Slyce.Tests";
			ns2.AddChild(cl);
			root = new CodeRoot(controller);
			root.AddChild(ns2);
			map.AddCodeRoot(root, Version.PrevGen);

			cl = new Class(controller, "Class1");
			Namespace ns3 = new Namespace(controller);
			ns3.Name = "Slyce.Tests";
			ns3.AddChild(cl);
			root = new CodeRoot(controller);
			root.AddChild(ns3);
			map.AddCodeRoot(root, Version.NewGen);

			map.MatchConstructs(map, ns1, ns3, ns2);

			foreach (CodeRootMapNode child in map.AllNodes)
			{
				Assert.That(child.PrevGenObj, Is.Not.Null,
							string.Format("PrevGen in {0} is null", child.GetFirstValidBaseConstruct().ShortName));
				Assert.That(child.NewGenObj, Is.Not.Null,
							string.Format("NewGen in {0} is null", child.GetFirstValidBaseConstruct().ShortName));
				Assert.That(child.UserObj, Is.Not.Null,
							string.Format("User in {0} is null", child.GetFirstValidBaseConstruct().ShortName));
			}

			string result = map.GetMergedCodeRoot().ToString();
			Assert.That(map.Diff(), Is.EqualTo(TypeOfDiff.UserChangeOnly));
			Assertions.StringContains(result, "class Class1", 1);
			Assertions.StringContains(result, "namespace ArchAngel.Tests", 1);
			Assertions.StringContains(result, "namespace Slyce.Tests", 0);

			int count = 0;
			foreach (CodeRootMapNode node in map.AllNodes)
			{
				if (node.IsTheSameReference(ns1))
					count++;
			}
			Assert.That(count, Is.EqualTo(1));
		}
	}
	
	[TestFixture]
	public class When_Removing_A_Construct_From_A_Match : Matching_Constructs_Base
	{
		[SetUp]
		public void Setup()
		{
			controller = new CSharpController();
			controller.Reorder = true;
		}

		[Test]
		public void Matching_Successful()
		{
			CodeRootMap map = new CodeRootMap();

			CodeRoot userroot = CreateBasicRoot();
			map.AddCodeRoot(userroot, Version.User);

			CodeRoot prevroot = CreateBasicRoot();
			map.AddCodeRoot(prevroot, Version.PrevGen);

			CodeRoot templateroot = CreateBasicRoot();
			map.AddCodeRoot(templateroot, Version.NewGen);

			string result = map.GetMergedCodeRoot().ToString();
			Assertions.StringContains(result, "class Class1", 1);
			Assertions.StringContains(result, "namespace ArchAngel.Tests", 1);

			map.MatchConstructs(map.GetExactNode(userroot.Namespaces[0]), userroot.Namespaces[0].Classes[0], null, prevroot.Namespaces[0].Classes[0]);
			map.Diff();

			Assert.That(map.ChildNodes[0].ChildNodes, Has.Count(2));
			Assert.That(map.ChildNodes[0].ChildNodes[0].DetermineMissingConstructs(), Is.EqualTo(MissingObject.User | MissingObject.PrevGen));
			Assert.That(map.ChildNodes[0].ChildNodes[1].DetermineMissingConstructs(), Is.EqualTo(MissingObject.NewGen));

			ICodeRoot merged = map.GetMergedCodeRoot();
			result = merged.ToString();
			Assertions.StringContains(result, "class Class1", 2);
			Assertions.StringContains(result, "namespace ArchAngel.Tests", 1);
		}
	}

	[TestFixture]
	public class When_Matching_By_Correct_Unique_Identifiers : Matching_Constructs_Base
	{
		[SetUp]
		public void Setup()
		{
			controller = new CSharpController();
		}

		[Test]
		public void Matching_Successful()
		{
			controller.Reorder = true;
			Class cl = new Class(controller, "Class1");
			cl.IsPartial = true;
			cl.GenericTypes.Add("T");
			Class c2 = new Class(controller, "Class2"); // Extra class in user
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
			map.AddCodeRoot(root, Version.User);

			root = GetPrevGenOrNewGenRoot();
			map.AddCodeRoot(root, Version.PrevGen);

			root = GetPrevGenOrNewGenRoot();
			map.AddCodeRoot(root, Version.NewGen);

			bool matchResult = map.MatchConstructs("ArchAngel.Tests|Class2", "ArchAngel.Tests|Class3", "ArchAngel.Tests|Class3");

			Assert.That(matchResult, Is.True);

			string result = map.GetMergedCodeRoot().ToString();
			Assert.That(map.Diff(), Is.EqualTo(TypeOfDiff.UserChangeOnly));
			Assertions.StringContains(result, "class Class1");
			Assertions.StringContains(result, "[Serializable(true)]");
			Assertions.StringContains(result, "namespace ArchAngel.Tests");
			Assertions.StringContains(result, "Class2");
			Assertions.StringContains(result, "Class3", 0);
			int count = 0;
			foreach (CodeRootMapNode node in map.AllNodes)
			{
				if (node.IsTheSameReference(c2))
					count++;
			}
			Assert.That(count, Is.EqualTo(1));
		}

		private CodeRoot GetPrevGenOrNewGenRoot()
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

			// Create another class to match to the user one. 
			Class c3 = new Class(controller, "Class3");

			Namespace ns = new Namespace(controller);
			ns.Name = "ArchAngel.Tests";
			ns.AddChild(cl);
			ns.AddChild(c3);
			CodeRoot root = new CodeRoot(controller);
			root.AddChild(ns);
			return root;
		}
	}

    public class Matching_Constructs_Base
    {
        protected Controller controller = null;

		protected CodeRoot CreateBasicRoot()
        {
            Class cl = new Class(controller, "Class1");
            Namespace ns = new Namespace(controller);
            ns.Name = "ArchAngel.Tests";
            ns.AddChild(cl);
            
            CodeRoot root = new CodeRoot(controller);
            root.AddChild(ns);
            return root;
        }

    }
}