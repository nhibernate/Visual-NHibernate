using System;
using System.IO;
using System.Reflection;
using ArchAngel.Common;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Workbench.Properties;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;
using Slyce.IntelliMerge.Controller;

namespace ArchAngel.Workbench.Tests
{
    [TestFixture]
    public class TestGenerationHelper
    {
        private MockRepository mocks;

        private ITaskProgressHelper<GenerateFilesProgress> progressHelper;
        private IWorkbenchProject projectInfo;
        private IFolder folder;
        private IScriptBaseObject scriptObject;
        private ITemplateLoader loader;
        private IController controller;
    	private IFileController fileController;

        [SetUp]
        public void Setup()
        {
            mocks = new MockRepository();

            progressHelper = mocks.DynamicMock<ITaskProgressHelper<GenerateFilesProgress>>();
			projectInfo = mocks.DynamicMock<IWorkbenchProject>();
            folder = mocks.DynamicMock<IFolder>();
            scriptObject = mocks.DynamicMock<IScriptBaseObject>();
			loader = mocks.DynamicMock<ITemplateLoader>();
            controller = mocks.DynamicMock<IController>();
			fileController = mocks.DynamicMock<IFileController>();
        }
        
        [Test]
        public void TestGenerateAllFiles_No_Files()
        {
            using(mocks.Record())
            {
                SetupFolder(folder, new IFolder[0],  new IScript[0], new IFile[0], "");
				SetupProject();
            }

			GenerationHelper helper = new GenerationHelper(progressHelper, loader, projectInfo, new FileController());
            ProjectFileTree tree = new ProjectFileTree();
			helper.GenerateAllFiles("fasdkj", folder, tree, scriptObject, "C:\\Temp");

            Assert.That(tree.AllNodes, Is.Empty);
			
        }

        [Test]
        public void TestGenerateAllFiles_Static_File()
        {
            IFile file = mocks.DynamicMock<IFile>();
            ProviderInfo providerMock = mocks.DynamicMock<ProviderInfo>();

            using (mocks.Record())
            {
                SetupFolder(folder, new IFolder[0],  new IScript[0], new[] { file }, "");
                SetupProject();
				SetupLoader();
                SetupStaticFile(file);
                SetupProvider(providerMock);
                SetupController();
				Expect.Call(() => projectInfo.AddGeneratedFile(new GeneratedFile(
					"file.jpg", "C:\\Temp\\file.jpg", "file.jpg", "", "Iterator")));
            }

        	GenerationHelper helper = new GenerationHelper(progressHelper, loader, projectInfo, fileController);
            ProjectFileTree tree = new ProjectFileTree();
			helper.GenerateAllFiles("", folder, tree, scriptObject, "C:\\Temp");

            Assert.That(tree.AllNodes.Count, Is.EqualTo(1));
            Assert.That(tree.AllNodes[0].Status, Is.EqualTo(ProjectFileStatusEnum.UnAnalysedFile));
            Assert.That(tree.AllNodes[0].Text, Is.EqualTo("file.jpg"));
            Assert.That(tree.ChildNodes[0].AssociatedFile.RelativeFilePath, Is.EqualTo("file.jpg"));
            BinaryFileInformation bfi = (BinaryFileInformation)tree.ChildNodes[0].AssociatedFile;
            Assert.That(bfi.NewGenFile.FilePath,
                Is.EqualTo(Path.Combine(controller.GetTempFilePathForComponent(ComponentKey.Workbench_FileGenerator), tree.ChildNodes[0].AssociatedFile.RelativeFilePath)),
                "New Gen file was not assigned to");
			mocks.VerifyAll();
        }

