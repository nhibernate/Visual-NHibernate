using ArchAngel.Interfaces.ITemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Reflection;

namespace ArchAngel.Providers.Database.Model
{
    [Serializable]
    [Interfaces.Attributes.ArchAngelEditor(true, true, "Alias")]
    [DotfuscatorDoNotRename]
    public class Lookup : ScriptBase, ISerializable
    {
        [DotfuscatorDoNotRename] protected Database _database;
        [DotfuscatorDoNotRename] private ScriptObject _backingObject;
        [DotfuscatorDoNotRename] private Column _idColumn;
        [DotfuscatorDoNotRename] private Column _nameColumn;
        [DotfuscatorDoNotRename] private List<IScriptBase> _subscribingObjects = new List<IScriptBase>();
        [DotfuscatorDoNotRename] private List<LookupValue> _LookupValues = new List<LookupValue>();

         #region Constructors
        public Lookup()
        {
        }

        public Lookup(string name, bool isUserDefined)
            : base(name, isUserDefined)
        {
        }
        #endregion

        /// <exclude/>
        public Lookup(SerializationInfo serializationInfo, StreamingContext streamingContext)
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
            _alias = serializationInfo.GetString("Alias");
            _aliasPlural = serializationInfo.GetString("AliasPlural");
            _enabled = serializationInfo.GetBoolean("Enabled");
            _isUserDefined = serializationInfo.GetBoolean("IsUserDefined");
            _name = serializationInfo.GetString("Name");
            _userOptions = (List<IUserOption>)serializationInfo.GetValue("UserOptions", ModelTypes.UserOptionList);
            _description = serializationInfo.GetString("Description");
            _backingObject = (ScriptObject)serializationInfo.GetValue("BackingObject", ModelTypes.ScriptObject);
            _idColumn = (Column)serializationInfo.GetValue("IdColumn", ModelTypes.Column);
            _nameColumn = (Column)serializationInfo.GetValue("NameColumn", ModelTypes.Column);
            _LookupValues = (List<LookupValue>)serializationInfo.GetValue("LookupValues", ModelTypes.LookupValueList);
        }

        /// <exclude/>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SerializationVersion", SerializationVersion);
            info.AddValue("Alias", _alias);
            info.AddValue("AliasPlural", _aliasPlural);
            info.AddValue("Enabled", _enabled);
            info.AddValue("IsUserDefined", _isUserDefined);
            info.AddValue("Name", _name);
            info.AddValue("UserOptions", _userOptions);
            info.AddValue("Description", _description);
            info.AddValue("BackingObject", _backingObject);
            info.AddValue("IdColumn", _idColumn);
            info.AddValue("NameColumn", _nameColumn);
            info.AddValue("LookupValues", _LookupValues);
        }

        public Database Database
        {
            get { return _database; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _database, value);
                _database = value;
            }
        }

        public List<LookupValue> LookupValues
        {
            get { return _LookupValues; }
            set { _LookupValues = value; }
        }

        public ScriptObject BackingObject
        {
            get { return _backingObject; }
            set { _backingObject = value; }
        }

        public Column IdColumn
        {
            get { return _idColumn; }
            set { _idColumn = value; }
        }

        public Column NameColumn
        {
            get { return _nameColumn; }
            set { _nameColumn = value; }
        }

        public List<IScriptBase> SubscribingObjects
        {
            get { return _subscribingObjects; }
            set { _subscribingObjects = value; }
        }

        public override string AliasPluralDefault(IScriptBase scriptBase)
        {
            return AliasPluralDefault((Lookup)scriptBase);
        }

        [Interfaces.Attributes.ApiExtension]
        public virtual string AliasPluralDefault(Lookup lookup)
        {
            return ArchAngel.Providers.Database.Helper.Script.GetPlural(lookup.Alias);
        }

        public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
        {
            return AliasPluralValidate((Lookup)scriptBase, out failReason);
        }


        [Interfaces.Attributes.ApiExtension]
        public bool AliasPluralValidate(Lookup lookup, out string failReason)
        {
            failReason = "";
            /*Don't check items that are not enabled*/

            if (!lookup.Enabled)
            {
                return true;
            }
            if (string.IsNullOrEmpty(lookup.AliasPlural))
            {
                failReason = "AliasPlural cannot be zero-length.";
                return false;
            }
            if (lookup.AliasPlural.IndexOf(" ") >= 0)
            {
                failReason = "AliasPlural cannot have spaces.";
                return false;
            }

            foreach (Lookup sibling in lookup.Database.Lookups)
            {
                if (sibling != lookup && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.AliasPlural, lookup.AliasPlural, false))
                {
                    failReason = "Duplicate AliasPlural: " + lookup.AliasPlural;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets whether the Lookup is in a valid state.
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
                failReason += string.Format("Lookup.Alias ({1}): {0}\n", tempFailReason, Name);
            }
            // Check that all values are unique
            List<object> existingLookupIds = new List<object>();
            List<string> existingLookupNames = new List<string>();

            foreach (LookupValue lookupValue in this.LookupValues)
            {
                if (existingLookupIds.BinarySearch(lookupValue.Id) >= 0 ||
                    existingLookupNames.BinarySearch(lookupValue.Name) >= 0)
                {
                    isValid = false;
                    failReason += string.Format("Non-unique lookup values: ID({0}), Name({1})\n", lookupValue.Id, lookupValue.Name);
                }
            }
            if (deepCheck)
            {
                /*Check inner objects*/

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

        /// <summary>
        /// Gets a collection of all columns using this Lookup.
        /// </summary>
        /// <returns></returns>
        public List<Column> GetSubscribingColumns()
        {
            List<Column> subscribingColumns = new List<Column>();
            ScriptObject[] scriptObjects = this.Database.ScriptObjects;

            for (int scriptObjectCounter = 0; scriptObjectCounter < scriptObjects.Length; scriptObjectCounter++)
            {
                Column[] columns = scriptObjects[scriptObjectCounter].Columns;

                for (int columnCounter = 0; columnCounter < columns.Length; columnCounter++)
                {
                    if (columns[columnCounter].Lookup == this)
                    {
                        subscribingColumns.Add(columns[columnCounter]);
                    }
                }
            }
            return subscribingColumns;
        }

    }
}
