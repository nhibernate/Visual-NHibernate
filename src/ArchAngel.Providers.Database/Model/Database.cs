using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Attributes;
using Slyce.Common;
using Utility = ArchAngel.Providers.Database.Helper.Utility;

namespace ArchAngel.Providers.Database.Model
{
    /// <summary>
    /// This represents a database in the object model. It usually corresponds to a real database, 
    /// however it can also be a virtual database created by the end-user in ArchAngel Workbench.
    /// </summary>
    [Serializable]
    [DotfuscatorDoNotRename]
    [ArchAngelEditor(true, true, "Name", new[] { typeof(Database[]), typeof(List<Database>) })]
    public class Database : ScriptBaseObject, ISerializable
    {
        //[DotfuscatorDoNotRename]
        //private const int SerializationVersion = 1;
        [DotfuscatorDoNotRename]
        public static bool IncludeSystemObjects;
        private readonly Compare AliasCompare = new Compare(new[] { "Alias" });
        [DotfuscatorDoNotRename]
        private BLL.ConnectionStringHelper _connectionString;
        [DotfuscatorDoNotRename]
        private DatabaseTypes _databaseType;
        [DotfuscatorDoNotRename]
        private string _name;
        [DotfuscatorDoNotRename]
        internal List<StoredProcedure> _storedProcedures = new List<StoredProcedure>();
        [DotfuscatorDoNotRename]
        internal List<Table> _tables = new List<Table>();
        [DotfuscatorDoNotRename]
        internal List<View> _views = new List<View>();
        [DotfuscatorDoNotRename]
        internal List<Lookup> _lookups = new List<Lookup>();

        private ScriptObject[] ScriptObjectsCached;

        #region Comparer

        /// <summary>
        /// Sorting Generic list based on field name
        /// </summary>
        public sealed class Compare : IComparer<ScriptObject>
        {
            private readonly IList<string> _fieldNames = new List<string>();

            public Compare(string[] fieldNames)
            {
                foreach (string fieldName in fieldNames)
                {
                    _fieldNames.Add(fieldName);
                }
            }

            #region IComparer<ScriptObject> Members

            int IComparer<ScriptObject>.Compare(ScriptObject x, ScriptObject y)
            {
                Type typeObj1 = null;
                Type typeObj2 = null;
                //System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-NZ");
                //System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CurrentCulture;
                int compare = 0;

                foreach (string fieldName in _fieldNames)
                {
                    switch (fieldName)
                    {
                        case "Alias":
                            compare = string.Compare(x.Alias, y.Alias, StringComparison.OrdinalIgnoreCase);
                            //compare = (new System.Collections.CaseInsensitiveComparer(culture)).Compare(x.Alias, y.Alias);
                            break;
                        case "AliasPlural":
                            //compare = (new System.Collections.CaseInsensitiveComparer(culture)).Compare(x.AliasPlural, y.AliasPlural);
                            compare = string.Compare(x.AliasPlural, y.AliasPlural, StringComparison.OrdinalIgnoreCase);
                            break;
                        case "Name":
                            compare = string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
                            //compare = (new System.Collections.CaseInsensitiveComparer(culture)).Compare(x.Name, y.Name);
                            break;
                        default:
                            if (typeObj1 == null)
                            {
                                typeObj1 = x.GetType();
                                typeObj2 = y.GetType();
                            }
                            compare = (new CaseInsensitiveComparer(CultureInfo.CurrentCulture)).Compare(
                                typeObj1.InvokeMember(fieldName, BindingFlags.GetProperty, null, x, null, CultureInfo.CurrentCulture),
                                typeObj2.InvokeMember(fieldName, BindingFlags.GetProperty, null, y, null, CultureInfo.CurrentCulture)
                                );
                            break;
                    }
                    if (compare != 0)
                    {
                        break;
                    }
                }
                return (compare);
            }

            #endregion
        }

        #endregion

