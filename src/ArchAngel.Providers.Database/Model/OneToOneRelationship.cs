using ArchAngel.Interfaces.ITemplate;
using Slyce.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ArchAngel.Providers.Database.Model
{
	[Serializable]
	[Interfaces.Attributes.ArchAngelEditor(true, false, "Alias")]
	[DotfuscatorDoNotRename]
	public class OneToOneRelationship : Relationship, ISerializable
	{

		#region Fields
		[DotfuscatorDoNotRename]
		internal bool _isBase;
		#endregion


		#region Constructors
		public OneToOneRelationship()
		{
		}

		public OneToOneRelationship(string name, bool isUserDefined, ScriptObject primaryScriptObject, Column[] primaryColumns, ScriptObject foreignScriptObject, Column[] foreignColumns, Filter filter, bool isBase)
			: base(name, isUserDefined, primaryScriptObject, primaryScriptObject, primaryColumns, foreignScriptObject, foreignColumns, filter)
		{
			_isBase = isBase;
			ResetDefaults();
		}

		/// <exclude/>
		public OneToOneRelationship(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (SerializerHelper.UseFastSerialization)
			{
				_foreignColumns = null;

				using (SerializationReader reader = new SerializationReader((byte[])serializationInfo.GetValue("d", typeof(byte[]))))
				{
					UniqueId = reader.ReadString();
					Lookups.Add(UniqueId, this);
					_alias = reader.ReadString();
					_enabled = reader.ReadBoolean();
					_filterId = reader.ReadString();
					_ForeignColumnIds = reader.ReadStringArray();
					_ForeignRelationshipId = reader.ReadString();
					_ForeignScriptObjectId = reader.ReadString();
					_isBase = reader.ReadBoolean();
					_isUserDefined = reader.ReadBoolean();
					_name = reader.ReadString();
					// TODO: Parent
					string[] primaryColumnIds = reader.ReadStringArray();

					foreach (string primaryColumnId in primaryColumnIds)
					{
						_primaryColumns.Add((Column)Lookups[primaryColumnId]);
					}
					// TODO: PrimaryScriptObject
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
				_enabled = serializationInfo.GetBoolean("Enabled");
				//_exposedUserOptions = serializationInfo.GetValue("ExposedUserOptions", ModelTypes.Object);
				_filter = (Filter)serializationInfo.GetValue("Filter", ModelTypes.Filter);
				_foreignColumns = (List<Column>)serializationInfo.GetValue("ForeignColumns", ModelTypes.ColumnList);
				_foreignRelationship = (Relationship)serializationInfo.GetValue("ForeignRelationship", ModelTypes.Relationship);
				_foreignScriptObject = (ScriptObject)serializationInfo.GetValue("ForeignScriptObject", ModelTypes.ScriptObject);
				_isBase = serializationInfo.GetBoolean("IsBase");
				_isUserDefined = serializationInfo.GetBoolean("IsUserDefined");
				_name = serializationInfo.GetString("Name");
				_parent = (ScriptObject)serializationInfo.GetValue("Parent", ModelTypes.ScriptObject);
				_primaryColumns = (List<Column>)serializationInfo.GetValue("PrimaryColumns", ModelTypes.ColumnList);
				_primaryScriptObject = (ScriptObject)serializationInfo.GetValue("PrimaryScriptObject", ModelTypes.ScriptObject);
				_userOptions = (List<IUserOption>)serializationInfo.GetValue("UserOptions", ModelTypes.UserOptionList);

                if (version >= 8)
                {
                    _description = serializationInfo.GetString("Description");
                }
			}
		}

		#endregion

		#region Properties
		public bool IsBase
		{
			get { return _isBase; }
		}

		public bool IsDerived
		{
			get { return !_isBase; }
		}

		#endregion


		#region Functions
		public override bool NameValidate(IScriptBase scriptBase, out string failReason)
		{
			return NameValidate((OneToOneRelationship)scriptBase, out failReason);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool NameValidate(OneToOneRelationship oneToOneRelationship, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!oneToOneRelationship.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(oneToOneRelationship.Name))
			{
				failReason = "Name cannot be zero-length.";
				return false;
			}
			if (oneToOneRelationship.Name.IndexOf(" ") >= 0)
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
				writer.Write(_enabled);
				//writer.Write(_exposedUserOptions);
				writer.Write(Filter.UniqueId);
				//writer.Write(_foreignColumns);
				writer.Write(ScriptBase.GetUniqueIds(ForeignColumns));
				writer.Write(ForeignRelationship.UniqueId);
				writer.Write(ForeignScriptObject.UniqueId);
				writer.Write(_isBase);
				writer.Write(_isUserDefined);
				writer.Write(_name);
				//writer.WriteObject(_parent);
				//writer.Write(_primaryColumns);
				writer.Write(ScriptBase.GetUniqueIds(PrimaryColumns));
				//writer.WriteObject(_primaryScriptObject);
				writer.WriteObject(_userOptions);
				info.AddValue("d", writer.ToArray());
			}
#else
            info.AddValue("SerializationVersion", SerializationVersion);
            info.AddValue("Alias", _alias);
            info.AddValue("Enabled", _enabled);
			//info.AddValue("ExposedUserOptions", _exposedUserOptions);
            info.AddValue("Filter", _filter);
            info.AddValue("ForeignColumns", _foreignColumns);
            info.AddValue("ForeignRelationship", _foreignRelationship);
            info.AddValue("ForeignScriptObject", _foreignScriptObject);
            info.AddValue("IsBase", _isBase);
            info.AddValue("IsUserDefined", _isUserDefined);
            info.AddValue("Name", _name);
            info.AddValue("Parent", _parent);
            info.AddValue("PrimaryColumns", _primaryColumns);
            info.AddValue("PrimaryScriptObject", _primaryScriptObject);
            info.AddValue("UserOptions", _userOptions);
            info.AddValue("Description", _description);
#endif
		}


		public override void ResetDefaults()
		{
			Alias = AliasDefault(this);
		}


		public override string AliasDefault(IScriptBase oneToOneRelationship)
		{
			return AliasDefault((OneToOneRelationship)oneToOneRelationship);
		}


		[Interfaces.Attributes.ApiExtension]
		public string AliasDefault(OneToOneRelationship oneToOneRelationship)
		{
			return oneToOneRelationship.Parent.Alias;
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasValidate(OneToOneRelationship oneToOneRelationship, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!oneToOneRelationship.Enabled)
			{
				return true;
			}

			foreach (Relationship sibling in oneToOneRelationship.Parent.Relationships)
			{
				if (sibling != oneToOneRelationship && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Alias, oneToOneRelationship.Alias, false))
				{
					failReason = "Duplicate alias: " + oneToOneRelationship.Alias;
					return false;
				}
			}
			return true;
		}


		[Interfaces.Attributes.ApiExtension]
		public virtual string AliasPluralDefault(OneToOneRelationship oneToOneRelationship)
		{
			return ArchAngel.Providers.Database.Helper.Script.GetPlural(oneToOneRelationship.Alias);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasPluralValidate(OneToOneRelationship oneToOneRelationship, out string failReason)
		{
			failReason = "";
			return true;
		}


		public override string AliasPluralDefault(Relationship relationship)
		{
			return AliasPluralDefault((OneToOneRelationship)relationship);
		}


		public override string AliasPluralDefault(IScriptBase scriptBase)
		{
			return AliasPluralDefault((OneToOneRelationship)scriptBase);
		}


		public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasPluralValidate((OneToOneRelationship)scriptBase, out failReason);
		}


		public override bool AliasValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasValidate((OneToOneRelationship)scriptBase, out failReason);
		}

        /// <summary>
        /// Gets whether the OneToOneRelationship is in a valid state.
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
			if (Filter.IsReturnTypeCollection)
			{
				isValid = false;
				failReason += "Primary filter needs to return a single object, not a collection.";
			}
			if (ForeignRelationship.Filter.IsReturnTypeCollection)
			{
				isValid = false;
				failReason += "Foreign filter needs to return a single object, not a collection.";
			}
			if (!IsValidFilter(this, out tempFailReason) || !IsValidFilter(ForeignRelationship, out tempFailReason))
			{
				isValid = false;
				failReason += tempFailReason;
			}
			if (deepCheck)
			{
				/*Check inner objects*/
				if (Filter != null && !Filter.IsValid(deepCheck, out tempFailReason))
				{
					isValid = false;
					failReason += string.Format("{0}.Filter: {1}\n", Name, tempFailReason);
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

