using System.IO;
using System;

namespace Slyce.Common
{
	public class IOHelper
	{
		public static bool FileIsHidden(string filepath)
		{
			return ((File.GetAttributes(filepath) & FileAttributes.Hidden) == FileAttributes.Hidden);
		}

		public static bool FileIsReadOnly(string filepath)
		{
			return ((File.GetAttributes(filepath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);
		}

		public static bool FileIsArchive(string filepath)
		{
			return ((File.GetAttributes(filepath) & FileAttributes.Archive) == FileAttributes.Archive);
		}

		public static bool FileIsSystem(string filepath)
		{
			return ((File.GetAttributes(filepath) & FileAttributes.System) == FileAttributes.System);
		}

		public static void CopyDirectory(string sourceDir, string targetDir, bool copyHiddenFiles)
		{
			foreach (var dir in Directory.GetDirectories(sourceDir))
			{
				string subTargetDir = Path.Combine(targetDir, Path.GetFileName(dir));
				Directory.CreateDirectory(subTargetDir);
				CopyDirectory(dir, subTargetDir, copyHiddenFiles);
			}
			foreach (var file in Directory.GetFiles(sourceDir))
			{
				// Don't copy hidden files
				if (!copyHiddenFiles && FileIsHidden(file))
					continue;

				string targetFile = Path.Combine(targetDir, Path.GetFileName(file));

				if (File.Exists(targetFile))
				{
					if (FileIsReadOnly(targetFile))
					{
						FileInfo fileInfo = new FileInfo(targetFile);
						fileInfo.IsReadOnly = false;
					}
					File.Delete(targetFile);
				}
				try
				{
					File.Copy(file, targetFile);
				}
				catch (PathTooLongException ex)
				{
					throw new PathTooLongException(string.Format("Error while copying file.\n{0}\nCopy from: [{1}]\nCopy to: [{2}]", ex.Message, file, targetFile), ex);
				}
			}
		}
	}
}