        /// <summary>
        /// TODO: I don't think this should be exposed to the user???
        /// </summary>
        public Database()
        {
        }

        /// <summary>
        /// TODO: I don't think this should be exposed to the user???
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="databaseType"></param>
        public Database(string name, BLL.ConnectionStringHelper connectionString, DatabaseTypes databaseType)
        {
            _name = name;
            _connectionString = connectionString;
            _databaseType = databaseType;
        }

        /// <summary>
        /// TODO: I don't think this should be exposed to the user???
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="databaseType"></param>
        /// <param name="tables"></param>
        /// <param name="views"></param>
        /// <param name="storedProcedures"></param>
        public Database(string name, BLL.ConnectionStringHelper connectionString, DatabaseTypes databaseType, Table[] tables,
                        View[] views, StoredProcedure[] storedProcedures)
        {
            _name = name;
            _connectionString = connectionString;
            _databaseType = databaseType;

            if (tables != null)
            {
                _tables.AddRange(tables);

                foreach (Table table in tables)
                {
                    table.Database = this;
                }
            }
            if (views != null)
            {
                _views.AddRange(views);

                foreach (View view in views)
                {
                    view.Database = this;
                }
            }
            if (storedProcedures != null)
            {
                _storedProcedures.AddRange(storedProcedures);

                foreach (StoredProcedure storedProcedure in storedProcedures)
                {
                    storedProcedure.Database = this;
                }
            }
        }

        /// <exclude/>
        /// <summary>
        /// Deserialization Constructor. TODO: I don't think this should be exposed to the user???
        /// </summary>
        public Database(SerializationInfo serializationInfo, StreamingContext streamingContext) //: base(null)
        {
            if (SerializerHelper.UseFastSerialization)
            {
                using (
                    SerializationReader reader = new SerializationReader((byte[])serializationInfo.GetValue("d", typeof(byte[]))))
                {
                    _connectionString = (BLL.ConnectionStringHelper)reader.ReadObject();
                    _databaseType = (DatabaseTypes)reader.ReadObject();
                    _name = reader.ReadString();
                    _storedProcedures = (List<StoredProcedure>)reader.ReadObject();
                    _tables = (List<Table>)reader.ReadObject();
                    _views = (List<View>)reader.ReadObject();

                    foreach (StoredProcedure sp in _storedProcedures)
                    {
                        sp.Database = this;
                    }
                    foreach (Table table in _tables)
                    {
                        table.Database = this;
                    }
                    foreach (View view in _views)
                    {
                        view.Database = this;
                    }
                }
            }
            else
            {
                int version = 0;

                if (SerializationVersionExists)
                {
                    try
                    {
                        version = serializationInfo.GetInt32("SerializationVersion");
                    }
                    catch (SerializationException)
                    {
                        // ignore
                        SerializationVersionExists = false;
                    }
                }
                _connectionString =
                    (BLL.ConnectionStringHelper)serializationInfo.GetValue("ConnectionString", ModelTypes.ConnectionStringHelper);

                if (SerializerHelper.CachedDatabases != null)
                {
                    foreach (Database db in SerializerHelper.CachedDatabases)
                    {
                        if (_connectionString.IsTheSame(db.ConnectionString))
                        {
                            return;
                        }
                    }
                }
                _databaseType = (DatabaseTypes)serializationInfo.GetValue("DatabaseType", ModelTypes.DatabaseTypes);
                _name = serializationInfo.GetString("Name");
                _storedProcedures =
                    (List<StoredProcedure>)serializationInfo.GetValue("StoredProcedures", ModelTypes.StoredProcedureList);
                _tables = (List<Table>)serializationInfo.GetValue("Tables", ModelTypes.TableList);
                _views = (List<View>)serializationInfo.GetValue("Views", ModelTypes.ViewList);

                if (SerializerHelper.CachedDatabases != null)
                {
                    SerializerHelper.CachedDatabases.Add(this);
                }
                if (version >= 9)
                {
                    _lookups = (List<Lookup>)serializationInfo.GetValue("Lookups", ModelTypes.LookupList);

                    //foreach (Lookup lookup in _lookups)
                    //{
                    //    lookup.Database = this;
                    //}
                }
            }
        }

