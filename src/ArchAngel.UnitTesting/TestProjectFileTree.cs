using System.IO;
using ArchAngel.Common;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;
using Slyce.IntelliMerge.Controller;

namespace ArchAngel.Workbench.Tests
{
    [TestFixture]
    public class TestProjectFileTree
    {
        [Test]
        public void TestConstuction()
        {
            ProjectFileTree tree = new ProjectFileTree();
            Assert.That(tree.IsTreeRoot, Is.True, "A ProjectFileTree must always be a tree root.");
            Assert.That(tree.ParentNode, Is.Null, "A ProjectFileTree must not have a parent node");
            Assert.That(tree.ParentTree, Is.Null, "A ProjectFileTree must not have a parent tree");
            Assert.That(tree.AssociatedFile, Is.Null, "A ProjectFileTree must not have an associated file");
            Assert.That(tree.Status, Is.EqualTo(ProjectFileStatusEnum.Folder), "A ProjectFileTree status must be Folder.");
        }

        [Test]
        public void TestAddChildNodeWithNullFile()
        {
            ProjectFileTree tree = new ProjectFileTree();
            ProjectFileTreeNode child = tree.AddChildNode();
            Assert.That(tree.ChildNodes.Count, Is.EqualTo(1), "The child should have been added to the tree's ChildNodes");
            Assert.That(tree.AllNodes.Count, Is.EqualTo(1), "The child should have been added to the tree's Nodes");
            Assert.That(tree.ChildNodes[0], Is.SameAs(child), "The first ChildNode must be the new child node.");
            Assert.That(tree.AllNodes[0], Is.SameAs(child), "The first node in the AllNodes collection must be the new child.");

            Assert.That(tree.ChildNodes[0].AssociatedFile, Is.Null, "The child must be constructed with a null AssociatedFile.");
            Assert.That(tree.ChildNodes[0].ParentNode, Is.SameAs(tree), "The ParentNode must be the tree.");
            Assert.That(tree.ChildNodes[0].ParentTree, Is.SameAs(tree), "The ParentTree must be the tree.");
            Assert.That(tree.ChildNodes[0].Status, Is.EqualTo(ProjectFileStatusEnum.UnAnalysedFile), "A ProjectFileTreeNode's status must be UnanalysedFile by default.");
        }

        [Test]
        public void TestAddChildNodeWithAssociatedFile()
        {
            // We don't need this to do anything except be not null.
            IFileInformation fileInformation = MockRepository.GenerateStub<IFileInformation>();

            ProjectFileTree tree = new ProjectFileTree();
            ProjectFileTreeNode child = tree.AddChildNode(fileInformation);

            Assert.That(tree.ChildNodes[0].AssociatedFile, Is.SameAs(fileInformation),
                        "The child must be constructed with the supplied AssociatedFile.");
            Assert.That(tree.ChildNodes[0], Is.SameAs(child), "The first ChildNode must be the new child node.");
            Assert.That(tree.AllNodes[0], Is.SameAs(child),
                        "The first node in the AllNodes collection must be the new child.");
            
        }
    }

    [TestFixture]
    public class Test_LoadFiles
    {
        private MockRepository mocks;
        private IController controller;
        private const string RelativePath = "Class.cs";
        private const string TempUserPath = "TempUserPath";
        private const string TempPrevGenPath = "TempPrevGenPath";

        [SetUp]
        public void Setup()
        {
            mocks = new MockRepository();
            using (mocks.Record())
            {
                SetupController();
            }
            Directory.CreateDirectory(TempUserPath);
            Directory.CreateDirectory(TempPrevGenPath);

            //File.CreateText(Path.Combine(tempUserPath, relativePath));
            //File.CreateText(Path.Combine(tempPrevGenPath, relativePath));
        }

        /*
        [TearDown]
        public void Teardown()
        {
            File.Delete(Path.Combine(tempUserPath, relativePath));
            File.Delete(Path.Combine(tempPrevGenPath, relativePath));
            Directory.Delete(tempUserPath, true);
            Directory.Delete(tempPrevGenPath, true);
        }
        */
        [Test]
        public void Test_LoadPrevGenFile()
        {
            TextFileInformation tfi = new TextFileInformation();
            tfi.RelativeFilePath = RelativePath;

            ProjectFileTree tree = new ProjectFileTree();
            tree.AddChildNode(tfi, RelativePath);
            tree.LoadPrevGenFiles(controller);

			// loading a non existant file will result in tfi.PrevGenFile being set to TextFile.Blank
			Assert.That(tfi.PrevGenFile.FilePath, Is.Null);
			Assert.That(tfi.PrevGenFile.HasContents, Is.False);
			Assert.That(tfi.PrevGenFile.IsFileOnDisk, Is.False);
        }

        [Test]
        public void Test_LoadUserFile()
        {
            TextFileInformation tfi = new TextFileInformation();
            tfi.RelativeFilePath = RelativePath;
            
            ProjectFileTree tree = new ProjectFileTree();
            tree.AddChildNode(tfi, RelativePath);
            tree.LoadUserFiles(controller);

			// loading a non existant file will result in tfi.UserFile being set to TextFile.Blank
			Assert.That(tfi.UserFile.FilePath, Is.Null);
			Assert.That(tfi.UserFile.HasContents, Is.False);
			Assert.That(tfi.UserFile.IsFileOnDisk, Is.False);
        }

        private void SetupController()
        {
            controller = mocks.StrictMock<IController>();
            ProjectSettings settings = new ProjectSettings();
            settings.OutputPath = TempUserPath;
			WorkbenchProject project = new WorkbenchProject();
			project.ProjectSettings = settings;
            Expect.Call(controller.GetTempFilePathForComponent(ComponentKey.Workbench_FileGenerator)).IgnoreArguments().Return(@"C:\Temp").Repeat.Any();
			Expect.Call(controller.CurrentProject).Return(project).Repeat.Any();
        }
    }
}
