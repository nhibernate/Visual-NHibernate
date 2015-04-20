using System.Collections.Generic;
// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.BLL
{
    public abstract class ScriptBLL
    {
    	//private List<string> _scriptObjectPrefixes;
        private readonly List<string> _ErrorMessages = new List<string>();

    	protected ScriptBLL()
        {
        }

    	protected ScriptBLL(DatabaseTypes dalAssemblyName, ConnectionStringHelper connectionString)//, List<string> scriptObjectPrefixes)
        {
            DalAssemblyName = dalAssemblyName;
            ConnectionString = connectionString;
        }

    	protected ConnectionStringHelper ConnectionString { get; set; }

    	protected DatabaseTypes DalAssemblyName { get; set; }

    	public List<string> ErrorMessages
        {
            get { return _ErrorMessages; }
        }

        public static ScriptObject[] GetEnabledScriptObjects(Model.Table[] tables, Model.View[] views, Model.StoredProcedure[] storedProcedures)
        {
            ScriptObject[] scriptObjects = new ScriptObject[tables.Length + views.Length + storedProcedures.Length];
            tables.CopyTo(scriptObjects, 0);
            views.CopyTo(scriptObjects, tables.Length);
            storedProcedures.CopyTo(scriptObjects, tables.Length + views.Length);

            ScriptObject[] enabledScriptObjects = GetEnabledScriptObjects(scriptObjects);
            return enabledScriptObjects;
        }

        public static ScriptObject[] GetEnabledScriptObjects(ScriptObject[] scriptObjects)
        {
            int total = 0;
            int index = 0;

            foreach (Model.ScriptObject obj in scriptObjects)
            {
                if (obj.Enabled) { total++; }
            }
            ScriptObject[] enabledScriptObjects = new ScriptObject[total];

            foreach (ScriptObject obj in scriptObjects)
            {
                if (obj.Enabled)
                {
                    enabledScriptObjects[index++] = obj;
                }
            }
            return enabledScriptObjects;
        }

        public static ScriptObject[] GetUserDefinedScriptObjects(ScriptObject[] scriptObjects)
        {
            int total = 0;
            int index = 0;

            foreach (Model.ScriptObject obj in scriptObjects)
            {
                if (obj.IsUserDefined) { total++; }
            }
            ScriptObject[] userDefinedScriptObjects = new ScriptObject[total];

            foreach (ScriptObject obj in scriptObjects)
            {
                if (obj.IsUserDefined)
                {
                    userDefinedScriptObjects[index++] = obj;
                }
            }
            return userDefinedScriptObjects;
        }

        public static Column[] GetEnabledColumns(Column[] columns)
        {
            List<Column> enabledColumns = new List<Column>();
            foreach (Column column in columns)
            {
                if (column.Enabled)
                {
                    enabledColumns.Add(column);
                }
            }

            return enabledColumns.ToArray();
        }

        public static Column[] GetUserDefinedColumns(Column[] columns)
        {
            List<Column> userDefinedColumns = new List<Column>();
            foreach (Column column in columns)
            {
                if (column.IsUserDefined)
                {
                    userDefinedColumns.Add(column);
                }
            }

            return userDefinedColumns.ToArray();
        }

        public static Relationship[] GetEnabledRelationships(Relationship[] relationships)
        {
            List<Relationship> enabledRelationships = new List<Relationship>();
            foreach (Relationship relationship in relationships)
            {
                if (relationship.Enabled)
                {
                    enabledRelationships.Add(relationship);
                }
            }

            return enabledRelationships.ToArray();
        }

        public static Relationship[] GetUserDefinedRelationships(Relationship[] relationships)
        {
            List<Relationship> userDefinedRelationships = new List<Relationship>();
            foreach (Relationship relationship in relationships)
            {
                if (relationship.IsUserDefined)
                {
                    userDefinedRelationships.Add(relationship);
                }
            }

            return userDefinedRelationships.ToArray();
        }

        public static Filter[] GetEnabledFilters(Filter[] filters)
        {
            List<Filter> enabledFilters = new List<Filter>();
            foreach (Filter filter in filters)
            {
                if (filter.Enabled)
                {
                    enabledFilters.Add(filter);
                }
            }

            return enabledFilters.ToArray();
        }

        public static Filter[] GetUserDefinedFilters(Filter[] filters)
        {
            List<Filter> userDefinedFilters = new List<Filter>();
            foreach (Filter filter in filters)
            {
                if (filter.IsUserDefined)
                {
                    userDefinedFilters.Add(filter);
                }
            }

            return userDefinedFilters.ToArray();
        }

        public static Key[] GetEnabledKeys(Key[] keys)
        {
            List<Key> enabledKeys = new List<Key>();
            foreach (Key key in keys)
            {
                if (key.Enabled)
                {
                    enabledKeys.Add(key);
                }
            }

            return enabledKeys.ToArray();
        }

        public static Key[] GetUserDefinedKeys(Key[] keys)
        {
            List<Key> userDefinedKeys = new List<Key>();
            foreach (Key key in keys)
            {
                if (key.IsUserDefined)
                {
                    userDefinedKeys.Add(key);
                }
            }

            return userDefinedKeys.ToArray();
        }

        public static Index[] GetEnabledIndexs(Index[] indexes)
        {
            List<Index> enabledIndexes = new List<Index>();
            foreach (Index index in indexes)
            {
                if (index.Enabled)
                {
                    enabledIndexes.Add(index);
                }
            }

            return enabledIndexes.ToArray();
        }

        public static Index[] GetUserDefinedIndexes(Index[] indexes)
        {
            List<Index> userDefinedIndexes = new List<Index>();

            foreach (Index index in indexes)
            {
                if (index.IsUserDefined)
                {
                    userDefinedIndexes.Add(index);
                }
            }
            return userDefinedIndexes.ToArray();
        }
    }
}