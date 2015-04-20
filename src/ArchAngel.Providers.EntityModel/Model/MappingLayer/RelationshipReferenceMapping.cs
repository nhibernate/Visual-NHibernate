using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.Providers.EntityModel.Model.MappingLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	public class RelationshipReferenceMappingImpl : ModelObject, RelationshipReferenceMapping
	{
		private Relationship fromRelationship;
		private Reference toReference;

		public RelationshipReferenceMappingImpl()
		{
		}

		public MappingSet MappingSet { get; set; }

		public Relationship FromRelationship
		{
			get { return fromRelationship; }
			set { fromRelationship = value; RaisePropertyChanged("FromRelationship"); }
		}

		public Reference ToReference
		{
			get { return toReference; }
			set
			{
				toReference = value;
				RaisePropertyChanged("ToReference");
			}
		}

		public override string DisplayName
		{
			get { return "Mapping"; }
		}

		public void Delete()
		{
			if (MappingSet != null)
			{
				MappingSet.RemoveMapping(this);
			}
			ToReference = null;
			FromRelationship = null;
		}
	}
}
