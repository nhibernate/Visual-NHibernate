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
    [Interfaces.Attributes.ArchAngelEditor(true, true, "Alias")]
    [DotfuscatorDoNotRename]
    public class LookupValue : ScriptBase, ISerializable
    {
        [DotfuscatorDoNotRename]
        private object _id;
        [DotfuscatorDoNotRename]
        private Lookup _Parent;

        #region Constructors
        public LookupValue()
        {
        }

        public LookupValue(string name, bool isUserDefined, Lookup parent)
            : base(name, isUserDefined)
        {
            _Parent = parent;
            ResetDefaults();
        }

        public LookupValue(string name, bool isUserDefined, object id, string description, Lookup parent)
            : base(name, isUserDefined)
        {
            _id = id;
            _description = description;
            _Parent = parent;
            ResetDefaults();
        }
        #endregion

        /// <exclude/>
        public LookupValue(SerializationInfo serializationInfo, StreamingContext streamingContext)
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
            _id = serializationInfo.GetValue("Id", ModelTypes.Object);
            _description = serializationInfo.GetString("Description");
            _Parent = (Lookup)serializationInfo.GetValue("Parent", ModelTypes.Lookup);
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
            info.AddValue("Parent", _Parent);
        }

        public object Id
        {
            get { return _id; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _id, value);
                _id = value;
            }
        }

        public Lookup Parent
        {
            get { return _Parent; }
        }

        public override bool NameValidate(IScriptBase scriptBase, out string failReason)
        {
            return NameValidate((LookupValue)scriptBase, out failReason);
        }

        [Interfaces.Attributes.ApiExtension]
        public bool AliasDisplayValidate(LookupValue lookupValue, out string failReason)
        {
            failReason = "";
            return true;
        }

        [Interfaces.Attributes.ApiExtension]
        public bool NameValidate(LookupValue lookupValue, out string failReason)
        {
            failReason = "";
            /*Don't check items that are not enabled*/

            if (!lookupValue.Enabled)
            {
                return true;
            }
            if (string.IsNullOrEmpty(lookupValue.Name))
            {
                failReason = "Name cannot be zero-length.";
                return false;
            }
            if (lookupValue.Name.IndexOf(" ") >= 0)
            {
                failReason = "Name cannot have spaces.";
                return false;
            }

            foreach (LookupValue sibling in lookupValue.Parent.LookupValues)
            {
                if (sibling != this && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Name, Name, false))
                {
                    failReason = "Duplicate name: " + Name;
                    return false;
                }
            }
            return true;
        }

        public override string AliasPluralDefault(IScriptBase scriptBase)
        {
            return AliasPluralDefault((LookupValue)scriptBase);
        }

        [Interfaces.Attributes.ApiExtension]
        public virtual string AliasPluralDefault(LookupValue lookupValue)
        {
            return ArchAngel.Providers.Database.Helper.Script.GetPlural(lookupValue.Alias);
        }

        public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
        {
            return AliasPluralValidate((LookupValue)scriptBase, out failReason);
        }


        [Interfaces.Attributes.ApiExtension]
        public bool AliasPluralValidate(LookupValue lookupValue, out string failReason)
        {
            failReason = "";
            /*Don't check items that are not enabled*/

            if (!lookupValue.Enabled)
            {
                return true;
            }
            if (string.IsNullOrEmpty(lookupValue.AliasPlural))
            {
                failReason = "AliasPlural cannot be zero-length.";
                return false;
            }
            if (lookupValue.AliasPlural.IndexOf(" ") >= 0)
            {
                failReason = "AliasPlural cannot have spaces.";
                return false;
            }

            foreach (LookupValue sibling in lookupValue.Parent.LookupValues)
            {
                if (sibling != lookupValue && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.AliasPlural, lookupValue.AliasPlural, false))
                {
                    failReason = "Duplicate AliasPlural: " + lookupValue.AliasPlural;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets whether the LookupValue is in a valid state.
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

            foreach (LookupValue sibling in this.Parent.LookupValues)
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

        public override void ResetDefaults()
        {
            Alias = AliasDefault(this);
            AliasPlural = AliasPluralDefault(this);
        }

    }
}
