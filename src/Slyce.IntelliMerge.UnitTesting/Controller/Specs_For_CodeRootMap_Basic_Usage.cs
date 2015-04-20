using System.Collections.Generic;
using ArchAngel.Providers.CodeProvider;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.IntelliMerge.Controller;

namespace Specs_For_CodeRootMap.Basic_Usage
{
    [TestFixture]
    public class Constructing_A_new_CodeRootMap
    {
        [Test]
        public void Map_Is_Initially_Empty()
        {
            CodeRootMap map = new CodeRootMap();

            Assert.That(map.ChildNodes, Is.Not.Null);
            Assert.That(map.AllNodes, Is.Not.Null);
            Assert.That(map.ChildNodes, Is.Empty);
            Assert.That(map.AllNodes, Is.Empty);
            Assert.That(map.IsTreeRoot, Is.True);
            Assert.That(map.Omit, Is.False);
            Assert.That(map.ParentNode, Is.Null);
            Assert.That(map.ParentTree, Is.SameAs(map));
            
            Assert.That(map.PrevGenCodeRoot, Is.Null);
            Assert.That(map.UserCodeRoot, Is.Null);
            Assert.That(map.NewGenCodeRoot, Is.Null);
            Assert.That(map.PrevGenObj, Is.Null);
            Assert.That(map.UserObj, Is.Null);
            Assert.That(map.NewGenObj, Is.Null);
        }

        [Test]
        public void ClearMap()
        {
            MockRepository mocks = new MockRepository();
            CodeRootMap map = new CodeRootMap();
            IBaseConstruct bc;
            using(mocks.Record())
            {
                bc = mocks.DynamicMock<IBaseConstruct>();
                Expect.Call(bc.WalkChildren()).Return(new List<IBaseConstruct>().AsReadOnly());
            }

            map.AddBaseConstructAsNewChild(bc, Version.User);

            Assert.That(map.AllNodes, Has.Count(1));
            Assert.That(map.ChildNodes, Has.Count(1));

            map.Clear();

            Assert.That(map.AllNodes, Has.Count(0));
            Assert.That(map.ChildNodes, Has.Count(0));
        }
    }

    [TestFixture]
    public class Adding_A_Single_CodeRoot_To_A_New_CodeRootmap
    {
        private MockRepository mocks;
        [SetUp]
        public void Setup()
        {
            mocks = new MockRepository();
        }

        [Test]
        public void Map_Contains_CodeRoot()
        {
            CodeRootMap map = new CodeRootMap();
			ICodeRoot coderoot = mocks.StrictMock<ICodeRoot>();
            IBaseConstruct bc1 = mocks.DynamicMock<IBaseConstruct>();
            IBaseConstruct bc2 = mocks.DynamicMock<IBaseConstruct>();
            Utility.SetupBaseConstructs(mocks, bc1, bc2, coderoot);

            using(mocks.Playback())
            {
                map.AddCodeRoot(coderoot, Version.User);
            }

            Assert.That(map.ChildNodes, Has.Count(1));
            Assert.That(map.AllNodes, Has.Count(2));
            Assert.That(map.ChildNodes[0], Is.Not.Null);
            Assert.That(map.ChildNodes[0].IsTreeRoot, Is.False);
            Assert.That(map.ChildNodes[0].UserObj, Is.SameAs(bc1));
            Assert.That(map.ChildNodes[0].Omit, Is.False);
            Assert.That(map.ChildNodes[0].ParentNode, Is.SameAs(map));
            Assert.That(map.ChildNodes[0].ParentTree, Is.SameAs(map));

            CodeRootMapNode node = map.ChildNodes[0].ChildNodes[0];

            Assert.That(node, Is.Not.Null);
            Assert.That(node.IsTreeRoot, Is.False);
            Assert.That(node.UserObj, Is.SameAs(bc2));
            Assert.That(node.Omit, Is.False);
            Assert.That(node.ParentNode, Is.SameAs(map.ChildNodes[0]));
            Assert.That(node.ParentTree, Is.SameAs(map));
        }
    }

    [TestFixture]
    public class Adding_Two_CodeRoots_To_A_New_CodeRootmap
    {
        private MockRepository mocks;
        [SetUp]
        public void Setup()
        {
            mocks = new MockRepository();
        }

        [Test]
        public void Map_Contains_Both_CodeRoots()
        {
            CodeRootMap map = new CodeRootMap();
			ICodeRoot coderoot = mocks.StrictMock<ICodeRoot>();
            IBaseConstruct bc1 = mocks.DynamicMock<IBaseConstruct>();
            IBaseConstruct bc2 = mocks.DynamicMock<IBaseConstruct>();
            Utility.SetupBaseConstructs(mocks, bc1, bc2, coderoot);

            using (mocks.Playback())
            {
                map.AddCodeRoot(coderoot, Version.User);
                map.AddCodeRoot(coderoot, Version.NewGen);
            }

            Assert.That(map.ChildNodes, Has.Count(1));
            Assert.That(map.AllNodes, Has.Count(2));
            Assert.That(map.ChildNodes[0], Is.Not.Null);
            Assert.That(map.ChildNodes[0].IsTreeRoot, Is.False);
            Assert.That(map.ChildNodes[0].UserObj, Is.SameAs(bc1));
            Assert.That(map.ChildNodes[0].NewGenObj, Is.SameAs(bc1));
            Assert.That(map.ChildNodes[0].Omit, Is.False);
            Assert.That(map.ChildNodes[0].ParentNode, Is.SameAs(map));
            Assert.That(map.ChildNodes[0].ParentTree, Is.SameAs(map));

            CodeRootMapNode node = map.ChildNodes[0].ChildNodes[0];

            Assert.That(node, Is.Not.Null);
            Assert.That(node.IsTreeRoot, Is.False);
            Assert.That(node.UserObj, Is.SameAs(bc2));
            Assert.That(node.NewGenObj, Is.SameAs(bc2));
            Assert.That(node.Omit, Is.False);
            Assert.That(node.ParentNode, Is.SameAs(map.ChildNodes[0]));
            Assert.That(node.ParentTree, Is.SameAs(map));
        }
    }

    public static class Utility
    {
        public static void SetupBaseConstructs(MockRepository mocks, IBaseConstruct bc1, IBaseConstruct bc2, ICodeRoot coderoot)
        {
            using(mocks.Record())
            {
                Expect.Call(bc1.ShortName).Repeat.Any().Return("bc1");
                Expect.Call(bc2.ShortName).Repeat.Any().Return("bc2");
                
                Expect.Call(bc1.IsTheSame(bc1)).Repeat.Any().Return(true);
                Expect.Call(bc2.IsTheSame(bc2)).Repeat.Any().Return(true);
                
                Expect.Call(bc2.WalkChildren()).Repeat.AtLeastOnce().Return(new List<IBaseConstruct>().AsReadOnly());

                List<IBaseConstruct> bc1Children = new List<IBaseConstruct>();
                bc1Children.Add(bc2);
                Expect.Call(bc1.WalkChildren()).Repeat.AtLeastOnce().Return(bc1Children.AsReadOnly());

                List<IBaseConstruct> rootChildren = new List<IBaseConstruct>();
                rootChildren.Add(bc1);
                Expect.On(coderoot).Call(coderoot.WalkChildren()).Repeat.AtLeastOnce().Return(rootChildren.AsReadOnly());    
            }
        }
    }
}
