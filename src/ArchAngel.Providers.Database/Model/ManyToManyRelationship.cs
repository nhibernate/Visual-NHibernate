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
	public class ManyToManyRelationship : Relationship, ISerializable
	{

		#region Fields
		[DotfuscatorDoNotRename]
		private OneToManyRelationship _intermediatePrimaryRelationship;
		private readonly string _intermediatePrimaryRelationshipId;
		[DotfuscatorDoNotRename]
		private ManyToOneRelationship _intermediateForeignRelationship;
		private readonly string _intermediateForeignRelationshipId;
		#endregion


		#region Constructors
		public ManyToManyRelationship()
		{
		}

		public ManyToManyRelationship(string name, bool isUserDefined, OneToManyRelationship primaryRelationship, ManyToOneRelationship foreignRelationship, Filter filter)
			: base(name, isUserDefined, primaryRelationship.Parent, primaryRelationship.Parent, primaryRelationship.PrimaryColumns, foreignRelationship.ForeignRelationship.Parent, foreignRelationship.ForeignRelationship.PrimaryColumns, filter)
		{
			_intermediatePrimaryRelationship = primaryRelationship;
			_intermediateForeignRelationship = foreignRelationship;
			ResetDefaults();
		}

		/// <exclude/>
		public ManyToManyRelationship(SerializationInfo serializationInfo, StreamingContext streamingContext)
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
					_intermediateForeignRelationshipId = reader.ReadString();
					_intermediatePrimaryRelationshipId = reader.ReadString();
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
				_intermediateForeignRelationship = (ManyToOneRelationship)serializationInfo.GetValue("IntermediateForeignRelationship", ModelTypes.ManyToOneRelationship);
				_intermediatePrimaryRelationship = (OneToManyRelationship)serializationInfo.GetValue("IntermediatePrimaryRelationship", ModelTypes.OneToManyRelationship);
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
		public override string Path
		{
			get
			{
				string path = GetColumnNameList(IntermediatePrimaryRelationship.PrimaryColumns) + " -> " + GetColumnNameList(IntermediatePrimaryRelationship.ForeignColumns) + " -> " + GetColumnNameList(IntermediateForeignRelationship.PrimaryColumns) + " -> " + GetColumnNameList(IntermediateForeignRelationship.ForeignColumns);
				return path;
			}

		}

		public ScriptObject IntermediateForeignScriptObject
		{
			get { return _intermediateForeignRelationship.Parent; }
		}

		public OneToManyRelationship IntermediatePrimaryRelationship
		{
			get 
			{
				if (_intermediatePrimaryRelationship == null && _intermediatePrimaryRelationshipId != null)
				{
					_intermediatePrimaryRelationship = (OneToManyRelationship)Lookups[_intermediatePrimaryRelationshipId];
				}
				return _intermediatePrimaryRelationship; 
			}
			set
			{
				Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _intermediatePrimaryRelationship, value);
				_intermediatePrimaryRelationship = value;
			}

		}

		public ManyToOneRelationship IntermediateForeignRelationship
		{
			get 
			{
				if (_intermediateForeignRelationship == null && _intermediateForeignRelationshipId != null)
				{
					_intermediateForeignRelationship = (ManyToOneRelationship)Lookups[_intermediateForeignRelationshipId];
				}
				return _intermediateForeignRelationship; 
			}
			set
			{
				Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _intermediateForeignRelationship, value);
				_intermediateForeignRelationship = value;
			}

		}

		#endregion


		#region Functions
		public override bool NameValidate(IScriptBase scriptBase, out string failReason)
		{
			return NameValidate((ManyToManyRelationship)scriptBase, out failReason);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool NameValidate(ManyToManyRelationship manyToManyRelationship, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!manyToManyRelationship.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(manyToManyRelationship.Name))
			{
				failReason = "Name cannot be zero-length.";
				return false;
			}
			if (manyToManyRelationship.Name.IndexOf(" ") >= 0)
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
				writer.Write(Filter.UniqueId);
				writer.Write(ScriptBase.GetUniqueIds(ForeignColumns));
				writer.Write(ForeignRelationship.UniqueId);
				writer.Write(ForeignScriptObject.UniqueId);
				writer.Write(IntermediateForeignRelationship.UniqueId);
				writer.Write(IntermediatePrimaryRelationship.UniqueId);
				writer.Write(_isUserDefined);
				writer.Write(_name);
				//writer.WriteObject(_parent);
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
            info.AddValue("IntermediateForeignRelationship", _intermediateForeignRelationship);
            info.AddValue("IntermediatePrimaryRelationship", _intermediatePrimaryRelationship);
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


		public override string AliasDefault(IScriptBase manyToManyRelationship)
		{
			return AliasDefault((ManyToManyRelationship)manyToManyRelationship);
		}


		[Interfaces.Attributes.ApiExtension]
		public string AliasDefault(ManyToManyRelationship manyToManyRelationship)
		{
			return manyToManyRelationship.IntermediateForeignRelationship.ForeignScriptObject.AliasPlural;
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasValidate(ManyToManyRelationship manyToManyRelationship, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!manyToManyRelationship.Enabled)
			{
				return true;
			}

			foreach (Relationship sibling in manyToManyRelationship.Parent.Relationships)
			{
				if (sibling != manyToManyRelationship && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Alias, manyToManyRelationship.Alias, false))
				{
					failReason = "Duplicate alias: " + manyToManyRelationship.Alias;
					return false;
				}
			}
			return true;
		}


		[Interfaces.Attributes.ApiExtension]
		public virtual string AliasPluralDefault(ManyToManyRelationship manyToManyRelationship)
		{
			return ArchAngel.Providers.Database.Helper.Script.GetPlural(manyToManyRelationship.Alias);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasPluralValidate(ManyToManyRelationship manyToManyRelationship, out string failReason)
		{
			failReason = "";
			return true;
		}


		public override string AliasPluralDefault(Relationship relationship)
		{
			return AliasPluralDefault((ManyToManyRelationship)relationship);
		}


		public override string AliasPluralDefault(IScriptBase scriptBase)
		{
			return AliasPluralDefault((ManyToManyRelationship)scriptBase);
		}


		public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasPluralValidate((ManyToManyRelationship)scriptBase, out failReason);
		}


		public override bool AliasValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasValidate((ManyToManyRelationship)scriptBase, out failReason);
		}

        /// <summary>
        /// Gets whether the ManyToManyRelationship is in a valid state.
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
			if (!IsValidManyToManyFilter(this, out tempFailReason) || !IsValidManyToManyFilter((ManyToManyRelationship)ForeignRelationship, out tempFailReason))
			{
				isValid = false;
				failReason += tempFailReason;
			}
			//if (!this.Filter.IsReturnTypeCollection)
			//{
			//    isValid = false;
			//    failReason += "Primary filter needs to return a collection, not a single object.";
			//}
			//if (!this.ForeignRelationship.Filter.IsReturnTypeCollection)
			//{
			//    isValid = false;
			//    failReason += "Foreign filter needs to return a collection, not a single object.";
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

