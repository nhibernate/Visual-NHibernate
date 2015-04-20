using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Slyce.Common
{
	// This interface exists so I can mock out file write operations in my unit tests.
	public interface IFileController
	{
		void WriteStreamToFile(Stream input, string filePath);
		void WriteResourceToFile(Assembly assembly, string resourceName, string path);
		void WriteAllText(string path, string contents);
		string ReadAllText(string path);
		byte[] ReadAllBytes(string path);
		bool CanCreateFilesIn(string directoryPath);
		bool DirectoryExists(string directoryPath);
		bool CanReadFilesFrom(string directoryPath);
		IEnumerable<string> FindAllFilesLike(string directoryPath, string searchExpression);
		IEnumerable<string> FindAllFilesLike(string directoryPath, string searchExpression, SearchOption option);
		bool FileExists(string filePath);
		bool CanReadFile(string filePath);
		string ToAbsolutePath(string relativePath, string baseFilename);
		void CreateDirectory(string folder);
		void WriteAllBytes(string path, byte[] contents);

		IEnumerable<string> GetAllFilesFrom(string folder);
		/// <summary>
		/// Gets the full absolute path for a given filename, expanding all ..s and .s
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		string GetFullPath(string filename);
	}

	public class FileController : IFileController
	{
		public void WriteStreamToFile(Stream input, string filePath)
		{
			Utility.WriteStreamToFile(input, filePath);
		}

		public void WriteResourceToFile(Assembly assembly, string resourceName, string path)
		{
			Utility.WriteResourceToFile(assembly, resourceName, path);
		}

		public void WriteAllText(string path, string contents)
		{
			try
			{
				if (Directory.Exists(Path.GetDirectoryName(path)) == false)
					Directory.CreateDirectory(Path.GetDirectoryName(path));
				File.WriteAllText(path, contents, System.Text.Encoding.Unicode);
			}
			catch (PathTooLongException)
			{
				throw new PathTooLongException("The path \"" + path + "\" is longer than 260 characters, or has a directory name longer than 240 characters.");
			}
		}

		public void WriteAllBytes(string path, byte[] contents)
		{
			try
			{
				if (Directory.Exists(Path.GetDirectoryName(path)) == false)
					Directory.CreateDirectory(Path.GetDirectoryName(path));
				File.WriteAllBytes(path, contents);
			}
			catch (PathTooLongException)
			{
				throw new PathTooLongException("The path \"" + path + "\" is longer than 260 characters, or has a directory name longer than 240 characters.");
			}
		}

		public string ReadAllText(string path)
		{
			return File.ReadAllText(path);
		}

		public byte[] ReadAllBytes(string path)
		{
			return File.ReadAllBytes(path);
		}

		public bool CanCreateFilesIn(string directoryPath)
		{
			// Add check to see if the directory is writable. I don't know how to do
			// this and google is giving me no answers.
			return DirectoryExists(directoryPath);
		}

		public bool DirectoryExists(string directoryPath)
		{
			return Directory.Exists(directoryPath);
		}

		public bool CanReadFilesFrom(string directoryPath)
		{
			// Add check to see if the directory is readable. I don't know how to do
			// this and google is giving me no answers.
			return DirectoryExists(directoryPath);
		}

		public IEnumerable<string> FindAllFilesLike(string directoryPath, string searchExpression)
		{
			return Directory.GetFiles(directoryPath, searchExpression);
		}

		public IEnumerable<string> FindAllFilesLike(string directoryPath, string searchExpression, SearchOption option)
		{
			return Slyce.Common.Utility.GetFiles(directoryPath, searchExpression, option);
		}

		public IEnumerable<string> GetAllFilesFrom(string folder)
		{
			return Directory.GetFiles(folder);
		}

		public string GetFullPath(string filename)
		{
			return Path.GetFullPath(filename);
		}

		public bool FileExists(string filePath)
		{
			return File.Exists(filePath);
		}

		public bool CanReadFile(string filePath)
		{
			// Still haven't figured out file permissions.
			return FileExists(filePath);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="relativePath"></param>
		/// <param name="baseFilename"></param>
		/// <returns></returns>
		public string ToAbsolutePath(string relativePath, string baseFilename)
		{
			string basePath;
			if (string.IsNullOrEmpty(baseFilename))
			{
				basePath = "";
			}
			else
			{
				basePath = Path.GetDirectoryName(baseFilename);
			}
			return RelativePaths.RelativeToAbsolutePath(basePath, relativePath);
		}

		public void CreateDirectory(string folder)
		{
			Directory.CreateDirectory(folder);
		}
	}
}
