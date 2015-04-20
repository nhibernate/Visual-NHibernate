using ArchAngel.Interfaces.ITemplate;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ArchAngel.Providers.Database.Model
{
    [Serializable]
    [Interfaces.Attributes.ArchAngelEditor(true, true, "Alias")]
    [DotfuscatorDoNotRename]
    public class ScriptObject : ScriptBase
	{
		#region Fields
		[DotfuscatorDoNotRename]
        protected Database _database;
        [DotfuscatorDoNotRename]
        internal List<Column> _columns = new List<Column>();
        [DotfuscatorDoNotRename]
        internal List<OneToOneRelationship> _oneToOneRelationships = new List<OneToOneRelationship>();
        [DotfuscatorDoNotRename]
        internal List<OneToManyRelationship> _oneToManyRelationships = new List<OneToManyRelationship>();
        [DotfuscatorDoNotRename]
        internal List<ManyToOneRelationship> _manyToOneRelationships = new List<ManyToOneRelationship>();
        [DotfuscatorDoNotRename]
        internal List<ManyToManyRelationship> _manyToManyRelationships = new List<ManyToManyRelationship>();
        [DotfuscatorDoNotRename]
        internal List<Filter> _filters = new List<Filter>();
        [DotfuscatorDoNotRename]
        protected string _Schema = "";
        [DotfuscatorDoNotRename]
        protected List<Association> _Associations = new List<Association>();
        [DotfuscatorDoNotRename]
        protected List<string> _Errors = new List<string>();
        protected bool _IsTable = false;
        protected bool _IsStoredProcedure = false;
        protected bool _IsView = false;
		protected SortedList<string, Column> _ColumnsSortedByName;
		protected SortedList<string, Column> _ColumnsSortedByUniqueId;
        #endregion

        #region Constructors
        public ScriptObject()
        {
        }

        public ScriptObject(string name, bool isUserDefined)
            : base(name, isUserDefined)
        {
            if (ModelTypes.ScriptObject.IsInstanceOfType(this))
            {
                ResetDefaults();
            }
        }

        /// <exclude/>
        public ScriptObject(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
			throw new NotImplementedException("This shouldn't get called.");
        }

        #endregion

        #region Properties
        public List<Association> Associations
        {
            get { return _Associations; }
            set { _Associations = value; }
        }

        public string Schema
        {
            get { return _Schema; }
            set
            {
                _Schema = value;
            }

        }

        /// <summary>
        /// Gets whether this SciptObject is a Table.
        /// </summary>
        public bool IsTable
        {
            get { return _IsTable; }
        }

        /// <summary>
        /// Gets whether this SciptObject is a StoredProcedure.
        /// </summary>
        public bool IsStoredProcedure
        {
            get { return _IsStoredProcedure; }
        }

        /// <summary>
        /// Gets whether this SciptObject is a View.
        /// </summary>
        public bool IsView
        {
            get { return _IsView; }
        }

        public new string Alias
        {
            get { return _alias; }
            set
            {
                _alias = value;
                AliasPlural = AliasPluralDefault(this);
            }

        }

        public List<string> Errors
        {
            get { return _Errors; }
            set { _Errors = value; }
        }

        public Database Database
        {
            get { return _database; }
            /*TODO: Make internal*/

            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _database, value);
                _database = value;
            }

        }

        /// <summary>
        /// Gets all Lookups for which this object is a BackingObject.
        /// </summary>
        public List<Lookup> AttachedLookups
        {
            get
            {
                List<Lookup> attachedLookups = new List<Lookup>();

                for (int i = 0; i < this.Database.Lookups.Length; i++)
                {
                    if (this.Database.Lookups[i].BackingObject == this)
                    {
                        attachedLookups.Add(this.Database.Lookups[i]);
                    }
                }
                return attachedLookups;
            }
        }

		/// <summary>
		/// Gets all Columns of the ScriptObject, including MapColumns.
		/// </summary>
        public Column[] Columns
        {
            get
            {
            	if (Interfaces.SharedData.IsBusyGenerating)
                {
                    return EnabledColumns;
                }
// ReSharper disable RedundantCast
            	return (Column[])_columns.ToArray();
// ReSharper restore RedundantCast
            }
        }

		/// <summary>
		/// Gets all Columns that have been selected (checked) by the user.
		/// </summary>
        public Column[] EnabledColumns
        {
            get
            {
                List<Column> enabledColumns = new List<Column>();

                foreach (Column column in _columns)
                {
                    if (column.Enabled)
                    {
                        enabledColumns.Add(column);
                    }
                }
                return enabledColumns.ToArray();
            }

        }

		/// <summary>
		/// Gets the subset of Columns that are MapColumns ie: they are from a joined entity.
		/// </summary>
        public MapColumn[] MapColumns
        {
            get
            {
                if (Interfaces.SharedData.IsBusyGenerating)
                {
                    return EnabledMapColumns;
                }
            	List<MapColumn> mapColumns = new List<MapColumn>();

            	foreach (Column column in _columns)
            	{
            		if (ModelTypes.MapColumn.IsInstanceOfType(column))
            		{
            			mapColumns.Add((MapColumn)column);
            		}
            	}
            	return mapColumns.ToArray();
            }
        }

		/// <summary>
		/// Gets all MapColumns that have been selected (checked) by the user.
		/// </summary>
        public MapColumn[] EnabledMapColumns
        {
            get
            {
                List<MapColumn> enabledMapColumns = new List<MapColumn>();

                foreach (Column column in _columns)
                {
                    if (column.Enabled && ModelTypes.MapColumn.IsInstanceOfType(column))
                    {
                        enabledMapColumns.Add((MapColumn)column);
                    }
                }
                return enabledMapColumns.ToArray();
            }
        }

        /// <summary>
        /// Gets the subset of Columns that are LookupColumns ie: they have a specific set of allowed values.
        /// </summary>
        public Column[] LookupColumns
        {
            get
            {
                if (Interfaces.SharedData.IsBusyGenerating)
                {
                    return EnabledLookupColumns;
                }
                List<Column> lookupColumns = new List<Column>();

                foreach (Column column in _columns)
                {
                    if (column.Lookup != null)
                    {
                        lookupColumns.Add(column);
                    }
                }
                return lookupColumns.ToArray();
            }
        }

        /// <summary>
        /// Gets all LookupColumns that have been selected (checked) by the user.
        /// </summary>
        public Column[] EnabledLookupColumns
        {
            get
            {
                List<Column> enabledLookupColumns = new List<Column>();

                foreach (Column column in _columns)
                {
                    if (column.Enabled && column.Lookup != null)
                    {
                        enabledLookupColumns.Add(column);
                    }
                }
                return enabledLookupColumns.ToArray();
            }
        }

		/// <summary>
		/// Gets all Columns that are ReadOnly.
		/// </summary>
        public Column[] ReadOnlyColumns
        {
            get
            {
                List<Column> readOnlyColumns = new List<Column>();

                foreach (Column column in _columns)
                {
                    if (column.ReadOnly)
                    {
                        readOnlyColumns.Add(column);
                    }
                }
                return readOnlyColumns.ToArray();
            }

        }

		/// <summary>
		/// Gets all Columns that are not ReadOnly. Columns that are identity fields in the underlying database are also returned.
		/// </summary>
        public Column[] UpdateableColumns
        {
            get
            {
                List<Column> updateableColumns = new List<Column>();

                foreach (Column column in _columns)
                {
                    if (!column.ReadOnly || column.IsIdentity)
                    {
                        updateableColumns.Add(column);
                    }
                }
                return updateableColumns.ToArray();
            }

        }

		/// <summary>
		/// Gets all enabled Columns that are not ReadOnly. Columns that are identity fields in the underlying database are also returned.
		/// </summary>
		public Column[] EnabledUpdateableColumns
		{
			get
			{
				List<Column> updateableColumns = new List<Column>();

				foreach (Column column in _columns)
				{
					if (column.Enabled && (!column.ReadOnly || column.IsIdentity))
					{
						updateableColumns.Add(column);
					}
				}
				return updateableColumns.ToArray();
			}

		}

		/// <summary>
		/// Gets all Filters.
		/// </summary>
        public Filter[] Filters
        {
            get
            {
            	if (Interfaces.SharedData.IsBusyGenerating)
                {
                    return EnabledFilters;
                }
            	return _filters.ToArray();
            }
        }

		/// <summary>
		/// Gets all Filters that are selected (checked) by the user.
		/// </summary>
        public Filter[] EnabledFilters
        {
            get
            {
                List<Filter> enabledFilters = new List<Filter>();

                foreach (Filter filter in _filters)
                {
                    if (filter.Enabled)
                    {
                        enabledFilters.Add(filter);
                    }
                }
                return enabledFilters.ToArray();
            }

        }

		/// <summary>
		/// Gets a superset of all Relationships.
		/// </summary>
        public Relationship[] Relationships
        {
            get
            {
                if (Interfaces.SharedData.IsBusyGenerating)
                {
                    return EnabledRelationships;
                }
            	List<Relationship> relationships = new List<Relationship>();
            	relationships.AddRange(OneToOneRelationships);
            	relationships.AddRange(OneToManyRelationships);
            	relationships.AddRange(ManyToOneRelationships);
            	relationships.AddRange(ManyToManyRelationships);
            	return relationships.ToArray();
            }

        }

		/// <summary>
		/// Gets a superset of all Relationships that are selected (checked) by the user.
		/// </summary>
        public Relationship[] EnabledRelationships
        {
            get
            {
                List<Relationship> relationships = new List<Relationship>();
                relationships.AddRange(OneToOneRelationships);
                relationships.AddRange(OneToManyRelationships);
                relationships.AddRange(ManyToOneRelationships);
                relationships.AddRange(ManyToManyRelationships);
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

        }

		/// <summary>
		/// Gets all OneToOneRelationships.
		/// </summary>
        public OneToOneRelationship[] OneToOneRelationships
        {
            get
            {
            	if (Interfaces.SharedData.IsBusyGenerating)
                {
                    return EnabledOneToOneRelationships;
                }
            	return _oneToOneRelationships.ToArray();
            }
        }

		/// <summary>
		/// Gets all OneToOneRelationships that are selected (checked) by the user.
		/// </summary>
        public OneToOneRelationship[] EnabledOneToOneRelationships
        {
            get
            {
                List<OneToOneRelationship> enabledOneToOneRelationships = new List<OneToOneRelationship>();

                foreach (OneToOneRelationship oneToOneRelationship in _oneToOneRelationships)
                {
                    if (oneToOneRelationship.Enabled)
                    {
                        enabledOneToOneRelationships.Add(oneToOneRelationship);
                    }
                }
                return enabledOneToOneRelationships.ToArray();
            }

        }

		/// <summary>
		/// Gets all OneToManyRelationships.
		/// </summary>
        public OneToManyRelationship[] OneToManyRelationships
        {
            get
            {
            	if (Interfaces.SharedData.IsBusyGenerating)
                {
                    return EnabledOneToManyRelationships;
                }
            	return _oneToManyRelationships.ToArray();
            }
        }

		/// <summary>
		/// Gets all OneToManyRelationships that are selected (checked) by the user.
		/// </summary>
        public OneToManyRelationship[] EnabledOneToManyRelationships
        {
            get
            {
                List<OneToManyRelationship> enabledOneToManyRelationships = new List<OneToManyRelationship>();

                foreach (OneToManyRelationship oneToManyRelationship in _oneToManyRelationships)
                {
                    if (oneToManyRelationship.Enabled)
                    {
                        enabledOneToManyRelationships.Add(oneToManyRelationship);
                    }
                }
                return enabledOneToManyRelationships.ToArray();
            }

        }

		/// <summary>
		/// Gets all ManyToOneRelationships.
		/// </summary>
        public ManyToOneRelationship[] ManyToOneRelationships
        {
            get
            {
            	if (Interfaces.SharedData.IsBusyGenerating)
                {
                    return EnabledManyToOneRelationships;
                }
            	return _manyToOneRelationships.ToArray();
            }
        }

		/// <summary>
		/// Gets all ManyToOneRelationships that are selected (checked) by the user.
		/// </summary>
        public ManyToOneRelationship[] EnabledManyToOneRelationships
        {
            get
            {
                List<ManyToOneRelationship> enabledManyToOneRelationships = new List<ManyToOneRelationship>();

                foreach (ManyToOneRelationship manyToOneRelationship in _manyToOneRelationships)
                {
                    if (manyToOneRelationship.Enabled)
                    {
                        enabledManyToOneRelationships.Add(manyToOneRelationship);
                    }
                }
                return enabledManyToOneRelationships.ToArray();
            }

        }

		/// <summary>
		/// Gets all ManyToManyRelationships.
		/// </summary>
        public ManyToManyRelationship[] ManyToManyRelationships
        {
            get
            {
            	if (Interfaces.SharedData.IsBusyGenerating)
                {
                    return EnabledManyToManyRelationships;
                }
            	return _manyToManyRelationships.ToArray();
            }
        }

		/// <summary>
		/// Gets all ManyToManyRelationships that are selected (checked) by the user.
		/// </summary>
        public ManyToManyRelationship[] EnabledManyToManyRelationships
        {
            get
            {
                List<ManyToManyRelationship> enabledManyToManyRelationships = new List<ManyToManyRelationship>();

                foreach (ManyToManyRelationship manyToManyRelationship in _manyToManyRelationships)
                {
                    if (manyToManyRelationship.Enabled)
                    {
                        enabledManyToManyRelationships.Add(manyToManyRelationship);
                    }
                }
                return enabledManyToManyRelationships.ToArray();
            }

        }

        #endregion


        #region Functions
		//protected List<RelationshipStoreForSerialization> RelationshipStores
		//{
		//    get
		//    {
		//        List<RelationshipStoreForSerialization> results = new List<RelationshipStoreForSerialization>(this.ManyToManyRelationships.Length);

		//        foreach (ManyToManyRelationship relationship in this.ManyToManyRelationships)
		//        {
		//            results.Add(new RelationshipStoreForSerialization(relationship, relationship.PrimaryScriptObject, relationship.ForeignScriptObject, relationship.ForeignRelationship, relationship.IntermediateForeignRelationship, relationship.IntermediatePrimaryRelationship, relationship.IntermediateForeignScriptObject));
		//        }
		//        foreach (ManyToOneRelationship relationship in this.ManyToOneRelationships)
		//        {
		//            results.Add(new RelationshipStoreForSerialization(relationship, relationship.PrimaryScriptObject, relationship.ForeignScriptObject, relationship.ForeignRelationship));
		//        }
		//        foreach (OneToManyRelationship relationship in this.OneToManyRelationships)
		//        {
		//            results.Add(new RelationshipStoreForSerialization(relationship, relationship.PrimaryScriptObject, relationship.ForeignScriptObject, relationship.ForeignRelationship));
		//        }
		//        foreach (OneToOneRelationship relationship in this.OneToOneRelationships)
		//        {
		//            results.Add(new RelationshipStoreForSerialization(relationship, relationship.PrimaryScriptObject, relationship.ForeignScriptObject, relationship.ForeignRelationship));
		//        }
		//        return results;
		//    }
		//}

		public SortedList<string,Column> ColumnsSortedByName
		{
			get 
			{
				if (_ColumnsSortedByName == null)
				{
					FillSortedColumns();
				}
				return _ColumnsSortedByName; 
			}
		}

		public SortedList<string, Column> ColumnsSortedByUniqueId
		{
			get
			{
				if (_ColumnsSortedByUniqueId == null)
				{
					FillSortedColumns();
				}
				return _ColumnsSortedByUniqueId;
			}
		}

		private void FillSortedColumns()
		{
			_ColumnsSortedByName = new SortedList<string, Column>();
			_ColumnsSortedByUniqueId = new SortedList<string, Column>();

			foreach (Column column in Columns)
			{
				_ColumnsSortedByName.Add(column.Name, column);
				_ColumnsSortedByUniqueId.Add(column.UniqueId, column);
			}
		}

        /// <exclude/>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new Exception("Fast serialization: why are we here?");
        }


        public override void ResetDefaults()
        {
            _alias = AliasDefault(this);
            _aliasPlural = AliasPluralDefault(this);
        }

        /// <summary>
        /// Gets whether the ScriptObject is in a valid state.
        /// </summary>
        /// <param name="deepCheck"></param>
        /// <param name="failReason"></param>
        /// <returns></returns>
        [Interfaces.Attributes.ApiExtension]
        public override bool IsValid(bool deepCheck, out string failReason)
        {
            bool isValid = true;
            failReason = "";
            string tempFailReason;

			if (!Enabled)
			{
				return true;
			}
            if (!AliasValidate(this, out tempFailReason))
            {
                isValid = false;
                failReason += string.Format("{1}.Alias: {0}\n", tempFailReason, Name);
            }
            if (!AliasPluralValidate(this, out tempFailReason))
            {
                isValid = false;
                failReason += string.Format("{1}.AliasPlural: {0}\n", tempFailReason, Name);
            }
            if (deepCheck)
            {
                /*Check inner objects*/

                foreach (Column column in Columns)
                {
                    if (!column.IsValid(deepCheck, out tempFailReason))
                    {
                        isValid = false;
                        failReason += string.Format("{0}.Column: {1}\n", Name, tempFailReason);
                    }
                }

                foreach (Filter filter in Filters)
                {
                    if (!filter.IsValid(deepCheck, out tempFailReason))
                    {
                        isValid = false;
                        failReason += string.Format("{0}.Filter: {1}\n", Name, tempFailReason);
                    }
                }

                foreach (MapColumn mapColumn in MapColumns)
                {
                    if (!mapColumn.IsValid(deepCheck, out tempFailReason))
                    {
                        isValid = false;
                        failReason += string.Format("{0}.MapColumn: {1}\n", Name, tempFailReason);
                    }
                }

                foreach (Relationship relationship in Relationships)
                {
                    if (!relationship.IsValid(deepCheck, out tempFailReason))
                    {
                        isValid = false;
                        failReason += string.Format("{0}.Relationship: {1}\n", Name, tempFailReason);
                    }
                }

				foreach (IUserOption userOption in Ex)
                {
                    if (!userOption.IsValid(deepCheck, out tempFailReason))
                    {
                        isValid = false;
                        failReason += string.Format("{0}.UserOption: {1}\n", Name, tempFailReason);
                    }
                }
            }
            return isValid;
        }


		[Interfaces.Attributes.ApiExtension]
        public virtual string AliasDefault(ScriptObject scriptObject)
        {
            string alias = ArchAngel.Providers.Database.Helper.Script.GetSingular(ArchAngel.Providers.Database.Helper.Script.GetSingleWord(scriptObject.Name.Trim()));
            return alias.Replace("_", "");
        }


        public override bool AliasValidate(IScriptBase scriptBase, out string failReason)
        {
            return AliasValidate((ScriptObject)scriptBase, out failReason);
        }


        public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
        {
            return AliasPluralValidate((ScriptObject)scriptBase, out failReason);
        }


        public override bool NameValidate(IScriptBase scriptBase, out string failReason)
        {
            return NameValidate((ScriptObject)scriptBase, out failReason);
        }


		[Interfaces.Attributes.ApiExtension]
        public virtual bool AliasValidate(ScriptObject scriptObject, out string failReason)
        {
            failReason = "";
            /*Don't check items that are not enabled*/

            if (!scriptObject.Enabled)
            {
                return true;
            }
            if (string.IsNullOrEmpty(scriptObject.Alias))
            {
                failReason = "Alias cannot be zero-length.";
                return false;
            }
            if (scriptObject.Alias.IndexOf(" ") >= 0)
            {
                failReason = "Alias cannot have spaces.";
                return false;
            }

            foreach (ScriptObject sibling in scriptObject.Database.EnabledScriptObjects)
            {
                if (sibling != scriptObject && scriptObject.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Alias, scriptObject.Alias, false))
                {
                    failReason = "Duplicate alias: " + scriptObject.Alias;
                    return false;
                }
            }
            return true;
        }


        public override bool Validate(IScriptBase scriptBase, out string failReason)
        {
            return Validate((ScriptObject)scriptBase, out failReason);
        }


		[Interfaces.Attributes.ApiExtension]
        public virtual bool Validate(ScriptObject scriptObject, out string failReason)
        {
            failReason = "";
            return true;
        }


		[Interfaces.Attributes.ApiExtension]
        public virtual bool NameValidate(ScriptObject scriptObject, out string failReason)
        {
            failReason = "";
            /*Don't check items that are not enabled*/

            if (!scriptObject.Enabled)
            {
                return true;
            }
            if (string.IsNullOrEmpty(scriptObject.Name))
            {
                failReason = "Name cannot be zero-length.";
                return false;
            }
            if (scriptObject.Name.IndexOf(" ") >= 0)
            {
                failReason = "Name cannot have spaces.";
                return false;
            }

            foreach (ScriptObject sibling in scriptObject.Database.EnabledScriptObjects)
            {
                if (sibling != this && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Name, Name, false))
                {
                    failReason = "Duplicate name: " + Name;
                    return false;
                }
            }
            return true;
        }


        public override string AliasDefault(IScriptBase scriptBase)
        {
            return AliasDefault((ScriptObject)scriptBase);
        }


        public override string AliasPluralDefault(IScriptBase scriptBase)
        {
            return AliasPluralDefault((ScriptObject)scriptBase);
        }


		[Interfaces.Attributes.ApiExtension]
        public virtual string AliasPluralDefault(ScriptObject scriptObject)
        {
            return ArchAngel.Providers.Database.Helper.Script.GetPlural(scriptObject.Alias);
        }


		[Interfaces.Attributes.ApiExtension]
        public virtual bool AliasPluralValidate(ScriptObject scriptObject, out string failReason)
        {
            failReason = "";
            /*Don't check items that are not enabled*/

            if (!scriptObject.Enabled)
            {
                return true;
            }
            if (string.IsNullOrEmpty(scriptObject.AliasPlural))
            {
                failReason = "AliasPlural cannot be zero-length.";
                return false;
            }
            if (scriptObject.AliasPlural.IndexOf(" ") >= 0)
            {
                failReason = "AliasPlural cannot have spaces.";
                return false;
            }
            if (ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(scriptObject.AliasPlural, scriptObject.Alias, false))
            {
                failReason = "AliasPlural must be different to Alias.";
                return false;
            }

            foreach (ScriptObject sibling in scriptObject.Database.EnabledScriptObjects)
            {
                if (sibling != this && sibling.Enabled)
                {
                    if (ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.AliasPlural, AliasPlural, false))
                    {
                        failReason = string.Format("Duplicate AliasPlural: {0}", AliasPlural);
                        return false;
                    }
                    if (ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Alias, AliasPlural, false))
                    {
                        failReason = string.Format("Object with same name exists: {0}", sibling.Alias);
                        return false;
                    }
                }
            }
            return true;
        }


        public Column GetColumn(string name)
        {

            foreach (Column column in Columns)
            {
                if (column.Name == name)
                {
                    return column;
                }
            }
            return null;
        }


        public Filter GetFilter(string name)
        {

            foreach (Filter filter in Filters)
            {
                if (filter.Name == name)
                {
                    return filter;
                }
            }
            return null;
        }


        public Relationship GetRelationship(string name)
        {

            foreach (Relationship relationship in Relationships)
            {
                if (relationship.Name == name)
                {
                    return relationship;
                }
            }
            return null;
        }


        public void AddColumn(Column column)
        {
            Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, column);
            _columns.Add(column);
        }


        public void UpdateColumn(int index, Column column)
        {
            Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _columns[index], column);
            _columns[index] = column;
        }


        public void RemoveColumn(Column column)
        {
            Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), column, null);
            _columns.Remove(column);
        }

        public void RemoveAssociation(Association association)
        {
            Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), association, null);
            _Associations.Remove(association);
        }

        public void AddRelationship(Relationship relationship)
        {
            Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, relationship);

            switch (relationship.GetType().Name)
            {
                case "OneToOneRelationship":
                    _oneToOneRelationships.Add((OneToOneRelationship)relationship);
                    break;
                case "OneToManyRelationship":
                    _oneToManyRelationships.Add((OneToManyRelationship)relationship);
                    break;
                case "ManyToOneRelationship":
                    _manyToOneRelationships.Add((ManyToOneRelationship)relationship);
                    break;
                case "ManyToManyRelationship":
                    _manyToManyRelationships.Add((ManyToManyRelationship)relationship);
                    break;
                default:

                    throw new NotImplementedException("This relationship type has not been coded yet.");
            }
        }


        public void UpdateRelationship(int index, Relationship relationship)
        {

            switch (relationship.GetType().Name)
            {
                case "OneToOneRelationship":
                    Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _oneToOneRelationships[index], relationship);
                    _oneToOneRelationships[index] = (OneToOneRelationship)relationship;
                    break;
                case "OneToManyRelationship":
                    Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _oneToManyRelationships[index], relationship);
                    _oneToManyRelationships[index] = (OneToManyRelationship)relationship;
                    break;
                case "ManyToOneRelationship":
                    Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _manyToOneRelationships[index], relationship);
                    _manyToOneRelationships[index] = (ManyToOneRelationship)relationship;
                    break;
                case "ManyToManyRelationship":
                    Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _manyToManyRelationships[index], relationship);
                    _manyToManyRelationships[index] = (ManyToManyRelationship)relationship;
                    break;
                default:

                    throw new NotImplementedException("This relationship type has not been coded yet.");
            }
        }


        public void RemoveRelationship(Relationship relationship)
        {
            Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), relationship, null);

            switch (relationship.GetType().Name)
            {
                case "OneToOneRelationship":
                    _oneToOneRelationships.Remove((OneToOneRelationship)relationship);
                    break;
                case "OneToManyRelationship":
                    _oneToManyRelationships.Remove((OneToManyRelationship)relationship);
                    break;
                case "ManyToOneRelationship":
                    _manyToOneRelationships.Remove((ManyToOneRelationship)relationship);
                    break;
                case "ManyToManyRelationship":
                    _manyToManyRelationships.Remove((ManyToManyRelationship)relationship);
                    break;
                default:

                    throw new NotImplementedException("This relationship type has not been coded yet.");
            }
        }


        public void AddFilter(Filter filter)
        {
            Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, filter);
            _filters.Add(filter);
        }

        public void AddAssociation(Association association)
        {
            Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, association);
            _Associations.Add(association);
        }

        public void UpdateFilter(int index, Filter filter)
        {
            Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _filters[index], filter);
            _filters[index] = filter;
        }


        public void RemoveFilter(Filter filter)
        {
            Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), filter, null);
            _filters.Remove(filter);
        }


        #endregion

    }

}

