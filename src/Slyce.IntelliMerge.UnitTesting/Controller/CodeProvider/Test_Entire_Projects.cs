using System;
using System.Collections.Generic;
using System.IO;
using ArchAngel.Providers.CodeProvider;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_CSharp_Formatter
{
	[TestFixture]
	public class Test_Entire_Projects
	{
		private List<string> _Files = new List<string>();

		[SetUp]
		public void Setup()
		{
			_Files = new List<string>();
		}

		public void LoadFilesToTestFrom(string folder)
		{
			DirectoryInfo dir = new DirectoryInfo(folder);
			// --- Add files in this folder
			foreach (FileInfo file in dir.GetFiles("*.cs"))
			{
				_Files.Add(file.FullName);
			}
			// --- Add files in subfolders
			foreach (DirectoryInfo subDir in dir.GetDirectories())
			{
				LoadFilesToTestFrom(subDir.FullName);
			}
		}

		private void TestFiles()
		{
			foreach(string file in _Files)
			{
				try
				{
					GC.Collect();
					CSharpParser parser = new CSharpParser();
					parser.ParseCode(file, File.ReadAllText(file));
					if (parser.ErrorOccurred == false) continue;
					Assert.IsNull(parser.ExceptionThrown, parser.ExceptionThrown == null ? "" : parser.ExceptionThrown.ToString());
					Assert.That(parser.SyntaxErrors.Count, Is.EqualTo(0), "There were syntax errors in file " + file);
				}
				catch(Exception e)
				{
					Assert.Fail(e.ToString());
				}
			}
		}

		[Test]
		[Ignore("This is just a smoke test.")]
		public void TestArchAngel()
		{
			const string folder = "../../../../";
			LoadFilesToTestFrom(folder);

			TestFiles();
		}
	}
}
