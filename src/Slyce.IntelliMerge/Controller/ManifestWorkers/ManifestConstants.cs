using System;
using System.IO;
using System.Xml;

namespace Slyce.IntelliMerge.Controller.ManifestWorkers
{
    public static class ManifestConstants
    {
    	public const string FilenameAttribute = "filename";
    	public const string FileElement = "File";
    	public const string CodeRootMappingFilenameAttribute = "filename";
    	/* Filenames */
        public const string MANIFEST_FILENAME = "__AAManifest.xml";
        public const string ArchAngelFolder = ".ArchAngel";

        /* Xml Element Names */
        public const string ManifestElement = "Manifest";
        
        public const string MappingsElement = "Mappings";
        public const string CodeRootMappingsElement = "CodeRootMappings";
        public const string CodeRootMappingElement = "CodeRootMap";
        public const string UserObjectElement = "UserObject";
        public const string NewGenObjectElement = "NewGenObject";
        public const string PrevGenObjectElement = "PrevGenObject";

        public const string UserMD5Element = "UserMD5";
        public const string TemplateMD5Element = "TemplateMD5";
        public const string PrevGenMD5Element = "PrevGenMD5";

		/// <summary>
		/// Loads the manifest file in the specified file, or returns a new XmlDocument.
		/// </summary>
		/// <param name="filename">The file to try load from.</param>
		/// <returns>The manifest file in the specified file, or returns a new XmlDocument.</returns>
		public static XmlDocument LoadManifestDocument(string filename)
		{
			XmlDocument doc = new XmlDocument();

			// If the manifest file already exists, load it. Otherwise, just use a new XmlDocument.
			if (File.Exists(filename))
			{
				try
				{
					doc.LoadXml(File.ReadAllText(filename));
				}
				catch (Exception)
				{
					doc = new XmlDocument();
				}
			}

			return doc;
		}
    }
}
