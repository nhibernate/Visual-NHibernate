using ArchAngel.Interfaces.ITemplate;
using Slyce.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ArchAngel.Providers.Database.Model
{
	/// <summary> This represents a table in the object model. It usually corresponds to
	/// a real table in the database from which the schema is derived, however it can
	/// also be a virtual table created by the end-user in ArchAngel Workbench.
	/// </summary>
	[Serializable]
	[Interfaces.Attributes.ArchAngelEditor(true, true, "Alias")]
	[DotfuscatorDoNotRename]
	public class Table : ScriptObject, ISerializable
	{

		#region Fields
		[DotfuscatorDoNotRename]
		public static List<string> TablePrefixes = new List<string>();
		[DotfuscatorDoNotRename]
		internal List<Index> _indexes = new List<Index>();
		[DotfuscatorDoNotRename]
		internal List<Key> _keys = new List<Key>();
		#endregion


		#region Constructors
		/// <summary>
		/// TODO: I don't think this should be exposed to the user???
		/// </summary>
		public Table()
		{
			_IsTable = true;
		}

		/// <summary>
		/// TODO: I don't think this should be exposed to the user???
		/// </summary>
		/// <param name="name">Actual name of the table.</param>
		/// <param name="isUserDefined">Whether the table is real or virtual. Virtual tables do not exist in the database and are defined by the user.</param>
		public Table(string name, bool isUserDefined)
			: base(name, isUserDefined)
		{
			_IsTable = true;
			ResetDefaults();
		}

		/// <exclude/>
		/// <summary>
		/// TODO: I don't think this should be exposed to the user???
		/// </summary>
		/// <param name="serializationInfo"></param>
		/// <param name="streamingContext"></param>
		public Table(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (SerializerHelper.UseFastSerialization)
			{
				using (SerializationReader reader = new SerializationReader((byte[])serializationInfo.GetValue("d", typeof(byte[]))))
				{
					UniqueId = reader.ReadString();
					Lookups.Add(UniqueId, this);
					_alias = reader.ReadString();
					_aliasPlural = reader.ReadString();
					_columns = (List<Column>)reader.ReadObject();
					// TODO: Database
					_enabled = reader.ReadBoolean();
					_filters = (List<Filter>)reader.ReadObject();
					_indexes = (List<Index>)reader.ReadObject();
					_isUserDefined = reader.ReadBoolean();
					_keys = (List<Key>)reader.ReadObject();
					_manyToManyRelationships = (List<ManyToManyRelationship>)reader.ReadObject();
					_manyToOneRelationships = (List<ManyToOneRelationship>)reader.ReadObject();
					_name = reader.ReadString();
					_oneToManyRelationships = (List<OneToManyRelationship>)reader.ReadObject();
					_oneToOneRelationships = (List<OneToOneRelationship>)reader.ReadObject();
					_userOptions = (List<IUserOption>)reader.ReadObject();
					_Schema = reader.ReadString();
					_Associations = (List<Association>)reader.ReadObject();

					foreach (Column column in _columns)
					{
						column.Parent = this;
					}
					foreach (Filter filter in _filters)
					{
						filter.Parent = this;
					}
					foreach (Index index in _indexes)
					{
						index.Parent = this;
					}
					foreach (Key key in _keys)
					{
						key.Parent = this;
					}
					foreach (Association association in _Associations)
					{
						association.PrimaryObject = this;
					}
					foreach (ManyToManyRelationship relationship in _manyToManyRelationships)
					{
						relationship.Parent = this;
					}
					foreach (ManyToOneRelationship relationship in _manyToOneRelationships)
					{
						relationship.Parent = this;
					}
					foreach (OneToManyRelationship relationship in _oneToManyRelationships)
					{
						relationship.Parent = this;
					}
					foreach (OneToOneRelationship relationship in _oneToOneRelationships)
					{
						relationship.Parent = this;
					}
					for (int i = 0; i < _userOptions.Count; i++)
					{
						_userOptions[i].Owner = this;
					}
				}
			}
			else
			{
				_IsTable = true;

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
				_columns = (List<Column>)serializationInfo.GetValue("Columns", ModelTypes.ColumnList);
				_database = (Database)serializationInfo.GetValue("Database", ModelTypes.Database);
				_enabled = serializationInfo.GetBoolean("Enabled");
				//_exposedUserOptions = serializationInfo.GetValue("ExposedUserOptions", ModelTypes.Object);
				_filters = (List<Filter>)serializationInfo.GetValue("Filters", ModelTypes.FilterList);
				_indexes = (List<Index>)serializationInfo.GetValue("Indexes", ModelTypes.IndexList);
				_isUserDefined = serializationInfo.GetBoolean("IsUserDefined");
				_keys = (List<Key>)serializationInfo.GetValue("Keys", ModelTypes.KeyList);
				_manyToManyRelationships = (List<ManyToManyRelationship>)serializationInfo.GetValue("ManyToManyRelationships", ModelTypes.ManyToManyRelationshipList);
				_manyToOneRelationships = (List<ManyToOneRelationship>)serializationInfo.GetValue("ManyToOneRelationships", ModelTypes.ManyToOneRelationshipList);
				_name = serializationInfo.GetString("Name");
				_oneToManyRelationships = (List<OneToManyRelationship>)serializationInfo.GetValue("OneToManyRelationships", ModelTypes.OneToManyRelationshipList);
				_oneToOneRelationships = (List<OneToOneRelationship>)serializationInfo.GetValue("OneToOneRelationships", ModelTypes.OneToOneRelationshipList);
				_userOptions = (List<IUserOption>)serializationInfo.GetValue("UserOptions", ModelTypes.UserOptionList);

				if (version > 0)
				{
					_Schema = serializationInfo.GetString("Schema");

					if (version > 4)
					{
						_Errors = (List<string>)serializationInfo.GetValue("Errors", typeof(List<string>));

						if (version >= 6)
						{
							_Associations = (List<Association>)serializationInfo.GetValue("Associations", typeof(List<Association>));

                            if (version >= 8)
                            {
                                _description = serializationInfo.GetString("Description");
                            }
						}
					}
				}
			}
		}

		#endregion

		#region Properties
		/// <summary>
		/// Collection of indexes for the table.
		/// </summary>
		public Index[] Indexes
		{
			get
			{
				if (Interfaces.SharedData.IsBusyGenerating)
				{
					return EnabledIndexes;
				}
				return _indexes.ToArray();
			}
		}

		/// <summary>
		/// Collection of enabled indexes for the table. Enabled indexes are those that the user has selected.
		/// </summary>
		public Index[] EnabledIndexes
		{
			get
			{
				List<Index> enabledIndexes = new List<Index>();

				foreach (Index index in _indexes)
				{
					if (index.Enabled)
					{
						enabledIndexes.Add(index);
					}
				}
				return enabledIndexes.ToArray();
			}

		}

		/// <summary>
		/// Collection of all keys that belong to the table. This includes primary and foreign keys.
		/// </summary>
		public Key[] Keys
		{
			get
			{
				if (Interfaces.SharedData.IsBusyGenerating)
				{
					return EnabledKeys;
				}
				return _keys.ToArray();
			}
		}

		/// <summary>
		/// Collection of all enabled keys that belong to the table. Enabled keys are those that the user has selected. This includes primary and foreign keys.
		/// </summary>
		public Key[] EnabledKeys
		{
			get
			{
				List<Key> enabledKeys = new List<Key>();

				foreach (Key key in _keys)
				{
					if (key.Enabled)
					{
						enabledKeys.Add(key);
					}
				}
				return enabledKeys.ToArray();
			}

		}

		/// <summary>
		/// Collection of all primary keys that belong to the table.
		/// </summary>
		public Column[] PrimaryKeyColumns
		{
			get
			{
				if (Interfaces.SharedData.IsBusyGenerating)
				{
					return EnabledPrimaryKeyColumns;
				}
				List<Column> primaryKeyColumns = new List<Column>();

				foreach (Column column in Columns)
				{
					if (column.InPrimaryKey)
					{
						primaryKeyColumns.Add(column);
					}
				}
				return primaryKeyColumns.ToArray();
			}

		}

		/// <summary>
		/// Collection of all enabled primary keys that belong to the table. Enabled primary keys are those that the user has selected.
		/// </summary>
		/// <value>The enabled primary key columns.</value>
		public Column[] EnabledPrimaryKeyColumns
		{
			get
			{
				List<Column> enabledPrimaryKeyColumns = new List<Column>();

				foreach (Column column in Columns)
				{
					if (column.InPrimaryKey && column.Enabled)
					{
						enabledPrimaryKeyColumns.Add(column);
					}
				}
				return enabledPrimaryKeyColumns.ToArray();
			}

		}

		#endregion

		#region Functions
		/// <exclude/>
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
#if FAST_SERIALIZATION
			using (SerializationWriter writer = new SerializationWriter())
			{
				_ColumnsSortedByName = null;
				writer.Write(UniqueId);
				writer.Write(_alias);
				writer.Write(_aliasPlural);
				writer.WriteObject(_columns);
				//writer.WriteObject(_database);
				writer.Write(_enabled);
				//writer.Write(_exposedUserOptions);
				writer.WriteObject(_filters);
				writer.WriteObject(_indexes);
				writer.Write(_isUserDefined);
				writer.WriteObject(_keys);
				writer.WriteObject(_manyToManyRelationships);
				writer.WriteObject(_manyToOneRelationships);
				writer.Write(_name);
				writer.WriteObject(_oneToManyRelationships);
				writer.WriteObject(_oneToOneRelationships);
				writer.WriteObject(_userOptions);
				writer.Write(_Schema);
				writer.WriteObject(_Associations);
				//writer.Write(RelationshipStores);
				info.AddValue("d", writer.ToArray());
			}
#else
            info.AddValue("SerializationVersion", SerializationVersion);
            info.AddValue("Alias", _alias);
            info.AddValue("AliasPlural", _aliasPlural);
            info.AddValue("Columns", _columns);
            info.AddValue("Database", _database);
            info.AddValue("Enabled", _enabled);
			//info.AddValue("ExposedUserOptions", _exposedUserOptions);
            info.AddValue("Filters", _filters);
            info.AddValue("Indexes", _indexes);
            info.AddValue("IsUserDefined", _isUserDefined);
            info.AddValue("Keys", _keys);
            info.AddValue("ManyToManyRelationships", _manyToManyRelationships);
            info.AddValue("ManyToOneRelationships", _manyToOneRelationships);
            info.AddValue("Name", _name);
            info.AddValue("OneToManyRelationships", _oneToManyRelationships);
            info.AddValue("OneToOneRelationships", _oneToOneRelationships);
            info.AddValue("UserOptions", _userOptions);
            info.AddValue("Schema", _Schema);
            info.AddValue("Errors", _Errors);
            info.AddValue("Associations", _Associations);
            info.AddValue("Description", _description);
#endif
		}


		public override void ResetDefaults()
		{
			Alias = AliasDefault(this);
			AliasPlural = AliasPluralDefault(this);
		}


		public override string AliasDefault(IScriptBase table)
		{
			return AliasDefault((Table)table);
		}


		[Interfaces.Attributes.ApiExtension]
		public string AliasDefault(Table table)
		{
			string alias = ArchAngel.Providers.Database.Helper.Script.GetSingleWord(table.Name.Trim());
			string aliasLower = alias.ToLower();

			foreach (string prefix in TablePrefixes)
			{
				if (aliasLower.IndexOf(prefix.ToLower()) == 0)
				{
					alias = alias.Substring(prefix.Length);
					break;
				}
			}
			return ArchAngel.Providers.Database.Helper.Script.GetSingular(alias).Replace("_", "");
		}


		[Interfaces.Attributes.ApiExtension]
		public virtual string AliasPluralDefault(Table table)
		{
			return ArchAngel.Providers.Database.Helper.Script.GetPlural(table.Alias);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasValidate(Table table, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!table.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(table.Alias))
			{
				failReason = "Alias cannot be zero-length.";
				return false;
			}
			if (table.Alias.IndexOf(" ") >= 0)
			{
				failReason = "Alias cannot have spaces.";
				return false;
			}

			foreach (ScriptObject sibling in table.Database.EnabledScriptObjects)
			{
				if (sibling != table && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Alias, table.Alias, false))
				{
					failReason = "Duplicate alias: " + table.Alias;
					return false;
				}
			}
			return true;
		}


		[Interfaces.Attributes.ApiExtension]
		public bool Validate(Table table, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!table.Enabled)
			{
				return true;
			}
			if (!table.IsUserDefined && table.PrimaryKeyColumns.Length == 0)
			{
				failReason = "No primary key exists.";
				return false;
			}
			return true;
		}


		public override string AliasPluralDefault(IScriptBase scriptBase)
		{
			return AliasPluralDefault((Table)scriptBase);
		}


		public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasPluralValidate((Table)scriptBase, out failReason);
		}


		public override bool AliasValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasValidate((Table)scriptBase, out failReason);
		}


		public override string AliasDefault(ScriptObject scriptObject)
		{
			return AliasDefault((Table)scriptObject);
		}


		public override string AliasPluralDefault(ScriptObject scriptObject)
		{
			return AliasPluralDefault((Table)scriptObject);
		}


		public override bool AliasPluralValidate(ScriptObject scriptObject, out string failReason)
		{
			return AliasPluralValidate((Table)scriptObject, out failReason);
		}


		public override bool AliasValidate(ScriptObject scriptObject, out string failReason)
		{
			return AliasValidate((Table)scriptObject, out failReason);
		}


		public override bool Validate(ScriptObject scriptObject, out string failReason)
		{
			return Validate((Table)scriptObject, out failReason);
		}


		public override bool NameValidate(IScriptBase scriptBase, out string failReason)
		{
			return NameValidate((Table)scriptBase, out failReason);
		}


		public override bool NameValidate(ScriptObject scriptObject, out string failReason)
		{
			return NameValidate((Table)scriptObject, out failReason);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool NameValidate(Table table, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!table.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(table.Name))
			{
				failReason = "Name cannot be zero-length.";
				return false;
			}
			if (table.Name.IndexOf(" ") >= 0)
			{
				failReason = "Name cannot have spaces.";
				return false;
			}

			foreach (ScriptObject sibling in table.Database.EnabledScriptObjects)
			{
				if (sibling != this && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Name, Name, false))
				{
					failReason = "Duplicate name: " + Name;
					return false;
				}
			}
			return true;
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasPluralValidate(Table table, out string failReason)
		{
			failReason = "";

			/*Don't check items that are not enabled*/
			if (!table.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(table.AliasPlural))
			{
				failReason = "AliasPlural cannot be zero-length.";
				return false;
			}
			if (table.AliasPlural.IndexOf(" ") >= 0)
			{
				failReason = "AliasPlural cannot have spaces.";
				return false;
			}
			if (ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(table.AliasPlural, table.Alias, false))
			{
				failReason = "AliasPlural must be different to Alias.";
				return false;
			}

            foreach (ScriptObject sibling in table.Database.EnabledScriptObjects)
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
						failReason = string.Format("Table with same name exists: {0}", sibling.Alias);
						return false;
					}
				}
			}
			return true;
		}


		/// <summary>
		/// TODO: I don't think this should be exposed to the user???
		/// </summary>
		/// <param name="index"></param>
		public void AddIndex(Index index)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, index);
			_indexes.Add(index);
		}


		/// <summary>
		/// TODO: I don't think this should be exposed to the user???
		/// </summary>
		/// <param name="indexOf"></param>
		/// <param name="index"></param>
		public void UpdateIndex(int indexOf, Index index)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _indexes[indexOf], index);
			_indexes[indexOf] = index;
		}


		/// <summary>
		/// TODO: I don't think this should be exposed to the user???
		/// </summary>
		/// <param name="index"></param>
		public void RemoveIndex(Index index)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), index, null);
			_indexes.Remove(index);
		}


		/// <summary>
		/// TODO: I don't think this should be exposed to the user???
		/// </summary>
		/// <param name="key"></param>
		public void AddKey(Key key)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, key);
			_keys.Add(key);
		}


		/// <summary>
		/// TODO: I don't think this should be exposed to the user???
		/// </summary>
		/// <param name="index"></param>
		/// <param name="key"></param>
		public void UpdateKey(int index, Key key)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _keys[index], key);
			_keys[index] = key;
		}


		/// <summary>
		/// TODO: I don't think this should be exposed to the user???
		/// </summary>
		/// <param name="key"></param>
		public void RemoveKey(Key key)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), key, null);
			_keys.Remove(key);
		}

        /// <summary>
        /// Gets whether the Table is in a valid state.
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
			if (!Validate(this, out tempFailReason))
			{
				isValid = false;
				failReason += string.Format("{1}: {0}\n", tempFailReason, Name);
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

				foreach (Index index in Indexes)
				{
					if (!index.IsValid(deepCheck, out tempFailReason))
					{
						isValid = false;
						failReason += string.Format("{0}.Index: {1}\n", Name, tempFailReason);
					}
				}

				foreach (Key key in Keys)
				{
					if (!key.IsValid(deepCheck, out tempFailReason))
					{
						isValid = false;
						failReason += string.Format("{0}.Key: {1}\n", Name, tempFailReason);
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


		#endregion

	}

}

