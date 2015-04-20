using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Component = ArchAngel.Providers.EntityModel.Model.EntityLayer.Component;

namespace ArchAngel.Providers.EntityModel.Model.MappingLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	public class MappingSetImpl : ModelObject, MappingSet
	{
		private readonly List<Mapping> mappings = new List<Mapping>();
		private readonly List<TableReferenceMapping> referenceMappings = new List<TableReferenceMapping>();
		private readonly List<RelationshipReferenceMapping> relationshipMappings = new List<RelationshipReferenceMapping>();
		private readonly List<ComponentMapping> componentMappings = new List<ComponentMapping>();
		private IDatabase database;
		private EntitySet entitySet;

		private bool CacheInvalid = true;
		private readonly MappingCache<IColumn, Property> columnPropertyCache = new MappingCache<IColumn, Property>();
		private readonly MappingCache<ITable, Reference> tableReferenceCache = new MappingCache<ITable, Reference>();
		private readonly MappingCache<Relationship, Reference> relationshipReferenceCache = new MappingCache<Relationship, Reference>();
		private readonly MappingCache<IColumn, ComponentPropertyMarker> componentPropertyColumnCache = new MappingCache<IColumn, ComponentPropertyMarker>();
		private List<string> _TablePrefixes = null;
		private List<string> _ColumnPrefixes = null;
		private List<string> _TableSuffixes = null;
		private List<string> _ColumnSuffixes = null;

		[ArchAngel.Interfaces.Attributes.ArchAngelEditor(IsGeneratorIterator = true)]
		public IDatabase Database
		{
			get { return database; }
			set
			{
				database = value;
				if (database != null) database.MappingSet = this;
			}
		}

		public override string DisplayName
		{
			get { return "Entity Model"; }
		}

		[ArchAngel.Interfaces.Attributes.ArchAngelEditor(IsGeneratorIterator = true)]
		public EntitySet EntitySet
		{
			get { return entitySet; }
			set { entitySet = value; entitySet.MappingSet = this; }
		}

		public ParseResults CodeParseResults
		{
			get;
			set;
		}

		public MappingSetImpl()
		{
			Database = new Database("", ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes.Unknown);
			EntitySet = new EntitySetImpl();

			EventAggregator = new EventAggregator(this);
			TablePrefixes = new List<string>();
			ColumnPrefixes = new List<string>();
			TableSuffixes = new List<string>();
			ColumnSuffixes = new List<string>();
		}

		public MappingSetImpl(IDatabase database, EntitySet entities)
		{
			Database = database;
			EntitySet = entities;

			EventAggregator = new EventAggregator(this);
			TablePrefixes = new List<string>();
			ColumnPrefixes = new List<string>();
			TableSuffixes = new List<string>();
			ColumnSuffixes = new List<string>();
		}

		public EventAggregator EventAggregator
		{
			get;
			private set;
		}

		public ReadOnlyCollection<Mapping> Mappings
		{
			get
			{
				return mappings.AsReadOnly();
			}
		}

		public ReadOnlyCollection<TableReferenceMapping> ReferenceMappings
		{
			get
			{
				return referenceMappings.AsReadOnly();
			}
		}

		public ReadOnlyCollection<RelationshipReferenceMapping> RelationshipMappings
		{
			get
			{
				return relationshipMappings.AsReadOnly();
			}
		}

		public ReadOnlyCollection<ComponentMapping> ComponentMappings
		{
			get
			{
				return componentMappings.AsReadOnly();
			}
		}

		public void RemoveMappingBetween(Reference reference, ITable table)
		{
			if (tableReferenceCache.ContainsObject(reference))
			{
				referenceMappings.RemoveAll(r => r.ToReference == reference && r.FromTable == table);
				CacheInvalid = true;
				RaisePropertyChanged("ReferenceMappings");
			}
		}

		public void RemoveMappingBetween(Reference reference, Relationship relationship)
		{
			if (relationshipReferenceCache.ContainsObject(reference))
			{
				relationshipMappings.RemoveAll(r => r.ToReference == reference && r.FromRelationship == relationship);
				CacheInvalid = true;
				RaisePropertyChanged("RelationshipMappings");
			}
		}

		public void RemoveMappingBetween(ComponentPropertyMarker property, IColumn column)
		{
			if (column == null) throw new ArgumentNullException("column");
			if (property == null) throw new ArgumentNullException("property");

			var component = property.Component;
			var table = column.Parent;

			var mapping = componentMappings.FirstOrDefault(m => m.ToComponent == component && m.FromTable == table);
			if (mapping != null)
			{
				mapping.SetMapping(property, null);
				if (mapping.Mappings.Count() == 0)
					RemoveMapping(mapping);
			}
			CacheInvalid = true;
		}

		public void RemoveMappingBetween(Property property, IColumn column)
		{
			if (column == null) throw new ArgumentNullException("column");
			if (property == null) throw new ArgumentNullException("property");

			var entity = property.Entity;
			var table = column.Parent;

			Mapping mapping = mappings.FirstOrDefault(m => m.ToEntity == entity && m.FromTable == table);
			if (mapping != null)
			{
				mapping.SetMapping(property, null);
				if (mapping.Mappings.Count() == 0)
					RemoveMapping(mapping);
			}
			CacheInvalid = true;
		}

		public void InvalidateCache() { CacheInvalid = true; }

		public void DeleteEntity(Entity entity)
		{
			mappings.RemoveAll(m => m.ToEntity == entity);
			CacheInvalid = true;
			RaisePropertyChanged("Mappings");
		}

		public void DeleteTable(ITable table)
		{
			int mappingCount = mappings.RemoveAll(m => m.FromTable == table);
			int refCount = referenceMappings.RemoveAll(m => m.FromTable == table);
			CacheInvalid = true;

			if (mappingCount > 0) RaisePropertyChanged("Mappings");
			if (refCount > 0) RaisePropertyChanged("ReferenceMappings");
		}

		public void DeleteView(ITable view)
		{
			int mappingCount = mappings.RemoveAll(m => m.FromTable == view);
			int refCount = referenceMappings.RemoveAll(m => m.FromTable == view);
			CacheInvalid = true;

			if (mappingCount > 0) RaisePropertyChanged("Mappings");
			if (refCount > 0) RaisePropertyChanged("ReferenceMappings");
		}

		public void DeleteReference(Reference reference)
		{
			int refCount = referenceMappings.RemoveAll(m => m.ToReference == reference);
			int relCount = relationshipMappings.RemoveAll(m => m.ToReference == reference);
			CacheInvalid = true;

			if (refCount > 0) RaisePropertyChanged("ReferenceMappings");
			if (relCount > 0) RaisePropertyChanged("RelationshipMappings");
		}

		public void DeleteRelationship(Relationship relationship)
		{
			int refCount = relationshipMappings.RemoveAll(m => m.FromRelationship == relationship);
			CacheInvalid = true;

			if (refCount > 0) RaisePropertyChanged("RelationshipMappings");
		}

		public void AddMapping(Mapping mapping)
		{
			if (mappings.Contains(mapping))
				return;

			mappings.Add(mapping);
			mapping.MappingSet = this;
			mapping.PropertyChanged += mapping_PropertyChanged;
			CacheInvalid = true;
			RaisePropertyChanged("Mappings");
		}

		public void AddMapping(TableReferenceMapping mapping)
		{
			if (referenceMappings.Contains(mapping))
				return;

			referenceMappings.Add(mapping);
			mapping.MappingSet = this;
			CacheInvalid = true;
			RaisePropertyChanged("ReferenceMappings");
		}

		public void AddMapping(RelationshipReferenceMapping mapping)
		{
			if (relationshipMappings.Contains(mapping))
				return;

			relationshipMappings.Add(mapping);
			mapping.MappingSet = this;
			CacheInvalid = true;
			RaisePropertyChanged("RelationshipMappings");
		}

		public void AddMapping(ComponentMapping mapping)
		{
			if (componentMappings.Contains(mapping))
				return;

			componentMappings.Add(mapping);
			mapping.MappingSet = this;
			CacheInvalid = true;
			RaisePropertyChanged("ComponentMappings");
		}

		public void RemoveMapping(Mapping mapping)
		{
			if (mappings.Remove(mapping))
			{
				mapping.MappingSet = null;
				mapping.PropertyChanged -= mapping_PropertyChanged;
			}

			CacheInvalid = true;
			RaisePropertyChanged("Mappings");
		}

		public void RemoveMapping(TableReferenceMapping mapping)
		{
			if (referenceMappings.Remove(mapping))
			{
				mapping.MappingSet = null;
			}

			CacheInvalid = true;
			RaisePropertyChanged("ReferenceMappings");
		}

		public void RemoveMapping(RelationshipReferenceMapping mapping)
		{
			if (relationshipMappings.Remove(mapping))
			{
				mapping.MappingSet = null;
			}

			CacheInvalid = true;
			RaisePropertyChanged("RelationshipMappings");
		}

		public void RemoveMapping(ComponentMapping mapping)
		{
			if (componentMappings.Remove(mapping))
			{
				mapping.MappingSet = null;
			}

			CacheInvalid = true;
			RaisePropertyChanged("ComponentMappings");
		}

		public IEnumerable<ITable> GetMappedTablesFor(Entity e)
		{
			return GetMappingsContaining(e).Select(n => n.FromTable);
		}

		public IEnumerable<Entity> GetMappedEntitiesFor(ITable table)
		{
			return GetMappingsContaining(table).Select(n => n.ToEntity);
		}

		public IEnumerable<Mapping> GetMappingsContaining(Entity e)
		{
			return Mappings.Where(n => ReferenceEquals(n.ToEntity, e));
		}

		public IEnumerable<Mapping> GetMappingsContaining(ITable e)
		{
			return Mappings.Where(n => ReferenceEquals(n.FromTable, e));
		}

		public ITable GetMappedTableFor(Component component)
		{
			return componentMappings.Where(c => ReferenceEquals(c.ToComponent, component)).Select(c => c.FromTable).FirstOrDefault();
		}

		public ComponentMapping GetMappingFor(Component component)
		{
			return componentMappings.FirstOrDefault(c => ReferenceEquals(c.ToComponent, component));
		}

		public IColumn GetMappedColumnFor(Property property)
		{
			if (CacheInvalid) ClearAndRebuildCache();
			return columnPropertyCache.GetMappedObject(property);
		}

		public IColumn GetMappedColumnFor(ComponentPropertyMarker property)
		{
			if (CacheInvalid) ClearAndRebuildCache();
			return componentPropertyColumnCache.GetMappedObject(property);
		}

		public IEnumerable<Property> GetMappedPropertiesFor(IColumn column)
		{
			if (CacheInvalid) ClearAndRebuildCache();
			return columnPropertyCache.GetMappedObjects(column);
		}

		public ITable GetMappedTableFor(Reference reference)
		{
			if (CacheInvalid) ClearAndRebuildCache();
			return tableReferenceCache.GetMappedObject(reference);
		}

		public IEnumerable<Reference> GetMappedReferencesFor(ITable table)
		{
			if (CacheInvalid) ClearAndRebuildCache();
			return tableReferenceCache.GetMappedObjects(table);
		}

		public Relationship GetMappedRelationshipFor(Reference reference)
		{
			if (CacheInvalid) ClearAndRebuildCache();
			return relationshipReferenceCache.GetMappedObject(reference);
		}

		public IEnumerable<Reference> GetMappedReferencesFor(Relationship relationship)
		{
			if (CacheInvalid) ClearAndRebuildCache();
			return relationshipReferenceCache.GetMappedObjects(relationship);
		}

		public IEnumerable<ITable> GetAssociationTablesFor(IDatabase database)
		{
			if (CacheInvalid) ClearAndRebuildCache();
			return database.Tables.Where(t => IsAssociationTable(t));
		}

		private bool IsAssociationTable(ITable table)
		{
			// Association tables should only have two foreign keys.
			if (table.ForeignKeys.Count() != 2)
				return false;

			var fk1 = table.ForeignKeys.First();
			var fk2 = table.ForeignKeys.Last();

			// All columns in the table should be in either of the foreign keys, or should be an Identity field.
			if (table.Columns.Any(c => fk1.Columns.Contains(c) == false && fk2.Columns.Contains(c) == false && !c.IsIdentity))
				return false;

			return true;
		}

		public IEnumerable<Component> GetMappedComponentsFor(ITable table)
		{
			if (CacheInvalid) ClearAndRebuildCache();
			return componentMappings.Where(c => ReferenceEquals(c.FromTable, table)).Select(c => c.ToComponent);
		}

		/// <summary>
		/// This method removes any mappings between the given property and a column.
		/// If this leaves any Mapping objects with no mappings in them, they are deleted. 
		/// </summary>
		/// <param name="property"></param>
		public void RemoveMappingsContaining(Property property)
		{
			var mappingsToProcess = mappings.Where(m => m.ToEntity == property.Entity).ToList();

			// Use ToList() so we don't get a exception thown because we are modifying to the mappings collection.
			foreach (var mapping in mappingsToProcess)
			{
				mapping.RemovePropertyAndMappedColumn(property);

				if (mapping.Mappings.Any() == false)
					RemoveMapping(mapping);
			}
		}

		public void RemoveMappingsContaining(Relationship relationship)
		{
			relationshipMappings.RemoveAll(m => m.FromRelationship == relationship);
			CacheInvalid = true;
		}

		/// <summary>
		/// This method removes any mappings between the given column and some properties.
		/// If this leaves any Mapping objects with no mappings in them, they are deleted. 
		/// </summary>
		/// <param name="column"></param>
		public void RemoveMappingsContaining(IColumn column)
		{
			var mappingsToProcess = mappings.Where(m => m.FromTable.InternalIdentifier == column.Parent.InternalIdentifier).ToList();

			foreach (var mapping in mappingsToProcess)
			{
				mapping.RemoveColumnAndMappedProperties(column);
				if (mapping.Mappings.Any() == false)
				{
					RemoveMapping(mapping);
				}
			}
		}

		public IEnumerable<Entity> GetEntitiesFromEntitySet()
		{
			if (EntitySet != null)
				return EntitySet.Entities;

			return new List<Entity>();
		}

		public IEnumerable<ITable> GetEntitiesFromDatabase()
		{
			if (Database != null)
				return Database.Tables;

			return new List<ITable>();
		}

		void mapping_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Mappings") CacheInvalid = true;
		}

		public ITable GetNullEntityObject()
		{
			if (Database != null)
				return Database.GetNullEntityObject();

			return new NullEntityObject();
		}

		public MappingChanger ChangeMappedColumnFor(Property property)
		{
			return new MappingChanger(this, property);
		}

		public ReferenceMappingChanger ChangeMappingFor(Reference reference)
		{
			return new ReferenceMappingChanger(this, reference);
		}

		public ComponentPropertyMappingChanger ChangeMappingFor(ComponentPropertyMarker component)
		{
			return new ComponentPropertyMappingChanger(this, component);
		}

		private void ClearAndRebuildCache()
		{
			columnPropertyCache.Clear();
			tableReferenceCache.Clear();
			relationshipReferenceCache.Clear();
			componentPropertyColumnCache.Clear();

			foreach (var mapping in ReferenceMappings.Where(r => r.FromTable != null && r.ToReference != null))
				tableReferenceCache.AddMapping(mapping.FromTable, mapping.ToReference);

			foreach (var mapping in RelationshipMappings.Where(r => r.FromRelationship != null && r.ToReference != null))
				relationshipReferenceCache.AddMapping(mapping.FromRelationship, mapping.ToReference);

			foreach (var mapping in Mappings)
				foreach (var columnPropMapping in mapping.Mappings.Where(m => m.Column != null && m.Property != null))
					columnPropertyCache.AddMapping(columnPropMapping.Column, columnPropMapping.Property);

			foreach (var mapping in componentMappings)
				foreach (var columnPropMapping in mapping.Mappings.Where(m => m.Column != null && m.Property != null))
					componentPropertyColumnCache.AddMapping(columnPropMapping.Column, columnPropMapping.Property);

			CacheInvalid = false;
		}

		/// <summary>
		/// Returns the mapping object that maps the given entity and table.
		/// If one does not exist, a new Mapping is created and returned.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="table"></param>
		/// <returns></returns>
		public Mapping GetMappingBetween(Entity entity, ITable table)
		{
			Mapping mapping = mappings.FirstOrDefault(m => m.ToEntity == entity && m.FromTable == table);

			if (mapping == null)
			{
				mapping = new MappingImpl { ToEntity = entity, FromTable = table };
				AddMapping(mapping);
			}

			return mapping;
		}

		public List<string> TablePrefixes
		{
			get
			{
				if (_TablePrefixes == null)
					_TablePrefixes = new List<string>();

				return _TablePrefixes;
			}
			set
			{
				_TablePrefixes = value;

				for (int i = 0; i < _TablePrefixes.Count; i++)
					_TablePrefixes[i] = _TablePrefixes[i].Trim();
			}
		}

		public List<string> ColumnPrefixes
		{
			get
			{
				if (_ColumnPrefixes == null)
					_ColumnPrefixes = new List<string>();

				return _ColumnPrefixes;
			}
			set
			{
				_ColumnPrefixes = value;

				for (int i = 0; i < _ColumnPrefixes.Count; i++)
					_ColumnPrefixes[i] = _ColumnPrefixes[i].Trim();
			}
		}

		public List<string> TableSuffixes
		{
			get
			{
				if (_TableSuffixes == null)
					_TableSuffixes = new List<string>();

				return _TableSuffixes;
			}
			set
			{
				_TableSuffixes = value;

				for (int i = 0; i < _TableSuffixes.Count; i++)
					_TableSuffixes[i] = _TableSuffixes[i].Trim();
			}
		}

		public List<string> ColumnSuffixes
		{
			get
			{
				if (_ColumnSuffixes == null)
					_ColumnSuffixes = new List<string>();

				return _ColumnSuffixes;
			}
			set
			{
				_ColumnSuffixes = value;

				for (int i = 0; i < _ColumnSuffixes.Count; i++)
					_ColumnSuffixes[i] = _ColumnSuffixes[i].Trim();
			}
		}
	}

	internal class NullDatabase : Database
	{
		public NullDatabase()
			: base("", ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes.Unknown)
		{
		}

		public static bool IsNullDatabase(IDatabase database)
		{
			return database is NullDatabase;
		}

		public override void AddEntity(IEntity entity)
		{
		}

		public override void RemoveEntity(IEntity entity)
		{
		}

		public override ITable AddTable(ITable table)
		{
			return table;
		}

		public override void RemoveTable(ITable table)
		{
		}
	}

	internal class NullEntitySet : EntitySetImpl
	{
		public static bool IsNullEntitySet(EntitySet es)
		{
			return es is NullEntitySet;
		}

		public override void AddEntity(Entity entity)
		{
		}

		public override void AddReference(Reference reference)
		{
		}

		public override void DeleteEntity(Entity entity)
		{
		}
	}
}