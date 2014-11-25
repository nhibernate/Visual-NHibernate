using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Slyce.Common
{
    public class AASettingsProvider : SettingsProvider
    {
        private static readonly object _Lock = new object();
        private readonly string Filename;

        public AASettingsProvider()
        {
            Filename = GetSettingsFilename();
        }

        public static string GetSettingsFilename()
        {
            // Filename should be of the form %AppData%\ArchAngel\{AssemblyName}
            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ArchAngel");
            Assembly asm = Assembly.GetEntryAssembly();
            filename =
                asm == null ?
                    Path.Combine(filename, "Test Environment") :
                    Path.Combine(filename, asm.GetName().Name);

            filename = Path.Combine(filename, "configuration.xml");

            return filename;
        }

        public void Initialize()
        {
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            lock (_Lock)
            {
                SettingsPropertyValueCollection valueCollection = new SettingsPropertyValueCollection();

                XmlDocument doc = new XmlDocument();
                if (File.Exists(Filename))
                {
                    try
                    {
                        doc.LoadXml(File.ReadAllText(Filename));
                    }
                    catch (XmlException)
                    {
                        doc = new XmlDocument();
                    }
                    catch (IOException)
                    {
                        doc = new XmlDocument();
                    }
                }
                else
                {
                    // Create the directory and the file.
                    try
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(Filename));
                        File.Create(Filename).Close();
                    }
                    catch (IOException)
                    {
                        // Couldn't create the file. Ignore for now.
                    }
                }

                XmlNode root = SetupDocument(doc);

                XmlNode appRoot = root.SelectSingleNode("ApplicationSettings");
                XmlNode userRoot = root.SelectSingleNode("UserSettings");

                // Get the settings
                foreach (SettingsProperty setting in collection)
                {
                    XmlNode node = IsUserScoped(setting) ? userRoot.SelectSingleNode(setting.Name) : appRoot.SelectSingleNode(setting.Name);

                    // Add the property to the list.
                    SettingsPropertyValue value = new SettingsPropertyValue(setting);
                    value.IsDirty = false;
                    value.SerializedValue = node != null ? node.InnerText : setting.DefaultValue;
                    valueCollection.Add(value);
                }

                return valueCollection;
            }
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            lock (_Lock)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    if (File.Exists(Filename))
                    {
                        try
                        {
                            doc.LoadXml(File.ReadAllText(Filename));
                        }
                        catch (XmlException)
                        {
                            doc = new XmlDocument();
                        }
                        catch (IOException)
                        {
                            doc = new XmlDocument();
                        }
                    }

                    XmlNode root = SetupDocument(doc);

                    XmlNode appRoot = root.SelectSingleNode("ApplicationSettings");
                    XmlNode userRoot = root.SelectSingleNode("UserSettings");

                    // Write the settings
                    foreach (SettingsPropertyValue setting in collection)
                    {
                        XmlNode node = IsUserScoped(setting.Property) ? userRoot.SelectSingleNode(setting.Name) : appRoot.SelectSingleNode(setting.Name);

                        if (node == null)
                        {
                            node = doc.CreateElement(setting.Name);
                            if (IsUserScoped(setting.Property))
                                userRoot.AppendChild(node);
                            else
                                appRoot.AppendChild(node);
                        }

                        // Set the value of the property in the XmlDocument
                        node.InnerText = setting.SerializedValue == null ? "" : setting.SerializedValue.ToString();
                    }

                    // Save the settings file.
                    using (FileStream writer = File.Open(Filename, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        doc.Save(writer);
                    }
                }
                catch (XmlException) { }
                catch (IOException) { }
            }
        }

        private static XmlNode SetupDocument(XmlDocument doc)
        {
            XmlNode root = doc.SelectSingleNode("Settings");
            if (root == null)
            {
                root = doc.CreateElement("Settings");
                doc.AppendChild(root);
            }

            XmlNode appRoot = doc.SelectSingleNode("Settings/ApplicationSettings");
            if (appRoot == null)
            {
                appRoot = doc.CreateElement("ApplicationSettings");
                root.AppendChild(appRoot);
            }

            XmlNode userRoot = doc.SelectSingleNode("Settings/UserSettings");
            if (userRoot == null)
            {
                userRoot = doc.CreateElement("UserSettings");
                root.AppendChild(userRoot);
            }

            return root;
        }

        public override string ApplicationName
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Name;
            }
            set { }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(ApplicationName, config);
        }

        // Taken from the MSDN example code for implementing a Registry settings provider.
        // Helper method: walks the "attribute bag" for a given property
        // to determine if it is user-scoped or not.
        // Note that this provider does not enforce other rules, such as 
        //   - unknown attributes
        //   - improper attribute combinations (e.g. both user and app - this implementation
        //     would say true for user-scoped regardless of existence of app-scoped)
        private static bool IsUserScoped(SettingsProperty prop)
        {
            foreach (DictionaryEntry d in prop.Attributes)
            {
                Attribute a = (Attribute)d.Value;
                if (a.GetType() == typeof(UserScopedSettingAttribute))
                    return true;
            }
            return false;
        }
    }
}