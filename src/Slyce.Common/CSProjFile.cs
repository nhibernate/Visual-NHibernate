using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Slyce.Common
{
	public class CSProjFile
	{
		internal const string MSB_NAMESPACE = "http://schemas.microsoft.com/developer/msbuild/2003";

		private readonly XmlDocument doc;
		private readonly XmlNamespaceManager namespaceManager;

		public CSProjFile(string filepath)
		{
			FilePath = filepath;
			doc = new XmlDocument();
			doc.Load(FilePath);

			namespaceManager = GetNamespaceManagerForCSProjFile(doc);
		}

		public CSProjFile(XmlDocument doc, string filepath)
		{
			FilePath = filepath;
			this.doc = doc;
			namespaceManager = GetNamespaceManagerForCSProjFile(doc);
		}

		public string FilePath { get; private set; }

		public bool FileExists
		{
			get { return File.Exists(FilePath); }
		}

		public static XmlNamespaceManager GetNamespaceManagerForCSProjFile(XmlDocument doc)
		{
			XmlNamespaceManager nsManager = null;

			if (doc != null)
			{
				nsManager = new XmlNamespaceManager(doc.NameTable);
				nsManager.AddNamespace("msb", MSB_NAMESPACE);
			}
			return nsManager;
		}

		public string GetProjectGuid()
		{
			if (doc == null)
				return "";

			XmlNode node = doc.SelectSingleNode("/msb:Project/msb:PropertyGroup/msb:ProjectGuid", namespaceManager);

			if (node == null)
				return "";
			else
				return node.InnerText.Replace("{", "").Replace("}", "");
		}

		public IEnumerable<string> GetEmbeddedResources(Func<string, bool> matches)
		{
			var nodes = doc.SelectNodes("/msb:Project/msb:ItemGroup/msb:EmbeddedResource", namespaceManager);
			return GetFilesUsingInclude(nodes, matches);
		}

		public IEnumerable<string> GetReferencedAssemblies()
		{
			List<string> referencedFiles = new List<string>();

			foreach (XmlNode node in doc.SelectNodes("/msb:Project/msb:ItemGroup/msb:Reference", namespaceManager))
			{
				string filename = node.Attributes["Include"].Value;
				XmlNode pathNode = node.SelectSingleNode("msb:HintPath", namespaceManager);

				if (pathNode != null)
				{
					string path = pathNode.InnerText;
					int commaPos = filename.IndexOf(",");

					if (commaPos > 0)
						filename = filename.Substring(0, filename.IndexOf(","));

					filename = path; // Path.Combine(path, filename);
					filename = Slyce.Common.RelativePaths.GetFullPath(Path.GetDirectoryName(this.FilePath), filename);
				}
				referencedFiles.Add(filename);
			}
			return referencedFiles;
		}

		public IEnumerable<string> GetContentFiles(Func<string, bool> matches)
		{
			var nodes = doc.SelectNodes("/msb:Project/msb:ItemGroup/msb:Content", namespaceManager);
			return GetFilesUsingInclude(nodes, matches);
		}

		public string GetAssemblyName()
		{
			string outputType = doc.SelectSingleNode("/msb:Project/msb:PropertyGroup/msb:OutputType", namespaceManager).InnerText;
			string filename = doc.SelectSingleNode("/msb:Project/msb:PropertyGroup/msb:AssemblyName", namespaceManager).InnerText;

			return outputType.ToLower() == "library" ? filename + ".dll" : filename + ".exe";
		}

		public IEnumerable<string> GetOutputPaths()
		{
			var nodes = doc.SelectNodes("/msb:Project/msb:PropertyGroup/msb:OutputPath", namespaceManager);
			List<string> locations = new List<string>();
			string rootFolder = Path.GetDirectoryName(FilePath);

			foreach (XmlNode node in nodes)
				locations.Add(Path.Combine(rootFolder, node.InnerText));

			return locations;
		}

		public IEnumerable<string> GetResourceFiles(Func<string, bool> matches)
		{
			var nodes = doc.SelectNodes("/msb:Project/msb:ItemGroup/msb:Resource", namespaceManager);

			return GetFilesUsingInclude(nodes, matches);
		}

		public IEnumerable<string> GetFilesMarkedCompile(Func<string, bool> matches)
		{
			var nodes = doc.SelectNodes("/msb:Project/msb:ItemGroup/msb:Compile", namespaceManager);
			return GetFilesUsingInclude(nodes, matches);
		}

		public IEnumerable<string> GetFilesMarkedNone(Func<string, bool> matches)
		{
			var nodes = doc.SelectNodes("/msb:Project/msb:ItemGroup/msb:None", namespaceManager);
			return GetFilesUsingInclude(nodes, matches);
		}

		private IEnumerable<string> GetFilesUsingInclude(XmlNodeList nodes, Func<string, bool> matches)
		{
			if (nodes != null)
			{
				foreach (XmlNode node in nodes)
				{
					var includeAttr = node.Attributes["Include"];
					if (includeAttr != null && matches(includeAttr.Value))
					{
						yield return includeAttr.Value;
					}
				}
			}
		}

		private XmlNode EnsureItemGroupExists(string name)
		{
			XmlNode itemGroupNode = doc.SelectSingleNode(string.Format(@"/msb:Project/msb:ItemGroup/msb:{0}[1]", name), namespaceManager);

			if (itemGroupNode == null)
			{
				itemGroupNode = doc.CreateElement(name, MSB_NAMESPACE);
				doc.SelectSingleNode("/msb:Project", namespaceManager).AppendChild(itemGroupNode);
			}
			return itemGroupNode.ParentNode;
		}

		public XmlNode EnsureReferenceExists(string include, string specificVersion, string hintPath)
		{
			XmlNode node = doc.SelectSingleNode(string.Format(@"/msb:Project/msb:ItemGroup/msb:Reference[@Include='{0}']", include), namespaceManager);

			if (node == null)
			{
				XmlNode referenceItemGroupNode = EnsureItemGroupExists("Reference");
				node = doc.CreateElement("Reference", MSB_NAMESPACE);

				XmlAttribute includeAtt = doc.CreateAttribute("Include");
				includeAtt.Value = include;
				node.Attributes.Append(includeAtt);

				referenceItemGroupNode.AppendChild(node);

				if (!string.IsNullOrEmpty(specificVersion))
				{
					XmlNode specificVersionNode = doc.CreateElement("SpecificVersion", MSB_NAMESPACE);
					specificVersionNode.InnerText = specificVersion;
					node.AppendChild(specificVersionNode);
				}
				if (!string.IsNullOrEmpty(hintPath))
				{
					XmlNode hintPathNode = doc.CreateElement("HintPath", MSB_NAMESPACE);
					hintPathNode.InnerText = hintPath;
					node.AppendChild(hintPathNode);
				}
			}
			return node;
		}

		public XmlNode EnsureNoneFileExists(string include, string copyToOutputDirectory)
		{
			XmlNode node = doc.SelectSingleNode(string.Format(@"/msb:Project/msb:ItemGroup/msb:None[@Include='{0}']", include), namespaceManager);

			if (node == null)
			{
				XmlNode noneItemGroupNode = EnsureItemGroupExists("None");
				node = doc.CreateElement("None", MSB_NAMESPACE);

				XmlAttribute includeAtt = doc.CreateAttribute("Include");
				includeAtt.Value = include;
				node.Attributes.Append(includeAtt);

				noneItemGroupNode.AppendChild(node);

				if (!string.IsNullOrEmpty(copyToOutputDirectory))
				{
					XmlNode hintPathNode = doc.CreateElement("CopyToOutputDirectory", MSB_NAMESPACE);
					hintPathNode.InnerText = copyToOutputDirectory;
					node.AppendChild(hintPathNode);
				}
			}
			return node;
		}

		public XmlNode EnsurePropertyGroupItemExists(string itemName, string value)
		{
			XmlNode node = doc.SelectSingleNode(string.Format(@"/msb:Project/msb:PropertyGroup/msb:{0}", itemName), namespaceManager);

			if (node == null)
			{
				XmlNode propertyGroupNode = doc.SelectSingleNode(@"/msb:Project/msb:PropertyGroup[1]", namespaceManager);

				if (propertyGroupNode == null)
				{
					propertyGroupNode = doc.CreateElement("PropertyGroup", MSB_NAMESPACE);
					doc.SelectSingleNode("/msb:Project", namespaceManager).AppendChild(propertyGroupNode);
				}
				node = doc.CreateElement(itemName, MSB_NAMESPACE);
				node.InnerText = value;
				propertyGroupNode.AppendChild(node);
			}
			return node;
		}

		public XmlNode EnsureCompileFileExists(string include)
		{
			XmlNode node = doc.SelectSingleNode(string.Format(@"/msb:Project/msb:ItemGroup/msb:Compile[@Include='{0}']", include), namespaceManager);

			if (node == null)
			{
				XmlNode compileItemGroupNode = EnsureItemGroupExists("Compile");
				node = doc.CreateElement("Compile", MSB_NAMESPACE);

				XmlAttribute includeAtt = doc.CreateAttribute("Include");
				includeAtt.Value = include;
				node.Attributes.Append(includeAtt);

				compileItemGroupNode.AppendChild(node);
			}
			return node;
		}

		public XmlNode EnsureEmbeddedResourceExists(string include)
		{
			XmlNode node = doc.SelectSingleNode(string.Format(@"/msb:Project/msb:ItemGroup/msb:EmbeddedResource[@Include='{0}']", include), namespaceManager);

			if (node == null)
			{
				XmlNode compileItemGroupNode = EnsureItemGroupExists("EmbeddedResource");
				node = doc.CreateElement("EmbeddedResource", MSB_NAMESPACE);

				XmlAttribute includeAtt = doc.CreateAttribute("Include");
				includeAtt.Value = include;
				node.Attributes.Append(includeAtt);

				compileItemGroupNode.AppendChild(node);
			}
			return node;
		}

		public void RemoveReference(string include)
		{
			XmlNode node = doc.SelectSingleNode(string.Format("/msb:Project/msb:ItemGroup/msb:Reference[@Include='{0}']", include), namespaceManager);

			if (node != null)
			{
				doc.RemoveChild(node);
			}
		}

		public string Xml
		{
			get
			{
				using (StringWriter sw = new StringWriter())
				{
					using (XmlTextWriter xw = new XmlTextWriter(sw))
					{
						xw.Formatting = Formatting.Indented;
						doc.WriteTo(xw);
						return sw.ToString();
					}
				}
			}
		}
	}
}