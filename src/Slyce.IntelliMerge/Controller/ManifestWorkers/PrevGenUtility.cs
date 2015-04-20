using System;
using System.IO;
using System.Xml;
using Slyce.Common;

namespace Slyce.IntelliMerge.Controller.ManifestWorkers
{
	///<summary>
	/// Class used to generate Manifest files for ArchAngel projects, and copy PrevGen files to and fromt he temporary directories.
	///</summary>
	public class PrevGenUtility
	{
		/// <summary>
		/// Copies the user's prevgen folders to the staging folder, taking them out of the .ArchAngel folders at the same time.
		/// ProjectDir
		///    |- X.cs
		///    |- .ArchAngel
		///        |- {templateGuid}_TemplateName
		///            |- X.cs
		///            |- Manifest.xml
		///    |- BLL
		///        |- Y.cs
		///        |- .ArchAngel
		///            |- {templateGuid}_TemplateName              
		///                |- Y.cs
		///                |- Manifest.xml
		///  to:
		/// Staging
		///    |- X.cs
		///    |- Manifest.xml
		///    |- BLL
		///        |- Y.cs
		///        |- Manifest.xml
		/// </summary>
		/// <param name="userFolder">The user's project folder, which contains the .ArchAngel directories we are looking for.</param>
		/// <param name="stagingFolder">The folder we will store the temporary prevgen files while the user is working on the project.</param>
		/// <param name="templateGuid">The Guid of the Template we are currently working with.</param>
		public void CopyUserPrevGenFiles(string userFolder, string stagingFolder, Guid templateGuid)
		{
			userFolder = Path.GetFullPath(userFolder);
			stagingFolder = Path.GetFullPath(stagingFolder);
			if (Directory.Exists(stagingFolder))
			{
				Utility.DeleteDirectoryBrute(stagingFolder);
			}
			Directory.CreateDirectory(stagingFolder);
			string[] directories = Directory.GetDirectories(userFolder, ManifestConstants.ArchAngelFolder, SearchOption.AllDirectories);
			foreach (string aaDir in directories)
			{
				if (Directory.Exists(aaDir) == false)
					continue;

				DirectoryInfo parent = Directory.GetParent(aaDir);
				if (parent == null)
					continue;

				string[] potentialPrevgenDirs = Directory.GetDirectories(aaDir, templateGuid.ToString("B") + "*");
				if (potentialPrevgenDirs == null || potentialPrevgenDirs.Length == 0)
					continue;

				string prevgenDir = potentialPrevgenDirs[0];

				string[] files = Directory.GetFiles(prevgenDir);

				string relativeDirectoryPath = parent.FullName;
				relativeDirectoryPath = Utility.RelativePathTo(userFolder, relativeDirectoryPath);

				string destinationDirectory = Path.Combine(stagingFolder, relativeDirectoryPath);

				Directory.CreateDirectory(destinationDirectory);

				foreach (string file in files)
				{
					string newFilePath = Path.Combine(destinationDirectory, Path.GetFileName(file));
					File.Copy(file, newFilePath, true);
				}
			}
		}

