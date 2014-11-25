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
    public class Relationship : ScriptBase, ISerializable
    {

        #region Fields
        [DotfuscatorDoNotRename]
        internal ScriptObject _parent;
        [DotfuscatorDoNotRename]
        internal ScriptObject _primaryScriptObject;
        [DotfuscatorDoNotRename]
        internal List<Column> _primaryColumns = new List<Column>();
        [DotfuscatorDoNotRename]
        protected ScriptObject _foreignScriptObject;
		protected string _ForeignScriptObjectId;
        [DotfuscatorDoNotRename]
        internal List<Column> _foreignColumns = new List<Column>();
		protected string[] _ForeignColumnIds;
        [DotfuscatorDoNotRename]
        protected Relationship _foreignRelationship;
		protected string _ForeignRelationshipId;
        [DotfuscatorDoNotRename]
        internal Filter _filter;
		protected string _filterId;
        private static readonly Type ManyToManyType = typeof(ManyToManyRelationship);
        #endregion


        #region Constructors
        public Relationship()
        {
        }

        public Relationship(string name, bool isUserDefined, ScriptObject parent, ScriptObject primaryScriptObject, Column[] primaryColumns, ScriptObject foreignScriptObject, Column[] foreignColumns, Filter filter)
            : base(name, isUserDefined)
        {
            _parent = parent;
            _primaryScriptObject = primaryScriptObject;
            _primaryColumns.AddRange(primaryColumns);
            _foreignScriptObject = foreignScriptObject;
            _foreignColumns.AddRange(foreignColumns);
            _filter = filter;

            if (GetType() == typeof(Relationship))
            {
                ResetDefaults();
            }
        }

        /// <exclude/>
        public Relationship(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
			throw new NotImplementedException("Why are we here?");
        }

        #endregion

        #region Properties
        public virtual string Path
        {
            get
            {
                string path = GetColumnNameList(PrimaryColumns) + " -> " + GetColumnNameList(ForeignColumns);
                return path;
            }

        }

        public ScriptObject Parent
        {
            get { return _parent; }
			internal set { _parent = value; }// This is only to be called from ScriptObject serialization constructors (table, view, storedprocedure)
        }

        public ScriptObject PrimaryScriptObject
        {
            get { return _primaryScriptObject; }
        }

        public Column[] PrimaryColumns
        {
            get
            {
            	if (Interfaces.SharedData.IsBusyGenerating)
                {
                    return EnabledPrimaryColumns;
                }
            	return _primaryColumns.ToArray();
            }

        	set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _primaryColumns, value);
                _primaryColumns = new List<Column>(value);
            }

        }

        public Column[] EnabledPrimaryColumns
        {
            get
            {
                List<Column> enabledPrimaryColumns = new List<Column>();

                foreach (Column column in _primaryColumns)
                {
                    if (column.Enabled)
                    {
                        enabledPrimaryColumns.Add(column);
                    }
                }
                return enabledPrimaryColumns.ToArray();
            }

        }

        public ScriptObject ForeignScriptObject
        {
            get 
			{
				if (_foreignScriptObject == null && _ForeignScriptObjectId != null)
				{
					_foreignScriptObject = (ScriptObject)Lookups[_ForeignScriptObjectId];
				}
				return _foreignScriptObject; 
			}
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _foreignScriptObject, value);
                _foreignScriptObject = value;
            }

        }

        public Column[] ForeignColumns
        {
            get
            {
                if (Interfaces.SharedData.IsBusyGenerating)
                {
                    return EnabledForeignColumns;
                }
            	if (_foreignColumns == null)
            	{
            		_foreignColumns = new List<Column>();

            		foreach (string foreignColumnId in _ForeignColumnIds)
            		{
            			_foreignColumns.Add((Column)Lookups[foreignColumnId]);
            		}
            	}
            	return _foreignColumns.ToArray();
            }

            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _foreignColumns, value);
                _foreignColumns = new List<Column>(value);
            }

        }

        public Column[] EnabledForeignColumns
        {
            get
            {
				if (_foreignColumns == null)
				{
					foreach (string foreignColumnId in _ForeignColumnIds)
					{
						_foreignColumns.Add((Column)Lookups[foreignColumnId]);
					}
				}
                List<Column> enabledForeignColumns = new List<Column>();

                foreach (Column column in _foreignColumns)
                {
                    if (column.Enabled)
                    {
                        enabledForeignColumns.Add(column);
                    }
                }
                return enabledForeignColumns.ToArray();
            }

        }

        public Relationship ForeignRelationship
        {
            get 
			{
				if (_foreignRelationship == null && _ForeignRelationshipId != null)
				{
					_foreignRelationship = (Relationship)Lookups[_ForeignRelationshipId];
				}
				return _foreignRelationship; 
			}
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _foreignRelationship, value);
                _foreignRelationship = value;
            }

        }

        public Filter Filter
        {
            get 
			{
				if (_filter == null && _filterId != null)
				{
					_filter = (Filter)Lookups[_filterId];
				}
				return _filter; 
			}
            set { _filter = value; }
        }

        #endregion


        #region Functions
        public override bool NameValidate(IScriptBase scriptBase, out string failReason)
        {
            return NameValidate((Relationship)scriptBase, out failReason);
        }


		[Interfaces.Attributes.ApiExtension]
        public bool NameValidate(Relationship relationship, out string failReason)
        {
            failReason = "";
            /*Don't check items that are not enabled*/

            if (!relationship.Enabled)
            {
                return true;
            }
            if (string.IsNullOrEmpty(relationship.Name))
            {
                failReason = "Name cannot be zero-length.";
                return false;
            }
            if (relationship.Name.IndexOf(" ") >= 0)
            {
                failReason = "Name cannot have spaces.";
                return false;
            }
            return true;
        }


        /// <exclude/>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
