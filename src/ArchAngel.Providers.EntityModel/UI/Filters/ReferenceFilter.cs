using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.Providers.EntityModel.UI.Filters
{
	public class ReferenceFilter : MappingSetViewFilter
	{
		private readonly Reference reference;

		public ReferenceFilter(SchemaDiagrammerController diagramController, SchemaController schemaController, Reference reference)
			: base(diagramController, schemaController, reference.EntitySet.MappingSet)
		{
			this.reference = reference;
		}

		protected override void RunFilterImpl()
		{
			schemaController.SetEntitiesAndRelationshipsToVisible(new HashSet<IEntity>(new[] { reference.Entity1, reference.Entity2 }));
		}

	    protected override void OnReferenceRemoved(Reference removedReference)
	    {
	        if(reference.InternalIdentifier == removedReference.InternalIdentifier)
	        {
                SetAllEntitiesToVisible();
	        }
	    }

	    public override bool CanRun()
	    {
	        return reference != null;
	    }
	}
}