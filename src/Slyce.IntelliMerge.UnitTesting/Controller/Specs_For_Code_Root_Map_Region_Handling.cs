using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_CSharp_Code_Root.Region_Handling
{
	[TestFixture]
	public class When_Regions_Are_Used
	{
		private CSharpController controller;

		[SetUp]
		public void Setup()
		{
			controller = new CSharpController();
		}

		[Test]
		[Ignore("This issue is on our list of things to do.")]
		public void They_Should_Not_Show_In_The_Object_Heirarchy()
		{
			Namespace ns = new Namespace(controller) { Index = 1};
			Region region = new Region(controller, "RegionStart") { Index = 2};
			Class clazz = new Class(controller, "Class1") { Index = 3};

			region.AddChild(clazz);
			ns.AddChild(region);

			Assert.That(clazz.Parent, Is.SameAs(ns), "The parent of the Class should be the Namespace, not the Region");
		}

		[Test]
		[Ignore("This issue is on our list of things to do.")]
		public void They_Should_Not_Show_In_The_Fully_Qualified_Display_Name()
		{
			Namespace ns = new Namespace(controller);
			Region region = new Region(controller, "RegionStart");
			Class clazz = new Class(controller, "Class1");

			region.AddChild(clazz);
			ns.AddChild(region);

			Assert.That(clazz.FullyQualifiedDisplayName.IndexOf("RegionStart") < 0, 
				"The fully qualified display name of the Class should not contain the region name");
		}
	}
}
