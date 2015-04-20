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
	public class OneToManyRelationship : Relationship, ISerializable
	{

		#region Constructors
		public OneToManyRelationship()
		{
		}

		public OneToManyRelationship(string name, bool isUserDefined, ScriptObject primaryScriptObject, Column[] primaryColumns, ScriptObject foreignScriptObject, Column[] foreignColumns, Filter filter)
			: base(name, isUserDefined, primaryScriptObject, primaryScriptObject, primaryColumns, foreignScriptObject, foreignColumns, filter)
		{
			ResetDefaults();
		}

		/// <exclude/>
		public OneToManyRelationship(SerializationInfo serializationInfo, StreamingContext streamingContext)
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


		#region Functions
		public override bool NameValidate(IScriptBase scriptBase, out string failReason)
		{
			return NameValidate((OneToManyRelationship)scriptBase, out failReason);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool NameValidate(OneToManyRelationship oneToManyRelationship, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!oneToManyRelationship.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(oneToManyRelationship.Name))
			{
				failReason = "Name cannot be zero-length.";
				return false;
			}
			if (oneToManyRelationship.Name.IndexOf(" ") >= 0)
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


		public override string AliasDefault(IScriptBase oneToManyRelationship)
		{
			return AliasDefault((OneToManyRelationship)oneToManyRelationship);
		}


		[Interfaces.Attributes.ApiExtension]
		public string AliasDefault(OneToManyRelationship oneToManyRelationship)
		{
			return oneToManyRelationship.ForeignScriptObject.AliasPlural;
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasValidate(OneToManyRelationship oneToManyRelationship, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!oneToManyRelationship.Enabled)
			{
				return true;
			}

			foreach (Relationship sibling in oneToManyRelationship.Parent.Relationships)
			{
				if (sibling != oneToManyRelationship && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Alias, oneToManyRelationship.Alias, false))
				{
					failReason = "Duplicate alias: " + oneToManyRelationship.Alias;
					return false;
				}
			}
			return true;
		}


		[Interfaces.Attributes.ApiExtension]
		public virtual string AliasPluralDefault(OneToManyRelationship oneToManyRelationship)
		{
			return ArchAngel.Providers.Database.Helper.Script.GetPlural(oneToManyRelationship.Alias);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasPluralValidate(OneToManyRelationship oneToManyRelationship, out string failReason)
		{
			failReason = "";
			return true;
		}


		public override string AliasPluralDefault(Relationship relationship)
		{
			return AliasPluralDefault((OneToManyRelationship)relationship);
		}


		public override string AliasPluralDefault(IScriptBase scriptBase)
		{
			return AliasPluralDefault((OneToManyRelationship)scriptBase);
		}


		public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasPluralValidate((OneToManyRelationship)scriptBase, out failReason);
		}


		public override bool AliasValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasValidate((OneToManyRelationship)scriptBase, out failReason);
		}

        /// <summary>
        /// Gets whether the OneToManyRelationship is in a valid state.
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
			if (!ForeignRelationship.Filter.IsReturnTypeCollection)
			{
				isValid = false;
				failReason += "Foreign filter needs to return a collection, not a single object.";
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

