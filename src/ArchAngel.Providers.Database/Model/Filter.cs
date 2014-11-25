using ArchAngel.Interfaces.ITemplate;
using Slyce.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ArchAngel.Providers.Database.Model
{
	/// <summary>
	/// This represents a filter in the object model. A filter defines a mechanism that can be used to filter collections.
	/// ArchAngel automatically creates a filter for each indexed columns.
	/// </summary>
	[Serializable]
	[Interfaces.Attributes.ArchAngelEditor(true, true, "Alias")]
	[DotfuscatorDoNotRename]
	public class Filter : ScriptBase, ISerializable
	{

		#region Fields
		[DotfuscatorDoNotRename]
		internal ScriptObject _parent;
		[DotfuscatorDoNotRename]
		private bool _isReturnTypeCollection;
		[DotfuscatorDoNotRename]
		private bool _createStoredProcedure;
		[DotfuscatorDoNotRename]
		private bool _useCustomWhere;
		[DotfuscatorDoNotRename]
		private string _customWhere;
		[DotfuscatorDoNotRename]
		internal List<FilterColumn> _filterColumns = new List<FilterColumn>();
		[DotfuscatorDoNotRename]
		internal List<OrderByColumn> _orderByColumns = new List<OrderByColumn>();
		[DotfuscatorDoNotRename]
		internal Key _key;
		#endregion


		#region Constructors
		internal Filter()
		{
		}

		public Filter(string name, bool isUserDefined, ScriptObject parent, bool isReturnTypeCollection, bool createStoredProcedure, bool useCustomWhere, string customWhere, Key key)
			: base(name, isUserDefined)
		{
			_key = key;
			_parent = parent;
			_isReturnTypeCollection = isReturnTypeCollection;
			_createStoredProcedure = createStoredProcedure;
			_useCustomWhere = useCustomWhere;
			_customWhere = customWhere;
			ResetDefaults();
		}

		/// <exclude/>
		public Filter(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (SerializerHelper.UseFastSerialization)
			{
				using (SerializationReader reader = new SerializationReader((byte[])serializationInfo.GetValue("d", typeof(byte[]))))
				{
					// TODO: Parent
					UniqueId = reader.ReadString();
					Lookups.Add(UniqueId, this);
					_alias = reader.ReadString();
					_createStoredProcedure = reader.ReadBoolean();
					_customWhere = reader.ReadString();
					_enabled = reader.ReadBoolean();
					_filterColumns = (List<FilterColumn>)reader.ReadObject();
					_isReturnTypeCollection = reader.ReadBoolean();
					_isUserDefined = reader.ReadBoolean();
					_name = reader.ReadString();
					_orderByColumns = (List<OrderByColumn>)reader.ReadObject();
					_useCustomWhere = reader.ReadBoolean();
					_userOptions = (List<IUserOption>)reader.ReadObject();

					for (int i = 0; i < _userOptions.Count; i++)
					{
						_userOptions[i].Owner = this;
					}
					foreach (FilterColumn filterColumn in _filterColumns)
					{
						filterColumn.Parent = this;
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
				_createStoredProcedure = serializationInfo.GetBoolean("CreateStoredProcedure");
				_customWhere = serializationInfo.GetString("CustomWhere");
				_enabled = serializationInfo.GetBoolean("Enabled");
				_filterColumns = (List<FilterColumn>)serializationInfo.GetValue("FilterColumns", ModelTypes.FilterColumnList);
				_isReturnTypeCollection = serializationInfo.GetBoolean("IsReturnTypeCollection");
				_isUserDefined = serializationInfo.GetBoolean("IsUserDefined");
				_name = serializationInfo.GetString("Name");
				_orderByColumns = (List<OrderByColumn>)serializationInfo.GetValue("OrderByColumns", ModelTypes.OrderByColumnList);
				_parent = (ScriptObject)serializationInfo.GetValue("Parent", ModelTypes.ScriptObject);
				_useCustomWhere = serializationInfo.GetBoolean("UserCustomWhere");
				_userOptions = (List<IUserOption>)serializationInfo.GetValue("UserOptions", ModelTypes.UserOptionList);

				foreach (FilterColumn filterColumn in _filterColumns)
				{
					if (filterColumn != null)
					{
						filterColumn.Parent = this;
					}
				}
                if (version >= 8)
                {
                    _description = serializationInfo.GetString("Description");
                }
			}
		}

		#endregion

		#region Properties
		public Key Key
		{
			get { return _key; }
		}

		public ScriptObject Parent
		{
			get { return _parent; }
			internal set { _parent = value; } // This should only get called from the scriptobject serialization constructor (table, view, storedprocedure)
		}

		public bool IsReturnTypeCollection
		{
			get { return _isReturnTypeCollection; }
			set
			{
				Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _isReturnTypeCollection, value);
				_isReturnTypeCollection = value;
			}

		}

		public bool CreateStoredProcedure
		{
			get { return _createStoredProcedure; }
			set
			{
				Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _createStoredProcedure, value);
				_createStoredProcedure = value;
			}

		}

		public bool UseCustomWhere
		{
			get { return _useCustomWhere; }
			set
			{
				Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _useCustomWhere, value);
				_useCustomWhere = value;
			}

		}

		public string CustomWhere
		{
			get { return _customWhere; }
			set
			{
				Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _customWhere, value);
				_customWhere = value;
			}

		}

		public FilterColumn[] FilterColumns
		{
			get
			{
				/*TODO: Implement a ColumnComparer so that we can call _filterColumns.Sort(comparer) instead of this hack*/
				if (_filterColumns.Count == 0)
				{
					return new FilterColumn[0];
				}
				int[] ordinalPositions = new int[_filterColumns.Count];

				for (int i = 0; i < _filterColumns.Count; i++)
				{
					ordinalPositions[i] = _filterColumns[i].Column.OrdinalPosition;
				}
				FilterColumn[] arrFilterColumns = _filterColumns.ToArray();
				Array.Sort(ordinalPositions, arrFilterColumns);
				return arrFilterColumns;
				/*return (FilterColumn[])_filterColumns.ToArray();*/
			}

		}

		public OrderByColumn[] OrderByColumns
		{
			get { return _orderByColumns.ToArray(); }
		}

		#endregion


		#region Functions
		public override bool NameValidate(IScriptBase scriptBase, out string failReason)
		{
			return NameValidate((Filter)scriptBase, out failReason);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool NameValidate(Filter filter, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/

			if (!filter.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(filter.Name))
			{
				failReason = "Name cannot be zero-length.";
				return false;
			}
			if (filter.Name.IndexOf(" ") >= 0)
			{
				failReason = "Name cannot have spaces.";
				return false;
			}

			foreach (Filter sibling in filter.Parent.Filters)
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
				foreach (FilterColumn col in _filterColumns)
				{
					col.Parent = this;
				}
				writer.Write(UniqueId);
				writer.Write(_alias);
				writer.Write(_createStoredProcedure);
				writer.Write(_customWhere);
				writer.Write(_enabled);
				//writer.WriteObject(_exposedUserOptions);
				writer.WriteObject(_filterColumns);
				writer.Write(_isReturnTypeCollection);
				writer.Write(_isUserDefined);
				writer.Write(_name);
				writer.WriteObject(_orderByColumns);
				//writer.WriteObject(_parent);
				writer.Write(_useCustomWhere);
				writer.WriteObject(_userOptions);
				info.AddValue("d", writer.ToArray());
			}
			info.AddValue("UserOptions", this._userOptions);
#else
            info.AddValue("SerializationVersion", SerializationVersion);
            info.AddValue("Alias", _alias);
            info.AddValue("CreateStoredProcedure", _createStoredProcedure);
            info.AddValue("CustomWhere", _customWhere);
            info.AddValue("Enabled", _enabled);
            info.AddValue("FilterColumns", _filterColumns);
            info.AddValue("IsReturnTypeCollection", _isReturnTypeCollection);
            info.AddValue("IsUserDefined", _isUserDefined);
            info.AddValue("Name", _name);
            info.AddValue("OrderByColumns", _orderByColumns);
            info.AddValue("Parent", _parent);
            info.AddValue("UserCustomWhere", _useCustomWhere);
            info.AddValue("UserOptions", _userOptions);
            info.AddValue("Description", _description);
#endif
		}


		public override void ResetDefaults()
		{
			Alias = AliasDefault(this);
		}


		public override string AliasDefault(IScriptBase filter)
		{
			return AliasDefault((Filter)filter);
		}


		[Interfaces.Attributes.ApiExtension]
		public string AliasDefault(Filter filter)
		{
			if (filter.Key != null
				/*This filter is derived from a database key*/)
			{
				string alias;

				if (filter.Key.Type == Helper.DatabaseConstant.KeyType.Primary)
				{
					alias = "Get" + filter.Key.Parent.Alias;
				}
				else
				{
					if (filter.Key.Type == Helper.DatabaseConstant.KeyType.Foreign)
					{
						alias = "Get" + filter.Key.Parent.AliasPlural + "By";
					}
					else
					{
						alias = "Get" + filter.Key.Parent.Alias + "By";
					}

					for (int i = 0; i < filter.Key.Columns.Length; i++)
					{
						if (i > 0)
						{
							alias += "And";
						}
						alias += filter.Key.Columns[i].Alias;
					}
				}
				return alias;
			}
			if (filter.IsReturnTypeCollection)
			{
				string byString = "";

				for (int filterColCounter = 0; filterColCounter < filter.FilterColumns.Length; filterColCounter++)
				{
					if (filterColCounter == 0)
					{
						byString = "By";
					}
					else
					{
						byString += "And";
					}
					byString += filter.FilterColumns[filterColCounter].Alias;
				}
				return "Get" + filter.Parent.AliasPlural + byString;
			}
			else
			{
				string byString = "";

				for (int filterColCounter = 0; filterColCounter < filter.FilterColumns.Length; filterColCounter++)
				{
					if (filterColCounter == 0)
					{
						byString = "By";
					}
					else
					{
						byString += "And";
					}
					byString += filter.FilterColumns[filterColCounter].Alias;
				}
				return "Get" + filter.Parent.Alias + byString;
			}
		}

		[Interfaces.Attributes.ApiExtension]
		public bool AliasValidate(Filter filter, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/
			if (!filter.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(filter.Alias))
			{
				failReason = "Alias cannot be zero-length.";
				return false;
			}
			if (filter.Alias.IndexOf(" ") >= 0)
			{
				failReason = "Alias cannot have spaces.";
				return false;
			}
			foreach (Filter sibling in filter.Parent.Filters)
			{
				if (sibling != filter && sibling.Enabled && ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.Alias, filter.Alias, false))
				{
					failReason = "Duplicate alias: " + filter.Alias;
					return false;
				}
			}
			return true;
		}


		[Interfaces.Attributes.ApiExtension]
		public string CustomWhereDefault(Filter filter)
		{
			return "";
		}


		[Interfaces.Attributes.ApiExtension]
		public bool CustomWhereValidate(Filter filter, out string failReason)
		{
			failReason = "";
			return true;
		}


		public void AddFilterColumn(FilterColumn filterColumn)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, filterColumn);
			_filterColumns.Add(filterColumn);
		}


		public void UpdateFilterColumn(int index, FilterColumn filterColumn)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _filterColumns[index], filterColumn);
			_filterColumns[index] = filterColumn;
		}


		public void AddOrderByColumn(OrderByColumn orderByColumn)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, orderByColumn);
			_orderByColumns.Add(orderByColumn);
		}


		public void RemoveOrderByColumn(OrderByColumn orderByColumn)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, orderByColumn);
			_orderByColumns.Remove(orderByColumn);
		}


		public void UpdateOrderByColumn(int index, OrderByColumn orderByColumn)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _orderByColumns[index], orderByColumn);
			_orderByColumns[index] = orderByColumn;
		}


		public void ClearFilterColumns()
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _filterColumns, null);
			_filterColumns = new List<FilterColumn>();
		}


		public void ClearOrderByColumns()
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _orderByColumns, null);
			_orderByColumns = new List<OrderByColumn>();
		}


		[Interfaces.Attributes.ApiExtension]
		public virtual string AliasPluralDefault(Filter filter)
		{
			return ArchAngel.Providers.Database.Helper.Script.GetPlural(filter.Alias);
		}


		[Interfaces.Attributes.ApiExtension]
		public bool AliasPluralValidate(Filter filter, out string failReason)
		{
			failReason = "";
			/*Don't check items that are not enabled*/
			if (!filter.Enabled)
			{
				return true;
			}
			if (string.IsNullOrEmpty(filter.AliasPlural))
			{
				filter.AliasPlural = "";
				//failReason = "AliasPlural cannot be zero-length.";
				//return false;
			}
			else if (filter.AliasPlural.IndexOf(" ") >= 0)
			{
				failReason = "AliasPlural cannot have spaces.";
				return false;
			}
			if (ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(filter.AliasPlural, filter.Alias, false))
			{
				failReason = "AliasPlural must be different to Alias.";
				return false;
			}

			//foreach (Filter sibling in filter.Parent.Filters)
			//{
			//    if (sibling != this && sibling.Enabled)
			//    {
			//        if (ArchAngel.ArchAngel.Providers.Database.Helper.Script.StringsAreEqual(sibling.AliasPlural, this.AliasPlural, false))
			//        {
			//            failReason = string.Format("Duplicate AliasPlural: {0}", this.AliasPlural);
			//            return false;
			//        }
			//    }
			//}
			return true;
		}


		public override bool AliasValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasValidate((Filter)scriptBase, out failReason);
		}


		public override string AliasPluralDefault(IScriptBase scriptBase)
		{
			return AliasPluralDefault((Filter)scriptBase);
		}


		public override bool AliasPluralValidate(IScriptBase scriptBase, out string failReason)
		{
			return AliasPluralValidate((Filter)scriptBase, out failReason);
		}

        /// <summary>
        /// Gets whether the Filter is in a valid state.
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
			if (!CustomWhereValidate(this, out tempFailReason))
			{
				isValid = false;
				failReason += string.Format("{1}.CustomWhere: {0}\n", tempFailReason, Name);
			}
			bool parentIsStoredProc = typeof(StoredProcedure).IsInstanceOfType(Parent);

			foreach (FilterColumn filterColumn in FilterColumns)
			{
				bool found = false;

				if (!parentIsStoredProc)
				{
					foreach (Column column in Parent.Columns)
					{

						if (filterColumn.Column.Name == column.Name)
						{
							found = true;
							break;
						}
					}
				}
				else
				{
					// Parent is a stored procedure, so need to check parameters instead
					foreach (StoredProcedure.Parameter parameter in ((StoredProcedure)Parent).Parameters)
					{
						if (filterColumn.Column.Name == parameter.Name)
						{
							found = true;
							break;
						}
					}
				}
				if (!found)
				{
					isValid = false;

					if (!parentIsStoredProc)
					{
						failReason += string.Format("{0}: FilterColumn [{1}] is missing from the columns collection of {2}.\n", Name, filterColumn.Column.Name, Parent.Alias);
					}
					else
					{
						// Parent is a stored procedure
						failReason += string.Format("{0}: FilterColumn [{1}] is missing from the parameters collection of {2}.\n", Name, filterColumn.Column.Name, Parent.Alias);
					}
				}
                //if (string.IsNullOrEmpty(filterColumn.CompareOperator))
                //{
                //    isValid = false;
                //    failReason += string.Format("{0}: FilterColumn [{1}] has no 'Compare Operator'.\n", Name, filterColumn.Column.Name);
                //}
                //if (string.IsNullOrEmpty(filterColumn.LogicalOperator))
                //{
                //    isValid = false;
                //    failReason += string.Format("{0}: FilterColumn [{1}] has no 'Logical Operator'.\n", Name, filterColumn.Column.Name);
                //}
			}
			if (deepCheck)
			{
				/*Check inner objects*/
				if (Key != null && !Key.IsValid(deepCheck, out tempFailReason))
				{
					isValid = false;
					failReason += string.Format("{0}.Key: {1}\n", Name, tempFailReason);
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
		public class FilterColumn : ISerializable
		{

			#region Fields
			[DotfuscatorDoNotRename]
			private Column _column;
			private readonly string _columnId;
			[DotfuscatorDoNotRename]
			private string _logicalOperator;
			[DotfuscatorDoNotRename]
			private string _compareOperator;
			[DotfuscatorDoNotRename]
			private string _alias;

			#endregion

			#region Constructors
			/// <summary>
			/// TODO: I don't think this should be exposed to the user???
			/// </summary>
			/// <param name="column">The underlying column.</param>
			/// <param name="logicalOperator">The logical operator to be used by the filter. One of 'And', 'Or'.</param>
			/// <param name="compareOperator">The operator to be used by the filter for comparison purposes. One of '=', '&lt;', '&gt;', '&gt;=', '&lt;=', '&lt;&gt;'.</param>
			/// <param name="alias">The name supplied by the user to refer to this filter.</param>
			public FilterColumn(Column column, string logicalOperator, string compareOperator, string alias)
			{
				/*Sanity-check*/
				if (column == null || string.IsNullOrEmpty(column.Name))
				{

					throw new InvalidOperationException("A null column is being added.");
				}
				_column = column;
				_logicalOperator = logicalOperator;
				_compareOperator = compareOperator;
				_alias = alias;
			}

			/// <exclude/>
			public FilterColumn(SerializationInfo serializationInfo, StreamingContext streamingContext)
			{
				if (SerializerHelper.UseFastSerialization)
				{
					using (SerializationReader reader = new SerializationReader((byte[])serializationInfo.GetValue("d", typeof(byte[]))))
					{
						_alias = reader.ReadString();
						_columnId = reader.ReadString();
						_compareOperator = reader.ReadString();
						_logicalOperator = reader.ReadString();
					}
				}
				else
				{
					if (SerializerHelper.FileVersionCurrent <= SerializerHelper.FileVersionLatest)
					{
						_alias = serializationInfo.GetString("Alias");
						_column = (Column)serializationInfo.GetValue("Column", ModelTypes.Column);
						_compareOperator = serializationInfo.GetString("CompareOperator");
						_logicalOperator = serializationInfo.GetString("LogicalOperator");
					}
					else
					{

						throw new NotImplementedException(string.Format("FilterColumn deserialize does not handle version {0} yet.", SerializerHelper.FileVersionCurrent));
					}
				}
			}

			#endregion

			#region Properties

			/// <summary>
			/// The Filter that this FilterColumn belongs to.
			/// </summary>
			public Filter Parent { get; set; }

			/// <summary>
			/// The underlying column.
			/// </summary>
			public Column Column
			{
				get
				{
					if (_column == null && _columnId != null)
					{
						_column = (Column)Lookups[_columnId];
					}
					return _column;
				}
			}

			/// <summary>
			/// The logical operator to be used by the filter. One of 'And', 'Or'. TODO: I think this should be an enum.
			/// </summary>
			public string LogicalOperator
			{
				get 
                {
                    //if (string.IsNullOrEmpty(_logicalOperator))
                    //{
                    //    _logicalOperator = "And";
                    //}
                    return _logicalOperator; 
                }
				set { _logicalOperator = value; }
			}

			/// <summary>
			/// The operator to be used by the filter for comparison purposes. One of '=', '&lt;', '&gt;', '&gt;=', '&lt;=', '&lt;&gt;'. TODO: I think this should be an enum.
			/// </summary>
			public string CompareOperator
			{
				get
				{
                    if (string.IsNullOrEmpty(_compareOperator))
                    {
                        _compareOperator = "=";
                    }
                    return _compareOperator;
				}

				set { _compareOperator = value; }
			}

			/// <summary>
			/// The name supplied by the user to refer to this filter.
			/// </summary>
			public string Alias
			{
				get
				{
					if (!string.IsNullOrEmpty(_alias) || !Interfaces.SharedData.IsBusyGenerating)
					{
                        if (string.IsNullOrEmpty(_alias))
                        {
                            return _column.Alias;
                        }
						return _alias;
					}
					return _column.Alias;
				}

				set { _alias = value; }
			}

			#endregion

			#region Functions
			/// <exclude/>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
			public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
			{
#if FAST_SERIALIZATION
				using (SerializationWriter writer = new SerializationWriter())
				{
					if (!_Parent.Parent.ColumnsSortedByUniqueId.ContainsKey(Column.UniqueId))
					{
						if (_Parent.Parent.ColumnsSortedByName.ContainsKey(Column.Name))
						{
							Column.UniqueId = _Parent.Parent.ColumnsSortedByName[Column.Name].UniqueId;
						}
					}
					writer.Write(_alias);
					writer.WriteObject(Column.UniqueId);
					writer.Write(_compareOperator);
					writer.Write(_logicalOperator);
					info.AddValue("d", writer.ToArray());
				}
#else
                info.AddValue("Alias", _alias);
                info.AddValue("Column", _column);
                info.AddValue("CompareOperator", _compareOperator);
                info.AddValue("LogicalOperator", _logicalOperator);
#endif
			}


			#endregion

		}

		[Serializable]
        [Interfaces.Attributes.ArchAngelEditor(false, false, "ColumnAlias")]
		public class OrderByColumn : ISerializable
		{

			#region Fields
			[DotfuscatorDoNotRename]
			private Column _column;
			private readonly string _columnId;
			[DotfuscatorDoNotRename]
			private string _sortOperator;
			#endregion

			#region Constructors
			public OrderByColumn(Column column, string sortOperator)
			{
				_column = column;
				_sortOperator = sortOperator;
			}

			/// <exclude/>
			public OrderByColumn(SerializationInfo serializationInfo, StreamingContext streamingContext)
			{
				if (SerializerHelper.UseFastSerialization)
				{
					using (SerializationReader reader = new SerializationReader((byte[])serializationInfo.GetValue("d", typeof(byte[]))))
					{
						_columnId = reader.ReadString();
						_sortOperator = reader.ReadString();
					}
				}
				else
				{
					if (SerializerHelper.FileVersionCurrent <= SerializerHelper.FileVersionLatest)
					{
						_column = (Column)serializationInfo.GetValue("Column", ModelTypes.Column);
						_sortOperator = serializationInfo.GetString("SortOperator");
					}
					else
					{

						throw new NotImplementedException(string.Format("OrderByColumn deserialize does not handle version {0} yet.", SerializerHelper.FileVersionCurrent));
					}
				}
			}

			#endregion

			#region Properties
			public Column Column
			{
				get
				{
					if (_column == null && _columnId != null)
					{
						_column = (Column)Lookups[_columnId];
					}
					return _column;
				}
			}

			public string SortOperator
			{
				get { return _sortOperator; }
				set { _sortOperator = value; }
			}

            public string ColumnAlias
            {
                get { return Column.Alias; }
            }
			#endregion

			#region Functions
			/// <exclude/>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
			public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
			{
#if FAST_SERIALIZATION
				using (SerializationWriter writer = new SerializationWriter())
				{
					writer.Write(Column.UniqueId);
					writer.Write(_sortOperator);
					info.AddValue("d", writer.ToArray());
				}
#else
				info.AddValue("Column", _column);
				info.AddValue("SortOperator", _sortOperator);
#endif
			}


			#endregion

		}

		#endregion

	}

}