        [Test]
        public void TestGenerateAllFiles_Folder_Sub_File()
        {
            IFile staticFile = mocks.DynamicMock<IFile>();
            IScript scriptFile = mocks.DynamicMock<IScript>();
            IFolder subFolder = mocks.DynamicMock<IFolder>();
            ProviderInfo providerMock = mocks.DynamicMock<ProviderInfo>();

            using (mocks.Record())
            {
                SetupFolder(subFolder, new IFolder[0], new[] { scriptFile }, new[] { staticFile }, "folder");
                SetupFolder(folder, new[]{subFolder}, new IScript[0], new IFile[0], "");
                SetupProject();
                SetupStaticFile(staticFile);
                SetupScriptFile(scriptFile);
                SetupProvider(providerMock);
                SetupController();
                SetupLoader(scriptFile);
				Expect.Call(() => projectInfo.AddGeneratedFile(new GeneratedFile(
					"Class.cs", "C:\\Temp\\folder\\Class.cs", "folder\\Class.cs", "Class", "Iterator")));
				Expect.Call(() => projectInfo.AddGeneratedFile(new GeneratedFile(
					"file.jpg", "C:\\Temp\\folder\\file.jpg", "folder\\file.jpg", "", "Iterator")));
            }

			GenerationHelper helper = new GenerationHelper(progressHelper, loader, projectInfo, fileController);
            ProjectFileTree tree = new ProjectFileTree();
            helper.GenerateAllFiles("", folder, tree, scriptObject, "C:\\Temp");

            Assert.That(tree.AllNodes.Count, Is.EqualTo(3));
            ProjectFileTreeNode subfolder = tree.ChildNodes[0];
            
            Assert.That(subfolder.Status, Is.EqualTo(ProjectFileStatusEnum.Folder));
            Assert.That(subfolder.Text, Is.EqualTo("folder"));
            Assert.That(subfolder.ChildNodes.Count, Is.EqualTo(2));

            ProjectFileTreeNode childNode = subfolder.ChildNodes[0];
            Assert.That(childNode.Status, Is.EqualTo(ProjectFileStatusEnum.UnAnalysedFile));
            Assert.That(childNode.Text, Is.EqualTo("Class.cs"));
            Assert.That(childNode.ChildNodes, Is.Empty);
            Assert.That(childNode.AssociatedFile.RelativeFilePath, Is.EqualTo(Path.Combine("folder", "Class.cs")));
            TextFileInformation tfi = (TextFileInformation)childNode.AssociatedFile;
            Assert.That(tfi.NewGenFile.FilePath,
                Is.EqualTo(Path.Combine(controller.GetTempFilePathForComponent(ComponentKey.Workbench_FileGenerator), childNode.AssociatedFile.RelativeFilePath)),
                "New Gen file was not assigned to");

            childNode = subfolder.ChildNodes[1];
            Assert.That(childNode.Status, Is.EqualTo(ProjectFileStatusEnum.UnAnalysedFile));
            Assert.That(childNode.Text, Is.EqualTo("file.jpg"));
            Assert.That(childNode.ChildNodes, Is.Empty);
            Assert.That(childNode.AssociatedFile.RelativeFilePath, Is.EqualTo(Path.Combine("folder", "file.jpg")));
            BinaryFileInformation bfi = (BinaryFileInformation)childNode.AssociatedFile;
            Assert.That(bfi.NewGenFile.FilePath,
                Is.EqualTo(Path.Combine(controller.GetTempFilePathForComponent(ComponentKey.Workbench_FileGenerator), childNode.AssociatedFile.RelativeFilePath)),
                "New Gen file was not assigned to");

			mocks.VerifyAll();
        }

        [Test]
        public void TestGenerateAllFiles_Script_File()
        {
            IScript script = mocks.DynamicMock<IScript>();
            ProviderInfo providerMock = mocks.DynamicMock<ProviderInfo>();

            using (mocks.Record())
            {
                SetupFolder(folder, new IFolder[0], new[]{script}, new IFile[0], "");
                SetupProject();
                SetupScriptFile(script);
                SetupProvider(providerMock);
                SetupController();
                SetupLoader(script);
				Expect.Call(() => projectInfo.AddGeneratedFile(new GeneratedFile(
					"Class.cs", "C:\\Temp\\Class.cs", "Class.cs", "Class", "Iterator")));
            }

			GenerationHelper helper = new GenerationHelper(progressHelper, loader, projectInfo, new FileController());
            ProjectFileTree tree = new ProjectFileTree();
			helper.GenerateAllFiles("", folder, tree, scriptObject, "C:\\Temp");

            Assert.That(tree.AllNodes.Count, Is.EqualTo(1));
            Assert.That(tree.AllNodes[0].Status, Is.EqualTo(ProjectFileStatusEnum.UnAnalysedFile));
            Assert.That(tree.AllNodes[0].Text, Is.EqualTo("Class.cs"));
            Assert.That(tree.ChildNodes[0].AssociatedFile.RelativeFilePath, Is.EqualTo("Class.cs"));

            TextFileInformation tfi = (TextFileInformation)tree.ChildNodes[0].AssociatedFile;
            Assert.That(tfi.NewGenFile.FilePath, 
                Is.EqualTo(Path.Combine(controller.GetTempFilePathForComponent(ComponentKey.Workbench_FileGenerator), "Class.cs")), 
                "New Gen file was not assigned to");

			mocks.VerifyAll();
        }

