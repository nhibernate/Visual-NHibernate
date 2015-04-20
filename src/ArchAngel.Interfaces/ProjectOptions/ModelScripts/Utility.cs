using System;
using System.IO;
using System.Xml;

namespace ArchAngel.Interfaces.ProjectOptions.ModelScripts
{
	public class Utility
	{
		private readonly static string DEFAULT_SCRIPTS_XML = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.xml")).ReadToEnd();
		private readonly static string USER_FILE = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Visual NHibernate" + Path.DirectorySeparatorChar + "Settings"), "Model Scripts.xml");

		internal static DateTime TimeOfLastScriptChange = DateTime.Now;

		private static string _EntityNamingScript;
		private static string _PropertyNamingScript;
		public static string OriginalEntityNamingScript { get; set; }
		public static string OriginalPropertyNamingScript { get; set; }


		public static string EntityNamingScript
		{
			get { return _EntityNamingScript; }
			set
			{
				if (_EntityNamingScript != value)
				{
					_EntityNamingScript = value;
					TimeOfLastScriptChange = DateTime.Now;
				}
			}
		}

		public static string PropertyNamingScript
		{
			get { return _PropertyNamingScript; }
			set
			{
				if (_PropertyNamingScript != value)
				{
					_PropertyNamingScript = value;
					TimeOfLastScriptChange = DateTime.Now;
				}
			}
		}

		public static void LoadSettings()
		{
			// Load the default settings
			LoadSettings(DEFAULT_SCRIPTS_XML);
			// Load user-customised settings

			if (File.Exists(USER_FILE))
				LoadSettings(File.ReadAllText(USER_FILE));
		}

		private static void LoadSettings(string xml)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			string value;

			if (EntityNamingScript == null)
			{
				value = ReadScripts(doc, "entity-name");

				if (value != null)
				{
					EntityNamingScript = value;
					OriginalEntityNamingScript = value;
				}
			}
			else
			{
				value = ReadScripts(doc, "entity-name");

				if (value != null)
					EntityNamingScript = ReadScripts(doc, "entity-name");
			}
			if (PropertyNamingScript == null)
			{
				value = ReadScripts(doc, "property-name");

				if (value != null)
				{
					PropertyNamingScript = value;
					OriginalPropertyNamingScript = value;
				}
			}
			else
			{
				value = ReadScripts(doc, "property-name");

				if (value != null)
					PropertyNamingScript = ReadScripts(doc, "property-name");
			}
		}

		private static string ReadScripts(XmlDocument doc, string elementName)
		{
			XmlNode node = doc.SelectSingleNode(string.Format(@"model-scripts/{0}", elementName));

			if (node != null)
				return node.InnerText;

			return null;
		}

		/// <summary>
		/// Save the customised settings
		/// </summary>
		public static void SaveSettings()
		{
			bool changesExist = EntityNamingScript != OriginalEntityNamingScript ||
				PropertyNamingScript != OriginalPropertyNamingScript;

			if (changesExist)
			{
				XmlDocument doc = new XmlDocument();

				XmlNode root = doc.CreateElement("model-scripts");
				doc.AppendChild(root);

				if (EntityNamingScript != OriginalEntityNamingScript)
					root.AppendChild(CreateScriptNode(doc, "entity-name", Slyce.Common.Utility.StandardizeLineBreaks(EntityNamingScript, Slyce.Common.Utility.LineBreaks.Windows)));

				if (PropertyNamingScript != OriginalPropertyNamingScript)
					root.AppendChild(CreateScriptNode(doc, "property-name", Slyce.Common.Utility.StandardizeLineBreaks(PropertyNamingScript, Slyce.Common.Utility.LineBreaks.Windows)));

				string dir = Path.GetDirectoryName(USER_FILE);

				if (!Directory.Exists(dir))
					Directory.CreateDirectory(dir);

				doc.Save(USER_FILE);
			}
		}

		private static XmlNode CreateScriptNode(XmlDocument doc, string name, string script)
		{
			XmlNode node = doc.CreateElement(name);
			node.InnerText = script;
			return node;
		}

	}
}
