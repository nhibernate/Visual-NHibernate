using ArchAngel.Interfaces.ITemplate;
using Slyce.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ArchAngel.Providers.Database.Model
{
    [Serializable]
    [Interfaces.Attributes.ArchAngelEditor(true, false, "Alias")]
    [DotfuscatorDoNotRename]
    public class RestrictedValue : ScriptBase, ISerializable
    {
        #region Fields

        [DotfuscatorDoNotRename]
        private object _id;
        [DotfuscatorDoNotRename]
        internal Column _parent;

        #endregion

        #region Constructors
        public RestrictedValue()
        {
        }

        public RestrictedValue(string name, bool isUserDefined)
            : base(name, isUserDefined)
        {
            ResetDefaults();
        }

        public RestrictedValue(string name, bool isUserDefined, Column parent, object id, string description)
            : base(name, isUserDefined)
        {
            _parent = parent;
            _id = id;
            _description = description;
            ResetDefaults();
        }

        /// <exclude/>
        public RestrictedValue(SerializationInfo serializationInfo, StreamingContext streamingContext)
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
            _id = serializationInfo.GetString("Id");
            _description = serializationInfo.GetString("Description");
            _parent = (Column)serializationInfo.GetValue("Parent", ModelTypes.Column);
        }

        #endregion

        #region Properties

        public object Id
        {
            get { return _id; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _id, value);
                _id = value;
            }
        }

        public Column Parent
        {
            get { return _parent; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _parent, value);
                _parent = value;
            }
        }

        #endregion

        #region Functions

        public override bool NameValidate(IScriptBase scriptBase, out string failReason)
        {
            return NameValidate((RestrictedValue)scriptBase, out failReason);
        }

        [Interfaces.Attributes.ApiExtension]
        public bool NameValidate(RestrictedValue restrictedValue, out string failReason)
        {
            failReason = "";
            /*Don't check items that are not enabled*/

            if (!restrictedValue.Enabled)
            {
                return true;
            }
            if (string.IsNullOrEmpty(restrictedValue.Name))
            {
                failReason = "Name cannot be zero-length.";
                return false;
            }
            if (restrictedValue.Name.IndexOf(" ") >= 0)
            {
                failReason = "Name cannot have spaces.";
                return false;
            }

            foreach (RestrictedValue sibling in restrictedValue.Parent.RestrictedValues)
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
            info.AddValue("SerializationVersion", SerializationVersion);
            info.AddValue("Alias", _alias);
            info.AddValue("AliasPlural", _aliasPlural);
            info.AddValue("Enabled", _enabled);
            info.AddValue("IsUserDefined", _isUserDefined);
            info.AddValue("Name", _name);
            info.AddValue("UserOptions", _userOptions);
            info.AddValue("Id", _id);
            info.AddValue("Description", _description);
            info.AddValue("Parent", _parent);
        }

        public override void ResetDefaults()
        {
            Alias = AliasDefault(this);
            AliasPlural = AliasPluralDefault(this);
        }

        public override string AliasDefault(IScriptBase restrictedValue)
        {
            return AliasDefault((RestrictedValue)restrictedValue);
        }

        [Interfaces.Attributes.ApiExtension]
        public string AliasDefault(RestrictedValue restrictedValue)
        {
            string alias = ArchAngel.Providers.Database.Helper.Script.GetSingleWord(restrictedValue.Name.Trim());
            alias = ArchAngel.Providers.Database.Helper.Script.GetSingular(alias);
            return alias.Replace("_", "").Replace("/", "_or_").Replace("\"", "_or_");
        }

        [Interfaces.Attributes.ApiExtension]
        public bool AliasValidate(RestrictedValue restrictedValue, out string failReason)
        {
            failReason = "";
            return true;
        }

        [Interfaces.Attributes.ApiExtension]
        public string AliasDisplayDefault(RestrictedValue restrictedValue)
        {
            return restrictedValue.Alias;
        }

        [Interfaces.Attributes.ApiExtension]
        public bool AliasDisplayValidate(RestrictedValue restrictedValue, out string failReason)
        {
            failReason = "";
            return true;
        }

        [Interfaces.Attributes.ApiExtension]
        public bool AliasPluralValidate(RestrictedValue restrictedValue, out string failReason)
        {
            failReason = "";
            return true;
        }

        public override string AliasPluralDefault(IScriptBase restrictedValue)
        {
            return AliasPluralDefault((RestrictedValue)restrictedValue);
        }

        [Interfaces.Attributes.ApiExtension]
        public virtual string AliasPluralDefault(RestrictedValue restrictedValue)
        {
            return ArchAngel.Providers.Database.Helper.Script.GetPlural(restrictedValue.Alias);
        }

        public override bool AliasPluralValidate(IScriptBase restrictedValue, out string failReason)
        {
            return AliasPluralValidate((RestrictedValue)restrictedValue, out failReason);
        }

        public override bool AliasValidate(IScriptBase restrictedValue, out string failReason)
        {
            return AliasValidate((RestrictedValue)restrictedValue, out failReason);
        }

        /// <summary>
        /// Gets whether the RestrictedValue is in a valid state.
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
                failReason += string.Format("LookupValue.Alias ({1}): {0}\n", tempFailReason, Name);
            }
            if (!AliasDisplayValidate(this, out tempFailReason))
            {
                isValid = false;
                failReason += string.Format("LookupValue.AliasDisplay ({1}): {0}\n", tempFailReason, Name);
            }
            // Check that all values are unique
            List<object> existingLookupIds = new List<object>();
            List<string> existingLookupNames = new List<string>();

            foreach (RestrictedValue sibling in this.Parent.RestrictedValues)
            {
                if (sibling == this)
                {
                    continue;
                }
                if (sibling.Id.Equals(this.Id))
                {
                    isValid = false;
                    failReason += "Duplicate Id.";
                    break;
                }
                if (Slyce.Common.Utility.StringsAreEqual(sibling.Name, this.Name, false))
                {
                    isValid = false;
                    failReason += "Duplicate Name.";
                    break;
                }
                if (Slyce.Common.Utility.StringsAreEqual(sibling.Alias, this.Alias, false))
                {
                    isValid = false;
                    failReason += "Duplicate Alias.";
                    break;
                }
                if (Slyce.Common.Utility.StringsAreEqual(sibling.AliasPlural, this.AliasPlural, false))
                {
                    isValid = false;
                    failReason += "Duplicate AliasPlural.";
                    break;
                }
            }
            if (deepCheck)
            {
                /*Check inner objects*/

                foreach (IUserOption userOption in VirtualProperties)
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

