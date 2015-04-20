using System;
using System.IO;
using NUnit.Framework;
using Slyce.IntelliMerge.Controller.ManifestWorkers;

namespace Specs_For_BackupUtility
{
    public class TestBaseClass
    {
        protected static readonly string RESOURCES_FOLDER = Path.Combine("Resources", "Manifest");
        protected static readonly string SCRATCH_FOLDER = Path.Combine(RESOURCES_FOLDER, "TestScratchFolder");
        protected const string TemplateName = "TemplateName";

        protected BackupUtility utility = new BackupUtility();

        protected void ClearScratchFolder()
        {
            lock(SCRATCH_FOLDER)
            {
                if(Directory.Exists(SCRATCH_FOLDER))
                    Directory.Delete(SCRATCH_FOLDER, true);
                Directory.CreateDirectory(SCRATCH_FOLDER);
                utility = new BackupUtility();
            }
        }

        protected static void DeleteScratchFolder()
        {
            try
            {
                if (Directory.Exists(SCRATCH_FOLDER))
                    Directory.Delete(SCRATCH_FOLDER, true);
            }
            catch
            {
            	// Ignore. The setup method deletes the folder as well, we are just trying to be
            	// good citizens here.
            }
        }
    }
	[TestFixture]
	public class When_no_prevgen_files_exist : TestBaseClass
	{
		private readonly string Directory = Path.Combine(RESOURCES_FOLDER, "Manifest");
		[SetUp]
		public void Setup()
		{
			ClearScratchFolder();
		}

		[TearDown]
		public void TearDown()
		{
			DeleteScratchFolder();
		}

		[Test]
		public void The_Process_Doesnt_Fail()
		{
			string timeString = BackupUtility.GetTimeString();
			utility.CreateBackup(Directory, new Guid("{c1285b30-66b7-4e2f-a85c-6d169d9977c7}"), TemplateName, SCRATCH_FOLDER, timeString);
		}
	}

    [TestFixture]
    public class When_files_exist_in_the_top_level_directory : TestBaseClass
    {
        private readonly string Directory = Path.Combine(RESOURCES_FOLDER, "TLD");

        [SetUp]
        public void Setup()
        {
            ClearScratchFolder();
        }

        [TearDown]
        public void TearDown()
        {
            DeleteScratchFolder();
        }

        [Test]
        public void The_files_are_copied()
        {
            string timeString = BackupUtility.GetTimeString();
            utility.CreateBackup(Directory, new Guid("{c1285b30-66b7-4e2f-a85c-6d169d9977c7}"), TemplateName, SCRATCH_FOLDER, timeString);

            string aaDir = @"Resources\Manifest\TestScratchFolder\.ArchAngel\ArchAngelBackup";
            aaDir = Path.Combine(aaDir, timeString);

            string userDir = Path.Combine(aaDir, "UserFiles");
            Assert.That(File.Exists(Path.Combine(userDir, "X.cs")));

            string prevgenDir = Path.Combine(aaDir, "PrevGenFiles");
            Assert.That(File.Exists(Path.Combine(prevgenDir, "X.cs")));
            Assert.That(File.Exists(Path.Combine(prevgenDir, ManifestConstants.MANIFEST_FILENAME)));
        }
    }


    [TestFixture]
    public class When_files_exist_in_sub_directories : TestBaseClass
    {
        private readonly string Directory = Path.Combine(RESOURCES_FOLDER, "SubDirs");

        [SetUp]
        public void Setup()
        {
            ClearScratchFolder();
        }

        [TearDown]
        public void TearDown()
        {
            DeleteScratchFolder();
        }

        [Test]
        public void The_files_are_copied()
        {
            string timeString = BackupUtility.GetTimeString();
            utility.CreateBackup(Directory, new Guid("{c1285b30-66b7-4e2f-a85c-6d169d9977c7}"), TemplateName, SCRATCH_FOLDER, timeString);
            utility.CreateBackup(Path.Combine(Directory, "SubDir"), new Guid("{c1285b30-66b7-4e2f-a85c-6d169d9977c7}"), TemplateName, Path.Combine(SCRATCH_FOLDER, "SubDir"), timeString);

            string aaDir = @"Resources\Manifest\TestScratchFolder\SubDir\.ArchAngel\ArchAngelBackup".Replace("\\", Path.DirectorySeparatorChar.ToString());
            aaDir = Path.Combine(aaDir, timeString);

            string userDir = Path.Combine(aaDir, "UserFiles");
            Assert.That(File.Exists(Path.Combine(userDir, "Y.cs")), "User File exists");

            string prevgenDir = Path.Combine(aaDir, "PrevGenFiles");
            Assert.That(File.Exists(Path.Combine(prevgenDir, "Y.cs")), "Prevgen file exists");
            Assert.That(File.Exists(Path.Combine(prevgenDir, ManifestConstants.MANIFEST_FILENAME)), "Manifest file exists");
        }
    }
}