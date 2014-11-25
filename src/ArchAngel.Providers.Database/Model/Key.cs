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
	public class Key : ScriptBase, ISerializable
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
		internal List<Column> _referencedColumns = new List<Column>();
		private readonly List<string> _referencedColumnIds;
		[DotfuscatorDoNotRename]
		private Table _referencedTable;
		private readonly string _referencedTableId;
		[DotfuscatorDoNotRename]
		private Key _referencedKey;
		private readonly string _referencedKeyId;
		[DotfuscatorDoNotRename]
		private bool _isReferencedKey;
		private bool BusyResettingColumnOrder;
		#endregion


		#region Constructors
		public Key()
		{
		}

		public Key(string name, bool isUserDefined, string type, ScriptObject parent, bool isReferencedKey)
			: base(name, isUserDefined)
		{
			_isReferencedKey = isReferencedKey;
			_parent = parent;
			_type = type;
			ResetDefaults();
		}

		public Key(string name, bool isUserDefined, bool isReferencedKey)
			: base(name, isUserDefined)
		{
			_isReferencedKey = isReferencedKey;
			ResetDefaults();
		}

		/// <exclude/>
		public Key(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (SerializerHelper.UseFastSerialization)
			{
				_columns = null;
				_referencedColumns = null;

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
					_referencedColumnIds = new List<string>(reader.ReadStringArray());
					_referencedKeyId = reader.ReadString(); // TODO: could be null - is this handled correctly?
					_referencedTableId = reader.ReadString();
					_type = reader.ReadString();
					_userOptions = (List<IUserOption>)reader.ReadObject();

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
				_columns = (List<Column>)serializationInfo.GetValue("Columns", ModelTypes.ColumnList);
				_enabled = serializationInfo.GetBoolean("Enabled");
				//this._exposedUserOptions = serializationInfo.GetValue("ExposedUserOptions", ModelTypes.Object);
				_isUserDefined = serializationInfo.GetBoolean("IsUserDefined");
				_name = serializationInfo.GetString("Name");
				_parent = (ScriptObject)serializationInfo.GetValue("Parent", ModelTypes.ScriptObject);
				_referencedColumns = (List<Column>)serializationInfo.GetValue("ReferencedColumns", ModelTypes.ColumnList);
				_referencedKey = (Key)serializationInfo.GetValue("ReferencedKey", ModelTypes.Key);
				_referencedTable = (Table)serializationInfo.GetValue("ReferencedTable", ModelTypes.Table);
				_type = serializationInfo.GetString("Type");
				_userOptions = (List<IUserOption>)serializationInfo.GetValue("UserOptions", ModelTypes.UserOptionList);

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
				if (!BusyResettingColumnOrder)
				{
					ResetColumnOrder();
				}
				if (_columns == null)
				{
					_columns = new List<Column>();

					foreach (string columnId in _columnIds)
					{
						_columns.Add((Column)Lookups[columnId]);
					}
				}
				if (Interfaces.SharedData.IsBusyGenerating)
				{
					return EnabledColumns;
				}
				return _columns.ToArray();
			}

		}

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

		public Column[] ReferencedColumns
		{
			get
			{
				if (Interfaces.SharedData.IsBusyGenerating)
				{
					return EnabledReferencedColumns;
				}
				if (_referencedColumns == null)
				{
					_referencedColumns = new List<Column>();

					foreach (string referencedColumnId in _referencedColumnIds)
					{
						_referencedColumns.Add((Column)Lookups[referencedColumnId]);
					}
				}
				return _referencedColumns.ToArray();
			}

		}

		public Column[] EnabledReferencedColumns
		{
			get
			{
				if (_referencedColumns == null)
				{
					foreach (string referencedColumnId in _referencedColumnIds)
					{
						_referencedColumns.Add((Column)Lookups[referencedColumnId]);
					}
				}
				List<Column> enabledReferencedColumns = new List<Column>();

				foreach (Column column in _referencedColumns)
				{
					if (column.Enabled)
					{
						enabledReferencedColumns.Add(column);
					}
				}
				return enabledReferencedColumns.ToArray();
			}

		}

		public Table ReferencedTable
		{
			get
			{
				if (_referencedTable == null && _referencedTableId != null)
				{
					_referencedTable = (Table)Lookups[_referencedTableId];
				}
				return _referencedTable; 
			}
			set
			{
				Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _referencedTable, value);
				_referencedTable = value;
			}

		}

		public Key ReferencedKey
		{
			get 
			{
				if (_referencedKey == null && _referencedKeyId != null)
				{
					_referencedKey = (Key)Lookups[_referencedKeyId];
				}
				return _referencedKey; 
			}
			set
			{
				Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _referencedKey, value);
				_referencedKey = value;
			}

		}

		#endregion


		#region Functions
		public override bool NameValidate(IScriptBase scriptBase, out string failReason)
		{
			return NameValidate((Key)scriptBase, out failReason);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool NameValidate(Key key, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!key.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(key.Name))
			{
				failReason = "Name cannot be zero-length.";
				return false;
			}
			if (key.Name.IndexOf(" ") >= 0)
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
				string[] ids;

				writer.Write(UniqueId);
				writer.Write(_alias);
				writer.Write(ScriptBase.GetUniqueIds(Columns));
				writer.Write(_enabled);
				writer.Write(_isUserDefined);
				writer.Write(_name);
				//writer.WriteObject(_parent);
				writer.Write(ScriptBase.GetUniqueIds(ReferencedColumns));

				if (ReferencedKey == null)
				{
					writer.WriteObject(null);
				}
				else
				{
					writer.WriteObject(ReferencedKey.UniqueId);
				}
				if (ReferencedTable == null)
				{
					writer.WriteObject(null);
				}
				else
				{
					writer.WriteObject(ReferencedTable.UniqueId);
				}
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
            info.AddValue("ReferencedColumns", _referencedColumns);
            info.AddValue("ReferencedKey", _referencedKey);
            info.AddValue("ReferencedTable", _referencedTable);
            info.AddValue("Type", _type);
            info.AddValue("UserOptions", _userOptions);
            info.AddValue("Description", _description);
#endif
		}


		public override void ResetDefaults()
		{
			Alias = AliasDefault(this);
			/*Make sure that the columns are in the same order as the parent's columns.*/
			ResetColumnOrder();
		}


		/// <summary>
		/// Synchronises the order of the Key columns to match the order they appear in in the parent.
		/// </summary>
		public void ResetColumnOrder()
		{
			BusyResettingColumnOrder = true;
			try
			{
				if (Columns.Length > 0)
				{
					List<Column> orderedColumns = new List<Column>();

					foreach (Column parentColumn in Parent.Columns)
					{

						foreach (Column keyColumn in Columns)
						{
							if (Utility.StringsAreEqual(keyColumn.Name, parentColumn.Name, false) && Utility.StringsAreEqual(keyColumn.DataType, parentColumn.DataType, false))
							{
								orderedColumns.Add(keyColumn);
							}
						}
					}
					if (_columns.Count != orderedColumns.Count)
					{

						throw new InvalidOperationException("Sanity Check: Column counts are different.");
					}
					_columns = orderedColumns;
				}
			}
			finally
			{
				BusyResettingColumnOrder = false;
			}
		}


		public override string AliasDefault(IScriptBase key)
		{
			return AliasDefault((Key)key);
		}


		[Interfaces.Attributes.ApiExtension]
		public string AliasDefault(Key key)
		{
			if (key._isReferencedKey)
			{
				return ArchAngel.Providers.Database.Helper.Script.GetSingular(ArchAngel.Providers.Database.Helper.Script.GetSingleWord(key.Name.Trim()));
			}
			string keyAlias = key.Type + "_";

			for (int i = 0; i < key.Columns.Length; i++)
			{
				if (i > 0)
				{
					keyAlias += "And";
				}
				keyAlias += key.Columns[i].Name.Trim();
			}
			return ArchAngel.Providers.Database.Helper.Script.GetSingular(keyAlias);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasValidate(Key key, out string failReason)
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


		public void AddReferencedColumn(Column column)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, column);
			_referencedColumns.Add(column);
		}


		public void UpdateReferencedColumn(int index, Column column)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _referencedColumns[index], column);
			_referencedColumns[index] = column;
		}


		[Interfaces.Attributes.ApiExtension]
		public virtual string AliasPluralDefault(Key key)
		{
			return ArchAngel.Providers.Database.Helper.Script.GetPlural(key.Alias);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasPluralValidate(Key key, out string failReason)
		{
			failReason = "";
			return true;
		}


		public override string AliasPluralDefault(IScriptBase scriptBase)
		{
			return AliasPluralDefault((Key)scriptBase);
		}


		public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasPluralValidate((Key)scriptBase, out failReason);
		}


		public override bool AliasValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasValidate((Key)scriptBase, out failReason);
		}

        /// <summary>
        /// Gets whether the Key is in a valid state.
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

