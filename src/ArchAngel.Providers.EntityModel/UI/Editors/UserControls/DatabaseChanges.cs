using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using DevComponents.AdvTree;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class DatabaseChanges : UserControl
	{
		private IDatabase DB1;
		private IDatabase DB2;
		private const int IMG_TABLE = 0;
		private const int IMG_NEW = 1;
		private const int IMG_CHANGED = 2;
		private const int IMG_REMOVED = 3;

		public DatabaseChanges()
		{
			InitializeComponent();

			nodeNewTables.Visible = false;
			nodeModifiedTables.Visible = false;
			nodeRemovedTables.Visible = false;

			nodeEmpty.Visible = true;
		}

		public void Fill(DatabaseMergeResult results, IDatabase db1, IDatabase db2)
		{
			if (results == null || db1 == null || db2 == null)
				throw new Exception(string.Format("Parameter is null (DatabaseChanges.Fill). Please inform Slyce of this error, including your scenario. [{0}{1}{2}]",
					results == null ? "results: null" : "",
					db1 == null ? "db1: null" : "",
					db2 == null ? "db2: null" : ""));

			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => Fill(results, db1, db2)));
				return;
			}
			DB1 = db1;
			DB2 = db2;

			advTreeTables.BeginUpdate();

			nodeNewTables.Nodes.Clear();
			nodeModifiedTables.Nodes.Clear();
			nodeRemovedTables.Nodes.Clear();

			nodeNewViews.Nodes.Clear();
			nodeModifiedViews.Nodes.Clear();
			nodeRemovedViews.Nodes.Clear();

			nodeNewTables.Checked = true;
			nodeModifiedTables.Checked = true;
			nodeRemovedTables.Checked = false;

			nodeNewViews.Checked = false;
			nodeModifiedViews.Checked = true;
			nodeRemovedViews.Checked = false;

			try
			{
				#region Database-only
				Node schemaNode = null;
				string currentSchema = null;

				foreach (TableAdditionOperation addedTableOperation in results.TableOperations.Where(r => r is TableAdditionOperation).OrderBy(a => a.Object.Schema).ThenBy(a => a.Object.Name))
				{
					if (addedTableOperation.Object.Schema != currentSchema)
					{
						currentSchema = addedTableOperation.Object.Schema;
						schemaNode = new Node(currentSchema)
							{
								CheckBoxVisible = true,
								Checked = true
							};
						nodeNewTables.Nodes.Add(schemaNode);
					}
					Node node = new Node(addedTableOperation.Object.Name);
					node.Tag = addedTableOperation;
					node.CheckBoxVisible = true;
					node.Checked = true;
					node.ImageIndex = IMG_NEW;
					schemaNode.Nodes.Add(node);
				}
				schemaNode = null;
				currentSchema = null;

				foreach (TableAdditionOperation addedViewOperation in results.ViewOperations.Where(r => r is TableAdditionOperation).OrderBy(a => a.Object.Schema).ThenBy(a => a.Object.Name))
				{
					if (addedViewOperation.Object.Schema != currentSchema)
					{
						currentSchema = addedViewOperation.Object.Schema;
						schemaNode = new Node(currentSchema)
						{
							CheckBoxVisible = true,
							Checked = false
						};
						nodeNewViews.Nodes.Add(schemaNode);
					}
					Node node = new Node(addedViewOperation.Object.Name);
					node.Tag = addedViewOperation;
					node.CheckBoxVisible = true;
					node.Checked = false;
					node.ImageIndex = IMG_NEW;
					schemaNode.Nodes.Add(node);
				}

				#endregion

				#region Model-only
				schemaNode = null;
				currentSchema = null;

				foreach (TableRemovalOperation removedTableOperation in results.TableOperations.Where(r => r is TableRemovalOperation).OrderBy(a => a.Object.Schema).ThenBy(a => a.Object.Name))
				{
					if (removedTableOperation.Object.Schema != currentSchema)
					{
						currentSchema = removedTableOperation.Object.Schema;
						schemaNode = new Node(currentSchema)
						{
							CheckBoxVisible = true,
							Checked = false
						};
						nodeRemovedTables.Nodes.Add(schemaNode);
					}
					Node node = new Node(removedTableOperation.Object.Name);
					node.Tag = removedTableOperation;
					node.CheckBoxVisible = true;
					node.Checked = false;
					node.ImageIndex = IMG_REMOVED;
					schemaNode.Nodes.Add(node);
				}
				schemaNode = null;
				currentSchema = null;

				foreach (TableRemovalOperation removedViewOperation in results.ViewOperations.Where(r => r is TableRemovalOperation).OrderBy(a => a.Object.Schema).ThenBy(a => a.Object.Name))
				{
					if (removedViewOperation.Object.Schema != currentSchema)
					{
						currentSchema = removedViewOperation.Object.Schema;
						schemaNode = new Node(currentSchema)
						{
							CheckBoxVisible = true,
							Checked = false
						};
						nodeRemovedViews.Nodes.Add(schemaNode);
					}
					Node node = new Node(removedViewOperation.Object.Name);
					node.Tag = removedViewOperation;
					node.CheckBoxVisible = true;
					node.Checked = false;
					node.ImageIndex = IMG_REMOVED;
					schemaNode.Nodes.Add(node);
				}

				#endregion

				#region Changes
				HashSet<string> changedTables = new HashSet<string>();

				#region Get tables with changes
				foreach (var result in results.ColumnOperations.Where(o => o.Object != null && o.Object.Parent != null))
					changedTables.Add(string.Format("{0}.{1}", result.Object.Parent.Schema, result.Object.Parent.Name));

				foreach (var result in results.IndexOperations.Where(o => o.Object != null && o.Object.Parent != null))
					changedTables.Add(string.Format("{0}.{1}", result.Object.Parent.Schema, result.Object.Parent.Name));

				foreach (var result in results.KeyOperations.Where(o => o.Object != null && o.Object.Parent != null))
					changedTables.Add(string.Format("{0}.{1}", result.Object.Parent.Schema, result.Object.Parent.Name));

				foreach (var result in results.RelationshipOperations.Where(o => o.Object != null && o.Object.PrimaryTable != null))
					changedTables.Add(string.Format("{0}.{1}", result.Object.PrimaryTable.Schema, result.Object.PrimaryTable.Name));

				#endregion

				schemaNode = null;
				currentSchema = null;

				foreach (string changedTable in changedTables)
				{
					string schema = changedTable.Split('.')[0];
					string tableName = changedTable.Split('.')[1];

					if (schema != currentSchema)
					{
						currentSchema = schema;
						schemaNode = new Node(currentSchema)
						{
							CheckBoxVisible = true,
							Checked = true
						};
						nodeModifiedTables.Nodes.Add(schemaNode);
					}
					Node node = new Node(tableName);
					node.Tag = changedTable;
					node.CheckBoxVisible = true;
					node.Checked = true;
					node.ImageIndex = IMG_CHANGED;
					schemaNode.Nodes.Add(node);

					#region Columns
					foreach (ColumnAdditionOperation newColumn in results.ColumnOperations.Where(r => r is ColumnAdditionOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node columnnode = new Node(string.Format("Add column to model: [{0}]", newColumn.Object.Name));
						columnnode.Tag = newColumn;
						node.Nodes.Add(columnnode);
					}
					foreach (ColumnChangeOperation changedColumn in results.ColumnOperations.Where(r => r is ColumnChangeOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node columnnode = new Node(string.Format("Update column in model [{0}]", changedColumn.Object.Name));
						columnnode.Tag = changedColumn;
						node.Nodes.Add(columnnode);

						foreach (string d in changedColumn.Description.Split(','))
							columnnode.Nodes.Add(new Node(d));
					}
					foreach (ColumnRemovalOperation removedColumn in results.ColumnOperations.Where(r => r is ColumnRemovalOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node columnnode = new Node(string.Format("Remove column from model: [{0}]", removedColumn.Object.Name));
						columnnode.Tag = removedColumn;
						node.Nodes.Add(columnnode);
					}
					#endregion

					#region Indexes
					foreach (IndexAdditionOperation newIndex in results.IndexOperations.Where(r => r is IndexAdditionOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node indexNode = new Node(string.Format("Add index to model: [{0}]", newIndex.Object.Name));
						indexNode.Tag = newIndex;
						node.Nodes.Add(indexNode);
					}
					foreach (IndexChangeOperation changedIndex in results.IndexOperations.Where(r => r is IndexChangeOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node indexNode = new Node(string.Format("Update index in model [{0}]", changedIndex.Object.Name));
						indexNode.Tag = changedIndex;
						node.Nodes.Add(indexNode);

						foreach (string d in changedIndex.Description.Split(','))
							indexNode.Nodes.Add(new Node(d));
					}
					foreach (IndexRemovalOperation removedIndex in results.IndexOperations.Where(r => r is IndexRemovalOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node indexNode = new Node(string.Format("Remove index from model: [{0}]", removedIndex.Object.Name));
						indexNode.Tag = removedIndex;
						node.Nodes.Add(indexNode);
					}
					#endregion

					#region Keys
					foreach (KeyAdditionOperation newKey in results.KeyOperations.Where(r => r is KeyAdditionOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node keyNode = new Node(string.Format("Add key to model: [{0}]", newKey.Object.Name));
						keyNode.Tag = newKey;
						node.Nodes.Add(keyNode);
					}
					foreach (KeyChangeOperation changedKey in results.KeyOperations.Where(r => r is KeyChangeOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node keyNode = new Node(string.Format("Update key in model [{0}]", changedKey.Object.Name));
						keyNode.Tag = changedKey;
						node.Nodes.Add(keyNode);

						foreach (string d in changedKey.Description.Split(','))
							keyNode.Nodes.Add(new Node(d));
					}
					foreach (KeyRemovalOperation removedKey in results.KeyOperations.Where(r => r is KeyRemovalOperation).Where(c => string.Format("{0}.{1}", c.Object.Parent.Schema, c.Object.Parent.Name) == changedTable))
					{
						Node keyNode = new Node(string.Format("Remove key from model: [{0}]", removedKey.Object.Name));
						keyNode.Tag = removedKey;
						node.Nodes.Add(keyNode);
					}
					#endregion

					#region Relationships
					foreach (RelationshipAdditionOperation newRelationship in results.RelationshipOperations.Where(r => r is RelationshipAdditionOperation).Where(c => string.Format("{0}.{1}", c.Object.PrimaryTable.Schema, c.Object.PrimaryTable.Name) == changedTable))
					{
						Node relationshipNode = new Node(string.Format("Add relationship to model, with {0}: [{1}]", newRelationship.Object.ForeignTable.Name, newRelationship.Object.Name));
						relationshipNode.Tag = newRelationship;
						node.Nodes.Add(relationshipNode);
					}
					foreach (RelationshipChangeOperation changedRelationship in results.RelationshipOperations.Where(r => r is RelationshipChangeOperation).Where(c => string.Format("{0}.{1}", c.Object.PrimaryTable.Schema, c.Object.PrimaryTable.Name) == changedTable))
					{
						Node relationshipNode = new Node(string.Format("Update relationship in model, with {0}:  [{1}]", changedRelationship.Object.ForeignTable.Name, changedRelationship.Object.Name));
						relationshipNode.Tag = changedRelationship;
						node.Nodes.Add(relationshipNode);

						foreach (string d in changedRelationship.Description.Split(','))
							relationshipNode.Nodes.Add(new Node(d));
					}
					foreach (RelationshipRemovalOperation removedRelationship in results.RelationshipOperations.Where(r => r is RelationshipRemovalOperation).Where(c => string.Format("{0}.{1}", c.Object.PrimaryTable.Schema, c.Object.PrimaryTable.Name) == changedTable))
					{
						Node relationshipNode = new Node(string.Format("Remove relationship from model, with {0}: [{1}]", removedRelationship.Object.ForeignTable.Name, removedRelationship.Object.Name));
						relationshipNode.Tag = removedRelationship;
						node.Nodes.Add(relationshipNode);
					}
					#endregion
				}
				#endregion

				nodeNewTables.Visible = nodeNewTables.Nodes.Count > 0;
				nodeModifiedTables.Visible = nodeModifiedTables.Nodes.Count > 0;
				nodeRemovedTables.Visible = nodeRemovedTables.Nodes.Count > 0;

				nodeNewViews.Visible = nodeNewViews.Nodes.Count > 0;
				nodeModifiedViews.Visible = nodeModifiedViews.Nodes.Count > 0;
				nodeRemovedViews.Visible = nodeRemovedViews.Nodes.Count > 0;

				if (!nodeNewTables.Visible && !nodeModifiedTables.Visible && !nodeRemovedTables.Visible &&
					!nodeNewViews.Visible && !nodeModifiedViews.Visible && !nodeRemovedViews.Visible)
				{
					nodeEmpty.Text = "The database has no changes";
					nodeEmpty.Visible = true;
				}
				else
					nodeEmpty.Visible = false;
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("Error in DatabaseChanges.Fill: {0}\n{1}", ex.Message, ex.StackTrace));
			}
			finally
			{
				advTreeTables.EndUpdate();
			}
		}

		public void AcceptChanges()
		{
			List<ITwoStepMergeOperation> twoStepOps = new List<ITwoStepMergeOperation>();

			List<TableAdditionOperation> addedTableOps = new List<TableAdditionOperation>();
			List<TableRemovalOperation> removedTableOps = new List<TableRemovalOperation>();
			List<TableChangeOperation> changedTableOps = new List<TableChangeOperation>();

			List<TableAdditionOperation> addedViewOps = new List<TableAdditionOperation>();
			List<TableRemovalOperation> removedViewOps = new List<TableRemovalOperation>();
			List<TableChangeOperation> changedViewOps = new List<TableChangeOperation>();

			List<IMergeOperation<IColumn>> changedColumnOps = new List<IMergeOperation<IColumn>>();
			List<IMergeOperation<IIndex>> changedIndexOps = new List<IMergeOperation<IIndex>>();
			List<IMergeOperation<IKey>> changedKeyOps = new List<IMergeOperation<IKey>>();
			List<IMergeOperation<Relationship>> changedRelationshipOps = new List<IMergeOperation<Relationship>>();

			#region Tables
			foreach (Node schemaNode in nodeNewTables.Nodes)
				foreach (Node node in schemaNode.Nodes)
					if (node.Checked)
						addedTableOps.Add((TableAdditionOperation)node.Tag);

			foreach (Node schemaNode in nodeRemovedTables.Nodes)
				foreach (Node node in schemaNode.Nodes)
					if (node.Checked)
						removedTableOps.Add((TableRemovalOperation)node.Tag);

			foreach (Node schemaNode in nodeModifiedTables.Nodes)
				foreach (Node node in schemaNode.Nodes)
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
			#endregion

			#region Views
			foreach (Node schemaNode in nodeNewViews.Nodes)
				foreach (Node node in schemaNode.Nodes)
					if (node.Checked)
						addedViewOps.Add((TableAdditionOperation)node.Tag);

			foreach (Node schemaNode in nodeRemovedViews.Nodes)
				foreach (Node node in schemaNode.Nodes)
					if (node.Checked)
						removedViewOps.Add((TableRemovalOperation)node.Tag);

			foreach (Node schemaNode in nodeModifiedViews.Nodes)
				foreach (Node node in schemaNode.Nodes)
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
			#endregion

			RunOperations(addedTableOps, twoStepOps);
			RunOperations(removedTableOps, twoStepOps);
			RunOperations(changedTableOps, twoStepOps);
			RunOperations(addedViewOps, twoStepOps);
			RunOperations(removedViewOps, twoStepOps);
			RunOperations(changedViewOps, twoStepOps);
			RunOperations(changedColumnOps, twoStepOps);
			RunOperations(changedRelationshipOps, twoStepOps);
			RunOperations(changedIndexOps, twoStepOps);
			RunOperations(changedKeyOps, twoStepOps);

			foreach (var op in twoStepOps.Distinct())
				op.RunSecondStep();

			// Run the merge again to get missing info for the newly-added tables
			if (addedTableOps.Count > 0 || addedViewOps.Count > 0)
			{
				// Run the merge
				var results = new DatabaseProcessor().MergeDatabases(DB1, DB2);
				HashSet<ITable> tablesInUpdatedRelationships = new HashSet<ITable>();

				if (results.AnyChanges)
				{
					foreach (var rel in changedRelationshipOps.Where(r => !(r is RelationshipRemovalOperation)).Select(r => r.Object))
					{
						var updateTable = DB1.Tables.SingleOrDefault(t => t.Name == rel.PrimaryTable.Name && t.Schema == rel.PrimaryTable.Schema);

						if (updateTable != null)
							tablesInUpdatedRelationships.Add(updateTable);

						updateTable = DB1.Tables.SingleOrDefault(t => t.Name == rel.ForeignTable.Name && t.Schema == rel.ForeignTable.Schema);

						if (updateTable != null)
							tablesInUpdatedRelationships.Add(updateTable);
					}
					changedColumnOps.Clear();
					changedIndexOps.Clear();
					changedKeyOps.Clear();
					changedRelationshipOps.Clear();

					foreach (var table in addedTableOps.Select(to => to.Object))
					{
						changedColumnOps.AddRange(results.ColumnOperations.Where(r => r != null && r is IMergeOperation<IColumn>).Where(c => c != null && c.Object != null && c.Object.Parent != null && c.Object.Parent.Name == table.Name && c.Object.Parent.Schema == table.Schema));
						changedIndexOps.AddRange(results.IndexOperations.Where(r => r != null && r is IMergeOperation<IIndex>).Where(c => c != null && c.Object != null && c.Object.Parent != null && c.Object.Parent.Name == table.Name && c.Object.Parent.Schema == table.Schema));
						changedKeyOps.AddRange(results.KeyOperations.Where(r => r != null && r is IMergeOperation<IKey>).Where(c => c != null && c.Object != null && c.Object.Parent != null && c.Object.Parent.Name == table.Name && c.Object.Parent.Schema == table.Schema));
						changedRelationshipOps.AddRange(results.RelationshipOperations.Where(r => r != null && r is IMergeOperation<Relationship>).Where(c => c != null && c.Object != null && c.Object.PrimaryTable != null && c.Object.PrimaryTable.Name == table.Name && c.Object.PrimaryTable.Schema == table.Schema));
					}
					foreach (var table in addedViewOps.Select(to => to.Object))
					{
						changedColumnOps.AddRange(results.ColumnOperations.Where(r => r != null && r is IMergeOperation<IColumn>).Where(c => c != null && c.Object != null && c.Object.Parent != null && c.Object.Parent.Name == table.Name && c.Object.Parent.Schema == table.Schema));
						changedIndexOps.AddRange(results.IndexOperations.Where(r => r != null && r is IMergeOperation<IIndex>).Where(c => c != null && c.Object != null && c.Object.Parent != null && c.Object.Parent.Name == table.Name && c.Object.Parent.Schema == table.Schema));
						changedKeyOps.AddRange(results.KeyOperations.Where(r => r != null && r is IMergeOperation<IKey>).Where(c => c != null && c.Object != null && c.Object.Parent != null && c.Object.Parent.Name == table.Name && c.Object.Parent.Schema == table.Schema));
						changedRelationshipOps.AddRange(results.RelationshipOperations.Where(r => r != null && r is IMergeOperation<Relationship>).Where(c => c != null && c.Object != null && c.Object.PrimaryTable != null && c.Object.PrimaryTable.Name == table.Name && c.Object.PrimaryTable.Schema == table.Schema));
					}

					//RunOperations(addedTableOps, twoStepOps);
					//RunOperations(removedTableOps, twoStepOps);
					//RunOperations(changedTableOps, twoStepOps);
					RunOperations(changedColumnOps, twoStepOps);
					RunOperations(changedRelationshipOps, twoStepOps);
					RunOperations(changedIndexOps, twoStepOps);
					RunOperations(changedKeyOps, twoStepOps);

					foreach (var op in twoStepOps)
						op.RunSecondStep();
				}
				ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.TablePrefixes = DB1.MappingSet.TablePrefixes.ToList();
				ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.ColumnPrefixes = DB1.MappingSet.ColumnPrefixes.ToList();
				ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.TableSuffixes = DB1.MappingSet.TableSuffixes.ToList();
				ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.ColumnSuffixes = DB1.MappingSet.ColumnSuffixes.ToList();
				HashSet<Entity> newEntities = new HashSet<Entity>();

				// Create new entities for the new tables
				foreach (ITable table in addedTableOps.Select(to => to.Object))
				{
					table.Database = DB1;
					ITable concreteTable = DB1.Tables.Single(t => t.Schema == table.Schema && t.Name == table.Name);

					if (checkBoxCreateEntities.Checked)
						newEntities.Add(CreateNewEntityFromTable(concreteTable));
				}
				// Create new entities for the new views
				foreach (ITable view in addedViewOps.Select(to => to.Object))
				{
					view.Database = DB1;
					ITable concreteView = DB1.Views.Single(t => t.Schema == view.Schema && t.Name == view.Name);

					if (checkBoxCreateEntities.Checked)
						newEntities.Add(CreateNewEntityFromTable(concreteView));
				}
				foreach (Table t in tablesInUpdatedRelationships)
					foreach (Entity entity in t.MappedEntities())
						newEntities.Add(entity);

				ArchAngel.Providers.EntityModel.Controller.MappingLayer.MappingProcessor.CreateManyToManyMappings(newEntities.ToList(), DB1.MappingSet.EntitySet);
			}
			DB1.MappingSet.InvalidateCache();

			nodeNewTables.Visible = false;
			nodeModifiedTables.Visible = false;
			nodeRemovedTables.Visible = false;

			nodeNewViews.Visible = false;
			nodeModifiedViews.Visible = false;
			nodeRemovedViews.Visible = false;

			nodeEmpty.Visible = true;
			this.Refresh();
		}

		private Entity CreateNewEntityFromTable(ITable table)
		{
			List<ITable> tablesToProcess = new List<ITable>();
			tablesToProcess.Add(table);
			EntityModel.Controller.MappingLayer.MappingProcessor proc = new EntityModel.Controller.MappingLayer.MappingProcessor(new EntityModel.Controller.MappingLayer.OneToOneEntityProcessor(table.Database.MappingSet.EntitySet.Entities.Select(ent => ent.Name)));
			List<Entity> newEntities = proc.CreateOneToOneMappingsFor(tablesToProcess, table.Database.MappingSet);

			if (newEntities.Count != 1)
				throw new Exception("Only one entity should be created.");

			return newEntities[0];
		}

		private void RunOperations<T>(IEnumerable<IMergeOperation<T>> operations, ICollection<ITwoStepMergeOperation> twoStepOps) where T : class
		{
			foreach (var op in operations)
			{
				op.RunOperation();
				if (op is ITwoStepMergeOperation)
					twoStepOps.Add(op as ITwoStepMergeOperation);
			}
		}

		private bool BusyApplyingCheckStates = false;

		private void advTreeTables_AfterCheck(object sender, AdvTreeCellEventArgs e)
		{
			if (BusyApplyingCheckStates)
				return;

			BusyApplyingCheckStates = true;
			advTreeTables.BeginUpdate();
			Node node = e.Cell.Parent;

			//if (node.Parent == null)
			// Check for schema nodes - Level 1
			if (node.Parent != null && node.Parent.Parent == null)
			{
				if ((node.CheckBoxThreeState && node.CheckState != CheckState.Checked) ||
					(!node.CheckBoxThreeState && !node.Checked))
				{
					node.CheckBoxThreeState = false;
					node.SetChecked(false, eTreeAction.Code);
				}
				else
				{
					node.CheckBoxThreeState = false;
					node.SetChecked(true, eTreeAction.Code);
				}

				foreach (Node childNode in node.Nodes)
					childNode.SetChecked(node.Checked, eTreeAction.Code);
			}
			//else
			else if (node.Parent != null)
				ResetTopLevelNodeCheckState(node);

			advTreeTables.EndUpdate();
			BusyApplyingCheckStates = false;
		}

		private void ResetTopLevelNodeCheckState(Node childNode)
		{
			bool? checkedState = null;

			foreach (Node n in childNode.Parent.Nodes)
			{
				if (checkedState == null)
					checkedState = n.Checked;
				else if (checkedState.Value != n.Checked)
				{
					childNode.Parent.CheckBoxThreeState = true;
					childNode.Parent.SetChecked(CheckState.Indeterminate, eTreeAction.Code);
					return;
				}
			}
			if (checkedState == null)
				checkedState = true;

			childNode.Parent.CheckBoxThreeState = false;

			if (checkedState.Value)
				childNode.Parent.SetChecked(true, eTreeAction.Code);
			else
				childNode.Parent.SetChecked(false, eTreeAction.Code);
		}

		private void buttonAddSchemas_Click(object sender, EventArgs e)
		{
			List<string> existingSchemas = new List<string>();
			existingSchemas = DB1.Tables.Union(DB1.Views).Select(t => t.Schema).Distinct().ToList();

			FormSelectSchemas form = new FormSelectSchemas(existingSchemas);
			form.ShowDialog(this);
		}

	}
}
