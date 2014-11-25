using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.EntityModel.Helper;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	[DebuggerDisplay("Schema.Name = {Schema}.{Name}")]
	public class Table : ScriptBase, ITable
	{
		protected readonly List<IColumn> _columns = new List<IColumn>();
		protected readonly List<Relationship> _relationships = new List<Relationship>();

		public Table()
		{
		}

		public Table(string name, string schema)
		{
			Name = name;
			Schema = schema;
		}

		public bool IsView { get; set; }

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as ITable);
		}

		public bool Equals(ITable obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return base.Equals(obj) && Equals(obj.Name, Name) && Equals(obj.Schema, Schema);
		}

		public ReadOnlyCollection<Relationship> Relationships
		{
			get { return _relationships.AsReadOnly(); }
		}

		public event EventHandler<CollectionChangeEvent<IColumn>> ColumnsChanged;
		public event CollectionChangeHandler<Relationship> RelationshipsChanged;

		public IEnumerable<DirectedRelationship> DirectedRelationships
		{
			get
			{
				foreach (var relationship in Relationships)
					yield return new DirectedRelationship(relationship, this);
			}
		}

		public IKey FirstPrimaryKey
		{
			get { return _keys.FirstOrDefault(k => k.Keytype == DatabaseKeyType.Primary); }
		}

		public IEnumerable<IKey> ForeignKeys
		{
			get { return _keys.Where(k => k.Keytype == DatabaseKeyType.Foreign); }
		}

		public IEnumerable<IKey> UniqueKeys
		{
			get { return _keys.Where(k => k.Keytype == DatabaseKeyType.Unique); }
		}

		public ReadOnlyCollection<IColumn> Columns
		{
			get { return _columns.AsReadOnly(); }
		}

		public IColumn GetColumn(string columnName)
		{
			return _columns.Find(c => c.Name == columnName);
		}

		public IColumn GetColumn(string columnName, StringComparison comparison)
		{
			return _columns.Find(c => c.Name.Equals(columnName, comparison));
		}

		public void AddColumn(IColumn column)
		{
			_columns.Add(column);
			column.Parent = this;

			column.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(column_PropertyChanged);
			RaisePropertyChanged("Columns");
			ColumnsChanged.RaiseEvent(this,
				new CollectionChangeEvent<IColumn>(CollectionChangeAction.Addition, column));
		}

		void column_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			RaisePropertyChanged("Column");
		}

		public void RemoveColumn(IColumn column)
		{
			_columns.Remove(column);

			RaisePropertyChanged("Columns");
			ColumnsChanged.RaiseEvent(this,
				new CollectionChangeEvent<IColumn>(CollectionChangeAction.Deletion, column));
		}

		public void AddRelationship(Relationship relationship)
		{
			if (Relationships.Contains(relationship))
				return;
			_relationships.Add(relationship);
			relationship.Database = Database;

			Database.AddRelationship(relationship);
			RaisePropertyChanged("Relationships");
			RelationshipsChanged.RaiseAdditionEventEx(this, relationship);
		}

		public void RemoveRelationship(Relationship relationship)
		{
			relationship.Database.RemoveRelationship(relationship);
			_relationships.Remove(relationship);

			RaisePropertyChanged("Relationships");
			RelationshipsChanged.RaiseDeletionEventEx(this, relationship);
		}

		IEnumerable<IRelationship> IEntity.Relationships
		{
			get { return Relationships.Cast<IRelationship>(); }
		}

		public string EntityName
		{
			get { return Name; }
			set { Name = value; }
		}

		public override string DisplayName
		{
			get { return "Table:" + Name; }
		}

		private List<IKey> _keys = new List<IKey>();
		public ReadOnlyCollection<IKey> Keys
		{
			get { return _keys.AsReadOnly(); }
		}

		private List<IIndex> _indexes = new List<IIndex>();
		public ReadOnlyCollection<IIndex> Indexes
		{
			get { return _indexes.AsReadOnly(); }
		}

		public IEnumerable<IColumn> ColumnsInPrimaryKey
		{
			get
			{
				return Columns.Where(c => c.InPrimaryKey);
			}
		}

		public IEnumerable<IColumn> ColumnsNotInPrimaryKey
		{
			get
			{
				return Columns.Where(c => !c.InPrimaryKey);
			}
		}

		public IIndex AddIndex(IIndex index)
		{
			var existingIndex = _indexes.SingleOrDefault(i => i.Name == index.Name && i.Columns.Count == index.Columns.Count && i.IndexType == index.IndexType);

			if (existingIndex != null)
				return existingIndex;

			_indexes.Add(index);
			index.Parent = this;
			RaisePropertyChanged("Indexes");

			return index;
		}

		public IIndex GetIndex(string indexName)
		{
			return _indexes.Find(i => i.Name == indexName);
		}

		public void RemoveIndex(IIndex index)
		{
			_indexes.Remove(index);
			index.Parent = null;
			RaisePropertyChanged("Indexes");
		}

		public IKey AddKey(IKey key)
		{
			_keys.Add(key);
			key.Parent = this;
			RaisePropertyChanged("Keys");

			return key;
		}

		public void RemoveKey(IKey key)
		{
			_keys.Remove(key);
			key.Parent = null;

			Database.RemoveRelationshipsContaining(key);

			RaisePropertyChanged("Keys");
		}

		public void DeleteColumn(IColumn column)
		{
			var keysToRemoveFrom = Keys.Where(k => k.Columns.Contains(column)).ToList();

			foreach (var key in keysToRemoveFrom)
			{
				key.RemoveColumn(column);

				if (key.Columns.Count == 0)
				{
					RemoveKey(key);
				}
			}

			RemoveColumn(column);

			if (Database != null && Database.MappingSet != null)
			{
				Database.MappingSet.RemoveMappingsContaining(column);
			}
		}

		public IKey GetKey(string keyName)
		{
			return _keys.Find(s => s.Name == keyName);
		}

		public Relationship CreateRelationshipTo(ITable targetTable)
		{
			var newRel = new RelationshipImpl();
			newRel.PrimaryTable = this;
			newRel.ForeignTable = targetTable;

			newRel.PrimaryKey = new Key(Guid.NewGuid().ToString()) { IsUserDefined = true, Keytype = DatabaseKeyType.Primary };
			newRel.ForeignKey = new Key(Guid.NewGuid().ToString()) { IsUserDefined = true, Keytype = DatabaseKeyType.Foreign };

			AddKey(newRel.PrimaryKey);
			targetTable.AddKey(newRel.ForeignKey);

			newRel.PrimaryKey.Parent = this;
			newRel.ForeignKey.Parent = targetTable;

			newRel.Database = Database;
			Database.AddRelationship(newRel);

			AddRelationship(newRel);
			targetTable.AddRelationship(newRel);

			return newRel;
		}

		public Relationship CreateRelationshipUsing(IKey thisKey, IKey otherTableKey)
		{
			var newRel = new RelationshipImpl();
			newRel.PrimaryTable = this;
			newRel.ForeignTable = otherTableKey.Parent;

			newRel.PrimaryKey = thisKey;
			newRel.ForeignKey = otherTableKey;

			AddRelationship(newRel);
			otherTableKey.Parent.AddRelationship(newRel);

			newRel.Database = Database;
			Database.AddRelationship(newRel);

			return newRel;
		}

		public ITable Clone()
		{
			Table t = new Table();
			CopyInto(t);
			return t;
		}

		public void CopyInto(ITable t)
		{
			base.CopyInto(t);
			t.Schema = this.Schema;
			t.IsView = this.IsView;
		}

		public bool HasChanges(ITable value)
		{
			if (Name.Equals(value.Name) == false)
				return true;

			return base.HasChanges(value);
		}

		public virtual void DeleteSelf()
		{
			foreach (var dirRelationship in DirectedRelationships.ToList())
			{
				dirRelationship.ToTable.RemoveRelationship(dirRelationship.Relationship);
				dirRelationship.ToTable.RemoveKey(dirRelationship.Relationship.ForeignKey);
			}

			_relationships.Clear();

			if (Database != null)
			{
				if (this.IsView)
					Database.DeleteTable(this);
				else
					Database.DeleteTable(this);
			}
		}

		public string Serialise(IDatabaseSerialisationScheme scheme)
		{
			return scheme.SerialiseTable(this);
		}

		public override bool ValidateObject(List<ValidationFailure> failures)
		{
			base.ValidateObject(failures);

			if (DatabaseContainsAnotherObjectNamedTheSameAsThis())
			{
				failures.Add(new ValidationFailure("Duplicated Name: " + Name, "Name"));
			}

			return failures.Count == 0;
		}

		private bool DatabaseContainsAnotherObjectNamedTheSameAsThis()
		{
			if (Database == null)
				return false;

			return Database.Tables.Any(so => ReferenceEquals(this, so) == false && so.Name == Name);
		}

		/// <summary>
		/// Custom comparer for Table class: compares on Name and Schema.
		/// </summary>
		public class TableComparer : IEqualityComparer<ITable>
		{
			// Tables are equal if their names and schemas are equal.
			public bool Equals(ITable x, ITable y)
			{
				// Check whether the compared objects reference the same data.
				if (Object.ReferenceEquals(x, y)) return true;

				// Check whether any of the compared objects is null.
				if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
					return false;

				//Check whether the tables' properties are equal.
				return x.Schema == y.Schema && x.Name == y.Name;
			}

			// If Equals() returns true for a pair of objects 
			// then GetHashCode() must return the same value for these objects.

			public int GetHashCode(ITable table)
			{
				//Check whether the object is null
				if (Object.ReferenceEquals(table, null)) return 0;

				//Get hash code for the Name field if it is not null.
				int hashTableName = table.Name == null ? 0 : table.Name.GetHashCode();

				//Get hash code for the Code field.
				int hashTableSchema = table.Schema == null ? 0 : table.Schema.GetHashCode();

				//Calculate the hash code for the product.
				return hashTableName ^ hashTableSchema;
			}

		}

	}
}
