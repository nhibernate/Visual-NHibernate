using System;
using System.IO;
using System.Xml;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;
using Slyce.IntelliMerge.Controller.ManifestWorkers;
using Specs_For_PrevGenUtility_Copy_Working_PrevGen_Files;

namespace Specs_For_PrevGenUtility_Copy_Working_PrevGen_Files
{
    public class TestBaseClass
    {
        protected static readonly string RESOURCES_FOLDER = Path.Combine("Resources", "Manifest");
        protected static readonly string SCRATCH_FOLDER = Path.Combine(RESOURCES_FOLDER, "TestScratchFolder");

        protected PrevGenUtility utility = new PrevGenUtility();

        protected void ClearScratchFolder()
        {
            lock(SCRATCH_FOLDER)
            {
                if(Directory.Exists(SCRATCH_FOLDER))
                    Directory.Delete(SCRATCH_FOLDER, true);
                Directory.CreateDirectory(SCRATCH_FOLDER);
                utility = new PrevGenUtility();
            }
        }

        protected static void DeleteScratchFolder()
        {
            if (Directory.Exists(SCRATCH_FOLDER))
                Directory.Delete(SCRATCH_FOLDER, true);
        }
    }

    [TestFixture]
    public class When_files_exist_in_the_top_level_directory : TestBaseClass
    {
        private readonly string Directory = Path.Combine(RESOURCES_FOLDER, "TLDw");

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
            utility.CopyProgramPrevGenFiles(Directory, SCRATCH_FOLDER, new Guid("{c1285b30-66b7-4e2f-a85c-6d169d9977c7}"), "TemplateName");

            string aaDir = Path.Combine(SCRATCH_FOLDER, ".ArchAngel");
            aaDir = Path.Combine(aaDir, "{c1285b30-66b7-4e2f-a85c-6d169d9977c7}".ToUpper()+"_TemplateName");

            Assert.That(File.Exists(Path.Combine(aaDir, ManifestConstants.MANIFEST_FILENAME)));
            Assert.That(File.Exists(Path.Combine(aaDir, "X.cs")));
        }
    }


    [TestFixture]
    public class When_files_exist_in_sub_directories : TestBaseClass
    {
        private readonly string Directory = Path.Combine(RESOURCES_FOLDER, "SubDirsw");

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
            utility.CopyProgramPrevGenFiles(Directory, SCRATCH_FOLDER, new Guid("{c1285b30-66b7-4e2f-a85c-6d169d9977c7}"), "TemplateName");
            
            string aaDir = Path.Combine(SCRATCH_FOLDER, ".ArchAngel");
            aaDir = Path.Combine(aaDir, "{c1285b30-66b7-4e2f-a85c-6d169d9977c7}".ToUpper() + "_TemplateName");

            Assert.That(File.Exists(Path.Combine(aaDir, ManifestConstants.MANIFEST_FILENAME)));
            Assert.That(File.Exists(Path.Combine(aaDir, "X.cs")));

            aaDir = Path.Combine(SCRATCH_FOLDER, Path.Combine("SubDir", ".ArchAngel"));
            aaDir = Path.Combine(aaDir, "{c1285b30-66b7-4e2f-a85c-6d169d9977c7}".ToUpper() + "_TemplateName");

            Assert.That(File.Exists(Path.Combine(aaDir, ManifestConstants.MANIFEST_FILENAME)));
            Assert.That(File.Exists(Path.Combine(aaDir, "Y.cs")));
        }
    }
}

namespace Specs_For_PrevGenUtility_Copy_Users_PrevGen_Files
{
	[TestFixture]
	public class When_Target_Directory_Is_Not_Empty : TestBaseClass
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
		public void The_directory_is_cleared_before_files_are_copied()
		{
			const string unwantedFile = "TestFile.txt";
			using(StreamWriter stream = File.CreateText(Path.Combine(SCRATCH_FOLDER, unwantedFile)))
			{
				stream.Write("klsdflkj");
			}
			
			utility.CopyUserPrevGenFiles(Directory, SCRATCH_FOLDER, new Guid("{c1285b30-66b7-4e2f-a85c-6d169d9977c7}"));

			Assert.That(File.Exists(Path.Combine(SCRATCH_FOLDER, ManifestConstants.MANIFEST_FILENAME)), Is.True, "Files not copied");
			Assert.That(File.Exists(Path.Combine(SCRATCH_FOLDER, "X.cs")), Is.True, "Files not copied");
			Assert.That(File.Exists(Path.Combine(SCRATCH_FOLDER, unwantedFile)), Is.False,"Directory not cleared");
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
            utility.CopyUserPrevGenFiles(Directory, SCRATCH_FOLDER, new Guid("{c1285b30-66b7-4e2f-a85c-6d169d9977c7}"));

            Assert.That(File.Exists(Path.Combine(SCRATCH_FOLDER, ManifestConstants.MANIFEST_FILENAME)), "Files not copied");
            Assert.That(File.Exists(Path.Combine(SCRATCH_FOLDER, "X.cs")), "Files not copied");
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
            utility.CopyUserPrevGenFiles(Directory, SCRATCH_FOLDER, new Guid("{c1285b30-66b7-4e2f-a85c-6d169d9977c7}"));

            string subdir = Path.Combine(SCRATCH_FOLDER, "SubDir");

            Assert.That(File.Exists(Path.Combine(SCRATCH_FOLDER, ManifestConstants.MANIFEST_FILENAME)));
            Assert.That(File.Exists(Path.Combine(SCRATCH_FOLDER, "X.cs")));

            Assert.That(File.Exists(Path.Combine(subdir, ManifestConstants.MANIFEST_FILENAME)));
            Assert.That(File.Exists(Path.Combine(subdir, "Y.cs")));
        }
    }
}

