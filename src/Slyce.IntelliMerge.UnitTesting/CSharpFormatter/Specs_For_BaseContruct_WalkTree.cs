using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_BaseContruct_WalkTree
{
    [TestFixture]
    public class When_Walking_A_CodeRoot_Tree
    {
        private ICodeRoot codeRoot;
        [SetUp]
        public void SetUp()
        {
            var controller = new CSharpController();
            codeRoot = new CodeRoot(controller);
            var ns = new Namespace(controller, "Namespace1") { Index = 0 };
            codeRoot.AddChild(ns);
            ns.AddChild(new Class(controller, "Class1") { Index = 1 });
        }

        [Test]
        public void Each_Node_Should_Only_Appear_Once()
        {
            Assert.That(codeRoot.WalkTree().Count(), Is.EqualTo(2));
        }
    }
}
