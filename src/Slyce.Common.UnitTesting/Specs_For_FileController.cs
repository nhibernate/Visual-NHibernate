using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;

namespace Specs_For_FileController
{
	[TestFixture]
	public class When_Constructing_Relative_Paths
	{
		[Test]
		public void The_File_Should_Be_In_The_Same_Folder()
		{
			string baseFile = "folder\\file.txt";
			string relativePath = "file2.txt";

			IFileController controller = new FileController();
			Assert.That(controller.ToAbsolutePath(relativePath, baseFile), Is.EqualTo("folder\\file2.txt"));
		}

		[Test]
		public void The_File_Should_Be_In_The_SubFolder()
		{
			string baseFile = "folder\\file.txt";
			string relativePath = "folder2\\file2.txt";

			IFileController controller = new FileController();
			Assert.That(controller.ToAbsolutePath(relativePath, baseFile), Is.EqualTo("folder\\folder2\\file2.txt"));
		}
	}
}