namespace Specs_For_PrevGenUtility_CreateManifestFile
{
    [TestFixture]
    public class When_files_exist_in_sub_directories : TestBaseClass
    {
        private readonly string TLD = Path.Combine(RESOURCES_FOLDER, "TLD");

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
        public void Xml_Document_Returned()
        {
        	XmlDocument doc = new XmlDocument();
			utility.AddMD5InfoToManifest(doc, TLD, TLD, TLD);

            TestDocument(doc);
        }

		[Test]
		public void Xml_Document_Cleared_Before_Adding_New_Node()
		{
			XmlDocument doc = new XmlDocument();

			XmlElement root = doc.CreateElement(ManifestConstants.ManifestElement);
			doc.AppendChild(root);

			XmlElement node = doc.CreateElement(ManifestConstants.FileElement);
			XmlAttribute attr = doc.CreateAttribute(ManifestConstants.FilenameAttribute);
			attr.Value = "X.cs";
			node.Attributes.Append(attr);
			root.AppendChild(node);

			utility.AddMD5InfoToManifest(doc, TLD, TLD, TLD);

			TestDocument(doc);
		}

        public void TestDocument(XmlDocument doc)
        {
            Assert.That(doc, Is.Not.Null);
            Assert.That(doc.ChildNodes[0].Name, Is.EqualTo("Manifest"));

            XmlNode root = doc.ChildNodes[0];
            Assert.That(root.ChildNodes, Has.Count(1));

            XmlElement node = (XmlElement)root.ChildNodes[0];
            Assert.That(node.Name, Is.EqualTo("File"));
            Assert.That(node.HasAttribute("filename"), Is.True);
            Assert.That(node.GetAttribute("filename"), Is.EqualTo("X.cs"));
            Assert.That(node.ChildNodes, Has.Count(3));

            XmlNode child = node.ChildNodes[0];
            string checksum = Utility.GetCheckSumOfString(File.ReadAllText(Path.Combine(TLD, "X.cs")));
            Assert.That(child.InnerText, Is.EqualTo(checksum));
            child = node.ChildNodes[1];
            Assert.That(child.InnerText, Is.EqualTo(checksum));
            child = node.ChildNodes[2];
            Assert.That(child.InnerText, Is.EqualTo(checksum));
        }
    }
}


namespace Specs_For_PrevGenUtility_CreateManifestFiles
{
    [TestFixture]
    public class When_A_Single_File_Exists : TestBaseClass
    {
        private readonly string MANIFEST_FOLDER = Path.Combine(RESOURCES_FOLDER, "Manifest");

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
        public void The_Manifest_File_Contains_The_Correct_MD5()
        {
            File.Copy(Path.Combine(MANIFEST_FOLDER, "X.cs"), Path.Combine(SCRATCH_FOLDER, "X.cs"), true);

            utility.CreateManifestFiles(SCRATCH_FOLDER, MANIFEST_FOLDER, MANIFEST_FOLDER);

            string expectedManifestFilename = Path.Combine(SCRATCH_FOLDER, ManifestConstants.MANIFEST_FILENAME);
            XmlDocument doc = new XmlDocument();
            doc.Load(expectedManifestFilename);

            TestDocument(doc);
        }

        protected static void TestDocument(XmlDocument doc)
        {
            Assert.That(doc, Is.Not.Null);
            Assert.That(doc.ChildNodes[0].Name, Is.EqualTo("Manifest"));

            XmlNode root = doc.ChildNodes[0];
            Assert.That(root.ChildNodes, Has.Count(1));

            XmlElement node = (XmlElement)root.ChildNodes[0];
            Assert.That(node.Name, Is.EqualTo("File"));
            Assert.That(node.HasAttribute("filename"), Is.True);
            Assert.That(node.GetAttribute("filename"), Is.EqualTo("X.cs"));
            Assert.That(node.ChildNodes, Has.Count(3));

            XmlNode child = node.ChildNodes[0];
            string checksum = Utility.GetCheckSumOfString(File.ReadAllText(Path.Combine(SCRATCH_FOLDER, "X.cs")));
            Assert.That(child.InnerText, Is.EqualTo(checksum));
            child = node.ChildNodes[1];
            Assert.That(child.InnerText, Is.EqualTo(checksum));
            child = node.ChildNodes[2];
            Assert.That(child.InnerText, Is.EqualTo(checksum));
        }
    }
}