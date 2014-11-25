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
	public class Index : ScriptBase, ISerializable
	{

		#region Fields
		[DotfuscatorDoNotRename]
		internal ScriptObject _parent;
		[DotfuscatorDoNotRename]
		private string _type;
		[DotfuscatorDoNotRename]
		internal List<Column> _columns = new List<Column>();
		private readonly string[] _columnIds;
		[DotfuscatorDoNotRename]
		internal bool _isClustered;
		[DotfuscatorDoNotRename]
		internal bool _isUnique;
		#endregion


		#region Constructors
		public Index()
		{
		}

		public Index(string name, bool isUserDefined, string type, ScriptObject parent, bool isUnique, bool isClustered)
			: base(name, isUserDefined)
		{
			_parent = parent;
			_type = type;
			_isUnique = isUnique;
			_isClustered = isClustered;
			ResetDefaults();
		}

		public Index(string name, bool isUserDefined)
			: base(name, isUserDefined)
		{
			ResetDefaults();
		}

		/// <exclude/>
		public Index(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (SerializerHelper.UseFastSerialization)
			{
				_columns = null;

				using (SerializationReader reader = new SerializationReader((byte[])serializationInfo.GetValue("d", typeof(byte[]))))
				{
					UniqueId = reader.ReadString();
					Lookups.Add(UniqueId, this);
					_alias = reader.ReadString();
					_columnIds = reader.ReadStringArray();
					_enabled = reader.ReadBoolean();
					_isUserDefined = reader.ReadBoolean();
					_name = reader.ReadString();
					// TODO: Parent
					_type = reader.ReadString();
					_userOptions = (List<IUserOption>)reader.ReadObject();
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
				_columns = (List<Column>)serializationInfo.GetValue("Columns", ModelTypes.ColumnList);
				_enabled = serializationInfo.GetBoolean("Enabled");
				//this._exposedUserOptions = serializationInfo.GetValue("ExposedUserOptions", ModelTypes.Object);
				_isUserDefined = serializationInfo.GetBoolean("IsUserDefined");
				_name = serializationInfo.GetString("Name");
				_parent = (ScriptObject)serializationInfo.GetValue("Parent", ModelTypes.ScriptObject);
				_type = serializationInfo.GetString("Type");
				_userOptions = (List<IUserOption>)serializationInfo.GetValue("UserOptions", ModelTypes.UserOptionList);

				for (int i = 0; i < _userOptions.Count; i++)
				{
					_userOptions[i].Owner = this;
				}
                if (version >= 8)
                {
                    _description = serializationInfo.GetString("Description");
                }
			}
		}

		#endregion

		#region Properties
		public ScriptObject Parent
		{
			get { return _parent; }
			internal set { _parent = value; } // This should only get called from the scriptobject serialization constructor (table, view, storedprocedure)
		}

		public bool IsClustered
		{
			get { return _isClustered; }
		}

		public bool IsUnique
		{
			get { return _isUnique; }
		}

		public string Type
		{
			get { return _type; }
			set
			{
				Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _type, value);
				_type = value;
			}

		}

		public Column[] Columns
		{
			get
			{
				if (Interfaces.SharedData.IsBusyGenerating)
				{
					return EnabledColumns;
				}
				if (_columns == null)
				{
					_columns = new List<Column>();

					foreach (string columnId in _columnIds)
					{
						_columns.Add((Column)Lookups[columnId]);
					}
				}
				return _columns.ToArray();
			}

		}

		public Column[] EnabledColumns
		{
			get
			{
				if (_columns == null)
				{
					foreach (string columnId in _columnIds)
					{
						_columns.Add((Column)Lookups[columnId]);
					}
				}
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

		#endregion


		#region Functions
		public override bool NameValidate(IScriptBase scriptBase, out string failReason)
		{
			return NameValidate((Index)scriptBase, out failReason);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool NameValidate(Index index, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!index.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(index.Name))
			{
				failReason = "Name cannot be zero-length.";
				return false;
			}
			if (index.Name.IndexOf(" ") >= 0)
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
			using (SerializationWriter writer = new SerializationWriter())
			{
				writer.Write(UniqueId);
				writer.Write(_alias);
				//writer.Write(_columns);
				writer.Write(ScriptBase.GetUniqueIds(Columns));
				writer.Write(_enabled);
				//writer.WriteObject(_exposedUserOptions);
				writer.Write(_isUserDefined);
				writer.Write(_name);
				//writer.WriteObject(_parent);
				writer.Write(_type);
				writer.WriteObject(_userOptions);
				info.AddValue("d", writer.ToArray());
			}
#else
            info.AddValue("SerializationVersion", SerializationVersion);
            info.AddValue("Alias", _alias);
            info.AddValue("Columns", _columns);
            info.AddValue("Enabled", _enabled);
			//info.AddValue("ExposedUserOptions", this._exposedUserOptions);
            info.AddValue("IsUserDefined", _isUserDefined);
            info.AddValue("Name", _name);
            info.AddValue("Parent", _parent);
            info.AddValue("Type", _type);
            info.AddValue("UserOptions", _userOptions);
            info.AddValue("Description", _description);
#endif
		}


		public override void ResetDefaults()
		{
			Alias = AliasDefault(this);
		}


		public override string AliasDefault(IScriptBase index)
		{
			return AliasDefault((Index)index);
		}


		[Interfaces.Attributes.ApiExtension]
		public string AliasDefault(Index index)
		{
			return index.Name.Trim();
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasValidate(Index index, out string failReason)
		{
			failReason = "";
			return true;
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


		[Interfaces.Attributes.ApiExtension]
		public virtual string AliasPluralDefault(Index index)
		{
			return ArchAngel.Providers.Database.Helper.Script.GetPlural(index.Alias);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasPluralValidate(Index index, out string failReason)
		{
			failReason = "";
			return true;
		}


		public override string AliasPluralDefault(IScriptBase scriptBase)
		{
			return AliasPluralDefault((Index)scriptBase);
		}


		public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasPluralValidate((Index)scriptBase, out failReason);
		}


		public override bool AliasValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasValidate((Index)scriptBase, out failReason);
		}

        /// <summary>
        /// Gets whether the Index is in a valid state.
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

