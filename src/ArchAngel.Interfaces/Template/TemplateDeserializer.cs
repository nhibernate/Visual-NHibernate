using System;
using System.Text;
using System.Xml;

namespace ArchAngel.Interfaces.Template
{
	public class TemplateDeserializer
	{
		private static int FolderIdCounter = 0;

		public static TemplateProject DeserialiseTemplateProject(string xml)
		{
			FolderIdCounter = 2; // OutputFolder = 1
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			TemplateProject proj = DeserialiseTemplateProject(doc.DocumentElement);
			proj.IsDirty = false;
			return proj;
		}

		private static TemplateProject DeserialiseTemplateProject(XmlNode templateProjectNode)
		{
			TemplateProject project = new TemplateProject();

			var delimiterAtt = templateProjectNode.Attributes["delimiter-style"];

			if (delimiterAtt != null)
				project.Delimiter = (TemplateProject.DelimiterTypes)Enum.Parse(typeof(TemplateProject.DelimiterTypes), delimiterAtt.Value, true);

			foreach (XmlNode folderNode in templateProjectNode.SelectNodes("folder"))
			{
				Folder subFolder = DeserialiseFolder(folderNode);
				subFolder.ParentFolder = project.OutputFolder;
				project.OutputFolder.Folders.Add(subFolder);
			}
			foreach (XmlNode fileNode in templateProjectNode.SelectNodes("file"))
			{
				File file = DeserialiseFile(fileNode);
				file.ParentFolder = project.OutputFolder;
				project.OutputFolder.Files.Add(file);
			}
			foreach (XmlNode fileNode in templateProjectNode.SelectNodes("static-file"))
			{
				StaticFile file = DeserialiseStaticFile(fileNode);
				file.ParentFolder = project.OutputFolder;
				project.OutputFolder.StaticFiles.Add(file);
			}
			return project;
		}

		private static Folder DeserialiseFolder(XmlNode folderNode)
		{
			Folder folder = new Folder();
			folder.ID = FolderIdCounter++;
			folder.Name = folderNode.Attributes["name"].Value;
			folder.Iterator = (IteratorTypes)Enum.Parse(typeof(IteratorTypes), folderNode.Attributes["iterator"].Value, true);

			foreach (XmlNode fileNode in folderNode.SelectNodes("file"))
			{
				File file = DeserialiseFile(fileNode);
				file.ParentFolder = folder;
				folder.Files.Add(file);
			}
			foreach (XmlNode fileNode in folderNode.SelectNodes("static-file"))
			{
				StaticFile file = DeserialiseStaticFile(fileNode);
				file.ParentFolder = folder;
				folder.StaticFiles.Add(file);
			}
			foreach (XmlNode subFolderNode in folderNode.SelectNodes("folder"))
			{
				Folder subFolder = DeserialiseFolder(subFolderNode);
				subFolder.ParentFolder = folder;
				folder.Folders.Add(subFolder);
			}
			return folder;
		}

		private static File DeserialiseFile(XmlNode fileNode)
		{
			File file = new File();
			file.Id = int.Parse(fileNode.Attributes["id"].Value);
			file.Name = fileNode.Attributes["name"].Value;
			file.Iterator = (IteratorTypes)Enum.Parse(typeof(IteratorTypes), fileNode.Attributes["iterator"].Value, true);
			file.Script = new Script();
			file.Script.Syntax = (Slyce.Common.TemplateContentLanguage)Enum.Parse(typeof(Slyce.Common.TemplateContentLanguage), fileNode.Attributes["syntax"].Value, true);

			if (fileNode.Attributes["encoding"] == null)
				file.Encoding = Encoding.Unicode;
			else
				switch (fileNode.Attributes["encoding"].Value)
				{
					case "US-ASCII":
						file.Encoding = Encoding.ASCII;
						break;
					case "Unicode":
						file.Encoding = Encoding.Unicode;
						break;
					case "Unicode (UTF-8)":
						file.Encoding = Encoding.UTF8;
						break;
					default:
						throw new NotImplementedException("Encoding name not handled yet in DeserialiseFile(): " + fileNode.Attributes["encoding"].Value);
				}

			//var scriptNode = fileNode.SelectSingleNode("script");
			//file.Script = new Script();
			//file.Script.Body = scriptNode.InnerText;
			//file.Script.Syntax = (Slyce.Common.TemplateContentLanguage)Enum.Parse(typeof(Slyce.Common.TemplateContentLanguage), scriptNode.Attributes["syntax"].Value, true);

			return file;
		}

		private static StaticFile DeserialiseStaticFile(XmlNode fileNode)
		{
			StaticFile file = new StaticFile();
			file.Id = int.Parse(fileNode.Attributes["id"].Value);
			file.Name = fileNode.Attributes["name"].Value;
			file.Iterator = (IteratorTypes)Enum.Parse(typeof(IteratorTypes), fileNode.Attributes["iterator"].Value, true);
			file.ResourceName = fileNode.Attributes["resource-name"].Value;
			return file;
		}
	}
}