#if FAST_SERIALIZATION
			throw new Exception("Why are we here?");
            using (SerializationWriter writer = new SerializationWriter())
            {
				writer.Write(UniqueId);
                writer.Write(_alias);
                writer.Write(_enabled);
				//writer.Write(_exposedUserOptions);
                writer.Write(Filter.UniqueId);
                //writer.Write(_foreignColumns);
				writer.Write(ScriptBase.GetUniqueIds(ForeignColumns));
                writer.Write(ForeignRelationship.UniqueId);
                writer.Write(ForeignScriptObject.UniqueId);
                writer.Write(_isUserDefined);
                writer.Write(_name);
				//writer.WriteObject(_parent);
                //writer.Write(_primaryColumns);
				writer.Write(ScriptBase.GetUniqueIds(PrimaryColumns));
                //writer.WriteObject(_primaryScriptObject);
				writer.WriteObject(_userOptions);
                info.AddValue("d", writer.ToArray());
            }
#else
            info.AddValue("SerializationVersion", SerializationVersion);
            info.AddValue("Alias", _alias);
            info.AddValue("Enabled", _enabled);
			//info.AddValue("ExposedUserOptions", _exposedUserOptions);
            info.AddValue("Filter", _filter);
            info.AddValue("ForeignColumns", _foreignColumns);
            info.AddValue("ForeignRelationship", _foreignRelationship);
            info.AddValue("ForeignScriptObject", _foreignScriptObject);
            info.AddValue("IsUserDefined", _isUserDefined);
            info.AddValue("Name", _name);
            info.AddValue("Parent", _parent);
            info.AddValue("PrimaryColumns", _primaryColumns);
            info.AddValue("PrimaryScriptObject", _primaryScriptObject);
            info.AddValue("UserOptions", _userOptions);
