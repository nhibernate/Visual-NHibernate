using System;
using System.IO;

namespace Slyce.Common
{
	public class RelativePaths
	{
		public static string GetFullPath(string basePath, string relativePath)
		{
			if (!string.IsNullOrEmpty(relativePath))
			{
				if (!Path.IsPathRooted(relativePath))
				{
					relativePath = Path.GetFullPath(Path.Combine(basePath, relativePath));
				}
			}
			return relativePath;
		}

		public static string GetRelativePath(string basePath, string absolutePath)
		{
			return GetRelativePath(basePath, absolutePath, false);
		}

		public static string GetRelativePath(string basePath, string absolutePath, bool startWithDotSlash)
		{
			basePath = basePath.TrimEnd('\\');

			if (!string.IsNullOrEmpty(absolutePath))
			{
				if (Path.IsPathRooted(absolutePath))
				{
					char[] separators = {
									Path.DirectorySeparatorChar, 
									Path.AltDirectorySeparatorChar, 
									Path.VolumeSeparatorChar 
								};
					//split the paths into their component parts
					string[] basePathParts = basePath.Split(separators);
					string[] absPathParts = absolutePath.Split(separators);
					int index;

					// Do the 2 paths have anything in common?
					int minLength = Math.Min(basePathParts.Length, absPathParts.Length);

					for (index = 0; index < minLength; ++index)
					{
						if (String.Compare(basePathParts[index], absPathParts[index], true) != 0)
						{
							break;
						}
					}
					// Do they have anything in common?
					if (index == 0)
					{
						// The 2 files have nothing in common, so just return the absolutePath
						return absolutePath;
					}
					// A relative path exists, so create it
					string relPath = "";

					if (index == basePathParts.Length)
					{
						if (startWithDotSlash)
						{
							// the entire base path is in the abs path
							// so the rel path starts with "./"
							relPath += "." + Path.DirectorySeparatorChar;
						}
					}
					else
					{
						// Move up a directory at a time, from the base to the common root 
						for (int i = index; i < basePathParts.Length; ++i)
						{
							relPath += ".." + Path.DirectorySeparatorChar;
						}
					}
					// add the path from the common root to the absPath
					relPath += String.Join(Path.DirectorySeparatorChar.ToString(), absPathParts, index, absPathParts.Length - index);
					absolutePath = relPath;
				}
			}
			return absolutePath;
		}

		/// <summary>
		/// Converts a given base and relative path to an absolute path
		/// </summary>
		/// <param name="basePath">The base directory path</param>
		/// <param name="relativePath">A path to the base directory path</param>
		/// <returns>An absolute path</returns>
		public static string RelativeToAbsolutePath(string basePath, string relativePath)
		{
			//if the relativePath isn't relative, just return it.
			if (Path.IsPathRooted(relativePath))
			{
				return relativePath;
			}

			// If basePath is empty, return relative path.
			if (string.IsNullOrEmpty(basePath))
			{
				return relativePath;
			}

			//split the paths into their component parts
			string[] basePathParts = basePath.Split(Path.DirectorySeparatorChar);
			string[] relPathParts = relativePath.Split(Path.DirectorySeparatorChar);

			//determine how many we must go up from the base path
			int index;

			for (index = 0; index < relPathParts.Length; ++index)
			{
				if (!relPathParts[index].Equals(".."))
				{
					break;
				}
			}
			//if the rel path contains no ".." it is below the base
			//therefor just concatonate the rel path to the base
			if (index == 0)
			{
				int offset = 0;
				//ignore the first part, if it is a rooting "."
				if (relPathParts[0] == ".") offset = 1;

				return basePath + Path.DirectorySeparatorChar + String.Join(Path.DirectorySeparatorChar.ToString(), relPathParts, offset, relPathParts.Length - offset);
			}
			string absPath = String.Join(Path.DirectorySeparatorChar.ToString(), basePathParts, 0, Math.Max(0, basePathParts.Length - index));
			absPath += Path.DirectorySeparatorChar + String.Join(Path.DirectorySeparatorChar.ToString(), relPathParts, index, relPathParts.Length - index);
			return absPath;
		}
	}
}
