using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using ArchAngel.Interfaces;

namespace Demo.Providers.Test
{
    public class ProviderInfo : ArchAngel.Interfaces.ProviderInfo
    {
        internal static List<School> Schools = new List<School>();
    	private bool screensCreated = false;

        public ProviderInfo()
        {
            Name = "My Provider";
            Description = "Default provider description.";
        }

    	public override void CreateScreens()
    	{
			if (screensCreated) return;

			// Add the screens in the order they should appear in ArchAngel Workbench
			Screens = new ArchAngel.Interfaces.Controls.ContentItems.ContentItem[1];
			Screens[0] = new Screens.Screen1();
    		screensCreated = true;
    	}

		public override IEnumerable<object> RootPreviewObjects
        {
            get { return new object[0]; }
        }
        public override void Clear()
        {
            // Do nothing
        }

        /// <summary>
        /// Saves this provider's state to a single file or multiple files in the specified folder.
        /// All files in the folder will be embedded within the workbench project structure. You can save the provider's
        /// data in any format you like, such as xml, binary serialization etc. What files you create and
        /// what you put inside them is totally up to you.
        /// </summary>
        /// <param name="folder">Folder to save the files to.</param>
        public override void Save(string folder)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode schoolsNode = doc.CreateElement("schools");
            doc.AppendChild(schoolsNode);

            foreach (School school in Schools)
            {
                XmlNode schoolNode = doc.CreateElement("school");
                AddAttribute(schoolNode, "name", school.Name);
                schoolsNode.AppendChild(schoolNode);

                XmlNode pupilsNode = doc.CreateElement("pupils");
                schoolNode.AppendChild(pupilsNode);

                foreach (Pupil pupil in school.Pupils)
                {
                    XmlNode pupilNode = doc.CreateElement("pupil");
                    AddAttribute(pupilNode, "name", pupil.Name);
                    pupilsNode.AppendChild(pupilNode);
                }
            }
            string settingsFile = Path.Combine(folder, "settings.xml");
            doc.Save(settingsFile);
        }

        private static void AddAttribute(XmlNode node, string name, string value)
        {
            XmlAttribute att = node.OwnerDocument.CreateAttribute(name);
            att.Value = value;
            node.Attributes.Append(att);
        }

        /// <summary>
        /// Reads the page/object(s) state from file eg: xml, binary serialization etc
        /// </summary>
        /// <param name="folder">Folder to open files from.</param>
        public override void Open(string folder)
        {
            string settingsFile = Path.Combine(folder, "settings.xml");

            if (File.Exists(settingsFile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(settingsFile);

                XmlNodeList schoolNodes = doc.SelectNodes("schools/school");

                foreach (XmlNode schoolNode in schoolNodes)
                {
                    School school = new School();
                    school.Name = schoolNode.Attributes["name"].Value;

                    XmlNodeList pupilNodes = schoolNode.SelectNodes("pupils/pupil");

                    foreach (XmlNode pupilNode in pupilNodes)
                    {
                        Pupil pupil = new Pupil();
                        pupil.Name = pupilNode.Attributes["name"].Value;
                        school.Pupils.Add(pupil);
                    }
                    Schools.Add(school);
                }
                ((Screens.Screen1)Screens[0]).Populate();
            }
        }

        /// <summary>
        /// Perform any actions you need to just before analysis and generation begins.
        /// </summary>
        public override void PerformPreAnalysisActions()
        {
        }

        /// <summary>
        /// Returns whether this provider is in a valid state. This typically gets called just before generation.
        /// If false is returned, analysis and generation will not proceed.
        /// </summary>
        /// <param name="failReason">The reason for the invalid state.</param>
        /// <returns>True if the provider's state is valid, false otherwise.</returns>
        public override bool IsValid(out string failReason)
        {
            // TODO: Add code to determine whether this provider is in a valid state.
            failReason = "";
            return true;
        }

        /// <summary>
        /// Returns all objects of the specified type that currently exist in this provider. This typically gets called
        /// when analysis and generation begins and the objects get passed to the template to create the output files.
        /// </summary>
        /// <param name="typeName">Fully qualified name of the type of objects to return.</param>
        /// <returns>An arra of objects of the specified type.</returns>
        public override IEnumerable<ArchAngel.Interfaces.IScriptBaseObject> GetAllObjectsOfType(string typeName)
        {
            ArchAngel.Interfaces.IScriptBaseObject[] results = new ArchAngel.Interfaces.IScriptBaseObject[0];

            switch (typeName)
            {
                case "Demo.Providers.Test.School":
                    results = new ArchAngel.Interfaces.IScriptBaseObject[Schools.Count];

                    for (int i = 0; i < Schools.Count; i++)
                    {
                        School school = Schools[i];
                        results[i] = school;
                    }
                    break;
                case "Demo.Providers.Test.Pupil":
                    int total = 0;

                    foreach (School school in Schools)
                    {
                        total += school.Pupils.Count;
                    }
                    results = new ArchAngel.Interfaces.IScriptBaseObject[total];
                    int counter = 0;

                    foreach (School school in Schools)
                    {
                        foreach (Pupil pupil in school.Pupils)
                        {
                            results[counter] = pupil;
                            counter++;
                        }
                    }
                    break;
                default:
                    throw new NotImplementedException(string.Format("This type of object hasn't been handled yet: {0} in the Demo.Providers.Test assembly.", typeName));
            }
            return results;
        }

        /// <summary>
        /// Returns all objects of the specified type that are valid 'beneath' the supplied rootObject. This typically gets called
        /// when analysis and generation begins and the objects get passed to the template to create the output files.
        /// </summary>
        /// <param name="typeName">Fully qualified name of the type of objects to return.</param>
        /// <param name="rootObject">Object by which the results must get filtered.</param>
        /// <returns>An arra of objects of the specified type.</returns>
        public override  IEnumerable<ArchAngel.Interfaces.IScriptBaseObject> GetAllObjectsOfType(string typeName, ArchAngel.Interfaces.IScriptBaseObject rootObject)
        {
            if (rootObject == null)
            {
                return GetAllObjectsOfType(typeName);
            }
            ArchAngel.Interfaces.IScriptBaseObject[] results = new ArchAngel.Interfaces.IScriptBaseObject[0];
            string rootObjectTypeName = rootObject.GetType().FullName;

            switch (rootObjectTypeName)
            {
                case "Demo.Providers.Test.School":
                    switch (typeName)
                    {
                        case "Demo.Providers.Test.Pupil":
                            results = ((School)rootObject).Pupils.ToArray();
                            break;
                        default:
                            throw new NotImplementedException(string.Format("GetAllObjectsOfType does not yet cater for objects of type [{0} with with a rootObject of type [{1}].", typeName, rootObjectTypeName));
                    }
                    break;
                case "Demo.Providers.Test.Pupil":
                    throw new NotImplementedException(string.Format("GetAllObjectsOfType does not yet cater for objects of type [{0} with with a rootObject of type [{1}].", typeName, rootObjectTypeName));
                default:
                    throw new NotImplementedException(string.Format("This type of rootObject hasn't been handled yet: {0} in the Demo.Providers.Test assembly.", rootObjectTypeName));
            }
            return results;
        }

    	public override void LoadFromNewProjectInformation(INewProjectInformation info)
    	{
    	}

    	public override ValidationResult RunPreGenerationValidation()
    	{
    		return new ValidationResult();
    	}
    }
}
