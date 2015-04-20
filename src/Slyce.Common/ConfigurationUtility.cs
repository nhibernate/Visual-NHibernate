using System;
using System.Configuration;
using System.IO;
using System.Xml;

namespace Slyce.Common
{
	public class ConfigurationUtility
	{
		/// <summary>
		/// Clears the current application configuration (app.config) of all settings.
		/// </summary>
		public static void ClearConfiguration()
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			ClearConfiguration(config);
			//#if DEBUG
			//            SaveCurrentConfig(@"C:\xxx.xml");
			//#endif
		}

		/// <summary>
		/// Clears configuration (app.config) of all settings.
		/// </summary>
		/// <param name="config">Configuration to be cleared.</param>
		public static void ClearConfiguration(Configuration config)
		{
			for (int i = config.Sections.Count - 1; i >= 0; i--)
			{
				ConfigurationSection section = config.Sections[i];

				// Ensure that this section came from the file we provided and not elsewhere, such as machine.config 
				if (section.SectionInformation.IsDeclared || (section.ElementInformation.Source != null && section.ElementInformation.Source.Equals(config.FilePath)))
				{
					string parentSectionName = section.SectionInformation.GetParentSection().SectionInformation.SectionName;
					config.Sections.RemoveAt(i);
					config.Save(ConfigurationSaveMode.Full, false);
					ConfigurationManager.RefreshSection(parentSectionName);
				}
			}
			config.Save(ConfigurationSaveMode.Full, false);
		}

		/// <summary>
		/// Merge the contents of the source config file into the application's current config file.
		/// </summary>
		/// <param name="sourceFile"></param>
		public static void MergeAppConfigFiles(string sourceFile)
		{
			string inlinedSource = InlineExternalSections(sourceFile);

			//Load Source 
			ExeConfigurationFileMap map = new ExeConfigurationFileMap { ExeConfigFilename = inlinedSource };
			Configuration source = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

			//Load Target
			Configuration target = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			MergeAppConfigFiles(source, target);
		}

		public static void SaveCurrentConfig(string filename)
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			config.SaveAs(filename);
		}

		/// <summary>
		/// Merge the contents of the source config file into the destination config file.
		/// </summary>
		/// <param name="sourceFile"></param>
		/// <param name="targetFile"></param>
		public static void MergeAppConfigFiles(string sourceFile, string targetFile)
		{
			string inlinedSource = InlineExternalSections(sourceFile);
			string inlinedTarget = InlineExternalSections(targetFile);

			//Load Source 
			ExeConfigurationFileMap map = new ExeConfigurationFileMap { ExeConfigFilename = inlinedSource };
			Configuration source = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

			//Load Target 
			map.ExeConfigFilename = inlinedTarget;
			Configuration target = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
			MergeAppConfigFiles(source, target);
		}

		/// <summary>
		/// Moves the data from external referenced files into the specified file, making all sections local.
		/// </summary>
		/// <param name="configFile"></param>
		private static string InlineExternalSections(string configFile)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(configFile);
			bool updated = false;

			foreach (XmlNode node in doc.SelectNodes("//*[@configSource]"))
			{
				updated = true;
				string externalFile = Path.Combine(Path.GetDirectoryName(configFile), node.SelectSingleNode("@configSource").InnerXml);

				if (!File.Exists(externalFile))
				{
					System.Windows.Forms.MessageBox.Show(string.Format("Referenced config file file is missing: {0}\n\nIt was referenced from: {1}", externalFile, configFile), "Missing File", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
				}
				XmlDocument externalDoc = new XmlDocument();
				externalDoc.Load(externalFile);
				XmlNode extRootNode = externalDoc.SelectSingleNode("*");
				XmlNode parentNode = node.ParentNode;
				parentNode.ReplaceChild(doc.ImportNode(extRootNode, true), node);
			}
			foreach (XmlNode node in doc.SelectNodes("//appSettings[@file]"))
			{
				if (node.SelectSingleNode("@file").InnerXml.Trim().Length == 0)
				{
					continue;
				}
				updated = true;
				string externalFile = Path.Combine(Path.GetDirectoryName(configFile), node.SelectSingleNode("@file").InnerXml);

				if (!File.Exists(externalFile))
				{
					System.Windows.Forms.MessageBox.Show(string.Format("Referenced config file is missing: {0}\n\nIt was referenced from: {1}", externalFile, configFile), "Missing File", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
				}
				XmlDocument externalDoc = new XmlDocument();
				externalDoc.Load(externalFile);
				XmlNode extRootNode = externalDoc.SelectSingleNode("*");
				XmlNode parentNode = node.ParentNode;
				parentNode.ReplaceChild(doc.ImportNode(extRootNode, true), node);
			}
			if (updated)
			{
				string newPath = Path.GetTempFileName() + ".config";
				doc.Save(newPath);
				return newPath;
			}
			else
			{
				return configFile;
			}
		}

		/// <summary>
		/// Merge the source configuration into the target configuration.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="target"></param>
		public static void MergeAppConfigFiles(Configuration source, Configuration target)
		{
			foreach (ConfigurationSection section in source.Sections)
			{
				//We want to ensure that this guy came from the file we provided.. and not from say machine config 
				if (section.SectionInformation.IsDeclared || (section.ElementInformation.Source != null && section.ElementInformation.Source.Equals(source.FilePath)))
				{
					//Enumerator is on AppSettings. So we update the appSettings 
					if (section is AppSettingsSection)
					{
						foreach (KeyValueConfigurationElement element in source.AppSettings.Settings)
						{
							target.AppSettings.Settings.Remove(element.Key);
							//target.Save(ConfigurationSaveMode.Full, false);
							target.AppSettings.Settings.Add(element);
						}
					}
					//Enumerator is on a custom section 
					else
					{
						//Remove from target and add from source.  
						target.Sections.Remove(section.SectionInformation.SectionName);
						//Just paranoid. 
						target.Save(ConfigurationSaveMode.Full, false);
						//Using reflection to instantiate since no public ctor and the instance we hold is tied to "Source" 
						ConfigurationSection reflectedSection = (ConfigurationSection)Activator.CreateInstance(section.GetType());
						reflectedSection.SectionInformation.SetRawXml(section.SectionInformation.GetRawXml());
						reflectedSection.SectionInformation.Type = section.SectionInformation.Type;
						//Bug/Feature in framework prevents target.Sections.Add(section.SectionInformation.Name, Section); 
						target.Sections.Add(section.SectionInformation.Name, reflectedSection);
					}
					target.Save(ConfigurationSaveMode.Full, false);
					ConfigurationManager.RefreshSection(section.SectionInformation.SectionName);
				}
			}
			target.Save(ConfigurationSaveMode.Full, false);
			//#if DEBUG
			//            SaveCurrentConfig(@"C:\xxx.xml");
			//#endif
		}
	}
}
