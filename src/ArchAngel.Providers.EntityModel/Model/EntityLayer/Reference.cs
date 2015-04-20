using System;
using System.Collections.Generic;
using System.Diagnostics;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Attributes;
using ArchAngel.Interfaces.SchemaDiagrammer;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	[ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	[DebuggerDisplay("Name = {Name}, Source = {Entity1}, Target = {Entity2}")]
	public class ReferenceImpl : ModelObject, Reference
	{
		private Entity entity1;
		private Entity entity2;
		private string name;
		private Cardinality cardinality1;
		private Cardinality cardinality2;
		private bool end1Enabled;
		private bool end2Enabled;
		private string end1Name;
		private string end2Name;
		private IList<string> _OldEnd1Names = new List<string>();
		private IList<string> _OldEnd2Names = new List<string>();

		public ReferenceImpl()
		{
			Identifier = Guid.NewGuid();
		}

		public override string DisplayName
		{
			get { return "Reference:" + Name; }
		}

		public ReferenceImpl(Guid identifier)
		{
			Identifier = identifier;
		}

		public ReferenceImpl(Entity entity1, Entity entity2)
		{
			Identifier = Guid.NewGuid();
			this.entity1 = entity1;
			this.entity2 = entity2;
		}

		public Guid Identifier { get; set; }
		public EntitySet EntitySet { get; set; }
		public bool IncludeForeignKey { get; set; }
		public DatabaseLayer.Key MappedTableKey { get; set; }

		public Entity Entity1
		{
			get
			{
				return entity1;
			}
			set
			{
				entity1 = value;
				RaisePropertyChanged("Entity1");
			}
		}

		public Entity Entity2
		{
			get
			{
				return entity2;
			}
			set
			{
				entity2 = value;
				RaisePropertyChanged("Entity2");
			}
		}

		public bool SelfReference { get { return entity1 == entity2; } }

		public Cardinality Cardinality1
		{
			get
			{
				return cardinality1;
			}
			set
			{
				cardinality1 = value;
				RaisePropertyChanged("Cardinality1");
			}
		}

		public Cardinality Cardinality2
		{
			get
			{
				return cardinality2;
			}
			set
			{
				cardinality2 = value;
				RaisePropertyChanged("Cardinality2");
			}
		}

		public bool End1Enabled
		{
			get
			{
				return end1Enabled;
			}
			set
			{
				end1Enabled = value;
				RaisePropertyChanged("End1Enabled");
			}
		}

		public bool End2Enabled
		{
			get
			{
				return end2Enabled;
			}
			set
			{
				end2Enabled = value;
				RaisePropertyChanged("End2Enabled");
			}
		}

		public string End1Name
		{
			get
			{
				return end1Name;
			}
			set
			{
				if (end1Name != value)
				{
					if (OldEnd1Names.Count == 0 && !string.IsNullOrEmpty(value))
						OldEnd1Names.Add(value);

					end1Name = value;
					RaisePropertyChanged("End1Name");
					RaisePropertyChanged("Name");
				}
			}
		}

		public string End2Name
		{
			get
			{
				return end2Name;
			}
			set
			{
				if (end2Name != value)
				{
					if (OldEnd2Names.Count == 0 && !string.IsNullOrEmpty(value))
						OldEnd2Names.Add(value);

					end2Name = value;
					RaisePropertyChanged("End2Name");
					RaisePropertyChanged("Name");
				}
			}
		}

		public IList<string> OldEnd1Names
		{
			get { return _OldEnd1Names; }
		}

		public IList<string> OldEnd2Names
		{
			get { return _OldEnd2Names; }
		}

		IEntity IRelationship.SourceEntity
		{
			get { return Entity1; }
			set
			{
				if (value is Entity)
					Entity1 = value as Entity;
				throw new ArgumentException("Cannot set PrimaryTable on a Reference to anything but an Entity");
			}
		}

		IEntity IRelationship.TargetEntity
		{
			get { return Entity2; }
			set
			{
				if (value is Entity)
				{
					Entity2 = value as Entity;
				}
				throw new ArgumentException("Cannot set ForeignTable on a Reference to anything but an Entity");
			}
		}

		string IRelationship.Name
		{
			get { return "Relationship between " + Entity1.Name + " and " + Entity2.Name; }
			set { }
		}

		[IntelliSenseVisibility(Visible = false)]
		public Cardinality SourceCardinality
		{
			get { return Cardinality1; }
			set { Cardinality2 = value; RaisePropertyChanged("SourceCardinality"); }
		}

		[IntelliSenseVisibility(Visible = false)]
		public Cardinality TargetCardinality
		{
			get { return Cardinality2; }
			set { Cardinality2 = value; RaisePropertyChanged("TargetCardinality"); }
		}

		public string Name
		{
			get { return name; }
			set { name = value; RaisePropertyChanged("Name"); }
		}

		public void AddThisTo(Entity entity_1, Entity entity_2)
		{
			Entity1 = entity_1;
			Entity2 = entity_2;

			entity_1.AddReference(this);
			if (entity_1 != entity_2)
				entity_2.AddReference(this);

			if (EntitySet != null)
				EntitySet.AddReference(this);
		}

		public void DeleteSelf()
		{
			if (Entity1 != null)
				Entity1.RemoveReference(this);

			if (Entity2 != null)
				Entity2.RemoveReference(this);

			Entity1 = null;
			Entity2 = null;

			if (EntitySet != null)
				EntitySet.DeleteReference(this);

			EntitySet = null;
		}

		public bool Equals(ReferenceImpl other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.entity1, entity1) && Equals(other.entity2, entity2)
				&& Equals(other.end2Name, end2Name) && Equals(other.end1Name, end1Name)
				&& Equals(other.MappedRelationship(), this.MappedRelationship());
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(ReferenceImpl)) return false;
			return Equals((ReferenceImpl)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = (entity1 != null ? entity1.GetHashCode() : 0);
				result = (result * 397) ^ (entity2 != null ? entity2.GetHashCode() : 0);
				result = (result * 397) ^ (end2Name != null ? end2Name.GetHashCode() : 0);
				result = (result * 397) ^ (end1Name != null ? end1Name.GetHashCode() : 0);
				return result;
			}
		}
	}

	[DebuggerDisplay("FromEntity = {FromEntity}, ToEntity = {ToEntity}, FromName = {FromName}, ToName = {ToName}")]
	public class DirectedReference
	{
		private readonly Entity fromEntity;
		private readonly Reference reference;
		private readonly bool fromIsEnd1;

		public DirectedReference(Entity fromEntity, Reference reference)
		{
			this.fromEntity = fromEntity;
			this.reference = reference;

			fromIsEnd1 = reference.Entity1 == fromEntity;
		}

		public DirectedReference(Entity fromEntity, Reference reference, bool selfReferenceSwitchEnds)
		{
			this.fromEntity = fromEntity;
			this.reference = reference;

			if (reference.SelfReference)
			{
				fromIsEnd1 = !selfReferenceSwitchEnds;
			}
		}

		public bool SelfReference { get { return reference.SelfReference; } }
		public bool Entity1IsFromEnd { get { return fromIsEnd1; } }

		public Entity FromEntity
		{
			get { return fromEntity; }
		}

		public Entity ToEntity
		{
			get { return fromIsEnd1 ? reference.Entity2 : reference.Entity1; }
		}

		public string FromName
		{
			get { return fromIsEnd1 ? reference.End1Name : reference.End2Name; }
		}

		public IList<string> OldFromNames
		{
			get { return fromIsEnd1 ? reference.OldEnd1Names : reference.OldEnd2Names; }
		}

		public string ToName
		{
			get { return fromIsEnd1 ? reference.End2Name : reference.End1Name; }
		}

		public IList<string> OldToNames
		{
			get { return fromIsEnd1 ? reference.OldEnd2Names : reference.OldEnd1Names; }
		}

		public bool FromEndEnabled
		{
			get { return fromIsEnd1 ? reference.End1Enabled : reference.End2Enabled; }
		}

		public bool ToEndEnabled
		{
			get { return fromIsEnd1 ? reference.End2Enabled : reference.End1Enabled; }
		}

		public Cardinality FromEndCardinality
		{
			get { return fromIsEnd1 ? reference.Cardinality1 : reference.Cardinality2; }
		}

		public Cardinality ToEndCardinality
		{
			get { return fromIsEnd1 ? reference.Cardinality2 : reference.Cardinality1; }
		}

		public Reference Reference
		{
			get { return reference; }
		}
	}
}
