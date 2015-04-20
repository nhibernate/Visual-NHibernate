using System;
using System.ComponentModel;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using Slyce.Common;
using Slyce.Common.EventExtensions;
using CollectionChangeAction=Slyce.Common.CollectionChangeAction;

namespace ArchAngel.Providers.EntityModel.Model
{
	public class EventAggregator : IEventSender
	{
		private MappingSet mappingSet;
		private IDatabase database;
		private EntitySet entities;

		public event EventHandler MappingSetChanged;
		public event EventHandler<CollectionChangeEvent<Entity>> EntitiesChanged;
		public event EventHandler<CollectionChangeEvent<Entity>> EntityChildrenChanged;
		public event PropertyChangedEventHandler EntityPropertyChanged;
		public event EventHandler<CollectionChangeEvent<ITable>> TablesChanged;
		public event EventHandler<CollectionChangeEvent<Reference>> ReferencesChanged;
		public event EventHandler<CollectionChangeEvent<Relationship>> RelationshipsChanged;

		public EventAggregator(MappingSet mappingSet)
		{
			SetMappingSet(mappingSet);
		}

		public void SetMappingSet(MappingSet newMappingSet)
		{
			RemoveEventSubscriptions();

			if (newMappingSet == null) return;

			mappingSet = newMappingSet;
			MappingSetChanged.RaiseEvent(this);

			SetupEventSubscriptions();
		}

		private void SetupEventSubscriptions()
		{
			if (mappingSet == null) return;

			mappingSet.PropertyChanged += MappingSet_PropertyChanged;

			if(mappingSet.Database != null)
			{
				SetupEventSubscriptions(mappingSet.Database);
			}
			if(mappingSet.EntitySet != null)
			{
				SetupEventSubscriptions(mappingSet.EntitySet);
			}
		}

		private void SetupEventSubscriptions(IDatabase db)
		{
			if(db == null) return;

			database = db;

			db.TablesChanged += OnDatabaseTablesChanged;
			db.RelationshipsChanged += OnDatabaseRelationshipsChanged;
		}

	    private void SetupEventSubscriptions(EntitySet entitySet)
		{
			if (entitySet == null) return;

			entities = entitySet;

			entities.EntitiesChanged += OnEntitiesChanged;
			entities.ReferencesChanged += OnReferencesChanged;

			foreach (var entity in entities.Entities)
			{
				SetupEventSubscriptions(entity);
			}
		}

		private void MappingSet_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if(e.PropertyName == "EntitySet")
			{
				RemoveEventSubscriptions(entities);
				SetupEventSubscriptions(mappingSet.EntitySet);
			}

			if (e.PropertyName == "Database")
			{
				RemoveEventSubscriptions(database);
				SetupEventSubscriptions(mappingSet.Database);
			}
		}

		private void RemoveEventSubscriptions()
		{
			if (mappingSet == null) return;

			if (database != null)
			{
				RemoveEventSubscriptions(database);
				database = null;
			}
			if (entities != null)
			{
				RemoveEventSubscriptions(entities);
				entities = null;
			}
		}

		private void RemoveEventSubscriptions(IDatabase db)
		{
			db.TablesChanged -= OnDatabaseTablesChanged;
			db.RelationshipsChanged += OnDatabaseRelationshipsChanged;
		}

		private void RemoveEventSubscriptions(EntitySet entitySet)
		{
			entitySet.EntitiesChanged -= OnEntitiesChanged;
			entitySet.ReferencesChanged -= OnReferencesChanged;

			foreach (var entity in entitySet.Entities)
			{
				RemoveEventSubscriptions(entity);
			}
		}

		private void RemoveEventSubscriptions(Entity entity)
		{
			entity.ChildrenChanged -= entity_ChildrenChanged;
		}

		private void SetupEventSubscriptions(Entity entity)
		{
			entity.ChildrenChanged += entity_ChildrenChanged;
			entity.PropertyChanged += entity_PropertyChanged;
		}

		void entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			EntityPropertyChanged.RaiseEvent(sender, e.PropertyName);
		}

		void entity_ChildrenChanged(object sender, CollectionChangeEvent<Entity> e)
		{
			EntityChildrenChanged.RaiseEvent(sender, e);
		}

		void OnReferencesChanged(object sender, CollectionChangeEvent<Reference> e)
		{
			ReferencesChanged.RaiseEvent(entities, e);
		}

		void OnEntitiesChanged(object sender, CollectionChangeEvent<Entity> e)
		{
			EntitiesChanged.RaiseEvent(entities, e);

			if(e.ChangeType == CollectionChangeAction.Addition)
			{
				SetupEventSubscriptions(e.ChangedObject);
			}
			else if(e.ChangeType == CollectionChangeAction.Deletion)
			{
				RemoveEventSubscriptions(e.ChangedObject);
			}
		}

		private void OnDatabaseRelationshipsChanged(object sender, CollectionChangeEvent<Relationship> e)
		{
			RelationshipsChanged.RaiseEvent(database, e);
		}

		private void OnDatabaseTablesChanged(object s, CollectionChangeEvent<ITable> e)
		{
			TablesChanged.RaiseEvent(database, e);
		}

		public bool EventRaisingDisabled
		{
			get; set;
		}
	}
}
