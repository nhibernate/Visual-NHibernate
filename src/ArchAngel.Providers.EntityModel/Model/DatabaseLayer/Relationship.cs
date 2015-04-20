using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.EntityModel.Helper;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	[DebuggerDisplay("Source = {PrimaryTable}, Target = {ForeignTable}")]
	public class RelationshipImpl : ScriptBase, Relationship
	{
		private Cardinality _PrimaryCardinality;
		private Cardinality _ForeignCardinality;

		public RelationshipImpl()
		{
			Identifier = Guid.NewGuid();
		}

		public RelationshipImpl(Guid identifier)
		{
			Identifier = identifier;
		}

		public void CopyInto(Relationship relationship)
		{
			base.CopyInto(relationship);

			relationship.Description = this.Description; // TODO: figure out what to do here
			relationship.Enabled = this.Enabled;
			relationship.ForeignCardinality = this.ForeignCardinality;
			relationship.ForeignKey = this.ForeignKey.Clone();
			//relationship.ForeignTable
			relationship.IsUserDefined = this.IsUserDefined;
			//relationship.MappedColumns
			relationship.PrimaryCardinality = this.PrimaryCardinality;
			relationship.PrimaryKey = this.PrimaryKey.Clone();
			//relationship.PrimaryTable
			relationship.Schema = this.Schema;
			//relationship.SetForeignEnd
			//relationship.SetPrimaryEnd(
			//relationship.SourceCardinality = this.sou

		}

		public Relationship Clone()
		{
			RelationshipImpl relationship = new RelationshipImpl();

			CopyInto(relationship);

			return relationship;
		}

		public IEnumerable<MappedColumn> MappedColumns
		{
			get
			{
				// Don't worry about mismatched number of columns, a validation rule will alert the user
				int minCount = Math.Min(PrimaryKey.Columns.Count, ForeignKey.Columns.Count);

				for (int i = 0; i < minCount; i++)
				{
					yield return new MappedColumn(PrimaryKey.Columns[i], ForeignKey.Columns[i]);
				}
			}
		}

		public override string DisplayName
		{
			get { return "Relationship:" + Name; }
		}

		#region Implementation of IRelationship

		IEntity IRelationship.SourceEntity
		{
			get { return PrimaryTable; }
			set { PrimaryTable = (ITable)value; }
		}

		IEntity IRelationship.TargetEntity
		{
			get { return ForeignTable; }
			set { ForeignTable = (ITable)value; }
		}

		Cardinality IRelationship.SourceCardinality { get { return PrimaryCardinality; } set { PrimaryCardinality = value; } }
		Cardinality IRelationship.TargetCardinality { get { return ForeignCardinality; } set { ForeignCardinality = value; } }

		#endregion

		public Guid Identifier { get; set; }
		private ITable _primaryTable;
		private ITable _foreignTable;
		private IKey _primaryKey;
		private IKey _foreignKey;

		public ITable PrimaryTable
		{
			get { return _primaryTable; }
			set { _primaryTable = value; }
		}

		public ITable ForeignTable
		{
			get { return _foreignTable; }
			set { _foreignTable = value; }
		}

		public IKey PrimaryKey
		{
			get { return _primaryKey; }
			set
			{
				//if (value.Keytype != DatabaseKeyType.Primary) throw new ArgumentException("PrimaryKey must be a Primary Key");
				_primaryKey = value;
				RaisePropertyChanged("PrimaryKey");
			}
		}

		public IKey ForeignKey
		{
			get { return _foreignKey; }
			set
			{
				if (value.Keytype != DatabaseKeyType.Foreign && value.Keytype != DatabaseKeyType.Unique) throw new ArgumentException(string.Format("ForeignKey must be a Foreign Key or a Unique Key. It is a {0} key.", value.Keytype.ToString()));
				_foreignKey = value;
				RaisePropertyChanged("ForeignKey");
			}
		}

		public Cardinality PrimaryCardinality
		{
			get { return _PrimaryCardinality; }
			set
			{
				if (_PrimaryCardinality != value)
					_PrimaryCardinality = value;
			}
		}

		public Cardinality ForeignCardinality
		{
			get { return _ForeignCardinality; }
			set
			{
				if (_ForeignCardinality != value)
					_ForeignCardinality = value;
			}
		}

		public void AddThisTo(ITable source, ITable target)
		{
			PrimaryTable = source;
			ForeignTable = target;
			Database = source.Database;
			source.AddRelationship(this);
			target.AddRelationship(this);
			Database.AddRelationship(this);
		}

		public void DeleteSelf()
		{
			// Delete all mapped references
			//List<ITable> associationTables = this.GetMappingSet().GetAssociationTablesFor(this.Database).ToList();

			foreach (ITable associationTable in this.GetMappingSet().GetAssociationTablesFor(this.Database))
			{
				foreach (Relationship rel in associationTable.Relationships.Where(r => r.PrimaryTable == this.PrimaryTable || r.PrimaryTable == this.ForeignTable))
				{
					foreach (var reference in associationTable.MappedReferences())
					{
						List<ITable> sourceEntityMappedTables = ((EntityLayer.EntityImpl)reference.SourceEntity).MappedTables().ToList();
						List<ITable> targetEntityMappedTables = ((EntityLayer.EntityImpl)reference.TargetEntity).MappedTables().ToList();

						if (sourceEntityMappedTables.Contains(this.ForeignTable) ||
							sourceEntityMappedTables.Contains(this.PrimaryTable) ||
							targetEntityMappedTables.Contains(this.ForeignTable) ||
							targetEntityMappedTables.Contains(this.PrimaryTable))
						{
							reference.DeleteSelf();
						}
					}
				}
			}

			//List<EntityLayer.Reference> mappedReferences = this.MappedReferences().ToList();

			foreach (var reference in this.MappedReferences())
				reference.DeleteSelf();

			var mappingSet = this.GetMappingSet();

			if (mappingSet != null)
				mappingSet.RemoveMappingsContaining(this);

			if (PrimaryTable != null)
				PrimaryTable.RemoveRelationship(this);

			if (ForeignTable != null)
				ForeignTable.RemoveRelationship(this);

			Database.RemoveRelationship(this);
		}

		public bool HasChanges(Relationship value, out string description)
		{
			StringBuilder sb = new StringBuilder();

			// Note: IsUnique is dependant on Column changes, so we don't check it here.
			if (base.HasChanges(value))
				sb.Append("Value,");
			if (this.ForeignCardinality != null && value.ForeignCardinality != null && ForeignCardinality != value.ForeignCardinality)
				sb.Append("ForeignCardinality,");
			if (this.ForeignKey != null && value.ForeignCardinality != null && ForeignKey.Name != value.ForeignKey.Name)
				sb.Append("ForeignKeyName,");
			if (this.ForeignTable != null && value.ForeignTable != null && ForeignTable.Name != value.ForeignTable.Name)
				sb.Append("ForeignTableName,");
			if (this.PrimaryCardinality != null && value.PrimaryCardinality != null && PrimaryCardinality != value.PrimaryCardinality)
				sb.Append("PrimaryCardinality,");
			if (this.PrimaryKey != null && value.PrimaryCardinality != null && PrimaryKey.Name != value.PrimaryKey.Name)
				sb.Append("PrimaryKeyName,");
			if (this.PrimaryTable != null && value.PrimaryTable != null && PrimaryTable.Name != value.PrimaryTable.Name)
				sb.Append("PrimaryTableName,");
			if (this.Name != value.Name)
				sb.Append("Name,");
			if (this.Schema != value.Schema)
				sb.Append("Schema,");

			List<MappedColumn> myMappedColumns = MappedColumns.ToList();
			List<MappedColumn> theirMappedColumns = value.MappedColumns.ToList();

			if (myMappedColumns.Count() != theirMappedColumns.Count())
				sb.Append("CountOfColumns,");

			for (int i = 0; i < myMappedColumns.Count; i++)
			{
				MappedColumn matchingColumn = theirMappedColumns.SingleOrDefault(c => c.Source.Name == myMappedColumns[i].Source.Name && c.Target.Name == myMappedColumns[i].Target.Name);

				if (matchingColumn == null)// ||
					//myMappedColumns[i].Name != matchingColumn.Name)
					sb.Append(string.Format("MappedColumns[{0}]:[{1}],", myMappedColumns[i].Source.Name, myMappedColumns[i].Target.Name));
			}
			if (sb.Length > 0)
			{
				description = sb.ToString().TrimEnd(',');
				return true;
			}
			description = "";
			return false;
		}

		public void SetPrimaryEnd(IKey key)
		{
			var oldPrimaryTable = PrimaryTable;

			PrimaryTable = key.Parent;
			PrimaryKey = key;

			oldPrimaryTable.RemoveRelationship(this);
			PrimaryTable.AddRelationship(this);
		}

		public void SetForeignEnd(IKey key)
		{
			var oldForeignTable = ForeignTable;

			ForeignTable = key.Parent;
			ForeignKey = key;

			oldForeignTable.RemoveRelationship(this);
			ForeignTable.AddRelationship(this);
		}

		public bool Equals(RelationshipImpl other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return base.Equals(other) && Equals(other.PrimaryTable, PrimaryTable) && Equals(other.ForeignTable, ForeignTable) && Equals(other.PrimaryKey, PrimaryKey) && Equals(other.ForeignKey, ForeignKey);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as RelationshipImpl);
		}

		public override string ToString()
		{
			return string.Format("Relationship: (Name: {0}, Source: {1}, Target: {2})", Name, PrimaryTable, ForeignTable);
		}

		/// <summary>
		/// Custom comparer for Relationship class: compares on Name.
		/// </summary>
		public class RelationshipComparer : IEqualityComparer<IRelationship>
		{
			// Tables are equal if their names are equal.
			public bool Equals(IRelationship x, IRelationship y)
			{
				// Check whether the compared objects reference the same data.
				if (Object.ReferenceEquals(x, y)) return true;

				// Check whether any of the compared objects is null.
				if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
					return false;

				//Check whether the relationships' properties are equal.
				return x.Name == y.Name;
			}

			/// <summary>
			/// If Equals() returns true for a pair of objects 
			/// then GetHashCode() must return the same value for these objects.
			/// </summary>
			/// <param name="column"></param>
			/// <returns></returns>
			public int GetHashCode(IRelationship relationship)
			{
				//Check whether the object is null
				if (Object.ReferenceEquals(relationship, null)) return 0;

				//Get hash code for the Name field if it is not null.
				int hashKeyName = relationship.Name == null ? 0 : relationship.Name.GetHashCode();

				return hashKeyName;
			}
		}
	}

	public class DirectedRelationship
	{
		private readonly Relationship rel;
		private readonly ITable fromTable;

		public DirectedRelationship(Relationship rel, ITable fromTable)
		{
			this.rel = rel;
			this.fromTable = fromTable;
		}

		public bool Table1IsFromEnd { get { return rel.PrimaryTable == fromTable; } }

		public string Name { get { return rel.Name; } }

		public IEnumerable<MappedColumn> MappedColumns
		{
			get
			{
				IKey sourceKey;
				IKey targetKey;

				if (!Table1IsFromEnd)
				{
					sourceKey = FromKey;
					targetKey = ToKey;
				}
				else
				{
					sourceKey = ToKey;
					targetKey = FromKey;
				}
				for (int i = 0; i < sourceKey.Columns.Count; i++)
				{
					yield return new MappedColumn(sourceKey.Columns[i], targetKey.Columns[i]);
				}
			}
		}

		public IKey FromKey
		{
			get
			{
				return Table1IsFromEnd ? rel.PrimaryKey : rel.ForeignKey;
			}
		}

		public IKey ToKey
		{
			get
			{
				return Table1IsFromEnd ? rel.ForeignKey : rel.PrimaryKey;
			}
		}

		public Relationship Relationship
		{
			get
			{
				return rel;
			}
		}

		public ITable FromTable
		{
			get
			{
				return fromTable;
			}
		}

		public ITable ToTable
		{
			get
			{
				return Table1IsFromEnd ? rel.ForeignTable : rel.PrimaryTable;
			}
		}
	}

	public class MappedColumn
	{
		public IColumn Source { get; private set; }
		public IColumn Target { get; private set; }

		public MappedColumn(IColumn source, IColumn target)
		{
			Source = source;
			Target = target;
		}
	}
}