using System.Linq;
using ArchAngel.Interfaces.ITemplate;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace ArchAngel.Providers.Database.Model
{
    
    [Serializable]
    [Interfaces.Attributes.ArchAngelEditor(false, false, "Alias")]
    [DotfuscatorDoNotRename]
    public class ScriptBase : ISerializable, IScriptBase
    {
        #region Fields
        //[DotfuscatorDoNotRename]
        //private new System.Collections.Hashtable HashMemberInfos = new System.Collections.Hashtable();
        [DotfuscatorDoNotRename]
        protected bool _enabled;
        [DotfuscatorDoNotRename]
        protected string _name;
        [DotfuscatorDoNotRename]
        internal bool _isUserDefined;
        [DotfuscatorDoNotRename]
        protected string _alias;
        [DotfuscatorDoNotRename]
        protected string _aliasPlural;
        [DotfuscatorDoNotRename]
        protected List<IUserOption> _userOptions = new List<IUserOption>();
        //[DotfuscatorDoNotRename]
        //protected object _exposedUserOptions;
        [DotfuscatorDoNotRename]
        private Type _MyType;
		[DotfuscatorDoNotRename]
		private string _UniqueId = string.Empty;
        /* SerializationVersion 1: added code to serialize and deserialize Table.Schema property.
         * SerializationVersion 2: added IsCalculated field to column object.
         * SerializationVersion 3: added Precision and Scale fields to the Column and StoredProcedure.Parameter objects.
         * SerializationVersion 4: added Association class.
         * SerializationVersion 5: added Errors property to ScriptObject
         * SerializationVersion 6: added Associations to serialization process for ScriptObjects, Tables, Views and StoredProcedures
         * SerializationVersion 7: added LookupValue class
         * SerializationVersion 8: added Description to IScriptBase
         * SerializationVersion 9: added Lookup class
         */
        [DotfuscatorDoNotRename]
        internal static int SerializationVersion = 9;
        [DotfuscatorDoNotRename]
        protected static bool SerializationVersionExists = true;
        //[NonSerialized]
        //private bool _VirtualPropertiesAreSet = false;
        internal static Type MapColumnType = typeof(MapColumn);
		internal static Dictionary<string, IScriptBase> Lookups = new Dictionary<string, IScriptBase>();
        [DotfuscatorDoNotRename]
        protected string _description;
        #endregion

        #region Constructors
        public ScriptBase()
        {
            _MyType = GetType();
        }

        public ScriptBase(string name, bool isUserDefined)
        {
            _MyType = GetType();
            Name = name;
            Enabled = true;
            _isUserDefined = isUserDefined;

            if (GetType() == typeof(ScriptBase))
            {
                ResetDefaults();
            }
			Lookups.Add(UniqueId, this);
        }

        /// <exclude/>
        public ScriptBase(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {

            throw new NotImplementedException("Why is ScriptBase being deserialized?");
        }

        #endregion

        #region Properties
        public Type MyType
        {
            get
            {
                if (_MyType == null)
                {

                    throw new Exception("Type has not been set in the constructor of this type.");
                }
                return _MyType;
            }

        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _enabled, value);
                _enabled = value;
            }

        }

        public string AliasPlural
        {
            get { return _aliasPlural; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _aliasPlural, value);
                _aliasPlural = value;
            }

        }

        public bool IsUserDefined
        {
            get { return _isUserDefined; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _name, value);
                _name = value;
            }

        }

        public string Description
        {
            get { return _description; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _description, value);
                _description = value;
            }

        }

        public string Alias
        {
            get { return _alias; }
            set
            {
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _alias, value);
                _alias = value;
            }

        }

		public List<IUserOption> Ex
        {
            get
            {
                //if (!VirtualPropertiesAreSet)
                //{
                //    ProjectHelper.MyCurrentProject.PopulateVirtualProperties(this);
                //}
                return _userOptions;
            }
            set
            {                
                _userOptions = value;
				foreach(var val in value)
				{
					val.Owner = this;
				}
                Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _userOptions, value);
            }

        }

		public string UniqueId
		{
			get
			{
				if (string.IsNullOrEmpty(_UniqueId))
				{
					_UniqueId = Guid.NewGuid().ToString();
				}
				return _UniqueId;
			}
			set
			{
				_UniqueId = value;
			}
		}
        #endregion


        #region Functions
		internal static string[] GetUniqueIds(IScriptBase[] objects)
		{
			string[] ids = new string[objects.Length];

			for (int i = 0; i < objects.Length; i++)
			{
				ids[i] = objects[i].UniqueId;
			}
			return ids;
		}

		internal static string[] GetUniqueIds(List<Column> objects)
		{
			string[] ids = new string[objects.Count];

			for (int i = 0; i < objects.Count; i++)
			{
				ids[i] = objects[i].UniqueId;
			}
			return ids;
		}

		[Interfaces.Attributes.ApiExtension]
        public virtual string AliasPluralDefault(IScriptBase scriptBase)
        {
            return ArchAngel.Providers.Database.Helper.Script.GetPlural(scriptBase.Alias);
        }


		[Interfaces.Attributes.ApiExtension]
        public virtual bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
        {
            failReason = "";
            return true;
        }


        /// <exclude/>
        public virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new Exception("Fast Serialization: Why are we here?");
        }

        public virtual void ResetDefaults()
        {
            _alias = AliasDefault(this);
        }


		[Interfaces.Attributes.ApiExtension]
        public virtual string AliasDefault(IScriptBase scriptBase)
        {
            return ArchAngel.Providers.Database.Helper.Script.GetSingular(ArchAngel.Providers.Database.Helper.Script.GetSingleWord(scriptBase.Name.Trim()));
        }


		[Interfaces.Attributes.ApiExtension]
        public virtual bool AliasValidate(IScriptBase scriptBase, out string failReason)
        {
            failReason = "";
            return true;
        }


		[Interfaces.Attributes.ApiExtension]
        public virtual bool Validate(IScriptBase scriptBase, out string failReason)
        {
            failReason = "";
            return true;
        }


		[Interfaces.Attributes.ApiExtension]
        public virtual bool NameValidate(IScriptBase scriptBase, out string failReason)
        {
            failReason = "";
            return true;
        }


        public void AddUserOption(IUserOption userOption)
        {
            Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, _userOptions);
            _userOptions.Add(userOption);
            userOption.Owner = this;
        }
		
		public bool HasUserOption(string name)
		{
			if(Ex.Any(uo => uo.Name == name))
				return true;

			// Virtual Property not found, so this object was probably created and added during the
			// current session, because Virtual Properties are bulk-populated when the project is 
			// initially opened, in Project.FillVirtualProperties(). So now we will populate this object
			// individually.
			Interfaces.SharedData.CurrentProject.PopulateVirtualProperties(this);

			return Ex.Any(uo => uo.Name == name);
		}

		public T GetUserOptionValue<T>(string name)
		{
			object value = GetUserOptionValue(name);
			if(value is T)
				return (T)value;
			return default(T);
		}

    	public object GetUserOptionValue(string name)
        {
			foreach (IUserOption userOption in Ex)
            {
                if (userOption.Name == name)
                {
                    return userOption.Value;
                }
            }
            // Virtual Property not found, so this object was probably created and added during the
            // current session, because Virtual Properties are bulk-populated when the project is 
            // initially opened, in Project.FillVirtualProperties(). So now we will populate this object
            // individually.
            Interfaces.SharedData.CurrentProject.PopulateVirtualProperties(this);

			foreach (IUserOption userOption in Ex)
            {
                if (userOption.Name == name)
                {
                    return userOption.Value;
                }
            }
            throw new MissingMemberException("UserOption not found: " + name);
        }

        /// <summary>
        /// Determines whether the object is in a valid state.
        /// </summary>
        /// <param name="deepCheck"></param>
        /// <param name="failReason"></param>
        /// <returns></returns>
        public virtual bool IsValid(bool deepCheck, out string failReason)
        {
            throw new InvalidOperationException("ScriptBase.IsValid has been called directly - it must always be overridden.");
        }


        #endregion


        //#region Inner Classes
        //public class ScriptObjectTypes
        //{

        //    #region Fields
        //    public static Type Column = typeof(Model.Column);
        //    public static Type Filter = typeof(Model.Filter);
        //    public static Type Index = typeof(Model.Index);
        //    public static Type Key = typeof(Model.Key);
        //    public static Type ManyToManyRelationship = typeof(Model.ManyToManyRelationship);
        //    public static Type ManyToOneRelationship = typeof(Model.ManyToOneRelationship);
        //    public static Type MapColumn = typeof(Model.MapColumn);
        //    public static Type OneToManyRelationship = typeof(Model.OneToManyRelationship);
        //    public static Type OneToOneRelationship = typeof(Model.OneToOneRelationship);
        //    public static Type Relationship = typeof(Model.Relationship);
        //    public static Type ScriptBase = typeof(Model.ScriptBase);
        //    public static Type ScriptObject = typeof(Model.ScriptObject);
        //    public static Type StoredProcedure = typeof(Model.StoredProcedure);
        //    public static Type Table = typeof(Model.Table);
        //    public static Type View = typeof(Model.View);
        //    #endregion

        //}

        //#endregion

    }

}

