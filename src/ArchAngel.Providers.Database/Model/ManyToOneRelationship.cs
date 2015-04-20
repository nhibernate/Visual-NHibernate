using ArchAngel.Interfaces.ITemplate;
using Slyce.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ArchAngel.Providers.Database.Model
{
	[Serializable]
	[Interfaces.Attributes.ArchAngelEditor(true, true, "Alias")]
	[DotfuscatorDoNotRename]
	public class ManyToOneRelationship : Relationship, ISerializable
	{

		#region Constructors
		public ManyToOneRelationship()
		{
		}

		public ManyToOneRelationship(string name, bool isUserDefined, ScriptObject primaryScriptObject, Column[] primaryColumns, ScriptObject foreignScriptObject, Column[] foreignColumns, Filter filter)
			: base(name, isUserDefined, primaryScriptObject, primaryScriptObject, primaryColumns, foreignScriptObject, foreignColumns, filter)
		{
			ResetDefaults();
		}

		/// <exclude/>
		public ManyToOneRelationship(SerializationInfo serializationInfo, StreamingContext streamingContext)
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
				_filter = (Filter)serializationInfo.GetValue("Filter", typeof(Filter));
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
			return NameValidate((ManyToOneRelationship)scriptBase, out failReason);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool NameValidate(ManyToOneRelationship manyToOneRelationship, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!manyToOneRelationship.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(manyToOneRelationship.Name))
			{
				failReason = "Name cannot be zero-length.";
				return false;
			}
			if (manyToOneRelationship.Name.IndexOf(" ") >= 0)
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
				//writer.WriteObject(_exposedUserOptions);
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


		public override string AliasDefault(IScriptBase manyToOneRelationship)
		{
			return AliasDefault((ManyToOneRelationship)manyToOneRelationship);
		}


		[Interfaces.Attributes.ApiExtension]
		public string AliasDefault(ManyToOneRelationship manyToOneRelationship)
		{
			return manyToOneRelationship.ForeignScriptObject.Alias;
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasValidate(ManyToOneRelationship manyToOneRelationship, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!manyToOneRelationship.Enabled)
			{
				return true;
			}

			foreach (Relationship sibling in manyToOneRelationship.Parent.Relationships)
			{
				if (sibling != manyToOneRelationship && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Alias, manyToOneRelationship.Alias, false))
				{
					failReason = "Duplicate alias: " + manyToOneRelationship.Alias;
					return false;
				}
			}
			return true;
		}


		[Interfaces.Attributes.ApiExtension]
		public virtual string AliasPluralDefault(ManyToOneRelationship manyToOneRelationship)
		{
			return ArchAngel.Providers.Database.Helper.Script.GetPlural(manyToOneRelationship.Alias);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasPluralValidate(ManyToOneRelationship manyToOneRelationship, out string failReason)
		{
			failReason = "";
			return true;
		}


		public override string AliasPluralDefault(Relationship relationship)
		{
			return AliasPluralDefault((ManyToOneRelationship)relationship);
		}


		public override string AliasPluralDefault(IScriptBase scriptBase)
		{
			return AliasPluralDefault((ManyToOneRelationship)scriptBase);
		}


		public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasPluralValidate((ManyToOneRelationship)scriptBase, out failReason);
		}


		public override bool AliasValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasValidate((ManyToOneRelationship)scriptBase, out failReason);
		}

        /// <summary>
        /// Gets whether the ManyToOneRelationship is in a valid state.
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
			if (!Filter.IsReturnTypeCollection)
			{
				isValid = false;
				failReason += "Primary filter needs to return a collection, not a single object.";
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
			if (!Filter.IsValid(false, out tempFailReason))
			{
				isValid = false;
				failReason += "Primary filter is invalid: " + tempFailReason;
			}
			if (!ForeignRelationship.Filter.IsValid(false, out tempFailReason))
			{
				isValid = false;
				failReason += "Foreign filter is invalid: " + tempFailReason;
			}
			// Ensure that MapColumns aren't used to map back to their parent ScriptObject
			for (int i = 0; i < PrimaryColumns.Length; i++)
			{
				if (MapColumnType.IsInstanceOfType(PrimaryColumns[i]))
				{
					MapColumn primaryCol = (MapColumn)PrimaryColumns[i];
					Column foreignCol = ForeignColumns[i];

					if (primaryCol.ForeignColumn.Parent.Name == foreignCol.Parent.Name)
					{
						isValid = false;
						failReason += string.Format("A MapColumn cannot be joined to its parent: {0} [{1}.{2}]{3}", primaryCol.Alias, primaryCol.Parent.Name, primaryCol.Name, Environment.NewLine);
					}
				}
			}
			// Ensure that the filter's column count equals the number of primary columns
			if (Filter.FilterColumns.Length != PrimaryColumns.Length)
			{
				isValid = false;
				failReason += "Count of Filter's columns doesn't number of joined columns." + Environment.NewLine;

			}
			//foreach (Column primaryColumn in this.PrimaryColumns)
			//{
			//    if (MapColumnType.IsInstanceOfType(primaryColumn))
			//    {
			//        if /*(primaryColumn.Parent == this.ForeignScriptObject)*/(primaryColumn.Parent == this.PrimaryScriptObject)
			//        {
			//            isValid = false;
			//            failReason += string.Format("A MapColumn cannot be joined to its parent: {0} [{1}.{2}]{3}", primaryColumn.Alias, primaryColumn.Parent.Name, primaryColumn.Name, Environment.NewLine);
			//        }
			//    }
			//}
			//foreach (ManyToOneRelationship rel in this.Parent.ManyToOneRelationships)
			//{
			//    if (rel == this || !rel.Enabled)
			//    {
			//        continue;
			//    }
			//    if (rel.Name == this.Name || rel.Alias == this.Alias || rel.AliasPlural == this.AliasPlural)
			//    {
			//        isValid = false;
			//        failReason += "Duplicate named relationship.";
			//    }
			//    if (rel.ForeignColumns.Length == this.ForeignColumns.Length)
			//    {
			//        bool sameColumns = true;

			//        foreach (Column foreignColumn in rel.ForeignColumns)
			//        {
			//            foreach (Column column in this.ForeignColumns)
			//            {
			//                if (foreignColumn != column)
			//                {
			//                    sameColumns = false;
			//                    break;
			//                }
			//            }
			//            if (!sameColumns)
			//            {
			//                break;
			//            }
			//        }
			//        if (sameColumns)
			//        {
			//            isValid = false;
			//            failReason += "Duplicate relationship.";
			//        }
			//    }
			//}
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

