using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using NUnit.Framework;
using Slyce.IntelliMerge.Controller;
using Is=NUnit.Framework.SyntaxHelpers.Is;
using Version=Slyce.IntelliMerge.Controller.Version;

namespace Specs_For_CodeRootMap.GetSiblingsOfType
{
	[TestFixture]
	public class Given_A_Function : SiblingTestBase
	{
		[SetUp]
		public void Setup()
		{
			SetupFunctions();
		}
	
		[Test]
		public void Returns_Three_Siblings()
		{
			CodeRootMap root = new CodeRootMap();
			root.AddCodeRoot(controller.Root, Version.NewGen);

			ReadOnlyCollection<IBaseConstruct> children =
				root.ChildNodes[0].ChildNodes[0].GetSiblingsOfSameType(Version.NewGen);

			Assert.That(children.Count, Is.EqualTo(3));
			Assert.That(children.Contains(func2), "Contains second function");
			Assert.That(children.Contains(func3), "Contains third function");
			Assert.That(children.Contains(func4), "Contains fourth function");
		}
	}

	[TestFixture]
	public class Given_A_Class : SiblingTestBase
	{
		[SetUp]
		public void Setup()
		{
			SetupFunctions();
		}

		[Test]
		public void Returns_An_Empty_Collection()
		{
			CodeRootMap root = new CodeRootMap();
			root.AddCodeRoot(controller.Root, Version.NewGen);

			ReadOnlyCollection<IBaseConstruct> children =
				root.ChildNodes[0].GetSiblingsOfSameType(Version.NewGen);

			Assert.That(children.Count, Is.EqualTo(0));
		}
	}

	[TestFixture]
	public class Given_A_Verson_That_Does_Not_Exist : SiblingTestBase
	{
		[SetUp]
		public void Setup()
		{
			SetupFunctions();
		}

		[Test]
		public void Returns_An_Empty_Collection()
		{
			CodeRootMap root = new CodeRootMap();
			root.AddCodeRoot(controller.Root, Version.NewGen);

			ReadOnlyCollection<IBaseConstruct> children =
				root.ChildNodes[0].GetSiblingsOfSameType(Version.User);

			Assert.That(children.Count, Is.EqualTo(0));
		}
	}

	public class SiblingTestBase : Merge.GetMergedCodeRootBase
	{
		protected Function func1;
		protected Function func2;
		protected Function func3;
		protected Function func4;


		protected void SetupFunctions()
		{
			controller = new CSharpController();

			func1 = CreateFunction("Function1", "i", 21);
			func2 = CreateFunction("Function2", "j", 22);
			func3 = CreateFunction("Function3", "k", 23);
			func4 = CreateFunction("Function4", "l", 24);

			Class cl = new Class(controller, "Class1");
			cl.AddChild(func1);
			cl.AddChild(func2);
			cl.AddChild(func3);
			cl.AddChild(func4);

			controller.Root.AddChild(cl);
		}
	}
}