#endif
        }


        public override void ResetDefaults()
        {
            Alias = AliasDefault(this);
        }


        public override string AliasDefault(IScriptBase relationship)
        {
            return AliasDefault((Relationship)relationship);
        }


		[Interfaces.Attributes.ApiExtension]
        public string AliasDefault(Relationship relationship)
        {
            return ArchAngel.Providers.Database.Helper.Script.GetSingular(ArchAngel.Providers.Database.Helper.Script.GetSingleWord(relationship.Name.Trim()));
        }


		[Interfaces.Attributes.ApiExtension]
        public bool AliasValidate(Relationship relationship, out string failReason)
        {
			failReason = "";

			if (relationship.Parent == null)
			{
				failReason = "Missing parent: " + relationship.Alias;
				return false;
			}
            foreach (Relationship sibling in relationship.Parent.Relationships)
            {
                if (sibling != relationship && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Alias, relationship.Alias, false))
                {
                    failReason = "Duplicate alias: " + relationship.Alias;
                    return false;
                }
            }
            return true;
        }


        protected static string GetColumnNameList(Column[] columns)
        {
        	string columnList = "";

        	if (columns.Length > 1)
        	{
        		columnList += "(";
        	}

        	for (int i = 0; i < columns.Length; i++)
        	{
        		Column column = columns[i];
        		columnList += column.Parent.Alias + "." + column.Alias;

        		if (i < columns.Length - 1)
        		{
        			columnList += ",";
        		}
        	}

        	if (columns.Length > 1)
        	{
        		columnList += ")";
        	}
        	return columnList;
        }


		[Interfaces.Attributes.ApiExtension]
        public virtual string AliasPluralDefault(Relationship relationship)
        {
            return ArchAngel.Providers.Database.Helper.Script.GetPlural(relationship.Alias);
        }


		[Interfaces.Attributes.ApiExtension]
        public bool AliasPluralValidate(Relationship relationship, out string failReason)
        {
            failReason = "";
            return true;
        }


        public override string AliasPluralDefault(IScriptBase scriptBase)
        {
            return AliasPluralDefault((Relationship)scriptBase);
        }


        public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
        {
            return AliasPluralValidate((Relationship)scriptBase, out failReason);
        }


        public override bool AliasValidate(IScriptBase scriptBase, out string failReason)
        {
            return AliasValidate((Relationship)scriptBase, out failReason);
        }

        internal static bool IsValidFilter(Relationship relationship, out string failReason)
        {
			failReason = "";

			if (!relationship.Enabled)
			{
				return true;
			}
            if (ManyToManyType.IsInstanceOfType(relationship))
            {
                return IsValidManyToManyFilter((ManyToManyRelationship)relationship, out failReason);
            }
			if (relationship.Filter == null)
			{
				failReason = string.Format("Relationship [{0}] has no filter.", relationship.Alias);
				return false;
			}
            for (int columnCounter = 0; columnCounter < relationship.PrimaryColumns.Length; columnCounter++)
            {
                bool found = false;

                for (int i = 0; i < relationship.Filter.FilterColumns.Length; i++)
                {
                    if (relationship.Filter.FilterColumns[i].Column == relationship.PrimaryColumns[columnCounter])
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    failReason = string.Format("{0}: Filter doesn't contain all necessary Relationship columns.\n", relationship.Filter.Alias);
                    return false;
                }
            }
            return true;
        }

        internal static bool IsValidManyToManyFilter(ManyToManyRelationship relationship, out string failReason)
        {
            failReason = "";

			if (!relationship.Enabled)
			{
				return true;
			}
			if (relationship.IntermediateForeignRelationship.Filter == null)
			{
				failReason = string.Format("{0} [{1}.{2}] has no filter.", 
					relationship.IntermediateForeignRelationship.GetType().Name, 
					relationship.IntermediateForeignRelationship.Parent.Alias, 
					relationship.IntermediateForeignRelationship.Alias);
				return false;
			}
			if (relationship.IntermediatePrimaryRelationship.Filter == null)
			{
				failReason = string.Format("{0} [{1}.{2}] has no filter.", 
					relationship.IntermediatePrimaryRelationship.GetType().Name,
					relationship.IntermediatePrimaryRelationship.Parent.Alias,
					relationship.IntermediatePrimaryRelationship.Alias);
				return false;
			}
			if (!relationship.IntermediateForeignRelationship.IsValid(true, out failReason))
			{
				return false;
			}
			if (!relationship.IntermediatePrimaryRelationship.IsValid(true, out failReason))
			{
				return false;
			}
			//for (int columnCounter = 0; columnCounter < relationship.ForeignColumns.Length; columnCounter++)
			//{
			//    bool found = false;

			//    for (int i = 0; i < relationship.Filter.FilterColumns.Length; i++)
			//    {
			//        if (relationship.Filter.FilterColumns[i].Column == relationship.ForeignColumns[columnCounter])
			//        {
			//            found = true;
			//            break;
			//        }
			//    }
			//    if (!found)
			//    {
			//        failReason = string.Format("{0}: Filter doesn't contain all necessary Relationship columns.\n", relationship.Filter.Alias);
			//        return false;
			//    }
			//}
            return true;
        }

        /// <summary>
        /// Gets whether the Relationship is in a valid state.
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
            if (!IsValidFilter(this, out tempFailReason) || !IsValidFilter(ForeignRelationship, out tempFailReason))
            {
                isValid = false;
                failReason += tempFailReason;
            }
            if (deepCheck)
            {
                /*Check inner objects*/
                if (Filter != null && !Filter.IsValid(deepCheck, out tempFailReason))
                {
                    isValid = false;
                    failReason += string.Format("{0}.Filter: {1}\n", Name, tempFailReason);
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


        #endregion

    }

}

