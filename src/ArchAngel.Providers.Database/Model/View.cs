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
	public class View : ScriptObject, ISerializable
	{

		#region Fields
		[DotfuscatorDoNotRename]
		public static List<string> ViewPrefixes = new List<string>();
		#endregion


		#region Constructors
		public View()
		{
			_IsView = true;
		}

		public View(string name, bool isUserDefined)
			: base(name, isUserDefined)
		{
			_IsView = true;
			ResetDefaults();
		}

		/// <exclude/>
		public View(SerializationInfo serializationInfo, StreamingContext streamingContext)
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
				_IsView = true;
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
				writer.Write(_isUserDefined);
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
            info.AddValue("IsUserDefined", _isUserDefined);
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


		public override string AliasDefault(IScriptBase view)
		{
			return AliasDefault((View)view);
		}


		[Interfaces.Attributes.ApiExtension]
		public string AliasDefault(View view)
		{
			string alias = ArchAngel.Providers.Database.Helper.Script.GetSingleWord(view.Name.Trim());
			string aliasLower = alias.ToLower();

			foreach (string prefix in ViewPrefixes)
			{
				if (aliasLower.IndexOf(prefix.ToLower()) == 0)
				{
					alias = alias.Substring(prefix.Length);
					break;
				}
			}
			return ArchAngel.Providers.Database.Helper.Script.GetSingular(alias).Replace("_", "");
		}


		public override bool NameValidate(IScriptBase scriptBase, out string failReason)
		{
			return NameValidate((View)scriptBase, out failReason);
		}


		public override bool NameValidate(ScriptObject scriptObject, out string failReason)
		{
			return NameValidate((View)scriptObject, out failReason);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool NameValidate(View view, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!view.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(view.Name))
			{
				failReason = "Name cannot be zero-length.";
				return false;
			}
			if (view.Name.IndexOf(" ") >= 0)
			{
				failReason = "Name cannot have spaces.";
				return false;
			}

			foreach (ScriptObject sibling in view.Database.EnabledScriptObjects)
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
		public bool AliasValidate(View view, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!view.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(view.Alias))
			{
				failReason = "Alias cannot be zero-length.";
				return false;
			}
			if (view.Alias.IndexOf(" ") >= 0)
			{
				failReason = "Alias cannot have spaces.";
				return false;
			}

            foreach (ScriptObject sibling in view.Database.EnabledScriptObjects)
			{
				if (sibling != view && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Alias, view.Alias, false))
				{
					failReason = "Duplicate alias: " + view.Alias;
					return false;
				}
			}
			return true;
		}


		public override string AliasDefault(ScriptObject scriptObject)
		{
			return AliasDefault((View)scriptObject);
		}


		public override string AliasPluralDefault(IScriptBase scriptBase)
		{
			return AliasPluralDefault((View)scriptBase);
		}


		public override string AliasPluralDefault(ScriptObject scriptObject)
		{
			return AliasPluralDefault((View)scriptObject);
		}


		public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasPluralValidate((View)scriptBase, out failReason);
		}


		public override bool AliasPluralValidate(ScriptObject scriptObject, out string failReason)
		{
			return AliasPluralValidate((View)scriptObject, out failReason);
		}


		public override bool AliasValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasValidate((View)scriptBase, out failReason);
		}


		public override bool AliasValidate(ScriptObject scriptObject, out string failReason)
		{
			return AliasValidate((View)scriptObject, out failReason);
		}


		[Interfaces.Attributes.ApiExtension]
		public virtual string AliasPluralDefault(View view)
		{
			return ArchAngel.Providers.Database.Helper.Script.GetPlural(view.Alias);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasPluralValidate(View view, out string failReason)
		{
			failReason = "";

			/*Don't check items that are not enabled*/
			if (!view.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(view.AliasPlural))
			{
				failReason = "AliasPlural cannot be zero-length.";
				return false;
			}
			if (view.AliasPlural.IndexOf(" ") >= 0)
			{
				failReason = "AliasPlural cannot have spaces.";
				return false;
			}
			if (ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(view.AliasPlural, view.Alias, false))
			{
				failReason = "AliasPlural must be different to Alias.";
				return false;
			}

            foreach (ScriptObject sibling in view.Database.EnabledScriptObjects)
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
						failReason = string.Format("View with same name exists: {0}", sibling.Alias);
						return false;
					}
				}
			}
			return true;
		}

        /// <summary>
        /// Gets whether the View is in a valid state.
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

