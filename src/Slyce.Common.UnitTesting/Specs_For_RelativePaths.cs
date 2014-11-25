using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;

namespace Specs_For_RelativePaths
{
	[TestFixture]
	public class GetRelativePath
	{
		[Test]
		public void SameDirectory()
		{
			string path = @"C:\Temp";
			string filename = @"C:\Temp\file.txt";

			Assert.That(RelativePaths.GetRelativePath(path, filename), Is.EqualTo(".\\file.txt"));
		}

		[Test]
		public void OneDirectoryUp()
		{
			string path = @"C:\Temp";
			string filename = @"C:\file.txt";

			Assert.That(RelativePaths.GetRelativePath(path, filename), Is.EqualTo("..\\file.txt"));
		}

		[Test]
		public void OneDirectoryDown()
		{
			string path = @"C:\Temp";
			string filename = @"C:\Temp\T\file.txt";

			Assert.That(RelativePaths.GetRelativePath(path, filename), Is.EqualTo(".\\T\\file.txt"));
		}

		[Test]
		public void DifferentDrives()
		{
			string path = @"C:\Temp";
			string filename = @"D:\file.txt";

			Assert.That(RelativePaths.GetRelativePath(path, filename), Is.EqualTo("D:\\file.txt"));
		}
	}

	[TestFixture]
	public class GetAbsolutePath
	{
		[Test]
		public void SameDirectory()
		{
			string path = @"C:\Temp";
			string filename = @"file.txt";

			Assert.That(RelativePaths.RelativeToAbsolutePath(path, filename), Is.EqualTo(@"C:\Temp\file.txt"));
		}

		[Test]
		public void OneDirectoryUp()
		{
			string path = @"C:\Temp";
			string filename = @"..\file.txt";

			Assert.That(RelativePaths.RelativeToAbsolutePath(path, filename), Is.EqualTo(@"C:\file.txt"));
		}

		[Test]
		public void OneDirectoryDown()
		{
			string path = @"C:\Temp";
			string filename = @"C:\Temp\T\file.txt";

			Assert.That(RelativePaths.RelativeToAbsolutePath(path, filename), Is.EqualTo(@"C:\Temp\T\file.txt"));
		}

		[Test]
		public void DifferentDrives()
		{
			string path = @"C:\Temp";
			string filename = @"D:\file.txt";

			Assert.That(RelativePaths.RelativeToAbsolutePath(path, filename), Is.EqualTo("D:\\file.txt"));
		}
	}
}
