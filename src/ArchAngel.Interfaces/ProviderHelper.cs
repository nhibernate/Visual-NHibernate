using System;
using System.IO;
using System.Xml;
using Slyce.Common;

namespace ArchAngel.Interfaces
{
	public class ProviderHelper
	{
		/// <summary>
		/// Populates the provider with the saved data from the given project file.
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="projectFilePath"></param>
		public static void PopulateProviderFromProjectFile(ProviderInfo provider, string projectFilePath)
		{
			if (string.IsNullOrEmpty(projectFilePath)) throw new ArgumentException("Project File path cannot be empty");
			if (provider == null) throw new ArgumentException("Provider to populate cannot be null");

			DirectoryInfo parent = Directory.GetParent(projectFilePath);
			if (parent != null)
			{
				string projectDirectory = parent.FullName;
				string folder = GetProjectFilesFolder(projectFilePath);
				string providerDirectory = Path.Combine(projectDirectory, folder);

				PopulateProvider(provider, providerDirectory);
			}
		}

		public static string GetProjectFilesDirectoryName(string projectFile)
		{
			string dir = Path.GetFileNameWithoutExtension(projectFile) + "_files";
			return dir;
		}

		public static string GetProjectFilesFolder(string projectFilename)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(projectFilename);

			string folder = GetProjectFilesDirectoryName(projectFilename);

			if (!Directory.Exists(RelativePaths.RelativeToAbsolutePath(Path.GetDirectoryName(projectFilename), folder)))

				try
				{
					NodeProcessor proc = new NodeProcessor(doc.DocumentElement);
					folder = proc.GetString("Folder");
				}
				catch
				{
					throw new Exception("Files folder not found for this project.\nIf your project file is called 'xyz.wbproj' then the files folder should be called 'xyz_files'.");
				}
			return RelativePaths.RelativeToAbsolutePath(Path.GetDirectoryName(projectFilename), folder);
		}

		/// <summary>
		/// Populates the provider with the saved data from the folder that contains the Provider Data folders.
		/// Don't use this method if you could use PopulateProviderFromProjectFile. It has the logic for finding
		/// the right folder from the projectFile.
		/// For example, we would be expecting to be given the full path to Project Files under this scheme:
		///
		/// Project Files
		/// |--> ExampleProvider_data
		///      |--> somefile.xml
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="folder"></param>
		public static void PopulateProvider(ProviderInfo provider, string folder)
		{
			string providerFolder = folder.PathSlash(provider.Assembly.GetName().Name.Replace(".", "_") + "_data");

			if (Directory.Exists(providerFolder))
			{
				provider.Open(providerFolder);
			}
			else if (provider.Assembly.FullName.IndexOf("ArchAngel.Providers.Database") >= 0)
			{
				// Check for a non-zipped version of 'provider_database.settings'. This is for backwards compatibility with older versions of ArchAngel.
				string dbFile = folder.PathSlash("provider_database.settings");
				string zipFile = folder.PathSlash("provider_database_data.zip");
				string tempFolder = GetProviderTempFolder(provider);

				if (File.Exists(zipFile))
				{
					Utility.UnzipFile(zipFile, tempFolder);
					provider.Open(tempFolder);
				}
				else if (File.Exists(dbFile))
				{
					File.Copy(dbFile, tempFolder.PathSlash(Path.GetFileName(dbFile)));
					provider.Open(tempFolder);
				}
				// Delete the temp folder
				Utility.DeleteDirectoryBrute(tempFolder);
			}
		}

		/// <summary>
		/// Returns the path string of the folder where assemblies are temporarily put for processing.
		/// The directory is not created.
		/// </summary>
		/// <param name="provider">The provider to get the assembly name from.</param>
		/// <returns>A temporary path to place files associated with a provider.</returns>
		public static string GetProviderTempFolder(ProviderInfo provider)
		{
			return Path.GetTempPath().PathSlash("ArchAngel").PathSlash("ProviderTemp")
				.PathSlash(provider.Assembly.GetName().Name.Replace(".", "_") + "Temp");
		}
	}
}