        public bool SnapshotMode { get; set; }

        /// <summary>
        /// Real name of the database.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Connectionstring for accessing this database. TODO: this should use the ConnectionString object we have for returning different kinds of connection strings.
        /// </summary>
        public BLL.ConnectionStringHelper ConnectionString
        {
            get { return _connectionString; }
            set
            {
                Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _connectionString, value);
                _connectionString = value;
            }
        }

        /// <summary>
        /// The type of database engine that this database uses.
        /// </summary>
        public DatabaseTypes DatabaseType
        {
            get { return _databaseType; }
            set
            {
                Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _databaseType, value);
                _databaseType = value;
            }
        }

        /// <summary>
        /// Collection of tables in the database.
        /// </summary>
        public Table[] Tables
        {
            get
            {
                if (SharedData.IsBusyGenerating)
                {
                    return EnabledTables;
                }
                return _tables.ToArray();
            }
        }

        /// <summary>
        /// Collection of tables in the database that the user has selected.
        /// </summary>
        public Table[] EnabledTables
        {
            get
            {
                List<Table> enabledTables = new List<Table>();

                foreach (Table table in _tables)
                {
                    if (table.Enabled)
                    {
                        enabledTables.Add(table);
                    }
                }
                return enabledTables.ToArray();
            }
        }

        /// <summary>
        /// Virtual tables that the user has defined.
        /// </summary>
        public Table[] UserDefinedTables
        {
            get
            {
                List<Table> userDefinedTables = new List<Table>();
                foreach (Table table in Tables)
                {
                    if (table.IsUserDefined)
                    {
                        userDefinedTables.Add(table);
                    }
                }
                return userDefinedTables.ToArray();
            }
        }

        /// <summary>
        /// Collection of Lookups in the database.
        /// </summary>
        public Lookup[] Lookups
        {
            get { return _lookups.ToArray(); }
        }

        /// <summary>
        /// Collection of views in the database.
        /// </summary>
        public View[] Views
        {
            get
            {
                if (SharedData.IsBusyGenerating)
                {
                    return EnabledViews;
                }
                return _views.ToArray();
            }
        }

        /// <summary>
        /// Collection of views in the database that the user has selected.
        /// </summary>
        public View[] EnabledViews
        {
            get
            {
                List<View> enabledViews = new List<View>();

                foreach (View view in _views)
                {
                    if (view.Enabled)
                    {
                        enabledViews.Add(view);
                    }
                }
                return enabledViews.ToArray();
            }
        }

        /// <summary>
        /// Virtual views that the user has defined.
        /// </summary>
        public View[] UserDefinedViews
        {
            get
            {
                List<View> userDefinedViews = new List<View>();
                foreach (View view in Views)
                {
                    if (view.IsUserDefined)
                    {
                        userDefinedViews.Add(view);
                    }
                }

                return userDefinedViews.ToArray();
            }
        }

        /// <summary>
        /// Collection of stored procedures in the database.
        /// </summary>
        public StoredProcedure[] StoredProcedures
        {
            get
            {
                if (SharedData.IsBusyGenerating)
                {
                    return EnabledStoredProcedures;
                }
                return _storedProcedures.ToArray();
            }
        }

        /// <summary>
        /// Collection of stored procedures in the database that the user has selected.
        /// </summary>
        public StoredProcedure[] EnabledStoredProcedures
        {
            get
            {
                List<StoredProcedure> enabledStoredProcedures = new List<StoredProcedure>();

                foreach (StoredProcedure storedProcedure in _storedProcedures)
                {
                    if (storedProcedure.Enabled)
                    {
                        enabledStoredProcedures.Add(storedProcedure);
                    }
                }
                return enabledStoredProcedures.ToArray();
            }
        }

        /// <summary>
        /// Virtual stored procedures that the user has defined.
        /// </summary>
        public StoredProcedure[] UserDefinedStoredProcedures
        {
            get
            {
                List<StoredProcedure> userDefinedStoredProcedures = new List<StoredProcedure>();
                foreach (StoredProcedure storedProcedure in StoredProcedures)
                {
                    if (storedProcedure.IsUserDefined)
                    {
                        userDefinedStoredProcedures.Add(storedProcedure);
                    }
                }

                return userDefinedStoredProcedures.ToArray();
            }
        }

        /// <summary>
        /// Collection of all tables, views and stored procedures in the database. ScriptObject is the base object of Table, View and StoredProcedure, so
        /// a ScriptObject can be casted to the relevant type if required. While generation is happening this
        /// returns EnabledScriptObjects. It returns all script objects at other times.
        /// </summary>
        public ScriptObject[] ScriptObjects
        {
            get
            {
                if (SharedData.IsBusyGenerating)
                {
                    return EnabledScriptObjects;
                }
                return AllScriptObjects;
            }
        }

        /// <summary>
        /// Forces the cached script objects (ScriptObjectsCached) to be invalidated, so that it gets refreshed
        /// the next time AllScriptObjects is accessed.
        /// </summary>
        internal void ResetAllScriptObjects()
        {
            ScriptObjectsCached = null;
        }

        /// <summary>
        /// This always returns all script objects (ie: both enabled and disabled).
        /// </summary>
        public ScriptObject[] AllScriptObjects
        {
            get
            {
                if (ScriptObjectsCached == null || !SnapshotMode)
                {
                    ScriptObjectsCached = new ScriptObject[Tables.Length + Views.Length + StoredProcedures.Length];
                    int index = 0;

                    foreach (Table table in Tables)
                    {
                        ScriptObjectsCached[index++] = table;
                    }
                    foreach (View view in Views)
                    {
                        ScriptObjectsCached[index++] = view;
                    }
                    foreach (StoredProcedure sp in StoredProcedures)
                    {
                        ScriptObjectsCached[index++] = sp;
                    }
                    Array.Sort(ScriptObjectsCached, AliasCompare);
                }
                return ScriptObjectsCached;
            }
        }

        /// <summary>
        /// Collection of all tables, views and stored procedures in the database that the user has selected. ScriptObject is the base object of Table, View and StoredProcedure, so
        /// a ScriptObject can be casted to the relevant type if required.
        /// </summary>
        public ScriptObject[] EnabledScriptObjects
        {
            get
            {
                int index = 0;
                int total = 0;

                foreach (Table table in Tables)
                {
                    if (table.Enabled)
                    {
                        total++;
                    }
                }
                foreach (View view in Views)
                {
                    if (view.Enabled)
                    {
                        total++;
                    }
                }
                foreach (StoredProcedure sp in StoredProcedures)
                {
                    if (sp.Enabled)
                    {
                        total++;
                    }
                }
                ScriptObject[] enabledScriptObjects = new ScriptObject[total];

                foreach (Table table in Tables)
                {
                    if (table.Enabled)
                    {
                        enabledScriptObjects[index++] = table;
                    }
                }
                foreach (View view in Views)
                {
                    if (view.Enabled)
                    {
                        enabledScriptObjects[index++] = view;
                    }
                }
                foreach (StoredProcedure sp in StoredProcedures)
                {
                    if (sp.Enabled)
                    {
                        enabledScriptObjects[index++] = sp;
                    }
                }
                Array.Sort(enabledScriptObjects, AliasCompare);
                return enabledScriptObjects;
            }
        }

        /// <summary>
        /// Collection of all virtual tables, views and stored procedures that the user has defined. ScriptObject is the base object of Table, View and StoredProcedure, so
        /// a ScriptObject can be casted to the relevant type if required.
        /// </summary>
        public ScriptObject[] UserDefinedScriptObjects
        {
            get
            {
                int index = 0;
                int total = 0;

                foreach (Table table in Tables)
                {
                    if (table.IsUserDefined)
                    {
                        total++;
                    }
                }
                foreach (View view in Views)
                {
                    if (view.IsUserDefined)
                    {
                        total++;
                    }
                }
                foreach (StoredProcedure sp in StoredProcedures)
                {
                    if (sp.IsUserDefined)
                    {
                        total++;
                    }
                }
                ScriptObject[] userDefinedScriptObjects = new ScriptObject[total];

                foreach (Table table in Tables)
                {
                    if (table.IsUserDefined)
                    {
                        userDefinedScriptObjects[index++] = table;
                    }
                }
                foreach (View view in Views)
                {
                    if (view.IsUserDefined)
                    {
                        userDefinedScriptObjects[index++] = view;
                    }
                }
                foreach (StoredProcedure sp in StoredProcedures)
                {
                    if (sp.IsUserDefined)
                    {
                        userDefinedScriptObjects[index++] = sp;
                    }
                }
                Array.Sort(userDefinedScriptObjects, AliasCompare);
                return userDefinedScriptObjects;
            }
        }

        #region ISerializable Members

        /// <exclude/>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
