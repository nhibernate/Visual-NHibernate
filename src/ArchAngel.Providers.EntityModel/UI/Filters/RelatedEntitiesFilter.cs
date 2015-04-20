using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using SchemaDiagrammer.Controller;

namespace ArchAngel.Providers.EntityModel.UI.Filters
{
	public class RelatedEntitiesFilter : MappingSetViewFilter
	{
		private readonly Entity entity;

		public int DegreeOfRelationshipsToShow = 1;
		public bool ShowTables;

		public RelatedEntitiesFilter(SchemaDiagrammerController diagramController, SchemaController schemaController, Entity entity)
		: base(diagramController, schemaController, entity.EntitySet.MappingSet)
		{
			this.entity = entity;
		}

		protected override void RunFilterImpl()
		{
			IEnumerable<Entity> relatedObjects = entity.EntitySet.GetRelatedEntities(entity, DegreeOfRelationshipsToShow);

			var hashSet = new HashSet<IEntity>(relatedObjects.Cast<IEntity>());
			schemaController.SetEntitiesAndRelationshipsToVisible(hashSet);

			// Add Parents
			foreach(var ent in relatedObjects)
			{
				if (ent.Parent == null) continue;
				if(hashSet.Contains(ent) && hashSet.Contains(ent.Parent))
				{
					schemaController.SetVisibilityOfVirtualRelationship(ent.Parent, ent, Visibility.Visible);
				}
			}

			if (!ShowTables) return;

			// Get the mapped tables for the primary entity we are showing.
			var mappings = mappingSet.GetMappingsContaining(entity);

			HashSet<IEntity> tables = new HashSet<IEntity>(mappings.Select(m => m.FromTable).Cast<IEntity>());
			schemaController.SetEntitiesAndRelationshipsToVisible(tables);

			foreach(var m in mappings)
			{
				schemaController.SetVisibility(m, Visibility.Visible);
			}
		}

	    public override bool CanRun()
	    {
	        return entity != null && entity.EntitySet != null && mappingSet != null;
	    }
	}
}