        private void SetupLoader(IScript script)
        {
            SetupLoader();

            object[] p = new object[] { script };
            Expect
                .Call(loader.CallTemplateFunction("Class", ref p))
                .Return("public class SomeClass { }")
                .Repeat.Any();
			Expect
				.Call(loader.GetTemplateFunctionLanguage(null))
                .Return("Plain Text")
				.IgnoreArguments()
                .Repeat.Any();
        }

        private void SetupLoader()
        {
            object[] p = new object[0];
            Expect
                .Call(loader.CallTemplateFunction("InternalFunctions.MustSkipCurrentFile", ref p))
                .Return(false)
                .Repeat.Any();
			Expect
			   .Call(loader.CallTemplateFunction("InternalFunctions.MustSkipCurrentFile"))
			   .Return(false)
			   .Repeat.Any();

			Expect
				.Call(loader.GetAssemblyVersionNumber())
				.Return("99.99.99.99")
				.Repeat.Any();

            p = new object[] { scriptObject };
            Expect
                .Call(loader.CallTemplateFunction("Class", ref p))
                .Return("public class SomeClass { }")
                .Repeat.Any();
        }

        private void SetupProvider(ProviderInfo providerMock)
        {
            ProviderInfo provider;
            Expect
                .Call(projectInfo.GetIteratorTypeFromProviders("Iterator", out provider))
                .Return(typeof(IScriptBaseObject)).OutRef(providerMock)
                .Repeat.Any();
            Expect.Call(projectInfo.GetIteratorTypeFromProviders("Iterator"))
				.Return(typeof(IScriptBaseObject))
				.Repeat.Any();
			 Expect.Call(providerMock.GetAllObjectsOfType(null))
				 .IgnoreArguments()
				.Return(new System.Collections.Generic.List<IScriptBaseObject>())
				.Repeat.Any();
        }

        private static void SetupStaticFile(IFile file)
        {
            Expect.Call(file.Name).Return("file.jpg").Repeat.Any();
            Expect.Call(file.IteratorName).Return("Iterator").Repeat.Any();
            Expect.Call(file.StaticFileName).Return("file.jpg").Repeat.Any();
        }

        private static void SetupScriptFile(IScript script)
        {
            Expect.Call(script.FileName).Return("Class.cs").Repeat.Any();
            Expect.Call(script.IteratorName).Return("Iterator").Repeat.Any();
            Expect.Call(script.ScriptName).Return("Class").Repeat.Any();
        }

        private static void SetupFolder(IFolder folder, IFolder[] subFolders, IScript[] scripts, IFile[] files, string folderName)
        {
            Expect.Call(folder.SubFolders).Return(subFolders).Repeat.Any();
            Expect.Call(folder.Scripts).Return(scripts).Repeat.Any();
            Expect.Call(folder.Files).Return(files).Repeat.Any();
            Expect.Call(folder.Name).Return(folderName).Repeat.Any();
            Expect.Call(folder.IteratorName).Return("Iterator").Repeat.Any();
        }

        private void SetupProject()
        {
            Expect.Call(projectInfo.Options).Return(new System.Collections.Generic.List<IOption>()).Repeat.Any();
			Expect.Call(projectInfo.ProjectSettings).Return(new ProjectSettings{ OutputPath = @"C:\" }).Repeat.Any();
			Expect.Call(projectInfo.Providers).Return(new System.Collections.Generic.List<ProviderInfo>()).Repeat.Any();
            //Expect.Call(projectInfo.FileSkippingIsImplemented).Return(false).Repeat.Any();
        }

        private void SetupController()
        {
            Expect.Call(controller.GetTempFilePathForComponent(ComponentKey.Workbench_FileGenerator)).Return("C:\\Temp").Repeat.Any();
			Settings.Default.PerformMergeAnalysis = true;
			//Expect.Call(controller.SettingGet(Controller.SettingNames.PerformMergeAnalysis)).Return(true).Repeat.Any();
        }
    }
}