#if FAST_SERIALIZATION
			using (SerializationWriter writer = new SerializationWriter())
			{
				writer.WriteObject(_connectionString);
				writer.WriteObject(_databaseType);
				writer.Write(_name);
				writer.WriteObject(_storedProcedures);
				writer.WriteObject(_tables);
				writer.WriteObject(_views);
				info.AddValue("d", writer.ToArray());
			}
#else
            info.AddValue("SerializationVersion", Model.ScriptBase.SerializationVersion);
            info.AddValue("ConnectionString", _connectionString);
            info.AddValue("DatabaseType", _databaseType);
            info.AddValue("Name", _name);
            info.AddValue("StoredProcedures", _storedProcedures);
            info.AddValue("Tables", _tables);
            info.AddValue("Views", _views);
            info.AddValue("Lookups", _lookups);
#endif
        }

        #endregion

        public static Database LoadNewDatabase(
            string name,
            DatabaseTypes databaseType,
            BLL.ConnectionStringHelper connectionString)
        {
            return LoadNewDatabase(name, databaseType, connectionString, null, null, null, true, true, true);
        }

        public static Database LoadNewDatabase(
            string name,
            DatabaseTypes databaseType,
            BLL.ConnectionStringHelper connectionString,
            bool fetchTables,
            bool fetchViews,
            bool fetchStroredProcedures)
        {
            return LoadNewDatabase(name, databaseType, connectionString, null, null, null, fetchTables, fetchViews,
                                   fetchStroredProcedures);
        }

        /// <summary>
        /// Gets the database schema and creats an object model.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="databaseType"></param>
        /// <param name="connectionString"></param>
        /// <param name="tablePrefixes"></param>
        /// <param name="viewPrefixes"></param>
        /// <param name="storedProcedurePredixes"></param>
        /// <param name="fetchTables"></param>
        /// <param name="fetchViews"></param>
        /// <param name="fetchStroredProcedures"></param>
        /// <returns></returns>
        public static Database LoadNewDatabase(
            string name,
            DatabaseTypes databaseType,
            BLL.ConnectionStringHelper connectionString,
            List<string> tablePrefixes,
            List<string> viewPrefixes,
            List<string> storedProcedurePredixes,
            bool fetchTables,
            bool fetchViews,
            bool fetchStroredProcedures)
        {
            Utility.ResetAllConnections();

            Table.TablePrefixes = tablePrefixes ?? new List<string>();
            View.ViewPrefixes = viewPrefixes ?? new List<string>();
            StoredProcedure.StoredProcedurePrefixes = storedProcedurePredixes ?? new List<string>();

            BLL.Table bllTable;
            BLL.View bllView;
            BLL.StoredProcedure bllStoredProcedure;

            Table[] tables = null;
            View[] views = null;
            StoredProcedure[] storedProcedures = null;

            if (fetchTables)
            {
                bllTable = new BLL.Table(databaseType, connectionString);
                tables = bllTable.Tables;
            }
            if (fetchViews)
            {
                bllView = new BLL.View(databaseType, connectionString);
                views = bllView.Views;
            }
            if (fetchStroredProcedures)
            {
                bllStoredProcedure = new BLL.StoredProcedure(databaseType, connectionString);
                storedProcedures = bllStoredProcedure.StoredProcedures;
            }
            return new Database(name, connectionString, databaseType, tables, views, storedProcedures);
        }

        internal System.Data.DataTable RunQuerySQL(string sql)
        {
            IDAL.ITable table = DALFactory.DataAccess.CreateTable(this.DatabaseType, this.ConnectionString);
            return table.RunQueryDataTable(sql);
        }

        /// <summary>
        /// TODO: I don't think this should be exposed to the user???
        /// </summary>
        /// <param name="scriptObject"></param>
        public void AddScriptObject(ScriptObject scriptObject)
        {
            if (ModelTypes.Table.IsInstanceOfType(scriptObject))
            {
                _tables.Add((Table)scriptObject);
            }
            if (scriptObject.GetType() == typeof(View))
            {
                _views.Add((View)scriptObject);
            }
            if (scriptObject.GetType() == typeof(StoredProcedure))
            {
                _storedProcedures.Add((StoredProcedure)scriptObject);
            }
            scriptObject.Database = this;
        }

        public void AddLookup(Lookup lookup)
        {
            _lookups.Add(lookup);
            lookup.Database = this;
        }

        /// <summary>
        /// TODO: I don't think this should be exposed to the user???
        /// </summary>
        /// <param name="scriptObject"></param>
        public void RemoveScriptObject(ScriptObject scriptObject)
        {
            if (ModelTypes.Table.IsInstanceOfType(scriptObject))
            {
                _tables.Remove((Table)scriptObject);
            }
            if (scriptObject.GetType() == typeof(View))
            {
                _views.Remove((View)scriptObject);
            }
            if (scriptObject.GetType() == typeof(StoredProcedure))
            {
                _storedProcedures.Remove((StoredProcedure)scriptObject);
            }
        }

        /// <summary>
        /// TODO: I don't think this should be exposed to the user???
        /// </summary>
        /// <param name="lookup"></param>
        public void RemoveLookup(Lookup lookup)
        {
            // Remove from all SubscribingColumns first
            List<Column> subscribingColumns = lookup.GetSubscribingColumns();

            foreach (Column column in subscribingColumns)
            {
                column.Lookup = null;
            }
            _lookups.Remove(lookup);
        }

        public bool IsValid(bool deepCheck, out string failReason)
        {
            bool isValid = true;
            failReason = "";
            string tempFailReason;
            // refresh the cached objects
            ScriptObjectsCached = null;
            bool originalSnapshotMode = SnapshotMode;
            SnapshotMode = true;

            foreach (Table table in EnabledTables)
            {
                if (!table.IsValid(deepCheck, out tempFailReason))
                {
                    isValid = false;
                    failReason += string.Format("Table: {0}\n", tempFailReason);
                }
            }
            foreach (View view in EnabledViews)
            {
                if (!view.IsValid(deepCheck, out tempFailReason))
                {
                    isValid = false;
                    failReason += string.Format("View: {0}\n", tempFailReason);
                }
            }
            foreach (StoredProcedure storedProcedure in EnabledStoredProcedures)
            {
                if (!storedProcedure.IsValid(deepCheck, out tempFailReason))
                {
                    isValid = false;
                    failReason += string.Format("StoredProcedure: {0}\n", tempFailReason);
                }
            }
            SnapshotMode = originalSnapshotMode;
            return isValid;
        }
    }
}