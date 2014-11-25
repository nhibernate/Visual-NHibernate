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
	public class MapColumn : Column, ISerializable
	{

		#region Fields
		[DotfuscatorDoNotRename]
		private ScriptObject _currentParent;
		[DotfuscatorDoNotRename]
		private Column _foreignColumn;
		private readonly string _foreignColumnId;
		[DotfuscatorDoNotRename]
		internal Relationship[] _relationshipPath;
		private readonly string[] _RelationshipPathIds;
		#endregion


		#region Constructors
		public MapColumn()
		{
			_IsMapColumn = true;
		}

		public MapColumn(string name, bool isUserDefined)
			: base(name, isUserDefined)
		{
			_IsMapColumn = true;
			ResetDefaults();
		}

		public MapColumn(string name, bool isUserDefined, Relationship[] relationshipPath, Column foreignColumn, int ordinalPosition, bool isNullable, string dataType, int characterMaximumLength, int precision, int scale)
			: base(name, isUserDefined, relationshipPath == null ? null : relationshipPath[0].Parent, ordinalPosition, isNullable, dataType, characterMaximumLength, false, false, null, true, false, precision, scale)
		{
			_IsMapColumn = true;
			_currentParent = relationshipPath == null ? null : relationshipPath[0].Parent;
			_foreignColumn = foreignColumn;
			_relationshipPath = relationshipPath;
			ResetDefaults();
		}

		/// <exclude/>
		public MapColumn(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (SerializerHelper.UseFastSerialization)
			{
				_relationshipPath = null;

				using (SerializationReader reader = new SerializationReader((byte[])serializationInfo.GetValue("d", typeof(byte[]))))
				{
					UniqueId = reader.ReadString();
					Lookups.Add(UniqueId, this);
					_alias = reader.ReadString();
					_aliasDisplay = reader.ReadString();
					_characterMaximumLength = reader.ReadInt32();
					// TODO: CurrentParent
					_dataType = reader.ReadString();
					_default = reader.ReadString();
					_enabled = reader.ReadBoolean();
					_foreignColumnId = reader.ReadString();
					_inPrimaryKey = reader.ReadBoolean();
					_isIdentity = reader.ReadBoolean();
					_isNullable = reader.ReadBoolean();
					_isUserDefined = reader.ReadBoolean();
					_name = reader.ReadString();
					_ordinalPosition = reader.ReadInt32();
					// TODO: Parent
					_readOnly = reader.ReadBoolean();
					_RelationshipPathIds = reader.ReadStringArray();
					_userOptions = (List<IUserOption>)reader.ReadObject();

					for (int i = 0; i < _userOptions.Count; i++)
					{
						_userOptions[i].Owner = this;
					}
				}
			}
			else
			{
				_IsMapColumn = true;
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
				_currentParent = (ScriptObject)serializationInfo.GetValue("CurrentParent", ModelTypes.ScriptObject);
				_dataType = serializationInfo.GetString("DataType");
				_default = serializationInfo.GetString("Default");
				_enabled = serializationInfo.GetBoolean("Enabled");
				//_exposedUserOptions = serializationInfo.GetValue("ExposedUserOptions", ModelTypes.Object);
				_foreignColumn = (Column)serializationInfo.GetValue("ForeignColumn", ModelTypes.Column);
				_inPrimaryKey = serializationInfo.GetBoolean("InPrimaryKey");
				_isIdentity = serializationInfo.GetBoolean("IsIdentity");
				_isNullable = serializationInfo.GetBoolean("IsNullable");
				_isUserDefined = serializationInfo.GetBoolean("IsUserDefined");
				_name = serializationInfo.GetString("Name");
				_ordinalPosition = serializationInfo.GetInt32("OrdinalPosition");
				_parent = (ScriptObject)serializationInfo.GetValue("Parent", ModelTypes.ScriptObject);
				_readOnly = serializationInfo.GetBoolean("ReadOnly");
				_relationshipPath = (Relationship[])serializationInfo.GetValue("RelationshipPath", ModelTypes.RelationshipArray);
				_userOptions = (List<IUserOption>)serializationInfo.GetValue("UserOptions", ModelTypes.UserOptionList);

                if (version >= 8)
                {
                    _description = serializationInfo.GetString("Description");
                }
			}
		}

		#endregion

		#region Properties
        //public new ScriptObject Parent
        //{
        //    get 
        //    {
        //        return _parent;
        //        //return _currentParent; 
        //    }
        //}

		public Column[] PrimaryColumns
		{
			get { return _relationshipPath[0].PrimaryColumns; }
		}

		public Column ForeignColumn
		{
			get 
			{
				if (_foreignColumn == null && _foreignColumnId != null)
				{
					_foreignColumn = (Column)Lookups[_foreignColumnId];
				}
				return _foreignColumn; 
			}
			set
			{
				Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _foreignColumn, value);
				_foreignColumn = value;
			}

		}

		public Relationship[] RelationshipPath
		{
			get 
			{
				if (_relationshipPath == null && _RelationshipPathIds != null)
				{
					_relationshipPath = new Relationship[_RelationshipPathIds.Length];

					for (int i = 0; i < _RelationshipPathIds.Length; i++)
					{
						_relationshipPath[i] = (Relationship)Lookups[_RelationshipPathIds[i]];
					}
				}
				return _relationshipPath;
			}
			set
			{
				Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _relationshipPath, value);
				_relationshipPath = value;

                if (_currentParent != null && _parent  != null && _currentParent != _parent)
                {
                    throw new Exception("Relationship parent and MapColumn parent are different.");
                }
                _parent = _relationshipPath == null ? _parent : _relationshipPath[0].Parent;
				//_currentParent = _relationshipPath == null ? _parent : _relationshipPath[0].Parent;
			}

		}

		#endregion


		#region Functions
		public override bool NameValidate(IScriptBase scriptBase, out string failReason)
		{
			return NameValidate((MapColumn)scriptBase, out failReason);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool NameValidate(MapColumn mapColumn, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!mapColumn.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(mapColumn.Name))
			{
				failReason = "Name cannot be zero-length.";
				return false;
			}
			if (mapColumn.Name.IndexOf(" ") >= 0)
			{
				failReason = "Name cannot have spaces.";
				return false;
			}

			foreach (Column sibling in mapColumn.Parent.Columns)
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
				//writer.WriteObject(_currentParent);
				writer.Write(_dataType);
				writer.Write(_default);
				writer.Write(_enabled);
				writer.Write(ForeignColumn.UniqueId);
				writer.Write(_inPrimaryKey);
				writer.Write(_isIdentity);
				writer.Write(_isNullable);
				writer.Write(_isUserDefined);
				writer.Write(_name);
				writer.Write(_ordinalPosition);
				//writer.WriteObject(_parent);
				writer.Write(_readOnly);
				//writer.WriteTokenizedObject(_relationshipPath);
				writer.Write(ScriptBase.GetUniqueIds(RelationshipPath));
				writer.WriteObject(_userOptions);
				info.AddValue("d", writer.ToArray());
			}