		/// <summary>
		/// Copies the programs working copy of the prevgen folders to the users directory, adding them to .ArchAngel folders.
		/// Staging
		///    |- X.cs
		///    |- Manifest.xml
		///    |- BLL
		///        |- Y.cs
		///        |- Manifest.xml
		/// 
		/// to:
		/// 
		/// ProjectDir
		///    |- X.cs
		///    |- .ArchAngel
		///        |- {templateGuid}_TemplateName
		///            |- X.cs
		///            |- Manifest.xml
		///    |- BLL
		///        |- Y.cs
		///        |- .ArchAngel
		///            |- {templateGuid}_TemplateName              
		///                |- Y.cs
		///                |- Manifest.xml
		/// </summary>
		/// <param name="userFolder">The user's project folder, which we will write the prevgen files into.</param>
		/// <param name="stagingFolder">The folder we store the temporary prevgen files while the user is working on the project.</param>
		/// <param name="templateGuid">The template's Guid</param>
		/// <param name="templateName">The name of the template.</param>
		public void CopyProgramPrevGenFiles(string stagingFolder, string userFolder, Guid templateGuid, string templateName)
		{
			string[] directories = Directory.GetDirectories(stagingFolder, "*", SearchOption.AllDirectories);
			for (int i = -1; i < directories.Length; i++)
			{
				string directory;
				if (i == -1)
					directory = stagingFolder;
				else
					directory = directories[i];

				if (Directory.Exists(directory) == false)
					continue;

				string[] files = Directory.GetFiles(directory);

				string relativeDirectoryPath = Utility.RelativePathTo(stagingFolder, directory);
				string destinationDirectory = Path.Combine(userFolder, Path.Combine(relativeDirectoryPath, ManifestConstants.ArchAngelFolder));
				destinationDirectory =
					Path.Combine(destinationDirectory, templateGuid.ToString("B").ToUpper() + "_" + templateName);

				Directory.CreateDirectory(destinationDirectory);

				foreach (string file in files)
				{
					string newFilePath = Path.Combine(destinationDirectory, Path.GetFileName(file));
					File.Copy(file, newFilePath, true);
				}
			}
		}

		///<summary>
		/// Creates Manifest files for every directory in the prevgen folder.
		///</summary>
		///<param name="prevgenFolder">The folder containing the prevgen files.</param>
		///<param name="userFolder">The directory containing the users files.</param>
		///<param name="templateFolder">The directory containing the newly generated files.</param>
		public void CreateManifestFiles(string prevgenFolder, string userFolder, string templateFolder)
		{
			string[] subdirs = Directory.GetDirectories(prevgenFolder, "*", SearchOption.AllDirectories);
			string[] directories = new string[subdirs.Length + 1];
			subdirs.CopyTo(directories, 1);
			directories[0] = prevgenFolder;

			foreach (string dir in directories)
			{
				string relativePath = Utility.RelativePathTo(prevgenFolder, dir);

				string subUserFolder = Path.Combine(userFolder, relativePath);
				string subTemplateFolder = Path.Combine(templateFolder, relativePath);

				string manifestFilename = Path.Combine(dir, ManifestConstants.MANIFEST_FILENAME);
				XmlDocument doc = ManifestConstants.LoadManifestDocument(manifestFilename);
				AddMD5InfoToManifest(doc, dir, subUserFolder, subTemplateFolder);
				doc.Save(manifestFilename);
			}
		}

		///<summary>
		/// Gets the MD5 strings stored in the XmlDocument for the given filename.
		///</summary>
		///<param name="doc">The XML document the MD5s are stored in.</param>
		///<param name="filename">The name of the file to get the MD5s for.</param>
		///<param name="prevgenMD5">The MD5 of the prevgen file. "" if it does not exist.</param>
		///<param name="templateMD5">The MD5 of the template file. "" if it does not exist.</param>
		///<param name="userMD5">The MD5 of the user file. "" if it does not exist.</param>
		public static void GetMD5sForFile(XmlDocument doc, string filename, out string prevgenMD5, out string templateMD5, out string userMD5)
		{
			prevgenMD5 = "";
			templateMD5 = "";
			userMD5 = "";

			XmlNode filenode = doc.SelectSingleNode(string.Format("/Manifest/File[@filename=\"{0}\"]", filename));
			if (filenode != null)
			{
				XmlNode node = filenode.SelectSingleNode(ManifestConstants.UserMD5Element);
				if (node != null)
					userMD5 = node.InnerText;

				node = filenode.SelectSingleNode(ManifestConstants.TemplateMD5Element);
				if (node != null)
					templateMD5 = node.InnerText;

				node = filenode.SelectSingleNode(ManifestConstants.PrevGenMD5Element);
				if (node != null)
					prevgenMD5 = node.InnerText;
			}
		}

