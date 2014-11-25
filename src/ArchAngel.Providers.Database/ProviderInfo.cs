using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Controls.ContentItems;

namespace ArchAngel.Providers.Database
{
    [DotfuscatorDoNotRename]
    public class ProviderInfo : Interfaces.ProviderInfo
    {
        internal enum SettingNames
        {
            AssociationActions,
            AllowAssociations
        }
        [DotfuscatorDoNotRename]
        public ProviderInfo()
        {
            Name = "Database Provider";
            FileName = "provider_database.settings";
            Description = @"Database Provider that ships with ArchAngel as a standard provider.";

            FillSettings();
        }

    	public override void CreateScreens()
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA) return;

            Screens = new ContentItem[2];
            Screens[0] = new Controls.ContentItems.SetupDatabase();
            Screens[1] = new Controls.ContentItems.EditModel();
        }

        private static void FillSettings()
        {
            Settings.Clear();
            Settings.Add(SettingNames.AssociationActions.ToString(), new Interfaces.Setting(typeof(Enum), "Available Association Actions", new List<string>(new string[] { "Get", "Insert", "Update", "Delete" })));
            //#if DEBUG
            Settings.Add(SettingNames.AllowAssociations.ToString(), new Interfaces.Setting(typeof(bool), "Allow user to create Associations", true));
            //#else
            //            Settings.Add(SettingNames.AllowAssociations.ToString(), new ArchAngel.Interfaces.Setting(typeof(bool), "Allow user to create Associations", false));
            //#endif
        }

        [DotfuscatorDoNotRename]
        private static BLL.Database _TheBllDatabase;
        [DotfuscatorDoNotRename]
        internal static List<string> _TablePrefixes = new List<string>();
        [DotfuscatorDoNotRename]
        internal static List<string> _ViewPrefixes = new List<string>();
        [DotfuscatorDoNotRename]
        internal static List<string> _StoredProcedurePrefixes = new List<string>();
        [DotfuscatorDoNotRename]
        private static bool _IsLoaded;

        [DotfuscatorDoNotRename]
        public static BLL.Database TheBllDatabase
        {
            get { return _TheBllDatabase; }
            set { _TheBllDatabase = value; }
        }

        [DotfuscatorDoNotRename]
        public override IEnumerable<object> RootPreviewObjects
        {
            get
            {
				if (TheBllDatabase == null)
					return new object[0];
                return TheBllDatabase.Databases;
            }
        }
        [DotfuscatorDoNotRename]
        public static bool IsLoaded
        {
            get { return _IsLoaded; }
            set { _IsLoaded = value; }
        }

        [DotfuscatorDoNotRename]
        internal static List<string> TablePrefixes
        {
            get { return _TablePrefixes; }
            set
            {
                _TablePrefixes = value;
                Model.Table.TablePrefixes = value;
            }
        }

        [DotfuscatorDoNotRename]
        internal static List<string> ViewPrefixes
        {
            get { return _ViewPrefixes; }
            set
            {
                _ViewPrefixes = value;
                Model.View.ViewPrefixes = value;
            }
        }

        [DotfuscatorDoNotRename]
        internal static List<string> StoredProcedurePrefixes
        {
            get { return _StoredProcedurePrefixes; }
            set
            {
                _StoredProcedurePrefixes = value;
                Model.StoredProcedure.StoredProcedurePrefixes = value;
            }
        }

        /// <summary>
        /// Saves the provider's state to a single file or multiple files eg: xml, binary serialization etc.
        /// </summary>
        /// <param name="folder"></param>
        public override void Save(string folder)
        {
            string databaseFile = Path.Combine(folder, "provider_database.settings");
            string settingsFile = Path.Combine(folder, "provider_database_settings.xml");
            XmlDocument doc = StoreXmlSettings();
            doc.Save(settingsFile);

            if (TheBllDatabase != null)
            {
                TheBllDatabase.Save(databaseFile);
            }
        }

        /// <summary>
        /// Reads the providers state from the providers saved files.
        /// </summary>
        /// <param name="folder">The folder which contains all of your files and only your files.</param>
        public override void Open(string folder)
        {
            Clear();

            string databaseFile = Path.Combine(folder, "provider_database.settings");
            string settingsFile = Path.Combine(folder, "provider_database_settings.xml");
            //string zipFile = System.IO.Path.Combine(folder, "provider_database_data.zip");

            //if (System.IO.File.Exists(zipFile))
            //{
            //    Slyce.Common.Utility.UnzipFile(zipFile, tempFolder);
            //    //provider.Open(tempFolder);
            //}
            //else if (System.IO.File.Exists(databaseFile))
            //{
            //    System.IO.File.Copy(databaseFile, System.IO.Path.Combine(tempFolder, System.IO.Path.GetFileName(dbFile)));
            //    //provider.Open(tempFolder);
            //}
            if (File.Exists(databaseFile))
            {
                TheBllDatabase = new BLL.Database(databaseFile);
            }
            if (File.Exists(settingsFile))
            {
                GetXmlSettings(settingsFile);
            }
            IsLoaded = true;

            if (Screens != null && Screens.Length >= 2)
            {
                ((Controls.ContentItems.SetupDatabase)Screens[0]).Populate();
                ((Controls.ContentItems.EditModel)Screens[1]).Populate();
            }
        }

        public override void PerformPreAnalysisActions()
        {
			if(Screens != null && Screens.Length >= 2)
                ((Controls.ContentItems.EditModel)Screens[1]).SetPerObjectUserOptions();
        }

        /// <summary>
        /// Clears stored information and removes the provider screens.
        /// </summary>
        public override void Clear()
        {
            if (TheBllDatabase != null)
            {
                TheBllDatabase = null;
            }
            TheBllDatabase = new BLL.Database();
            TablePrefixes.Clear();
            ViewPrefixes.Clear();
            StoredProcedurePrefixes.Clear();

            foreach (ContentItem contentIten in Screens)
            {
                contentIten.Clear();
            }
            if (Screens != null && Screens.Length >= 2)
            {
                Screens[0] = null;
                Screens[1] = null;
                Screens = null;
            }
        }

        private static void GetXmlSettings(string xmlFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);

            string[] prefixes = doc.SelectSingleNode("database_provider_settings/table_prefixes").InnerText.Split('|');

            foreach (string prefix in prefixes)
            {
                if (!string.IsNullOrEmpty(prefix))
                {
                    TablePrefixes.Add(prefix);
                }
            }
            prefixes = doc.SelectSingleNode("database_provider_settings/view_prefixes").InnerText.Split('|');

            foreach (string prefix in prefixes)
            {
                if (!string.IsNullOrEmpty(prefix))
                {
                    ViewPrefixes.Add(prefix);
                }
            }
            prefixes = doc.SelectSingleNode("database_provider_settings/stored_procedure_prefixes").InnerText.Split('|');

            foreach (string prefix in prefixes)
            {
                if (!string.IsNullOrEmpty(prefix))
                {
                    StoredProcedurePrefixes.Add(prefix);
                }
            }
            Model.Table.TablePrefixes = TablePrefixes;
            Model.View.ViewPrefixes = ViewPrefixes;
            Model.StoredProcedure.StoredProcedurePrefixes = StoredProcedurePrefixes;
        }

        private static XmlDocument StoreXmlSettings()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode rootNode = doc.CreateElement("database_provider_settings");
            doc.AppendChild(rootNode);

            XmlNode prefixesNode = doc.CreateElement("table_prefixes");
            rootNode.AppendChild(prefixesNode);
            prefixesNode.InnerText = GetCSVvalues(TablePrefixes, '|');

            prefixesNode = doc.CreateElement("view_prefixes");
            rootNode.AppendChild(prefixesNode);
            prefixesNode.InnerText = GetCSVvalues(ViewPrefixes, '|');

            prefixesNode = doc.CreateElement("stored_procedure_prefixes");
            rootNode.AppendChild(prefixesNode);
            prefixesNode.InnerText = GetCSVvalues(StoredProcedurePrefixes, '|');

            return doc;
        }

        private static string GetCSVvalues(IList<string> list, char seperator)
        {
            StringBuilder sb = new StringBuilder(list.Count * 5);

            for (int i = 0; i < list.Count; i++)
            {
                string prefix = list[i];

                if (i > 0) { sb.Append(seperator); }

                sb.Append(prefix);
            }
            return sb.ToString();
        }

        public override bool IsValid(out string failReason)
        {
            if (TheBllDatabase == null)
            {
                failReason = "No database has been selected.";
                return false;
            }
            StringBuilder sb = new StringBuilder(100);
            bool isInvalid = false;

            foreach (Model.Database database in TheBllDatabase.Databases)
            {
                string tempString;

                if (!database.IsValid(true, out tempString))
                {
                    isInvalid = true;
                    sb.AppendLine(tempString);
                }
            }
            failReason = sb.ToString();
            return !isInvalid;
        }

        public override IEnumerable<Interfaces.IScriptBaseObject> GetAllObjectsOfType(string typeName)
        {
            Dictionary<Model.Database, bool> originalSnapshotModes = new Dictionary<Model.Database, bool>();

            if (TheBllDatabase != null)
            {
                foreach (Model.Database database in TheBllDatabase.Databases)
                {
                    originalSnapshotModes.Add(database, database.SnapshotMode);
                    database.SnapshotMode = true;
                }
            }
            typeName = "_" + typeName;
            int total = 0;
            //Dictionary<ArchAngel.Providers.Database.Model.Database, bool> originalSnapshotModes = new Dictionary<ArchAngel.Providers.Database.Model.Database, bool>();

            try
            {
                //if (TheBllDatabase != null)
                //{
                //    foreach (ArchAngel.Providers.Database.Model.Database database in TheBllDatabase.Databases)
                //    {
                //        originalSnapshotModes.Add(database, database.SnapshotMode);
                //        database.SnapshotMode = true;
                //    }
                //}
                switch (typeName)
                {
                    case "_ArchAngel.Providers.Database.Model.Database[]":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.Database), 0);
                        }
                        return TheBllDatabase.Databases;
                    case "_ArchAngel.Providers.Database.Model.Database":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.Database), 0);
                        }
                        return TheBllDatabase.Databases;
                    case "_ArchAngel.Providers.Database.Model.ScriptObject":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.ScriptObject), 0);
                        }
                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            foreach (Model.ScriptObject obj in database.ScriptObjects)
                            {
                                total++;
                            }
                        }
                        List<Model.ScriptObject> scriptObjects = new List<Model.ScriptObject>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            foreach (Model.ScriptObject obj in database.ScriptObjects)
                            {
                                scriptObjects.Add(obj);
                            }
                        }
                        return scriptObjects.ToArray();

                    case "_ArchAngel.Providers.Database.Model.Table":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.Table), 0);
                        }
                        List<Model.Table> tables = new List<Model.Table>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            tables.AddRange(database.Tables);
                        }
                        return tables.ToArray();

                    case "_ArchAngel.Providers.Database.Model.View":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.View), 0);
                        }
                        List<Model.View> views = new List<Model.View>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            views.AddRange(database.Views);
                        }
                        return views.ToArray();

                    case "_ArchAngel.Providers.Database.Model.StoredProcedure":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.StoredProcedure), 0);
                        }
                        List<Model.StoredProcedure> storedProcs = new List<Model.StoredProcedure>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            storedProcs.AddRange(database.StoredProcedures);
                        }
                        return storedProcs.ToArray();

                    case "_ArchAngel.Providers.Database.Model.Column":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.Column), 0);
                        }
                        List<Model.Column> columns = new List<Model.Column>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            foreach (Model.ScriptObject scriptObject in database.ScriptObjects)
                            {
                                columns.AddRange(scriptObject.Columns);
                            }
                        }
                        return columns.ToArray();

                    case "_ArchAngel.Providers.Database.Model.MapColumn":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.MapColumn), 0);
                        }
                        List<Model.MapColumn> mapColumns = new List<Model.MapColumn>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            foreach (Model.Table table in database.Tables)
                            {
                                mapColumns.AddRange(table.MapColumns);
                            }
                        }
                        return mapColumns.ToArray();

                    case "_ArchAngel.Providers.Database.Model.OneToOneRelationship":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.OneToOneRelationship), 0);
                        }
                        List<Model.OneToOneRelationship> oneToOneRelationships = new List<Model.OneToOneRelationship>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            foreach (Model.ScriptObject scriptObject in database.ScriptObjects)
                            {
                                oneToOneRelationships.AddRange(scriptObject.OneToOneRelationships);
                            }
                        }
                        return oneToOneRelationships.ToArray();

                    case "_ArchAngel.Providers.Database.Model.OneToManyRelationship":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.OneToManyRelationship), 0);
                        }
                        List<Model.OneToManyRelationship> oneToManyRelationships = new List<Model.OneToManyRelationship>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            foreach (Model.ScriptObject scriptObject in database.ScriptObjects)
                            {
                                oneToManyRelationships.AddRange(scriptObject.OneToManyRelationships);
                            }
                        }
                        return oneToManyRelationships.ToArray();

                    case "_ArchAngel.Providers.Database.Model.ManyToOneRelationship":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.ManyToOneRelationship), 0);
                        }
                        List<Model.ManyToOneRelationship> manyToOneRelationships = new List<Model.ManyToOneRelationship>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            foreach (Model.ScriptObject scriptObject in database.ScriptObjects)
                            {
                                manyToOneRelationships.AddRange(scriptObject.ManyToOneRelationships);
                            }
                        }
                        return manyToOneRelationships.ToArray();

                    case "_ArchAngel.Providers.Database.Model.ManyToManyRelationship":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.ManyToManyRelationship), 0);
                        }
                        List<Model.ManyToManyRelationship> manyToManyRelationships = new List<Model.ManyToManyRelationship>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            foreach (Model.ScriptObject scriptObject in database.ScriptObjects)
                            {
                                manyToManyRelationships.AddRange(scriptObject.ManyToManyRelationships);
                            }
                        }
                        return manyToManyRelationships.ToArray();
                    case "_ArchAngel.Providers.Database.Model.Relationship":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.Relationship), 0);
                        }
                        List<Model.Relationship> relationships = new List<Model.Relationship>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            foreach (Model.ScriptObject scriptObject in database.ScriptObjects)
                            {
                                relationships.AddRange(scriptObject.Relationships);
                            }
                        }
                        return relationships.ToArray();
                    case "_ArchAngel.Providers.Database.Model.Filter":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.Filter), 0);
                        }
                        List<Model.Filter> filters = new List<Model.Filter>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            foreach (Model.ScriptObject scriptObject in database.ScriptObjects)
                            {
                                filters.AddRange(scriptObject.Filters);
                            }
                        }
                        return filters.ToArray();
                    case "_ArchAngel.Providers.Database.Model.Key":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.Key), 0);
                        }
                        List<Model.Key> keys = new List<Model.Key>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            foreach (Model.Table table in database.Tables)
                            {
                                keys.AddRange(table.Keys);
                            }
                        }
                        return keys.ToArray();
                    case "_ArchAngel.Providers.Database.Model.Index":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.Index), 0);
                        }
                        List<Model.Index> indexes = new List<Model.Index>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            foreach (Model.Table table in database.Tables)
                            {
                                indexes.AddRange(table.Indexes);
                            }
                        }
                        return indexes.ToArray();
                    case "_ArchAngel.Providers.Database.Model.Lookup":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.Lookup), 0);
                        }
                        List<Model.Lookup> lookups = new List<Model.Lookup>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            foreach (Model.Lookup lookup in database.Lookups)
                            {
                                lookups.Add(lookup);
                            }
                        }
                        return lookups.ToArray();
                    case "_ArchAngel.Providers.Database.Model.LookupValue":
                        if (TheBllDatabase == null)
                        {
                            return (Interfaces.IScriptBaseObject[])Array.CreateInstance(typeof(Model.LookupValue), 0);
                        }
                        List<Model.LookupValue> lookupValues2 = new List<Model.LookupValue>();

                        foreach (Model.Database database in TheBllDatabase.Databases)
                        {
                            foreach (Model.Lookup lookup in database.Lookups)
                            {
                                lookupValues2.AddRange(lookup.LookupValues);
                            }
                        }
                        return lookupValues2.ToArray();
                    default:
                        throw new Exception("IteratorName not handled: " + typeName);
                }
            }
            finally
            {
                if (TheBllDatabase != null)
                {
                    foreach (Model.Database database in TheBllDatabase.Databases)
                    {
                        //originalSnapshotModes.Add(database, database.SnapshotMode);
                        database.SnapshotMode = originalSnapshotModes[database];
                    }
                }
            }
            //finally
            //{
            //    if (TheBllDatabase != null)
            //    {
            //        foreach (ArchAngel.Providers.Database.Model.Database database in TheBllDatabase.Databases)
            //        {
            //            database.SnapshotMode = originalSnapshotModes[database];
            //        }
            //    }
            //}
        }

        public override IEnumerable<Interfaces.IScriptBaseObject> GetAllObjectsOfType(string typeName, Interfaces.IScriptBaseObject rootObject)
        {
            if (rootObject == null)
            {
                return GetAllObjectsOfType(typeName);
            }
            if (typeof(Model.Table).IsInstanceOfType(rootObject))
            {
                switch (typeName)
                {
                    case "ArchAngel.Providers.Database.Model.Column":
                        return ((Model.Table)rootObject).Columns;
                    case "ArchAngel.Providers.Database.Model.Filter":
                        return ((Model.Table)rootObject).Filters;
                    case "ArchAngel.Providers.Database.Model.Index":
                        return ((Model.Table)rootObject).Indexes;
                    case "ArchAngel.Providers.Database.Model.Key":
                        return ((Model.Table)rootObject).Keys;
                    case "ArchAngel.Providers.Database.Model.OneToManyRelationship":
                        return ((Model.Table)rootObject).OneToManyRelationships;
                    case "ArchAngel.Providers.Database.Model.OneToOneRelationship":
                        return ((Model.Table)rootObject).OneToOneRelationships;
                    case "ArchAngel.Providers.Database.Model.Relationship":
                        return ((Model.Table)rootObject).Relationships;
                }
            }
            if (typeof(Model.StoredProcedure).IsInstanceOfType(rootObject))
            {
                switch (typeName)
                {
                    case "ArchAngel.Providers.Database.Model.Column":
                        return ((Model.StoredProcedure)rootObject).Columns;
                    case "ArchAngel.Providers.Database.Model.Filter":
                        return ((Model.StoredProcedure)rootObject).Filters;
                    case "ArchAngel.Providers.Database.Model.OneToManyRelationship":
                        return ((Model.StoredProcedure)rootObject).OneToManyRelationships;
                    case "ArchAngel.Providers.Database.Model.OneToOneRelationship":
                        return ((Model.StoredProcedure)rootObject).OneToOneRelationships;
                    case "ArchAngel.Providers.Database.Model.StoredProcedure+Parameter":
                        return ((Model.StoredProcedure)rootObject).Parameters;
                    case "ArchAngel.Providers.Database.Model.Relationship":
                        return ((Model.StoredProcedure)rootObject).Relationships;
                }
            }
            if (typeof(Model.View).IsInstanceOfType(rootObject))
            {
                switch (typeName)
                {
                    case "ArchAngel.Providers.Database.Model.Column":
                        return ((Model.View)rootObject).Columns;
                    case "ArchAngel.Providers.Database.Model.Filter":
                        return ((Model.View)rootObject).Filters;
                    case "ArchAngel.Providers.Database.Model.ManyToManyRelationship":
                        return ((Model.View)rootObject).ManyToManyRelationships;
                    case "ArchAngel.Providers.Database.Model.ManyToOneRelationship":
                        return ((Model.View)rootObject).ManyToOneRelationships;
                    case "ArchAngel.Providers.Database.Model.OneToManyRelationship":
                        return ((Model.View)rootObject).OneToManyRelationships;
                    case "ArchAngel.Providers.Database.Model.OneToOneRelationship":
                        return ((Model.View)rootObject).OneToOneRelationships;
                    case "ArchAngel.Providers.Database.Model.Relationship":
                        return ((Model.View)rootObject).Relationships;
                }
            }
            if (typeof(Model.Database).IsInstanceOfType(rootObject))
            {
                switch (typeName)
                {
                    case "ArchAngel.Providers.Database.Model.ScriptObject":
                        return ((Model.Database)rootObject).ScriptObjects;
                    case "ArchAngel.Providers.Database.Model.Table":
                        return ((Model.Database)rootObject).Tables;
                    case "ArchAngel.Providers.Database.Model.StoredProcedure":
                        return ((Model.Database)rootObject).StoredProcedures;
                    case "ArchAngel.Providers.Database.Model.View":
                        return ((Model.Database)rootObject).Views;
                }
            }
            throw new NotImplementedException(string.Format("This type of rootObject [{0}] and typeName [{1}] hasn't been catered for yet. Please contact support@slyce.com", rootObject.GetType().FullName, typeName));
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