#else
            info.AddValue("SerializationVersion", SerializationVersion);
            info.AddValue("Alias", _alias);
            info.AddValue("AliasDisplay", _aliasDisplay);
            info.AddValue("CharacterMaximumLength", _characterMaximumLength);
            info.AddValue("CurrentParent", _currentParent);
            info.AddValue("DataType", _dataType);
            info.AddValue("Default", _default);
            info.AddValue("Enabled", _enabled);
			//info.AddValue("ExposedUserOptions", _exposedUserOptions);
            info.AddValue("ForeignColumn", _foreignColumn);
            info.AddValue("InPrimaryKey", _inPrimaryKey);
            info.AddValue("IsIdentity", _isIdentity);
            info.AddValue("IsNullable", _isNullable);
            info.AddValue("IsUserDefined", _isUserDefined);
            info.AddValue("Name", _name);
            info.AddValue("OrdinalPosition", _ordinalPosition);
            info.AddValue("Parent", _parent);
            info.AddValue("ReadOnly", _readOnly);
            info.AddValue("RelationshipPath", _relationshipPath);
            info.AddValue("UserOptions", _userOptions);
            info.AddValue("Description", _description);
#endif
		}


		public override void ResetDefaults()
		{
			Alias = AliasDefault(this);
			AliasDisplay = AliasDisplayDefault(this);
		}


		public override string AliasDefault(IScriptBase mapColumn)
		{
			return AliasDefault((MapColumn)mapColumn);
		}


		[Interfaces.Attributes.ApiExtension]
		public string AliasDefault(MapColumn mapColumn)
		{
			if (mapColumn.RelationshipPath == null || mapColumn.ForeignColumn == null)
			{
				return "temp";
			}
			string alias = mapColumn.RelationshipPath[0].ForeignScriptObject.Alias + mapColumn.ForeignColumn.Alias;
			return alias.Replace("_", "");
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasValidate(MapColumn mapColumn, out string failReason)
		{
			failReason = "";
			return true;
		}


		[Interfaces.Attributes.ApiExtension]
		public string AliasDisplayDefault(MapColumn mapColumn)
		{
			if (mapColumn.RelationshipPath == null || mapColumn.ForeignColumn == null)
			{
				return "temp";
			}
			return mapColumn.RelationshipPath[0].ForeignScriptObject.Alias + mapColumn.ForeignColumn.Alias;
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasDisplayValidate(MapColumn mapColumn, out string failReason)
		{
			failReason = "";
			return true;
		}


		[Interfaces.Attributes.ApiExtension]
		public virtual string AliasPluralDefault(MapColumn mapColumn)
		{
			return ArchAngel.Providers.Database.Helper.Script.GetPlural(mapColumn.Alias);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasPluralValidate(MapColumn mapColumn, out string failReason)
		{
			failReason = "";
			return true;
		}


		public override string AliasPluralDefault(Column column)
		{
			return AliasPluralDefault((MapColumn)column);
		}


		public override string AliasPluralDefault(IScriptBase scriptBase)
		{
			return AliasPluralDefault((MapColumn)scriptBase);
		}


		public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasPluralValidate((MapColumn)scriptBase, out failReason);
		}


		public override bool AliasValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasValidate((MapColumn)scriptBase, out failReason);
		}

        /// <summary>
        /// Gets whether the MapColumn is in a valid state.
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
			if (!AliasDisplayValidate(this, out tempFailReason))
			{
				isValid = false;
				failReason += string.Format("{1}.AliasDisplay: {0}\n", tempFailReason, Name);
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

