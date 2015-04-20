using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using DevComponents.AdvTree;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class ModelChanges : UserControl
	{
		internal EventHandler RefreshCalled;
		private IDatabase DB1;
		private IDatabase DB2;
		private const int IMG_TABLE = 0;
		private const int IMG_NEW = 1;
		private const int IMG_CHANGED = 2;
		private const int IMG_REMOVED = 3;
		private bool BusyPopulating = false;
		private ArchAngel.Interfaces.Scripting.DatabaseChanges.IDatabase ScriptModelDatabase;

		public ModelChanges()
		{
			InitializeComponent();

			nodeNewEntities.Visible = false;
			nodeModifiedEntities.Visible = false;
			nodeRemovedEntities.Visible = false;

			nodeEmpty.Visible = true;

			PopulateDatabaseTypes();
		}

		public void ShowChangeScripts(string databaseName, bool changesOnly)
		{
			if (changesOnly)
				ScriptModelDatabase = FillScriptModel(databaseName);
			else
				ScriptModelDatabase = FillScriptModelCreateAll();
		}

		private ArchAngel.Interfaces.Scripting.DatabaseChanges.IDatabase FillScriptModel(string databaseName)
		{
			ArchAngel.Interfaces.Scripting.DatabaseChanges.IDatabase database = new Interfaces.Scripting.DatabaseChanges.IDatabase(databaseName)
			{
				//DatabaseType = (Interfaces.ProjectOptions.TypeMappings.Utility.DatabaseTypes)Enum.Parse(typeof(Interfaces.ProjectOptions.TypeMappings.Utility.DatabaseTypes), )
				DatabaseType = Interfaces.ProjectOptions.TypeMappings.Utility.DatabaseTypes.SqlServer
			};
			//database.

			List<ITwoStepMergeOperation> twoStepOps = new List<ITwoStepMergeOperation>();
			List<TableRemovalOperation> addedTableOps = new List<TableRemovalOperation>();
			List<TableAdditionOperation> removedTableOps = new List<TableAdditionOperation>();
			Dictionary<ITable, object> changedTableOps = new Dictionary<ITable, object>();

			List<IMergeOperation<IColumn>> changedColumnOps = new List<IMergeOperation<IColumn>>();
			List<IMergeOperation<IIndex>> changedIndexOps = new List<IMergeOperation<IIndex>>();
			List<IMergeOperation<IKey>> changedKeyOps = new List<IMergeOperation<IKey>>();
			List<IMergeOperation<Relationship>> changedRelationshipOps = new List<IMergeOperation<Relationship>>();

			foreach (Node node in nodeNewEntities.Nodes)
				if (node.Checked)
					addedTableOps.Add((TableRemovalOperation)node.Tag);

			foreach (Node node in nodeRemovedEntities.Nodes)
				if (node.Checked)
					removedTableOps.Add((TableAdditionOperation)node.Tag);

			foreach (Node node in nodeModifiedEntities.Nodes)
				if (node.Checked)
				{
					foreach (Node childNode in node.Nodes)
					{
						if (childNode.Tag is ColumnAdditionOperation ||
							childNode.Tag is ColumnChangeOperation ||
							childNode.Tag is ColumnRemovalOperation)
						{
							changedColumnOps.Add((IMergeOperation<IColumn>)childNode.Tag);
						}
						else if (childNode.Tag is IndexAdditionOperation ||
							childNode.Tag is IndexChangeOperation ||
							childNode.Tag is IndexRemovalOperation)
						{
							changedIndexOps.Add((IMergeOperation<IIndex>)childNode.Tag);
						}
						else if (childNode.Tag is KeyAdditionOperation ||
							childNode.Tag is KeyChangeOperation ||
							childNode.Tag is KeyRemovalOperation)
						{
							changedKeyOps.Add((IMergeOperation<IKey>)childNode.Tag);
						}
						else if (childNode.Tag is RelationshipAdditionOperation ||
						childNode.Tag is RelationshipChangeOperation ||
						childNode.Tag is RelationshipRemovalOperation)
						{
							changedRelationshipOps.Add((IMergeOperation<Relationship>)childNode.Tag);
						}
						else
							throw new NotImplementedException("Modification type not handled yet: " + childNode.Tag.GetType().Name);
					}
					//changedTableOps.Add((TableChangeOperation)node.Tag);
				}

			#region Added Tables

			foreach (var op in addedTableOps)
			{
				ITable oldTable = op.Object;

				ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable newTable = new Interfaces.Scripting.NHibernate.Model.ITable(databaseName)
				{
					Name = oldTable.Name,
					Schema = oldTable.Schema
				};
				database.NewTables.Add(newTable);

				foreach (IColumn oldColumn in oldTable.Columns)
					newTable.Columns.Add(ConvertColumn(oldColumn));

				foreach (IIndex oldIndex in oldTable.Indexes)
				{
					ArchAngel.Interfaces.Scripting.NHibernate.Model.IIndex newIndex = new Interfaces.Scripting.NHibernate.Model.IIndex()
					{
					};
					newTable.Indexes.Add(newIndex);
				}
				foreach (IKey oldKey in oldTable.ForeignKeys)
				{
					ArchAngel.Interfaces.Scripting.NHibernate.Model.IKey newKey = new Interfaces.Scripting.NHibernate.Model.IKey()
					{
						Name = oldKey.Name
					};
					foreach (IColumn oldColumn in oldKey.Columns)
						newKey.Columns.Add(ConvertColumn(oldColumn));

					newTable.ForeignKeys.Add(newKey);
				}
				if (oldTable.FirstPrimaryKey != null)
				{
					newTable.PrimaryKey = new Interfaces.Scripting.NHibernate.Model.IKey()
					{
						Name = oldTable.FirstPrimaryKey.Name
					};
					foreach (IColumn oldColumn in oldTable.FirstPrimaryKey.Columns)
						newTable.PrimaryKey.Columns.Add(ConvertColumn(oldColumn));

				}
			}
			#endregion

			#region Removed Tables
			foreach (var op in removedTableOps)
			{
				ITable oldTable = op.Object;

				ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable newTable = new Interfaces.Scripting.NHibernate.Model.ITable(databaseName)
				{
					Name = oldTable.Name,
					Schema = oldTable.Schema
				};
				database.RemovedTables.Add(newTable);
			}
			#endregion

			#region New, Changed, Removed columns

			foreach (var op in changedColumnOps)
			{
				IColumn oldColumn = op.Object;
				ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable table = GetChangedTable(database, oldColumn.Parent);

				if (op is ColumnAdditionOperation)
					table.RemovedColumns.Add(ConvertColumn(oldColumn));
				else if (op is ColumnChangeOperation)
				{
					ColumnChangeOperation changeOp = (ColumnChangeOperation)op;
					ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedColumn changedColumn = new Interfaces.Scripting.DatabaseChanges.IChangedColumn();
					//changeOp.
					IColumn chColumn = (IColumn)changeOp.Object;
					changedColumn.OldName = oldColumn.Name;
					changedColumn.NewName = chColumn.Name;
					changedColumn.NewType = chColumn.OriginalDataType;
					changedColumn.NewLength = chColumn.Size;

					table.ChangedColumns.Add(changedColumn);
				}
				else if (op is ColumnRemovalOperation)
					table.NewColumns.Add(ConvertColumn(oldColumn));
			}
			#endregion

			#region New, Changed, Removed indexes

			foreach (var op in changedIndexOps)
			{
				IIndex oldIndex = op.Object;
				ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable table = GetChangedTable(database, oldIndex.Parent);
				ArchAngel.Interfaces.Scripting.NHibernate.Model.IIndex newIndex = new Interfaces.Scripting.NHibernate.Model.IIndex()
				{
					Name = oldIndex.Name
				};

				if (op is IndexAdditionOperation)
				{
					foreach (IColumn oldColumn in oldIndex.Columns)
						newIndex.Columns.Add(ConvertColumn(oldColumn));

					table.RemovedIndexes.Add(newIndex);
				}
				else if (op is IndexChangeOperation)
					table.ChangedIndexes.Add(newIndex);
				else if (op is IndexRemovalOperation)
					table.NewIndexes.Add(newIndex);
			}
			#endregion

			#region New, Changed, Removed keys

			foreach (var op in changedKeyOps)
			{
				IKey oldKey = op.Object;
				ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable table = GetChangedTable(database, oldKey.Parent);
				ArchAngel.Interfaces.Scripting.NHibernate.Model.IKey newKey = new Interfaces.Scripting.NHibernate.Model.IKey()
				{
					Name = oldKey.Name,
					TableName = oldKey.Parent.Name,
					TableSchema = oldKey.Parent.Schema
				};
				newKey.KeyType = (Interfaces.Scripting.NHibernate.Model.IKey.KeyTypes)Enum.Parse(typeof(Interfaces.Scripting.NHibernate.Model.IKey.KeyTypes), oldKey.Keytype.ToString());

				foreach (IColumn oldColumn in oldKey.Columns)
					newKey.Columns.Add(ConvertColumn(oldColumn));

				if (newKey.KeyType == Interfaces.Scripting.NHibernate.Model.IKey.KeyTypes.Foreign)
				{
					ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable primaryTable = GetChangedTable(database, oldKey.ReferencedKey.Parent);
					ArchAngel.Interfaces.Scripting.NHibernate.Model.IKey newPrimaryKey = new Interfaces.Scripting.NHibernate.Model.IKey()
					{
						Name = oldKey.ReferencedKey.Name,
						TableSchema = oldKey.ReferencedKey.Parent.Schema,
						TableName = oldKey.ReferencedKey.Parent.Name
					};
					newPrimaryKey.KeyType = (Interfaces.Scripting.NHibernate.Model.IKey.KeyTypes)Enum.Parse(typeof(Interfaces.Scripting.NHibernate.Model.IKey.KeyTypes), oldKey.ReferencedKey.Keytype.ToString());

					foreach (IColumn oldColumn in oldKey.ReferencedKey.Columns)
						newPrimaryKey.Columns.Add(ConvertColumn(oldColumn));

					newKey.ReferencedPrimaryKey = newPrimaryKey;
				}

				if (op is KeyAdditionOperation)
				{
					if (newKey.KeyType == Interfaces.Scripting.NHibernate.Model.IKey.KeyTypes.Foreign)
						table.RemovedForeignKeys.Add(newKey);
					else if (newKey.KeyType == Interfaces.Scripting.NHibernate.Model.IKey.KeyTypes.Primary)
						table.RemovedPrimaryKeys.Add(newKey);
					else
						throw new NotImplementedException("KeyType not handled yet: " + newKey.KeyType.ToString());
				}
				else if (op is KeyChangeOperation)
				{
					if (newKey.KeyType == Interfaces.Scripting.NHibernate.Model.IKey.KeyTypes.Foreign)
						table.ChangedForeignKeys.Add(newKey);
					else if (newKey.KeyType == Interfaces.Scripting.NHibernate.Model.IKey.KeyTypes.Primary)
						table.ChangedPrimaryKeys.Add(newKey);
					else
						throw new NotImplementedException("KeyType not handled yet: " + newKey.KeyType.ToString());
				}
				else if (op is KeyRemovalOperation)
				{
					if (newKey.KeyType == Interfaces.Scripting.NHibernate.Model.IKey.KeyTypes.Foreign)
						table.NewForeignKeys.Add(newKey);
					else if (newKey.KeyType == Interfaces.Scripting.NHibernate.Model.IKey.KeyTypes.Primary)
						table.NewPrimaryKeys.Add(newKey);
					else
						throw new NotImplementedException("KeyType not handled yet: " + newKey.KeyType.ToString());
				}
			}
			#endregion

			// Clean-up non-changed tables
			database.ChangedTables = database.ChangedTables.Where(t => t.CountOfChanges > 0).ToList();
			return database;
		}

		private ArchAngel.Interfaces.Scripting.DatabaseChanges.IDatabase FillScriptModelCreateAll()
		{
			ArchAngel.Interfaces.Scripting.DatabaseChanges.IDatabase database = new Interfaces.Scripting.DatabaseChanges.IDatabase(DB1.Name)
			{
				DatabaseType = Interfaces.ProjectOptions.TypeMappings.Utility.DatabaseTypes.SqlServer
			};
			Dictionary<IKey, ArchAngel.Interfaces.Scripting.NHibernate.Model.IKey> primaryKeyLookups = new Dictionary<IKey, Interfaces.Scripting.NHibernate.Model.IKey>();
			Dictionary<ITable, ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable> tableLookups = new Dictionary<ITable, Interfaces.Scripting.NHibernate.Model.ITable>();

			foreach (var table in DB1.Tables)
			{
				ITable oldTable = table;

				ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable newTable = new Interfaces.Scripting.NHibernate.Model.ITable(DB1.Name)
				{
					Name = oldTable.Name,
					Schema = oldTable.Schema
				};
				tableLookups.Add(oldTable, newTable);
				database.NewTables.Add(newTable);

				foreach (IColumn oldColumn in oldTable.Columns.OrderBy(c => c.OrdinalPosition))
					newTable.Columns.Add(ConvertColumn(oldColumn));

				foreach (IIndex oldIndex in oldTable.Indexes)
				{
					ArchAngel.Interfaces.Scripting.NHibernate.Model.IIndex newIndex = new Interfaces.Scripting.NHibernate.Model.IIndex()
					{
					};
					newTable.Indexes.Add(newIndex);
				}
				if (oldTable.FirstPrimaryKey != null)
				{
					newTable.PrimaryKey = new Interfaces.Scripting.NHibernate.Model.IKey()
					{
						Name = oldTable.FirstPrimaryKey.Name,
						TableName = oldTable.Name,
						TableSchema = oldTable.Schema
					};
					foreach (IColumn oldColumn in oldTable.FirstPrimaryKey.Columns.OrderBy(c => c.OrdinalPosition))
						newTable.PrimaryKey.Columns.Add(ConvertColumn(oldColumn));

					primaryKeyLookups.Add(oldTable.FirstPrimaryKey, newTable.PrimaryKey);

				}
			}
			// Foreign keys
			foreach (var table in DB1.Tables)
			{
				ITable oldTable = table;

				foreach (IKey oldKey in oldTable.ForeignKeys)
				{
					ArchAngel.Interfaces.Scripting.NHibernate.Model.IKey newKey = new Interfaces.Scripting.NHibernate.Model.IKey()
					{
						Name = oldKey.Name,
						ReferencedPrimaryKey = primaryKeyLookups[oldKey.ReferencedKey]
					};
					foreach (IColumn oldColumn in oldKey.Columns.OrderBy(c => c.OrdinalPosition))
						newKey.Columns.Add(ConvertColumn(oldColumn));

					tableLookups[oldTable].ForeignKeys.Add(newKey);
				}
			}

			return database;
		}

		private ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn ConvertColumn(IColumn oldColumn)
		{
			ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn newColumn = new Interfaces.Scripting.NHibernate.Model.IColumn()
			{
				Name = oldColumn.Name,
				IsNullable = oldColumn.IsNullable,
				//IsText = oldColumn.ist
				Length = oldColumn.Size,
				Type = oldColumn.OriginalDataType,
				IsIdentity = oldColumn.IsIdentity
			};
			return newColumn;
		}

		private ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable GetChangedTable(ArchAngel.Interfaces.Scripting.DatabaseChanges.IDatabase database, ITable oldTable)
		{
			ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable table = database.ChangedTables.SingleOrDefault(t => t.Name == oldTable.Name && t.Schema == oldTable.Schema);

			if (table == null)
			{
				table = new ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable(database.Name)
				{
					Name = oldTable.Name,
					Schema = oldTable.Schema
				};
				database.ChangedTables.Add(table);
			}
			return table;
		}

		public void Fill(DatabaseMergeResult results, IDatabase db1, IDatabase db2)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => Fill(results, db1, db2)));
				return;
			}
			DB1 = db1;
			DB2 = db2;

			advTreeTables.BeginUpdate();
			nodeNewEntities.Nodes.Clear();
			nodeModifiedEntities.Nodes.Clear();
			nodeRemovedEntities.Nodes.Clear();

			nodeNewEntities.Checked = true;
			nodeModifiedEntities.Checked = true;
			nodeRemovedEntities.Checked = false;

			try
			{
				#region Database-only

				foreach (TableAdditionOperation addedTableOperation in results.TableOperations.Where(r => r is TableAdditionOperation))
				{
					Node node = new Node(string.Format("{0}.{1}", addedTableOperation.Object.Schema, addedTableOperation.Object.Name));
					node.Tag = addedTableOperation;
					node.CheckBoxVisible = true;
					node.Checked = false;
					node.ImageIndex = IMG_NEW;
					nodeRemovedEntities.Nodes.Add(node);
				}

				#endregion

				#region Model-only
				IList<SynchroGrid.SynchroCanvas.TableMatch> MatchesModelOnly = new List<SynchroGrid.SynchroCanvas.TableMatch>();

				foreach (TableRemovalOperation removedTableOperation in results.TableOperations.Where(r => r is TableRemovalOperation))
				{
					Node node = new Node(string.Format("{0}.{1}", removedTableOperation.Object.Schema, removedTableOperation.Object.Name));
					node.Tag = removedTableOperation;
					node.CheckBoxVisible = true;
					node.Checked = true;
					node.ImageIndex = IMG_REMOVED;
					nodeNewEntities.Nodes.Add(node);
				}

				#endregion

				#region Changes
				HashSet<string> changedTables = new HashSet<string>();

				#region Get tables with changes
				foreach (var result in results.ColumnOperations)
					changedTables.Add(string.Format("{0}.{1}", result.Object.Parent.Schema, result.Object.Parent.Name));

				foreach (var result in results.IndexOperations)
					changedTables.Add(string.Format("{0}.{1}", result.Object.Parent.Schema, result.Object.Parent.Name));

				foreach (var result in results.KeyOperations)
					changedTables.Add(string.Format("{0}.{1}", result.Object.Parent.Schema, result.Object.Parent.Name));

				//foreach (var result in results.RelationshipOperations)
				//    changedTables.Add(string.Format("{0}.{1}", result.Object.PrimaryTable.Schema, result.Object.PrimaryTable.Name));

				#endregion

				foreach (string changedTable in changedTables)
				{
					Node node = new Node(changedTable);
					node.Tag = changedTable;
					node.CheckBoxVisible = true;
					node.Checked = true;
					node.ImageIndex = IMG_CHANGED;
					nodeModifiedEntities.Nodes.Add(node);

					#region Columns
					foreach (ColumnAdditionOperation newColumn in results.ColumnOperations.Where(r => r is ColumnAdditionOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node columnnode = new Node(string.Format("Remove column from database: [{0}]", newColumn.Object.Name));
						columnnode.Tag = newColumn;
						node.Nodes.Add(columnnode);
					}
					foreach (ColumnChangeOperation changedColumn in results.ColumnOperations.Where(r => r is ColumnChangeOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node columnnode = new Node(string.Format("Update column in database [{0}]", changedColumn.Object.Name));
						columnnode.Tag = changedColumn;
						node.Nodes.Add(columnnode);

						foreach (string d in changedColumn.Description.Split(','))
							columnnode.Nodes.Add(new Node(d));
					}
					foreach (ColumnRemovalOperation removedColumn in results.ColumnOperations.Where(r => r is ColumnRemovalOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node columnnode = new Node(string.Format("Add column to database: [{0}]", removedColumn.Object.Name));
						columnnode.Tag = removedColumn;
						node.Nodes.Add(columnnode);
					}
					#endregion

					#region Indexes
					foreach (IndexAdditionOperation newIndex in results.IndexOperations.Where(r => r is IndexAdditionOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node indexNode = new Node(string.Format("Remove index from database: [{0}]", newIndex.Object.Name));
						indexNode.Tag = newIndex;
						node.Nodes.Add(indexNode);
					}
					foreach (IndexChangeOperation changedIndex in results.IndexOperations.Where(r => r is IndexChangeOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node indexNode = new Node(string.Format("Update index in database [{0}]", changedIndex.Object.Name));
						indexNode.Tag = changedIndex;
						node.Nodes.Add(indexNode);

						foreach (string d in changedIndex.Description.Split(','))
							indexNode.Nodes.Add(new Node(d));
					}
					foreach (IndexRemovalOperation removedIndex in results.IndexOperations.Where(r => r is IndexRemovalOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node indexNode = new Node(string.Format("Add index to database: [{0}]", removedIndex.Object.Name));
						indexNode.Tag = removedIndex;
						node.Nodes.Add(indexNode);
					}
					#endregion

					#region Keys
					foreach (KeyAdditionOperation newKey in results.KeyOperations.Where(r => r is KeyAdditionOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node keyNode = new Node(string.Format("Remove key from database: [{0}]", newKey.Object.Name));
						keyNode.Tag = newKey;
						node.Nodes.Add(keyNode);
					}
					foreach (KeyChangeOperation changedKey in results.KeyOperations.Where(r => r is KeyChangeOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node keyNode = new Node(string.Format("Update key in database [{0}]", changedKey.Object.Name));
						keyNode.Tag = changedKey;
						node.Nodes.Add(keyNode);

						foreach (string d in changedKey.Description.Split(','))
							keyNode.Nodes.Add(new Node(d));
					}
					foreach (KeyRemovalOperation removedKey in results.KeyOperations.Where(r => r is KeyRemovalOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node keyNode = new Node(string.Format("Add key to database: [{0}]", removedKey.Object.Name));
						keyNode.Tag = removedKey;
						node.Nodes.Add(keyNode);
					}
					#endregion

					#region Relationships
					//foreach (RelationshipAdditionOperation newRelationship in results.RelationshipOperations.Where(r => r is RelationshipAdditionOperation).Where(c => string.Format("{0}.{1}", c.Object.PrimaryTable.Schema, c.Object.PrimaryTable.Name) == changedTable).Distinct())
					//{
					//    Node relationshipNode = new Node(string.Format("Remove relationship from database, with {0}: [{1}]", newRelationship.Object.ForeignTable.Name, newRelationship.Object.Name));
					//    relationshipNode.Tag = newRelationship;
					//    node.Nodes.Add(relationshipNode);
					//}
					//foreach (RelationshipChangeOperation changedRelationship in results.RelationshipOperations.Where(r => r is RelationshipChangeOperation).Where(c => string.Format("{0}.{1}", c.Object.PrimaryTable.Schema, c.Object.PrimaryTable.Name) == changedTable))
					//{
					//    Node relationshipNode = new Node(string.Format("Update relationship in database, with {0}:  [{1}]", changedRelationship.Object.ForeignTable.Name, changedRelationship.Object.Name));
					//    relationshipNode.Tag = changedRelationship;
					//    node.Nodes.Add(relationshipNode);

					//    foreach (string d in changedRelationship.Description.Split(','))
					//        relationshipNode.Nodes.Add(new Node(d));
					//}
					//foreach (RelationshipRemovalOperation removedRelationship in results.RelationshipOperations.Where(r => r is RelationshipRemovalOperation).Where(c => string.Format("{0}.{1}", c.Object.PrimaryTable.Schema, c.Object.PrimaryTable.Name) == changedTable))
					//{
					//    Node relationshipNode = new Node(string.Format("Add relationship to database, with {0}: [{1}]", removedRelationship.Object.ForeignTable.Name, removedRelationship.Object.Name));
					//    relationshipNode.Tag = removedRelationship;
					//    node.Nodes.Add(relationshipNode);
					//}
					#endregion
				}
				#endregion

				nodeNewEntities.Visible = nodeNewEntities.Nodes.Count > 0;
				nodeModifiedEntities.Visible = nodeModifiedEntities.Nodes.Count > 0;
				nodeRemovedEntities.Visible = nodeRemovedEntities.Nodes.Count > 0;

				if (!nodeNewEntities.Visible && !nodeModifiedEntities.Visible && !nodeRemovedEntities.Visible)
				{
					nodeEmpty.Text = "The database has no changes";
					nodeEmpty.Visible = true;
				}
				else
					nodeEmpty.Visible = false;
			}
			finally
			{
				advTreeTables.EndUpdate();
			}
		}

		private string GetScriptForAll(bool changesOnly)
		{
			if (BusyPopulating || DB1 == null)
				return "";

			if (comboBoxDatabaseTypes.SelectedItem == null)
			{
				MessageBox.Show(this, "Select a database-type.", "Invalid database", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return "";
			}
			if (DB1 == null)
			{
				if (RefreshCalled != null)
					RefreshCalled(null, null);
				else
					return "";
			}
			while (ArchAngel.Providers.EntityModel.UI.PropertyGrids.FormDatabase.RefreshingSchema)
			{
				Application.DoEvents();
				System.Threading.Thread.Sleep(100);
			}
			ShowChangeScripts(DB1.Name, changesOnly);

			ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.ScriptRunnerContainer scriptContainer = null;
			string db = comboBoxDatabaseTypes.SelectedItem.ToString();

			if (db == "SQL Server 2005/2008/Azure")
			{
				scriptContainer = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.ContainerForSqlServer;
				Slyce.Common.SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorScript, Slyce.Common.TemplateContentLanguage.Sql, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp, @"<%", @"%>");
			}
			else if (db == "Oracle")
			{
				scriptContainer = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.ContainerForOracle;
				Slyce.Common.SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorScript, Slyce.Common.TemplateContentLanguage.Sql, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp, @"<%", @"%>");
			}
			else if (db == "MySQL 5")
			{
				scriptContainer = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.ContainerForMySql;
				Slyce.Common.SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorScript, Slyce.Common.TemplateContentLanguage.Sql, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp, @"<%", @"%>");
			}
			else
			{
				MessageBox.Show(this, "Only SQL Server is supported at this time. Support for other databases is coming soon.", "Invalid database", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return "";
			}
			//else if (db == "PostgreSQL")
			//{
			//    scriptContainer = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.ContainerForPostgreSql;
			//    Slyce.Common.SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorScript, Slyce.Common.TemplateContentLanguage.Sql, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp, @"<%", @"%>");
			//}
			//else if (db == "Firebird")
			//{
			//    scriptContainer = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.ContainerForFirebird;
			//    Slyce.Common.SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorScript, Slyce.Common.TemplateContentLanguage.Sql, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp, @"<%", @"%>");
			//}
			//else
			//    throw new NotImplementedException("Database type not handled yet: " + db);

			try
			{
				StringBuilder sb = new StringBuilder(10000);
				sb.AppendLine(scriptContainer.GetHeader(ScriptModelDatabase));

				//if (comboBoxTables.SelectedItem is ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable)
				//{
				//    ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable t = (ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable)comboBoxTables.SelectedItem;
				//    sb.AppendLine(scriptContainer.GetUpdate(t));
				//}
				//else if (comboBoxTables.SelectedItem is ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable)
				//{
				//    ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable t = (ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable)comboBoxTables.SelectedItem;

				//    if (Database.NewTables.Contains(t))
				//        sb.AppendLine(scriptContainer.GetCreate(t));
				//    else
				//        sb.AppendLine(scriptContainer.GetDelete(t));
				//}
				//else
				//{
				//    foreach (var tbl in Database.NewTables)
				//        sb.AppendLine(scriptContainer.GetCreate(tbl));

				//    foreach (var tbl in Database.ChangedTables)
				//        sb.AppendLine(scriptContainer.GetUpdate(tbl));

				//    foreach (var tbl in Database.RemovedTables)
				//        sb.AppendLine(scriptContainer.GetDelete(tbl));
				//}
				return sb.ToString();
			}
			catch (Exception e)
			{
				//MessageBox.Show(this.Parent, e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return e.Message;
			}
		}

		private void comboBoxDatabases_SelectedIndexChanged(object sender, EventArgs e)
		{
			//syntaxEditorScript.Text = GetScriptForAll();
		}

		private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
		{
			//syntaxEditorScript.Text = GetScriptForAll();
		}

		private void PopulateDatabaseTypes()
		{
			comboBoxDatabaseTypes.Items.Clear();
			Array enumValues = Enum.GetValues(typeof(DatabaseTypes));
			DatabaseTypes lastDbUsed = SettingsEngine.LastDatabaseTypeUsed;
			int sqlIndex = -1;

			if (lastDbUsed == DatabaseTypes.Unknown)
				lastDbUsed = DatabaseTypes.SQLServer2005;

			foreach (DatabaseTypes item in enumValues)
			{
				if (item == DatabaseTypes.Unknown)
					continue;
				//if (item == DatabaseTypes.SQLite)
				//    // TODO: Remove this when SQLite dev is finished
				//    continue;

				var description = Slyce.Common.Utility.GetDescription(item);
				comboBoxDatabaseTypes.Items.Add(new Slyce.Common.ComboBoxItemEx<DatabaseTypes>(item, f => description));

				if (item == DatabaseTypes.SQLServer2005)
				{
					sqlIndex = comboBoxDatabaseTypes.Items.Count - 1;
				}
				if (item == lastDbUsed)
					comboBoxDatabaseTypes.SelectedIndex = comboBoxDatabaseTypes.Items.Count - 1;
			}
			if (comboBoxDatabaseTypes.SelectedIndex < 0)
				comboBoxDatabaseTypes.SelectedIndex = sqlIndex;

			//comboBoxDatabaseTypes.Sorted = true;
		}

		private void buttonCopy_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(syntaxEditorScript.Text))
				Clipboard.Clear();
			else
				Clipboard.SetText(syntaxEditorScript.Text);
		}

		private void checkBoxFull_CheckedChanged(object sender, EventArgs e)
		{
			//syntaxEditorScript.Text = GetScriptForAll();
		}

		private void checkBoxChanges_CheckedChanged(object sender, EventArgs e)
		{
			//syntaxEditorScript.Text = GetScriptForAll();
		}

		private void buttonRunScript_Click(object sender, EventArgs e)
		{
			syntaxEditorScript.Text = GetScriptForAll(false);
		}

		private void buttonCreateChangeScript_Click(object sender, EventArgs e)
		{
			syntaxEditorScript.Text = GetScriptForAll(true);
		}

		private void buttonX1_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.DefaultExt = ".sql";
			//dialog.InitialDirectory = ArchAngel.Common.UserTemplateHelper.GetTemplatesFolder();
			dialog.Filter = "SQL files (*.sql)|*.sql|Text files (*.txt)|*.txt|All files (*.*)|*.*";
			dialog.FileName = "script";

			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				if (File.Exists(dialog.FileName) && MessageBox.Show(this, "Overwrite existing file?", "Existing file", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
					return;

				File.WriteAllText(dialog.FileName, syntaxEditorScript.Text);
			}
		}

	}
}