		/// <summary>
		/// Creates a Manifest XML Document that contains the MD5 checksums of files that exist in all 3 of the supplied folders.
		/// Does not recursively process folders.
		/// </summary>
		/// <param name="prevgenFolder">The folder containing the prevgen files. </param>
		/// <param name="userFolder">The folder containing the user's files</param>
		/// <param name="templateFolder">The folder containing the newly generated template files.</param>
		/// <param name="doc">The XmlDocument to add the MD5 information to.</param>
		public void AddMD5InfoToManifest(XmlDocument doc, string prevgenFolder, string userFolder, string templateFolder)
		{
			XmlElement root = doc.SelectSingleNode(ManifestConstants.ManifestElement) as XmlElement;
			if (root == null)
			{
				root = doc.CreateElement(ManifestConstants.ManifestElement);
				doc.AppendChild(root);
			}

			string[] files = Directory.GetFiles(templateFolder);
			foreach (string templateFilename in files)
			{
				string filename = Path.GetFileName(templateFilename);
				string userFilename = Path.Combine(userFolder, filename);
				string prevgenFilename = Path.Combine(prevgenFolder, filename);

				// If one of the other files does not exist, skip this prevgen file. We only
				// make MD5 hashes of files where all 3 exist.
				if (!File.Exists(userFilename) || !File.Exists(templateFilename)) continue;

				XmlElement fileElement = root.SelectSingleNode("File[@filename='" + filename + "']") as XmlElement;
				// If an element already exists for this file, remove it.
				if (fileElement != null)
				{
					root.RemoveChild(fileElement);
				}

				fileElement = doc.CreateElement("File");
				AddAttribute(fileElement, "filename", filename);
				root.AppendChild(fileElement);

				string userMD5 = File.Exists(userFilename) ? Utility.GetCheckSumOfString(File.ReadAllText(userFilename)) : "";
				string templateMD5 = File.Exists(templateFilename) ? Utility.GetCheckSumOfString(File.ReadAllText(templateFilename)) : "";
				string prevgenMD5 = File.Exists(prevgenFilename) ? Utility.GetCheckSumOfString(File.ReadAllText(prevgenFilename)) : "";

				AddElement(fileElement, ManifestConstants.UserMD5Element, userMD5);
				AddElement(fileElement, ManifestConstants.TemplateMD5Element, templateMD5);
				AddElement(fileElement, ManifestConstants.PrevGenMD5Element, prevgenMD5);
			}
		}

		private static void AddAttribute(XmlNode element, string attributeName, string attributeText)
		{
			XmlAttribute attr = element.OwnerDocument.CreateAttribute(attributeName);
			attr.InnerXml = attributeText;
			element.Attributes.Append(attr);
		}

		private static void AddElement(XmlNode element, string elementName, string elementText)
		{
			XmlElement ele = element.OwnerDocument.CreateElement(elementName);
			ele.InnerXml = elementText;
			element.AppendChild(ele);
		}

		/// <summary>
		/// Unzips the _AAZ file into a mirror folder structure so that diffing can be performed.
		/// </summary>
		/// <param name="fromFolder">Folder where the _AAZ resides.</param>
		/// <param name="toFolder">Temporary folder to mirror the extracted files.</param>
		public void CopyPrevGenFiles_AAZ(string fromFolder, string toFolder)
		{
			if (Directory.Exists(toFolder))
			{
				Utility.DeleteDirectoryBrute(toFolder);
			}
			Directory.CreateDirectory(toFolder);

			if (Directory.Exists(fromFolder))
			{
				// Look for a zip file in the fromFolder
				string temp = Path.Combine(fromFolder, "_ArchAngel.aaz");

				if (File.Exists(temp))
				{
					// Copy all the files into the toFolder
					Utility.UnzipFile(temp, toFolder);
				}
				// Process sub-folders
				string[] subFolders = Directory.GetDirectories(fromFolder);

				foreach (string subFolder in subFolders)
				{
					string dirName = subFolder.Substring(subFolder.LastIndexOf(Path.DirectorySeparatorChar) + 1);
					CopyPrevGenFiles_AAZ(subFolder, Path.Combine(toFolder, dirName));
				}
			}
		}
	}
}