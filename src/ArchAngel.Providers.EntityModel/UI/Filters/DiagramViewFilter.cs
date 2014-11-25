using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.UI.Filters
{
	public interface DiagramViewFilter
	{
		void Disconnect();
		void Connect();
		void RunFilter();
	    bool CanRun();
	}

	public abstract class MappingSetViewFilter : DiagramViewFilter
	{
		protected readonly SchemaDiagrammerController diagramController;
		protected readonly SchemaController schemaController;
		protected readonly MappingSet mappingSet;

		protected abstract void RunFilterImpl();
	    public abstract bool CanRun();

		protected MappingSetViewFilter(SchemaDiagrammerController diagramController, SchemaController schemaController, MappingSet mappingSet)
		{
			this.diagramController = diagramController;
			this.schemaController = schemaController;
			this.mappingSet = mappingSet;
		}

		public void Disconnect()
		{
			mappingSet.EventAggregator.TablesChanged -= Database_TablesChanged;
			mappingSet.EventAggregator.RelationshipsChanged -= Database_RelationshipsChanged;
			mappingSet.EventAggregator.EntitiesChanged -= EntitySet_EntitiesChanged;
			mappingSet.EventAggregator.ReferencesChanged -= EntitySet_ReferencesChanged;
			mappingSet.EventAggregator.EntityChildrenChanged -= entity_ChildrenChanged;
		}

		public void Connect()
		{
			mappingSet.EventAggregator.TablesChanged += Database_TablesChanged;
			mappingSet.EventAggregator.RelationshipsChanged += Database_RelationshipsChanged;
			mappingSet.EventAggregator.EntitiesChanged += EntitySet_EntitiesChanged;
			mappingSet.EventAggregator.ReferencesChanged += EntitySet_ReferencesChanged;
			mappingSet.EventAggregator.EntityChildrenChanged += entity_ChildrenChanged;
		}

		public void RunFilter()
		{
			if (schemaController.IsShowingMappingSet(mappingSet) == false)
			{
				diagramController.RefreshSchemaDiagrammer(mappingSet);
			}

			diagramController.HideShapesOffScreen();
			schemaController.ClearVisibleSet();

			RunFilterImpl();
		}

		protected virtual void OnChildRemoved(Entity child, Entity oldParent)
		{
			schemaController.RemoveParentChildRelationship(oldParent, child);
		}

		protected virtual void OnChildAdded(Entity child, Entity newParent)
		{
			schemaController.AddParentChildRelationship(child, newParent);
		}

		protected virtual void OnEntityAdded(Entity entity)
		{
			schemaController.AddEntity(entity);
		}

		protected virtual void OnEntityRemoved(Entity entity)
		{
			diagramController.RefreshSchemaDiagrammer(mappingSet);
			//RunFilter();
			diagramController.RunLayout();
		}

		protected virtual void OnReferenceAdded(Reference reference)
		{
			schemaController.AddRelationship(reference);
			RunFilter();
			diagramController.RunLayout();
		}

		protected virtual void OnReferenceRemoved(Reference reference)
		{
			diagramController.RefreshSchemaDiagrammer(mappingSet);
			RunFilter();
			diagramController.RunLayout();
		}

		protected virtual void OnTableAdded(ITable table)
		{
			schemaController.AddEntity(table);
		}

		protected virtual void OnTableRemoved(ITable table)
		{
			diagramController.RefreshSchemaDiagrammer(mappingSet);
			RunFilter();
			diagramController.RunLayout();
		}

		protected virtual void OnRelationshipAdded(Relationship relationship)
		{
			schemaController.AddRelationship(relationship);
		}

		protected virtual void OnRelationshipRemoved(Relationship relationship)
		{
			diagramController.RefreshSchemaDiagrammer(mappingSet);
			RunFilter();
			diagramController.RunLayout();
		}

		void entity_ChildrenChanged(object sender, CollectionChangeEvent<Entity> e)
		{
			switch (e.ChangeType)
			{
				case CollectionChangeAction.Addition:
					OnChildAdded(e.ChangedObject, sender as Entity);
					break;
				case CollectionChangeAction.Deletion:
					OnChildRemoved(e.ChangedObject, sender as Entity);
					break;
			}
		}

		void EntitySet_ReferencesChanged(object sender, CollectionChangeEvent<Reference> e)
		{
			switch (e.ChangeType)
			{
				case CollectionChangeAction.Addition:
					OnReferenceAdded(e.ChangedObject);
					break;
				case CollectionChangeAction.Deletion:
					OnReferenceRemoved(e.ChangedObject);
					break;
			}
		}

		void EntitySet_EntitiesChanged(object sender, CollectionChangeEvent<Entity> e)
		{
			switch (e.ChangeType)
			{
				case CollectionChangeAction.Addition:
					OnEntityAdded(e.ChangedObject);
					break;
				case CollectionChangeAction.Deletion:
					OnEntityRemoved(e.ChangedObject);
					break;
			}
		}

		private void Database_RelationshipsChanged(object sender, CollectionChangeEvent<Relationship> e)
		{
			switch (e.ChangeType)
			{
				case CollectionChangeAction.Addition:
					OnRelationshipAdded(e.ChangedObject);
					break;
				case CollectionChangeAction.Deletion:
					OnRelationshipRemoved(e.ChangedObject);
					break;
			}
		}

		void Database_TablesChanged(object sender, CollectionChangeEvent<ITable> e)
		{
			switch (e.ChangeType)
			{
				case CollectionChangeAction.Addition:
					OnTableAdded(e.ChangedObject);
					break;
				case CollectionChangeAction.Deletion:
					OnTableRemoved(e.ChangedObject);
					break;
			}
		}

	    protected void SetAllEntitiesToVisible()
	    {
	        schemaController.SetEntitiesAndRelationshipsToVisible(new HashSet<IEntity>(mappingSet.EntitySet.Entities.Cast<IEntity>()));
	    }
	}

	public abstract class DatabaseViewFilter : DiagramViewFilter
	{
		protected readonly SchemaDiagrammerController diagramController;
		protected readonly SchemaController schemaController;
		protected readonly IDatabase database;

		protected abstract void RunFilterImpl();

		protected virtual void OnTableAdded(ITable table) { }
		protected virtual void OnTableRemoved(ITable table) { }
		protected virtual void OnRelationshipAdded(Relationship relationship) { }
		protected virtual void OnRelationshipRemoved(Relationship relationship) { }

		protected DatabaseViewFilter(SchemaDiagrammerController diagramController, SchemaController schemaController, IDatabase database)
		{
			this.diagramController = diagramController;
			this.schemaController = schemaController;
			this.database = database;

            if (database == null)
            {
                throw new Exception("Database is null for this table.");
            }
		}

		public void Disconnect()
		{
		    database.MappingSet.EventAggregator.TablesChanged -= database_TablesChanged;
            database.MappingSet.EventAggregator.RelationshipsChanged -= database_RelationshipsChanged;
		}

		public void Connect()
		{
			database.TablesChanged += database_TablesChanged;
			database.RelationshipsChanged += database_RelationshipsChanged;
		}

		void database_RelationshipsChanged(object sender, CollectionChangeEvent<Relationship> e)
		{
			switch (e.ChangeType)
			{
				case CollectionChangeAction.Addition:
					OnRelationshipAdded(e.ChangedObject);
					break;
				case CollectionChangeAction.Deletion:
					OnRelationshipRemoved(e.ChangedObject);
					break;
			}
		}

		void database_TablesChanged(object sender, CollectionChangeEvent<ITable> e)
		{
			switch(e.ChangeType)
			{
				case CollectionChangeAction.Addition:
					OnTableAdded(e.ChangedObject);
					break;
				case CollectionChangeAction.Deletion:
					OnTableRemoved(e.ChangedObject);
					break;
			}
		}

		public virtual void RunFilter()
		{
			if (schemaController.IsShowingDatabase(database) == false)
			{
				diagramController.RefreshSchemaDiagrammer(database);
			}

			diagramController.HideShapesOffScreen();
			schemaController.ClearVisibleSet();

			RunFilterImpl();
		}

	    public abstract bool CanRun();
	}
}