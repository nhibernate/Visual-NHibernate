using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ArchAngel.Common;
using ArchAngel.Workbench.IntelliMerge;
using ArchAngel.Workbench.Properties;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using Slyce.Common;
using Slyce.IntelliMerge;
using Slyce.IntelliMerge.Controller;
using Is=NUnit.Framework.SyntaxHelpers.Is;
using MockIs = Rhino.Mocks.Constraints.Is;
namespace ArchAngel.Workbench.Tests
{
    [TestFixture]
    public class TestAnalysisHelper
    {
        private MockRepository mocks;
        private IController controller;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            using (mocks.Record())
            {
                SetupController();
            }
        }

        [TearDown]
        public void Teardown()
        {
            if(Directory.Exists("C:\\doesnotexist"))
            {
                Directory.Delete("C:\\doesnotexist", true);
            }
        }

        private class AnalysisProgressHelperStub : AnalysisProgressHelper
        {
            public List<object> LastProgressObjectList = new List<object>();

            public override bool IsCancellationPending()
            {
                return false;
            }

            public override void Cancel()
            {
                throw new NotImplementedException();
            }

            public override void ReportProgress(int percentageComplete, object userState)
            {
                LastProgressObjectList.Add(userState);
            }
        }

        [Test]
        public void BasicRun()
        {
            AnalysisProgressHelperStub helperStub = new AnalysisProgressHelperStub();

            AnalysisHelper ah = new AnalysisHelper();
            ah.StartAnalysis(helperStub, controller, new ProjectFileTree());

            Assert.That(helperStub.Count, Is.EqualTo(0));
            Assert.That(helperStub.LastProgressObjectList.Count, Is.EqualTo(2));

            AnalyseFilesProgress progress = (AnalyseFilesProgress)helperStub.LastProgressObjectList[1];
            Assert.That(progress, Is.Not.Null);
            Assert.That(progress.NumberOfConflicts, Is.EqualTo(0));
            Assert.That(progress.NumberOfResolved, Is.EqualTo(0));
            Assert.That(progress.NumberOfExactCopies, Is.EqualTo(0));
        }

        [Test]
		[Ignore]
        public void SingleFile()
        {
            AnalysisProgressHelperStub helperStub = new AnalysisProgressHelperStub();
            ProjectFileTree tree = new ProjectFileTree();
            TextFileInformation tfi = new TextFileInformation();
            tfi.RelativeFilePath = "Class.cs";
            tfi.PrevGenFile = tfi.NewGenFile = tfi.UserFile = new TextFile("public class SomeClass { }");
            tree.AddChildNode(tfi, "Class.cs");

            AnalysisHelper ah = new AnalysisHelper();
            ah.StartAnalysis(helperStub, controller, tree);

            Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));

            Assert.That(helperStub.Count, Is.EqualTo(0));
            Assert.That(helperStub.LastProgressObjectList.Count, Is.EqualTo(4));

            AnalyseFilesProgress progress = (AnalyseFilesProgress)helperStub.LastProgressObjectList[2];
            Assert.That(progress, Is.Not.Null);
            Assert.That(progress.NumberOfConflicts, Is.EqualTo(0));
            Assert.That(progress.NumberOfResolved, Is.EqualTo(0));
            Assert.That(progress.NumberOfExactCopies, Is.EqualTo(0));
            Assert.That(progress.ChangedFilePath, Is.EqualTo("Class.cs"));
            Assert.That(progress.ChangedFileStatus, Is.EqualTo(ProjectFileStatusEnum.Busy));

            progress = (AnalyseFilesProgress)helperStub.LastProgressObjectList[3];
            Assert.That(progress, Is.Not.Null);
            Assert.That(progress.NumberOfConflicts, Is.EqualTo(0));
            Assert.That(progress.NumberOfResolved, Is.EqualTo(0));
            Assert.That(progress.NumberOfExactCopies, Is.EqualTo(1));
            Assert.That(progress.ChangedFilePath, Is.EqualTo("Class.cs"));
            Assert.That(progress.ChangedFileStatus, Is.EqualTo(ProjectFileStatusEnum.AnalysedFile));
			Assert.That(tfi.CalculateMergedFile(), Is.True);
			Assert.That(tfi.MergedFile.HasContents, Is.True);
            string nl = Environment.NewLine;
            Assert.That(tfi.MergedFile.GetContents(), Is.EqualTo(nl + "public class SomeClass"+nl+"{"+nl+nl+"}"));
        }

        [Test]
		[Ignore]
        public void Folder_And_File()
        {
            AnalysisProgressHelperStub helperStub = new AnalysisProgressHelperStub();
            ProjectFileTree tree = new ProjectFileTree();
            TextFileInformation tfi = new TextFileInformation();
            tfi.RelativeFilePath = "Class.cs";
            tfi.PrevGenFile = tfi.NewGenFile = tfi.UserFile = new TextFile("public class SomeClass { }");
            tree.AddChildNode("folder").AddChildNode(tfi, "Class.cs");

            AnalysisHelper ah = new AnalysisHelper();
            ah.StartAnalysis(helperStub, controller, tree);

            Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.ExactCopy));


            Assert.That(helperStub.Count, Is.EqualTo(0));
            Assert.That(helperStub.LastProgressObjectList.Count, Is.EqualTo(4));

            AnalyseFilesProgress progress = (AnalyseFilesProgress)helperStub.LastProgressObjectList[2];
            Assert.That(progress, Is.Not.Null);
            Assert.That(progress.NumberOfConflicts, Is.EqualTo(0));
            Assert.That(progress.NumberOfResolved, Is.EqualTo(0));
            Assert.That(progress.NumberOfExactCopies, Is.EqualTo(0));
            Assert.That(progress.ChangedFilePath, Is.EqualTo("folder\\Class.cs"));
            Assert.That(progress.ChangedFileStatus, Is.EqualTo(ProjectFileStatusEnum.Busy));

            progress = (AnalyseFilesProgress)helperStub.LastProgressObjectList[3];
            Assert.That(progress, Is.Not.Null);
            Assert.That(progress.NumberOfConflicts, Is.EqualTo(0));
            Assert.That(progress.NumberOfResolved, Is.EqualTo(0));
            Assert.That(progress.NumberOfExactCopies, Is.EqualTo(1));
            Assert.That(progress.ChangedFilePath, Is.EqualTo("folder\\Class.cs"));
            Assert.That(progress.ChangedFileStatus, Is.EqualTo(ProjectFileStatusEnum.AnalysedFile));
            string nl = Environment.NewLine;
			Assert.That(tfi.CalculateMergedFile(), Is.True);
			Assert.That(tfi.MergedFile.HasContents, Is.True);
            Assert.That(tfi.MergedFile.GetContents(), Is.EqualTo(nl + "public class SomeClass" + nl + "{" + nl + nl + "}"));
        }

        private void SetupController()
        {
            controller = mocks.DynamicMock<IController>();
            Expect.Call(controller.GetTempFilePathForComponent(ComponentKey.Workbench_FileGenerator)).IgnoreArguments().Return("C:/Temp").Repeat.Any();
			Settings.Default.PerformMergeAnalysis = true;
			//Expect.Call(controller.SettingGet(Controller.SettingNames.PerformMergeAnalysis)).Return(true).Repeat.Any();
            ProjectSettings settings = new ProjectSettings();
            settings.OutputPath = "C:\\doesnotexist";
			WorkbenchProject project = new WorkbenchProject();
        	project.ProjectSettings = settings;
        	Expect.Call(controller.CurrentProject).Return(project);
        }
    }
}
