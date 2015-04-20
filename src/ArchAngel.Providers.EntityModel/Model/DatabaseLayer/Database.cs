using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	[DebuggerDisplay("Name = {Name}")]
	public class Database : ModelObject, IDatabase
	{
		public event EventHandler<CollectionChangeEvent<ITable>> TablesChanged;
		public event EventHandler<CollectionChangeEvent<ITable>> ViewsChanged;
		public event EventHandler<CollectionChangeEvent<Relationship>> RelationshipsChanged;

		private readonly List<ITable> _tables = new List<ITable>();
		private readonly List<ITable> _views = new List<ITable>();
		private ArchAngel.Providers.EntityModel.Helper.ConnectionStringHelper _ConnectionInformation = null;
		private DatabaseTypes _DatabaseType = DatabaseTypes.Unknown;

		public ReadOnlyCollection<ITable> Tables
		{
			get { return _tables.AsReadOnly(); }
		}

		public ReadOnlyCollection<ITable> Views
		{
			get { return _views.AsReadOnly(); }
		}

		private IDatabaseLoader _Loader;

		public IDatabaseLoader Loader
		{
			get { return _Loader; }
			set { _Loader = value; }
		}

		public MappingSet MappingSet { get; set; }

		public DatabaseTypes DatabaseType
		{
			get { return _DatabaseType; }
			set { _DatabaseType = value; }
		}

		public ArchAngel.Providers.EntityModel.Helper.ConnectionStringHelper ConnectionInformation
		{
			get { return _ConnectionInformation; }
			set { _ConnectionInformation = value; }
		}

		private NullEntityObject nullEntity;
		private string name;

		private HashSet<Relationship> _relationships = new HashSet<Relationship>();

		public string Name
		{
			get { return name; }
			private set
			{
				name = value;
				RaisePropertyChanged("Name");
			}
		}

		public Database(string name, DatabaseTypes databaseType)
		{
			Name = name;
			DatabaseType = databaseType;
		}

		public IEnumerable<string> GetSchemas()
		{
			return Tables.Select(t => t.Schema).Distinct();
		}

		public override string DisplayName
		{
			get { return "Database"; }
		}

		public string Serialise(IDatabaseSerialisationScheme scheme)
		{
			return scheme.Serialise(this);
		}

		public NullEntityObject GetNullEntityObject()
		{
			if (nullEntity == null)
				nullEntity = new NullEntityObject();

			return nullEntity;
		}

		/// <summary>
		/// Serialises the database using the latest version of the serialisation scheme
		/// </summary>
		/// <returns></returns>
		public string Serialise()
		{
			return DatabaseSerialisationScheme.LatestVersion.Serialise(this);
		}

		public bool Contains(IEntity entity)
		{
			if (entity is ITable)
				return Tables.Contains(entity as ITable);

			return false;
		}

		public virtual void AddEntity(IEntity entity)
		{
			if (Contains(entity)) return;

			if (entity is ITable)
				AddTable(entity as ITable);
			else
				throw new ArgumentException("Cannot add entity of type " + entity.GetType().FullName + " to Model2.IDatabase");
		}

		public virtual void RemoveEntity(IEntity entity)
		{
			if (!Contains(entity)) return;

			if (entity is ITable)
				RemoveTable(entity as ITable);
			else
				throw new ArgumentException("Cannot remove entity of type " + entity.GetType().FullName + " from Model2.IDatabase");
		}

		public void RemoveRelationshipsContaining(IKey key)
		{
			var relationshipsToRemove =
				Relationships.Where(
					r => r.PrimaryKey.InternalIdentifier == key.InternalIdentifier || r.ForeignKey.InternalIdentifier == key.InternalIdentifier)
					.ToList();

			relationshipsToRemove.ForEach(DeleteRelationship);
		}

		public IEnumerable<IKey> GetAllKeys()
		{
			foreach (var table in _tables)
			{
				foreach (var key in table.Keys)
					yield return key;
			}
		}

		public virtual ITable AddTable(ITable table)
		{
			_tables.Add(table);
			table.Database = this;

			RaisePropertyChanged("Tables");
			TablesChanged.RaiseEventEx(this,
				new CollectionChangeEvent<ITable>(CollectionChangeAction.Addition, table));

			return table;
		}

		public virtual void RemoveTable(ITable table)
		{
			_tables.Remove(table);
			table.Database = null;

			RaisePropertyChanged("Tables");
			TablesChanged.RaiseEventEx(this,
				new CollectionChangeEvent<ITable>(CollectionChangeAction.Deletion, table));
		}

		public virtual void RemoveView(ITable view)
		{
			_views.Remove(view);
			view.Database = null;

			RaisePropertyChanged("Views");
			ViewsChanged.RaiseEventEx(this,
				new CollectionChangeEvent<ITable>(CollectionChangeAction.Deletion, view));
		}

		public virtual ITable AddView(ITable view)
		{
			_views.Add(view);
			view.Database = this;

			RaisePropertyChanged("Views");
			ViewsChanged.RaiseEventEx(this,
				new CollectionChangeEvent<ITable>(CollectionChangeAction.Addition, view));

			return view;
		}

		public void AddRelationship(Relationship relationship)
		{
			if (_relationships.Contains(relationship)) return;

			_relationships.Add(relationship);
			relationship.Database = this;

			RaisePropertyChanged("Relationships");
			RelationshipsChanged.RaiseEventEx(this,
				new CollectionChangeEvent<Relationship>(CollectionChangeAction.Addition, relationship));
		}

		public void DeleteRelationship(Relationship relationship)
		{
			foreach (var entity in EntityObjects)
			{
				entity.RemoveRelationship(relationship);
			}

			RemoveRelationship(relationship);
		}

		public void RemoveRelationship(Relationship relationship)
		{
			if (_relationships.Contains(relationship) == false) return;

			_relationships.Remove(relationship);

			RaisePropertyChanged("Relationships");
			RelationshipsChanged.RaiseEventEx(this, new CollectionChangeEvent<Relationship>(CollectionChangeAction.Deletion, relationship));
		}

		public void DeleteTable(ITable table)
		{
			RemoveTable(table);

			if (MappingSet != null)
				MappingSet.DeleteTable(table);
		}

		public void DeleteView(ITable view)
		{
			RemoveView(view);

			if (MappingSet != null)
				MappingSet.DeleteView(view);
		}

		public ITable GetTable(string tableName, string schema)
		{
			// Look for table by table-name and schema
			ITable table = _tables.SingleOrDefault(p => p.Name.ToLowerInvariant() == tableName.ToLowerInvariant() && (p.Schema == null || p.Schema.ToLowerInvariant() == schema.ToLowerInvariant()));

			// If we can't find it the look by table-name only, as long as there's only one match
			if (table == null && string.IsNullOrWhiteSpace(schema))
			{
				var tables = _tables.Where(p => p.Name.ToLowerInvariant() == tableName.ToLowerInvariant());

				if (tables.Count() == 1)
					return tables.ElementAt(0);
			}
			return table;
		}

		public ITable GetView(string viewName, string schema)
		{
			// Look for view by view-name and schema
			ITable view = _views.SingleOrDefault(p => p.Name.ToLowerInvariant() == viewName.ToLowerInvariant() && p.Schema.ToLowerInvariant() == schema.ToLowerInvariant());

			// If we can't find it the look by view-name only, as long as there's only one match
			if (view == null && string.IsNullOrWhiteSpace(schema))
			{
				var views = _views.Where(p => p.Name.ToLowerInvariant() == viewName.ToLowerInvariant());

				if (views.Count() == 1)
					return views.ElementAt(0);
			}
			return view;
		}

		public override string ToString()
		{
			return "Database : " + Name;
		}

		public IEnumerable<ITable> EntityObjects
		{
			get { return Tables.Cast<ITable>(); }
		}

		public IEnumerable<Relationship> Relationships
		{
			get
			{
				return _relationships;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return EntityObjects.Count() == 0;
			}
		}

		public IEnumerable<ITable> GetRelatedEntities(ITable table, int i)
		{
			var entities = new HashSet<ITable>();

			entities.Add(table);

			if (i == 0)
				return entities;

			var relatedTables = new HashSet<ITable>();
			foreach (var relationship in table.DirectedRelationships)
			{
				relatedTables.Add(relationship.ToTable);
				entities.Add(relationship.ToTable);
			}

			if (i > 1)
			{
				foreach (var relatedTable in relatedTables)
				{
					foreach (var relatedEntity in GetRelatedEntities(relatedTable, i - 1))
						entities.Add(relatedEntity);
				}
			}

			return entities;
		}

		public IKey GetKey(string tableName, string schema, string keyName)
		{
			ITable table = GetTable(tableName, schema);
			if (table == null) return null;//throw new ArgumentException("Could not find table " + tableName);
			return table.GetKey(keyName);
		}
	}
}