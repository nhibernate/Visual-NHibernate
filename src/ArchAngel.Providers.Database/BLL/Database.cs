using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;
// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.BLL
{
	#region Delegates

	//public delegate void ProcessingObjectDelegate(string objectName, string description);
	public delegate void FinishedDelegate();

	#endregion

	public class Database : ScriptBLL
	{
		#region Events

		//public event ProcessingObjectDelegate ObjectBeingProcessed;
		public event FinishedDelegate Finished;

		#endregion

		private readonly List<Model.Database> _databases = new List<Model.Database>();
		private readonly Model.Database.Compare AliasCompare = new Model.Database.Compare(new string[] { "Alias" });

		public Database()
		{
		}

		public Database(string filename)
		{
			try
			{
				if (!File.Exists(filename))
				{
					return;
				}
				_databases = SerializerHelper.Deserialize(filename);
				return;
			}
			catch (Exception ex)
			{
				string message = ex.Message;

				if (ex.InnerException != null)
				{
					message += "\n\nInner Exception:\n" + ex.InnerException.Message;
				}
				message += "\n\nStack Trace:\n" + ex.StackTrace;
				throw;
			}
		}

		Assembly CurrentDomain_TypeResolve(object sender, ResolveEventArgs args)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		private void RaiseFinishedEvent()
		{
			if (Finished != null)
			{
				Finished();
			}
		}

		public void Save(string filename)
		{
			if (_databases == null)
			{
				return;
			}
			string folder = Path.GetDirectoryName(filename);

			if (!Directory.Exists(folder))
			{
				// TODO: This is all f***d up. It is required due to the inconsistency caused by Common.TempBllDatabaseFilePath.
				// Look in the code to see why. Hint: a temp file is created during the Analysis step.
				filename = Path.Combine(Path.GetDirectoryName(folder), "provider_database.settings");
			}
			SerializerHelper.Serialize(filename, _databases);
			return;
		}

		public Model.Database[] Databases
		{
			get { return _databases.ToArray(); }
		}

		public void AddDatabase(Model.Database database)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, database);
			_databases.Add(database);
		}

		public void UpdateDatabase(Model.Database database, int index)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _databases[index], database);
			_databases[index] = database;
		}

		public void DeleteDatabase(Model.Database database)
		{
			Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), database, null);
			_databases.Remove(database);
		}

		public void AddDatabase(string name, DatabaseTypes databaseType, ConnectionStringHelper connectionString, string[] tablePrefixes, string[] viewPrefixes, string[] storedProcedurePredixes)
		{
			Model.Table.TablePrefixes = new List<string>(tablePrefixes);
			Model.View.ViewPrefixes = new List<string>(viewPrefixes);
			Model.StoredProcedure.StoredProcedurePrefixes = new List<string>(storedProcedurePredixes);

			Table bllTable = new Table(databaseType, connectionString);
			View bllView = new View(databaseType, connectionString);
			StoredProcedure bllStoredProcedure = new StoredProcedure(databaseType, connectionString);

			Model.Database database = new Model.Database(name, connectionString, databaseType, bllTable.Tables, bllView.Views, bllStoredProcedure.StoredProcedures);
			AddDatabase(database);
		}

		public void LoadNewDatabase(
			int index,
			string name,
			DatabaseTypes databaseType,
			ConnectionStringHelper connectionString,
			List<string> tablePrefixes,
			List<string> viewPrefixes,
			List<string> storedProcedurePredixes,
			bool fetchTables,
			bool fetchViews,
			bool fetchStroredProcedures)
		{
			Model.Database database = Model.Database.LoadNewDatabase(name, databaseType, connectionString, tablePrefixes, viewPrefixes, storedProcedurePredixes, fetchTables, fetchViews, fetchStroredProcedures);
			UpdateDatabase(database, index);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="oldDatabase"></param>
		/// <param name="index"></param>
		/// <param name="tablePrefixes"></param>
		/// <param name="viewPrefixes"></param>
		/// <param name="storedProcedurePredixes"></param>
		/// <param name="processTables"></param>
		/// <param name="processViews"></param>
		/// <param name="processStoredProcedures"></param>
		/// <returns>Returns a description of any errors that occurred or alerts that the user needs to be aware of.</returns>
		public string RefreshDatabase(
			Model.Database oldDatabase,
			int index,
			List<string> tablePrefixes,
			List<string> viewPrefixes,
			List<string> storedProcedurePredixes,
			bool processTables,
			bool processViews,
			bool processStoredProcedures)
		{
			Providers.Database.Helper.Utility.ResetAllConnections();
			StringBuilder sb = new StringBuilder(1000);
			Table bllTable = null;
			View bllView = null;
			StoredProcedure bllStoredProcedure = null;

			Model.Table.TablePrefixes = tablePrefixes;
			Model.View.ViewPrefixes = viewPrefixes;
			Model.StoredProcedure.StoredProcedurePrefixes = storedProcedurePredixes;
            Model.Table[] tables = null;
            Model.View[] views = null;
            Model.StoredProcedure[] storedProcedures = null;

            if (processTables)
            {
                Interfaces.Events.RaiseObjectBeingProcessedEvent("DB", "Reading tables...");
                bllTable = new Table(oldDatabase.DatabaseType, oldDatabase.ConnectionString);
                tables = bllTable.Tables;
            }
            else
            {
                tables = oldDatabase.Tables;
            }
            if (processViews)
            {
                Interfaces.Events.RaiseObjectBeingProcessedEvent("DB", "Reading views...");
                bllView = new View(oldDatabase.DatabaseType, oldDatabase.ConnectionString);
                views = bllView.Views;
            }
            else
            {
                views = oldDatabase.Views;
            }
            if (processStoredProcedures)
            {
                Interfaces.Events.RaiseObjectBeingProcessedEvent("DB", "Reading stored procedures...");
                bllStoredProcedure = new StoredProcedure(oldDatabase.DatabaseType, oldDatabase.ConnectionString);
                storedProcedures = bllStoredProcedure.StoredProcedures;

                foreach (string error in bllStoredProcedure.ErrorMessages)
                {
                    sb.AppendLine(error);
                }
            }
            else
            {
                storedProcedures = oldDatabase.StoredProcedures;
            }
			Model.Database newDatabase = new Model.Database(oldDatabase.Name, oldDatabase.ConnectionString, oldDatabase.DatabaseType, tables, views, storedProcedures);
			string errors = UpdateScriptObjects(newDatabase, oldDatabase, processTables, processViews, processStoredProcedures);

			if (errors.Length < 0)
			{
				sb.AppendLine(errors);
			}
			UpdateDatabase(newDatabase, index);
			return sb.ToString();
		}

		public void FillStoredProcedureColumns(Model.StoredProcedure storedProcedure)
		{
			StoredProcedure bllStoredProcedure = new StoredProcedure(storedProcedure.Database.DatabaseType, storedProcedure.Database.ConnectionString);
			List<Column> oldColumns = new List<Column>(storedProcedure.Columns);

			bllStoredProcedure.FillStoredProcedureColumns(storedProcedure);

			// Update new columns with old properties
			foreach (Column newColumn in storedProcedure.Columns)
			{
				// Find old column if it exists
				Column oldColumn = Search.GetColumn(oldColumns.ToArray(), newColumn.Name, false);
				if (oldColumn == null)
				{
					continue;
				}

				// Update new column
				newColumn.Enabled = oldColumn.Enabled;
				newColumn.Alias = oldColumn.Alias;
				newColumn.AliasDisplay = oldColumn.AliasDisplay;
			}
		}

		/// <summary>
		/// Updates objects that have just been read from the database with any changes the user made, such as renaming.
		/// </summary>
		/// <param name="newDatabase"></param>
		/// <param name="oldDatabase"></param>
		/// <param name="processViews"></param>
		/// <param name="processStoredProcedures"></param>
		/// <param name="processTables"></param>
		/// <returns>Returns a description of any errors that occurred or alerts that the user needs to be aware of.</returns>
		private string UpdateScriptObjects(
            Model.Database newDatabase, 
            Model.Database oldDatabase,
            bool processTables,
            bool processViews,
            bool processStoredProcedures)
		{
			oldDatabase.SnapshotMode = true;

			StringBuilder sb = new StringBuilder(1000);

			#region Script Objects

			// Find old user defined script objects and add to new database
			ScriptObject[] userDefinedScriptObjects = GetUserDefinedScriptObjects(oldDatabase.AllScriptObjects);

			foreach (ScriptObject scriptObject in userDefinedScriptObjects)
			{
				newDatabase.AddScriptObject(scriptObject);
				Interfaces.Events.RaiseObjectBeingProcessedEvent(scriptObject.Name, "Script Object");
				//RaiseObjectBeingProcessedEvent(scriptObject.Name, "Script Object");
			}

			#endregion

			#region Force update of the cached ScriptObjects
            oldDatabase.ResetAllScriptObjects();
			ScriptObject[] ScriptObjects = oldDatabase.AllScriptObjects;
			newDatabase.SnapshotMode = true;
            newDatabase.ResetAllScriptObjects();
			#endregion

			#region Columns

			Interfaces.Events.RaiseObjectBeingProcessedEvent("", "Updating Columns");

			// Update new script objects with old columns
			foreach (ScriptObject newScriptObject in newDatabase.AllScriptObjects)
			{
                if ((newScriptObject.IsTable && !processTables) ||
                    (newScriptObject.IsStoredProcedure && !processStoredProcedures) ||
                    (newScriptObject.IsView && !processViews))
                {
                    continue;
                }
				// Find old script object if it exists
				ScriptObject oldScriptObject = Search.GetScriptObject(oldDatabase.AllScriptObjects, newScriptObject, newScriptObject.GetType(), false);

				if (oldScriptObject == null || oldScriptObject.IsUserDefined)
				{
					continue;
				}
				// Find old user defined columns and add to script object
				Column[] userDefinedColumns = GetUserDefinedColumns(oldScriptObject.Columns);
				foreach (Column oldColumn in userDefinedColumns)
				{
					if (oldColumn.GetType() == typeof(Column))
					{
						Interfaces.Events.RaiseObjectBeingProcessedEvent(oldColumn.Name, "Column");
						//RaiseObjectBeingProcessedEvent(oldColumn.Name, "Column");
						Column newColumn = new Column(oldColumn.Name, oldColumn.IsUserDefined, newScriptObject,
							newScriptObject.Columns.Length, oldColumn.IsNullable, oldColumn.DataType, oldColumn.Size, oldColumn.InPrimaryKey,
							oldColumn.IsIdentity, oldColumn.Default, oldColumn.ReadOnly, oldColumn.IsCalculated, oldColumn.Precision, oldColumn.Scale);

						newColumn.Alias = oldColumn.Alias;
						newColumn.AliasDisplay = oldColumn.AliasDisplay;
						newScriptObject.AddColumn(newColumn);
					}
				}

				if (newScriptObject.GetType() == typeof(Model.StoredProcedure) && oldScriptObject.Enabled)
				{
					// Execute Stored Procedure if enabled and retrieve columns
					FillStoredProcedureColumns((Model.StoredProcedure)newScriptObject);
				}
				foreach (string error in newScriptObject.Errors)
				{
					sb.AppendLine(error);
				}
			}

			#endregion

			#region Map Columns

			Interfaces.Events.RaiseObjectBeingProcessedEvent("", "Updating Map Columns");

			// Update new script objects with old properties
			foreach (ScriptObject newScriptObject in newDatabase.AllScriptObjects)
			{
                if ((newScriptObject.IsTable && !processTables) ||
                    (newScriptObject.IsStoredProcedure && !processStoredProcedures) ||
                    (newScriptObject.IsView && !processViews))
                {
                    continue;
                }
				// Find old script object if it exists
				ScriptObject oldScriptObject = Search.GetScriptObject(oldDatabase.AllScriptObjects, newScriptObject, newScriptObject.GetType(), false);
				if (oldScriptObject == null)
				{
					continue;
				}
				// Find old user defined columns and add to script object
				Column[] userDefinedColumns = GetUserDefinedColumns(oldScriptObject.Columns);
				foreach (Column oldColumn in userDefinedColumns)
				{
					if (ModelTypes.MapColumn.IsInstanceOfType(oldColumn))
					{
						Interfaces.Events.RaiseObjectBeingProcessedEvent(oldColumn.Name, "Map Column");
						//RaiseObjectBeingProcessedEvent(oldColumn.Name, "Map Column");
						MapColumn oldMapColumn = (MapColumn)oldColumn;

						// We insert a null RelationshipPath here. Don't worry, because we wire up the correct ones below in the region called 'Update Map Column Relationship Path and Foreign Column'
						MapColumn newMapColumn = new MapColumn(oldMapColumn.Name, oldMapColumn.IsUserDefined,
							null, null, newScriptObject.Columns.Length, oldMapColumn.IsNullable, oldMapColumn.DataType, oldMapColumn.Size, oldMapColumn.Precision, oldMapColumn.Scale);

						newMapColumn.Alias = oldMapColumn.Alias;
						newMapColumn.AliasDisplay = oldMapColumn.AliasDisplay;

						if (oldMapColumn.RelationshipPath != null)
						{
							//foreach (Relationship rel in oldMapColumn.RelationshipPath)
							//{
							//    Relationship newRel = Search.GetRelationship(newScriptObject.Relationships, rel.Name, false);
							//}
						}
						//newMapColumn.Parent = newScriptObject;
						newScriptObject.AddColumn(newMapColumn);
					}
				}
			}

			#endregion

			#region Key and Index

			ArchAngel.Interfaces.Events.RaiseObjectBeingProcessedEvent("", "Updating Keys and Indexes");

            if (processTables)
            {
                // Update new script objects with old properties
				foreach (Model.Table newTable in newDatabase.Tables)
				{
					// Find old script object if it exists
					Model.Table oldTable = Search.GetTable(oldDatabase.Tables, newTable, false);
					if (oldTable == null)
					{
						continue;
					}
					// Find old user defined keys
					Key[] userDefinedKeys = GetUserDefinedKeys(oldTable.Keys);

					foreach (Key oldKey in userDefinedKeys)
					{
						Interfaces.Events.RaiseObjectBeingProcessedEvent(oldKey.Name, "User-Defined Key");
						// TODO: Add new key
						//Key newKey = new Key(oldKey.Name, oldKey.Alias, oldKey.IsUserDefined, oldKey.AliasDisplay, newScriptObject, newScriptObject.Keys.Length, oldKey.IsNullable, oldKey.DataType, oldKey.Size, oldKey.InPrimaryKey, oldKey.IsMap, oldKey.IsIdentity);
						//newTable.AddKey(newKey);
					}
					// Find old user defined indexes
					Index[] userDefinedIndexes = GetUserDefinedIndexes(oldTable.Indexes);

					foreach (Index oldIndex in userDefinedIndexes)
					{
						Interfaces.Events.RaiseObjectBeingProcessedEvent(oldIndex.Name, "User-Defined Index");
						// TODO: Add new index
						//Index newIndex = new Index(oldIndex.Name, oldIndex.Alias, oldIndex.IsUserDefined, oldIndex.AliasDisplay, newScriptObject, newScriptObject.Indexs.Length, oldIndex.IsNullable, oldIndex.DataType, oldIndex.Size, oldIndex.InPrimaryIndex, oldIndex.IsMap, oldIndex.IsIdentity);
						//newTable.AddIndex(newIndex);
					}
				}
            }

			#endregion

			#region Filter

			Interfaces.Events.RaiseObjectBeingProcessedEvent("", "Updating Filters");

			// Update new script objects with old properties
			foreach (ScriptObject newScriptObject in newDatabase.AllScriptObjects)
			{
                if ((newScriptObject.IsTable && !processTables) ||
                    (newScriptObject.IsStoredProcedure && !processStoredProcedures) ||
                    (newScriptObject.IsView && !processViews))
                {
                    continue;
                }
				// Find old script object if it exists
				ScriptObject oldScriptObject = Search.GetScriptObject(oldDatabase.AllScriptObjects, newScriptObject, newScriptObject.GetType(), false);

				if (oldScriptObject == null || oldScriptObject.IsUserDefined)
				{
					continue;
				}
				// Find old user defined filters
				Filter[] userDefinedFilters = GetUserDefinedFilters(oldScriptObject.Filters);

				foreach (Filter oldFilter in userDefinedFilters)
				{
					Interfaces.Events.RaiseObjectBeingProcessedEvent(oldFilter.Name, "Filter");

					Filter newFilter = new Filter(oldFilter.Name, oldFilter.IsUserDefined, newScriptObject, oldFilter.IsReturnTypeCollection, oldFilter.CreateStoredProcedure,
						oldFilter.UseCustomWhere, oldFilter.CustomWhere, oldFilter.Key);

					newFilter.Alias = oldFilter.Alias;
					newFilter.Name = oldFilter.Name;
					newFilter.CustomWhere = oldFilter.CustomWhere;
					newFilter.CreateStoredProcedure = oldFilter.CreateStoredProcedure;
					newFilter.Enabled = oldFilter.Enabled;
					newFilter.IsReturnTypeCollection = oldFilter.IsReturnTypeCollection;

					foreach (Filter.FilterColumn oldFilterColumn in oldFilter.FilterColumns)
					{
						Column newFilterColumnColumn = GetNewScriptBase(newDatabase.AllScriptObjects, oldFilter, oldFilterColumn.Column);

						Filter.FilterColumn newFilterColumn = new Filter.FilterColumn(newFilterColumnColumn, oldFilterColumn.LogicalOperator, oldFilterColumn.CompareOperator, oldFilterColumn.Alias);
						newFilter.AddFilterColumn(newFilterColumn);
					}
					foreach (Filter.OrderByColumn oldOrderByColumn in oldFilter.OrderByColumns)
					{
						Column newOrderByColumnColumn = GetNewScriptBase(newDatabase.AllScriptObjects, oldFilter, oldOrderByColumn.Column);

						Filter.OrderByColumn newOrderByColumn = new Filter.OrderByColumn(newOrderByColumnColumn, oldOrderByColumn.SortOperator);
						newFilter.AddOrderByColumn(newOrderByColumn);
					}
					newScriptObject.AddFilter(newFilter);
				}
			}

			#endregion

			#region Relationship

			Interfaces.Events.RaiseObjectBeingProcessedEvent("", "Updating Relationships");

			// Update new script objects with old properties
			foreach (ScriptObject newScriptObject in newDatabase.AllScriptObjects)
			{
                if ((newScriptObject.IsTable && !processTables) ||
                    (newScriptObject.IsStoredProcedure && !processStoredProcedures) ||
                    (newScriptObject.IsView && !processViews))
                {
                    continue;
                }
				// Find old script object if it exists
				ScriptObject oldScriptObject = Search.GetScriptObject(oldDatabase.AllScriptObjects, newScriptObject, newScriptObject.GetType(), false);

				if (oldScriptObject == null)// || oldScriptObject.IsUserDefined)
				{
					continue;
				}
				// Find old user defined relationships
				Relationship[] userDefinedRelationships = GetUserDefinedRelationships(oldScriptObject.Relationships);

				foreach (Relationship oldRelationship in userDefinedRelationships)
				{
					if (oldScriptObject.IsUserDefined)
					{
						oldScriptObject.RemoveRelationship(oldRelationship);
					}
					Interfaces.Events.RaiseObjectBeingProcessedEvent(oldRelationship.Name, "Relationship");

					// Find relationship script objects in new database schema
					ScriptObject newPrimaryScriptObject = GetNewScriptBase(newDatabase.AllScriptObjects, oldRelationship.PrimaryScriptObject);
					ScriptObject newForeignScriptObject = GetNewScriptBase(newDatabase.AllScriptObjects, oldRelationship.ForeignScriptObject);
					
					if (newPrimaryScriptObject == null || newForeignScriptObject == null)
					{
						// The corresponding ScriptObjects have been deleted, so remove the Relationship
						// TODO: inform the user about Relationships that have been removed, and the reason.
						continue;
					}
					// Find relationship columns in new databse schema
					Column[] newPrimaryColumns = GetNewScriptBase(newDatabase.AllScriptObjects, oldRelationship, oldRelationship.PrimaryColumns);
					Column[] newForeignColumns = GetNewScriptBase(newDatabase.AllScriptObjects, oldRelationship, oldRelationship.ForeignColumns);

					if (oldRelationship.GetType() == typeof(OneToOneRelationship))
					{
						// Find relationship filter in new databse schema
						Filter newFilter = GetNewScriptBase(newScriptObject, oldRelationship, oldRelationship.Filter);

						OneToOneRelationship oldOneToOneRelationship = (OneToOneRelationship)oldRelationship;
						OneToOneRelationship newRelationship = new OneToOneRelationship(oldOneToOneRelationship.Name, oldOneToOneRelationship.IsUserDefined,
							 newPrimaryScriptObject, newPrimaryColumns, newForeignScriptObject, newForeignColumns, newFilter, oldOneToOneRelationship.IsBase);

						Relationship foreignRelationship = null;

						if (oldScriptObject.IsUserDefined)
						{
							foreignRelationship = GetNewScriptBase(newDatabase.AllScriptObjects, oldRelationship.ForeignRelationship);
						}
						if (oldRelationship.ForeignScriptObject.IsUserDefined)
						{
							foreignRelationship = GetNewScriptBase(newDatabase.AllScriptObjects, oldRelationship.ForeignRelationship);
						}
						if (foreignRelationship != null)
						{
							newRelationship.ForeignRelationship = foreignRelationship;
							foreignRelationship.ForeignRelationship = newRelationship;
						}
						newRelationship.Alias = oldOneToOneRelationship.Alias;
						newScriptObject.AddRelationship(newRelationship);
					}

					if (oldRelationship.GetType() == typeof(OneToManyRelationship))
					{
						// Find relationship filter in new databse schema
						Filter newFilter = GetNewScriptBase(newScriptObject, oldRelationship, oldRelationship.Filter);

						OneToManyRelationship oldOneToManyRelationship = (OneToManyRelationship)oldRelationship;
						OneToManyRelationship newRelationship = new OneToManyRelationship(oldOneToManyRelationship.Name, oldOneToManyRelationship.IsUserDefined,
							 newPrimaryScriptObject, newPrimaryColumns, newForeignScriptObject, newForeignColumns, newFilter);

						newRelationship.Alias = oldOneToManyRelationship.Alias;
						newScriptObject.AddRelationship(newRelationship);
					}

					if (oldRelationship.GetType() == typeof(ManyToOneRelationship))
					{
						// Find relationship filter in new databse schema
						Filter newFilter = GetNewScriptBase(newScriptObject, oldRelationship, oldRelationship.Filter);

						ManyToOneRelationship oldManyToOneRelationship = (ManyToOneRelationship)oldRelationship;
						ManyToOneRelationship newRelationship = new ManyToOneRelationship(oldManyToOneRelationship.Name, oldManyToOneRelationship.IsUserDefined,
							 newPrimaryScriptObject, newPrimaryColumns, newForeignScriptObject, newForeignColumns, newFilter);

						newRelationship.Alias = oldManyToOneRelationship.Alias;
						newScriptObject.AddRelationship(newRelationship);
					}

					//if (oldRelationship.GetType() == typeof(ManyToManyRelationship))
					//{
					//    ManyToManyRelationship oldManyToManyRelationship = (ManyToManyRelationship)oldRelationship;
					//}
				}
			}

			#endregion

			#region Update Foreign Relationship

			Interfaces.Events.RaiseObjectBeingProcessedEvent("", "Updating Foreign Relationships");

		    foreach (ScriptObject newScriptObject in newDatabase.AllScriptObjects)
			{
                if ((newScriptObject.IsTable && !processTables) ||
                    (newScriptObject.IsStoredProcedure && !processStoredProcedures) ||
                    (newScriptObject.IsView && !processViews))
                {
                    continue;
                }
				// Find old script object if it exists
				ScriptObject oldScriptObject = Search.GetScriptObject(oldDatabase.AllScriptObjects, newScriptObject, newScriptObject.GetType(), false);

				if (oldScriptObject == null)// || oldScriptObject.IsUserDefined)
				{
					continue;
				}
				// Hookup foreign relationships
				// Find old user defined relationships
				Relationship[] userDefinedRelationships = GetUserDefinedRelationships(oldScriptObject.Relationships);

				for (int relationshipCounter = userDefinedRelationships.Length - 1; relationshipCounter >= 0; relationshipCounter--)
				{
					Relationship oldRelationship = userDefinedRelationships[relationshipCounter];

					if (oldRelationship.GetType() == typeof(ManyToManyRelationship))
					{
						continue;
					}
					Interfaces.Events.RaiseObjectBeingProcessedEvent(oldRelationship.Name, "User-Defined Relationship");
					ScriptObject newPrimaryScriptObject = GetNewScriptBase(newDatabase.AllScriptObjects, oldRelationship.PrimaryScriptObject);
					ScriptObject newForeignScriptObject = GetNewScriptBase(newDatabase.AllScriptObjects, oldRelationship.ForeignScriptObject);

					if (newPrimaryScriptObject == null || newForeignScriptObject == null)
					{
						// The corresponding ScriptObjects have been deleted, so remove the Relationship
						// TODO: inform the user about Relationships that have been removed, and the reason.
						continue;
					}
					Relationship newPrimaryRelationship = GetNewScriptBase(newPrimaryScriptObject, oldRelationship);
					Relationship newForeignRelationship = GetNewScriptBase(newForeignScriptObject, oldRelationship.ForeignRelationship);

					if (newPrimaryRelationship == null || newForeignRelationship == null)
					{
						// The corresponding ScriptObjects have been deleted, so remove the Relationship
						// TODO: inform the user about Relationships that have been removed, and the reason.
						continue;
					}
					newPrimaryRelationship.ForeignRelationship = newForeignRelationship;
					newForeignRelationship.ForeignRelationship = newPrimaryRelationship;
				}
			}

			#endregion

			#region Create Many to Many Relationship

			ArchAngel.Interfaces.Events.RaiseObjectBeingProcessedEvent("", "Updating Many to Many Relationships");

			foreach (ScriptObject newScriptObject in newDatabase.AllScriptObjects)
			{
                if ((newScriptObject.IsTable && !processTables) ||
                    (newScriptObject.IsStoredProcedure && !processStoredProcedures) ||
                    (newScriptObject.IsView && !processViews))
                {
                    continue;
                }
				// Find old script object if it exists
				ScriptObject oldScriptObject = Search.GetScriptObject(oldDatabase.AllScriptObjects, newScriptObject, newScriptObject.GetType(), false);
				if (oldScriptObject == null)
				{
					continue;
				}
				// Find old user defined relationships
				Relationship[] userDefinedRelationships = GetUserDefinedRelationships(oldScriptObject.Relationships);
				foreach (Relationship oldRelationship in userDefinedRelationships)
				{
					// Add userdefined may to many relationships here as they are based on other relationships that must all be loaded
					if (oldRelationship.GetType() == typeof(ManyToManyRelationship))
					{
						Interfaces.Events.RaiseObjectBeingProcessedEvent(oldRelationship.Name, "Many-To-Many Relationship");
						//ScriptObject newPrimaryScriptObject = GetNewScriptBase(newDatabase.AllScriptObjects, oldRelationship, oldRelationship.PrimaryScriptObject);
						//ScriptObject newForeignScriptObject = GetNewScriptBase(newDatabase.AllScriptObjects, oldRelationship, oldRelationship.ForeignScriptObject);
						Filter newFilter = GetNewScriptBase(newScriptObject, oldRelationship, oldRelationship.Filter);

						ManyToManyRelationship oldManyToManyRelationship = (ManyToManyRelationship)oldRelationship;

						ScriptObject newIntermediateScriptObject = GetNewScriptBase(newDatabase.AllScriptObjects, oldManyToManyRelationship.IntermediateForeignScriptObject);

						OneToManyRelationship newIntermediatePrimaryRelationship = (OneToManyRelationship)GetNewScriptBase(newScriptObject, oldManyToManyRelationship.IntermediatePrimaryRelationship);
						ManyToOneRelationship newIntermediateForeignRelationship = (ManyToOneRelationship)GetNewScriptBase(newIntermediateScriptObject, oldManyToManyRelationship.IntermediateForeignRelationship);

						ManyToManyRelationship newRelationship = new ManyToManyRelationship(oldManyToManyRelationship.Name, oldManyToManyRelationship.IsUserDefined,
							  newIntermediatePrimaryRelationship, newIntermediateForeignRelationship, newFilter);

						newRelationship.Alias = oldManyToManyRelationship.Alias;
						newScriptObject.AddRelationship(newRelationship);
					}
				}
			}

			#endregion

			#region Update Remaining Properties

			// Update new script objects with old properties
			foreach (ScriptObject newScriptObject in newDatabase.AllScriptObjects)
			{
                if ((newScriptObject.IsTable && !processTables) ||
                    (newScriptObject.IsStoredProcedure && !processStoredProcedures) ||
                    (newScriptObject.IsView && !processViews))
                {
                    continue;
                }
				// Find old script object if it exists
				ScriptObject oldScriptObject = Search.GetScriptObject(oldDatabase.AllScriptObjects, newScriptObject, newScriptObject.GetType(), false);

				if (oldScriptObject == null)
				{
					continue;
				}
				Interfaces.Events.RaiseObjectBeingProcessedEvent(oldScriptObject.Name, "Update Remaining Properties");

				// Update new script object
				newScriptObject.Enabled = oldScriptObject.Enabled;
				newScriptObject.Alias = oldScriptObject.Alias;
				newScriptObject.AliasPlural = oldScriptObject.AliasPlural;
				newScriptObject.Ex = oldScriptObject.Ex;

				// Update new columns with old properties
				foreach (Column newColumn in newScriptObject.Columns)
				{
					// Find old column if it exists
                    Column oldColumn;

                    if (ModelTypes.MapColumn.IsInstanceOfType(newColumn))
                    {
                        // Lookup MapColumns by using Alias as well, otherwise we have a problem when multiple MapColumns link to the
                        // same foreign column.
                        oldColumn = Search.GetColumn(oldScriptObject.Columns, newColumn.Name, newColumn.Alias, false);
                    }
                    else
                    {
                        oldColumn = Search.GetColumn(oldScriptObject.Columns, newColumn.Name, false);
                    }
                    if (oldColumn == null)
					{
						continue;
					}
					// Update new column
					newColumn.Enabled = oldColumn.Enabled;
                    newColumn.IsNullable = oldColumn.IsNullable;
					newColumn.Alias = oldColumn.Alias;
					newColumn.AliasDisplay = oldColumn.AliasDisplay;
					newColumn.Ex = oldColumn.Ex;
					newColumn.Parent = Search.GetScriptObject(newDatabase.AllScriptObjects, oldColumn.Parent, oldColumn.Parent.GetType(), false);
				}
				// TODO: the code below should be refactored to use the Search class instead of traversing the individual elements. Eg: Search.GetFilterColumns
				// This will reduce the amount of duplicate code.

				#region Update new filters with old properties
				foreach (Filter newFilter in newScriptObject.Filters)
				{
					#region Find old filter if it exists
					Filter oldFilter = Search.GetFilter(oldScriptObject.Filters, newFilter.Name, false);

					if (oldFilter == null)
					{
						oldFilter = Search.GetFilter(oldScriptObject.Filters, newFilter);
					}
					if (oldFilter == null)
					{
						continue;
					}
					#endregion

					SyncFilterProperties(sb, newFilter, oldFilter);
				}
				#endregion

				#region Update new relationships with old properties
				foreach (Relationship newRelationship in newScriptObject.Relationships)
				{
					Relationship oldRelationship = Search.GetRelationship(oldScriptObject.Relationships, newRelationship.Name, false);
					if (oldRelationship == null)
					{
						continue;
					}

					// Update new relationship
					newRelationship.Enabled = oldRelationship.Enabled;
					newRelationship.Alias = oldRelationship.Alias;
					newRelationship.Ex = oldRelationship.Ex;
				}
				#endregion

				if (newScriptObject.GetType() == typeof(Model.Table))
				{
					Model.Table oldTable = (Model.Table)oldScriptObject;
					Model.Table newTable = (Model.Table)newScriptObject;

					// Update new keys with old properties
					foreach (Key newKey in newTable.Keys)
					{
						Key oldKey = Search.GetKey(oldTable.Keys, newKey.Name, false);
						if (oldKey == null)
						{
							continue;
						}

						// Update new key
						newKey.Enabled = oldKey.Enabled;
						newKey.Alias = oldKey.Alias;
						newKey.Ex = oldKey.Ex;
					}

					// Update new indexes with old properties
					foreach (Index newIndex in newTable.Indexes)
					{
						Index oldIndex = Search.GetIndex(oldTable.Indexes, newIndex.Name, false);
						if (oldIndex == null)
						{
							continue;
						}

						// Update new index
						newIndex.Enabled = oldIndex.Enabled;
						newIndex.Alias = oldIndex.Alias;
						newIndex.Ex = oldIndex.Ex;
					}
				}
			}

			#endregion

			#region Update Map Column Relationship Path and Foreign Column

			Interfaces.Events.RaiseObjectBeingProcessedEvent("", "Updating Map Columns Relationships");

			// Update new script objects with old properties
			foreach (ScriptObject newScriptObject in newDatabase.AllScriptObjects)
			{
                if ((newScriptObject.IsTable && !processTables) ||
                    (newScriptObject.IsStoredProcedure && !processStoredProcedures) ||
                    (newScriptObject.IsView && !processViews))
                {
                    continue;
                }
				// Find old script object if it exists
				ScriptObject oldScriptObject = Search.GetScriptObject(oldDatabase.AllScriptObjects, newScriptObject, newScriptObject.GetType(), false);
				if (oldScriptObject == null)
				{
					continue;
				}
				// Find old user defined columns and add to script object
				Column[] userDefinedColumns = GetUserDefinedColumns(oldScriptObject.Columns);

				//foreach (Column oldColumn in userDefinedColumns)
				for (int columnCounter = userDefinedColumns.Length - 1; columnCounter >= 0; columnCounter--)
				{
					Column oldColumn = userDefinedColumns[columnCounter];

					if (ModelTypes.MapColumn.IsInstanceOfType(oldColumn))
					{
						//ArchAngel.Interfaces.ProjectHelper.RaiseObjectBeingProcessedEvent(oldColumn.Name, "User-Defined Column");
						MapColumn oldMapColumn = (MapColumn)oldColumn;

						// Find foreign column's parent in new database schema
						ScriptObject newForeignScriptObject = null;

						if (oldMapColumn.ForeignColumn != null)
						{
							newForeignScriptObject = GetNewScriptBase(newDatabase.AllScriptObjects, oldMapColumn.ForeignColumn.Parent);
						}
						else
						{
							oldScriptObject.RemoveColumn(oldColumn);
							continue;
						}

						// Find foreign column in new database schema
						Column newForeignColumn = GetNewScriptBase(newDatabase.AllScriptObjects, oldMapColumn, oldMapColumn.ForeignColumn);

						Relationship[] newRelationshipPath = new Relationship[oldMapColumn.RelationshipPath.Length];
						for (int i = 0; i < oldMapColumn.RelationshipPath.Length; i++)
						{
							Relationship newRelationship = GetNewScriptBase(newScriptObject, oldMapColumn.RelationshipPath[i]);
							newRelationshipPath[i] = newRelationship;
						}

                        MapColumn newMapColumn = (MapColumn)Search.GetColumn(newScriptObject.Columns, oldMapColumn.Name, oldMapColumn.Alias);
						newMapColumn.RelationshipPath = newRelationshipPath;
						newMapColumn.ForeignColumn = newForeignColumn;
					}
				}
			}

			#endregion

			newDatabase.SnapshotMode = false;
			oldDatabase.SnapshotMode = false;
			return sb.ToString();
		}

		/// <summary>
		/// Update newFilter with the properties of the oldFilter.
		/// </summary>
		/// <param name="sb"></param>
		/// <param name="newFilter"></param>
		/// <param name="oldFilter"></param>
		internal static void SyncFilterProperties(StringBuilder sb, Filter newFilter, Filter oldFilter)
		{
			#region Update new filter's primary properties
			newFilter.Enabled = oldFilter.Enabled;
			newFilter.Alias = oldFilter.Alias;
			newFilter.Ex = oldFilter.Ex;

			newFilter.Name = oldFilter.Name;
			newFilter.CustomWhere = oldFilter.CustomWhere;
			newFilter.CreateStoredProcedure = oldFilter.CreateStoredProcedure;
			newFilter.IsReturnTypeCollection = oldFilter.IsReturnTypeCollection;
			#endregion

			#region FilterColumns
			foreach (Filter.FilterColumn newColumn in newFilter.FilterColumns)
			{
				foreach (Filter.FilterColumn oldColumn in oldFilter.FilterColumns)
				{
					if (newColumn.Column.Name == oldColumn.Column.Name &&
						newColumn.Column.Parent.Name == oldColumn.Column.Parent.Name)
					{
						newColumn.Alias = oldColumn.Alias;
						newColumn.CompareOperator = oldColumn.CompareOperator;
						newColumn.LogicalOperator = oldColumn.LogicalOperator;
						break;
					}
				}
			}
			#endregion

			#region OrderByColumns
			for (int orderColumnCounter = newFilter.OrderByColumns.Length - 1; orderColumnCounter >= 0; orderColumnCounter--)
			{
				Filter.OrderByColumn newColumn = newFilter.OrderByColumns[orderColumnCounter];
				bool found = false;

				foreach (Filter.OrderByColumn oldColumn in oldFilter.OrderByColumns)
				{
					if (newColumn.Column.Name == oldColumn.Column.Name &&
						newColumn.Column.Parent.Name == oldColumn.Column.Parent.Name)
					{
						newColumn.SortOperator = oldColumn.SortOperator;
						found = true;
						break;
					}
				}
				if (!found)
				{
					newFilter.RemoveOrderByColumn(newColumn);
				}
			}
			#endregion

			#region Add missing FilterColumns
			foreach (Filter.FilterColumn oldColumn in oldFilter.FilterColumns)
			{
				bool found = false;

				foreach (Filter.FilterColumn newColumn in newFilter.FilterColumns)
				{
					if (newColumn.Column.Name == oldColumn.Column.Name &&
						newColumn.Column.Parent.Name == oldColumn.Column.Parent.Name)
					{
						found = true;
						break;
					}
				}
				if (!found)
				{
					bool success = false;

					foreach (Column column in newFilter.Parent.Columns)
					{
						if (column.Name == oldColumn.Column.Name &&
							column.Parent.Name == oldColumn.Column.Parent.Name)
						{
							Filter.FilterColumn filterColumn = new Filter.FilterColumn(column, oldColumn.LogicalOperator, oldColumn.CompareOperator, oldColumn.Alias);
							newFilter.AddFilterColumn(filterColumn);
							success = true;
							break;
						}
					}
					if (!success)
					{
						//sb.AppendLine(string.Format("Filter column is missing and couldn't be updated. Filter:{0}, Column:{1}.{2}", newFilter.Alias, oldColumn.Column.Parent.Name, oldColumn.Column.Alias));
					}
				}
			}
			#endregion

			#region Add missing OrderByColumns
			foreach (Filter.OrderByColumn oldColumn in oldFilter.OrderByColumns)
			{
				bool found = false;

				foreach (Filter.OrderByColumn newColumn in newFilter.OrderByColumns)
				{
					if (newColumn.Column.Name == oldColumn.Column.Name &&
						newColumn.Column.Parent.Name == oldColumn.Column.Parent.Name)
					{
						found = true;
						break;
					}
				}
				if (!found)
				{
					bool success = false;

					foreach (Column column in newFilter.Parent.Columns)
					{
						if (column.Name == oldColumn.Column.Name &&
							column.Parent.Name == oldColumn.Column.Parent.Name)
						{
							Filter.OrderByColumn orderByColumn = new Filter.OrderByColumn(column, oldColumn.SortOperator);
							newFilter.AddOrderByColumn(orderByColumn);
							success = true;
							break;
						}
					}
					if (!success)
					{
						sb.AppendLine(string.Format("OrderBy column is missing and couldn't be updated. Filter:{0}, Column:{1}", newFilter.Alias, oldColumn.Column.Parent.Name + oldColumn.Column.Alias));
					}
				}
			}
			#endregion
		}

		private static ScriptObject GetNewScriptBase(ScriptObject[] newScriptObjects, ScriptObject oldScriptObject)
		{
			// Look for script object still existing
			ScriptObject newScriptObject = Search.GetScriptObject(newScriptObjects, oldScriptObject, oldScriptObject.GetType(), false);
			//if (newScriptObject == null)
			//{
			//    string errorMessage = oldImmediateParent.GetType().Name + " " + oldImmediateParent.Alias + " references deleted script object " + oldScriptObject.Alias +
			//        ".\n" + oldScriptObject.GetType().Name + " " + oldScriptObject.Alias + " does not exist anymore." +
			//        "\nPlease delete " + oldScriptObject.GetType().Name + " " + oldScriptObject.Alias + " from this " + oldImmediateParent.GetType().Name;
			//    throw new Exception(errorMessage);
			//}

			return newScriptObject;
		}

		private static Column GetNewScriptBase(ScriptObject[] newScriptObjects, ScriptBase oldImmediateParent, Column oldColumn)
		{
			// Look for script object still existing
			ScriptObject newScriptObject = GetNewScriptBase(newScriptObjects, oldColumn.Parent);

			// Look for column still existing
			Column newColumn = Search.GetColumn(newScriptObject.Columns, oldColumn.Name, false);

			// Stored procedure create columns in place of parameters in order to create filters
			if (newColumn == null && newScriptObject.GetType() == typeof(Model.StoredProcedure))
			{
				Model.StoredProcedure newStoredProcedure = (Model.StoredProcedure)newScriptObject;
				Model.StoredProcedure.Parameter parameter = Search.GetParameter(newStoredProcedure.Parameters, oldColumn.Name);

				if (parameter == null)
				{
					string errorMessage = oldImmediateParent.GetType().Name + " " + oldImmediateParent.Alias + " references deleted parameter " + oldColumn.Alias + " of script object " + oldColumn.Parent.Alias +
						".\n" + oldColumn.GetType().Name + " " + oldColumn.Alias + " does not exist anymore." +
						"\nPlease delete " + oldColumn.GetType().Name + " " + oldColumn.Alias + " from this " + oldImmediateParent.GetType().Name;
					throw new Exception(errorMessage);
				}

				// Create columns for parameter as it is used as a filter column
				newColumn = new Column(parameter.Name, false, newScriptObject, parameter.OrdinalPosition, false, parameter.DataType,
						parameter.Size, false, false, "", true, false, parameter.Precision, parameter.Scale);
			}

			if (newColumn == null)
			{
				string errorMessage = oldImmediateParent.GetType().Name + " " + oldImmediateParent.Alias + " references deleted column " + oldColumn.Alias + " of script object " + oldColumn.Parent.Alias +
					".\n" + oldColumn.GetType().Name + " " + oldColumn.Alias + " does not exist anymore." +
					"\nPlease delete " + oldColumn.GetType().Name + " " + oldColumn.Alias + " from this " + oldImmediateParent.GetType().Name;
				throw new Exception(errorMessage);
			}

			return newColumn;
		}

		private static Relationship GetNewScriptBase(ScriptObject newScriptObject, Relationship oldRelationship)
		{
			Relationship newRelationship = Search.GetRelationship(newScriptObject.Relationships, oldRelationship.Name, false);

			//if (newRelationship == null)
			//{
			//    string errorMessage = string.Format("{0} {1} references deleted relationship {2} of script object {3}.\n {4} does not exist anymore.\nPlease delete {5} {6} from this {7}.", 
			//        oldImmediateParent.GetType().Name,
			//        oldImmediateParent.Alias,
			//        oldRelationship.Alias,
			//        oldRelationship.Parent.Alias,
			//        oldRelationship.GetType().Name,
			//        oldRelationship.Alias,
			//        oldRelationship.GetType().Name,
			//        oldRelationship.Alias,
			//        oldImmediateParent.GetType().Name);
			//    throw new Exception(errorMessage);
			//}

			return newRelationship;
		}

		private static Relationship GetNewScriptBase(ScriptObject[] newScriptObjects, Relationship oldRelationship)
		{
			ScriptObject newScriptObject = GetNewScriptBase(newScriptObjects, oldRelationship.Parent);
			Relationship newRelationship = Search.GetRelationship(newScriptObject.Relationships, oldRelationship.Name, false);

			//if (newRelationship == null)
			//{
			//    string errorMessage = oldImmediateParent.GetType().Name + " " + oldImmediateParent.Alias + " references deleted relationship " + oldRelationship.Alias + " of script object " + oldRelationship.Parent.Alias +
			//        ".\n" + oldRelationship.GetType().Name + " " + oldRelationship.Alias + " does not exist anymore." +
			//        "\nPlease delete " + oldRelationship.GetType().Name + " " + oldRelationship.Alias + " from this " + oldImmediateParent.GetType().Name;
			//    throw new Exception(errorMessage);
			//}
			return newRelationship;
		}

		private static Filter GetNewScriptBase(ScriptObject newScriptObject, IScriptBase oldImmediateParent, Filter oldFilter)
		{
			Filter newFilter = null;

			try
			{
				newFilter = Search.GetFilter(newScriptObject.Filters, oldFilter.Name, false);

				if (newFilter == null)
				{
					newFilter = Search.GetFilter(newScriptObject.Filters, oldFilter);
				}
				if (newFilter == null)
				{
					// Filters should never just disappear. instead of throwing an exception we'll
					// add it back in.
					newFilter = new Filter();
					newFilter.Parent = newScriptObject;
					SyncFilterProperties(new StringBuilder(), newFilter, oldFilter);

					//string errorMessage = string.Format("{0} [{2}.{1}] references missing filter [{2}.{3}].\n Select another filter or create a new one.", 
					//    oldImmediateParent.GetType().Name,
					//    oldImmediateParent.Alias,
					//    oldFilter.Parent.Alias, 
					//    oldFilter.Alias);
					//throw new Exception(errorMessage);
				}
			}
			catch
			{
				if (newFilter == null)
				{
					string errorMessage = string.Format("{0} [{2}.{1}] references missing filter [{2}.{3}].\n Select another filter or create a new one.",
						oldImmediateParent.GetType().Name,
						oldImmediateParent.Alias,
						oldFilter.Parent.Alias,
						oldFilter.Alias);
					throw new Exception(errorMessage);
				}
			}
			return newFilter;
		}

		private static Column[] GetNewScriptBase(ScriptObject[] newScriptObjects, ScriptBase oldImmediateParent, Column[] oldColumns)
		{
			Column[] newColumns = new Column[oldColumns.Length];
			for (int i = 0; i < oldColumns.Length; i++)
			{
				Column oldColumn = oldColumns[i];
				Column newColumn = GetNewScriptBase(newScriptObjects, oldImmediateParent, oldColumn);
				newColumns[i] = newColumn;
			}

			return newColumns;
		}

		public ScriptObject[] ScriptObjects
		{
			get
			{
				int total = 0;
				int index = 0;

				foreach (Model.Database database in _databases)
				{
					total = database.ScriptObjects.Length;
				}
				ScriptObject[] scriptObjects = new ScriptObject[total];

				foreach (Model.Database database in _databases)
				{
					foreach (ScriptObject obj in database.ScriptObjects)
					{
						scriptObjects[index++] = obj;
					}
				}
				Array.Sort(scriptObjects, AliasCompare);
				return scriptObjects;
			}
		}

		public ScriptObject[] AllScriptObjects
		{
			get
			{
				int total = 0;
				int index = 0;

				foreach (Model.Database database in _databases)
				{
					total = database.AllScriptObjects.Length;
				}
				ScriptObject[] scriptObjects = new ScriptObject[total];

				foreach (Model.Database database in _databases)
				{
					foreach (ScriptObject obj in database.AllScriptObjects)
					{
						scriptObjects[index++] = obj;
					}
				}
				Array.Sort(scriptObjects, AliasCompare);
				return scriptObjects;
			}
		}

		public ScriptObject[] EnabledScriptObjects
		{
			get
			{
				int total = 0;
				int index = 0;

				foreach (Model.Database database in _databases)
				{
					total = database.EnabledScriptObjects.Length;
				}
				ScriptObject[] enabledScriptObjects = new ScriptObject[total];

				foreach (Model.Database database in _databases)
				{
					foreach (ScriptObject obj in database.EnabledScriptObjects)
					{
						enabledScriptObjects[index++] = obj;
					}
				}
				Array.Sort(enabledScriptObjects, AliasCompare);
				return enabledScriptObjects;
			}
		}

		public ScriptObject[] UserDefinedScriptObjects
		{
			get
			{
				int total = 0;
				int index = 0;

				foreach (Model.Database database in _databases)
				{
					total = database.UserDefinedScriptObjects.Length;
				}
				ScriptObject[] userDefinedScriptObjects = new ScriptObject[total];

				foreach (Model.Database database in _databases)
				{
					foreach (ScriptObject obj in database.UserDefinedScriptObjects)
					{
						userDefinedScriptObjects[index++] = obj;
					}
				}
				Array.Sort(userDefinedScriptObjects, AliasCompare);
				return userDefinedScriptObjects;
			}
		}
	}
}
