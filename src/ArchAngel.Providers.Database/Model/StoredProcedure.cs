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
	public class StoredProcedure : ScriptObject, ISerializable
	{

		#region Fields
		[DotfuscatorDoNotRename]
		public static List<string> StoredProcedurePrefixes = new List<string>();
		[DotfuscatorDoNotRename]
		internal List<Parameter> _parameters = new List<Parameter>();
		#endregion


		#region Constructors
		public StoredProcedure()
		{
			_IsStoredProcedure = true;
		}

		public StoredProcedure(string name, bool isUserDefined)
			: base(name, isUserDefined)
		{
			_IsStoredProcedure = true;
			ResetDefaults();
		}

		/// <exclude/>
		public StoredProcedure(SerializationInfo serializationInfo, StreamingContext streamingContext)
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
					_isUserDefined = reader.ReadBoolean();
					_manyToManyRelationships = (List<ManyToManyRelationship>)reader.ReadObject();
					_manyToOneRelationships = (List<ManyToOneRelationship>)reader.ReadObject();
					_name = reader.ReadString();
					_oneToManyRelationships = (List<OneToManyRelationship>)reader.ReadObject();
					_oneToOneRelationships = (List<OneToOneRelationship>)reader.ReadObject();
					_parameters = (List<Parameter>)reader.ReadObject();
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
				_IsStoredProcedure = true;
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
				_isUserDefined = serializationInfo.GetBoolean("IsUserDefined");
				_manyToManyRelationships = (List<ManyToManyRelationship>)serializationInfo.GetValue("ManyToManyRelationships", ModelTypes.ManyToManyRelationshipList);
				_manyToOneRelationships = (List<ManyToOneRelationship>)serializationInfo.GetValue("ManyToOneRelationships", ModelTypes.ManyToOneRelationshipList);
				_name = serializationInfo.GetString("Name");
				_oneToManyRelationships = (List<OneToManyRelationship>)serializationInfo.GetValue("OneToManyRelationships", ModelTypes.OneToManyRelationshipList);
				_oneToOneRelationships = (List<OneToOneRelationship>)serializationInfo.GetValue("OneToOneRelationships", ModelTypes.OneToOneRelationshipList);
				_parameters = (List<Parameter>)serializationInfo.GetValue("Parameters", ModelTypes.ParameterList);
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
        /// Collection of parameters for this StoredProcedure object.
        /// </summary>
		public Parameter[] Parameters
		{
			get { return _parameters.ToArray(); }
		}

        /// <summary>
        /// Collection of parameters for this StoredProcedure object whose direction is 'IN' or INOUT'.
        /// </summary>
        public List<Parameter> OutParameters
        {
            get
            {
                // TODO: look at caching these. Can only do this once we have implemented collection types that 
                // notify us of additions, insertions and modifications otherwise this collection will become out-of-date.
                List<Parameter> outParameters = new List<Parameter>();

                foreach (Parameter parameter in _parameters)
                {
                    if (parameter.Direction.ToLower().IndexOf("out") >= 0)
                    {
                        outParameters.Add(parameter);
                    }
                }
                return outParameters;
            }
        }

		public string SQLParameters
		{
			get
			{
				string sqlParms = "";

				for (int i = 0; i < _parameters.Count; i++)
				{
					sqlParms += "null";

					if (i < _parameters.Count)
					{
						sqlParms += ",";
					}
				}
				return sqlParms;
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
				int i = _database.StoredProcedures.Length;
				writer.Write(UniqueId);
				writer.Write(_alias);
				writer.Write(_aliasPlural);
				writer.WriteObject(_columns);
				//writer.WriteObject(_database);
				writer.Write(_enabled);
				writer.WriteObject(_filters);
				writer.Write(_isUserDefined);
				writer.WriteObject(_manyToManyRelationships);
				writer.WriteObject(_manyToOneRelationships);
				writer.Write(_name);
				writer.WriteObject(_oneToManyRelationships);
				writer.WriteObject(_oneToOneRelationships);
				writer.WriteObject(_parameters);
				writer.WriteObject(_userOptions);
				writer.Write(_Schema);
				writer.WriteObject(_Associations);
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
            info.AddValue("IsUserDefined", _isUserDefined);
            info.AddValue("ManyToManyRelationships", _manyToManyRelationships);
            info.AddValue("ManyToOneRelationships", _manyToOneRelationships);
            info.AddValue("Name", _name);
            info.AddValue("OneToManyRelationships", _oneToManyRelationships);
            info.AddValue("OneToOneRelationships", _oneToOneRelationships);
            info.AddValue("Parameters", _parameters);
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


		public override string AliasDefault(IScriptBase storedProcedure)
		{
			return AliasDefault((StoredProcedure)storedProcedure);
		}


		[Interfaces.Attributes.ApiExtension]
		public string AliasDefault(StoredProcedure storedProcedure)
		{
			string alias = ArchAngel.Providers.Database.Helper.Script.GetSingleWord(storedProcedure.Name.Trim());
			string aliasLower = alias.ToLower();

			foreach (string prefix in StoredProcedurePrefixes)
			{
				if (aliasLower.IndexOf(prefix.ToLower()) == 0)
				{
					alias = alias.Substring(prefix.Length);
					break;
				}
			}
			return ArchAngel.Providers.Database.Helper.Script.GetSingular(alias);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasValidate(StoredProcedure storedProcedure, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!storedProcedure.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(storedProcedure.Alias))
			{
				failReason = "Alias cannot be zero-length.";
				return false;
			}
			if (storedProcedure.Alias.IndexOf(" ") >= 0)
			{
				failReason = "Alias cannot have spaces.";
				return false;
			}

			foreach (ScriptObject sibling in storedProcedure.Database.EnabledScriptObjects)
			{
				if (sibling != storedProcedure && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Alias, storedProcedure.Alias, false))
				{
					failReason = "Duplicate alias: " + storedProcedure.Alias;
					return false;
				}
			}
			return true;
		}


		[Interfaces.Attributes.ApiExtension]
		public bool NameValidate(StoredProcedure storedProcedure, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!storedProcedure.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(storedProcedure.Name))
			{
				failReason = "Name cannot be zero-length.";
				return false;
			}
			if (storedProcedure.Name.IndexOf(" ") >= 0)
			{
				failReason = "Name cannot have spaces.";
				return false;
			}

			foreach (ScriptObject sibling in storedProcedure.Database.EnabledScriptObjects)
			{
				if (sibling != this && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Name, Name, false))
				{
					failReason = "Duplicate name: " + Name;
					return false;
				}
			}
			return true;
		}


		public void AddParameter(Parameter parameter)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, parameter);
			_parameters.Add(parameter);
		}


		public void UpdateParameter(int index, Parameter parameter)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _parameters[index], parameter);
			_parameters[index] = parameter;
		}


		[Interfaces.Attributes.ApiExtension]
		public virtual string AliasPluralDefault(StoredProcedure storedProcedure)
		{
			return ArchAngel.Providers.Database.Helper.Script.GetPlural(storedProcedure.Alias);
		}


		public override bool NameValidate(IScriptBase scriptBase, out string failReason)
		{
			return NameValidate((StoredProcedure)scriptBase, out failReason);
		}


		public override bool NameValidate(ScriptObject scriptObject, out string failReason)
		{
			return NameValidate((StoredProcedure)scriptObject, out failReason);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasPluralValidate(StoredProcedure storedProcedure, out string failReason)
		{
			failReason = "";

			/*Don't check items that are not enabled*/
			if (!storedProcedure.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(storedProcedure.AliasPlural))
			{
				failReason = "AliasPlural cannot be zero-length.";
				return false;
			}
			if (storedProcedure.AliasPlural.IndexOf(" ") >= 0)
			{
				failReason = "AliasPlural cannot have spaces.";
				return false;
			}
			if (ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(storedProcedure.AliasPlural, storedProcedure.Alias, false))
			{
				failReason = "AliasPlural must be different to Alias.";
				return false;
			}

            foreach (ScriptObject sibling in storedProcedure.Database.EnabledScriptObjects)
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
						failReason = string.Format("Stored procedure with same name exists: {0}", sibling.Alias);
						return false;
					}
				}
			}
			return true;
		}


		public override string AliasPluralDefault(IScriptBase scriptBase)
		{
			return AliasPluralDefault((StoredProcedure)scriptBase);
		}


		public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasPluralValidate((StoredProcedure)scriptBase, out failReason);
		}


		public override bool AliasValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasValidate((StoredProcedure)scriptBase, out failReason);
		}


		public override string AliasDefault(ScriptObject scriptObject)
		{
			return AliasDefault((StoredProcedure)scriptObject);
		}


		public override string AliasPluralDefault(ScriptObject scriptObject)
		{
			return AliasPluralDefault((StoredProcedure)scriptObject);
		}


		public override bool AliasPluralValidate(ScriptObject scriptObject, out string failReason)
		{
			return AliasPluralValidate((StoredProcedure)scriptObject, out failReason);
		}


		public override bool AliasValidate(ScriptObject scriptObject, out string failReason)
		{
			return AliasValidate((StoredProcedure)scriptObject, out failReason);
		}

        /// <summary>
        /// Gets whether the StoredProcedure is in a valid state.
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

				foreach (Parameter parameter in Parameters)
				{
					if (!parameter.IsValid(deepCheck, out tempFailReason))
					{
						isValid = false;
						failReason += string.Format("{0}.Parameter: {1}\n", Name, tempFailReason);
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


		#region Inner Classes
		[Serializable]
        [Interfaces.Attributes.ArchAngelEditor(false, false, "Alias")]
		[DotfuscatorDoNotRename]
		public class Parameter : ScriptBase, ISerializable
		{

			#region Fields
			[DotfuscatorDoNotRename]
			private string _dataType;
			[DotfuscatorDoNotRename]
			private string _direction;
			[DotfuscatorDoNotRename]
			private int _ordinalPosition;
			[DotfuscatorDoNotRename]
			private int _characterMaximumLength;
			[DotfuscatorDoNotRename]
			private int _precision;
			[DotfuscatorDoNotRename]
			private int _scale;
			#endregion


			#region Constructors
			public Parameter()
			{
			}

			public Parameter(string name, string dataType, string direction, int ordinalPosition, int characterMaximumLength, int precision, int scale)
				: base(name, false)
			{
				_dataType = dataType;
				_direction = direction;
				_ordinalPosition = ordinalPosition;
				_characterMaximumLength = characterMaximumLength;
				_precision = precision;
				_scale = scale;
				ResetDefaults();
			}

			public Parameter(SerializationInfo serializationInfo, StreamingContext streamingContext)
			{
				if (SerializerHelper.UseFastSerialization)
				{
					using (SerializationReader reader = new SerializationReader((byte[])serializationInfo.GetValue("d", typeof(byte[]))))
					{
						_alias = reader.ReadString();
						_characterMaximumLength = reader.ReadInt32();
						_dataType = reader.ReadString();
						_direction = reader.ReadString();
						_enabled = reader.ReadBoolean();
						_isUserDefined = reader.ReadBoolean();
						_name = reader.ReadString();
						_ordinalPosition = reader.ReadInt32();
						_userOptions = (List<IUserOption>)reader.ReadObject();
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
					_characterMaximumLength = serializationInfo.GetInt32("CharacterMaximumLength");
					_dataType = serializationInfo.GetString("DataType");
					_direction = serializationInfo.GetString("Direction");
					_enabled = serializationInfo.GetBoolean("Enabled");
					//_exposedUserOptions = serializationInfo.GetValue("ExposedUserOptions", ModelTypes.Object);
					_isUserDefined = serializationInfo.GetBoolean("IsUserDefined");
					_name = serializationInfo.GetString("Name");
					_ordinalPosition = serializationInfo.GetInt32("OrdinalPosition");
					_userOptions = (List<IUserOption>)serializationInfo.GetValue("UserOptions", ModelTypes.UserOptionList);

					if (version >= 3)
					{
						_precision = serializationInfo.GetInt32("Precision");
						_scale = serializationInfo.GetInt32("Scale");
					}
				}
			}

			#endregion

			#region Properties
			public string DataType
			{
				get { return _dataType; }
			}

			public string Direction
			{
				get { return _direction; }
			}

			public int OrdinalPosition
			{
				get { return _ordinalPosition; }
			}

			public int Size
			{
				get { return _characterMaximumLength; }
			}

			public int Precision
			{
				get { return _precision; }
			}

			public int Scale
			{
				get { return _scale; }
			}
			#endregion


			#region Functions
			public override bool IsValid(bool deepCheck, out string failReason)
			{
				bool isValid = true;
				failReason = "";
				string tempFailReason;
				/*Properties*/

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
				return isValid;
			}


			public override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
#if FAST_SERIALIZATION
				using (SerializationWriter writer = new SerializationWriter())
				{
					writer.Write(_alias);
					writer.Write(_characterMaximumLength);
					writer.Write(_dataType);
					writer.Write(_direction);
					writer.Write(_enabled);
					writer.Write(_isUserDefined);
					writer.Write(_name);
					writer.Write(_ordinalPosition);
					writer.WriteObject(_userOptions);
					writer.Write(_precision);
					writer.Write(_scale);
					info.AddValue("d", writer.ToArray());
				}
#else
                info.AddValue("SerializationVersion", SerializationVersion);
                info.AddValue("Alias", _alias);
                info.AddValue("CharacterMaximumLength", _characterMaximumLength);
                info.AddValue("DataType", _dataType);
                info.AddValue("Direction", _direction);
                info.AddValue("Enabled", _enabled);
				//info.AddValue("ExposedUserOptions", _exposedUserOptions);
                info.AddValue("IsUserDefined", _isUserDefined);
                info.AddValue("Name", _name);
                info.AddValue("OrdinalPosition", _ordinalPosition);
                info.AddValue("UserOptions", _userOptions);
                info.AddValue("Precision", _precision);
                info.AddValue("Scale", _scale);
#endif
			}


			public override void ResetDefaults()
			{
				Alias = AliasDefault(this);
			}


			public override string AliasDefault(IScriptBase parameter)
			{
				return AliasDefault((Parameter)parameter);
			}


			[Interfaces.Attributes.ApiExtension]
			public string AliasDefault(Parameter parameter)
			{
				return parameter.Name.Replace("@", "");
			}


			[Interfaces.Attributes.ApiExtension]
			public bool AliasValidate(Parameter parameter)
			{
				return true;
			}


			#endregion

		}

		#endregion

	}

}

