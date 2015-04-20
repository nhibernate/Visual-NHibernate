using ArchAngel.Interfaces.ITemplate;
using Slyce.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ArchAngel.Providers.Database.Model
{
    /// <summary> This represents a column in the object model. A column belongs to a
    /// ScriptObject (Table, View or StoreedProcedure). It usually corresponds to a real column in the database from which the
    /// schema is derived, however it can also be a virtual column created by the
    /// end-user in ArchAngel Workbench.</summary>
    [Serializable]
    [Interfaces.Attributes.ArchAngelEditor(true, true, "Alias")]
    [DotfuscatorDoNotRename]
    public class Column : ScriptBase, ISerializable
    {

        #region Fields
        [DotfuscatorDoNotRename]
        protected string _aliasDisplay;
        [DotfuscatorDoNotRename]
        internal ScriptObject _parent;
        [DotfuscatorDoNotRename]
        internal int _ordinalPosition;
        [DotfuscatorDoNotRename]
        protected bool _isNullable;
        [DotfuscatorDoNotRename]
        protected string _dataType;
        [DotfuscatorDoNotRename]
        protected int _characterMaximumLength;
        [DotfuscatorDoNotRename]
        protected bool _inPrimaryKey;
        [DotfuscatorDoNotRename]
        internal bool _isIdentity;
        [DotfuscatorDoNotRename]
        internal string _default;
        [DotfuscatorDoNotRename]
        internal bool _readOnly;
        [DotfuscatorDoNotRename]
        internal bool _IsCalculated;
        [DotfuscatorDoNotRename]
        internal int _precision;
        [DotfuscatorDoNotRename]
        internal int _scale;
        protected bool _IsMapColumn = false;
        [DotfuscatorDoNotRename]
        protected Lookup _Lookup;
        #endregion

        #region Constructors
        /// <summary>
        /// TODO: I don't think this should be exposed to the user???
        /// </summary>
        public Column()
        {
        }

        /// <summary>
        /// TODO: I don't think this should be exposed to the user???
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isUserDefined"></param>
        public Column(string name, bool isUserDefined)
            : base(name, isUserDefined)
        {
            if (GetType() == typeof(Column))
            {
                ResetDefaults();
            }
        }

        /// <summary>
        /// TODO: I don't think this should be exposed to the user???
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isUserDefined"></param>
        /// <param name="parent"></param>
        /// <param name="ordinalPosition"></param>
        /// <param name="isNullable"></param>
        /// <param name="dataType"></param>
        /// <param name="characterMaximumLength"></param>
        /// <param name="inPrimaryKey"></param>
        /// <param name="isIdentity"></param>
        /// <param name="defaultValue"></param>
        /// <param name="readOnly"></param>
        /// <param name="isCalculated"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        public Column(string name, bool isUserDefined, ScriptObject parent, int ordinalPosition, bool isNullable, string dataType, int characterMaximumLength, bool inPrimaryKey, bool isIdentity, string defaultValue, bool readOnly, bool isCalculated, int precision, int scale)
            : base(name, isUserDefined)
        {
            _parent = parent;
            _ordinalPosition = ordinalPosition;
            _isNullable = isNullable;
            _dataType = dataType;
            _characterMaximumLength = characterMaximumLength;
            _inPrimaryKey = inPrimaryKey;
            _isIdentity = isIdentity;
            _default = defaultValue;
            _readOnly = readOnly;
            _IsCalculated = isCalculated;
            _precision = precision;
            _scale = scale;

            if (GetType() == typeof(Column))
            {
                ResetDefaults();
            }
        }

        /// <summary>
        /// TODO: I don't think this should be exposed to the user???
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        public Column(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            if (SerializerHelper.UseFastSerialization)
            {
                using (SerializationReader reader = new SerializationReader((byte[])serializationInfo.GetValue("d", typeof(byte[]))))
                {
                    UniqueId = reader.ReadString();
                    Lookups.Add(UniqueId, this);
                    _alias = reader.ReadString();
                    _aliasDisplay = reader.ReadString();
                    _characterMaximumLength = reader.ReadInt32();
                    _dataType = reader.ReadString();
                    _default = reader.ReadString();
                    _enabled = reader.ReadBoolean();
                    _inPrimaryKey = reader.ReadBoolean();
                    _isIdentity = reader.ReadBoolean();
                    _isNullable = reader.ReadBoolean();
                    _isUserDefined = reader.ReadBoolean();
                    _name = reader.ReadString();
                    _ordinalPosition = reader.ReadInt32();
                    // Parent
                    _readOnly = reader.ReadBoolean();
                    _userOptions = (List<IUserOption>)reader.ReadObject();
                    _IsCalculated = reader.ReadBoolean();
                    _precision = reader.ReadInt32();
                    _scale = reader.ReadInt32();

                    for (int i = 0; i < _userOptions.Count; i++)
                    {
                        _userOptions[i].Owner = this;
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
                _alias = serializationInfo.GetString("Alias");
                _aliasDisplay = serializationInfo.GetString("AliasDisplay");
                _characterMaximumLength = serializationInfo.GetInt32("CharacterMaximumLength");
                _dataType = serializationInfo.GetString("DataType");
                _default = serializationInfo.GetString("Default");
                _enabled = serializationInfo.GetBoolean("Enabled");
                //this._exposedUserOptions = serializationInfo.GetValue("ExposedUserOptions", ObjectType);
                _inPrimaryKey = serializationInfo.GetBoolean("InPrimaryKey");
                _isIdentity = serializationInfo.GetBoolean("IsIdentity");
                _isNullable = serializationInfo.GetBoolean("IsNullable");
                _isUserDefined = serializationInfo.GetBoolean("IsUserDefined");
                _name = serializationInfo.GetString("Name");
                _ordinalPosition = serializationInfo.GetInt32("OrdinalPosition");
                _parent = (ScriptObject)serializationInfo.GetValue("Parent", ModelTypes.ScriptObject);
                _readOnly = serializationInfo.GetBoolean("ReadOnly");
                _userOptions = (List<IUserOption>)serializationInfo.GetValue("UserOptions", ModelTypes.UserOptionList);

                if (version >= 2)
                {
                    _IsCalculated = serializationInfo.GetBoolean("IsCalculated");

                    if (version >= 3)
                    {
                        _precision = serializationInfo.GetInt32("Precision");
                        _scale = serializationInfo.GetInt32("Scale");

                        if (version >= 8)
                        {
                            _description = serializationInfo.GetString("Description");

                            if (version >= 9)
                            {
                                _Lookup = (Lookup)serializationInfo.GetValue("Lookup", ModelTypes.Lookup);
                                //_Lookup.SubscribingObjects.Add(this);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Properties
        /// <summary>
        /// TODO: Why is this not called Alias for consistency?
        /// </summary>
        public string AliasDisplay
        {
            get { return _aliasDisplay; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _aliasDisplay, value);
                _aliasDisplay = value;
            }

        }

        /// <summary>
        /// The object which this column belongs to. It can be one of table, view, stored
        /// procedure or ScriptObject, which is the base object of the other three.
        /// </summary>
        public ScriptObject Parent
        {
            get { return _parent; }
            internal set { _parent = value; }
        }

        /// <summary>
        /// Index of this column in the collection of columns that it belongs to. This indicates the order of the column.
        /// </summary>
        public int OrdinalPosition
        {
            get { return _ordinalPosition; }
        }

        /// <summary>
        /// Whether this column can accept null values.
        /// </summary>
        public bool IsNullable
        {
            get { return _isNullable; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _isNullable, value);
                _isNullable = value;
            }

        }

        /// <summary>
        /// The data-type that this column stores.
        /// </summary>
        public string DataType
        {
            get { return _dataType; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _dataType, value);
                _dataType = value;
            }
        }

        /// <summary>
        /// The Lookup that this column uses.
        /// </summary>
        public Lookup Lookup
        {
            get { return _Lookup; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _Lookup, value);
                _Lookup = value;
            }
        }

        /// <summary>
        /// The data-type that this column stores.
        /// </summary>
        public int Precision
        {
            get { return _precision; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _precision, value);
                _precision = value;
            }

        }

        /// <summary>
        /// Gets whether this column is a MapColumn.
        /// </summary>
        public bool IsMapColumn
        {
            get { return _IsMapColumn; }
        }

        /// <summary>
        /// The data-type that this column stores.
        /// </summary>
        public int Scale
        {
            get { return _scale; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _scale, value);
                _scale = value;
            }

        }

        /// <summary>
        /// When DataType is a string type, this indicates what the maximum length of the string can be.
        /// </summary>
        public int Size
        {
            get { return _characterMaximumLength; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _characterMaximumLength, value);
                _characterMaximumLength = value;
            }

        }

        /// <summary>
        /// Whether this column is part of it's parent's primary key. A primary key can consist of one or more columns.
        /// </summary>
        /// <value>Returns true if this column is part of the primary key, false otherwise.</value>
        public bool InPrimaryKey
        {
            get { return _inPrimaryKey; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _inPrimaryKey, value);
                _inPrimaryKey = value;
            }

        }

        /// <summary>
        /// Whether this columns values are automatically assigned by the database engine.
        /// </summary>
        /// <example>
        /// Returns true if this column is an identity field in the database, false
        /// otherwise.
        /// </example>
        public bool IsIdentity
        {
            get { return _isIdentity; }
        }

        /// <summary>
        /// The default value assigned to this column by the database engine.
        /// </summary>
        public string Default
        {
            get { return _default; }
        }

        /// <summary>
        /// Whether this column is read-only or can be written to. True if the column is an IDENTITY field, a computed field or a timestamp data-type.
        /// </summary>
        public bool ReadOnly
        {
            get { return _readOnly; }
        }

        public bool IsCalculated
        {
            get { return _IsCalculated; }
        }
        #endregion


        #region Functions
        public override bool NameValidate(IScriptBase scriptBase, out string failReason)
        {
            return NameValidate((Column)scriptBase, out failReason);
        }


        [Interfaces.Attributes.ApiExtension]
        public bool NameValidate(Column column, out string failReason)
        {
            failReason = "";
            /*Don't check items that are not enabled*/

            if (!column.Enabled)
            {
                return true;
            }
            if (string.IsNullOrEmpty(column.Name))
            {
                failReason = "Name cannot be zero-length.";
                return false;
            }
            if (column.Name.IndexOf(" ") >= 0)
            {
                failReason = "Name cannot have spaces.";
                return false;
            }

            foreach (Column sibling in column.Parent.Columns)
            {
                if (sibling != this && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Name, Name, false))
                {
                    failReason = "Duplicate name: " + Name;
                    return false;
                }
            }
            return true;
        }


        /// <exclude/>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
#if FAST_SERIALIZATION
			using (SerializationWriter writer = new SerializationWriter())
			{
				writer.Write(UniqueId);
				writer.Write(_alias);
				writer.Write(_aliasDisplay);
				writer.Write(_characterMaximumLength);
				writer.Write(_dataType);
				writer.Write(_default);
				writer.Write(_enabled);
				//writer.Write(_exposedUserOptions);
				writer.Write(_inPrimaryKey);
				writer.Write(_isIdentity);
				writer.Write(_isNullable);
				writer.Write(_isUserDefined);
				writer.Write(_name);
				writer.Write(_ordinalPosition);
				//writer.WriteObject(_parent);
				writer.Write(_readOnly);
				writer.WriteObject(_userOptions);
				writer.Write(_IsCalculated);
				writer.Write(_precision);
				writer.Write(_scale);
				info.AddValue("d", writer.ToArray());
			}
#else
            info.AddValue("SerializationVersion", SerializationVersion);
            info.AddValue("Alias", _alias);
            info.AddValue("AliasDisplay", _aliasDisplay);
            info.AddValue("CharacterMaximumLength", _characterMaximumLength);
            info.AddValue("DataType", _dataType);
            info.AddValue("Default", _default);
            info.AddValue("Enabled", _enabled);
            //info.AddValue("ExposedUserOptions", this._exposedUserOptions);
            info.AddValue("InPrimaryKey", _inPrimaryKey);
            info.AddValue("IsIdentity", _isIdentity);
            info.AddValue("IsNullable", _isNullable);
            info.AddValue("IsUserDefined", _isUserDefined);
            info.AddValue("Name", _name);
            info.AddValue("OrdinalPosition", _ordinalPosition);
            info.AddValue("Parent", _parent);
            info.AddValue("ReadOnly", _readOnly);
            info.AddValue("UserOptions", _userOptions);
            info.AddValue("IsCalculated", _IsCalculated);
            info.AddValue("Precision", _precision);
            info.AddValue("Scale", _scale);
            info.AddValue("Description", _description);
            info.AddValue("Lookup", _Lookup);
#endif
        }


        public override void ResetDefaults()
        {
            Alias = AliasDefault(this);
            AliasDisplay = AliasDisplayDefault(this);
        }


        public override string AliasDefault(IScriptBase column)
        {
            return AliasDefault((Column)column);
        }


        [Interfaces.Attributes.ApiExtension]
        public string AliasDefault(Column column)
        {
            string alias = ArchAngel.Providers.Database.Helper.Script.GetSingleWord(column.Name.Trim());
            return alias.Replace("_", "");
        }


        [Interfaces.Attributes.ApiExtension]
        public bool AliasValidate(Column column, out string failReason)
        {
            failReason = "";
            /*Don't check items that are not enabled*/

            if (!column.Enabled)
            {
                return true;
            }
            if (string.IsNullOrEmpty(column.Alias))
            {
                failReason = "Alias cannot be zero-length.";
                return false;
            }
            if (column.Alias.IndexOf(" ") >= 0)
            {
                failReason = "Alias cannot have spaces.";
                return false;
            }

            foreach (Column sibling in column.Parent.Columns)
            {
                if (sibling != column && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Alias, column.Alias, false))
                {
                    failReason = "Duplicate alias: " + column.Alias;
                    return false;
                }
            }
            return true;
        }


        [Interfaces.Attributes.ApiExtension]
        public string AliasDisplayDefault(Column column)
        {
            return column.Alias;
        }


        [Interfaces.Attributes.ApiExtension]
        public bool AliasDisplayValidate(Column column, out string failReason)
        {
            failReason = "";
            /*Don't check items that are not enabled*/

            if (!column.Enabled)
            {
                return true;
            }
            if (string.IsNullOrEmpty(column.AliasDisplay))
            {
                failReason = "AliasDisplay cannot be zero-length.";
                return false;
            }

            foreach (Column sibling in column.Parent.Columns)
            {
                if (sibling != column && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.AliasDisplay, column.AliasDisplay, false))
                {
                    failReason = "Duplicate AliasDisplay: " + column.AliasDisplay;
                    return false;
                }
            }
            return true;
        }


        [Interfaces.Attributes.ApiExtension]
        public virtual string AliasPluralDefault(Column column)
        {
            return ArchAngel.Providers.Database.Helper.Script.GetPlural(column.Alias);
        }


        public override bool AliasValidate(IScriptBase scriptBase, out string failReason)
        {
            return AliasValidate((Column)scriptBase, out failReason);
        }


        public override string AliasPluralDefault(IScriptBase scriptBase)
        {
            return AliasPluralDefault((Column)scriptBase);
        }


        public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
        {
            return AliasPluralValidate((Column)scriptBase, out failReason);
        }


        [Interfaces.Attributes.ApiExtension]
        public bool AliasPluralValidate(Column column, out string failReason)
        {
            failReason = "";
            /*Don't check items that are not enabled*/

            if (!column.Enabled)
            {
                return true;
            }
            if (string.IsNullOrEmpty(column.AliasPlural))
            {
                failReason = "AliasPlural cannot be zero-length.";
                return false;
            }
            if (column.AliasPlural.IndexOf(" ") >= 0)
            {
                failReason = "AliasPlural cannot have spaces.";
                return false;
            }

            foreach (Column sibling in column.Parent.Columns)
            {
                if (sibling != column && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.AliasPlural, column.AliasPlural, false))
                {
                    failReason = "Duplicate AliasPlural: " + column.AliasPlural;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets whether the Column is in a valid state.
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
                failReason += string.Format("Column.Alias ({1}): {0}\n", tempFailReason, Name);
            }
            if (!AliasDisplayValidate(this, out tempFailReason))
            {
                isValid = false;
                failReason += string.Format("Column.AliasDisplay ({1}): {0}\n", tempFailReason, Name);
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


        #endregion

    }

}

