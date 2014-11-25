using System;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.Providers.EntityModel.Model.MappingLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	public class TableReferenceMappingImpl : ModelObject, TableReferenceMapping
	{
		private ITable fromTable;
		private Reference toReference;

		public MappingSet MappingSet { get; set; }

		public ITable FromTable
		{
			get { return fromTable; }
			set { fromTable = value; RaisePropertyChanged("FromTable"); }
		}

		public Reference ToReference
		{
			get { return toReference; }
			set { toReference = value; RaisePropertyChanged("ToReference"); }
		}

        public override string DisplayName
        {
            get { return "Mapping"; }
        }

		public void Delete()
		{
			if(MappingSet != null)
			{
				MappingSet.RemoveMapping(this);
			}
			ToReference = null;
			FromTable = null;
		}
	}
}
